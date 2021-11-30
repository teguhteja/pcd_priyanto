using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.CvEnum;
using System.Threading;
using System.Xml;
using System.Diagnostics;

namespace Object_Tracking
{
    public partial class Form1 : Form
    {
                
        private int _width = 480;
        private int _height = 360;
        private Point rectPoint1;
        private Point rectPoint2;
        private Boolean first = true;
        private Boolean firstCheck = true;
        Capture GTcapture;
        Image<Bgr, Byte> GTcaptureFrame;

        private Boolean colorTexture;
        XmlWriterSettings settings = new XmlWriterSettings();
        XmlTextReader gtReader;
        XmlWriter writer;

        private Graphics graphic, colorGraphic, colorTextureGraphic;
        VideoWriter video;

        Stopwatch stopwatch;

        private Boolean drawRect;
        private Boolean dragRect;

        private int windowSize =5;
        private int redBins=8, greenBins=8, blueBins = 8;
        private int lbpRiu5 = 5;
        private double[, , ,] histo = new double[8,8,8,5];

        private int hueBins = 8;
        private int satBins = 2;       
        private int valBinst = 4;

        private Image<Bgr, Byte> rgbFrame;
       
        private Image<Hsv, Byte> hsvFrame;

        private Image<Gray, Byte> hueFrame;

        private Image<Gray, Byte> lbpRiu;

        private Image<Gray, Byte> lbpGlobal;
        private Image<Gray, Byte> hueGlobal;

        private Thread colorTextureThread;

        private DenseHistogram lbpHist = new DenseHistogram(256, new RangeF(0.0f, 255.0f));
      
        private DenseHistogram hueHist;
        private DenseHistogram satHist;
        private DenseHistogram valHist;
        private DenseHistogram hueHist2;

        private List<Rectangle> listRect1 = new List<Rectangle>();
        private List<Rectangle> gtRectList = new List<Rectangle>();

        private List<double> quList = new List<double>();
        private List<double> puList = new List<double>();

        private int totalBins;
        
        private Image<Gray, Byte> lbpImage;
        private Image<Gray, Byte> searchWindow;
        private Image<Gray, Byte> nullWindow;

        private double threshold =1;
        private double th = 1;
        private double minDist = 0.1;
        private int maxIteration = 15;
        private double fail = 0;
        private double value = 1;
        private Rectangle rects;
        private Rectangle rects22;
        private Rectangle rects23;
        private int incre = 5;
        private int frameNumber = 1;
        int univIter = 2700;
        private Boolean isLost = false;

        int allIter = 0;
        public Form1()
        {
            InitializeComponent();
            drawRect = false;
            dragRect = false;
            first = true;
            rgbFrame = new Image<Bgr, byte>(_width, _height);
            hsvFrame = new Image<Hsv, byte>(_width, _height);
            lbpImage = new Image<Gray, Byte>(_width, _height);
            searchWindow = new Image<Gray, Byte>(_width, _height);
            hueFrame = new Image<Gray, Byte>(_width, _height);
            nullWindow = new Image<Gray, Byte>(_width, _height);
            lbpRiu = new Image<Gray, Byte>(_width, _height);
            lbpGlobal = new Image<Gray, Byte>(_width, _height);
            hueGlobal = new Image<Gray, Byte>(_width, _height);
            settings.Indent = true;

            hueHist = new DenseHistogram(8, new RangeF(0, 255));
            satHist = new DenseHistogram(satBins, new RangeF(0, 256));
            valHist = new DenseHistogram(valBinst, new RangeF(0, 256));
            hueHist2 = new DenseHistogram(256, new RangeF(0, 255));

            totalBins = redBins * greenBins * blueBins * lbpRiu5;         
           
        }

        private void loadBtn_Click(object sender, EventArgs e)
        {
            ofd.ShowDialog();
          
        }

        private void ofd_FileOk(object sender, CancelEventArgs e)
        {


            Capture  _capture = new Capture(ofd.FileName);
            try
            {
                _capture = new Capture(ofd.FileName);
            }
            catch (NullReferenceException ex)
            {   //show errors if there is any
                MessageBox.Show(ex.Message);
            }

            GTcapture = new Capture(ofd.FileName);
            try
            {
                GTcapture = new Capture(ofd.FileName);
            }
            catch (NullReferenceException ex)
            {   //show errors if there is any
                MessageBox.Show(ex.Message);
            }
            GTcaptureFrame = GTcapture.QueryFrame();

            Image<Bgr, Byte> captureFrame = _capture.QueryFrame();
                     
            CvInvoke.cvResize(captureFrame, rgbFrame, INTER.CV_INTER_LINEAR);

            convertToHSV(rgbFrame,hsvFrame);

            pictureBox2.Image = hsvFrame[0].ToBitmap();
            pictureBox3.Image = hsvFrame[1].ToBitmap();
            pictureBox4.Image = hsvFrame[2].ToBitmap();

            doLBP(rgbFrame.Convert<Gray, Byte>(), lbpImage);
            doLBPRIU5(rgbFrame.Convert<Gray, Byte>(), lbpRiu);
            pictureBox1.Image = rgbFrame.ToBitmap();
            maskingLBPRiu(lbpRiu, rgbFrame);

            _capture.Dispose();

        }

