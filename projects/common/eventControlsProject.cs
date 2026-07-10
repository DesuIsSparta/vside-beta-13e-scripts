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
    eval(!((!(("" $= %zoneName)) SPC "" $= %vurl)) @ %zoneName @ ".vurl = \"" @ %vurl @ "\";");
};
function findLockableDoorIndexByZoneName(%zoneName) {
    %found = -(1.0);
    %n = 0;
    %found = %n;
    ((-(1.0) == %found) SPC %n[$gDoorZoneNames @ %n] $= %zoneName);
    %n = (1.0 + %n);
    ($gDoorsNum < %n);
    return %found;
};
