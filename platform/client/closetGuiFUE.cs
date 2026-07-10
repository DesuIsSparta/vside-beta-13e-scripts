$gClosetGuiFueStepCount = 0;
$gClosetGuiFueCurrentStep = -(1);
function ClosetGuiFUE::open(%this)
{
    if (%this.refreshingOrInitializing)
    {
        %sched = gGetField(%this, "closetGuiFUEOpenSched");
        cancel(%sched);
        %sched = %this.schedule(250, open);
        gSetField(%this, "closetGuiFUEOpenSched", %sched);
        return;
    }
    if (!%this.initialized)
    {
        %this.arrivedAtFinalTip = 0;
        %this.Initialize();
    }
    %this.reposition();
    %this.showAllAsInactive();
    %this.goToStepByName(ClosetTabs.getCurrentTab().name);
}
function ClosetGuiFUE::close(%this)
{
    %this.hide();
}
function ClosetGuiFUE::show(%this)
{
    if (%this.refreshingOrInitializing)
    {
        %sched = gGetField(%this, "closetGuiFUEShowSched");
        cancel(%sched);
        %sched = %this.schedule(250, show);
        gSetField(%this, "closetGuiFUEShowSched", %sched);
        return;
    }
    %this.setVisible(1);
    %this.goToStepByName(ClosetTabs.getCurrentTab().name);
}
function ClosetGuiFUE::hide(%this)
{
    %this.setVisible(0);
}
function ClosetGuiFUE::reposition(%this)
{
    %this.position = (((getWord(ClosetGuiPositioner.extent, 0) - 960) / 2) + 1) @ " " @ ((((getWord(ClosetGuiPositioner.extent, 1) - 576) - 32) / 2) + 1);
}
function ClosetGuiFUE::showAllAsInactive(%this)
{
    if (%this.refreshingOrInitializing)
    {
        %sched = gGetField(%this, "closetGuiFUEShowAllAsInactiveSched");
        cancel(%sched);
        %sched = %this.schedule(250, showAllAsInactive);
        gSetField(%this, "closetGuiFUEShowAllAsInactiveSched", %sched);
        return;
    }
    %i = 0;
    while (%i < $gClosetGuiFueStepCount)
    {
        %i.setVisible(%this.stepContainers["active"], 0);
        if (isObject(%i, %this.stepContainers["inactive"]))
        {
            %i.setVisible(%this.stepContainers["inactive"], !%this.hideTipsCtrl.getValue());
        }
        %i = %i + 1;
    }
    ClosetGuiFUE.show();
}
function ClosetGuiFUE::addStep(%this, %activeContainer, %inactiveContainer)
{
    if (!isObject(%activeContainer))
    {
        error(getScopeName() @ " " @ "- empty activeContainer object -" @ " " @ getTrace());
        return 0;
    }
    if (!(%inactiveContainer $= ""))
    {
    }
    if (!isObject(%inactiveContainer))
    {
        error(getScopeName() @ " " @ "- empty inactiveContainer object -" @ " " @ getTrace());
        return 0;
    }
    %activeContainer.setVisible(0);
    %this.add(%activeContainer);
    %this.stepContainers[$gClosetGuiFueStepCount,"active"] = %activeContainer;
    if (isObject(%inactiveContainer))
    {
        %inactiveContainer.setVisible(0);
        %this.add(%inactiveContainer);
    }
    %this.stepContainers[$gClosetGuiFueStepCount,"inactive"] = %inactiveContainer;
    $gClosetGuiFueStepCount = $gClosetGuiFueStepCount + 1;
    return 1;
}
function ClosetGuiFUE::addStepWithName(%this, %activeContainer, %inactiveContainer, %stepName)
{
    %this.stepNumbersByName[strlwr(%stepName)] = $gClosetGuiFueStepCount;
    if (!%this.addStep(%activeContainer, %inactiveContainer))
    {
        warn(getScopeName() @ " " @ "- step" @ " " @ %stepName @ " " @ "not added -" @ " " @ getTrace());
        %this.stepNumbersByName[strlwr(%stepName)] = -(1);
        return 0;
    }
    return 1;
}
function ClosetGuiFUE::firstStep(%this)
{
    if ($gClosetGuiFueStepCount == 0)
    {
        error(getScopeName() @ " " @ "- first step has not yet been set -" @ " " @ getTrace());
        return;
    }
    %this.hideCurrentStep();
    $gClosetGuiFueCurrentStep = 0;
    %this.showCurrentStep();
}
function ClosetGuiFUE::nextStep(%this)
{
    %this.hideCurrentStep();
    if ($gClosetGuiFueCurrentStep >= -(1))
    {
    }
    if ($gClosetGuiFueCurrentStep < ($gClosetGuiFueStepCount - 1))
    {
        $gClosetGuiFueCurrentStep = $gClosetGuiFueCurrentStep + 1;
    }
    else
    {
        $gClosetGuiFueCurrentStep = -(1);
    }
    %this.showCurrentStep();
}
function ClosetGuiFUE::goToStepByName(%this, %stepName)
{
    if (0)
    {
        error(getScopeName() @ " " @ "- invalid stepName" @ " " @ %stepName @ " " @ "-" @ " " @ getTrace());
        return;
    }
    %this.hideCurrentStep();
    if (%stepName $= "Shops")
    {
        closetGuiFUEShopsDirBitmap.setVisible(($gCurrentStoreName $= ""));
        StoreShoppingBag.setVisible(!($gCurrentStoreName $= ""));
        StoreAddItemsButton.setVisible(!($gCurrentStoreName $= ""));
        ClosetTabs.setLeaveStoreControlsVisible(0);
    }
    else
    {
        closetGuiFUEShopsDirBitmap.setVisible(0);
    }
    $gClosetGuiFueCurrentStep = %this.stepNumbersByName[strlwr(%stepName)];
    %this.showCurrentStep();
}
function ClosetGuiFUE::goToStepByNumber(%this, %stepNumber)
{
    if ((%stepNumber < 0) || (%stepNumber >= $gClosetGuiFueStepCount))
    {
        error(getScopeName() @ " " @ "- invalid stepNumber" @ " " @ %stepNumber @ " " @ "-" @ " " @ getTrace());
        return;
    }
    %this.hideCurrentStep();
    $gClosetGuiFueCurrentStep = %stepNumber;
    %this.showCurrentStep();
}
function ClosetGuiFUE::showCurrentStep(%this)
{
    if ($gClosetGuiFueCurrentStep == -(1))
    {
        return;
    }
    if (%this.refreshingOrInitializing)
    {
        %sched = gGetField(%this, "closetGuiFUEShowCurrentStepSched");
        cancel(%sched);
        %sched = %this.schedule(250, showCurrentStep);
        gSetField(%this, "closetGuiFUEShowCurrentStepSched", %sched);
        return;
    }
    $gClosetGuiFueCurrentStep.setVisible(%this.stepContainers["inactive"], 0);
    $gClosetGuiFueCurrentStep.setVisible(%this.stepContainers["active"], !%this.hideTipsCtrl.getValue());
    if (!%this.arrivedAtFinalTip)
    {
        %this.arrivedAtFinalTip = $gClosetGuiFueCurrentStep == %this.stepNumbersByName[strlwr("Snapshot")];
    }
}
function ClosetGuiFUE::hideCurrentStep(%this)
{
    if ($gClosetGuiFueCurrentStep == -(1))
    {
        return;
    }
    $gClosetGuiFueCurrentStep.setVisible(%this.stepContainers["active"], 0);
    $gClosetGuiFueCurrentStep.setVisible(%this.stepContainers["inactive"], !%this.hideTipsCtrl.getValue());
}
function ClosetGuiFUE::refresh(%this)
{
    if (%this.refreshingOrInitializing)
    {
        %sched = gGetField(%this, "closetGuiFUERefreshSched");
        cancel(%sched);
        %sched = %this.schedule(250, refresh);
        gSetField(%this, "closetGuiFUERefreshSched", %sched);
        return;
    }
    %this.refreshingOrInitializing = 1;
    ClosetGuiFUE.deleteMembers();
    $gClosetGuiFueStepCount = 0;
    $gClosetGuiFueCurrentStep = -(1);
    %this.Initialize();
    if (%visible)
    {
        %this.showAllAsInactive();
    }
    %this.refreshingOrInitializing = 0;
}
function ClosetGuiFUE::Initialize(%this)
{
    %this.refreshingOrInitializing = 1;
    if (!isObject(closetGuiFUEHideTipsCtrl))
    {
        %this.hideTipsCtrl = new GuiCheckBoxCtrl(closetGuiFUEHideTipsCtrl) {
            profile = "ETSCheckBoxProfile";
            horizSizing = "right";
            vertSizing = "bottom";
            position = "863 557";
            extent = "99 20";
            minExtent = "8 2";
            sluggishness = -1;
            visible = 0;
            text = "Don't Show Tips";
            groupNum = -1;
            buttonType = "ToggleButton";
        };
        %this.add(%this.hideTipsCtrl);
    }
    else
    {
        %this.hideTipsCtrl = closetGuiFUEHideTipsCtrl;
        %this.add(%this.hideTipsCtrl);
    }
    %buttonPosition = ClosetTabs.getTabWithName("Body").button.getPosition();
    %newActiveStep = new GuiControl("") {
        profile = "ETSNonModalProfile";
        horizSizing = ClosetGuiFUE.horizSizing;
        vertSizing = ClosetGuiFUE.vertSizing;
        position = "0 0";
        extent = ClosetGuiFUE.extent;
        minExtent = ClosetGuiFUE.minExtent;
        sluggishness = ClosetGuiFUE.sluggishness;
        visible = 0;
    };
    new GuiBitmapCtrl(closetGuiFUEWelcomeImage) {
        profile = new GuiBitmapCtrl("") {
        profile = "ETSNonModalProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = (getWord(%buttonPosition, 0) - 10) @ " " @ (getWord(%buttonPosition, 1) - 10);
        extent = "123 82";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        bitmap = "platform/client/ui/closetGuiFUE_step3_active";
    }; @ "ETSNonModalProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "511 32";
        extent = "447 544";
        minExtent = "20 20";
        visible = 1;
        bitmap = "platform/client/ui/welcomeBodyPanel";
    };
    %newInactiveStep = new GuiControl("") {
        profile = "ETSNonModalProfile";
        horizSizing = ClosetGuiFUE.horizSizing;
        vertSizing = ClosetGuiFUE.vertSizing;
        position = "0 0";
        extent = ClosetGuiFUE.extent;
        minExtent = ClosetGuiFUE.minExtent;
        sluggishness = ClosetGuiFUE.sluggishness;
        visible = 0;
    };
    new GuiBitmapCtrl("") {
        profile = "ETSNonModalProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = (getWord(%buttonPosition, 0) - 10) @ " " @ (getWord(%buttonPosition, 1) - 10);
        extent = "123 82";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        bitmap = "platform/client/ui/closetGuiFUE_step3_inactive";
    };
    %this.addStepWithName(%newActiveStep, %newInactiveStep, "Body");
    %buttonPosition = ClosetTabs.getTabWithName("Closet").button.getPosition();
    %newActiveStep = new GuiControl("") {
        profile = "ETSNonModalProfile";
        horizSizing = ClosetGuiFUE.horizSizing;
        vertSizing = ClosetGuiFUE.vertSizing;
        position = "0 0";
        extent = ClosetGuiFUE.extent;
        minExtent = ClosetGuiFUE.minExtent;
        sluggishness = ClosetGuiFUE.sluggishness;
        visible = 0;
    };
    new GuiBitmapCtrl(closetGuiFUEWelcomeImage_b : closetGuiFUEWelcomeImage);
    %newInactiveStep = new GuiControl("") {
        profile = new GuiBitmapCtrl("") {
        profile = "ETSNonModalProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = (getWord(%buttonPosition, 0) - 10) @ " " @ (getWord(%buttonPosition, 1) - 10);
        extent = "123 82";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        bitmap = "platform/client/ui/closetGuiFUE_step2_active";
    }; @ "ETSNonModalProfile";
        horizSizing = ClosetGuiFUE.horizSizing;
        vertSizing = ClosetGuiFUE.vertSizing;
        position = "0 0";
        extent = ClosetGuiFUE.extent;
        minExtent = ClosetGuiFUE.minExtent;
        sluggishness = ClosetGuiFUE.sluggishness;
        visible = 0;
    };
    new GuiBitmapCtrl("") {
        profile = "ETSNonModalProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = (getWord(%buttonPosition, 0) - 10) @ " " @ (getWord(%buttonPosition, 1) - 10);
        extent = "123 82";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        bitmap = "platform/client/ui/closetGuiFUE_step2_inactive";
    };
    %this.addStepWithName(%newActiveStep, %newInactiveStep, "Closet");
    if (!ClosetTabs.tabShopsInitialized)
    {
        ClosetTabs.fillStoreTab();
    }
    %button = ClosetTabs.getTabWithName("Shops").button;
    %buttonPosition = %button.getPosition();
    %newActiveStep = new GuiControl("") {
        profile = "ETSNonModalProfile";
        horizSizing = ClosetGuiFUE.horizSizing;
        vertSizing = ClosetGuiFUE.vertSizing;
        position = "0 0";
        extent = ClosetGuiFUE.extent;
        minExtent = ClosetGuiFUE.minExtent;
        sluggishness = ClosetGuiFUE.sluggishness;
        visible = 0;
    };
    new GuiBitmapCtrl("") {
        profile = "ETSNonModalProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = (getWord(%buttonPosition, 0) - 10) @ " " @ (getWord(%buttonPosition, 1) - 10);
        extent = "123 82";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        bitmap = "platform/client/ui/closetGuiFUE_step1_active";
    };
    if (!isObject(closetGuiFUEShopsDirBitmap))
    {
        %this.shopsDirBitmap = new GuiBitmapCtrl(closetGuiFUEShopsDirBitmap) {
            profile = "ETSNonModalProfile";
            horizSizing = "right";
            vertSizing = "bottom";
            position = "35 125";
            extent = "441 375";
            minExtent = "1 1";
            sluggishness = -1;
            visible = ($gCurrentStoreName $= "");
            bitmap = "platform/client/ui/closetGuiFUE_shop_active_shopsDir";
        };
        %this.add(%this.shopsDirBitmap);
    }
    else
    {
        %this.shopsDirBitmap = closetGuiFUEShopsDirBitmap;
        %this.add(%this.shopsDirBitmap);
    }
    %credsBitmapCtrl = new GuiBitmapCtrl(closetGuiFUE_vPoints_vBux_Image) {
        profile = "ETSNonModalProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "607 32";
        extent = "346 340";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        bitmap = "platform/client/ui/closetGuiFUE_shop_active_creds";
    };
    %newActiveStep.add(%credsBitmapCtrl);
    %newInactiveStep = new GuiControl("") {
        profile = "ETSNonModalProfile";
        horizSizing = ClosetGuiFUE.horizSizing;
        vertSizing = ClosetGuiFUE.vertSizing;
        position = "0 0";
        extent = ClosetGuiFUE.extent;
        minExtent = ClosetGuiFUE.minExtent;
        sluggishness = ClosetGuiFUE.sluggishness;
        visible = 0;
    };
    new GuiBitmapCtrl("") {
        profile = "ETSNonModalProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = (getWord(%buttonPosition, 0) - 10) @ " " @ (getWord(%buttonPosition, 1) - 10);
        extent = "123 82";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        bitmap = "platform/client/ui/closetGuiFUE_step1_inactive";
    };
    %this.addStepWithName(%newActiveStep, %newInactiveStep, "Shops");
    %buttonPosition = ClosetTabs.getTabWithName("Snapshot").button.getPosition();
    %newActiveStep = new GuiControl("") {
        profile = "ETSNonModalProfile";
        horizSizing = ClosetGuiFUE.horizSizing;
        vertSizing = ClosetGuiFUE.vertSizing;
        position = "0 0";
        extent = ClosetGuiFUE.extent;
        minExtent = ClosetGuiFUE.minExtent;
        sluggishness = ClosetGuiFUE.sluggishness;
        visible = 0;
    };
    new GuiBitmapCtrl("") {
        profile = new GuiBitmapCtrl("") {
        profile = "ETSNonModalProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = (getWord(%buttonPosition, 0) - 10) @ " " @ (getWord(%buttonPosition, 1) - 10);
        extent = "123 82";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        bitmap = "platform/client/ui/closetGuiFUE_step4_active";
    }; @ "ETSNonModalProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = (getWord(ClosetTabContainer.getPosition(), 0) + 759) @ " " @ (getWord(ClosetTabContainer.getPosition(), 0) + 492);
        extent = "123 82";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        bitmap = "platform/client/ui/closetGuiFUE_step5_active";
    };
    %newInactiveStep = new GuiControl("") {
        profile = "ETSNonModalProfile";
        horizSizing = ClosetGuiFUE.horizSizing;
        vertSizing = ClosetGuiFUE.vertSizing;
        position = "0 0";
        extent = ClosetGuiFUE.extent;
        minExtent = ClosetGuiFUE.minExtent;
        sluggishness = ClosetGuiFUE.sluggishness;
        visible = 0;
    };
    new GuiBitmapCtrl("") {
        profile = new GuiBitmapCtrl("") {
        profile = "ETSNonModalProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = (getWord(%buttonPosition, 0) - 10) @ " " @ (getWord(%buttonPosition, 1) - 10);
        extent = "123 82";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        bitmap = "platform/client/ui/closetGuiFUE_step4_inactive";
    }; @ "ETSNonModalProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = (getWord(ClosetTabContainer.getPosition(), 0) + 759) @ " " @ (getWord(ClosetTabContainer.getPosition(), 0) + 492);
        extent = "123 82";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        bitmap = "platform/client/ui/closetGuiFUE_step5_inactive";
    };
    %this.addStepWithName(%newActiveStep, %newInactiveStep, "Snapshot");
    %this.refreshingOrInitializing = 0;
    %this.initialized = 1;
}
function closetGuiFUEHideTipsCtrl::onAction(%this)
{
    if (%this.getValue())
    {
        %this.hideTips();
    }
    else
    {
        %this.showTips();
    }
}
function closetGuiFUEHideTipsCtrl::hideTips(%this)
{
    if (ClosetGuiFUE.refreshingOrInitializing)
    {
        %sched = gGetField(%this, "closetGuiFUEHideTipsCtrlShowOrHideTipsSched");
        cancel(%sched);
        %sched = %this.schedule(250, showOrHideTips);
        gSetField(%this, "closetGuiFUEHideTipsCtrlShowOrHideTipsSched", %sched);
        return;
    }
    %i = 0;
    while (%i < $gClosetGuiFueStepCount)
    {
        %i.setVisible(ClosetGuiFUE.stepContainers["active"], 0);
        if (isObject(%i, ClosetGuiFUE.stepContainers["inactive"]))
        {
            %i.setVisible(ClosetGuiFUE.stepContainers["inactive"], 0);
        }
        %i = %i + 1;
    }
}
function closetGuiFUEHideTipsCtrl::showTips(%this)
{
    if (ClosetGuiFUE.refreshingOrInitializing)
    {
        %sched = gGetField(%this, "closetGuiFUEHideTipsCtrlShowOrHideTipsSched");
        cancel(%sched);
        %sched = %this.schedule(250, showOrHideTips);
        gSetField(%this, "closetGuiFUEHideTipsCtrlShowOrHideTipsSched", %sched);
        return;
    }
    %i = 0;
    while (%i < $gClosetGuiFueStepCount)
    {
        %i.setVisible(ClosetGuiFUE.stepContainers["active"], (%i == $gClosetGuiFueCurrentStep));
        if (isObject(%i, ClosetGuiFUE.stepContainers["inactive"]))
        {
            %i.setVisible(ClosetGuiFUE.stepContainers["inactive"], (%i != $gClosetGuiFueCurrentStep));
        }
        %i = %i + 1;
    }
}
