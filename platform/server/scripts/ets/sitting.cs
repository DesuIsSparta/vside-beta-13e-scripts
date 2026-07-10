category = datablock MissionMarkerData(SeatMarker) @ "markers";
shapeFile = "projects/common/worlds/markers/arrowmarker.dts";
sitOffset = "0 0 0";
sitAnim = "cent";
standAnim = "cext";
sitIdle = "cidl2a";
sitSound = "replaceme";
standSound = "replaceme";
idleDelay = 500;
listeningStation = 0;
shapeFile = datablock EtsClientModelData(ClientSeatDisplayData) @ "projects/common/worlds/seatdisplay.dts";
dynamicType = $TypeMasks::UsableObjectType;
sequence = "ambient";
sequenceRate = 1;
shapeFile = datablock EtsClientModelData(ClientSeatListeningDisplayData : ClientSeatDisplayData) @ "projects/common/worlds/headphones.dts";
function InitSittingSystem() {
    initSeatsTakenSet();
    return;
};
$SeatsTakenSet = 0;
function initSeatsTakenSet() {
    if (isObject($SeatsTakenSet)) {
        $SeatsTakenSet.delete();
    }
    $SeatsTakenSet = new ""();
    SimSet;
    $SeatsTakenSet.add();
    return MissionCleanup;
};
function isSeatTaken(%seat) {
    if (!(isObject($SeatsTakenSet))) {
        warn("isSeatTaken: $SeatsTakenSet must be initialized first");
        return 0;
    }
    return $SeatsTakenSet.isMember(%seat);
};
function takeSeat(%seat) {
    if (!(isObject($SeatsTakenSet))) {
        warn("takeSeat: $SeatsTakenSet must be initialized first");
        return;
    }
    if ($SeatsTakenSet.isMember(%seat)) {
        warn("takeSeat: taking a seat that is already taken: " @ %seat);
    }
    $SeatsTakenSet.add(%seat);
    return;
};
function freeSeat(%seat) {
    if (!(isObject($SeatsTakenSet))) {
        warn("freeSeat: $SeatsTakenSet must be initialized first");
        return;
    }
    if (!($SeatsTakenSet.isMember(%seat))) {
        warn("freeSeat: freeing a seat that is not taken yet: " @ %seat);
    }
    $SeatsTakenSet.remove(%seat);
    return;
};
function showClientAvailableSeats(%seats, %client) {
    %i = 0;
    if ((%seats.getCount() < %i)) {
        %seat = %seats.getObject(%i);
        if (!(isSeatTaken(%seat))) {
            %seatType = "ClientSeatDisplayData";
            if (isObject(listeningStation)) {
                %seatType = "ClientSeatListeningDisplayData";
                %seat;
            }
            commandToClient(%client, 'ShowSeat', %seat.getId(), %seat.getTransform(), %seatType);
        }
        %i = (1.0 + %i);
    }
};
function showPossibleSeats(%seats, %client) {
    showClientAvailableSeats(%seats, %client);
    return;
};
function hideClientSeats(%seats, %client) {
    %count = %seats.getCount();
    if ((0.0 > %count)) {
        %seatList = "";
        %i = 0;
        if ((%count < %i)) {
            %seat = %seats.getObject(%i);
            %seatList = %seatList @ " " @ %seat.getId();
            %i = (1.0 + %i);
        }
        commandToClient(%client, 'HideSeats', %seatList);
    }
    return (%count < %i);
};
function hidePossibleSeats(%seats, %client) {
    hideClientSeats(%seats, %client);
    return;
};
function TurnOnSitCam(%player) {
    %client = client;
    %player;
    if (isObject(%client)) {
        if (!(isObject(sitCam))) {
            dataBlock = new ""() @ SittingObserver;
            Camera;
            sitCam = %client @ 0 @ %client;
            sitCam.add();
            sitCam.scopeToClient(%client);
        }
        %theta = getWord(camAngles, 0);
        mySeat;
        %phi = getWord(camAngles, 1);
        mySeat;
        if ((%player SPC %theta $= "")) {
            %theta = 180;
            %player;
            %phi = 20;
            %client;
        }
        %theta = mDegToRad(%theta);
        %client;
        %phi = mDegToRad(%phi);
        MissionCleanup;
        %rot = getOrientationRelativeToObject(%player, %theta, %phi);
        sitCam.setOrbitMode(%player, "0 0 0" @ " " @ %rot, 0.5, 2.5, 1.5);
        %client.setControlObject(sitCam);
    }
    return %client;
};
function TurnOffSitCam(%player, %delay) {
    %client = client;
    %player;
    if (!(%delay)) {
        %delay = 500;
    }
    if (isObject(%client)) {
        %client.schedule(%delay, "setControlObject", %player);
    }
    return;
};
function SitPlayerDown(%player, %seatID) {
    if (isSitting) {
        error("SitPlayerDown: player should not already be sitting, fix this!");
    }
    takeSeat(%seatID);
    mySeat = %player @ %seatID @ %player;
    isSitting = 1 @ %player;
    %player.sitDown();
    TurnOnSitCam(%player);
    return;
};
function NeedSeatRefresh(%player) {
    needRefreshVisibleSeats = 1 @ %player;
    return;
};
function StandPlayerUp(%player, %moveDir) {
    if (!(isSitting)) {
        error("StandPlayerUp: player should be already sitting, fix this!");
    }
    if (isObject(listeningStation)) {
    }
    if ((0.0 < %moveDir)) {
        return 0;
    }
    %player.standUp();
    freeSeat(mySeat);
    mySeat = %player @ 0 @ %player;
    isSitting = 0 @ %player;
    %msUntilPlayerControl = 500;
    TurnOffSitCam(%player, %msUntilPlayerControl);
    schedule(%msUntilPlayerControl, 0, "NeedSeatRefresh", %player);
    return 1;
    return;
};
function serverCmdRequestToSit(%client, %seatID) {
    if (!(isObject(Player))) {
        error(%client @ "serverCmdRequestToSit: client" @ %client @ " has no player.");
        return;
    }
    if (!(isObject(%seatID))) {
        error("serverCmdRequestToSit: bad seat ID: " @ %seatID);
        return;
    }
    if (isSitting) {
        error(%client @ Player);
        return Player @ "serverCmdRequestToSit: player already sitting ";
    }
    if (isSeatTaken(%seatID)) {
        commandToClient(%client, 'SeatWasTaken', %seatID);
        return;
    }
    SitPlayerDown(Player, %seatID);
    commandToClient(%client, 'SitRequestSuccessful', %seatID);
    return %client;
};
function serverCmdRequestToStand(%client, %moveDir) {
    if (!(isObject(Player))) {
        error("serverCmdRequestToStand: bad player object");
        return %client;
    }
    %ret = StandPlayerUp(Player, %moveDir);
    %client;
    if (%ret) {
        commandToClient(%client, 'StandRequestSuccessful', %seatID);
    }
    return;
};
tickPeriodMS = datablock TriggerData(SeatingArea) @ 200;
seats = "ReplaceMeWith a Sim Group Name";
function SeatingArea::onEnterTrigger(%this, %trigger, %player) {
    Parent::onEnterTrigger(%this, %trigger, %player);
    %client = client;
    %player;
    if (!(isObject(%client))) {
        return;
    }
    if (!(isObject(seats))) {
        error("SeatingArea::onEnterTrigger:  Did not find seats SimGroup member. Must have group of seats to function");
        return %trigger;
    }
    if (!(isObject($SeatsTakenSet))) {
        needRefreshVisibleSeats = 1 @ %player;
        return;
    }
    showPossibleSeats(seats, %client);
    needRefreshVisibleSeats = %trigger @ 0 @ %player;
    return;
};
function SeatingArea::onTickTrigger(%this, %trigger) {
    Parent::onTickTrigger(%this, %trigger);
    if (!(isObject($SeatsTakenSet))) {
        return;
    }
    %n = 0;
    if ((%trigger.getNumObjects() < %n)) {
        %obj = %trigger.getObject(%n);
        if (needRefreshVisibleSeats) {
        }
        if (isObject(client)) {
            showPossibleSeats(seats, client);
            needRefreshVisibleSeats = %obj @ 0 @ %obj;
            %trigger;
        }
        %n = (1.0 + %n);
        %obj;
    }
};
function SeatingArea::onLeaveTrigger(%this, %trigger, %player) {
    Parent::onLeaveTrigger(%this, %trigger, %player);
    if (!(isObject($SeatsTakenSet))) {
        return;
    }
    %client = client;
    %player;
    if (!(isObject(%client))) {
        return;
    }
    if (!(isObject(seats))) {
        return %trigger;
    }
    hidePossibleSeats(seats, %client);
    return %trigger;
};
mode = datablock CameraData(SittingObserver) @ "Observer";
cameraMinFov = 30;
cameraMaxFov = 120;
function SittingObserver::onTrigger(%this, %camera, %trigger, %state) {
    if ((0.0 == %state)) {
        return;
    }
    %client = %camera.getControllingClient();
    return;
};
function SittingObserver::setMode(%this, %obj, %mode, %unused, %unused, %unused) {
    if ((%mode $= "Observer")) {
        %obj.setFlyMode();
    }
    mode = %mode @ %obj;
    return;
};
function Player::sitDown(%this) {
    %seat = mySeat;
    %this;
    if (!(isObject(%seat))) {
        error("Player::sitDown: no mySeat member variable");
        return;
    }
    %transform = %seat.getTransform();
    %pos = getWords(%transform, 0, 2);
    %rot = getWords(%transform, 3, 6);
    %pos = VectorAdd(%pos, sitOffset);
    %seat;
    %transform = %pos @ " " @ %rot;
    %this.setTransform(%transform);
    if ((%seat SPC sitAnim $= "")) {
        warn("seat " @ %seat @ " does not specify a sitAnim");
    }
    %this.setActionThread(sitAnim, 1, 1);
    if ((%seat SPC sitIdle $= "")) {
        warn(%seat @ "seat " @ %seat @ " does not specify a sitIdle");
    }
    if ((%seat SPC idleDelay $= "")) {
        warn("seat " @ %seat @ " does not specify a idleDelay");
    }
    if ((%seat SPC sitSound $= "")) {
        warn("seat " @ %seat @ " does not specify a sitSound");
    }
    %this.playAudio(0, sitSound);
    if (isObject(listeningStation)) {
        listeningStation.playThread(0, "start");
        %meshName = %this.getDataBlock() @ gender @ ".headphones.dj";
        %seat;
        %this.MeshOn(%meshName);
        if (!(listeningStation SPC stream $= "")) {
            commandToClient(client, 'StartListeningStationAudio', %seat.getId(), stream);
        }
        warn(listeningStation @ "listeningStation " @ %seat @ listeningStation @ " does not specify a stream, not starting");
    }
    %this.schedule(idleDelay, "setActionThread", sitIdle, 0, 0);
    return %seat;
};
function Player::standUp(%this) {
    %seat = mySeat;
    %this;
    if (!(isObject(%seat))) {
        error("Player::standUp: no mySeat member variable");
        return;
    }
    if ((%seat SPC standSound $= "")) {
        warn("seat " @ %seat @ " does not specify a standSound");
    }
    %this.playAudio(0, standSound);
    if ((%seat SPC standAnim $= "")) {
        warn(%seat @ "seat " @ %seat @ " does not specify a standAnim");
    }
    %this.setActionThread(standAnim, 0, 0);
    if (isObject(listeningStation)) {
        listeningStation.stopThread(0);
        listeningStation.playThread(0, "ambient");
        %meshName = %this.getDataBlock() @ gender @ ".headphones.dj";
        %seat;
        %this.MeshOff(%meshName);
        if (!(listeningStation SPC stream $= "")) {
            commandToClient(client, 'StopListeningStationAudio', %seat.getId(), stream);
        }
        warn(listeningStation @ "listeningStation " @ %seat @ listeningStation @ " does not specify a stream, not stopping");
    }
    return %seat;
};
