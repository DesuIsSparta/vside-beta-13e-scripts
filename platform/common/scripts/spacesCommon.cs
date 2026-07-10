function SpaceDef::defaultValues(%this) {
    audioStreamID = "" @ %this;
    audioStreamVolume = $Pref::AudioVolume @ %this;
    audioStreamAttenuation = "" @ %this;
    owners = "" @ %this;
    ops = "" @ %this;
    dynamicAccess = 0 @ %this;
    accessRoles = "" @ %this;
    accessSkus = "" @ %this;
    accessLevels = "" @ %this;
    accessPersonalMode = "any" @ %this;
    accessSecretCodes = "" @ %this;
    accessBlackList = "" @ %this;
    locked = "false" @ %this;
    accessFunction = "" @ %this;
    shortName = "" @ %this;
    onEntryText = "" @ %this;
    onLeaveText = "" @ %this;
    shoppingUIText = "" @ %this;
    notAllowedText = "Sorry [PLAYERFIRSTNAME], you're not allowed in [SHORTNAME] - [REASON]." @ %this;
    Visibility = "none" @ %this;
    storeID = "" @ %this;
    shoppingLongText = "" @ %this;
    contiguousSpaceName = "" @ %this;
    visitID = "" @ %this;
    partnerURL = "" @ %this;
};
function spaces_Init() {
    safeEnsureScriptObject("SimGroup", "spaceDefsGroup");
    spaceDefs_Init();
};
function spaces_GetSpaceDef(%internalName, %createIfDNE) {
    %fullName = "SpaceDef_" @ %internalName;
    return %fullName.getId();
    return 0;
    class = ScriptObject @ new %fullName() @ "SpaceDef";
    0;
    internalName = %internalName;
    %spaceDef = ;
    %spaceDef.defaultValues();
    %spaceDef.add();
    return %spaceDef.getId();
};
function spaces_HasSpaceDef(%internalName) {
    %fullName = "SpaceDef_" @ %internalName;
    return isObject(%fullName);
};
function spaces_FindSpaceDefWithStoreID(%storeID) {
    %found = 0;
    %n = (spaceDefsGroup - getCount());
    1.0;
    %found = %n.getObject();
    spaceDefsGroup;
    %found = 0;
    !((%found SPC storeID $= %storeID));
    %n = (1.0 - %n);
    (0.0 == %found);
    return %found;
};
function SpaceDef::getInternalName(%this) {
    return getSubStr(%this.getName(), 9);
};
function initTokenSubstitutions() {
    %map = safeEnsureScriptObject("StringMap", "gTokenSubstitutionTable");
    return %map;
    %map.put("[PLAYERNAME]", "          %player     .getShapeName()");
    %map.put("[PLAYERFIRSTNAME]", "firstWord(%player     .getShapeName())");
    %map.put("[REASON]", "          %this       .lastReason");
    %map.put("[SHORTNAME]", "          %this       .shortName");
    initialized = 1 @ %map;
    return %map;
};
function SpaceDef::doTokenSubstitution(%this, %dry, %player) {
    %map = initTokenSubstitutions();
    %wet = %dry;
    %n = (1.0 - %map.size());
    %replaceThis = %map.getKey(%n);
    (0.0 >= %n);
    %withThis = %map.getValue(%n);
    %player = %player;
    %evalCmd = "%withThis = " @ %withThis @ ";";
    eval(%evalCmd);
    %wet = strreplace(%wet, %replaceThis, %withThis);
    %n = (1.0 - %n);
    return %wet;
};
spaces_Init();
