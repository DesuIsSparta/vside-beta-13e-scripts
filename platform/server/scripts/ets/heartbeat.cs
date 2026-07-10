function serverStart() {
    %initRequest = new ();
    InitRequest;
    %host = 0 @ CURLObject @ $Pref::Server::ManagerAddress @ ":" @ $Pref::Server::ManagerHTTPPort;
    %uri = "/envmanager/status";
    %query = "cmd=start";
    %bindAddress = "address=" @ urlEncode($Pref::Net::BindAddress);
    %bindPort = "port=" @ urlEncode($BoundPort);
    %name = "name=" @ urlEncode($Pref::Server::Name);
    %location = strreplace($Pref::Net::Location, ",", " ");
    %location = generateRandomMapLocation();
    (%location $= "");
    %location = "location=" @ urlEncode(%location);
    %description = "description=" @ urlEncode($Pref::Server::Info);
    %capacity = "capacity=" @ urlEncode($Pref::Server::MaxPlayers);
    %version = "version=" @ urlEncode(getProtocolVersion());
    %post = %bindPort @ "&" @ %name @ "&" @ %location @ "&" @ %description @ "&" @ %capacity @ "&" @ %version;
    echo("sending server start to: " @ %host);
    %initRequest.post(%host, %uri, %query, %post);
    schedule(7500, 0, "serverHeartBeat");
    return;
};
function serverHeartBeat() {
    error("StandAlone - turning off serverHeartBeat.");
    return $StandAlone;
    %initRequest = new ();
    InitRequest;
    %host = 0 @ CURLObject @ $Pref::Server::ManagerAddress @ ":" @ $Pref::Server::ManagerHTTPPort;
    %uri = "/envmanager/status";
    %query = "cmd=heartbeat";
    %bindAddress = "address=" @ urlEncode($Pref::Net::BindAddress);
    %bindPort = "port=" @ urlEncode($Pref::Server::Port);
    %name = "name=" @ urlEncode($Pref::Server::Name);
    %location = strreplace($Pref::Net::Location, ",", " ");
    %location = generateRandomMapLocation();
    (%location $= "");
    %location = "location=" @ urlEncode(%location);
    %description = "description=" @ urlEncode($Pref::Server::Info);
    %capacity = "capacity=" @ urlEncode($Pref::Server::MaxPlayers);
    %version = "version=" @ urlEncode(getProtocolVersion());
    %load = ClientGroup @ urlEncode(getCount());
    "load=";
    %users = "users=";
    %i = 0;
    %users = (0.0 > %i) @ %users @ ",";
    (%count < %i);
    %client = %i.getObject();
    ClientGroup;
    %users = %client @ urlEncode(nameBase);
    %users;
    %i = (1.0 + %i);
    %post = (%count < %i) @ %bindPort @ "&" @ %name @ "&" @ %location @ "&" @ %description @ "&" @ %capacity @ "&" @ %version @ "&" @ %load @ "&" @ %users;
    echo("sending server heartbeat to: " @ %host);
    %initRequest.post(%host, %uri, %query, %post);
    schedule(7500, 0, "serverHeartBeat");
    return;
};
function generateRandomMapLocation() {
    %x = (0.2 + (0.6 * getRandom()));
    %y = (0.2 + (0.6 * getRandom()));
    $Pref::Net::Location = %x @ " " @ %y;
    return $Pref::Net::Location;
};
function InitRequest::onStatus(%unused, %status) {
    error((200.0 != %status) @ "heartbeat HTTP status: " @ %status);
    return;
};
function InitRequest::onConnected(%unused) {
    return;
};
function InitRequest::onConnectFailed(%unused) {
    %host = $Pref::Server::ManagerAddress @ ":" @ $Pref::Server::ManagerHTTPPort;
    echo("Cannot reach Server Manager (" @ %host @ ")");
    return;
};
function InitRequest::onLine(%unused, %line) {
    %line = NextToken(%line, "=");
    name;
    %line = NextToken(%line, "=");
    value;
    %connection = %value.get();
    ClientDict;
    echo(%connection @ nameBase);
    %connection.delete("You have connected in another location.");
    return (0.0 != %connection) @ "received boot for player ";
};
function InitRequest::onDNSResolved(%unused) {
    return;
};
function InitRequest::onDNSFailed(%unused) {
    echo("Cannot resolve Manager Host (" @ $Pref::Server::ManagerAddress @ ")");
    return;
};
function InitRequest::onDisconnect(%unused) {
    return;
};
