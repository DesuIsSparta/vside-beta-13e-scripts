function GuiTableCtrl::Initialize(%this)
{
    if (!isDefined("%this.initialized") && !(%this.initialized))
    {
        %this.initialized = 1;
        %this.setProfile(GuiTableProfile);
        %this.dataRowCellProfile = GuiTableBodyCellProfile;
        %this.dataRowHilitedProfile = GuiTableBodyRowHilitedProfile;
        %this.dataRowUnhilitedProfile = GuiTableBodyRowUnhilitedProfile;
        %headerArray = %this.getHeaderArrayCtrl();
        %scroll = %this.getScrollCtrl();
        %bodyArray = %this.getBodyArrayCtrl();
        %bodyArrayContainer = isObject(%bodyArray) ? %this.getBodyArrayCtrl().getParent() : "";
        if (isObject(%headerArray))
        {
            %this.doSetupColumnHeaders(%headerArray);
        }
        else
        {
            warn(getScopeName() SPC "- missing gui table header array -" SPC getTrace());
        }
        if (isObject(%scroll))
        {
            %this.doSetupBodyScroll(%scroll);
        }
        else
        {
            warn(getScopeName() SPC "- missing gui table body scroll -" SPC getTrace());
        }
        if (isObject(%bodyArrayContainer))
        {
            %this.doSetupBodyContainer(%bodyArrayContainer);
        }
        else
        {
            warn(getScopeName() SPC "- missing gui table body array container -" SPC getTrace());
        }
        if (isObject(%bodyArray))
        {
            %this.doSetupArrayOfRows(%bodyArray);
        }
        else
        {
            warn(getScopeName() SPC "- missing gui table body array -" SPC getTrace());
        }
    }
    return ;
}
function GuiTableCtrl::doSetupColumnHeaders(%this, %headerArray)
{
    %headerArray.setProfile(GuiTableHeaderRowProfile);
    %this.setHeaderCellProfile(GuiTableHeaderCell_N_Profile);
    %this.setHeaderCellButtonProfile(GuiTableHeaderCellButtonProfile);
    %this.setHeaderCellMLTextProfile(GuiTableHeaderCellMLTextProfile);
    return ;
}
function GuiTableCtrl::doSetupBodyScroll(%this, %scroll)
{
    %scroll.setProfile(GuiTableScrollProfile);
    %scroll.modulationColor = "177 183 209 160";
    return ;
}
function GuiTableCtrl::doSetupBodyContainer(%this, %container)
{
    %container.setProfile(GuiDefaultProfile);
    return ;
}
function GuiTableCtrl::doSetupArrayOfRows(%this, %arrayOfRows)
{
    %arrayOfRows.setProfile(GuiDefaultProfile);
    return ;
}
function GuiTableCtrl::doSetupRowGuiArray(%this, %rowArray)
{
    %rowArray.setProfile(GuiTableBodyRowUnhilitedProfile);
    return ;
}
function GuiTableBodyCellCtrl::doSetupBodyCellForText(%this, %mlTextCtrl)
{
    %this.setProfile(GuiTableBodyCellProfile);
    %mlTextCtrl.setProfile(GuiTableBodyCellMLTextProfile);
    return ;
}
function GuiTableBodyCellCtrl::doSetupBodyCellForImage(%this, %bitmapCtrl)
{
    %this.setProfile(GuiTableBodyCellProfile);
    %bitmapCtrl.setProfile(GuiTableBodyCellBitmapProfile);
    return ;
}
$gCurrentTableHeaderCellHighlight = "";
function GuiTableRowCtrl::onMouseEnterBounds(%this)
{
    return ;
}
function GuiTableRowCtrl::onMouseLeaveBounds(%this)
{
    return ;
}
function GuiTableHeaderCellButtonCtrl::onMouseEnterBounds(%this)
{
    %headerCell = %this.getParent();
    if (!%headerCell.getParent().getParent().getDataTable().getColumnIsSortable(%headerCell.getParent().getObjectIndex(%headerCell)))
    {
        return ;
    }
    if (isObject($gCurrentTableHeaderCellHighlight) && ($gCurrentTableHeaderCellHighlight != %headerCell))
    {
        $gCurrentTableHeaderCellHighlight.setProfile(GuiTableHeaderCell_N_Profile);
    }
    %headerCell.setProfile(GuiTableHeaderCell_H_Profile);
    $gCurrentTableHeaderCellHighlight = %headerCell;
    return ;
}
function GuiTableHeaderCellButtonCtrl::onMouseLeaveBounds(%this)
{
    %headerCell = %this.getParent();
    if (!%headerCell.getParent().getParent().getDataTable().getColumnIsSortable(%headerCell.getParent().getObjectIndex(%headerCell)))
    {
        return ;
    }
    %this.getParent().setProfile(GuiTableHeaderCell_N_Profile);
    $gCurrentTableHeaderCellHighlight = "";
    return ;
}
function GuiTableHeaderCellButtonCtrl::onMouseDown(%this)
{
    %headerCell = %this.getParent();
    if (%headerCell.getParent().getObjectIndex(%headerCell) == 0)
    {
        return ;
    }
    if (isObject($gCurrentTableHeaderCellHighlight) && ($gCurrentTableHeaderCellHighlight != %headerCell))
    {
        $gCurrentTableHeaderCellHighlight.setProfile(GuiTableHeaderCell_N_Profile);
    }
    %headerCell.setProfile(GuiTableHeaderCell_D_Profile);
    $gCurrentTableHeaderCellHighlight = %headerCell;
    return ;
}
function GuiTableHeaderCellButtonCtrl::onMouseUp(%this)
{
    %headerCell = %this.getParent();
    if (%headerCell.getParent().getObjectIndex(%headerCell) == 0)
    {
        return ;
    }
    if (%this.pointInControl(%this.globalToLocal(Canvas.getCursorPos())))
    {
        %headerCell.setProfile(GuiTableHeaderCell_H_Profile);
    }
    else
    {
        %headerCell.setProfile(GuiTableHeaderCell_N_Profile);
    }
    return ;
}
