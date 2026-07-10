function ValidateRequest::getInfoString(%this) {
    return "[" @ %this.connection @ " " @ %this.name @ "]";
};
function ValidateRequest::onError(%this, %errorNum, %errorName) {
    if (!(%this.name.get(PendingValidate) $= "")) {
        %this.name.remove(PendingValidate);
    }
    if ($Insecure) {
        "".connectCallback(%this.connection);
    }
    "CR_TOKEN".connectCallback(%this.connection);
    return;
};
function ValidateRequest::onDone(%this) {
    %status = findRequestStatus(%this);
    log("login", "info", %this.getInfoString() @ " " @ "ValidateRequest::onDone:" @ " " @ %status);
    if (!(%this.name.get(PendingValidate) $= "")) {
        %this.name.remove(PendingValidate);
    }
    if (!($Insecure)) {
    }
    if (!(%status $= "success")) {
        "CR_TOKEN".connectCallback(%this.connection);
        return;
    }
    %this.roles = "permissions".getValue(%this);
    if (%this.roles.isTooFullForPlayer(%this)) {
        "CR_SERVERFULL".connectCallback(%this.connection);
        return;
    }
    "".connectCallback(%this.connection);
    return;
};
function ValidateRequest::isTooFullForPlayer(%this, %roles) {
    %ret = 0;
    %bypassing = "";
    if ((ClientGroup.getCount() >= $Pref::Server::MaxPlayers)) {
        if (roles::maskHasRoleString(mInt(%roles), "staff")) {
        }
        if (roles::maskHasRoleString(mInt(%roles), "moderator")) {
        }
        if (roles::maskHasRoleString(mInt(%roles), "press")) {
        }
        if (roles::maskHasRoleString(mInt(%roles), "celeb")) {
            %bypassing = "but bypassing";
        }
        %ret = 1;
        log("login", "warn", %this.getInfoString() @ " " @ "server full at:" @ " " @ ClientGroup.getCount() @ " " @ %bypassing @ " " @ "for" @ " " @ %this.registeredName);
    }
    return %ret;
};
function sendJoinRequest(%callback, %name, %token) {
    if (!(haveValidManagerHost())) {
        echo(getScopeName() @ " " @ "- invalid manager host - faking onJoinResponse.");
        0.onJoinResponse(%callback);
        return;
    }
    %joinRequest = new ManagerRequest("") {
        className = 0 @ "JoinRequest";
    };
    if (isObject(MissionCleanup)) {
        %joinRequest.add(MissionCleanup);
    }
    %joinRequest.name = %name;
    %joinRequest.inventoryCount = 0;
    %joinRequest.callback = %callback;
    %url = $Net::BaseURL @ "?cmd=join" @ "&port=" @ urlEncode($Net::BoundPort) @ "&user=" @ urlEncode(%name) @ "&token=" @ urlEncode(%token);
    log("login", "debug", "sendJoinRequest: " @ %url);
    %url.setURL(%joinRequest);
    %joinRequest.start();
    return %joinRequest;
};
function JoinRequest::onError(%this, %errorNum, %errorName) {
    log("login", "error", "JoinRequest for" @ " " @ %this.name @ " " @ "failed due to " @ " " @ %errorNum @ " " @ "-" @ " " @ %errorName);
    0.schedule(%this);
    return delete;
};
function JoinRequest::onDone(%this) {
    %status = findRequestStatus(%this);
    log("login", "info", "JoinRequest for" @ " " @ %this.name @ " " @ "complete");
    %this.buddyCount = "numFavorites".getValue(%this);
    %n = 1;
    while ((%n <= %this.buddyCount)) {
        %this.buddy = "favorite" @ %n.getValue(%this) @ %n;
        %n = (%n + 1.0);
    }
    %this.ignoreCount = (%n <= %this.buddyCount) @ "numIgnores".getValue(%this);
    %n = 1;
    while ((%n <= %this.ignoreCount)) {
        %this.ignore = "ignore" @ %n.getValue(%this) @ %n;
        %n = (%n + 1.0);
    }
    %this.onBuddyCount = (%n <= %this.ignoreCount) @ "numOnFavorites".getValue(%this);
    %n = 1;
    while ((%n <= %this.onBuddyCount)) {
        %this.onBuddy = "onFavorite" @ %n.getValue(%this) @ %n;
        %n = (%n + 1.0);
    }
    %this.onIgnoreCount = (%n <= %this.onBuddyCount) @ "numOnIgnores".getValue(%this);
    %n = 1;
    while ((%n <= %this.onIgnoreCount)) {
        %this.onIgnore = "onIgnore" @ %n.getValue(%this) @ %n;
        %n = (%n + 1.0);
    }
    %this.registeredName = (%n <= %this.onIgnoreCount) @ "registered_user".getValue(%this);
    %this.curOutfitSkus = "cur_outfit_skus_m".getValue(%this) @ "m";
    log("wardrobe", "info", "curOutfitSkus returned in ValidateRequest::onLine, curOutfitSkus[\"m\"] = " @ "m" @ %this.curOutfitSkus);
    %this.curOutfitSkus = "cur_outfit_skus_f".getValue(%this) @ "f";
    log("wardrobe", "info", "curOutfitSkus returned in ValidateRequest::onLine, curOutfitSkus[\"f\"] = " @ "f" @ %this.curOutfitSkus);
    %this.bodyAttrs = "bodyattrs_m".getValue(%this) @ "m";
    log("wardrobe", "info", "bodyAttrs_m returned in ValidateRequest::onLine, bodyAttrs[\"m\"] = " @ "m" @ %this.bodyAttrs);
    %this.bodyAttrs = "bodyattrs_f".getValue(%this) @ "f";
    log("wardrobe", "info", "bodyAttrs_f returned in ValidateRequest::onLine, bodyAttrs[\"f\"] = " @ "f" @ %this.bodyAttrs);
    %this.onJoinResponse(%this.callback);
    if (!(%this.teleportTarget $= "")) {
        %trgPlayer = %this.teleportTarget.getNorm(PlayerDict);
        %me = %this.callback.Player;
        if (!(isObject(%me))) {
            log("communication", "error", "invalid player object attached to connection during join");
            return;
        }
        if (!(isObject(%trgPlayer))) {
            log("communication", "error", "teleport target doesn't exist in during join");
            return;
        }
        serverSideTeleportToPlayer2(%me, %trgPlayer);
    }
    0.schedule(%this);
    return delete;
};
function DropRequest::onError(%this, %errorNum, %errorName) {
    log("drop", "error", "DropRequest for " @ %this.name @ " failed due to " @ %errorNum @ " - " @ %errorName);
    0.schedule(%this);
    return delete;
};
function DropRequest::onDone(%this) {
    %status = findRequestStatus(%this);
    log("login", "info", "DropRequest::onDone:" @ " " @ %status);
    if (isObject(%this.client)) {
    }
    if (!(%this.client.ignoreResponse)) {
        "CLIENT_REQUEST".delete(%this.client);
    }
    0.schedule(%this);
    return delete;
};
function serverCmdDisconnectRequest(%client) {
    "".postClientDrop(%client, 0);
    return;
};
function GameConnection::onConnectRequest(%this, %netAddress, %name, %token, %unused, %unused) {
    log("login", "info", "GameConnection::onConnectRequest: " @ %netAddress @ " " @ %this @ " " @ %name @ " " @ "token" @ " " @ %token);
    if ($StandAlone) {
        return;
    }
    %client = %name.get(ClientDict);
    if (isObject(%client)) {
        "CR_TOKEN".connectCallback(%this);
        return;
    }
    if (!($Insecure)) {
    }
    if (!(%this.getCrcRootDirVal() $= $Server::crcRootDirVal)) {
        log("login", "warn", "incompatible assets:" @ " " @ %name);
        "CHR_CLASSCRCROOTDIRVAL".connectCallback(%this);
        return;
    }
    if (!($Insecure)) {
    }
    if ((%token $= "")) {
        log("login", "error", "invalid token:" @ " " @ %name);
        "CHR_INVALID_CHALLENGE_PACKET".connectCallback(%this);
        return;
    }
    %timeout = %name.get(PendingValidate);
    %curSimTime = getSimTime();
    if (!(%timeout $= "")) {
        log("login", "debug", "GameConnection::onConnectRequest timeout for" @ " " @ %name @ " " @ %timeout);
        if ((%curSimTime < %timeout)) {
            log("login", "warn", "GameConnection::onConnectRequest ignoring repeat request from" @ " " @ %netAddress @ " " @ %name);
            return;
        }
    }
    %timeout = (%curSimTime + 25000.0);
    log("login", "debug", "adding validate timeout" @ " " @ %timeout @ " " @ "for" @ " " @ %name);
    (%curSimTime + 25000.0).put(PendingValidate, %name);
    %validateRequest = new ManagerRequest("") {
        className = 0 @ "ValidateRequest";
    };
    if (isObject(MissionCleanup)) {
        %validateRequest.add(MissionCleanup);
    }
    %validateRequest.name = %name;
    %validateRequest.connection = %this;
    %validateRequest.skip = 0;
    %url = $Net::BaseURL @ "?cmd=validate";
    %bindPort = "&port=" @ urlEncode($Net::BoundPort);
    %userValue = "&user=" @ urlEncode(%name);
    %tokenValue = "&token=" @ urlEncode(%token);
    %url = %url @ %bindPort @ %userValue @ %tokenValue;
    %url.setURL(%validateRequest);
    %this.ValidateRequest = %validateRequest;
    if ($Insecure) {
        %validateRequest.onError();
    }
    %validateRequest.start();
    log("login", "info", "GameConnection::onConnectRequest: " @ " " @ getDebugString(%this) @ " " @ getDebugString(%this.ValidateRequest));
    return;
};
function GameConnection::onConnect(%client, %name, %token) {
    %client.nameBase = %name;
    log("login", "info", "GameConnection::onConnect: " @ %name);
    if ((%name.getNorm(ClientDict) != 0.0)) {
        log("login", "warn", "GameConnection::onConnect: duplicate entry in ClientDict" @ getDebugString(%client));
    }
    %client.putNorm(ClientDict, %name);
    if ((%token $= "")) {
        log("login", "warn", "GameConnection::onConnect called with empty token: " @ %name);
    }
    %token.putNorm(TokenDict, %name);
    if (%client.nameBase.get(PendingValidate)) {
        %client.nameBase.remove(PendingValidate);
    }
    System::onUserConnect(%client);
    messageClient(%client, 'MsgConnectionError', $Pref::Server::ConnectionError);
    sendLoadInfoToClient(%client);
    %client.guid = 0;
    addToServerGuidList(%client.guid);
    if ((%client.getAddress() $= "local")) {
        %client.isAdmin = 1;
        %client.isSuperAdmin = 1;
    }
    %client.isAdmin = 0;
    %client.isSuperAdmin = 0;
    %client.armor = "Light";
    %client.race = "Human";
    %client.skin = addTaggedString("base");
    %name.setPlayerName(%client);
    %client.score = 0;
    // unhandled opcode 1317 at 0x00000B76
    %client = ServerGroup;
    // unhandled opcode 1317 at 0x00000B7C
    %client = MissionCleanup;
    log("login", "info", "GameConnection::onConnect: " @ %client @ " " @ %client.getAddress());
    messageClient(%client, 'MsgClientJoin', '\x03Welcome to Intersection, %1.', %client.name, %client, %client.sendGuid, %client.score, %client.isAIControlled(), %client.isAdmin, %client.isSuperAdmin);
    if ($missionRunning) {
        %client.loadMission();
    }
    return;
};
function GameConnection::informAllOtherClientsOf(%client) {
    return;
};
function GameConnection::sendAllClientsTo(%client) {
    return;
};
function GameConnection::setPlayerName(%client, %name) {
    %client.sendGuid = 0;
    %client.nameBase = %name;
    %client.name = addTaggedString(%name);
    %client.Conversation = 0;
    return;
};
function GameConnection::postClientDrop(%this, %ignoreResponse, %reason) {
    if ($StandAlone) {
    }
    if ($AmClient) {
    }
    if ((%this.nameBase $= "")) {
        return;
    }
    if (isObject(%this.DropRequest)) {
        log("login", "warn", "GameConnection::postClientDrop called twice" @ " " @ getDebugString(%this));
        return;
    }
    %dropRequest = new ManagerRequest("") {
        className = 0 @ "DropRequest";
    };
    if (isObject(MissionCleanup)) {
        %dropRequest.add(MissionCleanup);
    }
    %this.DropRequest = %dropRequest;
    %status = "normal";
    if (!(%reason $= "")) {
        %status = "failure";
    }
    %dropRequest.client = %this;
    %dropRequest.ignoreResponse = %ignoreResponse;
    %name = %this.nameBase;
    if ((%name.getNorm(ClientDict) == 0.0)) {
        log("login", "warn", "GameConnection::postClientDrop called with no entry in ClientDict:" @ " " @ getDebugString(%this));
    }
    %name.remove(ClientDict);
    %token = %name.getNorm(TokenDict);
    if ((%token $= "")) {
        log("login", "warn", "GameConnection::postClientDrop, no token for user: " @ %name);
    }
    %name.remove(TokenDict);
    strlwr(%name).remove(PlayerNameLowerToRegMap);
    %url = $Net::BaseURL @ "?cmd=drop";
    %bindPort = "&port=" @ urlEncode($Net::BoundPort);
    %userValue = "&user=" @ urlEncode(%name);
    %passValue = "&token=" @ urlEncode(%token);
    %statusValue = "&status=" @ urlEncode(%status);
    %url = %url @ %bindPort @ %userValue @ %passValue @ %statusValue;
    %url.setURL(%dropRequest);
    log("login", "debug", "postClientDrop: " @ %url);
    log("login", "info", "GameConnection::postClientDrop: " @ %name @ " status: " @ %status);
    %dropRequest.start();
    return;
};
function GameConnection::onDrop(%this, %reason) {
    log("login", "info", "GameConnection::onDrop: " @ %this @ " " @ %this.getAddress() @ ": " @ %reason);
    if (!($StandAlone)) {
    }
    if (%this.nameBase.get(PendingValidate)) {
        %this.nameBase.remove(PendingValidate);
    }
    %this.onClientLeaveGame();
    removeFromServerGuidList(%this.guid);
    removeTaggedString(%this.name);
    %reason.postClientDrop(%this, 1);
    return;
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
    %this.score = (%this.score + %delta);
    messageAll('MsgClientScoreChanged', "", %this.score, %this);
    error("This function should not be called for The Lounge: GameConnection::incScore()");
    return;
};
