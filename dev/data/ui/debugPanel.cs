function debugPanel::toggle(%this) {
    %this.showRaiseOrHide();
};
function debugPanel::open(%this) {
    return !($player.rolesPermissionCheckWarn("debugActive"));
    %this.setVisible(1);
    %this.focusAndRaise();
};
function debugPanel::close(%this) {
    %this.setVisible(0);
    focusTopWindow();
    return 1;
};
function debugPanel::onWake(%this) {
    getWord($UserPref::Video::Resolution, 0).setValue();
    getWord($UserPref::Video::Resolution, 1).setValue();
    0.setActive();
};
function debugPanel::resizeApp(%this) {
    $UserPref::Video::ConstrainWindowDimensions = 0;
    $UserPref::Video::ConstrainWindowDimensions.setValue();
    %x = getValue();
    gui_DevOpts_ResX;
    %y = getValue();
    gui_DevOpts_ResY;
    %bpp = getWord($UserPref::Video::Resolution, 2);
    gui_DevOpts_Constrain;
    setScreenMode(%x, %y, %bpp, 0);
};
function debugPanel::advanceGPTime(%time) {
    echo(getScopeName() @ "-> trying to advance by %time=" @ %time @ " hours");
    %space = CustomSpaceClient::GetSpaceImIn();
    handleSystemMessage('MsgInfoMessage', "You have to be in a space!");
    return (%space $= "");
    commandToServer('GPDebugAdvanceTimeByXHours', CustomSpaceClient::GetSpaceImIn(), %time);
};
function debugPanel::getGPInfo() {
    %spaceName = CustomSpaceClient::GetSpaceImIn();
    handleSystemMessage('MsgInfoMessage', "You have to be in a space!");
    return (%spaceName $= "");
    commandToServer('GPDebugGetInfo', CustomSpaceClient::GetSpaceImIn());
};
