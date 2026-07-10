$httpObjTestRequest = 0;
function httpObjTest::init()
{
    %httpObj = new HTTPObject(httpObjTestRequest);
    %httpObj.gotEOF = 0;
    %httpObj.numLines = 0;
    %httpObj.numChars = 0;
    %httpObj.requestingClient = 0;
    return %httpObj;
}
function testHTTPObject()
{
    testHTTPObjectReal(0);
}
function testHTTPObjectReal(%client)
{
    %httpObj = httpObjTest::init();
    %httpObj.requestingClient = %client;
    %httpObj.get("winbuild:80", "/scripts/orion/tests/pi.txt", "");
}
function serverCmdTestHTTPObject(%client)
{
    if (!%client.hasPlayerObjectAndPermission_Warn("debugActive"))
    {
        return;
    }
    testHTTPObjectReal(%client);
}
function httpObjTestRequest::onLine(%this, %line)
{
    if (%line $= "EOF")
    {
        %this.gotEOF = 1;
    }
    %this.numLines = %this.numLines + 1;
    %this.numChars = %this.numChars + strlen(%line);
    log("network", "debug", "HTTPObjTestRequest::onLine:" @ " " @ %line);
}
function httpObjTestRequest::onDisconnect(%this)
{
    %wwo = %this.gotEOF ? "with" : "without";
    %lvl = %this.gotEOF ? "debug" : "error";
    %line = "HTTPObjTestRequest::onDisconnect" @ " " @ %wwo @ " " @ "EOF. lines =" @ " " @ %this.numLines @ " " @ "chars =" @ " " @ %this.numChars;
    log("network", %lvl, %line);
    %this.notifyRequestingClient(%line);
    %this.delete();
}
function httpObjTestRequest::onConnectFailed(%this)
{
    %line = "HTTPObjTestRequest::onConnectFailed.";
    log("network", "error", %line);
    %this.notifyRequestingClient(%line);
}
function httpObjTestRequest::onConnected(%this)
{
    %line = "HTTPObjTestRequest::onConnected.";
    %this.notifyRequestingClient(%line);
}
function httpObjTestRequest::notifyRequestingClient(%this, %line)
{
    if (isObject(%this.requestingClient))
    {
        admin::doSystemMessagePlayer(%this.requestingClient.Player, %line, 'MsgInfoMessage');
    }
}
