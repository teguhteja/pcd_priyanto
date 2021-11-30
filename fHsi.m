function x = fHsi(h, s)
    hn = double(h);
	sn = double(s);
    x = 1+((sn*cos(hn * pi/180))/cos((60-hn)* pi/180));
end
