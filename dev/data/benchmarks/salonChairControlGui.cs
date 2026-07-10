function toggleSalonChairControlDialog() {
    if (!($StandAlone)) {
        return;
    }
    if (!($gContiguousSpaceName $= "minimal")) {
        return;
    }
    if (!("manageUsersBasic".rolesPermissionCheckNoWarn($player))) {
        return;
    }
    toggleVisibleState(salonChairControlGui);
};
function salonChairControlGui::open(%this) {
    0.pushDialog(Canvas, %this);
    1.setVisible(%this);
    %this.onRefreshTargetsList();
};
function salonChairControlGui::close(%this, %unused) {
    %this.popDialog(Canvas);
    0.setVisible(%this);
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
    %targetName.setText(salonChairControlTargetsPopup);
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
    "getting list..".setText(salonChairControlTargetsPopup);
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
        $gSalonChairControlTargetsList.onGotTargetsList(salonChairControlGui);
    }
};
function salonChairControlGui::onGotTargetsList(%this, %theList) {
    salonChairControlTargetsPopup.clear();
    %num = getRecordCount(%theList);
    if ((%num < 1.0)) {
        error("apparently nobody is here. this is bad.");
        return;
    }
    %nextItem = getRecord(%theList, 0);
    %n = 0;
    while ((%n < %num)) {
        %entry = getRecord(%theList, %n);
        %n.add(salonChairControlTargetsPopup, %entry);
        if ((%entry $= $gSalonChairControlGuiPrevMenuTarget)) {
            %nextItem = %entry;
        }
        %n = (%n + 1.0);
    }
    salonChairControlTargetsPopup.sort();
    if (!((%n < %num) @ " " @ %this.defaultTarget $= "")) {
        %this.defaultTarget.setText(salonChairControlTargetsPopup);
    }
    %nextItem.setText(salonChairControlTargetsPopup);
};
