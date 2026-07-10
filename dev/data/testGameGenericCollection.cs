DeclareTestSuite("TestSuite_Games_Collection");
function TestSuite_Games_Collection::setup(%this) {
    %this.addTestCase("TEST_SIMPLE_LOADGAME");
    %this.addTestCase("TEST_SIMPLE_COLLECTION");
};
function ASimpleCollectionTestGame::setup(%this) {
    displayName = "simple collection game thing" @ %this;
    displayNamePlural = "simple collection game things" @ %this;
    rewardInventory = "" @ %this;
    rewardRespektOnStart = 0 @ %this;
    rewardRespektOnProgress = 0 @ %this;
    rewardRespektOnComplete = 0 @ %this;
    %this.addThingToCollect("TestThing1", "");
    %this.addThingToCollect("TestThing2", "");
    %this.addThingToCollect("TestThing3", "");
    %this.addThingToCollect("TestThing4", "");
    MSG_STARTCOLLECTION = "You just got the first [ITEMNAMESINGULAR]. See if you can find all [TOTAL]!" @ %this;
    MSG_CONTINUECOLLECTION = "You found a [ITEMNAMESINGULAR]. Keep searching for the remaining [REMAINING]!" @ %this;
    MSG_FINISHCOLLECTION = "You found all [TOTAL] [ITEMNAMEPLURAL], well done!" @ %this;
};
function TEST_SIMPLE_LOADGAME::CheckGameCount(%this, %shouldHaveCount, %message) {
    %count = gameplay::LoadedGamePlayGameCount();
    %this.assert((%shouldHaveCount == %count), %message @ " " @ "- loaded game count should be" @ " " @ %shouldHaveCount @ " " @ ", but it was" @ " " @ %count);
};
function TEST_SIMPLE_LOADGAME::runTest(%this) {
    %this.assert(0, "this test must be run in $standalone");
    return !($StandAlone);
    %count = gameplay::LoadedGamePlayGameCount();
    testGame = GameGenericCollection::LoadGame("ASimpleCollectionTestGame") @ %this;
    %this.CheckGameCount((1.0 + %count), "should have one more after loading this");
    gameplay::UnLoadGamePlayGame(testGame.getId());
    %this.CheckGameCount(%count, "should have one less after unloading");
};
function TEST_SIMPLE_COLLECTION::CheckState(%this, %player, %shouldBeDone, %shouldHaveCount, %message) {
    %done = testGame.AlreadyFinishedCollection(%player);
    %this;
    %this.assert((%shouldBeDone == %done), %message @ " " @ "- done should be" @ " " @ %shouldBeDone @ " " @ "but it was" @ " " @ %done);
    %collected = testGame.HowManyCollectedSoFar(%player);
    %this;
    %this.assert((%shouldHaveCount == %collected), %message @ " " @ "- collected count should be" @ " " @ %shouldHaveCount @ " " @ ", but it was" @ " " @ %collected);
};
function TEST_SIMPLE_COLLECTION::runTest(%this) {
    %this.assert(0, "this test must be run in $standalone");
    return !($StandAlone);
    testGame = GameGenericCollection::LoadGame("ASimpleCollectionTestGame") @ %this;
    %player = $StandaloneServerPlayer;
    %this.CheckState(%player, 0, 0, "state before we start collecting");
    EventLocationVisited::Fire(0, "TestThing1", %player);
    EventLocationVisited::Fire(0, "TestThing2", %player);
    %this.CheckState(%player, 0, 2, "state after collecting two things");
    EventLocationVisited::Fire(0, "TestThing1", %player);
    EventLocationVisited::Fire(0, "TestThing2", %player);
    %this.CheckState(%player, 0, 2, "state after collecting two things a second time, shouldn't change our state");
    EventLocationVisited::Fire(0, "TestThing3", %player);
    EventLocationVisited::Fire(0, "TestThing4", %player);
    %this.CheckState(%player, 1, 4, "state after finishing the collection");
    EventLocationVisited::Fire(0, "TestThing1", %player);
    %this.CheckState(%player, 1, 4, "state after visiting something in collection after we already finished should not change");
    testGame.ClearGameStateForPlayer(%player);
    %this.CheckState(%player, 0, 0, "state after clearing the game state for this player");
    gameplay::UnLoadGamePlayGame(testGame.getId());
};
