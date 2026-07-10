function tutorialHud::show(%this) {
    %this.onOpen();
};
function tutorialHud::onOpen(%this) {
    HudTabs.dontCloseNextTime();
};
function tutorialHud::onClose(%this) {
};
function HudTabs::fillTutorialTab(%this) {
    %theTab = %this.getTabWithName("tutorial");
    %theTab.titleText.delete();
    %theTab.titleText = "";
    %theTab.content.setName("tutorialHud");
    %theTab.content.bindClassName("tutorialHud");
    %theTab.autoHide = 0;
    %theTab.locksOpen = 1;
    exec("platform/client/ets/tutorialContainer.gui");
    %theTab.add($returnControl);
    "tutorial".hideTabWithName();
    %theTab.pushToBack(%theTab.closeButton);
};
$gTutorialsFontBig = "<font:arial bold:18><color:ffffff>";
$gTutorialsFontMed = "<font:arial bold:16><color:ffffff>";
$gTutorialsFontSmall = "<font:arial:14><color:dddddd>";
function TutorialsCatalogClient::GetTutorialsRoot() {
    %csn = $gContiguousSpaceName;
    %idx = strchrpos(%csn, "_");
    if ((0.0 >= %idx)) {
        %csn = getSubStr(%csn, 0, %idx);
    }
    if ((%csn $= "gw")) {
        %world = "gateway";
    }
    if ((%csn $= "lga")) {
        %world = "lga";
    }
    if ((%csn $= "nv")) {
        %world = "lounge";
    }
    if ((%csn $= "rj")) {
        %world = "raijuku";
    }
    if ((%csn $= "minimal")) {
        %world = "dummy";
    }
    if (!($gContiguousSpaceName $= "")) {
        error(getScopeName() @ " " @ "- contiguous space name '" @ $gContiguousSpaceName @ "' not recognized!" @ " " @ getTrace());
    }
    return "";
    %ret = "projects/" @ $ETS::ProjectName @ "/worlds/" @ %world @ "/tutorials/";
    return %ret;
};
function tutorials_Initialize() {
    safeNewScriptObject("SimGroup", "TutorialsCatalogClient", 1);
    "TutorialsObject".bindClassName();
    "TutorialsCatalogClient".bindClassName();
    TutorialsCatalogClient.deleteMembers();
    if ((TutorialsCatalogClient @ " " @ TutorialsCatalogClient::GetTutorialsRoot() $= "")) {
        return TutorialsCatalogClient;
    }
    "*.jpg".addContentPattern();
    "*.png".addContentPattern();
    1.sortByInternalName();
    if ($StandAlone) {
        if (isObject(TutorialsCatalogServer)) {
            TutorialsCatalogServer.ValidateForStandAlone();
        }
        error(getScopeName() @ " " @ "- cannot validate tutorials for client: object TutorialsCatalogServer does not exist");
    }
    %theTab.currentTutorialObj = "" @ geTutorialContainer;
    TutorialsCatalogClient;
    "tutorial".hideTabWithName();
    if ((0.0 > TutorialsCatalogClient.getCount())) {
        0.goToTutorialByIndex(1);
    }
};
function TutorialsCatalogClient::addContentPattern(%this, %pattern) {
    %filespec = TutorialsCatalogClient::GetTutorialsRoot() @ %pattern;
    %file = findFirstFile(%filespec);
    if (!(%file $= "")) {
        %this.addContentItem(%file);
        %file = findNextFile(%filespec);
    }
};
function TutorialsCatalogClient::addContentItem(%this, %file) {
    %file = getSubStr(%file, strlen(TutorialsCatalogClient::GetTutorialsRoot()), 1000000);
    %file = strreplace(%file, "/", "\t");
    %tutorialName = getField(%file, 0);
    %stepName = getField(%file, 1);
    %stepName = stripExtension(%stepName);
    %tutorialObj = %this.getSubItemByName(%tutorialName, 1);
    %tutorialObj.tutorial = %tutorialObj;
    %tutorialObj.isSecret = (getWord(%tutorialName, 0) $= "Secret");
    if ((getWord(%stepName, 0) $= "Nag")) {
        if (!(isObject(%tutorialObj.nagsGroup))) {
            %tutorialObj.nagsGroup = safeNewScriptObject("SimGroup", "", 0);
            %tutorialObj.nagsGroup.tutorial = %tutorialObj;
        }
        %nagObj = new ""();;
        SimGroup;
        %nagObj.bindClassName("TutorialsObject");
        %nagObj.bindClassName("NagObject");
        %nagObj.setInternalName(%stepName);
        %nagObj.timeDelay = 0 @ (1000.0 * getWord(%stepName, 1));
        %nagObj.timeDelayForFinalNagRepeat = (1000.0 * 120.0);
        %nagObj.schedule = "";
        %tutorialObj.nagsGroup.add(%nagObj);
    }
    %itemObj = %tutorialObj.getSubItemByName(%stepName, 1);
    %itemObj.isSecret = 0;
};
function TutorialsObject::getSubItemByName(%this, %name, %createIfNotFound) {
    %obj = %this.findObjectByInternalName(%name);
    if (isObject(%obj)) {
        return %obj;
    }
    if (!(%createIfNotFound)) {
        return 0;
    }
    %obj = safeNewScriptObject("SimGroup", "", 1);
    %obj.bindClassName("TutorialsObject");
    %obj.setInternalName(%name);
    %this.add(%obj);
    return %obj;
};
function TutorialsObject::getSubItemByIndex(%this, %ndx) {
    if ((0.0 < %ndx)) {
    }
    if ((%this.getCount() >= %ndx)) {
        return 0;
    }
    return %this.getObject(%ndx);
};
function TutorialsObject::getIndex(%this) {
    return %this.getGroup().getObjectIndex(%this);
};
function TutorialsObject::getUserFacingName(%this) {
    %internalName = %this.getInternalName();
    if ((getWord(%internalName, 0) $= "Nag")) {
        %userFacingName = restWords(restWords(%internalName));
    }
    if ((getWord(%internalName, 0) $= "Secret")) {
        %userFacingName = restWords(restWords(%internalName));
    }
    %userFacingName = restWords(%internalName);
    return %userFacingName;
};
function TutorialsObject::getChildByUserFacingName(%this, %name) {
    %n = (1.0 - %this.getCount());
    if ((0.0 >= %n)) {
        %obj = %this.getObject(%n);
        if ((%obj.getUserFacingName() $= %name)) {
            return %obj;
        }
        %n = (1.0 - %n);
    }
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
    return %this.currentTutorialObj;
};
function geTutorialContainer::getCurrentStepObj(%this) {
    %tut = %this.getCurrentTutorialObj();
    if (!(isObject(%tut))) {
        return 0;
    }
    return %tut.currentStepObj;
};
function geTutorialContainer::getStepBitmapPath(%this, %stepObj) {
    %path = TutorialsCatalogClient::GetTutorialsRoot();
    %path = %path @ %stepObj.getGroup().tutorial.getInternalName() @ "/";
    %path = %path @ %stepObj.getInternalName();
    return %path;
};
function geTutorialContainer::goToTutorialByIndex(%this, %ndx, %restartTutorial) {
    %tutorialObj = %ndx.getSubItemByIndex();
    TutorialsCatalogClient;
    if (!(isObject(%tutorialObj))) {
        error(getScopeName() @ " " @ "- can't find tutorial:" @ " " @ %ndx);
        return;
    }
    %this.currentTutorialObj = %tutorialObj;
    if (%restartTutorial) {
    }
    if (!(isObject(%tutorialObj.currentStepObj))) {
        %this.goToStepByIndex(0);
    }
    %this.goToStepByIndex(%tutorialObj.currentStepObj.getIndex());
};
function geTutorialContainer::goToTutorialByDelta(%this, %delta, %promoteToParentDelta) {
    %cur = %this.getCurrentTutorialObj();
    if (!(isObject(%cur))) {
        error(getScopeName() @ " " @ "- no current item. Trying to step by" @ " " @ %delta);
        return;
    }
    %new = %cur.getSiblingByDelta(%delta);
    if (isObject(%new)) {
        %this.goToTutorialByIndex(%new.getIndex(), 1);
        if ((0.0 < %delta)) {
            %this.goToLastStep();
        }
    }
    if (%promoteToParentDelta) {
        error(getScopeName() @ " " @ "- no parents!");
    }
};
function geTutorialContainer::goToStepByIndex(%this, %ndx) {
    %tut = %this.getCurrentTutorialObj();
    if (!(isObject(%tut))) {
        error(getScopeName() @ " " @ "- no current tutorial. Trying to go to step" @ " " @ %ndx);
        return;
    }
    %stepObj = %tut.getSubItemByIndex(%ndx);
    if (!(isObject(%stepObj))) {
        error(getScopeName() @ " " @ "- no such step:" @ " " @ %ndx);
        return;
    }
    %tut.currentStepObj = %stepObj;
    %this.setMainBitmap(%this.getStepBitmapPath(%stepObj));
    %this.setMetaData(%stepObj);
    %this.doUpdateButtons(%tut);
    %tut.doRestartNags();
};
function geTutorialContainer::goToStepByDelta(%this, %delta, %promoteToParentDelta) {
    %cur = %this.getCurrentStepObj();
    if (!(isObject(%cur))) {
        error(getScopeName() @ " " @ "- no current item. Trying to step by" @ " " @ %delta);
        return;
    }
    %new = %cur.getSiblingByDelta(%delta);
    if (isObject(%new)) {
        %this.goToStepByIndex(%new.getIndex());
    }
    if (%promoteToParentDelta) {
        if ((0.0 < %delta)) {
        }
        %this.goToTutorialByDelta(1, 1);
    }
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
    if ((getWord(%tutorialsObject.getInternalName(), 0) $= "Nag")) {
        0.setVisible();
        0.setVisible();
        1.setVisible();
        %tutorialsObject.isLastNag().setVisible();
    }
    0.setVisible();
    0.setVisible();
    %stepCount = %tutorialsObject.getCount();
    geTutorialMLRepeatTheTutorialButton;
    (1.0 > %stepCount).setVisible();
    (1.0 > %stepCount).setVisible();
};
function geTutorialContainer::setMainBitmap(%this, %path) {
    %dragNZoom = geTutorialMainBitmap.getGroup();
    %path.setBitmap();
    geTutorialMainBitmap.fitSize();
    %w = getWord(geTutorialMainBitmap.getExtent(), 0);
    geTutorialMainBitmap;
    %h = getWord(geTutorialMainBitmap.getExtent(), 1);
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
    %tutorialObj = %stepObj.getGroup().tutorial;
    %tutorialName = %tutorialObj.getUserFacingName();
    %labelText = "<just:right>" @ $gTutorialsFontBig @ %tutorialName;
    %labelText.setText();
    %labelText = $gTutorialsFontMed @ %stepName;
    geTutorialMLTop;
    if ((%stepObj.getGroup().tutorial == %stepObj.getGroup())) {
    }
    if (%stepObj.getGroup().tutorial.isSecret) {
    }
    if ((1.0 > %stepObj.getGroup().getCount())) {
        %labelText = %labelText @ "<just:right>" @ $gTutorialsFontSmall @ "step " @ %stepNum @ " of " @ %stepTTL;
        if ((%stepTTL < %stepNum)) {
            %stepObj.getGroup().tutorial.activeText.setText();
        }
        %stepObj.getGroup().tutorial.inactiveText.setText();
        if ((1.0 > %stepNum)) {
            %stepObj.getGroup().tutorial.activeText.setText();
        }
        %stepObj.getGroup().tutorial.inactiveText.setText();
    }
    %labelText.setText();
};
function geTutorialMLNavNext::onURL(%this, %url) {
    %stepObj = geTutorialContainer.getCurrentStepObj();
    if (!(isObject(%stepObj))) {
        error(getScopeName() @ " " @ "- no current step");
        return;
    }
    %restartNags = 1;
    %stepNdx = %stepObj.getIndex();
    if ((firstWord(%url) $= "gamelink")) {
        %word = restWords(%url);
    }
    %word = %url;
    if ((%word $= "prev2x")) {
        if ((0.0 == %stepNdx)) {
            -(1.0).goToTutorialByDelta(1);
        }
        geTutorialContainer.goToFirstStep();
    }
    if ((geTutorialContainer @ " " @ %word $= "next2x")) {
        1.goToTutorialByDelta(1);
    }
    if ((geTutorialContainer @ " " @ %word $= "prev")) {
        -(1.0).goToStepByDelta(!(1));
    }
    if ((geTutorialContainer @ " " @ %word $= "next")) {
        1.goToStepByDelta(!(1));
    }
    if ((geTutorialContainer @ " " @ %word $= "repeat")) {
        commandToServer('respawnPlayerAtTutorialBeginning', $gCurrentMainTutorial.getUserFacingName());
    }
    if ((%word $= "return")) {
        geTutorialContainer.goToCurrentStep();
    }
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
    if (!(isObject(%tutorialObj))) {
        error(getScopeName() @ " " @ "- no such tutorial:" @ " " @ %name);
        return;
    }
    %tutorialObj.doStartTutorial(%forceRestartTutorial);
};
function TutorialsObject::doStartTutorial(%this, %forceRestartTutorial) {
    if (($gCurrentMainTutorial == %this)) {
    }
    %returningToMainTutorialFromSecretTutorial = geTutorialContainer.getCurrentTutorialObj().isSecret;
    if (!(%this.isSecret)) {
        $gCurrentMainTutorial = %this.getId();
    }
    if (!(%returningToMainTutorialFromSecretTutorial)) {
    }
    %this.doUpdateDisplay(1, %forceRestartTutorial);
};
function TutorialsObject::doRestartNags(%this) {
    TutorialsCatalogClient.doCancelAllNagSchedules();
    if (isObject(%this.nagsGroup)) {
        %i = (1.0 - %this.nagsGroup.getCount());
        if ((0.0 >= %i)) {
            %nagObj = %this.nagsGroup.getObject(%i);
            %nagObj.schedule = doUpdateDisplay @ %nagObj.schedule(%nagObj.timeDelay);
            %i = (1.0 - %i);
        }
    }
};
function TutorialsCatalogClient::forceNextNag() {
    if (!($ETS::devMode)) {
        return;
    }
    if (isObject($gCurrentMainTutorial)) {
        $gCurrentMainTutorial.forceNextNag();
    }
    handleSystemMessage("msgInfoMessage", "Not currently in a tutorial.");
};
function TutorialsObject::forceNextNag(%this) {
    if (!($ETS::devMode)) {
        return;
    }
    if (isObject(%this.nagsGroup)) {
        %nagsCount = %this.nagsGroup.getCount();
        %i = 0;
        if ((%nagsCount < %i)) {
            %nagObj = %this.nagsGroup.getObject(%i);
            if (%nagObj.schedule) {
                %nagObj.doUpdateDisplay();
                return;
            }
            %i = (1.0 + %i);
        }
    }
    handleSystemMessage("msgInfoMessage", "No nags exist for the current tutorial.");
};
function TutorialsObject::doUpdateDisplay(%this, %showPanel, %restartTutorial) {
    if (%showPanel) {
        %this.doUpdateButtons();
        if ((geTutorialContainer @ " " @ $gTutorialOpenTimer $= "")) {
            %nagObj.overrideLockedOpen = 1 @ HudTabs;
            HudTabs.close();
            $gTutorialOpenTimer = schedule(250, 0, "openTutorialPaneReally", %this, %restartTutorial);
        }
    }
    %nagObj.overrideLockedOpen = 1 @ HudTabs;
    "tutorial".hideTabWithName();
};
function openTutorialPaneReally(%tutorialObj, %restartTutorial) {
    cancel($gTutorialOpenTimer);
    $gTutorialOpenTimer = "";
    alxPlay(AudioProfile_Tutorial);
    %tutorialObj.getIndex().goToTutorialByIndex(%restartTutorial);
    "tutorial".selectTabWithName();
};
function clientCmdLeaveTutorialSpace(%name) {
    %tutorialObj = %name.getChildByUserFacingName();
    TutorialsCatalogClient;
    if (!(isObject(%tutorialObj))) {
        error(getScopeName() @ " " @ "- no such tutorial:" @ " " @ %name);
        return;
    }
    if (%tutorialObj.isSecret) {
        if (isObject($gCurrentMainTutorial)) {
            $gCurrentMainTutorial.doUpdateDisplay(1, 0);
        }
        %tutorialObj.doUpdateDisplay(0, 1);
    }
    %tutorialObj.doFinishTutorial();
};
function TutorialsObject::doFinishTutorial(%this) {
    if ((%this == $gCurrentMainTutorial)) {
        TutorialsCatalogClient.doCancelAllNagSchedules();
        %this.doUpdateDisplay(0, 1);
    }
};
function NagObject::doUpdateDisplay(%this) {
    cancel(%this.schedule);
    %this.schedule = "";
    if (!(isObject(%this.getGroup().tutorial))) {
    }
    if ((TutorialsCatalogClient < %this.getGroup().tutorial.getObjectIndex())) {
    }
    if ((%this.getGroup().tutorial.getId() != $gCurrentMainTutorial)) {
        return 0.0;
    }
    if (%this.isLastNag()) {
        %this.schedule = doUpdateDisplay @ %this.schedule(%this.timeDelayForFinalNagRepeat);
    }
    %this.doUpdateButtons();
    alxPlay(AudioProfile_Tutorial);
    "tutorial".selectTabWithName();
    %this.getStepBitmapPath().setMainBitmap();
    %this.setMetaData();
};
function NagObject::isLastNag(%this) {
    return ((1.0 - %this.getGroup().getCount()) == %this.getGroup().getObjectIndex(%this));
};
function TutorialsCatalogClient::doCancelAllNagSchedules(%this) {
    %i = (1.0 - %this.getCount());
    if ((0.0 >= %i)) {
        %tutorialObj = %this.getObject(%i);
        if (isObject(%tutorialObj.nagsGroup)) {
            %j = (1.0 - %tutorialObj.nagsGroup.getCount());
            if ((0.0 >= %j)) {
                %nagObj = %tutorialObj.nagsGroup.getObject(%j);
                cancel(%nagObj.schedule);
                %nagObj.schedule = "";
                %j = (1.0 - %j);
            }
        }
        %i = (1.0 - %i);
        (0.0 >= %j);
    }
};
function leaveAllTutorialSpaces() {
    if (isObject(TutorialsCatalogClient)) {
        TutorialsCatalogClient.doCancelAllNagSchedules();
    }
    $gCurrentMainTutorial = 0;
    %nagObj.overrideLockedOpen = 1 @ HudTabs;
    HudTabs.close();
    "tutorial".hideTabWithName();
    CSControlPanel.close();
};
