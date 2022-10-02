$gMyShopAboutLinks = "";
function ClosetTabs::fillMyShopTab(%this)
{
    %theTab = %this.getTabWithName("MY DESIGNS");
    if (!isObject(%theTab))
    {
        return ;
    }
    if (0)
    {
    }
    %xPos = 34;
    %xGap = 6;
    %ypos = 84;
    %xSiz = 66;
    %viewType = "Accepted";
    %xPos = %xPos + (%xSiz + %xGap);
    if (0)
    {
        %xSiz = 64;
        %viewType = "Pending";
        %xPos = %xPos + (%xSiz + %xGap);
        %xSiz = 76;
        %viewType = "Rejected";
        %xPos = %xPos + (%xSiz + %xGap);
    }
    %xSiz = 74;
    %viewType = "Templates";
    if (0)
    {
        error("TODO - need to check for FashionPolice permission");
        %viewType = "Incoming";
        %xPos = %xPos + (%xSiz + %xGap);
    }
    %itemsFrame = new GuiControl(MyShopItemsFrame)
    {
        profile = "GuiDefaultProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "22 120";
        extent = "467 368";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
    };
    %itemsInfoText = new GuiMLTextCtrl()
    {
        profile = "ClosetLeftInfoProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "5 0";
        extent = "457 16";
        minExtent = "1 1";
        sluggishness = -1;
        visible = fase;
        text = "no matching items";
        maxLength = 255;
    };
    %itemsFrame.add(%itemsInfoText);
    %ctrl = new GuiTextCtrl()
    {
        profile = "ClosetRightInfoProfile";
        horizSizing = "left";
        vertSizing = "bottom";
        position = "313 1";
        extent = "125 14";
        minExtent = "125 1";
        sluggishness = -1;
        visible = 1;
        text = "";
        maxLength = 255;
    };
    %itemsFrame.add(%ctrl);
    %theTab.rangeText = %ctrl;
    %ctrl = new GuiMLTextCtrl()
    {
        profile = "ClosetRightInfoProfile";
        horizSizing = "left";
        vertSizing = "bottom";
        position = "338 354";
        extent = "125 14";
        modal = 0;
    };
    %itemsFrame.add(%ctrl);
    %theTab.otherGenderText = %ctrl;
    %ctrl = new GuiScrollCtrl()
    {
        profile = "ETSScrollProfile";
        position = "0 20";
        extent = "465 326";
        minExtent = "1 1";
        horizSizing = "right";
        vertSizing = "bottom";
        visible = 1;
        hScrollBar = "dynamic";
        vScrollBar = "dynamic";
        constantThumbHeight = 1;
        scrollMultiplier = 16.1;
    };
    %ctrl.bindClassName("ClosetItemsScroll");
    %theTab.itemsScroll = %ctrl;
    %itemsFrame.add(%ctrl);
    %thumbnails = new GuiArray2Ctrl(ClosetThumbnailsMyShop)
    {
        class = "ClosetThumbnails";
        profile = "FocusableDefaultProfile";
        childrenClassName = "GuiMouseEventCtrl";
        childrenExtent = "109 159";
        spacing = 2;
        numRowsOrCols = 4;
        inRows = 0;
        canHilite = 0;
        infoText = %itemsInfoText;
        tab = %theTab;
        scroll = %theTab.itemsScroll;
        otherGenderText = %theTab.otherGenderText;
    };
    %theTab.itemsScroll.add(%thumbnails);
    %theTab.itemsScroll.thumbnails = %thumbnails;
    %theTab.add(%itemsFrame);
    %theTab.thumbnails = %thumbnails;
    %itemsFrame.thumbnails = %thumbnails;
    %itemDescFrame = new GuiControl()
    {
        profile = ETSNonModalProfile;
        horizSizing = "right";
        vertSizing = "bottom";
        position = "689 309";
        extent = "245 179";
    };
    %theTab.add(%itemDescFrame);
    MyShopCopyToOutfitText.setTextWithStyle("<just:right><a:gamelink:COPY_TO_OUTFIT>copy to outfit</a>");
    %ctrl = new GuiMLTextCtrl()
    {
        horizSizing = "right";
        vertSizing = "bottom";
        position = "232 5";
        extent = "10 18";
        style = "plainOnWhiteBlueLinks";
        visible = 0;
        stripGamelink = 1;
    };
    %ctrl.bindClassName("MyShopGenericMLText");
    %ctrl.setTextWithStyle("<a:gamelink:MYDESIGNS_ABOUT DESCSHORT>-?</a>");
    $gMyShopAboutLinks = $gMyShopAboutLinks SPC %ctrl;
    MyShopItemDeetsPanel.add(%ctrl);
    %ctrl = new GuiMLTextCtrl()
    {
        horizSizing = "right";
        vertSizing = "bottom";
        position = "232 25";
        extent = "10 18";
        style = "plainOnWhiteBlueLinks";
        visible = 0;
        stripGamelink = 1;
    };
    %ctrl.bindClassName("MyShopGenericMLText");
    %ctrl.setTextWithStyle("<a:gamelink:MYDESIGNS_ABOUT DESCLONG>-?</a>");
    $gMyShopAboutLinks = $gMyShopAboutLinks SPC %ctrl;
    MyShopItemDeetsPanel.add(%ctrl);
    %letterWords = "A B C D";
    %posX = 0;
    %posY = 0;
    %extX = 84;
    %extY = 84;
    %dx = %extX + 13;
    %dy = %extY + 4;
    %n = 0;
    new GuiScrollCtrl(MyShopItemDeets_TexturesScroll)
    {
        profile = "DottedScrollProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "8 43";
        extent = 210 SPC %extY + 18;
        hScrollBar = "dynamic";
        vScrollBar = "alwaysOff";
    }.add(new GuiScrollCtrl(MyShopItemDeets_TexturesScroll)
    {
        profile = "DottedScrollProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "8 43";
        extent = 210 SPC %extY + 18;
        hScrollBar = "dynamic";
        vScrollBar = "alwaysOff";
    });
    %ctrl = new GuiBitmapCtrl()
    {
        profile = "ClosetDkBackgroundProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = %posX SPC %posY;
        extent = %extX SPC %extY;
        systemDragDrop = 1;
        canHilite = 1;
        visible = 0;
    };
    %ctrl.bindClassName("geTextureDropTarget");
    MyShopItemDeets_TexturesContainer.add(%ctrl);
    MyShopItemDeetsPanel.geTextureTarget[%n] = %ctrl;
    %posX = %posX + %dx;
    %n = %n + 1;
    %ctrl = new GuiBitmapCtrl()
    {
        profile = "ClosetDkBackgroundProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = %posX SPC %posY;
        extent = %extX SPC %extY;
        systemDragDrop = 1;
        canHilite = 1;
        visible = 0;
    };
    %ctrl.bindClassName("geTextureDropTarget");
    MyShopItemDeets_TexturesContainer.add(%ctrl);
    MyShopItemDeetsPanel.geTextureTarget[%n] = %ctrl;
    %posX = %posX + %dx;
    %n = %n + 1;
    %ctrl = new GuiBitmapCtrl()
    {
        profile = "ClosetDkBackgroundProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = %posX SPC %posY;
        extent = %extX SPC %extY;
        systemDragDrop = 1;
        canHilite = 1;
        visible = 0;
    };
    %ctrl.bindClassName("geTextureDropTarget");
    MyShopItemDeets_TexturesContainer.add(%ctrl);
    MyShopItemDeetsPanel.geTextureTarget[%n] = %ctrl;
    %posX = %posX + %dx;
    %n = %n + 1;
    %ctrl = new GuiBitmapCtrl()
    {
        profile = "ClosetDkBackgroundProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = %posX SPC %posY;
        extent = %extX SPC %extY;
        systemDragDrop = 1;
        canHilite = 1;
        visible = 0;
    };
    %ctrl.bindClassName("geTextureDropTarget");
    MyShopItemDeets_TexturesContainer.add(%ctrl);
    MyShopItemDeetsPanel.geTextureTarget[%n] = %ctrl;
    %posX = %posX + %dx;
    %n = %n + 1;
    %ctrl = new GuiMLTextCtrl()
    {
        horizSizing = "right";
        vertSizing = "bottom";
        position = "87 71";
        extent = "10 16";
        stripGamelink = 1;
        style = "plainOnWhiteBlueLinks";
        visible = 0;
    };
    %ctrl.bindClassName("MyShopGenericMLText");
    %ctrl.setTextWithStyle("<a:gamelink:MYDESIGNS_ABOUT TEXTURES>-?</a>");
    $gMyShopAboutLinks = $gMyShopAboutLinks SPC %ctrl;
    MyShopItemDeets_TexturesScroll.add(%ctrl);
    %ctrl = new GuiVariableWidthButtonCtrl(MyShopRefreshTexturesCtrl)
    {
        profile = "BracketButton19Profile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "90 200";
        extent = "119 19";
        command = "ClosetGUI_RefreshTextures();";
        text = "Reload Textures";
        buttonType = "PushButton";
        visible = 0;
        tooltip = "(Ctrl-R)";
    };
    MyShopItemDeetsPanel.add(%ctrl);
    %ctrl = new GuiControl(MyShopItemSettings)
    {
        profile = ETSNonModalProfile;
        position = "8 142";
        extent = "232 80";
        horizSizing = "right";
        vertSizing = "bottom";
        visible = 0;
    };
    MyShopItemDeetsPanel.add(%ctrl);
    %ctrl = new GuiMLTextCtrl()
    {
        profile = "ETSNonModalProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "0 2";
        extent = "45 18";
        style = "plainOnWhite";
        stripGamelink = 1;
    };
    %ctrl.setTextWithStyle("vBux:");
    MyShopItemSettings.add(%ctrl);
    %ctrl = new GuiTextEditCtrl(MyShopVBuxField)
    {
        profile = "Profile_MyShop_SettingsField";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "42 0";
        extent = "50 18";
        text = "";
        command = "$ThisControl.onKeyStroke();";
        validInputChars = 0123456789;
        tooltip = "How much this will cost";
    };
    MyShopItemSettings.add(%ctrl);
    %ctrl = new GuiMLTextCtrl()
    {
        horizSizing = "right";
        vertSizing = "bottom";
        position = "94 2";
        extent = "45 18";
        style = "plainOnWhiteBlueLinks";
        visible = 0;
        stripGamelink = 1;
    };
    %ctrl.bindClassName("MyShopGenericMLText");
    %ctrl.setTextWithStyle("<a:gamelink:MYDESIGNS_ABOUT VBUX>-?</a>");
    $gMyShopAboutLinks = $gMyShopAboutLinks SPC %ctrl;
    MyShopItemSettings.add(%ctrl);
    %ctrl = new GuiMLTextCtrl()
    {
        profile = "ETSNonModalProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "0 22";
        extent = "45 18";
        style = "plainOnWhite";
    };
    %ctrl.setTextWithStyle("vPoints:");
    MyShopItemSettings.add(%ctrl);
    %ctrl = new GuiTextEditCtrl(MyShopVPointsField)
    {
        profile = "Profile_MyShop_SettingsField";
        modal = 0;
        horizSizing = "right";
        vertSizing = "bottom";
        position = "42 20";
        extent = "50 18";
        text = "";
        readOnly = 1;
        command = "$ThisControl.onKeyStroke();";
        validInputChars = 0123456789;
        tooltip = "How much this will cost";
    };
    MyShopItemSettings.add(%ctrl);
    %ctrl.setActive(0);
    %ctrl = new GuiMLTextCtrl()
    {
        horizSizing = "right";
        vertSizing = "bottom";
        position = "94 22";
        extent = "45 18";
        style = "plainOnWhiteBlueLinks";
        visible = 0;
        stripGamelink = 1;
    };
    %ctrl.bindClassName("MyShopGenericMLText");
    %ctrl.setTextWithStyle("<a:gamelink:MYDESIGNS_ABOUT VPOINTS>-?</a>");
    $gMyShopAboutLinks = $gMyShopAboutLinks SPC %ctrl;
    MyShopItemSettings.add(%ctrl);
    %ctrl = new GuiCheckBoxCtrl(MyShopPriceForSaleOption)
    {
        profile = "ETSCheckBoxProfile2";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "150 -3";
        extent = "50 18";
        text = "for sale";
        buttonType = "ToggleButton";
        tooltip = "Make this item for sale in your store";
    };
    %ctrl.setValue(1);
    MyShopItemSettings.add(%ctrl);
    %ctrl = new GuiMLTextCtrl()
    {
        horizSizing = "right";
        vertSizing = "bottom";
        position = "209 0";
        extent = "10 18";
        style = "plainOnWhiteBlueLinks";
        visible = 0;
        stripGamelink = 1;
    };
    %ctrl.bindClassName("MyShopGenericMLText");
    %ctrl.setTextWithStyle("<a:gamelink:MYDESIGNS_ABOUT FOR_SALE>-?</a>");
    $gMyShopAboutLinks = $gMyShopAboutLinks SPC %ctrl;
    MyShopItemSettings.add(%ctrl);
    %ctrl = new GuiCheckBoxCtrl(MyShopPriceFeaturedOption)
    {
        profile = "ETSCheckBoxProfile2";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "150 12";
        extent = "85 18";
        text = "featured";
        buttonType = "ToggleButton";
        tooltip = "Make this your one item in the VHD store";
    };
    MyShopItemSettings.add(%ctrl);
    %ctrl = new GuiMLTextCtrl()
    {
        horizSizing = "right";
        vertSizing = "bottom";
        position = "209 15";
        extent = "10 18";
        style = "plainOnWhiteBlueLinks";
        visible = 0;
        stripGamelink = 1;
    };
    %ctrl.bindClassName("MyShopGenericMLText");
    %ctrl.setTextWithStyle("<a:gamelink:MYDESIGNS_ABOUT FEATURED>-?</a>");
    $gMyShopAboutLinks = $gMyShopAboutLinks SPC %ctrl;
    MyShopItemSettings.add(%ctrl);
    %ctrl = new GuiMLTextCtrl(MyShopPriceOutOfRangeText)
    {
        profile = "ETSNonModalProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "10 44";
        extent = "214 14";
        style = "plainRedSmall";
        stripGamelink = 1;
    };
    %ctrl.setTextWithStyle("");
    MyShopItemSettings.add(%ctrl);
    %ctrl = new GuiVariableWidthButtonCtrl(MyShopSubmitButton)
    {
        profile = "BracketButton17NonDefaultProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "10 60";
        extent = "214 17";
        command = "$ThisControl.onClick();";
        canHilite = 1;
        text = "Submit this template as a new item";
        drawText = 1;
        modulationColor = "255 255 255 64";
        command = "$ThisControl.onClick();";
    };
    MyShopItemSettings.add(%ctrl);
    %ctrl.setActive(0);
    %ctrl = new GuiVariableWidthButtonCtrl(MyShopStartNewButton)
    {
        profile = "BracketButton19Profile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "8 200";
        extent = "76 19";
        command = "$ThisControl.onClick();";
        text = "Start New";
        buttonType = "PushButton";
        visible = 0;
        tooltip = "Start designing with a new copy of this template";
    };
    MyShopItemDeetsPanel.add(%ctrl);
    new GuiBitmapCtrl(MyShopDragFilesImage)
    {
        profile = "ETSNonModalProfile";
        horizSizing = "left";
        vertSizing = "bottom";
        position = "907 128";
        extent = "43 174";
        bitmap = "platform/client/ui/closet_myshop_dragfiles";
        modulationColor = "255 255 255 80";
        visible = 0;
    };
    %w = 461;
    %h = 423;
    new GuiWindowCtrl(MyShopTextureInspector)
    {
        profile = "DottedWindowProfile";
        position = "25 65";
        extent = %w SPC %h;
        visible = 0;
        canHilite = 0;
        resizeWidth = 0;
        resizeHeight = 0;
        canMove = 0;
        canClose = 0;
        canMinimize = 0;
        canMaximize = 0;
        systemDragDrop = 1;
        canHilite = 1;
    }.add(new GuiWindowCtrl(MyShopTextureInspector)
    {
        profile = "DottedWindowProfile";
        position = "25 65";
        extent = %w SPC %h;
        visible = 0;
        canHilite = 0;
        resizeWidth = 0;
        resizeHeight = 0;
        canMove = 0;
        canClose = 0;
        canMinimize = 0;
        canMaximize = 0;
        systemDragDrop = 1;
        canHilite = 1;
    });
    %ctrl = new GuiVariableWidthButtonCtrl()
    {
        profile = "BracketButton19Profile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "829 519";
        extent = "43 19";
        minExtent = "1 1";
        visible = 1;
        command = "ClosetGui.close(false);";
        text = "Done";
        buttonType = "PushButton";
        drawText = 1;
    };
    %theTab.add(%ctrl);
    %theTab.doneButton = %ctrl;
    %ctrl = new GuiVariableWidthButtonCtrl()
    {
        profile = "BracketButton19NonDefaultProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "882 519";
        extent = "52 19";
        minExtent = "1 1";
        visible = 1;
        command = "ClosetGui.close(true);";
        text = "Cancel";
        buttonType = "PushButton";
        drawText = 1;
    };
    %theTab.add(%ctrl);
    %theTab.cancelButton = %ctrl;
    ClosetTabs.setStoreControlsVisible(1);
    %theTab.firstLoad = 1;
    ClosetGui_MyShop_SetView(ACCEPTED);
    %this.tabMyShopInitialized = 1;
    return ;
}
$gPrevMyShopTypeButton = "";
function ClosetGui_MyShop_SetView(%viewType)
{
    if (isObject($gPrevMyShopTypeButton))
    {
        $gPrevMyShopTypeButton.setProfile(BracketButton17NonDefaultProfile);
        $gPrevMyShopTypeButton.modal = 1;
    }
    %buttonCtrl = "geClosetMyShopViewButton_" @ %viewType;
    %buttonCtrl.setProfile(BracketButton17Profile);
    %buttonCtrl.modal = 0;
    $gPrevMyShopTypeButton = %buttonCtrl;
    getUGCItems(%viewType);
    return ;
}
$gUGCSkus["ACCEPTED"] = "";
$gUGCSkus["PENDING"] = "";
$gUGCSkus["REJECTED"] = "";
$gUGCSkus["TEMPLATES"] = "";
$gUGCSkus["INCOMING"] = "";
$gUGCPrevSku["ACCEPTED"] = "";
$gUGCPrevSku["PENDING"] = "";
$gUGCPrevSku["REJECTED"] = "";
$gUGCPrevSku["TEMPLATES"] = "";
$gUGCPrevSku["INCOMING"] = "";
function getUGCItems(%type)
{
    $gUGCSkus[%type] = "";
    if (%type $= "TEMPLATES")
    {
        %list = SkuManager.getSkusTag("template", $player.getGender());
        %list = %list SPC SkuManager.getSkusTag("template", $player.getOtherGender());
        onGotUGCItems(%type, %list);
    }
    else
    {
        if (%type $= "ACCEPTED")
        {
            %list = SkuManager.getSkusType("mesh");
            %list = SkuManager.filterSkusAuthor(%list, $Player::Name);
            onGotUGCItems(%type, %list);
        }
        else
        {
            error(getScopeName() SPC "- not implemented" SPC %type SPC getTrace());
            if (0)
            {
                ClosetThumbnailsMyShop.infoText.setVisible(1);
                ClosetThumbnailsMyShop.infoText.setText("Fetching..");
                %list = $gSampleUGC[%type];
                %n = getWordCount(%list) - 1;
                while (%n >= 0)
                {
                    %sku = getWord(%list, %n);
                    %si = SkuManager.findBySku(%sku);
                    %si.brand = "";
                    %si.tags = findAndRemoveAllOccurrencesOfWord(%si.tags, "new");
                    %si.expireTime = "";
                    if ((%type $= "ACCEPTED") && (%type $= "PENDING"))
                    {
                        %si.author = $Player::Name;
                    }
                    %n = %n - 1;
                }
                schedule(500, 0, "onGotUGCItems", %type, %list);
            }
        }
    }
    return ;
}
function onGotUGCItems(%type, %list)
{
    $gUGCSkus[%type] = %list ;
    if (%type $= "REJECTED")
    {
        ClosetThumbnailsMyShop.setUnfilteredSkus("");
        ClosetThumbnailsMyShop.infoText.setText("rejected list not implemented yet");
    }
    else
    {
        ClosetThumbnailsMyShop.setUnfilteredSkus(%list);
    }
    if (%list $= "")
    {
        %theTab = ClosetTabs.getTabWithName("MY DESIGNS");
        if (%theTab.firstLoad)
        {
            %theTab.firstLoad = 0;
            ClosetGui_MyShop_SetView("TEMPLATES");
        }
        else
        {
            ClosetThumbnailsMyShop.infoText.setText($MsgCat::MyShop["EMPTYLIST",%type]);
        }
    }
    ClosetGui_MyShop_SetCurrentSku($gUGCPrevSku[%type]);
    return ;
}
function ClosetGui_MyShop_GetSkuUGCStatus(%sku)
{
    %si = SkuManager.findBySku(%sku);
    if (!(%si.ugcStatus $= ""))
    {
        return %si.ugcStatus;
    }
    if (%si.hasTag("TEMPLATE"))
    {
        return "TEMPLATES";
    }
    %statusi = "ACCEPTED PENDING REJECTED TEMPLATES INCOMING";
    %status = "";
    %n = getWordCount(%statusi) - 1;
    while (%n >= 0)
    {
        %s = getWord(%statusi, %n);
        if (hasWord($gUGCSkus[%s], %sku))
        {
            %status = %s;
        }
        %n = %n - 1;
    }
    %si.ugcStatus = %status;
    if (%status $= "")
    {
        error(getScopeName() SPC "- no UGC status for sku" SPC %sku SPC getTrace());
    }
    return %status;
}
function ClosetGui_MyShop_GetSkuUGCStatusIcon(%sku)
{
    %status = ClosetGui_MyShop_GetSkuUGCStatus(%sku);
    if (%status $= "")
    {
        %ret = "";
    }
    else
    {
        %ret = "platform/client/ui/ugcStatus_" @ %status;
    }
    return %ret;
}
function MyShopItemsFrame::update(%this)
{
    if (!(ClosetTabs.getCurrentTab().name $= "MY DESIGNS"))
    {
        return ;
    }
    %this.thumbnails.refilter();
    ClosetGui_MyShop_SetCurrentSku($gMyShopCurrentSku);
    return ;
}
$gSkusMyShopLayer = "";
$gMyShopCurrentSku = "";
function ClosetGUI_ToggleSku_MyShop(%sku)
{
    if (hasWord($gSkusMyShopLayer, %sku))
    {
        $gSkusMyShopLayer = findAndRemoveAllOccurrencesOfWord($gSkusMyShopLayer, %sku);
        ClosetGui_MyShop_SetCurrentSku("");
    }
    else
    {
        $gSkusMyShopLayer = SkuManager.overlaySkus($gSkusMyShopLayer, %sku);
        ClosetGui_MyShop_SetCurrentSku(%sku);
    }
    return ;
}
function ClosetGui_MyShop_SetCurrentSku(%sku)
{
    if (%sku $= "")
    {
        %itemStatus = "";
    }
    else
    {
        %itemStatus = ClosetGui_MyShop_GetSkuUGCStatus(%sku);
        $gUGCPrevSku[%itemStatus] = %sku ;
    }
    if (%itemStatus $= "ACCEPTED")
    {
        %readOnlyDesc = 1;
        %readOnlySettings = 0;
        %showSettings = 1;
        %showSubmit = 1;
        %showAboutLinks = 1;
        %showStartNew = 1;
        MyShopSubmitButton.setText("Update Item");
    }
    else
    {
        if (%itemStatus $= "PENDING")
        {
            error(getScopeName() SPC "- not implemented yet:" SPC %itemStatus SPC getTrace());
            %readOnlyDesc = 1;
            %readOnlySettings = 1;
            %showSettings = 1;
            %showSubmit = 0;
            %showAboutLinks = 1;
            %showStartNew = 1;
        }
        else
        {
            if (%itemStatus $= "REJECTED")
            {
                error(getScopeName() SPC "- not implemented yet:" SPC %itemStatus SPC getTrace());
                %readOnlyDesc = 1;
                %readOnlySettings = 1;
                %showSettings = 1;
                %showSubmit = 0;
                %showAboutLinks = 0;
                %showStartNew = 0;
            }
            else
            {
                if (%itemStatus $= "TEMPLATES")
                {
                    %readOnlyDesc = 0;
                    %readOnlySettings = 0;
                    %showSettings = 1;
                    %showSubmit = 1;
                    %showAboutLinks = 1;
                    %showStartNew = 1;
                    MyShopSubmitButton.setText("Submit this template as a new item");
                }
                else
                {
                    if (%itemStatus $= "INCOMING")
                    {
                        error(getScopeName() SPC "- not implemented yet:" SPC %itemStatus SPC getTrace());
                        %readOnlyDesc = 1;
                        %readOnlySettings = 1;
                        %showSettings = 1;
                        %showSubmit = 1;
                        %showAboutLinks = 0;
                        %showStartNew = 0;
                        MyShopSubmitButton.setText("Approve or Reject this item");
                    }
                    else
                    {
                        %readOnlyDesc = 1;
                        %readOnlySettings = 1;
                        %showSettings = 0;
                        %showSubmit = 0;
                        %showAboutLinks = 0;
                        %showStartNew = 0;
                    }
                }
            }
        }
    }
    %readOnlyDesc = 1;
    %readOnlySettings = 1;
    %showSettings = 0;
    %showSubmit = 0;
    %showAboutLinks = 0;
    Profile_MyShop_TextField.border = %readOnlyDesc ? 0 : 1;
    Profile_MyShop_TextField_Bold.border = %readOnlyDesc ? 0 : 1;
    Profile_MyShop_SettingsField.border = %readOnlySettings ? 0 : 1;
    MyShopItemDeets_DescShort.readOnly = %readOnlyDesc;
    MyShopItemDeets_DescShort.setActive(!%readOnlyDesc);
    MyShopItemDeets_DescLong.readOnly = %readOnlyDesc;
    MyShopItemDeets_DescLong.setActive(!%readOnlyDesc);
    MyShopVBuxField.readOnly = %readOnlySettings;
    MyShopVBuxField.setActive(!%readOnlySettings);
    MyShopSubmitButton.setActive(!%readOnlySettings);
    MyShopSubmitButton.setVisible(%showSubmit);
    MyShopPriceForSaleOption.setActive(!%readOnlySettings);
    MyShopPriceFeaturedOption.setActive(!%readOnlySettings);
    MyShopItemSettings.setVisible(%showSettings);
    MyShopStartNewButton.setVisible(%showStartNew);
    $gMyShopAboutLinks = trim($gMyShopAboutLinks);
    %n = getWordCount($gMyShopAboutLinks) - 1;
    while (%n >= 0)
    {
        %ctrl = getWord($gMyShopAboutLinks, %n);
        %ctrl.setVisible(%showAboutLinks);
        %n = %n - 1;
    }
    MyShopItemDeetsPanel.setSkuBaseTextures(%sku);
    MyShopItemDeets_DescShort.setText("");
    MyShopItemDeets_DescShort.setText("");
    if (%sku $= "")
    {
        MyShopItemDeets_DescShort.setText("no current item");
        MyShopItemDeets_DescLong.setText("");
        $gMyShopCurrentSku = %sku;
        ClosetMainObjectView.zoomToSKU("");
        return ;
    }
    %si = SkuManager.findBySku(%sku);
    if (!isObject(%si))
    {
        MyShopItemDeets_DescLong.setText("hmm, something went wrong. please check the bug forum.");
        $gMyShopCurrentSku = "";
        return ;
    }
    MyShopItemDeets_DescShort.setText(%si.descShrt);
    MyShopItemDeets_DescLong.setText(%si.descLong);
    $gMyShopCurrentSku = %sku;
    ClosetMainObjectView.zoomToSKU(%sku);
    MyShopVBuxField.setValue(%si.price);
    MyShopVBuxField.onKeystroke();
    return ;
}
function MyShopItemDeetsPanel::setSkuBaseTextures(%this, %sku)
{
    %this.geTextureTarget[0].setVisible(0);
    %this.geTextureTarget[1].setVisible(0);
    %this.geTextureTarget[2].setVisible(0);
    %this.geTextureTarget[3].setVisible(0);
    %isValid = 1;
    %si = SkuManager.findBySku(%sku);
    if (!isObject(%si))
    {
        %isValid = 0;
    }
    if (%isValid)
    {
        %baseTextures = %si.getTxtrNames();
        %num = getWordCount(%baseTextures);
        %this.numTextures = %num;
        %n = 0;
        while (%n < %num)
        {
            %baseTexture = getWord(%baseTextures, %n);
            if (ClosetGui_MyShop_GetSkuUGCStatus(%sku) $= "TEMPLATES")
            {
                %path = "user/textures/" @ %baseTexture;
            }
            else
            {
                %path = "projects/common/characters/" @ $player.getGender() @ "_player/" @ %baseTexture;
            }
            if (isFile(%path @ ".jpg") && isFile(%path @ ".png"))
            {
                %this.geTextureTarget[%n].setBitmap(%path);
            }
            else
            {
                %this.geTextureTarget[%n].setBitmap("projects/vside/worlds/common/swatches/Gray50");
            }
            %this.geTextureTarget[%n].setVisible(1);
            %this.geTextureTarget[%n].baseTexture = %baseTexture;
            %this.geTextureTarget[%n].sku = %sku;
            %n = %n + 1;
        }
    }
    else
    {
        %num = 0;
    }
    %stepX = getWord(%this.geTextureTarget[1].getPosition(), 0) - getWord(%this.geTextureTarget[0].getPosition(), 0);
    %padding = %stepX - getWord(%this.geTextureTarget[0].getExtent(), 0);
    MyShopItemDeets_TexturesContainer.resize((%stepX * %num) - %padding, getWord(%this.geTextureTarget[0].getExtent(), 1));
    %prevParent = MyShopDragFilesImage.getGroup();
    if (!%isValid)
    {
        MyShopDragFilesImage.setVisible(0);
        MyShopDragFilesImage.reparent(PlayGui, "-1000 -1000", "", "");
        MyShopRefreshTexturesCtrl.setVisible(0);
        MyShopItemDeets_TexturesScroll.setVisible(0);
    }
    else
    {
        if (%num == 1)
        {
            MyShopDragFilesImage.setVisible(%si.hasTag("TEMPLATE"));
            MyShopDragFilesImage.reparent(ClosetMainObjectViewContainer, "151 223", "", "");
            MyShopDragFilesImage.modulationColor = "200 50 180 180";
            MyShopRefreshTexturesCtrl.setVisible(!(%si.getTxtrNames() $= %si.originalTxtrNames));
            MyShopItemDeets_TexturesScroll.setVisible(1);
        }
        else
        {
            MyShopDragFilesImage.setVisible(%si.hasTag("TEMPLATE"));
            MyShopDragFilesImage.reparent(ClosetTabs.getTabWithName("MY DESIGNS"), "907 128", "", "");
            MyShopDragFilesImage.modulationColor = "0 0 0 80";
            MyShopRefreshTexturesCtrl.setVisible(!(%si.getTxtrNames() $= %si.originalTxtrNames));
            MyShopItemDeets_TexturesScroll.setVisible(1);
        }
    }
    %currParent = MyShopDragFilesImage.getGroup();
    if ((%isValid && (%currParent != %prevParent)) && %si.hasTag("TEMPLATE"))
    {
        MyShopDragFilesImage.FlashVisibility(5, 150);
    }
    return ;
}
function ClosetGui_MyShop_ToggleCurrentSku(%sku)
{
    if ($gMyShopCurrentSku == %sku)
    {
        %sku = "";
    }
    ClosetGui_MyShop_SetCurrentSku(%sku);
    return ;
}
function MyShopItemDeets_Desc::onURL(%this, %url)
{
    ClosetGui_MyShop_onURL(%url, %this);
    return ;
}
function MyShopCopyToOutfitText::onURL(%this, %url)
{
    ClosetGui_MyShop_onURL(%url, %this);
    return ;
}
function MyShopGenericMLText::onURL(%this, %url)
{
    ClosetGui_MyShop_onURL(%url, %this);
    return ;
}
function ClosetGui_MyShop_onURL(%url, %mlTextCtrl)
{
    if (getWord(%url, 0) $= "SELECTNONE")
    {
        ClosetGui_MyShop_SetCurrentSku("");
    }
    else
    {
        if (getWord(%url, 0) $= "TOGGLESKU")
        {
            %sku = getWord(%url, 1);
            ClosetGUI_ToggleSku_MyShop(%sku);
        }
        else
        {
            if (getWord(%url, 0) $= "SELECTSKU")
            {
                %sku = getWord(%url, 1);
                ClosetGui_MyShop_SetCurrentSku(%sku);
            }
            else
            {
                if (getWord(%url, 0) $= "COPY_TO_OUTFIT")
                {
                    userTips::showNow("closet_myshop_copyToOutfit");
                }
                else
                {
                    if (getWord(%url, 0) $= "REFRESH_TEXTURES")
                    {
                        ClosetGUI_RefreshTextures();
                    }
                    else
                    {
                        if (getWord(%url, 0) $= "MYDESIGNS_ABOUT")
                        {
                            ClosetGui_About("MY DESIGNS", getWord(%url, 1));
                        }
                        else
                        {
                            error(getScopeName() SPC "- unknown command" SPC %url SPC getDebugString(%mlTextCtrl) SPC getTrace());
                        }
                    }
                }
            }
        }
    }
    return ;
}
function MyShopTextureInspector::onSystemDragDroppedEvent(%this, %text, %pt)
{
    ClosetMainObjectView::onSystemDragDroppedEvent_MyShop(%this, %text, %pt);
    return ;
}
function ClosetMainObjectView::acceptsSystemDragDropContent(%this, %text)
{
    return geTextureDropTarget::acceptsSystemDragDropContent(%this, %text);
}
function ClosetMainObjectView::onSystemDragDroppedEvent_MyShop(%this, %text, %pt)
{
    hiliteControl("");
    if (MyShopItemDeetsPanel.numTextures == 1)
    {
        MyShopItemDeetsPanel.geTextureTarget[0].applyTexture(%text);
    }
    else
    {
        MessageBoxOK("Use the texture list", $MsgCat::closetMyShop["E-USE-TEXTURE-LIST"]);
    }
    return ;
}
function geTextureDropTarget::acceptsSystemDragDropContent(%this, %text)
{
    if (!(ClosetGui_MyShop_GetSkuUGCStatus($gMyShopCurrentSku) $= "TEMPLATES"))
    {
        return 0;
    }
    if (hasSubString(%text, "http:"))
    {
        return 0;
    }
    %extension = getExtension(%text);
    if (!((%extension $= ".jpg")) && !((%extension $= ".png")))
    {
        echo(getScopeName() SPC "- invalid extension:" SPC %text);
        return 0;
    }
    return 1;
}
function geTextureDropTarget::onSystemDragDropEvent(%this, %text, %eventType, %pt)
{
    if (!Parent::onSystemDragDropEvent(%this, %text, %eventType, %pt))
    {
        return 0;
    }
    if (%eventType $= "BREAK")
    {
        %this.applyTexture(%text, %this.sku);
    }
    return 1;
}
function geTextureDropTarget::applyTexture(%this, %texturePath)
{
    %newTextureName = %this.baseTexture;
    %extension = getExtension(%texturePath);
    %newFullPathNoExt = "user/textures/" @ %newTextureName;
    %newFullPath = %newFullPathNoExt @ %extension;
    %this.incomingPath = %texturePath;
    if ($Platform $= "macos")
    {
        %newFullPath = getPrefsDir() @ "/" @ %newFullPath;
    }
    %ok = fileCopy(%texturePath, %newFullPath);
    if (%ok)
    {
        MyShopRefreshTexturesCtrl.setVisible(1);
        %otherExtension = %extension $= ".png" ? ".jpg" : ".png";
        %otherFullPath = "user/textures/" @ %newTextureName @ %otherExtension;
        if (isFile(%otherFullPath))
        {
            deleteFile(%otherFullPath);
        }
        removeFile(%newFullPath);
        addFile(%newFullPath);
        %this.setBitmap("");
        %this.setBitmap(%newFullPathNoExt);
        MyShopTextureInspectorBitmap.setBitmap("");
        MyShopTextureInspectorBitmap.setBitmap(%newFullPathNoExt);
        reloadMeshTexture(%this.textureName, %newFullPathNoExt);
        SkuManager.findBySku(%this.sku).replaceTextureName(%newTextureName);
    }
    else
    {
        error(getScopeName() SPC "- could not copy" SPC %texturePath SPC "to" SPC %newFullPath SPC getTrace());
    }
    return ;
}
function ClosetGUI_RefreshTextures()
{
    %n = 0;
    while (%n < 4)
    {
        %obj = MyShopItemDeetsPanel.geTextureTarget[%n];
        if (%obj.isVisible())
        {
            %obj.applyTexture(%obj.incomingPath);
        }
        %n = %n + 1;
    }
}

