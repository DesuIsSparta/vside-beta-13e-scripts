function serverCmdChangeRelation(%client, %other, %relType, %oper)
{
    doLocalChangeRelation(%client, %other, %relType, %oper);
    %relRequest = new CURLObject(RelRequest);
    %host = $Pref::Server::ManagerAddress @ ":" @ $Pref::Server::ManagerHTTPPort;
    %uri = "/envmanager/status";
    %query = "cmd=relate";
    %user = %client.nameBase;
    %userId = PlayerDict.get(%user);
    %otherId = PlayerDict.get(%other);
    if (%userId == 0)
    {
        return ;
    }
    RelRequest.userId = %userId;
    RelRequest.otherId = %otherId;
    RelRequest.relType = %relType;
    RelRequest.oper = %oper;
    %userValue = "user=" @ urlEncode(%user);
    %otherValue = "other=" @ urlEncode(%other);
    %relTypeValue = "type=" @ urlEncode(%relType);
    %operTypeValue = "op=" @ urlEncode(%oper);
    %post = %userValue @ "&" @ %otherValue @ "&" @ %relTypeValue @ "&" @ %operTypeValue;
    %relRequest.post(%host, %uri, %query, %post);
    return ;
}
function RelRequest::onConnected(%this)
{
    return ;
}
function RelRequest::onConnectFailed(%this)
{
    %host = $Pref::Server::ManagerAddress @ ":" @ $Pref::Server::ManagerHTTPPort;
    echo("Cannot reach Server Manager (" @ %host @ ")");
    return ;
}
function RelRequest::onLine(%this, %line)
{
    if (%line $= "success")
    {
        changeRelation(%this.userId, %this.otherId, %this.relType, %this.oper);
    }
    else
    {
        if (%line $= "fail")
        {
        }
    }
    return ;
}
function RelRequest::onDNSResolved(%this)
{
    return ;
}
function RelRequest::onDNSFailed(%this)
{
    echo("Cannot resolve Manager Host (" @ $Pref::Server::ManagerAddress @ ")");
    return ;
}
function RelRequest::onDisconnect(%this)
{
    return ;
}
function doLocalChangeRelation(%client, %other, %relType, %oper)
{
    %sender = %client.Player;
    %otherId = PlayerDict.get(%other);
    if (!isPlayerObject(%otherId))
    {
        error("bad other in doLocalChangeRelation:" SPC getDebugString(%otherId) SPC "from" SPC getDebugString(%sender));
        return ;
    }
    if (%oper $= "add")
    {
        %opCode = 0;
    }
    else
    {
        %opCode = 1;
    }
    changeRelation(%sender, %otherId, %relType, %opCode);
    return ;
}
