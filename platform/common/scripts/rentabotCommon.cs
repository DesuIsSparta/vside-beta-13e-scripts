function rentabot_getCoreName(%name)
{
    if (!rentabot_isRentabotName(%name))
    {
        return %name;
    }
    %len = strlen(%name);
    %cn = getSubStr(%name, 1, %len - 2);
    return %cn;
}
function rentabot_isRentabotName(%name)
{
    return hasPrefix(%name, "[") && hasSuffix(%name, "]");
}
function rentabot_makeRentabotName(%name)
{
    if (!rentabot_isRentabotName(%name))
    {
        %name = "[" @ %name @ "]";
    }
    return %name;
}
