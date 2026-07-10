exec("./skeletonClient.cs");
function testLoginAndStay() {
    $loginLogout = 0;
    userName = skeletonClient @ new () @ $UserPref::Player::Name;
    ScriptObject;
    password = 0 @ $UserPref::Player::Password;
    joinAction = "walk";
    quitOnError = "true";
    %testLogin = ;
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
    pChat @ "Hello from" @ " " @ $Hostname @ ".".say(0, 0);
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
    $genre = "i";
    (0.0 == %num);
    $genre = "h";
    (1.0 == %num);
    $genre = "p";
    (2.0 == %num);
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
    $genre = test::getRandomGenre();
    (0.0 == %num);
    $genre.selectGenre();
    test::doDance();
    test::doWhisper();
    test::doAddBuddy();
    test::doRemoveBuddy();
};
