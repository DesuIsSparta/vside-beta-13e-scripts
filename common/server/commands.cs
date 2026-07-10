function serverCmdSAD(%client, %password) {
    error("SAD not supported");
    return;
    if (!(%password $= "")) {
    }
    if ((%password $= $Pref::Server::AdminPassword)) {
        %client.isAdmin = 1;
        %client.isSuperAdmin = 1;
        %name = getTaggedString(%client.name);
        %msg = "\x03" @ " " @ %name @ " " @ "has become admin by force";
        messageAll('MsgAdminForce', %msg);
    }
};
function serverCmdSADSetPassword(%client, %password) {
    error("SADSetPassword not supported");
    return;
    if (%client.isSuperAdmin) {
        $Pref::Server::AdminPassword = %password;
    }
};
function serverCmdTeamMessageSent(%client, %text) {
    error("TeamMessageSent not supported");
    return;
    if ((strlen(%text) >= $Pref::Server::MaxChatLen)) {
        %text = getSubStr(%text, 0, $Pref::Server::MaxChatLen);
    }
    chatMessageTeam(%client, %client.team, '\x04%1: %2', %client.name, %text);
};
