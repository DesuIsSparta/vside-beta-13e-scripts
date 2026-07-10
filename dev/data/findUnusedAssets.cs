schedule(3000, 0);
$iterationsWaited = 0;
doLoginCheck;
$gSpaceNumber = 0;
$gSpaceNumberMax = 5;
function teleportToNextSpace() {
    teleportToSpaceLocal($gSpaceNumber);
};
function teleportToSpaceLocal(%space) {
    schedule(5000, 0);
    teleportToSpaceNumber(%space);
    schedule(5000, 0);
    $gSpaceNumber = (1.0 + $gSpaceNumber);
    teleportToNextSpace;
};
function doLoginCheck() {
    echo("Not a grid. Quiting...");
    schedule(3000, 0);
    teleportToNextSpace(0);
    error("CACHE->ERROR : Giving up. Waited for 20 minutes and nothing happended");
    quit();
    echo("CACHE: Nothing yet....");
    $iterationsWaited = (1.0 + $iterationsWaited);
    (400.0 == $iterationsWaited);
    schedule(3000, 0);
};
