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
    %i[$Dances @ %i = (%i + 1.0)] = "hdnc1";
    %i[$Dances @ %i = (%i + 1.0)] = "hdnc2";
    %i[$Dances @ %i = (%i + 1.0)] = "hdnc3";
    %i[$Dances @ %i = (%i + 1.0)] = "hdnc4";
    %i[$Dances @ %i = (%i + 1.0)] = "idnc1";
    %i[$Dances @ %i = (%i + 1.0)] = "idnc2";
    %i[$Dances @ %i = (%i + 1.0)] = "idnc3";
    %i[$Dances @ %i = (%i + 1.0)] = "idnc4";
    %i[$Dances @ %i = (%i + 1.0)] = "pdnc1";
    %i[$Dances @ %i = (%i + 1.0)] = "pdnc2";
    %i[$Dances @ %i = (%i + 1.0)] = "pdnc3";
    %i[$Dances @ %i = (%i + 1.0)] = "pdnc4";
    %i[$Dances @ %i = (%i + 1.0)] = "hdncb1";
    %i[$Dances @ %i = (%i + 1.0)] = "hdncb2";
    %i[$Dances @ %i = (%i + 1.0)] = "hdncb3";
    %i[$Dances @ %i = (%i + 1.0)] = "hdncb4";
    $DancesCount = %i;
    $DancesAvail = %i;
};
function test::initGenres() {
    %i = 0;
    %i[$Genres @ %i = (%i + 1.0)] = "i";
    %i[$Genres @ %i = (%i + 1.0)] = "h";
    %i[$Genres @ %i = (%i + 1.0)] = "p";
    $GenresCount = %i;
    $GenresAvail = %i;
};
test::initDances();
test::initGenres();
function walk() {
    $mvYawLeftSpeed = $Pref::Input::KeyboardTurnSpeed;
    $mvForwardAction = $movementSpeed;
    schedule(1000, 0, stopAndTalk);
};
function stopAndTalk() {
    $mvYawLeftSpeed = 0;
    $mvForwardAction = 0;
    0.say(pChat, "Hello from" @ " " @ $Hostname @ ".", 0);
    schedule(1000, 0, stopAndDance);
};
function stopAndDance() {
    $mvYawLeftSpeed = 0;
    $mvForwardAction = 0;
    doAction();
    schedule(5000, 0, walk);
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
