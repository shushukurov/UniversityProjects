%---------------------------------------------------------------------
% Name: Shokhzod Shukurov %%
% SID: 1917828 %%
%---------------------------------------------------------------------
function ciphered_message = AtbashEncode(PlainText)

    % Converting chars to Uppercase
    PlainText = upper(PlainText);
    % Converting chars to ASCII numbers
    PlainNumbers = uint8(PlainText);
    % Getting ASCII numbers of plain/ciphered text
    CipheredNums = 155 - PlainNumbers;
    % Converting numbers to chars
    ciphered_message = char(CipheredNums);



