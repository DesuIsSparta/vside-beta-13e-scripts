DeclareTestSuite("TestSuite_CSServerSideTests");
DeclareTestSuite("TestSuite_CSServerSideTests2");
function TestSuite_CSServerSideTests::ShouldRunOnServer(%this)
{
    return 1;
}
function TestSuite_CSServerSideTests::setup(%this)
{
    %this.addTestCaseDelayed("TEST_CS_DoubleActivateSpace", 3000);
    return ;
}
function TestSuite_CSServerSideTests::TearDown(%this)
{
    return ;
}
function TestSuite_CSServerSideTests2::ShouldRunOnServer(%this)
{
    return 1;
}
function TestSuite_CSServerSideTests2::setup(%this)
{
    %this.addTestCaseDelayed("TEST_CS_DoubleActivateSpaceType2", 3000);
    return ;
}
function TestSuite_CSServerSideTests2::TearDown(%this)
{
    return ;
}
function TEST_CS_DoubleActivateSpace::runTest(%this)
{
    %this.assertSameString(MissionInfo.mode, "PrivateSpaceGrid", "This Test should only be run in a private space grid");
    %freeGridNumber = CustomizableSpaceServerGrid::GetNextUnActivatedSpaceGridNumber();
    %this.assert(%freeGridNumber > 0, "I couldn\'t find any free grid space to run this test with");
    %this.lastFreeNumber = %freeGridNumber;
    %client = ClientGroup.getObject(0);
    %userName = %client.nameBase;
    %player = %client.Player;
    %apartmentName = GetServerNameSpaceTaggedName(MissionInfo.building) @ "." @ %userName;
    %theSpaceTrigger = "PRIVATESPACE_AREA_" @ %freeGridNumber;
    CustomizableSpaceServerGrid::ActivateSpace(%userName, %apartmentName, %theSpaceTrigger, %player);
    CustomizableSpaceServerGrid::ActivateSpace(%userName, %apartmentName, %theSpaceTrigger, %player);
    return ;
}
function TEST_CS_DoubleActivateSpace::delayedEval(%this)
{
    %numActiveSpaces = CustomizableSpaceServerGrid::GetNumActiveSpacesInGrid();
    %this.assert(%numActiveSpaces == 2, "I expected to have a model apartment and one other space active, for a total of only 2 but I have" SPC %numActiveSpaces);
    %freeGridNumber = CustomizableSpaceServerGrid::GetNextUnActivatedSpaceGridNumber();
    %this.assert(%this.lastFreeNumber != %freeGridNumber, "I expected to have taken that grid space, but it is still marked as free");
    return ;
}
function TEST_CS_DoubleActivateSpaceType2::runTest(%this)
{
    %this.assertSameString(MissionInfo.mode, "PrivateSpaceGrid", "This Test should only be run in a private space grid");
    %client = ClientGroup.getObject(0);
    %userName = %client.nameBase;
    %player = %client.Player;
    %apartmentName = GetServerNameSpaceTaggedName(MissionInfo.building) @ "." @ %userName;
    %freeGridNumber = CustomizableSpaceServerGrid::GetNextUnActivatedSpaceGridNumber();
    %this.assert(%freeGridNumber > 0, "I couldn\'t find any free grid space to run this test with");
    %this.lastFreeNumber = %freeGridNumber;
    %theSpaceTrigger = "PRIVATESPACE_AREA_" @ %freeGridNumber;
    CustomizableSpaceServerGrid::ActivateSpace(%userName, %apartmentName, %theSpaceTrigger, %player);
    %freeGridNumber2 = CustomizableSpaceServerGrid::GetNextUnActivatedSpaceGridNumber();
    %this.assert(%freeGridNumber2 != %freeGridNumber, "expected a different number here, since the first one would be pending");
    %theSpaceTrigger2 = "PRIVATESPACE_AREA_" @ %freeGridNumber2;
    CustomizableSpaceServerGrid::ActivateSpace(%userName, %apartmentName, %theSpaceTrigger2, %player);
    return ;
}
function TEST_CS_DoubleActivateSpaceType2::delayedEval(%this)
{
    %numActiveSpaces = CustomizableSpaceServerGrid::GetNumActiveSpacesInGrid();
    %this.assert(%numActiveSpaces == 2, "I expected to have a model apartment and one other space active, for a total of only 2 but I have" SPC %numActiveSpaces);
    %freeGridNumber = CustomizableSpaceServerGrid::GetNextUnActivatedSpaceGridNumber();
    %this.assert(%this.lastFreeNumber != %freeGridNumber, "I expected to have taken that grid space, but it is still marked as free");
    return ;
}
