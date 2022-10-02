function doCSSuiteTest()
{
    RunTestSuite("TestSuite_CSSmokeTests");
    return ;
}
DeclareTestSuite("TestSuite_CSSmokeTests");
function MockTriggerArea::getNumObjects(%this)
{
    return 0;
}
function MockTriggerArea::getObject(%this, %unused)
{
    return 0;
}
function MockInterior::getActiveSkuPairs(%this)
{
    return %this.activeSkuPairs;
}
function MockInterior::setActiveSkuPairs(%this, %pairs)
{
    %this.activeSkuPairs = %pairs;
    return ;
}
function TestSuite_CSSmokeTests::setup(%this)
{
    if (isObject(TestCS_MockTriggerObj))
    {
        TestCS_MockTriggerObj.delete();
    }
    if (isObject(TestCS_MockInteriorObj))
    {
        TestCS_MockInteriorObj.delete();
    }
    %mockInteriorobj = new ScriptObject(TestCS_MockInteriorObj)
    {
        class = "MockInterior";
        activeSkuPairs = "10 20";
    };
    RootGroup.add(%mockInteriorobj);
    %mockTrigger = new ScriptObject(TestCS_MockTriggerObj)
    {
        class = "MockTriggerArea";
        interior = %mockInteriorobj;
    };
    RootGroup.add(%mockTrigger);
    %this.addTestCase("TEST_CS_SpaceCreate");
    %this.addTestCase("TEST_CS_NuggetCreate");
    %this.addTestCase("TEST_CS_NuggetCreateForEdit");
    %this.addTestCase("TEST_CS_CreationThroughSpace");
    %this.addTestCase("TEST_CS_CreationThroughSpaceAndManipulate");
    %this.addTestCase("TEST_CS_SpaceMultiSkuAndLimits");
    %this.addTestCase("TEST_CS_SpaceDelete");
    %this.addTestCase("TEST_CS_SpaceDeleteUnKnownRef");
    %this.addTestCase("TEST_CS_SpaceSaveToStringMapAndLoad");
    %this.addTestCase("TEST_CS_SpaceListIDs");
    %this.addTestCase("TEST_CS_SpaceManager");
    %this.addTestCase("TEST_CS_EditPermissions");
    %this.addTestCase("TEST_CS_ConstrainMovementToSpace");
    %this.addTestCase("TEST_CS_SpaceSaveToStringMapAndLoadBasedOnOffsets");
    %this.addTestCase("TEST_CS_SpaceLoadConfigurationsFromStringMap");
    %this.addTestCase("TEST_CS_SpaceSaveToStringWithTestingObject");
    %this.addTestCase("TEST_CS_SpaceDeleteUnownedObjects");
    return ;
}
function TestSuite_CSSmokeTests::TearDown(%this)
{
    if (isObject(TestCS_MockTriggerObj))
    {
        TestCS_MockTriggerObj.delete();
    }
    if (isObject(TestCS_MockInteriorObj))
    {
        TestCS_MockInteriorObj.delete();
    }
    return ;
}
function TEST_CS_SpaceCreate::runTest(%this)
{
    %maxItems = 1000;
    %name = "Bobs Home";
    %thespace = CustomizableSpace::construct(TestCS_MockTriggerObj, %name, %maxItems);
    %this.assert(isObject(%thespace), "CustomizableSpace::Construct failed");
    %this.assert(isObject(%thespace.nuggets), "CustomizableSpace has bad nuggets");
    %this.assert(isObject(%thespace.myTriggerObject), "CustomizableSpace has bad triggerObject");
    %this.assert(%thespace.maxItems == %maxItems, "CustomizableSpace has incorrect %maxItems");
    %this.assertSameString(%thespace.spaceName, %name, "created position is not correct");
    %thespace.delete();
    %this.assert(!isObject(%thespace.nuggets), "CustomizableSpace still has nuggets after being deleted");
    return ;
}
function TEST_CS_NuggetCreate::runTest(%this)
{
    %guid = 1;
    %sku = 41000;
    %theNugget = InventoryNugget::construct(%sku, 1, "0 0 0", "0 0 0", 0, %guid, "");
    %this.assert(isObject(%theNugget), "InventoryNugget::Construct failed");
    %realGroup = %theNugget.objectGroup;
    %this.assert(isObject(%realGroup), "InventoryNugget has bad objectGroup");
    %this.assert(!isObject("CUSTOMIZABLE_GROUP"), "CUSTOMIZABLE_GROUP should not exist");
    %this.assert(!isObject("CUSTOMIZABLE_GROUP_SEATAREA"), "CUSTOMIZABLE_GROUP_SEATAREA should not exist");
    %theNugget.delete();
    %this.assert(!isObject(%realGroup), "the group" SPC %realGroup SPC " should not exist after nugget was deleted");
    return ;
}
function TEST_CS_NuggetCreateForEdit::runTest(%this)
{
    %guid = 1;
    %sku = 41000;
    %theNugget = InventoryNugget::construct(%sku, "0 0 0", "0 0 0", 1, %guid, "");
    %realGroup = %theNugget.objectGroup;
    %this.assert(isObject(%theNugget), "InventoryNugget::Construct failed");
    %this.assert(isObject(%realGroup), "InventoryNugget has bad objectGroup");
    %this.assert(isObject("CUSTOMIZABLE_GROUP"), "CUSTOMIZABLE_GROUP should exist when loading for edit");
    %this.assertSameObject("CUSTOMIZABLE_GROUP", %realGroup, "CUSTOMIZABLE_GROUP and objectgroup for the nugget are not the same");
    %theNugget.delete();
    %this.assert(!isObject(%realGroup), "the group" SPC %realGroup SPC " should not exist after nugget was deleted");
    return ;
}
function TEST_CS_CreationThroughSpace::runTest(%this)
{
    %sku = 41000;
    %thespace = CustomizableSpace::construct(TestCS_MockTriggerObj, "Bobs Home", 1000);
    %reference = %thespace.CreateInventoryItem(%sku, 1, "0 0 0", "0 0 0");
    %nugget = %thespace.GetInventoryItem(%reference);
    %this.assert(isObject(%nugget), "GetInventoryItem using ref:" SPC %reference SPC "failed to get an inventory nugget");
    %nugget = %thespace.GetInventoryItem("BAD ID");
    %this.assert(!isObject(%nugget), "GetInventoryItem using ref:" SPC %reference SPC "returned an inventory nugget");
    %nugget = %thespace.GetInventoryItem(%reference);
    %thespace.delete();
    %this.assert(!isObject(%nugget), "after deleting thespace, the nugget still exists");
    return ;
}
function TEST_CS_CreationThroughSpaceAndManipulate::runTest(%this)
{
    %sku = 41000;
    %createPos = "0 0 0";
    %createOrient = "0 0 0";
    %thespace = CustomizableSpace::construct(TestCS_MockTriggerObj, "Deluxe Apartment In The Sky", 1000);
    %reference = %thespace.CreateInventoryItem(%sku, 1, %createPos, %createOrient);
    %nugget = %thespace.GetInventoryItem(%reference);
    %this.assertSameString(%nugget.position, %createPos, "created position is not correct");
    %this.assertSameString(%nugget.orientation, %createOrient, "created orientation is not correct");
    %offset = "2.5 3 5";
    %nugget.MoveByOffset(%offset);
    %this.assertSameString(%nugget.position, %offset, "position after moving not correct");
    %this.assertSameString(%nugget.orientation, %createOrient, "orientation after moving not correct");
    %rotAmt = 90;
    %expected = setWord(%createOrient, 2, %rotAmt);
    %nugget.RotateClockwiseZ(%rotAmt);
    %this.assertSameString(%nugget.position, %offset, "position after moving not correct");
    %this.assertSameString(%nugget.orientation, %expected, "orientation after moving not correct");
    %offsetB = "-0.5 0 0";
    %expectedOffset = "2 3 5";
    %nugget.MoveByOffset(%offsetB);
    %this.assertSameString(%nugget.position, %expectedOffset, "position after moving not correct");
    %this.assertSameString(%nugget.orientation, %expected, "orientation after moving not correct");
    %thespace.delete();
    return ;
}
function TEST_CS_SpaceMultiSkuAndLimits::runTest(%this)
{
    %sku = 41000;
    %createPos = "0 0 0";
    %createOrient = "0 0 0";
    %maxItems = 2;
    %thespace = CustomizableSpace::construct(TestCS_MockTriggerObj, "a hovel", %maxItems);
    %reference1 = %thespace.CreateInventoryItem(%sku, 1, %createPos, %createOrient);
    %reference2 = %thespace.CreateInventoryItem(%sku, 1, %createPos, %createOrient);
    %reference3 = %thespace.CreateInventoryItem(%sku, 1, %createPos, %createOrient);
    %nugget1 = %thespace.GetInventoryItem(%reference1);
    %nugget2 = %thespace.GetInventoryItem(%reference2);
    %nugget3 = %thespace.GetInventoryItem(%reference3);
    %this.assert(isObject(%nugget1), "reference1 did not get good object");
    %this.assert(isObject(%nugget2), "reference2 did not get good object");
    %this.assertDifferentObject(%nugget1, %nugget2, "creating the same sku twice should yield two different objects");
    %this.assert(%nugget3 == 0, "should not be able to create more than" SPC %maxItems);
    %this.assert(%thespace.nuggets.getCount() == 2, "the space has more items than it should");
    %thespace.delete();
    %this.assert(!isObject(%nugget1), "objects should all be deleted when the space is deleted");
    %this.assert(!isObject(%nugget2), "objects should all be deleted when the space is deleted");
    return ;
}
function TEST_CS_SpaceDelete::runTest(%this)
{
    %sku = 41000;
    %createPos = "0 0 0";
    %createOrient = "0 0 0";
    %maxItems = 2;
    %thespace = CustomizableSpace::construct(TestCS_MockTriggerObj, "a hovel", %maxItems);
    %reference1 = %thespace.CreateInventoryItem(%sku, 1, %createPos, %createOrient);
    %thespace.DeleteInventoryItem(%reference1);
    %nugget1 = %thespace.GetInventoryItem(%reference1);
    %this.assert(%nugget1 == 0, "should not get a valid object using deleted reference id");
    %this.assert(%thespace.nuggets.getCount() == 0, "the space should be empty");
    %thespace.delete();
    return ;
}
function TEST_CS_SpaceDeleteUnKnownRef::runTest(%this)
{
    %sku = 41000;
    %createPos = "0 0 0";
    %createOrient = "0 0 0";
    %maxItems = 2;
    %thespace = CustomizableSpace::construct(TestCS_MockTriggerObj, "a hovel", %maxItems);
    %reference1 = %thespace.CreateInventoryItem(%sku, 1, %createPos, %createOrient);
    %unknownRef = 42;
    %ret = %thespace.DeleteInventoryItem(%unknownRef);
    %this.assert(%ret == 0, "if we delete an unknown reference id, DeleteInventoryItem should return zero");
    %this.assert(%thespace.nuggets.getCount() == 1, "the space should still have the good object in it after we delete a bad reference id");
    %thespace.delete();
    return ;
}
function TEST_CS_SpaceSaveToStringMapAndLoad::runTest(%this)
{
    %sku = 41000;
    %createPos = "5 10 20";
    %createOrient = "0 0 0";
    %maxItems = 2;
    %name = "cardboard box";
    %thespace = CustomizableSpace::construct(TestCS_MockTriggerObj, %name, %maxItems);
    %thespace.CreateInventoryItem(%sku, 1, %createPos, %createOrient);
    %thespace.CreateInventoryItem(%sku, 1, %createPos, %createOrient);
    %theStrings = %thespace.SaveSpaceToNewStringMap();
    %this.assert(isObject(%theStrings), "SaveSpaceToStringMap did not return a valid object");
    %thespace.delete();
    %thespace = CustomizableSpace::ConstructFromStringMap(TestCS_MockTriggerObj, %name, "The-Manager", %maxItems, %theStrings);
    %this.assert(%thespace.nuggets.getCount() == 2, "loaded space should have same number of objects as saved one");
    %this.assertSameString(%thespace.spaceName, %name, "loaded space has wrong name");
    %theStrings.delete();
    %thespace.delete();
    return ;
}
function TEST_CS_SpaceSaveToStringWithTestingObject::runTest(%this)
{
    %sku = 41000;
    %createPos = "5 10 20";
    %createOrient = "0 0 0";
    %maxItems = 2;
    %name = "cardboard box";
    %thespace = CustomizableSpace::construct(TestCS_MockTriggerObj, %name, %maxItems);
    %thespace.CreateInventoryItem(%sku, 1, %createPos, %createOrient);
    %thespace.CreateInventoryItem(%sku, 0, %createPos, %createOrient);
    %theStrings = %thespace.SaveSpaceToNewStringMap();
    %this.assert(isObject(%theStrings), "SaveSpaceToStringMap did not return a valid object");
    %thespace.delete();
    %thespace = CustomizableSpace::ConstructFromStringMap(TestCS_MockTriggerObj, %name, "The-Manager", %maxItems, %theStrings);
    %this.assert(%thespace.nuggets.getCount() == 1, "loaded space should have only the object we owned when we saved it");
    %this.assertSameString(%thespace.spaceName, %name, "loaded space has wrong name");
    %theStrings.delete();
    %thespace.delete();
    return ;
}
function TEST_CS_SpaceSaveToStringMapAndLoadBasedOnOffsets::runTest(%this)
{
    %space1Offset = "0 0 0";
    %space2Offset = "100 200 300";
    %space3Offset = "-100 -200 300";
    %objectPos1 = "0 0 0";
    %objectPos2 = "100 200 300";
    %objectPos3 = "-100 -200 300";
    %sku = 41000;
    %createOrient = "0 0 0";
    %maxItems = 2;
    %name = "cardboard box";
    %boundBox = "-500 -500 -500 500 500 500";
    %thespace = CustomizableSpace::construct(TestCS_MockTriggerObj, %name, %maxItems, %boundBox, %space1Offset);
    %thespace.CreateInventoryItem(%sku, 1, %objectPos1, %createOrient);
    %theStrings = %thespace.SaveSpaceToNewStringMap();
    %thespace.delete();
    %thespace = CustomizableSpace::ConstructFromStringMap(TestCS_MockTriggerObj, %name, "The-Manager", %maxItems, %theStrings, %boundBox, %space2Offset);
    %theStrings.delete();
    %refID = trim(%thespace.ListReferenceIDs());
    %nugget = %thespace.GetInventoryItem(%refID);
    %this.assertSameString(%nugget.position, %objectPos2, "after creating the nugget in the new offset spot, it did not have the correct offset itself");
    %theStrings = %thespace.SaveSpaceToNewStringMap();
    %thespace.delete();
    %thespace = CustomizableSpace::ConstructFromStringMap(TestCS_MockTriggerObj, %name, "The-Manager", %maxItems, %theStrings, %boundBox, %space3Offset);
    %refID = trim(%thespace.ListReferenceIDs());
    echo("refid is" SPC %refID);
    %nugget = %thespace.GetInventoryItem(%refID);
    %this.assertSameString(%nugget.position, %objectPos3, "after creating the nugget in the new negative offset spot, it did not have the correct offset itself");
    %theStrings.delete();
    %thespace.delete();
    return ;
}
function TEST_CS_SpaceListIDs::runTest(%this)
{
    %sku = 41000;
    %createPos = "5 10 20";
    %createOrient = "0 0 0";
    %maxItems = 2;
    %name = "cardboard box";
    %thespace = CustomizableSpace::construct(TestCS_MockTriggerObj, %name, %maxItems);
    %ref1 = %thespace.CreateInventoryItem(%sku, 1, %createPos, %createOrient);
    %ref2 = %thespace.CreateInventoryItem(%sku, 1, %createPos, %createOrient);
    %expectedList = %ref1 SPC %ref2;
    %list = %thespace.ListReferenceIDs();
    %this.assertSameString(%list, %expectedList, "list of ids does not match expected");
    %thespace.delete();
    return ;
}
function TEST_CS_SpaceManager::runTest(%this)
{
    %manager = SpaceManager::GetInstance();
    %manager2 = SpaceManager::GetInstance();
    %this.assert(isObject(%manager), "The Space Manager Singleton GetInstance function did not return a valid object");
    %this.assertSameObject(%manager, %manager2, "expected the space manager to be a singleton, one and only one that is");
    %spaces = %manager.spaces;
    %this.assert(isObject(%spaces), "expected the TheSpaces to be a valid object");
    %name = "My Massive Mortuary";
    %this.assert(!isObject(%manager.FindSpaceNamed(%name)), "did not expect the space named" SPC %name SPC "to exist yet");
    %thespace = %manager.CreateSpaceNamed(TestCS_MockTriggerObj, %name);
    %this.assert(%thespace.nuggets.getCount() == 0, "expected the space to be empty after first creation");
    %this.assert(isObject(%manager.FindSpaceNamed(%name)), "expected to find the space called" SPC %name SPC " but we did not.");
    %thespace = %manager.FindSpaceNamed(%name);
    %thespace.delete();
    %this.assert(!isObject(%manager.FindSpaceNamed(%name)), "after deleting the space named" SPC %name SPC "we should not be able to find it in the space manager");
    %sku = 41000;
    %createPos = "5 10 20";
    %createOrient = "0 0 0";
    %maxItems = 2;
    %thespace = %manager.CreateSpaceNamed(TestCS_MockTriggerObj, %name);
    %thespace.CreateInventoryItem(%sku, 1, %createPos, %createOrient);
    %thespace.CreateInventoryItem(%sku, 1, %createPos, %createOrient);
    %this.assert(%thespace.nuggets.getCount() == 2, "expected the space to have two items in it, but it had" SPC %thespace.nuggets.getCount());
    %manager.StandAloneSaveSpaceNamed(%name);
    %thespace.delete();
    %thespace = %manager.CreateSpaceNamed(TestCS_MockTriggerObj, %name);
    %this.assert(%thespace.nuggets.getCount() == 2, "after saving a space with 2 items in it, expected the space to be created with the same two items next time");
    %thespace.DeleteAllInventory();
    %this.assert(%thespace.nuggets.getCount() == 0, "after deleting inventory, the space should be empty");
    %manager.StandAloneSaveSpaceNamed(%name);
    %thespace.delete();
    %thespace = %manager.CreateSpaceNamed(TestCS_MockTriggerObj, %name);
    %thespace2 = %manager.CreateSpaceNamed(TestCS_MockTriggerObj, %name);
    %this.assert(!isObject(%thespace2), "Creating the same space twice should fail, but we got back a valid object");
    %thespace.delete();
    return ;
}
function TEST_CS_ConstrainMovementToSpace::runTest(%this)
{
    %manager = SpaceManager::GetInstance();
    %name = "My Massive Mortuary";
    %sku = 41000;
    %createPos = "0 0 0";
    %createOrient = "0 0 0";
    %maxItems = 2;
    %boundBox = "-100 -100 -100 100 100 100";
    %thespace = %manager.CreateSpaceNamed(TestCS_MockTriggerObj, %name, "", %boundBox);
    %this.assertSameString(%thespace.constrainToBox, %boundBox, "space should have constraintobox we gave the spacemanager when creating it");
    %reference = %thespace.CreateInventoryItem(%sku, 1, %createPos, %createOrient);
    %nugget = %thespace.GetInventoryItem(%reference);
    %this.assertSameString(%nugget.constrainToBox, %boundBox, "nugget should have constraintobox we gave the space when creating it");
    %offset = "-200 -200 -200";
    %nugget.MoveByOffset(%offset);
    %this.assertSameString(%nugget.position, %createPos, "attempting to move outside of the min bounds should have failed");
    %offset = "200 0 0";
    %nugget.MoveByOffset(%offset);
    %this.assertSameString(%nugget.position, %createPos, "attempting to move outside of the max bounds should have failed");
    %createPosBad = "-1000 -0 0";
    %reference = %thespace.CreateInventoryItem(%sku, 1, %createPosBad, %createOrient);
    %this.assert(%reference == 0, "trying to create an inventory nugget outside of the space bounds should have failed");
    %thespace.delete();
    return ;
}
function TEST_CS_EditPermissions::runTest(%this)
{
    %mockPlayerA = new ScriptObject();
    %mockPlayerB = new ScriptObject();
    %name = "Blah Blah Space";
    %manager = SpaceManager::GetInstance();
    %thespace = %manager.CreateSpaceNamed(TestCS_MockTriggerObj, %name);
    %ret = %thespace.StartEditing(%mockPlayerA);
    %this.assert(%ret, "expected mockPlayerA to be able to edit the space");
    %this.assertSameObject(%thespace.currentEditor, %mockPlayerA, "expected mockPlayerA to be the same as space.currentEditor");
    %ret = %thespace.StartEditing(%mockPlayerB);
    %this.assert(!%ret, "mockPlayerB should not be able to edit a space already being edited");
    %thespace.StopEditing(%mockPlayerA);
    %this.assert(%thespace.currentEditor == 0, "when mockPlayerA stops editing, we expect the current editor to be null");
    %ret = %thespace.StartEditing(%mockPlayerB);
    %this.assert(%ret, "mockPlayerB should now be able to edit the space since no one else is");
    %manager.OnPlayerRemoved(%mockPlayerB);
    %this.assert(%thespace.currentEditor == 0, "when mockPlayerB is removed, he should no longer be editing the space either");
    %this.assert(!%thespace.StopEditing(%mockPlayerB), "if player is not editing the space, stopEditing should return false");
    %thespace.delete();
    %mockPlayerA.delete();
    %mockPlayerB.delete();
    return ;
}
function TEST_CS_SpaceLoadConfigurationsFromStringMap::runTest(%this)
{
    %sku = 41000;
    %createPos = "5 10 20";
    %createOrient = "0 0 0";
    %maxItems = 5;
    %name = "voltron the space";
    %thespace = CustomizableSpace::construct(TestCS_MockTriggerObj, %name, %maxItems);
    %thespace.CreateInventoryItem(%sku, 1, %createPos, %createOrient, "");
    %thespace.CreateInventoryItem(%sku, 1, %createPos, %createOrient, "");
    %theStrings = %thespace.SaveSpaceToNewStringMap();
    %thespace.DeleteAllInventory();
    %this.assert(%thespace.nuggets.getCount() == 0, "after deleting inventory, the space should be empty");
    %thespace.LoadConfigurationFromStringMap(%theStrings);
    %this.assert(%thespace.nuggets.getCount() == 2, "loaded configuration should have same number of objects as saved one");
    %thespace.CreateInventoryItem(%sku, 1, %createPos, %createOrient, "");
    %thespace.CreateInventoryItem(%sku, 1, %createPos, %createOrient, "");
    %thespace.CreateInventoryItem(%sku, 1, %createPos, %createOrient, "");
    %this.assert(%thespace.nuggets.getCount() == 5, "after loading the configuration and adding 3 more, we should have 5 now");
    %thespace.LoadConfigurationFromStringMap(%theStrings);
    %this.assert(%thespace.nuggets.getCount() == 2, "loaded configuration should have deleted all the 5 existing and loaded only 2");
    %theStrings.delete();
    %thespace.delete();
    return ;
}
function TEST_CS_SpaceDeleteUnownedObjects::runTest(%this)
{
    %sku = 41000;
    %createPos = "5 10 20";
    %createOrient = "0 0 0";
    %maxItems = 10;
    %name = "voltron the space";
    %thespace = CustomizableSpace::construct(TestCS_MockTriggerObj, %name, %maxItems);
    %thespace.CreateInventoryItem(%sku, 1, %createPos, %createOrient, "");
    %thespace.CreateInventoryItem(%sku, 1, %createPos, %createOrient, "");
    %thespace.CreateInventoryItem(%sku, 1, %createPos, %createOrient, "");
    %thespace.CreateInventoryItem(%sku, 1, %createPos, %createOrient, "");
    %thespace.CreateInventoryItem(%sku, 1, %createPos, %createOrient, "");
    %this.assert(%thespace.nuggets.getCount() == 5, "after loading the configuration and adding 3 more, we should have 5 now");
    %deleteTheseMap = new StringMap();
    if (isObject(MissionCleanup))
    {
        MissionCleanup.add(%deleteTheseMap);
    }
    %deleteTheseMap.put(%sku, 2);
    %badSku = 32;
    %deleteTheseMap.put(%badSku, 1);
    %thespace.DeleteUnownedSkus(%deleteTheseMap);
    %this.assert(%thespace.nuggets.getCount() == 3, "after notifying the space to delete 2 of sku, we should only have 3 objects, but we have" SPC %thespace.nuggets.getCount());
    %thespace.DeleteUnownedSkus(%deleteTheseMap);
    %this.assert(%thespace.nuggets.getCount() == 1, "after notifying the space to delete 2 more of sku, we should only have 1 objects, but we have" SPC %thespace.nuggets.getCount());
    %thespace.DeleteUnownedSkus(%deleteTheseMap);
    %this.assert(%thespace.nuggets.getCount() == 0, "after notifying the space to delete 2 more of sku, we should have no more, but we have" SPC %thespace.nuggets.getCount());
    %deleteTheseMap.delete();
    %thespace.delete();
    return ;
}
