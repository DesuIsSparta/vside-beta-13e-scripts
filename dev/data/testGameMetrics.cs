DeclareTestSuite("TestSuite_GameMetrics");
function TestSuite_GameMetrics::setup(%this)
{
    %this.addTestCase("TEST_GAMEMETRICS_BASICS");
    return ;
}
function TEST_GAMEMETRICS_BASICS::runTest(%this)
{
    if (!$StandAlone)
    {
        %this.assert(0, "this test must be run in $standalone");
        return ;
    }
    %player = $StandaloneServerPlayer;
    GMetrics::GamePlayStartEvent("TEST", "TEST_GAMEMETRICS_BASICS_ONE", %player);
    GMetrics::GamePlayStopEvent("TEST", "TEST_GAMEMETRICS_BASICS_ONE", %player, 0);
    GMetrics::GamePlayStartEvent("TEST", "TEST_GAMEMETRICS_BASICS_TWO", %player);
    GMetrics::GamePlayStopEvent("TEST", "TEST_GAMEMETRICS_BASICS_TWO", %player, 10000);
    GMetrics::GamePlayStartEvent("TEST", "TEST_GAMEMETRICS_BASICS_THREE", %player);
    GMetrics::GamePlayStartEvent("TEST", "TEST_GAMEMETRICS_BASICS_THREE", %player);
    GMetrics::GamePlayStopEvent("TEST", "TEST_GAMEMETRICS_BASICS_THREE", %player, 10000);
    GMetrics::GamePlayStopEvent("TEST", "TEST_GAMEMETRICS_BASICS_THREE", %player, 10000);
    GMetrics::GamePlayStartEvent("TEST", "TEST_GAMEMETRICS_BASICS_THREE", %player);
    GMetrics::GamePlayStartEvent("TEST", "TEST_GAMEMETRICS_BASICS_THREE", %player);
    GMetrics::GamePlayStopEvent("TEST", "TEST_GAMEMETRICS_BASICS_THREE", %player, 10000);
    GMetrics::GamePlayStopEvent("TEST", "TEST_GAMEMETRICS_BASICS_THREE", %player, 10000);
    GMetrics::GameTouchEvent("TEST", "TEST_GAMEMETRICS_BASICS_FOUR", %player, 2000);
    GMetrics::GameTouchEvent("TEST", "TEST_GAMEMETRICS_BASICS_FOUR", %player, 2000);
    GMetrics::GamePlayStartEvent("TEST", "TEST_GAMEMETRICS_BASICS_FIVE", %player);
    GMetrics::GamePlayStartEvent("TEST", "TEST_GAMEMETRICS_BASICS_FIVE", %player);
    GMetrics::GamePlayStopEvent("TEST", "TEST_GAMEMETRICS_BASICS_FIVE", %player, 10000);
    GMetrics::GamePlayStopEvent("TEST", "TEST_GAMEMETRICS_BASICS_FIVE", %player, 11000);
    GMetrics::GamePlayStartEvent("TEST", "TEST_GAMEMETRICS_BASICS_SIX", %player);
    GMetrics::GamePlayStopEvent("TEST", "TEST_GAMEMETRICS_BASICS_SIX", %player, 10000);
    GMetrics::GamePlayStartEvent("TEST", "TEST_GAMEMETRICS_BASICS_SIX", %player);
    GMetrics::GamePlayStopEvent("TEST", "TEST_GAMEMETRICS_BASICS_SIX", %player, 10000);
    %this.assert(0, "you will have to check the gameMetrics.log in a short time to see if this test really worked");
    return ;
}
