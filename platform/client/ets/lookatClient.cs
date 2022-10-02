$LookAtSchedule = 0;
$LookAtResetTimeout = 7000;
$KissResetTimeout = 10000;
$DanceWithResetTimeout = 60000;
$LookAtPrevObj = 0;
function doLookAt(%obj, %isDanceWith, %isKiss)
{
    %ghostID = -1;
    if (%isDanceWith)
    {
        %resetTime = $DanceWithResetTimeout;
    }
    if (%isKiss)
    {
        %resetTime = $KissResetTimeout;
    }
    if (!%isKiss && !%isDanceWith)
    {
        %resetTime = $LookAtResetTimeout;
    }
    if (%obj != $player)
    {
        if (isObject(%obj))
        {
            %ghostID = %obj.getGhostID();
        }
        else
        {
            %ghostID = -1;
        }
        if (%ghostID == 0)
        {
            %ghostID = -1;
        }
        commandToServer('SetLookAt', %ghostID, %isKiss, %isDanceWith);
        $LookAtPrevObj = %obj;
    }
    if ($LookAtSchedule)
    {
        cancel($LookAtSchedule);
        $LookAtSchedule = 0;
    }
    if (%ghostID != -1)
    {
        $LookAtSchedule = schedule(%resetTime, 0, "doLookAt", 0, 0, 0);
    }
    return ;
}
$PointAtSchedule = 0;
$PointAtResetTimeout = 7000;
$PointAtPrevObj = 0;
function doPointAt(%obj)
{
    %ghostID = -1;
    %resetTime = $PointAtResetTimeout;
    if ((%obj != $PointAtPrevObj) && (%obj != $player))
    {
        if (isObject(%obj))
        {
            %ghostID = %obj.getGhostID();
        }
        else
        {
            %ghostID = -1;
        }
        if (%ghostID == 0)
        {
            %ghostID = -1;
        }
        commandToServer('SetPointAt', %ghostID);
        $PointAtPrevObj = %obj;
    }
    if ($PointAtSchedule)
    {
        cancel($PointAtSchedule);
        $PointAtSchedule = 0;
    }
    if (%ghostID != -1)
    {
        $PointAtSchedule = schedule(%resetTime, 0, "doPointAt", 0);
    }
    return ;
}
