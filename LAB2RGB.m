% Nama File: LAB2RGB.m
% Deskripsi: Melakukan konversi model warna citra dari CIELab ke RGB

% Inisialisasi
function f = LAB2RGB(I)
	IL = I(:,:,1);
	Ia = I(:,:,2);
	Ib = I(:,:,3);
	[m,n] = size(IL);
	 
	% white point d65
	xn = 0.95047;
	yn = 1;
	zn = 1.08883;

	for i = 1 : m
	   for j = 1 : n
			L = IL(i,j);
			a = Ia(i,j);
			b = Ib(i,j);
		   
			fY = ((L + 16)/116) ^ 3;
			if fY <= 0.008856
				fY = (L / 903.3);
			end
			Iy(i,j) = fY;
			fY = fLab(fY);
		   
			fX = a/500 + fY;
			if fX > 0.008856
				fX = fX^3;
			else
				fX = (fX - 16/116)/7.787;
			end
			Ix(i,j) = fX;
			
			fZ = fY - b/200;
			if fZ > 0.008856
				fZ = fZ^3;
			else
				fZ = (fZ - 16/116)/7.787;
			end
			Iz(i,j) = fZ;
	   end
	end
	Ixyz(:,:,1) = Ix;
	Ixyz(:,:,2) = Iy;
	Ixyz(:,:,3) = Iz;
	Irgb = xyz2rgb(Ixyz);
	% figure(1), imshow(Irgb);
    f = Irgb;
end

