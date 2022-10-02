function GameConnection::etsInit(%this)
{
    WorldMap.setLoggedIn(1);
    $gWorldMapJoiningServer = 0;
    resetScreenSize();
    ButtonBar.Initialize();
    ButtonBar.schedule(1000, "showAndHide");
    $gClientGameConnection = %this;
    $player = %this.getPlayerObject();
    $IN_ORBIT_CAM = %this.getControlObject().isClassCamera();
    $Client::MissionLoadTimeFinish = getSimTime();
    echo("client-side load time total:  " SPC $Client::MissionLoadTimeFinish * 0.001 SPC "seconds.");
    echo("client-side load time mission:" SPC ($Client::MissionLoadTimeFinish - $Client::MissionLoadTimeStart) * 0.001 SPC "seconds.");
    echo("client-side player init:" SPC getDebugString($player));
    $player.prevRolesMask = -1;
    $player.onGotRoles($player.getRolesMask());
    Inventory::fetchPlayerInventoryIfNeedTo($player);
    $player.playersNotifiedOfIdleStatus = new StringMap();
    if ($UserPref::Player::Genre $= "")
    {
        %rand = getRandom(0, 2);
        $UserPref::Player::Genre = getSubStr($player.getDataBlock().possibleGenres, %rand, 1);
        echo("Chose random genre:" SPC $UserPref::Player::Genre);
    }
    sendAnimToServer("root");
    sendInitialPrefsToServer();
    $player.gender = getSubStr($player.getDataBlock().possibleGenders, 0, 1);
    $UserPref::Player::gender = $player.gender;
    $player.startImpressionsTimer();
    echo("setting master volume to" SPC $UserPref::Audio::masterVolume);
    MuteButton.setMuted($UserPref::Audio::mute);
    OptionsPanel.Initialize();
    WindowManager.Initialize();
    if (isFunction(gui_DevOpts_ShowCamPos))
    {
        gui_DevOpts_ShowCamPos();
    }
    if (isObject(EmoteHudTabs))
    {
        EmoteHudTabs.refreshLists();
    }
    $player.configBoneBlends();
    $StoreSkusLayer = "";
    %startingOutfit = $player.getGender() @ $gOutfits.get("currentOutfit");
    $player.setActiveSKUs(outfits_getCurrentSkus());
    commandToServer('setActiveSkus', $player.getActiveSKUs());
    $gClosetGuiNeedsOpen = 0;
    if (!$StandAlone && !$gRetrievedOutfits)
    {
        $gClosetGuiNeedsOpen = 1;
        $gRetrievedOutfits = 1;
    }
    afterEtsInit();
    if ($UserPref::AIM::AutoSignin)
    {
        doAIMSignIn();
    }
    if (isObjectAndHasPermission_NoWarn($player, "debugPassive") && $DevPref::reportTriggers)
    {
        commandToServer('reportTriggers', 1);
    }
    BuddyHudWin.refreshFavoritesList();
    log("general", "info", "ets_init_memory=" @ getCurrentMemoryUsage() / 1024);
    Music::createGetMusicStreamsRequest();
    HudTabs.addPermissionBasedContent();
    setWindowTitle(generateWindowTitle($ServerName));
    getBalancesAndScores();
    if ($gDFNotify)
    {
        commandToServer('DFStart', 0, $gDFNotifyCode);
        $gDFNotify = 0;
        $gDFNotifyCode = "";
    }
    setIdle(0);
    getUserActivityMgr().setActivityActive("traveling", 0);
    if (isFunction(rf_TrySetup))
    {
        rf_TrySetup();
    }
    setNowRendering();
    return ;
}
function Player::startImpressionsTimer(%this)
{
    %this.lastImpressionCount = -1;
    return ;
}
function Player::takeImpressionsTimer(%this)
{
    if (%this.takeImpressions())
    {
        %this.schedule(500, "takeImpressionsTimer");
    }
    else
    {
        %this.schedule(2000, "takeImpressionsTimer");
    }
    return ;
}
function Player::takeImpressions(%this)
{
    if (!isObject($GameConnection))
    {
        warn("$GameConnection is null");
        return 0;
    }
    %imps = $GameConnection.takeImpressions();
    if (%imps != %this.lastImpressionCount)
    {
        SayConv("Impressions:" SPC %imps);
        %this.lastImpressionCount = %imps;
    }
    return 1;
}
function forceOnscreen(%top, %left, %bottom, %right, %hudwidth, %hudheight)
{
    %screenright = getWord($UserPref::Video::Resolution, 0);
    %screenbottom = getWord($UserPref::Video::Resolution, 1);
    %rightslop = %screenright - (%right + %hudwidth);
    %leftslop = %left - %hudwidth;
    %topslop = %top - %hudheight;
    %bottomslop = %screenbottom - (%bottom + %hudheight);
    %topB = %topslop > %bottomslop ? 1 : 0;
    %leftB = %leftslop > %rightslop ? 1 : 0;
    if (%topB)
    {
        %ypos = %top - %hudheight;
    }
    else
    {
        %ypos = %bottom;
    }
    if (%leftB)
    {
        %xPos = %left - %hudwidth;
    }
    else
    {
        %xPos = %right;
    }
    if (%leftB)
    {
        if (%xPos < 0)
        {
            %xPos = 0;
        }
    }
    else
    {
        if ((%xPos + %hudwidth) > %screenright)
        {
            %xPos = %screenright - %hudwidth;
        }
    }
    if (%topB)
    {
        if (%ypos < 0)
        {
            %ypos = 0;
        }
    }
    else
    {
        if ((%ypos + %hudheight) > %screenbottom)
        {
            %ypos = %screenbottom - %hudheight;
        }
    }
    return %xPos SPC %ypos;
}
function Player::onAddClient(%this)
{
    if (!isObject(%this))
    {
        echo("Player::onAddClient() non object" SPC %this);
        return ;
    }
    %this.initGlobalFields();
    gSetField(%this, prevSkuBadge, 0);
    if (%this.isAdded)
    {
        return ;
    }
    %this.isAdded = 1;
    gSetField(%this, affinityLevel, 0);
    gSetField(%this, lastTypingSomethingText, "");
    %this.addToPlayerInstanceDict();
    %relation = BuddyHudWin.getFriendStatus(%this.getShapeName());
    if (%relation $= "friends")
    {
        %this.setBuddy(1);
        %this.setAmFave(1);
    }
    else
    {
        if (%relation $= "favorite")
        {
            %this.setBuddy(1);
        }
        else
        {
            if (%relation $= "fan")
            {
                %this.setAmFave(1);
            }
        }
    }
    %this.setIgnore(BuddyHudWin.getIgnoreStatus(%this.getShapeName()));
    %this.rebuildHudCtrl();
    if (isObject(geMapHud2DTheOrthoMap))
    {
        geMapHud2DTheOrthoMap.playerAdd(%this);
    }
    return ;
}
function Player::addToPlayerInstanceDict(%this)
{
    %dict = safeEnsureScriptObjectWithInit("StringMap", "PlayerInstanceDict", "{ ignoreCase = true; }");
    %dict.put(%this.getShapeName(), %this);
    return ;
}
function Player::removeFromPlayerInstanceDict(%this)
{
    %dict = PlayerInstanceDict;
    %dict.remove(%this.getShapeName());
    return ;
}
function Player::findPlayerInstance(%playerName)
{
    %dict = safeEnsureScriptObjectWithInit("StringMap", "PlayerInstanceDict", "{ ignoreCase = true; }");
    return %dict.get(%playerName);
}
function getBitmapFilename(%category, %fileName)
{
    %rootPath = $gBitmapCategoryRoot[%category];
    if (%rootPath $= "")
    {
        error("unknown bitmap category:" SPC %category SPC "for filename" SPC %fileName SPC getTrace());
        return "";
    }
    return %rootPath @ %fileName;
}
function Player::rebuildHudCtrl(%this)
{
    if (!isObject(%this.hudCtrl))
    {
        %this.hudCtrl = new Gui3DProjectionCtrl()
        {
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
        %ctrl = new GuiBitmapCtrl()
        {
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
    if (isObject($player))
    {
        %localPlayerIsStaffOrMod = $player.isStaffOrModerator();
    }
    else
    {
        %localPlayerIsStaffOrMod = 0;
    }
    %bitmapName = %this.getBadgeBitmapName();
    %hudCtrl.roleCtrl.setBitmap(%bitmapName);
    return ;
}
function Player::getBadgeBitmapName(%this)
{
    %ret = "";
    if (%ret $= "")
    {
        %ret = %this.getRoleBadgeBitmapName();
    }
    if (%ret $= "")
    {
        %ret = %this.getAffinityBadgeBitmapName();
    }
    return %ret;
}
function Player::getAffinityBadgeBitmapName(%this)
{
    %level = gGetField(%this, affinityLevel);
    if (%level != 0)
    {
        %level = 1;
    }
    if (%level == 0)
    {
        %ret = "";
    }
    else
    {
        %ret = getBitmapFilename("badge", "affinity_" @ %level);
    }
    return %ret;
}
$gRoleBadgeBitmapNamesInitted = 0;
function Player::getRoleBadgeBitmapName(%this)
{
    if (!$gRoleBadgeBitmapNamesInitted)
    {
        %n = 0;
        $gRoleBadgeBitmapNames[%n,"role"] = "snooped";
        $gRoleBadgeBitmapNames[%n,"bitmapName"] = "neighborhoodwatch";
        $gRoleBadgeBitmapNames[%n,"canSeePerm"] = "snoop";
        %n = %n + 1;
        $gRoleBadgeBitmapNames[%n,"role"] = "djam";
        $gRoleBadgeBitmapNames[%n,"bitmapName"] = "djam";
        $gRoleBadgeBitmapNames[%n,"canSeePerm"] = "";
        %n = %n + 1;
        $gRoleBadgeBitmapNames[%n,"role"] = "celeb";
        $gRoleBadgeBitmapNames[%n,"bitmapName"] = "celeb";
        $gRoleBadgeBitmapNames[%n,"canSeePerm"] = "";
        %n = %n + 1;
        $gRoleBadgeBitmapNamesNum = %n;
        $gRoleBadgeBitmapNamesInitted = 1;
    }
    %ret = "";
    %n = 0;
    while (%ret $= "")
    {
        if (%this.hasRoleString($gRoleBadgeBitmapNames[%n,"role"]))
        {
            if (isObject($player))
            {
                if ($player.rolesPermissionCheckNoWarn($gRoleBadgeBitmapNames[%n,"canSeePerm"]))
                {
                    %ret = getBitmapFilename("badge", $gRoleBadgeBitmapNames[%n,"bitmapName"]);
                }
            }
            else
            {
                if ($gRoleBadgeBitmapNames[%n,"canSeePerm"] $= "")
                {
                    %ret = getBitmapFilename("badge", $gRoleBadgeBitmapNames[%n,"bitmapName"]);
                }
            }
        }
        %n = %n + 1;
    }
    return %ret;
}
function afterEtsInit()
{
    if (!($gEvalAfterEtsInit $= ""))
    {
        error("afterEtsInit g=" SPC $gEvalAfterEtsInit);
        eval($gEvalAfterEtsInit);
    }
    return ;
}
