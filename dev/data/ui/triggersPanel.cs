function TriggersPanel::toggle(%this) {
    %this.showRaiseOrHide();
};
function TriggersPanel::open(%this) {
    if (!($player.rolesPermissionCheckNoWarn("debugActive"))) {
        return;
    }
    if (!(%this.isVisible())) {
        %this.setVisible(1);
        %this.focusAndRaise();
    }
    TriggersPanelTriggerPopup.clear();
    TriggersPanelStreamPopup.clear();
    commandToServer('GetMusicTriggers', addTaggedString("GetMusicTriggerNamesCallback"));
    commandToServer('GetStreamIDs', addTaggedString("GetStreamIDsCallback"));
};
function TriggersPanel::close(%this) {
    %this.setVisible(0);
    playGui.focusTopWindow();
    return 1;
};
function TriggersPanel::setStream(%this) {
    %trigger = TriggersPanelTriggerPopup.getValue();
    %stream = TriggersPanelStreamPopup.getValue();
    if (!(%trigger $= "")) {
    }
    if (!(%stream $= "")) {
        MessageBoxYesNo("Set Stream", "Are you sure you want to set the stream for trigger \"" @ %trigger @ "\" to \"" @ %stream @ "\"?", "TriggersPanel.setStreamReally();", "");
    }
};
function TriggersPanel::setStreamReally(%this) {
    %trigger = TriggersPanelTriggerPopup.getValue();
    %stream = TriggersPanelStreamPopup.getValue();
    if (!(%trigger $= "")) {
    }
    if (!(%stream $= "")) {
        log("Communication", "info", "Setting stream for trigger " @ %trigger @ " to " @ %stream);
        commandToServer('SetMusicStreamMapping', %trigger, %stream);
        triggersPanelTextList.clear();
        "".setValue();
        commandToServer('reportTriggers', $DevPref::reportTriggers);
    }
};
function TriggersPanel::setLocalURLMapping(%this) {
    %stream = $TriggersPanel::NewStreamName;
    %newUrl = $TriggersPanel::NewURL;
    if (!(%stream $= "")) {
    }
    if (!(%newUrl $= "")) {
        MessageBoxYesNo("Set Stream", "Are you sure you want to set stream \"" @ %stream @ "\" to \"" @ %newUrl @ "\"?", "TriggersPanel.setLocalURLMappingReally();", "");
    }
    MessageBoxOK("Test URL", "Please provide a value for both Stream and URL", "");
};
function TriggersPanel::setLocalURLMappingReally(%this) {
    %stream = $TriggersPanel::NewStreamName;
    %newUrl = $TriggersPanel::NewURL;
    if (!(%stream $= "")) {
    }
    if (!(%newUrl $= "")) {
        log("communication", "info", "Stream " @ %stream @ " " @ "will now be mapped to " @ %newUrl @ " " @ "on this server only");
        commandToServer('SetUrl', %stream, %newUrl);
        TriggersPanelStreamPopup.clear();
        TriggersPanelTriggerPopup.clear();
        "".setValue();
        commandToServer('GetMusicTriggers', addTaggedString("GetMusicTriggerNamesCallback"));
        commandToServer('GetStreamIDs', addTaggedString("GetStreamIDsCallback"));
    }
};
function TriggersPanel::setURLMapping(%this) {
    %stream = $TriggersPanel::NewStreamName;
    %newUrl = $TriggersPanel::NewURL;
    if (!(%stream $= "")) {
    }
    if (!(%newUrl $= "")) {
        MessageBoxYesNo("Set Stream", "Are you sure you want to set stream \"" @ %stream @ "\" to \"" @ %newUrl @ "\"?", "TriggersPanel.setURLMappingReally();", "");
    }
    MessageBoxOK("Test URL", "Please provide a value for both Stream and URL", "");
};
function TriggersPanel::setURLMappingReally(%this) {
    %stream = $TriggersPanel::NewStreamName;
    %newUrl = $TriggersPanel::NewURL;
    if (!(%stream $= "")) {
    }
    if (!(%newUrl $= "")) {
        log("communication", "info", "Stream " @ %stream @ " " @ "will now be mapped to " @ %newUrl);
        %this.urlsToEnvmanager(%stream, %newUrl);
    }
};
function TriggersPanel::urlsToEnvmanager(%this, %stream, %musicURL) {
    0;
    %request = new ""() {
        className = ManagerRequest @ "ChangeStreamIDMappingRequest";
    };
    if (isObject(MissionCleanup)) {
        %request.add();
    }
    %url = $Net::ClientServiceURL @ "/UpdateMusicStreamIDMapping" @ "?user=" @ urlEncode($Player::Name) @ "&token=" @ urlEncode($Token);
    MissionCleanup;
    %url = %url @ "&streamID=" @ urlEncode(%stream);
    %url = %url @ "&mountURL=" @ urlEncode(%musicURL);
    log("network", "info", getScopeName() @ ":" @ %url);
    %request.setURL(%url);
    %request.start();
};
function ChangeStreamIDMappingRequest::onDone(%this) {
    %status = findRequestStatus(%this);
    log("network", "info", getScopeName() @ ":" @ %status);
    if ((%status $= "fail")) {
        warn("network", getScopeName() @ " request failed: " @ %this.getValue("statusMessage"));
    }
    TriggersPanelStreamPopup.clear();
    TriggersPanelTriggerPopup.clear();
    "".setValue();
    commandToServer('GetMusicTriggers', addTaggedString("GetMusicTriggerNamesCallback"));
    commandToServer('GetStreamIDs', addTaggedString("GetStreamIDsCallback"));
    %this.schedule(0, "delete");
};
function TriggersPanel::testURL(%this) {
    if ((TriggersPanelTestUrl.getText() $= "Test URL")) {
        "Stop Test".setText();
    }
    if ((TriggersPanelTestUrl @ " " @ TriggersPanelTestUrl.getText() $= "Stop Test")) {
        "TriggersPanelTest".popStream();
        "Test URL".setText();
        return TriggersPanelTestUrl;
    }
    %newUrl = $TriggersPanel::NewURL;
    if (!(%newUrl $= "")) {
        "TriggersPanelTest".pushStreamWithVolume(%newUrl, 0.8, "");
    }
    MessageBoxOK("Test URL", "Please specify a URL to test.", "");
};
function TriggersPanel::selectTrigger(%this) {
    %selected = triggersPanelTextList.getValue();
    if ((0.0 < strstr(%selected, "MusicTrigger"))) {
        return;
    }
    %wc = getWordCount(%selected);
    %trigger = getWord(%selected, (3.0 - %wc));
    %stream = getWord(%selected, (1.0 - %wc));
    %trigger.setValue();
    %stream.setValue();
    commandToServer('getUrl', %stream, addTaggedString("GetTriggersPanelURLCallback"));
};
function setTriggerReportingState() {
    if ($DevPref::reportTriggers) {
        triggersPanelTextList.clear();
    }
    commandToServer('reportTriggers', $DevPref::reportTriggers);
};
function setShowOnlyMusicTriggers() {
    triggersPanelTextList.clear();
    commandToServer('reportTriggers', $DevPref::reportTriggers);
};
function ClientCmdTriggerSet(%triggerDesc) {
    %isMusicTrigger = (0.0 >= strstr(%triggerDesc, "MusicTrigger"));
    if (!($DevPref::showOnlyMusicTriggers)) {
    }
    %displayTrigger = %isMusicTrigger;
    if (%displayTrigger) {
        %idx = triggersPanelTextList.rowCount();
        %idx.addRow(%triggerDesc, %idx);
        if ($DevPref::autoOpenTriggers) {
            TriggersPanel.open();
        }
    }
};
function ClientCmdTriggerUnset(%triggerDesc) {
    %idx = %triggerDesc.findTextIndex();
    triggersPanelTextList;
    if ((0.0 >= %idx)) {
        %idx.removeRow();
    }
    warn(getScopeName() @ "Couldn't match a trigger description to delete it! desc = " @ %triggerDesc);
};
function ClientCmdTriggerSetByList(%set) {
    %num = getFieldCount(%set);
    %rowNum = triggersPanelTextList.rowCount();
    %n = 0;
    if ((%num < %n)) {
        %desc = getField(%set, %n);
        %isMusicTrigger = (0.0 >= strstr(%desc, "MusicTrigger"));
        if (!($DevPref::showOnlyMusicTriggers)) {
        }
        %displayTrigger = %isMusicTrigger;
        if (%displayTrigger) {
            (%rowNum + %n).addRow(%desc, (%rowNum + %n));
        }
        %n = (1.0 + %n);
        triggersPanelTextList;
    }
    if ($DevPref::autoOpenTriggers) {
        TriggersPanel.open();
    }
};
function clientCmdGetMusicTriggerNamesCallback(%names) {
    %count = getFieldCount(%names);
    %i = 0;
    if ((%count < %i)) {
        %name = getField(%names, %i);
        if (!(%name $= "")) {
            %name.add();
        }
        %i = (1.0 + %i);
        TriggersPanelTriggerPopup;
    }
};
function clientCmdGetStreamIDsCallback(%names) {
    %count = getFieldCount(%names);
    %i = 0;
    if ((%count < %i)) {
        %name = getField(%names, %i);
        if (!(%name $= "")) {
            %name.add();
        }
        %i = (1.0 + %i);
        TriggersPanelStreamPopup;
    }
};
function TriggersPanelStreamPopup::streamSelected(%this) {
    %stream = %this.getValue();
    if (!(%stream $= "")) {
        commandToServer('getUrl', %stream, addTaggedString("GetTriggersPanelURLCallback"));
    }
};
function TriggersPanelTriggerPopup::triggerSelected(%this) {
    %trigger = %this.getValue();
    if (!(%trigger $= "")) {
        commandToServer('GetStreamID', %trigger, addTaggedString("GetStreamIDCallback"));
    }
};
function clientCmdGetStreamIDCallback(%stream) {
    %stream.setValue();
    commandToServer('getUrl', %stream, addTaggedString("GetTriggersPanelURLCallback"));
};
function clientCmdGetTriggersPanelURLCallback(%url) {
    if ((%url $= "")) {
        %url = "<blank>";
    }
    %url.setValue();
};
