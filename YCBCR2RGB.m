% Nama File: YCBCR2RGB.m
% Deskripsi: Melakukan konversi model warna citra dari YCbCr ke RGB
 
% Inisialisasi
function f = YCBCR2RGB(I)
	Iy  = I (:,:,1);
	Icb = I (:,:,2);
	Icr = I (:,:,3);
	[m,n] = size(Iy);

	k = [1 0 1.403;
		 1 -0.344 -0.714;
		 1 1.773 0;];
	 
	for i = 1 : m
	   for j = 1 : n
		   ycbcr = [Iy(i,j); Icb(i,j)-128; Icr(i,j)-128];
		   rgb = k*double(ycbcr);
		   Ir(i,j) = uint8(rgb (1,:));
		   Ig(i,j) = uint8(rgb (2,:));
		   Ib(i,j) = uint8(rgb (3,:));
	   end
	end
	Irgb(:,:,1) = Ir;
	Irgb(:,:,2) = Ig;
	Irgb(:,:,3) = Ib;
	 
	% figure(1), imshow(Irgb);
    f = Irgb;
end