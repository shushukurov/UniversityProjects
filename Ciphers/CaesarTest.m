%---------------------------------------------------------------------
% Name: Shokhzod Shukurov %%
% SID: 1917828 %%
%---------------------------------------------------------------------
clear all;
clc;

% Test Encoder

plainmessage = 'THESEARENOTTHEDROIDSYOUARELOOKINGFOR';
shift = 3;
ciphermessage = CaesarEncode(plainmessage, shift);

disp(['Plain text: ', plainmessage]);
disp(['Ciphered text: ', ciphermessage]);

% Test Decoder

plainmessage = CaesarDecode(ciphermessage,shift);

disp(['Ciphered text: ', ciphermessage]);
disp(['Plain text: ', plainmessage]);