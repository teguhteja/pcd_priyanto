
% Nama File: RGB2CMY.m
% Deskripsi: Melakukan konversi model warna citra dari RGB ke CMY
 
% Inisialsisasi
function f = RGB2CMY(I)
	Ir = double(I(:,:,1));
	Ig = double(I(:,:,2));
	Ib = double(I(:,:,3));
	[m,n] = size(Ir);

	for i = 1 : m
	   for j = 1 : n
		   c = 1 - Ir(i,j)/255;
		   m = 1 - Ig(i,j)/255;
		   y = 1 - Ib(i,j)/255;
		   
		   Ic(i,j) = double(c);
		   Im(i,j) = double(m);
		   Iy(i,j) = double(y);
	   end
	end
	Icmy(:,:,1) = Ic;
	Icmy(:,:,2) = Im;
	Icmy(:,:,3) = Iy;
	 
	% figure(1), imshow(Icmy);
    f = Icmy;
end