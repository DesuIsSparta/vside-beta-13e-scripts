function serverCmdAdminAction(%senderConnection, %action, %target, %message) {
    if (!isObject(%senderConnection.Player)) {
        error("null player sending boot command:" @ " " @ %senderConnection);
        return;
    }
    if (!%senderConnection.Player.isStaff()) {
        error("non-staff player sending admin action:" @ " " @ %senderConnection.Player.getShapeName());
        return;
    }
    if ((%target != 0.0)) {
        %target = %senderConnection.resolveObjectFromGhostIndex(%target);
    }
    if (!admin::isActionable(%target, %action)) {
        error("Got invalid target/action in serverCmdAdminAction() - sender =" @ " " @ %senderConnection.Player.getShapeName() @ " " @ "action =" @ " " @ %action @ " " @ "target =" @ " " @ %target);
        return;
    }
    %adminName = %senderConnection.Player.getShapeName();
    %targetName = admin::getTargetName(%target);
    warn("AdminAction:" @ " " @ %adminName @ " " @ %action @ " " @ "object" @ " " @ %target @ " " @ %targetName);
    if ((%action $= "Boot")) {
        admin::doBoot(%target, %message, %senderConnection.Player);
    }
    if ((%action $= "Ban")) {
        admin::doBan(%target, %message, %senderConnection.Player);
    }
    if ((%action $= "Message")) {
        admin::doMessage(%target, %message, %senderConnection.Player);
    }
    if ((%action $= "Throw Voice")) {
        admin::doThrowVoice(%target, %message, %senderConnection.Player);
    }
    return;
};
function admin::doBoot(%target, %message, %adminPlayer) {
    %client = %target.getControllingClient();
    if ((%client == 0.0)) {
        %target.delete();
        return;
    }
    commandToClient(%client, 'beingBooted', %message);
    %client.schedule(1000, "delete", %message);
    return;
};
function BanRequest::onLine(%this, %line) {
    if (!(%line $= "success")) {
        error("ban failed:" @ " " @ %this.user);
    }
    return;
};
function admin::doBan(%target, %message, %adminPlayer) {
    %client = %target.getControllingClient();
    if ((%client == 0.0)) {
        %target.delete();
        return;
    }
    %banRequest = new CURLObject(BanRequest);
    %banRequest.user = %target.getShapeName();
    %host = $Pref::Server::ManagerAddress @ ":" @ $Pref::Server::ManagerHTTPPort;
    %uri = "/envmanager/status";
    %query = "cmd=ban";
    %userValue = "user=" @ urlEncode(%banRequest.user);
    %post = %userValue @ "&" @ "ban=true";
    %banRequest.post(%host, %uri, %query, %post);
    commandToClient(%client, 'beingBanned', %message);
    %client.schedule(1000, "delete", %message);
    return;
};
function admin::doMessage(%target, %message, %adminPlayer) {
    %adminName = %adminPlayer.getShapeName();
    %targetName = admin::getTargetName(%target);
    %msg = admin::composeSystemMessage(%target, %message, %adminPlayer);
    if ((%target == 0.0)) {
        messageAll('MsgSystemMessage', %msg);
    }
    %targetClient = %target.getControllingClient();
    if (!isObject(%targetClient)) {
        error("Attempting to message clientless target:" @ " " @ %target @ " " @ %targetName);
        return;
    }
    messageClient(%targetClient, 'MsgSystemMessage', %msg);
    return;
};
function admin::doThrowVoice(%target, %message, %adminPlayer) {
    %adminName = %adminPlayer.getShapeName();
    %targetName = admin::getTargetName(%target);
    %msg = %message;
    if ((%target == 0.0)) {
        NPCManager.doThrowVoice(%message, %adminPlayer);
    }
    ServersideChatMessage(%target, 0, %msg);
    return;
};
function NPCManager::doThrowVoice(%this, %msg, %adminPlayer) {
    if (!isObject(%this.NPCGroup)) {
        warn("No NPC group..");
        return;
    }
    %NPCNum = %this.NPCGroup.getCount();
    %n = 0;
    while ((%n < %NPCNum)) {
        ServersideChatMessage(%this.NPCGroup.getObject(%n), 0, %msg);
        %n = (%n + 1.0);
    }
};
