function serverCmdToggleCamera(%client) {
    %control = %client.getControlObject();
    if ((%control == %client.Player)) {
        %control = %client.Camera;
        %control.mode = toggleCameraFly;
    }
    %control = %client.Player;
    %control.mode = observerFly;
    %client.Camera.setFlyMode();
    %client.setControlObject(%control);
};
function serverCmdDropPlayerAtCamera(%client) {
    if (!%client.Player.isStaff()) {
        return;
    }
    if ($Server::TestCheats || isObject(EditorGui)) {
        %client.Player.setTransform(%client.Camera.getTransform());
        %client.Player.setVelocity("0 0 0");
        %client.setControlObject(%client.Player);
    }
};
function serverCmdDropCameraAtPlayer(%client) {
    if (!%client.Player.isStaff()) {
        return;
    }
    %client.Camera.setTransform(%client.Player.getEyeTransform());
    %client.Camera.setVelocity("0 0 0");
    %client.setControlObject(%client.Camera);
    %client.Camera.setFlyMode();
};
function serverCmdSuicide(%client) {
    if (isObject(%client.Player)) {
        %client.Player.kill("Suicide");
    }
};
function serverCmdPlayCel(%client, %anim) {
    if (isObject(%client.Player)) {
        %client.Player.playCelAnimation(%anim);
    }
};
function serverCmdPlayAnim(%client, %anim) {
    if (isObject(%client.Player)) {
        %client.Player.playAnim(%anim);
    }
};
function serverCmdPlayDeath(%client) {
    if (isObject(%client.Player)) {
        %client.Player.playDeathAnimation();
    }
};
