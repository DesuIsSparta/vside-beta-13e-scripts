function DataTable::clear(%this) {
    %this.removeRowsByIndex(0, %this.getRowCount());
};
function DataTable::hasColumnNamed(%this, %name) {
    return (0.0 >= %this.getColumnIndex(%name));
};
