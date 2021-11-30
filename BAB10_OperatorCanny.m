% Nama File: BAB10_OperatorCanny.m
% Deskripsi: Melakukan pendeteksian tepi menggunakan operator Canny
% Input    : Citra grayscale (perhatikan hal ini)
% Output   : Citra biner berisi edge

Ig = imread('Source Image/gdr.bmp');
Iedge  = edge(Ig,'canny');
 
figure,
subplot(1,2,1), imshow(Ig), title('Citra Asli');
subplot(1,2,2), imshow(Iedge), title('Citra Hasil Edge Detection');

