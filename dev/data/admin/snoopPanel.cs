function toggleSnoopPanel() {
    SnoopPanel.toggle();
};
function SnoopPanel::toggle(%this) {
    %this.ensureAdded();
    %this.showRaiseOrHide();
};
function SnoopPanel::open(%this) {
    if (!($player.rolesPermissionCheckNoWarn("snoop"))) {
        return;
    }
    %this.ensureAdded();
    if (!(%this.isVisible())) {
        %this.setVisible(1);
        %this.restoreDims();
        %this.focusAndRaise();
    }
};
function SnoopPanel::close(%this) {
    %this.ensureAdded();
    %this.setVisible(0);
    playGui.focusTopWindow();
    %this.storeDims();
};
function SnoopPanel::restoreDims(%this) {
    %dim = $DevPref::Mod::SnoopWindow::Dim;
    %this.resize(getWord(%dim, 0), getWord(%dim, 1), getWord(%dim, 2), getWord(%dim, 3));
};
function SnoopPanel::storeDims(%this) {
    $DevPref::Mod::SnoopWindow::Dim = %this.getPosition() @ " " @ %this.getExtent();
};
function SnoopPanel::addLine(%this, %text) {
    if ($DevPref::Mod::censorSnoop) {
        %text = fixBadWords(%text);
    }
    if ($DevPref::Mod::autoOpenSnoop) {
        %this.open();
    }
    %timeStamp = SystemMessageDialog::getTimeStampNice(getTimeStamp()) @ " ";
    if (!(snoopPanelTextCtrl.getText() $= "")) {
        %newLine = "\n";
    }
    %newLine = "";
    %newLine @ %timeStamp @ %text.addText(1, SnoopPanelScroll.isAtBottom());
};
function SnoopPanel::addLine2(%this, %line) {
    %this.addLine(%line);
};
function SnoopPanel::handleIncoming(%this, %text, %name, %whisperedTo, %ignored, %speechType, %isAutoReply) {
    %text = pChat::composeLine(%text, %name, %whisperedTo, %ignored, %speechType, %isAutoReply);
    %text = strreplace(%text, "<color:000000", "<color:ffffff");
    if ((%speechType $= "sos")) {
        %text = "<spush><color:dd0000>sos<spop>  " @ " " @ %text;
    }
    if ((%speechType $= "abuse")) {
        %text = "<spush><color:dd0000>abuse<spop>  " @ " " @ %text;
    }
    %text = "<spush><color:00aa00>snoop" @ " " @ %text @ "<spop>";
    %this.addLine2(%text);
    if ($DevPref::Audio::NotifySnoop) {
        if ((%speechType $= "sos")) {
            alxPlay(Audio_SOSMessageIn);
        }
        if ((%speechType $= "abuse")) {
            alxPlay(Audio_SOSMessageIn);
        }
    }
};
function ClientCmdSnoopIn(%text, %name, %whisperedTo, %ignored, %speechType, %isAutoReply) {
    %text.handleIncoming(%name, %whisperedTo, %ignored, %speechType, %isAutoReply);
};
function onModNotificationCussing(%playerName, %param2) {
    if (!($DevPref::Mod::cusses)) {
        return;
    }
    %text = NextToken(%param2, "verb", " ");
    %line = "<spush><color:880088>cuss ";
    %line = pChat @ %playerName.getPlayerMarkup("");
    %line @ " ";
    %line = %line @ " " @ %verb @ " " @ %text;
    %line = %line @ " " @ "<spop>";
    %line.addLine2();
    %soundNum = stringToInteger(%playerName, $gAudioProfile_CussesNum);
    SnoopPanel;
    alxPlay2(%soundNum[$gAudioProfile_Cusses @ %soundNum]);
};
function stringToInteger(%string, %maxInteger) {
    if ((0.0 <= %maxInteger)) {
        error("%maxInteger must be positive" @ " " @ getTrace());
        return 0;
    }
    %val = 0;
    %a = munge(%string);
    if (!(%a $= "")) {
        %chars = 4;
        %b = getSubStr(%a, 0, %chars);
        eval("%b = 0x" @ %b @ ";");
        %val = (%b ^ %val);
        %a = getSubStr(%a, %chars, 10000000);
    }
    %val = (%maxInteger % %val);
    !(%a $= "");
    return %val;
};
function snoopPanelTextCtrl::onRightURL(%this, %url) {
    if ((firstWord(%url) $= "gamelink")) {
        %name = unmunge(getWords(%url, 1));
        onRightClickPlayerName(%name);
    }
};
function snoopPanelTextCtrl::onUrl(%this, %url) {
    if ((firstWord(%url) $= "gamelink")) {
        %name = unmunge(getWords(%url, 1));
        onLeftClickPlayerName(%name, "");
    }
    if ((getSubStr(%url, 0, 7) $= "http://")) {
        gotoWebPage(%url);
    }
    if ((getSubStr(%url, 0, 7) $= "vside:/")) {
        vurlOperation(%url);
    }
};
function SnoopPanel::copyToClipboard(%this) {
    setClipboard(StripMLControlChars(snoopPanelTextCtrl.getText()));
};
function doUserSnoop(%playerName, %on) {
    commandToServer('SnoopPlayer', %playerName, %on);
};
