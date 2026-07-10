$seenThrottleMessage = 0;
$seenUnThrottleMessage = 0;
$PackageDownload::ConcurrentTextureThreshold = 10;
$PackageDownload::ConcurrentTextureMaxBytes = 10000;
$PackageDownload::MinimumCityDownloadSpeedThreshold = (1.0 * 1024.0);
$PackageDownload::MinimumCityDownloadTimeThreshold = 30;
$PackageDownload::GuestimatedSize = ((40.0 * 1024.0) * 1024.0);
$PackageDownload::GuestimatedCommonSize = ((5.0 * 1024.0) * 1024.0);
function queuePackageUpdatesByString(%missing) {
    %missingA = AssetManager::StringToArray(%missing);
    %missingA.reinit(packageDownload);
};
function queuePackageUpdates(%missing) {
    if (!(isObject(packageDownload))) {
        new ScriptObject(packageDownload);
        if (isObject(MissionCleanup)) {
            packageDownload.add(MissionCleanup);
        }
    }
    if (packageDownload.isActive()) {
        return;
    }
    if (!(isObject(%missing))) {
        %missing = AssetManager::getMissingAssets();
    }
    packageDownload.missingPackages = %missing;
    packageDownload.init();
};
function downloadPackageUpdates(%missing) {
    if (!(isObject(packageDownload))) {
    }
    if (isObject(%missing)) {
        queuePackageUpdates(%missing);
    }
    packageDownload.start();
};
function packageDownload::reinit(%this, %missingArray) {
    %realCurrentItem = (%this.currentItem - 1.0);
    %currentKey = %realCurrentItem.getKey(%this.missingPackages);
    %newMissingArray = new Array("");
    if (isObject(MissionCleanup)) {
        %newMissingArray.add(MissionCleanup);
    }
    %newStatusMap = new StringMap("");
    if (isObject(MissionCleanup)) {
        %newStatusMap.add(MissionCleanup);
    }
    %currentKey.get(%this.statusMap).put(%newStatusMap, %currentKey);
    0.push_back(%newMissingArray, %currentKey);
    if ((0.getKey(%missingArray) $= %currentKey)) {
        echo("Currently downloading " @ %currentKey @ ". Will truncate current download session.");
    }
    %i = 0;
    while ((%i < %missingArray.count())) {
        %key = %i.getKey(%missingArray);
        "incomplete".put(%newStatusMap, %key);
        (%i + 1.0).push_back(%newMissingArray, %key);
        %i = (%i + 1.0);
    }
    %newMissingArray.sorta();
    echo("Shifting currentItem to start. Putting new lists in place.");
    %this.wasInterrupted = (%i < %missingArray.count()) @ 1;
    %this.currentItem = 1;
    %this.statusMap.delete();
    %this.statusMap = %newStatusMap;
    %this.missingPackages.delete();
    %this.missingPackages = %newMissingArray;
};
function packageDownload::init(%this) {
    %this.currentItem = 0;
    %this.wasInterrupted = 0;
    %this.statusMap = new StringMap("");
    if (isObject(MissionCleanup)) {
        %this.statusMap.add(MissionCleanup);
    }
    %this.bytesDownloadedMap = new StringMap("");
    if (isObject(MissionCleanup)) {
        %this.bytesDownloadedMap.add(MissionCleanup);
    }
    %this.isActive = 0;
    %i = 0;
    while ((%i < %this.missingPackages.count())) {
        %key = %i.getKey(%this.missingPackages);
        "incomplete".put(%this.statusMap, %key);
        0.put(%this.bytesDownloadedMap, %key);
        %i = (%i + 1.0);
    }
    return 1;
};
function packageDownload::isActive(%this) {
    return %this.isActive;
};
function packageDownload::isDone(%this) {
    %i = 0;
    while ((%i < %this.statusMap.size())) {
        if ((%i.getValue(%this.statusMap) $= "incomplete")) {
            return 0;
        }
        %i = (%i + 1.0);
    }
    return 1;
};
function packageDownload::completedSuccessfully(%this) {
    %i = 0;
    while ((%i < %this.statusMap.size())) {
        if ((%i.getValue(%this.statusMap) $= "error")) {
            return 0;
        }
        %i = (%i + 1.0);
    }
    return 1;
};
function packageDownload::getEstimatedSize(%this) {
    %total = 0;
    %i = 0;
    while ((%i < %this.statusMap.size())) {
        if ((%i.getKey(%this.statusMap) $= $AssetManager::COMMONPACKAGE)) {
            %total = (%total + $PackageDownload::GuestimatedCommonSize);
        }
        %total = (%total + $PackageDownload::GuestimatedSize);
        %i = (%i + 1.0);
    }
    return %total;
};
function packageDownload::start(%this) {
    if (!(isObject(%this.missingPackages))) {
    }
    if ((%this.missingPackages.count() == 0.0)) {
        return 0;
    }
    echo("Starting package download.");
    %this.isActive = 1;
    %this.downloadFile();
};
function packageDownload::doneDownloading(%this) {
    if ((%this.currentItem >= %this.missingPackages.count())) {
        return 1;
    }
    return 0;
};
function packageDownload::downloadFile(%this) {
    if (%this.doneDownloading()) {
        %this.isActive = 0;
        if (!(%this.completedSuccessfully())) {
            MessageBoxOK("Could not download", "Could not download required files. Click OK to logout.", "buttonBarMenuLogout();");
            return;
        }
        if (isObject(%this.callBackSink)) {
            %this.onDone(%this.callBackSink);
        }
        if (%this.wasInterrupted) {
            checkForPackageUpdates(1);
            echo("resuming normal download ...");
        }
        %this.callBackSink = "";
        return;
    }
    %name = "packageDownloadClass" @ getRandom(0, 1000);
    %curl = new URLPostObject(%name);
    if (isObject(MissionCleanup)) {
        %curl.add(MissionCleanup);
    }
    %this.CURLObject = %curl;
    %file = %this.currentItem.getKey(%this.missingPackages);
    if ((%file $= "")) {
        echo("Skipping empty file.");
        %this.currentItem = (%this.currentItem + 1.0);
        %this.downloadFile();
        return;
    }
    echo("Fetching " @ %file @ "(" @ %this.currentItem @ ")");
    %url = $Asset::DownloadURL @ "/" @ %file;
    %url.setURL(%curl);
    %file.setDownloadFile(%curl);
    1.setRecvData(%curl);
    1.setProgress(%curl);
    "deflate,gzip".setEncodings(%curl);
    echo("City threshold speed set to " @ $PackageDownload::MinimumCityDownloadSpeedThreshold @ " bytes/sec");
    $PackageDownload::MinimumCityDownloadSpeedThreshold.setLowSpeedLimit(%curl);
    $PackageDownload::MinimumCityDownloadTimeThreshold.setLowSpeedTime(%curl);
    %curl.callBackSink = %this;
    %curl.NoAutoDelete = 1;
    %curl.packageName = %file;
    "packageDownload_onCompletedDownload".setCompletedCallback(%curl);
    "started".put(%this.statusMap, %file);
    if (!(%curl.start())) {
        "failed".put(%this.statusMap, %file);
        %curl.delete();
        echo("Problems starting download of " @ %url @ " to " @ %file);
        return;
    }
    %curl.add(CURLSimGroup);
    %this.currentItem = (%this.currentItem + 1.0);
};
function packageDownload::onError(%this, %request, %errNo) {
    error("Problems downloading: " @ %request.getDownloadFile());
    if ((%errNo == $CURL::OperationTimedOut)) {
    }
    if ((%this.retryCount < 3.0)) {
        echo("Retrying timed-out file " @ %request.getDownloadFile() @ " (Attempt #" @ (%this.retryCount + 1.0) @ ")");
        %this.retryCount = (%this.retryCount + 1.0);
        %request.restart();
    }
    "error".put(%this.statusMap, %request.getDownloadFile());
    delete.schedule(%request, 0);
    %this.downloadFile();
};
function packageDownload_onCompletedDownload(%request, %result) {
    if ((%result == 0.0)) {
        %request.packageName.onDone(packageDownload);
    }
    %result.onError(packageDownload, %request);
};
function packageDownload::onDone(%this, %packageName) {
    echo("Got file " @ %packageName);
    AssetManager::updatePackageHash(%packageName);
    rescanDir("projects");
    "done".put(%this.statusMap, %packageName);
    WorldMap.UpdateCityStatuses();
    %this.downloadFile();
};
function packageDownload::onProgress(%this, %this2, %dltotal, %dlnow) {
    if (isObject(%this.callBackSink)) {
        %dlnow.onProgress(%this.callBackSink, %dltotal);
    }
    %dlnow.put(packageDownload.bytesDownloadedMap, %this2.packageName);
    if ((textureDownloadQueuedCount() > $PackageDownload::ConcurrentTextureThreshold)) {
        if (!($seenThrottleMessage)) {
            echo("textureDownloadQueuedCount is > " @ $PackageDownload::ConcurrentTextureThreshold @ ". Throttling city download.");
            $seenThrottleMessage = 1;
            $seenUnThrottleMessage = 0;
        }
        $PackageDownload::ConcurrentTextureMaxBytes.setMaxDownloadSpeed(%this.CURLObject);
    }
    if (!($seenUnThrottleMessage)) {
        echo("Texture DownloadQueuedCount is < " @ $PackageDownload::ConcurrentTextureThreshold @ ". Un-throttling city download.");
        $seenUnThrottleMessage = 1;
        $seenThrottleMessage = 0;
    }
    0.setMaxDownloadSpeed(%this.CURLObject);
};
function packageDownload::getCurrentItem(%this) {
    return (%this.currentItem - 1.0).getKey(%this.missingPackages);
};
function packageDownload::getCurrentPackageIndex(%this) {
    return %this.currentItem;
};
function packageDownload::getTotalPackages(%this) {
    return %this.missingPackages.count();
};
function packageDownload::getCurrentCityName(%this) {
    %package = (%this.currentItem - 1.0).getKey(%this.missingPackages);
    if ((%package $= "")) {
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
    if (isObject(%this.statusMap)) {
        return %package.get(%this.statusMap);
    }
    return "incomplete";
};
function packageDownload::getCurrentItemStatus(%this) {
    return %this.getCurrentItem().get(%this.statusMap);
};
function packageDownload::getPercentComplete(%this, %city) {
    %package = AssetManager::cityToPackage(%city);
    if (isObject(%this.statusMap)) {
    }
    if ((%package.get(%this.statusMap) $= "incomplete")) {
        return 0;
    }
    if ((%package.get(%this.statusMap) $= "done")) {
        return 1;
    }
    %currentDownloaded = %package.get(%this.bytesDownloadedMap);
    if ((%currentDownloaded == 0.0)) {
    }
    if ((%currentDownloaded $= "")) {
        return 0;
    }
    if ((%package $= $AssetManager::COMMONPACKAGE)) {
        %percent = (%currentDownloaded / $PackageDownload::GuestimatedCommonSize);
    }
    %percent = (%currentDownloaded / $PackageDownload::GuestimatedSize);
    return %percent;
};
function packageDownload::getStatus(%this) {
    return %this.getCurrentItemStatus();
};
function packageDownload::getStatusForCity(%this, %city) {
    %package = AssetManager::cityToPackage(%city);
    %status = %package.getItemStatus(%this);
    %common_status = $AssetManager::COMMONPACKAGE.getItemStatus(%this);
    if ((%status $= "")) {
    }
    if ((%status $= "done") && (%city $= "gw") && (%common_status $= "")) {
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
    %request = new ManagerRequest("") {
        className = "packageDownloadCheck";
    };
    if (isObject(MissionCleanup)) {
        %request.add(MissionCleanup);
    }
    if (%startDownload) {
        %request.startDownload = %startDownload;
    }
    %request.startDownload = 0;
    $Asset::DownloadURL @ "/checksums_resp.txt".setURL(%request);
    %request.start();
};
function packageDownloadCheck::onError(%this, %unused, %errorName) {
    log("Admin", "error", getScopeName() @ " " @ getDebugString(%this) @ " " @ "- error = " @ " " @ %errorName @ " " @ "url = " @ " " @ %this.getURL());
    log("Admin", "error", "Could not contact download site. Turning off package download.");
    $AutoDownloadPackages = 0;
    delete.schedule(%this, 0);
};
function packageDownloadCheck::onDone(%this) {
    %status = findRequestStatus(%this);
    log("network", "info", getScopeName() @ " " @ "- packageDownloadCheck status =" @ " " @ %status @ " " @ "url =" @ " " @ %this.getURL());
    if (!(%status $= "success")) {
        log("Admin", "error", getScopeName() @ " " @ "- status =" @ " " @ %status);
        log("Admin", "error", "Could not contact download site. Turning off package download.");
        $AutoDownloadPackages = 0;
        delete.schedule(%this, 0);
        return;
    }
    %map = AssetManager::StringToMap(AssetManager::getCurrentAssetSet());
    %badpackages = new Array("");
    if (isObject(MissionCleanup)) {
        %badpackages.add(MissionCleanup);
    }
    %available = "client_version".getValue(%this);
    %buildVersion = formatInt("%d", getBuildVersion());
    %protocolVersion = formatInt("%d", getProtocolVersion());
    if ((%available > %buildVersion)) {
    }
    if ((%available > %protocolVersion)) {
        echo("There's a newer client version available. Letting normal upgrade process take over from here.");
        queuePackageUpdates(%badpackages);
        return;
    }
    %orderMap = AssetManager::getPackageOrder();
    %tempOrderArray = new Array("");
    if (isObject(MissionCleanup)) {
        %tempOrderArray.add(MissionCleanup);
    }
    %n = 0;
    while ((%n < %orderMap.size())) {
        %key = %n.getKey(%map);
        if ((%key $= "")) {
        }
        %remoteValue = %key.getValue(%this);
        if (!(packageUpToDate(%key.get(%map), %remoteValue))) {
            %key.get(%orderMap).push_back(%tempOrderArray, %key);
        }
        %n = (%n + 1.0);
    }
    %tempOrderArray.sorta();
    %n = 0;
    (%n < %orderMap.size());
    while ((%n < %tempOrderArray.count())) {
        %key = %n.getKey(%tempOrderArray);
        %n.push_back(%badpackages, %key);
        %n = (%n + 1.0);
    }
    %badpackages.sorta();
    %tempOrderArray.delete();
    queuePackageUpdates(%badpackages);
    if ((%this.startDownload == 1.0)) {
        echo("Starting download of package updates.");
        downloadPackageUpdates();
    }
    delete.schedule(%this, 0);
};
