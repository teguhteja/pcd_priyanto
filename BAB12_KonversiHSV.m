
% Nama File: BAB12_KonversiHSV.m
% Deskripsi: Melakukan konversi model warna citra dari RGB ke HSV dan 
% HSV ke RGB
 
% Inisialsisasi
I = imread('Source Image/flower-garden.jpg');
Ihsv = RGB2HSV(I);
Irgb = HSV2RGB(Ihsv);
 
figure,
subplot(1,3,1), imshow(I), title('Citra Original');
subplot(1,3,2), imshow(Ihsv), title('Citra RGB ke HSV');
subplot(1,3,3), imshow(Irgb), title('Citra HSV ke RGB');
 
