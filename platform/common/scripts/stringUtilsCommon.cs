function numSubstringsInString(%haystack, %needles)
{
    %ret = 0;
    %n = getWordCount(%needles) - 1;
    while (%n >= 0)
    {
        if (strstr(%haystack, getWord(%needles, %n)) >= 0)
        {
            %ret = %ret + 1;
        }
        %n = %n - 1;
    }
    return %ret;
}
function numWordsInWords(%haystack, %needles)
{
    %ret = 0;
    %n = getWordCount(%needles) - 1;
    while (%n >= 0)
    {
        if (findWord(%haystack, getWord(%needles, %n)) >= 0)
        {
            %ret = %ret + 1;
        }
        %n = %n - 1;
    }
    return %ret;
}
function wordsNotInWords(%haystack, %needles)
{
    %ret = "";
    %n = getWordCount(%needles) - 1;
    while (%n >= 0)
    {
        %word = getWord(%needles, %n);
        if (!hasWord(%haystack, %word))
        {
            %ret = %ret SPC %word;
        }
        %n = %n - 1;
    }
    %ret = trim(%ret);
    return %ret;
}
function stripSurroundingQuotes(%text)
{
    %text = trim(%text);
    if (getSubStr(%text, 0, 1) $= "\"")
    {
        %text = getSubStr(%text, 1, strlen(%text) - 1);
    }
    if (getSubStr(%text, strlen(%text) - 1, 1) $= "\"")
    {
        %text = getSubStr(%text, 0, strlen(%text) - 1);
    }
    return %text;
}
function chopTextToFitLineWidths(%text, %profile, %generalWidth, %lineWidths)
{
    if (%text $= "")
    {
        return "";
    }
    if (%generalWidth == 0)
    {
        return "";
    }
    if (%profile $= "")
    {
        %profile = GuiDefaultProfile;
    }
    %inputWordCount = getWordCount(%text);
    %outputText = "";
    %currentWordIndex = 0;
    %lineCount = 0;
    while (%currentWordIndex < %inputWordCount)
    {
        %thisLine = "";
        %atEndOfLine = 0;
        while (!%atEndOfLine)
        {
            %currentWord = getWord(%text, %currentWordIndex);
            if (%thisLine $= "")
            {
                %thisLine = %currentWord;
                %thisLineWidth = getStrWidth(%currentWord, %profile);
                %thisLineMaxWidth = chopTextToFitLineWidths_getLineWidth(%generalWidth, %lineWidths, %lineCount);
                if (%thisLineMaxWidth < 0)
                {
                    return "";
                }
                else
                {
                    if (%thisLineMaxWidth == 0)
                    {
                        %thisLine = "";
                        %atEndOfLine = 1;
                    }
                    else
                    {
                        if (%thisLineWidth < %thisLineMaxWidth)
                        {
                            %currentWordIndex = %currentWordIndex + 1;
                        }
                        else
                        {
                            if (%thisLineWidth == %thisLineMaxWidth)
                            {
                                %currentWordIndex = %currentWordIndex + 1;
                                %atEndOfLine = 1;
                            }
                            else
                            {
                                %wordLength = strlen(%currentWord);
                                %partialWord = "";
                                %beginningOfNextWord = 0;
                                %wordDone = 0;
                                %i = 1;
                                while (!%wordDone)
                                {
                                    %partialWord = getSubStr(%currentWord, 0, %i);
                                    %thisLineWidth = getStrWidth(%partialWord, %profile);
                                    if (%thisLineWidth > %thisLineMaxWidth)
                                    {
                                        if (%i > 1)
                                        {
                                            %partialWord = getSubStr(%currentWord, 0, %i - 1);
                                            %beginningOfNextWord = %i - 1;
                                        }
                                        else
                                        {
                                            %beginningOfNextWord = 1;
                                        }
                                        %wordDone = 1;
                                    }
                                    %i = %i + 1;
                                }
                                %thisLine = %partialWord;
                                if (%beginningOfNextWord > 0)
                                {
                                    %text = setWord(%text, %currentWordIndex, getSubStr(%currentWord, %beginningOfNextWord, %wordLength));
                                }
                                %atEndOfLine = 1;
                            }
                        }
                    }
                }
            }
            else
            {
                %thisLineWidth = getStrWidth(%thisLine SPC %currentWord, %profile);
                %thisLineMaxWidth = chopTextToFitLineWidths_getLineWidth(%generalWidth, %lineWidths, %lineCount);
                if (%thisLineWidth < %thisLineMaxWidth)
                {
                    %thisLine = %thisLine SPC %currentWord;
                    %currentWordIndex = %currentWordIndex + 1;
                }
                else
                {
                    if (%thisLineWidth == %thisLineMaxWidth)
                    {
                        %thisLine = %thisLine SPC %currentWord;
                        %currentWordIndex = %currentWordIndex + 1;
                        %atEndOfLine = 1;
                    }
                    else
                    {
                        %atEndOfLine = 1;
                    }
                }
            }
            if (%currentWordIndex >= %inputWordCount)
            {
                %atEndOfLine = 1;
            }
        }
        if (%outputText $= "")
        {
            %outputText = %thisLine;
        }
        else
        {
            %outputText = %outputText NL %thisLine;
        }
        %lineCount = %lineCount + 1;
    }
    return %outputText;
}
function chopTextToFitLineWidths_getLineWidth(%generalWidth, %lineWidths, %index)
{
    if (%index < getWordCount(%lineWidths))
    {
        return getWord(%lineWidths, %index);
    }
    return %generalWidth;
}
$gTargetPlayerName = "(unknown)";
function standardSubstitutions(%dry)
{
    %wet = %dry;
    %wet = strreplace(%wet, "[PLAYERNAME]", $Player::Name);
    %wet = strreplace(%wet, "[PLAYERNAME_URL]", urlEncode($Player::Name));
    %wet = strreplace(%wet, "[TARGETPLAYERNAME]", $gTargetPlayerName);
    %wet = strreplace(%wet, "[READTOU]", $MsgCat::login["E-DONT-KNOW-RULES"]);
    if (isDefined("$Net::RegistrationID"))
    {
        %wet = strreplace(%wet, "[REGISTRATIONID]", $Net::RegistrationID);
    }
    return %wet;
}
function findAndRemoveFirstOccurrenceOfWord(%haystack, %needle)
{
    %index = findWord(%haystack, %needle);
    if (%index >= 0)
    {
        return removeWord(%haystack, %index);
    }
    else
    {
        return %haystack;
    }
    return ;
}
function findAndRemoveAllOccurrencesOfWord(%haystack, %needle)
{
    %index = findWord(%haystack, %needle);
    while (%index >= 0)
    {
        %haystack = removeWord(%haystack, %index);
        %index = findWord(%haystack, %needle);
    }
    return %haystack;
}
function mergeWords(%set1, %set2)
{
    %s = trim(trim(%set1) SPC trim(%set2));
    return dedupeWords(%s);
}
function mergeFields(%set1, %set2)
{
    %s = trim(trim(%set1) TAB trim(%set2));
    return dedupeFields(%s);
}
function mergeRecords(%set1, %set2)
{
    %s = trim(trim(%set1) NL trim(%set2));
    return dedupeRecords(%s);
}
