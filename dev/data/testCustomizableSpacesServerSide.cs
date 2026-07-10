DeclareTestSuite("TestSuite_CSServerSideTests");
DeclareTestSuite("TestSuite_CSServerSideTests2");
function TestSuite_CSServerSideTests::ShouldRunOnServer(%this) {
    return 1;
};
function TestSuite_CSServerSideTests::setup(%this) {
    3000.addTestCaseDelayed(%this, "TEST_CS_DoubleActivateSpace");
};
function TestSuite_CSServerSideTests::TearDown(%this) {
};
function TestSuite_CSServerSideTests2::ShouldRunOnServer(%this) {
    return 1;
};
function TestSuite_CSServerSideTests2::setup(%this) {
    3000.addTestCaseDelayed(%this, "TEST_CS_DoubleActivateSpaceType2");
};
function TestSuite_CSServerSideTests2::TearDown(%this) {
};
function TEST_CS_DoubleActivateSpace::runTest(%this) {
    "This Test should only be run in a private space grid".assertSameString(%this, MissionInfo.mode, "PrivateSpaceGrid");
    %freeGridNumber = CustomizableSpaceServerGrid::GetNextUnActivatedSpaceGridNumber();
    "I couldn't find any free grid space to run this test with".assert(%this, (%freeGridNumber > 0.0));
    %this.lastFreeNumber = %freeGridNumber;
    %client = 0.getObject(ClientGroup);
    %userName = %client.nameBase;
    %player = %client.Player;
    %apartmentName = GetServerNameSpaceTaggedName(MissionInfo.building) @ "." @ %userName;
    %theSpaceTrigger = "PRIVATESPACE_AREA_" @ %freeGridNumber;
    CustomizableSpaceServerGrid::ActivateSpace(%userName, %apartmentName, %theSpaceTrigger, %player);
    CustomizableSpaceServerGrid::ActivateSpace(%userName, %apartmentName, %theSpaceTrigger, %player);
};
function TEST_CS_DoubleActivateSpace::delayedEval(%this) {
    %numActiveSpaces = CustomizableSpaceServerGrid::GetNumActiveSpacesInGrid();
    "I expected to have a model apartment and one other space active, for a total of only 2 but I have" @ " " @ %numActiveSpaces.assert(%this, (%numActiveSpaces == 2.0));
    %freeGridNumber = CustomizableSpaceServerGrid::GetNextUnActivatedSpaceGridNumber();
    "I expected to have taken that grid space, but it is still marked as free".assert(%this, (%this.lastFreeNumber != %freeGridNumber));
};
function TEST_CS_DoubleActivateSpaceType2::runTest(%this) {
    "This Test should only be run in a private space grid".assertSameString(%this, MissionInfo.mode, "PrivateSpaceGrid");
    %client = 0.getObject(ClientGroup);
    %userName = %client.nameBase;
    %player = %client.Player;
    %apartmentName = GetServerNameSpaceTaggedName(MissionInfo.building) @ "." @ %userName;
    %freeGridNumber = CustomizableSpaceServerGrid::GetNextUnActivatedSpaceGridNumber();
    "I couldn't find any free grid space to run this test with".assert(%this, (%freeGridNumber > 0.0));
    %this.lastFreeNumber = %freeGridNumber;
    %theSpaceTrigger = "PRIVATESPACE_AREA_" @ %freeGridNumber;
    CustomizableSpaceServerGrid::ActivateSpace(%userName, %apartmentName, %theSpaceTrigger, %player);
    %freeGridNumber2 = CustomizableSpaceServerGrid::GetNextUnActivatedSpaceGridNumber();
    "expected a different number here, since the first one would be pending".assert(%this, (%freeGridNumber2 != %freeGridNumber));
    %theSpaceTrigger2 = "PRIVATESPACE_AREA_" @ %freeGridNumber2;
    CustomizableSpaceServerGrid::ActivateSpace(%userName, %apartmentName, %theSpaceTrigger2, %player);
};
function TEST_CS_DoubleActivateSpaceType2::delayedEval(%this) {
    %numActiveSpaces = CustomizableSpaceServerGrid::GetNumActiveSpacesInGrid();
    "I expected to have a model apartment and one other space active, for a total of only 2 but I have" @ " " @ %numActiveSpaces.assert(%this, (%numActiveSpaces == 2.0));
    %freeGridNumber = CustomizableSpaceServerGrid::GetNextUnActivatedSpaceGridNumber();
    "I expected to have taken that grid space, but it is still marked as free".assert(%this, (%this.lastFreeNumber != %freeGridNumber));
};
