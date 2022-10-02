$gGrowingPlantSkuList = "41401 41506";
function GrowingPlantClient::onPlantCreated(%nuggetId)
{
    if ((CustomSpaceClient::GetSpaceImIn() $= "") && !CustomSpaceClient::isOwner())
    {
        return ;
    }
    if ((%nuggetId $= "") && (%nuggetId == 0))
    {
        warn(getScopeName() @ "->passed empty nuggetID");
        return ;
    }
    $DlgNameAPlant = MessageBoxTextEntryWithCancel($MsgCat::furniture["NAMEPLANT-TITLE"], $MsgCat::furniture["NAMEPLANT-PROMPT"], "GrowingPlantClient::NamePlantDialogSubmit", "Planty", 32);
    $DlgNameAPlant.plantNuggetID = %nuggetId;
    return ;
}
function GrowingPlantClient::NamePlantDialogSubmit(%newName)
{
    commandToServer('GrowingPlant_NamePlant', CustomSpaceClient::GetSpaceImIn(), $DlgNameAPlant.plantNuggetID, %newName);
    return ;
}
function GrowingPlantClient::isPlant(%plantSkuOrObject)
{
    if (isObject(%plantSkuOrObject))
    {
        %sku = %plantSkuOrObject.nugget.sku;
    }
    else
    {
        %sku = %plantSkuOrObject;
    }
    return findWord($gGrowingPlantSkuList, %sku) >= 0;
}
