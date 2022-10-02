$gIsTyping = 0;
$gTypingPreviewAlternator = 1;
$gLastPreviewText = "";
function Player::sendPreviewText(%this, %text)
{
    %text = StripMLControlChars(%text);
    if (getSubStr(trim(%text), 0, 1) $= "/")
    {
        return ;
    }
    %this.onGotTypingSomething(%text);
    if ($Chat::Preview::WordBoundaries)
    {
        %text = removeLastWordIfNotFollowedByWhiteSpace(%text);
    }
    %text = trim(%text);
    if (%text $= "")
    {
        if (!(%text $= $gLastPreviewText))
        {
            $gIsTyping = 0;
            commandToServer('ChatPreviewClear');
        }
        $gLastPreviewText = %text;
        return ;
    }
    $gIsTyping = 1;
    %numChars = $Chat::Preview::Size + 1;
    %start = strlen(%text) - %numChars;
    if (%start < 0)
    {
        %start = 0;
    }
    %text = getSubStr(%text, %start, %numChars);
    if (%text $= $gLastPreviewText)
    {
        return ;
    }
    $gLastPreviewText = %text;
    if (!$UserPref::Chat::ShowTyping)
    {
        $gTypingPreviewAlternator = !$gTypingPreviewAlternator;
        if ($gTypingPreviewAlternator)
        {
            %text = "";
        }
        else
        {
            %text = " ";
        }
    }
    commandToServer('ChatPreview', %text);
    setIdle(0);
    return ;
}
function Player::onGotChatPreview(%this, %dry)
{
    %wet = TryFixBadWords(%dry);
    if (%this $= $player)
    {
        if ($Chat::Preview::ShowOwn)
        {
            %this.setChatPreview(%wet);
        }
        else
        {
            %this.setChatPreview("");
        }
    }
    else
    {
        %this.setChatPreview(%wet);
        %this.onGotTypingSomething(%wet);
    }
    return ;
}
function Player::onGotTypingSomething(%this, %text)
{
    if (%text $= gGetField(%this, lastTypingSomethingText))
    {
        return ;
    }
    gSetField(%this, lastTypingSomethingText, %text);
    cancel(gGetField(%this, IsNoLongerTypingTimer));
    cancel(gGetField(%this, TimeoutChatPreviewTimer));
    if (%text $= "")
    {
        %this.setTyping(0);
    }
    else
    {
        %this.setTyping(1);
        gSetField(%this, IsNoLongerTypingTimer, %this.schedule($Chat::Preview::IsNoLongerTypingDelay, "setTyping", 0));
        gSetField(%this, TimeoutChatPreviewTimer, %this.schedule($Chat::Preview::ChatPreviewTimeout, "onGotChatPreview", ""));
        if ($GameConnection.isPresentAtBody())
        {
            %this.talkingAnimTimer(1);
        }
    }
    return ;
}
function Player::talkingAnimTimer(%this, %startflag)
{
    %whichAnim = 2;
    if (%startflag)
    {
        if (!%this.talking)
        {
            %this.triggerBoneBlendAnimation($BB_HEAD_TALK, 1, 0);
            %this.talking = 1;
            if (%this.hasMicrophone())
            {
                %this.setBlendTargetValue($BB_UPPR_MICROPHONE, 0.75);
                %this.triggerBoneBlendAnimation($BB_UPPR_MICROPHONE, 1, 0);
            }
        }
        %dur = %this.getBlendDuration($BB_HEAD_TALK);
        if (!(%this.AnimationTalkingTimer $= ""))
        {
            cancel(%this.AnimationTalkingTimer);
            %this.AnimationTalkingTimer = "";
        }
        %this.AnimationTalkingTimer = %this.schedule(%dur, talkingAnimTimer, 0);
    }
    else
    {
        %this.triggerBoneBlendAnimation($BB_HEAD_TALK, 0, 0);
        %this.AnimationTalkingTimer = "";
        %this.talking = 0;
        if (%this.hasMicrophone())
        {
            %this.setBlendTargetValue($BB_UPPR_MICROPHONE, 0.2);
            %this.triggerBoneBlendAnimation($BB_UPPR_MICROPHONE, 1, 0);
        }
    }
    return ;
}
function talkBlender::animate(%this)
{
    return ;
}
