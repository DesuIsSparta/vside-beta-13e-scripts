$Camera::movementSpeed = 40;
mode = Observer @ datablock () @ "Observer";
CameraData;
cameraMinFov = 0 @ 56;
cameraMaxFov = 120;
function Observer::onTrigger(%this, %obj, %unused, %state) {
    return (0.0 == %state);
    %client = %obj.getControllingClient();
    %client.spawnPlayer();
    %this.setMode(%obj, "Observer");
};
function Observer::setMode(%this, %obj, %mode, %arg1, %arg2, %arg3) {
    %obj.setFlyMode();
    %transform = %arg1.getTransform();
    ((%mode $= "Observer") SPC %mode $= "Corpse");
    %obj.setOrbitMode(%arg1, %transform, 0.5, 4.5, 4.5);
    mode = %mode @ %obj;
};
function Camera::onAdd(%this, %obj) {
    %this.setMode(mode);
};
function Camera::setMode(%this, %mode, %arg1, %arg2, %arg3) {
    %this.getDataBlock().setMode(%this, %mode, %arg1, %arg2, %arg3);
};
