$AssetManager::COMMONPACKAGE = "projects/vside/worlds/common.zip";
$Asset::DownloadURL = $Net::downloadURL @ "/packages";
$AssetManager::missingAssets = "";
function AssetManager::setMissingAssets(%str)
{
    $AssetManager::missingAssets = %str;
    return ;
}
$AssetManager::defaultPackages = "";
function AssetManager::initPackages()
{
    %map = AssetManager::getPackageOrder();
    $AssetManager::defaultPackages = %map;
    return ;
}
function AssetManager::getPackageOrder()
{
    %userOwner = "doppelganger";
    if (!($Net::userOwner $= ""))
    {
        %userOwner = $Net::userOwner;
    }
    %map = new StringMap();
    if (isObject(MissionCleanup))
    {
        MissionCleanup.add(%map);
    }
    %map.put("projects/vside/worlds/common.zip", 0);
    %map.put("projects/vside/worlds/gateway.zip", 1);
    if (%userOwner $= "degrassi")
    {
        %map.put("projects/vside/worlds/lounge.zip", 3);
        %map.put("projects/vside/worlds/raijuku.zip", 4);
        %map.put("projects/vside/worlds/lga.zip", 2);
    }
    else
    {
        %map.put("projects/vside/worlds/lounge.zip", 2);
        %map.put("projects/vside/worlds/raijuku.zip", 3);
        %map.put("projects/vside/worlds/lga.zip", 4);
    }
    %map.put("projects/common.zip", 0);
    return %map;
}
function AssetManager::getPackages()
{
    return $AssetManager::defaultPackages;
}
function AssetManager::commonInit()
{
    AssetManager::initPackages();
    return ;
}
function AssetManager::getMissingAssets()
{
    %map = new StringMap();
    if (isObject(MissionCleanup))
    {
        MissionCleanup.add(%map);
    }
    if ($AssetManager::missingAssets $= "")
    {
        return %map;
    }
    %num = getFieldCount($AssetManager::missingAssets);
    %orderMap = AssetManager::getPackageOrder();
    %n = 0;
    while (%n < %num)
    {
        %key = getField($AssetManager::missingAssets, %n);
        %order = %orderMap.get(%key);
        echo("key: " @ %key @ " or: " @ %order);
        %map.put(%key, %order);
        %n = %n + 1;
    }
    return %map;
}
function AssetManager::MapToString(%map)
{
    %str = "";
    %orderMap = AssetManager::getPackageOrder();
    %n = 0;
    while (%n < %orderMap.size())
    {
        %key = %orderMap.getKey(%n);
        %str = %str @ %key @ "=" @ %map.getValue(%n) TAB "";
        %n = %n + 1;
    }
}

function AssetManager::StringToMap(%str)
{
    %map = new StringMap();
    if (isObject(MissionCleanup))
    {
        MissionCleanup.add(%map);
    }
    %num = getFieldCount(%str);
    %n = 0;
    while (%n < %num)
    {
        %tag = getField(%str, %n);
        %fs = strstr(%tag, "=");
        %value = getSubStr(strrchr(%tag, "="), 1, 10000);
        %key = getSubStr(%tag, 0, %fs);
        %map.put(%key, %value);
        %n = %n + 1;
    }
    return %map;
}
function AssetManager::StringToArray(%str)
{
    %array = new Array();
    if (isObject(MissionCleanup))
    {
        MissionCleanup.add(%array);
    }
    %num = getFieldCount(%str);
    %n = 0;
    while (%n < %num)
    {
        %tag = getField(%str, %n);
        %fs = strstr(%tag, "=");
        %value = getSubStr(strrchr(%tag, "="), 1, 10000);
        %key = getSubStr(%tag, 0, %fs);
        %array.push_back(%key, %value);
        %n = %n + 1;
    }
    return %array;
}
function AssetManager::dumpMap(%map)
{
    echo("Map: " @ %map);
    %n = 0;
    while (%n < %map.size())
    {
        %key = %map.getKey(%n);
        echo(%key @ " = " @ %map.getValue(%n));
        %n = %n + 1;
    }
}

function AssetManager::rehashSet(%map)
{
    if (!isObject(%map))
    {
        %map = AssetManager::getMissingAssets();
    }
    assetManagerPurge();
    %n = 0;
    while (%n < %map.size())
    {
        %key = %map.getKey(%n);
        assetManagerInsert(%key);
        %n = %n + 1;
    }
    assetManagerHashPackages();
    return assetManagerMapString();
}
function AssetManager::cityToPackage(%str)
{
    if (%str $= "nv")
    {
        return "projects/vside/worlds/lounge.zip";
    }
    else
    {
        if (%str $= "lga")
        {
            return "projects/vside/worlds/lga.zip";
        }
        else
        {
            if (%str $= "rj")
            {
                return "projects/vside/worlds/raijuku.zip";
            }
            else
            {
                if (%str $= "gw")
                {
                    return "projects/vside/worlds/gateway.zip";
                }
                else
                {
                    return "";
                }
            }
        }
    }
    return ;
}
function AssetManager::packageToCity(%str)
{
    if (%str $= "lounge.zip")
    {
        return "nv";
    }
    else
    {
        if (%str $= "lga.zip")
        {
            return "lga";
        }
        else
        {
            if (%str $= "raijuku.zip")
            {
                return "rj";
            }
            else
            {
                if (%str $= "gateway.zip")
                {
                    return "gw";
                }
                else
                {
                    return "";
                }
            }
        }
    }
    return ;
}
