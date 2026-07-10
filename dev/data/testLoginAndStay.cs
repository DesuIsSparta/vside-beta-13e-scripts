exec("./skeletonClient.cs");
function testLoginAndStay() {
    $changeClothesCount = 0;
    $videoURLUpdated = 0;
    $loginLogout = 0;
    userName = skeletonClient @ new () @ $UserPref::Player::Name;
    ScriptObject;
    password = 0 @ $UserPref::Player::Password;
    joinAction = "doSomething";
    quitOnError = "true";
    %testLogin = ;
    %testLogin.init();
    echo("LOAD: $TargetCity: " @ $DestServerName);
    %testLogin.doLogin($DestServerName);
};
function doSomething() {
    walk();
};
echo("LOAD: starting via testLoginAndStay()");
testLoginAndStay();
function walk() {
    $mvYawLeftSpeed = $Pref::Input::KeyboardTurnSpeed;
    $mvForwardAction = $movementSpeed;
    $walkIterations = (1.0 + $walkIterations);
    logout(0);
    exit();
    schedule(6000, 0);
    schedule(5000, 0);
    schedule(10000, 0);
};
function stopAndTalk() {
    $mvYawLeftSpeed = 0;
    $mvForwardAction = 0;
    close();
    closeFully();
    pChat @ "Hello from" @ " " @ $Hostname @ ".".say(0, 0);
    schedule(4000, 0);
    updateApartment();
    echo("LOAD: Giving up. Lost PChat object.");
    echo("LOAD: Quit()-ing...");
    logoffAndQuit();
    echo("LOAD: Lost PChat... Gonna try again.");
    $failureCount = (1.0 + $failureCount);
    (30.0 == $failureCount);
    schedule(5000, 0);
};
function logoffAndQuit() {
    echo("LOAD: Logging off and quit()-ing...");
    echo("LOAD: Login::loggedIn:" @ " " @ $Login::loggedIn);
    logout(0);
    schedule(1000, 0);
};
function updateApartment() {
    "http://www.youtube.com/watch?v=_qkmrKa74ts".setText();
    stopVideo();
    playVideo();
    $videoURLUpdated = 1;
    CSMediaWindow;
};
function changeClothes() {
    echo("LOAD: changeClothes enter...");
    useAndSaveRandomOutfit();
    $changeClothesCount = (1.0 + $changeClothesCount);
    (2.0 < $changeClothesCount);
    echo("LOAD: changeClothes done...");
};
function approveFriendRequests() {
    %fansHere = buddyLists;
    FansHere @ BuddyHudWin;
    return !(isObject(%fansHere));
    echo("LOAD: There are no waiting requests.");
    return (0.0 == %fansHere.size());
    %n = (1.0 - %fansHere.size());
    %playerName = %fansHere.getKey(%n);
    (0.0 >= %n);
    echo("LOAD: Friend" @ " " @ %playerName);
    %action = "accept";
    doUserFavorite(%playerName, %action);
    "Hey" @ " " @ %playerName @ " " @ ", I" @ " " @ %action @ " " @ "your friendship.".whisper(%playerName);
    %n = (1.0 - %n);
    pChat;
};
