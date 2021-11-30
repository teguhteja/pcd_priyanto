% Nama File: BAB06_KonvolusiValid.m
% Deskripsi: Melakukan operasi konvolusi dengan mengabaikan nilai terluar pada citra

% Inisialsisasi
F = [4 7 1 9 8 5;3 8 5 1 3 9;2 2 8 2 5 7;3 3 4 6 7 9;2 5 5 1 4 8;7 3 6 3 8 5];
G = [0 -1 0;-1 4 -1;0 -1 0];
FxG = F;

% Konvolusi
[bf, kf] = size(F);
[bg, kg] = size(G);

m2 = floor(bg/2);
n2 = floor(kg/2);

for i = m2+1 : bf-m2;
    for j = n2+1 : kf-n2;
        temp = 0;
        for k = -m2: m2;
            for l = -n2: n2;
                temp = temp + G(k + m2+1,l+n2+1) * F(i-k,j-l);
            end
        end
        FxG(i,j) = temp;
    end
end

FxG = FxG(2:bf-1,2:kf-1)