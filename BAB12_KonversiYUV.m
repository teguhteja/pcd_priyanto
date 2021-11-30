% Nama File: BAB12_KonversiYUV.m
% Deskripsi: Melakukan konversi model warna citra dari RGB ke YUV dan YUV ke RGB
 
% Inisialisasi
I = imread('Source Image/flower-garden.jpg');
IYuv = RGB2YUV(I);
Irgb = YUV2RGB(IYuv);

figure,
subplot(1,3,1), imshow(I), title('Citra Original');
subplot(1,3,2), imshow(IYuv), title('Citra RGB ke YUV');
subplot(1,3,3), imshow(Irgb), title('Citra YUV ke RGB');

