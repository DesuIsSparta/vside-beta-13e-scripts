exec("./dif2dae.cs");
function portInit(%port)
{
    %failCount = 0;
    while (!setNetPort(%port))
    {
        echo("Port init failed on port " @ %port @ " trying next port.");
        %port = %port + 1;
        %failCount = %failCount + 1;
    }
    $Net::BoundPort = %port;
    return %failCount;
}
function createServer(%serverType, %mission)
{
    if (%mission $= "")
    {
        error("createServer: mission name unspecified");
        return ;
    }
    destroyServer();
    $MissionSequence = 0;
    $Server::ServerType = %serverType;
    $Net::BoundPort = 0;
    if (%serverType $= "MultiPlayer")
    {
        portInit($Pref::Server::Port);
        allowConnections(1);
    }
    $ServerGroup = new SimGroup(ServerGroup);
    $ClientDict = new StringMap(ClientDict);
    $PlayerDict = new StringMap(PlayerDict);
    $TokenDict = new StringMap(TokenDict);
    $PendingValidate = new StringMap(PendingValidate);
    ClientDict.allowInstanceMethods();
    PlayerDict.allowInstanceMethods();
    TokenDict.allowInstanceMethods();
    new StringMap(PlayerNameLowerToRegMap);
    onServerCreated();
    loadMission(%mission, 1);
    return ;
}
function destroyServer()
{
    $Server::ServerType = "";
    allowConnections(0);
    $missionRunning = 0;
    endMission();
    onServerDestroyed();
    if (isObject(MissionGroup))
    {
        MissionGroup.delete();
    }
    if (isObject(MissionCleanup))
    {
        MissionCleanup.delete();
    }
    if (isObject($ServerGroup))
    {
        $ServerGroup.delete();
    }
    while (ClientGroup.getCount())
    {
        %client = ClientGroup.getObject(0);
        %client.delete();
    }
    $Server::GuidList = "";
    deleteDataBlocks();
    purgeResources();
    return ;
}
function resetServerDefaults()
{
    echo("Resetting server defaults...");
    exec("~/defaults.cs");
    exec("~/prefs.cs");
    loadMission($Server::MissionFile);
    return ;
}
function addToServerGuidList(%guid)
{
    %count = getFieldCount($Server::GuidList);
    %i = 0;
    while (%i < %count)
    {
        if (getField($Server::GuidList, %i) == %guid)
        {
            return ;
        }
        %i = %i + 1;
    }
    $Server::GuidList = $Server::GuidList $= "" ? %guid : $Server::GuidList;
    return ;
}
function removeFromServerGuidList(%guid)
{
    %count = getFieldCount($Server::GuidList);
    %i = 0;
    while (%i < %count)
    {
        if (getField($Server::GuidList, %i) == %guid)
        {
            $Server::GuidList = removeField($Server::GuidList, %i);
            return ;
        }
        %i = %i + 1;
    }
}

function isUserConnected(%userName)
{
    %client = ClientDict.getNorm(%userName);
    if (!(%client $= ""))
    {
        return 1;
    }
    return 0;
}
function onServerInfoQuery()
{
    return "OK";
}
