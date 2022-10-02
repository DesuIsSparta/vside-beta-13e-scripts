function ClosetTabs::fillClosetTab(%this)
{
    %theTab = %this.getTabWithName("CLOSET");
    if (!isObject(%theTab))
    {
        return ;
    }
    %itemLabel = new GuiTextCtrl()
    {
        profile = "ClosetTitleProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "181 64";
        extent = "35 20";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        text = "Item";
        maxLength = 255;
    };
    %theTab.add(%itemLabel);
    %itemPopup = new GuiPopUp2MenuCtrl(ClosetItemPopup)
    {
        profile = "ClosetPopupProfile";
        scrollProfile = "DottedScrollProfile";
        winProfile = "ClosetPopupWindowProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "180 84";
        extent = "150 24";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        maxLength = 255;
        maxPopupHeight = 200;
        allowReverse = 0;
    };
    %theTab.add(%itemPopup);
    %categoryList = "All Items" TAB "All Garments" TAB "All Accessories" TAB "Tops" TAB "Bottoms" TAB "Shoes" TAB "Ear" TAB "Neck" TAB "Waist" TAB "Skin" TAB "Hands" TAB "Bags" TAB "Glasses" TAB "Props" TAB "Misc" TAB "BodyMod" TAB "Badges" TAB "Tokens";
    %itemPopup.possibleCategoryList = %categoryList;
    %itemPopup.displayedCategoryList = %categoryList;
    %n = 0;
    while (%n < getFieldCount(%categoryList))
    {
        %category = getField(%categoryList, %n);
        if (Closet::skuListHasCategory($Player::inventory, %category))
        {
            %itemPopup.add(%category);
        }
        %n = %n + 1;
    }
    %brandLabel = new GuiTextCtrl()
    {
        profile = "ClosetTitleProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "27 64";
        extent = "104 20";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        text = "Brand";
        maxLength = 255;
    };
    %theTab.add(%brandLabel);
    %brandPopup = new GuiPopUp2MenuCtrl(ClosetBrandPopup)
    {
        profile = "ClosetPopupProfile";
        scrollProfile = "DottedScrollProfile";
        winProfile = "ClosetPopupWindowProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "26 84";
        extent = "150 24";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        maxLength = 255;
        maxPopupHeight = 200;
        allowReverse = 0;
    };
    %theTab.add(%brandPopup);
    %itemsFrame = new GuiControl(ClosetItemsFrame)
    {
        profile = "GuiDefaultProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "22 120";
        extent = "467 306";
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
        extent = "465 282";
        minExtent = "1 1";
        horizSizing = "right";
        vertSizing = "bottom";
        visible = 1;
        hScrollBar = "dynamic";
        vScrollBar = "dynamic";
        constantThumbHeight = 1;
        scrollMultiplier = 14;
    };
    %itemsScroll.bindClassName("ClosetItemsScroll");
    %theTab.itemsScroll = %itemsScroll;
    %thumbnails = new GuiArray2Ctrl(ClosetThumbnailsCloset)
    {
        class = "ClosetThumbnails";
        profile = "FocusableDefaultProfile";
        childrenClassName = "GuiMouseEventCtrl";
        childrenExtent = "109 138";
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
    %myOutfitsFrame = new GuiControl(ClosetMyOutfitsFrame)
    {
        profile = "GuiDefaultProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "5 430";
        extent = "498 112";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        bitmap = "";
    };
    %hangers = new GuiMouseEventCtrl(ClosetHangersFrame)
    {
        profile = "GuiDefaultProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "21 0";
        extent = "471 112";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
    };
    %myOutfitsFrame.add(%hangers);
    %myOutfitsFrame.hangers = %hangers;
    %xPos = 0;
    %ypos = 0;
    %i = 0;
    while (%i < $gClosetNumOutfits)
    {
        %objectView = new GuiObjectView("ClosetOutfitObjectView" @ %i)
        {
            profile = "GuiDefaultProfile";
            horizSizing = "right";
            vertSizing = "bottom";
            position = %xPos SPC %ypos;
            extent = "46 112";
            minExtent = "1 1";
            sluggishness = -1;
            CamSluggishness = 0.0000001;
            visible = 1;
        };
        if (isObject($player))
        {
            %objectView.setSimObject($player);
        }
        %objectView.setRotation(0.2, 0, 2.8);
        %objectView.setLookAtNudge("0.2 -1 0.15");
        %objectView.toonLineWidth = 2;
        %label = new GuiMLTextCtrl()
        {
            profile = "ETSNonModalProfile";
            position = "10 0";
            extent = "30 15";
            text = "<font:Arial:16><color:ffffff><b><outline><just:right>" SPC %i + 1 @ " ";
            visible = 0;
        };
        %button = new GuiBitmapButtonCtrl()
        {
            profile = "ClosetHangerButtonProfile";
            horizSizing = "right";
            vertSizing = "bottom";
            position = %xPos SPC %ypos;
            extent = "39 112";
            minExtent = "1 1";
            sluggishness = -1;
            visible = 1;
            command = "ClosetMyOutfitsFrame.hangerSelected(" @ %i @ ");";
            text = %i + 1;
            groupNum = $ClosetHangersGroup;
            buttonType = "RadioButton";
            bitmap = "platform/client/buttons/outfit";
            drawText = 0;
            index = %i;
        };
        %button.bindClassName("ClosetOutfitButton");
        %button.setName("ClosetOutfitButton" @ %i);
        %button.add(%label);
        %button.label = %label;
        %hangers.add(%objectView);
        %hangers.add(%button);
        %hangers.button[%i] = %button;
        %xPos = %xPos + 39;
        %i = %i + 1;
    }
    %theTab.add(%myOutfitsFrame);
    %whatYoureWearingContainer = new GuiControl(ClosetWhatYourWearingContainer)
    {
        profile = "ETSNonModalProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "689 207";
        extent = "245 281";
    };
    %whatYoureWearingPanel = %this.createWhatYourWearingPanel();
    %whatYoureWearingContainer.add(%whatYoureWearingPanel);
    %theTab.add(%whatYoureWearingContainer);
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
    %skus = getFilteredInventoryForSetDrawers();
    %brandPopup.update(%skus);
    %itemPopup.update(%skus);
    %brandPopup.SetSelected(0);
    %itemPopup.SetSelected(0);
    ClosetItemsFrame.update();
    %this.tabClosetInitialized = 1;
    return ;
}
function ClosetTabs::getOutfitObjectView(%this, %index)
{
    return "ClosetOutfitObjectView" @ %index;
}
function ClosetTabs::getOutfitButton(%this, %index)
{
    return "ClosetOutfitButton" @ %index;
}
function ClosetTabs::updateOutfitObjectView(%this, %index)
{
    %objectView = %this.getOutfitObjectView(%index);
    %name = ClosetMyOutfitsFrame.getOutfitNameForHanger(%index);
    %objectView.setSkus($ClosetSkusBody SPC $ClosetSkusOutfit[%name]);
    return ;
}
$gSwimsuitOutfitIndex = 6;
function ClosetTabs::doCopyOutfit(%this, %src, %dest)
{
    %title = $MsgCat::closet["MSG-COPY-OUTFIT-WARN","TITLE"];
    %body = $MsgCat::closet["MSG-COPY-OUTFIT-WARN","BODY"];
    %body = strreplace(%body, "[SRC]", %src + 1);
    %body = strreplace(%body, "[DST]", %dest + 1);
    if (%dest == $gSwimsuitOutfitIndex)
    {
        %body = %body NL "" NL $MsgCat::closet["MSG-COPY-OUTFIT-WARN","SWIM"];
    }
    MessageBoxYesNo(%title, %body, "ClosetTabs.doCopyOutfitReally(" @ %src @ ", " @ %dest @ ");", "");
    return ;
}
function ClosetTabs::doCopyOutfitReally(%this, %src, %dest)
{
    %srcName = ClosetMyOutfitsFrame.getOutfitNameForHanger(%src);
    %destName = ClosetMyOutfitsFrame.getOutfitNameForHanger(%dest);
    $ClosetSkusOutfit[%destName] = $ClosetSkusOutfit[%srcName] ;
    ClosetTabs.updateOutfitObjectView(%dest);
    ClosetTabs.getOutfitButton(%dest).performClick();
    return ;
}
function ClosetTabs::doSwapOutfits(%this, %src, %dest)
{
    if ((%src == $gSwimsuitOutfitIndex) && (%dest == $gSwimsuitOutfitIndex))
    {
        %title = $MsgCat::closet["MSG-SWAP-OUTFIT-WARN","TITLE"];
        %body = $MsgCat::closet["MSG-SWAP-OUTFIT-WARN","BODY"];
        MessageBoxYesNo(%title, %body, "ClosetTabs.doSwapOutfitsReally(" @ %src @ ", " @ %dest @ ");", "");
    }
    else
    {
        %this.doSwapOutfitsReally(%src, %dest);
    }
    return ;
}
function ClosetTabs::doSwapOutfitsReally(%this, %src, %dest)
{
    %srcName = ClosetMyOutfitsFrame.getOutfitNameForHanger(%src);
    %destName = ClosetMyOutfitsFrame.getOutfitNameForHanger(%dest);
    %tmp = $ClosetSkusOutfit[%srcName];
    $ClosetSkusOutfit[%srcName] = $ClosetSkusOutfit[%destName] ;
    $ClosetSkusOutfit[%destName] = %tmp ;
    ClosetTabs.updateOutfitObjectView(%src);
    ClosetTabs.updateOutfitObjectView(%dest);
    ClosetTabs.getOutfitButton(%dest).performClick();
    return ;
}
function ClosetOutfitButton::onMouseDown(%this)
{
    %this.origin = Canvas.getCursorPos();
    return ;
}
function ClosetOutfitButton::onMouseDragged(%this, %modifier)
{
    %vec = VectorSub(%this.origin, Canvas.getCursorPos());
    if (VectorLenSquared(%vec) < (10 * 10))
    {
        return 0;
    }
    %mask = $Platform $= "macos" ? $EventModifier::ALT : $EventModifier::CTRL;
    %this.operation = %modifier & %mask ? "COPY" : "SWAP";
    %this.setAsDragControl(1);
    return 1;
}
function ClosetOutfitButton::makeVisualClone(%this)
{
    %objectView = new GuiObjectView()
    {
        profile = "GuiDefaultProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "0 0";
        extent = "46 112";
        minExtent = "1 1";
        sluggishness = -1;
        CamSluggishness = 0.0000001;
        visible = 1;
    };
    if (isObject($player))
    {
        %objectView.setSimObject($player);
    }
    %objectView.setRotation(0.2, 0, 2.8);
    %objectView.setLookAtNudge("0.2 -1 0.15");
    %objectView.toonLineWidth = 2;
    %objectView.setSkus($ClosetSkusBody SPC $ClosetSkusOutfit[ClosetMyOutfitsFrame.getOutfitNameForHanger(%this.index)]);
    %label = new GuiMLTextCtrl()
    {
        profile = "ETSNonModalProfile";
        position = "10 0";
        extent = "30 15";
        text = "<font:Arial:16><color:ffffff><b><outline><just:right>" SPC %this.index + 1 @ " ";
    };
    if (%this.operation $= "")
    {
        %this.operation = "SWAP";
    }
    %operationIcon = new GuiBitmapCtrl()
    {
        profile = "ETSNonModalProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "20 76";
        extent = "20 20";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 0;
        bitmap = "platform/client/ui/" @ %this.operation;
    };
    %clone = new GuiBitmapButtonCtrl()
    {
        position = "0 0";
        extent = %this.getExtent();
        bitmap = %this.bitmap;
    };
    %clone.add(%objectView);
    %clone.add(%label);
    %clone.add(%operationIcon);
    %clone.operationIcon = %operationIcon;
    %this.clone = %clone;
    return %clone;
}
function ClosetOutfitButton::dragAndDropCtrl(%this, %make)
{
    if (%make)
    {
        %this.operation = "COPY";
    }
    else
    {
        %this.operation = "SWAP";
    }
    %this.clone.operationIcon.setBitmap("platform/client/ui/" @ %this.operation);
    return ;
}
function ClosetOutfitButton::onDragSet(%this)
{
    %this.depressed = 1;
    %i = 0;
    while (isObject(%button = ClosetTabs.getOutfitButton(%i)))
    {
        %button.mouseOver = 0;
        %i = %i + 1;
    }
    Canvas.centerDragHiliteAroundCursor();
    %this.clone.mouseOver = 1;
    return ;
}
function ClosetOutfitButton::onDragReleased(%this)
{
    %this.depressed = 0;
    return ;
}
function ClosetOutfitButton::onDragAndDropEnter(%this, %dragCtrl)
{
    if (findWord(%dragCtrl.getNamespaceList(), "ClosetOutfitButton") == -1)
    {
        return ;
    }
    if (%this != %dragCtrl)
    {
        hiliteControl(%this, 1);
        %this.depressed = 1;
        %dragCtrl.clone.operationIcon.setVisible(1);
        %this.label.setVisible(1);
    }
    return ;
}
function ClosetOutfitButton::onDragAndDropLeave(%this, %dragCtrl)
{
    if (%this != %dragCtrl)
    {
        hiliteControl(0);
        %this.depressed = 0;
        %dragCtrl.clone.operationIcon.setVisible(0);
        %this.label.setVisible(0);
    }
    return ;
}
function ClosetOutfitButton::onDragAndDropDrop(%this, %dragCtrl, %unused)
{
    if (findWord(%dragCtrl.getNamespaceList(), "ClosetOutfitButton") == -1)
    {
        return 0;
    }
    if (%this == %dragCtrl)
    {
        return 0;
    }
    if (%dragCtrl.operation $= "COPY")
    {
        ClosetTabs.doCopyOutfit(%dragCtrl.index, %this.index);
    }
    else
    {
        if (%dragCtrl.operation $= "SWAP")
        {
            ClosetTabs.doSwapOutfits(%dragCtrl.index, %this.index);
        }
    }
    return 1;
}
function ClosetBrandPopup::update(%this, %skus)
{
    %this.clear();
    %i = 0;
    while (%i < getFieldCount($gClosetBrands))
    {
        %brand = getField($gClosetBrands, %i);
        if (SkuManager.skusHaveBrands(%skus, $gClosetBrandsIntrnl[%brand] @ " "))
        {
            %this.add(%brand);
        }
        %i = %i + 1;
    }
}

function ClosetItemPopup::update(%this, %skus)
{
    NoItemInBrandNameLabel.setText("");
    %prevSelText = %this.getTextById(%this.GetSelected());
    if (!(%skus $= ""))
    {
        %newList = "";
        %n = 0;
        while (%n < getFieldCount(%this.possibleCategoryList))
        {
            %category = getField(%this.possibleCategoryList, %n);
            if (Closet::skuListHasCategory(%skus, %category))
            {
                %newList = %newList TAB %category;
            }
            %n = %n + 1;
        }
        %newList = trim(%newList);
        if (%newList $= %this.displayedCategoryList)
        {
            return ;
        }
    }
    else
    {
        %newList = getField(%this.possibleCategoryList, 0);
    }
    %this.displayedCategoryList = %newList;
    %prevSelText = %this.getTextById(%this.GetSelected());
    %this.clear();
    %n = 0;
    while (%n < getFieldCount(%newList))
    {
        %this.add(getField(%newList, %n));
        %n = %n + 1;
    }
    %newSel = %this.findText(%prevSelText);
    if (%newSel < 0)
    {
        %this.SetSelected(0);
        if (ClosetBrandPopup.getText() $= "All")
        {
            %brandString = "";
        }
        else
        {
            if (ClosetBrandPopup.getText() $= "Basic")
            {
                %brandString = " basic";
            }
            else
            {
                %brandString = " " @ ClosetBrandPopup.getText() SPC "brand";
            }
        }
        %categoryString = strlwr(firstWord(%prevSelText) $= "All" ? restWords(%prevSelText) : %prevSelText);
        if (%categoryString $= "")
        {
            %categoryString = "clothes";
        }
        %msg = $MsgCat::closet["H-NO-BRAND-ITEMS1"] @ %brandString SPC %categoryString @ $MsgCat::closet["H-NO-BRAND-ITEMS2"] @ %brandString SPC $MsgCat::closet["H-NO-BRAND-ITEMS3"];
        NoItemInBrandNameLabel.setText(%msg);
    }
    else
    {
        %this.setText(%prevSelText);
    }
    return ;
}
function ClosetItemsFrame::update(%this)
{
    if (ClosetTabs.getCurrentTab().name $= "CLOSET")
    {
        %this.thumbnails.setDrawers("");
    }
    return ;
}
function ClosetMyOutfitsFrame::getOutfitNameForHanger(%this, %hanger)
{
    return getWord($Player::HangerNames[$player.getGender()], %hanger);
}
function ClosetMyOutfitsFrame::hangerSelected(%this, %hanger)
{
    %this.currentHanger = %hanger;
    $ClosetOutfitName = %this.getOutfitNameForHanger(%hanger);
    ClosetItemsFrame.update();
    if (ClosetTabs.getCurrentTab().name $= "CLOSET")
    {
        ClosetTabs.getTabWithName("CLOSET").thumbnails.setSelectedThumbs();
    }
    ClosetGui.updateVisibleAvatar();
    return ;
}
function ClosetItemPopup::onSelect(%this, %unused, %entries)
{
    if (ClosetItemsFrame.category $= %entries)
    {
        return ;
    }
    ClosetItemsFrame.category = %entries;
    if ($gUpdatingClosetItemPopupFromThumbnailsSetDrawers)
    {
        $gUpdatingClosetItemPopupFromThumbnailsSetDrawers = 0;
        return ;
    }
    else
    {
        if (ClosetTabs.tabClosetInitialized)
        {
            ClosetItemsFrame.update();
        }
    }
    ClosetThumbnailsCloset.getParent().scrollToTop();
    return ;
}
function ClosetBrandPopup::onSelect(%this, %unused, %entries)
{
    if (ClosetItemsFrame.brand $= %entries)
    {
        return ;
    }
    ClosetItemsFrame.brand = %entries;
    if (ClosetTabs.tabClosetInitialized)
    {
        ClosetItemsFrame.update();
    }
    ClosetThumbnailsCloset.getParent().scrollToTop();
    return ;
}
function ClosetWhatYoureWearingList::onCreatedChild(%this, %child)
{
    %currentTabName = ClosetTabs.getCurrentTab().name;
    %background = new GuiControl()
    {
        profile = "ClosetLtBackgroundProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "0 0";
        extent = "228 36";
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
        extent = "228 36";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 0;
    };
    %itemDesc = new GuiMLTextCtrl()
    {
        profile = "ClosetSmallLinkProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "5 2";
        extent = "206 15";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        text = "";
        groupNum = -1;
        command = "";
        drawText = 1;
    };
    %itemDesc.bindClassName(ClosetWhatYoureWearingButton);
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
        command = "ClosetThumbnailsCloset.scroll.scrollToSku(" @ %child.getId() @ ".sku); ClosetGui.toggleSku(" @ %child.getId() @ ".sku);";
        text = "";
        groupNum = -1;
        buttonType = "PushButton";
        bitmap = "platform/client/buttons/closet_close";
        drawText = 0;
    };
    %ugcStatusIcon = new GuiBitmapCtrl()
    {
        profile = "ETSNonModalProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "211 19";
        extent = "16 16";
        modulationColor = "255 255 255 100";
    };
    %expiringIcon = new GuiBitmapCtrl()
    {
        profile = "ETSNonModalProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "88 16";
        extent = "20 20";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        bitmap = "";
        modulationColor = "255 255 255 100";
    };
    %authorText = new GuiMLTextCtrl()
    {
        profile = "ClosetSmallLinkProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "80 18";
        extent = 146 - %currentTabName $= "MY DESIGNS" ? 18 : 0 SPC 15;
    };
    %child.add(%background);
    %child.add(%hilite);
    %child.add(%ugcStatusIcon);
    %child.add(%itemDesc);
    %child.add(%expiringIcon);
    %child.add(%closeBox);
    %child.add(%authorText);
    %child.background = %background;
    %child.hiliteCtrl = %hilite;
    %child.desc = %itemDesc;
    %child.ugcStatusIcon = %ugcStatusIcon;
    %child.expiringIcon = %expiringIcon;
    %child.closeBox = %closeBox;
    %child.authorText = %authorText;
    %child.sku = 0;
    if (!(getWord(%child.getNamespaceList(), 0) $= "ClosetWhatYoureWearingItem"))
    {
        %child.bindClassName("ClosetWhatYoureWearingItem");
    }
    return ;
}
$gNoSkuList = "400 850 875 900 950 5400 5850 5875 5900 5950 5980";
function ClosetWhatYoureWearingList::addSku(%this, %sku)
{
    if (findWord($gNoSkuList, %sku) != -1)
    {
        return ;
    }
    %drawerAction = "";
    %child = %this.addChild();
    %child.sku = %sku;
    %si = SkuManager.findBySku(%sku);
    if (!(SkuManager.getPropSkus(%sku) $= ""))
    {
        if (InstrumentRegistryClient.isInstrumentSku(%sku))
        {
            %drawerAction = "  (no animation)";
        }
        else
        {
            if (ClosetGui.isDoingPropAction)
            {
                %drawerAction = "  <a:gamelink stopPropAction>stop animation</a>";
            }
            else
            {
                %drawerAction = "  <a:gamelink startPropAction>start animation</a>";
            }
        }
    }
    %child.desc.setText("<a:gamelink " @ %sku @ ">" @ %si.descShrt @ "</a>" @ "<br><spush><font:Arial:12><color:00000099>" @ %si.getUserFacingDrawerName() @ %drawerAction @ "<spop>");
    if (!(%si.author $= ""))
    {
        %playerEncoded = urlEncode(stripUnprintables(%si.author));
        %profileURL = $Net::ProfileURL @ %playerEncoded;
        %child.authorText.setText("<just:right><font:Arial:12><color:00000044><linkcolor:00000066>" @ "by <a:gamelink " @ %profileURL @ ">" @ %si.author @ "</a>");
    }
    else
    {
        %child.authorText.setText("");
    }
    if (!(%si.expireTime $= ""))
    {
        %child.expiringIcon.setBitmap("platform/client/ui/expiring_icon");
        %child.expiringIcon.setVisible(1);
    }
    else
    {
        %child.expiringIcon.setVisible(0);
    }
    %currentTabName = ClosetTabs.getCurrentTab().name;
    if (%currentTabName $= "MY DESIGNS")
    {
        %child.ugcStatusIcon.setBitmap(ClosetGui_MyShop_GetSkuUGCStatusIcon(%sku));
        %child.ugcStatusIcon.setVisible(1);
    }
    else
    {
        %child.ugcStatusIcon.setVisible(0);
    }
    if (%this.filterByRemovable)
    {
        %showRemoveButton = SkuManager.isOptionalDrawer(%si.drwrName);
    }
    else
    {
        %showRemoveButton = 1;
    }
    %child.closeBox.setActive(%showRemoveButton);
    %this.hiliteCell(0, %this.getCount() - 1);
    %this.reseatChildren();
    return ;
}
function ClosetWhatYoureWearingList::setSkus(%this, %skus)
{
    %propSku = SkuManager.getFirstPropSku($ClosetSkusOutfit[$ClosetOutfitName]);
    if ((%propSku $= "") && (%this.lastPropSku != %propSku))
    {
        stopPropAction();
    }
    %this.lastPropSku = %propSku;
    if (%propSku $= "")
    {
        $player.setGenre("p");
    }
    else
    {
        %instrument = InstrumentRegistryClient.getInstrumentBySku(%propSku);
        if (%instrument $= "")
        {
            $player.setGenre("p");
        }
        else
        {
            $player.setGenre(%instrument.genre);
        }
    }
    %this.setNumChildren(0);
    if (getWordCount(%skus) == 0)
    {
        ClosetWhatYoureWearingNone.setTextWithStyle("(wearing none)");
        if (isObject(MyShopCopyToOutfitText))
        {
            MyShopCopyToOutfitText.setVisible(0);
        }
    }
    else
    {
        ClosetWhatYoureWearingNone.setTextWithStyle("");
        if (isObject(MyShopCopyToOutfitText))
        {
            MyShopCopyToOutfitText.setVisible(1);
        }
    }
    %skus = SkuManager.sortSkusByDrawer(%skus);
    %count = getWordCount(%skus);
    %i = 0;
    while (%i < %count)
    {
        %this.addSku(getWord(%skus, %i));
        %i = %i + 1;
    }
    %this.skus = %skus;
    return ;
}
function ClosetWhatYoureWearingList::refresh(%this, %skus)
{
    ClosetWhatYoureWearingList.setSkus(%skus);
    return ;
}
function ClosetWhatYoureWearingButton::onURL(%this, %url)
{
    if (getWord(%url, 0) $= "gamelink")
    {
        %url = getWords(%url, 1);
    }
    if (%url $= "startPropAction")
    {
        ClosetMainObjectView.zoomToSKU("");
        doPropAction();
        ClosetWhatYoureWearingList.refresh($ClosetSkusOutfit[$ClosetOutfitName]);
    }
    else
    {
        if (%url $= "stopPropAction")
        {
            stopPropAction();
            ClosetWhatYoureWearingList.refresh($ClosetSkusOutfit[$ClosetOutfitName]);
        }
        else
        {
            if (ClosetTabs.getCurrentTab().name $= "CLOSET")
            {
                ClosetThumbnailsCloset.scroll.scrollToSku(%url);
            }
            else
            {
                if (ClosetTabs.getCurrentTab().name $= "MY DESIGNS")
                {
                    ClosetGui_MyShop_ToggleCurrentSku(%url);
                }
            }
        }
    }
    return ;
}
function ClosetGUI_ToggleSku_Closet(%sku)
{
    %drawer = SkuManager.findBySku(%sku).drwrName;
    %removable = SkuManager.isOptionalDrawer(%drawer);
    if (SkuManager.isBodySku(%sku))
    {
        %wordLoc = findWord($ClosetSkusBody, %sku);
        if (%wordLoc >= 0)
        {
            if (%removable)
            {
                $ClosetSkusBody = removeWord($ClosetSkusBody, %wordLoc);
            }
        }
        else
        {
            $ClosetSkusBody = SkuManager.overlaySkus($ClosetSkusBody, %sku);
        }
    }
    else
    {
        if (SkuManager.isOutfitSku(%sku))
        {
            %wordLoc = findWord($ClosetSkusOutfit[$ClosetOutfitName], %sku);
            if (%wordLoc >= 0)
            {
                if (%removable)
                {
                    $ClosetSkusOutfit[$ClosetOutfitName] = removeWord($ClosetSkusOutfit[$ClosetOutfitName], %wordLoc) ;
                }
            }
            else
            {
                $ClosetSkusOutfit[$ClosetOutfitName] = SkuManager.overlaySkus($ClosetSkusOutfit[$ClosetOutfitName], %sku) ;
            }
            %outfitNum = findWord($Player::HangerNames[$player.getGender()], $ClosetOutfitName);
            %objectView = ClosetTabs.getOutfitObjectView(%outfitNum);
            %objectView.setSkus($ClosetSkusBody SPC $ClosetSkusOutfit[$ClosetOutfitName]);
        }
    }
    return ;
}
