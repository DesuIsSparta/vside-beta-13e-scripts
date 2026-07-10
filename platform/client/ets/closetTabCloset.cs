function ClosetTabs::fillClosetTab(%this) {
    %theTab = "CLOSET".getTabWithName(%this);
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
    new GuiMLTextCtrl(NoItemInBrandNameLabel) {
        profile = "ClosetLeftInfoProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "26 111";
        extent = "457 17";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        allowColorChars = 0;
        maxChars = -1;
        text = "";
    };.add(%theTab);
    %itemLabel = new GuiTextCtrl("") {
        profile = 0 @ "ClosetTitleProfile";
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
    %itemLabel.add(%theTab);
    %itemPopup = new GuiPopUp2MenuCtrl(ClosetItemPopup) {
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
    %itemPopup.add(%theTab);
    %categoryList = "All Items" @ "\t" @ "All Garments" @ "\t" @ "All Accessories" @ "\t" @ "Tops" @ "\t" @ "Bottoms" @ "\t" @ "Shoes" @ "\t" @ "Ear" @ "\t" @ "Neck" @ "\t" @ "Waist" @ "\t" @ "Skin" @ "\t" @ "Hands" @ "\t" @ "Bags" @ "\t" @ "Glasses" @ "\t" @ "Props" @ "\t" @ "Misc" @ "\t" @ "BodyMod" @ "\t" @ "Badges" @ "\t" @ "Tokens";
    %itemPopup.possibleCategoryList = %categoryList;
    %itemPopup.displayedCategoryList = %categoryList;
    %n = 0;
    while ((%n < getFieldCount(%categoryList))) {
        %category = getField(%categoryList, %n);
        if (Closet::skuListHasCategory($Player::inventory, %category)) {
            %category.add(%itemPopup);
        }
        %n = (%n + 1.0);
    }
    (%n < getFieldCount(%categoryList));
    %brandLabel = new GuiTextCtrl("") {
        profile = 0 @ "ClosetTitleProfile";
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
    %brandLabel.add(%theTab);
    %brandPopup = new GuiPopUp2MenuCtrl(ClosetBrandPopup) {
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
    %brandPopup.add(%theTab);
    %itemsFrame = new GuiControl(ClosetItemsFrame) {
        profile = "GuiDefaultProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "22 120";
        extent = "467 306";
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
    "ClosetItemsScroll".bindClassName(%itemsScroll);
    %theTab.itemsScroll = %itemsScroll;
    %thumbnails = new GuiArray2Ctrl(ClosetThumbnailsCloset) {
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
    %thumbnails.add(%itemsScroll);
    %itemsScroll.thumbnails = %thumbnails;
    %itemsScroll.add(%itemsFrame);
    %itemsFrame.thumbnails = %thumbnails;
    %itemsFrame.add(%theTab);
    %theTab.thumbnails = %thumbnails;
    new GuiMLTextCtrl(ClosetShortDescText) {
        profile = "ClosetLeftInfoProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "692 84";
        extent = "242 25";
        lineSpacing = -(3.0);
    };.add(%theTab);
    new GuiMLTextCtrl(ClosetLongDescText) {
        profile = "ClosetLeftInfoProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "692 106";
        extent = "173 32";
        lineSpacing = -(3.0);
    };.add(%theTab);
    %myOutfitsFrame = new GuiControl(ClosetMyOutfitsFrame) {
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
    new GuiBitmapCtrl("") {
        profile = "GuiDefaultProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "0 10";
        extent = "15 56";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        bitmap = "platform/client/ui/outfits";
    };
    %hangers = new GuiMouseEventCtrl(ClosetHangersFrame) {
        profile = "GuiDefaultProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "21 0";
        extent = "471 112";
        minExtent = "1 1";
        sluggishness = -(1.0);
        visible = 1;
    };
    %hangers.add(%myOutfitsFrame);
    %myOutfitsFrame.hangers = %hangers;
    %xPos = 0;
    %ypos = 0;
    %i = 0;
    while ((%i < $gClosetNumOutfits)) {
        %objectView = new GuiObjectView("ClosetOutfitObjectView" @ %i) {
            profile = 0 @ "GuiDefaultProfile";
            horizSizing = "right";
            vertSizing = "bottom";
            position = %xPos @ " " @ %ypos;
            extent = "46 112";
            minExtent = "1 1";
            sluggishness = -1;
            CamSluggishness = 0.0000001;
            visible = 1;
        };
        if (isObject($player)) {
            $player.setSimObject(%objectView);
        }
        2.8.setRotation(%objectView, 0.2, 0);
        "0.2 -1 0.15".setLookAtNudge(%objectView);
        %objectView.toonLineWidth = 2;
        %label = new GuiMLTextCtrl("") {
            profile = 0 @ "ETSNonModalProfile";
            position = "10 0";
            extent = "30 15";
            text = "<font:Arial:16><color:ffffff><b><outline><just:right>" @ " " @ (%i + 1.0) @ " ";
            visible = 0;
        };
        %button = new GuiBitmapButtonCtrl("") {
            profile = 0 @ "ClosetHangerButtonProfile";
            horizSizing = "right";
            vertSizing = "bottom";
            position = %xPos @ " " @ %ypos;
            extent = "39 112";
            minExtent = "1 1";
            sluggishness = -1;
            visible = 1;
            command = "ClosetMyOutfitsFrame.hangerSelected(" @ %i @ ");";
            text = (%i + 1.0);
            groupNum = $ClosetHangersGroup;
            buttonType = "RadioButton";
            bitmap = "platform/client/buttons/outfit";
            drawText = 0;
            index = %i;
        };
        "ClosetOutfitButton".bindClassName(%button);
        "ClosetOutfitButton" @ %i.setName(%button);
        %label.add(%button);
        %button.label = %label;
        %objectView.add(%hangers);
        %button.add(%hangers);
        %hangers.button = %button @ %i;
        %xPos = (%xPos + 39.0);
        %i = (%i + 1.0);
    }
    %myOutfitsFrame.add(%theTab);
    %whatYoureWearingContainer = new GuiControl(ClosetWhatYourWearingContainer) {
        profile = (%i < $gClosetNumOutfits) @ "ETSNonModalProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "689 207";
        extent = "245 281";
    };
    %whatYoureWearingPanel = %this.createWhatYourWearingPanel();
    %whatYoureWearingPanel.add(%whatYoureWearingContainer);
    %whatYoureWearingContainer.add(%theTab);
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
    %skus = getFilteredInventoryForSetDrawers();
    %skus.update(%brandPopup);
    %skus.update(%itemPopup);
    0.SetSelected(%brandPopup);
    0.SetSelected(%itemPopup);
    ClosetItemsFrame.update();
    %this.tabClosetInitialized = 1;
};
function ClosetTabs::getOutfitObjectView(%this, %index) {
    return "ClosetOutfitObjectView" @ %index;
};
function ClosetTabs::getOutfitButton(%this, %index) {
    return "ClosetOutfitButton" @ %index;
};
function ClosetTabs::updateOutfitObjectView(%this, %index) {
    %objectView = %index.getOutfitObjectView(%this);
    %name = %index.getOutfitNameForHanger(ClosetMyOutfitsFrame);
    $ClosetSkusBody @ " " @ %name[$ClosetSkusOutfit @ %name].setSkus(%objectView);
};
$gSwimsuitOutfitIndex = 6;
function ClosetTabs::doCopyOutfit(%this, %src, %dest) {
    %title = $gSwimsuitOutfitIndex[$MsgCat::closet TAB "MSG-COPY-OUTFIT-WARN" @ "TITLE"];
    %body = %title[$MsgCat::closet TAB "MSG-COPY-OUTFIT-WARN" @ "BODY"];
    %body = strreplace(%body, "[SRC]", (%src + 1.0));
    %body = strreplace(%body, "[DST]", (%dest + 1.0));
    if ((%dest == $gSwimsuitOutfitIndex)) {
        %body = %body[%body @ "\n" @ "" @ "\n" @ $MsgCat::closet TAB "MSG-COPY-OUTFIT-WARN" @ "SWIM"];
    }
    MessageBoxYesNo(%title, %body, "ClosetTabs.doCopyOutfitReally(" @ %src @ ", " @ %dest @ ");", "");
};
function ClosetTabs::doCopyOutfitReally(%this, %src, %dest) {
    %srcName = %src.getOutfitNameForHanger(ClosetMyOutfitsFrame);
    %destName = %dest.getOutfitNameForHanger(ClosetMyOutfitsFrame);
    %destName[$ClosetSkusOutfit @ %destName] = %srcName[$ClosetSkusOutfit @ %srcName];
    %dest.updateOutfitObjectView(ClosetTabs);
    %dest.getOutfitButton(ClosetTabs).performClick();
};
function ClosetTabs::doSwapOutfits(%this, %src, %dest) {
    if ((%src == $gSwimsuitOutfitIndex)) {
    }
    if ((%dest == $gSwimsuitOutfitIndex)) {
        %title = %dest[$MsgCat::closet TAB "MSG-SWAP-OUTFIT-WARN" @ "TITLE"];
        %body = %title[$MsgCat::closet TAB "MSG-SWAP-OUTFIT-WARN" @ "BODY"];
        MessageBoxYesNo(%title, %body, "ClosetTabs.doSwapOutfitsReally(" @ %src @ ", " @ %dest @ ");", "");
    }
    %dest.doSwapOutfitsReally(%this, %src);
};
function ClosetTabs::doSwapOutfitsReally(%this, %src, %dest) {
    %srcName = %src.getOutfitNameForHanger(ClosetMyOutfitsFrame);
    %destName = %dest.getOutfitNameForHanger(ClosetMyOutfitsFrame);
    %tmp = %srcName[$ClosetSkusOutfit @ %srcName];
    %srcName[$ClosetSkusOutfit @ %srcName] = %destName[$ClosetSkusOutfit @ %destName];
    %destName[$ClosetSkusOutfit @ %destName] = %tmp;
    %src.updateOutfitObjectView(ClosetTabs);
    %dest.updateOutfitObjectView(ClosetTabs);
    %dest.getOutfitButton(ClosetTabs).performClick();
};
function ClosetOutfitButton::onMouseDown(%this) {
    %this.origin = Canvas.getCursorPos();
};
function ClosetOutfitButton::onMouseDragged(%this, %modifier) {
    %vec = VectorSub(%this.origin, Canvas.getCursorPos());
    if ((VectorLenSquared(%vec) < (10.0 * 10.0))) {
        return 0;
    }
    if (($Platform $= "macos")) {
    }
    %mask = $EventModifier::CTRL;
    $EventModifier::ALT;
    %this.operation = (%modifier & %mask) ? "COPY" : "SWAP";
    1.setAsDragControl(%this);
    return 1;
};
function ClosetOutfitButton::makeVisualClone(%this) {
    %objectView = new GuiObjectView("") {
        profile = 0 @ "GuiDefaultProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "0 0";
        extent = "46 112";
        minExtent = "1 1";
        sluggishness = -1;
        CamSluggishness = 0.0000001;
        visible = 1;
    };
    if (isObject($player)) {
        $player.setSimObject(%objectView);
    }
    2.8.setRotation(%objectView, 0.2, 0);
    "0.2 -1 0.15".setLookAtNudge(%objectView);
    %objectView.toonLineWidth = 2;
    $ClosetSkusBody @ " ".setSkus(%objectView);
    %label = new GuiMLTextCtrl("") {
        profile = 0 @ "ETSNonModalProfile";
        position = "10 0";
        extent = "30 15";
        text = "<font:Arial:16><color:ffffff><b><outline><just:right>" @ " " @ (%this.index + 1.0) @ " ";
    };
    if ((%this.operation $= "")) {
        %this.operation = "SWAP";
    }
    %operationIcon = new GuiBitmapCtrl("") {
        profile = 0 @ "ETSNonModalProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "20 76";
        extent = "20 20";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 0;
        bitmap = "platform/client/ui/" @ %this.operation;
    };
    %clone = new GuiBitmapButtonCtrl("") {
        position = 0 @ "0 0";
        extent = %this.getExtent();
        bitmap = %this.bitmap;
    };
    %objectView.add(%clone);
    %label.add(%clone);
    %operationIcon.add(%clone);
    %clone.operationIcon = %operationIcon;
    %this.clone = %clone;
    return %clone;
};
function ClosetOutfitButton::dragAndDropCtrl(%this, %make) {
    if (%make) {
        %this.operation = "COPY";
    }
    %this.operation = "SWAP";
    "platform/client/ui/" @ %this.operation.setBitmap(%this.clone.operationIcon);
};
function ClosetOutfitButton::onDragSet(%this) {
    %this.depressed = 1;
    %i = 0;
    %button = %i.getOutfitButton(ClosetTabs);
    while (isObject()) {
        %button.mouseOver = 0;
        %i = (%i + 1.0);
        %button = %i.getOutfitButton(ClosetTabs);
    }
    Canvas.centerDragHiliteAroundCursor();
    %this.clone.mouseOver = isObject() @ 1;
};
function ClosetOutfitButton::onDragReleased(%this) {
    %this.depressed = 0;
};
function ClosetOutfitButton::onDragAndDropEnter(%this, %dragCtrl) {
    if ((findWord(%dragCtrl.getNamespaceList(), "ClosetOutfitButton") == -(1.0))) {
        return;
    }
    if ((%this != %dragCtrl)) {
        hiliteControl(%this, 1);
        %this.depressed = 1;
        1.setVisible(%dragCtrl.clone.operationIcon);
        1.setVisible(%this.label);
    }
};
function ClosetOutfitButton::onDragAndDropLeave(%this, %dragCtrl) {
    if ((%this != %dragCtrl)) {
        hiliteControl(0);
        %this.depressed = 0;
        0.setVisible(%dragCtrl.clone.operationIcon);
        0.setVisible(%this.label);
    }
};
function ClosetOutfitButton::onDragAndDropDrop(%this, %dragCtrl, %unused) {
    if ((findWord(%dragCtrl.getNamespaceList(), "ClosetOutfitButton") == -(1.0))) {
        return 0;
    }
    if ((%this == %dragCtrl)) {
        return 0;
    }
    if ((%dragCtrl.operation $= "COPY")) {
        %this.index.doCopyOutfit(ClosetTabs, %dragCtrl.index);
    }
    if ((%dragCtrl.operation $= "SWAP")) {
        %this.index.doSwapOutfits(ClosetTabs, %dragCtrl.index);
    }
    return 1;
};
function ClosetBrandPopup::update(%this, %skus) {
    %this.clear();
    %i = 0;
    while ((%i < getFieldCount($gClosetBrands))) {
        %brand = getField($gClosetBrands, %i);
        if (%brand[$gClosetBrandsIntrnl @ %brand] @ " ".skusHaveBrands(SkuManager, %skus)) {
            %brand.add(%this);
        }
        %i = (%i + 1.0);
    }
};
function ClosetItemPopup::update(%this, %skus) {
    "".setText(NoItemInBrandNameLabel);
    %prevSelText = %this.GetSelected().getTextById(%this);
    if (!(%skus $= "")) {
        %newList = "";
        %n = 0;
        while ((%n < getFieldCount(%this.possibleCategoryList))) {
            %category = getField(%this.possibleCategoryList, %n);
            if (Closet::skuListHasCategory(%skus, %category)) {
                %newList = %newList @ "\t" @ %category;
            }
            %n = (%n + 1.0);
        }
        %newList = trim(%newList);
        (%n < getFieldCount(%this.possibleCategoryList));
        if ((%newList $= %this.displayedCategoryList)) {
            return;
        }
    }
    %newList = getField(%this.possibleCategoryList, 0);
    %this.displayedCategoryList = %newList;
    %prevSelText = %this.GetSelected().getTextById(%this);
    %this.clear();
    %n = 0;
    while ((%n < getFieldCount(%newList))) {
        getField(%newList, %n).add(%this);
        %n = (%n + 1.0);
    }
    %newSel = %prevSelText.findText(%this);
    (%n < getFieldCount(%newList));
    if ((%newSel < 0.0)) {
        0.SetSelected(%this);
        if ((ClosetBrandPopup.getText() $= "All")) {
            %brandString = "";
        }
        if ((ClosetBrandPopup.getText() $= "Basic")) {
            %brandString = " basic";
        }
        %brandString = " " @ ClosetBrandPopup.getText() @ " " @ "brand";
        if ((firstWord(%prevSelText) $= "All")) {
        }
        %categoryString = strlwr(restWords(%prevSelText), %prevSelText);
        if ((%categoryString $= "")) {
            %categoryString = "clothes";
        }
        %msg = %categoryString[$MsgCat::closet @ "H-NO-BRAND-ITEMS1"] @ %brandString @ " " @ %categoryString @ %categoryString[$MsgCat::closet @ "H-NO-BRAND-ITEMS2"] @ %brandString @ " " @ %brandString[$MsgCat::closet @ "H-NO-BRAND-ITEMS3"];
        %msg.setText(NoItemInBrandNameLabel);
    }
    %prevSelText.setText(%this);
};
function ClosetItemsFrame::update(%this) {
    if ((ClosetTabs.getCurrentTab().name $= "CLOSET")) {
        "".setDrawers(%this.thumbnails);
    }
};
function ClosetMyOutfitsFrame::getOutfitNameForHanger(%this, %hanger) {
    return getWord(, %hanger);
};
function ClosetMyOutfitsFrame::hangerSelected(%this, %hanger) {
    %this.currentHanger = %hanger;
    $ClosetOutfitName = %hanger.getOutfitNameForHanger(%this);
    ClosetItemsFrame.update();
    if ((ClosetTabs.getCurrentTab().name $= "CLOSET")) {
        "CLOSET".getTabWithName(ClosetTabs).thumbnails.setSelectedThumbs();
    }
    ClosetGui.updateVisibleAvatar();
};
function ClosetItemPopup::onSelect(%this, %unused, %entries) {
    if ((ClosetItemsFrame @ " " @ "CLOSET".getTabWithName(ClosetTabs).category $= %entries)) {
        return;
    }
    "CLOSET".getTabWithName(ClosetTabs).category = %entries @ ClosetItemsFrame;
    if ($gUpdatingClosetItemPopupFromThumbnailsSetDrawers) {
        $gUpdatingClosetItemPopupFromThumbnailsSetDrawers = 0;
        return;
    }
    if ("CLOSET".getTabWithName(ClosetTabs).tabClosetInitialized) {
        ClosetItemsFrame.update();
    }
    ClosetThumbnailsCloset.getParent().scrollToTop();
};
function ClosetBrandPopup::onSelect(%this, %unused, %entries) {
    if ((ClosetItemsFrame @ " " @ "CLOSET".getTabWithName(ClosetTabs).brand $= %entries)) {
        return;
    }
    "CLOSET".getTabWithName(ClosetTabs).brand = %entries @ ClosetItemsFrame;
    if ("CLOSET".getTabWithName(ClosetTabs).tabClosetInitialized) {
        ClosetItemsFrame.update();
    }
    ClosetThumbnailsCloset.getParent().scrollToTop();
};
function ClosetWhatYoureWearingList::onCreatedChild(%this, %child) {
    %currentTabName = ClosetTabs.getCurrentTab().name;
    %background = new GuiControl("") {
        profile = 0 @ "ClosetLtBackgroundProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "0 0";
        extent = "228 36";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
    };
    %hilite = new GuiControl("") {
        profile = 0 @ "ClosetHiliteProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "0 0";
        extent = "228 36";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 0;
    };
    %itemDesc = new GuiMLTextCtrl("") {
        profile = 0 @ "ClosetSmallLinkProfile";
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
    %itemDesc.bindClassName();
    ClosetWhatYoureWearingButton;
    %closeBox = new GuiBitmapButtonCtrl("") {
        profile = 0 @ "GuiDefaultProfile";
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
    %ugcStatusIcon = new GuiBitmapCtrl("") {
        profile = 0 @ "ETSNonModalProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "211 19";
        extent = "16 16";
        modulationColor = "255 255 255 100";
    };
    %expiringIcon = new GuiBitmapCtrl("") {
        profile = 0 @ "ETSNonModalProfile";
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
    if ((%currentTabName $= "MY DESIGNS")) {
    }
    %authorText = new GuiMLTextCtrl("") {
        profile = 0 @ "ClosetSmallLinkProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "80 18";
        extent = 18.0 @ (146.0 - 0.0) @ " " @ 15;
    };
    %background.add(%child);
    %hilite.add(%child);
    %ugcStatusIcon.add(%child);
    %itemDesc.add(%child);
    %expiringIcon.add(%child);
    %closeBox.add(%child);
    %authorText.add(%child);
    %child.background = %background;
    %child.hiliteCtrl = %hilite;
    %child.desc = %itemDesc;
    %child.ugcStatusIcon = %ugcStatusIcon;
    %child.expiringIcon = %expiringIcon;
    %child.closeBox = %closeBox;
    %child.authorText = %authorText;
    %child.sku = 0;
    if (!(getWord(%child.getNamespaceList(), 0) $= "ClosetWhatYoureWearingItem")) {
        "ClosetWhatYoureWearingItem".bindClassName(%child);
    }
};
$gNoSkuList = "400 850 875 900 950 5400 5850 5875 5900 5950 5980";
function ClosetWhatYoureWearingList::addSku(%this, %sku) {
    if ((findWord($gNoSkuList, %sku) != -(1.0))) {
        return;
    }
    %drawerAction = "";
    %child = %this.addChild();
    %child.sku = %sku;
    %si = %sku.findBySku(SkuManager);
    if (!(%sku.getPropSkus(SkuManager) $= "")) {
        if (%sku.isInstrumentSku(InstrumentRegistryClient)) {
            %drawerAction = "  (no animation)";
        }
        if (%child.isDoingPropAction) {
            %drawerAction = "  <a:gamelink stopPropAction>stop animation</a>";
            ClosetGui;
        }
        %drawerAction = "  <a:gamelink startPropAction>start animation</a>";
    }
    "<a:gamelink " @ %sku @ ">" @ %si.descShrt @ "</a>" @ "<br><spush><font:Arial:12><color:00000099>" @ %si.getUserFacingDrawerName() @ %drawerAction @ "<spop>".setText(%child.desc);
    if (!(%si.author $= "")) {
        %playerEncoded = urlEncode(stripUnprintables(%si.author));
        %profileURL = $Net::ProfileURL @ %playerEncoded;
        "<just:right><font:Arial:12><color:00000044><linkcolor:00000066>" @ "by <a:gamelink " @ %profileURL @ ">" @ %si.author @ "</a>".setText(%child.authorText);
    }
    "".setText(%child.authorText);
    if (!(%si.expireTime $= "")) {
        "platform/client/ui/expiring_icon".setBitmap(%child.expiringIcon);
        1.setVisible(%child.expiringIcon);
    }
    0.setVisible(%child.expiringIcon);
    %currentTabName = ClosetTabs.getCurrentTab().name;
    if ((%currentTabName $= "MY DESIGNS")) {
        ClosetGui_MyShop_GetSkuUGCStatusIcon(%sku).setBitmap(%child.ugcStatusIcon);
        1.setVisible(%child.ugcStatusIcon);
    }
    0.setVisible(%child.ugcStatusIcon);
    if (%this.filterByRemovable) {
        %showRemoveButton = %si.drwrName.isOptionalDrawer(SkuManager);
    }
    %showRemoveButton = 1;
    %showRemoveButton.setActive(%child.closeBox);
    (%this.getCount() - 1.0).hiliteCell(%this, 0);
    %this.reseatChildren();
};
function ClosetWhatYoureWearingList::setSkus(%this, %skus) {
    %propSku = $ClosetOutfitName[$ClosetSkusOutfit @ $ClosetOutfitName].getFirstPropSku(SkuManager);
    if ((%propSku $= "")) {
    }
    if ((%this.lastPropSku != %propSku)) {
        stopPropAction();
    }
    %this.lastPropSku = %propSku;
    if ((%propSku $= "")) {
        "p".setGenre($player);
    }
    %instrument = %propSku.getInstrumentBySku(InstrumentRegistryClient);
    if ((%instrument $= "")) {
        "p".setGenre($player);
    }
    %instrument.genre.setGenre($player);
    0.setNumChildren(%this);
    if ((getWordCount(%skus) == 0.0)) {
        "(wearing none)".setTextWithStyle(ClosetWhatYoureWearingNone);
        if (isObject(MyShopCopyToOutfitText)) {
            0.setVisible(MyShopCopyToOutfitText);
        }
    }
    "".setTextWithStyle(ClosetWhatYoureWearingNone);
    if (isObject(MyShopCopyToOutfitText)) {
        1.setVisible(MyShopCopyToOutfitText);
    }
    %skus = %skus.sortSkusByDrawer(SkuManager);
    %count = getWordCount(%skus);
    %i = 0;
    while ((%i < %count)) {
        getWord(%skus, %i).addSku(%this);
        %i = (%i + 1.0);
    }
    %this.skus = (%i < %count) @ %skus;
};
function ClosetWhatYoureWearingList::refresh(%this, %skus) {
    %skus.setSkus(ClosetWhatYoureWearingList);
};
function ClosetWhatYoureWearingButton::onURL(%this, %url) {
    if ((getWord(%url, 0) $= "gamelink")) {
        %url = getWords(%url, 1);
    }
    if ((%url $= "startPropAction")) {
        "".zoomToSKU(ClosetMainObjectView);
        doPropAction();
        $ClosetOutfitName[$ClosetSkusOutfit @ $ClosetOutfitName].refresh(ClosetWhatYoureWearingList);
    }
    if ((%url $= "stopPropAction")) {
        stopPropAction();
        $ClosetOutfitName[$ClosetSkusOutfit @ $ClosetOutfitName].refresh(ClosetWhatYoureWearingList);
    }
    if ((ClosetTabs.getCurrentTab().name $= "CLOSET")) {
        %url.scrollToSku(ClosetThumbnailsCloset, ClosetTabs.getCurrentTab().scroll);
    }
    if ((ClosetTabs.getCurrentTab().name $= "MY DESIGNS")) {
        ClosetGui_MyShop_ToggleCurrentSku(%url);
    }
};
function ClosetGUI_ToggleSku_Closet(%sku) {
    %drawer = %sku.findBySku(SkuManager).drwrName;
    %removable = %drawer.isOptionalDrawer(SkuManager);
    if (%sku.isBodySku(SkuManager)) {
        %wordLoc = findWord($ClosetSkusBody, %sku);
        if ((%wordLoc >= 0.0)) {
            if (%removable) {
                $ClosetSkusBody = removeWord($ClosetSkusBody, %wordLoc);
            }
        }
        $ClosetSkusBody = %sku.overlaySkus(SkuManager, $ClosetSkusBody);
    }
    if (%sku.isOutfitSku(SkuManager)) {
        %wordLoc = findWord($ClosetOutfitName[$ClosetSkusOutfit @ $ClosetOutfitName], %sku);
        if ((%wordLoc >= 0.0)) {
            if (%removable) {
                $ClosetOutfitName[$ClosetSkusOutfit @ $ClosetOutfitName] = removeWord($ClosetOutfitName[$ClosetSkusOutfit @ $ClosetOutfitName], %wordLoc);
            }
        }
        $ClosetOutfitName[$ClosetSkusOutfit @ $ClosetOutfitName] = %sku.overlaySkus(SkuManager, $ClosetOutfitName[$ClosetSkusOutfit @ $ClosetOutfitName]);
        %outfitNum = findWord(, $ClosetOutfitName);
        %objectView = %outfitNum.getOutfitObjectView(ClosetTabs);
        $ClosetSkusBody @ " " @ $ClosetOutfitName[$ClosetSkusOutfit @ $ClosetOutfitName].setSkus(%objectView);
    }
};
