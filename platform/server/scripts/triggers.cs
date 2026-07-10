datablock TriggerData(DefaultTrigger) {
    tickPeriodMS = 100;
};
function DefaultTrigger::onEnterTrigger(%this, %trigger, %obj) {
    Parent::onEnterTrigger(%this, %trigger, %obj);
};
function DefaultTrigger::onLeaveTrigger(%this, %trigger, %obj) {
    Parent::onLeaveTrigger(%this, %trigger, %obj);
};
function DefaultTrigger::onTickTrigger(%this, %trigger) {
    Parent::onTickTrigger(%this, %trigger);
};
datablock TriggerData(MusicTrigger) {
    tickPeriodMS = 500;
};
datablock TriggerData(RespawnTriggerDB) {
    targetSpawnSphere = "";
};
function MusicTrigger::onEnterTrigger(%this, %trigger, %obj) {
    %cmd = addTaggedString(%trigger.getName() @ "Enter");
    commandToClient(%obj.client, %cmd);
    Parent::onEnterTrigger(%this, %trigger, %obj);
};
function MusicTrigger::onLeaveTrigger(%this, %trigger, %obj) {
    %cmd = addTaggedString(%trigger.getName() @ "Exit");
    commandToClient(%obj.client, %cmd);
    Parent::onLeaveTrigger(%this, %trigger, %obj);
};
function RespawnTriggerDB::onEnterTrigger(%this, %trigger, %obj) {
    %obj.teleportToRandomSpawnSphere();
};
