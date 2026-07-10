schedule(3000, 0);
$iterationsWaited = 0;
doLoginCheck;
function doLoginCheck() {
    if (isObject(pChat)) {
        echo("CACHE: We found PChat. Quitting in 10 seconds...");
        schedule(10000, 0);
    }
    if ((400.0 == $iterationsWaited)) {
        error("CACHE->ERROR : Giving up. Waited for 20 minutes and nothing happended");
        quit();
    }
    echo("CACHE: Nothing yet....");
    $iterationsWaited = (1.0 + $iterationsWaited);
    quit;
    schedule(3000, 0);
};
