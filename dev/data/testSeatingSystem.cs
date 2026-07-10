function doSeatingSystemSuiteTest() {
    RunTestSuite("TestSuite_SeatingSystemSmokeTests");
};
DeclareTestSuite("TestSuite_SeatingSystemSmokeTests");
function testSeatingSystem_Master() {
    RunTestSuite_QuiteWhenDone("TestSuite_SeatingSystemSmokeTests");
};
function TestSuite_SeatingSystemSmokeTests::setup(%this) {
    if (isObject(TestSeatingSystemTestSet)) {
        TestSeatingSystemTestSet.delete();
    }
    new SimSet(TestSeatingSystemTestSet);
    TestSeatingSystemTestSet.add(MissionCleanup);
    recursiveCollectSeatsFromSimGroup(MissionGroup, TestSeatingSystemTestSet);
    commandToServer('dropCameraAtPlayer');
    commandToServer('dropPlayerAtCamera');
    "TEST_SeatAvailable".addTestCase(%this);
    1000.addTestCaseDelayed(%this, "TEST_SitDown");
    2000.addTestCaseDelayed(%this, "TEST_StandUp");
    1000.addTestCaseDelayed(%this, "TEST_SitDown");
    500.addTestCaseDelayed(%this, "TEST_TeleportAway");
};
function TestSuite_SeatingSystemSmokeTests::TearDown(%this) {
    if (isObject(TestSeatingSystemTestSet)) {
        TestSeatingSystemTestSet.delete();
    }
};
function TEST_SeatAvailable::runTest(%this) {
    %seatID = 0.getObject(TestSeatingSystemTestSet);
    %seatID @ " " @ "is not available, it was expected to be".assert(%this, !(%seatID.isSeatTaken()));
};
function TEST_SitDown::runTest(%this) {
    %seatID = 0.getObject(TestSeatingSystemTestSet);
    %seatID @ " " @ "should not be taken if we are going to sit down in it".assert(%this, !(%seatID.isSeatTaken()));
    commandToServer('RequestToSit', %seatID);
};
function TEST_SitDown::delayedEval(%this) {
    "the player should be sitting after we tell her to".assert(%this, $player.isSitting());
    %seatID = 0.getObject(TestSeatingSystemTestSet);
    %seatID @ " " @ "is not taken, after we sit down, it should be taken".assert(%this, %seatID.isSeatTaken());
};
function TEST_StandUp::runTest(%this) {
    "the player should be sitting if we are running the standup test".assert(%this, $player.isSitting());
    SendStandCommand(1);
};
function TEST_StandUp::delayedEval(%this) {
    "the player should be standing up after well tell him to".assert(%this, !($player.isSitting()));
    %seatID = 0.getObject(TestSeatingSystemTestSet);
    %seatID @ " " @ "is taken, but it should be available after we stand up".assert(%this, !(%seatID.isSeatTaken()));
};
function TEST_TeleportAway::runTest(%this) {
    "the player should be sitting if we are running the teleport away test".assert(%this, $player.isSitting());
    commandToServer('DropPlayerAtCamera');
};
function TEST_TeleportAway::delayedEval(%this) {
    "the player should be standing up after well tell him to".assert(%this, !($player.isSitting()));
    %seatID = 0.getObject(TestSeatingSystemTestSet);
    %seatID @ " " @ "is taken, but it should be available after we stand up".assert(%this, !(%seatID.isSeatTaken()));
    %this.animName = $PLAYER_FORCE_IDLE_ANIM;
    %this.expectedAnimName = $player.getGender() @ $player.getGenre() @ %this.animName;
    %curr = $player.getCurrActionName();
    "expected the player to be in the force idle anim after we teleport away".assertSameString(%this, %curr, %this.expectedAnimName);
};
