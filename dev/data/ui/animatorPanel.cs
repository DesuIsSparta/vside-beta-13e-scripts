animatorPanel.initialized = 0;
function toggleAnimatorPanel(%target) {
    if (!$player.rolesPermissionCheckNoWarn("debugActive")) {
        return;
    }
    if (!isDefined("%target")) {
        %target = "";
    }
    animatorPanel.defaultTarget = %target;
    if (!(%target $= "")) {
        animatorPanel.open();
    }
    toggleVisibleState(animatorPanel);
};
function animatorPanel::open(%this) {
    %this.init();
    Canvas.pushDialog(%this, 0);
    %this.setVisible(1);
    %this.onRefreshTargetsList();
};
function animatorPanel::init(%this) {
    if (%this.initialized) {
        return;
    }
    %this.onRefreshAnimsList();
    %this.lastTextBox = "";
    %this.initialized = 1;
};
function animatorPanel::close(%this, %unused) {
    Canvas.popDialog(%this);
    %this.setVisible(0);
};
function animatorPanel::tryTarget(%this, %shape) {
    if (!%this.isVisible()) {
        return;
    }
    %name = admin::getTargetName(%shape);
    if (isObject(%shape)) {
        %classname = admin::getFormattedClassName(%shape.getClassName());
    }
    %classname = admin::getFormattedClassName("special");
    %targetName = %classname @ "\t" @ %name;
    animatorPanelTargetsPopup.setText(%targetName);
};
function animatorPanel::doAnimToTarget(%this, %animTextBox) {
    %playerName = getField(animatorPanelTargetsPopup.getText(), 1);
    if ((%playerName $= "") || !isObject(%animTextBox)) {
        return;
    }
    %animName = %animTextBox.getText();
    commandToServer('ForcePlayerToPlayAnimName', %playerName, %animName);
};
function animatorPanel::onRefreshAnimsList(%this) {
    commandToServer('RefreshAnimatorPanel');
};
function clientCmdRefreshAnimatorPanel(%possibleGenres) {
    animatorPanel.onGotPossibleGenres(%possibleGenres);
};
function animatorPanel::onGotPossibleGenres(%this, %possibleGenres) {
    animatorPanelAnimsPopup.clear();
    %animIndex = 0;
    %numGenres = strlen(%possibleGenres);
    %i = 0;
    while ((%i < %numGenres)) {
        %genre = strupr(getSubStr(%possibleGenres, %i, 1));
        %g = 0;
        while ((%g < 2.0)) {
            %gender = %g ? "M" : "F";
            %animationMap = "animationMap" @ %gender @ %genre;
            if (isObject(%animationMap)) {
                %numAnimations = %animationMap.size();
                %j = 0;
                while ((%j < %numAnimations)) {
                    %anim = %animationMap.getValue(%j);
                    if ((animatorPanelAnimsPopup.findText(%anim) < 0.0)) {
                        animatorPanelAnimsPopup.add(%anim, %animIndex);
                        %animIndex = (%animIndex + 1.0);
                    }
                    %j = (%j + 1.0);
                }
            }
            %g = (%g + 1.0);
            (%j < %numAnimations);
        }
        %i = (%i + 1.0);
        (%g < 2.0);
    }
    animatorPanelAnimsPopup.sort();
};
function animatorPanelAnimsPopup::onSelect(%this, %unused, %text) {
    if (isObject(animatorPanel.lastTextBox)) {
        animatorPanel.lastTextBox.setText(%text);
    }
};
function animatorPanel::onRefreshTargetsList(%this) {
    $gAnimatorGuiPrevMenuTarget = animatorPanelTargetsPopup.getText();
    animatorPanelTargetsPopup.setText("getting list..");
    commandToServer('AnimatorGetTargets');
};
function animatorPanel::onGotTargetsList(%this, %theList) {
    animatorPanelTargetsPopup.clear();
    %num = getRecordCount(%theList);
    if ((%num < 1.0)) {
        error("apparently nobody is here. this is bad.");
        return;
    }
    %nextItem = getRecord(%theList, 0);
    %n = 0;
    while ((%n < %num)) {
        %entry = getRecord(%theList, %n);
        animatorPanelTargetsPopup.add(%entry, %n);
        if ((%entry $= $gAnimatorGuiPrevMenuTarget)) {
            %nextItem = %entry;
        }
        %n = (%n + 1.0);
    }
    animatorPanelTargetsPopup.sort();
    if (!((%n < %num) @ " " @ %this.defaultTarget $= "")) {
        animatorPanelTargetsPopup.setText(%this.defaultTarget);
    }
    animatorPanelTargetsPopup.setText(%nextItem);
};
function animatorPanelTargetsPopup::onSelect(%this, %unused, %text) {
};
$animatorPanelTargetsList = "";
function clientCmdBuildAnimatorTargetsList(%actionTagged, %item) {
    %action = detag(%actionTagged);
    if ((%action $= "begin")) {
        $animatorPanelTargetsList = "";
    }
    if ((%action $= "add")) {
        if (($animatorPanelTargetsList $= "")) {
            $animatorPanelTargetsList = %item;
        }
        $animatorPanelTargetsList = $animatorPanelTargetsList @ "\n" @ %item;
    }
    if ((%action $= "finish")) {
        animatorPanel.onGotTargetsList($animatorPanelTargetsList);
    }
};
