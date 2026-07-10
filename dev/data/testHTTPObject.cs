$httpObjTestRequest = 0;
function httpObjTest::init() {
    %httpObj = new HTTPObject(httpObjTestRequest);;
    %httpObj.gotEOF = 0;
    %httpObj.numLines = 0;
    %httpObj.numChars = 0;
    %httpObj.requestingClient = 0;
    return %httpObj;
};
function testHTTPObject() {
    testHTTPObjectReal(0);
};
function testHTTPObjectReal(%client) {
    %httpObj = httpObjTest::init();
    %httpObj.requestingClient = %client;
    "".get(%httpObj, "winbuild:80", "/scripts/orion/tests/pi.txt");
};
function serverCmdTestHTTPObject(%client) {
    if (!("debugActive".hasPlayerObjectAndPermission_Warn(%client))) {
        return;
    }
    testHTTPObjectReal(%client);
};
function httpObjTestRequest::onLine(%this, %line) {
    if ((%line $= "EOF")) {
        %this.gotEOF = 1;
    }
    %this.numLines = (%this.numLines + 1.0);
    %this.numChars = (%this.numChars + strlen(%line));
    log("network", "debug", "HTTPObjTestRequest::onLine:" @ " " @ %line);
};
function httpObjTestRequest::onDisconnect(%this) {
    %wwo = %this.gotEOF ? "with" : "without";
    %lvl = %this.gotEOF ? "debug" : "error";
    %line = "HTTPObjTestRequest::onDisconnect" @ " " @ %wwo @ " " @ "EOF. lines =" @ " " @ %this.numLines @ " " @ "chars =" @ " " @ %this.numChars;
    log("network", %lvl, %line);
    %line.notifyRequestingClient(%this);
    %this.delete();
};
function httpObjTestRequest::onConnectFailed(%this) {
    %line = "HTTPObjTestRequest::onConnectFailed.";
    log("network", "error", %line);
    %line.notifyRequestingClient(%this);
};
function httpObjTestRequest::onConnected(%this) {
    %line = "HTTPObjTestRequest::onConnected.";
    %line.notifyRequestingClient(%this);
};
function httpObjTestRequest::notifyRequestingClient(%this, %line) {
    if (isObject(%this.requestingClient)) {
        admin::doSystemMessagePlayer(%this.requestingClient.Player, %line, 'MsgInfoMessage');
    }
};
