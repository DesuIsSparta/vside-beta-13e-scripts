$gClientHeartbeatTimer = "";
$gClientHeartbeatPeriodMS = (1000 * 60) * 2;
$gClientHeartbeatPeriodMinimumMS = 1000 * 15;
function clientHeartbeat()
{
    if (!($gClientHeartbeatTimer $= ""))
    {
        cancel($gClientHeartbeatTimer);
        $gClientHeartbeatTimer = "";
    }
    if (!($Token $= ""))
    {
        if ($gClientHeartbeatPeriodMS < $gClientHeartbeatPeriodMinimumMS)
        {
            error(getScopeName() @ " " @ "- heartbeat too frequent. setting to" @ " " @ $gClientHeartbeatPeriodMinimumMS @ " " @ "MS");
            $gClientHeartbeatPeriodMS = $gClientHeartbeatPeriodMinimumMS;
        }
        sendRequest_ClientHeartbeat($Player::Name, "onDoneOrErrorCallback_ClientHeartbeat");
        $gClientHeartbeatTimer = schedule($gClientHeartbeatPeriodMS, 0, "clientHeartbeat");
    }
    else
    {
        echo("not repeating halting client heartbeat");
    }
}
function onDoneOrErrorCallback_ClientHeartbeat(%request)
{
    if (!%request.checkSuccess())
    {
        %errorCode = %request.getValue("errorCode");
        error(getScopeName() @ " " @ "- heartbeat failed, error =" @ " " @ %errorCode);
        if (%errorCode $= "invalid")
        {
            error(getScopeName() @ " " @ "- heartbeat failed due to invalid token, logging out.");
            %msg = $MsgCat::network["E-DROPPED"] @ $MsgCat::network["E-DROPPED"][$MsgCat::network @ "E-HEARTBEAT-INVALID"];
            logout(0);
            disconnectedCleanup(LoginGui);
            MessageBoxOK("DISCONNECT", %msg, "");
        }
    }
}
