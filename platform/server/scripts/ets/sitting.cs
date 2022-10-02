datablock MissionMarkerData(SeatMarker)
{
    category = "markers";
    shapeFile = "projects/common/worlds/markers/arrowmarker.dts";
    sitOffset = "0 0 0";
    sitAnim = "cent";
    standAnim = "cext";
    sitIdle = "cidl2a";
    sitSound = "replaceme";
    standSound = "replaceme";
    idleDelay = 500;
    listeningStation = 0;
};
datablock EtsClientModelData(ClientSeatDisplayData)
{
    shapeFile = "projects/common/worlds/seatdisplay.dts";
    dynamicType = $TypeMasks::UsableObjectType;
    sequence = "ambient";
    sequenceRate = 1;
};
datablock EtsClientModelData(ClientSeatListeningDisplayData : ClientSeatDisplayData);
function InitSittingSystem()
{
    initSeatsTakenSet();
    return ;
}
$SeatsTakenSet = 0;
function initSeatsTakenSet()
{
    if (isObject($SeatsTakenSet))
    {
        $SeatsTakenSet.delete();
    }
    $SeatsTakenSet = new SimSet();
    MissionCleanup.add($SeatsTakenSet);
    return ;
}
function isSeatTaken(%seat)
{
    if (!isObject($SeatsTakenSet))
    {
        warn("isSeatTaken: $SeatsTakenSet must be initialized first");
        return 0;
    }
    return $SeatsTakenSet.isMember(%seat);
}
function takeSeat(%seat)
{
    if (!isObject($SeatsTakenSet))
    {
        warn("takeSeat: $SeatsTakenSet must be initialized first");
        return ;
    }
    if ($SeatsTakenSet.isMember(%seat))
    {
        warn("takeSeat: taking a seat that is already taken: " @ %seat);
    }
    $SeatsTakenSet.add(%seat);
    return ;
}
function freeSeat(%seat)
{
    if (!isObject($SeatsTakenSet))
    {
        warn("freeSeat: $SeatsTakenSet must be initialized first");
        return ;
    }
    if (!$SeatsTakenSet.isMember(%seat))
    {
        warn("freeSeat: freeing a seat that is not taken yet: " @ %seat);
    }
    $SeatsTakenSet.remove(%seat);
    return ;
}
function showClientAvailableSeats(%seats, %client)
{
    %i = 0;
    while (%i < %seats.getCount())
    {
        %seat = %seats.getObject(%i);
        if (!isSeatTaken(%seat))
        {
            %seatType = "ClientSeatDisplayData";
            if (isObject(%seat.listeningStation))
            {
                %seatType = "ClientSeatListeningDisplayData";
            }
            commandToClient(%client, 'ShowSeat', %seat.getId(), %seat.getTransform(), %seatType);
        }
        %i = %i + 1;
    }
}

