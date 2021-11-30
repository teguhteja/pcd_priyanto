% Nama File: BAB12_KonversiXYZ.m
% Deskripsi: Melakukan konversi model warna citra dari RGB ke XYZ dan XYZ ke RGB
 
% Inisialisasi
I = imread('Source Image/flower-garden.jpg');
Ixyz = RGB2XYZ(I);
Irgb = XYZ2RGB(Ixyz);

figure,
subplot(1,3,1), imshow(I), title('Citra Original');
subplot(1,3,2), imshow(Ixyz), title('Citra RGB ke XYZ');
subplot(1,3,3), imshow(Irgb), title('Citra XYZ ke RGB');

