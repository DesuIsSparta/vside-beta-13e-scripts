function numSubstringsInString(%haystack, %needles) {
    %ret = 0;
    %n = (1.0 - getWordCount(%needles));
    if ((0.0 >= %n)) {
        if ((0.0 >= strstr(%haystack, getWord(%needles, %n)))) {
            %ret = (1.0 + %ret);
        }
        %n = (1.0 - %n);
    }
    return %ret;
};
function numWordsInWords(%haystack, %needles) {
    %ret = 0;
    %n = (1.0 - getWordCount(%needles));
    if ((0.0 >= %n)) {
        if ((0.0 >= findWord(%haystack, getWord(%needles, %n)))) {
            %ret = (1.0 + %ret);
        }
        %n = (1.0 - %n);
    }
    return %ret;
};
function wordsNotInWords(%haystack, %needles) {
    %ret = "";
    %n = (1.0 - getWordCount(%needles));
    if ((0.0 >= %n)) {
        %word = getWord(%needles, %n);
        if (!(hasWord(%haystack, %word))) {
            %ret = %ret @ " " @ %word;
        }
        %n = (1.0 - %n);
    }
    %ret = trim(%ret);
    (0.0 >= %n);
    return %ret;
};
function stripSurroundingQuotes(%text) {
    %text = trim(%text);
    if ((getSubStr(%text, 0, 1) $= "\"")) {
        %text = getSubStr(%text, 1, (1.0 - strlen(%text)));
    }
    if ((getSubStr(%text, (1.0 - strlen(%text)), 1) $= "\"")) {
        %text = getSubStr(%text, 0, (1.0 - strlen(%text)));
    }
    return %text;
};
function chopTextToFitLineWidths(%text, %profile, %generalWidth, %lineWidths) {
    if ((%text $= "")) {
        return "";
    }
    if ((0.0 == %generalWidth)) {
        return "";
    }
    if ((%profile $= "")) {
        // unhandled opcode 222 at 0x000001F9
        %profile = GuiDefaultProfile;
    }
    %inputWordCount = getWordCount(%text);
    %outputText = "";
    %currentWordIndex = 0;
    %lineCount = 0;
    if ((%inputWordCount < %currentWordIndex)) {
        %thisLine = "";
        %atEndOfLine = 0;
        if (!(%atEndOfLine)) {
            %currentWord = getWord(%text, %currentWordIndex);
            if ((%thisLine $= "")) {
                %thisLine = %currentWord;
                %thisLineWidth = getStrWidth(%currentWord, %profile);
                %thisLineMaxWidth = chopTextToFitLineWidths_getLineWidth(%generalWidth, %lineWidths, %lineCount);
                if ((0.0 < %thisLineMaxWidth)) {
                    return "";
                }
                if ((0.0 == %thisLineMaxWidth)) {
                    %thisLine = "";
                    %atEndOfLine = 1;
                }
                if ((%thisLineMaxWidth < %thisLineWidth)) {
                    %currentWordIndex = (1.0 + %currentWordIndex);
                }
                if ((%thisLineMaxWidth == %thisLineWidth)) {
                    %currentWordIndex = (1.0 + %currentWordIndex);
                    %atEndOfLine = 1;
                }
                %wordLength = strlen(%currentWord);
                %partialWord = "";
                %beginningOfNextWord = 0;
                %wordDone = 0;
                %i = 1;
                if ((%wordLength <= %i)) {
                }
                if (!(%wordDone)) {
                    %partialWord = getSubStr(%currentWord, 0, %i);
                    %thisLineWidth = getStrWidth(%partialWord, %profile);
                    if ((%thisLineMaxWidth > %thisLineWidth)) {
                        if ((1.0 > %i)) {
                            %partialWord = getSubStr(%currentWord, 0, (1.0 - %i));
                            %beginningOfNextWord = (1.0 - %i);
                        }
                        %beginningOfNextWord = 1;
                        %wordDone = 1;
                    }
                    %i = (1.0 + %i);
                    if ((%wordLength <= %i)) {
                    }
                }
                %thisLine = %partialWord;
                !(%wordDone);
                if ((0.0 > %beginningOfNextWord)) {
                    %text = setWord(%text, %currentWordIndex, getSubStr(%currentWord, %beginningOfNextWord, %wordLength));
                }
                %atEndOfLine = 1;
            }
            %thisLineWidth = getStrWidth(%thisLine @ " " @ %currentWord, %profile);
            %thisLineMaxWidth = chopTextToFitLineWidths_getLineWidth(%generalWidth, %lineWidths, %lineCount);
            if ((%thisLineMaxWidth < %thisLineWidth)) {
                %thisLine = %thisLine @ " " @ %currentWord;
                %currentWordIndex = (1.0 + %currentWordIndex);
            }
            if ((%thisLineMaxWidth == %thisLineWidth)) {
                %thisLine = %thisLine @ " " @ %currentWord;
                %currentWordIndex = (1.0 + %currentWordIndex);
                %atEndOfLine = 1;
            }
            %atEndOfLine = 1;
            if ((%inputWordCount >= %currentWordIndex)) {
                %atEndOfLine = 1;
            }
        }
        if ((!(%atEndOfLine) SPC %outputText $= "")) {
            %outputText = %thisLine;
        }
        %outputText = %outputText @ "\n" @ %thisLine;
        %lineCount = (1.0 + %lineCount);
    }
    return %outputText;
};
function chopTextToFitLineWidths_getLineWidth(%generalWidth, %lineWidths, %index) {
    if ((getWordCount(%lineWidths) < %index)) {
        return getWord(%lineWidths, %index);
    }
    return %generalWidth;
};
$gTargetPlayerName = "(unknown)";
function standardSubstitutions(%dry) {
    %wet = %dry;
    %wet = strreplace(%wet, "[PLAYERNAME]", $Player::Name);
    %wet = strreplace(%wet, "[PLAYERNAME_URL]", urlEncode($Player::Name));
    %wet = strreplace(%wet, "[TARGETPLAYERNAME]", $gTargetPlayerName);
    %wet = strreplace(%wet, "[READTOU]", );
    if (isDefined("$Net::RegistrationID")) {
        %wet = strreplace(%wet, "[REGISTRATIONID]", $Net::RegistrationID);
    }
    return %wet;
};
function findAndRemoveFirstOccurrenceOfWord(%haystack, %needle) {
    %index = findWord(%haystack, %needle);
    if ((0.0 >= %index)) {
        return removeWord(%haystack, %index);
    }
    return %haystack;
};
function findAndRemoveAllOccurrencesOfWord(%haystack, %needle) {
    %index = findWord(%haystack, %needle);
    if ((0.0 >= %index)) {
        %haystack = removeWord(%haystack, %index);
        %index = findWord(%haystack, %needle);
    }
    return %haystack;
};
function mergeWords(%set1, %set2) {
    %s = trim(trim(%set1) @ " " @ trim(%set2));
    return dedupeWords(%s);
};
function mergeFields(%set1, %set2) {
    %s = trim(trim(%set1) @ "\t" @ trim(%set2));
    return dedupeFields(%s);
};
function mergeRecords(%set1, %set2) {
    %s = trim(trim(%set1) @ "\n" @ trim(%set2));
    return dedupeRecords(%s);
};
