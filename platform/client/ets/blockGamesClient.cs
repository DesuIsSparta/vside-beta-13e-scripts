function clientCmdBlockGameEngage(%gameType)
{
    setFOV(90);
    ApplauseMeterGui.open("blockgame", %gameType);
    ConvBub.chooseProfile();
    getUserActivityMgr().setActivityActive("gaming", 1);
}
function clientCmdBlockGameDisengage()
{
    ApplauseMeterGui.closingFromServer = 1;
    ApplauseMeterGui.close();
    ConvBub.chooseProfile();
    getUserActivityMgr().setActivityActive("gaming", 0);
}
