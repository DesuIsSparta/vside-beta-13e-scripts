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
        HudTabs.selectTabWithName("word");
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
    if ((12.0 >= %hr)) {
        %hr = (12.0 - %hr);
        %ap = "pm";
    }
    if ((0.0 == %hr)) {
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
    if (( >= 0.0)) {
        %lastMsgLvl = getSubStr(%msgString, %idx, 9);
        %msgString = getSubStr(%msgString, 0, %idx) @ getSubStr(%msgString, (9.0 + %idx), 1000);
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
        HudTabs.pulseTabWithName("word");
    }
    %timeStamp = "<spush><color:66aaffff>" @ %timeStamp @ "<spop>";
    SystemMessageTextCtrl.addText(%timeStamp @ " " @ %msgString, 1, 1);
    if ($UserPref::Audio::NotifyWhisper) {
        alxPlay(AudioIm_SystemMessageIn);
    }
};
HudTabs.getCurrentTab().bufferSize = 0 @ SystemMessageTextCtrl;
function SystemMessageTextCtrl::addText(%this, %txtString) {
    if ((%this.bufferSize $= "")) {
        %this.bufferSize = 0;
    }
    if ((20.0 >= %this.bufferSize)) {
        %this.deleteOldestBufferLine();
    }
    %this.bufferMessage = %txtString @ %this.bufferSize;
    %this.bufferSize = (1.0 + %this.bufferSize);
    %this.refresh();
};
function SystemMessageTextCtrl::clearText(%this) {
    %this.bufferSize = 0;
    %this.setText(%this.DefaultMessage);
};
function SystemMessageTextCtrl::deleteOldestBufferLine(%this) {
    %n = 0;
    if ((%this.bufferSize < %n)) {
        %this.bufferMessage = (1.0 + %n) @ %this.bufferMessage @ %n;
        %n = (1.0 + %n);
    }
    %this.bufferSize = (1.0 - %this.bufferSize);
    (%this.bufferSize < %n);
};
function SystemMessageTextCtrl::refresh(%this) {
    %this.setText("");
    %n = (1.0 - %this.bufferSize);
    if ((0.0 >= %n)) {
        %curString = "<spush>";
        if (((1.0 - %this.bufferSize) == %n)) {
            %curString = %curString @ "<b>";
        }
        %curString = %curString @ "<color:" @ %this.getMessageColor((1.0 - (%n - %this.bufferSize))) @ ">" @ %n @ %this.bufferMessage @ "<spop>\n";
        Parent::addText(%this, %curString, 0, 1);
        %n = (1.0 - %n);
    }
    %this.scrollToTop();
};
function SystemMessageTextCtrl::getMessageColor(%this, %age) {
    if ((3.0 > %age)) {
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
        LinkContextMenu.initWithURL(%url);
        LinkContextMenu.showAtCursor();
    }
    if (!(%this.selectionActive)) {
        TheShapeNameHud.makeFirstResponder(1);
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
            %this.updateTwoPlayerActionRequest(%name, %coanim, %requestId, 1);
        }
    }
    if ((getWord(%url, 0) $= "DECLINE_2PLAYER_ACTION")) {
        %name = unmunge(getWord(%url, 1));
        %requestId = getWord(%url, 2);
        %coanim = getWords(%url, 3);
        if (!(%name $= "")) {
            commandToServer('CoAnimRespond', %requestId, "DECLINE MANUAL");
            %this.updateTwoPlayerActionRequest(%name, %coanim, %requestId, 0);
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
            gameMgrClient.requestToInspectGame(getWord(%url, 2));
            GameMgrHudWin.open();
            GameMgrHudTabs.selectTabWithName("INSPECT");
        }
    }
    if ((getWord(%url, 0) $= "answerHelpMeMode")) {
        %requestId = getWord(%url, 1);
        %newbName = unmunge(getWords(%url, 2, 11111));
        answerHelpMeMode(%newbName, %requestId);
        %this.changeLinesEndingInString("<a:" @ %url, "- You answered the call!");
    }
    if (!(%this.selectionActive)) {
        TheShapeNameHud.makeFirstResponder(1);
    }
};
function SystemMessageTextCtrl::updateFriendRequest(%this, %name, %accept) {
    %acceptString = "";
    if ((1.0 == %accept)) {
        %acceptString = %acceptString @ "Accepted!";
    }
    if ((0.0 == %accept)) {
        %acceptString = %acceptString @ "Declined!";
    }
    %acceptString = %acceptString @ "(they cancelled)";
    if (!(%name $= "")) {
        %linkStart = "<a:ACCEPT " @ munge(%name) @ ">";
    }
    %linkStart = "<a:ACCEPT ";
    %this.changeLinesEndingInString(%linkStart, %acceptString);
};
function SystemMessageTextCtrl::updateTwoPlayerActionRequest(%this, %name, %coAnimName, %requestId, %accept) {
    %acceptString = "";
    if ((0.0 == %accept)) {
        %acceptString = %acceptString @ "Declined!";
    }
    if ((1.0 == %accept)) {
        %acceptString = %acceptString @ "Accepted!";
    }
    if ((2.0 == %accept)) {
        %acceptString = %acceptString @ "(they cancelled)";
    }
    %acceptString = %acceptString @ "(timed out)";
    if (!(%name $= "")) {
        %linkStart = "<a:ACCEPT_2PLAYER_ACTION " @ munge(%name) @ " " @ %requestId @ " " @ %coAnimName @ ">";
    }
    %this.changeLinesEndingInString(%linkStart, %acceptString);
};
function SystemMessageTextCtrl::changeLinesEndingInString(%this, %replaceThis, %withThis) {
    %i = (1.0 - %this.bufferSize);
    if ((0.0 >= %i)) {
        %curLine = %this.bufferMessage;
        %i;
        %start = strstr(%curLine, %replaceThis);
        if ((0.0 < %start)) {
        }
        %newLine = getSubStr(%curLine, 0, %start) @ " " @ %withThis;
        %this.bufferMessage = %newLine @ %i;
        %i = (1.0 - %i);
    }
    %this.refresh();
};
