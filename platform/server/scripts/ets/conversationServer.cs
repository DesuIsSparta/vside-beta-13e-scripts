if (!isObject(ConversationList)) {
    new SimGroup(ConversationList);
}
function newConversation(%senderPlayer, %targetPlayer) {
    %senderPos = %senderPlayer.getPosition();
    %conversationPos = %senderPos;
    %newConversation = new Conversation("") {
        dataBlock = "release_conv";
        position = %conversationPos;
    };
    %senderPlayer.setConversation(%newConversation);
    %newConversation.addParticipant(%senderPlayer);
    ConversationList.add(%newConversation);
    return %newConversation;
};
function leaveListening(%senderPlayer, %conversation) {
    CONVBUB_DEBUG(senderPlayer @ " " @ "leaving listening on" @ " " @ %conversation);
    if (isObject(%conversation)) {
        %conversation.removeListener(%senderPlayer);
        %senderPlayer.setConversation(0);
    } else {
        echo("..oops - NULL conversation");
    }
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
    if (!isObject(%conv)) {
    }
    if (!%conv.hasParticipant(%targetPlayer)) {
        if (isObject(%senderPlayer.getConversation())) {
            %conv = %senderPlayer.getConversation();
        } else {
            %conv = newConversation(%senderPlayer, %targetPlayer);
            CONVBUB_DEBUG("new conversation: " @ getDebugString(%conv));
        }
    }
    return %conv;
};
function updateConversationLocations() {
    %count = ConversationList.getCount();
    CONVBUB_DEBUG("ConversationList has" @ " " @ %count);
    %i = 0;
    while ((%i < %count)) {
        %conversation = ConversationList.getObject(%i);
        if (!%conversation.updateLocation()) {
            ConversationList.remove(%conversation);
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
        %targetPlayer = %senderConnection.resolveObjectFromGhostIndex(%targetPlayer);
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
        NPCManager.handleTalkedToNPC(%senderPlayer, %targetPlayer, %message);
    }
    %conv = findConversation(%senderPlayer, %targetPlayer);
    if (!isObject(%conv)) {
        error("could not find conversation.");
        return;
    }
    CONVBUB_DEBUG("found conv:" @ " " @ %conv);
    %conv.addParticipant(%senderPlayer);
    %conv.addMessage(%senderPlayer, %message);
    if (0 && (%conv.countParticipants() > 1.0) && (gGetField(%senderPlayer, orientedConversation) != %conv)) {
        %senderPlayer.orientTowardsOverTime(%conv, 700);
        gSetField(%senderPlayer, orientedConversation, %conv);
    }
    return;
};
function serverCmdEavesdrop(%senderConnection, %newTarget) {
    CONVBUB_DEBUG("EAVESDROP: " @ %newTarget);
    if ((%newTarget != 0.0)) {
        %newTarget = %senderConnection.resolveObjectFromGhostIndex(%newTarget);
    }
    %senderPlayer = %senderConnection.Player;
    serverSideEavesdrop(%senderPlayer, %newTarget);
    return;
};
function serverSideEavesdrop(%senderPlayer, %targetPlayer) {
    CONVBUB_DEBUG("in serverSideEavesdrop:" @ " " @ getDebugString(%senderPlayer) @ " " @ getDebugString(%targetPlayer));
    if (!isPlayerObject(%senderPlayer)) {
        error("serverSideEavesdrop: got Non-player sender:" @ " " @ getDebugString(%senderPlayer));
        return;
    }
    if ((%targetPlayer != 0.0)) {
    }
    if (!isPlayerObject(%targetPlayer)) {
        error("serverSideEavesdrop: got Non-zero, Non-player target:" @ " " @ getDebugString(%targetPlayer));
        return;
    }
    %targetConv = 0;
    if (isObject(%targetPlayer)) {
        %targetConv = %targetPlayer.getConversation();
        if (!isObject(%targetConv)) {
            CONVBUB_DEBUG("serverSideEavesdrop:" @ " " @ getDebugString(%senderPlayer) @ " " @ "eavesdropping on no conversation!" @ " " @ getDebugString(%targetPlayer));
            return;
        }
        if (!%targetConv.hasParticipant(%targetPlayer)) {
            CONVBUB_DEBUG("serverSideEavesdrop:" @ " " @ getDebugString(%senderPlayer) @ " " @ "eavesdropping on somebody who ain't talking:" @ " " @ getDebugString(%targetPlayer));
            return;
        }
    }
    %senderPlayer.joinConversation(%targetConv, 0);
    return;
};
function Player::joinConversation(%this, %conv, %asParticipant) {
    %oldConv = %this.getConversation();
    if ((%oldConv == %conv)) {
        CONVBUB_DEBUG("no change in conversation" @ " " @ getDebugString(%oldConv));
        return;
    }
    if (isObject(%oldConv)) {
        if (%oldConv.hasListener(%this)) {
            %oldConv.removeListener(%this);
        } else {
            if (%oldConv.hasParticipant(%this)) {
                %oldConv.removeParticipant(%this);
            } else {
                error(%this.getDebugString() @ " " @ "thinks it's in the wrong conversation:" @ " " @ getDebugString(%oldConv));
            }
        }
    }
    if (!isObject(%conv)) {
        return;
    }
    if (%asParticipant) {
        %conv.addParticipant(%this);
        %tmp = "participant";
    } else {
        %conv.addListener(%this);
        %tmp = "listener";
    }
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
