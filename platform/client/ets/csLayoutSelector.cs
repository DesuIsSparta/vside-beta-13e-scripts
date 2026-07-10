$CSLayoutSelector::NumLayouts = 3;
function CSLayoutSelector::toggle(%this) {
    if (%this.isVisible()) {
        %this.close();
    }
    %this.open();
};
function CSLayoutSelector::open(%this) {
    if ($ETS::devMode && "debugActive".rolesPermissionCheckNoWarn($player)) {
    }
    $StandAlone.setVisible(CSLayoutSelectorSaveAsDefaultLink);
    1.setVisible(%this);
    %this.focusAndRaise(PlayGui);
    WindowManager.update();
    "".setMode(%this);
    CustomSpaceClient::checkEditingSpace();
};
function CSLayoutSelector::close(%this) {
    0.setVisible(%this);
    CustomSpaceClient::checkEditingSpace();
    PlayGui.focusTopWindow();
    WindowManager.update();
    return 1;
};
function CSLayoutSelector::layoutSelected(%this, %newLayoutSelection) {
    if ((%this.selectedLayout == %newLayoutSelection)) {
        "".setMode(%this);
        return;
    }
    if ((%this.layMode $= "COPY")) {
        %newLayoutSelection.getCopyTargetInfo(%this, %this.selectedLayout);
        return;
    }
    %undoClickCmd = "CSLayoutButtonsArray.getChild(" @ %this.selectedLayout @ ",0).buttonSelect.performClick();";
    MessageBoxYesNo($MsgCat::custSpace["LAYOUT_CHANGE","TITLE"], $MsgCat::custSpace["LAYOUT_CHANGE","BODY"], "CSLayoutSelector.layoutSelectedAndConfirmed(" @ %newLayoutSelection @ ");", %undoClickCmd);
};
function CSLayoutSelector::layoutSelectedAndConfirmed(%this, %newLayoutSelection) {
    0.setSelectionState(%this, %this.selectedLayout);
    1.setSelectionState(%this, %newLayoutSelection);
    %this.selectedLayout = %newLayoutSelection;
    %this.saveSettings();
};
function CSLayoutSelector::setSelectionState(%this, %buttonIndex, %selected) {
    !(%selected).setActive(%buttonIndex.getObject(CSLayoutButtonsArray).buttonSelect);
};
function CSLayoutSelector::saveSettings(%this) {
    echo("Saving new space layout selection: " @ %this.selectedLayout);
    csSelectLayout(%this.selectedLayout);
};
function CSLayoutSelector::updateSettings(%this, %numLayouts, %curLayout) {
    if ((%curLayout >= %numLayouts)) {
        error(getScopeName() @ "->being told the selected layout is out of bounds!");
    }
    if ((CSLayoutButtonsArray.getCount() != %numLayouts)) {
        %numLayouts.setNumChildren(CSLayoutButtonsArray);
    }
    %oldSelected = %this.selectedLayout;
    %this.selectedLayout = %curLayout;
    %this.selectedLayout.getObject(CSLayoutButtonsArray).buttonSelect.performClick();
    1.setSelectionState(%this, %this.selectedLayout);
    if (!(%oldSelected $= "")) {
    }
    if ((%oldSelected != %this.selectedLayout)) {
        0.setSelectionState(%this, %oldSelected);
    }
};
function CSLayoutSelector::cloneLayout(%this, %sourceLayout) {
    %title = $MsgCat::custSpace["LAYOUT_CLONE","TITLE"];
    %body = $MsgCat::custSpace["LAYOUT_CLONE","BODY"];
    %body = strreplace(%body, "[SRC]", (%sourceLayout + 1.0));
    %body = strreplace(%body, "[DST]", (%this.selectedLayout + 1.0));
    MessageBoxYesNo(%title, %body, "CSLayoutSelector.cloneLayoutConfirmed(" @ %sourceLayout @ ");", "");
};
function CSLayoutSelector::cloneLayoutConfirmed(%this, %sourceLayout) {
    csCopyLayoutFromTo(%sourceLayout, %this.selectedLayout);
};
function CSLayoutButtonsArray::onCreatedChild(%this, %child) {
    %num = (%this.getCount() - 1.0);
    %ctrl = new GuiBitmapButtonCtrl("") {
        position = "3 0";
        extent = "35 35";
        profile = "GuiClickLabelProfile";
        command = CSLayoutSelector.getId() @ ".layoutSelected(" @ %num @ ");";
        bitmap = "platform/client/buttons/emptyCircleBrightInactive";
    };
    new GuiMLTextCtrl("") {
        profile = "etsNonModalProfile";
        position = "9 11";
        extent = "15 15";
        text = "<color:ffffff><b><just:center>" @ " " @ (%num + 1.0);
    };
    "CSLayoutButton".bindClassName(%ctrl);
    %ctrl.num = %num;
    %child.buttonSelect = %ctrl;
    %ctrl.add(%child);
    0.setSelectionState(CSLayoutSelector, %num);
};
function CSLayoutSelectorLink::onURL(%this, %url) {
    if ((firstWord(%url) $= "gamelink")) {
        %url = restWords(%url);
    }
    %cmd = firstWord(%url);
    %args = restWords(%url);
    if ((%cmd $= "CLONE")) {
        %args.cloneLayout(CSLayoutSelector);
    }
    if ((%cmd $= "MODE")) {
        trim(%args).setMode(CSLayoutSelector);
    }
    if ((%cmd $= "ERASE")) {
        customSpace::ConfirmEraseLayout(CSLayoutSelector.selectedLayout);
    }
    if ((%cmd $= "DEFAULT")) {
        customSpace::ConfirmResetLayoutToDefault(CSLayoutSelector.selectedLayout);
    }
    if ((%cmd $= "SAVE_AS_DEFAULT")) {
        customSpace::ConfirmSaveLayoutAsDefault(CSLayoutSelector.selectedLayout);
    }
};
function CSLayoutSelector::setMode(%this, %mode) {
    if ((%mode $= "COPY")) {
        %titleText = "<color:ffffff>Copy Layout " @ (%this.selectedLayout + 1.0) @ " To";
        %descText = "<color:ffffff>Select the layout to copy layout " @ (%this.selectedLayout + 1.0) @ " into!";
        %copyLink = "<a:gamelink MODE DEFAULT>[cancel]</a>";
        %eraseLink = "";
        %defaultLink = "";
        %this.layMode = "COPY";
    }
    %titleText = "<color:ffffff>Layouts";
    %descText = "<color:ffffff>Use different layouts for your space!";
    %copyLink = "<a:gamelink MODE COPY>[copy]</a>";
    %eraseLink = "<a:gamelink ERASE>[erase]</a>";
    %defaultLink = "<a:gamelink DEFAULT>[default]</a>";
    %this.layMode = "";
    %titleText.setText(CSLayoutSelectorTitleText);
    %descText.setText(CSLayoutSelectorDescText);
    %copyLink.setText(CSLayoutSelectorCopyLink);
    %eraseLink.setText(CSLayoutSelectorEraseLink);
    %defaultLink.setText(CSLayoutSelectorDefaultLink);
};
function CSLayoutSelector::getCopyTargetInfo(%this, %sourceLayout, %layoutNum) {
    %this.layMode = "COPY";
    %this.sourceLayout = %sourceLayout;
    %this.copyTarget = %layoutNum;
    csGetLayoutVitals(%layoutNum);
};
function CSLayoutSelector::gotCopyTargetInfo(%this, %infoStr) {
    %layoutNum = getField(%infoStr, 0);
    if ((%this.copyTarget != %layoutNum)) {
        warn(getScopeName() @ "-> got passed info string for layout idx = \"" @ %layoutNum @ "\" when copyTarget = \"" @ %this.copyTarget @ "\".");
        %this.copyTarget = "";
        return;
    }
    if (!(%this.layMode $= "COPY")) {
        return;
    }
    %this.copyTarget = "";
    %layoutFrom = (%this.sourceLayout + 1.0);
    %layoutTo = (%layoutNum + 1.0);
    %texturesChnged = trim(getField(%infoStr, 2));
    %numFurnishings = getField(%infoStr, 1);
    if ((%numFurnishings > 0.0)) {
        %title = %numFurnishings[$MsgCat::custSpace TAB "LAYOUT_COPY_TRG_INVAL" @ "TITLE"];
        %title = strreplace(%title, "[SRC]", %layoutFrom);
        %title = strreplace(%title, "[DST]", %layoutTo);
        %body = $MsgCat::custSpace["LAYOUT_COPY_TRG_INVAL","BODY"];
        %body = strreplace(%body, "[SRC]", %layoutFrom);
        %body = strreplace(%body, "[DST]", %layoutTo);
        MessageBoxOK(%title, %body, "");
        "".setMode(%this);
        return;
    }
    %texturesChnged.copyLayout(%this, %this.sourceLayout, %layoutNum);
};
function CSLayoutSelector::copyLayout(%this, %layoutFrom, %layoutTo, %texturesChnged) {
    %title = $MsgCat::custSpace["LAYOUT_COPY","TITLE"];
    %title = strreplace(%title, "[SRC]", (%layoutFrom + 1.0));
    %title = strreplace(%title, "[DST]", (%layoutTo + 1.0));
    if (!(%texturesChnged $= 1)) {
        %body = %texturesChnged[$MsgCat::custSpace TAB "LAYOUT_COPY" @ "BODY"];
    }
    %body = $MsgCat::custSpace["LAYOUT_COPY","BODY_LOSE_TEX"];
    %body = strreplace(%body, "[SRC]", (%layoutFrom + 1.0));
    %body = strreplace(%body, "[DST]", (%layoutTo + 1.0));
    %normalModeCmd = "CSLayoutSelector.setMode(\"\");";
    MessageBoxYesNo(%title, %body, "csCopyLayoutFromTo(" @ %layoutFrom @ ", " @ %layoutTo @ ");" @ %normalModeCmd, %normalModeCmd);
};
function customSpace::ConfirmEraseLayout(%layoutNum) {
    %title = $MsgCat::custSpace["LAYOUT_ERASE","TITLE"];
    %title = strreplace(%title, "[TRG]", (%layoutNum + 1.0));
    %body = $MsgCat::custSpace["LAYOUT_ERASE","BODY"];
    %body = strreplace(%body, "[TRG]", (%layoutNum + 1.0));
    %cbOkay = "CustomSpace::EraseLayout(" @ %layoutNum @ ");";
    %cbCancel = "";
    MessageBoxOkCancel(%title, %body, %cbOkay, %cbCancel);
};
function customSpace::EraseLayout(%layoutNum) {
    if ((%layoutNum $= "")) {
        error(getScopeName() @ "->passed empty string for layout num... ");
        return;
    }
    commandToServer('CSClearLayout', CustomSpaceClient::GetSpaceImIn(), %layoutNum);
    setIdle(0);
};
function customSpace::ConfirmResetLayoutToDefault(%layoutNum) {
    %title = $MsgCat::custSpace["LAYOUT_DEFAULT","TITLE"];
    %title = strreplace(%title, "[TRG]", (%layoutNum + 1.0));
    %body = $MsgCat::custSpace["LAYOUT_DEFAULT","BODY"];
    %body = strreplace(%body, "[TRG]", (%layoutNum + 1.0));
    %cbOkay = "CustomSpace::ResetLayoutToDefault(" @ %layoutNum @ ");";
    %cbCancel = "";
    MessageBoxOkCancel(%title, %body, %cbOkay, %cbCancel);
};
function customSpace::ResetLayoutToDefault(%layoutNum) {
    if ((%layoutNum $= "")) {
        error(getScopeName() @ "->passed empty string for layout num...");
        return;
    }
    commandToServer('CSResetLayoutToDefault', CustomSpaceClient::GetSpaceImIn(), %layoutNum);
    setIdle(0);
};
function customSpace::ConfirmSaveLayoutAsDefault(%layoutIdx) {
    %title = "Save Layout " @ (%layoutIdx + 1.0) @ " To Default Apartment";
    %body = "Do you want to make layout " @ (%layoutIdx + 1.0) @ " in your current space the default layout " @ (%layoutIdx + 1.0) @ " for new apartments of";
    if ($StandAlone) {
        %body = %body @ " type " @ MissionInfo.modelID;
    }
    %body = %body @ " this type";
    %body = %body @ "? The previous default layout " @ (%layoutIdx + 1.0) @ " will be replaced locally.";
    %cbOkay = "csSaveLayoutAsDefault(" @ %layoutIdx @ ");";
    %cbCancel = "";
    MessageBoxOkCancel(%title, %body, %cbOkay, %cbCancel);
};
function CSLayoutButton::onMouseDown(%this) {
    %this.origin = Canvas.getCursorPos();
};
function CSLayoutButton::onMouseDragged(%this) {
    %vec = VectorSub(%this.origin, Canvas.getCursorPos());
    if ((VectorLenSquared(%vec) < (6.0 * 6.0))) {
        return 0;
    }
    1.setAsDragControl(%this);
    return 1;
};
function CSLayoutButton::makeVisualClone(%this) {
    return new GuiBitmapButtonCtrl("") {
        position = "0 0";
        extent = %this.getExtent();
        bitmap = %this.bitmap;
    };;
};
function CSLayoutButton::onDragAndDropEnter(%this, %dragCtrl) {
    if ((findWord(%dragCtrl.getNamespaceList(), "CSLayoutButton") == -(1.0))) {
        return;
    }
    if ((%this != %dragCtrl)) {
        hiliteControl(%this, 1);
        %this.depressed = 1;
    }
};
function CSLayoutButton::onDragAndDropLeave(%this, %dragCtrl) {
    hiliteControl(0);
    %this.depressed = 0;
};
function CSLayoutButton::onDragAndDropDrop(%this, %dragCtrl, %unused) {
    if ((findWord(%dragCtrl.getNamespaceList(), "CSLayoutButton") == -(1.0))) {
        return 0;
    }
    if ((%this == %dragCtrl)) {
        return 0;
    }
    %this.num.getCopyTargetInfo(CSLayoutSelector, %dragCtrl.num);
    return 1;
};
