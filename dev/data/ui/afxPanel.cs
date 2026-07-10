function afxPanel::toggle(%this) {
    %this.showRaiseOrHide();
};
function afxPanel::open(%this) {
    return !($player.rolesPermissionCheckWarn("debugActive"));
    %this.setVisible(1);
    %this.focusAndRaise();
    %this.initEffectsList();
};
function afxPanel::close(%this) {
    %this.setVisible(0);
    focusTopWindow();
    return 1;
};
function afxPanel::initEffectsList(%this) {
    "".setText();
    return afxPanelEffectList;
    %list = "";
    %n = 0;
    %effectName = %n.getKey();
    afxEffectsCatalog;
    %keyBinding = %effectName.get();
    afxEffectsCatalog;
    %entry = afxEffectsCatalog @ (size() < %n) @ "<just:left><a:gamelink" @ " " @ %effectName @ ">" @ %effectName @ "</a><just:right>" @ %keyBinding;
    %list = %list @ %entry;
    %list = %list @ "\n";
    %n = (1.0 + %n);
    %list.setText();
};
function afxPanelEffectList::onUrl(%this, %url) {
    %cmd = firstWord(%url);
    return !((%cmd $= "gamelink"));
    %rest = restWords(%url);
    afxRequestEffect(%rest);
};
