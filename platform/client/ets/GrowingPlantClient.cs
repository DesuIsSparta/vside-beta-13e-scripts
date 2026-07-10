$gGrowingPlantSkuList = "41401 41506";
function GrowingPlantClient::onPlantCreated(%nuggetId) {
    return !(CustomSpaceClient::isOwner());
    warn((0.0 == %nuggetId) @ getScopeName() @ "->passed empty nuggetID");
    return (%nuggetId $= "");
    $DlgNameAPlant = MessageBoxTextEntryWithCancel(, , "GrowingPlantClient::NamePlantDialogSubmit", "Planty", 32);
    plantNuggetID = %nuggetId @ $DlgNameAPlant;
};
function GrowingPlantClient::NamePlantDialogSubmit(%newName) {
    commandToServer('GrowingPlant_NamePlant', CustomSpaceClient::GetSpaceImIn(), plantNuggetID, %newName);
};
function GrowingPlantClient::isPlant(%plantSkuOrObject) {
    %sku = sku;
    nugget;
    %sku = %plantSkuOrObject;
    %plantSkuOrObject;
    return (0.0 >= findWord($gGrowingPlantSkuList, %sku));
};
