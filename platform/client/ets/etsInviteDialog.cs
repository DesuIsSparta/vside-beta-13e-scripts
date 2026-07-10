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
    focusTopWindow();
    return 1;
};
function toggleEtsInviteDialog() {
    showRaiseOrHide();
};
function EtsInviteDialog::setControlsActive(%this, %flag) {
    %flag.setActive();
};
function EtsInviteDialog::onWake(%this) {
    %this.setControlsActive(1);
    if (!(isObject())) {
        class = SendInvitePBController @ new ScriptObject(SendInvitePBController) @ "ProgressBarController";
        if (isObject()) {
            add();
        }
    }
};
function EtsInviteDialog::initializeWithDefaults(%this) {
    "".setValue();
    %text = "";
    ETSInviteToTextCtrl;
    %text.setText();
};
function EtsInviteDialog::sendInvite(%this) {
    %to = trim(getText());
    ETSInviteToTextCtrl;
    %note = trim(getText());
    ETSInviteNoteTextCtrl;
    if ((%to $= "")) {
        MessageBoxOK(%to[$MsgCat::invitation @ "E-SEND-TITLE"], , "");
        return;
    }
    %this.sendInviteRequestToEnvManager(%to, %note);
};
function EtsInviteDialog::sendInviteRequestToEnvManager(%this, %to, %message) {
    if (isObject()) {
        delete();
    }
    %inviteRequest = new ManagerRequest(EtsInviteRequest);
    EtsInviteRequest;
    if (isObject()) {
        %inviteRequest.add();
    }
    %url = MissionCleanup @ $Net::SecureURL @ "?cmd=invite_email";
    MissionCleanup;
    %token = EtsInviteRequest @ "&token=" @ urlEncode($Token);
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
    MessageBoxOK("Could not connect", EtsInviteDialog @ "Could not connect to " @ $ETS::AppName @ " servers.  " @ $ETS::AppName[$MsgCat::network @ "H-SYS-DOWN"] @ "  " @ $ETS::AppName[$MsgCat::network @ "H-SYS-DOWN"][$MsgCat::network @ "H-SEE-FORUMS"], "");
};
function EtsInviteRequest::onConnected(%this) {
    0.5.setValue();
};
function EtsInviteRequest::onDone(%this) {
    1.setControlsActive();
    1.setValue();
    if (($HTTP::StatusOK != %this.statusCode())) {
        "Error communicating with server".onConnectFailed();
        log("communication", "error", EtsInviteDialog @ "client HTTP code: " @ %this.statusCode());
        MessageBoxOK("Server Unavailable", SendInvitePBController, "");
        return EtsInviteDialog;
    }
    %status = findRequestStatus(%this);
    log("network", "debug", "EtsInviteRequest::onDone status: " @ %status);
    if ((%status $= "fail")) {
        %this.getValue("statusMsg").onInviteError();
    }
    if ((EtsInviteDialog SPC %status $= "error")) {
        %this.getValue("statusMsg").onInviteError();
    }
    if ((EtsInviteDialog SPC %status $= "success")) {
        onInviteSuccess();
    }
};
