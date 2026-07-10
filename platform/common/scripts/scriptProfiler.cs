$gScriptProfiler_Active = 0;
$gScriptProfiler_EntryTimes = 0;
$gScriptProfiler_TotalTimes = 0;
$gScriptProfiler_TotalCalls = 0;
$gScriptProfiler_ValidChars = "abcdefghijklmnopqrstuvwxyz" @ "ABCDEFGHIJKLMNOPQRSTUVWXYZ" @ "0123456789-_:()-<>";
function scriptProfiler_Initialize() {
    if (isObject($gScriptProfiler_EntryTimes)) {
        $gScriptProfiler_EntryTimes.clear();
        $gScriptProfiler_TotalTimes.clear();
        $gScriptProfiler_TotalCalls.clear();
        return;
    }
    $gScriptProfiler_EntryTimes = safeNewScriptObject("StringMap", "", 0);
    $gScriptProfiler_TotalTimes = safeNewScriptObject("StringMap", "", 0);
    $gScriptProfiler_TotalCalls = safeNewScriptObject("StringMap", "", 0);
};
function scriptProfiler_GetScopeId() {
    return stripString(getScopeName(2), $gScriptProfiler_ValidChars, "_");
};
function scriptProfiler_EnterScope() {
    if (!($gScriptProfiler_Active)) {
        return;
    }
    scriptProfiler_EnterSection(scriptProfiler_GetScopeId());
};
function scriptProfiler_EnterScopeSection(%sectionId) {
    if (!($gScriptProfiler_Active)) {
        return;
    }
    scriptProfiler_EnterSection(scriptProfiler_GetScopeId() @ "_" @ %sectionId);
};
function scriptProfiler_EnterSection(%sectionId) {
    %curTime = getSimTime();
    %curTime.put($gScriptProfiler_EntryTimes, %sectionId);
    %sectionId.get($gScriptProfiler_TotalTimes).put($gScriptProfiler_TotalTimes, %sectionId);
    (%sectionId.get($gScriptProfiler_TotalCalls) + 1.0).put($gScriptProfiler_TotalCalls, %sectionId);
};
function scriptProfiler_LeaveScope() {
    if (!($gScriptProfiler_Active)) {
        return;
    }
    scriptProfiler_LeaveSection(scriptProfiler_GetScopeId());
};
function scriptProfiler_LeaveScopeSection(%sectionId) {
    if (!($gScriptProfiler_Active)) {
        return;
    }
    scriptProfiler_LeaveSection(scriptProfiler_GetScopeId() @ "_" @ %sectionId);
};
function scriptProfiler_LeaveSection(%sectionId) {
    if (!($gScriptProfiler_Active)) {
        return;
    }
    %ntrTime = %sectionId.get($gScriptProfiler_EntryTimes);
    %ttlTime = %sectionId.get($gScriptProfiler_TotalTimes);
    %dltTime = mSubS32(getSimTime(), %ntrTime);
    %newTime = mAddS32(%ttlTime, %dltTime);
    if (0) {
        error("ntrTime" @ " " @ %ntrTime);
        error("ttlTime" @ " " @ %ttlTime);
        error("dltTime" @ " " @ %dltTime);
        error("newTime" @ " " @ %newTime);
    }
    %newTime.put($gScriptProfiler_TotalTimes, %sectionId);
    "".put($gScriptProfiler_EntryTimes, %sectionId);
};
function scriptProfiler_getTotals() {
    %ret = "";
    %delim = "";
    %n = ($gScriptProfiler_TotalTimes.size() - 1.0);
    while ((%n >= 0.0)) {
        %time = formatFloat("%10.4f", (%n.getValue($gScriptProfiler_TotalTimes) / 1000.0));
        %id = %n.getKey($gScriptProfiler_TotalTimes);
        %id2 = formatString("%-70s", %id);
        %bal = (%id.get($gScriptProfiler_EntryTimes) $= "") ? "(  balanced)" : "(unbalanced)";
        %cls = formatInt("%5d", %n.getValue($gScriptProfiler_TotalCalls));
        %ret = %ret @ %delim @ %time @ " " @ %bal @ " " @ %cls @ " " @ %id2;
        %delim = "\n";
        %n = (%n - 1.0);
    }
    return %ret;
};
function scriptProfiler_dumpTotals() {
    echo("\n" @ scriptProfiler_getTotals());
};
function scriptProfiler_reset() {
    scriptProfiler_Initialize();
};
function scriptProfiler_activate() {
    $gScriptProfiler_Active = 1;
};
function scriptProfiler_deactivate() {
    $gScriptProfiler_Active = 0;
};
function scriptProfiler_foo() {
    scriptProfiler_EnterScope();
    %n = 0;
    while ((%n < 200000.0)) {
        %m = (%m + mSqrt(%n));
        %n = (%n + 1.0);
    }
    scriptProfiler_LeaveScope();
};
scriptProfiler_Initialize();
