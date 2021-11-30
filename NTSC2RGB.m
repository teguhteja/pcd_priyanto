% Nama File: NTSC2RGB.m
% Deskripsi: Melakukan konversi model warna citra dari NTSC/YIQ ke RGB
 
% Inisialisasi
function f = NTSC2RGB(I)
	IY = I(:,:,1);
	Ii = I(:,:,2);
	Iq = I(:,:,3);
	[m,n] = size(IY);

	k = [1 0.956 0.621;
		 1 -0.272 -0.647;
		 1 -1.106 1.703;];
	 
	for i = 1 : m
	   for j = 1 : n
		   yiq = [IY(i,j); Ii(i,j); Iq(i,j)];
		   rgb = k*double(yiq);
		   Ir(i,j) = uint8(rgb(1,:)*255);
		   Ig(i,j) = uint8(rgb(2,:)*255);
		   Ib(i,j) = uint8(rgb(3,:)*255);
	   end
	end
	Irgb(:,:,1) = Ir;
	Irgb(:,:,2) = Ig;
	Irgb(:,:,3) = Ib;
	 
	% figure(1), imshow(Irgb);
    f = Irgb;
end