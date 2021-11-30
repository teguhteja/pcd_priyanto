
% Nama File: BAB12_KonversiHSI.m
% Deskripsi: Melakukan konversi model warna citra dari RGB ke HSI dan 
% HSI ke RGB
 
% Inisialsisasi
I = imread('Source Image/flower-garden.jpg');
Ihsi = RGB2HSI(I);
Irgb = HSI2RGB(Ihsi);
 
figure,
subplot(1,3,1), imshow(I), title('Citra Original');
subplot(1,3,2), imshow(Ihsi), title('Citra RGB ke HSI');
subplot(1,3,3), imshow(Irgb), title('Citra HSI ke RGB');
 
