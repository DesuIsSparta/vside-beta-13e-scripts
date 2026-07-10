if (!(isObject(ConversationList))) {
    new SimGroup(ConversationList);
}
function newConversation(%senderPlayer, %targetPlayer) {
    %senderPos = %senderPlayer.getPosition();
    %conversationPos = %senderPos;
    %newConversation = new Conversation("") {
        dataBlock = "release_conv";
        position = %conversationPos;
    };
    %newConversation.setConversation(%senderPlayer);
    %senderPlayer.addParticipant(%newConversation);
    %newConversation.add(ConversationList);
    return %newConversation;
};
function leaveListening(%senderPlayer, %conversation) {
    CONVBUB_DEBUG(senderPlayer @ " " @ "leaving listening on" @ " " @ %conversation);
    if (isObject(%conversation)) {
        %senderPlayer.removeListener(%conversation);
        0.setConversation(%senderPlayer);
    }
    echo("..oops - NULL conversation");
    return;
};
function leaveConversation(%senderPlayer) {
    %conversation = %senderPlayer.getConversation();
    CONVBUB_DEBUG("LEAVE CONVERSATION" @ " " @ getDebugString(%conversation));
    if (isObject(%conversation)) {
        %senderPlayer.removeMember(%conversation);
        0.setConversation(%senderPlayer);
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
    if (!(%targetPlayer.hasParticipant(%conv))) {
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
    while ((%i < %count)) {
        %conversation = %i.getObject(ConversationList);
        if (!(%conversation.updateLocation())) {
            %conversation.remove(ConversationList);
            %conversation.delete();
        }
        %i = (%i + 1.0);
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
    if ((%targetPlayer != 0.0)) {
        %targetPlayer = %targetPlayer.resolveObjectFromGhostIndex(%senderConnection);
    }
    %senderPlayer = %senderConnection.Player;
    ServersideChatMessage(%senderPlayer, %targetPlayer, %message);
    return;
};
function ServersideChatMessage(%senderPlayer, %targetPlayer, %message) {
    if ((strlen(%message) >= $Pref::Server::MaxChatLen)) {
        %message = getSubStr(%message, 0, $Pref::Server::MaxChatLen);
    }
    CONVBUB_DEBUG("CHAT MESSAGE sender: " @ getDebugString(%senderPlayer) @ "  target: " @ getDebugString(%targetPlayer) @ "  message: " @ %message);
    if (isAIPlayerObject(%targetPlayer)) {
        %message.handleTalkedToNPC(NPCManager, %senderPlayer, %targetPlayer);
    }
    %conv = findConversation(%senderPlayer, %targetPlayer);
    if (!(isObject(%conv))) {
        error("could not find conversation.");
        return;
    }
    CONVBUB_DEBUG("found conv:" @ " " @ %conv);
    %senderPlayer.addParticipant(%conv);
    %message.addMessage(%conv, %senderPlayer);
    if (0 && (%conv.countParticipants() > 1.0) && (gGetField(%senderPlayer, orientedConversation) != %conv)) {
        700.orientTowardsOverTime(%senderPlayer, %conv);
        gSetField(%senderPlayer, orientedConversation, %conv);
    }
    return;
};
function serverCmdEavesdrop(%senderConnection, %newTarget) {
    CONVBUB_DEBUG("EAVESDROP: " @ %newTarget);
    if ((%newTarget != 0.0)) {
        %newTarget = %newTarget.resolveObjectFromGhostIndex(%senderConnection);
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
    if ((%targetPlayer != 0.0)) {
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
        if (!(%targetPlayer.hasParticipant(%targetConv))) {
            CONVBUB_DEBUG("serverSideEavesdrop:" @ " " @ getDebugString(%senderPlayer) @ " " @ "eavesdropping on somebody who ain't talking:" @ " " @ getDebugString(%targetPlayer));
            return;
        }
    }
    0.joinConversation(%senderPlayer, %targetConv);
    return;
};
function Player::joinConversation(%this, %conv, %asParticipant) {
    %oldConv = %this.getConversation();
    if ((%oldConv == %conv)) {
        CONVBUB_DEBUG("no change in conversation" @ " " @ getDebugString(%oldConv));
        return;
    }
    if (isObject(%oldConv)) {
        if (%this.hasListener(%oldConv)) {
            %this.removeListener(%oldConv);
        }
        if (%this.hasParticipant(%oldConv)) {
            %this.removeParticipant(%oldConv);
        }
        error(%this.getDebugString() @ " " @ "thinks it's in the wrong conversation:" @ " " @ getDebugString(%oldConv));
    }
    if (!(isObject(%conv))) {
        return;
    }
    if (%asParticipant) {
        %this.addParticipant(%conv);
        %tmp = "participant";
    }
    %this.addListener(%conv);
    %tmp = "listener";
    CONVBUB_DEBUG(getDebugString(%this) @ " " @ "joined" @ " " @ getDebugString(%conv) @ " " @ "as a" @ " " @ %tmp);
    %conv.setConversation(%this);
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
