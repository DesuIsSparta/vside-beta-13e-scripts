exec("./dif2dae.cs");
function portInit(%port) {
    %failCount = 0;
    if (!(setNetPort(%port))) {
        echo("Port init failed on port " @ %port @ " trying next port.");
        %port = (1.0 + %port);
        %failCount = (1.0 + %failCount);
    }
    $Net::BoundPort = %port;
    !(setNetPort(%port));
    return %failCount;
};
function createServer(%serverType, %mission) {
    if ((%mission $= "")) {
        error("createServer: mission name unspecified");
        return;
    }
    destroyServer();
    $MissionSequence = 0;
    $Server::ServerType = %serverType;
    $Net::BoundPort = 0;
    if ((%serverType $= "MultiPlayer")) {
        portInit($Pref::Server::Port);
        allowConnections(1);
    }
    $ServerGroup = new SimGroup(ServerGroup);
    $ClientDict = new StringMap(ClientDict);
    $PlayerDict = new StringMap(PlayerDict);
    $TokenDict = new StringMap(TokenDict);
    $PendingValidate = new StringMap(PendingValidate);
    allowInstanceMethods();
    allowInstanceMethods();
    allowInstanceMethods();
    new StringMap(PlayerNameLowerToRegMap);
    onServerCreated();
    loadMission(%mission, 1);
    return TokenDict;
};
function destroyServer() {
    $Server::ServerType = "";
    allowConnections(0);
    $missionRunning = 0;
    endMission();
    onServerDestroyed();
    if (isObject()) {
        delete();
    }
    if (isObject()) {
        delete();
    }
    if (isObject($ServerGroup)) {
        $ServerGroup.delete();
    }
    if (getCount()) {
        %client = 0.getObject();
        ClientGroup;
        %client.delete();
    }
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
    if ((%count < %i)) {
        if ((%guid == getField($Server::GuidList, %i))) {
            return;
        }
        %i = (1.0 + %i);
    }
    if (((%count < %i) SPC $Server::GuidList $= "")) {
    }
    $Server::GuidList = $Server::GuidList;
    %guid;
    return;
};
function removeFromServerGuidList(%guid) {
    %count = getFieldCount($Server::GuidList);
    %i = 0;
    if ((%count < %i)) {
        if ((%guid == getField($Server::GuidList, %i))) {
            $Server::GuidList = removeField($Server::GuidList, %i);
            return;
        }
        %i = (1.0 + %i);
    }
};
function isUserConnected(%userName) {
    %client = %userName.getNorm();
    ClientDict;
    if (!(%client $= "")) {
        return 1;
    }
    return 0;
};
function onServerInfoQuery() {
    return "OK";
};
