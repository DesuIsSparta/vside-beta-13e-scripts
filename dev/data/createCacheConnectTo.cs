echo("CACHE: Using port " @ $Pref::Net::Port);
connectLocal("invalidtestuser");
schedule(3000, 0);
$iterationsWaited = 0;
doLoginCheck;
function doLoginCheck() {
    if (isObject(pChat)) {
        echo("CACHE: We found PChat. Quitting in 2 seconds...");
        schedule(2000, 0);
        echo("CACHE: Telling server to shutdown.");
        commandToServer('KillServer');
    }
    if (($iterationsWaited == 400.0)) {
        error("CACHE->ERROR : Giving up. Waited for 20 minutes and nothing happended");
        schedule(2000, 0);
        echo("CACHE: Telling server to shutdown.");
        commandToServer('KillServer');
    }
    echo("CACHE: Nothing yet....");
    $iterationsWaited = ($iterationsWaited + 1.0);
    logoutAndQuit;
    schedule(3000, 0);
};
function logoutAndQuit() {
    logout(0);
    WorldMap.exit();
    quit();
};
