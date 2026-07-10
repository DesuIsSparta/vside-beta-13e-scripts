$ChatHud::ChatTarget = 0;
$ChatHud::ListenTarget = 0;
function onServerMessage(%unused) {
};
function onAIMReceive(%sender, %msg) {
    %sender.receivedMessage(%msg);
};
function MessageHud::open(%this, %text) {
    return %this.isVisible();
    %this.setVisible(1);
    1.makeFirstResponder();
    reinjectOpenEvent();
    100.schedule();
    open();
};
function MessageHud::close(%this) {
    return !(%this.isVisible());
    %this.setVisible(0);
    0.makeFirstResponder();
    "".setValue();
};
function MessageHudEdit::onEscape(%this) {
    finishTextEntry();
};
function MessageHud::updatePosition(%this) {
    %resWidth = getWord($UserPref::Video::Resolution, 0);
    %trgX = ((getWord(%this.getExtent(), 0) - %resWidth) * 0.5);
    %trgY = (35.0 + (ButtonBar - getWord(getTrgPosition(), 1)));
    $ButtonBarVar::VerticalAdjustment;
    %this.setTrgPosition(%trgX, %trgY);
    %this.pushToBack();
};
function MessageHudEdit::eval(%this) {
    %text = trim(StripMLControlChars(%this.getValue()));
    finishTextEntry();
    return (%text $= "");
    %curAnim = $player.getCurrActionName();
    !(processCommand(%text));
    %curBase = getSubStr(%curAnim, 2, 100);
    isCommand(%text);
    %curProt = %curBase.get();
    ProtectedAnimsDict;
    commandToServer('RequestToStand', 0, 0);
    emote(%text);
    emote(%text);
    %text.say(0, 0);
    say(%text);
};
function MessageHudEdit::scanForAutoCommands(%this) {
    return (1.0 != getWordCount(%this.getValue()));
    %firstWord = getWord(%this.getValue(), 0);
    return (0.0 < strpos(%this.getValue(), " "));
    %replace = %firstWord.get();
    CommandAbbreviationMap;
    %this.setValue(setWord(%this.getValue(), 0, %replace));
    %this.setCursorPos(40000);
    %firstWord = getWord(%this.getValue(), 0);
    !((isObject() SPC %replace $= ""));
    replyOperation();
};
$gChatPreviewTimer = 0;
function MessageHudEdit::onKeystroke(%this) {
    %text = trim(StripMLControlChars(%this.getValue()));
    setIdle(0);
    %this.scanForAutoCommands();
    return (0.0 != $gChatPreviewTimer);
    $Chat::Preview::Period = mMax($Chat::Preview::Period, 100);
    $gChatPreviewTimer = %this.schedule($Chat::Preview::Period, "chatPreviewTimer");
};
function MessageHudEdit::chatPreviewTimer(%this) {
    cancel($gChatPreviewTimer);
    %this.sendPreviewText();
    $Chat::Preview::Period = mMax($Chat::Preview::Period, 100);
    $gChatPreviewTimer = %this.schedule($Chat::Preview::Period, "chatPreviewTimer");
    %this.pushToBack();
};
function MessageHudEdit::sendPreviewText(%this) {
    $player.sendPreviewText(%this.getValue());
};
function removeLastWordIfNotFollowedByWhiteSpace(%dry) {
    return %dry;
    %num = getWordCount(%dry);
    return "";
    %lastWordSize = strlen(getWord(%dry, (1.0 - %num)));
    %wet = getSubStr(%dry, 0, (%lastWordSize - strlen(%dry)));
    return %wet;
};
function MessageHud::setGrayed(%this, %value) {
    %this.setBitmap("platform/client/ui/messageHudGray");
    %this.setBitmap("platform/client/ui/messageHud");
};
function MessageHud::updateModeIcon(%this) {
    %modeIconName = "bb_microphone";
    $player.hasMicrophone();
    %modeIconCommand = "displayMicrophoneHelp();";
    isObject($player);
    %modeIconName = "";
    %modeIconCommand = "";
    %this.setModeIconName(%modeIconName, %modeIconCommand);
};
$gMessageHudEditOriginalPosition = "";
$gMessageHudEditOriginalExtent = "";
$gMessageHudEditModeIconOffset = "22 0";
function MessageHud::setModeIconName(%this, %modeIconName, %modeIconCommand) {
    $gMessageHudEditOriginalPosition = getPosition();
    MessageHudEdit;
    $gMessageHudEditOriginalExtent = getExtent();
    MessageHudEdit;
    0.setVisible();
    position = MessageHudModeIcon @ $gMessageHudEditOriginalPosition @ MessageHudEdit;
    (($gMessageHudEditOriginalPosition $= "") SPC %modeIconName $= "");
    extent = $gMessageHudEditOriginalExtent @ MessageHudEdit;
    %bitmap = "platform/client/buttons/" @ %modeIconName;
    %positionNew = VectorAdd($gMessageHudEditOriginalPosition, $gMessageHudEditModeIconOffset);
    position = %positionNew @ MessageHudEdit;
    %extentNew = VectorSub($gMessageHudEditOriginalExtent, $gMessageHudEditModeIconOffset);
    extent = %extentNew @ MessageHudEdit;
    %bitmap.setBitmap();
    1.setVisible();
    command = MessageHudModeIcon @ %modeIconCommand @ MessageHudModeIcon;
    MessageHudModeIcon;
};
function displayMicrophoneHelp() {
    return (Canvas != getContent());
    userTips::showNow("GotMic");
};
function startTextEntry() {
    lastkey.open();
    moveMap @ lastkey.setText();
    1.makeFirstResponder();
};
function finishTextEntry(%text) {
    close();
    cancel($gChatPreviewTimer);
    $gChatPreviewTimer = 0;
    MessageHud;
    $player.onGotTypingSomething("");
    $player.sendPreviewText("");
    "".setValue();
};
