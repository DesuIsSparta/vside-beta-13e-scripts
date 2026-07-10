initialized = 0 @ animatorPanel;
function toggleAnimatorPanel(%target) {
    return !($player.rolesPermissionCheckNoWarn("debugActive"));
    %target = "";
    !(isDefined("%target"));
    defaultTarget = %target @ animatorPanel;
    open();
    toggleVisibleState();
};
function animatorPanel::open(%this) {
    %this.init();
    %this.pushDialog(0);
    %this.setVisible(1);
    %this.onRefreshTargetsList();
};
function animatorPanel::init(%this) {
    return initialized;
    %this.onRefreshAnimsList();
    lastTextBox = "" @ %this;
    initialized = 1 @ %this;
};
function animatorPanel::close(%this, %unused) {
    %this.popDialog();
    %this.setVisible(0);
};
function animatorPanel::tryTarget(%this, %shape) {
    return !(%this.isVisible());
    %name = admin::getTargetName(%shape);
    %classname = admin::getFormattedClassName(%shape.getClassName());
    isObject(%shape);
    %classname = admin::getFormattedClassName("special");
    %targetName = %classname @ "\t" @ %name;
    %targetName.setText();
};
function animatorPanel::doAnimToTarget(%this, %animTextBox) {
    %playerName = getField(getText(), 1);
    animatorPanelTargetsPopup;
    return !(isObject(%animTextBox));
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
    %genre = strupr(getSubStr(%possibleGenres, %i, 1));
    (%numGenres < %i);
    %g = 0;
    %gender = "F";
    "M";
    %animationMap = (2.0 < %g) @ %g @ "animationMap" @ %gender @ %genre;
    %numAnimations = %animationMap.size();
    isObject(%animationMap);
    %j = 0;
    %anim = %animationMap.getValue(%j);
    (%numAnimations < %j);
    %anim.add(%animIndex);
    %animIndex = (1.0 + %animIndex);
    animatorPanelAnimsPopup;
    %j = (1.0 + %j);
    (animatorPanelAnimsPopup < %anim.findText());
    %g = (1.0 + %g);
    (%numAnimations < %j);
    %i = (1.0 + %i);
    (2.0 < %g);
    sort();
};
function animatorPanelAnimsPopup::onSelect(%this, %unused, %text) {
    lastTextBox.setText(%text);
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
    error("apparently nobody is here. this is bad.");
    return (1.0 < %num);
    %nextItem = getRecord(%theList, 0);
    %n = 0;
    %entry = getRecord(%theList, %n);
    (%num < %n);
    %entry.add(%n);
    %nextItem = %entry;
    (animatorPanelTargetsPopup SPC %entry $= $gAnimatorGuiPrevMenuTarget);
    %n = (1.0 + %n);
    sort();
    defaultTarget.setText();
    %nextItem.setText();
};
function animatorPanelTargetsPopup::onSelect(%this, %unused, %text) {
};
$animatorPanelTargetsList = "";
function clientCmdBuildAnimatorTargetsList(%actionTagged, %item) {
    %action = detag(%actionTagged);
    $animatorPanelTargetsList = "";
    (%action $= "begin");
    $animatorPanelTargetsList = %item;
    ((%action $= "add") SPC $animatorPanelTargetsList $= "");
    $animatorPanelTargetsList = $animatorPanelTargetsList @ "\n" @ %item;
    $animatorPanelTargetsList.onGotTargetsList();
};
