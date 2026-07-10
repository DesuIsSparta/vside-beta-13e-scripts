$Net::upgradeAvailable = 0;
$Net::UpgradeToolAvailable = 1;
function clientVersion::startUpgrade()
{
    echo("We will now upgrade you!");
    %analytic = getAnalytic();
    %analytic.trackPageView("/client/clientUpdate");
    launchClientUpdater();
}
function clientVersion::checkForUpgrades()
{
    if (!isValidHostAddress($Net::DownloadHost))
    {
        return;
    }
    if (($Net::UpgradeToolAvailable == 0) || !platformIsFile("bin\\_update.exe"))
    {
        $Net::UpgradeToolAvailable = 0;
        echo("No upgrade tool to do upgrading. Skipping further work.");
        return;
    }
    %url = $Net::downloadURL @ "/version_resp.txt";
    new ManagerRequest(clientVersionCheck);
    if (isObject(MissionCleanup))
    {
        MissionCleanup.add(clientVersionCheck);
    }
    clientVersionCheck.setURL(%url);
    clientVersionCheck.start();
}
function clientVersionCheck::onDone(%this, %unused)
{
    %status = findRequestStatus(%this);
    if (!(%status $= "success"))
    {
        log("Admin", "error", getScopeName() @ " " @ "- status =" @ " " @ %status);
        %this.schedule(0, delete);
        return;
    }
    isUpToDate(%this.getValue("client_version"));
    %this.schedule(0, delete);
}
function isUpToDate(%available)
{
    %buildVersion = formatInt("%d", getBuildVersion());
    %protocolVersion = formatInt("%d", getProtocolVersion());
    if ((%buildVersion <= 0) || (%protocolVersion <= 0))
    {
        echo("We're not sure about our own versions. Returning...");
        $Net::upgradeAvailable = 0;
        return 0;
    }
    if (%available > %buildVersion)
    {
    }
    if (%available > %protocolVersion)
    {
        echo("A new client version(" @ %available @ ") is available. We have " @ %buildVersion @ ".");
        $Net::upgradeAvailable = 1;
        return 1;
    }
    $Net::upgradeAvailable = 0;
    return 0;
}
function clientVersionCheck::onError(%this)
{
    $Net::upgradeAvailable = 0;
    log("Admin", "error", getScopeName() @ " " @ getDebugString(%this) @ " " @ "- error = " @ " " @ %errorName @ " " @ "url = " @ " " @ %this.getURL());
    $Net::upgradeAvailable = 0;
    %this.schedule(0, delete);
}
