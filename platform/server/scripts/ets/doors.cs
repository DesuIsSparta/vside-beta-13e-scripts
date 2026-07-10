datablock StaticShapeData(BaseDoorData) {
    className = "DoorShapeData";
    category = "Doors";
};
datablock StaticShapeData(SlidingDoor : BaseDoorData) {
    shapeFile = "projects/common/worlds/slidingDoor.dts";
};
datablock StaticShapeData(ClubMainDoor : BaseDoorData) {
    shapeFile = "projects/common/worlds/clubmaindoor.dts";
};
datablock StaticShapeData(Deckdoor : BaseDoorData) {
    shapeFile = "projects/common/worlds/deckdoor.dts";
};
datablock StaticShapeData(Ps1upperdoor : BaseDoorData) {
    shapeFile = "projects/common/worlds/ps1upperdoor.dts";
};
datablock StaticShapeData(SecretSteps : BaseDoorData) {
    shapeFile = "projects/common/worlds/secretsteps.dts";
};
function DoorShapeData::onAdd(%this, %obj)
{
    %obj.doorOpen = 0;
    %obj.insideCount = 0;
    return;
}
function DoorShapeData::openDoor(%obj)
{
    if (!%obj.doorOpen)
    {
        %obj.setThreadDir(0, 1);
        0.playThread("open", %obj);
        %obj.doorOpen = 1;
    }
    return;
}
function DoorShapeData::closeDoor(%obj)
{
    if (%obj.doorOpen)
    {
        %obj.setThreadDir(0, 0);
        0.playThread("open", %obj);
        %obj.doorOpen = 0;
    }
    return;
}
datablock TriggerData(DoorTrigger) {
    tickPeriodMS = 200;
    door = "ReplaceMeWith a Door Name";
};
function DoorTrigger::onEnterTrigger(%this, %trigger, %player)
{
    Parent::onEnterTrigger(%this, %trigger, %player);
    %client = %player.client;
    if (!isObject(%client))
    {
        return;
    }
    if (!isObject(%trigger.door))
    {
        error("DoorTrigger::onEnterTrigger:  Did not find door member. must have a door dynamic var to work");
        return;
    }
    if (!%trigger.door.doorOpen)
    {
        DoorShapeData::openDoor(%trigger.door);
    }
    %trigger.door.insideCount = (%trigger.door.insideCount + 1.0);
    return;
}
function DoorTrigger::onTickTrigger(%this, %trigger)
{
    Parent::onTickTrigger(%this, %trigger);
    return;
}
function DoorTrigger::onLeaveTrigger(%this, %trigger, %player)
{
    Parent::onLeaveTrigger(%this, %trigger, %player);
    %client = %player.client;
    if (!isObject(%client))
    {
        return;
    }
    if (!isObject(%trigger.door))
    {
        error("DoorTrigger::onEnterTrigger:  Did not find door member. must have a door dynamic var to work");
        return;
    }
    %trigger.door.insideCount = (%trigger.door.insideCount - 1.0);
    if ((%trigger.door.insideCount == 0.0))
    {
    }
    if (%trigger.door.doorOpen)
    {
        DoorShapeData::closeDoor(%trigger.door);
    }
    return;
}
