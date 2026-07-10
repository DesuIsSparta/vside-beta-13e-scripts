function TestLoginRequest::onError(%this, %unused, %unused) {
};
function TestLoginRequest::onDone(%this, %unused) {
    echo("status:          " @ %this.status());
    echo("status code:     " @ %this.statusCode());
    echo("token:           " @ "token".getValue(%this));
    echo("gender:          " @ "gender".getValue(%this));
    echo("registered_user: " @ "registered_user".getValue(%this));
    echo("user_type:       " @ "user_type".getValue(%this));
    echo("permissions:     " @ "permissions".getValue(%this));
    echo("outfit:          " @ "outfit".getValue(%this));
    echo("cur_outfit_name: " @ "cur_outfit_name".getValue(%this));
    echo("bodyattrs:       " @ "bodyattrs".getValue(%this));
    echo("inventory:       " @ "inventory".getValue(%this));
    echo("acctbal:         " @ "acctbal".getValue(%this));
    echo("activated:       " @ "activated".getValue(%this));
    echo("hasemail:        " @ "hasemail".getValue(%this));
};
function TestBootRequest::onDone(%this) {
    schedule(1000, 0, Login);
};
function bootThenLogin() {
    if (isObject(TestBootRequest)) {
        TestBootRequest.delete();
    }
    %bootRequest = new ManagerRequest(TestBootRequest);
    if (isObject(MissionCleanup)) {
        %bootRequest.add(MissionCleanup);
    }
    %url = $Net::SecureClientServiceURL @ "/Boot";
    %url = %url @ "?user=doppeladmin&password=doppeladmin";
    %url.setURL(%bootRequest);
    1.setVerbose(%bootRequest);
    if (%bootRequest.start()) {
        %bootRequest.add(CURLSimGroup);
    }
    %bootRequest.delete();
};
function Login() {
    if (isObject(TestLoginRequest)) {
        TestLoginRequest.delete();
    }
    %loginRequest = new ManagerRequest(TestLoginRequest);
    if (isObject(MissionCleanup)) {
        %loginRequest.add(MissionCleanup);
    }
    "http://s-envmanager.eviltwinstudios.net/envmanager/envclient/login?user=doppeladmin&password=doppeladmin&build=unknown&version=unknown".setURL(%loginRequest);
    1.setProgress(%loginRequest);
    if (%loginRequest.start()) {
        %loginRequest.add(CURLSimGroup);
    }
    %loginRequest.delete();
};
