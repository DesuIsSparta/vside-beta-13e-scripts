function DeclareFloorplan(%floorplanName, %sku) {
    %sku[$gFloorPlanFromSKU @ %sku] = %floorplanName;
    %floorplanName[$gSKUFromFloorPlan @ %floorplanName] = %sku;
};
function Buildings::GetFloorPlanNameFromSku(%sku) {
    return %sku[$gFloorPlanFromSKU @ %sku];
};
function Buildings::GetSkuFromFloorPlanName(%floorplanName) {
    return %floorplanName[$gSKUFromFloorPlan @ %floorplanName];
};
function DeclareBuilding(%buildingName, %buildingDescription, %longDescription, %minlevelToOwn, %areaNames, %floorplans) {
    %areaNameCity = DestinationList::GetAreaNameCity(firstWord(%areaNames));
    if ((%areaNameCity $= "")) {
    }
    if (!(%areaNames $= "")) {
        error(getScopeName() @ " " @ "- areaName does not correspond to a city. -" @ " " @ %areaNames @ " " @ getTrace());
    }
    %buildingName[$gBuildingDesc @ %buildingName] = %buildingDescription;
    %buildingName[$gBuildingLongDesc @ %buildingName] = %longDescription;
    %buildingName[$gBuildingMinLevelToOwn @ %buildingName] = %minlevelToOwn;
    %buildingName[$gBuildingVURL @ %buildingName] = "vside:/location/" @ %areaNameCity @ "/" @ %buildingName @ "_ReturnSpawn";
    %buildingName[$gBuildingAreaNames @ %buildingName] = %areaNames;
    %buildingName[$gBuildingFloorplans @ %buildingName] = %floorplans;
    %i = (1.0 - getWordCount(%floorplans));
    if ((0.0 >= %i)) {
        %floorplanName = getWord(%floorplans, %i);
        %sku = Buildings::GetSkuFromFloorPlanName(%floorplanName);
        if ((%sku $= "")) {
            error(getScopeName() @ " " @ "This Floorplan has not properly been declared yet, see DeclareFloorplan");
        }
        %floorplanName[$gBuildingNamesFromFloorplans @ %floorplanName] = %buildingName;
        %i = (1.0 - %i);
    }
};
function Buildings::GetDescription(%name) {
    return %name[$gBuildingDesc @ %name];
};
function Buildings::GetLongDescription(%name) {
    %ret = %name[$gBuildingLongDesc @ %name];
    if ((%ret $= "")) {
        %ret = %ret[$gMlStyle @ "CSProfileDescriptionHeaderNormal"] @ "Looking for a hoppin' party?" @ "\n" @ %ret[$gMlStyle @ "CSProfileDescriptionHeaderNormal"][$gMlStyle @ "CSProfileDescriptionTextNormal"] @ "Check out the directory to your left and pick an apartment with lots of people. Hop around!" @ "\n\n" @ %ret[$gMlStyle @ "CSProfileDescriptionHeaderNormal"][$gMlStyle @ "CSProfileDescriptionTextNormal"][$gMlStyle @ "CSProfileDescriptionHeaderNormal"] @ "Looking to meet people?" @ "\n" @ %ret[$gMlStyle @ "CSProfileDescriptionHeaderNormal"][$gMlStyle @ "CSProfileDescriptionTextNormal"][$gMlStyle @ "CSProfileDescriptionHeaderNormal"][$gMlStyle @ "CSProfileDescriptionTextNormal"] @ "Browse the directory and see who's home. Don't be shy!" @ "\n\n" @ %ret[$gMlStyle @ "CSProfileDescriptionHeaderNormal"][$gMlStyle @ "CSProfileDescriptionTextNormal"][$gMlStyle @ "CSProfileDescriptionHeaderNormal"][$gMlStyle @ "CSProfileDescriptionTextNormal"][$gMlStyle @ "CSProfileDescriptionHeaderNormal"] @ "Want an apartment to call your own?" @ "\n" @ %ret[$gMlStyle @ "CSProfileDescriptionHeaderNormal"][$gMlStyle @ "CSProfileDescriptionTextNormal"][$gMlStyle @ "CSProfileDescriptionHeaderNormal"][$gMlStyle @ "CSProfileDescriptionTextNormal"][$gMlStyle @ "CSProfileDescriptionHeaderNormal"][$gMlStyle @ "CSProfileDescriptionTextNormal"] @ "Visit the model apartment to get your own apartment! Stylize as you see fit and invite your friends over to meet up!" @ "\n\n" @ %ret[$gMlStyle @ "CSProfileDescriptionHeaderNormal"][$gMlStyle @ "CSProfileDescriptionTextNormal"][$gMlStyle @ "CSProfileDescriptionHeaderNormal"][$gMlStyle @ "CSProfileDescriptionTextNormal"][$gMlStyle @ "CSProfileDescriptionHeaderNormal"][$gMlStyle @ "CSProfileDescriptionTextNormal"][$gMlStyle @ "CSProfileDescriptionHeaderNormal"] @ "Strut your stuff!" @ "\n" @ %ret[$gMlStyle @ "CSProfileDescriptionHeaderNormal"][$gMlStyle @ "CSProfileDescriptionTextNormal"][$gMlStyle @ "CSProfileDescriptionHeaderNormal"][$gMlStyle @ "CSProfileDescriptionTextNormal"][$gMlStyle @ "CSProfileDescriptionHeaderNormal"][$gMlStyle @ "CSProfileDescriptionTextNormal"][$gMlStyle @ "CSProfileDescriptionHeaderNormal"][$gMlStyle @ "CSProfileDescriptionTextNormal"] @ "Make your own jaw-dropping vSide party. Pick your favorite YouTube vids and jam!";
    }
    return %ret;
};
function Buildings::GetMinLevelToOwn(%name) {
    return %name[$gBuildingMinLevelToOwn @ %name];
};
function Buildings::getReturnVURL(%name) {
    return %name[$gBuildingVURL @ %name];
};
function Buildings::GetContiguousSpace(%name) {
    %areaName = Buildings::GetAreaName(%name);
    %cityName = DestinationList::GetAreaNameCity(%areaName);
    return %cityName;
};
function Buildings::GetAreaNames(%name) {
    return %name[$gBuildingAreaNames @ %name];
};
function Buildings::GetAreaName(%name) {
    return firstWord(Buildings::GetAreaNames(%name));
};
function Buildings::GetFloorplans(%name) {
    return %name[$gBuildingFloorplans @ %name];
};
function Buildings::GetBuildingNameFromFloorplan(%name) {
    return %name[$gBuildingNamesFromFloorplans @ %name];
};
