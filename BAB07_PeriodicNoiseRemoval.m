% Nama File: BAB07_PeriodicNoiseRemoval.m
% Deskripsi: Mengurangi derau periodik dengan transformasi Fourier

% Membaca citra input berderau periodik
I = imread ('Source Image/cruise-periodicNoise.jpg');
I = rgb2gray(I); % Untuk citra berwarna. Hapus untuk input citra grayscale

% Transformasi Fourier
Ifreq = fft2(I);
Ifreq = fftshift(Ifreq);

% Notch filter
Ifreq(:,158) = 0; Ifreq(:,194) = 0;
Ifreq(165,:) = 0; Ifreq(187,:) = 0;

% Invers Fourier Transform
Ifreq_inv = ifft2 (Ifreq);

% Menampilkan citra output
fftshow (Ifreq, 'log')
figure, fftshow (Ifreq_inv, 'abs')