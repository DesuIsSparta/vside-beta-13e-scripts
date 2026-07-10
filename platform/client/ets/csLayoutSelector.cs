$CSLayoutSelector::NumLayouts = 3;
function CSLayoutSelector::toggle(%this) {
    if (%this.isVisible()) {
        %this.close();
    }
    %this.open();
};
function CSLayoutSelector::open(%this) {
    if ($ETS::devMode) {
        if ($player.rolesPermissionCheckNoWarn("debugActive")) {
        }
    }
    $StandAlone.setVisible();
    %this.setVisible(1);
    %this.focusAndRaise();
    update();
    %this.setMode("");
    CustomSpaceClient::checkEditingSpace();
};
function CSLayoutSelector::close(%this) {
    %this.setVisible(0);
    CustomSpaceClient::checkEditingSpace();
    focusTopWindow();
    update();
    return 1;
};
function CSLayoutSelector::layoutSelected(%this, %newLayoutSelection) {
    if ((%this == selectedLayout)) {
        %this.setMode("");
        return %newLayoutSelection;
    }
    if ((%this SPC layMode $= "COPY")) {
        %this.getCopyTargetInfo(selectedLayout, %newLayoutSelection);
        return %this;
    }
    %undoClickCmd = "CSLayoutButtonsArray.getChild(" @ %this @ selectedLayout @ ",0).buttonSelect.performClick();";
    MessageBoxYesNo(%undoClickCmd[$MsgCat::custSpace TAB "LAYOUT_CHANGE" @ "TITLE"], , "CSLayoutSelector.layoutSelectedAndConfirmed(" @ %newLayoutSelection @ ");", %undoClickCmd);
};
function CSLayoutSelector::layoutSelectedAndConfirmed(%this, %newLayoutSelection) {
    %this.setSelectionState(selectedLayout, 0);
    %this.setSelectionState(%newLayoutSelection, 1);
    selectedLayout = %this @ %newLayoutSelection @ %this;
    %this.saveSettings();
};
function CSLayoutSelector::setSelectionState(%this, %buttonIndex, %selected) {
    buttonSelect.setActive(!(%selected));
};
function CSLayoutSelector::saveSettings(%this) {
    echo(%this @ selectedLayout);
    csSelectLayout(selectedLayout);
};
function CSLayoutSelector::updateSettings(%this, %numLayouts, %curLayout) {
    if ((%numLayouts >= %curLayout)) {
        error(getScopeName() @ "->being told the selected layout is out of bounds!");
    }
    if ((CSLayoutButtonsArray != getCount())) {
        %numLayouts.setNumChildren();
    }
    %oldSelected = selectedLayout;
    %this;
    selectedLayout = CSLayoutButtonsArray @ %curLayout @ %this;
    %numLayouts;
    buttonSelect.performClick();
    %this.setSelectionState(selectedLayout, 1);
    if (!(%this SPC %oldSelected $= "")) {
    }
    if ((selectedLayout != %oldSelected)) {
        %this.setSelectionState(%oldSelected, 0);
    }
};
function CSLayoutSelector::cloneLayout(%this, %sourceLayout) {
    %title = ;
    %body = %title[$MsgCat::custSpace TAB "LAYOUT_CLONE" @ "BODY"];
    %body = strreplace(%body, "[SRC]", (1.0 + %sourceLayout));
    %body = strreplace(%body, "[DST]", (%this + selectedLayout));
    1.0;
    MessageBoxYesNo(%title, %body, "CSLayoutSelector.cloneLayoutConfirmed(" @ %sourceLayout @ ");", "");
};
function CSLayoutSelector::cloneLayoutConfirmed(%this, %sourceLayout) {
    csCopyLayoutFromTo(%sourceLayout, selectedLayout);
};
function CSLayoutButtonsArray::onCreatedChild(%this, %child) {
    %num = (1.0 - %this.getCount());
    position = GuiBitmapButtonCtrl @ new ""() @ "3 0";
    0;
    extent = "35 35";
    profile = "GuiClickLabelProfile";
    command = CSLayoutSelector @ getId() @ ".layoutSelected(" @ %num @ ");";
    bitmap = "platform/client/buttons/emptyCircleBrightInactive";
    profile = GuiMLTextCtrl @ new ""() @ "etsNonModalProfile";
    position = "9 11";
    extent = "15 15";
    text = "<color:ffffff><b><just:center>" @ " " @ (1.0 + %num);
    %ctrl = ;
    %ctrl.bindClassName("CSLayoutButton");
    num = %num @ %ctrl;
    buttonSelect = %ctrl @ %child;
    %child.add(%ctrl);
    %num.setSelectionState(0);
};
function CSLayoutSelectorLink::onURL(%this, %url) {
    if ((firstWord(%url) $= "gamelink")) {
        %url = restWords(%url);
    }
    %cmd = firstWord(%url);
    %args = restWords(%url);
    if ((%cmd $= "CLONE")) {
        %args.cloneLayout();
    }
    if ((CSLayoutSelector SPC %cmd $= "MODE")) {
        trim(%args).setMode();
    }
    if ((CSLayoutSelector SPC %cmd $= "ERASE")) {
        customSpace::ConfirmEraseLayout(selectedLayout);
    }
    if ((CSLayoutSelector SPC %cmd $= "DEFAULT")) {
        customSpace::ConfirmResetLayoutToDefault(selectedLayout);
    }
    if ((CSLayoutSelector SPC %cmd $= "SAVE_AS_DEFAULT")) {
        customSpace::ConfirmSaveLayoutAsDefault(selectedLayout);
    }
};
function CSLayoutSelector::setMode(%this, %mode) {
    if ((%mode $= "COPY")) {
        %titleText = "<color:ffffff>Copy Layout " @ 1.0 @ (%this + selectedLayout) @ " To";
        %descText = "<color:ffffff>Select the layout to copy layout " @ 1.0 @ (%this + selectedLayout) @ " into!";
        %copyLink = "<a:gamelink MODE DEFAULT>[cancel]</a>";
        %eraseLink = "";
        %defaultLink = "";
        layMode = "COPY" @ %this;
    }
    %titleText = "<color:ffffff>Layouts";
    %descText = "<color:ffffff>Use different layouts for your space!";
    %copyLink = "<a:gamelink MODE COPY>[copy]</a>";
    %eraseLink = "<a:gamelink ERASE>[erase]</a>";
    %defaultLink = "<a:gamelink DEFAULT>[default]</a>";
    layMode = "" @ %this;
    %titleText.setText();
    %descText.setText();
    %copyLink.setText();
    %eraseLink.setText();
    %defaultLink.setText();
};
function CSLayoutSelector::getCopyTargetInfo(%this, %sourceLayout, %layoutNum) {
    layMode = "COPY" @ %this;
    sourceLayout = %sourceLayout @ %this;
    copyTarget = %layoutNum @ %this;
    csGetLayoutVitals(%layoutNum);
};
function CSLayoutSelector::gotCopyTargetInfo(%this, %infoStr) {
    %layoutNum = getField(%infoStr, 0);
    if ((%this != copyTarget)) {
        warn(%layoutNum @ getScopeName() @ "-> got passed info string for layout idx = \"" @ %layoutNum @ "\" when copyTarget = \"" @ %this @ copyTarget @ "\".");
        copyTarget = "" @ %this;
        return;
    }
    if (!(%this SPC layMode $= "COPY")) {
        return;
    }
    copyTarget = "" @ %this;
    %layoutFrom = (%this + sourceLayout);
    1.0;
    %layoutTo = (1.0 + %layoutNum);
    %texturesChnged = trim(getField(%infoStr, 2));
    %numFurnishings = getField(%infoStr, 1);
    if ((0.0 > %numFurnishings)) {
        %title = %numFurnishings[$MsgCat::custSpace TAB "LAYOUT_COPY_TRG_INVAL" @ "TITLE"];
        %title = strreplace(%title, "[SRC]", %layoutFrom);
        %title = strreplace(%title, "[DST]", %layoutTo);
        %body = %title[$MsgCat::custSpace TAB "LAYOUT_COPY_TRG_INVAL" @ "BODY"];
        %body = strreplace(%body, "[SRC]", %layoutFrom);
        %body = strreplace(%body, "[DST]", %layoutTo);
        MessageBoxOK(%title, %body, "");
        %this.setMode("");
        return;
    }
    %this.copyLayout(sourceLayout, %layoutNum, %texturesChnged);
};
function CSLayoutSelector::copyLayout(%this, %layoutFrom, %layoutTo, %texturesChnged) {
    %title = ;
    %title = strreplace(%title, "[SRC]", (1.0 + %layoutFrom));
    %title = strreplace(%title, "[DST]", (1.0 + %layoutTo));
    if (!(%texturesChnged $= 1)) {
        %body = %texturesChnged[$MsgCat::custSpace TAB "LAYOUT_COPY" @ "BODY"];
    }
    %body = %body[$MsgCat::custSpace TAB "LAYOUT_COPY" @ "BODY_LOSE_TEX"];
    %body = strreplace(%body, "[SRC]", (1.0 + %layoutFrom));
    %body = strreplace(%body, "[DST]", (1.0 + %layoutTo));
    %normalModeCmd = "CSLayoutSelector.setMode(\"\");";
    MessageBoxYesNo(%title, %body, "csCopyLayoutFromTo(" @ %layoutFrom @ ", " @ %layoutTo @ ");" @ %normalModeCmd, %normalModeCmd);
};
function customSpace::ConfirmEraseLayout(%layoutNum) {
    %title = ;
    %title = strreplace(%title, "[TRG]", (1.0 + %layoutNum));
    %body = %title[$MsgCat::custSpace TAB "LAYOUT_ERASE" @ "BODY"];
    %body = strreplace(%body, "[TRG]", (1.0 + %layoutNum));
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
    %title = ;
    %title = strreplace(%title, "[TRG]", (1.0 + %layoutNum));
    %body = %title[$MsgCat::custSpace TAB "LAYOUT_DEFAULT" @ "BODY"];
    %body = strreplace(%body, "[TRG]", (1.0 + %layoutNum));
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
    %title = "Save Layout " @ (1.0 + %layoutIdx) @ " To Default Apartment";
    %body = "Do you want to make layout " @ (1.0 + %layoutIdx) @ " in your current space the default layout " @ (1.0 + %layoutIdx) @ " for new apartments of";
    if ($StandAlone) {
        %body = MissionInfo @ modelID;
        %body @ " type ";
    }
    %body = %body @ " this type";
    %body = %body @ "? The previous default layout " @ (1.0 + %layoutIdx) @ " will be replaced locally.";
    %cbOkay = "csSaveLayoutAsDefault(" @ %layoutIdx @ ");";
    %cbCancel = "";
    MessageBoxOkCancel(%title, %body, %cbOkay, %cbCancel);
};
function CSLayoutButton::onMouseDown(%this) {
    origin = Canvas @ getCursorPos() @ %this;
};
function CSLayoutButton::onMouseDragged(%this) {
    %vec = VectorSub(origin, getCursorPos());
    Canvas;
    if (((6.0 * 6.0) < VectorLenSquared(%vec))) {
        return 0;
    }
    %this.setAsDragControl(1);
    return 1;
};
function CSLayoutButton::makeVisualClone(%this) {
    position = GuiBitmapButtonCtrl @ new ""() @ "0 0";
    0;
    extent = %this.getExtent();
    bitmap = %this @ bitmap;
    profile = GuiMLTextCtrl @ new ""() @ "etsNonModalProfile";
    position = "9 11";
    extent = "15 15";
    text = "<color:ffffff><b><just:center>" @ " " @ 1.0 @ (%this + num);
    return;
};
function CSLayoutButton::onDragAndDropEnter(%this, %dragCtrl) {
    if ((-(1.0) == findWord(%dragCtrl.getNamespaceList(), "CSLayoutButton"))) {
        return;
    }
    if ((%dragCtrl != %this)) {
        hiliteControl(%this, 1);
        depressed = 1 @ %this;
    }
};
function CSLayoutButton::onDragAndDropLeave(%this, %dragCtrl) {
    hiliteControl(0);
    depressed = 0 @ %this;
};
function CSLayoutButton::onDragAndDropDrop(%this, %dragCtrl, %unused) {
    if ((-(1.0) == findWord(%dragCtrl.getNamespaceList(), "CSLayoutButton"))) {
        return 0;
    }
    if ((%dragCtrl == %this)) {
        return 0;
    }
    num.getCopyTargetInfo(num);
    return 1;
};
