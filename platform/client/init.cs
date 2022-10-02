function initClient()
{
    echo("--------- Initializing Client ---------");
    initAVPlayer();
    $AmClient = 1;
    $Token = "";
    $TokenStandalone = "TOKEN_STANDALONE";
    $Server::Dedicated = 0;
    $Client::GameTypeQuery = "FPS Starter Kit";
    $Client::MissionTypeQuery = "Any";
    $GameConnection = 0;
    $SpawnTargetSavedVURL = "";
    exec("./customProfiles.cs");
    initBaseClient();
    if (!initCanvas(generateWindowTitle("")))
    {
        return ;
    }
    if ($StandAlone && $Preload)
    {
        preloadResources();
    }
    exec("./audio.cs");
    OpenALInit();
    exec("./audioProfiles.cs");
    exec("./fmod.cs");
    exec("./music.cs");
    exec("./musicHud.cs");
    exec("./guiTracker.cs");
    exec("./firstUserExperience.cs");
    exec("./defaultGameProfiles.cs");
    exec("./playGui.gui");
    exec("./playGui.cs");
    exec("./dancePadGui.cs");
    exec("./applauseMeterGui.cs");
    exec("./salonStyleSelector.cs");
    exec("./plantDetailsGui.cs");
    exec("./chatHud.cs");
    exec("./emoticon.cs");
    exec("./reportAbuse.gui");
    exec("./loginGui.gui");
    exec("./loginGui.cs");
    exec("./worldmap.gui");
    exec("./worldmap.cs");
    exec("./loadingGui.gui");
    exec("./loadingGui.cs");
    exec("./licenseDlg.gui");
    exec("./closetGui.gui");
    exec("./closetGui.cs");
    exec("./closetGuiFUE.cs");
    exec("./aimGui.cs");
    exec("./optionsPanel.cs");
    exec("./emotesPanel.cs");
    exec("./buttonBar.cs");
    exec("./cityDownload.gui");
    exec("./cityDownload.cs");
    exec("./ets/initGUIs.cs");
    exec("common/synapseGaming/contentPacks/lightingPack/sgDeployClient.cs");
    exec("./client.cs");
    exec("./game.cs");
    exec("./missionDownload.cs");
    exec("./serverConnection.cs");
    exec("./waitAFrameAndCall.cs");
    exec("./default.bind.cs");
    exec("./ets/init.cs");
    execFilesWithName("projects/*autoInit.cs");
    setNetPort(0);
    setShadowDetailLevel($Pref::shadows);
    setDefaultFov($UserPref::Player::DefaultFOV);
    setZoomSpeed($Pref::Player::zoomSpeed);
    if (fmodInitialize())
    {
        echo("FMOD Audio Initialized");
        fmodSetMute($UserPref::Audio::mute);
    }
    else
    {
        error("FMOD Audio Initialization Failed");
    }
    Canvas.setCursor("DefaultCursor");
    userProperties_makeManager("gUserPropMgrClient", 1);
    AssetManager::clientInit();
    textureDownloadSetDownloadHost($Net::DownloadHost);
    if ($StandAlone == 1)
    {
        userProperties_makeManager("gUserPropMgrServer", 0);
        log("general", "info", "--------- Starting standalone ---------");
        startStandAlone();
    }
    else
    {
        if (!($JoinGameAddress $= ""))
        {
            log("general", "info", "--------- Joining: " @ $JoinGameAddress @ "---------");
            join($JoinGameAddress);
        }
        else
        {
            checkForPackageUpdates($AutoDownloadPackages);
            loadMainMenu();
        }
    }
    $TransitionScreenshot = new ScreenShotUploader();
    HudTabs::setup();
    enableManualWindowResize(1);
    dlMgr::smInit();
    loadAlwaysLoadManifest();
    return ;
}
function shutdownClient()
{
    dlMgr.shutDown();
    return ;
}
function loadMainMenu()
{
    Canvas.setContent(LoginGui);
    if ($Audio::initFailed)
    {
        MessageBoxOK("Audio Initialization Failed", "A sound card must be installed to hear audio playback.  If a soundcard is already present please ensure the drivers are installed properly.", "");
    }
    Canvas.setCursor("DefaultCursor");
    return ;
}
function startStandAlone()
{
    log("initialization", "info", "start connectLocal()");
    if ($MissionArg $= "")
    {
        $MissionArg = "projects/vside/worlds/lounge/missions/lounge.mis";
        log("initialization", "warn", "no mission specified. using" SPC $MissionArg);
    }
    $Player::Name = $UserPref::Player::Name;
    if ($Player::Name $= "")
    {
        $Player::Name = "no_name";
    }
    gUserPropMgrClient.forgetProperties($Player::Name);
    gUserPropMgrClient.requestProperties($Player::Name, "startStandAlone_Part2();");
    return ;
}
function startStandAlone_Part2()
{
    outfits_init();
    createServer("SinglePlayer", $MissionArg);
    $GameConnection = new GameConnection(ServerConnection);
    $GameConnection.setCommonPreconnectClientSettings("");
    RootGroup.add(ServerConnection);
    $GameConnection.connectLocal();
    log("initialization", "info", "end connectLocal()");
    return ;
}
function join(%joinGameAddress)
{
    loadMainMenu();
    echo("join:: connecting to: " @ %joinGameAddress);
    $lastJoinedServer = %joinGameAddress;
    $GameConnection = new GameConnection(ServerConnection);
    $GameConnection.setCommonPreconnectClientSettings("");
    $GameConnection.connect(%joinGameAddress);
    return ;
}
function showLicense()
{
    %file = findFirstFile("*/license.txt");
    %fo = new FileObject();
    %fo.openForRead(%file);
    %text = "";
    while (!%fo.isEOF())
    {
        %text = %text @ %fo.readLine() @ "\n";
    }
    LicenseText.setText(%text);
    Canvas.pushDialog(licenseDlg, 0);
    return ;
}
function onVideoDeactivate()
{
    stopMoving();
    if (isObject(cameraTestsGroup))
    {
        benchmarks::onVideoDeactivate();
    }
    $Video::Inactive = 1;
    return ;
}
function onVideoReactivate()
{
    $Video::Inactive = 0;
    return ;
}
function quitApp()
{
    echoDebug(getScopeName() SPC "- Disconnecting." SPC getTrace());
    if ((!$StandAlone && $AmClient) && !(($Token $= "")))
    {
        logout(1);
    }
    else
    {
        doQuit();
    }
    return ;
}
function logout(%doQuit)
{
    %analytic = getAnalytic();
    %analytic.trackPageView("/client/logout/" @ %doQuit);
    if (isObject(ApplauseMeterGui))
    {
        ApplauseMeterGui.close();
    }
    if (isObject(SalonStyleSelector))
    {
        $gSalonChairCurrent = 0;
        SalonStyleSelector.close();
    }
    if (isObject(PlantDetailsGui))
    {
        PlantDetailsGui.close();
    }
    CustomSpaceClient::OnClientDisconnect();
    if (isObject($player))
    {
        $player.applySkuBadge(0);
    }
    silentAIMDisconnect();
    if (!$UserPref::AIM::RememberMe && isObject(AIMScreenNameField))
    {
        AIMScreenNameField.setText("");
    }
    if (!$UserPref::AIM::SavePassword && isObject(AIMPasswordField))
    {
        AIMPasswordField.setText("");
    }
    WorldMap.setNotConnectedToServer();
    if (isObject(ConvBub))
    {
        ConvBub.close(0);
    }
    if (isObject(BuddyHudWin))
    {
        BuddyHudWin.close();
    }
    if (isObject(UserListFriends))
    {
        UserListFriends.clear();
    }
    if (isObject(UserListFavorites))
    {
        UserListFavorites.clear();
    }
    if (isObject(UserListFans))
    {
        UserListFans.clear();
    }
    if (isObject(SystemMessageDialog))
    {
        SystemMessageTextCtrl.clearText();
        SystemMessageDialog.close();
    }
    setWindowTitle(generateWindowTitle($ServerName));
    if (!$Login::loggedIn)
    {
        return ;
    }
    if (isObject(LogoutRequest))
    {
        return ;
    }
    %cmd = "logoutPart2(" @ %doQuit @ ");";
    geShoutout_Credential_Twitter_Username.setText("");
    geShoutout_Credential_Twitter_Password.setText("");
    gUserPropMgrClient.setProperty($Player::Name, "prevBalanceVBux", $Player::VBux);
    gUserPropMgrClient.setProperty($Player::Name, "prevBalanceVPoints", $Player::VPoints);
    gUserPropMgrClient.persistReally($Player::Name, %cmd);
    return ;
}
function logoutPart2(%doQuit)
{
    %logout = new ManagerRequest(LogoutRequest);
    if (isObject(MissionCleanup))
    {
        MissionCleanup.add(%logout);
    }
    %logout.doQuit = %doQuit;
    %url = $Net::ClientServiceURL @ "/logout";
    if ($Player::Name $= "")
    {
        log("login", "error", getScopeName() SPC "- logout called with empty player name" SPC getTrace());
        return ;
    }
    if ($Token $= "")
    {
        log("login", "error", getScopeName() SPC "- logout called with empty token" SPC getTrace());
        return ;
    }
    %userValue = "?user=" @ urlEncode($Player::Name);
    %tokenValue = "&token=" @ urlEncode($Token);
    if (isObject(AIMConvManager))
    {
        %aimMessagesSentValue = "&aimMessagesSent=" @ urlEncode(AIMConvManager.totalMessagesSent);
    }
    %url = %url @ %userValue @ %tokenValue @ %aimMessagesSentValue;
    log("login", "debug", "logout: " @ %url);
    %logout.setURL(%url);
    %logout.start();
    return ;
}
function LogoutRequest::onError(%this, %errorNum, %errorName)
{
    if (%this.doQuit)
    {
        doQuit();
    }
    %this.delete();
    return ;
}
function LogoutRequest::onDone(%this)
{
    $Login::loggedIn = 0;
    $Player::inventory = "";
    if (isObject(geTGF))
    {
        geTGF.close();
    }
    if (isObject(WorldMap))
    {
        WorldMap.exit();
    }
    if (isObject(HudScoresContent))
    {
        HudScoresContent.previousRespektPoints = 0;
    }
    log("login", "debug", "logout done");
    if (%this.doQuit)
    {
        doQuit();
    }
    %this.delete();
    return ;
}
$gLoginStatusMessage = "";
$gVPointsRatio = 50;
function StatusRequest::onDone(%this)
{
    %status = findRequestStatus(%this);
    log("login", "info", %this.getInfoString() SPC "StatusRequest::onDone:" SPC %status);
    if (%status $= "success")
    {
        %dfEnabled = %this.getValueBool("doubleFusionEnabled");
        if (isFunction("Using_DF") && Using_DF())
        {
            setDFEnabled(%dfEnabled);
        }
        %preload = %this.getValueBool("assetPreloadEnabled");
        if (($Preload && %preload) && !$NoDisplay)
        {
            preloadResources();
        }
        %cache = %this.getValueBool("missionCacheEnabled");
        if ($CacheFlagIsSet && !%cache)
        {
            $CacheFlagIsSet = 0;
        }
        $gLoginStatusMessage = %this.getValue("message");
        if ($gLoginStatusMessage $= "")
        {
            $gLoginStatusMessage = $MsgCat::network["A-OKAY"];
        }
        parseGiftingSettings(%this);
        $gVPointsRatio = %this.getValue("vPointsRatio");
        $gVPointsRatio = 50;
    }
    else
    {
        $gLoginStatusMessage = $MsgCat::network["H-SYS-DOWN"] @ "  " @ $MsgCat::network["H-SEE-FORUMS"];
    }
    LoginGui.update();
    return ;
}
function StatusRequest::onError(%this, %errorNum, %errorName)
{
    if (%errorNum == $CURL::CouldNotResolveHost)
    {
        $gLoginStatusMessage = $MsgCat::network["E-SERVER-DNS"];
    }
    else
    {
        $gLoginStatusMessage = $MsgCat::network["H-SYS-DOWN"] @ "  " @ $MsgCat::network["H-SEE-FORUMS"];
    }
    LoginGui.update();
    log("login", "info", %this.getInfoString() SPC "StatusRequest::onError:" SPC %errorName);
    return ;
}
function StatusRequest::getInfoString(%this)
{
    return "[" @ %this.connection SPC %this.name @ "]";
}
function sendStatusRequest()
{
    %request = safeEnsureScriptObject("ManagerRequest", "StatusRequest");
    if (%request.isOpen())
    {
        warn("network", getScopeName() SPC "- got overlapping requests. postponing. url =" SPC %request.getURL());
        return ;
    }
    %url = $Net::ClientServiceURL @ "/SystemStatus";
    log("login", "info", "sending system status request: " @ %url);
    %request.setURL(%url);
    %request.start();
    $gLoginStatusMessage = $MsgCat::network["H-SEARCHING"];
    LoginGui.update();
    return ;
}
function FirstLaunchRequest::onDone(%this)
{
    log("login", "info", %this.getInfoString() SPC "FirstLaunchRequest::onDone");
    return ;
}
function FirstLaunchRequest::onError(%this, %errorNum, %errorName)
{
    log("login", "info", %this.getInfoString() SPC "FirstLaunchRequest::onError:" SPC %errorName);
    return ;
}
function FirstLaunchRequest::getInfoString(%this)
{
    return "[" @ %this.connection SPC %this.name @ "]";
}
function sendFirstLaunchRequest()
{
    %request = safeEnsureScriptObject("ManagerRequest", "FirstLaunchRequest");
    if (%request.isOpen())
    {
        warn("network", getScopeName() SPC "- got overlapping requests. postponing. url =" SPC %request.getURL());
        return ;
    }
    if (!($Net::userReferrer $= ""))
    {
        %referrer = $Net::userReferrer;
    }
    else
    {
        %referrer = "";
    }
    if (!($Net::userOwner $= ""))
    {
        %owner = $Net::userOwner;
    }
    else
    {
        %owner = "doppelganger";
    }
    %url = $Net::downloadURL @ "/first_launch?status=true&platform=" @ $Platform @ "&referrer=" @ %referrer @ "&owner=" @ %owner;
    %request.setURL(%url);
    %request.start();
    return ;
}
