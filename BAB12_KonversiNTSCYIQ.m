% Nama File: BAB12_KonversiNTSCYIQ.m
% Deskripsi: Melakukan konversi model warna citra dari RGB ke NTSC/YIQ dan 
% NTSC/YIQ ke RGB
 
% Inisialisasi
I = imread('Source Image/flower-garden.jpg');
Intsc = RGB2NTSC(I);
Irgb = NTSC2RGB(Intsc);
 
figure,
subplot(1,3,1), imshow(I), title('Citra Original');
subplot(1,3,2), imshow(Intsc), title('Citra RGB ke NTSC/YIQ');
subplot(1,3,3), imshow(Irgb), title('Citra NTSC/YIQ ke RGB');
 
