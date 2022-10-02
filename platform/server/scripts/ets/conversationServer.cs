if (!isObject(ConversationList))
{
}
new SimGroup(ConversationList);
function newConversation(%senderPlayer, %targetPlayer)
{
    %senderPos = %senderPlayer.getPosition();
    %conversationPos = %senderPos;
    %newConversation = new Conversation()
    {
        dataBlock = "release_conv";
        position = %conversationPos;
    };
    %senderPlayer.setConversation(%newConversation);
    %newConversation.addParticipant(%senderPlayer);
    ConversationList.add(%newConversation);
    return %newConversation;
}
function leaveListening(%senderPlayer, %conversation)
{
    CONVBUB_DEBUG(senderPlayer SPC "leaving listening on" SPC %conversation);
    if (isObject(%conversation))
    {
        %conversation.removeListener(%senderPlayer);
        %senderPlayer.setConversation(0);
    }
    else
    {
        echo("..oops - NULL conversation");
    }
    return ;
}
function leaveConversation(%senderPlayer)
{
    %conversation = %senderPlayer.getConversation();
    CONVBUB_DEBUG("LEAVE CONVERSATION" SPC getDebugString(%conversation));
    if (isObject(%conversation))
    {
        %conversation.removeMember(%senderPlayer);
        %senderPlayer.setConversation(0);
        gSetField(%senderPlayer, orientedConversation, 0);
    }
    return ;
}
function findConversation(%senderPlayer, %targetPlayer)
{
    %conv = 0;
    if (isObject(%targetPlayer))
    {
        %conv = %targetPlayer.getConversation();
    }
    if (!isObject(%conv) && !%conv.hasParticipant(%targetPlayer))
    {
        if (isObject(%senderPlayer.getConversation()))
        {
            %conv = %senderPlayer.getConversation();
        }
        else
        {
            %conv = newConversation(%senderPlayer, %targetPlayer);
            CONVBUB_DEBUG("new conversation: " @ getDebugString(%conv));
        }
    }
    return %conv;
}
function updateConversationLocations()
{
    %count = ConversationList.getCount();
    CONVBUB_DEBUG("ConversationList has" SPC %count);
    %i = 0;
    while (%i < %count)
    {
        %conversation = ConversationList.getObject(%i);
        if (!%conversation.updateLocation())
        {
            ConversationList.remove(%conversation);
            %conversation.delete();
        }
        %i = %i + 1;
    }
}

