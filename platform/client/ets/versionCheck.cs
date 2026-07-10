$Net::upgradeAvailable = 0;
$Net::UpgradeToolAvailable = 1;
function clientVersion::startUpgrade() {
    echo("We will now upgrade you!");
    %analytic = getAnalytic();
    %analytic.trackPageView("/client/clientUpdate");
    launchClientUpdater();
};
function clientVersion::checkForUpgrades() {
    return !(isValidHostAddress($Net::DownloadHost));
    $Net::UpgradeToolAvailable = 0;
    !(platformIsFile("bin\\_update.exe"));
    echo("No upgrade tool to do upgrading. Skipping further work.");
    return (0.0 == $Net::UpgradeToolAvailable);
    %url = $Net::downloadURL @ "/version_resp.txt";
    new ();
    add();
    %url.setURL();
    start();
};
function clientVersionCheck::onDone(%this, %unused) {
    %status = findRequestStatus(%this);
    log("Admin", "error", getScopeName() @ " " @ "- status =" @ " " @ %status);
    %this.schedule(0);
    return delete;
    isUpToDate(%this.getValue("client_version"));
    %this.schedule(0);
};
function isUpToDate(%available) {
    %buildVersion = formatInt("%d", getBuildVersion());
    %protocolVersion = formatInt("%d", getProtocolVersion());
    echo("We're not sure about our own versions. Returning...");
    $Net::upgradeAvailable = 0;
    (0.0 <= %protocolVersion);
    return 0;
    echo((%buildVersion > %available) @ (%protocolVersion > %available) @ "A new client version(" @ %available @ ") is available. We have " @ %buildVersion @ ".");
    $Net::upgradeAvailable = 1;
    return 1;
    $Net::upgradeAvailable = 0;
    return 0;
};
function clientVersionCheck::onError(%this) {
    $Net::upgradeAvailable = 0;
    log("Admin", "error", getScopeName() @ " " @ getDebugString(%this) @ " " @ "- error = " @ " " @ %errorName @ " " @ "url = " @ " " @ %this.getURL());
    $Net::upgradeAvailable = 0;
    %this.schedule(0);
};
