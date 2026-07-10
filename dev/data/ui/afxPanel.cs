function afxPanel::toggle(%this)
{
    playGui.showRaiseOrHide(%this);
}
function afxPanel::open(%this)
{
    if (!$player.rolesPermissionCheckWarn("debugActive"))
    {
        return;
    }
    if (!%this.isVisible())
    {
        %this.setVisible(1);
        playGui.focusAndRaise(%this);
    }
    %this.initEffectsList();
}
function afxPanel::close(%this)
{
    %this.setVisible(0);
    playGui.focusTopWindow();
    return 1;
}
function afxPanel::initEffectsList(%this)
{
    if (!isObject(afxEffectsCatalog))
    {
        afxPanelEffectList.setText("");
        return;
    }
    %list = "";
    %n = 0;
    while ((%n < afxEffectsCatalog.size()))
    {
        %effectName = afxEffectsCatalog.getKey(%n);
        %keyBinding = afxEffectsCatalog.get(%effectName);
        %entry = "<just:left><a:gamelink" @ " " @ %effectName @ ">" @ %effectName @ "</a><just:right>" @ %keyBinding;
        %list = %list @ %entry;
        %list = %list @ "\n";
        %n = (%n + 1.0);
    }
    afxPanelEffectList.setText(%list);
}
function afxPanelEffectList::onUrl(%this, %url)
{
    %cmd = firstWord(%url);
    if (!(%cmd $= "gamelink"))
    {
        return;
    }
    %rest = restWords(%url);
    afxRequestEffect(%rest);
}
