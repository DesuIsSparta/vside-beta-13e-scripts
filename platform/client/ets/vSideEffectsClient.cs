function SceneObject::ve_onActivate(%this, %channel, %type)
{
    echoDebug(getScopeName() SPC "- effectType" SPC %type SPC "in channel" SPC %channel SPC "on object" SPC getDebugString(%this));
    return ;
}
function SceneObject::ve_onDeactivate(%this, %channel, %type)
{
    echoDebug(getScopeName() SPC "- effectType" SPC %type SPC "in channel" SPC %channel SPC "on object" SPC getDebugString(%this));
    return ;
}
function SceneObject::ve_OnChangedParams(%this, %channel, %type)
{
    echoDebug(getScopeName() SPC "- effectType" SPC %type SPC "in channel" SPC %channel SPC "on object" SPC getDebugString(%this));
    return ;
}
function SceneObject::ve_OnChangedTiming(%this, %channel, %type)
{
    echoDebug(getScopeName() SPC "- effectType" SPC %type SPC "in channel" SPC %channel SPC "on object" SPC getDebugString(%this));
    return ;
}
function SceneObject::ve_OnChangedUnknown(%this, %channel, %type)
{
    echoWarn(getScopeName() SPC "- effectType" SPC %type SPC "in channel" SPC %channel SPC "on object" SPC getDebugString(%this));
    return ;
}
