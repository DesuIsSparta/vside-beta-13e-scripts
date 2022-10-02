DeclareTestSuite("TestSuite_GameState");
function TestSuite_GameState::setup(%this)
{
    %this.addTestCase("TEST_GAMESTATE_BASICS");
    %this.addTestCase("TEST_GAMESTATEBITSET");
    %this.addTestCase("TEST_GAMESTATEBITSETTOOMANYTHINGS");
    %this.addTestCase("TEST_GAMESTATEBITSET_NON_THINGS");
    return ;
}
function TEST_GAMESTATE_BASICS::runTest(%this)
{
    if (!$StandAlone)
    {
        %this.assert(0, "this test must be run in $standalone");
        return ;
    }
    %player = $StandaloneServerPlayer;
    %stateName = "TEST_GAMESTATE_BASICS_STATE";
    %this.assert(gameplay::getState(%player, %stateName) == 0, "a nonexistent gamestate should default to zero");
    gameplay::setState(%player, %stateName, 1);
    %this.assert(gameplay::getState(%player, %stateName) == 1, "the state should be 1 right after we set it");
    %new = gameplay::getState(%player, %stateName) + 1;
    gameplay::setState(%player, %stateName, %new);
    %this.assert(gameplay::getState(%player, %stateName) == 2, "the state should be 2 right after we increment it");
    gameplay::ClearState(%player, %stateName);
    %this.assert(gameplay::getState(%player, %stateName) == 0, "the state should be zero again after we clear it");
    return ;
}
function TEST_GAMESTATEBITSET::runTest(%this)
{
    if (!$StandAlone)
    {
        %this.assert(0, "this test must be run in $standalone");
        return ;
    }
    %player = $StandaloneServerPlayer;
    %set = GameStateBitSet::construct("testBits_set");
    %this.assert(isObject(%set), "failed to construct GameStateBitSet");
    %set.AddThing("blarney");
    %set.AddThing("balleyhoo");
    %set.AddThing("gold");
    %count = %set.CountBits(%player);
    %this.assert(%count == 0, "should have counted no bits since we haven\'t set any yet");
    %set.SetBit(%player, "gold");
    %ret = %set.IsBitSet(%player, "blarney");
    %this.assert(%ret == 0, "a bit was set that should not be set");
    %ret = %set.IsBitSet(%player, "gold");
    %this.assert(%ret == 1, "a bit that should have been set was not set");
    %set.SetBit(%player, "blarney");
    %set.SetBit(%player, "balleyhoo");
    %count = %set.CountBits(%player);
    %this.assert(%count == 3, "should have counted 3 bits since we set them all");
    %set.ClearState(%player);
    %count = %set.CountBits(%player);
    %this.assert(%count == 0, "should have counted no bits since we cleared the state for this player");
    %set.delete();
    return ;
}
function TEST_GAMESTATEBITSETTOOMANYTHINGS::runTest(%this)
{
    if (!$StandAlone)
    {
        %this.assert(0, "this test must be run in $standalone");
        return ;
    }
    %player = $StandaloneServerPlayer;
    %set = GameStateBitSet::construct("testBits_set");
    %i = 0;
    while (%i < 32)
    {
        %ret = %set.AddThing(0 SPC %i);
        %this.assert(%ret == 1, "failed to add a thing to the set when it should have worked");
        %i = %i + 1;
    }
    %ret = %set.AddThing("TOO MANY!");
    %this.assert(%ret == 0, "we should not have been able to add this thing to the set, only 32 things shoudl work");
    %set.delete();
    return ;
}
function TEST_GAMESTATEBITSET_NON_THINGS::runTest(%this)
{
    if (!$StandAlone)
    {
        %this.assert(0, "this test must be run in $standalone");
        return ;
    }
    %player = $StandaloneServerPlayer;
    %set = GameStateBitSet::construct("testBits_set");
    %count = %set.CountBits(%player);
    %this.assert(%count == 0, "empty set should have empty bits");
    %ret = %set.SetBit(%player, "gold");
    %this.assert(%ret == 0, "should fail to set a bit for a thing that doesn\'t exist in this set");
    %ret = %set.IsBitSet(%player, "blarney");
    %this.assert(%ret == 0, "a bit should not be set for this set that has no things in it.");
    %set.ClearState(%player);
    %set.delete();
    return ;
}