        private void toGrayScale(Image<Bgr, byte> src)
        {
            
            Image<Gray,int> gray = new Image<Gray, int>(src.Width, src.Height);
            for (int i = 0; i < gray.Height; i++)
            {
                for (int j = 0; j < gray.Width; j++)
                {
                    gray.Data[i, j, 0] = ((int)(src.Data[i, j, 0] * 0.144) + (int)(src.Data[i, j, 1] * 0.587) +
                                        (int)(src.Data[i, j, 0] * 0.299));
                }
            }
            pictureBox1.Image = gray.ToBitmap();
        }



        private void button2_Click(object sender, EventArgs e)
        {
            lbpHist.Clear();
            hueHist.Clear();
            satHist.Clear();
            valHist.Clear();
            hueHist2.Clear();
            quList.Clear();
            puList.Clear();

            //rects = gtRectList[0];
            univIter = 7000;

            Image<Bgr, byte> rgbBlock = new Image<Bgr, byte>(rects.Width, rects.Height);
            Image<Gray, byte> lbpBlock = new Image<Gray, byte>(rects.Width, rects.Height);
          
            Array.Clear(histo, 0, totalBins);
           
            CvInvoke.cvSetImageROI(lbpImage, rects);
            CvInvoke.cvSetImageROI(lbpRiu, rects);
            CvInvoke.cvSetImageROI(rgbFrame, rects);
            CvInvoke.cvSetImageROI(hsvFrame, rects);
            rgbFrame.CopyTo(rgbBlock);
            lbpRiu.CopyTo(lbpBlock);
            calculateJointColorTextureModel(rgbBlock, lbpBlock,rects);
            convertHistoIntoVector(quList);


            lbpHist.Calculate(new Image<Gray, Byte>[] { lbpImage }, true, null);

            hueHist.Calculate(new Image<Gray, Byte>[] { hsvFrame[0] }, true,null);
            satHist.Calculate(new Image<Gray, Byte>[] { hsvFrame[1] }, true, null);
            valHist.Calculate(new Image<Gray, Byte>[] { hsvFrame[2] }, true, null);
            hueHist2.Calculate(new Image<Gray, Byte>[] { hsvFrame[0] }, true, null);

               
            CvInvoke.cvResetImageROI(rgbFrame);
            CvInvoke.cvResetImageROI(lbpImage);
            CvInvoke.cvResetImageROI(hsvFrame);

            rgbBlock.Dispose();
            lbpBlock.Dispose();
            
        }


        private void convertToHSV(Image<Bgr, Byte> img, Image<Hsv, Byte> dst)
        {
            Emgu.CV.CvInvoke.cvCvtColor(img, dst, COLOR_CONVERSION.CV_BGR2HSV);

        }


        private Image<Hsv, Byte> meanShiftHSV(Image<Hsv, Byte> img, Image<Hsv, Byte> _frame, int sp, int dp, int maxLevel)
        {

            Emgu.CV.CvInvoke.cvPyrMeanShiftFiltering(img, _frame, sp, dp, maxLevel, new MCvTermCriteria(5, 1));
            return _frame;
        }
      
        private void doLBP(Image<Gray, Byte> frame, Image<Gray, Byte> dst)
        {
            //CvInvoke.cvEqualizeHist(frame, frame);

            int lbp;
            int centerData;

              int[] tab1 = new int[40] {7,22,148,208,224,104,41,11
                                    ,23,150,212,240,232,105,43,15
                                    ,151,214,244,248,233,107,47,31
									,3,6,20,144,192,96,40,9
									,215,246,252,249,235,111,63,159
                                    };

          

            for (int i = 1; i < _height - 1; i++)
            {
                for (int j = 1; j < _width - 1; j++)
                {
                     
                    lbp = 0;
                    centerData = frame.Data[i, j, 0];

                    if (frame.Data[i - 1, j - 1, 0] >= centerData)
                    {
                        lbp += 1;
                    }

                    if (frame.Data[i - 1, j, 0] >= centerData)
                    {
                        lbp += 2;
                    }

                    if (frame.Data[i - 1, j + 1, 0] >= centerData)
                    {
                        lbp += 4;
                    }

                    if (frame.Data[i, j - 1, 0] >= centerData)
                    {
                        lbp += 8;
                    }


                    if (frame.Data[i, j + 1, 0] >= centerData)
                    {
                        lbp += 16;
                    }

                    if (frame.Data[i + 1, j - 1, 0] >= centerData)
                    {
                        lbp += 32;
                    }

                    if (frame.Data[i + 1, j, 0] >= centerData)
                    {
                        lbp += 64;
                    }

                    if (frame.Data[i + 1, j + 1, 0] >= centerData)
                    {
                        lbp += 128;
                    }


                    //if (!tab1.Contains(lbp))
                    //{
                    //    lbp = 0;
                    //}

                    dst.Data[i, j, 0] = (Byte)lbp;

                }
            }
         
        }



