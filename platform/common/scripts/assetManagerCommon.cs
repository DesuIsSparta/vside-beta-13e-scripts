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
    if (!($Net::userOwner $= "")) {
        %userOwner = $Net::userOwner;
    }
    %map = new StringMap("");
    if (isObject(MissionCleanup)) {
        %map.add(MissionCleanup);
    }
    0.put(%map, "projects/vside/worlds/common.zip");
    1.put(%map, "projects/vside/worlds/gateway.zip");
    if ((%userOwner $= "degrassi")) {
        3.put(%map, "projects/vside/worlds/lounge.zip");
        4.put(%map, "projects/vside/worlds/raijuku.zip");
        2.put(%map, "projects/vside/worlds/lga.zip");
    }
    2.put(%map, "projects/vside/worlds/lounge.zip");
    3.put(%map, "projects/vside/worlds/raijuku.zip");
    4.put(%map, "projects/vside/worlds/lga.zip");
    0.put(%map, "projects/common.zip");
    return %map;
};
function AssetManager::getPackages() {
    return $AssetManager::defaultPackages;
};
function AssetManager::commonInit() {
    AssetManager::initPackages();
};
function AssetManager::getMissingAssets() {
    %map = new StringMap("");
    if (isObject(MissionCleanup)) {
        %map.add(MissionCleanup);
    }
    if (($AssetManager::missingAssets $= "")) {
        return %map;
    }
    %num = getFieldCount($AssetManager::missingAssets);
    %orderMap = AssetManager::getPackageOrder();
    %n = 0;
    while ((%n < %num)) {
        %key = getField($AssetManager::missingAssets, %n);
        %order = %key.get(%orderMap);
        echo("key: " @ %key @ " or: " @ %order);
        %order.put(%map, %key);
        %n = (%n + 1.0);
    }
    return %map;
};
function AssetManager::MapToString(%map) {
    %str = "";
    %orderMap = AssetManager::getPackageOrder();
    %n = 0;
    while ((%n < %orderMap.size())) {
        %key = %n.getKey(%orderMap);
        %str = %str @ %key @ "=" @ %n.getValue(%map) @ "\t" @ "";
        %n = (%n + 1.0);
    }
};
function AssetManager::StringToMap(%str) {
    %map = new StringMap("");
    if (isObject(MissionCleanup)) {
        %map.add(MissionCleanup);
    }
    %num = getFieldCount(%str);
    %n = 0;
    while ((%n < %num)) {
        %tag = getField(%str, %n);
        %fs = strstr(%tag, "=");
        %value = getSubStr(strrchr(%tag, "="), 1, 10000);
        %key = getSubStr(%tag, 0, %fs);
        %value.put(%map, %key);
        %n = (%n + 1.0);
    }
    return %map;
};
function AssetManager::StringToArray(%str) {
    %array = new Array("");
    if (isObject(MissionCleanup)) {
        %array.add(MissionCleanup);
    }
    %num = getFieldCount(%str);
    %n = 0;
    while ((%n < %num)) {
        %tag = getField(%str, %n);
        %fs = strstr(%tag, "=");
        %value = getSubStr(strrchr(%tag, "="), 1, 10000);
        %key = getSubStr(%tag, 0, %fs);
        %value.push_back(%array, %key);
        %n = (%n + 1.0);
    }
    return %array;
};
function AssetManager::dumpMap(%map) {
    echo("Map: " @ %map);
    %n = 0;
    while ((%n < %map.size())) {
        %key = %n.getKey(%map);
        echo(%key @ " = " @ %n.getValue(%map));
        %n = (%n + 1.0);
    }
};
function AssetManager::rehashSet(%map) {
    if (!(isObject(%map))) {
        %map = AssetManager::getMissingAssets();
    }
    assetManagerPurge();
    %n = 0;
    while ((%n < %map.size())) {
        %key = %n.getKey(%map);
        assetManagerInsert(%key);
        %n = (%n + 1.0);
    }
    assetManagerHashPackages();
    return assetManagerMapString();
};
function AssetManager::cityToPackage(%str) {
    if ((%str $= "nv")) {
        return "projects/vside/worlds/lounge.zip";
    }
    if ((%str $= "lga")) {
        return "projects/vside/worlds/lga.zip";
    }
    if ((%str $= "rj")) {
        return "projects/vside/worlds/raijuku.zip";
    }
    if ((%str $= "gw")) {
        return "projects/vside/worlds/gateway.zip";
    }
    return "";
};
function AssetManager::packageToCity(%str) {
    if ((%str $= "lounge.zip")) {
        return "nv";
    }
    if ((%str $= "lga.zip")) {
        return "lga";
    }
    if ((%str $= "raijuku.zip")) {
        return "rj";
    }
    if ((%str $= "gateway.zip")) {
        return "gw";
    }
    return "";
};
