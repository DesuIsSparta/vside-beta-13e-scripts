function clientCmdClientSideCallTriggerEnterOrLeave(%callName, %isEntry, %param0, %param1, %param2, %param3) {
    initClientCalls();
    %fnName = %callName.get($gClientCallsList);
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
    $gClientCallsList = new StringMap("");
    "gatewayExitTransition".put($gClientCallsList, "gatewayExitTransition");
    %n = ($gClientCallsList.size() - 1.0);
    while ((%n >= 0.0)) {
        %callName = %n.getKey($gClientCallsList);
        %fnName = %callName.get($gClientCallsList);
        if (!(isFunction(%fnName))) {
            error(getScopeName() @ " " @ "- no such call" @ " " @ %callName @ " " @ "-" @ " " @ %fnName);
        }
        %n = (%n - 1.0);
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
        %currentAreaName = "currentAreaName".getResult(%request);
        $Player::inviterGender = "gender".getResult(%request);
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
    %partnerObj = $Net::userOwner.getPartnerObj(gLoginPartnersInfo);
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
    %callbackNum = (%callbackNum + 1.0);
    %dlg.callback = "gatewayExitTransitionMyPlace();" @ %callbackNum;
    %callbackNum = (%callbackNum + 1.0);
    if (!($Player::inviterOnline $= "")) {
        %dlg.callback = "gatewayExitTransitionInviter();" @ %callbackNum;
        %callbackNum = (%callbackNum + 1.0);
    }
    %dlg.callback = "gatewayExitTransitionCancel ();" @ %callbackNum;
    %callbackNum = (%callbackNum + 1.0);
    %dlg.window.canMove = 0;
    %dlg.doCallbackOnEscape = 0;
};
function gatewayExitTransitionWorld() {
    %partnerObj = $Net::userOwner.getPartnerObj(gLoginPartnersInfo);
    %vurl = %partnerObj.vurl;
    commandToServer('skipGateway', "world", $Net::userOwner);
    schedule(1000, 0, "vurlOperation", %vurl, 1);
    if (!($Player::inviterOnline $= "")) {
        sendC2CCmd("finishedGateway", $Player::inviterOnline, $player.getGender());
    }
    %analytic = getAnalytic();
    "/client/gw/exit/Party".trackPageView(%analytic);
};
function gatewayExitTransitionMyPlace() {
    commandToServer('skipGateway', "space", $Net::userOwner);
    schedule(1000, 0, "doTeleportToMyApartment", 1);
    if (!($Player::inviterOnline $= "")) {
        sendC2CCmd("finishedGateway", $Player::inviterOnline, $player.getGender());
    }
    %analytic = getAnalytic();
    "/client/gw/exit/MyPlace".trackPageView(%analytic);
};
function gatewayExitTransitionInviter() {
    doUserTeleportTo($Player::inviter);
    %analytic = getAnalytic();
    "/client/gw/exit/Inviter".trackPageView(%analytic);
};
function gatewayExitTransitionCancel() {
    %analytic = getAnalytic();
    "/client/gw/exit/Cancel".trackPageView(%analytic);
};
