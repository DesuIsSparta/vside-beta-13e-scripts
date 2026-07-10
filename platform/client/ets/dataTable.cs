function DataTable::clear(%this)
{
    %this.removeRowsByIndex(0, %this.getRowCount());
}
function DataTable::hasColumnNamed(%this, %name)
{
    return %this.getColumnIndex(%name) >= 0;
}
