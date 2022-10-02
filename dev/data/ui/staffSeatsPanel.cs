function staffSeatsPanel::toggle(%this)
{
    playGui.showRaiseOrHide(%this);
    return ;
}
function staffSeatsPanel::open(%this)
{
    if (!$player.rolesPermissionCheckWarn("events"))
    {
        return ;
    }
    %this.setVisible(1);
    playGui.focusAndRaise(%this);
    return ;
}
function staffSeatsPanel::close(%this)
{
    %this.setVisible(0);
    playGui.focusTopWindow();
    return 1;
}
$staffSeatsPanel_TOTALNUM = 0;
$staffSeatsPanel_CUR = 0;
$staffSeatsWaitForStandingRecheckDelay = 32;
$staffSeatsWaitingSchedule = 0;
function doNextSeatSit(%seatNumber)
{
    if ($player.isSitting())
    {
        echo("not taking seat #" @ %seatNumber + 1 @ "id: " @ %id @ "because the player is already sitting elsewhere");
        return ;
    }
    %id = StaffSeatsPanelTestSet.getObject(%seatNumber);
    commandToServer('RequestToSit', %id);
    staffSeatsGuiCurNumber.setText(%seatNumber + 1);
    echo("testing seat #" @ %seatNumber + 1 @ "id: " @ %id);
    return ;
}
function waitForStandingBeforeSit(%seatNumber)
{
    if ($player.isSitting())
    {
        cancel($staffSeatsWaitingSchedule);
        $staffSeatsWaitingSchedule = schedule($staffSeatsWaitForStandingRecheckDelay, 0, waitForStandingBeforeSit, %seatNumber);
    }
    else
    {
        schedule(1000, 0, doNextSeatSit, %seatNumber);
    }
    return ;
}
function staffSeatsPanel::testSeat(%this, %seatNumber)
{
    cancel($staffSeatsWaitingSchedule);
    $staffSeatsWaitingSchedule = 0;
    if ($player.isSitting())
    {
        SendStandCommand(1);
        $staffSeatsWaitingSchedule = schedule($staffSeatsWaitForStandingRecheckDelay, 0, waitForStandingBeforeSit, %seatNumber);
    }
    else
    {
        doNextSeatSit(%seatNumber);
    }
    return ;
}
function staffSeatsPanel::testNextSeat(%this)
{
    if ($staffSeatsPanel_TOTALNUM > 0)
    {
        %last = $staffSeatsPanel_CUR;
        $staffSeatsPanel_CUR = $staffSeatsPanel_CUR + 1;
        if ($staffSeatsPanel_CUR > $staffSeatsPanel_TOTALNUM)
        {
            $staffSeatsPanel_CUR = $staffSeatsPanel_CUR - 1;
            return ;
        }
        if (%last != $staffSeatsPanel_CUR)
        {
            %this.testSeat($staffSeatsPanel_CUR - 1);
        }
    }
    return ;
}
function staffSeatsPanel::testPrevSeat(%this)
{
    if ($staffSeatsPanel_TOTALNUM > 0)
    {
        %last = $staffSeatsPanel_CUR;
        $staffSeatsPanel_CUR = $staffSeatsPanel_CUR - 1;
        if ($staffSeatsPanel_CUR <= 0)
        {
            $staffSeatsPanel_CUR = $staffSeatsPanel_CUR + 1;
            return ;
        }
        if (%last != $staffSeatsPanel_CUR)
        {
            %this.testSeat($staffSeatsPanel_CUR - 1);
        }
    }
    return ;
}
function staffSeatsPanel::editCurSeat(%this)
{
    return ;
}
function recursiveCollectSeatsFromSimGroup(%obj, %seatSet)
{
    if (!isObject(%obj))
    {
        return ;
    }
    if (%obj.isClassSimGroup())
    {
        %num = %obj.getCount();
        %n = 0;
        while (%n < %num)
        {
            recursiveCollectSeatsFromSimGroup(%obj.getObject(%n), %seatSet);
            %n = %n + 1;
        }
    }
    return ;
    if ((%obj.getClassName() $= "MissionMarker") && (%obj.getClassName() $= "ETSSeatMarker"))
    {
        %dbName = %obj.getDataBlock().getName();
        %seatMarkerFound = strstr(%dbName, "SeatMarker");
        if (%seatMarkerFound != -1)
        {
            %seatSet.add(%obj.getId());
        }
    }
    return ;
}
function staffSeatsPanel::startTestingSeats(%this)
{
    $staffSeatsPanel_TOTALNUM = 0;
    $staffSeatsPanel_CUR = 0;
    if (!$StandAlone)
    {
        return ;
    }
    if (!isObject(MissionGroup))
    {
        error("startTestingSeats no missiongroup!");
        return ;
    }
    if (isObject(StaffSeatsPanelTestSet))
    {
        StaffSeatsPanelTestSet.delete();
    }
    new SimSet(StaffSeatsPanelTestSet);
    MissionCleanup.add(StaffSeatsPanelTestSet);
    recursiveCollectSeatsFromSimGroup(MissionGroup, StaffSeatsPanelTestSet);
    $staffSeatsPanel_TOTALNUM = StaffSeatsPanelTestSet.getCount();
    $staffSeatsPanel_CUR = 0;
    staffSeatsGuiCurNumber.setText($staffSeatsPanel_CUR);
    staffSeatsGuiTotalNumber.setText($staffSeatsPanel_TOTALNUM);
    return ;
}
