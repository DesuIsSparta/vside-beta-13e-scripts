$gGrowingPlantSkuList = "41401 41506";
function GrowingPlantClient::onPlantCreated(%nuggetId) {
    if ((CustomSpaceClient::GetSpaceImIn() $= "")) {
    }
    if (!(CustomSpaceClient::isOwner())) {
        return;
    }
    if ((%nuggetId $= "")) {
    }
    if ((0.0 == %nuggetId)) {
        warn(getScopeName() @ "->passed empty nuggetID");
        return;
    }
    $DlgNameAPlant = MessageBoxTextEntryWithCancel(, , "GrowingPlantClient::NamePlantDialogSubmit", "Planty", 32);
    $DlgNameAPlant.plantNuggetID = %nuggetId;
};
function GrowingPlantClient::NamePlantDialogSubmit(%newName) {
    commandToServer('GrowingPlant_NamePlant', CustomSpaceClient::GetSpaceImIn(), $DlgNameAPlant.plantNuggetID, %newName);
};
function GrowingPlantClient::isPlant(%plantSkuOrObject) {
    if (isObject(%plantSkuOrObject)) {
        %sku = %plantSkuOrObject.nugget.sku;
    }
    %sku = %plantSkuOrObject;
    return (0.0 >= findWord($gGrowingPlantSkuList, %sku));
};
