function clientCmdClientSideCallTriggerEnterOrLeave(%callName, %isEntry, %param0, %param1, %param2, %param3) {
    initClientCalls();
    %fnName = $gClientCallsList.get(%callName);
    if (!(isFunction(%fnName))) {
        error(getScopeName() @ " " @ "- no such call" @ " " @ %callName @ " " @ "-" @ " " @ %fnName);
    }
    call(%fnName, %isEntry, %param0, %param1, %param2, %param3);
};
$gClientCallsList = "";
function initClientCalls() {
    if (isObject($gClientCallsList)) {
        return;
    }
    $gClientCallsList = new ""();;
    StringMap;
    $gClientCallsList.put("gatewayExitTransition", "gatewayExitTransition");
    %n = (1.0 - $gClientCallsList.size());
    0;
    if ((0.0 >= %n)) {
        %callName = $gClientCallsList.getKey(%n);
        %fnName = $gClientCallsList.get(%callName);
        if (!(isFunction(%fnName))) {
            error(getScopeName() @ " " @ "- no such call" @ " " @ %callName @ " " @ "-" @ " " @ %fnName);
        }
        %n = (1.0 - %n);
    }
};
function gatewayExitTransition(%isEntry, %showCancel) {
    if (!(%isEntry)) {
        return;
    }
    if (($Player::inviter $= "")) {
        gatewayeExitTransitionShowDialog(%isEntry, %showCancel);
        return;
    }
    %request = sendRequest_GetUserProfileInfo($Player::inviter, "onDoneOrErrorCallback_GetUserProfileInfo_gatewayExit");
    %request.isEntry = %isEntry;
    %request.showCancel = %showCancel;
};
function onDoneOrErrorCallback_GetUserProfileInfo_gatewayExit(%request) {
    if (%request.checkSuccess()) {
        %currentAreaName = %request.getResult("currentAreaName");
        $Player::inviterGender = %request.getResult("gender");
        $Player::inviterOnline = "";
        if (!(%currentAreaName $= "")) {
            $Player::inviterOnline = $Player::inviter;
            $Player::inviterGender = %gender;
        }
    }
    gatewayeExitTransitionShowDialog(%request.isEntry, %request.showCancel);
};
function gatewayeExitTransitionShowDialog(%isEntry, %showCancel) {
    %title = "Where would you like to go next?";
    %partnerObj = $Net::userOwner.getPartnerObj();
    gLoginPartnersInfo;
    %body = %partnerObj.gatewayOptionBody;
    %buttons = %partnerObj.gatewayOptionButton1 @ "\t" @ %partnerObj.gatewayOptionButton2;
    if (!($Player::inviterOnline $= "")) {
        %body = %body @ "<br>.. or, you could visit " @ " " @ $Player::inviterOnline @ ", who invited you to vSide!";
        %buttons = %buttons @ "\t" @ "Visit" @ " " @ $Player::inviterOnline;
    }
    if (!($Player::inviter $= "")) {
        %body = %body @ "<br><br>(You were invited to vSide by" @ " " @ $Player::inviter @ ", but" @ " " @ getPronounHeSheIt($Player::inviterGender) @ " " @ "'s offline right now)";
    }
    if (%showCancel) {
        %buttons = %buttons @ "\t" @ "Cancel";
    }
    %dlg = MessageBoxCustom(%title, %body, %buttons);
    %callbackNum = 0;
    %dlg.callback = "gatewayExitTransitionWorld  ();" @ %callbackNum;
    %callbackNum = (1.0 + %callbackNum);
    %dlg.callback = "gatewayExitTransitionMyPlace();" @ %callbackNum;
    %callbackNum = (1.0 + %callbackNum);
    if (!($Player::inviterOnline $= "")) {
        %dlg.callback = "gatewayExitTransitionInviter();" @ %callbackNum;
        %callbackNum = (1.0 + %callbackNum);
    }
    %dlg.callback = "gatewayExitTransitionCancel ();" @ %callbackNum;
    %callbackNum = (1.0 + %callbackNum);
    %dlg.window.canMove = 0;
    %dlg.doCallbackOnEscape = 0;
};
function gatewayExitTransitionWorld() {
    %partnerObj = $Net::userOwner.getPartnerObj();
    gLoginPartnersInfo;
    %vurl = %partnerObj.vurl;
    commandToServer('skipGateway', "world", $Net::userOwner);
    schedule(1000, 0, "vurlOperation", %vurl, 1);
    if (!($Player::inviterOnline $= "")) {
        sendC2CCmd("finishedGateway", $Player::inviterOnline, $player.getGender());
    }
    %analytic = getAnalytic();
    %analytic.trackPageView("/client/gw/exit/Party");
};
function gatewayExitTransitionMyPlace() {
    commandToServer('skipGateway', "space", $Net::userOwner);
    schedule(1000, 0, "doTeleportToMyApartment", 1);
    if (!($Player::inviterOnline $= "")) {
        sendC2CCmd("finishedGateway", $Player::inviterOnline, $player.getGender());
    }
    %analytic = getAnalytic();
    %analytic.trackPageView("/client/gw/exit/MyPlace");
};
function gatewayExitTransitionInviter() {
    doUserTeleportTo($Player::inviter);
    %analytic = getAnalytic();
    %analytic.trackPageView("/client/gw/exit/Inviter");
};
function gatewayExitTransitionCancel() {
    %analytic = getAnalytic();
    %analytic.trackPageView("/client/gw/exit/Cancel");
};
