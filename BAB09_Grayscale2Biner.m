% Nama File : BAB09_Grayscale2Biner.m
% Fungsi konversi citra grayscale ke citra biner
% img_gs  	: citra grayscale
% img_bin 	: citra biner
% T         : nilai ambang batas (threshold)

img_gs = imread('Source Image/rice.png');
T = 127;
[m,n] = size(img_gs);
for i = 1 : m
    for j = 1 : n
        if img_gs(i,j) <= T
            img_bin(i,j) = 0;
        else
            img_bin(i,j) = 1;
        end
    end
end
figure,
subplot(1,2,1), imshow(img_gs), title('Citra Grayscale');
subplot(1,2,2), imshow(img_bin), title('Citra Biner');
