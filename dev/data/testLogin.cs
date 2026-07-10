exec("./skeletonClient.cs");
function testLogin() {
    %testLogin = new ScriptObject(testLogin) {
        userName = $UserPref::Player::Name;
        password = $UserPref::Player::Password;
        joinAction = "joinAction";
    };
    %testLogin.init();
    "NewVenezia".doLogin(%testLogin);
};
function joinAction() {
    logout();
    quit();
};
testLogin();
