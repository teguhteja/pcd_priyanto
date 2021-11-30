% Nama File: BAB12_KonversiLab.m
% Deskripsi: Melakukan konversi model warna citra dari RGB ke CIELab dan CIELab ke RGB
 
% Inisialisasi
I = imread('Source Image/flower-garden.jpg');
ILab = RGB2LAB(I);
Irgb = LAB2RGB(ILab);

figure,
subplot(1,3,1), imshow(I), title('Citra Original');
subplot(1,3,2), imshow(ILab), title('Citra RGB ke CIELab');
subplot(1,3,3), imshow(Irgb), title('Citra CIELab ke RGB');

