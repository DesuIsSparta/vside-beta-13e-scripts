$httpObjTestRequest = 0;
function httpObjTest::init() {
    %httpObj = new HTTPObject(httpObjTestRequest);
    gotEOF = 0 @ %httpObj;
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
    if (!(%client.hasPlayerObjectAndPermission_Warn("debugActive"))) {
        return;
    }
    testHTTPObjectReal(%client);
};
function httpObjTestRequest::onLine(%this, %line) {
    if ((%line $= "EOF")) {
        gotEOF = 1 @ %this;
    }
    numLines = (%this + numLines);
    1.0;
    numChars = (%this + numChars);
    strlen(%line);
    log("network", "debug", "HTTPObjTestRequest::onLine:" @ " " @ %line);
};
function httpObjTestRequest::onDisconnect(%this) {
    %wwo = gotEOF ? "with" : "without";
    %this;
    %lvl = gotEOF ? "debug" : "error";
    %this;
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
    if (isObject(requestingClient)) {
        admin::doSystemMessagePlayer(Player, %line, 'MsgInfoMessage');
    }
};
