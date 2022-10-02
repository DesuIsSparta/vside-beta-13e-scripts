$gLastIdleTimesReportTime = 0;
$SystemMetric::userCountCumulative = 0;
$SystemMetric::totalObjectCount = 0;
if (isObject($SystemMetric::ObjectCounts))
{
    $SystemMetric::ObjectCounts.delete();
}
$SystemMetric::ObjectCounts = new StringMap();
if (isObject(MissionCleanup))
{
    MissionCleanup.add($SystemMetric::ObjectCounts);
}
if (isDefined("$GMetricsLogFile") && isObject($GMetricsLogFile))
{
    $GMetricsLogFile.delete();
}
if ($StandAlone && $Server::Dedicated)
{
    $GMetricsLogFile = new FileLogger(GMetricsLogger, "gameMetrics.log");
    if (isObject(MissionCleanup))
    {
        MissionCleanup.add($GMetricsLogFile);
    }
}
function System::compileClassInstanceCounts3(%obj, %counts, %depth)
{
    if (!isObject(%obj))
    {
        return ;
    }
    %classname = %obj.getClassName();
    %key = %classname;
    %curCount = %counts.get(%key);
    %curCount = %curCount + 1;
    $SystemMetric::totalObjectCount = $SystemMetric::totalObjectCount + 1;
    %counts.put(%key, %curCount);
    if (!%obj.isClassSimGroup())
    {
        return ;
    }
    if (%depth >= $Pref::System::dumpMetricsMaxRecurseDepth)
    {
        error("Hit Maximum recurse depth" SPC getDebugString(%obj));
        return ;
    }
    %depth = %depth + 1;
    %num = %obj.getCount();
    %n = 0;
    while (%n < %num)
    {
        System::compileClassInstanceCounts3(%obj.getObject(%n), %counts, %depth);
        %n = %n + 1;
    }
}

function System::getClassInstanceCounts(%obj)
{
    %ret = "";
    System::compileClassInstanceCounts2(%obj);
    %n = $SystemMetric::ObjectCounts.size() - 1;
    while (%n >= 0)
    {
        %ret = $SystemMetric::ObjectCounts.getKey(%n) SPC $SystemMetric::ObjectCounts.getValue(%n) NL %ret;
        %n = %n - 1;
    }
    %ret = %ret @ "Total objects =" SPC $SystemMetric::totalObjectCount NL "";
    return %ret;
}
function System::dumpClassInstanceCounts(%obj)
{
    warn("System::dumpClassInstanceCounts() begin");
    warn(System::getClassInstanceCounts(%obj));
    warn("System::dumpClassInstanceCounts() finish");
    return ;
}
function System::compileClassInstanceCounts()
{
    System::compileClassInstanceCounts2(RootGroup);
    return ;
}
function System::compileClassInstanceCounts2(%obj)
{
    $SystemMetric::totalObjectCount = 0;
    $SystemMetric::ObjectCounts.clear();
    System::compileClassInstanceCounts3(%obj, $SystemMetric::ObjectCounts, 0);
    return ;
}
function System::getClassInstanceCount(%classname)
{
    if (!$SystemMetric::ObjectCounts.findKey(%classname) < 0)
    {
        return 0;
    }
    return $SystemMetric::ObjectCounts.get(%classname);
}
function SystemMetric::dumpPairsInstances(%unused, %key, %value)
{
    warn(%value SPC %key);
    return ;
}
function SystemMetric::dumpKeyValuePairs(%unused, %key, %value)
{
    warn(%key SPC %value);
    return ;
}
function System::dumpObjectsRecurse(%obj, %depth)
{
    if (!isObject(%obj))
    {
        warn("not an object -" SPC %obj);
        return ;
    }
    %indent = "";
    %n = %depth;
    while (%n > 0)
    {
        %indent = %indent @ " ";
        %n = %n - 1;
    }
    warn(%indent @ getDebugString(%obj));
    if (!%obj.isClassSimGroup())
    {
        return ;
    }
    if (%depth >= $Pref::System::dumpMetricsMaxRecurseDepth)
    {
        error("Hit Maximum recurse depth" SPC getDebugString(%obj));
        return ;
    }
    %depth = %depth + 1;
    %num = %obj.getCount();
    %n = 0;
    while (%n < %num)
    {
        System::dumpObjectsRecurse(%obj.getObject(%n), %depth);
        %n = %n + 1;
    }
}

