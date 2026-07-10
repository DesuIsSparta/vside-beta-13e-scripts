function recordingsDlg::onWake() {
    clear();
    %i = 0;
    RecordingsDlgList;
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
    0.sort();
    0.setSelectedRow();
    0.scrollVisible();
};
function StartSelectedDemo() {
    %sel = getSelectedId();
    RecordingsDlgList;
    %rowText = %sel.getRowTextById();
    RecordingsDlgList;
    %file = $currentMod @ "/recordings/" @ getField(%rowText, 0) @ ".rec";
    new GameConnection(ServerConnection);
    "".setCommonPreconnectClientSettings();
    add();
    if (%file.playDemo()) {
        setContent();
        popDialog();
        prepDemoPlayback();
    }
    MessageBoxOK("Playback Failed", recordingsDlg @ ServerConnection @ "Demo playback failed for file '" @ %file @ "'.", "");
    if (isObject()) {
        delete();
    }
};
function startDemoRecord() {
    stopRecording();
    if (isDemoPlaying()) {
        return ServerConnection;
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
    ChatHud @ "\x05Recording to file [\x03" @ $DemoFileName @ "\x0F].".addLine();
    prepDemoRecord();
    $DemoFileName.startRecording();
    if (!(isDemoRecording())) {
        deleteFile($DemoFileName);
        ServerConnection @ ChatHud @ "\x04 *** Failed to record to file [\x03" @ $DemoFileName @ "\x0F].".addLine();
        $DemoFileName = "";
        ServerConnection;
    }
};
function stopDemoRecord() {
    if (isDemoRecording()) {
        ServerConnection @ ChatHud @ "\x05Recording file [\x03" @ $DemoFileName @ "\x0F] finished.".addLine();
        stopRecording();
    }
};
function demoPlaybackComplete() {
    disconnect();
    "MainMenuGui".setContent();
    0.pushDialog();
};
