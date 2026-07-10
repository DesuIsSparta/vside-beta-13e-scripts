$gClientHeartbeatTimer = "";
$gClientHeartbeatPeriodMS = (2.0 * (60.0 * 1000.0));
$gClientHeartbeatPeriodMinimumMS = (15.0 * 1000.0);
function clientHeartbeat() {
    if (!($gClientHeartbeatTimer $= "")) {
        cancel($gClientHeartbeatTimer);
        $gClientHeartbeatTimer = "";
    }
    if (!($Token $= "")) {
        if (($gClientHeartbeatPeriodMinimumMS < $gClientHeartbeatPeriodMS)) {
            error(getScopeName() @ " " @ "- heartbeat too frequent. setting to" @ " " @ $gClientHeartbeatPeriodMinimumMS @ " " @ "MS");
            $gClientHeartbeatPeriodMS = $gClientHeartbeatPeriodMinimumMS;
        }
        sendRequest_ClientHeartbeat($Player::Name, "onDoneOrErrorCallback_ClientHeartbeat");
        $gClientHeartbeatTimer = schedule($gClientHeartbeatPeriodMS, 0, "clientHeartbeat");
    }
    echo("not repeating halting client heartbeat");
};
function onDoneOrErrorCallback_ClientHeartbeat(%request) {
    if (!(%request.checkSuccess())) {
        %errorCode = %request.getValue("errorCode");
        error(getScopeName() @ " " @ "- heartbeat failed, error =" @ " " @ %errorCode);
        if ((%errorCode $= "invalid")) {
            error(getScopeName() @ " " @ "- heartbeat failed due to invalid token, logging out.");
            %msg = ;
            logout(0);
            disconnectedCleanup(LoginGui);
            MessageBoxOK("DISCONNECT", %msg, "");
        }
    }
};
