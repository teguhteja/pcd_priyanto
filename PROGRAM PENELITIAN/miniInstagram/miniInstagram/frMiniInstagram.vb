Imports System.Drawing.Imaging

Public Class frMiniInstagram
    Private m_OriginalBitmap As Bitmap


    Private Sub OpenToolStripMenuItem_Click(ByVal sender As System.Object, _
                                            ByVal e As System.EventArgs) Handles OpenToolStripMenuItem.Click
        If OpenFileDialog1.ShowDialog() = DialogResult.OK Then
            Dim file_bm As New Bitmap(OpenFileDialog1.FileName)

            m_OriginalBitmap = New Bitmap(file_bm.Width, file_bm.Height)
            Dim gr As Graphics = Graphics.FromImage(m_OriginalBitmap)
            gr.DrawImage(file_bm, New Point(0, 0))
            picSource.Image = m_OriginalBitmap

            Dim bm As New Bitmap(file_bm.Width, file_bm.Height)
            gr = Graphics.FromImage(bm)
            gr.Clear(picResult.BackColor)
            gr.Dispose()
            picResult.Image = bm
            picResult.Left = picSource.Size.Width + 2

            Me.Width = picResult.Left + picResult.Width + _
            Me.Width - Me.ClientSize.Width
            Me.Height = picResult.Top + picResult.Height + _
            Me.Height - Me.ClientSize.Height

            file_bm.Dispose()

            SaveFileDialog1.InitialDirectory = OpenFileDialog1.InitialDirectory
            ImageEffectsToolStripMenuItem.Enabled = True
        End If
    End Sub


    Private Sub ImageEffectsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ImageEffectsToolStripMenuItem.Click

    End Sub

    Private Sub SaveAsToolStripMenuItem_Click(ByVal sender As System.Object, _
                                              ByVal e As System.EventArgs) Handles SaveAsToolStripMenuItem.Click
        If SaveFileDialog1.ShowDialog() = DialogResult.OK Then
            Dim bm As Bitmap = DirectCast(picResult.Image.Clone(), Bitmap)

            Dim ext As String = SaveFileDialog1.FileName
            ext = ext.Substring(ext.LastIndexOf("."))
            Select Case ext.ToLower()
                Case ".bmp"
                    bm.Save(SaveFileDialog1.FileName, ImageFormat.Bmp)
                Case ".gif"
                    bm.Save(SaveFileDialog1.FileName, ImageFormat.Gif)
                Case ".jpg", ".jpeg"
                    bm.Save(SaveFileDialog1.FileName, ImageFormat.Jpeg)
            End Select
            bm.Dispose()

            OpenFileDialog1.InitialDirectory = SaveFileDialog1.InitialDirectory
        End If
    End Sub

    Private Sub ExitToolStripMenuItem_Click(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles ExitToolStripMenuItem.Click
        Me.Close()
    End Sub

    Private Sub frMiniInstagram_Load(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles MyBase.Load
        Dim init_dir As String = Application.StartupPath
        init_dir = init_dir.Substring(0, init_dir.LastIndexOf("\"))
        OpenFileDialog1.InitialDirectory = init_dir
        SaveFileDialog1.InitialDirectory = init_dir
    End Sub

    Private Sub ApplyFilter(ByVal filter(,) As Single, Optional ByVal _
offset_r As Integer = 0, Optional ByVal offset_g As Integer = 0, _
Optional ByVal offset_b As Integer = 0)
        Dim bm As Bitmap = DirectCast(m_OriginalBitmap.Clone(), Bitmap)

        ' Apply the filter.
        Dim y_rank As Integer = filter.GetUpperBound(0) \ 2
        Dim x_rank As Integer = filter.GetUpperBound(1) \ 2
        Dim r As Integer
        Dim g As Integer
        Dim b As Integer
        For py As Integer = y_rank To bm.Height - 1 - y_rank
            For px As Integer = x_rank To bm.Width - 1 - x_rank
                ' Calculate the value for pixel (px, py).
                r = offset_r
                g = offset_g
                b = offset_b

                ' Loop over the filter.
                For fy As Integer = 0 To filter.GetUpperBound(0)
                    For fx As Integer = 0 To filter.GetUpperBound(1)
                        With m_OriginalBitmap.GetPixel(px + fx - x_rank, _
                                                       py + fy - y_rank)
                            r += .R * filter(fx, fy)
                            g += .G * filter(fx, fy)
                            b += .B * filter(fx, fy)
                        End With
                    Next fx
                Next fy

                ' Set the pixel's value.
                If r < 0 Then
                    r = 0
                ElseIf r > 255 Then
                    r = 255
                End If
                If g < 0 Then
                    g = 0
                ElseIf g > 255 Then
                    g = 255
                End If
                If b < 0 Then
                    b = 0
                ElseIf b > 255 Then
                    b = 255
                End If
                bm.SetPixel(px, py, Color.FromArgb(255, r, g, b))
            Next px
        Next py

        ' Display the result.
        picResult.Image = bm
    End Sub


    Private Sub EmbossToolStripMenuItem_Click(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles EmbossToolStripMenuItem.Click

        ApplyFilter( _
            New Single(,) { _
                {1, 0, 0}, _
                {0, 0, 0}, _
                {0, 0, -1}}, _
            picResult.BackColor.R, picResult.BackColor.G, picResult.BackColor.B)
    End Sub

    Private Sub ImageSmoothingToolStripMenuItem_Click(ByVal sender As  _
        System.Object, ByVal e As System.EventArgs) Handles _
        ImageSmoothingToolStripMenuItem.Click

        ApplyFilter( _
            New Single(,) { _
                {1 / 15, 2 / 15, 1 / 15}, _
                {2 / 15, 3 / 15, 2 / 15}, _
                {1 / 15, 2 / 15, 1 / 15}})
    End Sub

    Private Sub ImageSharpeningToolStripMenuItem_Click(ByVal sender As  _
       System.Object, ByVal e As System.EventArgs) Handles _
       ImageSharpeningToolStripMenuItem.Click

        ApplyFilter( _
            New Single(,) { _
                {-1 / 4, -1 / 4, -1 / 4}, _
                {-1 / 4, 12 / 4, -1 / 4}, _
                {-1 / 4, -1 / 4, -1 / 4}})
    End Sub

    Private Sub InkWellToolStripMenuItem_Click(ByVal sender As System.Object, _
        ByVal e As System.EventArgs) Handles InkWellToolStripMenuItem.Click

        Dim bitmap As Bitmap
        bitmap = New Bitmap(Me.picSource.Image)
        Dim x, y
        Dim d As Byte
        For x = 0 To Me.picSource.Image.Width - 1
            For y = 0 To Me.picSource.Image.Height - 1
                d = Math.Round((bitmap.GetPixel(x, y).R * 0.299 + _
                    bitmap.GetPixel(x, y).G * 0.587 + _
                    bitmap.GetPixel(x, y).B * 0.114))
                bitmap.SetPixel(x, y, Color.FromArgb(d, d, d))
            Next y
        Next x
        Me.picResult.Image = bitmap

        
    End Sub

    Private Sub BlackAndWhiteToolStripMenuItem_Click(ByVal sender As  _
        System.Object, ByVal e As System.EventArgs) Handles _
        BlackAndWhiteToolStripMenuItem.Click

        Dim bitmap As Bitmap
        bitmap = New Bitmap(picSource.Image)
        Dim x, y
        Dim d As Byte
        Dim threshold = 128
        For x = 0 To Me.picSource.Image.Width - 1
            For y = 0 To picSource.Image.Height - 1
                d = Math.Round((bitmap.GetPixel(x, y).R * 0.299 + _
                    bitmap.GetPixel(x, y).G * 0.587 + _
                    bitmap.GetPixel(x, y).B * 0.114))
                If (d > threshold) Then
                    d = 255
                Else
                    d = 0
                End If
                bitmap.SetPixel(x, y, Color.FromArgb(d, d, d))
            Next y
        Next x
        picResult.Image = bitmap

    End Sub

    Private Sub EdgeOnlyToolStripMenuItem_Click(ByVal sender As  _
        System.Object, ByVal e As System.EventArgs) Handles _
        EdgeOnlyToolStripMenuItem.Click

        ApplyFilter( _
            New Single(,) { _
                {-1, -1, -1}, _
                {-1, 8, -1}, _
                {-1, -1, -1}})
    End Sub
End Class

