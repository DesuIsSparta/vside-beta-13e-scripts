function tutorialHud::show(%this)
{
    %this.onOpen();
    return ;
}
function tutorialHud::onOpen(%this)
{
    HudTabs.dontCloseNextTime();
    return ;
}
function tutorialHud::onClose(%this)
{
    return ;
}
function HudTabs::fillTutorialTab(%this)
{
    %theTab = %this.getTabWithName("tutorial");
    %theTab.titleText.delete();
    %theTab.titleText = "";
    %theTab.content.setName("tutorialHud");
    %theTab.content.bindClassName("tutorialHud");
    %theTab.autoHide = 0;
    %theTab.locksOpen = 1;
    exec("platform/client/ets/tutorialContainer.gui");
    %theTab.add($returnControl);
    HudTabs.hideTabWithName("tutorial");
    %theTab.pushToBack(%theTab.closeButton);
    return ;
}
$gTutorialsFontBig = "<font:arial bold:18><color:ffffff>";
$gTutorialsFontMed = "<font:arial bold:16><color:ffffff>";
$gTutorialsFontSmall = "<font:arial:14><color:dddddd>";
function TutorialsCatalogClient::GetTutorialsRoot()
{
    %csn = $gContiguousSpaceName;
    %idx = strchrpos(%csn, "_");
    if (%idx >= 0)
    {
        %csn = getSubStr(%csn, 0, %idx);
    }
    if (%csn $= "gw")
    {
        %world = "gateway";
    }
    else
    {
        if (%csn $= "lga")
        {
            %world = "lga";
        }
        else
        {
            if (%csn $= "nv")
            {
                %world = "lounge";
            }
            else
            {
                if (%csn $= "rj")
                {
                    %world = "raijuku";
                }
                else
                {
                    if (%csn $= "minimal")
                    {
                        %world = "dummy";
                    }
                    else
                    {
                        if (!($gContiguousSpaceName $= ""))
                        {
                            error(getScopeName() SPC "- contiguous space name \'" @ $gContiguousSpaceName @ "\' not recognized!" SPC getTrace());
                        }
                        return "";
                    }
                }
            }
        }
    }
    %ret = "projects/" @ $ETS::ProjectName @ "/worlds/" @ %world @ "/tutorials/";
    return %ret;
}
function tutorials_Initialize()
{
    safeNewScriptObject("SimGroup", "TutorialsCatalogClient", 1);
    TutorialsCatalogClient.bindClassName("TutorialsObject");
    TutorialsCatalogClient.bindClassName("TutorialsCatalogClient");
    TutorialsCatalogClient.deleteMembers();
    if (TutorialsCatalogClient::GetTutorialsRoot() $= "")
    {
        return ;
    }
    TutorialsCatalogClient.addContentPattern("*.jpg");
    TutorialsCatalogClient.addContentPattern("*.png");
    TutorialsCatalogClient.sortByInternalName(1);
    if ($StandAlone)
    {
        if (isObject(TutorialsCatalogServer))
        {
            TutorialsCatalogServer.ValidateForStandAlone();
        }
        else
        {
            error(getScopeName() SPC "- cannot validate tutorials for client: object TutorialsCatalogServer does not exist");
        }
    }
    geTutorialContainer.currentTutorialObj = "";
    HudTabs.hideTabWithName("tutorial");
    if (TutorialsCatalogClient.getCount() > 0)
    {
        geTutorialContainer.goToTutorialByIndex(0, 1);
    }
    return ;
}
function TutorialsCatalogClient::addContentPattern(%this, %pattern)
{
    %filespec = TutorialsCatalogClient::GetTutorialsRoot() @ %pattern;
    %file = findFirstFile(%filespec);
    while (!(%file $= ""))
    {
        %this.addContentItem(%file);
        %file = findNextFile(%filespec);
    }
}

