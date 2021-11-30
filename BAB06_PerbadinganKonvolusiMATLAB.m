% Nama File: BAB06_PerbadinganKonvolusiMATLAB.m
% Deskripsi: Melakukan operasi konvolusi dengan fungsi conv2

M = [4 7 1 9 8 5;3 8 5 1 3 9;2 2 8 2 5 7;3 3 4 6 7 9;2 5 5 1 4 8;7 3 6 3 8 5];
kernel = [0 -1 0;-1 4 -1;0 -1 0];
 
KonvValid = uint8(conv2(M, kernel, 'valid'))
KonvSame = uint8(conv2(M, kernel, 'same'))
KonvFull = uint8(conv2(M, kernel, 'full'))
