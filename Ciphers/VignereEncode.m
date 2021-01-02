%---------------------------------------------------------------------
% Name: Shokhzod Shukurov %%
% SID: 1917828 %%
%---------------------------------------------------------------------
function Ctext = VignereEncode(PlainText, key)

    keyIndex = mod(0:(numel(PlainText)-1), numel(key))+1;
    key = key(keyIndex);
    Ctext = '';
    for i=1:length(PlainText)
        a = mod(double(PlainText(i)) + key(i),26);
        a = a + double('A');
        Ctext(i) = char(a);
    end 
    