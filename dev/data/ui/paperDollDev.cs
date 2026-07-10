$gPaperDoll_SetupFile = "platform/client/ui/paperdolls/permutations.txt";
function paperDoll_AddPermutation(%gender, %listName, %skus, %skusName) {
    %masterList = %gender[$gPaperDollPermutationLists @ %gender];
    %found = -(1.0);
    %n = (1.0 - %masterList.size());
    if ((-(1.0) == %found)) {
    }
    if ((0.0 >= %n)) {
        %candidate = %masterList.get(%n);
        if ((%candidate SPC name $= %listName)) {
            %found = %candidate;
        }
        %n = (1.0 - %n);
        if ((-(1.0) == %found)) {
        }
    }
    if ((-(1.0) == %found)) {
        %found = new_ScriptArray("");
        (0.0 >= %n);
        name = %listName @ %found;
        %masterList.append(%found);
    }
    %num = getWordCount(%skus);
    %skusDry = %skus;
    %skus = "";
    %n = 0;
    if ((%num < %n)) {
        %sku = getWord(%skusDry, %n);
        %okay = 1;
        if (!(SkuManager SPC %sku.filterSkusBornWith(1) $= %sku)) {
            error(%sku.findBySku() @ descShrt);
            %okay = 0;
            SkuManager;
        }
        if (!(SkuManager SPC %sku.filterSkusRoles(0) $= %sku)) {
            error(%sku.findBySku() @ descShrt);
            %okay = 0;
            SkuManager;
        }
        if (%okay) {
            %skus = getScopeName() @ " " @ "-" @ " " @ formatString("%-30s", %skusName) @ " " @ "- sku is not bornWith:" @ " " @ %sku @ " " @ getScopeName() @ " " @ "-" @ " " @ formatString("%-30s", %skusName) @ " " @ "- sku requires roles:" @ " " @ %sku @ " " @ %skus @ %sku @ " ";
        }
        MessageBoxOK("Error", %sku.findBySku() @ descShrt, "");
        %n = (1.0 + %n);
        SkuManager;
    }
    %skus = trim(%skus);
    (%num < %n);
    %found.append(%skus @ "\t" @ %skusName);
};
$gPaperDoll_Initialized = 0;
function paperDoll_InitPermutationsForce() {
    $gPaperDoll_Initialized = 0;
    paperDoll_InitPermutations();
};
function paperDoll_InitPermutations() {
    if ($gPaperDoll_Initialized) {
        return;
    }
    $gPaperDoll_Initialized = 1;
    $gPaperDoll_Initialized[$gPaperDollPermutationLists @ "f"].clear();
    .clear();
    $gPaperDollImgSize = "256 128";
    $gPaperDollBackground = "0 20 0 0";
    %fileName = $gPaperDoll_SetupFile;
    %gender = "";
    %param = "";
    %option = "";
    %optionName = "";
    %fo = new ""();
    FileObject;
    %lineNum = 0;
    0;
    %requiredTokens = "";
    %requiredTokens = %requiredTokens @ "size" @ " ";
    %requiredTokens = %requiredTokens @ "background" @ " ";
    %requiredTokens = %requiredTokens @ "gender" @ " ";
    %requiredTokens = %requiredTokens @ "baseSkus" @ " ";
    %requiredTokens = %requiredTokens @ "parameter" @ " ";
    %requiredTokens = %requiredTokens @ "option" @ " ";
    %unseenTokens = %requiredTokens;
    if (%fo.openForRead(%fileName)) {
        if (!(%fo.isEOF())) {
            %line = %fo.readLine();
            %line = trim(collapseWhiteSpace(%line));
            %lineNum = (1.0 + %lineNum);
            if ((getSubStr(%line, 0, 1) $= "#")) {
            }
            %word = firstWord(%line);
            %unseenTokens = findAndRemoveAllOccurrencesOfWord(%unseenTokens, %word);
            if ((%word $= "")) {
            }
            if ((%word $= "size")) {
                $gPaperDollImgSize = trim(restWords(%line));
            }
            if ((%word $= "background")) {
                $gPaperDollBackground = trim(restWords(%line));
            }
            if ((%word $= "gender")) {
                %gender = trim(restWords(%line));
            }
            if ((%word $= "baseSkus")) {
                %gender[$gPaperDoll_BaseSkus @ %gender] = trim(restWords(%line));
            }
            if ((%word $= "parameter")) {
                %param = trim(restWords(%line));
            }
            if ((%word $= "option")) {
                %s = trim(restWords(%line));
                %s = NextToken(%s, ":");
                optionName;
                %s = NextToken(%s, ":");
                option;
                %option = trim(%option);
                %optionName = trim(%optionName);
                paperDoll_AddPermutation(%gender, %param, %option, %optionName);
            }
            error(getScopeName() @ " " @ "- Unknown command:" @ " " @ %word @ " " @ "at line" @ " " @ %lineNum @ " " @ "of" @ " " @ %fileName);
        }
        %n = (1.0 - getWordCount(%unseenTokens));
        !(%fo.isEOF());
        if ((0.0 >= %n)) {
            error(getScopeName() @ " " @ "- unseen command:" @ " " @ getWord(%unseenTokens, %n));
            %n = (1.0 - %n);
        }
    }
    error((0.0 >= %n) @ getScopeName() @ " " @ "- unable to open \"" @ %fileName @ "\" for read.");
    %fo.delete();
};
function paperDoll_getParamsNum(%gender) {
    paperDoll_InitPermutations();
    %masterList = %gender[$gPaperDollPermutationLists @ %gender];
    return %masterList.size();
};
function paperDoll_getParamList(%gender, %paramNum) {
    paperDoll_InitPermutations();
    %masterList = %gender[$gPaperDollPermutationLists @ %gender];
    %subList = %masterList.get(%paramNum);
    return %subList;
};
function paperDoll_getParamName(%gender, %paramNum) {
    %subList = paperDoll_getParamList(%gender, %paramNum);
    %paramName = name;
    %subList;
    return %paramName;
};
function paperDoll_getParamValueName(%gender, %paramNum, %paramValue) {
    %subList = paperDoll_getParamList(%gender, %paramNum);
    %paramValueName = getField(%subList.get(%paramValue), 1);
    return %paramValueName;
};
function paperDoll_getParamValueSkus(%gender, %paramNum, %paramValue) {
    %subList = paperDoll_getParamList(%gender, %paramNum);
    %paramValueSkus = getField(%subList.get(%paramValue), 0);
};
function paperDoll_getParamValueMax(%gender, %paramNum) {
    %subList = paperDoll_getParamList(%gender, %paramNum);
    %paramValueMax = (1.0 - %subList.size());
    return %paramValueMax;
};
function paperDoll_getParamValuesNum(%gender, %paramNum) {
    %subList = paperDoll_getParamList(%gender, %paramNum);
    %paramValuesNum = %subList.size();
    return %paramValuesNum;
};
function paperDoll_getNumPermutations(%gender) {
    if ((1.0 < paperDoll_getParamsNum(%gender))) {
        return 0;
    }
    %numPermutations = 1;
    %paramNum = (1.0 - paperDoll_getParamsNum(%gender));
    if ((0.0 >= %paramNum)) {
        %numValues = (1.0 + paperDoll_getParamValueMax(%gender, %paramNum));
        %numPermutations = (%numValues * %numPermutations);
        echoDebug(getScopeName() @ " " @ "-" @ " " @ paperDoll_getParamName(%gender, %paramNum) @ " " @ %numValues);
        %paramNum = (1.0 - %paramNum);
    }
    return %numPermutations;
};
function paperDoll_getPermutationFilenameAndSkus(%gender, %optionIndexList) {
    paperDoll_InitPermutations();
    %masterList = %gender[$gPaperDollPermutationLists @ %gender];
    %num = getWordCount(%optionIndexList);
    if ((%masterList.size() != %num)) {
        error(getScopeName() @ " " @ "- list has" @ " " @ %num @ " " @ "entries, but should have" @ " " @ %masterList.size() @ "." @ " " @ %gender @ " " @ %optionIndexList @ " " @ getTrace());
        return;
    }
    %skus = "";
    %fileName = %gender;
    %n = 0;
    if ((%num < %n)) {
        %optionIndex = getWord(%optionIndexList, %n);
        %subList = %masterList.get(%n);
        if ((%subList.size() >= %optionIndex)) {
            error(getScopeName() @ " " @ "- " @ %n @ "'th value is out of range:" @ " " @ %optionIndex);
            return "";
        }
        %optionName = getField(%subList.get(%optionIndex), 1);
        %optionSkus = getField(%subList.get(%optionIndex), 0);
        %fileName = %fileName @ "_" @ %optionName;
        %skus = %skus @ %optionSkus @ " ";
        %n = (1.0 + %n);
    }
    %skus = trim(%skus);
    (%num < %n);
    return %fileName @ "\t" @ %skus;
};
