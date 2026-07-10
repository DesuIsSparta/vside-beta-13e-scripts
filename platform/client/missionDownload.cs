function onMissionDownloadPhase1(%unused, %unused) {
    if (isObject(LoadingPBController)) {
        0.setValue();
        "Loading data".setValue();
    }
    if (isObject(TransitionPBController)) {
        0.setValue();
    }
};
function onPhase1Progress(%progress) {
    if (isObject(LoadingPBController)) {
        (0.33 * %progress).setValue();
    }
    if (isObject(TransitionPBController)) {
        (0.33 * %progress).setValue();
    }
};
function onPhase1Complete() {
    if (isObject(LoadingPBController)) {
        0.33.setValue();
    }
};
function onMissionDownloadPhase2() {
    $Client::MissionLoadTimeStart = getSimTime();
    if (isObject(LoadingPBController)) {
        "Loading objects".setValue();
    }
    if (isObject(TransitionPBController)) {
        "Loading objects".setValue();
    }
};
function onPhase2ProgressUpdateStatusDisplay(%progress) {
    if (isObject(LoadingPBController)) {
        (0.33 + (0.33 * %progress)).setValue();
    }
    if (isObject(TransitionPBController)) {
        (0.33 + (0.33 * %progress)).setValue();
    }
};
function onPhase2Complete() {
};
function onFileChunkReceived(%fileName, %ofs, %size) {
    if (isObject(LoadingPBController)) {
        (%size / %ofs).setValue();
        "Downloading " @ %fileName @ "...".setValue();
    }
    if (isObject(TransitionPBController)) {
        (%size / %ofs).setValue();
    }
};
function onMissionDownloadPhase3() {
    if (isObject(LoadingProgressTxt)) {
        "Lighting".setValue();
    }
};
function onPhase3Progress(%progress) {
    if (isObject(LoadingPBController)) {
        (0.66 + (0.33 * %progress)).setValue();
    }
    if (isObject(TransitionPBController)) {
        (0.66 + (0.33 * %progress)).setValue();
    }
};
function onPhase3Complete() {
    if (isObject(LoadingPBController)) {
        1.setValue();
    }
    if (isObject(TransitionPBController)) {
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
    if (WorldMap.isAwake()) {
    }
    if (LoginGui.isAwake()) {
    }
    if ($StandAlone) {
        0.setTransitioning();
        "LoadingGui".setContent();
        %line = 0;
        Canvas;
        if ((qLineCount < %line)) {
            qLine = "" @ %line @ LoadingGui;
            LoadingGui;
            %line = (1.0 + %line);
            LoadingGui;
        }
        qLineCount = 0 @ LoadingGui;
        (qLineCount < %line);
    }
    if (PlayGui.isAwake()) {
        1.setTransitioning();
        "LoadingGui".setContent();
        $TransitionScreenshot.setScreenshotBitmap();
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
