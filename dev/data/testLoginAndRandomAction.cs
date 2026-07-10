exec("./skeletonClient.cs");
function testLoginAndStay() {
    $loginLogout = 0;
    %testLogin = new ScriptObject(skeletonClient) {
        userName = $UserPref::Player::Name;
        password = $UserPref::Player::Password;
        joinAction = "walk";
        quitOnError = "true";
    };
    %testLogin.init();
    echo("LOAD: $TargetCity: " @ $DestServerName);
    $DestServerName.doLogin(%testLogin);
};
testLoginAndStay();
function test::initDances() {
    %i = 0;
    %i = (%i + 1.0);
    %i["hdnc1" @ $Dances] = ;
    %i = (%i + 1.0);
    %i["hdnc2" @ $Dances] = ;
    %i = (%i + 1.0);
    %i["hdnc3" @ $Dances] = ;
    %i = (%i + 1.0);
    %i["hdnc4" @ $Dances] = ;
    %i = (%i + 1.0);
    %i["idnc1" @ $Dances] = ;
    %i = (%i + 1.0);
    %i["idnc2" @ $Dances] = ;
    %i = (%i + 1.0);
    %i["idnc3" @ $Dances] = ;
    %i = (%i + 1.0);
    %i["idnc4" @ $Dances] = ;
    %i = (%i + 1.0);
    %i["pdnc1" @ $Dances] = ;
    %i = (%i + 1.0);
    %i["pdnc2" @ $Dances] = ;
    %i = (%i + 1.0);
    %i["pdnc3" @ $Dances] = ;
    %i = (%i + 1.0);
    %i["pdnc4" @ $Dances] = ;
    %i = (%i + 1.0);
    %i["hdncb1" @ $Dances] = ;
    %i = (%i + 1.0);
    %i["hdncb2" @ $Dances] = ;
    %i = (%i + 1.0);
    %i["hdncb3" @ $Dances] = ;
    %i = (%i + 1.0);
    %i["hdncb4" @ $Dances] = ;
    $DancesCount = %i;
    $DancesAvail = %i;
};
function test::initGenres() {
    %i = 0;
    %i = (%i + 1.0);
    %i["i" @ $Genres] = ;
    %i = (%i + 1.0);
    %i["h" @ $Genres] = ;
    %i = (%i + 1.0);
    %i["p" @ $Genres] = ;
    $GenresCount = %i;
    $GenresAvail = %i;
};
test::initDances();
test::initGenres();
function walk() {
    $mvYawLeftSpeed = $Pref::Input::KeyboardTurnSpeed;
    $mvForwardAction = $movementSpeed;
    schedule(1000, 0);
};
function stopAndTalk() {
    $mvYawLeftSpeed = 0;
    $mvForwardAction = 0;
    0.say(pChat, "Hello from" @ " " @ $Hostname @ ".", 0);
    schedule(1000, 0);
};
function stopAndDance() {
    $mvYawLeftSpeed = 0;
    $mvForwardAction = 0;
    doAction();
    schedule(5000, 0);
};
function test::doDance() {
    %danceNum = getRandom($DancesCount);
    $dance = %danceNum[$Dances @ %danceNum];
    commandToServer('EtsPlayAnimName', $dance);
    0.say(pChat, "I'm doing dance" @ " " @ $dance @ " " @ ".", 0);
};
function test::getRandomGenre() {
    %num = getRandom(2);
    $genre = "";
    if ((%num == 0.0)) {
        $genre = "i";
    }
    if ((%num == 1.0)) {
        $genre = "h";
    }
    if ((%num == 2.0)) {
        $genre = "p";
    }
    return $genre;
};
function test::doWhisper() {
    doUserWhisper($BestFriend, "Hey Beautiful!", 0);
};
function test::doAddBuddy() {
    doUserFavorite($BestFriend, "add");
};
function test::doRemoveBuddy() {
    doUserFavorite($BestFriend, "remove");
};
function doAction() {
    %num = getRandom(4);
    if ((%num == 0.0)) {
        $genre = test::getRandomGenre();
        $genre.selectGenre(ClosetGui);
    }
    if ((%num == 1.0)) {
        test::doDance();
    }
    if ((%num == 2.0)) {
        test::doWhisper();
    }
    if ((%num == 3.0)) {
        test::doAddBuddy();
    }
    if ((%num == 4.0)) {
        test::doRemoveBuddy();
    }
};
