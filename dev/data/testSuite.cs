$G_DECLARED_TEST_COUNT = 0;
$G_DECLARED_TEST_COUNT[$G_DECLARED_TEST @ 0] = 0;
function DeclareTestSuite(%name) {
    $G_DECLARED_TEST_COUNT[$G_DECLARED_TEST @ $G_DECLARED_TEST_COUNT] = %name;
    $G_DECLARED_TEST_COUNT = ($G_DECLARED_TEST_COUNT + 1.0);
};
function DeclaredTestSuiteCount() {
    return $G_DECLARED_TEST_COUNT;
};
function DeclaredTestSuiteGet(%num) {
    if ((%num < $G_DECLARED_TEST_COUNT)) {
        return %num[$G_DECLARED_TEST @ %num];
    }
    return -(1.0);
};
function RunTestSuite(%suitename) {
    TestSuite::construct(%suitename);
    if (!($AmServer) && %suitename.ShouldRunOnServer()) {
        %suitename.delete();
        echo("running" @ " " @ %suitename @ " " @ "on the server");
        commandToServer('RunTestSuiteServerSide', %suitename);
        return;
    }
    %suitename.execute();
};
function serverCmdRunTestSuiteServerSide(%unused, %suitename) {
    TestSuite::construct(%suitename);
    %suitename.execute();
};
function RunTestSuite_QuiteWhenDone(%suitename) {
    TestSuite::construct(%suitename);
    %suitename.quitWhenDone = 1;
    %suitename.execute();
};
function RunTestCase(%testname, %dialogTitle) {
    TestCase::construct(%testname);
    %testname.execute();
    if (!(%dialogTitle $= "") && (%testname.errorCount > 0.0)) {
        %message = "<font:Arial Bold:18><color:FFFFFF>DO NOT IGNORE THIS MESSAGE, THESE NEED TO BE FIXED BEFORE YOU CHECK IN!!!!!<font:Arial Bold:12>Hi There! It's likely that the changes you have recently made have introduced some serious errors. Please don't check in until these are fixed. If the fix is not obvious, feel free to ask richard or terrence or orion or clint for help. Thanks!<br><br>There are **maybe" @ " " @ %testname.errorCount @ " " @ "problems with this missionfile.** Look in the console for things labeled <color:FF0000>TEST_MISSIONGROUPINTEGRITY<color:FFFFFF> in red, or talk to one of the engineers for help.\nAnd by the way, you are doing great work! Have a fine day." @ "<font:Arial:12>\n" @ %testname.getErrorMessagesBrief();
        %dlg = MessageBoxOK(%dialogTitle, %message, "");
        getWord(%dlg.getExtent(), 1).resize(%dlg.window, 500);
    }
    return %testname.errorCount;
};
function TestRunner_SmokeTests::setup(%this) {
    "TestSuite_AnimationSystemSmokeTests".addTestSuite(%this);
    "TestSuite_CSSmokeTests".addTestSuite(%this);
    "TestSuite_MissionGroup".addTestSuite(%this);
    "TestSuite_SeatingSystemSmokeTests".addTestSuite(%this);
};
function RunTestRunner(%runnername) {
    TestSuiteRunner::construct(%runnername);
    %runnername.execute();
};
function TestSuiteRunner::construct(%name) {
    %ret = new ScriptObject(%name) {
        class = "TestSuiteRunner";
        testSuiteCount = 0;
        quitWhenDone = 0;
    };
    if (isObject(MissionCleanup)) {
        %ret.add(MissionCleanup);
    }
    return %ret;
};
function TestSuiteRunner::addTestSuite(%this, %name) {
    %this.Suite = %name @ %this.testSuiteCount;
    %this.testSuiteCount = (%this.testSuiteCount + 1.0);
};
function TestSuiteRunner::setup(%this) {
    error("TestSuiteRunner::Setup: you must override this and addTestSuites for the runner");
};
function TestSuiteRunner::TearDown(%this) {
};
function TestSuiteRunner::execute(%this) {
    %this.setup();
    %this.running = 1;
    %this.currentSuite = 0;
    %this.TimerProcess = 0;
    echo(%this.getName() @ " " @ ":  begin, with" @ " " @ %this.suiteCount @ " " @ "suites:");
    %this.nextSuite = -(1.0);
    %this.ProcessLoop();
};
function TestSuiteRunner::ProcessLoop(%this) {
    cancel(%this.TimerProcess);
    if (isObject(%this.currentSuite) && %this.currentSuite.running) {
        %this.TimerProcess = "ProcessLoop".schedule(%this, 100);
        return;
    }
    %this.nextSuite = (%this.nextSuite + 1.0);
    if ((%this.nextSuite >= %this.testSuiteCount)) {
        %this.finishTesting();
        return;
    }
    %testSuiteName = %this.Suite;
    %this.currentSuite = %this.nextSuite @ TestSuite::construct(%testSuiteName);
    %this.currentSuite.execute();
    %this.TimerProcess = "ProcessLoop".schedule(%this, 100);
};
function TestSuiteRunner::finishTesting(%this) {
    %this.TearDown();
    %this.reportResults();
    echo(%this.getName() @ " " @ ":  completed.");
    %this.running = 0;
    if (%this.quitWhenDone) {
        quit();
    }
};
function TestSuiteRunner::reportResults(%this) {
    echo(" ");
    echo(%this.getName() @ " " @ "results summary --------------");
    %i = 0;
    while ((%i < %this.testSuiteCount)) {
        %testname = %this.Suite;
        %message = "    " @ %testname.errorCount @ " " @ "errors" @ " " @ %testname.assertCount @ " " @ "asserts" @ " " @ "reported by" @ " " @ %testname;
        %level = "info";
        if ((%testname.errorCount > 0.0)) {
            %level = "error";
        }
        log("general", %level, %message);
        %i = (%i + 1.0);
    }
    echo(" ");
};
function TestSuite::construct(%name) {
    %ret = new ScriptObject(%name) {
        class = "TestSuite";
        testCount = 0;
        quitWhenDone = 0;
    };
    if (isObject(MissionCleanup)) {
        %ret.add(MissionCleanup);
    }
    return %ret;
};
function TestSuite::ShouldRunOnServer(%this) {
    return 0;
};
function TestSuite::addTestCase(%this, %name) {
    %this.test = %name @ %this.testCount;
    %this.TestDelay = 0 @ %this.testCount;
    %this.testCount = (%this.testCount + 1.0);
};
function TestSuite::addTestCaseDelayed(%this, %name, %delay) {
    %this.test = %name @ %this.testCount;
    %this.TestDelay = %delay @ %this.testCount;
    %this.testCount = (%this.testCount + 1.0);
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
    %this.running = 0;
    if (%this.quitWhenDone) {
        quit();
    }
};
function TestSuite::FinishDelayedTest(%this, %testname) {
    cancel(%this.TimerNextTest);
    %testname.executeFinishForDelay();
    %this.ExecNextTest();
};
function TestSuite::ExecNextTest(%this) {
    cancel(%this.TimerNextTest);
    %this.nextTest = (%this.nextTest + 1.0);
    if ((%this.nextTest >= %this.testCount)) {
        %this.finishTesting();
        return;
    }
    %testname = %this.test;
    %delay = %this.TestDelay;
    TestCase::construct(%testname);
    if ((%delay == 0.0)) {
        %testname.execute();
        %this.TimerNextTest = %this.nextTest @ %this.nextTest @ "ExecNextTest".schedule(%this, 0);
        return;
    }
    %testname.executeStartForDelay();
    %this.TimerNextTest = %testname.schedule(%this, %delay, "FinishDelayedTest");
    return;
};
function TestSuite::execute(%this) {
    %this.running = 1;
    %this.TimerNextTest = 0;
    %this.setup();
    echo(%this.getName() @ " " @ ":  begin, with" @ " " @ %this.testCount @ " " @ "tests:");
    %this.nextTest = -(1.0);
    %this.ExecNextTest();
};
function TestSuite::reportResults(%this) {
    echo(" ");
    echo(%this.getName() @ " " @ "results summary --------------");
    %this.errorCount = 0;
    %this.assertCount = 0;
    %i = 0;
    while ((%i < %this.testCount)) {
        %testname = %this.test;
        %this.errorCount = (%this.errorCount + %testname.errorCount);
        %i;
        %this.assertCount = (%this.assertCount + %testname.assertCount);
        %message = "    " @ %testname.errorCount @ " " @ "errors" @ " " @ %testname.assertCount @ " " @ "asserts" @ " " @ "reported by" @ " " @ %testname;
        %level = "info";
        if ((%testname.errorCount > 0.0)) {
            %level = "error";
        }
        log("general", %level, %message);
        %i = (%i + 1.0);
    }
    echo(" ");
};
function TestCase::construct(%name) {
    %ret = new ScriptObject(%name) {
        class = "TestCase";
        errorCount = 0;
        assertCount = 0;
    };
    if (isObject(MissionCleanup)) {
        %ret.add(MissionCleanup);
    }
    return %ret;
};
function TestCase::recordError(%this, %message, %messageBrief) {
    %this.ErrorMessage = %this.getName() @ " " @ "error:" @ " " @ %message @ %this.errorCount;
    %this.ErrorMessageBrief = %messageBrief @ %this.errorCount;
    %this.errorCount = (%this.errorCount + 1.0);
};
function TestCase::getErrorMessagesBrief(%this) {
    %ret = "";
    %delim = "";
    %n = 0;
    while ((%n < %this.errorCount)) {
        %ret = %ret @ (%n + 1.0) @ "." @ %n @ %this.ErrorMessageBrief @ "\n";
        %n = (%n + 1.0);
    }
    return %ret;
};
function TestCase::assert(%this, %val, %message) {
    %this.assertCount = (%this.assertCount + 1.0);
    if (!(%val)) {
        %messageBrief = %message;
        %messageBrief.recordError(%this, %message);
    }
    return !(%val);
};
function TestCase::assertSameObject(%this, %objA, %objB, %message) {
    %this.assertCount = (%this.assertCount + 1.0);
    %val = (%objA.getId() == %objB.getId());
    if (!(%val)) {
        %message = "\"" @ %objA.getId() @ "\"" @ " " @ "!=" @ " " @ "\"" @ %objB.getId() @ "\"" @ " " @ ":" @ " " @ %message;
        %messageBrief = "different:" @ " " @ getDebugString(%objA) @ " " @ getDebugString(%objB);
        %messageBrief.recordError(%this, %message);
    }
    return !(%val);
};
function TestCase::assertDifferentObject(%this, %objA, %objB, %message) {
    %this.assertCount = (%this.assertCount + 1.0);
    %val = (%objA.getId() != %objB.getId());
    if (!(%val)) {
        %message = "\"" @ %objA.getId() @ "\"" @ " " @ "!=" @ " " @ "\"" @ %objB.getId() @ "\"" @ " " @ ":" @ " " @ %message;
        %messageBrief = "same:" @ " " @ getDebugString(%objA) @ " " @ getDebugString(%objB);
        %messageBrief.recordError(%this, %message);
    }
    return !(%val);
};
function TestCase::assertSameString(%this, %strA, %strB, %message) {
    %this.assertCount = (%this.assertCount + 1.0);
    %val = (%strA $= %strB);
    if (!(%val)) {
        %message = "\"" @ %strA @ "\"" @ " " @ "!$=" @ " " @ "\"" @ %strB @ "\"" @ " " @ ":" @ " " @ %message;
        %messageBrief = "different:" @ " " @ %strA @ " " @ %strB;
        %messageBrief.recordError(%this, %message);
    }
    return !(%val);
};
function TestCase::assertDifferentString(%this, %strA, %strB, %message) {
    %this.assertCount = (%this.assertCount + 1.0);
    %val = !(%strA $= %strB);
    if (!(%val)) {
        %message = "\"" @ %strA @ "\"" @ " " @ "$=" @ " " @ "\"" @ %strB @ "\"" @ " " @ ":" @ " " @ %message;
        %messageBrief = "same:" @ " " @ %strA @ " " @ %strB;
        %messageBrief.recordError(%this, %message);
    }
    return !(%val);
};
function TestCase::reportResults(%this) {
    %i = 0;
    while ((%i < %this.errorCount)) {
        %message = "  " @ %i @ %this.ErrorMessage;
        log("general", error, %message);
        %i = (%i + 1.0);
    }
    echo(%this.getName() @ " " @ ":" @ " " @ %this.errorCount @ " " @ "errors found, " @ " " @ %this.assertCount @ " " @ "assertions");
};
function TestCase::setup(%this) {
};
function TestCase::runTest(%this) {
    "TestCase::runTest must be overriden".assert(%this, 0);
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
