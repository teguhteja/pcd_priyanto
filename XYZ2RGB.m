% Nama File: XYZ2RGB.m
% Deskripsi: Melakukan konversi model warna citra dari XYZ ke RGB

% Inisialisasi
function f = XYZ2RGB(I)
	Ix = I(:,:,1);
	Iy = I(:,:,2);
	Iz = I(:,:,3);
	[m,n] = size(Ix);
	 
	k = [0.41847 -0.15866 -0.082835;
		 -0.091169 0.25243 0.015708;
		 0.00092090 0.0025498 0.17860;];
	 
	for i = 1 : m
	   for j = 1 : n
		   xyz = [Ix(i,j); Iy(i,j); Iz(i,j)];
		   rgb = k*double(xyz);
		   Ir(i,j) = uint8(rgb(1,:));
		   Ig(i,j) = uint8(rgb(2,:));
		   Ib(i,j) = uint8(rgb(3,:));
	   end
	end
	Irgb(:,:,1) = Ir;
	Irgb(:,:,2) = Ig;
	Irgb(:,:,3) = Ib;
	 
	% figure(1), imshow(Irgb);
    f = Irgb;
end