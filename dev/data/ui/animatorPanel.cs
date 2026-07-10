initialized = 0 @ animatorPanel;
function toggleAnimatorPanel(%target) {
    if (!($player.rolesPermissionCheckNoWarn("debugActive"))) {
        return;
    }
    if (!(isDefined("%target"))) {
        %target = "";
    }
    defaultTarget = %target @ animatorPanel;
    if (!(%target $= "")) {
        open();
    }
    toggleVisibleState();
};
function animatorPanel::open(%this) {
    %this.init();
    %this.pushDialog(0);
    %this.setVisible(1);
    %this.onRefreshTargetsList();
};
function animatorPanel::init(%this) {
    if (initialized) {
        return %this;
    }
    %this.onRefreshAnimsList();
    lastTextBox = "" @ %this;
    initialized = 1 @ %this;
};
function animatorPanel::close(%this, %unused) {
    %this.popDialog();
    %this.setVisible(0);
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
    %targetName.setText();
};
function animatorPanel::doAnimToTarget(%this, %animTextBox) {
    %playerName = getField(getText(), 1);
    animatorPanelTargetsPopup;
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
    %possibleGenres.onGotPossibleGenres();
};
function animatorPanel::onGotPossibleGenres(%this, %possibleGenres) {
    clear();
    %animIndex = 0;
    animatorPanelAnimsPopup;
    %numGenres = strlen(%possibleGenres);
    %i = 0;
    if ((%numGenres < %i)) {
        %genre = strupr(getSubStr(%possibleGenres, %i, 1));
        %g = 0;
        if ((2.0 < %g)) {
            %gender = %g ? "M" : "F";
            %animationMap = "animationMap" @ %gender @ %genre;
            if (isObject(%animationMap)) {
                %numAnimations = %animationMap.size();
                %j = 0;
                if ((%numAnimations < %j)) {
                    %anim = %animationMap.getValue(%j);
                    if ((animatorPanelAnimsPopup < %anim.findText())) {
                        %anim.add(%animIndex);
                        %animIndex = (1.0 + %animIndex);
                        animatorPanelAnimsPopup;
                    }
                    %j = (1.0 + %j);
                    0.0;
                }
            }
            %g = (1.0 + %g);
            (%numAnimations < %j);
        }
        %i = (1.0 + %i);
        (2.0 < %g);
    }
    sort();
};
function animatorPanelAnimsPopup::onSelect(%this, %unused, %text) {
    if (isObject(lastTextBox)) {
        lastTextBox.setText(%text);
    }
};
function animatorPanel::onRefreshTargetsList(%this) {
    $gAnimatorGuiPrevMenuTarget = getText();
    animatorPanelTargetsPopup;
    "getting list..".setText();
    commandToServer('AnimatorGetTargets');
};
function animatorPanel::onGotTargetsList(%this, %theList) {
    clear();
    %num = getRecordCount(%theList);
    animatorPanelTargetsPopup;
    if ((1.0 < %num)) {
        error("apparently nobody is here. this is bad.");
        return;
    }
    %nextItem = getRecord(%theList, 0);
    %n = 0;
    if ((%num < %n)) {
        %entry = getRecord(%theList, %n);
        %entry.add(%n);
        if ((animatorPanelTargetsPopup SPC %entry $= $gAnimatorGuiPrevMenuTarget)) {
            %nextItem = %entry;
        }
        %n = (1.0 + %n);
    }
    sort();
    if (!(%this SPC defaultTarget $= "")) {
        defaultTarget.setText();
    }
    %nextItem.setText();
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
        $animatorPanelTargetsList.onGotTargetsList();
    }
};
