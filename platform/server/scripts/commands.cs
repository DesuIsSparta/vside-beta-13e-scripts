function serverCmdToggleCamera(%client)
{
    %control = %client.getControlObject();
    if (%control == %client.Player)
    {
        %control = %client.Camera;
        %control.mode = toggleCameraFly;
    }
    else
    {
        %control = %client.Player;
        %control.mode = observerFly;
    }
    %client.Camera.setFlyMode();
    %client.setControlObject(%control);
    return ;
}
function serverCmdDropPlayerAtCamera(%client)
{
    if (!%client.Player.isStaff())
    {
        return ;
    }
    if ($Server::TestCheats && isObject(EditorGui))
    {
        %client.Player.setTransform(%client.Camera.getTransform());
        %client.Player.setVelocity("0 0 0");
        %client.setControlObject(%client.Player);
    }
    return ;
}
function serverCmdDropCameraAtPlayer(%client)
{
    if (!%client.Player.isStaff())
    {
        return ;
    }
    %client.Camera.setTransform(%client.Player.getEyeTransform());
    %client.Camera.setVelocity("0 0 0");
    %client.setControlObject(%client.Camera);
    %client.Camera.setFlyMode();
    return ;
}
function serverCmdSuicide(%client)
{
    if (isObject(%client.Player))
    {
        %client.Player.kill("Suicide");
    }
    return ;
}
function serverCmdPlayCel(%client, %anim)
{
    if (isObject(%client.Player))
    {
        %client.Player.playCelAnimation(%anim);
    }
    return ;
}
function serverCmdPlayAnim(%client, %anim)
{
    if (isObject(%client.Player))
    {
        %client.Player.playAnim(%anim);
    }
    return ;
}
function serverCmdPlayDeath(%client)
{
    if (isObject(%client.Player))
    {
        %client.Player.playDeathAnimation();
    }
    return ;
}
