function doAnimationSystemSuiteTest() {
    RunTestSuite("TestSuite_AnimationSystemSmokeTests");
};
DeclareTestSuite("TestSuite_AnimationSystemSmokeTests");
function TestSuite_AnimationSystemSmokeTests::setup(%this) {
    1000.addTestCaseDelayed(%this, "TEST_PlayAnimIdleB");
    1000.addTestCaseDelayed(%this, "TEST_PlayAnimIdleA");
};
function TEST_PlayAnimIdleB::runTest(%this) {
    %this.animName = "idl1b";
    %this.expectedAnimName = $player.getGender() @ $player.getGenre() @ %this.animName;
    commandToServer('EtsPlayAnimName', %this.animName);
};
function TEST_PlayAnimIdleB::delayedEval(%this) {
    %curr = $player.getCurrActionName();
    "expected the player to be playing the animation by now".assertSameString(%this, %curr, %this.expectedAnimName);
};
function TEST_PlayAnimIdleA::runTest(%this) {
    %this.animName = "idl1a";
    %this.expectedAnimName = $player.getGender() @ $player.getGenre() @ %this.animName;
    commandToServer('EtsPlayAnimName', %this.animName);
};
function TEST_PlayAnimIdleA::delayedEval(%this) {
    %curr = $player.getCurrActionName();
    "expected the player to be playing the animation by now".assertSameString(%this, %curr, %this.expectedAnimName);
};
