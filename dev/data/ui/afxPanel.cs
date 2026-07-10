function afxPanel::toggle(%this) {
    %this.showRaiseOrHide(playGui);
};
function afxPanel::open(%this) {
    if (!("debugActive".rolesPermissionCheckWarn($player))) {
        return;
    }
    if (!(%this.isVisible())) {
        1.setVisible(%this);
        %this.focusAndRaise(playGui);
    }
    %this.initEffectsList();
};
function afxPanel::close(%this) {
    0.setVisible(%this);
    playGui.focusTopWindow();
    return 1;
};
function afxPanel::initEffectsList(%this) {
    if (!(isObject(afxEffectsCatalog))) {
        "".setText(afxPanelEffectList);
        return;
    }
    %list = "";
    %n = 0;
    while ((%n < afxEffectsCatalog.size())) {
        %effectName = %n.getKey(afxEffectsCatalog);
        %keyBinding = %effectName.get(afxEffectsCatalog);
        %entry = "<just:left><a:gamelink" @ " " @ %effectName @ ">" @ %effectName @ "</a><just:right>" @ %keyBinding;
        %list = %list @ %entry;
        %list = %list @ "\n";
        %n = (%n + 1.0);
    }
    %list.setText(afxPanelEffectList);
};
function afxPanelEffectList::onUrl(%this, %url) {
    %cmd = firstWord(%url);
    if (!(%cmd $= "gamelink")) {
        return;
    }
    %rest = restWords(%url);
    afxRequestEffect(%rest);
};
