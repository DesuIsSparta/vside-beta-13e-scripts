function clientCmdMissionStart(%seq) {
    log("network", "debug", "clientCmdMissionStart seq:" @ " " @ %seq);
    if (!isObject(FMod)) {
        Music::init();
    }
};
function clientCmdMissionEnd(%seq) {
    log("network", "debug", "clientCmdMissionEnd seq:" @ " " @ %seq);
    alxStopAll();
    $lightingMission = 0;
    $sceneLighting::terminateLighting = 1;
};
