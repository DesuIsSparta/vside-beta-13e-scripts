function clientCmdBlockGameEngage(%gameType) {
    setFOV(90);
    %gameType.open(ApplauseMeterGui, "blockgame");
    ConvBub.chooseProfile();
    1.setActivityActive(getUserActivityMgr(), "gaming");
};
function clientCmdBlockGameDisengage() {
    closingFromServer = 1 @ ApplauseMeterGui;
    ApplauseMeterGui.close();
    ConvBub.chooseProfile();
    0.setActivityActive(getUserActivityMgr(), "gaming");
};
