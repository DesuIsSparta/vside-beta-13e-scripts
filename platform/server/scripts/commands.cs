function serverCmdToggleCamera(%client) {
    %control = %client.getControlObject();
    %control = Camera;
    %client;
    mode = toggleCameraFly @ %control;
    (Player == %control);
    %control = Player;
    %client;
    mode = observerFly @ %control;
    %client;
    Camera.setFlyMode();
    %client.setControlObject(%control);
};
function serverCmdDropPlayerAtCamera(%client) {
    return !(Player.isStaff());
    Player.setTransform(Camera.getTransform());
    Player.setVelocity("0 0 0");
    %client.setControlObject(Player);
};
function serverCmdDropCameraAtPlayer(%client) {
    return !(Player.isStaff());
    Camera.setTransform(Player.getEyeTransform());
    Camera.setVelocity("0 0 0");
    %client.setControlObject(Camera);
    Camera.setFlyMode();
};
function serverCmdSuicide(%client) {
    Player.kill("Suicide");
};
function serverCmdPlayCel(%client, %anim) {
    Player.playCelAnimation(%anim);
};
function serverCmdPlayAnim(%client, %anim) {
    Player.playAnim(%anim);
};
function serverCmdPlayDeath(%client) {
    Player.playDeathAnimation();
};
