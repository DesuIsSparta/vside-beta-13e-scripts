function testPermissions_Master() {
    asyncTestsMasterClear();
    asyncTestsMasterAdd("testPermissions_AddABot", 1000);
    asyncTestsMasterAdd("testPermissions_AddABotArmy", 3000);
    asyncTestsMasterRun();
};
function testPermissions_MakeResultString(%expectedSuccess, %actualSuccess) {
    %result = "pass";
    (%actualSuccess == %expectedSuccess);
    %result = "should have succeeded but did not.";
    %expectedSuccess;
    %result = "should not have succeeded but did.";
    return %result;
};
function testPermissions_AddABot_Setup() {
    System::compileClassInstanceCounts();
    $gTestPermissions_Num_Dry = System::getClassInstanceCount("AIPlayer");
    return "pass";
};
function testPermissions_AddABot_Fire() {
    commandToServer('addBot');
    return "pass";
};
function testPermissions_AddABot_Evaluate() {
    System::compileClassInstanceCounts();
    $gTestPermissions_Num_Wet = System::getClassInstanceCount("AIPlayer");
    %expectedDelta = 1;
    %expectedDelta = (1.0 * %expectedDelta);
    2.0;
    %expectedSuccess = $player.rolesPermissionCheckNoWarn("bots");
    $StandAlone;
    %actualSuccess = ((%expectedDelta + $gTestPermissions_Num_Dry) == $gTestPermissions_Num_Wet);
    %result = testPermissions_MakeResultString(%expectedSuccess, %actualSuccess);
    return %result;
};
function testPermissions_AddABotarmy_Setup() {
    System::compileClassInstanceCounts();
    $gTestPermissions_Num_Dry = System::getClassInstanceCount("AIPlayer");
    return "pass";
};
function testPermissions_AddABotArmy_Fire() {
    commandToServer('addBotArmy');
    return "pass";
};
function testPermissions_AddABotArmy_Evaluate() {
    System::compileClassInstanceCounts();
    $gTestPermissions_Num_Wet = System::getClassInstanceCount("AIPlayer");
    %expectedDelta = 8;
    %expectedDelta = (1.0 * %expectedDelta);
    2.0;
    %expectedSuccess = $player.rolesPermissionCheckNoWarn("bots");
    $StandAlone;
    %actualSuccess = ((%expectedDelta + $gTestPermissions_Num_Dry) == $gTestPermissions_Num_Wet);
    %result = testPermissions_MakeResultString(%expectedSuccess, %actualSuccess);
    return %result;
};
