schedule(3000, 0, doLoginCheck);
$iterationsWaited = 0;
function doLoginCheck()
{
    if (isObject(pChat))
    {
        echo("CACHE: We found PChat. Quitting in 10 seconds...");
        schedule(10000, 0, quit);
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
