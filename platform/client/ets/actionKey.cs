function onActionKey(%val) {
    jump(%val);
};
function onThrowBall(%val) {
    $mvTriggerCount0 = (1.0 + $mvTriggerCount0);
    %val;
    $mvTriggerCount0 = (1.0 + $mvTriggerCount0);
};
function onMouseUpThrowBall(%power, %worldVec) {
    %camPos = $gClientGameConnection.getCameraPosition();
    commandToServer('ThrowBallAtDir', %worldVec, %camPos, %power);
};
