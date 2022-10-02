function HudTabs::setup()
{
    if (!isObject(HudTabs))
    {
        new ScriptObject(HudTabs);
        if (isObject(MissionCleanup))
        {
            MissionCleanup.add(HudTabs);
        }
        HudTabs.Initialize(HudContainer, "46 39", "", "", "vertical");
        HudTabs.newTab("music", "platform/client/buttons/hud_music", "music & videos");
        HudTabs.newTab("affinity", "platform/client/buttons/hud_affinity", "");
        HudTabs.newTab("scores", "platform/client/buttons/hud_scores", "");
        HudTabs.newTab("word", "platform/client/buttons/hud_word", "");
        HudTabs.newTab("tutorial", "platform/client/buttons/hud_tutorials", "");
        HudTabs.fillTabs();
        HudTabs.close();
        HudTabs.closeTimer = 0;
    }
    return ;
}
function HudTabs::newTab(%this, %name, %bitmapName, %title)
{
    if (!isDefined("%title"))
    {
        %title = "";
    }
    %tab = Parent::newTab(%this, %name, %bitmapName);
    %tab.title = %title;
    %tab.pulsar = AnimCtrl::newAnimCtrl("0 0", "54 43");
    %tab.pulsar.setDelay(60);
    %tab.pulsar.addFrame("platform/client/ui/pulse/bracket_00.png");
    %tab.pulsar.addFrame("platform/client/ui/pulse/bracket_01.png");
    %tab.pulsar.addFrame("platform/client/ui/pulse/bracket_02.png");
    %tab.pulsar.addFrame("platform/client/ui/pulse/bracket_03.png");
    %tab.pulsar.addFrame("platform/client/ui/pulse/bracket_04.png");
    %tab.pulsar.addFrame("platform/client/ui/pulse/bracket_05.png");
    %tab.pulsar.addFrame("platform/client/ui/pulse/bracket_06.png");
    %tab.pulsar.addFrame("platform/client/ui/pulse/bracket_07.png");
    %tab.pulsar.addFrame("platform/client/ui/pulse/bracket_08.png");
    %tab.pulsar.addFrame("platform/client/ui/pulse/bracket_09.png");
    %tab.pulsar.addFrame("platform/client/ui/pulse/bracket_10.png");
    %tab.pulsar.addFrame("platform/client/ui/pulse/bracket_11.png");
    %tab.pulsar.addFrame("platform/client/ui/pulse/bracket_12.png");
    %tab.pulsar.addFrame("platform/client/ui/pulse/bracket_13.png");
    %tab.pulsar.setProfile(ETSNonModalProfile);
    %tab.pulsar.setVisible(0);
    %pos = %tab.button.getPosition();
    %xPos = getWord(%pos, 0) - 4;
    %ypos = getWord(%pos, 1) - 2;
    %tab.pulsar.reposition(%xPos, %ypos);
    %tab.pulseTimer = 0;
    return ;
}
function HudTabs::update(%this)
{
    Parent::update(%this);
    %numVisibleButtons = 0;
    %padding = %this.getPadding();
    %n = 0;
    while (%n < %this.numTabs)
    {
        if (%this.buttons[%n].isVisible())
        {
            %height = %height + (getWord(%this.buttons[%n].getExtent(), 1) + %padding);
        }
        %n = %n + 1;
    }
    %height = %height - %padding;
    %width = getWord(HudTabsCollapsed.getExtent(), 0);
    HudTabsCollapsed.resize(%width, %height);
    return ;
}
function HudTabs::pulseTab(%this, %tabObject)
{
    if (!isObject(%tabObject) && !isObject(%tabObject.pulsar))
    {
        error(getScopeName() @ "->invalid tab object or tab without pulsar object passed! returning!");
        return ;
    }
    %this.pausePulseOnAllTabs();
    HudContainer.add(%tabObject.pulsar);
    HudContainer.pushToBack(%tabObject.pulsar);
    %tabObject.pulsar.setVisible(1);
    %tabObject.pulsar.start();
    cancel(%tabObject.pulseTimer);
    %tabObject.pulseTimer = %this.schedule(4000, "pausePulseOnTab", %tabObject);
    return ;
}
function HudTabs::pulseTabWithName(%this, %tabName)
{
    %tabObject = 0;
    if (!(%tabName $= ""))
    {
        %tabObject = %this.getTabWithName(%tabName);
    }
    %this.pulseTab(%tabObject);
    return ;
}
function HudTabs::pausePulseOnTab(%this, %tabObject)
{
    cancel(%tabObject.pulseTimer);
    %tabObject.pulsar.stop();
    %tabObject.pulsar.setCurrentFrame(0);
    return ;
}
function HudTabs::pausePulseOnAllTabs(%this)
{
    %i = 0;
    while (%i < %this.numTabs)
    {
        %this.pausePulseOnTab(%this.tabs[%i]);
        %i = %i + 1;
    }
}

