function fakeBuddyInfo(%friends, %faves, %fans)
{
    if ($ServerName $= "")
    {
        $ServerName = "Raijuku";
    }
    safeEnsureScriptObjectWithInit("StringMap", "UserListFriends", "{ ignoreCase = true; }");
    safeEnsureScriptObjectWithInit("StringMap", "UserListFavorites", "{ ignoreCase = true; }");
    safeEnsureScriptObjectWithInit("StringMap", "UserListFans", "{ ignoreCase = true; }");
    UserListFriends.deleteValuesAsObjects();
    UserListFavorites.deleteValuesAsObjects();
    UserListFans.deleteValuesAsObjects();
    %n = 0;
    while (%n < %friends)
    {
        %record = getFakeBuddyRecord("fakefriend" SPC formatInt("%0.4d", %friends - %n));
        UserListFriends.put(%record.name, %record);
        %n = %n + 1;
    }
    %n = 0;
    while (%n < %faves)
    {
        %record = getFakeBuddyRecord("fakeFave" SPC formatInt("%0.4d", %n));
        UserListFavorites.put(%record.name, %record);
        %n = %n + 1;
    }
    %n = 0;
    while (%n < %fans)
    {
        %record = getFakeBuddyRecord("fakeFan" SPC formatInt("%0.4d", %n));
        UserListFans.put(%record.name, %record);
        %n = %n + 1;
    }
}

function getFakeBuddyRecord(%name)
{
    %words = "a A b B c C";
    %record = new ScriptObject();
    %record.loggedIn = getRandom(0, 1) ? 1 : 0;
    %record.name = getRandomWord(%words) SPC %name;
    %record.serverName = %record.loggedIn ? "Raijuku" : "";
    %record.roles = 0;
    %record.isIdle = getRandom(0, 1) ? 1 : 0;
    %record.isNPC = 0;
    %record.csn = "rj";
    return %record;
}
function dev_TestMLText(%onOrOff, %method)
{
    %numLines = 40;
    %numCols = 20;
    %lineLong = "als djalsk jdlaksj dlakjs dl;kaj sd;lahf;ha;fkqehrk;jhqrkljhals djalsk jdlaksj dlakjs dl;kaj sd;lahf;ha;fkqehrk;jhqrkljhals djalsk jdlaksj dlakjs dl;kaj sd;lahf;ha;fkqehrk;jhqrkljhq wekljrh qkwjrh qkjwh kqjwhr kqjrwh";
    %lineText[0] = "<color:ffff33>" @ %lineLong ;
    %lineText[1] = "<color:22ff33>" @ %lineLong ;
    %lineText[2] = %lineLong ;
    %lineText[3] = "";
    %lineText[4] = "platform/client/ui/evilbunny";
    if (isObject(geMLTest))
    {
        geMLTest.delete();
    }
    new GuiMLTextCtrl(geMLTest)
    {
        extent = playGui.getExtent();
        profile = ETSNonModalProfile;
    };
    playGui.add(geMLTest);
    if (isObject(geMLTestArray))
    {
        geMLTestArray.delete();
    }
    new GuiArray2Ctrl(geMLTestArray)
    {
        extent = playGui.getExtent();
        spacing = 0;
        inRows = 0;
        profile = ETSNonModalProfile;
    };
    playGui.add(geMLTestArray);
    if (%method == 0)
    {
        %text = "";
        if (%onOrOff)
        {
            %n = 0;
            while (%n < %numLines)
            {
                %text = %text @ %lineText[%method] @ "\n";
                %n = %n + 1;
            }
        }
        geMLTest.setText(%text);
    }
    if (%method == 1)
    {
        if (%onOrOff)
        {
            geMLTestArray.childrenExtent = getWord(playGui.getExtent(), 0) / 1 SPC 16;
            geMLTestArray.numRowsOrCols = 1;
            geMLTestArray.childrenClassName = "GuiMLTextCtrl";
            geMLTestArray.setNumChildren(%numLines);
            %n = 0;
            while (%n < %numLines)
            {
                %child = geMLTestArray.getObject(%n);
                %child.profile = ETSNonModalProfile;
                %child.setText(%lineText[%method]);
                %n = %n + 1;
            }
        }
    }
    if (%method == 2)
    {
        if (%onOrOff)
        {
            geMLTestArray.childrenExtent = getWord(playGui.getExtent(), 0) / 1 SPC 16;
            geMLTestArray.numRowsOrCols = 1;
            geMLTestArray.childrenClassName = "GuiTextCtrl";
            geMLTestArray.setNumChildren(%numLines);
            %n = 0;
            while (%n < %numLines)
            {
                %child = geMLTestArray.getObject(%n);
                %child.profile = ETSNonModalProfile;
                %child.setText(%lineText[%method]);
                %n = %n + 1;
            }
        }
    }
    if (%method == 3)
    {
        if (%onOrOff)
        {
            geMLTestArray.childrenExtent = getWord(playGui.getExtent(), 0) / %numCols SPC 16;
            geMLTestArray.numRowsOrCols = %numCols;
            geMLTestArray.childrenClassName = "GuiButtonCtrl";
            geMLTestArray.setNumChildren(%numLines * %numCols);
            %n = 0;
            while (%n < (%numLines * %numCols))
            {
                %child = geMLTestArray.getObject(%n);
                %child.profile = ETSNonModalProfile;
                %child.setText(%lineText[%method]);
                %n = %n + 1;
            }
        }
    }
    if (%method == 4)
    {
        if (%onOrOff)
        {
            geMLTestArray.childrenExtent = getWord(playGui.getExtent(), 0) / %numCols SPC 16;
            geMLTestArray.numRowsOrCols = %numCols;
            geMLTestArray.childrenClassName = "GuiBitmapCtrl";
            geMLTestArray.setNumChildren(%numLines * %numCols);
            %n = 0;
            while (%n < (%numLines * %numCols))
            {
                %child = geMLTestArray.getObject(%n);
                %child.profile = ETSNonModalProfile;
                %child.setBitmap(%lineText[%method]);
                %n = %n + 1;
            }
        }
    }
}

