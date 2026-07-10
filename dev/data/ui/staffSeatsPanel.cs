function staffSeatsPanel::toggle(%this) {
    %this.showRaiseOrHide(playGui);
};
function staffSeatsPanel::open(%this) {
    if (!("events".rolesPermissionCheckWarn($player))) {
        return;
    }
    1.setVisible(%this);
    %this.focusAndRaise(playGui);
};
function staffSeatsPanel::close(%this) {
    0.setVisible(%this);
    playGui.focusTopWindow();
    return 1;
};
$staffSeatsPanel_TOTALNUM = 0;
$staffSeatsPanel_CUR = 0;
$staffSeatsWaitForStandingRecheckDelay = 32;
$staffSeatsWaitingSchedule = 0;
function doNextSeatSit(%seatNumber) {
    if ($player.isSitting()) {
        echo("not taking seat #" @ (%seatNumber + 1.0) @ "id: " @ %id @ "because the player is already sitting elsewhere");
        return;
    }
    %id = %seatNumber.getObject(StaffSeatsPanelTestSet);
    commandToServer('RequestToSit', %id);
    (%seatNumber + 1.0).setText(staffSeatsGuiCurNumber);
    echo("testing seat #" @ (%seatNumber + 1.0) @ "id: " @ %id);
};
function waitForStandingBeforeSit(%seatNumber) {
    if ($player.isSitting()) {
        cancel($staffSeatsWaitingSchedule);
        $staffSeatsWaitingSchedule = schedule($staffSeatsWaitForStandingRecheckDelay, 0, waitForStandingBeforeSit, %seatNumber);
    }
    schedule(1000, 0, doNextSeatSit, %seatNumber);
};
function staffSeatsPanel::testSeat(%this, %seatNumber) {
    cancel($staffSeatsWaitingSchedule);
    $staffSeatsWaitingSchedule = 0;
    if ($player.isSitting()) {
        SendStandCommand(1);
        $staffSeatsWaitingSchedule = schedule($staffSeatsWaitForStandingRecheckDelay, 0, waitForStandingBeforeSit, %seatNumber);
    }
    doNextSeatSit(%seatNumber);
};
function staffSeatsPanel::testNextSeat(%this) {
    if (($staffSeatsPanel_TOTALNUM > 0.0)) {
        %last = $staffSeatsPanel_CUR;
        $staffSeatsPanel_CUR = ($staffSeatsPanel_CUR + 1.0);
        if (($staffSeatsPanel_CUR > $staffSeatsPanel_TOTALNUM)) {
            $staffSeatsPanel_CUR = ($staffSeatsPanel_CUR - 1.0);
            return;
        }
        if ((%last != $staffSeatsPanel_CUR)) {
            ($staffSeatsPanel_CUR - 1.0).testSeat(%this);
        }
    }
};
function staffSeatsPanel::testPrevSeat(%this) {
    if (($staffSeatsPanel_TOTALNUM > 0.0)) {
        %last = $staffSeatsPanel_CUR;
        $staffSeatsPanel_CUR = ($staffSeatsPanel_CUR - 1.0);
        if (($staffSeatsPanel_CUR <= 0.0)) {
            $staffSeatsPanel_CUR = ($staffSeatsPanel_CUR + 1.0);
            return;
        }
        if ((%last != $staffSeatsPanel_CUR)) {
            ($staffSeatsPanel_CUR - 1.0).testSeat(%this);
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
        while ((%n < %num)) {
            recursiveCollectSeatsFromSimGroup(%n.getObject(%obj), %seatSet);
            %n = (%n + 1.0);
        }
        return (%n < %num);
    }
    if ((%obj.getClassName() $= "MissionMarker")) {
    }
    if ((%obj.getClassName() $= "ETSSeatMarker")) {
        %dbName = %obj.getDataBlock().getName();
        %seatMarkerFound = strstr(%dbName, "SeatMarker");
        if ((%seatMarkerFound != -(1.0))) {
            %obj.getId().add(%seatSet);
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
    if (!(isObject(MissionGroup))) {
        error("startTestingSeats no missiongroup!");
        return;
    }
    if (isObject(StaffSeatsPanelTestSet)) {
        StaffSeatsPanelTestSet.delete();
    }
    new SimSet(StaffSeatsPanelTestSet);
    StaffSeatsPanelTestSet.add(MissionCleanup);
    recursiveCollectSeatsFromSimGroup(MissionGroup, StaffSeatsPanelTestSet);
    $staffSeatsPanel_TOTALNUM = StaffSeatsPanelTestSet.getCount();
    $staffSeatsPanel_CUR = 0;
    $staffSeatsPanel_CUR.setText(staffSeatsGuiCurNumber);
    $staffSeatsPanel_TOTALNUM.setText(staffSeatsGuiTotalNumber);
};
