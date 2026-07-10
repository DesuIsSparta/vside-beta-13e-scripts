function GuiTableCtrl::Initialize(%this) {
    if (!(isDefined("%this.initialized"))) {
    }
    if (!(%this.initialized)) {
        %this.initialized = 1;
        GuiTableProfile.setProfile(%this);
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
            %headerArray.doSetupColumnHeaders(%this);
        }
        warn(getScopeName() @ " " @ "- missing gui table header array -" @ " " @ getTrace());
        if (isObject(%scroll)) {
            %scroll.doSetupBodyScroll(%this);
        }
        warn(getScopeName() @ " " @ "- missing gui table body scroll -" @ " " @ getTrace());
        if (isObject(%bodyArrayContainer)) {
            %bodyArrayContainer.doSetupBodyContainer(%this);
        }
        warn(getScopeName() @ " " @ "- missing gui table body array container -" @ " " @ getTrace());
        if (isObject(%bodyArray)) {
            %bodyArray.doSetupArrayOfRows(%this);
        }
        warn(getScopeName() @ " " @ "- missing gui table body array -" @ " " @ getTrace());
    }
};
function GuiTableCtrl::doSetupColumnHeaders(%this, %headerArray) {
    GuiTableHeaderRowProfile.setProfile(%headerArray);
    GuiTableHeaderCell_N_Profile.setHeaderCellProfile(%this);
    GuiTableHeaderCellButtonProfile.setHeaderCellButtonProfile(%this);
    GuiTableHeaderCellMLTextProfile.setHeaderCellMLTextProfile(%this);
};
function GuiTableCtrl::doSetupBodyScroll(%this, %scroll) {
    GuiTableScrollProfile.setProfile(%scroll);
    %scroll.modulationColor = "177 183 209 160";
};
function GuiTableCtrl::doSetupBodyContainer(%this, %container) {
    GuiDefaultProfile.setProfile(%container);
};
function GuiTableCtrl::doSetupArrayOfRows(%this, %arrayOfRows) {
    GuiDefaultProfile.setProfile(%arrayOfRows);
};
function GuiTableCtrl::doSetupRowGuiArray(%this, %rowArray) {
    GuiTableBodyRowUnhilitedProfile.setProfile(%rowArray);
};
function GuiTableBodyCellCtrl::doSetupBodyCellForText(%this, %mlTextCtrl) {
    GuiTableBodyCellProfile.setProfile(%this);
    GuiTableBodyCellMLTextProfile.setProfile(%mlTextCtrl);
};
function GuiTableBodyCellCtrl::doSetupBodyCellForImage(%this, %bitmapCtrl) {
    GuiTableBodyCellProfile.setProfile(%this);
    GuiTableBodyCellBitmapProfile.setProfile(%bitmapCtrl);
};
$gCurrentTableHeaderCellHighlight = "";
function GuiTableRowCtrl::onMouseEnterBounds(%this) {
};
function GuiTableRowCtrl::onMouseLeaveBounds(%this) {
};
function GuiTableHeaderCellButtonCtrl::onMouseEnterBounds(%this) {
    %headerCell = %this.getParent();
    if (!(%headerCell.getObjectIndex(%headerCell.getParent()).getColumnIsSortable(%headerCell.getParent().getParent().getDataTable()))) {
        return;
    }
    if (isObject($gCurrentTableHeaderCellHighlight)) {
    }
    if (($gCurrentTableHeaderCellHighlight != %headerCell)) {
        GuiTableHeaderCell_N_Profile.setProfile($gCurrentTableHeaderCellHighlight);
    }
    GuiTableHeaderCell_H_Profile.setProfile(%headerCell);
    $gCurrentTableHeaderCellHighlight = %headerCell;
};
function GuiTableHeaderCellButtonCtrl::onMouseLeaveBounds(%this) {
    %headerCell = %this.getParent();
    if (!(%headerCell.getObjectIndex(%headerCell.getParent()).getColumnIsSortable(%headerCell.getParent().getParent().getDataTable()))) {
        return;
    }
    GuiTableHeaderCell_N_Profile.setProfile(%this.getParent());
    $gCurrentTableHeaderCellHighlight = "";
};
function GuiTableHeaderCellButtonCtrl::onMouseDown(%this) {
    %headerCell = %this.getParent();
    if ((%headerCell.getObjectIndex(%headerCell.getParent()) == 0.0)) {
        return;
    }
    if (isObject($gCurrentTableHeaderCellHighlight)) {
    }
    if (($gCurrentTableHeaderCellHighlight != %headerCell)) {
        GuiTableHeaderCell_N_Profile.setProfile($gCurrentTableHeaderCellHighlight);
    }
    GuiTableHeaderCell_D_Profile.setProfile(%headerCell);
    $gCurrentTableHeaderCellHighlight = %headerCell;
};
function GuiTableHeaderCellButtonCtrl::onMouseUp(%this) {
    %headerCell = %this.getParent();
    if ((%headerCell.getObjectIndex(%headerCell.getParent()) == 0.0)) {
        return;
    }
    if (Canvas.getCursorPos().globalToLocal(%this).pointInControl(%this)) {
        GuiTableHeaderCell_H_Profile.setProfile(%headerCell);
    }
    GuiTableHeaderCell_N_Profile.setProfile(%headerCell);
};
