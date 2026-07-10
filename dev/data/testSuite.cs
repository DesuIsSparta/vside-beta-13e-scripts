$G_DECLARED_TEST_COUNT = 0;
$G_DECLARED_TEST_COUNT[$G_DECLARED_TEST @ 0] = 0;
function DeclareTestSuite(%name) {
    $G_DECLARED_TEST_COUNT[$G_DECLARED_TEST @ $G_DECLARED_TEST_COUNT] = %name;
    $G_DECLARED_TEST_COUNT = (1.0 + $G_DECLARED_TEST_COUNT);
};
function DeclaredTestSuiteCount() {
    return $G_DECLARED_TEST_COUNT;
};
function DeclaredTestSuiteGet(%num) {
    if (($G_DECLARED_TEST_COUNT < %num)) {
        return %num[$G_DECLARED_TEST @ %num];
    }
    return -(1.0);
};
function RunTestSuite(%suitename) {
    TestSuite::construct(%suitename);
    if (!($AmServer)) {
        if (%suitename.ShouldRunOnServer()) {
            %suitename.delete();
            echo("running" @ " " @ %suitename @ " " @ "on the server");
            commandToServer('RunTestSuiteServerSide', %suitename);
            return;
        }
    }
    %suitename.execute();
};
function serverCmdRunTestSuiteServerSide(%unused, %suitename) {
    TestSuite::construct(%suitename);
    %suitename.execute();
};
function RunTestSuite_QuiteWhenDone(%suitename) {
    TestSuite::construct(%suitename);
    quitWhenDone = 1 @ %suitename;
    %suitename.execute();
};
function RunTestCase(%testname, %dialogTitle) {
    TestCase::construct(%testname);
    %testname.execute();
    if (!(%dialogTitle $= "")) {
        if ((%testname > errorCount)) {
            %message = 0.0 @ "<font:Arial Bold:18><color:FFFFFF>DO NOT IGNORE THIS MESSAGE, THESE NEED TO BE FIXED BEFORE YOU CHECK IN!!!!!<font:Arial Bold:12>Hi There! It's likely that the changes you have recently made have introduced some serious errors. Please don't check in until these are fixed. If the fix is not obvious, feel free to ask richard or terrence or orion or clint for help. Thanks!<br><br>There are **maybe" @ " " @ %testname @ errorCount @ " " @ "problems with this missionfile.** Look in the console for things labeled <color:FF0000>TEST_MISSIONGROUPINTEGRITY<color:FFFFFF> in red, or talk to one of the engineers for help.\nAnd by the way, you are doing great work! Have a fine day." @ "<font:Arial:12>\n" @ %testname.getErrorMessagesBrief();
            %dlg = MessageBoxOK(%dialogTitle, %message, "");
            window.resize(500, getWord(%dlg.getExtent(), 1));
        }
    }
    return errorCount;
};
function TestRunner_SmokeTests::setup(%this) {
    %this.addTestSuite("TestSuite_AnimationSystemSmokeTests");
    %this.addTestSuite("TestSuite_CSSmokeTests");
    %this.addTestSuite("TestSuite_MissionGroup");
    %this.addTestSuite("TestSuite_SeatingSystemSmokeTests");
};
function RunTestRunner(%runnername) {
    TestSuiteRunner::construct(%runnername);
    %runnername.execute();
};
function TestSuiteRunner::construct(%name) {
    class = ScriptObject @ new %name() @ "TestSuiteRunner";
    0;
    testSuiteCount = 0;
    quitWhenDone = 0;
    %ret = ;
    if (isObject()) {
        %ret.add();
    }
    return %ret;
};
function TestSuiteRunner::addTestSuite(%this, %name) {
    Suite = %name @ %this @ testSuiteCount @ %this;
    testSuiteCount = (%this + testSuiteCount);
    1.0;
};
function TestSuiteRunner::setup(%this) {
    error("TestSuiteRunner::Setup: you must override this and addTestSuites for the runner");
};
function TestSuiteRunner::TearDown(%this) {
};
function TestSuiteRunner::execute(%this) {
    %this.setup();
    running = 1 @ %this;
    currentSuite = 0 @ %this;
    TimerProcess = 0 @ %this;
    echo(%this @ suiteCount @ " " @ "suites:");
    nextSuite = %this.getName() @ " " @ ":  begin, with" @ " " @ -(1.0) @ %this;
    %this.ProcessLoop();
};
function TestSuiteRunner::ProcessLoop(%this) {
    cancel(TimerProcess);
    if (isObject(currentSuite)) {
        if (running) {
            TimerProcess = currentSuite @ %this.schedule(100, "ProcessLoop") @ %this;
            %this;
            return %this;
        }
    }
    nextSuite = (%this + nextSuite);
    1.0;
    if ((%this >= nextSuite)) {
        %this.finishTesting();
        return testSuiteCount;
    }
    %testSuiteName = Suite;
    %this @ nextSuite @ %this;
    currentSuite = TestSuite::construct(%testSuiteName) @ %this;
    currentSuite.execute();
    TimerProcess = %this @ %this.schedule(100, "ProcessLoop") @ %this;
};
function TestSuiteRunner::finishTesting(%this) {
    %this.TearDown();
    %this.reportResults();
    echo(%this.getName() @ " " @ ":  completed.");
    running = 0 @ %this;
    if (quitWhenDone) {
        quit();
    }
};
function TestSuiteRunner::reportResults(%this) {
    echo(" ");
    echo(%this.getName() @ " " @ "results summary --------------");
    %i = 0;
    if ((testSuiteCount < %i)) {
        %testname = Suite;
        %this @ %i @ %this;
        %message = %testname @ assertCount @ " " @ "asserts" @ " " @ "reported by" @ " " @ %testname;
        %testname @ errorCount @ " " @ "errors" @ " ";
        %level = "info";
        "    ";
        if ((%testname > errorCount)) {
            %level = "error";
            0.0;
        }
        log("general", %level, %message);
        %i = (1.0 + %i);
    }
    echo(" ");
};
function TestSuite::construct(%name) {
    class = ScriptObject @ new %name() @ "TestSuite";
    0;
    testCount = 0;
    quitWhenDone = 0;
    %ret = ;
    if (isObject()) {
        %ret.add();
    }
    return %ret;
};
function TestSuite::ShouldRunOnServer(%this) {
    return 0;
};
function TestSuite::addTestCase(%this, %name) {
    test = %name @ %this @ testCount @ %this;
    TestDelay = 0 @ %this @ testCount @ %this;
    testCount = (%this + testCount);
    1.0;
};
function TestSuite::addTestCaseDelayed(%this, %name, %delay) {
    test = %name @ %this @ testCount @ %this;
    TestDelay = %delay @ %this @ testCount @ %this;
    testCount = (%this + testCount);
    1.0;
};
function TestSuite::setup(%this) {
    error("TestSuite::Setup: you must override this and addTestCases for the suite");
};
function TestSuite::TearDown(%this) {
};
function TestSuite::finishTesting(%this) {
    %this.TearDown();
    %this.reportResults();
    echo(%this.getName() @ " " @ ":  completed.");
    running = 0 @ %this;
    if (quitWhenDone) {
        quit();
    }
};
function TestSuite::FinishDelayedTest(%this, %testname) {
    cancel(TimerNextTest);
    %testname.executeFinishForDelay();
    %this.ExecNextTest();
};
function TestSuite::ExecNextTest(%this) {
    cancel(TimerNextTest);
    nextTest = (%this + nextTest);
    1.0;
    if ((%this >= nextTest)) {
        %this.finishTesting();
        return testCount;
    }
    %testname = test;
    %this @ nextTest @ %this;
    %delay = TestDelay;
    %this @ nextTest @ %this;
    TestCase::construct(%testname);
    if ((0.0 == %delay)) {
        %testname.execute();
        TimerNextTest = %this.schedule(0, "ExecNextTest") @ %this;
        return;
    }
    %testname.executeStartForDelay();
    TimerNextTest = %this.schedule(%delay, "FinishDelayedTest", %testname) @ %this;
    return;
};
function TestSuite::execute(%this) {
    running = 1 @ %this;
    TimerNextTest = 0 @ %this;
    %this.setup();
    echo(%this @ testCount @ " " @ "tests:");
    nextTest = %this.getName() @ " " @ ":  begin, with" @ " " @ -(1.0) @ %this;
    %this.ExecNextTest();
};
function TestSuite::reportResults(%this) {
    echo(" ");
    echo(%this.getName() @ " " @ "results summary --------------");
    errorCount = 0 @ %this;
    assertCount = 0 @ %this;
    %i = 0;
    if ((testCount < %i)) {
        %testname = test;
        %this @ %i @ %this;
        errorCount = (%this + errorCount);
        errorCount;
        assertCount = (%this + assertCount);
        assertCount;
        %message = %testname @ assertCount @ " " @ "asserts" @ " " @ "reported by" @ " " @ %testname;
        %testname @ errorCount @ " " @ "errors" @ " ";
        %level = "info";
        %testname @ "    ";
        if ((%testname > errorCount)) {
            %level = "error";
            0.0;
        }
        log("general", %level, %message);
        %i = (1.0 + %i);
        %testname;
    }
    echo(" ");
};
function TestCase::construct(%name) {
    class = ScriptObject @ new %name() @ "TestCase";
    0;
    errorCount = 0;
    assertCount = 0;
    %ret = ;
    if (isObject()) {
        %ret.add();
    }
    return %ret;
};
function TestCase::recordError(%this, %message, %messageBrief) {
    ErrorMessage = %this.getName() @ " " @ "error:" @ " " @ %message @ %this @ errorCount @ %this;
    ErrorMessageBrief = %messageBrief @ %this @ errorCount @ %this;
    errorCount = (%this + errorCount);
    1.0;
};
function TestCase::getErrorMessagesBrief(%this) {
    %ret = "";
    %delim = "";
    %n = 0;
    if ((errorCount < %n)) {
        %ret = %this @ %ret @ (1.0 + %n) @ "." @ %n @ %this @ ErrorMessageBrief @ "\n";
        %n = (1.0 + %n);
    }
    return %ret;
};
function TestCase::assert(%this, %val, %message) {
    assertCount = (%this + assertCount);
    1.0;
    if (!(%val)) {
        %messageBrief = %message;
        %this.recordError(%message, %messageBrief);
    }
    return !(%val);
};
function TestCase::assertSameObject(%this, %objA, %objB, %message) {
    assertCount = (%this + assertCount);
    1.0;
    %val = (%objB.getId() == %objA.getId());
    if (!(%val)) {
        %message = "\"" @ %objA.getId() @ "\"" @ " " @ "!=" @ " " @ "\"" @ %objB.getId() @ "\"" @ " " @ ":" @ " " @ %message;
        %messageBrief = "different:" @ " " @ getDebugString(%objA) @ " " @ getDebugString(%objB);
        %this.recordError(%message, %messageBrief);
    }
    return !(%val);
};
function TestCase::assertDifferentObject(%this, %objA, %objB, %message) {
    assertCount = (%this + assertCount);
    1.0;
    %val = (%objB.getId() != %objA.getId());
    if (!(%val)) {
        %message = "\"" @ %objA.getId() @ "\"" @ " " @ "!=" @ " " @ "\"" @ %objB.getId() @ "\"" @ " " @ ":" @ " " @ %message;
        %messageBrief = "same:" @ " " @ getDebugString(%objA) @ " " @ getDebugString(%objB);
        %this.recordError(%message, %messageBrief);
    }
    return !(%val);
};
function TestCase::assertSameString(%this, %strA, %strB, %message) {
    assertCount = (%this + assertCount);
    1.0;
    %val = (%strA $= %strB);
    if (!(%val)) {
        %message = "\"" @ %strA @ "\"" @ " " @ "!$=" @ " " @ "\"" @ %strB @ "\"" @ " " @ ":" @ " " @ %message;
        %messageBrief = "different:" @ " " @ %strA @ " " @ %strB;
        %this.recordError(%message, %messageBrief);
    }
    return !(%val);
};
function TestCase::assertDifferentString(%this, %strA, %strB, %message) {
    assertCount = (%this + assertCount);
    1.0;
    %val = !(%strA $= %strB);
    if (!(%val)) {
        %message = "\"" @ %strA @ "\"" @ " " @ "$=" @ " " @ "\"" @ %strB @ "\"" @ " " @ ":" @ " " @ %message;
        %messageBrief = "same:" @ " " @ %strA @ " " @ %strB;
        %this.recordError(%message, %messageBrief);
    }
    return !(%val);
};
function TestCase::reportResults(%this) {
    %i = 0;
    if ((errorCount < %i)) {
        %message = %this @ "  " @ %i @ %this @ ErrorMessage;
        log("general", %message);
        %i = (1.0 + %i);
        error;
    }
    echo(%this @ assertCount @ " " @ "assertions");
};
function TestCase::setup(%this) {
};
function TestCase::runTest(%this) {
    %this.assert(0, "TestCase::runTest must be overriden");
};
function TestCase::delayedEval(%this) {
};
function TestCase::TearDown(%this) {
};
function TestCase::executeStartForDelay(%this) {
    echo(%this.getName() @ " " @ ":  begin.");
    %this.setup();
    %this.runTest();
};
function TestCase::executeFinishForDelay(%this) {
    %this.delayedEval();
    %this.TearDown();
    echo(%this.getName() @ " " @ ":  completed.");
    echo(%this.getName() @ " " @ "results...");
    %this.reportResults();
};
function TestCase::execute(%this) {
    %this.executeStartForDelay();
    %this.executeFinishForDelay();
};
