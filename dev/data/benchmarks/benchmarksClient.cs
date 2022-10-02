$benchmarks::callbackOnAllComplete = "";
$benchmarks::testsSchedule = 0;
$benchmarks::camera::repsRemaining = 0;
$benchmarks::camera::resultString = "";
$benchmarks::camera::testSchedule = 0;
function benchmarks::initTestsList()
{
    $benchmarks::currentTest = 0;
    $benchmarks::currentRunningTest = -1;
    $benchmarks::successCount = 0;
    %n = 0;
    $benchmarks::testEvalsList[%n] = "benchmarks::runSampleTest1();";
    $benchmarks::testNamesList[%n] = "Sample Test 1";
    %n = %n + 1;
    $benchmarks::testEvalsList[%n] = "benchmarks::runSampleTest2();";
    $benchmarks::testNamesList[%n] = "Sample Test 2";
    %n = %n + 1;
    $benchmarks::testEvalsList[%n] = "benchmarks::loadCameraTests();benchmarks::runCameraTestsReps();";
    $benchmarks::testNamesList[%n] = "FPS";
    %n = %n + 1;
    $benchmarks::testsListNum = %n;
    return ;
}
benchmarks::initTestsList();
function benchmarks::setup()
{
    echo("benchmarks::setUp()");
    return ;
}
function benchmarks::doAllTests()
{
    benchmarks::setup();
    $benchmarks::successCount = 0;
    $benchmarks::currentTest = 0;
    $benchmarks::currentRunningTest = -1;
    echo("running  all" SPC $benchmarks::testsListNum SPC "tests..");
    benchmarksTryNextTest();
    return ;
}
function benchmarksTryNextTest()
{
    cancel($benchmarks::testsSchedule);
    if ($benchmarks::currentTest >= $benchmarks::testsListNum)
    {
        echo("finished all tests, success =" SPC $benchmarks::successCount @ "/" @ $benchmarks::testsListNum);
        $benchmarks::currentRunningTest = $benchmarks::currentRunningTest - 1;
        benchmarks::finishedAllTests();
        return ;
    }
    if ($benchmarks::currentRunningTest < $benchmarks::currentTest)
    {
        benchmarks::runNextTest();
    }
    $benchmarks::testsSchedule = schedule(1000, 0, "benchmarksTryNextTest");
    return ;
}
function benchmarks::runNextTest()
{
    echo("running  test " @ $benchmarks::currentTest @ ": \"" @ $benchmarks::testNamesList[$benchmarks::currentTest] @ "\"");
    eval($benchmarks::testEvalsList[$benchmarks::currentTest]);
    $benchmarks::currentRunningTest = $benchmarks::currentRunningTest + 1;
    return ;
}
function benchmarks::finishedCurrentTest(%result)
{
    if ($benchmarks::currentRunningTest < 0)
    {
        return ;
    }
    if (%result $= "success")
    {
        $benchmarks::successCount = $benchmarks::successCount + 1;
    }
    echo("finished test " @ $benchmarks::currentTest @ ": \"" @ $benchmarks::testNamesList[$benchmarks::currentTest] @ "\" result = " @ %result);
    $benchmarks::currentTest = $benchmarks::currentTest + 1;
    return ;
}
function benchmarks::finishedAllTests(%result)
{
    if (!($benchmarks::callbackOnAllComplete $= ""))
    {
        eval($benchmarks::callbackOnAllComplete);
    }
    return ;
}
function benchmarks::runSampleTest1()
{
    GLEnableMetrics(0);
    echo("sample test 1 started..");
    schedule(100, 0, "benchmarksFinishSampleTest1");
    return ;
}
function benchmarksFinishSampleTest1()
{
    GLEnableMetrics(0);
    echo("..sample test 1 finished");
    benchmarks::finishedCurrentTest("success");
    return ;
}
function benchmarks::runSampleTest2()
{
    GLEnableMetrics(0);
    echo("sample test 2 run.");
    benchmarks::finishedCurrentTest("success");
    return ;
}
$echoBenchmarksCamera_DO = 1;
function echoBenchmarksCamera(%text)
{
    if ($echoBenchmarksCamera_DO)
    {
        echo("Benchmarks::Camera" SPC %text);
    }
    $benchmarks::camera::resultString = $benchmarks::camera::resultString @ %text @ "\n";
    return ;
}
function benchmarks::MessageBoxOK(%title, %body)
{
    if (benchmarks::isInteractive())
    {
        MessageBoxOK(%title, %body, "");
    }
    return ;
}
function benchmarks::loadCameraTests()
{
    return ;
}
function benchmarks::saveCameraTests()
{
    benchmarks::MessageBoxOK("SaveCameraTests is now unused", "Just Save The Mission Instead");
    return ;
}
function benchmarks::runCameraTestsReps()
{
    $benchmarks::camera::resultString = "";
    $benchmarks::camera::avgTrisSum = 0;
    $benchmarks::camera::minFPSSum = 0;
    $benchmarks::camera::maxFPSSum = 0;
    $benchmarks::camera::avgFPSSum = 0;
    $benchmarks::camera::repsDone = 0;
    if (isObject(cameraTestsGroup) && (cameraTestsGroup.getCount() > 0))
    {
        %n = 0;
        while (%n < cameraTestsGroup.getCount())
        {
            %obj = cameraTestsGroup.getObject(%n);
            %obj.totalFPS = 0;
            %obj.totalTests = 0;
            %n = %n + 1;
        }
        $benchmarks::camera::repsRemaining = $pref::benchmarks::fps::reps;
    }
    if ($benchmarks::camera::repsRemaining > 0)
    {
        benchmarks::runCameraTests();
    }
    else
    {
        echoBenchmarksCamera("no reps!");
        benchmarks::finishedCameraTestsReps("no reps");
    }
    return ;
}
function benchmarks::finishedCameraTestsRep(%result)
{
    $benchmarks::camera::repsRemaining = $benchmarks::camera::repsRemaining - 1;
    $benchmarks::camera::avgTrisSum = $benchmarks::camera::avgTrisSum + $benchmarks::camera::avgTris;
    $benchmarks::camera::minFPSSum = $benchmarks::camera::minFPSSum + $benchmarks::camera::minFPS;
    $benchmarks::camera::maxFPSSum = $benchmarks::camera::maxFPSSum + $benchmarks::camera::maxFPS;
    $benchmarks::camera::avgFPSSum = $benchmarks::camera::avgFPSSum + $benchmarks::camera::avgFPS;
    $benchmarks::camera::repsDone = $benchmarks::camera::repsDone + 1;
    if ((%result $= "success") && ($benchmarks::camera::repsRemaining > 0))
    {
        echoBenchmarksCamera("");
        benchmarks::runCameraTests();
    }
    else
    {
        benchmarks::finishedCameraTestsReps(%result);
    }
    return ;
}
function benchmarks::finishedCameraTestsReps(%result)
{
    echoBenchmarksCamera("");
    echoBenchmarksCamera("Completed" SPC $benchmarks::camera::repsDone SPC "of" SPC $pref::benchmarks::fps::reps SPC "reps.");
    if ($benchmarks::camera::repsDone > 0)
    {
        $benchmarks::camera::avgTrisAvg = $benchmarks::camera::avgTrisSum / $benchmarks::camera::repsDone;
        $benchmarks::camera::minFPSAvg = $benchmarks::camera::minFPSSum / $benchmarks::camera::repsDone;
        $benchmarks::camera::maxFPSAvg = $benchmarks::camera::maxFPSSum / $benchmarks::camera::repsDone;
        $benchmarks::camera::avgFPSAvg = $benchmarks::camera::avgFPSSum / $benchmarks::camera::repsDone;
        if ($pref::benchmarks::fps::countTris)
        {
            echoBenchmarksCamera("avgTrisAvg:" @ $benchmarks::camera::avgTrisAvg);
        }
        echoBenchmarksCamera("minFPSAvg:" @ $benchmarks::camera::minFPSAvg);
        echoBenchmarksCamera("maxFPSAvg:" @ $benchmarks::camera::maxFPSAvg);
        echoBenchmarksCamera("avgFPSAvg:" @ $benchmarks::camera::avgFPSAvg);
    }
    benchmarks::finishedCurrentTest(%result);
    if (isObject(benchmarksGui))
    {
        benchmarksGui.onFinishedCameraTests();
    }
    return ;
}
function benchmarks::runCameraTests()
{
    $gBenchmarksStoreOriginalCanSleepInBackground = $Platform::CanSleepInBackground;
    $Platform::CanSleepInBackground = 0;
    GLEnableMetrics(1);
    %repNum = $pref::benchmarks::fps::reps - $benchmarks::camera::repsRemaining;
    echoBenchmarksCamera("rep" SPC %repNum + 1 SPC "of" SPC $pref::benchmarks::fps::reps);
    $benchmarks::camera::originalSpot = LocalClientConnection.Camera.getTransform();
    if (!isObject(cameraTestsGroup) && (cameraTestsGroup.getCount() < 1))
    {
        echoBenchmarksCamera("No Tests!");
        benchmarks::MessageBoxOK("Benchmark Results", $benchmarks::camera::resultString);
        benchmarks::finishedCurrentTest("cameraTestsGroup not defined or empty");
        return ;
    }
    echoBenchmarksCamera("Window Resolution and Depth:" SPC $UserPref::Video::Resolution);
    echoBenchmarksCamera("Beginning" SPC cameraTestsGroup.getCount() SPC "tests, period =" SPC $pref::benchmarks::cameraPeriod * 0.001 SPC "seconds");
    $benchmarks::camera::totalFPS = 0;
    $benchmarks::camera::totalTris = 0;
    $benchmarks::camera::totalTests = 0;
    $benchmarks::camera::maxFPS = -1;
    $benchmarks::camera::minFPS = 100000000;
    $benchmarks::camera::curPoint = -1;
    benchmarksRunNextCameraTest();
    $benchmarks::currentTimeStamp = getSubStr(getTimeStamp(), 0, 8);
    return ;
}
function benchmarks::cancelCameraTests()
{
    cancel($benchmarks::camera::testSchedule);
    $benchmarks::camera::testSchedule = 0;
    echoBenchmarksCamera("ERROR: camera tests cancelled");
    benchmarks::finishedCameraTests("cancelled");
    return ;
}
function benchmarksRunNextCameraTest()
{
    cancel($benchmarks::camera::testSchedule);
    $benchmarks::camera::testSchedule = 0;
    if (!isObject(cameraTestsGroup) && (cameraTestsGroup.getCount() < 1))
    {
        benchmarks::finishedCurrentTest("cameraTestsGroup not defined or empty");
        return ;
    }
    if ($benchmarks::camera::curPoint >= 0)
    {
        %theMark = cameraTestsGroup.getObject($benchmarks::camera::curPoint);
        %tris = (($OpenGL::triCount0 + $OpenGL::triCount1) + $OpenGL::triCount2) + $OpenGL::triCount3;
        echoBenchmarksCamera("fps  " @ $benchmarks::camera::curPoint + 1 @ ":" @ %theMark.spotName @ ":" @ $fps::real);
        if ($pref::benchmarks::fps::countTris)
        {
            echoBenchmarksCamera("tris " @ $benchmarks::camera::curPoint + 1 @ ":" @ %theMark.spotName @ ":" @ %tris);
        }
        %theMark.totalFPS = %theMark.totalFPS + $fps::real;
        %theMark.totalTests = %theMark.totalTests + 1;
        $benchmarks::camera::totalFPS = $benchmarks::camera::totalFPS + $fps::real;
        $benchmarks::camera::totalTris = $benchmarks::camera::totalTris + %tris;
        $benchmarks::camera::totalTests = $benchmarks::camera::totalTests + 1;
        $benchmarks::camera::maxFPS = mMax($benchmarks::camera::maxFPS, $fps::real);
        $benchmarks::camera::minFPS = mMin($benchmarks::camera::minFPS, $fps::real);
        if ($pref::benchmarks::cameraScreenshots)
        {
            %screenshotFolder = $pref::benchmarks::dataPath;
            %screenshotFileName = %screenshotFolder @ "/" @ $benchmarks::currentTimeStamp @ "_" @ MissionInfo.name @ "_" @ %theMark.spotName @ ".jpg";
            if (isWriteableFileName(%screenshotFileName))
            {
                ScreenShot(%screenshotFileName, "JPEG");
            }
            else
            {
                error("Cannot write to file" SPC %screenshotFileName);
            }
        }
    }
    if ($benchmarks::camera::curPoint < (cameraTestsGroup.getCount() - 1))
    {
        benchmarks::nextCameraTestPoint();
        $benchmarks::camera::testSchedule = schedule($pref::benchmarks::cameraPeriod, 0, "benchmarksRunNextCameraTest");
    }
    else
    {
        benchmarks::finishedCameraTests("success");
    }
    return ;
}
function benchmarks::finishedCameraTests(%result)
{
    GLEnableMetrics(0);
    $Platform::CanSleepInBackground = $gBenchmarksStoreOriginalCanSleepInBackground;
    $benchmarks::camera::avgFPS = $benchmarks::camera::totalFPS / $benchmarks::camera::totalTests;
    $benchmarks::camera::avgTris = $benchmarks::camera::totalTris / $benchmarks::camera::totalTests;
    echoBenchmarksCamera("Completed" SPC $benchmarks::camera::totalTests SPC "of" SPC cameraTestsGroup.getCount() SPC "tests, period =" SPC $pref::benchmarks::cameraPeriod * 0.001 SPC "seconds");
    if ($pref::benchmarks::fps::countTris)
    {
        echoBenchmarksCamera("Avg Tris:" @ $benchmarks::camera::avgTris);
    }
    echoBenchmarksCamera("Min FPS:" @ $benchmarks::camera::minFPS);
    echoBenchmarksCamera("Max FPS:" @ $benchmarks::camera::maxFPS);
    echoBenchmarksCamera("Avg FPS:" @ $benchmarks::camera::avgFPS);
    commandToServer('dropCameraAtTransform', $benchmarks::camera::originalSpot);
    $benchmarks::camera::curPoint = -1;
    benchmarks::cameraToGui();
    benchmarks::finishedCameraTestsRep(%result);
    return ;
}
function benchmarks::onVideoDeactivate()
{
    if ($benchmarks::camera::testSchedule == 0)
    {
        return ;
    }
    echoBenchmarksCamera("benchmark: window lost focus during test.");
    return ;
}
function benchmarks::addNewCameraTestPoint(%name)
{
    if (!isObject(cameraTestsGroup))
    {
        new SimGroup(cameraTestsGroup);
        if (isObject(MissionCleanup))
        {
            MissionCleanup.add(cameraTestsGroup);
        }
    }
    MissionGroup.add(cameraTestsGroup);
    %spot = new MissionMarker();
    %spot.setTransform(LocalClientConnection.Camera.getTransform());
    %spot.fov = getFovCur();
    %spot.spotName = %name;
    cameraTestsGroup.add(%spot);
    $benchmarks::camera::curPoint = 0;
    benchmarks::prevCameraTestPoint();
    return ;
}
function benchmarks::prevCameraTestPoint()
{
    if (!isObject(cameraTestsGroup) && (cameraTestsGroup.getCount() < 1))
    {
        error("cameraTestsGroup not defined or empty");
        return ;
    }
    $benchmarks::camera::curPoint = $benchmarks::camera::curPoint - 1;
    if ($benchmarks::camera::curPoint < 0)
    {
        $benchmarks::camera::curPoint = cameraTestsGroup.getCount() - 1;
    }
    benchmarks::gotoCameraTestPoint(cameraTestsGroup.getObject($benchmarks::camera::curPoint));
    return ;
}
function benchmarks::nextCameraTestPoint()
{
    if (!isObject(cameraTestsGroup) && (cameraTestsGroup.getCount() < 1))
    {
        error("cameraTestsGroup not defined or empty");
        return ;
    }
    $benchmarks::camera::curPoint = $benchmarks::camera::curPoint + 1;
    if ($benchmarks::camera::curPoint >= cameraTestsGroup.getCount())
    {
        $benchmarks::camera::curPoint = 0;
    }
    benchmarks::gotoCameraTestPoint(cameraTestsGroup.getObject($benchmarks::camera::curPoint));
    return ;
}
function benchmarks::clearCameraTests()
{
    if (!isObject(cameraTestsGroup))
    {
        error("cameraTestsGroup not defined");
        return ;
    }
    cameraTestsGroup.delete();
    $benchmarks::camera::curPoint = -1;
    return ;
}
function benchmarks::gotoCameraTestPoint(%obj)
{
    %trans = %obj.getTransform();
    setFOV(%obj.fov);
    commandToServer('dropCameraAtTransform', %trans);
    benchmarks::cameraToGui();
    return ;
}
function benchmarks::cameraToGui()
{
    if (!benchmarks::isInteractive())
    {
        return ;
    }
    gui_Benchs_Cam_Cur.setText($benchmarks::camera::curPoint);
    %txt = "-";
    %obj = 0;
    if (isObject(cameraTestsGroup) && ($benchmarks::camera::curPoint >= 0))
    {
        %obj = cameraTestsGroup.getObject($benchmarks::camera::curPoint);
    }
    if (isObject(%obj))
    {
        %txt = %obj.spotName;
    }
    gui_Benchs_Cam_Name.setText(%txt);
    benchmarksGui.updateProgressBars();
    return ;
}
function benchmarks::isInteractive()
{
    return isObject(benchmarksGui) && benchmarksGui.isVisible();
}
