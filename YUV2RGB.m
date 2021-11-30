
% Nama File: YUV2RGB.m
% Deskripsi: Melakukan konversi model warna citra dari YUV ke RGB
 
% Inisialisasi
function f = YUV2RGB(I)
	Iy = I(:,:,1);
	Iu = I(:,:,2);
	Iv = I(:,:,3);
	[m,n] = size(Iy);
	 
	k = [1 0.000 1.140;
		 1 -0.395 -0.581;
		 1 2.032 0.000;];
	 
	for i = 1 : m
	   for j = 1 : n
		   yuv = [Iy(i,j); Iu(i,j); Iv(i,j)];
		   rgb = k*double(yuv);
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
