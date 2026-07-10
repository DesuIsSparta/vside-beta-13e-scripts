function toggleMOTDEditDialog() {
    toggleVisibleState(MOTDEditGui);
};
function MOTDEditGui::open(%this) {
    0.pushDialog(Canvas, %this);
    1.setVisible(%this);
};
function MOTDEditGui::close(%this, %unused) {
    %this.popDialog(Canvas);
    0.setVisible(%this);
};
function MOTDEditGui::refresh(%this, %messageType) {
    if ((%messageType $= "MOTD")) {
        geTGF_tabs.refreshMOTD();
    }
    $UserPref::QOTD::answered = "";
    geTGF_tabs.refreshQOTD();
};
function MOTDEditGui::onCopyBasicTo(%this, %messageType) {
    if ((%messageType $= "MOTD")) {
        %text = "<linkcolor:3399ff><linkcolorhl:ff93f8><spush><font:BauhausStd-Demi:24><color:eeeeee><just:right>vSide News<spop><br><spush><font:Verdana bold:12><color:ffffff>* Where's the music? Check the <a:http://forums.vside.com/forums/ann.jspa?annID=461>Music Guide.</a><spop><br><linkcolor:4ab2d5><br><br><spush><font:BauhausStd-Demi:19><just:right><color:eeeeee>Be Safe, Be Smart<spop><br><spush><font:Verdana Bold:12><color:FFFF00>* NEVER give out your PASSWORD or INFO<spop><spop><br><spush><font:Verdana Bold:12><color:00cc00><linkcolor:00cc00>* <a:http://www.vside.com/go/vbux>REAL vBux are ONLY sold on vSide.com</a><spop><spop><br><br><br><br><br><br><br><br><spop><spush><font:Verdana:12><spush><linkcolor:ff3bab><just:right><a:http://www.vside.com/go/guidelines>Rules</a> | <a:http://www.vside.com/app/help/category/safety/>Safety</a> | <a:http://www.vside.com/go/parents>Parents</a> | <a:http://www.myspace.com/doppelgangersf>MySpace</a> | <a:http://www.facebook.com/pages/vSide/8050861740>facebook</a><spop><br>";
    }
    %text = "TestQuestion1" @ "\n" @ "<just:center><color:ffffff><font:arial:20>" @ "\n" @ "Question Of The Day PlaceHolder" @ "\n" @ "<font:arial:18>" @ "\n" @ "What would you say to a nice hawaiin punch ?" @ "\n" @ "<a:answer:yes>sounds good</a>             <a:answer:no>no, thanks</a>";
    setClipboard(%text);
};
function MOTDEditGui::onCopyTo(%this) {
    %text = MOTDText.getValue();
    if (!(MOTDText @ " " @ qotdID $= "")) {
        %id = qotdID;
        MOTDText;
        %text = %id @ "\n" @ %text;
    }
    setClipboard(%text);
};
function MOTDEditGui::onPasteFrom(%this, %messageType) {
    %text = getClipboard();
    if ((%messageType $= "QOTD")) {
        $UserPref::QOTD::answered = "";
        %qID = trim(getWords(%text, 0, 0));
        %text = getWords(%text, 1);
        qotdID = %qID @ MOTDText;
    }
    qotdID = "" @ MOTDText;
    %text.setText(MOTDText);
};
function MOTDEditGui::onAction(%this, %messageType) {
    messageType = %messageType @ MOTDEditGui;
    "confirm: submit as" @ " " @ %messageType.setText(MOTDEditGuiButtonConfirm);
    0.setVisible(MOTDEditGuiButtonDoIt);
    0.setVisible(MOTDEditGuiButtonDoIt2);
    1.setVisible(MOTDEditGuiButtonConfirm);
    1.setVisible(MOTDEditGuiButtonCancel);
    %messageType.onPasteFrom(%this);
};
function MOTDEditGui::onCancel(%this) {
    1.setVisible(MOTDEditGuiButtonDoIt);
    1.setVisible(MOTDEditGuiButtonDoIt2);
    0.setVisible(MOTDEditGuiButtonConfirm);
    0.setVisible(MOTDEditGuiButtonCancel);
};
function MOTDEditGui::onConfirm(%this) {
    1.setVisible(MOTDEditGuiButtonDoIt);
    1.setVisible(MOTDEditGuiButtonDoIt2);
    0.setVisible(MOTDEditGuiButtonConfirm);
    0.setVisible(MOTDEditGuiButtonCancel);
    %message = getClipboard();
    if (isObject(MOTDEditRequest)) {
        MOTDEditRequest.delete();
    }
    new ManagerRequest(MOTDEditRequest);
    if (isObject(MissionCleanup)) {
        MOTDEditRequest.add(MissionCleanup);
    }
    %url = $Net::ClientServiceURL @ "/GlobalMessage";
    %url = %url @ "?user=" @ urlEncode($Player::Name);
    %url = %url @ "&token=" @ urlEncode($Token);
    %url = %url @ "&message=" @ encodeMOTDString(%message);
    %url = %url @ "&type=" @ urlEncode(%this.messageType);
    log("communication", "debug", "sending request to set the current" @ " " @ %this.messageType @ " " @ "message: " @ %url);
    %url.setURL(MOTDEditRequest);
    MOTDEditRequest.start();
    %this.messageType = %this.messageType @ MOTDEditRequest;
};
function MOTDEditRequest::onError(%this, %unused, %unused) {
    MessageBoxOK("Server Unavailable", "The server is currently unavailable." @ " " @ %this.messageType @ " " @ "NOT submitted.", "");
};
function MOTDEditRequest::onDone(%this) {
    %status = findRequestStatus(%this);
    if ((%status $= "success")) {
        MessageBoxOK("Success", %this.messageType @ " " @ "submitted, test it out to make sure it's what you wanted.", "");
    }
    if ((%status $= "fail")) {
        MessageBoxOK("Uh Oh", "It didn't work! here's why:" @ "\n" @ "statusMsg".getValue(%this), "");
        log("communication", "warn", "MOTDEditRequest::onDone(): fail");
    }
};
