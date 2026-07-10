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
    packageDownload.reinit(%missingA);
};
function queuePackageUpdates(%missing) {
    if (!(isObject(packageDownload))) {
        new ScriptObject(packageDownload);
        if (isObject(MissionCleanup)) {
            MissionCleanup.add(packageDownload);
        }
    }
    if (packageDownload.isActive(packageDownload)) {
        return;
    }
    if (!(isObject(%missing))) {
        %missing = AssetManager::getMissingAssets();
    }
    missingPackages = %missing @ packageDownload;
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
    %realCurrentItem = (1.0 - %this.currentItem);
    %currentKey = %this.missingPackages.getKey(%realCurrentItem);
    %newMissingArray = new Array("");;
    0;
    if (isObject(MissionCleanup)) {
        MissionCleanup.add(%newMissingArray);
    }
    %newStatusMap = new StringMap("");;
    0;
    if (isObject(MissionCleanup)) {
        MissionCleanup.add(%newStatusMap);
    }
    %newStatusMap.put(%currentKey, %this.statusMap.get(%currentKey));
    %newMissingArray.push_back(%currentKey, 0);
    if ((%missingArray.getKey(0) $= %currentKey)) {
        echo("Currently downloading " @ %currentKey @ ". Will truncate current download session.");
    }
    %i = 0;
    if ((%missingArray.count() < %i)) {
        %key = %missingArray.getKey(%i);
        %newStatusMap.put(%key, "incomplete");
        %newMissingArray.push_back(%key, (1.0 + %i));
        %i = (1.0 + %i);
    }
    %newMissingArray.sorta();
    echo("Shifting currentItem to start. Putting new lists in place.");
    %this.wasInterrupted = (%missingArray.count() < %i) @ 1;
    %this.currentItem = 1;
    %this.statusMap.delete();
    %this.statusMap = %newStatusMap;
    %this.missingPackages.delete();
    %this.missingPackages = %newMissingArray;
};
function packageDownload::init(%this) {
    %this.currentItem = 0;
    %this.wasInterrupted = 0;
    %this.statusMap = 0 @ new StringMap("");;
    if (isObject(MissionCleanup)) {
        MissionCleanup.add(%this.statusMap);
    }
    %this.bytesDownloadedMap = 0 @ new StringMap("");;
    if (isObject(MissionCleanup)) {
        MissionCleanup.add(%this.bytesDownloadedMap);
    }
    %this.isActive = 0;
    %i = 0;
    if ((%this.missingPackages.count() < %i)) {
        %key = %this.missingPackages.getKey(%i);
        %this.statusMap.put(%key, "incomplete");
        %this.bytesDownloadedMap.put(%key, 0);
        %i = (1.0 + %i);
    }
    return 1;
};
function packageDownload::isActive(%this) {
    return %this.isActive;
};
function packageDownload::isDone(%this) {
    %i = 0;
    if ((%this.statusMap.size() < %i)) {
        if ((%this.statusMap.getValue(%i) $= "incomplete")) {
            return 0;
        }
        %i = (1.0 + %i);
    }
    return 1;
};
function packageDownload::completedSuccessfully(%this) {
    %i = 0;
    if ((%this.statusMap.size() < %i)) {
        if ((%this.statusMap.getValue(%i) $= "error")) {
            return 0;
        }
        %i = (1.0 + %i);
    }
    return 1;
};
function packageDownload::getEstimatedSize(%this) {
    %total = 0;
    %i = 0;
    if ((%this.statusMap.size() < %i)) {
        if ((%this.statusMap.getKey(%i) $= $AssetManager::COMMONPACKAGE)) {
            %total = ($PackageDownload::GuestimatedCommonSize + %total);
        }
        %total = ($PackageDownload::GuestimatedSize + %total);
        %i = (1.0 + %i);
    }
    return %total;
};
function packageDownload::start(%this) {
    if (!(isObject(%this.missingPackages))) {
    }
    if ((0.0 == %this.missingPackages.count())) {
        return 0;
    }
    echo("Starting package download.");
    %this.isActive = 1;
    %this.downloadFile();
};
function packageDownload::doneDownloading(%this) {
    if ((%this.missingPackages.count() >= %this.currentItem)) {
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
            %this.callBackSink.onDone(%this);
        }
        if (%this.wasInterrupted) {
            checkForPackageUpdates(1);
            echo("resuming normal download ...");
        }
        %this.callBackSink = "";
        return;
    }
    %name = "packageDownloadClass" @ getRandom(0, 1000);
    %curl = new URLPostObject(%name);;
    0;
    if (isObject(MissionCleanup)) {
        MissionCleanup.add(%curl);
    }
    %this.CURLObject = %curl;
    %file = %this.missingPackages.getKey(%this.currentItem);
    if ((%file $= "")) {
        echo("Skipping empty file.");
        %this.currentItem = (1.0 + %this.currentItem);
        %this.downloadFile();
        return;
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
    if (!(%curl.start())) {
        %this.statusMap.put(%file, "failed");
        %curl.delete();
        echo("Problems starting download of " @ %url @ " to " @ %file);
        return;
    }
    CURLSimGroup.add(%curl);
    %this.currentItem = (1.0 + %this.currentItem);
};
function packageDownload::onError(%this, %request, %errNo) {
    error("Problems downloading: " @ %request.getDownloadFile());
    if (($CURL::OperationTimedOut == %errNo)) {
    }
    if ((3.0 < %this.retryCount)) {
        echo("Retrying timed-out file " @ %request.getDownloadFile() @ " (Attempt #" @ (1.0 + %this.retryCount) @ ")");
        %this.retryCount = (1.0 + %this.retryCount);
        %request.restart();
    }
    %this.statusMap.put(%request.getDownloadFile(), "error");
    %request.schedule(0);
    %this.downloadFile();
};
function packageDownload_onCompletedDownload(%request, %result) {
    if ((0.0 == %result)) {
        packageDownload.onDone(%request.packageName);
    }
    packageDownload.onError(%request, %result);
};
function packageDownload::onDone(%this, %packageName) {
    echo("Got file " @ %packageName);
    AssetManager::updatePackageHash(%packageName);
    rescanDir("projects");
    %this.statusMap.put(%packageName, "done");
    WorldMap.UpdateCityStatuses();
    %this.downloadFile();
};
function packageDownload::onProgress(%this, %this2, %dltotal, %dlnow) {
    if (isObject(%this.callBackSink)) {
        %this.callBackSink.onProgress(%dltotal, %dlnow);
    }
    packageDownload.put(%this.bytesDownloadedMap, %this2.packageName, %dlnow);
    if (($PackageDownload::ConcurrentTextureThreshold > textureDownloadQueuedCount())) {
        if (!($seenThrottleMessage)) {
            echo("textureDownloadQueuedCount is > " @ $PackageDownload::ConcurrentTextureThreshold @ ". Throttling city download.");
            $seenThrottleMessage = 1;
            $seenUnThrottleMessage = 0;
        }
        %this.CURLObject.setMaxDownloadSpeed($PackageDownload::ConcurrentTextureMaxBytes);
    }
    if (!($seenUnThrottleMessage)) {
        echo("Texture DownloadQueuedCount is < " @ $PackageDownload::ConcurrentTextureThreshold @ ". Un-throttling city download.");
        $seenUnThrottleMessage = 1;
        $seenThrottleMessage = 0;
    }
    %this.CURLObject.setMaxDownloadSpeed(0);
};
function packageDownload::getCurrentItem(%this) {
    return %this.missingPackages.getKey((1.0 - %this.currentItem));
};
function packageDownload::getCurrentPackageIndex(%this) {
    return %this.currentItem;
};
function packageDownload::getTotalPackages(%this) {
    return %this.missingPackages.count();
};
function packageDownload::getCurrentCityName(%this) {
    %package = %this.missingPackages.getKey((1.0 - %this.currentItem));
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
        return %this.statusMap.get(%package);
    }
    return "incomplete";
};
function packageDownload::getCurrentItemStatus(%this) {
    return %this.statusMap.get(%this.getCurrentItem());
};
function packageDownload::getPercentComplete(%this, %city) {
    %package = AssetManager::cityToPackage(%city);
    if (isObject(%this.statusMap)) {
    }
    if ((%this.statusMap.get(%package) $= "incomplete")) {
        return 0;
    }
    if ((%this.statusMap.get(%package) $= "done")) {
        return 1;
    }
    %currentDownloaded = %this.bytesDownloadedMap.get(%package);
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
    %request = new ManagerRequest("") {
        className = 0 @ "packageDownloadCheck";
    };
    if (isObject(MissionCleanup)) {
        MissionCleanup.add(%request);
    }
    if (%startDownload) {
        %request.startDownload = %startDownload;
    }
    %request.startDownload = 0;
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
    %badpackages = new Array("");;
    0;
    if (isObject(MissionCleanup)) {
        MissionCleanup.add(%badpackages);
    }
    %available = %this.getValue("client_version");
    %buildVersion = formatInt("%d", getBuildVersion());
    %protocolVersion = formatInt("%d", getProtocolVersion());
    if ((%buildVersion > %available)) {
    }
    if ((%protocolVersion > %available)) {
        echo("There's a newer client version available. Letting normal upgrade process take over from here.");
        queuePackageUpdates(%badpackages);
        return;
    }
    %orderMap = AssetManager::getPackageOrder();
    %tempOrderArray = new Array("");;
    0;
    if (isObject(MissionCleanup)) {
        MissionCleanup.add(%tempOrderArray);
    }
    %n = 0;
    if ((%orderMap.size() < %n)) {
        %key = %map.getKey(%n);
        if ((%key $= "")) {
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
    if ((1.0 == %this.startDownload)) {
        echo("Starting download of package updates.");
        downloadPackageUpdates();
    }
    %this.schedule(0);
};
