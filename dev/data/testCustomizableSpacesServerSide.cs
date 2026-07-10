DeclareTestSuite("TestSuite_CSServerSideTests");
DeclareTestSuite("TestSuite_CSServerSideTests2");
function TestSuite_CSServerSideTests::ShouldRunOnServer(%this) {
    return 1;
};
function TestSuite_CSServerSideTests::setup(%this) {
    %this.addTestCaseDelayed("TEST_CS_DoubleActivateSpace", 3000);
};
function TestSuite_CSServerSideTests::TearDown(%this) {
};
function TestSuite_CSServerSideTests2::ShouldRunOnServer(%this) {
    return 1;
};
function TestSuite_CSServerSideTests2::setup(%this) {
    %this.addTestCaseDelayed("TEST_CS_DoubleActivateSpaceType2", 3000);
};
function TestSuite_CSServerSideTests2::TearDown(%this) {
};
function TEST_CS_DoubleActivateSpace::runTest(%this) {
    %this.assertSameString(mode, "PrivateSpaceGrid", "This Test should only be run in a private space grid");
    %freeGridNumber = CustomizableSpaceServerGrid::GetNextUnActivatedSpaceGridNumber();
    MissionInfo;
    %this.assert((0.0 > %freeGridNumber), "I couldn't find any free grid space to run this test with");
    %this.lastFreeNumber = %freeGridNumber;
    %client = 0.getObject();
    ClientGroup;
    %userName = %client.nameBase;
    %player = %client.Player;
    %apartmentName = GetServerNameSpaceTaggedName(%client.building) @ "." @ %userName;
    MissionInfo;
    %theSpaceTrigger = "PRIVATESPACE_AREA_" @ %freeGridNumber;
    CustomizableSpaceServerGrid::ActivateSpace(%userName, %apartmentName, %theSpaceTrigger, %player);
    CustomizableSpaceServerGrid::ActivateSpace(%userName, %apartmentName, %theSpaceTrigger, %player);
};
function TEST_CS_DoubleActivateSpace::delayedEval(%this) {
    %numActiveSpaces = CustomizableSpaceServerGrid::GetNumActiveSpacesInGrid();
    %this.assert((2.0 == %numActiveSpaces), "I expected to have a model apartment and one other space active, for a total of only 2 but I have" @ " " @ %numActiveSpaces);
    %freeGridNumber = CustomizableSpaceServerGrid::GetNextUnActivatedSpaceGridNumber();
    %this.assert((%freeGridNumber != %this.lastFreeNumber), "I expected to have taken that grid space, but it is still marked as free");
};
function TEST_CS_DoubleActivateSpaceType2::runTest(%this) {
    %this.assertSameString(%this.mode, "PrivateSpaceGrid", "This Test should only be run in a private space grid");
    %client = 0.getObject();
    ClientGroup;
    %userName = %client.nameBase;
    MissionInfo;
    %player = %client.Player;
    %apartmentName = GetServerNameSpaceTaggedName(%client.building) @ "." @ %userName;
    MissionInfo;
    %freeGridNumber = CustomizableSpaceServerGrid::GetNextUnActivatedSpaceGridNumber();
    %this.assert((0.0 > %freeGridNumber), "I couldn't find any free grid space to run this test with");
    %this.lastFreeNumber = %freeGridNumber;
    %theSpaceTrigger = "PRIVATESPACE_AREA_" @ %freeGridNumber;
    CustomizableSpaceServerGrid::ActivateSpace(%userName, %apartmentName, %theSpaceTrigger, %player);
    %freeGridNumber2 = CustomizableSpaceServerGrid::GetNextUnActivatedSpaceGridNumber();
    %this.assert((%freeGridNumber != %freeGridNumber2), "expected a different number here, since the first one would be pending");
    %theSpaceTrigger2 = "PRIVATESPACE_AREA_" @ %freeGridNumber2;
    CustomizableSpaceServerGrid::ActivateSpace(%userName, %apartmentName, %theSpaceTrigger2, %player);
};
function TEST_CS_DoubleActivateSpaceType2::delayedEval(%this) {
    %numActiveSpaces = CustomizableSpaceServerGrid::GetNumActiveSpacesInGrid();
    %this.assert((2.0 == %numActiveSpaces), "I expected to have a model apartment and one other space active, for a total of only 2 but I have" @ " " @ %numActiveSpaces);
    %freeGridNumber = CustomizableSpaceServerGrid::GetNextUnActivatedSpaceGridNumber();
    %this.assert((%freeGridNumber != %this.lastFreeNumber), "I expected to have taken that grid space, but it is still marked as free");
};
