$gMyShopAboutLinks = "";
function ClosetTabs::fillMyShopTab(%this) {
    %theTab = %this.getTabWithName("MY DESIGNS");
    if (!(isObject(%theTab))) {
        return;
    }
    %theTab.add(new GuiBitmapCtrl("") {
        profile = 0 @ "ETSNonModalProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "26 26";
        extent = "571 37";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        bitmap = "platform/client/ui/closet_tabs_bracket";
    };);
    if (0) {
        %theTab.add(new GuiTextCtrl("") {
            profile = 0 @ "ClosetTitleProfile";
            horizSizing = "right";
            vertSizing = "bottom";
            position = "26 64";
            extent = "28 20";
            text = "My Designs:";
        };);
    }
    %xPos = 34;
    %xGap = 6;
    %ypos = 84;
    %xSiz = 66;
    %viewType = "Accepted";
    %theTab.add(new GuiVariableWidthButtonCtrl("geClosetMyShopViewButton_" @ %viewType) {
        profile = 0 @ "BracketButton17NonDefaultProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = %xPos @ " " @ %ypos;
        extent = %xSiz @ " " @ 17;
        command = "ClosetGui_MyShop_SetView(\"" @ %viewType @ "\");";
        text = %viewType;
        buttonType = "PushButton";
        canHilite = 0;
    };);
    %xPos = ((%xGap + %xSiz) + %xPos);
    if (0) {
        %xSiz = 64;
        %viewType = "Pending";
        %theTab.add(new GuiVariableWidthButtonCtrl("geClosetMyShopViewButton_" @ %viewType) {
            profile = 0 @ "BracketButton17NonDefaultProfile";
            horizSizing = "right";
            vertSizing = "bottom";
            position = %xPos @ " " @ 84;
            extent = %xSiz @ " " @ 17;
            command = "ClosetGui_MyShop_SetView(\"" @ %viewType @ "\");";
            text = %viewType;
            buttonType = "PushButton";
            canHilite = 0;
        };);
        %xPos = ((%xGap + %xSiz) + %xPos);
        %xSiz = 76;
        %viewType = "Rejected";
        %theTab.add(new GuiVariableWidthButtonCtrl("geClosetMyShopViewButton_" @ %viewType) {
            profile = 0 @ "BracketButton17NonDefaultProfile";
            horizSizing = "right";
            vertSizing = "bottom";
            position = %xPos @ " " @ 84;
            extent = %xSiz @ " " @ 17;
            command = "ClosetGui_MyShop_SetView(\"" @ %viewType @ "\");";
            text = %viewType;
            buttonType = "PushButton";
            canHilite = 0;
        };);
        %xPos = ((%xGap + %xSiz) + %xPos);
    }
    %xSiz = 74;
    %viewType = "Templates";
    %theTab.add(new GuiVariableWidthButtonCtrl("geClosetMyShopViewButton_" @ %viewType) {
        profile = 0 @ "BracketButton17NonDefaultProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = %xPos @ " " @ %ypos;
        extent = %xSiz @ " " @ 17;
        command = "ClosetGui_MyShop_SetView(\"" @ %viewType @ "\");";
        text = %viewType;
        buttonType = "PushButton";
        canHilite = 0;
    };);
    if (0) {
        error("TODO - need to check for FashionPolice permission");
        %viewType = "Incoming";
        %theTab.add(new GuiVariableWidthButtonCtrl("geClosetMyShopViewButton_" @ %viewType) {
            profile = 0 @ "BracketButton17NonDefaultProfile";
            horizSizing = "right";
            vertSizing = "bottom";
            position = %xPos @ " " @ 66;
            extent = %xSiz @ " " @ 17;
            command = "ClosetGui_MyShop_SetView(\"" @ %viewType @ "\");";
            text = %viewType;
            buttonType = "PushButton";
            canHilite = 0;
        };);
        %xPos = ((%xGap + %xSiz) + %xPos);
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
    %itemsFrame.add(%itemsInfoText);
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
    %itemsFrame.add(%ctrl);
    %theTab.rangeText = %ctrl;
    %ctrl = new GuiMLTextCtrl("") {
        profile = 0 @ "ClosetRightInfoProfile";
        horizSizing = "left";
        vertSizing = "bottom";
        position = "338 354";
        extent = "125 14";
        modal = 0;
    };
    %itemsFrame.add(%ctrl);
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
    %ctrl.bindClassName("ClosetItemsScroll");
    %theTab.itemsScroll = %ctrl;
    %itemsFrame.add(%ctrl);
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
    %theTab.itemsScroll.add(%thumbnails);
    %theTab.itemsScroll.thumbnails = %thumbnails;
    %theTab.add(%itemsFrame);
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
    %theTab.add(%itemDescFrame);
    MyShopCopyToOutfitText.setTextWithStyle("<just:right><a:gamelink:COPY_TO_OUTFIT>copy to outfit</a>");
    %theTab.add(new GuiWindowCtrl(MyShopItemDeetsPanel) {
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
    };);
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
    %ctrl.bindClassName("MyShopGenericMLText");
    %ctrl.setTextWithStyle("<a:gamelink:MYDESIGNS_ABOUT DESCSHORT>-?</a>");
    $gMyShopAboutLinks = $gMyShopAboutLinks @ " " @ %ctrl;
    MyShopItemDeetsPanel.add(%ctrl);
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
    %ctrl.bindClassName("MyShopGenericMLText");
    %ctrl.setTextWithStyle("<a:gamelink:MYDESIGNS_ABOUT DESCLONG>-?</a>");
    $gMyShopAboutLinks = $gMyShopAboutLinks @ " " @ %ctrl;
    MyShopItemDeetsPanel.add(%ctrl);
    %letterWords = "A B C D";
    %posX = 0;
    %posY = 0;
    %extX = 84;
    %extY = 84;
    %dx = (13.0 + %extX);
    %dy = (4.0 + %extY);
    %n = 0;
    new GuiControl(MyShopItemDeets_TexturesContainer) {
        profile = "ETSNonModalProfile";
        position = "0 0";
        extent = %extX @ " " @ %extY;
    };.add(new GuiScrollCtrl(MyShopItemDeets_TexturesScroll) {
        profile = MyShopItemDeetsPanel @ "DottedScrollProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "8 43";
        extent = 210 @ " " @ (18.0 + %extY);
        hScrollBar = "dynamic";
        vScrollBar = "alwaysOff";
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
    %ctrl.bindClassName("geTextureDropTarget");
    MyShopItemDeets_TexturesContainer.add(%ctrl);
    geTextureTarget = %ctrl @ %n @ MyShopItemDeetsPanel;
    %posX = (%dx + %posX);
    %n = (1.0 + %n);
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
    %ctrl.bindClassName("geTextureDropTarget");
    MyShopItemDeets_TexturesContainer.add(%ctrl);
    geTextureTarget = %ctrl @ %n @ MyShopItemDeetsPanel;
    %posX = (%dx + %posX);
    %n = (1.0 + %n);
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
    %ctrl.bindClassName("geTextureDropTarget");
    MyShopItemDeets_TexturesContainer.add(%ctrl);
    geTextureTarget = %ctrl @ %n @ MyShopItemDeetsPanel;
    %posX = (%dx + %posX);
    %n = (1.0 + %n);
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
    %ctrl.bindClassName("geTextureDropTarget");
    MyShopItemDeets_TexturesContainer.add(%ctrl);
    geTextureTarget = %ctrl @ %n @ MyShopItemDeetsPanel;
    %posX = (%dx + %posX);
    %n = (1.0 + %n);
    %ctrl = new GuiMLTextCtrl("") {
        horizSizing = 0 @ "right";
        vertSizing = "bottom";
        position = "87 71";
        extent = "10 16";
        stripGamelink = 1;
        style = "plainOnWhiteBlueLinks";
        visible = 0;
    };
    %ctrl.bindClassName("MyShopGenericMLText");
    %ctrl.setTextWithStyle("<a:gamelink:MYDESIGNS_ABOUT TEXTURES>-?</a>");
    $gMyShopAboutLinks = $gMyShopAboutLinks @ " " @ %ctrl;
    MyShopItemDeets_TexturesScroll.add(%ctrl);
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
    MyShopItemDeetsPanel.add(%ctrl);
    %ctrl = new GuiControl(MyShopItemSettings) {
        profile = ETSNonModalProfile;
        position = "8 142";
        extent = "232 80";
        horizSizing = "right";
        vertSizing = "bottom";
        visible = 0;
    };
    MyShopItemDeetsPanel.add(%ctrl);
    %ctrl = new GuiMLTextCtrl("") {
        profile = 0 @ "ETSNonModalProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "0 2";
        extent = "45 18";
        style = "plainOnWhite";
        stripGamelink = 1;
    };
    %ctrl.setTextWithStyle("vBux:");
    MyShopItemSettings.add(%ctrl);
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
    MyShopItemSettings.add(%ctrl);
    %ctrl = new GuiMLTextCtrl("") {
        horizSizing = 0 @ "right";
        vertSizing = "bottom";
        position = "94 2";
        extent = "45 18";
        style = "plainOnWhiteBlueLinks";
        visible = 0;
        stripGamelink = 1;
    };
    %ctrl.bindClassName("MyShopGenericMLText");
    %ctrl.setTextWithStyle("<a:gamelink:MYDESIGNS_ABOUT VBUX>-?</a>");
    $gMyShopAboutLinks = $gMyShopAboutLinks @ " " @ %ctrl;
    MyShopItemSettings.add(%ctrl);
    %ctrl = new GuiMLTextCtrl("") {
        profile = 0 @ "ETSNonModalProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "0 22";
        extent = "45 18";
        style = "plainOnWhite";
    };
    %ctrl.setTextWithStyle("vPoints:");
    MyShopItemSettings.add(%ctrl);
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
    MyShopItemSettings.add(%ctrl);
    %ctrl.setActive(0);
    %ctrl = new GuiMLTextCtrl("") {
        horizSizing = 0 @ "right";
        vertSizing = "bottom";
        position = "94 22";
        extent = "45 18";
        style = "plainOnWhiteBlueLinks";
        visible = 0;
        stripGamelink = 1;
    };
    %ctrl.bindClassName("MyShopGenericMLText");
    %ctrl.setTextWithStyle("<a:gamelink:MYDESIGNS_ABOUT VPOINTS>-?</a>");
    $gMyShopAboutLinks = $gMyShopAboutLinks @ " " @ %ctrl;
    MyShopItemSettings.add(%ctrl);
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
    %ctrl.setValue(1);
    MyShopItemSettings.add(%ctrl);
    %ctrl = new GuiMLTextCtrl("") {
        horizSizing = 0 @ "right";
        vertSizing = "bottom";
        position = "209 0";
        extent = "10 18";
        style = "plainOnWhiteBlueLinks";
        visible = 0;
        stripGamelink = 1;
    };
    %ctrl.bindClassName("MyShopGenericMLText");
    %ctrl.setTextWithStyle("<a:gamelink:MYDESIGNS_ABOUT FOR_SALE>-?</a>");
    $gMyShopAboutLinks = $gMyShopAboutLinks @ " " @ %ctrl;
    MyShopItemSettings.add(%ctrl);
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
    MyShopItemSettings.add(%ctrl);
    %ctrl = new GuiMLTextCtrl("") {
        horizSizing = 0 @ "right";
        vertSizing = "bottom";
        position = "209 15";
        extent = "10 18";
        style = "plainOnWhiteBlueLinks";
        visible = 0;
        stripGamelink = 1;
    };
    %ctrl.bindClassName("MyShopGenericMLText");
    %ctrl.setTextWithStyle("<a:gamelink:MYDESIGNS_ABOUT FEATURED>-?</a>");
    $gMyShopAboutLinks = $gMyShopAboutLinks @ " " @ %ctrl;
    MyShopItemSettings.add(%ctrl);
    %ctrl = new GuiMLTextCtrl(MyShopPriceOutOfRangeText) {
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
    MyShopItemSettings.add(%ctrl);
    %ctrl.setActive(0);
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
    MyShopItemDeetsPanel.add(%ctrl);
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
    %theTab.add(new GuiWindowCtrl(MyShopTextureInspector) {
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
    };);
    new GuiBitmapButtonCtrl("") {
        position = new GuiBitmapCtrl(MyShopTextureInspectorBitmap) {
        profile = new GuiBitmapCtrl("") {
        profile = ETSNonModalProfile;
        position = "1 1";
        extent = (2.0 - %w) @ " " @ (2.0 - %h);
        wrap = 1;
        bitmap = "platform/client/ui/greyChecks";
    }; @ "ETSNonModalProfile";
        position = (2.0 / (%h - %w)) @ " " @ 1;
        extent = (2.0 - %h) @ " " @ (2.0 - %h);
        horizSizing = "width";
        vertSizing = "height";
    }; @ (2.0 - (16.0 - 461.0)) @ " " @ 2;
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
    %theTab.add(%ctrl);
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
    %theTab.add(%ctrl);
    %theTab.cancelButton = %ctrl;
    ClosetTabs.setStoreControlsVisible(1);
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
        %list = SkuManager.getSkusTag("template", $player.getGender());
        %list = %list @ " " @ SkuManager.getSkusTag("template", $player.getOtherGender());
        onGotUGCItems(%type, %list);
    }
    if ((%type $= "ACCEPTED")) {
        %list = SkuManager.getSkusType("mesh");
        %list = SkuManager.filterSkusAuthor(%list, $Player::Name);
        onGotUGCItems(%type, %list);
    }
    error(getScopeName() @ " " @ "- not implemented" @ " " @ %type @ " " @ getTrace());
    if (0) {
        ClosetThumbnailsMyShop.setVisible(%buttonCtrl.infoText, 1);
        ClosetThumbnailsMyShop.setText(%buttonCtrl.infoText, "Fetching..");
        %list = %type[$gSampleUGC @ %type];
        %n = (1.0 - getWordCount(%list));
        if ((0.0 >= %n)) {
            %sku = getWord(%list, %n);
            %si = SkuManager.findBySku(%sku);
            %si.brand = "";
            %si.tags = findAndRemoveAllOccurrencesOfWord(%si.tags, "new");
            %si.expireTime = "";
            if ((%type $= "ACCEPTED")) {
            }
            if ((%type $= "PENDING")) {
                %si.author = $Player::Name;
            }
            %n = (1.0 - %n);
        }
        schedule(500, 0, "onGotUGCItems", %type, %list);
    }
};
function onGotUGCItems(%type, %list) {
    %type[$gUGCSkus @ %type] = %list;
    if ((%type $= "REJECTED")) {
        ClosetThumbnailsMyShop.setUnfilteredSkus("");
        ClosetThumbnailsMyShop.setText(%si.infoText, "rejected list not implemented yet");
    }
    ClosetThumbnailsMyShop.setUnfilteredSkus(%list);
    if ((%list $= "")) {
        %theTab = ClosetTabs.getTabWithName("MY DESIGNS");
        if (%theTab.firstLoad) {
            %theTab.firstLoad = 0;
            ClosetGui_MyShop_SetView("TEMPLATES");
        }
        ClosetThumbnailsMyShop.setText(%theTab.infoText, %type[$MsgCat::MyShop TAB "EMPTYLIST" @ %type]);
    }
    ClosetGui_MyShop_SetCurrentSku(%type[$gUGCPrevSku @ %type]);
};
function ClosetGui_MyShop_GetSkuUGCStatus(%sku) {
    %si = SkuManager.findBySku(%sku);
    if (!(%si.ugcStatus $= "")) {
        return %si.ugcStatus;
    }
    if (%si.hasTag("TEMPLATE")) {
        return "TEMPLATES";
    }
    %statusi = "ACCEPTED PENDING REJECTED TEMPLATES INCOMING";
    %status = "";
    %n = (1.0 - getWordCount(%statusi));
    if ((%status $= "")) {
    }
    if ((0.0 >= %n)) {
        %s = getWord(%statusi, %n);
        if (hasWord(%s[$gUGCSkus @ %s], %sku)) {
            %status = %s;
        }
        %n = (1.0 - %n);
        if ((%status $= "")) {
        }
    }
    %si.ugcStatus = (0.0 >= %n) @ %status;
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
    $gSkusMyShopLayer = SkuManager.overlaySkus($gSkusMyShopLayer, %sku);
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
        MyShopSubmitButton.setText("Update Item");
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
        MyShopSubmitButton.setText("Submit this template as a new item");
    }
    if ((%itemStatus $= "INCOMING")) {
        error(getScopeName() @ " " @ "- not implemented yet:" @ " " @ %itemStatus @ " " @ getTrace());
        %readOnlyDesc = 1;
        %readOnlySettings = 1;
        %showSettings = 1;
        %showSubmit = 1;
        %showAboutLinks = 0;
        %showStartNew = 0;
        MyShopSubmitButton.setText("Approve or Reject this item");
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
    MyShopItemDeets_DescShort.setActive(!(%readOnlyDesc));
    %this.readOnly = %readOnlyDesc @ MyShopItemDeets_DescLong;
    MyShopItemDeets_DescLong.setActive(!(%readOnlyDesc));
    %this.readOnly = %readOnlySettings @ MyShopVBuxField;
    MyShopVBuxField.setActive(!(%readOnlySettings));
    MyShopSubmitButton.setActive(!(%readOnlySettings));
    MyShopSubmitButton.setVisible(%showSubmit);
    MyShopPriceForSaleOption.setActive(!(%readOnlySettings));
    MyShopPriceFeaturedOption.setActive(!(%readOnlySettings));
    MyShopItemSettings.setVisible(%showSettings);
    MyShopStartNewButton.setVisible(%showStartNew);
    $gMyShopAboutLinks = trim($gMyShopAboutLinks);
    %n = (1.0 - getWordCount($gMyShopAboutLinks));
    if ((0.0 >= %n)) {
        %ctrl = getWord($gMyShopAboutLinks, %n);
        %ctrl.setVisible(%showAboutLinks);
        %n = (1.0 - %n);
    }
    MyShopItemDeetsPanel.setSkuBaseTextures(%sku);
    MyShopItemDeets_DescShort.setText("");
    MyShopItemDeets_DescShort.setText("");
    if (((0.0 >= %n) @ " " @ %sku $= "")) {
        MyShopItemDeets_DescShort.setText("no current item");
        MyShopItemDeets_DescLong.setText("");
        $gMyShopCurrentSku = %sku;
        ClosetMainObjectView.zoomToSKU("");
        return;
    }
    %si = SkuManager.findBySku(%sku);
    if (!(isObject(%si))) {
        MyShopItemDeets_DescLong.setText("hmm, something went wrong. please check the bug forum.");
        $gMyShopCurrentSku = "";
        return;
    }
    MyShopItemDeets_DescShort.setText(%si.descShrt);
    MyShopItemDeets_DescLong.setText(%si.descLong);
    $gMyShopCurrentSku = %sku;
    ClosetMainObjectView.zoomToSKU(%sku);
    MyShopVBuxField.setValue(%si.price);
    MyShopVBuxField.onKeystroke();
};
function MyShopItemDeetsPanel::setSkuBaseTextures(%this, %sku) {
    0.setVisible(%this.geTextureTarget, 0);
    1.setVisible(%this.geTextureTarget, 0);
    2.setVisible(%this.geTextureTarget, 0);
    3.setVisible(%this.geTextureTarget, 0);
    %isValid = 1;
    %si = SkuManager.findBySku(%sku);
    if (!(isObject(%si))) {
        %isValid = 0;
    }
    if (%isValid) {
        %baseTextures = %si.getTxtrNames();
        %num = getWordCount(%baseTextures);
        %this.numTextures = %num;
        %n = 0;
        if ((%num < %n)) {
            %baseTexture = getWord(%baseTextures, %n);
            if ((ClosetGui_MyShop_GetSkuUGCStatus(%sku) $= "TEMPLATES")) {
                %path = "user/textures/" @ %baseTexture;
            }
            %path = "projects/common/characters/" @ $player.getGender() @ "_player/" @ %baseTexture;
            if (isFile(%path @ ".jpg")) {
            }
            if (isFile(%path @ ".png")) {
                %n.setBitmap(%this.geTextureTarget, %path);
            }
            %n.setBitmap(%this.geTextureTarget, "projects/vside/worlds/common/swatches/Gray50");
            %n.setVisible(%this.geTextureTarget, 1);
            %this.geTextureTarget.baseTexture = %baseTexture @ %n;
            %this.geTextureTarget.sku = %sku @ %n;
            %n = (1.0 + %n);
        }
    }
    %num = 0;
    (%num < %n);
    %stepX = (getWord(0.getPosition(%this.geTextureTarget), 0) @ 1 - getWord(%this.geTextureTarget.getPosition(), 0));
    %padding = (getWord(0.getExtent(%this.geTextureTarget), 0) - %stepX);
    MyShopItemDeets_TexturesContainer.resize((%padding - (%num * %stepX)), getWord(0.getExtent(%this.geTextureTarget), 1));
    %prevParent = MyShopDragFilesImage.getGroup();
    if (!(%isValid)) {
        MyShopDragFilesImage.setVisible(0);
        MyShopDragFilesImage.reparent(PlayGui, "-1000 -1000", "", "");
        MyShopRefreshTexturesCtrl.setVisible(0);
        MyShopItemDeets_TexturesScroll.setVisible(0);
    }
    if ((1.0 == %num)) {
        MyShopDragFilesImage.setVisible(%si.hasTag("TEMPLATE"));
        MyShopDragFilesImage.reparent(ClosetMainObjectViewContainer, "151 223", "", "");
        %this.modulationColor = "200 50 180 180" @ MyShopDragFilesImage;
        !(MyShopRefreshTexturesCtrl @ " " @ %si.getTxtrNames() $= %si.originalTxtrNames).setVisible();
        MyShopItemDeets_TexturesScroll.setVisible(1);
    }
    MyShopDragFilesImage.setVisible(%si.hasTag("TEMPLATE"));
    MyShopDragFilesImage.reparent(ClosetTabs.getTabWithName("MY DESIGNS"), "907 128", "", "");
    %si.modulationColor = "0 0 0 80" @ MyShopDragFilesImage;
    !(MyShopRefreshTexturesCtrl @ " " @ %si.getTxtrNames() $= %si.originalTxtrNames).setVisible();
    MyShopItemDeets_TexturesScroll.setVisible(1);
    %currParent = MyShopDragFilesImage.getGroup();
    if (%isValid) {
    }
    if ((%prevParent != %currParent)) {
    }
    if (%si.hasTag("TEMPLATE")) {
        MyShopDragFilesImage.FlashVisibility(5, 150);
    }
};
function ClosetGui_MyShop_ToggleCurrentSku(%sku) {
    if ((%sku == $gMyShopCurrentSku)) {
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
    if ((MyShopItemDeetsPanel == %si.numTextures)) {
        0 @ MyShopItemDeetsPanel.applyTexture(%si.geTextureTarget, %text);
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
        %this.applyTexture(%text, %this.sku);
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
        MyShopRefreshTexturesCtrl.setVisible(1);
        %otherExtension = (%extension $= ".png") ? ".jpg" : ".png";
        %otherFullPath = "user/textures/" @ %newTextureName @ %otherExtension;
        if (isFile(%otherFullPath)) {
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
    error(getScopeName() @ " " @ "- could not copy" @ " " @ %texturePath @ " " @ "to" @ " " @ %newFullPath @ " " @ getTrace());
};
function ClosetGUI_RefreshTextures() {
    %n = 0;
    if ((4.0 < %n)) {
        %obj = %this.geTextureTarget;
        %n @ MyShopItemDeetsPanel;
        if (%obj.isVisible()) {
            %obj.applyTexture(%obj.incomingPath);
        }
        %n = (1.0 + %n);
    }
};
function ClosetGui_MyShop_CopySkusToOutfit() {
    $ClosetOutfitName[$ClosetSkusOutfit @ $ClosetOutfitName] = SkuManager.overlaySkus($ClosetOutfitName[$ClosetSkusOutfit @ $ClosetOutfitName], $gSkusMyShopLayer);
};
function MyShopItemDeets_DescShort::onKeystroke(%this) {
    %si = SkuManager.findBySku($gMyShopCurrentSku);
    if (!(isObject(%si))) {
        return;
    }
    %si.descShrt = %this.getValue();
};
function MyShopItemDeets_DescLong::onKeystroke(%this) {
    %si = SkuManager.findBySku($gMyShopCurrentSku);
    if (!(isObject(%si))) {
        return;
    }
    %si.descLong = %this.getValue();
};
function MyShopVBuxField::onKeystroke(%this) {
    %si = SkuManager.findBySku($gMyShopCurrentSku);
    if (!(isObject(%si))) {
        return;
    }
    %priceVBux = %this.getValue();
    %minPrice = %si.price;
    %maxPrice = (1.0 - 1000000.0);
    if ((%minPrice < %priceVBux)) {
        MyShopPriceOutOfRangeText.setTextWithStyle("<just:center>" @ %minPrice @ " " @ "vBux minimum for this item");
        %si.submitPriceValid = 0 @ MyShopItemDeetsPanel;
    }
    if ((%maxPrice > %priceVBux)) {
        MyShopPriceOutOfRangeText.setTextWithStyle("<just:center>" @ %maxPrice @ " " @ "vBux maximum");
        %si.submitPriceValid = 0 @ MyShopItemDeetsPanel;
    }
    MyShopPriceOutOfRangeText.setTextWithStyle("");
    %si.submitPriceValid = 1 @ MyShopItemDeetsPanel;
    MyShopItemDeetsPanel.updateSubmitValidity();
    %itemMultiplier = 1;
    %multiplier = (%itemMultiplier * $gVPointsRatio);
    %priceVPoints = (%multiplier * %priceVBux);
    MyShopVPointsField.setText(%priceVPoints);
};
function MyShopItemDeetsPanel::updateSubmitValidity(%this) {
    %valid = 1;
    %valid = (%this.submitPriceValid & %valid);
    %valid = (%this.submitTexturesValid & %valid);
    %valid = (%this.submitDescriptionValid & %valid);
    if (%valid) {
        MyShopSubmitButton.setActive(1);
        %this.modulationColor = "255 255 255 255" @ MyShopSubmitButton;
    }
    MyShopSubmitButton.setActive(0);
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
    %bitmap = %num @ MyShopItemDeetsPanel.getBitmap(%this.geTextureTarget);
    MyShopTextureInspectorBitmap.setBitmap(%bitmap);
    MyShopTextureInspector.open();
    MyShopTextureInspector.getGroup().pushToBack();
    %this.showingTextureNum = %num @ MyShopTextureInspector;
    MyShopTextureInspector;
};
function MyShopTextureInspector::open(%this) {
    %this.setVisible(1);
    MyShopItemsFrame.setVisible(%this.thumbnails.otherGenderText, 0);
};
function MyShopTextureInspector::close(%this) {
    %this.setVisible(0);
    MyShopItemsFrame.setVisible(%this.thumbnails.thumbnails.otherGenderText, 1);
};
function MyShopStartNewButton::onClick(%this) {
    if (!(ClosetGui_MyShop_GetSkuUGCStatus($gMyShopCurrentSku) $= "TEMPLATES")) {
        %templateSku = SkuManager.findTemplateSku($gMyShopCurrentSku);
        if ((%templateSku $= "")) {
            error(getScopeName() @ " " @ "- can't find template for sku" @ " " @ $gMyShopCurrentSku @ " " @ getTrace());
            return;
        }
        ClosetGui_MyShop_SetView("Templates");
        ClosetGui_MyShop_SetCurrentSku(%templateSku);
        ClosetGui.toggleSku(%templateSku);
    }
    %si = SkuManager.findBySku($gMyShopCurrentSku);
    if (!(isObject(%si))) {
        error(getScopeName() @ " " @ "- invalid sku" @ " " @ $gMyShopCurrentSku @ " " @ getTrace());
        return;
    }
    %trgdir = "user/textures/templates/" @ %si.descShrt @ "/" @ getTimeStamp();
    %n = (1.0 - getWordCount(%si.originalTxtrNames));
    if ((0.0 >= %n)) {
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
        %n @ MyShopItemDeetsPanel.applyTexture(%si.geTextureTarget.geTextureTarget, %trgFile);
        %srcFile = "platform/client/ets/ugcReadMe.txt";
        %trgFile = %trgdir @ "/readme.txt";
        fileCopy(%srcFile, %trgFile);
        %n = (1.0 - %n);
    }
    if (((0.0 >= %n) @ " " @ $Platform $= "windows")) {
    }
    if ((6.0 >= $Platform::Version::Major)) {
        %vsDir = getVirtualStoreDir() @ "/" @ %trgdir;
        if (platformIsFile(%vsDir @ "/readme.txt")) {
            openFileSystemFolder(%vsDir);
        }
        openFileSystemFolder(%trgdir);
    }
    openFileSystemFolder(%trgdir);
};
