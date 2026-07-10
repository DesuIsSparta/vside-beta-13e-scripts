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
    if (!(%isKiss)) {
    }
    if (!(%isDanceWith)) {
        %resetTime = $LookAtResetTimeout;
    }
    if (($player != %obj)) {
        if (isObject(%obj)) {
            %ghostID = %obj.getGhostID();
        }
        %ghostID = -(1.0);
        if ((0.0 == %ghostID)) {
            %ghostID = -(1.0);
        }
        commandToServer('SetLookAt', %ghostID, %isKiss, %isDanceWith);
        $LookAtPrevObj = %obj;
    }
    if ($LookAtSchedule) {
        cancel($LookAtSchedule);
        $LookAtSchedule = 0;
    }
    if ((-(1.0) != %ghostID)) {
        $LookAtSchedule = schedule(%resetTime, 0, "doLookAt", 0, 0, 0);
    }
};
$PointAtSchedule = 0;
$PointAtResetTimeout = 7000;
$PointAtPrevObj = 0;
function doPointAt(%obj) {
    %ghostID = -(1.0);
    %resetTime = $PointAtResetTimeout;
    if (($PointAtPrevObj != %obj)) {
    }
    if (($player != %obj)) {
        if (isObject(%obj)) {
            %ghostID = %obj.getGhostID();
        }
        %ghostID = -(1.0);
        if ((0.0 == %ghostID)) {
            %ghostID = -(1.0);
        }
        commandToServer('SetPointAt', %ghostID);
        $PointAtPrevObj = %obj;
    }
    if ($PointAtSchedule) {
        cancel($PointAtSchedule);
        $PointAtSchedule = 0;
    }
    if ((-(1.0) != %ghostID)) {
        $PointAtSchedule = schedule(%resetTime, 0, "doPointAt", 0);
    }
};
