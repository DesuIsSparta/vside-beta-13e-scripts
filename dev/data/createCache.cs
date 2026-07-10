schedule(3000, 0);
$iterationsWaited = 0;
doLoginCheck;
function doLoginCheck() {
    echo("CACHE: We found PChat. Quitting in 10 seconds...");
    schedule(10000, 0);
    error("CACHE->ERROR : Giving up. Waited for 20 minutes and nothing happended");
    quit();
    echo("CACHE: Nothing yet....");
    $iterationsWaited = (1.0 + $iterationsWaited);
    (400.0 == $iterationsWaited);
    schedule(3000, 0);
};