        private void doLBPRIU5(Image<Gray, Byte> frame, Image<Gray, Byte> dst)
        {
            //CvInvoke.cvEqualizeHist(frame, frame);

            int lbp;
            int centerData;

            int[] tab1 = new int[8] {7,22,148,208,224,104,41,11};
            int[] tab2 = new int[8] {23,150,212,240,232,105,43,15};
            int[] tab3 = new int[8] {151,214,244,248,233,107,47,31};
            int[] tab4 = new int[8] {3,6,20,144,192,96,40,9};
            int[] tab5 = new int[8] {215,246,252,249,235,111,63,159};




            for (int i = 1; i < _height - 1; i++)
            {
                for (int j = 1; j < _width - 1; j++)
                {

                    lbp = 0;
                    centerData = frame.Data[i, j, 0];

                    if (frame.Data[i - 1, j - 1, 0] >= centerData)
                    {
                        lbp += 1;
                    }

                    if (frame.Data[i - 1, j, 0] >= centerData)
                    {
                        lbp += 2;
                    }

                    if (frame.Data[i - 1, j + 1, 0] >= centerData)
                    {
                        lbp += 4;
                    }

                    if (frame.Data[i, j - 1, 0] >= centerData)
                    {
                        lbp += 8;
                    }


                    if (frame.Data[i, j + 1, 0] >= centerData)
                    {
                        lbp += 16;
                    }

                    if (frame.Data[i + 1, j - 1, 0] >= centerData)
                    {
                        lbp += 32;
                    }

                    if (frame.Data[i + 1, j, 0] >= centerData)
                    {
                        lbp += 64;
                    }

                    if (frame.Data[i + 1, j + 1, 0] >= centerData)
                    {
                        lbp += 128;
                    }


                    if (tab1.Contains(lbp))
                    {
                        lbp = 1;
                    }
                    else if (tab2.Contains(lbp))
                    {
                        lbp = 2;
                    }
                    else if (tab3.Contains(lbp))
                    {
                        lbp = 3;
                    }
                    else if (tab4.Contains(lbp))
                    {
                        lbp = 4;
                    }
                    else if (tab5.Contains(lbp))
                    {
                        lbp = 5;
                    }
                    else
                    {
                        lbp = 0;
                    }


                    dst.Data[i, j, 0] = (Byte)lbp;

                }
            }

        }

      

        private void calculateJointColorTextureModel(Image <Bgr,byte> rgb,Image <Gray,byte> gray, Rectangle rect)
        {
            Array.Clear(histo, 0, redBins * greenBins * blueBins * lbpRiu5);
            double wmax = Math.Pow((rect.Width / 2), 2) + Math.Pow((rect.Height / 2), 2)+1;
            double d = 0;
            double w = 0;
            int r, g, b, t = 0;
            int lbpData=0;
            for (int i = 0; i < rgb.Height; i++)
            {
                for (int j = 0; j < rgb.Width; j++)
                {
                    lbpData = gray.Data[i, j, 0];
                    if (lbpData != 0)
                    {
                        d = Math.Pow((i - rect.Height/2), 2) + Math.Pow((j - rect.Width/2), 2);
                        w = wmax - d;
                        r = (int)(rgb.Data[i, j, 2] / (256 / redBins));
                        g = (int)(rgb.Data[i, j, 1] / (256 / greenBins));
                        b = (int)(rgb.Data[i, j, 0] / (256 / blueBins));
                        t = lbpData-1;
                       
                        histo[r, g, b, t] = histo[r, g, b, t] + w;
                        
                    }
                    
                }
            }
        }


        private void convertHistoIntoVector(List<double> list)
        {
           double sum_q = 0;
            int index = 0;
            for (int i = 0; i < redBins; i++)
            {
                for (int j = 0; j < greenBins; j++)
                {
                    for (int k = 0; k < blueBins; k++)
                    {
                        for (int l = 0; l < lbpRiu5; l++)
                        {
                          
                          
                            list.Add(histo[i, j, k, l]);
                            sum_q = sum_q + histo[i, j, k, l];
                        }
                    }
                }
            }
          
          
            for (int i = 0; i < totalBins; i++)
            {
                list[i] = list[i] / sum_q;
              
            }

        }

        private Image<Gray, byte> setBackProjection(Image<Gray, byte> hue, Image<Gray, byte> sat,
            Image<Gray, byte> val)
        {
            for (int i = 0; i < hue.Height; i++)
            {
                for (int j = 0; j < hue.Width; j++)
                {

                    //Console.WriteLine((sat.Data[i, j, 0]/255) + "----" + val.Data[i, j, 0] +"--"+ ((0.8 * val.Data[i, j, 0] / 255)));
                    if (sat.Data[i, j, 0]/255 < (1 - (0.8 * val.Data[i, j, 0] / 255)))
                    {
                        sat.Data[i, j, 0] = val.Data[i, j, 0];

                    }

                }
            }

            return (hue.And(sat).And(val));
           
        }
    
