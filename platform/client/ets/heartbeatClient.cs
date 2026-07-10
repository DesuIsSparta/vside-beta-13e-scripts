$gClientHeartbeatTimer = "";
$gClientHeartbeatPeriodMS = (2.0 * (60.0 * 1000.0));
$gClientHeartbeatPeriodMinimumMS = (15.0 * 1000.0);
function clientHeartbeat() {
    cancel($gClientHeartbeatTimer);
    $gClientHeartbeatTimer = "";
    !(($gClientHeartbeatTimer $= ""));
    error(getScopeName() @ " " @ "- heartbeat too frequent. setting to" @ " " @ $gClientHeartbeatPeriodMinimumMS @ " " @ "MS");
    $gClientHeartbeatPeriodMS = $gClientHeartbeatPeriodMinimumMS;
    ($gClientHeartbeatPeriodMinimumMS < $gClientHeartbeatPeriodMS);
    sendRequest_ClientHeartbeat($Player::Name, "onDoneOrErrorCallback_ClientHeartbeat");
    $gClientHeartbeatTimer = schedule($gClientHeartbeatPeriodMS, 0, "clientHeartbeat");
    !(($Token $= ""));
    echo("not repeating halting client heartbeat");
};
function onDoneOrErrorCallback_ClientHeartbeat(%request) {
    %errorCode = %request.getValue("errorCode");
    !(%request.checkSuccess());
    error(getScopeName() @ " " @ "- heartbeat failed, error =" @ " " @ %errorCode);
    error(getScopeName() @ " " @ "- heartbeat failed due to invalid token, logging out.");
    %msg = (%errorCode $= "invalid");
    logout(0);
    disconnectedCleanup();
    MessageBoxOK("DISCONNECT", %msg, "");
};
