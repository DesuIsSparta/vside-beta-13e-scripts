function TextureManager_OnDelayedLoad(%num) {
    textureLoadingIndicator_initialize();
    if ((%num > 0.0)) {
        1.setVisible(geTextureLoadingContainer);
        HUDDarkProfile.setProfile(geTextureLoadingContainer);
        geTexturesLoadingIcon.resume();
    }
    0.setVisible(geTextureLoadingContainer);
    ETSNonModalProfile.setProfile(geTextureLoadingContainer);
    geTexturesLoadingIcon.stop();
    WindowManager.update();
};
function textureLoadingIndicator_initialize() {
    if (isObject(geTexturesLoadingIcon)) {
        return;
    }
    %wi = AnimCtrl::newAnimCtrl((getWord(geTextureLoadingContainer.getExtent(), 0) - 19.0) @ " " @ 0, "18 18");
    120.setDelay(%wi);
    "platform/client/ui/wait0.png".addFrame(%wi);
    "platform/client/ui/wait1.png".addFrame(%wi);
    "platform/client/ui/wait2.png".addFrame(%wi);
    "platform/client/ui/wait3.png".addFrame(%wi);
    "platform/client/ui/wait4.png".addFrame(%wi);
    "platform/client/ui/wait5.png".addFrame(%wi);
    "platform/client/ui/wait6.png".addFrame(%wi);
    "platform/client/ui/wait7.png".addFrame(%wi);
    "geTexturesLoadingIcon".setName(%wi);
    %wi.add(geTextureLoadingContainer);
    new GuiMLTextCtrl(geTGF_deets_eventTxtr) {
        profile = "InfoWindowTextProfile";
        position = "0 0";
        extent = (getWord(%wi.getPosition(), 0) - 2.0) @ " " @ 18;
        horizSizing = "width";
        vertSizing = "bottom";
        text = mlStyle("<just:right>loading.. ", "loadingHUD");
        autoDetectLinks = 0;
    };.add(geTextureLoadingContainer);
};