        private Boolean isInside(int i, int j)
        {
            if (i >= 0 && (i + rects.Width) <= _width && j >= 0 && (j + rects.Height) <= _height)
            {
                return true;
            }
            return false;
        }

        private Rectangle cekRectangle(Rectangle rect)
        {
            int x = rect.X;
            int y = rect.Y;
            


            if (x < 0)
            {
                rect.X = 0;
            }
            else if (x + rect.Width > _width || x>Width)
            {
                rect.X = _width - rect.Width;
            }

            if (y < 0)
            {
                rect.Y = 0;
            }
            else if (y + rect.Height> _height || y>Width)
            {
                rect.Y = _height - rect.Height;
            }
            return rect;
        }

        private void maskingLbp(Image<Gray, Byte> _lbp, Image<Gray, Byte> mask)
        {
            for (int i = 0; i < _height; i++)
            {
                for (int j = 0; j < _width; j++)
                {
                    if (mask.Data[i, j, 0] <255)
                    {
                        _lbp.Data[i, j, 0] = 0;
                    }
                }
            }

        }

        private void maskingLBPRiu(Image<Gray, Byte> _lbp, Image<Bgr, Byte> rgb)
        {
            for (int i = 0; i < _height; i++)
            {
                for (int j = 0; j < _width; j++)
                {
                    if (_lbp.Data[i,j,0]==0)
                    {
                        rgb.Data[i, j, 0] = 0;
                        rgb.Data[i, j, 1] = 0;
                        rgb.Data[i, j, 2] = 0;
                    }
                }
            }

        }

        private void cekPixel(Point seed)
        {

            int a5 = hueFrame.Data[seed.Y, seed.X, 0];
            Console.WriteLine("a5 " + a5);
          
        }
     
        private Image<Bgr, Byte> createHistogram(DenseHistogram _histo)
        {
            double value;
            int normalized;
             MCvScalar scalar = new MCvScalar(255, 255, 255);
            Image<Bgr, Byte> img = new Image<Bgr, byte>(_width, _height);
            for (int i = 0; i <8; i++) {
                value=CvInvoke.cvQueryHistValue_1D(_histo,i);
                normalized = (int) value *200/255;
                CvInvoke.cvLine(img, new Point(i*20+20, _height), new Point(i, _height - normalized),scalar,10,LINE_TYPE.EIGHT_CONNECTED,2);
                //cvLine(imgHistogram, cvPoint(i, 200), cvPoint(i, 200 - normalized), CV_RGB(0, 0, 0));
                //printf("%d\n", normalized);
            }
            return img;
        }

        private void calculateSizeAndOrientation(Image<Gray, Byte> src,Rectangle rect,
            out MCvConnectedComp comp,out MCvBox2D trackBox)
        {
           
          
           comp = new MCvConnectedComp();
           MCvMoments moment = new MCvMoments();

           CvInvoke.cvMoments(src, ref moment, 0);
                
            trackBox = new MCvBox2D();

            double m00 = 0, m01 = 0, m10 = 0,
                   s = 0,
                   m20 = 0, m02 = 0, m11 = 0,
                   a = 0, b = 0, c = 0, angle = 0,
                   theta =0, length = 0, width = 0,
                   rotate_a, rotate_c, xc = 0, yc = 0,
                   cs=0,sn=0,square=0;

            Boolean swap = false;

            m00 = moment.m00;
            m10 = moment.m10;
            m01 = moment.m01;

            m20 = moment.mu20;
            m02 = moment.mu02;

            m11 = moment.mu11;
                   
  
            xc = (int)Math.Round(m10 / m00 + rect.X) ;
            yc = (int)Math.Round(m01 / m00 +rect.Y);


            a = (m20 / m00);
            c = (m02 / m00);
            b = (m11 / m00);

            square = Math.Sqrt(4 * b * b + (a - c) * (a - c));
            theta = Math.Atan2( 2 * b, a - c + square );

            cs = Math.Cos(theta);
            sn = Math.Sin(theta);

            rotate_a = cs * cs * m20 + 2 * cs * sn * m11 + sn * sn * m02;
            rotate_c = sn * sn * m20 - 2 * cs * sn * m11 + cs * cs * m02;

            length = Math.Sqrt(rotate_a/m00) * 4;
            width = Math.Sqrt(rotate_c/m00) * 4;
      
            if (length < width)
            {
                double t;
                t = width;
                width = length;
                length = t;

                t = cs;
                cs = sn;
                sn = t;

                swap = true;
                Console.WriteLine("swap");
                theta = Math.PI * 0.5 - theta;
            }

            int t0, t1;
            int _xc = (int)Math.Round( xc );
            int _yc = (int) Math.Round( yc );

            t0 = (int) Math.Round( Math.Abs( length * cs ));
            t1 = (int) Math.Round( Math.Abs( width * sn ));        
            
            t0 = Math.Max( t0, t1 ) + 2;
            comp.rect.Width = Math.Min( t0, (_width - _xc) * 2 );

            t0 = (int) Math.Round( Math.Abs( length * sn ));
            t1 = (int) Math.Round( Math.Abs( width * cs ));

            t0 = Math.Max( t0, t1 ) + 2;
            comp.rect.Height = Math.Min( t0, (_height - _yc) * 2 );

            comp.rect.X = Math.Max(0, _xc - comp.rect.Width / 2);
            comp.rect.Y = Math.Max(0, _yc - comp.rect.Height / 2);

            comp.rect.Width = Math.Min(_width - comp.rect.X, comp.rect.Width);
            comp.rect.Height = Math.Min(_height - comp.rect.Y, comp.rect.Height);
            comp.area = (float)m00;
         
            trackBox.size.Height = (float)length;
            trackBox.size.Width = (float) width;

            if (comp.rect.Height<comp.rect.Width)
            {
                  theta = Math.PI * 0.5 - theta;
                  Console.WriteLine("swithc");
            }
           

            angle = (float)((Math.PI * 0.5 + theta) * 180 / Math.PI);
            double angles = theta * (180 / Math.PI);

            trackBox.angle = (float)angle;
           
            while (trackBox.angle < 0)
                trackBox.angle += 360;
            while (trackBox.angle >= 360)
                trackBox.angle -= 360;
            if (trackBox.angle >= 180)
                trackBox.angle -= 180;

            Console.WriteLine(angles + "/" + trackBox.angle+"/"+"/"+angle);
            trackBox.center = new PointF(comp.rect.X + comp.rect.Width*0.5f, comp.rect.Y+comp.rect.Height*0.5f);
            trackBox.size.Width = (float)width;
            trackBox.size.Height = (float)length;
           
        }


