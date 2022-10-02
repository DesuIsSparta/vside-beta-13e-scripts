$MAX_FREE_BONE_BLENDS = 23;
$BB_FLAVOR_ONESHOT = 1;
$BB_FLAVOR_PINGPONG = 2;
$BB_FLAVOR_PINGPONG_SINE = 3;
$BB_FLAVOR_SETPOS = 4;
$BB_FLAVOR_LOOP = 5;
$BB_FLAVOR_HOLD_AT_N_REVERSE = 6;
$BB_FLAVOR_HOLD_AT_N_COMPLETE = 7;
$BB_FLAVOR_HOLD_AT_N_REMOVE = 8;
$BB_HEAD_LR = 0;
$BB_HEAD_UD = 1;
$BB_HEAD_TALK = 2;
$BB_HEAD_XRCST = 3;
$FIRST_FREE_BLEND_INDEX = 7;
$BB_UPPR_WAVE = 7;
$BB_UPPR_LOL = 8;
$BB_UPPR_CHUG = 9;
$BB_UPPR_CHEERS = 10;
$BB_UPPR_DANCEMIX = 11;
$BB_LOWR_DANCEMIX = 12;
$BB_UPPR_CHEERS2 = 13;
$BB_BODY_PIVOT = 14;
$BB_BODY_BEND_T0 = 15;
$BB_BODY_KISS = 16;
$BB_BODY_WALK = 17;
$BB_HEAD_KISS_LR = 18;
$BB_HEAD_KISS_UD = 19;
$BB_BODY_DANCE_WITH = 20;
$BB_UPPR_OMG = 21;
$BB_BODY_DANCE_WITH_ROLL = 22;
$BB_UPPR_FLIRT = 23;
$BB_UPPR_DRINK = 24;
$BB_UPPR_MICROPHONE = 25;
$BB_UPPR_TOAST = 26;
$BB_SIT_KISS_BEND_TO = 27;
$BB_SIT_KISS_PIVOT = 28;
$BB_UPPR_KISS = 29;
new ScriptObject(blendAnim_0)
{
    title = "waving";
    defaultPosition = 0;
    flavor = $BB_FLAVOR_ONESHOT;
    holdPosition = 1;
    sequence = "mwave";
    isReplacement = 1;
    attackTime = 0.1;
    decayTime = 0.9;
    attackRate = 3;
    decayRate = 3;
};
new ScriptObject(blendAnim_1)
{
    title = "laughing";
    defaultPosition = 0;
    flavor = $BB_FLAVOR_ONESHOT;
    holdPosition = 1;
    sequence = "mlol";
    isReplacement = 1;
    attackTime = 0.1;
    decayTime = 0.9;
    attackRate = 3;
    decayRate = 3;
};
new ScriptObject(blendAnim_2)
{
    title = "chug";
    defaultPosition = 0;
    flavor = $BB_FLAVOR_ONESHOT;
    holdPosition = 1;
    sequence = "mchug";
    isReplacement = 1;
    attackTime = 0.1;
    decayTime = 0.9;
    attackRate = 3;
    decayRate = 3;
};
new ScriptObject(blendAnim_3)
{
    title = "cheer";
    defaultPosition = 0;
    flavor = $BB_FLAVOR_ONESHOT;
    holdPosition = 1;
    sequence = "mcheer";
    isReplacement = 1;
    attackTime = 0.1;
    decayTime = 0.9;
    attackRate = 3;
    decayRate = 3;
};
new ScriptObject(blendAnim_4)
{
    title = "upper body dance";
    defaultPosition = 0;
    flavor = $BB_FLAVOR_ONESHOT;
    holdPosition = 1;
    sequence = "mupdnc";
    isReplacement = 1;
    attackTime = 0.25;
    decayTime = 0.95;
    attackRate = 1;
    decayRate = 1;
};
new ScriptObject(blendAnim_5)
{
    title = "lower body dance";
    defaultPosition = 0;
    flavor = $BB_FLAVOR_ONESHOT;
    holdPosition = 1;
    sequence = "mlowdnc";
    isReplacement = 0;
    attackTime = 0;
    decayTime = 0;
    attackRate = 1;
    decayRate = 1;
};
new ScriptObject(blendAnim_6)
{
    title = "cheer1";
    defaultPosition = 0;
    flavor = $BB_FLAVOR_ONESHOT;
    holdPosition = 1;
    sequence = "mcheer1";
    isReplacement = 1;
    attackTime = 0.1;
    decayTime = 0.9;
    attackRate = 1;
    decayRate = 1;
};
new ScriptObject(blendAnim_7)
{
    title = "pivot whole body";
    defaultPosition = 0.5;
    flavor = $BB_FLAVOR_SETPOS;
    holdPosition = 1;
    sequence = "mpiv";
    isReplacement = 0;
    attackTime = 1;
    decayTime = 1;
    attackRate = 1;
    decayRate = 1;
};
new ScriptObject(blendAnim_8)
{
    title = "bend to object";
    defaultPosition = 0.25;
    flavor = $BB_FLAVOR_SETPOS;
    holdPosition = 1;
    sequence = "mpitch";
    isReplacement = 0;
    attackTime = 0;
    decayTime = 0;
    attackRate = 2;
    decayRate = 2;
};
new ScriptObject(blendAnim_9)
{
    title = "upper body for kiss";
    defaultPosition = 0;
    flavor = $BB_FLAVOR_ONESHOT;
    holdPosition = 1;
    sequence = "mupkis";
    isReplacement = 1;
    attackTime = 1;
    decayTime = 0.95;
    attackRate = 1;
    decayRate = 1;
};
new ScriptObject(blendAnim_10)
{
    title = "hold mic";
    defaultPosition = 0;
    flavor = $BB_FLAVOR_HOLD_AT_N_REVERSE;
    holdPosition = 0.7;
    sequence = "mmic";
    isReplacement = 1;
    attackTime = 0.2;
    decayTime = 0.1;
    attackRate = 3;
    decayRate = 3;
};
new ScriptObject(blendAnim_11)
{
    title = "head LR for kiss";
    defaultPosition = 0.5;
    flavor = $BB_FLAVOR_SETPOS;
    holdPosition = 1;
    sequence = "head_LR";
    isReplacement = 0;
    attackTime = 0;
    decayTime = 0;
    attackRate = 2;
    decayRate = 1;
};
new ScriptObject(blendAnim_12)
{
    title = "head UD for Kiss";
    defaultPosition = 0.5;
    flavor = $BB_FLAVOR_SETPOS;
    holdPosition = 1;
    sequence = "head_UD";
    isReplacement = 0;
    attackTime = 0;
    decayTime = 0;
    attackRate = 2;
    decayRate = 1;
};
new ScriptObject(blendAnim_13)
{
    title = "dance with modifier";
    defaultPosition = 0.5;
    flavor = $BB_FLAVOR_SETPOS;
    holdPosition = 1;
    sequence = "mdip";
    isReplacement = 0;
    attackTime = 1;
    decayTime = 1;
    attackRate = 10;
    decayRate = 1;
};
new ScriptObject(blendAnim_14)
{
    title = "oh my god";
    defaultPosition = 0;
    flavor = $BB_FLAVOR_ONESHOT;
    holdPosition = 1;
    sequence = "momg";
    isReplacement = 1;
    attackTime = 0.25;
    decayTime = 0.8;
    attackRate = 1;
    decayRate = 3.5;
};
new ScriptObject(blendAnim_15)
{
    title = "dance with modifier roll";
    defaultPosition = 0.5;
    flavor = $BB_FLAVOR_SETPOS;
    holdPosition = 1;
    sequence = "mroll";
    isReplacement = 0;
    attackTime = 1;
    decayTime = 1;
    attackRate = 10;
    decayRate = 1;
};
new ScriptObject(blendAnim_16)
{
    title = "flirt";
    defaultPosition = 0;
    flavor = $BB_FLAVOR_ONESHOT;
    holdPosition = 1;
    sequence = "mflrt";
    isReplacement = 1;
    attackTime = 0.15;
    decayTime = 0.8;
    attackRate = 3.5;
    decayRate = 3.5;
};
new ScriptObject(blendAnim_17)
{
    title = "drink";
    defaultPosition = 0;
    flavor = $BB_FLAVOR_ONESHOT;
    holdPosition = 1;
    sequence = "mdrink";
    isReplacement = 1;
    attackTime = 0.16;
    decayTime = 0.85;
    attackRate = 0.5;
    decayRate = 0.5;
};
new ScriptObject(blendAnim_18)
{
    title = "up mic";
    defaultPosition = 0;
    flavor = $BB_FLAVOR_SETPOS;
    holdPosition = 0;
    sequence = "mmic";
    isReplacement = 1;
    attackTime = 0.2;
    decayTime = 0.1;
    attackRate = 2;
    decayRate = 3;
};
new ScriptObject(blendAnim_19)
{
    title = "toast";
    defaultPosition = 0;
    flavor = $BB_FLAVOR_ONESHOT;
    holdPosition = 1;
    sequence = "mtoast";
    isReplacement = 1;
    attackTime = 0.16;
    decayTime = 0.85;
    attackRate = 0.5;
    decayRate = 0.5;
};
new ScriptObject(blendAnim_20)
{
    title = "sitting bend to object";
    defaultPosition = 0.5;
    flavor = $BB_FLAVOR_SETPOS;
    holdPosition = 1;
    sequence = "msitbend";
    isReplacement = 0;
    attackTime = 0;
    decayTime = 0;
    attackRate = 5.5;
    decayRate = 1.5;
};
new ScriptObject(blendAnim_21)
{
    title = "sitting pivot";
    defaultPosition = 0.5;
    flavor = $BB_FLAVOR_SETPOS;
    holdPosition = 1;
    sequence = "msitpiv";
    isReplacement = 0;
    attackTime = 0;
    decayTime = 0;
    attackRate = 5.5;
    decayRate = 1.5;
};
new ScriptObject(blendAnim_22)
{
    title = "upper body kiss";
    defaultPosition = 0;
    flavor = $BB_FLAVOR_ONESHOT;
    holdPosition = 1;
    sequence = "mupkis";
    isReplacement = 1;
    attackTime = 0.1;
    decayTime = 0.9;
    attackRate = 3;
    decayRate = 3;
};
$gBlendAnimsTitlesMap = 0;
function Player::configBoneBlends(%this)
{
    if ($gBlendAnimsTitlesMap == 0)
    {
        $gBlendAnimsTitlesMap = safeEnsureScriptObject("StringMap", "");
    }
    %i = 0;
    while (%i < $MAX_FREE_BONE_BLENDS)
    {
        %index = %i + $FIRST_FREE_BLEND_INDEX;
        %sobj = "blendAnim_" @ %i;
        %this.configBoneBlendAnimation(%index, %sobj.sequence, %sobj.flavor, %sobj.defaultPosition, %sobj.attackTime, %sobj.decayTime, %sobj.attackRate, %sobj.decayRate, %sobj.isReplacement, %sobj.holdPosition);
        $gBlendAnimsTitlesMap.put(%sobj.title, %i);
        %i = %i + 1;
    }
}

function Player::getBoneBlendIndexFromTitle(%this, %title)
{
    if (!isObject($gBlendAnimsTitlesMap))
    {
        warn(getScopeName() SPC "- $gBlendAnimsTitlesMap not configured");
        %this.configBoneBlends();
    }
    %ret = $gBlendAnimsTitlesMap.get(%title);
    if (%ret $= "")
    {
        %ret = -1;
    }
    else
    {
        %ret = %ret + $FIRST_FREE_BLEND_INDEX;
    }
    return %ret;
}
function Player::triggerBlendAnimByTitle(%this, %title, %doit)
{
    %index = %this.getBoneBlendIndexFromTitle(%title);
    if (%index < 0)
    {
        error(getScopeName() SPC "- blend anim not found:" SPC %title);
        return ;
    }
    if (%this.isServerObject())
    {
        %this.triggerBoneBlendAnimation(%index, %doit, 0);
    }
    else
    {
        %this.triggerBlendAnim(%index, %doit);
    }
    return ;
}
