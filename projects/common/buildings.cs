function DeclareFloorplan(%floorplanName, %sku)
{
    $gFloorPlanFromSKU[%sku] = %floorplanName ;
    $gSKUFromFloorPlan[%floorplanName] = %sku ;
    return ;
}
function Buildings::GetFloorPlanNameFromSku(%sku)
{
    return $gFloorPlanFromSKU[%sku];
}
function Buildings::GetSkuFromFloorPlanName(%floorplanName)
{
    return $gSKUFromFloorPlan[%floorplanName];
}
function DeclareBuilding(%buildingName, %buildingDescription, %longDescription, %minlevelToOwn, %areaNames, %floorplans)
{
    %areaNameCity = DestinationList::GetAreaNameCity(firstWord(%areaNames));
    if ((%areaNameCity $= "") && !((%areaNames $= "")))
    {
        error(getScopeName() SPC "- areaName does not correspond to a city. -" SPC %areaNames SPC getTrace());
    }
    $gBuildingDesc[%buildingName] = %buildingDescription ;
    $gBuildingLongDesc[%buildingName] = %longDescription ;
    $gBuildingMinLevelToOwn[%buildingName] = %minlevelToOwn ;
    $gBuildingVURL[%buildingName] = "vside:/location/" @ %areaNameCity @ "/" @ %buildingName @ "_ReturnSpawn";
    $gBuildingAreaNames[%buildingName] = %areaNames ;
    $gBuildingFloorplans[%buildingName] = %floorplans ;
    %i = getWordCount(%floorplans) - 1;
    while (%i >= 0)
    {
        %floorplanName = getWord(%floorplans, %i);
        %sku = Buildings::GetSkuFromFloorPlanName(%floorplanName);
        if (%sku $= "")
        {
            error(getScopeName() SPC "This Floorplan has not properly been declared yet, see DeclareFloorplan");
        }
        $gBuildingNamesFromFloorplans[%floorplanName] = %buildingName ;
        %i = %i - 1;
    }
}

function Buildings::GetDescription(%name)
{
    return $gBuildingDesc[%name];
}
function Buildings::GetLongDescription(%name)
{
    %ret = $gBuildingLongDesc[%name];
    if (%ret $= "")
    {
        %ret = $gMlStyle["CSProfileDescriptionHeaderNormal"] @ "Looking for a hoppin\' party?" NL $gMlStyle["CSProfileDescriptionTextNormal"] @ "Check out the directory to your left and pick an apartment with lots of people. Hop around!" @ "\n\n" @ $gMlStyle["CSProfileDescriptionHeaderNormal"] @ "Looking to meet people?" NL $gMlStyle["CSProfileDescriptionTextNormal"] @ "Browse the directory and see who\'s home. Don\'t be shy!" @ "\n\n" @ $gMlStyle["CSProfileDescriptionHeaderNormal"] @ "Want an apartment to call your own?" NL $gMlStyle["CSProfileDescriptionTextNormal"] @ "Visit the model apartment to get your own apartment! Stylize as you see fit and invite your friends over to meet up!" @ "\n\n" @ $gMlStyle["CSProfileDescriptionHeaderNormal"] @ "Strut your stuff!" NL $gMlStyle["CSProfileDescriptionTextNormal"] @ "Make your own jaw-dropping vSide party. Pick your favorite YouTube vids and jam!";
    }
    return %ret;
}
function Buildings::GetMinLevelToOwn(%name)
{
    return $gBuildingMinLevelToOwn[%name];
}
function Buildings::getReturnVURL(%name)
{
    return $gBuildingVURL[%name];
}
function Buildings::GetContiguousSpace(%name)
{
    %areaName = Buildings::GetAreaName(%name);
    %cityName = DestinationList::GetAreaNameCity(%areaName);
    return %cityName;
}
function Buildings::GetAreaNames(%name)
{
    return $gBuildingAreaNames[%name];
}
function Buildings::GetAreaName(%name)
{
    return firstWord(Buildings::GetAreaNames(%name));
}
function Buildings::GetFloorplans(%name)
{
    return $gBuildingFloorplans[%name];
}
function Buildings::GetBuildingNameFromFloorplan(%name)
{
    return $gBuildingNamesFromFloorplans[%name];
}
