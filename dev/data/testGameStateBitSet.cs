DeclareTestSuite("TestSuite_GameState");
function TestSuite_GameState::setup(%this) {
    "TEST_GAMESTATE_BASICS".addTestCase(%this);
    "TEST_GAMESTATEBITSET".addTestCase(%this);
    "TEST_GAMESTATEBITSETTOOMANYTHINGS".addTestCase(%this);
    "TEST_GAMESTATEBITSET_NON_THINGS".addTestCase(%this);
};
function TEST_GAMESTATE_BASICS::runTest(%this) {
    if (!($StandAlone)) {
        "this test must be run in $standalone".assert(%this, 0);
        return;
    }
    %player = $StandaloneServerPlayer;
    %stateName = "TEST_GAMESTATE_BASICS_STATE";
    "a nonexistent gamestate should default to zero".assert(%this, (gameplay::getState(%player, %stateName) == 0.0));
    gameplay::setState(%player, %stateName, 1);
    "the state should be 1 right after we set it".assert(%this, (gameplay::getState(%player, %stateName) == 1.0));
    %new = (gameplay::getState(%player, %stateName) + 1.0);
    gameplay::setState(%player, %stateName, %new);
    "the state should be 2 right after we increment it".assert(%this, (gameplay::getState(%player, %stateName) == 2.0));
    gameplay::ClearState(%player, %stateName);
    "the state should be zero again after we clear it".assert(%this, (gameplay::getState(%player, %stateName) == 0.0));
};
function TEST_GAMESTATEBITSET::runTest(%this) {
    if (!($StandAlone)) {
        "this test must be run in $standalone".assert(%this, 0);
        return;
    }
    %player = $StandaloneServerPlayer;
    %set = GameStateBitSet::construct("testBits_set");
    "failed to construct GameStateBitSet".assert(%this, isObject(%set));
    "blarney".AddThing(%set);
    "balleyhoo".AddThing(%set);
    "gold".AddThing(%set);
    %count = %player.CountBits(%set);
    "should have counted no bits since we haven't set any yet".assert(%this, (%count == 0.0));
    "gold".SetBit(%set, %player);
    %ret = "blarney".IsBitSet(%set, %player);
    "a bit was set that should not be set".assert(%this, (%ret == 0.0));
    %ret = "gold".IsBitSet(%set, %player);
    "a bit that should have been set was not set".assert(%this, (%ret == 1.0));
    "blarney".SetBit(%set, %player);
    "balleyhoo".SetBit(%set, %player);
    %count = %player.CountBits(%set);
    "should have counted 3 bits since we set them all".assert(%this, (%count == 3.0));
    %player.ClearState(%set);
    %count = %player.CountBits(%set);
    "should have counted no bits since we cleared the state for this player".assert(%this, (%count == 0.0));
    %set.delete();
};
function TEST_GAMESTATEBITSETTOOMANYTHINGS::runTest(%this) {
    if (!($StandAlone)) {
        "this test must be run in $standalone".assert(%this, 0);
        return;
    }
    %player = $StandaloneServerPlayer;
    %set = GameStateBitSet::construct("testBits_set");
    %i = 0;
    while ((%i < 32.0)) {
        %ret = 0 @ " " @ %i.AddThing(%set);
        "failed to add a thing to the set when it should have worked".assert(%this, (%ret == 1.0));
        %i = (%i + 1.0);
    }
    %ret = "TOO MANY!".AddThing(%set);
    (%i < 32.0);
    "we should not have been able to add this thing to the set, only 32 things shoudl work".assert(%this, (%ret == 0.0));
    %set.delete();
};
function TEST_GAMESTATEBITSET_NON_THINGS::runTest(%this) {
    if (!($StandAlone)) {
        "this test must be run in $standalone".assert(%this, 0);
        return;
    }
    %player = $StandaloneServerPlayer;
    %set = GameStateBitSet::construct("testBits_set");
    %count = %player.CountBits(%set);
    "empty set should have empty bits".assert(%this, (%count == 0.0));
    %ret = "gold".SetBit(%set, %player);
    "should fail to set a bit for a thing that doesn't exist in this set".assert(%this, (%ret == 0.0));
    %ret = "blarney".IsBitSet(%set, %player);
    "a bit should not be set for this set that has no things in it.".assert(%this, (%ret == 0.0));
    %player.ClearState(%set);
    %set.delete();
};
