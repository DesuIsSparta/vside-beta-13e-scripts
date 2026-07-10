addMessageCallback('MsgConnectionError');
function handleConnectionErrorMessage(%unused, %msgString) {
    $ServerConnectionErrorMessage = %msgString;
    handleConnectionErrorMessage;
};
function GameConnection::initialControlSet(%this) {
    echo("*** Initial Control Object");
    if (!(isObject(EditorGui))) {
    }
    if (!(Editor::checkActiveLoadDone())) {
        if ((PlayGui.getId() != Canvas.getContent())) {
            Canvas.setContent(PlayGui);
        }
    }
    %this.etsInit();
};
function GameConnection::setLagIcon(%this, %state) {
    if ((%this.getAddress() $= "local")) {
        return;
    }
    (LagIcon @ " " @ %state $= "true").setVisible();
};
function GameConnection::onConnectionAccepted(%this) {
    0.setVisible();
    $GameConnection = %this;
    LagIcon;
    $VURLcmd = "";
    getUserActivityMgr().setActivityActive("traveling", 1);
};
function GameConnection::onServerConnectionPossiblyTimingOut(%this) {
    warn("Possibly losing connection to server..");
};
function GameConnection::onServerConnectionRestored(%this) {
    warn("Restored connection to server.");
};
function GameConnection::onServerConnectionTimedOut(%this) {
    disconnectedCleanup(geTGF);
    MessageBoxOK("TIMED OUT", , "");
};
function GameConnection::onConnectionDropped(%this, %msg) {
    %msg = standardSubstitutions(%msg);
    if (%this.waitForDisconnect) {
        %this.waitForDisconnect = 0;
        disconnectedCleanup("");
        1.schedule("doServerJoin", $SpawnTargetSavedVURL);
        return WorldMap;
    }
    if ((getField(%msg, 0) $= "bootToMap")) {
        %currentCity = %this.currentCity;
        WorldMap;
        WorldMap.setNotConnectedToServer();
        "map".openToTabName();
        %levelOrCityName = getField(%msg, 1);
        geTGF;
        if ((%levelOrCityName $= 0)) {
        }
        if ((%levelOrCityName $= 1)) {
            %currentCity.selectCity();
        }
        %levelOrCityName.selectCity();
        MessageBoxOK("BOOTED TO MAP", getFields(%msg, 2), "");
    }
    logout(0);
    disconnectedCleanup(LoginGui);
    MessageBoxOK("DISCONNECT", WorldMap @ %msg, "");
};
function GameConnection::onConnectionError(%this, %msg) {
    if ($CacheFlagIsSet) {
        $CurrentMission.deleteCacheFile();
        $CurrentMission = "";
        ServerConnection;
    }
    disconnectedCleanup(geTGF);
    MessageBoxOK("DISCONNECT", $ServerConnectionErrorMessage @ " (" @ %msg @ ")", "");
};
function GameConnection::onConnectRequestRejected(%this, %msg, %extra) {
    // unhandled opcode 871 at 0x00000270
    if ((%msg $= "CR_INVALID_PROTOCOL_VERSION")) {
        %error = %msg[$MsgCat::network @ "E-PROTOCOL-VER"];
        // unhandled opcode 871 at 0x0000028B
        %error = geTGF;
    }
    if ((%msg $= "CR_INVALID_CONNECT_PACKET")) {
        %error = "Internal Error: badly formed network packet";
        // unhandled opcode 871 at 0x000002A2
        %error = geTGF;
    }
    if ((%msg $= "CR_YOUAREBANNED")) {
        %error = "You are not allowed to play on this server.";
    }
    if ((%msg $= "CR_TOKEN")) {
        %error = $ETS::AppName @ " " @ $ETS::AppName[$MsgCat::network @ "E-SERVICE-UNAVAIL"];
    }
    if ((%msg $= "CR_SERVERFULL")) {
        %error = %msg[$MsgCat::login @ "E-SERVER-FULL"];
        // unhandled opcode 871 at 0x000002F0
        %error = geTGF;
    }
    if ((%msg $= "CR_BAD_TARGET")) {
        %error = %msg[$MsgCat::login @ "E-BAD-TARGET"];
        // unhandled opcode 871 at 0x0000030D
        %error = geTGF;
    }
    if ((%msg $= "CR_CANNOT_ACTIVATE_APARTMENT")) {
        %error = %msg[$MsgCat::login @ "E-CANNOT-ACTIVATE-APARTMENT"];
        // unhandled opcode 871 at 0x0000032A
        %error = geTGF;
    }
    if ((%msg $= "CR_APARTMENT_ACTIVATION_DENIED")) {
        %error = %msg[$MsgCat::login @ "E-APARTMENT-ACTIVATION-DENIED"];
        // unhandled opcode 871 at 0x00000347
        %error = geTGF;
    }
    if ((%msg $= "CR_APARTMENT_ACTIVE_ELSEWHERE")) {
        %error = %msg[$MsgCat::login @ "E-APARTMENT-ACTIVATE-ELSEWHERE"];
        // unhandled opcode 871 at 0x00000364
        %error = geTGF;
    }
    if ((%msg $= "CR_LEVEL_COMPLETED")) {
        %error = %msg[$MsgCat::login @ "E-LEVEL-COMPLETED"];
        // unhandled opcode 871 at 0x00000381
        %error = geTGF;
    }
    if ((%msg $= "CHR_PASSWORD")) {
        if (($Client::Password $= "")) {
            MessageBoxOK("REJECTED", , "");
        }
        $Client::Password = "";
        MessageBoxOK("REJECTED", , "");
        return;
    }
    if ((%msg $= "CHR_PROTOCOL")) {
        %error = %msg[$MsgCat::network @ "E-PROTOCOL-VER"];
        %error = %error @ "\n" @ %error[$MsgCat::login @ "E-UPGRADE-2"];
        // unhandled opcode 871 at 0x000003F7
        %error = geTGF;
    }
    if ((%msg $= "CHR_CLASSCRC")) {
        %error = %msg[$MsgCat::login @ "E-UPGRADE-1"] @ $ETS::AppName @ ".";
        %error = %error @ "\n" @ %error[$MsgCat::login @ "E-UPGRADE-2"];
        // unhandled opcode 871 at 0x0000042F
        %error = geTGF;
    }
    if ((%msg $= "CHR_CLASSCRCROOTDIRVAL")) {
        %error = %msg[$MsgCat::login @ "E-UPGRADE-1"] @ $ETS::AppName @ ".";
        %error = %error @ "\n" @ %error[$MsgCat::login @ "E-UPGRADE-2"];
        // unhandled opcode 871 at 0x00000467
        %error = geTGF;
    }
    if ((%msg $= "CHR_INVALID_CHALLENGE_PACKET")) {
        %error = %msg[$MsgCat::login @ "E-UPGRADE-1"] @ $ETS::AppName @ ".";
        %error = %error @ "\n" @ %error[$MsgCat::login @ "E-UPGRADE-2"];
        %error = %error @ "\n" @ "(assets)";
        // unhandled opcode 871 at 0x000004AB
        %error = geTGF;
    }
    if ((%msg $= "CR_ASSETS_MISSING")) {
        %error = "Cities/packages are missing or out of date: " @ %extra;
        // unhandled opcode 871 at 0x000004C7
        %error = CityDownloadGui;
        queuePackageUpdatesByString(%extra);
    }
    %error = "Connection error.  Please try another server.  Error code: (" @ %msg @ ")";
    // unhandled opcode 871 at 0x000004E8
    %error = geTGF;
    %analytic = getAnalytic();
    %analytic.trackPageView("/client/connectionRejected/" @ %msg);
    if ((LoginGui.getId() == %destGui.getId())) {
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
    MessageBoxOK("TIMED OUT", , "");
};
function disconnect(%screen) {
    if (isObject(ServerConnection)) {
        ServerConnection.delete();
    }
    disconnectedCleanup(%screen);
    destroyServer();
};
function disconnectedStop() {
    0.close();
    alxStopAll();
    if (isObject(MusicPlayer)) {
        MusicPlayer.stop();
    }
};
function disconnectedCleanup(%screen) {
    $gWorldMapJoiningServer = 0;
    disconnectedStop();
    0.setVisible();
    if (isObject(%screen)) {
        if ((geTGF.getId() == %screen.getId())) {
            WorldMap.setNotConnectedToServer();
            geTGF.open();
        }
        %screen.setContent();
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
        Canvas;
        SalonStyleSelector.close();
    }
    if (isObject(PlantDetailsGui)) {
        PlantDetailsGui.close();
    }
    $StoreSkusLayer = "";
    LagIcon;
    clientCmdOnLeaveStore("");
    leaveAllTutorialSpaces();
    afxEndMissionNotify();
};
function loggedoutCleanup() {
    $Token = "";
};
