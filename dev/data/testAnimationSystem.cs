function doAnimationSystemSuiteTest()
{
    RunTestSuite("TestSuite_AnimationSystemSmokeTests");
    return ;
}
DeclareTestSuite("TestSuite_AnimationSystemSmokeTests");
function TestSuite_AnimationSystemSmokeTests::setup(%this)
{
    %this.addTestCaseDelayed("TEST_PlayAnimIdleB", 1000);
    %this.addTestCaseDelayed("TEST_PlayAnimIdleA", 1000);
    return ;
}
function TEST_PlayAnimIdleB::runTest(%this)
{
    %this.animName = "idl1b";
    %this.expectedAnimName = $player.getGender() @ $player.getGenre() @ %this.animName;
    commandToServer('EtsPlayAnimName', %this.animName);
    return ;
}
function TEST_PlayAnimIdleB::delayedEval(%this)
{
    %curr = $player.getCurrActionName();
    %this.assertSameString(%curr, %this.expectedAnimName, "expected the player to be playing the animation by now");
    return ;
}
function TEST_PlayAnimIdleA::runTest(%this)
{
    %this.animName = "idl1a";
    %this.expectedAnimName = $player.getGender() @ $player.getGenre() @ %this.animName;
    commandToServer('EtsPlayAnimName', %this.animName);
    return ;
}
function TEST_PlayAnimIdleA::delayedEval(%this)
{
    %curr = $player.getCurrActionName();
    %this.assertSameString(%curr, %this.expectedAnimName, "expected the player to be playing the animation by now");
    return ;
}
