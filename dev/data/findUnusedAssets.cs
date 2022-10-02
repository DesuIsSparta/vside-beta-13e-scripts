schedule(3000, 0, doLoginCheck);
$iterationsWaited = 0;
$gSpaceNumber = 0;
$gSpaceNumberMax = 5;
function teleportToNextSpace()
{
    teleportToSpaceLocal($gSpaceNumber);
    return ;
}
function teleportToSpaceLocal(%space)
{
    if (%space >= $gSpaceNumberMax)
    {
        schedule(5000, 0, quit);
    }
    teleportToSpaceNumber(%space);
    schedule(5000, 0, teleportToNextSpace);
    $gSpaceNumber = $gSpaceNumber + 1;
    return ;
}
function doLoginCheck()
{
    if (isObject(pChat))
    {
        if (!(MissionInfo.mode $= "PrivateSpaceGrid"))
        {
            echo("Not a grid. Quiting...");
            schedule(3000, 0, quit);
        }
        teleportToNextSpace(0);
    }
    else
    {
        if ($iterationsWaited == 400)
        {
            error("CACHE->ERROR : Giving up. Waited for 20 minutes and nothing happended");
            quit();
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
