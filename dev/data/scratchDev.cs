function fakeBuddyInfo(%friends, %faves, %fans) {
    if (($ServerName $= "")) {
        $ServerName = "Raijuku";
    }
    safeEnsureScriptObjectWithInit("StringMap", "UserListFriends", "{ ignoreCase = true; }");
    safeEnsureScriptObjectWithInit("StringMap", "UserListFavorites", "{ ignoreCase = true; }");
    safeEnsureScriptObjectWithInit("StringMap", "UserListFans", "{ ignoreCase = true; }");
    UserListFriends.deleteValuesAsObjects();
    UserListFavorites.deleteValuesAsObjects();
    UserListFans.deleteValuesAsObjects();
    %n = 0;
    while ((%n < %friends)) {
        %record = getFakeBuddyRecord("fakefriend" @ " " @ formatInt("%0.4d", (%friends - %n)));
        %record.put(UserListFriends, %record.name);
        %n = (%n + 1.0);
    }
    %n = 0;
    (%n < %friends);
    while ((%n < %faves)) {
        %record = getFakeBuddyRecord("fakeFave" @ " " @ formatInt("%0.4d", %n));
        %record.put(UserListFavorites, %record.name);
        %n = (%n + 1.0);
    }
    %n = 0;
    (%n < %faves);
    while ((%n < %fans)) {
        %record = getFakeBuddyRecord("fakeFan" @ " " @ formatInt("%0.4d", %n));
        %record.put(UserListFans, %record.name);
        %n = (%n + 1.0);
    }
};
function getFakeBuddyRecord(%name) {
    %words = "a A b B c C";
    %record = new ScriptObject("");
    %record.loggedIn = getRandom(0, 1) ? 1 : 0;
    %record.name = getRandomWord(%words) @ " " @ %name;
    %record.serverName = %record.loggedIn ? "Raijuku" : "";
    %record.roles = 0;
    %record.isIdle = getRandom(0, 1) ? 1 : 0;
    %record.isNPC = 0;
    %record.csn = "rj";
    return %record;
};
function dev_TestMLText(%onOrOff, %method) {
    %numLines = 40;
    %numCols = 20;
    %lineLong = "als djalsk jdlaksj dlakjs dl;kaj sd;lahf;ha;fkqehrk;jhqrkljhals djalsk jdlaksj dlakjs dl;kaj sd;lahf;ha;fkqehrk;jhqrkljhals djalsk jdlaksj dlakjs dl;kaj sd;lahf;ha;fkqehrk;jhqrkljhq wekljrh qkwjrh qkjwh kqjwhr kqjrwh";
    %lineLong[%lineText @ 0] = "<color:ffff33>" @ %lineLong;
    %lineLong[%lineText @ 1] = "<color:22ff33>" @ %lineLong;
    %lineLong[%lineText @ 2] = %lineLong;
    %lineText[3] = "";
    %lineText[4] = "platform/client/ui/evilbunny";
    if (isObject(geMLTest)) {
        geMLTest.delete();
    }
    new GuiMLTextCtrl(geMLTest) {
        extent = playGui.getExtent();
        profile = ETSNonModalProfile;
    };
    geMLTest.add(playGui);
    if (isObject(geMLTestArray)) {
        geMLTestArray.delete();
    }
    new GuiArray2Ctrl(geMLTestArray) {
        extent = playGui.getExtent();
        spacing = 0;
        inRows = 0;
        profile = ETSNonModalProfile;
    };
    geMLTestArray.add(playGui);
    if ((%method == 0.0)) {
        %text = "";
        if (%onOrOff) {
            %n = 0;
            while ((%n < %numLines)) {
                %text = %text @ %method[%lineText @ %method] @ "\n";
                %n = (%n + 1.0);
            }
        }
        %text.setText(geMLTest);
    }
    if ((%method == 1.0) && %onOrOff) {
        geMLTestArray.childrenExtent = (%n < %numLines) @ (getWord(playGui.getExtent(), 0) / 1.0) @ " " @ 16;
        geMLTestArray.numRowsOrCols = 1;
        geMLTestArray.childrenClassName = "GuiMLTextCtrl";
        %numLines.setNumChildren(geMLTestArray);
        %n = 0;
        while ((%n < %numLines)) {
            %child = %n.getObject(geMLTestArray);
            %child.profile = ETSNonModalProfile;
            %method[%lineText @ %method].setText(%child);
            %n = (%n + 1.0);
        }
    }
    if ((%method == 2.0) && %onOrOff) {
        geMLTestArray.childrenExtent = (%n < %numLines) @ (getWord(playGui.getExtent(), 0) / 1.0) @ " " @ 16;
        geMLTestArray.numRowsOrCols = 1;
        geMLTestArray.childrenClassName = "GuiTextCtrl";
        %numLines.setNumChildren(geMLTestArray);
        %n = 0;
        while ((%n < %numLines)) {
            %child = %n.getObject(geMLTestArray);
            %child.profile = ETSNonModalProfile;
            %method[%lineText @ %method].setText(%child);
            %n = (%n + 1.0);
        }
    }
    if ((%method == 3.0) && %onOrOff) {
        geMLTestArray.childrenExtent = (%n < %numLines) @ (getWord(playGui.getExtent(), 0) / %numCols) @ " " @ 16;
        geMLTestArray.numRowsOrCols = %numCols;
        geMLTestArray.childrenClassName = "GuiButtonCtrl";
        (%numLines * %numCols).setNumChildren(geMLTestArray);
        %n = 0;
        while ((%n < (%numLines * %numCols))) {
            %child = %n.getObject(geMLTestArray);
            %child.profile = ETSNonModalProfile;
            %method[%lineText @ %method].setText(%child);
            %n = (%n + 1.0);
        }
    }
    if ((%method == 4.0) && %onOrOff) {
        geMLTestArray.childrenExtent = (%n < (%numLines * %numCols)) @ (getWord(playGui.getExtent(), 0) / %numCols) @ " " @ 16;
        geMLTestArray.numRowsOrCols = %numCols;
        geMLTestArray.childrenClassName = "GuiBitmapCtrl";
        (%numLines * %numCols).setNumChildren(geMLTestArray);
        %n = 0;
        while ((%n < (%numLines * %numCols))) {
            %child = %n.getObject(geMLTestArray);
            %child.profile = ETSNonModalProfile;
            %method[%lineText @ %method].setBitmap(%child);
            %n = (%n + 1.0);
        }
    }
};
$gClientSideSceneObjectsTimer = "";
$gClientSideSceneObjectsTickNum = 0;
$gClientSideSceneObjectsGroup = "";
function dev_clientSideSceneObjectsTick() {
    cancel($gClientSideSceneObjectsTimer);
    if (!(isObject($gClientSideSceneObjectsGroup))) {
        $gClientSideSceneObjectsGroup = new SimGroup("");
        $gClientSideSceneObjectsGroup.add(ServerConnection);
        %a = new StaticShape("") {
            dataBlock = "db_CounterDie";
        };
        %a.add($gClientSideSceneObjectsGroup);
    }
    %windowCoord = Canvas.getCursorPos();
    %startPoint = %windowCoord.unproject(playGui);
    %camTran = playGui.getLastCameraTransform();
    %camPos = getWords(%camTran, 0, 2);
    %camPtVec = VectorSub(%startPoint, %camPos);
    %camPtVec = VectorNormalize(%camPtVec);
    %checkDistance = 200;
    %endPoint = VectorScale(%camPtVec, %checkDistance);
    %endPoint = VectorAdd(%startPoint, %endPoint);
    %possibleColiders = ((0 | $TypeMasks::InteriorObjectType) | $TypeMasks::WaterObjectType);
    %result = containerRayCast(%startPoint, %endPoint, %possibleColiders, $player, 1);
    %hitObject = getWord(%result, 0);
    if (isObject(%hitObject)) {
        %hitPosition = getWords(%result, 1, 3);
    }
    %hitPosition = VectorAdd(%startPoint, VectorScale(%camPtVec, 4));
    %t = ($gClientSideSceneObjectsTickNum * 0.1);
    %a = 0.getObject($gClientSideSceneObjectsGroup);
    MatrixMultiply(MatrixMultiply(playGui.getLastCameraTransform(), "0 2 0 1 0 0" @ " " @ %t), "0 0 0 1 0" @ " " @ ($gClientSideSceneObjectsTickNum * 0.0)).setTransform(%a);
    %hitPosition @ " " @ "0 0 1" @ " " @ %t.setTransform(%a);
    $gClientSideSceneObjectsTickNum = ($gClientSideSceneObjectsTickNum + 1.0);
    if ((($gClientSideSceneObjectsTickNum % 2) == 0.0)) {
        %datablock = unitCubeGreyDataBlock;
    }
    %datablock = unitCubeBlueDataBlock;
    $gClientSideSceneObjectsTimer = schedule(100, 0, "dev_clientSideSceneObjectsTick");
};
function standardizeWindowAspect() {
    %standardX = 960;
    %standardY = 544;
    %currentX = getWord($UserPref::Video::Resolution, 0);
    %currentY = getWord($UserPref::Video::Resolution, 1);
    %currentBPP = getWord($UserPref::Video::Resolution, 2);
    %proportionX = (%currentX / %standardX);
    %proportionY = (%currentY / %standardY);
    if ((%proportionX > %proportionY)) {
        %currentY = (%proportionX * %standardY);
    }
    %currentX = (%proportionY * %standardX);
    setScreenMode(%currentX, %currentY, %currentBPP, 0);
};
function tryArray() {
    %arrayCtrl = new GuiArray2Ctrl("") {
        childrenClassName = "GuiButtonCtrl";
        spacing = 10;
    };
    20.setChildrenExtents(%arrayCtrl, "20 40 80 160");
    20.setNumChildren(%arrayCtrl);
    %arrayCtrl.add(LoginGui);
};
function tryGuiTable() {
    if (isObject(erezG)) {
        erezG.delete();
    }
    %table = new GuiTableCtrl(erezG) {
        position = "30 30";
        extent = "400 400";
        visible = 1;
        childrenClassName = "GuiMLTextCtrl";
        spacing = 2;
    };
    %table.add(LoginGui);
};
function tryDataTable() {
    if (isObject(erezD)) {
        erezD.delete();
    }
    %table = new DataTable(erezD);
};
function tryTable() {
    tryGuiTable();
    tryDataTable();
    erezD.setDataTable(erezG);
    100.addColumn(erezD, "username", "User Names", "string");
    200.addColumn(erezD, "population", "Population", "number");
    50.addColumn(erezD, "online", "Online", "icon");
    "platform/client/ui/checkmark_green".addIconToColumn(erezD, "online", "online");
    "platform/client/ui/ellipsis_yellow".addIconToColumn(erezD, "online", "idle");
    "platform/client/ui/arrow_red_right".addIconToColumn(erezD, "online", "offline");
    5.addRows(erezD);
    "username" @ "\t" @ "erez" @ "\t" @ "erez" @ "\n" @ "population" @ "\t" @ 30 @ "\t" @ 30 @ "\n" @ "online" @ "\t" @ "online" @ "\t" @ "[ICON]".setRowDataByIndex(erezD, 0);
    "username" @ "\t" @ "ship" @ "\t" @ "<b>ship" @ "\n" @ "population" @ "\t" @ 70 @ "\t" @ 70 @ "\n" @ "online" @ "\t" @ "offline" @ "\t" @ "[ICON]".setRowDataByIndex(erezD, 1);
    "username" @ "\t" @ "boat" @ "\t" @ "<color:ff0000>boat" @ "\n" @ "population" @ "\t" @ 60 @ "\t" @ 60 @ "\n" @ "online" @ "\t" @ "online" @ "\t" @ "[ICON]".setRowDataByIndex(erezD, 2);
    "username" @ "\t" @ "band" @ "\t" @ "<clip:40>band</clip>" @ "\n" @ "population" @ "\t" @ 20 @ "\t" @ 20 @ "\n" @ "online" @ "\t" @ "idle" @ "\t" @ "[ICON]".setRowDataByIndex(erezD, 3);
    "username" @ "\t" @ "dunk" @ "\t" @ "<color:00ff00>dunk" @ "\n" @ "population" @ "\t" @ 90 @ "\t" @ 90 @ "\n" @ "online" @ "\t" @ "offline" @ "\t" @ "[ICON]".setRowDataByIndex(erezD, 4);
    erezD.updateListeners();
};
function devAvatarNamesNormal() {
    TheShapeNameHud.numNameColors = 0;
};
function devAvatarNamesBlues() {
    %n = 0;
    %n[$gDevNameColors @ %n] = "0.0 0.0 1.0 1.0";
    %n = (%n + 1.0);
    %n[$gDevNameColors @ %n] = "0.2 0.2 0.9 1.0";
    %n = (%n + 1.0);
    %n[$gDevNameColors @ %n] = "0.3 0.3 0.8 1.0";
    %n = (%n + 1.0);
    %n[$gDevNameColors @ %n] = "0.4 0.4 0.8 1.0";
    %n = (%n + 1.0);
    %n[$gDevNameColors @ %n] = "0.5 0.5 0.9 1.0";
    %n = (%n + 1.0);
    %n[$gDevNameColors @ %n] = "0.6 0.6 0.9 1.0";
    %n = (%n + 1.0);
    %n[$gDevNameColors @ %n] = "0.8 0.8 0.9 1.0";
    %n = (%n + 1.0);
    %n[$gDevNameColors @ %n] = "0.9 0.9 1.0 1.0";
    %n = (%n + 1.0);
    %n[$gDevNameColors @ %n] = "1.0 1.0 1.0 1.0";
    %n = (%n + 1.0);
    TheShapeNameHud.numNameColors = %n;
};
function devAvatarNamesGreens() {
    %n = 0;
    %n[$gDevNameColors @ %n] = "0.0 0.6 0.0 1.0";
    %n = (%n + 1.0);
    %n[$gDevNameColors @ %n] = "0.0 0.7 0.0 1.0";
    %n = (%n + 1.0);
    %n[$gDevNameColors @ %n] = "0.0 0.8 0.0 1.0";
    %n = (%n + 1.0);
    %n[$gDevNameColors @ %n] = "0.0 0.9 0.0 1.0";
    %n = (%n + 1.0);
    %n[$gDevNameColors @ %n] = "0.3 0.9 0.0 1.0";
    %n = (%n + 1.0);
    %n[$gDevNameColors @ %n] = "0.5 0.9 0.0 1.0";
    %n = (%n + 1.0);
    %n[$gDevNameColors @ %n] = "0.6 0.9 0.0 1.0";
    %n = (%n + 1.0);
    %n[$gDevNameColors @ %n] = "0.7 1.0 0.0 1.0";
    %n = (%n + 1.0);
    %n[$gDevNameColors @ %n] = "0.8 1.0 0.0 1.0";
    %n = (%n + 1.0);
    TheShapeNameHud.numNameColors = %n;
};
function devAvatarNamesIcons() {
    %n = 0;
    %n[$gDevNameColors @ %n] = "1.0 1.0 1.0 1.0 platform/client/buttons/star_d";
    %n = (%n + 1.0);
    %n[$gDevNameColors @ %n] = "1.0 1.0 1.0 1.0 platform/client/buttons/star_h";
    %n = (%n + 1.0);
    %n[$gDevNameColors @ %n] = "1.0 1.0 1.0 1.0 platform/client/buttons/star_i";
    %n = (%n + 1.0);
    %n[$gDevNameColors @ %n] = "1.0 1.0 1.0 1.0 platform/client/buttons/star_n";
    %n = (%n + 1.0);
    %n[$gDevNameColors @ %n] = "1.0 1.0 1.0 1.0 platform/client/buttons/pending_d";
    %n = (%n + 1.0);
    %n[$gDevNameColors @ %n] = "1.0 1.0 1.0 1.0 platform/client/buttons/pending_h";
    %n = (%n + 1.0);
    %n[$gDevNameColors @ %n] = "1.0 1.0 1.0 1.0 platform/client/buttons/pending_i";
    %n = (%n + 1.0);
    %n[$gDevNameColors @ %n] = "1.0 1.0 1.0 1.0 platform/client/buttons/pending_n";
    %n = (%n + 1.0);
    %n[$gDevNameColors @ %n] = "1.0 1.0 1.0 1.0 platform/client/buttons/hud_scores_d";
    %n = (%n + 1.0);
    %n[$gDevNameColors @ %n] = "1.0 1.0 1.0 1.0 platform/client/buttons/hud_scores_h";
    %n = (%n + 1.0);
    %n[$gDevNameColors @ %n] = "1.0 1.0 1.0 1.0 platform/client/buttons/hud_scores_i";
    %n = (%n + 1.0);
    %n[$gDevNameColors @ %n] = "1.0 1.0 1.0 1.0 platform/client/buttons/hud_scores_n";
    %n = (%n + 1.0);
    %n[$gDevNameColors @ %n] = "1.0 1.0 1.0 1.0 platform/client/buttons/buddies_d";
    %n = (%n + 1.0);
    %n[$gDevNameColors @ %n] = "1.0 1.0 1.0 1.0 platform/client/buttons/buddies_h";
    %n = (%n + 1.0);
    %n[$gDevNameColors @ %n] = "1.0 1.0 1.0 1.0 platform/client/buttons/buddies_i";
    %n = (%n + 1.0);
    %n[$gDevNameColors @ %n] = "1.0 1.0 1.0 1.0 platform/client/buttons/buddies_n";
    %n = (%n + 1.0);
    %n[$gDevNameColors @ %n] = "1.0 1.0 1.0 1.0 platform/client/ui/buildingDir_heart_blue";
    %n = (%n + 1.0);
    %n[$gDevNameColors @ %n] = "1.0 1.0 1.0 1.0 platform/client/ui/buildingDir_heart_green";
    %n = (%n + 1.0);
    %n[$gDevNameColors @ %n] = "1.0 1.0 1.0 1.0 platform/client/ui/buildingDir_heart_white";
    %n = (%n + 1.0);
    %n[$gDevNameColors @ %n] = "1.0 1.0 1.0 1.0 platform/client/ui/friendsHud_lightning_h";
    %n = (%n + 1.0);
    %n[$gDevNameColors @ %n] = "1.0 1.0 1.0 1.0 platform/client/ui/friendsHud_lightning_i";
    %n = (%n + 1.0);
    %n[$gDevNameColors @ %n] = "1.0 1.0 1.0 1.0 platform/client/ui/friendsHud_lightning_n";
    %n = (%n + 1.0);
    TheShapeNameHud.numNameColors = %n;
};
function devAvatarNamesColorsAndIcons() {
    %n = 0;
    %n[$gDevNameColors @ %n] = "0.0 0.0 1.0 1.0 platform/client/buttons/star_d";
    %n = (%n + 1.0);
    %n[$gDevNameColors @ %n] = "0.2 0.2 0.9 1.0 platform/client/buttons/star_h";
    %n = (%n + 1.0);
    %n[$gDevNameColors @ %n] = "0.3 0.3 0.8 1.0 platform/client/buttons/star_i";
    %n = (%n + 1.0);
    %n[$gDevNameColors @ %n] = "0.4 0.4 0.8 1.0 platform/client/buttons/star_n";
    %n = (%n + 1.0);
    %n[$gDevNameColors @ %n] = "0.5 0.5 0.9 1.0 platform/client/buttons/pending_d";
    %n = (%n + 1.0);
    %n[$gDevNameColors @ %n] = "0.6 0.6 0.9 1.0 platform/client/buttons/pending_h";
    %n = (%n + 1.0);
    %n[$gDevNameColors @ %n] = "0.8 0.8 0.9 1.0 platform/client/buttons/pending_i";
    %n = (%n + 1.0);
    %n[$gDevNameColors @ %n] = "0.9 0.9 1.0 1.0 platform/client/buttons/pending_n";
    %n = (%n + 1.0);
    %n[$gDevNameColors @ %n] = "1.0 1.0 1.0 1.0 platform/client/buttons/hud_scores_d";
    %n = (%n + 1.0);
    %n[$gDevNameColors @ %n] = "0.0 0.6 0.0 1.0 platform/client/buttons/hud_scores_h";
    %n = (%n + 1.0);
    %n[$gDevNameColors @ %n] = "0.0 0.7 0.0 1.0 platform/client/buttons/hud_scores_i";
    %n = (%n + 1.0);
    %n[$gDevNameColors @ %n] = "0.0 0.8 0.0 1.0 platform/client/buttons/hud_scores_n";
    %n = (%n + 1.0);
    %n[$gDevNameColors @ %n] = "0.0 0.9 0.0 1.0 platform/client/buttons/buddies_d";
    %n = (%n + 1.0);
    %n[$gDevNameColors @ %n] = "0.3 0.9 0.0 1.0 platform/client/buttons/buddies_h";
    %n = (%n + 1.0);
    %n[$gDevNameColors @ %n] = "0.5 0.9 0.0 1.0 platform/client/buttons/buddies_i";
    %n = (%n + 1.0);
    %n[$gDevNameColors @ %n] = "0.6 0.9 0.0 1.0 platform/client/buttons/buddies_n";
    %n = (%n + 1.0);
    %n[$gDevNameColors @ %n] = "0.7 1.0 0.0 1.0 platform/client/ui/buildingDir_heart_blue";
    %n = (%n + 1.0);
    %n[$gDevNameColors @ %n] = "0.8 1.0 0.0 1.0 platform/client/ui/buildingDir_heart_green";
    %n = (%n + 1.0);
    %n[$gDevNameColors @ %n] = "1.0 1.0 1.0 1.0 platform/client/ui/buildingDir_heart_white";
    %n = (%n + 1.0);
    %n[$gDevNameColors @ %n] = "1.0 1.0 1.0 1.0 platform/client/ui/friendsHud_lightning_h";
    %n = (%n + 1.0);
    %n[$gDevNameColors @ %n] = "1.0 1.0 1.0 1.0 platform/client/ui/friendsHud_lightning_i";
    %n = (%n + 1.0);
    %n[$gDevNameColors @ %n] = "1.0 1.0 1.0 1.0 platform/client/ui/friendsHud_lightning_n";
    %n = (%n + 1.0);
    TheShapeNameHud.numNameColors = %n;
};
$gAnimTestNum = 0;
$gAnimTestCur = 0;
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "bcidl1a";
$gAnimTestNum = ($gAnimTestNum + 1.0);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "bwlkf1";
$gAnimTestNum = ($gAnimTestNum + 1.0);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "iang";
$gAnimTestNum = ($gAnimTestNum + 1.0);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "iwlkf1";
$gAnimTestNum = ($gAnimTestNum + 1.0);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "narcadeidl";
$gAnimTestNum = ($gAnimTestNum + 1.0);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "nbassr1e";
$gAnimTestNum = ($gAnimTestNum + 1.0);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "nblext";
$gAnimTestNum = ($gAnimTestNum + 1.0);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "nblidl1";
$gAnimTestNum = ($gAnimTestNum + 1.0);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "nbrshft";
$gAnimTestNum = ($gAnimTestNum + 1.0);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "nbtwlkl";
$gAnimTestNum = ($gAnimTestNum + 1.0);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "nclbent";
$gAnimTestNum = ($gAnimTestNum + 1.0);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "nclbext";
$gAnimTestNum = ($gAnimTestNum + 1.0);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "nclbidl1";
$gAnimTestNum = ($gAnimTestNum + 1.0);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "ncutout01";
$gAnimTestNum = ($gAnimTestNum + 1.0);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "ncutout02";
$gAnimTestNum = ($gAnimTestNum + 1.0);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "nd2step";
$gAnimTestNum = ($gAnimTestNum + 1.0);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "nd2stepx";
$gAnimTestNum = ($gAnimTestNum + 1.0);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "ndhtoe";
$gAnimTestNum = ($gAnimTestNum + 1.0);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "ndlnwit";
$gAnimTestNum = ($gAnimTestNum + 1.0);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "ndrumr1e";
$gAnimTestNum = ($gAnimTestNum + 1.0);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "ndshfle";
$gAnimTestNum = ($gAnimTestNum + 1.0);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "ndslpsld";
$gAnimTestNum = ($gAnimTestNum + 1.0);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "ndvstepb";
$gAnimTestNum = ($gAnimTestNum + 1.0);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "ndwlkit";
$gAnimTestNum = ($gAnimTestNum + 1.0);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "ndxhop";
$gAnimTestNum = ($gAnimTestNum + 1.0);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "ngo01";
$gAnimTestNum = ($gAnimTestNum + 1.0);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "ngo02";
$gAnimTestNum = ($gAnimTestNum + 1.0);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "ngo03";
$gAnimTestNum = ($gAnimTestNum + 1.0);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "ngo04";
$gAnimTestNum = ($gAnimTestNum + 1.0);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "ngo05";
$gAnimTestNum = ($gAnimTestNum + 1.0);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "ngo06";
$gAnimTestNum = ($gAnimTestNum + 1.0);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "ngo07";
$gAnimTestNum = ($gAnimTestNum + 1.0);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "ngo08";
$gAnimTestNum = ($gAnimTestNum + 1.0);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "ngtrglr10a";
$gAnimTestNum = ($gAnimTestNum + 1.0);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "ngtrglr11a";
$gAnimTestNum = ($gAnimTestNum + 1.0);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "ngtrglr12a";
$gAnimTestNum = ($gAnimTestNum + 1.0);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "ngtrglr13a";
$gAnimTestNum = ($gAnimTestNum + 1.0);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "ngtrglr14a";
$gAnimTestNum = ($gAnimTestNum + 1.0);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "ngtrglr15b";
$gAnimTestNum = ($gAnimTestNum + 1.0);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "ngtrglr16b";
$gAnimTestNum = ($gAnimTestNum + 1.0);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "ngtrglr17b";
$gAnimTestNum = ($gAnimTestNum + 1.0);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "ngtrglr18b";
$gAnimTestNum = ($gAnimTestNum + 1.0);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "ngtrglr19b";
$gAnimTestNum = ($gAnimTestNum + 1.0);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "ngtrglr1e";
$gAnimTestNum = ($gAnimTestNum + 1.0);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "ngtrglr20b";
$gAnimTestNum = ($gAnimTestNum + 1.0);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "ngtrglr2e";
$gAnimTestNum = ($gAnimTestNum + 1.0);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "ngtrglr3e";
$gAnimTestNum = ($gAnimTestNum + 1.0);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "ngtrglr4e";
$gAnimTestNum = ($gAnimTestNum + 1.0);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "ngtrglr5e";
$gAnimTestNum = ($gAnimTestNum + 1.0);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "ngtrglr6e";
$gAnimTestNum = ($gAnimTestNum + 1.0);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "ngtrglr7e";
$gAnimTestNum = ($gAnimTestNum + 1.0);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "ngtrglr8a";
$gAnimTestNum = ($gAnimTestNum + 1.0);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "ngtrglr9a";
$gAnimTestNum = ($gAnimTestNum + 1.0);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "ngtrglridl1";
$gAnimTestNum = ($gAnimTestNum + 1.0);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "ngtrglrjmp01";
$gAnimTestNum = ($gAnimTestNum + 1.0);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "ngtrglrside01";
$gAnimTestNum = ($gAnimTestNum + 1.0);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "ngtrglrwlkb01";
$gAnimTestNum = ($gAnimTestNum + 1.0);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "ngtrglrwlkf01";
$gAnimTestNum = ($gAnimTestNum + 1.0);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "ngtrgr10e";
$gAnimTestNum = ($gAnimTestNum + 1.0);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "ngtrgr11e";
$gAnimTestNum = ($gAnimTestNum + 1.0);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "ngtrgr12a";
$gAnimTestNum = ($gAnimTestNum + 1.0);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "ngtrgr13a";
$gAnimTestNum = ($gAnimTestNum + 1.0);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "ngtrgr14a";
$gAnimTestNum = ($gAnimTestNum + 1.0);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "ngtrgr15a";
$gAnimTestNum = ($gAnimTestNum + 1.0);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "ngtrgr17a";
$gAnimTestNum = ($gAnimTestNum + 1.0);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "ngtrgr18a";
$gAnimTestNum = ($gAnimTestNum + 1.0);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "ngtrgr1e";
$gAnimTestNum = ($gAnimTestNum + 1.0);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "ngtrgr22a";
$gAnimTestNum = ($gAnimTestNum + 1.0);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "ngtrgr25b";
$gAnimTestNum = ($gAnimTestNum + 1.0);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "ngtrgr28b";
$gAnimTestNum = ($gAnimTestNum + 1.0);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "ngtrgr29b";
$gAnimTestNum = ($gAnimTestNum + 1.0);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "ngtrgr2e";
$gAnimTestNum = ($gAnimTestNum + 1.0);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "ngtrgr3e";
$gAnimTestNum = ($gAnimTestNum + 1.0);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "ngtrgr5e";
$gAnimTestNum = ($gAnimTestNum + 1.0);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "ngtrgr6e";
$gAnimTestNum = ($gAnimTestNum + 1.0);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "ngtrgr7e";
$gAnimTestNum = ($gAnimTestNum + 1.0);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "ngtrgr8e";
$gAnimTestNum = ($gAnimTestNum + 1.0);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "ngtrgr9e";
$gAnimTestNum = ($gAnimTestNum + 1.0);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "nhead_LR";
$gAnimTestNum = ($gAnimTestNum + 1.0);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "nhead_UD";
$gAnimTestNum = ($gAnimTestNum + 1.0);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "nhead_ss";
$gAnimTestNum = ($gAnimTestNum + 1.0);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "nhi5ee";
$gAnimTestNum = ($gAnimTestNum + 1.0);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "nhi5er";
$gAnimTestNum = ($gAnimTestNum + 1.0);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "nlsnext";
$gAnimTestNum = ($gAnimTestNum + 1.0);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "nlyidl1";
$gAnimTestNum = ($gAnimTestNum + 1.0);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "nmcheer";
$gAnimTestNum = ($gAnimTestNum + 1.0);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "nmcheer1";
$gAnimTestNum = ($gAnimTestNum + 1.0);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "nmchug";
$gAnimTestNum = ($gAnimTestNum + 1.0);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "nmdip";
$gAnimTestNum = ($gAnimTestNum + 1.0);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "nmdrink";
$gAnimTestNum = ($gAnimTestNum + 1.0);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "nmflrt";
$gAnimTestNum = ($gAnimTestNum + 1.0);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "nmlol";
$gAnimTestNum = ($gAnimTestNum + 1.0);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "nmlowdnc";
$gAnimTestNum = ($gAnimTestNum + 1.0);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "nmmic";
$gAnimTestNum = ($gAnimTestNum + 1.0);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "nmomg";
$gAnimTestNum = ($gAnimTestNum + 1.0);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "nmpitch";
$gAnimTestNum = ($gAnimTestNum + 1.0);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "nmpiv";
$gAnimTestNum = ($gAnimTestNum + 1.0);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "nmroll";
$gAnimTestNum = ($gAnimTestNum + 1.0);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "nmsitbend";
$gAnimTestNum = ($gAnimTestNum + 1.0);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "nmsitpiv";
$gAnimTestNum = ($gAnimTestNum + 1.0);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "nmtoast";
$gAnimTestNum = ($gAnimTestNum + 1.0);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "nmupdnc";
$gAnimTestNum = ($gAnimTestNum + 1.0);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "nmupkis";
$gAnimTestNum = ($gAnimTestNum + 1.0);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "nmupyaw";
$gAnimTestNum = ($gAnimTestNum + 1.0);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "nmwave";
$gAnimTestNum = ($gAnimTestNum + 1.0);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "nmwlk";
$gAnimTestNum = ($gAnimTestNum + 1.0);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "npwbbattack";
$gAnimTestNum = ($gAnimTestNum + 1.0);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "npwidle";
$gAnimTestNum = ($gAnimTestNum + 1.0);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "npwjabattack";
$gAnimTestNum = ($gAnimTestNum + 1.0);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "npwjabdefend";
$gAnimTestNum = ($gAnimTestNum + 1.0);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "npwjmp";
$gAnimTestNum = ($gAnimTestNum + 1.0);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "npwlongstun";
$gAnimTestNum = ($gAnimTestNum + 1.0);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "npwpowerattack";
$gAnimTestNum = ($gAnimTestNum + 1.0);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "npwpowerdefend";
$gAnimTestNum = ($gAnimTestNum + 1.0);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "npwsde";
$gAnimTestNum = ($gAnimTestNum + 1.0);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "npwshortstun";
$gAnimTestNum = ($gAnimTestNum + 1.0);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "npwtaunt01";
$gAnimTestNum = ($gAnimTestNum + 1.0);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "npwtaunt02";
$gAnimTestNum = ($gAnimTestNum + 1.0);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "npwwlkb";
$gAnimTestNum = ($gAnimTestNum + 1.0);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "npwwlkf";
$gAnimTestNum = ($gAnimTestNum + 1.0);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "nreachdown";
$gAnimTestNum = ($gAnimTestNum + 1.0);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "nrlidl1";
$gAnimTestNum = ($gAnimTestNum + 1.0);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "nrsbbeaux";
$gAnimTestNum = ($gAnimTestNum + 1.0);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "nrsbloop";
$gAnimTestNum = ($gAnimTestNum + 1.0);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "nrsbreaux";
$gAnimTestNum = ($gAnimTestNum + 1.0);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "nrsbsham";
$gAnimTestNum = ($gAnimTestNum + 1.0);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "nsitlsn";
$gAnimTestNum = ($gAnimTestNum + 1.0);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "nsittlk";
$gAnimTestNum = ($gAnimTestNum + 1.0);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "nspinbottle";
$gAnimTestNum = ($gAnimTestNum + 1.0);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "nssext";
$gAnimTestNum = ($gAnimTestNum + 1.0);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "nsumobbattack";
$gAnimTestNum = ($gAnimTestNum + 1.0);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "nsumoidle";
$gAnimTestNum = ($gAnimTestNum + 1.0);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "nsumojabattack";
$gAnimTestNum = ($gAnimTestNum + 1.0);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "nsumojabdefend";
$gAnimTestNum = ($gAnimTestNum + 1.0);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "nsumojmp";
$gAnimTestNum = ($gAnimTestNum + 1.0);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "nsumolongstun";
$gAnimTestNum = ($gAnimTestNum + 1.0);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "nsumopowerattack";
$gAnimTestNum = ($gAnimTestNum + 1.0);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "nsumopowerdefend";
$gAnimTestNum = ($gAnimTestNum + 1.0);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "nsumoshortstun";
$gAnimTestNum = ($gAnimTestNum + 1.0);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "nsumostun";
$gAnimTestNum = ($gAnimTestNum + 1.0);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "nsumotaunt01";
$gAnimTestNum = ($gAnimTestNum + 1.0);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "nsumotaunt02";
$gAnimTestNum = ($gAnimTestNum + 1.0);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "nsumowlkb";
$gAnimTestNum = ($gAnimTestNum + 1.0);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "nsumowlkf";
$gAnimTestNum = ($gAnimTestNum + 1.0);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "nsumowlks";
$gAnimTestNum = ($gAnimTestNum + 1.0);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "ntalk";
$gAnimTestNum = ($gAnimTestNum + 1.0);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "ntapglass";
$gAnimTestNum = ($gAnimTestNum + 1.0);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "nthink";
$gAnimTestNum = ($gAnimTestNum + 1.0);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "ntyidl1a";
$gAnimTestNum = ($gAnimTestNum + 1.0);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "ntyrsml";
$gAnimTestNum = ($gAnimTestNum + 1.0);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "ntyrturn";
$gAnimTestNum = ($gAnimTestNum + 1.0);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "nvom";
$gAnimTestNum = ($gAnimTestNum + 1.0);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "nxrcst";
$gAnimTestNum = ($gAnimTestNum + 1.0);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "nzidl1";
$gAnimTestNum = ($gAnimTestNum + 1.0);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "nzwlk";
$gAnimTestNum = ($gAnimTestNum + 1.0);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "ygunsling";
$gAnimTestNum = ($gAnimTestNum + 1.0);
function animTest_Again() {
    %anim = $player.getGender() @ $gAnimTestCur[$gAnimTestAnim @ $gAnimTestCur];
    %anim.playAnim($player);
    echo(getScopeName() @ " " @ "-" @ " " @ %anim);
};
function animTest_Next() {
    $gAnimTestCur = ($gAnimTestCur + 1.0);
    if (($gAnimTestCur >= $gAnimTestNum)) {
        $gAnimTestCur = 0;
    }
    animTest_Again();
};
function animTest_Prev() {
    $gAnimTestCur = ($gAnimTestCur - 1.0);
    if (($gAnimTestCur < 0.0)) {
        $gAnimTestCur = ($gAnimTestCur - 1.0);
    }
    animTest_Again();
};
function timeTest_tare() {
    $gTimeTest_StartTimeReal = getRealTime();
    $gTimeTest_StartTimeSim = getSimTime();
    $gTimeTest_dRealToSim = mSubS32($gTimeTest_StartTimeSim, $gTimeTest_StartTimeReal);
};
function timeTest_measure() {
    %timeReal = getRealTime();
    %timeSim = getSimTime();
    %elapsedReal = mSubS32(%timeReal, $gTimeTest_StartTimeReal);
    %elapsedSim = mSubS32(%timeSim, $gTimeTest_StartTimeSim);
    %expectedTimeSim = mAddS32(%timeReal, $gTimeTest_dRealToSim);
    %driftSim = mSubS32(%expectedTimeSim, %timeSim);
    echo("driftSim is" @ " " @ (%driftSim * 0.001));
    echo("elapsed real seconds   =" @ " " @ (%elapsedReal * 0.001));
    echo("elapsed sim  seconds   =" @ " " @ (%elapsedSim * 0.001));
    echo("driftSim  /elapsedReal =" @ " " @ (%driftSim / %elapsedReal));
    echo("elapsedSim/elapsedReal =" @ " " @ (%elapsedSim / %elapsedReal));
};
function dev_TestRequestRetry() {
    %url = "http://winbuild.doppelganger.com/scripts/orion/fakeEnvManagerResponses/failedRequest1.txt";
    %request = sendRequest_ArbitraryTestUrl(%url, "onDoneOrErrorCallback_TestRequestRetry");
    %request.retryTotal = 1;
};
function onDoneOrErrorCallback_TestRequestRetry(%request) {
    echo(getScopeName() @ " " @ "- YEP!" @ " " @ getTrace());
};
function sendRequest_ArbitraryTestUrl(%url, %callbackHandler) {
    %request = safeNewScriptObject("ManagerRequest", "", 0);
    "UniformManagerRequest".bindClassName(%request);
    %url.setURL(%request);
    %request.callbackHandler = %callbackHandler;
    %request.start();
    return %request;
};
function dev_testURLEncode() {
    %n = 0;
    while ((%n < 256.0)) {
        %c = intToChar(%n);
        %d = urlEncode(%c);
        %e = urlDecode(%d);
        echo(formatInt("%3d", %n) @ " " @ %c @ " " @ "->" @ " " @ %d);
        echo(formatInt("%3d", %n) @ " " @ %e @ " " @ "<-" @ " " @ %d);
        if ((%n > 20.0)) {
            %gnarly = %gnarly @ %c;
        }
        %n = (%n + 1.0);
    }
    echo(%gnarly);
    return %gnarly;
};
function dev_ensureRandomItemManager() {
    if (!(isObject(gRandomItemManager))) {
        new ScriptObject(gRandomItemManager);
        if (isObject(MissionCleanup)) {
            gRandomItemManager.add(MissionCleanup);
            gRandomItemManager.numItems = 0;
        }
    }
};
function dev_clearRandomItems() {
    dev_ensureRandomItemManager();
    gRandomItemManager.numItems = 0;
};
function dev_declareRandomItem(%itemName, %itemWeight) {
    dev_ensureRandomItemManager();
    %n = gRandomItemManager.numItems;
    gRandomItemManager.itemName = %itemName @ %n;
    gRandomItemManager.itemWeight = %itemWeight @ %n;
    gRandomItemManager.weightsNeedNormalizing = 1;
    gRandomItemManager.numItems = (gRandomItemManager.numItems + 1.0);
};
function dev_getRandomItem() {
    dev_ensureRandomItemManager();
    if (gRandomItemManager.weightsNeedNormalizing) {
        gRandomItemManager.weightsNeedNormalizing = 0;
        %totalWeight = 0;
        %n = 0;
        while ((%n < gRandomItemManager.numItems)) {
            gRandomItemManager.itemWeightCumulative = (gRandomItemManager.itemWeight + %totalWeight @ %n) @ %n;
            %totalWeight = (%totalWeight + gRandomItemManager.itemWeight);
            %n;
            %n = (%n + 1.0);
        }
        gRandomItemManager.totalWeight = (%n < gRandomItemManager.numItems) @ %totalWeight;
    }
    %rand = getRandom(0, (gRandomItemManager.totalWeight - 1.0));
    %n = 0;
    while ((%n < gRandomItemManager.numItems)) {
        if ((%rand < gRandomItemManager.itemWeightCumulative)) {
            return gRandomItemManager.itemName;
        }
        %n = (%n + 1.0);
    }
    error("something went wrong.");
    return "";
};
function dev_testRandomItems(%iterations) {
    %totals["A"] = 0;
    %totals["B"] = 0;
    %totals["C"] = 0;
    %totals["D"] = 0;
    dev_clearRandomItems();
    dev_declareRandomItem("A", 1);
    dev_declareRandomItem("B", 1);
    dev_declareRandomItem("C", 1);
    dev_declareRandomItem("D", 3);
    %n = 0;
    while ((%n < %iterations)) {
        %item = dev_getRandomItem();
        %item[%totals @ %item] = (%item[%totals @ %item] + 1.0);
        %n = (%n + 1.0);
    }
    echo("A -" @ " " @ %n[%totals @ "A"]);
    echo("B -" @ " " @ %totals["B"]);
    echo("C -" @ " " @ %totals["C"]);
    echo("D -" @ " " @ %totals["D"]);
};
function SimObject::getTypeStrings(%this) {
    %types = "";
    %mask = %this.getType();
    %types = %types @ (%mask & $TypeMasks::StaticObjectType) ? "StaticObjectType " : "";
    %types = %types @ (%mask & $TypeMasks::EnvironmentObjectType) ? "EnvironmentObjectType " : "";
    %types = %types @ (%mask & $TypeMasks::TerrainObjectType) ? "TerrainObjectType " : "";
    %types = %types @ (%mask & $TypeMasks::InteriorObjectType) ? "InteriorObjectType " : "";
    %types = %types @ (%mask & $TypeMasks::WaterObjectType) ? "WaterObjectType " : "";
    %types = %types @ (%mask & $TypeMasks::TriggerObjectType) ? "TriggerObjectType " : "";
    %types = %types @ (%mask & $TypeMasks::AntiPortalObjectType) ? "AntiPortalObjectType " : "";
    %types = %types @ (%mask & $TypeMasks::ZoneBoxObjectType) ? "ZoneBoxObjectType " : "";
    %types = %types @ (%mask & $TypeMasks::MarkerObjectType) ? "MarkerObjectType " : "";
    %types = %types @ (%mask & $TypeMasks::GameBaseObjectType) ? "GameBaseObjectType " : "";
    %types = %types @ (%mask & $TypeMasks::ShapeBaseObjectType) ? "ShapeBaseObjectType " : "";
    %types = %types @ (%mask & $TypeMasks::CameraObjectType) ? "CameraObjectType " : "";
    %types = %types @ (%mask & $TypeMasks::StaticShapeObjectType) ? "StaticShapeObjectType " : "";
    %types = %types @ (%mask & $TypeMasks::PlayerObjectType) ? "PlayerObjectType " : "";
    %types = %types @ (%mask & $TypeMasks::ItemObjectType) ? "ItemObjectType " : "";
    %types = %types @ (%mask & $TypeMasks::VehicleObjectType) ? "VehicleObjectType " : "";
    %types = %types @ (%mask & $TypeMasks::VehicleBlockerObjectType) ? "VehicleBlockerObjectType " : "";
    %types = %types @ (%mask & $TypeMasks::ProjectileObjectType) ? "ProjectileObjectType " : "";
    %types = %types @ (%mask & $TypeMasks::ExplosionObjectType) ? "ExplosionObjectType " : "";
    %types = %types @ (%mask & $TypeMasks::CorpseObjectType) ? "CorpseObjectType " : "";
    %types = %types @ (%mask & $TypeMasks::DebrisObjectType) ? "DebrisObjectType " : "";
    %types = %types @ (%mask & $TypeMasks::PhysicalZoneObjectType) ? "PhysicalZoneObjectType " : "";
    %types = %types @ (%mask & $TypeMasks::StaticTSObjectType) ? "StaticTSObjectType " : "";
    %types = %types @ (%mask & $TypeMasks::StaticRenderedObjectType) ? "StaticRenderedObjectType " : "";
    %types = %types @ (%mask & $TypeMasks::DamagableItemObjectType) ? "DamagableItemObjectType " : "";
    %types = %types @ (%mask & $TypeMasks::AdvertObjectType) ? "AdvertObjectType " : "";
    %types = %types @ (%mask & $TypeMasks::ConversationObjectType) ? "ConversationObjectType " : "";
    %types = %types @ (%mask & $TypeMasks::UsableObjectType) ? "UsableObjectType " : "";
    %types = trim(%types);
    return %types;
};
$gTwitterText = "";
$gTwitterTextCount = 1;
function twitterTest1(%text) {
    if (!(isDefined("%text"))) {
        %text = "hey there";
    }
    if ((%text $= $gTwitterText)) {
        $gTwitterTextCount = ($gTwitterTextCount + 1.0);
        %text = %text @ " " @ $gTwitterTextCount;
    }
    $gTwitterText = %text;
    $gTwitterTextCount = 1;
    %request = new URLPostObject("");
    "https://twitter.com/statuses/update.xml".setURL(%request);
    %text.setBodyParam(%request, "status");
    "elenzil:etspass777".setUserNameAndPassword(%request);
    %request.start();
};
function GuiControl::snapAndUpToTwitter(%this, %userName, %password, %asBackground, %tile) {
    isDefined("%asBackground", 0);
    isDefined("%tile", 0);
    %region = %this.getScreenPosition() @ " " @ %this.getExtent();
    return snapshot::snapAndUpRegionToTwitter(%region, "", %userName, %password, %asBackground, %tile);
};
function snapshot::snapAndUpRegionToTwitter(%region, %fileName, %userName, %password, %asBackground, %tile) {
    isDefined("%asBackground", 0);
    isDefined("%tile", 0);
    if ((%fileName $= "")) {
        %fileName = "screenshot_" @ getSubStr(getTimeStamp(), 0, 17) @ "_twitter_" @ $screenShotNum;
    }
    %fn_orig = %fileName;
    %fileName = %fileName @ ".jpg";
    if (%asBackground) {
        %url = "http://twitter.com/account/update_profile_background_image.html";
    }
    %url = "http://twitter.com/account/update_profile_image.xml";
    %uploader = "";
    if (!(snapshotTool::snapRegion(%region, %fileName))) {
        error(getScopeName() @ " " @ "- Unable to capture region." @ " " @ %region @ " " @ %fileName @ " " @ getTrace());
    }
    $screenShotNum = ($screenShotNum + 1.0);
    %uploader = new URLPostObject("");
    1.setProgress(%uploader);
    %url.setURL(%uploader);
    %userName @ ":" @ %password.setUserNameAndPassword(%uploader);
    %fileName.setPostFile(%uploader, "image");
    "Expect:".setCustomHeaders(%uploader);
    if (%tile) {
        "true".setBodyParam(%uploader, "tile");
    }
    if (!(%uploader.start())) {
        error(getScopeName() @ " " @ "- Unable to upload photo." @ " " @ %fileName @ " " @ %url @ " " @ getTrace());
    }
    return %uploader;
};
