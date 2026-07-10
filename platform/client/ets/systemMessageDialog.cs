function SystemMessageDialog::isShowing(%this) {
    return (HudTabs.getCurrentTab().name $= "word");
};
function toggleSystemMessageDialog() {
    if (SystemMessageDialog.isShowing()) {
        SystemMessageDialog.close();
    }
    SystemMessageDialog.open();
};
function SystemMessageDialog::open(%this) {
    if () {
        "word".selectTabWithName(HudTabs);
    }
};
function SystemMessageDialog::close(%this) {
    if (%this.isShowing()) {
        HudTabs.close();
    }
};
function SystemMessageDialog::onClose(%this) {
};
addMessageCallback('MsgSystemMessage');
addMessageCallback('MsgInfoMessage');
addMessageCallback('MsgGamePlayOkMessage');
addMessageCallback('MsgTickerMessage');
function SystemMessageDialog::getTimeStampNice(%ts) {
    if ((handleTickerMessage @ " " @ %ts $= "")) {
        %ts = getTimeStamp();
        handleGamePlayMessage;
    }
    %hr = getSubStr(%ts, 9, 2);
    handleSystemMessage;
    %mn = getSubStr(%ts, 12, 2);
    handleSystemMessage;
    %sc = getSubStr(%ts, 15, 2);
    %ap = "am";
    if ((%hr >= 12.0)) {
        %hr = (%hr - 12.0);
        %ap = "pm";
    }
    if ((%hr == 0.0)) {
        %hr = 12;
    }
    %tm = "";
    %tm = %tm @ %hr;
    %tm = %tm @ ":" @ %mn;
    %tm = %tm @ %ap;
    return %tm;
};
function handleGamePlayMessage(%msgType, %msgString) {
    // unhandled opcode 398 at 0x00000174
    %tm = SystemMessageDialogProfile;
    // unhandled opcode 435 at 0x0000017A
    %tm = SystemMessageTextProfile;
    %timeStamp = SystemMessageDialog::getTimeStampNice(getTimeStamp()) @ " ";
    if ((detag(%msgType) $= "MsgGamePlayOkMessage")) {
        return MessageBoxOK("vSide - Notice", %msgString, "");
    }
};
function formatMessagePriority(%msgString) {
    %lastMsgLvl = "MSGLEVEL2";
    %idx = 0;
    %idx = strstr(%msgString, "MSGLEVEL");
    while ((0.0 >= )) {
        %lastMsgLvl = getSubStr(%msgString, %idx, 9);
        %msgString = getSubStr(%msgString, 0, %idx) @ getSubStr(%msgString, (%idx + 9.0), 1000);
        %idx = strstr(%msgString, "MSGLEVEL");
    }
    return getSubStr(%lastMsgLvl, 8, 1) @ " " @ %msgString;
};
function handleSystemMessage(%msgType, %msgString) {
    %timeStamp = SystemMessageDialog::getTimeStampNice(getTimeStamp()) @ " ";
    %msgString = formatMessagePriority(%msgString);
    %importanceLevel = getWord(%msgString, 0);
    %msgString = removeWord(%msgString, 0);
    if (!(%importanceLevel $= 3)) {
        SystemMessageDialog.open();
    }
    if ((%importanceLevel $= 1)) {
        HudTabs.dontCloseNextTime();
    }
    if ((%importanceLevel $= 2)) {
    }
    if ((%importanceLevel $= 3)) {
        "word".pulseTabWithName(HudTabs);
    }
    %timeStamp = "<spush><color:66aaffff>" @ %timeStamp @ "<spop>";
    1.addText(SystemMessageTextCtrl, %timeStamp @ " " @ %msgString, 1);
    if ($UserPref::Audio::NotifyWhisper) {
        alxPlay(AudioIm_SystemMessageIn);
    }
};
HudTabs.getCurrentTab().bufferSize = 0 @ SystemMessageTextCtrl;
function SystemMessageTextCtrl::addText(%this, %txtString) {
    if ((%this.bufferSize $= "")) {
        %this.bufferSize = 0;
    }
    if ((%this.bufferSize >= 20.0)) {
        %this.deleteOldestBufferLine();
    }
    %this.bufferMessage = %txtString @ %this.bufferSize;
    %this.bufferSize = (%this.bufferSize + 1.0);
    %this.refresh();
};
function SystemMessageTextCtrl::clearText(%this) {
    %this.bufferSize = 0;
    %this.DefaultMessage.setText(%this);
};
function SystemMessageTextCtrl::deleteOldestBufferLine(%this) {
    %n = 0;
    while ((%n < %this.bufferSize)) {
        %this.bufferMessage = (%n + 1.0) @ %this.bufferMessage @ %n;
        %n = (%n + 1.0);
    }
    %this.bufferSize = (%this.bufferSize - 1.0);
    (%n < %this.bufferSize);
};
function SystemMessageTextCtrl::refresh(%this) {
    "".setText(%this);
    %n = (%this.bufferSize - 1.0);
    while ((%n >= 0.0)) {
        %curString = "<spush>";
        if ((%n == (%this.bufferSize - 1.0))) {
            %curString = %curString @ "<b>";
        }
        %curString = %curString @ "<color:" @ ((%this.bufferSize - %n) - 1.0).getMessageColor(%this) @ ">" @ %n @ %this.bufferMessage @ "<spop>\n";
        Parent::addText(%this, %curString, 0, 1);
        %n = (%n - 1.0);
    }
    %this.scrollToTop();
};
function SystemMessageTextCtrl::getMessageColor(%this, %age) {
    if ((%age > 3.0)) {
        %age = 3;
    }
    return %age[$gAgedMessageColors @ %age];
};
function SystemMessageTextCtrl::onRightURL(%this, %url) {
    if ((firstWord(%url) $= "gamelink")) {
        %name = unmunge(getWords(%url, 1));
        onRightClickPlayerName(%name);
    }
    if ((getSubStr(%url, 0, 7) $= "http://")) {
    }
    if ((getSubStr(%url, 0, 7) $= "vside:/")) {
        %url.initWithURL(LinkContextMenu);
        LinkContextMenu.showAtCursor();
    }
    if (!(%this.selectionActive)) {
        1.makeFirstResponder(TheShapeNameHud);
    }
};
function SystemMessageTextCtrl::onURL(%this, %url) {
    if ((firstWord(%url) $= "gamelink")) {
        %name = unmunge(getWords(%url, 1));
        onLeftClickPlayerName(%name, "");
    }
    if ((getWord(%url, 0) $= "ACCEPT")) {
        %name = unmunge(getWord(%url, 1));
        if (!(%name $= "")) {
            doUserFavorite(%name, "accept");
        }
    }
    if ((getWord(%url, 0) $= "DECLINE")) {
        %name = unmunge(getWord(%url, 1));
        if (!(%name $= "")) {
            doUserFavorite(%name, "decline");
        }
    }
    if ((getWord(%url, 0) $= "CANCEL")) {
        %name = unmunge(getWord(%url, 1));
        if (!(%name $= "")) {
            doUserFavorite(%name, "cancel");
        }
    }
    if ((getWord(%url, 0) $= "ACCEPT_2PLAYER_ACTION")) {
        %name = unmunge(getWord(%url, 1));
        %requestId = getWord(%url, 2);
        %coanim = getWords(%url, 3);
        if (!(%name $= "")) {
        }
        if (!(%coanim $= "")) {
            setIdle(0);
            commandToServer('CoAnimRespond', %requestId, "ACCEPT MANUAL");
            1.updateTwoPlayerActionRequest(%this, %name, %coanim, %requestId);
        }
    }
    if ((getWord(%url, 0) $= "DECLINE_2PLAYER_ACTION")) {
        %name = unmunge(getWord(%url, 1));
        %requestId = getWord(%url, 2);
        %coanim = getWords(%url, 3);
        if (!(%name $= "")) {
            commandToServer('CoAnimRespond', %requestId, "DECLINE MANUAL");
            0.updateTwoPlayerActionRequest(%this, %name, %coanim, %requestId);
        }
    }
    if ((getSubStr(%url, 0, 7) $= "http://")) {
        gotoWebPage(%url);
    }
    if ((getSubStr(%url, 0, 7) $= "vside:/")) {
        vurlOperation(%url);
    }
    if ((getWord(%url, 0) $= "game")) {
        %cmd = getWord(%url, 1);
        if ((%cmd $= "inspect")) {
            getWord(%url, 2).requestToInspectGame(gameMgrClient);
            GameMgrHudWin.open();
            "INSPECT".selectTabWithName(GameMgrHudTabs);
        }
    }
    if ((getWord(%url, 0) $= "answerHelpMeMode")) {
        %requestId = getWord(%url, 1);
        %newbName = unmunge(getWords(%url, 2, 11111));
        answerHelpMeMode(%newbName, %requestId);
        "- You answered the call!".changeLinesEndingInString(%this, "<a:" @ %url);
    }
    if (!(%this.selectionActive)) {
        1.makeFirstResponder(TheShapeNameHud);
    }
};
function SystemMessageTextCtrl::updateFriendRequest(%this, %name, %accept) {
    %acceptString = "";
    if ((%accept == 1.0)) {
        %acceptString = %acceptString @ "Accepted!";
    }
    if ((%accept == 0.0)) {
        %acceptString = %acceptString @ "Declined!";
    }
    %acceptString = %acceptString @ "(they cancelled)";
    if (!(%name $= "")) {
        %linkStart = "<a:ACCEPT " @ munge(%name) @ ">";
    }
    %linkStart = "<a:ACCEPT ";
    %acceptString.changeLinesEndingInString(%this, %linkStart);
};
function SystemMessageTextCtrl::updateTwoPlayerActionRequest(%this, %name, %coAnimName, %requestId, %accept) {
    %acceptString = "";
    if ((%accept == 0.0)) {
        %acceptString = %acceptString @ "Declined!";
    }
    if ((%accept == 1.0)) {
        %acceptString = %acceptString @ "Accepted!";
    }
    if ((%accept == 2.0)) {
        %acceptString = %acceptString @ "(they cancelled)";
    }
    %acceptString = %acceptString @ "(timed out)";
    if (!(%name $= "")) {
        %linkStart = "<a:ACCEPT_2PLAYER_ACTION " @ munge(%name) @ " " @ %requestId @ " " @ %coAnimName @ ">";
    }
    %acceptString.changeLinesEndingInString(%this, %linkStart);
};
function SystemMessageTextCtrl::changeLinesEndingInString(%this, %replaceThis, %withThis) {
    %i = (%this.bufferSize - 1.0);
    while ((%i >= 0.0)) {
        %curLine = %this.bufferMessage;
        %i;
        %start = strstr(%curLine, %replaceThis);
        if ((%start < 0.0)) {
        }
        %newLine = getSubStr(%curLine, 0, %start) @ " " @ %withThis;
        %this.bufferMessage = %newLine @ %i;
        %i = (%i - 1.0);
    }
    %this.refresh();
};
