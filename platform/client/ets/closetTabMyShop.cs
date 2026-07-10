$gMyShopAboutLinks = "";
function ClosetTabs::fillMyShopTab(%this) {
    %theTab = %this.getTabWithName("MY DESIGNS");
    if (!(isObject(%theTab))) {
        return;
    }
    0;
    %theTab.add(new ""() {
        profile = GuiBitmapCtrl @ "ETSNonModalProfile";
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
        0;
        %theTab.add(new ""() {
            profile = GuiTextCtrl @ "ClosetTitleProfile";
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
    0;
    %theTab.add(new "geClosetMyShopViewButton_" @ %viewType() {
        profile = GuiVariableWidthButtonCtrl @ "BracketButton17NonDefaultProfile";
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
        0;
        %theTab.add(new "geClosetMyShopViewButton_" @ %viewType() {
            profile = GuiVariableWidthButtonCtrl @ "BracketButton17NonDefaultProfile";
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
        0;
        %theTab.add(new "geClosetMyShopViewButton_" @ %viewType() {
            profile = GuiVariableWidthButtonCtrl @ "BracketButton17NonDefaultProfile";
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
    0;
    %theTab.add(new "geClosetMyShopViewButton_" @ %viewType() {
        profile = GuiVariableWidthButtonCtrl @ "BracketButton17NonDefaultProfile";
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
        0;
        %theTab.add(new "geClosetMyShopViewButton_" @ %viewType() {
            profile = GuiVariableWidthButtonCtrl @ "BracketButton17NonDefaultProfile";
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
    0;
    %itemsInfoText = new ""() {
        profile = GuiMLTextCtrl @ "ClosetLeftInfoProfile";
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
    0;
    %ctrl = new ""() {
        profile = GuiTextCtrl @ "ClosetRightInfoProfile";
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
    0;
    %ctrl = new ""() {
        profile = GuiMLTextCtrl @ "ClosetRightInfoProfile";
        horizSizing = "left";
        vertSizing = "bottom";
        position = "338 354";
        extent = "125 14";
        modal = 0;
    };
    %itemsFrame.add(%ctrl);
    %theTab.otherGenderText = %ctrl;
    0;
    %ctrl = new ""() {
        profile = GuiScrollCtrl @ "ETSScrollProfile";
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
    0;
    %itemDescFrame = new ""() {
        profile = GuiControl @ ETSNonModalProfile;
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
    "<just:right><a:gamelink:COPY_TO_OUTFIT>copy to outfit</a>".setTextWithStyle();
    %theTab.add(new GuiWindowCtrl(MyShopItemDeetsPanel) {
        profile = MyShopCopyToOutfitText @ "DottedWindowProfile";
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
    0;
    %ctrl = new ""() {
        horizSizing = GuiMLTextCtrl @ "right";
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
    %ctrl.add();
    MyShopItemDeetsPanel;
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
    0;
    %ctrl = new ""() {
        horizSizing = GuiMLTextCtrl @ "right";
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
    %ctrl.add();
    %letterWords = "A B C D";
    MyShopItemDeetsPanel;
    %posX = 0;
    %posY = 0;
    %extX = 84;
    %extY = 84;
    %dx = (13.0 + %extX);
    %dy = (4.0 + %extY);
    %n = 0;
    new GuiScrollCtrl(MyShopItemDeets_TexturesScroll) {
        profile = MyShopItemDeetsPanel @ "DottedScrollProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "8 43";
        extent = 210 @ " " @ (18.0 + %extY);
        hScrollBar = "dynamic";
        vScrollBar = "alwaysOff";
    };.add();
    0;
    new ""() {
        modal = GuiMLTextCtrl @ 0;
        position = "3 3";
        extent = "20 20";
        text = "<b><outline><shadowcolor:ffffffd0><color:000000d0>" @ getWord(%letterWords, %n);
    };
    %ctrl = new ""() {
        profile = GuiBitmapCtrl @ "ClosetDkBackgroundProfile";
        horizSizing = new GuiControl(MyShopItemDeets_TexturesContainer) {
        profile = "ETSNonModalProfile";
        position = "0 0";
        extent = %extX @ " " @ %extY;
    }; @ "right";
        vertSizing = "bottom";
        position = %posX @ " " @ %posY;
        extent = %extX @ " " @ %extY;
        systemDragDrop = 1;
        canHilite = 1;
        visible = 0;
    };
    new ""() {
        profile = GuiBitmapButtonCtrl @ "GuiDefaultProfile";
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
    %ctrl.add();
    geTextureTarget = %ctrl @ %n @ MyShopItemDeetsPanel;
    MyShopItemDeets_TexturesContainer;
    %posX = (%dx + %posX);
    %n = (1.0 + %n);
    0;
    new ""() {
        modal = GuiMLTextCtrl @ 0;
        position = "3 3";
        extent = "20 20";
        text = "<b><outline><shadowcolor:ffffffd0><color:000000d0>" @ getWord(%letterWords, %n);
    };
    %ctrl = new ""() {
        profile = GuiBitmapCtrl @ "ClosetDkBackgroundProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = %posX @ " " @ %posY;
        extent = %extX @ " " @ %extY;
        systemDragDrop = 1;
        canHilite = 1;
        visible = 0;
    };
    new ""() {
        profile = GuiBitmapButtonCtrl @ "GuiDefaultProfile";
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
    %ctrl.add();
    geTextureTarget = %ctrl @ %n @ MyShopItemDeetsPanel;
    MyShopItemDeets_TexturesContainer;
    %posX = (%dx + %posX);
    %n = (1.0 + %n);
    0;
    new ""() {
        modal = GuiMLTextCtrl @ 0;
        position = "3 3";
        extent = "20 20";
        text = "<b><outline><shadowcolor:ffffffd0><color:000000d0>" @ getWord(%letterWords, %n);
    };
    %ctrl = new ""() {
        profile = GuiBitmapCtrl @ "ClosetDkBackgroundProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = %posX @ " " @ %posY;
        extent = %extX @ " " @ %extY;
        systemDragDrop = 1;
        canHilite = 1;
        visible = 0;
    };
    new ""() {
        profile = GuiBitmapButtonCtrl @ "GuiDefaultProfile";
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
    %ctrl.add();
    geTextureTarget = %ctrl @ %n @ MyShopItemDeetsPanel;
    MyShopItemDeets_TexturesContainer;
    %posX = (%dx + %posX);
    %n = (1.0 + %n);
    0;
    new ""() {
        modal = GuiMLTextCtrl @ 0;
        position = "3 3";
        extent = "20 20";
        text = "<b><outline><shadowcolor:ffffffd0><color:000000d0>" @ getWord(%letterWords, %n);
    };
    %ctrl = new ""() {
        profile = GuiBitmapCtrl @ "ClosetDkBackgroundProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = %posX @ " " @ %posY;
        extent = %extX @ " " @ %extY;
        systemDragDrop = 1;
        canHilite = 1;
        visible = 0;
    };
    new ""() {
        profile = GuiBitmapButtonCtrl @ "GuiDefaultProfile";
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
    %ctrl.add();
    geTextureTarget = %ctrl @ %n @ MyShopItemDeetsPanel;
    MyShopItemDeets_TexturesContainer;
    %posX = (%dx + %posX);
    %n = (1.0 + %n);
    0;
    %ctrl = new ""() {
        horizSizing = GuiMLTextCtrl @ "right";
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
    %ctrl.add();
    %ctrl = new GuiVariableWidthButtonCtrl(MyShopRefreshTexturesCtrl) {
        profile = MyShopItemDeets_TexturesScroll @ "BracketButton19Profile";
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
    %ctrl.add();
    %ctrl = new GuiControl(MyShopItemSettings) {
        profile = MyShopItemDeetsPanel @ ETSNonModalProfile;
        position = "8 142";
        extent = "232 80";
        horizSizing = "right";
        vertSizing = "bottom";
        visible = 0;
    };
    %ctrl.add();
    0;
    %ctrl = new ""() {
        profile = GuiMLTextCtrl @ "ETSNonModalProfile";
        horizSizing = MyShopItemDeetsPanel @ "right";
        vertSizing = "bottom";
        position = "0 2";
        extent = "45 18";
        style = "plainOnWhite";
        stripGamelink = 1;
    };
    %ctrl.setTextWithStyle("vBux:");
    %ctrl.add();
    %ctrl = new GuiTextEditCtrl(MyShopVBuxField) {
        profile = MyShopItemSettings @ "Profile_MyShop_SettingsField";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "42 0";
        extent = "50 18";
        text = "";
        command = "$ThisControl.onKeyStroke();";
        validInputChars = 0123456789;
        tooltip = "How much this will cost";
    };
    %ctrl.add();
    0;
    %ctrl = new ""() {
        horizSizing = GuiMLTextCtrl @ "right";
        vertSizing = MyShopItemSettings @ "bottom";
        position = "94 2";
        extent = "45 18";
        style = "plainOnWhiteBlueLinks";
        visible = 0;
        stripGamelink = 1;
    };
    %ctrl.bindClassName("MyShopGenericMLText");
    %ctrl.setTextWithStyle("<a:gamelink:MYDESIGNS_ABOUT VBUX>-?</a>");
    $gMyShopAboutLinks = $gMyShopAboutLinks @ " " @ %ctrl;
    %ctrl.add();
    0;
    %ctrl = new ""() {
        profile = GuiMLTextCtrl @ "ETSNonModalProfile";
        horizSizing = MyShopItemSettings @ "right";
        vertSizing = "bottom";
        position = "0 22";
        extent = "45 18";
        style = "plainOnWhite";
    };
    %ctrl.setTextWithStyle("vPoints:");
    %ctrl.add();
    %ctrl = new GuiTextEditCtrl(MyShopVPointsField) {
        profile = MyShopItemSettings @ "Profile_MyShop_SettingsField";
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
    %ctrl.add();
    %ctrl.setActive(0);
    0;
    %ctrl = new ""() {
        horizSizing = GuiMLTextCtrl @ "right";
        vertSizing = MyShopItemSettings @ "bottom";
        position = "94 22";
        extent = "45 18";
        style = "plainOnWhiteBlueLinks";
        visible = 0;
        stripGamelink = 1;
    };
    %ctrl.bindClassName("MyShopGenericMLText");
    %ctrl.setTextWithStyle("<a:gamelink:MYDESIGNS_ABOUT VPOINTS>-?</a>");
    $gMyShopAboutLinks = $gMyShopAboutLinks @ " " @ %ctrl;
    %ctrl.add();
    %ctrl = new GuiCheckBoxCtrl(MyShopPriceForSaleOption) {
        profile = MyShopItemSettings @ "ETSCheckBoxProfile2";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "150 -3";
        extent = "50 18";
        text = "for sale";
        buttonType = "ToggleButton";
        tooltip = "Make this item for sale in your store";
    };
    %ctrl.setValue(1);
    %ctrl.add();
    0;
    %ctrl = new ""() {
        horizSizing = GuiMLTextCtrl @ "right";
        vertSizing = MyShopItemSettings @ "bottom";
        position = "209 0";
        extent = "10 18";
        style = "plainOnWhiteBlueLinks";
        visible = 0;
        stripGamelink = 1;
    };
    %ctrl.bindClassName("MyShopGenericMLText");
    %ctrl.setTextWithStyle("<a:gamelink:MYDESIGNS_ABOUT FOR_SALE>-?</a>");
    $gMyShopAboutLinks = $gMyShopAboutLinks @ " " @ %ctrl;
    %ctrl.add();
    %ctrl = new GuiCheckBoxCtrl(MyShopPriceFeaturedOption) {
        profile = MyShopItemSettings @ "ETSCheckBoxProfile2";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "150 12";
        extent = "85 18";
        text = "featured";
        buttonType = "ToggleButton";
        tooltip = "Make this your one item in the VHD store";
    };
    %ctrl.add();
    0;
    %ctrl = new ""() {
        horizSizing = GuiMLTextCtrl @ "right";
        vertSizing = MyShopItemSettings @ "bottom";
        position = "209 15";
        extent = "10 18";
        style = "plainOnWhiteBlueLinks";
        visible = 0;
        stripGamelink = 1;
    };
    %ctrl.bindClassName("MyShopGenericMLText");
    %ctrl.setTextWithStyle("<a:gamelink:MYDESIGNS_ABOUT FEATURED>-?</a>");
    $gMyShopAboutLinks = $gMyShopAboutLinks @ " " @ %ctrl;
    %ctrl.add();
    %ctrl = new GuiMLTextCtrl(MyShopPriceOutOfRangeText) {
        profile = MyShopItemSettings @ "ETSNonModalProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "10 44";
        extent = "214 14";
        style = "plainRedSmall";
        stripGamelink = 1;
    };
    %ctrl.setTextWithStyle("");
    %ctrl.add();
    %ctrl = new GuiVariableWidthButtonCtrl(MyShopSubmitButton) {
        profile = MyShopItemSettings @ "BracketButton17NonDefaultProfile";
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
    %ctrl.add();
    %ctrl.setActive(0);
    %ctrl = new GuiVariableWidthButtonCtrl(MyShopStartNewButton) {
        profile = MyShopItemSettings @ "BracketButton19Profile";
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
    %ctrl.add();
    new GuiBitmapCtrl(MyShopDragFilesImage) {
        profile = MyShopItemDeetsPanel @ "ETSNonModalProfile";
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
    new GuiBitmapCtrl(MyShopTextureInspectorBitmap) {
        profile = new ""() {
        profile = GuiBitmapCtrl @ ETSNonModalProfile;
        position = "1 1";
        extent = (2.0 - %w) @ " " @ (2.0 - %h);
        wrap = 1;
        bitmap = "platform/client/ui/greyChecks";
    }; @ "ETSNonModalProfile";
        position = (2.0 / (%h - %w)) @ " " @ 1;
        extent = (2.0 - %h) @ " " @ (2.0 - %h);
        horizSizing = "width";
        vertSizing = "height";
    };
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
    0;
    %ctrl = new ""() {
        profile = GuiVariableWidthButtonCtrl @ "BracketButton19Profile";
        horizSizing = new ""() {
        position = GuiBitmapButtonCtrl @ (2.0 - (16.0 - 461.0)) @ " " @ 2;
        extent = "16 16";
        command = "MyShopTextureInspector.close();";
        bitmap = "platform/client/buttons/gray_close";
    }; @ "right";
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
    0;
    %ctrl = new ""() {
        profile = GuiVariableWidthButtonCtrl @ "BracketButton19NonDefaultProfile";
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
    1.setStoreControlsVisible();
    %theTab.firstLoad = ClosetTabs @ 1;
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
        %list = "template".getSkusTag($player.getGender());
        SkuManager;
        %list = SkuManager @ "template".getSkusTag($player.getOtherGender());
        %list @ " ";
        onGotUGCItems(%type, %list);
    }
    if ((%type $= "ACCEPTED")) {
        %list = "mesh".getSkusType();
        SkuManager;
        %list = %list.filterSkusAuthor($Player::Name);
        SkuManager;
        onGotUGCItems(%type, %list);
    }
    error(getScopeName() @ " " @ "- not implemented" @ " " @ %type @ " " @ getTrace());
    if (0) {
        %buttonCtrl.infoText.setVisible(1);
        %buttonCtrl.infoText.setText("Fetching..");
        %list = %type[$gSampleUGC @ %type];
        ClosetThumbnailsMyShop;
        %n = (1.0 - getWordCount(%list));
        ClosetThumbnailsMyShop;
        if ((0.0 >= %n)) {
            %sku = getWord(%list, %n);
            %si = %sku.findBySku();
            SkuManager;
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
        "".setUnfilteredSkus();
        %si.infoText.setText("rejected list not implemented yet");
    }
    %list.setUnfilteredSkus();
    if ((ClosetThumbnailsMyShop @ " " @ %list $= "")) {
        %theTab = "MY DESIGNS".getTabWithName();
        ClosetTabs;
        if (%theTab.firstLoad) {
            %theTab.firstLoad = ClosetThumbnailsMyShop @ 0;
            ClosetThumbnailsMyShop;
            ClosetGui_MyShop_SetView("TEMPLATES");
        }
        %theTab.infoText.setText(%type[ClosetThumbnailsMyShop @ $MsgCat::MyShop TAB "EMPTYLIST" @ %type]);
    }
    ClosetGui_MyShop_SetCurrentSku(%type[$gUGCPrevSku @ %type]);
};
function ClosetGui_MyShop_GetSkuUGCStatus(%sku) {
    %si = %sku.findBySku();
    SkuManager;
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
    $gSkusMyShopLayer = $gSkusMyShopLayer.overlaySkus(%sku);
    SkuManager;
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
        "Update Item".setText();
    }
    if ((MyShopSubmitButton @ " " @ %itemStatus $= "PENDING")) {
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
        "Submit this template as a new item".setText();
    }
    if ((MyShopSubmitButton @ " " @ %itemStatus $= "INCOMING")) {
        error(getScopeName() @ " " @ "- not implemented yet:" @ " " @ %itemStatus @ " " @ getTrace());
        %readOnlyDesc = 1;
        %readOnlySettings = 1;
        %showSettings = 1;
        %showSubmit = 1;
        %showAboutLinks = 0;
        %showStartNew = 0;
        "Approve or Reject this item".setText();
    }
    %readOnlyDesc = 1;
    MyShopSubmitButton;
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
    !(%readOnlyDesc).setActive();
    %this.readOnly = %readOnlyDesc @ MyShopItemDeets_DescLong;
    MyShopItemDeets_DescShort;
    !(%readOnlyDesc).setActive();
    %this.readOnly = %readOnlySettings @ MyShopVBuxField;
    MyShopItemDeets_DescLong;
    !(%readOnlySettings).setActive();
    !(%readOnlySettings).setActive();
    %showSubmit.setVisible();
    !(%readOnlySettings).setActive();
    !(%readOnlySettings).setActive();
    %showSettings.setVisible();
    %showStartNew.setVisible();
    $gMyShopAboutLinks = trim($gMyShopAboutLinks);
    MyShopStartNewButton;
    %n = (1.0 - getWordCount($gMyShopAboutLinks));
    MyShopItemSettings;
    if ((0.0 >= %n)) {
        %ctrl = getWord($gMyShopAboutLinks, %n);
        MyShopPriceFeaturedOption;
        %ctrl.setVisible(%showAboutLinks);
        %n = (1.0 - %n);
        MyShopPriceForSaleOption;
    }
    %sku.setSkuBaseTextures();
    "".setText();
    "".setText();
    if ((MyShopItemDeets_DescShort @ " " @ %sku $= "")) {
        "no current item".setText();
        "".setText();
        $gMyShopCurrentSku = %sku;
        MyShopItemDeets_DescLong;
        "".zoomToSKU();
        return ClosetMainObjectView;
    }
    %si = %sku.findBySku();
    SkuManager;
    if (!(isObject(%si))) {
        "hmm, something went wrong. please check the bug forum.".setText();
        $gMyShopCurrentSku = "";
        MyShopItemDeets_DescLong;
        return;
    }
    %si.descShrt.setText();
    %si.descLong.setText();
    $gMyShopCurrentSku = %sku;
    MyShopItemDeets_DescLong;
    %sku.zoomToSKU();
    %si.price.setValue();
    MyShopVBuxField.onKeystroke();
};
function MyShopItemDeetsPanel::setSkuBaseTextures(%this, %sku) {
    %this.geTextureTarget.setVisible(0);
    %this.geTextureTarget.setVisible(0);
    %this.geTextureTarget.setVisible(0);
    %this.geTextureTarget.setVisible(0);
    %isValid = 1;
    0 @ 1 @ 2 @ 3;
    %si = %sku.findBySku();
    SkuManager;
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
                %this.geTextureTarget.setBitmap(%path);
            }
            %this.geTextureTarget.setBitmap("projects/vside/worlds/common/swatches/Gray50");
            %this.geTextureTarget.setVisible(1);
            %this.geTextureTarget.baseTexture = %n @ %n @ %n @ %baseTexture @ %n;
            %this.geTextureTarget.sku = %sku @ %n;
            %n = (1.0 + %n);
        }
    }
    %num = 0;
    (%num < %n);
    %stepX = (getWord(%this.geTextureTarget.getPosition(), 0) @ 1 - getWord(%this.geTextureTarget.getPosition(), 0));
    0;
    %padding = (getWord(%this.geTextureTarget.getExtent(), 0) - %stepX);
    0;
    (%padding - (%num * %stepX)).resize(getWord(%this.geTextureTarget.getExtent(), 1));
    %prevParent = MyShopDragFilesImage.getGroup();
    MyShopItemDeets_TexturesContainer @ 0;
    if (!(%isValid)) {
        0.setVisible();
        "-1000 -1000".reparent("", "");
        0.setVisible();
        0.setVisible();
    }
    if ((1.0 == %num)) {
        %si.hasTag("TEMPLATE").setVisible();
        "151 223".reparent("", "");
        %this.modulationColor = "200 50 180 180" @ MyShopDragFilesImage;
        ClosetMainObjectViewContainer;
        !(MyShopRefreshTexturesCtrl @ " " @ %si.getTxtrNames() $= %si.originalTxtrNames).setVisible();
        1.setVisible();
    }
    %si.hasTag("TEMPLATE").setVisible();
    "MY DESIGNS".getTabWithName().reparent("907 128", "", "");
    %si.modulationColor = "0 0 0 80" @ MyShopDragFilesImage;
    ClosetTabs;
    !(MyShopRefreshTexturesCtrl @ " " @ %si.getTxtrNames() $= %si.originalTxtrNames).setVisible();
    1.setVisible();
    %currParent = MyShopDragFilesImage.getGroup();
    MyShopItemDeets_TexturesScroll;
    if (%isValid) {
    }
    if ((%prevParent != %currParent)) {
    }
    if (%si.hasTag("TEMPLATE")) {
        5.FlashVisibility(150);
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
        %si.geTextureTarget.applyTexture(%text);
    }
    MessageBoxOK("Use the texture list", 0 @ MyShopItemDeetsPanel);
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
        1.setVisible();
        %otherExtension = (MyShopRefreshTexturesCtrl @ " " @ %extension $= ".png") ? ".jpg" : ".png";
        %otherFullPath = "user/textures/" @ %newTextureName @ %otherExtension;
        if (isFile(%otherFullPath)) {
            deleteFile(%otherFullPath);
        }
        removeFile(%newFullPath);
        addFile(%newFullPath);
        %this.setBitmap("");
        %this.setBitmap(%newFullPathNoExt);
        "".setBitmap();
        %newFullPathNoExt.setBitmap();
        reloadMeshTexture(%this.textureName, %newFullPathNoExt);
        %this.sku.findBySku().replaceTextureName(%newTextureName);
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
    $ClosetOutfitName[$ClosetSkusOutfit @ $ClosetOutfitName] = SkuManager @ $ClosetOutfitName[$ClosetSkusOutfit @ $ClosetOutfitName].overlaySkus($gSkusMyShopLayer);
};
function MyShopItemDeets_DescShort::onKeystroke(%this) {
    %si = $gMyShopCurrentSku.findBySku();
    SkuManager;
    if (!(isObject(%si))) {
        return;
    }
    %si.descShrt = %this.getValue();
};
function MyShopItemDeets_DescLong::onKeystroke(%this) {
    %si = $gMyShopCurrentSku.findBySku();
    SkuManager;
    if (!(isObject(%si))) {
        return;
    }
    %si.descLong = %this.getValue();
};
function MyShopVBuxField::onKeystroke(%this) {
    %si = $gMyShopCurrentSku.findBySku();
    SkuManager;
    if (!(isObject(%si))) {
        return;
    }
    %priceVBux = %this.getValue();
    %minPrice = %si.price;
    %maxPrice = (1.0 - 1000000.0);
    if ((%minPrice < %priceVBux)) {
        "<just:center>" @ %minPrice @ " " @ "vBux minimum for this item".setTextWithStyle();
        %si.submitPriceValid = 0 @ MyShopItemDeetsPanel;
        MyShopPriceOutOfRangeText;
    }
    if ((%maxPrice > %priceVBux)) {
        "<just:center>" @ %maxPrice @ " " @ "vBux maximum".setTextWithStyle();
        %si.submitPriceValid = 0 @ MyShopItemDeetsPanel;
        MyShopPriceOutOfRangeText;
    }
    "".setTextWithStyle();
    %si.submitPriceValid = 1 @ MyShopItemDeetsPanel;
    MyShopPriceOutOfRangeText;
    MyShopItemDeetsPanel.updateSubmitValidity();
    %itemMultiplier = 1;
    %multiplier = (%itemMultiplier * $gVPointsRatio);
    %priceVPoints = (%multiplier * %priceVBux);
    %priceVPoints.setText();
};
function MyShopItemDeetsPanel::updateSubmitValidity(%this) {
    %valid = 1;
    %valid = (%this.submitPriceValid & %valid);
    %valid = (%this.submitTexturesValid & %valid);
    %valid = (%this.submitDescriptionValid & %valid);
    if (%valid) {
        1.setActive();
        %this.modulationColor = "255 255 255 255" @ MyShopSubmitButton;
        MyShopSubmitButton;
    }
    0.setActive();
    %this.modulationColor = "255 255 255 64" @ MyShopSubmitButton;
    MyShopSubmitButton;
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
    %bitmap = %this.geTextureTarget.getBitmap();
    %num @ MyShopItemDeetsPanel;
    %bitmap.setBitmap();
    MyShopTextureInspector.open();
    MyShopTextureInspector.getGroup().pushToBack();
    %this.showingTextureNum = %num @ MyShopTextureInspector;
    MyShopTextureInspector;
};
function MyShopTextureInspector::open(%this) {
    %this.setVisible(1);
    %this.thumbnails.otherGenderText.setVisible(0);
};
function MyShopTextureInspector::close(%this) {
    %this.setVisible(0);
    %this.thumbnails.thumbnails.otherGenderText.setVisible(1);
};
function MyShopStartNewButton::onClick(%this) {
    if (!(ClosetGui_MyShop_GetSkuUGCStatus($gMyShopCurrentSku) $= "TEMPLATES")) {
        %templateSku = $gMyShopCurrentSku.findTemplateSku();
        SkuManager;
        if ((%templateSku $= "")) {
            error(getScopeName() @ " " @ "- can't find template for sku" @ " " @ $gMyShopCurrentSku @ " " @ getTrace());
            return;
        }
        ClosetGui_MyShop_SetView("Templates");
        ClosetGui_MyShop_SetCurrentSku(%templateSku);
        %templateSku.toggleSku();
    }
    %si = $gMyShopCurrentSku.findBySku();
    SkuManager;
    if (!(isObject(%si))) {
        error(getScopeName() @ " " @ "- invalid sku" @ " " @ $gMyShopCurrentSku @ " " @ getTrace());
        return ClosetGui;
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
        %si.geTextureTarget.geTextureTarget.applyTexture(%trgFile);
        %srcFile = "platform/client/ets/ugcReadMe.txt";
        %n @ MyShopItemDeetsPanel;
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
