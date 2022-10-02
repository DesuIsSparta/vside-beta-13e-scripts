function debugPanel::toggle(%this)
{
    playGui.showRaiseOrHide(%this);
    return ;
}
function debugPanel::open(%this)
{
    if (!$player.rolesPermissionCheckWarn("debugActive"))
    {
        return ;
    }
    %this.setVisible(1);
    playGui.focusAndRaise(%this);
    return ;
}
function debugPanel::close(%this)
{
    %this.setVisible(0);
    playGui.focusTopWindow();
    return 1;
}
function debugPanel::onWake(%this)
{
    gui_DevOpts_ResX.setValue(getWord($UserPref::Video::Resolution, 0));
    gui_DevOpts_ResY.setValue(getWord($UserPref::Video::Resolution, 1));
    if (isObject(debugPanel_SkuSnapButton) && !isFunction("skuSnapshot_isSkuSnapshot"))
    {
        debugPanel_SkuSnapButton.setActive(0);
    }
    return ;
}
function debugPanel::resizeApp(%this)
{
    $UserPref::Video::ConstrainWindowDimensions = 0;
    gui_DevOpts_Constrain.setValue($UserPref::Video::ConstrainWindowDimensions);
    %x = gui_DevOpts_ResX.getValue();
    %y = gui_DevOpts_ResY.getValue();
    %bpp = getWord($UserPref::Video::Resolution, 2);
    setScreenMode(%x, %y, %bpp, 0);
    return ;
}
function debugPanel::advanceGPTime(%time)
{
    echo(getScopeName() @ "-> trying to advance by %time=" @ %time @ " hours");
    %space = CustomSpaceClient::GetSpaceImIn();
    if (%space $= "")
    {
        handleSystemMessage('MsgInfoMessage', "You have to be in a space!");
        return ;
    }
    commandToServer('GPDebugAdvanceTimeByXHours', CustomSpaceClient::GetSpaceImIn(), %time);
    return ;
}
function debugPanel::getGPInfo()
{
    %spaceName = CustomSpaceClient::GetSpaceImIn();
    if (%spaceName $= "")
    {
        handleSystemMessage('MsgInfoMessage', "You have to be in a space!");
        return ;
    }
    commandToServer('GPDebugGetInfo', CustomSpaceClient::GetSpaceImIn());
    return ;
}
