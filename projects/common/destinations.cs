$gDestinationNamesInternal = "";
$gDestinationFiltersInUse = "";
$gDestinationFiltersInDirectory = "event" SPC "shop" SPC "venue" SPC "plaza" SPC "residence";
function DestinationList::AddDestinationInfo(%codeName, %filters, %contiguousSpaceNames, %userFacingName, %userFacingDescriptionBase, %inWorldTrailer, %vurl, %okayForTGF)
{
    $gDestinationFilters[%codeName] = %filters ;
    $gDestinationNames[%codeName] = %userFacingName ;
    $gDestinationDescsInCloset[%codeName] = %userFacingDescriptionBase ;
    $gDestinationDescsInWorld[%codeName] = "Welcome to" SPC %userFacingName @ "!" SPC %inWorldTrailer ;
    $gDestinationSpaces[%codeName] = %contiguousSpaceNames ;
    $gDestinationVurls[%codeName] = %vurl ;
    $gDestinationNamesInternal = $gDestinationNamesInternal @ %codeName @ " ";
    %i = getWordCount(%filters) - 1;
    while (%i >= 0)
    {
        %filter = getWord(%filters, %i);
        if (%filter $= "shop")
        {
            $gStoreStockCacheSkus[%codeName] = "";
            $gStoreStockRevision[%codeName] = "";
        }
        if (findWord($gDestinationFiltersInUse, %filter) < 0)
        {
            $gDestinationFiltersInUse = $gDestinationFiltersInUse @ %filter @ " ";
        }
        if (findWord($gDestinationFiltersInDirectory, %filter) >= 0)
        {
            DestinationList::AddDestinationAd(%codeName, %vurl, %okayForTGF);
        }
        %i = %i - 1;
    }
}

