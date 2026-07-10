function toggleMOTDEditDialog() {
    toggleVisibleState();
};
function MOTDEditGui::open(%this) {
    %this.pushDialog(0);
    %this.setVisible(1);
};
function MOTDEditGui::close(%this, %unused) {
    %this.popDialog();
    %this.setVisible(0);
};
function MOTDEditGui::refresh(%this, %messageType) {
    refreshMOTD();
    $UserPref::QOTD::answered = "";
    geTGF_tabs;
    refreshQOTD();
};
function MOTDEditGui::onCopyBasicTo(%this, %messageType) {
    %text = "<linkcolor:3399ff><linkcolorhl:ff93f8><spush><font:BauhausStd-Demi:24><color:eeeeee><just:right>vSide News<spop><br><spush><font:Verdana bold:12><color:ffffff>* Where's the music? Check the <a:http://forums.vside.com/forums/ann.jspa?annID=461>Music Guide.</a><spop><br><linkcolor:4ab2d5><br><br><spush><font:BauhausStd-Demi:19><just:right><color:eeeeee>Be Safe, Be Smart<spop><br><spush><font:Verdana Bold:12><color:FFFF00>* NEVER give out your PASSWORD or INFO<spop><spop><br><spush><font:Verdana Bold:12><color:00cc00><linkcolor:00cc00>* <a:http://www.vside.com/go/vbux>REAL vBux are ONLY sold on vSide.com</a><spop><spop><br><br><br><br><br><br><br><br><spop><spush><font:Verdana:12><spush><linkcolor:ff3bab><just:right><a:http://www.vside.com/go/guidelines>Rules</a> | <a:http://www.vside.com/app/help/category/safety/>Safety</a> | <a:http://www.vside.com/go/parents>Parents</a> | <a:http://www.myspace.com/doppelgangersf>MySpace</a> | <a:http://www.facebook.com/pages/vSide/8050861740>facebook</a><spop><br>";
    (%messageType $= "MOTD");
    %text = "TestQuestion1" @ "\n" @ "<just:center><color:ffffff><font:arial:20>" @ "\n" @ "Question Of The Day PlaceHolder" @ "\n" @ "<font:arial:18>" @ "\n" @ "What would you say to a nice hawaiin punch ?" @ "\n" @ "<a:answer:yes>sounds good</a>             <a:answer:no>no, thanks</a>";
    setClipboard(%text);
};
function MOTDEditGui::onCopyTo(%this) {
    %text = getValue();
    MOTDText;
    %id = qotdID;
    MOTDText;
    %text = %id @ "\n" @ %text;
    !((MOTDText SPC qotdID $= ""));
    setClipboard(%text);
};
function MOTDEditGui::onPasteFrom(%this, %messageType) {
    %text = getClipboard();
    $UserPref::QOTD::answered = "";
    (%messageType $= "QOTD");
    %qID = trim(getWords(%text, 0, 0));
    %text = getWords(%text, 1);
    qotdID = %qID @ MOTDText;
    qotdID = "" @ MOTDText;
    %text.setText();
};
function MOTDEditGui::onAction(%this, %messageType) {
    messageType = %messageType @ MOTDEditGui;
    "confirm: submit as" @ " " @ %messageType.setText();
    0.setVisible();
    0.setVisible();
    1.setVisible();
    1.setVisible();
    %this.onPasteFrom(%messageType);
};
function MOTDEditGui::onCancel(%this) {
    1.setVisible();
    1.setVisible();
    0.setVisible();
    0.setVisible();
};
function MOTDEditGui::onConfirm(%this) {
    1.setVisible();
    1.setVisible();
    0.setVisible();
    0.setVisible();
    %message = getClipboard();
    MOTDEditGuiButtonCancel;
    delete();
    new ();
    add();
    %url = MOTDEditRequest @ $Net::ClientServiceURL @ "/GlobalMessage";
    MissionCleanup;
    %url = MissionCleanup @ isObject() @ %url @ "?user=" @ urlEncode($Player::Name);
    MOTDEditRequest;
    %url = 0 @ ManagerRequest @ %url @ "&token=" @ urlEncode($Token);
    MOTDEditRequest;
    %url = MOTDEditRequest @ isObject() @ %url @ "&message=" @ encodeMOTDString(%message);
    MOTDEditGuiButtonConfirm;
    %url = %this @ urlEncode(messageType);
    MOTDEditGuiButtonDoIt @ MOTDEditGuiButtonDoIt2 @ %url @ "&type=";
    log("communication", "debug", "sending request to set the current" @ " " @ %this @ messageType @ " " @ "message: " @ %url);
    %url.setURL();
    start();
    messageType = %this @ messageType @ MOTDEditRequest;
    MOTDEditRequest;
};
function MOTDEditRequest::onError(%this, %unused, %unused) {
    MessageBoxOK("Server Unavailable", %this @ messageType @ " " @ "NOT submitted.", "");
};
function MOTDEditRequest::onDone(%this) {
    %status = findRequestStatus(%this);
    MessageBoxOK("Success", messageType @ " " @ "submitted, test it out to make sure it's what you wanted.", "");
    MessageBoxOK("Uh Oh", "It didn't work! here's why:" @ "\n" @ %this.getValue("statusMsg"), "");
    log("communication", "warn", "MOTDEditRequest::onDone(): fail");
};
