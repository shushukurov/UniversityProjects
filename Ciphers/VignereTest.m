%---------------------------------------------------------------------
% Name: Shokhzod Shukurov %%
% SID: 1917828 %%
%---------------------------------------------------------------------
clear all;
clc;

% Test Encoder

plainmessage = 'student';
key = 'CELLARDOOR';
ciphermessage = VignereEncode(plainmessage, key);

disp(['Plain text: ', plainmessage]);
disp(['Ciphered text: ', ciphermessage]);

% Test Decoder
plainmessage = VignereDecode(ciphermessage,key);

disp(['Ciphered text: ', ciphermessage]);
disp(['Plain text: ', plainmessage]);