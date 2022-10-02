function SpaceDef::defaultValues(%this)
{
    %this.audioStreamID = "";
    %this.audioStreamVolume = $Pref::AudioVolume;
    %this.audioStreamAttenuation = "";
    %this.owners = "";
    %this.ops = "";
    %this.dynamicAccess = 0;
    %this.accessRoles = "";
    %this.accessSkus = "";
    %this.accessLevels = "";
    %this.accessPersonalMode = "any";
    %this.accessSecretCodes = "";
    %this.accessBlackList = "";
    %this.locked = "false";
    %this.accessFunction = "";
    %this.shortName = "";
    %this.onEntryText = "";
    %this.onLeaveText = "";
    %this.shoppingUIText = "";
    %this.notAllowedText = "Sorry [PLAYERFIRSTNAME], you\'re not allowed in [SHORTNAME] - [REASON].";
    %this.Visibility = "none";
    %this.storeID = "";
    %this.shoppingLongText = "";
    %this.contiguousSpaceName = "";
    %this.visitID = "";
    %this.partnerURL = "";
    return ;
}
function spaces_Init()
{
    safeEnsureScriptObject("SimGroup", "spaceDefsGroup");
    spaceDefs_Init();
    return ;
}
function spaces_GetSpaceDef(%internalName, %createIfDNE)
{
    %fullName = "SpaceDef_" @ %internalName;
    if (isObject(%fullName))
    {
        return %fullName.getId();
    }
    if (!%createIfDNE)
    {
        return 0;
    }
    %spaceDef = new ScriptObject(%fullName)
    {
        class = "SpaceDef";
        internalName = %internalName;
    };
    %spaceDef.defaultValues();
    spaceDefsGroup.add(%spaceDef);
    return %spaceDef.getId();
}
function spaces_HasSpaceDef(%internalName)
{
    %fullName = "SpaceDef_" @ %internalName;
    return isObject(%fullName);
}
function spaces_FindSpaceDefWithStoreID(%storeID)
{
    %found = 0;
    %n = spaceDefsGroup.getCount() - 1;
    while (%found == 0)
    {
        %found = spaceDefsGroup.getObject(%n);
        if (!(%found.storeID $= %storeID))
        {
            %found = 0;
        }
        %n = %n - 1;
    }
    return %found;
}
function SpaceDef::getInternalName(%this)
{
    return getSubStr(%this.getName(), 9);
}
function initTokenSubstitutions()
{
    %map = safeEnsureScriptObject("StringMap", "gTokenSubstitutionTable");
    if (0 && %map.initialized)
    {
        return %map;
    }
    %map.put("[PLAYERNAME]", "          %player     .getShapeName()");
    %map.put("[PLAYERFIRSTNAME]", "firstWord(%player     .getShapeName())");
    %map.put("[REASON]", "          %this       .lastReason");
    %map.put("[SHORTNAME]", "          %this       .shortName");
    %map.initialized = 1;
    return %map;
}
function SpaceDef::doTokenSubstitution(%this, %dry, %player)
{
    %map = initTokenSubstitutions();
    %wet = %dry;
    %n = %map.size() - 1;
    while (%n >= 0)
    {
        %replaceThis = %map.getKey(%n);
        %withThis = %map.getValue(%n);
        %player = %player;
        %evalCmd = "%withThis = " @ %withThis @ ";";
        eval(%evalCmd);
        %wet = strreplace(%wet, %replaceThis, %withThis);
        %n = %n - 1;
    }
    return %wet;
}
spaces_Init();

