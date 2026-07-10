DeclareTestSuite("TestSuite_Games_Collection");
function TestSuite_Games_Collection::setup(%this) {
    "TEST_SIMPLE_LOADGAME".addTestCase(%this);
    "TEST_SIMPLE_COLLECTION".addTestCase(%this);
};
function ASimpleCollectionTestGame::setup(%this) {
    %this.displayName = "simple collection game thing";
    %this.displayNamePlural = "simple collection game things";
    %this.rewardInventory = "";
    %this.rewardRespektOnStart = 0;
    %this.rewardRespektOnProgress = 0;
    %this.rewardRespektOnComplete = 0;
    "".addThingToCollect(%this, "TestThing1");
    "".addThingToCollect(%this, "TestThing2");
    "".addThingToCollect(%this, "TestThing3");
    "".addThingToCollect(%this, "TestThing4");
    %this.MSG_STARTCOLLECTION = "You just got the first [ITEMNAMESINGULAR]. See if you can find all [TOTAL]!";
    %this.MSG_CONTINUECOLLECTION = "You found a [ITEMNAMESINGULAR]. Keep searching for the remaining [REMAINING]!";
    %this.MSG_FINISHCOLLECTION = "You found all [TOTAL] [ITEMNAMEPLURAL], well done!";
};
function TEST_SIMPLE_LOADGAME::CheckGameCount(%this, %shouldHaveCount, %message) {
    %count = gameplay::LoadedGamePlayGameCount();
    %message @ " " @ "- loaded game count should be" @ " " @ %shouldHaveCount @ " " @ ", but it was" @ " " @ %count.assert(%this, (%count == %shouldHaveCount));
};
function TEST_SIMPLE_LOADGAME::runTest(%this) {
    if (!($StandAlone)) {
        "this test must be run in $standalone".assert(%this, 0);
        return;
    }
    %count = gameplay::LoadedGamePlayGameCount();
    %this.testGame = GameGenericCollection::LoadGame("ASimpleCollectionTestGame");
    "should have one more after loading this".CheckGameCount(%this, (%count + 1.0));
    gameplay::UnLoadGamePlayGame(%this.testGame.getId());
    "should have one less after unloading".CheckGameCount(%this, %count);
};
function TEST_SIMPLE_COLLECTION::CheckState(%this, %player, %shouldBeDone, %shouldHaveCount, %message) {
    %done = %player.AlreadyFinishedCollection(%this.testGame);
    %message @ " " @ "- done should be" @ " " @ %shouldBeDone @ " " @ "but it was" @ " " @ %done.assert(%this, (%done == %shouldBeDone));
    %collected = %player.HowManyCollectedSoFar(%this.testGame);
    %message @ " " @ "- collected count should be" @ " " @ %shouldHaveCount @ " " @ ", but it was" @ " " @ %collected.assert(%this, (%collected == %shouldHaveCount));
};
function TEST_SIMPLE_COLLECTION::runTest(%this) {
    if (!($StandAlone)) {
        "this test must be run in $standalone".assert(%this, 0);
        return;
    }
    %this.testGame = GameGenericCollection::LoadGame("ASimpleCollectionTestGame");
    %player = $StandaloneServerPlayer;
    "state before we start collecting".CheckState(%this, %player, 0, 0);
    EventLocationVisited::Fire(0, "TestThing1", %player);
    EventLocationVisited::Fire(0, "TestThing2", %player);
    "state after collecting two things".CheckState(%this, %player, 0, 2);
    EventLocationVisited::Fire(0, "TestThing1", %player);
    EventLocationVisited::Fire(0, "TestThing2", %player);
    "state after collecting two things a second time, shouldn't change our state".CheckState(%this, %player, 0, 2);
    EventLocationVisited::Fire(0, "TestThing3", %player);
    EventLocationVisited::Fire(0, "TestThing4", %player);
    "state after finishing the collection".CheckState(%this, %player, 1, 4);
    EventLocationVisited::Fire(0, "TestThing1", %player);
    "state after visiting something in collection after we already finished should not change".CheckState(%this, %player, 1, 4);
    %player.ClearGameStateForPlayer(%this.testGame);
    "state after clearing the game state for this player".CheckState(%this, %player, 0, 0);
    gameplay::UnLoadGamePlayGame(%this.testGame.getId());
};
