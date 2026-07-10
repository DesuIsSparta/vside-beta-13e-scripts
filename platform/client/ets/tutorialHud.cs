function tutorialHud::show(%this) {
    %this.onOpen();
};
function tutorialHud::onOpen(%this) {
    HudTabs.dontCloseNextTime();
};
function tutorialHud::onClose(%this) {
};
function HudTabs::fillTutorialTab(%this) {
    %theTab = "tutorial".getTabWithName(%this);
    %theTab.titleText.delete();
    %theTab.titleText = "";
    "tutorialHud".setName(%theTab.content);
    "tutorialHud".bindClassName(%theTab.content);
    %theTab.autoHide = 0;
    %theTab.locksOpen = 1;
    exec("platform/client/ets/tutorialContainer.gui");
    $returnControl.add(%theTab);
    "tutorial".hideTabWithName(HudTabs);
    %theTab.closeButton.pushToBack(%theTab);
};
$gTutorialsFontBig = "<font:arial bold:18><color:ffffff>";
$gTutorialsFontMed = "<font:arial bold:16><color:ffffff>";
$gTutorialsFontSmall = "<font:arial:14><color:dddddd>";
function TutorialsCatalogClient::GetTutorialsRoot() {
    %csn = $gContiguousSpaceName;
    %idx = strchrpos(%csn, "_");
    if ((%idx >= 0.0)) {
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
    "TutorialsObject".bindClassName(TutorialsCatalogClient);
    "TutorialsCatalogClient".bindClassName(TutorialsCatalogClient);
    TutorialsCatalogClient.deleteMembers();
    if ((TutorialsCatalogClient::GetTutorialsRoot() $= "")) {
        return;
    }
    "*.jpg".addContentPattern(TutorialsCatalogClient);
    "*.png".addContentPattern(TutorialsCatalogClient);
    1.sortByInternalName(TutorialsCatalogClient);
    if ($StandAlone) {
        if (isObject(TutorialsCatalogServer)) {
            TutorialsCatalogServer.ValidateForStandAlone();
        }
        error(getScopeName() @ " " @ "- cannot validate tutorials for client: object TutorialsCatalogServer does not exist");
    }
    geTutorialContainer.currentTutorialObj = "";
    "tutorial".hideTabWithName(HudTabs);
    if ((TutorialsCatalogClient.getCount() > 0.0)) {
        1.goToTutorialByIndex(geTutorialContainer, 0);
    }
};
function TutorialsCatalogClient::addContentPattern(%this, %pattern) {
    %filespec = TutorialsCatalogClient::GetTutorialsRoot() @ %pattern;
    %file = findFirstFile(%filespec);
    while (!(%file $= "")) {
        %file.addContentItem(%this);
        %file = findNextFile(%filespec);
    }
};
function TutorialsCatalogClient::addContentItem(%this, %file) {
    %file = getSubStr(%file, strlen(TutorialsCatalogClient::GetTutorialsRoot()), 1000000);
    %file = strreplace(%file, "/", "\t");
    %tutorialName = getField(%file, 0);
    %stepName = getField(%file, 1);
    %stepName = stripExtension(%stepName);
    %tutorialObj = 1.getSubItemByName(%this, %tutorialName);
    %tutorialObj.tutorial = %tutorialObj;
    %tutorialObj.isSecret = (getWord(%tutorialName, 0) $= "Secret");
    if ((getWord(%stepName, 0) $= "Nag")) {
        if (!(isObject(%tutorialObj.nagsGroup))) {
            %tutorialObj.nagsGroup = safeNewScriptObject("SimGroup", "", 0);
            %tutorialObj.nagsGroup.tutorial = %tutorialObj;
        }
        %nagObj = new SimGroup("");
        "TutorialsObject".bindClassName(%nagObj);
        "NagObject".bindClassName(%nagObj);
        %stepName.setInternalName(%nagObj);
        %nagObj.timeDelay = (getWord(%stepName, 1) * 1000.0);
        %nagObj.timeDelayForFinalNagRepeat = (120.0 * 1000.0);
        %nagObj.schedule = "";
        %nagObj.add(%tutorialObj.nagsGroup);
    }
    %itemObj = 1.getSubItemByName(%tutorialObj, %stepName);
    %itemObj.isSecret = 0;
};
function TutorialsObject::getSubItemByName(%this, %name, %createIfNotFound) {
    %obj = %name.findObjectByInternalName(%this);
    if (isObject(%obj)) {
        return %obj;
    }
    if (!(%createIfNotFound)) {
        return 0;
    }
    %obj = safeNewScriptObject("SimGroup", "", 1);
    "TutorialsObject".bindClassName(%obj);
    %name.setInternalName(%obj);
    %obj.add(%this);
    return %obj;
};
function TutorialsObject::getSubItemByIndex(%this, %ndx) {
    if ((%ndx < 0.0)) {
    }
    if ((%ndx >= %this.getCount())) {
        return 0;
    }
    return %ndx.getObject(%this);
};
function TutorialsObject::getIndex(%this) {
    return %this.getObjectIndex(%this.getGroup());
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
    %n = (%this.getCount() - 1.0);
    while ((%n >= 0.0)) {
        %obj = %n.getObject(%this);
        if ((%obj.getUserFacingName() $= %name)) {
            return %obj;
        }
        %n = (%n - 1.0);
    }
    return 0;
};
function TutorialsObject::getSiblingByDelta(%this, %delta) {
    %ndx = %this.getIndex();
    %ndx = (%ndx + %delta);
    return %ndx.getSubItemByIndex(%this.getGroup());
};
function TutorialsObject::getFirstSibling(%this) {
    return 0.getSubItemByIndex(%this.getGroup());
};
function TutorialsObject::getLastSibling(%this) {
    return (%this.getGroup().getCount() - 1.0).getSubItemByIndex(%this.getGroup());
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
    %tutorialObj = %ndx.getSubItemByIndex(TutorialsCatalogClient);
    if (!(isObject(%tutorialObj))) {
        error(getScopeName() @ " " @ "- can't find tutorial:" @ " " @ %ndx);
        return;
    }
    %this.currentTutorialObj = %tutorialObj;
    if (%restartTutorial) {
    }
    if (!(isObject(%tutorialObj.currentStepObj))) {
        0.goToStepByIndex(%this);
    }
    %tutorialObj.currentStepObj.getIndex().goToStepByIndex(%this);
};
function geTutorialContainer::goToTutorialByDelta(%this, %delta, %promoteToParentDelta) {
    %cur = %this.getCurrentTutorialObj();
    if (!(isObject(%cur))) {
        error(getScopeName() @ " " @ "- no current item. Trying to step by" @ " " @ %delta);
        return;
    }
    %new = %delta.getSiblingByDelta(%cur);
    if (isObject(%new)) {
        1.goToTutorialByIndex(%this, %new.getIndex());
        if ((%delta < 0.0)) {
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
    %stepObj = %ndx.getSubItemByIndex(%tut);
    if (!(isObject(%stepObj))) {
        error(getScopeName() @ " " @ "- no such step:" @ " " @ %ndx);
        return;
    }
    %tut.currentStepObj = %stepObj;
    %stepObj.getStepBitmapPath(%this).setMainBitmap(%this);
    %stepObj.setMetaData(%this);
    %tut.doUpdateButtons(%this);
    %tut.doRestartNags();
};
function geTutorialContainer::goToStepByDelta(%this, %delta, %promoteToParentDelta) {
    %cur = %this.getCurrentStepObj();
    if (!(isObject(%cur))) {
        error(getScopeName() @ " " @ "- no current item. Trying to step by" @ " " @ %delta);
        return;
    }
    %new = %delta.getSiblingByDelta(%cur);
    if (isObject(%new)) {
        %new.getIndex().goToStepByIndex(%this);
    }
    if (%promoteToParentDelta) {
        if ((%delta < 0.0)) {
        }
        1.goToTutorialByDelta(%this, -(1.0), 1);
    }
};
function geTutorialContainer::goToFirstStep(%this) {
    %cur = %this.getCurrentStepObj();
    %cur.getFirstSibling().getIndex().goToStepByIndex(%this);
};
function geTutorialContainer::goToCurrentStep(%this) {
    %cur = %this.getCurrentStepObj();
    %cur.getIndex().goToStepByIndex(%this);
};
function geTutorialContainer::goToLastStep(%this) {
    %cur = %this.getCurrentStepObj();
    %cur.getLastSibling().getIndex().goToStepByIndex(%this);
};
function geTutorialContainer::doUpdateButtons(%this, %tutorialsObject) {
    if ((getWord(%tutorialsObject.getInternalName(), 0) $= "Nag")) {
        0.setVisible(geTutorialMLNavNext);
        0.setVisible(geTutorialMLNavPrev);
        1.setVisible(geTutorialMLReturnToTutorialButton);
        %tutorialsObject.isLastNag().setVisible(geTutorialMLRepeatTheTutorialButton);
    }
    0.setVisible(geTutorialMLReturnToTutorialButton);
    0.setVisible(geTutorialMLRepeatTheTutorialButton);
    %stepCount = %tutorialsObject.getCount();
    (%stepCount > 1.0).setVisible(geTutorialMLNavNext);
    (%stepCount > 1.0).setVisible(geTutorialMLNavPrev);
};
function geTutorialContainer::setMainBitmap(%this, %path) {
    %dragNZoom = geTutorialMainBitmap.getGroup();
    %path.setBitmap(geTutorialMainBitmap);
    geTutorialMainBitmap.fitSize();
    %w = getWord(geTutorialMainBitmap.getExtent(), 0);
    %h = getWord(geTutorialMainBitmap.getExtent(), 1);
    %h.resize(%dragNZoom, %w);
    %dragNZoom.inspectPostApply();
    %h.resize(geTutorialMainBitmap, %w);
    %dragNZoom.fitAroundParent();
    0.reposition(%dragNZoom, 0);
};
function geTutorialContainer::setMetaData(%this, %stepObj) {
    %stepName = %stepObj.getUserFacingName();
    %stepNum = (%stepObj.getIndex() + 1.0);
    %stepTTL = %stepObj.getGroup().getCount();
    %tutorialObj = %stepObj.getGroup().tutorial;
    %tutorialName = %tutorialObj.getUserFacingName();
    %labelText = "<just:right>" @ $gTutorialsFontBig @ %tutorialName;
    %labelText.setText(geTutorialMLTop);
    %labelText = $gTutorialsFontMed @ %stepName;
    if ((%stepObj.getGroup() == %stepObj.getGroup().tutorial)) {
    }
    if (%stepObj.getGroup().tutorial.isSecret) {
    }
    if ((%stepObj.getGroup().getCount() > 1.0)) {
        %labelText = %labelText @ "<just:right>" @ $gTutorialsFontSmall @ "step " @ %stepNum @ " of " @ %stepTTL;
        if ((%stepNum < %stepTTL)) {
            geTutorialMLNavNext.activeText.setText(geTutorialMLNavNext);
        }
        geTutorialMLNavNext.inactiveText.setText(geTutorialMLNavNext);
        if ((%stepNum > 1.0)) {
            geTutorialMLNavPrev.activeText.setText(geTutorialMLNavPrev);
        }
        geTutorialMLNavPrev.inactiveText.setText(geTutorialMLNavPrev);
    }
    %labelText.setText(geTutorialMLBot);
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
        if ((%stepNdx == 0.0)) {
            1.goToTutorialByDelta(geTutorialContainer, -(1.0));
        }
        geTutorialContainer.goToFirstStep();
    }
    if ((%word $= "next2x")) {
        1.goToTutorialByDelta(geTutorialContainer, 1);
    }
    if ((%word $= "prev")) {
        !(1).goToStepByDelta(geTutorialContainer, -(1.0));
    }
    if ((%word $= "next")) {
        !(1).goToStepByDelta(geTutorialContainer, 1);
    }
    if ((%word $= "repeat")) {
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
    %tutorialObj = %name.getChildByUserFacingName(TutorialsCatalogClient);
    if (!(isObject(%tutorialObj))) {
        error(getScopeName() @ " " @ "- no such tutorial:" @ " " @ %name);
        return;
    }
    %forceRestartTutorial.doStartTutorial(%tutorialObj);
};
function TutorialsObject::doStartTutorial(%this, %forceRestartTutorial) {
    if ((%this == $gCurrentMainTutorial)) {
    }
    %returningToMainTutorialFromSecretTutorial = geTutorialContainer.getCurrentTutorialObj().isSecret;
    if (!(%this.isSecret)) {
        $gCurrentMainTutorial = %this.getId();
    }
    if (!(%returningToMainTutorialFromSecretTutorial)) {
    }
    %forceRestartTutorial.doUpdateDisplay(%this, 1);
};
function TutorialsObject::doRestartNags(%this) {
    TutorialsCatalogClient.doCancelAllNagSchedules();
    if (isObject(%this.nagsGroup)) {
        %i = (%this.nagsGroup.getCount() - 1.0);
        while ((%i >= 0.0)) {
            %nagObj = %i.getObject(%this.nagsGroup);
            %nagObj.schedule = doUpdateDisplay.schedule(%nagObj, %nagObj.timeDelay);
            %i = (%i - 1.0);
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
        while ((%i < %nagsCount)) {
            %nagObj = %i.getObject(%this.nagsGroup);
            if (%nagObj.schedule) {
                %nagObj.doUpdateDisplay();
                return;
            }
            %i = (%i + 1.0);
        }
    }
    handleSystemMessage("msgInfoMessage", "No nags exist for the current tutorial.");
};
function TutorialsObject::doUpdateDisplay(%this, %showPanel, %restartTutorial) {
    if (%showPanel) {
        %this.doUpdateButtons(geTutorialContainer);
        if (($gTutorialOpenTimer $= "")) {
            HudTabs.overrideLockedOpen = 1;
            HudTabs.close();
            $gTutorialOpenTimer = schedule(250, 0, "openTutorialPaneReally", %this, %restartTutorial);
        }
    }
    HudTabs.overrideLockedOpen = 1;
    "tutorial".hideTabWithName(HudTabs);
};
function openTutorialPaneReally(%tutorialObj, %restartTutorial) {
    cancel($gTutorialOpenTimer);
    $gTutorialOpenTimer = "";
    alxPlay(AudioProfile_Tutorial);
    %restartTutorial.goToTutorialByIndex(geTutorialContainer, %tutorialObj.getIndex());
    "tutorial".selectTabWithName(HudTabs);
};
function clientCmdLeaveTutorialSpace(%name) {
    %tutorialObj = %name.getChildByUserFacingName(TutorialsCatalogClient);
    if (!(isObject(%tutorialObj))) {
        error(getScopeName() @ " " @ "- no such tutorial:" @ " " @ %name);
        return;
    }
    if (%tutorialObj.isSecret) {
        if (isObject($gCurrentMainTutorial)) {
            0.doUpdateDisplay($gCurrentMainTutorial, 1);
        }
        1.doUpdateDisplay(%tutorialObj, 0);
    }
    %tutorialObj.doFinishTutorial();
};
function TutorialsObject::doFinishTutorial(%this) {
    if (($gCurrentMainTutorial == %this)) {
        TutorialsCatalogClient.doCancelAllNagSchedules();
        1.doUpdateDisplay(%this, 0);
    }
};
function NagObject::doUpdateDisplay(%this) {
    cancel(%this.schedule);
    %this.schedule = "";
    if (!(isObject(%this.getGroup().tutorial))) {
    }
    if ((%this.getGroup().tutorial.getObjectIndex(TutorialsCatalogClient) < 0.0)) {
    }
    if (($gCurrentMainTutorial != %this.getGroup().tutorial.getId())) {
        return;
    }
    if (%this.isLastNag()) {
        %this.schedule = doUpdateDisplay.schedule(%this, %this.timeDelayForFinalNagRepeat);
    }
    %this.doUpdateButtons(geTutorialContainer);
    alxPlay(AudioProfile_Tutorial);
    "tutorial".selectTabWithName(HudTabs);
    %this.getStepBitmapPath(geTutorialContainer).setMainBitmap(geTutorialContainer);
    %this.setMetaData(geTutorialContainer);
};
function NagObject::isLastNag(%this) {
    return (%this.getObjectIndex(%this.getGroup()) == (%this.getGroup().getCount() - 1.0));
};
function TutorialsCatalogClient::doCancelAllNagSchedules(%this) {
    %i = (%this.getCount() - 1.0);
    while ((%i >= 0.0)) {
        %tutorialObj = %i.getObject(%this);
        if (isObject(%tutorialObj.nagsGroup)) {
            %j = (%tutorialObj.nagsGroup.getCount() - 1.0);
            while ((%j >= 0.0)) {
                %nagObj = %j.getObject(%tutorialObj.nagsGroup);
                cancel(%nagObj.schedule);
                %nagObj.schedule = "";
                %j = (%j - 1.0);
            }
        }
        %i = (%i - 1.0);
        (%j >= 0.0);
    }
};
function leaveAllTutorialSpaces() {
    if (isObject(TutorialsCatalogClient)) {
        TutorialsCatalogClient.doCancelAllNagSchedules();
    }
    $gCurrentMainTutorial = 0;
    HudTabs.overrideLockedOpen = 1;
    HudTabs.close();
    "tutorial".hideTabWithName(HudTabs);
    CSControlPanel.close();
};
