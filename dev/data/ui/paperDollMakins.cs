$gPaperDoll_DryRun = 0;
$gPaperDoll_NumRemaining = 0;
$gPaperDoll_ImgExtension = "png";
$gPaperDoll_Nudge = "0 0";
$gPaperDoll_NudgeStep = "0.025 0.025";
function gePaperDollMakins::open(%this)
{
    $UserPref::Video::ConstrainWindowDimensions = 0;
    Canvas.pushDialog(%this, 0);
    setScreenMode(1048, 1048, getWord($UserPref::Video::Resolution, 2), 0);
    if (!gePaperDollWhichSetup_Client.getValue() && !gePaperDollWhichSetup_Web.getValue())
    {
        gePaperDollWhichSetup_Client.performClick();
    }
    %this.paperDoll_refresh();
    if (isObject($player))
    {
        gePaperDollObjectView.setSimObject($player);
    }
    gePaperDollInfo_Long.setText("<color:ffffff>" NL "Welcome to the paper doll making interface." NL "This runs on two different folders." NL "for the client:<spush><b>platform/client/ui/paperdolls/<spop>" NL "for the web:<spush><b>web/paperdolls/<spop>." NL "in each of those, <spush><b>permutations.txt<spop> sets up everything." NL "When you click \"refresh\", <spush><b>permutations.xml<spop> and <spush><b>permutations_manifest.txt<spop> are generated." NL "" NL "The client and the web can have different setup files. eg, you can have [many] more options on the web, if you want." NL "" NL "The size of the avatar area is set in permutations.txt." NL "Since the alpha channel is not anti-aliased, i recommend setting the avatar area to twice the actual desired image size, and then using photoshop or similar to batch-process the images down to size." NL "" NL "Also, surfaces which have alpha (such as glasses or some hair) will be saved transparent in those regions, which looks weird. To fix this, again use batch processing in photoshop to simply duplicate the layer of the image several times, building up the opacity." NL "" NL "During a real run, the images are saved out to <spush><b>images/source<spop>." NL "<spush><color:77FF44><b>For the client, these must be copied into just plain \"images/\"!<spop>" NL "For the web, they may need to be copied elsewhere as well; that process hasn\'t been worked out yet." NL "" NL "Don\'t check in the images in \"source/\", only the ones from \"images\"." NL "" NL "During a real or dry run, HTML files are also generated previewing all the images." NL "" NL "<spush><color:77FF44><b>To get alpha, the -alphaBuffer option must be used on the command line.<spop>");
    return ;
}
function gePaperDollMakins::close(%this)
{
    Canvas.popDialog(%this);
    return ;
}
function gePaperDollMakins::paperDoll_refresh(%this)
{
    $gPaperDoll_SetupFile = paperDoll_getBaseFilepath() @ "permutations.txt";
    paperDoll_InitPermutationsForce();
    %numf = paperDoll_getNumPermutations("f");
    %numm = paperDoll_getNumPermutations("m");
    if (!isObject($player))
    {
        %genderText = "(none)";
        %gender = "X";
    }
    else
    {
        %genderText = $player.getGender() $= "f" ? "female" : "male";
        %gender = $player.getGender();
    }
    %text = "";
    %text = %text @ "num F =" SPC %numf;
    %text = %text NL "num M =" SPC %numm;
    %text = %text NL "currently:" SPC %genderText;
    gePaperDollInfo.setText(%text);
    gePaperDollEraser.resize(getWord($gPaperDollImgSize, 0), getWord($gPaperDollImgSize, 1));
    gePaperDollEraser.eraserColor = $gPaperDollBackground;
    paperDoll_generateXML();
    paperDoll_generateJSON();
    paperDoll_generateManifest();
    $gPaperDoll_SkuArray = new_ScriptArray("");
    paperDoll_RecursePermutations(%gender, "", %gender, 0, $gPaperDoll_SkuArray);
    $gPaperDoll_SkuArray.dumpValues();
    return ;
}
function paperDoll_StartTakingSnaps()
{
    $gPaperDoll_ObjViewCtrl = gePaperDollObjectView;
    $gPaperDoll_CurIndex = 0;
    $gPaperDoll_CancelRun = 0;
    $gPaperDoll_PreviewFile = "";
    if (1)
    {
        $gPaperDoll_PreviewFile = new FileObject();
        %fileName = paperDoll_getBaseFilepath();
        %fileName = %fileName @ "index_" @ $player.getGender() @ ".html";
        $gPaperDoll_PreviewFile.openForWrite(%fileName);
        $gPaperDoll_PreviewFile.writeLine("<html>\n<body background=\"greychecks.png\">");
    }
    gePaperDollDryRun.setVisible(0);
    gePaperDollRealRun.setVisible(0);
    gePaperDollCancel.setVisible(1);
    gePaperDollInfo_Running.setVisible(1);
    $gPaperDoll_NumRemaining = paperDoll_getNumPermutations($player.getGender());
    paperDoll_prepareNextSnapshot();
    return ;
}
function paperDoll_finishedSnapshots()
{
    if (isObject($gPaperDoll_PreviewFile))
    {
        $gPaperDoll_PreviewFile.writeLine("</body>\n</html>");
        $gPaperDoll_PreviewFile.close();
        $gPaperDoll_PreviewFile.delete();
        $gPaperDoll_PreviewFile = "";
    }
    gePaperDollDryRun.setVisible(1);
    gePaperDollRealRun.setVisible(1);
    gePaperDollCancel.setVisible(0);
    gePaperDollInfo_Running.setVisible(0);
    return ;
}
function paperDoll_prepareNextSnapshot()
{
    paperDoll_prepareOneSnapshot($gPaperDoll_CurIndex);
    waitAFrameAndCall("paperDoll_callingTakeCurrentSnapshot");
    return ;
}
function paperDoll_prepareOneSnapshot(%index)
{
    if ((%index < 0) && (%index >= $gPaperDoll_SkuArray.size()))
    {
        return ;
    }
    $gPaperDoll_CurSkus = getField($gPaperDoll_SkuArray.get(%index), 0);
    $gPaperDoll_CurName = getField($gPaperDoll_SkuArray.get(%index), 1);
    %skus = SkuManager.overlaySkus($gPaperDoll_BaseSkus[$player.getGender()], $gPaperDoll_CurSkus);
    $gPaperDoll_ObjViewCtrl.setSkus(%skus);
    gePaperDollCurOutfitField.setValue(%index);
    %tmp = gePaperDollCurOutfitSlider.altCommand;
    gePaperDollCurOutfitSlider.altCommand = "";
    gePaperDollCurOutfitSlider.setValue(1 - (%index / ($gPaperDoll_SkuArray.size() - 1)));
    gePaperDollCurOutfitSlider.altCommand = %tmp;
    $gPaperDoll_CurIndex = %index;
    return ;
}
function paperDoll_Permute_Cancel()
{
    $gPaperDoll_CancelRun = 1;
    return ;
}
function paperDoll_callingTakeCurrentSnapshot()
{
    paperDoll_takeCurrentSnapshot();
    if (!$gPaperDoll_CancelRun && ($gPaperDoll_CurIndex < ($gPaperDoll_SkuArray.size() - 1)))
    {
        $gPaperDoll_CurIndex = $gPaperDoll_CurIndex + 1;
        paperDoll_prepareNextSnapshot();
    }
    else
    {
        paperDoll_finishedSnapshots();
    }
    return ;
}
function paperDoll_getBaseFilepath()
{
    if (gePaperDollWhichSetup_Client.getValue())
    {
        %ret = "platform/client/ui/paperdolls/";
    }
    else
    {
        %ret = "web/paperdolls/";
    }
    return %ret;
}
function paperDoll_takeCurrentSnapshot()
{
    %justFileName = $gPaperDoll_CurName;
    %justFileName = %justFileName @ "." @ $gPaperDoll_ImgExtension;
    %fileName = paperDoll_getBaseFilepath() @ "images/source/" @ %justFileName;
    if (!$gPaperDoll_DryRun)
    {
        $gPaperDoll_ObjViewCtrl.snapshot(%fileName);
    }
    if (isObject($gPaperDoll_PreviewFile))
    {
        $gPaperDoll_PreviewFile.writeLine("<img src=\"images/" @ %justFileName @ "\">");
    }
    $gPaperDoll_NumRemaining = $gPaperDoll_NumRemaining - 1;
    gePaperDollInfo_Running.setText("Remaining:" SPC $gPaperDoll_NumRemaining);
    return ;
}
function paperDoll_MakePermutations(%gender)
{
    paperDoll_StartTakingSnaps();
    return ;
}
function paperDoll_RecursePermutations(%gender, %currentSkus, %currentNames, %startingDepth, %array)
{
    %masterList = $gPaperDollPermutationLists[%gender];
    %masterListSize = %masterList.size();
    if (%startingDepth >= %masterListSize)
    {
        %array.append(%currentSkus TAB %currentNames);
        return ;
    }
    %subList = %masterList.get(%startingDepth);
    %subListSize = %subList.size();
    %n = 0;
    while (%n < %subListSize)
    {
        %skus = %currentSkus @ getField(%subList.get(%n), 0) @ " ";
        %names = %currentNames @ "_" @ getField(%subList.get(%n), 1);
        paperDoll_RecursePermutations(%gender, %skus, %names, %startingDepth + 1, %array);
        %n = %n + 1;
    }
}

