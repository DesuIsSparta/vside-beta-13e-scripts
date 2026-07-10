function adminActionPopup::onSelect(%this, %unused, %text) {
    selectAdminAction(%text);
};
function selectAdminAction(%text) {
    %text.setText();
    %text[$gAdminActionDefaultMessages @ %text].setValue();
    1.setVisible();
    0.setVisible();
    0.setVisible();
    %isBanCommand = (adminGuiButtonCancel @ " " @ %text $= "Ban");
    adminGuiButtonConfirm;
    %isBanCommand.setVisible();
    %isBanCommand.setVisible();
    0.setValue();
    %isBanCommand.setVisible();
    %isBanCommand.setVisible();
    %isBanCommand.setVisible();
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
    %this.pushDialog(0);
    %this.setVisible(1);
    0.onSelect(adminActionPopup.getText());
    1.makeFirstResponder();
    adminGuiEditMessage.selectAll();
    %this.initMenu();
    if (!(adminGuiEditMessage @ " " @ %this.defaultAction $= "")) {
        selectAdminAction(%this.defaultAction);
    }
    %this.onRefreshTargetsList();
};
function adminGui::close(%this, %unused) {
    %this.popDialog();
    %this.setVisible(0);
};
function adminGui::initMenu(%this, %unused) {
    %prevItem = adminActionPopup.getText();
    if ((%prevItem $= "")) {
        %prevItem = "Message";
    }
    adminActionPopup.clear();
    %grey = "0 0 0 128";
    1.addScheme(%grey, %grey, %grey);
    %n = 0;
    adminActionPopup;
    %disabled = 0;
    %itemText = "Message";
    %n = (1.0 + %n);
    %itemText.add(adminActionPopup, %disabled);
    %itemText = "Boot";
    %n = (1.0 + %n);
    %itemText.add(adminActionPopup, %disabled);
    %itemText = "BootQuiet";
    %n = (1.0 + %n);
    %itemText.add(adminActionPopup, %disabled);
    %itemText = "Ban";
    %n = (1.0 + %n);
    %itemText.add(adminActionPopup, %disabled);
    %itemText = "Fly To";
    %n = (1.0 + %n);
    %itemText.add(adminActionPopup, %disabled);
    %itemText = "Track";
    %n = (1.0 + %n);
    %itemText.add(adminActionPopup, %disabled);
    %itemText = "Snoop Toggle";
    %n = (1.0 + %n);
    %itemText.add(adminActionPopup, %disabled);
    %itemText = "Teleport To";
    %n = (1.0 + %n);
    %itemText.add(adminActionPopup, %disabled);
    %itemText = "Respawn";
    %n = (1.0 + %n);
    %itemText.add(adminActionPopup, %disabled);
    %itemText = "Summon";
    %n = (1.0 + %n);
    %itemText.add(adminActionPopup, %disabled);
    %itemText = "Throw Voice";
    %n = (1.0 + %n);
    %itemText.add(adminActionPopup, %disabled);
    %prevItem.setText();
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
    %targetName.setText();
    1.setVisible();
    0.setVisible();
    0.setVisible();
};
function adminGui::onAction(%this) {
    0.setVisible();
    "confirm" @ " " @ adminActionPopup.getText() @ ":" @ " " @ adminTargetsPopup.getText().setText();
    1.setVisible();
    1.setVisible();
};
function adminGui::onCancel(%this) {
    1.setVisible();
    0.setVisible();
    0.setVisible();
};
function adminGui::onConfirm(%this) {
    1.setVisible();
    0.setVisible();
    0.setVisible();
    %message = adminGuiEditMessage.getValue();
    adminGuiButtonCancel;
    %action = adminActionPopup.getText();
    adminGuiButtonConfirm;
    %banUser = adminBanUserName.getValue();
    adminGuiButtonDoIt;
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
    "getting list..".setText();
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
        %entry.add(%n);
        if ((adminTargetsPopup @ " " @ %entry $= $gAdminGuiPrevMenuTarget)) {
            %nextItem = %entry;
        }
        %n = (1.0 + %n);
    }
    adminTargetsPopup.sort();
    if (!((%num < %n) @ " " @ %this.defaultTarget $= "")) {
        %this.defaultTarget.setText();
    }
    %nextItem.setText();
};
function adminTargetsPopup::onSelect(%this, %unused, %text) {
    1.setVisible();
    0.setVisible();
    0.setVisible();
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
        $adminTargetsList.onGotTargetsList();
    }
};
