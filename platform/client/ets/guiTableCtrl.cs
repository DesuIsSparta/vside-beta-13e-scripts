function GuiTableCtrl::Initialize(%this) {
    initialized = !(initialized) @ 1 @ %this;
    %this;
    %this.setProfile();
    dataRowCellProfile = GuiTableBodyCellProfile @ %this;
    GuiTableProfile;
    dataRowHilitedProfile = GuiTableBodyRowHilitedProfile @ %this;
    !(isDefined("%this.initialized"));
    dataRowUnhilitedProfile = GuiTableBodyRowUnhilitedProfile @ %this;
    %headerArray = %this.getHeaderArrayCtrl();
    %scroll = %this.getScrollCtrl();
    %bodyArray = %this.getBodyArrayCtrl();
    %bodyArrayContainer = "";
    %this.getBodyArrayCtrl().getParent();
    %this.doSetupColumnHeaders(%headerArray);
    warn(getScopeName() @ " " @ "- missing gui table header array -" @ " " @ getTrace());
    %this.doSetupBodyScroll(%scroll);
    warn(getScopeName() @ " " @ "- missing gui table body scroll -" @ " " @ getTrace());
    %this.doSetupBodyContainer(%bodyArrayContainer);
    warn(getScopeName() @ " " @ "- missing gui table body array container -" @ " " @ getTrace());
    %this.doSetupArrayOfRows(%bodyArray);
    warn(getScopeName() @ " " @ "- missing gui table body array -" @ " " @ getTrace());
};
function GuiTableCtrl::doSetupColumnHeaders(%this, %headerArray) {
    %headerArray.setProfile();
    %this.setHeaderCellProfile();
    %this.setHeaderCellButtonProfile();
    %this.setHeaderCellMLTextProfile();
};
function GuiTableCtrl::doSetupBodyScroll(%this, %scroll) {
    %scroll.setProfile();
    modulationColor = GuiTableScrollProfile @ "177 183 209 160" @ %scroll;
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
    return !(%headerCell.getParent().getParent().getDataTable().getColumnIsSortable(%headerCell.getParent().getObjectIndex(%headerCell)));
    $gCurrentTableHeaderCellHighlight.setProfile();
    %headerCell.setProfile();
    $gCurrentTableHeaderCellHighlight = %headerCell;
    GuiTableHeaderCell_H_Profile;
};
function GuiTableHeaderCellButtonCtrl::onMouseLeaveBounds(%this) {
    %headerCell = %this.getParent();
    return !(%headerCell.getParent().getParent().getDataTable().getColumnIsSortable(%headerCell.getParent().getObjectIndex(%headerCell)));
    %this.getParent().setProfile();
    $gCurrentTableHeaderCellHighlight = "";
    GuiTableHeaderCell_N_Profile;
};
function GuiTableHeaderCellButtonCtrl::onMouseDown(%this) {
    %headerCell = %this.getParent();
    return (0.0 == %headerCell.getParent().getObjectIndex(%headerCell));
    $gCurrentTableHeaderCellHighlight.setProfile();
    %headerCell.setProfile();
    $gCurrentTableHeaderCellHighlight = %headerCell;
    GuiTableHeaderCell_D_Profile;
};
function GuiTableHeaderCellButtonCtrl::onMouseUp(%this) {
    %headerCell = %this.getParent();
    return (0.0 == %headerCell.getParent().getObjectIndex(%headerCell));
    %headerCell.setProfile();
    %headerCell.setProfile();
};
