function toggleMOTDEditDialog()
{
    toggleVisibleState(MOTDEditGui);
    return ;
}
function MOTDEditGui::open(%this)
{
    Canvas.pushDialog(%this, 0);
    %this.setVisible(1);
    return ;
}
function MOTDEditGui::close(%this, %unused)
{
    Canvas.popDialog(%this);
    %this.setVisible(0);
    return ;
}
function MOTDEditGui::refresh(%this, %messageType)
{
    if (%messageType $= "MOTD")
    {
        geTGF_tabs.refreshMOTD();
    }
    else
    {
        $UserPref::QOTD::answered = "";
        geTGF_tabs.refreshQOTD();
    }
    return ;
}
function MOTDEditGui::onCopyBasicTo(%this, %messageType)
{
    if (%messageType $= "MOTD")
    {
        %text = "<linkcolor:3399ff><linkcolorhl:ff93f8><spush><font:BauhausStd-Demi:24><color:eeeeee><just:right>vSide News<spop><br><spush><font:Verdana bold:12><color:ffffff>* Where\'s the music? Check the <a:http://forums.vside.com/forums/ann.jspa?annID=461>Music Guide.</a><spop><br><linkcolor:4ab2d5><br><br><spush><font:BauhausStd-Demi:19><just:right><color:eeeeee>Be Safe, Be Smart<spop><br><spush><font:Verdana Bold:12><color:FFFF00>* NEVER give out your PASSWORD or INFO<spop><spop><br><spush><font:Verdana Bold:12><color:00cc00><linkcolor:00cc00>* <a:http://www.vside.com/go/vbux>REAL vBux are ONLY sold on vSide.com</a><spop><spop><br><br><br><br><br><br><br><br><spop><spush><font:Verdana:12><spush><linkcolor:ff3bab><just:right><a:http://www.vside.com/go/guidelines>Rules</a> | <a:http://www.vside.com/app/help/category/safety/>Safety</a> | <a:http://www.vside.com/go/parents>Parents</a> | <a:http://www.myspace.com/doppelgangersf>MySpace</a> | <a:http://www.facebook.com/pages/vSide/8050861740>facebook</a><spop><br>";
    }
    else
    {
        %text = "TestQuestion1" NL "<just:center><color:ffffff><font:arial:20>" NL "Question Of The Day PlaceHolder" NL "<font:arial:18>" NL "What would you say to a nice hawaiin punch ?" NL "<a:answer:yes>sounds good</a>             <a:answer:no>no, thanks</a>";
    }
    setClipboard(%text);
    return ;
}
function MOTDEditGui::onCopyTo(%this)
{
    %text = MOTDText.getValue();
    if (!(MOTDText.qotdID $= ""))
    {
        %id = MOTDText.qotdID;
        %text = %id NL %text;
    }
    setClipboard(%text);
    return ;
}
function MOTDEditGui::onPasteFrom(%this, %messageType)
{
    %text = getClipboard();
    if (%messageType $= "QOTD")
    {
        $UserPref::QOTD::answered = "";
        %qID = trim(getWords(%text, 0, 0));
        %text = getWords(%text, 1);
        MOTDText.qotdID = %qID;
    }
    else
    {
        MOTDText.qotdID = "";
    }
    MOTDText.setText(%text);
    return ;
}
function MOTDEditGui::onAction(%this, %messageType)
{
    MOTDEditGui.messageType = %messageType;
    MOTDEditGuiButtonConfirm.setText("confirm: submit as" SPC %messageType);
    MOTDEditGuiButtonDoIt.setVisible(0);
    MOTDEditGuiButtonDoIt2.setVisible(0);
    MOTDEditGuiButtonConfirm.setVisible(1);
    MOTDEditGuiButtonCancel.setVisible(1);
    %this.onPasteFrom(%messageType);
    return ;
}
function MOTDEditGui::onCancel(%this)
{
    MOTDEditGuiButtonDoIt.setVisible(1);
    MOTDEditGuiButtonDoIt2.setVisible(1);
    MOTDEditGuiButtonConfirm.setVisible(0);
    MOTDEditGuiButtonCancel.setVisible(0);
    return ;
}
function MOTDEditGui::onConfirm(%this)
{
    MOTDEditGuiButtonDoIt.setVisible(1);
    MOTDEditGuiButtonDoIt2.setVisible(1);
    MOTDEditGuiButtonConfirm.setVisible(0);
    MOTDEditGuiButtonCancel.setVisible(0);
    %message = getClipboard();
    if (isObject(MOTDEditRequest))
    {
        MOTDEditRequest.delete();
    }
    new ManagerRequest(MOTDEditRequest);
    if (isObject(MissionCleanup))
    {
        MissionCleanup.add(MOTDEditRequest);
    }
    %url = $Net::ClientServiceURL @ "/GlobalMessage";
    %url = %url @ "?user=" @ urlEncode($Player::Name);
    %url = %url @ "&token=" @ urlEncode($Token);
    %url = %url @ "&message=" @ encodeMOTDString(%message);
    %url = %url @ "&type=" @ urlEncode(%this.messageType);
    log("communication", "debug", "sending request to set the current" SPC %this.messageType SPC "message: " @ %url);
    MOTDEditRequest.setURL(%url);
    MOTDEditRequest.start();
    MOTDEditRequest.messageType = %this.messageType;
    return ;
}
function MOTDEditRequest::onError(%this, %unused, %unused)
{
    MessageBoxOK("Server Unavailable", "The server is currently unavailable." SPC %this.messageType SPC "NOT submitted.", "");
    return ;
}
function MOTDEditRequest::onDone(%this)
{
    %status = findRequestStatus(%this);
    if (%status $= "success")
    {
        MessageBoxOK("Success", %this.messageType SPC "submitted, test it out to make sure it\'s what you wanted.", "");
    }
    else
    {
        if (%status $= "fail")
        {
            MessageBoxOK("Uh Oh", "It didn\'t work! here\'s why:" NL %this.getValue("statusMsg"), "");
            log("communication", "warn", "MOTDEditRequest::onDone(): fail");
        }
    }
    return ;
}
