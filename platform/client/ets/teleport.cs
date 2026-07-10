function clientCmdTeleportSuccessful() {
    if (isObject($VURL::curVURL)) {
        $VURL::curVURL.doReportSuccess();
        $VURL::curVURL.delete();
        $VURL::curVURL = "";
    }
    echo("Teleport successful");
    doreopen = 0 @ geTGF;
    setIdle(0);
    if (isObject($player)) {
        $player.adjustHorizontalScale();
    }
};
function clientCmdTeleportFailure(%retry) {
    if (isObject($VURL::curVURL)) {
        echo($VURL::curVURL @ vurl);
        if ((0.0 != %retry)) {
            log("network", "info", "Attempting Retry.");
            if ($VURL::curVURL.execute()) {
                return "VURL Teleport Failed. VURL=";
            }
            log("network", "warn", "VURL Teleportion faild, retries exausted.");
        }
        reopen();
        $VURL::curVURL.doReportError("FAIL", "");
        $VURL::curVURL.delete();
    }
    echo("Teleport failed");
    reopen();
};
function clientCmdNotifyOfRefuseTeleport() {
    handleSystemMessage("msgInfoMessage", );
};
if (!(isObject($pi))) {
    $pi = 3.1415926536;
}
function Player::adjustHorizontalScale(%this) {
    %hScale = getWord(%this.getScale(), 0);
    %vScale = getWord(%this.getScale(), 2);
    if (!(gGetField(%this))) {
        gSetField(%this, 1);
        gSetField(%this, %hScale);
        %hScale = 0.05;
        baseHorizScale;
    }
    %hScale = mMin((0.05 + %hScale), gGetField(%this));
    baseHorizScale;
    %this.setScale(%hScale @ " " @ %hScale @ " " @ %vScale);
    if ((gGetField(%this) < %hScale)) {
        %this.schedule(25, "adjustHorizontalScale");
    }
    gSetField(%this, 0);
};
function doTeleportToMyApartment(%ignoreDownloadStatus) {
    if (!(isDefined("%ignoreDownloadStatus"))) {
        %ignoreDownloadStatus = 0;
    }
    getApartmentVURL("doTeleportToMyApartmentCallback", %ignoreDownloadStatus);
};
function doTeleportToMyApartmentCallback(%status, %vurl, %ignoreDownloadStatus) {
    if ((%status $= "fail")) {
        handleSystemMessage("msgInfoMessage", "We could not find your apartment at this time.");
    }
    if ((%status $= "noOwnedSpace")) {
        %statusMsg = "statusMsg".getValue();
        GetMyApartmentVURLCommand;
        handleSystemMessage("msgInfoMessage", "We could not find your apartment." @ "\n" @ %statusMsg);
    }
    if ((%vurl $= "")) {
        handleSystemMessage("msgInfoMessage", "You do not appear to own an appartment.");
    }
    if (isVisible()) {
        close();
    }
    vurlOperation(%vurl, %ignoreDownloadStatus);
};
function getApartmentVURL(%callback, %ignoreDownloadStatus) {
    %request = safeEnsureScriptObject("ManagerRequest", "GetMyApartmentVURLCommand");
    if (%request.isOpen()) {
        return;
    }
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
    if ((%status $= "fail")) {
        echo(getScopeName() @ "->failed");
        %statusMsg = %this.getValue("statusMsg");
        log("network", "error", "GetMyApartmentAddress failed due to: " @ %statusMsg);
    }
    echo(getScopeName() @ "->success");
    log("network", "debug", "GetMyApartmentAddress::onDone: " @ %status);
    %vurl = %this.getValue("vurl");
    $Player::myPlaceVURL = %vurl;
    if (!(%this SPC callback $= "")) {
        %cmd = %this @ callback @ "(\"" @ %status @ "\", \"" @ %vurl @ "\", \"" @ %this @ ignoreDownloadStatus @ "\");";
        eval(%cmd);
    }
};
function GetMyApartmentVURLCommand::onError(%this, %unused, %errMsg) {
    log("network", "debug", "GetMyApartmentVURLCommand::onError: " @ %errMsg);
    $Player::myPlaceVURL = "";
    %this.schedule(0, "delete");
};
