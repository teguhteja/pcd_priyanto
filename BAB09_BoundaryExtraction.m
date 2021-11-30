% Nama File: BAB09_BoundaryExtraction.m
% Deskripsi: Melakukan operasi boundary extraction pada citra biner

% Inisialsisasi
filebinA = imread('Source Image/binA.jpg');
binAGrayscale = rgb2gray(filebinA);
binABW = im2bw(binAGrayscale);
SE = ones(5,5); % SE = structuring element BOX (5x5)

AB = binABW - (imerode(binABW,SE));
figure,
subplot(1,2,1), imshow(binABW), title('Citra Biner');
subplot(1,2,2), imshow(AB), title('Operasi boundary extraction');