$ChatHud::ChatTarget = 0;
$ChatHud::ListenTarget = 0;
function onServerMessage(%unused) {
};
function onAIMReceive(%sender, %msg) {
    %sender.receivedMessage(%msg);
};
function MessageHud::open(%this, %text) {
    if (%this.isVisible()) {
        return;
    }
    %this.setVisible(1);
    1.makeFirstResponder();
    MessageHudEdit.reinjectOpenEvent();
    100.schedule();
    if (isObject(ConvBubVecCtrlMsgVec)) {
        if ((0.0 > ConvBubVecCtrlMsgVec.getNumLines())) {
            ConvBub.open();
        }
    }
};
function MessageHud::close(%this) {
    if (!(%this.isVisible())) {
        return;
    }
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
    %trgY = ($ButtonBarVar::VerticalAdjustment + (35.0 - getWord(ButtonBar.getTrgPosition(), 1)));
    %this.setTrgPosition(%trgX, %trgY);
    %this.pushToBack();
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
            %curProt = %curBase.get();
            ProtectedAnimsDict;
            if ((1.0 == %curProt)) {
                commandToServer('RequestToStand', 0, 0);
            }
            emote(%text);
        }
    }
    emote(%text);
    if (isObject(pChat)) {
        %text.say(0, 0);
    }
    say(%text);
};
function MessageHudEdit::scanForAutoCommands(%this) {
    if ((1.0 != getWordCount(%this.getValue()))) {
        return;
    }
    %firstWord = getWord(%this.getValue(), 0);
    if ((0.0 < strpos(%this.getValue(), " "))) {
        return;
    }
    if (isObject(CommandAbbreviationMap)) {
        %replace = %firstWord.get();
        CommandAbbreviationMap;
        if (!(%replace $= "")) {
            %this.setValue(setWord(%this.getValue(), 0, %replace));
            %this.setCursorPos(40000);
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
    if ((0.0 != $gChatPreviewTimer)) {
        return;
    }
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
    if (!(%dry $= rtrim(%dry))) {
        return %dry;
    }
    %num = getWordCount(%dry);
    if ((1.0 < %num)) {
        return "";
    }
    %lastWordSize = strlen(getWord(%dry, (1.0 - %num)));
    %wet = getSubStr(%dry, 0, (%lastWordSize - strlen(%dry)));
    return %wet;
};
function MessageHud::setGrayed(%this, %value) {
    if (%value) {
        %this.setBitmap("platform/client/ui/messageHudGray");
    }
    %this.setBitmap("platform/client/ui/messageHud");
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
    %this.setModeIconName(%modeIconName, %modeIconCommand);
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
        0.setVisible();
        position = $gMessageHudEditOriginalPosition @ MessageHudEdit;
        MessageHudModeIcon;
        extent = $gMessageHudEditOriginalExtent @ MessageHudEdit;
    }
    %bitmap = "platform/client/buttons/" @ %modeIconName;
    %positionNew = VectorAdd($gMessageHudEditOriginalPosition, $gMessageHudEditModeIconOffset);
    position = %positionNew @ MessageHudEdit;
    %extentNew = VectorSub($gMessageHudEditOriginalExtent, $gMessageHudEditModeIconOffset);
    extent = %extentNew @ MessageHudEdit;
    %bitmap.setBitmap();
    1.setVisible();
    command = %modeIconCommand @ MessageHudModeIcon;
    MessageHudModeIcon;
};
function displayMicrophoneHelp() {
    if ((PlayGui.getId() != Canvas.getContent())) {
        return;
    }
    userTips::showNow("GotMic");
};
function startTextEntry() {
    if (!(MessageHud.isVisible())) {
        lastkey.open();
    }
    moveMap @ lastkey.setText();
    1.makeFirstResponder();
};
function finishTextEntry(%text) {
    MessageHud.close();
    cancel($gChatPreviewTimer);
    $gChatPreviewTimer = 0;
    $player.onGotTypingSomething("");
    $player.sendPreviewText("");
    "".setValue();
};
