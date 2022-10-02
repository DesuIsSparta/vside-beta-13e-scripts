$G_DECLARED_TEST_COUNT = 0;
$G_DECLARED_TEST[0] = 0;
function DeclareTestSuite(%name)
{
    $G_DECLARED_TEST[$G_DECLARED_TEST_COUNT] = %name ;
    $G_DECLARED_TEST_COUNT = $G_DECLARED_TEST_COUNT + 1;
    return ;
}
function DeclaredTestSuiteCount()
{
    return $G_DECLARED_TEST_COUNT;
}
function DeclaredTestSuiteGet(%num)
{
    if (%num < $G_DECLARED_TEST_COUNT)
    {
        return $G_DECLARED_TEST[%num];
    }
    return -1;
}
function RunTestSuite(%suitename)
{
    TestSuite::construct(%suitename);
    if (!$AmServer)
    {
        if (%suitename.ShouldRunOnServer())
        {
            %suitename.delete();
            echo("running" SPC %suitename SPC "on the server");
            commandToServer('RunTestSuiteServerSide', %suitename);
            return ;
        }
    }
    %suitename.execute();
    return ;
}
function serverCmdRunTestSuiteServerSide(%unused, %suitename)
{
    TestSuite::construct(%suitename);
    %suitename.execute();
    return ;
}
function RunTestSuite_QuiteWhenDone(%suitename)
{
    TestSuite::construct(%suitename);
    %suitename.quitWhenDone = 1;
    %suitename.execute();
    return ;
}
function RunTestCase(%testname, %dialogTitle)
{
    TestCase::construct(%testname);
    %testname.execute();
    if (!(%dialogTitle $= ""))
    {
        if (%testname.errorCount > 0)
        {
            %message = "<font:Arial Bold:18><color:FFFFFF>DO NOT IGNORE THIS MESSAGE, THESE NEED TO BE FIXED BEFORE YOU CHECK IN!!!!!<font:Arial Bold:12>Hi There! It\'s likely that the changes you have recently made have introduced some serious errors. Please don\'t check in until these are fixed. If the fix is not obvious, feel free to ask richard or terrence or orion or clint for help. Thanks!<br><br>There are **maybe" SPC %testname.errorCount SPC "problems with this missionfile.** Look in the console for things labeled <color:FF0000>TEST_MISSIONGROUPINTEGRITY<color:FFFFFF> in red, or talk to one of the engineers for help.\nAnd by the way, you are doing great work! Have a fine day." @ "<font:Arial:12>\n" @ %testname.getErrorMessagesBrief();
            %dlg = MessageBoxOK(%dialogTitle, %message, "");
            %dlg.window.resize(500, getWord(%dlg.getExtent(), 1));
        }
    }
    return %testname.errorCount;
}
function TestRunner_SmokeTests::setup(%this)
{
    %this.addTestSuite("TestSuite_AnimationSystemSmokeTests");
    %this.addTestSuite("TestSuite_CSSmokeTests");
    %this.addTestSuite("TestSuite_MissionGroup");
    %this.addTestSuite("TestSuite_SeatingSystemSmokeTests");
    return ;
}
function RunTestRunner(%runnername)
{
    TestSuiteRunner::construct(%runnername);
    %runnername.execute();
    return ;
}
function TestSuiteRunner::construct(%name)
{
    %ret = new ScriptObject(%name)
    {
        class = "TestSuiteRunner";
        testSuiteCount = 0;
        quitWhenDone = 0;
    };
    if (isObject(MissionCleanup))
    {
        MissionCleanup.add(%ret);
    }
    return %ret;
}
function TestSuiteRunner::addTestSuite(%this, %name)
{
    %this.Suite[%this.testSuiteCount] = %name;
    %this.testSuiteCount = %this.testSuiteCount + 1;
    return ;
}
function TestSuiteRunner::setup(%this)
{
    error("TestSuiteRunner::Setup: you must override this and addTestSuites for the runner");
    return ;
}
function TestSuiteRunner::TearDown(%this)
{
    return ;
}
function TestSuiteRunner::execute(%this)
{
    %this.setup();
    %this.running = 1;
    %this.currentSuite = 0;
    %this.TimerProcess = 0;
    echo(%this.getName() SPC ":  begin, with" SPC %this.suiteCount SPC "suites:");
    %this.nextSuite = -1;
    %this.ProcessLoop();
    return ;
}
function TestSuiteRunner::ProcessLoop(%this)
{
    cancel(%this.TimerProcess);
    if (isObject(%this.currentSuite))
    {
        if (%this.currentSuite.running)
        {
            %this.TimerProcess = %this.schedule(100, "ProcessLoop");
            return ;
        }
    }
    %this.nextSuite = %this.nextSuite + 1;
    if (%this.nextSuite >= %this.testSuiteCount)
    {
        %this.finishTesting();
        return ;
    }
    %testSuiteName = %this.Suite[%this.nextSuite];
    %this.currentSuite = TestSuite::construct(%testSuiteName);
    %this.currentSuite.execute();
    %this.TimerProcess = %this.schedule(100, "ProcessLoop");
    return ;
}
function TestSuiteRunner::finishTesting(%this)
{
    %this.TearDown();
    %this.reportResults();
    echo(%this.getName() SPC ":  completed.");
    %this.running = 0;
    if (%this.quitWhenDone)
    {
        quit();
    }
    return ;
}
function TestSuiteRunner::reportResults(%this)
{
    echo(" ");
    echo(%this.getName() SPC "results summary --------------");
    %i = 0;
    while (%i < %this.testSuiteCount)
    {
        %testname = %this.Suite[%i];
        %message = "    " @ %testname.errorCount SPC "errors" SPC %testname.assertCount SPC "asserts" SPC "reported by" SPC %testname;
        %level = "info";
        if (%testname.errorCount > 0)
        {
            %level = "error";
        }
        log("general", %level, %message);
        %i = %i + 1;
    }
    echo(" ");
    return ;
}
function TestSuite::construct(%name)
{
    %ret = new ScriptObject(%name)
    {
        class = "TestSuite";
        testCount = 0;
        quitWhenDone = 0;
    };
    if (isObject(MissionCleanup))
    {
        MissionCleanup.add(%ret);
    }
    return %ret;
}
function TestSuite::ShouldRunOnServer(%this)
{
    return 0;
}
function TestSuite::addTestCase(%this, %name)
{
    %this.test[%this.testCount] = %name;
    %this.TestDelay[%this.testCount] = 0;
    %this.testCount = %this.testCount + 1;
    return ;
}
function TestSuite::addTestCaseDelayed(%this, %name, %delay)
{
    %this.test[%this.testCount] = %name;
    %this.TestDelay[%this.testCount] = %delay;
    %this.testCount = %this.testCount + 1;
    return ;
}
function TestSuite::setup(%this)
{
    error("TestSuite::Setup: you must override this and addTestCases for the suite");
    return ;
}
function TestSuite::TearDown(%this)
{
    return ;
}
function TestSuite::finishTesting(%this)
{
    %this.TearDown();
    %this.reportResults();
    echo(%this.getName() SPC ":  completed.");
    %this.running = 0;
    if (%this.quitWhenDone)
    {
        quit();
    }
    return ;
}
function TestSuite::FinishDelayedTest(%this, %testname)
{
    cancel(%this.TimerNextTest);
    %testname.executeFinishForDelay();
    %this.ExecNextTest();
    return ;
}
function TestSuite::ExecNextTest(%this)
{
    cancel(%this.TimerNextTest);
    %this.nextTest = %this.nextTest + 1;
    if (%this.nextTest >= %this.testCount)
    {
        %this.finishTesting();
        return ;
    }
    %testname = %this.test[%this.nextTest];
    %delay = %this.TestDelay[%this.nextTest];
    TestCase::construct(%testname);
    if (%delay == 0)
    {
        %testname.execute();
        %this.TimerNextTest = %this.schedule(0, "ExecNextTest");
        return ;
    }
    else
    {
        %testname.executeStartForDelay();
        %this.TimerNextTest = %this.schedule(%delay, "FinishDelayedTest", %testname);
        return ;
    }
    return ;
}
function TestSuite::execute(%this)
{
    %this.running = 1;
    %this.TimerNextTest = 0;
    %this.setup();
    echo(%this.getName() SPC ":  begin, with" SPC %this.testCount SPC "tests:");
    %this.nextTest = -1;
    %this.ExecNextTest();
    return ;
}
function TestSuite::reportResults(%this)
{
    echo(" ");
    echo(%this.getName() SPC "results summary --------------");
    %this.errorCount = 0;
    %this.assertCount = 0;
    %i = 0;
    while (%i < %this.testCount)
    {
        %testname = %this.test[%i];
        %this.errorCount = %this.errorCount + %testname.errorCount;
        %this.assertCount = %this.assertCount + %testname.assertCount;
        %message = "    " @ %testname.errorCount SPC "errors" SPC %testname.assertCount SPC "asserts" SPC "reported by" SPC %testname;
        %level = "info";
        if (%testname.errorCount > 0)
        {
            %level = "error";
        }
        log("general", %level, %message);
        %i = %i + 1;
    }
    echo(" ");
    return ;
}
function TestCase::construct(%name)
{
    %ret = new ScriptObject(%name)
    {
        class = "TestCase";
        errorCount = 0;
        assertCount = 0;
    };
    if (isObject(MissionCleanup))
    {
        MissionCleanup.add(%ret);
    }
    return %ret;
}
function TestCase::recordError(%this, %message, %messageBrief)
{
    %this.ErrorMessage[%this.errorCount] = %this.getName() SPC "error:" SPC %message;
    %this.ErrorMessageBrief[%this.errorCount] = %messageBrief;
    %this.errorCount = %this.errorCount + 1;
    return ;
}
function TestCase::getErrorMessagesBrief(%this)
{
    %ret = "";
    %delim = "";
    %n = 0;
    while (%n < %this.errorCount)
    {
        %ret = %ret @ %n + 1 @ "." @ %this.ErrorMessageBrief[%n] @ "\n";
        %n = %n + 1;
    }
    return %ret;
}
function TestCase::assert(%this, %val, %message)
{
    %this.assertCount = %this.assertCount + 1;
    if (!%val)
    {
        %messageBrief = %message;
        %this.recordError(%message, %messageBrief);
    }
    return !%val;
}
function TestCase::assertSameObject(%this, %objA, %objB, %message)
{
    %this.assertCount = %this.assertCount + 1;
    %val = %objA.getId() == %objB.getId();
    if (!%val)
    {
        %message = "\"" @ %objA.getId() @ "\"" SPC "!=" SPC "\"" @ %objB.getId() @ "\"" SPC ":" SPC %message;
        %messageBrief = "different:" SPC getDebugString(%objA) SPC getDebugString(%objB);
        %this.recordError(%message, %messageBrief);
    }
    return !%val;
}
function TestCase::assertDifferentObject(%this, %objA, %objB, %message)
{
    %this.assertCount = %this.assertCount + 1;
    %val = %objA.getId() != %objB.getId();
    if (!%val)
    {
        %message = "\"" @ %objA.getId() @ "\"" SPC "!=" SPC "\"" @ %objB.getId() @ "\"" SPC ":" SPC %message;
        %messageBrief = "same:" SPC getDebugString(%objA) SPC getDebugString(%objB);
        %this.recordError(%message, %messageBrief);
    }
    return !%val;
}
function TestCase::assertSameString(%this, %strA, %strB, %message)
{
    %this.assertCount = %this.assertCount + 1;
    %val = %strA $= %strB;
    if (!%val)
    {
        %message = "\"" @ %strA @ "\"" SPC "!$=" SPC "\"" @ %strB @ "\"" SPC ":" SPC %message;
        %messageBrief = "different:" SPC %strA SPC %strB;
        %this.recordError(%message, %messageBrief);
    }
    return !%val;
}
function TestCase::assertDifferentString(%this, %strA, %strB, %message)
{
    %this.assertCount = %this.assertCount + 1;
    %val = !(%strA $= %strB);
    if (!%val)
    {
        %message = "\"" @ %strA @ "\"" SPC "$=" SPC "\"" @ %strB @ "\"" SPC ":" SPC %message;
        %messageBrief = "same:" SPC %strA SPC %strB;
        %this.recordError(%message, %messageBrief);
    }
    return !%val;
}
function TestCase::reportResults(%this)
{
    %i = 0;
    while (%i < %this.errorCount)
    {
        %message = "  " @ %this.ErrorMessage[%i];
        log("general", error, %message);
        %i = %i + 1;
    }
    echo(%this.getName() SPC ":" SPC %this.errorCount SPC "errors found, " SPC %this.assertCount SPC "assertions");
    return ;
}
function TestCase::setup(%this)
{
    return ;
}
function TestCase::runTest(%this)
{
    %this.assert(0, "TestCase::runTest must be overriden");
    return ;
}
function TestCase::delayedEval(%this)
{
    return ;
}
function TestCase::TearDown(%this)
{
    return ;
}
function TestCase::executeStartForDelay(%this)
{
    echo(%this.getName() SPC ":  begin.");
    %this.setup();
    %this.runTest();
    return ;
}
function TestCase::executeFinishForDelay(%this)
{
    %this.delayedEval();
    %this.TearDown();
    echo(%this.getName() SPC ":  completed.");
    echo(%this.getName() SPC "results...");
    %this.reportResults();
    return ;
}
function TestCase::execute(%this)
{
    %this.executeStartForDelay();
    %this.executeFinishForDelay();
    return ;
}
