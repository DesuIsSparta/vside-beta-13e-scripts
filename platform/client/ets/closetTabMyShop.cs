$gMyShopAboutLinks = "";
function ClosetTabs::fillMyShopTab(%this) {
    %theTab = %this.getTabWithName("MY DESIGNS");
    if (!(isObject(%theTab))) {
        return;
    }
    profile = GuiBitmapCtrl @ new ""() @ "ETSNonModalProfile";
    0;
    horizSizing = "right";
    vertSizing = "bottom";
    position = "26 26";
    extent = "571 37";
    minExtent = "1 1";
    sluggishness = -1;
    visible = 1;
    bitmap = "platform/client/ui/closet_tabs_bracket";
    %theTab.add();
    if (0) {
        profile = GuiTextCtrl @ new ""() @ "ClosetTitleProfile";
        0;
        horizSizing = "right";
        vertSizing = "bottom";
        position = "26 64";
        extent = "28 20";
        text = "My Designs:";
        %theTab.add();
    }
    %xPos = 34;
    %xGap = 6;
    %ypos = 84;
    %xSiz = 66;
    %viewType = "Accepted";
    profile = 0 @ new GuiVariableWidthButtonCtrl @ "geClosetMyShopViewButton_" @ %viewType() @ "BracketButton17NonDefaultProfile";
    horizSizing = "right";
    vertSizing = "bottom";
    position = %xPos @ " " @ %ypos;
    extent = %xSiz @ " " @ 17;
    command = "ClosetGui_MyShop_SetView(\"" @ %viewType @ "\");";
    text = %viewType;
    buttonType = "PushButton";
    canHilite = 0;
    %theTab.add();
    %xPos = ((%xGap + %xSiz) + %xPos);
    if (0) {
        %xSiz = 64;
        %viewType = "Pending";
        profile = 0 @ new GuiVariableWidthButtonCtrl @ "geClosetMyShopViewButton_" @ %viewType() @ "BracketButton17NonDefaultProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = %xPos @ " " @ 84;
        extent = %xSiz @ " " @ 17;
        command = "ClosetGui_MyShop_SetView(\"" @ %viewType @ "\");";
        text = %viewType;
        buttonType = "PushButton";
        canHilite = 0;
        %theTab.add();
        %xPos = ((%xGap + %xSiz) + %xPos);
        %xSiz = 76;
        %viewType = "Rejected";
        profile = 0 @ new GuiVariableWidthButtonCtrl @ "geClosetMyShopViewButton_" @ %viewType() @ "BracketButton17NonDefaultProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = %xPos @ " " @ 84;
        extent = %xSiz @ " " @ 17;
        command = "ClosetGui_MyShop_SetView(\"" @ %viewType @ "\");";
        text = %viewType;
        buttonType = "PushButton";
        canHilite = 0;
        %theTab.add();
        %xPos = ((%xGap + %xSiz) + %xPos);
    }
    %xSiz = 74;
    %viewType = "Templates";
    profile = 0 @ new GuiVariableWidthButtonCtrl @ "geClosetMyShopViewButton_" @ %viewType() @ "BracketButton17NonDefaultProfile";
    horizSizing = "right";
    vertSizing = "bottom";
    position = %xPos @ " " @ %ypos;
    extent = %xSiz @ " " @ 17;
    command = "ClosetGui_MyShop_SetView(\"" @ %viewType @ "\");";
    text = %viewType;
    buttonType = "PushButton";
    canHilite = 0;
    %theTab.add();
    if (0) {
        error("TODO - need to check for FashionPolice permission");
        %viewType = "Incoming";
        profile = 0 @ new GuiVariableWidthButtonCtrl @ "geClosetMyShopViewButton_" @ %viewType() @ "BracketButton17NonDefaultProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = %xPos @ " " @ 66;
        extent = %xSiz @ " " @ 17;
        command = "ClosetGui_MyShop_SetView(\"" @ %viewType @ "\");";
        text = %viewType;
        buttonType = "PushButton";
        canHilite = 0;
        %theTab.add();
        %xPos = ((%xGap + %xSiz) + %xPos);
    }
    profile = new GuiControl(MyShopItemsFrame) @ "GuiDefaultProfile";
    horizSizing = "right";
    vertSizing = "bottom";
    position = "22 120";
    extent = "467 368";
    minExtent = "1 1";
    sluggishness = -1;
    visible = 1;
    %itemsFrame = ;
    profile = GuiMLTextCtrl @ new ""() @ "ClosetLeftInfoProfile";
    0;
    horizSizing = "right";
    vertSizing = "bottom";
    position = "5 0";
    extent = "457 16";
    minExtent = "1 1";
    sluggishness = -1;
    visible = fase;
    text = "no matching items";
    maxLength = 255;
    %itemsInfoText = ;
    %itemsFrame.add(%itemsInfoText);
    profile = GuiTextCtrl @ new ""() @ "ClosetRightInfoProfile";
    0;
    horizSizing = "left";
    vertSizing = "bottom";
    position = "313 1";
    extent = "125 14";
    minExtent = "125 1";
    sluggishness = -1;
    visible = 1;
    text = "";
    maxLength = 255;
    %ctrl = ;
    %itemsFrame.add(%ctrl);
    rangeText = %ctrl @ %theTab;
    profile = GuiMLTextCtrl @ new ""() @ "ClosetRightInfoProfile";
    0;
    horizSizing = "left";
    vertSizing = "bottom";
    position = "338 354";
    extent = "125 14";
    modal = 0;
    %ctrl = ;
    %itemsFrame.add(%ctrl);
    otherGenderText = %ctrl @ %theTab;
    profile = GuiScrollCtrl @ new ""() @ "ETSScrollProfile";
    0;
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
    %ctrl = ;
    %ctrl.bindClassName("ClosetItemsScroll");
    itemsScroll = %ctrl @ %theTab;
    %itemsFrame.add(%ctrl);
    class = new GuiArray2Ctrl(ClosetThumbnailsMyShop) @ "ClosetThumbnails";
    profile = "FocusableDefaultProfile";
    childrenClassName = "GuiMouseEventCtrl";
    childrenExtent = "109 159";
    spacing = 2;
    numRowsOrCols = 4;
    inRows = 0;
    canHilite = 0;
    infoText = %itemsInfoText;
    tab = %theTab;
    scroll = %theTab @ itemsScroll;
    otherGenderText = %theTab @ otherGenderText;
    %thumbnails = ;
    itemsScroll.add(%thumbnails);
    thumbnails = %theTab @ itemsScroll;
    %theTab @ %thumbnails;
    %theTab.add(%itemsFrame);
    thumbnails = %thumbnails @ %theTab;
    thumbnails = %thumbnails @ %itemsFrame;
    profile = new ""() @ ETSNonModalProfile;
    GuiControl;
    horizSizing = 0 @ "right";
    vertSizing = "bottom";
    position = "689 309";
    extent = "245 179";
    profile = new GuiWindowCtrl(MyShopWhatYourWearingContainer) @ "DottedWindowProfile";
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
    horizSizing = new GuiMLTextCtrl(MyShopCopyToOutfitText) @ "left";
    vertSizing = "bottom";
    position = "0 2";
    extent = "233 18";
    style = "plainOnWhiteSmall";
    stripGamelink = 1;
    %itemDescFrame = ;
    %theTab.add(%itemDescFrame);
    "<just:right><a:gamelink:COPY_TO_OUTFIT>copy to outfit</a>".setTextWithStyle();
    profile = MyShopCopyToOutfitText @ new GuiWindowCtrl(MyShopItemDeetsPanel) @ "DottedWindowProfile";
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
    %theTab.add();
    profile = MyShopItemDeetsPanel @ new GuiTextEditCtrl(MyShopItemDeets_DescShort) @ "Profile_MyShop_TextField_Bold";
    horizSizing = "right";
    vertSizing = "bottom";
    position = "8 3";
    extent = "222 18";
    text = "Item Details";
    readOnly = 1;
    command = "$ThisControl.onKeyStroke();";
    .add();
    horizSizing = GuiMLTextCtrl @ new ""() @ "right";
    0;
    vertSizing = "bottom";
    position = "232 5";
    extent = "10 18";
    style = "plainOnWhiteBlueLinks";
    visible = 0;
    stripGamelink = 1;
    %ctrl = ;
    %ctrl.bindClassName("MyShopGenericMLText");
    %ctrl.setTextWithStyle("<a:gamelink:MYDESIGNS_ABOUT DESCSHORT>-?</a>");
    $gMyShopAboutLinks = $gMyShopAboutLinks @ " " @ %ctrl;
    %ctrl.add();
    profile = MyShopItemDeetsPanel @ new GuiTextEditCtrl(MyShopItemDeets_DescLong) @ "Profile_MyShop_TextField";
    MyShopItemDeetsPanel;
    horizSizing = "right";
    vertSizing = "bottom";
    position = "8 23";
    extent = "222 18";
    text = "Item Details Long";
    readOnly = 1;
    command = "$ThisControl.onKeyStroke();";
    .add();
    horizSizing = GuiMLTextCtrl @ new ""() @ "right";
    0;
    vertSizing = "bottom";
    position = "232 25";
    extent = "10 18";
    style = "plainOnWhiteBlueLinks";
    visible = 0;
    stripGamelink = 1;
    %ctrl = ;
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
    profile = MyShopItemDeetsPanel @ new GuiScrollCtrl(MyShopItemDeets_TexturesScroll) @ "DottedScrollProfile";
    horizSizing = "right";
    vertSizing = "bottom";
    position = "8 43";
    extent = 210 @ " " @ (18.0 + %extY);
    hScrollBar = "dynamic";
    vScrollBar = "alwaysOff";
    profile = new GuiControl(MyShopItemDeets_TexturesContainer) @ "ETSNonModalProfile";
    position = "0 0";
    extent = %extX @ " " @ %extY;
    .add();
    profile = GuiBitmapCtrl @ new ""() @ "ClosetDkBackgroundProfile";
    0;
    horizSizing = "right";
    vertSizing = "bottom";
    position = %posX @ " " @ %posY;
    extent = %extX @ " " @ %extY;
    systemDragDrop = 1;
    canHilite = 1;
    visible = 0;
    modal = GuiMLTextCtrl @ new ""() @ 0;
    position = "3 3";
    extent = "20 20";
    text = "<b><outline><shadowcolor:ffffffd0><color:000000d0>" @ getWord(%letterWords, %n);
    profile = GuiBitmapButtonCtrl @ new ""() @ "GuiDefaultProfile";
    horizSizing = "right";
    vertSizing = "bottom";
    position = "0 0";
    extent = %extX @ " " @ %extY;
    visible = 1;
    command = "MyShop_InspectTexture(" @ %n @ ");";
    canHilite = 0;
    bitmap = "platform/client/buttons/tgf/tgf_buttonframe_100x100";
    lazyLoad = 1;
    %ctrl = ;
    %ctrl.bindClassName("geTextureDropTarget");
    %ctrl.add();
    geTextureTarget = MyShopItemDeets_TexturesContainer @ %ctrl @ %n @ MyShopItemDeetsPanel;
    %posX = (%dx + %posX);
    %n = (1.0 + %n);
    profile = GuiBitmapCtrl @ new ""() @ "ClosetDkBackgroundProfile";
    0;
    horizSizing = "right";
    vertSizing = "bottom";
    position = %posX @ " " @ %posY;
    extent = %extX @ " " @ %extY;
    systemDragDrop = 1;
    canHilite = 1;
    visible = 0;
    modal = GuiMLTextCtrl @ new ""() @ 0;
    position = "3 3";
    extent = "20 20";
    text = "<b><outline><shadowcolor:ffffffd0><color:000000d0>" @ getWord(%letterWords, %n);
    profile = GuiBitmapButtonCtrl @ new ""() @ "GuiDefaultProfile";
    horizSizing = "right";
    vertSizing = "bottom";
    position = "0 0";
    extent = %extX @ " " @ %extY;
    visible = 1;
    command = "MyShop_InspectTexture(" @ %n @ ");";
    canHilite = 0;
    bitmap = "platform/client/buttons/tgf/tgf_buttonframe_100x100";
    lazyLoad = 1;
    %ctrl = ;
    %ctrl.bindClassName("geTextureDropTarget");
    %ctrl.add();
    geTextureTarget = MyShopItemDeets_TexturesContainer @ %ctrl @ %n @ MyShopItemDeetsPanel;
    %posX = (%dx + %posX);
    %n = (1.0 + %n);
    profile = GuiBitmapCtrl @ new ""() @ "ClosetDkBackgroundProfile";
    0;
    horizSizing = "right";
    vertSizing = "bottom";
    position = %posX @ " " @ %posY;
    extent = %extX @ " " @ %extY;
    systemDragDrop = 1;
    canHilite = 1;
    visible = 0;
    modal = GuiMLTextCtrl @ new ""() @ 0;
    position = "3 3";
    extent = "20 20";
    text = "<b><outline><shadowcolor:ffffffd0><color:000000d0>" @ getWord(%letterWords, %n);
    profile = GuiBitmapButtonCtrl @ new ""() @ "GuiDefaultProfile";
    horizSizing = "right";
    vertSizing = "bottom";
    position = "0 0";
    extent = %extX @ " " @ %extY;
    visible = 1;
    command = "MyShop_InspectTexture(" @ %n @ ");";
    canHilite = 0;
    bitmap = "platform/client/buttons/tgf/tgf_buttonframe_100x100";
    lazyLoad = 1;
    %ctrl = ;
    %ctrl.bindClassName("geTextureDropTarget");
    %ctrl.add();
    geTextureTarget = MyShopItemDeets_TexturesContainer @ %ctrl @ %n @ MyShopItemDeetsPanel;
    %posX = (%dx + %posX);
    %n = (1.0 + %n);
    profile = GuiBitmapCtrl @ new ""() @ "ClosetDkBackgroundProfile";
    0;
    horizSizing = "right";
    vertSizing = "bottom";
    position = %posX @ " " @ %posY;
    extent = %extX @ " " @ %extY;
    systemDragDrop = 1;
    canHilite = 1;
    visible = 0;
    modal = GuiMLTextCtrl @ new ""() @ 0;
    position = "3 3";
    extent = "20 20";
    text = "<b><outline><shadowcolor:ffffffd0><color:000000d0>" @ getWord(%letterWords, %n);
    profile = GuiBitmapButtonCtrl @ new ""() @ "GuiDefaultProfile";
    horizSizing = "right";
    vertSizing = "bottom";
    position = "0 0";
    extent = %extX @ " " @ %extY;
    visible = 1;
    command = "MyShop_InspectTexture(" @ %n @ ");";
    canHilite = 0;
    bitmap = "platform/client/buttons/tgf/tgf_buttonframe_100x100";
    lazyLoad = 1;
    %ctrl = ;
    %ctrl.bindClassName("geTextureDropTarget");
    %ctrl.add();
    geTextureTarget = MyShopItemDeets_TexturesContainer @ %ctrl @ %n @ MyShopItemDeetsPanel;
    %posX = (%dx + %posX);
    %n = (1.0 + %n);
    horizSizing = GuiMLTextCtrl @ new ""() @ "right";
    0;
    vertSizing = "bottom";
    position = "87 71";
    extent = "10 16";
    stripGamelink = 1;
    style = "plainOnWhiteBlueLinks";
    visible = 0;
    %ctrl = ;
    %ctrl.bindClassName("MyShopGenericMLText");
    %ctrl.setTextWithStyle("<a:gamelink:MYDESIGNS_ABOUT TEXTURES>-?</a>");
    $gMyShopAboutLinks = $gMyShopAboutLinks @ " " @ %ctrl;
    %ctrl.add();
    profile = MyShopItemDeets_TexturesScroll @ new GuiVariableWidthButtonCtrl(MyShopRefreshTexturesCtrl) @ "BracketButton19Profile";
    horizSizing = "right";
    vertSizing = "bottom";
    position = "90 200";
    extent = "119 19";
    command = "ClosetGUI_RefreshTextures();";
    text = "Reload Textures";
    buttonType = "PushButton";
    visible = 0;
    tooltip = "(Ctrl-R)";
    %ctrl = ;
    %ctrl.add();
    profile = new GuiControl(MyShopItemSettings) @ ETSNonModalProfile;
    MyShopItemDeetsPanel;
    position = "8 142";
    extent = "232 80";
    horizSizing = "right";
    vertSizing = "bottom";
    visible = 0;
    %ctrl = ;
    %ctrl.add();
    profile = GuiMLTextCtrl @ new ""() @ "ETSNonModalProfile";
    0;
    horizSizing = MyShopItemDeetsPanel @ "right";
    vertSizing = "bottom";
    position = "0 2";
    extent = "45 18";
    style = "plainOnWhite";
    stripGamelink = 1;
    %ctrl = ;
    %ctrl.setTextWithStyle("vBux:");
    %ctrl.add();
    profile = MyShopItemSettings @ new GuiTextEditCtrl(MyShopVBuxField) @ "Profile_MyShop_SettingsField";
    horizSizing = "right";
    vertSizing = "bottom";
    position = "42 0";
    extent = "50 18";
    text = "";
    command = "$ThisControl.onKeyStroke();";
    validInputChars = 0123456789;
    tooltip = "How much this will cost";
    %ctrl = ;
    %ctrl.add();
    horizSizing = GuiMLTextCtrl @ new ""() @ "right";
    0;
    vertSizing = MyShopItemSettings @ "bottom";
    position = "94 2";
    extent = "45 18";
    style = "plainOnWhiteBlueLinks";
    visible = 0;
    stripGamelink = 1;
    %ctrl = ;
    %ctrl.bindClassName("MyShopGenericMLText");
    %ctrl.setTextWithStyle("<a:gamelink:MYDESIGNS_ABOUT VBUX>-?</a>");
    $gMyShopAboutLinks = $gMyShopAboutLinks @ " " @ %ctrl;
    %ctrl.add();
    profile = GuiMLTextCtrl @ new ""() @ "ETSNonModalProfile";
    0;
    horizSizing = MyShopItemSettings @ "right";
    vertSizing = "bottom";
    position = "0 22";
    extent = "45 18";
    style = "plainOnWhite";
    %ctrl = ;
    %ctrl.setTextWithStyle("vPoints:");
    %ctrl.add();
    profile = MyShopItemSettings @ new GuiTextEditCtrl(MyShopVPointsField) @ "Profile_MyShop_SettingsField";
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
    %ctrl = ;
    %ctrl.add();
    %ctrl.setActive(0);
    horizSizing = GuiMLTextCtrl @ new ""() @ "right";
    0;
    vertSizing = MyShopItemSettings @ "bottom";
    position = "94 22";
    extent = "45 18";
    style = "plainOnWhiteBlueLinks";
    visible = 0;
    stripGamelink = 1;
    %ctrl = ;
    %ctrl.bindClassName("MyShopGenericMLText");
    %ctrl.setTextWithStyle("<a:gamelink:MYDESIGNS_ABOUT VPOINTS>-?</a>");
    $gMyShopAboutLinks = $gMyShopAboutLinks @ " " @ %ctrl;
    %ctrl.add();
    profile = MyShopItemSettings @ new GuiCheckBoxCtrl(MyShopPriceForSaleOption) @ "ETSCheckBoxProfile2";
    horizSizing = "right";
    vertSizing = "bottom";
    position = "150 -3";
    extent = "50 18";
    text = "for sale";
    buttonType = "ToggleButton";
    tooltip = "Make this item for sale in your store";
    %ctrl = ;
    %ctrl.setValue(1);
    %ctrl.add();
    horizSizing = GuiMLTextCtrl @ new ""() @ "right";
    0;
    vertSizing = MyShopItemSettings @ "bottom";
    position = "209 0";
    extent = "10 18";
    style = "plainOnWhiteBlueLinks";
    visible = 0;
    stripGamelink = 1;
    %ctrl = ;
    %ctrl.bindClassName("MyShopGenericMLText");
    %ctrl.setTextWithStyle("<a:gamelink:MYDESIGNS_ABOUT FOR_SALE>-?</a>");
    $gMyShopAboutLinks = $gMyShopAboutLinks @ " " @ %ctrl;
    %ctrl.add();
    profile = MyShopItemSettings @ new GuiCheckBoxCtrl(MyShopPriceFeaturedOption) @ "ETSCheckBoxProfile2";
    horizSizing = "right";
    vertSizing = "bottom";
    position = "150 12";
    extent = "85 18";
    text = "featured";
    buttonType = "ToggleButton";
    tooltip = "Make this your one item in the VHD store";
    %ctrl = ;
    %ctrl.add();
    horizSizing = GuiMLTextCtrl @ new ""() @ "right";
    0;
    vertSizing = MyShopItemSettings @ "bottom";
    position = "209 15";
    extent = "10 18";
    style = "plainOnWhiteBlueLinks";
    visible = 0;
    stripGamelink = 1;
    %ctrl = ;
    %ctrl.bindClassName("MyShopGenericMLText");
    %ctrl.setTextWithStyle("<a:gamelink:MYDESIGNS_ABOUT FEATURED>-?</a>");
    $gMyShopAboutLinks = $gMyShopAboutLinks @ " " @ %ctrl;
    %ctrl.add();
    profile = MyShopItemSettings @ new GuiMLTextCtrl(MyShopPriceOutOfRangeText) @ "ETSNonModalProfile";
    horizSizing = "right";
    vertSizing = "bottom";
    position = "10 44";
    extent = "214 14";
    style = "plainRedSmall";
    stripGamelink = 1;
    %ctrl = ;
    %ctrl.setTextWithStyle("");
    %ctrl.add();
    profile = MyShopItemSettings @ new GuiVariableWidthButtonCtrl(MyShopSubmitButton) @ "BracketButton17NonDefaultProfile";
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
    %ctrl = ;
    %ctrl.add();
    %ctrl.setActive(0);
    profile = MyShopItemSettings @ new GuiVariableWidthButtonCtrl(MyShopStartNewButton) @ "BracketButton19Profile";
    horizSizing = "right";
    vertSizing = "bottom";
    position = "8 200";
    extent = "76 19";
    command = "$ThisControl.onClick();";
    text = "Start New";
    buttonType = "PushButton";
    visible = 0;
    tooltip = "Start designing with a new copy of this template";
    %ctrl = ;
    %ctrl.add();
    profile = MyShopItemDeetsPanel @ new GuiBitmapCtrl(MyShopDragFilesImage) @ "ETSNonModalProfile";
    horizSizing = "left";
    vertSizing = "bottom";
    position = "907 128";
    extent = "43 174";
    bitmap = "platform/client/ui/closet_myshop_dragfiles";
    modulationColor = "255 255 255 80";
    visible = 0;
    %w = 461;
    %h = 423;
    profile = new GuiWindowCtrl(MyShopTextureInspector) @ "DottedWindowProfile";
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
    profile = new ""() @ ETSNonModalProfile;
    GuiBitmapCtrl;
    position = "1 1";
    extent = (2.0 - %w) @ " " @ (2.0 - %h);
    wrap = 1;
    bitmap = "platform/client/ui/greyChecks";
    profile = new GuiBitmapCtrl(MyShopTextureInspectorBitmap) @ "ETSNonModalProfile";
    position = (2.0 / (%h - %w)) @ " " @ 1;
    extent = (2.0 - %h) @ " " @ (2.0 - %h);
    horizSizing = "width";
    vertSizing = "height";
    position = GuiBitmapButtonCtrl @ new ""() @ (2.0 - (16.0 - 461.0)) @ " " @ 2;
    extent = "16 16";
    command = "MyShopTextureInspector.close();";
    bitmap = "platform/client/buttons/gray_close";
    %theTab.add();
    profile = GuiVariableWidthButtonCtrl @ new ""() @ "BracketButton19Profile";
    0;
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
    %ctrl = ;
    %theTab.add(%ctrl);
    doneButton = %ctrl @ %theTab;
    profile = GuiVariableWidthButtonCtrl @ new ""() @ "BracketButton19NonDefaultProfile";
    0;
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
    %ctrl = ;
    %theTab.add(%ctrl);
    cancelButton = %ctrl @ %theTab;
    1.setStoreControlsVisible();
    firstLoad = ClosetTabs @ 1 @ %theTab;
    ClosetGui_MyShop_SetView();
    tabMyShopInitialized = ACCEPTED @ 1 @ %this;
};
$gPrevMyShopTypeButton = "";
function ClosetGui_MyShop_SetView(%viewType) {
    if (isObject($gPrevMyShopTypeButton)) {
        $gPrevMyShopTypeButton.setProfile();
        modal = BracketButton17NonDefaultProfile @ 1 @ $gPrevMyShopTypeButton;
    }
    %buttonCtrl = "geClosetMyShopViewButton_" @ %viewType;
    %buttonCtrl.setProfile();
    modal = BracketButton17Profile @ 0 @ %buttonCtrl;
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
        infoText.setVisible(1);
        infoText.setText("Fetching..");
        %list = %type[$gSampleUGC @ %type];
        ClosetThumbnailsMyShop;
        %n = (1.0 - getWordCount(%list));
        ClosetThumbnailsMyShop;
        if ((0.0 >= %n)) {
            %sku = getWord(%list, %n);
            %si = %sku.findBySku();
            SkuManager;
            brand = "" @ %si;
            tags = %si @ findAndRemoveAllOccurrencesOfWord(tags, "new") @ %si;
            expireTime = "" @ %si;
            if ((%type $= "ACCEPTED")) {
            }
            if ((%type $= "PENDING")) {
                author = $Player::Name @ %si;
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
        infoText.setText("rejected list not implemented yet");
    }
    %list.setUnfilteredSkus();
    if ((ClosetThumbnailsMyShop SPC %list $= "")) {
        %theTab = "MY DESIGNS".getTabWithName();
        ClosetTabs;
        if (firstLoad) {
            firstLoad = %theTab @ 0 @ %theTab;
            ClosetThumbnailsMyShop;
            ClosetGui_MyShop_SetView("TEMPLATES");
        }
        infoText.setText(%type[ClosetThumbnailsMyShop @ $MsgCat::MyShop TAB "EMPTYLIST" @ %type]);
    }
    ClosetGui_MyShop_SetCurrentSku(%type[$gUGCPrevSku @ %type]);
};
function ClosetGui_MyShop_GetSkuUGCStatus(%sku) {
    %si = %sku.findBySku();
    SkuManager;
    if (!(%si SPC ugcStatus $= "")) {
        return ugcStatus;
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
    ugcStatus = (0.0 >= %n) @ %status @ %si;
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
    if (!(getCurrentTab() SPC name $= "MY DESIGNS")) {
        return ClosetTabs;
    }
    thumbnails.refilter();
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
    if ((MyShopSubmitButton SPC %itemStatus $= "PENDING")) {
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
    if ((MyShopSubmitButton SPC %itemStatus $= "INCOMING")) {
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
    border = %readOnlyDesc ? 0 : 1 @ Profile_MyShop_TextField;
    border = %readOnlyDesc ? 0 : 1 @ Profile_MyShop_TextField_Bold;
    border = %readOnlySettings ? 0 : 1 @ Profile_MyShop_SettingsField;
    readOnly = %readOnlyDesc @ MyShopItemDeets_DescShort;
    !(%readOnlyDesc).setActive();
    readOnly = MyShopItemDeets_DescShort @ %readOnlyDesc @ MyShopItemDeets_DescLong;
    !(%readOnlyDesc).setActive();
    readOnly = MyShopItemDeets_DescLong @ %readOnlySettings @ MyShopVBuxField;
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
    if ((MyShopItemDeets_DescShort SPC %sku $= "")) {
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
    descShrt.setText();
    descLong.setText();
    $gMyShopCurrentSku = %sku;
    %si;
    %sku.zoomToSKU();
    price.setValue();
    onKeystroke();
};
function MyShopItemDeetsPanel::setSkuBaseTextures(%this, %sku) {
    geTextureTarget.setVisible(0);
    geTextureTarget.setVisible(0);
    geTextureTarget.setVisible(0);
    geTextureTarget.setVisible(0);
    %isValid = 1;
    0 @ %this @ 1 @ %this @ 2 @ %this @ 3 @ %this;
    %si = %sku.findBySku();
    SkuManager;
    if (!(isObject(%si))) {
        %isValid = 0;
    }
    if (%isValid) {
        %baseTextures = %si.getTxtrNames();
        %num = getWordCount(%baseTextures);
        numTextures = %num @ %this;
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
                geTextureTarget.setBitmap(%path);
            }
            geTextureTarget.setBitmap("projects/vside/worlds/common/swatches/Gray50");
            geTextureTarget.setVisible(1);
            baseTexture = %n @ %this @ %n @ %this @ %n @ %this @ %baseTexture @ %n @ %this @ geTextureTarget;
            sku = %sku @ %n @ %this @ geTextureTarget;
            %n = (1.0 + %n);
        }
    }
    %num = 0;
    (%num < %n);
    %stepX = (getWord(geTextureTarget.getPosition(), 0) @ 1 @ %this - getWord(geTextureTarget.getPosition(), 0));
    0 @ %this;
    %padding = (getWord(geTextureTarget.getExtent(), 0) - %stepX);
    0 @ %this;
    (%padding - (%num * %stepX)).resize(getWord(geTextureTarget.getExtent(), 1));
    %prevParent = getGroup();
    MyShopDragFilesImage;
    if (!(%isValid)) {
        0.setVisible();
        "-1000 -1000".reparent("", "");
        0.setVisible();
        0.setVisible();
    }
    if ((1.0 == %num)) {
        %si.hasTag("TEMPLATE").setVisible();
        "151 223".reparent("", "");
        modulationColor = ClosetMainObjectViewContainer @ "200 50 180 180" @ MyShopDragFilesImage;
        MyShopDragFilesImage;
        !(%si $= originalTxtrNames).setVisible();
        1.setVisible();
    }
    %si.hasTag("TEMPLATE").setVisible();
    "MY DESIGNS".getTabWithName().reparent("907 128", "", "");
    modulationColor = ClosetTabs @ "0 0 0 80" @ MyShopDragFilesImage;
    MyShopDragFilesImage;
    !(%si $= originalTxtrNames).setVisible();
    1.setVisible();
    %currParent = getGroup();
    MyShopDragFilesImage;
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
    if ((MyShopItemDeetsPanel == numTextures)) {
        geTextureTarget.applyTexture(%text);
    }
    MessageBoxOK("Use the texture list", 1.0 @ 0 @ MyShopItemDeetsPanel);
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
        %this.applyTexture(%text, sku);
    }
    return 1;
};
function geTextureDropTarget::applyTexture(%this, %texturePath) {
    %newTextureName = baseTexture;
    %this;
    %extension = getExtension(%texturePath);
    %newFullPathNoExt = "user/textures/" @ %newTextureName;
    %newFullPath = %newFullPathNoExt @ %extension;
    incomingPath = %texturePath @ %this;
    if (($Platform $= "macos")) {
        %newFullPath = getPrefsDir() @ "/" @ %newFullPath;
    }
    %ok = fileCopy(%texturePath, %newFullPath);
    if (%ok) {
        1.setVisible();
        %otherExtension = (MyShopRefreshTexturesCtrl SPC %extension $= ".png") ? ".jpg" : ".png";
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
        reloadMeshTexture(textureName, %newFullPathNoExt);
        sku.findBySku().replaceTextureName(%newTextureName);
    }
    error(getScopeName() @ " " @ "- could not copy" @ " " @ %texturePath @ " " @ "to" @ " " @ %newFullPath @ " " @ getTrace());
};
function ClosetGUI_RefreshTextures() {
    %n = 0;
    if ((4.0 < %n)) {
        %obj = geTextureTarget;
        %n @ MyShopItemDeetsPanel;
        if (%obj.isVisible()) {
            %obj.applyTexture(incomingPath);
        }
        %n = (1.0 + %n);
        %obj;
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
    descShrt = %this.getValue() @ %si;
};
function MyShopItemDeets_DescLong::onKeystroke(%this) {
    %si = $gMyShopCurrentSku.findBySku();
    SkuManager;
    if (!(isObject(%si))) {
        return;
    }
    descLong = %this.getValue() @ %si;
};
function MyShopVBuxField::onKeystroke(%this) {
    %si = $gMyShopCurrentSku.findBySku();
    SkuManager;
    if (!(isObject(%si))) {
        return;
    }
    %priceVBux = %this.getValue();
    %minPrice = price;
    %si;
    %maxPrice = (1.0 - 1000000.0);
    if ((%minPrice < %priceVBux)) {
        MyShopPriceOutOfRangeText @ "<just:center>" @ %minPrice @ " " @ "vBux minimum for this item".setTextWithStyle();
        submitPriceValid = 0 @ MyShopItemDeetsPanel;
    }
    if ((%maxPrice > %priceVBux)) {
        MyShopPriceOutOfRangeText @ "<just:center>" @ %maxPrice @ " " @ "vBux maximum".setTextWithStyle();
        submitPriceValid = 0 @ MyShopItemDeetsPanel;
    }
    "".setTextWithStyle();
    submitPriceValid = MyShopPriceOutOfRangeText @ 1 @ MyShopItemDeetsPanel;
    updateSubmitValidity();
    %itemMultiplier = 1;
    MyShopItemDeetsPanel;
    %multiplier = (%itemMultiplier * $gVPointsRatio);
    %priceVPoints = (%multiplier * %priceVBux);
    %priceVPoints.setText();
};
function MyShopItemDeetsPanel::updateSubmitValidity(%this) {
    %valid = 1;
    %valid = (submitPriceValid & %valid);
    %this;
    %valid = (submitTexturesValid & %valid);
    %this;
    %valid = (submitDescriptionValid & %valid);
    %this;
    if (%valid) {
        1.setActive();
        modulationColor = MyShopSubmitButton @ "255 255 255 255" @ MyShopSubmitButton;
    }
    0.setActive();
    modulationColor = MyShopSubmitButton @ "255 255 255 64" @ MyShopSubmitButton;
};
function MyShopSubmitButton::onClick(%this) {
    %itemType = ClosetGui_MyShop_GetSkuUGCStatus($gMyShopCurrentSku);
    MessageBoxOkCancel(%itemType[$gMyShopSubmitTitle @ %itemType], %itemType[$gMyShopSubmitBody @ %itemType], "MessageBoxOK(\"not implemented\", \"\", \"\");", "");
};
function MyShop_InspectTexture(%num) {
    if (isVisible()) {
    }
    if ((MyShopTextureInspector SPC showingTextureNum $= %num)) {
        close();
        showingTextureNum = MyShopTextureInspector @ "" @ MyShopTextureInspector;
        MyShopTextureInspector;
    }
    %bitmap = geTextureTarget.getBitmap();
    %num @ MyShopItemDeetsPanel;
    %bitmap.setBitmap();
    open();
    getGroup().pushToBack();
    showingTextureNum = MyShopTextureInspector @ %num @ MyShopTextureInspector;
    MyShopTextureInspector;
};
function MyShopTextureInspector::open(%this) {
    %this.setVisible(1);
    otherGenderText.setVisible(0);
};
function MyShopTextureInspector::close(%this) {
    %this.setVisible(0);
    otherGenderText.setVisible(1);
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
    %trgdir = "user/textures/templates/" @ %si @ descShrt @ "/" @ getTimeStamp();
    %n = (%si - getWordCount(originalTxtrNames));
    1.0;
    if ((0.0 >= %n)) {
        %originalName = getWord(originalTxtrNames, %n);
        %si;
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
        textureName = %textureName @ %n @ MyShopItemDeetsPanel @ geTextureTarget;
        geTextureTarget.applyTexture(%trgFile);
        %srcFile = "platform/client/ets/ugcReadMe.txt";
        %n @ MyShopItemDeetsPanel;
        %trgFile = %trgdir @ "/readme.txt";
        fileCopy(%srcFile, %trgFile);
        %n = (1.0 - %n);
    }
    if (((0.0 >= %n) SPC $Platform $= "windows")) {
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
