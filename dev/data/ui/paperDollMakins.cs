$gPaperDoll_DryRun = 0;
$gPaperDoll_NumRemaining = 0;
$gPaperDoll_ImgExtension = "png";
$gPaperDoll_Nudge = "0 0";
$gPaperDoll_NudgeStep = "0.025 0.025";
function gePaperDollMakins::open(%this) {
    $UserPref::Video::ConstrainWindowDimensions = 0;
    0.pushDialog(Canvas, %this);
    setScreenMode(1048, 1048, getWord($UserPref::Video::Resolution, 2), 0);
    if (!(gePaperDollWhichSetup_Client.getValue())) {
    }
    if (!(gePaperDollWhichSetup_Web.getValue())) {
        gePaperDollWhichSetup_Client.performClick();
    }
    %this.paperDoll_refresh();
    if (isObject($player)) {
        $player.setSimObject(gePaperDollObjectView);
    }
    "<color:ffffff>" @ "\n" @ "Welcome to the paper doll making interface." @ "\n" @ "This runs on two different folders." @ "\n" @ "for the client:<spush><b>platform/client/ui/paperdolls/<spop>" @ "\n" @ "for the web:<spush><b>web/paperdolls/<spop>." @ "\n" @ "in each of those, <spush><b>permutations.txt<spop> sets up everything." @ "\n" @ "When you click \"refresh\", <spush><b>permutations.xml<spop> and <spush><b>permutations_manifest.txt<spop> are generated." @ "\n" @ "" @ "\n" @ "The client and the web can have different setup files. eg, you can have [many] more options on the web, if you want." @ "\n" @ "" @ "\n" @ "The size of the avatar area is set in permutations.txt." @ "\n" @ "Since the alpha channel is not anti-aliased, i recommend setting the avatar area to twice the actual desired image size, and then using photoshop or similar to batch-process the images down to size." @ "\n" @ "" @ "\n" @ "Also, surfaces which have alpha (such as glasses or some hair) will be saved transparent in those regions, which looks weird. To fix this, again use batch processing in photoshop to simply duplicate the layer of the image several times, building up the opacity." @ "\n" @ "" @ "\n" @ "During a real run, the images are saved out to <spush><b>images/source<spop>." @ "\n" @ "<spush><color:77FF44><b>For the client, these must be copied into just plain \"images/\"!<spop>" @ "\n" @ "For the web, they may need to be copied elsewhere as well; that process hasn't been worked out yet." @ "\n" @ "" @ "\n" @ "Don't check in the images in \"source/\", only the ones from \"images\"." @ "\n" @ "" @ "\n" @ "During a real or dry run, HTML files are also generated previewing all the images." @ "\n" @ "" @ "\n" @ "<spush><color:77FF44><b>To get alpha, the -alphaBuffer option must be used on the command line.<spop>".setText(gePaperDollInfo_Long);
};
function gePaperDollMakins::close(%this) {
    %this.popDialog(Canvas);
};
function gePaperDollMakins::paperDoll_refresh(%this) {
    $gPaperDoll_SetupFile = paperDoll_getBaseFilepath() @ "permutations.txt";
    paperDoll_InitPermutationsForce();
    %numf = paperDoll_getNumPermutations("f");
    %numm = paperDoll_getNumPermutations("m");
    if (!(isObject($player))) {
        %genderText = "(none)";
        %gender = "X";
    }
    %genderText = ($player.getGender() $= "f") ? "female" : "male";
    %gender = $player.getGender();
    %text = "";
    %text = %text @ "num F =" @ " " @ %numf;
    %text = %text @ "\n" @ "num M =" @ " " @ %numm;
    %text = %text @ "\n" @ "currently:" @ " " @ %genderText;
    %text.setText(gePaperDollInfo);
    getWord($gPaperDollImgSize, 1).resize(gePaperDollEraser, getWord($gPaperDollImgSize, 0));
    gePaperDollEraser.eraserColor = $gPaperDollBackground;
    paperDoll_generateXML();
    paperDoll_generateJSON();
    paperDoll_generateManifest();
    $gPaperDoll_SkuArray = new_ScriptArray("");
    paperDoll_RecursePermutations(%gender, "", %gender, 0, $gPaperDoll_SkuArray);
    $gPaperDoll_SkuArray.dumpValues();
};
function paperDoll_StartTakingSnaps() {
    $gPaperDoll_ObjViewCtrl = gePaperDollObjectView;
    $gPaperDoll_CurIndex = 0;
    $gPaperDoll_CancelRun = 0;
    $gPaperDoll_PreviewFile = "";
    if (1) {
        $gPaperDoll_PreviewFile = new FileObject("");
        %fileName = paperDoll_getBaseFilepath();
        %fileName = %fileName @ "index_" @ $player.getGender() @ ".html";
        %fileName.openForWrite($gPaperDoll_PreviewFile);
        "<html>\n<body background=\"greychecks.png\">".writeLine($gPaperDoll_PreviewFile);
    }
    0.setVisible(gePaperDollDryRun);
    0.setVisible(gePaperDollRealRun);
    1.setVisible(gePaperDollCancel);
    1.setVisible(gePaperDollInfo_Running);
    $gPaperDoll_NumRemaining = paperDoll_getNumPermutations($player.getGender());
    paperDoll_prepareNextSnapshot();
};
function paperDoll_finishedSnapshots() {
    if (isObject($gPaperDoll_PreviewFile)) {
        "</body>\n</html>".writeLine($gPaperDoll_PreviewFile);
        $gPaperDoll_PreviewFile.close();
        $gPaperDoll_PreviewFile.delete();
        $gPaperDoll_PreviewFile = "";
    }
    1.setVisible(gePaperDollDryRun);
    1.setVisible(gePaperDollRealRun);
    0.setVisible(gePaperDollCancel);
    0.setVisible(gePaperDollInfo_Running);
};
function paperDoll_prepareNextSnapshot() {
    paperDoll_prepareOneSnapshot($gPaperDoll_CurIndex);
    waitAFrameAndCall("paperDoll_callingTakeCurrentSnapshot");
};
function paperDoll_prepareOneSnapshot(%index) {
    if ((%index < 0.0)) {
    }
    if ((%index >= $gPaperDoll_SkuArray.size())) {
        return;
    }
    $gPaperDoll_CurSkus = getField(%index.get($gPaperDoll_SkuArray), 0);
    $gPaperDoll_CurName = getField(%index.get($gPaperDoll_SkuArray), 1);
    %skus = $gPaperDoll_CurSkus.overlaySkus(SkuManager, );
    %skus.setSkus($gPaperDoll_ObjViewCtrl);
    %index.setValue(gePaperDollCurOutfitField);
    %tmp = gePaperDollCurOutfitSlider.altCommand;
    gePaperDollCurOutfitSlider.altCommand = "";
    (1.0 - (%index / ($gPaperDoll_SkuArray.size() - 1.0))).setValue(gePaperDollCurOutfitSlider);
    gePaperDollCurOutfitSlider.altCommand = %tmp;
    $gPaperDoll_CurIndex = %index;
};
function paperDoll_Permute_Cancel() {
    $gPaperDoll_CancelRun = 1;
};
function paperDoll_callingTakeCurrentSnapshot() {
    paperDoll_takeCurrentSnapshot();
    if (!($gPaperDoll_CancelRun)) {
    }
    if (($gPaperDoll_CurIndex < ($gPaperDoll_SkuArray.size() - 1.0))) {
        $gPaperDoll_CurIndex = ($gPaperDoll_CurIndex + 1.0);
        paperDoll_prepareNextSnapshot();
    }
    paperDoll_finishedSnapshots();
};
function paperDoll_getBaseFilepath() {
    if (gePaperDollWhichSetup_Client.getValue()) {
        %ret = "platform/client/ui/paperdolls/";
    }
    %ret = "web/paperdolls/";
    return %ret;
};
function paperDoll_takeCurrentSnapshot() {
    %justFileName = $gPaperDoll_CurName;
    %justFileName = %justFileName @ "." @ $gPaperDoll_ImgExtension;
    %fileName = paperDoll_getBaseFilepath() @ "images/source/" @ %justFileName;
    if (!($gPaperDoll_DryRun)) {
        %fileName.snapshot($gPaperDoll_ObjViewCtrl);
    }
    if (isObject($gPaperDoll_PreviewFile)) {
        "<img src=\"images/" @ %justFileName @ "\">".writeLine($gPaperDoll_PreviewFile);
    }
    $gPaperDoll_NumRemaining = ($gPaperDoll_NumRemaining - 1.0);
    "Remaining:" @ " " @ $gPaperDoll_NumRemaining.setText(gePaperDollInfo_Running);
};
function paperDoll_MakePermutations(%gender) {
    paperDoll_StartTakingSnaps();
};
function paperDoll_RecursePermutations(%gender, %currentSkus, %currentNames, %startingDepth, %array) {
    %masterList = %gender[$gPaperDollPermutationLists @ %gender];
    %masterListSize = %masterList.size();
    if ((%startingDepth >= %masterListSize)) {
        %currentSkus @ "\t" @ %currentNames.append(%array);
        return;
    }
    %subList = %startingDepth.get(%masterList);
    %subListSize = %subList.size();
    %n = 0;
    while ((%n < %subListSize)) {
        %skus = %currentSkus @ getField(%n.get(%subList), 0) @ " ";
        %names = %currentNames @ "_" @ getField(%n.get(%subList), 1);
        paperDoll_RecursePermutations(%gender, %skus, %names, (%startingDepth + 1.0), %array);
        %n = (%n + 1.0);
    }
};
function paperDoll_PermuteWithDialog() {
    userTips::showOnceThisSession("PaperDollPermute");
};
function paperDoll_Permute() {
    schedule(1000, 0, "paperDoll_Permute_Really");
};
function paperDoll_Permute_Really() {
    $gPaperDoll_DryRun = 0;
    paperDoll_InitPermutationsForce();
    paperDoll_MakePermutations($player.getGender());
};
function paperDoll_Permute_DryRun() {
    $gPaperDoll_DryRun = 1;
    paperDoll_InitPermutationsForce();
    paperDoll_MakePermutations($player.getGender());
};
function paperDoll_Nudge(%vec) {
    %vec = VectorScale(%vec, $gPaperDoll_NudgeStep);
    $gPaperDoll_Nudge = VectorAdd($gPaperDoll_Nudge, %vec);
    getWord($gPaperDoll_Nudge, 0) @ " " @ 0 @ " " @ getWord($gPaperDoll_Nudge, 1).setLookAtNudge(gePaperDollObjectView);
    getWords($gPaperDoll_Nudge, 0, 1).setValue(gePaperDollNudgeField);
};
function paperDoll_NudgeSet(%vec) {
    $gPaperDoll_Nudge = %vec;
    paperDoll_Nudge("0 0");
};
function paperDoll_CurOutfitSet(%val) {
    %firstChar = getSubStr(%val, 0, 1);
    if ((%firstChar $= "-")) {
        %newVal = ($gPaperDoll_CurIndex + %val);
    }
    if ((%firstChar $= "+")) {
        %newVal = ($gPaperDoll_CurIndex + getSubStr(%val, 1, 100));
    }
    %newVal = %val;
    paperDoll_prepareOneSnapshot(%newVal);
};
function gePaperDollCurOutfitSlider::valueChanged(%this) {
    %val = mFloor((($gPaperDoll_SkuArray.size() - 1.0) * (1.0 - %this.getValue())));
    paperDoll_prepareOneSnapshot(%val);
};
function paperDoll_generateXML() {
    %fileName = paperDoll_getBaseFilepath() @ "permutations.xml";
    %file = new FileObject("");
    if (!(%fileName.openForWrite(%file))) {
        error(getScopeName() @ " " @ "- unable to open \"" @ %fileName @ "\" for write.");
        %file.delete();
        return;
    }
    %file.indent = "";
    %file.indentString = "    ";
    "<?xml version=\"1.0\" encoding=\"UTF-8\"?>".writeLineIndented(%file);
    "".writeLineIndented(%file);
    "<!--".writeLineIndented(%file);
    %file.indent();
    "Document   : permutations.xml".writeLineIndented(%file);
    "Created on : " @ getTimeStamp().writeLineIndented(%file);
    "Author     : envClient / orion".writeLineIndented(%file);
    "Description: a description of the parameters and their possible values for each gender which describe the possible paper dolls (initial avatars) of the player." @ "\n" @ "an image must exist for every permutation, of the file name \"<gender>_<param0 value>_<param1 value>_..._<paramN value>.<image extension>\"" @ "\n" @ "see also permutations_manifest.txt for an enumeration of the files & skus generated by this set of parameters & values.".writeLineIndented(%file);
    %file.unindent();
    "-->".writeLineIndented(%file);
    %genders = "f m";
    %genders[%gendersLong @ "f"] = "female";
    %genders[%gendersLong @ "f"][%gendersLong @ "m"] = "male";
    "".writeLineIndented(%file);
    "xmlns=\"http://www.doppelganger.com/datamodel\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xsi:schemaLocation=\"http://www.doppelganger.com/datamodel schema/initial_avatar_permutations.xsd\"".writeOpenTag(%file, "Permutations");
    "Nikita: need schema description in previous ?".writeCommentTag(%file);
    %n = 0;
    while ((%n < getWordCount(%genders))) {
        %gender = getWord(%genders, %n);
        %genderLong = %gender[%gendersLong @ %gender];
        "".writeLineIndented(%file);
        "name=\"" @ %gender @ "\"".writeOpenTag(%file, "Gender");
        "Parameters for gender" @ " " @ %genderLong.writeCommentTag(%file);
        %paramNum = 0;
        while ((%paramNum < paperDoll_getParamsNum(%gender))) {
            %paramName = paperDoll_getParamName(%gender, %paramNum);
            "".writeLineIndented(%file);
            "name=\"" @ %paramName @ "\"".writeOpenTag(%file, "Param");
            "Possible values for" @ " " @ %genderLong @ " " @ "parameter" @ " " @ %paramName.writeCommentTag(%file);
            %valueNum = 0;
            while ((%valueNum < paperDoll_getParamValuesNum(%gender, %paramNum))) {
                "".writeLineIndented(%file);
                %valuename = paperDoll_getParamValueName(%gender, %paramNum, %valueNum);
                %valueSkus = paperDoll_getParamValueSkus(%gender, %paramNum, %valueNum);
                "name=\"" @ %valuename @ "\"".writeOpenTag(%file, "Value");
                %skunum = 0;
                while ((%skunum < getWordCount(%valueSkus))) {
                    %sku = getWord(%valueSkus, %skunum);
                    %sku.writeShortTag(%file, "sku", "");
                    %skunum = (%skunum + 1.0);
                }
                "Value".writeCloseTag(%file);
                %valueNum = (%valueNum + 1.0);
            }
            "Param".writeCloseTag(%file);
            %paramNum = (%paramNum + 1.0);
        }
        "Gender".writeCloseTag(%file);
        %n = (%n + 1.0);
    }
    "Permutations".writeCloseTag(%file);
    %file.close();
    %file.delete();
};
function paperDoll_generateJSON() {
    %fileName = paperDoll_getBaseFilepath() @ "permutations.json";
    %file = new FileObject("");
    if (!(%fileName.openForWrite(%file))) {
        error(getScopeName() @ " " @ "- unable to open \"" @ %fileName @ "\" for write.");
        %file.delete();
        return;
    }
    %file.indent = "";
    %file.indentString = "    ";
    %genders = "f m";
    %genders[%gendersLong @ "f"] = "female";
    %genders[%gendersLong @ "f"][%gendersLong @ "m"] = "male";
    "var avatarData = {".writeLineIndented(%file);
    %file.indent();
    "\"permutations\":".writeLineIndented(%file);
    "[".writeLineIndented(%file);
    %file.indent();
    %n = 0;
    while ((%n < getWordCount(%genders))) {
        %gender = getWord(%genders, %n);
        %genderLong = %gender[%gendersLong @ %gender];
        "{".writeLineIndented(%file);
        %file.indent();
        "\"Gender\": \"" @ %gender @ "\",".writeLineIndented(%file);
        "\"Parameters\":".writeLineIndented(%file);
        "[".writeLineIndented(%file);
        %file.indent();
        %paramNum = 0;
        while ((%paramNum < paperDoll_getParamsNum(%gender))) {
            %paramName = paperDoll_getParamName(%gender, %paramNum);
            "{".writeLineIndented(%file);
            %file.indent();
            "\"Parameter\": \"" @ %paramName @ "\",".writeLineIndented(%file);
            "\"Values\":".writeLineIndented(%file);
            "[".writeLineIndented(%file);
            %file.indent();
            %valueNum = 0;
            while ((%valueNum < paperDoll_getParamValuesNum(%gender, %paramNum))) {
                %valuename = paperDoll_getParamValueName(%gender, %paramNum, %valueNum);
                %valueSkus = paperDoll_getParamValueSkus(%gender, %paramNum, %valueNum);
                "{".writeLineIndented(%file);
                %file.indent();
                "\"Value\": \"" @ %valuename @ "\",".writeLineIndented(%file);
                "\"skus\":".writeLineIndented(%file);
                "[".writeLineIndented(%file);
                %file.indent();
                %skunum = 0;
                while ((%skunum < getWordCount(%valueSkus))) {
                    %sku = getWord(%valueSkus, %skunum);
                    if (((%skunum + 1.0) == getWordCount(%valueSkus))) {
                        "\"" @ %sku @ "\"".writeLineIndented(%file);
                    }
                    "\"" @ %sku @ "\",".writeLineIndented(%file);
                    %skunum = (%skunum + 1.0);
                }
                %file.unindent();
                "]".writeLineIndented(%file);
                %file.unindent();
                if (((%valueNum + 1.0) == paperDoll_getParamValuesNum(%gender, %paramNum))) {
                    "}".writeLineIndented(%file);
                }
                "},".writeLineIndented(%file);
                %valueNum = (%valueNum + 1.0);
            }
            %file.unindent();
            "]".writeLineIndented(%file);
            %file.unindent();
            if (((%paramNum + 1.0) == paperDoll_getParamsNum(%gender))) {
                "}".writeLineIndented(%file);
            }
            "},".writeLineIndented(%file);
            %paramNum = (%paramNum + 1.0);
        }
        %file.unindent();
        "]".writeLineIndented(%file);
        %file.unindent();
        if (((%n + 1.0) == getWordCount(%genders))) {
            "}".writeLineIndented(%file);
        }
        "},".writeLineIndented(%file);
        %n = (%n + 1.0);
    }
    %file.unindent();
    "]".writeLineIndented(%file);
    %file.unindent();
    "}".writeLineIndented(%file);
    %file.close();
    %file.delete();
};
function paperDoll_generateManifest() {
    %fileName = paperDoll_getBaseFilepath() @ "permutations_manifest.txt";
    %file = new FileObject("");
    if (!(%fileName.openForWrite(%file))) {
        error(getScopeName() @ " " @ "- unable to open \"" @ %fileName @ "\" for write.");
        %file.delete();
        return;
    }
    %genders = "f m";
    "".writeOpenTag(%file, "permutations");
    "permutations manifest".writeCommentTag(%file);
    "total number of permutations =" @ " " @ (paperDoll_getNumPermutations("f") + paperDoll_getNumPermutations("m")).writeCommentTag(%file);
    "".writeLineIndented(%file);
    %n = 0;
    while ((%n < getWordCount(%genders))) {
        %gender = getWord(%genders, %n);
        "".writeLineIndented(%file);
        "".writeOpenTag(%file, "gender");
        "number of permutations =" @ " " @ paperDoll_getNumPermutations(%gender).writeCommentTag(%file);
        paperDoll_generateManifest_Recurse(%file, %gender, 0, "");
        "gender".writeCloseTag(%file);
        %n = (%n + 1.0);
    }
    "permutations".writeCloseTag(%file);
    %file.close();
    %file.delete();
};
function paperDoll_generateManifest_Recurse(%file, %gender, %initialDepth, %valueIndicesList) {
    %valueNum = 0;
    while ((%valueNum < paperDoll_getParamValuesNum(%gender, %initialDepth))) {
        %valList = %valueIndicesList @ %valueNum @ " ";
        if ((%initialDepth >= (paperDoll_getParamsNum(%gender) - 1.0))) {
            %s = paperDoll_getPermutationFilenameAndSkus(%gender, %valList);
            %imgFilename = getField(%s, 0);
            %imgFilename = %imgFilename @ "." @ $gPaperDoll_ImgExtension;
            %skus = getField(%s, 1);
            %skusEntire = %skus.overlaySkus(SkuManager, %gender[$gPaperDoll_BaseSkus @ %gender]);
            "".writeLineIndented(%file);
            "".writeOpenTag(%file, "permutation");
            %imgFilename.writeShortTag(%file, "filename", "");
            %skusEntire.writeShortTag(%file, "skus", "");
            "".writeCloseTag(%file, "permutation");
        }
        paperDoll_generateManifest_Recurse(%file, %gender, (%initialDepth + 1.0), %valList);
        %valueNum = (%valueNum + 1.0);
    }
};
