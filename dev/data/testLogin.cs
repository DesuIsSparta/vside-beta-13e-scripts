exec("./skeletonClient.cs");
function testLogin() {
    userName = testLogin @ new () @ $UserPref::Player::Name;
    ScriptObject;
    password = 0 @ $UserPref::Player::Password;
    joinAction = "joinAction";
    %testLogin = ;
    %testLogin.init();
    %testLogin.doLogin("NewVenezia");
};
function joinAction() {
    logout();
    quit();
};
testLogin();
