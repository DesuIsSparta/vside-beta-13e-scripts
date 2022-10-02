function initializeAnimationMaps()
{
    initializeAnimationMap(animationMapFH, "f", "h");
    initializeAnimationMap(animationMapFI, "f", "i");
    initializeAnimationMap(animationMapFP, "f", "p");
    initializeAnimationMap(animationMapFB, "f", "b");
    initializeAnimationMap(animationMapMH, "m", "h");
    initializeAnimationMap(animationMapMI, "m", "i");
    initializeAnimationMap(animationMapMP, "m", "p");
    initializeAnimationMap(animationMapMB, "m", "b");
    initializeAnimationMapAnimal(animationMapDH, "d", "h");
    initializeAnimationMapAnimal(animationMapDI, "d", "i");
    initializeAnimationMapAnimal(animationMapDP, "d", "p");
    makeAnimationMapZombie(animationMapMZ, animationMapMP, "m");
    makeAnimationMapZombie(animationMapFZ, animationMapFP, "f");
    makeAnimationMapSkate(animationMapMK, animationMapMP, "m");
    makeAnimationMapSkate(animationMapFK, animationMapFP, "f");
    makeAnimationMapSwim(animationMapMW, animationMapMP, "m");
    makeAnimationMapSwim(animationMapFW, animationMapFP, "f");
    makeAnimationMapTyra(animationMapMT, animationMapMH, "m");
    makeAnimationMapTyra(animationMapFT, animationMapFH, "f");
    makeAnimationMapSuperTyra(animationMapMS, animationMapMT, "m");
    makeAnimationMapSuperTyra(animationMapFS, animationMapFT, "f");
    makeAnimationMapSumo(animationMapMO, animationMapMP, "m");
    makeAnimationMapSumo(animationMapFO, animationMapFP, "f");
    makeAnimationMapPillow(animationMapML, animationMapMP, "m");
    makeAnimationMapPillow(animationMapFL, animationMapFP, "f");
    makeAnimationMapProps(animationMapMY, animationMapMP, "m");
    makeAnimationMapProps(animationMapFY, animationMapFP, "f");
    makeAnimationMapCross(animationMapMX, animationMapMP, "m");
    makeAnimationMapCross(animationMapFX, animationMapFP, "f");
    makeAnimationMapBottleShake(animationMapME, animationMapMP, "m");
    makeAnimationMapBottleShake(animationMapFE, animationMapFP, "f");
    makeAnimationMapBottle(animationMapMF, animationMapMP, "m");
    makeAnimationMapBottle(animationMapFF, animationMapFP, "f");
    makeAnimationMapDrinkStem(animationMapMU, animationMapMP, "m");
    makeAnimationMapDrinkStem(animationMapFU, animationMapFP, "f");
    makeAnimationMapDrinkCup(animationMapMV, animationMapMP, "m");
    makeAnimationMapDrinkCup(animationMapFV, animationMapFP, "f");
    makeAnimationMapRedMana(animationMapMJ, animationMapMP, "m");
    makeAnimationMapRedMana(animationMapFJ, animationMapFP, "f");
    makeAnimationMapBlueMana(animationMapMM, animationMapMP, "m");
    makeAnimationMapBlueMana(animationMapFM, animationMapFP, "f");
    return ;
}
function makeAnimationMapSuperTyra(%map, %src, %gender)
{
    copyAnimationMap(%map, %src);
    %map.put("run", %gender @ "ntyrwlk");
    %map.put("root", %gender @ "ntyidl1a");
    %map.put("back", %gender @ "hwlkb1");
    %map.put("sml", %gender @ "ntyrsml");
    return ;
}
function makeAnimationMapTyra(%map, %src, %gender)
{
    copyAnimationMap(%map, %src);
    %map.put("run", %gender @ "ntyrwlk");
    %map.put("root", %gender @ "ntyidl1a");
    %map.put("back", %gender @ "hwlkb1");
    %map.put("sml", %gender @ "ntyrsml");
    return ;
}
function makeAnimationMapSkate(%map, %src, %gender)
{
    copyAnimationMap(%map, %src);
    %map.put("run", %gender @ "kwlkf1");
    %map.put("side", %gender @ "ksde");
    %map.put("back", %gender @ "kwlkb1");
    return ;
}
function makeAnimationMapSwim(%map, %src, %gender)
{
    copyAnimationMap(%map, %src);
    %map.put("root", %gender @ "nswmidl1");
    %map.put("run", %gender @ "nswmf1");
    %map.put("side", %gender @ "nswmsde");
    %map.put("back", %gender @ "nswmb1");
    return ;
}
function makeAnimationMapZombie(%map, %src, %gender)
{
    copyAnimationMap(%map, %src);
    %map.put("run", %gender @ "nzwlk");
    %map.put("root", %gender @ "nzidl1");
    return ;
}
function makeAnimationMapInstrument(%gender, %genre, %rootAnim, %runAnim, %sideAnim, %backAnim, %jumpAnim)
{
    %src = %gender $= "f" ? animationMapFP : animationMapMP;
    %animationMapName = "animationMap" @ %gender @ %genre;
    if (isObject(%animationMapName))
    {
        %map = %animationMapName.getId();
    }
    else
    {
        %map = new StringMap(%animationMapName);
    }
    copyAnimationMap(%map, %src);
    %map.put("root", %gender @ %rootAnim);
    %map.put("run", %gender @ %runAnim);
    %map.put("side", %gender @ %sideAnim);
    %map.put("back", %gender @ %backAnim);
    %map.put("jump", %gender @ %jumpAnim);
    return %map;
}
function makeAnimationMapSumo(%map, %src, %gender)
{
    copyAnimationMap(%map, %src);
    %map.put("root", %gender @ "nsumoidle");
    %map.put("run", %gender @ "nsumowlkf");
    %map.put("side", %gender @ "nsumowlks");
    %map.put("back", %gender @ "nsumowlkb");
    %map.put("jump", %gender @ "nsumojmp");
    return ;
}
function makeAnimationMapPillow(%map, %src, %gender)
{
    copyAnimationMap(%map, %src);
    %map.put("root", %gender @ "npwidle");
    %map.put("run", %gender @ "npwwlkf");
    %map.put("side", %gender @ "npwsde");
    %map.put("back", %gender @ "npwwlkb");
    %map.put("jump", %gender @ "npwjmp");
    return ;
}
function makeAnimationMapProps(%map, %src, %gender)
{
    copyAnimationMap(%map, %src);
    %map.put("root", %gender @ "y" @ "idl1a");
    %map.put("idl1a", %gender @ "y" @ "idl1a");
    %map.put("idl1b", %gender @ "y" @ "idl1a");
    %map.put("idl1c", %gender @ "y" @ "idl1a");
    %map.put("idl1d", %gender @ "y" @ "idl1a");
    %map.put("idl2a", %gender @ "y" @ "idl1a");
    %map.put("idl2b", %gender @ "y" @ "idl1a");
    %map.put("idl2c", %gender @ "y" @ "idl1a");
    %map.put("idl2d", %gender @ "y" @ "idl1a");
    %map.put("idl3a", %gender @ "y" @ "idl1a");
    %map.put("idl3b", %gender @ "y" @ "idl1a");
    %map.put("idl3c", %gender @ "y" @ "idl1a");
    %map.put("idl3d", %gender @ "y" @ "idl1a");
    %map.put("run", %gender @ "y" @ "wlkf1");
    %map.put("wlkf1", %gender @ "y" @ "wlkf1");
    %map.put("side", %gender @ "y" @ "sde");
    %map.put("back", %gender @ "y" @ "wlkb1");
    %map.put("wlkb1", %gender @ "y" @ "wlkb1");
    %map.put("jump", %gender @ "y" @ "jmp");
    %map.put("cidl1a", %gender @ "y" @ "cidl1a");
    %map.put("cidl2a", %gender @ "y" @ "cidl2a");
    %map.put("lidl1a", %gender @ "y" @ "lidl1a");
    %map.put("lidl2a", %gender @ "y" @ "lidl2a");
    %map.put("lidl3a", %gender @ "y" @ "lidl3a");
    return ;
}
function makeAnimationMapCross(%map, %src, %gender)
{
    copyAnimationMap(%map, %src);
    %map.put("root", %gender @ "x" @ "idl1a");
    %map.put("run", %gender @ "x" @ "wlkf1");
    %map.put("wlkf1", %gender @ "x" @ "wlkf1");
    %map.put("back", %gender @ "x" @ "wlkb1");
    %map.put("wlkb1", %gender @ "x" @ "wlkb1");
    %map.put("cidl1a", %gender @ "x" @ "cidl1a");
    %map.put("cidl2a", %gender @ "x" @ "cidl2a");
    %map.put("lidl1a", %gender @ "x" @ "lidl1a");
    %map.put("lidl2a", %gender @ "x" @ "lidl2a");
    return ;
}
function makeAnimationMapBottleShake(%map, %src, %gender)
{
    copyAnimationMap(%map, %src);
    %map.put("root", %gender @ "e" @ "idl1a");
    %map.put("idl1a", %gender @ "e" @ "idl1a");
    %map.put("idl1b", %gender @ "e" @ "idl1a");
    %map.put("idl1c", %gender @ "e" @ "idl1a");
    %map.put("idl1d", %gender @ "e" @ "idl1a");
    %map.put("idl2a", %gender @ "e" @ "idl1a");
    %map.put("idl2b", %gender @ "e" @ "idl1a");
    %map.put("idl2c", %gender @ "e" @ "idl1a");
    %map.put("idl2d", %gender @ "e" @ "idl1a");
    %map.put("idl3a", %gender @ "e" @ "idl1a");
    %map.put("idl3b", %gender @ "e" @ "idl1a");
    %map.put("idl3c", %gender @ "e" @ "idl1a");
    %map.put("idl3d", %gender @ "e" @ "idl1a");
    %map.put("run", %gender @ "e" @ "wlkf1");
    %map.put("wlkf1", %gender @ "e" @ "wlkf1");
    %map.put("side", %gender @ "e" @ "sde");
    %map.put("back", %gender @ "e" @ "wlkb1");
    %map.put("wlkb1", %gender @ "e" @ "wlkb1");
    %map.put("cidl1a", %gender @ "e" @ "cidl1a");
    %map.put("cidl2a", %gender @ "e" @ "cidl2a");
    %map.put("lidl1a", %gender @ "e" @ "lidl1a");
    %map.put("lidl2a", %gender @ "e" @ "lidl2a");
    return ;
}
function makeAnimationMapBottle(%map, %src, %gender)
{
    copyAnimationMap(%map, %src);
    %map.put("root", %gender @ "f" @ "idl1a");
    %map.put("idl1a", %gender @ "f" @ "idl1a");
    %map.put("idl1b", %gender @ "f" @ "idl1a");
    %map.put("idl1c", %gender @ "f" @ "idl1a");
    %map.put("idl1d", %gender @ "f" @ "idl1a");
    %map.put("idl2a", %gender @ "f" @ "idl1a");
    %map.put("idl2b", %gender @ "f" @ "idl1a");
    %map.put("idl2c", %gender @ "f" @ "idl1a");
    %map.put("idl2d", %gender @ "f" @ "idl1a");
    %map.put("idl3a", %gender @ "f" @ "idl1a");
    %map.put("idl3b", %gender @ "f" @ "idl1a");
    %map.put("idl3c", %gender @ "f" @ "idl1a");
    %map.put("idl3d", %gender @ "f" @ "idl1a");
    %map.put("run", %gender @ "f" @ "wlkf1");
    %map.put("side", %gender @ "f" @ "sde");
    %map.put("wlkf1", %gender @ "f" @ "wlkf1");
    %map.put("back", %gender @ "f" @ "wlkb1");
    %map.put("wlkb1", %gender @ "f" @ "wlkb1");
    %map.put("cidl1a", %gender @ "f" @ "cidl1a");
    %map.put("cidl2a", %gender @ "f" @ "cidl2a");
    %map.put("lidl1a", %gender @ "f" @ "lidl1a");
    %map.put("lidl2a", %gender @ "f" @ "lidl2a");
    return ;
}
function makeAnimationMapDrinkStem(%map, %src, %gender)
{
    copyAnimationMap(%map, %src);
    %map.put("root", %gender @ "u" @ "idl1a");
    %map.put("idl1a", %gender @ "u" @ "idl1a");
    %map.put("idl1b", %gender @ "u" @ "idl1a");
    %map.put("idl1c", %gender @ "u" @ "idl1a");
    %map.put("idl1d", %gender @ "u" @ "idl1a");
    %map.put("idl2a", %gender @ "u" @ "idl1a");
    %map.put("idl2b", %gender @ "u" @ "idl1a");
    %map.put("idl2c", %gender @ "u" @ "idl1a");
    %map.put("idl2d", %gender @ "u" @ "idl1a");
    %map.put("idl3a", %gender @ "u" @ "idl1a");
    %map.put("idl3b", %gender @ "u" @ "idl1a");
    %map.put("idl3c", %gender @ "u" @ "idl1a");
    %map.put("idl3d", %gender @ "u" @ "idl1a");
    %map.put("run", %gender @ "u" @ "wlkf1");
    %map.put("side", %gender @ "u" @ "sde");
    %map.put("wlkf1", %gender @ "u" @ "wlkf1");
    %map.put("back", %gender @ "u" @ "wlkb1");
    %map.put("wlkb1", %gender @ "u" @ "wlkb1");
    %map.put("cidl1a", %gender @ "u" @ "cidl1a");
    %map.put("cidl2a", %gender @ "u" @ "cidl2a");
    %map.put("lidl1a", %gender @ "u" @ "lidl1a");
    %map.put("lidl2a", %gender @ "u" @ "lidl2a");
    return ;
}
function makeAnimationMapDrinkCup(%map, %src, %gender)
{
    copyAnimationMap(%map, %src);
    %map.put("root", %gender @ "v" @ "idl1a");
    %map.put("idl1a", %gender @ "v" @ "idl1a");
    %map.put("idl1b", %gender @ "v" @ "idl1a");
    %map.put("idl1c", %gender @ "v" @ "idl1a");
    %map.put("idl1d", %gender @ "v" @ "idl1a");
    %map.put("idl2a", %gender @ "v" @ "idl1a");
    %map.put("idl2b", %gender @ "v" @ "idl1a");
    %map.put("idl2c", %gender @ "v" @ "idl1a");
    %map.put("idl2d", %gender @ "v" @ "idl1a");
    %map.put("idl3a", %gender @ "v" @ "idl1a");
    %map.put("idl3b", %gender @ "v" @ "idl1a");
    %map.put("idl3c", %gender @ "v" @ "idl1a");
    %map.put("idl3d", %gender @ "v" @ "idl1a");
    %map.put("run", %gender @ "v" @ "wlkf1");
    %map.put("side", %gender @ "v" @ "sde");
    %map.put("wlkf1", %gender @ "v" @ "wlkf1");
    %map.put("back", %gender @ "v" @ "wlkb1");
    %map.put("wlkb1", %gender @ "v" @ "wlkb1");
    %map.put("cidl1a", %gender @ "v" @ "cidl1a");
    %map.put("cidl2a", %gender @ "v" @ "cidl2a");
    %map.put("lidl1a", %gender @ "v" @ "lidl1a");
    %map.put("lidl2a", %gender @ "v" @ "lidl2a");
    return ;
}
function makeAnimationMapRedMana(%map, %src, %gender)
{
    copyAnimationMap(%map, %src);
    %map.put("root", %gender @ "j" @ "idl1a");
    %map.put("idl1a", %gender @ "j" @ "idl1a");
    %map.put("idl1b", %gender @ "j" @ "idl1a");
    %map.put("idl1c", %gender @ "j" @ "idl1a");
    %map.put("idl1d", %gender @ "j" @ "idl1a");
    %map.put("idl2a", %gender @ "j" @ "idl1a");
    %map.put("idl2b", %gender @ "j" @ "idl1a");
    %map.put("idl2c", %gender @ "j" @ "idl1a");
    %map.put("idl2d", %gender @ "j" @ "idl1a");
    %map.put("idl3a", %gender @ "j" @ "idl1a");
    %map.put("idl3b", %gender @ "j" @ "idl1a");
    %map.put("idl3c", %gender @ "j" @ "idl1a");
    %map.put("idl3d", %gender @ "j" @ "idl1a");
    %map.put("run", %gender @ "j" @ "wlkf1");
    %map.put("wlkf1", %gender @ "j" @ "wlkf1");
    %map.put("side", %gender @ "j" @ "sde");
    %map.put("back", %gender @ "j" @ "wlkb1");
    %map.put("wlkb1", %gender @ "j" @ "wlkb1");
    %map.put("cidl1a", %gender @ "e" @ "cidl1a");
    %map.put("cidl2a", %gender @ "e" @ "cidl2a");
    %map.put("lidl1a", %gender @ "e" @ "lidl1a");
    %map.put("lidl2a", %gender @ "e" @ "lidl2a");
    return ;
}
function makeAnimationMapBlueMana(%map, %src, %gender)
{
    copyAnimationMap(%map, %src);
    %map.put("root", %gender @ "m" @ "idl1a");
    %map.put("idl1a", %gender @ "m" @ "idl1a");
    %map.put("idl1b", %gender @ "m" @ "idl1a");
    %map.put("idl1c", %gender @ "m" @ "idl1a");
    %map.put("idl1d", %gender @ "m" @ "idl1a");
    %map.put("idl2a", %gender @ "m" @ "idl1a");
    %map.put("idl2b", %gender @ "m" @ "idl1a");
    %map.put("idl2c", %gender @ "m" @ "idl1a");
    %map.put("idl2d", %gender @ "m" @ "idl1a");
    %map.put("idl3a", %gender @ "m" @ "idl1a");
    %map.put("idl3b", %gender @ "m" @ "idl1a");
    %map.put("idl3c", %gender @ "m" @ "idl1a");
    %map.put("idl3d", %gender @ "m" @ "idl1a");
    %map.put("run", %gender @ "m" @ "wlkf1");
    %map.put("wlkf1", %gender @ "m" @ "wlkf1");
    %map.put("side", %gender @ "m" @ "sde");
    %map.put("back", %gender @ "m" @ "wlkb1");
    %map.put("wlkb1", %gender @ "m" @ "wlkb1");
    %map.put("cidl1a", %gender @ "e" @ "cidl1a");
    %map.put("cidl2a", %gender @ "e" @ "cidl2a");
    %map.put("lidl1a", %gender @ "e" @ "lidl1a");
    %map.put("lidl2a", %gender @ "e" @ "lidl2a");
    return ;
}
function initializeAnimationMapAnimal(%map, %gender, %genre)
{
    if (!isObject(%map))
    {
        return;
    }
    new StringMap(%map);
    if (isObject(MissionCleanup))
    {
        MissionCleanup.add(%map);
    }
    %map.put("root", %gender @ %genre @ "idle1");
    %map.put("run", %gender @ %genre @ "wlkf");
    %map.put("back", %gender @ %genre @ "wlkf");
    %map.put("idl1a", %gender @ %genre @ "idle1");
    %map.put("idl1b", %gender @ %genre @ "idle2");
    %map.put("idl1c", %gender @ %genre @ "idle3");
    %map.put("idl1d", %gender @ %genre @ "idle4");
    return ;
}
function initializeAnimationMap(%map, %gender, %genre)
{
    if (!isObject(%map))
    {
        return;
    }
    new StringMap(%map);
    if (isObject(MissionCleanup))
    {
        MissionCleanup.add(%map);
    }
    addGenreSpecificAnimations(%map, %gender, %genre);
    addGenreNeutralAnimations(%map, %gender);
    return ;
}
function copyAnimationMap(%map, %src)
{
    if (!isObject(%map))
    {
        return;
    }
    new StringMap(%map);
    if (isObject(MissionCleanup))
    {
        MissionCleanup.add(%map);
    }
    %map.duplicate(%src);
    return ;
}
$gKnownAnimationTags = "dance";
function addAnimationToMap(%map, %mapThis, %toThis, %tags)
{
    %map.put(%mapThis, %toThis);
    %n = getWordCount(%tags);
    while (%n >= 0)
    {
        %tag = getWord(%tags, %n);
        if (%tag $= "")
        {
            continue;
        }
        if (!hasWord($gKnownAnimationTags, %tag))
        {
            error(getScopeName() SPC "- unknown animation tag:\"" @ %tag @ "\"." SPC getTrace());
        }
        else
        {
            safeEnsureScriptObject("StringMap", "gAnimationTags");
            %animTags = gAnimationTags.get(%toThis);
            if (hasWord(%animTags, %tag))
            {
            }
            else
            {
                %animTags = %tag SPC %animTags;
                gAnimationTags.put(%toThis, %animTags);
            }
        }
        %n = %n - 1;
    }
}

