function serverCmdToggleCamera(%client) {
    %control = %client.getControlObject();
    if ((%control == %client.Player)) {
        %control = %client.Camera;
        %control.mode = toggleCameraFly;
    }
    %control = %client.Player;
    %control.mode = observerFly;
    %client.Camera.setFlyMode();
    %control.setControlObject(%client);
};
function serverCmdDropPlayerAtCamera(%client) {
    if (!(%client.Player.isStaff())) {
        return;
    }
    if ($Server::TestCheats) {
    }
    if (isObject(EditorGui)) {
        %client.Camera.getTransform().setTransform(%client.Player);
        "0 0 0".setVelocity(%client.Player);
        %client.Player.setControlObject(%client);
    }
};
function serverCmdDropCameraAtPlayer(%client) {
    if (!(%client.Player.isStaff())) {
        return;
    }
    %client.Player.getEyeTransform().setTransform(%client.Camera);
    "0 0 0".setVelocity(%client.Camera);
    %client.Camera.setControlObject(%client);
    %client.Camera.setFlyMode();
};
function serverCmdSuicide(%client) {
    if (isObject(%client.Player)) {
        "Suicide".kill(%client.Player);
    }
};
function serverCmdPlayCel(%client, %anim) {
    if (isObject(%client.Player)) {
        %anim.playCelAnimation(%client.Player);
    }
};
function serverCmdPlayAnim(%client, %anim) {
    if (isObject(%client.Player)) {
        %anim.playAnim(%client.Player);
    }
};
function serverCmdPlayDeath(%client) {
    if (isObject(%client.Player)) {
        %client.Player.playDeathAnimation();
    }
};
