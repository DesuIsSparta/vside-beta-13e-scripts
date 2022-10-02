function toggleEventControlWindow()
{
    if (!$player.rolesPermissionCheckNoWarn("events"))
    {
        return ;
    }
    playGui.showRaiseOrHide(eventControlWindow);
    return ;
}
function eventControlWindow::open(%this)
{
    if (!$player.rolesPermissionCheckNoWarn("events"))
    {
        return ;
    }
    %this.setVisible(1);
    playGui.focusAndRaise(%this);
    %this.populateDoorsList();
    return ;
}
function eventControlWindow::close(%this)
{
    %this.setVisible(0);
    playGui.focusTopWindow();
    return 1;
}
function eventControlWindow::toggleChristmas(%this)
{
    commandToServer('ChristmasEventToggle');
    return ;
}
function eventControlWindow::toggleSnow(%this)
{
    commandToServer('ChristmasEventToggleSnow');
    return ;
}
function eventControlWindow::toggleHeavyMist(%this)
{
    commandToServer('ChristmasEventToggleHeavyMist');
    return ;
}
function eventControlWindow::toggleCostumes(%this)
{
    commandToServer('ChristmasEventCostumesToggle');
    return ;
}
function eventControlWindow::getEventStatus(%this)
{
    commandToServer('ChristmasEventGetStatus');
    return ;
}
function HalloweenSpecifics::toggleHalloween(%this)
{
    commandToServer('HalloweenEventToggle');
    return ;
}
function HalloweenSpecifics::toggleLightning(%this)
{
    commandToServer('HalloweenEventLightningToggle');
    return ;
}
function HalloweenSpecifics::lightningStrike(%this)
{
    commandToServer('HalloweenEventDoLightning');
    return ;
}
function HalloweenSpecifics::toggleZombieGame(%this)
{
    commandToServer('HalloweenEventZombieGameToggle');
    return ;
}
function HalloweenSpecifics::toggleCostumes(%this)
{
    commandToServer('HalloweenEventCostumesToggle');
    return ;
}
function HalloweenSpecifics::getEventStatus(%this)
{
    commandToServer('HalloweenEventGetStatus');
    return ;
}
function HalloweenSpecifics::dumpGameStandings(%this)
{
    commandToServer('HalloweenEventDumpUnsortedStandingsToLog');
    return ;
}
function SlideshowGui::start()
{
    SlideshowGui::GuiToPrefs();
    commandToServer('SlideshowStart', $userPref::slideshow::objName, $userPref::slideshow::baseURL, $userPref::slideshow::picMin, $userPref::slideshow::picMax, $userPref::slideshow::fileName, $userPref::slideshow::periodSecs, $UserPref::slideshow::random);
    return ;
}
function SlideshowGui::pause()
{
    commandToServer('SlideshowPause');
    return ;
}
function SlideshowGui::stop()
{
    commandToServer('SlideshowStop');
    return ;
}
function SlideshowGui::GuiToPrefs()
{
    $userPref::slideshow::objName = guiSlideShowFieldObjName.getValue();
    $userPref::slideshow::baseURL = guiSlideShowFieldBaseUrl.getValue();
    $userPref::slideshow::picMin = guiSlideShowFieldPicMin.getValue();
    $userPref::slideshow::picMax = guiSlideShowFieldPicMax.getValue();
    $userPref::slideshow::fileName = guiSlideShowFieldFilename.getValue();
    $userPref::slideshow::periodSecs = guiSlideShowFieldPeriodSecs.getValue();
    $UserPref::slideshow::random = guiSlideShowFieldRandom.getValue();
    return ;
}
function SlideshowGui::previewParams()
{
    SlideshowGui::GuiToPrefs();
    %fn = "";
    %fn = %fn @ $userPref::slideshow::baseURL;
    %fn = %fn @ formatInt("%0.4d", $userPref::slideshow::picMin);
    %fn = %fn @ $userPref::slideshow::fileName;
    guiSlideShowFieldExample.setValue(%fn);
    return ;
}
function SlideshowGui::viewSampleImage()
{
    SlideshowGui::previewParams();
    gotoWebPage(guiSlideShowFieldExample.getValue(), 0);
    return ;
}
function SlideshowGui::sampleSettings()
{
    guiSlideShowFieldBaseUrl.setValue("http://s-download/content/events/tyera/slides/");
    guiSlideShowFieldFilename.setValue("_tyeraslides.png");
    guiSlideShowFieldObjName.setValue("tyeraset");
    guiSlideShowFieldPeriodSecs.setValue(20);
    guiSlideShowFieldPicMin.setValue(6);
    guiSlideShowFieldPicMax.setValue(6);
    guiSlideShowFieldRandom.setValue(0);
    return ;
}
$gEventControlsWindow::initialized = 0;
function eventControlWindow::populateDoorsList(%this)
{
    EventControlsDoorsArray.deleteMembers();
    %n = 0;
    while (%n < $gDoorsNum)
    {
        %this.addDoorControl($gDoorNames[%n], $gDoorGroupNames[%n], $gDoorZoneNames[%n], $gDoorToLockNames[%n], $gDoorCSN[%n] $= $gContiguousSpaceName);
        %n = %n + 1;
    }
}

