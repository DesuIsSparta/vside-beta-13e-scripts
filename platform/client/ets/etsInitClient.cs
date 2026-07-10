function GameConnection::etsInit(%this) {
    1.setLoggedIn();
    $gWorldMapJoiningServer = 0;
    WorldMap;
    resetScreenSize();
    Initialize();
    1000.schedule("showAndHide");
    $gClientGameConnection = %this;
    ButtonBar;
    $player = %this.getPlayerObject();
    ButtonBar;
    $IN_ORBIT_CAM = %this.getControlObject().isClassCamera();
    $Client::MissionLoadTimeFinish = getSimTime();
    echo("client-side load time total:  " @ " " @ (0.001 * $Client::MissionLoadTimeFinish) @ " " @ "seconds.");
    echo("client-side load time mission:" @ " " @ (0.001 * ($Client::MissionLoadTimeStart - $Client::MissionLoadTimeFinish)) @ " " @ "seconds.");
    echo("client-side player init:" @ " " @ getDebugString($player));
    prevRolesMask = -(1.0) @ $player;
    $player.onGotRoles($player.getRolesMask());
    Inventory::fetchPlayerInventoryIfNeedTo($player);
    playersNotifiedOfIdleStatus = StringMap @ new ""() @ $player;
    0;
    if (($UserPref::Player::Genre $= "")) {
        %rand = getRandom(0, 2);
        $UserPref::Player::Genre = getSubStr(possibleGenres, %rand, 1);
        $player.getDataBlock();
        echo("Chose random genre:" @ " " @ $UserPref::Player::Genre);
    }
    sendAnimToServer("root");
    sendInitialPrefsToServer();
    gender = $player.getDataBlock() @ getSubStr(possibleGenders, 0, 1) @ $player;
    $UserPref::Player::gender = gender;
    $player;
    $player.startImpressionsTimer();
    echo("setting master volume to" @ " " @ $UserPref::Audio::masterVolume);
    $UserPref::Audio::mute.setMuted();
    Initialize();
    Initialize();
    if (isFunction()) {
        gui_DevOpts_ShowCamPos();
    }
    if (isObject()) {
        refreshLists();
    }
    $player.configBoneBlends();
    $StoreSkusLayer = "";
    EmoteHudTabs;
    %startingOutfit = EmoteHudTabs @ $player.getGender() @ $gOutfits.get("currentOutfit");
    gui_DevOpts_ShowCamPos;
    $player.setActiveSKUs(outfits_getCurrentSkus());
    commandToServer('setActiveSkus', $player.getActiveSKUs());
    $gClosetGuiNeedsOpen = 0;
    WindowManager;
    if (!($StandAlone)) {
    }
    if (!($gRetrievedOutfits)) {
        $gClosetGuiNeedsOpen = 1;
        OptionsPanel;
        $gRetrievedOutfits = 1;
        MuteButton;
    }
    afterEtsInit();
    if ($UserPref::AIM::AutoSignin) {
        doAIMSignIn();
    }
    if (isObjectAndHasPermission_NoWarn($player, "debugPassive")) {
    }
    if ($DevPref::reportTriggers) {
        commandToServer('reportTriggers', 1);
    }
    refreshFavoritesList();
    log("general", "info", BuddyHudWin @ "ets_init_memory=" @ (1024.0 / getCurrentMemoryUsage()));
    Music::createGetMusicStreamsRequest();
    addPermissionBasedContent();
    setWindowTitle(generateWindowTitle($ServerName));
    getBalancesAndScores();
    if ($gDFNotify) {
        commandToServer('DFStart', 0, $gDFNotifyCode);
        $gDFNotify = 0;
        HudTabs;
        $gDFNotifyCode = "";
    }
    setIdle(0);
    getUserActivityMgr().setActivityActive("traveling", 0);
    if (isFunction()) {
        rf_TrySetup();
    }
    setNowRendering();
};
function Player::startImpressionsTimer(%this) {
    lastImpressionCount = -(1.0) @ %this;
};
function Player::takeImpressionsTimer(%this) {
    if (%this.takeImpressions()) {
        %this.schedule(500, "takeImpressionsTimer");
    }
    %this.schedule(2000, "takeImpressionsTimer");
};
function Player::takeImpressions(%this) {
    if (!(isObject($GameConnection))) {
        warn("$GameConnection is null");
        return 0;
    }
    %imps = $GameConnection.takeImpressions();
    if ((lastImpressionCount != %imps)) {
        SayConv("Impressions:" @ " " @ %imps);
        lastImpressionCount = %this @ %imps @ %this;
    }
    return 1;
};
function forceOnscreen(%top, %left, %bottom, %right, %hudwidth, %hudheight) {
    %screenright = getWord($UserPref::Video::Resolution, 0);
    %screenbottom = getWord($UserPref::Video::Resolution, 1);
    %rightslop = ((%hudwidth + %right) - %screenright);
    %leftslop = (%hudwidth - %left);
    %topslop = (%hudheight - %top);
    %bottomslop = ((%hudheight + %bottom) - %screenbottom);
    %topB = (%bottomslop > %topslop) ? 1 : 0;
    %leftB = (%rightslop > %leftslop) ? 1 : 0;
    if (%topB) {
        %ypos = (%hudheight - %top);
    }
    %ypos = %bottom;
    if (%leftB) {
        %xPos = (%hudwidth - %left);
    }
    %xPos = %right;
    if (%leftB) {
        if ((0.0 < %xPos)) {
            %xPos = 0;
        }
    }
    if ((%screenright > (%hudwidth + %xPos))) {
        %xPos = (%hudwidth - %screenright);
    }
    if (%topB) {
        if ((0.0 < %ypos)) {
            %ypos = 0;
        }
    }
    if ((%screenbottom > (%hudheight + %ypos))) {
        %ypos = (%hudheight - %screenbottom);
    }
    return %xPos @ " " @ %ypos;
};
function Player::onAddClient(%this) {
    if (!(isObject(%this))) {
        echo("Player::onAddClient() non object" @ " " @ %this);
        return;
    }
    %this.initGlobalFields();
    gSetField(%this, 0);
    if (isAdded) {
        return %this;
    }
    isAdded = 1 @ %this;
    gSetField(%this, 0);
    gSetField(%this, "");
    %this.addToPlayerInstanceDict();
    %relation = %this.getShapeName().getFriendStatus();
    BuddyHudWin;
    if ((lastTypingSomethingText SPC %relation $= "friends")) {
        %this.setBuddy(1);
        %this.setAmFave(1);
    }
    if ((affinityLevel SPC %relation $= "favorite")) {
        %this.setBuddy(1);
    }
    if ((%relation $= "fan")) {
        %this.setAmFave(1);
    }
    %this.setIgnore(%this.getShapeName().getIgnoreStatus());
    %this.rebuildHudCtrl();
    if (isObject()) {
        %this.playerAdd();
    }
};
function Player::addToPlayerInstanceDict(%this) {
    %dict = safeEnsureScriptObjectWithInit("StringMap", "PlayerInstanceDict", "{ ignoreCase = true; }");
    %dict.put(%this.getShapeName(), %this);
};
function Player::removeFromPlayerInstanceDict(%this) {
    // unhandled opcode 1954 at 0x0000063F
    %dict.remove(%this.getShapeName());
};
function Player::findPlayerInstance(%playerName) {
    %dict = safeEnsureScriptObjectWithInit("StringMap", "PlayerInstanceDict", "{ ignoreCase = true; }");
    return %dict.get(%playerName);
};
function getBitmapFilename(%category, %fileName) {
    %rootPath = %category[$gBitmapCategoryRoot @ %category];
    if ((%rootPath $= "")) {
        error("unknown bitmap category:" @ " " @ %category @ " " @ "for filename" @ " " @ %fileName @ " " @ getTrace());
        return "";
    }
    return %rootPath @ %fileName;
};
function Player::rebuildHudCtrl(%this) {
    if (!(isObject(hudCtrl))) {
        profile = Gui3DProjectionCtrl @ new ""() @ "ETSNonModalProfile";
        0;
        horizSizing = %this @ "right";
        vertSizing = "bottom";
        position = "10 10";
        extent = "64 64";
        minExtent = "8 2";
        sluggishness = -1;
        worldSluggishness = 0.4;
        visible = 1;
        offsetObject = "0 -0.2 0.24";
        offsetScreen = "0 -36";
        useEyePoint = "1 1";
        visibleDist = $pref::TS::distBadgesVis;
        hudCtrl = %this;
        %hudCtrl = hudCtrl;
        %this;
        %hudCtrl.setAttachedTo(%this);
        %hudCtrl.add();
        profile = GuiBitmapCtrl @ new ""() @ "ETSNonModalProfile";
        0;
        horizSizing = TheBadgesHud @ "right";
        vertSizing = "bottom";
        position = "0 0";
        extent = "64 64";
        minExtent = "64 64";
        sluggishness = -1;
        visible = 1;
        bitmap = "";
        %ctrl = ;
        roleCtrl = %ctrl @ %hudCtrl;
        %hudCtrl.add(roleCtrl);
    }
    %hudCtrl = hudCtrl;
    %this;
    %bitmap = "";
    %hudCtrl;
    if (isObject($player)) {
        %localPlayerIsStaffOrMod = $player.isStaffOrModerator();
    }
    %localPlayerIsStaffOrMod = 0;
    %bitmapName = %this.getBadgeBitmapName();
    roleCtrl.setBitmap(%bitmapName);
};
function Player::getBadgeBitmapName(%this) {
    %ret = "";
    if ((%ret $= "")) {
        %ret = %this.getRoleBadgeBitmapName();
    }
    if ((%ret $= "")) {
        %ret = %this.getAffinityBadgeBitmapName();
    }
    return %ret;
};
function Player::getAffinityBadgeBitmapName(%this) {
    %level = gGetField(%this);
    affinityLevel;
    if ((0.0 != %level)) {
        %level = 1;
    }
    if ((0.0 == %level)) {
        %ret = "";
    }
    %ret = getBitmapFilename("badge", "affinity_" @ %level);
    return %ret;
};
$gRoleBadgeBitmapNamesInitted = 0;
function Player::getRoleBadgeBitmapName(%this) {
    if (!($gRoleBadgeBitmapNamesInitted)) {
        %n = 0;
        %n["snooped" @ $gRoleBadgeBitmapNames TAB %n @ "role"] = ;
        %n["neighborhoodwatch" @ $gRoleBadgeBitmapNames TAB %n @ "bitmapName"] = ;
        %n["snoop" @ $gRoleBadgeBitmapNames TAB %n @ "canSeePerm"] = ;
        %n = (1.0 + %n);
        %n["djam" @ $gRoleBadgeBitmapNames TAB %n @ "role"] = ;
        %n["djam" @ $gRoleBadgeBitmapNames TAB %n @ "bitmapName"] = ;
        %n["" @ $gRoleBadgeBitmapNames TAB %n @ "canSeePerm"] = ;
        %n = (1.0 + %n);
        %n["celeb" @ $gRoleBadgeBitmapNames TAB %n @ "role"] = ;
        %n["celeb" @ $gRoleBadgeBitmapNames TAB %n @ "bitmapName"] = ;
        %n["" @ $gRoleBadgeBitmapNames TAB %n @ "canSeePerm"] = ;
        %n = (1.0 + %n);
        $gRoleBadgeBitmapNamesNum = %n;
        $gRoleBadgeBitmapNamesInitted = 1;
    }
    %ret = "";
    %n = 0;
    if (($gRoleBadgeBitmapNamesNum < %n)) {
    }
    if ((%ret $= "")) {
        if (%this.hasRoleString(%n[$gRoleBadgeBitmapNames TAB %n @ "role"])) {
            if (isObject($player)) {
                if ($player.rolesPermissionCheckNoWarn(%n[$gRoleBadgeBitmapNames TAB %n @ "canSeePerm"])) {
                    %ret = getBitmapFilename("badge", %n[$gRoleBadgeBitmapNames TAB %n @ "bitmapName"]);
                }
            }
            if ((%n[$gRoleBadgeBitmapNames TAB %n @ "canSeePerm"] $= "")) {
                %ret = getBitmapFilename("badge", %n[$gRoleBadgeBitmapNames TAB %n @ "bitmapName"]);
            }
        }
        %n = (1.0 + %n);
        if (($gRoleBadgeBitmapNamesNum < %n)) {
        }
    }
    return %ret;
};
function afterEtsInit() {
    if (!($gEvalAfterEtsInit $= "")) {
        error("afterEtsInit g=" @ " " @ $gEvalAfterEtsInit);
        eval($gEvalAfterEtsInit);
    }
};