function addGenreSpecificAnimations(%map, %gender, %genre)
{
    addAnimationToMap(%map, "root", %gender @ %genre @ "idl1a", "");
    addAnimationToMap(%map, "run", %gender @ %genre @ "wlkf1", "");
    addAnimationToMap(%map, "back", %gender @ %genre @ "wlkb1", "");
    addAnimationToMap(%map, "wlkf1", %gender @ %genre @ "wlkf1", "");
    addAnimationToMap(%map, "wlkb1", %gender @ %genre @ "wlkb1", "");
    addAnimationToMap(%map, "idl1a", %gender @ %genre @ "idl1a", "");
    addAnimationToMap(%map, "idl1b", %gender @ %genre @ "idl1b", "");
    addAnimationToMap(%map, "idl1c", %gender @ %genre @ "idl1c", "");
    addAnimationToMap(%map, "idl1d", %gender @ %genre @ "idl1d", "");
    addAnimationToMap(%map, "idl2a", %gender @ %genre @ "idl2a", "");
    addAnimationToMap(%map, "idl2b", %gender @ %genre @ "idl2b", "");
    addAnimationToMap(%map, "idl2c", %gender @ %genre @ "idl2c", "");
    addAnimationToMap(%map, "idl2d", %gender @ %genre @ "idl2d", "");
    addAnimationToMap(%map, "idl3a", %gender @ %genre @ "idl3a", "");
    addAnimationToMap(%map, "idl3b", %gender @ %genre @ "idl3b", "");
    addAnimationToMap(%map, "idl3c", %gender @ %genre @ "idl3c", "");
    addAnimationToMap(%map, "idl3d", %gender @ %genre @ "idl3d", "");
    addAnimationToMap(%map, "cidl1a", %gender @ %genre @ "cidl1a", "");
    addAnimationToMap(%map, "cidl2a", %gender @ %genre @ "cidl2a", "");
    addAnimationToMap(%map, "lidl1a", %gender @ %genre @ "lidl1a", "");
    addAnimationToMap(%map, "lidl2a", %gender @ %genre @ "lidl2a", "");
    addAnimationToMap(%map, "dnc1", %gender @ %genre @ "dnc1", "dance");
    addAnimationToMap(%map, "dnc2", %gender @ %genre @ "dnc2", "dance");
    addAnimationToMap(%map, "dnc3", %gender @ %genre @ "dnc3", "dance");
    addAnimationToMap(%map, "dnc4", %gender @ %genre @ "dnc4", "dance");
    addAnimationToMap(%map, "sml", %gender @ %genre @ "sml", "");
    addAnimationToMap(%map, "ang", %gender @ %genre @ "ang", "");
    addAnimationToMap(%map, "ttth", %gender @ %genre @ "ttth", "");
    addAnimationToMap(%map, "wve", %gender @ %genre @ "wve", "");
    addAnimationToMap(%map, "cool", %gender @ %genre @ "cool", "");
    addAnimationToMap(%map, "shhh", %gender @ %genre @ "shhh", "");
    addAnimationToMap(%map, "flr", %gender @ %genre @ "flr", "");
    addAnimationToMap(%map, "lol", %gender @ %genre @ "lol", "");
    addAnimationToMap(%map, "bow", %gender @ %genre @ "bow", "");
    addAnimationToMap(%map, "jump", %gender @ "njmp", "");
    if ((%gender $= "f") && (%genre $= "p"))
    {
        addAnimationToMap(%map, "root", %gender @ %genre @ "idl1b", "");
    }
    return ;
}
function addGenreNeutralAnimations(%map, %gender)
{
    addAnimationToMap(%map, "sumopowerdefend", %gender @ "nsumopowerdefend", "");
    addAnimationToMap(%map, "jump", %gender @ "njmp", "");
    addAnimationToMap(%map, "standjump", %gender @ "njmp", "");
    addAnimationToMap(%map, "land", %gender @ "nland", "");
    addAnimationToMap(%map, "side", %gender @ "nsde", "");
    addAnimationToMap(%map, "fall", %gender @ "nfall", "");
    addAnimationToMap(%map, "cent", %gender @ "ncent", "");
    addAnimationToMap(%map, "cext", %gender @ "ncext", "");
    addAnimationToMap(%map, "lent", %gender @ "nlent", "");
    addAnimationToMap(%map, "lext", %gender @ "nlext", "");
    addAnimationToMap(%map, "tlk", %gender @ "ntlk", "");
    addAnimationToMap(%map, "lsn", %gender @ "nlsn", "");
    addAnimationToMap(%map, "sad", %gender @ "nsad", "");
    addAnimationToMap(%map, "spr", %gender @ "nspr", "");
    addAnimationToMap(%map, "srd", %gender @ "nsrd", "");
    addAnimationToMap(%map, "cry", %gender @ "ncry", "");
    addAnimationToMap(%map, "cryhard", %gender @ "ncryhard", "");
    addAnimationToMap(%map, "ilve", %gender @ "nilve", "");
    addAnimationToMap(%map, "kiss", %gender @ "nkiss", "");
    addAnimationToMap(%map, "emb", %gender @ "nemb", "");
    addAnimationToMap(%map, "vom", %gender @ "nvom", "");
    addAnimationToMap(%map, "cnf", %gender @ "ncnf", "");
    addAnimationToMap(%map, "slpy", %gender @ "nslpy", "");
    addAnimationToMap(%map, "whw", %gender @ "nwhw", "");
    addAnimationToMap(%map, "rotfl", %gender @ "nrotfl", "");
    addAnimationToMap(%map, "apls", %gender @ "napls", "");
    addAnimationToMap(%map, "apls01", %gender @ "napls01", "");
    addAnimationToMap(%map, "apls02", %gender @ "napls02", "");
    addAnimationToMap(%map, "apls03", %gender @ "napls03", "");
    addAnimationToMap(%map, "doh", %gender @ "ndoh", "");
    addAnimationToMap(%map, "wait", %gender @ "nwait", "");
    addAnimationToMap(%map, "busy", %gender @ "nbusy", "");
    addAnimationToMap(%map, "nlst", %gender @ "nnlst", "");
    addAnimationToMap(%map, "think", %gender @ "nthink", "");
    addAnimationToMap(%map, "rent", %gender @ "nrent", "");
    addAnimationToMap(%map, "rext", %gender @ "nrext", "");
    addAnimationToMap(%map, "ridl1", %gender @ "nridl1", "");
    addAnimationToMap(%map, "widl1", %gender @ "nwidl1", "");
    addAnimationToMap(%map, "htidl1", %gender @ "nhtidl1", "");
    addAnimationToMap(%map, "went2", %gender @ "nwent2", "");
    addAnimationToMap(%map, "wext2", %gender @ "nwext2", "");
    addAnimationToMap(%map, "widl2", %gender @ "nwidl2", "");
    addAnimationToMap(%map, "sidl1", %gender @ "nsidl1", "");
    addAnimationToMap(%map, "sent", %gender @ "nsent", "");
    addAnimationToMap(%map, "sext", %gender @ "nsext", "");
    addAnimationToMap(%map, "sitlsn", %gender @ "nsitlsn", "");
    addAnimationToMap(%map, "sittlk", %gender @ "nsittlk", "");
    addAnimationToMap(%map, "hdnc1", %gender @ "hdnc1", "dance");
    addAnimationToMap(%map, "hdnc2", %gender @ "hdnc2", "dance");
    addAnimationToMap(%map, "idnc1", %gender @ "idnc1", "dance");
    addAnimationToMap(%map, "idnc2", %gender @ "idnc2", "dance");
    addAnimationToMap(%map, "pdnc1", %gender @ "pdnc1", "dance");
    addAnimationToMap(%map, "pdnc2", %gender @ "pdnc2", "dance");
    addAnimationToMap(%map, "hdnc3", %gender @ "hdnc3", "dance");
    addAnimationToMap(%map, "hdnc4", %gender @ "hdnc4", "dance");
    addAnimationToMap(%map, "idnc3", %gender @ "idnc3", "dance");
    addAnimationToMap(%map, "idnc4", %gender @ "idnc4", "dance");
    addAnimationToMap(%map, "pdnc3", %gender @ "pdnc3", "dance");
    addAnimationToMap(%map, "pdnc4", %gender @ "pdnc4", "dance");
    addAnimationToMap(%map, "lsnidl1", %gender @ "nlsnidl1", "");
    addAnimationToMap(%map, "lsnent", %gender @ "nlsnent", "");
    addAnimationToMap(%map, "lsnext", %gender @ "nlsnext", "");
    addAnimationToMap(%map, "blidl1", %gender @ "nblidl1", "");
    addAnimationToMap(%map, "blent", %gender @ "nblent", "");
    addAnimationToMap(%map, "blext", %gender @ "nblext", "");
    addAnimationToMap(%map, "sidl2", %gender @ "nsidl2", "");
    addAnimationToMap(%map, "sidl3", %gender @ "nsidl3", "");
    addAnimationToMap(%map, "rlidl1", %gender @ "nrlidl1", "");
    addAnimationToMap(%map, "lidl3a", %gender @ "nlidl3a", "");
    addAnimationToMap(%map, "zsidl1", %gender @ "nzsidl1", "");
    addAnimationToMap(%map, "zsent", %gender @ "nzsent", "");
    addAnimationToMap(%map, "zsext", %gender @ "nzsext", "");
    addAnimationToMap(%map, "plent", %gender @ "nplent", "");
    addAnimationToMap(%map, "plext", %gender @ "nplext", "");
    addAnimationToMap(%map, "plidl1", %gender @ "nplidl1", "");
    addAnimationToMap(%map, "sidl4", %gender @ "nsidl4", "");
    addAnimationToMap(%map, "hdncb1", %gender @ "nhdncb1", "dance");
    addAnimationToMap(%map, "hdncb2", %gender @ "nhdncb2", "dance");
    addAnimationToMap(%map, "hdncb3", %gender @ "nhdncb3", "dance");
    addAnimationToMap(%map, "hdncb4", %gender @ "nhdncb4", "dance");
    addAnimationToMap(%map, "zwlk", %gender @ "nzwlk", "");
    addAnimationToMap(%map, "shoo", %gender @ "nshoo", "");
    addAnimationToMap(%map, "shrug", %gender @ "nshrug", "");
    addAnimationToMap(%map, "point", %gender @ "npoint", "");
    addAnimationToMap(%map, "yes", %gender @ "nyes", "");
    addAnimationToMap(%map, "no", %gender @ "nno", "");
    addAnimationToMap(%map, "here", %gender @ "nhere", "");
    addAnimationToMap(%map, "thmup", %gender @ "nthmup", "");
    addAnimationToMap(%map, "thmdn", %gender @ "nthmdn", "");
    addAnimationToMap(%map, "skfst", %gender @ "nskfst", "");
    addAnimationToMap(%map, "clbidl1", %gender @ "nclbidl1", "");
    addAnimationToMap(%map, "clbent", %gender @ "nclbent", "");
    addAnimationToMap(%map, "clbext", %gender @ "nclbext", "");
    addAnimationToMap(%map, "ph01", %gender @ "nph01", "");
    addAnimationToMap(%map, "ph02", %gender @ "nph02", "");
    addAnimationToMap(%map, "ph03", %gender @ "nph03", "");
    addAnimationToMap(%map, "ph04", %gender @ "nph04", "");
    addAnimationToMap(%map, "ph05", %gender @ "nph05", "");
    addAnimationToMap(%map, "ph06", %gender @ "nph06", "");
    addAnimationToMap(%map, "ph07", %gender @ "nph07", "");
    addAnimationToMap(%map, "ph08", %gender @ "nph08", "");
    addAnimationToMap(%map, "huger", %gender @ "nhuger", "");
    addAnimationToMap(%map, "hugee", %gender @ "nhugee", "");
    addAnimationToMap(%map, "sitks", %gender @ "nsitks", "");
    addAnimationToMap(%map, "gtrglridl1", %gender @ "ngtrglridl1", "");
    addAnimationToMap(%map, "gtrglrwlkf01", %gender @ "ngtrglrwlkf01", "");
    addAnimationToMap(%map, "gtrglrwlkb01", %gender @ "ngtrglrwlkb01", "");
    addAnimationToMap(%map, "gtrglrside01", %gender @ "ngtrglrside01", "");
    addAnimationToMap(%map, "gtrglrjmp01", %gender @ "ngtrglrjmp01", "");
    addAnimationToMap(%map, "gtrglr1e", %gender @ "ngtrglr1e", "");
    addAnimationToMap(%map, "gtrglr2e", %gender @ "ngtrglr2e", "");
    addAnimationToMap(%map, "gtrglr3e", %gender @ "ngtrglr3e", "");
    addAnimationToMap(%map, "gtrglr4e", %gender @ "ngtrglr4e", "");
    addAnimationToMap(%map, "gtrglr5e", %gender @ "ngtrglr5e", "");
    addAnimationToMap(%map, "gtrglr6e", %gender @ "ngtrglr6e", "");
    addAnimationToMap(%map, "gtrglr7e", %gender @ "ngtrglr7e", "");
    addAnimationToMap(%map, "gtrglr8a", %gender @ "ngtrglr8a", "");
    addAnimationToMap(%map, "gtrglr9a", %gender @ "ngtrglr9a", "");
    addAnimationToMap(%map, "gtrglr10a", %gender @ "ngtrglr10a", "");
    addAnimationToMap(%map, "gtrglr11a", %gender @ "ngtrglr11a", "");
    addAnimationToMap(%map, "gtrglr12a", %gender @ "ngtrglr12a", "");
    addAnimationToMap(%map, "gtrglr13a", %gender @ "ngtrglr13a", "");
    addAnimationToMap(%map, "gtrglr14a", %gender @ "ngtrglr14a", "");
    addAnimationToMap(%map, "gtrglr15b", %gender @ "ngtrglr15b", "");
    addAnimationToMap(%map, "gtrglr16b", %gender @ "ngtrglr16b", "");
    addAnimationToMap(%map, "gtrglr17b", %gender @ "ngtrglr17b", "");
    addAnimationToMap(%map, "gtrglr18b", %gender @ "ngtrglr18b", "");
    addAnimationToMap(%map, "gtrglr19b", %gender @ "ngtrglr19b", "");
    addAnimationToMap(%map, "gtrglr20b", %gender @ "ngtrglr20b", "");
    addAnimationToMap(%map, "go01", %gender @ "ngo01", "dance");
    addAnimationToMap(%map, "go02", %gender @ "ngo02", "dance");
    addAnimationToMap(%map, "go03", %gender @ "ngo03", "dance");
    addAnimationToMap(%map, "go04", %gender @ "ngo04", "dance");
    addAnimationToMap(%map, "go05", %gender @ "ngo05", "dance");
    addAnimationToMap(%map, "go06", %gender @ "ngo06", "dance");
    addAnimationToMap(%map, "go07", %gender @ "ngo07", "dance");
    addAnimationToMap(%map, "go08", %gender @ "ngo08", "dance");
    addAnimationToMap(%map, "swsh", %gender @ "nswsh", "");
    addAnimationToMap(%map, "spidl1", %gender @ "nspidl1", "");
    addAnimationToMap(%map, "srain", %gender @ "nsrain", "");
    addAnimationToMap(%map, "tsitdwn", %gender @ "ntsitdwn", "");
    addAnimationToMap(%map, "tsitidl01", %gender @ "ntsitidl01", "");
    addAnimationToMap(%map, "tsitup", %gender @ "ntsitup", "");
    addAnimationToMap(%map, "wh", %gender @ "nwh", "");
    addAnimationToMap(%map, "wf", %gender @ "nwf", "");
    addAnimationToMap(%map, "wt", %gender @ "nwt", "");
    addAnimationToMap(%map, "wb", %gender @ "nwb", "");
    addAnimationToMap(%map, "ws", %gender @ "nws", "");
    addAnimationToMap(%map, "vside", %gender @ "nvside", "");
    addAnimationToMap(%map, "boo", %gender @ "nboo", "");
    addAnimationToMap(%map, "dhtoe", %gender @ "ndhtoe", "dance");
    addAnimationToMap(%map, "dlnwit", %gender @ "ndlnwit", "dance");
    addAnimationToMap(%map, "dshfle", %gender @ "ndshfle", "dance");
    addAnimationToMap(%map, "dslpsld", %gender @ "ndslpsld", "dance");
    addAnimationToMap(%map, "dwlkit", %gender @ "ndwlkit", "dance");
    addAnimationToMap(%map, "dxhop", %gender @ "ndxhop", "dance");
    addAnimationToMap(%map, "d2step", %gender @ "nd2step", "dance");
    addAnimationToMap(%map, "d2stepx", %gender @ "nd2stepx", "dance");
    addAnimationToMap(%map, "dvstepb", %gender @ "ndvstepb", "dance");
    addAnimationToMap(%map, "brshft", %gender @ "nbrshft", "dance");
    addAnimationToMap(%map, "jbdnc01", %gender @ "njbdnc01", "dance");
    addAnimationToMap(%map, "jbdnc02", %gender @ "njbdnc02", "dance");
    addAnimationToMap(%map, "jbdnc03", %gender @ "njbdnc03", "dance");
    addAnimationToMap(%map, "jbdnc04", %gender @ "njbdnc04", "dance");
    addAnimationToMap(%map, "jbdnc05", %gender @ "njbdnc05", "dance");
    addAnimationToMap(%map, "jbdnc06", %gender @ "njbdnc06", "dance");
    addAnimationToMap(%map, "jbdnc07", %gender @ "njbdnc07", "dance");
    addAnimationToMap(%map, "jbdnc08", %gender @ "njbdnc08", "dance");
    addAnimationToMap(%map, "jbdnc10", %gender @ "njbdnc10", "dance");
    addAnimationToMap(%map, "rsbloop", %gender @ "nrsbloop", "");
    addAnimationToMap(%map, "rsbreaux", %gender @ "nrsbreaux", "");
    addAnimationToMap(%map, "rsbsham", %gender @ "nrsbsham", "");
    addAnimationToMap(%map, "rsbbeaux", %gender @ "nrsbbeaux", "");
    addAnimationToMap(%map, "mjbtrst1", %gender @ "nmjbtrst1", "dance");
    addAnimationToMap(%map, "mjbtrst2", %gender @ "nmjbtrst2", "dance");
    addAnimationToMap(%map, "mjclaw1", %gender @ "nmjclaw1", "dance");
    addAnimationToMap(%map, "mjclaw2", %gender @ "nmjclaw2", "dance");
    addAnimationToMap(%map, "mjhtilt", %gender @ "nmjhtilt", "dance");
    addAnimationToMap(%map, "mjhturn", %gender @ "nmjhturn", "dance");
    addAnimationToMap(%map, "mjmstep", %gender @ "nmjmstep", "dance");
    addAnimationToMap(%map, "mjptrst", %gender @ "nmjptrst", "dance");
    addAnimationToMap(%map, "mjpvt", %gender @ "nmjpvt", "dance");
    addAnimationToMap(%map, "mjshrug", %gender @ "nmjshrug", "dance");
    addAnimationToMap(%map, "mjsstep", %gender @ "nmjsstep", "dance");
    addAnimationToMap(%map, "mjsstep2", %gender @ "nmjsstep2", "dance");
    addAnimationToMap(%map, "mjtclap1", %gender @ "nmjtclap1", "dance");
    addAnimationToMap(%map, "mjtclap2", %gender @ "nmjtclap2", "dance");
    addAnimationToMap(%map, "mjzpose", %gender @ "nmjzpose", "dance");
    addAnimationToMap(%map, "mjlih", %gender @ "nmjlih", "dance");
    addAnimationToMap(%map, "mjspin", %gender @ "nmjspin", "dance");
    addAnimationToMap(%map, "mjzstep", %gender @ "nmjzstep", "dance");
    addAnimationToMap(%map, "hi5ee", %gender @ "nhi5ee", "");
    addAnimationToMap(%map, "hi5er", %gender @ "nhi5er", "");
    addAnimationToMap(%map, "lyidl1", %gender @ "nlyidl1", "");
    addAnimationToMap(%map, "sumoidle", %gender @ "nsumoidle", "");
    addAnimationToMap(%map, "sumowlkf", %gender @ "nsumowlkf", "");
    addAnimationToMap(%map, "sumowlkb", %gender @ "nsumowlkb", "");
    addAnimationToMap(%map, "sumowlks", %gender @ "nsumowlks", "");
    addAnimationToMap(%map, "sumojmp", %gender @ "nsumojmp", "");
    addAnimationToMap(%map, "sumojabattack", %gender @ "nsumojabattack", "");
    addAnimationToMap(%map, "sumopowerattack", %gender @ "nsumopowerattack", "");
    addAnimationToMap(%map, "sumobbattack", %gender @ "nsumobbattack", "");
    addAnimationToMap(%map, "sumojabdefend", %gender @ "nsumojabdefend", "");
    addAnimationToMap(%map, "sumopowerdefend", %gender @ "nsumopowerdefend", "");
    addAnimationToMap(%map, "sumoshortstun", %gender @ "nsumoshortstun", "");
    addAnimationToMap(%map, "sumolongstun", %gender @ "nsumolongstun", "");
    addAnimationToMap(%map, "sumotaunt01", %gender @ "nsumotaunt01", "");
    addAnimationToMap(%map, "sumotaunt02", %gender @ "nsumotaunt02", "");
    addAnimationToMap(%map, "dgoth01", %gender @ "ndgoth01", "dance");
    addAnimationToMap(%map, "dgoth02", %gender @ "ndgoth02", "dance");
    addAnimationToMap(%map, "dgoth03", %gender @ "ndgoth03", "dance");
    addAnimationToMap(%map, "dgoth04", %gender @ "ndgoth04", "dance");
    addAnimationToMap(%map, "dgoth05", %gender @ "ndgoth05", "dance");
    addAnimationToMap(%map, "bhop", %gender @ "nbhop", "");
    addAnimationToMap(%map, "bedentr", %gender @ "nbedentr", "");
    addAnimationToMap(%map, "bedextr", %gender @ "nbedextr", "");
    addAnimationToMap(%map, "bedextl", %gender @ "nbedextl", "");
    addAnimationToMap(%map, "bedentl", %gender @ "nbedentl", "");
    addAnimationToMap(%map, "bedslpbk", %gender @ "nbedslpbk", "");
    addAnimationToMap(%map, "bedslpsdr", %gender @ "nbedslpsdr", "");
    addAnimationToMap(%map, "bedslpsdl", %gender @ "nbedslpsdl", "");
    addAnimationToMap(%map, "bedrlx", %gender @ "nbedrlx", "");
    addAnimationToMap(%map, "chzlngidl1", %gender @ "nchzlngidl1", "");
    addAnimationToMap(%map, "pckride", %gender @ "npckride", "");
    addAnimationToMap(%map, "reachdown", %gender @ "nreachdown", "");
    addAnimationToMap(%map, "spinbottle", %gender @ "nspinbottle", "");
    addAnimationToMap(%map, "gtrgr1e", %gender @ "ngtrgr1e", "");
    addAnimationToMap(%map, "gtrgr2e", %gender @ "ngtrgr2e", "");
    addAnimationToMap(%map, "gtrgr3e", %gender @ "ngtrgr3e", "");
    addAnimationToMap(%map, "gtrgr5e", %gender @ "ngtrgr5e", "");
    addAnimationToMap(%map, "gtrgr6e", %gender @ "ngtrgr6e", "");
    addAnimationToMap(%map, "gtrgr7e", %gender @ "ngtrgr7e", "");
    addAnimationToMap(%map, "gtrgr8e", %gender @ "ngtrgr8e", "");
    addAnimationToMap(%map, "gtrgr9e", %gender @ "ngtrgr9e", "");
    addAnimationToMap(%map, "gtrgr10e", %gender @ "ngtrgr10e", "");
    addAnimationToMap(%map, "gtrgr11e", %gender @ "ngtrgr11e", "");
    addAnimationToMap(%map, "gtrgr12a", %gender @ "ngtrgr12a", "");
    addAnimationToMap(%map, "gtrgr13a", %gender @ "ngtrgr13a", "");
    addAnimationToMap(%map, "gtrgr14a", %gender @ "ngtrgr14a", "");
    addAnimationToMap(%map, "gtrgr15a", %gender @ "ngtrgr15a", "");
    addAnimationToMap(%map, "gtrgr17a", %gender @ "ngtrgr17a", "");
    addAnimationToMap(%map, "gtrgr18a", %gender @ "ngtrgr18a", "");
    addAnimationToMap(%map, "gtrgr22a", %gender @ "ngtrgr22a", "");
    addAnimationToMap(%map, "gtrgr25b", %gender @ "ngtrgr25b", "");
    addAnimationToMap(%map, "gtrgr28b", %gender @ "ngtrgr28b", "");
    addAnimationToMap(%map, "gtrgr29b", %gender @ "ngtrgr29b", "");
    addAnimationToMap(%map, "drumr1e", %gender @ "ndrumr1e", "");
    addAnimationToMap(%map, "bassr1e", %gender @ "nbassr1e", "");
    addAnimationToMap(%map, "arcadeidl", %gender @ "narcadeidl", "");
    addAnimationToMap(%map, "delite1", %gender @ "ndelite1", "dance");
    addAnimationToMap(%map, "delite2", %gender @ "ndelite2", "dance");
    addAnimationToMap(%map, "delite3", %gender @ "ndelite3", "dance");
    addAnimationToMap(%map, "delite4", %gender @ "ndelite4", "dance");
    addAnimationToMap(%map, "delite5", %gender @ "ndelite5", "dance");
    addAnimationToMap(%map, "tyrturn", %gender @ "ntyrturn", "dance");
    addAnimationToMap(%map, "yidl1a", %gender @ "yidl1a", "");
    addAnimationToMap(%map, "ywlkf1", %gender @ "ywlkf1", "");
    addAnimationToMap(%map, "ysde", %gender @ "ysde", "");
    addAnimationToMap(%map, "ywlkb1", %gender @ "ywlkb1", "");
    addAnimationToMap(%map, "yjmp", %gender @ "yjmp", "");
    addAnimationToMap(%map, "ycidl1a", %gender @ "ycidl1a", "");
    addAnimationToMap(%map, "ycidl2a", %gender @ "ycidl2a", "");
    addAnimationToMap(%map, "ylidl1a", %gender @ "ylidl1a", "");
    addAnimationToMap(%map, "ylidl2a", %gender @ "ylidl2a", "");
    addAnimationToMap(%map, "ylidl3a", %gender @ "ylidl3a", "");
    addAnimationToMap(%map, "swmidl1", %gender @ "nswmidl1", "");
    addAnimationToMap(%map, "swmf1", %gender @ "nswmf1", "");
    addAnimationToMap(%map, "swmb1", %gender @ "nswmb1", "");
    addAnimationToMap(%map, "swmsde", %gender @ "nswmsde", "");
    addAnimationToMap(%map, "lookat", %gender @ "nlookat", "");
    addAnimationToMap(%map, "tapglass", %gender @ "ntapglass", "");
    addAnimationToMap(%map, "pwidle", %gender @ "npwidle", "");
    addAnimationToMap(%map, "pwwlkf", %gender @ "npwwlkf", "");
    addAnimationToMap(%map, "pwwlkb", %gender @ "npwwlkb", "");
    addAnimationToMap(%map, "pwsde", %gender @ "npwsde", "");
    addAnimationToMap(%map, "pwjmp", %gender @ "npwjmp", "");
    addAnimationToMap(%map, "pwjabattack", %gender @ "npwjabattack", "");
    addAnimationToMap(%map, "pwpowerattack", %gender @ "npwpowerattack", "");
    addAnimationToMap(%map, "pwbbattack", %gender @ "npwbbattack", "");
    addAnimationToMap(%map, "pwjabdefend", %gender @ "npwjabdefend", "");
    addAnimationToMap(%map, "pwpowerdefend", %gender @ "npwpowerdefend", "");
    addAnimationToMap(%map, "pwshortstun", %gender @ "npwshortstun", "");
    addAnimationToMap(%map, "pwlongstun", %gender @ "npwlongstun", "");
    addAnimationToMap(%map, "pwtaunt01", %gender @ "npwtaunt01", "");
    addAnimationToMap(%map, "pwtaunt02", %gender @ "npwtaunt02", "");
    addAnimationToMap(%map, "dymca1", %gender @ "ndymca1", "dance");
    addAnimationToMap(%map, "dymca2", %gender @ "ndymca2", "dance");
    addAnimationToMap(%map, "dymca3", %gender @ "ndymca3", "dance");
    addAnimationToMap(%map, "bchlid1a", %gender @ "nbchlid1a", "");
    addAnimationToMap(%map, "bchlext", %gender @ "nbchlext", "");
    addAnimationToMap(%map, "bchlent", %gender @ "nbchlent", "");
    addAnimationToMap(%map, "lookat", %gender @ "nlookat", "");
    addAnimationToMap(%map, "cutout01", %gender @ "ncutout01", "");
    addAnimationToMap(%map, "cutout02", %gender @ "ncutout02", "");
    addAnimationToMap(%map, "spcentr", %gender @ "nspcentr", "");
    addAnimationToMap(%map, "spcexit", %gender @ "nspcexit", "");
    addAnimationToMap(%map, "spcidl1", %gender @ "nspcidl1", "");
    addAnimationToMap(%map, "spcft", %gender @ "nspcft", "");
    addAnimationToMap(%map, "spedentr", %gender @ "nspedentr", "");
    addAnimationToMap(%map, "spedexit", %gender @ "nspedexit", "");
    addAnimationToMap(%map, "spedidl", %gender @ "nspedidl", "");
    addAnimationToMap(%map, "smentr", %gender @ "nsmentr", "");
    addAnimationToMap(%map, "smexit", %gender @ "nsmexit", "");
    addAnimationToMap(%map, "smcidl1", %gender @ "nsmcidl1", "");
    addAnimationToMap(%map, "smanidl1", %gender @ "nsmanidl1", "");
    addAnimationToMap(%map, "ssentr", %gender @ "nssentr", "");
    addAnimationToMap(%map, "ssext", %gender @ "nssext", "");
    addAnimationToMap(%map, "ssidl1", %gender @ "nssidl1", "");
    addAnimationToMap(%map, "styl1", %gender @ "nstyl1", "");
    addAnimationToMap(%map, "cut1", %gender @ "ycut1", "");
    addAnimationToMap(%map, "bdry", %gender @ "ybdry", "");
    addAnimationToMap(%map, "brush", %gender @ "ybrush", "");
    addAnimationToMap(%map, "clip", %gender @ "yclip", "");
    addAnimationToMap(%map, "hpick", %gender @ "yhpick", "");
    addAnimationToMap(%map, "shears", %gender @ "yshears", "");
    addAnimationToMap(%map, "gunsling", %gender @ "ygunsling", "");
    addAnimationToMap(%map, "mime", %gender @ "ymime", "");
    addAnimationToMap(%map, "manpntnails", %gender @ "ysmanpnt", "");
    addAnimationToMap(%map, "manflnails", %gender @ "ysmanfl", "");
    addAnimationToMap(%map, "pedpntnails", %gender @ "yspedpnt", "");
    addAnimationToMap(%map, "pedflnails", %gender @ "yspedfl", "");
    addAnimationToMap(%map, "nailpolish", %gender @ "ynailpolish", "");
    addAnimationToMap(%map, "nailfile", %gender @ "ynailfile", "");
    addAnimationToMap(%map, "crdwve", %gender @ "ncrdwve", "");
    addAnimationToMap(%map, "losr", %gender @ "nlosr", "");
    addAnimationToMap(%map, "admrnail", %gender @ "nadmrnail", "");
    addAnimationToMap(%map, "scissorhand", %gender @ "yscissorhand", "");
    addAnimationToMap(%map, "switchcomb", %gender @ "yswitchcomb", "");
    addAnimationToMap(%map, "adjwrench", %gender @ "yadjwrench", "");
    addAnimationToMap(%map, "pipewrench", %gender @ "ypipewrench", "");
    addAnimationToMap(%map, "piercegun", %gender @ "ypiercegun", "");
    addAnimationToMap(%map, "forcepa", %gender @ "yforcepa", "");
    addAnimationToMap(%map, "forcepb", %gender @ "yforcepb", "");
    addAnimationToMap(%map, "pliera", %gender @ "ypliera", "");
    addAnimationToMap(%map, "plierb", %gender @ "yplierb", "");
    addAnimationToMap(%map, "eidl1a", %gender @ "eidl1a", "");
    addAnimationToMap(%map, "ewlkf1", %gender @ "ewlkf1", "");
    addAnimationToMap(%map, "esde", %gender @ "esde", "");
    addAnimationToMap(%map, "ewlkb1", %gender @ "ewlkb1", "");
    addAnimationToMap(%map, "ecidl1a", %gender @ "ecidl1a", "");
    addAnimationToMap(%map, "ecidl2a", %gender @ "ecidl2a", "");
    addAnimationToMap(%map, "elidl1a", %gender @ "elidl1a", "");
    addAnimationToMap(%map, "elidl2a", %gender @ "elidl2a", "");
    addAnimationToMap(%map, "elidl3a", %gender @ "elidl3a", "");
    addAnimationToMap(%map, "fidl1a", %gender @ "fidl1a", "");
    addAnimationToMap(%map, "fwlkf1", %gender @ "fwlkf1", "");
    addAnimationToMap(%map, "fsde", %gender @ "fsde", "");
    addAnimationToMap(%map, "fwlkb1", %gender @ "fwlkb1", "");
    addAnimationToMap(%map, "fcidl1a", %gender @ "fcidl1a", "");
    addAnimationToMap(%map, "fcidl2a", %gender @ "fcidl2a", "");
    addAnimationToMap(%map, "flidl1a", %gender @ "flidl1a", "");
    addAnimationToMap(%map, "flidl2a", %gender @ "flidl2a", "");
    addAnimationToMap(%map, "flidl3a", %gender @ "flidl3a", "");
    addAnimationToMap(%map, "uidl1a", %gender @ "uidl1a", "");
    addAnimationToMap(%map, "uwlkf1", %gender @ "uwlkf1", "");
    addAnimationToMap(%map, "usde", %gender @ "usde", "");
    addAnimationToMap(%map, "uwlkb1", %gender @ "uwlkb1", "");
    addAnimationToMap(%map, "ucidl1a", %gender @ "ucidl1a", "");
    addAnimationToMap(%map, "ucidl2a", %gender @ "ucidl2a", "");
    addAnimationToMap(%map, "ulidl1a", %gender @ "ulidl1a", "");
    addAnimationToMap(%map, "ulidl2a", %gender @ "ulidl2a", "");
    addAnimationToMap(%map, "ulidl3a", %gender @ "ulidl3a", "");
    addAnimationToMap(%map, "vidl1a", %gender @ "vidl1a", "");
    addAnimationToMap(%map, "vwlkf1", %gender @ "vwlkf1", "");
    addAnimationToMap(%map, "vsde", %gender @ "vsde", "");
    addAnimationToMap(%map, "vwlkb1", %gender @ "vwlkb1", "");
    addAnimationToMap(%map, "vcidl1a", %gender @ "vcidl1a", "");
    addAnimationToMap(%map, "vcidl2a", %gender @ "vcidl2a", "");
    addAnimationToMap(%map, "vlidl1a", %gender @ "vlidl1a", "");
    addAnimationToMap(%map, "vlidl2a", %gender @ "vlidl2a", "");
    addAnimationToMap(%map, "vlidl3a", %gender @ "vlidl3a", "");
    addAnimationToMap(%map, "makewine", %gender @ "fmakewine", "");
    addAnimationToMap(%map, "makeshot", %gender @ "fmakeshot", "");
    addAnimationToMap(%map, "makemartini", %gender @ "emakemartini", "");
    addAnimationToMap(%map, "makemargrita", %gender @ "emakemargrita", "");
    addAnimationToMap(%map, "makehurrican", %gender @ "emakehurrican", "");
    addAnimationToMap(%map, "makehighball", %gender @ "emakehighball", "");
    addAnimationToMap(%map, "makeheavy", %gender @ "emakeheavy", "");
    addAnimationToMap(%map, "makegcylin", %gender @ "fmakegcylin", "");
    addAnimationToMap(%map, "makecoconut", %gender @ "emakecoconut", "");
    addAnimationToMap(%map, "makechmp", %gender @ "emakechmp", "");
    addAnimationToMap(%map, "makebrandy", %gender @ "emakebrandy", "");
    addAnimationToMap(%map, "edrink01", %gender @ "edrink01", "");
    addAnimationToMap(%map, "edrink02", %gender @ "edrink02", "");
    addAnimationToMap(%map, "edrink03", %gender @ "edrink03", "");
    addAnimationToMap(%map, "fdrink01", %gender @ "fdrink01", "");
    addAnimationToMap(%map, "fdrink02", %gender @ "fdrink02", "");
    addAnimationToMap(%map, "fdrink03", %gender @ "fdrink03", "");
    addAnimationToMap(%map, "udrink01", %gender @ "udrink01", "");
    addAnimationToMap(%map, "drinkbottle01", %gender @ "ndrinkbottle01", "");
    addAnimationToMap(%map, "drinkbottle02", %gender @ "ndrinkbottle02", "");
    addAnimationToMap(%map, "drinkbottle03", %gender @ "ndrinkbottle03", "");
    addAnimationToMap(%map, "drinkmugwipe02", %gender @ "ndrinkmugwipe02", "");
    addAnimationToMap(%map, "drinkkristal", %gender @ "ndrinkkristal", "");
    addAnimationToMap(%map, "jidl1a", %gender @ "jidl1a", "");
    addAnimationToMap(%map, "jwlkf1", %gender @ "jwlkf1", "");
    addAnimationToMap(%map, "jwlkb1", %gender @ "jwlkb1", "");
    addAnimationToMap(%map, "jsde", %gender @ "jsde", "");
    addAnimationToMap(%map, "djfwc", %gender @ "ndjfwc", "");
    addAnimationToMap(%map, "djidl1", %gender @ "ndjidl1", "");
    %coanims = "";
    %delim = "";
    %n = $gCoAnimDictionary.size() - 1;
    while (%n >= 0)
    {
        %animName = getField($gCoAnimDictionary.getValue(%n), 0);
        %coanims = %coanims @ %delim @ %animName;
        %delim = " ";
        %n = %n - 1;
    }
    %coanimCount = getWordCount(%coanims);
    %prefixes = "sti mti mmi msi tsi str mtr mmr msr tsr";
    %prefixCount = getWordCount(%prefixes);
    %i = 0;
    while (%i < %coanimCount)
    {
        %coanim = getWord(%coanims, %i);
        %j = 0;
        while (%j < %prefixCount)
        {
            %prefix = getWord(%prefixes, %j);
            addAnimationToMap(%map, %prefix @ "_" @ %coanim, %gender @ "n" @ %prefix @ "_" @ %coanim, "");
            %j = %j + 1;
        }
        %i = %i + 1;
    }
}

