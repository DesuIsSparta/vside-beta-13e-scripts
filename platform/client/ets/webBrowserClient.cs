function dlMgr::smInit() {
    if (isObject()) {
        return dlMgr;
    }
    new ScriptObject(dlMgr);
    if (isObject()) {
        add();
    }
    reset();
};
function dlMgr::reset(%this) {
    cacheIndex = safeNewScriptObject("StringMap", "", 0) @ %this;
    cacheIndexFilename = $DC::GUIFolderName @ "/index.txt" @ %this;
    %this.loadCacheIndex();
    outstanding = safeNewScriptObject("StringMap", "", 0) @ %this;
    toDownload = safeNewScriptObject("Array", "", 0) @ %this;
    failCounts = safeNewScriptObject("StringMap", "", 0) @ %this;
    policies = safeNewScriptObject("StringMap", "", 0) @ %this;
    %this.setPolicyValue("default", "expirationDuration", minutesToSeconds(1));
    %this.setPolicyValue("default", "useStale", 1);
    %this.setPolicyValue("default", "cacheDuration", daysToSeconds(14));
    %this.setPolicyValue("avatar", "expirationDuration", minutesToSeconds(1));
    %this.setPolicyValue("youtube", "expirationDuration", minutesToSeconds((2.0 * (60.0 * 24.0))));
    maxOutstanding = 10 @ %this;
    maxFailures = 3 @ %this;
    retryDelay = 2 @ %this;
};
function dlMgr::shutDown(%this) {
    %this.purgeCache();
    %this.saveCacheIndex();
};
function dlMgr::setPolicyValue(%this, %policyName, %valuename, %value) {
    %policy = %this.getPolicy(%policyName);
    %policy.put(%valuename, %value);
};
function dlMgr::getPolicyValue(%this, %policyName, %valuename) {
    %policy = %this.getPolicy(%policyName);
    if (!(%policy.hasKey(%valuename))) {
        %policy = %this.getPolicy("default");
    }
    if (!(%policy.hasKey(%valuename))) {
        error("unknown policy value:" @ " " @ %policyName @ ":" @ %valuename);
        return "";
    }
    return %policy.get(%valuename);
};
function dlMgr::getPolicy(%this, %policyName) {
    %policy = policies.get(%policyName);
    %this;
    if (!(isObject(%policy))) {
        %policy = safeNewScriptObject("StringMap", "", 0);
        name = %policyName @ %policy;
        policies.put(%policyName, %policy);
    }
    return %policy;
};
function dlMgr::applyUrl(%this, %url, %callback, %errorCallback, %callbackData, %policyName) {
    %dlItem = %this.buildDLItem(%url, %callback, %errorCallback, %callbackData, %policyName);
    if ((%dlItem SPC localFilename $= "")) {
        %this.enqueueItem(%dlItem);
        return;
    }
    %isFresh = 1;
    %record = cacheIndex.get(url);
    %dlItem;
    if ((%this SPC %record $= "")) {
        error(%dlItem @ url @ " " @ getTrace());
    }
    %accessTime = getField(%record, 1);
    getScopeName() @ " " @ "- no entry in cache index." @ " ";
    %age = (%accessTime - getTime());
    if ((%this.getPolicyValue(%policyName, "expirationDuration") > %age)) {
        %isFresh = 0;
    }
    %this.applyItem(%dlItem, %isFresh);
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
    url = %url @ %dlItem;
    callback = %callback @ %dlItem;
    errorCallback = %errorCallback @ %dlItem;
    callbackData = %callbackData @ %dlItem;
    policyName = %policyName @ %dlItem;
    localFilename = %this.getCachedFilename(%url) @ %dlItem;
    if (!(%dlItem SPC localFilename $= "")) {
        if (!(isFile(localFilename))) {
            error(%dlItem @ url @ " " @ getTrace());
            localFilename = %dlItem @ localFilename @ " " @ "" @ %dlItem;
            getScopeName() @ " " @ "- file missing from cache:" @ " ";
        }
    }
    return %dlItem;
};
function dlMgr::getCachedFilename(%this, %url) {
    return getField(cacheIndex.get(%url), 0);
};
function dlMgr::enqueueItem(%this, %dlItem) {
    toDownload.push_back(%dlItem, "");
    %this.serviceToDownloadQueue();
};
function dlMgr::serviceToDownloadQueue(%this) {
    if ((%this >= outstanding.size())) {
        echoDebug(%this @ outstanding.size() @ " " @ getTrace());
        return getScopeName() @ " " @ "- too many outstanding already:" @ " ";
    }
    if ((%this < outstanding.size())) {
        %dlItem = %this.getAndRemoveFirstActionableItemInToDownloadQueue();
        maxOutstanding;
        if (!(isObject(%dlItem))) {
        }
        %this.beginDownloadingItem(%dlItem);
    }
};
function dlMgr::getAndRemoveFirstActionableItemInToDownloadQueue(%this) {
    %num = toDownload.count();
    %this;
    %found = -(1.0);
    %n = 0;
    if ((%num < %n)) {
    }
    if ((-(1.0) == %found)) {
        %dlItem = toDownload.getKey(%n);
        %this;
        if (!(%this.isUrlOutstanding(url))) {
            %found = %n;
            %dlItem;
        }
        %n = (1.0 + %n);
        if ((%num < %n)) {
        }
    }
    if ((-(1.0) == %found)) {
        return "";
    }
    %dlItem = toDownload.getKey(%found);
    %this;
    toDownload.erase(%found);
    return %dlItem;
};
function dlMgr::isUrlOutstanding(%this, %url) {
    return outstanding.hasKey(%url);
};
function dlMgr::beginDownloadingItem(%this, %dlItem) {
    %failCount = failCounts.get(url);
    %dlItem;
    if ((maxFailures >= %failCount)) {
        %dlItem.delete();
        return %this;
    }
    localFilename = %dlItem @ %this.makeLocalFilename(url) @ %dlItem;
    outstanding.put(url, %dlItem);
    %curl = new ""();
    URLPostObject;
    dlItem = 0 @ %dlItem @ %curl;
    %dlItem;
    %curl.setURL(url);
    %curl.setDownloadFile(localFilename);
    %curl.setRecvData(1);
    %curl.setCompletedCallback("dlMgrRequest_onCompletedDownload");
    %curl.start();
};
function dlMgr::makeLocalFilename(%this, %url) {
    %ext = getExtension(%url);
    %localBase = stripExtension(%url);
    %localFileName = $DC::GUIFolderName @ "/ui_" @ MD5(%localBase) @ %ext;
    return %localFileName;
};
function dlMgrRequest_onCompletedDownload(%request, %result) {
    %dlItem = dlItem;
    %request;
    if ((0.0 == %result)) {
        %dlItem.downloadSucceeded();
    }
    %dlItem.downloadFailed(%request, %result);
};
function dlMgr::downloadFailed(%this, %dlItem, %curl, %error) {
    error(getScopeName() @ " " @ "-" @ " " @ %error @ " " @ %curl.statusCode() @ " " @ %curl.resultCodeToString(%error));
    outstanding.remove(url);
    %failCount = failCounts.get(url);
    %dlItem;
    %failCount = (1.0 + %failCount);
    %this;
    failCounts.put(url, %failCount);
    if ((302.0 == %curl.statusCode())) {
        failCounts.put(url, maxFailures);
        if (!(%dlItem SPC errorCallback $= "")) {
            call(errorCallback, %dlItem);
        }
        %dlItem.delete();
    }
    if ((maxFailures < %failCount)) {
        %this.schedule((%this * retryDelay), "enqueueItem", %dlItem);
    }
    error(%dlItem @ url);
    if (!(%dlItem SPC errorCallback $= "")) {
        call(errorCallback, %dlItem);
    }
    %dlItem.delete();
    %this.serviceToDownloadQueue();
};
function dlMgr::downloadSucceeded(%this, %dlItem) {
    outstanding.remove(url);
    failCounts.remove(url);
    removeFile(localFilename);
    addFile(localFilename);
    %curSeconds = getTime();
    %dlItem;
    %record = %dlItem @ policyName;
    localFilename @ "\t" @ %curSeconds @ "\t" @ %curSeconds @ "\t";
    cacheIndex.put(url, %record);
    %this.applyItem(%dlItem, 1);
    %this.serviceToDownloadQueue();
};
function dlMgr::applyItem(%this, %dlItem, %isFresh) {
    if ((%dlItem SPC callback $= "")) {
        error(%dlItem @ url @ " " @ getTrace());
        %dlItem.delete();
        return getScopeName() @ " " @ "- no callback!" @ " ";
    }
    if (%isFresh) {
    }
    if (%this.getPolicyValue(policyName, "useStale")) {
        if (!(%isFresh)) {
            echoDebug(%dlItem @ url);
        }
        call(callback, %dlItem, %isFresh);
    }
    %record = cacheIndex.get(url);
    %dlItem;
    if ((%this SPC %record $= "")) {
        error(%dlItem @ url @ " " @ getTrace());
    }
    %record = setField(%record, 1, getTime());
    getScopeName() @ " " @ "- no entry in cache index." @ " ";
    cacheIndex.put(url, %record);
    if (!(%isFresh)) {
        echoDebug(%dlItem @ url @ " " @ getTrace());
        %this.enqueueItem(%dlItem);
    }
    %dlItem.delete();
};
function dlMgrDefaultCallback(%dlItem, %isFresh) {
    error(getScopeName() @ " " @ "- callbackData =" @ " " @ %dlItem);
    %dlItem.dumpFields();
};
function dlMgr::loadCacheIndex(%this) {
    cacheIndex.loadFrom(cacheIndexFilename, "debug");
};
function dlMgr::saveCacheIndex(%this) {
    cacheIndex.saveTo(cacheIndexFilename);
};
function dlMgr::clearCache(%this) {
    %n = (%this - cacheIndex.size());
    1.0;
    if ((0.0 >= %n)) {
        %localFile = getField(cacheIndex.getValue(%n), 0);
        %this;
        deleteFile(%localFile);
        %n = (1.0 - %n);
    }
    cacheIndex.clear();
    %this.saveCacheIndex();
};
function dlMgr::purgeCache(%this) {
    %num = cacheIndex.size();
    %this;
    %purgedCount = 0;
    %n = (1.0 - %num);
    if ((0.0 >= %n)) {
        %url = cacheIndex.getKey(%n);
        %this;
        %record = cacheIndex.getValue(%n);
        %this;
        %localFile = getField(%record, 0);
        %aTime = getField(%record, 1);
        %cTime = getField(%record, 2);
        %policyName = getField(%record, 3);
        %cacheDuration = %this.getPolicyValue(%policyName, "cacheDuration");
        %time = getTime();
        %aAge = mSubS32(%time, %aTime);
        %cAge = mSubS32(%time, %cTime);
        if ((%cacheDuration > %aAge)) {
            %purgedCount = (1.0 + %purgedCount);
            %this.purgeCacheEntry(%url);
        }
        %n = (1.0 - %n);
    }
    echo(getScopeName() @ " " @ "- purged" @ " " @ %purgedCount @ " " @ "out of" @ " " @ %num @ " " @ "files.");
    %this.saveCacheIndex();
};
function dlMgr::purgeCacheEntry(%this, %url) {
    if (!(cacheIndex.hasKey(%url))) {
        echoDebug(getScopeName() @ " " @ "- no such record:" @ " " @ %url @ " " @ getTrace());
        return %this;
    }
    %record = cacheIndex.get(%url);
    %this;
    %localFile = getField(%record, 0);
    echoDebug(getScopeName() @ " " @ "- purging! \"" @ %localFile @ "\"");
    cacheIndex.remove(%url);
    deleteFile(%localFile);
};
function dlMgr::dumpCache(%this) {
    %num = cacheIndex.size();
    %this;
    %n = 0;
    if ((%num < %n)) {
        %url = cacheIndex.getKey(%n);
        %this;
        %record = cacheIndex.getValue(%n);
        %this;
        %localFile = getField(%record, 0);
        %aTime = getField(%record, 1);
        %cTime = getField(%record, 2);
        %policyName = getField(%record, 3);
        %cacheDuration = %this.getPolicyValue(%policyName, "cacheDuration");
        %staleTime = %this.getPolicyValue(%policyName, "expirationDuration");
        %useStale = %this.getPolicyValue(%policyName, "useStale");
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
        %n = (1.0 + %n);
    }
};
function GuiControl::downloadAndApplyBitmap(%this, %url, %policyName) {
    if (!(%this.hasMethod("setBitmap"))) {
        error(getScopeName() @ " " @ "- no setBitmap() method!" @ " " @ getDebugString(%this) @ " " @ getTrace());
        return;
    }
    if (!(isDefined("%policyName"))) {
        %policyName = "";
    }
    expectedImageUrl = %url @ %this;
    %url.applyUrl("dlMgrCallback_GuiControl", "", %this, %policyName);
};
function dlMgrCallback_GuiControl(%dlItem, %isFresh) {
    %ctrl = callbackData;
    %dlItem;
    if (!(isObject(%ctrl))) {
        warn(%dlItem @ url @ " " @ getTrace());
        return getScopeName() @ " " @ "- control no longer exists!" @ " " @ %ctrl @ " ";
    }
    if (!(%dlItem $= url)) {
        echoDebug(%ctrl SPC expectedImageUrl @ getScopeName() @ " " @ "- unexpected URL retrieved. Expected \"" @ %ctrl @ expectedImageUrl @ "\" but got \"" @ %dlItem @ url @ "\".");
    }
    %ctrl.setBitmap("");
    %ctrl.setBitmap(localFilename);
    expectedUrl = %dlItem @ "" @ %ctrl;
    %ctrl.fitInParentAsBitmap();
};
