function TestLoginRequest::onError(%this, %unused, %unused)
{
    return ;
}
function TestLoginRequest::onDone(%this, %unused)
{
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
    return ;
}
function TestBootRequest::onDone(%this)
{
    schedule(1000, 0, Login);
    return ;
}
function bootThenLogin()
{
    if (isObject(TestBootRequest))
    {
        TestBootRequest.delete();
    }
    %bootRequest = new ManagerRequest(TestBootRequest);
    if (isObject(MissionCleanup))
    {
        MissionCleanup.add(%bootRequest);
    }
    %url = $Net::SecureClientServiceURL @ "/Boot";
    %url = %url @ "?user=doppeladmin&password=doppeladmin";
    %bootRequest.setURL(%url);
    %bootRequest.setVerbose(1);
    if (%bootRequest.start())
    {
        CURLSimGroup.add(%bootRequest);
    }
    else
    {
        %bootRequest.delete();
    }
    return ;
}
function Login()
{
    if (isObject(TestLoginRequest))
    {
        TestLoginRequest.delete();
    }
    %loginRequest = new ManagerRequest(TestLoginRequest);
    if (isObject(MissionCleanup))
    {
        MissionCleanup.add(%loginRequest);
    }
    %loginRequest.setURL("http://s-envmanager.eviltwinstudios.net/envmanager/envclient/login?user=doppeladmin&password=doppeladmin&build=unknown&version=unknown");
    %loginRequest.setProgress(1);
    if (%loginRequest.start())
    {
        CURLSimGroup.add(%loginRequest);
    }
    else
    {
        %loginRequest.delete();
    }
    return ;
}
