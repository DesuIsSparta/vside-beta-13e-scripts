function testPermissions_Master()
{
    asyncTestsMasterClear();
    asyncTestsMasterAdd("testPermissions_AddABot", 1000);
    asyncTestsMasterAdd("testPermissions_AddABotArmy", 3000);
    asyncTestsMasterRun();
    return ;
}
function testPermissions_MakeResultString(%expectedSuccess, %actualSuccess)
{
    if (%expectedSuccess == %actualSuccess)
    {
        %result = "pass";
    }
    else
    {
        if (%expectedSuccess)
        {
            %result = "should have succeeded but did not.";
        }
        else
        {
            %result = "should not have succeeded but did.";
        }
    }
    return %result;
}
function testPermissions_AddABot_Setup()
{
    System::compileClassInstanceCounts();
    $gTestPermissions_Num_Dry = System::getClassInstanceCount("AIPlayer");
    return "pass";
}
function testPermissions_AddABot_Fire()
{
    commandToServer('addBot');
    return "pass";
}
function testPermissions_AddABot_Evaluate()
{
    System::compileClassInstanceCounts();
    $gTestPermissions_Num_Wet = System::getClassInstanceCount("AIPlayer");
    %expectedDelta = 1;
    %expectedDelta = %expectedDelta * $StandAlone ? 2 : 1;
    %expectedSuccess = $player.rolesPermissionCheckNoWarn("bots");
    %actualSuccess = $gTestPermissions_Num_Wet == ($gTestPermissions_Num_Dry + %expectedDelta);
    %result = testPermissions_MakeResultString(%expectedSuccess, %actualSuccess);
    return %result;
}
function testPermissions_AddABotarmy_Setup()
{
    System::compileClassInstanceCounts();
    $gTestPermissions_Num_Dry = System::getClassInstanceCount("AIPlayer");
    return "pass";
}
function testPermissions_AddABotArmy_Fire()
{
    commandToServer('addBotArmy');
    return "pass";
}
function testPermissions_AddABotArmy_Evaluate()
{
    System::compileClassInstanceCounts();
    $gTestPermissions_Num_Wet = System::getClassInstanceCount("AIPlayer");
    %expectedDelta = 8;
    %expectedDelta = %expectedDelta * $StandAlone ? 2 : 1;
    %expectedSuccess = $player.rolesPermissionCheckNoWarn("bots");
    %actualSuccess = $gTestPermissions_Num_Wet == ($gTestPermissions_Num_Dry + %expectedDelta);
    %result = testPermissions_MakeResultString(%expectedSuccess, %actualSuccess);
    return %result;
}
