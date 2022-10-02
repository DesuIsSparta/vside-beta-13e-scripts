function clientCmdMissionStart(%seq)
{
    log("network", "debug", "clientCmdMissionStart seq:" SPC %seq);
    if (!isObject(FMod))
    {
        Music::init();
    }
    return ;
}
function clientCmdMissionEnd(%seq)
{
    log("network", "debug", "clientCmdMissionEnd seq:" SPC %seq);
    alxStopAll();
    $lightingMission = 0;
    $sceneLighting::terminateLighting = 1;
    return ;
}
