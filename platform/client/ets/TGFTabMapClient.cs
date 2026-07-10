$geTGF::DestinationNoFilterName = "All Destinations";
$geTGF::DestinationFilterCodes = "shop venue residence plaza";
$geTGF::DestinationFilterNames = "Shops Venues Residences Plazas";
$geTGF::DestinationFilterExclude = "NID";
$geTGF::DestinationThumbnails = "";
$geTGF::Map_PageTabVisited = 0;
$geTGF::Map_ApartmentVURL = "";
function geTGF_tabs::fillTabMap(%this) {
    %tabName = "map";
    %tab = %tabName.getTabWithName(%this);
    if (%tab.filled) {
        return;
    }
    %tab.filled = 1;
    %tab.fillTabGeneric(%this);
    $geTGF::Map_PageTabVisited = 1;
    1.openTGF(WorldMap);
    WorldMap.add(%tab);
    "0 -47".reposition(WorldMap);
    %worldctrl = %tab.Maps_buildSmallWorldControl(%this);
    %worldctrl.add(%tab);
    %destCtrl = %tab.Maps_buildDestControl(%this);
    %destCtrl.add(%tab);
    $geTGF::DestinationNoFilterName.add(TGFDestinationTypeList);
    getWord($geTGF::DestinationFilterNames, 0).add(TGFDestinationTypeList);
    getWord($geTGF::DestinationFilterNames, 1).add(TGFDestinationTypeList);
    getWord($geTGF::DestinationFilterNames, 2).add(TGFDestinationTypeList);
    getWord($geTGF::DestinationFilterNames, 3).add(TGFDestinationTypeList);
    0.SetSelected(TGFDestinationTypeList);
    TGFWorldMapMultiCitySmall.visible = 0;
    %this.Maps_filterType = "";
    %this.Maps_filterCity = "";
    %this.refreshTabMap();
};
function geTGF_tabs::onShowTabMap(%this) {
    cancel(geTGF.geTGF_Refresh_Schedule);
    0.setVisible(geTGF_Refresh);
    0.setActive(geTGF_Refresh);
};
function geTGF_tabs::refreshTabMap(%this) {
    %this.Maps_filterCity.Maps_filterDestinations(%this, %this.Maps_filterType);
    WorldMap.refresh();
    geTGF.mGeTabs.Maps_GetApartmentVURL();
};
function geTGF_tabs::Maps_buildDestControl(%this, %tab) {
    %padding = 4;
    %ctrlPosition = "682 4";
    %childRatio = (167.0 / 137.0);
    %tabExtent = %tab.getExtent();
    %ctrlExtent = ((getWord(%tabExtent, 0) - getWord(%ctrlPosition, 0)) - 1.0) @ " " @ ((getWord(%tabExtent, 1) - getWord(%ctrlPosition, 1)) - 1.0);
    %MainCtrl = new GuiBitmapCtrl(TGFDestinations) {
        profile = "GuiDefaultProfile";
        horizSizing = "width";
        vertSizing = "bottom";
        position = %ctrlPosition;
        extent = %ctrlExtent;
        sluggishness = 0.8;
    };
    %posX = (%padding + 4.0);
    %posY = 1;
    %textLabel = new GuiMLTextCtrl("") {
        profile = "ETSNonModalProfile";
        position = %posX @ " " @ %posY;
        extent = "65 20";
        text = mlStyle("<just:right>Show: ", "tgfWebLink_Light");
    };
    %textLabel.add(%MainCtrl);
    %posX = (%posX + (getWord(%textLabel.getExtent(), 0) + 2.0));
    %posY = (%posY + 1.0);
    %dropdown = new GuiPopUp2MenuCtrl(TGFDestinationTypeList) {
        profile = "InfoWindowPopupProfile";
        scrollProfile = "DottedScrollProfile";
        winProfile = "InfoWindowPopupWindowProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = %posX @ " " @ %posY;
        extent = ((getWord(%ctrlExtent, 0) - %posX) - 1.0) @ " " @ 20;
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        command = %this @ ".Maps_changedTypeFilter(TGFDestinationTypeList);";
        text = "";
        maxLength = 255;
        maxPopupHeight = 200;
        allowReverse = 0;
    };
    %dropdown.command = %this @ ".Maps_changedTypeFilter(" @ %dropdown @ ");";
    %dropdown.add(%MainCtrl);
    %posX = 1;
    %posY = (%posY + (20.0 + 1.0));
    %scroll = new GuiScrollCtrl(TGFDestinationsScrollList) {
        profile = "DottedScrollDarkProfile";
        horizSizing = "right";
        vertSizing = "height";
        position = %posX @ " " @ %posY;
        extent = ((getWord(%ctrlExtent, 0) - %posX) - 1.0) @ " " @ ((getWord(%ctrlExtent, 1) - %posY) - 1.0);
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        canHilite = 1;
        allowAutoFirstResponderUpdates = 1;
        willFirstRespond = 1;
        hScrollBar = "alwaysOff";
        vScrollBar = "alwaysOn";
        constantThumbHeight = 0;
        childMargin = "0 0";
        saneDrag = 1;
        scrollMultiplier = 4;
        stickyBottom = 0;
        border = 1;
    };
    new GuiArray2Ctrl(TGFDestinationsArray) {
        profile = "CSProfileListBox";
        horizSizing = "width";
        vertSizing = "height";
        position = "1 1";
        extent = "267 465";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        canHilite = 1;
        allowAutoFirstResponderUpdates = 1;
        numRowsOrCols = 2;
        childrenExtent = "137 167";
        inRows = 0;
        spacing = %padding;
        childrenClassName = "GuiBitmapCtrl";
        keyWrapX = 1;
        keyWrapY = 0;
        hilited = 0;
        lastClicked = 0;
        paddingAboveText = 1;
        scroll = "TGFDestinationsScrollList";
    };
    %nowhere = new GuiMLTextCtrl(TGFDestinationsNowhere) {
        profile = "ETSNonModalProfile";
        position = 11 @ " " @ 32;
        extent = "227 20";
        text = mlStyle("Sorry, no destinations here.", "tgfWebLink_Light");
        visible = 0;
    };
    %scroll.add(%MainCtrl);
    %nowhere.add(%MainCtrl);
    %arrayWidth = (getWord(%scroll.extent, 0) - 12.0);
    %childWidth = mFloor(((%arrayWidth - (%padding * 3.0)) / 2.0));
    %childHeight = mFloor((%childWidth * %childRatio));
    TGFDestinationsArray.childrenExtent = %childWidth @ " " @ %childHeight;
    %MainCtrl.childHeightDelta = (%childHeight + %padding);
    return %MainCtrl;
};
function geTGF_tabs::Maps_buildSmallWorldControl(%this, %tab) {
    %origSize = "269 187";
    %ratio = (269.0 / 187.0);
    %ctrlExtent = "275 156";
    %ctrlPosition = "682 4";
    %expbtnExt = "30 19";
    %MainCtrl = new GuiBitmapCtrl(TGFWorldMapMultiCitySmall) {
        profile = "GuiDefaultProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = %ctrlPosition;
        extent = %ctrlExtent;
        minExtent = "1 1";
        visible = 1;
        bitmap = "platform/client/ui/small_multi_city_bkgd";
    };
    %expandBtn = new GuiBitmapButtonCtrl("") {
        profile = "GuiDefaultProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = ((getWord(%ctrlExtent, 0) - getWord(%expbtnExt, 0)) - 2.0) @ " " @ ((getWord(%ctrlExtent, 1) - getWord(%expbtnExt, 1)) - 2.0);
        extent = %expbtnExt;
        minExtent = "1 1";
        visible = 1;
        command = "WorldMap.setView(\"multi_city\");";
        text = "";
        groupNum = -1;
        buttonType = "PushButton";
        bitmap = "platform/client/buttons/expand";
        drawText = 0;
    };
    %expandBtn.add(%MainCtrl);
    %rY = (getWord(%ctrlExtent, 1) / getWord(%origSize, 1));
    %rX = %rY;
    %size = WorldMapCityInfoMap.size();
    %i = 0;
    while ((%i < %size)) {
        %cityName = %i.getValue(WorldMapCityInfoMap).name;
        %smallCityButton = 1.getCityButton(WorldMap, %cityName, 1);
        %oldPosition = %smallCityButton.position;
        %smallCityButton.position = mFloor((getWord(%oldPosition, 0) * %rX)) @ " " @ mFloor((getWord(%oldPosition, 1) * %rY));
        %smallCityButton.add(%MainCtrl);
        %MainCtrl.citybutton = %smallCityButton @ %cityName;
        %i = (%i + 1.0);
    }
    return %MainCtrl;
};
function TGFDestinationsArray::onCreatedChild(%this, %child) {
    %extent = %child.getExtent();
    %button = new GuiBitmapButtonCtrl("") {
        profile = "GuiDefaultProfile";
        position = "0 0";
        extent = %extent;
        bitmap = "platform/client/buttons/tgf/tgf_buttonframe_125x152";
        command = "";
    };
    %child.button = %button;
    %height = 20;
    %boxExtent = getWord(%child.getExtent(), 0) @ " " @ %height;
    %zoomExtent = "12 18";
    %box = new GuiControl("") {
        profile = "EtsDarkBorderlessBoxProfile";
        extent = %boxExtent;
        position = 0 @ " " @ (getWord(%child.getExtent(), 1) - %height);
    };
    %cityName = new GuiMLTextCtrl("") {
        profile = "ETSNonModalProfile";
        position = "4 1";
        extent = ((getWord(%box.extent, 0) - getWord(%zoomExtent, 0)) + 1.0) @ " " @ 18;
        text = mlStyle("city", "tgfItem_DestinationCity");
    };
    %cityName.add(%box);
    %contiguous = new GuiBitmapCtrl("") {
        extent = %zoomExtent;
        position = ((getWord(%boxExtent, 0) - getWord(%zoomExtent, 0)) - 1.0) @ " " @ 1;
        profile = "EtsNonModalProfile";
        bitmap = "platform/client/ui/tgf/tgf_map_fasttravel";
        visible = 0;
    };
    %contiguous.add(%box);
    %child.contiguous = %contiguous;
    %child.cityName = %cityName;
    %box.add(%child);
    %button.add(%child);
};
function geTGF_tabs::Maps_changedTypeFilter(%this, %dropdown) {
    %text = %dropdown.getText();
    %idx = %text.findText(%dropdown);
    if ((%idx < 0.0)) {
        return;
    }
    %typefilter = "";
    if ((%idx > 0.0)) {
        %typefilter = getWord($geTGF::DestinationFilterCodes, (%idx - 1.0));
    }
    %this.Maps_filterCity.Maps_filterDestinations(%this, %typefilter);
};
function geTGF_tabs::Maps_changedCityFilter(%this, %cityName) {
    if ((%cityName $= "multi_city")) {
        %cityName = "";
    }
    if (!(%cityName $= %this.Maps_filterCity)) {
        if (!(%cityName $= "")) {
        }
        if ((TGFWorldMapMultiCitySmall.isVisible() == 0.0)) {
            %pos = TGFDestinations.getPosition();
            %ext = TGFDestinations.getExtent();
            (getWord(%ext, 1) - TGFDestinations.childHeightDelta).resize(TGFDestinations, getWord(%ext, 0));
            getWord(%pos, 0) @ " " @ (getWord(%pos, 1) + TGFDestinations.childHeightDelta).setTrgPosition(TGFDestinations);
            1.setVisible(TGFWorldMapMultiCitySmall);
        }
        if ((%cityName $= "")) {
        }
        if ((TGFWorldMapMultiCitySmall.isVisible() == 1.0)) {
            %pos = TGFDestinations.getPosition();
            %ext = TGFDestinations.getExtent();
            (getWord(%ext, 1) + TGFDestinations.childHeightDelta).resize(TGFDestinations, getWord(%ext, 0));
            getWord(%pos, 0) @ " " @ (getWord(%pos, 1) - TGFDestinations.childHeightDelta).setTrgPosition(TGFDestinations);
            0.setVisible(TGFWorldMapMultiCitySmall);
        }
    }
    %cityName.Maps_filterDestinations(%this, %this.Maps_filterType);
};
function geTGF_tabs::Maps_updateFiltering(%this) {
    %this.Maps_filterCity.Maps_filterDestinations(%this, %this.Maps_filterType);
};
function geTGF_tabs::Maps_filterDestinationsByType(%this, %filterType) {
    %this.Maps_filterCity.Maps_filterDestinations(%this, %filterType);
};
function geTGF_tabs::Maps_filterDestinationsByCity(%this, %filterCity) {
    %filterCity.Maps_filterDestinations(%this, %this.Maps_filterType);
};
function geTGF_tabs::Maps_filterDestinations(%this, %type, %city) {
    %dests = "";
    %lastCityCheck = "";
    %cityAvailable = 1;
    %count = getWordCount($gDestinationNamesInternal);
    %idx = 0;
    while ((%idx < %count)) {
        %use = 1;
        %destCode = getWord($gDestinationNamesInternal, %idx);
        if (!(%type $= "") && (findWord(%destCode[$gDestinationFilters @ %destCode], %type) < 0.0)) {
            %use = 0;
        }
        if (!(%lastCityCheck $= %destCode[$gDestinationSpaces @ %destCode])) {
            %lastCityCheck = %destCode[$gDestinationSpaces @ %destCode];
            %cityAvailable = %lastCityCheck.isServerForCity(WorldMap);
        }
        if (!(%city $= "") && !(%city $= %destCode[$gDestinationSpaces @ %destCode])) {
            %use = 0;
        }
        if ((strstr(%destCode[$gDestinationFilters @ %destCode], $geTGF::DestinationFilterExclude) >= 0.0)) {
            %use = 0;
        }
        if (%use) {
            %dests = %dests @ " " @ %destCode;
        }
        %idx = (%idx + 1.0);
    }
    %dests = trim(%dests);
    (%idx < %count);
    %neardests = "";
    %fardests = "";
    %count = getWordCount(%dests);
    %this.Maps_filterType = %type;
    %this.Maps_filterCity = %city;
    %idx = 0;
    if (!(%type $= "")) {
        %idx = findWord($geTGF::DestinationFilterCodes, %type);
        %idx = (%idx + 1.0);
        if ((%idx < 0.0)) {
            %idx = 0;
        }
    }
    %idx.SetSelected(TGFDestinationTypeList);
    if ((%count == 0.0)) {
        TGFDestinationsNowhere.visible = 1;
        TGFDestinationsScrollList.visible = 0;
        return;
    }
    TGFDestinationsNowhere.visible = 0;
    TGFDestinationsScrollList.visible = 1;
    %idx = 0;
    while ((%idx < %count)) {
        %destCode = getWord(%dests, %idx);
        if (DestinationList::IsDestinationInMyContiguousSpace(%destCode)) {
            %neardests = %neardests @ " " @ %destCode;
        }
        %fardests = %fardests @ " " @ %destCode;
        %idx = (%idx + 1.0);
    }
    %neardests = trim(%neardests);
    (%idx < %count);
    %fardests = trim(%fardests);
    %dests = trim(%neardests @ " " @ %fardests);
    %count = getWordCount(%dests);
    %count.setNumChildren(TGFDestinationsArray);
    "venue".clearItemList(geTGF, "map");
    %bitmapDelay = 3000;
    %bitmapInc = 75;
    %idx = 0;
    while ((%idx < %count)) {
        %destCode = getWord(%dests, %idx);
        %child = %idx.getObject(TGFDestinationsArray);
        %thumbnail = DestinationList::getBitmapLocation(%destCode);
        if ((findWord($geTGF::DestinationThumbnails, %thumbnail) < 0.0)) {
            %thumbnail.schedule(%child, (%bitmapDelay + (%bitmapInc * %idx)), "setBitmap");
            if (($geTGF::DestinationThumbnails $= "")) {
                $geTGF::DestinationThumbnails = %thumbnail;
            }
            $geTGF::DestinationThumbnails = $geTGF::DestinationThumbnails @ " " @ %thumbnail;
        }
        %thumbnail.setBitmap(%child);
        %child.button.command = "geTGF_tabs::Maps_clickLocation(\"" @ %destCode @ "\");";
        %child.contiguous.visible = DestinationList::IsDestinationInMyContiguousSpace(%destCode);
        %citname = strupr(DestinationList::getDestinationContiguousSpace(%destCode));
        mlStyle(%citname, "tgfItem_DestinationCity").setText(%child.cityName);
        %item = %destCode.createNewItem(geTGF, "map", "venue");
        %item.codeName = %destCode;
        %idx = (%idx + 1.0);
    }
};
function geTGF::map_GetAndOpenDetailsContainer(%this, %item) {
    %item.constructDeetsWindow(%this, geDeetsWindow);
    1.setVisible(geDeetsLayer);
    return geDeetsWindow;
};
function geTGF_tabs::Maps_clickLocation(%destCode) {
    %item = %destCode.findItem(geTGF, "map", "venue");
    %item.DoDetails(geTGF, "map");
};
