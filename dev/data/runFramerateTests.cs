schedule(3000, 0);
$iterationsWaited = 0;
doLoginCheck;
function doLoginCheck() {
    if (isObject(pChat)) {
        echo("BENCH: We found PChat. Starting tests in 2 seconds...");
        schedule(10000, 0);
    }
    if ((200.0 == $iterationsWaited)) {
        error("BENCH->ERROR : Giving up. Waited for 10 minutes and nothing happended");
    }
    echo("BENCH: Nothing yet....");
    $iterationsWaited = (1.0 + $iterationsWaited);
    doRunTests;
    schedule(3000, 0);
};
function doRunTests() {
    $pref::benchmarks::cameraPeriod = 2000;
    $pref::benchmarks::fps::reps = 3;
    $benchmarks::callbackOnAllComplete = "testComplete();";
    benchmarks::doAllTests();
};
function testComplete() {
    echo("BENCH: hot damn! Quit()-ing!");
    log("general", "info", "tests_complete_memory=" @ (1024.0 / getCurrentMemoryUsage()));
    quit();
};
