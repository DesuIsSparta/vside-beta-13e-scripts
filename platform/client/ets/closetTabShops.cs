function ClosetTabs::refreshStoreTab(%this)
{
    StoreCategoryPopup.clear();
    if (!$gStoreStockLoaded[$gCurrentStoreName])
    {
        StoreCategoryPopup.add("Loading ...");
        return ;
    }
    %allCategories = "All Items" TAB "All Garments" TAB "All Accessories" TAB "Tops" TAB "Bottoms" TAB "Hair" TAB "Face" TAB "Skin" TAB "Shoes" TAB "Ear" TAB "Neck" TAB "Waist" TAB "Hands" TAB "Bags" TAB "Glasses" TAB "Props" TAB "Misc" TAB "BodyMod";
    %storeDrwrs = SkuManager.getSkuDrwrs(SkuManager.filterSkusGender(Inventory::getCurrentStoreSkus(), $player.getGender()));
    if (%storeDrwrs $= "")
    {
        StoreItemsFrame.update();
    }
    else
    {
        %n = 0;
        while (%n < getFieldCount(%allCategories))
        {
            %cat = getField(%allCategories, %n);
            %catDrwrs = ThumbCategories.get(strlwr(%cat));
            %m = 0;
            while (%m < getWordCount(%catDrwrs))
            {
                %found = findWord(%storeDrwrs, getWord(%catDrwrs, %m));
                if (%found >= 0)
                {
                    StoreCategoryPopup.add(%cat);
                    break;
                }
                %m = %m + 1;
            }
            %n = %n + 1;
        }
    }
    loadStorePosition();
    return ;
}
function ClosetTabs::fillStoreTab(%this)
{
    %theTab = %this.getTabWithName("SHOPS");
    if (!isObject(%theTab))
    {
        return ;
    }
    %tabWidth = getWord(%theTab.getExtent(), 0);
    %tabHeight = getWord(%theTab.getExtent(), 1);
    %storename = new GuiTextCtrl()
    {
        profile = "ClosetTitleProfile";
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
    %storeDesc = new GuiMLTextCtrl()
    {
        profile = "ClosetLeftInfoProfile";
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
    %nameDescFrame = new GuiControl(StoreNameDescFrame)
    {
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
    %categoryLabel = new GuiTextCtrl(StoreCategoryLabel)
    {
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
    %categoryPopup = new GuiPopUp2MenuCtrl(StoreCategoryPopup)
    {
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
    %ctrl = new GuiControl(StoreExpirationLegend)
    {
        position = "26 465";
        extent = "260 20";
        visible = 0;
        lastStore = "";
    };
    %theTab.add(%ctrl);
    %itemsFrame = new GuiControl(StoreItemsFrame)
    {
        profile = "GuiDefaultProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "22 120";
        extent = "467 350";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
    };
    %itemsInfoText = new GuiTextCtrl()
    {
        profile = "ClosetLeftInfoProfile";
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
    %itemsRangeText = new GuiTextCtrl()
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
    %itemsFrame.add(%itemsRangeText);
    %theTab.rangeText = %itemsRangeText;
    %itemsScroll = new GuiScrollCtrl()
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
    %itemsScroll.bindClassName("ClosetItemsScroll");
    %theTab.itemsScroll = %itemsScroll;
    %thumbnails = new GuiArray2Ctrl(ClosetThumbnailsShop)
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
        scroll = %itemsScroll;
    };
    %itemsScroll.add(%thumbnails);
    %itemsScroll.thumbnails = %thumbnails;
    %itemsFrame.add(%itemsScroll);
    %itemsFrame.thumbnails = %thumbnails;
    %theTab.add(%itemsFrame);
    %theTab.thumbnails = %thumbnails;
    new GuiControl(StoreBannerFrame)
    {
        profile = "GuiDefaultProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "26 483";
        extent = "464 69";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
    }.add(new GuiControl(StoreBannerFrame)
    {
        profile = "GuiDefaultProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "26 483";
        extent = "464 69";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
    });
    new GuiBitmapCtrl()
    {
        profile = "GuiDefaultProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "689 26";
        extent = "245 45";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        bitmap = "platform/client/ui/balance_bracket";
    }.add(new GuiBitmapCtrl()
    {
        profile = "GuiDefaultProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "689 26";
        extent = "245 45";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        bitmap = "platform/client/ui/balance_bracket";
    });
    %itemDescFrame = new GuiWindowCtrl(StoreItemDescFrame)
    {
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
    %theTab.add(%itemDescFrame);
    %shoppingBag = new GuiWindowCtrl(StoreShoppingBag)
    {
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
    StoreShoppingBag.waitIcon = %wi;
    %wi.setVisible(0);
    %shoppingScroll = new GuiScrollCtrl()
    {
        profile = "ETSScrollProfile";
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
    %shoppingList = new GuiArray2Ctrl(StoreShoppingList)
    {
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
    %doneButton = new GuiVariableWidthButtonCtrl()
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
    %cancelButton = new GuiVariableWidthButtonCtrl()
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
    %theTab.add(%doneButton);
    %theTab.doneButton = %doneButton;
    %theTab.add(%cancelButton);
    %theTab.cancelButton = %cancelButton;
    ClosetTabs.setStoreControlsVisible(1);
    %this.tabShopsInitialized = 1;
    return ;
}
function ClosetTabs::setStoreControlsVisible(%this, %flag)
{
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
    StoreDirectoryLink.setVisible(%flag && !isInFUE());
    if (!%flag)
    {
        StoreItemDescHiliteFrame.setVisible(%flag);
        StoreFloatingHiliteFrame.setVisible(%flag);
    }
    if (isInFUE())
    {
        closetGuiFUE_vPoints_vBux_Image.setVisible(!%flag);
    }
    return ;
}
function ClosetTabs::setLeaveStoreControlsVisible(%this, %flag)
{
    StoreDirectoryLinkBigText.setVisible(%flag);
    StoreDirectoryLinkBig.setVisible(%flag);
    return ;
}
function StoreShoppingList::onCreatedChild(%this, %child)
{
    %background = new GuiControl()
    {
        profile = "ClosetLtBackgroundProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "0 0";
        extent = "233 36";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
    };
    %hilite = new GuiControl()
    {
        profile = "ClosetHiliteProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "0 0";
        extent = "233 36";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 0;
    };
    %itemDesc = new GuiVariableWidthButtonCtrl()
    {
        profile = "StoreItemButtonProfile";
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
    %closeBox = new GuiBitmapButtonCtrl()
    {
        profile = "GuiDefaultProfile";
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
    %expiringIcon = new GuiBitmapCtrl()
    {
        profile = "ETSNonModalProfile";
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
    %pointsLink = new GuiMLTextCtrl()
    {
        profile = "ClosetPointsProfile";
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
    %buxLink = new GuiMLTextCtrl()
    {
        profile = "ClosetBuxProfile";
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
    %totalButton = new GuiVariableWidthButtonCtrl()
    {
        profile = "HiddenBracketButton15Profile";
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
    if (!(getWord(%child.getNamespaceList(), 0) $= "StoreShoppingListItem"))
    {
        %child.bindClassName("StoreShoppingListItem");
    }
    return ;
}
function StoreShoppingList::addSku(%this, %sku)
{
    if (findWord($Player::inventory, %sku) >= 0)
    {
        return ;
    }
    %count = %this.getCount();
    %idx = 0;
    while (%idx < %count)
    {
        if (%this.getObject(%idx).sku $= %sku)
        {
            return ;
        }
        %idx = %idx + 1;
    }
    %child = %this.addChild();
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
    if (!(%si.expireTime $= ""))
    {
        %child.expiringIcon.setBitmap("platform/client/ui/expiring_icon");
        %child.expiringIcon.setVisible(1);
    }
    else
    {
        %child.expiringIcon.setBitmap("");
        %child.expiringIcon.setVisible(0);
    }
    %count = StoreItemsFrame.thumbnails.getCount();
    %i = 0;
    while (%i < %count)
    {
        %obj = StoreItemsFrame.thumbnails.getObject(%i);
        if (%obj.sku == %sku)
        {
            %obj.toggleCartButton.setBitmap("platform/client/buttons/removeFromCart");
            break;
        }
        %i = %i + 1;
    }
    %this.update();
    %this.hiliteCell(0, %this.getCount() - 1);
    return ;
}
function StoreShoppingList::addSkus(%this, %skulist)
{
    %skulist = trim(%skulist);
    %count = getWordCount(%skulist);
    %i = 0;
    while (%i < %count)
    {
        %this.addSku(getWord(%skulist, %i));
        %i = %i + 1;
    }
}

function StoreShoppingList::removeSku(%this, %sku)
{
    %count = %this.getCount();
    %i = 0;
    while (%i < %count)
    {
        %obj = %this.getObject(%i);
        if (%obj.sku == %sku)
        {
            %obj.delete();
            break;
        }
        %i = %i + 1;
    }
    %count = StoreItemsFrame.thumbnails.getCount();
    %i = 0;
    while (%i < %count)
    {
        %obj = StoreItemsFrame.thumbnails.getObject(%i);
        if (%obj.sku == %sku)
        {
            %obj.toggleCartButton.setBitmap("platform/client/buttons/add2cart");
            break;
        }
        %i = %i + 1;
    }
    if (findWord($StoreSkusLayer, %sku) != -1)
    {
        StoreAddItemsButton.setActive(1);
    }
    %this.update();
    return ;
}
function StoreShoppingList::removeSkus(%this, %skulist)
{
    %skulist = trim(%skulist);
    %count = getWordCount(%skulist);
    %i = 0;
    while (%i < %count)
    {
        %this.removeSku(getWord(%skulist, %i));
        %i = %i + 1;
    }
}

function StoreShoppingList::clear(%this)
{
    %this.removeSkus(%this.getSkus());
    return ;
}
function StoreShoppingList::containsSku(%this, %sku)
{
    %count = %this.getCount();
    %i = 0;
    while (%i < %count)
    {
        if (%this.getObject(%i).sku == %sku)
        {
            return 1;
        }
        %i = %i + 1;
    }
    return 0;
}
function StoreShoppingList::getSkus(%this)
{
    %skus = "";
    %count = %this.getCount();
    %i = 0;
    while (%i < %count)
    {
        %skus = %skus SPC %this.getObject(%i).sku;
        %i = %i + 1;
    }
    return trim(%skus);
}
function StoreShoppingList::addItemsYoureWearing(%this)
{
    %count = getWordCount($StoreSkusLayer);
    %i = 0;
    while (%i < %count)
    {
        %this.addSku(getWord($StoreSkusLayer, %i));
        %i = %i + 1;
    }
    StoreAddItemsButton.setActive(0);
    return ;
}
function StoreShoppingList::clear(%this)
{
    Parent::clear(%this);
    %this.update();
    return ;
}
function StoreShoppingList::update(%this)
{
    StoreNoItemsText.setVisible(%this.getCount() == 0);
    %this.reseatChildren();
    %this.sumPrices();
    %count = %this.getCount();
    %i = 0;
    while (%i < %count)
    {
        %child = %this.getObject(%i);
        %child.background.setProfile((%i % 2) == 0 ? ClosetLtBackgroundProfile : ClosetDkBackgroundProfile);
        %i = %i + 1;
    }
}

function StoreShoppingList::sumPrices(%this)
{
    %pointsSum = 0;
    %buxSum = 0;
    %count = %this.getCount();
    %idx = 0;
    while (%idx < %count)
    {
        %child = %this.getObject(%idx);
        if (!(%child.points $= "-"))
        {
            %pointsSum = %pointsSum + %child.points;
        }
        if (!(%child.bux $= "-"))
        {
            %buxSum = %buxSum + %child.bux;
        }
        %idx = %idx + 1;
    }
    %pointsIcon = "<bitmap:platform/client/ui/vpoints_9>";
    %buxIcon = "<bitmap:platform/client/ui/vbux_9>";
    StorePointsTotalText.setText(%pointsIcon @ " " @ %pointsSum);
    StoreBuxTotalText.setText(%buxIcon @ " " @ %buxSum);
    %this.pointsTotal = %pointsSum;
    %this.buxTotal = %buxSum;
    return ;
}
function StoreShoppingList::scrollToItem(%this, %item)
{
    %idx = %this.getObjectIndex(%item);
    if (%idx >= 0)
    {
        %scroll = %this.scroll;
        %cellHeight = getWord(%this.childrenExtent, 1) + %this.spacing;
        %numRowsVisible = getWord(%scroll.getExtent(), 1) / %cellHeight;
        %ypos = 1 - getWord(%this.getPosition(), 1);
        %closestRow = %ypos / %cellHeight;
        %targetRow = getWord(%this.hilitedCell, 1);
        if (%targetRow < %closestRow)
        {
            %scroll.scrollTo(0, %cellHeight * %targetRow);
        }
        else
        {
            if (%targetRow >= ((%closestRow + %numRowsVisible) - 1))
            {
                %scroll.scrollTo(0, (%cellHeight * ((%targetRow - %numRowsVisible) + 1)) + (2 * %this.Parent.spacing));
            }
        }
    }
    return ;
}
function StoreShoppingListItem::onMouseEnterBounds(%this)
{
    %idx = %this.shoppingList.getObjectIndex(%this);
    %this.shoppingList.hiliteCell(0, %idx);
    return ;
}
function StoreShoppingListItem::onMouseLeaveBounds(%this)
{
    %this.onUnhilite();
    return ;
}
function StoreShoppingListItem::onHilite(%this)
{
    if (0 && isObject(StoreLongDescText))
    {
        StoreShortDescText.setDesc(ClosetTabs.getShortSkuDesc(%this.sku));
        StoreLongDescText.setDesc(ClosetTabs.getLongSkuDesc(%this.sku));
        ClosetTabs.updateAuthorWidget(%this.sku);
    }
    StoreShoppingList.scrollToItem(%this);
    %this.hiliteCtrl.setVisible(1);
    return ;
}
function StoreShoppingListItem::onUnhilite(%this)
{
    if (0 && isObject(StoreLongDescText))
    {
        StoreShortDescText.showBaseDesc();
        StoreLongDescText.showBaseDesc();
        ClosetTabs.updateAuthorWidget("");
    }
    if (isObject(StoreItemDescHiliteFrame))
    {
        StoreItemDescHiliteFrame.setVisible(0);
    }
    if (isObject(StoreFloatingHiliteFrame))
    {
        StoreFloatingHiliteFrame.setVisible(0);
    }
    %this.hiliteCtrl.setVisible(0);
    return ;
}
function StoreBalanceText::update(%this)
{
    %pointsIcon = "<bitmap:platform/client/ui/vpoints_14>";
    %buxIcon = "<bitmap:platform/client/ui/vbux_14>";
    %pointsInfoLink = "<spush><linkcolor:159fe7><a:gamelink " @ $Net::HelpURL_VPoints @ ">>> How to earn vPoints</a><spop>";
    %getBuxLink = "<spush><linkcolor:13b93c><a:gamelink " @ $Net::AddFundsURL @ ">>> Get more vBux</a><spop>";
    %this.setText("You Have   " @ "<spush><font:Arial Bold:16><color:159fe7>" @ %pointsIcon @ " " @ commaify($Player::VPoints) @ "<spop>" @ "   " @ "<spush><font:Arial Bold:16><color:13b93c>" @ %buxIcon @ " " @ commaify($Player::VBux) @ "<spop>" @ "<br>" @ "<font:Arial Bold:13>" @ %pointsInfoLink @ "   " @ %getBuxLink);
    return ;
}
function StoreBalanceText::onURL(%this, %url)
{
    if (getWord(%url, 0) $= "gamelink")
    {
        %url = getWords(%url, 1);
    }
    Parent::onURL(%this, %url);
    return ;
}
function StoreItemsFrame::update(%this)
{
    if (!$gStoreStockLoaded[$gCurrentStoreName])
    {
        %this.thumbnails.infoText.setText("Loading store inventory...");
    }
    else
    {
        if (Inventory::getCurrentStoreSkus() $= "")
        {
            %this.thumbnails.infoText.setText("Nothing in stock!");
        }
        else
        {
            %this.thumbnails.infoText.setText("No matching items.");
        }
    }
    %this.thumbnails.setDrawers(ThumbCategories.get(strlwr(%this.category)));
    return ;
}
function StoreCategoryPopup::onSelect(%this, %unused, %entries)
{
    StoreItemsFrame.category = %entries;
    StoreItemsFrame.update();
    StoreShortDescText.schedule(0, "setBaseDesc", "");
    StoreLongDescText.schedule(0, "setBaseDesc", "");
    ClosetThumbnailsShop.getParent().scrollToTop();
    return ;
}
function StoreLongDescText::setBaseDesc(%this, %desc)
{
    %this.baseText = %desc;
    %this.setText(%desc);
    if (%desc $= "")
    {
        StoreItemDescHiliteFrame.setVisible(0);
        StoreFloatingHiliteFrame.setVisible(0);
    }
    return ;
}
function StoreLongDescText::setDesc(%this, %desc)
{
    %this.setText(%desc);
    return ;
}
function StoreLongDescText::showBaseDesc(%this)
{
    %this.setText(%this.baseText);
    return ;
}
function StoreShortDescText::setBaseDesc(%this, %desc)
{
    StoreLongDescText::setBaseDesc(%this, %desc);
    return ;
}
function StoreShortDescText::setDesc(%this, %desc)
{
    StoreLongDescText::setDesc(%this, %desc);
    return ;
}
function StoreShortDescText::showBaseDesc(%this)
{
    StoreLongDescText::showBaseDesc(%this, %desc);
    return ;
}
function getCurrentStoreID()
{
    %storename = stripChars($gCurrentStoreName, 0123456789);
    if (%storename $= "edocsecret")
    {
        %storename = "edoc";
    }
    if (%storename $= "")
    {
        return $gCurrentStoreName;
    }
    return %storename;
}
function StoreBanner::doAction(%this)
{
    %storename = getCurrentStoreID();
    if (!(%storename $= ""))
    {
        gotoWebPage($Net::PartnerURL @ "/" @ %storename, 0);
    }
    return ;
}
function updateAccountBalanceDisplays()
{
    if (isObject(StoreBalanceText))
    {
        StoreBalanceText.update();
    }
    if (isObject(AccountBalanceHud))
    {
        AccountBalanceHud.update();
    }
    return ;
}
function clientCmdUpdateAccountBalances(%newPoints, %newBux)
{
    if (!(%newBux $= ""))
    {
        BalanceUpdateSpecialEffects("vBux", $Player::VBux, %newBux, 1);
        $Player::VBux = mFloor(%newBux);
    }
    if (!(%newPoints $= ""))
    {
        BalanceUpdateSpecialEffects("vPoints", $Player::VPoints, %newPoints, 1);
        $Player::VPoints = mFloor(%newPoints);
    }
    updateAccountBalanceDisplays();
    return ;
}
function clientCmdRefreshVPoints()
{
    getBalancesAndScores();
    return ;
}
$gBalanceUpdateSpecialEffect_Delay["vBux"] = 150;
$gBalanceUpdateSpecialEffect_Delay["vPoints"] = 300;
$gBalanceUpdateSpecialEffect_Threshhold1["vBux"] = 1;
$gBalanceUpdateSpecialEffect_Threshhold1["vPoints"] = 100;
$gBalanceUpdateSpecialEffect_Threshhold2["vBux"] = 1;
$gBalanceUpdateSpecialEffect_Threshhold2["vPoints"] = 1000;
$gBalanceUpdateSpecialEffect_Sound1["vBux"] = "AudioIm_CaChing";
$gBalanceUpdateSpecialEffect_Sound1["vPoints"] = "AudioIm_vPoints1";
$gBalanceUpdateSpecialEffect_Sound2["vBux"] = "AudioIm_CaChing";
$gBalanceUpdateSpecialEffect_Sound2["vPoints"] = "AudioIm_vPoints2";
$gBalanceUpdateSpecialEffect_GuiControl1["vBux"] = "AccountBalanceVBuxText";
$gBalanceUpdateSpecialEffect_GuiControl1["vPoints"] = "AccountBalanceVPointsText";
$gBalanceUpdateSpecialEffect_GuiControl2["vBux"] = "";
$gBalanceUpdateSpecialEffect_GuiControl2["vPoints"] = "AccountBalanceHud";
$gBalanceUpdateSpecialEffect_PulseCount1["vBux"] = 1;
$gBalanceUpdateSpecialEffect_PulseCount1["vPoints"] = 1;
$gBalanceUpdateSpecialEffect_PulseCount2["vBux"] = 4;
$gBalanceUpdateSpecialEffect_PulseCount2["vPoints"] = 4;
function BalanceUpdateSpecialEffects(%whichBalance, %oldVal, %newVal, %notify)
{
    %delta = %newVal - %oldVal;
    if (%notify)
    {
        schedule(3000, 0, "floatBalanceChange", %whichBalance, %delta, $player);
    }
    %threshhold1 = $gBalanceUpdateSpecialEffect_Threshhold1[%whichBalance];
    if (%delta < %threshhold1)
    {
        return ;
    }
    %delay = $gBalanceUpdateSpecialEffect_Delay[%whichBalance];
    %threshhold2 = $gBalanceUpdateSpecialEffect_Threshhold2[%whichBalance];
    %sound1 = $gBalanceUpdateSpecialEffect_Sound1[%whichBalance];
    %sound2 = $gBalanceUpdateSpecialEffect_Sound2[%whichBalance];
    %guiControl1 = $gBalanceUpdateSpecialEffect_GuiControl1[%whichBalance];
    %guiControl2 = $gBalanceUpdateSpecialEffect_GuiControl2[%whichBalance];
    %pulseCount1 = $gBalanceUpdateSpecialEffect_PulseCount1[%whichBalance];
    %pulseCount2 = $gBalanceUpdateSpecialEffect_PulseCount2[%whichBalance];
    if (%delta < %threshhold2)
    {
        %sound = %sound1;
        %pulseCount = %pulseCount1;
    }
    else
    {
        %sound = %sound2;
        %pulseCount = %pulseCount2;
    }
    alxPlay(%sound);
    %guiControl1.schedule(%delay, "blinkSet", "bounce", 150, 100, "0 -1 0 0", 5);
    if (isObject(%guiControl2))
    {
        %guiControl2.startPulse(%pulseCount);
    }
    return ;
}
function floatBalanceChange(%whichBalance, %change, %player)
{
    %threshhold = 1;
    if (%change < %threshhold)
    {
        return ;
    }
    if (!isObject(%player))
    {
        error(getScopeName() SPC "- can\'t find player!" SPC getTrace());
        return ;
    }
    if (!isObject(%player.hudCtrl))
    {
        error(getScopeName() SPC "- no hudCtrl to attach to!" SPC getTrace());
        return ;
    }
    %isVPoints = %whichBalance $= "vPoints";
    %alot = %isVPoints ? 1000 : 100;
    %amountNorm = mClampF(%change / %alot, 0, 1);
    %amountNorm = 1 - ((1 - %amountNorm) * (1 - %amountNorm));
    %isALot = %amountNorm > 0.95;
    %speed = ((1 - %amountNorm) * 2) + 3;
    %fontTag = %isALot ? "<font:arial:18>" : "<font:arial:14>";
    %currencyText = %isVPoints ? "vPoints" : "vBux";
    %text = %fontTag @ "<b><outline>+" SPC %change SPC %currencyText;
    %maxAge = (%amountNorm * 70) + 70;
    %baseColor = %isVPoints ? "55eeff" : "22dd44";
    %baseAlpha = (%amountNorm * 0.4) + 0.6;
    %player.floatText(%text, %maxAge, %speed, %baseColor, %baseAlpha);
    return ;
}
function Player::floatText(%this, %text, %maxAge, %speed, %baseColor, %baseAlpha)
{
    %this.hudCtrl.updatePosition();
    %text = "<just:center>" @ %text;
    %width = 600;
    %ctrl = new GuiMLTextCtrl()
    {
        extent = %width SPC 18;
        position = getWord(%this.hudCtrl.position, 0) - (%width / 2) SPC getWord(%this.hudCtrl.position, 1) + 100;
        age = 0;
        maxAge = %maxAge;
        speed = %speed;
        BaseColor = %baseColor;
        baseText = %text;
        baseAlpha = %baseAlpha;
    };
    ThePointsFloaterHud.add(%ctrl);
    ThePointsFloaterHud.doTick();
    return ;
}
$gFloatingTextStyles["font","default"] = "arial:14";
$gFloatingTextStyles["outline","default"] = 1;
$gFloatingTextStyles["color","default"] = "FFFFFF";
$gFloatingTextStyles["baseAlpha","default"] = 0.7;
$gFloatingTextStyles["maxAge","default"] = 100;
$gFloatingTextStyles["speed","default"] = 5;
$gFloatingTextStyles["prepend","default"] = "";
$gFloatingTextStyles["append","default"] = "";
$gFloatingTextStyles["font","emphatic1"] = "arial:14";
$gFloatingTextStyles["outline","emphatic1"] = 1;
$gFloatingTextStyles["color","emphatic1"] = "FFFFFF";
$gFloatingTextStyles["baseAlpha","emphatic1"] = 0.8;
$gFloatingTextStyles["maxAge","emphatic1"] = 200;
$gFloatingTextStyles["speed","emphatic1"] = 4;
$gFloatingTextStyles["prepend","emphatic1"] = "";
$gFloatingTextStyles["append","emphatic1"] = "";
$gFloatingTextStyles["font","light1"] = "arial:14";
$gFloatingTextStyles["outline","light1"] = !1;
$gFloatingTextStyles["color","light1"] = "FFFFFF";
$gFloatingTextStyles["baseAlpha","light1"] = 0.7;
$gFloatingTextStyles["maxAge","light1"] = 20;
$gFloatingTextStyles["speed","light1"] = 10;
$gFloatingTextStyles["prepend","light1"] = "";
$gFloatingTextStyles["append","light1"] = "";
$gFloatingTextStyles["font","SUPERSLOWDRIP"] = "arial:14";
$gFloatingTextStyles["outline","SUPERSLOWDRIP"] = 1;
$gFloatingTextStyles["color","SUPERSLOWDRIP"] = "159FE7";
$gFloatingTextStyles["baseAlpha","SUPERSLOWDRIP"] = 1;
$gFloatingTextStyles["maxAge","SUPERSLOWDRIP"] = 100;
$gFloatingTextStyles["speed","SUPERSLOWDRIP"] = 4;
$gFloatingTextStyles["prepend","SUPERSLOWDRIP"] = "";
$gFloatingTextStyles["append","SUPERSLOWDRIP"] = "";
function Player::floatTextSimple(%this, %text, %style)
{
    if (!isDefined("%style"))
    {
        %style = "";
    }
    if (%style $= "")
    {
        %style = "default";
    }
    if ($gFloatingTextStyles["font",%style] $= "")
    {
        error(getScopeName() SPC "- unknown style:" SPC %style SPC %text SPC getTrace());
        %style = "default";
    }
    %outline = $gFloatingTextStyles["outline",%style] ? "<b><outline>" : "";
    %text = "<font:" @ $gFloatingTextStyles["font",%style] @ ">" @ %text;
    %text = %outline @ %text;
    %text = $gFloatingTextStyles["prepend",%style] @ %text;
    %text = %text @ $gFloatingTextStyles["append",%style];
    %baseColor = $gFloatingTextStyles["color",%style];
    %baseAlpha = $gFloatingTextStyles["baseAlpha",%style];
    %maxAge = $gFloatingTextStyles["maxAge",%style];
    %speed = $gFloatingTextStyles["speed",%style];
    %this.floatText(%text, %maxAge, %speed, %baseColor, %baseAlpha);
    return ;
}
function ClientCmdFloatText(%playerGhostID, %text, %style)
{
    %player = ServerConnection.resolveGhostID(%playerGhostID);
    if (!isObject(%player))
    {
        error(getScopeName() SPC "- could not resolve ghost" SPC %playerGhostID SPC %text);
        return ;
    }
    %player.floatTextSimple(%text, %style);
    return ;
}
function ThePointsFloaterHud::doTick(%this)
{
    cancel(%this.timerID);
    %this.timerID = "";
    %numChildren = %this.getCount();
    if (%numChildren < 1)
    {
        return ;
    }
    %n = %numChildren - 1;
    while (%n >= 0)
    {
        %ctrl = %this.getObject(%n);
        %ageNorm = %ctrl.age / %ctrl.maxAge;
        if (%ageNorm > 1)
        {
            %ctrl.delete();
        }
        else
        {
            %ctrl.age = %ctrl.age + 1;
            %x = getWord(%ctrl.position, 0);
            %y = getWord(%ctrl.position, 1);
            if (%ageNorm > 0.2)
            {
                %x = %x + ((%ctrl.speed * (%ageNorm - 0.2)) * 3);
                %y = %y - %ctrl.speed;
            }
            %ctrl.reposition(%x, %y);
            %alpha = (1 - %ageNorm) * %ctrl.baseAlpha;
            if (%ageNorm < 0.2)
            {
                %alpha = 1 - (%alpha * ((mSin(getSimTime() * 0.03) * 0.5) + 0.5));
            }
            %alpha1 = formatInt("%0.2X", %alpha * 255);
            %alpha2 = formatInt("%0.2X", 1 * 255);
            if (%ageNorm > 0.2)
            {
                %alpha2 = 00;
            }
            %colorTag = "<color:" @ %ctrl.BaseColor @ %alpha1 @ ">";
            %shadowTag = "<shadowcolor:" @ 000000 @ %alpha2 @ ">";
            %ctrl.setText(%shadowTag @ %colorTag @ %ctrl.baseText);
        }
        %n = %n - 1;
    }
    %this.timerID = %this.schedule(%this.tickPeriodMS, "doTick");
    return ;
}
function clientCmdUpdateVPoints(%newPoints, %notify)
{
    if (!isDefined("%notify"))
    {
        %notify = 1;
    }
    if (!(%newPoints $= ""))
    {
        BalanceUpdateSpecialEffects("vPoints", $Player::VPoints, %newPoints, %notify);
        $Player::VPoints = mFloor(%newPoints);
        updateAccountBalanceDisplays();
    }
    return ;
}
function clientCmdUpdateVBux(%newBux, %notify)
{
    if (!isDefined("%notify"))
    {
        %notify = 1;
    }
    if (!(%newBux $= ""))
    {
        BalanceUpdateSpecialEffects("vBux", $Player::VBux, %newBux, %notify);
        $Player::VBux = mFloor(%newBux);
        updateAccountBalanceDisplays();
    }
    return ;
}
function ClosetGUI_ToggleSku_Shops(%sku)
{
    %wordLoc = findWord($StoreSkusLayer, %sku);
    if (%wordLoc >= 0)
    {
        $StoreSkusLayer = removeWord($StoreSkusLayer, %wordLoc);
        StoreShortDescText.setBaseDesc("");
        StoreLongDescText.setBaseDesc("");
        ClosetTabs.updateAuthorWidget("");
    }
    else
    {
        $StoreSkusLayer = SkuManager.overlaySkus($StoreSkusLayer, %sku);
        StoreShortDescText.setBaseDesc(ClosetTabs.getShortSkuDesc(%sku));
        StoreLongDescText.setBaseDesc(ClosetTabs.getLongSkuDesc(%sku));
        ClosetTabs.updateAuthorWidget(%sku);
    }
    %count = getWordCount($StoreSkusLayer);
    %itemsToAdd = 0;
    %i = 0;
    while (%i < %count)
    {
        %sku2 = getWord($StoreSkusLayer, %i);
        if (!StoreShoppingList.containsSku(%sku2))
        {
            %itemsToAdd = 1;
            break;
        }
        %i = %i + 1;
    }
    StoreAddItemsButton.setActive(%itemsToAdd);
    return ;
}
