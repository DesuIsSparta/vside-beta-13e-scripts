function TextureManager_OnDelayedLoad(%num) {
    textureLoadingIndicator_initialize();
    if ((0.0 > %num)) {
        geTextureLoadingContainer.setVisible(1);
        geTextureLoadingContainer.setProfile(HUDDarkProfile);
        geTexturesLoadingIcon.resume();
    }
    geTextureLoadingContainer.setVisible(0);
    geTextureLoadingContainer.setProfile(ETSNonModalProfile);
    geTexturesLoadingIcon.stop();
    WindowManager.update();
};
function textureLoadingIndicator_initialize() {
    if (isObject(geTexturesLoadingIcon)) {
        return;
    }
    %wi = AnimCtrl::newAnimCtrl((19.0 - getWord(geTextureLoadingContainer.getExtent(), 0)) @ " " @ 0, "18 18");
    %wi.setDelay(120);
    %wi.addFrame("platform/client/ui/wait0.png");
    %wi.addFrame("platform/client/ui/wait1.png");
    %wi.addFrame("platform/client/ui/wait2.png");
    %wi.addFrame("platform/client/ui/wait3.png");
    %wi.addFrame("platform/client/ui/wait4.png");
    %wi.addFrame("platform/client/ui/wait5.png");
    %wi.addFrame("platform/client/ui/wait6.png");
    %wi.addFrame("platform/client/ui/wait7.png");
    %wi.setName("geTexturesLoadingIcon");
    geTextureLoadingContainer.add(%wi);
    new GuiMLTextCtrl(geTGF_deets_eventTxtr) {
        profile = geTextureLoadingContainer @ "InfoWindowTextProfile";
        position = "0 0";
        extent = (2.0 - getWord(%wi.getPosition(), 0)) @ " " @ 18;
        horizSizing = "width";
        vertSizing = "bottom";
        text = mlStyle("<just:right>loading.. ", "loadingHUD");
        autoDetectLinks = 0;
    };.add();
};
