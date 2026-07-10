function ShapeBase::setDisplayName(%this, %name) {
    %name.setShapeName(%this);
};
function ShapeBase::getDisplayName(%this) {
    return %this.getShapeName();
};
