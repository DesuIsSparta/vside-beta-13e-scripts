function dlMgr::smInit()
{
    if (isObject(dlMgr))
    {
        return ;
    }
    new ScriptObject(dlMgr);
    if (isObject(MissionCleanup))
    {
        MissionCleanup.add(dlMgr);
    }
    dlMgr.reset();
    return ;
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
    %this.setPolicyValue("youtube", "expirationDuration", minutesToSeconds((24 * 60) * 2));
    %this.maxOutstanding = 10;
    %this.maxFailures = 3;
    %this.retryDelay = 2;
    return ;
}
function dlMgr::shutDown(%this)
{
    %this.purgeCache();
    %this.saveCacheIndex();
    return ;
}
function dlMgr::setPolicyValue(%this, %policyName, %valuename, %value)
{
    %policy = %this.getPolicy(%policyName);
    %policy.put(%valuename, %value);
    return ;
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
        error("unknown policy value:" SPC %policyName @ ":" @ %valuename);
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
    if (%dlItem.localFilename $= "")
    {
        %this.enqueueItem(%dlItem);
        return ;
    }
    %isFresh = 1;
    %record = %this.cacheIndex.get(%dlItem.url);
    if (%record $= "")
    {
        error(getScopeName() SPC "- no entry in cache index." SPC %dlItem.url SPC getTrace());
    }
    else
    {
        %accessTime = getField(%record, 1);
        %age = getTime() - %accessTime;
        if (%age > %this.getPolicyValue(%policyName, "expirationDuration"))
        {
            %isFresh = 0;
        }
    }
    %this.applyItem(%dlItem, %isFresh);
    return ;
}
function dlMgr::buildDLItem(%this, %url, %callback, %errorCallback, %callbackData, %policyName)
{
    if (!isDefined("%policyName") && (%policyName $= ""))
    {
        %policyName = "default";
    }
    if (!isDefined("%errorCallback"))
    {
        %errorCallback = "";
    }
    if (%callback $= "")
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
    if (!(%dlItem.localFilename $= ""))
    {
        if (!isFile(%dlItem.localFilename))
        {
            error(getScopeName() SPC "- file missing from cache:" SPC %dlItem.localFilename SPC %dlItem.url SPC getTrace());
            %dlItem.localFilename = "";
        }
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
    return ;
}
function dlMgr::serviceToDownloadQueue(%this)
{
    if (%this.outstanding.size() >= %this.maxOutstanding)
    {
        echoDebug(getScopeName() SPC "- too many outstanding already:" SPC %this.outstanding.size() SPC getTrace());
        return ;
    }
    while (%this.outstanding.size() < %this.maxOutstanding)
    {
        %dlItem = %this.getAndRemoveFirstActionableItemInToDownloadQueue();
        if (!isObject(%dlItem))
        {
            continue;
        }
        %this.beginDownloadingItem(%dlItem);
    }
}

function dlMgr::getAndRemoveFirstActionableItemInToDownloadQueue(%this)
{
    %num = %this.toDownload.count();
    %found = -1;
    %n = 0;
    while (%found == -1)
    {
        %dlItem = %this.toDownload.getKey(%n);
        if (!%this.isUrlOutstanding(%dlItem.url))
        {
            %found = %n;
        }
        %n = %n + 1;
    }
    if (%found == -1)
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
    if (%failCount >= %this.maxFailures)
    {
        %dlItem.delete();
        return ;
    }
    %dlItem.localFilename = %this.makeLocalFilename(%dlItem.url);
    %this.outstanding.put(%dlItem.url, %dlItem);
    %curl = new URLPostObject();
    %curl.dlItem = %dlItem;
    %curl.setURL(%dlItem.url);
    %curl.setDownloadFile(%dlItem.localFilename);
    %curl.setRecvData(1);
    %curl.setCompletedCallback("dlMgrRequest_onCompletedDownload");
    %curl.start();
    return ;
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
    if (%result == 0)
    {
        dlMgr.downloadSucceeded(%dlItem);
    }
    else
    {
        dlMgr.downloadFailed(%dlItem, %request, %result);
    }
    return ;
}
function dlMgr::downloadFailed(%this, %dlItem, %curl, %error)
{
    error(getScopeName() SPC "-" SPC %error SPC %curl.statusCode() SPC %curl.resultCodeToString(%error));
    %this.outstanding.remove(%dlItem.url);
    %failCount = %this.failCounts.get(%dlItem.url);
    %failCount = %failCount + 1;
    %this.failCounts.put(%dlItem.url, %failCount);
    if (%curl.statusCode() == 302)
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
        if (%failCount < %this.maxFailures)
        {
            %this.schedule(%this.retryDelay * 1000, "enqueueItem", %dlItem);
        }
        else
        {
            error(getScopeName() SPC "- failed" SPC %failCount SPC "times; giving up on" SPC %dlItem.url);
            if (!(%dlItem.errorCallback $= ""))
            {
                call(%dlItem.errorCallback, %dlItem);
            }
            %dlItem.delete();
        }
    }
    %this.serviceToDownloadQueue();
    return ;
}
function dlMgr::downloadSucceeded(%this, %dlItem)
{
    %this.outstanding.remove(%dlItem.url);
    %this.failCounts.remove(%dlItem.url);
    removeFile(%dlItem.localFilename);
    addFile(%dlItem.localFilename);
    %curSeconds = getTime();
    %record = %dlItem.localFilename TAB %curSeconds TAB %curSeconds TAB %dlItem.policyName;
    %this.cacheIndex.put(%dlItem.url, %record);
    %this.applyItem(%dlItem, 1);
    %this.serviceToDownloadQueue();
    return ;
}
function dlMgr::applyItem(%this, %dlItem, %isFresh)
{
    if (%dlItem.callback $= "")
    {
        error(getScopeName() SPC "- no callback!" SPC %dlItem.url SPC getTrace());
        %dlItem.delete();
        return ;
    }
    if (%isFresh && %this.getPolicyValue(%dlItem.policyName, "useStale"))
    {
        if (!%isFresh)
        {
            echoDebug(getScopeName() SPC "- using stale data -" SPC %dlItem.url);
        }
        call(%dlItem.callback, %dlItem, %isFresh);
    }
    %record = %this.cacheIndex.get(%dlItem.url);
    if (%record $= "")
    {
        error(getScopeName() SPC "- no entry in cache index." SPC %dlItem.url SPC getTrace());
    }
    else
    {
        %record = setField(%record, 1, getTime());
        %this.cacheIndex.put(%dlItem.url, %record);
    }
    if (!%isFresh)
    {
        echoDebug(getScopeName() SPC "- re-downloading" SPC %dlItem.url SPC getTrace());
        %this.enqueueItem(%dlItem);
    }
    else
    {
        %dlItem.delete();
    }
    return ;
}
function dlMgrDefaultCallback(%dlItem, %isFresh)
{
    error(getScopeName() SPC "- callbackData =" SPC %dlItem);
    %dlItem.dumpFields();
    return ;
}
function dlMgr::loadCacheIndex(%this)
{
    %this.cacheIndex.loadFrom(%this.cacheIndexFilename, "debug");
    return ;
}
function dlMgr::saveCacheIndex(%this)
{
    dlMgr.cacheIndex.saveTo(dlMgr.cacheIndexFilename);
    return ;
}
function dlMgr::clearCache(%this)
{
    %n = %this.cacheIndex.size() - 1;
    while (%n >= 0)
    {
        %localFile = getField(%this.cacheIndex.getValue(%n), 0);
        deleteFile(%localFile);
        %n = %n - 1;
    }
    %this.cacheIndex.clear();
    %this.saveCacheIndex();
    return ;
}
function dlMgr::purgeCache(%this)
{
    %num = %this.cacheIndex.size();
    %purgedCount = 0;
    %n = %num - 1;
    while (%n >= 0)
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
        if (%aAge > %cacheDuration)
        {
            %purgedCount = %purgedCount + 1;
            %this.purgeCacheEntry(%url);
        }
        %n = %n - 1;
    }
    echo(getScopeName() SPC "- purged" SPC %purgedCount SPC "out of" SPC %num SPC "files.");
    %this.saveCacheIndex();
    return ;
}
function dlMgr::purgeCacheEntry(%this, %url)
{
    if (!%this.cacheIndex.hasKey(%url))
    {
        echoDebug(getScopeName() SPC "- no such record:" SPC %url SPC getTrace());
        return ;
    }
    %record = %this.cacheIndex.get(%url);
    %localFile = getField(%record, 0);
    echoDebug(getScopeName() SPC "- purging! \"" @ %localFile @ "\"");
    %this.cacheIndex.remove(%url);
    deleteFile(%localFile);
    return ;
}
function dlMgr::dumpCache(%this)
{
    %num = %this.cacheIndex.size();
    %n = 0;
    while (%n < %num)
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
        echo(getScopeName() SPC "- entry" SPC formatInt("%0.5d", %n));
        echo(getScopeName() SPC "- url         : \"" @ %url @ "\"");
        echo(getScopeName() SPC "- local file  : \"" @ %localFile @ "\"");
        echo(getScopeName() SPC "- access   age: " @ secondsToDaysHoursMinutesSeconds(%aAge));
        echo(getScopeName() SPC "- creation age: " @ secondsToDaysHoursMinutesSeconds(%cAge));
        echo(getScopeName() SPC "- policy      : " @ %policyName);
        echo(getScopeName() SPC "  - purge time: " @ secondsToDaysHoursMinutesSeconds(%cacheDuration));
        echo(getScopeName() SPC "  - stale time: " @ secondsToDaysHoursMinutesSeconds(%staleTime));
        echo(getScopeName() SPC "  - use stale : " @ %useStale);
        %n = %n + 1;
    }
}

