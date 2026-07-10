function initServer() {
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
};
function Monitor::onConnected(%unused) {
    echo("Monitor Connected");
};
function Monitor::onConnectFailed(%unused) {
    echo("Monitor Connection Failed");
};
function Monitor::onDisconnect(%unused) {
    echo("Monitor Disconnected");
};
function Monitor::onLine(%unused, %line) {
    echo("Monitor TCP: " @ %line);
};
function Monitor::onConnectRequest(%unused, %unused, %id) {
    echo("Monitor Accept: " @ %id);
};
function openMonitorSocket() {
    %mon = new TCPObject(Monitor);
    if ((0.0 != $Pref::Server::MonitorPort)) {
        %mon.listen($Pref::Server::MonitorPort);
    }
    %mon.listen(28000);
};
function initDedicated() {
    enableWinConsole(1);
    echo("\n--------- Starting Dedicated Server ---------");
    $Server::Dedicated = 1;
    if (!($missionArg $= "")) {
        openMonitorSocket();
        createServer("MultiPlayer", $missionArg);
    }
    echo("No mission specified (use -mission filename)");
};
function quitApp() {
    echo("Server quitting");
    if (!($StandAlone)) {
    }
    if ($AmServer) {
        stopServer();
    }
    doQuit();
};
function stopServer() {
    %stopRequest = new HTTPObject(StopRequest);
    %host = $Pref::Server::ManagerAddress @ ":" @ $Pref::Server::ManagerHTTPPort;
    %uri = "/envmanager/status";
    %query = "cmd=stop";
    %post = "";
    %stopRequest.post(%host, %uri, %query, %post);
};
function StopRequest::onStatus(%unused) {
    doQuit();
};
