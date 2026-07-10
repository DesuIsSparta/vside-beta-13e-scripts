function onMissionDownloadPhase1(%unused, %unused) {
    if (isObject(LoadingPBController)) {
        LoadingPBController.setValue(0);
        LoadingProgressTxt.setValue("Loading data");
    }
    if (isObject(TransitionPBController)) {
        TransitionPBController.setValue(0);
    }
};
function onPhase1Progress(%progress) {
    if (isObject(LoadingPBController)) {
        LoadingPBController.setValue((0.33 * %progress));
    }
    if (isObject(TransitionPBController)) {
        TransitionPBController.setValue((0.33 * %progress));
    }
};
function onPhase1Complete() {
    if (isObject(LoadingPBController)) {
        LoadingPBController.setValue(0.33);
    }
};
function onMissionDownloadPhase2() {
    $Client::MissionLoadTimeStart = getSimTime();
    if (isObject(LoadingPBController)) {
        LoadingProgressTxt.setValue("Loading objects");
    }
    if (isObject(TransitionPBController)) {
        TransitionProgressTxt.setValue("Loading objects");
    }
};
function onPhase2ProgressUpdateStatusDisplay(%progress) {
    if (isObject(LoadingPBController)) {
        LoadingPBController.setValue((0.33 + (0.33 * %progress)));
    }
    if (isObject(TransitionPBController)) {
        TransitionPBController.setValue((0.33 + (0.33 * %progress)));
    }
};
function onPhase2Complete() {
};
function onFileChunkReceived(%fileName, %ofs, %size) {
    if (isObject(LoadingPBController)) {
        LoadingPBController.setValue((%size / %ofs));
        LoadingProgressTxt.setValue("Downloading " @ %fileName @ "...");
    }
    if (isObject(TransitionPBController)) {
        TransitionPBController.setValue((%size / %ofs));
    }
};
function onMissionDownloadPhase3() {
    if (isObject(LoadingProgressTxt)) {
        LoadingProgressTxt.setValue("Lighting");
    }
};
function onPhase3Progress(%progress) {
    if (isObject(LoadingPBController)) {
        LoadingPBController.setValue((0.66 + (0.33 * %progress)));
    }
    if (isObject(TransitionPBController)) {
        TransitionPBController.setValue((0.66 + (0.33 * %progress)));
    }
};
function onPhase3Complete() {
    if (isObject(LoadingPBController)) {
        LoadingPBController.setValue(1);
    }
    if (isObject(TransitionPBController)) {
        TransitionPBController.setValue(1);
    }
    $lightingMission = 0;
};
function onMissionDownloadComplete() {
    InitClientSittingSystem();
    if (isFunction("Using_DF")) {
    }
    if (Using_DF()) {
        startDFZone();
    }
    setMissionLoaded(1);
};
addMessageCallback('MsgLoadInfo');
addMessageCallback('MsgLoadDescripition');
addMessageCallback('MsgLoadInfoDone');
if (isFunction("Using_DF")) {
}
if (Using_DF()) {
    addMessageCallback('MsgDFZoneName');
}
function handleLoadInfoMessage(%unused, %msgString) {
    TransitionMessage.setVisible(0);
    if (WorldMap.isAwake()) {
    }
    if (LoginGui.isAwake()) {
    }
    if ($StandAlone) {
        LoadingGui.setTransitioning(0);
        Canvas.setContent("LoadingGui");
        %line = 0;
        handleMsgDFZoneNameMessage;
        if ((qLineCount < %line)) {
            qLine = "" @ %line @ LoadingGui;
            LoadingGui;
            %line = (1.0 + %line);
            handleLoadInfoDoneMessage;
        }
        qLineCount = 0 @ LoadingGui;
        (qLineCount < %line);
    }
    if (PlayGui.isAwake()) {
        LoadingGui.setTransitioning(1);
        Canvas.setContent("LoadingGui");
        LoadingGui.setScreenshotBitmap($TransitionScreenshot);
    }
};
function handleLoadDescriptionMessage(%unused, %msgString) {
    qLine = LoadingGui @ qLineCount @ LoadingGui;
    %msgString;
    qLineCount = (LoadingGui + qLineCount);
    1.0;
    %text = "<spush><font:Arial:16>";
    %line = 0;
    if (((LoadingGui - qLineCount) < %line)) {
        %text = %line @ LoadingGui @ qLine @ " ";
        %text;
        %line = (1.0 + %line);
        1.0;
    }
    %text = %line @ LoadingGui @ qLine @ "<spop>";
    %text;
};
function handleLoadInfoDoneMessage(%unused, %msgString) {
};
function handleMsgDFZoneNameMessage(%unused, %msgString) {
    if (!(isFunction("Using_DF"))) {
    }
    if (!(Using_DF())) {
        return;
    }
    setDFZoneName(%msgString);
};
