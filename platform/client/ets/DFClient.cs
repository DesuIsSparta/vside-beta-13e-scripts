function setDFEnabled(%val)
{
    if (!isFunction("Using_DF") && !Using_DF())
    {
        return ;
    }
    if (%val)
    {
        DFManagerInit();
    }
    else
    {
        DFManagerDestroy();
    }
    return ;
}
function clientCmdSetDFEnabled(%val)
{
    if (!isFunction("Using_DF") && !Using_DF())
    {
        return ;
    }
    setDFEnabled(%val);
    return ;
}
$gDFNotify = 0;
$gDFNotifyCode = "";
function onDFEngineStartError(%errorCode)
{
    if (isObject(ServerConnection))
    {
        commandToServer('DFStart', 0, %errorCode);
    }
    else
    {
        $gDFNotify = 1;
        $gDFNotifyCode = %errorCode;
    }
    return ;
}
function onDFEngineStarted()
{
    commandToServer('DFStart', 1, "");
    $gDFNotify = 0;
    $gDFNotifyCode = "";
    return ;
}
$gDFDebugNeedsRefresh = 1;
$gDFDebugAdvertsList = new_ScriptArray("");
$gDFDebugCurrAdvert = "-";
function DFDebugRefresh()
{
    if (!$gDFDebugNeedsRefresh)
    {
        return ;
    }
    $gDFDebugNeedsRefresh = 0;
    $gDFDebugAdvertsList.clear();
    %num = ServerConnection.getCount();
    %n = 0;
    while (%n < %num)
    {
        %obj = ServerConnection.getObject(%n);
        if (%obj.getClassName() $= "DFTextureAdvert")
        {
            $gDFDebugAdvertsList.append(%obj);
        }
        %n = %n + 1;
    }
    $gDFDebugCurrAdvert = "-";
    DFDebugUpdateGuiStatus();
    return ;
}
function DFDebugUpdateGuiStatus()
{
    %obj = ($gDFDebugCurrAdvert < 1) && ($gDFDebugAdvertsList.size() > 0) ? "" : $gDFDebugAdvertsList.get($gDFDebugCurrAdvert - 1);
    %objText = "";
    if (isObject(%obj))
    {
        %objText = %objText @ "-" SPC %obj.getDFObjectName();
        if (!(%obj.getName() $= ""))
        {
            %objText = %objText @ "-" SPC %obj.getName();
        }
    }
    geDFDebugStatusText.setValue($gDFDebugCurrAdvert SPC "/" SPC $gDFDebugAdvertsList.size() SPC %objText);
    return ;
}
function DFDebugRefreshForce()
{
    $gDFDebugNeedsRefresh = 1;
    DFDebugRefresh();
    return ;
}
function DFDebugPrev()
{
    if (!$Pref::DF::debugMode)
    {
        return ;
    }
    DFDebugRefresh();
    DFDebugGotoAdvert($gDFDebugCurrAdvert - 1);
    return ;
}
function DFDebugNext()
{
    if (!$Pref::DF::debugMode)
    {
        return ;
    }
    DFDebugRefresh();
    DFDebugGotoAdvert($gDFDebugCurrAdvert + 1);
    return ;
}
function DFDebugGotoAdvert(%advertNumber)
{
    %advertObj = "";
    if (%advertNumber > $gDFDebugAdvertsList.size())
    {
        %advertNumber = 1;
    }
    if (%advertNumber < 1)
    {
        %advertNumber = $gDFDebugAdvertsList.size();
    }
    if (%advertNumber < 1)
    {
        %advertNumber = "-";
    }
    else
    {
        %advertObj = $gDFDebugAdvertsList.get(%advertNumber - 1);
    }
    $gDFDebugCurrAdvert = %advertNumber;
    DFDebugUpdateGuiStatus();
    if (isObject(%advertObj))
    {
        %trans = %advertObj.localToWorldTransform("0 0 0 0 0 1 -3.14159");
        %offset = %advertObj.localToWorldVector("0 5 0");
        %point = %advertObj.getWorldBoxCenter();
        %point = VectorAdd(%offset, %point);
        %trans = %point SPC getWords(%trans, 3, 100);
        commandToServer('DropCameraAtTransform', %trans);
    }
    return ;
}
