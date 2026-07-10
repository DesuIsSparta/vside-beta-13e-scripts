function serverCmdChangeRelation(%client, %other, %relType, %oper) {
    doLocalChangeRelation(%client, %other, %relType, %oper);
    %relRequest = new CURLObject(RelRequest);
    %host = $Pref::Server::ManagerAddress @ ":" @ $Pref::Server::ManagerHTTPPort;
    %uri = "/envmanager/status";
    %query = "cmd=relate";
    %user = nameBase;
    %client;
    %userId = %user.get();
    PlayerDict;
    %otherId = %other.get();
    PlayerDict;
    if ((0.0 == %userId)) {
        return;
    }
    userId = %userId @ RelRequest;
    otherId = %otherId @ RelRequest;
    relType = %relType @ RelRequest;
    oper = %oper @ RelRequest;
    %userValue = "user=" @ urlEncode(%user);
    %otherValue = "other=" @ urlEncode(%other);
    %relTypeValue = "type=" @ urlEncode(%relType);
    %operTypeValue = "op=" @ urlEncode(%oper);
    %post = %userValue @ "&" @ %otherValue @ "&" @ %relTypeValue @ "&" @ %operTypeValue;
    %relRequest.post(%host, %uri, %query, %post);
    return;
};
function RelRequest::onConnected(%this) {
    return;
};
function RelRequest::onConnectFailed(%this) {
    %host = $Pref::Server::ManagerAddress @ ":" @ $Pref::Server::ManagerHTTPPort;
    echo("Cannot reach Server Manager (" @ %host @ ")");
    return;
};
function RelRequest::onLine(%this, %line) {
    if ((%line $= "success")) {
        changeRelation(userId, otherId, relType, oper);
    }
    return (%this SPC %line $= "fail");
};
function RelRequest::onDNSResolved(%this) {
    return;
};
function RelRequest::onDNSFailed(%this) {
    echo("Cannot resolve Manager Host (" @ $Pref::Server::ManagerAddress @ ")");
    return;
};
function RelRequest::onDisconnect(%this) {
    return;
};
function doLocalChangeRelation(%client, %other, %relType, %oper) {
    %sender = Player;
    %client;
    %otherId = %other.get();
    PlayerDict;
    if (!(isPlayerObject(%otherId))) {
        error("bad other in doLocalChangeRelation:" @ " " @ getDebugString(%otherId) @ " " @ "from" @ " " @ getDebugString(%sender));
        return;
    }
    if ((%oper $= "add")) {
        %opCode = 0;
    }
    %opCode = 1;
    changeRelation(%sender, %otherId, %relType, %opCode);
    return;
};
