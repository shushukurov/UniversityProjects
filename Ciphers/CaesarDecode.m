%---------------------------------------------------------------------
% Name: Shokhzod Shukurov %%
% SID: 1917828 %%
%---------------------------------------------------------------------
% shift (can be 0 to 26); k=0 == No encryption
function plain_message=CaesarDecode(Ciphered_text,shift)
    % Converting chars to Uppercase
    Ciphered_text = upper(Ciphered_text);
    % Getting plain text
    plain_message = '';
    for i=1:length(Ciphered_text)
            a = double(Ciphered_text) - shift;
        if (a(i) < double('A'))
            a(i) = a(i) + 26;
            plain_message(i)=a(i);
        else
            plain_message(i)=a(i);
        end
    end
    % Converting numbers to chars
    plain_message=char(plain_message);