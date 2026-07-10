function onMissionDownloadPhase1(%unused, %unused) {
    if (isObject(LoadingPBController)) {
        0.setValue(LoadingPBController);
        "Loading data".setValue(LoadingProgressTxt);
    }
    if (isObject(TransitionPBController)) {
        0.setValue(TransitionPBController);
    }
};
function onPhase1Progress(%progress) {
    if (isObject(LoadingPBController)) {
        (%progress * 0.33).setValue(LoadingPBController);
    }
    if (isObject(TransitionPBController)) {
        (%progress * 0.33).setValue(TransitionPBController);
    }
};
function onPhase1Complete() {
    if (isObject(LoadingPBController)) {
        0.33.setValue(LoadingPBController);
    }
};
function onMissionDownloadPhase2() {
    $Client::MissionLoadTimeStart = getSimTime();
    if (isObject(LoadingPBController)) {
        "Loading objects".setValue(LoadingProgressTxt);
    }
    if (isObject(TransitionPBController)) {
        "Loading objects".setValue(TransitionProgressTxt);
    }
};
function onPhase2ProgressUpdateStatusDisplay(%progress) {
    if (isObject(LoadingPBController)) {
        ((%progress * 0.33) + 0.33).setValue(LoadingPBController);
    }
    if (isObject(TransitionPBController)) {
        ((%progress * 0.33) + 0.33).setValue(TransitionPBController);
    }
};
function onPhase2Complete() {
};
function onFileChunkReceived(%fileName, %ofs, %size) {
    if (isObject(LoadingPBController)) {
        (%ofs / %size).setValue(LoadingPBController);
        "Downloading " @ %fileName @ "...".setValue(LoadingProgressTxt);
    }
    if (isObject(TransitionPBController)) {
        (%ofs / %size).setValue(TransitionPBController);
    }
};
function onMissionDownloadPhase3() {
    if (isObject(LoadingProgressTxt)) {
        "Lighting".setValue(LoadingProgressTxt);
    }
};
function onPhase3Progress(%progress) {
    if (isObject(LoadingPBController)) {
        ((%progress * 0.33) + 0.66).setValue(LoadingPBController);
    }
    if (isObject(TransitionPBController)) {
        ((%progress * 0.33) + 0.66).setValue(TransitionPBController);
    }
};
function onPhase3Complete() {
    if (isObject(LoadingPBController)) {
        1.setValue(LoadingPBController);
    }
    if (isObject(TransitionPBController)) {
        1.setValue(TransitionPBController);
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
    0.setVisible(TransitionMessage);
    if (WorldMap.isAwake()) {
    }
    if (LoginGui.isAwake()) {
    }
    if ($StandAlone) {
        0.setTransitioning(LoadingGui);
        "LoadingGui".setContent(Canvas);
        %line = 0;
        handleMsgDFZoneNameMessage;
        while ((%line < qLineCount)) {
            qLine = "" @ %line @ LoadingGui;
            LoadingGui;
            %line = (%line + 1.0);
            handleLoadInfoDoneMessage;
        }
        qLineCount = 0 @ LoadingGui;
        (%line < qLineCount);
    }
    if (PlayGui.isAwake()) {
        1.setTransitioning(LoadingGui);
        "LoadingGui".setContent(Canvas);
        $TransitionScreenshot.setScreenshotBitmap(LoadingGui);
    }
};
function handleLoadDescriptionMessage(%unused, %msgString) {
    qLine = LoadingGui @ qLineCount @ LoadingGui;
    %msgString;
    qLineCount = (qLineCount + LoadingGui);
    1.0;
    %text = "<spush><font:Arial:16>";
    %line = 0;
    while ((%line < (qLineCount - LoadingGui))) {
        %text = %line @ LoadingGui @ qLine @ " ";
        %text;
        %line = (%line + 1.0);
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
