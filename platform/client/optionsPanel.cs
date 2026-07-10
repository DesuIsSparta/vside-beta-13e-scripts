$OptionsPanel::scheduledPersistID = 0;
if (!(isObject(OptionsPanelTabs))) {
    new ScriptObject(OptionsPanelTabs) {
        class = "TabControl";
    };
    if (isObject(MissionCleanup)) {
        MissionCleanup.add(OptionsPanelTabs);
    }
}
function OptionsPanelTabs::setup(%this) {
    if (!(%this.initialized)) {
        %this.Initialize("88 33", "", "0 0", "vertical");
        %this.newTab("social", "platform/client/buttons/settings_social");
        %this.newTab("audio", "platform/client/buttons/settings_audio");
        %this.newTab("visual", "platform/client/buttons/settings_visual");
        %this.newTab("tabs", "platform/client/buttons/settings_tabs");
        %this.newTab("vip", "platform/client/buttons/settings_vip");
        %this.selectTabWithName("social");
        %this.hideTabWithName("vip");
        OptionsPanelTabs.fillTabs(%this);
    }
};
function OptionsPanelTabs::fillTabs(%this) {
    %i = 0;
    if ((%this.numTabs < %i)) {
        %tab = %this.tabs;
        %i;
        %tab.setProfile();
        %tab.clear();
        0;
        %tab.add(new ""() {
            profile = GuiBitmapCtrl @ "ETSNonModalProfile";
            horizSizing = ETSNonModalProfile @ "right";
            vertSizing = "bottom";
            position = "0 0";
            extent = "7 239";
            minExtent = "1 1";
            sluggishness = -1;
            visible = 1;
            bitmap = "./ui/settings_bracket_left";
        };);
        0;
        %tab.add(new ""() {
            profile = GuiBitmapCtrl @ "ETSNonModalProfile";
            horizSizing = "right";
            vertSizing = "bottom";
            position = "375 0";
            extent = "7 239";
            minExtent = "1 1";
            sluggishness = -1;
            visible = 1;
            bitmap = "./ui/settings_bracket_right";
        };);
        %i = (1.0 + %i);
    }
    %this.fillSocialTab();
    %this.fillAudioTab();
    %this.fillVisualTab();
    %this.fillTabsTab();
    %this.fillVIPTab();
};
function OptionsPanelTabs::tabSelected(%this, %tab) {
    if (%tab.hasFieldValue("initialFirstResponder")) {
        if (isObject(%tab.initialFirstResponder)) {
            %tab.initialFirstResponder.makeFirstResponder(1);
        }
    }
};
function OptionsPanelTabs::fillSocialTab(%this) {
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
    0;
    %tab.add(new ""() {
        profile = GuiTextCtrl @ "ETSShadowTextProfile";
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
    %posY = (%dPosY + %posY);
    %posX = (%indent1 + %posX);
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
    %posY = (%dPosY + %posY);
    0;
    %tab.add(new ""() {
        profile = GuiVariableWidthButtonCtrl @ "BracketButton15Profile";
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
    0;
    %tab.add(new ""() {
        profile = GuiVariableWidthButtonCtrl @ "BracketButton15Profile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = (60.0 + %posX) @ " " @ %posY;
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
        position = (120.0 + %posX) @ " " @ (1.0 - %posY);
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
    %posY = (%dPosY + %posY);
    %posY = (%dPosYSmall + %posY);
    %posX = (%indent1 - %posX);
    0;
    %tab.add(new ""() {
        position = GuiTextCtrl @ %posX @ " " @ %posY;
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
    %posY = (%dPosY + %posY);
    %posX = (%indent1 + %posX);
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
        position = (%indent2 + %posX) @ " " @ %posY;
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
    %posY = (%dPosY + %posY);
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
        position = (%indent2 + %posX) @ " " @ %posY;
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
    %posY = (%dPosY + %posY);
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
        position = (%indent2 + %posX) @ " " @ %posY;
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
    %posY = (%dPosY + %posY);
    %posY = (%dPosYSmall + %posY);
    %posX = (%indent1 - %posX);
    0;
    %tab.add(new ""() {
        position = GuiTextCtrl @ %posX @ " " @ %posY;
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
    %posX = (120.0 + %posX);
    0;
    %tab.add(new ""() {
        position = GuiTextCtrl @ %posX @ " " @ %posY;
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
        position = (42.0 + %posX) @ " " @ %posY;
        extent = "54 16";
        minExtent = "8 2";
        sluggishness = -1;
        visible = 1;
        maxLength = 255;
        maxPopupHeight = 200;
        tooltip = "Choose what happens when a friend does a two-player action with me";
    };);
    %posX = (120.0 + %posX);
    0;
    %tab.add(new ""() {
        position = GuiTextCtrl @ %posX @ " " @ %posY;
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
        position = (65.0 + %posX) @ " " @ %posY;
        extent = "54 16";
        minExtent = "8 2";
        sluggishness = -1;
        visible = 1;
        maxLength = 255;
        maxPopupHeight = 200;
        tooltip = "Choose what happens when a stranger does a two-player action with me";
    };);
    %posX = (240.0 - %posX);
    %posY = (%dPosY + %posY);
    %posY = (5.0 + %posY);
    0;
    %tab.add(new ""() {
        position = GuiTextCtrl @ %posX @ " " @ %posY;
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
    %posX = (120.0 + %posX);
    0;
    %tab.add(new ""() {
        position = GuiTextCtrl @ %posX @ " " @ %posY;
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
        position = (42.0 + %posX) @ " " @ %posY;
        extent = "54 16";
        minExtent = "8 2";
        sluggishness = -1;
        visible = 1;
        maxLength = 255;
        maxPopupHeight = 200;
        tooltip = "Choose what happens when a friend wants to give me vBux or vPoints";
    };);
    %posX = (120.0 + %posX);
    0;
    %tab.add(new ""() {
        position = GuiTextCtrl @ %posX @ " " @ %posY;
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
        position = (65.0 + %posX) @ " " @ %posY;
        extent = "54 16";
        minExtent = "8 2";
        sluggishness = -1;
        visible = 1;
        maxLength = 255;
        maxPopupHeight = 200;
        tooltip = "Choose what happens when a stranger wants to give me vBux or vPoints";
    };);
    %posX = (240.0 - %posX);
    %posY = (%dPosY + %posY);
    %posX = (%indent1 - %posX);
    0;
    %tab.add(new ""() {
        profile = GuiVariableWidthButtonCtrl @ "BracketButton15Profile";
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
};
function OptionsPanelTabs::fillAudioTab(%this) {
    %tab = %this.getTabWithName("audio");
    %originX = 10;
    %originY = 10;
    %posX = %originX;
    %posY = %originY;
    %dPosX = 180;
    %dPosY = 20;
    %indent = 38;
    0;
    %tab.add(new ""() {
        profile = GuiTextCtrl @ "ETSRightJustifiedShadowTextProfile";
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
        position = (55.0 + %posX) @ " " @ %posY;
        extent = "106 21";
        minExtent = "8 2";
        sluggishness = -1;
        visible = 1;
        altCommand = "VolumeSlider.applySettings();";
        range = "0.000000 1.000000";
        ticks = 10;
        value = 1;
    };);
    %posY = (%dPosY + %posY);
    0;
    %tab.add(new ""() {
        profile = GuiTextCtrl @ "ETSRightJustifiedShadowTextProfile";
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
        position = (55.0 + %posX) @ " " @ %posY;
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
    %posY = (%dPosY + %posY);
    0;
    %tab.add(new ""() {
        profile = GuiTextCtrl @ "ETSRightJustifiedShadowTextProfile";
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
        position = (55.0 + %posX) @ " " @ %posY;
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
    %posY = (%dPosY + %posY);
    %posX = (%indent + %posX);
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
    %posY = (%dPosY + %posY);
    %posX = (%indent - %posX);
    %posY = (%dPosY + %posY);
    0;
    %tab.add(new ""() {
        profile = GuiTextCtrl @ "ETSShadowTextProfile";
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
    %posY = (%dPosY + %posY);
    %posX = (%indent + %posX);
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
    %posY = (%dPosY + %posY);
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
    %posY = (%dPosY + %posY);
    %posX = (%indent - %posX);
    0;
    %tab.add(new ""() {
        profile = GuiVariableWidthButtonCtrl @ "BracketButton15Profile";
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
};
function OptionsPanelTabs::fillVisualTab(%this) {
    %tab = %this.getTabWithName("visual");
    %originX = 10;
    %originY = 10;
    %posX = %originX;
    %posY = %originY;
    %dPosX = 180;
    %dPosY = 20;
    %indent = 14;
    0;
    %tab.add(new ""() {
        profile = GuiTextCtrl @ "ETSShadowTextProfile";
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
        position = (96.0 + %posX) @ " " @ %posY;
        extent = "63 16";
        minExtent = "8 2";
        sluggishness = -1;
        visible = 1;
        maxLength = 255;
        maxPopupHeight = 200;
    };);
    %posY = (%dPosY + %posY);
    0;
    %tab.add(new ""() {
        profile = GuiTextCtrl @ "ETSShadowTextProfile";
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
        position = (96.0 + %posX) @ " " @ %posY;
        extent = "63 16";
        minExtent = "8 2";
        sluggishness = -1;
        visible = 1;
        maxLength = 255;
        maxPopupHeight = 200;
    };);
    %posY = (%dPosY + %posY);
    0;
    %tab.add(new ""() {
        profile = GuiTextCtrl @ "ETSShadowTextProfile";
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
        position = (96.0 + %posX) @ " " @ %posY;
        extent = "63 16";
        minExtent = "8 2";
        sluggishness = -1;
        visible = 1;
        maxLength = 255;
        maxPopupHeight = 200;
    };);
    %posY = (%dPosY + %posY);
    0;
    %tab.add(new ""() {
        profile = GuiTextCtrl @ "ETSShadowTextProfile";
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
        position = (96.0 + %posX) @ " " @ %posY;
        extent = "63 16";
        minExtent = "8 2";
        sluggishness = -1;
        visible = 1;
        maxLength = 255;
        maxPopupHeight = 200;
    };);
    %posY = (%dPosY + %posY);
    0;
    %tab.add(new ""() {
        profile = GuiTextCtrl @ "ETSShadowTextProfile";
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
        position = (96.0 + %posX) @ " " @ %posY;
        extent = "63 16";
        minExtent = "8 2";
        sluggishness = -1;
        visible = 1;
        maxLength = 255;
        maxPopupHeight = 200;
    };);
    %posY = (%dPosY + %posY);
    %posX = (%indent + %posX);
    %tab.add(new GuiTextCtrl(BrightnessLabel) {
        profile = "ETSShadowTextProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = %posX @ " " @ %posY;
        extent = "31 18";
        minExtent = "8 2";
        sluggishness = -1;
        visible = OptionsPanel @ showBrightnessControls;
        text = "Brightness:";
        maxLength = 255;
    };);
    %tab.add(new GuiSliderCtrl(BrightnessSlider) {
        profile = "ETSSliderProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = (55.0 + %posX) @ " " @ %posY;
        extent = "97 21";
        minExtent = "8 2";
        sluggishness = -1;
        visible = OptionsPanel @ showBrightnessControls;
        variable = "$UserPref::Video::Exposure";
        altCommand = "BrightnessSlider.applySettings();";
        range = "0.200000 1.0";
        ticks = 10;
        value = $UserPref::Video::Exposure;
        defaultValues = 0.5;
        snapToDefaultRangeRatio = 0.1;
        displayValue = 0;
    };);
    %posY = ((%dPosY * 2.0) + %posY);
    %posX = (%indent - %posX);
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
    0;
    %tab.add(new ""() {
        profile = GuiMLTextCtrl @ "InfoTextProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = (60.0 + %posX) @ " " @ (2.0 + %posY);
        extent = "305 42";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        lineSpacing = 0;
        allowColorChars = 1;
        maxChars = -1;
        text = "Show people's names above their heads";
    };);
    %posY = (%dPosY + %posY);
    %posX = (%indent + %posX);
    0;
    %tab.add(new ""() {
        profile = GuiTextCtrl @ "ETSShadowTextProfile";
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
        position = (60.0 + %posX) @ " " @ %posY;
        extent = "53 16";
        minExtent = "8 2";
        sluggishness = -1;
        visible = 1;
        maxLength = 255;
        maxPopupHeight = 200;
    };);
    %posY = ((%dPosY * 2.0) + %posY);
    %posX = (%indent - %posX);
    %posX = (%dPosX + %posX);
    %posY = (12.0 - %originY);
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
    $UserPref::ETS::ButtonBar::AutoHide.setValue();
    %posY = (%dPosY + %posY);
    AutoHideButtonBarCheckBox;
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
    $UserPref::UI::Radar::AutoOpen.setValue();
    %posY = (%dPosY + %posY);
    AutoOpenLocalMapCheckBox;
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
    %posY = (%dPosY + %posY);
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
    %posY = (%dPosY + %posY);
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
    %posY = (%dPosY + %posY);
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
    %posY = (%dPosY + %posY);
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
    %posY = (%dPosY + %posY);
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
    %posY = (%dPosY + %posY);
    0;
    %tab.add(new ""() {
        profile = GuiVariableWidthButtonCtrl @ "BracketButton15Profile";
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
};
function OptionsPanelTabs::fillTabsTab(%this) {
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
    0;
    %tab.add(new ""() {
        profile = GuiMLTextCtrl @ "GuiDefaultProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = (%indent2 + %posX) @ " " @ %posY;
        extent = "50 42";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        lineSpacing = 0;
        allowColorChars = 1;
        maxChars = -1;
        text = "<color:ffffff>Auto Open";
    };);
    0;
    %tab.add(new ""() {
        profile = GuiMLTextCtrl @ "GuiDefaultProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = (%indent3 + %posX) @ " " @ %posY;
        extent = "50 42";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        lineSpacing = 0;
        allowColorChars = 1;
        maxChars = -1;
        text = "<color:ffffff>Auto Close";
    };);
    %posY = (%dPosY + %posY);
    0;
    %tab.add(new ""() {
        position = GuiControl @ %posX @ " " @ %posY;
        extent = "36 29";
        visible = 1;
    };);
    0;
    %tab.add(new ""() {
        profile = GuiMLTextCtrl @ "GuiDefaultProfile";
        horizSizing = new ""() {
        profile = GuiBitmapCtrl @ "ETSNonModalProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "-5 -5";
        extent = "46 39";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        bitmap = "./buttons/hud_music_n";
    }; @ "right";
        vertSizing = "bottom";
        position = (%indent1 + %posX) @ " " @ (9.0 + %posY);
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
        position = (6.0 + (%indent2 + %posX)) @ " " @ (8.0 + %posY);
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
        position = (6.0 + (%indent3 + %posX)) @ " " @ (8.0 + %posY);
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
    %posY = (%dPosY + %posY);
    0;
    %tab.add(new ""() {
        position = GuiControl @ %posX @ " " @ %posY;
        extent = "36 29";
        visible = 1;
    };);
    0;
    %tab.add(new ""() {
        profile = GuiMLTextCtrl @ "GuiDefaultProfile";
        horizSizing = new ""() {
        profile = GuiBitmapCtrl @ "ETSNonModalProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "-5 -5";
        extent = "46 39";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        bitmap = "./buttons/hud_affinity_n";
    }; @ "right";
        vertSizing = "bottom";
        position = (%indent1 + %posX) @ " " @ (9.0 + %posY);
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
        position = (6.0 + (%indent2 + %posX)) @ " " @ (8.0 + %posY);
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
        position = (6.0 + (%indent3 + %posX)) @ " " @ (8.0 + %posY);
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
    %posY = (%dPosY + %posY);
    0;
    %tab.add(new ""() {
        position = GuiControl @ %posX @ " " @ %posY;
        extent = "36 29";
        visible = 1;
    };);
    0;
    %tab.add(new ""() {
        profile = GuiMLTextCtrl @ "GuiDefaultProfile";
        horizSizing = new ""() {
        profile = GuiBitmapCtrl @ "ETSNonModalProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "-5 -5";
        extent = "46 39";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        bitmap = "./buttons/hud_scores_n";
    }; @ "right";
        vertSizing = "bottom";
        position = (%indent1 + %posX) @ " " @ (9.0 + %posY);
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
        position = (6.0 + (%indent2 + %posX)) @ " " @ (8.0 + %posY);
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
        position = (6.0 + (%indent3 + %posX)) @ " " @ (8.0 + %posY);
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
    %posY = (%dPosY + %posY);
    0;
    %tab.add(new ""() {
        position = GuiControl @ %posX @ " " @ %posY;
        extent = "36 29";
        visible = 1;
    };);
    0;
    %tab.add(new ""() {
        profile = GuiMLTextCtrl @ "GuiDefaultProfile";
        horizSizing = new ""() {
        profile = GuiBitmapCtrl @ "ETSNonModalProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "-5 -5";
        extent = "46 39";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        bitmap = "./buttons/hud_word_n";
    }; @ "right";
        vertSizing = "bottom";
        position = (%indent1 + %posX) @ " " @ (9.0 + %posY);
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
        position = (6.0 + (%indent2 + %posX)) @ " " @ (8.0 + %posY);
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
        position = (6.0 + (%indent3 + %posX)) @ " " @ (8.0 + %posY);
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
    %posY = (%dPosY + %posY);
    %posX = (%dPosX + %posX);
    %posY = %originY;
    %posY = (%dPosY + %posY);
    0;
    %tab.add(new ""() {
        profile = GuiMLTextCtrl @ "InfoTextProfile";
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
    %posY = (%dPosY + %posY);
    0;
    %tab.add(new ""() {
        profile = GuiVariableWidthButtonCtrl @ "BracketButton15Profile";
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
};
function OptionsPanelTabs::fillVIPTab(%this) {
    %tab = %this.getTabWithName("vip");
    %originX = 10;
    %originY = 10;
    %posX = %originX;
    %posY = %originY;
    %dPosX = 180;
    %dPosY = 20;
    %indent = 10;
    0;
    %tab.add(new ""() {
        profile = GuiMLTextCtrl @ "InfoTextProfile";
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
    %posY = (%dPosY + %posY);
    %tab.add(new GuiControl(ForceFieldCtrls) {
        position = %posX @ " " @ %posY;
        extent = 94 @ " " @ (5.0 * %dPosY);
        visible = $gPerformerMode;
    };);
    %posY = ((%dPosY * 6.0) + %posY);
    new GuiRadioCtrl(performerPanelRadioButtonForceField3) {
        profile = new GuiRadioCtrl(performerPanelRadioButtonForceField2) {
        profile = new GuiRadioCtrl(performerPanelRadioButtonForceField1) {
        profile = new GuiRadioCtrl(performerPanelRadioButtonForceField0) {
        profile = new ""() {
        position = GuiTextCtrl @ 0 @ " " @ (0.0 * %dPosY);
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
        position = 10 @ " " @ (1.0 * %dPosY);
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
        position = 10 @ " " @ (2.0 * %dPosY);
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
        position = 10 @ " " @ (3.0 * %dPosY);
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
        position = 10 @ " " @ (4.0 * %dPosY);
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
    1.setValue();
    %tab.add(new GuiCheckBoxCtrl(HUDHideChatCheckBox) {
        profile = performerPanelRadioButtonForceField0 @ "ETSCheckBoxProfile";
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
    %posY = (%dPosY + %posY);
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
        position = (150.0 + %posX) @ " " @ %posY;
        extent = "150 18";
        minExtent = "8 2";
        sluggishness = -1;
        variable = "$Pref::DF::ShowDefaults";
        command = "OptionsPanelTabs.onChangedDFShowDefaults();";
        text = "Show Default Ads";
        groupNum = -1;
        buttonType = "ToggleButton";
    };);
    %posY = (%dPosY + %posY);
    %posX = (%indent + %posX);
    new ""() {
        profile = GuiVariableWidthButtonCtrl @ "BracketButton13Profile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "0 0";
        extent = "40 18";
        minExtent = "8 2";
        sluggishness = -1;
        command = "DFDebugPrev();";
        text = "Prev";
        buttonType = "PushButton";
    };
    new ""() {
        profile = GuiVariableWidthButtonCtrl @ "BracketButton13Profile";
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
    };
    %tab.add(new GuiControl(geDFDebugCtrls) {
        horizSizing = "right";
        vertSizing = "bottom";
        position = %posX @ " " @ %posY;
        extent = "300 20";
        visible = $Pref::DF::debugMode;
    };);
    %posY = (%dPosY + %posY);
    new GuiTextCtrl(geDFDebugStatusText) {
        profile = new ""() {
        profile = GuiVariableWidthButtonCtrl @ "BracketButton13Profile";
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
    %posX = (%indent - %posX);
    %posX = (%dPosX + %posX);
    %posY = %originY;
    %posY = (%dPosY + %posY);
    new GuiTextEditCtrl(FarNameOpacityTextEditCtrl) {
        profile = new ""() {
        profile = GuiTextCtrl @ "ETSShadowTextProfile";
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
    };
    %tab.add(new GuiControl(FarNameOpacityCtrl) {
        profile = "GuiDefaultProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = %posX @ " " @ %posY;
        extent = "190 70";
        minExtent = "8 2";
        visible = 0;
    };);
    %posY = ((4.0 * %dPosY) + %posY);
    new ""() {
        profile = GuiMLTextCtrl @ "InfoTextProfile";
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
        extent = 190 @ " " @ (3.0 * %dPosY);
        visible = $gPerformerMode;
    };);
    0;
    %tab.add(new ""() {
        profile = GuiVariableWidthButtonCtrl @ "BracketButton15Profile";
        horizSizing = new GuiRadioCtrl(optionsPanelAlertOnErrorCheckBox) {
        profile = new GuiRadioCtrl(optionsPanelAlertOnWarningCheckBox) {
        profile = new ""() {
        position = GuiTextCtrl @ 0 @ " " @ (0.0 * %dPosY);
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
        position = 10 @ " " @ (1.0 * %dPosY);
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
        position = 80 @ " " @ (1.0 * %dPosY);
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
    }; @ "right";
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
    if (($Defaults::UserPref::Audio::mute != $UserPref::Audio::mute)) {
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
    $Defaults::UserPref::ETS::ButtonBar::AutoHide.setAutoHiding();
    if (isObject(gMessageBoxDontShow)) {
        gMessageBoxDontShow.clear();
    }
    OptionsPanel.readSettings();
    schedulePersist();
};
function OptionsPanelTabs::restoreTabsDefaults(%this) {
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
    !($Pref::DF::debugMode).setValue();
    geDFDebugMode.performClick();
    !($Pref::DF::showDefaults).setValue();
    geDFShowDefaults.performClick();
    OptionsPanel.readSettings();
    schedulePersist();
};
function OptionsPanelTabs::onChangedHideChat(%this) {
    if ($UserPref::Display::hideChat) {
        0.close();
        SystemMessageDialog.close();
    }
    if ((0.0 > ConvBubVecCtrlMsgVec.getNumLines())) {
        ConvBub.open();
    }
    schedulePersist();
};
function OptionsPanelTabs::onChangedDFDebugMode(%this) {
    $Pref::DF::debugMode.setVisible();
    DF_DebugMode($Pref::DF::debugMode);
};
function OptionsPanelTabs::onChangedDFShowDefaults(%this) {
    DF_ShowDefaults($Pref::DF::showDefaults);
};
function OptionsPanelTabs::onChangedShowNames(%this) {
    $UserPref::Display::hideNames = !(HUDShowNamesCheckBox.getValue());
    !($UserPref::Display::hideNames).setVisible();
};
function OptionsPanel::open(%this) {
    %this.readSettings();
    %this.setVisible(1);
    %this.focusAndRaise();
    if ($player) {
        if ($player.rolesPermissionCheckNoWarn("quietHUD")) {
        }
    }
    if ($player.rolesPermissionCheckNoWarn("farNameOpacity")) {
        "vip".showTabWithName();
    }
    if ((OptionsPanelTabs @ " " @ OptionsPanelTabs.getCurrentTab().name $= "vip")) {
        0.selectTabAtIndex();
    }
    "vip".hideTabWithName();
    OptionsPanelTabs.selectCurrentTab();
};
function OptionsPanel::close(%this) {
    %this.applySettings();
    %this.setVisible(0);
    PlayGui.focusTopWindow();
    return 1;
};
function OptionsPanel::wakeUp(%this) {
    OptionsPanelTabs.wakeUp();
};
function OptionsPanel::Initialize(%this) {
    OptionsPanelTabs.setup();
    TwoPlayerActionsFriendsPopup.clear();
    " Accept".add(0);
    " Ask Me".add(1);
    " Decline".add(2);
    TwoPlayerActionsStrangersPopup.clear();
    " Accept".add(0);
    " Ask Me".add(1);
    " Decline".add(2);
    GiftsFriendsPopup.clear();
    " Accept".add(0);
    " Ask Me".add(1);
    " Decline".add(2);
    GiftsStrangersPopup.clear();
    " Accept".add(0);
    " Ask Me".add(1);
    " Decline".add(2);
    RenderQualityPopup.clear();
    " Low".add(0);
    " Medium".add(1);
    " High".add(2);
    " Automatic".add(3);
    ShadowDetailSizePopup.clear();
    " Low".add(0);
    " Medium".add(1);
    " High".add(2);
    " Automatic".add(3);
    VisibleDistancePopup.clear();
    " Low".add(0);
    " Medium".add(1);
    " High".add(2);
    " Automatic".add(3);
    WaterReflectionModePopup.clear();
    " Low".add(0);
    " High".add(2);
    " Automatic".add(3);
    ExposureFilterModePopup.clear();
    " Low".add(0);
    " High".add(2);
    " Automatic".add(3);
    FontSizePopup.clear();
    " Small".add(0);
    " Medium".add(1);
    " Large".add(2);
    %this.readSettings();
};
function schedulePersist() {
    if ((0.0 != $OptionsPanel::scheduledPersistID)) {
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
    $Player::Name.setProperty("volumeMaster", $UserPref::Audio::masterVolume);
    $Player::Name.setProperty("volumeMusic", $UserPref::Audio::channelVolume1);
    $Player::Name.setProperty("volumeSfx", $UserPref::Audio::channelVolume2);
    $Player::Name.setProperty("volumeMute", $UserPref::Audio::mute);
    $Player::Name.setProperty("flashIncomingChat", $UserPref::Audio::NotifyChat);
    $Player::Name.setProperty("flashIncomingWhisper", $UserPref::Audio::NotifyWhisper);
    $Player::Name.setProperty("showTyping", $UserPref::Chat::ShowTyping);
    $Player::Name.setProperty("farOpacity", $UserPref::Display::farNameOpacity);
    $Player::Name.setProperty("hideChat", $UserPref::Display::hideChat);
    $Player::Name.setProperty("hideNames", $UserPref::Display::hideNames);
    $Player::Name.setProperty("hideButtonBar", $UserPref::ETS::ButtonBar::AutoHide);
    $Player::Name.setProperty("autoOpenTabMusic", gUserPropMgrClient);
    $Player::Name.setProperty("autoCloseTabMusic", gUserPropMgrClient);
    $Player::Name.setProperty("autoOpenTabAffinity", gUserPropMgrClient);
    $Player::Name.setProperty("autoCloseTabAffinity", gUserPropMgrClient);
    $Player::Name.setProperty("autoOpenTabScores", gUserPropMgrClient);
    $Player::Name.setProperty("autoCloseTabScores", gUserPropMgrClient);
    $Player::Name.setProperty("autoOpenTabWord", gUserPropMgrClient);
    $Player::Name.setProperty("autoCloseTabWord", gUserPropMgrClient);
    $Player::Name.setProperty("refuseTeleports", $UserPref::Player::TeleportBlock);
    $Player::Name.setProperty("refuseWhispers", $UserPref::Player::WhisperBlock);
    $Player::Name.setProperty("refuseYells", $UserPref::Player::YellBlock);
    $Player::Name.setProperty("emotesPermissionsFriends", $UserPref::Player::EmotesPermissionFriends);
    $Player::Name.setProperty("emotesPermissionsStrangers", $UserPref::Player::EmotesPermissionStrangers);
    $Player::Name.setProperty("giftsPermissionsFriends", $UserPref::Player::GiftsPermissionFriends);
    $Player::Name.setProperty("giftsPermissionsStrangers", $UserPref::Player::GiftsPermissionStrangers);
    $Player::Name.setProperty("awayMessage", $UserPref::Player::awayMessage);
    $Player::Name.setProperty("autoReplyToWhipsers", $UserPref::Player::autoReplyToWhispersWhenAway);
    $Player::Name.setProperty("filterProfanity", $UserPref::Player::filterProfanity);
    $Player::Name.setProperty("playerMood", $UserPref::Player::Genre);
    $Player::Name.setProperty("avatarHeight", $UserPref::Player::height);
    $Player::Name.setProperty("showOnRadar", $UserPref::Player::showOnRadar);
    $Player::Name.setProperty("flashTaskBar", $UserPref::UI::FlashTaskBar);
    $Player::Name.setProperty("radarAutoOpen", $UserPref::UI::Radar::AutoOpen);
    $Player::Name.setProperty("showAccountHud", $UserPref::UI::ShowAccountHud);
    $Player::Name.setProperty("showTooltips", $UserPref::UI::ShowTooltips);
    $Player::Name.setProperty("videoExposure", $UserPref::Video::Exposure);
    $Player::Name.setProperty("videoNameSize", $UserPref::Video::shapeNameFontSize);
    $Player::Name.setProperty("videoRenderQuality", $UserPref::Video::renderQualitySetting);
    $Player::Name.setProperty("videoShadowQuality", $UserPref::Video::shadowQualitySetting);
    $Player::Name.setProperty("videoVisibleDistance", $UserPref::Video::visibledistanceQualitySetting);
    $Player::Name.setProperty("videoWaterReflection", $UserPref::Video::waterreflectionQualitySetting);
    $Player::Name.setProperty("videoConstrainWindowDimensions", $UserPref::Video::ConstrainWindowDimensions);
    $Player::Name.setProperty("alertOnLogWarning", $UserPref::debug::alertOnLogWarning);
    $Player::Name.setProperty("alertOnLogError", $UserPref::debug::alertOnLogError);
    %maxNumberKeyCombos = getFieldCount($Defaults::UserPref::emotes::defaultKeyCombinations);
    gUserPropMgrClient;
    %i = (1.0 - %maxNumberKeyCombos);
    gUserPropMgrClient;
    if ((0.0 >= %i)) {
        %keyCombo = getField($Defaults::UserPref::emotes::defaultKeyCombinations, %i);
        gUserPropMgrClient;
        $Player::Name.setProperty("favoriteActionsKey_f_" @ %keyCombo, %keyCombo[gUserPropMgrClient @ $UserPref::emotes TAB "f" @ %keyCombo]);
        $Player::Name.setProperty("favoriteActionsKey_m_" @ %keyCombo, %keyCombo[gUserPropMgrClient @ $UserPref::emotes TAB "m" @ %keyCombo]);
        %i = (1.0 - %i);
        gUserPropMgrClient;
    }
    $OptionsPanel::scheduledPersistID = 0;
    (0.0 >= %i);
    legacyPersistOptionsPanelSettingsToManager();
};
function LegacySaveSettingsRequest::onDone(%this) {
    log("communication", "info", "settings successfully saved to manager");
    %this.schedule(0, "delete");
};
function LegacySaveSettingsRequest::onError(%this, %unused, %errName) {
    log("communication", "info", "error saving settings: " @ %errName);
    %this.schedule(0, "delete");
};
function legacyPersistOptionsPanelSettingsToManager() {
    if (!(haveValidManagerHost())) {
    }
    if (!(haveValidToken())) {
        return;
    }
    %request = new ManagerRequest(LegacySaveSettingsRequest);;
    if (isObject(MissionCleanup)) {
        %request.add();
    }
    %url = $Net::ClientServiceURL @ "/SaveSettings";
    MissionCleanup;
    %url = %url @ "?user=" @ urlEncode($Player::Name);
    %url = %url @ "&token=" @ urlEncode($Token);
    %url = %url @ "&settingsMapCount=" @ 3;
    %url = %url @ "&settingsMap0.key=refuseTeleports&settingsMap0.value=" @ urlEncode($UserPref::Player::TeleportBlock);
    %url = %url @ "&settingsMap1.key=refuseWhispers&settingsMap1.value=" @ urlEncode($UserPref::Player::WhisperBlock);
    %url = %url @ "&settingsMap2.key=awayMessage&settingsMap2.value=" @ urlEncode($UserPref::Player::awayMessage);
    log("communication", "debug", "save settings: " @ %url);
    %request.setURL(%url);
    %request.start();
};
function OptionsPanel::readSettings(%this) {
    $UserPref::Player::awayMessage.setValue();
    DefaultAwayMsgEdit.applySettings();
    $UserPref::Chat::ShowTyping.setValue();
    $UserPref::Player::filterProfanity.setValue();
    $UserPref::Player::TeleportBlock.setValue();
    $UserPref::Player::WhisperBlock.setValue();
    $UserPref::Player::YellBlock.setValue();
    $UserPref::Player::filterProfanity.setValue();
    $UserPref::Player::showOnRadar.setValue();
    $UserPref::Player::EmotesPermissionFriends.SetSelected();
    $UserPref::Player::EmotesPermissionStrangers.SetSelected();
    $UserPref::Player::GiftsPermissionFriends.SetSelected();
    $UserPref::Player::GiftsPermissionStrangers.SetSelected();
    $UserPref::Audio::masterVolume.setValue();
    VolumeSlider.applySettings();
    $UserPref::Audio::channelVolume1.setValue();
    VolumeMusicSlider.applySettings();
    $UserPref::Audio::channelVolume2.setValue();
    VolumeSFXSlider.applySettings();
    $UserPref::Audio::mute.setValue();
    $UserPref::Audio::NotifyChat.setValue();
    $UserPref::Audio::NotifyWhisper.setValue();
    $UserPref::Video::Exposure.setValue();
    BrightnessSlider.applySettings();
    $UserPref::Video::renderQualitySetting.SetSelected();
    $UserPref::Video::shadowQualitySetting.SetSelected();
    $UserPref::Video::visibledistanceQualitySetting.SetSelected();
    $UserPref::Video::waterreflectionQualitySetting.SetSelected();
    $UserPref::Video::exposureQualitySetting.SetSelected();
    $UserPref::Video::shapeNameFontSize.SetSelected();
    $UserPref::UI::Radar::AutoOpen.setValue();
    $UserPref::ETS::ButtonBar::AutoHide.setValue();
    $UserPref::UI::FlashTaskBar.setValue();
    $UserPref::UI::ShowAccountHud.setValue();
    $UserPref::UI::ShowTooltips.setValue();
    %curScreenMode = getRes();
    OptionsVisualShowTooltipsCheckbox;
    %curScreenScale = (16.0 / getWord(%curScreenMode, 0));
    ShowAccountHudCheckBox;
    %curScreenScale[$UserPref::HudTabs::AutoOpen @ "music"].setValue();
    HudMusicAutoCloseCheckBox.setValue();
    HudAffinityAutoOpenCheckBox.setValue();
    HudAffinityAutoCloseCheckBox.setValue();
    HudScoresAutoOpenCheckBox.setValue();
    HudScoresAutoCloseCheckBox.setValue();
    HudWordAutoOpenCheckBox.setValue();
    HudWordAutoCloseCheckBox.setValue();
    !($UserPref::Display::hideNames).setValue();
    $UserPref::Display::hideChat.setValue();
    $UserPref::Display::farNameOpacity.setValue();
    $UserPref::debug::alertOnLogWarning.setValue();
    $UserPref::debug::alertOnLogError.setValue();
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
        %flag.setVisible();
        %flag.setVisible();
    }
};
function DefaultAwayMsgEdit::applySettings(%this) {
    %this.setValue(trim(%this.getValue()));
    %newAwayMsg = %this.getValue();
    if ((%newAwayMsg $= "")) {
        if (($UserPref::Player::awayMessage $= "")) {
            $UserPref::Player::awayMessage = $Pref::Player::defaultAwayMessage;
        }
        %this.setValue($UserPref::Player::awayMessage);
    }
    $UserPref::Player::awayMessage = %newAwayMsg;
    if (isIdle()) {
        setIdle(1, $UserPref::Player::awayMessage);
    }
    schedulePersist();
};
function VolumeSlider::applySettings(%this) {
    $UserPref::Audio::masterVolume = %this.value;
    VolumeSlider;
    %multiplier = $UserPref::Audio::mute ? 0 : 1;
    alxListenerf(($UserPref::Audio::masterVolume * %multiplier));
    fmodSetMasterVolume(($UserPref::Audio::channelVolume1 * ($UserPref::Audio::masterVolume * %multiplier)));
    if (Using_FFMPEG()) {
        ffmpegSetMasterVolume(($UserPref::Audio::channelVolume1 * ($UserPref::Audio::masterVolume * %multiplier)));
    }
    schedulePersist();
};
function VolumeMusicSlider::applySettings(%this) {
    $UserPref::Audio::channelVolume1 = %this.value;
    alxSetChannelVolume(1, $UserPref::Audio::channelVolume1);
    %multiplier = $UserPref::Audio::mute ? 0 : 1;
    fmodSetMasterVolume(($UserPref::Audio::channelVolume1 * ($UserPref::Audio::masterVolume * %multiplier)));
    if (Using_FFMPEG()) {
        ffmpegSetMasterVolume(($UserPref::Audio::channelVolume1 * ($UserPref::Audio::masterVolume * %multiplier)));
    }
    schedulePersist();
};
function VolumeSFXSlider::applySettings(%this) {
    $UserPref::Audio::channelVolume2 = %this.value;
    VolumeSFXSlider;
    alxSetChannelVolume(2, $UserPref::Audio::channelVolume2);
    schedulePersist();
};
function TwoPlayerActionsFriendsPopup::onSelect(%this, %id, %unused) {
    $UserPref::Player::EmotesPermissionFriends = %id;
    if ((%id < $UserPref::Player::EmotesPermissionStrangers)) {
        %id.SetSelected();
    }
    schedulePersist();
};
function TwoPlayerActionsStrangersPopup::onSelect(%this, %id, %unused) {
    $UserPref::Player::EmotesPermissionStrangers = %id;
    if ((%id > $UserPref::Player::EmotesPermissionFriends)) {
        %id.SetSelected();
    }
    schedulePersist();
};
function GiftsFriendsPopup::onSelect(%this, %id, %unused) {
    $UserPref::Player::GiftsPermissionFriends = %id;
    if ((%id < $UserPref::Player::GiftsPermissionStrangers)) {
        %id.SetSelected();
    }
    schedulePersist();
};
function GiftsStrangersPopup::onSelect(%this, %id, %unused) {
    $UserPref::Player::GiftsPermissionStrangers = %id;
    if ((%id > $UserPref::Player::GiftsPermissionFriends)) {
        %id.SetSelected();
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
    if ((0.0 < %val)) {
    }
    if ((2.0 > %val)) {
        error("Unknown font size:" @ " " @ %val);
        return;
    }
    $UserPref::Video::shapeNameFontSize = %val;
    if ((0.0 == %val)) {
        // unhandled opcode 7130 at 0x0000524D
        %val = SmallShapeNameHudProfile;
        // unhandled opcode 7165 at 0x00005253
        %val = BoldSmallShapeNameHudProfile;
    }
    if ((1.0 == %val)) {
        // unhandled opcode 7130 at 0x00005263
        %val = MediumShapeNameHudProfile;
        // unhandled opcode 7165 at 0x00005269
        %val = BoldMediumShapeNameHudProfile;
    }
    if ((2.0 == %val)) {
        // unhandled opcode 7130 at 0x00005279
        %val = LargeShapeNameHudProfile;
        // unhandled opcode 7165 at 0x0000527F
        %val = BoldLargeShapeNameHudProfile;
    }
    %prof.setProfile();
    %this.otherProfile = %otherProf @ TheShapeNameHud;
    TheShapeNameHud;
};
setShapeNameFontSize($UserPref::Video::shapeNameFontSize);
function toggleAutoHideButtonBar() {
    AutoHideButtonBarCheckBox.getValue().setAutoHiding();
    schedulePersist();
};
function updateHudTabsHiding() {
    %i = 0;
    if ((%this.numTabs < %i)) {
        %tab = %i.getTabAtIndex();
        HudTabs;
        if (!(HudTabs @ " " @ %tab.name $= "tutorial")) {
            %tab.autoHide = %tab[$UserPref::HudTabs::AutoClose @ %tab.name];
        }
        %i = (1.0 + %i);
    }
    %currentTab = HudTabs.getCurrentTab();
    (%tab.numTabs < %i);
    if ((HudTabs @ " " @ %currentTab $= "")) {
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
    gSetField($player, "");
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
    "social".selectTabWithName();
    1.makeFirstResponder();
    DefaultAwayMsgEdit.selectAll();
};
