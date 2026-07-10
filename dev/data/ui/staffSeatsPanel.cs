function staffSeatsPanel::toggle(%this) {
    %this.showRaiseOrHide();
};
function staffSeatsPanel::open(%this) {
    if (!($player.rolesPermissionCheckWarn("events"))) {
        return;
    }
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
    if ($player.isSitting()) {
        echo("not taking seat #" @ (1.0 + %seatNumber) @ "id: " @ %id @ "because the player is already sitting elsewhere");
        return;
    }
    %id = %seatNumber.getObject();
    StaffSeatsPanelTestSet;
    commandToServer('RequestToSit', %id);
    (1.0 + %seatNumber).setText();
    echo(staffSeatsGuiCurNumber @ "testing seat #" @ (1.0 + %seatNumber) @ "id: " @ %id);
};
function waitForStandingBeforeSit(%seatNumber) {
    if ($player.isSitting()) {
        cancel($staffSeatsWaitingSchedule);
        $staffSeatsWaitingSchedule = schedule($staffSeatsWaitForStandingRecheckDelay, 0, %seatNumber);
        waitForStandingBeforeSit;
    }
    schedule(1000, 0, %seatNumber);
};
function staffSeatsPanel::testSeat(%this, %seatNumber) {
    cancel($staffSeatsWaitingSchedule);
    $staffSeatsWaitingSchedule = 0;
    if ($player.isSitting()) {
        SendStandCommand(1);
        $staffSeatsWaitingSchedule = schedule($staffSeatsWaitForStandingRecheckDelay, 0, %seatNumber);
        waitForStandingBeforeSit;
    }
    doNextSeatSit(%seatNumber);
};
function staffSeatsPanel::testNextSeat(%this) {
    if ((0.0 > $staffSeatsPanel_TOTALNUM)) {
        %last = $staffSeatsPanel_CUR;
        $staffSeatsPanel_CUR = (1.0 + $staffSeatsPanel_CUR);
        if (($staffSeatsPanel_TOTALNUM > $staffSeatsPanel_CUR)) {
            $staffSeatsPanel_CUR = (1.0 - $staffSeatsPanel_CUR);
            return;
        }
        if (($staffSeatsPanel_CUR != %last)) {
            %this.testSeat((1.0 - $staffSeatsPanel_CUR));
        }
    }
};
function staffSeatsPanel::testPrevSeat(%this) {
    if ((0.0 > $staffSeatsPanel_TOTALNUM)) {
        %last = $staffSeatsPanel_CUR;
        $staffSeatsPanel_CUR = (1.0 - $staffSeatsPanel_CUR);
        if ((0.0 <= $staffSeatsPanel_CUR)) {
            $staffSeatsPanel_CUR = (1.0 + $staffSeatsPanel_CUR);
            return;
        }
        if (($staffSeatsPanel_CUR != %last)) {
            %this.testSeat((1.0 - $staffSeatsPanel_CUR));
        }
    }
};
function staffSeatsPanel::editCurSeat(%this) {
};
function recursiveCollectSeatsFromSimGroup(%obj, %seatSet) {
    if (!(isObject(%obj))) {
        return;
    }
    if (%obj.isClassSimGroup()) {
        %num = %obj.getCount();
        %n = 0;
        if ((%num < %n)) {
            recursiveCollectSeatsFromSimGroup(%obj.getObject(%n), %seatSet);
            %n = (1.0 + %n);
        }
        return (%num < %n);
    }
    if ((%obj.getClassName() $= "MissionMarker")) {
    }
    if ((%obj.getClassName() $= "ETSSeatMarker")) {
        %dbName = %obj.getDataBlock().getName();
        %seatMarkerFound = strstr(%dbName, "SeatMarker");
        if ((-(1.0) != %seatMarkerFound)) {
            %seatSet.add(%obj.getId());
        }
    }
    return;
};
function staffSeatsPanel::startTestingSeats(%this) {
    $staffSeatsPanel_TOTALNUM = 0;
    $staffSeatsPanel_CUR = 0;
    if (!($StandAlone)) {
        return;
    }
    if (!(isObject())) {
        error("startTestingSeats no missiongroup!");
        return MissionGroup;
    }
    if (isObject()) {
        delete();
    }
    new SimSet(StaffSeatsPanelTestSet);
    add();
    recursiveCollectSeatsFromSimGroup();
    $staffSeatsPanel_TOTALNUM = getCount();
    StaffSeatsPanelTestSet;
    $staffSeatsPanel_CUR = 0;
    StaffSeatsPanelTestSet;
    $staffSeatsPanel_CUR.setText();
    $staffSeatsPanel_TOTALNUM.setText();
};
