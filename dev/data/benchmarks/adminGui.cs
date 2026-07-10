function adminActionPopup::onSelect(%this, %unused, %text) {
    selectAdminAction(%text);
};
function selectAdminAction(%text) {
    %text.setText();
    %text[$gAdminActionDefaultMessages @ %text].setValue();
    1.setVisible();
    0.setVisible();
    0.setVisible();
    %isBanCommand = (adminGuiButtonCancel SPC %text $= "Ban");
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
        open();
    }
    toggleVisibleState();
};
function adminGui::open(%this) {
    %this.pushDialog(0);
    %this.setVisible(1);
    0.onSelect(getText());
    1.makeFirstResponder();
    selectAll();
    %this.initMenu();
    if (!(%this SPC defaultAction $= "")) {
        selectAdminAction(defaultAction);
    }
    %this.onRefreshTargetsList();
};
function adminGui::close(%this, %unused) {
    %this.popDialog();
    %this.setVisible(0);
};
function adminGui::initMenu(%this, %unused) {
    %prevItem = getText();
    adminActionPopup;
    if ((%prevItem $= "")) {
        %prevItem = "Message";
    }
    clear();
    %grey = "0 0 0 128";
    adminActionPopup;
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
    if (isVisible()) {
        return adminGuiButtonConfirm;
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
    adminTargetsPopup @ getText().setText();
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
    %message = getValue();
    adminGuiEditMessage;
    %action = getText();
    adminActionPopup;
    %banUser = getValue();
    adminBanUserName;
    %duration = getValue();
    adminGuiEditDuration;
    if ((adminGuiButtonCancel SPC %action $= "Ban")) {
        %internalMsg = getValue();
        adminGuiEditInternalMessage;
    }
    %internalMsg = "";
    adminGuiButtonConfirm;
    warn(adminTargetsPopup @ getText() @ " " @ "with message:" @ " " @ %message);
    commandToServer('AdminAction', %action, getText(), %message, %banUser, %duration, %internalMsg);
};
function adminGui::onRefreshTargetsList(%this) {
    $gAdminGuiPrevMenuTarget = getText();
    adminTargetsPopup;
    "getting list..".setText();
    commandToServer('AdminGetTargets');
};
function adminGui::onGotTargetsList(%this, %theList) {
    clear();
    %num = getRecordCount(%theList);
    adminTargetsPopup;
    if ((1.0 < %num)) {
        error("apparently nobody is here. this is bad.");
        return;
    }
    %nextItem = getRecord(%theList, 0);
    %n = 0;
    if ((%num < %n)) {
        %entry = getRecord(%theList, %n);
        %entry.add(%n);
        if ((adminTargetsPopup SPC %entry $= $gAdminGuiPrevMenuTarget)) {
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
