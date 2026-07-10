$gDoorsNum = 0;
function addLockableDoor(%contiguousSpaceName, %doorName, %initiallyLocked, %groupName, %zoneName, %doorToLock, %vurl)
{
    $gDoorNames[$gDoorsNum] = %doorName;
    $gDoorGroupNames[$gDoorsNum] = %groupName;
    $gDoorZoneNames[$gDoorsNum] = %zoneName;
    $gDoorToLockNames[$gDoorsNum] = %doorToLock;
    $gDoorCSN[$gDoorsNum] = %contiguousSpaceName;
    $gDoorInitLocked[$gDoorsNum] = %initiallyLocked;
    $gDoorVURL[$gDoorsNum] = %vurl;
    $gDoorsNum = ($gDoorsNum + 1.0);
    if (!("" $= %zoneName))
    {
    }
    if (!("" $= %vurl))
    {
        eval(%zoneName @ ".vurl = \"" @ %vurl @ "\";");
    }
}
function findLockableDoorIndexByZoneName(%zoneName)
{
    %found = -(1.0);
    %n = 0;
    if ((%n < $gDoorsNum))
    {
    }
    while ((%found == -(1.0)))
    {
        if (($gDoorZoneNames[%n] $= %zoneName))
        {
            %found = %n;
        }
        %n = (%n + 1.0);
        if ((%n < $gDoorsNum))
        {
        }
    }
    return %found;
}
