function toggleSalonChairControlDialog()
{
    if (!$StandAlone)
    {
        return ;
    }
    if (!($gContiguousSpaceName $= "minimal"))
    {
        return ;
    }
    if (!$player.rolesPermissionCheckNoWarn("manageUsersBasic"))
    {
        return ;
    }
    toggleVisibleState(salonChairControlGui);
    return ;
}
function salonChairControlGui::open(%this)
{
    Canvas.pushDialog(%this, 0);
    %this.setVisible(1);
    %this.onRefreshTargetsList();
    return ;
}
function salonChairControlGui::close(%this, %unused)
{
    Canvas.popDialog(%this);
    %this.setVisible(0);
    return ;
}
function salonChairControlGui::tryTarget(%this, %shape)
{
    if (!%this.isVisible())
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
    salonChairControlTargetsPopup.setText(%targetName);
    return ;
}
function salonChairControlGui::sitInChair(%this, %chairType)
{
    %name = getField(salonChairControlTargetsPopup.getText(), 1);
    commandToServer('RequestOtherPlayerToSit', %name, "seSalonChairClient" @ %chairType);
    return ;
}
function salonChairControlGui::releaseFromChair(%this, %teleportAway)
{
    %name = getField(salonChairControlTargetsPopup.getText(), 1);
    commandToServer('RequestOtherPlayerToStand', %name, %teleportAway);
    return ;
}
$gSalonChairControlTargetsList = "";
function salonChairControlGui::onRefreshTargetsList(%this)
{
    $gSalonChairControlGuiPrevMenuTarget = salonChairControlTargetsPopup.getText();
    salonChairControlTargetsPopup.setText("getting list..");
    commandToServer('SalonChairControlGetTargets');
    return ;
}
function clientCmdBuildSalonChairControlTargetsList(%actionTagged, %item)
{
    %action = detag(%actionTagged);
    if (%action $= "begin")
    {
        $gSalonChairControlTargetsList = "";
    }
    else
    {
        if (%action $= "add")
        {
            if ($gSalonChairControlTargetsList $= "")
            {
                $gSalonChairControlTargetsList = %item;
            }
            else
            {
                $gSalonChairControlTargetsList = $gSalonChairControlTargetsList @ "\n" @ %item;
            }
        }
        else
        {
            if (%action $= "finish")
            {
                salonChairControlGui.onGotTargetsList($gSalonChairControlTargetsList);
            }
        }
    }
    return ;
}
function salonChairControlGui::onGotTargetsList(%this, %theList)
{
    salonChairControlTargetsPopup.clear();
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
        salonChairControlTargetsPopup.add(%entry, %n);
        if (%entry $= $gSalonChairControlGuiPrevMenuTarget)
        {
            %nextItem = %entry;
        }
        %n = %n + 1;
    }
    salonChairControlTargetsPopup.sort();
    if (!(%this.defaultTarget $= ""))
    {
        salonChairControlTargetsPopup.setText(%this.defaultTarget);
    }
    else
    {
        salonChairControlTargetsPopup.setText(%nextItem);
    }
    return ;
}
