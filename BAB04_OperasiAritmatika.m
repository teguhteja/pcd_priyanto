% Nama File: BAB04_OperasiAritmatika.m
% Deskripsi: Melakukan operasi aritmatika pada citra digital

fileParrot = imread('Source Image/parrot_color.jpg');
fileFlowerGarden = imread('Source Image/flower-garden.jpg');

grayParrot = rgb2gray(fileParrot);
grayFlower = rgb2gray(fileFlowerGarden);

%4.1.1 Penjumlahan
plus50 = grayFlower + 50;
%4.1.2 Pengurangan
min50 = grayFlower - 50;

%Crop
cropFlower = imcrop(grayFlower,[421 0 960 425]);
cropParrot = imcrop(grayParrot,[0 0 540 425]);
 
%4.1.3 MAX
maxFlowerParrot = max(cropFlower, cropParrot);
%4.1.4 MIN
minFlowerParrot = min(cropFlower, cropParrot);
%4.1.5 INVERSE
invGrayParrot = 255 - grayParrot;
invRGBParrot = 255 - fileParrot;

figure,
subplot(4,3,1), imshow(fileParrot), title('Citra Parrot RGB');
subplot(4,3,2), imshow(fileFlowerGarden), title('Citra Flower Garden RGB');
subplot(4,3,3), imshow(grayParrot), title('Citra Parrot Grayscale');
subplot(4,3,4), imshow(grayFlower), title('Citra Flowergarden Grayscale');
subplot(4,3,5), imshow(plus50), title('FlowerGarden + 50');
subplot(4,3,6), imshow(min50), title('FlowerGarden - 50');
subplot(4,3,7), imshow(maxFlowerParrot), title('Operasi MAX');
subplot(4,3,8), imshow(minFlowerParrot), title('Operasi MIN');
subplot(4,3,9), imshow(invGrayParrot), title('Citra Inverse Grayscale');
subplot(4,3,10), imshow(invRGBParrot), title('Citra Inverse RGB');