function showPossibleSeats(%seats, %client)
{
    showClientAvailableSeats(%seats, %client);
    return ;
}
function hideClientSeats(%seats, %client)
{
    %count = %seats.getCount();
    if (%count > 0)
    {
        %seatList = "";
        %i = 0;
        while (%i < %count)
        {
            %seat = %seats.getObject(%i);
            %seatList = %seatList SPC %seat.getId();
            %i = %i + 1;
        }
        commandToClient(%client, 'HideSeats', %seatList);
    }
    return ;
}
function hidePossibleSeats(%seats, %client)
{
    hideClientSeats(%seats, %client);
    return ;
}
function TurnOnSitCam(%player)
{
    %client = %player.client;
    if (isObject(%client))
    {
        if (!isObject(%client.sitCam))
        {
            %client.sitCam = new Camera();
            MissionCleanup.add(%client.sitCam);
            %client.sitCam.scopeToClient(%client);
        }
        %theta = getWord(%player.mySeat.camAngles, 0);
        %phi = getWord(%player.mySeat.camAngles, 1);
        if (%theta $= "")
        {
            %theta = 180;
            %phi = 20;
        }
        %theta = mDegToRad(%theta);
        %phi = mDegToRad(%phi);
        %rot = getOrientationRelativeToObject(%player, %theta, %phi);
        %client.sitCam.setOrbitMode(%player, "0 0 0" SPC %rot, 0.5, 2.5, 1.5);
        %client.setControlObject(%client.sitCam);
    }
    return ;
}
function TurnOffSitCam(%player, %delay)
{
    %client = %player.client;
    if (!%delay)
    {
        %delay = 500;
    }
    if (isObject(%client))
    {
        %client.schedule(%delay, "setControlObject", %player);
    }
    return ;
}
function SitPlayerDown(%player, %seatID)
{
    if (%player.isSitting)
    {
        error("SitPlayerDown: player should not already be sitting, fix this!");
    }
    takeSeat(%seatID);
    %player.mySeat = %seatID;
    %player.isSitting = 1;
    %player.sitDown();
    TurnOnSitCam(%player);
    return ;
}
function NeedSeatRefresh(%player)
{
    %player.needRefreshVisibleSeats = 1;
    return ;
}
function StandPlayerUp(%player, %moveDir)
{
    if (!%player.isSitting)
    {
        error("StandPlayerUp: player should be already sitting, fix this!");
    }
    if (isObject(%player.mySeat.listeningStation))
    {
    }
    else
    {
        if (%moveDir < 0)
        {
            return 0;
        }
    }
    %player.standUp();
    freeSeat(%player.mySeat);
    %player.mySeat = 0;
    %player.isSitting = 0;
    %msUntilPlayerControl = 500;
    TurnOffSitCam(%player, %msUntilPlayerControl);
    schedule(%msUntilPlayerControl, 0, "NeedSeatRefresh", %player);
    return 1;
    return ;
}
function serverCmdRequestToSit(%client, %seatID)
{
    if (!isObject(%client.Player))
    {
        error("serverCmdRequestToSit: client" @ %client @ " has no player.");
        return ;
    }
    if (!isObject(%seatID))
    {
        error("serverCmdRequestToSit: bad seat ID: " @ %seatID);
        return ;
    }
    if (%client.Player.isSitting)
    {
        error("serverCmdRequestToSit: player already sitting " @ %client.Player);
        return ;
    }
    if (isSeatTaken(%seatID))
    {
        commandToClient(%client, 'SeatWasTaken', %seatID);
        return ;
    }
    SitPlayerDown(%client.Player, %seatID);
    commandToClient(%client, 'SitRequestSuccessful', %seatID);
    return ;
}
function serverCmdRequestToStand(%client, %moveDir)
{
    if (!isObject(%client.Player))
    {
        error("serverCmdRequestToStand: bad player object");
        return ;
    }
    %ret = StandPlayerUp(%client.Player, %moveDir);
    if (%ret)
    {
        commandToClient(%client, 'StandRequestSuccessful', %seatID);
    }
    return ;
}
datablock TriggerData(SeatingArea)
{
    tickPeriodMS = 200;
    seats = "ReplaceMeWith a Sim Group Name";
};
function SeatingArea::onEnterTrigger(%this, %trigger, %player)
{
    Parent::onEnterTrigger(%this, %trigger, %player);
    %client = %player.client;
    if (!isObject(%client))
    {
        return ;
    }
    if (!isObject(%trigger.seats))
    {
        error("SeatingArea::onEnterTrigger:  Did not find seats SimGroup member. Must have group of seats to function");
        return ;
    }
    if (!isObject($SeatsTakenSet))
    {
        %player.needRefreshVisibleSeats = 1;
        return ;
    }
    showPossibleSeats(%trigger.seats, %client);
    %player.needRefreshVisibleSeats = 0;
    return ;
}
function SeatingArea::onTickTrigger(%this, %trigger)
{
    Parent::onTickTrigger(%this, %trigger);
    if (!isObject($SeatsTakenSet))
    {
        return ;
    }
    %n = 0;
    while (%n < %trigger.getNumObjects())
    {
        %obj = %trigger.getObject(%n);
        if (%obj.needRefreshVisibleSeats && isObject(%obj.client))
        {
            showPossibleSeats(%trigger.seats, %obj.client);
            %obj.needRefreshVisibleSeats = 0;
        }
        %n = %n + 1;
    }
}

