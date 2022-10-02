$gAdminActionDefaultMessages["Boot"] = "You have been booted by staff.";
$gAdminActionDefaultMessages["BootQuiet"] = "You have been courteously booted by staff.";
$gAdminActionDefaultMessages["Ban"] = "You have been suspended by staff.";
$gAdminActionDefaultMessages["Message"] = "";
$gAdminActionDefaultMessages["Summon"] = "You have been teleported!";
$gAdminActionDefaultMessages["Respawn"] = "You have been teleported away!";
$gAdminActionDefaultMessages["Throw Voice"] = "";
function adminActionPopup::onSelect(%this, %unused, %text)
{
    selectAdminAction(%text);
    return ;
}
function selectAdminAction(%text)
{
    adminActionPopup.setText(%text);
    adminGuiEditMessage.setValue($gAdminActionDefaultMessages[%text]);
    adminGuiButtonDoIt.setVisible(1);
    adminGuiButtonConfirm.setVisible(0);
    adminGuiButtonCancel.setVisible(0);
    %isBanCommand = %text $= "Ban";
    adminGuiInternalMessage.setVisible(%isBanCommand);
    adminGuiEditInternalMessage.setVisible(%isBanCommand);
    adminBanUserName.setValue(0);
    adminBanUserName.setVisible(%isBanCommand);
    adminGuiDuration.setVisible(%isBanCommand);
    adminGuiEditDuration.setVisible(%isBanCommand);
    return ;
}
function toggleAdminDialog(%action, %target)
{
    if (!$player.rolesPermissionCheckNoWarn("manageUsersBasic"))
    {
        return ;
    }
    if (!isDefined("%action"))
    {
        %action = "";
    }
    if (!isDefined("%target"))
    {
        %target = "";
    }
    adminGui.defaultAction = %action;
    adminGui.defaultTarget = %target;
    if (!((%action $= "")) && !((%target $= "")))
    {
        adminGui.open();
    }
    else
    {
        toggleVisibleState(adminGui);
    }
    return ;
}
function adminGui::open(%this)
{
    Canvas.pushDialog(%this, 0);
    %this.setVisible(1);
    adminActionPopup.onSelect(0, adminActionPopup.getText());
    adminGuiEditMessage.makeFirstResponder(1);
    adminGuiEditMessage.selectAll();
    %this.initMenu();
    if (!(%this.defaultAction $= ""))
    {
        selectAdminAction(%this.defaultAction);
    }
    %this.onRefreshTargetsList();
    return ;
}
function adminGui::close(%this, %unused)
{
    Canvas.popDialog(%this);
    %this.setVisible(0);
    return ;
}
function adminGui::initMenu(%this, %unused)
{
    %prevItem = adminActionPopup.getText();
    if (%prevItem $= "")
    {
        %prevItem = "Message";
    }
    adminActionPopup.clear();
    %grey = "0 0 0 128";
    adminActionPopup.addScheme(1, %grey, %grey, %grey);
    %n = 0;
    %disabled = 0;
    %itemText = "Message";
    adminActionPopup.add(%itemText, %n = %n + 1, %disabled);
    %itemText = "Boot";
    adminActionPopup.add(%itemText, %n = %n + 1, %disabled);
    %itemText = "BootQuiet";
    adminActionPopup.add(%itemText, %n = %n + 1, %disabled);
    %itemText = "Ban";
    adminActionPopup.add(%itemText, %n = %n + 1, %disabled);
    %itemText = "Fly To";
    adminActionPopup.add(%itemText, %n = %n + 1, %disabled);
    %itemText = "Track";
    adminActionPopup.add(%itemText, %n = %n + 1, %disabled);
    %itemText = "Snoop Toggle";
    adminActionPopup.add(%itemText, %n = %n + 1, %disabled);
    %itemText = "Teleport To";
    adminActionPopup.add(%itemText, %n = %n + 1, %disabled);
    %itemText = "Respawn";
    adminActionPopup.add(%itemText, %n = %n + 1, %disabled);
    %itemText = "Summon";
    adminActionPopup.add(%itemText, %n = %n + 1, %disabled);
    %itemText = "Throw Voice";
    adminActionPopup.add(%itemText, %n = %n + 1, %disabled);
    adminActionPopup.setText(%prevItem);
    return ;
}
function adminGui::tryTarget(%this, %shape)
{
    if (!%this.isVisible())
    {
        return ;
    }
    if (adminGuiButtonConfirm.isVisible())
    {
        return ;
    }
    %name = admin::getTargetName(%shape);
    if (isObject(%shape))
    {
        %classname = admin::getFormattedClassName(%shape.getClassName());
    }
    else
    {
        %classname = admin::getFormattedClassName("special");
    }
    %targetName = %classname @ "\t" @ %name;
    adminTargetsPopup.setText(%targetName);
    adminGuiButtonDoIt.setVisible(1);
    adminGuiButtonConfirm.setVisible(0);
    adminGuiButtonCancel.setVisible(0);
    return ;
}
function adminGui::onAction(%this)
{
    adminGuiButtonDoIt.setVisible(0);
    adminGuiButtonConfirm.setText("confirm" SPC adminActionPopup.getText() @ ":" SPC adminTargetsPopup.getText());
    adminGuiButtonConfirm.setVisible(1);
    adminGuiButtonCancel.setVisible(1);
    return ;
}
function adminGui::onCancel(%this)
{
    adminGuiButtonDoIt.setVisible(1);
    adminGuiButtonConfirm.setVisible(0);
    adminGuiButtonCancel.setVisible(0);
    return ;
}
function adminGui::onConfirm(%this)
{
    adminGuiButtonDoIt.setVisible(1);
    adminGuiButtonConfirm.setVisible(0);
    adminGuiButtonCancel.setVisible(0);
    %message = adminGuiEditMessage.getValue();
    %action = adminActionPopup.getText();
    %banUser = adminBanUserName.getValue();
    %duration = adminGuiEditDuration.getValue();
    if (%action $= "Ban")
    {
        %internalMsg = adminGuiEditInternalMessage.getValue();
    }
    else
    {
        %internalMsg = "";
    }
    warn("adminAction:" SPC $player.getShapeName() SPC %action SPC "on" SPC adminTargetsPopup.getText() SPC "with message:" SPC %message);
    commandToServer('AdminAction', %action, adminTargetsPopup.getText(), %message, %banUser, %duration, %internalMsg);
    return ;
}
function adminGui::onRefreshTargetsList(%this)
{
    $gAdminGuiPrevMenuTarget = adminTargetsPopup.getText();
    adminTargetsPopup.setText("getting list..");
    commandToServer('AdminGetTargets');
    return ;
}
function adminGui::onGotTargetsList(%this, %theList)
{
    adminTargetsPopup.clear();
    %num = getRecordCount(%theList);
    if (%num < 1)
    {
        error("apparently nobody is here. this is bad.");
        return ;
    }
    %nextItem = getRecord(%theList, 0);
    %n = 0;
    while (%n < %num)
    {
        %entry = getRecord(%theList, %n);
        adminTargetsPopup.add(%entry, %n);
        if (%entry $= $gAdminGuiPrevMenuTarget)
        {
            %nextItem = %entry;
        }
        %n = %n + 1;
    }
    adminTargetsPopup.sort();
    if (!(%this.defaultTarget $= ""))
    {
        adminTargetsPopup.setText(%this.defaultTarget);
    }
    else
    {
        adminTargetsPopup.setText(%nextItem);
    }
    return ;
}
function adminTargetsPopup::onSelect(%this, %unused, %text)
{
    adminGuiButtonDoIt.setVisible(1);
    adminGuiButtonConfirm.setVisible(0);
    adminGuiButtonCancel.setVisible(0);
    return ;
}
$adminTargetsList = "";
function clientCmdBuildTargetsList(%actionTagged, %item)
{
    %action = detag(%actionTagged);
    if (%action $= "begin")
    {
        $adminTargetsList = "";
    }
    else
    {
        if (%action $= "add")
        {
            if ($adminTargetsList $= "")
            {
                $adminTargetsList = %item;
            }
            else
            {
                $adminTargetsList = $adminTargetsList @ "\n" @ %item;
            }
        }
        else
        {
            if (%action $= "finish")
            {
                adminGui.onGotTargetsList($adminTargetsList);
            }
        }
    }
    return ;
}