        private void doTracking(Image<Gray, Byte> src, Image<Gray, Byte> newBlock, DenseHistogram denseHistogram1,
            DenseHistogram denseHistogram2, Rectangle rect, double minTh, int counter)
        {

            denseHistogram2.Clear();

            rect = cekRectangle(rect);

                if (!listRect1.Contains(rect) || counter<1)
                {

                    listRect1.Add(rect);

                    CvInvoke.cvSetImageROI(src,rect);
                    //CvInvoke.cvSetImageROI(searchWindow, rect);

                    src.CopyTo(newBlock);
                    //src.CopyTo(searchWindow);

                    CvInvoke.cvResetImageROI(src);
                    //CvInvoke.cvResetImageROI(searchWindow);

                    denseHistogram2.Calculate(new Image<Gray, Byte>[] { newBlock }, true, null);

                  
                    value = CvInvoke.cvCompareHist(denseHistogram1, denseHistogram2, HISTOGRAM_COMP_METHOD.CV_COMP_BHATTACHARYYA);
                    //Console.WriteLine(counter + "-"+val+"-"+minTh);
                    if (counter <= maxIteration || value<th)
                    {
                     

                        if (value < th) // get best rect position
                        {
                            //Console.WriteLine("new val  " + value + "----" + th);

                            rects = rect;
                            th = value;
                            isLost = false;
                           

                        }
                        else if (isLost)
                        {
                            fail = value;
                         
                        }

                        int _x = rect.X;
                        int _y = rect.Y;

                        //NBR1
                        rect.X = _x - windowSize;
                        rect.Y = _y - windowSize;


                        doTracking(src,newBlock,denseHistogram1,denseHistogram2,rect,
                            minTh, counter + 1);

                        //NBR2
                        rect.X = _x;
                        rect.Y = _y - windowSize;

                        doTracking(src, newBlock, denseHistogram1, denseHistogram2, rect,
                           minTh, counter + 1);

                        //NBR3
                        rect.X = _x + windowSize;
                        rect.Y = _y - windowSize;

                        doTracking(src, newBlock, denseHistogram1, denseHistogram2, rect,
                           minTh, counter + 1);

                        //NBR4
                        rect.X = _x - windowSize;
                        rect.Y = _y;

                        doTracking(src, newBlock, denseHistogram1, denseHistogram2, rect,
                            minTh, counter + 1);

                        //NBR6
                        rect.X = _x + windowSize;
                        rect.Y = _y;

                        doTracking(src, newBlock, denseHistogram1, denseHistogram2, rect,
                           minTh, counter + 1);

                        //NBR7
                        rect.X = _x - windowSize;
                        rect.Y = _y + windowSize;

                        doTracking(src, newBlock, denseHistogram1, denseHistogram2, rect,
                            minTh, counter + 1);

                        //NBR8
                        rect.X = _x;
                        rect.Y = _y + windowSize;

                        doTracking(src, newBlock, denseHistogram1, denseHistogram2, rect,
                            minTh, counter + 1);

                        //NBR9
                        rect.X = _x + windowSize;
                        rect.Y = _y + windowSize;

                        doTracking(src, newBlock, denseHistogram1, denseHistogram2, rect,
                           minTh, counter + 1);

                    }

                }

        }

