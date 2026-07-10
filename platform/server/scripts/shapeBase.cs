function ShapeBase::Damage(%this, %sourceObject, %position, %damage, %damageType) {
    %this.getDataBlock().Damage(%this, %sourceObject, %position, %damage, %damageType);
};
function ShapeBase::setDamageDt(%this, %damageAmount, %damageType) {
    if (!(%obj.getState() $= "Dead")) {
        %this.Damage(0, "0 0 0", %damageAmount, %damageType);
        damageSchedule = %obj.schedule(50, "setDamageDt", %damageAmount, %damageType) @ %obj;
    }
    damageSchedule = "" @ %obj;
};
function ShapeBase::clearDamageDt(%this) {
    if (!(%obj SPC damageSchedule $= "")) {
        cancel(damageSchedule);
        damageSchedule = %obj @ "" @ %obj;
    }
};
function ShapeBaseData::Damage(%this, %obj, %position, %unused, %unused, %damageType) {
};
