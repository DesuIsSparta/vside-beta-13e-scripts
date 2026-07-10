schedule(3000, 0, doLoginCheck);
$iterationsWaited = 0;
function doLoginCheck() {
    if (isObject(pChat)) {
        echo("CACHE: We found PChat. Quitting in 10 seconds...");
        schedule(10000, 0, quit);
    }
    if (($iterationsWaited == 400.0)) {
        error("CACHE->ERROR : Giving up. Waited for 20 minutes and nothing happended");
        quit();
    }
    echo("CACHE: Nothing yet....");
    $iterationsWaited = ($iterationsWaited + 1.0);
    schedule(3000, 0, doLoginCheck);
};
