function initServer()
{
    echo("--------- Initializing MOD: Intersection: Server ---------");
    $AmServer = 1;
    $Server::Status = "Unknown";
    $Server::TestCheats = 1;
    $Server::MissionFileSpec = "*/missions/*.mis";
    initBaseServer();
    exec("./scripts/commands.cs");
    exec("./scripts/centerPrint.cs");
    exec("./scripts/game.cs");
    exec("./scripts/audio.cs");
    exec("./scripts/ets/init.cs");
    return ;
}
function Monitor::onConnected(%unused)
{
    echo("Monitor Connected");
    return ;
}
function Monitor::onConnectFailed(%unused)
{
    echo("Monitor Connection Failed");
    return ;
}
function Monitor::onDisconnect(%unused)
{
    echo("Monitor Disconnected");
    return ;
}
function Monitor::onLine(%unused, %line)
{
    echo("Monitor TCP: " @ %line);
    return ;
}
function Monitor::onConnectRequest(%unused, %unused, %id)
{
    echo("Monitor Accept: " @ %id);
    return ;
}
function openMonitorSocket()
{
    %mon = new TCPObject(Monitor);
    if ($Pref::Server::MonitorPort != 0)
    {
        %mon.listen($Pref::Server::MonitorPort);
    }
    else
    {
        %mon.listen(28000);
    }
    return ;
}
function initDedicated()
{
    enableWinConsole(1);
    echo("\n--------- Starting Dedicated Server ---------");
    $Server::Dedicated = 1;
    if (!($missionArg $= ""))
    {
        openMonitorSocket();
        createServer("MultiPlayer", $missionArg);
    }
    else
    {
        echo("No mission specified (use -mission filename)");
    }
    return ;
}
function quitApp()
{
    echo("Server quitting");
    if (!$StandAlone && $AmServer)
    {
        stopServer();
    }
    else
    {
        doQuit();
    }
    return ;
}
function stopServer()
{
    %stopRequest = new HTTPObject(StopRequest);
    %host = $Pref::Server::ManagerAddress @ ":" @ $Pref::Server::ManagerHTTPPort;
    %uri = "/envmanager/status";
    %query = "cmd=stop";
    %post = "";
    %stopRequest.post(%host, %uri, %query, %post);
    return ;
}
function StopRequest::onStatus(%unused)
{
    doQuit();
    return ;
}
