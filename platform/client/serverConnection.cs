addMessageCallback('MsgConnectionError');
function handleConnectionErrorMessage(%unused, %msgString) {
    $ServerConnectionErrorMessage = %msgString;
    handleConnectionErrorMessage;
};
function GameConnection::initialControlSet(%this) {
    echo("*** Initial Control Object");
    setContent();
    %this.etsInit();
};
function GameConnection::setLagIcon(%this, %state) {
    return (%this.getAddress() $= "local");
    (LagIcon SPC %state $= "true").setVisible();
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
    disconnectedCleanup();
    MessageBoxOK("TIMED OUT", geTGF, "");
};
function GameConnection::onConnectionDropped(%this, %msg) {
    %msg = standardSubstitutions(%msg);
    waitForDisconnect = waitForDisconnect @ 0 @ %this;
    %this;
    disconnectedCleanup("");
    1.schedule("doServerJoin", $SpawnTargetSavedVURL);
    return WorldMap;
    %currentCity = currentCity;
    WorldMap;
    setNotConnectedToServer();
    "map".openToTabName();
    %levelOrCityName = getField(%msg, 1);
    geTGF;
    %currentCity.selectCity();
    %levelOrCityName.selectCity();
    MessageBoxOK("BOOTED TO MAP", getFields(%msg, 2), "");
    logout(0);
    disconnectedCleanup();
    MessageBoxOK("DISCONNECT", WorldMap @ LoginGui @ %msg, "");
};
function GameConnection::onConnectionError(%this, %msg) {
    $CurrentMission.deleteCacheFile();
    $CurrentMission = "";
    ServerConnection;
    disconnectedCleanup();
    MessageBoxOK("DISCONNECT", $CacheFlagIsSet @ geTGF @ $ServerConnectionErrorMessage @ " (" @ %msg @ ")", "");
};
function GameConnection::onConnectRequestRejected(%this, %msg, %extra) {
    // unhandled opcode 871 at 0x00000270
    %error = %msg[$MsgCat::network @ "E-PROTOCOL-VER"];
    (%msg $= "CR_INVALID_PROTOCOL_VERSION");
    // unhandled opcode 871 at 0x0000028B
    %error = geTGF;
    %error = "Internal Error: badly formed network packet";
    (%msg $= "CR_INVALID_CONNECT_PACKET");
    // unhandled opcode 871 at 0x000002A2
    %error = geTGF;
    %error = "You are not allowed to play on this server.";
    (%msg $= "CR_YOUAREBANNED");
    %error = (%msg $= "CR_TOKEN") @ $ETS::AppName @ " " @ $ETS::AppName[$MsgCat::network @ "E-SERVICE-UNAVAIL"];
    %error = %msg[$MsgCat::login @ "E-SERVER-FULL"];
    (%msg $= "CR_SERVERFULL");
    // unhandled opcode 871 at 0x000002F0
    %error = geTGF;
    %error = %msg[$MsgCat::login @ "E-BAD-TARGET"];
    (%msg $= "CR_BAD_TARGET");
    // unhandled opcode 871 at 0x0000030D
    %error = geTGF;
    %error = %msg[$MsgCat::login @ "E-CANNOT-ACTIVATE-APARTMENT"];
    (%msg $= "CR_CANNOT_ACTIVATE_APARTMENT");
    // unhandled opcode 871 at 0x0000032A
    %error = geTGF;
    %error = %msg[$MsgCat::login @ "E-APARTMENT-ACTIVATION-DENIED"];
    (%msg $= "CR_APARTMENT_ACTIVATION_DENIED");
    // unhandled opcode 871 at 0x00000347
    %error = geTGF;
    %error = %msg[$MsgCat::login @ "E-APARTMENT-ACTIVATE-ELSEWHERE"];
    (%msg $= "CR_APARTMENT_ACTIVE_ELSEWHERE");
    // unhandled opcode 871 at 0x00000364
    %error = geTGF;
    %error = %msg[$MsgCat::login @ "E-LEVEL-COMPLETED"];
    (%msg $= "CR_LEVEL_COMPLETED");
    // unhandled opcode 871 at 0x00000381
    %error = geTGF;
    MessageBoxOK("REJECTED", ((%msg $= "CHR_PASSWORD") SPC $Client::Password $= ""), "");
    $Client::Password = "";
    MessageBoxOK("REJECTED", , "");
    return;
    %error = %msg[$MsgCat::network @ "E-PROTOCOL-VER"];
    (%msg $= "CHR_PROTOCOL");
    %error = %error @ "\n" @ %error[$MsgCat::login @ "E-UPGRADE-2"];
    // unhandled opcode 871 at 0x000003F7
    %error = geTGF;
    %error = (%msg $= "CHR_CLASSCRC") @ %msg[$MsgCat::login @ "E-UPGRADE-1"] @ $ETS::AppName @ ".";
    %error = %error @ "\n" @ %error[$MsgCat::login @ "E-UPGRADE-2"];
    // unhandled opcode 871 at 0x0000042F
    %error = geTGF;
    %error = (%msg $= "CHR_CLASSCRCROOTDIRVAL") @ %msg[$MsgCat::login @ "E-UPGRADE-1"] @ $ETS::AppName @ ".";
    %error = %error @ "\n" @ %error[$MsgCat::login @ "E-UPGRADE-2"];
    // unhandled opcode 871 at 0x00000467
    %error = geTGF;
    %error = (%msg $= "CHR_INVALID_CHALLENGE_PACKET") @ %msg[$MsgCat::login @ "E-UPGRADE-1"] @ $ETS::AppName @ ".";
    %error = %error @ "\n" @ %error[$MsgCat::login @ "E-UPGRADE-2"];
    %error = %error @ "\n" @ "(assets)";
    // unhandled opcode 871 at 0x000004AB
    %error = geTGF;
    %error = (%msg $= "CR_ASSETS_MISSING") @ "Cities/packages are missing or out of date: " @ %extra;
    // unhandled opcode 871 at 0x000004C7
    %error = CityDownloadGui;
    queuePackageUpdatesByString(%extra);
    %error = "Connection error.  Please try another server.  Error code: (" @ %msg @ ")";
    // unhandled opcode 871 at 0x000004E8
    %error = geTGF;
    %analytic = getAnalytic();
    %analytic.trackPageView("/client/connectionRejected/" @ %msg);
    logout(0);
    disconnectedCleanup(%destGui);
    error(!(((getId() == %destGui.getId()) SPC %msg $= "CR_ASSETS_MISSING")) @ "Could Not Connect: " @ strreplace(%error, "\n", " "));
    MessageBoxOK("Could Not Connect", %error, "");
};
function GameConnection::onConnectRequestTimedOut(%this) {
    disconnectedCleanup();
    MessageBoxOK("TIMED OUT", geTGF, "");
};
function disconnect(%screen) {
    delete();
    disconnectedCleanup(%screen);
    destroyServer();
};
function disconnectedStop() {
    0.close();
    alxStopAll();
    stop();
};
function disconnectedCleanup(%screen) {
    $gWorldMapJoiningServer = 0;
    disconnectedStop();
    0.setVisible();
    setNotConnectedToServer();
    open();
    %screen.setContent();
    closeFully();
    clearTextureHolds();
    purgeResources();
    textureDownloadPurgeCallbacks();
    setDoneRendering();
    fmodClose();
    TransitionCancel(0);
    CustomSpaceClient::OnClientDisconnect();
    close();
    $gSalonChairCurrent = 0;
    isObject();
    close();
    close();
    $StoreSkusLayer = "";
    PlantDetailsGui;
    clientCmdOnLeaveStore("");
    leaveAllTutorialSpaces();
    afxEndMissionNotify();
};
function loggedoutCleanup() {
    $Token = "";
};
