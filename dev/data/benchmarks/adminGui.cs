function adminActionPopup::onSelect(%this, %unused, %text) {
    selectAdminAction(%text);
};
function selectAdminAction(%text) {
    %text.setText(adminActionPopup);
    %text[$gAdminActionDefaultMessages @ %text].setValue(adminGuiEditMessage);
    1.setVisible(adminGuiButtonDoIt);
    0.setVisible(adminGuiButtonConfirm);
    0.setVisible(adminGuiButtonCancel);
    %isBanCommand = (%text $= "Ban");
    %isBanCommand.setVisible(adminGuiInternalMessage);
    %isBanCommand.setVisible(adminGuiEditInternalMessage);
    0.setValue(adminBanUserName);
    %isBanCommand.setVisible(adminBanUserName);
    %isBanCommand.setVisible(adminGuiDuration);
    %isBanCommand.setVisible(adminGuiEditDuration);
};
function toggleAdminDialog(%action, %target) {
    if (!("manageUsersBasic".rolesPermissionCheckNoWarn($player))) {
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
    0.pushDialog(Canvas, %this);
    1.setVisible(%this);
    adminActionPopup.getText().onSelect(adminActionPopup, 0);
    1.makeFirstResponder(adminGuiEditMessage);
    adminGuiEditMessage.selectAll();
    %this.initMenu();
    if (!(%this.defaultAction $= "")) {
        selectAdminAction(%this.defaultAction);
    }
    %this.onRefreshTargetsList();
};
function adminGui::close(%this, %unused) {
    %this.popDialog(Canvas);
    0.setVisible(%this);
};
function adminGui::initMenu(%this, %unused) {
    %prevItem = adminActionPopup.getText();
    if ((%prevItem $= "")) {
        %prevItem = "Message";
    }
    adminActionPopup.clear();
    %grey = "0 0 0 128";
    %grey.addScheme(adminActionPopup, 1, %grey, %grey);
    %n = 0;
    %disabled = 0;
    %itemText = "Message";
    %n = (%n + 1.0);
    %disabled.add(adminActionPopup, %itemText, );
    %itemText = "Boot";
    %n = (%n + 1.0);
    %disabled.add(adminActionPopup, %itemText, );
    %itemText = "BootQuiet";
    %n = (%n + 1.0);
    %disabled.add(adminActionPopup, %itemText, );
    %itemText = "Ban";
    %n = (%n + 1.0);
    %disabled.add(adminActionPopup, %itemText, );
    %itemText = "Fly To";
    %n = (%n + 1.0);
    %disabled.add(adminActionPopup, %itemText, );
    %itemText = "Track";
    %n = (%n + 1.0);
    %disabled.add(adminActionPopup, %itemText, );
    %itemText = "Snoop Toggle";
    %n = (%n + 1.0);
    %disabled.add(adminActionPopup, %itemText, );
    %itemText = "Teleport To";
    %n = (%n + 1.0);
    %disabled.add(adminActionPopup, %itemText, );
    %itemText = "Respawn";
    %n = (%n + 1.0);
    %disabled.add(adminActionPopup, %itemText, );
    %itemText = "Summon";
    %n = (%n + 1.0);
    %disabled.add(adminActionPopup, %itemText, );
    %itemText = "Throw Voice";
    %n = (%n + 1.0);
    %disabled.add(adminActionPopup, %itemText, );
    %prevItem.setText(adminActionPopup);
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
    %targetName.setText(adminTargetsPopup);
    1.setVisible(adminGuiButtonDoIt);
    0.setVisible(adminGuiButtonConfirm);
    0.setVisible(adminGuiButtonCancel);
};
function adminGui::onAction(%this) {
    0.setVisible(adminGuiButtonDoIt);
    "confirm" @ " " @ adminActionPopup.getText() @ ":" @ " " @ adminTargetsPopup.getText().setText(adminGuiButtonConfirm);
    1.setVisible(adminGuiButtonConfirm);
    1.setVisible(adminGuiButtonCancel);
};
function adminGui::onCancel(%this) {
    1.setVisible(adminGuiButtonDoIt);
    0.setVisible(adminGuiButtonConfirm);
    0.setVisible(adminGuiButtonCancel);
};
function adminGui::onConfirm(%this) {
    1.setVisible(adminGuiButtonDoIt);
    0.setVisible(adminGuiButtonConfirm);
    0.setVisible(adminGuiButtonCancel);
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
    "getting list..".setText(adminTargetsPopup);
    commandToServer('AdminGetTargets');
};
function adminGui::onGotTargetsList(%this, %theList) {
    adminTargetsPopup.clear();
    %num = getRecordCount(%theList);
    if ((%num < 1.0)) {
        error("apparently nobody is here. this is bad.");
        return;
    }
    %nextItem = getRecord(%theList, 0);
    %n = 0;
    while ((%n < %num)) {
        %entry = getRecord(%theList, %n);
        %n.add(adminTargetsPopup, %entry);
        if ((%entry $= $gAdminGuiPrevMenuTarget)) {
            %nextItem = %entry;
        }
        %n = (%n + 1.0);
    }
    adminTargetsPopup.sort();
    if (!((%n < %num) @ " " @ %this.defaultTarget $= "")) {
        %this.defaultTarget.setText(adminTargetsPopup);
    }
    %nextItem.setText(adminTargetsPopup);
};
function adminTargetsPopup::onSelect(%this, %unused, %text) {
    1.setVisible(adminGuiButtonDoIt);
    0.setVisible(adminGuiButtonConfirm);
    0.setVisible(adminGuiButtonCancel);
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
        $adminTargetsList.onGotTargetsList(adminGui);
    }
};
