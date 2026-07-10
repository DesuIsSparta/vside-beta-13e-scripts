$gPaperDoll_DryRun = 0;
$gPaperDoll_NumRemaining = 0;
$gPaperDoll_ImgExtension = "png";
$gPaperDoll_Nudge = "0 0";
$gPaperDoll_NudgeStep = "0.025 0.025";
function gePaperDollMakins::open(%this) {
    $UserPref::Video::ConstrainWindowDimensions = 0;
    %this.pushDialog(0);
    setScreenMode(1048, 1048, getWord($UserPref::Video::Resolution, 2), 0);
    if (!(gePaperDollWhichSetup_Client.getValue())) {
    }
    if (!(gePaperDollWhichSetup_Web.getValue())) {
        gePaperDollWhichSetup_Client.performClick();
    }
    %this.paperDoll_refresh();
    if (isObject($player)) {
        $player.setSimObject();
    }
    "<color:ffffff>" @ "\n" @ "Welcome to the paper doll making interface." @ "\n" @ "This runs on two different folders." @ "\n" @ "for the client:<spush><b>platform/client/ui/paperdolls/<spop>" @ "\n" @ "for the web:<spush><b>web/paperdolls/<spop>." @ "\n" @ "in each of those, <spush><b>permutations.txt<spop> sets up everything." @ "\n" @ "When you click \"refresh\", <spush><b>permutations.xml<spop> and <spush><b>permutations_manifest.txt<spop> are generated." @ "\n" @ "" @ "\n" @ "The client and the web can have different setup files. eg, you can have [many] more options on the web, if you want." @ "\n" @ "" @ "\n" @ "The size of the avatar area is set in permutations.txt." @ "\n" @ "Since the alpha channel is not anti-aliased, i recommend setting the avatar area to twice the actual desired image size, and then using photoshop or similar to batch-process the images down to size." @ "\n" @ "" @ "\n" @ "Also, surfaces which have alpha (such as glasses or some hair) will be saved transparent in those regions, which looks weird. To fix this, again use batch processing in photoshop to simply duplicate the layer of the image several times, building up the opacity." @ "\n" @ "" @ "\n" @ "During a real run, the images are saved out to <spush><b>images/source<spop>." @ "\n" @ "<spush><color:77FF44><b>For the client, these must be copied into just plain \"images/\"!<spop>" @ "\n" @ "For the web, they may need to be copied elsewhere as well; that process hasn't been worked out yet." @ "\n" @ "" @ "\n" @ "Don't check in the images in \"source/\", only the ones from \"images\"." @ "\n" @ "" @ "\n" @ "During a real or dry run, HTML files are also generated previewing all the images." @ "\n" @ "" @ "\n" @ "<spush><color:77FF44><b>To get alpha, the -alphaBuffer option must be used on the command line.<spop>".setText();
};
function gePaperDollMakins::close(%this) {
    %this.popDialog();
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
    %text.setText();
    getWord($gPaperDollImgSize, 0).resize(getWord($gPaperDollImgSize, 1));
    eraserColor = $gPaperDollBackground @ gePaperDollEraser;
    gePaperDollEraser;
    paperDoll_generateXML();
    paperDoll_generateJSON();
    paperDoll_generateManifest();
    $gPaperDoll_SkuArray = new_ScriptArray("");
    gePaperDollInfo;
    paperDoll_RecursePermutations(%gender, "", %gender, 0, $gPaperDoll_SkuArray);
    $gPaperDoll_SkuArray.dumpValues();
};
function paperDoll_StartTakingSnaps() {
    // unhandled opcode 892 at 0x00000268
    $gPaperDoll_CurIndex = 0;
    $gPaperDoll_CancelRun = 0;
    $gPaperDoll_PreviewFile = "";
    if (1) {
        $gPaperDoll_PreviewFile = new ""();;
        FileObject;
        %fileName = paperDoll_getBaseFilepath();
        0;
        %fileName = %fileName @ "index_" @ $player.getGender() @ ".html";
        $gPaperDoll_PreviewFile.openForWrite(%fileName);
        $gPaperDoll_PreviewFile.writeLine("<html>\n<body background=\"greychecks.png\">");
    }
    0.setVisible();
    0.setVisible();
    1.setVisible();
    1.setVisible();
    $gPaperDoll_NumRemaining = paperDoll_getNumPermutations($player.getGender());
    gePaperDollInfo_Running;
    paperDoll_prepareNextSnapshot();
};
function paperDoll_finishedSnapshots() {
    if (isObject($gPaperDoll_PreviewFile)) {
        $gPaperDoll_PreviewFile.writeLine("</body>\n</html>");
        $gPaperDoll_PreviewFile.close();
        $gPaperDoll_PreviewFile.delete();
        $gPaperDoll_PreviewFile = "";
    }
    1.setVisible();
    1.setVisible();
    0.setVisible();
    0.setVisible();
};
function paperDoll_prepareNextSnapshot() {
    paperDoll_prepareOneSnapshot($gPaperDoll_CurIndex);
    waitAFrameAndCall("paperDoll_callingTakeCurrentSnapshot");
};
function paperDoll_prepareOneSnapshot(%index) {
    if ((0.0 < %index)) {
    }
    if (($gPaperDoll_SkuArray.size() >= %index)) {
        return;
    }
    $gPaperDoll_CurSkus = getField($gPaperDoll_SkuArray.get(%index), 0);
    $gPaperDoll_CurName = getField($gPaperDoll_SkuArray.get(%index), 1);
    %skus = SkuManager.overlaySkus($gPaperDoll_CurSkus);
    $gPaperDoll_ObjViewCtrl.setSkus(%skus);
    %index.setValue();
    %tmp = altCommand;
    gePaperDollCurOutfitSlider;
    altCommand = "" @ gePaperDollCurOutfitSlider;
    gePaperDollCurOutfitField;
    (((1.0 - $gPaperDoll_SkuArray.size()) / %index) - 1.0).setValue();
    altCommand = %tmp @ gePaperDollCurOutfitSlider;
    gePaperDollCurOutfitSlider;
    $gPaperDoll_CurIndex = %index;
};
function paperDoll_Permute_Cancel() {
    $gPaperDoll_CancelRun = 1;
};
function paperDoll_callingTakeCurrentSnapshot() {
    paperDoll_takeCurrentSnapshot();
    if (!($gPaperDoll_CancelRun)) {
    }
    if (((1.0 - $gPaperDoll_SkuArray.size()) < $gPaperDoll_CurIndex)) {
        $gPaperDoll_CurIndex = (1.0 + $gPaperDoll_CurIndex);
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
        $gPaperDoll_ObjViewCtrl.snapshot(%fileName);
    }
    if (isObject($gPaperDoll_PreviewFile)) {
        $gPaperDoll_PreviewFile.writeLine("<img src=\"images/" @ %justFileName @ "\">");
    }
    $gPaperDoll_NumRemaining = (1.0 - $gPaperDoll_NumRemaining);
    "Remaining:" @ " " @ $gPaperDoll_NumRemaining.setText();
};
function paperDoll_MakePermutations(%gender) {
    paperDoll_StartTakingSnaps();
};
function paperDoll_RecursePermutations(%gender, %currentSkus, %currentNames, %startingDepth, %array) {
    %masterList = %gender[$gPaperDollPermutationLists @ %gender];
    %masterListSize = %masterList.size();
    if ((%masterListSize >= %startingDepth)) {
        %array.append(%currentSkus @ "\t" @ %currentNames);
        return;
    }
    %subList = %masterList.get(%startingDepth);
    %subListSize = %subList.size();
    %n = 0;
    if ((%subListSize < %n)) {
        %skus = %currentSkus @ getField(%subList.get(%n), 0) @ " ";
        %names = %currentNames @ "_" @ getField(%subList.get(%n), 1);
        paperDoll_RecursePermutations(%gender, %skus, %names, (1.0 + %startingDepth), %array);
        %n = (1.0 + %n);
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
    getWord($gPaperDoll_Nudge, 0) @ " " @ 0 @ " " @ getWord($gPaperDoll_Nudge, 1).setLookAtNudge();
    getWords($gPaperDoll_Nudge, 0, 1).setValue();
};
function paperDoll_NudgeSet(%vec) {
    $gPaperDoll_Nudge = %vec;
    paperDoll_Nudge("0 0");
};
function paperDoll_CurOutfitSet(%val) {
    %firstChar = getSubStr(%val, 0, 1);
    if ((%firstChar $= "-")) {
        %newVal = (%val + $gPaperDoll_CurIndex);
    }
    if ((%firstChar $= "+")) {
        %newVal = (getSubStr(%val, 1, 100) + $gPaperDoll_CurIndex);
    }
    %newVal = %val;
    paperDoll_prepareOneSnapshot(%newVal);
};
function gePaperDollCurOutfitSlider::valueChanged(%this) {
    %val = mFloor(((%this.getValue() - 1.0) * (1.0 - $gPaperDoll_SkuArray.size())));
    paperDoll_prepareOneSnapshot(%val);
};
function paperDoll_generateXML() {
    %fileName = paperDoll_getBaseFilepath() @ "permutations.xml";
    %file = new ""();;
    FileObject;
    if (!(%file.openForWrite(%fileName))) {
        error(getScopeName() @ " " @ "- unable to open \"" @ %fileName @ "\" for write.");
        %file.delete();
        return 0;
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
    %file.writeLineIndented("Description: a description of the parameters and their possible values for each gender which describe the possible paper dolls (initial avatars) of the player." @ "\n" @ "an image must exist for every permutation, of the file name \"<gender>_<param0 value>_<param1 value>_..._<paramN value>.<image extension>\"" @ "\n" @ "see also permutations_manifest.txt for an enumeration of the files & skus generated by this set of parameters & values.");
    %file.unindent();
    %file.writeLineIndented("-->");
    %genders = "f m";
    %genders[%gendersLong @ "f"] = "female";
    %genders[%gendersLong @ "f"][%gendersLong @ "m"] = "male";
    %file.writeLineIndented("");
    %file.writeOpenTag("Permutations", "xmlns=\"http://www.doppelganger.com/datamodel\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" xsi:schemaLocation=\"http://www.doppelganger.com/datamodel schema/initial_avatar_permutations.xsd\"");
    %file.writeCommentTag("Nikita: need schema description in previous ?");
    %n = 0;
    if ((getWordCount(%genders) < %n)) {
        %gender = getWord(%genders, %n);
        %genderLong = %gender[%gendersLong @ %gender];
        %file.writeLineIndented("");
        %file.writeOpenTag("Gender", "name=\"" @ %gender @ "\"");
        %file.writeCommentTag("Parameters for gender" @ " " @ %genderLong);
        %paramNum = 0;
        if ((paperDoll_getParamsNum(%gender) < %paramNum)) {
            %paramName = paperDoll_getParamName(%gender, %paramNum);
            %file.writeLineIndented("");
            %file.writeOpenTag("Param", "name=\"" @ %paramName @ "\"");
            %file.writeCommentTag("Possible values for" @ " " @ %genderLong @ " " @ "parameter" @ " " @ %paramName);
            %valueNum = 0;
            if ((paperDoll_getParamValuesNum(%gender, %paramNum) < %valueNum)) {
                %file.writeLineIndented("");
                %valuename = paperDoll_getParamValueName(%gender, %paramNum, %valueNum);
                %valueSkus = paperDoll_getParamValueSkus(%gender, %paramNum, %valueNum);
                %file.writeOpenTag("Value", "name=\"" @ %valuename @ "\"");
                %skunum = 0;
                if ((getWordCount(%valueSkus) < %skunum)) {
                    %sku = getWord(%valueSkus, %skunum);
                    %file.writeShortTag("sku", "", %sku);
                    %skunum = (1.0 + %skunum);
                }
                %file.writeCloseTag("Value");
                %valueNum = (1.0 + %valueNum);
                (getWordCount(%valueSkus) < %skunum);
            }
            %file.writeCloseTag("Param");
            %paramNum = (1.0 + %paramNum);
            (paperDoll_getParamValuesNum(%gender, %paramNum) < %valueNum);
        }
        %file.writeCloseTag("Gender");
        %n = (1.0 + %n);
        (paperDoll_getParamsNum(%gender) < %paramNum);
    }
    %file.writeCloseTag("Permutations");
    %file.close();
    %file.delete();
};
function paperDoll_generateJSON() {
    %fileName = paperDoll_getBaseFilepath() @ "permutations.json";
    %file = new ""();;
    FileObject;
    if (!(%file.openForWrite(%fileName))) {
        error(getScopeName() @ " " @ "- unable to open \"" @ %fileName @ "\" for write.");
        %file.delete();
        return 0;
    }
    %file.indent = "";
    %file.indentString = "    ";
    %genders = "f m";
    %genders[%gendersLong @ "f"] = "female";
    %genders[%gendersLong @ "f"][%gendersLong @ "m"] = "male";
    %file.writeLineIndented("var avatarData = {");
    %file.indent();
    %file.writeLineIndented("\"permutations\":");
    %file.writeLineIndented("[");
    %file.indent();
    %n = 0;
    if ((getWordCount(%genders) < %n)) {
        %gender = getWord(%genders, %n);
        %genderLong = %gender[%gendersLong @ %gender];
        %file.writeLineIndented("{");
        %file.indent();
        %file.writeLineIndented("\"Gender\": \"" @ %gender @ "\",");
        %file.writeLineIndented("\"Parameters\":");
        %file.writeLineIndented("[");
        %file.indent();
        %paramNum = 0;
        if ((paperDoll_getParamsNum(%gender) < %paramNum)) {
            %paramName = paperDoll_getParamName(%gender, %paramNum);
            %file.writeLineIndented("{");
            %file.indent();
            %file.writeLineIndented("\"Parameter\": \"" @ %paramName @ "\",");
            %file.writeLineIndented("\"Values\":");
            %file.writeLineIndented("[");
            %file.indent();
            %valueNum = 0;
            if ((paperDoll_getParamValuesNum(%gender, %paramNum) < %valueNum)) {
                %valuename = paperDoll_getParamValueName(%gender, %paramNum, %valueNum);
                %valueSkus = paperDoll_getParamValueSkus(%gender, %paramNum, %valueNum);
                %file.writeLineIndented("{");
                %file.indent();
                %file.writeLineIndented("\"Value\": \"" @ %valuename @ "\",");
                %file.writeLineIndented("\"skus\":");
                %file.writeLineIndented("[");
                %file.indent();
                %skunum = 0;
                if ((getWordCount(%valueSkus) < %skunum)) {
                    %sku = getWord(%valueSkus, %skunum);
                    if ((getWordCount(%valueSkus) == (1.0 + %skunum))) {
                        %file.writeLineIndented("\"" @ %sku @ "\"");
                    }
                    %file.writeLineIndented("\"" @ %sku @ "\",");
                    %skunum = (1.0 + %skunum);
                }
                %file.unindent();
                %file.writeLineIndented("]");
                %file.unindent();
                if ((paperDoll_getParamValuesNum(%gender, %paramNum) == (1.0 + %valueNum))) {
                    %file.writeLineIndented("}");
                }
                %file.writeLineIndented("},");
                %valueNum = (1.0 + %valueNum);
                (getWordCount(%valueSkus) < %skunum);
            }
            %file.unindent();
            %file.writeLineIndented("]");
            %file.unindent();
            if ((paperDoll_getParamsNum(%gender) == (1.0 + %paramNum))) {
                %file.writeLineIndented("}");
            }
            %file.writeLineIndented("},");
            %paramNum = (1.0 + %paramNum);
            (paperDoll_getParamValuesNum(%gender, %paramNum) < %valueNum);
        }
        %file.unindent();
        %file.writeLineIndented("]");
        %file.unindent();
        if ((getWordCount(%genders) == (1.0 + %n))) {
            %file.writeLineIndented("}");
        }
        %file.writeLineIndented("},");
        %n = (1.0 + %n);
        (paperDoll_getParamsNum(%gender) < %paramNum);
    }
    %file.unindent();
    %file.writeLineIndented("]");
    %file.unindent();
    %file.writeLineIndented("}");
    %file.close();
    %file.delete();
};
function paperDoll_generateManifest() {
    %fileName = paperDoll_getBaseFilepath() @ "permutations_manifest.txt";
    %file = new ""();;
    FileObject;
    if (!(%file.openForWrite(%fileName))) {
        error(getScopeName() @ " " @ "- unable to open \"" @ %fileName @ "\" for write.");
        %file.delete();
        return 0;
    }
    %genders = "f m";
    %file.writeOpenTag("permutations", "");
    %file.writeCommentTag("permutations manifest");
    %file.writeCommentTag("total number of permutations =" @ " " @ (paperDoll_getNumPermutations("m") + paperDoll_getNumPermutations("f")));
    %file.writeLineIndented("");
    %n = 0;
    if ((getWordCount(%genders) < %n)) {
        %gender = getWord(%genders, %n);
        %file.writeLineIndented("");
        %file.writeOpenTag("gender", "");
        %file.writeCommentTag("number of permutations =" @ " " @ paperDoll_getNumPermutations(%gender));
        paperDoll_generateManifest_Recurse(%file, %gender, 0, "");
        %file.writeCloseTag("gender");
        %n = (1.0 + %n);
    }
    %file.writeCloseTag("permutations");
    %file.close();
    %file.delete();
};
function paperDoll_generateManifest_Recurse(%file, %gender, %initialDepth, %valueIndicesList) {
    %valueNum = 0;
    if ((paperDoll_getParamValuesNum(%gender, %initialDepth) < %valueNum)) {
        %valList = %valueIndicesList @ %valueNum @ " ";
        if (((1.0 - paperDoll_getParamsNum(%gender)) >= %initialDepth)) {
            %s = paperDoll_getPermutationFilenameAndSkus(%gender, %valList);
            %imgFilename = getField(%s, 0);
            %imgFilename = %imgFilename @ "." @ $gPaperDoll_ImgExtension;
            %skus = getField(%s, 1);
            %skusEntire = %gender[$gPaperDoll_BaseSkus @ %gender].overlaySkus(%skus);
            SkuManager;
            %file.writeLineIndented("");
            %file.writeOpenTag("permutation", "");
            %file.writeShortTag("filename", "", %imgFilename);
            %file.writeShortTag("skus", "", %skusEntire);
            %file.writeCloseTag("permutation", "");
        }
        paperDoll_generateManifest_Recurse(%file, %gender, (1.0 + %initialDepth), %valList);
        %valueNum = (1.0 + %valueNum);
    }
};