function System::dumpObjects(%obj)
{
    warn("System::dumpObjects() begin");
    System::dumpObjectsRecurse(%obj, 0);
    warn("System::dumpObjects() finish");
    return ;
}
if (isObject($System::LoginLog))
{
    $System::LoginLog.delete();
}
$SystemMetric::loginLog = new StringMap();
if (isObject(MissionCleanup))
{
    MissionCleanup.add($SystemMetric::loginLog);
}
$SystemMetric::connectCount = 0;
$SystemMetric::disconnectCount = 0;
$SystemMetric::enteredGameCount = 0;
function System::onUserConnect(%client)
{
    $SystemMetric::connectCount = $SystemMetric::connectCount + 1;
    if (!isObject(%client))
    {
        warn("got onUserConnect on non-object client:" SPC %client);
    }
    else
    {
        %name = %client.nameBase;
        %curCount = $SystemMetric::loginLog.get(%name);
        %curCount = %curCount + 1;
        $SystemMetric::loginLog.put(%name, %curCount);
    }
    return ;
}
function System::onUserDisconnect(%client)
{
    $SystemMetric::disconnectCount = $SystemMetric::disconnectCount + 1;
    if (!isObject(%client))
    {
        warn("got onUserDisconnect on non-object client:" SPC %client);
    }
    else
    {
        if (!isObject(%client.Player))
        {
            warn("got onUserDisconnect on non-object player:" SPC %client.Player);
        }
    }
    return ;
}
function System::onUserEnteredGame(%client)
{
    $SystemMetric::enteredGameCount = $SystemMetric::enteredGameCount + 1;
    return ;
}
function System::calculateLoginMetrics()
{
    $SystemMetric::userCountNonIdle = 0;
    $SystemMetric::userCountIdle = 0;
    $SystemMetric::userCountOrphan = 0;
    warn("current users begin");
    %n = ClientGroup.getCount() - 1;
    while (%n >= 0)
    {
        %player = ClientGroup.getObject(%n).Player;
        if (!isObject(%player))
        {
            $SystemMetric::userCountOrphan = $SystemMetric::userCountOrphan + 1;
        }
        else
        {
            warn(getDebugString(%player));
            if (%player.getAFK())
            {
                $SystemMetric::userCountIdle = $SystemMetric::userCountIdle + 1;
            }
            else
            {
                $SystemMetric::userCountNonIdle = $SystemMetric::userCountNonIdle + 1;
            }
        }
        %n = %n - 1;
    }
    warn("current users finish");
    $SystemMetric::clientCount = $SystemMetric::userCountIdle + $SystemMetric::userCountNonIdle;
    $SystemMetric::uniqueLoginCount = $SystemMetric::loginLog.size();
    $SystemMetric::connectsMinusDisconnects = $SystemMetric::connectCount - $SystemMetric::disconnectCount;
    $SystemMetric::connectsMinusGameEntries = $SystemMetric::connectCount - $SystemMetric::enteredGameCount;
    $SystemMetric::pendingEventCount = countPendingEvents();
    return ;
}
function System::dumpLoginMetrics()
{
    warn("System::dumpLoginMetrics() begin");
    if ($Pref::System::dumpMetricsVerboseLogins)
    {
        warn("cumulative logins details begin");
        $SystemMetric::loginLog.forEach("dumpPairsInstances");
        warn("cumulative logins details finish");
    }
    warn("events pending                :" SPC $SystemMetric::pendingEventCount);
    warn("cumulative connects           :" SPC $SystemMetric::connectCount);
    warn("cumulative disconnects        :" SPC $SystemMetric::disconnectCount);
    warn("connects-disconnects          :" SPC $SystemMetric::connectsMinusDisconnects);
    warn("cumulative game entries       :" SPC $SystemMetric::enteredGameCount);
    warn("connects-game entries         :" SPC $SystemMetric::connectsMinusGameEntries);
    warn("unique logins                 :" SPC $SystemMetric::uniqueLoginCount);
    warn("current user count            :" SPC $SystemMetric::clientCount);
    warn("current users idle            :" SPC $SystemMetric::userCountIdle);
    warn("current users nonidle         :" SPC $SystemMetric::userCountNonIdle);
    warn("current user orphans          :" SPC $SystemMetric::userCountOrphan);
    warn("System::dumpLoginMetrics() finish");
    return ;
}
function System::dumpMetrics()
{
    warn("System::dumpMetrics() begin");
    if ($Pref::System::dumpMetricsVerboseObjects)
    {
        if ($AmClient)
        {
            warn("complete object dump crashes on client - skipped.");
        }
        else
        {
            System::dumpObjects(RootGroup);
        }
    }
    System::dumpClassInstanceCounts(RootGroup);
    System::calculateLoginMetrics();
    System::dumpLoginMetrics();
    dumpIdleTimes();
    warn("System::dumpMetrics() finish");
    return ;
}
function dumpIdleTimes()
{
    %secondsSinceLastIdleTimesDump = (getSimTime() / 1000) - $gLastIdleTimesReportTime;
    if (%secondsSinceLastIdleTimesDump < 3600)
    {
        return ;
    }
    $gLastIdleTimesReportTime = getSimTime() / 1000;
    safeEnsureScriptObject("StringMap", "gIdleTimesReport");
    gIdleTimesReport.dumpValues();
    gIdleTimesReport.clear();
    return ;
}
function SystemDumpMetricsTimer()
{
    cancel($System::dumpMetricsTimerID);
    if ($Game::Compile)
    {
        warn("deactivating SystemDumpMetricsTimer because $Game::Compile is " SPC $Game::Compile);
        return ;
    }
    if ($Pref::System::dumpMetricsTimerPeriodMS <= 0)
    {
        error("deactivating SystemDumpMetricsTimer because timer period is " SPC $Pref::System::dumpMetricsTimerPeriodMS);
        return ;
    }
    if ($AmClient && !$Pref::System::dumpMetricsOnStandAloneClient)
    {
        echo("deactivating SystemDumpMetricsTimer because am client");
        return ;
    }
    System::dumpMetrics();
    $System::dumpMetricsTimerID = schedule($Pref::System::dumpMetricsTimerPeriodMS, 0, "SystemDumpMetricsTimer");
    return ;
}
cancel($System::dumpMetricsTimerID);
if ($Pref::System::dumpMetricsTimerPeriodMS > 0)
{
    $System::dumpMetricsTimerID = schedule($Pref::System::dumpMetricsTimerPeriodMS, 0, "SystemDumpMetricsTimer");
}
function GMetrics::GamePlayStartEvent(%group, %specificGame, %player)
{
    if (%specificGame $= "")
    {
        error(getScopeName() SPC "specificGame name must be set, it is an empty string. group=" SPC %group SPC getTrace());
        return ;
    }
    if (!isObject(%player.client))
    {
        return ;
    }
    if (%player.GMetricsDelayStopTimer[%specificGame])
    {
        cancel(%player.GMetricsDelayStopTimer[%specificGame]);
        %player.GMetricsDelayStopTimer[%specificGame] = 0;
        return ;
    }
    if (!(%player.GMetricsStart[%specificGame] $= ""))
    {
    }
    else
    {
        %player.GMetricsStart[%specificGame] = getSimTime();
    }
    return ;
}
function DelayedRealPlayStopEvent(%group, %specificGame, %player)
{
    if (!isObject(%player))
    {
        echo(getScopeName() SPC %group SPC %specificGame SPC "player is not an object, not logging this:" SPC %player);
        return ;
    }
    cancel(%player.GMetricsDelayStopTimer[%specificGame]);
    %player.GMetricsDelayStopTimer[%specificGame] = 0;
    %startTime = %player.GMetricsStart[%specificGame];
    %seconds = 0;
    if (!(%startTime $= ""))
    {
        %seconds = 0.001 * (getSimTime() - %startTime);
        if (%seconds < 0)
        {
            error(getScopeName() SPC %specificGame SPC "got stop but had no startime just using:" SPC %seconds SPC "for duration.");
            %seconds = 0;
        }
    }
    else
    {
        error(getScopeName() SPC %specificGame SPC "got stop but had no startime just using:" SPC %seconds SPC "for duration.");
    }
    %player.GMetricsStart[%specificGame] = "";
    %duration = formatFloat("%0.0f", mCeil(%seconds));
    %eventTXT = "[event=playstop]";
    %detailTXT = "[group=" @ %group @ "][game=" @ %specificGame @ "][user=" @ %player.getShapeName() @ "][duration=" @ %duration @ "]";
    GMetrics::LogToFile(%eventTXT, %detailTXT);
    return ;
}
function GMetrics::GamePlayStopEvent(%group, %specificGame, %player, %delayTillRealStop)
{
    if (%specificGame $= "")
    {
        error(getScopeName() SPC "specificGame name must be set, it is an empty string. group=" SPC %group SPC getTrace());
        return ;
    }
    if (!isObject(%player.client))
    {
        return ;
    }
    cancel(%player.GMetricsDelayStopTimer[%specificGame]);
    %player.GMetricsDelayStopTimer[%specificGame] = 0;
    if (%delayTillRealStop <= 0)
    {
        return DelayedRealPlayStopEvent(%group, %specificGame, %player);
    }
    %player.GMetricsDelayStopTimer[%specificGame] = schedule(%delayTillRealStop, 0, "DelayedRealPlayStopEvent", %group, %specificGame, %player);
    return ;
}
function GMetricsDoneTouching(%group, %specificGame, %player)
{
    cancel(%player.GMetricsTouchStopTimer[%specificGame]);
    %player.GMetricsTouchStopTimer[%specificGame] = 0;
    GMetrics::GamePlayStopEvent(%group, %specificGame, %player, 0);
    return ;
}
function GMetrics::GameTouchEvent(%group, %specificGame, %player, %TimeToWaitForPlayerToStop)
{
    if (%specificGame $= "")
    {
        error(getScopeName() SPC "specificGame name must be set, it is an empty string. group=" SPC %group SPC getTrace());
        return ;
    }
    if (!isObject(%player.client))
    {
        return ;
    }
    if (%TimeToWaitForPlayerToStop <= 0)
    {
        %TimeToWaitForPlayerToStop = 10000;
    }
    if (%player.GMetricsTouchStopTimer[%specificGame])
    {
        cancel(%player.GMetricsTouchStopTimer[%specificGame]);
        %player.GMetricsTouchStopTimer[%specificGame] = schedule(%TimeToWaitForPlayerToStop, 0, "GMetricsDoneTouching", %group, %specificGame, %player);
    }
    else
    {
        GMetrics::GamePlayStartEvent(%group, %specificGame, %player);
        %player.GMetricsTouchStopTimer[%specificGame] = schedule(%TimeToWaitForPlayerToStop, 0, "GMetricsDoneTouching", %group, %specificGame, %player);
    }
    return ;
}
function GMetrics::GameAwardEvent(%group, %specificGame, %player, %points)
{
    if (%specificGame $= "")
    {
        error(getScopeName() SPC "specificGame name must be set, it is an empty string. group=" SPC %group SPC getTrace());
        return ;
    }
    if (!isObject(%player.client))
    {
        return ;
    }
    if (%points <= 0)
    {
        return ;
    }
    %eventTXT = "[event=award]";
    %detailTXT = "[group=" @ %group @ "][game=" @ %specificGame @ "][user=" @ %player.getShapeName() @ "][points=" @ %points @ "]";
    GMetrics::LogToFile(%eventTXT, %detailTXT);
    return ;
}
function GMetrics::LogToFile(%eventTXT, %detailTXT)
{
    %text = %eventTXT @ %detailTXT;
    %time = getFormattedTime("[%m/%d/%Y %H:%M:%S]");
    $GMetricsLogFile.log("games", "info", %time @ "[games]" @ %text);
    return ;
}

