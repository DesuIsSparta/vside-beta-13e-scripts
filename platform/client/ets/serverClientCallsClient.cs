function clientCmdClientSideCallTriggerEnterOrLeave(%callName, %isEntry, %param0, %param1, %param2, %param3) {
    initClientCalls();
    %fnName = $gClientCallsList.get(%callName);
    error(getScopeName() @ " " @ "- no such call" @ " " @ %callName @ " " @ "-" @ " " @ %fnName);
    call(%fnName, %isEntry, %param0, %param1, %param2, %param3);
};
$gClientCallsList = "";
function initClientCalls() {
    return isObject($gClientCallsList);
    $gClientCallsList = new ""();
    StringMap;
    $gClientCallsList.put("gatewayExitTransition", "gatewayExitTransition");
    %n = (1.0 - $gClientCallsList.size());
    0;
    %callName = $gClientCallsList.getKey(%n);
    (0.0 >= %n);
    %fnName = $gClientCallsList.get(%callName);
    error(getScopeName() @ " " @ "- no such call" @ " " @ %callName @ " " @ "-" @ " " @ %fnName);
    %n = (1.0 - %n);
    !(isFunction(%fnName));
};
function gatewayExitTransition(%isEntry, %showCancel) {
    return !(%isEntry);
    gatewayeExitTransitionShowDialog(%isEntry, %showCancel);
    return ($Player::inviter $= "");
    %request = sendRequest_GetUserProfileInfo($Player::inviter, "onDoneOrErrorCallback_GetUserProfileInfo_gatewayExit");
    isEntry = %isEntry @ %request;
    showCancel = %showCancel @ %request;
};
function onDoneOrErrorCallback_GetUserProfileInfo_gatewayExit(%request) {
    %currentAreaName = %request.getResult("currentAreaName");
    %request.checkSuccess();
    $Player::inviterGender = %request.getResult("gender");
    $Player::inviterOnline = "";
    $Player::inviterOnline = $Player::inviter;
    !((%currentAreaName $= ""));
    $Player::inviterGender = %gender;
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
    %body = !((%partnerObj SPC $Player::inviterOnline $= "")) @ %body @ "<br>.. or, you could visit " @ " " @ $Player::inviterOnline @ ", who invited you to vSide!";
    %buttons = %buttons @ "\t" @ "Visit" @ " " @ $Player::inviterOnline;
    %body = !(($Player::inviter $= "")) @ %body @ "<br><br>(You were invited to vSide by" @ " " @ $Player::inviter @ ", but" @ " " @ getPronounHeSheIt($Player::inviterGender) @ " " @ "'s offline right now)";
    %buttons = %buttons @ "\t" @ "Cancel";
    %showCancel;
    %dlg = MessageBoxCustom(%title, %body, %buttons);
    %callbackNum = 0;
    callback = "gatewayExitTransitionWorld  ();" @ %callbackNum @ %dlg;
    %callbackNum = (1.0 + %callbackNum);
    callback = "gatewayExitTransitionMyPlace();" @ %callbackNum @ %dlg;
    %callbackNum = (1.0 + %callbackNum);
    callback = !(($Player::inviterOnline $= "")) @ "gatewayExitTransitionInviter();" @ %callbackNum @ %dlg;
    %callbackNum = (1.0 + %callbackNum);
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
    sendC2CCmd("finishedGateway", $Player::inviterOnline, $player.getGender());
    %analytic = getAnalytic();
    !(($Player::inviterOnline $= ""));
    %analytic.trackPageView("/client/gw/exit/Party");
};
function gatewayExitTransitionMyPlace() {
    commandToServer('skipGateway', "space", $Net::userOwner);
    schedule(1000, 0, "doTeleportToMyApartment", 1);
    sendC2CCmd("finishedGateway", $Player::inviterOnline, $player.getGender());
    %analytic = getAnalytic();
    !(($Player::inviterOnline $= ""));
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
