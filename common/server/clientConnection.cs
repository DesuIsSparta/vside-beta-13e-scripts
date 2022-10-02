function ValidateRequest::getInfoString(%this)
{
    return "[" @ %this.connection SPC %this.name @ "]";
}
function ValidateRequest::onError(%this, %errorNum, %errorName)
{
    if (!(PendingValidate.get(%this.name) $= ""))
    {
        PendingValidate.remove(%this.name);
    }
    if ($Insecure)
    {
        %this.connection.connectCallback("");
    }
    else
    {
        %this.connection.connectCallback("CR_TOKEN");
    }
    return ;
}
function ValidateRequest::onDone(%this)
{
    %status = findRequestStatus(%this);
    log("login", "info", %this.getInfoString() SPC "ValidateRequest::onDone:" SPC %status);
    if (!(PendingValidate.get(%this.name) $= ""))
    {
        PendingValidate.remove(%this.name);
    }
    if (!$Insecure && !((%status $= "success")))
    {
        %this.connection.connectCallback("CR_TOKEN");
        return ;
    }
    %this.roles = %this.getValue("permissions");
    if (%this.isTooFullForPlayer(%this.roles))
    {
        %this.connection.connectCallback("CR_SERVERFULL");
        return ;
    }
    %this.connection.connectCallback("");
    return ;
}
function ValidateRequest::isTooFullForPlayer(%this, %roles)
{
    %ret = 0;
    %bypassing = "";
    if (ClientGroup.getCount() >= $Pref::Server::MaxPlayers)
    {
        if (((roles::maskHasRoleString(mInt(%roles), "staff") || roles::maskHasRoleString(mInt(%roles), "moderator")) || roles::maskHasRoleString(mInt(%roles), "press")) || roles::maskHasRoleString(mInt(%roles), "celeb"))
        {
            %bypassing = "but bypassing";
        }
        else
        {
            %ret = 1;
        }
        log("login", "warn", %this.getInfoString() SPC "server full at:" SPC ClientGroup.getCount() SPC %bypassing SPC "for" SPC %this.registeredName);
    }
    return %ret;
}
function sendJoinRequest(%callback, %name, %token)
{
    if (!haveValidManagerHost())
    {
        echo(getScopeName() SPC "- invalid manager host - faking onJoinResponse.");
        %callback.onJoinResponse(0);
        return ;
    }
    %joinRequest = new ManagerRequest();
    if (isObject(MissionCleanup))
    {
        MissionCleanup.add(%joinRequest);
    }
    %joinRequest.name = %name;
    %joinRequest.inventoryCount = 0;
    %joinRequest.callback = %callback;
    %url = $Net::BaseURL @ "?cmd=join" @ "&port=" @ urlEncode($Net::BoundPort) @ "&user=" @ urlEncode(%name) @ "&token=" @ urlEncode(%token);
    log("login", "debug", "sendJoinRequest: " @ %url);
    %joinRequest.setURL(%url);
    %joinRequest.start();
    return %joinRequest;
}
function JoinRequest::onError(%this, %errorNum, %errorName)
{
    log("login", "error", "JoinRequest for" SPC %this.name SPC "failed due to " SPC %errorNum SPC "-" SPC %errorName);
    %this.schedule(0, delete);
    return ;
}
function JoinRequest::onDone(%this)
{
    %status = findRequestStatus(%this);
    log("login", "info", "JoinRequest for" SPC %this.name SPC "complete");
    %this.buddyCount = %this.getValue("numFavorites");
    %n = 1;
    while (%n <= %this.buddyCount)
    {
        %this.buddy[%n] = %this.getValue("favorite" @ %n);
        %n = %n + 1;
    }
    %this.ignoreCount = %this.getValue("numIgnores");
    %n = 1;
    while (%n <= %this.ignoreCount)
    {
        %this.ignore[%n] = %this.getValue("ignore" @ %n);
        %n = %n + 1;
    }
    %this.onBuddyCount = %this.getValue("numOnFavorites");
    %n = 1;
    while (%n <= %this.onBuddyCount)
    {
        %this.onBuddy[%n] = %this.getValue("onFavorite" @ %n);
        %n = %n + 1;
    }
    %this.onIgnoreCount = %this.getValue("numOnIgnores");
    %n = 1;
    while (%n <= %this.onIgnoreCount)
    {
        %this.onIgnore[%n] = %this.getValue("onIgnore" @ %n);
        %n = %n + 1;
    }
    %this.registeredName = %this.getValue("registered_user");
    %this.curOutfitSkus["m"] = %this.getValue("cur_outfit_skus_m");
    log("wardrobe", "info", "curOutfitSkus returned in ValidateRequest::onLine, curOutfitSkus[\"m\"] = " @ %this.curOutfitSkus["m"]);
    %this.curOutfitSkus["f"] = %this.getValue("cur_outfit_skus_f");
    log("wardrobe", "info", "curOutfitSkus returned in ValidateRequest::onLine, curOutfitSkus[\"f\"] = " @ %this.curOutfitSkus["f"]);
    %this.bodyAttrs["m"] = %this.getValue("bodyattrs_m");
    log("wardrobe", "info", "bodyAttrs_m returned in ValidateRequest::onLine, bodyAttrs[\"m\"] = " @ %this.bodyAttrs["m"]);
    %this.bodyAttrs["f"] = %this.getValue("bodyattrs_f");
    log("wardrobe", "info", "bodyAttrs_f returned in ValidateRequest::onLine, bodyAttrs[\"f\"] = " @ %this.bodyAttrs["f"]);
    %this.callback.onJoinResponse(%this);
    if (!(%this.teleportTarget $= ""))
    {
        %trgPlayer = PlayerDict.getNorm(%this.teleportTarget);
        %me = %this.callback.Player;
        if (!isObject(%me))
        {
            log("communication", "error", "invalid player object attached to connection during join");
            return ;
        }
        if (!isObject(%trgPlayer))
        {
            log("communication", "error", "teleport target doesn\'t exist in during join");
            return ;
        }
        serverSideTeleportToPlayer2(%me, %trgPlayer);
    }
    %this.schedule(0, delete);
    return ;
}
function DropRequest::onError(%this, %errorNum, %errorName)
{
    log("drop", "error", "DropRequest for " @ %this.name @ " failed due to " @ %errorNum @ " - " @ %errorName);
    %this.schedule(0, delete);
    return ;
}
function DropRequest::onDone(%this)
{
    %status = findRequestStatus(%this);
    log("login", "info", "DropRequest::onDone:" SPC %status);
    if (isObject(%this.client) && !(%this.client.ignoreResponse))
    {
        %this.client.delete("CLIENT_REQUEST");
    }
    %this.schedule(0, delete);
    return ;
}
function serverCmdDisconnectRequest(%client)
{
    %client.postClientDrop(0, "");
    return ;
}
function GameConnection::onConnectRequest(%this, %netAddress, %name, %token, %unused, %unused)
{
    log("login", "info", "GameConnection::onConnectRequest: " @ %netAddress SPC %this SPC %name SPC "token" SPC %token);
    if ($StandAlone)
    {
        return ;
    }
    %client = ClientDict.get(%name);
    if (isObject(%client))
    {
        %this.connectCallback("CR_TOKEN");
        return ;
    }
    if (!$Insecure && !((%this.getCrcRootDirVal() $= $Server::crcRootDirVal)))
    {
        log("login", "warn", "incompatible assets:" SPC %name);
        %this.connectCallback("CHR_CLASSCRCROOTDIRVAL");
        return ;
    }
    if (!$Insecure && (%token $= ""))
    {
        log("login", "error", "invalid token:" SPC %name);
        %this.connectCallback("CHR_INVALID_CHALLENGE_PACKET");
        return ;
    }
    %timeout = PendingValidate.get(%name);
    %curSimTime = getSimTime();
    if (!(%timeout $= ""))
    {
        log("login", "debug", "GameConnection::onConnectRequest timeout for" SPC %name SPC %timeout);
        if (%curSimTime < %timeout)
        {
            log("login", "warn", "GameConnection::onConnectRequest ignoring repeat request from" SPC %netAddress SPC %name);
            return ;
        }
    }
    %timeout = %curSimTime + 25000;
    log("login", "debug", "adding validate timeout" SPC %timeout SPC "for" SPC %name);
    PendingValidate.put(%name, %curSimTime + 25000);
    %validateRequest = new ManagerRequest();
    if (isObject(MissionCleanup))
    {
        MissionCleanup.add(%validateRequest);
    }
    %validateRequest.name = %name;
    %validateRequest.connection = %this;
    %validateRequest.skip = 0;
    %url = $Net::BaseURL @ "?cmd=validate";
    %bindPort = "&port=" @ urlEncode($Net::BoundPort);
    %userValue = "&user=" @ urlEncode(%name);
    %tokenValue = "&token=" @ urlEncode(%token);
    %url = %url @ %bindPort @ %userValue @ %tokenValue;
    %validateRequest.setURL(%url);
    %this.ValidateRequest = %validateRequest;
    if ($Insecure)
    {
        %validateRequest.onError();
    }
    else
    {
        %validateRequest.start();
    }
    log("login", "info", "GameConnection::onConnectRequest: " SPC getDebugString(%this) SPC getDebugString(%this.ValidateRequest));
    return ;
}
function GameConnection::onConnect(%client, %name, %token)
{
    %client.nameBase = %name;
    log("login", "info", "GameConnection::onConnect: " @ %name);
    if (ClientDict.getNorm(%name) != 0)
    {
        log("login", "warn", "GameConnection::onConnect: duplicate entry in ClientDict" @ getDebugString(%client));
    }
    ClientDict.putNorm(%name, %client);
    if (%token $= "")
    {
        log("login", "warn", "GameConnection::onConnect called with empty token: " @ %name);
    }
    TokenDict.putNorm(%name, %token);
    if (PendingValidate.get(%client.nameBase))
    {
        PendingValidate.remove(%client.nameBase);
    }
    System::onUserConnect(%client);
    messageClient(%client, 'MsgConnectionError', $Pref::Server::ConnectionError);
    sendLoadInfoToClient(%client);
    %client.guid = 0;
    addToServerGuidList(%client.guid);
    if (%client.getAddress() $= "local")
    {
        %client.isAdmin = 1;
        %client.isSuperAdmin = 1;
    }
    else
    {
        %client.isAdmin = 0;
        %client.isSuperAdmin = 0;
    }
    %client.armor = "Light";
    %client.race = "Human";
    %client.skin = addTaggedString("base");
    %client.setPlayerName(%name);
    %client.score = 0;
    $instantGroup = ServerGroup;
    $instantGroup = MissionCleanup;
    log("login", "info", "GameConnection::onConnect: " @ %client @ " " @ %client.getAddress());
    messageClient(%client, 'MsgClientJoin', '\c2Welcome to Intersection, %1.', %client.name, %client, %client.sendGuid, %client.score, %client.isAIControlled(), %client.isAdmin, %client.isSuperAdmin);
    if ($missionRunning)
    {
        %client.loadMission();
    }
    return ;
}
function GameConnection::informAllOtherClientsOf(%client)
{
    return ;
}
function GameConnection::sendAllClientsTo(%client)
{
    return ;
}
function GameConnection::setPlayerName(%client, %name)
{
    %client.sendGuid = 0;
    %client.nameBase = %name;
    %client.name = addTaggedString(%name);
    %client.Conversation = 0;
    return ;
}
function GameConnection::postClientDrop(%this, %ignoreResponse, %reason)
{
    if (($StandAlone || $AmClient) || (%this.nameBase $= ""))
    {
        return ;
    }
    if (isObject(%this.DropRequest))
    {
        log("login", "warn", "GameConnection::postClientDrop called twice" SPC getDebugString(%this));
        return ;
    }
    %dropRequest = new ManagerRequest();
    if (isObject(MissionCleanup))
    {
        MissionCleanup.add(%dropRequest);
    }
    %this.DropRequest = %dropRequest;
    %status = "normal";
    if (!(%reason $= ""))
    {
        %status = "failure";
    }
    %dropRequest.client = %this;
    %dropRequest.ignoreResponse = %ignoreResponse;
    %name = %this.nameBase;
    if (ClientDict.getNorm(%name) == 0)
    {
        log("login", "warn", "GameConnection::postClientDrop called with no entry in ClientDict:" SPC getDebugString(%this));
    }
    ClientDict.remove(%name);
    %token = TokenDict.getNorm(%name);
    if (%token $= "")
    {
        log("login", "warn", "GameConnection::postClientDrop, no token for user: " @ %name);
    }
    TokenDict.remove(%name);
    PlayerNameLowerToRegMap.remove(strlwr(%name));
    %url = $Net::BaseURL @ "?cmd=drop";
    %bindPort = "&port=" @ urlEncode($Net::BoundPort);
    %userValue = "&user=" @ urlEncode(%name);
    %passValue = "&token=" @ urlEncode(%token);
    %statusValue = "&status=" @ urlEncode(%status);
    %url = %url @ %bindPort @ %userValue @ %passValue @ %statusValue;
    %dropRequest.setURL(%url);
    log("login", "debug", "postClientDrop: " @ %url);
    log("login", "info", "GameConnection::postClientDrop: " @ %name @ " status: " @ %status);
    %dropRequest.start();
    return ;
}
function GameConnection::onDrop(%this, %reason)
{
    log("login", "info", "GameConnection::onDrop: " @ %this @ " " @ %this.getAddress() @ ": " @ %reason);
    if (!$StandAlone && PendingValidate.get(%this.nameBase))
    {
        PendingValidate.remove(%this.nameBase);
    }
    %this.onClientLeaveGame();
    removeFromServerGuidList(%this.guid);
    removeTaggedString(%this.name);
    %this.postClientDrop(1, %reason);
    return ;
}
function GameConnection::startMission(%this)
{
    commandToClient(%this, 'MissionStart', $MissionSequence);
    return ;
}
function GameConnection::endMission(%this)
{
    commandToClient(%this, 'MissionEnd', $MissionSequence);
    return ;
}
function GameConnection::syncClock(%client, %time)
{
    commandToClient(%client, 'syncClock', %time);
    commandToClient(%client, 'syncSolarTimeOfDay', getSolarTimeOfDayInCity());
    return ;
}
function GameConnection::incScore(%this, %delta)
{
    %this.score = %this.score + %delta;
    messageAll('MsgClientScoreChanged', "", %this.score, %this);
    error("This function should not be called for The Lounge: GameConnection::incScore()");
    return ;
}
