function TestRunner_SmokeTestQuick::setup(%this)
{
    %this.addTestSuite("TestSuite_CSSmokeTests");
    %this.addTestSuite("TestSuite_GameState");
    %this.addTestSuite("TestSuite_Games_Collection");
    %this.addTestSuite("TestSuite_MissionGroup");
    %this.addTestSuite("TestSuite_VURL");
    %this.addTestSuite("TestSuite_NAMESPACE");
}
function TestRunner_SmokeTestLong::setup(%this)
{
    TestRunner_SmokeTestQuick::setup(%this);
    %this.addTestSuite("TestSuite_AnimationSystemSmokeTests");
    %this.addTestSuite("TestSuite_SeatingSystemSmokeTests");
    %this.addTestSuite("TestSuite_GameMetrics");
}
function SmokeTestQuick()
{
    RunTestRunner("TestRunner_SmokeTestQuick");
}
function SmokeTestLong()
{
    RunTestRunner("TestRunner_SmokeTestLong");
}
