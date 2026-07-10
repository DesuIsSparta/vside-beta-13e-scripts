function ShapeBase::Damage(%this, %sourceObject, %position, %damage, %damageType) {
    %this.getDataBlock().Damage(%this, %sourceObject, %position, %damage, %damageType);
};
function ShapeBase::setDamageDt(%this, %damageAmount, %damageType) {
    %this.Damage(0, "0 0 0", %damageAmount, %damageType);
    damageSchedule = !((%obj.getState() $= "Dead")) @ %obj.schedule(50, "setDamageDt", %damageAmount, %damageType) @ %obj;
    damageSchedule = "" @ %obj;
};
function ShapeBase::clearDamageDt(%this) {
    cancel(damageSchedule);
    damageSchedule = %obj @ "" @ %obj;
    !((%obj SPC damageSchedule $= ""));
};
function ShapeBaseData::Damage(%this, %obj, %position, %unused, %unused, %damageType) {
};
