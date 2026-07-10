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
    Canvas.pushDialog(%this, 0);
    %this.setVisible(1);
    %this.onRefreshTargetsList();
};
function salonChairControlGui::close(%this, %unused) {
    Canvas.popDialog(%this);
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
    salonChairControlTargetsPopup.setText(%targetName);
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
    salonChairControlTargetsPopup.setText("getting list..");
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
        salonChairControlGui.onGotTargetsList($gSalonChairControlTargetsList);
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
        salonChairControlTargetsPopup.add(%entry, %n);
        if ((%entry $= $gSalonChairControlGuiPrevMenuTarget)) {
            %nextItem = %entry;
        }
        %n = (1.0 + %n);
    }
    salonChairControlTargetsPopup.sort();
    if (!((%num < %n) @ " " @ %this.defaultTarget $= "")) {
        salonChairControlTargetsPopup.setText(%this.defaultTarget);
    }
    salonChairControlTargetsPopup.setText(%nextItem);
};
