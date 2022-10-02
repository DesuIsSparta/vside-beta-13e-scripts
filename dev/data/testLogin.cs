exec("./skeletonClient.cs");
function testLogin()
{
    %testLogin = new ScriptObject(testLogin)
    {
        userName = $UserPref::Player::Name;
        password = $UserPref::Player::Password;
        joinAction = "joinAction";
    };
    %testLogin.init();
    %testLogin.doLogin("NewVenezia");
    return ;
}
function joinAction()
{
    logout();
    quit();
    return ;
}
testLogin();

