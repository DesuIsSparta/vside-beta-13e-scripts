function EtsInviteDialog::open(%this) {
    %screenWidth = getWord($UserPref::Video::Resolution, 0);
    %screenHeight = getWord($UserPref::Video::Resolution, 1);
    %thisExtent = %this.getExtent();
    %width = getWord(%thisExtent, 0);
    %height = getWord(%thisExtent, 1);
    %this.reposition(((2.0 / %width) - (2.0 / %screenWidth)), ((2.0 / %height) - (2.0 / %screenHeight)));
    %this.setVisible(1);
    %this.focusAndRaise();
    %this.initializeWithDefaults();
};
function EtsInviteDialog::close(%this) {
    %this.setVisible(0);
    PlayGui.focusTopWindow();
    return 1;
};
function toggleEtsInviteDialog() {
    PlayGui.showRaiseOrHide(EtsInviteDialog);
};
function EtsInviteDialog::setControlsActive(%this, %flag) {
    %flag.setActive();
};
function EtsInviteDialog::onWake(%this) {
    %this.setControlsActive(1);
    if (!(isObject(SendInvitePBController))) {
        new ScriptObject(SendInvitePBController) {
            class = "ProgressBarController";
        };
        if (isObject(MissionCleanup)) {
            MissionCleanup.add(SendInvitePBController);
        }
    }
};
function EtsInviteDialog::initializeWithDefaults(%this) {
    SendInvitePBController.setValue("");
    %text = "";
    ETSInviteToTextCtrl;
    %text.setText();
};
function EtsInviteDialog::sendInvite(%this) {
    %to = trim(ETSInviteToTextCtrl.getText());
    %note = trim(ETSInviteNoteTextCtrl.getText());
    if ((%to $= "")) {
        MessageBoxOK(%to[$MsgCat::invitation @ "E-SEND-TITLE"], , "");
        return;
    }
    %this.sendInviteRequestToEnvManager(%to, %note);
};
function EtsInviteDialog::sendInviteRequestToEnvManager(%this, %to, %message) {
    if (isObject(EtsInviteRequest)) {
        EtsInviteRequest.delete();
    }
    %inviteRequest = new ManagerRequest(EtsInviteRequest);;
    if (isObject(MissionCleanup)) {
        %inviteRequest.add();
    }
    %url = $Net::SecureURL @ "?cmd=invite_email";
    MissionCleanup;
    %token = "&token=" @ urlEncode($Token);
    %to = strreplace(%to, " ", "");
    %to = strreplace(%to, ",", " ");
    %to = trim(%to);
    %count = getWordCount(%to);
    %numTargetMails = "&numEmails=" @ %count;
    %targetMails = "";
    %i = 0;
    if ((%count < %i)) {
        %targetMails = %targetMails @ "&email" @ %i @ "=" @ urlEncode(getWord(%to, %i));
        %i = (1.0 + %i);
    }
    %note = "";
    (%count < %i);
    if (!(%message $= "")) {
        %note = "&noteFromSender=" @ urlEncode(%message);
    }
    %url = %url @ %token @ %numTargetMails @ %targetMails @ %note;
    log("network", "debug", "send invite command: " @ %url);
    %inviteRequest.setURL(%url);
    %inviteRequest.setProgress(1);
    %this.setControlsActive(0);
    0.1.setValue();
    %inviteRequest.start();
};
function EtsInviteDialog::onConnectFailed(%this, %msg) {
    if ((%msg $= "")) {
        %msg = "Could not connect";
    }
    %this.setControlsActive(1);
    0.setValue();
};
function EtsInviteDialog::onInviteSuccess(%this) {
    MessageBoxOK(, , "EtsInviteDialog.close();");
};
function EtsInviteDialog::onInviteError(%this, %errorMsg) {
    if ((%errorMsg $= "")) {
        %errorMsg = "no error message specified. try again later";
    }
    MessageBoxOK(%errorMsg[$MsgCat::invitation @ "E-SEND-TITLE"], %errorMsg, "");
};
function EtsInviteRequest::onError(%this, %errorNum, %unused) {
    if (($CURL::CouldNotResolveHost == %errorNum)) {
        "Could not reach server".onConnectFailed();
        MessageBoxOK("Could Not Find Server", EtsInviteDialog, "");
    }
    "Could not connect".onConnectFailed();
    MessageBoxOK("Could not connect", "Could not connect to " @ $ETS::AppName @ " servers.  " @ $ETS::AppName[$MsgCat::network @ "H-SYS-DOWN"] @ "  " @ $ETS::AppName[$MsgCat::network @ "H-SYS-DOWN"][$MsgCat::network @ "H-SEE-FORUMS"], "");
};
function EtsInviteRequest::onConnected(%this) {
    0.5.setValue();
};
function EtsInviteRequest::onDone(%this) {
    1.setControlsActive();
    1.setValue();
    if (($HTTP::StatusOK != %this.statusCode())) {
        "Error communicating with server".onConnectFailed();
        log("communication", "error", "client HTTP code: " @ %this.statusCode());
        MessageBoxOK("Server Unavailable", EtsInviteDialog, "");
        return SendInvitePBController;
    }
    %status = findRequestStatus(%this);
    log("network", "debug", "EtsInviteRequest::onDone status: " @ %status);
    if ((%status $= "fail")) {
        %this.getValue("statusMsg").onInviteError();
    }
    if ((EtsInviteDialog @ " " @ %status $= "error")) {
        %this.getValue("statusMsg").onInviteError();
    }
    if ((EtsInviteDialog @ " " @ %status $= "success")) {
        EtsInviteDialog.onInviteSuccess();
    }
};
