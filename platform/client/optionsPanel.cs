$OptionsPanel::scheduledPersistID = 0;
if (!isObject(OptionsPanelTabs))
{
    new ScriptObject(OptionsPanelTabs) {
        class = "TabControl";
    };
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
}
function OptionsPanelTabs::fillTabs(%this)
{
    %i = 0;
    while (%i < %this.numTabs)
    {
        %tab = %this.tabs[%i];
        %tab.setProfile(ETSNonModalProfile);
        %tab.clear();
        %tab.add(new GuiBitmapCtrl("") {
            profile = "ETSNonModalProfile";
            horizSizing = "right";
            vertSizing = "bottom";
            position = "0 0";
            extent = "7 239";
            minExtent = "1 1";
            sluggishness = -1;
            visible = 1;
            bitmap = "./ui/settings_bracket_left";
        };);
        %tab.add(new GuiBitmapCtrl("") {
            profile = "ETSNonModalProfile";
            horizSizing = "right";
            vertSizing = "bottom";
            position = "375 0";
            extent = "7 239";
            minExtent = "1 1";
            sluggishness = -1;
            visible = 1;
            bitmap = "./ui/settings_bracket_right";
        };);
        %i = %i + 1;
    }
    %this.fillSocialTab();
    %this.fillAudioTab();
    %this.fillVisualTab();
    %this.fillTabsTab();
    %this.fillVIPTab();
}
function OptionsPanelTabs::tabSelected(%this, %tab)
{
    if (%tab.hasFieldValue("initialFirstResponder") && isObject(%tab.initialFirstResponder))
    {
        %tab.initialFirstResponder.makeFirstResponder(1);
    }
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
    %tab.add(new GuiTextCtrl("") {
        profile = "ETSShadowTextProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = %posX @ " " @ %posY;
        extent = "117 18";
        minExtent = "8 2";
        sluggishness = -1;
        visible = 1;
        text = "Away Message:";
        maxLength = 255;
    };);
    %posY = %posY + %dPosY;
    %posX = %posX + %indent1;
    %tab.add(new GuiTextEditCtrl(DefaultAwayMsgEdit) {
        profile = "ETSDarkTextEditProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = %posX @ " " @ %posY;
        extent = "310 16";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        altCommand = "DefaultAwayMsgEdit.applySettings();";
        validate = "DefaultAwayMsgEdit.applySettings();";
        maxLength = 40;
        historySize = 0;
        password = 0;
        tabComplete = 0;
        sinkAllKeyEvents = 0;
        cursorType = 1;
    };);
    %posY = %posY + %dPosY;
    %tab.add(new GuiVariableWidthButtonCtrl("") {
        profile = "BracketButton15Profile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = %posX @ " " @ %posY;
        extent = "51 15";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        command = "DefaultAwayMsgEdit.applySettings();";
        text = "Save";
        groupNum = -1;
        buttonType = "PushButton";
    };);
    %tab.add(new GuiVariableWidthButtonCtrl("") {
        profile = "BracketButton15Profile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = (%posX + 60) @ " " @ %posY;
        extent = "51 15";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        command = "awayOperation();";
        text = "Go Idle";
        groupNum = -1;
        buttonType = "PushButton";
    };);
    %tab.add(new GuiCheckBoxCtrl(AutoReplyToWhispersCheckBox) {
        profile = "ETSCheckBoxProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = (%posX + 120) @ " " @ (%posY - 1);
        extent = "200 18";
        minExtent = "8 2";
        sluggishness = -1;
        visible = 1;
        variable = "$UserPref::Player::autoReplyToWhispersWhenAway";
        command = "doAutoReplyToWhispersWhenAway();";
        text = "Auto-reply to whispers when away";
        groupNum = -1;
        buttonType = "ToggleButton";
    };);
    %posY = %posY + %dPosY;
    %posY = %posY + %dPosYSmall;
    %posX = %posX - %indent1;
    %tab.add(new GuiTextCtrl("") {
        position = %posX @ " " @ %posY;
        extent = "94 18";
        profile = "ETSShadowTextProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        minExtent = "8 2";
        sluggishness = -1;
        visible = 1;
        text = "General Options:";
        maxLength = 255;
    };);
    %posY = %posY + %dPosY;
    %posX = %posX + %indent1;
    %text = "Show My Typing";
    %tab.add(new GuiCheckBoxCtrl(ShowTypingCheckBox) {
        profile = "ETSCheckBoxProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = %posX @ " " @ %posY;
        extent = "111 18";
        minExtent = "8 2";
        sluggishness = -1;
        visible = 1;
        variable = "$UserPref::Chat::ShowTyping";
        command = "doShowTyping();";
        text = %text;
        groupNum = -1;
        buttonType = "ToggleButton";
        tooltip = "Show my words above my head as I type";
    };);
    %text = "Refuse Teleports";
    %tab.add(new GuiCheckBoxCtrl(TeleportBlockCheckBox) {
        profile = "ETSCheckBoxProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = (%posX + %indent2) @ " " @ %posY;
        extent = "111 18";
        minExtent = "8 2";
        sluggishness = -1;
        visible = 1;
        variable = "$UserPref::Player::TeleportBlock";
        command = "doTeleportBlock();";
        text = %text;
        groupNum = -1;
        buttonType = "ToggleButton";
        tooltip = "Don't allow people to teleport to me";
    };);
    %posY = %posY + %dPosY;
    %text = "Filter Profanity";
    %tab.add(new GuiCheckBoxCtrl(FilterProfanityCheckBox) {
        profile = "ETSCheckBoxProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = %posX @ " " @ %posY;
        extent = "111 18";
        minExtent = "8 2";
        sluggishness = -1;
        visible = 1;
        variable = "$UserPref::Player::filterProfanity";
        command = "doFilterProfanity();";
        text = %text;
        groupNum = -1;
        buttonType = "ToggleButton";
        tooltip = "Replace dirty language with !@*&^#";
    };);
    %text = "Refuse Whispers";
    %tab.add(new GuiCheckBoxCtrl(WhisperBlockCheckBox) {
        profile = "ETSCheckBoxProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = (%posX + %indent2) @ " " @ %posY;
        extent = "111 18";
        minExtent = "8 2";
        sluggishness = -1;
        visible = 1;
        variable = "$UserPref::Player::WhisperBlock";
        command = "doWhisperBlock(true);";
        text = %text;
        groupNum = -1;
        buttonType = "ToggleButton";
        tooltip = "Don't allow people to whisper to me";
    };);
    %posY = %posY + %dPosY;
    %text = "Show Me on Radar";
    %tab.add(new GuiCheckBoxCtrl(ShowOnRadarCheckBox) {
        profile = "ETSCheckBoxProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = %posX @ " " @ %posY;
        extent = "111 18";
        minExtent = "8 2";
        sluggishness = -1;
        visible = 1;
        variable = "$UserPref::Player::showOnRadar";
        command = "doShowOnRadar();";
        text = %text;
        groupNum = -1;
        buttonType = "ToggleButton";
        tooltip = "Be visible to others on the radar.";
    };);
    %text = "Refuse Yells";
    %tab.add(new GuiCheckBoxCtrl(YellBlockCheckBox) {
        profile = "ETSCheckBoxProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = (%posX + %indent2) @ " " @ %posY;
        extent = "111 18";
        minExtent = "8 2";
        sluggishness = -1;
        visible = 1;
        variable = "$UserPref::Player::YellBlock";
        command = "schedulePersist();";
        text = %text;
        groupNum = -1;
        buttonType = "ToggleButton";
        tooltip = "Don't show yells in the chat bubble";
    };);
    %posY = %posY + %dPosY;
    %posY = %posY + %dPosYSmall;
    %posX = %posX - %indent1;
    %tab.add(new GuiTextCtrl("") {
        position = %posX @ " " @ %posY;
        extent = "94 18";
        profile = "ETSShadowTextProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        minExtent = "8 2";
        sluggishness = -1;
        visible = 1;
        text = "Two-Player Actions:";
        maxLength = 255;
    };);
    %posX = %posX + 120;
    %tab.add(new GuiTextCtrl("") {
        position = %posX @ " " @ %posY;
        extent = "94 18";
        profile = "ETSShadowTextProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        minExtent = "8 2";
        sluggishness = -1;
        visible = 1;
        text = "Friends:";
        maxLength = 255;
    };);
    %tab.add(new GuiPopUpMenuCtrl(TwoPlayerActionsFriendsPopup) {
        profile = "ETSDarkPopUpMenuProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = (%posX + 42) @ " " @ %posY;
        extent = "54 16";
        minExtent = "8 2";
        sluggishness = -1;
        visible = 1;
        maxLength = 255;
        maxPopupHeight = 200;
        tooltip = "Choose what happens when a friend does a two-player action with me";
    };);
    %posX = %posX + 120;
    %tab.add(new GuiTextCtrl("") {
        position = %posX @ " " @ %posY;
        extent = "94 18";
        profile = "ETSShadowTextProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        minExtent = "8 2";
        sluggishness = -1;
        visible = 1;
        text = "Non-Friends:";
        maxLength = 255;
    };);
    %tab.add(new GuiPopUpMenuCtrl(TwoPlayerActionsStrangersPopup) {
        profile = "ETSDarkPopUpMenuProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = (%posX + 65) @ " " @ %posY;
        extent = "54 16";
        minExtent = "8 2";
        sluggishness = -1;
        visible = 1;
        maxLength = 255;
        maxPopupHeight = 200;
        tooltip = "Choose what happens when a stranger does a two-player action with me";
    };);
    %posX = %posX - 240;
    %posY = %posY + %dPosY;
    %posY = %posY + 5;
    %tab.add(new GuiTextCtrl("") {
        position = %posX @ " " @ %posY;
        extent = "94 18";
        profile = "ETSShadowTextProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        minExtent = "8 2";
        sluggishness = -1;
        visible = 1;
        text = "Gifts:";
        maxLength = 255;
    };);
    %posX = %posX + 120;
    %tab.add(new GuiTextCtrl("") {
        position = %posX @ " " @ %posY;
        extent = "94 18";
        profile = "ETSShadowTextProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        minExtent = "8 2";
        sluggishness = -1;
        visible = 1;
        text = "Friends:";
        maxLength = 255;
    };);
    %tab.add(new GuiPopUpMenuCtrl(GiftsFriendsPopup) {
        profile = "ETSDarkPopUpMenuProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = (%posX + 42) @ " " @ %posY;
        extent = "54 16";
        minExtent = "8 2";
        sluggishness = -1;
        visible = 1;
        maxLength = 255;
        maxPopupHeight = 200;
        tooltip = "Choose what happens when a friend wants to give me vBux or vPoints";
    };);
    %posX = %posX + 120;
    %tab.add(new GuiTextCtrl("") {
        position = %posX @ " " @ %posY;
        extent = "94 18";
        profile = "ETSShadowTextProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        minExtent = "8 2";
        sluggishness = -1;
        visible = 1;
        text = "Non-Friends:";
        maxLength = 255;
    };);
    %tab.add(new GuiPopUpMenuCtrl(GiftsStrangersPopup) {
        profile = "ETSDarkPopUpMenuProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = (%posX + 65) @ " " @ %posY;
        extent = "54 16";
        minExtent = "8 2";
        sluggishness = -1;
        visible = 1;
        maxLength = 255;
        maxPopupHeight = 200;
        tooltip = "Choose what happens when a stranger wants to give me vBux or vPoints";
    };);
    %posX = %posX - 240;
    %posY = %posY + %dPosY;
    %posX = %posX - %indent1;
    %tab.add(new GuiVariableWidthButtonCtrl("") {
        profile = "BracketButton15Profile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = %originX @ " " @ 215;
        extent = "105 18";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        command = "OptionsPanelTabs.restoreSocialDefaults();";
        text = "Restore Defaults";
        groupNum = -1;
        buttonType = "PushButton";
    };);
    %tab.initialFirstResponder = DefaultAwayMsgEdit;
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
    %tab.add(new GuiTextCtrl("") {
        profile = "ETSRightJustifiedShadowTextProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = %posX @ " " @ %posY;
        extent = "39 18";
        minExtent = "50 1";
        sluggishness = -1;
        visible = 1;
        text = "Volume:";
        maxLength = 255;
    };);
    %tab.add(new GuiSliderCtrl(VolumeSlider) {
        profile = "ETSSliderProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = (%posX + 55) @ " " @ %posY;
        extent = "106 21";
        minExtent = "8 2";
        sluggishness = -1;
        visible = 1;
        altCommand = "VolumeSlider.applySettings();";
        range = "0.000000 1.000000";
        ticks = 10;
        value = 1;
    };);
    %posY = %posY + %dPosY;
    %tab.add(new GuiTextCtrl("") {
        profile = "ETSRightJustifiedShadowTextProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = %posX @ " " @ %posY;
        extent = "31 18";
        minExtent = "50 1";
        sluggishness = -1;
        visible = 1;
        text = "Music:";
        maxLength = 255;
    };);
    %tab.add(new GuiSliderCtrl(VolumeMusicSlider) {
        profile = "ETSSliderProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = (%posX + 55) @ " " @ %posY;
        extent = "106 21";
        minExtent = "8 2";
        sluggishness = -1;
        visible = 1;
        variable = 0;
        altCommand = "VolumeMusicSlider.applySettings();";
        range = "0.000000 1.000000";
        ticks = 10;
        value = 0;
    };);
    %posY = %posY + %dPosY;
    %tab.add(new GuiTextCtrl("") {
        profile = "ETSRightJustifiedShadowTextProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = %posX @ " " @ %posY;
        extent = "47 18";
        minExtent = "50 1";
        sluggishness = -1;
        visible = 1;
        text = "SoundFX:";
        maxLength = 255;
    };);
    %tab.add(new GuiSliderCtrl(VolumeSFXSlider) {
        profile = "ETSSliderProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = (%posX + 55) @ " " @ %posY;
        extent = "106 21";
        minExtent = "8 2";
        sluggishness = -1;
        visible = 1;
        variable = 0;
        altCommand = "VolumeSFXSlider.applySettings();";
        range = "0.000000 1.000000";
        ticks = 10;
        value = 0.897959;
    };);
    %posY = %posY + %dPosY;
    %posX = %posX + %indent;
    %tab.add(new GuiCheckBoxCtrl(MuteCheckBox) {
        profile = "ETSCheckBoxProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = %posX @ " " @ %posY;
        extent = "40 18";
        minExtent = "8 2";
        sluggishness = -1;
        visible = 1;
        command = "Music::toggleMute();";
        text = "Mute";
        groupNum = -1;
        buttonType = "ToggleButton";
    };);
    %posY = %posY + %dPosY;
    %posX = %posX - %indent;
    %posY = %posY + %dPosY;
    %tab.add(new GuiTextCtrl("") {
        profile = "ETSShadowTextProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = %posX @ " " @ %posY;
        extent = "50 18";
        minExtent = "50 1";
        sluggishness = -1;
        visible = 1;
        text = "Play Sound On:";
        maxLength = 255;
    };);
    %posY = %posY + %dPosY;
    %posX = %posX + %indent;
    %tab.add(new GuiCheckBoxCtrl(NotifyChatCheckBox) {
        profile = "ETSCheckBoxProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = %posX @ " " @ %posY;
        extent = "100 18";
        minExtent = "8 2";
        sluggishness = -1;
        visible = 1;
        variable = "$UserPref::Audio::NotifyChat";
        command = "schedulePersist();";
        text = "Incoming Chat";
        groupNum = -1;
        buttonType = "ToggleButton";
    };);
    %posY = %posY + %dPosY;
    %tab.add(new GuiCheckBoxCtrl(NotifyWhisperCheckBox) {
        profile = "ETSCheckBoxProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = %posX @ " " @ %posY;
        extent = "100 18";
        minExtent = "8 2";
        sluggishness = -1;
        visible = 1;
        variable = "$UserPref::Audio::NotifyWhisper";
        command = "schedulePersist();";
        text = "Incoming Whisper";
        groupNum = -1;
        buttonType = "ToggleButton";
    };);
    %posY = %posY + %dPosY;
    %posX = %posX - %indent;
    %tab.add(new GuiVariableWidthButtonCtrl("") {
        profile = "BracketButton15Profile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = %originX @ " " @ 215;
        extent = "105 18";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        command = "OptionsPanelTabs.restoreAudioDefaults();";
        text = "Restore Defaults";
        groupNum = -1;
        buttonType = "PushButton";
    };);
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
    %tab.add(new GuiTextCtrl("") {
        profile = "ETSShadowTextProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = %posX @ " " @ %posY;
        extent = "85 18";
        minExtent = "8 2";
        sluggishness = -1;
        visible = 1;
        text = " Rendering Quality:";
        maxLength = 255;
    };);
    %tab.add(new GuiPopUpMenuCtrl(RenderQualityPopup) {
        profile = "ETSDarkPopUpMenuProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = (%posX + 96) @ " " @ %posY;
        extent = "63 16";
        minExtent = "8 2";
        sluggishness = -1;
        visible = 1;
        maxLength = 255;
        maxPopupHeight = 200;
    };);
    %posY = %posY + %dPosY;
    %tab.add(new GuiTextCtrl("") {
        profile = "ETSShadowTextProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = %posX @ " " @ %posY;
        extent = "85 18";
        minExtent = "8 2";
        sluggishness = -1;
        visible = 1;
        text = " Shadow Detail:";
        maxLength = 255;
    };);
    %tab.add(new GuiPopUpMenuCtrl(ShadowDetailSizePopup) {
        profile = "ETSDarkPopUpMenuProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = (%posX + 96) @ " " @ %posY;
        extent = "63 16";
        minExtent = "8 2";
        sluggishness = -1;
        visible = 1;
        maxLength = 255;
        maxPopupHeight = 200;
    };);
    %posY = %posY + %dPosY;
    %tab.add(new GuiTextCtrl("") {
        profile = "ETSShadowTextProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = %posX @ " " @ %posY;
        extent = "85 18";
        minExtent = "8 2";
        sluggishness = -1;
        visible = 1;
        text = " Visible Distance:";
        maxLength = 255;
    };);
    %tab.add(new GuiPopUpMenuCtrl(VisibleDistancePopup) {
        profile = "ETSDarkPopUpMenuProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = (%posX + 96) @ " " @ %posY;
        extent = "63 16";
        minExtent = "8 2";
        sluggishness = -1;
        visible = 1;
        maxLength = 255;
        maxPopupHeight = 200;
    };);
    %posY = %posY + %dPosY;
    %tab.add(new GuiTextCtrl("") {
        profile = "ETSShadowTextProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = %posX @ " " @ %posY;
        extent = "85 18";
        minExtent = "8 2";
        sluggishness = -1;
        visible = 1;
        text = " Water Reflection:";
        maxLength = 255;
    };);
    %tab.add(new GuiPopUpMenuCtrl(WaterReflectionModePopup) {
        profile = "ETSDarkPopUpMenuProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = (%posX + 96) @ " " @ %posY;
        extent = "63 16";
        minExtent = "8 2";
        sluggishness = -1;
        visible = 1;
        maxLength = 255;
        maxPopupHeight = 200;
    };);
    %posY = %posY + %dPosY;
    %tab.add(new GuiTextCtrl("") {
        profile = "ETSShadowTextProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = %posX @ " " @ %posY;
        extent = "85 18";
        minExtent = "8 2";
        sluggishness = -1;
        visible = 1;
        text = " Brightness Filter:";
        maxLength = 255;
    };);
    %tab.add(new GuiPopUpMenuCtrl(ExposureFilterModePopup) {
        profile = "ETSDarkPopUpMenuProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = (%posX + 96) @ " " @ %posY;
        extent = "63 16";
        minExtent = "8 2";
        sluggishness = -1;
        visible = 1;
        maxLength = 255;
        maxPopupHeight = 200;
    };);
    %posY = %posY + %dPosY;
    %posX = %posX + %indent;
    %tab.add(new GuiTextCtrl(BrightnessLabel) {
        profile = "ETSShadowTextProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = %posX @ " " @ %posY;
        extent = "31 18";
        minExtent = "8 2";
        sluggishness = -1;
        visible = OptionsPanel.showBrightnessControls;
        text = "Brightness:";
        maxLength = 255;
    };);
    %tab.add(new GuiSliderCtrl(BrightnessSlider) {
        profile = "ETSSliderProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = (%posX + 55) @ " " @ %posY;
        extent = "97 21";
        minExtent = "8 2";
        sluggishness = -1;
        visible = OptionsPanel.showBrightnessControls;
        variable = "$UserPref::Video::Exposure";
        altCommand = "BrightnessSlider.applySettings();";
        range = "0.200000 1.0";
        ticks = 10;
        value = $UserPref::Video::Exposure;
        defaultValues = 0.5;
        snapToDefaultRangeRatio = 0.1;
        displayValue = 0;
    };);
    %posY = %posY + (2 * %dPosY);
    %posX = %posX - %indent;
    %tab.add(new GuiCheckBoxCtrl(HUDShowNamesCheckBox) {
        profile = "ETSCheckBoxProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = %posX @ " " @ %posY;
        extent = "50 18";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        variable = "";
        command = "OptionsPanelTabs.onChangedShowNames();";
        text = "Names";
        groupNum = -1;
        buttonType = "ToggleButton";
    };);
    %tab.add(new GuiMLTextCtrl("") {
        profile = "InfoTextProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = (%posX + 60) @ " " @ (%posY + 2);
        extent = "305 42";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        lineSpacing = 0;
        allowColorChars = 1;
        maxChars = -1;
        text = "Show people's names above their heads";
    };);
    %posY = %posY + %dPosY;
    %posX = %posX + %indent;
    %tab.add(new GuiTextCtrl("") {
        profile = "ETSShadowTextProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = %posX @ " " @ %posY;
        extent = "48 18";
        minExtent = "8 2";
        sluggishness = -1;
        visible = 1;
        text = "Name Size:";
        maxLength = 255;
    };);
    %tab.add(new GuiPopUpMenuCtrl(FontSizePopup) {
        profile = "ETSDarkPopUpMenuProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = (%posX + 60) @ " " @ %posY;
        extent = "53 16";
        minExtent = "8 2";
        sluggishness = -1;
        visible = 1;
        maxLength = 255;
        maxPopupHeight = 200;
    };);
    %posY = %posY + (2 * %dPosY);
    %posX = %posX - %indent;
    %posX = %posX + %dPosX;
    %posY = %originY - 12;
    %tab.add(new GuiCheckBoxCtrl(AutoHideButtonBarCheckBox) {
        profile = "ETSCheckBoxProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = %posX @ " " @ %posY;
        extent = "119 18";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        variable = "";
        command = "toggleAutoHideButtonBar();";
        text = "Auto Hide Button Bar";
        groupNum = -1;
        buttonType = "ToggleButton";
    };);
    AutoHideButtonBarCheckBox.setValue($UserPref::ETS::ButtonBar::AutoHide);
    %posY = %posY + %dPosY;
    %tab.add(new GuiCheckBoxCtrl(AutoOpenLocalMapCheckBox) {
        profile = "ETSCheckBoxProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = %posX @ " " @ %posY;
        extent = "120 18";
        minExtent = "8 2";
        sluggishness = -1;
        visible = 1;
        variable = "$UserPref::UI::Radar::AutoOpen";
        text = "Auto-Open Radar";
        groupNum = -1;
        buttonType = "ToggleButton";
        command = "toggleAutoOpenLocalMap();";
    };);
    AutoOpenLocalMapCheckBox.setValue($UserPref::UI::Radar::AutoOpen);
    %posY = %posY + %dPosY;
    %tab.add(new GuiCheckBoxCtrl(DisplayFlashTaskBarCheckBox) {
        profile = "ETSCheckBoxProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = %posX @ " " @ %posY;
        extent = "94 18";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        variable = "$UserPref::UI::FlashTaskBar";
        command = "";
        text = "Flash Task Bar";
        groupNum = -1;
        buttonType = "ToggleButton";
    };);
    %posY = %posY + %dPosY;
    %tab.add(new GuiCheckBoxCtrl(ShowAccountHudCheckBox) {
        profile = "ETSCheckBoxProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = %posX @ " " @ %posY;
        extent = "126 18";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        variable = "$UserPref::UI::ShowAccountHud";
        command = "AccountBalanceHud.update();";
        text = "Show Accounts Panel";
        groupNum = -1;
        buttonType = "ToggleButton";
    };);
    %posY = %posY + %dPosY;
    %tab.add(new GuiCheckBoxCtrl(ConstrainWindowDimensions) {
        profile = "ETSCheckBoxProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = %posX @ " " @ %posY;
        extent = "150 18";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        variable = "$UserPref::Video::ConstrainWindowDimensions";
        command = "tryStandardizeScreenAspect();";
        text = "Lock Window Proportions";
        groupNum = -1;
        buttonType = "ToggleButton";
    };);
    %posY = %posY + %dPosY;
    %tab.add(new GuiCheckBoxCtrl(OptionsVisualShowTooltipsCheckbox) {
        profile = "ETSCheckBoxProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = %posX @ " " @ %posY;
        extent = "126 18";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        variable = "$UserPref::UI::ShowTooltips";
        command = "";
        text = "Show Tooltips";
        groupNum = -1;
        buttonType = "ToggleButton";
    };);
    %posY = %posY + %dPosY;
    %tab.add(new GuiCheckBoxCtrl(OptionsVisualShowReloadTexturesCheckbox) {
        profile = "ETSCheckBoxProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = %posX @ " " @ %posY;
        extent = "126 18";
        variable = "$UserPref::UI::ShowReloadTextures";
        command = "changedShowReloadTextures();";
        text = "Show Reload Textures";
        buttonType = "ToggleButton";
    };);
    %posY = %posY + %dPosY;
    %tab.add(new GuiCheckBoxCtrl(DisableVideosCheckBox) {
        profile = "ETSCheckBoxProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = %posX @ " " @ %posY;
        extent = "126 18";
        text = "Disable Videos";
        buttonType = "ToggleButton";
        variable = "$UserPref::ETS::VideoRenderer::Disable";
        tooltip = "Only check this if you are having trouble with videos";
    };);
    %posY = %posY + %dPosY;
    %tab.add(new GuiVariableWidthButtonCtrl("") {
        profile = "BracketButton15Profile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = %originX @ " " @ 215;
        extent = "105 18";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        command = "OptionsPanelTabs.restoreVisualDefaults();";
        text = "Restore Defaults";
        groupNum = -1;
        buttonType = "PushButton";
    };);
    %tab.initialFirstResponder = RenderQualityPopup;
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
    %tab.add(new GuiMLTextCtrl("") {
        profile = "GuiDefaultProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = (%posX + %indent2) @ " " @ %posY;
        extent = "50 42";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        lineSpacing = 0;
        allowColorChars = 1;
        maxChars = -1;
        text = "<color:ffffff>Auto Open";
    };);
    %tab.add(new GuiMLTextCtrl("") {
        profile = "GuiDefaultProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = (%posX + %indent3) @ " " @ %posY;
        extent = "50 42";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        lineSpacing = 0;
        allowColorChars = 1;
        maxChars = -1;
        text = "<color:ffffff>Auto Close";
    };);
    %posY = %posY + %dPosY;
    %tab.add(new GuiBitmapCtrl("") {
        profile = "ETSNonModalProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "-5 -5";
        extent = "46 39";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        bitmap = "./buttons/hud_music_n";
    };, new GuiControl("") {
        position = %posX @ " " @ %posY;
        extent = "36 29";
        visible = 1;
    };);
    %tab.add(new GuiMLTextCtrl("") {
        profile = "GuiDefaultProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = (%posX + %indent1) @ " " @ (%posY + 9);
        extent = "50 14";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        lineSpacing = 0;
        allowColorChars = 1;
        maxChars = -1;
        text = "<color:ffffff> Music";
    };);
    %tab.add(new GuiCheckBoxCtrl(HudMusicAutoOpenCheckBox) {
        profile = "ETSCheckBoxProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = ((%posX + %indent2) + 6) @ " " @ (%posY + 8);
        extent = "18 18";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        variable = "";
        command = "updateHudTabsHiding();";
        text = "";
        groupNum = -1;
        buttonType = "ToggleButton";
    };);
    %tab.add(new GuiCheckBoxCtrl(HudMusicAutoCloseCheckBox) {
        profile = "ETSCheckBoxProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = ((%posX + %indent3) + 6) @ " " @ (%posY + 8);
        extent = "18 18";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        variable = "";
        command = "updateHudTabsHiding();";
        text = "";
        groupNum = -1;
        buttonType = "ToggleButton";
    };);
    %posY = %posY + %dPosY;
    %tab.add(new GuiBitmapCtrl("") {
        profile = "ETSNonModalProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "-5 -5";
        extent = "46 39";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        bitmap = "./buttons/hud_affinity_n";
    };, new GuiControl("") {
        position = %posX @ " " @ %posY;
        extent = "36 29";
        visible = 1;
    };);
    %tab.add(new GuiMLTextCtrl("") {
        profile = "GuiDefaultProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = (%posX + %indent1) @ " " @ (%posY + 9);
        extent = "50 14";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        lineSpacing = 0;
        allowColorChars = 1;
        maxChars = -1;
        text = "<color:ffffff> Affinity";
    };);
    %tab.add(new GuiCheckBoxCtrl(HudAffinityAutoOpenCheckBox) {
        profile = "ETSCheckBoxProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = ((%posX + %indent2) + 6) @ " " @ (%posY + 8);
        extent = "18 18";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        variable = "";
        command = "updateHudTabsHiding();";
        text = "";
        groupNum = -1;
        buttonType = "ToggleButton";
    };);
    %tab.add(new GuiCheckBoxCtrl(HudAffinityAutoCloseCheckBox) {
        profile = "ETSCheckBoxProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = ((%posX + %indent3) + 6) @ " " @ (%posY + 8);
        extent = "18 18";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        variable = "";
        command = "updateHudTabsHiding();";
        text = "";
        groupNum = -1;
        buttonType = "ToggleButton";
    };);
    %posY = %posY + %dPosY;
    %tab.add(new GuiBitmapCtrl("") {
        profile = "ETSNonModalProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "-5 -5";
        extent = "46 39";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        bitmap = "./buttons/hud_scores_n";
    };, new GuiControl("") {
        position = %posX @ " " @ %posY;
        extent = "36 29";
        visible = 1;
    };);
    %tab.add(new GuiMLTextCtrl("") {
        profile = "GuiDefaultProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = (%posX + %indent1) @ " " @ (%posY + 9);
        extent = "50 14";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        lineSpacing = 0;
        allowColorChars = 1;
        maxChars = -1;
        text = "<color:ffffff> Scores";
    };);
    %tab.add(new GuiCheckBoxCtrl(HudScoresAutoOpenCheckBox) {
        profile = "ETSCheckBoxProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = ((%posX + %indent2) + 6) @ " " @ (%posY + 8);
        extent = "18 18";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 0;
        variable = "";
        command = "updateHudTabsHiding();";
        text = "";
        groupNum = -1;
        buttonType = "ToggleButton";
    };);
    %tab.add(new GuiCheckBoxCtrl(HudScoresAutoCloseCheckBox) {
        profile = "ETSCheckBoxProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = ((%posX + %indent3) + 6) @ " " @ (%posY + 8);
        extent = "18 18";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        variable = "";
        command = "updateHudTabsHiding();";
        text = "";
        groupNum = -1;
        buttonType = "ToggleButton";
    };);
    %posY = %posY + %dPosY;
    %tab.add(new GuiBitmapCtrl("") {
        profile = "ETSNonModalProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "-5 -5";
        extent = "46 39";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        bitmap = "./buttons/hud_word_n";
    };, new GuiControl("") {
        position = %posX @ " " @ %posY;
        extent = "36 29";
        visible = 1;
    };);
    %tab.add(new GuiMLTextCtrl("") {
        profile = "GuiDefaultProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = (%posX + %indent1) @ " " @ (%posY + 9);
        extent = "50 14";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        lineSpacing = 0;
        allowColorChars = 1;
        maxChars = -1;
        text = "<color:ffffff> Word";
    };);
    %tab.add(new GuiCheckBoxCtrl(HudWordAutoOpenCheckBox) {
        profile = "ETSCheckBoxProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = ((%posX + %indent2) + 6) @ " " @ (%posY + 8);
        extent = "18 18";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        variable = "";
        command = "updateHudTabsHiding();";
        text = "";
        groupNum = -1;
        buttonType = "ToggleButton";
    };);
    %tab.add(new GuiCheckBoxCtrl(HudWordAutoCloseCheckBox) {
        profile = "ETSCheckBoxProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = ((%posX + %indent3) + 6) @ " " @ (%posY + 8);
        extent = "18 18";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        variable = "";
        command = "updateHudTabsHiding();";
        text = "";
        groupNum = -1;
        buttonType = "ToggleButton";
    };);
    %posY = %posY + %dPosY;
    %posX = %posX + %dPosX;
    %posY = %originY;
    %posY = %posY + %dPosY;
    %tab.add(new GuiMLTextCtrl("") {
        profile = "InfoTextProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = %posX @ " " @ %posY;
        extent = "150 42";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        lineSpacing = 0;
        allowColorChars = 1;
        maxChars = -1;
        text = "Auto Open:<br>Tab will open by itself when new information comes in.  Note that the Scores tab never auto-opens.<br><br>Auto Close:<br>Tab will close by itself after it's been open for a few seconds.";
    };);
    %posY = %posY + %dPosY;
    %tab.add(new GuiVariableWidthButtonCtrl("") {
        profile = "BracketButton15Profile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = %originX @ " " @ 215;
        extent = "105 18";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        command = "OptionsPanelTabs.restoreTabsDefaults();";
        text = "Restore Defaults";
        groupNum = -1;
        buttonType = "PushButton";
    };);
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
    %tab.add(new GuiMLTextCtrl("") {
        profile = "InfoTextProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = %posX @ " " @ %posY;
        extent = "255 42";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        lineSpacing = 0;
        allowColorChars = 1;
        maxChars = -1;
        text = "<spush><b>Note:<spop> Only VIPs and staff have these settings.";
    };);
    %posY = %posY + %dPosY;
    %tab.add(new GuiControl(ForceFieldCtrls) {
        position = %posX @ " " @ %posY;
        extent = 94 @ " " @ (%dPosY * 5);
        visible = $gPerformerMode;
    };);
    %posY = %posY + (6 * %dPosY);
    new GuiRadioCtrl(performerPanelRadioButtonForceField3) {
        profile = new GuiRadioCtrl(performerPanelRadioButtonForceField2) {
        profile = new GuiRadioCtrl(performerPanelRadioButtonForceField1) {
        profile = new GuiRadioCtrl(performerPanelRadioButtonForceField0) {
        profile = new GuiTextCtrl("") {
        position = 0 @ " " @ (%dPosY * 0);
        extent = "94 18";
        profile = "ETSShadowTextProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        minExtent = "8 2";
        sluggishness = -1;
        text = "Force Field:";
        maxLength = 255;
    }; @ "ETSCheckBoxProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = 10 @ " " @ (%dPosY * 1);
        extent = "84 18";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        command = "performerClient::SetForceField(0);";
        text = "Off";
        groupNum = 1;
        buttonType = "RadioButton";
        depressed = 0;
        mouseOver = 0;
    }; @ "ETSCheckBoxProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = 10 @ " " @ (%dPosY * 2);
        extent = "84 18";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        command = "performerClient::SetForceField(1);";
        text = "Small";
        groupNum = 1;
        buttonType = "RadioButton";
        depressed = 0;
        mouseOver = 0;
    }; @ "ETSCheckBoxProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = 10 @ " " @ (%dPosY * 3);
        extent = "84 18";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        command = "performerClient::SetForceField(2);";
        text = "Medium";
        groupNum = 1;
        buttonType = "RadioButton";
        depressed = 0;
        mouseOver = 0;
    }; @ "ETSCheckBoxProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = 10 @ " " @ (%dPosY * 4);
        extent = "84 18";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        command = "performerClient::SetForceField(3);";
        text = "Grande";
        groupNum = 1;
        buttonType = "RadioButton";
        depressed = 0;
        mouseOver = 0;
    };
    performerPanelRadioButtonForceField0.setValue(1);
    %tab.add(new GuiCheckBoxCtrl(HUDHideChatCheckBox) {
        profile = "ETSCheckBoxProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = %posX @ " " @ %posY;
        extent = "120 18";
        minExtent = "8 2";
        sluggishness = -1;
        visible = 0;
        variable = "$UserPref::Display::hideChat";
        command = "OptionsPanelTabs.onChangedHideChat();";
        text = "Hide Chat";
        groupNum = -1;
        buttonType = "ToggleButton";
    };);
    %posY = %posY + %dPosY;
    %tab.add(new GuiCheckBoxCtrl(geDFDebugMode) {
        profile = "ETSCheckBoxProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = %posX @ " " @ %posY;
        extent = "150 18";
        minExtent = "8 2";
        sluggishness = -1;
        variable = "$Pref::DF::debugMode";
        command = "OptionsPanelTabs.onChangedDFDebugMode();";
        text = "Double Fusion Debug Mode";
        groupNum = -1;
        buttonType = "ToggleButton";
    };);
    %tab.add(new GuiCheckBoxCtrl(geDFShowDefaults) {
        profile = "ETSCheckBoxProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = (%posX + 150) @ " " @ %posY;
        extent = "150 18";
        minExtent = "8 2";
        sluggishness = -1;
        variable = "$Pref::DF::ShowDefaults";
        command = "OptionsPanelTabs.onChangedDFShowDefaults();";
        text = "Show Default Ads";
        groupNum = -1;
        buttonType = "ToggleButton";
    };);
    %posY = %posY + %dPosY;
    %posX = %posX + %indent;
    %tab.add(new GuiControl(geDFDebugCtrls) {
        horizSizing = "right";
        vertSizing = "bottom";
        position = %posX @ " " @ %posY;
        extent = "300 20";
        visible = $Pref::DF::debugMode;
    };);
    %posY = %posY + %dPosY;
    new GuiTextCtrl(geDFDebugStatusText) {
        profile = new GuiVariableWidthButtonCtrl("") {
        profile = new GuiVariableWidthButtonCtrl("") {
        profile = new GuiVariableWidthButtonCtrl("") {
        profile = "BracketButton13Profile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "0 0";
        extent = "40 18";
        minExtent = "8 2";
        sluggishness = -1;
        command = "DFDebugPrev();";
        text = "Prev";
        buttonType = "PushButton";
    }; @ "BracketButton13Profile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "42 0";
        extent = "40 18";
        minExtent = "8 2";
        sluggishness = -1;
        command = "DFDebugNext();";
        text = "Next";
        groupNum = -1;
        buttonType = "PushButton";
    }; @ "BracketButton13Profile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "84 0";
        extent = "40 18";
        minExtent = "8 2";
        sluggishness = -1;
        command = "DFDebugRefreshForce();";
        text = "Refresh";
        groupNum = -1;
        buttonType = "PushButton";
    }; @ "ETSShadowTextProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "130 0";
        extent = "500 18";
        minExtent = "8 2";
        visible = 1;
        text = "";
    };
    %posX = %posX - %indent;
    %posX = %posX + %dPosX;
    %posY = %originY;
    %posY = %posY + %dPosY;
    %tab.add(new GuiControl(FarNameOpacityCtrl) {
        profile = "GuiDefaultProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = %posX @ " " @ %posY;
        extent = "190 70";
        minExtent = "8 2";
        visible = 0;
    };);
    %posY = %posY + (%dPosY * 4);
    new GuiMLTextCtrl("") {
        profile = new GuiTextEditCtrl(FarNameOpacityTextEditCtrl) {
        profile = new GuiTextCtrl("") {
        profile = "ETSShadowTextProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "0 0";
        extent = "120 18";
        minExtent = "8 2";
        sluggishness = -1;
        visible = 1;
        text = "Far Opacity";
    }; @ "ETSDarkTextEditProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "61 0";
        extent = "40 18";
        minExtent = "8 2";
        sluggishness = -1;
        visible = 1;
        variable = "$UserPref::Display::farNameOpacity";
        command = "schedulePersist();";
        maxLength = 1024;
        historySize = 0;
        password = 0;
        tabComplete = 0;
        sinkAllKeyEvents = 0;
        cursorType = 1;
    }; @ "InfoTextProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "0 20";
        extent = "160 42";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        lineSpacing = 0;
        allowColorChars = 1;
        maxChars = -1;
        text = "Shows names of people hidden from view or too far away to see.  Enter a number from 0 to 1.";
    };
    %tab.add(new GuiControl(optionsPanelAlertOnLogCtrl) {
        position = %posX @ " " @ %posY;
        extent = 190 @ " " @ (%dPosY * 3);
        visible = $gPerformerMode;
    };);
    %tab.add(new GuiVariableWidthButtonCtrl("") {
        profile = new GuiRadioCtrl(optionsPanelAlertOnErrorCheckBox) {
        profile = new GuiRadioCtrl(optionsPanelAlertOnWarningCheckBox) {
        profile = new GuiTextCtrl("") {
        position = 0 @ " " @ (%dPosY * 0);
        extent = "94 18";
        profile = "ETSShadowTextProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        minExtent = "8 2";
        sluggishness = -1;
        text = "Alert On:";
        maxLength = 255;
    }; @ "ETSCheckBoxProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = 10 @ " " @ (%dPosY * 1);
        extent = "84 18";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        variable = "$UserPref::debug::alertOnLogWarning";
        command = "schedulePersist();";
        text = "Warnings";
        groupNum = -1;
        buttonType = "ToggleButton";
        mouseOver = 0;
    }; @ "ETSCheckBoxProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = 80 @ " " @ (%dPosY * 1);
        extent = "84 18";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        variable = "$UserPref::debug::alertOnLogError";
        command = "schedulePersist();";
        text = "Errors";
        groupNum = -1;
        buttonType = "ToggleButton";
        mouseOver = 0;
    }; @ "BracketButton15Profile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = %originX @ " " @ 215;
        extent = "105 18";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        command = "OptionsPanelTabs.restoreVIPDefaults();";
        text = "Restore Defaults";
        groupNum = -1;
        buttonType = "PushButton";
    };);
    %tab.initialFirstResponder = FarNameOpacityTextEditCtrl;
}
function OptionsPanelTabs::wakeUp(%this)
{
    %this.setup();
    %this.selectCurrentTab();
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
}
function OptionsPanelTabs::restoreTabsDefaults(%this)
{
    $Defaults::UserPref::HudTabs::AutoOpen["music"][$UserPref::HudTabs::AutoOpen @ "music"] = $Defaults::UserPref::HudTabs::AutoOpen["music"];
    $Defaults::UserPref::HudTabs::AutoClose["music"][$UserPref::HudTabs::AutoClose @ "music"] = $Defaults::UserPref::HudTabs::AutoClose["music"];
    $Defaults::UserPref::HudTabs::AutoOpen["affinity"][$UserPref::HudTabs::AutoOpen @ "affinity"] = $Defaults::UserPref::HudTabs::AutoOpen["affinity"];
    $Defaults::UserPref::HudTabs::AutoClose["affinity"][$UserPref::HudTabs::AutoClose @ "affinity"] = $Defaults::UserPref::HudTabs::AutoClose["affinity"];
    $Defaults::UserPref::HudTabs::AutoOpen["scores"][$UserPref::HudTabs::AutoOpen @ "scores"] = $Defaults::UserPref::HudTabs::AutoOpen["scores"];
    $Defaults::UserPref::HudTabs::AutoClose["scores"][$UserPref::HudTabs::AutoClose @ "scores"] = $Defaults::UserPref::HudTabs::AutoClose["scores"];
    $Defaults::UserPref::HudTabs::AutoOpen["word"][$UserPref::HudTabs::AutoOpen @ "word"] = $Defaults::UserPref::HudTabs::AutoOpen["word"];
    $Defaults::UserPref::HudTabs::AutoClose["word"][$UserPref::HudTabs::AutoClose @ "word"] = $Defaults::UserPref::HudTabs::AutoClose["word"];
    $Defaults::UserPref::HudTabs::AutoOpen["tutorial"][$UserPref::HudTabs::AutoOpen @ "tutorial"] = $Defaults::UserPref::HudTabs::AutoOpen["tutorial"];
    $Defaults::UserPref::HudTabs::AutoClose["tutorial"][$UserPref::HudTabs::AutoClose @ "tutorial"] = $Defaults::UserPref::HudTabs::AutoClose["tutorial"];
    %currentTab = HudTabs.getCurrentTab();
    if (%currentTab $= "")
    {
    }
    else
    {
    }
    %tabName = %currentTab.name;
    "";
    if ($UserPref::HudTabs::AutoClose[%tabName])
    {
        HudTabs.autoHide();
    }
    OptionsPanel.readSettings();
    schedulePersist();
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
}
function OptionsPanelTabs::onChangedDFDebugMode(%this)
{
    geDFDebugCtrls.setVisible($Pref::DF::debugMode);
    DF_DebugMode($Pref::DF::debugMode);
}
function OptionsPanelTabs::onChangedDFShowDefaults(%this)
{
    DF_ShowDefaults($Pref::DF::showDefaults);
}
function OptionsPanelTabs::onChangedShowNames(%this)
{
    $UserPref::Display::hideNames = !HUDShowNamesCheckBox.getValue();
    TheBadgesHud.setVisible(!$UserPref::Display::hideNames);
}
function OptionsPanel::open(%this)
{
    %this.readSettings();
    %this.setVisible(1);
    PlayGui.focusAndRaise(%this);
    if ($player && $player.rolesPermissionCheckNoWarn("quietHUD") || $player.rolesPermissionCheckNoWarn("farNameOpacity"))
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
}
function schedulePersist()
{
    if ($OptionsPanel::scheduledPersistID != 0)
    {
        cancel($OptionsPanel::scheduledPersistID);
    }
    $OptionsPanel::scheduledPersistID = schedule(4000, 0, "persistOptionsPanelSettingsToManager");
}
function persistOptionsPanelSettingsToManager()
{
    if (!haveValidManagerHost() || !haveValidToken())
    {
        return;
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
}
function LegacySaveSettingsRequest::onDone(%this)
{
    log("communication", "info", "settings successfully saved to manager");
    %this.schedule(0, "delete");
}
function LegacySaveSettingsRequest::onError(%this, %unused, %errName)
{
    log("communication", "info", "error saving settings: " @ %errName);
    %this.schedule(0, "delete");
}
function legacyPersistOptionsPanelSettingsToManager()
{
    if (!haveValidManagerHost() || !haveValidToken())
    {
        return;
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
}
function OptionsPanel::applySettings(%this)
{
    DefaultAwayMsgEdit.applySettings();
    schedulePersist();
}
function OptionsPanel::showBrightnessControls(%this, %flag)
{
    %this.showBrightnessControls = %flag;
    if (isObject(BrightnessLabel) && isObject(BrightnessSlider))
    {
        BrightnessLabel.setVisible(%flag);
        BrightnessSlider.setVisible(%flag);
    }
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
}
function VolumeSlider::applySettings(%this)
{
    $UserPref::Audio::masterVolume = VolumeSlider.value;
    %multiplier = $UserPref::Audio::mute ? 0 : 1;
    alxListenerf(AL_GAIN_LINEAR, (%multiplier * $UserPref::Audio::masterVolume));
    fmodSetMasterVolume(((%multiplier * $UserPref::Audio::masterVolume) * $UserPref::Audio::channelVolume1));
    if (Using_FFMPEG())
    {
        ffmpegSetMasterVolume(((%multiplier * $UserPref::Audio::masterVolume) * $UserPref::Audio::channelVolume1));
    }
    schedulePersist();
}
function VolumeMusicSlider::applySettings(%this)
{
    $UserPref::Audio::channelVolume1 = %this.value;
    alxSetChannelVolume(1, $UserPref::Audio::channelVolume1);
    %multiplier = $UserPref::Audio::mute ? 0 : 1;
    fmodSetMasterVolume(((%multiplier * $UserPref::Audio::masterVolume) * $UserPref::Audio::channelVolume1));
    if (Using_FFMPEG())
    {
        ffmpegSetMasterVolume(((%multiplier * $UserPref::Audio::masterVolume) * $UserPref::Audio::channelVolume1));
    }
    schedulePersist();
}
function VolumeSFXSlider::applySettings(%this)
{
    $UserPref::Audio::channelVolume2 = VolumeSFXSlider.value;
    alxSetChannelVolume(2, $UserPref::Audio::channelVolume2);
    schedulePersist();
}
function TwoPlayerActionsFriendsPopup::onSelect(%this, %id, %unused)
{
    $UserPref::Player::EmotesPermissionFriends = %id;
    if ($UserPref::Player::EmotesPermissionStrangers < %id)
    {
        TwoPlayerActionsStrangersPopup.SetSelected(%id);
    }
    schedulePersist();
}
function TwoPlayerActionsStrangersPopup::onSelect(%this, %id, %unused)
{
    $UserPref::Player::EmotesPermissionStrangers = %id;
    if ($UserPref::Player::EmotesPermissionFriends > %id)
    {
        TwoPlayerActionsFriendsPopup.SetSelected(%id);
    }
    schedulePersist();
}
function GiftsFriendsPopup::onSelect(%this, %id, %unused)
{
    $UserPref::Player::GiftsPermissionFriends = %id;
    if ($UserPref::Player::GiftsPermissionStrangers < %id)
    {
        GiftsStrangersPopup.SetSelected(%id);
    }
    schedulePersist();
}
function GiftsStrangersPopup::onSelect(%this, %id, %unused)
{
    $UserPref::Player::GiftsPermissionStrangers = %id;
    if ($UserPref::Player::GiftsPermissionFriends > %id)
    {
        GiftsFriendsPopup.SetSelected(%id);
    }
    schedulePersist();
}
function RenderQualityPopup::onSelect(%this, %id, %unused)
{
    setRenderQuality(%id);
}
function ShadowDetailSizePopup::onSelect(%this, %id, %unused)
{
    setShadowDetailSize(%id);
}
function SmallTexturesModePopup::onSelect(%this, %id, %unused)
{
    setSmallTextureMode(%id);
}
function VisibleDistancePopup::onSelect(%this, %id, %unused)
{
    setVisibleDistanceOption(%id);
}
function WaterReflectionModePopup::onSelect(%this, %id, %unused)
{
    setWaterReflection(%id);
}
function ExposureFilterModePopup::onSelect(%this, %id, %unused)
{
    setExposureFilter(%id);
}
function FontSizePopup::onSelect(%this, %id, %unused)
{
    setShapeNameFontSize(%id);
}
function BrightnessSlider::applySettings(%this)
{
    fxEts::updateExposureFilter();
}
function setShapeNameFontSize(%val)
{
    if ((%val < 0) || (%val > 2))
    {
        error("Unknown font size:" @ " " @ %val);
        return;
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
        if (%val == 2)
        {
            %prof = LargeShapeNameHudProfile;
            %otherProf = BoldLargeShapeNameHudProfile;
        }
    }
    TheShapeNameHud.setProfile(%prof);
    TheShapeNameHud.otherProfile = %otherProf;
}
setShapeNameFontSize($UserPref::Video::shapeNameFontSize);
function toggleAutoHideButtonBar()
{
    ButtonBar.setAutoHiding(AutoHideButtonBarCheckBox.getValue());
    schedulePersist();
}
function updateHudTabsHiding()
{
    $UserPref::HudTabs::AutoOpen["music"] = HudMusicAutoOpenCheckBox.getValue();
    $UserPref::HudTabs::AutoClose["music"] = HudMusicAutoCloseCheckBox.getValue();
    $UserPref::HudTabs::AutoOpen["affinity"] = HudAffinityAutoOpenCheckBox.getValue();
    $UserPref::HudTabs::AutoClose["affinity"] = HudAffinityAutoCloseCheckBox.getValue();
    $UserPref::HudTabs::AutoOpen["scores"] = HudScoresAutoOpenCheckBox.getValue();
    $UserPref::HudTabs::AutoClose["scores"] = HudScoresAutoCloseCheckBox.getValue();
    $UserPref::HudTabs::AutoOpen["word"] = HudWordAutoOpenCheckBox.getValue();
    $UserPref::HudTabs::AutoClose["word"] = HudWordAutoCloseCheckBox.getValue();
    %i = 0;
    while (%i < HudTabs.numTabs)
    {
        %tab = HudTabs.getTabAtIndex(%i);
        if (!(%tab.name $= "tutorial"))
        {
            %tab.autoHide = %tab[$UserPref::HudTabs::AutoClose @ %tab.name];
        }
        %i = %i + 1;
    }
    %currentTab = HudTabs.getCurrentTab();
    if (%currentTab $= "")
    {
    }
    else
    {
    }
    %tabName = %currentTab.name;
    "";
    if ($UserPref::HudTabs::AutoClose[%tabName])
    {
        HudTabs.autoHide();
    }
}
function toggleAutoOpenLocalMap()
{
    $UserPref::UI::Radar::AutoOpen = AutoOpenLocalMapCheckBox.getValue();
    HudTabs.autoHide();
    schedulePersist();
}
function doAutoReplyToWhispersWhenAway()
{
    schedulePersist();
}
function doShowTyping()
{
    gSetField($player, lastPreviewText, "");
    MessageHudEdit.sendPreviewText();
    schedulePersist();
}
function doFilterProfanity()
{
    schedulePersist();
}
function doShowOnRadar()
{
    commandToServer('setShowOnRadar', $UserPref::Player::showOnRadar);
    schedulePersist();
}
function doTeleportBlock()
{
    commandToServer('setTeleportBlock', $UserPref::Player::TeleportBlock);
    schedulePersist();
}
function doWhisperBlock(%clearNotify)
{
    commandToServer('setWhisperBlock', $UserPref::Player::WhisperBlock, %clearNotify);
    schedulePersist();
}
function sendInitialPrefsToServer()
{
    commandToServer('setGenre', $UserPref::Player::Genre);
    commandToServer('setHeight', $UserPref::Player::height);
    commandToServer('setShowOnRadar', $UserPref::Player::showOnRadar);
    commandToServer('setWhisperBlock', $UserPref::Player::WhisperBlock, 0);
    commandToServer('setTeleportBlock', $UserPref::Player::TeleportBlock);
}
function doEditAwayMessage()
{
    OptionsPanel.open();
    OptionsPanelTabs.selectTabWithName("social");
    DefaultAwayMsgEdit.makeFirstResponder(1);
    DefaultAwayMsgEdit.selectAll();
}
