$gMyShopAboutLinks = "";
function ClosetTabs::fillMyShopTab(%this) {
    %theTab = "MY DESIGNS".getTabWithName(%this);
    if (!(isObject(%theTab))) {
        return;
    }
    new GuiBitmapCtrl("") {
        profile = 0 @ "ETSNonModalProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "26 26";
        extent = "571 37";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        bitmap = "platform/client/ui/closet_tabs_bracket";
    };.add(%theTab);
    if (0) {
        new GuiTextCtrl("") {
            profile = 0 @ "ClosetTitleProfile";
            horizSizing = "right";
            vertSizing = "bottom";
            position = "26 64";
            extent = "28 20";
            text = "My Designs:";
        };.add(%theTab);
    }
    %xPos = 34;
    %xGap = 6;
    %ypos = 84;
    %xSiz = 66;
    %viewType = "Accepted";
    new GuiVariableWidthButtonCtrl("geClosetMyShopViewButton_" @ %viewType) {
        profile = 0 @ "BracketButton17NonDefaultProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = %xPos @ " " @ %ypos;
        extent = %xSiz @ " " @ 17;
        command = "ClosetGui_MyShop_SetView(\"" @ %viewType @ "\");";
        text = %viewType;
        buttonType = "PushButton";
        canHilite = 0;
    };.add(%theTab);
    %xPos = (%xPos + (%xSiz + %xGap));
    if (0) {
        %xSiz = 64;
        %viewType = "Pending";
        new GuiVariableWidthButtonCtrl("geClosetMyShopViewButton_" @ %viewType) {
            profile = 0 @ "BracketButton17NonDefaultProfile";
            horizSizing = "right";
            vertSizing = "bottom";
            position = %xPos @ " " @ 84;
            extent = %xSiz @ " " @ 17;
            command = "ClosetGui_MyShop_SetView(\"" @ %viewType @ "\");";
            text = %viewType;
            buttonType = "PushButton";
            canHilite = 0;
        };.add(%theTab);
        %xPos = (%xPos + (%xSiz + %xGap));
        %xSiz = 76;
        %viewType = "Rejected";
        new GuiVariableWidthButtonCtrl("geClosetMyShopViewButton_" @ %viewType) {
            profile = 0 @ "BracketButton17NonDefaultProfile";
            horizSizing = "right";
            vertSizing = "bottom";
            position = %xPos @ " " @ 84;
            extent = %xSiz @ " " @ 17;
            command = "ClosetGui_MyShop_SetView(\"" @ %viewType @ "\");";
            text = %viewType;
            buttonType = "PushButton";
            canHilite = 0;
        };.add(%theTab);
        %xPos = (%xPos + (%xSiz + %xGap));
    }
    %xSiz = 74;
    %viewType = "Templates";
    new GuiVariableWidthButtonCtrl("geClosetMyShopViewButton_" @ %viewType) {
        profile = 0 @ "BracketButton17NonDefaultProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = %xPos @ " " @ %ypos;
        extent = %xSiz @ " " @ 17;
        command = "ClosetGui_MyShop_SetView(\"" @ %viewType @ "\");";
        text = %viewType;
        buttonType = "PushButton";
        canHilite = 0;
    };.add(%theTab);
    if (0) {
        error("TODO - need to check for FashionPolice permission");
        %viewType = "Incoming";
        new GuiVariableWidthButtonCtrl("geClosetMyShopViewButton_" @ %viewType) {
            profile = 0 @ "BracketButton17NonDefaultProfile";
            horizSizing = "right";
            vertSizing = "bottom";
            position = %xPos @ " " @ 66;
            extent = %xSiz @ " " @ 17;
            command = "ClosetGui_MyShop_SetView(\"" @ %viewType @ "\");";
            text = %viewType;
            buttonType = "PushButton";
            canHilite = 0;
        };.add(%theTab);
        %xPos = (%xPos + (%xSiz + %xGap));
    }
    %itemsFrame = new GuiControl(MyShopItemsFrame) {
        profile = "GuiDefaultProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "22 120";
        extent = "467 368";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
    };
    %itemsInfoText = new GuiMLTextCtrl("") {
        profile = 0 @ "ClosetLeftInfoProfile";
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
    %itemsInfoText.add(%itemsFrame);
    %ctrl = new GuiTextCtrl("") {
        profile = 0 @ "ClosetRightInfoProfile";
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
    %ctrl.add(%itemsFrame);
    %theTab.rangeText = %ctrl;
    %ctrl = new GuiMLTextCtrl("") {
        profile = 0 @ "ClosetRightInfoProfile";
        horizSizing = "left";
        vertSizing = "bottom";
        position = "338 354";
        extent = "125 14";
        modal = 0;
    };
    %ctrl.add(%itemsFrame);
    %theTab.otherGenderText = %ctrl;
    %ctrl = new GuiScrollCtrl("") {
        profile = 0 @ "ETSScrollProfile";
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
    "ClosetItemsScroll".bindClassName(%ctrl);
    %theTab.itemsScroll = %ctrl;
    %ctrl.add(%itemsFrame);
    %thumbnails = new GuiArray2Ctrl(ClosetThumbnailsMyShop) {
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
    %thumbnails.add(%theTab.itemsScroll);
    %theTab.itemsScroll.thumbnails = %thumbnails;
    %itemsFrame.add(%theTab);
    %theTab.thumbnails = %thumbnails;
    %itemsFrame.thumbnails = %thumbnails;
    %itemDescFrame = new GuiControl("") {
        profile = 0 @ ETSNonModalProfile;
        horizSizing = "right";
        vertSizing = "bottom";
        position = "689 309";
        extent = "245 179";
    };
    new GuiMLTextCtrl(MyShopCopyToOutfitText) {
        horizSizing = new GuiWindowCtrl(MyShopWhatYourWearingContainer) {
        profile = "DottedWindowProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "0 0";
        extent = "245 179";
        resizeWidth = 0;
        resizeHeight = 0;
        canMove = 0;
        canClose = 0;
        canMinimize = 0;
        canMaximize = 0;
        canHilite = 0;
    }; @ "left";
        vertSizing = "bottom";
        position = "0 2";
        extent = "233 18";
        style = "plainOnWhiteSmall";
        stripGamelink = 1;
    };
    %itemDescFrame.add(%theTab);
    "<just:right><a:gamelink:COPY_TO_OUTFIT>copy to outfit</a>".setTextWithStyle(MyShopCopyToOutfitText);
    new GuiWindowCtrl(MyShopItemDeetsPanel) {
        profile = "DottedWindowProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "689 84";
        extent = "245 223";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        resizeWidth = 0;
        resizeHeight = 0;
        canMove = 0;
        canClose = 0;
        canMinimize = 0;
        canMaximize = 0;
        canHilite = 0;
        submitPriceValid = 0;
        submitTexturesValid = 1;
        submitDescriptionValid = 1;
    };.add(%theTab);
    new GuiTextEditCtrl(MyShopItemDeets_DescShort) {
        profile = MyShopItemDeetsPanel @ "Profile_MyShop_TextField_Bold";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "8 3";
        extent = "222 18";
        text = "Item Details";
        readOnly = 1;
        command = "$ThisControl.onKeyStroke();";
    };.add();
    %ctrl = new GuiMLTextCtrl("") {
        horizSizing = 0 @ "right";
        vertSizing = "bottom";
        position = "232 5";
        extent = "10 18";
        style = "plainOnWhiteBlueLinks";
        visible = 0;
        stripGamelink = 1;
    };
    "MyShopGenericMLText".bindClassName(%ctrl);
    "<a:gamelink:MYDESIGNS_ABOUT DESCSHORT>-?</a>".setTextWithStyle(%ctrl);
    $gMyShopAboutLinks = $gMyShopAboutLinks @ " " @ %ctrl;
    %ctrl.add(MyShopItemDeetsPanel);
    new GuiTextEditCtrl(MyShopItemDeets_DescLong) {
        profile = MyShopItemDeetsPanel @ "Profile_MyShop_TextField";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "8 23";
        extent = "222 18";
        text = "Item Details Long";
        readOnly = 1;
        command = "$ThisControl.onKeyStroke();";
    };.add();
    %ctrl = new GuiMLTextCtrl("") {
        horizSizing = 0 @ "right";
        vertSizing = "bottom";
        position = "232 25";
        extent = "10 18";
        style = "plainOnWhiteBlueLinks";
        visible = 0;
        stripGamelink = 1;
    };
    "MyShopGenericMLText".bindClassName(%ctrl);
    "<a:gamelink:MYDESIGNS_ABOUT DESCLONG>-?</a>".setTextWithStyle(%ctrl);
    $gMyShopAboutLinks = $gMyShopAboutLinks @ " " @ %ctrl;
    %ctrl.add(MyShopItemDeetsPanel);
    %letterWords = "A B C D";
    %posX = 0;
    %posY = 0;
    %extX = 84;
    %extY = 84;
    %dx = (%extX + 13.0);
    %dy = (%extY + 4.0);
    %n = 0;
    new GuiScrollCtrl(MyShopItemDeets_TexturesScroll) {
        profile = MyShopItemDeetsPanel @ "DottedScrollProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "8 43";
        extent = 210 @ " " @ (%extY + 18.0);
        hScrollBar = "dynamic";
        vScrollBar = "alwaysOff";
    };.add(new GuiControl(MyShopItemDeets_TexturesContainer) {
        profile = "ETSNonModalProfile";
        position = "0 0";
        extent = %extX @ " " @ %extY;
    };);
    %ctrl = new GuiBitmapCtrl("") {
        profile = 0 @ "ClosetDkBackgroundProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = %posX @ " " @ %posY;
        extent = %extX @ " " @ %extY;
        systemDragDrop = 1;
        canHilite = 1;
        visible = 0;
    };
    new GuiBitmapButtonCtrl("") {
        profile = new GuiMLTextCtrl("") {
        modal = 0;
        position = "3 3";
        extent = "20 20";
        text = "<b><outline><shadowcolor:ffffffd0><color:000000d0>" @ getWord(%letterWords, %n);
    }; @ "GuiDefaultProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "0 0";
        extent = %extX @ " " @ %extY;
        visible = 1;
        command = "MyShop_InspectTexture(" @ %n @ ");";
        canHilite = 0;
        bitmap = "platform/client/buttons/tgf/tgf_buttonframe_100x100";
        lazyLoad = 1;
    };
    "geTextureDropTarget".bindClassName(%ctrl);
    %ctrl.add(MyShopItemDeets_TexturesContainer);
    geTextureTarget = %ctrl @ %n @ MyShopItemDeetsPanel;
    %posX = (%posX + %dx);
    %n = (%n + 1.0);
    %ctrl = new GuiBitmapCtrl("") {
        profile = 0 @ "ClosetDkBackgroundProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = %posX @ " " @ %posY;
        extent = %extX @ " " @ %extY;
        systemDragDrop = 1;
        canHilite = 1;
        visible = 0;
    };
    new GuiBitmapButtonCtrl("") {
        profile = new GuiMLTextCtrl("") {
        modal = 0;
        position = "3 3";
        extent = "20 20";
        text = "<b><outline><shadowcolor:ffffffd0><color:000000d0>" @ getWord(%letterWords, %n);
    }; @ "GuiDefaultProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "0 0";
        extent = %extX @ " " @ %extY;
        visible = 1;
        command = "MyShop_InspectTexture(" @ %n @ ");";
        canHilite = 0;
        bitmap = "platform/client/buttons/tgf/tgf_buttonframe_100x100";
        lazyLoad = 1;
    };
    "geTextureDropTarget".bindClassName(%ctrl);
    %ctrl.add(MyShopItemDeets_TexturesContainer);
    geTextureTarget = %ctrl @ %n @ MyShopItemDeetsPanel;
    %posX = (%posX + %dx);
    %n = (%n + 1.0);
    %ctrl = new GuiBitmapCtrl("") {
        profile = 0 @ "ClosetDkBackgroundProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = %posX @ " " @ %posY;
        extent = %extX @ " " @ %extY;
        systemDragDrop = 1;
        canHilite = 1;
        visible = 0;
    };
    new GuiBitmapButtonCtrl("") {
        profile = new GuiMLTextCtrl("") {
        modal = 0;
        position = "3 3";
        extent = "20 20";
        text = "<b><outline><shadowcolor:ffffffd0><color:000000d0>" @ getWord(%letterWords, %n);
    }; @ "GuiDefaultProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "0 0";
        extent = %extX @ " " @ %extY;
        visible = 1;
        command = "MyShop_InspectTexture(" @ %n @ ");";
        canHilite = 0;
        bitmap = "platform/client/buttons/tgf/tgf_buttonframe_100x100";
        lazyLoad = 1;
    };
    "geTextureDropTarget".bindClassName(%ctrl);
    %ctrl.add(MyShopItemDeets_TexturesContainer);
    geTextureTarget = %ctrl @ %n @ MyShopItemDeetsPanel;
    %posX = (%posX + %dx);
    %n = (%n + 1.0);
    %ctrl = new GuiBitmapCtrl("") {
        profile = 0 @ "ClosetDkBackgroundProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = %posX @ " " @ %posY;
        extent = %extX @ " " @ %extY;
        systemDragDrop = 1;
        canHilite = 1;
        visible = 0;
    };
    new GuiBitmapButtonCtrl("") {
        profile = new GuiMLTextCtrl("") {
        modal = 0;
        position = "3 3";
        extent = "20 20";
        text = "<b><outline><shadowcolor:ffffffd0><color:000000d0>" @ getWord(%letterWords, %n);
    }; @ "GuiDefaultProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "0 0";
        extent = %extX @ " " @ %extY;
        visible = 1;
        command = "MyShop_InspectTexture(" @ %n @ ");";
        canHilite = 0;
        bitmap = "platform/client/buttons/tgf/tgf_buttonframe_100x100";
        lazyLoad = 1;
    };
    "geTextureDropTarget".bindClassName(%ctrl);
    %ctrl.add(MyShopItemDeets_TexturesContainer);
    geTextureTarget = %ctrl @ %n @ MyShopItemDeetsPanel;
    %posX = (%posX + %dx);
    %n = (%n + 1.0);
    %ctrl = new GuiMLTextCtrl("") {
        horizSizing = 0 @ "right";
        vertSizing = "bottom";
        position = "87 71";
        extent = "10 16";
        stripGamelink = 1;
        style = "plainOnWhiteBlueLinks";
        visible = 0;
    };
    "MyShopGenericMLText".bindClassName(%ctrl);
    "<a:gamelink:MYDESIGNS_ABOUT TEXTURES>-?</a>".setTextWithStyle(%ctrl);
    $gMyShopAboutLinks = $gMyShopAboutLinks @ " " @ %ctrl;
    %ctrl.add(MyShopItemDeets_TexturesScroll);
    %ctrl = new GuiVariableWidthButtonCtrl(MyShopRefreshTexturesCtrl) {
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
    %ctrl.add(MyShopItemDeetsPanel);
    %ctrl = new GuiControl(MyShopItemSettings) {
        profile = ETSNonModalProfile;
        position = "8 142";
        extent = "232 80";
        horizSizing = "right";
        vertSizing = "bottom";
        visible = 0;
    };
    %ctrl.add(MyShopItemDeetsPanel);
    %ctrl = new GuiMLTextCtrl("") {
        profile = 0 @ "ETSNonModalProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "0 2";
        extent = "45 18";
        style = "plainOnWhite";
        stripGamelink = 1;
    };
    "vBux:".setTextWithStyle(%ctrl);
    %ctrl.add(MyShopItemSettings);
    %ctrl = new GuiTextEditCtrl(MyShopVBuxField) {
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
    %ctrl.add(MyShopItemSettings);
    %ctrl = new GuiMLTextCtrl("") {
        horizSizing = 0 @ "right";
        vertSizing = "bottom";
        position = "94 2";
        extent = "45 18";
        style = "plainOnWhiteBlueLinks";
        visible = 0;
        stripGamelink = 1;
    };
    "MyShopGenericMLText".bindClassName(%ctrl);
    "<a:gamelink:MYDESIGNS_ABOUT VBUX>-?</a>".setTextWithStyle(%ctrl);
    $gMyShopAboutLinks = $gMyShopAboutLinks @ " " @ %ctrl;
    %ctrl.add(MyShopItemSettings);
    %ctrl = new GuiMLTextCtrl("") {
        profile = 0 @ "ETSNonModalProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "0 22";
        extent = "45 18";
        style = "plainOnWhite";
    };
    "vPoints:".setTextWithStyle(%ctrl);
    %ctrl.add(MyShopItemSettings);
    %ctrl = new GuiTextEditCtrl(MyShopVPointsField) {
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
    %ctrl.add(MyShopItemSettings);
    0.setActive(%ctrl);
    %ctrl = new GuiMLTextCtrl("") {
        horizSizing = 0 @ "right";
        vertSizing = "bottom";
        position = "94 22";
        extent = "45 18";
        style = "plainOnWhiteBlueLinks";
        visible = 0;
        stripGamelink = 1;
    };
    "MyShopGenericMLText".bindClassName(%ctrl);
    "<a:gamelink:MYDESIGNS_ABOUT VPOINTS>-?</a>".setTextWithStyle(%ctrl);
    $gMyShopAboutLinks = $gMyShopAboutLinks @ " " @ %ctrl;
    %ctrl.add(MyShopItemSettings);
    %ctrl = new GuiCheckBoxCtrl(MyShopPriceForSaleOption) {
        profile = "ETSCheckBoxProfile2";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "150 -3";
        extent = "50 18";
        text = "for sale";
        buttonType = "ToggleButton";
        tooltip = "Make this item for sale in your store";
    };
    1.setValue(%ctrl);
    %ctrl.add(MyShopItemSettings);
    %ctrl = new GuiMLTextCtrl("") {
        horizSizing = 0 @ "right";
        vertSizing = "bottom";
        position = "209 0";
        extent = "10 18";
        style = "plainOnWhiteBlueLinks";
        visible = 0;
        stripGamelink = 1;
    };
    "MyShopGenericMLText".bindClassName(%ctrl);
    "<a:gamelink:MYDESIGNS_ABOUT FOR_SALE>-?</a>".setTextWithStyle(%ctrl);
    $gMyShopAboutLinks = $gMyShopAboutLinks @ " " @ %ctrl;
    %ctrl.add(MyShopItemSettings);
    %ctrl = new GuiCheckBoxCtrl(MyShopPriceFeaturedOption) {
        profile = "ETSCheckBoxProfile2";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "150 12";
        extent = "85 18";
        text = "featured";
        buttonType = "ToggleButton";
        tooltip = "Make this your one item in the VHD store";
    };
    %ctrl.add(MyShopItemSettings);
    %ctrl = new GuiMLTextCtrl("") {
        horizSizing = 0 @ "right";
        vertSizing = "bottom";
        position = "209 15";
        extent = "10 18";
        style = "plainOnWhiteBlueLinks";
        visible = 0;
        stripGamelink = 1;
    };
    "MyShopGenericMLText".bindClassName(%ctrl);
    "<a:gamelink:MYDESIGNS_ABOUT FEATURED>-?</a>".setTextWithStyle(%ctrl);
    $gMyShopAboutLinks = $gMyShopAboutLinks @ " " @ %ctrl;
    %ctrl.add(MyShopItemSettings);
    %ctrl = new GuiMLTextCtrl(MyShopPriceOutOfRangeText) {
        profile = "ETSNonModalProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "10 44";
        extent = "214 14";
        style = "plainRedSmall";
        stripGamelink = 1;
    };
    "".setTextWithStyle(%ctrl);
    %ctrl.add(MyShopItemSettings);
    %ctrl = new GuiVariableWidthButtonCtrl(MyShopSubmitButton) {
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
    %ctrl.add(MyShopItemSettings);
    0.setActive(%ctrl);
    %ctrl = new GuiVariableWidthButtonCtrl(MyShopStartNewButton) {
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
    %ctrl.add(MyShopItemDeetsPanel);
    new GuiBitmapCtrl(MyShopDragFilesImage) {
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
    new GuiWindowCtrl(MyShopTextureInspector) {
        profile = "DottedWindowProfile";
        position = "25 65";
        extent = %w @ " " @ %h;
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
    };.add(%theTab);
    new GuiBitmapButtonCtrl("") {
        position = new GuiBitmapCtrl(MyShopTextureInspectorBitmap) {
        profile = new GuiBitmapCtrl("") {
        profile = ETSNonModalProfile;
        position = "1 1";
        extent = (%w - 2.0) @ " " @ (%h - 2.0);
        wrap = 1;
        bitmap = "platform/client/ui/greyChecks";
    }; @ "ETSNonModalProfile";
        position = ((%w - %h) / 2.0) @ " " @ 1;
        extent = (%h - 2.0) @ " " @ (%h - 2.0);
        horizSizing = "width";
        vertSizing = "height";
    }; @ ((461.0 - 16.0) - 2.0) @ " " @ 2;
        extent = "16 16";
        command = "MyShopTextureInspector.close();";
        bitmap = "platform/client/buttons/gray_close";
    };
    %ctrl = new GuiVariableWidthButtonCtrl("") {
        profile = 0 @ "BracketButton19Profile";
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
    %ctrl.add(%theTab);
    %theTab.doneButton = %ctrl;
    %ctrl = new GuiVariableWidthButtonCtrl("") {
        profile = 0 @ "BracketButton19NonDefaultProfile";
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
    %ctrl.add(%theTab);
    %theTab.cancelButton = %ctrl;
    1.setStoreControlsVisible(ClosetTabs);
    %theTab.firstLoad = 1;
    ClosetGui_MyShop_SetView(ACCEPTED);
    %this.tabMyShopInitialized = 1;
};
$gPrevMyShopTypeButton = "";
function ClosetGui_MyShop_SetView(%viewType) {
    if (isObject($gPrevMyShopTypeButton)) {
        $gPrevMyShopTypeButton.setProfile();
        $gPrevMyShopTypeButton.modal = BracketButton17NonDefaultProfile @ 1;
    }
    %buttonCtrl = "geClosetMyShopViewButton_" @ %viewType;
    %buttonCtrl.setProfile();
    %buttonCtrl.modal = BracketButton17Profile @ 0;
    $gPrevMyShopTypeButton = %buttonCtrl;
    getUGCItems(%viewType);
};
function getUGCItems(%type) {
    %type[$gUGCSkus @ %type] = "";
    if ((%type $= "TEMPLATES")) {
        %list = $player.getGender().getSkusTag(SkuManager, "template");
        %list = %list @ " " @ $player.getOtherGender().getSkusTag(SkuManager, "template");
        onGotUGCItems(%type, %list);
    }
    if ((%type $= "ACCEPTED")) {
        %list = "mesh".getSkusType(SkuManager);
        %list = $Player::Name.filterSkusAuthor(SkuManager, %list);
        onGotUGCItems(%type, %list);
    }
    error(getScopeName() @ " " @ "- not implemented" @ " " @ %type @ " " @ getTrace());
    if (0) {
        1.setVisible(ClosetThumbnailsMyShop, %buttonCtrl.infoText);
        "Fetching..".setText(ClosetThumbnailsMyShop, %buttonCtrl.infoText);
        %list = %type[$gSampleUGC @ %type];
        %n = (getWordCount(%list) - 1.0);
        while ((%n >= 0.0)) {
            %sku = getWord(%list, %n);
            %si = %sku.findBySku(SkuManager);
            %si.brand = "";
            %si.tags = findAndRemoveAllOccurrencesOfWord(%si.tags, "new");
            %si.expireTime = "";
            if ((%type $= "ACCEPTED")) {
            }
            if ((%type $= "PENDING")) {
                %si.author = $Player::Name;
            }
            %n = (%n - 1.0);
        }
        schedule(500, 0, "onGotUGCItems", %type, %list);
    }
};
function onGotUGCItems(%type, %list) {
    %type[$gUGCSkus @ %type] = %list;
    if ((%type $= "REJECTED")) {
        "".setUnfilteredSkus(ClosetThumbnailsMyShop);
        "rejected list not implemented yet".setText(ClosetThumbnailsMyShop, %si.infoText);
    }
    %list.setUnfilteredSkus(ClosetThumbnailsMyShop);
    if ((%list $= "")) {
        %theTab = "MY DESIGNS".getTabWithName(ClosetTabs);
        if (%theTab.firstLoad) {
            %theTab.firstLoad = 0;
            ClosetGui_MyShop_SetView("TEMPLATES");
        }
        %type[$MsgCat::MyShop TAB "EMPTYLIST" @ %type].setText(ClosetThumbnailsMyShop, %theTab.infoText);
    }
    ClosetGui_MyShop_SetCurrentSku(%type[$gUGCPrevSku @ %type]);
};
function ClosetGui_MyShop_GetSkuUGCStatus(%sku) {
    %si = %sku.findBySku(SkuManager);
    if (!(%si.ugcStatus $= "")) {
        return %si.ugcStatus;
    }
    if ("TEMPLATE".hasTag(%si)) {
        return "TEMPLATES";
    }
    %statusi = "ACCEPTED PENDING REJECTED TEMPLATES INCOMING";
    %status = "";
    %n = (getWordCount(%statusi) - 1.0);
    if ((%status $= "")) {
    }
    while ((%n >= 0.0)) {
        %s = getWord(%statusi, %n);
        if (hasWord(%s[$gUGCSkus @ %s], %sku)) {
            %status = %s;
        }
        %n = (%n - 1.0);
        if ((%status $= "")) {
        }
    }
    %si.ugcStatus = (%n >= 0.0) @ %status;
    if ((%status $= "")) {
        error(getScopeName() @ " " @ "- no UGC status for sku" @ " " @ %sku @ " " @ getTrace());
    }
    return %status;
};
function ClosetGui_MyShop_GetSkuUGCStatusIcon(%sku) {
    %status = ClosetGui_MyShop_GetSkuUGCStatus(%sku);
    if ((%status $= "")) {
        %ret = "";
    }
    %ret = "platform/client/ui/ugcStatus_" @ %status;
    return %ret;
};
function MyShopItemsFrame::update(%this) {
    if (!(ClosetTabs.getCurrentTab().name $= "MY DESIGNS")) {
        return;
    }
    %this.thumbnails.refilter();
    ClosetGui_MyShop_SetCurrentSku($gMyShopCurrentSku);
};
$gSkusMyShopLayer = "";
$gMyShopCurrentSku = "";
function ClosetGUI_ToggleSku_MyShop(%sku) {
    if (hasWord($gSkusMyShopLayer, %sku)) {
        $gSkusMyShopLayer = findAndRemoveAllOccurrencesOfWord($gSkusMyShopLayer, %sku);
        ClosetGui_MyShop_SetCurrentSku("");
    }
    $gSkusMyShopLayer = %sku.overlaySkus(SkuManager, $gSkusMyShopLayer);
    ClosetGui_MyShop_SetCurrentSku(%sku);
};
function ClosetGui_MyShop_SetCurrentSku(%sku) {
    if ((%sku $= "")) {
        %itemStatus = "";
    }
    %itemStatus = ClosetGui_MyShop_GetSkuUGCStatus(%sku);
    %itemStatus[$gUGCPrevSku @ %itemStatus] = %sku;
    if ((%itemStatus $= "ACCEPTED")) {
        %readOnlyDesc = 1;
        %readOnlySettings = 0;
        %showSettings = 1;
        %showSubmit = 1;
        %showAboutLinks = 1;
        %showStartNew = 1;
        "Update Item".setText(MyShopSubmitButton);
    }
    if ((%itemStatus $= "PENDING")) {
        error(getScopeName() @ " " @ "- not implemented yet:" @ " " @ %itemStatus @ " " @ getTrace());
        %readOnlyDesc = 1;
        %readOnlySettings = 1;
        %showSettings = 1;
        %showSubmit = 0;
        %showAboutLinks = 1;
        %showStartNew = 1;
    }
    if ((%itemStatus $= "REJECTED")) {
        error(getScopeName() @ " " @ "- not implemented yet:" @ " " @ %itemStatus @ " " @ getTrace());
        %readOnlyDesc = 1;
        %readOnlySettings = 1;
        %showSettings = 1;
        %showSubmit = 0;
        %showAboutLinks = 0;
        %showStartNew = 0;
    }
    if ((%itemStatus $= "TEMPLATES")) {
        %readOnlyDesc = 0;
        %readOnlySettings = 0;
        %showSettings = 1;
        %showSubmit = 1;
        %showAboutLinks = 1;
        %showStartNew = 1;
        "Submit this template as a new item".setText(MyShopSubmitButton);
    }
    if ((%itemStatus $= "INCOMING")) {
        error(getScopeName() @ " " @ "- not implemented yet:" @ " " @ %itemStatus @ " " @ getTrace());
        %readOnlyDesc = 1;
        %readOnlySettings = 1;
        %showSettings = 1;
        %showSubmit = 1;
        %showAboutLinks = 0;
        %showStartNew = 0;
        "Approve or Reject this item".setText(MyShopSubmitButton);
    }
    %readOnlyDesc = 1;
    %readOnlySettings = 1;
    %showSettings = 0;
    %showSubmit = 0;
    %showAboutLinks = 0;
    %showStartNew = 0;
    %readOnlyDesc = 1;
    %readOnlySettings = 1;
    %showSettings = 0;
    %showSubmit = 0;
    %showAboutLinks = 0;
    %this.border = %readOnlyDesc ? 0 : 1 @ Profile_MyShop_TextField;
    %this.border = %readOnlyDesc ? 0 : 1 @ Profile_MyShop_TextField_Bold;
    %this.border = %readOnlySettings ? 0 : 1 @ Profile_MyShop_SettingsField;
    %this.readOnly = %readOnlyDesc @ MyShopItemDeets_DescShort;
    !(%readOnlyDesc).setActive(MyShopItemDeets_DescShort);
    %this.readOnly = %readOnlyDesc @ MyShopItemDeets_DescLong;
    !(%readOnlyDesc).setActive(MyShopItemDeets_DescLong);
    %this.readOnly = %readOnlySettings @ MyShopVBuxField;
    !(%readOnlySettings).setActive(MyShopVBuxField);
    !(%readOnlySettings).setActive(MyShopSubmitButton);
    %showSubmit.setVisible(MyShopSubmitButton);
    !(%readOnlySettings).setActive(MyShopPriceForSaleOption);
    !(%readOnlySettings).setActive(MyShopPriceFeaturedOption);
    %showSettings.setVisible(MyShopItemSettings);
    %showStartNew.setVisible(MyShopStartNewButton);
    $gMyShopAboutLinks = trim($gMyShopAboutLinks);
    %n = (getWordCount($gMyShopAboutLinks) - 1.0);
    while ((%n >= 0.0)) {
        %ctrl = getWord($gMyShopAboutLinks, %n);
        %showAboutLinks.setVisible(%ctrl);
        %n = (%n - 1.0);
    }
    %sku.setSkuBaseTextures(MyShopItemDeetsPanel);
    "".setText(MyShopItemDeets_DescShort);
    "".setText(MyShopItemDeets_DescShort);
    if (((%n >= 0.0) @ " " @ %sku $= "")) {
        "no current item".setText(MyShopItemDeets_DescShort);
        "".setText(MyShopItemDeets_DescLong);
        $gMyShopCurrentSku = %sku;
        "".zoomToSKU(ClosetMainObjectView);
        return;
    }
    %si = %sku.findBySku(SkuManager);
    if (!(isObject(%si))) {
        "hmm, something went wrong. please check the bug forum.".setText(MyShopItemDeets_DescLong);
        $gMyShopCurrentSku = "";
        return;
    }
    %si.descShrt.setText(MyShopItemDeets_DescShort);
    %si.descLong.setText(MyShopItemDeets_DescLong);
    $gMyShopCurrentSku = %sku;
    %sku.zoomToSKU(ClosetMainObjectView);
    %si.price.setValue(MyShopVBuxField);
    MyShopVBuxField.onKeystroke();
};
function MyShopItemDeetsPanel::setSkuBaseTextures(%this, %sku) {
    0.setVisible(0, %this.geTextureTarget);
    0.setVisible(1, %this.geTextureTarget);
    0.setVisible(2, %this.geTextureTarget);
    0.setVisible(3, %this.geTextureTarget);
    %isValid = 1;
    %si = %sku.findBySku(SkuManager);
    if (!(isObject(%si))) {
        %isValid = 0;
    }
    if (%isValid) {
        %baseTextures = %si.getTxtrNames();
        %num = getWordCount(%baseTextures);
        %this.numTextures = %num;
        %n = 0;
        while ((%n < %num)) {
            %baseTexture = getWord(%baseTextures, %n);
            if ((ClosetGui_MyShop_GetSkuUGCStatus(%sku) $= "TEMPLATES")) {
                %path = "user/textures/" @ %baseTexture;
            }
            %path = "projects/common/characters/" @ $player.getGender() @ "_player/" @ %baseTexture;
            if (isFile(%path @ ".jpg")) {
            }
            if (isFile(%path @ ".png")) {
                %path.setBitmap(%n, %this.geTextureTarget);
            }
            "projects/vside/worlds/common/swatches/Gray50".setBitmap(%n, %this.geTextureTarget);
            1.setVisible(%n, %this.geTextureTarget);
            %this.geTextureTarget.baseTexture = %baseTexture @ %n;
            %this.geTextureTarget.sku = %sku @ %n;
            %n = (%n + 1.0);
        }
    }
    %num = 0;
    (%n < %num);
    %stepX = (getWord(%this.geTextureTarget.getPosition(), 0) - getWord(%this.geTextureTarget.getPosition(0), 0) @ 1);
    %padding = (%stepX - getWord(%this.geTextureTarget.getExtent(0), 0));
    getWord(%this.geTextureTarget.getExtent(0), 1).resize(MyShopItemDeets_TexturesContainer, ((%stepX * %num) - %padding));
    %prevParent = MyShopDragFilesImage.getGroup();
    if (!(%isValid)) {
        0.setVisible(MyShopDragFilesImage);
        "".reparent(MyShopDragFilesImage, PlayGui, "-1000 -1000", "");
        0.setVisible(MyShopRefreshTexturesCtrl);
        0.setVisible(MyShopItemDeets_TexturesScroll);
    }
    if ((%num == 1.0)) {
        "TEMPLATE".hasTag(%si).setVisible(MyShopDragFilesImage);
        "".reparent(MyShopDragFilesImage, ClosetMainObjectViewContainer, "151 223", "");
        %this.modulationColor = "200 50 180 180" @ MyShopDragFilesImage;
        !(MyShopRefreshTexturesCtrl @ " " @ %si.getTxtrNames() $= %si.originalTxtrNames).setVisible();
        1.setVisible(MyShopItemDeets_TexturesScroll);
    }
    "TEMPLATE".hasTag(%si).setVisible(MyShopDragFilesImage);
    "".reparent(MyShopDragFilesImage, "MY DESIGNS".getTabWithName(ClosetTabs), "907 128", "");
    %si.modulationColor = "0 0 0 80" @ MyShopDragFilesImage;
    !(MyShopRefreshTexturesCtrl @ " " @ %si.getTxtrNames() $= %si.originalTxtrNames).setVisible();
    1.setVisible(MyShopItemDeets_TexturesScroll);
    %currParent = MyShopDragFilesImage.getGroup();
    if (%isValid) {
    }
    if ((%currParent != %prevParent)) {
    }
    if ("TEMPLATE".hasTag(%si)) {
        150.FlashVisibility(MyShopDragFilesImage, 5);
    }
};
function ClosetGui_MyShop_ToggleCurrentSku(%sku) {
    if (($gMyShopCurrentSku == %sku)) {
        %sku = "";
    }
    ClosetGui_MyShop_SetCurrentSku(%sku);
};
function MyShopItemDeets_Desc::onURL(%this, %url) {
    ClosetGui_MyShop_onURL(%url, %this);
};
function MyShopCopyToOutfitText::onURL(%this, %url) {
    ClosetGui_MyShop_onURL(%url, %this);
};
function MyShopGenericMLText::onURL(%this, %url) {
    ClosetGui_MyShop_onURL(%url, %this);
};
function ClosetGui_MyShop_onURL(%url, %mlTextCtrl) {
    if ((getWord(%url, 0) $= "SELECTNONE")) {
        ClosetGui_MyShop_SetCurrentSku("");
    }
    if ((getWord(%url, 0) $= "TOGGLESKU")) {
        %sku = getWord(%url, 1);
        ClosetGUI_ToggleSku_MyShop(%sku);
    }
    if ((getWord(%url, 0) $= "SELECTSKU")) {
        %sku = getWord(%url, 1);
        ClosetGui_MyShop_SetCurrentSku(%sku);
    }
    if ((getWord(%url, 0) $= "COPY_TO_OUTFIT")) {
        userTips::showNow("closet_myshop_copyToOutfit");
    }
    if ((getWord(%url, 0) $= "REFRESH_TEXTURES")) {
        ClosetGUI_RefreshTextures();
    }
    if ((getWord(%url, 0) $= "MYDESIGNS_ABOUT")) {
        ClosetGui_About("MY DESIGNS", getWord(%url, 1));
    }
    error(getScopeName() @ " " @ "- unknown command" @ " " @ %url @ " " @ getDebugString(%mlTextCtrl) @ " " @ getTrace());
};
function MyShopTextureInspector::onSystemDragDroppedEvent(%this, %text, %pt) {
    ClosetMainObjectView::onSystemDragDroppedEvent_MyShop(%this, %text, %pt);
};
function ClosetMainObjectView::acceptsSystemDragDropContent(%this, %text) {
    return geTextureDropTarget::acceptsSystemDragDropContent(%this, %text);
};
function ClosetMainObjectView::onSystemDragDroppedEvent_MyShop(%this, %text, %pt) {
    hiliteControl("");
    if ((%si.numTextures == MyShopItemDeetsPanel)) {
        %text.applyTexture(0 @ MyShopItemDeetsPanel, %si.geTextureTarget);
    }
    MessageBoxOK("Use the texture list", 1.0);
};
function geTextureDropTarget::acceptsSystemDragDropContent(%this, %text) {
    if (!(ClosetGui_MyShop_GetSkuUGCStatus($gMyShopCurrentSku) $= "TEMPLATES")) {
        return 0;
    }
    if (hasSubString(%text, "http:")) {
        return 0;
    }
    %extension = getExtension(%text);
    if (!(%extension $= ".jpg")) {
    }
    if (!(%extension $= ".png")) {
        echo(getScopeName() @ " " @ "- invalid extension:" @ " " @ %text);
        return 0;
    }
    return 1;
};
function geTextureDropTarget::onSystemDragDropEvent(%this, %text, %eventType, %pt) {
    if (!(Parent::onSystemDragDropEvent(%this, %text, %eventType, %pt))) {
        return 0;
    }
    if ((%eventType $= "BREAK")) {
        %this.sku.applyTexture(%this, %text);
    }
    return 1;
};
function geTextureDropTarget::applyTexture(%this, %texturePath) {
    %newTextureName = %this.baseTexture;
    %extension = getExtension(%texturePath);
    %newFullPathNoExt = "user/textures/" @ %newTextureName;
    %newFullPath = %newFullPathNoExt @ %extension;
    %this.incomingPath = %texturePath;
    if (($Platform $= "macos")) {
        %newFullPath = getPrefsDir() @ "/" @ %newFullPath;
    }
    %ok = fileCopy(%texturePath, %newFullPath);
    if (%ok) {
        1.setVisible(MyShopRefreshTexturesCtrl);
        %otherExtension = (%extension $= ".png") ? ".jpg" : ".png";
        %otherFullPath = "user/textures/" @ %newTextureName @ %otherExtension;
        if (isFile(%otherFullPath)) {
            deleteFile(%otherFullPath);
        }
        removeFile(%newFullPath);
        addFile(%newFullPath);
        "".setBitmap(%this);
        %newFullPathNoExt.setBitmap(%this);
        "".setBitmap(MyShopTextureInspectorBitmap);
        %newFullPathNoExt.setBitmap(MyShopTextureInspectorBitmap);
        reloadMeshTexture(%this.textureName, %newFullPathNoExt);
        %newTextureName.replaceTextureName(%this.sku.findBySku(SkuManager));
    }
    error(getScopeName() @ " " @ "- could not copy" @ " " @ %texturePath @ " " @ "to" @ " " @ %newFullPath @ " " @ getTrace());
};
function ClosetGUI_RefreshTextures() {
    %n = 0;
    while ((%n < 4.0)) {
        %obj = %this.geTextureTarget;
        %n @ MyShopItemDeetsPanel;
        if (%obj.isVisible()) {
            %obj.incomingPath.applyTexture(%obj);
        }
        %n = (%n + 1.0);
    }
};
function ClosetGui_MyShop_CopySkusToOutfit() {
    $ClosetOutfitName[$ClosetSkusOutfit @ $ClosetOutfitName] = $gSkusMyShopLayer.overlaySkus(SkuManager, $ClosetOutfitName[$ClosetSkusOutfit @ $ClosetOutfitName]);
};
function MyShopItemDeets_DescShort::onKeystroke(%this) {
    %si = $gMyShopCurrentSku.findBySku(SkuManager);
    if (!(isObject(%si))) {
        return;
    }
    %si.descShrt = %this.getValue();
};
function MyShopItemDeets_DescLong::onKeystroke(%this) {
    %si = $gMyShopCurrentSku.findBySku(SkuManager);
    if (!(isObject(%si))) {
        return;
    }
    %si.descLong = %this.getValue();
};
function MyShopVBuxField::onKeystroke(%this) {
    %si = $gMyShopCurrentSku.findBySku(SkuManager);
    if (!(isObject(%si))) {
        return;
    }
    %priceVBux = %this.getValue();
    %minPrice = %si.price;
    %maxPrice = (1000000.0 - 1.0);
    if ((%priceVBux < %minPrice)) {
        "<just:center>" @ %minPrice @ " " @ "vBux minimum for this item".setTextWithStyle(MyShopPriceOutOfRangeText);
        %si.submitPriceValid = 0 @ MyShopItemDeetsPanel;
    }
    if ((%priceVBux > %maxPrice)) {
        "<just:center>" @ %maxPrice @ " " @ "vBux maximum".setTextWithStyle(MyShopPriceOutOfRangeText);
        %si.submitPriceValid = 0 @ MyShopItemDeetsPanel;
    }
    "".setTextWithStyle(MyShopPriceOutOfRangeText);
    %si.submitPriceValid = 1 @ MyShopItemDeetsPanel;
    MyShopItemDeetsPanel.updateSubmitValidity();
    %itemMultiplier = 1;
    %multiplier = ($gVPointsRatio * %itemMultiplier);
    %priceVPoints = (%priceVBux * %multiplier);
    %priceVPoints.setText(MyShopVPointsField);
};
function MyShopItemDeetsPanel::updateSubmitValidity(%this) {
    %valid = 1;
    %valid = (%valid & %this.submitPriceValid);
    %valid = (%valid & %this.submitTexturesValid);
    %valid = (%valid & %this.submitDescriptionValid);
    if (%valid) {
        1.setActive(MyShopSubmitButton);
        %this.modulationColor = "255 255 255 255" @ MyShopSubmitButton;
    }
    0.setActive(MyShopSubmitButton);
    %this.modulationColor = "255 255 255 64" @ MyShopSubmitButton;
};
function MyShopSubmitButton::onClick(%this) {
    %itemType = ClosetGui_MyShop_GetSkuUGCStatus($gMyShopCurrentSku);
    MessageBoxOkCancel(%itemType[$gMyShopSubmitTitle @ %itemType], %itemType[$gMyShopSubmitBody @ %itemType], "MessageBoxOK(\"not implemented\", \"\", \"\");", "");
};
function MyShop_InspectTexture(%num) {
    if (MyShopTextureInspector.isVisible()) {
    }
    if ((MyShopTextureInspector @ " " @ %this.showingTextureNum $= %num)) {
        MyShopTextureInspector.close();
        %this.showingTextureNum = "" @ MyShopTextureInspector;
    }
    %bitmap = %this.geTextureTarget.getBitmap(%num @ MyShopItemDeetsPanel);
    %bitmap.setBitmap(MyShopTextureInspectorBitmap);
    MyShopTextureInspector.open();
    MyShopTextureInspector.getGroup().pushToBack();
    %this.showingTextureNum = %num @ MyShopTextureInspector;
    MyShopTextureInspector;
};
function MyShopTextureInspector::open(%this) {
    1.setVisible(%this);
    0.setVisible(MyShopItemsFrame, %this.thumbnails.otherGenderText);
};
function MyShopTextureInspector::close(%this) {
    0.setVisible(%this);
    1.setVisible(MyShopItemsFrame, %this.thumbnails.thumbnails.otherGenderText);
};
function MyShopStartNewButton::onClick(%this) {
    if (!(ClosetGui_MyShop_GetSkuUGCStatus($gMyShopCurrentSku) $= "TEMPLATES")) {
        %templateSku = $gMyShopCurrentSku.findTemplateSku(SkuManager);
        if ((%templateSku $= "")) {
            error(getScopeName() @ " " @ "- can't find template for sku" @ " " @ $gMyShopCurrentSku @ " " @ getTrace());
            return;
        }
        ClosetGui_MyShop_SetView("Templates");
        ClosetGui_MyShop_SetCurrentSku(%templateSku);
        %templateSku.toggleSku(ClosetGui);
    }
    %si = $gMyShopCurrentSku.findBySku(SkuManager);
    if (!(isObject(%si))) {
        error(getScopeName() @ " " @ "- invalid sku" @ " " @ $gMyShopCurrentSku @ " " @ getTrace());
        return;
    }
    %trgdir = "user/textures/templates/" @ %si.descShrt @ "/" @ getTimeStamp();
    %n = (getWordCount(%si.originalTxtrNames) - 1.0);
    while ((%n >= 0.0)) {
        %originalName = getWord(%si.originalTxtrNames, %n);
        %originalPath = "projects/common/characters/" @ $player.getGender() @ "_player/" @ %originalName;
        %textureName = %originalPath;
        %ext = "";
        %try = ".png";
        if ((%ext $= "")) {
        }
        if (isFile(%originalPath @ %try)) {
            %ext = %try;
        }
        %try = ".jpg";
        if ((%ext $= "")) {
        }
        if (isFile(%originalPath @ %try)) {
            %ext = %try;
        }
        %try = ".png";
        if ((%ext $= "")) {
        }
        if (isFile($DC::CacheFolderName @ "/" @ %originalPath @ %try)) {
            %ext = %try;
            %originalPath = $DC::CacheFolderName @ "/" @ %originalPath;
        }
        if ((%ext $= "")) {
            error(getScopeName() @ " " @ "- can't find original file" @ " " @ %originalPath @ " " @ getTrace());
            return;
        }
        %srcFile = %originalPath @ %ext;
        if (($Platform $= "macos")) {
            %workingDir = getPrefsDir();
            %trgdir = %workingDir @ "/" @ %trgdir;
            %srcFile = %workingDir @ "/" @ %srcFile;
        }
        %trgFile = %trgdir @ "/" @ %originalName @ %ext;
        echoDebug(getScopeName() @ " " @ "- copying" @ " " @ %srcFile @ " " @ "to" @ " " @ %trgFile);
        if (!(fileCopy(%srcFile, %trgFile))) {
            error(getScopeName() @ " " @ "- error copying" @ " " @ %srcFile @ " " @ "to" @ " " @ %trgFile);
            return;
        }
        %si.geTextureTarget.textureName = %textureName @ %n @ MyShopItemDeetsPanel;
        %trgFile.applyTexture(%n @ MyShopItemDeetsPanel, %si.geTextureTarget.geTextureTarget);
        %srcFile = "platform/client/ets/ugcReadMe.txt";
        %trgFile = %trgdir @ "/readme.txt";
        fileCopy(%srcFile, %trgFile);
        %n = (%n - 1.0);
    }
    if (((%n >= 0.0) @ " " @ $Platform $= "windows")) {
    }
    if (($Platform::Version::Major >= 6.0)) {
        %vsDir = getVirtualStoreDir() @ "/" @ %trgdir;
        if (platformIsFile(%vsDir @ "/readme.txt")) {
            openFileSystemFolder(%vsDir);
        }
        openFileSystemFolder(%trgdir);
    }
    openFileSystemFolder(%trgdir);
};
