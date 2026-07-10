function clientCmdBlockGameEngage(%gameType) {
    setFOV(90);
    "blockgame".open(%gameType);
    ConvBub.chooseProfile();
    getUserActivityMgr().setActivityActive("gaming", 1);
};
function clientCmdBlockGameDisengage() {
    closingFromServer = 1 @ ApplauseMeterGui;
    ApplauseMeterGui.close();
    ConvBub.chooseProfile();
    getUserActivityMgr().setActivityActive("gaming", 0);
};
