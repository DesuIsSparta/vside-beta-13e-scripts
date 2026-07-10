function recordingsDlg::onWake() {
    RecordingsDlgList.clear();
    %i = 0;
    %filespec = $currentMod @ "/recordings/*.rec";
    echo(%filespec);
    %file = findFirstFile(%filespec);
    while (!(%file $= "")) {
        %fileName = fileBase(%file);
        if ((strstr(%file, "/CVS/") == -(1.0))) {
            RecordingsDlgList.addRow(%i = (%i + 1.0), %fileName);
        }
        %file = findNextFile(%filespec);
    }
    RecordingsDlgList.sort(0);
    RecordingsDlgList.setSelectedRow(0);
    RecordingsDlgList.scrollVisible(0);
};
function StartSelectedDemo() {
    %sel = RecordingsDlgList.getSelectedId();
    %rowText = RecordingsDlgList.getRowTextById(%sel);
    %file = $currentMod @ "/recordings/" @ getField(%rowText, 0) @ ".rec";
    new GameConnection(ServerConnection);
    ServerConnection.setCommonPreconnectClientSettings("");
    RootGroup.add(ServerConnection);
    if (ServerConnection.playDemo(%file)) {
        Canvas.setContent(PlayGui);
        Canvas.popDialog(recordingsDlg);
        ServerConnection.prepDemoPlayback();
    } else {
        MessageBoxOK("Playback Failed", "Demo playback failed for file '" @ %file @ "'.", "");
        if (isObject(ServerConnection)) {
            ServerConnection.delete();
        }
    }
};
function startDemoRecord() {
    ServerConnection.stopRecording();
    if (ServerConnection.isDemoPlaying()) {
        return;
    }
    %i = 0;
    if ((%i < 1000.0)) {
        %num = %i;
        if ((%num < 10.0)) {
            %num = 0 @ %num;
        }
        if ((%num < 100.0)) {
            %num = 0 @ %num;
        }
        %file = $currentMod @ "/recordings/demo" @ %num @ ".rec";
        if (!isFile(%file)) {
        } else {
            %i = (%i + 1.0);
        }
    }
    if ((%i == 1000.0)) {
        return (%i < 1000.0);
    }
    $DemoFileName = %file;
    ChatHud.addLine("\x05Recording to file [\x03" @ $DemoFileName @ "\x0F].");
    ServerConnection.prepDemoRecord();
    ServerConnection.startRecording($DemoFileName);
    if (!ServerConnection.isDemoRecording()) {
        deleteFile($DemoFileName);
        ChatHud.addLine("\x04 *** Failed to record to file [\x03" @ $DemoFileName @ "\x0F].");
        $DemoFileName = "";
    }
};
function stopDemoRecord() {
    if (ServerConnection.isDemoRecording()) {
        ChatHud.addLine("\x05Recording file [\x03" @ $DemoFileName @ "\x0F] finished.");
        ServerConnection.stopRecording();
    }
};
function demoPlaybackComplete() {
    disconnect();
    Canvas.setContent("MainMenuGui");
    Canvas.pushDialog(recordingsDlg, 0);
};
