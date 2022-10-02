exec("./skeletonClient.cs");
function testLoginAndStay()
{
    $changeClothesCount = 0;
    $videoURLUpdated = 0;
    $loginLogout = 0;
    %testLogin = new ScriptObject(skeletonClient)
    {
        userName = $UserPref::Player::Name;
        password = $UserPref::Player::Password;
        joinAction = "doSomething";
        quitOnError = "true";
    };
    %testLogin.init();
    echo("LOAD: $TargetCity: " @ $DestServerName);
    %testLogin.doLogin($DestServerName);
    return ;
}
function doSomething()
{
    walk();
    return ;
}
echo("LOAD: starting via testLoginAndStay()");
testLoginAndStay();
function walk()
{
    $mvYawLeftSpeed = $Pref::Input::KeyboardTurnSpeed;
    $mvForwardAction = $movementSpeed;
    $walkIterations = $walkIterations + 1;
    if ($loginLogout && ($walkIterations == 5))
    {
        logout(0);
        WorldMap.exit();
        schedule(6000, 0, doLogin);
    }
    else
    {
        schedule(5000, 0, stopAndTalk);
        schedule(10000, 0, approveFriendRequests);
    }
    return ;
}
function stopAndTalk()
{
    $mvYawLeftSpeed = 0;
    $mvForwardAction = 0;
    if (isObject(pChat))
    {
        if (ClosetGui.isVisible())
        {
            ClosetGui.close();
        }
        if (geTGF.isVisible())
        {
            geTGF.closeFully();
        }
        pChat.say("Hello from" SPC $Hostname @ ".", 0, 0);
        schedule(4000, 0, changeClothes);
        if (($DestServerName $= "MyApartment") && !$videoURLUpdated)
        {
            updateApartment();
        }
    }
    else
    {
        if ($failureCount == 30)
        {
            echo("LOAD: Giving up. Lost PChat object.");
            echo("LOAD: Quit()-ing...");
            logoffAndQuit();
        }
        else
        {
            echo("LOAD: Lost PChat... Gonna try again.");
            $failureCount = $failureCount + 1;
        }
    }
    schedule(5000, 0, walk);
    return ;
}
function logoffAndQuit()
{
    echo("LOAD: Logging off and quit()-ing...");
    echo("LOAD: Login::loggedIn:" SPC $Login::loggedIn);
    logout(0);
    schedule(1000, 0, doQuit);
    return ;
}
function updateApartment()
{
    CSMediaVideoTextBox.setText("http://www.youtube.com/watch?v=_qkmrKa74ts");
    CSMediaWindow.stopVideo();
    CSMediaWindow.playVideo();
    $videoURLUpdated = 1;
    return ;
}
function changeClothes()
{
    if ($changeClothesCount < 2)
    {
        echo("LOAD: changeClothes enter...");
        useAndSaveRandomOutfit();
        $changeClothesCount = $changeClothesCount + 1;
        echo("LOAD: changeClothes done...");
    }
    return ;
}
function approveFriendRequests()
{
    %fansHere = BuddyHudWin.buddyLists[FansHere];
    if (!isObject(%fansHere))
    {
        return ;
    }
    if (%fansHere.size() == 0)
    {
        echo("LOAD: There are no waiting requests.");
        return ;
    }
    %n = %fansHere.size() - 1;
    while (%n >= 0)
    {
        %playerName = %fansHere.getKey(%n);
        echo("LOAD: Friend" SPC %playerName);
        %action = "accept";
        doUserFavorite(%playerName, %action);
        pChat.whisper("Hey" SPC %playerName SPC ", I" SPC %action SPC "your friendship.", %playerName);
        %n = %n - 1;
    }
}


