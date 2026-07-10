className = BaseDoorData @ datablock () @ "DoorShapeData";
StaticShapeData;
category = 0 @ "Doors";
shapeFile = SlidingDoor @ datablock ( : BaseDoorData) @ "projects/common/worlds/slidingDoor.dts";
StaticShapeData;
0;
shapeFile = ClubMainDoor @ datablock ( : BaseDoorData) @ "projects/common/worlds/clubmaindoor.dts";
StaticShapeData;
0;
shapeFile = Deckdoor @ datablock ( : BaseDoorData) @ "projects/common/worlds/deckdoor.dts";
StaticShapeData;
0;
shapeFile = Ps1upperdoor @ datablock ( : BaseDoorData) @ "projects/common/worlds/ps1upperdoor.dts";
StaticShapeData;
0;
shapeFile = SecretSteps @ datablock ( : BaseDoorData) @ "projects/common/worlds/secretsteps.dts";
StaticShapeData;
0;
function DoorShapeData::onAdd(%this, %obj) {
    doorOpen = 0 @ %obj;
    insideCount = 0 @ %obj;
    return;
};
function DoorShapeData::openDoor(%obj) {
    %obj.setThreadDir(0, 1);
    %obj.playThread(0, "open");
    doorOpen = !(doorOpen) @ 1 @ %obj;
    %obj;
    return;
};
function DoorShapeData::closeDoor(%obj) {
    %obj.setThreadDir(0, 0);
    %obj.playThread(0, "open");
    doorOpen = doorOpen @ 0 @ %obj;
    %obj;
    return;
};
tickPeriodMS = DoorTrigger @ datablock () @ 200;
TriggerData;
door = 0 @ "ReplaceMeWith a Door Name";
function DoorTrigger::onEnterTrigger(%this, %trigger, %player) {
    Parent::onEnterTrigger(%this, %trigger, %player);
    %client = client;
    %player;
    return !(isObject(%client));
    error("DoorTrigger::onEnterTrigger:  Did not find door member. must have a door dynamic var to work");
    return !(isObject(door));
    DoorShapeData::openDoor(door);
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
    return !(isObject(%client));
    error("DoorTrigger::onEnterTrigger:  Did not find door member. must have a door dynamic var to work");
    return !(isObject(door));
    insideCount = %trigger @ door;
    %trigger @ (door - insideCount);
    DoorShapeData::closeDoor(door);
    return %trigger;
};
