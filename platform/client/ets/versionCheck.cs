$Net::upgradeAvailable = 0;
$Net::UpgradeToolAvailable = 1;
function clientVersion::startUpgrade() {
    echo("We will now upgrade you!");
    %analytic = getAnalytic();
    "/client/clientUpdate".trackPageView(%analytic);
    launchClientUpdater();
};
function clientVersion::checkForUpgrades() {
    if (!(isValidHostAddress($Net::DownloadHost))) {
        return;
    }
    if (($Net::UpgradeToolAvailable == 0.0)) {
    }
    if (!(platformIsFile("bin\\_update.exe"))) {
        $Net::UpgradeToolAvailable = 0;
        echo("No upgrade tool to do upgrading. Skipping further work.");
        return;
    }
    %url = $Net::downloadURL @ "/version_resp.txt";
    new ManagerRequest(clientVersionCheck);
    if (isObject(MissionCleanup)) {
        clientVersionCheck.add(MissionCleanup);
    }
    %url.setURL(clientVersionCheck);
    clientVersionCheck.start();
};
function clientVersionCheck::onDone(%this, %unused) {
    %status = findRequestStatus(%this);
    if (!(%status $= "success")) {
        log("Admin", "error", getScopeName() @ " " @ "- status =" @ " " @ %status);
        0.schedule(%this);
        return delete;
    }
    isUpToDate("client_version".getValue(%this));
    0.schedule(%this);
};
function isUpToDate(%available) {
    %buildVersion = formatInt("%d", getBuildVersion());
    %protocolVersion = formatInt("%d", getProtocolVersion());
    if ((%buildVersion <= 0.0)) {
    }
    if ((%protocolVersion <= 0.0)) {
        echo("We're not sure about our own versions. Returning...");
        $Net::upgradeAvailable = 0;
        return 0;
    }
    if ((%available > %buildVersion)) {
    }
    if ((%available > %protocolVersion)) {
        echo("A new client version(" @ %available @ ") is available. We have " @ %buildVersion @ ".");
        $Net::upgradeAvailable = 1;
        return 1;
    }
    $Net::upgradeAvailable = 0;
    return 0;
};
function clientVersionCheck::onError(%this) {
    $Net::upgradeAvailable = 0;
    log("Admin", "error", getScopeName() @ " " @ getDebugString(%this) @ " " @ "- error = " @ " " @ %errorName @ " " @ "url = " @ " " @ %this.getURL());
    $Net::upgradeAvailable = 0;
    0.schedule(%this);
};
