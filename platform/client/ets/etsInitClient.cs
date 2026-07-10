function GameConnection::etsInit(%this) {
    WorldMap.setLoggedIn(1);
    $gWorldMapJoiningServer = 0;
    resetScreenSize();
    ButtonBar.Initialize();
    ButtonBar.schedule(1000, "showAndHide");
    $gClientGameConnection = %this;
    $player = %this.getPlayerObject();
    $IN_ORBIT_CAM = %this.getControlObject().isClassCamera();
    $Client::MissionLoadTimeFinish = getSimTime();
    echo("client-side load time total:  " @ " " @ ($Client::MissionLoadTimeFinish * 0.001) @ " " @ "seconds.");
    echo("client-side load time mission:" @ " " @ (($Client::MissionLoadTimeFinish - $Client::MissionLoadTimeStart) * 0.001) @ " " @ "seconds.");
    echo("client-side player init:" @ " " @ getDebugString($player));
    $player.prevRolesMask = -(1.0);
    $player.onGotRoles($player.getRolesMask());
    Inventory::fetchPlayerInventoryIfNeedTo($player);
    $player.playersNotifiedOfIdleStatus = new StringMap("");
    if (($UserPref::Player::Genre $= "")) {
        %rand = getRandom(0, 2);
        $UserPref::Player::Genre = getSubStr($player.getDataBlock().possibleGenres, %rand, 1);
        echo("Chose random genre:" @ " " @ $UserPref::Player::Genre);
    }
    sendAnimToServer("root");
    sendInitialPrefsToServer();
    $player.gender = getSubStr($player.getDataBlock().possibleGenders, 0, 1);
    $UserPref::Player::gender = $player.gender;
    $player.startImpressionsTimer();
    echo("setting master volume to" @ " " @ $UserPref::Audio::masterVolume);
    MuteButton.setMuted($UserPref::Audio::mute);
    OptionsPanel.Initialize();
    WindowManager.Initialize();
    if (isFunction(gui_DevOpts_ShowCamPos)) {
        gui_DevOpts_ShowCamPos();
    }
    if (isObject(EmoteHudTabs)) {
        EmoteHudTabs.refreshLists();
    }
    $player.configBoneBlends();
    $StoreSkusLayer = "";
    %startingOutfit = $player.getGender() @ $gOutfits.get("currentOutfit");
    $player.setActiveSKUs(outfits_getCurrentSkus());
    commandToServer('setActiveSkus', $player.getActiveSKUs());
    $gClosetGuiNeedsOpen = 0;
    if (!$StandAlone) {
    }
    if (!$gRetrievedOutfits) {
        $gClosetGuiNeedsOpen = 1;
        $gRetrievedOutfits = 1;
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
    BuddyHudWin.refreshFavoritesList();
    log("general", "info", "ets_init_memory=" @ (getCurrentMemoryUsage() / 1024.0));
    Music::createGetMusicStreamsRequest();
    HudTabs.addPermissionBasedContent();
    setWindowTitle(generateWindowTitle($ServerName));
    getBalancesAndScores();
    if ($gDFNotify) {
        commandToServer('DFStart', 0, $gDFNotifyCode);
        $gDFNotify = 0;
        $gDFNotifyCode = "";
    }
    setIdle(0);
    getUserActivityMgr().setActivityActive("traveling", 0);
    if (isFunction(rf_TrySetup)) {
        rf_TrySetup();
    }
    setNowRendering();
};
function Player::startImpressionsTimer(%this) {
    %this.lastImpressionCount = -(1.0);
};
function Player::takeImpressionsTimer(%this) {
    if (%this.takeImpressions()) {
        %this.schedule(500, "takeImpressionsTimer");
    }
    %this.schedule(2000, "takeImpressionsTimer");
};
function Player::takeImpressions(%this) {
    if (!isObject($GameConnection)) {
        warn("$GameConnection is null");
        return 0;
    }
    %imps = $GameConnection.takeImpressions();
    if ((%imps != %this.lastImpressionCount)) {
        SayConv("Impressions:" @ " " @ %imps);
        %this.lastImpressionCount = %imps;
    }
    return 1;
};
function forceOnscreen(%top, %left, %bottom, %right, %hudwidth, %hudheight) {
    %screenright = getWord($UserPref::Video::Resolution, 0);
    %screenbottom = getWord($UserPref::Video::Resolution, 1);
    %rightslop = (%screenright - (%right + %hudwidth));
    %leftslop = (%left - %hudwidth);
    %topslop = (%top - %hudheight);
    %bottomslop = (%screenbottom - (%bottom + %hudheight));
    %topB = (%topslop > %bottomslop) ? 1 : 0;
    %leftB = (%leftslop > %rightslop) ? 1 : 0;
    if (%topB) {
        %ypos = (%top - %hudheight);
    }
    %ypos = %bottom;
    if (%leftB) {
        %xPos = (%left - %hudwidth);
    }
    %xPos = %right;
    if (%leftB) {
        if ((%xPos < 0.0)) {
            %xPos = 0;
        }
    }
    if (((%xPos + %hudwidth) > %screenright)) {
        %xPos = (%screenright - %hudwidth);
    }
    if (%topB) {
        if ((%ypos < 0.0)) {
            %ypos = 0;
        }
    }
    if (((%ypos + %hudheight) > %screenbottom)) {
        %ypos = (%screenbottom - %hudheight);
    }
    return %xPos @ " " @ %ypos;
};
function Player::onAddClient(%this) {
    if (!isObject(%this)) {
        echo("Player::onAddClient() non object" @ " " @ %this);
        return;
    }
    %this.initGlobalFields();
    gSetField(%this, prevSkuBadge, 0);
    if (%this.isAdded) {
        return;
    }
    %this.isAdded = 1;
    gSetField(%this, affinityLevel, 0);
    gSetField(%this, lastTypingSomethingText, "");
    %this.addToPlayerInstanceDict();
    %relation = BuddyHudWin.getFriendStatus(%this.getShapeName());
    if ((%relation $= "friends")) {
        %this.setBuddy(1);
        %this.setAmFave(1);
    }
    if ((%relation $= "favorite")) {
        %this.setBuddy(1);
    }
    if ((%relation $= "fan")) {
        %this.setAmFave(1);
    }
    %this.setIgnore(BuddyHudWin.getIgnoreStatus(%this.getShapeName()));
    %this.rebuildHudCtrl();
    if (isObject(geMapHud2DTheOrthoMap)) {
        geMapHud2DTheOrthoMap.playerAdd(%this);
    }
};
function Player::addToPlayerInstanceDict(%this) {
    %dict = safeEnsureScriptObjectWithInit("StringMap", "PlayerInstanceDict", "{ ignoreCase = true; }");
    %dict.put(%this.getShapeName(), %this);
};
function Player::removeFromPlayerInstanceDict(%this) {
    %dict = PlayerInstanceDict;
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
    if (!isObject(%this.hudCtrl)) {
        %this.hudCtrl = new Gui3DProjectionCtrl("") {
            profile = "ETSNonModalProfile";
            horizSizing = "right";
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
        };
        %hudCtrl = %this.hudCtrl;
        %hudCtrl.setAttachedTo(%this);
        TheBadgesHud.add(%hudCtrl);
        %ctrl = new GuiBitmapCtrl("") {
            profile = "ETSNonModalProfile";
            horizSizing = "right";
            vertSizing = "bottom";
            position = "0 0";
            extent = "64 64";
            minExtent = "64 64";
            sluggishness = -1;
            visible = 1;
            bitmap = "";
        };
        %hudCtrl.roleCtrl = %ctrl;
        %hudCtrl.add(%hudCtrl.roleCtrl);
    }
    %hudCtrl = %this.hudCtrl;
    %bitmap = "";
    if (isObject($player)) {
        %localPlayerIsStaffOrMod = $player.isStaffOrModerator();
    }
    %localPlayerIsStaffOrMod = 0;
    %bitmapName = %this.getBadgeBitmapName();
    %hudCtrl.roleCtrl.setBitmap(%bitmapName);
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
    %level = gGetField(%this, affinityLevel);
    if ((%level != 0.0)) {
        %level = 1;
    }
    if ((%level == 0.0)) {
        %ret = "";
    }
    %ret = getBitmapFilename("badge", "affinity_" @ %level);
    return %ret;
};
$gRoleBadgeBitmapNamesInitted = 0;
function Player::getRoleBadgeBitmapName(%this) {
    if (!$gRoleBadgeBitmapNamesInitted) {
        %n = 0;
        %n["snooped" @ $gRoleBadgeBitmapNames TAB %n @ "role"] = ;
        %n["neighborhoodwatch" @ $gRoleBadgeBitmapNames TAB %n @ "bitmapName"] = ;
        %n["snoop" @ $gRoleBadgeBitmapNames TAB %n @ "canSeePerm"] = ;
        %n = (%n + 1.0);
        %n["djam" @ $gRoleBadgeBitmapNames TAB %n @ "role"] = ;
        %n["djam" @ $gRoleBadgeBitmapNames TAB %n @ "bitmapName"] = ;
        %n["" @ $gRoleBadgeBitmapNames TAB %n @ "canSeePerm"] = ;
        %n = (%n + 1.0);
        %n["celeb" @ $gRoleBadgeBitmapNames TAB %n @ "role"] = ;
        %n["celeb" @ $gRoleBadgeBitmapNames TAB %n @ "bitmapName"] = ;
        %n["" @ $gRoleBadgeBitmapNames TAB %n @ "canSeePerm"] = ;
        %n = (%n + 1.0);
        $gRoleBadgeBitmapNamesNum = %n;
        $gRoleBadgeBitmapNamesInitted = 1;
    }
    %ret = "";
    %n = 0;
    if ((%n < $gRoleBadgeBitmapNamesNum)) {
    }
    while ((%ret $= "")) {
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
        %n = (%n + 1.0);
        if ((%n < $gRoleBadgeBitmapNamesNum)) {
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
