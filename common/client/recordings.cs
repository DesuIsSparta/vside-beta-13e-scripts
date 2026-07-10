function recordingsDlg::onWake() {
    RecordingsDlgList.clear();
    %i = 0;
    %filespec = $currentMod @ "/recordings/*.rec";
    echo(%filespec);
    %file = findFirstFile(%filespec);
    while (!(%file $= "")) {
        %fileName = fileBase(%file);
        if ((strstr(%file, "/CVS/") == -(1.0))) {
            %fileName.addRow(RecordingsDlgList, %i = (%i + 1.0));
        }
        %file = findNextFile(%filespec);
    }
    0.sort(RecordingsDlgList);
    0.setSelectedRow(RecordingsDlgList);
    0.scrollVisible(RecordingsDlgList);
};
function StartSelectedDemo() {
    %sel = RecordingsDlgList.getSelectedId();
    %rowText = %sel.getRowTextById(RecordingsDlgList);
    %file = $currentMod @ "/recordings/" @ getField(%rowText, 0) @ ".rec";
    new GameConnection(ServerConnection);
    "".setCommonPreconnectClientSettings(ServerConnection);
    ServerConnection.add(RootGroup);
    if (%file.playDemo(ServerConnection)) {
        PlayGui.setContent(Canvas);
        recordingsDlg.popDialog(Canvas);
        ServerConnection.prepDemoPlayback();
    }
    MessageBoxOK("Playback Failed", "Demo playback failed for file '" @ %file @ "'.", "");
    if (isObject(ServerConnection)) {
        ServerConnection.delete();
    }
};
function startDemoRecord() {
    ServerConnection.stopRecording();
    if (ServerConnection.isDemoPlaying()) {
        return;
    }
    %i = 0;
    while ((%i < 1000.0)) {
        %num = %i;
        if ((%num < 10.0)) {
            %num = 0 @ %num;
        }
        if ((%num < 100.0)) {
            %num = 0 @ %num;
        }
        %file = $currentMod @ "/recordings/demo" @ %num @ ".rec";
        if (!(isFile(%file))) {
        }
        %i = (%i + 1.0);
    }
    if ((%i == 1000.0)) {
        return (%i < 1000.0);
    }
    $DemoFileName = %file;
    "\x05Recording to file [\x03" @ $DemoFileName @ "\x0F].".addLine(ChatHud);
    ServerConnection.prepDemoRecord();
    $DemoFileName.startRecording(ServerConnection);
    if (!(ServerConnection.isDemoRecording())) {
        deleteFile($DemoFileName);
        "\x04 *** Failed to record to file [\x03" @ $DemoFileName @ "\x0F].".addLine(ChatHud);
        $DemoFileName = "";
    }
};
function stopDemoRecord() {
    if (ServerConnection.isDemoRecording()) {
        "\x05Recording file [\x03" @ $DemoFileName @ "\x0F] finished.".addLine(ChatHud);
        ServerConnection.stopRecording();
    }
};
function demoPlaybackComplete() {
    disconnect();
    "MainMenuGui".setContent(Canvas);
    0.pushDialog(Canvas, recordingsDlg);
};
