$gIsTyping = 0;
$gTypingPreviewAlternator = 1;
$gLastPreviewText = "";
function Player::sendPreviewText(%this, %text) {
    %text = StripMLControlChars(%text);
    if ((getSubStr(trim(%text), 0, 1) $= "/")) {
        return;
    }
    %text.onGotTypingSomething(%this);
    if ($Chat::Preview::WordBoundaries) {
        %text = removeLastWordIfNotFollowedByWhiteSpace(%text);
    }
    %text = trim(%text);
    if ((%text $= "")) {
        if (!(%text $= $gLastPreviewText)) {
            $gIsTyping = 0;
            commandToServer('ChatPreviewClear');
        }
        $gLastPreviewText = %text;
        return;
    }
    $gIsTyping = 1;
    %numChars = ($Chat::Preview::Size + 1.0);
    %start = (strlen(%text) - %numChars);
    if ((%start < 0.0)) {
        %start = 0;
    }
    %text = getSubStr(%text, %start, %numChars);
    if ((%text $= $gLastPreviewText)) {
        return;
    }
    $gLastPreviewText = %text;
    if (!($UserPref::Chat::ShowTyping)) {
        $gTypingPreviewAlternator = !($gTypingPreviewAlternator);
        if ($gTypingPreviewAlternator) {
            %text = "";
        }
        %text = " ";
    }
    commandToServer('ChatPreview', %text);
    setIdle(0);
};
function Player::onGotChatPreview(%this, %dry) {
    %wet = TryFixBadWords(%dry);
    if ((%this $= $player)) {
        if ($Chat::Preview::ShowOwn) {
            %wet.setChatPreview(%this);
        }
        "".setChatPreview(%this);
    }
    %wet.setChatPreview(%this);
    %wet.onGotTypingSomething(%this);
};
function Player::onGotTypingSomething(%this, %text) {
    if ((lastTypingSomethingText $= gGetField(%this))) {
        return %text;
    }
    gSetField(%this, lastTypingSomethingText, %text);
    cancel(IsNoLongerTypingTimer, gGetField(%this));
    cancel(TimeoutChatPreviewTimer, gGetField(%this));
    if ((%text $= "")) {
        0.setTyping(%this);
    }
    1.setTyping(%this);
    gSetField(%this, IsNoLongerTypingTimer, 0.schedule(%this, $Chat::Preview::IsNoLongerTypingDelay, "setTyping"));
    gSetField(%this, TimeoutChatPreviewTimer, "".schedule(%this, $Chat::Preview::ChatPreviewTimeout, "onGotChatPreview"));
    if ($GameConnection.isPresentAtBody()) {
        1.talkingAnimTimer(%this);
    }
};
function Player::talkingAnimTimer(%this, %startflag) {
    %whichAnim = 2;
    if (%startflag) {
        if (!(%this.talking)) {
            0.triggerBoneBlendAnimation(%this, $BB_HEAD_TALK, 1);
            %this.talking = 1;
            if (%this.hasMicrophone()) {
                0.75.setBlendTargetValue(%this, $BB_UPPR_MICROPHONE);
                0.triggerBoneBlendAnimation(%this, $BB_UPPR_MICROPHONE, 1);
            }
        }
        %dur = $BB_HEAD_TALK.getBlendDuration(%this);
        if (!(%this.AnimationTalkingTimer $= "")) {
            cancel(%this.AnimationTalkingTimer);
            %this.AnimationTalkingTimer = "";
        }
        %this.AnimationTalkingTimer = 0.schedule(%this, %dur, talkingAnimTimer);
    }
    0.triggerBoneBlendAnimation(%this, $BB_HEAD_TALK, 0);
    %this.AnimationTalkingTimer = "";
    %this.talking = 0;
    if (%this.hasMicrophone()) {
        0.2.setBlendTargetValue(%this, $BB_UPPR_MICROPHONE);
        0.triggerBoneBlendAnimation(%this, $BB_UPPR_MICROPHONE, 1);
    }
};
function talkBlender::animate(%this) {
};