        private double matchHistogram(Image<Gray, Byte> src, List<Rectangle> _listRect1,
             out List<Rectangle> listRect2, DenseHistogram denseHistogram1, DenseHistogram denseHistogram2, Rectangle rect)
        {

            listRect2 = _listRect1;

            double _value = threshold;
            if (!_listRect1.Contains(rect))
            {

                listRect2.Add(rect);
                CvInvoke.cvSetImageROI(src, rect);
                //CvInvoke.cvSetImageROI(searchWindow, rect);
                //src.CopyTo(searchWindow);
                //CvInvoke.cvResetImageROI(searchWindow);

                denseHistogram2.Clear();
                denseHistogram2.Calculate(new Image<Gray, Byte>[] { src }, true, null);
                CvInvoke.cvResetImageROI(src);
                _value = CvInvoke.cvCompareHist(denseHistogram1, denseHistogram2,
                    HISTOGRAM_COMP_METHOD.CV_COMP_BHATTACHARYYA);

            }

            return _value;


        }

        private void getBestRectangle(double oldValue, double newValue, out double bestValue,
            Rectangle oldRectangle, Rectangle newRectangle, out Rectangle bestRectangle)
        {
            bestValue = oldValue;
            bestRectangle = oldRectangle;
            if (newValue < oldValue)
            {
                bestValue = newValue;
                bestRectangle = newRectangle;

            }
            //bcTb.Invoke(new UpdateTextBox(this.UpdateTextbox),
            //    new object[] { bcTb, (1 - bestValue).ToString() });
            //;
        }

        private void doTracking2(Image<Gray, Byte> src, List<Rectangle> _listRect1,
            DenseHistogram denseHistogram1, DenseHistogram denseHistogram2, Rectangle rect,
            double minTh, out double bestValue, out Rectangle bestRectangle)
        {


            double _value = minTh;
            int _x = rect.X;
            int _y = rect.Y;
            int _windowSize;

            bestRectangle = rect;
            bestValue = minTh;

            _value = matchHistogram(src, _listRect1, out _listRect1, denseHistogram1, denseHistogram2, rect);
            getBestRectangle(bestValue, _value, out bestValue, bestRectangle, rect, out bestRectangle);


            for (int i = 1; i <= maxIteration; i++)
            {

                _windowSize = windowSize * i;
                //NBR1

                rect.X = _x - _windowSize;
                rect.Y = _y - _windowSize;
                rect = cekRectangle(rect);
                _value = matchHistogram(src, _listRect1, out _listRect1, denseHistogram1, denseHistogram2, rect);
                getBestRectangle(bestValue, _value, out bestValue, bestRectangle, rect, out bestRectangle);


                //NBR2
                rect.X = _x;
                rect.Y = _y - _windowSize;
                rect = cekRectangle(rect);
                _value = matchHistogram(src, _listRect1, out _listRect1, denseHistogram1, denseHistogram2, rect);
                getBestRectangle(bestValue, _value, out bestValue, bestRectangle, rect, out bestRectangle);

                //NBR3
                rect.X = _x + _windowSize;
                rect.Y = _y - _windowSize;
                rect = cekRectangle(rect);

                _value = matchHistogram(src, _listRect1, out _listRect1, denseHistogram1, denseHistogram2, rect);
                getBestRectangle(bestValue, _value, out bestValue, bestRectangle, rect, out bestRectangle);


                //NBR4
                rect.X = _x - _windowSize;
                rect.Y = _y;
                rect = cekRectangle(rect);
                _value = matchHistogram(src, _listRect1, out _listRect1, denseHistogram1, denseHistogram2, rect);
                getBestRectangle(bestValue, _value, out bestValue, bestRectangle, rect, out bestRectangle);


                //NBR6
                rect.X = _x + _windowSize;
                rect.Y = _y;
                rect = cekRectangle(rect);
                _value = matchHistogram(src, _listRect1, out _listRect1, denseHistogram1, denseHistogram2, rect);
                getBestRectangle(bestValue, _value, out bestValue, bestRectangle, rect, out bestRectangle);


                //NBR7
                rect.X = _x - _windowSize;
                rect.Y = _y + _windowSize;
                rect = cekRectangle(rect);
                _value = matchHistogram(src, _listRect1, out _listRect1, denseHistogram1, denseHistogram2, rect);
                getBestRectangle(bestValue, _value, out bestValue, bestRectangle, rect, out bestRectangle);


                //NBR8
                rect.X = _x;
                rect.Y = _y + _windowSize;
                rect = cekRectangle(rect);
                _value = matchHistogram(src, _listRect1, out _listRect1, denseHistogram1, denseHistogram2, rect);
                getBestRectangle(bestValue, _value, out bestValue, bestRectangle, rect, out bestRectangle);


                //NBR9
                rect.X = _x + _windowSize;
                rect.Y = _y + _windowSize;
                rect = cekRectangle(rect);

                _value = matchHistogram(src, _listRect1, out _listRect1, denseHistogram1, denseHistogram2, rect);
                getBestRectangle(bestValue, _value, out bestValue, bestRectangle, rect, out bestRectangle);
            }
            _listRect1.Clear();
  

        }


 
        private void searchLostObject(Image<Gray, Byte> src,
              Image<Gray, Byte> newBlock, Rectangle rect,Rectangle window, double minTh, int counter)
        {

            int _x = rect.X;
            int _y = rect.Y;
            int k = 0;
                    
            for (int i = -1; i< 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                   
                    if (isLost)
                    {
                        listRect1.Clear();
                        rect.X = _x + (window.Width)*j;
                        rect.Y = _y + (window.Height) * i;
                       
   
                  
                       
                        pictureBox4.Image = searchWindow.ToBitmap();
                        pictureBox4.Refresh();

                    }
                    k++;
                }
            }

           
      }


