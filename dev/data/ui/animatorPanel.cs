initialized = 0 @ animatorPanel;
function toggleAnimatorPanel(%target) {
    if (!("debugActive".rolesPermissionCheckNoWarn($player))) {
        return;
    }
    if (!(isDefined("%target"))) {
        %target = "";
    }
    defaultTarget = %target @ animatorPanel;
    if (!(%target $= "")) {
        animatorPanel.open();
    }
    toggleVisibleState(animatorPanel);
};
function animatorPanel::open(%this) {
    %this.init();
    0.pushDialog(Canvas, %this);
    1.setVisible(%this);
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
    %this.popDialog(Canvas);
    0.setVisible(%this);
};
function animatorPanel::tryTarget(%this, %shape) {
    if (!(%this.isVisible())) {
        return;
    }
    %name = admin::getTargetName(%shape);
    if (isObject(%shape)) {
        %classname = admin::getFormattedClassName(%shape.getClassName());
    }
    %classname = admin::getFormattedClassName("special");
    %targetName = %classname @ "\t" @ %name;
    %targetName.setText(animatorPanelTargetsPopup);
};
function animatorPanel::doAnimToTarget(%this, %animTextBox) {
    %playerName = getField(animatorPanelTargetsPopup.getText(), 1);
    if ((%playerName $= "")) {
    }
    if (!(isObject(%animTextBox))) {
        return;
    }
    %animName = %animTextBox.getText();
    commandToServer('ForcePlayerToPlayAnimName', %playerName, %animName);
};
function animatorPanel::onRefreshAnimsList(%this) {
    commandToServer('RefreshAnimatorPanel');
};
function clientCmdRefreshAnimatorPanel(%possibleGenres) {
    %possibleGenres.onGotPossibleGenres(animatorPanel);
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
                    %anim = %j.getValue(%animationMap);
                    if ((%anim.findText(animatorPanelAnimsPopup) < 0.0)) {
                        %animIndex.add(animatorPanelAnimsPopup, %anim);
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
    if (isObject(animatorPanel, %this.lastTextBox)) {
        %text.setText(animatorPanel, %this.lastTextBox);
    }
};
function animatorPanel::onRefreshTargetsList(%this) {
    $gAnimatorGuiPrevMenuTarget = animatorPanelTargetsPopup.getText();
    "getting list..".setText(animatorPanelTargetsPopup);
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
        %n.add(animatorPanelTargetsPopup, %entry);
        if ((%entry $= $gAnimatorGuiPrevMenuTarget)) {
            %nextItem = %entry;
        }
        %n = (%n + 1.0);
    }
    animatorPanelTargetsPopup.sort();
    if (!((%n < %num) @ " " @ %this.defaultTarget $= "")) {
        %this.defaultTarget.setText(animatorPanelTargetsPopup);
    }
    %nextItem.setText(animatorPanelTargetsPopup);
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
        $animatorPanelTargetsList.onGotTargetsList(animatorPanel);
    }
};
