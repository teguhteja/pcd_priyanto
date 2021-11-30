% Nama File: BAB10_OperatorRoberts.m
% Deskripsi: Melakukan pendeteksian tepi menggunakan operator Roberts
% Input    : Citra grayscale (perhatikan hal ini)
% Output   : Citra biner berisi edge

Ig = imread('Source Image/gdr.bmp');

Gx = [1 0 ; 0 -1];
Gy = [0 1 ; -1 0];
Ix = conv2(double(Ig), double(Gx), 'same');
Iy = conv2(double(Ig), double(Gy), 'same');
Iedge = sqrt(Ix.^2 + Iy.^2);
 
figure,
subplot(1,2,1), imshow(Ig), title('Citra Asli');
subplot(1,2,2), imshow(uint8(Iedge)), title('Citra Hasil Edge Detection');

