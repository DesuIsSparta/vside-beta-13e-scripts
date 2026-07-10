function DataTable::clear(%this) {
    %this.getRowCount().removeRowsByIndex(%this, 0);
};
function DataTable::hasColumnNamed(%this, %name) {
    return (%name.getColumnIndex(%this) >= 0.0);
};
