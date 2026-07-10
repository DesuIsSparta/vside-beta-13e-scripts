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
    class = SendInvitePBController @ new () @ "ProgressBarController";
    ScriptObject;
    0;
    add();
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
    MessageBoxOK(%to[$MsgCat::invitation @ "E-SEND-TITLE"], (%to $= ""), "");
    return;
    %this.sendInviteRequestToEnvManager(%to, %note);
};
function EtsInviteDialog::sendInviteRequestToEnvManager(%this, %to, %message) {
    delete();
    %inviteRequest = new ();
    EtsInviteRequest;
    %inviteRequest.add();
    %url = MissionCleanup @ $Net::SecureURL @ "?cmd=invite_email";
    isObject();
    %token = MissionCleanup @ "&token=" @ urlEncode($Token);
    ManagerRequest;
    %to = strreplace(%to, " ", "");
    0;
    %to = strreplace(%to, ",", " ");
    EtsInviteRequest;
    %to = trim(%to);
    isObject();
    %count = getWordCount(%to);
    EtsInviteRequest;
    %numTargetMails = "&numEmails=" @ %count;
    %targetMails = "";
    %i = 0;
    %targetMails = (%count < %i) @ %targetMails @ "&email" @ %i @ "=" @ urlEncode(getWord(%to, %i));
    %i = (1.0 + %i);
    %note = "";
    (%count < %i);
    %note = !((%message $= "")) @ "&noteFromSender=" @ urlEncode(%message);
    %url = %url @ %token @ %numTargetMails @ %targetMails @ %note;
    log("network", "debug", "send invite command: " @ %url);
    %inviteRequest.setURL(%url);
    %inviteRequest.setProgress(1);
    %this.setControlsActive(0);
    0.1.setValue();
    %inviteRequest.start();
};
function EtsInviteDialog::onConnectFailed(%this, %msg) {
    %msg = "Could not connect";
    (%msg $= "");
    %this.setControlsActive(1);
    0.setValue();
};
function EtsInviteDialog::onInviteSuccess(%this) {
    MessageBoxOK(, , "EtsInviteDialog.close();");
};
function EtsInviteDialog::onInviteError(%this, %errorMsg) {
    %errorMsg = "no error message specified. try again later";
    (%errorMsg $= "");
    MessageBoxOK(%errorMsg[$MsgCat::invitation @ "E-SEND-TITLE"], %errorMsg, "");
};
function EtsInviteRequest::onError(%this, %errorNum, %unused) {
    "Could not reach server".onConnectFailed();
    MessageBoxOK("Could Not Find Server", EtsInviteDialog, "");
    "Could not connect".onConnectFailed();
    MessageBoxOK("Could not connect", ($CURL::CouldNotResolveHost == %errorNum) @ EtsInviteDialog @ "Could not connect to " @ $ETS::AppName @ " servers.  " @ $ETS::AppName[$MsgCat::network @ "H-SYS-DOWN"] @ "  " @ $ETS::AppName[$MsgCat::network @ "H-SYS-DOWN"][$MsgCat::network @ "H-SEE-FORUMS"], "");
};
function EtsInviteRequest::onConnected(%this) {
    0.5.setValue();
};
function EtsInviteRequest::onDone(%this) {
    1.setControlsActive();
    1.setValue();
    "Error communicating with server".onConnectFailed();
    log("communication", "error", EtsInviteDialog @ "client HTTP code: " @ %this.statusCode());
    MessageBoxOK("Server Unavailable", ($HTTP::StatusOK != %this.statusCode()), "");
    return SendInvitePBController;
    %status = findRequestStatus(%this);
    log("network", "debug", "EtsInviteRequest::onDone status: " @ %status);
    %this.getValue("statusMsg").onInviteError();
    %this.getValue("statusMsg").onInviteError();
    onInviteSuccess();
};
