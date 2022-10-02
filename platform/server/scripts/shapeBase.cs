function ShapeBase::Damage(%this, %sourceObject, %position, %damage, %damageType)
{
    %this.getDataBlock().Damage(%this, %sourceObject, %position, %damage, %damageType);
    return ;
}
function ShapeBase::setDamageDt(%this, %damageAmount, %damageType)
{
    if (!(%obj.getState() $= "Dead"))
    {
        %this.Damage(0, "0 0 0", %damageAmount, %damageType);
        %obj.damageSchedule = %obj.schedule(50, "setDamageDt", %damageAmount, %damageType);
    }
    else
    {
        %obj.damageSchedule = "";
    }
    return ;
}
function ShapeBase::clearDamageDt(%this)
{
    if (!(%obj.damageSchedule $= ""))
    {
        cancel(%obj.damageSchedule);
        %obj.damageSchedule = "";
    }
    return ;
}
function ShapeBaseData::Damage(%this, %obj, %position, %unused, %unused, %damageType)
{
    return ;
}
