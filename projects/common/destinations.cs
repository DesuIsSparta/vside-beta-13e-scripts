$gDestinationNamesInternal = "";
$gDestinationFiltersInUse = "";
$gDestinationFiltersInDirectory = "event" @ " " @ "shop" @ " " @ "venue" @ " " @ "plaza" @ " " @ "residence";
function DestinationList::AddDestinationInfo(%codeName, %filters, %contiguousSpaceNames, %userFacingName, %userFacingDescriptionBase, %inWorldTrailer, %vurl, %okayForTGF) {
    %codeName[$gDestinationFilters @ %codeName] = %filters;
    %codeName[$gDestinationNames @ %codeName] = %userFacingName;
    %codeName[$gDestinationDescsInCloset @ %codeName] = %userFacingDescriptionBase;
    %codeName[$gDestinationDescsInWorld @ %codeName] = "Welcome to" @ " " @ %userFacingName @ "!" @ " " @ %inWorldTrailer;
    %codeName[$gDestinationSpaces @ %codeName] = %contiguousSpaceNames;
    %codeName[$gDestinationVurls @ %codeName] = %vurl;
    $gDestinationNamesInternal = $gDestinationNamesInternal @ %codeName @ " ";
    %i = (1.0 - getWordCount(%filters));
    if ((0.0 >= %i)) {
        %filter = getWord(%filters, %i);
        if ((%filter $= "shop")) {
            %codeName[$gStoreStockCacheSkus @ %codeName] = "";
            %codeName[$gStoreStockRevision @ %codeName] = "";
        }
        if ((0.0 < findWord($gDestinationFiltersInUse, %filter))) {
            $gDestinationFiltersInUse = $gDestinationFiltersInUse @ %filter @ " ";
        }
        if ((0.0 >= findWord($gDestinationFiltersInDirectory, %filter))) {
            DestinationList::AddDestinationAd(%codeName, %vurl, %okayForTGF);
        }
        %i = (1.0 - %i);
    }
};
function DestinationList::getDestinationContiguousSpace(%codeName) {
    return %codeName[$gDestinationSpaces @ %codeName];
};
function DestinationList::IsDestinationInMyContiguousSpace(%codeName) {
    if (($gContiguousSpaceName $= "")) {
        return 0;
    }
    return (0.0 >= findWord(DestinationList::getDestinationContiguousSpace(%codeName), $gContiguousSpaceName));
};
function DestinationList::getCityNameForDestination(%codeName) {
    %dest = DestinationList::getDestinationContiguousSpace(%codeName);
    return getContiguousSpaceFullName(%dest);
};
function DestinationList::getBitmapLocation(%codeName) {
    if (("" $= %codeName)) {
        return 0;
    }
    %filter = getWord(%codeName[$gDestinationFilters @ %codeName], 0);
    if (("" $= %filter)) {
        return 0;
    }
    return "platform/client/buttons/" @ %filter @ "ads/" @ %filter @ "ad_" @ %codeName;
};
function DestinationList::getOccluderBitmapLocation() {
    return "platform/client/buttons/destinationsDirectoryOccluder";
};
$gDestinationAdsNum = 0;
function DestinationList::AddDestinationAd(%codeName, %vurl, %okayForTGF) {
    %isNewEntry = (%codeName[$gDestinationAdsNumByName @ %codeName] $= "");
    $gDestinationAdsNum[%codeName @ $gDestinationAds TAB $gDestinationAdsNum @ "codename"] = ;
    $gDestinationAdsNum[%okayForTGF @ $gDestinationAds TAB $gDestinationAdsNum @ "okayForTGF"] = ;
    if (%isNewEntry) {
        %codeName[$gDestinationAdsNumByName @ %codeName] = $gDestinationAdsNum;
        $gDestinationAdsNum = (1.0 + $gDestinationAdsNum);
    }
};
function DestinationList::GetRandomDestinationForTGF(%filter, %butNotThese) {
    if (!(isDefined("%butNotThese"))) {
        %butNotThese = "interscope_lounge";
    }
    %candidates = "";
    %delim = "";
    %n = 0;
    if (($gDestinationAdsNum < %n)) {
        %eligible = %n[$gDestinationAds TAB %n @ "okayForTGF"];
        if (!(%eligible)) {
        }
        %codeName = %n[$gDestinationAds TAB %n @ "codename"];
        if (hasWord(%butNotThese, %codeName)) {
        }
        %filters = %codeName[$gDestinationFilters @ %codeName];
        if (hasWord(%filters, %filter)) {
            %candidates = %candidates @ %delim @ %codeName;
            %delim = " ";
        }
        %n = (1.0 + %n);
    }
    if ((($gDestinationAdsNum < %n) @ " " @ %candidates $= "")) {
        error("Unable to find any candidates for filter \"" @ %filter @ "\"." @ " " @ getTrace());
        return "";
    }
    return getRandomWord(%candidates);
};
function DestinationList::goToDestination(%codeName) {
    if ((0.0 < findWord($gDestinationNamesInternal, %codeName))) {
        return;
    }
    %vurl = %codeName[$gDestinationVurls @ %codeName];
    if ((%vurl $= "")) {
        error(getScopeName() @ " " @ "- no vurl for destination" @ " " @ %codeName);
        return;
    }
    %command = "geTGF.close();" @ " " @ "vurlOperation(\"" @ %vurl @ "\");";
    if (!(DestinationList::IsDestinationInMyContiguousSpace(%codeName))) {
        %title = ;
        %body = strreplace(%title[$MsgCat::destinations @ "REMOTE-BODY"], "[NAME]", %codeName[$gDestinationNames @ %codeName]);
        MessageBoxOkCancel(%title, %body, %command, "");
    }
    eval(%command);
};
function transferFromShopToDestinationsDirectory() {
    transferFromShopToDestinationsDirectoryPart2(1, 0);
};
function transferFromShopToDestinationsDirectoryPart2(%askForSave, %doSave) {
    if (%askForSave) {
        %askForSave = ClosetGui.userHasChangedBodyOrOutfit();
        if (%askForSave) {
            %title = "Save Your Changes";
            %text = "You've made some changes to your appearance.\nWould you like to save your changes?";
            %buttons = "Yes" @ "\t" @ "No" @ "\t" @ "Cancel";
            %dlg = MessageBoxCustom(%title, %text, %buttons);
            %dlg.callback = "transferFromShopToDestinationsDirectoryPart2(false, true);" @ 0;
            %dlg.callback = "transferFromShopToDestinationsDirectoryPart2(false, false);" @ 1;
            %dlg.callback = "" @ 2;
            return;
        }
        %doSave = 0;
    }
    !(%doSave).doClose(0);
    toggleTGFMapFiltered("shop");
};
$gAreaNamesInternalList = "";
function DestinationList::AddAreaNameInfo(%areaName, %shortName, %city, %userFacingName, %iconPath) {
    %areaName[$gAreaNamesShortName @ %areaName] = %shortName;
    %areaName[$gAreaNamesCity @ %areaName] = %city;
    %areaName[$gAreaNamesUserFacingName @ %areaName] = %userFacingName;
    %areaName[$gAreaNamesIconPath @ %areaName] = %iconPath;
    $gAreaNamesInternalList = $gAreaNamesInternalList @ %areaName @ " " @ "";
};
function DestinationList::GetAreaNameCount() {
    return getWordCount($gAreaNamesInternalList);
};
function DestinationList::GetRandomAreaName() {
    return getRandomWord($gAreaNamesInternalList);
};
function DestinationList::GetAreaNameByIndex(%index) {
    if (($gAreaNamesInternalList $= "")) {
        return "";
    }
    return getWord($gAreaNamesInternalList, %index);
};
function DestinationList::GetAreaNameShortName(%areaName) {
    return %areaName[$gAreaNamesShortName @ %areaName];
};
function DestinationList::GetAreaNameCity(%areaName) {
    return %areaName[$gAreaNamesCity @ %areaName];
};
function DestinationList::GetAreaNameUserFacingName(%areaName) {
    return %areaName[$gAreaNamesUserFacingName @ %areaName];
};
function DestinationList::GetAreaNameUserFacingNameCityAndBuildingShort(%areaName, %delimiter) {
    %cityName = DestinationList::GetAreaNameCity(%areaName);
    %cityName = strupr(%cityName);
    if ((%cityName $= %areaName)) {
        return %cityName;
    }
    %bldgName = DestinationList::GetAreaNameShortName(%areaName);
    if (!(isDefined("%delimiter"))) {
        %delimiter = " - ";
    }
    return %cityName @ %delimiter @ %bldgName;
};
function DestinationList::GetAreaNameUserFacingNameCityAndBuilding(%areaName, %delimiter) {
    %cityName = DestinationList::GetAreaNameCity(%areaName);
    %cityName = strupr(%cityName);
    if ((%cityName $= %areaName)) {
        return %cityName;
    }
    %bldgName = DestinationList::GetAreaNameUserFacingName(%areaName);
    if (!(isDefined("%delimiter"))) {
        %delimiter = " - ";
    }
    return %cityName @ %delimiter @ %bldgName;
};
function DestinationList::GetAreaNameIconPath(%areaName) {
    return %areaName[$gAreaNamesIconPath @ %areaName];
};
