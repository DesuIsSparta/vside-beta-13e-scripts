$gPaperDollPermutationLists["f"] = new_ScriptArray("") ;
$gPaperDollPermutationLists["m"] = new_ScriptArray("") ;
$gPaperDoll_SetupFile = "platform/client/ui/paperdolls/permutations.txt";
function paperDoll_AddPermutation(%gender, %listName, %skus, %skusName)
{
    %masterList = $gPaperDollPermutationLists[%gender];
    %found = -1;
    %n = %masterList.size() - 1;
    while (%n >= 0)
    {
        %candidate = %masterList.get(%n);
        if (%candidate.name $= %listName)
        {
            %found = %candidate;
        }
        %n = %n - 1;
    }
    if (%found == -1)
    {
        %found = new_ScriptArray("");
        %found.name = %listName;
        %masterList.append(%found);
    }
    %num = getWordCount(%skus);
    %skusDry = %skus;
    %skus = "";
    %n = 0;
    while (%n < %num)
    {
        %sku = getWord(%skusDry, %n);
        %okay = 1;
        if (!(SkuManager.filterSkusBornWith(%sku, 1) $= %sku))
        {
            error(getScopeName() SPC "-" SPC formatString("%-30s", %skusName) SPC "- sku is not bornWith:" SPC %sku SPC SkuManager.findBySku(%sku).descShrt);
            %okay = 0;
        }
        if (!(SkuManager.filterSkusRoles(%sku, 0) $= %sku))
        {
            error(getScopeName() SPC "-" SPC formatString("%-30s", %skusName) SPC "- sku requires roles:" SPC %sku SPC SkuManager.findBySku(%sku).descShrt);
            %okay = 0;
        }
        if (%okay)
        {
            %skus = %skus @ %sku @ " ";
        }
        else
        {
            MessageBoxOK("Error", "invalid sku" SPC %sku SPC "in paper doll list\nsee console.log for\"" SPC getScopeName() SPC "\"" NL %skusName NL %sku SPC SkuManager.findBySku(%sku).descShrt, "");
        }
        %n = %n + 1;
    }
    %skus = trim(%skus);
    %found.append(%skus TAB %skusName);
    return %found;
}
$gPaperDoll_Initialized = 0;
function paperDoll_InitPermutationsForce()
{
    $gPaperDoll_Initialized = 0;
    paperDoll_InitPermutations();
    return ;
}
function paperDoll_InitPermutations()
{
    if ($gPaperDoll_Initialized)
    {
        return ;
    }
    $gPaperDoll_Initialized = 1;
    $gPaperDollPermutationLists["f"].clear();
    $gPaperDollPermutationLists["m"].clear();
    $gPaperDollImgSize = "256 128";
    $gPaperDollBackground = "0 20 0 0";
    %fileName = $gPaperDoll_SetupFile;
    %gender = "";
    %param = "";
    %option = "";
    %optionName = "";
    %fo = new FileObject();
    %lineNum = 0;
    %requiredTokens = "";
    %requiredTokens = %requiredTokens @ "size" @ " ";
    %requiredTokens = %requiredTokens @ "background" @ " ";
    %requiredTokens = %requiredTokens @ "gender" @ " ";
    %requiredTokens = %requiredTokens @ "baseSkus" @ " ";
    %requiredTokens = %requiredTokens @ "parameter" @ " ";
    %requiredTokens = %requiredTokens @ "option" @ " ";
    %unseenTokens = %requiredTokens;
    if (%fo.openForRead(%fileName))
    {
        while (!%fo.isEOF())
        {
            %line = %fo.readLine();
            %line = trim(collapseWhiteSpace(%line));
            %lineNum = %lineNum + 1;
            if (getSubStr(%line, 0, 1) $= "#")
            {
                continue;
            }
            %word = firstWord(%line);
            %unseenTokens = findAndRemoveAllOccurrencesOfWord(%unseenTokens, %word);
            if (%word $= "")
            {
                continue;
            }
            if (%word $= "size")
            {
                $gPaperDollImgSize = trim(restWords(%line));
            }
            else
            {
                if (%word $= "background")
                {
                    $gPaperDollBackground = trim(restWords(%line));
                }
                else
                {
                    if (%word $= "gender")
                    {
                        %gender = trim(restWords(%line));
                    }
                    else
                    {
                        if (%word $= "baseSkus")
                        {
                            $gPaperDoll_BaseSkus[%gender] = trim(restWords(%line)) ;
                        }
                        else
                        {
                            if (%word $= "parameter")
                            {
                                %param = trim(restWords(%line));
                            }
                            else
                            {
                                if (%word $= "option")
                                {
                                    %s = trim(restWords(%line));
                                    %s = NextToken(%s, optionName, ":");
                                    %s = NextToken(%s, option, ":");
                                    %option = trim(%option);
                                    %optionName = trim(%optionName);
                                    paperDoll_AddPermutation(%gender, %param, %option, %optionName);
                                }
                                else
                                {
                                    error(getScopeName() SPC "- Unknown command:" SPC %word SPC "at line" SPC %lineNum SPC "of" SPC %fileName);
                                }
                            }
                        }
                    }
                }
            }
        }
        %n = getWordCount(%unseenTokens) - 1;
        while (%n >= 0)
        {
            error(getScopeName() SPC "- unseen command:" SPC getWord(%unseenTokens, %n));
            %n = %n - 1;
        }
    }
    else
    {
        error(getScopeName() SPC "- unable to open \"" @ %fileName @ "\" for read.");
    }
    %fo.delete();
    return ;
}
function paperDoll_getParamsNum(%gender)
{
    paperDoll_InitPermutations();
    %masterList = $gPaperDollPermutationLists[%gender];
    return %masterList.size();
}
function paperDoll_getParamList(%gender, %paramNum)
{
    paperDoll_InitPermutations();
    %masterList = $gPaperDollPermutationLists[%gender];
    %subList = %masterList.get(%paramNum);
    return %subList;
}
function paperDoll_getParamName(%gender, %paramNum)
{
    %subList = paperDoll_getParamList(%gender, %paramNum);
    %paramName = %subList.name;
    return %paramName;
}
function paperDoll_getParamValueName(%gender, %paramNum, %paramValue)
{
    %subList = paperDoll_getParamList(%gender, %paramNum);
    %paramValueName = getField(%subList.get(%paramValue), 1);
    return %paramValueName;
}
function paperDoll_getParamValueSkus(%gender, %paramNum, %paramValue)
{
    %subList = paperDoll_getParamList(%gender, %paramNum);
    %paramValueSkus = getField(%subList.get(%paramValue), 0);
    return ;
}
function paperDoll_getParamValueMax(%gender, %paramNum)
{
    %subList = paperDoll_getParamList(%gender, %paramNum);
    %paramValueMax = %subList.size() - 1;
    return %paramValueMax;
}
function paperDoll_getParamValuesNum(%gender, %paramNum)
{
    %subList = paperDoll_getParamList(%gender, %paramNum);
    %paramValuesNum = %subList.size();
    return %paramValuesNum;
}
function paperDoll_getNumPermutations(%gender)
{
    if (paperDoll_getParamsNum(%gender) < 1)
    {
        return 0;
    }
    %numPermutations = 1;
    %paramNum = paperDoll_getParamsNum(%gender) - 1;
    while (%paramNum >= 0)
    {
        %numValues = paperDoll_getParamValueMax(%gender, %paramNum) + 1;
        %numPermutations = %numPermutations * %numValues;
        echoDebug(getScopeName() SPC "-" SPC paperDoll_getParamName(%gender, %paramNum) SPC %numValues);
        %paramNum = %paramNum - 1;
    }
    return %numPermutations;
}
function paperDoll_getPermutationFilenameAndSkus(%gender, %optionIndexList)
{
    paperDoll_InitPermutations();
    %masterList = $gPaperDollPermutationLists[%gender];
    %num = getWordCount(%optionIndexList);
    if (%num != %masterList.size())
    {
        error(getScopeName() SPC "- list has" SPC %num SPC "entries, but should have" SPC %masterList.size() @ "." SPC %gender SPC %optionIndexList SPC getTrace());
        return ;
    }
    %skus = "";
    %fileName = %gender;
    %n = 0;
    while (%n < %num)
    {
        %optionIndex = getWord(%optionIndexList, %n);
        %subList = %masterList.get(%n);
        if (%optionIndex >= %subList.size())
        {
            error(getScopeName() SPC "- " @ %n @ "\'th value is out of range:" SPC %optionIndex);
            return "";
        }
        %optionName = getField(%subList.get(%optionIndex), 1);
        %optionSkus = getField(%subList.get(%optionIndex), 0);
        %fileName = %fileName @ "_" @ %optionName;
        %skus = %skus @ %optionSkus @ " ";
        %n = %n + 1;
    }
    %skus = trim(%skus);
    return %fileName TAB %skus;
}
