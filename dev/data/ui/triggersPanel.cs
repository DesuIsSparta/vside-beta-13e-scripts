function TriggersPanel::toggle(%this)
{
    playGui.showRaiseOrHide(%this);
    return ;
}
function TriggersPanel::open(%this)
{
    if (!$player.rolesPermissionCheckNoWarn("debugActive"))
    {
        return ;
    }
    if (!%this.isVisible())
    {
        %this.setVisible(1);
        playGui.focusAndRaise(%this);
    }
    TriggersPanelTriggerPopup.clear();
    TriggersPanelStreamPopup.clear();
    commandToServer('GetMusicTriggers', addTaggedString("GetMusicTriggerNamesCallback"));
    commandToServer('GetStreamIDs', addTaggedString("GetStreamIDsCallback"));
    return ;
}
function TriggersPanel::close(%this)
{
    %this.setVisible(0);
    playGui.focusTopWindow();
    return 1;
}
function TriggersPanel::setStream(%this)
{
    %trigger = TriggersPanelTriggerPopup.getValue();
    %stream = TriggersPanelStreamPopup.getValue();
    if (!((%trigger $= "")) && !((%stream $= "")))
    {
        MessageBoxYesNo("Set Stream", "Are you sure you want to set the stream for trigger \"" @ %trigger @ "\" to \"" @ %stream @ "\"?", "TriggersPanel.setStreamReally();", "");
    }
    return ;
}
function TriggersPanel::setStreamReally(%this)
{
    %trigger = TriggersPanelTriggerPopup.getValue();
    %stream = TriggersPanelStreamPopup.getValue();
    if (!((%trigger $= "")) && !((%stream $= "")))
    {
        log("Communication", "info", "Setting stream for trigger " @ %trigger @ " to " @ %stream);
        commandToServer('SetMusicStreamMapping', %trigger, %stream);
        triggersPanelTextList.clear();
        TriggersPanelURLText.setValue("");
        commandToServer('reportTriggers', $DevPref::reportTriggers);
    }
    return ;
}
function TriggersPanel::setLocalURLMapping(%this)
{
    %stream = $TriggersPanel::NewStreamName;
    %newUrl = $TriggersPanel::NewURL;
    if (!((%stream $= "")) && !((%newUrl $= "")))
    {
        MessageBoxYesNo("Set Stream", "Are you sure you want to set stream \"" @ %stream @ "\" to \"" @ %newUrl @ "\"?", "TriggersPanel.setLocalURLMappingReally();", "");
    }
    else
    {
        MessageBoxOK("Test URL", "Please provide a value for both Stream and URL", "");
    }
    return ;
}
function TriggersPanel::setLocalURLMappingReally(%this)
{
    %stream = $TriggersPanel::NewStreamName;
    %newUrl = $TriggersPanel::NewURL;
    if (!((%stream $= "")) && !((%newUrl $= "")))
    {
        log("communication", "info", "Stream " @ %stream SPC "will now be mapped to " @ %newUrl SPC "on this server only");
        commandToServer('SetUrl', %stream, %newUrl);
        TriggersPanelStreamPopup.clear();
        TriggersPanelTriggerPopup.clear();
        TriggersPanelURLText.setValue("");
        commandToServer('GetMusicTriggers', addTaggedString("GetMusicTriggerNamesCallback"));
        commandToServer('GetStreamIDs', addTaggedString("GetStreamIDsCallback"));
    }
    return ;
}
function TriggersPanel::setURLMapping(%this)
{
    %stream = $TriggersPanel::NewStreamName;
    %newUrl = $TriggersPanel::NewURL;
    if (!((%stream $= "")) && !((%newUrl $= "")))
    {
        MessageBoxYesNo("Set Stream", "Are you sure you want to set stream \"" @ %stream @ "\" to \"" @ %newUrl @ "\"?", "TriggersPanel.setURLMappingReally();", "");
    }
    else
    {
        MessageBoxOK("Test URL", "Please provide a value for both Stream and URL", "");
    }
    return ;
}
function TriggersPanel::setURLMappingReally(%this)
{
    %stream = $TriggersPanel::NewStreamName;
    %newUrl = $TriggersPanel::NewURL;
    if (!((%stream $= "")) && !((%newUrl $= "")))
    {
        log("communication", "info", "Stream " @ %stream SPC "will now be mapped to " @ %newUrl);
        %this.urlsToEnvmanager(%stream, %newUrl);
    }
    return ;
}
function TriggersPanel::urlsToEnvmanager(%this, %stream, %musicURL)
{
    %request = new ManagerRequest();
    if (isObject(MissionCleanup))
    {
        MissionCleanup.add(%request);
    }
    %url = $Net::ClientServiceURL @ "/UpdateMusicStreamIDMapping" @ "?user=" @ urlEncode($Player::Name) @ "&token=" @ urlEncode($Token);
    %url = %url @ "&streamID=" @ urlEncode(%stream);
    %url = %url @ "&mountURL=" @ urlEncode(%musicURL);
    log("network", "info", getScopeName() @ ":" @ %url);
    %request.setURL(%url);
    %request.start();
    return ;
}
function ChangeStreamIDMappingRequest::onDone(%this)
{
    %status = findRequestStatus(%this);
    log("network", "info", getScopeName() @ ":" @ %status);
    if (%status $= "fail")
    {
        warn("network", getScopeName() @ " request failed: " @ %this.getValue("statusMessage"));
    }
    TriggersPanelStreamPopup.clear();
    TriggersPanelTriggerPopup.clear();
    TriggersPanelURLText.setValue("");
    commandToServer('GetMusicTriggers', addTaggedString("GetMusicTriggerNamesCallback"));
    commandToServer('GetStreamIDs', addTaggedString("GetStreamIDsCallback"));
    %this.schedule(0, "delete");
    return ;
}
function TriggersPanel::testURL(%this)
{
    if (TriggersPanelTestUrl.getText() $= "Test URL")
    {
        TriggersPanelTestUrl.setText("Stop Test");
    }
    else
    {
        if (TriggersPanelTestUrl.getText() $= "Stop Test")
        {
            FMod.popStream("TriggersPanelTest");
            TriggersPanelTestUrl.setText("Test URL");
            return ;
        }
    }
    %newUrl = $TriggersPanel::NewURL;
    if (!(%newUrl $= ""))
    {
        FMod.pushStreamWithVolume("TriggersPanelTest", %newUrl, 0.8, "");
    }
    else
    {
        MessageBoxOK("Test URL", "Please specify a URL to test.", "");
    }
    return ;
}
function TriggersPanel::selectTrigger(%this)
{
    %selected = triggersPanelTextList.getValue();
    if (strstr(%selected, "MusicTrigger") < 0)
    {
        return ;
    }
    %wc = getWordCount(%selected);
    %trigger = getWord(%selected, %wc - 3);
    %stream = getWord(%selected, %wc - 1);
    TriggersPanelTriggerPopup.setValue(%trigger);
    TriggersPanelStreamPopup.setValue(%stream);
    commandToServer('getUrl', %stream, addTaggedString("GetTriggersPanelURLCallback"));
    return ;
}
function setTriggerReportingState()
{
    if ($DevPref::reportTriggers)
    {
        triggersPanelTextList.clear();
    }
    commandToServer('reportTriggers', $DevPref::reportTriggers);
    return ;
}
function setShowOnlyMusicTriggers()
{
    triggersPanelTextList.clear();
    commandToServer('reportTriggers', $DevPref::reportTriggers);
    return ;
}
function ClientCmdTriggerSet(%triggerDesc)
{
    %isMusicTrigger = strstr(%triggerDesc, "MusicTrigger") >= 0;
    %displayTrigger = !$DevPref::showOnlyMusicTriggers || %isMusicTrigger;
    if (%displayTrigger)
    {
        %idx = triggersPanelTextList.rowCount();
        triggersPanelTextList.addRow(%idx, %triggerDesc, %idx);
        if ($DevPref::autoOpenTriggers)
        {
            TriggersPanel.open();
        }
    }
    return ;
}
function ClientCmdTriggerUnset(%triggerDesc)
{
    %idx = triggersPanelTextList.findTextIndex(%triggerDesc);
    if (%idx >= 0)
    {
        triggersPanelTextList.removeRow(%idx);
    }
    else
    {
        warn(getScopeName() @ "Couldn\'t match a trigger description to delete it! desc = " @ %triggerDesc);
    }
    return ;
}
function ClientCmdTriggerSetByList(%set)
{
    %num = getFieldCount(%set);
    %rowNum = triggersPanelTextList.rowCount();
    %n = 0;
    while (%n < %num)
    {
        %desc = getField(%set, %n);
        %isMusicTrigger = strstr(%desc, "MusicTrigger") >= 0;
        %displayTrigger = !$DevPref::showOnlyMusicTriggers || %isMusicTrigger;
        if (%displayTrigger)
        {
            triggersPanelTextList.addRow(%n + %rowNum, %desc, %n + %rowNum);
        }
        %n = %n + 1;
    }
    if ($DevPref::autoOpenTriggers)
    {
        TriggersPanel.open();
    }
    return ;
}
function clientCmdGetMusicTriggerNamesCallback(%names)
{
    %count = getFieldCount(%names);
    %i = 0;
    while (%i < %count)
    {
        %name = getField(%names, %i);
        if (!(%name $= ""))
        {
            TriggersPanelTriggerPopup.add(%name);
        }
        %i = %i + 1;
    }
}

