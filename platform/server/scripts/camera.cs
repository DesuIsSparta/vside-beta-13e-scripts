$Camera::movementSpeed = 40;
datablock CameraData(Observer) {
    mode = "Observer";
    cameraMinFov = 56;
    cameraMaxFov = 120;
};
function Observer::onTrigger(%this, %obj, %unused, %state) {
    if ((%state == 0.0)) {
        return;
    }
    %client = %obj.getControllingClient();
    if ((%obj.mode $= "Observer")) {
    }
    if ((%obj.mode $= "Corpse")) {
        %client.spawnPlayer();
        "Observer".setMode(%this, %obj);
    }
};
function Observer::setMode(%this, %obj, %mode, %arg1, %arg2, %arg3) {
    if ((%mode $= "Observer")) {
        %obj.setFlyMode();
    }
    if ((%mode $= "Corpse")) {
        %transform = %arg1.getTransform();
        4.5.setOrbitMode(%obj, %arg1, %transform, 0.5, 4.5);
    }
    %obj.mode = %mode;
};
function Camera::onAdd(%this, %obj) {
    %this.mode.setMode(%this);
};
function Camera::setMode(%this, %mode, %arg1, %arg2, %arg3) {
    %arg3.setMode(%this.getDataBlock(), %this, %mode, %arg1, %arg2);
};
