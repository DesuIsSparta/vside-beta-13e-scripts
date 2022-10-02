exec("./skeletonClient.cs");
function testLoginAndStay()
{
    $loginLogout = 0;
    %testLogin = new ScriptObject(skeletonClient)
    {
        userName = $UserPref::Player::Name;
        password = $UserPref::Player::Password;
        joinAction = "walk";
        quitOnError = "true";
    };
    %testLogin.init();
    echo("LOAD: $TargetCity: " @ $DestServerName);
    %testLogin.doLogin($DestServerName);
    return ;
}
testLoginAndStay();
function test::initDances()
{
    %i = 0;
    $Dances[%i = %i + 1] = "hdnc1";
    $Dances[%i = %i + 1] = "hdnc2";
    $Dances[%i = %i + 1] = "hdnc3";
    $Dances[%i = %i + 1] = "hdnc4";
    $Dances[%i = %i + 1] = "idnc1";
    $Dances[%i = %i + 1] = "idnc2";
    $Dances[%i = %i + 1] = "idnc3";
    $Dances[%i = %i + 1] = "idnc4";
    $Dances[%i = %i + 1] = "pdnc1";
    $Dances[%i = %i + 1] = "pdnc2";
    $Dances[%i = %i + 1] = "pdnc3";
    $Dances[%i = %i + 1] = "pdnc4";
    $Dances[%i = %i + 1] = "hdncb1";
    $Dances[%i = %i + 1] = "hdncb2";
    $Dances[%i = %i + 1] = "hdncb3";
    $Dances[%i = %i + 1] = "hdncb4";
    $DancesCount = %i;
    $DancesAvail = %i;
    return ;
}
function test::initGenres()
{
    %i = 0;
    $Genres[%i = %i + 1] = "i";
    $Genres[%i = %i + 1] = "h";
    $Genres[%i = %i + 1] = "p";
    $GenresCount = %i;
    $GenresAvail = %i;
    return ;
}
test::initDances();
test::initGenres();
function walk()
{
    $mvYawLeftSpeed = $Pref::Input::KeyboardTurnSpeed;
    $mvForwardAction = $movementSpeed;
    schedule(1000, 0, stopAndTalk);
    return ;
}
function stopAndTalk()
{
    $mvYawLeftSpeed = 0;
    $mvForwardAction = 0;
    pChat.say("Hello from" SPC $Hostname @ ".", 0, 0);
    schedule(1000, 0, stopAndDance);
    return ;
}
function stopAndDance()
{
    $mvYawLeftSpeed = 0;
    $mvForwardAction = 0;
    doAction();
    schedule(5000, 0, walk);
    return ;
}
function test::doDance()
{
    %danceNum = getRandom($DancesCount);
    $dance = $Dances[%danceNum];
    commandToServer('EtsPlayAnimName', $dance);
    pChat.say("I\'m doing dance" SPC $dance SPC ".", 0, 0);
    return ;
}
function test::getRandomGenre()
{
    %num = getRandom(2);
    $genre = "";
    if (%num == 0)
    {
        $genre = "i";
    }
    else
    {
        if (%num == 1)
        {
            $genre = "h";
        }
        else
        {
            if (%num == 2)
            {
                $genre = "p";
            }
        }
    }
    return $genre;
}
function test::doWhisper()
{
    doUserWhisper($BestFriend, "Hey Beautiful!", 0);
    return ;
}
function test::doAddBuddy()
{
    doUserFavorite($BestFriend, "add");
    return ;
}
function test::doRemoveBuddy()
{
    doUserFavorite($BestFriend, "remove");
    return ;
}
function doAction()
{
    %num = getRandom(4);
    if (%num == 0)
    {
        $genre = test::getRandomGenre();
        ClosetGui.selectGenre($genre);
    }
    else
    {
        if (%num == 1)
        {
            test::doDance();
        }
        else
        {
            if (%num == 2)
            {
                test::doWhisper();
            }
            else
            {
                if (%num == 3)
                {
                    test::doAddBuddy();
                }
                else
                {
                    if (%num == 4)
                    {
                        test::doRemoveBuddy();
                    }
                }
            }
        }
    }
    return ;
}
