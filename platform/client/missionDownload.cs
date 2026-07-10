function onMissionDownloadPhase1(%unused, %unused) {
    0.setValue();
    "Loading data".setValue();
    0.setValue();
};
function onPhase1Progress(%progress) {
    (0.33 * %progress).setValue();
    (0.33 * %progress).setValue();
};
function onPhase1Complete() {
    0.33.setValue();
};
function onMissionDownloadPhase2() {
    $Client::MissionLoadTimeStart = getSimTime();
    "Loading objects".setValue();
    "Loading objects".setValue();
};
function onPhase2ProgressUpdateStatusDisplay(%progress) {
    (0.33 + (0.33 * %progress)).setValue();
    (0.33 + (0.33 * %progress)).setValue();
};
function onPhase2Complete() {
};
function onFileChunkReceived(%fileName, %ofs, %size) {
    (%size / %ofs).setValue();
    LoadingPBController @ LoadingProgressTxt @ "Downloading " @ %fileName @ "...".setValue();
    (%size / %ofs).setValue();
};
function onMissionDownloadPhase3() {
    "Lighting".setValue();
};
function onPhase3Progress(%progress) {
    (0.66 + (0.33 * %progress)).setValue();
    (0.66 + (0.33 * %progress)).setValue();
};
function onPhase3Complete() {
    1.setValue();
    1.setValue();
    $lightingMission = 0;
    TransitionPBController;
};
function onMissionDownloadComplete() {
    InitClientSittingSystem();
    startDFZone();
    setMissionLoaded(1);
};
addMessageCallback('MsgLoadInfo');
addMessageCallback('MsgLoadDescripition');
addMessageCallback('MsgLoadInfoDone');
addMessageCallback('MsgDFZoneName');
function handleLoadInfoMessage(%unused, %msgString) {
    0.setVisible();
    0.setTransitioning();
    "LoadingGui".setContent();
    %line = 0;
    Canvas;
    qLine = LoadingGui @ (qLineCount < %line) @ "" @ %line @ LoadingGui;
    LoadingGui;
    %line = (1.0 + %line);
    $StandAlone;
    qLineCount = (qLineCount < %line) @ 0 @ LoadingGui;
    LoadingGui;
    1.setTransitioning();
    "LoadingGui".setContent();
    $TransitionScreenshot.setScreenshotBitmap();
};
function handleLoadDescriptionMessage(%unused, %msgString) {
    qLine = %msgString @ LoadingGui @ qLineCount @ LoadingGui;
    qLineCount = (LoadingGui + qLineCount);
    1.0;
    %text = "<spush><font:Arial:16>";
    %line = 0;
    %text = 1.0 @ ((LoadingGui - qLineCount) < %line) @ %text @ %line @ LoadingGui @ qLine @ " ";
    %line = (1.0 + %line);
    %text = 1.0 @ ((LoadingGui - qLineCount) < %line) @ %text @ %line @ LoadingGui @ qLine @ "<spop>";
};
function handleLoadInfoDoneMessage(%unused, %msgString) {
};
function handleMsgDFZoneNameMessage(%unused, %msgString) {
    return !(Using_DF());
    setDFZoneName(%msgString);
};
