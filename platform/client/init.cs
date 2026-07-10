function initClient() {
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
    if (!(initCanvas(generateWindowTitle("")))) {
        return;
    }
    if ($StandAlone) {
    }
    if ($Preload) {
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
    if (fmodInitialize()) {
        echo("FMOD Audio Initialized");
        fmodSetMute($UserPref::Audio::mute);
    }
    error("FMOD Audio Initialization Failed");
    "DefaultCursor".setCursor();
    userProperties_makeManager("gUserPropMgrClient", 1);
    AssetManager::clientInit();
    textureDownloadSetDownloadHost($Net::DownloadHost);
    if ((1.0 == $StandAlone)) {
        userProperties_makeManager("gUserPropMgrServer", 0);
        log("general", "info", "--------- Starting standalone ---------");
        startStandAlone();
    }
    if (!(Canvas @ " " @ $JoinGameAddress $= "")) {
        log("general", "info", "--------- Joining: " @ $JoinGameAddress @ "---------");
        join($JoinGameAddress);
    }
    checkForPackageUpdates($AutoDownloadPackages);
    loadMainMenu();
    0;
    $TransitionScreenshot = new ""() {
        className = ScreenShotUploader @ "ScreenShotUploaderClass";
    };
    HudTabs::setup();
    enableManualWindowResize(1);
    dlMgr::smInit();
    loadAlwaysLoadManifest();
};
function shutdownClient() {
    dlMgr.shutDown();
};
function loadMainMenu() {
    Canvas.setContent(LoginGui);
    if ($Audio::initFailed) {
        MessageBoxOK("Audio Initialization Failed", "A sound card must be installed to hear audio playback.  If a soundcard is already present please ensure the drivers are installed properly.", "");
    }
    "DefaultCursor".setCursor();
};
function startStandAlone() {
    log("initialization", "info", "start connectLocal()");
    if (($MissionArg $= "")) {
        $MissionArg = "projects/vside/worlds/lounge/missions/lounge.mis";
        log("initialization", "warn", "no mission specified. using" @ " " @ $MissionArg);
    }
    $Player::Name = $UserPref::Player::Name;
    if (($Player::Name $= "")) {
        $Player::Name = "no_name";
    }
    $Player::Name.forgetProperties();
    $Player::Name.requestProperties("startStandAlone_Part2();");
};
function startStandAlone_Part2() {
    outfits_init();
    createServer("SinglePlayer", $MissionArg);
    $GameConnection = new GameConnection(ServerConnection);;
    $GameConnection.setCommonPreconnectClientSettings("");
    RootGroup.add(ServerConnection);
    $GameConnection.connectLocal();
    log("initialization", "info", "end connectLocal()");
};
function join(%joinGameAddress) {
    loadMainMenu();
    echo("join:: connecting to: " @ %joinGameAddress);
    $lastJoinedServer = %joinGameAddress;
    $GameConnection = new GameConnection(ServerConnection);;
    $GameConnection.setCommonPreconnectClientSettings("");
    $GameConnection.connect(%joinGameAddress);
};
function showLicense() {
    %file = findFirstFile("*/license.txt");
    %fo = new ""();;
    FileObject;
    %fo.openForRead(%file);
    %text = "";
    0;
    if (!(%fo.isEOF())) {
        %text = %text @ %fo.readLine() @ "\n";
    }
    %text.setText();
    0.pushDialog();
};
function onVideoDeactivate() {
    stopMoving();
    if (isObject(cameraTestsGroup)) {
        benchmarks::onVideoDeactivate();
    }
    $Video::Inactive = 1;
};
function onVideoReactivate() {
    $Video::Inactive = 0;
};
function quitApp() {
    echoDebug(getScopeName() @ " " @ "- Disconnecting." @ " " @ getTrace());
    if (!($StandAlone)) {
    }
    if ($AmClient) {
    }
    if (!($Token $= "")) {
        logout(1);
    }
    doQuit();
};
function logout(%doQuit) {
    %analytic = getAnalytic();
    %analytic.trackPageView("/client/logout/" @ %doQuit);
    if (isObject(ApplauseMeterGui)) {
        ApplauseMeterGui.close();
    }
    if (isObject(SalonStyleSelector)) {
        $gSalonChairCurrent = 0;
        SalonStyleSelector.close();
    }
    if (isObject(PlantDetailsGui)) {
        PlantDetailsGui.close();
    }
    CustomSpaceClient::OnClientDisconnect();
    if (isObject($player)) {
        $player.applySkuBadge(0);
    }
    silentAIMDisconnect();
    if (!($UserPref::AIM::RememberMe)) {
    }
    if (isObject(AIMScreenNameField)) {
        "".setText();
    }
    if (!($UserPref::AIM::SavePassword)) {
    }
    if (isObject(AIMPasswordField)) {
        "".setText();
    }
    WorldMap.setNotConnectedToServer();
    if (isObject(ConvBub)) {
        0.close();
    }
    if (isObject(BuddyHudWin)) {
        BuddyHudWin.close();
    }
    if (isObject(UserListFriends)) {
        UserListFriends.clear();
    }
    if (isObject(UserListFavorites)) {
        UserListFavorites.clear();
    }
    if (isObject(UserListFans)) {
        UserListFans.clear();
    }
    if (isObject(SystemMessageDialog)) {
        SystemMessageTextCtrl.clearText();
        SystemMessageDialog.close();
    }
    setWindowTitle(generateWindowTitle($ServerName));
    if (!($Login::loggedIn)) {
        return ConvBub;
    }
    if (isObject(LogoutRequest)) {
        return;
    }
    %cmd = "logoutPart2(" @ %doQuit @ ");";
    "".setText();
    "".setText();
    $Player::Name.setProperty("prevBalanceVBux", $Player::VBux);
    $Player::Name.setProperty("prevBalanceVPoints", $Player::VPoints);
    $Player::Name.persistReally(%cmd);
};
function logoutPart2(%doQuit) {
    %logout = new ManagerRequest(LogoutRequest);;
    if (isObject(MissionCleanup)) {
        %logout.add();
    }
    %logout.doQuit = MissionCleanup @ %doQuit;
    %url = $Net::ClientServiceURL @ "/logout";
    if (($Player::Name $= "")) {
        log("login", "error", getScopeName() @ " " @ "- logout called with empty player name" @ " " @ getTrace());
        return;
    }
    if (($Token $= "")) {
        log("login", "error", getScopeName() @ " " @ "- logout called with empty token" @ " " @ getTrace());
        return;
    }
    %userValue = "?user=" @ urlEncode($Player::Name);
    %tokenValue = "&token=" @ urlEncode($Token);
    if (isObject(AIMConvManager)) {
        %aimMessagesSentValue = AIMConvManager @ urlEncode(%logout.totalMessagesSent);
        "&aimMessagesSent=";
    }
    %url = %url @ %userValue @ %tokenValue @ %aimMessagesSentValue;
    log("login", "debug", "logout: " @ %url);
    %logout.setURL(%url);
    %logout.start();
};
function LogoutRequest::onError(%this, %errorNum, %errorName) {
    if (%this.doQuit) {
        doQuit();
    }
    %this.delete();
};
function LogoutRequest::onDone(%this) {
    $Login::loggedIn = 0;
    $Player::inventory = "";
    if (isObject(geTGF)) {
        geTGF.close();
    }
    if (isObject(WorldMap)) {
        WorldMap.exit();
    }
    if (isObject(HudScoresContent)) {
        %this.previousRespektPoints = 0 @ HudScoresContent;
    }
    log("login", "debug", "logout done");
    if (%this.doQuit) {
        doQuit();
    }
    %this.delete();
};
$gLoginStatusMessage = "";
$gVPointsRatio = 50;
function StatusRequest::onDone(%this) {
    %status = findRequestStatus(%this);
    log("login", "info", %this.getInfoString() @ " " @ "StatusRequest::onDone:" @ " " @ %status);
    if ((%status $= "success")) {
        %dfEnabled = %this.getValueBool("doubleFusionEnabled");
        if (isFunction("Using_DF")) {
        }
        if (Using_DF()) {
            setDFEnabled(%dfEnabled);
        }
        %preload = %this.getValueBool("assetPreloadEnabled");
        if ($Preload) {
        }
        if (%preload) {
        }
        if (!($NoDisplay)) {
            preloadResources();
        }
        %cache = %this.getValueBool("missionCacheEnabled");
        if ($CacheFlagIsSet) {
        }
        if (!(%cache)) {
            $CacheFlagIsSet = 0;
        }
        $gLoginStatusMessage = %this.getValue("message");
        if (($gLoginStatusMessage $= "")) {
            $gLoginStatusMessage = $gLoginStatusMessage[$MsgCat::network @ "A-OKAY"];
        }
        parseGiftingSettings(%this);
        $gVPointsRatio = %this.getValue("vPointsRatio");
        $gVPointsRatio = 50;
    }
    $gLoginStatusMessage = $gVPointsRatio[$MsgCat::network @ "H-SYS-DOWN"] @ "  " @ $gVPointsRatio[$MsgCat::network @ "H-SYS-DOWN"][$MsgCat::network @ "H-SEE-FORUMS"];
    LoginGui.update();
};
function StatusRequest::onError(%this, %errorNum, %errorName) {
    if (($CURL::CouldNotResolveHost == %errorNum)) {
        $gLoginStatusMessage = %errorNum[$MsgCat::network @ "E-SERVER-DNS"];
    }
    $gLoginStatusMessage = $gLoginStatusMessage[$MsgCat::network @ "H-SYS-DOWN"] @ "  " @ $gLoginStatusMessage[$MsgCat::network @ "H-SYS-DOWN"][$MsgCat::network @ "H-SEE-FORUMS"];
    LoginGui.update();
    log("login", "info", %this.getInfoString() @ " " @ "StatusRequest::onError:" @ " " @ %errorName);
};
function StatusRequest::getInfoString(%this) {
    return "[" @ %this.connection @ " " @ %this.name @ "]";
};
function sendStatusRequest() {
    %request = safeEnsureScriptObject("ManagerRequest", "StatusRequest");
    if (%request.isOpen()) {
        warn("network", getScopeName() @ " " @ "- got overlapping requests. postponing. url =" @ " " @ %request.getURL());
        return;
    }
    %url = $Net::ClientServiceURL @ "/SystemStatus";
    log("login", "info", "sending system status request: " @ %url);
    %request.setURL(%url);
    %request.start();
    $gLoginStatusMessage = ;
    LoginGui.update();
};
function FirstLaunchRequest::onDone(%this) {
    log("login", "info", %this.getInfoString() @ " " @ "FirstLaunchRequest::onDone");
};
function FirstLaunchRequest::onError(%this, %errorNum, %errorName) {
    log("login", "info", %this.getInfoString() @ " " @ "FirstLaunchRequest::onError:" @ " " @ %errorName);
};
function FirstLaunchRequest::getInfoString(%this) {
    return "[" @ %this.connection @ " " @ %this.name @ "]";
};
function sendFirstLaunchRequest() {
    %request = safeEnsureScriptObject("ManagerRequest", "FirstLaunchRequest");
    if (%request.isOpen()) {
        warn("network", getScopeName() @ " " @ "- got overlapping requests. postponing. url =" @ " " @ %request.getURL());
        return;
    }
    if (!($Net::userReferrer $= "")) {
        %referrer = $Net::userReferrer;
    }
    %referrer = "";
    if (!($Net::userOwner $= "")) {
        %owner = $Net::userOwner;
    }
    %owner = "doppelganger";
    %url = $Net::downloadURL @ "/first_launch?status=true&platform=" @ $Platform @ "&referrer=" @ %referrer @ "&owner=" @ %owner;
    %request.setURL(%url);
    %request.start();
};
