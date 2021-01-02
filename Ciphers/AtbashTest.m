%---------------------------------------------------------------------
% Name: Shokhzod Shukurov %%
% SID: 1917828 %%
%---------------------------------------------------------------------
clear all;
clc;

% Test Encoder

plainmessage = 'IAMBEINGMADETOEATANDDRINKMYOWNTHOUGHTS';
ciphermessage = AtbashEncode(plainmessage);

disp(['Plain text: ', plainmessage]);
disp(['Ciphered text: ', ciphermessage]);

% Test Decoder
plainmessage = AtbashEncode(ciphermessage);

disp(['Ciphered text: ', ciphermessage]);
disp(['Plain text: ', plainmessage]);