$geTGF::DestinationNoFilterName = "All Destinations";
$geTGF::DestinationFilterCodes = "shop venue residence plaza";
$geTGF::DestinationFilterNames = "Shops Venues Residences Plazas";
$geTGF::DestinationFilterExclude = "NID";
$geTGF::DestinationThumbnails = "";
$geTGF::Map_PageTabVisited = 0;
$geTGF::Map_ApartmentVURL = "";
function geTGF_tabs::fillTabMap(%this)
{
    %tabName = "map";
    %tab = %this.getTabWithName(%tabName);
    if (%tab.filled)
    {
        return ;
    }
    %tab.filled = 1;
    %this.fillTabGeneric(%tab);
    $geTGF::Map_PageTabVisited = 1;
    WorldMap.openTGF(1);
    %tab.add(WorldMap);
    WorldMap.reposition("0 -47");
    %worldctrl = %this.Maps_buildSmallWorldControl(%tab);
    %tab.add(%worldctrl);
    %destCtrl = %this.Maps_buildDestControl(%tab);
    %tab.add(%destCtrl);
    TGFDestinationTypeList.add($geTGF::DestinationNoFilterName);
    TGFDestinationTypeList.add(getWord($geTGF::DestinationFilterNames, 0));
    TGFDestinationTypeList.add(getWord($geTGF::DestinationFilterNames, 1));
    TGFDestinationTypeList.add(getWord($geTGF::DestinationFilterNames, 2));
    TGFDestinationTypeList.add(getWord($geTGF::DestinationFilterNames, 3));
    TGFDestinationTypeList.SetSelected(0);
    TGFWorldMapMultiCitySmall.visible = 0;
    %this.Maps_filterType = "";
    %this.Maps_filterCity = "";
    %this.refreshTabMap();
    return ;
}
function geTGF_tabs::onShowTabMap(%this)
{
    cancel(geTGF.geTGF_Refresh_Schedule);
    geTGF_Refresh.setVisible(0);
    geTGF_Refresh.setActive(0);
    return ;
}
function geTGF_tabs::refreshTabMap(%this)
{
    %this.Maps_filterDestinations(%this.Maps_filterType, %this.Maps_filterCity);
    WorldMap.refresh();
    geTGF.mGeTabs.Maps_GetApartmentVURL();
    return ;
}
function geTGF_tabs::Maps_buildDestControl(%this, %tab)
{
    %padding = 4;
    %ctrlPosition = "682 4";
    %childRatio = 167 / 137;
    %tabExtent = %tab.getExtent();
    %ctrlExtent = (getWord(%tabExtent, 0) - getWord(%ctrlPosition, 0)) - 1 SPC (getWord(%tabExtent, 1) - getWord(%ctrlPosition, 1)) - 1;
    %MainCtrl = new GuiBitmapCtrl(TGFDestinations)
    {
        profile = "GuiDefaultProfile";
        horizSizing = "width";
        vertSizing = "bottom";
        position = %ctrlPosition;
        extent = %ctrlExtent;
        sluggishness = 0.8;
    };
    %posX = %padding + 4;
    %posY = 1;
    %textLabel = new GuiMLTextCtrl()
    {
        profile = "ETSNonModalProfile";
        position = %posX SPC %posY;
        extent = "65 20";
        text = mlStyle("<just:right>Show: ", "tgfWebLink_Light");
    };
    %MainCtrl.add(%textLabel);
    %posX = %posX + (getWord(%textLabel.getExtent(), 0) + 2);
    %posY = %posY + 1;
    %dropdown = new GuiPopUp2MenuCtrl(TGFDestinationTypeList)
    {
        profile = "InfoWindowPopupProfile";
        scrollProfile = "DottedScrollProfile";
        winProfile = "InfoWindowPopupWindowProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = %posX SPC %posY;
        extent = (getWord(%ctrlExtent, 0) - %posX) - 1 SPC 20;
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
    %posY = %posY + (20 + 1);
    %scroll = new GuiScrollCtrl(TGFDestinationsScrollList)
    {
        profile = "DottedScrollDarkProfile";
        horizSizing = "right";
        vertSizing = "height";
        position = %posX SPC %posY;
        extent = (getWord(%ctrlExtent, 0) - %posX) - 1 SPC (getWord(%ctrlExtent, 1) - %posY) - 1;
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
    %nowhere = new GuiMLTextCtrl(TGFDestinationsNowhere)
    {
        profile = "ETSNonModalProfile";
        position = 11 SPC 32;
        extent = "227 20";
        text = mlStyle("Sorry, no destinations here.", "tgfWebLink_Light");
        visible = 0;
    };
    %MainCtrl.add(%scroll);
    %MainCtrl.add(%nowhere);
    %arrayWidth = getWord(%scroll.extent, 0) - 12;
    %childWidth = mFloor((%arrayWidth - (%padding * 3)) / 2);
    %childHeight = mFloor(%childWidth * %childRatio);
    TGFDestinationsArray.childrenExtent = %childWidth SPC %childHeight;
    %MainCtrl.childHeightDelta = %childHeight + %padding;
    return %MainCtrl;
}
function geTGF_tabs::Maps_buildSmallWorldControl(%this, %tab)
{
    %origSize = "269 187";
    %ratio = 269 / 187;
    %ctrlExtent = "275 156";
    %ctrlPosition = "682 4";
    %expbtnExt = "30 19";
    %MainCtrl = new GuiBitmapCtrl(TGFWorldMapMultiCitySmall)
    {
        profile = "GuiDefaultProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = %ctrlPosition;
        extent = %ctrlExtent;
        minExtent = "1 1";
        visible = 1;
        bitmap = "platform/client/ui/small_multi_city_bkgd";
    };
    %expandBtn = new GuiBitmapButtonCtrl()
    {
        profile = "GuiDefaultProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = (getWord(%ctrlExtent, 0) - getWord(%expbtnExt, 0)) - 2 SPC (getWord(%ctrlExtent, 1) - getWord(%expbtnExt, 1)) - 2;
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
    %rY = getWord(%ctrlExtent, 1) / getWord(%origSize, 1);
    %rX = %rY;
    %size = WorldMapCityInfoMap.size();
    %i = 0;
    while (%i < %size)
    {
        %cityName = WorldMapCityInfoMap.getValue(%i).name;
        %smallCityButton = WorldMap.getCityButton(%cityName, 1, 1);
        %oldPosition = %smallCityButton.position;
        %smallCityButton.position = mFloor(getWord(%oldPosition, 0) * %rX) SPC mFloor(getWord(%oldPosition, 1) * %rY);
        %MainCtrl.add(%smallCityButton);
        %MainCtrl.citybutton[%cityName] = %smallCityButton;
        %i = %i + 1;
    }
    return %MainCtrl;
}
function TGFDestinationsArray::onCreatedChild(%this, %child)
{
    %extent = %child.getExtent();
    %button = new GuiBitmapButtonCtrl()
    {
        profile = "GuiDefaultProfile";
        position = "0 0";
        extent = %extent;
        bitmap = "platform/client/buttons/tgf/tgf_buttonframe_125x152";
        command = "";
    };
    %child.button = %button;
    %height = 20;
    %boxExtent = getWord(%child.getExtent(), 0) SPC %height;
    %zoomExtent = "12 18";
    %box = new GuiControl()
    {
        profile = "EtsDarkBorderlessBoxProfile";
        extent = %boxExtent;
        position = 0 SPC getWord(%child.getExtent(), 1) - %height;
    };
    %cityName = new GuiMLTextCtrl()
    {
        profile = "ETSNonModalProfile";
        position = "4 1";
        extent = (getWord(%box.extent, 0) - getWord(%zoomExtent, 0)) + 1 SPC 18;
        text = mlStyle("city", "tgfItem_DestinationCity");
    };
    %box.add(%cityName);
    %contiguous = new GuiBitmapCtrl()
    {
        extent = %zoomExtent;
        position = (getWord(%boxExtent, 0) - getWord(%zoomExtent, 0)) - 1 SPC 1;
        profile = "EtsNonModalProfile";
        bitmap = "platform/client/ui/tgf/tgf_map_fasttravel";
        visible = 0;
    };
    %box.add(%contiguous);
    %child.contiguous = %contiguous;
    %child.cityName = %cityName;
    %child.add(%box);
    %child.add(%button);
    return ;
}
function geTGF_tabs::Maps_changedTypeFilter(%this, %dropdown)
{
    %text = %dropdown.getText();
    %idx = %dropdown.findText(%text);
    if (%idx < 0)
    {
        return ;
    }
    %typefilter = "";
    if (%idx > 0)
    {
        %typefilter = getWord($geTGF::DestinationFilterCodes, %idx - 1);
    }
    %this.Maps_filterDestinations(%typefilter, %this.Maps_filterCity);
    return ;
}
function geTGF_tabs::Maps_changedCityFilter(%this, %cityName)
{
    if (%cityName $= "multi_city")
    {
        %cityName = "";
    }
    if (!(%cityName $= %this.Maps_filterCity))
    {
        if (!((%cityName $= "")) && (TGFWorldMapMultiCitySmall.isVisible() == 0))
        {
            %pos = TGFDestinations.getPosition();
            %ext = TGFDestinations.getExtent();
            TGFDestinations.resize(getWord(%ext, 0), getWord(%ext, 1) - TGFDestinations.childHeightDelta);
            TGFDestinations.setTrgPosition(getWord(%pos, 0) SPC getWord(%pos, 1) + TGFDestinations.childHeightDelta);
            TGFWorldMapMultiCitySmall.setVisible(1);
        }
        else
        {
            if ((%cityName $= "") && (TGFWorldMapMultiCitySmall.isVisible() == 1))
            {
                %pos = TGFDestinations.getPosition();
                %ext = TGFDestinations.getExtent();
                TGFDestinations.resize(getWord(%ext, 0), getWord(%ext, 1) + TGFDestinations.childHeightDelta);
                TGFDestinations.setTrgPosition(getWord(%pos, 0) SPC getWord(%pos, 1) - TGFDestinations.childHeightDelta);
                TGFWorldMapMultiCitySmall.setVisible(0);
            }
        }
    }
    %this.Maps_filterDestinations(%this.Maps_filterType, %cityName);
    return ;
}
function geTGF_tabs::Maps_updateFiltering(%this)
{
    %this.Maps_filterDestinations(%this.Maps_filterType, %this.Maps_filterCity);
    return ;
}
function geTGF_tabs::Maps_filterDestinationsByType(%this, %filterType)
{
    %this.Maps_filterDestinations(%filterType, %this.Maps_filterCity);
    return ;
}
function geTGF_tabs::Maps_filterDestinationsByCity(%this, %filterCity)
{
    %this.Maps_filterDestinations(%this.Maps_filterType, %filterCity);
    return ;
}
function geTGF_tabs::Maps_filterDestinations(%this, %type, %city)
{
    %dests = "";
    %lastCityCheck = "";
    %cityAvailable = 1;
    %count = getWordCount($gDestinationNamesInternal);
    %idx = 0;
    while (%idx < %count)
    {
        %use = 1;
        %destCode = getWord($gDestinationNamesInternal, %idx);
        if (!(%type $= ""))
        {
            if (findWord($gDestinationFilters[%destCode], %type) < 0)
            {
                %use = 0;
            }
        }
        if (!(%lastCityCheck $= $gDestinationSpaces[%destCode]))
        {
            %lastCityCheck = $gDestinationSpaces[%destCode];
            %cityAvailable = WorldMap.isServerForCity(%lastCityCheck);
        }
        if (!(%city $= ""))
        {
            if (!(%city $= $gDestinationSpaces[%destCode]))
            {
                %use = 0;
            }
        }
        if (strstr($gDestinationFilters[%destCode], $geTGF::DestinationFilterExclude) >= 0)
        {
            %use = 0;
        }
        if (%use)
        {
            %dests = %dests SPC %destCode;
        }
        %idx = %idx + 1;
    }
    %dests = trim(%dests);
    %neardests = "";
    %fardests = "";
    %count = getWordCount(%dests);
    %this.Maps_filterType = %type;
    %this.Maps_filterCity = %city;
    %idx = 0;
    if (!(%type $= ""))
    {
        %idx = findWord($geTGF::DestinationFilterCodes, %type);
        %idx = %idx + 1;
        if (%idx < 0)
        {
            %idx = 0;
        }
    }
    TGFDestinationTypeList.SetSelected(%idx);
    if (%count == 0)
    {
        TGFDestinationsNowhere.visible = 1;
        TGFDestinationsScrollList.visible = 0;
        return ;
    }
    TGFDestinationsNowhere.visible = 0;
    TGFDestinationsScrollList.visible = 1;
    %idx = 0;
    while (%idx < %count)
    {
        %destCode = getWord(%dests, %idx);
        if (DestinationList::IsDestinationInMyContiguousSpace(%destCode))
        {
            %neardests = %neardests SPC %destCode;
        }
        else
        {
            %fardests = %fardests SPC %destCode;
        }
        %idx = %idx + 1;
    }
    %neardests = trim(%neardests);
    %fardests = trim(%fardests);
    %dests = trim(%neardests SPC %fardests);
    %count = getWordCount(%dests);
    TGFDestinationsArray.setNumChildren(%count);
    geTGF.clearItemList("map", "venue");
    %bitmapDelay = 3000;
    %bitmapInc = 75;
    %idx = 0;
    while (%idx < %count)
    {
        %destCode = getWord(%dests, %idx);
        %child = TGFDestinationsArray.getObject(%idx);
        %thumbnail = DestinationList::getBitmapLocation(%destCode);
        if (findWord($geTGF::DestinationThumbnails, %thumbnail) < 0)
        {
            %child.schedule(%bitmapDelay + (%bitmapInc * %idx), "setBitmap", %thumbnail);
            if ($geTGF::DestinationThumbnails $= "")
            {
                $geTGF::DestinationThumbnails = %thumbnail;
            }
            else
            {
                $geTGF::DestinationThumbnails = $geTGF::DestinationThumbnails SPC %thumbnail;
            }
        }
        else
        {
            %child.setBitmap(%thumbnail);
        }
        %child.button.command = "geTGF_tabs::Maps_clickLocation(\"" @ %destCode @ "\");";
        %child.contiguous.visible = DestinationList::IsDestinationInMyContiguousSpace(%destCode);
        %citname = strupr(DestinationList::getDestinationContiguousSpace(%destCode));
        %child.cityName.setText(mlStyle(%citname, "tgfItem_DestinationCity"));
        %item = geTGF.createNewItem("map", "venue", %destCode);
        %item.codeName = %destCode;
        %idx = %idx + 1;
    }
}

function geTGF::map_GetAndOpenDetailsContainer(%this, %item)
{
    %this.constructDeetsWindow(geDeetsWindow, %item);
    geDeetsLayer.setVisible(1);
    return geDeetsWindow;
}
function geTGF_tabs::Maps_clickLocation(%destCode)
{
    %item = geTGF.findItem("map", "venue", %destCode);
    geTGF.DoDetails("map", %item);
    return ;
}