// TODO: Figure out what causes this syntax error crash for the two functions below
// [5/24/2020 21:25:54][Wrn][Script ] >>> Advanced script error report.  Line 937.
// [5/24/2020 21:25:54][Wrn][Script ] >>> Some error context, with ## on sides of error halt:
// [5/24/2020 21:25:54][Err][Script ]         %line = formatString("%-40s", %container.instanceCounts[(%n,##"##class")]) @ formatInt("%5d", %container.instanceCounts[(%n,"count")]);
// [5/24/2020 21:25:54][Wrn][Script ] >>> Error report complete.

function dumpClassInstances(%simGroup)
{
    // if (!isObject(%simGroup))
    // {
    //     return ;
    // }
    // %container = new SimObject();
    // %container.numClasses = 0;
    // %total = 0;
    // compileClassInstances(%simGroup, %container);
    // %n = 0;
    // while (%n < %container.numClasses)
    // {
    //     %line = formatString("%-40s", %container.instanceCounts[(%n,"class")]) @ formatInt("%5d", %container.instanceCounts[(%n,"count")]);
    //     echo(%line);
    //     %total = %total + %container.instanceCounts[(%n,"count")];
    //     %n = %n + 1;
    // }
    // %line = formatString("%-40s", "Total object instances:") @ formatInt("%5d", %total);
    // echo(%line);
    // %container.delete();
    // return ;
}
function compileClassInstances(%obj, %container)
{
    // %classname = %obj.getClassName();
    // %found = -1;
    // %n = 0;
    // while (%found == -1)
    // {
    //     if (%container.instanceCounts[(%n,"class")] $= %classname)
    //     {
    //         %found = %n;
    //     }
    //     %n = %n + 1;
    // }
    // if (%found == -1)
    // {
    //     %found = %container.numClasses;
    //     %container.numClasses = %container.numClasses + 1;
    //     %container.instanceCounts[%found,"class"] = %classname;
    // }
    // %curr = %container.instanceCounts[(%found,"count")];
    // %curr = %curr $= "" ? 0 : %curr;
    // %container.instanceCounts[%found,"count"] = %curr + 1;
    // if (%obj.isClassSimGroup())
    // {
    //     %n = %obj.getCount() - 1;
    //     while (%n >= 0)
    //     {
    //         %child = %obj.getObject(%n);
    //         compileClassInstances(%child, %container);
    //         %n = %n - 1;
    //     }
    // }
}


