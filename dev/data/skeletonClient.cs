function skeletonClient_postJoinAction()
{
    if (isObject($player))
    {
        echo("LOAD: Logged in. Calling " @ $skeletonClient::joinAction);
        schedule(2000, 0, skeletonClient_doPostJoinAction);
    }
    else
    {
        if ($iterationsWaited == 200)
        {
            error("LOAD: Giving up. Waited for 10 minutes and nothing happened.");
            skeletonClient::quit();
        }
        else
        {
            echo("LOAD: Waiting ...");
            $iterationsWaited = $iterationsWaited + 1;
            schedule(3000, 0, skeletonClient_postJoinAction);
        }
    }
    return ;
}
function fakeFrameCount()
{
    $Canvas::frameCount = $Canvas::frameCount + 1;
    schedule(500, 0, fakeFrameCount);
    return ;
}
function skeletonClient_doPostJoinAction()
{
    if (!($skeletonClient::joinAction $= ""))
    {
        call($skeletonClient::joinAction);
    }
    return ;
}
function skeletonClient::init(%this)
{
    $skeletonClient::targetCity = "";
    $skeletonClient::joinAction = "";
    $skeletonClient::quitOnError = 0;
    $skeletonClient::bootAttempted = 0;
    $Login::loggedIn = 0;
    $iterationsWaited = 0;
    if (!(%this.quitOnError $= ""))
    {
        $skeletonClient::quitOnError = %this.quitOnError;
    }
    if (!(%this.joinAction $= ""))
    {
        $skeletonClient::joinAction = %this.joinAction;
    }
    skeletonClient::initSpawnPoints();
    schedule(500, 0, fakeFrameCount);
    return ;
}
function skeletonClient::initSpawnPoints(%this)
{
    %i = 0;
    $Spawns[%i = %i + 1] = "DanceFloorSpawns";
    $Spawns[%i = %i + 1] = "PlazaSpawns";
    $Spawns[%i = %i + 1] = "ShoppingSpawns";
    $Spawns[%i = %i + 1] = "LoungeSpawns";
    $Spawns[%i = %i + 1] = "RailwaySpawns";
    $Spawns[%i = %i + 1] = "LobbySpawns_NV255Lofts";
    $Spawns[%i = %i + 1] = "GariSpawns";
    $Spawns[%i = %i + 1] = "LAXSpawns";
    $Spawns[%i = %i + 1] = "DanceFloorSpawns";
    $Spawns[%i = %i + 1] = "ShoppingSpawns_sf1972";
    $Spawns[%i = %i + 1] = "TeaHouseSpawns";
    $Spawns[%i = %i + 1] = "SkyBarSpawns";
    $SpawnsCount = %i;
    return ;
}
function skeletonClient::getHWSpawns()
{
    %spawnNum = getRandom(1, $SpawnsCount);
    return $Spawns[%spawnNum];
}
function doLoginButton()
{
    LoginGui.doLoginButton();
    return ;
}
function skeletonClient::doLogin(%this, %destinationCity)
{
    if (!(%destinationCity $= ""))
    {
        $skeletonClient::targetCity = %destinationCity;
    }
    else
    {
        $skeletonClient::targetCity = "NewVeneziaNorth";
    }
    echo("LOAD: Setting targetCity to " @ $skeletonClient::targetCity);
    LoginUserNameField.setValue(%this.userName);
    LoginPasswordField.setValue(%this.password);
    LoginGui.isAwake();
    LoginGui.doLoginButton();
    %this.schedule(1000, checkStatus);
    return ;
}
function skeletonClient::checkStatus()
{
    if (!isObject(LoginRequest))
    {
        echo("LOAD: No LoginRequest object yet. Trying again in 5 seconds.");
        schedule(7000, skeletonClient, checkStatus);
        return ;
    }
    return ;
}
function GameConnection::onConnectionDropped(%this, %msg)
{
    echo("LOAD: The server has dropped the connection: " @ %msg);
    if ($Login::loggedIn)
    {
        skeletonClient::logoffAndQuit();
    }
    return ;
}
function GameConnection::onServerConnectionTimedOut(%this)
{
    echo("LOAD: We\'re disconnected for some unknown reason.");
    skeletonClient::logoffAndQuit();
    return ;
}
function GameConnection::onConnectRequestRejected(%this)
{
    echo("LOAD: We\'re rejected for some reason reason.");
    skeletonClient::logoffAndQuit();
    return ;
}
function BootRequest::onDone(%this)
{
    log("login", "debug", "LOAD: BootRequest::onDone");
    if (%this.statusCode() != $HTTP::StatusOK)
    {
        error("LOAD: Client HTTP code: " @ %this.statusCode());
        %this.quit();
    }
    %status = findRequestStatus(%this);
    log("login", "info", "LOAD: BootRequest::onDone status:" SPC %status);
    if (%status $= "success")
    {
        echo("LOAD: Boot suceeded.");
        schedule(2000, 0, doLoginButton);
    }
    else
    {
        if (%status $= "fail")
        {
            echo("LOAD: Boot failed.");
            skeletonClient::quit();
        }
        else
        {
            if (%status $= "error")
            {
                echo("LOAD: Boot errored.");
                skeletonClient::quit();
            }
        }
    }
    return ;
}
function LoginRequest::onError(%this, %errorNum, %unused)
{
    if (%errorNum == $CURL::CouldNotResolveHost)
    {
        echo("LOAD: CURL::CouldNotResolveHost");
    }
    else
    {
        echo("LOAD: OtherError");
    }
    echo("LOAD: Couldn\'t login to envmanager. Giving up.");
    skeletonClient::quit();
    return ;
}
function LoginRequest::onDone(%this)
{
    log("login", "debug", "LOAD: LoginRequest::onDone");
    if (%this.statusCode() != $HTTP::StatusOK)
    {
        error("LOAD: Client HTTP code: " @ %this.statusCode());
        skeletonClient::quit();
    }
    %status = strlwr(findRequestStatus(%this));
    log("login", "debug", "LOAD: LoginRequest::onDone status: " @ %status);
    if ((%status $= "fail") && (%status $= "error"))
    {
        %errorCode = %this.getValue("errorCode");
        %errorCode = strlwr(%errorCode);
        log("login", "error", "LOAD: errorCode = " @ %errorCode);
        if (%errorCode $= "alreadyloggedin")
        {
            if ($skeletonClient::bootAttempted == 0)
            {
                echo("LOAD: Test login auto-booting from previously joined server");
                LoginRequest::handleBoot();
                $skeletonClient::bootAttempted = 1;
                %this.schedule(1000, skeletonClient, checkStatus);
            }
            else
            {
                error("LOAD: Boot failed. Giving up.");
                echo("LOAD: Quit()-ing...");
                skeletonClient::quit();
            }
        }
        else
        {
            error("LOAD: Login [" @ $UserPref::Player::Name @ "/" @ $UserPref::Player::Password @ "] failed due to " @ LoginRequest.loginResult);
            skeletonClient::quit();
        }
    }
    else
    {
        if (%status $= "success")
        {
            %this.parseResponse();
            outfits_init();
            outfits_retrieve();
            WorldMap.setNotConnectedToServer();
            WorldMap.initCityMaps();
            WorldMap.open();
            $Login::loggedIn = 1;
            schedule(2000, 0, joinServer);
        }
        else
        {
            if (%status $= "upgrade_available")
            {
                %this.parseResponse();
                outfits_init();
                outfits_retrieve();
                WorldMap.setNotConnectedToServer();
                WorldMap.initCityMaps();
                WorldMap.open();
                $Login::loggedIn = 1;
                schedule(2000, 0, joinServer);
            }
        }
    }
    return ;
}
function joinServer()
{
    return skeletonClient::joinServer();
}
function skeletonClient::joinServer()
{
    echo("LOAD: count:" SPC WorldMapServers.getCount());
    if (WorldMapServers.getCount() == 0)
    {
        echo("LOAD: We got 0 servers. Trying again in 5 seconds.");
        schedule(5000, 0, joinServer);
        return ;
    }
    if ("vside:" $= getSubStr($skeletonClient::targetCity, 0, 6))
    {
        echo("LOAD: Using vurl " @ $skeletonClient::targetCity);
        vurlOperation($skeletonClient::targetCity);
        schedule(15000, 0, skeletonClient_postJoinAction);
        WorldMap.close();
        return ;
    }
    else
    {
        if ($skeletonClient::targetCity $= "MyApartment")
        {
            echo("LOAD: Logging into my apartment!");
            WorldMap.close();
            doTeleportToMyApartment();
            schedule(15000, 0, skeletonClient_postJoinAction);
            return ;
        }
    }
    %spawn = skeletonClient::getHWSpawns();
    %targetVurl = "vside:/location/generic/" @ %spawn;
    echo("LOAD: Using spawn point" SPC %spawn SPC "in" SPC $skeletonClient::targetCity);
    %foundCity = 0;
    %i = 0;
    while (%i < WorldMapServers.getCount())
    {
        if (WorldMapServers.getObject(%i).get("name") $= $skeletonClient::targetCity)
        {
            %foundCity = 1;
            WorldMap.join(WorldMapServers.getObject(%i), 0, %targetVurl);
            echo("LOAD: Joined server " @ WorldMapServers.getObject(%i).get("name"));
            echo("LOAD: Login completed");
            schedule(15000, 0, skeletonClient_postJoinAction);
            break;
        }
        %i = %i + 1;
    }
    if (!%foundCity)
    {
        echo("LOAD: Did not find targetCity " @ $skeletonClient::targetCity @ ". Giving up.");
        skeletonClient::quit();
    }
    return ;
}
function skeletonClient::reallyQuit(%this)
{
    echo("LOAD: Quit()-ing...");
    quit();
    return ;
}
function skeletonClient::quit(%this)
{
    echo("quit?: " @ $skeletonClient::quitOnError);
    if ($skeletonClient::quitOnError $= "true")
    {
        echo("LOAD: Quit()-ing...");
        quit();
    }
    return ;
}
function skeletonClient::logoffAndQuit()
{
    echo("LOAD: Logging off and quit()-ing...");
    echo("LOAD: Login::loggedIn:" SPC $Login::loggedIn);
    logout(0);
    schedule(1000, 0, doQuit);
    return ;
}
if ($Platform $= "x86UNIX")
{
    exec("./skeletonClient_linux.cs");
}
function useAndSaveRandomOutfit()
{
    %drwrs = SkuManager.commonDrawers();
    %skus = SkuManager.getRandomSkusForLocalPlayer(%drwrs);
    commandToServer('SetActiveSkus', %skus);
    return ;
}
