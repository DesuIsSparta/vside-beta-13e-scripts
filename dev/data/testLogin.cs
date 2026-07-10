exec("./skeletonClient.cs");
function testLogin() {
    userName = new ScriptObject(testLogin) @ $UserPref::Player::Name;
    password = $UserPref::Player::Password;
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
