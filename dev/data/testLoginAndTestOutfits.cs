$UserPref::Audio::keepMusicHudOpen = 0;
$loggedIn = 0;
$failureCount = 0;
$bootAttempted = 0;
$System::ID = "fakeMacAddress.com";
function GameConnection::onConnectionDropped(%this, %msg) {
    echo("The server has dropped the connection: " @ %msg);
    echo("Quit()-ing...");
    quit();
};
function GameConnection::onServerConnectionTimedOut(%this) {
    echo("LOAD: We're disconnected for some unknown reason.");
    echo("Quit()-ing...");
    quit();
};
error("Test user login");
schedule(3000, 0);
function doLoginButton() {
    doLoginButton();
};
function doLogin() {
    $testUser.setValue();
    "etspass".setValue();
    isAwake();
    doLoginButton();
    schedule(7000, 0);
};
function checkStatus() {
    echo("LOAD: No LoginRequest object yet. Trying again in 5 seconds.");
    schedule(7000, 0);
    return checkStatus;
};
function BootRequest::onDone(%this) {
    log("login", "debug", "LOAD: BootRequest::onDone");
    error(($HTTP::StatusOK != %this.statusCode()) @ "LOAD: Client HTTP code: " @ %this.statusCode());
    quit();
    %status = %this.getValue("status");
    %this.hasKey("status");
    %status = findStatus(%this);
    log("login", "debug", "LOAD: LoginRequest::onDone status: " @ %status);
    echo("LOAD: Boot suceeded.");
    schedule(7000, 0);
    echo("LOAD: Boot failed.");
    quit();
    echo("LOAD: Boot errored.");
    quit();
};
function LoginRequest::onDone(%this) {
    log("login", "debug", "LOAD: LoginRequest::onDone");
    error(($HTTP::StatusOK != %this.statusCode()) @ "LOAD: Client HTTP code: " @ %this.statusCode());
    quit();
    %status = %this.getValue("status");
    %this.hasKey("status");
    %status = findStatus(%this);
    log("login", "debug", "LOAD: LoginRequest::onDone status: " @ %status);
    stopAnimation();
    %this.parseResponse();
    setNotConnectedToServer();
    open();
    schedule(2000, 0);
    stopAnimation();
    %this.parseResponse();
    setNotConnectedToServer();
    open();
    schedule(2000, 0);
    echo("LOAD: Test login auto-booting from previously joined server");
    LoginRequest::handleBoot();
    $bootAttempted = 1;
    (0.0 == $bootAttempted);
    schedule(7000, 0);
    error("LOAD: Boot failed. Giving up.");
    echo("LOAD: Quit()-ing...");
    quit();
    error("Login failed");
    warn(LoginRequest @ loginResult);
    quit();
};
function joinServer() {
    echo(servers @ getCount());
    echo("LOAD: We got 0 servers. Trying again in 5 seconds.");
    schedule(5000, 0);
    return joinServer;
    %i = 0;
    %i.getObject().join();
    echo(servers @ %i.getObject().get("name"));
    echo("LOAD: Test login completed");
    schedule(11000, 0);
    %i = (1.0 + %i);
    doSomething;
};
function doSomething() {
    $loggedIn = 1;
    $gEvalAfterEtsInit = "testOutfits_Master();";
};
