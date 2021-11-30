
% Nama File: BAB12_KonversiYCbCr.m
% Deskripsi: Melakukan konversi model warna citra dari RGB ke YCbCr dan YCbCr ke RGB
 
% Inisialisasi
I = imread('Source Image/flower-garden.jpg');
Iycbcr = RGB2YCBCR(I);
Irgb = YCBCR2RGB(Iycbcr);

figure,
subplot(1,3,1), imshow(I), title('Citra Original');
subplot(1,3,2), imshow(uint8(Iycbcr)), title('Citra RGB ke YCbCr');
subplot(1,3,3), imshow(Irgb), title('Citra YCbCr ke RGB');

