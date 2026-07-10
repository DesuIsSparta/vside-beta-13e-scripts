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
    %testLogin.doLogin($DestServerName);
};
testLoginAndStay();
function test::initDances() {
    %i = 0;
    %i = (1.0 + %i);
    %i["hdnc1" @ $Dances] = ;
    %i = (1.0 + %i);
    %i["hdnc2" @ $Dances] = ;
    %i = (1.0 + %i);
    %i["hdnc3" @ $Dances] = ;
    %i = (1.0 + %i);
    %i["hdnc4" @ $Dances] = ;
    %i = (1.0 + %i);
    %i["idnc1" @ $Dances] = ;
    %i = (1.0 + %i);
    %i["idnc2" @ $Dances] = ;
    %i = (1.0 + %i);
    %i["idnc3" @ $Dances] = ;
    %i = (1.0 + %i);
    %i["idnc4" @ $Dances] = ;
    %i = (1.0 + %i);
    %i["pdnc1" @ $Dances] = ;
    %i = (1.0 + %i);
    %i["pdnc2" @ $Dances] = ;
    %i = (1.0 + %i);
    %i["pdnc3" @ $Dances] = ;
    %i = (1.0 + %i);
    %i["pdnc4" @ $Dances] = ;
    %i = (1.0 + %i);
    %i["hdncb1" @ $Dances] = ;
    %i = (1.0 + %i);
    %i["hdncb2" @ $Dances] = ;
    %i = (1.0 + %i);
    %i["hdncb3" @ $Dances] = ;
    %i = (1.0 + %i);
    %i["hdncb4" @ $Dances] = ;
    $DancesCount = %i;
    $DancesAvail = %i;
};
function test::initGenres() {
    %i = 0;
    %i = (1.0 + %i);
    %i["i" @ $Genres] = ;
    %i = (1.0 + %i);
    %i["h" @ $Genres] = ;
    %i = (1.0 + %i);
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
    "Hello from" @ " " @ $Hostname @ ".".say(0, 0);
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
    "I'm doing dance" @ " " @ $dance @ " " @ ".".say(0, 0);
};
function test::getRandomGenre() {
    %num = getRandom(2);
    $genre = "";
    if ((0.0 == %num)) {
        $genre = "i";
    }
    if ((1.0 == %num)) {
        $genre = "h";
    }
    if ((2.0 == %num)) {
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
    if ((0.0 == %num)) {
        $genre = test::getRandomGenre();
        $genre.selectGenre();
    }
    if ((1.0 == %num)) {
        test::doDance();
    }
    if ((2.0 == %num)) {
        test::doWhisper();
    }
    if ((3.0 == %num)) {
        test::doAddBuddy();
    }
    if ((4.0 == %num)) {
        test::doRemoveBuddy();
    }
};
