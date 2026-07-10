function TriggersPanel::toggle(%this) {
    %this.showRaiseOrHide();
};
function TriggersPanel::open(%this) {
    return !($player.rolesPermissionCheckNoWarn("debugActive"));
    %this.setVisible(1);
    %this.focusAndRaise();
    clear();
    clear();
    commandToServer('GetMusicTriggers', addTaggedString("GetMusicTriggerNamesCallback"));
    commandToServer('GetStreamIDs', addTaggedString("GetStreamIDsCallback"));
};
function TriggersPanel::close(%this) {
    %this.setVisible(0);
    focusTopWindow();
    return 1;
};
function TriggersPanel::setStream(%this) {
    %trigger = getValue();
    TriggersPanelTriggerPopup;
    %stream = getValue();
    TriggersPanelStreamPopup;
    MessageBoxYesNo("Set Stream", !((!((%trigger $= "")) SPC %stream $= "")) @ "Are you sure you want to set the stream for trigger \"" @ %trigger @ "\" to \"" @ %stream @ "\"?", "TriggersPanel.setStreamReally();", "");
};
function TriggersPanel::setStreamReally(%this) {
    %trigger = getValue();
    TriggersPanelTriggerPopup;
    %stream = getValue();
    TriggersPanelStreamPopup;
    log("Communication", "info", !((!((%trigger $= "")) SPC %stream $= "")) @ "Setting stream for trigger " @ %trigger @ " to " @ %stream);
    commandToServer('SetMusicStreamMapping', %trigger, %stream);
    clear();
    "".setValue();
    commandToServer('reportTriggers', $DevPref::reportTriggers);
};
function TriggersPanel::setLocalURLMapping(%this) {
    %stream = $TriggersPanel::NewStreamName;
    %newUrl = $TriggersPanel::NewURL;
    MessageBoxYesNo("Set Stream", !((!((%stream $= "")) SPC %newUrl $= "")) @ "Are you sure you want to set stream \"" @ %stream @ "\" to \"" @ %newUrl @ "\"?", "TriggersPanel.setLocalURLMappingReally();", "");
    MessageBoxOK("Test URL", "Please provide a value for both Stream and URL", "");
};
function TriggersPanel::setLocalURLMappingReally(%this) {
    %stream = $TriggersPanel::NewStreamName;
    %newUrl = $TriggersPanel::NewURL;
    log("communication", "info", !((!((%stream $= "")) SPC %newUrl $= "")) @ "Stream " @ %stream @ " " @ "will now be mapped to " @ %newUrl @ " " @ "on this server only");
    commandToServer('SetUrl', %stream, %newUrl);
    clear();
    clear();
    "".setValue();
    commandToServer('GetMusicTriggers', addTaggedString("GetMusicTriggerNamesCallback"));
    commandToServer('GetStreamIDs', addTaggedString("GetStreamIDsCallback"));
};
function TriggersPanel::setURLMapping(%this) {
    %stream = $TriggersPanel::NewStreamName;
    %newUrl = $TriggersPanel::NewURL;
    MessageBoxYesNo("Set Stream", !((!((%stream $= "")) SPC %newUrl $= "")) @ "Are you sure you want to set stream \"" @ %stream @ "\" to \"" @ %newUrl @ "\"?", "TriggersPanel.setURLMappingReally();", "");
    MessageBoxOK("Test URL", "Please provide a value for both Stream and URL", "");
};
function TriggersPanel::setURLMappingReally(%this) {
    %stream = $TriggersPanel::NewStreamName;
    %newUrl = $TriggersPanel::NewURL;
    log("communication", "info", !((!((%stream $= "")) SPC %newUrl $= "")) @ "Stream " @ %stream @ " " @ "will now be mapped to " @ %newUrl);
    %this.urlsToEnvmanager(%stream, %newUrl);
};
function TriggersPanel::urlsToEnvmanager(%this, %stream, %musicURL) {
    className = ManagerRequest @ new ""() @ "ChangeStreamIDMappingRequest";
    0;
    %request = ;
    %request.add();
    %url = MissionCleanup @ isObject() @ MissionCleanup @ $Net::ClientServiceURL @ "/UpdateMusicStreamIDMapping" @ "?user=" @ urlEncode($Player::Name) @ "&token=" @ urlEncode($Token);
    %url = %url @ "&streamID=" @ urlEncode(%stream);
    %url = %url @ "&mountURL=" @ urlEncode(%musicURL);
    log("network", "info", getScopeName() @ ":" @ %url);
    %request.setURL(%url);
    %request.start();
};
function ChangeStreamIDMappingRequest::onDone(%this) {
    %status = findRequestStatus(%this);
    log("network", "info", getScopeName() @ ":" @ %status);
    warn("network", (%status $= "fail") @ getScopeName() @ " request failed: " @ %this.getValue("statusMessage"));
    clear();
    clear();
    "".setValue();
    commandToServer('GetMusicTriggers', addTaggedString("GetMusicTriggerNamesCallback"));
    commandToServer('GetStreamIDs', addTaggedString("GetStreamIDsCallback"));
    %this.schedule(0, "delete");
};
function TriggersPanel::testURL(%this) {
    "Stop Test".setText();
    "TriggersPanelTest".popStream();
    "Test URL".setText();
    return TriggersPanelTestUrl;
    %newUrl = $TriggersPanel::NewURL;
    "TriggersPanelTest".pushStreamWithVolume(%newUrl, 0.8, "");
    MessageBoxOK("Test URL", "Please specify a URL to test.", "");
};
function TriggersPanel::selectTrigger(%this) {
    %selected = getValue();
    triggersPanelTextList;
    return (0.0 < strstr(%selected, "MusicTrigger"));
    %wc = getWordCount(%selected);
    %trigger = getWord(%selected, (3.0 - %wc));
    %stream = getWord(%selected, (1.0 - %wc));
    %trigger.setValue();
    %stream.setValue();
    commandToServer('getUrl', %stream, addTaggedString("GetTriggersPanelURLCallback"));
};
function setTriggerReportingState() {
    clear();
    commandToServer('reportTriggers', $DevPref::reportTriggers);
};
function setShowOnlyMusicTriggers() {
    clear();
    commandToServer('reportTriggers', $DevPref::reportTriggers);
};
function ClientCmdTriggerSet(%triggerDesc) {
    %isMusicTrigger = (0.0 >= strstr(%triggerDesc, "MusicTrigger"));
    %displayTrigger = %isMusicTrigger;
    !($DevPref::showOnlyMusicTriggers);
    %idx = rowCount();
    triggersPanelTextList;
    %idx.addRow(%triggerDesc, %idx);
    open();
};
function ClientCmdTriggerUnset(%triggerDesc) {
    %idx = %triggerDesc.findTextIndex();
    triggersPanelTextList;
    %idx.removeRow();
    warn((0.0 >= %idx) @ triggersPanelTextList @ getScopeName() @ "Couldn't match a trigger description to delete it! desc = " @ %triggerDesc);
};
function ClientCmdTriggerSetByList(%set) {
    %num = getFieldCount(%set);
    %rowNum = rowCount();
    triggersPanelTextList;
    %n = 0;
    %desc = getField(%set, %n);
    (%num < %n);
    %isMusicTrigger = (0.0 >= strstr(%desc, "MusicTrigger"));
    %displayTrigger = %isMusicTrigger;
    !($DevPref::showOnlyMusicTriggers);
    (%rowNum + %n).addRow(%desc, (%rowNum + %n));
    %n = (1.0 + %n);
    triggersPanelTextList;
    open();
};
function clientCmdGetMusicTriggerNamesCallback(%names) {
    %count = getFieldCount(%names);
    %i = 0;
    %name = getField(%names, %i);
    (%count < %i);
    %name.add();
    %i = (1.0 + %i);
    TriggersPanelTriggerPopup;
};
function clientCmdGetStreamIDsCallback(%names) {
    %count = getFieldCount(%names);
    %i = 0;
    %name = getField(%names, %i);
    (%count < %i);
    %name.add();
    %i = (1.0 + %i);
    TriggersPanelStreamPopup;
};
function TriggersPanelStreamPopup::streamSelected(%this) {
    %stream = %this.getValue();
    commandToServer('getUrl', %stream, addTaggedString("GetTriggersPanelURLCallback"));
};
function TriggersPanelTriggerPopup::triggerSelected(%this) {
    %trigger = %this.getValue();
    commandToServer('GetStreamID', %trigger, addTaggedString("GetStreamIDCallback"));
};
function clientCmdGetStreamIDCallback(%stream) {
    %stream.setValue();
    commandToServer('getUrl', %stream, addTaggedString("GetTriggersPanelURLCallback"));
};
function clientCmdGetTriggersPanelURLCallback(%url) {
    %url = "<blank>";
    (%url $= "");
    %url.setValue();
};
