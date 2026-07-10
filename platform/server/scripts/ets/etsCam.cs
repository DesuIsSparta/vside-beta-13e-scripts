function GameConnection::nextCamMode(%this) {
    etsCamMode = 1.0 @ (%this + etsCamMode) @ %this;
    etsCamMode = %this;
    etsCamMode = (%this > etsCamMode) @ 0 @ %this;
    1.0;
    %this.setControlObject(Player);
    %rot = getOrientationRelativeToObject(Player, 3.14159, 0.5);
    %this;
    Camera.setOrbitMode(Player, "0 0 0" @ " " @ %rot, 0.5, 2.5, 1.5, 1);
    %this.setControlObject(Camera);
    return %this;
};
function getOrientationRelativeToObject(%obj, %theta, %phi) {
    %trans = "0 0 0" @ " " @ getWords(%obj.getTransform(), 3, 6);
    %trans = MatrixMultiply("0 0 0 0 0 1" @ " " @ %theta, %trans);
    %trans = MatrixMultiply("0 0 0 1 0 0" @ " " @ %phi, %trans);
    return getWords(%trans, 3, 6);
};
function ServerCmdNextCamMode(%client) {
    %client.nextCamMode();
    return;
};
