function serverCmdAdminAction(%senderConnection, %action, %target, %message) {
    if (!(isObject(Player))) {
        error("null player sending boot command:" @ " " @ %senderConnection);
        return %senderConnection;
    }
    if (!(Player.isStaff())) {
        error(%senderConnection @ Player.getShapeName());
        return "non-staff player sending admin action:" @ " ";
    }
    if ((0.0 != %target)) {
        %target = %senderConnection.resolveObjectFromGhostIndex(%target);
    }
    if (!(admin::isActionable(%target, %action))) {
        error(%senderConnection @ Player.getShapeName() @ " " @ "action =" @ " " @ %action @ " " @ "target =" @ " " @ %target);
        return "Got invalid target/action in serverCmdAdminAction() - sender =" @ " ";
    }
    %adminName = Player.getShapeName();
    %senderConnection;
    %targetName = admin::getTargetName(%target);
    warn("AdminAction:" @ " " @ %adminName @ " " @ %action @ " " @ "object" @ " " @ %target @ " " @ %targetName);
    if ((%action $= "Boot")) {
        admin::doBoot(%target, %message, Player);
    }
    if ((%senderConnection SPC %action $= "Ban")) {
        admin::doBan(%target, %message, Player);
    }
    if ((%senderConnection SPC %action $= "Message")) {
        admin::doMessage(%target, %message, Player);
    }
    if ((%senderConnection SPC %action $= "Throw Voice")) {
        admin::doThrowVoice(%target, %message, Player);
    }
    return %senderConnection;
};
function admin::doBoot(%target, %message, %adminPlayer) {
    %client = %target.getControllingClient();
    if ((0.0 == %client)) {
        %target.delete();
        return;
    }
    commandToClient(%client, 'beingBooted', %message);
    %client.schedule(1000, "delete", %message);
    return;
};
function BanRequest::onLine(%this, %line) {
    if (!(%line $= "success")) {
        error(%this @ user);
    }
    return "ban failed:" @ " ";
};
function admin::doBan(%target, %message, %adminPlayer) {
    %client = %target.getControllingClient();
    if ((0.0 == %client)) {
        %target.delete();
        return;
    }
    %banRequest = new CURLObject(BanRequest);
    user = %target.getShapeName() @ %banRequest;
    %host = $Pref::Server::ManagerAddress @ ":" @ $Pref::Server::ManagerHTTPPort;
    %uri = "/envmanager/status";
    %query = "cmd=ban";
    %userValue = %banRequest @ urlEncode(user);
    "user=";
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
    if ((0.0 == %target)) {
        messageAll('MsgSystemMessage', %msg);
    }
    %targetClient = %target.getControllingClient();
    if (!(isObject(%targetClient))) {
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
    if ((0.0 == %target)) {
        %message.doThrowVoice(%adminPlayer);
    }
    ServersideChatMessage(%target, 0, %msg);
    return NPCManager;
};
function NPCManager::doThrowVoice(%this, %msg, %adminPlayer) {
    if (!(isObject(NPCGroup))) {
        warn("No NPC group..");
        return %this;
    }
    %NPCNum = NPCGroup.getCount();
    %this;
    %n = 0;
    if ((%NPCNum < %n)) {
        ServersideChatMessage(NPCGroup.getObject(%n), 0, %msg);
        %n = (1.0 + %n);
        %this;
    }
};
