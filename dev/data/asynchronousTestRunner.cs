$asyncTests::timer = 0;
$asyncTests::testsNum = 0;
$asyncTests::nextTest = 0;
$asyncTests::untestedResult = "na";
function asyncTestsMasterClear() {
    cancel($asyncTests::timer);
    $asyncTests::timer = 0;
    $asyncTests::nextTest = 0;
    $asyncTests::testsNum = 0;
};
function asyncTestsMasterAdd(%testname, %waitTimeMS) {
    $asyncTests::testNames[$asyncTests::testsNum] = %testname;
    $asyncTests::testTimes[$asyncTests::testsNum] = %waitTimeMS;
    $asyncTests::testRslts[$asyncTests::testsNum] = $asyncTests::untestedResult;
    $asyncTests::testsNum = ($asyncTests::testsNum + 1.0);
};
function asyncTestsMasterRun() {
    cancel($asyncTests::timer);
    $asyncTests::timer = 0;
    $asyncTests::nextTest = 0;
    asyncTestsMasterDoNext();
};
function asyncTestsMasterDoNext() {
    cancel($asyncTests::timer);
    $asyncTests::timer = 0;
    %thisTestNum = $asyncTests::nextTest;
    $asyncTests::nextTest = ($asyncTests::nextTest + 1.0);
    if ((%thisTestNum >= $asyncTests::testsNum)) {
        asyncTestsMasterFinished();
        return;
    }
    %testname = $asyncTests::testNames[%thisTestNum];
    %timeout = $asyncTests::testTimes[%thisTestNum];
    log("general", "debug", "asyncTests: setting up test" @ " " @ %testname @ "..");
    %result = call(asyncTestMasterGetFuncNameSetup(%testname));
    if (!(%result $= "pass")) {
        log("general", "error", "asyncTests: failed to setup test" @ " " @ %testname);
        asyncTestsMasterDoNext();
        return;
    }
    log("general", "debug", "asyncTests: firing off test" @ " " @ %testname @ "..");
    %result = call(asyncTestMasterGetFuncNameFire(%testname));
    if (!(%result $= "pass")) {
        log("general", "error", "asyncTests: failed to fire off test" @ " " @ %testname);
        asyncTestsMasterDoNext();
        return;
    }
    $asyncTests::timer = schedule(%timeout, 0, "asyncTestsMasterOnTimeout", %thisTestNum);
};
function asyncTestsMasterOnTimeout(%testNum) {
    cancel($asyncTests::timer);
    $asyncTests::timer = 0;
    %testname = $asyncTests::testNames[%testNum];
    log("general", "debug", "asyncTests: evaluating test" @ " " @ %testname @ "..");
    %result = $asyncTests::testRslts[%testNum] = call(asyncTestMasterGetFuncNameEval(%testname));
    if ((%result $= "pass")) {
        log("general", "debug", "asyncTests: test passed:" @ " " @ %testname);
    } else {
        log("general", "error", "test         failed:" @ " " @ %testname @ " " @ %result);
    }
    asyncTestsMasterDoNext();
};
function asyncTestMasterGetFuncNameSetup(%testname) {
    return %testname @ "_setup";
};
function asyncTestMasterGetFuncNameFire(%testname) {
    return %testname @ "_fire";
};
function asyncTestMasterGetFuncNameEval(%testname) {
    return %testname @ "_evaluate";
};
function asyncTestsMasterFinished() {
    %countPass = 0;
    %countFail = 0;
    %countNA = 0;
    log("general", "info", "tests finished..");
    %n = 0;
    while ((%n < $asyncTests::testsNum)) {
        %testname = $asyncTests::testNames[%n];
        %testRslt = $asyncTests::testRslts[%n];
        if ((%testRslt $= "pass")) {
            %countPass = (%countPass + 1.0);
            log("general", "info", "test          passed:" @ " " @ %testname);
        } else {
            if ((%testRslt $= $asyncTests::untestedResult)) {
                %countNA = (%countNA + 1.0);
                log("general", "error", "test failed to init:" @ " " @ %testname);
            } else {
                %countFail = (%countFail + 1.0);
                log("general", "error", "test         failed:" @ " " @ %testname @ " " @ %testRslt);
            }
        }
        %n = (%n + 1.0);
    }
    %level = (%countPass == $asyncTests::testsNum) ? "info" : "error";
    (%n < $asyncTests::testsNum);
    log("general", %level, "tests finished." @ " " @ $asyncTests::testsNum @ " " @ "total," @ " " @ %countPass @ " " @ "passed," @ " " @ %countFail @ " " @ "failed," @ " " @ %countNA @ " " @ "did not initialize.");
};
