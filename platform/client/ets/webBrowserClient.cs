function dlMgr::smInit()
{
    if (isObject(dlMgr))
    {
        return;
    }
    new ScriptObject(dlMgr);
    if (isObject(MissionCleanup))
    {
        MissionCleanup.add(dlMgr);
    }
    dlMgr.reset();
}
function dlMgr::reset(%this)
{
    %this.cacheIndex = safeNewScriptObject("StringMap", "", 0);
    %this.cacheIndexFilename = $DC::GUIFolderName @ "/index.txt";
    %this.loadCacheIndex();
    %this.outstanding = safeNewScriptObject("StringMap", "", 0);
    %this.toDownload = safeNewScriptObject("Array", "", 0);
    %this.failCounts = safeNewScriptObject("StringMap", "", 0);
    %this.policies = safeNewScriptObject("StringMap", "", 0);
    %this.setPolicyValue("default", "expirationDuration", minutesToSeconds(1));
    %this.setPolicyValue("default", "useStale", 1);
    %this.setPolicyValue("default", "cacheDuration", daysToSeconds(14));
    %this.setPolicyValue("avatar", "expirationDuration", minutesToSeconds(1));
    %this.setPolicyValue("youtube", "expirationDuration", minutesToSeconds(((24.0 * 60.0) * 2.0)));
    %this.maxOutstanding = 10;
    %this.maxFailures = 3;
    %this.retryDelay = 2;
}
function dlMgr::shutDown(%this)
{
    %this.purgeCache();
    %this.saveCacheIndex();
}
function dlMgr::setPolicyValue(%this, %policyName, %valuename, %value)
{
    %policy = %this.getPolicy(%policyName);
    %policy.put(%valuename, %value);
}
function dlMgr::getPolicyValue(%this, %policyName, %valuename)
{
    %policy = %this.getPolicy(%policyName);
    if (!%policy.hasKey(%valuename))
    {
        %policy = %this.getPolicy("default");
    }
    if (!%policy.hasKey(%valuename))
    {
        error("unknown policy value:" @ " " @ %policyName @ ":" @ %valuename);
        return "";
    }
    return %policy.get(%valuename);
}
function dlMgr::getPolicy(%this, %policyName)
{
    %policy = %this.policies.get(%policyName);
    if (!isObject(%policy))
    {
        %policy = safeNewScriptObject("StringMap", "", 0);
        %policy.name = %policyName;
        %this.policies.put(%policyName, %policy);
    }
    return %policy;
}
function dlMgr::applyUrl(%this, %url, %callback, %errorCallback, %callbackData, %policyName)
{
    %dlItem = %this.buildDLItem(%url, %callback, %errorCallback, %callbackData, %policyName);
    if ((%dlItem.localFilename $= ""))
    {
        %this.enqueueItem(%dlItem);
        return;
    }
    %isFresh = 1;
    %record = %this.cacheIndex.get(%dlItem.url);
    if ((%record $= ""))
    {
        error(getScopeName() @ " " @ "- no entry in cache index." @ " " @ %dlItem.url @ " " @ getTrace());
    }
    else
    {
        %accessTime = getField(%record, 1);
        %age = (getTime() - %accessTime);
        if ((%age > %this.getPolicyValue(%policyName, "expirationDuration")))
        {
            %isFresh = 0;
        }
    }
    %this.applyItem(%dlItem, %isFresh);
}
function dlMgr::buildDLItem(%this, %url, %callback, %errorCallback, %callbackData, %policyName)
{
    if (!isDefined("%policyName") || (%policyName $= ""))
    {
        %policyName = "default";
    }
    if (!isDefined("%errorCallback"))
    {
        %errorCallback = "";
    }
    if ((%callback $= ""))
    {
        %callback = "dlMgrDefaultCallback";
    }
    %dlItem = safeNewScriptObject("ScriptObject", "", 0);
    %dlItem.url = %url;
    %dlItem.callback = %callback;
    %dlItem.errorCallback = %errorCallback;
    %dlItem.callbackData = %callbackData;
    %dlItem.policyName = %policyName;
    %dlItem.localFilename = %this.getCachedFilename(%url);
    if (!(%dlItem.localFilename $= "") && !isFile(%dlItem.localFilename))
    {
        error(getScopeName() @ " " @ "- file missing from cache:" @ " " @ %dlItem.localFilename @ " " @ %dlItem.url @ " " @ getTrace());
        %dlItem.localFilename = "";
    }
    return %dlItem;
}
function dlMgr::getCachedFilename(%this, %url)
{
    return getField(%this.cacheIndex.get(%url), 0);
}
function dlMgr::enqueueItem(%this, %dlItem)
{
    %this.toDownload.push_back(%dlItem, "");
    %this.serviceToDownloadQueue();
}
function dlMgr::serviceToDownloadQueue(%this)
{
    if ((%this.outstanding.size() >= %this.maxOutstanding))
    {
        echoDebug(getScopeName() @ " " @ "- too many outstanding already:" @ " " @ %this.outstanding.size() @ " " @ getTrace());
        return;
    }
    if ((%this.outstanding.size() < %this.maxOutstanding))
    {
        %dlItem = %this.getAndRemoveFirstActionableItemInToDownloadQueue();
        if (!isObject(%dlItem))
        {
        }
        else
        {
            %this.beginDownloadingItem(%dlItem);
        }
    }
}
function dlMgr::getAndRemoveFirstActionableItemInToDownloadQueue(%this)
{
    %num = %this.toDownload.count();
    %found = -(1.0);
    %n = 0;
    if ((%n < %num))
    {
    }
    while ((%found == -(1.0)))
    {
        %dlItem = %this.toDownload.getKey(%n);
        if (!%this.isUrlOutstanding(%dlItem.url))
        {
            %found = %n;
        }
        %n = (%n + 1.0);
        if ((%n < %num))
        {
        }
    }
    if ((%found == -(1.0)))
    {
        return "";
    }
    %dlItem = %this.toDownload.getKey(%found);
    %this.toDownload.erase(%found);
    return %dlItem;
}
function dlMgr::isUrlOutstanding(%this, %url)
{
    return %this.outstanding.hasKey(%url);
}
function dlMgr::beginDownloadingItem(%this, %dlItem)
{
    %failCount = %this.failCounts.get(%dlItem.url);
    if ((%failCount >= %this.maxFailures))
    {
        %dlItem.delete();
        return;
    }
    %dlItem.localFilename = %this.makeLocalFilename(%dlItem.url);
    %this.outstanding.put(%dlItem.url, %dlItem);
    %curl = new URLPostObject("");
    %curl.dlItem = %dlItem;
    %curl.setURL(%dlItem.url);
    %curl.setDownloadFile(%dlItem.localFilename);
    %curl.setRecvData(1);
    %curl.setCompletedCallback("dlMgrRequest_onCompletedDownload");
    %curl.start();
}
function dlMgr::makeLocalFilename(%this, %url)
{
    %ext = getExtension(%url);
    %localBase = stripExtension(%url);
    %localFileName = $DC::GUIFolderName @ "/ui_" @ MD5(%localBase) @ %ext;
    return %localFileName;
}
function dlMgrRequest_onCompletedDownload(%request, %result)
{
    %dlItem = %request.dlItem;
    if ((%result == 0.0))
    {
        dlMgr.downloadSucceeded(%dlItem);
    }
    else
    {
        dlMgr.downloadFailed(%dlItem, %request, %result);
    }
}
function dlMgr::downloadFailed(%this, %dlItem, %curl, %error)
{
    error(getScopeName() @ " " @ "-" @ " " @ %error @ " " @ %curl.statusCode() @ " " @ %curl.resultCodeToString(%error));
    %this.outstanding.remove(%dlItem.url);
    %failCount = %this.failCounts.get(%dlItem.url);
    %failCount = (%failCount + 1.0);
    %this.failCounts.put(%dlItem.url, %failCount);
    if ((%curl.statusCode() == 302.0))
    {
        %this.failCounts.put(%dlItem.url, %this.maxFailures);
        if (!(%dlItem.errorCallback $= ""))
        {
            call(%dlItem.errorCallback, %dlItem);
        }
        %dlItem.delete();
    }
    else
    {
        if ((%failCount < %this.maxFailures))
        {
            %this.schedule((%this.retryDelay * 1000.0), "enqueueItem", %dlItem);
        }
        else
        {
            error(getScopeName() @ " " @ "- failed" @ " " @ %failCount @ " " @ "times; giving up on" @ " " @ %dlItem.url);
            if (!(%dlItem.errorCallback $= ""))
            {
                call(%dlItem.errorCallback, %dlItem);
            }
            %dlItem.delete();
        }
    }
    %this.serviceToDownloadQueue();
}
function dlMgr::downloadSucceeded(%this, %dlItem)
{
    %this.outstanding.remove(%dlItem.url);
    %this.failCounts.remove(%dlItem.url);
    removeFile(%dlItem.localFilename);
    addFile(%dlItem.localFilename);
    %curSeconds = getTime();
    %record = %dlItem.localFilename @ "\t" @ %curSeconds @ "\t" @ %curSeconds @ "\t" @ %dlItem.policyName;
    %this.cacheIndex.put(%dlItem.url, %record);
    %this.applyItem(%dlItem, 1);
    %this.serviceToDownloadQueue();
}
function dlMgr::applyItem(%this, %dlItem, %isFresh)
{
    if ((%dlItem.callback $= ""))
    {
        error(getScopeName() @ " " @ "- no callback!" @ " " @ %dlItem.url @ " " @ getTrace());
        %dlItem.delete();
        return;
    }
    if (%isFresh || %this.getPolicyValue(%dlItem.policyName, "useStale"))
    {
        if (!%isFresh)
        {
            echoDebug(getScopeName() @ " " @ "- using stale data -" @ " " @ %dlItem.url);
        }
        call(%dlItem.callback, %dlItem, %isFresh);
    }
    %record = %this.cacheIndex.get(%dlItem.url);
    if ((%record $= ""))
    {
        error(getScopeName() @ " " @ "- no entry in cache index." @ " " @ %dlItem.url @ " " @ getTrace());
    }
    else
    {
        %record = setField(%record, 1, getTime());
        %this.cacheIndex.put(%dlItem.url, %record);
    }
    if (!%isFresh)
    {
        echoDebug(getScopeName() @ " " @ "- re-downloading" @ " " @ %dlItem.url @ " " @ getTrace());
        %this.enqueueItem(%dlItem);
    }
    else
    {
        %dlItem.delete();
    }
}
function dlMgrDefaultCallback(%dlItem, %isFresh)
{
    error(getScopeName() @ " " @ "- callbackData =" @ " " @ %dlItem);
    %dlItem.dumpFields();
}
function dlMgr::loadCacheIndex(%this)
{
    %this.cacheIndex.loadFrom(%this.cacheIndexFilename, "debug");
}
function dlMgr::saveCacheIndex(%this)
{
    dlMgr.cacheIndex.saveTo(dlMgr.cacheIndexFilename);
}
function dlMgr::clearCache(%this)
{
    %n = (%this.cacheIndex.size() - 1.0);
    while ((%n >= 0.0))
    {
        %localFile = getField(%this.cacheIndex.getValue(%n), 0);
        deleteFile(%localFile);
        %n = (%n - 1.0);
    }
    %this.cacheIndex.clear();
    %this.saveCacheIndex();
}
function dlMgr::purgeCache(%this)
{
    %num = %this.cacheIndex.size();
    %purgedCount = 0;
    %n = (%num - 1.0);
    while ((%n >= 0.0))
    {
        %url = %this.cacheIndex.getKey(%n);
        %record = %this.cacheIndex.getValue(%n);
        %localFile = getField(%record, 0);
        %aTime = getField(%record, 1);
        %cTime = getField(%record, 2);
        %policyName = getField(%record, 3);
        %cacheDuration = %this.getPolicyValue(%policyName, "cacheDuration");
        %time = getTime();
        %aAge = mSubS32(%time, %aTime);
        %cAge = mSubS32(%time, %cTime);
        if ((%aAge > %cacheDuration))
        {
            %purgedCount = (%purgedCount + 1.0);
            %this.purgeCacheEntry(%url);
        }
        %n = (%n - 1.0);
    }
    echo(getScopeName() @ " " @ "- purged" @ " " @ %purgedCount @ " " @ "out of" @ " " @ %num @ " " @ "files.");
    %this.saveCacheIndex();
}
function dlMgr::purgeCacheEntry(%this, %url)
{
    if (!%this.cacheIndex.hasKey(%url))
    {
        echoDebug(getScopeName() @ " " @ "- no such record:" @ " " @ %url @ " " @ getTrace());
        return;
    }
    %record = %this.cacheIndex.get(%url);
    %localFile = getField(%record, 0);
    echoDebug(getScopeName() @ " " @ "- purging! \"" @ %localFile @ "\"");
    %this.cacheIndex.remove(%url);
    deleteFile(%localFile);
}
function dlMgr::dumpCache(%this)
{
    %num = %this.cacheIndex.size();
    %n = 0;
    while ((%n < %num))
    {
        %url = %this.cacheIndex.getKey(%n);
        %record = %this.cacheIndex.getValue(%n);
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
        %n = (%n + 1.0);
    }
}
function GuiControl::downloadAndApplyBitmap(%this, %url, %policyName)
{
    if (!%this.hasMethod("setBitmap"))
    {
        error(getScopeName() @ " " @ "- no setBitmap() method!" @ " " @ getDebugString(%this) @ " " @ getTrace());
        return;
    }
    if (!isDefined("%policyName"))
    {
        %policyName = "";
    }
    %this.expectedImageUrl = %url;
    dlMgr.applyUrl(%url, "dlMgrCallback_GuiControl", "", %this, %policyName);
}
function dlMgrCallback_GuiControl(%dlItem, %isFresh)
{
    %ctrl = %dlItem.callbackData;
    if (!isObject(%ctrl))
    {
        warn(getScopeName() @ " " @ "- control no longer exists!" @ " " @ %ctrl @ " " @ %dlItem.url @ " " @ getTrace());
        return;
    }
    if (!(%ctrl.expectedImageUrl $= %dlItem.url))
    {
        echoDebug(getScopeName() @ " " @ "- unexpected URL retrieved. Expected \"" @ %ctrl.expectedImageUrl @ "\" but got \"" @ %dlItem.url @ "\".");
    }
    else
    {
        %ctrl.setBitmap("");
        %ctrl.setBitmap(%dlItem.localFilename);
        %ctrl.expectedUrl = "";
        %ctrl.fitInParentAsBitmap();
    }
}
