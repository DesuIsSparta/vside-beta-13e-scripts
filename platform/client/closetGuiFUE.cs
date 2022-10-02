$gClosetGuiFueStepCount = 0;
$gClosetGuiFueCurrentStep = -1;
function ClosetGuiFUE::open(%this)
{
    if (%this.refreshingOrInitializing)
    {
        %sched = gGetField(%this, "closetGuiFUEOpenSched");
        cancel(%sched);
        %sched = %this.schedule(250, open);
        gSetField(%this, "closetGuiFUEOpenSched", %sched);
        return ;
    }
    if (!%this.initialized)
    {
        %this.arrivedAtFinalTip = 0;
        %this.Initialize();
    }
    %this.reposition();
    %this.showAllAsInactive();
    %this.goToStepByName(ClosetTabs.getCurrentTab().name);
    return ;
}
function ClosetGuiFUE::close(%this)
{
    %this.hide();
    return ;
}
function ClosetGuiFUE::show(%this)
{
    if (%this.refreshingOrInitializing)
    {
        %sched = gGetField(%this, "closetGuiFUEShowSched");
        cancel(%sched);
        %sched = %this.schedule(250, show);
        gSetField(%this, "closetGuiFUEShowSched", %sched);
        return ;
    }
    %this.setVisible(1);
    %this.goToStepByName(ClosetTabs.getCurrentTab().name);
    return ;
}
function ClosetGuiFUE::hide(%this)
{
    %this.setVisible(0);
    return ;
}
function ClosetGuiFUE::reposition(%this)
{
    %this.position = ((getWord(ClosetGuiPositioner.extent, 0) - 960) / 2) + 1 SPC (((getWord(ClosetGuiPositioner.extent, 1) - 576) - 32) / 2) + 1;
    return ;
}
function ClosetGuiFUE::showAllAsInactive(%this)
{
    if (%this.refreshingOrInitializing)
    {
        %sched = gGetField(%this, "closetGuiFUEShowAllAsInactiveSched");
        cancel(%sched);
        %sched = %this.schedule(250, showAllAsInactive);
        gSetField(%this, "closetGuiFUEShowAllAsInactiveSched", %sched);
        return ;
    }
    %i = 0;
    while (%i < $gClosetGuiFueStepCount)
    {
        %this.stepContainers[(%i,"active")].setVisible(0);
        if (isObject(%this.stepContainers[(%i,"inactive")]))
        {
            %this.stepContainers[(%i,"inactive")].setVisible(!%this.hideTipsCtrl.getValue());
        }
        %i = %i + 1;
    }
    ClosetGuiFUE.show();
    return ;
}
function ClosetGuiFUE::addStep(%this, %activeContainer, %inactiveContainer)
{
    if (!isObject(%activeContainer))
    {
        error(getScopeName() SPC "- empty activeContainer object -" SPC getTrace());
        return 0;
    }
    if (!((%inactiveContainer $= "")) && !isObject(%inactiveContainer))
    {
        error(getScopeName() SPC "- empty inactiveContainer object -" SPC getTrace());
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
        warn(getScopeName() SPC "- step" SPC %stepName SPC "not added -" SPC getTrace());
        %this.stepNumbersByName[strlwr(%stepName)] = -1;
        return 0;
    }
    return 1;
}
function ClosetGuiFUE::firstStep(%this)
{
    if ($gClosetGuiFueStepCount == 0)
    {
        error(getScopeName() SPC "- first step has not yet been set -" SPC getTrace());
        return ;
    }
    %this.hideCurrentStep();
    $gClosetGuiFueCurrentStep = 0;
    %this.showCurrentStep();
    return ;
}
function ClosetGuiFUE::nextStep(%this)
{
    %this.hideCurrentStep();
    if (($gClosetGuiFueCurrentStep >= -1) && ($gClosetGuiFueCurrentStep < ($gClosetGuiFueStepCount - 1)))
    {
        $gClosetGuiFueCurrentStep = $gClosetGuiFueCurrentStep + 1;
    }
    else
    {
        $gClosetGuiFueCurrentStep = -1;
    }
    %this.showCurrentStep();
    return ;
}
function ClosetGuiFUE::goToStepByName(%this, %stepName)
{
    if (0)
    {
        error(getScopeName() SPC "- invalid stepName" SPC %stepName SPC "-" SPC getTrace());
        return ;
    }
    %this.hideCurrentStep();
    if (%stepName $= "Shops")
    {
        closetGuiFUEShopsDirBitmap.setVisible($gCurrentStoreName $= "");
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
    return ;
}
function ClosetGuiFUE::goToStepByNumber(%this, %stepNumber)
{
    if ((%stepNumber < 0) && (%stepNumber >= $gClosetGuiFueStepCount))
    {
        error(getScopeName() SPC "- invalid stepNumber" SPC %stepNumber SPC "-" SPC getTrace());
        return ;
    }
    %this.hideCurrentStep();
    $gClosetGuiFueCurrentStep = %stepNumber;
    %this.showCurrentStep();
    return ;
}
function ClosetGuiFUE::showCurrentStep(%this)
{
    if ($gClosetGuiFueCurrentStep == -1)
    {
        return ;
    }
    if (%this.refreshingOrInitializing)
    {
        %sched = gGetField(%this, "closetGuiFUEShowCurrentStepSched");
        cancel(%sched);
        %sched = %this.schedule(250, showCurrentStep);
        gSetField(%this, "closetGuiFUEShowCurrentStepSched", %sched);
        return ;
    }
    %this.stepContainers[($gClosetGuiFueCurrentStep,"inactive")].setVisible(0);
    %this.stepContainers[($gClosetGuiFueCurrentStep,"active")].setVisible(!%this.hideTipsCtrl.getValue());
    if (!%this.arrivedAtFinalTip)
    {
        %this.arrivedAtFinalTip = $gClosetGuiFueCurrentStep == %this.stepNumbersByName[strlwr("Snapshot")];
    }
    return ;
}
function ClosetGuiFUE::hideCurrentStep(%this)
{
    if ($gClosetGuiFueCurrentStep == -1)
    {
        return ;
    }
    %this.stepContainers[($gClosetGuiFueCurrentStep,"active")].setVisible(0);
    %this.stepContainers[($gClosetGuiFueCurrentStep,"inactive")].setVisible(!%this.hideTipsCtrl.getValue());
    return ;
}
function ClosetGuiFUE::refresh(%this)
{
    if (%this.refreshingOrInitializing)
    {
        %sched = gGetField(%this, "closetGuiFUERefreshSched");
        cancel(%sched);
        %sched = %this.schedule(250, refresh);
        gSetField(%this, "closetGuiFUERefreshSched", %sched);
        return ;
    }
    %this.refreshingOrInitializing = 1;
    ClosetGuiFUE.deleteMembers();
    $gClosetGuiFueStepCount = 0;
    $gClosetGuiFueCurrentStep = -1;
    %this.Initialize();
    if (%visible)
    {
        %this.showAllAsInactive();
    }
    %this.refreshingOrInitializing = 0;
    return ;
}
function ClosetGuiFUE::Initialize(%this)
{
    %this.refreshingOrInitializing = 1;
    if (!isObject(closetGuiFUEHideTipsCtrl))
    {
        %this.hideTipsCtrl = new GuiCheckBoxCtrl(closetGuiFUEHideTipsCtrl)
        {
            profile = "ETSCheckBoxProfile";
            horizSizing = "right";
            vertSizing = "bottom";
            position = "863 557";
            extent = "99 20";
            minExtent = "8 2";
            sluggishness = -1;
            visible = 0;
            text = "Don\'t Show Tips";
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
    %newActiveStep = new GuiControl()
    {
        profile = "ETSNonModalProfile";
        horizSizing = ClosetGuiFUE.horizSizing;
        vertSizing = ClosetGuiFUE.vertSizing;
        position = "0 0";
        extent = ClosetGuiFUE.extent;
        minExtent = ClosetGuiFUE.minExtent;
        sluggishness = ClosetGuiFUE.sluggishness;
        visible = 0;
    };
    %newInactiveStep = new GuiControl()
    {
        profile = "ETSNonModalProfile";
        horizSizing = ClosetGuiFUE.horizSizing;
        vertSizing = ClosetGuiFUE.vertSizing;
        position = "0 0";
        extent = ClosetGuiFUE.extent;
        minExtent = ClosetGuiFUE.minExtent;
        sluggishness = ClosetGuiFUE.sluggishness;
        visible = 0;
    };
    %this.addStepWithName(%newActiveStep, %newInactiveStep, "Body");
    %buttonPosition = ClosetTabs.getTabWithName("Closet").button.getPosition();
    %newActiveStep = new GuiControl()
    {
        profile = "ETSNonModalProfile";
        horizSizing = ClosetGuiFUE.horizSizing;
        vertSizing = ClosetGuiFUE.vertSizing;
        position = "0 0";
        extent = ClosetGuiFUE.extent;
        minExtent = ClosetGuiFUE.minExtent;
        sluggishness = ClosetGuiFUE.sluggishness;
        visible = 0;
    };
    %newInactiveStep = new GuiControl()
    {
        profile = "ETSNonModalProfile";
        horizSizing = ClosetGuiFUE.horizSizing;
        vertSizing = ClosetGuiFUE.vertSizing;
        position = "0 0";
        extent = ClosetGuiFUE.extent;
        minExtent = ClosetGuiFUE.minExtent;
        sluggishness = ClosetGuiFUE.sluggishness;
        visible = 0;
    };
    %this.addStepWithName(%newActiveStep, %newInactiveStep, "Closet");
    if (!ClosetTabs.tabShopsInitialized)
    {
        ClosetTabs.fillStoreTab();
    }
    %button = ClosetTabs.getTabWithName("Shops").button;
    %buttonPosition = %button.getPosition();
    %newActiveStep = new GuiControl()
    {
        profile = "ETSNonModalProfile";
        horizSizing = ClosetGuiFUE.horizSizing;
        vertSizing = ClosetGuiFUE.vertSizing;
        position = "0 0";
        extent = ClosetGuiFUE.extent;
        minExtent = ClosetGuiFUE.minExtent;
        sluggishness = ClosetGuiFUE.sluggishness;
        visible = 0;
    };
    if (!isObject(closetGuiFUEShopsDirBitmap))
    {
        %this.shopsDirBitmap = new GuiBitmapCtrl(closetGuiFUEShopsDirBitmap)
        {
            profile = "ETSNonModalProfile";
            horizSizing = "right";
            vertSizing = "bottom";
            position = "35 125";
            extent = "441 375";
            minExtent = "1 1";
            sluggishness = -1;
            visible = $gCurrentStoreName $= "";
            bitmap = "platform/client/ui/closetGuiFUE_shop_active_shopsDir";
        };
        %this.add(%this.shopsDirBitmap);
    }
    else
    {
        %this.shopsDirBitmap = closetGuiFUEShopsDirBitmap;
        %this.add(%this.shopsDirBitmap);
    }
    %credsBitmapCtrl = new GuiBitmapCtrl(closetGuiFUE_vPoints_vBux_Image)
    {
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
    %newInactiveStep = new GuiControl()
    {
        profile = "ETSNonModalProfile";
        horizSizing = ClosetGuiFUE.horizSizing;
        vertSizing = ClosetGuiFUE.vertSizing;
        position = "0 0";
        extent = ClosetGuiFUE.extent;
        minExtent = ClosetGuiFUE.minExtent;
        sluggishness = ClosetGuiFUE.sluggishness;
        visible = 0;
    };
    %this.addStepWithName(%newActiveStep, %newInactiveStep, "Shops");
    %buttonPosition = ClosetTabs.getTabWithName("Snapshot").button.getPosition();
    %newActiveStep = new GuiControl()
    {
        profile = "ETSNonModalProfile";
        horizSizing = ClosetGuiFUE.horizSizing;
        vertSizing = ClosetGuiFUE.vertSizing;
        position = "0 0";
        extent = ClosetGuiFUE.extent;
        minExtent = ClosetGuiFUE.minExtent;
        sluggishness = ClosetGuiFUE.sluggishness;
        visible = 0;
    };
    %newInactiveStep = new GuiControl()
    {
        profile = "ETSNonModalProfile";
        horizSizing = ClosetGuiFUE.horizSizing;
        vertSizing = ClosetGuiFUE.vertSizing;
        position = "0 0";
        extent = ClosetGuiFUE.extent;
        minExtent = ClosetGuiFUE.minExtent;
        sluggishness = ClosetGuiFUE.sluggishness;
        visible = 0;
    };
    %this.addStepWithName(%newActiveStep, %newInactiveStep, "Snapshot");
    %this.refreshingOrInitializing = 0;
    %this.initialized = 1;
    return ;
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
    return ;
}
function closetGuiFUEHideTipsCtrl::hideTips(%this)
{
    if (ClosetGuiFUE.refreshingOrInitializing)
    {
        %sched = gGetField(%this, "closetGuiFUEHideTipsCtrlShowOrHideTipsSched");
        cancel(%sched);
        %sched = %this.schedule(250, showOrHideTips);
        gSetField(%this, "closetGuiFUEHideTipsCtrlShowOrHideTipsSched", %sched);
        return ;
    }
    %i = 0;
    while (%i < $gClosetGuiFueStepCount)
    {
        ClosetGuiFUE.stepContainers[(%i,"active")].setVisible(0);
        if (isObject(ClosetGuiFUE.stepContainers[(%i,"inactive")]))
        {
            ClosetGuiFUE.stepContainers[(%i,"inactive")].setVisible(0);
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
        return ;
    }
    %i = 0;
    while (%i < $gClosetGuiFueStepCount)
    {
        ClosetGuiFUE.stepContainers[(%i,"active")].setVisible(%i == $gClosetGuiFueCurrentStep);
        if (isObject(ClosetGuiFUE.stepContainers[(%i,"inactive")]))
        {
            ClosetGuiFUE.stepContainers[(%i,"inactive")].setVisible(%i != $gClosetGuiFueCurrentStep);
        }
        %i = %i + 1;
    }
}


