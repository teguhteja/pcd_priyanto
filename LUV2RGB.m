% Nama File: LUV2RGB.m
% Deskripsi: Melakukan konversi model warna citra dari CIELuv ke RGB
 
% Inisialisasi
function f = LUV2RGB(I)
	IL = I(:,:,1);
	Iu = I(:,:,2);
	Iv = I(:,:,3);
	[m,n] = size(IL);
	 
	% white point d65
	xn = 0.95047;
	yn = 1;
	zn = 1.08883;
	 
	for i = 1 : m
	   for j = 1 : n
			L = IL(i,j);
			u = Iu(i,j);
			v = Iv(i,j);
			
			Li = L/903.3;
			if Li > 0.008856
				Li = ((L+16)/116)^3;
			end
			fY = Li * yn;
			Iy(i,j) = fY;
			
			un = 4*xn/(xn+15*yn+3*zn);
			vn = 9*yn/(xn+15*yn+3*zn);
			
			fU = (u/(13*L)) + un;
			fV = (v/(13*L)) + vn;
			
			%Iz(i,j) = (15*fU*fV*fY + 9*fU*fY)/fV - (15*fV*fY + 9*fY)/fV + 15*fU*fY;
			Iz(i,j) = (9*fY/fV*(4/fU - 1) - 15*fY*(4/fU - 1) - 15*fY)/(12/fU);
			Ix(i,j) = 9*fY/fV - 15*fY - 3*Iz(i,j);
	   end
	end
	Ixyz2(:,:,1) = Ix;
	Ixyz2(:,:,2) = Iy;
	Ixyz2(:,:,3) = Iz;
	Irgb = xyz2rgb(Ixyz2);
	% figure(1), imshow(Irgb);
    f = Irgb;
end

