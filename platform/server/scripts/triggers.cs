datablock TriggerData(DefaultTrigger);
function DefaultTrigger::onEnterTrigger(%this, %trigger, %obj)
{
    Parent::onEnterTrigger(%this, %trigger, %obj);
    return ;
}
function DefaultTrigger::onLeaveTrigger(%this, %trigger, %obj)
{
    Parent::onLeaveTrigger(%this, %trigger, %obj);
    return ;
}
function DefaultTrigger::onTickTrigger(%this, %trigger)
{
    Parent::onTickTrigger(%this, %trigger);
    return ;
}
datablock TriggerData(MusicTrigger);
datablock TriggerData(RespawnTriggerDB);
function MusicTrigger::onEnterTrigger(%this, %trigger, %obj)
{
    %cmd = addTaggedString(%trigger.getName() @ "Enter");
    commandToClient(%obj.client, %cmd);
    Parent::onEnterTrigger(%this, %trigger, %obj);
    return ;
}
function MusicTrigger::onLeaveTrigger(%this, %trigger, %obj)
{
    %cmd = addTaggedString(%trigger.getName() @ "Exit");
    commandToClient(%obj.client, %cmd);
    Parent::onLeaveTrigger(%this, %trigger, %obj);
    return ;
}
function RespawnTriggerDB::onEnterTrigger(%this, %trigger, %obj)
{
    %obj.teleportToRandomSpawnSphere();
    return ;
}
