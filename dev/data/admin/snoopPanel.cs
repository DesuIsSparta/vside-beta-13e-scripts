function toggleSnoopPanel() {
    SnoopPanel.toggle();
};
function SnoopPanel::toggle(%this) {
    %this.ensureAdded(playGui);
    %this.showRaiseOrHide(playGui);
};
function SnoopPanel::open(%this) {
    if (!("snoop".rolesPermissionCheckNoWarn($player))) {
        return;
    }
    %this.ensureAdded(playGui);
    if (!(%this.isVisible())) {
        1.setVisible(%this);
        %this.restoreDims();
        %this.focusAndRaise(playGui);
    }
};
function SnoopPanel::close(%this) {
    %this.ensureAdded(playGui);
    0.setVisible(%this);
    playGui.focusTopWindow();
    %this.storeDims();
};
function SnoopPanel::restoreDims(%this) {
    %dim = $DevPref::Mod::SnoopWindow::Dim;
    getWord(%dim, 3).resize(%this, getWord(%dim, 0), getWord(%dim, 1), getWord(%dim, 2));
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
    SnoopPanelScroll.isAtBottom().addText(snoopPanelTextCtrl, %newLine @ %timeStamp @ %text, 1);
};
function SnoopPanel::addLine2(%this, %line) {
    %line.addLine(%this);
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
    %text.addLine2(%this);
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
    %isAutoReply.handleIncoming(SnoopPanel, %text, %name, %whisperedTo, %ignored, %speechType);
};
function onModNotificationCussing(%playerName, %param2) {
    if (!($DevPref::Mod::cusses)) {
        return;
    }
    %text = NextToken(%param2, "verb", " ");
    %line = "<spush><color:880088>cuss ";
    %line = %line @ " " @ "".getPlayerMarkup(pChat, %playerName);
    %line = %line @ " " @ %verb @ " " @ %text;
    %line = %line @ " " @ "<spop>";
    %line.addLine2(SnoopPanel);
    %soundNum = stringToInteger(%playerName, $gAudioProfile_CussesNum);
    alxPlay2(%soundNum[$gAudioProfile_Cusses @ %soundNum]);
};
function stringToInteger(%string, %maxInteger) {
    if ((%maxInteger <= 0.0)) {
        error("%maxInteger must be positive" @ " " @ getTrace());
        return 0;
    }
    %val = 0;
    %a = munge(%string);
    while (!(%a $= "")) {
        %chars = 4;
        %b = getSubStr(%a, 0, %chars);
        eval("%b = 0x" @ %b @ ";");
        %val = (%val ^ %b);
        %a = getSubStr(%a, %chars, 10000000);
    }
    %val = (%val % %maxInteger);
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
