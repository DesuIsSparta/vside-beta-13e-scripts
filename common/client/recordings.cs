function recordingsDlg::onWake() {
    RecordingsDlgList.clear();
    %i = 0;
    %filespec = $currentMod @ "/recordings/*.rec";
    echo(%filespec);
    %file = findFirstFile(%filespec);
    if (!(%file $= "")) {
        %fileName = fileBase(%file);
        if ((-(1.0) == strstr(%file, "/CVS/"))) {
            %i = (1.0 + %i);
            RecordingsDlgList.addRow(%fileName);
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
        ServerConnection.prepDemoPlayback(ServerConnection);
    }
    MessageBoxOK("Playback Failed", "Demo playback failed for file '" @ %file @ "'.", "");
    if (isObject(ServerConnection)) {
        ServerConnection.delete(ServerConnection);
    }
};
function startDemoRecord() {
    ServerConnection.stopRecording(ServerConnection);
    if (ServerConnection.isDemoPlaying(ServerConnection)) {
        return;
    }
    %i = 0;
    if ((1000.0 < %i)) {
        %num = %i;
        if ((10.0 < %num)) {
            %num = 0 @ %num;
        }
        if ((100.0 < %num)) {
            %num = 0 @ %num;
        }
        %file = $currentMod @ "/recordings/demo" @ %num @ ".rec";
        if (!(isFile(%file))) {
        }
        %i = (1.0 + %i);
    }
    if ((1000.0 == %i)) {
        return (1000.0 < %i);
    }
    $DemoFileName = %file;
    ChatHud.addLine("\x05Recording to file [\x03" @ $DemoFileName @ "\x0F].");
    ServerConnection.prepDemoRecord();
    ServerConnection.startRecording($DemoFileName);
    if (!(ServerConnection.isDemoRecording())) {
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
