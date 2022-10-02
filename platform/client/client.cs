function SAD(%password)
{
    if (!(%password $= ""))
    {
        commandToServer('SAD', %password);
    }
    return ;
}
function SADSetPassword(%password)
{
    commandToServer('SADSetPassword', %password);
    return ;
}
function clientCmdSyncClock(%time)
{
    $Sim::TimeDeltaToServer = %time;
    return ;
}
function getServerSimTime()
{
    return getSimTime() + $Sim::TimeDeltaToServer;
}
function clientCmdSyncSolarTimeOfDay(%sod)
{
    echo("got solar HOD:" SPC %sod / (60 * 60));
    $Sim::TimeDeltaToCity = (%sod * 1000) - getSimTime();
    return ;
}
