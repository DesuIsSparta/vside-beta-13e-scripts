function SAD(%password)
{
    if (!(%password $= ""))
    {
        commandToServer('SAD', %password);
    }
}
function SADSetPassword(%password)
{
    commandToServer('SADSetPassword', %password);
}
function clientCmdSyncClock(%time)
{
    $Sim::TimeDeltaToServer = %time;
}
function getServerSimTime()
{
    return getSimTime() + $Sim::TimeDeltaToServer;
}
function clientCmdSyncSolarTimeOfDay(%sod)
{
    echo("got solar HOD:" @ " " @ (%sod / (60 * 60)));
    $Sim::TimeDeltaToCity = (%sod * 1000) - getSimTime();
}