function initNoAutoEmoteList()
{
    addNoAutoEmoteWord("angry");
    addNoAutoEmoteWord("applause");
    addNoAutoEmoteWord("busy");
    addNoAutoEmoteWord("bye");
    addNoAutoEmoteWord("confused");
    addNoAutoEmoteWord("cool");
    addNoAutoEmoteWord("cry");
    addNoAutoEmoteWord("danceA1");
    addNoAutoEmoteWord("danceA2");
    addNoAutoEmoteWord("danceA3");
    addNoAutoEmoteWord("danceA4");
    addNoAutoEmoteWord("danceB1");
    addNoAutoEmoteWord("danceB2");
    addNoAutoEmoteWord("danceB3");
    addNoAutoEmoteWord("danceB4");
    addNoAutoEmoteWord("danceC1");
    addNoAutoEmoteWord("danceC2");
    addNoAutoEmoteWord("danceC3");
    addNoAutoEmoteWord("danceC4");
    addNoAutoEmoteWord("doh");
    addNoAutoEmoteWord("embarassed");
    addNoAutoEmoteWord("embarrassed");
    addNoAutoEmoteWord("flirt");
    addNoAutoEmoteWord("ha");
    addNoAutoEmoteWord("hello");
    addNoAutoEmoteWord("haha");
    addNoAutoEmoteWord("heh");
    addNoAutoEmoteWord("hehe");
    addNoAutoEmoteWord("hey");
    addNoAutoEmoteWord("hi");
    addNoAutoEmoteWord("hmm");
    addNoAutoEmoteWord("huh");
    addNoAutoEmoteWord("inLove");
    addNoAutoEmoteWord("in-love");
    addNoAutoEmoteWord("kiss");
    addNoAutoEmoteWord("later");
    addNoAutoEmoteWord("listen");
    addNoAutoEmoteWord("notlistening");
    addNoAutoEmoteWord("not-listening");
    addNoAutoEmoteWord("sad");
    addNoAutoEmoteWord("scared");
    addNoAutoEmoteWord("shhh");
    addNoAutoEmoteWord("sit");
    addNoAutoEmoteWord("sleepy");
    addNoAutoEmoteWord("smile");
    addNoAutoEmoteWord("spin");
    addNoAutoEmoteWord("surprised");
    addNoAutoEmoteWord("talk");
    addNoAutoEmoteWord("talktothehand");
    addNoAutoEmoteWord("talk-to-the-hand");
    addNoAutoEmoteWord("thinking");
    addNoAutoEmoteWord("vomit");
    addNoAutoEmoteWord("puke");
    addNoAutoEmoteWord("upchuck");
    addNoAutoEmoteWord("waiting");
    addNoAutoEmoteWord("wave");
    addNoAutoEmoteWord("whew");
    addNoAutoEmoteWord("wow");
    addNoAutoEmoteWord("zzz");
    return ;
}
$gNoAutoEmoteWords = 0;
function addNoAutoEmoteWord(%word)
{
    if (!isObject($gNoAutoEmoteWords))
    {
        $gNoAutoEmoteWords = new StringMap();
        if (isObject(MissionCleanup))
        {
            MissionCleanup.add($gNoAutoEmoteWords);
        }
    }
    $gNoAutoEmoteWords.put(%word, 1);
    return ;
}
function isNoAutoEmoteWord(%word)
{
    if (!isObject($gNoAutoEmoteWords))
    {
        initNoAutoEmoteList();
    }
    return $gNoAutoEmoteWords.get(%word);
}
initializeAnimationMaps();

