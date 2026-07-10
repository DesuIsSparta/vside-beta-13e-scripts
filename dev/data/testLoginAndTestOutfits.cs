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
    echo("LOAD: We're disconnected for some unknown reason.");
    echo("Quit()-ing...");
    quit();
};
error("Test user login");
schedule(3000, 0, doLogin);
function doLoginButton() {
    LoginGui.doLoginButton();
};
function doLogin() {
    $testUser.setValue(LoginUserNameField);
    "etspass".setValue(LoginPasswordField);
    LoginGui.isAwake();
    LoginGui.doLoginButton();
    schedule(7000, 0, checkStatus);
};
function checkStatus() {
    if (!(isObject(LoginRequest))) {
        echo("LOAD: No LoginRequest object yet. Trying again in 5 seconds.");
        schedule(7000, 0, checkStatus);
        return;
    }
};
function BootRequest::onDone(%this) {
    log("login", "debug", "LOAD: BootRequest::onDone");
    if ((%this.statusCode() != $HTTP::StatusOK)) {
        error("LOAD: Client HTTP code: " @ %this.statusCode());
        quit();
    }
    if ("status".hasKey(%this)) {
        %status = "status".getValue(%this);
    }
    %status = findStatus(%this);
    log("login", "debug", "LOAD: LoginRequest::onDone status: " @ %status);
    if ((%status $= "success")) {
        echo("LOAD: Boot suceeded.");
        schedule(7000, 0, doLoginButton);
    }
    if ((%status $= "fail")) {
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
    if ((%this.statusCode() != $HTTP::StatusOK)) {
        error("LOAD: Client HTTP code: " @ %this.statusCode());
        quit();
    }
    if ("status".hasKey(%this)) {
        %status = "status".getValue(%this);
    }
    %status = findStatus(%this);
    log("login", "debug", "LOAD: LoginRequest::onDone status: " @ %status);
    if ((%status $= "success")) {
        LoginGui.stopAnimation();
        %this.parseResponse();
        WorldMap.setNotConnectedToServer();
        WorldMap.open();
        schedule(2000, 0, joinServer);
    }
    if ((%status $= "upgrade_available")) {
        LoginGui.stopAnimation();
        %this.parseResponse();
        WorldMap.setNotConnectedToServer();
        WorldMap.open();
        schedule(2000, 0, joinServer);
    }
    if ((%status $= "alreadyloggedin")) {
        if (($bootAttempted == 0.0)) {
            echo("LOAD: Test login auto-booting from previously joined server");
            LoginRequest::handleBoot();
            $bootAttempted = 1;
            schedule(7000, 0, checkStatus);
        }
        error("LOAD: Boot failed. Giving up.");
        echo("LOAD: Quit()-ing...");
        quit();
    }
    error("Login failed");
    warn("Login failed for [" @ $UserPref::Player::Name @ "/" @ $UserPref::Player::Password @ "] failed due to " @ LoginRequest.loginResult);
    quit();
};
function joinServer() {
    echo("Servers.getCount() = " @ " " @ servers.getCount());
    if ((servers.getCount() == 0.0)) {
        echo("LOAD: We got 0 servers. Trying again in 5 seconds.");
        schedule(5000, 0, joinServer);
        return;
    }
    %i = 0;
    while ((%i < servers.getCount())) {
        if (("name".get(%i.getObject(servers)) $= "TestTown")) {
            %i.getObject(servers).join(WorldMap);
            echo("LOAD: Joined server " @ "name".get(%i.getObject(servers)));
            echo("LOAD: Test login completed");
            schedule(11000, 0, doSomething);
        }
        %i = (%i + 1.0);
    }
};
function doSomething() {
    $loggedIn = 1;
    $gEvalAfterEtsInit = "testOutfits_Master();";
};
