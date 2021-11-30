% Nama File: BAB13_LocalBinaryPattern.m
% Deskripsi: Melakukan operasi ekstraksi fitur menggunakan local binary
% pattern

fileParrot = imread('Source Image/parrot_color.jpg');
fileParrotRotate = imread('Source Image/parrot_color_rotate.jpg');
fileCruise = imread('Source Image/cruise.jpg');

grayParrot = rgb2gray(fileParrot);
grayParrotRotate = rgb2gray(fileParrotRotate);
grayCruise = rgb2gray(fileCruise);

lbpParrot = extractLBPFeatures(grayParrot);
lbpParrotRotate = extractLBPFeatures(grayParrotRotate);
lbpCruise = extractLBPFeatures(grayCruise);

ParrotvsParrot = (lbpParrot - lbpParrotRotate).^2;
ParrotvsCruise = (lbpParrot - lbpCruise).^2;

figure
bar([ParrotvsParrot; ParrotvsCruise]','grouped')
title('Squared Error of LBP Histograms')
xlabel('LBP Histogram Bins')
legend('Parrot vs Rotated Parrot','Parrot vs Cruise')

