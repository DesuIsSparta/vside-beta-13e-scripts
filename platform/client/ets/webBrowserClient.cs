function dlMgr::smInit() {
    if (isObject(dlMgr)) {
        return;
    }
    new ScriptObject(dlMgr);
    if (isObject(MissionCleanup)) {
        dlMgr.add(MissionCleanup);
    }
    dlMgr.reset();
};
function dlMgr::reset(%this) {
    %this.cacheIndex = safeNewScriptObject("StringMap", "", 0);
    %this.cacheIndexFilename = $DC::GUIFolderName @ "/index.txt";
    %this.loadCacheIndex();
    %this.outstanding = safeNewScriptObject("StringMap", "", 0);
    %this.toDownload = safeNewScriptObject("Array", "", 0);
    %this.failCounts = safeNewScriptObject("StringMap", "", 0);
    %this.policies = safeNewScriptObject("StringMap", "", 0);
    minutesToSeconds(1).setPolicyValue(%this, "default", "expirationDuration");
    1.setPolicyValue(%this, "default", "useStale");
    daysToSeconds(14).setPolicyValue(%this, "default", "cacheDuration");
    minutesToSeconds(1).setPolicyValue(%this, "avatar", "expirationDuration");
    minutesToSeconds(((24.0 * 60.0) * 2.0)).setPolicyValue(%this, "youtube", "expirationDuration");
    %this.maxOutstanding = 10;
    %this.maxFailures = 3;
    %this.retryDelay = 2;
};
function dlMgr::shutDown(%this) {
    %this.purgeCache();
    %this.saveCacheIndex();
};
function dlMgr::setPolicyValue(%this, %policyName, %valuename, %value) {
    %policy = %policyName.getPolicy(%this);
    %value.put(%policy, %valuename);
};
function dlMgr::getPolicyValue(%this, %policyName, %valuename) {
    %policy = %policyName.getPolicy(%this);
    if (!(%valuename.hasKey(%policy))) {
        %policy = "default".getPolicy(%this);
    }
    if (!(%valuename.hasKey(%policy))) {
        error("unknown policy value:" @ " " @ %policyName @ ":" @ %valuename);
        return "";
    }
    return %valuename.get(%policy);
};
function dlMgr::getPolicy(%this, %policyName) {
    %policy = %policyName.get(%this.policies);
    if (!(isObject(%policy))) {
        %policy = safeNewScriptObject("StringMap", "", 0);
        %policy.name = %policyName;
        %policy.put(%this.policies, %policyName);
    }
    return %policy;
};
function dlMgr::applyUrl(%this, %url, %callback, %errorCallback, %callbackData, %policyName) {
    %dlItem = %policyName.buildDLItem(%this, %url, %callback, %errorCallback, %callbackData);
    if ((%dlItem.localFilename $= "")) {
        %dlItem.enqueueItem(%this);
        return;
    }
    %isFresh = 1;
    %record = %dlItem.url.get(%this.cacheIndex);
    if ((%record $= "")) {
        error(getScopeName() @ " " @ "- no entry in cache index." @ " " @ %dlItem.url @ " " @ getTrace());
    }
    %accessTime = getField(%record, 1);
    %age = (getTime() - %accessTime);
    if ((%age > "expirationDuration".getPolicyValue(%this, %policyName))) {
        %isFresh = 0;
    }
    %isFresh.applyItem(%this, %dlItem);
};
function dlMgr::buildDLItem(%this, %url, %callback, %errorCallback, %callbackData, %policyName) {
    if (!(isDefined("%policyName"))) {
    }
    if ((%policyName $= "")) {
        %policyName = "default";
    }
    if (!(isDefined("%errorCallback"))) {
        %errorCallback = "";
    }
    if ((%callback $= "")) {
        %callback = "dlMgrDefaultCallback";
    }
    %dlItem = safeNewScriptObject("ScriptObject", "", 0);
    %dlItem.url = %url;
    %dlItem.callback = %callback;
    %dlItem.errorCallback = %errorCallback;
    %dlItem.callbackData = %callbackData;
    %dlItem.policyName = %policyName;
    %dlItem.localFilename = %url.getCachedFilename(%this);
    if (!(%dlItem.localFilename $= "") && !(isFile(%dlItem.localFilename))) {
        error(getScopeName() @ " " @ "- file missing from cache:" @ " " @ %dlItem.localFilename @ " " @ %dlItem.url @ " " @ getTrace());
        %dlItem.localFilename = "";
    }
    return %dlItem;
};
function dlMgr::getCachedFilename(%this, %url) {
    return getField(%url.get(%this.cacheIndex), 0);
};
function dlMgr::enqueueItem(%this, %dlItem) {
    "".push_back(%this.toDownload, %dlItem);
    %this.serviceToDownloadQueue();
};
function dlMgr::serviceToDownloadQueue(%this) {
    if ((%this.outstanding.size() >= %this.maxOutstanding)) {
        echoDebug(getScopeName() @ " " @ "- too many outstanding already:" @ " " @ %this.outstanding.size() @ " " @ getTrace());
        return;
    }
    while ((%this.outstanding.size() < %this.maxOutstanding)) {
        %dlItem = %this.getAndRemoveFirstActionableItemInToDownloadQueue();
        if (!(isObject(%dlItem))) {
        }
        %dlItem.beginDownloadingItem(%this);
    }
};
function dlMgr::getAndRemoveFirstActionableItemInToDownloadQueue(%this) {
    %num = %this.toDownload.count();
    %found = -(1.0);
    %n = 0;
    if ((%n < %num)) {
    }
    while ((%found == -(1.0))) {
        %dlItem = %n.getKey(%this.toDownload);
        if (!(%dlItem.url.isUrlOutstanding(%this))) {
            %found = %n;
        }
        %n = (%n + 1.0);
        if ((%n < %num)) {
        }
    }
    if ((%found == -(1.0))) {
        return "";
    }
    %dlItem = %found.getKey(%this.toDownload);
    %found.erase(%this.toDownload);
    return %dlItem;
};
function dlMgr::isUrlOutstanding(%this, %url) {
    return %url.hasKey(%this.outstanding);
};
function dlMgr::beginDownloadingItem(%this, %dlItem) {
    %failCount = %dlItem.url.get(%this.failCounts);
    if ((%failCount >= %this.maxFailures)) {
        %dlItem.delete();
        return;
    }
    %dlItem.localFilename = %dlItem.url.makeLocalFilename(%this);
    %dlItem.put(%this.outstanding, %dlItem.url);
    %curl = new URLPostObject("");;
    0;
    %curl.dlItem = %dlItem;
    %dlItem.url.setURL(%curl);
    %dlItem.localFilename.setDownloadFile(%curl);
    1.setRecvData(%curl);
    "dlMgrRequest_onCompletedDownload".setCompletedCallback(%curl);
    %curl.start();
};
function dlMgr::makeLocalFilename(%this, %url) {
    %ext = getExtension(%url);
    %localBase = stripExtension(%url);
    %localFileName = $DC::GUIFolderName @ "/ui_" @ MD5(%localBase) @ %ext;
    return %localFileName;
};
function dlMgrRequest_onCompletedDownload(%request, %result) {
    %dlItem = %request.dlItem;
    if ((%result == 0.0)) {
        %dlItem.downloadSucceeded(dlMgr);
    }
    %result.downloadFailed(dlMgr, %dlItem, %request);
};
function dlMgr::downloadFailed(%this, %dlItem, %curl, %error) {
    error(getScopeName() @ " " @ "-" @ " " @ %error @ " " @ %curl.statusCode() @ " " @ %error.resultCodeToString(%curl));
    %dlItem.url.remove(%this.outstanding);
    %failCount = %dlItem.url.get(%this.failCounts);
    %failCount = (%failCount + 1.0);
    %failCount.put(%this.failCounts, %dlItem.url);
    if ((%curl.statusCode() == 302.0)) {
        %this.maxFailures.put(%this.failCounts, %dlItem.url);
        if (!(%dlItem.errorCallback $= "")) {
            call(%dlItem.errorCallback, %dlItem);
        }
        %dlItem.delete();
    }
    if ((%failCount < %this.maxFailures)) {
        %dlItem.schedule(%this, (%this.retryDelay * 1000.0), "enqueueItem");
    }
    error(getScopeName() @ " " @ "- failed" @ " " @ %failCount @ " " @ "times; giving up on" @ " " @ %dlItem.url);
    if (!(%dlItem.errorCallback $= "")) {
        call(%dlItem.errorCallback, %dlItem);
    }
    %dlItem.delete();
    %this.serviceToDownloadQueue();
};
function dlMgr::downloadSucceeded(%this, %dlItem) {
    %dlItem.url.remove(%this.outstanding);
    %dlItem.url.remove(%this.failCounts);
    removeFile(%dlItem.localFilename);
    addFile(%dlItem.localFilename);
    %curSeconds = getTime();
    %record = %dlItem.localFilename @ "\t" @ %curSeconds @ "\t" @ %curSeconds @ "\t" @ %dlItem.policyName;
    %record.put(%this.cacheIndex, %dlItem.url);
    1.applyItem(%this, %dlItem);
    %this.serviceToDownloadQueue();
};
function dlMgr::applyItem(%this, %dlItem, %isFresh) {
    if ((%dlItem.callback $= "")) {
        error(getScopeName() @ " " @ "- no callback!" @ " " @ %dlItem.url @ " " @ getTrace());
        %dlItem.delete();
        return;
    }
    if (%isFresh) {
    }
    if ("useStale".getPolicyValue(%this, %dlItem.policyName)) {
        if (!(%isFresh)) {
            echoDebug(getScopeName() @ " " @ "- using stale data -" @ " " @ %dlItem.url);
        }
        call(%dlItem.callback, %dlItem, %isFresh);
    }
    %record = %dlItem.url.get(%this.cacheIndex);
    if ((%record $= "")) {
        error(getScopeName() @ " " @ "- no entry in cache index." @ " " @ %dlItem.url @ " " @ getTrace());
    }
    %record = setField(%record, 1, getTime());
    %record.put(%this.cacheIndex, %dlItem.url);
    if (!(%isFresh)) {
        echoDebug(getScopeName() @ " " @ "- re-downloading" @ " " @ %dlItem.url @ " " @ getTrace());
        %dlItem.enqueueItem(%this);
    }
    %dlItem.delete();
};
function dlMgrDefaultCallback(%dlItem, %isFresh) {
    error(getScopeName() @ " " @ "- callbackData =" @ " " @ %dlItem);
    %dlItem.dumpFields();
};
function dlMgr::loadCacheIndex(%this) {
    "debug".loadFrom(%this.cacheIndex, %this.cacheIndexFilename);
};
function dlMgr::saveCacheIndex(%this) {
    %this.cacheIndexFilename.saveTo(dlMgr, %this.cacheIndex, dlMgr);
};
function dlMgr::clearCache(%this) {
    %n = (%this.cacheIndex.size() - 1.0);
    while ((%n >= 0.0)) {
        %localFile = getField(%n.getValue(%this.cacheIndex), 0);
        deleteFile(%localFile);
        %n = (%n - 1.0);
    }
    %this.cacheIndex.clear();
    %this.saveCacheIndex();
};
function dlMgr::purgeCache(%this) {
    %num = %this.cacheIndex.size();
    %purgedCount = 0;
    %n = (%num - 1.0);
    while ((%n >= 0.0)) {
        %url = %n.getKey(%this.cacheIndex);
        %record = %n.getValue(%this.cacheIndex);
        %localFile = getField(%record, 0);
        %aTime = getField(%record, 1);
        %cTime = getField(%record, 2);
        %policyName = getField(%record, 3);
        %cacheDuration = "cacheDuration".getPolicyValue(%this, %policyName);
        %time = getTime();
        %aAge = mSubS32(%time, %aTime);
        %cAge = mSubS32(%time, %cTime);
        if ((%aAge > %cacheDuration)) {
            %purgedCount = (%purgedCount + 1.0);
            %url.purgeCacheEntry(%this);
        }
        %n = (%n - 1.0);
    }
    echo(getScopeName() @ " " @ "- purged" @ " " @ %purgedCount @ " " @ "out of" @ " " @ %num @ " " @ "files.");
    %this.saveCacheIndex();
};
function dlMgr::purgeCacheEntry(%this, %url) {
    if (!(%url.hasKey(%this.cacheIndex))) {
        echoDebug(getScopeName() @ " " @ "- no such record:" @ " " @ %url @ " " @ getTrace());
        return;
    }
    %record = %url.get(%this.cacheIndex);
    %localFile = getField(%record, 0);
    echoDebug(getScopeName() @ " " @ "- purging! \"" @ %localFile @ "\"");
    %url.remove(%this.cacheIndex);
    deleteFile(%localFile);
};
function dlMgr::dumpCache(%this) {
    %num = %this.cacheIndex.size();
    %n = 0;
    while ((%n < %num)) {
        %url = %n.getKey(%this.cacheIndex);
        %record = %n.getValue(%this.cacheIndex);
        %localFile = getField(%record, 0);
        %aTime = getField(%record, 1);
        %cTime = getField(%record, 2);
        %policyName = getField(%record, 3);
        %cacheDuration = "cacheDuration".getPolicyValue(%this, %policyName);
        %staleTime = "expirationDuration".getPolicyValue(%this, %policyName);
        %useStale = "useStale".getPolicyValue(%this, %policyName);
        %time = getTime();
        %aAge = mSubS32(%time, %aTime);
        %cAge = mSubS32(%time, %cTime);
        echo(getScopeName() @ " " @ "- entry" @ " " @ formatInt("%0.5d", %n));
        echo(getScopeName() @ " " @ "- url         : \"" @ %url @ "\"");
        echo(getScopeName() @ " " @ "- local file  : \"" @ %localFile @ "\"");
        echo(getScopeName() @ " " @ "- access   age: " @ secondsToDaysHoursMinutesSeconds(%aAge));
        echo(getScopeName() @ " " @ "- creation age: " @ secondsToDaysHoursMinutesSeconds(%cAge));
        echo(getScopeName() @ " " @ "- policy      : " @ %policyName);
        echo(getScopeName() @ " " @ "  - purge time: " @ secondsToDaysHoursMinutesSeconds(%cacheDuration));
        echo(getScopeName() @ " " @ "  - stale time: " @ secondsToDaysHoursMinutesSeconds(%staleTime));
        echo(getScopeName() @ " " @ "  - use stale : " @ %useStale);
        %n = (%n + 1.0);
    }
};
function GuiControl::downloadAndApplyBitmap(%this, %url, %policyName) {
    if (!("setBitmap".hasMethod(%this))) {
        error(getScopeName() @ " " @ "- no setBitmap() method!" @ " " @ getDebugString(%this) @ " " @ getTrace());
        return;
    }
    if (!(isDefined("%policyName"))) {
        %policyName = "";
    }
    %this.expectedImageUrl = %url;
    %policyName.applyUrl(dlMgr, %url, "dlMgrCallback_GuiControl", "", %this);
};
function dlMgrCallback_GuiControl(%dlItem, %isFresh) {
    %ctrl = %dlItem.callbackData;
    if (!(isObject(%ctrl))) {
        warn(getScopeName() @ " " @ "- control no longer exists!" @ " " @ %ctrl @ " " @ %dlItem.url @ " " @ getTrace());
        return;
    }
    if (!(%ctrl.expectedImageUrl $= %dlItem.url)) {
        echoDebug(getScopeName() @ " " @ "- unexpected URL retrieved. Expected \"" @ %ctrl.expectedImageUrl @ "\" but got \"" @ %dlItem.url @ "\".");
    }
    "".setBitmap(%ctrl);
    %dlItem.localFilename.setBitmap(%ctrl);
    %ctrl.expectedUrl = "";
    %ctrl.fitInParentAsBitmap();
};
