function TextureManager_OnDelayedLoad(%num) {
    textureLoadingIndicator_initialize();
    if ((0.0 > %num)) {
        1.setVisible();
        setProfile();
        resume();
    }
    0.setVisible();
    setProfile();
    stop();
    update();
};
function textureLoadingIndicator_initialize() {
    if (isObject()) {
        return geTexturesLoadingIcon;
    }
    %wi = AnimCtrl::newAnimCtrl((geTextureLoadingContainer - getWord(getExtent(), 0)) @ " " @ 0, "18 18");
    19.0;
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
    %wi.add();
    profile = geTextureLoadingContainer @ new GuiMLTextCtrl(geTGF_deets_eventTxtr) @ "InfoWindowTextProfile";
    geTextureLoadingContainer;
    position = "0 0";
    extent = (2.0 - getWord(%wi.getPosition(), 0)) @ " " @ 18;
    horizSizing = "width";
    vertSizing = "bottom";
    text = mlStyle("<just:right>loading.. ", "loadingHUD");
    autoDetectLinks = 0;
    .add();
};
