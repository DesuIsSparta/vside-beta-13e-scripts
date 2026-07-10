function doAnimationSystemSuiteTest() {
    RunTestSuite("TestSuite_AnimationSystemSmokeTests");
};
DeclareTestSuite("TestSuite_AnimationSystemSmokeTests");
function TestSuite_AnimationSystemSmokeTests::setup(%this) {
    %this.addTestCaseDelayed("TEST_PlayAnimIdleB", 1000);
    %this.addTestCaseDelayed("TEST_PlayAnimIdleA", 1000);
};
function TEST_PlayAnimIdleB::runTest(%this) {
    animName = "idl1b" @ %this;
    expectedAnimName = $player.getGender() @ $player.getGenre() @ %this @ animName @ %this;
    commandToServer('EtsPlayAnimName', animName);
};
function TEST_PlayAnimIdleB::delayedEval(%this) {
    %curr = $player.getCurrActionName();
    %this.assertSameString(%curr, expectedAnimName, "expected the player to be playing the animation by now");
};
function TEST_PlayAnimIdleA::runTest(%this) {
    animName = "idl1a" @ %this;
    expectedAnimName = $player.getGender() @ $player.getGenre() @ %this @ animName @ %this;
    commandToServer('EtsPlayAnimName', animName);
};
function TEST_PlayAnimIdleA::delayedEval(%this) {
    %curr = $player.getCurrActionName();
    %this.assertSameString(%curr, expectedAnimName, "expected the player to be playing the animation by now");
};
