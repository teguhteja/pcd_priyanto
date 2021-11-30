% Nama File: BAB10_OperatorKirsch.m
% Deskripsi: Melakukan operasi deteksi tepi menggunakan kirsch
% Input    : Citra grayscale (perhatikan hal ini)
% Output   : Citra biner berisi edge

Ig = imread('Source Image/gdr.bmp');

W = [5 -3 -3;5 0 -3;5 -3 -3];
NW = [5 5 -3;5 0 -3;-3 -3 -3];
N = [5 5 5;-3 0 -3;-3 -3 -3];
NE = [-3 5 5;-3 0 5;-3 -3 -3];
SW = [-3 -3 -3;5 0 -3;5 5 -3];
S = [-3 -3 -3;-3 0 -3;5 5 5];
SE = [-3 -3 -3;-3 0 5;-3 5 5];
E = [-3 -3 5;-3 0 5;-3 -3 5];
 
KirschW = conv2(double(Ig),double(W));
KirschNW = conv2(double(Ig),double(NW));
KirschN = conv2(double(Ig),double(N));
KirschNE = conv2(double(Ig),double(NE));
KirschSW = conv2(double(Ig),double(SW));
KirschS = conv2(double(Ig),double(S));
KirschSE = conv2(double(Ig),double(SE));
KirschE = conv2(double(Ig),double(E));
 
Kirsch1 = max(KirschW,max(KirschNW,max(KirschN,KirschNE)));
Kirsch2 = max(KirschSW,max(KirschS,max(KirschSE,KirschE)));
Kirsch = max(Kirsch1, Kirsch2);

figure,
subplot(1,2,1), imshow(uint8(Ig)), title('Citra Grayscale');
subplot(1,2,2), imshow(uint8(Kirsch)), title('Citra Hasil Edge Detection');