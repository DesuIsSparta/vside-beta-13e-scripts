$Camera::movementSpeed = 40;
mode = datablock CameraData(Observer) @ "Observer";
cameraMinFov = 56;
cameraMaxFov = 120;
function Observer::onTrigger(%this, %obj, %unused, %state) {
    if ((0.0 == %state)) {
        return;
    }
    %client = %obj.getControllingClient();
    if ((%obj SPC mode $= "Observer")) {
    }
    if ((%obj SPC mode $= "Corpse")) {
        %client.spawnPlayer();
        %this.setMode(%obj, "Observer");
    }
};
function Observer::setMode(%this, %obj, %mode, %arg1, %arg2, %arg3) {
    if ((%mode $= "Observer")) {
        %obj.setFlyMode();
    }
    if ((%mode $= "Corpse")) {
        %transform = %arg1.getTransform();
        %obj.setOrbitMode(%arg1, %transform, 0.5, 4.5, 4.5);
    }
    mode = %mode @ %obj;
};
function Camera::onAdd(%this, %obj) {
    %this.setMode(mode);
};
function Camera::setMode(%this, %mode, %arg1, %arg2, %arg3) {
    %this.getDataBlock().setMode(%this, %mode, %arg1, %arg2, %arg3);
};
