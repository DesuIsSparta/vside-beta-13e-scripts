function serverCmdSAD(%client, %password)
{
    error("SAD not supported");
    return ;
    if (!((%password $= "")) && (%password $= $Pref::Server::AdminPassword))
    {
        %client.isAdmin = 1;
        %client.isSuperAdmin = 1;
        %name = getTaggedString(%client.name);
        %msg = "\c2" SPC %name SPC "has become admin by force";
        messageAll('MsgAdminForce', %msg);
    }
    return ;
}
function serverCmdSADSetPassword(%client, %password)
{
    error("SADSetPassword not supported");
    return ;
    if (%client.isSuperAdmin)
    {
        $Pref::Server::AdminPassword = %password;
    }
    return ;
}
function serverCmdTeamMessageSent(%client, %text)
{
    error("TeamMessageSent not supported");
    return ;
    if (strlen(%text) >= $Pref::Server::MaxChatLen)
    {
        %text = getSubStr(%text, 0, $Pref::Server::MaxChatLen);
    }
    chatMessageTeam(%client, %client.team, '\c3%1: %2', %client.name, %text);
    return ;
}
