% Nama File: BAB12_KonversiLuv.m
% Deskripsi: Melakukan konversi model warna citra dari RGB ke CIELuv dan CIELuv ke RGB
 
% Inisialisasi
I = imread('Source Image/flower-garden.jpg');
ILuv = RGB2LUV(I);
Irgb = LUV2RGB(ILuv);

figure,
subplot(1,3,1), imshow(I), title('Citra Original');
subplot(1,3,2), imshow(ILuv), title('Citra RGB ke CIELuv');
subplot(1,3,3), imshow(Irgb), title('Citra CIELuv ke RGB');

