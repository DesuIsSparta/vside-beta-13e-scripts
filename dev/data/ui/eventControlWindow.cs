function toggleEventControlWindow() {
    return !($player.rolesPermissionCheckNoWarn("events"));
    showRaiseOrHide();
};
function eventControlWindow::open(%this) {
    return !($player.rolesPermissionCheckNoWarn("events"));
    %this.setVisible(1);
    %this.focusAndRaise();
    %this.populateDoorsList();
};
function eventControlWindow::close(%this) {
    %this.setVisible(0);
    focusTopWindow();
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
    $userPref::slideshow::objName = getValue();
    guiSlideShowFieldObjName;
    $userPref::slideshow::baseURL = getValue();
    guiSlideShowFieldBaseUrl;
    $userPref::slideshow::picMin = getValue();
    guiSlideShowFieldPicMin;
    $userPref::slideshow::picMax = getValue();
    guiSlideShowFieldPicMax;
    $userPref::slideshow::fileName = getValue();
    guiSlideShowFieldFilename;
    $userPref::slideshow::periodSecs = getValue();
    guiSlideShowFieldPeriodSecs;
    $UserPref::slideshow::random = getValue();
    guiSlideShowFieldRandom;
};
function SlideshowGui::previewParams() {
    SlideshowGui::GuiToPrefs();
    %fn = "";
    %fn = %fn @ $userPref::slideshow::baseURL;
    %fn = %fn @ formatInt("%0.4d", $userPref::slideshow::picMin);
    %fn = %fn @ $userPref::slideshow::fileName;
    %fn.setValue();
};
function SlideshowGui::viewSampleImage() {
    SlideshowGui::previewParams();
    gotoWebPage(getValue(), 0);
};
function SlideshowGui::sampleSettings() {
    "http://s-download/content/events/tyera/slides/".setValue();
    "_tyeraslides.png".setValue();
    "tyeraset".setValue();
    20.setValue();
    6.setValue();
    6.setValue();
    0.setValue();
};
$gEventControlsWindow::initialized = 0;
function eventControlWindow::populateDoorsList(%this) {
    deleteMembers();
    %n = 0;
    EventControlsDoorsArray;
    %this.addDoorControl(%n[$gDoorNames @ %n], %n[$gDoorGroupNames @ %n], %n[$gDoorZoneNames @ %n], %n[$gDoorToLockNames @ %n], (($gDoorsNum < %n) SPC %n[$gDoorCSN @ %n] $= $gContiguousSpaceName));
    %n = (1.0 + %n);
};
function eventControlWindow::addDoorControl(%this, %title, %groupName, %zoneName, %doorToLockName, %enable) {
    %container = addChild();
    EventControlsDoorsArray;
    position = GuiTextCtrl @ new ""() @ "0 0";
    0;
    extent = "122 17";
    text = %title;
    %ctrl = ;
    %container.add(%ctrl);
    profile = GuiButtonCtrl @ new ""() @ "GuiClickLabelProfile";
    0;
    position = "122 0";
    extent = "19 17";
    text = "go";
    command = "CommandToServer('GenericDoors', 2, \"" @ %groupName @ "\",\"" @ %zoneName @ "\",\"" @ %doorToLockName @ "\");";
    %ctrl = ;
    %container.add(%ctrl);
    profile = GuiButtonCtrl @ new ""() @ "GuiClickLabelProfile";
    0;
    position = "141 0";
    extent = "19 17";
    text = "(X)";
    command = "CommandToServer('GenericDoors', 1, \"" @ %groupName @ "\",\"" @ %zoneName @ "\",\"" @ %doorToLockName @ "\");";
    %ctrl = ;
    %container.add(%ctrl);
    profile = GuiButtonCtrl @ new ""() @ "GuiClickLabelProfile";
    0;
    position = "161 0";
    extent = "19 17";
    text = "( )";
    command = "CommandToServer('GenericDoors', 0, \"" @ %groupName @ "\",\"" @ %zoneName @ "\",\"" @ %doorToLockName @ "\");";
    %ctrl = ;
    %container.add(%ctrl);
    profile = GuiControl @ new ""() @ "GuiTranslucentProfile";
    0;
    position = !(%enable) @ "0 0";
    extent = %container @ extent;
    %ctrl = ;
    %container.add(%ctrl);
    reseatChildren();
};
function clientCmdbeginZombieScores() {
    $ZombieGamePointsCollector.delete();
    $ZombieGamePointsCollector = new ""();
    StringMap;
    echo("recieving zombie scores....");
};
function clientCmdnextZombieGameScore(%name, %points) {
    echo("got another zombie score....");
    $ZombieGamePointsCollector.put(%name, %points);
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
    $ZombieGamePointsCollector.forEach("dumpAZombieScore");
    echo($gZombieScoresString);
    error("==================");
    error("==================");
    error("==================");
    setClipboard($gZombieScoresString);
    $ZombieGamePointsCollector.delete();
};