$Conv::updateLocationsTimerID = 0;
function updateConvLocationsTimer()
{
    updateConversationLocations();
    cancel($Conv::updateLocationsTimerID);
    $Conv::updateLocationsTimerID = schedule(400, 0, "updateConvLocationsTimer");
    return ;
}
updateConvLocationsTimer();
function serverCmdChatMessage(%senderConnection, %targetPlayer, %message)
{
    if ((%message $= "") && spamAlert(%senderConnection))
    {
        return ;
    }
    if (%targetPlayer != 0)
    {
        %targetPlayer = %senderConnection.resolveObjectFromGhostIndex(%targetPlayer);
    }
    %senderPlayer = %senderConnection.Player;
    ServersideChatMessage(%senderPlayer, %targetPlayer, %message);
    return ;
}
function ServersideChatMessage(%senderPlayer, %targetPlayer, %message)
{
    if (strlen(%message) >= $Pref::Server::MaxChatLen)
    {
        %message = getSubStr(%message, 0, $Pref::Server::MaxChatLen);
    }
    CONVBUB_DEBUG("CHAT MESSAGE sender: " @ getDebugString(%senderPlayer) @ "  target: " @ getDebugString(%targetPlayer) @ "  message: " @ %message);
    if (isAIPlayerObject(%targetPlayer))
    {
        NPCManager.handleTalkedToNPC(%senderPlayer, %targetPlayer, %message);
    }
    %conv = findConversation(%senderPlayer, %targetPlayer);
    if (!isObject(%conv))
    {
        error("could not find conversation.");
        return ;
    }
    CONVBUB_DEBUG("found conv:" SPC %conv);
    %conv.addParticipant(%senderPlayer);
    %conv.addMessage(%senderPlayer, %message);
    if (0)
    {
        if (%conv.countParticipants() > 1)
        {
            if (gGetField(%senderPlayer, orientedConversation) != %conv)
            {
                %senderPlayer.orientTowardsOverTime(%conv, 700);
                gSetField(%senderPlayer, orientedConversation, %conv);
            }
        }
    }
    return ;
}
function serverCmdEavesdrop(%senderConnection, %newTarget)
{
    CONVBUB_DEBUG("EAVESDROP: " @ %newTarget);
    if (%newTarget != 0)
    {
        %newTarget = %senderConnection.resolveObjectFromGhostIndex(%newTarget);
    }
    %senderPlayer = %senderConnection.Player;
    serverSideEavesdrop(%senderPlayer, %newTarget);
    return ;
}
function serverSideEavesdrop(%senderPlayer, %targetPlayer)
{
    CONVBUB_DEBUG("in serverSideEavesdrop:" SPC getDebugString(%senderPlayer) SPC getDebugString(%targetPlayer));
    if (!isPlayerObject(%senderPlayer))
    {
        error("serverSideEavesdrop: got Non-player sender:" SPC getDebugString(%senderPlayer));
        return ;
    }
    if ((%targetPlayer != 0) && !isPlayerObject(%targetPlayer))
    {
        error("serverSideEavesdrop: got Non-zero, Non-player target:" SPC getDebugString(%targetPlayer));
        return ;
    }
    %targetConv = 0;
    if (isObject(%targetPlayer))
    {
        %targetConv = %targetPlayer.getConversation();
        if (!isObject(%targetConv))
        {
            CONVBUB_DEBUG("serverSideEavesdrop:" SPC getDebugString(%senderPlayer) SPC "eavesdropping on no conversation!" SPC getDebugString(%targetPlayer));
            return ;
        }
        if (!%targetConv.hasParticipant(%targetPlayer))
        {
            CONVBUB_DEBUG("serverSideEavesdrop:" SPC getDebugString(%senderPlayer) SPC "eavesdropping on somebody who ain\'t talking:" SPC getDebugString(%targetPlayer));
            return ;
        }
    }
    %senderPlayer.joinConversation(%targetConv, 0);
    return ;
}
function Player::joinConversation(%this, %conv, %asParticipant)
{
    %oldConv = %this.getConversation();
    if (%oldConv == %conv)
    {
        CONVBUB_DEBUG("no change in conversation" SPC getDebugString(%oldConv));
        return ;
    }
    if (isObject(%oldConv))
    {
        if (%oldConv.hasListener(%this))
        {
            %oldConv.removeListener(%this);
        }
        else
        {
            if (%oldConv.hasParticipant(%this))
            {
                %oldConv.removeParticipant(%this);
            }
            else
            {
                error(%this.getDebugString() SPC "thinks it\'s in the wrong conversation:" SPC getDebugString(%oldConv));
            }
        }
    }
    if (!isObject(%conv))
    {
        return ;
    }
    if (%asParticipant)
    {
        %conv.addParticipant(%this);
        %tmp = "participant";
    }
    else
    {
        %conv.addListener(%this);
        %tmp = "listener";
    }
    CONVBUB_DEBUG(getDebugString(%this) SPC "joined" SPC getDebugString(%conv) SPC "as a" SPC %tmp);
    %this.setConversation(%conv);
    return ;
}
function Conversation::onListenerLeft(%this, %player)
{
    commandToClient(%player.getControllingClient(), 'ConvLeftListener');
    return ;
}
function Conversation::onParticipantLeft(%this, %player)
{
    commandToClient(%player.getControllingClient(), 'ConvLeftParticipant');
    return ;
}
function serverCmdLeaveConversation(%senderConnection)
{
    %senderPlayer = %senderConnection.Player;
    CONVBUB_DEBUG("LEAVECONVERSATION: " @ getDebugString(%senderPlayer));
    leaveConversation(%senderPlayer);
    return ;
}
