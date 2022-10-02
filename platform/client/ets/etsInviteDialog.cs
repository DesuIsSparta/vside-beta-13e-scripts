function EtsInviteDialog::open(%this)
{
    %screenWidth = getWord($UserPref::Video::Resolution, 0);
    %screenHeight = getWord($UserPref::Video::Resolution, 1);
    %thisExtent = %this.getExtent();
    %width = getWord(%thisExtent, 0);
    %height = getWord(%thisExtent, 1);
    %this.reposition((%screenWidth / 2) - (%width / 2), (%screenHeight / 2) - (%height / 2));
    %this.setVisible(1);
    PlayGui.focusAndRaise(%this);
    %this.initializeWithDefaults();
    return ;
}
function EtsInviteDialog::close(%this)
{
    %this.setVisible(0);
    PlayGui.focusTopWindow();
    return 1;
}
function toggleEtsInviteDialog()
{
    PlayGui.showRaiseOrHide(EtsInviteDialog);
    return ;
}
function EtsInviteDialog::setControlsActive(%this, %flag)
{
    InviteDialogButtonSend.setActive(%flag);
    return ;
}
function EtsInviteDialog::onWake(%this)
{
    %this.setControlsActive(1);
    if (!isObject(SendInvitePBController))
    {
        new ScriptObject(SendInvitePBController);
        if (isObject(MissionCleanup))
        {
            MissionCleanup.add(SendInvitePBController);
        }
    }
    return ;
}
function EtsInviteDialog::initializeWithDefaults(%this)
{
    ETSInviteToTextCtrl.setValue("");
    %text = "";
    ETSInviteNoteTextCtrl.setText(%text);
    return ;
}
function EtsInviteDialog::sendInvite(%this)
{
    %to = trim(ETSInviteToTextCtrl.getText());
    %note = trim(ETSInviteNoteTextCtrl.getText());
    if (%to $= "")
    {
        MessageBoxOK($MsgCat::invitation["E-SEND-TITLE"], $MsgCat::invitation["EMPTY-TO-FIELD"], "");
        return ;
    }
    %this.sendInviteRequestToEnvManager(%to, %note);
    return ;
}
function EtsInviteDialog::sendInviteRequestToEnvManager(%this, %to, %message)
{
    if (isObject(EtsInviteRequest))
    {
        EtsInviteRequest.delete();
    }
    %inviteRequest = new ManagerRequest(EtsInviteRequest);
    if (isObject(MissionCleanup))
    {
        MissionCleanup.add(%inviteRequest);
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
    while (%i < %count)
    {
        %targetMails = %targetMails @ "&email" @ %i @ "=" @ urlEncode(getWord(%to, %i));
        %i = %i + 1;
    }
    %note = "";
    if (!(%message $= ""))
    {
        %note = "&noteFromSender=" @ urlEncode(%message);
    }
    %url = %url @ %token @ %numTargetMails @ %targetMails @ %note;
    log("network", "debug", "send invite command: " @ %url);
    %inviteRequest.setURL(%url);
    %inviteRequest.setProgress(1);
    %this.setControlsActive(0);
    SendInvitePBController.setValue(0.1);
    %inviteRequest.start();
    return ;
}
function EtsInviteDialog::onConnectFailed(%this, %msg)
{
    if (%msg $= "")
    {
        %msg = "Could not connect";
    }
    %this.setControlsActive(1);
    SendInvitePBController.setValue(0);
    return ;
}
function EtsInviteDialog::onInviteSuccess(%this)
{
    MessageBoxOK($MsgCat::invitation["S-SEND-TITLE"], $MsgCat::invitation["INVITE-SENT"], "EtsInviteDialog.close();");
    return ;
}
function EtsInviteDialog::onInviteError(%this, %errorMsg)
{
    if (%errorMsg $= "")
    {
        %errorMsg = "no error message specified. try again later";
    }
    MessageBoxOK($MsgCat::invitation["E-SEND-TITLE"], %errorMsg, "");
    return ;
}
function EtsInviteRequest::onError(%this, %errorNum, %unused)
{
    if (%errorNum == $CURL::CouldNotResolveHost)
    {
        EtsInviteDialog.onConnectFailed("Could not reach server");
        MessageBoxOK("Could Not Find Server", $MsgCat::network["E-SERVER-DNS"], "");
    }
    else
    {
        EtsInviteDialog.onConnectFailed("Could not connect");
        MessageBoxOK("Could not connect", "Could not connect to " @ $ETS::AppName @ " servers.  " @ $MsgCat::network["H-SYS-DOWN"] @ "  " @ $MsgCat::network["H-SEE-FORUMS"], "");
    }
    return ;
}
function EtsInviteRequest::onConnected(%this)
{
    SendInvitePBController.setValue(0.5);
    return ;
}
function EtsInviteRequest::onDone(%this)
{
    EtsInviteDialog.setControlsActive(1);
    SendInvitePBController.setValue(1);
    if (%this.statusCode() != $HTTP::StatusOK)
    {
        EtsInviteDialog.onConnectFailed("Error communicating with server");
        log("communication", "error", "client HTTP code: " @ %this.statusCode());
        MessageBoxOK("Server Unavailable", $MsgCat::network["E-SERVER-UNAVAIL"], "");
        return ;
    }
    %status = findRequestStatus(%this);
    log("network", "debug", "EtsInviteRequest::onDone status: " @ %status);
    if (%status $= "fail")
    {
        EtsInviteDialog.onInviteError(%this.getValue("statusMsg"));
    }
    else
    {
        if (%status $= "error")
        {
            EtsInviteDialog.onInviteError(%this.getValue("statusMsg"));
        }
        else
        {
            if (%status $= "success")
            {
                EtsInviteDialog.onInviteSuccess();
            }
        }
    }
    return ;
}