function SeatingArea::onLeaveTrigger(%this, %trigger, %player)
{
    Parent::onLeaveTrigger(%this, %trigger, %player);
    if (!isObject($SeatsTakenSet))
    {
        return ;
    }
    %client = %player.client;
    if (!isObject(%client))
    {
        return ;
    }
    if (!isObject(%trigger.seats))
    {
        return ;
    }
    hidePossibleSeats(%trigger.seats, %client);
    return ;
}
datablock CameraData(SittingObserver)
{
    mode = "Observer";
    cameraMinFov = 30;
    cameraMaxFov = 120;
};
function SittingObserver::onTrigger(%this, %camera, %trigger, %state)
{
    if (%state == 0)
    {
        return ;
    }
    %client = %camera.getControllingClient();
    return ;
}
function SittingObserver::setMode(%this, %obj, %mode, %unused, %unused, %unused)
{
    if (%mode $= "Observer")
    {
        %obj.setFlyMode();
    }
    %obj.mode = %mode;
    return ;
}
function Player::sitDown(%this)
{
    %seat = %this.mySeat;
    if (!isObject(%seat))
    {
        error("Player::sitDown: no mySeat member variable");
        return ;
    }
    %transform = %seat.getTransform();
    %pos = getWords(%transform, 0, 2);
    %rot = getWords(%transform, 3, 6);
    %pos = VectorAdd(%pos, %seat.sitOffset);
    %transform = %pos SPC %rot;
    %this.setTransform(%transform);
    if (%seat.sitAnim $= "")
    {
        warn("seat " @ %seat @ " does not specify a sitAnim");
    }
    else
    {
        %this.setActionThread(%seat.sitAnim, 1, 1);
    }
    if (%seat.sitIdle $= "")
    {
        warn("seat " @ %seat @ " does not specify a sitIdle");
    }
    if (%seat.idleDelay $= "")
    {
        warn("seat " @ %seat @ " does not specify a idleDelay");
    }
    if (%seat.sitSound $= "")
    {
        warn("seat " @ %seat @ " does not specify a sitSound");
    }
    else
    {
        %this.playAudio(0, %seat.sitSound);
    }
    if (isObject(%seat.listeningStation))
    {
        %seat.listeningStation.playThread(0, "start");
        %meshName = %this.getDataBlock().gender @ ".headphones.dj";
        %this.MeshOn(%meshName);
        if (!(%seat.listeningStation.stream $= ""))
        {
            commandToClient(%this.client, 'StartListeningStationAudio', %seat.getId(), %seat.listeningStation.stream);
        }
        else
        {
            warn("listeningStation " @ %seat.listeningStation @ " does not specify a stream, not starting");
        }
    }
    %this.schedule(%seat.idleDelay, "setActionThread", %seat.sitIdle, 0, 0);
    return ;
}
function Player::standUp(%this)
{
    %seat = %this.mySeat;
    if (!isObject(%seat))
    {
        error("Player::standUp: no mySeat member variable");
        return ;
    }
    if (%seat.standSound $= "")
    {
        warn("seat " @ %seat @ " does not specify a standSound");
    }
    else
    {
        %this.playAudio(0, %seat.standSound);
    }
    if (%seat.standAnim $= "")
    {
        warn("seat " @ %seat @ " does not specify a standAnim");
    }
    else
    {
        %this.setActionThread(%seat.standAnim, 0, 0);
    }
    if (isObject(%seat.listeningStation))
    {
        %seat.listeningStation.stopThread(0);
        %seat.listeningStation.playThread(0, "ambient");
        %meshName = %this.getDataBlock().gender @ ".headphones.dj";
        %this.MeshOff(%meshName);
        if (!(%seat.listeningStation.stream $= ""))
        {
            commandToClient(%this.client, 'StopListeningStationAudio', %seat.getId(), %seat.listeningStation.stream);
        }
        else
        {
            warn("listeningStation " @ %seat.listeningStation @ " does not specify a stream, not stopping");
        }
    }
    return ;
}
