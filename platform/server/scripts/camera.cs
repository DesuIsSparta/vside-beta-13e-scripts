$Camera::movementSpeed = 40;
datablock CameraData(Observer)
{
    mode = "Observer";
    cameraMinFov = 56;
    cameraMaxFov = 120;
};
function Observer::onTrigger(%this, %obj, %unused, %state)
{
    if (%state == 0)
    {
        return ;
    }
    %client = %obj.getControllingClient();
    if (%obj.mode $= "Observer")
    {
    }
    else
    {
        if (%obj.mode $= "Corpse")
        {
            %client.spawnPlayer();
            %this.setMode(%obj, "Observer");
        }
    }
    return ;
}
function Observer::setMode(%this, %obj, %mode, %arg1, %arg2, %arg3)
{
    if (%mode $= "Observer")
    {
        %obj.setFlyMode();
    }
    else
    {
        if (%mode $= "Corpse")
        {
            %transform = %arg1.getTransform();
            %obj.setOrbitMode(%arg1, %transform, 0.5, 4.5, 4.5);
        }
    }
    %obj.mode = %mode;
    return ;
}
function Camera::onAdd(%this, %obj)
{
    %this.setMode(%this.mode);
    return ;
}
function Camera::setMode(%this, %mode, %arg1, %arg2, %arg3)
{
    %this.getDataBlock().setMode(%this, %mode, %arg1, %arg2, %arg3);
    return ;
}
