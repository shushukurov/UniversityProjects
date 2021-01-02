%---------------------------------------------------------------------
% Name: Shokhzod Shukurov %%
% SID: 1917828 %%
%---------------------------------------------------------------------
function Ptext = VignereDecode(CipherText, key)

    keyIndex = mod(0:(numel(CipherText)-1), numel(key))+1;
    key = key(keyIndex);
    Ptext = '';
    for i=1:length(CipherText)
        a = mod(double(CipherText(i) - key(i) + 26),26);
        a = a + double('A');
        Ptext(i) = char(a);
    end