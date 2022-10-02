function clientCmdClientSideCallTriggerEnterOrLeave(%callName, %isEntry, %param0, %param1, %param2, %param3)
{
    initClientCalls();
    %fnName = $gClientCallsList.get(%callName);
    if (!isFunction(%fnName))
    {
        error(getScopeName() SPC "- no such call" SPC %callName SPC "-" SPC %fnName);
    }
    else
    {
        call(%fnName, %isEntry, %param0, %param1, %param2, %param3);
    }
    return ;
}
$gClientCallsList = "";
function initClientCalls()
{
    if (isObject($gClientCallsList))
    {
        return ;
    }
    $gClientCallsList = new StringMap();
    $gClientCallsList.put("gatewayExitTransition", "gatewayExitTransition");
    %n = $gClientCallsList.size() - 1;
    while (%n >= 0)
    {
        %callName = $gClientCallsList.getKey(%n);
        %fnName = $gClientCallsList.get(%callName);
        if (!isFunction(%fnName))
        {
            error(getScopeName() SPC "- no such call" SPC %callName SPC "-" SPC %fnName);
        }
        %n = %n - 1;
    }
}

function gatewayExitTransition(%isEntry, %showCancel)
{
    if (!%isEntry)
    {
        return ;
    }
    if ($Player::inviter $= "")
    {
        gatewayeExitTransitionShowDialog(%isEntry, %showCancel);
        return ;
    }
    %request = sendRequest_GetUserProfileInfo($Player::inviter, "onDoneOrErrorCallback_GetUserProfileInfo_gatewayExit");
    %request.isEntry = %isEntry;
    %request.showCancel = %showCancel;
    return ;
}
function onDoneOrErrorCallback_GetUserProfileInfo_gatewayExit(%request)
{
    if (%request.checkSuccess())
    {
        %currentAreaName = %request.getResult("currentAreaName");
        $Player::inviterGender = %request.getResult("gender");
        $Player::inviterOnline = "";
        if (!(%currentAreaName $= ""))
        {
            $Player::inviterOnline = $Player::inviter;
            $Player::inviterGender = %gender;
        }
    }
    gatewayeExitTransitionShowDialog(%request.isEntry, %request.showCancel);
    return ;
}
function gatewayeExitTransitionShowDialog(%isEntry, %showCancel)
{
    %title = "Where would you like to go next?";
    %partnerObj = gLoginPartnersInfo.getPartnerObj($Net::userOwner);
    %body = %partnerObj.gatewayOptionBody;
    %buttons = %partnerObj.gatewayOptionButton1 TAB %partnerObj.gatewayOptionButton2;
    if (!($Player::inviterOnline $= ""))
    {
        %body = %body @ "<br>.. or, you could visit " SPC $Player::inviterOnline @ ", who invited you to vSide!";
        %buttons = %buttons TAB "Visit" SPC $Player::inviterOnline;
    }
    else
    {
        if (!($Player::inviter $= ""))
        {
            %body = %body @ "<br><br>(You were invited to vSide by" SPC $Player::inviter @ ", but" SPC getPronounHeSheIt($Player::inviterGender) SPC "\'s offline right now)";
        }
    }
    if (%showCancel)
    {
        %buttons = %buttons TAB "Cancel";
    }
    %dlg = MessageBoxCustom(%title, %body, %buttons);
    %callbackNum = 0;
    %dlg.callback[%callbackNum] = "gatewayExitTransitionWorld  ();";
    %callbackNum = %callbackNum + 1;
    %dlg.callback[%callbackNum] = "gatewayExitTransitionMyPlace();";
    %callbackNum = %callbackNum + 1;
    if (!($Player::inviterOnline $= ""))
    {
        %dlg.callback[%callbackNum] = "gatewayExitTransitionInviter();";
        %callbackNum = %callbackNum + 1;
    }
    %dlg.callback[%callbackNum] = "gatewayExitTransitionCancel ();";
    %callbackNum = %callbackNum + 1;
    %dlg.window.canMove = 0;
    %dlg.doCallbackOnEscape = 0;
    return ;
}
function gatewayExitTransitionWorld()
{
    %partnerObj = gLoginPartnersInfo.getPartnerObj($Net::userOwner);
    %vurl = %partnerObj.vurl;
    commandToServer('skipGateway', "world", $Net::userOwner);
    schedule(1000, 0, "vurlOperation", %vurl, 1);
    if (!($Player::inviterOnline $= ""))
    {
        sendC2CCmd("finishedGateway", $Player::inviterOnline, $player.getGender());
    }
    %analytic = getAnalytic();
    %analytic.trackPageView("/client/gw/exit/Party");
    return ;
}
function gatewayExitTransitionMyPlace()
{
    commandToServer('skipGateway', "space", $Net::userOwner);
    schedule(1000, 0, "doTeleportToMyApartment", 1);
    if (!($Player::inviterOnline $= ""))
    {
        sendC2CCmd("finishedGateway", $Player::inviterOnline, $player.getGender());
    }
    %analytic = getAnalytic();
    %analytic.trackPageView("/client/gw/exit/MyPlace");
    return ;
}
function gatewayExitTransitionInviter()
{
    doUserTeleportTo($Player::inviter);
    %analytic = getAnalytic();
    %analytic.trackPageView("/client/gw/exit/Inviter");
    return ;
}
function gatewayExitTransitionCancel()
{
    %analytic = getAnalytic();
    %analytic.trackPageView("/client/gw/exit/Cancel");
    return ;
}
