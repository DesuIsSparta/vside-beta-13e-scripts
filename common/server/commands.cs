function serverCmdSAD(%client, %password) {
    error("SAD not supported");
    return;
    if (!(%password $= "")) {
    }
    if ((%password $= $Pref::Server::AdminPassword)) {
        isAdmin = 1 @ %client;
        isSuperAdmin = 1 @ %client;
        %name = getTaggedString(name);
        %client;
        %msg = "\x03" @ " " @ %name @ " " @ "has become admin by force";
        messageAll('MsgAdminForce', %msg);
    }
};
function serverCmdSADSetPassword(%client, %password) {
    error("SADSetPassword not supported");
    return;
    if (isSuperAdmin) {
        $Pref::Server::AdminPassword = %password;
        %client;
    }
};
function serverCmdTeamMessageSent(%client, %text) {
    error("TeamMessageSent not supported");
    return;
    if (($Pref::Server::MaxChatLen >= strlen(%text))) {
        %text = getSubStr(%text, 0, $Pref::Server::MaxChatLen);
    }
    chatMessageTeam(%client, team, '\x04%1: %2', name, %text);
};