function GuiControl::downloadAndApplyBitmap(%this, %url, %policyName)
{
    if (!%this.hasMethod("setBitmap"))
    {
        error(getScopeName() SPC "- no setBitmap() method!" SPC getDebugString(%this) SPC getTrace());
        return ;
    }
    if (!isDefined("%policyName"))
    {
        %policyName = "";
    }
    %this.expectedImageUrl = %url;
    dlMgr.applyUrl(%url, "dlMgrCallback_GuiControl", "", %this, %policyName);
    return ;
}
function dlMgrCallback_GuiControl(%dlItem, %isFresh)
{
    %ctrl = %dlItem.callbackData;
    if (!isObject(%ctrl))
    {
        warn(getScopeName() SPC "- control no longer exists!" SPC %ctrl SPC %dlItem.url SPC getTrace());
        return ;
    }
    if (!(%ctrl.expectedImageUrl $= %dlItem.url))
    {
        echoDebug(getScopeName() SPC "- unexpected URL retrieved. Expected \"" @ %ctrl.expectedImageUrl @ "\" but got \"" @ %dlItem.url @ "\".");
    }
    else
    {
        %ctrl.setBitmap("");
        %ctrl.setBitmap(%dlItem.localFilename);
        %ctrl.expectedUrl = "";
        %ctrl.fitInParentAsBitmap();
    }
    return ;
}
