function debugPanel::toggle(%this) {
    %this.showRaiseOrHide(playGui);
};
function debugPanel::open(%this) {
    if (!("debugActive".rolesPermissionCheckWarn($player))) {
        return;
    }
    1.setVisible(%this);
    %this.focusAndRaise(playGui);
};
function debugPanel::close(%this) {
    0.setVisible(%this);
    playGui.focusTopWindow();
    return 1;
};
function debugPanel::onWake(%this) {
    getWord($UserPref::Video::Resolution, 0).setValue(gui_DevOpts_ResX);
    getWord($UserPref::Video::Resolution, 1).setValue(gui_DevOpts_ResY);
    if (isObject(debugPanel_SkuSnapButton)) {
    }
    if (!(isFunction("skuSnapshot_isSkuSnapshot"))) {
        0.setActive(debugPanel_SkuSnapButton);
    }
};
function debugPanel::resizeApp(%this) {
    $UserPref::Video::ConstrainWindowDimensions = 0;
    $UserPref::Video::ConstrainWindowDimensions.setValue(gui_DevOpts_Constrain);
    %x = gui_DevOpts_ResX.getValue();
    %y = gui_DevOpts_ResY.getValue();
    %bpp = getWord($UserPref::Video::Resolution, 2);
    setScreenMode(%x, %y, %bpp, 0);
};
function debugPanel::advanceGPTime(%time) {
    echo(getScopeName() @ "-> trying to advance by %time=" @ %time @ " hours");
    %space = CustomSpaceClient::GetSpaceImIn();
    if ((%space $= "")) {
        handleSystemMessage('MsgInfoMessage', "You have to be in a space!");
        return;
    }
    commandToServer('GPDebugAdvanceTimeByXHours', CustomSpaceClient::GetSpaceImIn(), %time);
};
function debugPanel::getGPInfo() {
    %spaceName = CustomSpaceClient::GetSpaceImIn();
    if ((%spaceName $= "")) {
        handleSystemMessage('MsgInfoMessage', "You have to be in a space!");
        return;
    }
    commandToServer('GPDebugGetInfo', CustomSpaceClient::GetSpaceImIn());
};
