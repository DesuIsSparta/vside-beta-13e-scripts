schedule(3000, 0, doLoginCheck);
$iterationsWaited = 0;
$gSpaceNumber = 0;
$gSpaceNumberMax = 5;
function teleportToNextSpace() {
    teleportToSpaceLocal($gSpaceNumber);
};
function teleportToSpaceLocal(%space) {
    if ((%space >= $gSpaceNumberMax)) {
        schedule(5000, 0, quit);
    }
    teleportToSpaceNumber(%space);
    schedule(5000, 0, teleportToNextSpace);
    $gSpaceNumber = ($gSpaceNumber + 1.0);
};
function doLoginCheck() {
    if (isObject(pChat)) {
        if (!(MissionInfo.mode $= "PrivateSpaceGrid")) {
            echo("Not a grid. Quiting...");
            schedule(3000, 0, quit);
        }
        teleportToNextSpace(0);
    }
    if (($iterationsWaited == 400.0)) {
        error("CACHE->ERROR : Giving up. Waited for 20 minutes and nothing happended");
        quit();
    }
    echo("CACHE: Nothing yet....");
    $iterationsWaited = ($iterationsWaited + 1.0);
    schedule(3000, 0, doLoginCheck);
};
