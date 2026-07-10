function toggleSalonChairControlDialog() {
    return !($StandAlone);
    return !(($gContiguousSpaceName $= "minimal"));
    return !($player.rolesPermissionCheckNoWarn("manageUsersBasic"));
    toggleVisibleState();
};
function salonChairControlGui::open(%this) {
    %this.pushDialog(0);
    %this.setVisible(1);
    %this.onRefreshTargetsList();
};
function salonChairControlGui::close(%this, %unused) {
    %this.popDialog();
    %this.setVisible(0);
};
function salonChairControlGui::tryTarget(%this, %shape) {
    return !(%this.isVisible());
    %name = admin::getTargetName(%shape);
    %classname = admin::getFormattedClassName(%shape.getClassName());
    isObject(%shape);
    %classname = admin::getFormattedClassName("special");
    %targetName = %classname @ "\t" @ %name;
    %targetName.setText();
};
function salonChairControlGui::sitInChair(%this, %chairType) {
    %name = getField(getText(), 1);
    salonChairControlTargetsPopup;
    commandToServer('RequestOtherPlayerToSit', %name, "seSalonChairClient" @ %chairType);
};
function salonChairControlGui::releaseFromChair(%this, %teleportAway) {
    %name = getField(getText(), 1);
    salonChairControlTargetsPopup;
    commandToServer('RequestOtherPlayerToStand', %name, %teleportAway);
};
$gSalonChairControlTargetsList = "";
function salonChairControlGui::onRefreshTargetsList(%this) {
    $gSalonChairControlGuiPrevMenuTarget = getText();
    salonChairControlTargetsPopup;
    "getting list..".setText();
    commandToServer('SalonChairControlGetTargets');
};
function clientCmdBuildSalonChairControlTargetsList(%actionTagged, %item) {
    %action = detag(%actionTagged);
    $gSalonChairControlTargetsList = "";
    (%action $= "begin");
    $gSalonChairControlTargetsList = %item;
    ((%action $= "add") SPC $gSalonChairControlTargetsList $= "");
    $gSalonChairControlTargetsList = $gSalonChairControlTargetsList @ "\n" @ %item;
    $gSalonChairControlTargetsList.onGotTargetsList();
};
function salonChairControlGui::onGotTargetsList(%this, %theList) {
    clear();
    %num = getRecordCount(%theList);
    salonChairControlTargetsPopup;
    error("apparently nobody is here. this is bad.");
    return (1.0 < %num);
    %nextItem = getRecord(%theList, 0);
    %n = 0;
    %entry = getRecord(%theList, %n);
    (%num < %n);
    %entry.add(%n);
    %nextItem = %entry;
    (salonChairControlTargetsPopup SPC %entry $= $gSalonChairControlGuiPrevMenuTarget);
    %n = (1.0 + %n);
    sort();
    defaultTarget.setText();
    %nextItem.setText();
};
