function toggleSalonChairControlDialog() {
    if (!($StandAlone)) {
        return;
    }
    if (!($gContiguousSpaceName $= "minimal")) {
        return;
    }
    if (!($player.rolesPermissionCheckNoWarn("manageUsersBasic"))) {
        return;
    }
    toggleVisibleState(salonChairControlGui);
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
function salonChairControlGui::sitInChair(%this, %chairType) {
    %name = getField(salonChairControlTargetsPopup.getText(), 1);
    commandToServer('RequestOtherPlayerToSit', %name, "seSalonChairClient" @ %chairType);
};
function salonChairControlGui::releaseFromChair(%this, %teleportAway) {
    %name = getField(salonChairControlTargetsPopup.getText(), 1);
    commandToServer('RequestOtherPlayerToStand', %name, %teleportAway);
};
$gSalonChairControlTargetsList = "";
function salonChairControlGui::onRefreshTargetsList(%this) {
    $gSalonChairControlGuiPrevMenuTarget = salonChairControlTargetsPopup.getText();
    "getting list..".setText();
    commandToServer('SalonChairControlGetTargets');
};
function clientCmdBuildSalonChairControlTargetsList(%actionTagged, %item) {
    %action = detag(%actionTagged);
    if ((%action $= "begin")) {
        $gSalonChairControlTargetsList = "";
    }
    if ((%action $= "add")) {
        if (($gSalonChairControlTargetsList $= "")) {
            $gSalonChairControlTargetsList = %item;
        }
        $gSalonChairControlTargetsList = $gSalonChairControlTargetsList @ "\n" @ %item;
    }
    if ((%action $= "finish")) {
        $gSalonChairControlTargetsList.onGotTargetsList();
    }
};
function salonChairControlGui::onGotTargetsList(%this, %theList) {
    salonChairControlTargetsPopup.clear();
    %num = getRecordCount(%theList);
    if ((1.0 < %num)) {
        error("apparently nobody is here. this is bad.");
        return;
    }
    %nextItem = getRecord(%theList, 0);
    %n = 0;
    if ((%num < %n)) {
        %entry = getRecord(%theList, %n);
        %entry.add(%n);
        if ((salonChairControlTargetsPopup @ " " @ %entry $= $gSalonChairControlGuiPrevMenuTarget)) {
            %nextItem = %entry;
        }
        %n = (1.0 + %n);
    }
    salonChairControlTargetsPopup.sort();
    if (!((%num < %n) @ " " @ %this.defaultTarget $= "")) {
        %this.defaultTarget.setText();
    }
    %nextItem.setText();
};