function eventControlWindow::addDoorControl(%this, %title, %groupName, %zoneName, %doorToLockName, %enable)
{
    %container = EventControlsDoorsArray.addChild();
    %ctrl = new GuiTextCtrl()
    {
        position = "0 0";
        extent = "122 17";
        text = %title;
    };
    %container.add(%ctrl);
    %ctrl = new GuiButtonCtrl()
    {
        profile = "GuiClickLabelProfile";
        position = "122 0";
        extent = "19 17";
        text = "go";
        command = "CommandToServer(\'GenericDoors\', 2, \"" @ %groupName @ "\",\"" @ %zoneName @ "\",\"" @ %doorToLockName @ "\");";
    };
    %container.add(%ctrl);
    %ctrl = new GuiButtonCtrl()
    {
        profile = "GuiClickLabelProfile";
        position = "141 0";
        extent = "19 17";
        text = "(X)";
        command = "CommandToServer(\'GenericDoors\', 1, \"" @ %groupName @ "\",\"" @ %zoneName @ "\",\"" @ %doorToLockName @ "\");";
    };
    %container.add(%ctrl);
    %ctrl = new GuiButtonCtrl()
    {
        profile = "GuiClickLabelProfile";
        position = "161 0";
        extent = "19 17";
        text = "( )";
        command = "CommandToServer(\'GenericDoors\', 0, \"" @ %groupName @ "\",\"" @ %zoneName @ "\",\"" @ %doorToLockName @ "\");";
    };
    %container.add(%ctrl);
    if (!%enable)
    {
        %ctrl = new GuiControl()
        {
            profile = "GuiTranslucentProfile";
            position = "0 0";
            extent = %container.extent;
        };
        %container.add(%ctrl);
    }
    EventControlsDoorsArray.reseatChildren();
    return ;
}
function clientCmdbeginZombieScores()
{
    if (isObject($ZombieGamePointsCollector))
    {
        $ZombieGamePointsCollector.delete();
    }
    $ZombieGamePointsCollector = new StringMap();
    echo("recieving zombie scores....");
    return ;
}
function clientCmdnextZombieGameScore(%name, %points)
{
    echo("got another zombie score....");
    $ZombieGamePointsCollector.put(%name, %points);
    return ;
}
$gZombieScoresString = "";
function StringMap::dumpAZombieScore(%this, %key, %value)
{
    %line = %key SPC "," SPC %value;
    $gZombieScoresString = $gZombieScoresString NL %line;
    return ;
}
function clientCmdendZombieScores()
{
    %logName = "localZombieScores";
    $gZombieScoresString = "";
    error("==================");
    error("==================");
    error("==================");
    error("ZOMBIE GAME SCORES!!");
    error("==================");
    $ZombieGamePointsCollector.forEach("dumpAZombieScore");
    echo($gZombieScoresString);
    error("==================");
    error("==================");
    error("==================");
    setClipboard($gZombieScoresString);
    if (isObject($ZombieGamePointsCollector))
    {
        $ZombieGamePointsCollector.delete();
    }
    return ;
}
