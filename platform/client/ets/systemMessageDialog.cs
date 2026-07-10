function SystemMessageDialog::isShowing(%this) {
    return (getCurrentTab() SPC name $= "word");
};
function toggleSystemMessageDialog() {
    close();
    open();
};
function SystemMessageDialog::open(%this) {
    "word".selectTabWithName();
};
function SystemMessageDialog::close(%this) {
    close();
};
function SystemMessageDialog::onClose(%this) {
};
addMessageCallback('MsgSystemMessage');
addMessageCallback('MsgInfoMessage');
addMessageCallback('MsgGamePlayOkMessage');
addMessageCallback('MsgTickerMessage');
function SystemMessageDialog::getTimeStampNice(%ts) {
    %ts = getTimeStamp();
    (handleTickerMessage SPC %ts $= "");
    %hr = getSubStr(%ts, 9, 2);
    handleGamePlayMessage;
    %mn = getSubStr(%ts, 12, 2);
    handleSystemMessage;
    %sc = getSubStr(%ts, 15, 2);
    handleSystemMessage;
    %ap = "am";
    %hr = (12.0 - %hr);
    (12.0 >= %hr);
    %ap = "pm";
    %hr = 12;
    (0.0 == %hr);
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
    return MessageBoxOK("vSide - Notice", %msgString, "");
};
function formatMessagePriority(%msgString) {
    %lastMsgLvl = "MSGLEVEL2";
    %idx = 0;
    %idx = strstr(%msgString, "MSGLEVEL");
    %lastMsgLvl = getSubStr(%msgString, %idx, 9);
    ( >= 0.0);
    %msgString = getSubStr(%msgString, 0, %idx) @ getSubStr(%msgString, (9.0 + %idx), 1000);
    %idx = strstr(%msgString, "MSGLEVEL");
    return getSubStr(%lastMsgLvl, 8, 1) @ " " @ %msgString;
};
function handleSystemMessage(%msgType, %msgString) {
    %timeStamp = SystemMessageDialog::getTimeStampNice(getTimeStamp()) @ " ";
    %msgString = formatMessagePriority(%msgString);
    %importanceLevel = getWord(%msgString, 0);
    %msgString = removeWord(%msgString, 0);
    open();
    dontCloseNextTime();
    "word".pulseTabWithName();
    %timeStamp = ((HudTabs SPC %importanceLevel $= 2) SPC %importanceLevel $= 3) @ HudTabs @ "<spush><color:66aaffff>" @ %timeStamp @ "<spop>";
    (SystemMessageDialog SPC %importanceLevel $= 1);
    !((%importanceLevel $= 3)) @ SystemMessageTextCtrl @ %timeStamp @ " " @ %msgString.addText(1, 1);
    alxPlay();
};
bufferSize = 0 @ SystemMessageTextCtrl;
function SystemMessageTextCtrl::addText(%this, %txtString) {
    bufferSize = (%this SPC bufferSize $= "") @ 0 @ %this;
    %this.deleteOldestBufferLine();
    bufferMessage = (%this >= bufferSize) @ %txtString @ %this @ bufferSize @ %this;
    20.0;
    bufferSize = (%this + bufferSize);
    1.0;
    %this.refresh();
};
function SystemMessageTextCtrl::clearText(%this) {
    bufferSize = 0 @ %this;
    %this.setText(DefaultMessage);
};
function SystemMessageTextCtrl::deleteOldestBufferLine(%this) {
    %n = 0;
    bufferMessage = %this @ (bufferSize < %n) @ (1.0 + %n) @ %this @ bufferMessage @ %n @ %this;
    %n = (1.0 + %n);
    bufferSize = (%this - bufferSize);
    1.0;
};
function SystemMessageTextCtrl::refresh(%this) {
    %this.setText("");
    %n = (%this - bufferSize);
    1.0;
    %curString = "<spush>";
    (0.0 >= %n);
    %curString = ((%this - bufferSize) == %n) @ %curString @ "<b>";
    1.0;
    %curString = %curString @ "<color:" @ 1.0 @ %this.getMessageColor((%n - (%this - bufferSize))) @ ">" @ %n @ %this @ bufferMessage @ "<spop>\n";
    Parent::addText(%this, %curString, 0, 1);
    %n = (1.0 - %n);
    %this.scrollToTop();
};
function SystemMessageTextCtrl::getMessageColor(%this, %age) {
    %age = 3;
    (3.0 > %age);
    return %age[$gAgedMessageColors @ %age];
};
function SystemMessageTextCtrl::onRightURL(%this, %url) {
    %name = unmunge(getWords(%url, 1));
    (firstWord(%url) $= "gamelink");
    onRightClickPlayerName(%name);
    %url.initWithURL();
    showAtCursor();
    1.makeFirstResponder();
};
function SystemMessageTextCtrl::onURL(%this, %url) {
    %name = unmunge(getWords(%url, 1));
    (firstWord(%url) $= "gamelink");
    onLeftClickPlayerName(%name, "");
    %name = unmunge(getWord(%url, 1));
    (getWord(%url, 0) $= "ACCEPT");
    doUserFavorite(%name, "accept");
    %name = unmunge(getWord(%url, 1));
    (!((%name $= "")) SPC getWord(%url, 0) $= "DECLINE");
    doUserFavorite(%name, "decline");
    %name = unmunge(getWord(%url, 1));
    (!((%name $= "")) SPC getWord(%url, 0) $= "CANCEL");
    doUserFavorite(%name, "cancel");
    %name = unmunge(getWord(%url, 1));
    (!((%name $= "")) SPC getWord(%url, 0) $= "ACCEPT_2PLAYER_ACTION");
    %requestId = getWord(%url, 2);
    %coanim = getWords(%url, 3);
    setIdle(0);
    commandToServer('CoAnimRespond', %requestId, "ACCEPT MANUAL");
    %this.updateTwoPlayerActionRequest(%name, %coanim, %requestId, 1);
    %name = unmunge(getWord(%url, 1));
    (!((!((%name $= "")) SPC %coanim $= "")) SPC getWord(%url, 0) $= "DECLINE_2PLAYER_ACTION");
    %requestId = getWord(%url, 2);
    %coanim = getWords(%url, 3);
    commandToServer('CoAnimRespond', %requestId, "DECLINE MANUAL");
    %this.updateTwoPlayerActionRequest(%name, %coanim, %requestId, 0);
    gotoWebPage(%url);
    vurlOperation(%url);
    %cmd = getWord(%url, 1);
    (((!((%name $= "")) SPC getSubStr(%url, 0, 7) $= "http://") SPC getSubStr(%url, 0, 7) $= "vside:/") SPC getWord(%url, 0) $= "game");
    getWord(%url, 2).requestToInspectGame();
    open();
    "INSPECT".selectTabWithName();
    %requestId = getWord(%url, 1);
    (GameMgrHudTabs SPC getWord(%url, 0) $= "answerHelpMeMode");
    %newbName = unmunge(getWords(%url, 2, 11111));
    GameMgrHudWin;
    answerHelpMeMode(%newbName, %requestId);
    %this.changeLinesEndingInString(gameMgrClient @ "<a:" @ %url, "- You answered the call!");
    1.makeFirstResponder();
};
function SystemMessageTextCtrl::updateFriendRequest(%this, %name, %accept) {
    %acceptString = "";
    %acceptString = (1.0 == %accept) @ %acceptString @ "Accepted!";
    %acceptString = (0.0 == %accept) @ %acceptString @ "Declined!";
    %acceptString = %acceptString @ "(they cancelled)";
    %linkStart = !((%name $= "")) @ "<a:ACCEPT " @ munge(%name) @ ">";
    %linkStart = "<a:ACCEPT ";
    %this.changeLinesEndingInString(%linkStart, %acceptString);
};
function SystemMessageTextCtrl::updateTwoPlayerActionRequest(%this, %name, %coAnimName, %requestId, %accept) {
    %acceptString = "";
    %acceptString = (0.0 == %accept) @ %acceptString @ "Declined!";
    %acceptString = (1.0 == %accept) @ %acceptString @ "Accepted!";
    %acceptString = (2.0 == %accept) @ %acceptString @ "(they cancelled)";
    %acceptString = %acceptString @ "(timed out)";
    %linkStart = !((%name $= "")) @ "<a:ACCEPT_2PLAYER_ACTION " @ munge(%name) @ " " @ %requestId @ " " @ %coAnimName @ ">";
    %this.changeLinesEndingInString(%linkStart, %acceptString);
};
function SystemMessageTextCtrl::changeLinesEndingInString(%this, %replaceThis, %withThis) {
    %i = (%this - bufferSize);
    1.0;
    %curLine = bufferMessage;
    (0.0 >= %i) @ %i @ %this;
    %start = strstr(%curLine, %replaceThis);
    %newLine = getSubStr(%curLine, 0, %start) @ " " @ %withThis;
    (0.0 < %start);
    bufferMessage = %newLine @ %i @ %this;
    %i = (1.0 - %i);
    %this.refresh();
};
