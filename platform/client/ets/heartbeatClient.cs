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
            error(getScopeName() SPC "- heartbeat too frequent. setting to" SPC $gClientHeartbeatPeriodMinimumMS SPC "MS");
            $gClientHeartbeatPeriodMS = $gClientHeartbeatPeriodMinimumMS;
        }
        sendRequest_ClientHeartbeat($Player::Name, "onDoneOrErrorCallback_ClientHeartbeat");
        $gClientHeartbeatTimer = schedule($gClientHeartbeatPeriodMS, 0, "clientHeartbeat");
    }
    else
    {
        echo("not repeating halting client heartbeat");
    }
    return ;
}
function onDoneOrErrorCallback_ClientHeartbeat(%request)
{
    if (!%request.checkSuccess())
    {
        %errorCode = %request.getValue("errorCode");
        error(getScopeName() SPC "- heartbeat failed, error =" SPC %errorCode);
        if (%errorCode $= "invalid")
        {
            error(getScopeName() SPC "- heartbeat failed due to invalid token, logging out.");
            %msg = $MsgCat::network["E-DROPPED"] @ $MsgCat::network["E-HEARTBEAT-INVALID"];
            logout(0);
            disconnectedCleanup(LoginGui);
            MessageBoxOK("DISCONNECT", %msg, "");
        }
    }
    return ;
}
