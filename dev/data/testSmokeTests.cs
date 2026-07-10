function TestRunner_SmokeTestQuick::setup(%this) {
    "TestSuite_CSSmokeTests".addTestSuite(%this);
    "TestSuite_GameState".addTestSuite(%this);
    "TestSuite_Games_Collection".addTestSuite(%this);
    "TestSuite_MissionGroup".addTestSuite(%this);
    "TestSuite_VURL".addTestSuite(%this);
    "TestSuite_NAMESPACE".addTestSuite(%this);
};
function TestRunner_SmokeTestLong::setup(%this) {
    TestRunner_SmokeTestQuick::setup(%this);
    "TestSuite_AnimationSystemSmokeTests".addTestSuite(%this);
    "TestSuite_SeatingSystemSmokeTests".addTestSuite(%this);
    "TestSuite_GameMetrics".addTestSuite(%this);
};
function SmokeTestQuick() {
    RunTestRunner("TestRunner_SmokeTestQuick");
};
function SmokeTestLong() {
    RunTestRunner("TestRunner_SmokeTestLong");
};
