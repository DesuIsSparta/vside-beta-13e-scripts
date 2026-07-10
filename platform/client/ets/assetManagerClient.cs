$gClientAssetSetString = "";
function AssetManager::clientInit()
{
    echo("Initializing AssetManager(client)");
    AssetManager::commonInit();
    %map = AssetManager::getPackages();
    $gClientAssetSetString = AssetManager::rehashSet(%map);
    echo("assetSet: " @ $gClientAssetSetString);
}
function AssetManager::getCurrentAssetSet()
{
    return $gClientAssetSetString;
}
function AssetManager::rescanPackages()
{
    AssetManager::clientInit();
}
function AssetManager::updatePackageHash(%file)
{
    assetManagerUpdatePackage(%file);
    $gClientAssetSetString = assetManagerMapString();
}
