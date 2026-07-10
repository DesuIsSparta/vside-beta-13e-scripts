function GuiTableCtrl::Initialize(%this) {
    if (!(isDefined("%this.initialized"))) {
    }
    if (!(%this.initialized)) {
        %this.initialized = 1;
        %this.setProfile();
        %this.dataRowCellProfile = GuiTableProfile @ GuiTableBodyCellProfile;
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
    %headerArray.setProfile();
    %this.setHeaderCellProfile();
    %this.setHeaderCellButtonProfile();
    %this.setHeaderCellMLTextProfile();
};
function GuiTableCtrl::doSetupBodyScroll(%this, %scroll) {
    %scroll.setProfile();
    %scroll.modulationColor = GuiTableScrollProfile @ "177 183 209 160";
};
function GuiTableCtrl::doSetupBodyContainer(%this, %container) {
    %container.setProfile();
};
function GuiTableCtrl::doSetupArrayOfRows(%this, %arrayOfRows) {
    %arrayOfRows.setProfile();
};
function GuiTableCtrl::doSetupRowGuiArray(%this, %rowArray) {
    %rowArray.setProfile();
};
function GuiTableBodyCellCtrl::doSetupBodyCellForText(%this, %mlTextCtrl) {
    %this.setProfile();
    %mlTextCtrl.setProfile();
};
function GuiTableBodyCellCtrl::doSetupBodyCellForImage(%this, %bitmapCtrl) {
    %this.setProfile();
    %bitmapCtrl.setProfile();
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
        $gCurrentTableHeaderCellHighlight.setProfile();
    }
    %headerCell.setProfile();
    $gCurrentTableHeaderCellHighlight = %headerCell;
    GuiTableHeaderCell_H_Profile;
};
function GuiTableHeaderCellButtonCtrl::onMouseLeaveBounds(%this) {
    %headerCell = %this.getParent();
    if (!(%headerCell.getObjectIndex(%headerCell.getParent()).getColumnIsSortable(%headerCell.getParent().getParent().getDataTable()))) {
        return;
    }
    %this.getParent().setProfile();
    $gCurrentTableHeaderCellHighlight = "";
    GuiTableHeaderCell_N_Profile;
};
function GuiTableHeaderCellButtonCtrl::onMouseDown(%this) {
    %headerCell = %this.getParent();
    if ((%headerCell.getObjectIndex(%headerCell.getParent()) == 0.0)) {
        return;
    }
    if (isObject($gCurrentTableHeaderCellHighlight)) {
    }
    if (($gCurrentTableHeaderCellHighlight != %headerCell)) {
        $gCurrentTableHeaderCellHighlight.setProfile();
    }
    %headerCell.setProfile();
    $gCurrentTableHeaderCellHighlight = %headerCell;
    GuiTableHeaderCell_D_Profile;
};
function GuiTableHeaderCellButtonCtrl::onMouseUp(%this) {
    %headerCell = %this.getParent();
    if ((%headerCell.getObjectIndex(%headerCell.getParent()) == 0.0)) {
        return;
    }
    if (Canvas.getCursorPos().globalToLocal(%this).pointInControl(%this)) {
        %headerCell.setProfile();
    }
    %headerCell.setProfile();
};
