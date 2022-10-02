$gScriptProfiler_Active = 0;
$gScriptProfiler_EntryTimes = 0;
$gScriptProfiler_TotalTimes = 0;
$gScriptProfiler_TotalCalls = 0;
$gScriptProfiler_ValidChars = "abcdefghijklmnopqrstuvwxyz" @ "ABCDEFGHIJKLMNOPQRSTUVWXYZ" @ "0123456789-_:()-<>";
function scriptProfiler_Initialize()
{
    if (isObject($gScriptProfiler_EntryTimes))
    {
        $gScriptProfiler_EntryTimes.clear();
        $gScriptProfiler_TotalTimes.clear();
        $gScriptProfiler_TotalCalls.clear();
        return ;
    }
    $gScriptProfiler_EntryTimes = safeNewScriptObject("StringMap", "", 0);
    $gScriptProfiler_TotalTimes = safeNewScriptObject("StringMap", "", 0);
    $gScriptProfiler_TotalCalls = safeNewScriptObject("StringMap", "", 0);
    return ;
}
function scriptProfiler_GetScopeId()
{
    return stripString(getScopeName(2), $gScriptProfiler_ValidChars, "_");
}
function scriptProfiler_EnterScope()
{
    if (!$gScriptProfiler_Active)
    {
        return ;
    }
    scriptProfiler_EnterSection(scriptProfiler_GetScopeId());
    return ;
}
function scriptProfiler_EnterScopeSection(%sectionId)
{
    if (!$gScriptProfiler_Active)
    {
        return ;
    }
    scriptProfiler_EnterSection(scriptProfiler_GetScopeId() @ "_" @ %sectionId);
    return ;
}
function scriptProfiler_EnterSection(%sectionId)
{
    %curTime = getSimTime();
    $gScriptProfiler_EntryTimes.put(%sectionId, %curTime);
    $gScriptProfiler_TotalTimes.put(%sectionId, $gScriptProfiler_TotalTimes.get(%sectionId));
    $gScriptProfiler_TotalCalls.put(%sectionId, $gScriptProfiler_TotalCalls.get(%sectionId) + 1);
    return ;
}
function scriptProfiler_LeaveScope()
{
    if (!$gScriptProfiler_Active)
    {
        return ;
    }
    scriptProfiler_LeaveSection(scriptProfiler_GetScopeId());
    return ;
}
function scriptProfiler_LeaveScopeSection(%sectionId)
{
    if (!$gScriptProfiler_Active)
    {
        return ;
    }
    scriptProfiler_LeaveSection(scriptProfiler_GetScopeId() @ "_" @ %sectionId);
    return ;
}
function scriptProfiler_LeaveSection(%sectionId)
{
    if (!$gScriptProfiler_Active)
    {
        return ;
    }
    %ntrTime = $gScriptProfiler_EntryTimes.get(%sectionId);
    %ttlTime = $gScriptProfiler_TotalTimes.get(%sectionId);
    %dltTime = mSubS32(getSimTime(), %ntrTime);
    %newTime = mAddS32(%ttlTime, %dltTime);
    if (0)
    {
        error("ntrTime" SPC %ntrTime);
        error("ttlTime" SPC %ttlTime);
        error("dltTime" SPC %dltTime);
        error("newTime" SPC %newTime);
    }
    $gScriptProfiler_TotalTimes.put(%sectionId, %newTime);
    $gScriptProfiler_EntryTimes.put(%sectionId, "");
    return ;
}
function scriptProfiler_getTotals()
{
    %ret = "";
    %delim = "";
    %n = $gScriptProfiler_TotalTimes.size() - 1;
    while (%n >= 0)
    {
        %time = formatFloat("%10.4f", $gScriptProfiler_TotalTimes.getValue(%n) / 1000);
        %id = $gScriptProfiler_TotalTimes.getKey(%n);
        %id2 = formatString("%-70s", %id);
        %bal = $gScriptProfiler_EntryTimes.get(%id) $= "" ? "(  balanced)" : "(unbalanced)";
        %cls = formatInt("%5d", $gScriptProfiler_TotalCalls.getValue(%n));
        %ret = %ret @ %delim @ %time SPC %bal SPC %cls SPC %id2;
        %delim = "\n";
        %n = %n - 1;
    }
    return %ret;
}
function scriptProfiler_dumpTotals()
{
    echo("\n" @ scriptProfiler_getTotals());
    return ;
}
function scriptProfiler_reset()
{
    scriptProfiler_Initialize();
    return ;
}
function scriptProfiler_activate()
{
    $gScriptProfiler_Active = 1;
    return ;
}
function scriptProfiler_deactivate()
{
    $gScriptProfiler_Active = 0;
    return ;
}
function scriptProfiler_foo()
{
    scriptProfiler_EnterScope();
    %n = 0;
    while (%n < 200000)
    {
        %m = %m + mSqrt(%n);
        %n = %n + 1;
    }
    scriptProfiler_LeaveScope();
    return ;
}
scriptProfiler_Initialize();

