function doCSTestCreateRandomThingIOwn()
{
    if (!CustomSpaceClient::isOwner() && (CustomSpaceClient::GetSpaceImIn() $= ""))
    {
        echo("I\'m not in a private space, or I\'m in one that I don\'t own, not running the test");
        return ;
    }
    RunTestSuite("TestSuite_CSActive_CreateRandom");
    return ;
}
function doCSTestCreateEveryThingIOwn()
{
    if (!CustomSpaceClient::isOwner() && (CustomSpaceClient::GetSpaceImIn() $= ""))
    {
        echo("I\'m not in a private space, or I\'m in one that I don\'t own, not running the test");
        return ;
    }
    RunTestSuite("TestSuite_CSActive_CreateAllOwned");
    return ;
}
function doCSTestTryoutRandomThing()
{
    if (!CustomSpaceClient::isOwner() && (CustomSpaceClient::GetSpaceImIn() $= ""))
    {
        echo("I\'m not in a private space, or I\'m in one that I don\'t own, not running the test");
        return ;
    }
    RunTestSuite("TestSuite_CSActive_TryOut");
    return ;
}
DeclareTestSuite("TestSuite_CSActive_CreateRandom");
DeclareTestSuite("TestSuite_CSActive_TryOut");
DeclareTestSuite("TestSuite_CSActive_CreateAllOwned");
function TestSuite_CSActive_TryOut::setup(%this)
{
    %this.addTestCaseDelayed("TEST_CSActive_RequestToEdit", 1000);
    %this.addTestCaseDelayed("TEST_CS_TryOutRandomOwnedFurnitureItem", 1000);
    %this.addTestCaseDelayed("TEST_CSActive_DoneEditing", 1000);
    return ;
}
function TestSuite_CSActive_CreateRandom::setup(%this)
{
    %this.addTestCaseDelayed("TEST_CSActive_RequestToEdit", 1000);
    %this.addTestCaseDelayed("TEST_CS_CreateRandomOwnedFurnitureItem", 1000);
    %this.addTestCaseDelayed("TEST_CSActive_DoneEditing", 1000);
    return ;
}
function TestSuite_CSActive_CreateAllOwned::setup(%this)
{
    %this.addTestCaseDelayed("TEST_CSActive_RequestToEdit", 1000);
    %this.addTestCaseDelayed("TEST_CS_CreateAllOwnedFurnitureItems", 2000);
    %this.addTestCaseDelayed("TEST_CSActive_DoneEditing", 1000);
    return ;
}
function TEST_CSActive_RequestToEdit::runTest(%this)
{
    csRequestToEditSpace();
    return ;
}
function TEST_CSActive_RequestToEdit::delayedEval(%this)
{
    %this.assert($CSMaximumSlots > 0, "this space thinks we can\'t put any furniture items in it still");
    return ;
}
function TEST_CS_CreateRandomOwnedFurnitureItem::runTest(%this)
{
    %this.assert(!(CustomSpaceClient::GetSpaceImIn() $= ""), "We are not in a custom space, this test will not work");
    %this.assert(CustomSpaceClient::isOwner(), "We are not the owner of the space we are in, this test will not work");
    if (!CustomSpaceClient::isOwner() && (CustomSpaceClient::GetSpaceImIn() $= ""))
    {
        return ;
    }
    %this.ownedFurnitureToTestCount = 0;
    %count = $Player::furnitureInventory.count();
    %index = 0;
    while (%index < %count)
    {
        %sku = $Player::furnitureInventory.getKey(%index);
        %inUse = numUsingFurnitureSku(%sku);
        %numOwned = numOwnedFurnitureSku(%sku);
        if (%numOwned > %inUse)
        {
            %this.ownedFurnitureToTest[%this.ownedFurnitureToTestCount] = %sku;
            %this.ownedFurnitureToTestCount = %this.ownedFurnitureToTestCount + 1;
        }
        %index = %index + 1;
    }
    if (%this.ownedFurnitureToTestCount <= 0)
    {
        %this.assert(0, "we do not own any furniture that we can test with");
        return ;
    }
    %rand = getRandom(0, %this.ownedFurnitureToTestCount);
    %skuToTest = %this.ownedFurnitureToTest[%rand];
    %this.lastNumUsed = numUsingFurnitureSku(%skuToTest);
    %this.lastSkuTested = %skuToTest;
    %this.assert(%skuToTest > 0, "we got a bad sku for this");
    %alreadyHave = numUsingFurnitureAll();
    if (%alreadyHave >= $CSMaximumSlots)
    {
        %this.assert(0, "We are already using the max furniture we can place in this space: (" SPC %alreadyHave SPC "out of" SPC $CSMaximumSlots SPC ")");
        return ;
    }
    CustomSpaceClient::placeSkuInWorld(%skuToTest);
    return ;
}
function TEST_CS_CreateRandomOwnedFurnitureItem::delayedEval(%this)
{
    %numUsedNow = numUsingFurnitureSku(%this.lastSkuTested);
    %this.assert(%numUsedNow == (%this.lastNumUsed + 1), "We should be using one more sku that when we started but we are not. started with:" SPC %this.lastNumUsed SPC ", using now:" SPC %numUsedNow SPC ", note this could just be because we are checking too soon, and it hasn\'t filtered back to us yet, if envmanager is being slow");
    return ;
}
function TEST_CSActive_DoneEditing::runTest(%this)
{
    csDoneEditingSpace();
    return ;
}
function TEST_CSActive_DoneEditing::delayedEval(%this)
{
    return ;
}
function TEST_CS_TryOutRandomOwnedFurnitureItem::runTest(%this)
{
    %this.assert(!(CustomSpaceClient::GetSpaceImIn() $= ""), "We are not in a custom space, this test will not work");
    %this.assert(CustomSpaceClient::isOwner(), "We are not the owner of the space we are in, this test will not work");
    if (!CustomSpaceClient::isOwner() && (CustomSpaceClient::GetSpaceImIn() $= ""))
    {
        return ;
    }
    %this.UnOwnedFurnitureToTestCount = 0;
    %count = $Player::furnitureInventory.count();
    if (%count <= 0)
    {
        %this.assert(0, "we did not find any furniture we can test!");
        return ;
    }
    %rand = getRandom(0, %count);
    %skuToTest = $Player::furnitureInventory.getKey(%rand);
    %this.assert(%skuToTest > 0, "we got a bad sku for this");
    commandToServer('CreateInventoryBySkuJustTestingItOut', CustomSpaceClient::GetSpaceImIn(), %skuToTest);
    return ;
}
function TEST_CS_TryOutRandomOwnedFurnitureItem::delayedEval(%this)
{
    return ;
}
function TEST_CS_CreateAllOwnedFurnitureItems::runTest(%this)
{
    %this.assert(!(CustomSpaceClient::GetSpaceImIn() $= ""), "We are not in a custom space, this test will not work");
    %this.assert(CustomSpaceClient::isOwner(), "We are not the owner of the space we are in, this test will not work");
    if (!CustomSpaceClient::isOwner() && (CustomSpaceClient::GetSpaceImIn() $= ""))
    {
        return ;
    }
    %this.ownedFurnitureToTestCount = 0;
    %count = $Player::furnitureInventory.count();
    %index = 0;
    while (%index < %count)
    {
        %sku = $Player::furnitureInventory.getKey(%index);
        %inUse = numUsingFurnitureSku(%sku);
        %numOwned = numOwnedFurnitureSku(%sku);
        if (%numOwned > %inUse)
        {
            %this.ownedFurnitureToTest[%this.ownedFurnitureToTestCount] = %sku;
            %this.ownedFurnitureToTestCount = %this.ownedFurnitureToTestCount + 1;
        }
        %index = %index + 1;
    }
    if (%this.ownedFurnitureToTestCount <= 0)
    {
        echo("we either don\'t own any furniture or hav eplaced it all, not making any new stuff");
        return ;
    }
    %i = 0;
    while (%i < %this.ownedFurnitureToTestCount)
    {
        %sku = %this.ownedFurnitureToTest[%i];
        %inUse = numUsingFurnitureSku(%sku);
        %numOwned = numOwnedFurnitureSku(%sku);
        %numToMake = %numOwned - %inUse;
        %j = 0;
        while (%j < %numToMake)
        {
            %alreadyHave = numUsingFurnitureAll();
            if (%alreadyHave >= $CSMaximumSlots)
            {
                %this.assert(0, "We are already using the max furniture we can place in this space: (" SPC %alreadyHave SPC "out of" SPC $CSMaximumSlots SPC ")");
                return ;
            }
            CustomSpaceClient::placeSkuInWorld(%sku);
            %j = %j + 1;
        }
        %i = %i + 1;
    }
}


