function serverCmdAdminAction(%senderConnection, %action, %target, %message)
{
    if (!isObject(%senderConnection.Player))
    {
        error("null player sending boot command:" SPC %senderConnection);
        return ;
    }
    if (!%senderConnection.Player.isStaff())
    {
        error("non-staff player sending admin action:" SPC %senderConnection.Player.getShapeName());
        return ;
    }
    if (%target != 0)
    {
        %target = %senderConnection.resolveObjectFromGhostIndex(%target);
    }
    if (!admin::isActionable(%target, %action))
    {
        error("Got invalid target/action in serverCmdAdminAction() - sender =" SPC %senderConnection.Player.getShapeName() SPC "action =" SPC %action SPC "target =" SPC %target);
        return ;
    }
    %adminName = %senderConnection.Player.getShapeName();
    %targetName = admin::getTargetName(%target);
    warn("AdminAction:" SPC %adminName SPC %action SPC "object" SPC %target SPC %targetName);
    if (%action $= "Boot")
    {
        admin::doBoot(%target, %message, %senderConnection.Player);
    }
    else
    {
        if (%action $= "Ban")
        {
            admin::doBan(%target, %message, %senderConnection.Player);
        }
        else
        {
            if (%action $= "Message")
            {
                admin::doMessage(%target, %message, %senderConnection.Player);
            }
            else
            {
                if (%action $= "Throw Voice")
                {
                    admin::doThrowVoice(%target, %message, %senderConnection.Player);
                }
            }
        }
    }
    return ;
}
function admin::doBoot(%target, %message, %adminPlayer)
{
    %client = %target.getControllingClient();
    if (%client == 0)
    {
        %target.delete();
        return ;
    }
    commandToClient(%client, 'beingBooted', %message);
    %client.schedule(1000, "delete", %message);
    return ;
}
function BanRequest::onLine(%this, %line)
{
    if (!(%line $= "success"))
    {
        error("ban failed:" SPC %this.user);
    }
    return ;
}
function admin::doBan(%target, %message, %adminPlayer)
{
    %client = %target.getControllingClient();
    if (%client == 0)
    {
        %target.delete();
        return ;
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
    return ;
}
function admin::doMessage(%target, %message, %adminPlayer)
{
    %adminName = %adminPlayer.getShapeName();
    %targetName = admin::getTargetName(%target);
    %msg = admin::composeSystemMessage(%target, %message, %adminPlayer);
    if (%target == 0)
    {
        messageAll('MsgSystemMessage', %msg);
    }
    else
    {
        %targetClient = %target.getControllingClient();
        if (!isObject(%targetClient))
        {
            error("Attempting to message clientless target:" SPC %target SPC %targetName);
            return ;
        }
        messageClient(%targetClient, 'MsgSystemMessage', %msg);
    }
    return ;
}
function admin::doThrowVoice(%target, %message, %adminPlayer)
{
    %adminName = %adminPlayer.getShapeName();
    %targetName = admin::getTargetName(%target);
    %msg = %message;
    if (%target == 0)
    {
        NPCManager.doThrowVoice(%message, %adminPlayer);
    }
    else
    {
        ServersideChatMessage(%target, 0, %msg);
    }
    return ;
}
function NPCManager::doThrowVoice(%this, %msg, %adminPlayer)
{
    if (!isObject(%this.NPCGroup))
    {
        warn("No NPC group..");
        return ;
    }
    %NPCNum = %this.NPCGroup.getCount();
    %n = 0;
    while (%n < %NPCNum)
    {
        ServersideChatMessage(%this.NPCGroup.getObject(%n), 0, %msg);
        %n = %n + 1;
    }
}


