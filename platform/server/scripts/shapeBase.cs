function ShapeBase::Damage(%this, %sourceObject, %position, %damage, %damageType) {
    %damageType.Damage(%this.getDataBlock(), %this, %sourceObject, %position, %damage);
};
function ShapeBase::setDamageDt(%this, %damageAmount, %damageType) {
    if (!(%obj.getState() $= "Dead")) {
        %damageType.Damage(%this, 0, "0 0 0", %damageAmount);
        %obj.damageSchedule = %damageType.schedule(%obj, 50, "setDamageDt", %damageAmount);
    }
    %obj.damageSchedule = "";
};
function ShapeBase::clearDamageDt(%this) {
    if (!(%obj.damageSchedule $= "")) {
        cancel(%obj.damageSchedule);
        %obj.damageSchedule = "";
    }
};
function ShapeBaseData::Damage(%this, %obj, %position, %unused, %unused, %damageType) {
};
