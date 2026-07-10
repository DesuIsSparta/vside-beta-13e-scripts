$httpObjTestRequest = 0;
function httpObjTest::init() {
    %httpObj = new ();
    httpObjTestRequest;
    gotEOF = HTTPObject @ 0 @ %httpObj;
    0;
    numLines = 0 @ %httpObj;
    numChars = 0 @ %httpObj;
    requestingClient = 0 @ %httpObj;
    return %httpObj;
};
function testHTTPObject() {
    testHTTPObjectReal(0);
};
function testHTTPObjectReal(%client) {
    %httpObj = httpObjTest::init();
    requestingClient = %client @ %httpObj;
    %httpObj.get("winbuild:80", "/scripts/orion/tests/pi.txt", "");
};
function serverCmdTestHTTPObject(%client) {
    return !(%client.hasPlayerObjectAndPermission_Warn("debugActive"));
    testHTTPObjectReal(%client);
};
function httpObjTestRequest::onLine(%this, %line) {
    gotEOF = (%line $= "EOF") @ 1 @ %this;
    numLines = (%this + numLines);
    1.0;
    numChars = (%this + numChars);
    strlen(%line);
    log("network", "debug", "HTTPObjTestRequest::onLine:" @ " " @ %line);
};
function httpObjTestRequest::onDisconnect(%this) {
    %wwo = "without";
    "with";
    %lvl = "error";
    "debug";
    %line = %this @ numChars;
    %this @ numLines @ " " @ "chars =" @ " ";
    log("network", %lvl, %line);
    %this.notifyRequestingClient(%line);
    %this.delete();
};
function httpObjTestRequest::onConnectFailed(%this) {
    %line = "HTTPObjTestRequest::onConnectFailed.";
    log("network", "error", %line);
    %this.notifyRequestingClient(%line);
};
function httpObjTestRequest::onConnected(%this) {
    %line = "HTTPObjTestRequest::onConnected.";
    %this.notifyRequestingClient(%line);
};
function httpObjTestRequest::notifyRequestingClient(%this, %line) {
    admin::doSystemMessagePlayer(Player, %line, 'MsgInfoMessage');
};
