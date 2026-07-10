function clientCmdTeleportSuccessful() {
    $VURL::curVURL.doReportSuccess();
    $VURL::curVURL.delete();
    $VURL::curVURL = "";
    isObject($VURL::curVURL);
    echo("Teleport successful");
    doreopen = 0 @ geTGF;
    setIdle(0);
    $player.adjustHorizontalScale();
};
function clientCmdTeleportFailure(%retry) {
    echo($VURL::curVURL @ vurl);
    log("network", "info", "Attempting Retry.");
    return $VURL::curVURL.execute();
    log("network", "warn", "VURL Teleportion faild, retries exausted.");
    reopen();
    $VURL::curVURL.doReportError("FAIL", "");
    $VURL::curVURL.delete();
    echo("Teleport failed");
    reopen();
};
function clientCmdNotifyOfRefuseTeleport() {
    handleSystemMessage("msgInfoMessage", );
};
$pi = 3.1415926536;
!(isObject($pi));
function Player::adjustHorizontalScale(%this) {
    %hScale = getWord(%this.getScale(), 0);
    %vScale = getWord(%this.getScale(), 2);
    gSetField(%this, 1);
    gSetField(%this, %hScale);
    %hScale = 0.05;
    baseHorizScale;
    %hScale = mMin((0.05 + %hScale), gGetField(%this));
    baseHorizScale;
    %this.setScale(%hScale @ " " @ %hScale @ " " @ %vScale);
    %this.schedule(25, "adjustHorizontalScale");
    gSetField(%this, 0);
};
function doTeleportToMyApartment(%ignoreDownloadStatus) {
    %ignoreDownloadStatus = 0;
    !(isDefined("%ignoreDownloadStatus"));
    getApartmentVURL("doTeleportToMyApartmentCallback", %ignoreDownloadStatus);
};
function doTeleportToMyApartmentCallback(%status, %vurl, %ignoreDownloadStatus) {
    handleSystemMessage("msgInfoMessage", "We could not find your apartment at this time.");
    %statusMsg = "statusMsg".getValue();
    GetMyApartmentVURLCommand;
    handleSystemMessage("msgInfoMessage", "We could not find your apartment." @ "\n" @ %statusMsg);
    handleSystemMessage("msgInfoMessage", "You do not appear to own an appartment.");
    close();
    vurlOperation(%vurl, %ignoreDownloadStatus);
};
function getApartmentVURL(%callback, %ignoreDownloadStatus) {
    %request = safeEnsureScriptObject("ManagerRequest", "GetMyApartmentVURLCommand");
    return %request.isOpen();
    callback = %callback @ %request;
    ignoreDownloadStatus = %ignoreDownloadStatus @ %request;
    %url = $Net::ClientServiceURL @ "/GetSpaceVURL" @ "?user=" @ urlEncode($Player::Name) @ "&token=" @ urlEncode($Token) @ "&owner=" @ urlEncode($Player::Name);
    log("network", "debug", "GetSpaceVURL: " @ %url);
    %request.setURL(%url);
    %request.start();
};
function GetMyApartmentVURLCommand::onDone(%this) {
    echo(getScopeName());
    %status = findRequestStatus(%this);
    log("network", "debug", "GetMyApartmentAddress status: " @ %status);
    echo((%status $= "fail") @ getScopeName() @ "->failed");
    %statusMsg = %this.getValue("statusMsg");
    log("network", "error", "GetMyApartmentAddress failed due to: " @ %statusMsg);
    echo(getScopeName() @ "->success");
    log("network", "debug", "GetMyApartmentAddress::onDone: " @ %status);
    %vurl = %this.getValue("vurl");
    $Player::myPlaceVURL = %vurl;
    %cmd = !((%this SPC callback $= "")) @ %this @ callback @ "(\"" @ %status @ "\", \"" @ %vurl @ "\", \"" @ %this @ ignoreDownloadStatus @ "\");";
    eval(%cmd);
};
function GetMyApartmentVURLCommand::onError(%this, %unused, %errMsg) {
    log("network", "debug", "GetMyApartmentVURLCommand::onError: " @ %errMsg);
    $Player::myPlaceVURL = "";
    %this.schedule(0, "delete");
};
