function SceneObject::ve_onActivate(%this, %channel, %type)
{
    echoDebug(getScopeName() @ " " @ "- effectType" @ " " @ %type @ " " @ "in channel" @ " " @ %channel @ " " @ "on object" @ " " @ getDebugString(%this));
}
function SceneObject::ve_onDeactivate(%this, %channel, %type)
{
    echoDebug(getScopeName() @ " " @ "- effectType" @ " " @ %type @ " " @ "in channel" @ " " @ %channel @ " " @ "on object" @ " " @ getDebugString(%this));
}
function SceneObject::ve_OnChangedParams(%this, %channel, %type)
{
    echoDebug(getScopeName() @ " " @ "- effectType" @ " " @ %type @ " " @ "in channel" @ " " @ %channel @ " " @ "on object" @ " " @ getDebugString(%this));
}
function SceneObject::ve_OnChangedTiming(%this, %channel, %type)
{
    echoDebug(getScopeName() @ " " @ "- effectType" @ " " @ %type @ " " @ "in channel" @ " " @ %channel @ " " @ "on object" @ " " @ getDebugString(%this));
}
function SceneObject::ve_OnChangedUnknown(%this, %channel, %type)
{
    echoWarn(getScopeName() @ " " @ "- effectType" @ " " @ %type @ " " @ "in channel" @ " " @ %channel @ " " @ "on object" @ " " @ getDebugString(%this));
}
