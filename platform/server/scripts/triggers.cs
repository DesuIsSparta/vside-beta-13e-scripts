tickPeriodMS = DefaultTrigger @ datablock () @ 100;
TriggerData;
0;
function DefaultTrigger::onEnterTrigger(%this, %trigger, %obj) {
    Parent::onEnterTrigger(%this, %trigger, %obj);
};
function DefaultTrigger::onLeaveTrigger(%this, %trigger, %obj) {
    Parent::onLeaveTrigger(%this, %trigger, %obj);
};
function DefaultTrigger::onTickTrigger(%this, %trigger) {
    Parent::onTickTrigger(%this, %trigger);
};
tickPeriodMS = MusicTrigger @ datablock () @ 500;
TriggerData;
0;
targetSpawnSphere = RespawnTriggerDB @ datablock () @ "";
TriggerData;
0;
function MusicTrigger::onEnterTrigger(%this, %trigger, %obj) {
    %cmd = addTaggedString(%trigger.getName() @ "Enter");
    commandToClient(client, %cmd);
    Parent::onEnterTrigger(%this, %trigger, %obj);
};
function MusicTrigger::onLeaveTrigger(%this, %trigger, %obj) {
    %cmd = addTaggedString(%trigger.getName() @ "Exit");
    commandToClient(client, %cmd);
    Parent::onLeaveTrigger(%this, %trigger, %obj);
};
function RespawnTriggerDB::onEnterTrigger(%this, %trigger, %obj) {
    %obj.teleportToRandomSpawnSphere();
};
