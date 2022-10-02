$testPlayer::sampleInitialInventory1 = 12345;
$testPlayer::sampleInitialInventory2 = "12345 45678";
$testPlayer::sampleSKUsOne = 12345;
$testPlayer::sampleSKUsTwo = 45678;
function testPlayer_Master()
{
    error("inside testPlayer_Master");
    return ;
}
function testPlayer_AddInventoryTest()
{
    $Player::inventory = $testPlayer::sampleInitialInventory1;
    $player.addInventorySKUs($testPlayer::sampleSKUsTwo);
    echo("testPlayer_AddInventoryTest(): " @ $Player::inventory);
    if (!($Player::inventory $= $testPlayer::sampleSKUsTwo) SPC $testPlayer::sampleInitialInventory1)
    {
        log("network", "error", "testPlayer_AddInventoryTest(): failed");
    }
    return ;
}
function testPlayer_RemoveInventoryTest()
{
    $Player::inventory = $testPlayer::sampleInitialInventory2;
    $player.removeInventorySKUs($testPlayer::sampleSKUsTwo);
    echo("testPlayer_RemoveInventoryTest(): " @ $Player::inventory);
    if (!($Player::inventory $= $testPlayer::sampleInitialInventory1))
    {
        log("network", "error", "testPlayer_RemoveInventoryTest(): failed");
    }
    return ;
}
function testPlayer_AddInventoryMultiTest()
{
    error("inside testPlayer_AddInventoryMultiTest");
    %q = "testPlayer_AddInventoryMultiTest()";
    %dry = "1 2";
    %delta = "3 4";
    %wetExpected = "3 4 1 2";
    $Player::inventory = %dry;
    $player.addInventorySKUs(%delta);
    %wetActual = $Player::inventory;
    return testPlayer_Evaluate(%dry, %delta, %wetExpected, %wetActual, %q);
}
function testPlayer_RemoveInventoryMultiTest()
{
    error("inside testPlayer_RemoveInventoryMultiTest");
    %q = "testPlayer_RemoveInventoryMultiTest()";
    %dry = "1 2 4 3 5";
    %delta = "5 4 6";
    %wetExpected = "1 2 3";
    $Player::inventory = %dry;
    $player.removeInventorySKUs(%delta);
    %wetActual = $Player::inventory;
    return testPlayer_Evaluate(%dry, %delta, %wetExpected, %wetActual, %q);
}
function testPlayer_Evaluate(%dry, %delta, %wetExpected, %wetActual, %testname)
{
    %pass = %wetExpected $= %wetActual;
    if (!%pass)
    {
        log("wardrobe", "error", "failed:" SPC %testname);
        log("wardrobe", "debug", "dry         = \"" @ %dry @ "\"");
        log("wardrobe", "debug", "delta       = \"" @ %delta @ "\"");
        log("wardrobe", "debug", "wetActual   = \"" @ %wetActual @ "\"");
        log("wardrobe", "debug", "wetExpected = \"" @ %wetExpected @ "\"");
    }
    else
    {
        log("wardrobe", "debug", "passed:" SPC %testname);
    }
    return %pass;
}
