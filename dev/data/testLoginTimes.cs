exec("./skeletonClient.cs");
function testLoginTimes() {
    $loginLogout = 0;
    userName = new ScriptObject(skeletonClient) @ $UserPref::Player::Name;
    password = $UserPref::Player::Password;
    joinAction = "doSomething";
    quitOnError = "true";
    %testLogin = ;
    %testLogin.init();
    echo("LOAD: Logging into " @ $cityIndex[$Cities @ $cityIndex]);
    %testLogin.doLogin($cityIndex[$Cities @ $cityIndex]);
    $cityIndex = (1.0 + $cityIndex);
};
function doSomething() {
    if (isVisible()) {
        close();
    }
    "Hello!".say(0, 0);
    "Goodbye!".say(0, 0);
    logout(0);
    exit();
    if (($maxCities <= $cityIndex)) {
        schedule(3000, 0);
    }
    skeletonClient::reallyQuit();
};
function initCities() {
    %i = 0;
    %i = (1.0 + %i);
    %i["NewVeneziaNorth" @ $Cities] = ;
    %i = (1.0 + %i);
    %i["LaGenoaAiresNorth" @ $Cities] = ;
    %i = (1.0 + %i);
    %i["RaijukuNorth" @ $Cities] = ;
    %i = (1.0 + %i);
    %i["IIR Raijuku" @ $Cities] = ;
    %i = (1.0 + %i);
    %i["LaBoca Apartments" @ $Cities] = ;
    %i = (1.0 + %i);
    %i["Warehouse Loft" @ $Cities] = ;
    %i = (1.0 + %i);
    %i["BeatUp" @ $Cities] = ;
    $maxCities = %i;
    $cityIndex = 1;
};
initCities();
testLoginTimes();
