$asyncTests::timer = 0;
$asyncTests::testsNum = 0;
$asyncTests::nextTest = 0;
$asyncTests::untestedResult = "na";
function asyncTestsMasterClear()
{
    cancel($asyncTests::timer);
    $asyncTests::timer = 0;
    $asyncTests::nextTest = 0;
    $asyncTests::testsNum = 0;
    return ;
}
function asyncTestsMasterAdd(%testname, %waitTimeMS)
{
    $asyncTests::testNames[$asyncTests::testsNum] = %testname ;
    $asyncTests::testTimes[$asyncTests::testsNum] = %waitTimeMS ;
    $asyncTests::testRslts[$asyncTests::testsNum] = $asyncTests::untestedResult ;
    $asyncTests::testsNum = $asyncTests::testsNum + 1;
    return ;
}
function asyncTestsMasterRun()
{
    cancel($asyncTests::timer);
    $asyncTests::timer = 0;
    $asyncTests::nextTest = 0;
    asyncTestsMasterDoNext();
    return ;
}
function asyncTestsMasterDoNext()
{
    cancel($asyncTests::timer);
    $asyncTests::timer = 0;
    %thisTestNum = $asyncTests::nextTest;
    $asyncTests::nextTest = $asyncTests::nextTest + 1;
    if (%thisTestNum >= $asyncTests::testsNum)
    {
        asyncTestsMasterFinished();
        return ;
    }
    %testname = $asyncTests::testNames[%thisTestNum];
    %timeout = $asyncTests::testTimes[%thisTestNum];
    log("general", "debug", "asyncTests: setting up test" SPC %testname @ "..");
    %result = call(asyncTestMasterGetFuncNameSetup(%testname));
    if (!(%result $= "pass"))
    {
        log("general", "error", "asyncTests: failed to setup test" SPC %testname);
        asyncTestsMasterDoNext();
        return ;
    }
    log("general", "debug", "asyncTests: firing off test" SPC %testname @ "..");
    %result = call(asyncTestMasterGetFuncNameFire(%testname));
    if (!(%result $= "pass"))
    {
        log("general", "error", "asyncTests: failed to fire off test" SPC %testname);
        asyncTestsMasterDoNext();
        return ;
    }
    $asyncTests::timer = schedule(%timeout, 0, "asyncTestsMasterOnTimeout", %thisTestNum);
    return ;
}
function asyncTestsMasterOnTimeout(%testNum)
{
    cancel($asyncTests::timer);
    $asyncTests::timer = 0;
    %testname = $asyncTests::testNames[%testNum];
    log("general", "debug", "asyncTests: evaluating test" SPC %testname @ "..");
    %result = $asyncTests::testRslts[%testNum] = call(asyncTestMasterGetFuncNameEval(%testname)) ;
    if (%result $= "pass")
    {
        log("general", "debug", "asyncTests: test passed:" SPC %testname);
    }
    else
    {
        log("general", "error", "test         failed:" SPC %testname SPC %result);
    }
    asyncTestsMasterDoNext();
    return ;
}
function asyncTestMasterGetFuncNameSetup(%testname)
{
    return %testname @ "_setup";
}
function asyncTestMasterGetFuncNameFire(%testname)
{
    return %testname @ "_fire";
}
function asyncTestMasterGetFuncNameEval(%testname)
{
    return %testname @ "_evaluate";
}
function asyncTestsMasterFinished()
{
    %countPass = 0;
    %countFail = 0;
    %countNA = 0;
    log("general", "info", "tests finished..");
    %n = 0;
    while (%n < $asyncTests::testsNum)
    {
        %testname = $asyncTests::testNames[%n];
        %testRslt = $asyncTests::testRslts[%n];
        if (%testRslt $= "pass")
        {
            %countPass = %countPass + 1;
            log("general", "info", "test          passed:" SPC %testname);
        }
        else
        {
            if (%testRslt $= $asyncTests::untestedResult)
            {
                %countNA = %countNA + 1;
                log("general", "error", "test failed to init:" SPC %testname);
            }
            else
            {
                %countFail = %countFail + 1;
                log("general", "error", "test         failed:" SPC %testname SPC %testRslt);
            }
        }
        %n = %n + 1;
    }
    %level = %countPass == $asyncTests::testsNum ? "info" : "error";
    log("general", %level, "tests finished." SPC $asyncTests::testsNum SPC "total," SPC %countPass SPC "passed," SPC %countFail SPC "failed," SPC %countNA SPC "did not initialize.");
    return ;
}
