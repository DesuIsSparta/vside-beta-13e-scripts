$seenThrottleMessage = 0;
$seenUnThrottleMessage = 0;
$PackageDownload::ConcurrentTextureThreshold = 10;
$PackageDownload::ConcurrentTextureMaxBytes = 10000;
$PackageDownload::MinimumCityDownloadSpeedThreshold = 1 * 1024;
$PackageDownload::MinimumCityDownloadTimeThreshold = 30;
$PackageDownload::GuestimatedSize = (40 * 1024) * 1024;
$PackageDownload::GuestimatedCommonSize = (5 * 1024) * 1024;
function queuePackageUpdatesByString(%missing)
{
    %missingA = AssetManager::StringToArray(%missing);
    packageDownload.reinit(%missingA);
    return ;
}
function queuePackageUpdates(%missing)
{
    if (!isObject(packageDownload))
    {
        new ScriptObject(packageDownload);
        if (isObject(MissionCleanup))
        {
            MissionCleanup.add(packageDownload);
        }
    }
    if (packageDownload.isActive())
    {
        return ;
    }
    if (!isObject(%missing))
    {
        %missing = AssetManager::getMissingAssets();
    }
    packageDownload.missingPackages = %missing;
    packageDownload.init();
    return ;
}
function downloadPackageUpdates(%missing)
{
    if (!isObject(packageDownload) && isObject(%missing))
    {
        queuePackageUpdates(%missing);
    }
    packageDownload.start();
    return ;
}
function packageDownload::reinit(%this, %missingArray)
{
    %realCurrentItem = %this.currentItem - 1;
    %currentKey = %this.missingPackages.getKey(%realCurrentItem);
    %newMissingArray = new Array();
    if (isObject(MissionCleanup))
    {
        MissionCleanup.add(%newMissingArray);
    }
    %newStatusMap = new StringMap();
    if (isObject(MissionCleanup))
    {
        MissionCleanup.add(%newStatusMap);
    }
    %newStatusMap.put(%currentKey, %this.statusMap.get(%currentKey));
    %newMissingArray.push_back(%currentKey, 0);
    if (%missingArray.getKey(0) $= %currentKey)
    {
        echo("Currently downloading " @ %currentKey @ ". Will truncate current download session.");
    }
    else
    {
        %i = 0;
        while (%i < %missingArray.count())
        {
            %key = %missingArray.getKey(%i);
            %newStatusMap.put(%key, "incomplete");
            %newMissingArray.push_back(%key, %i + 1);
            %i = %i + 1;
        }
        %newMissingArray.sorta();
    }
    echo("Shifting currentItem to start. Putting new lists in place.");
    %this.wasInterrupted = 1;
    %this.currentItem = 1;
    %this.statusMap.delete();
    %this.statusMap = %newStatusMap;
    %this.missingPackages.delete();
    %this.missingPackages = %newMissingArray;
    return ;
}
function packageDownload::init(%this)
{
    %this.currentItem = 0;
    %this.wasInterrupted = 0;
    %this.statusMap = new StringMap();
    if (isObject(MissionCleanup))
    {
        MissionCleanup.add(%this.statusMap);
    }
    %this.bytesDownloadedMap = new StringMap();
    if (isObject(MissionCleanup))
    {
        MissionCleanup.add(%this.bytesDownloadedMap);
    }
    %this.isActive = 0;
    %i = 0;
    while (%i < %this.missingPackages.count())
    {
        %key = %this.missingPackages.getKey(%i);
        %this.statusMap.put(%key, "incomplete");
        %this.bytesDownloadedMap.put(%key, 0);
        %i = %i + 1;
    }
    return 1;
}
function packageDownload::isActive(%this)
{
    return %this.isActive;
}
function packageDownload::isDone(%this)
{
    %i = 0;
    while (%i < %this.statusMap.size())
    {
        if (%this.statusMap.getValue(%i) $= "incomplete")
        {
            return 0;
        }
        %i = %i + 1;
    }
    return 1;
}
function packageDownload::completedSuccessfully(%this)
{
    %i = 0;
    while (%i < %this.statusMap.size())
    {
        if (%this.statusMap.getValue(%i) $= "error")
        {
            return 0;
        }
        %i = %i + 1;
    }
    return 1;
}
function packageDownload::getEstimatedSize(%this)
{
    %total = 0;
    %i = 0;
    while (%i < %this.statusMap.size())
    {
        if (%this.statusMap.getKey(%i) $= $AssetManager::COMMONPACKAGE)
        {
            %total = %total + $PackageDownload::GuestimatedCommonSize;
        }
        else
        {
            %total = %total + $PackageDownload::GuestimatedSize;
        }
        %i = %i + 1;
    }
    return %total;
}
function packageDownload::start(%this)
{
    if (!isObject(%this.missingPackages) && (%this.missingPackages.count() == 0))
    {
        return 0;
    }
    echo("Starting package download.");
    %this.isActive = 1;
    %this.downloadFile();
    return ;
}
function packageDownload::doneDownloading(%this)
{
    if (%this.currentItem >= %this.missingPackages.count())
    {
        return 1;
    }
    return 0;
}
function packageDownload::downloadFile(%this)
{
    if (%this.doneDownloading())
    {
        %this.isActive = 0;
        if (!%this.completedSuccessfully())
        {
            MessageBoxOK("Could not download", "Could not download required files. Click OK to logout.", "buttonBarMenuLogout();");
            return ;
        }
        if (isObject(%this.callBackSink))
        {
            %this.callBackSink.onDone(%this);
        }
        if (%this.wasInterrupted)
        {
            checkForPackageUpdates(1);
            echo("resuming normal download ...");
        }
        %this.callBackSink = "";
        return ;
    }
    %name = "packageDownloadClass" @ getRandom(0, 1000);
    %curl = new URLPostObject(%name);
    if (isObject(MissionCleanup))
    {
        MissionCleanup.add(%curl);
    }
    %this.CURLObject = %curl;
    %file = %this.missingPackages.getKey(%this.currentItem);
    if (%file $= "")
    {
        echo("Skipping empty file.");
        %this.currentItem = %this.currentItem + 1;
        %this.downloadFile();
        return ;
    }
    echo("Fetching " @ %file @ "(" @ %this.currentItem @ ")");
    %url = $Asset::DownloadURL @ "/" @ %file;
    %curl.setURL(%url);
    %curl.setDownloadFile(%file);
    %curl.setRecvData(1);
    %curl.setProgress(1);
    %curl.setEncodings("deflate,gzip");
    echo("City threshold speed set to " @ $PackageDownload::MinimumCityDownloadSpeedThreshold @ " bytes/sec");
    %curl.setLowSpeedLimit($PackageDownload::MinimumCityDownloadSpeedThreshold);
    %curl.setLowSpeedTime($PackageDownload::MinimumCityDownloadTimeThreshold);
    %curl.callBackSink = %this;
    %curl.NoAutoDelete = 1;
    %curl.packageName = %file;
    %curl.setCompletedCallback("packageDownload_onCompletedDownload");
    %this.statusMap.put(%file, "started");
    if (!%curl.start())
    {
        %this.statusMap.put(%file, "failed");
        %curl.delete();
        echo("Problems starting download of " @ %url @ " to " @ %file);
        return ;
    }
    CURLSimGroup.add(%curl);
    %this.currentItem = %this.currentItem + 1;
    return ;
}
function packageDownload::onError(%this, %request, %errNo)
{
    error("Problems downloading: " @ %request.getDownloadFile());
    if ((%errNo == $CURL::OperationTimedOut) && (%this.retryCount < 3))
    {
        echo("Retrying timed-out file " @ %request.getDownloadFile() @ " (Attempt #" @ %this.retryCount + 1 @ ")");
        %this.retryCount = %this.retryCount + 1;
        %request.restart();
    }
    else
    {
        %this.statusMap.put(%request.getDownloadFile(), "error");
        %request.schedule(0, delete);
        %this.downloadFile();
    }
    return ;
}
function packageDownload_onCompletedDownload(%request, %result)
{
    if (%result == 0)
    {
        packageDownload.onDone(%request.packageName);
    }
    else
    {
        packageDownload.onError(%request, %result);
    }
    return ;
}
function packageDownload::onDone(%this, %packageName)
{
    echo("Got file " @ %packageName);
    AssetManager::updatePackageHash(%packageName);
    rescanDir("projects");
    %this.statusMap.put(%packageName, "done");
    WorldMap.UpdateCityStatuses();
    %this.downloadFile();
    return ;
}
function packageDownload::onProgress(%this, %this2, %dltotal, %dlnow)
{
    if (isObject(%this.callBackSink))
    {
        %this.callBackSink.onProgress(%dltotal, %dlnow);
    }
    packageDownload.bytesDownloadedMap.put(%this2.packageName, %dlnow);
    if (textureDownloadQueuedCount() > $PackageDownload::ConcurrentTextureThreshold)
    {
        if (!$seenThrottleMessage)
        {
            echo("textureDownloadQueuedCount is > " @ $PackageDownload::ConcurrentTextureThreshold @ ". Throttling city download.");
            $seenThrottleMessage = 1;
            $seenUnThrottleMessage = 0;
        }
        %this.CURLObject.setMaxDownloadSpeed($PackageDownload::ConcurrentTextureMaxBytes);
    }
    else
    {
        if (!$seenUnThrottleMessage)
        {
            echo("Texture DownloadQueuedCount is < " @ $PackageDownload::ConcurrentTextureThreshold @ ". Un-throttling city download.");
            $seenUnThrottleMessage = 1;
            $seenThrottleMessage = 0;
        }
        %this.CURLObject.setMaxDownloadSpeed(0);
    }
    return ;
}
function packageDownload::getCurrentItem(%this)
{
    return %this.missingPackages.getKey(%this.currentItem - 1);
}
function packageDownload::getCurrentPackageIndex(%this)
{
    return %this.currentItem;
}
function packageDownload::getTotalPackages(%this)
{
    return %this.missingPackages.count();
}
function packageDownload::getCurrentCityName(%this)
{
    %package = %this.missingPackages.getKey(%this.currentItem - 1);
    if (%package $= "")
    {
        return "nothing";
    }
    %package = getSubStr(strrchr(%package, "/"), 1, 100);
    %city = AssetManager::packageToCity(%package);
    if (%city $= "")
    {
        %city = "common";
    }
    return %city;
}
function packageDownload::getItemStatus(%this, %package)
{
    if (isObject(%this.statusMap))
    {
        return %this.statusMap.get(%package);
    }
    else
    {
        return "incomplete";
    }
    return ;
}
function packageDownload::getCurrentItemStatus(%this)
{
    return %this.statusMap.get(%this.getCurrentItem());
}
function packageDownload::getPercentComplete(%this, %city)
{
    %package = AssetManager::cityToPackage(%city);
    if (isObject(%this.statusMap) && (%this.statusMap.get(%package) $= "incomplete"))
    {
        return 0;
    }
    if (%this.statusMap.get(%package) $= "done")
    {
        return 1;
    }
    %currentDownloaded = %this.bytesDownloadedMap.get(%package);
    if ((%currentDownloaded == 0) && (%currentDownloaded $= ""))
    {
        return 0;
    }
    if (%package $= $AssetManager::COMMONPACKAGE)
    {
        %percent = %currentDownloaded / $PackageDownload::GuestimatedCommonSize;
    }
    else
    {
        %percent = %currentDownloaded / $PackageDownload::GuestimatedSize;
    }
    return %percent;
}
function packageDownload::getStatus(%this)
{
    return %this.getCurrentItemStatus();
}
function packageDownload::getStatusForCity(%this, %city)
{
    %package = AssetManager::cityToPackage(%city);
    %status = %this.getItemStatus(%package);
    %common_status = %this.getItemStatus($AssetManager::COMMONPACKAGE);
    if (((((%status $= "") || (%status $= "done")) && (%city $= "gw")) || (%common_status $= "")) || (%common_status $= "done"))
    {
        return "done";
    }
    else
    {
        return %status;
    }
    return ;
}
function checkForPackageUpdates(%startDownload)
{
    if (!isValidHostAddress($Net::DownloadHost))
    {
        return ;
    }
    %request = new ManagerRequest();
    if (isObject(MissionCleanup))
    {
        MissionCleanup.add(%request);
    }
    if (%startDownload)
    {
        %request.startDownload = %startDownload;
    }
    else
    {
        %request.startDownload = 0;
    }
    %request.setURL($Asset::DownloadURL @ "/checksums_resp.txt");
    %request.start();
    return ;
}
function packageDownloadCheck::onError(%this, %unused, %errorName)
{
    log("Admin", "error", getScopeName() SPC getDebugString(%this) SPC "- error = " SPC %errorName SPC "url = " SPC %this.getURL());
    log("Admin", "error", "Could not contact download site. Turning off package download.");
    $AutoDownloadPackages = 0;
    %this.schedule(0, delete);
    return ;
}
function packageDownloadCheck::onDone(%this)
{
    %status = findRequestStatus(%this);
    log("network", "info", getScopeName() SPC "- packageDownloadCheck status =" SPC %status SPC "url =" SPC %this.getURL());
    if (!(%status $= "success"))
    {
        log("Admin", "error", getScopeName() SPC "- status =" SPC %status);
        log("Admin", "error", "Could not contact download site. Turning off package download.");
        $AutoDownloadPackages = 0;
        %this.schedule(0, delete);
        return ;
    }
    %map = AssetManager::StringToMap(AssetManager::getCurrentAssetSet());
    %badpackages = new Array();
    if (isObject(MissionCleanup))
    {
        MissionCleanup.add(%badpackages);
    }
    %available = %this.getValue("client_version");
    %buildVersion = formatInt("%d", getBuildVersion());
    %protocolVersion = formatInt("%d", getProtocolVersion());
    if ((%available > %buildVersion) && (%available > %protocolVersion))
    {
        echo("There\'s a newer client version available. Letting normal upgrade process take over from here.");
        queuePackageUpdates(%badpackages);
        return ;
    }
    %orderMap = AssetManager::getPackageOrder();
    %tempOrderArray = new Array();
    if (isObject(MissionCleanup))
    {
        MissionCleanup.add(%tempOrderArray);
    }
    %n = 0;
    while (%n < %orderMap.size())
    {
        %key = %map.getKey(%n);
        if (%key $= "")
        {
            continue;
        }
        %remoteValue = %this.getValue(%key);
        if (!packageUpToDate(%map.get(%key), %remoteValue))
        {
            %tempOrderArray.push_back(%key, %orderMap.get(%key));
        }
        %n = %n + 1;
    }
    %tempOrderArray.sorta();
    %n = 0;
    while (%n < %tempOrderArray.count())
    {
        %key = %tempOrderArray.getKey(%n);
        %badpackages.push_back(%key, %n);
        %n = %n + 1;
    }
    %badpackages.sorta();
    %tempOrderArray.delete();
    queuePackageUpdates(%badpackages);
    if (%this.startDownload == 1)
    {
        echo("Starting download of package updates.");
        downloadPackageUpdates();
    }
    %this.schedule(0, delete);
    return ;
}
