function serverCmdToggleCamera(%client) {
    %control = %client.getControlObject();
    if ((Player == %control)) {
        %control = Camera;
        %client;
        mode = toggleCameraFly @ %control;
        %client;
    }
    %control = Player;
    %client;
    mode = observerFly @ %control;
    Camera.setFlyMode();
    %client.setControlObject(%control);
};
function serverCmdDropPlayerAtCamera(%client) {
    if (!(Player.isStaff())) {
        return %client;
    }
    if ($Server::TestCheats) {
    }
    if (isObject()) {
        Player.setTransform(Camera.getTransform());
        Player.setVelocity("0 0 0");
        %client.setControlObject(Player);
    }
};
function serverCmdDropCameraAtPlayer(%client) {
    if (!(Player.isStaff())) {
        return %client;
    }
    Camera.setTransform(Player.getEyeTransform());
    Camera.setVelocity("0 0 0");
    %client.setControlObject(Camera);
    Camera.setFlyMode();
};
function serverCmdSuicide(%client) {
    if (isObject(Player)) {
        Player.kill("Suicide");
    }
};
function serverCmdPlayCel(%client, %anim) {
    if (isObject(Player)) {
        Player.playCelAnimation(%anim);
    }
};
function serverCmdPlayAnim(%client, %anim) {
    if (isObject(Player)) {
        Player.playAnim(%anim);
    }
};
function serverCmdPlayDeath(%client) {
    if (isObject(Player)) {
        Player.playDeathAnimation();
    }
};