function ClosetGui_MyShop_CopySkusToOutfit()
{
    $ClosetSkusOutfit[$ClosetOutfitName] = SkuManager.overlaySkus($ClosetSkusOutfit[$ClosetOutfitName], $gSkusMyShopLayer) ;
    return ;
}
function MyShopItemDeets_DescShort::onKeystroke(%this)
{
    %si = SkuManager.findBySku($gMyShopCurrentSku);
    if (!isObject(%si))
    {
        return ;
    }
    %si.descShrt = %this.getValue();
    return ;
}
function MyShopItemDeets_DescLong::onKeystroke(%this)
{
    %si = SkuManager.findBySku($gMyShopCurrentSku);
    if (!isObject(%si))
    {
        return ;
    }
    %si.descLong = %this.getValue();
    return ;
}
function MyShopVBuxField::onKeystroke(%this)
{
    %si = SkuManager.findBySku($gMyShopCurrentSku);
    if (!isObject(%si))
    {
        return ;
    }
    %priceVBux = %this.getValue();
    %minPrice = %si.price;
    %maxPrice = 1e+06 - 1;
    if (%priceVBux < %minPrice)
    {
        MyShopPriceOutOfRangeText.setTextWithStyle("<just:center>" @ %minPrice SPC "vBux minimum for this item");
        MyShopItemDeetsPanel.submitPriceValid = 0;
    }
    else
    {
        if (%priceVBux > %maxPrice)
        {
            MyShopPriceOutOfRangeText.setTextWithStyle("<just:center>" @ %maxPrice SPC "vBux maximum");
            MyShopItemDeetsPanel.submitPriceValid = 0;
        }
        else
        {
            MyShopPriceOutOfRangeText.setTextWithStyle("");
            MyShopItemDeetsPanel.submitPriceValid = 1;
        }
    }
    MyShopItemDeetsPanel.updateSubmitValidity();
    %itemMultiplier = 1;
    %multiplier = $gVPointsRatio * %itemMultiplier;
    %priceVPoints = %priceVBux * %multiplier;
    MyShopVPointsField.setText(%priceVPoints);
    return ;
}
function MyShopItemDeetsPanel::updateSubmitValidity(%this)
{
    %valid = 1;
    %valid = %valid & %this.submitPriceValid;
    %valid = %valid & %this.submitTexturesValid;
    %valid = %valid & %this.submitDescriptionValid;
    if (%valid)
    {
        MyShopSubmitButton.setActive(1);
        MyShopSubmitButton.modulationColor = "255 255 255 255";
    }
    else
    {
        MyShopSubmitButton.setActive(0);
        MyShopSubmitButton.modulationColor = "255 255 255 64";
    }
    return ;
}
$gMyShopSubmitTitle["ACCEPTED"] = "Change item settings";
$gMyShopSubmitBody["ACCEPTED"] = "You can change the price & availability of an existing item, but not its description or textures.";
$gMyShopSubmitTitle["PENDING"] = "";
$gMyShopSubmitBody["PENDING"] = "";
$gMyShopSubmitTitle["REJECTED"] = "";
$gMyShopSubmitBody["REJECTED"] = "";
$gMyShopSubmitTitle["TEMPLATES"] = "Submit new item";
$gMyShopSubmitBody["TEMPLATES"] = "This will cost money. Once accepted, you can change the price & availability of an item, but not its description or textures.";
$gMyShopSubmitTitle["INCOMING"] = "Accept or Decline item";
$gMyShopSubmitBody["INCOMING"] = "yep";
function MyShopSubmitButton::onClick(%this)
{
    %itemType = ClosetGui_MyShop_GetSkuUGCStatus($gMyShopCurrentSku);
    MessageBoxOkCancel($gMyShopSubmitTitle[%itemType], $gMyShopSubmitBody[%itemType], "MessageBoxOK(\"not implemented\", \"\", \"\");", "");
    return ;
}
function MyShop_InspectTexture(%num)
{
    if (MyShopTextureInspector.isVisible() && (MyShopTextureInspector.showingTextureNum $= %num))
    {
        MyShopTextureInspector.close();
        MyShopTextureInspector.showingTextureNum = "";
    }
    else
    {
        %bitmap = MyShopItemDeetsPanel.geTextureTarget[%num].getBitmap();
        MyShopTextureInspectorBitmap.setBitmap(%bitmap);
        MyShopTextureInspector.open();
        MyShopTextureInspector.getGroup().pushToBack(MyShopTextureInspector);
        MyShopTextureInspector.showingTextureNum = %num;
    }
    return ;
}
function MyShopTextureInspector::open(%this)
{
    %this.setVisible(1);
    MyShopItemsFrame.thumbnails.otherGenderText.setVisible(0);
    return ;
}
function MyShopTextureInspector::close(%this)
{
    %this.setVisible(0);
    MyShopItemsFrame.thumbnails.otherGenderText.setVisible(1);
    return ;
}
function MyShopStartNewButton::onClick(%this)
{
    if (!(ClosetGui_MyShop_GetSkuUGCStatus($gMyShopCurrentSku) $= "TEMPLATES"))
    {
        %templateSku = SkuManager.findTemplateSku($gMyShopCurrentSku);
        if (%templateSku $= "")
        {
            error(getScopeName() SPC "- can\'t find template for sku" SPC $gMyShopCurrentSku SPC getTrace());
            return ;
        }
        ClosetGui_MyShop_SetView("Templates");
        ClosetGui_MyShop_SetCurrentSku(%templateSku);
        ClosetGui.toggleSku(%templateSku);
    }
    %si = SkuManager.findBySku($gMyShopCurrentSku);
    if (!isObject(%si))
    {
        error(getScopeName() SPC "- invalid sku" SPC $gMyShopCurrentSku SPC getTrace());
        return ;
    }
    %trgdir = "user/textures/templates/" @ %si.descShrt @ "/" @ getTimeStamp();
    %n = getWordCount(%si.originalTxtrNames) - 1;
    while (%n >= 0)
    {
        %originalName = getWord(%si.originalTxtrNames, %n);
        %originalPath = "projects/common/characters/" @ $player.getGender() @ "_player/" @ %originalName;
        %textureName = %originalPath;
        %ext = "";
        %try = ".png";
        if ((%ext $= "") && isFile(%originalPath @ %try))
        {
            %ext = %try;
        }
        %try = ".jpg";
        if ((%ext $= "") && isFile(%originalPath @ %try))
        {
            %ext = %try;
        }
        %try = ".png";
        if ((%ext $= "") && isFile($DC::CacheFolderName @ "/" @ %originalPath @ %try))
        {
            %ext = %try;
            %originalPath = $DC::CacheFolderName @ "/" @ %originalPath;
        }
        if (%ext $= "")
        {
            error(getScopeName() SPC "- can\'t find original file" SPC %originalPath SPC getTrace());
            return ;
        }
        %srcFile = %originalPath @ %ext;
        if ($Platform $= "macos")
        {
            %workingDir = getPrefsDir();
            %trgdir = %workingDir @ "/" @ %trgdir;
            %srcFile = %workingDir @ "/" @ %srcFile;
        }
        %trgFile = %trgdir @ "/" @ %originalName @ %ext;
        echoDebug(getScopeName() SPC "- copying" SPC %srcFile SPC "to" SPC %trgFile);
        if (!fileCopy(%srcFile, %trgFile))
        {
            error(getScopeName() SPC "- error copying" SPC %srcFile SPC "to" SPC %trgFile);
            return ;
        }
        MyShopItemDeetsPanel.geTextureTarget[%n].textureName = %textureName;
        MyShopItemDeetsPanel.geTextureTarget[%n].applyTexture(%trgFile);
        %srcFile = "platform/client/ets/ugcReadMe.txt";
        %trgFile = %trgdir @ "/readme.txt";
        fileCopy(%srcFile, %trgFile);
        %n = %n - 1;
    }
    if (($Platform $= "windows") && ($Platform::Version::Major >= 6))
    {
        %vsDir = getVirtualStoreDir() @ "/" @ %trgdir;
        if (platformIsFile(%vsDir @ "/readme.txt"))
        {
            openFileSystemFolder(%vsDir);
        }
        else
        {
            openFileSystemFolder(%trgdir);
        }
    }
    else
    {
        openFileSystemFolder(%trgdir);
    }
    return ;
}
