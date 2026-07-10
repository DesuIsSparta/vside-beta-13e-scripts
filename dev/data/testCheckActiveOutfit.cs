$UserPref::Audio::keepMusicHudOpen = 0;
$loggedIn = 0;
$failureCount = 0;
$bootAttempted = 0;
$System::ID = "fakeMacAddress.com";
function GameConnection::onConnectionDropped(%this, %msg) {
    echo("The server has dropped the connection: " @ %msg);
    if ($loggedIn) {
        echo("Quit()-ing...");
        quit();
    }
};
function GameConnection::onServerConnectionTimedOut(%this) {
    error("We're disconnected for some unknown reason.");
    quit();
};
error("Test user login");
schedule(3000, 0);
function doLoginButton() {
    doLoginButton();
};
function doLogin() {
    echo("login as " @ $testUser);
    $testUser.setValue();
    "etspass".setValue();
    isAwake();
    doLoginButton();
    schedule(7000, 0);
};
function checkStatus() {
    if (!(isObject())) {
        echo("LOAD: No LoginRequest object yet. Trying again in 5 seconds.");
        schedule(7000, 0);
        return checkStatus;
    }
};
function BootRequest::onDone(%this) {
    log("login", "debug", "LOAD: BootRequest::onDone");
    if (($HTTP::StatusOK != %this.statusCode())) {
        error("LOAD: Client HTTP code: " @ %this.statusCode());
        quit();
    }
    if (%this.hasKey("status")) {
        %status = %this.getValue("status");
    }
    %status = findStatus(%this);
    log("login", "debug", "LOAD: LoginRequest::onDone status: " @ %status);
    if ((%status $= "success")) {
        echo("LOAD: Boot suceeded.");
        schedule(7000, 0);
    }
    if ((doLoginButton SPC %status $= "fail")) {
        echo("LOAD: Boot failed.");
        quit();
    }
    if ((%status $= "error")) {
        echo("LOAD: Boot errored.");
        quit();
    }
};
function LoginRequest::onDone(%this) {
    log("login", "debug", "LOAD: LoginRequest::onDone");
    if (($HTTP::StatusOK != %this.statusCode())) {
        error("LOAD: Client HTTP code: " @ %this.statusCode());
        quit();
    }
    if (%this.hasKey("status")) {
        %status = %this.getValue("status");
    }
    %status = findStatus(%this);
    log("login", "debug", "LOAD: LoginRequest::onDone status: " @ %status);
    if ((%status $= "success")) {
        stopAnimation();
        %this.parseResponse();
        setNotConnectedToServer();
        open();
        schedule(2000, 0);
    }
    if ((joinServer SPC %status $= "upgrade_available")) {
        stopAnimation();
        %this.parseResponse();
        setNotConnectedToServer();
        open();
        schedule(2000, 0);
    }
    if ((joinServer SPC %status $= "alreadyloggedin")) {
        if ((0.0 == $bootAttempted)) {
            echo("LOAD: Test login auto-booting from previously joined server");
            LoginRequest::handleBoot();
            $bootAttempted = 1;
            WorldMap;
            schedule(7000, 0);
        }
        error("LOAD: Boot failed. Giving up.");
        echo("LOAD: Quit()-ing...");
        quit();
    }
    error("Login failed");
    warn(LoginRequest @ loginResult);
    quit();
};
function joinServer() {
    echo(servers @ getCount());
    if ((servers == getCount())) {
        echo("LOAD: We got 0 servers. Trying again in 5 seconds.");
        schedule(5000, 0);
        return joinServer;
    }
    %i = 0;
    if ((getCount() < %i)) {
        if ((servers SPC %i.getObject().get("name") $= "TestTown")) {
            %i.getObject().join();
            echo(servers @ %i.getObject().get("name"));
            echo("LOAD: Test login completed");
            schedule(11000, 0);
        }
        %i = (1.0 + %i);
        doSomething;
    }
};
function checkActiveOutfit() {
    error("player.activeSkus = " @ $player.getActiveSKUs());
    logout(0);
    quit();
};
function doSomething() {
    $loggedIn = 1;
    $gEvalAfterEtsInit = "schedule(10000, 0, checkActiveOutfit);";
};
