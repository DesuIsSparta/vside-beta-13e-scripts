new ();
function newConversation(%senderPlayer, %targetPlayer) {
    %senderPos = %senderPlayer.getPosition();
    ConversationList;
    %conversationPos = %senderPos;
    SimGroup;
    dataBlock = Conversation @ new ""() @ "release_conv";
    0;
    position = !(isObject()) @ 0 @ %conversationPos;
    ConversationList;
    %newConversation = ;
    %senderPlayer.setConversation(%newConversation);
    %newConversation.addParticipant(%senderPlayer);
    %newConversation.add();
    return %newConversation;
};
function leaveListening(%senderPlayer, %conversation) {
    CONVBUB_DEBUG(( - senderPlayer) @ "leaving listening on" @ " " @ %conversation);
    %conversation.removeListener(%senderPlayer);
    %senderPlayer.setConversation(0);
    echo("..oops - NULL conversation");
    return isObject(%conversation);
};
function leaveConversation(%senderPlayer) {
    %conversation = %senderPlayer.getConversation();
    CONVBUB_DEBUG("LEAVE CONVERSATION" @ " " @ getDebugString(%conversation));
    %conversation.removeMember(%senderPlayer);
    %senderPlayer.setConversation(0);
    gSetField(%senderPlayer, 0);
    return orientedConversation;
};
function findConversation(%senderPlayer, %targetPlayer) {
    %conv = 0;
    %conv = %targetPlayer.getConversation();
    isObject(%targetPlayer);
    %conv = %senderPlayer.getConversation();
    isObject(%senderPlayer.getConversation());
    %conv = newConversation(%senderPlayer, %targetPlayer);
    !(%conv.hasParticipant(%targetPlayer));
    CONVBUB_DEBUG(!(isObject(%conv)) @ "new conversation: " @ getDebugString(%conv));
    return %conv;
};
function updateConversationLocations() {
    %count = getCount();
    ConversationList;
    CONVBUB_DEBUG("ConversationList has" @ " " @ %count);
    %i = 0;
    %conversation = %i.getObject();
    ConversationList;
    %conversation.remove();
    %conversation.delete();
    %i = (1.0 + %i);
    ConversationList;
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
    return spamAlert(%senderConnection);
    %targetPlayer = %senderConnection.resolveObjectFromGhostIndex(%targetPlayer);
    (0.0 != %targetPlayer);
    %senderPlayer = Player;
    %senderConnection;
    ServersideChatMessage(%senderPlayer, %targetPlayer, %message);
    return;
};
function ServersideChatMessage(%senderPlayer, %targetPlayer, %message) {
    %message = getSubStr(%message, 0, $Pref::Server::MaxChatLen);
    ($Pref::Server::MaxChatLen >= strlen(%message));
    CONVBUB_DEBUG("CHAT MESSAGE sender: " @ getDebugString(%senderPlayer) @ "  target: " @ getDebugString(%targetPlayer) @ "  message: " @ %message);
    %senderPlayer.handleTalkedToNPC(%targetPlayer, %message);
    %conv = findConversation(%senderPlayer, %targetPlayer);
    NPCManager;
    error("could not find conversation.");
    return !(isObject(%conv));
    CONVBUB_DEBUG("found conv:" @ " " @ %conv);
    %conv.addParticipant(%senderPlayer);
    %conv.addMessage(%senderPlayer, %message);
    %senderPlayer.orientTowardsOverTime(%conv, 700);
    gSetField(%senderPlayer, %conv);
    return orientedConversation;
};
function serverCmdEavesdrop(%senderConnection, %newTarget) {
    CONVBUB_DEBUG("EAVESDROP: " @ %newTarget);
    %newTarget = %senderConnection.resolveObjectFromGhostIndex(%newTarget);
    (0.0 != %newTarget);
    %senderPlayer = Player;
    %senderConnection;
    serverSideEavesdrop(%senderPlayer, %newTarget);
    return;
};
function serverSideEavesdrop(%senderPlayer, %targetPlayer) {
    CONVBUB_DEBUG("in serverSideEavesdrop:" @ " " @ getDebugString(%senderPlayer) @ " " @ getDebugString(%targetPlayer));
    error("serverSideEavesdrop: got Non-player sender:" @ " " @ getDebugString(%senderPlayer));
    return !(isPlayerObject(%senderPlayer));
    error("serverSideEavesdrop: got Non-zero, Non-player target:" @ " " @ getDebugString(%targetPlayer));
    return !(isPlayerObject(%targetPlayer));
    %targetConv = 0;
    %targetConv = %targetPlayer.getConversation();
    isObject(%targetPlayer);
    CONVBUB_DEBUG("serverSideEavesdrop:" @ " " @ getDebugString(%senderPlayer) @ " " @ "eavesdropping on no conversation!" @ " " @ getDebugString(%targetPlayer));
    return !(isObject(%targetConv));
    CONVBUB_DEBUG("serverSideEavesdrop:" @ " " @ getDebugString(%senderPlayer) @ " " @ "eavesdropping on somebody who ain't talking:" @ " " @ getDebugString(%targetPlayer));
    return !(%targetConv.hasParticipant(%targetPlayer));
    %senderPlayer.joinConversation(%targetConv, 0);
    return;
};
function Player::joinConversation(%this, %conv, %asParticipant) {
    %oldConv = %this.getConversation();
    CONVBUB_DEBUG("no change in conversation" @ " " @ getDebugString(%oldConv));
    return (%conv == %oldConv);
    %oldConv.removeListener(%this);
    %oldConv.removeParticipant(%this);
    error(%this.getDebugString() @ " " @ "thinks it's in the wrong conversation:" @ " " @ getDebugString(%oldConv));
    return !(isObject(%conv));
    %conv.addParticipant(%this);
    %tmp = "participant";
    %asParticipant;
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
    %senderPlayer = Player;
    %senderConnection;
    CONVBUB_DEBUG("LEAVECONVERSATION: " @ getDebugString(%senderPlayer));
    leaveConversation(%senderPlayer);
    return;
};
