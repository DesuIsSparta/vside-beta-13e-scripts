$geTGF::DestinationNoFilterName = "All Destinations";
$geTGF::DestinationFilterCodes = "shop venue residence plaza";
$geTGF::DestinationFilterNames = "Shops Venues Residences Plazas";
$geTGF::DestinationFilterExclude = "NID";
$geTGF::DestinationThumbnails = "";
$geTGF::Map_PageTabVisited = 0;
$geTGF::Map_ApartmentVURL = "";
function geTGF_tabs::fillTabMap(%this) {
    %tabName = "map";
    %tab = %this.getTabWithName(%tabName);
    if (%tab.filled) {
        return;
    }
    %tab.filled = 1;
    %this.fillTabGeneric(%tab);
    $geTGF::Map_PageTabVisited = 1;
    WorldMap.openTGF(1);
    %tab.add();
    WorldMap.reposition("0 -47");
    %worldctrl = %this.Maps_buildSmallWorldControl(%tab);
    WorldMap;
    %tab.add(%worldctrl);
    %destCtrl = %this.Maps_buildDestControl(%tab);
    %tab.add(%destCtrl);
    TGFDestinationTypeList.add($geTGF::DestinationNoFilterName);
    TGFDestinationTypeList.add(getWord($geTGF::DestinationFilterNames, 0));
    TGFDestinationTypeList.add(getWord($geTGF::DestinationFilterNames, 1));
    TGFDestinationTypeList.add(getWord($geTGF::DestinationFilterNames, 2));
    TGFDestinationTypeList.add(getWord($geTGF::DestinationFilterNames, 3));
    TGFDestinationTypeList.SetSelected(0);
    %tab.visible = 0 @ TGFWorldMapMultiCitySmall;
    %this.Maps_filterType = "";
    %this.Maps_filterCity = "";
    %this.refreshTabMap();
};
function geTGF_tabs::onShowTabMap(%this) {
    cancel(geTGF, %this.geTGF_Refresh_Schedule);
    geTGF_Refresh.setVisible(0);
    geTGF_Refresh.setActive(0);
};
function geTGF_tabs::refreshTabMap(%this) {
    %this.Maps_filterDestinations(%this.Maps_filterType, %this.Maps_filterCity);
    WorldMap.refresh();
    geTGF.Maps_GetApartmentVURL(%this.mGeTabs);
};
function geTGF_tabs::Maps_buildDestControl(%this, %tab) {
    %padding = 4;
    %ctrlPosition = "682 4";
    %childRatio = (137.0 / 167.0);
    %tabExtent = %tab.getExtent();
    %ctrlExtent = (1.0 - (getWord(%ctrlPosition, 0) - getWord(%tabExtent, 0))) @ " " @ (1.0 - (getWord(%ctrlPosition, 1) - getWord(%tabExtent, 1)));
    %MainCtrl = new GuiBitmapCtrl(TGFDestinations) {
        profile = "GuiDefaultProfile";
        horizSizing = "width";
        vertSizing = "bottom";
        position = %ctrlPosition;
        extent = %ctrlExtent;
        sluggishness = 0.8;
    };
    %posX = (4.0 + %padding);
    %posY = 1;
    %textLabel = new GuiMLTextCtrl("") {
        profile = 0 @ "ETSNonModalProfile";
        position = %posX @ " " @ %posY;
        extent = "65 20";
        text = mlStyle("<just:right>Show: ", "tgfWebLink_Light");
    };
    %MainCtrl.add(%textLabel);
    %posX = ((2.0 + getWord(%textLabel.getExtent(), 0)) + %posX);
    %posY = (1.0 + %posY);
    %dropdown = new GuiPopUp2MenuCtrl(TGFDestinationTypeList) {
        profile = "InfoWindowPopupProfile";
        scrollProfile = "DottedScrollProfile";
        winProfile = "InfoWindowPopupWindowProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = %posX @ " " @ %posY;
        extent = (1.0 - (%posX - getWord(%ctrlExtent, 0))) @ " " @ 20;
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
    %MainCtrl.add(%dropdown);
    %posX = 1;
    %posY = ((1.0 + 20.0) + %posY);
    %scroll = new GuiScrollCtrl(TGFDestinationsScrollList) {
        profile = "DottedScrollDarkProfile";
        horizSizing = "right";
        vertSizing = "height";
        position = %posX @ " " @ %posY;
        extent = (1.0 - (%posX - getWord(%ctrlExtent, 0))) @ " " @ (1.0 - (%posY - getWord(%ctrlExtent, 1)));
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
    %MainCtrl.add(%scroll);
    %MainCtrl.add(%nowhere);
    %arrayWidth = (12.0 - getWord(%scroll.extent, 0));
    %childWidth = mFloor((2.0 / ((3.0 * %padding) - %arrayWidth)));
    %childHeight = mFloor((%childRatio * %childWidth));
    %scroll.childrenExtent = %childWidth @ " " @ %childHeight @ TGFDestinationsArray;
    %MainCtrl.childHeightDelta = (%padding + %childHeight);
    return %MainCtrl;
};
function geTGF_tabs::Maps_buildSmallWorldControl(%this, %tab) {
    %origSize = "269 187";
    %ratio = (187.0 / 269.0);
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
        profile = 0 @ "GuiDefaultProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = (2.0 - (getWord(%expbtnExt, 0) - getWord(%ctrlExtent, 0))) @ " " @ (2.0 - (getWord(%expbtnExt, 1) - getWord(%ctrlExtent, 1)));
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
    %MainCtrl.add(%expandBtn);
    %rY = (getWord(%origSize, 1) / getWord(%ctrlExtent, 1));
    %rX = %rY;
    %size = WorldMapCityInfoMap.size();
    %i = 0;
    if ((%size < %i)) {
        %cityName = WorldMapCityInfoMap.getValue(%i).name;
        %smallCityButton = WorldMap.getCityButton(%cityName, 1, 1);
        %oldPosition = %smallCityButton.position;
        %smallCityButton.position = mFloor((%rX * getWord(%oldPosition, 0))) @ " " @ mFloor((%rY * getWord(%oldPosition, 1)));
        %MainCtrl.add(%smallCityButton);
        %MainCtrl.citybutton = %smallCityButton @ %cityName;
        %i = (1.0 + %i);
    }
    return %MainCtrl;
};
function TGFDestinationsArray::onCreatedChild(%this, %child) {
    %extent = %child.getExtent();
    %button = new GuiBitmapButtonCtrl("") {
        profile = 0 @ "GuiDefaultProfile";
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
        profile = 0 @ "EtsDarkBorderlessBoxProfile";
        extent = %boxExtent;
        position = 0 @ " " @ (%height - getWord(%child.getExtent(), 1));
    };
    %cityName = new GuiMLTextCtrl("") {
        profile = 0 @ "ETSNonModalProfile";
        position = "4 1";
        extent = (1.0 + (getWord(%zoomExtent, 0) - getWord(%box.extent, 0))) @ " " @ 18;
        text = mlStyle("city", "tgfItem_DestinationCity");
    };
    %box.add(%cityName);
    %contiguous = new GuiBitmapCtrl("") {
        extent = 0 @ %zoomExtent;
        position = (1.0 - (getWord(%zoomExtent, 0) - getWord(%boxExtent, 0))) @ " " @ 1;
        profile = "EtsNonModalProfile";
        bitmap = "platform/client/ui/tgf/tgf_map_fasttravel";
        visible = 0;
    };
    %box.add(%contiguous);
    %child.contiguous = %contiguous;
    %child.cityName = %cityName;
    %child.add(%box);
    %child.add(%button);
};
function geTGF_tabs::Maps_changedTypeFilter(%this, %dropdown) {
    %text = %dropdown.getText();
    %idx = %dropdown.findText(%text);
    if ((0.0 < %idx)) {
        return;
    }
    %typefilter = "";
    if ((0.0 > %idx)) {
        %typefilter = getWord($geTGF::DestinationFilterCodes, (1.0 - %idx));
    }
    %this.Maps_filterDestinations(%typefilter, %this.Maps_filterCity);
};
function geTGF_tabs::Maps_changedCityFilter(%this, %cityName) {
    if ((%cityName $= "multi_city")) {
        %cityName = "";
    }
    if (!(%cityName $= %this.Maps_filterCity)) {
        if (!(%cityName $= "")) {
        }
        if ((0.0 == TGFWorldMapMultiCitySmall.isVisible())) {
            %pos = TGFDestinations.getPosition();
            %ext = TGFDestinations.getExtent();
            TGFDestinations.resize(getWord(%ext, 0), TGFDestinations, (%this.childHeightDelta - getWord(%ext, 1)));
            TGFDestinations.setTrgPosition(getWord(%pos, 0) @ " ", TGFDestinations @ (%this.childHeightDelta + getWord(%pos, 1)));
            TGFWorldMapMultiCitySmall.setVisible(1);
        }
        if ((%cityName $= "")) {
        }
        if ((1.0 == TGFWorldMapMultiCitySmall.isVisible())) {
            %pos = TGFDestinations.getPosition();
            %ext = TGFDestinations.getExtent();
            TGFDestinations.resize(getWord(%ext, 0), TGFDestinations, (%this.childHeightDelta + getWord(%ext, 1)));
            TGFDestinations.setTrgPosition(getWord(%pos, 0) @ " ", TGFDestinations @ (%this.childHeightDelta - getWord(%pos, 1)));
            TGFWorldMapMultiCitySmall.setVisible(0);
        }
    }
    %this.Maps_filterDestinations(%this.Maps_filterType, %cityName);
};
function geTGF_tabs::Maps_updateFiltering(%this) {
    %this.Maps_filterDestinations(%this.Maps_filterType, %this.Maps_filterCity);
};
function geTGF_tabs::Maps_filterDestinationsByType(%this, %filterType) {
    %this.Maps_filterDestinations(%filterType, %this.Maps_filterCity);
};
function geTGF_tabs::Maps_filterDestinationsByCity(%this, %filterCity) {
    %this.Maps_filterDestinations(%this.Maps_filterType, %filterCity);
};
function geTGF_tabs::Maps_filterDestinations(%this, %type, %city) {
    %dests = "";
    %lastCityCheck = "";
    %cityAvailable = 1;
    %count = getWordCount($gDestinationNamesInternal);
    %idx = 0;
    if ((%count < %idx)) {
        %use = 1;
        %destCode = getWord($gDestinationNamesInternal, %idx);
        if (!(%type $= "")) {
            if ((0.0 < findWord(%destCode[$gDestinationFilters @ %destCode], %type))) {
                %use = 0;
            }
        }
        if (!(%lastCityCheck $= %destCode[$gDestinationSpaces @ %destCode])) {
            %lastCityCheck = %destCode[$gDestinationSpaces @ %destCode];
            %cityAvailable = WorldMap.isServerForCity(%lastCityCheck);
        }
        if (!(%city $= "")) {
            if (!(%city $= %destCode[$gDestinationSpaces @ %destCode])) {
                %use = 0;
            }
        }
        if ((0.0 >= strstr(%destCode[$gDestinationFilters @ %destCode], $geTGF::DestinationFilterExclude))) {
            %use = 0;
        }
        if (%use) {
            %dests = %dests @ " " @ %destCode;
        }
        %idx = (1.0 + %idx);
    }
    %dests = trim(%dests);
    (%count < %idx);
    %neardests = "";
    %fardests = "";
    %count = getWordCount(%dests);
    %this.Maps_filterType = %type;
    %this.Maps_filterCity = %city;
    %idx = 0;
    if (!(%type $= "")) {
        %idx = findWord($geTGF::DestinationFilterCodes, %type);
        %idx = (1.0 + %idx);
        if ((0.0 < %idx)) {
            %idx = 0;
        }
    }
    TGFDestinationTypeList.SetSelected(%idx);
    if ((0.0 == %count)) {
        %this.visible = 1 @ TGFDestinationsNowhere;
        %this.visible = 0 @ TGFDestinationsScrollList;
        return;
    }
    %this.visible = 0 @ TGFDestinationsNowhere;
    %this.visible = 1 @ TGFDestinationsScrollList;
    %idx = 0;
    if ((%count < %idx)) {
        %destCode = getWord(%dests, %idx);
        if (DestinationList::IsDestinationInMyContiguousSpace(%destCode)) {
            %neardests = %neardests @ " " @ %destCode;
        }
        %fardests = %fardests @ " " @ %destCode;
        %idx = (1.0 + %idx);
    }
    %neardests = trim(%neardests);
    (%count < %idx);
    %fardests = trim(%fardests);
    %dests = trim(%neardests @ " " @ %fardests);
    %count = getWordCount(%dests);
    TGFDestinationsArray.setNumChildren(%count);
    geTGF.clearItemList("map", "venue");
    %bitmapDelay = 3000;
    %bitmapInc = 75;
    %idx = 0;
    if ((%count < %idx)) {
        %destCode = getWord(%dests, %idx);
        %child = TGFDestinationsArray.getObject(%idx);
        %thumbnail = DestinationList::getBitmapLocation(%destCode);
        if ((0.0 < findWord($geTGF::DestinationThumbnails, %thumbnail))) {
            %child.schedule(((%idx * %bitmapInc) + %bitmapDelay), "setBitmap", %thumbnail);
            if (($geTGF::DestinationThumbnails $= "")) {
                $geTGF::DestinationThumbnails = %thumbnail;
            }
            $geTGF::DestinationThumbnails = $geTGF::DestinationThumbnails @ " " @ %thumbnail;
        }
        %child.setBitmap(%thumbnail);
        %child.button.command = "geTGF_tabs::Maps_clickLocation(\"" @ %destCode @ "\");";
        %child.contiguous.visible = DestinationList::IsDestinationInMyContiguousSpace(%destCode);
        %citname = strupr(DestinationList::getDestinationContiguousSpace(%destCode));
        %child.cityName.setText(mlStyle(%citname, "tgfItem_DestinationCity"));
        %item = geTGF.createNewItem("map", "venue", %destCode);
        %item.codeName = %destCode;
        %idx = (1.0 + %idx);
    }
};
function geTGF::map_GetAndOpenDetailsContainer(%this, %item) {
    %this.constructDeetsWindow(geDeetsWindow, %item);
    geDeetsLayer.setVisible(1);
};
function geTGF_tabs::Maps_clickLocation(%destCode) {
    %item = geTGF.findItem("map", "venue", %destCode);
    geTGF.DoDetails("map", %item);
};
