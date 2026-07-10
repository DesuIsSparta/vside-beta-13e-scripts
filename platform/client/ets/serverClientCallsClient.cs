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
    $gClientCallsList = new ""();
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
    isEntry = %isEntry @ %request;
    showCancel = %showCancel @ %request;
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
    gatewayeExitTransitionShowDialog(isEntry, showCancel);
};
function gatewayeExitTransitionShowDialog(%isEntry, %showCancel) {
    %title = "Where would you like to go next?";
    %partnerObj = $Net::userOwner.getPartnerObj();
    gLoginPartnersInfo;
    %body = gatewayOptionBody;
    %partnerObj;
    %buttons = %partnerObj @ gatewayOptionButton2;
    gatewayOptionButton1 @ "\t";
    if (!(%partnerObj SPC $Player::inviterOnline $= "")) {
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
    callback = "gatewayExitTransitionWorld  ();" @ %callbackNum @ %dlg;
    %callbackNum = (1.0 + %callbackNum);
    callback = "gatewayExitTransitionMyPlace();" @ %callbackNum @ %dlg;
    %callbackNum = (1.0 + %callbackNum);
    if (!($Player::inviterOnline $= "")) {
        callback = "gatewayExitTransitionInviter();" @ %callbackNum @ %dlg;
        %callbackNum = (1.0 + %callbackNum);
    }
    callback = "gatewayExitTransitionCancel ();" @ %callbackNum @ %dlg;
    %callbackNum = (1.0 + %callbackNum);
    canMove = %dlg @ window;
    0;
    doCallbackOnEscape = 0 @ %dlg;
};
function gatewayExitTransitionWorld() {
    %partnerObj = $Net::userOwner.getPartnerObj();
    gLoginPartnersInfo;
    %vurl = vurl;
    %partnerObj;
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
