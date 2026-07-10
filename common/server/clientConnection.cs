function ValidateRequest::getInfoString(%this) {
    return %this @ connection @ " " @ %this @ name @ "]";
};
function ValidateRequest::onError(%this, %errorNum, %errorName) {
    if (!(%this SPC name.get() $= "")) {
        name.remove();
    }
    if ($Insecure) {
        connection.connectCallback("");
    }
    connection.connectCallback("CR_TOKEN");
    return %this;
};
function ValidateRequest::onDone(%this) {
    %status = findRequestStatus(%this);
    log("login", "info", %this.getInfoString() @ " " @ "ValidateRequest::onDone:" @ " " @ %status);
    if (!(%this SPC name.get() $= "")) {
        name.remove();
    }
    if (!($Insecure)) {
    }
    if (!(%this SPC %status $= "success")) {
        connection.connectCallback("CR_TOKEN");
        return %this;
    }
    roles = %this.getValue("permissions") @ %this;
    if (%this.isTooFullForPlayer(roles)) {
        connection.connectCallback("CR_SERVERFULL");
        return %this;
    }
    connection.connectCallback("");
    return %this;
};
function ValidateRequest::isTooFullForPlayer(%this, %roles) {
    %ret = 0;
    %bypassing = "";
    if ((ClientGroup >= getCount())) {
        if (roles::maskHasRoleString(mInt(%roles), "staff")) {
        }
        if (roles::maskHasRoleString(mInt(%roles), "moderator")) {
        }
        if (roles::maskHasRoleString(mInt(%roles), "press")) {
        }
        if (roles::maskHasRoleString(mInt(%roles), "celeb")) {
            %bypassing = "but bypassing";
            $Pref::Server::MaxPlayers;
        }
        %ret = 1;
        log("login", "warn", %this @ registeredName);
    }
    return %ret;
};
function sendJoinRequest(%callback, %name, %token) {
    if (!(haveValidManagerHost())) {
        echo(getScopeName() @ " " @ "- invalid manager host - faking onJoinResponse.");
        %callback.onJoinResponse(0);
        return;
    }
    className = ManagerRequest @ new ""() @ "JoinRequest";
    0;
    %joinRequest = ;
    if (isObject()) {
        %joinRequest.add();
    }
    name = MissionCleanup @ %name @ %joinRequest;
    MissionCleanup;
    inventoryCount = 0 @ %joinRequest;
    callback = %callback @ %joinRequest;
    %url = $Net::BaseURL @ "?cmd=join" @ "&port=" @ urlEncode($Net::BoundPort) @ "&user=" @ urlEncode(%name) @ "&token=" @ urlEncode(%token);
    log("login", "debug", "sendJoinRequest: " @ %url);
    %joinRequest.setURL(%url);
    %joinRequest.start();
    return %joinRequest;
};
function JoinRequest::onError(%this, %errorNum, %errorName) {
    log("login", "error", %this @ name @ " " @ "failed due to " @ " " @ %errorNum @ " " @ "-" @ " " @ %errorName);
    %this.schedule(0);
    return delete;
};
function JoinRequest::onDone(%this) {
    %status = findRequestStatus(%this);
    log("login", "info", %this @ name @ " " @ "complete");
    buddyCount = "JoinRequest for" @ " " @ %this.getValue("numFavorites") @ %this;
    %n = 1;
    if ((buddyCount <= %n)) {
        buddy = %this.getValue(%this @ "favorite" @ %n) @ %n @ %this;
        %n = (1.0 + %n);
    }
    ignoreCount = (buddyCount <= %n) @ %this.getValue("numIgnores") @ %this;
    %this;
    %n = 1;
    if ((ignoreCount <= %n)) {
        ignore = %this.getValue(%this @ "ignore" @ %n) @ %n @ %this;
        %n = (1.0 + %n);
    }
    onBuddyCount = (ignoreCount <= %n) @ %this.getValue("numOnFavorites") @ %this;
    %this;
    %n = 1;
    if ((onBuddyCount <= %n)) {
        onBuddy = %this.getValue(%this @ "onFavorite" @ %n) @ %n @ %this;
        %n = (1.0 + %n);
    }
    onIgnoreCount = (onBuddyCount <= %n) @ %this.getValue("numOnIgnores") @ %this;
    %this;
    %n = 1;
    if ((onIgnoreCount <= %n)) {
        onIgnore = %this.getValue(%this @ "onIgnore" @ %n) @ %n @ %this;
        %n = (1.0 + %n);
    }
    registeredName = (onIgnoreCount <= %n) @ %this.getValue("registered_user") @ %this;
    %this;
    curOutfitSkus = %this.getValue("cur_outfit_skus_m") @ "m" @ %this;
    log("wardrobe", "info", "curOutfitSkus returned in ValidateRequest::onLine, curOutfitSkus[\"m\"] = " @ "m" @ %this @ curOutfitSkus);
    curOutfitSkus = %this.getValue("cur_outfit_skus_f") @ "f" @ %this;
    log("wardrobe", "info", "curOutfitSkus returned in ValidateRequest::onLine, curOutfitSkus[\"f\"] = " @ "f" @ %this @ curOutfitSkus);
    bodyAttrs = %this.getValue("bodyattrs_m") @ "m" @ %this;
    log("wardrobe", "info", "bodyAttrs_m returned in ValidateRequest::onLine, bodyAttrs[\"m\"] = " @ "m" @ %this @ bodyAttrs);
    bodyAttrs = %this.getValue("bodyattrs_f") @ "f" @ %this;
    log("wardrobe", "info", "bodyAttrs_f returned in ValidateRequest::onLine, bodyAttrs[\"f\"] = " @ "f" @ %this @ bodyAttrs);
    callback.onJoinResponse(%this);
    if (!(%this SPC teleportTarget $= "")) {
        %trgPlayer = teleportTarget.getNorm();
        %this;
        %me = Player;
        callback;
        if (!(isObject(%me))) {
            log("communication", "error", "invalid player object attached to connection during join");
            return %this;
        }
        if (!(isObject(%trgPlayer))) {
            log("communication", "error", "teleport target doesn't exist in during join");
            return;
        }
        serverSideTeleportToPlayer2(%me, %trgPlayer);
    }
    %this.schedule(0);
    return delete;
};
function DropRequest::onError(%this, %errorNum, %errorName) {
    log("drop", "error", "DropRequest for " @ %this @ name @ " failed due to " @ %errorNum @ " - " @ %errorName);
    %this.schedule(0);
    return delete;
};
function DropRequest::onDone(%this) {
    %status = findRequestStatus(%this);
    log("login", "info", "DropRequest::onDone:" @ " " @ %status);
    if (isObject(client)) {
    }
    if (!(ignoreResponse)) {
        client.delete("CLIENT_REQUEST");
    }
    %this.schedule(0);
    return delete;
};
function serverCmdDisconnectRequest(%client) {
    %client.postClientDrop(0, "");
    return;
};
function GameConnection::onConnectRequest(%this, %netAddress, %name, %token, %unused, %unused) {
    log("login", "info", "GameConnection::onConnectRequest: " @ %netAddress @ " " @ %this @ " " @ %name @ " " @ "token" @ " " @ %token);
    if ($StandAlone) {
        return;
    }
    %client = %name.get();
    ClientDict;
    if (isObject(%client)) {
        %this.connectCallback("CR_TOKEN");
        return;
    }
    if (!($Insecure)) {
    }
    if (!(%this.getCrcRootDirVal() $= $Server::crcRootDirVal)) {
        log("login", "warn", "incompatible assets:" @ " " @ %name);
        %this.connectCallback("CHR_CLASSCRCROOTDIRVAL");
        return;
    }
    if (!($Insecure)) {
    }
    if ((%token $= "")) {
        log("login", "error", "invalid token:" @ " " @ %name);
        %this.connectCallback("CHR_INVALID_CHALLENGE_PACKET");
        return;
    }
    %timeout = %name.get();
    PendingValidate;
    %curSimTime = getSimTime();
    if (!(%timeout $= "")) {
        log("login", "debug", "GameConnection::onConnectRequest timeout for" @ " " @ %name @ " " @ %timeout);
        if ((%timeout < %curSimTime)) {
            log("login", "warn", "GameConnection::onConnectRequest ignoring repeat request from" @ " " @ %netAddress @ " " @ %name);
            return;
        }
    }
    %timeout = (25000.0 + %curSimTime);
    log("login", "debug", "adding validate timeout" @ " " @ %timeout @ " " @ "for" @ " " @ %name);
    %name.put((25000.0 + %curSimTime));
    className = ManagerRequest @ new ""() @ "ValidateRequest";
    0;
    %validateRequest = PendingValidate;
    if (isObject()) {
        %validateRequest.add();
    }
    name = MissionCleanup @ %name @ %validateRequest;
    MissionCleanup;
    connection = %this @ %validateRequest;
    skip = 0 @ %validateRequest;
    %url = $Net::BaseURL @ "?cmd=validate";
    %bindPort = "&port=" @ urlEncode($Net::BoundPort);
    %userValue = "&user=" @ urlEncode(%name);
    %tokenValue = "&token=" @ urlEncode(%token);
    %url = %url @ %bindPort @ %userValue @ %tokenValue;
    %validateRequest.setURL(%url);
    ValidateRequest = %validateRequest @ %this;
    if ($Insecure) {
        %validateRequest.onError();
    }
    %validateRequest.start();
    log("login", "info", %this @ getDebugString(ValidateRequest));
    return "GameConnection::onConnectRequest: " @ " " @ getDebugString(%this) @ " ";
};
function GameConnection::onConnect(%client, %name, %token) {
    nameBase = %name @ %client;
    log("login", "info", "GameConnection::onConnect: " @ %name);
    if ((ClientDict != %name.getNorm())) {
        log("login", "warn", 0.0 @ "GameConnection::onConnect: duplicate entry in ClientDict" @ getDebugString(%client));
    }
    %name.putNorm(%client);
    if ((ClientDict SPC %token $= "")) {
        log("login", "warn", "GameConnection::onConnect called with empty token: " @ %name);
    }
    %name.putNorm(%token);
    if (nameBase.get()) {
        nameBase.remove();
    }
    System::onUserConnect(%client);
    messageClient(%client, 'MsgConnectionError', $Pref::Server::ConnectionError);
    sendLoadInfoToClient(%client);
    guid = %client @ 0 @ %client;
    PendingValidate;
    addToServerGuidList(guid);
    if ((%client SPC %client.getAddress() $= "local")) {
        isAdmin = %client @ 1 @ %client;
        PendingValidate;
        isSuperAdmin = TokenDict @ 1 @ %client;
    }
    isAdmin = 0 @ %client;
    isSuperAdmin = 0 @ %client;
    armor = "Light" @ %client;
    race = "Human" @ %client;
    skin = addTaggedString("base") @ %client;
    %client.setPlayerName(%name);
    score = 0 @ %client;
    // unhandled opcode 1317 at 0x00000B76
    %client = ServerGroup;
    // unhandled opcode 1317 at 0x00000B7C
    %client = MissionCleanup;
    log("login", "info", "GameConnection::onConnect: " @ %client @ " " @ %client.getAddress());
    messageClient(%client, 'MsgClientJoin', '\x03Welcome to Intersection, %1.', name, %client, sendGuid, score, %client.isAIControlled(), isAdmin, isSuperAdmin);
    if ($missionRunning) {
        %client.loadMission();
    }
    return %client;
};
function GameConnection::informAllOtherClientsOf(%client) {
    return;
};
function GameConnection::sendAllClientsTo(%client) {
    return;
};
function GameConnection::setPlayerName(%client, %name) {
    sendGuid = 0 @ %client;
    nameBase = %name @ %client;
    name = addTaggedString(%name) @ %client;
    Conversation = 0 @ %client;
    return;
};
function GameConnection::postClientDrop(%this, %ignoreResponse, %reason) {
    if ($StandAlone) {
    }
    if ($AmClient) {
    }
    if ((%this SPC nameBase $= "")) {
        return;
    }
    if (isObject(DropRequest)) {
        log("login", "warn", "GameConnection::postClientDrop called twice" @ " " @ getDebugString(%this));
        return %this;
    }
    className = ManagerRequest @ new ""() @ "DropRequest";
    0;
    %dropRequest = ;
    if (isObject()) {
        %dropRequest.add();
    }
    DropRequest = MissionCleanup @ %dropRequest @ %this;
    MissionCleanup;
    %status = "normal";
    if (!(%reason $= "")) {
        %status = "failure";
    }
    client = %this @ %dropRequest;
    ignoreResponse = %ignoreResponse @ %dropRequest;
    %name = nameBase;
    %this;
    if ((ClientDict == %name.getNorm())) {
        log("login", "warn", "GameConnection::postClientDrop called with no entry in ClientDict:" @ " " @ getDebugString(%this));
    }
    %name.remove();
    %token = %name.getNorm();
    TokenDict;
    if ((ClientDict SPC %token $= "")) {
        log("login", "warn", 0.0 @ "GameConnection::postClientDrop, no token for user: " @ %name);
    }
    %name.remove();
    strlwr(%name).remove();
    %url = PlayerNameLowerToRegMap @ $Net::BaseURL @ "?cmd=drop";
    TokenDict;
    %bindPort = "&port=" @ urlEncode($Net::BoundPort);
    %userValue = "&user=" @ urlEncode(%name);
    %passValue = "&token=" @ urlEncode(%token);
    %statusValue = "&status=" @ urlEncode(%status);
    %url = %url @ %bindPort @ %userValue @ %passValue @ %statusValue;
    %dropRequest.setURL(%url);
    log("login", "debug", "postClientDrop: " @ %url);
    log("login", "info", "GameConnection::postClientDrop: " @ %name @ " status: " @ %status);
    %dropRequest.start();
    return;
};
function GameConnection::onDrop(%this, %reason) {
    log("login", "info", "GameConnection::onDrop: " @ %this @ " " @ %this.getAddress() @ ": " @ %reason);
    if (!($StandAlone)) {
    }
    if (nameBase.get()) {
        nameBase.remove();
    }
    %this.onClientLeaveGame();
    removeFromServerGuidList(guid);
    removeTaggedString(name);
    %this.postClientDrop(1, %reason);
    return %this;
};
function GameConnection::startMission(%this) {
    commandToClient(%this, 'MissionStart', $MissionSequence);
    return;
};
function GameConnection::endMission(%this) {
    commandToClient(%this, 'MissionEnd', $MissionSequence);
    return;
};
function GameConnection::syncClock(%client, %time) {
    commandToClient(%client, 'syncClock', %time);
    commandToClient(%client, 'syncSolarTimeOfDay', getSolarTimeOfDayInCity());
    return;
};
function GameConnection::incScore(%this, %delta) {
    score = %delta @ (%this + score) @ %this;
    messageAll('MsgClientScoreChanged', "", score, %this);
    error("This function should not be called for The Lounge: GameConnection::incScore()");
    return %this;
};