        private void colorTextureTrackingBtn_Click(object sender, EventArgs e)
        {
            colorTextureThread = new Thread(new ThreadStart(this.colorTextureTracking));

            colorTextureThread.Start();
         
        }


        private void colorTextureTracking()
        {
            
            if (colorTextureThread.IsBackground)
            {
                colorTextureThread.Abort();

            }
            Capture capture = new Capture(ofd.FileName);

            Image<Gray, Byte> blockImage;
            Image<Gray, Byte> blockImage2;
            Image<Gray, Byte> frame = new Image<Gray, byte>(_width, _height);
            Image<Gray, Byte> frame2 = new Image<Gray, byte>(_width, _height);
            Image<Gray, Byte> frame3 = new Image<Gray, byte>(_width, _height);
          
            Image<Gray, Byte> lbpFrame = new Image<Gray, byte>(_width, _height);
            Image<Bgr, Byte> _bgr = new Image<Bgr, byte>(_width, _height);
            Image<Hsv, Byte> _hsv = new Image<Hsv, byte>(_width, _height);
            Image<Bgr, Byte> captureFrame = capture.QueryFrame();
           
            Bitmap bitmap = new Bitmap(_width, _height);
            Bitmap bitmap2 = new Bitmap(_width, _height);
            List<Rectangle> _listRect1 = new List<Rectangle>();
           
            colorTextureGraphic = System.Drawing.Graphics.FromImage(bitmap);
            colorGraphic = System.Drawing.Graphics.FromImage(bitmap2);
            DenseHistogram lbpHist1 = new DenseHistogram(256, new RangeF(0, 255));

            video = new VideoWriter(@"D:\dumpCT.avi", 25, 480, 360, true);
            string resultPath = @"result.txt";
            StreamWriter tw = new StreamWriter(resultPath);
            tw.Flush();

            //string path = @"E:\AppServ\Example.txt";
            //File.AppendAllLines(path, new[] { "The very first line!" });

            double newThreshold = threshold;
            Rectangle _rect = rects;  
            Rectangle rotatedRect = rects;
            Rectangle _rect2 = rects;
            Rectangle searchRect = rects;
            int iter = 0;

            MCvConnectedComp connectedComp;
            MCvBox2D trackbox;

            MCvConnectedComp connectedComp2;
            MCvBox2D trackbox2;
            double overlapValue = 0;
            double overlapAverage = 0;
            int truePositif = 0;
          
            stopwatch = new Stopwatch();
            double totalFps = 0, _value = 0;
            stopwatch.Start();
            while (captureFrame != null && iter <univIter)
            {
                colorTexture = true;       
                CvInvoke.cvResize(captureFrame, _bgr, INTER.CV_INTER_LINEAR);
                 newThreshold = threshold;
                convertToHSV(_bgr,_hsv);
                //hueGlobal = _hsv[0];
                hsvFrame = _hsv;
                blockImage = new Image<Gray, byte>(_rect.Width, _rect.Height) ;

                doLBP(_bgr.Convert<Gray, Byte>(), lbpFrame);                
                bitmap = _bgr.ToBitmap();
                bitmap2 = _bgr.ToBitmap();
               
                         
                frameTb.Invoke(new UpdateTextBox(this.UpdateTextbox),
               new object[] { frameTb, iter.ToString() });
               
                frame = hueHist.BackProject(new Image<Gray, Byte>[] { _hsv[0] });
                frame2 = satHist.BackProject(new Image<Gray, Byte>[] { _hsv[1] });
                frame3 = valHist.BackProject(new Image<Gray, Byte>[] { _hsv[2] });

                hueFrame = frame.And(frame2).And(frame3);
                maskingLbp(lbpFrame, hueFrame);


                
                colorTextureGraphic = System.Drawing.Graphics.FromImage(bitmap);
                colorGraphic = System.Drawing.Graphics.FromImage(bitmap2);

                
                doTracking2(lbpFrame, _listRect1, lbpHist, lbpHist1,
                    _rect, threshold, out newThreshold, out _rect); //result tracking
                tw.Write(iter+1 + ".");
                tw.Write( _rect.X + "," + _rect.Y + ";");
                tw.WriteLine(_rect.Right + "," + _rect.Bottom);

                _rect2 = rects;
              
                lbpGlobal = lbpFrame;


                if (!newThreshold.Equals(threshold))
                {

                    overlapValue = cekOverlapp(_rect, _rect2);
                    colorTextureGraphic.DrawRectangle(new Pen(Brushes.Red, 2), _rect);
                }
                else
                {
                    overlapValue = 0;
                }

                overlapAverage = overlapAverage + overlapValue;
                if (overlapValue >0.5)
                {
                    truePositif++;
                    colorGraphic.DrawRectangle(Pens.Red, _rect);

                }
                else
                {
                    colorTextureGraphic.DrawRectangle(new Pen(Brushes.Red, 3), _rect);
                }

                pictureBox1.Invoke(new UpdatePictureBox(this.UpdatePicture),
                new object[] { pictureBox1, hueFrame.ToBitmap() });

                pictureBox2.Invoke(new UpdatePictureBox(this.UpdatePicture),
                    new object[] { pictureBox2, lbpFrame.ToBitmap() });

                pictureBox3.Invoke(new UpdatePictureBox(this.UpdatePicture),
               new object[] { pictureBox3, _hsv[2].ToBitmap() });


                pictureBox4.Invoke(new UpdatePictureBox(this.UpdatePicture),
                   new object[] { pictureBox4, bitmap });


               iter++;
              
               blockImage.Dispose();
                nullWindow.CopyTo(searchWindow);
                _listRect1.Clear();
                captureFrame = capture.QueryFrame();
            }


            tw.WriteLine("end");
            tw.Close();
            double totalSecond = (TimeSpan.FromMilliseconds(stopwatch.ElapsedMilliseconds).TotalSeconds);
            double successRate = (double)truePositif / (double)gtRectList.Count() * 100;

            stopwatch.Stop();
            colorTextureThread.Abort();
            video.Dispose();
            
           
           
        }



