function serverStart()
{
    %initRequest = new CURLObject(InitRequest);
    %host = $Pref::Server::ManagerAddress @ ":" @ $Pref::Server::ManagerHTTPPort;
    %uri = "/envmanager/status";
    %query = "cmd=start";
    %bindAddress = "address=" @ urlEncode($Pref::Net::BindAddress);
    %bindPort = "port=" @ urlEncode($BoundPort);
    %name = "name=" @ urlEncode($Pref::Server::Name);
    %location = strreplace($Pref::Net::Location, ",", " ");
    if (%location $= "")
    {
        %location = generateRandomMapLocation();
    }
    %location = "location=" @ urlEncode(%location);
    %description = "description=" @ urlEncode($Pref::Server::Info);
    %capacity = "capacity=" @ urlEncode($Pref::Server::MaxPlayers);
    %version = "version=" @ urlEncode(getProtocolVersion());
    %post = %bindPort @ "&" @ %name @ "&" @ %location @ "&" @ %description @ "&" @ %capacity @ "&" @ %version;
    echo("sending server start to: " @ %host);
    %initRequest.post(%host, %uri, %query, %post);
    schedule(7500, 0, "serverHeartBeat");
    return ;
}
function serverHeartBeat()
{
    if ($StandAlone)
    {
        error("StandAlone - turning off serverHeartBeat.");
        return ;
    }
    %initRequest = new CURLObject(InitRequest);
    %host = $Pref::Server::ManagerAddress @ ":" @ $Pref::Server::ManagerHTTPPort;
    %uri = "/envmanager/status";
    %query = "cmd=heartbeat";
    %bindAddress = "address=" @ urlEncode($Pref::Net::BindAddress);
    %bindPort = "port=" @ urlEncode($Pref::Server::Port);
    %name = "name=" @ urlEncode($Pref::Server::Name);
    %location = strreplace($Pref::Net::Location, ",", " ");
    if (%location $= "")
    {
        %location = generateRandomMapLocation();
    }
    %location = "location=" @ urlEncode(%location);
    %description = "description=" @ urlEncode($Pref::Server::Info);
    %capacity = "capacity=" @ urlEncode($Pref::Server::MaxPlayers);
    %version = "version=" @ urlEncode(getProtocolVersion());
    %load = "load=" @ urlEncode(ClientGroup.getCount());
    %users = "users=";
    %i = 0;
    while (%i < %count)
    {
        if (%i > 0)
        {
            %users = %users @ ",";
        }
        %client = ClientGroup.getObject(%i);
        %users = %users @ urlEncode(%client.nameBase);
        %i = %i + 1;
    }
    %post = %bindPort @ "&" @ %name @ "&" @ %location @ "&" @ %description @ "&" @ %capacity @ "&" @ %version @ "&" @ %load @ "&" @ %users;
    echo("sending server heartbeat to: " @ %host);
    %initRequest.post(%host, %uri, %query, %post);
    schedule(7500, 0, "serverHeartBeat");
    return ;
}
function generateRandomMapLocation()
{
    %x = (getRandom() * 0.6) + 0.2;
    %y = (getRandom() * 0.6) + 0.2;
    $Pref::Net::Location = %x SPC %y;
    return $Pref::Net::Location;
}
function InitRequest::onStatus(%unused, %status)
{
    if (%status != 200)
    {
        error("heartbeat HTTP status: " @ %status);
    }
    return ;
}
function InitRequest::onConnected(%unused)
{
    return ;
}
function InitRequest::onConnectFailed(%unused)
{
    %host = $Pref::Server::ManagerAddress @ ":" @ $Pref::Server::ManagerHTTPPort;
    echo("Cannot reach Server Manager (" @ %host @ ")");
    return ;
}
function InitRequest::onLine(%unused, %line)
{
    %line = NextToken(%line, name, "=");
    %line = NextToken(%line, value, "=");
    if (%name $= "boot")
    {
        %connection = ClientDict.get(%value);
        if (%connection != 0)
        {
            echo("received boot for player " @ %connection.nameBase);
            %connection.delete("You have connected in another location.");
        }
    }
    return ;
}
function InitRequest::onDNSResolved(%unused)
{
    return ;
}
function InitRequest::onDNSFailed(%unused)
{
    echo("Cannot resolve Manager Host (" @ $Pref::Server::ManagerAddress @ ")");
    return ;
}
function InitRequest::onDisconnect(%unused)
{
    return ;
}
