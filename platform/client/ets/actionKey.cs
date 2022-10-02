function onActionKey(%val)
{
    jump(%val);
    return ;
}
function onThrowBall(%val)
{
    if (%val)
    {
        $mvTriggerCount0 = $mvTriggerCount0 + 1;
        $mvTriggerCount0 = $mvTriggerCount0 + 1;
    }
    return ;
}
function onMouseUpThrowBall(%power, %worldVec)
{
    %camPos = $gClientGameConnection.getCameraPosition();
    commandToServer('ThrowBallAtDir', %worldVec, %camPos, %power);
    return ;
}
