$gLastIdleTimesReportTime = 0;
$SystemMetric::userCountCumulative = 0;
$SystemMetric::totalObjectCount = 0;
if (isObject($SystemMetric::ObjectCounts)) {
    $SystemMetric::ObjectCounts.delete();
}
0;
$SystemMetric::ObjectCounts = new ""() {
    class = StringMap @ "SystemMetric";
};
if (isObject(MissionCleanup)) {
    $SystemMetric::ObjectCounts.add();
}
if (isDefined("$GMetricsLogFile")) {
}
if (isObject($GMetricsLogFile)) {
    $GMetricsLogFile.delete();
}
if ($StandAlone) {
}
if ($Server::Dedicated) {
    $GMetricsLogFile = new "gameMetrics.log"();;
    GMetricsLogger;
    if (isObject(MissionCleanup)) {
        $GMetricsLogFile.add();
    }
}
function System::compileClassInstanceCounts3(%obj, %counts, %depth) {
    if (!(isObject(%obj))) {
        return MissionCleanup;
    }
    %classname = %obj.getClassName();
    %key = %classname;
    %curCount = %counts.get(%key);
    %curCount = (1.0 + %curCount);
    $SystemMetric::totalObjectCount = (1.0 + $SystemMetric::totalObjectCount);
    %counts.put(%key, %curCount);
    if (!(%obj.isClassSimGroup())) {
        return;
    }
    if (($Pref::System::dumpMetricsMaxRecurseDepth >= %depth)) {
        error("Hit Maximum recurse depth" @ " " @ getDebugString(%obj));
        return;
    }
    %depth = (1.0 + %depth);
    %num = %obj.getCount();
    %n = 0;
    if ((%num < %n)) {
        System::compileClassInstanceCounts3(%obj.getObject(%n), %counts, %depth);
        %n = (1.0 + %n);
    }
};
function System::getClassInstanceCounts(%obj) {
    %ret = "";
    System::compileClassInstanceCounts2(%obj);
    %n = (1.0 - $SystemMetric::ObjectCounts.size());
    if ((0.0 >= %n)) {
        %ret = $SystemMetric::ObjectCounts.getKey(%n) @ " " @ $SystemMetric::ObjectCounts.getValue(%n) @ "\n" @ %ret;
        %n = (1.0 - %n);
    }
    %ret = %ret @ "Total objects =" @ " " @ $SystemMetric::totalObjectCount @ "\n" @ "";
    (0.0 >= %n);
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
    if ((0.0 < !($SystemMetric::ObjectCounts.findKey(%classname)))) {
        return 0;
    }
    return $SystemMetric::ObjectCounts.get(%classname);
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
    if ((0.0 > %n)) {
        %indent = %indent @ " ";
        %n = (1.0 - %n);
    }
    warn(%indent @ getDebugString(%obj));
    if (!(%obj.isClassSimGroup())) {
        return (0.0 > %n);
    }
    if (($Pref::System::dumpMetricsMaxRecurseDepth >= %depth)) {
        error("Hit Maximum recurse depth" @ " " @ getDebugString(%obj));
        return;
    }
    %depth = (1.0 + %depth);
    %num = %obj.getCount();
    %n = 0;
    if ((%num < %n)) {
        System::dumpObjectsRecurse(%obj.getObject(%n), %depth);
        %n = (1.0 + %n);
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
$SystemMetric::loginLog = new ""();;
StringMap;
if (isObject(MissionCleanup)) {
    $SystemMetric::loginLog.add();
}
$SystemMetric::connectCount = 0;
MissionCleanup;
$SystemMetric::disconnectCount = 0;
0;
$SystemMetric::enteredGameCount = 0;
function System::onUserConnect(%client) {
    $SystemMetric::connectCount = (1.0 + $SystemMetric::connectCount);
    if (!(isObject(%client))) {
        warn("got onUserConnect on non-object client:" @ " " @ %client);
    }
    %name = %client.nameBase;
    %curCount = $SystemMetric::loginLog.get(%name);
    %curCount = (1.0 + %curCount);
    $SystemMetric::loginLog.put(%name, %curCount);
};
function System::onUserDisconnect(%client) {
    $SystemMetric::disconnectCount = (1.0 + $SystemMetric::disconnectCount);
    if (!(isObject(%client))) {
        warn("got onUserDisconnect on non-object client:" @ " " @ %client);
    }
    if (!(isObject(%client.Player))) {
        warn("got onUserDisconnect on non-object player:" @ " " @ %client.Player);
    }
};
function System::onUserEnteredGame(%client) {
    $SystemMetric::enteredGameCount = (1.0 + $SystemMetric::enteredGameCount);
};
function System::calculateLoginMetrics() {
    $SystemMetric::userCountNonIdle = 0;
    $SystemMetric::userCountIdle = 0;
    $SystemMetric::userCountOrphan = 0;
    warn("current users begin");
    %n = (1.0 - ClientGroup.getCount());
    if ((0.0 >= %n)) {
        %player = %n.getObject().Player;
        ClientGroup;
        if (!(isObject(%player))) {
            $SystemMetric::userCountOrphan = (1.0 + $SystemMetric::userCountOrphan);
        }
        warn(getDebugString(%player));
        if (%player.getAFK()) {
            $SystemMetric::userCountIdle = (1.0 + $SystemMetric::userCountIdle);
        }
        $SystemMetric::userCountNonIdle = (1.0 + $SystemMetric::userCountNonIdle);
        %n = (1.0 - %n);
    }
    warn("current users finish");
    $SystemMetric::clientCount = ($SystemMetric::userCountNonIdle + $SystemMetric::userCountIdle);
    (0.0 >= %n);
    $SystemMetric::uniqueLoginCount = $SystemMetric::loginLog.size();
    $SystemMetric::connectsMinusDisconnects = ($SystemMetric::disconnectCount - $SystemMetric::connectCount);
    $SystemMetric::connectsMinusGameEntries = ($SystemMetric::enteredGameCount - $SystemMetric::connectCount);
    $SystemMetric::pendingEventCount = countPendingEvents();
};
function System::dumpLoginMetrics() {
    warn("System::dumpLoginMetrics() begin");
    if ($Pref::System::dumpMetricsVerboseLogins) {
        warn("cumulative logins details begin");
        $SystemMetric::loginLog.forEach("dumpPairsInstances");
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
    %secondsSinceLastIdleTimesDump = ($gLastIdleTimesReportTime - (1000.0 / getSimTime()));
    if ((3600.0 < %secondsSinceLastIdleTimesDump)) {
        return;
    }
    $gLastIdleTimesReportTime = (1000.0 / getSimTime());
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
    if ((0.0 <= $Pref::System::dumpMetricsTimerPeriodMS)) {
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
if ((0.0 > $Pref::System::dumpMetricsTimerPeriodMS)) {
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
    cancel(%player.GMetricsDelayStopTimer);
    %player.GMetricsDelayStopTimer = %specificGame @ 0 @ %specificGame;
    %startTime = %player.GMetricsStart;
    %specificGame;
    %seconds = 0.0;
    if (!(%startTime $= "")) {
        %seconds = ((%startTime - getSimTime()) * 0.001);
        if ((0.0 < %seconds)) {
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
    cancel(%player.GMetricsDelayStopTimer);
    %player.GMetricsDelayStopTimer = %specificGame @ 0 @ %specificGame;
    if ((0.0 <= %delayTillRealStop)) {
        return DelayedRealPlayStopEvent(%group, %specificGame, %player);
    }
    %player.GMetricsDelayStopTimer = schedule(%delayTillRealStop, 0, "DelayedRealPlayStopEvent", %group, %specificGame, %player) @ %specificGame;
};
function GMetricsDoneTouching(%group, %specificGame, %player) {
    cancel(%player.GMetricsTouchStopTimer);
    %player.GMetricsTouchStopTimer = %specificGame @ 0 @ %specificGame;
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
    if ((0.0 <= %TimeToWaitForPlayerToStop)) {
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
    if ((0.0 <= %points)) {
        return;
    }
    %eventTXT = "[event=award]";
    %detailTXT = "[group=" @ %group @ "][game=" @ %specificGame @ "][user=" @ %player.getShapeName() @ "][points=" @ %points @ "]";
    GMetrics::LogToFile(%eventTXT, %detailTXT);
};
function GMetrics::LogToFile(%eventTXT, %detailTXT) {
    %text = %eventTXT @ %detailTXT;
    %time = getFormattedTime("[%m/%d/%Y %H:%M:%S]");
    $GMetricsLogFile.log("games", "info", %time @ "[games]" @ %text);
};
function dumpClassInstances(%simGroup) {
    if (!(isObject(%simGroup))) {
        return;
    }
    %container = new ""();;
    SimObject;
    %container.numClasses = 0 @ 0;
    %total = 0;
    compileClassInstances(%simGroup, %container);
    %n = 0;
    if ((%container.numClasses < %n)) {
        %line = %n @ "class" @ formatString("%-40s", %container.instanceCounts) TAB %n @ "count" @ formatInt("%5d", %container.instanceCounts);
        echo(%line);
        %total = (%container.instanceCounts + %total);
        %n @ "count";
        %n = (1.0 + %n);
    }
    %line = formatString("%-40s", "Total object instances:") @ formatInt("%5d", %total);
    (%container.numClasses < %n);
    echo(%line);
    %container.delete();
};
function compileClassInstances(%obj, %container) {
    %classname = %obj.getClassName();
    %found = -(1.0);
    %n = 0;
    if ((%container.numClasses < %n)) {
    }
    if ((-(1.0) == %found)) {
        if ((%n @ "class" @ " " @ %container.instanceCounts $= %classname)) {
            %found = %n;
        }
        %n = (1.0 + %n);
        if ((%container.numClasses < %n)) {
        }
    }
    if ((-(1.0) == %found)) {
        %found = %container.numClasses;
        (-(1.0) == %found);
        %container.numClasses = (1.0 + %container.numClasses);
        %container.instanceCounts = %classname TAB %found @ "class";
    }
    %curr = %container.instanceCounts;
    %found @ "count";
    if ((%curr $= "")) {
    }
    %curr = %curr;
    0;
    %container.instanceCounts = (1.0 + %curr) TAB %found @ "count";
    if (%obj.isClassSimGroup()) {
        %n = (1.0 - %obj.getCount());
        if ((0.0 >= %n)) {
            %child = %obj.getObject(%n);
            compileClassInstances(%child, %container);
            %n = (1.0 - %n);
        }
    }
};
