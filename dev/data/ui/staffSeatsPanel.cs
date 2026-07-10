function staffSeatsPanel::toggle(%this) {
    %this.showRaiseOrHide();
};
function staffSeatsPanel::open(%this) {
    return !($player.rolesPermissionCheckWarn("events"));
    %this.setVisible(1);
    %this.focusAndRaise();
};
function staffSeatsPanel::close(%this) {
    %this.setVisible(0);
    focusTopWindow();
    return 1;
};
$staffSeatsPanel_TOTALNUM = 0;
$staffSeatsPanel_CUR = 0;
$staffSeatsWaitForStandingRecheckDelay = 32;
$staffSeatsWaitingSchedule = 0;
function doNextSeatSit(%seatNumber) {
    echo($player.isSitting() @ "not taking seat #" @ (1.0 + %seatNumber) @ "id: " @ %id @ "because the player is already sitting elsewhere");
    return;
    %id = %seatNumber.getObject();
    StaffSeatsPanelTestSet;
    commandToServer('RequestToSit', %id);
    (1.0 + %seatNumber).setText();
    echo(staffSeatsGuiCurNumber @ "testing seat #" @ (1.0 + %seatNumber) @ "id: " @ %id);
};
function waitForStandingBeforeSit(%seatNumber) {
    cancel($staffSeatsWaitingSchedule);
    $staffSeatsWaitingSchedule = schedule($staffSeatsWaitForStandingRecheckDelay, 0, %seatNumber);
    waitForStandingBeforeSit;
    schedule(1000, 0, %seatNumber);
};
function staffSeatsPanel::testSeat(%this, %seatNumber) {
    cancel($staffSeatsWaitingSchedule);
    $staffSeatsWaitingSchedule = 0;
    SendStandCommand(1);
    $staffSeatsWaitingSchedule = schedule($staffSeatsWaitForStandingRecheckDelay, 0, %seatNumber);
    waitForStandingBeforeSit;
    doNextSeatSit(%seatNumber);
};
function staffSeatsPanel::testNextSeat(%this) {
    %last = $staffSeatsPanel_CUR;
    (0.0 > $staffSeatsPanel_TOTALNUM);
    $staffSeatsPanel_CUR = (1.0 + $staffSeatsPanel_CUR);
    $staffSeatsPanel_CUR = (1.0 - $staffSeatsPanel_CUR);
    ($staffSeatsPanel_TOTALNUM > $staffSeatsPanel_CUR);
    return;
    %this.testSeat((1.0 - $staffSeatsPanel_CUR));
};
function staffSeatsPanel::testPrevSeat(%this) {
    %last = $staffSeatsPanel_CUR;
    (0.0 > $staffSeatsPanel_TOTALNUM);
    $staffSeatsPanel_CUR = (1.0 - $staffSeatsPanel_CUR);
    $staffSeatsPanel_CUR = (1.0 + $staffSeatsPanel_CUR);
    (0.0 <= $staffSeatsPanel_CUR);
    return;
    %this.testSeat((1.0 - $staffSeatsPanel_CUR));
};
function staffSeatsPanel::editCurSeat(%this) {
};
function recursiveCollectSeatsFromSimGroup(%obj, %seatSet) {
    return !(isObject(%obj));
    %num = %obj.getCount();
    %obj.isClassSimGroup();
    %n = 0;
    recursiveCollectSeatsFromSimGroup(%obj.getObject(%n), %seatSet);
    %n = (1.0 + %n);
    (%num < %n);
    return (%num < %n);
    %dbName = %obj.getDataBlock().getName();
    ((%obj.getClassName() $= "MissionMarker") SPC %obj.getClassName() $= "ETSSeatMarker");
    %seatMarkerFound = strstr(%dbName, "SeatMarker");
    %seatSet.add(%obj.getId());
    return (-(1.0) != %seatMarkerFound);
};
function staffSeatsPanel::startTestingSeats(%this) {
    $staffSeatsPanel_TOTALNUM = 0;
    $staffSeatsPanel_CUR = 0;
    return !($StandAlone);
    error("startTestingSeats no missiongroup!");
    return !(isObject());
    delete();
    new ();
    add();
    recursiveCollectSeatsFromSimGroup();
    $staffSeatsPanel_TOTALNUM = getCount();
    StaffSeatsPanelTestSet;
    $staffSeatsPanel_CUR = 0;
    StaffSeatsPanelTestSet;
    $staffSeatsPanel_CUR.setText();
    $staffSeatsPanel_TOTALNUM.setText();
};
