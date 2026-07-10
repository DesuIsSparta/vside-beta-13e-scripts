function ClosetTabs::refreshStoreTab(%this) {
    StoreCategoryPopup.clear();
    if (!($gCurrentStoreName[$gStoreStockLoaded @ $gCurrentStoreName])) {
        "Loading ...".add(StoreCategoryPopup);
        return;
    }
    %allCategories = "All Items" @ "\t" @ "All Garments" @ "\t" @ "All Accessories" @ "\t" @ "Tops" @ "\t" @ "Bottoms" @ "\t" @ "Hair" @ "\t" @ "Face" @ "\t" @ "Skin" @ "\t" @ "Shoes" @ "\t" @ "Ear" @ "\t" @ "Neck" @ "\t" @ "Waist" @ "\t" @ "Hands" @ "\t" @ "Bags" @ "\t" @ "Glasses" @ "\t" @ "Props" @ "\t" @ "Misc" @ "\t" @ "BodyMod";
    %storeDrwrs = $player.getGender().filterSkusGender(SkuManager, Inventory::getCurrentStoreSkus()).getSkuDrwrs(SkuManager);
    if ((%storeDrwrs $= "")) {
        StoreItemsFrame.update();
    }
    %n = 0;
    while ((%n < getFieldCount(%allCategories))) {
        %cat = getField(%allCategories, %n);
        %catDrwrs = strlwr(%cat).get(ThumbCategories);
        %m = 0;
        while ((%m < getWordCount(%catDrwrs))) {
            %found = findWord(%storeDrwrs, getWord(%catDrwrs, %m));
            if ((%found >= 0.0)) {
                %cat.add(StoreCategoryPopup);
            }
            %m = (%m + 1.0);
        }
        %n = (%n + 1.0);
        (%m < getWordCount(%catDrwrs));
    }
    loadStorePosition();
};
function ClosetTabs::fillStoreTab(%this) {
    %theTab = "SHOPS".getTabWithName(%this);
    if (!(isObject(%theTab))) {
        return;
    }
    new GuiBitmapCtrl("") {
        profile = 0 @ "GuiDefaultProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "26 26";
        extent = "571 37";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        bitmap = "platform/client/ui/closet_tabs_bracket";
    };.add(%theTab);
    %tabWidth = getWord(%theTab.getExtent(), 0);
    %tabHeight = getWord(%theTab.getExtent(), 1);
    new GuiBitmapCtrl(StoreSpecificBackground) {
        profile = "GuiDefaultProfile";
        horizSizing = "center";
        vertSizing = "center";
        position = ((%tabWidth / 2.0) - 256.0) @ " " @ ((%tabHeight / 2.0) - 256.0);
        extent = "512 512";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 0;
        bitmap = "";
    };.add(%theTab);
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
    %storename.add(%nameDescFrame);
    %storeDesc.add(%nameDescFrame);
    %nameDescFrame.add(%theTab);
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
    %categoryLabel.add(%theTab);
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
    %categoryPopup.add(%theTab);
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
    %ctrl.add(%theTab);
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
    %itemsInfoText.add(%itemsFrame);
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
    %itemsRangeText.add(%itemsFrame);
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
    "ClosetItemsScroll".bindClassName(%itemsScroll);
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
    %thumbnails.add(%itemsScroll);
    %itemsScroll.thumbnails = %thumbnails;
    %itemsScroll.add(%itemsFrame);
    %itemsFrame.thumbnails = %thumbnails;
    %itemsFrame.add(%theTab);
    %theTab.thumbnails = %thumbnails;
    new GuiVariableWidthButtonCtrl(StoreDirectoryLink) {
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
    };.add(%theTab);
    new GuiMLTextCtrl(StoreDirectoryLinkBigText) {
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
    };.add(%theTab);
    new GuiBitmapButtonCtrl(StoreDirectoryLinkBig) {
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
    };.add(%theTab);
    new GuiControl(StoreBannerFrame) {
        profile = "GuiDefaultProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "26 483";
        extent = "464 69";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
    };.add(%theTab, new GuiBitmapButtonCtrl(StoreBanner) {
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
    };);
    new GuiBitmapCtrl("") {
        profile = 0 @ "GuiDefaultProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "689 26";
        extent = "245 45";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        bitmap = "platform/client/ui/balance_bracket";
    };.add(%theTab, new GuiMLTextCtrl(StoreBalanceText) {
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
    %itemDescFrame.add(%theTab);
    new GuiWindowCtrl(StoreItemDescHiliteFrame) {
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
    };.add(%theTab);
    new GuiWindowCtrl(StoreFloatingHiliteFrame) {
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
    };.add(%theTab);
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
    new GuiVariableWidthButtonCtrl(StoreAddItemsButton) {
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
    };.add(%theTab);
    0.setActive(StoreAddItemsButton);
    %wi = AnimCtrl::newAnimCtrl("129 213", "18 18");
    60.setDelay(%wi);
    "platform/client/ui/wait0.png".addFrame(%wi);
    "platform/client/ui/wait1.png".addFrame(%wi);
    "platform/client/ui/wait2.png".addFrame(%wi);
    "platform/client/ui/wait3.png".addFrame(%wi);
    "platform/client/ui/wait4.png".addFrame(%wi);
    "platform/client/ui/wait5.png".addFrame(%wi);
    "platform/client/ui/wait6.png".addFrame(%wi);
    "platform/client/ui/wait7.png".addFrame(%wi);
    %wi.add(StoreShoppingBag);
    waitIcon = %wi @ StoreShoppingBag;
    0.setVisible(%wi);
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
    %shoppingList.add(%shoppingScroll);
    %shoppingScroll.add(%shoppingBag);
    %shoppingBag.add(%theTab);
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
    %doneButton.add(%theTab);
    %theTab.doneButton = %doneButton;
    %cancelButton.add(%theTab);
    %theTab.cancelButton = %cancelButton;
    1.setStoreControlsVisible(ClosetTabs);
    %this.tabShopsInitialized = 1;
};
function ClosetTabs::setStoreControlsVisible(%this, %flag) {
    %flag.setVisible(StoreNameDescFrame);
    %flag.setVisible(StoreCategoryLabel);
    %flag.setVisible(StoreCategoryPopup);
    %flag.setVisible(StoreItemsFrame);
    %flag.setVisible(StoreBannerFrame);
    %flag.setVisible(StoreAddItemsButton);
    %flag.setVisible(StoreShortDescText);
    %flag.setVisible(StoreLongDescText);
    %flag.setVisible(StoreShoppingBag);
    %flag.setVisible(StoreItemDescFrame);
    if (%flag) {
    }
    !(isInFUE()).setVisible(StoreDirectoryLink);
    if (!(%flag)) {
        %flag.setVisible(StoreItemDescHiliteFrame);
        %flag.setVisible(StoreFloatingHiliteFrame);
    }
    if (isInFUE()) {
        !(%flag).setVisible(closetGuiFUE_vPoints_vBux_Image);
    }
};
function ClosetTabs::setLeaveStoreControlsVisible(%this, %flag) {
    %flag.setVisible(StoreDirectoryLinkBigText);
    %flag.setVisible(StoreDirectoryLinkBig);
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
    %background.add(%child);
    %hilite.add(%child);
    %itemDesc.add(%child);
    %expiringIcon.add(%child);
    %buxLink.add(%child);
    %pointsLink.add(%child);
    %totalButton.add(%child);
    %closeBox.add(%child);
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
        "StoreShoppingListItem".bindClassName(%child);
    }
};
function StoreShoppingList::addSku(%this, %sku) {
    if ((findWord($Player::inventory, %sku) >= 0.0)) {
        return;
    }
    %count = %this.getCount();
    %idx = 0;
    while ((%idx < %count)) {
        if ((%idx.getObject(%this).sku $= %sku)) {
            return;
        }
        %idx = (%idx + 1.0);
    }
    %child = %this.addChild();
    (%idx < %count);
    %child.sku = %sku;
    %si = %sku.findBySku(SkuManager);
    %si.descShrt.setText(%child.desc);
    %child.desc.command = "ClosetGui.toggleSku(" @ %sku @ ");" @ "ClosetThumbnailsShop.scroll.scrollToSku(" @ %sku @ ");";
    %pointsIcon = "<bitmap:platform/client/ui/vpoints_9>";
    %buxIcon = "<bitmap:platform/client/ui/vbux_9>";
    %child.points = Inventory::getVPointsPriceForSku(%child.sku);
    %child.bux = Inventory::getVBuxPriceForSku(%child.sku);
    %pointsIcon @ " " @ %child.points.setText(%child.pointsLink);
    %buxIcon @ " " @ %child.bux.setText(%child.buxLink);
    if (!(%si.expireTime $= "")) {
        "platform/client/ui/expiring_icon".setBitmap(%child.expiringIcon);
        1.setVisible(%child.expiringIcon);
    }
    "".setBitmap(%child.expiringIcon);
    0.setVisible(%child.expiringIcon);
    %count = %child.thumbnails.getCount(StoreItemsFrame);
    %i = 0;
    while ((%i < %count)) {
        %obj = %i.getObject(StoreItemsFrame, %child.thumbnails);
        if ((%obj.sku == %sku)) {
            "platform/client/buttons/removeFromCart".setBitmap(%obj.toggleCartButton);
        }
        %i = (%i + 1.0);
    }
    %this.update();
    (%this.getCount() - 1.0).hiliteCell(%this, 0);
};
function StoreShoppingList::addSkus(%this, %skulist) {
    %skulist = trim(%skulist);
    %count = getWordCount(%skulist);
    %i = 0;
    while ((%i < %count)) {
        getWord(%skulist, %i).addSku(%this);
        %i = (%i + 1.0);
    }
};
function StoreShoppingList::removeSku(%this, %sku) {
    %count = %this.getCount();
    %i = 0;
    while ((%i < %count)) {
        %obj = %i.getObject(%this);
        if ((%obj.sku == %sku)) {
            %obj.delete();
        }
        %i = (%i + 1.0);
    }
    %count = %obj.thumbnails.getCount(StoreItemsFrame);
    (%i < %count);
    %i = 0;
    while ((%i < %count)) {
        %obj = %i.getObject(StoreItemsFrame, %obj.thumbnails);
        if ((%obj.sku == %sku)) {
            "platform/client/buttons/add2cart".setBitmap(%obj.toggleCartButton);
        }
        %i = (%i + 1.0);
    }
    if ((findWord($StoreSkusLayer, %sku) != -(1.0))) {
        1.setActive(StoreAddItemsButton);
    }
    %this.update();
};
function StoreShoppingList::removeSkus(%this, %skulist) {
    %skulist = trim(%skulist);
    %count = getWordCount(%skulist);
    %i = 0;
    while ((%i < %count)) {
        getWord(%skulist, %i).removeSku(%this);
        %i = (%i + 1.0);
    }
};
function StoreShoppingList::clear(%this) {
    %this.getSkus().removeSkus(%this);
};
function StoreShoppingList::containsSku(%this, %sku) {
    %count = %this.getCount();
    %i = 0;
    while ((%i < %count)) {
        if ((%i.getObject(%this).sku == %sku)) {
            return 1;
        }
        %i = (%i + 1.0);
    }
    return 0;
};
function StoreShoppingList::getSkus(%this) {
    %skus = "";
    %count = %this.getCount();
    %i = 0;
    while ((%i < %count)) {
        %skus = %skus @ " " @ %i.getObject(%this).sku;
        %i = (%i + 1.0);
    }
    return trim(%skus);
};
function StoreShoppingList::addItemsYoureWearing(%this) {
    %count = getWordCount($StoreSkusLayer);
    %i = 0;
    while ((%i < %count)) {
        getWord($StoreSkusLayer, %i).addSku(%this);
        %i = (%i + 1.0);
    }
    0.setActive(StoreAddItemsButton);
};
function StoreShoppingList::clear(%this) {
    Parent::clear(%this);
    %this.update();
};
function StoreShoppingList::update(%this) {
    (%this.getCount() == 0.0).setVisible(StoreNoItemsText);
    %this.reseatChildren();
    %this.sumPrices();
    %count = %this.getCount();
    %i = 0;
    while ((%i < %count)) {
        %child = %i.getObject(%this);
        if (((%i % 2) == 0.0)) {
            // unhandled opcode 8756 at 0x00002231
        }
        %child.background.setProfile();
        %i = (%i + 1.0);
        ClosetDkBackgroundProfile;
    }
};
function StoreShoppingList::sumPrices(%this) {
    %pointsSum = 0;
    %buxSum = 0;
    %count = %this.getCount();
    %idx = 0;
    while ((%idx < %count)) {
        %child = %idx.getObject(%this);
        if (!(%child.points $= "-")) {
            %pointsSum = (%pointsSum + %child.points);
        }
        if (!(%child.bux $= "-")) {
            %buxSum = (%buxSum + %child.bux);
        }
        %idx = (%idx + 1.0);
    }
    %pointsIcon = "<bitmap:platform/client/ui/vpoints_9>";
    (%idx < %count);
    %buxIcon = "<bitmap:platform/client/ui/vbux_9>";
    %pointsIcon @ " " @ %pointsSum.setText(StorePointsTotalText);
    %buxIcon @ " " @ %buxSum.setText(StoreBuxTotalText);
    %this.pointsTotal = %pointsSum;
    %this.buxTotal = %buxSum;
};
function StoreShoppingList::scrollToItem(%this, %item) {
    %idx = %item.getObjectIndex(%this);
    if ((%idx >= 0.0)) {
        %scroll = %this.scroll;
        %cellHeight = (getWord(%this.childrenExtent, 1) + %this.spacing);
        %numRowsVisible = (getWord(%scroll.getExtent(), 1) / %cellHeight);
        %ypos = (1.0 - getWord(%this.getPosition(), 1));
        %closestRow = (%ypos / %cellHeight);
        %targetRow = getWord(%this.hilitedCell, 1);
        if ((%targetRow < %closestRow)) {
            (%cellHeight * %targetRow).scrollTo(%scroll, 0);
        }
        if ((%targetRow >= ((%closestRow + %numRowsVisible) - 1.0))) {
            ((%cellHeight * ((%targetRow - %numRowsVisible) + 1.0)) + (2.0 * %this.Parent.spacing)).scrollTo(%scroll, 0);
        }
    }
};
function StoreShoppingListItem::onMouseEnterBounds(%this) {
    %idx = %this.getObjectIndex(%this.shoppingList);
    %idx.hiliteCell(%this.shoppingList, 0);
};
function StoreShoppingListItem::onMouseLeaveBounds(%this) {
    %this.onUnhilite();
};
function StoreShoppingListItem::onHilite(%this) {
    if (0) {
    }
    if (isObject(StoreLongDescText)) {
        %this.sku.getShortSkuDesc(ClosetTabs).setDesc(StoreShortDescText);
        %this.sku.getLongSkuDesc(ClosetTabs).setDesc(StoreLongDescText);
        %this.sku.updateAuthorWidget(ClosetTabs);
    }
    %this.scrollToItem(StoreShoppingList);
    1.setVisible(%this.hiliteCtrl);
};
function StoreShoppingListItem::onUnhilite(%this) {
    if (0) {
    }
    if (isObject(StoreLongDescText)) {
        StoreShortDescText.showBaseDesc();
        StoreLongDescText.showBaseDesc();
        "".updateAuthorWidget(ClosetTabs);
    }
    if (isObject(StoreItemDescHiliteFrame)) {
        0.setVisible(StoreItemDescHiliteFrame);
    }
    if (isObject(StoreFloatingHiliteFrame)) {
        0.setVisible(StoreFloatingHiliteFrame);
    }
    0.setVisible(%this.hiliteCtrl);
};
function StoreBalanceText::update(%this) {
    %pointsIcon = "<bitmap:platform/client/ui/vpoints_14>";
    %buxIcon = "<bitmap:platform/client/ui/vbux_14>";
    %pointsInfoLink = "<spush><linkcolor:159fe7><a:gamelink " @ $Net::HelpURL_VPoints @ ">>> How to earn vPoints</a><spop>";
    %getBuxLink = "<spush><linkcolor:13b93c><a:gamelink " @ $Net::AddFundsURL @ ">>> Get more vBux</a><spop>";
    "You Have   " @ "<spush><font:Arial Bold:16><color:159fe7>" @ %pointsIcon @ " " @ commaify($Player::VPoints) @ "<spop>" @ "   " @ "<spush><font:Arial Bold:16><color:13b93c>" @ %buxIcon @ " " @ commaify($Player::VBux) @ "<spop>" @ "<br>" @ "<font:Arial Bold:13>" @ %pointsInfoLink @ "   " @ %getBuxLink.setText(%this);
};
function StoreBalanceText::onURL(%this, %url) {
    if ((getWord(%url, 0) $= "gamelink")) {
        %url = getWords(%url, 1);
    }
    Parent::onURL(%this, %url);
};
function StoreItemsFrame::update(%this) {
    if (!($gCurrentStoreName[$gStoreStockLoaded @ $gCurrentStoreName])) {
        "Loading store inventory...".setText(%this.thumbnails.infoText);
    }
    if ((Inventory::getCurrentStoreSkus() $= "")) {
        "Nothing in stock!".setText(%this.thumbnails.infoText);
    }
    "No matching items.".setText(%this.thumbnails.infoText);
    strlwr(%this.category).get(ThumbCategories).setDrawers(%this.thumbnails);
};
function StoreCategoryPopup::onSelect(%this, %unused, %entries) {
    %this.category = %entries @ StoreItemsFrame;
    StoreItemsFrame.update();
    "".schedule(StoreShortDescText, 0, "setBaseDesc");
    "".schedule(StoreLongDescText, 0, "setBaseDesc");
    ClosetThumbnailsShop.getParent().scrollToTop();
};
function StoreLongDescText::setBaseDesc(%this, %desc) {
    %this.baseText = %desc;
    %desc.setText(%this);
    if ((%desc $= "")) {
        0.setVisible(StoreItemDescHiliteFrame);
        0.setVisible(StoreFloatingHiliteFrame);
    }
};
function StoreLongDescText::setDesc(%this, %desc) {
    %desc.setText(%this);
};
function StoreLongDescText::showBaseDesc(%this) {
    %this.baseText.setText(%this);
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
    %delta = (%newVal - %oldVal);
    if (%notify) {
        schedule(3000, 0, "floatBalanceChange", %whichBalance, %delta, $player);
    }
    %threshhold1 = %whichBalance[$gBalanceUpdateSpecialEffect_Threshhold1 @ %whichBalance];
    if ((%delta < %threshhold1)) {
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
    if ((%delta < %threshhold2)) {
        %sound = %sound1;
        %pulseCount = %pulseCount1;
    }
    %sound = %sound2;
    %pulseCount = %pulseCount2;
    alxPlay(%sound);
    5.schedule(%guiControl1, %delay, "blinkSet", "bounce", 150, 100, "0 -1 0 0");
    if (isObject(%guiControl2)) {
        %pulseCount.startPulse(%guiControl2);
    }
};
function floatBalanceChange(%whichBalance, %change, %player) {
    %threshhold = 1;
    if ((%change < %threshhold)) {
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
    %amountNorm = mClampF((%change / %alot), 0, 1);
    %amountNorm = (1.0 - ((1.0 - %amountNorm) * (1.0 - %amountNorm)));
    %isALot = (%amountNorm > 0.95);
    %speed = (((1.0 - %amountNorm) * 2.0) + 3.0);
    %fontTag = %isALot ? "<font:arial:18>" : "<font:arial:14>";
    %currencyText = %isVPoints ? "vPoints" : "vBux";
    %text = %fontTag @ "<b><outline>+" @ " " @ %change @ " " @ %currencyText;
    %maxAge = ((%amountNorm * 70.0) + 70.0);
    %baseColor = %isVPoints ? "55eeff" : "22dd44";
    %baseAlpha = ((%amountNorm * 0.4) + 0.6);
    %baseAlpha.floatText(%player, %text, %maxAge, %speed, %baseColor);
};
function Player::floatText(%this, %text, %maxAge, %speed, %baseColor, %baseAlpha) {
    %this.hudCtrl.updatePosition();
    %text = "<just:center>" @ %text;
    %width = 600;
    %ctrl = new GuiMLTextCtrl("") {
        extent = 0 @ %width @ " " @ 18;
        position = (getWord(%this.hudCtrl.position, 0) - (%width / 2.0)) @ " " @ (getWord(%this.hudCtrl.position, 1) + 100.0);
        age = 0;
        maxAge = %maxAge;
        speed = %speed;
        BaseColor = %baseColor;
        baseText = %text;
        baseAlpha = %baseAlpha;
    };
    %ctrl.add(ThePointsFloaterHud);
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
    %baseAlpha.floatText(%this, %text, %maxAge, %speed, %baseColor);
};
function ClientCmdFloatText(%playerGhostID, %text, %style) {
    %player = %playerGhostID.resolveGhostID(ServerConnection);
    if (!(isObject(%player))) {
        error(getScopeName() @ " " @ "- could not resolve ghost" @ " " @ %playerGhostID @ " " @ %text);
        return;
    }
    %style.floatTextSimple(%player, %text);
};
function ThePointsFloaterHud::doTick(%this) {
    cancel(%this.timerID);
    %this.timerID = "";
    %numChildren = %this.getCount();
    if ((%numChildren < 1.0)) {
        return;
    }
    %n = (%numChildren - 1.0);
    while ((%n >= 0.0)) {
        %ctrl = %n.getObject(%this);
        %ageNorm = (%ctrl.age / %ctrl.maxAge);
        if ((%ageNorm > 1.0)) {
            %ctrl.delete();
        }
        %ctrl.age = (%ctrl.age + 1.0);
        %x = getWord(%ctrl.position, 0);
        %y = getWord(%ctrl.position, 1);
        if ((%ageNorm > 0.2)) {
            %x = (%x + ((%ctrl.speed * (%ageNorm - 0.2)) * 3.0));
            %y = (%y - %ctrl.speed);
        }
        %y.reposition(%ctrl, %x);
        %alpha = ((1.0 - %ageNorm) * %ctrl.baseAlpha);
        if ((%ageNorm < 0.2)) {
            %alpha = (1.0 - (%alpha * ((mSin((getSimTime() * 0.03)) * 0.5) + 0.5)));
        }
        %alpha1 = formatInt("%0.2X", (%alpha * 255.0));
        %alpha2 = formatInt("%0.2X", (1.0 * 255.0));
        if ((%ageNorm > 0.2)) {
            %alpha2 = 00;
        }
        %colorTag = "<color:" @ %ctrl.BaseColor @ %alpha1 @ ">";
        %shadowTag = "<shadowcolor:" @ 000000 @ %alpha2 @ ">";
        %shadowTag @ %colorTag @ %ctrl.baseText.setText(%ctrl);
        %n = (%n - 1.0);
    }
    %this.timerID = (%n >= 0.0) @ "doTick".schedule(%this, %this.tickPeriodMS);
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
    if ((%wordLoc >= 0.0)) {
        $StoreSkusLayer = removeWord($StoreSkusLayer, %wordLoc);
        "".setBaseDesc(StoreShortDescText);
        "".setBaseDesc(StoreLongDescText);
        "".updateAuthorWidget(ClosetTabs);
    }
    $StoreSkusLayer = %sku.overlaySkus(SkuManager, $StoreSkusLayer);
    %sku.getShortSkuDesc(ClosetTabs).setBaseDesc(StoreShortDescText);
    %sku.getLongSkuDesc(ClosetTabs).setBaseDesc(StoreLongDescText);
    %sku.updateAuthorWidget(ClosetTabs);
    %count = getWordCount($StoreSkusLayer);
    %itemsToAdd = 0;
    %i = 0;
    while ((%i < %count)) {
        %sku2 = getWord($StoreSkusLayer, %i);
        if (!(%sku2.containsSku(StoreShoppingList))) {
            %itemsToAdd = 1;
        }
        %i = (%i + 1.0);
    }
    %itemsToAdd.setActive(StoreAddItemsButton);
};
