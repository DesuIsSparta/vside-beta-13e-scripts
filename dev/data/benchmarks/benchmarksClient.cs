$benchmarks::callbackOnAllComplete = "";
$benchmarks::testsSchedule = 0;
$benchmarks::camera::repsRemaining = 0;
$benchmarks::camera::resultString = "";
$benchmarks::camera::testSchedule = 0;
function benchmarks::initTestsList() {
    $benchmarks::currentTest = 0;
    $benchmarks::currentRunningTest = -(1.0);
    $benchmarks::successCount = 0;
    %n = 0;
    %n[$benchmarks::testEvalsList @ %n] = "benchmarks::runSampleTest1();";
    %n[$benchmarks::testNamesList @ %n] = "Sample Test 1";
    %n = (1.0 + %n);
    %n[$benchmarks::testEvalsList @ %n] = "benchmarks::runSampleTest2();";
    %n[$benchmarks::testNamesList @ %n] = "Sample Test 2";
    %n = (1.0 + %n);
    %n[$benchmarks::testEvalsList @ %n] = "benchmarks::loadCameraTests();benchmarks::runCameraTestsReps();";
    %n[$benchmarks::testNamesList @ %n] = "FPS";
    %n = (1.0 + %n);
    $benchmarks::testsListNum = %n;
};
benchmarks::initTestsList();
function benchmarks::setup() {
    echo("benchmarks::setUp()");
};
function benchmarks::doAllTests() {
    benchmarks::setup();
    $benchmarks::successCount = 0;
    $benchmarks::currentTest = 0;
    $benchmarks::currentRunningTest = -(1.0);
    echo("running  all" @ " " @ $benchmarks::testsListNum @ " " @ "tests..");
    benchmarksTryNextTest();
};
function benchmarksTryNextTest() {
    cancel($benchmarks::testsSchedule);
    if (($benchmarks::testsListNum >= $benchmarks::currentTest)) {
        echo("finished all tests, success =" @ " " @ $benchmarks::successCount @ "/" @ $benchmarks::testsListNum);
        $benchmarks::currentRunningTest = (1.0 - $benchmarks::currentRunningTest);
        benchmarks::finishedAllTests();
        return;
    }
    if (($benchmarks::currentTest < $benchmarks::currentRunningTest)) {
        benchmarks::runNextTest();
    }
    $benchmarks::testsSchedule = schedule(1000, 0, "benchmarksTryNextTest");
};
function benchmarks::runNextTest() {
    echo("running  test " @ $benchmarks::currentTest @ ": \"" @ $benchmarks::currentTest[$benchmarks::testNamesList @ $benchmarks::currentTest] @ "\"");
    eval($benchmarks::currentTest[$benchmarks::testEvalsList @ $benchmarks::currentTest]);
    $benchmarks::currentRunningTest = (1.0 + $benchmarks::currentRunningTest);
};
function benchmarks::finishedCurrentTest(%result) {
    if ((0.0 < $benchmarks::currentRunningTest)) {
        return;
    }
    if ((%result $= "success")) {
        $benchmarks::successCount = (1.0 + $benchmarks::successCount);
    }
    echo("finished test " @ $benchmarks::currentTest @ ": \"" @ $benchmarks::currentTest[$benchmarks::testNamesList @ $benchmarks::currentTest] @ "\" result = " @ %result);
    $benchmarks::currentTest = (1.0 + $benchmarks::currentTest);
};
function benchmarks::finishedAllTests(%result) {
    if (!($benchmarks::callbackOnAllComplete $= "")) {
        eval($benchmarks::callbackOnAllComplete);
    }
};
function benchmarks::runSampleTest1() {
    GLEnableMetrics(0);
    echo("sample test 1 started..");
    schedule(100, 0, "benchmarksFinishSampleTest1");
};
function benchmarksFinishSampleTest1() {
    GLEnableMetrics(0);
    echo("..sample test 1 finished");
    benchmarks::finishedCurrentTest("success");
};
function benchmarks::runSampleTest2() {
    GLEnableMetrics(0);
    echo("sample test 2 run.");
    benchmarks::finishedCurrentTest("success");
};
$echoBenchmarksCamera_DO = 1;
function echoBenchmarksCamera(%text) {
    if ($echoBenchmarksCamera_DO) {
        echo("Benchmarks::Camera" @ " " @ %text);
    }
    $benchmarks::camera::resultString = $benchmarks::camera::resultString @ %text @ "\n";
};
function benchmarks::MessageBoxOK(%title, %body) {
    if (benchmarks::isInteractive()) {
        MessageBoxOK(%title, %body, "");
    }
};
function benchmarks::loadCameraTests() {
};
function benchmarks::saveCameraTests() {
    benchmarks::MessageBoxOK("SaveCameraTests is now unused", "Just Save The Mission Instead");
};
function benchmarks::runCameraTestsReps() {
    $benchmarks::camera::resultString = "";
    $benchmarks::camera::avgTrisSum = 0;
    $benchmarks::camera::minFPSSum = 0;
    $benchmarks::camera::maxFPSSum = 0;
    $benchmarks::camera::avgFPSSum = 0;
    $benchmarks::camera::repsDone = 0;
    if (isObject(cameraTestsGroup)) {
    }
    if ((0.0 > cameraTestsGroup.getCount())) {
        %n = 0;
        if ((cameraTestsGroup.getCount() < %n)) {
            %obj = %n.getObject();
            cameraTestsGroup;
            %obj.totalFPS = 0;
            %obj.totalTests = 0;
            %n = (1.0 + %n);
        }
        $benchmarks::camera::repsRemaining = $pref::benchmarks::fps::reps;
        (cameraTestsGroup.getCount() < %n);
    }
    if ((0.0 > $benchmarks::camera::repsRemaining)) {
        benchmarks::runCameraTests();
    }
    echoBenchmarksCamera("no reps!");
    benchmarks::finishedCameraTestsReps("no reps");
};
function benchmarks::finishedCameraTestsRep(%result) {
    $benchmarks::camera::repsRemaining = (1.0 - $benchmarks::camera::repsRemaining);
    $benchmarks::camera::avgTrisSum = ($benchmarks::camera::avgTris + $benchmarks::camera::avgTrisSum);
    $benchmarks::camera::minFPSSum = ($benchmarks::camera::minFPS + $benchmarks::camera::minFPSSum);
    $benchmarks::camera::maxFPSSum = ($benchmarks::camera::maxFPS + $benchmarks::camera::maxFPSSum);
    $benchmarks::camera::avgFPSSum = ($benchmarks::camera::avgFPS + $benchmarks::camera::avgFPSSum);
    $benchmarks::camera::repsDone = (1.0 + $benchmarks::camera::repsDone);
    if ((%result $= "success")) {
    }
    if ((0.0 > $benchmarks::camera::repsRemaining)) {
        echoBenchmarksCamera("");
        benchmarks::runCameraTests();
    }
    benchmarks::finishedCameraTestsReps(%result);
};
function benchmarks::finishedCameraTestsReps(%result) {
    echoBenchmarksCamera("");
    echoBenchmarksCamera("Completed" @ " " @ $benchmarks::camera::repsDone @ " " @ "of" @ " " @ $pref::benchmarks::fps::reps @ " " @ "reps.");
    if ((0.0 > $benchmarks::camera::repsDone)) {
        $benchmarks::camera::avgTrisAvg = ($benchmarks::camera::repsDone / $benchmarks::camera::avgTrisSum);
        $benchmarks::camera::minFPSAvg = ($benchmarks::camera::repsDone / $benchmarks::camera::minFPSSum);
        $benchmarks::camera::maxFPSAvg = ($benchmarks::camera::repsDone / $benchmarks::camera::maxFPSSum);
        $benchmarks::camera::avgFPSAvg = ($benchmarks::camera::repsDone / $benchmarks::camera::avgFPSSum);
        if ($pref::benchmarks::fps::countTris) {
            echoBenchmarksCamera("avgTrisAvg:" @ $benchmarks::camera::avgTrisAvg);
        }
        echoBenchmarksCamera("minFPSAvg:" @ $benchmarks::camera::minFPSAvg);
        echoBenchmarksCamera("maxFPSAvg:" @ $benchmarks::camera::maxFPSAvg);
        echoBenchmarksCamera("avgFPSAvg:" @ $benchmarks::camera::avgFPSAvg);
    }
    benchmarks::finishedCurrentTest(%result);
    if (isObject(benchmarksGui)) {
        benchmarksGui.onFinishedCameraTests();
    }
};
function benchmarks::runCameraTests() {
    $gBenchmarksStoreOriginalCanSleepInBackground = $Platform::CanSleepInBackground;
    $Platform::CanSleepInBackground = 0;
    GLEnableMetrics(1);
    %repNum = ($benchmarks::camera::repsRemaining - $pref::benchmarks::fps::reps);
    echoBenchmarksCamera("rep" @ " " @ (1.0 + %repNum) @ " " @ "of" @ " " @ $pref::benchmarks::fps::reps);
    $benchmarks::camera::originalSpot = %obj.Camera.getTransform();
    LocalClientConnection;
    if (!(isObject(cameraTestsGroup))) {
    }
    if ((1.0 < cameraTestsGroup.getCount())) {
        echoBenchmarksCamera("No Tests!");
        benchmarks::MessageBoxOK("Benchmark Results", $benchmarks::camera::resultString);
        benchmarks::finishedCurrentTest("cameraTestsGroup not defined or empty");
        return;
    }
    echoBenchmarksCamera("Window Resolution and Depth:" @ " " @ $UserPref::Video::Resolution);
    echoBenchmarksCamera("Beginning" @ " " @ cameraTestsGroup.getCount() @ " " @ "tests, period =" @ " " @ (0.001 * $pref::benchmarks::cameraPeriod) @ " " @ "seconds");
    $benchmarks::camera::totalFPS = 0;
    $benchmarks::camera::totalTris = 0;
    $benchmarks::camera::totalTests = 0;
    $benchmarks::camera::maxFPS = -(1.0);
    $benchmarks::camera::minFPS = 100000000;
    $benchmarks::camera::curPoint = -(1.0);
    benchmarksRunNextCameraTest();
    $benchmarks::currentTimeStamp = getSubStr(getTimeStamp(), 0, 8);
};
function benchmarks::cancelCameraTests() {
    cancel($benchmarks::camera::testSchedule);
    $benchmarks::camera::testSchedule = 0;
    echoBenchmarksCamera("ERROR: camera tests cancelled");
    benchmarks::finishedCameraTests("cancelled");
};
function benchmarksRunNextCameraTest() {
    cancel($benchmarks::camera::testSchedule);
    $benchmarks::camera::testSchedule = 0;
    if (!(isObject(cameraTestsGroup))) {
    }
    if ((1.0 < cameraTestsGroup.getCount())) {
        benchmarks::finishedCurrentTest("cameraTestsGroup not defined or empty");
        return;
    }
    if ((0.0 >= $benchmarks::camera::curPoint)) {
        %theMark = $benchmarks::camera::curPoint.getObject();
        cameraTestsGroup;
        %tris = ($OpenGL::triCount3 + ($OpenGL::triCount2 + ($OpenGL::triCount1 + $OpenGL::triCount0)));
        echoBenchmarksCamera("fps  " @ (1.0 + $benchmarks::camera::curPoint) @ ":" @ %theMark.spotName @ ":" @ $fps::real);
        if ($pref::benchmarks::fps::countTris) {
            echoBenchmarksCamera("tris " @ (1.0 + $benchmarks::camera::curPoint) @ ":" @ %theMark.spotName @ ":" @ %tris);
        }
        %theMark.totalFPS = ($fps::real + %theMark.totalFPS);
        %theMark.totalTests = (1.0 + %theMark.totalTests);
        $benchmarks::camera::totalFPS = ($fps::real + $benchmarks::camera::totalFPS);
        $benchmarks::camera::totalTris = (%tris + $benchmarks::camera::totalTris);
        $benchmarks::camera::totalTests = (1.0 + $benchmarks::camera::totalTests);
        $benchmarks::camera::maxFPS = mMax($benchmarks::camera::maxFPS, $fps::real);
        $benchmarks::camera::minFPS = mMin($benchmarks::camera::minFPS, $fps::real);
        if ($pref::benchmarks::cameraScreenshots) {
            %screenshotFolder = $pref::benchmarks::dataPath;
            %screenshotFileName = MissionInfo @ %theMark.name @ "_" @ %theMark.spotName @ ".jpg";
            %screenshotFolder @ "/" @ $benchmarks::currentTimeStamp @ "_";
            if (isWriteableFileName(%screenshotFileName)) {
                ScreenShot(%screenshotFileName, "JPEG");
            }
            error("Cannot write to file" @ " " @ %screenshotFileName);
        }
    }
    if (((1.0 - cameraTestsGroup.getCount()) < $benchmarks::camera::curPoint)) {
        benchmarks::nextCameraTestPoint();
        $benchmarks::camera::testSchedule = schedule($pref::benchmarks::cameraPeriod, 0, "benchmarksRunNextCameraTest");
    }
    benchmarks::finishedCameraTests("success");
};
function benchmarks::finishedCameraTests(%result) {
    GLEnableMetrics(0);
    $Platform::CanSleepInBackground = $gBenchmarksStoreOriginalCanSleepInBackground;
    $benchmarks::camera::avgFPS = ($benchmarks::camera::totalTests / $benchmarks::camera::totalFPS);
    $benchmarks::camera::avgTris = ($benchmarks::camera::totalTests / $benchmarks::camera::totalTris);
    echoBenchmarksCamera("Completed" @ " " @ $benchmarks::camera::totalTests @ " " @ "of" @ " " @ cameraTestsGroup.getCount() @ " " @ "tests, period =" @ " " @ (0.001 * $pref::benchmarks::cameraPeriod) @ " " @ "seconds");
    if ($pref::benchmarks::fps::countTris) {
        echoBenchmarksCamera("Avg Tris:" @ $benchmarks::camera::avgTris);
    }
    echoBenchmarksCamera("Min FPS:" @ $benchmarks::camera::minFPS);
    echoBenchmarksCamera("Max FPS:" @ $benchmarks::camera::maxFPS);
    echoBenchmarksCamera("Avg FPS:" @ $benchmarks::camera::avgFPS);
    commandToServer('dropCameraAtTransform', $benchmarks::camera::originalSpot);
    $benchmarks::camera::curPoint = -(1.0);
    benchmarks::cameraToGui();
    benchmarks::finishedCameraTestsRep(%result);
};
function benchmarks::onVideoDeactivate() {
    if ((0.0 == $benchmarks::camera::testSchedule)) {
        return;
    }
    echoBenchmarksCamera("benchmark: window lost focus during test.");
};
function benchmarks::addNewCameraTestPoint(%name) {
    if (!(isObject(cameraTestsGroup))) {
        new SimGroup(cameraTestsGroup);
        if (isObject(MissionCleanup)) {
            MissionCleanup.add(cameraTestsGroup);
        }
    }
    MissionGroup.add(cameraTestsGroup);
    0;
    %spot = new ""() {
        dataBlock = MissionMarker @ "CameraWayPointMarker";
    };
    %spot.setTransform(Camera.getTransform());
    %spot.fov = LocalClientConnection @ getFovCur();
    %spot.spotName = %name;
    %spot.add();
    $benchmarks::camera::curPoint = 0;
    cameraTestsGroup;
    benchmarks::prevCameraTestPoint();
};
function benchmarks::prevCameraTestPoint() {
    if (!(isObject(cameraTestsGroup))) {
    }
    if ((1.0 < cameraTestsGroup.getCount())) {
        error("cameraTestsGroup not defined or empty");
        return;
    }
    $benchmarks::camera::curPoint = (1.0 - $benchmarks::camera::curPoint);
    if ((0.0 < $benchmarks::camera::curPoint)) {
        $benchmarks::camera::curPoint = (1.0 - cameraTestsGroup.getCount());
    }
    benchmarks::gotoCameraTestPoint($benchmarks::camera::curPoint.getObject());
};
function benchmarks::nextCameraTestPoint() {
    if (!(isObject(cameraTestsGroup))) {
    }
    if ((1.0 < cameraTestsGroup.getCount())) {
        error("cameraTestsGroup not defined or empty");
        return;
    }
    $benchmarks::camera::curPoint = (1.0 + $benchmarks::camera::curPoint);
    if ((cameraTestsGroup.getCount() >= $benchmarks::camera::curPoint)) {
        $benchmarks::camera::curPoint = 0;
    }
    benchmarks::gotoCameraTestPoint($benchmarks::camera::curPoint.getObject());
};
function benchmarks::clearCameraTests() {
    if (!(isObject(cameraTestsGroup))) {
        error("cameraTestsGroup not defined");
        return;
    }
    cameraTestsGroup.delete();
    $benchmarks::camera::curPoint = -(1.0);
};
function benchmarks::gotoCameraTestPoint(%obj) {
    %trans = %obj.getTransform();
    setFOV(%obj.fov);
    commandToServer('dropCameraAtTransform', %trans);
    benchmarks::cameraToGui();
};
function benchmarks::cameraToGui() {
    if (!(benchmarks::isInteractive())) {
        return;
    }
    $benchmarks::camera::curPoint.setText();
    %txt = "-";
    gui_Benchs_Cam_Cur;
    %obj = 0;
    if (isObject(cameraTestsGroup)) {
    }
    if ((0.0 >= $benchmarks::camera::curPoint)) {
        %obj = $benchmarks::camera::curPoint.getObject();
        cameraTestsGroup;
    }
    if (isObject(%obj)) {
        %txt = %obj.spotName;
    }
    %txt.setText();
    benchmarksGui.updateProgressBars();
};
function benchmarks::isInteractive() {
    if (isObject(benchmarksGui)) {
    }
    return benchmarksGui.isVisible();
};