function DestinationList::getDestinationContiguousSpace(%codeName)
{
    return $gDestinationSpaces[%codeName];
}
function DestinationList::IsDestinationInMyContiguousSpace(%codeName)
{
    if ($gContiguousSpaceName $= "")
    {
        return 0;
    }
    return findWord(DestinationList::getDestinationContiguousSpace(%codeName), $gContiguousSpaceName) >= 0;
}
function DestinationList::getCityNameForDestination(%codeName)
{
    %dest = DestinationList::getDestinationContiguousSpace(%codeName);
    return getContiguousSpaceFullName(%dest);
}
function DestinationList::getBitmapLocation(%codeName)
{
    if ("" $= %codeName)
    {
        return 0;
    }
    %filter = getWord($gDestinationFilters[%codeName], 0);
    if ("" $= %filter)
    {
        return 0;
    }
    return "platform/client/buttons/" @ %filter @ "ads/" @ %filter @ "ad_" @ %codeName;
}
function DestinationList::getOccluderBitmapLocation()
{
    return "platform/client/buttons/destinationsDirectoryOccluder";
}
$gDestinationAdsNum = 0;
function DestinationList::AddDestinationAd(%codeName, %vurl, %okayForTGF)
{
    %isNewEntry = $gDestinationAdsNumByName[%codeName] $= "";
    $gDestinationAds[$gDestinationAdsNum,"codename"] = %codeName ;
    $gDestinationAds[$gDestinationAdsNum,"okayForTGF"] = %okayForTGF ;
    if (%isNewEntry)
    {
        $gDestinationAdsNumByName[%codeName] = $gDestinationAdsNum ;
        $gDestinationAdsNum = $gDestinationAdsNum + 1;
    }
    return ;
}
function DestinationList::GetRandomDestinationForTGF(%filter, %butNotThese)
{
    if (!isDefined("%butNotThese"))
    {
        %butNotThese = "interscope_lounge";
    }
    %candidates = "";
    %delim = "";
    %n = 0;
    while (%n < $gDestinationAdsNum)
    {
        %eligible = $gDestinationAds[%n,"okayForTGF"];
        if (!%eligible)
        {
            continue;
        }
        %codeName = $gDestinationAds[%n,"codename"];
        if (hasWord(%butNotThese, %codeName))
        {
            continue;
        }
        %filters = $gDestinationFilters[%codeName];
        if (hasWord(%filters, %filter))
        {
            %candidates = %candidates @ %delim @ %codeName;
            %delim = " ";
        }
        %n = %n + 1;
    }
    if (%candidates $= "")
    {
        error("Unable to find any candidates for filter \"" @ %filter @ "\"." SPC getTrace());
        return "";
    }
    return getRandomWord(%candidates);
}
function DestinationList::goToDestination(%codeName)
{
    if (findWord($gDestinationNamesInternal, %codeName) < 0)
    {
        return ;
    }
    %vurl = $gDestinationVurls[%codeName];
    if (%vurl $= "")
    {
        error(getScopeName() SPC "- no vurl for destination" SPC %codeName);
        return ;
    }
    %command = "geTGF.close();" SPC "vurlOperation(\"" @ %vurl @ "\");";
    if (!DestinationList::IsDestinationInMyContiguousSpace(%codeName))
    {
        %title = $MsgCat::destinations["REMOTE-TITLE"];
        %body = strreplace($MsgCat::destinations["REMOTE-BODY"], "[NAME]", $gDestinationNames[%codeName]);
        MessageBoxOkCancel(%title, %body, %command, "");
    }
    else
    {
        eval(%command);
    }
    return ;
}
function transferFromShopToDestinationsDirectory()
{
    transferFromShopToDestinationsDirectoryPart2(1, 0);
    return ;
}
function transferFromShopToDestinationsDirectoryPart2(%askForSave, %doSave)
{
    if (%askForSave)
    {
        %askForSave = ClosetGui.userHasChangedBodyOrOutfit();
        if (%askForSave)
        {
            %title = "Save Your Changes";
            %text = "You\'ve made some changes to your appearance.\nWould you like to save your changes?";
            %buttons = "Yes" TAB "No" TAB "Cancel";
            %dlg = MessageBoxCustom(%title, %text, %buttons);
            %dlg.callback[0] = "transferFromShopToDestinationsDirectoryPart2(false, true);";
            %dlg.callback[1] = "transferFromShopToDestinationsDirectoryPart2(false, false);";
            %dlg.callback[2] = "";
            return ;
        }
        else
        {
            %doSave = 0;
        }
    }
    ClosetGui.doClose(!%doSave, 0);
    toggleTGFMapFiltered("shop");
    return ;
}
$gAreaNamesInternalList = "";
function DestinationList::AddAreaNameInfo(%areaName, %shortName, %city, %userFacingName, %iconPath)
{
    $gAreaNamesShortName[%areaName] = %shortName ;
    $gAreaNamesCity[%areaName] = %city ;
    $gAreaNamesUserFacingName[%areaName] = %userFacingName ;
    $gAreaNamesIconPath[%areaName] = %iconPath ;
    $gAreaNamesInternalList = $gAreaNamesInternalList @ %areaName SPC "";
    return ;
}
function DestinationList::GetAreaNameCount()
{
    return getWordCount($gAreaNamesInternalList);
}
function DestinationList::GetRandomAreaName()
{
    return getRandomWord($gAreaNamesInternalList);
}
function DestinationList::GetAreaNameByIndex(%index)
{
    if ($gAreaNamesInternalList $= "")
    {
        return "";
    }
    return getWord($gAreaNamesInternalList, %index);
}
function DestinationList::GetAreaNameShortName(%areaName)
{
    return $gAreaNamesShortName[%areaName];
}
function DestinationList::GetAreaNameCity(%areaName)
{
    return $gAreaNamesCity[%areaName];
}
function DestinationList::GetAreaNameUserFacingName(%areaName)
{
    return $gAreaNamesUserFacingName[%areaName];
}
function DestinationList::GetAreaNameUserFacingNameCityAndBuildingShort(%areaName, %delimiter)
{
    %cityName = DestinationList::GetAreaNameCity(%areaName);
    %cityName = strupr(%cityName);
    if (%cityName $= %areaName)
    {
        return %cityName;
    }
    %bldgName = DestinationList::GetAreaNameShortName(%areaName);
    if (!isDefined("%delimiter"))
    {
        %delimiter = " - ";
    }
    return %cityName @ %delimiter @ %bldgName;
}
function DestinationList::GetAreaNameUserFacingNameCityAndBuilding(%areaName, %delimiter)
{
    %cityName = DestinationList::GetAreaNameCity(%areaName);
    %cityName = strupr(%cityName);
    if (%cityName $= %areaName)
    {
        return %cityName;
    }
    %bldgName = DestinationList::GetAreaNameUserFacingName(%areaName);
    if (!isDefined("%delimiter"))
    {
        %delimiter = " - ";
    }
    return %cityName @ %delimiter @ %bldgName;
}
function DestinationList::GetAreaNameIconPath(%areaName)
{
    return $gAreaNamesIconPath[%areaName];
}