function HudTabs::stopPulseOnTab(%this, %tabObject)
{
    cancel(%tabObject.pulseTimer);
    %tabObject.pulsar.stop();
    %tabObject.pulsar.setVisible(0);
    return ;
}
function HudTabs::getPadding(%this)
{
    return 0;
}
function HudTabs::close(%this)
{
    %tab = %this.getCurrentTab();
    if (isObject(%tab) && isObject(%tab.content))
    {
        %tab.content.onClose();
    }
    HudTabsCollapsed.setVisible(1);
    %this.selectTabAtIndex(-1);
    return ;
}
function HudTabs::onHiddenButton(%this)
{
    Parent::onHiddenButton(%this);
    %this.close();
    return ;
}
function HudTabs::hideOrShowTab(%this, %tabObject, %show)
{
    if (%this.currentTabIsLockedOpen())
    {
        Parent::hideOrShowTab(%this, %tabObject, %show);
        return ;
    }
    if (%this.getCurrentTab() == %tabObject)
    {
        %this.close();
    }
    Parent::hideOrShowTab(%this, %tabObject, %show);
    if (isObject(%tabObject.pulsar))
    {
        if (%tabObject.pulsar.isVisible())
        {
            %this.stopPulseOnTab(%tabObject);
        }
    }
    return ;
}
function HudTabs::setTabAtIndexVisible(%this, %tabIndex, %visible)
{
    %tab = %this.tabs[%tabIndex];
    if (!isObject(%tab))
    {
        return ;
    }
    %posX = getWord(%this.tabPosition, 0);
    %posY = getWord(%this.tabPosition, 1);
    if (%visible)
    {
        %tab.setVisible(1);
        %tab.setTrgPosition(%posX, %posY);
    }
    else
    {
        %tab.setTrgPosition(%posX - getWord(%tab.getExtent(), 0), %posY);
    }
    return ;
}
function HudTabs::autoHide(%this)
{
    %currentTab = %this.getCurrentTab();
    %tabName = %currentTab $= "" ? "" : %currentTab;
    if (!$UserPref::HudTabs::AutoClose[%tabName])
    {
        return ;
    }
    if (%this.container.cursorInControl())
    {
        %this.autoHideSchedule(2000);
        return ;
    }
    %this.close();
    if (%currentTab.pulsar.isVisible())
    {
        %this.pausePulseOnTab(%currentTab);
    }
    return ;
}
function HudTabs::autoHideSchedule(%this, %ms)
{
    cancel(%this.closeTimer);
    %this.closeTimer = %this.schedule(%ms, "autoHide");
    return ;
}
function HudTabs::dontCloseNextTime(%this)
{
    cancel(%this.closeTimer);
    %this.closeTimer = 0;
    return ;
}
function HudTabs::tabSelected(%this, %tab)
{
    if (isObject(%tab))
    {
        HudTabsCollapsed.setVisible(0);
        %this.autoHideSchedule($Pref::ETS::HudTabs::timeout);
        if (isObject(%tab.pulsar))
        {
            %this.stopPulseOnTab(%tab);
        }
    }
    if (!%tab.autoHide)
    {
        %this.dontCloseNextTime();
    }
    %prevTab = %this.getPreviousTab();
    if (%prevTab != %tab)
    {
        if (isObject(%prevTab) && isObject(%prevTab.content))
        {
            %prevTab.content.onClose();
            if (%prevTab.pulsar.isVisible())
            {
                %this.pausePulseOnTab(%prevTab);
            }
        }
    }
    return ;
}
function HudTabs::getInitialButtonOffset(%this)
{
    return "46 0";
}
function HudTabs::CreateTab(%this, %name)
{
    %tab = Parent::CreateTab(%this, %name);
    %tab.sluggishness = 0.5;
    %tab.reposition(getWord(%this.tabPosition, 0) - getWord(%tab.getExtent(), 0), getWord(%this.tabPosition, 1));
    return %tab;
}
function HudTabs::fillTabs(%this)
{
    %i = 0;
    while (%i < HudTabs.numTabs)
    {
        %tab = HudTabs.tabs[%i];
        %tab.setProfile(GuiDefaultProfile);
        %tab.clear();
        %tab.button.tooltip = %tab.name;
        %title = %tab.title $= "" ? %tab : %tab;
        %titleText = new GuiMLTextCtrl()
        {
            profile = "ETSHudHeadingProfile";
            horizSizing = "right";
            vertSizing = "bottom";
            position = "47 5";
            extent = "170 20";
            minExtent = "1 1";
            sluggishness = -1;
            visible = 1;
            maxLength = 255;
            text = %title;
        };
        %tab.titleText = %titleText;
        %tab.add(%titleText);
        %closeButton = new GuiBitmapButtonCtrl()
        {
            profile = "GuiDefaultProfile";
            horizSizing = "left";
            vertSizing = "bottom";
            position = "270 5";
            extent = "16 16";
            minExtent = "1 1";
            sluggishness = -1;
            visible = 1;
            bitmap = "platform/client/buttons/gray_close";
            command = "HudTabs.overrideLockedOpen = true; HudTabs.close();";
        };
        %tab.closeButton = %closeButton;
        %tab.add(%closeButton);
        %content = new GuiControl()
        {
            profile = "GuiDefaultProfile";
            horizSizing = "right";
            vertSizing = "bottom";
            position = "47 35";
            extent = "235 180";
            minExtent = "1 1";
            sluggishness = -1;
            visible = 1;
        };
        %tab.content = %content;
        %tab.add(%content);
        %i = %i + 1;
    }
    %this.filledPrivateSpaceTab = 0;
    HudTabs.fillMusicTab();
    HudTabs.fillAffinityTab();
    HudTabs.fillScoresTab();
    HudTabs.fillWordTab();
    HudTabs.fillTutorialTab();
    return ;
}
function HudTabs::fillMusicTab(%this)
{
    %theTab = %this.getTabWithName("music");
    %theTab.content.setName("MusicHud");
    %theTab.content.bindClassName("MusicHud");
    %theTab.toggleSoundTxt = new GuiMLTextCtrl(MusicTabToggleSoundTxt)
    {
        profile = "ETSShadowTextProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "227 11";
        extent = "28 18";
        text = "(on)";
    };
    %theTab.toggleSoundTxt.updateText();
    %theTab.add(%theTab.toggleSoundTxt);
    MusicHud.station = "";
    new GuiControl(MusicHudBasicView)
    {
        profile = "GuiDefaultProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "0 0";
        extent = MusicHud.getExtent();
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
    }.add(new GuiControl(MusicHudBasicView)
    {
        profile = "GuiDefaultProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "0 0";
        extent = MusicHud.getExtent();
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
    });
    %ratingLabel = new GuiMLTextCtrl()
    {
        profile = "MusicRatingTextProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "0 20";
        extent = "150 17";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        text = "";
        lineSpacing = -2;
        allowColorChars = 1;
    };
    MusicHud.ratingControl = new GuiControl()
    {
        profile = "GuiDefaultProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "2 115";
        extent = "150 44";
        minExtent = "1 1";
        visible = 1;
        label = %ratingLabel;
    };
    MusicHud.ratingControl.bindClassName("RatingControl");
    MusicHud.ratingControl.bindClassName("MusicRatingControl");
    MusicHud.ratingControl.Initialize(5, "19 19", "platform/client/buttons/star");
    MusicHud.ratingControl.add(%ratingLabel);
    MusicHudBasicView.add(MusicHud.ratingControl);
    new GuiControl(MusicHudEditView)
    {
        profile = "GuiDefaultProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "0 0";
        extent = MusicHud.getExtent();
        minExtent = "1 1";
        sluggishness = -1;
        visible = 0;
    }.add(new GuiControl(MusicHudEditView)
    {
        profile = "GuiDefaultProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "0 0";
        extent = MusicHud.getExtent();
        minExtent = "1 1";
        sluggishness = -1;
        visible = 0;
    });
    return ;
}
function MusicTabToggleSoundTxt::updateText(%this)
{
    if ($UserPref::Audio::mute)
    {
        %soundTxt = "(off)";
    }
    else
    {
        %soundTxt = "(on)";
    }
    %this.setText(%soundTxt);
    return ;
}
function MusicRatingControl::updatePosition(%this)
{
    %musicTextBottomY = (getWord(MusicText.getExtent(), 1) + getWord(MusicTextScroll.getPosition(), 1)) + getWord(MusicText.getPosition(), 1);
    %mTScrollBottomY = getWord(MusicTextScroll.getExtent(), 1) + getWord(MusicTextScroll.getPosition(), 1);
    %padding = 10;
    %this.reposition(getWord(%this.getPosition, 0), mMin(%musicTextBottomY, %mTScrollBottomY) + %padding);
    return ;
}
function HudTabs::fillAffinityTab(%this)
{
    %theTab = %this.getTabWithName("affinity");
    if (!showPlayerInfoPopup())
    {
        %theTab.button.setVisible(0);
    }
    %theTab.content.setName("InfoPopupDlg");
    %theTab.content.bindClassName("InfoPopupDlg");
    new GuiMLTextCtrl(InfoPopupTagsText)
    {
        profile = "InfoWindowTextProfile";
        horizSizing = "width";
        vertSizing = "bottom";
        position = "0 0";
        extent = "220 45";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        lineSpacing = 1;
        allowColorChars = 0;
        maxChars = -1;
    };
    InfoPopupTagsScroll.add(InfoPopupTagsText);
    InfoPopupDlg.add(InfoPopupTagsScroll);
    InfoPopupDlg.init();
    return ;
}
function HudTabs::fillScoresTab(%this)
{
    %theTab = %this.getTabWithName("scores");
    %theTab.content.setName("HudScoresContent");
    %theTab.content.bindClassName("HudScoresContent");
    %ypos = 2;
    %fieldx = 110;
    HudScoresContent.respektLevelLabel = new GuiMLTextCtrl()
    {
        profile = "InfoWindowTextProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = %fieldx SPC %ypos;
        extent = "134 18";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        text = "1 - Freshman";
        maxLength = 64;
    };
    HudScoresContent.add(HudScoresContent.respektLevelLabel);
    if (!isObject(HudScoresPBController))
    {
        new ScriptObject(HudScoresPBController);
        if (isObject(MissionCleanup))
        {
            MissionCleanup.add(HudScoresPBController);
        }
    }
    HudScoresContent.respektBarContainer = new GuiControl()
    {
        profile = "ETSRespektLevelPBProfile";
        horizSizing = "center";
        vertSizing = "top";
        position = %fieldx - 2 SPC %ypos + 2;
        extent = "125 14";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
    };
    HudScoresContent.respektBarHolder = new GuiControl()
    {
        profile = "BlankProfile";
        horizSizing = "center";
        vertSizing = "top";
        position = "1 1";
        extent = "114 13";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
    };
    HudScoresContent.respektBarContainer.add(HudScoresContent.respektBarHolder);
    HudScoresContent.add(HudScoresContent.respektBarContainer);
    HudScoresPBController.Initialize(HudScoresContent.respektBarHolder, "", "platform/client/ui/respektprogress_fill", "", "");
    HudScoresContent.respektScoreLabel = new GuiMLTextCtrl()
    {
        profile = "InfoWindowTextProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = %fieldx SPC %ypos;
        extent = "100 18";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        text = "";
        maxLength = 64;
    };
    HudScoresContent.add(HudScoresContent.respektScoreLabel);
    if (0)
    {
        HudScoresContent.respektRankLabel = new GuiMLTextCtrl()
        {
            profile = "InfoWindowTextProfile";
            horizSizing = "right";
            vertSizing = "bottom";
            position = %fieldx SPC %ypos;
            extent = "68 18";
            minExtent = "1 1";
            sluggishness = -1;
            visible = 1;
            text = "#1";
            maxLength = 64;
        };
        HudScoresContent.add(HudScoresContent.respektRankLabel);
    }
    HudScoresContent.collectionsScroll = new GuiScrollCtrl()
    {
        profile = "ETSInviteMessageScrollProfile";
        horizSizing = "width";
        vertSizing = "height";
        position = 1 SPC %ypos = %ypos + 20;
        extent = "233 99";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        willFirstRespond = 1;
        hScrollBar = "alwaysOff";
        vScrollBar = "dynamic";
        constantThumbHeight = 1;
        childMargin = "2 2";
        saneDrag = 1;
        scrollMultiplier = 1;
        stickyBottom = 0;
    };
    HudScoresContent.collectionsList = new GuiMLTextCtrl()
    {
        profile = "InfoWindowTextListProfile";
        horizSizing = "width";
        vertSizing = "bottom";
        position = "0 0";
        extent = "218 99";
        minExtent = "80 60";
        sluggishness = -1;
        visible = 1;
        command = "";
        altCommand = "";
        enumerate = 1;
        resizeCell = 1;
        fitParentWidth = 1;
        clipColumnText = 1;
    };
    HudScoresContent.collectionsScroll.add(HudScoresContent.collectionsList);
    HudScoresContent.add(HudScoresContent.collectionsScroll);
    if (!isObject(HudScoresContent.collectionsSet))
    {
        HudScoresContent.collectionsSet = new SimSet(ScoresHudCollectionsSet);
    }
    HudScoresContent.previousRespektPoints = 0;
    HudScoresContent.setRespektPoints(0, 0);
    return ;
}
function HudScoresContent::setRespektPoints(%this, %points, %notify)
{
    %this.respektScoreLabel.setText(%points);
    %level = respektScoreToLevel(%points);
    %levelName = respektLevelToNameWithoutArticle(%level);
    %this.respektLevelLabel.setText(%level @ " - " @ %levelName);
    HudScoresPBController.setValue(1 - respektPercentToNextLevel(%points));
    %levelPrev = respektScoreToLevel(%this.previousRespektPoints);
    if ((%level != %levelPrev) && (%this.previousRespektPoints != 0))
    {
        if (%level > %levelPrev)
        {
            alxPlay(AudioRespektLevelGained);
        }
        if (%level == 1)
        {
            %code = "LEVELCHANGE1";
        }
        else
        {
            if (%level == 2)
            {
                %code = "LEVELCHANGE2";
            }
            else
            {
                %code = "LEVELCHANGE";
            }
        }
        schedule(5000, 0, "respektHandle", "", %points, %points - %this.previousRespektPoints, %code, 0, 1);
        HudTabs.schedule(5100, "pulseTabWithName", "scores");
    }
    if (%notify && (%points != %this.previousRespektPoints))
    {
        HudTabs.pulseTabWithName("scores");
    }
    %this.previousRespektPoints = %points;
    return ;
}
function HudScoresContent::setRespektRank(%this, %rank)
{
    if (%rank $= "")
    {
        %text = "(unknown)";
    }
    else
    {
        %text = "#" @ %rank;
    }
    if (isObject(%this.respektRankLabel))
    {
        %this.respektRankLabel.setText(%text);
    }
    if (%rank != %this.previousRespektRank)
    {
        HudTabs.pulseTabWithName("scores");
    }
    %this.previousRespektRank = %rank;
    return ;
}
function HudScoresContent::clearCollections(%this)
{
    %count = %this.collectionsSet.getCount();
    %n = %count - 1;
    while (%n >= 0)
    {
        %collection = %this.collectionsSet.getObject(%n);
        %this.collectionsSet.remove(%collection);
        %collection.delete();
        %n = %n - 1;
    }
    %this.refreshCollections();
    return ;
}
function clientCmdSetCollectionStatus(%name, %sofar, %total)
{
    HudScoresContent.setCollectionStatus(%name, %sofar, %total);
    return ;
}
function HudScoresContent::setCollectionStatus(%this, %name, %sofar, %total)
{
    %ourCopy = %this.getCollectionObject(%name);
    if (!isObject(%ourCopy))
    {
        if ((%total == 0) && (%sofar == 0))
        {
            return ;
        }
        %ourCopy = new ScriptObject();
        if (isObject(MissionCleanup))
        {
            MissionCleanup.add(%ourCopy);
        }
        %this.collectionsSet.add(%ourCopy);
    }
    if ((%total == 0) && (%sofar == 0))
    {
        %this.collectionsSet.remove(%ourCopy);
        %ourCopy.delete();
    }
    else
    {
        %ourCopy.sofar = %sofar;
        %ourCopy.total = %total;
    }
    %this.refreshCollections();
    return ;
}
function HudScoresContent::getCollectionObject(%this, %name)
{
    %n = %this.collectionsSet.getCount() - 1;
    while (%n >= 0)
    {
        %cur = %this.collectionsSet.getObject(%n);
        if (%name $= %cur.name)
        {
            return %cur;
        }
        %n = %n - 1;
    }
    return -1;
}
function HudScoresContent::refreshCollections(%this)
{
    %this.collectionsList.setText("");
    %count = %this.collectionsSet.getCount();
    %stringToSort = "";
    %completed = "";
    %i = %count - 1;
    while (%i >= 0)
    {
        %collection = %this.collectionsSet.getObject(%i);
        %ratio = %collection.sofar / %collection.total;
        if (%ratio != 1)
        {
            %append = formatFloat("%1.3f", %ratio) TAB %collection.getId();
            if (!(%stringToSort $= ""))
            {
                %stringToSort = %stringToSort SPC %append;
            }
            else
            {
                %stringToSort = %append;
            }
        }
        else
        {
            if (!(%completed $= ""))
            {
                %completed = %completed SPC %collection.getId();
            }
            else
            {
                %completed = %collection.getId();
            }
        }
        %i = %i - 1;
    }
    %stringToSort = SortWords(%stringToSort);
    %count = getFieldCount(%stringToSort);
    %this.collectionsList.addText("<spush><just:left><b> In Progress:<spop><br>", 0);
    %i = %count - 1;
    while (%i > 0)
    {
        %collection = getWord(getField(%stringToSort, %i), 0);
        %this.collectionsList.addText("<spush><just:left>   " @ %collection.name SPC "<just:right>(" @ %collection.sofar @ "/" @ %collection.total @ ")<spop><br>", 0);
        %i = %i - 1;
    }
    if (%completed $= "")
    {
        return ;
    }
    %this.collectionsList.addText("<spush><just:left><b> Completed:<spop><br>", 0);
    %count = getWordCount(%completed);
    %i = 0;
    while (%i < %count)
    {
        %collection = getWord(%completed, %i);
        %this.collectionsList.addText("<spush><just:left>   " @ %collection.name SPC "<just:right>(" @ %collection.sofar @ "/" @ %collection.total @ ")<spop><br>", 0);
        %i = %i + 1;
    }
    if (%this.collectionsList.isAwake())
    {
        %this.collectionsList.forceReflow();
    }
    return ;
}
function HudScoresContent::onClose(%this)
{
    return ;
}
function HudTabs::fillWordTab(%this)
{
    %theTab = %this.getTabWithName("word");
    %theTab.content.setName("SystemMessageDialog");
    %theTab.content.bindClassName("SystemMessageDialog");
    new GuiScrollCtrl(SystemMessageScrollCtrl)
    {
        profile = "ETSScrollProfile";
        horizSizing = "width";
        vertSizing = "height";
        position = "3 0";
        extent = "229 180";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        willFirstRespond = 1;
        hScrollBar = "alwaysOff";
        vScrollBar = "dynamic";
        constantThumbHeight = 1;
        childMargin = "0 0";
    }.add(new GuiScrollCtrl(SystemMessageScrollCtrl)
    {
        profile = "ETSScrollProfile";
        horizSizing = "width";
        vertSizing = "height";
        position = "3 0";
        extent = "229 180";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        willFirstRespond = 1;
        hScrollBar = "alwaysOff";
        vScrollBar = "dynamic";
        constantThumbHeight = 1;
        childMargin = "0 0";
    });
    SystemMessageTextCtrl.setText(SystemMessageTextCtrl.DefaultMessage);
    return ;
}
function HudTabs::fillPrivateSpaceTab(%this)
{
    HudTabs.hideTabWithName("private space");
    %theTab = %this.getTabWithName("private space");
    %theTab.content.setName("PrivSpaceHud");
    %theTab.content.bindClassName("PrivSpaceHud");
    PrivSpaceHud.toggleOP = new GuiMLTextCtrl(PrivSpaceHudToggleOP)
    {
        profile = "ETSShadowTextProfile";
        extent = "68 18";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "187 11";
        extent = "80 25";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 0;
        text = "";
        maxLength = 64;
    };
    %theTab.add(PrivSpaceHud.toggleOP);
    OPSpaceHud.setup();
    NonOPSpaceHud.setup();
    PrivSpaceHud.hideOP();
    new GuiMLTextCtrl(SpaceSurfText)
    {
        profile = "InfoWindowTextProfile";
        horizSizing = "width";
        vertSizing = "bottom";
        position = "207 199";
        extent = "50 18";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 0;
        text = "<a:spaceP> << </a><a:spaceN> >> </a>";
        lineSpacing = 1;
        allowColorChars = 0;
        maxChars = -1;
    };
    %theTab.add(SpaceSurfText);
    new GuiTextEditCtrl(SpaceSurfTE)
    {
        profile = "InfoWindowTextEditProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "261 200";
        extent = "25 18";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 0;
        altCommand = "SpaceSurfTE.onEnter();";
        maxLength = 32;
        historySize = 1;
        password = 0;
        tabComplete = 0;
        sinkAllKeyEvents = 0;
    };
    %theTab.add(SpaceSurfTE);
    %this.filledPrivateSpaceTab = 1;
    return ;
}
function PrivSpaceHud::onClose(%this)
{
    OPSpaceHud.descriptionChanged();
    return ;
}
function PrivSpaceHudToggleOP::onURL(%this, %url)
{
    if (getWord(%url, 0) $= "OPon")
    {
        PrivSpaceHud.showOP();
    }
    else
    {
        if (getWord(%url, 0) $= "OPoff")
        {
            PrivSpaceHud.hideOP();
        }
        else
        {
            error("Url in PrivSpaceHud.toggleOP is broken.<-" @ getScopeName());
        }
    }
    return ;
}
function PrivSpaceHud::enableOPlink(%this)
{
    %this.toggleOP.setVisible(1);
    return ;
}
function PrivSpaceHud::disableOPlink(%this)
{
    %this.toggleOP.setVisible(0);
    return ;
}
function PrivSpaceHud::showOP(%this)
{
    OPSpaceHud.setVisible(1);
    NonOPSpaceHud.setVisible(0);
    %this.toggleOP.setText("<a:OPoff >(guest view)</a>");
    if (HudTabs.getCurrentTab().name $= "private space")
    {
        HudTabs.dontCloseNextTime();
    }
    CSControlPanel.open();
    return ;
}
function PrivSpaceHud::hideOP(%this)
{
    OPSpaceHud.setVisible(0);
    NonOPSpaceHud.setVisible(1);
    %this.toggleOP.setText("<a:OPon >(host\'s view)</a>");
    if (HudTabs.getCurrentTab().name $= "private space")
    {
        HudTabs.autoHideSchedule($Pref::ETS::HudTabs::timeout);
        OPSpaceHud.descriptionChanged();
    }
    CSControlPanel.close();
    return ;
}
function PrivSpaceHud::updateMusic(%this, %newStreamID)
{
    if (isObject($musicStreamIDMap))
    {
        %newStreamName = $musicStreamIDMap.get(%newStreamID);
        if (%newStreamName $= "")
        {
            error("Stream ID (\"" @ %newStreamID @ "\") not in music stream id -> name mapping! <-" @ getScopeName());
            %newStreamName = %newStreamID;
        }
    }
    else
    {
        warn("the music stream ID map was not initialized.  This should have been done in GameConnection::etsInit()");
        %newStreamName = %newStreamID;
    }
    OPSpaceHud.updateMusic(%newStreamName);
    NonOPSpaceHud.updateMusic(%newStreamName);
    return ;
}
function clientCmdPrivSpaceHudUpdateMusic(%newStreamID)
{
    return ;
}
function clientCmdPrivSpaceHudUpdateVideo(%unused)
{
    return ;
}
function OPSpaceHud::setup(%this)
{
    %ypos = 0;
    %this.spaceNameField = new GuiTextCtrl()
    {
        profile = "InfoWindowNonModalTextProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = 60 SPC %ypos;
        extent = "155 18";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        text = "";
        maxLength = 64;
    };
    %this.add(%this.spaceNameField);
    %this.spaceDescField = new GuiTextEditCtrl()
    {
        profile = "InfoWindowTextEditProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = 0 SPC %ypos = %ypos + 20;
        extent = "215 16";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        variable = "text";
        altCommand = "OPSpaceHud.descriptionChanged();";
        maxLength = 32;
        historySize = 1;
        password = 0;
        tabComplete = 0;
        sinkAllKeyEvents = 0;
    };
    %this.add(%this.spaceDescField);
    %this.AccessOptAnyone = new GuiRadioCtrl()
    {
        profile = "InfoWindowRadioButtonProfile";
        groupNum = 1;
        buttonType = "RadioButton";
        horizSizing = "right";
        vertSizing = "bottom";
        position = 0 SPC %ypos = %ypos + 20;
        extent = "68 18";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        text = "Open";
        command = "OPSpaceHud.accessSelected(\"Open\");";
        maxLength = 64;
    };
    %this.add(%this.AccessOptAnyone);
    %this.AccessOptFriends = new GuiRadioCtrl()
    {
        profile = "InfoWindowRadioButtonProfile";
        groupNum = 1;
        buttonType = "RadioButton";
        horizSizing = "right";
        vertSizing = "bottom";
        position = 0 SPC %ypos = %ypos + 20;
        extent = "120 18";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        text = "Friends Only";
        command = "OPSpaceHud.accessSelected(\"FriendsOnly\");";
        maxLength = 64;
    };
    %this.add(%this.AccessOptFriends);
    %this.MusicStreamDropdown = new GuiPopUp2MenuCtrl()
    {
        profile = "InfoWindowPopupProfile";
        scrollProfile = "DottedScrollProfile";
        winProfile = "InfoWindowPopupWindowProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = 0 SPC %ypos = %ypos + 20;
        extent = "200 30";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        command = "OPSpaceHud.MusicSelected();";
        text = "";
        maxLength = 255;
        maxPopupHeight = 200;
        allowReverse = 0;
    };
    %this.add(%this.MusicStreamDropdown);
    return ;
}
function OPSpaceHud::accessSelected(%this, %accessLevel)
{
    if (!(%accessLevel $= %this.accessLevel))
    {
        CustomSpaceSettings::saveSettings(CustomSpaceClient::GetSpaceImIn(), "", %accessLevel, "", "");
    }
    %this.accessLevel = %accessLevel;
    return ;
}
function OPSpaceHud::descriptionChanged(%this)
{
    %description = %this.spaceDescField.getValue();
    if (!(%this.description $= %description))
    {
        CustomSpaceSettings::saveSettings(CustomSpaceClient::GetSpaceImIn(), %description, "", "", "");
    }
    %this.description = %description;
    return ;
}
function OPSpaceHud::MusicSelected(%this)
{
    %selection = %this.MusicStreamDropdown.getText();
    if (%selection $= "")
    {
        error("Empty string was selected on MusicStreamDropdown. Not sending request. Check that the server sent StreamID was in the $musicStreamIDMap checked by PrivSpaceHud::updateMusic <-" @ getScopeName());
        return ;
    }
    if (!(%selection $= %this.musicStream))
    {
        if (isObject($musicStreamNameMap))
        {
            log("communication", "debug", "getting the stream name from the musicStreamMap which is" SPC $musicStreamNameMap);
            %streamID = $musicStreamNameMap.get(%selection);
        }
        else
        {
            warn("the MusicStreamMap variable is not defined. We cannot get the music stream mapping.. so using PrivateSpace (the default)");
            %streamID = "PrivateSpace";
        }
        echo("setting music stream id = " @ %streamID);
        customSpace::SetMusicStreamID(%streamID);
    }
    %this.musicStream = %selection;
    return ;
}
function OPSpaceHud::updateMusic(%this, %newStreamName)
{
    if (%newStreamName $= "")
    {
        warn(getScopeName() @ "-> received an empty string for stream name.");
    }
    %this.musicStream = %newStreamName;
    if (%this.MusicStreamDropdown.size() != 0)
    {
        %index = %this.MusicStreamDropdown.findText(%newStreamName);
        if (%index < 0)
        {
            warn("Some music streams are loaded, but the latest update is not in the dropdown!<-" @ getScopeName());
        }
        %this.MusicStreamDropdown.SetSelected(%index);
    }
    else
    {
        warn("Tried to set selected music stream on updating OPSpaceHud settings, but the music wasn\'t loaded!<-" @ getScopeName());
    }
    return ;
}
function OPSpaceHud::updateStreams(%this, %streamList)
{
    %this.MusicStreamDropdown.fillFromList(%streamList);
    %selectedIndex = 0;
    if (!(%this.musicStream $= ""))
    {
        %selectedIndex = %this.MusicStreamDropdown.findText(%this.musicStream);
    }
    %this.MusicStreamDropdown.SetSelected(%selectedIndex);
    return ;
}
function OPSpaceHud::updateSettings(%this, %name, %description, %accessMode)
{
    %this.description = %description;
    %this.spaceDescField.setText(%description);
    %this.accessLevel = %accessMode;
    if (%accessMode $= "Open")
    {
        %this.AccessOptAnyone.performClick();
    }
    else
    {
        if (%accessMode $= "FriendsOnly")
        {
            %this.AccessOptFriends.performClick();
        }
        else
        {
            %this.accessLevel = "Open";
            %this.AccessOptAnyone.performClick();
        }
    }
    %this.spaceNameField.setText(%name);
    return ;
}
function NonOPSpaceHud::setup(%this)
{
    %ypos = 0;
    %this.spaceNameField = new GuiMLTextCtrl()
    {
        profile = "InfoWindowTextProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = 60 SPC %ypos;
        extent = "170 18";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        text = "";
        maxLength = 64;
    };
    %this.add(%this.spaceNameField);
    %this.spaceOwnerField = new GuiMLTextCtrl(NonOPSpaceHudOwnerField)
    {
        profile = "InfoWindowTextProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = 60 SPC %ypos;
        extent = "170 18";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        text = "";
        maxLength = 64;
    };
    %this.add(%this.spaceOwnerField);
    %this.spaceDescField = new GuiTextCtrl()
    {
        profile = "InfoWindowNonModalTextProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = 0 SPC %ypos = %ypos + 20;
        extent = "215 18";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        text = "a description";
        maxLength = 64;
    };
    %this.add(%this.spaceDescField);
    %this.musicStreamField = new GuiTextCtrl()
    {
        profile = "InfoWindowNonModalTextProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = 0 SPC %ypos = %ypos + 20;
        extent = "215 18";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        text = "My apartment radio ";
        maxLength = 64;
    };
    %this.add(%this.musicStreamField);
    %this.bigMLText = new GuiMLTextCtrl()
    {
        profile = "InfoWindowTextProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = 0 SPC %ypos = %ypos + 30;
        extent = "215 200";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        text = "";
        maxLength = 64;
    };
    %this.add(%this.bigMLText);
    return ;
}
function NonOPSpaceHud::updateSettings(%this, %name, %description, %owner)
{
    if (!(%owner $= ""))
    {
        %this.spaceOwnerField.setText("<a:owner " @ munge(%owner) @ ">" @ %owner @ "</a>");
    }
    else
    {
        %this.spaceOwnerField.setText("<a:noowner >Take Control</a>");
    }
    %this.spaceDescField.setText(%description);
    %this.spaceNameField.setText(%name);
    return ;
}
function NonOPSpaceHud::updateMusic(%this, %newStreamName)
{
    %this.musicStreamField.setText(%newStreamName);
    return ;
}
function GuiPopUp2MenuCtrl::fillFromList(%this, %list)
{
    %this.clear();
    %count = getFieldCount(%list);
    %i = 0;
    while (%i < %count)
    {
        %this.add(getField(%list, %i));
        %i = %i + 1;
    }
}

