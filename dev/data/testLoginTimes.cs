exec("./skeletonClient.cs");
function testLoginTimes() {
    $loginLogout = 0;
    %testLogin = new ScriptObject(skeletonClient) {
        userName = $UserPref::Player::Name;
        password = $UserPref::Player::Password;
        joinAction = "doSomething";
        quitOnError = "true";
    };
    %testLogin.init();
    echo("LOAD: Logging into " @ $cityIndex[$Cities @ $cityIndex]);
    $cityIndex[$Cities @ $cityIndex].doLogin(%testLogin);
    $cityIndex = ($cityIndex + 1.0);
};
function doSomething() {
    if (ClosetGui.isVisible()) {
        ClosetGui.close();
    }
    0.say(pChat, "Hello!", 0);
    0.say(pChat, "Goodbye!", 0);
    logout(0);
    WorldMap.exit();
    if (($cityIndex <= $maxCities)) {
        schedule(3000, 0, testLoginTimes);
    }
    skeletonClient::reallyQuit();
};
function initCities() {
    %i = 0;
    $Cities[%i = (%i + 1.0)] = "NewVeneziaNorth";
    $Cities[%i = (%i + 1.0)] = "LaGenoaAiresNorth";
    $Cities[%i = (%i + 1.0)] = "RaijukuNorth";
    $Cities[%i = (%i + 1.0)] = "IIR Raijuku";
    $Cities[%i = (%i + 1.0)] = "LaBoca Apartments";
    $Cities[%i = (%i + 1.0)] = "Warehouse Loft";
    $Cities[%i = (%i + 1.0)] = "BeatUp";
    $maxCities = %i;
    $cityIndex = 1;
};
initCities();
testLoginTimes();
