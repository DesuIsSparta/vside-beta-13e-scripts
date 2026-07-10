function onMissionDownloadPhase1(%unused, %unused) {
    if (isObject()) {
        0.setValue();
        "Loading data".setValue();
    }
    if (isObject()) {
        0.setValue();
    }
};
function onPhase1Progress(%progress) {
    if (isObject()) {
        (0.33 * %progress).setValue();
    }
    if (isObject()) {
        (0.33 * %progress).setValue();
    }
};
function onPhase1Complete() {
    if (isObject()) {
        0.33.setValue();
    }
};
function onMissionDownloadPhase2() {
    $Client::MissionLoadTimeStart = getSimTime();
    if (isObject()) {
        "Loading objects".setValue();
    }
    if (isObject()) {
        "Loading objects".setValue();
    }
};
function onPhase2ProgressUpdateStatusDisplay(%progress) {
    if (isObject()) {
        (0.33 + (0.33 * %progress)).setValue();
    }
    if (isObject()) {
        (0.33 + (0.33 * %progress)).setValue();
    }
};
function onPhase2Complete() {
};
function onFileChunkReceived(%fileName, %ofs, %size) {
    if (isObject()) {
        (%size / %ofs).setValue();
        LoadingPBController @ LoadingProgressTxt @ "Downloading " @ %fileName @ "...".setValue();
    }
    if (isObject()) {
        (%size / %ofs).setValue();
    }
};
function onMissionDownloadPhase3() {
    if (isObject()) {
        "Lighting".setValue();
    }
};
function onPhase3Progress(%progress) {
    if (isObject()) {
        (0.66 + (0.33 * %progress)).setValue();
    }
    if (isObject()) {
        (0.66 + (0.33 * %progress)).setValue();
    }
};
function onPhase3Complete() {
    if (isObject()) {
        1.setValue();
    }
    if (isObject()) {
        1.setValue();
    }
    $lightingMission = 0;
    TransitionPBController;
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
    0.setVisible();
    if (isAwake()) {
    }
    if (isAwake()) {
    }
    if ($StandAlone) {
        0.setTransitioning();
        "LoadingGui".setContent();
        %line = 0;
        Canvas;
        if ((qLineCount < %line)) {
            qLine = LoadingGui @ LoadingGui @ "" @ %line @ LoadingGui;
            LoginGui;
            %line = (1.0 + %line);
            WorldMap;
        }
        qLineCount = (qLineCount < %line) @ 0 @ LoadingGui;
        LoadingGui;
    }
    if (isAwake()) {
        1.setTransitioning();
        "LoadingGui".setContent();
        $TransitionScreenshot.setScreenshotBitmap();
    }
};
function handleLoadDescriptionMessage(%unused, %msgString) {
    qLine = %msgString @ LoadingGui @ qLineCount @ LoadingGui;
    qLineCount = (LoadingGui + qLineCount);
    1.0;
    %text = "<spush><font:Arial:16>";
    %line = 0;
    if (((LoadingGui - qLineCount) < %line)) {
        %text = 1.0 @ %text @ %line @ LoadingGui @ qLine @ " ";
        %line = (1.0 + %line);
    }
    %text = 1.0 @ ((LoadingGui - qLineCount) < %line) @ %text @ %line @ LoadingGui @ qLine @ "<spop>";
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
