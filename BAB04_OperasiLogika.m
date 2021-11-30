% Nama File: BAB04_OperasiLogika.m
% Deskripsi: Melakukan operasi logika pada citra digital

fileBinA = imread('Source Image/binA.jpg');
fileBinB = imread('Source Image/binB.jpg');

grayBinA = rgb2gray(fileBinA);
grayBinB = rgb2gray(fileBinB);

bwA = im2bw(fileBinA);
bwB = im2bw(fileBinB);

notA = not(bwA);
AorB = or(bwA, bwB);
AandB = and(bwA, bwB);
AxorB = xor(bwA, bwB);

figure,
subplot(3,2,1), imshow(bwA), title('Citra binary A');
subplot(3,2,2), imshow(bwB), title('Citra binary B');
subplot(3,2,3), imshow(notA), title('Operasi NOT A');
subplot(3,2,4), imshow(AorB), title('Operasi A OR B');
subplot(3,2,5), imshow(AandB), title('Operasi A AND B');
subplot(3,2,6), imshow(AxorB), title('Operasi A XOR B');
