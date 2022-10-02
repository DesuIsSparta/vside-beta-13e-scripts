function GameConnection::nextCamMode(%this)
{
    %this.etsCamMode = %this.etsCamMode = %this.etsCamMode + 1;
    if (%this.etsCamMode > 1)
    {
        %this.etsCamMode = 0;
    }
    if (%this.etsCamMode == 0)
    {
        %this.setControlObject(%this.Player);
    }
    else
    {
        if (%this.etsCamMode == 1)
        {
            %rot = getOrientationRelativeToObject(%this.Player, 3.14159, 0.5);
            %this.Camera.setOrbitMode(%this.Player, "0 0 0" SPC %rot, 0.5, 2.5, 1.5, 1);
            %this.setControlObject(%this.Camera);
        }
    }
    return ;
}
function getOrientationRelativeToObject(%obj, %theta, %phi)
{
    %trans = "0 0 0" SPC getWords(%obj.getTransform(), 3, 6);
    %trans = MatrixMultiply("0 0 0 0 0 1" SPC %theta, %trans);
    %trans = MatrixMultiply("0 0 0 1 0 0" SPC %phi, %trans);
    return getWords(%trans, 3, 6);
}
function ServerCmdNextCamMode(%client)
{
    %client.nextCamMode();
    return ;
}
