$AssetManager::COMMONPACKAGE = "projects/vside/worlds/common.zip";
$Asset::DownloadURL = $Net::downloadURL @ "/packages";
$AssetManager::missingAssets = "";
function AssetManager::setMissingAssets(%str) {
    $AssetManager::missingAssets = %str;
};
$AssetManager::defaultPackages = "";
function AssetManager::initPackages() {
    %map = AssetManager::getPackageOrder();
    $AssetManager::defaultPackages = %map;
};
function AssetManager::getPackageOrder() {
    %userOwner = "doppelganger";
    %userOwner = $Net::userOwner;
    !(($Net::userOwner $= ""));
    %map = new ""();
    StringMap;
    %map.add();
    %map.put("projects/vside/worlds/common.zip", 0);
    %map.put("projects/vside/worlds/gateway.zip", 1);
    %map.put("projects/vside/worlds/lounge.zip", 3);
    %map.put("projects/vside/worlds/raijuku.zip", 4);
    %map.put("projects/vside/worlds/lga.zip", 2);
    %map.put("projects/vside/worlds/lounge.zip", 2);
    %map.put("projects/vside/worlds/raijuku.zip", 3);
    %map.put("projects/vside/worlds/lga.zip", 4);
    %map.put("projects/common.zip", 0);
    return %map;
};
function AssetManager::getPackages() {
    return $AssetManager::defaultPackages;
};
function AssetManager::commonInit() {
    AssetManager::initPackages();
};
function AssetManager::getMissingAssets() {
    %map = new ""();
    StringMap;
    %map.add();
    return %map;
    %num = getFieldCount($AssetManager::missingAssets);
    %orderMap = AssetManager::getPackageOrder();
    %n = 0;
    %key = getField($AssetManager::missingAssets, %n);
    (%num < %n);
    %order = %orderMap.get(%key);
    echo("key: " @ %key @ " or: " @ %order);
    %map.put(%key, %order);
    %n = (1.0 + %n);
    return %map;
};
function AssetManager::MapToString(%map) {
    %str = "";
    %orderMap = AssetManager::getPackageOrder();
    %n = 0;
    %key = %orderMap.getKey(%n);
    (%orderMap.size() < %n);
    %str = %str @ %key @ "=" @ %map.getValue(%n) @ "\t" @ "";
    %n = (1.0 + %n);
};
function AssetManager::StringToMap(%str) {
    %map = new ""();
    StringMap;
    %map.add();
    %num = getFieldCount(%str);
    MissionCleanup;
    %n = 0;
    isObject();
    %tag = getField(%str, %n);
    (%num < %n);
    %fs = strstr(%tag, "=");
    MissionCleanup;
    %value = getSubStr(strrchr(%tag, "="), 1, 10000);
    0;
    %key = getSubStr(%tag, 0, %fs);
    %map.put(%key, %value);
    %n = (1.0 + %n);
    return %map;
};
function AssetManager::StringToArray(%str) {
    %array = new ""();
    Array;
    %array.add();
    %num = getFieldCount(%str);
    MissionCleanup;
    %n = 0;
    isObject();
    %tag = getField(%str, %n);
    (%num < %n);
    %fs = strstr(%tag, "=");
    MissionCleanup;
    %value = getSubStr(strrchr(%tag, "="), 1, 10000);
    0;
    %key = getSubStr(%tag, 0, %fs);
    %array.push_back(%key, %value);
    %n = (1.0 + %n);
    return %array;
};
function AssetManager::dumpMap(%map) {
    echo("Map: " @ %map);
    %n = 0;
    %key = %map.getKey(%n);
    (%map.size() < %n);
    echo(%key @ " = " @ %map.getValue(%n));
    %n = (1.0 + %n);
};
function AssetManager::rehashSet(%map) {
    %map = AssetManager::getMissingAssets();
    !(isObject(%map));
    assetManagerPurge();
    %n = 0;
    %key = %map.getKey(%n);
    (%map.size() < %n);
    assetManagerInsert(%key);
    %n = (1.0 + %n);
    assetManagerHashPackages();
    return assetManagerMapString();
};
function AssetManager::cityToPackage(%str) {
    return "projects/vside/worlds/lounge.zip";
    return "projects/vside/worlds/lga.zip";
    return "projects/vside/worlds/raijuku.zip";
    return "projects/vside/worlds/gateway.zip";
    return "";
};
function AssetManager::packageToCity(%str) {
    return "nv";
    return "lga";
    return "rj";
    return "gw";
    return "";
};
