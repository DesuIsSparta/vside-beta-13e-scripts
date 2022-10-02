$ChatHud::ChatTarget = 0;
$ChatHud::ListenTarget = 0;
function onServerMessage(%unused)
{
    return ;
}
function onAIMReceive(%sender, %msg)
{
    AIMConvManager.receivedMessage(%sender, %msg);
    return ;
}
function MessageHud::open(%this, %text)
{
    if (%this.isVisible())
    {
        return ;
    }
    %this.setVisible(1);
    MessageHudEdit.makeFirstResponder(1);
    MessageHudEdit.reinjectOpenEvent();
    MessageHud.schedule(100, updatePosition);
    if (isObject(ConvBubVecCtrlMsgVec))
    {
        if (ConvBubVecCtrlMsgVec.getNumLines() > 0)
        {
            ConvBub.open();
        }
    }
    return ;
}
function MessageHud::close(%this)
{
    if (!%this.isVisible())
    {
        return ;
    }
    %this.setVisible(0);
    MessageHudEdit.makeFirstResponder(0);
    MessageHudEdit.setValue("");
    return ;
}
function MessageHudEdit::onEscape(%this)
{
    finishTextEntry();
    return ;
}
function MessageHud::updatePosition(%this)
{
    %resWidth = getWord($UserPref::Video::Resolution, 0);
    %trgX = 0.5 * (%resWidth - getWord(%this.getExtent(), 0));
    %trgY = (getWord(ButtonBar.getTrgPosition(), 1) - 35) + $ButtonBarVar::VerticalAdjustment;
    %this.setTrgPosition(%trgX, %trgY);
    PlayGui.pushToBack(%this);
    return ;
}
function MessageHudEdit::eval(%this)
{
    %text = trim(StripMLControlChars(%this.getValue()));
    finishTextEntry();
    if (%text $= "")
    {
        return ;
    }
    if (isCommand(%text))
    {
        if (!processCommand(%text))
        {
            %curAnim = $player.getCurrActionName();
            %curBase = getSubStr(%curAnim, 2, 100);
            %curProt = ProtectedAnimsDict.get(%curBase);
            if (%curProt == 1)
            {
                commandToServer('RequestToStand', 0, 0);
            }
            emote(%text);
        }
    }
    else
    {
        emote(%text);
        if (isObject(pChat))
        {
            pChat.say(%text, 0, 0);
        }
        else
        {
            say(%text);
        }
    }
    return ;
}
function MessageHudEdit::scanForAutoCommands(%this)
{
    if (getWordCount(%this.getValue()) != 1)
    {
        return ;
    }
    %firstWord = getWord(%this.getValue(), 0);
    if (strpos(%this.getValue(), " ") < 0)
    {
        return ;
    }
    if (isObject(CommandAbbreviationMap))
    {
        %replace = CommandAbbreviationMap.get(%firstWord);
        if (!(%replace $= ""))
        {
            %this.setValue(setWord(%this.getValue(), 0, %replace));
            %this.setCursorPos(40000);
        }
    }
    %firstWord = getWord(%this.getValue(), 0);
    if (%firstWord $= "/reply")
    {
        replyOperation();
    }
    else
    {
        if (%firstWord $= "/sos")
        {
        }
        else
        {
            if (%firstWord $= "/911")
            {
            }
        }
    }
    return ;
}
$gChatPreviewTimer = 0;
function MessageHudEdit::onKeystroke(%this)
{
    %text = trim(StripMLControlChars(%this.getValue()));
    if (!(%text $= ""))
    {
        setIdle(0);
    }
    %this.scanForAutoCommands();
    if ($gChatPreviewTimer != 0)
    {
        return ;
    }
    $Chat::Preview::Period = mMax($Chat::Preview::Period, 100);
    $gChatPreviewTimer = %this.schedule($Chat::Preview::Period, "chatPreviewTimer");
    return ;
}
function MessageHudEdit::chatPreviewTimer(%this)
{
    cancel($gChatPreviewTimer);
    %this.sendPreviewText();
    $Chat::Preview::Period = mMax($Chat::Preview::Period, 100);
    $gChatPreviewTimer = %this.schedule($Chat::Preview::Period, "chatPreviewTimer");
    PlayGui.pushToBack(%this);
    return ;
}
function MessageHudEdit::sendPreviewText(%this)
{
    $player.sendPreviewText(%this.getValue());
    return ;
}
function removeLastWordIfNotFollowedByWhiteSpace(%dry)
{
    if (!(%dry $= rtrim(%dry)))
    {
        return %dry;
    }
    %num = getWordCount(%dry);
    if (%num < 1)
    {
        return "";
    }
    %lastWordSize = strlen(getWord(%dry, %num - 1));
    %wet = getSubStr(%dry, 0, strlen(%dry) - %lastWordSize);
    return %wet;
}
function MessageHud::setGrayed(%this, %value)
{
    if (%value)
    {
        %this.setBitmap("platform/client/ui/messageHudGray");
    }
    else
    {
        %this.setBitmap("platform/client/ui/messageHud");
    }
    return ;
}
function MessageHud::updateModeIcon(%this)
{
    if (isObject($player) && $player.hasMicrophone())
    {
        %modeIconName = "bb_microphone";
        %modeIconCommand = "displayMicrophoneHelp();";
    }
    else
    {
        %modeIconName = "";
        %modeIconCommand = "";
    }
    %this.setModeIconName(%modeIconName, %modeIconCommand);
    return ;
}
$gMessageHudEditOriginalPosition = "";
$gMessageHudEditOriginalExtent = "";
$gMessageHudEditModeIconOffset = "22 0";
function MessageHud::setModeIconName(%this, %modeIconName, %modeIconCommand)
{
    if ($gMessageHudEditOriginalPosition $= "")
    {
        $gMessageHudEditOriginalPosition = MessageHudEdit.getPosition();
        $gMessageHudEditOriginalExtent = MessageHudEdit.getExtent();
    }
    if (%modeIconName $= "")
    {
        MessageHudModeIcon.setVisible(0);
        MessageHudEdit.position = $gMessageHudEditOriginalPosition;
        MessageHudEdit.extent = $gMessageHudEditOriginalExtent;
    }
    else
    {
        %bitmap = "platform/client/buttons/" @ %modeIconName;
        %positionNew = VectorAdd($gMessageHudEditOriginalPosition, $gMessageHudEditModeIconOffset);
        MessageHudEdit.position = %positionNew;
        %extentNew = VectorSub($gMessageHudEditOriginalExtent, $gMessageHudEditModeIconOffset);
        MessageHudEdit.extent = %extentNew;
        MessageHudModeIcon.setBitmap(%bitmap);
        MessageHudModeIcon.setVisible(1);
        MessageHudModeIcon.command = %modeIconCommand;
    }
    return ;
}
function displayMicrophoneHelp()
{
    if (Canvas.getContent() != PlayGui.getId())
    {
        return ;
    }
    userTips::showNow("GotMic");
    return ;
}
function startTextEntry()
{
    if (!MessageHud.isVisible())
    {
        MessageHud.open(moveMap.lastkey);
    }
    else
    {
        MessageHudEdit.setText(MessageHudEdit.getValue() @ moveMap.lastkey);
        MessageHudEdit.makeFirstResponder(1);
    }
    return ;
}
function finishTextEntry(%text)
{
    MessageHud.close();
    cancel($gChatPreviewTimer);
    $gChatPreviewTimer = 0;
    $player.onGotTypingSomething("");
    $player.sendPreviewText("");
    MessageHudEdit.setValue("");
    return ;
}
