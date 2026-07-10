$gLastIdleTimesReportTime = 0;
$SystemMetric::userCountCumulative = 0;
$SystemMetric::totalObjectCount = 0;
if (isObject($SystemMetric::ObjectCounts)) {
    $SystemMetric::ObjectCounts.delete();
}
$SystemMetric::ObjectCounts = new StringMap("") {
    class = "SystemMetric";
};
if (isObject(MissionCleanup)) {
    $SystemMetric::ObjectCounts.add(MissionCleanup);
}
if (isDefined("$GMetricsLogFile")) {
}
if (isObject($GMetricsLogFile)) {
    $GMetricsLogFile.delete();
}
if ($StandAlone) {
}
if ($Server::Dedicated) {
    $GMetricsLogFile = new FileLogger(GMetricsLogger);
    if (isObject(MissionCleanup)) {
        $GMetricsLogFile.add(MissionCleanup);
    }
}
function System::compileClassInstanceCounts3(%obj, %counts, %depth) {
    if (!(isObject(%obj))) {
        return;
    }
    %classname = %obj.getClassName();
    %key = %classname;
    %curCount = %key.get(%counts);
    %curCount = (%curCount + 1.0);
    $SystemMetric::totalObjectCount = ($SystemMetric::totalObjectCount + 1.0);
    %curCount.put(%counts, %key);
    if (!(%obj.isClassSimGroup())) {
        return;
    }
    if ((%depth >= $Pref::System::dumpMetricsMaxRecurseDepth)) {
        error("Hit Maximum recurse depth" @ " " @ getDebugString(%obj));
        return;
    }
    %depth = (%depth + 1.0);
    %num = %obj.getCount();
    %n = 0;
    while ((%n < %num)) {
        System::compileClassInstanceCounts3(%n.getObject(%obj), %counts, %depth);
        %n = (%n + 1.0);
    }
};
function System::getClassInstanceCounts(%obj) {
    %ret = "";
    System::compileClassInstanceCounts2(%obj);
    %n = ($SystemMetric::ObjectCounts.size() - 1.0);
    while ((%n >= 0.0)) {
        %ret = %n.getKey($SystemMetric::ObjectCounts) @ " " @ %n.getValue($SystemMetric::ObjectCounts) @ "\n" @ %ret;
        %n = (%n - 1.0);
    }
    %ret = %ret @ "Total objects =" @ " " @ $SystemMetric::totalObjectCount @ "\n" @ "";
    (%n >= 0.0);
    return %ret;
};
function System::dumpClassInstanceCounts(%obj) {
    warn("System::dumpClassInstanceCounts() begin");
    warn(System::getClassInstanceCounts(%obj));
    warn("System::dumpClassInstanceCounts() finish");
};
function System::compileClassInstanceCounts() {
    System::compileClassInstanceCounts2(RootGroup);
};
function System::compileClassInstanceCounts2(%obj) {
    $SystemMetric::totalObjectCount = 0;
    $SystemMetric::ObjectCounts.clear();
    System::compileClassInstanceCounts3(%obj, $SystemMetric::ObjectCounts, 0);
};
function System::getClassInstanceCount(%classname) {
    if ((!(%classname.findKey($SystemMetric::ObjectCounts)) < 0.0)) {
        return 0;
    }
    return %classname.get($SystemMetric::ObjectCounts);
};
function SystemMetric::dumpPairsInstances(%unused, %key, %value) {
    warn(%value @ " " @ %key);
};
function SystemMetric::dumpKeyValuePairs(%unused, %key, %value) {
    warn(%key @ " " @ %value);
};
function System::dumpObjectsRecurse(%obj, %depth) {
    if (!(isObject(%obj))) {
        warn("not an object -" @ " " @ %obj);
        return;
    }
    %indent = "";
    %n = %depth;
    while ((%n > 0.0)) {
        %indent = %indent @ " ";
        %n = (%n - 1.0);
    }
    warn(%indent @ getDebugString(%obj));
    if (!(%obj.isClassSimGroup())) {
        return (%n > 0.0);
    }
    if ((%depth >= $Pref::System::dumpMetricsMaxRecurseDepth)) {
        error("Hit Maximum recurse depth" @ " " @ getDebugString(%obj));
        return;
    }
    %depth = (%depth + 1.0);
    %num = %obj.getCount();
    %n = 0;
    while ((%n < %num)) {
        System::dumpObjectsRecurse(%n.getObject(%obj), %depth);
        %n = (%n + 1.0);
    }
};
function System::dumpObjects(%obj) {
    warn("System::dumpObjects() begin");
    System::dumpObjectsRecurse(%obj, 0);
    warn("System::dumpObjects() finish");
};
if (isObject($System::LoginLog)) {
    $System::LoginLog.delete();
}
$SystemMetric::loginLog = new StringMap("");
if (isObject(MissionCleanup)) {
    $SystemMetric::loginLog.add(MissionCleanup);
}
$SystemMetric::connectCount = 0;
$SystemMetric::disconnectCount = 0;
$SystemMetric::enteredGameCount = 0;
function System::onUserConnect(%client) {
    $SystemMetric::connectCount = ($SystemMetric::connectCount + 1.0);
    if (!(isObject(%client))) {
        warn("got onUserConnect on non-object client:" @ " " @ %client);
    }
    %name = %client.nameBase;
    %curCount = %name.get($SystemMetric::loginLog);
    %curCount = (%curCount + 1.0);
    %curCount.put($SystemMetric::loginLog, %name);
};
function System::onUserDisconnect(%client) {
    $SystemMetric::disconnectCount = ($SystemMetric::disconnectCount + 1.0);
    if (!(isObject(%client))) {
        warn("got onUserDisconnect on non-object client:" @ " " @ %client);
    }
    if (!(isObject(%client.Player))) {
        warn("got onUserDisconnect on non-object player:" @ " " @ %client.Player);
    }
};
function System::onUserEnteredGame(%client) {
    $SystemMetric::enteredGameCount = ($SystemMetric::enteredGameCount + 1.0);
};
function System::calculateLoginMetrics() {
    $SystemMetric::userCountNonIdle = 0;
    $SystemMetric::userCountIdle = 0;
    $SystemMetric::userCountOrphan = 0;
    warn("current users begin");
    %n = (ClientGroup.getCount() - 1.0);
    while ((%n >= 0.0)) {
        %player = %n.getObject(ClientGroup).Player;
        if (!(isObject(%player))) {
            $SystemMetric::userCountOrphan = ($SystemMetric::userCountOrphan + 1.0);
        }
        warn(getDebugString(%player));
        if (%player.getAFK()) {
            $SystemMetric::userCountIdle = ($SystemMetric::userCountIdle + 1.0);
        }
        $SystemMetric::userCountNonIdle = ($SystemMetric::userCountNonIdle + 1.0);
        %n = (%n - 1.0);
    }
    warn("current users finish");
    $SystemMetric::clientCount = ($SystemMetric::userCountIdle + $SystemMetric::userCountNonIdle);
    (%n >= 0.0);
    $SystemMetric::uniqueLoginCount = $SystemMetric::loginLog.size();
    $SystemMetric::connectsMinusDisconnects = ($SystemMetric::connectCount - $SystemMetric::disconnectCount);
    $SystemMetric::connectsMinusGameEntries = ($SystemMetric::connectCount - $SystemMetric::enteredGameCount);
    $SystemMetric::pendingEventCount = countPendingEvents();
};
function System::dumpLoginMetrics() {
    warn("System::dumpLoginMetrics() begin");
    if ($Pref::System::dumpMetricsVerboseLogins) {
        warn("cumulative logins details begin");
        "dumpPairsInstances".forEach($SystemMetric::loginLog);
        warn("cumulative logins details finish");
    }
    warn("events pending                :" @ " " @ $SystemMetric::pendingEventCount);
    warn("cumulative connects           :" @ " " @ $SystemMetric::connectCount);
    warn("cumulative disconnects        :" @ " " @ $SystemMetric::disconnectCount);
    warn("connects-disconnects          :" @ " " @ $SystemMetric::connectsMinusDisconnects);
    warn("cumulative game entries       :" @ " " @ $SystemMetric::enteredGameCount);
    warn("connects-game entries         :" @ " " @ $SystemMetric::connectsMinusGameEntries);
    warn("unique logins                 :" @ " " @ $SystemMetric::uniqueLoginCount);
    warn("current user count            :" @ " " @ $SystemMetric::clientCount);
    warn("current users idle            :" @ " " @ $SystemMetric::userCountIdle);
    warn("current users nonidle         :" @ " " @ $SystemMetric::userCountNonIdle);
    warn("current user orphans          :" @ " " @ $SystemMetric::userCountOrphan);
    warn("System::dumpLoginMetrics() finish");
};
function System::dumpMetrics() {
    warn("System::dumpMetrics() begin");
    if ($Pref::System::dumpMetricsVerboseObjects) {
        if ($AmClient) {
            warn("complete object dump crashes on client - skipped.");
        }
        System::dumpObjects(RootGroup);
    }
    System::dumpClassInstanceCounts(RootGroup);
    System::calculateLoginMetrics();
    System::dumpLoginMetrics();
    dumpIdleTimes();
    warn("System::dumpMetrics() finish");
};
function dumpIdleTimes() {
    %secondsSinceLastIdleTimesDump = ((getSimTime() / 1000.0) - $gLastIdleTimesReportTime);
    if ((%secondsSinceLastIdleTimesDump < 3600.0)) {
        return;
    }
    $gLastIdleTimesReportTime = (getSimTime() / 1000.0);
    safeEnsureScriptObject("StringMap", "gIdleTimesReport");
    gIdleTimesReport.dumpValues();
    gIdleTimesReport.clear();
};
function SystemDumpMetricsTimer() {
    cancel($System::dumpMetricsTimerID);
    if ($Game::Compile) {
        warn("deactivating SystemDumpMetricsTimer because $Game::Compile is " @ " " @ $Game::Compile);
        return;
    }
    if (($Pref::System::dumpMetricsTimerPeriodMS <= 0.0)) {
        error("deactivating SystemDumpMetricsTimer because timer period is " @ " " @ $Pref::System::dumpMetricsTimerPeriodMS);
        return;
    }
    if ($AmClient) {
    }
    if (!($Pref::System::dumpMetricsOnStandAloneClient)) {
        echo("deactivating SystemDumpMetricsTimer because am client");
        return;
    }
    System::dumpMetrics();
    $System::dumpMetricsTimerID = schedule($Pref::System::dumpMetricsTimerPeriodMS, 0, "SystemDumpMetricsTimer");
};
cancel($System::dumpMetricsTimerID);
if (($Pref::System::dumpMetricsTimerPeriodMS > 0.0)) {
    $System::dumpMetricsTimerID = schedule($Pref::System::dumpMetricsTimerPeriodMS, 0, "SystemDumpMetricsTimer");
}
function GMetrics::GamePlayStartEvent(%group, %specificGame, %player) {
    if ((%specificGame $= "")) {
        error(getScopeName() @ " " @ "specificGame name must be set, it is an empty string. group=" @ " " @ %group @ " " @ getTrace());
        return;
    }
    if (!(isObject(%player.client))) {
        return;
    }
    if (%player.GMetricsDelayStopTimer) {
        cancel(%player.GMetricsDelayStopTimer);
        %player.GMetricsDelayStopTimer = %specificGame @ %specificGame @ 0 @ %specificGame;
        return;
    }
    if (!(%specificGame @ " " @ %player.GMetricsStart $= "")) {
    }
    %player.GMetricsStart = getSimTime() @ %specificGame;
};
function DelayedRealPlayStopEvent(%group, %specificGame, %player) {
    if (!(isObject(%player))) {
        echo(getScopeName() @ " " @ %group @ " " @ %specificGame @ " " @ "player is not an object, not logging this:" @ " " @ %player);
        return;
    }
    cancel(%specificGame, %player.GMetricsDelayStopTimer);
    %player.GMetricsDelayStopTimer = 0 @ %specificGame;
    %startTime = %player.GMetricsStart;
    %specificGame;
    %seconds = 0.0;
    if (!(%startTime $= "")) {
        %seconds = (0.001 * (getSimTime() - %startTime));
        if ((%seconds < 0.0)) {
            error(getScopeName() @ " " @ %specificGame @ " " @ "got stop but had no startime just using:" @ " " @ %seconds @ " " @ "for duration.");
            %seconds = 0.0;
        }
    }
    error(getScopeName() @ " " @ %specificGame @ " " @ "got stop but had no startime just using:" @ " " @ %seconds @ " " @ "for duration.");
    %player.GMetricsStart = "" @ %specificGame;
    %duration = formatFloat("%0.0f", mCeil(%seconds));
    %eventTXT = "[event=playstop]";
    %detailTXT = "[group=" @ %group @ "][game=" @ %specificGame @ "][user=" @ %player.getShapeName() @ "][duration=" @ %duration @ "]";
    GMetrics::LogToFile(%eventTXT, %detailTXT);
};
function GMetrics::GamePlayStopEvent(%group, %specificGame, %player, %delayTillRealStop) {
    if ((%specificGame $= "")) {
        error(getScopeName() @ " " @ "specificGame name must be set, it is an empty string. group=" @ " " @ %group @ " " @ getTrace());
        return;
    }
    if (!(isObject(%player.client))) {
        return;
    }
    cancel(%specificGame, %player.GMetricsDelayStopTimer);
    %player.GMetricsDelayStopTimer = 0 @ %specificGame;
    if ((%delayTillRealStop <= 0.0)) {
        return DelayedRealPlayStopEvent(%group, %specificGame, %player);
    }
    %player.GMetricsDelayStopTimer = schedule(%delayTillRealStop, 0, "DelayedRealPlayStopEvent", %group, %specificGame, %player) @ %specificGame;
};
function GMetricsDoneTouching(%group, %specificGame, %player) {
    cancel(%specificGame, %player.GMetricsTouchStopTimer);
    %player.GMetricsTouchStopTimer = 0 @ %specificGame;
    GMetrics::GamePlayStopEvent(%group, %specificGame, %player, 0);
};
function GMetrics::GameTouchEvent(%group, %specificGame, %player, %TimeToWaitForPlayerToStop) {
    if ((%specificGame $= "")) {
        error(getScopeName() @ " " @ "specificGame name must be set, it is an empty string. group=" @ " " @ %group @ " " @ getTrace());
        return;
    }
    if (!(isObject(%player.client))) {
        return;
    }
    if ((%TimeToWaitForPlayerToStop <= 0.0)) {
        %TimeToWaitForPlayerToStop = 10000;
    }
    if (%player.GMetricsTouchStopTimer) {
        cancel(%player.GMetricsTouchStopTimer);
        %player.GMetricsTouchStopTimer = %specificGame @ %specificGame @ schedule(%TimeToWaitForPlayerToStop, 0, "GMetricsDoneTouching", %group, %specificGame, %player) @ %specificGame;
    }
    GMetrics::GamePlayStartEvent(%group, %specificGame, %player);
    %player.GMetricsTouchStopTimer = schedule(%TimeToWaitForPlayerToStop, 0, "GMetricsDoneTouching", %group, %specificGame, %player) @ %specificGame;
};
function GMetrics::GameAwardEvent(%group, %specificGame, %player, %points) {
    if ((%specificGame $= "")) {
        error(getScopeName() @ " " @ "specificGame name must be set, it is an empty string. group=" @ " " @ %group @ " " @ getTrace());
        return;
    }
    if (!(isObject(%player.client))) {
        return;
    }
    if ((%points <= 0.0)) {
        return;
    }
    %eventTXT = "[event=award]";
    %detailTXT = "[group=" @ %group @ "][game=" @ %specificGame @ "][user=" @ %player.getShapeName() @ "][points=" @ %points @ "]";
    GMetrics::LogToFile(%eventTXT, %detailTXT);
};
function GMetrics::LogToFile(%eventTXT, %detailTXT) {
    %text = %eventTXT @ %detailTXT;
    %time = getFormattedTime("[%m/%d/%Y %H:%M:%S]");
    %time @ "[games]" @ %text.log($GMetricsLogFile, "games", "info");
};
function dumpClassInstances(%simGroup) {
    if (!(isObject(%simGroup))) {
        return;
    }
    %container = new SimObject("");
    %container.numClasses = 0;
    %total = 0;
    compileClassInstances(%simGroup, %container);
    %n = 0;
    while ((%n < %container.numClasses)) {
        %line = formatString("%-40s", %n @ "class", %container.instanceCounts) TAB %n @ "count" @ formatInt("%5d", %container.instanceCounts);
        echo(%line);
        %total = (%total + %container.instanceCounts);
        %n @ "count";
        %n = (%n + 1.0);
    }
    %line = formatString("%-40s", "Total object instances:") @ formatInt("%5d", %total);
    (%n < %container.numClasses);
    echo(%line);
    %container.delete();
};
function compileClassInstances(%obj, %container) {
    %classname = %obj.getClassName();
    %found = -(1.0);
    %n = 0;
    if ((%n < %container.numClasses)) {
    }
    while ((%found == -(1.0))) {
        if ((%n @ "class" @ " " @ %container.instanceCounts $= %classname)) {
            %found = %n;
        }
        %n = (%n + 1.0);
        if ((%n < %container.numClasses)) {
        }
    }
    if ((%found == -(1.0))) {
        %found = %container.numClasses;
        (%found == -(1.0));
        %container.numClasses = (%container.numClasses + 1.0);
        %container.instanceCounts = %classname TAB %found @ "class";
    }
    %curr = %container.instanceCounts;
    %found @ "count";
    if ((%curr $= "")) {
    }
    %curr = %curr;
    0;
    %container.instanceCounts = (%curr + 1.0) TAB %found @ "count";
    if (%obj.isClassSimGroup()) {
        %n = (%obj.getCount() - 1.0);
        while ((%n >= 0.0)) {
            %child = %n.getObject(%obj);
            compileClassInstances(%child, %container);
            %n = (%n - 1.0);
        }
    }
};
