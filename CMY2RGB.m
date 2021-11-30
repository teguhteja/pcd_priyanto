% Nama File: CMY2RGB.m
% Deskripsi: Melakukan konversi model warna citra dari CMY ke RGB
 
% Inisialsisasi
function f = CMY2RGB(I)
	Ic = double(I(:,:,1));
	Im = double(I(:,:,2));
	Iy = double(I(:,:,3));
	[m,n] = size(Ic);

	for i = 1 : m
	   for j = 1 : n
		   r = (1 - Ic(i,j));
		   g = (1 - Im(i,j));
		   b = (1 - Iy(i,j));
		   
		   Ir2(i,j) = uint8(r*255);
		   Ig2(i,j) = uint8(g*255);
		   Ib2(i,j) = uint8(b*255);
	   end
	end
	Irgb(:,:,1) = Ir2;
	Irgb(:,:,2) = Ig2;
	Irgb(:,:,3) = Ib2;
	 
	figure(1), imshow(Irgb);
    f = Irgb;
end

