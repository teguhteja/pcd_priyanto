% Nama File: BAB06_PerbadinganJenisFilter.m
% Deskripsi: Melakukan konvolusi dengan berbagai jenis kernel filter

fileSailboat = imread('Source Image/sailboat-color.jpg');
sailboatGray = rgb2gray(fileSailboat);

g1 = [1/9 1/9 1/9;1/9 1/9 1/9;1/9 1/9 1/9];
g2 = [0 -1 0;-1 4 -1;0 -1 0];
g3 = [0 -1 0;-1 5 -1;0 -1 0];

K1 = [5 -3 -3,5 0 -3,5 -3 -3];
K2 = [5 5 -3,5 0 -3,-3 -3 -3];
K3 = [5 5 5,-3 0 -3,-3 -3 -3];
K4 = [-3 5 5,-3 0 5,-3 -3 -3];

Kirsh1 = conv2(double(sailboatGray),double(K1));
Kirsh2 = conv2(double(sailboatGray),double(K2));
Kirsh3 = conv2(double(sailboatGray),double(K3));
Kirsh4 = conv2(double(sailboatGray),double(K4));

Kirsh = max(Kirsh4,max(Kirsh3,max(Kirsh1,Kirsh2)));
lowPass = conv2(double(sailboatGray),g1);
highPass = conv2(double(sailboatGray),g2);
bandPass = conv2(double(sailboatGray),g3);

figure, 
subplot(2,2,1); imshow(uint8(sailboatGray));
subplot(2,2,2); imshow(uint8(lowPass)); 
subplot(2,2,3); imshow(uint8(Kirsh));
subplot(2,2,4); imshow(uint8(bandPass)); 