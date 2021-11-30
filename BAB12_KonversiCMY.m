% Nama File: BAB12_KonversiCMY.m
% Deskripsi: Melakukan konversi model warna citra dari RGB ke CMY dan CMY ke RGB
 
% Inisialisasi
I = imread('Source Image/flower-garden.jpg');
ICmy = RGB2CMY(I);
Irgb = CMY2RGB(ICmy);

figure,
subplot(1,3,1), imshow(I), title('Citra Original');
subplot(1,3,2), imshow(ICmy), title('Citra RGB ke CMY');
subplot(1,3,3), imshow(Irgb), title('Citra CMY ke RGB');

