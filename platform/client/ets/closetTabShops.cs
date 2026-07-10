function ClosetTabs::refreshStoreTab(%this) {
    StoreCategoryPopup.clear();
    if (!($gCurrentStoreName[$gStoreStockLoaded @ $gCurrentStoreName])) {
        StoreCategoryPopup.add("Loading ...");
        return;
    }
    %allCategories = "All Items" @ "\t" @ "All Garments" @ "\t" @ "All Accessories" @ "\t" @ "Tops" @ "\t" @ "Bottoms" @ "\t" @ "Hair" @ "\t" @ "Face" @ "\t" @ "Skin" @ "\t" @ "Shoes" @ "\t" @ "Ear" @ "\t" @ "Neck" @ "\t" @ "Waist" @ "\t" @ "Hands" @ "\t" @ "Bags" @ "\t" @ "Glasses" @ "\t" @ "Props" @ "\t" @ "Misc" @ "\t" @ "BodyMod";
    %storeDrwrs = SkuManager.getSkuDrwrs(SkuManager.filterSkusGender(Inventory::getCurrentStoreSkus(), $player.getGender()));
    if ((%storeDrwrs $= "")) {
        StoreItemsFrame.update();
    }
    %n = 0;
    if ((getFieldCount(%allCategories) < %n)) {
        %cat = getField(%allCategories, %n);
        %catDrwrs = ThumbCategories.get(strlwr(%cat));
        %m = 0;
        if ((getWordCount(%catDrwrs) < %m)) {
            %found = findWord(%storeDrwrs, getWord(%catDrwrs, %m));
            if ((0.0 >= %found)) {
                StoreCategoryPopup.add(%cat);
            }
            %m = (1.0 + %m);
        }
        %n = (1.0 + %n);
        (getWordCount(%catDrwrs) < %m);
    }
    loadStorePosition();
};
function ClosetTabs::fillStoreTab(%this) {
    %theTab = %this.getTabWithName("SHOPS");
    if (!(isObject(%theTab))) {
        return;
    }
    %theTab.add(new GuiBitmapCtrl("") {
        profile = 0 @ "GuiDefaultProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "26 26";
        extent = "571 37";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        bitmap = "platform/client/ui/closet_tabs_bracket";
    };);
    %tabWidth = getWord(%theTab.getExtent(), 0);
    %tabHeight = getWord(%theTab.getExtent(), 1);
    %theTab.add(new GuiBitmapCtrl(StoreSpecificBackground) {
        profile = "GuiDefaultProfile";
        horizSizing = "center";
        vertSizing = "center";
        position = (256.0 - (2.0 / %tabWidth)) @ " " @ (256.0 - (2.0 / %tabHeight));
        extent = "512 512";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 0;
        bitmap = "";
    };);
    %storename = new GuiTextCtrl("") {
        profile = 0 @ "ClosetTitleProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "0 0";
        extent = "237 17";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        text = "";
        maxLength = 255;
    };
    %storeDesc = new GuiMLTextCtrl("") {
        profile = 0 @ "ClosetLeftInfoProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "0 23";
        extent = "237 17";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        allowColorChars = 0;
        maxChars = -1;
        text = "";
    };
    %nameDescFrame = new GuiControl(StoreNameDescFrame) {
        profile = "GuiDefaultProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "26 66";
        extent = "251 59";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        nameCtrl = %storename;
        descCtrl = %storeDesc;
    };
    %nameDescFrame.add(%storename);
    %nameDescFrame.add(%storeDesc);
    %theTab.add(%nameDescFrame);
    %categoryLabel = new GuiTextCtrl(StoreCategoryLabel) {
        profile = "ClosetTitleProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "181 64";
        extent = "75 20";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        text = "Item";
        maxLength = 255;
    };
    %theTab.add(%categoryLabel);
    %categoryPopup = new GuiPopUp2MenuCtrl(StoreCategoryPopup) {
        profile = "ClosetPopupProfile";
        scrollProfile = "DottedScrollProfile";
        winProfile = "ClosetPopupWindowProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "180 84";
        extent = "150 17";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        maxLength = 255;
        maxPopupHeight = 200;
        allowReverse = 0;
    };
    %theTab.add(%categoryPopup);
    %ctrl = new GuiControl(StoreExpirationLegend) {
        position = "26 465";
        extent = "260 20";
        visible = 0;
        lastStore = "";
    };
    new GuiTextCtrl("") {
        profile = new GuiBitmapCtrl("") {
        profile = "ETSNonModalProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "0 0";
        extent = "16 16";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        bitmap = "platform/client/ui/expiring_icon_small";
        modulationColor = "255 255 255 255";
    }; @ "ClosetLeftInfoProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "16 -1";
        extent = "200 18";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        text = " = Expires after a certain amount of time.";
        maxLength = 255;
    };
    %theTab.add(%ctrl);
    %itemsFrame = new GuiControl(StoreItemsFrame) {
        profile = "GuiDefaultProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "22 120";
        extent = "467 350";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
    };
    %itemsInfoText = new GuiTextCtrl("") {
        profile = 0 @ "ClosetLeftInfoProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "1 30";
        extent = "77 16";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 0;
        text = "no matching items";
        maxLength = 255;
    };
    %itemsFrame.add(%itemsInfoText);
    %itemsRangeText = new GuiTextCtrl("") {
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
    %itemsFrame.add(%itemsRangeText);
    %theTab.rangeText = %itemsRangeText;
    %itemsScroll = new GuiScrollCtrl("") {
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
    %itemsScroll.bindClassName("ClosetItemsScroll");
    %theTab.itemsScroll = %itemsScroll;
    %thumbnails = new GuiArray2Ctrl(ClosetThumbnailsShop) {
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
        scroll = %itemsScroll;
    };
    %itemsScroll.add(%thumbnails);
    %itemsScroll.thumbnails = %thumbnails;
    %itemsFrame.add(%itemsScroll);
    %itemsFrame.thumbnails = %thumbnails;
    %theTab.add(%itemsFrame);
    %theTab.thumbnails = %thumbnails;
    %theTab.add(new GuiVariableWidthButtonCtrl(StoreDirectoryLink) {
        profile = "BracketButton13Profile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "26 124";
        extent = "126 13";
        minExtent = "1 1";
        visible = 1;
        text = "Go to Shops Directory";
        buttonType = "PushButton";
        drawText = 1;
        command = "transferFromShopToDestinationsDirectory();";
    };);
    %theTab.add(new GuiMLTextCtrl(StoreDirectoryLinkBigText) {
        profile = "ClosetLeftInfoProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "26 124";
        extent = "350 17";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 0;
        allowColorChars = 0;
        maxChars = -1;
        text = ;
    };);
    %theTab.add(new GuiBitmapButtonCtrl(StoreDirectoryLinkBig) {
        profile = "GuiDefaultProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "100 200";
        extent = "300 50";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 0;
        bitmap = "platform/client/buttons/closet_shopsDirButton";
        command = "transferFromShopToDestinationsDirectory();";
    };);
    %theTab.add(new GuiBitmapButtonCtrl(StoreBanner) {
        profile = "GuiButtonProfile";
        horizSizing = "left";
        vertSizing = "bottom";
        position = "8 6";
        extent = "448 57";
        minExtent = "2 2";
        sluggishness = -1;
        visible = 1;
        command = "StoreBanner.doAction();";
        text = "";
        groupNum = -1;
        buttonType = "PushButton";
        bitmap = "";
        drawText = 0;
    };, new GuiBitmapCtrl(StoreBannerBrackets) {
        profile = "GuiDefaultProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "0 0";
        extent = "464 69";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 0;
        bitmap = "platform/client/ui/banner_bracket";
    };, new GuiControl(StoreBannerFrame) {
        profile = "GuiDefaultProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "26 483";
        extent = "464 69";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
    };);
    %theTab.add(new GuiMLTextCtrl(StoreBalanceText) {
        profile = "ClosetLargeLinkProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "9 5";
        extent = "228 42";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        maxChars = -1;
        text = "";
    };, new GuiBitmapCtrl("") {
        profile = 0 @ "GuiDefaultProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "689 26";
        extent = "245 45";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        bitmap = "platform/client/ui/balance_bracket";
    };);
    %itemDescFrame = new GuiWindowCtrl(StoreItemDescFrame) {
        profile = "DottedWindowProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "689 84";
        extent = "245 110";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        resizeWidth = 0;
        resizeHeight = 0;
        canMove = 0;
        canClose = 0;
        canMinimize = 0;
        canMaximize = 0;
        closeCommand = "";
    };
    new GuiMLTextCtrl(StoreLongDescText) {
        profile = new GuiMLTextCtrl(StoreShortDescText) {
        profile = "ClosetLeftInfoProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "3 0";
        extent = "242 25";
        lineSpacing = -3;
    }; @ "ClosetLeftInfoProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "3 22";
        extent = "173 16";
        lineSpacing = -3;
    };
    %theTab.add(%itemDescFrame);
    %theTab.add(new GuiWindowCtrl(StoreItemDescHiliteFrame) {
        profile = "StoreHiliteFrameProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "679 80";
        extent = "263 124";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 0;
        resizeWidth = 0;
        resizeHeight = 0;
        canMove = 0;
        canClose = 0;
        canMinimize = 0;
        canMaximize = 0;
        closeCommand = "";
    };);
    %theTab.add(new GuiWindowCtrl(StoreFloatingHiliteFrame) {
        profile = "StoreHiliteFrameProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "0 0";
        extent = "119 143";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 0;
        resizeWidth = 0;
        resizeHeight = 0;
        canMove = 0;
        canClose = 0;
        canMinimize = 0;
        canMaximize = 0;
        closeCommand = "";
    };);
    %shoppingBag = new GuiWindowCtrl(StoreShoppingBag) {
        profile = "DottedWindowProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "689 207";
        extent = "245 281";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        resizeWidth = 0;
        resizeHeight = 0;
        canMove = 0;
        canClose = 0;
        canMinimize = 0;
        canMaximize = 0;
        closeCommand = "";
    };
    new GuiWindowCtrl("") {
        profile = new GuiVariableWidthButtonCtrl(StoreTotalButton) {
        profile = new GuiMLTextCtrl(StoreBuxTotalText) {
        profile = new GuiMLTextCtrl(StorePointsTotalText) {
        profile = new GuiMLTextCtrl("") {
        profile = new GuiMLTextCtrl(StoreNoItemsText) {
        profile = new GuiTextCtrl("") {
        profile = new GuiBitmapCtrl("") {
        profile = "GuiDefaultProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "7 4";
        extent = "18 13";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        bitmap = "platform/client/ui/cart";
    }; @ "ClosetTitleProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "28 1";
        extent = "108 20";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        text = "Shopping Cart";
        maxLength = 255;
    }; @ "ClosetLeftInfoProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "5 28";
        extent = "221 16";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        text = "No items in shopping cart.";
        maxLength = 255;
    }; @ "ShoppingBagItemProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "7 261";
        extent = "60 16";
        minExtent = "60 1";
        sluggishness = -1;
        visible = 1;
        text = "Buy All For";
        maxLength = 255;
    }; @ "ClosetPointsProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "90 261";
        extent = "60 16";
        minExtent = "60 1";
        sluggishness = -1;
        visible = 1;
        maxLength = 255;
    }; @ "ClosetBuxProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "162 261";
        extent = "50 16";
        minExtent = "50 1";
        sluggishness = -1;
        visible = 1;
        maxLength = 255;
    }; @ "HiddenBracketButton15Profile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "85 261";
        extent = "132 16";
        minExtent = "1 1";
        visible = 1;
        command = "ClosetGui.doCheckout();";
        text = "";
        buttonType = "PushButton";
        drawText = 0;
    }; @ "DottedWindowProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "6 258";
        extent = "233 1";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        resizeWidth = 0;
        resizeHeight = 0;
        canMove = 0;
        canClose = 0;
        canMinimize = 0;
        canMaximize = 0;
        closeCommand = "";
    };
    %theTab.add(new GuiVariableWidthButtonCtrl(StoreAddItemsButton) {
        profile = "BracketButton15NonDefaultProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "689 491";
        extent = "213 19";
        minExtent = "1 1";
        visible = 1;
        command = "StoreShoppingList.addItemsYoureWearing();";
        text = "Add Items You're Wearing To Cart";
        buttonType = "PushButton";
        drawText = 1;
    };);
    StoreAddItemsButton.setActive(0);
    %wi = AnimCtrl::newAnimCtrl("129 213", "18 18");
    %wi.setDelay(60);
    %wi.addFrame("platform/client/ui/wait0.png");
    %wi.addFrame("platform/client/ui/wait1.png");
    %wi.addFrame("platform/client/ui/wait2.png");
    %wi.addFrame("platform/client/ui/wait3.png");
    %wi.addFrame("platform/client/ui/wait4.png");
    %wi.addFrame("platform/client/ui/wait5.png");
    %wi.addFrame("platform/client/ui/wait6.png");
    %wi.addFrame("platform/client/ui/wait7.png");
    StoreShoppingBag.add(%wi);
    waitIcon = %wi @ StoreShoppingBag;
    %wi.setVisible(0);
    %shoppingScroll = new GuiScrollCtrl("") {
        profile = 0 @ "ETSScrollProfile";
        position = "3 22";
        extent = "239 232";
        minExtent = "1 1";
        horizSizing = "right";
        vertSizing = "bottom";
        visible = 1;
        hScrollBar = "alwaysOff";
        vScrollBar = "dynamic";
        constantThumbHeight = 1;
        scrollMultiplier = 2.5;
    };
    %shoppingList = new GuiArray2Ctrl(StoreShoppingList) {
        profile = "GuiDefaultProfile";
        childrenClassName = "GuiMouseEventCtrl";
        childrenExtent = "233 36";
        spacing = 2;
        numRowsOrCols = 1;
        inRows = 0;
        canHilite = 0;
        scroll = %shoppingScroll;
    };
    %shoppingScroll.add(%shoppingList);
    %shoppingBag.add(%shoppingScroll);
    %theTab.add(%shoppingBag);
    %doneButton = new GuiVariableWidthButtonCtrl("") {
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
    %cancelButton = new GuiVariableWidthButtonCtrl("") {
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
    %theTab.add(%doneButton);
    %theTab.doneButton = %doneButton;
    %theTab.add(%cancelButton);
    %theTab.cancelButton = %cancelButton;
    ClosetTabs.setStoreControlsVisible(1);
    %this.tabShopsInitialized = 1;
};
function ClosetTabs::setStoreControlsVisible(%this, %flag) {
    StoreNameDescFrame.setVisible(%flag);
    StoreCategoryLabel.setVisible(%flag);
    StoreCategoryPopup.setVisible(%flag);
    StoreItemsFrame.setVisible(%flag);
    StoreBannerFrame.setVisible(%flag);
    StoreAddItemsButton.setVisible(%flag);
    StoreShortDescText.setVisible(%flag);
    StoreLongDescText.setVisible(%flag);
    StoreShoppingBag.setVisible(%flag);
    StoreItemDescFrame.setVisible(%flag);
    if (%flag) {
    }
    StoreDirectoryLink.setVisible(!(isInFUE()));
    if (!(%flag)) {
        StoreItemDescHiliteFrame.setVisible(%flag);
        StoreFloatingHiliteFrame.setVisible(%flag);
    }
    if (isInFUE()) {
        closetGuiFUE_vPoints_vBux_Image.setVisible(!(%flag));
    }
};
function ClosetTabs::setLeaveStoreControlsVisible(%this, %flag) {
    StoreDirectoryLinkBigText.setVisible(%flag);
    StoreDirectoryLinkBig.setVisible(%flag);
};
function StoreShoppingList::onCreatedChild(%this, %child) {
    %background = new GuiControl("") {
        profile = 0 @ "ClosetLtBackgroundProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "0 0";
        extent = "233 36";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
    };
    %hilite = new GuiControl("") {
        profile = 0 @ "ClosetHiliteProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "0 0";
        extent = "233 36";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 0;
    };
    %itemDesc = new GuiVariableWidthButtonCtrl("") {
        profile = 0 @ "StoreItemButtonProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "1 2";
        extent = "208 15";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        text = "";
        groupNum = -1;
        buttonType = "PushButton";
        command = "";
        drawText = 1;
    };
    %closeBox = new GuiBitmapButtonCtrl("") {
        profile = 0 @ "GuiDefaultProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "213 4";
        extent = "10 10";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        command = "StoreShoppingList.removeSku(" @ %child.getId() @ ".sku);";
        text = "";
        groupNum = -1;
        buttonType = "PushButton";
        bitmap = "platform/client/buttons/closet_close";
        drawText = 0;
    };
    %expiringIcon = new GuiBitmapCtrl("") {
        profile = 0 @ "ETSNonModalProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "208 16";
        extent = "20 20";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        bitmap = "";
        modulationColor = "255 255 255 100";
    };
    %pointsLink = new GuiMLTextCtrl("") {
        profile = 0 @ "ClosetPointsProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "6 18";
        extent = "60 18";
        minExtent = "60 1";
        sluggishness = -1;
        visible = 1;
        allowColorChars = 0;
        maxChars = -1;
        text = "";
    };
    %buxLink = new GuiMLTextCtrl("") {
        profile = 0 @ "ClosetBuxProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "78 18";
        extent = "50 18";
        minExtent = "50 1";
        sluggishness = -1;
        visible = 1;
        allowColorChars = 0;
        maxChars = -1;
        text = "";
    };
    %totalButton = new GuiVariableWidthButtonCtrl("") {
        profile = 0 @ "HiddenBracketButton15Profile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "1 18";
        extent = "132 16";
        minExtent = "1 1";
        visible = 1;
        command = "ClosetGui.purchaseSkus(" @ %child @ ".sku);";
        text = "";
        buttonType = "PushButton";
        drawText = 0;
    };
    %child.add(%background);
    %child.add(%hilite);
    %child.add(%itemDesc);
    %child.add(%expiringIcon);
    %child.add(%buxLink);
    %child.add(%pointsLink);
    %child.add(%totalButton);
    %child.add(%closeBox);
    %child.background = %background;
    %child.hiliteCtrl = %hilite;
    %child.desc = %itemDesc;
    %child.expiringIcon = %expiringIcon;
    %child.pointsLink = %pointsLink;
    %child.buxLink = %buxLink;
    %child.points = "-";
    %child.bux = "-";
    %child.sku = 0;
    %child.shoppingList = %this;
    if (!(getWord(%child.getNamespaceList(), 0) $= "StoreShoppingListItem")) {
        %child.bindClassName("StoreShoppingListItem");
    }
};
function StoreShoppingList::addSku(%this, %sku) {
    if ((0.0 >= findWord($Player::inventory, %sku))) {
        return;
    }
    %count = %this.getCount();
    %idx = 0;
    if ((%count < %idx)) {
        if ((%this.getObject(%idx).sku $= %sku)) {
            return;
        }
        %idx = (1.0 + %idx);
    }
    %child = %this.addChild();
    (%count < %idx);
    %child.sku = %sku;
    %si = SkuManager.findBySku(%sku);
    %child.desc.setText(%si.descShrt);
    %child.desc.command = "ClosetGui.toggleSku(" @ %sku @ ");" @ "ClosetThumbnailsShop.scroll.scrollToSku(" @ %sku @ ");";
    %pointsIcon = "<bitmap:platform/client/ui/vpoints_9>";
    %buxIcon = "<bitmap:platform/client/ui/vbux_9>";
    %child.points = Inventory::getVPointsPriceForSku(%child.sku);
    %child.bux = Inventory::getVBuxPriceForSku(%child.sku);
    %child.pointsLink.setText(%pointsIcon @ " " @ %child.points);
    %child.buxLink.setText(%buxIcon @ " " @ %child.bux);
    if (!(%si.expireTime $= "")) {
        %child.expiringIcon.setBitmap("platform/client/ui/expiring_icon");
        %child.expiringIcon.setVisible(1);
    }
    %child.expiringIcon.setBitmap("");
    %child.expiringIcon.setVisible(0);
    %count = StoreItemsFrame.getCount(%child.thumbnails);
    %i = 0;
    if ((%count < %i)) {
        %obj = StoreItemsFrame.getObject(%child.thumbnails, %i);
        if ((%sku == %obj.sku)) {
            %obj.toggleCartButton.setBitmap("platform/client/buttons/removeFromCart");
        }
        %i = (1.0 + %i);
    }
    %this.update();
    %this.hiliteCell(0, (1.0 - %this.getCount()));
};
function StoreShoppingList::addSkus(%this, %skulist) {
    %skulist = trim(%skulist);
    %count = getWordCount(%skulist);
    %i = 0;
    if ((%count < %i)) {
        %this.addSku(getWord(%skulist, %i));
        %i = (1.0 + %i);
    }
};
function StoreShoppingList::removeSku(%this, %sku) {
    %count = %this.getCount();
    %i = 0;
    if ((%count < %i)) {
        %obj = %this.getObject(%i);
        if ((%sku == %obj.sku)) {
            %obj.delete();
        }
        %i = (1.0 + %i);
    }
    %count = StoreItemsFrame.getCount(%obj.thumbnails);
    (%count < %i);
    %i = 0;
    if ((%count < %i)) {
        %obj = StoreItemsFrame.getObject(%obj.thumbnails, %i);
        if ((%sku == %obj.sku)) {
            %obj.toggleCartButton.setBitmap("platform/client/buttons/add2cart");
        }
        %i = (1.0 + %i);
    }
    if ((-(1.0) != findWord($StoreSkusLayer, %sku))) {
        StoreAddItemsButton.setActive(1);
    }
    %this.update();
};
function StoreShoppingList::removeSkus(%this, %skulist) {
    %skulist = trim(%skulist);
    %count = getWordCount(%skulist);
    %i = 0;
    if ((%count < %i)) {
        %this.removeSku(getWord(%skulist, %i));
        %i = (1.0 + %i);
    }
};
function StoreShoppingList::clear(%this) {
    %this.removeSkus(%this.getSkus());
};
function StoreShoppingList::containsSku(%this, %sku) {
    %count = %this.getCount();
    %i = 0;
    if ((%count < %i)) {
        if ((%sku == %this.getObject(%i).sku)) {
            return 1;
        }
        %i = (1.0 + %i);
    }
    return 0;
};
function StoreShoppingList::getSkus(%this) {
    %skus = "";
    %count = %this.getCount();
    %i = 0;
    if ((%count < %i)) {
        %skus = %skus @ " " @ %this.getObject(%i).sku;
        %i = (1.0 + %i);
    }
    return trim(%skus);
};
function StoreShoppingList::addItemsYoureWearing(%this) {
    %count = getWordCount($StoreSkusLayer);
    %i = 0;
    if ((%count < %i)) {
        %this.addSku(getWord($StoreSkusLayer, %i));
        %i = (1.0 + %i);
    }
    StoreAddItemsButton.setActive(0);
};
function StoreShoppingList::clear(%this) {
    Parent::clear(%this);
    %this.update();
};
function StoreShoppingList::update(%this) {
    StoreNoItemsText.setVisible((0.0 == %this.getCount()));
    %this.reseatChildren();
    %this.sumPrices();
    %count = %this.getCount();
    %i = 0;
    if ((%count < %i)) {
        %child = %this.getObject(%i);
        if ((0.0 == (2 % %i))) {
            // unhandled opcode 8756 at 0x00002231
        }
        %child.background.setProfile();
        %i = (1.0 + %i);
        ClosetDkBackgroundProfile;
    }
};
function StoreShoppingList::sumPrices(%this) {
    %pointsSum = 0;
    %buxSum = 0;
    %count = %this.getCount();
    %idx = 0;
    if ((%count < %idx)) {
        %child = %this.getObject(%idx);
        if (!(%child.points $= "-")) {
            %pointsSum = (%child.points + %pointsSum);
        }
        if (!(%child.bux $= "-")) {
            %buxSum = (%child.bux + %buxSum);
        }
        %idx = (1.0 + %idx);
    }
    %pointsIcon = "<bitmap:platform/client/ui/vpoints_9>";
    (%count < %idx);
    %buxIcon = "<bitmap:platform/client/ui/vbux_9>";
    StorePointsTotalText.setText(%pointsIcon @ " " @ %pointsSum);
    StoreBuxTotalText.setText(%buxIcon @ " " @ %buxSum);
    %this.pointsTotal = %pointsSum;
    %this.buxTotal = %buxSum;
};
function StoreShoppingList::scrollToItem(%this, %item) {
    %idx = %this.getObjectIndex(%item);
    if ((0.0 >= %idx)) {
        %scroll = %this.scroll;
        %cellHeight = (%this.spacing + getWord(%this.childrenExtent, 1));
        %numRowsVisible = (%cellHeight / getWord(%scroll.getExtent(), 1));
        %ypos = (getWord(%this.getPosition(), 1) - 1.0);
        %closestRow = (%cellHeight / %ypos);
        %targetRow = getWord(%this.hilitedCell, 1);
        if ((%closestRow < %targetRow)) {
            %scroll.scrollTo(0, (%targetRow * %cellHeight));
        }
        if (((1.0 - (%numRowsVisible + %closestRow)) >= %targetRow)) {
            %scroll.scrollTo(0, ((%this.Parent.spacing * 2.0) + ((1.0 + (%numRowsVisible - %targetRow)) * %cellHeight)));
        }
    }
};
function StoreShoppingListItem::onMouseEnterBounds(%this) {
    %idx = %this.shoppingList.getObjectIndex(%this);
    %this.shoppingList.hiliteCell(0, %idx);
};
function StoreShoppingListItem::onMouseLeaveBounds(%this) {
    %this.onUnhilite();
};
function StoreShoppingListItem::onHilite(%this) {
    if (0) {
    }
    if (isObject(StoreLongDescText)) {
        StoreShortDescText.setDesc(ClosetTabs.getShortSkuDesc(%this.sku));
        StoreLongDescText.setDesc(ClosetTabs.getLongSkuDesc(%this.sku));
        ClosetTabs.updateAuthorWidget(%this.sku);
    }
    StoreShoppingList.scrollToItem(%this);
    %this.hiliteCtrl.setVisible(1);
};
function StoreShoppingListItem::onUnhilite(%this) {
    if (0) {
    }
    if (isObject(StoreLongDescText)) {
        StoreShortDescText.showBaseDesc();
        StoreLongDescText.showBaseDesc();
        ClosetTabs.updateAuthorWidget("");
    }
    if (isObject(StoreItemDescHiliteFrame)) {
        StoreItemDescHiliteFrame.setVisible(0);
    }
    if (isObject(StoreFloatingHiliteFrame)) {
        StoreFloatingHiliteFrame.setVisible(0);
    }
    %this.hiliteCtrl.setVisible(0);
};
function StoreBalanceText::update(%this) {
    %pointsIcon = "<bitmap:platform/client/ui/vpoints_14>";
    %buxIcon = "<bitmap:platform/client/ui/vbux_14>";
    %pointsInfoLink = "<spush><linkcolor:159fe7><a:gamelink " @ $Net::HelpURL_VPoints @ ">>> How to earn vPoints</a><spop>";
    %getBuxLink = "<spush><linkcolor:13b93c><a:gamelink " @ $Net::AddFundsURL @ ">>> Get more vBux</a><spop>";
    %this.setText("You Have   " @ "<spush><font:Arial Bold:16><color:159fe7>" @ %pointsIcon @ " " @ commaify($Player::VPoints) @ "<spop>" @ "   " @ "<spush><font:Arial Bold:16><color:13b93c>" @ %buxIcon @ " " @ commaify($Player::VBux) @ "<spop>" @ "<br>" @ "<font:Arial Bold:13>" @ %pointsInfoLink @ "   " @ %getBuxLink);
};
function StoreBalanceText::onURL(%this, %url) {
    if ((getWord(%url, 0) $= "gamelink")) {
        %url = getWords(%url, 1);
    }
    Parent::onURL(%this, %url);
};
function StoreItemsFrame::update(%this) {
    if (!($gCurrentStoreName[$gStoreStockLoaded @ $gCurrentStoreName])) {
        %this.thumbnails.infoText.setText("Loading store inventory...");
    }
    if ((Inventory::getCurrentStoreSkus() $= "")) {
        %this.thumbnails.infoText.setText("Nothing in stock!");
    }
    %this.thumbnails.infoText.setText("No matching items.");
    %this.thumbnails.setDrawers(ThumbCategories.get(strlwr(%this.category)));
};
function StoreCategoryPopup::onSelect(%this, %unused, %entries) {
    %this.category = %entries @ StoreItemsFrame;
    StoreItemsFrame.update();
    StoreShortDescText.schedule(0, "setBaseDesc", "");
    StoreLongDescText.schedule(0, "setBaseDesc", "");
    ClosetThumbnailsShop.getParent().scrollToTop();
};
function StoreLongDescText::setBaseDesc(%this, %desc) {
    %this.baseText = %desc;
    %this.setText(%desc);
    if ((%desc $= "")) {
        StoreItemDescHiliteFrame.setVisible(0);
        StoreFloatingHiliteFrame.setVisible(0);
    }
};
function StoreLongDescText::setDesc(%this, %desc) {
    %this.setText(%desc);
};
function StoreLongDescText::showBaseDesc(%this) {
    %this.setText(%this.baseText);
};
function StoreShortDescText::setBaseDesc(%this, %desc) {
    StoreLongDescText::setBaseDesc(%this, %desc);
};
function StoreShortDescText::setDesc(%this, %desc) {
    StoreLongDescText::setDesc(%this, %desc);
};
function StoreShortDescText::showBaseDesc(%this) {
    StoreLongDescText::showBaseDesc(%this, %desc);
};
function getCurrentStoreID() {
    %storename = stripChars($gCurrentStoreName, 0123456789);
    if ((%storename $= "edocsecret")) {
        %storename = "edoc";
    }
    if ((%storename $= "")) {
        return $gCurrentStoreName;
    }
    return %storename;
};
function StoreBanner::doAction(%this) {
    %storename = getCurrentStoreID();
    if (!(%storename $= "")) {
        gotoWebPage($Net::PartnerURL @ "/" @ %storename, 0);
    }
};
function updateAccountBalanceDisplays() {
    if (isObject(StoreBalanceText)) {
        StoreBalanceText.update();
    }
    if (isObject(AccountBalanceHud)) {
        AccountBalanceHud.update();
    }
};
function clientCmdUpdateAccountBalances(%newPoints, %newBux) {
    if (!(%newBux $= "")) {
        BalanceUpdateSpecialEffects("vBux", $Player::VBux, %newBux, 1);
        $Player::VBux = mFloor(%newBux);
    }
    if (!(%newPoints $= "")) {
        BalanceUpdateSpecialEffects("vPoints", $Player::VPoints, %newPoints, 1);
        $Player::VPoints = mFloor(%newPoints);
    }
    updateAccountBalanceDisplays();
};
function clientCmdRefreshVPoints() {
    getBalancesAndScores();
};
$Player::VPoints[$gBalanceUpdateSpecialEffect_Delay @ "vBux"] = 150;
$Player::VPoints[$gBalanceUpdateSpecialEffect_Delay @ "vBux"][$gBalanceUpdateSpecialEffect_Delay @ "vPoints"] = 300;
$Player::VPoints[$gBalanceUpdateSpecialEffect_Delay @ "vBux"][$gBalanceUpdateSpecialEffect_Delay @ "vPoints"][$gBalanceUpdateSpecialEffect_Threshhold1 @ "vBux"] = 1;
$Player::VPoints[$gBalanceUpdateSpecialEffect_Delay @ "vBux"][$gBalanceUpdateSpecialEffect_Delay @ "vPoints"][$gBalanceUpdateSpecialEffect_Threshhold1 @ "vBux"][$gBalanceUpdateSpecialEffect_Threshhold1 @ "vPoints"] = 100;
$Player::VPoints[$gBalanceUpdateSpecialEffect_Delay @ "vBux"][$gBalanceUpdateSpecialEffect_Delay @ "vPoints"][$gBalanceUpdateSpecialEffect_Threshhold1 @ "vBux"][$gBalanceUpdateSpecialEffect_Threshhold1 @ "vPoints"][$gBalanceUpdateSpecialEffect_Threshhold2 @ "vBux"] = 1;
$Player::VPoints[$gBalanceUpdateSpecialEffect_Delay @ "vBux"][$gBalanceUpdateSpecialEffect_Delay @ "vPoints"][$gBalanceUpdateSpecialEffect_Threshhold1 @ "vBux"][$gBalanceUpdateSpecialEffect_Threshhold1 @ "vPoints"][$gBalanceUpdateSpecialEffect_Threshhold2 @ "vBux"][$gBalanceUpdateSpecialEffect_Threshhold2 @ "vPoints"] = 1000;
$Player::VPoints[$gBalanceUpdateSpecialEffect_Delay @ "vBux"][$gBalanceUpdateSpecialEffect_Delay @ "vPoints"][$gBalanceUpdateSpecialEffect_Threshhold1 @ "vBux"][$gBalanceUpdateSpecialEffect_Threshhold1 @ "vPoints"][$gBalanceUpdateSpecialEffect_Threshhold2 @ "vBux"][$gBalanceUpdateSpecialEffect_Threshhold2 @ "vPoints"][$gBalanceUpdateSpecialEffect_Sound1 @ "vBux"] = "AudioIm_CaChing";
$Player::VPoints[$gBalanceUpdateSpecialEffect_Delay @ "vBux"][$gBalanceUpdateSpecialEffect_Delay @ "vPoints"][$gBalanceUpdateSpecialEffect_Threshhold1 @ "vBux"][$gBalanceUpdateSpecialEffect_Threshhold1 @ "vPoints"][$gBalanceUpdateSpecialEffect_Threshhold2 @ "vBux"][$gBalanceUpdateSpecialEffect_Threshhold2 @ "vPoints"][$gBalanceUpdateSpecialEffect_Sound1 @ "vBux"][$gBalanceUpdateSpecialEffect_Sound1 @ "vPoints"] = "AudioIm_vPoints1";
$Player::VPoints[$gBalanceUpdateSpecialEffect_Delay @ "vBux"][$gBalanceUpdateSpecialEffect_Delay @ "vPoints"][$gBalanceUpdateSpecialEffect_Threshhold1 @ "vBux"][$gBalanceUpdateSpecialEffect_Threshhold1 @ "vPoints"][$gBalanceUpdateSpecialEffect_Threshhold2 @ "vBux"][$gBalanceUpdateSpecialEffect_Threshhold2 @ "vPoints"][$gBalanceUpdateSpecialEffect_Sound1 @ "vBux"][$gBalanceUpdateSpecialEffect_Sound1 @ "vPoints"][$gBalanceUpdateSpecialEffect_Sound2 @ "vBux"] = "AudioIm_CaChing";
$Player::VPoints[$gBalanceUpdateSpecialEffect_Delay @ "vBux"][$gBalanceUpdateSpecialEffect_Delay @ "vPoints"][$gBalanceUpdateSpecialEffect_Threshhold1 @ "vBux"][$gBalanceUpdateSpecialEffect_Threshhold1 @ "vPoints"][$gBalanceUpdateSpecialEffect_Threshhold2 @ "vBux"][$gBalanceUpdateSpecialEffect_Threshhold2 @ "vPoints"][$gBalanceUpdateSpecialEffect_Sound1 @ "vBux"][$gBalanceUpdateSpecialEffect_Sound1 @ "vPoints"][$gBalanceUpdateSpecialEffect_Sound2 @ "vBux"][$gBalanceUpdateSpecialEffect_Sound2 @ "vPoints"] = "AudioIm_vPoints2";
$Player::VPoints[$gBalanceUpdateSpecialEffect_Delay @ "vBux"][$gBalanceUpdateSpecialEffect_Delay @ "vPoints"][$gBalanceUpdateSpecialEffect_Threshhold1 @ "vBux"][$gBalanceUpdateSpecialEffect_Threshhold1 @ "vPoints"][$gBalanceUpdateSpecialEffect_Threshhold2 @ "vBux"][$gBalanceUpdateSpecialEffect_Threshhold2 @ "vPoints"][$gBalanceUpdateSpecialEffect_Sound1 @ "vBux"][$gBalanceUpdateSpecialEffect_Sound1 @ "vPoints"][$gBalanceUpdateSpecialEffect_Sound2 @ "vBux"][$gBalanceUpdateSpecialEffect_Sound2 @ "vPoints"][$gBalanceUpdateSpecialEffect_GuiControl1 @ "vBux"] = "AccountBalanceVBuxText";
$Player::VPoints[$gBalanceUpdateSpecialEffect_Delay @ "vBux"][$gBalanceUpdateSpecialEffect_Delay @ "vPoints"][$gBalanceUpdateSpecialEffect_Threshhold1 @ "vBux"][$gBalanceUpdateSpecialEffect_Threshhold1 @ "vPoints"][$gBalanceUpdateSpecialEffect_Threshhold2 @ "vBux"][$gBalanceUpdateSpecialEffect_Threshhold2 @ "vPoints"][$gBalanceUpdateSpecialEffect_Sound1 @ "vBux"][$gBalanceUpdateSpecialEffect_Sound1 @ "vPoints"][$gBalanceUpdateSpecialEffect_Sound2 @ "vBux"][$gBalanceUpdateSpecialEffect_Sound2 @ "vPoints"][$gBalanceUpdateSpecialEffect_GuiControl1 @ "vBux"][$gBalanceUpdateSpecialEffect_GuiControl1 @ "vPoints"] = "AccountBalanceVPointsText";
$Player::VPoints[$gBalanceUpdateSpecialEffect_Delay @ "vBux"][$gBalanceUpdateSpecialEffect_Delay @ "vPoints"][$gBalanceUpdateSpecialEffect_Threshhold1 @ "vBux"][$gBalanceUpdateSpecialEffect_Threshhold1 @ "vPoints"][$gBalanceUpdateSpecialEffect_Threshhold2 @ "vBux"][$gBalanceUpdateSpecialEffect_Threshhold2 @ "vPoints"][$gBalanceUpdateSpecialEffect_Sound1 @ "vBux"][$gBalanceUpdateSpecialEffect_Sound1 @ "vPoints"][$gBalanceUpdateSpecialEffect_Sound2 @ "vBux"][$gBalanceUpdateSpecialEffect_Sound2 @ "vPoints"][$gBalanceUpdateSpecialEffect_GuiControl1 @ "vBux"][$gBalanceUpdateSpecialEffect_GuiControl1 @ "vPoints"][$gBalanceUpdateSpecialEffect_GuiControl2 @ "vBux"] = "";
$Player::VPoints[$gBalanceUpdateSpecialEffect_Delay @ "vBux"][$gBalanceUpdateSpecialEffect_Delay @ "vPoints"][$gBalanceUpdateSpecialEffect_Threshhold1 @ "vBux"][$gBalanceUpdateSpecialEffect_Threshhold1 @ "vPoints"][$gBalanceUpdateSpecialEffect_Threshhold2 @ "vBux"][$gBalanceUpdateSpecialEffect_Threshhold2 @ "vPoints"][$gBalanceUpdateSpecialEffect_Sound1 @ "vBux"][$gBalanceUpdateSpecialEffect_Sound1 @ "vPoints"][$gBalanceUpdateSpecialEffect_Sound2 @ "vBux"][$gBalanceUpdateSpecialEffect_Sound2 @ "vPoints"][$gBalanceUpdateSpecialEffect_GuiControl1 @ "vBux"][$gBalanceUpdateSpecialEffect_GuiControl1 @ "vPoints"][$gBalanceUpdateSpecialEffect_GuiControl2 @ "vBux"][$gBalanceUpdateSpecialEffect_GuiControl2 @ "vPoints"] = "AccountBalanceHud";
$Player::VPoints[$gBalanceUpdateSpecialEffect_Delay @ "vBux"][$gBalanceUpdateSpecialEffect_Delay @ "vPoints"][$gBalanceUpdateSpecialEffect_Threshhold1 @ "vBux"][$gBalanceUpdateSpecialEffect_Threshhold1 @ "vPoints"][$gBalanceUpdateSpecialEffect_Threshhold2 @ "vBux"][$gBalanceUpdateSpecialEffect_Threshhold2 @ "vPoints"][$gBalanceUpdateSpecialEffect_Sound1 @ "vBux"][$gBalanceUpdateSpecialEffect_Sound1 @ "vPoints"][$gBalanceUpdateSpecialEffect_Sound2 @ "vBux"][$gBalanceUpdateSpecialEffect_Sound2 @ "vPoints"][$gBalanceUpdateSpecialEffect_GuiControl1 @ "vBux"][$gBalanceUpdateSpecialEffect_GuiControl1 @ "vPoints"][$gBalanceUpdateSpecialEffect_GuiControl2 @ "vBux"][$gBalanceUpdateSpecialEffect_GuiControl2 @ "vPoints"][$gBalanceUpdateSpecialEffect_PulseCount1 @ "vBux"] = 1;
$Player::VPoints[$gBalanceUpdateSpecialEffect_Delay @ "vBux"][$gBalanceUpdateSpecialEffect_Delay @ "vPoints"][$gBalanceUpdateSpecialEffect_Threshhold1 @ "vBux"][$gBalanceUpdateSpecialEffect_Threshhold1 @ "vPoints"][$gBalanceUpdateSpecialEffect_Threshhold2 @ "vBux"][$gBalanceUpdateSpecialEffect_Threshhold2 @ "vPoints"][$gBalanceUpdateSpecialEffect_Sound1 @ "vBux"][$gBalanceUpdateSpecialEffect_Sound1 @ "vPoints"][$gBalanceUpdateSpecialEffect_Sound2 @ "vBux"][$gBalanceUpdateSpecialEffect_Sound2 @ "vPoints"][$gBalanceUpdateSpecialEffect_GuiControl1 @ "vBux"][$gBalanceUpdateSpecialEffect_GuiControl1 @ "vPoints"][$gBalanceUpdateSpecialEffect_GuiControl2 @ "vBux"][$gBalanceUpdateSpecialEffect_GuiControl2 @ "vPoints"][$gBalanceUpdateSpecialEffect_PulseCount1 @ "vBux"][$gBalanceUpdateSpecialEffect_PulseCount1 @ "vPoints"] = 1;
$Player::VPoints[$gBalanceUpdateSpecialEffect_Delay @ "vBux"][$gBalanceUpdateSpecialEffect_Delay @ "vPoints"][$gBalanceUpdateSpecialEffect_Threshhold1 @ "vBux"][$gBalanceUpdateSpecialEffect_Threshhold1 @ "vPoints"][$gBalanceUpdateSpecialEffect_Threshhold2 @ "vBux"][$gBalanceUpdateSpecialEffect_Threshhold2 @ "vPoints"][$gBalanceUpdateSpecialEffect_Sound1 @ "vBux"][$gBalanceUpdateSpecialEffect_Sound1 @ "vPoints"][$gBalanceUpdateSpecialEffect_Sound2 @ "vBux"][$gBalanceUpdateSpecialEffect_Sound2 @ "vPoints"][$gBalanceUpdateSpecialEffect_GuiControl1 @ "vBux"][$gBalanceUpdateSpecialEffect_GuiControl1 @ "vPoints"][$gBalanceUpdateSpecialEffect_GuiControl2 @ "vBux"][$gBalanceUpdateSpecialEffect_GuiControl2 @ "vPoints"][$gBalanceUpdateSpecialEffect_PulseCount1 @ "vBux"][$gBalanceUpdateSpecialEffect_PulseCount1 @ "vPoints"][$gBalanceUpdateSpecialEffect_PulseCount2 @ "vBux"] = 4;
$Player::VPoints[$gBalanceUpdateSpecialEffect_Delay @ "vBux"][$gBalanceUpdateSpecialEffect_Delay @ "vPoints"][$gBalanceUpdateSpecialEffect_Threshhold1 @ "vBux"][$gBalanceUpdateSpecialEffect_Threshhold1 @ "vPoints"][$gBalanceUpdateSpecialEffect_Threshhold2 @ "vBux"][$gBalanceUpdateSpecialEffect_Threshhold2 @ "vPoints"][$gBalanceUpdateSpecialEffect_Sound1 @ "vBux"][$gBalanceUpdateSpecialEffect_Sound1 @ "vPoints"][$gBalanceUpdateSpecialEffect_Sound2 @ "vBux"][$gBalanceUpdateSpecialEffect_Sound2 @ "vPoints"][$gBalanceUpdateSpecialEffect_GuiControl1 @ "vBux"][$gBalanceUpdateSpecialEffect_GuiControl1 @ "vPoints"][$gBalanceUpdateSpecialEffect_GuiControl2 @ "vBux"][$gBalanceUpdateSpecialEffect_GuiControl2 @ "vPoints"][$gBalanceUpdateSpecialEffect_PulseCount1 @ "vBux"][$gBalanceUpdateSpecialEffect_PulseCount1 @ "vPoints"][$gBalanceUpdateSpecialEffect_PulseCount2 @ "vBux"][$gBalanceUpdateSpecialEffect_PulseCount2 @ "vPoints"] = 4;
function BalanceUpdateSpecialEffects(%whichBalance, %oldVal, %newVal, %notify) {
    %delta = (%oldVal - %newVal);
    if (%notify) {
        schedule(3000, 0, "floatBalanceChange", %whichBalance, %delta, $player);
    }
    %threshhold1 = %whichBalance[$gBalanceUpdateSpecialEffect_Threshhold1 @ %whichBalance];
    if ((%threshhold1 < %delta)) {
        return;
    }
    %delay = %whichBalance[$gBalanceUpdateSpecialEffect_Delay @ %whichBalance];
    %threshhold2 = %whichBalance[$gBalanceUpdateSpecialEffect_Threshhold2 @ %whichBalance];
    %sound1 = %whichBalance[$gBalanceUpdateSpecialEffect_Sound1 @ %whichBalance];
    %sound2 = %whichBalance[$gBalanceUpdateSpecialEffect_Sound2 @ %whichBalance];
    %guiControl1 = %whichBalance[$gBalanceUpdateSpecialEffect_GuiControl1 @ %whichBalance];
    %guiControl2 = %whichBalance[$gBalanceUpdateSpecialEffect_GuiControl2 @ %whichBalance];
    %pulseCount1 = %whichBalance[$gBalanceUpdateSpecialEffect_PulseCount1 @ %whichBalance];
    %pulseCount2 = %whichBalance[$gBalanceUpdateSpecialEffect_PulseCount2 @ %whichBalance];
    if ((%threshhold2 < %delta)) {
        %sound = %sound1;
        %pulseCount = %pulseCount1;
    }
    %sound = %sound2;
    %pulseCount = %pulseCount2;
    alxPlay(%sound);
    %guiControl1.schedule(%delay, "blinkSet", "bounce", 150, 100, "0 -1 0 0", 5);
    if (isObject(%guiControl2)) {
        %guiControl2.startPulse(%pulseCount);
    }
};
function floatBalanceChange(%whichBalance, %change, %player) {
    %threshhold = 1;
    if ((%threshhold < %change)) {
        return;
    }
    if (!(isObject(%player))) {
        error(getScopeName() @ " " @ "- can't find player!" @ " " @ getTrace());
        return;
    }
    if (!(isObject(%player.hudCtrl))) {
        error(getScopeName() @ " " @ "- no hudCtrl to attach to!" @ " " @ getTrace());
        return;
    }
    %isVPoints = (%whichBalance $= "vPoints");
    %alot = %isVPoints ? 1000 : 100;
    %amountNorm = mClampF((%alot / %change), 0, 1);
    %amountNorm = (((%amountNorm - 1.0) * (%amountNorm - 1.0)) - 1.0);
    %isALot = (0.95 > %amountNorm);
    %speed = (3.0 + (2.0 * (%amountNorm - 1.0)));
    %fontTag = %isALot ? "<font:arial:18>" : "<font:arial:14>";
    %currencyText = %isVPoints ? "vPoints" : "vBux";
    %text = %fontTag @ "<b><outline>+" @ " " @ %change @ " " @ %currencyText;
    %maxAge = (70.0 + (70.0 * %amountNorm));
    %baseColor = %isVPoints ? "55eeff" : "22dd44";
    %baseAlpha = (0.6 + (0.4 * %amountNorm));
    %player.floatText(%text, %maxAge, %speed, %baseColor, %baseAlpha);
};
function Player::floatText(%this, %text, %maxAge, %speed, %baseColor, %baseAlpha) {
    %this.hudCtrl.updatePosition();
    %text = "<just:center>" @ %text;
    %width = 600;
    %ctrl = new GuiMLTextCtrl("") {
        extent = 0 @ %width @ " " @ 18;
        position = ((2.0 / %width) - getWord(%this.hudCtrl.position, 0)) @ " " @ (100.0 + getWord(%this.hudCtrl.position, 1));
        age = 0;
        maxAge = %maxAge;
        speed = %speed;
        BaseColor = %baseColor;
        baseText = %text;
        baseAlpha = %baseAlpha;
    };
    ThePointsFloaterHud.add(%ctrl);
    ThePointsFloaterHud.doTick();
};
function Player::floatTextSimple(%this, %text, %style) {
    if (!(isDefined("%style"))) {
        %style = "";
    }
    if ((%style $= "")) {
        %style = "default";
    }
    if ((%style[$gFloatingTextStyles TAB "font" @ %style] $= "")) {
        error(getScopeName() @ " " @ "- unknown style:" @ " " @ %style @ " " @ %text @ " " @ getTrace());
        %style = "default";
    }
    %outline = %style[$gFloatingTextStyles TAB "outline" @ %style] ? "<b><outline>" : "";
    %text = %style["<font:" @ $gFloatingTextStyles TAB "font" @ %style] @ ">" @ %text;
    %text = %outline @ %text;
    %text = %style[$gFloatingTextStyles TAB "prepend" @ %style] @ %text;
    %text = %style[%text @ $gFloatingTextStyles TAB "append" @ %style];
    %baseColor = %style[$gFloatingTextStyles TAB "color" @ %style];
    %baseAlpha = %style[$gFloatingTextStyles TAB "baseAlpha" @ %style];
    %maxAge = %style[$gFloatingTextStyles TAB "maxAge" @ %style];
    %speed = %style[$gFloatingTextStyles TAB "speed" @ %style];
    %this.floatText(%text, %maxAge, %speed, %baseColor, %baseAlpha);
};
function ClientCmdFloatText(%playerGhostID, %text, %style) {
    %player = ServerConnection.resolveGhostID(%playerGhostID);
    if (!(isObject(%player))) {
        error(getScopeName() @ " " @ "- could not resolve ghost" @ " " @ %playerGhostID @ " " @ %text);
        return;
    }
    %player.floatTextSimple(%text, %style);
};
function ThePointsFloaterHud::doTick(%this) {
    cancel(%this.timerID);
    %this.timerID = "";
    %numChildren = %this.getCount();
    if ((1.0 < %numChildren)) {
        return;
    }
    %n = (1.0 - %numChildren);
    if ((0.0 >= %n)) {
        %ctrl = %this.getObject(%n);
        %ageNorm = (%ctrl.maxAge / %ctrl.age);
        if ((1.0 > %ageNorm)) {
            %ctrl.delete();
        }
        %ctrl.age = (1.0 + %ctrl.age);
        %x = getWord(%ctrl.position, 0);
        %y = getWord(%ctrl.position, 1);
        if ((0.2 > %ageNorm)) {
            %x = ((3.0 * ((0.2 - %ageNorm) * %ctrl.speed)) + %x);
            %y = (%ctrl.speed - %y);
        }
        %ctrl.reposition(%x, %y);
        %alpha = (%ctrl.baseAlpha * (%ageNorm - 1.0));
        if ((0.2 < %ageNorm)) {
            %alpha = (((0.5 + (0.5 * mSin((0.03 * getSimTime())))) * %alpha) - 1.0);
        }
        %alpha1 = formatInt("%0.2X", (255.0 * %alpha));
        %alpha2 = formatInt("%0.2X", (255.0 * 1.0));
        if ((0.2 > %ageNorm)) {
            %alpha2 = 00;
        }
        %colorTag = "<color:" @ %ctrl.BaseColor @ %alpha1 @ ">";
        %shadowTag = "<shadowcolor:" @ 000000 @ %alpha2 @ ">";
        %ctrl.setText(%shadowTag @ %colorTag @ %ctrl.baseText);
        %n = (1.0 - %n);
    }
    %this.timerID = (0.0 >= %n) @ %this.schedule(%this.tickPeriodMS, "doTick");
};
function clientCmdUpdateVPoints(%newPoints, %notify) {
    if (!(isDefined("%notify"))) {
        %notify = 1;
    }
    if (!(%newPoints $= "")) {
        BalanceUpdateSpecialEffects("vPoints", $Player::VPoints, %newPoints, %notify);
        $Player::VPoints = mFloor(%newPoints);
        updateAccountBalanceDisplays();
    }
};
function clientCmdUpdateVBux(%newBux, %notify) {
    if (!(isDefined("%notify"))) {
        %notify = 1;
    }
    if (!(%newBux $= "")) {
        BalanceUpdateSpecialEffects("vBux", $Player::VBux, %newBux, %notify);
        $Player::VBux = mFloor(%newBux);
        updateAccountBalanceDisplays();
    }
};
function ClosetGUI_ToggleSku_Shops(%sku) {
    %wordLoc = findWord($StoreSkusLayer, %sku);
    if ((0.0 >= %wordLoc)) {
        $StoreSkusLayer = removeWord($StoreSkusLayer, %wordLoc);
        StoreShortDescText.setBaseDesc("");
        StoreLongDescText.setBaseDesc("");
        ClosetTabs.updateAuthorWidget("");
    }
    $StoreSkusLayer = SkuManager.overlaySkus($StoreSkusLayer, %sku);
    StoreShortDescText.setBaseDesc(ClosetTabs.getShortSkuDesc(%sku));
    StoreLongDescText.setBaseDesc(ClosetTabs.getLongSkuDesc(%sku));
    ClosetTabs.updateAuthorWidget(%sku);
    %count = getWordCount($StoreSkusLayer);
    %itemsToAdd = 0;
    %i = 0;
    if ((%count < %i)) {
        %sku2 = getWord($StoreSkusLayer, %i);
        if (!(StoreShoppingList.containsSku(%sku2))) {
            %itemsToAdd = 1;
        }
        %i = (1.0 + %i);
    }
    StoreAddItemsButton.setActive(%itemsToAdd);
};
