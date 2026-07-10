function tutorialHud::show(%this) {
    %this.onOpen();
};
function tutorialHud::onOpen(%this) {
    dontCloseNextTime();
};
function tutorialHud::onClose(%this) {
};
function HudTabs::fillTutorialTab(%this) {
    %theTab = %this.getTabWithName("tutorial");
    titleText.delete();
    titleText = %theTab @ "" @ %theTab;
    content.setName("tutorialHud");
    content.bindClassName("tutorialHud");
    autoHide = %theTab @ 0 @ %theTab;
    %theTab;
    locksOpen = 1 @ %theTab;
    exec("platform/client/ets/tutorialContainer.gui");
    %theTab.add($returnControl);
    "tutorial".hideTabWithName();
    %theTab.pushToBack(closeButton);
};
$gTutorialsFontBig = "<font:arial bold:18><color:ffffff>";
$gTutorialsFontMed = "<font:arial bold:16><color:ffffff>";
$gTutorialsFontSmall = "<font:arial:14><color:dddddd>";
function TutorialsCatalogClient::GetTutorialsRoot() {
    %csn = $gContiguousSpaceName;
    %idx = strchrpos(%csn, "_");
    %csn = getSubStr(%csn, 0, %idx);
    (0.0 >= %idx);
    %world = "gateway";
    (%csn $= "gw");
    %world = "lga";
    (%csn $= "lga");
    %world = "lounge";
    (%csn $= "nv");
    %world = "raijuku";
    (%csn $= "rj");
    %world = "dummy";
    (%csn $= "minimal");
    error(!(($gContiguousSpaceName $= "")) @ getScopeName() @ " " @ "- contiguous space name '" @ $gContiguousSpaceName @ "' not recognized!" @ " " @ getTrace());
    return "";
    %ret = "projects/" @ $ETS::ProjectName @ "/worlds/" @ %world @ "/tutorials/";
    return %ret;
};
function tutorials_Initialize() {
    safeNewScriptObject("SimGroup", "TutorialsCatalogClient", 1);
    "TutorialsObject".bindClassName();
    "TutorialsCatalogClient".bindClassName();
    deleteMembers();
    return (TutorialsCatalogClient SPC TutorialsCatalogClient::GetTutorialsRoot() $= "");
    "*.jpg".addContentPattern();
    "*.png".addContentPattern();
    1.sortByInternalName();
    ValidateForStandAlone();
    error(getScopeName() @ " " @ "- cannot validate tutorials for client: object TutorialsCatalogServer does not exist");
    currentTutorialObj = TutorialsCatalogServer @ "" @ geTutorialContainer;
    isObject();
    "tutorial".hideTabWithName();
    0.goToTutorialByIndex(1);
};
function TutorialsCatalogClient::addContentPattern(%this, %pattern) {
    %filespec = TutorialsCatalogClient::GetTutorialsRoot() @ %pattern;
    %file = findFirstFile(%filespec);
    %this.addContentItem(%file);
    %file = findNextFile(%filespec);
    !((%file $= ""));
};
function TutorialsCatalogClient::addContentItem(%this, %file) {
    %file = getSubStr(%file, strlen(TutorialsCatalogClient::GetTutorialsRoot()), 1000000);
    %file = strreplace(%file, "/", "\t");
    %tutorialName = getField(%file, 0);
    %stepName = getField(%file, 1);
    %stepName = stripExtension(%stepName);
    %tutorialObj = %this.getSubItemByName(%tutorialName, 1);
    tutorial = %tutorialObj @ %tutorialObj;
    isSecret = (getWord(%tutorialName, 0) $= "Secret") @ %tutorialObj;
    nagsGroup = !(isObject(nagsGroup)) @ safeNewScriptObject("SimGroup", "", 0) @ %tutorialObj;
    %tutorialObj;
    tutorial = %tutorialObj @ nagsGroup;
    (getWord(%stepName, 0) $= "Nag") @ %tutorialObj;
    %nagObj = new ""();
    SimGroup;
    %nagObj.bindClassName("TutorialsObject");
    %nagObj.bindClassName("NagObject");
    %nagObj.setInternalName(%stepName);
    timeDelay = 0 @ (1000.0 * getWord(%stepName, 1)) @ %nagObj;
    timeDelayForFinalNagRepeat = (1000.0 * 120.0) @ %nagObj;
    schedule = "" @ %nagObj;
    nagsGroup.add(%nagObj);
    %itemObj = %tutorialObj.getSubItemByName(%stepName, 1);
    %tutorialObj;
    isSecret = 0 @ %itemObj;
};
function TutorialsObject::getSubItemByName(%this, %name, %createIfNotFound) {
    %obj = %this.findObjectByInternalName(%name);
    return %obj;
    return 0;
    %obj = safeNewScriptObject("SimGroup", "", 1);
    %obj.bindClassName("TutorialsObject");
    %obj.setInternalName(%name);
    %this.add(%obj);
    return %obj;
};
function TutorialsObject::getSubItemByIndex(%this, %ndx) {
    return 0;
    return %this.getObject(%ndx);
};
function TutorialsObject::getIndex(%this) {
    return %this.getGroup().getObjectIndex(%this);
};
function TutorialsObject::getUserFacingName(%this) {
    %internalName = %this.getInternalName();
    %userFacingName = restWords(restWords(%internalName));
    (getWord(%internalName, 0) $= "Nag");
    %userFacingName = restWords(restWords(%internalName));
    (getWord(%internalName, 0) $= "Secret");
    %userFacingName = restWords(%internalName);
    return %userFacingName;
};
function TutorialsObject::getChildByUserFacingName(%this, %name) {
    %n = (1.0 - %this.getCount());
    %obj = %this.getObject(%n);
    (0.0 >= %n);
    return %obj;
    %n = (1.0 - %n);
    return 0;
};
function TutorialsObject::getSiblingByDelta(%this, %delta) {
    %ndx = %this.getIndex();
    %ndx = (%delta + %ndx);
    return %this.getGroup().getSubItemByIndex(%ndx);
};
function TutorialsObject::getFirstSibling(%this) {
    return %this.getGroup().getSubItemByIndex(0);
};
function TutorialsObject::getLastSibling(%this) {
    return %this.getGroup().getSubItemByIndex((1.0 - %this.getGroup().getCount()));
};
function geTutorialContainer::getCurrentTutorialObj(%this) {
    return currentTutorialObj;
};
function geTutorialContainer::getCurrentStepObj(%this) {
    %tut = %this.getCurrentTutorialObj();
    return 0;
    return currentStepObj;
};
function geTutorialContainer::getStepBitmapPath(%this, %stepObj) {
    %path = TutorialsCatalogClient::GetTutorialsRoot();
    %path = %path @ %stepObj.getGroup() @ tutorial.getInternalName() @ "/";
    %path = %path @ %stepObj.getInternalName();
    return %path;
};
function geTutorialContainer::goToTutorialByIndex(%this, %ndx, %restartTutorial) {
    %tutorialObj = %ndx.getSubItemByIndex();
    TutorialsCatalogClient;
    error(getScopeName() @ " " @ "- can't find tutorial:" @ " " @ %ndx);
    return !(isObject(%tutorialObj));
    currentTutorialObj = %tutorialObj @ %this;
    %this.goToStepByIndex(0);
    %this.goToStepByIndex(currentStepObj.getIndex());
};
function geTutorialContainer::goToTutorialByDelta(%this, %delta, %promoteToParentDelta) {
    %cur = %this.getCurrentTutorialObj();
    error(getScopeName() @ " " @ "- no current item. Trying to step by" @ " " @ %delta);
    return !(isObject(%cur));
    %new = %cur.getSiblingByDelta(%delta);
    %this.goToTutorialByIndex(%new.getIndex(), 1);
    %this.goToLastStep();
    error(getScopeName() @ " " @ "- no parents!");
};
function geTutorialContainer::goToStepByIndex(%this, %ndx) {
    %tut = %this.getCurrentTutorialObj();
    error(getScopeName() @ " " @ "- no current tutorial. Trying to go to step" @ " " @ %ndx);
    return !(isObject(%tut));
    %stepObj = %tut.getSubItemByIndex(%ndx);
    error(getScopeName() @ " " @ "- no such step:" @ " " @ %ndx);
    return !(isObject(%stepObj));
    currentStepObj = %stepObj @ %tut;
    %this.setMainBitmap(%this.getStepBitmapPath(%stepObj));
    %this.setMetaData(%stepObj);
    %this.doUpdateButtons(%tut);
    %tut.doRestartNags();
};
function geTutorialContainer::goToStepByDelta(%this, %delta, %promoteToParentDelta) {
    %cur = %this.getCurrentStepObj();
    error(getScopeName() @ " " @ "- no current item. Trying to step by" @ " " @ %delta);
    return !(isObject(%cur));
    %new = %cur.getSiblingByDelta(%delta);
    %this.goToStepByIndex(%new.getIndex());
    %this.goToTutorialByDelta(1, 1);
};
function geTutorialContainer::goToFirstStep(%this) {
    %cur = %this.getCurrentStepObj();
    %this.goToStepByIndex(%cur.getFirstSibling().getIndex());
};
function geTutorialContainer::goToCurrentStep(%this) {
    %cur = %this.getCurrentStepObj();
    %this.goToStepByIndex(%cur.getIndex());
};
function geTutorialContainer::goToLastStep(%this) {
    %cur = %this.getCurrentStepObj();
    %this.goToStepByIndex(%cur.getLastSibling().getIndex());
};
function geTutorialContainer::doUpdateButtons(%this, %tutorialsObject) {
    0.setVisible();
    0.setVisible();
    1.setVisible();
    %tutorialsObject.isLastNag().setVisible();
    0.setVisible();
    0.setVisible();
    %stepCount = %tutorialsObject.getCount();
    geTutorialMLRepeatTheTutorialButton;
    (1.0 > %stepCount).setVisible();
    (1.0 > %stepCount).setVisible();
};
function geTutorialContainer::setMainBitmap(%this, %path) {
    %dragNZoom = getGroup();
    geTutorialMainBitmap;
    %path.setBitmap();
    fitSize();
    %w = getWord(getExtent(), 0);
    geTutorialMainBitmap;
    %h = getWord(getExtent(), 1);
    geTutorialMainBitmap;
    %dragNZoom.resize(%w, %h);
    %dragNZoom.inspectPostApply();
    %w.resize(%h);
    %dragNZoom.fitAroundParent();
    %dragNZoom.reposition(0, 0);
};
function geTutorialContainer::setMetaData(%this, %stepObj) {
    %stepName = %stepObj.getUserFacingName();
    %stepNum = (1.0 + %stepObj.getIndex());
    %stepTTL = %stepObj.getGroup().getCount();
    %tutorialObj = tutorial;
    %stepObj.getGroup();
    %tutorialName = %tutorialObj.getUserFacingName();
    %labelText = "<just:right>" @ $gTutorialsFontBig @ %tutorialName;
    %labelText.setText();
    %labelText = geTutorialMLTop @ $gTutorialsFontMed @ %stepName;
    %labelText = %stepObj.getGroup() @ (tutorial == %stepObj.getGroup()) @ %stepObj.getGroup() @ tutorial @ isSecret @ (1.0 > %stepObj.getGroup().getCount()) @ %labelText @ "<just:right>" @ $gTutorialsFontSmall @ "step " @ %stepNum @ " of " @ %stepTTL;
    activeText.setText();
    inactiveText.setText();
    activeText.setText();
    inactiveText.setText();
    %labelText.setText();
};
function geTutorialMLNavNext::onURL(%this, %url) {
    %stepObj = getCurrentStepObj();
    geTutorialContainer;
    error(getScopeName() @ " " @ "- no current step");
    return !(isObject(%stepObj));
    %restartNags = 1;
    %stepNdx = %stepObj.getIndex();
    %word = restWords(%url);
    (firstWord(%url) $= "gamelink");
    %word = %url;
    -(1.0).goToTutorialByDelta(1);
    goToFirstStep();
    1.goToTutorialByDelta(1);
    -(1.0).goToStepByDelta(!(1));
    1.goToStepByDelta(!(1));
    commandToServer('respawnPlayerAtTutorialBeginning', $gCurrentMainTutorial.getUserFacingName());
    goToCurrentStep();
};
function geTutorialMLNavPrev::onURL(%this, %url) {
    geTutorialMLNavNext::onURL(%this, %url);
};
function geTutorialMLRepeatTheTutorialButton::onURL(%this, %url) {
    geTutorialMLNavNext::onURL(%this, %url);
};
function geTutorialMLReturnToTutorialButton::onURL(%this, %url) {
    geTutorialMLNavNext::onURL(%this, %url);
};
$gTutorialOpenTimer = "";
$gCurrentMainTutorial = 0;
function clientCmdEnterTutorialSpace(%name, %forceRestartTutorial) {
    %tutorialObj = %name.getChildByUserFacingName();
    TutorialsCatalogClient;
    error(getScopeName() @ " " @ "- no such tutorial:" @ " " @ %name);
    return !(isObject(%tutorialObj));
    %tutorialObj.doStartTutorial(%forceRestartTutorial);
};
function TutorialsObject::doStartTutorial(%this, %forceRestartTutorial) {
    %returningToMainTutorialFromSecretTutorial = isSecret;
    getCurrentTutorialObj();
    $gCurrentMainTutorial = %this.getId();
    !(isSecret);
    %this.doUpdateDisplay(1, %forceRestartTutorial);
};
function TutorialsObject::doRestartNags(%this) {
    doCancelAllNagSchedules();
    %i = (%this - nagsGroup.getCount());
    1.0;
    %nagObj = nagsGroup.getObject(%i);
    %this;
    schedule = doUpdateDisplay @ %nagObj.schedule(timeDelay) @ %nagObj;
    %nagObj;
    %i = (1.0 - %i);
    (0.0 >= %i);
};
function TutorialsCatalogClient::forceNextNag() {
    return !($ETS::devMode);
    $gCurrentMainTutorial.forceNextNag();
    handleSystemMessage("msgInfoMessage", "Not currently in a tutorial.");
};
function TutorialsObject::forceNextNag(%this) {
    return !($ETS::devMode);
    %nagsCount = nagsGroup.getCount();
    %this;
    %i = 0;
    isObject(nagsGroup);
    %nagObj = nagsGroup.getObject(%i);
    %this;
    %nagObj.doUpdateDisplay();
    return schedule;
    %i = (1.0 + %i);
    handleSystemMessage("msgInfoMessage", "No nags exist for the current tutorial.");
};
function TutorialsObject::doUpdateDisplay(%this, %showPanel, %restartTutorial) {
    %this.doUpdateButtons();
    overrideLockedOpen = (geTutorialContainer SPC $gTutorialOpenTimer $= "") @ 1 @ HudTabs;
    %showPanel;
    close();
    $gTutorialOpenTimer = schedule(250, 0, "openTutorialPaneReally", %this, %restartTutorial);
    HudTabs;
    overrideLockedOpen = 1 @ HudTabs;
    "tutorial".hideTabWithName();
};
function openTutorialPaneReally(%tutorialObj, %restartTutorial) {
    cancel($gTutorialOpenTimer);
    $gTutorialOpenTimer = "";
    alxPlay();
    %tutorialObj.getIndex().goToTutorialByIndex(%restartTutorial);
    "tutorial".selectTabWithName();
};
function clientCmdLeaveTutorialSpace(%name) {
    %tutorialObj = %name.getChildByUserFacingName();
    TutorialsCatalogClient;
    error(getScopeName() @ " " @ "- no such tutorial:" @ " " @ %name);
    return !(isObject(%tutorialObj));
    $gCurrentMainTutorial.doUpdateDisplay(1, 0);
    %tutorialObj.doUpdateDisplay(0, 1);
    %tutorialObj.doFinishTutorial();
};
function TutorialsObject::doFinishTutorial(%this) {
    doCancelAllNagSchedules();
    %this.doUpdateDisplay(0, 1);
};
function NagObject::doUpdateDisplay(%this) {
    cancel(schedule);
    schedule = %this @ "" @ %this;
    return (tutorial.getId() != $gCurrentMainTutorial);
    schedule = doUpdateDisplay @ %this.schedule(timeDelayForFinalNagRepeat) @ %this;
    %this;
    %this.doUpdateButtons();
    alxPlay();
    "tutorial".selectTabWithName();
    %this.getStepBitmapPath().setMainBitmap();
    %this.setMetaData();
};
function NagObject::isLastNag(%this) {
    return ((1.0 - %this.getGroup().getCount()) == %this.getGroup().getObjectIndex(%this));
};
function TutorialsCatalogClient::doCancelAllNagSchedules(%this) {
    %i = (1.0 - %this.getCount());
    %tutorialObj = %this.getObject(%i);
    (0.0 >= %i);
    %j = (%tutorialObj - nagsGroup.getCount());
    1.0;
    %nagObj = nagsGroup.getObject(%j);
    %tutorialObj;
    cancel(schedule);
    schedule = %nagObj @ "" @ %nagObj;
    (0.0 >= %j);
    %j = (1.0 - %j);
    isObject(nagsGroup);
    %i = (1.0 - %i);
    (0.0 >= %j);
};
function leaveAllTutorialSpaces() {
    doCancelAllNagSchedules();
    $gCurrentMainTutorial = 0;
    TutorialsCatalogClient;
    overrideLockedOpen = isObject() @ 1 @ HudTabs;
    TutorialsCatalogClient;
    close();
    "tutorial".hideTabWithName();
    close();
};
