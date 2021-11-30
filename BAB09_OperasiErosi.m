% Nama File: BAB09_OperasiErosi.m
% Deskripsi: Melakukan operasi erosi pada citra biner

% Inisialisasi
fileGear = imread('Source Image/bike-gear.jpg');
gearGrayscale = rgb2gray(fileGear);
gearBW = not(im2bw(gearGrayscale));
SE = [1 1 1;1 1 1;1 1 1];

% Operasi erosi
A = gearBW;
B = SE;
hotx = 1;
hoty = 1;

[ta, la]=size(A); 
[tb, lb]=size(B);

Xb = [];
Yb = [];
jum_anggota = 0;
 
% Menentukan koordinat piksel bernilai 1 pada H
for baris = 1 : tb
    for kolom = 1 : lb
        if B(baris, kolom) == 1
            jum_anggota = jum_anggota + 1;
            Xb(jum_anggota) = -hotx + kolom;
            Yb(jum_anggota) = -hoty + baris;
        end
    end
end
 
AB = ones(ta, la); % Beri nilai satu semua pada hasil erosi
 
% Memproses erosi
for baris = 1 : ta
    for kolom = 1 : la
        for indeks = 1 : jum_anggota
            if A(baris, kolom) == 0
                xpos = kolom + Xb(indeks);
                ypos = baris + Yb(indeks);
                if (xpos >= 1) && (xpos <= la) && ...
                   (ypos >= 1) && (ypos <= ta)
                    AB(ypos, xpos) = 0;
                end
            end
        end
    end
end

figure,
subplot(1,2,1), imshow(gearBW), title('Citra Biner');
subplot(1,2,2), imshow(AB), title('Operasi Erosi');