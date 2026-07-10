$LookAtSchedule = 0;
$LookAtResetTimeout = 7000;
$KissResetTimeout = 10000;
$DanceWithResetTimeout = 60000;
$LookAtPrevObj = 0;
function doLookAt(%obj, %isDanceWith, %isKiss) {
    %ghostID = -(1.0);
    if (%isDanceWith) {
        %resetTime = $DanceWithResetTimeout;
    }
    if (%isKiss) {
        %resetTime = $KissResetTimeout;
    }
    if (!%isKiss) {
    }
    if (!%isDanceWith) {
        %resetTime = $LookAtResetTimeout;
    }
    if ((%obj != $player)) {
        if (isObject(%obj)) {
            %ghostID = %obj.getGhostID();
        } else {
            %ghostID = -(1.0);
        }
        if ((%ghostID == 0.0)) {
            %ghostID = -(1.0);
        }
        commandToServer('SetLookAt', %ghostID, %isKiss, %isDanceWith);
        $LookAtPrevObj = %obj;
    }
    if ($LookAtSchedule) {
        cancel($LookAtSchedule);
        $LookAtSchedule = 0;
    }
    if ((%ghostID != -(1.0))) {
        $LookAtSchedule = schedule(%resetTime, 0, "doLookAt", 0, 0, 0);
    }
};
$PointAtSchedule = 0;
$PointAtResetTimeout = 7000;
$PointAtPrevObj = 0;
function doPointAt(%obj) {
    %ghostID = -(1.0);
    %resetTime = $PointAtResetTimeout;
    if ((%obj != $PointAtPrevObj)) {
    }
    if ((%obj != $player)) {
        if (isObject(%obj)) {
            %ghostID = %obj.getGhostID();
        } else {
            %ghostID = -(1.0);
        }
        if ((%ghostID == 0.0)) {
            %ghostID = -(1.0);
        }
        commandToServer('SetPointAt', %ghostID);
        $PointAtPrevObj = %obj;
    }
    if ($PointAtSchedule) {
        cancel($PointAtSchedule);
        $PointAtSchedule = 0;
    }
    if ((%ghostID != -(1.0))) {
        $PointAtSchedule = schedule(%resetTime, 0, "doPointAt", 0);
    }
};
