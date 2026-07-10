$OptionsPanel::scheduledPersistID = 0;
if (!(isObject(OptionsPanelTabs))) {
    new ScriptObject(OptionsPanelTabs) {
        class = "TabControl";
    };
    if (isObject(MissionCleanup)) {
        OptionsPanelTabs.add(MissionCleanup);
    }
}
function OptionsPanelTabs::setup(%this) {
    if (!(%this.initialized)) {
        "vertical".Initialize(%this, OptionsPanelTabContainer, "88 33", "", "0 0");
        "platform/client/buttons/settings_social".newTab(%this, "social");
        "platform/client/buttons/settings_audio".newTab(%this, "audio");
        "platform/client/buttons/settings_visual".newTab(%this, "visual");
        "platform/client/buttons/settings_tabs".newTab(%this, "tabs");
        "platform/client/buttons/settings_vip".newTab(%this, "vip");
        "social".selectTabWithName(%this);
        "vip".hideTabWithName(%this);
        %this.fillTabs();
    }
};
function OptionsPanelTabs::fillTabs(%this) {
    %i = 0;
    while ((%i < %this.numTabs)) {
        %tab = %this.tabs;
        %i;
        ETSNonModalProfile.setProfile(%tab);
        %tab.clear();
        new GuiBitmapCtrl("") {
            profile = "ETSNonModalProfile";
            horizSizing = "right";
            vertSizing = "bottom";
            position = "0 0";
            extent = "7 239";
            minExtent = "1 1";
            sluggishness = -1;
            visible = 1;
            bitmap = "./ui/settings_bracket_left";
        };.add(%tab);
        new GuiBitmapCtrl("") {
            profile = "ETSNonModalProfile";
            horizSizing = "right";
            vertSizing = "bottom";
            position = "375 0";
            extent = "7 239";
            minExtent = "1 1";
            sluggishness = -1;
            visible = 1;
            bitmap = "./ui/settings_bracket_right";
        };.add(%tab);
        %i = (%i + 1.0);
    }
    %this.fillSocialTab();
    %this.fillAudioTab();
    %this.fillVisualTab();
    %this.fillTabsTab();
    %this.fillVIPTab();
};
function OptionsPanelTabs::tabSelected(%this, %tab) {
    if ("initialFirstResponder".hasFieldValue(%tab) && isObject(%tab.initialFirstResponder)) {
        1.makeFirstResponder(%tab.initialFirstResponder);
    }
};
function OptionsPanelTabs::fillSocialTab(%this) {
    %tab = "social".getTabWithName(%this);
    %originX = 10;
    %originY = 0;
    %posX = %originX;
    %posY = %originY;
    %dPosX = 180;
    %dPosY = 20;
    %dPosYSmall = 5;
    %indent1 = 10;
    %indent2 = 160;
    new GuiTextCtrl("") {
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
    };.add(%tab);
    %posY = (%posY + %dPosY);
    %posX = (%posX + %indent1);
    new GuiTextEditCtrl(DefaultAwayMsgEdit) {
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
    };.add(%tab);
    %posY = (%posY + %dPosY);
    new GuiVariableWidthButtonCtrl("") {
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
    };.add(%tab);
    new GuiVariableWidthButtonCtrl("") {
        profile = "BracketButton15Profile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = (%posX + 60.0) @ " " @ %posY;
        extent = "51 15";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        command = "awayOperation();";
        text = "Go Idle";
        groupNum = -1;
        buttonType = "PushButton";
    };.add(%tab);
    new GuiCheckBoxCtrl(AutoReplyToWhispersCheckBox) {
        profile = "ETSCheckBoxProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = (%posX + 120.0) @ " " @ (%posY - 1.0);
        extent = "200 18";
        minExtent = "8 2";
        sluggishness = -1;
        visible = 1;
        variable = "$UserPref::Player::autoReplyToWhispersWhenAway";
        command = "doAutoReplyToWhispersWhenAway();";
        text = "Auto-reply to whispers when away";
        groupNum = -1;
        buttonType = "ToggleButton";
    };.add(%tab);
    %posY = (%posY + %dPosY);
    %posY = (%posY + %dPosYSmall);
    %posX = (%posX - %indent1);
    new GuiTextCtrl("") {
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
    };.add(%tab);
    %posY = (%posY + %dPosY);
    %posX = (%posX + %indent1);
    %text = "Show My Typing";
    new GuiCheckBoxCtrl(ShowTypingCheckBox) {
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
    };.add(%tab);
    %text = "Refuse Teleports";
    new GuiCheckBoxCtrl(TeleportBlockCheckBox) {
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
    };.add(%tab);
    %posY = (%posY + %dPosY);
    %text = "Filter Profanity";
    new GuiCheckBoxCtrl(FilterProfanityCheckBox) {
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
    };.add(%tab);
    %text = "Refuse Whispers";
    new GuiCheckBoxCtrl(WhisperBlockCheckBox) {
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
    };.add(%tab);
    %posY = (%posY + %dPosY);
    %text = "Show Me on Radar";
    new GuiCheckBoxCtrl(ShowOnRadarCheckBox) {
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
    };.add(%tab);
    %text = "Refuse Yells";
    new GuiCheckBoxCtrl(YellBlockCheckBox) {
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
    };.add(%tab);
    %posY = (%posY + %dPosY);
    %posY = (%posY + %dPosYSmall);
    %posX = (%posX - %indent1);
    new GuiTextCtrl("") {
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
    };.add(%tab);
    %posX = (%posX + 120.0);
    new GuiTextCtrl("") {
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
    };.add(%tab);
    new GuiPopUpMenuCtrl(TwoPlayerActionsFriendsPopup) {
        profile = "ETSDarkPopUpMenuProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = (%posX + 42.0) @ " " @ %posY;
        extent = "54 16";
        minExtent = "8 2";
        sluggishness = -1;
        visible = 1;
        maxLength = 255;
        maxPopupHeight = 200;
        tooltip = "Choose what happens when a friend does a two-player action with me";
    };.add(%tab);
    %posX = (%posX + 120.0);
    new GuiTextCtrl("") {
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
    };.add(%tab);
    new GuiPopUpMenuCtrl(TwoPlayerActionsStrangersPopup) {
        profile = "ETSDarkPopUpMenuProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = (%posX + 65.0) @ " " @ %posY;
        extent = "54 16";
        minExtent = "8 2";
        sluggishness = -1;
        visible = 1;
        maxLength = 255;
        maxPopupHeight = 200;
        tooltip = "Choose what happens when a stranger does a two-player action with me";
    };.add(%tab);
    %posX = (%posX - 240.0);
    %posY = (%posY + %dPosY);
    %posY = (%posY + 5.0);
    new GuiTextCtrl("") {
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
    };.add(%tab);
    %posX = (%posX + 120.0);
    new GuiTextCtrl("") {
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
    };.add(%tab);
    new GuiPopUpMenuCtrl(GiftsFriendsPopup) {
        profile = "ETSDarkPopUpMenuProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = (%posX + 42.0) @ " " @ %posY;
        extent = "54 16";
        minExtent = "8 2";
        sluggishness = -1;
        visible = 1;
        maxLength = 255;
        maxPopupHeight = 200;
        tooltip = "Choose what happens when a friend wants to give me vBux or vPoints";
    };.add(%tab);
    %posX = (%posX + 120.0);
    new GuiTextCtrl("") {
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
    };.add(%tab);
    new GuiPopUpMenuCtrl(GiftsStrangersPopup) {
        profile = "ETSDarkPopUpMenuProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = (%posX + 65.0) @ " " @ %posY;
        extent = "54 16";
        minExtent = "8 2";
        sluggishness = -1;
        visible = 1;
        maxLength = 255;
        maxPopupHeight = 200;
        tooltip = "Choose what happens when a stranger wants to give me vBux or vPoints";
    };.add(%tab);
    %posX = (%posX - 240.0);
    %posY = (%posY + %dPosY);
    %posX = (%posX - %indent1);
    new GuiVariableWidthButtonCtrl("") {
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
    };.add(%tab);
    %tab.initialFirstResponder = DefaultAwayMsgEdit;
};
function OptionsPanelTabs::fillAudioTab(%this) {
    %tab = "audio".getTabWithName(%this);
    %originX = 10;
    %originY = 10;
    %posX = %originX;
    %posY = %originY;
    %dPosX = 180;
    %dPosY = 20;
    %indent = 38;
    new GuiTextCtrl("") {
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
    };.add(%tab);
    new GuiSliderCtrl(VolumeSlider) {
        profile = "ETSSliderProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = (%posX + 55.0) @ " " @ %posY;
        extent = "106 21";
        minExtent = "8 2";
        sluggishness = -1;
        visible = 1;
        altCommand = "VolumeSlider.applySettings();";
        range = "0.000000 1.000000";
        ticks = 10;
        value = 1;
    };.add(%tab);
    %posY = (%posY + %dPosY);
    new GuiTextCtrl("") {
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
    };.add(%tab);
    new GuiSliderCtrl(VolumeMusicSlider) {
        profile = "ETSSliderProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = (%posX + 55.0) @ " " @ %posY;
        extent = "106 21";
        minExtent = "8 2";
        sluggishness = -1;
        visible = 1;
        variable = 0;
        altCommand = "VolumeMusicSlider.applySettings();";
        range = "0.000000 1.000000";
        ticks = 10;
        value = 0;
    };.add(%tab);
    %posY = (%posY + %dPosY);
    new GuiTextCtrl("") {
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
    };.add(%tab);
    new GuiSliderCtrl(VolumeSFXSlider) {
        profile = "ETSSliderProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = (%posX + 55.0) @ " " @ %posY;
        extent = "106 21";
        minExtent = "8 2";
        sluggishness = -1;
        visible = 1;
        variable = 0;
        altCommand = "VolumeSFXSlider.applySettings();";
        range = "0.000000 1.000000";
        ticks = 10;
        value = 0.897959;
    };.add(%tab);
    %posY = (%posY + %dPosY);
    %posX = (%posX + %indent);
    new GuiCheckBoxCtrl(MuteCheckBox) {
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
    };.add(%tab);
    %posY = (%posY + %dPosY);
    %posX = (%posX - %indent);
    %posY = (%posY + %dPosY);
    new GuiTextCtrl("") {
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
    };.add(%tab);
    %posY = (%posY + %dPosY);
    %posX = (%posX + %indent);
    new GuiCheckBoxCtrl(NotifyChatCheckBox) {
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
    };.add(%tab);
    %posY = (%posY + %dPosY);
    new GuiCheckBoxCtrl(NotifyWhisperCheckBox) {
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
    };.add(%tab);
    %posY = (%posY + %dPosY);
    %posX = (%posX - %indent);
    new GuiVariableWidthButtonCtrl("") {
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
    };.add(%tab);
};
function OptionsPanelTabs::fillVisualTab(%this) {
    %tab = "visual".getTabWithName(%this);
    %originX = 10;
    %originY = 10;
    %posX = %originX;
    %posY = %originY;
    %dPosX = 180;
    %dPosY = 20;
    %indent = 14;
    new GuiTextCtrl("") {
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
    };.add(%tab);
    new GuiPopUpMenuCtrl(RenderQualityPopup) {
        profile = "ETSDarkPopUpMenuProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = (%posX + 96.0) @ " " @ %posY;
        extent = "63 16";
        minExtent = "8 2";
        sluggishness = -1;
        visible = 1;
        maxLength = 255;
        maxPopupHeight = 200;
    };.add(%tab);
    %posY = (%posY + %dPosY);
    new GuiTextCtrl("") {
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
    };.add(%tab);
    new GuiPopUpMenuCtrl(ShadowDetailSizePopup) {
        profile = "ETSDarkPopUpMenuProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = (%posX + 96.0) @ " " @ %posY;
        extent = "63 16";
        minExtent = "8 2";
        sluggishness = -1;
        visible = 1;
        maxLength = 255;
        maxPopupHeight = 200;
    };.add(%tab);
    %posY = (%posY + %dPosY);
    new GuiTextCtrl("") {
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
    };.add(%tab);
    new GuiPopUpMenuCtrl(VisibleDistancePopup) {
        profile = "ETSDarkPopUpMenuProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = (%posX + 96.0) @ " " @ %posY;
        extent = "63 16";
        minExtent = "8 2";
        sluggishness = -1;
        visible = 1;
        maxLength = 255;
        maxPopupHeight = 200;
    };.add(%tab);
    %posY = (%posY + %dPosY);
    new GuiTextCtrl("") {
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
    };.add(%tab);
    new GuiPopUpMenuCtrl(WaterReflectionModePopup) {
        profile = "ETSDarkPopUpMenuProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = (%posX + 96.0) @ " " @ %posY;
        extent = "63 16";
        minExtent = "8 2";
        sluggishness = -1;
        visible = 1;
        maxLength = 255;
        maxPopupHeight = 200;
    };.add(%tab);
    %posY = (%posY + %dPosY);
    new GuiTextCtrl("") {
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
    };.add(%tab);
    new GuiPopUpMenuCtrl(ExposureFilterModePopup) {
        profile = "ETSDarkPopUpMenuProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = (%posX + 96.0) @ " " @ %posY;
        extent = "63 16";
        minExtent = "8 2";
        sluggishness = -1;
        visible = 1;
        maxLength = 255;
        maxPopupHeight = 200;
    };.add(%tab);
    %posY = (%posY + %dPosY);
    %posX = (%posX + %indent);
    new GuiTextCtrl(BrightnessLabel) {
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
    };.add(%tab);
    new GuiSliderCtrl(BrightnessSlider) {
        profile = "ETSSliderProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = (%posX + 55.0) @ " " @ %posY;
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
    };.add(%tab);
    %posY = (%posY + (2.0 * %dPosY));
    %posX = (%posX - %indent);
    new GuiCheckBoxCtrl(HUDShowNamesCheckBox) {
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
    };.add(%tab);
    new GuiMLTextCtrl("") {
        profile = "InfoTextProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = (%posX + 60.0) @ " " @ (%posY + 2.0);
        extent = "305 42";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        lineSpacing = 0;
        allowColorChars = 1;
        maxChars = -1;
        text = "Show people's names above their heads";
    };.add(%tab);
    %posY = (%posY + %dPosY);
    %posX = (%posX + %indent);
    new GuiTextCtrl("") {
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
    };.add(%tab);
    new GuiPopUpMenuCtrl(FontSizePopup) {
        profile = "ETSDarkPopUpMenuProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = (%posX + 60.0) @ " " @ %posY;
        extent = "53 16";
        minExtent = "8 2";
        sluggishness = -1;
        visible = 1;
        maxLength = 255;
        maxPopupHeight = 200;
    };.add(%tab);
    %posY = (%posY + (2.0 * %dPosY));
    %posX = (%posX - %indent);
    %posX = (%posX + %dPosX);
    %posY = (%originY - 12.0);
    new GuiCheckBoxCtrl(AutoHideButtonBarCheckBox) {
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
    };.add(%tab);
    $UserPref::ETS::ButtonBar::AutoHide.setValue(AutoHideButtonBarCheckBox);
    %posY = (%posY + %dPosY);
    new GuiCheckBoxCtrl(AutoOpenLocalMapCheckBox) {
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
    };.add(%tab);
    $UserPref::UI::Radar::AutoOpen.setValue(AutoOpenLocalMapCheckBox);
    %posY = (%posY + %dPosY);
    new GuiCheckBoxCtrl(DisplayFlashTaskBarCheckBox) {
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
    };.add(%tab);
    %posY = (%posY + %dPosY);
    new GuiCheckBoxCtrl(ShowAccountHudCheckBox) {
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
    };.add(%tab);
    %posY = (%posY + %dPosY);
    new GuiCheckBoxCtrl(ConstrainWindowDimensions) {
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
    };.add(%tab);
    %posY = (%posY + %dPosY);
    new GuiCheckBoxCtrl(OptionsVisualShowTooltipsCheckbox) {
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
    };.add(%tab);
    %posY = (%posY + %dPosY);
    new GuiCheckBoxCtrl(OptionsVisualShowReloadTexturesCheckbox) {
        profile = "ETSCheckBoxProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = %posX @ " " @ %posY;
        extent = "126 18";
        variable = "$UserPref::UI::ShowReloadTextures";
        command = "changedShowReloadTextures();";
        text = "Show Reload Textures";
        buttonType = "ToggleButton";
    };.add(%tab);
    %posY = (%posY + %dPosY);
    new GuiCheckBoxCtrl(DisableVideosCheckBox) {
        profile = "ETSCheckBoxProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = %posX @ " " @ %posY;
        extent = "126 18";
        text = "Disable Videos";
        buttonType = "ToggleButton";
        variable = "$UserPref::ETS::VideoRenderer::Disable";
        tooltip = "Only check this if you are having trouble with videos";
    };.add(%tab);
    %posY = (%posY + %dPosY);
    new GuiVariableWidthButtonCtrl("") {
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
    };.add(%tab);
    %tab.initialFirstResponder = RenderQualityPopup;
};
function OptionsPanelTabs::fillTabsTab(%this) {
    %tab = "tabs".getTabWithName(%this);
    %originX = 10;
    %originY = 10;
    %posX = %originX;
    %posY = %originY;
    %dPosX = 180;
    %dPosY = 30;
    %indent1 = 37;
    %indent2 = 90;
    %indent3 = 140;
    new GuiMLTextCtrl("") {
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
    };.add(%tab);
    new GuiMLTextCtrl("") {
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
    };.add(%tab);
    %posY = (%posY + %dPosY);
    new GuiControl("") {
        position = %posX @ " " @ %posY;
        extent = "36 29";
        visible = 1;
    };.add(%tab, new GuiBitmapCtrl("") {
        profile = "ETSNonModalProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "-5 -5";
        extent = "46 39";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        bitmap = "./buttons/hud_music_n";
    };);
    new GuiMLTextCtrl("") {
        profile = "GuiDefaultProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = (%posX + %indent1) @ " " @ (%posY + 9.0);
        extent = "50 14";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        lineSpacing = 0;
        allowColorChars = 1;
        maxChars = -1;
        text = "<color:ffffff> Music";
    };.add(%tab);
    new GuiCheckBoxCtrl(HudMusicAutoOpenCheckBox) {
        profile = "ETSCheckBoxProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = ((%posX + %indent2) + 6.0) @ " " @ (%posY + 8.0);
        extent = "18 18";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        variable = "";
        command = "updateHudTabsHiding();";
        text = "";
        groupNum = -1;
        buttonType = "ToggleButton";
    };.add(%tab);
    new GuiCheckBoxCtrl(HudMusicAutoCloseCheckBox) {
        profile = "ETSCheckBoxProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = ((%posX + %indent3) + 6.0) @ " " @ (%posY + 8.0);
        extent = "18 18";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        variable = "";
        command = "updateHudTabsHiding();";
        text = "";
        groupNum = -1;
        buttonType = "ToggleButton";
    };.add(%tab);
    %posY = (%posY + %dPosY);
    new GuiControl("") {
        position = %posX @ " " @ %posY;
        extent = "36 29";
        visible = 1;
    };.add(%tab, new GuiBitmapCtrl("") {
        profile = "ETSNonModalProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "-5 -5";
        extent = "46 39";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        bitmap = "./buttons/hud_affinity_n";
    };);
    new GuiMLTextCtrl("") {
        profile = "GuiDefaultProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = (%posX + %indent1) @ " " @ (%posY + 9.0);
        extent = "50 14";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        lineSpacing = 0;
        allowColorChars = 1;
        maxChars = -1;
        text = "<color:ffffff> Affinity";
    };.add(%tab);
    new GuiCheckBoxCtrl(HudAffinityAutoOpenCheckBox) {
        profile = "ETSCheckBoxProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = ((%posX + %indent2) + 6.0) @ " " @ (%posY + 8.0);
        extent = "18 18";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        variable = "";
        command = "updateHudTabsHiding();";
        text = "";
        groupNum = -1;
        buttonType = "ToggleButton";
    };.add(%tab);
    new GuiCheckBoxCtrl(HudAffinityAutoCloseCheckBox) {
        profile = "ETSCheckBoxProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = ((%posX + %indent3) + 6.0) @ " " @ (%posY + 8.0);
        extent = "18 18";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        variable = "";
        command = "updateHudTabsHiding();";
        text = "";
        groupNum = -1;
        buttonType = "ToggleButton";
    };.add(%tab);
    %posY = (%posY + %dPosY);
    new GuiControl("") {
        position = %posX @ " " @ %posY;
        extent = "36 29";
        visible = 1;
    };.add(%tab, new GuiBitmapCtrl("") {
        profile = "ETSNonModalProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "-5 -5";
        extent = "46 39";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        bitmap = "./buttons/hud_scores_n";
    };);
    new GuiMLTextCtrl("") {
        profile = "GuiDefaultProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = (%posX + %indent1) @ " " @ (%posY + 9.0);
        extent = "50 14";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        lineSpacing = 0;
        allowColorChars = 1;
        maxChars = -1;
        text = "<color:ffffff> Scores";
    };.add(%tab);
    new GuiCheckBoxCtrl(HudScoresAutoOpenCheckBox) {
        profile = "ETSCheckBoxProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = ((%posX + %indent2) + 6.0) @ " " @ (%posY + 8.0);
        extent = "18 18";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 0;
        variable = "";
        command = "updateHudTabsHiding();";
        text = "";
        groupNum = -1;
        buttonType = "ToggleButton";
    };.add(%tab);
    new GuiCheckBoxCtrl(HudScoresAutoCloseCheckBox) {
        profile = "ETSCheckBoxProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = ((%posX + %indent3) + 6.0) @ " " @ (%posY + 8.0);
        extent = "18 18";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        variable = "";
        command = "updateHudTabsHiding();";
        text = "";
        groupNum = -1;
        buttonType = "ToggleButton";
    };.add(%tab);
    %posY = (%posY + %dPosY);
    new GuiControl("") {
        position = %posX @ " " @ %posY;
        extent = "36 29";
        visible = 1;
    };.add(%tab, new GuiBitmapCtrl("") {
        profile = "ETSNonModalProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "-5 -5";
        extent = "46 39";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        bitmap = "./buttons/hud_word_n";
    };);
    new GuiMLTextCtrl("") {
        profile = "GuiDefaultProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = (%posX + %indent1) @ " " @ (%posY + 9.0);
        extent = "50 14";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        lineSpacing = 0;
        allowColorChars = 1;
        maxChars = -1;
        text = "<color:ffffff> Word";
    };.add(%tab);
    new GuiCheckBoxCtrl(HudWordAutoOpenCheckBox) {
        profile = "ETSCheckBoxProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = ((%posX + %indent2) + 6.0) @ " " @ (%posY + 8.0);
        extent = "18 18";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        variable = "";
        command = "updateHudTabsHiding();";
        text = "";
        groupNum = -1;
        buttonType = "ToggleButton";
    };.add(%tab);
    new GuiCheckBoxCtrl(HudWordAutoCloseCheckBox) {
        profile = "ETSCheckBoxProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = ((%posX + %indent3) + 6.0) @ " " @ (%posY + 8.0);
        extent = "18 18";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        variable = "";
        command = "updateHudTabsHiding();";
        text = "";
        groupNum = -1;
        buttonType = "ToggleButton";
    };.add(%tab);
    %posY = (%posY + %dPosY);
    %posX = (%posX + %dPosX);
    %posY = %originY;
    %posY = (%posY + %dPosY);
    new GuiMLTextCtrl("") {
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
    };.add(%tab);
    %posY = (%posY + %dPosY);
    new GuiVariableWidthButtonCtrl("") {
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
    };.add(%tab);
};
function OptionsPanelTabs::fillVIPTab(%this) {
    %tab = "vip".getTabWithName(%this);
    %originX = 10;
    %originY = 10;
    %posX = %originX;
    %posY = %originY;
    %dPosX = 180;
    %dPosY = 20;
    %indent = 10;
    new GuiMLTextCtrl("") {
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
    };.add(%tab);
    %posY = (%posY + %dPosY);
    new GuiControl(ForceFieldCtrls) {
        position = %posX @ " " @ %posY;
        extent = 94 @ " " @ (%dPosY * 5.0);
        visible = $gPerformerMode;
    };.add(%tab);
    %posY = (%posY + (6.0 * %dPosY));
    new GuiRadioCtrl(performerPanelRadioButtonForceField3) {
        profile = new GuiRadioCtrl(performerPanelRadioButtonForceField2) {
        profile = new GuiRadioCtrl(performerPanelRadioButtonForceField1) {
        profile = new GuiRadioCtrl(performerPanelRadioButtonForceField0) {
        profile = new GuiTextCtrl("") {
        position = 0 @ " " @ (%dPosY * 0.0);
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
        position = 10 @ " " @ (%dPosY * 1.0);
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
        position = 10 @ " " @ (%dPosY * 2.0);
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
        position = 10 @ " " @ (%dPosY * 3.0);
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
        position = 10 @ " " @ (%dPosY * 4.0);
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
    1.setValue(performerPanelRadioButtonForceField0);
    new GuiCheckBoxCtrl(HUDHideChatCheckBox) {
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
    };.add(%tab);
    %posY = (%posY + %dPosY);
    new GuiCheckBoxCtrl(geDFDebugMode) {
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
    };.add(%tab);
    new GuiCheckBoxCtrl(geDFShowDefaults) {
        profile = "ETSCheckBoxProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = (%posX + 150.0) @ " " @ %posY;
        extent = "150 18";
        minExtent = "8 2";
        sluggishness = -1;
        variable = "$Pref::DF::ShowDefaults";
        command = "OptionsPanelTabs.onChangedDFShowDefaults();";
        text = "Show Default Ads";
        groupNum = -1;
        buttonType = "ToggleButton";
    };.add(%tab);
    %posY = (%posY + %dPosY);
    %posX = (%posX + %indent);
    new GuiControl(geDFDebugCtrls) {
        horizSizing = "right";
        vertSizing = "bottom";
        position = %posX @ " " @ %posY;
        extent = "300 20";
        visible = $Pref::DF::debugMode;
    };.add(%tab);
    %posY = (%posY + %dPosY);
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
    %posX = (%posX - %indent);
    %posX = (%posX + %dPosX);
    %posY = %originY;
    %posY = (%posY + %dPosY);
    new GuiControl(FarNameOpacityCtrl) {
        profile = "GuiDefaultProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = %posX @ " " @ %posY;
        extent = "190 70";
        minExtent = "8 2";
        visible = 0;
    };.add(%tab);
    %posY = (%posY + (%dPosY * 4.0));
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
    new GuiControl(optionsPanelAlertOnLogCtrl) {
        position = %posX @ " " @ %posY;
        extent = 190 @ " " @ (%dPosY * 3.0);
        visible = $gPerformerMode;
    };.add(%tab);
    new GuiVariableWidthButtonCtrl("") {
        profile = new GuiRadioCtrl(optionsPanelAlertOnErrorCheckBox) {
        profile = new GuiRadioCtrl(optionsPanelAlertOnWarningCheckBox) {
        profile = new GuiTextCtrl("") {
        position = 0 @ " " @ (%dPosY * 0.0);
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
        position = 10 @ " " @ (%dPosY * 1.0);
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
        position = 80 @ " " @ (%dPosY * 1.0);
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
    };.add(%tab);
    %tab.initialFirstResponder = FarNameOpacityTextEditCtrl;
};
function OptionsPanelTabs::wakeUp(%this) {
    %this.setup();
    %this.selectCurrentTab();
};
function OptionsPanelTabs::restoreSocialDefaults(%this) {
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
};
function OptionsPanelTabs::restoreAudioDefaults(%this) {
    $UserPref::Audio::masterVolume = $Defaults::UserPref::Audio::masterVolume;
    $UserPref::Audio::channelVolume1 = $Defaults::UserPref::Audio::channelVolume1;
    $UserPref::Audio::channelVolume2 = $Defaults::UserPref::Audio::channelVolume2;
    if (($UserPref::Audio::mute != $Defaults::UserPref::Audio::mute)) {
        Music::toggleMute();
    }
    $UserPref::Audio::NotifyChat = $Defaults::UserPref::Audio::NotifyChat;
    $UserPref::Audio::NotifyWhisper = $Defaults::UserPref::Audio::NotifyWhisper;
    OptionsPanel.readSettings();
    schedulePersist();
};
function OptionsPanelTabs::restoreVisualDefaults(%this) {
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
    $Defaults::UserPref::ETS::ButtonBar::AutoHide.setAutoHiding(ButtonBar);
    if (isObject(gMessageBoxDontShow)) {
        gMessageBoxDontShow.clear();
    }
    OptionsPanel.readSettings();
    schedulePersist();
};
function OptionsPanelTabs::restoreTabsDefaults(%this) {
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
    if ((%currentTab $= "")) {
    }
    %tabName = %currentTab.name;
    "";
    if (%tabName[$UserPref::HudTabs::AutoClose @ %tabName]) {
        HudTabs.autoHide();
    }
    OptionsPanel.readSettings();
    schedulePersist();
};
function OptionsPanelTabs::restoreVIPDefaults(%this) {
    performerPanelRadioButtonForceField0.performClick();
    $UserPref::Display::hideNames = $Defaults::UserPref::Display::hideNames;
    $UserPref::Display::hideChat = $Defaults::UserPref::Display::hideChat;
    $UserPref::Display::farNameOpacity = $Defaults::UserPref::Display::farNameOpacity;
    $UserPref::debug::alertOnLogWarning = $Defaults::UserPref::debug::alertOnLogWarning;
    $UserPref::debug::alertOnLogError = $Defaults::UserPref::debug::alertOnLogError;
    $Pref::DF::debugMode = $Defaults::pref::DF::debugMode;
    $Pref::DF::showDefaults = $Defaults::pref::DF::showDefaults;
    !($Pref::DF::debugMode).setValue(geDFDebugMode);
    geDFDebugMode.performClick();
    !($Pref::DF::showDefaults).setValue(geDFShowDefaults);
    geDFShowDefaults.performClick();
    OptionsPanel.readSettings();
    schedulePersist();
};
function OptionsPanelTabs::onChangedHideChat(%this) {
    if ($UserPref::Display::hideChat) {
        0.close(ConvBub);
        SystemMessageDialog.close();
    }
    if ((ConvBubVecCtrlMsgVec.getNumLines() > 0.0)) {
        ConvBub.open();
    }
    schedulePersist();
};
function OptionsPanelTabs::onChangedDFDebugMode(%this) {
    $Pref::DF::debugMode.setVisible(geDFDebugCtrls);
    DF_DebugMode($Pref::DF::debugMode);
};
function OptionsPanelTabs::onChangedDFShowDefaults(%this) {
    DF_ShowDefaults($Pref::DF::showDefaults);
};
function OptionsPanelTabs::onChangedShowNames(%this) {
    $UserPref::Display::hideNames = !(HUDShowNamesCheckBox.getValue());
    !($UserPref::Display::hideNames).setVisible(TheBadgesHud);
};
function OptionsPanel::open(%this) {
    %this.readSettings();
    1.setVisible(%this);
    %this.focusAndRaise(PlayGui);
    if ($player && "quietHUD".rolesPermissionCheckNoWarn($player)) {
    }
    if ("farNameOpacity".rolesPermissionCheckNoWarn($player)) {
        "vip".showTabWithName(OptionsPanelTabs);
    }
    if ((OptionsPanelTabs.getCurrentTab().name $= "vip")) {
        0.selectTabAtIndex(OptionsPanelTabs);
    }
    "vip".hideTabWithName(OptionsPanelTabs);
    OptionsPanelTabs.selectCurrentTab();
};
function OptionsPanel::close(%this) {
    %this.applySettings();
    0.setVisible(%this);
    PlayGui.focusTopWindow();
    return 1;
};
function OptionsPanel::wakeUp(%this) {
    OptionsPanelTabs.wakeUp();
};
function OptionsPanel::Initialize(%this) {
    OptionsPanelTabs.setup();
    TwoPlayerActionsFriendsPopup.clear();
    0.add(TwoPlayerActionsFriendsPopup, " Accept");
    1.add(TwoPlayerActionsFriendsPopup, " Ask Me");
    2.add(TwoPlayerActionsFriendsPopup, " Decline");
    TwoPlayerActionsStrangersPopup.clear();
    0.add(TwoPlayerActionsStrangersPopup, " Accept");
    1.add(TwoPlayerActionsStrangersPopup, " Ask Me");
    2.add(TwoPlayerActionsStrangersPopup, " Decline");
    GiftsFriendsPopup.clear();
    0.add(GiftsFriendsPopup, " Accept");
    1.add(GiftsFriendsPopup, " Ask Me");
    2.add(GiftsFriendsPopup, " Decline");
    GiftsStrangersPopup.clear();
    0.add(GiftsStrangersPopup, " Accept");
    1.add(GiftsStrangersPopup, " Ask Me");
    2.add(GiftsStrangersPopup, " Decline");
    RenderQualityPopup.clear();
    0.add(RenderQualityPopup, " Low");
    1.add(RenderQualityPopup, " Medium");
    2.add(RenderQualityPopup, " High");
    3.add(RenderQualityPopup, " Automatic");
    ShadowDetailSizePopup.clear();
    0.add(ShadowDetailSizePopup, " Low");
    1.add(ShadowDetailSizePopup, " Medium");
    2.add(ShadowDetailSizePopup, " High");
    3.add(ShadowDetailSizePopup, " Automatic");
    VisibleDistancePopup.clear();
    0.add(VisibleDistancePopup, " Low");
    1.add(VisibleDistancePopup, " Medium");
    2.add(VisibleDistancePopup, " High");
    3.add(VisibleDistancePopup, " Automatic");
    WaterReflectionModePopup.clear();
    0.add(WaterReflectionModePopup, " Low");
    2.add(WaterReflectionModePopup, " High");
    3.add(WaterReflectionModePopup, " Automatic");
    ExposureFilterModePopup.clear();
    0.add(ExposureFilterModePopup, " Low");
    2.add(ExposureFilterModePopup, " High");
    3.add(ExposureFilterModePopup, " Automatic");
    FontSizePopup.clear();
    0.add(FontSizePopup, " Small");
    1.add(FontSizePopup, " Medium");
    2.add(FontSizePopup, " Large");
    %this.readSettings();
};
function schedulePersist() {
    if (($OptionsPanel::scheduledPersistID != 0.0)) {
        cancel($OptionsPanel::scheduledPersistID);
    }
    $OptionsPanel::scheduledPersistID = schedule(4000, 0, "persistOptionsPanelSettingsToManager");
};
function persistOptionsPanelSettingsToManager() {
    if (!(haveValidManagerHost())) {
    }
    if (!(haveValidToken())) {
        return;
    }
    $UserPref::Audio::masterVolume.setProperty(gUserPropMgrClient, $Player::Name, "volumeMaster");
    $UserPref::Audio::channelVolume1.setProperty(gUserPropMgrClient, $Player::Name, "volumeMusic");
    $UserPref::Audio::channelVolume2.setProperty(gUserPropMgrClient, $Player::Name, "volumeSfx");
    $UserPref::Audio::mute.setProperty(gUserPropMgrClient, $Player::Name, "volumeMute");
    $UserPref::Audio::NotifyChat.setProperty(gUserPropMgrClient, $Player::Name, "flashIncomingChat");
    $UserPref::Audio::NotifyWhisper.setProperty(gUserPropMgrClient, $Player::Name, "flashIncomingWhisper");
    $UserPref::Chat::ShowTyping.setProperty(gUserPropMgrClient, $Player::Name, "showTyping");
    $UserPref::Display::farNameOpacity.setProperty(gUserPropMgrClient, $Player::Name, "farOpacity");
    $UserPref::Display::hideChat.setProperty(gUserPropMgrClient, $Player::Name, "hideChat");
    $UserPref::Display::hideNames.setProperty(gUserPropMgrClient, $Player::Name, "hideNames");
    $UserPref::ETS::ButtonBar::AutoHide.setProperty(gUserPropMgrClient, $Player::Name, "hideButtonBar");
    $UserPref::HudTabs::AutoOpen["music"].setProperty(gUserPropMgrClient, $Player::Name, "autoOpenTabMusic");
    $UserPref::HudTabs::AutoClose["music"].setProperty(gUserPropMgrClient, $Player::Name, "autoCloseTabMusic");
    $UserPref::HudTabs::AutoOpen["affinity"].setProperty(gUserPropMgrClient, $Player::Name, "autoOpenTabAffinity");
    $UserPref::HudTabs::AutoClose["affinity"].setProperty(gUserPropMgrClient, $Player::Name, "autoCloseTabAffinity");
    $UserPref::HudTabs::AutoOpen["scores"].setProperty(gUserPropMgrClient, $Player::Name, "autoOpenTabScores");
    $UserPref::HudTabs::AutoClose["scores"].setProperty(gUserPropMgrClient, $Player::Name, "autoCloseTabScores");
    $UserPref::HudTabs::AutoOpen["word"].setProperty(gUserPropMgrClient, $Player::Name, "autoOpenTabWord");
    $UserPref::HudTabs::AutoClose["word"].setProperty(gUserPropMgrClient, $Player::Name, "autoCloseTabWord");
    $UserPref::Player::TeleportBlock.setProperty(gUserPropMgrClient, $Player::Name, "refuseTeleports");
    $UserPref::Player::WhisperBlock.setProperty(gUserPropMgrClient, $Player::Name, "refuseWhispers");
    $UserPref::Player::YellBlock.setProperty(gUserPropMgrClient, $Player::Name, "refuseYells");
    $UserPref::Player::EmotesPermissionFriends.setProperty(gUserPropMgrClient, $Player::Name, "emotesPermissionsFriends");
    $UserPref::Player::EmotesPermissionStrangers.setProperty(gUserPropMgrClient, $Player::Name, "emotesPermissionsStrangers");
    $UserPref::Player::GiftsPermissionFriends.setProperty(gUserPropMgrClient, $Player::Name, "giftsPermissionsFriends");
    $UserPref::Player::GiftsPermissionStrangers.setProperty(gUserPropMgrClient, $Player::Name, "giftsPermissionsStrangers");
    $UserPref::Player::awayMessage.setProperty(gUserPropMgrClient, $Player::Name, "awayMessage");
    $UserPref::Player::autoReplyToWhispersWhenAway.setProperty(gUserPropMgrClient, $Player::Name, "autoReplyToWhipsers");
    $UserPref::Player::filterProfanity.setProperty(gUserPropMgrClient, $Player::Name, "filterProfanity");
    $UserPref::Player::Genre.setProperty(gUserPropMgrClient, $Player::Name, "playerMood");
    $UserPref::Player::height.setProperty(gUserPropMgrClient, $Player::Name, "avatarHeight");
    $UserPref::Player::showOnRadar.setProperty(gUserPropMgrClient, $Player::Name, "showOnRadar");
    $UserPref::UI::FlashTaskBar.setProperty(gUserPropMgrClient, $Player::Name, "flashTaskBar");
    $UserPref::UI::Radar::AutoOpen.setProperty(gUserPropMgrClient, $Player::Name, "radarAutoOpen");
    $UserPref::UI::ShowAccountHud.setProperty(gUserPropMgrClient, $Player::Name, "showAccountHud");
    $UserPref::UI::ShowTooltips.setProperty(gUserPropMgrClient, $Player::Name, "showTooltips");
    $UserPref::Video::Exposure.setProperty(gUserPropMgrClient, $Player::Name, "videoExposure");
    $UserPref::Video::shapeNameFontSize.setProperty(gUserPropMgrClient, $Player::Name, "videoNameSize");
    $UserPref::Video::renderQualitySetting.setProperty(gUserPropMgrClient, $Player::Name, "videoRenderQuality");
    $UserPref::Video::shadowQualitySetting.setProperty(gUserPropMgrClient, $Player::Name, "videoShadowQuality");
    $UserPref::Video::visibledistanceQualitySetting.setProperty(gUserPropMgrClient, $Player::Name, "videoVisibleDistance");
    $UserPref::Video::waterreflectionQualitySetting.setProperty(gUserPropMgrClient, $Player::Name, "videoWaterReflection");
    $UserPref::Video::ConstrainWindowDimensions.setProperty(gUserPropMgrClient, $Player::Name, "videoConstrainWindowDimensions");
    $UserPref::debug::alertOnLogWarning.setProperty(gUserPropMgrClient, $Player::Name, "alertOnLogWarning");
    $UserPref::debug::alertOnLogError.setProperty(gUserPropMgrClient, $Player::Name, "alertOnLogError");
    %maxNumberKeyCombos = getFieldCount($Defaults::UserPref::emotes::defaultKeyCombinations);
    %i = (%maxNumberKeyCombos - 1.0);
    while ((%i >= 0.0)) {
        %keyCombo = getField($Defaults::UserPref::emotes::defaultKeyCombinations, %i);
        %keyCombo[$UserPref::emotes TAB "f" @ %keyCombo].setProperty(gUserPropMgrClient, $Player::Name, "favoriteActionsKey_f_" @ %keyCombo);
        %keyCombo[$UserPref::emotes TAB "m" @ %keyCombo].setProperty(gUserPropMgrClient, $Player::Name, "favoriteActionsKey_m_" @ %keyCombo);
        %i = (%i - 1.0);
    }
    $OptionsPanel::scheduledPersistID = 0;
    (%i >= 0.0);
    legacyPersistOptionsPanelSettingsToManager();
};
function LegacySaveSettingsRequest::onDone(%this) {
    log("communication", "info", "settings successfully saved to manager");
    "delete".schedule(%this, 0);
};
function LegacySaveSettingsRequest::onError(%this, %unused, %errName) {
    log("communication", "info", "error saving settings: " @ %errName);
    "delete".schedule(%this, 0);
};
function legacyPersistOptionsPanelSettingsToManager() {
    if (!(haveValidManagerHost())) {
    }
    if (!(haveValidToken())) {
        return;
    }
    %request = new ManagerRequest(LegacySaveSettingsRequest);
    if (isObject(MissionCleanup)) {
        %request.add(MissionCleanup);
    }
    %url = $Net::ClientServiceURL @ "/SaveSettings";
    %url = %url @ "?user=" @ urlEncode($Player::Name);
    %url = %url @ "&token=" @ urlEncode($Token);
    %url = %url @ "&settingsMapCount=" @ 3;
    %url = %url @ "&settingsMap0.key=refuseTeleports&settingsMap0.value=" @ urlEncode($UserPref::Player::TeleportBlock);
    %url = %url @ "&settingsMap1.key=refuseWhispers&settingsMap1.value=" @ urlEncode($UserPref::Player::WhisperBlock);
    %url = %url @ "&settingsMap2.key=awayMessage&settingsMap2.value=" @ urlEncode($UserPref::Player::awayMessage);
    log("communication", "debug", "save settings: " @ %url);
    %url.setURL(%request);
    %request.start();
};
function OptionsPanel::readSettings(%this) {
    $UserPref::Player::awayMessage.setValue(DefaultAwayMsgEdit);
    DefaultAwayMsgEdit.applySettings();
    $UserPref::Chat::ShowTyping.setValue(ShowTypingCheckBox);
    $UserPref::Player::filterProfanity.setValue(FilterProfanityCheckBox);
    $UserPref::Player::TeleportBlock.setValue(TeleportBlockCheckBox);
    $UserPref::Player::WhisperBlock.setValue(WhisperBlockCheckBox);
    $UserPref::Player::YellBlock.setValue(YellBlockCheckBox);
    $UserPref::Player::filterProfanity.setValue(FilterProfanityCheckBox);
    $UserPref::Player::showOnRadar.setValue(ShowOnRadarCheckBox);
    $UserPref::Player::EmotesPermissionFriends.SetSelected(TwoPlayerActionsFriendsPopup);
    $UserPref::Player::EmotesPermissionStrangers.SetSelected(TwoPlayerActionsStrangersPopup);
    $UserPref::Player::GiftsPermissionFriends.SetSelected(GiftsFriendsPopup);
    $UserPref::Player::GiftsPermissionStrangers.SetSelected(GiftsStrangersPopup);
    $UserPref::Audio::masterVolume.setValue(VolumeSlider);
    VolumeSlider.applySettings();
    $UserPref::Audio::channelVolume1.setValue(VolumeMusicSlider);
    VolumeMusicSlider.applySettings();
    $UserPref::Audio::channelVolume2.setValue(VolumeSFXSlider);
    VolumeSFXSlider.applySettings();
    $UserPref::Audio::mute.setValue(MuteCheckBox);
    $UserPref::Audio::NotifyChat.setValue(NotifyChatCheckBox);
    $UserPref::Audio::NotifyWhisper.setValue(NotifyWhisperCheckBox);
    $UserPref::Video::Exposure.setValue(BrightnessSlider);
    BrightnessSlider.applySettings();
    $UserPref::Video::renderQualitySetting.SetSelected(RenderQualityPopup);
    $UserPref::Video::shadowQualitySetting.SetSelected(ShadowDetailSizePopup);
    $UserPref::Video::visibledistanceQualitySetting.SetSelected(VisibleDistancePopup);
    $UserPref::Video::waterreflectionQualitySetting.SetSelected(WaterReflectionModePopup);
    $UserPref::Video::exposureQualitySetting.SetSelected(ExposureFilterModePopup);
    $UserPref::Video::shapeNameFontSize.SetSelected(FontSizePopup);
    $UserPref::UI::Radar::AutoOpen.setValue(AutoOpenLocalMapCheckBox);
    $UserPref::ETS::ButtonBar::AutoHide.setValue(AutoHideButtonBarCheckBox);
    $UserPref::UI::FlashTaskBar.setValue(DisplayFlashTaskBarCheckBox);
    $UserPref::UI::ShowAccountHud.setValue(ShowAccountHudCheckBox);
    $UserPref::UI::ShowTooltips.setValue(OptionsVisualShowTooltipsCheckbox);
    %curScreenMode = getRes();
    %curScreenScale = (getWord(%curScreenMode, 0) / 16.0);
    $UserPref::HudTabs::AutoOpen["music"].setValue(HudMusicAutoOpenCheckBox);
    $UserPref::HudTabs::AutoClose["music"].setValue(HudMusicAutoCloseCheckBox);
    $UserPref::HudTabs::AutoOpen["affinity"].setValue(HudAffinityAutoOpenCheckBox);
    $UserPref::HudTabs::AutoClose["affinity"].setValue(HudAffinityAutoCloseCheckBox);
    $UserPref::HudTabs::AutoOpen["scores"].setValue(HudScoresAutoOpenCheckBox);
    $UserPref::HudTabs::AutoClose["scores"].setValue(HudScoresAutoCloseCheckBox);
    $UserPref::HudTabs::AutoOpen["word"].setValue(HudWordAutoOpenCheckBox);
    $UserPref::HudTabs::AutoClose["word"].setValue(HudWordAutoCloseCheckBox);
    !($UserPref::Display::hideNames).setValue(HUDShowNamesCheckBox);
    $UserPref::Display::hideChat.setValue(HUDHideChatCheckBox);
    $UserPref::Display::farNameOpacity.setValue(FarNameOpacityTextEditCtrl);
    $UserPref::debug::alertOnLogWarning.setValue(optionsPanelAlertOnWarningCheckBox);
    $UserPref::debug::alertOnLogError.setValue(optionsPanelAlertOnErrorCheckBox);
};
function OptionsPanel::applySettings(%this) {
    DefaultAwayMsgEdit.applySettings();
    schedulePersist();
};
function OptionsPanel::showBrightnessControls(%this, %flag) {
    %this.showBrightnessControls = %flag;
    if (isObject(BrightnessLabel)) {
    }
    if (isObject(BrightnessSlider)) {
        %flag.setVisible(BrightnessLabel);
        %flag.setVisible(BrightnessSlider);
    }
};
function DefaultAwayMsgEdit::applySettings(%this) {
    trim(%this.getValue()).setValue(%this);
    %newAwayMsg = %this.getValue();
    if ((%newAwayMsg $= "")) {
        if (($UserPref::Player::awayMessage $= "")) {
            $UserPref::Player::awayMessage = $Pref::Player::defaultAwayMessage;
        }
        $UserPref::Player::awayMessage.setValue(%this);
    }
    $UserPref::Player::awayMessage = %newAwayMsg;
    if (isIdle()) {
        setIdle(1, $UserPref::Player::awayMessage);
    }
    schedulePersist();
};
function VolumeSlider::applySettings(%this) {
    $UserPref::Audio::masterVolume = VolumeSlider.value;
    %multiplier = $UserPref::Audio::mute ? 0 : 1;
    alxListenerf(AL_GAIN_LINEAR, (%multiplier * $UserPref::Audio::masterVolume));
    fmodSetMasterVolume(((%multiplier * $UserPref::Audio::masterVolume) * $UserPref::Audio::channelVolume1));
    if (Using_FFMPEG()) {
        ffmpegSetMasterVolume(((%multiplier * $UserPref::Audio::masterVolume) * $UserPref::Audio::channelVolume1));
    }
    schedulePersist();
};
function VolumeMusicSlider::applySettings(%this) {
    $UserPref::Audio::channelVolume1 = %this.value;
    alxSetChannelVolume(1, $UserPref::Audio::channelVolume1);
    %multiplier = $UserPref::Audio::mute ? 0 : 1;
    fmodSetMasterVolume(((%multiplier * $UserPref::Audio::masterVolume) * $UserPref::Audio::channelVolume1));
    if (Using_FFMPEG()) {
        ffmpegSetMasterVolume(((%multiplier * $UserPref::Audio::masterVolume) * $UserPref::Audio::channelVolume1));
    }
    schedulePersist();
};
function VolumeSFXSlider::applySettings(%this) {
    $UserPref::Audio::channelVolume2 = VolumeSFXSlider.value;
    alxSetChannelVolume(2, $UserPref::Audio::channelVolume2);
    schedulePersist();
};
function TwoPlayerActionsFriendsPopup::onSelect(%this, %id, %unused) {
    $UserPref::Player::EmotesPermissionFriends = %id;
    if (($UserPref::Player::EmotesPermissionStrangers < %id)) {
        %id.SetSelected(TwoPlayerActionsStrangersPopup);
    }
    schedulePersist();
};
function TwoPlayerActionsStrangersPopup::onSelect(%this, %id, %unused) {
    $UserPref::Player::EmotesPermissionStrangers = %id;
    if (($UserPref::Player::EmotesPermissionFriends > %id)) {
        %id.SetSelected(TwoPlayerActionsFriendsPopup);
    }
    schedulePersist();
};
function GiftsFriendsPopup::onSelect(%this, %id, %unused) {
    $UserPref::Player::GiftsPermissionFriends = %id;
    if (($UserPref::Player::GiftsPermissionStrangers < %id)) {
        %id.SetSelected(GiftsStrangersPopup);
    }
    schedulePersist();
};
function GiftsStrangersPopup::onSelect(%this, %id, %unused) {
    $UserPref::Player::GiftsPermissionStrangers = %id;
    if (($UserPref::Player::GiftsPermissionFriends > %id)) {
        %id.SetSelected(GiftsFriendsPopup);
    }
    schedulePersist();
};
function RenderQualityPopup::onSelect(%this, %id, %unused) {
    setRenderQuality(%id);
};
function ShadowDetailSizePopup::onSelect(%this, %id, %unused) {
    setShadowDetailSize(%id);
};
function SmallTexturesModePopup::onSelect(%this, %id, %unused) {
    setSmallTextureMode(%id);
};
function VisibleDistancePopup::onSelect(%this, %id, %unused) {
    setVisibleDistanceOption(%id);
};
function WaterReflectionModePopup::onSelect(%this, %id, %unused) {
    setWaterReflection(%id);
};
function ExposureFilterModePopup::onSelect(%this, %id, %unused) {
    setExposureFilter(%id);
};
function FontSizePopup::onSelect(%this, %id, %unused) {
    setShapeNameFontSize(%id);
};
function BrightnessSlider::applySettings(%this) {
    fxEts::updateExposureFilter();
};
function setShapeNameFontSize(%val) {
    if ((%val < 0.0)) {
    }
    if ((%val > 2.0)) {
        error("Unknown font size:" @ " " @ %val);
        return;
    }
    $UserPref::Video::shapeNameFontSize = %val;
    if ((%val == 0.0)) {
        %prof = SmallShapeNameHudProfile;
        %otherProf = BoldSmallShapeNameHudProfile;
    }
    if ((%val == 1.0)) {
        %prof = MediumShapeNameHudProfile;
        %otherProf = BoldMediumShapeNameHudProfile;
    }
    if ((%val == 2.0)) {
        %prof = LargeShapeNameHudProfile;
        %otherProf = BoldLargeShapeNameHudProfile;
    }
    %prof.setProfile(TheShapeNameHud);
    TheShapeNameHud.otherProfile = %otherProf;
};
setShapeNameFontSize($UserPref::Video::shapeNameFontSize);
function toggleAutoHideButtonBar() {
    AutoHideButtonBarCheckBox.getValue().setAutoHiding(ButtonBar);
    schedulePersist();
};
function updateHudTabsHiding() {
    $UserPref::HudTabs::AutoOpen["music"] = HudMusicAutoOpenCheckBox.getValue();
    $UserPref::HudTabs::AutoClose["music"] = HudMusicAutoCloseCheckBox.getValue();
    $UserPref::HudTabs::AutoOpen["affinity"] = HudAffinityAutoOpenCheckBox.getValue();
    $UserPref::HudTabs::AutoClose["affinity"] = HudAffinityAutoCloseCheckBox.getValue();
    $UserPref::HudTabs::AutoOpen["scores"] = HudScoresAutoOpenCheckBox.getValue();
    $UserPref::HudTabs::AutoClose["scores"] = HudScoresAutoCloseCheckBox.getValue();
    $UserPref::HudTabs::AutoOpen["word"] = HudWordAutoOpenCheckBox.getValue();
    $UserPref::HudTabs::AutoClose["word"] = HudWordAutoCloseCheckBox.getValue();
    %i = 0;
    while ((%i < HudTabs.numTabs)) {
        %tab = %i.getTabAtIndex(HudTabs);
        if (!(%tab.name $= "tutorial")) {
            %tab.autoHide = %tab[$UserPref::HudTabs::AutoClose @ %tab.name];
        }
        %i = (%i + 1.0);
    }
    %currentTab = HudTabs.getCurrentTab();
    (%i < HudTabs.numTabs);
    if ((%currentTab $= "")) {
    }
    %tabName = %currentTab.name;
    "";
    if (%tabName[$UserPref::HudTabs::AutoClose @ %tabName]) {
        HudTabs.autoHide();
    }
};
function toggleAutoOpenLocalMap() {
    $UserPref::UI::Radar::AutoOpen = AutoOpenLocalMapCheckBox.getValue();
    HudTabs.autoHide();
    schedulePersist();
};
function doAutoReplyToWhispersWhenAway() {
    schedulePersist();
};
function doShowTyping() {
    gSetField($player, lastPreviewText, "");
    MessageHudEdit.sendPreviewText();
    schedulePersist();
};
function doFilterProfanity() {
    schedulePersist();
};
function doShowOnRadar() {
    commandToServer('setShowOnRadar', $UserPref::Player::showOnRadar);
    schedulePersist();
};
function doTeleportBlock() {
    commandToServer('setTeleportBlock', $UserPref::Player::TeleportBlock);
    schedulePersist();
};
function doWhisperBlock(%clearNotify) {
    commandToServer('setWhisperBlock', $UserPref::Player::WhisperBlock, %clearNotify);
    schedulePersist();
};
function sendInitialPrefsToServer() {
    commandToServer('setGenre', $UserPref::Player::Genre);
    commandToServer('setHeight', $UserPref::Player::height);
    commandToServer('setShowOnRadar', $UserPref::Player::showOnRadar);
    commandToServer('setWhisperBlock', $UserPref::Player::WhisperBlock, 0);
    commandToServer('setTeleportBlock', $UserPref::Player::TeleportBlock);
};
function doEditAwayMessage() {
    OptionsPanel.open();
    "social".selectTabWithName(OptionsPanelTabs);
    1.makeFirstResponder(DefaultAwayMsgEdit);
    DefaultAwayMsgEdit.selectAll();
};
