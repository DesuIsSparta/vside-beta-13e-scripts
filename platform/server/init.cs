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
    if (($Pref::Server::MonitorPort != 0.0)) {
        $Pref::Server::MonitorPort.listen(%mon);
    }
    28000.listen(%mon);
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
    %post.post(%stopRequest, %host, %uri, %query);
};
function StopRequest::onStatus(%unused) {
    doQuit();
};
