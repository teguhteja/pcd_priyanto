% Nama File: BAB11_Chaincode.m
% Deskripsi: Mendapatkan chain code berdasarkan boundary object

im = imread('Source Image/citra_objek_bw_10x9.png');
ig = rgb2gray(im);
level = graythresh(ig);
ibw = im2bw(ig,level);
boundary = bwboundaries(ibw,8,'noholes');
b = boundary{1,1};
b(end,:) = [];

%   Sumber:
%   https://www.mathworks.com/matlabcentral/fileexchange/29518-freeman-chain-code
%   Freeman Chain Code oleh Alessandro Mannini (2010)
%
%   Representasi chain code:
%   3  2  1                                             
%    \ | /                                          
%   4--P--0                                             
%    / | \                                          
%   5  6  7
%
%   Representasi chain code berdasarkan ketetanggaan terhadap titik P:
%   --------------------------
%   | deltax | deltay | code |
%   |------------------------|
%   |    0   |   +1   |   6  |
%   |    0   |   -1   |   2  |
%   |   -1   |   +1   |   5  |
%   |   -1   |   -1   |   3  |
%   |   +1   |   +1   |   7  |
%   |   +1   |   -1   |   1  |
%   |   -1   |    0   |   4  |
%   |   +1   |    0   |   0  |
%   --------------------------
%

sb=circshift(b,[-1 0]); % menggeser urutan lokasi piksel sebanyak 1 elemen
delta=sb-b; % Menghitung nilai (dy,dx) (8-connected)

% Memeriksa jika boundary tertutup
if abs(delta(end,1))>1 || abs(delta(end,2))>1
    delta=delta(1:(end-1),:); % Jika boundary terbuka, potong elemen terakhir
end

% Memeriksa jika boundary memiliki 8 ketetanggaan (8-connected)
n8c=find(abs(delta(:,1))>1 | abs(delta(:,2))>1);
n4c=abs(delta(:,1))+abs(delta(:,2))>1;
if size(n8c,1)>0 || sum(n4c)==0
    error('Curve isn''t 8-connected\n');
end

% Mengkonversi (dy,dx) ke indeks skalar,
% berdasarkan rumus: idx=3*(dy+1)+(dx+1) = 3dy+dx+4,
% menambahkan 1 jika ingin memulai indeks dari 1
% TPemetaan indeks skalar dengan kode dari chain code
%   --------------------------------------
%   | deltax | deltay | code |  Konversi  |
%   |-------------------------------------|
%   |    0   |   +1   |   6  |      8     | 
%   |    0   |   -1   |   2  |      2     | 
%   |   -1   |   +1   |   5  |      7     | 
%   |   -1   |   -1   |   3  |      1     | 
%   |   +1   |   +1   |   7  |      9     | 
%   |   +1   |   -1   |   1  |      3     | 
%   |   -1   |    0   |   4  |      4     | 
%   |   +1   |    0   |   0  |      6     | 
%   ---------------------------------------

idx=3*delta(:,1)+delta(:,2)+5;
cm([1 2 3 4 6 7 8 9])=[3 2 1 4 0 5 6 7];

ccx0=b(1,2); % titik awal
ccy0=b(1,1); % titik awal
cc_code=(cm(idx))'; % kode dari chain code

row = size(cc_code,1);
fprintf('Chain code: ');
for i=1:row
    fprintf('%d',cc_code(i,1));
end
fprintf('\n');