        private double cekOverlapp(Rectangle trackingRect, Rectangle gtRect)
        {

            Rectangle intersectRect = trackingRect;
            intersectRect.Intersect(gtRect);
            double intersectValue = intersectRect.Width * intersectRect.Height;
            double unionValue = (trackingRect.Height * trackingRect.Width) + gtRect.Height * gtRect.Width - intersectValue;

            return intersectValue / unionValue;


        }

        private void UpdatePicture(PictureBox pictureBox, Bitmap img)
        {
            
            pictureBox.Image = img;
            pictureBox.Refresh();
           
        }



        public delegate void UpdatePictureBox(PictureBox pictureBox, Bitmap img);



        private void UpdateTextbox(TextBox textbox, String text)
        {

            textbox.Text = text;

        }



        public delegate void UpdateTextBox(TextBox textbox, String text);

        private void UpdateFramebox(TextBox textbox, String text)
        {

            textbox.Text = text;

        }



        public delegate void UpdateFrameBox(TextBox textbox, String text);


        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (!first)
            {
                dragRect = true;
                graphic = pictureBox1.CreateGraphics();
                graphic.DrawRectangle(new Pen(Brushes.Red, 2), rects);
            }
            else
            {
                rectPoint1.X = e.X;
                rectPoint1.Y = e.Y;
                rects.X = rectPoint1.X;
                rects.Y = rectPoint1.Y;
                
                drawRect = true;
                graphic = pictureBox1.CreateGraphics();
            }
            

        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {

            if (first && drawRect)
            {

                pictureBox1.Refresh();

                    rectPoint2.X = e.X;
                    rectPoint2.Y = e.Y;

                    rects.Width = e.X - rectPoint1.X + 1;
                    rects.Height = e.Y - rectPoint1.Y + 1;
                   
                    graphic.DrawRectangle(new Pen(Brushes.Red, 2), rects);               

            }

            if (!first && dragRect)
            {

                if (!rects.X.Equals(e.X) && !rects.Y.Equals(e.Y))
                {
                    pictureBox1.Refresh();
                   

                }
              
                rectPoint2.X = e.X - ((int)rects.Width / 2);
                rectPoint2.Y = e.Y - ((int)rects.Height / 2);
                rects.X = rectPoint2.X;
                rects.Y = rectPoint2.Y;
                graphic.DrawRectangle(new Pen(Brushes.Red, 2), rects);
               

            }

            rects22 = rects;

        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (first)
            {
                drawRect = false;
            }

            if (dragRect)
            {
                graphic = pictureBox1.CreateGraphics();
                graphic.DrawRectangle(new Pen(Brushes.Red, 2), rects);
                dragRect = false;
            }


        }


        private void pictureBox2_MouseClick(object sender, MouseEventArgs e)
        {
            Point seed = new Point(e.X, e.Y);
            cekPixel(seed);
        }

   
    }
}
