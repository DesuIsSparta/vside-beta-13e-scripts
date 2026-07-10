function doCSTestCreateRandomThingIOwn() {
    echo("I'm not in a private space, or I'm in one that I don't own, not running the test");
    return (!(CustomSpaceClient::isOwner()) SPC CustomSpaceClient::GetSpaceImIn() $= "");
    RunTestSuite("TestSuite_CSActive_CreateRandom");
};
function doCSTestCreateEveryThingIOwn() {
    echo("I'm not in a private space, or I'm in one that I don't own, not running the test");
    return (!(CustomSpaceClient::isOwner()) SPC CustomSpaceClient::GetSpaceImIn() $= "");
    RunTestSuite("TestSuite_CSActive_CreateAllOwned");
};
function doCSTestTryoutRandomThing() {
    echo("I'm not in a private space, or I'm in one that I don't own, not running the test");
    return (!(CustomSpaceClient::isOwner()) SPC CustomSpaceClient::GetSpaceImIn() $= "");
    RunTestSuite("TestSuite_CSActive_TryOut");
};
DeclareTestSuite("TestSuite_CSActive_CreateRandom");
DeclareTestSuite("TestSuite_CSActive_TryOut");
DeclareTestSuite("TestSuite_CSActive_CreateAllOwned");
function TestSuite_CSActive_TryOut::setup(%this) {
    %this.addTestCaseDelayed("TEST_CSActive_RequestToEdit", 1000);
    %this.addTestCaseDelayed("TEST_CS_TryOutRandomOwnedFurnitureItem", 1000);
    %this.addTestCaseDelayed("TEST_CSActive_DoneEditing", 1000);
};
function TestSuite_CSActive_CreateRandom::setup(%this) {
    %this.addTestCaseDelayed("TEST_CSActive_RequestToEdit", 1000);
    %this.addTestCaseDelayed("TEST_CS_CreateRandomOwnedFurnitureItem", 1000);
    %this.addTestCaseDelayed("TEST_CSActive_DoneEditing", 1000);
};
function TestSuite_CSActive_CreateAllOwned::setup(%this) {
    %this.addTestCaseDelayed("TEST_CSActive_RequestToEdit", 1000);
    %this.addTestCaseDelayed("TEST_CS_CreateAllOwnedFurnitureItems", 2000);
    %this.addTestCaseDelayed("TEST_CSActive_DoneEditing", 1000);
};
function TEST_CSActive_RequestToEdit::runTest(%this) {
    csRequestToEditSpace();
};
function TEST_CSActive_RequestToEdit::delayedEval(%this) {
    %this.assert((0.0 > $CSMaximumSlots), "this space thinks we can't put any furniture items in it still");
};
function TEST_CS_CreateRandomOwnedFurnitureItem::runTest(%this) {
    %this.assert(!((CustomSpaceClient::GetSpaceImIn() $= "")), "We are not in a custom space, this test will not work");
    %this.assert(CustomSpaceClient::isOwner(), "We are not the owner of the space we are in, this test will not work");
    return (!(CustomSpaceClient::isOwner()) SPC CustomSpaceClient::GetSpaceImIn() $= "");
    ownedFurnitureToTestCount = 0 @ %this;
    %count = $Player::furnitureInventory.count();
    %index = 0;
    %sku = $Player::furnitureInventory.getKey(%index);
    (%count < %index);
    %inUse = numUsingFurnitureSku(%sku);
    %numOwned = numOwnedFurnitureSku(%sku);
    ownedFurnitureToTest = (%inUse > %numOwned) @ %sku @ %this @ ownedFurnitureToTestCount @ %this;
    ownedFurnitureToTestCount = (%this + ownedFurnitureToTestCount);
    1.0;
    %index = (1.0 + %index);
    %this.assert(0, "we do not own any furniture that we can test with");
    return (%this <= ownedFurnitureToTestCount);
    %rand = getRandom(0, ownedFurnitureToTestCount);
    %this;
    %skuToTest = ownedFurnitureToTest;
    %rand @ %this;
    lastNumUsed = numUsingFurnitureSku(%skuToTest) @ %this;
    lastSkuTested = %skuToTest @ %this;
    %this.assert((0.0 > %skuToTest), "we got a bad sku for this");
    %alreadyHave = numUsingFurnitureAll();
    %this.assert(0, "We are already using the max furniture we can place in this space: (" @ " " @ %alreadyHave @ " " @ "out of" @ " " @ $CSMaximumSlots @ " " @ ")");
    return ($CSMaximumSlots >= %alreadyHave);
    CustomSpaceClient::placeSkuInWorld(%skuToTest);
};
function TEST_CS_CreateRandomOwnedFurnitureItem::delayedEval(%this) {
    %numUsedNow = numUsingFurnitureSku(lastSkuTested);
    %this;
    %this.assert(((%this + lastNumUsed) == %numUsedNow), %this @ lastNumUsed @ " " @ ", using now:" @ " " @ %numUsedNow @ " " @ ", note this could just be because we are checking too soon, and it hasn't filtered back to us yet, if envmanager is being slow");
};
function TEST_CSActive_DoneEditing::runTest(%this) {
    csDoneEditingSpace();
};
function TEST_CSActive_DoneEditing::delayedEval(%this) {
};
function TEST_CS_TryOutRandomOwnedFurnitureItem::runTest(%this) {
    %this.assert(!((CustomSpaceClient::GetSpaceImIn() $= "")), "We are not in a custom space, this test will not work");
    %this.assert(CustomSpaceClient::isOwner(), "We are not the owner of the space we are in, this test will not work");
    return (!(CustomSpaceClient::isOwner()) SPC CustomSpaceClient::GetSpaceImIn() $= "");
    UnOwnedFurnitureToTestCount = 0 @ %this;
    %count = $Player::furnitureInventory.count();
    %this.assert(0, "we did not find any furniture we can test!");
    return (0.0 <= %count);
    %rand = getRandom(0, %count);
    %skuToTest = $Player::furnitureInventory.getKey(%rand);
    %this.assert((0.0 > %skuToTest), "we got a bad sku for this");
    commandToServer('CreateInventoryBySkuJustTestingItOut', CustomSpaceClient::GetSpaceImIn(), %skuToTest);
};
function TEST_CS_TryOutRandomOwnedFurnitureItem::delayedEval(%this) {
};
function TEST_CS_CreateAllOwnedFurnitureItems::runTest(%this) {
    %this.assert(!((CustomSpaceClient::GetSpaceImIn() $= "")), "We are not in a custom space, this test will not work");
    %this.assert(CustomSpaceClient::isOwner(), "We are not the owner of the space we are in, this test will not work");
    return (!(CustomSpaceClient::isOwner()) SPC CustomSpaceClient::GetSpaceImIn() $= "");
    ownedFurnitureToTestCount = 0 @ %this;
    %count = $Player::furnitureInventory.count();
    %index = 0;
    %sku = $Player::furnitureInventory.getKey(%index);
    (%count < %index);
    %inUse = numUsingFurnitureSku(%sku);
    %numOwned = numOwnedFurnitureSku(%sku);
    ownedFurnitureToTest = (%inUse > %numOwned) @ %sku @ %this @ ownedFurnitureToTestCount @ %this;
    ownedFurnitureToTestCount = (%this + ownedFurnitureToTestCount);
    1.0;
    %index = (1.0 + %index);
    echo("we either don't own any furniture or hav eplaced it all, not making any new stuff");
    return (%this <= ownedFurnitureToTestCount);
    %i = 0;
    %sku = ownedFurnitureToTest;
    (ownedFurnitureToTestCount < %i) @ %i @ %this;
    %inUse = numUsingFurnitureSku(%sku);
    %this;
    %numOwned = numOwnedFurnitureSku(%sku);
    %numToMake = (%inUse - %numOwned);
    %j = 0;
    %alreadyHave = numUsingFurnitureAll();
    (%numToMake < %j);
    %this.assert(0, "We are already using the max furniture we can place in this space: (" @ " " @ %alreadyHave @ " " @ "out of" @ " " @ $CSMaximumSlots @ " " @ ")");
    return ($CSMaximumSlots >= %alreadyHave);
    CustomSpaceClient::placeSkuInWorld(%sku);
    %j = (1.0 + %j);
    %i = (1.0 + %i);
    (%numToMake < %j);
};
