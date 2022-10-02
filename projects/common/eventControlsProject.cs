$gDoorsNum = 0;
function addLockableDoor(%contiguousSpaceName, %doorName, %initiallyLocked, %groupName, %zoneName, %doorToLock, %vurl)
{
    $gDoorNames[$gDoorsNum] = %doorName ;
    $gDoorGroupNames[$gDoorsNum] = %groupName ;
    $gDoorZoneNames[$gDoorsNum] = %zoneName ;
    $gDoorToLockNames[$gDoorsNum] = %doorToLock ;
    $gDoorCSN[$gDoorsNum] = %contiguousSpaceName ;
    $gDoorInitLocked[$gDoorsNum] = %initiallyLocked ;
    $gDoorVURL[$gDoorsNum] = %vurl ;
    $gDoorsNum = $gDoorsNum + 1;
    if (!(("" $= %zoneName)) && !(("" $= %vurl)))
    {
        eval(%zoneName @ ".vurl = \"" @ %vurl @ "\";");
    }
    return ;
}
function findLockableDoorIndexByZoneName(%zoneName)
{
    %found = -1;
    %n = 0;
    while (%found == -1)
    {
        if ($gDoorZoneNames[%n] $= %zoneName)
        {
            %found = %n;
        }
        %n = %n + 1;
    }
    return %found;
}
