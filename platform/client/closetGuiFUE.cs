$gClosetGuiFueStepCount = 0;
$gClosetGuiFueCurrentStep = -(1.0);
function ClosetGuiFUE::open(%this) {
    if (%this.refreshingOrInitializing) {
        %sched = gGetField(%this, "closetGuiFUEOpenSched");
        cancel(%sched);
        %sched = %this.schedule(250);
        open;
        gSetField(%this, "closetGuiFUEOpenSched", %sched);
        return;
    }
    if (!(%this.initialized)) {
        %this.arrivedAtFinalTip = 0;
        %this.Initialize();
    }
    %this.reposition();
    %this.showAllAsInactive();
    %this.goToStepByName(ClosetTabs.getCurrentTab().name);
};
function ClosetGuiFUE::close(%this) {
    %this.hide();
};
function ClosetGuiFUE::show(%this) {
    if (%this.refreshingOrInitializing) {
        %sched = gGetField(%this, "closetGuiFUEShowSched");
        cancel(%sched);
        %sched = %this.schedule(250);
        show;
        gSetField(%this, "closetGuiFUEShowSched", %sched);
        return;
    }
    %this.setVisible(1);
    %this.goToStepByName(ClosetTabs.getCurrentTab().name);
};
function ClosetGuiFUE::hide(%this) {
    %this.setVisible(0);
};
function ClosetGuiFUE::reposition(%this) {
    %this.position = (1.0 + (2.0 / (960.0 - getWord(ClosetGuiPositioner, ClosetTabs.getCurrentTab().extent, 0)))) @ " " @ (1.0 + (2.0 / (32.0 - (576.0 - getWord(ClosetGuiPositioner, ClosetTabs.getCurrentTab().extent, 1)))));
};
function ClosetGuiFUE::showAllAsInactive(%this) {
    if (%this.refreshingOrInitializing) {
        %sched = gGetField(%this, "closetGuiFUEShowAllAsInactiveSched");
        cancel(%sched);
        %sched = %this.schedule(250);
        showAllAsInactive;
        gSetField(%this, "closetGuiFUEShowAllAsInactiveSched", %sched);
        return;
    }
    %i = 0;
    if (($gClosetGuiFueStepCount < %i)) {
        %i @ "active".setVisible(%this.stepContainers, 0);
        if (isObject(%i @ "inactive", %this.stepContainers)) {
            %i @ "inactive".setVisible(%this.stepContainers, !(%this.hideTipsCtrl.getValue()));
        }
        %i = (1.0 + %i);
    }
    ClosetGuiFUE.show();
};
function ClosetGuiFUE::addStep(%this, %activeContainer, %inactiveContainer) {
    if (!(isObject(%activeContainer))) {
        error(getScopeName() @ " " @ "- empty activeContainer object -" @ " " @ getTrace());
        return 0;
    }
    if (!(%inactiveContainer $= "")) {
    }
    if (!(isObject(%inactiveContainer))) {
        error(getScopeName() @ " " @ "- empty inactiveContainer object -" @ " " @ getTrace());
        return 0;
    }
    %activeContainer.setVisible(0);
    %this.add(%activeContainer);
    %this.stepContainers = %activeContainer TAB $gClosetGuiFueStepCount @ "active";
    if (isObject(%inactiveContainer)) {
        %inactiveContainer.setVisible(0);
        %this.add(%inactiveContainer);
    }
    %this.stepContainers = %inactiveContainer TAB $gClosetGuiFueStepCount @ "inactive";
    $gClosetGuiFueStepCount = (1.0 + $gClosetGuiFueStepCount);
    return 1;
};
function ClosetGuiFUE::addStepWithName(%this, %activeContainer, %inactiveContainer, %stepName) {
    %this.stepNumbersByName = $gClosetGuiFueStepCount @ strlwr(%stepName);
    if (!(%this.addStep(%activeContainer, %inactiveContainer))) {
        warn(getScopeName() @ " " @ "- step" @ " " @ %stepName @ " " @ "not added -" @ " " @ getTrace());
        %this.stepNumbersByName = -(1.0) @ strlwr(%stepName);
        return 0;
    }
    return 1;
};
function ClosetGuiFUE::firstStep(%this) {
    if ((0.0 == $gClosetGuiFueStepCount)) {
        error(getScopeName() @ " " @ "- first step has not yet been set -" @ " " @ getTrace());
        return;
    }
    %this.hideCurrentStep();
    $gClosetGuiFueCurrentStep = 0;
    %this.showCurrentStep();
};
function ClosetGuiFUE::nextStep(%this) {
    %this.hideCurrentStep();
    if ((-(1.0) >= $gClosetGuiFueCurrentStep)) {
    }
    if (((1.0 - $gClosetGuiFueStepCount) < $gClosetGuiFueCurrentStep)) {
        $gClosetGuiFueCurrentStep = (1.0 + $gClosetGuiFueCurrentStep);
    }
    $gClosetGuiFueCurrentStep = -(1.0);
    %this.showCurrentStep();
};
function ClosetGuiFUE::goToStepByName(%this, %stepName) {
    if (0) {
        error(getScopeName() @ " " @ "- invalid stepName" @ " " @ %stepName @ " " @ "-" @ " " @ getTrace());
        return;
    }
    %this.hideCurrentStep();
    if ((%stepName $= "Shops")) {
        (closetGuiFUEShopsDirBitmap @ " " @ $gCurrentStoreName $= "").setVisible();
        !(StoreShoppingBag @ " " @ $gCurrentStoreName $= "").setVisible();
        !(StoreAddItemsButton @ " " @ $gCurrentStoreName $= "").setVisible();
        ClosetTabs.setLeaveStoreControlsVisible(0);
    }
    closetGuiFUEShopsDirBitmap.setVisible(0);
    $gClosetGuiFueCurrentStep = %this.stepNumbersByName;
    strlwr(%stepName);
    %this.showCurrentStep();
};
function ClosetGuiFUE::goToStepByNumber(%this, %stepNumber) {
    if ((0.0 < %stepNumber)) {
    }
    if (($gClosetGuiFueStepCount >= %stepNumber)) {
        error(getScopeName() @ " " @ "- invalid stepNumber" @ " " @ %stepNumber @ " " @ "-" @ " " @ getTrace());
        return;
    }
    %this.hideCurrentStep();
    $gClosetGuiFueCurrentStep = %stepNumber;
    %this.showCurrentStep();
};
function ClosetGuiFUE::showCurrentStep(%this) {
    if ((-(1.0) == $gClosetGuiFueCurrentStep)) {
        return;
    }
    if (%this.refreshingOrInitializing) {
        %sched = gGetField(%this, "closetGuiFUEShowCurrentStepSched");
        cancel(%sched);
        %sched = %this.schedule(250);
        showCurrentStep;
        gSetField(%this, "closetGuiFUEShowCurrentStepSched", %sched);
        return;
    }
    $gClosetGuiFueCurrentStep @ "inactive".setVisible(%this.stepContainers, 0);
    $gClosetGuiFueCurrentStep @ "active".setVisible(%this.stepContainers, !(%this.hideTipsCtrl.getValue()));
    if (!(%this.arrivedAtFinalTip)) {
        %this.arrivedAtFinalTip = strlwr("Snapshot") @ (%this.stepNumbersByName == $gClosetGuiFueCurrentStep);
    }
};
function ClosetGuiFUE::hideCurrentStep(%this) {
    if ((-(1.0) == $gClosetGuiFueCurrentStep)) {
        return;
    }
    $gClosetGuiFueCurrentStep @ "active".setVisible(%this.stepContainers, 0);
    $gClosetGuiFueCurrentStep @ "inactive".setVisible(%this.stepContainers, !(%this.hideTipsCtrl.getValue()));
};
function ClosetGuiFUE::refresh(%this) {
    if (%this.refreshingOrInitializing) {
        %sched = gGetField(%this, "closetGuiFUERefreshSched");
        cancel(%sched);
        %sched = %this.schedule(250);
        refresh;
        gSetField(%this, "closetGuiFUERefreshSched", %sched);
        return;
    }
    %this.refreshingOrInitializing = 1;
    ClosetGuiFUE.deleteMembers();
    $gClosetGuiFueStepCount = 0;
    $gClosetGuiFueCurrentStep = -(1.0);
    %this.Initialize();
    if (%visible) {
        %this.showAllAsInactive();
    }
    %this.refreshingOrInitializing = 0;
};
function ClosetGuiFUE::Initialize(%this) {
    %this.refreshingOrInitializing = 1;
    if (!(isObject(closetGuiFUEHideTipsCtrl))) {
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
    %this.hideTipsCtrl = closetGuiFUEHideTipsCtrl;
    %this.add(%this.hideTipsCtrl);
    %buttonPosition = ClosetTabs.getTabWithName("Body").button.getPosition();
    %newActiveStep = new GuiControl("") {
        profile = 0 @ "ETSNonModalProfile";
        horizSizing = ClosetGuiFUE @ horizSizing;
        vertSizing = ClosetGuiFUE @ vertSizing;
        position = "0 0";
        extent = ClosetGuiFUE @ extent;
        minExtent = ClosetGuiFUE @ minExtent;
        sluggishness = ClosetGuiFUE @ sluggishness;
        visible = 0;
    };
    new GuiBitmapCtrl(closetGuiFUEWelcomeImage) {
        profile = new GuiBitmapCtrl("") {
        profile = "ETSNonModalProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = (10.0 - getWord(%buttonPosition, 0)) @ " " @ (10.0 - getWord(%buttonPosition, 1));
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
        profile = 0 @ "ETSNonModalProfile";
        horizSizing = ClosetGuiFUE @ horizSizing;
        vertSizing = ClosetGuiFUE @ vertSizing;
        position = "0 0";
        extent = ClosetGuiFUE @ extent;
        minExtent = ClosetGuiFUE @ minExtent;
        sluggishness = ClosetGuiFUE @ sluggishness;
        visible = 0;
    };
    new GuiBitmapCtrl("") {
        profile = "ETSNonModalProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = (10.0 - getWord(%buttonPosition, 0)) @ " " @ (10.0 - getWord(%buttonPosition, 1));
        extent = "123 82";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        bitmap = "platform/client/ui/closetGuiFUE_step3_inactive";
    };
    %this.addStepWithName(%newActiveStep, %newInactiveStep, "Body");
    %buttonPosition = ClosetTabs.getTabWithName("Closet").button.getPosition();
    %newActiveStep = new GuiControl("") {
        profile = 0 @ "ETSNonModalProfile";
        horizSizing = ClosetGuiFUE @ horizSizing;
        vertSizing = ClosetGuiFUE @ vertSizing;
        position = "0 0";
        extent = ClosetGuiFUE @ extent;
        minExtent = ClosetGuiFUE @ minExtent;
        sluggishness = ClosetGuiFUE @ sluggishness;
        visible = 0;
    };
    new GuiBitmapCtrl(closetGuiFUEWelcomeImage_b : closetGuiFUEWelcomeImage);
    new GuiBitmapCtrl("") {
        profile = "ETSNonModalProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = (10.0 - getWord(%buttonPosition, 0)) @ " " @ (10.0 - getWord(%buttonPosition, 1));
        extent = "123 82";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        bitmap = "platform/client/ui/closetGuiFUE_step2_active";
    };
    %newInactiveStep = new GuiControl("") {
        profile = 0 @ "ETSNonModalProfile";
        horizSizing = ClosetGuiFUE @ horizSizing;
        vertSizing = ClosetGuiFUE @ vertSizing;
        position = "0 0";
        extent = ClosetGuiFUE @ extent;
        minExtent = ClosetGuiFUE @ minExtent;
        sluggishness = ClosetGuiFUE @ sluggishness;
        visible = 0;
    };
    new GuiBitmapCtrl("") {
        profile = "ETSNonModalProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = (10.0 - getWord(%buttonPosition, 0)) @ " " @ (10.0 - getWord(%buttonPosition, 1));
        extent = "123 82";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        bitmap = "platform/client/ui/closetGuiFUE_step2_inactive";
    };
    %this.addStepWithName(%newActiveStep, %newInactiveStep, "Closet");
    if (!(tabShopsInitialized)) {
        ClosetTabs.fillStoreTab();
    }
    %button = ClosetTabs.getTabWithName("Shops").button;
    ClosetTabs;
    %buttonPosition = %button.getPosition();
    %newActiveStep = new GuiControl("") {
        profile = 0 @ "ETSNonModalProfile";
        horizSizing = ClosetGuiFUE @ horizSizing;
        vertSizing = ClosetGuiFUE @ vertSizing;
        position = "0 0";
        extent = ClosetGuiFUE @ extent;
        minExtent = ClosetGuiFUE @ minExtent;
        sluggishness = ClosetGuiFUE @ sluggishness;
        visible = 0;
    };
    new GuiBitmapCtrl("") {
        profile = "ETSNonModalProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = (10.0 - getWord(%buttonPosition, 0)) @ " " @ (10.0 - getWord(%buttonPosition, 1));
        extent = "123 82";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        bitmap = "platform/client/ui/closetGuiFUE_step1_active";
    };
    if (!(isObject(closetGuiFUEShopsDirBitmap))) {
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
    %this.shopsDirBitmap = closetGuiFUEShopsDirBitmap;
    %this.add(%this.shopsDirBitmap);
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
        profile = 0 @ "ETSNonModalProfile";
        horizSizing = ClosetGuiFUE @ horizSizing;
        vertSizing = ClosetGuiFUE @ vertSizing;
        position = "0 0";
        extent = ClosetGuiFUE @ extent;
        minExtent = ClosetGuiFUE @ minExtent;
        sluggishness = ClosetGuiFUE @ sluggishness;
        visible = 0;
    };
    new GuiBitmapCtrl("") {
        profile = "ETSNonModalProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = (10.0 - getWord(%buttonPosition, 0)) @ " " @ (10.0 - getWord(%buttonPosition, 1));
        extent = "123 82";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        bitmap = "platform/client/ui/closetGuiFUE_step1_inactive";
    };
    %this.addStepWithName(%newActiveStep, %newInactiveStep, "Shops");
    %buttonPosition = ClosetTabs.getTabWithName("Snapshot").button.getPosition();
    %newActiveStep = new GuiControl("") {
        profile = 0 @ "ETSNonModalProfile";
        horizSizing = ClosetGuiFUE @ horizSizing;
        vertSizing = ClosetGuiFUE @ vertSizing;
        position = "0 0";
        extent = ClosetGuiFUE @ extent;
        minExtent = ClosetGuiFUE @ minExtent;
        sluggishness = ClosetGuiFUE @ sluggishness;
        visible = 0;
    };
    new GuiBitmapCtrl("") {
        profile = new GuiBitmapCtrl("") {
        profile = "ETSNonModalProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = (10.0 - getWord(%buttonPosition, 0)) @ " " @ (10.0 - getWord(%buttonPosition, 1));
        extent = "123 82";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        bitmap = "platform/client/ui/closetGuiFUE_step4_active";
    }; @ "ETSNonModalProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = (759.0 + getWord(ClosetTabContainer.getPosition(), 0)) @ " " @ (492.0 + getWord(ClosetTabContainer.getPosition(), 0));
        extent = "123 82";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        bitmap = "platform/client/ui/closetGuiFUE_step5_active";
    };
    %newInactiveStep = new GuiControl("") {
        profile = 0 @ "ETSNonModalProfile";
        horizSizing = ClosetGuiFUE @ horizSizing;
        vertSizing = ClosetGuiFUE @ vertSizing;
        position = "0 0";
        extent = ClosetGuiFUE @ extent;
        minExtent = ClosetGuiFUE @ minExtent;
        sluggishness = ClosetGuiFUE @ sluggishness;
        visible = 0;
    };
    new GuiBitmapCtrl("") {
        profile = new GuiBitmapCtrl("") {
        profile = "ETSNonModalProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = (10.0 - getWord(%buttonPosition, 0)) @ " " @ (10.0 - getWord(%buttonPosition, 1));
        extent = "123 82";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        bitmap = "platform/client/ui/closetGuiFUE_step4_inactive";
    }; @ "ETSNonModalProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = (759.0 + getWord(ClosetTabContainer.getPosition(), 0)) @ " " @ (492.0 + getWord(ClosetTabContainer.getPosition(), 0));
        extent = "123 82";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        bitmap = "platform/client/ui/closetGuiFUE_step5_inactive";
    };
    %this.addStepWithName(%newActiveStep, %newInactiveStep, "Snapshot");
    %this.refreshingOrInitializing = 0;
    %this.initialized = 1;
};
function closetGuiFUEHideTipsCtrl::onAction(%this) {
    if (%this.getValue()) {
        %this.hideTips();
    }
    %this.showTips();
};
function closetGuiFUEHideTipsCtrl::hideTips(%this) {
    if (%this.refreshingOrInitializing) {
        %sched = gGetField(%this, "closetGuiFUEHideTipsCtrlShowOrHideTipsSched");
        ClosetGuiFUE;
        cancel(%sched);
        %sched = %this.schedule(250);
        showOrHideTips;
        gSetField(%this, "closetGuiFUEHideTipsCtrlShowOrHideTipsSched", %sched);
        return;
    }
    %i = 0;
    if (($gClosetGuiFueStepCount < %i)) {
        %i @ "active" @ ClosetGuiFUE.setVisible(%this.stepContainers, 0);
        if (isObject(%i @ "inactive" @ ClosetGuiFUE, %this.stepContainers)) {
            %i @ "inactive" @ ClosetGuiFUE.setVisible(%this.stepContainers, 0);
        }
        %i = (1.0 + %i);
    }
};
function closetGuiFUEHideTipsCtrl::showTips(%this) {
    if (%this.refreshingOrInitializing) {
        %sched = gGetField(%this, "closetGuiFUEHideTipsCtrlShowOrHideTipsSched");
        ClosetGuiFUE;
        cancel(%sched);
        %sched = %this.schedule(250);
        showOrHideTips;
        gSetField(%this, "closetGuiFUEHideTipsCtrlShowOrHideTipsSched", %sched);
        return;
    }
    %i = 0;
    if (($gClosetGuiFueStepCount < %i)) {
        %i @ "active" @ ClosetGuiFUE.setVisible(%this.stepContainers, ($gClosetGuiFueCurrentStep == %i));
        if (isObject(%i @ "inactive" @ ClosetGuiFUE, %this.stepContainers)) {
            %i @ "inactive" @ ClosetGuiFUE.setVisible(%this.stepContainers, ($gClosetGuiFueCurrentStep != %i));
        }
        %i = (1.0 + %i);
    }
};
