schedule(3000, 0);
$iterationsWaited = 0;
doLoginCheck;
$gSpaceNumber = 0;
$gSpaceNumberMax = 5;
function teleportToNextSpace() {
    teleportToSpaceLocal($gSpaceNumber);
};
function teleportToSpaceLocal(%space) {
    if ((%space >= $gSpaceNumberMax)) {
        schedule(5000, 0);
    }
    teleportToSpaceNumber(%space);
    schedule(5000, 0);
    $gSpaceNumber = ($gSpaceNumber + 1.0);
    teleportToNextSpace;
};
function doLoginCheck() {
    if (isObject(pChat)) {
        if (!(MissionInfo @ " " @ mode $= "PrivateSpaceGrid")) {
            echo("Not a grid. Quiting...");
            schedule(3000, 0);
        }
        teleportToNextSpace(0);
    }
    if (($iterationsWaited == 400.0)) {
        error("CACHE->ERROR : Giving up. Waited for 20 minutes and nothing happended");
        quit();
    }
    echo("CACHE: Nothing yet....");
    $iterationsWaited = ($iterationsWaited + 1.0);
    quit;
    schedule(3000, 0);
};
