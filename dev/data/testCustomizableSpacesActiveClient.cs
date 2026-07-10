function doCSTestCreateRandomThingIOwn() {
    if (!(CustomSpaceClient::isOwner())) {
    }
    if ((CustomSpaceClient::GetSpaceImIn() $= "")) {
        echo("I'm not in a private space, or I'm in one that I don't own, not running the test");
        return;
    }
    RunTestSuite("TestSuite_CSActive_CreateRandom");
};
function doCSTestCreateEveryThingIOwn() {
    if (!(CustomSpaceClient::isOwner())) {
    }
    if ((CustomSpaceClient::GetSpaceImIn() $= "")) {
        echo("I'm not in a private space, or I'm in one that I don't own, not running the test");
        return;
    }
    RunTestSuite("TestSuite_CSActive_CreateAllOwned");
};
function doCSTestTryoutRandomThing() {
    if (!(CustomSpaceClient::isOwner())) {
    }
    if ((CustomSpaceClient::GetSpaceImIn() $= "")) {
        echo("I'm not in a private space, or I'm in one that I don't own, not running the test");
        return;
    }
    RunTestSuite("TestSuite_CSActive_TryOut");
};
DeclareTestSuite("TestSuite_CSActive_CreateRandom");
DeclareTestSuite("TestSuite_CSActive_TryOut");
DeclareTestSuite("TestSuite_CSActive_CreateAllOwned");
function TestSuite_CSActive_TryOut::setup(%this) {
    1000.addTestCaseDelayed(%this, "TEST_CSActive_RequestToEdit");
    1000.addTestCaseDelayed(%this, "TEST_CS_TryOutRandomOwnedFurnitureItem");
    1000.addTestCaseDelayed(%this, "TEST_CSActive_DoneEditing");
};
function TestSuite_CSActive_CreateRandom::setup(%this) {
    1000.addTestCaseDelayed(%this, "TEST_CSActive_RequestToEdit");
    1000.addTestCaseDelayed(%this, "TEST_CS_CreateRandomOwnedFurnitureItem");
    1000.addTestCaseDelayed(%this, "TEST_CSActive_DoneEditing");
};
function TestSuite_CSActive_CreateAllOwned::setup(%this) {
    1000.addTestCaseDelayed(%this, "TEST_CSActive_RequestToEdit");
    2000.addTestCaseDelayed(%this, "TEST_CS_CreateAllOwnedFurnitureItems");
    1000.addTestCaseDelayed(%this, "TEST_CSActive_DoneEditing");
};
function TEST_CSActive_RequestToEdit::runTest(%this) {
    csRequestToEditSpace();
};
function TEST_CSActive_RequestToEdit::delayedEval(%this) {
    "this space thinks we can't put any furniture items in it still".assert(%this, ($CSMaximumSlots > 0.0));
};
function TEST_CS_CreateRandomOwnedFurnitureItem::runTest(%this) {
    "We are not in a custom space, this test will not work".assert(%this, !(CustomSpaceClient::GetSpaceImIn() $= ""));
    "We are not the owner of the space we are in, this test will not work".assert(%this, CustomSpaceClient::isOwner());
    if (!(CustomSpaceClient::isOwner())) {
    }
    if ((CustomSpaceClient::GetSpaceImIn() $= "")) {
        return;
    }
    %this.ownedFurnitureToTestCount = 0;
    %count = $Player::furnitureInventory.count();
    %index = 0;
    while ((%index < %count)) {
        %sku = %index.getKey($Player::furnitureInventory);
        %inUse = numUsingFurnitureSku(%sku);
        %numOwned = numOwnedFurnitureSku(%sku);
        if ((%numOwned > %inUse)) {
            %this.ownedFurnitureToTest = %sku @ %this.ownedFurnitureToTestCount;
            %this.ownedFurnitureToTestCount = (%this.ownedFurnitureToTestCount + 1.0);
        }
        %index = (%index + 1.0);
    }
    if ((%this.ownedFurnitureToTestCount <= 0.0)) {
        "we do not own any furniture that we can test with".assert(%this, 0);
        return (%index < %count);
    }
    %rand = getRandom(0, %this.ownedFurnitureToTestCount);
    %skuToTest = %this.ownedFurnitureToTest;
    %rand;
    %this.lastNumUsed = numUsingFurnitureSku(%skuToTest);
    %this.lastSkuTested = %skuToTest;
    "we got a bad sku for this".assert(%this, (%skuToTest > 0.0));
    %alreadyHave = numUsingFurnitureAll();
    if ((%alreadyHave >= $CSMaximumSlots)) {
        "We are already using the max furniture we can place in this space: (" @ " " @ %alreadyHave @ " " @ "out of" @ " " @ $CSMaximumSlots @ " " @ ")".assert(%this, 0);
        return;
    }
    CustomSpaceClient::placeSkuInWorld(%skuToTest);
};
function TEST_CS_CreateRandomOwnedFurnitureItem::delayedEval(%this) {
    %numUsedNow = numUsingFurnitureSku(%this.lastSkuTested);
    "We should be using one more sku that when we started but we are not. started with:" @ " " @ %this.lastNumUsed @ " " @ ", using now:" @ " " @ %numUsedNow @ " " @ ", note this could just be because we are checking too soon, and it hasn't filtered back to us yet, if envmanager is being slow".assert(%this, (%numUsedNow == (%this.lastNumUsed + 1.0)));
};
function TEST_CSActive_DoneEditing::runTest(%this) {
    csDoneEditingSpace();
};
function TEST_CSActive_DoneEditing::delayedEval(%this) {
};
function TEST_CS_TryOutRandomOwnedFurnitureItem::runTest(%this) {
    "We are not in a custom space, this test will not work".assert(%this, !(CustomSpaceClient::GetSpaceImIn() $= ""));
    "We are not the owner of the space we are in, this test will not work".assert(%this, CustomSpaceClient::isOwner());
    if (!(CustomSpaceClient::isOwner())) {
    }
    if ((CustomSpaceClient::GetSpaceImIn() $= "")) {
        return;
    }
    %this.UnOwnedFurnitureToTestCount = 0;
    %count = $Player::furnitureInventory.count();
    if ((%count <= 0.0)) {
        "we did not find any furniture we can test!".assert(%this, 0);
        return;
    }
    %rand = getRandom(0, %count);
    %skuToTest = %rand.getKey($Player::furnitureInventory);
    "we got a bad sku for this".assert(%this, (%skuToTest > 0.0));
    commandToServer('CreateInventoryBySkuJustTestingItOut', CustomSpaceClient::GetSpaceImIn(), %skuToTest);
};
function TEST_CS_TryOutRandomOwnedFurnitureItem::delayedEval(%this) {
};
function TEST_CS_CreateAllOwnedFurnitureItems::runTest(%this) {
    "We are not in a custom space, this test will not work".assert(%this, !(CustomSpaceClient::GetSpaceImIn() $= ""));
    "We are not the owner of the space we are in, this test will not work".assert(%this, CustomSpaceClient::isOwner());
    if (!(CustomSpaceClient::isOwner())) {
    }
    if ((CustomSpaceClient::GetSpaceImIn() $= "")) {
        return;
    }
    %this.ownedFurnitureToTestCount = 0;
    %count = $Player::furnitureInventory.count();
    %index = 0;
    while ((%index < %count)) {
        %sku = %index.getKey($Player::furnitureInventory);
        %inUse = numUsingFurnitureSku(%sku);
        %numOwned = numOwnedFurnitureSku(%sku);
        if ((%numOwned > %inUse)) {
            %this.ownedFurnitureToTest = %sku @ %this.ownedFurnitureToTestCount;
            %this.ownedFurnitureToTestCount = (%this.ownedFurnitureToTestCount + 1.0);
        }
        %index = (%index + 1.0);
    }
    if ((%this.ownedFurnitureToTestCount <= 0.0)) {
        echo("we either don't own any furniture or hav eplaced it all, not making any new stuff");
        return (%index < %count);
    }
    %i = 0;
    while ((%i < %this.ownedFurnitureToTestCount)) {
        %sku = %this.ownedFurnitureToTest;
        %i;
        %inUse = numUsingFurnitureSku(%sku);
        %numOwned = numOwnedFurnitureSku(%sku);
        %numToMake = (%numOwned - %inUse);
        %j = 0;
        while ((%j < %numToMake)) {
            %alreadyHave = numUsingFurnitureAll();
            if ((%alreadyHave >= $CSMaximumSlots)) {
                "We are already using the max furniture we can place in this space: (" @ " " @ %alreadyHave @ " " @ "out of" @ " " @ $CSMaximumSlots @ " " @ ")".assert(%this, 0);
                return;
            }
            CustomSpaceClient::placeSkuInWorld(%sku);
            %j = (%j + 1.0);
        }
        %i = (%i + 1.0);
        (%j < %numToMake);
    }
};
