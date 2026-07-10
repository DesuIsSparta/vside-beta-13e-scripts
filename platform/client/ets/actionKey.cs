function onActionKey(%val)
{
    jump(%val);
}
function onThrowBall(%val)
{
    if (%val)
    {
        $mvTriggerCount0 = $mvTriggerCount0 + 1;
        $mvTriggerCount0 = $mvTriggerCount0 + 1;
    }
}
function onMouseUpThrowBall(%power, %worldVec)
{
    %camPos = $gClientGameConnection.getCameraPosition();
    commandToServer('ThrowBallAtDir', %worldVec, %camPos, %power);
}
