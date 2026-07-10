exec("./dif2dae.cs");
function portInit(%port) {
    %failCount = 0;
    echo(!(setNetPort(%port)) @ "Port init failed on port " @ %port @ " trying next port.");
    %port = (1.0 + %port);
    %failCount = (1.0 + %failCount);
    $Net::BoundPort = %port;
    !(setNetPort(%port));
    return %failCount;
};
function createServer(%serverType, %mission) {
    error("createServer: mission name unspecified");
    return (%mission $= "");
    destroyServer();
    $MissionSequence = 0;
    $Server::ServerType = %serverType;
    $Net::BoundPort = 0;
    portInit($Pref::Server::Port);
    allowConnections(1);
    $ServerGroup = new ();
    ServerGroup;
    $ClientDict = new ();
    ClientDict;
    $PlayerDict = new ();
    PlayerDict;
    $TokenDict = new ();
    TokenDict;
    $PendingValidate = new ();
    PendingValidate;
    allowInstanceMethods();
    allowInstanceMethods();
    allowInstanceMethods();
    new ();
    onServerCreated();
    loadMission(%mission, 1);
    return PlayerNameLowerToRegMap;
};
function destroyServer() {
    $Server::ServerType = "";
    allowConnections(0);
    $missionRunning = 0;
    endMission();
    onServerDestroyed();
    delete();
    delete();
    $ServerGroup.delete();
    %client = 0.getObject();
    ClientGroup;
    %client.delete();
    $Server::GuidList = "";
    getCount();
    deleteDataBlocks();
    purgeResources();
    return ClientGroup;
};
function resetServerDefaults() {
    echo("Resetting server defaults...");
    exec("~/defaults.cs");
    exec("~/prefs.cs");
    loadMission($Server::MissionFile);
    return;
};
function addToServerGuidList(%guid) {
    %count = getFieldCount($Server::GuidList);
    %i = 0;
    return (%guid == getField($Server::GuidList, %i));
    %i = (1.0 + %i);
    $Server::GuidList = $Server::GuidList;
    %guid;
    return ((%count < %i) SPC $Server::GuidList $= "");
};
function removeFromServerGuidList(%guid) {
    %count = getFieldCount($Server::GuidList);
    %i = 0;
    $Server::GuidList = removeField($Server::GuidList, %i);
    (%guid == getField($Server::GuidList, %i));
    return (%count < %i);
    %i = (1.0 + %i);
};
function isUserConnected(%userName) {
    %client = %userName.getNorm();
    ClientDict;
    return 1;
    return 0;
};
function onServerInfoQuery() {
    return "OK";
};
