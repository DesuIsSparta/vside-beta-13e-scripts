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
    $gScriptProfiler_EntryTimes.put(%sectionId, %curTime);
    $gScriptProfiler_TotalTimes.put(%sectionId, $gScriptProfiler_TotalTimes.get(%sectionId));
    $gScriptProfiler_TotalCalls.put(%sectionId, (1.0 + $gScriptProfiler_TotalCalls.get(%sectionId)));
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
    %ntrTime = $gScriptProfiler_EntryTimes.get(%sectionId);
    %ttlTime = $gScriptProfiler_TotalTimes.get(%sectionId);
    %dltTime = mSubS32(getSimTime(), %ntrTime);
    %newTime = mAddS32(%ttlTime, %dltTime);
    if (0) {
        error("ntrTime" @ " " @ %ntrTime);
        error("ttlTime" @ " " @ %ttlTime);
        error("dltTime" @ " " @ %dltTime);
        error("newTime" @ " " @ %newTime);
    }
    $gScriptProfiler_TotalTimes.put(%sectionId, %newTime);
    $gScriptProfiler_EntryTimes.put(%sectionId, "");
};
function scriptProfiler_getTotals() {
    %ret = "";
    %delim = "";
    %n = (1.0 - $gScriptProfiler_TotalTimes.size());
    if ((0.0 >= %n)) {
        %time = formatFloat("%10.4f", (1000.0 / $gScriptProfiler_TotalTimes.getValue(%n)));
        %id = $gScriptProfiler_TotalTimes.getKey(%n);
        %id2 = formatString("%-70s", %id);
        %bal = ($gScriptProfiler_EntryTimes.get(%id) $= "") ? "(  balanced)" : "(unbalanced)";
        %cls = formatInt("%5d", $gScriptProfiler_TotalCalls.getValue(%n));
        %ret = %ret @ %delim @ %time @ " " @ %bal @ " " @ %cls @ " " @ %id2;
        %delim = "\n";
        %n = (1.0 - %n);
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
    if ((200000.0 < %n)) {
        %m = (mSqrt(%n) + %m);
        %n = (1.0 + %n);
    }
    scriptProfiler_LeaveScope();
};
scriptProfiler_Initialize();
