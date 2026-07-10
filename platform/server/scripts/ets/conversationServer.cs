if (!(isObject(ConversationList))) {
    new SimGroup(ConversationList);
}
function newConversation(%senderPlayer, %targetPlayer) {
    %senderPos = ConversationList.getPosition(%senderPlayer);
    %conversationPos = %senderPos;
    %newConversation = new Conversation("") {
        dataBlock = 0 @ "release_conv";
        position = %conversationPos;
    };
    %senderPlayer.setConversation(%newConversation);
    %newConversation.addParticipant(%senderPlayer);
    ConversationList.add(%newConversation);
    return %newConversation;
};
function leaveListening(%senderPlayer, %conversation) {
    CONVBUB_DEBUG(( - senderPlayer) @ "leaving listening on" @ " " @ %conversation);
    if (isObject(%conversation)) {
        %conversation.removeListener(%senderPlayer);
        %senderPlayer.setConversation(0);
    }
    echo("..oops - NULL conversation");
    return;
};
function leaveConversation(%senderPlayer) {
    %conversation = %senderPlayer.getConversation();
    CONVBUB_DEBUG("LEAVE CONVERSATION" @ " " @ getDebugString(%conversation));
    if (isObject(%conversation)) {
        %conversation.removeMember(%senderPlayer);
        %senderPlayer.setConversation(0);
        gSetField(%senderPlayer, orientedConversation, 0);
    }
    return;
};
function findConversation(%senderPlayer, %targetPlayer) {
    %conv = 0;
    if (isObject(%targetPlayer)) {
        %conv = %targetPlayer.getConversation();
    }
    if (!(isObject(%conv))) {
    }
    if (!(%conv.hasParticipant(%targetPlayer))) {
        if (isObject(%senderPlayer.getConversation())) {
            %conv = %senderPlayer.getConversation();
        }
        %conv = newConversation(%senderPlayer, %targetPlayer);
        CONVBUB_DEBUG("new conversation: " @ getDebugString(%conv));
    }
    return %conv;
};
function updateConversationLocations() {
    %count = ConversationList.getCount();
    CONVBUB_DEBUG("ConversationList has" @ " " @ %count);
    %i = 0;
    if ((%count < %i)) {
        %conversation = ConversationList.getObject(%i);
        if (!(%conversation.updateLocation())) {
            ConversationList.remove(%conversation);
            %conversation.delete();
        }
        %i = (1.0 + %i);
    }
};
$Conv::updateLocationsTimerID = 0;
function updateConvLocationsTimer() {
    updateConversationLocations();
    cancel($Conv::updateLocationsTimerID);
    $Conv::updateLocationsTimerID = schedule(400, 0, "updateConvLocationsTimer");
    return;
};
updateConvLocationsTimer();
function serverCmdChatMessage(%senderConnection, %targetPlayer, %message) {
    if ((%message $= "")) {
    }
    if (spamAlert(%senderConnection)) {
        return;
    }
    if ((0.0 != %targetPlayer)) {
        %targetPlayer = %senderConnection.resolveObjectFromGhostIndex(%targetPlayer);
    }
    %senderPlayer = %senderConnection.Player;
    ServersideChatMessage(%senderPlayer, %targetPlayer, %message);
    return;
};
function ServersideChatMessage(%senderPlayer, %targetPlayer, %message) {
    if (($Pref::Server::MaxChatLen >= strlen(%message))) {
        %message = getSubStr(%message, 0, $Pref::Server::MaxChatLen);
    }
    CONVBUB_DEBUG("CHAT MESSAGE sender: " @ getDebugString(%senderPlayer) @ "  target: " @ getDebugString(%targetPlayer) @ "  message: " @ %message);
    if (isAIPlayerObject(%targetPlayer)) {
        NPCManager.handleTalkedToNPC(%senderPlayer, %targetPlayer, %message);
    }
    %conv = findConversation(%senderPlayer, %targetPlayer);
    if (!(isObject(%conv))) {
        error("could not find conversation.");
        return;
    }
    CONVBUB_DEBUG("found conv:" @ " " @ %conv);
    %conv.addParticipant(%senderPlayer);
    %conv.addMessage(%senderPlayer, %message);
    if (0) {
        if ((1.0 > %conv.countParticipants())) {
            if ((orientedConversation != gGetField(%senderPlayer))) {
                %senderPlayer.orientTowardsOverTime(%conv, 700);
                gSetField(%senderPlayer, orientedConversation, %conv);
            }
        }
    }
    return %conv;
};
function serverCmdEavesdrop(%senderConnection, %newTarget) {
    CONVBUB_DEBUG("EAVESDROP: " @ %newTarget);
    if ((0.0 != %newTarget)) {
        %newTarget = %senderConnection.resolveObjectFromGhostIndex(%newTarget);
    }
    %senderPlayer = %senderConnection.Player;
    serverSideEavesdrop(%senderPlayer, %newTarget);
    return;
};
function serverSideEavesdrop(%senderPlayer, %targetPlayer) {
    CONVBUB_DEBUG("in serverSideEavesdrop:" @ " " @ getDebugString(%senderPlayer) @ " " @ getDebugString(%targetPlayer));
    if (!(isPlayerObject(%senderPlayer))) {
        error("serverSideEavesdrop: got Non-player sender:" @ " " @ getDebugString(%senderPlayer));
        return;
    }
    if ((0.0 != %targetPlayer)) {
    }
    if (!(isPlayerObject(%targetPlayer))) {
        error("serverSideEavesdrop: got Non-zero, Non-player target:" @ " " @ getDebugString(%targetPlayer));
        return;
    }
    %targetConv = 0;
    if (isObject(%targetPlayer)) {
        %targetConv = %targetPlayer.getConversation();
        if (!(isObject(%targetConv))) {
            CONVBUB_DEBUG("serverSideEavesdrop:" @ " " @ getDebugString(%senderPlayer) @ " " @ "eavesdropping on no conversation!" @ " " @ getDebugString(%targetPlayer));
            return;
        }
        if (!(%targetConv.hasParticipant(%targetPlayer))) {
            CONVBUB_DEBUG("serverSideEavesdrop:" @ " " @ getDebugString(%senderPlayer) @ " " @ "eavesdropping on somebody who ain't talking:" @ " " @ getDebugString(%targetPlayer));
            return;
        }
    }
    %senderPlayer.joinConversation(%targetConv, 0);
    return;
};
function Player::joinConversation(%this, %conv, %asParticipant) {
    %oldConv = %this.getConversation();
    if ((%conv == %oldConv)) {
        CONVBUB_DEBUG("no change in conversation" @ " " @ getDebugString(%oldConv));
        return;
    }
    if (isObject(%oldConv)) {
        if (%oldConv.hasListener(%this)) {
            %oldConv.removeListener(%this);
        }
        if (%oldConv.hasParticipant(%this)) {
            %oldConv.removeParticipant(%this);
        }
        error(%this.getDebugString() @ " " @ "thinks it's in the wrong conversation:" @ " " @ getDebugString(%oldConv));
    }
    if (!(isObject(%conv))) {
        return;
    }
    if (%asParticipant) {
        %conv.addParticipant(%this);
        %tmp = "participant";
    }
    %conv.addListener(%this);
    %tmp = "listener";
    CONVBUB_DEBUG(getDebugString(%this) @ " " @ "joined" @ " " @ getDebugString(%conv) @ " " @ "as a" @ " " @ %tmp);
    %this.setConversation(%conv);
    return;
};
function Conversation::onListenerLeft(%this, %player) {
    commandToClient(%player.getControllingClient(), 'ConvLeftListener');
    return;
};
function Conversation::onParticipantLeft(%this, %player) {
    commandToClient(%player.getControllingClient(), 'ConvLeftParticipant');
    return;
};
function serverCmdLeaveConversation(%senderConnection) {
    %senderPlayer = %senderConnection.Player;
    CONVBUB_DEBUG("LEAVECONVERSATION: " @ getDebugString(%senderPlayer));
    leaveConversation(%senderPlayer);
    return;
};
