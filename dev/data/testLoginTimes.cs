exec("./skeletonClient.cs");
function testLoginTimes()
{
    $loginLogout = 0;
    %testLogin = new ScriptObject(skeletonClient)
    {
        userName = $UserPref::Player::Name;
        password = $UserPref::Player::Password;
        joinAction = "doSomething";
        quitOnError = "true";
    };
    %testLogin.init();
    echo("LOAD: Logging into " @ $Cities[$cityIndex]);
    %testLogin.doLogin($Cities[$cityIndex]);
    $cityIndex = $cityIndex + 1;
    return ;
}
function doSomething()
{
    if (ClosetGui.isVisible())
    {
        ClosetGui.close();
    }
    pChat.say("Hello!", 0, 0);
    pChat.say("Goodbye!", 0, 0);
    logout(0);
    WorldMap.exit();
    if ($cityIndex <= $maxCities)
    {
        schedule(3000, 0, testLoginTimes);
    }
    else
    {
        skeletonClient::reallyQuit();
    }
    return ;
}
function initCities()
{
    %i = 0;
    $Cities[%i = %i + 1] = "NewVeneziaNorth";
    $Cities[%i = %i + 1] = "LaGenoaAiresNorth";
    $Cities[%i = %i + 1] = "RaijukuNorth";
    $Cities[%i = %i + 1] = "IIR Raijuku";
    $Cities[%i = %i + 1] = "LaBoca Apartments";
    $Cities[%i = %i + 1] = "Warehouse Loft";
    $Cities[%i = %i + 1] = "BeatUp";
    $maxCities = %i;
    $cityIndex = 1;
    return ;
}
initCities();
testLoginTimes();