function NonOPSpaceHudOwnerField::onURL(%this, %url)
{
    if (getWord(%url, 0) $= "noowner")
    {
        CustomSpaceSettings::changeSpaceOwnership(CustomSpaceClient::GetSpaceImIn(), 1);
    }
    else
    {
        if (getWord(%url, 0) $= "owner")
        {
            onLeftClickPlayerName(unmunge(getWords(%url, 1)), "");
        }
    }
    return ;
}
function NonOPSpaceHudOwnerField::onRightURL(%this, %url)
{
    if (getWord(%url, 0) $= "noowner")
    {
    }
    else
    {
        if (getWord(%url, 0) $= "owner")
        {
            onRightClickPlayerName(unmunge(getWords(%url, 1)));
        }
    }
    return ;
}
function CustomSpaceSettings::saveSettings(%spaceName, %description, %accessMode, %password, %audioStream, %videoStream)
{
    if (!isObject($CSSpaceInfo) && !(($CSSpaceInfo.owner $= $Player::Name)))
    {
        return ;
    }
    if (!(%description $= ""))
    {
        $CSSpaceInfo.description = %description;
    }
    if (!(%accessMode $= ""))
    {
        $CSSpaceInfo.access = %accessMode;
    }
    if ($StandAlone)
    {
        echo(getScopeName() SPC "pretending success using standalone");
        return ;
    }
    if (!haveValidManagerHost() && !haveValidToken())
    {
        echo(getScopeName() SPC "No valid manager host or token.");
        return ;
    }
    %request = new ManagerRequest();
    if (isObject(MissionCleanup))
    {
        MissionCleanup.add(%request);
    }
    %url = $Net::ClientServiceURL @ "/SaveCustomSpaceSettings?" @ "user=" @ urlEncode($Player::Name) @ "&" @ "token=" @ urlEncode($Token) @ "&" @ "space=" @ urlEncode(%spaceName);
    if (!(%description $= ""))
    {
        %url = %url @ "&description=" @ urlEncode(%description);
    }
    if (!(%accessMode $= ""))
    {
        %url = %url @ "&accessMode=" @ urlEncode(%accessMode);
    }
    if (!(%password $= ""))
    {
        %url = %url @ "&password=" @ urlEncode(%password);
    }
    if (!(%audioStream $= ""))
    {
        %url = %url @ "&audioStream=" @ urlEncode(%audioStream);
    }
    if (!(%videoStream $= ""))
    {
        %url = %url @ "&videoStream=" @ urlEncode(%videoStream);
    }
    log("network", "info", getScopeName() @ ":" @ %url);
    %request.setURL(%url);
    %request.start();
    %request.requestDescription = %description;
    %request.requestAccessMode = %accessMode;
    if (!(%request.requestDescription $= ""))
    {
        CSRulesDescSavedIndicator.incrementRequestCount();
    }
    if (!(%request.requestAccessMode $= ""))
    {
        CSRulesPasswordSavedIndicator.incrementRequestCount();
    }
    return ;
}
function ModifyCustomSpaceSettingsRequest::onDone(%this)
{
    %status = findRequestStatus(%this);
    log("network", "debug", getScopeName() @ ":" @ %status);
    if (%status $= "fail")
    {
        warn("network", getScopeName() @ " request failed: " @ %this.getValue("statusMessage"));
    }
    %this.schedule(0, "delete");
    if (!(%this.requestDescription $= ""))
    {
        CSRulesDescSavedIndicator.decrementRequestCount();
    }
    if (!(%this.requestAccessMode $= ""))
    {
        CSRulesPasswordSavedIndicator.decrementRequestCount();
    }
    return ;
}
function ModifyCustomSpaceSettingsRequest::onError(%this, %unused, %errMsg)
{
    error("network", getScopeName() @ ":" @ %errMsg);
    %this.schedule(0, "delete");
    return ;
}
function CustomSpaceSettings::changeSpaceOwnership(%spaceName, %takeOwnershipBool)
{
    %request = new ManagerRequest();
    if (isObject(MissionCleanup))
    {
        MissionCleanup.add(%request);
    }
    %url = $Net::BaseURL @ "?cmd=ChangeSpaceOwnership" @ "&user=" @ urlEncode($Player::Name) @ "&token=" @ urlEncode($Token) @ "&space=" @ urlEncode(%spaceName) @ "&take=" @ urlEncode(%takeOwnershipBool ? 1 : 0);
    log("network", "info", getScopeName() @ ":" @ %url);
    %request.setURL(%url);
    %request.spaceName = %spaceName;
    %request.start();
    return ;
}
function ChangeSpaceOwnershipRequest::onDone(%this)
{
    %status = findRequestStatus(%this);
    %statusMsg = %this.getValue("statusMsg");
    log("network", "info", getScopeName() @ ":" @ %status @ " - msg: " @ %statusMsg);
    if (%status $= "success")
    {
        HudTabs.selectTabWithName("private space");
    }
    else
    {
        if (trim(getWords(%statusMsg, 0, 1)) $= "fail already-owned")
        {
            handleSystemMessage("msgInfoMessage", "Sorry, the space is already owned by someone else.");
        }
        else
        {
            if (trim(getWords(%statusMsg, 0, 1)) $= "fail respekt")
            {
                handleSystemMessage("msgInfoMessage", "Sorry, you must be at least a " @ getWord(%statusMsg, 2) @ " to own this space.");
            }
            else
            {
                handleSystemMessage("msgInfoMessage", "Sorry, you couldn\'t change the ownership of the space.");
            }
        }
    }
    %this.schedule(0, "delete");
    return ;
}
function CustomSpaceSettings::onError(%this, %unused, %errMsg)
{
    error("network", getScopeName() @ ":" @ %errMsg);
    %this.schedule(0, "delete");
    return ;
}
function HudTabs::addPermissionBasedContent(%this)
{
    if (!%this.filledPrivateSpaceTab)
    {
        return ;
    }
    %hasPerm = $player.rolesPermissionCheckNoWarn("fly");
    SpaceSurfText.setVisible(%hasPerm);
    SpaceSurfTE.setVisible(%hasPerm);
    if (%hasPerm && ($gMode $= "PrivateSpaceGrid"))
    {
        %this.showTabWithName("private space");
    }
    return ;
}
function SpaceSurfText::onURL(%this, %url)
{
    teleportToAdjacentSpace(%url $= "spaceN");
    return ;
}
function SpaceSurfTE::onEnter(%this)
{
    teleportToSpaceNumber(%this.getValue());
    return ;
}
