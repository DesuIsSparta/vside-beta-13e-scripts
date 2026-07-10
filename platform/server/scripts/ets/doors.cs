className = datablock StaticShapeData(BaseDoorData) @ "DoorShapeData";
category = "Doors";
shapeFile = datablock StaticShapeData(SlidingDoor : BaseDoorData) @ "projects/common/worlds/slidingDoor.dts";
shapeFile = datablock StaticShapeData(ClubMainDoor : BaseDoorData) @ "projects/common/worlds/clubmaindoor.dts";
shapeFile = datablock StaticShapeData(Deckdoor : BaseDoorData) @ "projects/common/worlds/deckdoor.dts";
shapeFile = datablock StaticShapeData(Ps1upperdoor : BaseDoorData) @ "projects/common/worlds/ps1upperdoor.dts";
shapeFile = datablock StaticShapeData(SecretSteps : BaseDoorData) @ "projects/common/worlds/secretsteps.dts";
function DoorShapeData::onAdd(%this, %obj) {
    doorOpen = 0 @ %obj;
    insideCount = 0 @ %obj;
    return;
};
function DoorShapeData::openDoor(%obj) {
    if (!(doorOpen)) {
        %obj.setThreadDir(0, 1);
        %obj.playThread(0, "open");
        doorOpen = %obj @ 1 @ %obj;
    }
    return;
};
function DoorShapeData::closeDoor(%obj) {
    if (doorOpen) {
        %obj.setThreadDir(0, 0);
        %obj.playThread(0, "open");
        doorOpen = %obj @ 0 @ %obj;
    }
    return;
};
tickPeriodMS = datablock TriggerData(DoorTrigger) @ 200;
door = "ReplaceMeWith a Door Name";
function DoorTrigger::onEnterTrigger(%this, %trigger, %player) {
    Parent::onEnterTrigger(%this, %trigger, %player);
    %client = client;
    %player;
    if (!(isObject(%client))) {
        return;
    }
    if (!(isObject(door))) {
        error("DoorTrigger::onEnterTrigger:  Did not find door member. must have a door dynamic var to work");
        return %trigger;
    }
    if (!(doorOpen)) {
        DoorShapeData::openDoor(door);
    }
    insideCount = %trigger @ door;
    %trigger @ (door + insideCount);
    return 1.0;
};
function DoorTrigger::onTickTrigger(%this, %trigger) {
    Parent::onTickTrigger(%this, %trigger);
    return;
};
function DoorTrigger::onLeaveTrigger(%this, %trigger, %player) {
    Parent::onLeaveTrigger(%this, %trigger, %player);
    %client = client;
    %player;
    if (!(isObject(%client))) {
        return;
    }
    if (!(isObject(door))) {
        error("DoorTrigger::onEnterTrigger:  Did not find door member. must have a door dynamic var to work");
        return %trigger;
    }
    insideCount = %trigger @ door;
    %trigger @ (door - insideCount);
    if ((door == insideCount)) {
    }
    if (doorOpen) {
        DoorShapeData::closeDoor(door);
    }
    return %trigger;
};
