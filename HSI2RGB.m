
% Nama File: HSI2RGB.m
% Deskripsi: Melakukan konversi model warna citra dari HSI ke RGB
 
% Inisialisasi
function f = HSI2RGB(I)
	Ih = double(I(:,:,1));
	Is = double(I(:,:,2));
	Ii = double(I(:,:,3));
	[m,n] = size(Ih);

	for i = 1 : m
	   for j = 1 : n
		   h = Ih(i,j) * 360;
		   s = Is(i,j);
		   i2 = Ii(i,j);
		   
		   if (h>=0 && h<=120) || h==360
			   r = i2*fHsi(h,Is(i,j));
			   b = i2-i2*s;
			   g = 3*i2-r-b;
		   elseif h>120 && h<=240
			   h = h - 120;
			   
			   g = i2*fHsi(h,Is(i,j));
			   r = i2-i2*s;
			   b = 3*i2-r-g;
		   elseif h>240 && h<360
			   h = h - 240;
			   
			   b = i2*fHsi(h,Is(i,j));
			   g = i2-i2*s;
			   r = 3*i2-g-b;
		   end
		   
		   Ir(i,j) = r;
		   Ig(i,j) = g;
		   Ib(i,j) = b;
	   end
	end

	Irgb(:,:,1) = Ir;
	Irgb(:,:,2) = Ig;
	Irgb(:,:,3) = Ib;
	 
	% figure(1), imshow(Irgb);
    f = Irgb;
end
 
