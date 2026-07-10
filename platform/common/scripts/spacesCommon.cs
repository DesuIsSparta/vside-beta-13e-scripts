function SpaceDef::defaultValues(%this) {
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
    %this.notAllowedText = "Sorry [PLAYERFIRSTNAME], you're not allowed in [SHORTNAME] - [REASON].";
    %this.Visibility = "none";
    %this.storeID = "";
    %this.shoppingLongText = "";
    %this.contiguousSpaceName = "";
    %this.visitID = "";
    %this.partnerURL = "";
};
function spaces_Init() {
    safeEnsureScriptObject("SimGroup", "spaceDefsGroup");
    spaceDefs_Init();
};
function spaces_GetSpaceDef(%internalName, %createIfDNE) {
    %fullName = "SpaceDef_" @ %internalName;
    if (isObject(%fullName)) {
        return %fullName.getId();
    }
    if (!(%createIfDNE)) {
        return 0;
    }
    %spaceDef = new ScriptObject(%fullName) {
        class = "SpaceDef";
        internalName = %internalName;
    };
    %spaceDef.defaultValues();
    %spaceDef.add(spaceDefsGroup);
    return %spaceDef.getId();
};
function spaces_HasSpaceDef(%internalName) {
    %fullName = "SpaceDef_" @ %internalName;
    return isObject(%fullName);
};
function spaces_FindSpaceDefWithStoreID(%storeID) {
    %found = 0;
    %n = (spaceDefsGroup.getCount() - 1.0);
    if ((%n >= 0.0)) {
    }
    while ((%found == 0.0)) {
        %found = %n.getObject(spaceDefsGroup);
        if (!(%found.storeID $= %storeID)) {
            %found = 0;
        }
        %n = (%n - 1.0);
        if ((%n >= 0.0)) {
        }
    }
    return %found;
};
function SpaceDef::getInternalName(%this) {
    return getSubStr(%this.getName(), 9);
};
function initTokenSubstitutions() {
    %map = safeEnsureScriptObject("StringMap", "gTokenSubstitutionTable");
    if (0) {
    }
    if (%map.initialized) {
        return %map;
    }
    "          %player     .getShapeName()".put(%map, "[PLAYERNAME]");
    "firstWord(%player     .getShapeName())".put(%map, "[PLAYERFIRSTNAME]");
    "          %this       .lastReason".put(%map, "[REASON]");
    "          %this       .shortName".put(%map, "[SHORTNAME]");
    %map.initialized = 1;
    return %map;
};
function SpaceDef::doTokenSubstitution(%this, %dry, %player) {
    %map = initTokenSubstitutions();
    %wet = %dry;
    %n = (%map.size() - 1.0);
    while ((%n >= 0.0)) {
        %replaceThis = %n.getKey(%map);
        %withThis = %n.getValue(%map);
        %player = %player;
        %evalCmd = "%withThis = " @ %withThis @ ";";
        eval(%evalCmd);
        %wet = strreplace(%wet, %replaceThis, %withThis);
        %n = (%n - 1.0);
    }
    return %wet;
};
spaces_Init();
