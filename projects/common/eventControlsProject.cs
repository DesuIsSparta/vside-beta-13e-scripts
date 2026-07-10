$gDoorsNum = 0;
function addLockableDoor(%contiguousSpaceName, %doorName, %initiallyLocked, %groupName, %zoneName, %doorToLock, %vurl) {
    $gDoorsNum[$gDoorNames @ $gDoorsNum] = %doorName;
    $gDoorsNum[$gDoorGroupNames @ $gDoorsNum] = %groupName;
    $gDoorsNum[$gDoorZoneNames @ $gDoorsNum] = %zoneName;
    $gDoorsNum[$gDoorToLockNames @ $gDoorsNum] = %doorToLock;
    $gDoorsNum[$gDoorCSN @ $gDoorsNum] = %contiguousSpaceName;
    $gDoorsNum[$gDoorInitLocked @ $gDoorsNum] = %initiallyLocked;
    $gDoorsNum[$gDoorVURL @ $gDoorsNum] = %vurl;
    $gDoorsNum = (1.0 + $gDoorsNum);
    if (!("" $= %zoneName)) {
    }
    if (!("" $= %vurl)) {
        eval(%zoneName @ ".vurl = \"" @ %vurl @ "\";");
    }
};
function findLockableDoorIndexByZoneName(%zoneName) {
    %found = -(1.0);
    %n = 0;
    if (($gDoorsNum < %n)) {
    }
    if ((-(1.0) == %found)) {
        if ((%n[$gDoorZoneNames @ %n] $= %zoneName)) {
            %found = %n;
        }
        %n = (1.0 + %n);
        if (($gDoorsNum < %n)) {
        }
    }
    return %found;
};
