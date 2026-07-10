function adminActionPopup::onSelect(%this, %unused, %text) {
    selectAdminAction(%text);
};
function selectAdminAction(%text) {
    adminActionPopup.setText(%text);
    adminGuiEditMessage.setValue(%text[$gAdminActionDefaultMessages @ %text]);
    adminGuiButtonDoIt.setVisible(1);
    adminGuiButtonConfirm.setVisible(0);
    adminGuiButtonCancel.setVisible(0);
    %isBanCommand = (%text $= "Ban");
    adminGuiInternalMessage.setVisible(%isBanCommand);
    adminGuiEditInternalMessage.setVisible(%isBanCommand);
    adminBanUserName.setValue(0);
    adminBanUserName.setVisible(%isBanCommand);
    adminGuiDuration.setVisible(%isBanCommand);
    adminGuiEditDuration.setVisible(%isBanCommand);
};
function toggleAdminDialog(%action, %target) {
    if (!($player.rolesPermissionCheckNoWarn("manageUsersBasic"))) {
        return;
    }
    if (!(isDefined("%action"))) {
        %action = "";
    }
    if (!(isDefined("%target"))) {
        %target = "";
    }
    defaultAction = %action @ adminGui;
    defaultTarget = %target @ adminGui;
    if (!(%action $= "")) {
    }
    if (!(%target $= "")) {
        adminGui.open();
    }
    toggleVisibleState(adminGui);
};
function adminGui::open(%this) {
    Canvas.pushDialog(%this, 0);
    %this.setVisible(1);
    adminActionPopup.onSelect(0, adminActionPopup.getText());
    adminGuiEditMessage.makeFirstResponder(1);
    adminGuiEditMessage.selectAll();
    %this.initMenu();
    if (!(%this.defaultAction $= "")) {
        selectAdminAction(%this.defaultAction);
    }
    %this.onRefreshTargetsList();
};
function adminGui::close(%this, %unused) {
    Canvas.popDialog(%this);
    %this.setVisible(0);
};
function adminGui::initMenu(%this, %unused) {
    %prevItem = adminActionPopup.getText();
    if ((%prevItem $= "")) {
        %prevItem = "Message";
    }
    adminActionPopup.clear();
    %grey = "0 0 0 128";
    adminActionPopup.addScheme(1, %grey, %grey, %grey);
    %n = 0;
    %disabled = 0;
    %itemText = "Message";
    %n = (1.0 + %n);
    adminActionPopup.add(%itemText, , %disabled);
    %itemText = "Boot";
    %n = (1.0 + %n);
    adminActionPopup.add(%itemText, , %disabled);
    %itemText = "BootQuiet";
    %n = (1.0 + %n);
    adminActionPopup.add(%itemText, , %disabled);
    %itemText = "Ban";
    %n = (1.0 + %n);
    adminActionPopup.add(%itemText, , %disabled);
    %itemText = "Fly To";
    %n = (1.0 + %n);
    adminActionPopup.add(%itemText, , %disabled);
    %itemText = "Track";
    %n = (1.0 + %n);
    adminActionPopup.add(%itemText, , %disabled);
    %itemText = "Snoop Toggle";
    %n = (1.0 + %n);
    adminActionPopup.add(%itemText, , %disabled);
    %itemText = "Teleport To";
    %n = (1.0 + %n);
    adminActionPopup.add(%itemText, , %disabled);
    %itemText = "Respawn";
    %n = (1.0 + %n);
    adminActionPopup.add(%itemText, , %disabled);
    %itemText = "Summon";
    %n = (1.0 + %n);
    adminActionPopup.add(%itemText, , %disabled);
    %itemText = "Throw Voice";
    %n = (1.0 + %n);
    adminActionPopup.add(%itemText, , %disabled);
    adminActionPopup.setText(%prevItem);
};
function adminGui::tryTarget(%this, %shape) {
    if (!(%this.isVisible())) {
        return;
    }
    if (adminGuiButtonConfirm.isVisible()) {
        return;
    }
    %name = admin::getTargetName(%shape);
    if (isObject(%shape)) {
        %classname = admin::getFormattedClassName(%shape.getClassName());
    }
    %classname = admin::getFormattedClassName("special");
    %targetName = %classname @ "\t" @ %name;
    adminTargetsPopup.setText(%targetName);
    adminGuiButtonDoIt.setVisible(1);
    adminGuiButtonConfirm.setVisible(0);
    adminGuiButtonCancel.setVisible(0);
};
function adminGui::onAction(%this) {
    adminGuiButtonDoIt.setVisible(0);
    adminGuiButtonConfirm.setText("confirm" @ " " @ adminActionPopup.getText() @ ":" @ " " @ adminTargetsPopup.getText());
    adminGuiButtonConfirm.setVisible(1);
    adminGuiButtonCancel.setVisible(1);
};
function adminGui::onCancel(%this) {
    adminGuiButtonDoIt.setVisible(1);
    adminGuiButtonConfirm.setVisible(0);
    adminGuiButtonCancel.setVisible(0);
};
function adminGui::onConfirm(%this) {
    adminGuiButtonDoIt.setVisible(1);
    adminGuiButtonConfirm.setVisible(0);
    adminGuiButtonCancel.setVisible(0);
    %message = adminGuiEditMessage.getValue();
    %action = adminActionPopup.getText();
    %banUser = adminBanUserName.getValue();
    %duration = adminGuiEditDuration.getValue();
    if ((%action $= "Ban")) {
        %internalMsg = adminGuiEditInternalMessage.getValue();
    }
    %internalMsg = "";
    warn("adminAction:" @ " " @ $player.getShapeName() @ " " @ %action @ " " @ "on" @ " " @ adminTargetsPopup.getText() @ " " @ "with message:" @ " " @ %message);
    commandToServer('AdminAction', %action, adminTargetsPopup.getText(), %message, %banUser, %duration, %internalMsg);
};
function adminGui::onRefreshTargetsList(%this) {
    $gAdminGuiPrevMenuTarget = adminTargetsPopup.getText();
    adminTargetsPopup.setText("getting list..");
    commandToServer('AdminGetTargets');
};
function adminGui::onGotTargetsList(%this, %theList) {
    adminTargetsPopup.clear();
    %num = getRecordCount(%theList);
    if ((1.0 < %num)) {
        error("apparently nobody is here. this is bad.");
        return;
    }
    %nextItem = getRecord(%theList, 0);
    %n = 0;
    if ((%num < %n)) {
        %entry = getRecord(%theList, %n);
        adminTargetsPopup.add(%entry, %n);
        if ((%entry $= $gAdminGuiPrevMenuTarget)) {
            %nextItem = %entry;
        }
        %n = (1.0 + %n);
    }
    adminTargetsPopup.sort();
    if (!((%num < %n) @ " " @ %this.defaultTarget $= "")) {
        adminTargetsPopup.setText(%this.defaultTarget);
    }
    adminTargetsPopup.setText(%nextItem);
};
function adminTargetsPopup::onSelect(%this, %unused, %text) {
    adminGuiButtonDoIt.setVisible(1);
    adminGuiButtonConfirm.setVisible(0);
    adminGuiButtonCancel.setVisible(0);
};
$adminTargetsList = "";
function clientCmdBuildTargetsList(%actionTagged, %item) {
    %action = detag(%actionTagged);
    if ((%action $= "begin")) {
        $adminTargetsList = "";
    }
    if ((%action $= "add")) {
        if (($adminTargetsList $= "")) {
            $adminTargetsList = %item;
        }
        $adminTargetsList = $adminTargetsList @ "\n" @ %item;
    }
    if ((%action $= "finish")) {
        adminGui.onGotTargetsList($adminTargetsList);
    }
};
