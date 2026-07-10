$gClosetGuiFueStepCount = 0;
$gClosetGuiFueCurrentStep = -(1.0);
function ClosetGuiFUE::open(%this) {
    if (%this.refreshingOrInitializing) {
        %sched = gGetField(%this, "closetGuiFUEOpenSched");
        cancel(%sched);
        %sched = 250.schedule(%this);
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
    ClosetTabs.getCurrentTab().name.goToStepByName(%this);
};
function ClosetGuiFUE::close(%this) {
    %this.hide();
};
function ClosetGuiFUE::show(%this) {
    if (%this.refreshingOrInitializing) {
        %sched = gGetField(%this, "closetGuiFUEShowSched");
        cancel(%sched);
        %sched = 250.schedule(%this);
        show;
        gSetField(%this, "closetGuiFUEShowSched", %sched);
        return;
    }
    1.setVisible(%this);
    ClosetTabs.getCurrentTab().name.goToStepByName(%this);
};
function ClosetGuiFUE::hide(%this) {
    0.setVisible(%this);
};
function ClosetGuiFUE::reposition(%this) {
    %this.position = (((getWord(ClosetGuiPositioner, ClosetTabs.getCurrentTab().extent, 0) - 960.0) / 2.0) + 1.0) @ " " @ ((((getWord(ClosetGuiPositioner, ClosetTabs.getCurrentTab().extent, 1) - 576.0) - 32.0) / 2.0) + 1.0);
};
function ClosetGuiFUE::showAllAsInactive(%this) {
    if (%this.refreshingOrInitializing) {
        %sched = gGetField(%this, "closetGuiFUEShowAllAsInactiveSched");
        cancel(%sched);
        %sched = 250.schedule(%this);
        showAllAsInactive;
        gSetField(%this, "closetGuiFUEShowAllAsInactiveSched", %sched);
        return;
    }
    %i = 0;
    while ((%i < $gClosetGuiFueStepCount)) {
        0.setVisible(%i @ "active", %this.stepContainers);
        if (isObject(%i @ "inactive", %this.stepContainers)) {
            !(%this.hideTipsCtrl.getValue()).setVisible(%i @ "inactive", %this.stepContainers);
        }
        %i = (%i + 1.0);
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
    0.setVisible(%activeContainer);
    %activeContainer.add(%this);
    %this.stepContainers = %activeContainer TAB $gClosetGuiFueStepCount @ "active";
    if (isObject(%inactiveContainer)) {
        0.setVisible(%inactiveContainer);
        %inactiveContainer.add(%this);
    }
    %this.stepContainers = %inactiveContainer TAB $gClosetGuiFueStepCount @ "inactive";
    $gClosetGuiFueStepCount = ($gClosetGuiFueStepCount + 1.0);
    return 1;
};
function ClosetGuiFUE::addStepWithName(%this, %activeContainer, %inactiveContainer, %stepName) {
    %this.stepNumbersByName = $gClosetGuiFueStepCount @ strlwr(%stepName);
    if (!(%inactiveContainer.addStep(%this, %activeContainer))) {
        warn(getScopeName() @ " " @ "- step" @ " " @ %stepName @ " " @ "not added -" @ " " @ getTrace());
        %this.stepNumbersByName = -(1.0) @ strlwr(%stepName);
        return 0;
    }
    return 1;
};
function ClosetGuiFUE::firstStep(%this) {
    if (($gClosetGuiFueStepCount == 0.0)) {
        error(getScopeName() @ " " @ "- first step has not yet been set -" @ " " @ getTrace());
        return;
    }
    %this.hideCurrentStep();
    $gClosetGuiFueCurrentStep = 0;
    %this.showCurrentStep();
};
function ClosetGuiFUE::nextStep(%this) {
    %this.hideCurrentStep();
    if (($gClosetGuiFueCurrentStep >= -(1.0))) {
    }
    if (($gClosetGuiFueCurrentStep < ($gClosetGuiFueStepCount - 1.0))) {
        $gClosetGuiFueCurrentStep = ($gClosetGuiFueCurrentStep + 1.0);
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
        0.setLeaveStoreControlsVisible(ClosetTabs);
    }
    0.setVisible(closetGuiFUEShopsDirBitmap);
    $gClosetGuiFueCurrentStep = %this.stepNumbersByName;
    strlwr(%stepName);
    %this.showCurrentStep();
};
function ClosetGuiFUE::goToStepByNumber(%this, %stepNumber) {
    if ((%stepNumber < 0.0)) {
    }
    if ((%stepNumber >= $gClosetGuiFueStepCount)) {
        error(getScopeName() @ " " @ "- invalid stepNumber" @ " " @ %stepNumber @ " " @ "-" @ " " @ getTrace());
        return;
    }
    %this.hideCurrentStep();
    $gClosetGuiFueCurrentStep = %stepNumber;
    %this.showCurrentStep();
};
function ClosetGuiFUE::showCurrentStep(%this) {
    if (($gClosetGuiFueCurrentStep == -(1.0))) {
        return;
    }
    if (%this.refreshingOrInitializing) {
        %sched = gGetField(%this, "closetGuiFUEShowCurrentStepSched");
        cancel(%sched);
        %sched = 250.schedule(%this);
        showCurrentStep;
        gSetField(%this, "closetGuiFUEShowCurrentStepSched", %sched);
        return;
    }
    0.setVisible($gClosetGuiFueCurrentStep @ "inactive", %this.stepContainers);
    !(%this.hideTipsCtrl.getValue()).setVisible($gClosetGuiFueCurrentStep @ "active", %this.stepContainers);
    if (!(%this.arrivedAtFinalTip)) {
        %this.arrivedAtFinalTip = strlwr("Snapshot") @ ($gClosetGuiFueCurrentStep == %this.stepNumbersByName);
    }
};
function ClosetGuiFUE::hideCurrentStep(%this) {
    if (($gClosetGuiFueCurrentStep == -(1.0))) {
        return;
    }
    0.setVisible($gClosetGuiFueCurrentStep @ "active", %this.stepContainers);
    !(%this.hideTipsCtrl.getValue()).setVisible($gClosetGuiFueCurrentStep @ "inactive", %this.stepContainers);
};
function ClosetGuiFUE::refresh(%this) {
    if (%this.refreshingOrInitializing) {
        %sched = gGetField(%this, "closetGuiFUERefreshSched");
        cancel(%sched);
        %sched = 250.schedule(%this);
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
        %this.hideTipsCtrl.add(%this);
    }
    %this.hideTipsCtrl = closetGuiFUEHideTipsCtrl;
    %this.hideTipsCtrl.add(%this);
    %buttonPosition = "Body".getTabWithName(ClosetTabs).button.getPosition();
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
        position = (getWord(%buttonPosition, 0) - 10.0) @ " " @ (getWord(%buttonPosition, 1) - 10.0);
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
        position = (getWord(%buttonPosition, 0) - 10.0) @ " " @ (getWord(%buttonPosition, 1) - 10.0);
        extent = "123 82";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        bitmap = "platform/client/ui/closetGuiFUE_step3_inactive";
    };
    "Body".addStepWithName(%this, %newActiveStep, %newInactiveStep);
    %buttonPosition = "Closet".getTabWithName(ClosetTabs).button.getPosition();
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
        position = (getWord(%buttonPosition, 0) - 10.0) @ " " @ (getWord(%buttonPosition, 1) - 10.0);
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
        position = (getWord(%buttonPosition, 0) - 10.0) @ " " @ (getWord(%buttonPosition, 1) - 10.0);
        extent = "123 82";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        bitmap = "platform/client/ui/closetGuiFUE_step2_inactive";
    };
    "Closet".addStepWithName(%this, %newActiveStep, %newInactiveStep);
    if (!(tabShopsInitialized)) {
        ClosetTabs.fillStoreTab();
    }
    %button = "Shops".getTabWithName(ClosetTabs).button;
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
        position = (getWord(%buttonPosition, 0) - 10.0) @ " " @ (getWord(%buttonPosition, 1) - 10.0);
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
        %this.shopsDirBitmap.add(%this);
    }
    %this.shopsDirBitmap = closetGuiFUEShopsDirBitmap;
    %this.shopsDirBitmap.add(%this);
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
    %credsBitmapCtrl.add(%newActiveStep);
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
        position = (getWord(%buttonPosition, 0) - 10.0) @ " " @ (getWord(%buttonPosition, 1) - 10.0);
        extent = "123 82";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        bitmap = "platform/client/ui/closetGuiFUE_step1_inactive";
    };
    "Shops".addStepWithName(%this, %newActiveStep, %newInactiveStep);
    %buttonPosition = "Snapshot".getTabWithName(ClosetTabs).button.getPosition();
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
        position = (getWord(%buttonPosition, 0) - 10.0) @ " " @ (getWord(%buttonPosition, 1) - 10.0);
        extent = "123 82";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        bitmap = "platform/client/ui/closetGuiFUE_step4_active";
    }; @ "ETSNonModalProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = (getWord(ClosetTabContainer.getPosition(), 0) + 759.0) @ " " @ (getWord(ClosetTabContainer.getPosition(), 0) + 492.0);
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
        position = (getWord(%buttonPosition, 0) - 10.0) @ " " @ (getWord(%buttonPosition, 1) - 10.0);
        extent = "123 82";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        bitmap = "platform/client/ui/closetGuiFUE_step4_inactive";
    }; @ "ETSNonModalProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = (getWord(ClosetTabContainer.getPosition(), 0) + 759.0) @ " " @ (getWord(ClosetTabContainer.getPosition(), 0) + 492.0);
        extent = "123 82";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        bitmap = "platform/client/ui/closetGuiFUE_step5_inactive";
    };
    "Snapshot".addStepWithName(%this, %newActiveStep, %newInactiveStep);
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
        %sched = 250.schedule(%this);
        showOrHideTips;
        gSetField(%this, "closetGuiFUEHideTipsCtrlShowOrHideTipsSched", %sched);
        return;
    }
    %i = 0;
    while ((%i < $gClosetGuiFueStepCount)) {
        0.setVisible(%i @ "active" @ ClosetGuiFUE, %this.stepContainers);
        if (isObject(%i @ "inactive" @ ClosetGuiFUE, %this.stepContainers)) {
            0.setVisible(%i @ "inactive" @ ClosetGuiFUE, %this.stepContainers);
        }
        %i = (%i + 1.0);
    }
};
function closetGuiFUEHideTipsCtrl::showTips(%this) {
    if (%this.refreshingOrInitializing) {
        %sched = gGetField(%this, "closetGuiFUEHideTipsCtrlShowOrHideTipsSched");
        ClosetGuiFUE;
        cancel(%sched);
        %sched = 250.schedule(%this);
        showOrHideTips;
        gSetField(%this, "closetGuiFUEHideTipsCtrlShowOrHideTipsSched", %sched);
        return;
    }
    %i = 0;
    while ((%i < $gClosetGuiFueStepCount)) {
        (%i == $gClosetGuiFueCurrentStep).setVisible(%i @ "active" @ ClosetGuiFUE, %this.stepContainers);
        if (isObject(%i @ "inactive" @ ClosetGuiFUE, %this.stepContainers)) {
            (%i != $gClosetGuiFueCurrentStep).setVisible(%i @ "inactive" @ ClosetGuiFUE, %this.stepContainers);
        }
        %i = (%i + 1.0);
    }
};
