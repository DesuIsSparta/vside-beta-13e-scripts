function skeletonClient_postJoinAction() {
    if (isObject($player)) {
        echo("LOAD: Logged in. Calling " @ $skeletonClient::joinAction);
        schedule(2000, 0);
    }
    if ((200.0 == $iterationsWaited)) {
        error("LOAD: Giving up. Waited for 10 minutes and nothing happened.");
        skeletonClient::quit();
    }
    echo("LOAD: Waiting ...");
    $iterationsWaited = (1.0 + $iterationsWaited);
    skeletonClient_doPostJoinAction;
    schedule(3000, 0);
};
function fakeFrameCount() {
    $Canvas::frameCount = (1.0 + $Canvas::frameCount);
    schedule(500, 0);
};
function skeletonClient_doPostJoinAction() {
    if (!($skeletonClient::joinAction $= "")) {
        call($skeletonClient::joinAction);
    }
};
function skeletonClient::init(%this) {
    $skeletonClient::targetCity = "";
    $skeletonClient::joinAction = "";
    $skeletonClient::quitOnError = 0;
    $skeletonClient::bootAttempted = 0;
    $Login::loggedIn = 0;
    $iterationsWaited = 0;
    if (!(%this.quitOnError $= "")) {
        $skeletonClient::quitOnError = %this.quitOnError;
    }
    if (!(%this.joinAction $= "")) {
        $skeletonClient::joinAction = %this.joinAction;
    }
    skeletonClient::initSpawnPoints();
    schedule(500, 0);
};
function skeletonClient::initSpawnPoints(%this) {
    %i = 0;
    %i = (1.0 + %i);
    %i["DanceFloorSpawns" @ $Spawns] = ;
    %i = (1.0 + %i);
    %i["PlazaSpawns" @ $Spawns] = ;
    %i = (1.0 + %i);
    %i["ShoppingSpawns" @ $Spawns] = ;
    %i = (1.0 + %i);
    %i["LoungeSpawns" @ $Spawns] = ;
    %i = (1.0 + %i);
    %i["RailwaySpawns" @ $Spawns] = ;
    %i = (1.0 + %i);
    %i["LobbySpawns_NV255Lofts" @ $Spawns] = ;
    %i = (1.0 + %i);
    %i["GariSpawns" @ $Spawns] = ;
    %i = (1.0 + %i);
    %i["LAXSpawns" @ $Spawns] = ;
    %i = (1.0 + %i);
    %i["DanceFloorSpawns" @ $Spawns] = ;
    %i = (1.0 + %i);
    %i["ShoppingSpawns_sf1972" @ $Spawns] = ;
    %i = (1.0 + %i);
    %i["TeaHouseSpawns" @ $Spawns] = ;
    %i = (1.0 + %i);
    %i["SkyBarSpawns" @ $Spawns] = ;
    $SpawnsCount = %i;
};
function skeletonClient::getHWSpawns() {
    %spawnNum = getRandom(1, $SpawnsCount);
    return %spawnNum[$Spawns @ %spawnNum];
};
function doLoginButton() {
    LoginGui.doLoginButton();
};
function skeletonClient::doLogin(%this, %destinationCity) {
    if (!(%destinationCity $= "")) {
        $skeletonClient::targetCity = %destinationCity;
    }
    $skeletonClient::targetCity = "NewVeneziaNorth";
    echo("LOAD: Setting targetCity to " @ $skeletonClient::targetCity);
    %this.userName.setValue();
    %this.password.setValue();
    LoginGui.isAwake();
    LoginGui.doLoginButton();
    %this.schedule(1000);
};
function skeletonClient::checkStatus() {
    if (!(isObject(LoginRequest))) {
        echo("LOAD: No LoginRequest object yet. Trying again in 5 seconds.");
        schedule(7000);
        return checkStatus;
    }
};
function GameConnection::onConnectionDropped(%this, %msg) {
    echo("LOAD: The server has dropped the connection: " @ %msg);
    if ($Login::loggedIn) {
        skeletonClient::logoffAndQuit();
    }
};
function GameConnection::onServerConnectionTimedOut(%this) {
    echo("LOAD: We're disconnected for some unknown reason.");
    skeletonClient::logoffAndQuit();
};
function GameConnection::onConnectRequestRejected(%this) {
    echo("LOAD: We're rejected for some reason reason.");
    skeletonClient::logoffAndQuit();
};
function BootRequest::onDone(%this) {
    log("login", "debug", "LOAD: BootRequest::onDone");
    if (($HTTP::StatusOK != %this.statusCode())) {
        error("LOAD: Client HTTP code: " @ %this.statusCode());
        %this.quit();
    }
    %status = findRequestStatus(%this);
    log("login", "info", "LOAD: BootRequest::onDone status:" @ " " @ %status);
    if ((%status $= "success")) {
        echo("LOAD: Boot suceeded.");
        schedule(2000, 0);
    }
    if ((doLoginButton @ " " @ %status $= "fail")) {
        echo("LOAD: Boot failed.");
        skeletonClient::quit();
    }
    if ((%status $= "error")) {
        echo("LOAD: Boot errored.");
        skeletonClient::quit();
    }
};
function LoginRequest::onError(%this, %errorNum, %unused) {
    if (($CURL::CouldNotResolveHost == %errorNum)) {
        echo("LOAD: CURL::CouldNotResolveHost");
    }
    echo("LOAD: OtherError");
    echo("LOAD: Couldn't login to envmanager. Giving up.");
    skeletonClient::quit();
};
function LoginRequest::onDone(%this) {
    log("login", "debug", "LOAD: LoginRequest::onDone");
    if (($HTTP::StatusOK != %this.statusCode())) {
        error("LOAD: Client HTTP code: " @ %this.statusCode());
        skeletonClient::quit();
    }
    %status = strlwr(findRequestStatus(%this));
    log("login", "debug", "LOAD: LoginRequest::onDone status: " @ %status);
    if ((%status $= "fail")) {
    }
    if ((%status $= "error")) {
        %errorCode = %this.getValue("errorCode");
        %errorCode = strlwr(%errorCode);
        log("login", "error", "LOAD: errorCode = " @ %errorCode);
        if ((%errorCode $= "alreadyloggedin")) {
            if ((0.0 == $skeletonClient::bootAttempted)) {
                echo("LOAD: Test login auto-booting from previously joined server");
                LoginRequest::handleBoot();
                $skeletonClient::bootAttempted = 1;
                %this.schedule(1000);
            }
            error("LOAD: Boot failed. Giving up.");
            echo("LOAD: Quit()-ing...");
            skeletonClient::quit();
        }
        error(LoginRequest @ %this.loginResult);
        skeletonClient::quit();
    }
    if (("LOAD: Login [" @ $UserPref::Player::Name @ "/" @ $UserPref::Player::Password @ "] failed due to " @ " " @ %status $= "success")) {
        %this.parseResponse();
        outfits_init();
        outfits_retrieve();
        WorldMap.setNotConnectedToServer();
        WorldMap.initCityMaps();
        WorldMap.open();
        $Login::loggedIn = 1;
        checkStatus;
        schedule(2000, 0);
    }
    if ((joinServer @ " " @ %status $= "upgrade_available")) {
        %this.parseResponse();
        outfits_init();
        outfits_retrieve();
        WorldMap.setNotConnectedToServer();
        WorldMap.initCityMaps();
        WorldMap.open();
        $Login::loggedIn = 1;
        skeletonClient;
        schedule(2000, 0);
    }
};
function joinServer() {
    return skeletonClient::joinServer();
};
function skeletonClient::joinServer() {
    echo("LOAD: count:" @ " " @ WorldMapServers.getCount());
    if ((0.0 == WorldMapServers.getCount())) {
        echo("LOAD: We got 0 servers. Trying again in 5 seconds.");
        schedule(5000, 0);
        return joinServer;
    }
    if (("vside:" $= getSubStr($skeletonClient::targetCity, 0, 6))) {
        echo("LOAD: Using vurl " @ $skeletonClient::targetCity);
        vurlOperation($skeletonClient::targetCity);
        schedule(15000, 0);
        WorldMap.close();
        return skeletonClient_postJoinAction;
    }
    if (($skeletonClient::targetCity $= "MyApartment")) {
        echo("LOAD: Logging into my apartment!");
        WorldMap.close();
        doTeleportToMyApartment();
        schedule(15000, 0);
        return skeletonClient_postJoinAction;
    }
    %spawn = skeletonClient::getHWSpawns();
    %targetVurl = "vside:/location/generic/" @ %spawn;
    echo("LOAD: Using spawn point" @ " " @ %spawn @ " " @ "in" @ " " @ $skeletonClient::targetCity);
    %foundCity = 0;
    %i = 0;
    if ((WorldMapServers.getCount() < %i)) {
        if ((WorldMapServers @ " " @ %i.getObject().get("name") $= $skeletonClient::targetCity)) {
            %foundCity = 1;
            %i.getObject().join(0, %targetVurl);
            echo(WorldMapServers @ %i.getObject().get("name"));
            echo("LOAD: Login completed");
            schedule(15000, 0);
        }
        %i = (1.0 + %i);
        skeletonClient_postJoinAction;
    }
    if (!(%foundCity)) {
        echo("LOAD: Did not find targetCity " @ $skeletonClient::targetCity @ ". Giving up.");
        skeletonClient::quit();
    }
};
function skeletonClient::reallyQuit(%this) {
    echo("LOAD: Quit()-ing...");
    quit();
};
function skeletonClient::quit(%this) {
    echo("quit?: " @ $skeletonClient::quitOnError);
    if (($skeletonClient::quitOnError $= "true")) {
        echo("LOAD: Quit()-ing...");
        quit();
    }
};
function skeletonClient::logoffAndQuit() {
    echo("LOAD: Logging off and quit()-ing...");
    echo("LOAD: Login::loggedIn:" @ " " @ $Login::loggedIn);
    logout(0);
    schedule(1000, 0);
};
if (($Platform $= "x86UNIX")) {
    exec("./skeletonClient_linux.cs");
}
function useAndSaveRandomOutfit() {
    %drwrs = SkuManager.commonDrawers();
    %skus = %drwrs.getRandomSkusForLocalPlayer();
    SkuManager;
    commandToServer('SetActiveSkus', %skus);
};