function TutorialsCatalogClient::addContentItem(%this, %file)
{
    %file = getSubStr(%file, strlen(TutorialsCatalogClient::GetTutorialsRoot()), 1000000);
    %file = strreplace(%file, "/", "\t");
    %tutorialName = getField(%file, 0);
    %stepName = getField(%file, 1);
    %stepName = stripExtension(%stepName);
    %tutorialObj = %this.getSubItemByName(%tutorialName, 1);
    %tutorialObj.tutorial = %tutorialObj;
    %tutorialObj.isSecret = getWord(%tutorialName, 0) $= "Secret";
    if (getWord(%stepName, 0) $= "Nag")
    {
        if (!isObject(%tutorialObj.nagsGroup))
        {
            %tutorialObj.nagsGroup = safeNewScriptObject("SimGroup", "", 0);
            %tutorialObj.nagsGroup.tutorial = %tutorialObj;
        }
        %nagObj = new SimGroup();
        %nagObj.bindClassName("TutorialsObject");
        %nagObj.bindClassName("NagObject");
        %nagObj.setInternalName(%stepName);
        %nagObj.timeDelay = getWord(%stepName, 1) * 1000;
        %nagObj.timeDelayForFinalNagRepeat = 120 * 1000;
        %nagObj.schedule = "";
        %tutorialObj.nagsGroup.add(%nagObj);
    }
    else
    {
        %itemObj = %tutorialObj.getSubItemByName(%stepName, 1);
        %itemObj.isSecret = 0;
    }
    return ;
}
function TutorialsObject::getSubItemByName(%this, %name, %createIfNotFound)
{
    %obj = %this.findObjectByInternalName(%name);
    if (isObject(%obj))
    {
        return %obj;
    }
    if (!%createIfNotFound)
    {
        return 0;
    }
    %obj = safeNewScriptObject("SimGroup", "", 1);
    %obj.bindClassName("TutorialsObject");
    %obj.setInternalName(%name);
    %this.add(%obj);
    return %obj;
}
function TutorialsObject::getSubItemByIndex(%this, %ndx)
{
    if ((%ndx < 0) && (%ndx >= %this.getCount()))
    {
        return 0;
    }
    else
    {
        return %this.getObject(%ndx);
    }
    return ;
}
function TutorialsObject::getIndex(%this)
{
    return %this.getGroup().getObjectIndex(%this);
}
function TutorialsObject::getUserFacingName(%this)
{
    %internalName = %this.getInternalName();
    if (getWord(%internalName, 0) $= "Nag")
    {
        %userFacingName = restWords(restWords(%internalName));
    }
    else
    {
        if (getWord(%internalName, 0) $= "Secret")
        {
            %userFacingName = restWords(restWords(%internalName));
        }
        else
        {
            %userFacingName = restWords(%internalName);
        }
    }
    return %userFacingName;
}
function TutorialsObject::getChildByUserFacingName(%this, %name)
{
    %n = %this.getCount() - 1;
    while (%n >= 0)
    {
        %obj = %this.getObject(%n);
        if (%obj.getUserFacingName() $= %name)
        {
            return %obj;
        }
        %n = %n - 1;
    }
    return 0;
}
function TutorialsObject::getSiblingByDelta(%this, %delta)
{
    %ndx = %this.getIndex();
    %ndx = %ndx + %delta;
    return %this.getGroup().getSubItemByIndex(%ndx);
}
function TutorialsObject::getFirstSibling(%this)
{
    return %this.getGroup().getSubItemByIndex(0);
}
function TutorialsObject::getLastSibling(%this)
{
    return %this.getGroup().getSubItemByIndex(%this.getGroup().getCount() - 1);
}
function geTutorialContainer::getCurrentTutorialObj(%this)
{
    return %this.currentTutorialObj;
}
function geTutorialContainer::getCurrentStepObj(%this)
{
    %tut = %this.getCurrentTutorialObj();
    if (!isObject(%tut))
    {
        return 0;
    }
    else
    {
        return %tut.currentStepObj;
    }
    return ;
}
function geTutorialContainer::getStepBitmapPath(%this, %stepObj)
{
    %path = TutorialsCatalogClient::GetTutorialsRoot();
    %path = %path @ %stepObj.getGroup().tutorial.getInternalName() @ "/";
    %path = %path @ %stepObj.getInternalName();
    return %path;
}
function geTutorialContainer::goToTutorialByIndex(%this, %ndx, %restartTutorial)
{
    %tutorialObj = TutorialsCatalogClient.getSubItemByIndex(%ndx);
    if (!isObject(%tutorialObj))
    {
        error(getScopeName() SPC "- can\'t find tutorial:" SPC %ndx);
        return ;
    }
    %this.currentTutorialObj = %tutorialObj;
    if (%restartTutorial && !isObject(%tutorialObj.currentStepObj))
    {
        %this.goToStepByIndex(0);
    }
    else
    {
        %this.goToStepByIndex(%tutorialObj.currentStepObj.getIndex());
    }
    return ;
}
function geTutorialContainer::goToTutorialByDelta(%this, %delta, %promoteToParentDelta)
{
    %cur = %this.getCurrentTutorialObj();
    if (!isObject(%cur))
    {
        error(getScopeName() SPC "- no current item. Trying to step by" SPC %delta);
        return ;
    }
    %new = %cur.getSiblingByDelta(%delta);
    if (isObject(%new))
    {
        %this.goToTutorialByIndex(%new.getIndex(), 1);
        if (%delta < 0)
        {
            %this.goToLastStep();
        }
    }
    else
    {
        if (%promoteToParentDelta)
        {
            error(getScopeName() SPC "- no parents!");
        }
    }
    return ;
}
function geTutorialContainer::goToStepByIndex(%this, %ndx)
{
    %tut = %this.getCurrentTutorialObj();
    if (!isObject(%tut))
    {
        error(getScopeName() SPC "- no current tutorial. Trying to go to step" SPC %ndx);
        return ;
    }
    %stepObj = %tut.getSubItemByIndex(%ndx);
    if (!isObject(%stepObj))
    {
        error(getScopeName() SPC "- no such step:" SPC %ndx);
        return ;
    }
    %tut.currentStepObj = %stepObj;
    %this.setMainBitmap(%this.getStepBitmapPath(%stepObj));
    %this.setMetaData(%stepObj);
    %this.doUpdateButtons(%tut);
    %tut.doRestartNags();
    return ;
}
function geTutorialContainer::goToStepByDelta(%this, %delta, %promoteToParentDelta)
{
    %cur = %this.getCurrentStepObj();
    if (!isObject(%cur))
    {
        error(getScopeName() SPC "- no current item. Trying to step by" SPC %delta);
        return ;
    }
    %new = %cur.getSiblingByDelta(%delta);
    if (isObject(%new))
    {
        %this.goToStepByIndex(%new.getIndex());
    }
    else
    {
        if (%promoteToParentDelta)
        {
            %this.goToTutorialByDelta(%delta < 0 ? 1 : 1, 1);
        }
    }
    return ;
}
function geTutorialContainer::goToFirstStep(%this)
{
    %cur = %this.getCurrentStepObj();
    %this.goToStepByIndex(%cur.getFirstSibling().getIndex());
    return ;
}
function geTutorialContainer::goToCurrentStep(%this)
{
    %cur = %this.getCurrentStepObj();
    %this.goToStepByIndex(%cur.getIndex());
    return ;
}
function geTutorialContainer::goToLastStep(%this)
{
    %cur = %this.getCurrentStepObj();
    %this.goToStepByIndex(%cur.getLastSibling().getIndex());
    return ;
}
function geTutorialContainer::doUpdateButtons(%this, %tutorialsObject)
{
    if (getWord(%tutorialsObject.getInternalName(), 0) $= "Nag")
    {
        geTutorialMLNavNext.setVisible(0);
        geTutorialMLNavPrev.setVisible(0);
        geTutorialMLReturnToTutorialButton.setVisible(1);
        geTutorialMLRepeatTheTutorialButton.setVisible(%tutorialsObject.isLastNag());
    }
    else
    {
        geTutorialMLReturnToTutorialButton.setVisible(0);
        geTutorialMLRepeatTheTutorialButton.setVisible(0);
        %stepCount = %tutorialsObject.getCount();
        geTutorialMLNavNext.setVisible(%stepCount > 1);
        geTutorialMLNavPrev.setVisible(%stepCount > 1);
    }
    return ;
}
function geTutorialContainer::setMainBitmap(%this, %path)
{
    %dragNZoom = geTutorialMainBitmap.getGroup();
    geTutorialMainBitmap.setBitmap(%path);
    geTutorialMainBitmap.fitSize();
    %w = getWord(geTutorialMainBitmap.getExtent(), 0);
    %h = getWord(geTutorialMainBitmap.getExtent(), 1);
    %dragNZoom.resize(%w, %h);
    %dragNZoom.inspectPostApply();
    geTutorialMainBitmap.resize(%w, %h);
    %dragNZoom.fitAroundParent();
    %dragNZoom.reposition(0, 0);
    return ;
}
function geTutorialContainer::setMetaData(%this, %stepObj)
{
    %stepName = %stepObj.getUserFacingName();
    %stepNum = %stepObj.getIndex() + 1;
    %stepTTL = %stepObj.getGroup().getCount();
    %tutorialObj = %stepObj.getGroup().tutorial;
    %tutorialName = %tutorialObj.getUserFacingName();
    %labelText = "<just:right>" @ $gTutorialsFontBig @ %tutorialName;
    geTutorialMLTop.setText(%labelText);
    %labelText = $gTutorialsFontMed @ %stepName;
    if (((%stepObj.getGroup() == %stepObj.getGroup().tutorial) || %stepObj.getGroup().tutorial.isSecret) && (%stepObj.getGroup().getCount() > 1))
    {
        %labelText = %labelText @ "<just:right>" @ $gTutorialsFontSmall @ "step " @ %stepNum @ " of " @ %stepTTL;
        if (%stepNum < %stepTTL)
        {
            geTutorialMLNavNext.setText(geTutorialMLNavNext.activeText);
        }
        else
        {
            geTutorialMLNavNext.setText(geTutorialMLNavNext.inactiveText);
        }
        if (%stepNum > 1)
        {
            geTutorialMLNavPrev.setText(geTutorialMLNavPrev.activeText);
        }
        else
        {
            geTutorialMLNavPrev.setText(geTutorialMLNavPrev.inactiveText);
        }
    }
    geTutorialMLBot.setText(%labelText);
    return ;
}
function geTutorialMLNavNext::onURL(%this, %url)
{
    %stepObj = geTutorialContainer.getCurrentStepObj();
    if (!isObject(%stepObj))
    {
        error(getScopeName() SPC "- no current step");
        return ;
    }
    %restartNags = 1;
    %stepNdx = %stepObj.getIndex();
    if (firstWord(%url) $= "gamelink")
    {
        %word = restWords(%url);
    }
    else
    {
        %word = %url;
    }
    if (%word $= "prev2x")
    {
        if (%stepNdx == 0)
        {
            geTutorialContainer.goToTutorialByDelta(-1, 1);
        }
        geTutorialContainer.goToFirstStep();
    }
    else
    {
        if (%word $= "next2x")
        {
            geTutorialContainer.goToTutorialByDelta(1, 1);
        }
        else
        {
            if (%word $= "prev")
            {
                geTutorialContainer.goToStepByDelta(-1, !1);
            }
            else
            {
                if (%word $= "next")
                {
                    geTutorialContainer.goToStepByDelta(1, !1);
                }
                else
                {
                    if (%word $= "repeat")
                    {
                        commandToServer('respawnPlayerAtTutorialBeginning', $gCurrentMainTutorial.getUserFacingName());
                    }
                    else
                    {
                        if (%word $= "return")
                        {
                            geTutorialContainer.goToCurrentStep();
                        }
                    }
                }
            }
        }
    }
    return ;
}
function geTutorialMLNavPrev::onURL(%this, %url)
{
    geTutorialMLNavNext::onURL(%this, %url);
    return ;
}
function geTutorialMLRepeatTheTutorialButton::onURL(%this, %url)
{
    geTutorialMLNavNext::onURL(%this, %url);
    return ;
}
function geTutorialMLReturnToTutorialButton::onURL(%this, %url)
{
    geTutorialMLNavNext::onURL(%this, %url);
    return ;
}
$gTutorialOpenTimer = "";
$gCurrentMainTutorial = 0;
function clientCmdEnterTutorialSpace(%name, %forceRestartTutorial)
{
    %tutorialObj = TutorialsCatalogClient.getChildByUserFacingName(%name);
    if (!isObject(%tutorialObj))
    {
        error(getScopeName() SPC "- no such tutorial:" SPC %name);
        return ;
    }
    %tutorialObj.doStartTutorial(%forceRestartTutorial);
    return ;
}
function TutorialsObject::doStartTutorial(%this, %forceRestartTutorial)
{
    %returningToMainTutorialFromSecretTutorial = (%this == $gCurrentMainTutorial) && geTutorialContainer.getCurrentTutorialObj().isSecret;
    if (!%this.isSecret)
    {
        $gCurrentMainTutorial = %this.getId();
    }
    %this.doUpdateDisplay(1, !%returningToMainTutorialFromSecretTutorial || %forceRestartTutorial);
    return ;
}
function TutorialsObject::doRestartNags(%this)
{
    TutorialsCatalogClient.doCancelAllNagSchedules();
    if (isObject(%this.nagsGroup))
    {
        %i = %this.nagsGroup.getCount() - 1;
        while (%i >= 0)
        {
            %nagObj = %this.nagsGroup.getObject(%i);
            %nagObj.schedule = %nagObj.schedule(%nagObj.timeDelay, doUpdateDisplay);
            %i = %i - 1;
        }
    }
}

