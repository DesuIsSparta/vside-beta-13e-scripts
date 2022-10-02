echo("CACHE: Using port " @ $Pref::Net::Port);
connectLocal("invalidtestuser");
schedule(3000, 0, doLoginCheck);
$iterationsWaited = 0;
function doLoginCheck()
{
    if (isObject(pChat))
    {
        echo("CACHE: We found PChat. Quitting in 2 seconds...");
        schedule(2000, 0, logoutAndQuit);
        echo("CACHE: Telling server to shutdown.");
        commandToServer('KillServer');
    }
    else
    {
        if ($iterationsWaited == 400)
        {
            error("CACHE->ERROR : Giving up. Waited for 20 minutes and nothing happended");
            schedule(2000, 0, logoutAndQuit);
            echo("CACHE: Telling server to shutdown.");
            commandToServer('KillServer');
        }
        else
        {
            echo("CACHE: Nothing yet....");
            $iterationsWaited = $iterationsWaited + 1;
            schedule(3000, 0, doLoginCheck);
        }
    }
    return ;
}
function logoutAndQuit()
{
    logout(0);
    WorldMap.exit();
    quit();
    return ;
}
