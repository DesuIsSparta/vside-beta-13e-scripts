function TextureManager_OnDelayedLoad(%num)
{
    textureLoadingIndicator_initialize();
    if (%num > 0)
    {
        geTextureLoadingContainer.setVisible(1);
        geTextureLoadingContainer.setProfile(HUDDarkProfile);
        geTexturesLoadingIcon.resume();
    }
    else
    {
        geTextureLoadingContainer.setVisible(0);
        geTextureLoadingContainer.setProfile(ETSNonModalProfile);
        geTexturesLoadingIcon.stop();
    }
    WindowManager.update();
    return ;
}
function textureLoadingIndicator_initialize()
{
    if (isObject(geTexturesLoadingIcon))
    {
        return ;
    }
    %wi = AnimCtrl::newAnimCtrl(getWord(geTextureLoadingContainer.getExtent(), 0) - 19 SPC 0, "18 18");
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
    return ;
}
