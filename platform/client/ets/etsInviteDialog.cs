function EtsInviteDialog::open(%this) {
    %screenWidth = getWord($UserPref::Video::Resolution, 0);
    %screenHeight = getWord($UserPref::Video::Resolution, 1);
    %thisExtent = %this.getExtent();
    %width = getWord(%thisExtent, 0);
    %height = getWord(%thisExtent, 1);
    ((%screenHeight / 2.0) - (%height / 2.0)).reposition(%this, ((%screenWidth / 2.0) - (%width / 2.0)));
    1.setVisible(%this);
    %this.focusAndRaise(PlayGui);
    %this.initializeWithDefaults();
};
function EtsInviteDialog::close(%this) {
    0.setVisible(%this);
    PlayGui.focusTopWindow();
    return 1;
};
function toggleEtsInviteDialog() {
    EtsInviteDialog.showRaiseOrHide(PlayGui);
};
function EtsInviteDialog::setControlsActive(%this, %flag) {
    %flag.setActive(InviteDialogButtonSend);
};
function EtsInviteDialog::onWake(%this) {
    1.setControlsActive(%this);
    if (!(isObject(SendInvitePBController))) {
        new ScriptObject(SendInvitePBController) {
            class = "ProgressBarController";
        };
        if (isObject(MissionCleanup)) {
            SendInvitePBController.add(MissionCleanup);
        }
    }
};
function EtsInviteDialog::initializeWithDefaults(%this) {
    "".setValue(ETSInviteToTextCtrl);
    %text = "";
    %text.setText(ETSInviteNoteTextCtrl);
};
function EtsInviteDialog::sendInvite(%this) {
    %to = trim(ETSInviteToTextCtrl.getText());
    %note = trim(ETSInviteNoteTextCtrl.getText());
    if ((%to $= "")) {
        MessageBoxOK(%to[$MsgCat::invitation @ "E-SEND-TITLE"], $MsgCat::invitation["EMPTY-TO-FIELD"], "");
        return;
    }
    %note.sendInviteRequestToEnvManager(%this, %to);
};
function EtsInviteDialog::sendInviteRequestToEnvManager(%this, %to, %message) {
    if (isObject(EtsInviteRequest)) {
        EtsInviteRequest.delete();
    }
    %inviteRequest = new ManagerRequest(EtsInviteRequest);
    if (isObject(MissionCleanup)) {
        %inviteRequest.add(MissionCleanup);
    }
    %url = $Net::SecureURL @ "?cmd=invite_email";
    %token = "&token=" @ urlEncode($Token);
    %to = strreplace(%to, " ", "");
    %to = strreplace(%to, ",", " ");
    %to = trim(%to);
    %count = getWordCount(%to);
    %numTargetMails = "&numEmails=" @ %count;
    %targetMails = "";
    %i = 0;
    while ((%i < %count)) {
        %targetMails = %targetMails @ "&email" @ %i @ "=" @ urlEncode(getWord(%to, %i));
        %i = (%i + 1.0);
    }
    %note = "";
    (%i < %count);
    if (!(%message $= "")) {
        %note = "&noteFromSender=" @ urlEncode(%message);
    }
    %url = %url @ %token @ %numTargetMails @ %targetMails @ %note;
    log("network", "debug", "send invite command: " @ %url);
    %url.setURL(%inviteRequest);
    1.setProgress(%inviteRequest);
    0.setControlsActive(%this);
    0.1.setValue(SendInvitePBController);
    %inviteRequest.start();
};
function EtsInviteDialog::onConnectFailed(%this, %msg) {
    if ((%msg $= "")) {
        %msg = "Could not connect";
    }
    1.setControlsActive(%this);
    0.setValue(SendInvitePBController);
};
function EtsInviteDialog::onInviteSuccess(%this) {
    MessageBoxOK($MsgCat::invitation["S-SEND-TITLE"], $MsgCat::invitation["INVITE-SENT"], "EtsInviteDialog.close();");
};
function EtsInviteDialog::onInviteError(%this, %errorMsg) {
    if ((%errorMsg $= "")) {
        %errorMsg = "no error message specified. try again later";
    }
    MessageBoxOK($MsgCat::invitation["E-SEND-TITLE"], %errorMsg, "");
};
function EtsInviteRequest::onError(%this, %errorNum, %unused) {
    if ((%errorNum == $CURL::CouldNotResolveHost)) {
        "Could not reach server".onConnectFailed(EtsInviteDialog);
        MessageBoxOK("Could Not Find Server", $MsgCat::network["E-SERVER-DNS"], "");
    }
    "Could not connect".onConnectFailed(EtsInviteDialog);
    MessageBoxOK("Could not connect", "Could not connect to " @ $ETS::AppName @ " servers.  " @ $ETS::AppName[$MsgCat::network @ "H-SYS-DOWN"] @ "  " @ $ETS::AppName[$MsgCat::network @ "H-SYS-DOWN"][$MsgCat::network @ "H-SEE-FORUMS"], "");
};
function EtsInviteRequest::onConnected(%this) {
    0.5.setValue(SendInvitePBController);
};
function EtsInviteRequest::onDone(%this) {
    1.setControlsActive(EtsInviteDialog);
    1.setValue(SendInvitePBController);
    if ((%this.statusCode() != $HTTP::StatusOK)) {
        "Error communicating with server".onConnectFailed(EtsInviteDialog);
        log("communication", "error", "client HTTP code: " @ %this.statusCode());
        MessageBoxOK("Server Unavailable", $MsgCat::network["E-SERVER-UNAVAIL"], "");
        return;
    }
    %status = findRequestStatus(%this);
    log("network", "debug", "EtsInviteRequest::onDone status: " @ %status);
    if ((%status $= "fail")) {
        "statusMsg".getValue(%this).onInviteError(EtsInviteDialog);
    }
    if ((%status $= "error")) {
        "statusMsg".getValue(%this).onInviteError(EtsInviteDialog);
    }
    if ((%status $= "success")) {
        EtsInviteDialog.onInviteSuccess();
    }
};
