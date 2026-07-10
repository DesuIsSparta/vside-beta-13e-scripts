$ChatHud::ChatTarget = 0;
$ChatHud::ListenTarget = 0;
function onServerMessage(%unused) {
};
function onAIMReceive(%sender, %msg) {
    %msg.receivedMessage(AIMConvManager, %sender);
};
function MessageHud::open(%this, %text) {
    if (%this.isVisible()) {
        return;
    }
    1.setVisible(%this);
    1.makeFirstResponder(MessageHudEdit);
    MessageHudEdit.reinjectOpenEvent();
    100.schedule(MessageHud);
    if (isObject(ConvBubVecCtrlMsgVec) && (ConvBubVecCtrlMsgVec.getNumLines() > 0.0)) {
        ConvBub.open();
    }
};
function MessageHud::close(%this) {
    if (!(%this.isVisible())) {
        return;
    }
    0.setVisible(%this);
    0.makeFirstResponder(MessageHudEdit);
    "".setValue(MessageHudEdit);
};
function MessageHudEdit::onEscape(%this) {
    finishTextEntry();
};
function MessageHud::updatePosition(%this) {
    %resWidth = getWord($UserPref::Video::Resolution, 0);
    %trgX = (0.5 * (%resWidth - getWord(%this.getExtent(), 0)));
    %trgY = ((getWord(ButtonBar.getTrgPosition(), 1) - 35.0) + $ButtonBarVar::VerticalAdjustment);
    %trgY.setTrgPosition(%this, %trgX);
    %this.pushToBack(PlayGui);
};
function MessageHudEdit::eval(%this) {
    %text = trim(StripMLControlChars(%this.getValue()));
    finishTextEntry();
    if ((%text $= "")) {
        return;
    }
    if (isCommand(%text)) {
        if (!(processCommand(%text))) {
            %curAnim = $player.getCurrActionName();
            %curBase = getSubStr(%curAnim, 2, 100);
            %curProt = %curBase.get(ProtectedAnimsDict);
            if ((%curProt == 1.0)) {
                commandToServer('RequestToStand', 0, 0);
            }
            emote(%text);
        }
    }
    emote(%text);
    if (isObject(pChat)) {
        0.say(pChat, %text, 0);
    }
    say(%text);
};
function MessageHudEdit::scanForAutoCommands(%this) {
    if ((getWordCount(%this.getValue()) != 1.0)) {
        return;
    }
    %firstWord = getWord(%this.getValue(), 0);
    if ((strpos(%this.getValue(), " ") < 0.0)) {
        return;
    }
    if (isObject(CommandAbbreviationMap)) {
        %replace = %firstWord.get(CommandAbbreviationMap);
        if (!(%replace $= "")) {
            setWord(%this.getValue(), 0, %replace).setValue(%this);
            40000.setCursorPos(%this);
        }
    }
    %firstWord = getWord(%this.getValue(), 0);
    if ((%firstWord $= "/reply")) {
        replyOperation();
    }
    if ((%firstWord $= "/sos")) {
    }
};
$gChatPreviewTimer = 0;
function MessageHudEdit::onKeystroke(%this) {
    %text = trim(StripMLControlChars(%this.getValue()));
    if (!(%text $= "")) {
        setIdle(0);
    }
    %this.scanForAutoCommands();
    if (($gChatPreviewTimer != 0.0)) {
        return;
    }
    $Chat::Preview::Period = mMax($Chat::Preview::Period, 100);
    $gChatPreviewTimer = "chatPreviewTimer".schedule(%this, $Chat::Preview::Period);
};
function MessageHudEdit::chatPreviewTimer(%this) {
    cancel($gChatPreviewTimer);
    %this.sendPreviewText();
    $Chat::Preview::Period = mMax($Chat::Preview::Period, 100);
    $gChatPreviewTimer = "chatPreviewTimer".schedule(%this, $Chat::Preview::Period);
    %this.pushToBack(PlayGui);
};
function MessageHudEdit::sendPreviewText(%this) {
    %this.getValue().sendPreviewText($player);
};
function removeLastWordIfNotFollowedByWhiteSpace(%dry) {
    if (!(%dry $= rtrim(%dry))) {
        return %dry;
    }
    %num = getWordCount(%dry);
    if ((%num < 1.0)) {
        return "";
    }
    %lastWordSize = strlen(getWord(%dry, (%num - 1.0)));
    %wet = getSubStr(%dry, 0, (strlen(%dry) - %lastWordSize));
    return %wet;
};
function MessageHud::setGrayed(%this, %value) {
    if (%value) {
        "platform/client/ui/messageHudGray".setBitmap(%this);
    }
    "platform/client/ui/messageHud".setBitmap(%this);
};
function MessageHud::updateModeIcon(%this) {
    if (isObject($player)) {
    }
    if ($player.hasMicrophone()) {
        %modeIconName = "bb_microphone";
        %modeIconCommand = "displayMicrophoneHelp();";
    }
    %modeIconName = "";
    %modeIconCommand = "";
    %modeIconCommand.setModeIconName(%this, %modeIconName);
};
$gMessageHudEditOriginalPosition = "";
$gMessageHudEditOriginalExtent = "";
$gMessageHudEditModeIconOffset = "22 0";
function MessageHud::setModeIconName(%this, %modeIconName, %modeIconCommand) {
    if (($gMessageHudEditOriginalPosition $= "")) {
        $gMessageHudEditOriginalPosition = MessageHudEdit.getPosition();
        $gMessageHudEditOriginalExtent = MessageHudEdit.getExtent();
    }
    if ((%modeIconName $= "")) {
        0.setVisible(MessageHudModeIcon);
        position = $gMessageHudEditOriginalPosition @ MessageHudEdit;
        extent = $gMessageHudEditOriginalExtent @ MessageHudEdit;
    }
    %bitmap = "platform/client/buttons/" @ %modeIconName;
    %positionNew = VectorAdd($gMessageHudEditOriginalPosition, $gMessageHudEditModeIconOffset);
    position = %positionNew @ MessageHudEdit;
    %extentNew = VectorSub($gMessageHudEditOriginalExtent, $gMessageHudEditModeIconOffset);
    extent = %extentNew @ MessageHudEdit;
    %bitmap.setBitmap(MessageHudModeIcon);
    1.setVisible(MessageHudModeIcon);
    command = %modeIconCommand @ MessageHudModeIcon;
};
function displayMicrophoneHelp() {
    if ((Canvas.getContent() != PlayGui.getId())) {
        return;
    }
    userTips::showNow("GotMic");
};
function startTextEntry() {
    if (!(MessageHud.isVisible())) {
        lastkey.open(MessageHud, moveMap);
    }
    moveMap @ lastkey.setText(MessageHudEdit, MessageHudEdit.getValue());
    1.makeFirstResponder(MessageHudEdit);
};
function finishTextEntry(%text) {
    MessageHud.close();
    cancel($gChatPreviewTimer);
    $gChatPreviewTimer = 0;
    "".onGotTypingSomething($player);
    "".sendPreviewText($player);
    "".setValue(MessageHudEdit);
};
