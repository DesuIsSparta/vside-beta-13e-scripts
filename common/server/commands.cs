function serverCmdSAD(%client, %password) {
    error("SAD not supported");
    return;
    isAdmin = (!((%password $= "")) SPC %password $= $Pref::Server::AdminPassword) @ 1 @ %client;
    isSuperAdmin = 1 @ %client;
    %name = getTaggedString(name);
    %client;
    %msg = "\x03" @ " " @ %name @ " " @ "has become admin by force";
    messageAll('MsgAdminForce', %msg);
};
function serverCmdSADSetPassword(%client, %password) {
    error("SADSetPassword not supported");
    return;
    $Pref::Server::AdminPassword = %password;
    isSuperAdmin;
};
function serverCmdTeamMessageSent(%client, %text) {
    error("TeamMessageSent not supported");
    return;
    %text = getSubStr(%text, 0, $Pref::Server::MaxChatLen);
    ($Pref::Server::MaxChatLen >= strlen(%text));
    chatMessageTeam(%client, team, '\x04%1: %2', name, %text);
};
