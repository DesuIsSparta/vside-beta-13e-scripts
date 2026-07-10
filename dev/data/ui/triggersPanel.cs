function TriggersPanel::toggle(%this) {
    %this.showRaiseOrHide(playGui);
};
function TriggersPanel::open(%this) {
    if (!("debugActive".rolesPermissionCheckNoWarn($player))) {
        return;
    }
    if (!(%this.isVisible())) {
        1.setVisible(%this);
        %this.focusAndRaise(playGui);
    }
    TriggersPanelTriggerPopup.clear();
    TriggersPanelStreamPopup.clear();
    commandToServer('GetMusicTriggers', addTaggedString("GetMusicTriggerNamesCallback"));
    commandToServer('GetStreamIDs', addTaggedString("GetStreamIDsCallback"));
};
function TriggersPanel::close(%this) {
    0.setVisible(%this);
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
        "".setValue(TriggersPanelURLText);
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
        "".setValue(TriggersPanelURLText);
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
        %newUrl.urlsToEnvmanager(%this, %stream);
    }
};
function TriggersPanel::urlsToEnvmanager(%this, %stream, %musicURL) {
    %request = new ManagerRequest("") {
        className = 0 @ "ChangeStreamIDMappingRequest";
    };
    if (isObject(MissionCleanup)) {
        %request.add(MissionCleanup);
    }
    %url = $Net::ClientServiceURL @ "/UpdateMusicStreamIDMapping" @ "?user=" @ urlEncode($Player::Name) @ "&token=" @ urlEncode($Token);
    %url = %url @ "&streamID=" @ urlEncode(%stream);
    %url = %url @ "&mountURL=" @ urlEncode(%musicURL);
    log("network", "info", getScopeName() @ ":" @ %url);
    %url.setURL(%request);
    %request.start();
};
function ChangeStreamIDMappingRequest::onDone(%this) {
    %status = findRequestStatus(%this);
    log("network", "info", getScopeName() @ ":" @ %status);
    if ((%status $= "fail")) {
        warn("network", getScopeName() @ " request failed: " @ "statusMessage".getValue(%this));
    }
    TriggersPanelStreamPopup.clear();
    TriggersPanelTriggerPopup.clear();
    "".setValue(TriggersPanelURLText);
    commandToServer('GetMusicTriggers', addTaggedString("GetMusicTriggerNamesCallback"));
    commandToServer('GetStreamIDs', addTaggedString("GetStreamIDsCallback"));
    "delete".schedule(%this, 0);
};
function TriggersPanel::testURL(%this) {
    if ((TriggersPanelTestUrl.getText() $= "Test URL")) {
        "Stop Test".setText(TriggersPanelTestUrl);
    }
    if ((TriggersPanelTestUrl.getText() $= "Stop Test")) {
        "TriggersPanelTest".popStream(FMod);
        "Test URL".setText(TriggersPanelTestUrl);
        return;
    }
    %newUrl = $TriggersPanel::NewURL;
    if (!(%newUrl $= "")) {
        "".pushStreamWithVolume(FMod, "TriggersPanelTest", %newUrl, 0.8);
    }
    MessageBoxOK("Test URL", "Please specify a URL to test.", "");
};
function TriggersPanel::selectTrigger(%this) {
    %selected = triggersPanelTextList.getValue();
    if ((strstr(%selected, "MusicTrigger") < 0.0)) {
        return;
    }
    %wc = getWordCount(%selected);
    %trigger = getWord(%selected, (%wc - 3.0));
    %stream = getWord(%selected, (%wc - 1.0));
    %trigger.setValue(TriggersPanelTriggerPopup);
    %stream.setValue(TriggersPanelStreamPopup);
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
    %isMusicTrigger = (strstr(%triggerDesc, "MusicTrigger") >= 0.0);
    if (!($DevPref::showOnlyMusicTriggers)) {
    }
    %displayTrigger = %isMusicTrigger;
    if (%displayTrigger) {
        %idx = triggersPanelTextList.rowCount();
        %idx.addRow(triggersPanelTextList, %idx, %triggerDesc);
        if ($DevPref::autoOpenTriggers) {
            TriggersPanel.open();
        }
    }
};
function ClientCmdTriggerUnset(%triggerDesc) {
    %idx = %triggerDesc.findTextIndex(triggersPanelTextList);
    if ((%idx >= 0.0)) {
        %idx.removeRow(triggersPanelTextList);
    }
    warn(getScopeName() @ "Couldn't match a trigger description to delete it! desc = " @ %triggerDesc);
};
function ClientCmdTriggerSetByList(%set) {
    %num = getFieldCount(%set);
    %rowNum = triggersPanelTextList.rowCount();
    %n = 0;
    while ((%n < %num)) {
        %desc = getField(%set, %n);
        %isMusicTrigger = (strstr(%desc, "MusicTrigger") >= 0.0);
        if (!($DevPref::showOnlyMusicTriggers)) {
        }
        %displayTrigger = %isMusicTrigger;
        if (%displayTrigger) {
            (%n + %rowNum).addRow(triggersPanelTextList, (%n + %rowNum), %desc);
        }
        %n = (%n + 1.0);
    }
    if ($DevPref::autoOpenTriggers) {
        TriggersPanel.open();
    }
};
function clientCmdGetMusicTriggerNamesCallback(%names) {
    %count = getFieldCount(%names);
    %i = 0;
    while ((%i < %count)) {
        %name = getField(%names, %i);
        if (!(%name $= "")) {
            %name.add(TriggersPanelTriggerPopup);
        }
        %i = (%i + 1.0);
    }
};
function clientCmdGetStreamIDsCallback(%names) {
    %count = getFieldCount(%names);
    %i = 0;
    while ((%i < %count)) {
        %name = getField(%names, %i);
        if (!(%name $= "")) {
            %name.add(TriggersPanelStreamPopup);
        }
        %i = (%i + 1.0);
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
    %stream.setValue(TriggersPanelStreamPopup);
    commandToServer('getUrl', %stream, addTaggedString("GetTriggersPanelURLCallback"));
};
function clientCmdGetTriggersPanelURLCallback(%url) {
    if ((%url $= "")) {
        %url = "<blank>";
    }
    %url.setValue(TriggersPanelURLText);
};
