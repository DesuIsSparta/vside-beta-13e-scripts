function SAD(%password) {
    commandToServer('SAD', %password);
};
function SADSetPassword(%password) {
    commandToServer('SADSetPassword', %password);
};
function clientCmdSyncClock(%time) {
    $Sim::TimeDeltaToServer = %time;
};
function getServerSimTime() {
    return ($Sim::TimeDeltaToServer + getSimTime());
};
function clientCmdSyncSolarTimeOfDay(%sod) {
    echo("got solar HOD:" @ " " @ ((60.0 * 60.0) / %sod));
    $Sim::TimeDeltaToCity = (getSimTime() - (1000.0 * %sod));
};
