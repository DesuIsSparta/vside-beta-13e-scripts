$CSLayoutSelector::NumLayouts = 3;
function CSLayoutSelector::toggle(%this)
{
    if (%this.isVisible())
    {
        %this.close();
    }
    else
    {
        %this.open();
    }
    return ;
}
function CSLayoutSelector::open(%this)
{
    CSLayoutSelectorSaveAsDefaultLink.setVisible(($ETS::devMode && $player.rolesPermissionCheckNoWarn("debugActive")) || $StandAlone);
    %this.setVisible(1);
    PlayGui.focusAndRaise(%this);
    WindowManager.update();
    %this.setMode("");
    CustomSpaceClient::checkEditingSpace();
    return ;
}
function CSLayoutSelector::close(%this)
{
    %this.setVisible(0);
    CustomSpaceClient::checkEditingSpace();
    PlayGui.focusTopWindow();
    WindowManager.update();
    return 1;
}
function CSLayoutSelector::layoutSelected(%this, %newLayoutSelection)
{
    if (%this.selectedLayout == %newLayoutSelection)
    {
        %this.setMode("");
        return ;
    }
    if (%this.layMode $= "COPY")
    {
        %this.getCopyTargetInfo(%this.selectedLayout, %newLayoutSelection);
        return ;
    }
    %undoClickCmd = "CSLayoutButtonsArray.getChild(" @ %this.selectedLayout @ ",0).buttonSelect.performClick();";
    MessageBoxYesNo($MsgCat::custSpace["LAYOUT_CHANGE","TITLE"], $MsgCat::custSpace["LAYOUT_CHANGE","BODY"], "CSLayoutSelector.layoutSelectedAndConfirmed(" @ %newLayoutSelection @ ");", %undoClickCmd);
    return ;
}
function CSLayoutSelector::layoutSelectedAndConfirmed(%this, %newLayoutSelection)
{
    %this.setSelectionState(%this.selectedLayout, 0);
    %this.setSelectionState(%newLayoutSelection, 1);
    %this.selectedLayout = %newLayoutSelection;
    %this.saveSettings();
    return ;
}
function CSLayoutSelector::setSelectionState(%this, %buttonIndex, %selected)
{
    CSLayoutButtonsArray.getObject(%buttonIndex).buttonSelect.setActive(!%selected);
    return ;
}
function CSLayoutSelector::saveSettings(%this)
{
    echo("Saving new space layout selection: " @ %this.selectedLayout);
    csSelectLayout(%this.selectedLayout);
    return ;
}
function CSLayoutSelector::updateSettings(%this, %numLayouts, %curLayout)
{
    if (%curLayout >= %numLayouts)
    {
        error(getScopeName() @ "->being told the selected layout is out of bounds!");
    }
    if (CSLayoutButtonsArray.getCount() != %numLayouts)
    {
        CSLayoutButtonsArray.setNumChildren(%numLayouts);
    }
    %oldSelected = %this.selectedLayout;
    %this.selectedLayout = %curLayout;
    CSLayoutButtonsArray.getObject(%this.selectedLayout).buttonSelect.performClick();
    %this.setSelectionState(%this.selectedLayout, 1);
    if (!((%oldSelected $= "")) && (%oldSelected != %this.selectedLayout))
    {
        %this.setSelectionState(%oldSelected, 0);
    }
    return ;
}
function CSLayoutSelector::cloneLayout(%this, %sourceLayout)
{
    %title = $MsgCat::custSpace["LAYOUT_CLONE","TITLE"];
    %body = $MsgCat::custSpace["LAYOUT_CLONE","BODY"];
    %body = strreplace(%body, "[SRC]", %sourceLayout + 1);
    %body = strreplace(%body, "[DST]", %this.selectedLayout + 1);
    MessageBoxYesNo(%title, %body, "CSLayoutSelector.cloneLayoutConfirmed(" @ %sourceLayout @ ");", "");
    return ;
}
function CSLayoutSelector::cloneLayoutConfirmed(%this, %sourceLayout)
{
    csCopyLayoutFromTo(%sourceLayout, %this.selectedLayout);
    return ;
}
function CSLayoutButtonsArray::onCreatedChild(%this, %child)
{
    %num = %this.getCount() - 1;
    %ctrl = new GuiBitmapButtonCtrl()
    {
        position = "3 0";
        extent = "35 35";
        profile = "GuiClickLabelProfile";
        command = CSLayoutSelector.getId() @ ".layoutSelected(" @ %num @ ");";
        bitmap = "platform/client/buttons/emptyCircleBrightInactive";
    };
    %ctrl.bindClassName("CSLayoutButton");
    %ctrl.num = %num;
    %child.buttonSelect = %ctrl;
    %child.add(%ctrl);
    CSLayoutSelector.setSelectionState(%num, 0);
    return ;
}
function CSLayoutSelectorLink::onURL(%this, %url)
{
    if (firstWord(%url) $= "gamelink")
    {
        %url = restWords(%url);
    }
    %cmd = firstWord(%url);
    %args = restWords(%url);
    if (%cmd $= "CLONE")
    {
        CSLayoutSelector.cloneLayout(%args);
    }
    else
    {
        if (%cmd $= "MODE")
        {
            CSLayoutSelector.setMode(trim(%args));
        }
        else
        {
            if (%cmd $= "ERASE")
            {
                customSpace::ConfirmEraseLayout(CSLayoutSelector.selectedLayout);
            }
            else
            {
                if (%cmd $= "DEFAULT")
                {
                    customSpace::ConfirmResetLayoutToDefault(CSLayoutSelector.selectedLayout);
                }
                else
                {
                    if (%cmd $= "SAVE_AS_DEFAULT")
                    {
                        customSpace::ConfirmSaveLayoutAsDefault(CSLayoutSelector.selectedLayout);
                    }
                }
            }
        }
    }
    return ;
}
function CSLayoutSelector::setMode(%this, %mode)
{
    if (%mode $= "COPY")
    {
        %titleText = "<color:ffffff>Copy Layout " @ %this.selectedLayout + 1 @ " To";
        %descText = "<color:ffffff>Select the layout to copy layout " @ %this.selectedLayout + 1 @ " into!";
        %copyLink = "<a:gamelink MODE DEFAULT>[cancel]</a>";
        %eraseLink = "";
        %defaultLink = "";
        %this.layMode = "COPY";
    }
    else
    {
        %titleText = "<color:ffffff>Layouts";
        %descText = "<color:ffffff>Use different layouts for your space!";
        %copyLink = "<a:gamelink MODE COPY>[copy]</a>";
        %eraseLink = "<a:gamelink ERASE>[erase]</a>";
        %defaultLink = "<a:gamelink DEFAULT>[default]</a>";
        %this.layMode = "";
    }
    CSLayoutSelectorTitleText.setText(%titleText);
    CSLayoutSelectorDescText.setText(%descText);
    CSLayoutSelectorCopyLink.setText(%copyLink);
    CSLayoutSelectorEraseLink.setText(%eraseLink);
    CSLayoutSelectorDefaultLink.setText(%defaultLink);
    return ;
}
function CSLayoutSelector::getCopyTargetInfo(%this, %sourceLayout, %layoutNum)
{
    %this.layMode = "COPY";
    %this.sourceLayout = %sourceLayout;
    %this.copyTarget = %layoutNum;
    csGetLayoutVitals(%layoutNum);
    return ;
}
function CSLayoutSelector::gotCopyTargetInfo(%this, %infoStr)
{
    %layoutNum = getField(%infoStr, 0);
    if (%this.copyTarget != %layoutNum)
    {
        warn(getScopeName() @ "-> got passed info string for layout idx = \"" @ %layoutNum @ "\" when copyTarget = \"" @ %this.copyTarget @ "\".");
        %this.copyTarget = "";
        return ;
    }
    if (!(%this.layMode $= "COPY"))
    {
        return ;
    }
    %this.copyTarget = "";
    %layoutFrom = %this.sourceLayout + 1;
    %layoutTo = %layoutNum + 1;
    %texturesChnged = trim(getField(%infoStr, 2));
    %numFurnishings = getField(%infoStr, 1);
    if (%numFurnishings > 0)
    {
        %title = $MsgCat::custSpace["LAYOUT_COPY_TRG_INVAL","TITLE"];
        %title = strreplace(%title, "[SRC]", %layoutFrom);
        %title = strreplace(%title, "[DST]", %layoutTo);
        %body = $MsgCat::custSpace["LAYOUT_COPY_TRG_INVAL","BODY"];
        %body = strreplace(%body, "[SRC]", %layoutFrom);
        %body = strreplace(%body, "[DST]", %layoutTo);
        MessageBoxOK(%title, %body, "");
        %this.setMode("");
        return ;
    }
    %this.copyLayout(%this.sourceLayout, %layoutNum, %texturesChnged);
    return ;
}
function CSLayoutSelector::copyLayout(%this, %layoutFrom, %layoutTo, %texturesChnged)
{
    %title = $MsgCat::custSpace["LAYOUT_COPY","TITLE"];
    %title = strreplace(%title, "[SRC]", %layoutFrom + 1);
    %title = strreplace(%title, "[DST]", %layoutTo + 1);
    if (!(%texturesChnged $= 1))
    {
        %body = $MsgCat::custSpace["LAYOUT_COPY","BODY"];
    }
    else
    {
        %body = $MsgCat::custSpace["LAYOUT_COPY","BODY_LOSE_TEX"];
    }
    %body = strreplace(%body, "[SRC]", %layoutFrom + 1);
    %body = strreplace(%body, "[DST]", %layoutTo + 1);
    %normalModeCmd = "CSLayoutSelector.setMode(\"\");";
    MessageBoxYesNo(%title, %body, "csCopyLayoutFromTo(" @ %layoutFrom @ ", " @ %layoutTo @ ");" @ %normalModeCmd, %normalModeCmd);
    return ;
}
function customSpace::ConfirmEraseLayout(%layoutNum)
{
    %title = $MsgCat::custSpace["LAYOUT_ERASE","TITLE"];
    %title = strreplace(%title, "[TRG]", %layoutNum + 1);
    %body = $MsgCat::custSpace["LAYOUT_ERASE","BODY"];
    %body = strreplace(%body, "[TRG]", %layoutNum + 1);
    %cbOkay = "CustomSpace::EraseLayout(" @ %layoutNum @ ");";
    %cbCancel = "";
    MessageBoxOkCancel(%title, %body, %cbOkay, %cbCancel);
    return ;
}
function customSpace::EraseLayout(%layoutNum)
{
    if (%layoutNum $= "")
    {
        error(getScopeName() @ "->passed empty string for layout num... ");
        return ;
    }
    commandToServer('CSClearLayout', CustomSpaceClient::GetSpaceImIn(), %layoutNum);
    setIdle(0);
    return ;
}
function customSpace::ConfirmResetLayoutToDefault(%layoutNum)
{
    %title = $MsgCat::custSpace["LAYOUT_DEFAULT","TITLE"];
    %title = strreplace(%title, "[TRG]", %layoutNum + 1);
    %body = $MsgCat::custSpace["LAYOUT_DEFAULT","BODY"];
    %body = strreplace(%body, "[TRG]", %layoutNum + 1);
    %cbOkay = "CustomSpace::ResetLayoutToDefault(" @ %layoutNum @ ");";
    %cbCancel = "";
    MessageBoxOkCancel(%title, %body, %cbOkay, %cbCancel);
    return ;
}
function customSpace::ResetLayoutToDefault(%layoutNum)
{
    if (%layoutNum $= "")
    {
        error(getScopeName() @ "->passed empty string for layout num...");
        return ;
    }
    commandToServer('CSResetLayoutToDefault', CustomSpaceClient::GetSpaceImIn(), %layoutNum);
    setIdle(0);
    return ;
}
function customSpace::ConfirmSaveLayoutAsDefault(%layoutIdx)
{
    %title = "Save Layout " @ %layoutIdx + 1 @ " To Default Apartment";
    %body = "Do you want to make layout " @ %layoutIdx + 1 @ " in your current space the default layout " @ %layoutIdx + 1 @ " for new apartments of";
    if ($StandAlone)
    {
        %body = %body @ " type " @ MissionInfo.modelID;
    }
    else
    {
        %body = %body @ " this type";
    }
    %body = %body @ "? The previous default layout " @ %layoutIdx + 1 @ " will be replaced locally.";
    %cbOkay = "csSaveLayoutAsDefault(" @ %layoutIdx @ ");";
    %cbCancel = "";
    MessageBoxOkCancel(%title, %body, %cbOkay, %cbCancel);
    return ;
}
function CSLayoutButton::onMouseDown(%this)
{
    %this.origin = Canvas.getCursorPos();
    return ;
}
function CSLayoutButton::onMouseDragged(%this)
{
    %vec = VectorSub(%this.origin, Canvas.getCursorPos());
    if (VectorLenSquared(%vec) < (6 * 6))
    {
        return 0;
    }
    %this.setAsDragControl(1);
    return 1;
}
function CSLayoutButton::makeVisualClone(%this)
{
    return new GuiBitmapButtonCtrl()
    {
        position = "0 0";
        extent = %this.getExtent();
        bitmap = %this.bitmap;
    };
    return ;
}
function CSLayoutButton::onDragAndDropEnter(%this, %dragCtrl)
{
    if (findWord(%dragCtrl.getNamespaceList(), "CSLayoutButton") == -1)
    {
        return ;
    }
    if (%this != %dragCtrl)
    {
        hiliteControl(%this, 1);
        %this.depressed = 1;
    }
    return ;
}
function CSLayoutButton::onDragAndDropLeave(%this, %dragCtrl)
{
    hiliteControl(0);
    %this.depressed = 0;
    return ;
}
function CSLayoutButton::onDragAndDropDrop(%this, %dragCtrl, %unused)
{
    if (findWord(%dragCtrl.getNamespaceList(), "CSLayoutButton") == -1)
    {
        return 0;
    }
    if (%this == %dragCtrl)
    {
        return 0;
    }
    CSLayoutSelector.getCopyTargetInfo(%dragCtrl.num, %this.num);
    return 1;
}
