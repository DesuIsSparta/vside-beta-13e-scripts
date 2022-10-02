function onMissionDownloadPhase1(%unused, %unused)
{
    if (isObject(LoadingPBController))
    {
        LoadingPBController.setValue(0);
        LoadingProgressTxt.setValue("Loading data");
    }
    if (isObject(TransitionPBController))
    {
        TransitionPBController.setValue(0);
    }
    return ;
}
function onPhase1Progress(%progress)
{
    if (isObject(LoadingPBController))
    {
        LoadingPBController.setValue(%progress * 0.33);
    }
    if (isObject(TransitionPBController))
    {
        TransitionPBController.setValue(%progress * 0.33);
    }
    return ;
}
function onPhase1Complete()
{
    if (isObject(LoadingPBController))
    {
        LoadingPBController.setValue(0.33);
    }
    return ;
}
function onMissionDownloadPhase2()
{
    $Client::MissionLoadTimeStart = getSimTime();
    if (isObject(LoadingPBController))
    {
        LoadingProgressTxt.setValue("Loading objects");
    }
    if (isObject(TransitionPBController))
    {
        TransitionProgressTxt.setValue("Loading objects");
    }
    return ;
}
function onPhase2ProgressUpdateStatusDisplay(%progress)
{
    if (isObject(LoadingPBController))
    {
        LoadingPBController.setValue((%progress * 0.33) + 0.33);
    }
    if (isObject(TransitionPBController))
    {
        TransitionPBController.setValue((%progress * 0.33) + 0.33);
    }
    return ;
}
function onPhase2Complete()
{
    return ;
}
function onFileChunkReceived(%fileName, %ofs, %size)
{
    if (isObject(LoadingPBController))
    {
        LoadingPBController.setValue(%ofs / %size);
        LoadingProgressTxt.setValue("Downloading " @ %fileName @ "...");
    }
    if (isObject(TransitionPBController))
    {
        TransitionPBController.setValue(%ofs / %size);
    }
    return ;
}
function onMissionDownloadPhase3()
{
    if (isObject(LoadingProgressTxt))
    {
        LoadingProgressTxt.setValue("Lighting");
    }
    return ;
}
function onPhase3Progress(%progress)
{
    if (isObject(LoadingPBController))
    {
        LoadingPBController.setValue((%progress * 0.33) + 0.66);
    }
    if (isObject(TransitionPBController))
    {
        TransitionPBController.setValue((%progress * 0.33) + 0.66);
    }
    return ;
}
function onPhase3Complete()
{
    if (isObject(LoadingPBController))
    {
        LoadingPBController.setValue(1);
    }
    if (isObject(TransitionPBController))
    {
        TransitionPBController.setValue(1);
    }
    $lightingMission = 0;
    return ;
}
function onMissionDownloadComplete()
{
    InitClientSittingSystem();
    if (isFunction("Using_DF") && Using_DF())
    {
        startDFZone();
    }
    setMissionLoaded(1);
    return ;
}
addMessageCallback('MsgLoadInfo', handleLoadInfoMessage);
addMessageCallback('MsgLoadDescripition', handleLoadDescriptionMessage);
addMessageCallback('MsgLoadInfoDone', handleLoadInfoDoneMessage);
if (isFunction("Using_DF") && Using_DF())
{
    addMessageCallback('MsgDFZoneName', handleMsgDFZoneNameMessage);
}
function handleLoadInfoMessage(%unused, %msgString)
{
    TransitionMessage.setVisible(0);
    if ((WorldMap.isAwake() || LoginGui.isAwake()) || $StandAlone)
    {
        LoadingGui.setTransitioning(0);
        Canvas.setContent("LoadingGui");
        %line = 0;
        while (%line < LoadingGui.qLineCount)
        {
            LoadingGui.qLine[%line] = "";
            %line = %line + 1;
        }
        LoadingGui.qLineCount = 0;
    }
    else
    {
        if (PlayGui.isAwake())
        {
            LoadingGui.setTransitioning(1);
            Canvas.setContent("LoadingGui");
            LoadingGui.setScreenshotBitmap($TransitionScreenshot);
        }
    }
    return ;
}
function handleLoadDescriptionMessage(%unused, %msgString)
{
    LoadingGui.qLine[LoadingGui.qLineCount] = %msgString;
    LoadingGui.qLineCount = LoadingGui.qLineCount + 1;
    %text = "<spush><font:Arial:16>";
    %line = 0;
    while (%line < (LoadingGui.qLineCount - 1))
    {
        %text = %text @ LoadingGui.qLine[%line] @ " ";
        %line = %line + 1;
    }
    %text = %text @ LoadingGui.qLine[%line] @ "<spop>";
    return ;
}
function handleLoadInfoDoneMessage(%unused, %msgString)
{
    return ;
}
function handleMsgDFZoneNameMessage(%unused, %msgString)
{
    if (!isFunction("Using_DF") && !Using_DF())
    {
        return ;
    }
    setDFZoneName(%msgString);
    return ;
}
