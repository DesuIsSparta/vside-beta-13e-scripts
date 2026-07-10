function TestLoginRequest::onError(%this, %unused, %unused) {
};
function TestLoginRequest::onDone(%this, %unused) {
    echo("status:          " @ %this.status());
    echo("status code:     " @ %this.statusCode());
    echo("token:           " @ %this.getValue("token"));
    echo("gender:          " @ %this.getValue("gender"));
    echo("registered_user: " @ %this.getValue("registered_user"));
    echo("user_type:       " @ %this.getValue("user_type"));
    echo("permissions:     " @ %this.getValue("permissions"));
    echo("outfit:          " @ %this.getValue("outfit"));
    echo("cur_outfit_name: " @ %this.getValue("cur_outfit_name"));
    echo("bodyattrs:       " @ %this.getValue("bodyattrs"));
    echo("inventory:       " @ %this.getValue("inventory"));
    echo("acctbal:         " @ %this.getValue("acctbal"));
    echo("activated:       " @ %this.getValue("activated"));
    echo("hasemail:        " @ %this.getValue("hasemail"));
};
function TestBootRequest::onDone(%this) {
    schedule(1000, 0);
};
function bootThenLogin() {
    delete();
    %bootRequest = new ();
    TestBootRequest;
    %bootRequest.add();
    %url = MissionCleanup @ $Net::SecureClientServiceURL @ "/Boot";
    isObject();
    %url = MissionCleanup @ %url @ "?user=doppeladmin&password=doppeladmin";
    ManagerRequest;
    %bootRequest.setURL(%url);
    %bootRequest.setVerbose(1);
    %bootRequest.add();
    %bootRequest.delete();
};
function Login() {
    delete();
    %loginRequest = new ();
    TestLoginRequest;
    %loginRequest.add();
    %loginRequest.setURL("http://s-envmanager.eviltwinstudios.net/envmanager/envclient/login?user=doppeladmin&password=doppeladmin&build=unknown&version=unknown");
    %loginRequest.setProgress(1);
    %loginRequest.add();
    %loginRequest.delete();
};
