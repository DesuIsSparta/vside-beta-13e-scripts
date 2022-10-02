function sPChat::init()
{
    if (isObject(pChat))
    {
        pChat.delete();
    }
    new ScriptObject(pChat);
    if (isObject(MissionCleanup))
    {
        MissionCleanup.add(pChat);
    }
    pChat.reset();
    return ;
}
function pChat::reset(%this)
{
    return ;
}
function pChat::say(%this, %text, %noMic, %isAutoReply)
{
    setIdle(0);
    if ($GameConnection.isPresentAtBody())
    {
        commandToServer('PChatSay', %text, %noMic, %isAutoReply);
    }
    else
    {
        handleSystemMessage("msgInfoMessage", "dude, you\'re not at your body. text not sent.");
    }
    %this.raiseHand(%text, "said");
    getUserActivityMgr().setActivityActive("chatting", 1);
    return ;
}
function pChat::whisper(%this, %text, %playerName, %isAutoReply)
{
    %text = trim(%text);
    if (%text $= "")
    {
        return ;
    }
    if (UserListIgnores.hasKey(%playerName))
    {
        handleSystemMessage("msgInfoMessage", "Sorry, you can\'t whisper to <linkcolor:ffddeeff><a:gamelink " @ munge(%playerName) @ ">" @ StripMLControlChars(%playerName) @ "</a>, because you are ignoring them!");
        return ;
    }
    commandToServer('PChatWhisper', %text, makeTaggedString(%playerName), %isAutoReply);
    %this.raiseHand(%text, "whispered");
    return ;
}
function pChat::yell(%this, %text, %isAutoReply)
{
    setIdle(0);
    if ($GameConnection.isPresentAtBody())
    {
        commandToServer('PChatYell', %text, %isAutoReply);
    }
    else
    {
        handleSystemMessage("msgInfoMessage", "dude, you\'re not at your body. text not yelled.");
    }
    %this.raiseHand(%text, "yelled");
    return ;
}
function pChat::clearHistory(%this)
{
    if (isObject(ConvBubVecCtrlMsgVec))
    {
        ConvBubVecCtrlMsgVec.clear();
    }
    return ;
}
function pChat::getPlayerMarkup(%this, %playerName, %color)
{
    return getPlayerMarkup(%playerName, %color, 1);
}
function pChat::getMessageOpenTags(%this, %whispered)
{
    if (%whispered)
    {
        return "<color:505060f0>";
    }
    return "<color:000000>";
}
function pChat::raiseHand(%this, %text, %type)
{
    %wet = fixBadWords(%text);
    if (%wet $= %text)
    {
        return ;
    }
    if (!testFlooding($player, "raiseHand", 1))
    {
        return ;
    }
    commandToServer('raiseHand', %text, %type);
    return ;
}
function Player::PChatProcessIncomingLine(%this, %text, %speechType, %isAutoReply)
{
    if (trim(%text) $= "")
    {
        return "";
    }
    if (%this.isIgnore())
    {
        return "";
    }
    %rangeRadial = $Player::PChat::rangeRadial[%speechType];
    %rangeAngular = $Player::PChat::rangeAngular[%speechType];
    if (!(%speechType $= "regular"))
    {
        %prox = proxRangeMutualDirected(%this.getTransform(), $GameConnection.getHere(), %rangeRadial, %rangeRadial, %rangeAngular, %rangeAngular);
    }
    else
    {
        %prox = %this.getProximityVal();
    }
    sPChat::echo("prox" SPC %prox SPC getDebugString(%this));
    if (%prox < 0.001)
    {
        return "";
    }
    pChat::ProcessIncomingLine(%text, %this, %this.getShapeName(), "", 0, %speechType, %isAutoReply);
    return ;
}
function makeColorTag(%color)
{
    return "<color:" @ %color @ ">";
}
function makeLinkColorTag(%color)
{
    return "<linkcolor:" @ %color @ ">";
}
function pChat::composeLine(%text, %name, %whisperedTo, %ignored, %speechType, %isAutoReply)
{
    %futl = %ignored ? " futilely" : "";
    %yellColor = "600000ff";
    %sosColor = "dd0000ff";
    %abuseColor = "dd0000ff";
    %whisperColor = "505060ff";
    %regularColor = "000000ff";
    %micColor = "dd00ddff";
    %pvtNotifyColor = "333333ff";
    %pubNotifyColor = "cc0000ff";
    %giftColor = "000000ff";
    if (%whisperedTo $= "")
    {
        if (%speechType $= "yell")
        {
            if ($UserPref::Player::YellBlock)
            {
                return "";
            }
            else
            {
                if (%isAutoReply)
                {
                    %text = pChat.getPlayerMarkup(%name, "") @ "autoreplies: " @ makeColorTag(%yellColor) @ %text;
                }
                else
                {
                    %text = pChat.getPlayerMarkup(%name, "") @ ": " @ makeColorTag(%yellColor) @ %text;
                }
            }
        }
        else
        {
            if (%speechType $= "sos")
            {
                %text = pChat.getPlayerMarkup(%name, "") @ " pleads: " @ makeColorTag(%sosColor) @ %text;
            }
            else
            {
                if (%speechType $= "mic")
                {
                    %text = pChat.getPlayerMarkup(%name, "") @ ": " @ makeColorTag(%micColor) @ %text;
                }
                else
                {
                    if (%speechType $= "pvtNotify")
                    {
                        %text = pChat.getPlayerMarkup(%name, "") @ " " @ makeColorTag(%pvtNotifyColor) @ %text;
                    }
                    else
                    {
                        if (%speechType $= "pubNotify")
                        {
                            %text = pChat.getPlayerMarkup(%name, %pubNotifyColor) @ " " @ makeColorTag(%pubNotifyColor) @ %text;
                        }
                        else
                        {
                            if (%isAutoReply)
                            {
                                %text = pChat.getPlayerMarkup(%name, "") @ "autoreplies: " @ makeColorTag(%regularColor) @ %text;
                            }
                            else
                            {
                                %text = pChat.getPlayerMarkup(%name, "") @ ": " @ makeColorTag(%regularColor) @ %text;
                            }
                        }
                    }
                }
            }
        }
    }
    else
    {
        if (%speechType $= "abuse")
        {
            %text = pChat.getPlayerMarkup(%name, "") @ makeColorTag(%abuseColor) @ " narcs on " @ pChat.getPlayerMarkup(%whisperedTo, "") @ ": " @ makeColorTag(%abuseColor) @ %text;
        }
        else
        {
            if (%speechType $= "gift")
            {
                %giftText = getField(%text, 0);
                %message = getField(%text, 1);
                %text = pChat.getPlayerMarkup(%name, "") @ makeColorTag(%giftColor) @ " gave " @ pChat.getPlayerMarkup(%whisperedTo, "") SPC %giftText @ ": " @ makeColorTag(%giftColor) @ %message;
            }
            else
            {
                if (%name $= $player.getShapeName())
                {
                    if (%name $= %whisperedTo)
                    {
                        %text = pChat.getPlayerMarkup(%name, "") @ makeColorTag(%whisperColor) @ " mutters" @ %futl @ " to " @ makeColorTag(%regularColor) @ getPronounHimHerIt($player) @ "self: " @ %text;
                    }
                    else
                    {
                        %text = pChat.getPlayerMarkup(%name, "") @ makeColorTag(%whisperColor) @ %isAutoReply ? " autoreplies" : " whispers" @ %futl @ " to " @ makeColorTag(%regularColor) @ pChat.getPlayerMarkup(%whisperedTo, "") @ ": " @ %text;
                    }
                }
                else
                {
                    if (!(%whisperedTo $= $player.getShapeName()))
                    {
                        if (%name $= %whisperedTo)
                        {
                            %text = pChat.getPlayerMarkup(%name, "") @ makeColorTag(%whisperColor) @ " mutters" @ %futl @ " to " @ makeColorTag(%regularColor) @ getPronounHimHerIt($player) @ "self: " @ %text;
                        }
                        else
                        {
                            %text = pChat.getPlayerMarkup(%name, "") @ makeColorTag(%whisperColor) @ %isAutoReply ? " autoreplies" : " whispers" @ %futl @ " to " @ makeColorTag(%regularColor) @ pChat.getPlayerMarkup(%whisperedTo, "") @ ": " @ %text;
                        }
                    }
                    else
                    {
                        %text = pChat.getPlayerMarkup(%name, "") @ makeColorTag(%whisperColor) @ %isAutoReply ? " autoreplies: " : " whispers: " @ makeColorTag(%regularColor) @ %text;
                        $previousIncomingWhisperer = %name;
                    }
                }
            }
        }
    }
    %text = "<spush>" @ %text @ "<spop>";
    return %text;
}
function pChat::ProcessIncomingLine(%text, %senderPlayer, %name, %whisperedTo, %ignored, %speechType, %isAutoReply)
{
    %text = TryFixBadWords(%text);
    %rawText = %text;
    %text = pChat::composeLine(%text, %name, %whisperedTo, %ignored, %speechType, %isAutoReply);
    if (%text $= "")
    {
        return ;
    }
    if (!isObject(ConvBubVecCtrlMsgVec))
    {
        new MessageVector(ConvBubVecCtrlMsgVec);
        if (isObject(MissionCleanup))
        {
            MissionCleanup.add(ConvBubVecCtrlMsgVec);
        }
    }
    ConvBubVecCtrl.attach(ConvBubVecCtrlMsgVec);
    ConvBubVecCtrlMsgVec.pushBackLine(%text, %senderPlayer);
    if ($UserPref::UI::FlashTaskBar == 1)
    {
        flashWindow(0);
    }
    if (%whisperedTo $= $player.getShapeName())
    {
        if ($UserPref::Audio::NotifyWhisper)
        {
            alxPlay(AudioIm_WhisperIn);
        }
    }
    else
    {
        if ((!isForegroundWindow() || isIdle()) || !PlayGui.canPlayerSeeWorld())
        {
            if ($UserPref::Audio::NotifyChat)
            {
                alxPlay(AudioIm_MessageIn);
            }
        }
    }
    ConvBub.open();
    gSetField(ConvBub, expanded, 0);
    if (!ConvBubScroll.isAtBottom())
    {
        if (ConvBub.isEavesdrop)
        {
            ConvBubScroll.setProfile(ETSScrollDimProfile);
        }
        else
        {
            ConvBubScroll.setProfile(ETSHiScrollProfile);
        }
    }
    pChat::tryLookAt(%senderPlayer, %speechType, %rawText);
    return %text;
}
function pChat::tryLookAt(%targetObj, %speechType, %text)
{
    if (isIdle())
    {
        return ;
    }
    if (%speechType $= "whisper")
    {
        return ;
    }
    if (%speechType $= "regular")
    {
        %length = strlen(%text);
        if (%length < 5)
        {
            return ;
        }
        if (%length < 30)
        {
            if (testFlooding($player, "autolookat", 0))
            {
                return ;
            }
        }
    }
    if (!isObject(%targetObj))
    {
        return ;
    }
    if (%targetObj.getId() == $player.getId())
    {
        return ;
    }
    doLookAt(%targetObj, 0, 0);
    return ;
}
function clientCmdPChatUse(%bool, %rangeRadialRegular, %rangeAngularRegular, %rangeRadialYell, %rangeAngularYell, %rangeRadialMic, %rangeAngularMic)
{
    if (%bool)
    {
        $Player::PChat::rangeRadial["regular"] = %rangeRadialRegular ;
        $Player::PChat::rangeAngular["regular"] = %rangeAngularRegular ;
        $Player::PChat::rangeRadial["yell"] = %rangeRadialYell ;
        $Player::PChat::rangeAngular["yell"] = %rangeAngularYell ;
        $Player::PChat::rangeRadial["mic"] = %rangeRadialMic ;
        $Player::PChat::rangeAngular["mic"] = %rangeAngularMic ;
        $Player::PChat::rangeRadial["pubNotify"] = %rangeRadialMic ;
        $Player::PChat::rangeAngular["pubNotify"] = %rangeAngularMic ;
        $Player::PChat::rangeRadial = %rangeRadialRegular;
        $Player::PChat::rangeAngular = %rangeAngularRegular;
        sPChat::init();
    }
    else
    {
        if (isObject(pChat))
        {
            pChat.delete();
        }
    }
    return ;
}
function clientCmdWhisperIn(%text, %name, %isAutoReply)
{
    if (getField(%text, 0) $= "[c2cCmd]")
    {
        %commandName = getField(%text, 1);
        %param1 = getField(%text, 2);
        %param2 = getField(%text, 3);
        handleC2CCmd(%commandName, %name, %param1, %param2);
        return ;
    }
    pChat::ProcessIncomingLine(%text, 0, %name, $player.getShapeName(), 0, "whisper", %isAutoReply);
    if ((((!%isAutoReply && $UserPref::Player::autoReplyToWhispersWhenAway) && $GameConnection.isPresentAtBody()) && $player.getAFK()) && !$player.haveNotifiedPlayerOfIdleStatus(%name))
    {
        doUserWhisper(%name, $player.getAwayMessage(), 1);
    }
    return ;
}
function clientCmdWhisperOut(%text, %name, %ignored, %isAutoReply)
{
    pChat::ProcessIncomingLine(%text, 0, $player.getShapeName(), %name, %ignored, "whisper", %isAutoReply);
    return ;
}
function clientCmdNotification(%unused, %name, %type)
{
    return ;
}
function clientCmdSosIn(%text, %name)
{
    %text = pChat.getPlayerMarkup(%name, "") SPC "pleads:" SPC %text;
    handleSystemMessage("msgSosMessage", %text);
    return ;
}
