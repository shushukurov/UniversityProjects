%---------------------------------------------------------------------
% Name: Shokhzod Shukurov %%
% SID: 1917828 %%
%---------------------------------------------------------------------
% shift (can be 0 to 26); k=0 mean No encryption
function ciphered_message=CaesarEncode(PlainText,shift)
    % Converting chars to Uppercase
    PlainText = upper(PlainText);
    % Getting ciphered text
    ciphered_message = '';
    for i=1:length(PlainText)
            a = double(PlainText) + shift;
        if (a(i) > double('Z'))
            a(i) = a(i) - 26;
            ciphered_message(i)=a(i);
        else
            ciphered_message(i)=a(i);
        end
    end
        % Converting numbers to chars
    ciphered_message=char(ciphered_message);