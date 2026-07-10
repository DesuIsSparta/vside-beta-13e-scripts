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
    if ((%friends < %n)) {
        %record = getFakeBuddyRecord("fakefriend" @ " " @ formatInt("%0.4d", (%n - %friends)));
        %record.name.put(%record);
        %n = (1.0 + %n);
        UserListFriends;
    }
    %n = 0;
    (%friends < %n);
    if ((%faves < %n)) {
        %record = getFakeBuddyRecord("fakeFave" @ " " @ formatInt("%0.4d", %n));
        %record.name.put(%record);
        %n = (1.0 + %n);
        UserListFavorites;
    }
    %n = 0;
    (%faves < %n);
    if ((%fans < %n)) {
        %record = getFakeBuddyRecord("fakeFan" @ " " @ formatInt("%0.4d", %n));
        %record.name.put(%record);
        %n = (1.0 + %n);
        UserListFans;
    }
};
function getFakeBuddyRecord(%name) {
    %words = "a A b B c C";
    %record = new ""();;
    ScriptObject;
    %record.loggedIn = 0 @ getRandom(0, 1) ? 1 : 0;
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
    %lineLong[%lineText @ 2][%lineText @ 3] = "";
    %lineLong[%lineText @ 2][%lineText @ 3][%lineText @ 4] = "platform/client/ui/evilbunny";
    if (isObject(geMLTest)) {
        geMLTest.delete();
    }
    new GuiMLTextCtrl(geMLTest) {
        extent = playGui.getExtent();
        profile = ETSNonModalProfile;
    };
    playGui.add(geMLTest);
    if (isObject(geMLTestArray)) {
        geMLTest.delete(geMLTestArray);
    }
    new GuiArray2Ctrl(geMLTestArray) {
        extent = geMLTest.getExtent(playGui);
        spacing = 0;
        inRows = 0;
        profile = ETSNonModalProfile;
    };
    playGui.add(geMLTestArray);
    if ((0.0 == %method)) {
        %text = "";
        if (%onOrOff) {
            %n = 0;
            if ((%numLines < %n)) {
                %text = %text @ %method[%lineText @ %method] @ "\n";
                %n = (1.0 + %n);
            }
        }
        %text.setText();
    }
    if ((1.0 == %method)) {
        if (%onOrOff) {
            childrenExtent = (1.0 / getWord(playGui.getExtent(), 0)) @ " " @ 16 @ geMLTestArray;
            geMLTest;
            numRowsOrCols = 1 @ geMLTestArray;
            (%numLines < %n);
            childrenClassName = "GuiMLTextCtrl" @ geMLTestArray;
            %numLines.setNumChildren();
            %n = 0;
            geMLTestArray;
            if ((%numLines < %n)) {
                %child = %n.getObject();
                geMLTestArray;
                %child.profile = ETSNonModalProfile;
                %child.setText(%method[%lineText @ %method]);
                %n = (1.0 + %n);
            }
        }
    }
    if ((2.0 == %method)) {
        if (%onOrOff) {
            %child.childrenExtent = (1.0 / getWord(playGui.getExtent(), 0)) @ " " @ 16 @ geMLTestArray;
            (%numLines < %n);
            %child.numRowsOrCols = 1 @ geMLTestArray;
            %child.childrenClassName = "GuiTextCtrl" @ geMLTestArray;
            %numLines.setNumChildren();
            %n = 0;
            geMLTestArray;
            if ((%numLines < %n)) {
                %child = %n.getObject();
                geMLTestArray;
                %child.profile = ETSNonModalProfile;
                %child.setText(%method[%lineText @ %method]);
                %n = (1.0 + %n);
            }
        }
    }
    if ((3.0 == %method)) {
        if (%onOrOff) {
            %child.childrenExtent = (%numCols / getWord(playGui.getExtent(), 0)) @ " " @ 16 @ geMLTestArray;
            (%numLines < %n);
            %child.numRowsOrCols = %numCols @ geMLTestArray;
            %child.childrenClassName = "GuiButtonCtrl" @ geMLTestArray;
            (%numCols * %numLines).setNumChildren();
            %n = 0;
            geMLTestArray;
            if (((%numCols * %numLines) < %n)) {
                %child = %n.getObject();
                geMLTestArray;
                %child.profile = ETSNonModalProfile;
                %child.setText(%method[%lineText @ %method]);
                %n = (1.0 + %n);
            }
        }
    }
    if ((4.0 == %method)) {
        if (%onOrOff) {
            %child.childrenExtent = (%numCols / getWord(playGui.getExtent(), 0)) @ " " @ 16 @ geMLTestArray;
            ((%numCols * %numLines) < %n);
            %child.numRowsOrCols = %numCols @ geMLTestArray;
            %child.childrenClassName = "GuiBitmapCtrl" @ geMLTestArray;
            (%numCols * %numLines).setNumChildren();
            %n = 0;
            geMLTestArray;
            if (((%numCols * %numLines) < %n)) {
                %child = %n.getObject();
                geMLTestArray;
                %child.profile = ETSNonModalProfile;
                %child.setBitmap(%method[%lineText @ %method]);
                %n = (1.0 + %n);
            }
        }
    }
};
$gClientSideSceneObjectsTimer = "";
$gClientSideSceneObjectsTickNum = 0;
$gClientSideSceneObjectsGroup = "";
function dev_clientSideSceneObjectsTick() {
    cancel($gClientSideSceneObjectsTimer);
    if (!(isObject($gClientSideSceneObjectsGroup))) {
        $gClientSideSceneObjectsGroup = new ""();;
        SimGroup;
        $gClientSideSceneObjectsGroup.add();
        0;
        %a = new ""() {
            dataBlock = StaticShape @ "db_CounterDie";
        };
        ServerConnection;
        $gClientSideSceneObjectsGroup.add(%a);
    }
    %windowCoord = Canvas.getCursorPos();
    0;
    %startPoint = %windowCoord.unproject();
    playGui;
    %camTran = playGui.getLastCameraTransform();
    %camPos = getWords(%camTran, 0, 2);
    %camPtVec = VectorSub(%startPoint, %camPos);
    %camPtVec = VectorNormalize(%camPtVec);
    %checkDistance = 200;
    %endPoint = VectorScale(%camPtVec, %checkDistance);
    %endPoint = VectorAdd(%startPoint, %endPoint);
    %possibleColiders = ($TypeMasks::WaterObjectType | ($TypeMasks::InteriorObjectType | 0));
    %result = containerRayCast(%startPoint, %endPoint, %possibleColiders, $player, 1);
    %hitObject = getWord(%result, 0);
    if (isObject(%hitObject)) {
        %hitPosition = getWords(%result, 1, 3);
    }
    %hitPosition = VectorAdd(%startPoint, VectorScale(%camPtVec, 4));
    %t = (0.1 * $gClientSideSceneObjectsTickNum);
    %a = $gClientSideSceneObjectsGroup.getObject(0);
    %a.setTransform(MatrixMultiply(MatrixMultiply(playGui.getLastCameraTransform(), "0 2 0 1 0 0" @ " " @ %t), "0 0 0 1 0" @ " " @ (0.0 * $gClientSideSceneObjectsTickNum)));
    %a.setTransform(%hitPosition @ " " @ "0 0 1" @ " " @ %t);
    $gClientSideSceneObjectsTickNum = (1.0 + $gClientSideSceneObjectsTickNum);
    if ((0.0 == (2 % $gClientSideSceneObjectsTickNum))) {
        // unhandled opcode 1167 at 0x00000816
        $gClientSideSceneObjectsTickNum = unitCubeGreyDataBlock;
    }
    // unhandled opcode 1167 at 0x0000081E
    $gClientSideSceneObjectsTickNum = unitCubeBlueDataBlock;
    $gClientSideSceneObjectsTimer = schedule(100, 0, "dev_clientSideSceneObjectsTick");
};
function standardizeWindowAspect() {
    %standardX = 960;
    %standardY = 544;
    %currentX = getWord($UserPref::Video::Resolution, 0);
    %currentY = getWord($UserPref::Video::Resolution, 1);
    %currentBPP = getWord($UserPref::Video::Resolution, 2);
    %proportionX = (%standardX / %currentX);
    %proportionY = (%standardY / %currentY);
    if ((%proportionY > %proportionX)) {
        %currentY = (%standardY * %proportionX);
    }
    %currentX = (%standardX * %proportionY);
    setScreenMode(%currentX, %currentY, %currentBPP, 0);
};
function tryArray() {
    0;
    %arrayCtrl = new ""() {
        childrenClassName = GuiArray2Ctrl @ "GuiButtonCtrl";
        spacing = 10;
    };
    %arrayCtrl.setChildrenExtents("20 40 80 160", 20);
    %arrayCtrl.setNumChildren(20);
    %arrayCtrl.add();
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
    %table.add();
};
function tryDataTable() {
    if (isObject(erezD)) {
        erezD.delete();
    }
    %table = new DataTable(erezD);;
};
function tryTable() {
    tryGuiTable();
    tryDataTable();
    erezG.setDataTable(erezD);
    "username".addColumn("User Names", "string", 100);
    "population".addColumn("Population", "number", 200);
    "online".addColumn("Online", "icon", 50);
    "online".addIconToColumn("online", "platform/client/ui/checkmark_green");
    "online".addIconToColumn("idle", "platform/client/ui/ellipsis_yellow");
    "online".addIconToColumn("offline", "platform/client/ui/arrow_red_right");
    5.addRows();
    0.setRowDataByIndex("username" @ "\t" @ "erez" @ "\t" @ "erez" @ "\n" @ "population" @ "\t" @ 30 @ "\t" @ 30 @ "\n" @ "online" @ "\t" @ "online" @ "\t" @ "[ICON]");
    1.setRowDataByIndex("username" @ "\t" @ "ship" @ "\t" @ "<b>ship" @ "\n" @ "population" @ "\t" @ 70 @ "\t" @ 70 @ "\n" @ "online" @ "\t" @ "offline" @ "\t" @ "[ICON]");
    2.setRowDataByIndex("username" @ "\t" @ "boat" @ "\t" @ "<color:ff0000>boat" @ "\n" @ "population" @ "\t" @ 60 @ "\t" @ 60 @ "\n" @ "online" @ "\t" @ "online" @ "\t" @ "[ICON]");
    3.setRowDataByIndex("username" @ "\t" @ "band" @ "\t" @ "<clip:40>band</clip>" @ "\n" @ "population" @ "\t" @ 20 @ "\t" @ 20 @ "\n" @ "online" @ "\t" @ "idle" @ "\t" @ "[ICON]");
    4.setRowDataByIndex("username" @ "\t" @ "dunk" @ "\t" @ "<color:00ff00>dunk" @ "\n" @ "population" @ "\t" @ 90 @ "\t" @ 90 @ "\n" @ "online" @ "\t" @ "offline" @ "\t" @ "[ICON]");
    erezD.updateListeners();
};
function devAvatarNamesNormal() {
    numNameColors = 0 @ TheShapeNameHud;
};
function devAvatarNamesBlues() {
    %n = 0;
    %n[$gDevNameColors @ %n] = "0.0 0.0 1.0 1.0";
    %n = (1.0 + %n);
    %n[$gDevNameColors @ %n] = "0.2 0.2 0.9 1.0";
    %n = (1.0 + %n);
    %n[$gDevNameColors @ %n] = "0.3 0.3 0.8 1.0";
    %n = (1.0 + %n);
    %n[$gDevNameColors @ %n] = "0.4 0.4 0.8 1.0";
    %n = (1.0 + %n);
    %n[$gDevNameColors @ %n] = "0.5 0.5 0.9 1.0";
    %n = (1.0 + %n);
    %n[$gDevNameColors @ %n] = "0.6 0.6 0.9 1.0";
    %n = (1.0 + %n);
    %n[$gDevNameColors @ %n] = "0.8 0.8 0.9 1.0";
    %n = (1.0 + %n);
    %n[$gDevNameColors @ %n] = "0.9 0.9 1.0 1.0";
    %n = (1.0 + %n);
    %n[$gDevNameColors @ %n] = "1.0 1.0 1.0 1.0";
    %n = (1.0 + %n);
    numNameColors = %n @ TheShapeNameHud;
};
function devAvatarNamesGreens() {
    %n = 0;
    %n[$gDevNameColors @ %n] = "0.0 0.6 0.0 1.0";
    %n = (1.0 + %n);
    %n[$gDevNameColors @ %n] = "0.0 0.7 0.0 1.0";
    %n = (1.0 + %n);
    %n[$gDevNameColors @ %n] = "0.0 0.8 0.0 1.0";
    %n = (1.0 + %n);
    %n[$gDevNameColors @ %n] = "0.0 0.9 0.0 1.0";
    %n = (1.0 + %n);
    %n[$gDevNameColors @ %n] = "0.3 0.9 0.0 1.0";
    %n = (1.0 + %n);
    %n[$gDevNameColors @ %n] = "0.5 0.9 0.0 1.0";
    %n = (1.0 + %n);
    %n[$gDevNameColors @ %n] = "0.6 0.9 0.0 1.0";
    %n = (1.0 + %n);
    %n[$gDevNameColors @ %n] = "0.7 1.0 0.0 1.0";
    %n = (1.0 + %n);
    %n[$gDevNameColors @ %n] = "0.8 1.0 0.0 1.0";
    %n = (1.0 + %n);
    numNameColors = %n @ TheShapeNameHud;
};
function devAvatarNamesIcons() {
    %n = 0;
    %n[$gDevNameColors @ %n] = "1.0 1.0 1.0 1.0 platform/client/buttons/star_d";
    %n = (1.0 + %n);
    %n[$gDevNameColors @ %n] = "1.0 1.0 1.0 1.0 platform/client/buttons/star_h";
    %n = (1.0 + %n);
    %n[$gDevNameColors @ %n] = "1.0 1.0 1.0 1.0 platform/client/buttons/star_i";
    %n = (1.0 + %n);
    %n[$gDevNameColors @ %n] = "1.0 1.0 1.0 1.0 platform/client/buttons/star_n";
    %n = (1.0 + %n);
    %n[$gDevNameColors @ %n] = "1.0 1.0 1.0 1.0 platform/client/buttons/pending_d";
    %n = (1.0 + %n);
    %n[$gDevNameColors @ %n] = "1.0 1.0 1.0 1.0 platform/client/buttons/pending_h";
    %n = (1.0 + %n);
    %n[$gDevNameColors @ %n] = "1.0 1.0 1.0 1.0 platform/client/buttons/pending_i";
    %n = (1.0 + %n);
    %n[$gDevNameColors @ %n] = "1.0 1.0 1.0 1.0 platform/client/buttons/pending_n";
    %n = (1.0 + %n);
    %n[$gDevNameColors @ %n] = "1.0 1.0 1.0 1.0 platform/client/buttons/hud_scores_d";
    %n = (1.0 + %n);
    %n[$gDevNameColors @ %n] = "1.0 1.0 1.0 1.0 platform/client/buttons/hud_scores_h";
    %n = (1.0 + %n);
    %n[$gDevNameColors @ %n] = "1.0 1.0 1.0 1.0 platform/client/buttons/hud_scores_i";
    %n = (1.0 + %n);
    %n[$gDevNameColors @ %n] = "1.0 1.0 1.0 1.0 platform/client/buttons/hud_scores_n";
    %n = (1.0 + %n);
    %n[$gDevNameColors @ %n] = "1.0 1.0 1.0 1.0 platform/client/buttons/buddies_d";
    %n = (1.0 + %n);
    %n[$gDevNameColors @ %n] = "1.0 1.0 1.0 1.0 platform/client/buttons/buddies_h";
    %n = (1.0 + %n);
    %n[$gDevNameColors @ %n] = "1.0 1.0 1.0 1.0 platform/client/buttons/buddies_i";
    %n = (1.0 + %n);
    %n[$gDevNameColors @ %n] = "1.0 1.0 1.0 1.0 platform/client/buttons/buddies_n";
    %n = (1.0 + %n);
    %n[$gDevNameColors @ %n] = "1.0 1.0 1.0 1.0 platform/client/ui/buildingDir_heart_blue";
    %n = (1.0 + %n);
    %n[$gDevNameColors @ %n] = "1.0 1.0 1.0 1.0 platform/client/ui/buildingDir_heart_green";
    %n = (1.0 + %n);
    %n[$gDevNameColors @ %n] = "1.0 1.0 1.0 1.0 platform/client/ui/buildingDir_heart_white";
    %n = (1.0 + %n);
    %n[$gDevNameColors @ %n] = "1.0 1.0 1.0 1.0 platform/client/ui/friendsHud_lightning_h";
    %n = (1.0 + %n);
    %n[$gDevNameColors @ %n] = "1.0 1.0 1.0 1.0 platform/client/ui/friendsHud_lightning_i";
    %n = (1.0 + %n);
    %n[$gDevNameColors @ %n] = "1.0 1.0 1.0 1.0 platform/client/ui/friendsHud_lightning_n";
    %n = (1.0 + %n);
    numNameColors = %n @ TheShapeNameHud;
};
function devAvatarNamesColorsAndIcons() {
    %n = 0;
    %n[$gDevNameColors @ %n] = "0.0 0.0 1.0 1.0 platform/client/buttons/star_d";
    %n = (1.0 + %n);
    %n[$gDevNameColors @ %n] = "0.2 0.2 0.9 1.0 platform/client/buttons/star_h";
    %n = (1.0 + %n);
    %n[$gDevNameColors @ %n] = "0.3 0.3 0.8 1.0 platform/client/buttons/star_i";
    %n = (1.0 + %n);
    %n[$gDevNameColors @ %n] = "0.4 0.4 0.8 1.0 platform/client/buttons/star_n";
    %n = (1.0 + %n);
    %n[$gDevNameColors @ %n] = "0.5 0.5 0.9 1.0 platform/client/buttons/pending_d";
    %n = (1.0 + %n);
    %n[$gDevNameColors @ %n] = "0.6 0.6 0.9 1.0 platform/client/buttons/pending_h";
    %n = (1.0 + %n);
    %n[$gDevNameColors @ %n] = "0.8 0.8 0.9 1.0 platform/client/buttons/pending_i";
    %n = (1.0 + %n);
    %n[$gDevNameColors @ %n] = "0.9 0.9 1.0 1.0 platform/client/buttons/pending_n";
    %n = (1.0 + %n);
    %n[$gDevNameColors @ %n] = "1.0 1.0 1.0 1.0 platform/client/buttons/hud_scores_d";
    %n = (1.0 + %n);
    %n[$gDevNameColors @ %n] = "0.0 0.6 0.0 1.0 platform/client/buttons/hud_scores_h";
    %n = (1.0 + %n);
    %n[$gDevNameColors @ %n] = "0.0 0.7 0.0 1.0 platform/client/buttons/hud_scores_i";
    %n = (1.0 + %n);
    %n[$gDevNameColors @ %n] = "0.0 0.8 0.0 1.0 platform/client/buttons/hud_scores_n";
    %n = (1.0 + %n);
    %n[$gDevNameColors @ %n] = "0.0 0.9 0.0 1.0 platform/client/buttons/buddies_d";
    %n = (1.0 + %n);
    %n[$gDevNameColors @ %n] = "0.3 0.9 0.0 1.0 platform/client/buttons/buddies_h";
    %n = (1.0 + %n);
    %n[$gDevNameColors @ %n] = "0.5 0.9 0.0 1.0 platform/client/buttons/buddies_i";
    %n = (1.0 + %n);
    %n[$gDevNameColors @ %n] = "0.6 0.9 0.0 1.0 platform/client/buttons/buddies_n";
    %n = (1.0 + %n);
    %n[$gDevNameColors @ %n] = "0.7 1.0 0.0 1.0 platform/client/ui/buildingDir_heart_blue";
    %n = (1.0 + %n);
    %n[$gDevNameColors @ %n] = "0.8 1.0 0.0 1.0 platform/client/ui/buildingDir_heart_green";
    %n = (1.0 + %n);
    %n[$gDevNameColors @ %n] = "1.0 1.0 1.0 1.0 platform/client/ui/buildingDir_heart_white";
    %n = (1.0 + %n);
    %n[$gDevNameColors @ %n] = "1.0 1.0 1.0 1.0 platform/client/ui/friendsHud_lightning_h";
    %n = (1.0 + %n);
    %n[$gDevNameColors @ %n] = "1.0 1.0 1.0 1.0 platform/client/ui/friendsHud_lightning_i";
    %n = (1.0 + %n);
    %n[$gDevNameColors @ %n] = "1.0 1.0 1.0 1.0 platform/client/ui/friendsHud_lightning_n";
    %n = (1.0 + %n);
    numNameColors = %n @ TheShapeNameHud;
};
$gAnimTestNum = 0;
$gAnimTestCur = 0;
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "bcidl1a";
$gAnimTestNum = (1.0 + $gAnimTestNum);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "bwlkf1";
$gAnimTestNum = (1.0 + $gAnimTestNum);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "iang";
$gAnimTestNum = (1.0 + $gAnimTestNum);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "iwlkf1";
$gAnimTestNum = (1.0 + $gAnimTestNum);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "narcadeidl";
$gAnimTestNum = (1.0 + $gAnimTestNum);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "nbassr1e";
$gAnimTestNum = (1.0 + $gAnimTestNum);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "nblext";
$gAnimTestNum = (1.0 + $gAnimTestNum);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "nblidl1";
$gAnimTestNum = (1.0 + $gAnimTestNum);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "nbrshft";
$gAnimTestNum = (1.0 + $gAnimTestNum);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "nbtwlkl";
$gAnimTestNum = (1.0 + $gAnimTestNum);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "nclbent";
$gAnimTestNum = (1.0 + $gAnimTestNum);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "nclbext";
$gAnimTestNum = (1.0 + $gAnimTestNum);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "nclbidl1";
$gAnimTestNum = (1.0 + $gAnimTestNum);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "ncutout01";
$gAnimTestNum = (1.0 + $gAnimTestNum);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "ncutout02";
$gAnimTestNum = (1.0 + $gAnimTestNum);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "nd2step";
$gAnimTestNum = (1.0 + $gAnimTestNum);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "nd2stepx";
$gAnimTestNum = (1.0 + $gAnimTestNum);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "ndhtoe";
$gAnimTestNum = (1.0 + $gAnimTestNum);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "ndlnwit";
$gAnimTestNum = (1.0 + $gAnimTestNum);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "ndrumr1e";
$gAnimTestNum = (1.0 + $gAnimTestNum);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "ndshfle";
$gAnimTestNum = (1.0 + $gAnimTestNum);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "ndslpsld";
$gAnimTestNum = (1.0 + $gAnimTestNum);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "ndvstepb";
$gAnimTestNum = (1.0 + $gAnimTestNum);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "ndwlkit";
$gAnimTestNum = (1.0 + $gAnimTestNum);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "ndxhop";
$gAnimTestNum = (1.0 + $gAnimTestNum);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "ngo01";
$gAnimTestNum = (1.0 + $gAnimTestNum);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "ngo02";
$gAnimTestNum = (1.0 + $gAnimTestNum);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "ngo03";
$gAnimTestNum = (1.0 + $gAnimTestNum);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "ngo04";
$gAnimTestNum = (1.0 + $gAnimTestNum);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "ngo05";
$gAnimTestNum = (1.0 + $gAnimTestNum);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "ngo06";
$gAnimTestNum = (1.0 + $gAnimTestNum);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "ngo07";
$gAnimTestNum = (1.0 + $gAnimTestNum);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "ngo08";
$gAnimTestNum = (1.0 + $gAnimTestNum);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "ngtrglr10a";
$gAnimTestNum = (1.0 + $gAnimTestNum);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "ngtrglr11a";
$gAnimTestNum = (1.0 + $gAnimTestNum);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "ngtrglr12a";
$gAnimTestNum = (1.0 + $gAnimTestNum);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "ngtrglr13a";
$gAnimTestNum = (1.0 + $gAnimTestNum);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "ngtrglr14a";
$gAnimTestNum = (1.0 + $gAnimTestNum);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "ngtrglr15b";
$gAnimTestNum = (1.0 + $gAnimTestNum);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "ngtrglr16b";
$gAnimTestNum = (1.0 + $gAnimTestNum);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "ngtrglr17b";
$gAnimTestNum = (1.0 + $gAnimTestNum);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "ngtrglr18b";
$gAnimTestNum = (1.0 + $gAnimTestNum);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "ngtrglr19b";
$gAnimTestNum = (1.0 + $gAnimTestNum);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "ngtrglr1e";
$gAnimTestNum = (1.0 + $gAnimTestNum);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "ngtrglr20b";
$gAnimTestNum = (1.0 + $gAnimTestNum);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "ngtrglr2e";
$gAnimTestNum = (1.0 + $gAnimTestNum);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "ngtrglr3e";
$gAnimTestNum = (1.0 + $gAnimTestNum);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "ngtrglr4e";
$gAnimTestNum = (1.0 + $gAnimTestNum);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "ngtrglr5e";
$gAnimTestNum = (1.0 + $gAnimTestNum);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "ngtrglr6e";
$gAnimTestNum = (1.0 + $gAnimTestNum);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "ngtrglr7e";
$gAnimTestNum = (1.0 + $gAnimTestNum);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "ngtrglr8a";
$gAnimTestNum = (1.0 + $gAnimTestNum);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "ngtrglr9a";
$gAnimTestNum = (1.0 + $gAnimTestNum);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "ngtrglridl1";
$gAnimTestNum = (1.0 + $gAnimTestNum);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "ngtrglrjmp01";
$gAnimTestNum = (1.0 + $gAnimTestNum);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "ngtrglrside01";
$gAnimTestNum = (1.0 + $gAnimTestNum);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "ngtrglrwlkb01";
$gAnimTestNum = (1.0 + $gAnimTestNum);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "ngtrglrwlkf01";
$gAnimTestNum = (1.0 + $gAnimTestNum);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "ngtrgr10e";
$gAnimTestNum = (1.0 + $gAnimTestNum);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "ngtrgr11e";
$gAnimTestNum = (1.0 + $gAnimTestNum);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "ngtrgr12a";
$gAnimTestNum = (1.0 + $gAnimTestNum);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "ngtrgr13a";
$gAnimTestNum = (1.0 + $gAnimTestNum);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "ngtrgr14a";
$gAnimTestNum = (1.0 + $gAnimTestNum);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "ngtrgr15a";
$gAnimTestNum = (1.0 + $gAnimTestNum);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "ngtrgr17a";
$gAnimTestNum = (1.0 + $gAnimTestNum);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "ngtrgr18a";
$gAnimTestNum = (1.0 + $gAnimTestNum);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "ngtrgr1e";
$gAnimTestNum = (1.0 + $gAnimTestNum);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "ngtrgr22a";
$gAnimTestNum = (1.0 + $gAnimTestNum);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "ngtrgr25b";
$gAnimTestNum = (1.0 + $gAnimTestNum);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "ngtrgr28b";
$gAnimTestNum = (1.0 + $gAnimTestNum);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "ngtrgr29b";
$gAnimTestNum = (1.0 + $gAnimTestNum);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "ngtrgr2e";
$gAnimTestNum = (1.0 + $gAnimTestNum);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "ngtrgr3e";
$gAnimTestNum = (1.0 + $gAnimTestNum);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "ngtrgr5e";
$gAnimTestNum = (1.0 + $gAnimTestNum);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "ngtrgr6e";
$gAnimTestNum = (1.0 + $gAnimTestNum);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "ngtrgr7e";
$gAnimTestNum = (1.0 + $gAnimTestNum);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "ngtrgr8e";
$gAnimTestNum = (1.0 + $gAnimTestNum);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "ngtrgr9e";
$gAnimTestNum = (1.0 + $gAnimTestNum);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "nhead_LR";
$gAnimTestNum = (1.0 + $gAnimTestNum);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "nhead_UD";
$gAnimTestNum = (1.0 + $gAnimTestNum);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "nhead_ss";
$gAnimTestNum = (1.0 + $gAnimTestNum);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "nhi5ee";
$gAnimTestNum = (1.0 + $gAnimTestNum);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "nhi5er";
$gAnimTestNum = (1.0 + $gAnimTestNum);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "nlsnext";
$gAnimTestNum = (1.0 + $gAnimTestNum);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "nlyidl1";
$gAnimTestNum = (1.0 + $gAnimTestNum);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "nmcheer";
$gAnimTestNum = (1.0 + $gAnimTestNum);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "nmcheer1";
$gAnimTestNum = (1.0 + $gAnimTestNum);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "nmchug";
$gAnimTestNum = (1.0 + $gAnimTestNum);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "nmdip";
$gAnimTestNum = (1.0 + $gAnimTestNum);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "nmdrink";
$gAnimTestNum = (1.0 + $gAnimTestNum);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "nmflrt";
$gAnimTestNum = (1.0 + $gAnimTestNum);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "nmlol";
$gAnimTestNum = (1.0 + $gAnimTestNum);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "nmlowdnc";
$gAnimTestNum = (1.0 + $gAnimTestNum);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "nmmic";
$gAnimTestNum = (1.0 + $gAnimTestNum);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "nmomg";
$gAnimTestNum = (1.0 + $gAnimTestNum);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "nmpitch";
$gAnimTestNum = (1.0 + $gAnimTestNum);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "nmpiv";
$gAnimTestNum = (1.0 + $gAnimTestNum);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "nmroll";
$gAnimTestNum = (1.0 + $gAnimTestNum);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "nmsitbend";
$gAnimTestNum = (1.0 + $gAnimTestNum);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "nmsitpiv";
$gAnimTestNum = (1.0 + $gAnimTestNum);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "nmtoast";
$gAnimTestNum = (1.0 + $gAnimTestNum);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "nmupdnc";
$gAnimTestNum = (1.0 + $gAnimTestNum);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "nmupkis";
$gAnimTestNum = (1.0 + $gAnimTestNum);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "nmupyaw";
$gAnimTestNum = (1.0 + $gAnimTestNum);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "nmwave";
$gAnimTestNum = (1.0 + $gAnimTestNum);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "nmwlk";
$gAnimTestNum = (1.0 + $gAnimTestNum);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "npwbbattack";
$gAnimTestNum = (1.0 + $gAnimTestNum);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "npwidle";
$gAnimTestNum = (1.0 + $gAnimTestNum);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "npwjabattack";
$gAnimTestNum = (1.0 + $gAnimTestNum);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "npwjabdefend";
$gAnimTestNum = (1.0 + $gAnimTestNum);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "npwjmp";
$gAnimTestNum = (1.0 + $gAnimTestNum);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "npwlongstun";
$gAnimTestNum = (1.0 + $gAnimTestNum);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "npwpowerattack";
$gAnimTestNum = (1.0 + $gAnimTestNum);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "npwpowerdefend";
$gAnimTestNum = (1.0 + $gAnimTestNum);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "npwsde";
$gAnimTestNum = (1.0 + $gAnimTestNum);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "npwshortstun";
$gAnimTestNum = (1.0 + $gAnimTestNum);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "npwtaunt01";
$gAnimTestNum = (1.0 + $gAnimTestNum);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "npwtaunt02";
$gAnimTestNum = (1.0 + $gAnimTestNum);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "npwwlkb";
$gAnimTestNum = (1.0 + $gAnimTestNum);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "npwwlkf";
$gAnimTestNum = (1.0 + $gAnimTestNum);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "nreachdown";
$gAnimTestNum = (1.0 + $gAnimTestNum);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "nrlidl1";
$gAnimTestNum = (1.0 + $gAnimTestNum);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "nrsbbeaux";
$gAnimTestNum = (1.0 + $gAnimTestNum);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "nrsbloop";
$gAnimTestNum = (1.0 + $gAnimTestNum);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "nrsbreaux";
$gAnimTestNum = (1.0 + $gAnimTestNum);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "nrsbsham";
$gAnimTestNum = (1.0 + $gAnimTestNum);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "nsitlsn";
$gAnimTestNum = (1.0 + $gAnimTestNum);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "nsittlk";
$gAnimTestNum = (1.0 + $gAnimTestNum);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "nspinbottle";
$gAnimTestNum = (1.0 + $gAnimTestNum);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "nssext";
$gAnimTestNum = (1.0 + $gAnimTestNum);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "nsumobbattack";
$gAnimTestNum = (1.0 + $gAnimTestNum);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "nsumoidle";
$gAnimTestNum = (1.0 + $gAnimTestNum);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "nsumojabattack";
$gAnimTestNum = (1.0 + $gAnimTestNum);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "nsumojabdefend";
$gAnimTestNum = (1.0 + $gAnimTestNum);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "nsumojmp";
$gAnimTestNum = (1.0 + $gAnimTestNum);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "nsumolongstun";
$gAnimTestNum = (1.0 + $gAnimTestNum);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "nsumopowerattack";
$gAnimTestNum = (1.0 + $gAnimTestNum);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "nsumopowerdefend";
$gAnimTestNum = (1.0 + $gAnimTestNum);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "nsumoshortstun";
$gAnimTestNum = (1.0 + $gAnimTestNum);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "nsumostun";
$gAnimTestNum = (1.0 + $gAnimTestNum);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "nsumotaunt01";
$gAnimTestNum = (1.0 + $gAnimTestNum);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "nsumotaunt02";
$gAnimTestNum = (1.0 + $gAnimTestNum);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "nsumowlkb";
$gAnimTestNum = (1.0 + $gAnimTestNum);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "nsumowlkf";
$gAnimTestNum = (1.0 + $gAnimTestNum);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "nsumowlks";
$gAnimTestNum = (1.0 + $gAnimTestNum);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "ntalk";
$gAnimTestNum = (1.0 + $gAnimTestNum);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "ntapglass";
$gAnimTestNum = (1.0 + $gAnimTestNum);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "nthink";
$gAnimTestNum = (1.0 + $gAnimTestNum);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "ntyidl1a";
$gAnimTestNum = (1.0 + $gAnimTestNum);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "ntyrsml";
$gAnimTestNum = (1.0 + $gAnimTestNum);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "ntyrturn";
$gAnimTestNum = (1.0 + $gAnimTestNum);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "nvom";
$gAnimTestNum = (1.0 + $gAnimTestNum);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "nxrcst";
$gAnimTestNum = (1.0 + $gAnimTestNum);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "nzidl1";
$gAnimTestNum = (1.0 + $gAnimTestNum);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "nzwlk";
$gAnimTestNum = (1.0 + $gAnimTestNum);
$gAnimTestNum[$gAnimTestAnim @ $gAnimTestNum] = "ygunsling";
$gAnimTestNum = (1.0 + $gAnimTestNum);
function animTest_Again() {
    %anim = $player.getGender() @ $gAnimTestCur[$gAnimTestAnim @ $gAnimTestCur];
    $player.playAnim(%anim);
    echo(getScopeName() @ " " @ "-" @ " " @ %anim);
};
function animTest_Next() {
    $gAnimTestCur = (1.0 + $gAnimTestCur);
    if (($gAnimTestNum >= $gAnimTestCur)) {
        $gAnimTestCur = 0;
    }
    animTest_Again();
};
function animTest_Prev() {
    $gAnimTestCur = (1.0 - $gAnimTestCur);
    if ((0.0 < $gAnimTestCur)) {
        $gAnimTestCur = (1.0 - $gAnimTestCur);
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
    echo("driftSim is" @ " " @ (0.001 * %driftSim));
    echo("elapsed real seconds   =" @ " " @ (0.001 * %elapsedReal));
    echo("elapsed sim  seconds   =" @ " " @ (0.001 * %elapsedSim));
    echo("driftSim  /elapsedReal =" @ " " @ (%elapsedReal / %driftSim));
    echo("elapsedSim/elapsedReal =" @ " " @ (%elapsedReal / %elapsedSim));
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
    %request.bindClassName("UniformManagerRequest");
    %request.setURL(%url);
    %request.callbackHandler = %callbackHandler;
    %request.start();
    return %request;
};
function dev_testURLEncode() {
    %n = 0;
    if ((256.0 < %n)) {
        %c = intToChar(%n);
        %d = urlEncode(%c);
        %e = urlDecode(%d);
        echo(formatInt("%3d", %n) @ " " @ %c @ " " @ "->" @ " " @ %d);
        echo(formatInt("%3d", %n) @ " " @ %e @ " " @ "<-" @ " " @ %d);
        if ((20.0 > %n)) {
            %gnarly = %gnarly @ %c;
        }
        %n = (1.0 + %n);
    }
    echo(%gnarly);
    return %gnarly;
};
function dev_ensureRandomItemManager() {
    if (!(isObject(gRandomItemManager))) {
        new ScriptObject(gRandomItemManager);
        if (isObject(MissionCleanup)) {
            MissionCleanup.add(gRandomItemManager);
            %request.numItems = 0 @ gRandomItemManager;
        }
    }
};
function dev_clearRandomItems() {
    dev_ensureRandomItemManager();
    %request.numItems = 0 @ gRandomItemManager;
};
function dev_declareRandomItem(%itemName, %itemWeight) {
    dev_ensureRandomItemManager();
    %n = %request.numItems;
    gRandomItemManager;
    %request.itemName = %itemName @ %n @ gRandomItemManager;
    %request.itemWeight = %itemWeight @ %n @ gRandomItemManager;
    %request.weightsNeedNormalizing = 1 @ gRandomItemManager;
    %request.numItems = (gRandomItemManager + %request.numItems);
    1.0;
};
function dev_getRandomItem() {
    dev_ensureRandomItemManager();
    if (%request.weightsNeedNormalizing) {
        %request.weightsNeedNormalizing = 0 @ gRandomItemManager;
        gRandomItemManager;
        %totalWeight = 0;
        %n = 0;
        if ((%request.numItems < %n)) {
            %request.itemWeightCumulative = (%n @ gRandomItemManager + %request.itemWeight) @ %n @ gRandomItemManager;
            %totalWeight;
            %totalWeight = (%request.itemWeight + %totalWeight);
            %n @ gRandomItemManager;
            %n = (1.0 + %n);
            gRandomItemManager;
        }
        %request.totalWeight = %totalWeight @ gRandomItemManager;
        (%request.numItems < %n);
    }
    %rand = getRandom(0, (gRandomItemManager - %request.totalWeight));
    1.0;
    %n = 0;
    gRandomItemManager;
    if ((%request.numItems < %n)) {
        if ((%request.itemWeightCumulative < %rand)) {
            return %request.itemName;
        }
        %n = (1.0 + %n);
    }
    error("something went wrong.");
    return "";
};
function dev_testRandomItems(%iterations) {
    dev_clearRandomItems();
    dev_declareRandomItem("A", 1);
    dev_declareRandomItem("B", 1);
    dev_declareRandomItem("C", 1);
    dev_declareRandomItem("D", 3);
    %n = 0;
    if ((%iterations < %n)) {
        %item = dev_getRandomItem();
        %item[%totals @ %item] = (1.0 + %item[%totals @ %item]);
        %n = (1.0 + %n);
    }
    echo("A -" @ " " @ %n[%totals @ "A"]);
    echo((%iterations < %n) @ "B -" @ " ");
    echo("C -" @ " ");
    echo("D -" @ " ");
};
function SimObject::getTypeStrings(%this) {
    %types = "";
    %mask = %this.getType();
    %types = %types @ ($TypeMasks::StaticObjectType & %mask) ? "StaticObjectType " : "";
    %types = %types @ ($TypeMasks::EnvironmentObjectType & %mask) ? "EnvironmentObjectType " : "";
    %types = %types @ ($TypeMasks::TerrainObjectType & %mask) ? "TerrainObjectType " : "";
    %types = %types @ ($TypeMasks::InteriorObjectType & %mask) ? "InteriorObjectType " : "";
    %types = %types @ ($TypeMasks::WaterObjectType & %mask) ? "WaterObjectType " : "";
    %types = %types @ ($TypeMasks::TriggerObjectType & %mask) ? "TriggerObjectType " : "";
    %types = %types @ ($TypeMasks::AntiPortalObjectType & %mask) ? "AntiPortalObjectType " : "";
    %types = %types @ ($TypeMasks::ZoneBoxObjectType & %mask) ? "ZoneBoxObjectType " : "";
    %types = %types @ ($TypeMasks::MarkerObjectType & %mask) ? "MarkerObjectType " : "";
    %types = %types @ ($TypeMasks::GameBaseObjectType & %mask) ? "GameBaseObjectType " : "";
    %types = %types @ ($TypeMasks::ShapeBaseObjectType & %mask) ? "ShapeBaseObjectType " : "";
    %types = %types @ ($TypeMasks::CameraObjectType & %mask) ? "CameraObjectType " : "";
    %types = %types @ ($TypeMasks::StaticShapeObjectType & %mask) ? "StaticShapeObjectType " : "";
    %types = %types @ ($TypeMasks::PlayerObjectType & %mask) ? "PlayerObjectType " : "";
    %types = %types @ ($TypeMasks::ItemObjectType & %mask) ? "ItemObjectType " : "";
    %types = %types @ ($TypeMasks::VehicleObjectType & %mask) ? "VehicleObjectType " : "";
    %types = %types @ ($TypeMasks::VehicleBlockerObjectType & %mask) ? "VehicleBlockerObjectType " : "";
    %types = %types @ ($TypeMasks::ProjectileObjectType & %mask) ? "ProjectileObjectType " : "";
    %types = %types @ ($TypeMasks::ExplosionObjectType & %mask) ? "ExplosionObjectType " : "";
    %types = %types @ ($TypeMasks::CorpseObjectType & %mask) ? "CorpseObjectType " : "";
    %types = %types @ ($TypeMasks::DebrisObjectType & %mask) ? "DebrisObjectType " : "";
    %types = %types @ ($TypeMasks::PhysicalZoneObjectType & %mask) ? "PhysicalZoneObjectType " : "";
    %types = %types @ ($TypeMasks::StaticTSObjectType & %mask) ? "StaticTSObjectType " : "";
    %types = %types @ ($TypeMasks::StaticRenderedObjectType & %mask) ? "StaticRenderedObjectType " : "";
    %types = %types @ ($TypeMasks::DamagableItemObjectType & %mask) ? "DamagableItemObjectType " : "";
    %types = %types @ ($TypeMasks::AdvertObjectType & %mask) ? "AdvertObjectType " : "";
    %types = %types @ ($TypeMasks::ConversationObjectType & %mask) ? "ConversationObjectType " : "";
    %types = %types @ ($TypeMasks::UsableObjectType & %mask) ? "UsableObjectType " : "";
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
        $gTwitterTextCount = (1.0 + $gTwitterTextCount);
        %text = %text @ " " @ $gTwitterTextCount;
    }
    $gTwitterText = %text;
    $gTwitterTextCount = 1;
    %request = new ""();;
    URLPostObject;
    %request.setURL("https://twitter.com/statuses/update.xml");
    %request.setBodyParam("status", %text);
    %request.setUserNameAndPassword("elenzil:etspass777");
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
    $screenShotNum = (1.0 + $screenShotNum);
    %uploader = new ""();;
    URLPostObject;
    %uploader.setProgress(1);
    %uploader.setURL(%url);
    %uploader.setUserNameAndPassword(%userName @ ":" @ %password);
    %uploader.setPostFile("image", %fileName);
    %uploader.setCustomHeaders("Expect:");
    if (%tile) {
        %uploader.setBodyParam("tile", "true");
    }
    if (!(%uploader.start())) {
        error(getScopeName() @ " " @ "- Unable to upload photo." @ " " @ %fileName @ " " @ %url @ " " @ getTrace());
    }
    return %uploader;
};
