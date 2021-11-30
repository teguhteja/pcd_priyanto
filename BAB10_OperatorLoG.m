
% Nama File: BAB10_OperatorLoG.m
% Deskripsi: Melakukan pendeteksian tepi menggunakan operator Laplace of
% Gaussian (LoG)
% Input    : Citra grayscale (perhatikan hal ini)
% Output   : Citra biner berisi edge

Ig = imread('Source Image/gdr.bmp');
 
G = [0 0 -1 0 0;
     0 -1 -2 -1 0;
     -1 -2 16 -2 -1;
     0 -1 -2 -1 0;
     0 0 -1 0 0];
Iedge = conv2(double(Ig), double(G), 'same');

figure,
subplot(1,2,1), imshow(Ig), title('Citra Asli');
subplot(1,2,2), imshow(uint8(Iedge)), title('Citra Hasil Edge Detection');
