function onActionKey(%val) {
    jump(%val);
};
function onThrowBall(%val) {
    if (%val) {
        $mvTriggerCount0 = (1.0 + $mvTriggerCount0);
        $mvTriggerCount0 = (1.0 + $mvTriggerCount0);
    }
};
function onMouseUpThrowBall(%power, %worldVec) {
    %camPos = $gClientGameConnection.getCameraPosition();
    commandToServer('ThrowBallAtDir', %worldVec, %camPos, %power);
};
