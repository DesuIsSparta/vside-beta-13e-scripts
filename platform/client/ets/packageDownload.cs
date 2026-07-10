$seenThrottleMessage = 0;
$seenUnThrottleMessage = 0;
$PackageDownload::ConcurrentTextureThreshold = 10;
$PackageDownload::ConcurrentTextureMaxBytes = 10000;
$PackageDownload::MinimumCityDownloadSpeedThreshold = (1024.0 * 1.0);
$PackageDownload::MinimumCityDownloadTimeThreshold = 30;
$PackageDownload::GuestimatedSize = (1024.0 * (1024.0 * 40.0));
$PackageDownload::GuestimatedCommonSize = (1024.0 * (1024.0 * 5.0));
function queuePackageUpdatesByString(%missing) {
    %missingA = AssetManager::StringToArray(%missing);
    %missingA.reinit();
};
function queuePackageUpdates(%missing) {
    if (!(isObject())) {
        new ScriptObject(packageDownload);
        if (isObject()) {
            add();
        }
    }
    if (isActive()) {
        return packageDownload;
    }
    if (!(isObject(%missing))) {
        %missing = AssetManager::getMissingAssets();
    }
    missingPackages = %missing @ packageDownload;
    init();
};
function downloadPackageUpdates(%missing) {
    if (!(isObject())) {
    }
    if (isObject(%missing)) {
        queuePackageUpdates(%missing);
    }
    start();
};
function packageDownload::reinit(%this, %missingArray) {
    %realCurrentItem = (%this - currentItem);
    1.0;
    %currentKey = missingPackages.getKey(%realCurrentItem);
    %this;
    %newMissingArray = new ""();
    Array;
    if (isObject()) {
        %newMissingArray.add();
    }
    %newStatusMap = new ""();
    StringMap;
    if (isObject()) {
        %newStatusMap.add();
    }
    %newStatusMap.put(%currentKey, statusMap.get(%currentKey));
    %newMissingArray.push_back(%currentKey, 0);
    if ((%this SPC %missingArray.getKey(0) $= %currentKey)) {
        echo(MissionCleanup @ MissionCleanup @ "Currently downloading " @ %currentKey @ ". Will truncate current download session.");
    }
    %i = 0;
    0;
    if ((%missingArray.count() < %i)) {
        %key = %missingArray.getKey(%i);
        MissionCleanup;
        %newStatusMap.put(%key, "incomplete");
        %newMissingArray.push_back(%key, (1.0 + %i));
        %i = (1.0 + %i);
        MissionCleanup;
    }
    %newMissingArray.sorta();
    echo("Shifting currentItem to start. Putting new lists in place.");
    wasInterrupted = (%missingArray.count() < %i) @ 1 @ %this;
    0;
    currentItem = 1 @ %this;
    statusMap.delete();
    statusMap = %this @ %newStatusMap @ %this;
    missingPackages.delete();
    missingPackages = %this @ %newMissingArray @ %this;
};
function packageDownload::init(%this) {
    currentItem = 0 @ %this;
    wasInterrupted = 0 @ %this;
    statusMap = StringMap @ new ""() @ %this;
    0;
    if (isObject()) {
        statusMap.add();
    }
    bytesDownloadedMap = StringMap @ new ""() @ %this;
    0;
    if (isObject()) {
        bytesDownloadedMap.add();
    }
    isActive = %this @ 0 @ %this;
    MissionCleanup;
    %i = 0;
    MissionCleanup;
    if ((missingPackages.count() < %i)) {
        %key = missingPackages.getKey(%i);
        %this;
        statusMap.put(%key, "incomplete");
        bytesDownloadedMap.put(%key, 0);
        %i = (1.0 + %i);
        %this;
    }
    return 1;
};
function packageDownload::isActive(%this) {
    return isActive;
};
function packageDownload::isDone(%this) {
    %i = 0;
    if ((statusMap.size() < %i)) {
        if ((%this SPC statusMap.getValue(%i) $= "incomplete")) {
            return 0;
        }
        %i = (1.0 + %i);
    }
    return 1;
};
function packageDownload::completedSuccessfully(%this) {
    %i = 0;
    if ((statusMap.size() < %i)) {
        if ((%this SPC statusMap.getValue(%i) $= "error")) {
            return 0;
        }
        %i = (1.0 + %i);
    }
    return 1;
};
function packageDownload::getEstimatedSize(%this) {
    %total = 0;
    %i = 0;
    if ((statusMap.size() < %i)) {
        if ((%this SPC statusMap.getKey(%i) $= $AssetManager::COMMONPACKAGE)) {
            %total = ($PackageDownload::GuestimatedCommonSize + %total);
            %this;
        }
        %total = ($PackageDownload::GuestimatedSize + %total);
        %i = (1.0 + %i);
    }
    return %total;
};
function packageDownload::start(%this) {
    if (!(isObject(missingPackages))) {
    }
    if ((%this == missingPackages.count())) {
        return 0;
    }
    echo("Starting package download.");
    isActive = 1 @ %this;
    %this.downloadFile();
};
function packageDownload::doneDownloading(%this) {
    if ((%this >= currentItem)) {
        return 1;
    }
    return 0;
};
function packageDownload::downloadFile(%this) {
    if (%this.doneDownloading()) {
        isActive = 0 @ %this;
        if (!(%this.completedSuccessfully())) {
            MessageBoxOK("Could not download", "Could not download required files. Click OK to logout.", "buttonBarMenuLogout();");
            return;
        }
        if (isObject(callBackSink)) {
            callBackSink.onDone(%this);
        }
        if (wasInterrupted) {
            checkForPackageUpdates(1);
            echo("resuming normal download ...");
        }
        callBackSink = %this @ "" @ %this;
        %this;
        return %this;
    }
    %name = "packageDownloadClass" @ getRandom(0, 1000);
    %curl = new %name();
    URLPostObject;
    if (isObject()) {
        %curl.add();
    }
    CURLObject = MissionCleanup @ %curl @ %this;
    MissionCleanup;
    %file = missingPackages.getKey(currentItem);
    %this;
    if ((%this SPC %file $= "")) {
        echo("Skipping empty file.");
        currentItem = (%this + currentItem);
        1.0;
        %this.downloadFile();
        return 0;
    }
    echo("Fetching " @ %file @ "(" @ %this @ currentItem @ ")");
    %url = $Asset::DownloadURL @ "/" @ %file;
    %curl.setURL(%url);
    %curl.setDownloadFile(%file);
    %curl.setRecvData(1);
    %curl.setProgress(1);
    %curl.setEncodings("deflate,gzip");
    echo("City threshold speed set to " @ $PackageDownload::MinimumCityDownloadSpeedThreshold @ " bytes/sec");
    %curl.setLowSpeedLimit($PackageDownload::MinimumCityDownloadSpeedThreshold);
    %curl.setLowSpeedTime($PackageDownload::MinimumCityDownloadTimeThreshold);
    callBackSink = %this @ %curl;
    NoAutoDelete = 1 @ %curl;
    packageName = %file @ %curl;
    %curl.setCompletedCallback("packageDownload_onCompletedDownload");
    statusMap.put(%file, "started");
    if (!(%curl.start())) {
        statusMap.put(%file, "failed");
        %curl.delete();
        echo(%this @ %this @ "Problems starting download of " @ %url @ " to " @ %file);
        return;
    }
    %curl.add();
    currentItem = (%this + currentItem);
    1.0;
};
function packageDownload::onError(%this, %request, %errNo) {
    error("Problems downloading: " @ %request.getDownloadFile());
    if (($CURL::OperationTimedOut == %errNo)) {
    }
    if ((%this < retryCount)) {
        echo(3.0 @ "Retrying timed-out file " @ %request.getDownloadFile() @ " (Attempt #" @ 1.0 @ (%this + retryCount) @ ")");
        retryCount = (%this + retryCount);
        1.0;
        %request.restart();
    }
    statusMap.put(%request.getDownloadFile(), "error");
    %request.schedule(0);
    %this.downloadFile();
};
function packageDownload_onCompletedDownload(%request, %result) {
    if ((0.0 == %result)) {
        packageName.onDone();
    }
    %request.onError(%result);
};
function packageDownload::onDone(%this, %packageName) {
    echo("Got file " @ %packageName);
    AssetManager::updatePackageHash(%packageName);
    rescanDir("projects");
    statusMap.put(%packageName, "done");
    UpdateCityStatuses();
    %this.downloadFile();
};
function packageDownload::onProgress(%this, %this2, %dltotal, %dlnow) {
    if (isObject(callBackSink)) {
        callBackSink.onProgress(%dltotal, %dlnow);
    }
    bytesDownloadedMap.put(packageName, %dlnow);
    if (($PackageDownload::ConcurrentTextureThreshold > textureDownloadQueuedCount())) {
        if (!($seenThrottleMessage)) {
            echo(packageDownload @ %this2 @ "textureDownloadQueuedCount is > " @ $PackageDownload::ConcurrentTextureThreshold @ ". Throttling city download.");
            $seenThrottleMessage = 1;
            %this;
            $seenUnThrottleMessage = 0;
            %this;
        }
        CURLObject.setMaxDownloadSpeed($PackageDownload::ConcurrentTextureMaxBytes);
    }
    if (!($seenUnThrottleMessage)) {
        echo(%this @ "Texture DownloadQueuedCount is < " @ $PackageDownload::ConcurrentTextureThreshold @ ". Un-throttling city download.");
        $seenUnThrottleMessage = 1;
        $seenThrottleMessage = 0;
    }
    CURLObject.setMaxDownloadSpeed(0);
};
function packageDownload::getCurrentItem(%this) {
    return missingPackages.getKey((%this - currentItem));
};
function packageDownload::getCurrentPackageIndex(%this) {
    return currentItem;
};
function packageDownload::getTotalPackages(%this) {
    return missingPackages.count();
};
function packageDownload::getCurrentCityName(%this) {
    %package = missingPackages.getKey((%this - currentItem));
    1.0;
    if ((%this SPC %package $= "")) {
        return "nothing";
    }
    %package = getSubStr(strrchr(%package, "/"), 1, 100);
    %city = AssetManager::packageToCity(%package);
    if ((%city $= "")) {
        %city = "common";
    }
    return %city;
};
function packageDownload::getItemStatus(%this, %package) {
    if (isObject(statusMap)) {
        return statusMap.get(%package);
    }
    return "incomplete";
};
function packageDownload::getCurrentItemStatus(%this) {
    return statusMap.get(%this.getCurrentItem());
};
function packageDownload::getPercentComplete(%this, %city) {
    %package = AssetManager::cityToPackage(%city);
    if (isObject(statusMap)) {
    }
    if ((%this SPC statusMap.get(%package) $= "incomplete")) {
        return 0;
    }
    if ((%this SPC statusMap.get(%package) $= "done")) {
        return 1;
    }
    %currentDownloaded = bytesDownloadedMap.get(%package);
    %this;
    if ((0.0 == %currentDownloaded)) {
    }
    if ((%currentDownloaded $= "")) {
        return 0;
    }
    if ((%package $= $AssetManager::COMMONPACKAGE)) {
        %percent = ($PackageDownload::GuestimatedCommonSize / %currentDownloaded);
    }
    %percent = ($PackageDownload::GuestimatedSize / %currentDownloaded);
    return %percent;
};
function packageDownload::getStatus(%this) {
    return %this.getCurrentItemStatus();
};
function packageDownload::getStatusForCity(%this, %city) {
    %package = AssetManager::cityToPackage(%city);
    %status = %this.getItemStatus(%package);
    %common_status = %this.getItemStatus($AssetManager::COMMONPACKAGE);
    if ((%status $= "")) {
    }
    if ((%status $= "done")) {
        if ((%city $= "gw")) {
            if ((%common_status $= "")) {
            }
        }
    }
    if ((%common_status $= "done")) {
        return "done";
    }
    return %status;
};
function checkForPackageUpdates(%startDownload) {
    if (!(isValidHostAddress($Net::DownloadHost))) {
        return;
    }
    className = ManagerRequest @ new ""() @ "packageDownloadCheck";
    0;
    %request = ;
    if (isObject()) {
        %request.add();
    }
    if (%startDownload) {
        startDownload = MissionCleanup @ %startDownload @ %request;
        MissionCleanup;
    }
    startDownload = 0 @ %request;
    %request.setURL($Asset::DownloadURL @ "/checksums_resp.txt");
    %request.start();
};
function packageDownloadCheck::onError(%this, %unused, %errorName) {
    log("Admin", "error", getScopeName() @ " " @ getDebugString(%this) @ " " @ "- error = " @ " " @ %errorName @ " " @ "url = " @ " " @ %this.getURL());
    log("Admin", "error", "Could not contact download site. Turning off package download.");
    $AutoDownloadPackages = 0;
    %this.schedule(0);
};
function packageDownloadCheck::onDone(%this) {
    %status = findRequestStatus(%this);
    log("network", "info", getScopeName() @ " " @ "- packageDownloadCheck status =" @ " " @ %status @ " " @ "url =" @ " " @ %this.getURL());
    if (!(%status $= "success")) {
        log("Admin", "error", getScopeName() @ " " @ "- status =" @ " " @ %status);
        log("Admin", "error", "Could not contact download site. Turning off package download.");
        $AutoDownloadPackages = 0;
        %this.schedule(0);
        return delete;
    }
    %map = AssetManager::StringToMap(AssetManager::getCurrentAssetSet());
    %badpackages = new ""();
    Array;
    if (isObject()) {
        %badpackages.add();
    }
    %available = %this.getValue("client_version");
    MissionCleanup;
    %buildVersion = formatInt("%d", getBuildVersion());
    MissionCleanup;
    %protocolVersion = formatInt("%d", getProtocolVersion());
    0;
    if ((%buildVersion > %available)) {
    }
    if ((%protocolVersion > %available)) {
        echo("There's a newer client version available. Letting normal upgrade process take over from here.");
        queuePackageUpdates(%badpackages);
        return;
    }
    %orderMap = AssetManager::getPackageOrder();
    %tempOrderArray = new ""();
    Array;
    if (isObject()) {
        %tempOrderArray.add();
    }
    %n = 0;
    MissionCleanup;
    if ((%orderMap.size() < %n)) {
        %key = %map.getKey(%n);
        MissionCleanup;
        if ((0 SPC %key $= "")) {
        }
        %remoteValue = %this.getValue(%key);
        if (!(packageUpToDate(%map.get(%key), %remoteValue))) {
            %tempOrderArray.push_back(%key, %orderMap.get(%key));
        }
        %n = (1.0 + %n);
    }
    %tempOrderArray.sorta();
    %n = 0;
    (%orderMap.size() < %n);
    if ((%tempOrderArray.count() < %n)) {
        %key = %tempOrderArray.getKey(%n);
        %badpackages.push_back(%key, %n);
        %n = (1.0 + %n);
    }
    %badpackages.sorta();
    %tempOrderArray.delete();
    queuePackageUpdates(%badpackages);
    if ((%this == startDownload)) {
        echo("Starting download of package updates.");
        downloadPackageUpdates();
    }
    %this.schedule(0);
};