$gClientSideSceneObjectsTimer = "";
$gClientSideSceneObjectsTickNum = 0;
$gClientSideSceneObjectsGroup = "";
function dev_clientSideSceneObjectsTick()
{
    cancel($gClientSideSceneObjectsTimer);
    if (!isObject($gClientSideSceneObjectsGroup))
    {
        $gClientSideSceneObjectsGroup = new SimGroup();
        ServerConnection.add($gClientSideSceneObjectsGroup);
        %a = new StaticShape();
        $gClientSideSceneObjectsGroup.add(%a);
    }
    %windowCoord = Canvas.getCursorPos();
    %startPoint = playGui.unproject(%windowCoord);
    %camTran = playGui.getLastCameraTransform();
    %camPos = getWords(%camTran, 0, 2);
    %camPtVec = VectorSub(%startPoint, %camPos);
    %camPtVec = VectorNormalize(%camPtVec);
    %checkDistance = 200;
    %endPoint = VectorScale(%camPtVec, %checkDistance);
    %endPoint = VectorAdd(%startPoint, %endPoint);
    %possibleColiders = (0 | $TypeMasks::InteriorObjectType) | $TypeMasks::WaterObjectType;
    %result = containerRayCast(%startPoint, %endPoint, %possibleColiders, $player, 1);
    %hitObject = getWord(%result, 0);
    if (isObject(%hitObject))
    {
        %hitPosition = getWords(%result, 1, 3);
    }
    else
    {
        %hitPosition = VectorAdd(%startPoint, VectorScale(%camPtVec, 4));
    }
    %t = $gClientSideSceneObjectsTickNum * 0.1;
    %a = $gClientSideSceneObjectsGroup.getObject(0);
    %a.setTransform(MatrixMultiply(MatrixMultiply(playGui.getLastCameraTransform(), "0 2 0 1 0 0" SPC %t), "0 0 0 1 0" SPC $gClientSideSceneObjectsTickNum * 0));
    %a.setTransform(%hitPosition SPC "0 0 1" SPC %t);
    $gClientSideSceneObjectsTickNum = $gClientSideSceneObjectsTickNum + 1;
    if (($gClientSideSceneObjectsTickNum % 2) == 0)
    {
        %datablock = unitCubeGreyDataBlock;
    }
    else
    {
        %datablock = unitCubeBlueDataBlock;
    }
    $gClientSideSceneObjectsTimer = schedule(100, 0, "dev_clientSideSceneObjectsTick");
    return ;
}
function standardizeWindowAspect()
{
    %standardX = 960;
    %standardY = 544;
    %currentX = getWord($UserPref::Video::Resolution, 0);
    %currentY = getWord($UserPref::Video::Resolution, 1);
    %currentBPP = getWord($UserPref::Video::Resolution, 2);
    %proportionX = %currentX / %standardX;
    %proportionY = %currentY / %standardY;
    if (%proportionX > %proportionY)
    {
        %currentY = %proportionX * %standardY;
    }
    else
    {
        %currentX = %proportionY * %standardX;
    }
    setScreenMode(%currentX, %currentY, %currentBPP, 0);
    return ;
}
function tryArray()
{
    %arrayCtrl = new GuiArray2Ctrl()
    {
        childrenClassName = "GuiButtonCtrl";
        spacing = 10;
    };
    %arrayCtrl.setChildrenExtents("20 40 80 160", 20);
    %arrayCtrl.setNumChildren(20);
    LoginGui.add(%arrayCtrl);
    return ;
}
function tryGuiTable()
{
    if (isObject(erezG))
    {
        erezG.delete();
    }
    %table = new GuiTableCtrl(erezG)
    {
        position = "30 30";
        extent = "400 400";
        visible = 1;
        childrenClassName = "GuiMLTextCtrl";
        spacing = 2;
    };
    LoginGui.add(%table);
    return ;
}
function tryDataTable()
{
    if (isObject(erezD))
    {
        erezD.delete();
    }
    %table = new DataTable(erezD);
    return ;
}
function tryTable()
{
    tryGuiTable();
    tryDataTable();
    erezG.setDataTable(erezD);
    erezD.addColumn("username", "User Names", "string", 100);
    erezD.addColumn("population", "Population", "number", 200);
    erezD.addColumn("online", "Online", "icon", 50);
    erezD.addIconToColumn("online", "online", "platform/client/ui/checkmark_green");
    erezD.addIconToColumn("online", "idle", "platform/client/ui/ellipsis_yellow");
    erezD.addIconToColumn("online", "offline", "platform/client/ui/arrow_red_right");
    erezD.addRows(5);
    erezD.setRowDataByIndex(0, "username" TAB "erez" TAB "erez" NL "population" TAB 30 TAB 30 NL "online" TAB "online" TAB "[ICON]");
    erezD.setRowDataByIndex(1, "username" TAB "ship" TAB "<b>ship" NL "population" TAB 70 TAB 70 NL "online" TAB "offline" TAB "[ICON]");
    erezD.setRowDataByIndex(2, "username" TAB "boat" TAB "<color:ff0000>boat" NL "population" TAB 60 TAB 60 NL "online" TAB "online" TAB "[ICON]");
    erezD.setRowDataByIndex(3, "username" TAB "band" TAB "<clip:40>band</clip>" NL "population" TAB 20 TAB 20 NL "online" TAB "idle" TAB "[ICON]");
    erezD.setRowDataByIndex(4, "username" TAB "dunk" TAB "<color:00ff00>dunk" NL "population" TAB 90 TAB 90 NL "online" TAB "offline" TAB "[ICON]");
    erezD.updateListeners();
    return ;
}
function devAvatarNamesNormal()
{
    TheShapeNameHud.numNameColors = 0;
    return ;
}
function devAvatarNamesBlues()
{
    %n = 0;
    $gDevNameColors[%n] = "0.0 0.0 1.0 1.0";
    %n = %n + 1;
    $gDevNameColors[%n] = "0.2 0.2 0.9 1.0";
    %n = %n + 1;
    $gDevNameColors[%n] = "0.3 0.3 0.8 1.0";
    %n = %n + 1;
    $gDevNameColors[%n] = "0.4 0.4 0.8 1.0";
    %n = %n + 1;
    $gDevNameColors[%n] = "0.5 0.5 0.9 1.0";
    %n = %n + 1;
    $gDevNameColors[%n] = "0.6 0.6 0.9 1.0";
    %n = %n + 1;
    $gDevNameColors[%n] = "0.8 0.8 0.9 1.0";
    %n = %n + 1;
    $gDevNameColors[%n] = "0.9 0.9 1.0 1.0";
    %n = %n + 1;
    $gDevNameColors[%n] = "1.0 1.0 1.0 1.0";
    %n = %n + 1;
    TheShapeNameHud.numNameColors = %n;
    return ;
}
function devAvatarNamesGreens()
{
    %n = 0;
    $gDevNameColors[%n] = "0.0 0.6 0.0 1.0";
    %n = %n + 1;
    $gDevNameColors[%n] = "0.0 0.7 0.0 1.0";
    %n = %n + 1;
    $gDevNameColors[%n] = "0.0 0.8 0.0 1.0";
    %n = %n + 1;
    $gDevNameColors[%n] = "0.0 0.9 0.0 1.0";
    %n = %n + 1;
    $gDevNameColors[%n] = "0.3 0.9 0.0 1.0";
    %n = %n + 1;
    $gDevNameColors[%n] = "0.5 0.9 0.0 1.0";
    %n = %n + 1;
    $gDevNameColors[%n] = "0.6 0.9 0.0 1.0";
    %n = %n + 1;
    $gDevNameColors[%n] = "0.7 1.0 0.0 1.0";
    %n = %n + 1;
    $gDevNameColors[%n] = "0.8 1.0 0.0 1.0";
    %n = %n + 1;
    TheShapeNameHud.numNameColors = %n;
    return ;
}
function devAvatarNamesIcons()
{
    %n = 0;
    $gDevNameColors[%n] = "1.0 1.0 1.0 1.0 platform/client/buttons/star_d";
    %n = %n + 1;
    $gDevNameColors[%n] = "1.0 1.0 1.0 1.0 platform/client/buttons/star_h";
    %n = %n + 1;
    $gDevNameColors[%n] = "1.0 1.0 1.0 1.0 platform/client/buttons/star_i";
    %n = %n + 1;
    $gDevNameColors[%n] = "1.0 1.0 1.0 1.0 platform/client/buttons/star_n";
    %n = %n + 1;
    $gDevNameColors[%n] = "1.0 1.0 1.0 1.0 platform/client/buttons/pending_d";
    %n = %n + 1;
    $gDevNameColors[%n] = "1.0 1.0 1.0 1.0 platform/client/buttons/pending_h";
    %n = %n + 1;
    $gDevNameColors[%n] = "1.0 1.0 1.0 1.0 platform/client/buttons/pending_i";
    %n = %n + 1;
    $gDevNameColors[%n] = "1.0 1.0 1.0 1.0 platform/client/buttons/pending_n";
    %n = %n + 1;
    $gDevNameColors[%n] = "1.0 1.0 1.0 1.0 platform/client/buttons/hud_scores_d";
    %n = %n + 1;
    $gDevNameColors[%n] = "1.0 1.0 1.0 1.0 platform/client/buttons/hud_scores_h";
    %n = %n + 1;
    $gDevNameColors[%n] = "1.0 1.0 1.0 1.0 platform/client/buttons/hud_scores_i";
    %n = %n + 1;
    $gDevNameColors[%n] = "1.0 1.0 1.0 1.0 platform/client/buttons/hud_scores_n";
    %n = %n + 1;
    $gDevNameColors[%n] = "1.0 1.0 1.0 1.0 platform/client/buttons/buddies_d";
    %n = %n + 1;
    $gDevNameColors[%n] = "1.0 1.0 1.0 1.0 platform/client/buttons/buddies_h";
    %n = %n + 1;
    $gDevNameColors[%n] = "1.0 1.0 1.0 1.0 platform/client/buttons/buddies_i";
    %n = %n + 1;
    $gDevNameColors[%n] = "1.0 1.0 1.0 1.0 platform/client/buttons/buddies_n";
    %n = %n + 1;
    $gDevNameColors[%n] = "1.0 1.0 1.0 1.0 platform/client/ui/buildingDir_heart_blue";
    %n = %n + 1;
    $gDevNameColors[%n] = "1.0 1.0 1.0 1.0 platform/client/ui/buildingDir_heart_green";
    %n = %n + 1;
    $gDevNameColors[%n] = "1.0 1.0 1.0 1.0 platform/client/ui/buildingDir_heart_white";
    %n = %n + 1;
    $gDevNameColors[%n] = "1.0 1.0 1.0 1.0 platform/client/ui/friendsHud_lightning_h";
    %n = %n + 1;
    $gDevNameColors[%n] = "1.0 1.0 1.0 1.0 platform/client/ui/friendsHud_lightning_i";
    %n = %n + 1;
    $gDevNameColors[%n] = "1.0 1.0 1.0 1.0 platform/client/ui/friendsHud_lightning_n";
    %n = %n + 1;
    TheShapeNameHud.numNameColors = %n;
    return ;
}
function devAvatarNamesColorsAndIcons()
{
    %n = 0;
    $gDevNameColors[%n] = "0.0 0.0 1.0 1.0 platform/client/buttons/star_d";
    %n = %n + 1;
    $gDevNameColors[%n] = "0.2 0.2 0.9 1.0 platform/client/buttons/star_h";
    %n = %n + 1;
    $gDevNameColors[%n] = "0.3 0.3 0.8 1.0 platform/client/buttons/star_i";
    %n = %n + 1;
    $gDevNameColors[%n] = "0.4 0.4 0.8 1.0 platform/client/buttons/star_n";
    %n = %n + 1;
    $gDevNameColors[%n] = "0.5 0.5 0.9 1.0 platform/client/buttons/pending_d";
    %n = %n + 1;
    $gDevNameColors[%n] = "0.6 0.6 0.9 1.0 platform/client/buttons/pending_h";
    %n = %n + 1;
    $gDevNameColors[%n] = "0.8 0.8 0.9 1.0 platform/client/buttons/pending_i";
    %n = %n + 1;
    $gDevNameColors[%n] = "0.9 0.9 1.0 1.0 platform/client/buttons/pending_n";
    %n = %n + 1;
    $gDevNameColors[%n] = "1.0 1.0 1.0 1.0 platform/client/buttons/hud_scores_d";
    %n = %n + 1;
    $gDevNameColors[%n] = "0.0 0.6 0.0 1.0 platform/client/buttons/hud_scores_h";
    %n = %n + 1;
    $gDevNameColors[%n] = "0.0 0.7 0.0 1.0 platform/client/buttons/hud_scores_i";
    %n = %n + 1;
    $gDevNameColors[%n] = "0.0 0.8 0.0 1.0 platform/client/buttons/hud_scores_n";
    %n = %n + 1;
    $gDevNameColors[%n] = "0.0 0.9 0.0 1.0 platform/client/buttons/buddies_d";
    %n = %n + 1;
    $gDevNameColors[%n] = "0.3 0.9 0.0 1.0 platform/client/buttons/buddies_h";
    %n = %n + 1;
    $gDevNameColors[%n] = "0.5 0.9 0.0 1.0 platform/client/buttons/buddies_i";
    %n = %n + 1;
    $gDevNameColors[%n] = "0.6 0.9 0.0 1.0 platform/client/buttons/buddies_n";
    %n = %n + 1;
    $gDevNameColors[%n] = "0.7 1.0 0.0 1.0 platform/client/ui/buildingDir_heart_blue";
    %n = %n + 1;
    $gDevNameColors[%n] = "0.8 1.0 0.0 1.0 platform/client/ui/buildingDir_heart_green";
    %n = %n + 1;
    $gDevNameColors[%n] = "1.0 1.0 1.0 1.0 platform/client/ui/buildingDir_heart_white";
    %n = %n + 1;
    $gDevNameColors[%n] = "1.0 1.0 1.0 1.0 platform/client/ui/friendsHud_lightning_h";
    %n = %n + 1;
    $gDevNameColors[%n] = "1.0 1.0 1.0 1.0 platform/client/ui/friendsHud_lightning_i";
    %n = %n + 1;
    $gDevNameColors[%n] = "1.0 1.0 1.0 1.0 platform/client/ui/friendsHud_lightning_n";
    %n = %n + 1;
    TheShapeNameHud.numNameColors = %n;
    return ;
}
$gAnimTestNum = 0;
$gAnimTestCur = 0;
$gAnimTestAnim[$gAnimTestNum] = "bcidl1a";
$gAnimTestNum = $gAnimTestNum + 1;
$gAnimTestAnim[$gAnimTestNum] = "bwlkf1";
$gAnimTestNum = $gAnimTestNum + 1;
$gAnimTestAnim[$gAnimTestNum] = "iang";
$gAnimTestNum = $gAnimTestNum + 1;
$gAnimTestAnim[$gAnimTestNum] = "iwlkf1";
$gAnimTestNum = $gAnimTestNum + 1;
$gAnimTestAnim[$gAnimTestNum] = "narcadeidl";
$gAnimTestNum = $gAnimTestNum + 1;
$gAnimTestAnim[$gAnimTestNum] = "nbassr1e";
$gAnimTestNum = $gAnimTestNum + 1;
$gAnimTestAnim[$gAnimTestNum] = "nblext";
$gAnimTestNum = $gAnimTestNum + 1;
$gAnimTestAnim[$gAnimTestNum] = "nblidl1";
$gAnimTestNum = $gAnimTestNum + 1;
$gAnimTestAnim[$gAnimTestNum] = "nbrshft";
$gAnimTestNum = $gAnimTestNum + 1;
$gAnimTestAnim[$gAnimTestNum] = "nbtwlkl";
$gAnimTestNum = $gAnimTestNum + 1;
$gAnimTestAnim[$gAnimTestNum] = "nclbent";
$gAnimTestNum = $gAnimTestNum + 1;
$gAnimTestAnim[$gAnimTestNum] = "nclbext";
$gAnimTestNum = $gAnimTestNum + 1;
$gAnimTestAnim[$gAnimTestNum] = "nclbidl1";
$gAnimTestNum = $gAnimTestNum + 1;
$gAnimTestAnim[$gAnimTestNum] = "ncutout01";
$gAnimTestNum = $gAnimTestNum + 1;
$gAnimTestAnim[$gAnimTestNum] = "ncutout02";
$gAnimTestNum = $gAnimTestNum + 1;
$gAnimTestAnim[$gAnimTestNum] = "nd2step";
$gAnimTestNum = $gAnimTestNum + 1;
$gAnimTestAnim[$gAnimTestNum] = "nd2stepx";
$gAnimTestNum = $gAnimTestNum + 1;
$gAnimTestAnim[$gAnimTestNum] = "ndhtoe";
$gAnimTestNum = $gAnimTestNum + 1;
$gAnimTestAnim[$gAnimTestNum] = "ndlnwit";
$gAnimTestNum = $gAnimTestNum + 1;
$gAnimTestAnim[$gAnimTestNum] = "ndrumr1e";
$gAnimTestNum = $gAnimTestNum + 1;
$gAnimTestAnim[$gAnimTestNum] = "ndshfle";
$gAnimTestNum = $gAnimTestNum + 1;
$gAnimTestAnim[$gAnimTestNum] = "ndslpsld";
$gAnimTestNum = $gAnimTestNum + 1;
$gAnimTestAnim[$gAnimTestNum] = "ndvstepb";
$gAnimTestNum = $gAnimTestNum + 1;
$gAnimTestAnim[$gAnimTestNum] = "ndwlkit";
$gAnimTestNum = $gAnimTestNum + 1;
$gAnimTestAnim[$gAnimTestNum] = "ndxhop";
$gAnimTestNum = $gAnimTestNum + 1;
$gAnimTestAnim[$gAnimTestNum] = "ngo01";
$gAnimTestNum = $gAnimTestNum + 1;
$gAnimTestAnim[$gAnimTestNum] = "ngo02";
$gAnimTestNum = $gAnimTestNum + 1;
$gAnimTestAnim[$gAnimTestNum] = "ngo03";
$gAnimTestNum = $gAnimTestNum + 1;
$gAnimTestAnim[$gAnimTestNum] = "ngo04";
$gAnimTestNum = $gAnimTestNum + 1;
$gAnimTestAnim[$gAnimTestNum] = "ngo05";
$gAnimTestNum = $gAnimTestNum + 1;
$gAnimTestAnim[$gAnimTestNum] = "ngo06";
$gAnimTestNum = $gAnimTestNum + 1;
$gAnimTestAnim[$gAnimTestNum] = "ngo07";
$gAnimTestNum = $gAnimTestNum + 1;
$gAnimTestAnim[$gAnimTestNum] = "ngo08";
$gAnimTestNum = $gAnimTestNum + 1;
$gAnimTestAnim[$gAnimTestNum] = "ngtrglr10a";
$gAnimTestNum = $gAnimTestNum + 1;
$gAnimTestAnim[$gAnimTestNum] = "ngtrglr11a";
$gAnimTestNum = $gAnimTestNum + 1;
$gAnimTestAnim[$gAnimTestNum] = "ngtrglr12a";
$gAnimTestNum = $gAnimTestNum + 1;
$gAnimTestAnim[$gAnimTestNum] = "ngtrglr13a";
$gAnimTestNum = $gAnimTestNum + 1;
$gAnimTestAnim[$gAnimTestNum] = "ngtrglr14a";
$gAnimTestNum = $gAnimTestNum + 1;
$gAnimTestAnim[$gAnimTestNum] = "ngtrglr15b";
$gAnimTestNum = $gAnimTestNum + 1;
$gAnimTestAnim[$gAnimTestNum] = "ngtrglr16b";
$gAnimTestNum = $gAnimTestNum + 1;
$gAnimTestAnim[$gAnimTestNum] = "ngtrglr17b";
$gAnimTestNum = $gAnimTestNum + 1;
$gAnimTestAnim[$gAnimTestNum] = "ngtrglr18b";
$gAnimTestNum = $gAnimTestNum + 1;
$gAnimTestAnim[$gAnimTestNum] = "ngtrglr19b";
$gAnimTestNum = $gAnimTestNum + 1;
$gAnimTestAnim[$gAnimTestNum] = "ngtrglr1e";
$gAnimTestNum = $gAnimTestNum + 1;
$gAnimTestAnim[$gAnimTestNum] = "ngtrglr20b";
$gAnimTestNum = $gAnimTestNum + 1;
$gAnimTestAnim[$gAnimTestNum] = "ngtrglr2e";
$gAnimTestNum = $gAnimTestNum + 1;
$gAnimTestAnim[$gAnimTestNum] = "ngtrglr3e";
$gAnimTestNum = $gAnimTestNum + 1;
$gAnimTestAnim[$gAnimTestNum] = "ngtrglr4e";
$gAnimTestNum = $gAnimTestNum + 1;
$gAnimTestAnim[$gAnimTestNum] = "ngtrglr5e";
$gAnimTestNum = $gAnimTestNum + 1;
$gAnimTestAnim[$gAnimTestNum] = "ngtrglr6e";
$gAnimTestNum = $gAnimTestNum + 1;
$gAnimTestAnim[$gAnimTestNum] = "ngtrglr7e";
$gAnimTestNum = $gAnimTestNum + 1;
$gAnimTestAnim[$gAnimTestNum] = "ngtrglr8a";
$gAnimTestNum = $gAnimTestNum + 1;
$gAnimTestAnim[$gAnimTestNum] = "ngtrglr9a";
$gAnimTestNum = $gAnimTestNum + 1;
$gAnimTestAnim[$gAnimTestNum] = "ngtrglridl1";
$gAnimTestNum = $gAnimTestNum + 1;
$gAnimTestAnim[$gAnimTestNum] = "ngtrglrjmp01";
$gAnimTestNum = $gAnimTestNum + 1;
$gAnimTestAnim[$gAnimTestNum] = "ngtrglrside01";
$gAnimTestNum = $gAnimTestNum + 1;
$gAnimTestAnim[$gAnimTestNum] = "ngtrglrwlkb01";
$gAnimTestNum = $gAnimTestNum + 1;
$gAnimTestAnim[$gAnimTestNum] = "ngtrglrwlkf01";
$gAnimTestNum = $gAnimTestNum + 1;
$gAnimTestAnim[$gAnimTestNum] = "ngtrgr10e";
$gAnimTestNum = $gAnimTestNum + 1;
$gAnimTestAnim[$gAnimTestNum] = "ngtrgr11e";
$gAnimTestNum = $gAnimTestNum + 1;
$gAnimTestAnim[$gAnimTestNum] = "ngtrgr12a";
$gAnimTestNum = $gAnimTestNum + 1;
$gAnimTestAnim[$gAnimTestNum] = "ngtrgr13a";
$gAnimTestNum = $gAnimTestNum + 1;
$gAnimTestAnim[$gAnimTestNum] = "ngtrgr14a";
$gAnimTestNum = $gAnimTestNum + 1;
$gAnimTestAnim[$gAnimTestNum] = "ngtrgr15a";
$gAnimTestNum = $gAnimTestNum + 1;
$gAnimTestAnim[$gAnimTestNum] = "ngtrgr17a";
$gAnimTestNum = $gAnimTestNum + 1;
$gAnimTestAnim[$gAnimTestNum] = "ngtrgr18a";
$gAnimTestNum = $gAnimTestNum + 1;
$gAnimTestAnim[$gAnimTestNum] = "ngtrgr1e";
$gAnimTestNum = $gAnimTestNum + 1;
$gAnimTestAnim[$gAnimTestNum] = "ngtrgr22a";
$gAnimTestNum = $gAnimTestNum + 1;
$gAnimTestAnim[$gAnimTestNum] = "ngtrgr25b";
$gAnimTestNum = $gAnimTestNum + 1;
$gAnimTestAnim[$gAnimTestNum] = "ngtrgr28b";
$gAnimTestNum = $gAnimTestNum + 1;
$gAnimTestAnim[$gAnimTestNum] = "ngtrgr29b";
$gAnimTestNum = $gAnimTestNum + 1;
$gAnimTestAnim[$gAnimTestNum] = "ngtrgr2e";
$gAnimTestNum = $gAnimTestNum + 1;
$gAnimTestAnim[$gAnimTestNum] = "ngtrgr3e";
$gAnimTestNum = $gAnimTestNum + 1;
$gAnimTestAnim[$gAnimTestNum] = "ngtrgr5e";
$gAnimTestNum = $gAnimTestNum + 1;
$gAnimTestAnim[$gAnimTestNum] = "ngtrgr6e";
$gAnimTestNum = $gAnimTestNum + 1;
$gAnimTestAnim[$gAnimTestNum] = "ngtrgr7e";
$gAnimTestNum = $gAnimTestNum + 1;
$gAnimTestAnim[$gAnimTestNum] = "ngtrgr8e";
$gAnimTestNum = $gAnimTestNum + 1;
$gAnimTestAnim[$gAnimTestNum] = "ngtrgr9e";
$gAnimTestNum = $gAnimTestNum + 1;
$gAnimTestAnim[$gAnimTestNum] = "nhead_LR";
$gAnimTestNum = $gAnimTestNum + 1;
$gAnimTestAnim[$gAnimTestNum] = "nhead_UD";
$gAnimTestNum = $gAnimTestNum + 1;
$gAnimTestAnim[$gAnimTestNum] = "nhead_ss";
$gAnimTestNum = $gAnimTestNum + 1;
$gAnimTestAnim[$gAnimTestNum] = "nhi5ee";
$gAnimTestNum = $gAnimTestNum + 1;
$gAnimTestAnim[$gAnimTestNum] = "nhi5er";
$gAnimTestNum = $gAnimTestNum + 1;
$gAnimTestAnim[$gAnimTestNum] = "nlsnext";
$gAnimTestNum = $gAnimTestNum + 1;
$gAnimTestAnim[$gAnimTestNum] = "nlyidl1";
$gAnimTestNum = $gAnimTestNum + 1;
$gAnimTestAnim[$gAnimTestNum] = "nmcheer";
$gAnimTestNum = $gAnimTestNum + 1;
$gAnimTestAnim[$gAnimTestNum] = "nmcheer1";
$gAnimTestNum = $gAnimTestNum + 1;
$gAnimTestAnim[$gAnimTestNum] = "nmchug";
$gAnimTestNum = $gAnimTestNum + 1;
$gAnimTestAnim[$gAnimTestNum] = "nmdip";
$gAnimTestNum = $gAnimTestNum + 1;
$gAnimTestAnim[$gAnimTestNum] = "nmdrink";
$gAnimTestNum = $gAnimTestNum + 1;
$gAnimTestAnim[$gAnimTestNum] = "nmflrt";
$gAnimTestNum = $gAnimTestNum + 1;
$gAnimTestAnim[$gAnimTestNum] = "nmlol";
$gAnimTestNum = $gAnimTestNum + 1;
$gAnimTestAnim[$gAnimTestNum] = "nmlowdnc";
$gAnimTestNum = $gAnimTestNum + 1;
$gAnimTestAnim[$gAnimTestNum] = "nmmic";
$gAnimTestNum = $gAnimTestNum + 1;
$gAnimTestAnim[$gAnimTestNum] = "nmomg";
$gAnimTestNum = $gAnimTestNum + 1;
$gAnimTestAnim[$gAnimTestNum] = "nmpitch";
$gAnimTestNum = $gAnimTestNum + 1;
$gAnimTestAnim[$gAnimTestNum] = "nmpiv";
$gAnimTestNum = $gAnimTestNum + 1;
$gAnimTestAnim[$gAnimTestNum] = "nmroll";
$gAnimTestNum = $gAnimTestNum + 1;
$gAnimTestAnim[$gAnimTestNum] = "nmsitbend";
$gAnimTestNum = $gAnimTestNum + 1;
$gAnimTestAnim[$gAnimTestNum] = "nmsitpiv";
$gAnimTestNum = $gAnimTestNum + 1;
$gAnimTestAnim[$gAnimTestNum] = "nmtoast";
$gAnimTestNum = $gAnimTestNum + 1;
$gAnimTestAnim[$gAnimTestNum] = "nmupdnc";
$gAnimTestNum = $gAnimTestNum + 1;
$gAnimTestAnim[$gAnimTestNum] = "nmupkis";
$gAnimTestNum = $gAnimTestNum + 1;
$gAnimTestAnim[$gAnimTestNum] = "nmupyaw";
$gAnimTestNum = $gAnimTestNum + 1;
$gAnimTestAnim[$gAnimTestNum] = "nmwave";
$gAnimTestNum = $gAnimTestNum + 1;
$gAnimTestAnim[$gAnimTestNum] = "nmwlk";
$gAnimTestNum = $gAnimTestNum + 1;
$gAnimTestAnim[$gAnimTestNum] = "npwbbattack";
$gAnimTestNum = $gAnimTestNum + 1;
$gAnimTestAnim[$gAnimTestNum] = "npwidle";
$gAnimTestNum = $gAnimTestNum + 1;
$gAnimTestAnim[$gAnimTestNum] = "npwjabattack";
$gAnimTestNum = $gAnimTestNum + 1;
$gAnimTestAnim[$gAnimTestNum] = "npwjabdefend";
$gAnimTestNum = $gAnimTestNum + 1;
$gAnimTestAnim[$gAnimTestNum] = "npwjmp";
$gAnimTestNum = $gAnimTestNum + 1;
$gAnimTestAnim[$gAnimTestNum] = "npwlongstun";
$gAnimTestNum = $gAnimTestNum + 1;
$gAnimTestAnim[$gAnimTestNum] = "npwpowerattack";
$gAnimTestNum = $gAnimTestNum + 1;
$gAnimTestAnim[$gAnimTestNum] = "npwpowerdefend";
$gAnimTestNum = $gAnimTestNum + 1;
$gAnimTestAnim[$gAnimTestNum] = "npwsde";
$gAnimTestNum = $gAnimTestNum + 1;
$gAnimTestAnim[$gAnimTestNum] = "npwshortstun";
$gAnimTestNum = $gAnimTestNum + 1;
$gAnimTestAnim[$gAnimTestNum] = "npwtaunt01";
$gAnimTestNum = $gAnimTestNum + 1;
$gAnimTestAnim[$gAnimTestNum] = "npwtaunt02";
$gAnimTestNum = $gAnimTestNum + 1;
$gAnimTestAnim[$gAnimTestNum] = "npwwlkb";
$gAnimTestNum = $gAnimTestNum + 1;
$gAnimTestAnim[$gAnimTestNum] = "npwwlkf";
$gAnimTestNum = $gAnimTestNum + 1;
$gAnimTestAnim[$gAnimTestNum] = "nreachdown";
$gAnimTestNum = $gAnimTestNum + 1;
$gAnimTestAnim[$gAnimTestNum] = "nrlidl1";
$gAnimTestNum = $gAnimTestNum + 1;
$gAnimTestAnim[$gAnimTestNum] = "nrsbbeaux";
$gAnimTestNum = $gAnimTestNum + 1;
$gAnimTestAnim[$gAnimTestNum] = "nrsbloop";
$gAnimTestNum = $gAnimTestNum + 1;
$gAnimTestAnim[$gAnimTestNum] = "nrsbreaux";
$gAnimTestNum = $gAnimTestNum + 1;
$gAnimTestAnim[$gAnimTestNum] = "nrsbsham";
$gAnimTestNum = $gAnimTestNum + 1;
$gAnimTestAnim[$gAnimTestNum] = "nsitlsn";
$gAnimTestNum = $gAnimTestNum + 1;
$gAnimTestAnim[$gAnimTestNum] = "nsittlk";
$gAnimTestNum = $gAnimTestNum + 1;
$gAnimTestAnim[$gAnimTestNum] = "nspinbottle";
$gAnimTestNum = $gAnimTestNum + 1;
$gAnimTestAnim[$gAnimTestNum] = "nssext";
$gAnimTestNum = $gAnimTestNum + 1;
$gAnimTestAnim[$gAnimTestNum] = "nsumobbattack";
$gAnimTestNum = $gAnimTestNum + 1;
$gAnimTestAnim[$gAnimTestNum] = "nsumoidle";
$gAnimTestNum = $gAnimTestNum + 1;
$gAnimTestAnim[$gAnimTestNum] = "nsumojabattack";
$gAnimTestNum = $gAnimTestNum + 1;
$gAnimTestAnim[$gAnimTestNum] = "nsumojabdefend";
$gAnimTestNum = $gAnimTestNum + 1;
$gAnimTestAnim[$gAnimTestNum] = "nsumojmp";
$gAnimTestNum = $gAnimTestNum + 1;
$gAnimTestAnim[$gAnimTestNum] = "nsumolongstun";
$gAnimTestNum = $gAnimTestNum + 1;
$gAnimTestAnim[$gAnimTestNum] = "nsumopowerattack";
$gAnimTestNum = $gAnimTestNum + 1;
$gAnimTestAnim[$gAnimTestNum] = "nsumopowerdefend";
$gAnimTestNum = $gAnimTestNum + 1;
$gAnimTestAnim[$gAnimTestNum] = "nsumoshortstun";
$gAnimTestNum = $gAnimTestNum + 1;
$gAnimTestAnim[$gAnimTestNum] = "nsumostun";
$gAnimTestNum = $gAnimTestNum + 1;
$gAnimTestAnim[$gAnimTestNum] = "nsumotaunt01";
$gAnimTestNum = $gAnimTestNum + 1;
$gAnimTestAnim[$gAnimTestNum] = "nsumotaunt02";
$gAnimTestNum = $gAnimTestNum + 1;
$gAnimTestAnim[$gAnimTestNum] = "nsumowlkb";
$gAnimTestNum = $gAnimTestNum + 1;
$gAnimTestAnim[$gAnimTestNum] = "nsumowlkf";
$gAnimTestNum = $gAnimTestNum + 1;
$gAnimTestAnim[$gAnimTestNum] = "nsumowlks";
$gAnimTestNum = $gAnimTestNum + 1;
$gAnimTestAnim[$gAnimTestNum] = "ntalk";
$gAnimTestNum = $gAnimTestNum + 1;
$gAnimTestAnim[$gAnimTestNum] = "ntapglass";
$gAnimTestNum = $gAnimTestNum + 1;
$gAnimTestAnim[$gAnimTestNum] = "nthink";
$gAnimTestNum = $gAnimTestNum + 1;
$gAnimTestAnim[$gAnimTestNum] = "ntyidl1a";
$gAnimTestNum = $gAnimTestNum + 1;
$gAnimTestAnim[$gAnimTestNum] = "ntyrsml";
$gAnimTestNum = $gAnimTestNum + 1;
$gAnimTestAnim[$gAnimTestNum] = "ntyrturn";
$gAnimTestNum = $gAnimTestNum + 1;
$gAnimTestAnim[$gAnimTestNum] = "nvom";
$gAnimTestNum = $gAnimTestNum + 1;
$gAnimTestAnim[$gAnimTestNum] = "nxrcst";
$gAnimTestNum = $gAnimTestNum + 1;
$gAnimTestAnim[$gAnimTestNum] = "nzidl1";
$gAnimTestNum = $gAnimTestNum + 1;
$gAnimTestAnim[$gAnimTestNum] = "nzwlk";
$gAnimTestNum = $gAnimTestNum + 1;
$gAnimTestAnim[$gAnimTestNum] = "ygunsling";
$gAnimTestNum = $gAnimTestNum + 1;
function animTest_Again()
{
    %anim = $player.getGender() @ $gAnimTestAnim[$gAnimTestCur];
    $player.playAnim(%anim);
    echo(getScopeName() SPC "-" SPC %anim);
    return ;
}
function animTest_Next()
{
    $gAnimTestCur = $gAnimTestCur + 1;
    if ($gAnimTestCur >= $gAnimTestNum)
    {
        $gAnimTestCur = 0;
    }
    animTest_Again();
    return ;
}
function animTest_Prev()
{
    $gAnimTestCur = $gAnimTestCur - 1;
    if ($gAnimTestCur < 0)
    {
        $gAnimTestCur = $gAnimTestCur - 1;
    }
    animTest_Again();
    return ;
}
function timeTest_tare()
{
    $gTimeTest_StartTimeReal = getRealTime();
    $gTimeTest_StartTimeSim = getSimTime();
    $gTimeTest_dRealToSim = mSubS32($gTimeTest_StartTimeSim, $gTimeTest_StartTimeReal);
    return ;
}
function timeTest_measure()
{
    %timeReal = getRealTime();
    %timeSim = getSimTime();
    %elapsedReal = mSubS32(%timeReal, $gTimeTest_StartTimeReal);
    %elapsedSim = mSubS32(%timeSim, $gTimeTest_StartTimeSim);
    %expectedTimeSim = mAddS32(%timeReal, $gTimeTest_dRealToSim);
    %driftSim = mSubS32(%expectedTimeSim, %timeSim);
    echo("driftSim is" SPC %driftSim * 0.001);
    echo("elapsed real seconds   =" SPC %elapsedReal * 0.001);
    echo("elapsed sim  seconds   =" SPC %elapsedSim * 0.001);
    echo("driftSim  /elapsedReal =" SPC %driftSim / %elapsedReal);
    echo("elapsedSim/elapsedReal =" SPC %elapsedSim / %elapsedReal);
    return ;
}
function dev_TestRequestRetry()
{
    %url = "http://winbuild.doppelganger.com/scripts/orion/fakeEnvManagerResponses/failedRequest1.txt";
    %request = sendRequest_ArbitraryTestUrl(%url, "onDoneOrErrorCallback_TestRequestRetry");
    %request.retryTotal = 1;
    return ;
}
function onDoneOrErrorCallback_TestRequestRetry(%request)
{
    echo(getScopeName() SPC "- YEP!" SPC getTrace());
    return ;
}
function sendRequest_ArbitraryTestUrl(%url, %callbackHandler)
{
    %request = safeNewScriptObject("ManagerRequest", "", 0);
    %request.bindClassName("UniformManagerRequest");
    %request.setURL(%url);
    %request.callbackHandler = %callbackHandler;
    %request.start();
    return %request;
}
function dev_testURLEncode()
{
    %n = 0;
    while (%n < 256)
    {
        %c = intToChar(%n);
        %d = urlEncode(%c);
        %e = urlDecode(%d);
        echo(formatInt("%3d", %n) SPC %c SPC "->" SPC %d);
        echo(formatInt("%3d", %n) SPC %e SPC "<-" SPC %d);
        if (%n > 20)
        {
            %gnarly = %gnarly @ %c;
        }
        %n = %n + 1;
    }
    echo(%gnarly);
    return %gnarly;
}
function dev_ensureRandomItemManager()
{
    if (!isObject(gRandomItemManager))
    {
        new ScriptObject(gRandomItemManager);
        if (isObject(MissionCleanup))
        {
            MissionCleanup.add(gRandomItemManager);
            gRandomItemManager.numItems = 0;
        }
    }
    return ;
}
function dev_clearRandomItems()
{
    dev_ensureRandomItemManager();
    gRandomItemManager.numItems = 0;
    return ;
}
function dev_declareRandomItem(%itemName, %itemWeight)
{
    dev_ensureRandomItemManager();
    %n = gRandomItemManager.numItems;
    gRandomItemManager.itemName[%n] = %itemName;
    gRandomItemManager.itemWeight[%n] = %itemWeight;
    gRandomItemManager.weightsNeedNormalizing = 1;
    gRandomItemManager.numItems = gRandomItemManager.numItems + 1;
    return ;
}
function dev_getRandomItem()
{
    dev_ensureRandomItemManager();
    if (gRandomItemManager.weightsNeedNormalizing)
    {
        gRandomItemManager.weightsNeedNormalizing = 0;
        %totalWeight = 0;
        %n = 0;
        while (%n < gRandomItemManager.numItems)
        {
            gRandomItemManager.itemWeightCumulative[%n] = gRandomItemManager.itemWeight[%n] + %totalWeight;
            %totalWeight = %totalWeight + gRandomItemManager.itemWeight[%n];
            %n = %n + 1;
        }
        gRandomItemManager.totalWeight = %totalWeight;
    }
    %rand = getRandom(0, gRandomItemManager.totalWeight - 1);
    %n = 0;
    while (%n < gRandomItemManager.numItems)
    {
        if (%rand < gRandomItemManager.itemWeightCumulative[%n])
        {
            return gRandomItemManager.itemName[%n];
        }
        %n = %n + 1;
    }
    error("something went wrong.");
    return "";
}
function dev_testRandomItems(%iterations)
{
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
    while (%n < %iterations)
    {
        %item = dev_getRandomItem();
        %totals[%item] = %totals[%item] + 1;
        %n = %n + 1;
    }
    echo("A -" SPC %totals["A"]);
    echo("B -" SPC %totals["B"]);
    echo("C -" SPC %totals["C"]);
    echo("D -" SPC %totals["D"]);
    return ;
}
function SimObject::getTypeStrings(%this)
{
    %types = "";
    %mask = %this.getType();
    %types = %types @ %mask & $TypeMasks::StaticObjectType ? "StaticObjectType " : "";
    %types = %types @ %mask & $TypeMasks::EnvironmentObjectType ? "EnvironmentObjectType " : "";
    %types = %types @ %mask & $TypeMasks::TerrainObjectType ? "TerrainObjectType " : "";
    %types = %types @ %mask & $TypeMasks::InteriorObjectType ? "InteriorObjectType " : "";
    %types = %types @ %mask & $TypeMasks::WaterObjectType ? "WaterObjectType " : "";
    %types = %types @ %mask & $TypeMasks::TriggerObjectType ? "TriggerObjectType " : "";
    %types = %types @ %mask & $TypeMasks::AntiPortalObjectType ? "AntiPortalObjectType " : "";
    %types = %types @ %mask & $TypeMasks::ZoneBoxObjectType ? "ZoneBoxObjectType " : "";
    %types = %types @ %mask & $TypeMasks::MarkerObjectType ? "MarkerObjectType " : "";
    %types = %types @ %mask & $TypeMasks::GameBaseObjectType ? "GameBaseObjectType " : "";
    %types = %types @ %mask & $TypeMasks::ShapeBaseObjectType ? "ShapeBaseObjectType " : "";
    %types = %types @ %mask & $TypeMasks::CameraObjectType ? "CameraObjectType " : "";
    %types = %types @ %mask & $TypeMasks::StaticShapeObjectType ? "StaticShapeObjectType " : "";
    %types = %types @ %mask & $TypeMasks::PlayerObjectType ? "PlayerObjectType " : "";
    %types = %types @ %mask & $TypeMasks::ItemObjectType ? "ItemObjectType " : "";
    %types = %types @ %mask & $TypeMasks::VehicleObjectType ? "VehicleObjectType " : "";
    %types = %types @ %mask & $TypeMasks::VehicleBlockerObjectType ? "VehicleBlockerObjectType " : "";
    %types = %types @ %mask & $TypeMasks::ProjectileObjectType ? "ProjectileObjectType " : "";
    %types = %types @ %mask & $TypeMasks::ExplosionObjectType ? "ExplosionObjectType " : "";
    %types = %types @ %mask & $TypeMasks::CorpseObjectType ? "CorpseObjectType " : "";
    %types = %types @ %mask & $TypeMasks::DebrisObjectType ? "DebrisObjectType " : "";
    %types = %types @ %mask & $TypeMasks::PhysicalZoneObjectType ? "PhysicalZoneObjectType " : "";
    %types = %types @ %mask & $TypeMasks::StaticTSObjectType ? "StaticTSObjectType " : "";
    %types = %types @ %mask & $TypeMasks::StaticRenderedObjectType ? "StaticRenderedObjectType " : "";
    %types = %types @ %mask & $TypeMasks::DamagableItemObjectType ? "DamagableItemObjectType " : "";
    %types = %types @ %mask & $TypeMasks::AdvertObjectType ? "AdvertObjectType " : "";
    %types = %types @ %mask & $TypeMasks::ConversationObjectType ? "ConversationObjectType " : "";
    %types = %types @ %mask & $TypeMasks::UsableObjectType ? "UsableObjectType " : "";
    %types = trim(%types);
    return %types;
}
$gTwitterText = "";
$gTwitterTextCount = 1;
function twitterTest1(%text)
{
    if (!isDefined("%text"))
    {
        %text = "hey there";
    }
    if (%text $= $gTwitterText)
    {
        $gTwitterTextCount = $gTwitterTextCount + 1;
        %text = %text SPC $gTwitterTextCount;
    }
    else
    {
        $gTwitterText = %text;
        $gTwitterTextCount = 1;
    }
    %request = new URLPostObject();
    %request.setURL("https://twitter.com/statuses/update.xml");
    %request.setBodyParam("status", %text);
    %request.setUserNameAndPassword("elenzil:etspass777");
    %request.start();
    return ;
}
function GuiControl::snapAndUpToTwitter(%this, %userName, %password, %asBackground, %tile)
{
    isDefined("%asBackground", 0);
    isDefined("%tile", 0);
    %region = %this.getScreenPosition() SPC %this.getExtent();
    return snapshot::snapAndUpRegionToTwitter(%region, "", %userName, %password, %asBackground, %tile);
}
function snapshot::snapAndUpRegionToTwitter(%region, %fileName, %userName, %password, %asBackground, %tile)
{
    isDefined("%asBackground", 0);
    isDefined("%tile", 0);
    if (%fileName $= "")
    {
        %fileName = "screenshot_" @ getSubStr(getTimeStamp(), 0, 17) @ "_twitter_" @ $screenShotNum;
    }
    %fn_orig = %fileName;
    %fileName = %fileName @ ".jpg";
    if (%asBackground)
    {
        %url = "http://twitter.com/account/update_profile_background_image.html";
    }
    else
    {
        %url = "http://twitter.com/account/update_profile_image.xml";
    }
    %uploader = "";
    if (!snapshotTool::snapRegion(%region, %fileName))
    {
        error(getScopeName() SPC "- Unable to capture region." SPC %region SPC %fileName SPC getTrace());
    }
    else
    {
        $screenShotNum = $screenShotNum + 1;
        %uploader = new URLPostObject();
        %uploader.setProgress(1);
        %uploader.setURL(%url);
        %uploader.setUserNameAndPassword(%userName @ ":" @ %password);
        %uploader.setPostFile("image", %fileName);
        %uploader.setCustomHeaders("Expect:");
        if (%tile)
        {
            %uploader.setBodyParam("tile", "true");
        }
        if (!%uploader.start())
        {
            error(getScopeName() SPC "- Unable to upload photo." SPC %fileName SPC %url SPC getTrace());
        }
    }
    return %uploader;
}
