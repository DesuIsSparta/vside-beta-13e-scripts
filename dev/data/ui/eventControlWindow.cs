function toggleEventControlWindow() {
    if (!("events".rolesPermissionCheckNoWarn($player))) {
        return;
    }
    eventControlWindow.showRaiseOrHide(playGui);
};
function eventControlWindow::open(%this) {
    if (!("events".rolesPermissionCheckNoWarn($player))) {
        return;
    }
    1.setVisible(%this);
    %this.focusAndRaise(playGui);
    %this.populateDoorsList();
};
function eventControlWindow::close(%this) {
    0.setVisible(%this);
    playGui.focusTopWindow();
    return 1;
};
function eventControlWindow::toggleChristmas(%this) {
    commandToServer('ChristmasEventToggle');
};
function eventControlWindow::toggleSnow(%this) {
    commandToServer('ChristmasEventToggleSnow');
};
function eventControlWindow::toggleHeavyMist(%this) {
    commandToServer('ChristmasEventToggleHeavyMist');
};
function eventControlWindow::toggleCostumes(%this) {
    commandToServer('ChristmasEventCostumesToggle');
};
function eventControlWindow::getEventStatus(%this) {
    commandToServer('ChristmasEventGetStatus');
};
function HalloweenSpecifics::toggleHalloween(%this) {
    commandToServer('HalloweenEventToggle');
};
function HalloweenSpecifics::toggleLightning(%this) {
    commandToServer('HalloweenEventLightningToggle');
};
function HalloweenSpecifics::lightningStrike(%this) {
    commandToServer('HalloweenEventDoLightning');
};
function HalloweenSpecifics::toggleZombieGame(%this) {
    commandToServer('HalloweenEventZombieGameToggle');
};
function HalloweenSpecifics::toggleCostumes(%this) {
    commandToServer('HalloweenEventCostumesToggle');
};
function HalloweenSpecifics::getEventStatus(%this) {
    commandToServer('HalloweenEventGetStatus');
};
function HalloweenSpecifics::dumpGameStandings(%this) {
    commandToServer('HalloweenEventDumpUnsortedStandingsToLog');
};
function SlideshowGui::start() {
    SlideshowGui::GuiToPrefs();
    commandToServer('SlideshowStart', $userPref::slideshow::objName, $userPref::slideshow::baseURL, $userPref::slideshow::picMin, $userPref::slideshow::picMax, $userPref::slideshow::fileName, $userPref::slideshow::periodSecs, $UserPref::slideshow::random);
};
function SlideshowGui::pause() {
    commandToServer('SlideshowPause');
};
function SlideshowGui::stop() {
    commandToServer('SlideshowStop');
};
function SlideshowGui::GuiToPrefs() {
    $userPref::slideshow::objName = guiSlideShowFieldObjName.getValue();
    $userPref::slideshow::baseURL = guiSlideShowFieldBaseUrl.getValue();
    $userPref::slideshow::picMin = guiSlideShowFieldPicMin.getValue();
    $userPref::slideshow::picMax = guiSlideShowFieldPicMax.getValue();
    $userPref::slideshow::fileName = guiSlideShowFieldFilename.getValue();
    $userPref::slideshow::periodSecs = guiSlideShowFieldPeriodSecs.getValue();
    $UserPref::slideshow::random = guiSlideShowFieldRandom.getValue();
};
function SlideshowGui::previewParams() {
    SlideshowGui::GuiToPrefs();
    %fn = "";
    %fn = %fn @ $userPref::slideshow::baseURL;
    %fn = %fn @ formatInt("%0.4d", $userPref::slideshow::picMin);
    %fn = %fn @ $userPref::slideshow::fileName;
    %fn.setValue(guiSlideShowFieldExample);
};
function SlideshowGui::viewSampleImage() {
    SlideshowGui::previewParams();
    gotoWebPage(guiSlideShowFieldExample.getValue(), 0);
};
function SlideshowGui::sampleSettings() {
    "http://s-download/content/events/tyera/slides/".setValue(guiSlideShowFieldBaseUrl);
    "_tyeraslides.png".setValue(guiSlideShowFieldFilename);
    "tyeraset".setValue(guiSlideShowFieldObjName);
    20.setValue(guiSlideShowFieldPeriodSecs);
    6.setValue(guiSlideShowFieldPicMin);
    6.setValue(guiSlideShowFieldPicMax);
    0.setValue(guiSlideShowFieldRandom);
};
$gEventControlsWindow::initialized = 0;
function eventControlWindow::populateDoorsList(%this) {
    EventControlsDoorsArray.deleteMembers();
    %n = 0;
    while ((%n < $gDoorsNum)) {
        (%n[$gDoorCSN @ %n] $= $gContiguousSpaceName).addDoorControl(%this, %n[$gDoorNames @ %n], %n[$gDoorGroupNames @ %n], %n[$gDoorZoneNames @ %n], %n[$gDoorToLockNames @ %n]);
        %n = (%n + 1.0);
    }
};
function eventControlWindow::addDoorControl(%this, %title, %groupName, %zoneName, %doorToLockName, %enable) {
    %container = EventControlsDoorsArray.addChild();
    %ctrl = new GuiTextCtrl("") {
        position = 0 @ "0 0";
        extent = "122 17";
        text = %title;
    };
    %ctrl.add(%container);
    %ctrl = new GuiButtonCtrl("") {
        profile = 0 @ "GuiClickLabelProfile";
        position = "122 0";
        extent = "19 17";
        text = "go";
        command = "CommandToServer('GenericDoors', 2, \"" @ %groupName @ "\",\"" @ %zoneName @ "\",\"" @ %doorToLockName @ "\");";
    };
    %ctrl.add(%container);
    %ctrl = new GuiButtonCtrl("") {
        profile = 0 @ "GuiClickLabelProfile";
        position = "141 0";
        extent = "19 17";
        text = "(X)";
        command = "CommandToServer('GenericDoors', 1, \"" @ %groupName @ "\",\"" @ %zoneName @ "\",\"" @ %doorToLockName @ "\");";
    };
    %ctrl.add(%container);
    %ctrl = new GuiButtonCtrl("") {
        profile = 0 @ "GuiClickLabelProfile";
        position = "161 0";
        extent = "19 17";
        text = "( )";
        command = "CommandToServer('GenericDoors', 0, \"" @ %groupName @ "\",\"" @ %zoneName @ "\",\"" @ %doorToLockName @ "\");";
    };
    %ctrl.add(%container);
    if (!(%enable)) {
        %ctrl = new GuiControl("") {
            profile = 0 @ "GuiTranslucentProfile";
            position = "0 0";
            extent = %container.extent;
        };
        %ctrl.add(%container);
    }
    EventControlsDoorsArray.reseatChildren();
};
function clientCmdbeginZombieScores() {
    if (isObject($ZombieGamePointsCollector)) {
        $ZombieGamePointsCollector.delete();
    }
    $ZombieGamePointsCollector = new StringMap("");;
    0;
    echo("recieving zombie scores....");
};
function clientCmdnextZombieGameScore(%name, %points) {
    echo("got another zombie score....");
    %points.put($ZombieGamePointsCollector, %name);
};
$gZombieScoresString = "";
function StringMap::dumpAZombieScore(%this, %key, %value) {
    %line = %key @ " " @ "," @ " " @ %value;
    $gZombieScoresString = $gZombieScoresString @ "\n" @ %line;
};
function clientCmdendZombieScores() {
    %logName = "localZombieScores";
    $gZombieScoresString = "";
    error("==================");
    error("==================");
    error("==================");
    error("ZOMBIE GAME SCORES!!");
    error("==================");
    "dumpAZombieScore".forEach($ZombieGamePointsCollector);
    echo($gZombieScoresString);
    error("==================");
    error("==================");
    error("==================");
    setClipboard($gZombieScoresString);
    if (isObject($ZombieGamePointsCollector)) {
        $ZombieGamePointsCollector.delete();
    }
};
