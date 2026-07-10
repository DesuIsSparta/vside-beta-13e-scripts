addMessageCallback('MsgConnectionError', handleConnectionErrorMessage);
function handleConnectionErrorMessage(%unused, %msgString) {
    $ServerConnectionErrorMessage = %msgString;
};
function GameConnection::initialControlSet(%this) {
    echo("*** Initial Control Object");
    if (!(isObject(EditorGui))) {
    }
    if (!(Editor::checkActiveLoadDone()) && (Canvas.getContent() != PlayGui.getId())) {
        PlayGui.setContent(Canvas);
    }
    %this.etsInit();
};
function GameConnection::setLagIcon(%this, %state) {
    if ((%this.getAddress() $= "local")) {
        return;
    }
    (%state $= "true").setVisible(LagIcon);
};
function GameConnection::onConnectionAccepted(%this) {
    0.setVisible(LagIcon);
    $GameConnection = %this;
    $VURLcmd = "";
    1.setActivityActive(getUserActivityMgr(), "traveling");
};
function GameConnection::onServerConnectionPossiblyTimingOut(%this) {
    warn("Possibly losing connection to server..");
};
function GameConnection::onServerConnectionRestored(%this) {
    warn("Restored connection to server.");
};
function GameConnection::onServerConnectionTimedOut(%this) {
    disconnectedCleanup(geTGF);
    MessageBoxOK("TIMED OUT", $MsgCat::network["E-SERVER-TIMEOUT"], "");
};
function GameConnection::onConnectionDropped(%this, %msg) {
    %msg = standardSubstitutions(%msg);
    if (%this.waitForDisconnect) {
        %this.waitForDisconnect = 0;
        disconnectedCleanup("");
        $SpawnTargetSavedVURL.schedule(WorldMap, 1, "doServerJoin");
        return;
    }
    if ((getField(%msg, 0) $= "bootToMap")) {
        %currentCity = WorldMap.currentCity;
        WorldMap.setNotConnectedToServer();
        "map".openToTabName(geTGF);
        %levelOrCityName = getField(%msg, 1);
        if ((%levelOrCityName $= 0)) {
        }
        if ((%levelOrCityName $= 1)) {
            %currentCity.selectCity(WorldMap);
        }
        %levelOrCityName.selectCity(WorldMap);
        MessageBoxOK("BOOTED TO MAP", getFields(%msg, 2), "");
    }
    logout(0);
    disconnectedCleanup(LoginGui);
    MessageBoxOK("DISCONNECT", $MsgCat::network["E-DROPPED"] @ %msg, "");
};
function GameConnection::onConnectionError(%this, %msg) {
    if ($CacheFlagIsSet) {
        $CurrentMission.deleteCacheFile(ServerConnection);
        $CurrentMission = "";
    }
    disconnectedCleanup(geTGF);
    MessageBoxOK("DISCONNECT", $ServerConnectionErrorMessage @ " (" @ %msg @ ")", "");
};
function GameConnection::onConnectRequestRejected(%this, %msg, %extra) {
    %destGui = LoginGui;
    if ((%msg $= "CR_INVALID_PROTOCOL_VERSION")) {
        %error = %msg[$MsgCat::network @ "E-PROTOCOL-VER"];
        %destGui = geTGF;
    }
    if ((%msg $= "CR_INVALID_CONNECT_PACKET")) {
        %error = "Internal Error: badly formed network packet";
        %destGui = geTGF;
    }
    if ((%msg $= "CR_YOUAREBANNED")) {
        %error = "You are not allowed to play on this server.";
    }
    if ((%msg $= "CR_TOKEN")) {
        %error = $ETS::AppName @ " " @ $ETS::AppName[$MsgCat::network @ "E-SERVICE-UNAVAIL"];
    }
    if ((%msg $= "CR_SERVERFULL")) {
        %error = %msg[$MsgCat::login @ "E-SERVER-FULL"];
        %destGui = geTGF;
    }
    if ((%msg $= "CR_BAD_TARGET")) {
        %error = %msg[$MsgCat::login @ "E-BAD-TARGET"];
        %destGui = geTGF;
    }
    if ((%msg $= "CR_CANNOT_ACTIVATE_APARTMENT")) {
        %error = %msg[$MsgCat::login @ "E-CANNOT-ACTIVATE-APARTMENT"];
        %destGui = geTGF;
    }
    if ((%msg $= "CR_APARTMENT_ACTIVATION_DENIED")) {
        %error = %msg[$MsgCat::login @ "E-APARTMENT-ACTIVATION-DENIED"];
        %destGui = geTGF;
    }
    if ((%msg $= "CR_APARTMENT_ACTIVE_ELSEWHERE")) {
        %error = %msg[$MsgCat::login @ "E-APARTMENT-ACTIVATE-ELSEWHERE"];
        %destGui = geTGF;
    }
    if ((%msg $= "CR_LEVEL_COMPLETED")) {
        %error = %msg[$MsgCat::login @ "E-LEVEL-COMPLETED"];
        %destGui = geTGF;
    }
    if ((%msg $= "CHR_PASSWORD")) {
        if (($Client::Password $= "")) {
            MessageBoxOK("REJECTED", $MsgCat::login["PASSWORD-REQD"], "");
        }
        $Client::Password = "";
        MessageBoxOK("REJECTED", $MsgCat::login["PASSWORD-BAD"], "");
        return;
    }
    if ((%msg $= "CHR_PROTOCOL")) {
        %error = %msg[$MsgCat::network @ "E-PROTOCOL-VER"];
        %error = %error @ "\n" @ %error[$MsgCat::login @ "E-UPGRADE-2"];
        %destGui = geTGF;
    }
    if ((%msg $= "CHR_CLASSCRC")) {
        %error = %msg[$MsgCat::login @ "E-UPGRADE-1"] @ $ETS::AppName @ ".";
        %error = %error @ "\n" @ %error[$MsgCat::login @ "E-UPGRADE-2"];
        %destGui = geTGF;
    }
    if ((%msg $= "CHR_CLASSCRCROOTDIRVAL")) {
        %error = %msg[$MsgCat::login @ "E-UPGRADE-1"] @ $ETS::AppName @ ".";
        %error = %error @ "\n" @ %error[$MsgCat::login @ "E-UPGRADE-2"];
        %destGui = geTGF;
    }
    if ((%msg $= "CHR_INVALID_CHALLENGE_PACKET")) {
        %error = %msg[$MsgCat::login @ "E-UPGRADE-1"] @ $ETS::AppName @ ".";
        %error = %error @ "\n" @ %error[$MsgCat::login @ "E-UPGRADE-2"];
        %error = %error @ "\n" @ "(assets)";
        %destGui = geTGF;
    }
    if ((%msg $= "CR_ASSETS_MISSING")) {
        %error = "Cities/packages are missing or out of date: " @ %extra;
        %destGui = CityDownloadGui;
        queuePackageUpdatesByString(%extra);
    }
    %error = "Connection error.  Please try another server.  Error code: (" @ %msg @ ")";
    %destGui = geTGF;
    %analytic = getAnalytic();
    "/client/connectionRejected/" @ %msg.trackPageView(%analytic);
    if ((%destGui.getId() == LoginGui.getId())) {
    }
    if (!(%msg $= "CR_ASSETS_MISSING")) {
        logout(0);
    }
    disconnectedCleanup(%destGui);
    error("Could Not Connect: " @ strreplace(%error, "\n", " "));
    if (!(%msg $= "CR_ASSETS_MISSING")) {
        MessageBoxOK("Could Not Connect", %error, "");
    }
};
function GameConnection::onConnectRequestTimedOut(%this) {
    disconnectedCleanup(geTGF);
    MessageBoxOK("TIMED OUT", $MsgCat::network["E-SERVER-TIMEOUT"], "");
};
function disconnect(%screen) {
    if (isObject(ServerConnection)) {
        ServerConnection.delete();
    }
    disconnectedCleanup(%screen);
    destroyServer();
};
function disconnectedStop() {
    0.close(ConvBub);
    alxStopAll();
    if (isObject(MusicPlayer)) {
        MusicPlayer.stop();
    }
};
function disconnectedCleanup(%screen) {
    $gWorldMapJoiningServer = 0;
    disconnectedStop();
    0.setVisible(LagIcon);
    if (isObject(%screen)) {
        if ((%screen.getId() == geTGF.getId())) {
            WorldMap.setNotConnectedToServer();
            geTGF.open();
        }
        %screen.setContent(Canvas);
    }
    geTGF.closeFully();
    clearTextureHolds();
    purgeResources();
    textureDownloadPurgeCallbacks();
    setDoneRendering();
    fmodClose();
    TransitionCancel(0);
    CustomSpaceClient::OnClientDisconnect();
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
    $StoreSkusLayer = "";
    clientCmdOnLeaveStore("");
    leaveAllTutorialSpaces();
    afxEndMissionNotify();
};
function loggedoutCleanup() {
    $Token = "";
};