function TutorialsCatalogClient::forceNextNag()
{
    if (!$ETS::devMode)
    {
        return ;
    }
    if (isObject($gCurrentMainTutorial))
    {
        $gCurrentMainTutorial.forceNextNag();
    }
    else
    {
        handleSystemMessage("msgInfoMessage", "Not currently in a tutorial.");
    }
    return ;
}
function TutorialsObject::forceNextNag(%this)
{
    if (!$ETS::devMode)
    {
        return ;
    }
    if (isObject(%this.nagsGroup))
    {
        %nagsCount = %this.nagsGroup.getCount();
        %i = 0;
        while (%i < %nagsCount)
        {
            %nagObj = %this.nagsGroup.getObject(%i);
            if (%nagObj.schedule)
            {
                %nagObj.doUpdateDisplay();
                return ;
            }
            %i = %i + 1;
        }
    }
    handleSystemMessage("msgInfoMessage", "No nags exist for the current tutorial.");
    return ;
}
function TutorialsObject::doUpdateDisplay(%this, %showPanel, %restartTutorial)
{
    if (%showPanel)
    {
        geTutorialContainer.doUpdateButtons(%this);
        if ($gTutorialOpenTimer $= "")
        {
            HudTabs.overrideLockedOpen = 1;
            HudTabs.close();
            $gTutorialOpenTimer = schedule(250, 0, "openTutorialPaneReally", %this, %restartTutorial);
        }
    }
    else
    {
        HudTabs.overrideLockedOpen = 1;
        HudTabs.hideTabWithName("tutorial");
    }
    return ;
}
function openTutorialPaneReally(%tutorialObj, %restartTutorial)
{
    cancel($gTutorialOpenTimer);
    $gTutorialOpenTimer = "";
    alxPlay(AudioProfile_Tutorial);
    geTutorialContainer.goToTutorialByIndex(%tutorialObj.getIndex(), %restartTutorial);
    HudTabs.selectTabWithName("tutorial");
    return ;
}
function clientCmdLeaveTutorialSpace(%name)
{
    %tutorialObj = TutorialsCatalogClient.getChildByUserFacingName(%name);
    if (!isObject(%tutorialObj))
    {
        error(getScopeName() SPC "- no such tutorial:" SPC %name);
        return ;
    }
    if (%tutorialObj.isSecret)
    {
        if (isObject($gCurrentMainTutorial))
        {
            $gCurrentMainTutorial.doUpdateDisplay(1, 0);
        }
        else
        {
            %tutorialObj.doUpdateDisplay(0, 1);
        }
    }
    else
    {
        %tutorialObj.doFinishTutorial();
    }
    return ;
}
function TutorialsObject::doFinishTutorial(%this)
{
    if ($gCurrentMainTutorial == %this)
    {
        TutorialsCatalogClient.doCancelAllNagSchedules();
        %this.doUpdateDisplay(0, 1);
    }
    return ;
}
function NagObject::doUpdateDisplay(%this)
{
    cancel(%this.schedule);
    %this.schedule = "";
    if ((!isObject(%this.getGroup().tutorial) || (TutorialsCatalogClient.getObjectIndex(%this.getGroup().tutorial) < 0)) || ($gCurrentMainTutorial != %this.getGroup().tutorial.getId()))
    {
        return ;
    }
    if (%this.isLastNag())
    {
        %this.schedule = %this.schedule(%this.timeDelayForFinalNagRepeat, doUpdateDisplay);
    }
    geTutorialContainer.doUpdateButtons(%this);
    alxPlay(AudioProfile_Tutorial);
    HudTabs.selectTabWithName("tutorial");
    geTutorialContainer.setMainBitmap(geTutorialContainer.getStepBitmapPath(%this));
    geTutorialContainer.setMetaData(%this);
    return ;
}
function NagObject::isLastNag(%this)
{
    return %this.getGroup().getObjectIndex(%this) == (%this.getGroup().getCount() - 1);
}
function TutorialsCatalogClient::doCancelAllNagSchedules(%this)
{
    %i = %this.getCount() - 1;
    while (%i >= 0)
    {
        %tutorialObj = %this.getObject(%i);
        if (isObject(%tutorialObj.nagsGroup))
        {
            %j = %tutorialObj.nagsGroup.getCount() - 1;
            while (%j >= 0)
            {
                %nagObj = %tutorialObj.nagsGroup.getObject(%j);
                cancel(%nagObj.schedule);
                %nagObj.schedule = "";
                %j = %j - 1;
            }
        }
        %i = %i - 1;
    }
}

function leaveAllTutorialSpaces()
{
    if (isObject(TutorialsCatalogClient))
    {
        TutorialsCatalogClient.doCancelAllNagSchedules();
    }
    $gCurrentMainTutorial = 0;
    HudTabs.overrideLockedOpen = 1;
    HudTabs.close();
    HudTabs.hideTabWithName("tutorial");
    CSControlPanel.close();
    return ;
}
