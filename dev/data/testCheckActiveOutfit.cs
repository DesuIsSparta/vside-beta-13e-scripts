$UserPref::Audio::keepMusicHudOpen = 0;
$loggedIn = 0;
$failureCount = 0;
$bootAttempted = 0;
$System::ID = "fakeMacAddress.com";
function GameConnection::onConnectionDropped(%this, %msg)
{
    echo("The server has dropped the connection: " @ %msg);
    if ($loggedIn)
    {
        echo("Quit()-ing...");
        quit();
    }
    return ;
}
function GameConnection::onServerConnectionTimedOut(%this)
{
    error("We\'re disconnected for some unknown reason.");
    quit();
    return ;
}
error("Test user login");
schedule(3000, 0, doLogin);
function doLoginButton()
{
    LoginGui.doLoginButton();
    return ;
}
function doLogin()
{
    echo("login as " @ $testUser);
    LoginUserNameField.setValue($testUser);
    LoginPasswordField.setValue("etspass");
    LoginGui.isAwake();
    LoginGui.doLoginButton();
    schedule(7000, 0, checkStatus);
    return ;
}
function checkStatus()
{
    if (!isObject(LoginRequest))
    {
        echo("LOAD: No LoginRequest object yet. Trying again in 5 seconds.");
        schedule(7000, 0, checkStatus);
        return ;
    }
    return ;
}
function BootRequest::onDone(%this)
{
    log("login", "debug", "LOAD: BootRequest::onDone");
    if (%this.statusCode() != $HTTP::StatusOK)
    {
        error("LOAD: Client HTTP code: " @ %this.statusCode());
        quit();
    }
    if (%this.hasKey("status"))
    {
        %status = %this.getValue("status");
    }
    else
    {
        %status = findStatus(%this);
    }
    log("login", "debug", "LOAD: LoginRequest::onDone status: " @ %status);
    if (%status $= "success")
    {
        echo("LOAD: Boot suceeded.");
        schedule(7000, 0, doLoginButton);
    }
    else
    {
        if (%status $= "fail")
        {
            echo("LOAD: Boot failed.");
            quit();
        }
        else
        {
            if (%status $= "error")
            {
                echo("LOAD: Boot errored.");
                quit();
            }
        }
    }
    return ;
}
function LoginRequest::onDone(%this)
{
    log("login", "debug", "LOAD: LoginRequest::onDone");
    if (%this.statusCode() != $HTTP::StatusOK)
    {
        error("LOAD: Client HTTP code: " @ %this.statusCode());
        quit();
    }
    if (%this.hasKey("status"))
    {
        %status = %this.getValue("status");
    }
    else
    {
        %status = findStatus(%this);
    }
    log("login", "debug", "LOAD: LoginRequest::onDone status: " @ %status);
    if (%status $= "success")
    {
        LoginGui.stopAnimation();
        %this.parseResponse();
        WorldMap.setNotConnectedToServer();
        WorldMap.open();
        schedule(2000, 0, joinServer);
    }
    else
    {
        if (%status $= "upgrade_available")
        {
            LoginGui.stopAnimation();
            %this.parseResponse();
            WorldMap.setNotConnectedToServer();
            WorldMap.open();
            schedule(2000, 0, joinServer);
        }
        else
        {
            if (%status $= "alreadyloggedin")
            {
                if ($bootAttempted == 0)
                {
                    echo("LOAD: Test login auto-booting from previously joined server");
                    LoginRequest::handleBoot();
                    $bootAttempted = 1;
                    schedule(7000, 0, checkStatus);
                }
                else
                {
                    error("LOAD: Boot failed. Giving up.");
                    echo("LOAD: Quit()-ing...");
                    quit();
                }
            }
            else
            {
                error("Login failed");
                warn("Login failed for [" @ $UserPref::Player::Name @ "/" @ $UserPref::Player::Password @ "] failed due to " @ LoginRequest.loginResult);
                quit();
            }
        }
    }
    return ;
}
function joinServer()
{
    echo("Servers.getCount() = " SPC servers.getCount());
    if (servers.getCount() == 0)
    {
        echo("LOAD: We got 0 servers. Trying again in 5 seconds.");
        schedule(5000, 0, joinServer);
        return ;
    }
    %i = 0;
    while (%i < servers.getCount())
    {
        if (servers.getObject(%i).get("name") $= "TestTown")
        {
            WorldMap.join(servers.getObject(%i));
            echo("LOAD: Joined server " @ servers.getObject(%i).get("name"));
            echo("LOAD: Test login completed");
            schedule(11000, 0, doSomething);
            break;
        }
        %i = %i + 1;
    }
}

function checkActiveOutfit()
{
    error("player.activeSkus = " @ $player.getActiveSKUs());
    logout(0);
    quit();
    return ;
}
function doSomething()
{
    $loggedIn = 1;
    $gEvalAfterEtsInit = "schedule(10000, 0, checkActiveOutfit);";
    return ;
}
