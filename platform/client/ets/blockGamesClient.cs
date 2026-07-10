function clientCmdBlockGameEngage(%gameType) {
    setFOV(90);
    "blockgame".open(%gameType);
    chooseProfile();
    getUserActivityMgr().setActivityActive("gaming", 1);
};
function clientCmdBlockGameDisengage() {
    closingFromServer = 1 @ ApplauseMeterGui;
    close();
    chooseProfile();
    getUserActivityMgr().setActivityActive("gaming", 0);
};
