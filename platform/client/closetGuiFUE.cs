$gClosetGuiFueStepCount = 0;
$gClosetGuiFueCurrentStep = -(1.0);
function ClosetGuiFUE::open(%this) {
    if (refreshingOrInitializing) {
        %sched = gGetField(%this, "closetGuiFUEOpenSched");
        %this;
        cancel(%sched);
        %sched = %this.schedule(250);
        open;
        gSetField(%this, "closetGuiFUEOpenSched", %sched);
        return;
    }
    if (!(initialized)) {
        arrivedAtFinalTip = %this @ 0 @ %this;
        %this.Initialize();
    }
    %this.reposition();
    %this.showAllAsInactive();
    %this.goToStepByName(name);
};
function ClosetGuiFUE::close(%this) {
    %this.hide();
};
function ClosetGuiFUE::show(%this) {
    if (refreshingOrInitializing) {
        %sched = gGetField(%this, "closetGuiFUEShowSched");
        %this;
        cancel(%sched);
        %sched = %this.schedule(250);
        show;
        gSetField(%this, "closetGuiFUEShowSched", %sched);
        return;
    }
    %this.setVisible(1);
    %this.goToStepByName(name);
};
function ClosetGuiFUE::hide(%this) {
    %this.setVisible(0);
};
function ClosetGuiFUE::reposition(%this) {
    position = (2.0 + (960.0 / (ClosetGuiPositioner - getWord(extent, 0)))) @ " " @ 1.0 @ (2.0 + (32.0 / (576.0 - (ClosetGuiPositioner - getWord(extent, 1))))) @ %this;
    1.0;
};
function ClosetGuiFUE::showAllAsInactive(%this) {
    if (refreshingOrInitializing) {
        %sched = gGetField(%this, "closetGuiFUEShowAllAsInactiveSched");
        %this;
        cancel(%sched);
        %sched = %this.schedule(250);
        showAllAsInactive;
        gSetField(%this, "closetGuiFUEShowAllAsInactiveSched", %sched);
        return;
    }
    %i = 0;
    if (($gClosetGuiFueStepCount < %i)) {
        stepContainers.setVisible(0);
        if (isObject(stepContainers)) {
            stepContainers.setVisible(!(hideTipsCtrl.getValue()));
        }
        %i = (1.0 + %i);
        %this;
    }
    show();
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
    stepContainers = %activeContainer TAB $gClosetGuiFueStepCount @ "active" @ %this;
    if (isObject(%inactiveContainer)) {
        %inactiveContainer.setVisible(0);
        %this.add(%inactiveContainer);
    }
    stepContainers = %inactiveContainer TAB $gClosetGuiFueStepCount @ "inactive" @ %this;
    $gClosetGuiFueStepCount = (1.0 + $gClosetGuiFueStepCount);
    return 1;
};
function ClosetGuiFUE::addStepWithName(%this, %activeContainer, %inactiveContainer, %stepName) {
    stepNumbersByName = $gClosetGuiFueStepCount @ strlwr(%stepName) @ %this;
    if (!(%this.addStep(%activeContainer, %inactiveContainer))) {
        warn(getScopeName() @ " " @ "- step" @ " " @ %stepName @ " " @ "not added -" @ " " @ getTrace());
        stepNumbersByName = -(1.0) @ strlwr(%stepName) @ %this;
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
        (closetGuiFUEShopsDirBitmap SPC $gCurrentStoreName $= "").setVisible();
        !(StoreShoppingBag SPC $gCurrentStoreName $= "").setVisible();
        !(StoreAddItemsButton SPC $gCurrentStoreName $= "").setVisible();
        0.setLeaveStoreControlsVisible();
    }
    0.setVisible();
    $gClosetGuiFueCurrentStep = stepNumbersByName;
    closetGuiFUEShopsDirBitmap @ strlwr(%stepName) @ %this;
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
    if (refreshingOrInitializing) {
        %sched = gGetField(%this, "closetGuiFUEShowCurrentStepSched");
        %this;
        cancel(%sched);
        %sched = %this.schedule(250);
        showCurrentStep;
        gSetField(%this, "closetGuiFUEShowCurrentStepSched", %sched);
        return;
    }
    stepContainers.setVisible(0);
    stepContainers.setVisible(!(hideTipsCtrl.getValue()));
    if (!(arrivedAtFinalTip)) {
        arrivedAtFinalTip = %this @ strlwr("Snapshot") @ %this @ (stepNumbersByName == $gClosetGuiFueCurrentStep) @ %this;
        %this;
    }
};
function ClosetGuiFUE::hideCurrentStep(%this) {
    if ((-(1.0) == $gClosetGuiFueCurrentStep)) {
        return;
    }
    stepContainers.setVisible(0);
    stepContainers.setVisible(!(hideTipsCtrl.getValue()));
};
function ClosetGuiFUE::refresh(%this) {
    if (refreshingOrInitializing) {
        %sched = gGetField(%this, "closetGuiFUERefreshSched");
        %this;
        cancel(%sched);
        %sched = %this.schedule(250);
        refresh;
        gSetField(%this, "closetGuiFUERefreshSched", %sched);
        return;
    }
    refreshingOrInitializing = 1 @ %this;
    deleteMembers();
    $gClosetGuiFueStepCount = 0;
    ClosetGuiFUE;
    $gClosetGuiFueCurrentStep = -(1.0);
    %this.Initialize();
    if (%visible) {
        %this.showAllAsInactive();
    }
    refreshingOrInitializing = 0 @ %this;
};
function ClosetGuiFUE::Initialize(%this) {
    refreshingOrInitializing = 1 @ %this;
    if (!(isObject())) {
        profile = closetGuiFUEHideTipsCtrl @ new GuiCheckBoxCtrl(closetGuiFUEHideTipsCtrl) @ "ETSCheckBoxProfile";
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
        hideTipsCtrl = %this;
        %this.add(hideTipsCtrl);
    }
    hideTipsCtrl = closetGuiFUEHideTipsCtrl @ %this;
    %this;
    %this.add(hideTipsCtrl);
    %buttonPosition = button.getPosition();
    "Body".getTabWithName();
    profile = GuiControl @ new ""() @ "ETSNonModalProfile";
    0;
    horizSizing = ClosetTabs @ ClosetGuiFUE @ horizSizing;
    %this;
    vertSizing = ClosetGuiFUE @ vertSizing;
    position = "0 0";
    extent = ClosetGuiFUE @ extent;
    minExtent = ClosetGuiFUE @ minExtent;
    sluggishness = ClosetGuiFUE @ sluggishness;
    visible = 0;
    profile = GuiBitmapCtrl @ new ""() @ "ETSNonModalProfile";
    horizSizing = "right";
    vertSizing = "bottom";
    position = (10.0 - getWord(%buttonPosition, 0)) @ " " @ (10.0 - getWord(%buttonPosition, 1));
    extent = "123 82";
    minExtent = "1 1";
    sluggishness = -1;
    visible = 1;
    bitmap = "platform/client/ui/closetGuiFUE_step3_active";
    profile = new GuiBitmapCtrl(closetGuiFUEWelcomeImage) @ "ETSNonModalProfile";
    horizSizing = "right";
    vertSizing = "bottom";
    position = "511 32";
    extent = "447 544";
    minExtent = "20 20";
    visible = 1;
    bitmap = "platform/client/ui/welcomeBodyPanel";
    %newActiveStep = ;
    profile = GuiControl @ new ""() @ "ETSNonModalProfile";
    0;
    horizSizing = ClosetGuiFUE @ horizSizing;
    vertSizing = ClosetGuiFUE @ vertSizing;
    position = "0 0";
    extent = ClosetGuiFUE @ extent;
    minExtent = ClosetGuiFUE @ minExtent;
    sluggishness = ClosetGuiFUE @ sluggishness;
    visible = 0;
    profile = GuiBitmapCtrl @ new ""() @ "ETSNonModalProfile";
    horizSizing = "right";
    vertSizing = "bottom";
    position = (10.0 - getWord(%buttonPosition, 0)) @ " " @ (10.0 - getWord(%buttonPosition, 1));
    extent = "123 82";
    minExtent = "1 1";
    sluggishness = -1;
    visible = 1;
    bitmap = "platform/client/ui/closetGuiFUE_step3_inactive";
    %newInactiveStep = ;
    %this.addStepWithName(%newActiveStep, %newInactiveStep, "Body");
    %buttonPosition = button.getPosition();
    "Closet".getTabWithName();
    profile = GuiControl @ new ""() @ "ETSNonModalProfile";
    0;
    horizSizing = ClosetTabs @ ClosetGuiFUE @ horizSizing;
    vertSizing = ClosetGuiFUE @ vertSizing;
    position = "0 0";
    extent = ClosetGuiFUE @ extent;
    minExtent = ClosetGuiFUE @ minExtent;
    sluggishness = ClosetGuiFUE @ sluggishness;
    visible = 0;
    profile = GuiBitmapCtrl @ new ""() @ "ETSNonModalProfile";
    horizSizing = "right";
    vertSizing = "bottom";
    position = (10.0 - getWord(%buttonPosition, 0)) @ " " @ (10.0 - getWord(%buttonPosition, 1));
    extent = "123 82";
    minExtent = "1 1";
    sluggishness = -1;
    visible = 1;
    bitmap = "platform/client/ui/closetGuiFUE_step2_active";
    %newActiveStep = new GuiBitmapCtrl(closetGuiFUEWelcomeImage_b : closetGuiFUEWelcomeImage);
    profile = GuiControl @ new ""() @ "ETSNonModalProfile";
    0;
    horizSizing = ClosetGuiFUE @ horizSizing;
    vertSizing = ClosetGuiFUE @ vertSizing;
    position = "0 0";
    extent = ClosetGuiFUE @ extent;
    minExtent = ClosetGuiFUE @ minExtent;
    sluggishness = ClosetGuiFUE @ sluggishness;
    visible = 0;
    profile = GuiBitmapCtrl @ new ""() @ "ETSNonModalProfile";
    horizSizing = "right";
    vertSizing = "bottom";
    position = (10.0 - getWord(%buttonPosition, 0)) @ " " @ (10.0 - getWord(%buttonPosition, 1));
    extent = "123 82";
    minExtent = "1 1";
    sluggishness = -1;
    visible = 1;
    bitmap = "platform/client/ui/closetGuiFUE_step2_inactive";
    %newInactiveStep = ;
    %this.addStepWithName(%newActiveStep, %newInactiveStep, "Closet");
    if (!(tabShopsInitialized)) {
        fillStoreTab();
    }
    %button = button;
    "Shops".getTabWithName();
    %buttonPosition = %button.getPosition();
    ClosetTabs;
    profile = GuiControl @ new ""() @ "ETSNonModalProfile";
    0;
    horizSizing = ClosetTabs @ ClosetGuiFUE @ horizSizing;
    ClosetTabs;
    vertSizing = ClosetGuiFUE @ vertSizing;
    position = "0 0";
    extent = ClosetGuiFUE @ extent;
    minExtent = ClosetGuiFUE @ minExtent;
    sluggishness = ClosetGuiFUE @ sluggishness;
    visible = 0;
    profile = GuiBitmapCtrl @ new ""() @ "ETSNonModalProfile";
    horizSizing = "right";
    vertSizing = "bottom";
    position = (10.0 - getWord(%buttonPosition, 0)) @ " " @ (10.0 - getWord(%buttonPosition, 1));
    extent = "123 82";
    minExtent = "1 1";
    sluggishness = -1;
    visible = 1;
    bitmap = "platform/client/ui/closetGuiFUE_step1_active";
    %newActiveStep = ;
    if (!(isObject())) {
        profile = closetGuiFUEShopsDirBitmap @ new GuiBitmapCtrl(closetGuiFUEShopsDirBitmap) @ "ETSNonModalProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "35 125";
        extent = "441 375";
        minExtent = "1 1";
        sluggishness = -1;
        visible = ($gCurrentStoreName $= "");
        bitmap = "platform/client/ui/closetGuiFUE_shop_active_shopsDir";
        shopsDirBitmap = %this;
        %this.add(shopsDirBitmap);
    }
    shopsDirBitmap = closetGuiFUEShopsDirBitmap @ %this;
    %this;
    %this.add(shopsDirBitmap);
    profile = %this @ new GuiBitmapCtrl(closetGuiFUE_vPoints_vBux_Image) @ "ETSNonModalProfile";
    horizSizing = "right";
    vertSizing = "bottom";
    position = "607 32";
    extent = "346 340";
    minExtent = "1 1";
    sluggishness = -1;
    visible = 1;
    bitmap = "platform/client/ui/closetGuiFUE_shop_active_creds";
    %credsBitmapCtrl = ;
    %newActiveStep.add(%credsBitmapCtrl);
    profile = GuiControl @ new ""() @ "ETSNonModalProfile";
    0;
    horizSizing = ClosetGuiFUE @ horizSizing;
    vertSizing = ClosetGuiFUE @ vertSizing;
    position = "0 0";
    extent = ClosetGuiFUE @ extent;
    minExtent = ClosetGuiFUE @ minExtent;
    sluggishness = ClosetGuiFUE @ sluggishness;
    visible = 0;
    profile = GuiBitmapCtrl @ new ""() @ "ETSNonModalProfile";
    horizSizing = "right";
    vertSizing = "bottom";
    position = (10.0 - getWord(%buttonPosition, 0)) @ " " @ (10.0 - getWord(%buttonPosition, 1));
    extent = "123 82";
    minExtent = "1 1";
    sluggishness = -1;
    visible = 1;
    bitmap = "platform/client/ui/closetGuiFUE_step1_inactive";
    %newInactiveStep = ;
    %this.addStepWithName(%newActiveStep, %newInactiveStep, "Shops");
    %buttonPosition = button.getPosition();
    "Snapshot".getTabWithName();
    profile = GuiControl @ new ""() @ "ETSNonModalProfile";
    0;
    horizSizing = ClosetTabs @ ClosetGuiFUE @ horizSizing;
    vertSizing = ClosetGuiFUE @ vertSizing;
    position = "0 0";
    extent = ClosetGuiFUE @ extent;
    minExtent = ClosetGuiFUE @ minExtent;
    sluggishness = ClosetGuiFUE @ sluggishness;
    visible = 0;
    profile = GuiBitmapCtrl @ new ""() @ "ETSNonModalProfile";
    horizSizing = "right";
    vertSizing = "bottom";
    position = (10.0 - getWord(%buttonPosition, 0)) @ " " @ (10.0 - getWord(%buttonPosition, 1));
    extent = "123 82";
    minExtent = "1 1";
    sluggishness = -1;
    visible = 1;
    bitmap = "platform/client/ui/closetGuiFUE_step4_active";
    profile = GuiBitmapCtrl @ new ""() @ "ETSNonModalProfile";
    horizSizing = "right";
    vertSizing = "bottom";
    position = 759.0 @ (ClosetTabContainer + getWord(getPosition(), 0)) @ " " @ 492.0 @ (ClosetTabContainer + getWord(getPosition(), 0));
    extent = "123 82";
    minExtent = "1 1";
    sluggishness = -1;
    visible = 1;
    bitmap = "platform/client/ui/closetGuiFUE_step5_active";
    %newActiveStep = ;
    profile = GuiControl @ new ""() @ "ETSNonModalProfile";
    0;
    horizSizing = ClosetGuiFUE @ horizSizing;
    vertSizing = ClosetGuiFUE @ vertSizing;
    position = "0 0";
    extent = ClosetGuiFUE @ extent;
    minExtent = ClosetGuiFUE @ minExtent;
    sluggishness = ClosetGuiFUE @ sluggishness;
    visible = 0;
    profile = GuiBitmapCtrl @ new ""() @ "ETSNonModalProfile";
    horizSizing = "right";
    vertSizing = "bottom";
    position = (10.0 - getWord(%buttonPosition, 0)) @ " " @ (10.0 - getWord(%buttonPosition, 1));
    extent = "123 82";
    minExtent = "1 1";
    sluggishness = -1;
    visible = 1;
    bitmap = "platform/client/ui/closetGuiFUE_step4_inactive";
    profile = GuiBitmapCtrl @ new ""() @ "ETSNonModalProfile";
    horizSizing = "right";
    vertSizing = "bottom";
    position = 759.0 @ (ClosetTabContainer + getWord(getPosition(), 0)) @ " " @ 492.0 @ (ClosetTabContainer + getWord(getPosition(), 0));
    extent = "123 82";
    minExtent = "1 1";
    sluggishness = -1;
    visible = 1;
    bitmap = "platform/client/ui/closetGuiFUE_step5_inactive";
    %newInactiveStep = ;
    %this.addStepWithName(%newActiveStep, %newInactiveStep, "Snapshot");
    refreshingOrInitializing = 0 @ %this;
    initialized = 1 @ %this;
};
function closetGuiFUEHideTipsCtrl::onAction(%this) {
    if (%this.getValue()) {
        %this.hideTips();
    }
    %this.showTips();
};
function closetGuiFUEHideTipsCtrl::hideTips(%this) {
    if (refreshingOrInitializing) {
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
        stepContainers.setVisible(0);
        if (isObject(stepContainers)) {
            stepContainers.setVisible(0);
        }
        %i = (1.0 + %i);
        %i @ "active" @ ClosetGuiFUE TAB %i @ "inactive" @ ClosetGuiFUE TAB %i @ "inactive" @ ClosetGuiFUE;
    }
};
function closetGuiFUEHideTipsCtrl::showTips(%this) {
    if (refreshingOrInitializing) {
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
        stepContainers.setVisible(($gClosetGuiFueCurrentStep == %i));
        if (isObject(stepContainers)) {
            stepContainers.setVisible(($gClosetGuiFueCurrentStep != %i));
        }
        %i = (1.0 + %i);
        %i @ "active" @ ClosetGuiFUE TAB %i @ "inactive" @ ClosetGuiFUE TAB %i @ "inactive" @ ClosetGuiFUE;
    }
};