function paperDoll_PermuteWithDialog()
{
    userTips::showOnceThisSession("PaperDollPermute");
    return ;
}
function paperDoll_Permute()
{
    schedule(1000, 0, "paperDoll_Permute_Really");
    return ;
}
function paperDoll_Permute_Really()
{
    $gPaperDoll_DryRun = 0;
    paperDoll_InitPermutationsForce();
    paperDoll_MakePermutations($player.getGender());
    return ;
}
function paperDoll_Permute_DryRun()
{
    $gPaperDoll_DryRun = 1;
    paperDoll_InitPermutationsForce();
    paperDoll_MakePermutations($player.getGender());
    return ;
}
function paperDoll_Nudge(%vec)
{
    %vec = VectorScale(%vec, $gPaperDoll_NudgeStep);
    $gPaperDoll_Nudge = VectorAdd($gPaperDoll_Nudge, %vec);
    gePaperDollObjectView.setLookAtNudge(getWord($gPaperDoll_Nudge, 0) SPC 0 SPC getWord($gPaperDoll_Nudge, 1));
    gePaperDollNudgeField.setValue(getWords($gPaperDoll_Nudge, 0, 1));
    return ;
}
function paperDoll_NudgeSet(%vec)
{
    $gPaperDoll_Nudge = %vec;
    paperDoll_Nudge("0 0");
    return ;
}
function paperDoll_CurOutfitSet(%val)
{
    %firstChar = getSubStr(%val, 0, 1);
    if (%firstChar $= "-")
    {
        %newVal = $gPaperDoll_CurIndex + %val;
    }
    else
    {
        if (%firstChar $= "+")
        {
            %newVal = $gPaperDoll_CurIndex + getSubStr(%val, 1, 100);
        }
        else
        {
            %newVal = %val;
        }
    }
    paperDoll_prepareOneSnapshot(%newVal);
    return ;
}
function gePaperDollCurOutfitSlider::valueChanged(%this)
{
    %val = mFloor(($gPaperDoll_SkuArray.size() - 1) * (1 - %this.getValue()));
    paperDoll_prepareOneSnapshot(%val);
    return ;
}
function paperDoll_generateXML()
{
    %fileName = paperDoll_getBaseFilepath() @ "permutations.xml";
    %file = new FileObject();
    if (!%file.openForWrite(%fileName))
    {
        error(getScopeName() SPC "- unable to open \"" @ %fileName @ "\" for write.");
        %file.delete();
        return ;
    }
    %file.indent = "";
    %file.indentString = "    ";
    %file.writeLineIndented("<?xml version=\"1.0\" encoding=\"UTF-8\"?>");
    %file.writeLineIndented("");
    %file.writeLineIndented("<!--");
    %file.indent();
    %file.writeLineIndented("Document   : permutations.xml");
    %file.writeLineIndented("Created on : " @ getTimeStamp());
    %file.writeLineIndented("Author     : envClient / orion");
    %file.writeLineIndented("Description: a description of the parameters and their possible values for each gender which describe the possible paper dolls (initial avatars) of the player." NL "an image must exist for every permutation, of the file name \"<gender>_<param0 value>_<param1 value>_..._<paramN value>.<image extension>\"" NL "see also permutations_manifest.txt for an enumeration of the files & skus generated by this set of parameters & values.");
    %file.unindent();
    %file.writeLineIndented("-->");
    %genders = "f m";
    %gendersLong["f"] = "female";
    %gendersLong["m"] = "male";
    %file.writeLineIndented("");
    %file.writeOpenTag("Permutations", "xmlns=\"http://www.doppelganger.com/datamodel\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xsi:schemaLocation=\"http://www.doppelganger.com/datamodel schema/initial_avatar_permutations.xsd\"");
    %file.writeCommentTag("Nikita: need schema description in previous ?");
    %n = 0;
    while (%n < getWordCount(%genders))
    {
        %gender = getWord(%genders, %n);
        %genderLong = %gendersLong[%gender];
        %file.writeLineIndented("");
        %file.writeOpenTag("Gender", "name=\"" @ %gender @ "\"");
        %file.writeCommentTag("Parameters for gender" SPC %genderLong);
        %paramNum = 0;
        while (%paramNum < paperDoll_getParamsNum(%gender))
        {
            %paramName = paperDoll_getParamName(%gender, %paramNum);
            %file.writeLineIndented("");
            %file.writeOpenTag("Param", "name=\"" @ %paramName @ "\"");
            %file.writeCommentTag("Possible values for" SPC %genderLong SPC "parameter" SPC %paramName);
            %valueNum = 0;
            while (%valueNum < paperDoll_getParamValuesNum(%gender, %paramNum))
            {
                %file.writeLineIndented("");
                %valuename = paperDoll_getParamValueName(%gender, %paramNum, %valueNum);
                %valueSkus = paperDoll_getParamValueSkus(%gender, %paramNum, %valueNum);
                %file.writeOpenTag("Value", "name=\"" @ %valuename @ "\"");
                %skunum = 0;
                while (%skunum < getWordCount(%valueSkus))
                {
                    %sku = getWord(%valueSkus, %skunum);
                    %file.writeShortTag("sku", "", %sku);
                    %skunum = %skunum + 1;
                }
                %file.writeCloseTag("Value");
                %valueNum = %valueNum + 1;
            }
            %file.writeCloseTag("Param");
            %paramNum = %paramNum + 1;
        }
        %file.writeCloseTag("Gender");
        %n = %n + 1;
    }
    %file.writeCloseTag("Permutations");
    %file.close();
    %file.delete();
    return ;
}
function paperDoll_generateJSON()
{
    %fileName = paperDoll_getBaseFilepath() @ "permutations.json";
    %file = new FileObject();
    if (!%file.openForWrite(%fileName))
    {
        error(getScopeName() SPC "- unable to open \"" @ %fileName @ "\" for write.");
        %file.delete();
        return ;
    }
    %file.indent = "";
    %file.indentString = "    ";
    %genders = "f m";
    %gendersLong["f"] = "female";
    %gendersLong["m"] = "male";
    %file.writeLineIndented("var avatarData = {");
    %file.indent();
    %file.writeLineIndented("\"permutations\":");
    %file.writeLineIndented("[");
    %file.indent();
    %n = 0;
    while (%n < getWordCount(%genders))
    {
        %gender = getWord(%genders, %n);
        %genderLong = %gendersLong[%gender];
        %file.writeLineIndented("{");
        %file.indent();
        %file.writeLineIndented("\"Gender\": \"" @ %gender @ "\",");
        %file.writeLineIndented("\"Parameters\":");
        %file.writeLineIndented("[");
        %file.indent();
        %paramNum = 0;
        while (%paramNum < paperDoll_getParamsNum(%gender))
        {
            %paramName = paperDoll_getParamName(%gender, %paramNum);
            %file.writeLineIndented("{");
            %file.indent();
            %file.writeLineIndented("\"Parameter\": \"" @ %paramName @ "\",");
            %file.writeLineIndented("\"Values\":");
            %file.writeLineIndented("[");
            %file.indent();
            %valueNum = 0;
            while (%valueNum < paperDoll_getParamValuesNum(%gender, %paramNum))
            {
                %valuename = paperDoll_getParamValueName(%gender, %paramNum, %valueNum);
                %valueSkus = paperDoll_getParamValueSkus(%gender, %paramNum, %valueNum);
                %file.writeLineIndented("{");
                %file.indent();
                %file.writeLineIndented("\"Value\": \"" @ %valuename @ "\",");
                %file.writeLineIndented("\"skus\":");
                %file.writeLineIndented("[");
                %file.indent();
                %skunum = 0;
                while (%skunum < getWordCount(%valueSkus))
                {
                    %sku = getWord(%valueSkus, %skunum);
                    if ((%skunum + 1) == getWordCount(%valueSkus))
                    {
                        %file.writeLineIndented("\"" @ %sku @ "\"");
                    }
                    else
                    {
                        %file.writeLineIndented("\"" @ %sku @ "\",");
                    }
                    %skunum = %skunum + 1;
                }
                %file.unindent();
                %file.writeLineIndented("]");
                %file.unindent();
                if ((%valueNum + 1) == paperDoll_getParamValuesNum(%gender, %paramNum))
                {
                    %file.writeLineIndented("}");
                }
                else
                {
                    %file.writeLineIndented("},");
                }
                %valueNum = %valueNum + 1;
            }
            %file.unindent();
            %file.writeLineIndented("]");
            %file.unindent();
            if ((%paramNum + 1) == paperDoll_getParamsNum(%gender))
            {
                %file.writeLineIndented("}");
            }
            else
            {
                %file.writeLineIndented("},");
            }
            %paramNum = %paramNum + 1;
        }
        %file.unindent();
        %file.writeLineIndented("]");
        %file.unindent();
        if ((%n + 1) == getWordCount(%genders))
        {
            %file.writeLineIndented("}");
        }
        else
        {
            %file.writeLineIndented("},");
        }
        %n = %n + 1;
    }
    %file.unindent();
    %file.writeLineIndented("]");
    %file.unindent();
    %file.writeLineIndented("}");
    %file.close();
    %file.delete();
    return ;
}
function paperDoll_generateManifest()
{
    %fileName = paperDoll_getBaseFilepath() @ "permutations_manifest.txt";
    %file = new FileObject();
    if (!%file.openForWrite(%fileName))
    {
        error(getScopeName() SPC "- unable to open \"" @ %fileName @ "\" for write.");
        %file.delete();
        return ;
    }
    %genders = "f m";
    %file.writeOpenTag("permutations", "");
    %file.writeCommentTag("permutations manifest");
    %file.writeCommentTag("total number of permutations =" SPC paperDoll_getNumPermutations("f") + paperDoll_getNumPermutations("m"));
    %file.writeLineIndented("");
    %n = 0;
    while (%n < getWordCount(%genders))
    {
        %gender = getWord(%genders, %n);
        %file.writeLineIndented("");
        %file.writeOpenTag("gender", "");
        %file.writeCommentTag("number of permutations =" SPC paperDoll_getNumPermutations(%gender));
        paperDoll_generateManifest_Recurse(%file, %gender, 0, "");
        %file.writeCloseTag("gender");
        %n = %n + 1;
    }
    %file.writeCloseTag("permutations");
    %file.close();
    %file.delete();
    return ;
}
function paperDoll_generateManifest_Recurse(%file, %gender, %initialDepth, %valueIndicesList)
{
    %valueNum = 0;
    while (%valueNum < paperDoll_getParamValuesNum(%gender, %initialDepth))
    {
        %valList = %valueIndicesList @ %valueNum @ " ";
        if (%initialDepth >= (paperDoll_getParamsNum(%gender) - 1))
        {
            %s = paperDoll_getPermutationFilenameAndSkus(%gender, %valList);
            %imgFilename = getField(%s, 0);
            %imgFilename = %imgFilename @ "." @ $gPaperDoll_ImgExtension;
            %skus = getField(%s, 1);
            %skusEntire = SkuManager.overlaySkus($gPaperDoll_BaseSkus[%gender], %skus);
            %file.writeLineIndented("");
            %file.writeOpenTag("permutation", "");
            %file.writeShortTag("filename", "", %imgFilename);
            %file.writeShortTag("skus", "", %skusEntire);
            %file.writeCloseTag("permutation", "");
        }
        else
        {
            paperDoll_generateManifest_Recurse(%file, %gender, %initialDepth + 1, %valList);
        }
        %valueNum = %valueNum + 1;
    }
}


