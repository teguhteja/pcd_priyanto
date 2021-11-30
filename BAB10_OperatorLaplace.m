
% Nama File: BAB10_OperatorLaplace.m
% Deskripsi: Melakukan pendeteksian tepi menggunakan operator Laplace
% Input    : Citra grayscale (perhatikan hal ini)
% Output   : Citra biner berisi edge

Ig = imread('Source Image/gdr.bmp');
 
G = [-1 -1 -1; -1 8 -1; -1 -1 -1];
Iedge = conv2(double(Ig), double(G), 'same');

figure,
subplot(1,2,1), imshow(Ig), title('Citra Asli');
subplot(1,2,2), imshow(uint8(Iedge)), title('Citra Hasil Edge Detection');
