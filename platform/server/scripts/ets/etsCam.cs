function GameConnection::nextCamMode(%this) {
    %this.etsCamMode = (1.0 + %this.etsCamMode);
    %this.etsCamMode = ;
    if ((1.0 > %this.etsCamMode)) {
        %this.etsCamMode = 0;
    }
    if ((0.0 == %this.etsCamMode)) {
        %this.setControlObject(%this.Player);
    }
    if ((1.0 == %this.etsCamMode)) {
        %rot = getOrientationRelativeToObject(%this.Player, 3.14159, 0.5);
        %this.Camera.setOrbitMode(%this.Player, "0 0 0" @ " " @ %rot, 0.5, 2.5, 1.5, 1);
        %this.setControlObject(%this.Camera);
    }
    return;
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