function clientCmdGetStreamIDsCallback(%names)
{
    %count = getFieldCount(%names);
    %i = 0;
    while (%i < %count)
    {
        %name = getField(%names, %i);
        if (!(%name $= ""))
        {
            TriggersPanelStreamPopup.add(%name);
        }
        %i = %i + 1;
    }
}

function TriggersPanelStreamPopup::streamSelected(%this)
{
    %stream = %this.getValue();
    if (!(%stream $= ""))
    {
        commandToServer('getUrl', %stream, addTaggedString("GetTriggersPanelURLCallback"));
    }
    return ;
}
function TriggersPanelTriggerPopup::triggerSelected(%this)
{
    %trigger = %this.getValue();
    if (!(%trigger $= ""))
    {
        commandToServer('GetStreamID', %trigger, addTaggedString("GetStreamIDCallback"));
    }
    return ;
}
function clientCmdGetStreamIDCallback(%stream)
{
    TriggersPanelStreamPopup.setValue(%stream);
    commandToServer('getUrl', %stream, addTaggedString("GetTriggersPanelURLCallback"));
    return ;
}
function clientCmdGetTriggersPanelURLCallback(%url)
{
    if (%url $= "")
    {
        %url = "<blank>";
    }
    TriggersPanelURLText.setValue(%url);
    return ;
}
