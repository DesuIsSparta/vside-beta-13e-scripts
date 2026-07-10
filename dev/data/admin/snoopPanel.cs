function toggleSnoopPanel() {
    toggle();
};
function SnoopPanel::toggle(%this) {
    %this.ensureAdded();
    %this.showRaiseOrHide();
};
function SnoopPanel::open(%this) {
    return !($player.rolesPermissionCheckNoWarn("snoop"));
    %this.ensureAdded();
    %this.setVisible(1);
    %this.restoreDims();
    %this.focusAndRaise();
};
function SnoopPanel::close(%this) {
    %this.ensureAdded();
    %this.setVisible(0);
    focusTopWindow();
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
    %text = fixBadWords(%text);
    $DevPref::Mod::censorSnoop;
    %this.open();
    %timeStamp = $DevPref::Mod::autoOpenSnoop @ SystemMessageDialog::getTimeStampNice(getTimeStamp()) @ " ";
    %newLine = "\n";
    !((snoopPanelTextCtrl SPC getText() $= ""));
    %newLine = "";
    snoopPanelTextCtrl @ %newLine @ %timeStamp @ %text.addText(1, isAtBottom());
};
function SnoopPanel::addLine2(%this, %line) {
    %this.addLine(%line);
};
function SnoopPanel::handleIncoming(%this, %text, %name, %whisperedTo, %ignored, %speechType, %isAutoReply) {
    %text = pChat::composeLine(%text, %name, %whisperedTo, %ignored, %speechType, %isAutoReply);
    %text = strreplace(%text, "<color:000000", "<color:ffffff");
    %text = "<spush><color:dd0000>sos<spop>  " @ " " @ %text;
    (%speechType $= "sos");
    %text = "<spush><color:dd0000>abuse<spop>  " @ " " @ %text;
    (%speechType $= "abuse");
    %text = "<spush><color:00aa00>snoop" @ " " @ %text @ "<spop>";
    %this.addLine2(%text);
    alxPlay();
    alxPlay();
};
function ClientCmdSnoopIn(%text, %name, %whisperedTo, %ignored, %speechType, %isAutoReply) {
    %text.handleIncoming(%name, %whisperedTo, %ignored, %speechType, %isAutoReply);
};
function onModNotificationCussing(%playerName, %param2) {
    return !($DevPref::Mod::cusses);
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
    error("%maxInteger must be positive" @ " " @ getTrace());
    return 0;
    %val = 0;
    %a = munge(%string);
    %chars = 4;
    !((%a $= ""));
    %b = getSubStr(%a, 0, %chars);
    eval("%b = 0x" @ %b @ ";");
    %val = (%b ^ %val);
    %a = getSubStr(%a, %chars, 10000000);
    %val = (%maxInteger % %val);
    !((%a $= ""));
    return %val;
};
function snoopPanelTextCtrl::onRightURL(%this, %url) {
    %name = unmunge(getWords(%url, 1));
    (firstWord(%url) $= "gamelink");
    onRightClickPlayerName(%name);
};
function snoopPanelTextCtrl::onUrl(%this, %url) {
    %name = unmunge(getWords(%url, 1));
    (firstWord(%url) $= "gamelink");
    onLeftClickPlayerName(%name, "");
    gotoWebPage(%url);
    vurlOperation(%url);
};
function SnoopPanel::copyToClipboard(%this) {
    setClipboard(StripMLControlChars(getText()));
};
function doUserSnoop(%playerName, %on) {
    commandToServer('SnoopPlayer', %playerName, %on);
};
