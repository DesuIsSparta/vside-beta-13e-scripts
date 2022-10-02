function doSeatingSystemSuiteTest()
{
    RunTestSuite("TestSuite_SeatingSystemSmokeTests");
    return ;
}
DeclareTestSuite("TestSuite_SeatingSystemSmokeTests");
function testSeatingSystem_Master()
{
    RunTestSuite_QuiteWhenDone("TestSuite_SeatingSystemSmokeTests");
    return ;
}
function TestSuite_SeatingSystemSmokeTests::setup(%this)
{
    if (isObject(TestSeatingSystemTestSet))
    {
        TestSeatingSystemTestSet.delete();
    }
    new SimSet(TestSeatingSystemTestSet);
    MissionCleanup.add(TestSeatingSystemTestSet);
    recursiveCollectSeatsFromSimGroup(MissionGroup, TestSeatingSystemTestSet);
    commandToServer('dropCameraAtPlayer');
    commandToServer('dropPlayerAtCamera');
    %this.addTestCase("TEST_SeatAvailable");
    %this.addTestCaseDelayed("TEST_SitDown", 1000);
    %this.addTestCaseDelayed("TEST_StandUp", 2000);
    %this.addTestCaseDelayed("TEST_SitDown", 1000);
    %this.addTestCaseDelayed("TEST_TeleportAway", 500);
    return ;
}
function TestSuite_SeatingSystemSmokeTests::TearDown(%this)
{
    if (isObject(TestSeatingSystemTestSet))
    {
        TestSeatingSystemTestSet.delete();
    }
    return ;
}
function TEST_SeatAvailable::runTest(%this)
{
    %seatID = TestSeatingSystemTestSet.getObject(0);
    %this.assert(!%seatID.isSeatTaken(), %seatID SPC "is not available, it was expected to be");
    return ;
}
function TEST_SitDown::runTest(%this)
{
    %seatID = TestSeatingSystemTestSet.getObject(0);
    %this.assert(!%seatID.isSeatTaken(), %seatID SPC "should not be taken if we are going to sit down in it");
    commandToServer('RequestToSit', %seatID);
    return ;
}
function TEST_SitDown::delayedEval(%this)
{
    %this.assert($player.isSitting(), "the player should be sitting after we tell her to");
    %seatID = TestSeatingSystemTestSet.getObject(0);
    %this.assert(%seatID.isSeatTaken(), %seatID SPC "is not taken, after we sit down, it should be taken");
    return ;
}
function TEST_StandUp::runTest(%this)
{
    %this.assert($player.isSitting(), "the player should be sitting if we are running the standup test");
    SendStandCommand(1);
    return ;
}
function TEST_StandUp::delayedEval(%this)
{
    %this.assert(!$player.isSitting(), "the player should be standing up after well tell him to");
    %seatID = TestSeatingSystemTestSet.getObject(0);
    %this.assert(!%seatID.isSeatTaken(), %seatID SPC "is taken, but it should be available after we stand up");
    return ;
}
function TEST_TeleportAway::runTest(%this)
{
    %this.assert($player.isSitting(), "the player should be sitting if we are running the teleport away test");
    commandToServer('DropPlayerAtCamera');
    return ;
}
function TEST_TeleportAway::delayedEval(%this)
{
    %this.assert(!$player.isSitting(), "the player should be standing up after well tell him to");
    %seatID = TestSeatingSystemTestSet.getObject(0);
    %this.assert(!%seatID.isSeatTaken(), %seatID SPC "is taken, but it should be available after we stand up");
    %this.animName = $PLAYER_FORCE_IDLE_ANIM;
    %this.expectedAnimName = $player.getGender() @ $player.getGenre() @ %this.animName;
    %curr = $player.getCurrActionName();
    %this.assertSameString(%curr, %this.expectedAnimName, "expected the player to be in the force idle anim after we teleport away");
    return ;
}
