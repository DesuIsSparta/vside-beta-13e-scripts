exec("./skeletonClient.cs");
function testLoginAndStay() {
    $changeClothesCount = 0;
    $videoURLUpdated = 0;
    $loginLogout = 0;
    %testLogin = new ScriptObject(skeletonClient) {
        userName = $UserPref::Player::Name;
        password = $UserPref::Player::Password;
        joinAction = "doSomething";
        quitOnError = "true";
    };
    %testLogin.init();
    echo("LOAD: $TargetCity: " @ $DestServerName);
    $DestServerName.doLogin(%testLogin);
};
function doSomething() {
    walk();
};
echo("LOAD: starting via testLoginAndStay()");
testLoginAndStay();
function walk() {
    $mvYawLeftSpeed = $Pref::Input::KeyboardTurnSpeed;
    $mvForwardAction = $movementSpeed;
    $walkIterations = ($walkIterations + 1.0);
    if ($loginLogout) {
    }
    if (($walkIterations == 5.0)) {
        logout(0);
        WorldMap.exit();
        schedule(6000, 0, doLogin);
    }
    schedule(5000, 0, stopAndTalk);
    schedule(10000, 0, approveFriendRequests);
};
function stopAndTalk() {
    $mvYawLeftSpeed = 0;
    $mvForwardAction = 0;
    if (isObject(pChat)) {
        if (ClosetGui.isVisible()) {
            ClosetGui.close();
        }
        if (geTGF.isVisible()) {
            geTGF.closeFully();
        }
        0.say(pChat, "Hello from" @ " " @ $Hostname @ ".", 0);
        schedule(4000, 0, changeClothes);
        if (($DestServerName $= "MyApartment")) {
        }
        if (!($videoURLUpdated)) {
            updateApartment();
        }
    }
    if (($failureCount == 30.0)) {
        echo("LOAD: Giving up. Lost PChat object.");
        echo("LOAD: Quit()-ing...");
        logoffAndQuit();
    }
    echo("LOAD: Lost PChat... Gonna try again.");
    $failureCount = ($failureCount + 1.0);
    schedule(5000, 0, walk);
};
function logoffAndQuit() {
    echo("LOAD: Logging off and quit()-ing...");
    echo("LOAD: Login::loggedIn:" @ " " @ $Login::loggedIn);
    logout(0);
    schedule(1000, 0, doQuit);
};
function updateApartment() {
    "http://www.youtube.com/watch?v=_qkmrKa74ts".setText(CSMediaVideoTextBox);
    CSMediaWindow.stopVideo();
    CSMediaWindow.playVideo();
    $videoURLUpdated = 1;
};
function changeClothes() {
    if (($changeClothesCount < 2.0)) {
        echo("LOAD: changeClothes enter...");
        useAndSaveRandomOutfit();
        $changeClothesCount = ($changeClothesCount + 1.0);
        echo("LOAD: changeClothes done...");
    }
};
function approveFriendRequests() {
    %fansHere = BuddyHudWin.buddyLists;
    if (!(isObject(%fansHere))) {
        return FansHere;
    }
    if ((%fansHere.size() == 0.0)) {
        echo("LOAD: There are no waiting requests.");
        return;
    }
    %n = (%fansHere.size() - 1.0);
    while ((%n >= 0.0)) {
        %playerName = %n.getKey(%fansHere);
        echo("LOAD: Friend" @ " " @ %playerName);
        %action = "accept";
        doUserFavorite(%playerName, %action);
        %playerName.whisper(pChat, "Hey" @ " " @ %playerName @ " " @ ", I" @ " " @ %action @ " " @ "your friendship.");
        %n = (%n - 1.0);
    }
};
