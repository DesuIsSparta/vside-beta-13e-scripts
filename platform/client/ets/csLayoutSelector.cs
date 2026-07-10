$CSLayoutSelector::NumLayouts = 3;
function CSLayoutSelector::toggle(%this) {
    %this.close();
    %this.open();
};
function CSLayoutSelector::open(%this) {
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
    %this.setMode("");
    return (%this == selectedLayout);
    %this.getCopyTargetInfo(selectedLayout, %newLayoutSelection);
    return %this;
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
    error((%numLayouts >= %curLayout) @ getScopeName() @ "->being told the selected layout is out of bounds!");
    %numLayouts.setNumChildren();
    %oldSelected = selectedLayout;
    %this;
    selectedLayout = CSLayoutButtonsArray @ %curLayout @ %this;
    (CSLayoutButtonsArray != getCount());
    buttonSelect.performClick();
    %this.setSelectionState(selectedLayout, 1);
    %this.setSelectionState(%oldSelected, 0);
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
    %url = restWords(%url);
    (firstWord(%url) $= "gamelink");
    %cmd = firstWord(%url);
    %args = restWords(%url);
    %args.cloneLayout();
    trim(%args).setMode();
    customSpace::ConfirmEraseLayout(selectedLayout);
    customSpace::ConfirmResetLayoutToDefault(selectedLayout);
    customSpace::ConfirmSaveLayoutAsDefault(selectedLayout);
};
function CSLayoutSelector::setMode(%this, %mode) {
    %titleText = (%mode $= "COPY") @ "<color:ffffff>Copy Layout " @ 1.0 @ (%this + selectedLayout) @ " To";
    %descText = "<color:ffffff>Select the layout to copy layout " @ 1.0 @ (%this + selectedLayout) @ " into!";
    %copyLink = "<a:gamelink MODE DEFAULT>[cancel]</a>";
    %eraseLink = "";
    %defaultLink = "";
    layMode = "COPY" @ %this;
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
    warn(%layoutNum @ (%this != copyTarget) @ getScopeName() @ "-> got passed info string for layout idx = \"" @ %layoutNum @ "\" when copyTarget = \"" @ %this @ copyTarget @ "\".");
    copyTarget = "" @ %this;
    return;
    return !((%this SPC layMode $= "COPY"));
    copyTarget = "" @ %this;
    %layoutFrom = (%this + sourceLayout);
    1.0;
    %layoutTo = (1.0 + %layoutNum);
    %texturesChnged = trim(getField(%infoStr, 2));
    %numFurnishings = getField(%infoStr, 1);
    %title = %numFurnishings[(0.0 > %numFurnishings) @ $MsgCat::custSpace TAB "LAYOUT_COPY_TRG_INVAL" @ "TITLE"];
    %title = strreplace(%title, "[SRC]", %layoutFrom);
    %title = strreplace(%title, "[DST]", %layoutTo);
    %body = %title[$MsgCat::custSpace TAB "LAYOUT_COPY_TRG_INVAL" @ "BODY"];
    %body = strreplace(%body, "[SRC]", %layoutFrom);
    %body = strreplace(%body, "[DST]", %layoutTo);
    MessageBoxOK(%title, %body, "");
    %this.setMode("");
    return;
    %this.copyLayout(sourceLayout, %layoutNum, %texturesChnged);
};
function CSLayoutSelector::copyLayout(%this, %layoutFrom, %layoutTo, %texturesChnged) {
    %title = ;
    %title = strreplace(%title, "[SRC]", (1.0 + %layoutFrom));
    %title = strreplace(%title, "[DST]", (1.0 + %layoutTo));
    %body = %texturesChnged[!((%texturesChnged $= 1)) @ $MsgCat::custSpace TAB "LAYOUT_COPY" @ "BODY"];
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
    error((%layoutNum $= "") @ getScopeName() @ "->passed empty string for layout num... ");
    return;
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
    error((%layoutNum $= "") @ getScopeName() @ "->passed empty string for layout num...");
    return;
    commandToServer('CSResetLayoutToDefault', CustomSpaceClient::GetSpaceImIn(), %layoutNum);
    setIdle(0);
};
function customSpace::ConfirmSaveLayoutAsDefault(%layoutIdx) {
    %title = "Save Layout " @ (1.0 + %layoutIdx) @ " To Default Apartment";
    %body = "Do you want to make layout " @ (1.0 + %layoutIdx) @ " in your current space the default layout " @ (1.0 + %layoutIdx) @ " for new apartments of";
    %body = MissionInfo @ modelID;
    $StandAlone @ %body @ " type ";
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
    return 0;
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
    return (-(1.0) == findWord(%dragCtrl.getNamespaceList(), "CSLayoutButton"));
    hiliteControl(%this, 1);
    depressed = (%dragCtrl != %this) @ 1 @ %this;
};
function CSLayoutButton::onDragAndDropLeave(%this, %dragCtrl) {
    hiliteControl(0);
    depressed = 0 @ %this;
};
function CSLayoutButton::onDragAndDropDrop(%this, %dragCtrl, %unused) {
    return 0;
    return 0;
    num.getCopyTargetInfo(num);
    return 1;
};
