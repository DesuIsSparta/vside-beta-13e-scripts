$OptionsPanel::scheduledPersistID = 0;
if (!isObject(OptionsPanelTabs))
{
    new ScriptObject(OptionsPanelTabs);
    if (isObject(MissionCleanup))
    {
        MissionCleanup.add(OptionsPanelTabs);
    }
}
function OptionsPanelTabs::setup(%this)
{
    if (!%this.initialized)
    {
        %this.Initialize(OptionsPanelTabContainer, "88 33", "", "0 0", "vertical");
        %this.newTab("social", "platform/client/buttons/settings_social");
        %this.newTab("audio", "platform/client/buttons/settings_audio");
        %this.newTab("visual", "platform/client/buttons/settings_visual");
        %this.newTab("tabs", "platform/client/buttons/settings_tabs");
        %this.newTab("vip", "platform/client/buttons/settings_vip");
        %this.selectTabWithName("social");
        %this.hideTabWithName("vip");
        %this.fillTabs();
    }
    return ;
}
function OptionsPanelTabs::fillTabs(%this)
{
    %i = 0;
    while (%i < %this.numTabs)
    {
        %tab = %this.tabs[%i];
        %tab.setProfile(ETSNonModalProfile);
        %tab.clear();
        %i = %i + 1;
    }
    %this.fillSocialTab();
    %this.fillAudioTab();
    %this.fillVisualTab();
    %this.fillTabsTab();
    %this.fillVIPTab();
    return ;
}
function OptionsPanelTabs::tabSelected(%this, %tab)
{
    if (%tab.hasFieldValue("initialFirstResponder"))
    {
        if (isObject(%tab.initialFirstResponder))
        {
            %tab.initialFirstResponder.makeFirstResponder(1);
        }
    }
    return ;
}
function OptionsPanelTabs::fillSocialTab(%this)
{
    %tab = %this.getTabWithName("social");
    %originX = 10;
    %originY = 0;
    %posX = %originX;
    %posY = %originY;
    %dPosX = 180;
    %dPosY = 20;
    %dPosYSmall = 5;
    %indent1 = 10;
    %indent2 = 160;
    %posY = %posY + %dPosY;
    %posX = %posX + %indent1;
    %posY = %posY + %dPosY;
    %posY = %posY + %dPosY;
    %posY = %posY + %dPosYSmall;
    %posX = %posX - %indent1;
    %posY = %posY + %dPosY;
    %posX = %posX + %indent1;
    %text = "Show My Typing";
    %text = "Refuse Teleports";
    %posY = %posY + %dPosY;
    %text = "Filter Profanity";
    %text = "Refuse Whispers";
    %posY = %posY + %dPosY;
    %text = "Show Me on Radar";
    %text = "Refuse Yells";
    %posY = %posY + %dPosY;
    %posY = %posY + %dPosYSmall;
    %posX = %posX - %indent1;
    %posX = %posX + 120;
    %posX = %posX + 120;
    %posX = %posX - 240;
    %posY = %posY + %dPosY;
    %posY = %posY + 5;
    %posX = %posX + 120;
    %posX = %posX + 120;
    %posX = %posX - 240;
    %posY = %posY + %dPosY;
    %posX = %posX - %indent1;
    %tab.initialFirstResponder = DefaultAwayMsgEdit;
    return ;
}
function OptionsPanelTabs::fillAudioTab(%this)
{
    %tab = %this.getTabWithName("audio");
    %originX = 10;
    %originY = 10;
    %posX = %originX;
    %posY = %originY;
    %dPosX = 180;
    %dPosY = 20;
    %indent = 38;
    %posY = %posY + %dPosY;
    %posY = %posY + %dPosY;
    %posY = %posY + %dPosY;
    %posX = %posX + %indent;
    %posY = %posY + %dPosY;
    %posX = %posX - %indent;
    %posY = %posY + %dPosY;
    %posY = %posY + %dPosY;
    %posX = %posX + %indent;
    %posY = %posY + %dPosY;
    %posY = %posY + %dPosY;
    %posX = %posX - %indent;
    return ;
}
function OptionsPanelTabs::fillVisualTab(%this)
{
    %tab = %this.getTabWithName("visual");
    %originX = 10;
    %originY = 10;
    %posX = %originX;
    %posY = %originY;
    %dPosX = 180;
    %dPosY = 20;
    %indent = 14;
    %posY = %posY + %dPosY;
    %posY = %posY + %dPosY;
    %posY = %posY + %dPosY;
    %posY = %posY + %dPosY;
    %posY = %posY + %dPosY;
    %posX = %posX + %indent;
    %posY = %posY + (2 * %dPosY);
    %posX = %posX - %indent;
    %posY = %posY + %dPosY;
    %posX = %posX + %indent;
    %posY = %posY + (2 * %dPosY);
    %posX = %posX - %indent;
    %posX = %posX + %dPosX;
    %posY = %originY - 12;
    AutoHideButtonBarCheckBox.setValue($UserPref::ETS::ButtonBar::AutoHide);
    %posY = %posY + %dPosY;
    AutoOpenLocalMapCheckBox.setValue($UserPref::UI::Radar::AutoOpen);
    %posY = %posY + %dPosY;
    %posY = %posY + %dPosY;
    %posY = %posY + %dPosY;
    %posY = %posY + %dPosY;
    %posY = %posY + %dPosY;
    %posY = %posY + %dPosY;
    %posY = %posY + %dPosY;
    %tab.initialFirstResponder = RenderQualityPopup;
    return ;
}
function OptionsPanelTabs::fillTabsTab(%this)
{
    %tab = %this.getTabWithName("tabs");
    %originX = 10;
    %originY = 10;
    %posX = %originX;
    %posY = %originY;
    %dPosX = 180;
    %dPosY = 30;
    %indent1 = 37;
    %indent2 = 90;
    %indent3 = 140;
    %posY = %posY + %dPosY;
    new GuiControl()
    {
        position = %posX SPC %posY;
        extent = "36 29";
        visible = 1;
    }.add(new GuiControl()
    {
        position = %posX SPC %posY;
        extent = "36 29";
        visible = 1;
    });
    %posY = %posY + %dPosY;
    new GuiControl()
    {
        position = %posX SPC %posY;
        extent = "36 29";
        visible = 1;
    }.add(new GuiControl()
    {
        position = %posX SPC %posY;
        extent = "36 29";
        visible = 1;
    });
    %posY = %posY + %dPosY;
    new GuiControl()
    {
        position = %posX SPC %posY;
        extent = "36 29";
        visible = 1;
    }.add(new GuiControl()
    {
        position = %posX SPC %posY;
        extent = "36 29";
        visible = 1;
    });
    %posY = %posY + %dPosY;
    new GuiControl()
    {
        position = %posX SPC %posY;
        extent = "36 29";
        visible = 1;
    }.add(new GuiControl()
    {
        position = %posX SPC %posY;
        extent = "36 29";
        visible = 1;
    });
    %posY = %posY + %dPosY;
    %posX = %posX + %dPosX;
    %posY = %originY;
    %posY = %posY + %dPosY;
    %posY = %posY + %dPosY;
    return ;
}
function OptionsPanelTabs::fillVIPTab(%this)
{
    %tab = %this.getTabWithName("vip");
    %originX = 10;
    %originY = 10;
    %posX = %originX;
    %posY = %originY;
    %dPosX = 180;
    %dPosY = 20;
    %indent = 10;
    %posY = %posY + %dPosY;
    new GuiControl(ForceFieldCtrls)
    {
        position = %posX SPC %posY;
        extent = 94 SPC %dPosY * 5;
        visible = $gPerformerMode;
    }.add(new GuiControl(ForceFieldCtrls)
    {
        position = %posX SPC %posY;
        extent = 94 SPC %dPosY * 5;
        visible = $gPerformerMode;
    });
    %posY = %posY + (6 * %dPosY);
    performerPanelRadioButtonForceField0.setValue(1);
    %posY = %posY + %dPosY;
    %posY = %posY + %dPosY;
    %posX = %posX + %indent;
    new GuiControl(geDFDebugCtrls)
    {
        horizSizing = "right";
        vertSizing = "bottom";
        position = %posX SPC %posY;
        extent = "300 20";
        visible = $Pref::DF::debugMode;
    }.add(new GuiControl(geDFDebugCtrls)
    {
        horizSizing = "right";
        vertSizing = "bottom";
        position = %posX SPC %posY;
        extent = "300 20";
        visible = $Pref::DF::debugMode;
    });
    %posY = %posY + %dPosY;
    %posX = %posX - %indent;
    %posX = %posX + %dPosX;
    %posY = %originY;
    %posY = %posY + %dPosY;
    new GuiControl(FarNameOpacityCtrl)
    {
        profile = "GuiDefaultProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = %posX SPC %posY;
        extent = "190 70";
        minExtent = "8 2";
        visible = 0;
    }.add(new GuiControl(FarNameOpacityCtrl)
    {
        profile = "GuiDefaultProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = %posX SPC %posY;
        extent = "190 70";
        minExtent = "8 2";
        visible = 0;
    });
    %posY = %posY + (%dPosY * 4);
    new GuiControl(optionsPanelAlertOnLogCtrl)
    {
        position = %posX SPC %posY;
        extent = 190 SPC %dPosY * 3;
        visible = $gPerformerMode;
    }.add(new GuiControl(optionsPanelAlertOnLogCtrl)
    {
        position = %posX SPC %posY;
        extent = 190 SPC %dPosY * 3;
        visible = $gPerformerMode;
    });
    %tab.initialFirstResponder = FarNameOpacityTextEditCtrl;
    return ;
}
function OptionsPanelTabs::wakeUp(%this)
{
    %this.setup();
    %this.selectCurrentTab();
    return ;
}
function OptionsPanelTabs::restoreSocialDefaults(%this)
{
    $UserPref::Player::awayMessage = $Pref::Player::defaultAwayMessage;
    $UserPref::Chat::ShowTyping = $Defaults::UserPref::Chat::ShowTyping;
    $UserPref::Player::TeleportBlock = $Defaults::UserPref::Player::TeleportBlock;
    $UserPref::Player::WhisperBlock = $Defaults::UserPref::Player::WhisperBlock;
    $UserPref::Player::YellBlock = $Defaults::UserPref::Player::YellBlock;
    $UserPref::Player::filterProfanity = $Defaults::UserPref::Player::filterProfanity;
    $UserPref::Player::showOnRadar = $Defaults::UserPref::Player::showOnRadar;
    $UserPref::Player::EmotesPermissionFriends = $Defaults::UserPref::Player::EmotesPermissionFriends;
    $UserPref::Player::EmotesPermissionStrangers = $Defaults::UserPref::Player::EmotesPermissionStrangers;
    $UserPref::Player::GiftsPermissionFriends = $Defaults::UserPref::Player::GiftsPermissionFriends;
    $UserPref::Player::GiftsPermissionStrangers = $Defaults::UserPref::Player::GiftsPermissionStrangers;
    OptionsPanel.readSettings();
    schedulePersist();
    return ;
}
function OptionsPanelTabs::restoreAudioDefaults(%this)
{
    $UserPref::Audio::masterVolume = $Defaults::UserPref::Audio::masterVolume;
    $UserPref::Audio::channelVolume1 = $Defaults::UserPref::Audio::channelVolume1;
    $UserPref::Audio::channelVolume2 = $Defaults::UserPref::Audio::channelVolume2;
    if ($UserPref::Audio::mute != $Defaults::UserPref::Audio::mute)
    {
        Music::toggleMute();
    }
    $UserPref::Audio::NotifyChat = $Defaults::UserPref::Audio::NotifyChat;
    $UserPref::Audio::NotifyWhisper = $Defaults::UserPref::Audio::NotifyWhisper;
    OptionsPanel.readSettings();
    schedulePersist();
    return ;
}
function OptionsPanelTabs::restoreVisualDefaults(%this)
{
    $UserPref::Video::Exposure = $Defaults::UserPref::Video::Exposure;
    $UserPref::Video::renderQualitySetting = $Defaults::UserPref::Video::renderQuality;
    $UserPref::Video::shadowQualitySetting = $Defaults::UserPref::Video::shadowQuality;
    $UserPref::Video::smalltextureQualitySetting = $Defaults::UserPref::Video::smalltextureQuality;
    $UserPref::Video::visibledistanceQualitySetting = $Defaults::UserPref::Video::visibledistanceQuality;
    $UserPref::Video::waterreflectionQualitySetting = $Defaults::UserPref::Video::waterreflectionQuality;
    $UserPref::Video::exposureQualitySetting = $Defaults::UserPref::Video::exposureQuality;
    $UserPref::Video::shapeNameFontSize = $Defaults::UserPref::Video::shapeNameFontSize;
    $UserPref::UI::Radar::AutoOpen = $Defaults::UserPref::UI::Radar::AutoOpen;
    $UserPref::UI::FlashTaskBar = $Defaults::UserPref::UI::FlashTaskBar;
    $UserPref::UI::ShowAccountHud = $Defaults::UserPref::UI::ShowAccountHud;
    AccountBalanceHud.update();
    $UserPref::UI::ShowTooltips = $Defaults::UserPref::UI::ShowTooltips;
    ButtonBar.setAutoHiding($Defaults::UserPref::ETS::ButtonBar::AutoHide);
    if (isObject(gMessageBoxDontShow))
    {
        gMessageBoxDontShow.clear();
    }
    OptionsPanel.readSettings();
    schedulePersist();
    return ;
}
function OptionsPanelTabs::restoreTabsDefaults(%this)
{
    $UserPref::HudTabs::AutoOpen["music"] = $Defaults::UserPref::HudTabs::AutoOpen["music"] ;
    $UserPref::HudTabs::AutoClose["music"] = $Defaults::UserPref::HudTabs::AutoClose["music"] ;
    $UserPref::HudTabs::AutoOpen["affinity"] = $Defaults::UserPref::HudTabs::AutoOpen["affinity"] ;
    $UserPref::HudTabs::AutoClose["affinity"] = $Defaults::UserPref::HudTabs::AutoClose["affinity"] ;
    $UserPref::HudTabs::AutoOpen["scores"] = $Defaults::UserPref::HudTabs::AutoOpen["scores"] ;
    $UserPref::HudTabs::AutoClose["scores"] = $Defaults::UserPref::HudTabs::AutoClose["scores"] ;
    $UserPref::HudTabs::AutoOpen["word"] = $Defaults::UserPref::HudTabs::AutoOpen["word"] ;
    $UserPref::HudTabs::AutoClose["word"] = $Defaults::UserPref::HudTabs::AutoClose["word"] ;
    $UserPref::HudTabs::AutoOpen["tutorial"] = $Defaults::UserPref::HudTabs::AutoOpen["tutorial"] ;
    $UserPref::HudTabs::AutoClose["tutorial"] = $Defaults::UserPref::HudTabs::AutoClose["tutorial"] ;
    %currentTab = HudTabs.getCurrentTab();
    %tabName = %currentTab $= "" ? "" : %currentTab;
    if ($UserPref::HudTabs::AutoClose[%tabName])
    {
        HudTabs.autoHide();
    }
    OptionsPanel.readSettings();
    schedulePersist();
    return ;
}
function OptionsPanelTabs::restoreVIPDefaults(%this)
{
    performerPanelRadioButtonForceField0.performClick();
    $UserPref::Display::hideNames = $Defaults::UserPref::Display::hideNames;
    $UserPref::Display::hideChat = $Defaults::UserPref::Display::hideChat;
    $UserPref::Display::farNameOpacity = $Defaults::UserPref::Display::farNameOpacity;
    $UserPref::debug::alertOnLogWarning = $Defaults::UserPref::debug::alertOnLogWarning;
    $UserPref::debug::alertOnLogError = $Defaults::UserPref::debug::alertOnLogError;
    $Pref::DF::debugMode = $Defaults::pref::DF::debugMode;
    $Pref::DF::showDefaults = $Defaults::pref::DF::showDefaults;
    geDFDebugMode.setValue(!$Pref::DF::debugMode);
    geDFDebugMode.performClick();
    geDFShowDefaults.setValue(!$Pref::DF::showDefaults);
    geDFShowDefaults.performClick();
    OptionsPanel.readSettings();
    schedulePersist();
    return ;
}
function OptionsPanelTabs::onChangedHideChat(%this)
{
    if ($UserPref::Display::hideChat)
    {
        ConvBub.close(0);
        SystemMessageDialog.close();
    }
    else
    {
        if (ConvBubVecCtrlMsgVec.getNumLines() > 0)
        {
            ConvBub.open();
        }
    }
    schedulePersist();
    return ;
}
function OptionsPanelTabs::onChangedDFDebugMode(%this)
{
    geDFDebugCtrls.setVisible($Pref::DF::debugMode);
    DF_DebugMode($Pref::DF::debugMode);
    return ;
}
function OptionsPanelTabs::onChangedDFShowDefaults(%this)
{
    DF_ShowDefaults($Pref::DF::showDefaults);
    return ;
}
function OptionsPanelTabs::onChangedShowNames(%this)
{
    $UserPref::Display::hideNames = !HUDShowNamesCheckBox.getValue();
    TheBadgesHud.setVisible(!$UserPref::Display::hideNames);
    return ;
}
function OptionsPanel::open(%this)
{
    %this.readSettings();
    %this.setVisible(1);
    PlayGui.focusAndRaise(%this);
    if (($player && $player.rolesPermissionCheckNoWarn("quietHUD")) || $player.rolesPermissionCheckNoWarn("farNameOpacity"))
    {
        OptionsPanelTabs.showTabWithName("vip");
    }
    else
    {
        if (OptionsPanelTabs.getCurrentTab().name $= "vip")
        {
            OptionsPanelTabs.selectTabAtIndex(0);
        }
        OptionsPanelTabs.hideTabWithName("vip");
    }
    OptionsPanelTabs.selectCurrentTab();
    return ;
}
function OptionsPanel::close(%this)
{
    %this.applySettings();
    %this.setVisible(0);
    PlayGui.focusTopWindow();
    return 1;
}
function OptionsPanel::wakeUp(%this)
{
    OptionsPanelTabs.wakeUp();
    return ;
}
function OptionsPanel::Initialize(%this)
{
    OptionsPanelTabs.setup();
    TwoPlayerActionsFriendsPopup.clear();
    TwoPlayerActionsFriendsPopup.add(" Accept", 0);
    TwoPlayerActionsFriendsPopup.add(" Ask Me", 1);
    TwoPlayerActionsFriendsPopup.add(" Decline", 2);
    TwoPlayerActionsStrangersPopup.clear();
    TwoPlayerActionsStrangersPopup.add(" Accept", 0);
    TwoPlayerActionsStrangersPopup.add(" Ask Me", 1);
    TwoPlayerActionsStrangersPopup.add(" Decline", 2);
    GiftsFriendsPopup.clear();
    GiftsFriendsPopup.add(" Accept", 0);
    GiftsFriendsPopup.add(" Ask Me", 1);
    GiftsFriendsPopup.add(" Decline", 2);
    GiftsStrangersPopup.clear();
    GiftsStrangersPopup.add(" Accept", 0);
    GiftsStrangersPopup.add(" Ask Me", 1);
    GiftsStrangersPopup.add(" Decline", 2);
    RenderQualityPopup.clear();
    RenderQualityPopup.add(" Low", 0);
    RenderQualityPopup.add(" Medium", 1);
    RenderQualityPopup.add(" High", 2);
    RenderQualityPopup.add(" Automatic", 3);
    ShadowDetailSizePopup.clear();
    ShadowDetailSizePopup.add(" Low", 0);
    ShadowDetailSizePopup.add(" Medium", 1);
    ShadowDetailSizePopup.add(" High", 2);
    ShadowDetailSizePopup.add(" Automatic", 3);
    VisibleDistancePopup.clear();
    VisibleDistancePopup.add(" Low", 0);
    VisibleDistancePopup.add(" Medium", 1);
    VisibleDistancePopup.add(" High", 2);
    VisibleDistancePopup.add(" Automatic", 3);
    WaterReflectionModePopup.clear();
    WaterReflectionModePopup.add(" Low", 0);
    WaterReflectionModePopup.add(" High", 2);
    WaterReflectionModePopup.add(" Automatic", 3);
    ExposureFilterModePopup.clear();
    ExposureFilterModePopup.add(" Low", 0);
    ExposureFilterModePopup.add(" High", 2);
    ExposureFilterModePopup.add(" Automatic", 3);
    FontSizePopup.clear();
    FontSizePopup.add(" Small", 0);
    FontSizePopup.add(" Medium", 1);
    FontSizePopup.add(" Large", 2);
    %this.readSettings();
    return ;
}
function schedulePersist()
{
    if ($OptionsPanel::scheduledPersistID != 0)
    {
        cancel($OptionsPanel::scheduledPersistID);
    }
    $OptionsPanel::scheduledPersistID = schedule(4000, 0, "persistOptionsPanelSettingsToManager");
    return ;
}
function persistOptionsPanelSettingsToManager()
{
    if (!haveValidManagerHost() && !haveValidToken())
    {
        return ;
    }
    gUserPropMgrClient.setProperty($Player::Name, "volumeMaster", $UserPref::Audio::masterVolume);
    gUserPropMgrClient.setProperty($Player::Name, "volumeMusic", $UserPref::Audio::channelVolume1);
    gUserPropMgrClient.setProperty($Player::Name, "volumeSfx", $UserPref::Audio::channelVolume2);
    gUserPropMgrClient.setProperty($Player::Name, "volumeMute", $UserPref::Audio::mute);
    gUserPropMgrClient.setProperty($Player::Name, "flashIncomingChat", $UserPref::Audio::NotifyChat);
    gUserPropMgrClient.setProperty($Player::Name, "flashIncomingWhisper", $UserPref::Audio::NotifyWhisper);
    gUserPropMgrClient.setProperty($Player::Name, "showTyping", $UserPref::Chat::ShowTyping);
    gUserPropMgrClient.setProperty($Player::Name, "farOpacity", $UserPref::Display::farNameOpacity);
    gUserPropMgrClient.setProperty($Player::Name, "hideChat", $UserPref::Display::hideChat);
    gUserPropMgrClient.setProperty($Player::Name, "hideNames", $UserPref::Display::hideNames);
    gUserPropMgrClient.setProperty($Player::Name, "hideButtonBar", $UserPref::ETS::ButtonBar::AutoHide);
    gUserPropMgrClient.setProperty($Player::Name, "autoOpenTabMusic", $UserPref::HudTabs::AutoOpen["music"]);
    gUserPropMgrClient.setProperty($Player::Name, "autoCloseTabMusic", $UserPref::HudTabs::AutoClose["music"]);
    gUserPropMgrClient.setProperty($Player::Name, "autoOpenTabAffinity", $UserPref::HudTabs::AutoOpen["affinity"]);
    gUserPropMgrClient.setProperty($Player::Name, "autoCloseTabAffinity", $UserPref::HudTabs::AutoClose["affinity"]);
    gUserPropMgrClient.setProperty($Player::Name, "autoOpenTabScores", $UserPref::HudTabs::AutoOpen["scores"]);
    gUserPropMgrClient.setProperty($Player::Name, "autoCloseTabScores", $UserPref::HudTabs::AutoClose["scores"]);
    gUserPropMgrClient.setProperty($Player::Name, "autoOpenTabWord", $UserPref::HudTabs::AutoOpen["word"]);
    gUserPropMgrClient.setProperty($Player::Name, "autoCloseTabWord", $UserPref::HudTabs::AutoClose["word"]);
    gUserPropMgrClient.setProperty($Player::Name, "refuseTeleports", $UserPref::Player::TeleportBlock);
    gUserPropMgrClient.setProperty($Player::Name, "refuseWhispers", $UserPref::Player::WhisperBlock);
    gUserPropMgrClient.setProperty($Player::Name, "refuseYells", $UserPref::Player::YellBlock);
    gUserPropMgrClient.setProperty($Player::Name, "emotesPermissionsFriends", $UserPref::Player::EmotesPermissionFriends);
    gUserPropMgrClient.setProperty($Player::Name, "emotesPermissionsStrangers", $UserPref::Player::EmotesPermissionStrangers);
    gUserPropMgrClient.setProperty($Player::Name, "giftsPermissionsFriends", $UserPref::Player::GiftsPermissionFriends);
    gUserPropMgrClient.setProperty($Player::Name, "giftsPermissionsStrangers", $UserPref::Player::GiftsPermissionStrangers);
    gUserPropMgrClient.setProperty($Player::Name, "awayMessage", $UserPref::Player::awayMessage);
    gUserPropMgrClient.setProperty($Player::Name, "autoReplyToWhipsers", $UserPref::Player::autoReplyToWhispersWhenAway);
    gUserPropMgrClient.setProperty($Player::Name, "filterProfanity", $UserPref::Player::filterProfanity);
    gUserPropMgrClient.setProperty($Player::Name, "playerMood", $UserPref::Player::Genre);
    gUserPropMgrClient.setProperty($Player::Name, "avatarHeight", $UserPref::Player::height);
    gUserPropMgrClient.setProperty($Player::Name, "showOnRadar", $UserPref::Player::showOnRadar);
    gUserPropMgrClient.setProperty($Player::Name, "flashTaskBar", $UserPref::UI::FlashTaskBar);
    gUserPropMgrClient.setProperty($Player::Name, "radarAutoOpen", $UserPref::UI::Radar::AutoOpen);
    gUserPropMgrClient.setProperty($Player::Name, "showAccountHud", $UserPref::UI::ShowAccountHud);
    gUserPropMgrClient.setProperty($Player::Name, "showTooltips", $UserPref::UI::ShowTooltips);
    gUserPropMgrClient.setProperty($Player::Name, "videoExposure", $UserPref::Video::Exposure);
    gUserPropMgrClient.setProperty($Player::Name, "videoNameSize", $UserPref::Video::shapeNameFontSize);
    gUserPropMgrClient.setProperty($Player::Name, "videoRenderQuality", $UserPref::Video::renderQualitySetting);
    gUserPropMgrClient.setProperty($Player::Name, "videoShadowQuality", $UserPref::Video::shadowQualitySetting);
    gUserPropMgrClient.setProperty($Player::Name, "videoVisibleDistance", $UserPref::Video::visibledistanceQualitySetting);
    gUserPropMgrClient.setProperty($Player::Name, "videoWaterReflection", $UserPref::Video::waterreflectionQualitySetting);
    gUserPropMgrClient.setProperty($Player::Name, "videoConstrainWindowDimensions", $UserPref::Video::ConstrainWindowDimensions);
    gUserPropMgrClient.setProperty($Player::Name, "alertOnLogWarning", $UserPref::debug::alertOnLogWarning);
    gUserPropMgrClient.setProperty($Player::Name, "alertOnLogError", $UserPref::debug::alertOnLogError);
    %maxNumberKeyCombos = getFieldCount($Defaults::UserPref::emotes::defaultKeyCombinations);
    %i = %maxNumberKeyCombos - 1;
    while (%i >= 0)
    {
        %keyCombo = getField($Defaults::UserPref::emotes::defaultKeyCombinations, %i);
        gUserPropMgrClient.setProperty($Player::Name, "favoriteActionsKey_f_" @ %keyCombo, $UserPref::emotes["f",%keyCombo]);
        gUserPropMgrClient.setProperty($Player::Name, "favoriteActionsKey_m_" @ %keyCombo, $UserPref::emotes["m",%keyCombo]);
        %i = %i - 1;
    }
    $OptionsPanel::scheduledPersistID = 0;
    legacyPersistOptionsPanelSettingsToManager();
    return ;
}
function LegacySaveSettingsRequest::onDone(%this)
{
    log("communication", "info", "settings successfully saved to manager");
    %this.schedule(0, "delete");
    return ;
}
function LegacySaveSettingsRequest::onError(%this, %unused, %errName)
{
    log("communication", "info", "error saving settings: " @ %errName);
    %this.schedule(0, "delete");
    return ;
}
function legacyPersistOptionsPanelSettingsToManager()
{
    if (!haveValidManagerHost() && !haveValidToken())
    {
        return ;
    }
    %request = new ManagerRequest(LegacySaveSettingsRequest);
    if (isObject(MissionCleanup))
    {
        MissionCleanup.add(%request);
    }
    %url = $Net::ClientServiceURL @ "/SaveSettings";
    %url = %url @ "?user=" @ urlEncode($Player::Name);
    %url = %url @ "&token=" @ urlEncode($Token);
    %url = %url @ "&settingsMapCount=" @ 3;
    %url = %url @ "&settingsMap0.key=refuseTeleports&settingsMap0.value=" @ urlEncode($UserPref::Player::TeleportBlock);
    %url = %url @ "&settingsMap1.key=refuseWhispers&settingsMap1.value=" @ urlEncode($UserPref::Player::WhisperBlock);
    %url = %url @ "&settingsMap2.key=awayMessage&settingsMap2.value=" @ urlEncode($UserPref::Player::awayMessage);
    log("communication", "debug", "save settings: " @ %url);
    %request.setURL(%url);
    %request.start();
    return ;
}
function OptionsPanel::readSettings(%this)
{
    DefaultAwayMsgEdit.setValue($UserPref::Player::awayMessage);
    DefaultAwayMsgEdit.applySettings();
    ShowTypingCheckBox.setValue($UserPref::Chat::ShowTyping);
    FilterProfanityCheckBox.setValue($UserPref::Player::filterProfanity);
    TeleportBlockCheckBox.setValue($UserPref::Player::TeleportBlock);
    WhisperBlockCheckBox.setValue($UserPref::Player::WhisperBlock);
    YellBlockCheckBox.setValue($UserPref::Player::YellBlock);
    FilterProfanityCheckBox.setValue($UserPref::Player::filterProfanity);
    ShowOnRadarCheckBox.setValue($UserPref::Player::showOnRadar);
    TwoPlayerActionsFriendsPopup.SetSelected($UserPref::Player::EmotesPermissionFriends);
    TwoPlayerActionsStrangersPopup.SetSelected($UserPref::Player::EmotesPermissionStrangers);
    GiftsFriendsPopup.SetSelected($UserPref::Player::GiftsPermissionFriends);
    GiftsStrangersPopup.SetSelected($UserPref::Player::GiftsPermissionStrangers);
    VolumeSlider.setValue($UserPref::Audio::masterVolume);
    VolumeSlider.applySettings();
    VolumeMusicSlider.setValue($UserPref::Audio::channelVolume1);
    VolumeMusicSlider.applySettings();
    VolumeSFXSlider.setValue($UserPref::Audio::channelVolume2);
    VolumeSFXSlider.applySettings();
    MuteCheckBox.setValue($UserPref::Audio::mute);
    NotifyChatCheckBox.setValue($UserPref::Audio::NotifyChat);
    NotifyWhisperCheckBox.setValue($UserPref::Audio::NotifyWhisper);
    BrightnessSlider.setValue($UserPref::Video::Exposure);
    BrightnessSlider.applySettings();
    RenderQualityPopup.SetSelected($UserPref::Video::renderQualitySetting);
    ShadowDetailSizePopup.SetSelected($UserPref::Video::shadowQualitySetting);
    VisibleDistancePopup.SetSelected($UserPref::Video::visibledistanceQualitySetting);
    WaterReflectionModePopup.SetSelected($UserPref::Video::waterreflectionQualitySetting);
    ExposureFilterModePopup.SetSelected($UserPref::Video::exposureQualitySetting);
    FontSizePopup.SetSelected($UserPref::Video::shapeNameFontSize);
    AutoOpenLocalMapCheckBox.setValue($UserPref::UI::Radar::AutoOpen);
    AutoHideButtonBarCheckBox.setValue($UserPref::ETS::ButtonBar::AutoHide);
    DisplayFlashTaskBarCheckBox.setValue($UserPref::UI::FlashTaskBar);
    ShowAccountHudCheckBox.setValue($UserPref::UI::ShowAccountHud);
    OptionsVisualShowTooltipsCheckbox.setValue($UserPref::UI::ShowTooltips);
    %curScreenMode = getRes();
    %curScreenScale = getWord(%curScreenMode, 0) / 16;
    HudMusicAutoOpenCheckBox.setValue($UserPref::HudTabs::AutoOpen["music"]);
    HudMusicAutoCloseCheckBox.setValue($UserPref::HudTabs::AutoClose["music"]);
    HudAffinityAutoOpenCheckBox.setValue($UserPref::HudTabs::AutoOpen["affinity"]);
    HudAffinityAutoCloseCheckBox.setValue($UserPref::HudTabs::AutoClose["affinity"]);
    HudScoresAutoOpenCheckBox.setValue($UserPref::HudTabs::AutoOpen["scores"]);
    HudScoresAutoCloseCheckBox.setValue($UserPref::HudTabs::AutoClose["scores"]);
    HudWordAutoOpenCheckBox.setValue($UserPref::HudTabs::AutoOpen["word"]);
    HudWordAutoCloseCheckBox.setValue($UserPref::HudTabs::AutoClose["word"]);
    HUDShowNamesCheckBox.setValue(!$UserPref::Display::hideNames);
    HUDHideChatCheckBox.setValue($UserPref::Display::hideChat);
    FarNameOpacityTextEditCtrl.setValue($UserPref::Display::farNameOpacity);
    optionsPanelAlertOnWarningCheckBox.setValue($UserPref::debug::alertOnLogWarning);
    optionsPanelAlertOnErrorCheckBox.setValue($UserPref::debug::alertOnLogError);
    return ;
}
function OptionsPanel::applySettings(%this)
{
    DefaultAwayMsgEdit.applySettings();
    schedulePersist();
    return ;
}
function OptionsPanel::showBrightnessControls(%this, %flag)
{
    %this.showBrightnessControls = %flag;
    if (isObject(BrightnessLabel) && isObject(BrightnessSlider))
    {
        BrightnessLabel.setVisible(%flag);
        BrightnessSlider.setVisible(%flag);
    }
    return ;
}
function DefaultAwayMsgEdit::applySettings(%this)
{
    %this.setValue(trim(%this.getValue()));
    %newAwayMsg = %this.getValue();
    if (%newAwayMsg $= "")
    {
        if ($UserPref::Player::awayMessage $= "")
        {
            $UserPref::Player::awayMessage = $Pref::Player::defaultAwayMessage;
        }
        %this.setValue($UserPref::Player::awayMessage);
    }
    else
    {
        $UserPref::Player::awayMessage = %newAwayMsg;
    }
    if (isIdle())
    {
        setIdle(1, $UserPref::Player::awayMessage);
    }
    schedulePersist();
    return ;
}
function VolumeSlider::applySettings(%this)
{
    $UserPref::Audio::masterVolume = VolumeSlider.value;
    %multiplier = $UserPref::Audio::mute ? 0 : 1;
    alxListenerf(AL_GAIN_LINEAR, %multiplier * $UserPref::Audio::masterVolume);
    fmodSetMasterVolume((%multiplier * $UserPref::Audio::masterVolume) * $UserPref::Audio::channelVolume1);
    if (Using_FFMPEG())
    {
        ffmpegSetMasterVolume((%multiplier * $UserPref::Audio::masterVolume) * $UserPref::Audio::channelVolume1);
    }
    schedulePersist();
    return ;
}
function VolumeMusicSlider::applySettings(%this)
{
    $UserPref::Audio::channelVolume1 = %this.value;
    alxSetChannelVolume(1, $UserPref::Audio::channelVolume1);
    %multiplier = $UserPref::Audio::mute ? 0 : 1;
    fmodSetMasterVolume((%multiplier * $UserPref::Audio::masterVolume) * $UserPref::Audio::channelVolume1);
    if (Using_FFMPEG())
    {
        ffmpegSetMasterVolume((%multiplier * $UserPref::Audio::masterVolume) * $UserPref::Audio::channelVolume1);
    }
    schedulePersist();
    return ;
}
function VolumeSFXSlider::applySettings(%this)
{
    $UserPref::Audio::channelVolume2 = VolumeSFXSlider.value;
    alxSetChannelVolume(2, $UserPref::Audio::channelVolume2);
    schedulePersist();
    return ;
}
function TwoPlayerActionsFriendsPopup::onSelect(%this, %id, %unused)
{
    $UserPref::Player::EmotesPermissionFriends = %id;
    if ($UserPref::Player::EmotesPermissionStrangers < %id)
    {
        TwoPlayerActionsStrangersPopup.SetSelected(%id);
    }
    schedulePersist();
    return ;
}
function TwoPlayerActionsStrangersPopup::onSelect(%this, %id, %unused)
{
    $UserPref::Player::EmotesPermissionStrangers = %id;
    if ($UserPref::Player::EmotesPermissionFriends > %id)
    {
        TwoPlayerActionsFriendsPopup.SetSelected(%id);
    }
    schedulePersist();
    return ;
}
function GiftsFriendsPopup::onSelect(%this, %id, %unused)
{
    $UserPref::Player::GiftsPermissionFriends = %id;
    if ($UserPref::Player::GiftsPermissionStrangers < %id)
    {
        GiftsStrangersPopup.SetSelected(%id);
    }
    schedulePersist();
    return ;
}
function GiftsStrangersPopup::onSelect(%this, %id, %unused)
{
    $UserPref::Player::GiftsPermissionStrangers = %id;
    if ($UserPref::Player::GiftsPermissionFriends > %id)
    {
        GiftsFriendsPopup.SetSelected(%id);
    }
    schedulePersist();
    return ;
}
function RenderQualityPopup::onSelect(%this, %id, %unused)
{
    setRenderQuality(%id);
    return ;
}
function ShadowDetailSizePopup::onSelect(%this, %id, %unused)
{
    setShadowDetailSize(%id);
    return ;
}
function SmallTexturesModePopup::onSelect(%this, %id, %unused)
{
    setSmallTextureMode(%id);
    return ;
}
function VisibleDistancePopup::onSelect(%this, %id, %unused)
{
    setVisibleDistanceOption(%id);
    return ;
}
function WaterReflectionModePopup::onSelect(%this, %id, %unused)
{
    setWaterReflection(%id);
    return ;
}
function ExposureFilterModePopup::onSelect(%this, %id, %unused)
{
    setExposureFilter(%id);
    return ;
}
function FontSizePopup::onSelect(%this, %id, %unused)
{
    setShapeNameFontSize(%id);
    return ;
}
function BrightnessSlider::applySettings(%this)
{
    fxEts::updateExposureFilter();
    return ;
}
function setShapeNameFontSize(%val)
{
    if ((%val < 0) && (%val > 2))
    {
        error("Unknown font size:" SPC %val);
        return ;
    }
    $UserPref::Video::shapeNameFontSize = %val;
    if (%val == 0)
    {
        %prof = SmallShapeNameHudProfile;
        %otherProf = BoldSmallShapeNameHudProfile;
    }
    else
    {
        if (%val == 1)
        {
            %prof = MediumShapeNameHudProfile;
            %otherProf = BoldMediumShapeNameHudProfile;
        }
        else
        {
            if (%val == 2)
            {
                %prof = LargeShapeNameHudProfile;
                %otherProf = BoldLargeShapeNameHudProfile;
            }
        }
    }
    TheShapeNameHud.setProfile(%prof);
    TheShapeNameHud.otherProfile = %otherProf;
    return ;
}
setShapeNameFontSize($UserPref::Video::shapeNameFontSize);
function toggleAutoHideButtonBar()
{
    ButtonBar.setAutoHiding(AutoHideButtonBarCheckBox.getValue());
    schedulePersist();
    return ;
}
function updateHudTabsHiding()
{
    $UserPref::HudTabs::AutoOpen["music"] = HudMusicAutoOpenCheckBox.getValue() ;
    $UserPref::HudTabs::AutoClose["music"] = HudMusicAutoCloseCheckBox.getValue() ;
    $UserPref::HudTabs::AutoOpen["affinity"] = HudAffinityAutoOpenCheckBox.getValue() ;
    $UserPref::HudTabs::AutoClose["affinity"] = HudAffinityAutoCloseCheckBox.getValue() ;
    $UserPref::HudTabs::AutoOpen["scores"] = HudScoresAutoOpenCheckBox.getValue() ;
    $UserPref::HudTabs::AutoClose["scores"] = HudScoresAutoCloseCheckBox.getValue() ;
    $UserPref::HudTabs::AutoOpen["word"] = HudWordAutoOpenCheckBox.getValue() ;
    $UserPref::HudTabs::AutoClose["word"] = HudWordAutoCloseCheckBox.getValue() ;
    %i = 0;
    while (%i < HudTabs.numTabs)
    {
        %tab = HudTabs.getTabAtIndex(%i);
        if (!(%tab.name $= "tutorial"))
        {
            %tab.autoHide = $UserPref::HudTabs::AutoClose[%tab.name];
        }
        %i = %i + 1;
    }
    %currentTab = HudTabs.getCurrentTab();
    %tabName = %currentTab $= "" ? "" : %currentTab;
    if ($UserPref::HudTabs::AutoClose[%tabName])
    {
        HudTabs.autoHide();
    }
    return ;
}
function toggleAutoOpenLocalMap()
{
    $UserPref::UI::Radar::AutoOpen = AutoOpenLocalMapCheckBox.getValue();
    HudTabs.autoHide();
    schedulePersist();
    return ;
}
function doAutoReplyToWhispersWhenAway()
{
    schedulePersist();
    return ;
}
function doShowTyping()
{
    gSetField($player, lastPreviewText, "");
    MessageHudEdit.sendPreviewText();
    schedulePersist();
    return ;
}
function doFilterProfanity()
{
    schedulePersist();
    return ;
}
function doShowOnRadar()
{
    commandToServer('setShowOnRadar', $UserPref::Player::showOnRadar);
    schedulePersist();
    return ;
}
function doTeleportBlock()
{
    commandToServer('setTeleportBlock', $UserPref::Player::TeleportBlock);
    schedulePersist();
    return ;
}
function doWhisperBlock(%clearNotify)
{
    commandToServer('setWhisperBlock', $UserPref::Player::WhisperBlock, %clearNotify);
    schedulePersist();
    return ;
}
function sendInitialPrefsToServer()
{
    commandToServer('setGenre', $UserPref::Player::Genre);
    commandToServer('setHeight', $UserPref::Player::height);
    commandToServer('setShowOnRadar', $UserPref::Player::showOnRadar);
    commandToServer('setWhisperBlock', $UserPref::Player::WhisperBlock, 0);
    commandToServer('setTeleportBlock', $UserPref::Player::TeleportBlock);
    return ;
}
function doEditAwayMessage()
{
    OptionsPanel.open();
    OptionsPanelTabs.selectTabWithName("social");
    DefaultAwayMsgEdit.makeFirstResponder(1);
    DefaultAwayMsgEdit.selectAll();
    return ;
}
