function DataTable::clear(%this)
{
    %this.removeRowsByIndex(0, %this.getRowCount());
    return ;
}
function DataTable::hasColumnNamed(%this, %name)
{
    return %this.getColumnIndex(%name) >= 0;
}
