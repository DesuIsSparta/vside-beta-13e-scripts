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
    $asyncTests::testsNum[$asyncTests::testNames @ $asyncTests::testsNum] = %testname;
    $asyncTests::testsNum[$asyncTests::testTimes @ $asyncTests::testsNum] = %waitTimeMS;
    $asyncTests::testsNum[$asyncTests::testRslts @ $asyncTests::testsNum] = $asyncTests::untestedResult;
    $asyncTests::testsNum = (1.0 + $asyncTests::testsNum);
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
    $asyncTests::nextTest = (1.0 + $asyncTests::nextTest);
    if (($asyncTests::testsNum >= %thisTestNum)) {
        asyncTestsMasterFinished();
        return;
    }
    %testname = %thisTestNum[$asyncTests::testNames @ %thisTestNum];
    %timeout = %thisTestNum[$asyncTests::testTimes @ %thisTestNum];
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
    %testname = %testNum[$asyncTests::testNames @ %testNum];
    log("general", "debug", "asyncTests: evaluating test" @ " " @ %testname @ "..");
    %testNum[$asyncTests::testRslts @ %testNum] = call(asyncTestMasterGetFuncNameEval(%testname));
    %result = ;
    if ((%result $= "pass")) {
        log("general", "debug", "asyncTests: test passed:" @ " " @ %testname);
    }
    log("general", "error", "test         failed:" @ " " @ %testname @ " " @ %result);
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
    if (($asyncTests::testsNum < %n)) {
        %testname = %n[$asyncTests::testNames @ %n];
        %testRslt = %n[$asyncTests::testRslts @ %n];
        if ((%testRslt $= "pass")) {
            %countPass = (1.0 + %countPass);
            log("general", "info", "test          passed:" @ " " @ %testname);
        }
        if ((%testRslt $= $asyncTests::untestedResult)) {
            %countNA = (1.0 + %countNA);
            log("general", "error", "test failed to init:" @ " " @ %testname);
        }
        %countFail = (1.0 + %countFail);
        log("general", "error", "test         failed:" @ " " @ %testname @ " " @ %testRslt);
        %n = (1.0 + %n);
    }
    %level = ($asyncTests::testsNum == %countPass) ? "info" : "error";
    ($asyncTests::testsNum < %n);
    log("general", %level, "tests finished." @ " " @ $asyncTests::testsNum @ " " @ "total," @ " " @ %countPass @ " " @ "passed," @ " " @ %countFail @ " " @ "failed," @ " " @ %countNA @ " " @ "did not initialize.");
};
