
% Nama File: HSV2RGB.m
% Deskripsi: Melakukan konversi model warna citra dari HSV ke RGB
 
% Inisialsisasi
function f = HSV2RGB(I)
	Ih = I(:,:,1);
	Is = I(:,:,2);
	Iv = I(:,:,3);
	[m,n] = size(Ih);
	 
	 
	for i = 1 : m
	   for j = 1 : n
		   h = Ih(i,j)/(60/360);
		   k = floor(h);
		   t = h-k; 
		   x = Iv(i,j)*(1-Is(i,j)); %t
		   y = Iv(i,j)*(1-Is(i,j)*t); %n
		   z = Iv(i,j)*(1-Is(i,j)*(1-t)); %p
		   
		   if k == 0 || k == 6
			   rgb = [Iv(i,j);z;x]; %[v, p, t]
		   elseif k == 1 
			   rgb = [y;Iv(i,j);x]; %[n, v, t]
		   elseif k == 2 
			   rgb = [x;Iv(i,j);z]; %[t, v, p]
		   elseif k == 3 
			   rgb = [x;y;Iv(i,j)]; %[t, n, v]
		   elseif k == 4 
			   rgb = [z;x;Iv(i,j)]; %[p, t, v]
		   elseif k == 5 
			   rgb = [Iv(i,j);x;y]; %[v, t, n]
		   end
		   Ir(i,j) = uint8(rgb(1,:) * 255);
		   Ig(i,j) = uint8(rgb(2,:) * 255);
		   Ib(i,j) = uint8(rgb(3,:) * 255);
	   end
	end
	Irgb(:,:,1) = uint8(Ir);
	Irgb(:,:,2) = uint8(Ig);
	Irgb(:,:,3) = uint8(Ib);
 
	% figure(1), imshow(Irgb);
    f = Irgb;
end
  


