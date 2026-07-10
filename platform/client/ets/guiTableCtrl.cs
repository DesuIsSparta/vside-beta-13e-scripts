function GuiTableCtrl::Initialize(%this) {
    if (!isDefined("%this.initialized") || !%this.initialized) {
        %this.initialized = 1;
        %this.setProfile(GuiTableProfile);
        %this.dataRowCellProfile = GuiTableBodyCellProfile;
        %this.dataRowHilitedProfile = GuiTableBodyRowHilitedProfile;
        %this.dataRowUnhilitedProfile = GuiTableBodyRowUnhilitedProfile;
        %headerArray = %this.getHeaderArrayCtrl();
        %scroll = %this.getScrollCtrl();
        %bodyArray = %this.getBodyArrayCtrl();
        if (isObject(%bodyArray)) {
        }
        %bodyArrayContainer = "";
        %this.getBodyArrayCtrl().getParent();
        if (isObject(%headerArray)) {
            %this.doSetupColumnHeaders(%headerArray);
        }
        warn(getScopeName() @ " " @ "- missing gui table header array -" @ " " @ getTrace());
        if (isObject(%scroll)) {
            %this.doSetupBodyScroll(%scroll);
        }
        warn(getScopeName() @ " " @ "- missing gui table body scroll -" @ " " @ getTrace());
        if (isObject(%bodyArrayContainer)) {
            %this.doSetupBodyContainer(%bodyArrayContainer);
        }
        warn(getScopeName() @ " " @ "- missing gui table body array container -" @ " " @ getTrace());
        if (isObject(%bodyArray)) {
            %this.doSetupArrayOfRows(%bodyArray);
        }
        warn(getScopeName() @ " " @ "- missing gui table body array -" @ " " @ getTrace());
    }
};
function GuiTableCtrl::doSetupColumnHeaders(%this, %headerArray) {
    %headerArray.setProfile(GuiTableHeaderRowProfile);
    %this.setHeaderCellProfile(GuiTableHeaderCell_N_Profile);
    %this.setHeaderCellButtonProfile(GuiTableHeaderCellButtonProfile);
    %this.setHeaderCellMLTextProfile(GuiTableHeaderCellMLTextProfile);
};
function GuiTableCtrl::doSetupBodyScroll(%this, %scroll) {
    %scroll.setProfile(GuiTableScrollProfile);
    %scroll.modulationColor = "177 183 209 160";
};
function GuiTableCtrl::doSetupBodyContainer(%this, %container) {
    %container.setProfile(GuiDefaultProfile);
};
function GuiTableCtrl::doSetupArrayOfRows(%this, %arrayOfRows) {
    %arrayOfRows.setProfile(GuiDefaultProfile);
};
function GuiTableCtrl::doSetupRowGuiArray(%this, %rowArray) {
    %rowArray.setProfile(GuiTableBodyRowUnhilitedProfile);
};
function GuiTableBodyCellCtrl::doSetupBodyCellForText(%this, %mlTextCtrl) {
    %this.setProfile(GuiTableBodyCellProfile);
    %mlTextCtrl.setProfile(GuiTableBodyCellMLTextProfile);
};
function GuiTableBodyCellCtrl::doSetupBodyCellForImage(%this, %bitmapCtrl) {
    %this.setProfile(GuiTableBodyCellProfile);
    %bitmapCtrl.setProfile(GuiTableBodyCellBitmapProfile);
};
$gCurrentTableHeaderCellHighlight = "";
function GuiTableRowCtrl::onMouseEnterBounds(%this) {
};
function GuiTableRowCtrl::onMouseLeaveBounds(%this) {
};
function GuiTableHeaderCellButtonCtrl::onMouseEnterBounds(%this) {
    %headerCell = %this.getParent();
    if (!%headerCell.getParent().getParent().getDataTable().getColumnIsSortable(%headerCell.getParent().getObjectIndex(%headerCell))) {
        return;
    }
    if (isObject($gCurrentTableHeaderCellHighlight)) {
    }
    if (($gCurrentTableHeaderCellHighlight != %headerCell)) {
        $gCurrentTableHeaderCellHighlight.setProfile(GuiTableHeaderCell_N_Profile);
    }
    %headerCell.setProfile(GuiTableHeaderCell_H_Profile);
    $gCurrentTableHeaderCellHighlight = %headerCell;
};
function GuiTableHeaderCellButtonCtrl::onMouseLeaveBounds(%this) {
    %headerCell = %this.getParent();
    if (!%headerCell.getParent().getParent().getDataTable().getColumnIsSortable(%headerCell.getParent().getObjectIndex(%headerCell))) {
        return;
    }
    %this.getParent().setProfile(GuiTableHeaderCell_N_Profile);
    $gCurrentTableHeaderCellHighlight = "";
};
function GuiTableHeaderCellButtonCtrl::onMouseDown(%this) {
    %headerCell = %this.getParent();
    if ((%headerCell.getParent().getObjectIndex(%headerCell) == 0.0)) {
        return;
    }
    if (isObject($gCurrentTableHeaderCellHighlight)) {
    }
    if (($gCurrentTableHeaderCellHighlight != %headerCell)) {
        $gCurrentTableHeaderCellHighlight.setProfile(GuiTableHeaderCell_N_Profile);
    }
    %headerCell.setProfile(GuiTableHeaderCell_D_Profile);
    $gCurrentTableHeaderCellHighlight = %headerCell;
};
function GuiTableHeaderCellButtonCtrl::onMouseUp(%this) {
    %headerCell = %this.getParent();
    if ((%headerCell.getParent().getObjectIndex(%headerCell) == 0.0)) {
        return;
    }
    if (%this.pointInControl(%this.globalToLocal(Canvas.getCursorPos()))) {
        %headerCell.setProfile(GuiTableHeaderCell_H_Profile);
    }
    %headerCell.setProfile(GuiTableHeaderCell_N_Profile);
};
