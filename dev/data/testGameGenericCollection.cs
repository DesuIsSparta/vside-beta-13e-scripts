DeclareTestSuite("TestSuite_Games_Collection");
function TestSuite_Games_Collection::setup(%this)
{
    %this.addTestCase("TEST_SIMPLE_LOADGAME");
    %this.addTestCase("TEST_SIMPLE_COLLECTION");
    return ;
}
function ASimpleCollectionTestGame::setup(%this)
{
    %this.displayName = "simple collection game thing";
    %this.displayNamePlural = "simple collection game things";
    %this.rewardInventory = "";
    %this.rewardRespektOnStart = 0;
    %this.rewardRespektOnProgress = 0;
    %this.rewardRespektOnComplete = 0;
    %this.addThingToCollect("TestThing1", "");
    %this.addThingToCollect("TestThing2", "");
    %this.addThingToCollect("TestThing3", "");
    %this.addThingToCollect("TestThing4", "");
    %this.MSG_STARTCOLLECTION = "You just got the first [ITEMNAMESINGULAR]. See if you can find all [TOTAL]!";
    %this.MSG_CONTINUECOLLECTION = "You found a [ITEMNAMESINGULAR]. Keep searching for the remaining [REMAINING]!";
    %this.MSG_FINISHCOLLECTION = "You found all [TOTAL] [ITEMNAMEPLURAL], well done!";
    return ;
}
function TEST_SIMPLE_LOADGAME::CheckGameCount(%this, %shouldHaveCount, %message)
{
    %count = gameplay::LoadedGamePlayGameCount();
    %this.assert(%count == %shouldHaveCount, %message SPC "- loaded game count should be" SPC %shouldHaveCount SPC ", but it was" SPC %count);
    return ;
}
function TEST_SIMPLE_LOADGAME::runTest(%this)
{
    if (!$StandAlone)
    {
        %this.assert(0, "this test must be run in $standalone");
        return ;
    }
    %count = gameplay::LoadedGamePlayGameCount();
    %this.testGame = GameGenericCollection::LoadGame("ASimpleCollectionTestGame");
    %this.CheckGameCount(%count + 1, "should have one more after loading this");
    gameplay::UnLoadGamePlayGame(%this.testGame.getId());
    %this.CheckGameCount(%count, "should have one less after unloading");
    return ;
}
function TEST_SIMPLE_COLLECTION::CheckState(%this, %player, %shouldBeDone, %shouldHaveCount, %message)
{
    %done = %this.testGame.AlreadyFinishedCollection(%player);
    %this.assert(%done == %shouldBeDone, %message SPC "- done should be" SPC %shouldBeDone SPC "but it was" SPC %done);
    %collected = %this.testGame.HowManyCollectedSoFar(%player);
    %this.assert(%collected == %shouldHaveCount, %message SPC "- collected count should be" SPC %shouldHaveCount SPC ", but it was" SPC %collected);
    return ;
}
function TEST_SIMPLE_COLLECTION::runTest(%this)
{
    if (!$StandAlone)
    {
        %this.assert(0, "this test must be run in $standalone");
        return ;
    }
    %this.testGame = GameGenericCollection::LoadGame("ASimpleCollectionTestGame");
    %player = $StandaloneServerPlayer;
    %this.CheckState(%player, 0, 0, "state before we start collecting");
    EventLocationVisited::Fire(0, "TestThing1", %player);
    EventLocationVisited::Fire(0, "TestThing2", %player);
    %this.CheckState(%player, 0, 2, "state after collecting two things");
    EventLocationVisited::Fire(0, "TestThing1", %player);
    EventLocationVisited::Fire(0, "TestThing2", %player);
    %this.CheckState(%player, 0, 2, "state after collecting two things a second time, shouldn\'t change our state");
    EventLocationVisited::Fire(0, "TestThing3", %player);
    EventLocationVisited::Fire(0, "TestThing4", %player);
    %this.CheckState(%player, 1, 4, "state after finishing the collection");
    EventLocationVisited::Fire(0, "TestThing1", %player);
    %this.CheckState(%player, 1, 4, "state after visiting something in collection after we already finished should not change");
    %this.testGame.ClearGameStateForPlayer(%player);
    %this.CheckState(%player, 0, 0, "state after clearing the game state for this player");
    gameplay::UnLoadGamePlayGame(%this.testGame.getId());
    return ;
}
