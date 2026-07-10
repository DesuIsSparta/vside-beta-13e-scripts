function ClosetTabs::fillClosetTab(%this) {
    %theTab = %this.getTabWithName("CLOSET");
    if (!(isObject(%theTab))) {
        return;
    }
    0;
    %theTab.add(new ""() {
        profile = GuiBitmapCtrl @ "GuiDefaultProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "26 26";
        extent = "571 37";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        bitmap = "platform/client/ui/closet_tabs_bracket";
    };);
    %theTab.add(new GuiMLTextCtrl(NoItemInBrandNameLabel) {
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
    };);
    0;
    %itemLabel = new ""() {
        profile = GuiTextCtrl @ "ClosetTitleProfile";
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
    %theTab.add(%itemPopup);
    %categoryList = "All Items" @ "\t" @ "All Garments" @ "\t" @ "All Accessories" @ "\t" @ "Tops" @ "\t" @ "Bottoms" @ "\t" @ "Shoes" @ "\t" @ "Ear" @ "\t" @ "Neck" @ "\t" @ "Waist" @ "\t" @ "Skin" @ "\t" @ "Hands" @ "\t" @ "Bags" @ "\t" @ "Glasses" @ "\t" @ "Props" @ "\t" @ "Misc" @ "\t" @ "BodyMod" @ "\t" @ "Badges" @ "\t" @ "Tokens";
    %itemPopup.possibleCategoryList = %categoryList;
    %itemPopup.displayedCategoryList = %categoryList;
    %n = 0;
    if ((getFieldCount(%categoryList) < %n)) {
        %category = getField(%categoryList, %n);
        if (Closet::skuListHasCategory($Player::inventory, %category)) {
            %itemPopup.add(%category);
        }
        %n = (1.0 + %n);
    }
    0;
    %brandLabel = new ""() {
        profile = GuiTextCtrl @ "ClosetTitleProfile";
        horizSizing = (getFieldCount(%categoryList) < %n) @ "right";
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
    %theTab.add(%brandPopup);
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
    0;
    %itemsInfoText = new ""() {
        profile = GuiTextCtrl @ "ClosetLeftInfoProfile";
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
    0;
    %itemsRangeText = new ""() {
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
    %itemsFrame.add(%itemsRangeText);
    %theTab.rangeText = %itemsRangeText;
    0;
    %itemsScroll = new ""() {
        profile = GuiScrollCtrl @ "ETSScrollProfile";
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
    %itemsScroll.add(%thumbnails);
    %itemsScroll.thumbnails = %thumbnails;
    %itemsFrame.add(%itemsScroll);
    %itemsFrame.thumbnails = %thumbnails;
    %theTab.add(%itemsFrame);
    %theTab.thumbnails = %thumbnails;
    %theTab.add(new GuiMLTextCtrl(ClosetShortDescText) {
        profile = "ClosetLeftInfoProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "692 84";
        extent = "242 25";
        lineSpacing = -(3.0);
    };);
    %theTab.add(new GuiMLTextCtrl(ClosetLongDescText) {
        profile = "ClosetLeftInfoProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "692 106";
        extent = "173 32";
        lineSpacing = -(3.0);
    };);
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
    new ""() {
        profile = GuiBitmapCtrl @ "GuiDefaultProfile";
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
    %myOutfitsFrame.add(%hangers);
    %myOutfitsFrame.hangers = %hangers;
    %xPos = 0;
    %ypos = 0;
    %i = 0;
    if (($gClosetNumOutfits < %i)) {
        0;
        %objectView = new "ClosetOutfitObjectView" @ %i() {
            profile = GuiObjectView @ "GuiDefaultProfile";
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
            %objectView.setSimObject($player);
        }
        %objectView.setRotation(0.2, 0, 2.8);
        %objectView.setLookAtNudge("0.2 -1 0.15");
        %objectView.toonLineWidth = 2;
        0;
        %label = new ""() {
            profile = GuiMLTextCtrl @ "ETSNonModalProfile";
            position = "10 0";
            extent = "30 15";
            text = "<font:Arial:16><color:ffffff><b><outline><just:right>" @ " " @ (1.0 + %i) @ " ";
            visible = 0;
        };
        0;
        %button = new ""() {
            profile = GuiBitmapButtonCtrl @ "ClosetHangerButtonProfile";
            horizSizing = "right";
            vertSizing = "bottom";
            position = %xPos @ " " @ %ypos;
            extent = "39 112";
            minExtent = "1 1";
            sluggishness = -1;
            visible = 1;
            command = "ClosetMyOutfitsFrame.hangerSelected(" @ %i @ ");";
            text = (1.0 + %i);
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
        %hangers.button = %button @ %i;
        %xPos = (39.0 + %xPos);
        %i = (1.0 + %i);
    }
    %theTab.add(%myOutfitsFrame);
    %whatYoureWearingContainer = new GuiControl(ClosetWhatYourWearingContainer) {
        profile = ($gClosetNumOutfits < %i) @ "ETSNonModalProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "689 207";
        extent = "245 281";
    };
    %whatYoureWearingPanel = %this.createWhatYourWearingPanel();
    %whatYoureWearingContainer.add(%whatYoureWearingPanel);
    %theTab.add(%whatYoureWearingContainer);
    0;
    %doneButton = new ""() {
        profile = GuiVariableWidthButtonCtrl @ "BracketButton19Profile";
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
    0;
    %cancelButton = new ""() {
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
};
function ClosetTabs::getOutfitObjectView(%this, %index) {
    return "ClosetOutfitObjectView" @ %index;
};
function ClosetTabs::getOutfitButton(%this, %index) {
    return "ClosetOutfitButton" @ %index;
};
function ClosetTabs::updateOutfitObjectView(%this, %index) {
    %objectView = %this.getOutfitObjectView(%index);
    %name = %index.getOutfitNameForHanger();
    ClosetMyOutfitsFrame;
    %objectView.setSkus($ClosetSkusBody @ " " @ %name[$ClosetSkusOutfit @ %name]);
};
$gSwimsuitOutfitIndex = 6;
function ClosetTabs::doCopyOutfit(%this, %src, %dest) {
    %title = $gSwimsuitOutfitIndex[$MsgCat::closet TAB "MSG-COPY-OUTFIT-WARN" @ "TITLE"];
    %body = %title[$MsgCat::closet TAB "MSG-COPY-OUTFIT-WARN" @ "BODY"];
    %body = strreplace(%body, "[SRC]", (1.0 + %src));
    %body = strreplace(%body, "[DST]", (1.0 + %dest));
    if (($gSwimsuitOutfitIndex == %dest)) {
        %body = %body[%body @ "\n" @ "" @ "\n" @ $MsgCat::closet TAB "MSG-COPY-OUTFIT-WARN" @ "SWIM"];
    }
    MessageBoxYesNo(%title, %body, "ClosetTabs.doCopyOutfitReally(" @ %src @ ", " @ %dest @ ");", "");
};
function ClosetTabs::doCopyOutfitReally(%this, %src, %dest) {
    %srcName = %src.getOutfitNameForHanger();
    ClosetMyOutfitsFrame;
    %destName = %dest.getOutfitNameForHanger();
    ClosetMyOutfitsFrame;
    %destName[$ClosetSkusOutfit @ %destName] = %srcName[$ClosetSkusOutfit @ %srcName];
    %dest.updateOutfitObjectView();
    %dest.getOutfitButton().performClick();
};
function ClosetTabs::doSwapOutfits(%this, %src, %dest) {
    if (($gSwimsuitOutfitIndex == %src)) {
    }
    if (($gSwimsuitOutfitIndex == %dest)) {
        %title = %dest[$MsgCat::closet TAB "MSG-SWAP-OUTFIT-WARN" @ "TITLE"];
        %body = %title[$MsgCat::closet TAB "MSG-SWAP-OUTFIT-WARN" @ "BODY"];
        MessageBoxYesNo(%title, %body, "ClosetTabs.doSwapOutfitsReally(" @ %src @ ", " @ %dest @ ");", "");
    }
    %this.doSwapOutfitsReally(%src, %dest);
};
function ClosetTabs::doSwapOutfitsReally(%this, %src, %dest) {
    %srcName = %src.getOutfitNameForHanger();
    ClosetMyOutfitsFrame;
    %destName = %dest.getOutfitNameForHanger();
    ClosetMyOutfitsFrame;
    %tmp = %srcName[$ClosetSkusOutfit @ %srcName];
    %srcName[$ClosetSkusOutfit @ %srcName] = %destName[$ClosetSkusOutfit @ %destName];
    %destName[$ClosetSkusOutfit @ %destName] = %tmp;
    %src.updateOutfitObjectView();
    %dest.updateOutfitObjectView();
    %dest.getOutfitButton().performClick();
};
function ClosetOutfitButton::onMouseDown(%this) {
    %this.origin = Canvas.getCursorPos();
};
function ClosetOutfitButton::onMouseDragged(%this, %modifier) {
    %vec = VectorSub(%this.origin, Canvas.getCursorPos());
    if (((10.0 * 10.0) < VectorLenSquared(%vec))) {
        return 0;
    }
    if (($Platform $= "macos")) {
    }
    %mask = $EventModifier::CTRL;
    $EventModifier::ALT;
    %this.operation = (%mask & %modifier) ? "COPY" : "SWAP";
    %this.setAsDragControl(1);
    return 1;
};
function ClosetOutfitButton::makeVisualClone(%this) {
    0;
    %objectView = new ""() {
        profile = GuiObjectView @ "GuiDefaultProfile";
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
        %objectView.setSimObject($player);
    }
    %objectView.setRotation(0.2, 0, 2.8);
    %objectView.setLookAtNudge("0.2 -1 0.15");
    %objectView.toonLineWidth = 2;
    %objectView.setSkus($ClosetSkusBody @ " " @ $ClosetSkusOutfit);
    0;
    %label = new ""() {
        profile = GuiMLTextCtrl @ "ETSNonModalProfile";
        position = "10 0";
        extent = "30 15";
        text = "<font:Arial:16><color:ffffff><b><outline><just:right>" @ " " @ (1.0 + %this.index) @ " ";
    };
    if ((%this.operation $= "")) {
        %this.operation = "SWAP";
    }
    0;
    %operationIcon = new ""() {
        profile = GuiBitmapCtrl @ "ETSNonModalProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "20 76";
        extent = "20 20";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 0;
        bitmap = "platform/client/ui/" @ %this.operation;
    };
    0;
    %clone = new ""() {
        position = GuiBitmapButtonCtrl @ "0 0";
        extent = %this.getExtent();
        bitmap = %this.bitmap;
    };
    %clone.add(%objectView);
    %clone.add(%label);
    %clone.add(%operationIcon);
    %clone.operationIcon = %operationIcon;
    %this.clone = %clone;
    return %clone;
};
function ClosetOutfitButton::dragAndDropCtrl(%this, %make) {
    if (%make) {
        %this.operation = "COPY";
    }
    %this.operation = "SWAP";
    %this.clone.operationIcon.setBitmap("platform/client/ui/" @ %this.operation);
};
function ClosetOutfitButton::onDragSet(%this) {
    %this.depressed = 1;
    %i = 0;
    %button = %i.getOutfitButton();
    if (isObject(ClosetTabs)) {
        %button.mouseOver = 0;
        %i = (1.0 + %i);
        %button = %i.getOutfitButton();
    }
    Canvas.centerDragHiliteAroundCursor();
    %this.clone.mouseOver = isObject(ClosetTabs) @ 1;
};
function ClosetOutfitButton::onDragReleased(%this) {
    %this.depressed = 0;
};
function ClosetOutfitButton::onDragAndDropEnter(%this, %dragCtrl) {
    if ((-(1.0) == findWord(%dragCtrl.getNamespaceList(), "ClosetOutfitButton"))) {
        return;
    }
    if ((%dragCtrl != %this)) {
        hiliteControl(%this, 1);
        %this.depressed = 1;
        %dragCtrl.clone.operationIcon.setVisible(1);
        %this.label.setVisible(1);
    }
};
function ClosetOutfitButton::onDragAndDropLeave(%this, %dragCtrl) {
    if ((%dragCtrl != %this)) {
        hiliteControl(0);
        %this.depressed = 0;
        %dragCtrl.clone.operationIcon.setVisible(0);
        %this.label.setVisible(0);
    }
};
function ClosetOutfitButton::onDragAndDropDrop(%this, %dragCtrl, %unused) {
    if ((-(1.0) == findWord(%dragCtrl.getNamespaceList(), "ClosetOutfitButton"))) {
        return 0;
    }
    if ((%dragCtrl == %this)) {
        return 0;
    }
    if ((%dragCtrl.operation $= "COPY")) {
        %dragCtrl.index.doCopyOutfit(%this.index);
    }
    if ((ClosetTabs @ " " @ %dragCtrl.operation $= "SWAP")) {
        %dragCtrl.index.doSwapOutfits(%this.index);
    }
    return 1;
};
function ClosetBrandPopup::update(%this, %skus) {
    %this.clear();
    %i = 0;
    if ((getFieldCount($gClosetBrands) < %i)) {
        %brand = getField($gClosetBrands, %i);
        if (%skus.skusHaveBrands(%brand[$gClosetBrandsIntrnl @ %brand] @ " ")) {
            %this.add(%brand);
        }
        %i = (1.0 + %i);
        SkuManager;
    }
};
function ClosetItemPopup::update(%this, %skus) {
    "".setText();
    %prevSelText = %this.getTextById(%this.GetSelected());
    NoItemInBrandNameLabel;
    if (!(%skus $= "")) {
        %newList = "";
        %n = 0;
        if ((getFieldCount(%this.possibleCategoryList) < %n)) {
            %category = getField(%this.possibleCategoryList, %n);
            if (Closet::skuListHasCategory(%skus, %category)) {
                %newList = %newList @ "\t" @ %category;
            }
            %n = (1.0 + %n);
        }
        %newList = trim(%newList);
        (getFieldCount(%this.possibleCategoryList) < %n);
        if ((%newList $= %this.displayedCategoryList)) {
            return;
        }
    }
    %newList = getField(%this.possibleCategoryList, 0);
    %this.displayedCategoryList = %newList;
    %prevSelText = %this.getTextById(%this.GetSelected());
    %this.clear();
    %n = 0;
    if ((getFieldCount(%newList) < %n)) {
        %this.add(getField(%newList, %n));
        %n = (1.0 + %n);
    }
    %newSel = %this.findText(%prevSelText);
    (getFieldCount(%newList) < %n);
    if ((0.0 < %newSel)) {
        %this.SetSelected(0);
        if ((ClosetBrandPopup.getText() $= "All")) {
            %brandString = "";
        }
        if ((ClosetBrandPopup.getText() $= "Basic")) {
            %brandString = " basic";
        }
        %brandString = " " @ ClosetBrandPopup.getText() @ " " @ "brand";
        if ((firstWord(%prevSelText) $= "All")) {
        }
        %categoryString = strlwr(%prevSelText);
        restWords(%prevSelText);
        if ((%categoryString $= "")) {
            %categoryString = "clothes";
        }
        %msg = %categoryString[$MsgCat::closet @ "H-NO-BRAND-ITEMS1"] @ %brandString @ " " @ %categoryString @ %categoryString[$MsgCat::closet @ "H-NO-BRAND-ITEMS2"] @ %brandString @ " " @ %brandString[$MsgCat::closet @ "H-NO-BRAND-ITEMS3"];
        %msg.setText();
    }
    %this.setText(%prevSelText);
};
function ClosetItemsFrame::update(%this) {
    if ((ClosetTabs.getCurrentTab().name $= "CLOSET")) {
        %this.thumbnails.setDrawers("");
    }
};
function ClosetMyOutfitsFrame::getOutfitNameForHanger(%this, %hanger) {
    return getWord(, %hanger);
};
function ClosetMyOutfitsFrame::hangerSelected(%this, %hanger) {
    %this.currentHanger = %hanger;
    $ClosetOutfitName = %this.getOutfitNameForHanger(%hanger);
    ClosetItemsFrame.update();
    if ((ClosetTabs.getCurrentTab().name $= "CLOSET")) {
        "CLOSET".getTabWithName().thumbnails.setSelectedThumbs();
    }
    ClosetGui.updateVisibleAvatar();
};
function ClosetItemPopup::onSelect(%this, %unused, %entries) {
    if ((ClosetItemsFrame @ " " @ "CLOSET".getTabWithName().category $= %entries)) {
        return;
    }
    "CLOSET".getTabWithName().category = %entries @ ClosetItemsFrame;
    if ($gUpdatingClosetItemPopupFromThumbnailsSetDrawers) {
        $gUpdatingClosetItemPopupFromThumbnailsSetDrawers = 0;
        return;
    }
    if ("CLOSET".getTabWithName().tabClosetInitialized) {
        ClosetItemsFrame.update();
    }
    ClosetThumbnailsCloset.getParent().scrollToTop();
};
function ClosetBrandPopup::onSelect(%this, %unused, %entries) {
    if ((ClosetItemsFrame @ " " @ "CLOSET".getTabWithName().brand $= %entries)) {
        return;
    }
    "CLOSET".getTabWithName().brand = %entries @ ClosetItemsFrame;
    if ("CLOSET".getTabWithName().tabClosetInitialized) {
        ClosetItemsFrame.update();
    }
    ClosetThumbnailsCloset.getParent().scrollToTop();
};
function ClosetWhatYoureWearingList::onCreatedChild(%this, %child) {
    %currentTabName = ClosetTabs.getCurrentTab().name;
    0;
    %background = new ""() {
        profile = GuiControl @ "ClosetLtBackgroundProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "0 0";
        extent = "228 36";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
    };
    0;
    %hilite = new ""() {
        profile = GuiControl @ "ClosetHiliteProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "0 0";
        extent = "228 36";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 0;
    };
    0;
    %itemDesc = new ""() {
        profile = GuiMLTextCtrl @ "ClosetSmallLinkProfile";
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
    0;
    %closeBox = new ""() {
        profile = GuiBitmapButtonCtrl @ "GuiDefaultProfile";
        horizSizing = ClosetWhatYoureWearingButton @ "right";
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
    0;
    %ugcStatusIcon = new ""() {
        profile = GuiBitmapCtrl @ "ETSNonModalProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "211 19";
        extent = "16 16";
        modulationColor = "255 255 255 100";
    };
    0;
    %expiringIcon = new ""() {
        profile = GuiBitmapCtrl @ "ETSNonModalProfile";
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
    0;
    if ((%currentTabName $= "MY DESIGNS")) {
    }
    %authorText = new ""() {
        profile = GuiMLTextCtrl @ "ClosetSmallLinkProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "80 18";
        extent = 18.0 @ (0.0 - 146.0) @ " " @ 15;
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
    if (!(getWord(%child.getNamespaceList(), 0) $= "ClosetWhatYoureWearingItem")) {
        %child.bindClassName("ClosetWhatYoureWearingItem");
    }
};
$gNoSkuList = "400 850 875 900 950 5400 5850 5875 5900 5950 5980";
function ClosetWhatYoureWearingList::addSku(%this, %sku) {
    if ((-(1.0) != findWord($gNoSkuList, %sku))) {
        return;
    }
    %drawerAction = "";
    %child = %this.addChild();
    %child.sku = %sku;
    %si = %sku.findBySku();
    SkuManager;
    if (!(SkuManager @ " " @ %sku.getPropSkus() $= "")) {
        if (%sku.isInstrumentSku()) {
            %drawerAction = "  (no animation)";
            InstrumentRegistryClient;
        }
        if (%child.isDoingPropAction) {
            %drawerAction = "  <a:gamelink stopPropAction>stop animation</a>";
            ClosetGui;
        }
        %drawerAction = "  <a:gamelink startPropAction>start animation</a>";
    }
    %child.desc.setText("<a:gamelink " @ %sku @ ">" @ %si.descShrt @ "</a>" @ "<br><spush><font:Arial:12><color:00000099>" @ %si.getUserFacingDrawerName() @ %drawerAction @ "<spop>");
    if (!(%si.author $= "")) {
        %playerEncoded = urlEncode(stripUnprintables(%si.author));
        %profileURL = $Net::ProfileURL @ %playerEncoded;
        %child.authorText.setText("<just:right><font:Arial:12><color:00000044><linkcolor:00000066>" @ "by <a:gamelink " @ %profileURL @ ">" @ %si.author @ "</a>");
    }
    %child.authorText.setText("");
    if (!(%si.expireTime $= "")) {
        %child.expiringIcon.setBitmap("platform/client/ui/expiring_icon");
        %child.expiringIcon.setVisible(1);
    }
    %child.expiringIcon.setVisible(0);
    %currentTabName = ClosetTabs.getCurrentTab().name;
    if ((%currentTabName $= "MY DESIGNS")) {
        %child.ugcStatusIcon.setBitmap(ClosetGui_MyShop_GetSkuUGCStatusIcon(%sku));
        %child.ugcStatusIcon.setVisible(1);
    }
    %child.ugcStatusIcon.setVisible(0);
    if (%this.filterByRemovable) {
        %showRemoveButton = %si.drwrName.isOptionalDrawer();
        SkuManager;
    }
    %showRemoveButton = 1;
    %child.closeBox.setActive(%showRemoveButton);
    %this.hiliteCell(0, (1.0 - %this.getCount()));
    %this.reseatChildren();
};
function ClosetWhatYoureWearingList::setSkus(%this, %skus) {
    %propSku = $ClosetOutfitName[$ClosetSkusOutfit @ $ClosetOutfitName].getFirstPropSku();
    SkuManager;
    if ((%propSku $= "")) {
    }
    if ((%propSku != %this.lastPropSku)) {
        stopPropAction();
    }
    %this.lastPropSku = %propSku;
    if ((%propSku $= "")) {
        $player.setGenre("p");
    }
    %instrument = %propSku.getInstrumentBySku();
    InstrumentRegistryClient;
    if ((%instrument $= "")) {
        $player.setGenre("p");
    }
    $player.setGenre(%instrument.genre);
    %this.setNumChildren(0);
    if ((0.0 == getWordCount(%skus))) {
        "(wearing none)".setTextWithStyle();
        if (isObject(MyShopCopyToOutfitText)) {
            0.setVisible();
        }
    }
    "".setTextWithStyle();
    if (isObject(MyShopCopyToOutfitText)) {
        1.setVisible();
    }
    %skus = %skus.sortSkusByDrawer();
    SkuManager;
    %count = getWordCount(%skus);
    MyShopCopyToOutfitText;
    %i = 0;
    ClosetWhatYoureWearingNone;
    if ((%count < %i)) {
        %this.addSku(getWord(%skus, %i));
        %i = (1.0 + %i);
        MyShopCopyToOutfitText;
    }
    %this.skus = (%count < %i) @ %skus;
    ClosetWhatYoureWearingNone;
};
function ClosetWhatYoureWearingList::refresh(%this, %skus) {
    %skus.setSkus();
};
function ClosetWhatYoureWearingButton::onURL(%this, %url) {
    if ((getWord(%url, 0) $= "gamelink")) {
        %url = getWords(%url, 1);
    }
    if ((%url $= "startPropAction")) {
        "".zoomToSKU();
        doPropAction();
        $ClosetOutfitName[$ClosetSkusOutfit @ $ClosetOutfitName].refresh();
    }
    if ((ClosetWhatYoureWearingList @ " " @ %url $= "stopPropAction")) {
        stopPropAction();
        $ClosetOutfitName[$ClosetSkusOutfit @ $ClosetOutfitName].refresh();
    }
    if ((ClosetWhatYoureWearingList @ " " @ ClosetTabs.getCurrentTab().name $= "CLOSET")) {
        ClosetTabs.getCurrentTab().scroll.scrollToSku(%url);
    }
    if ((ClosetThumbnailsCloset @ " " @ ClosetTabs.getCurrentTab().name $= "MY DESIGNS")) {
        ClosetGui_MyShop_ToggleCurrentSku(%url);
    }
};
function ClosetGUI_ToggleSku_Closet(%sku) {
    %drawer = %sku.findBySku().drwrName;
    SkuManager;
    %removable = %drawer.isOptionalDrawer();
    SkuManager;
    if (%sku.isBodySku()) {
        %wordLoc = findWord($ClosetSkusBody, %sku);
        SkuManager;
        if ((0.0 >= %wordLoc)) {
            if (%removable) {
                $ClosetSkusBody = removeWord($ClosetSkusBody, %wordLoc);
            }
        }
        $ClosetSkusBody = $ClosetSkusBody.overlaySkus(%sku);
        SkuManager;
    }
    if (%sku.isOutfitSku()) {
        %wordLoc = findWord($ClosetOutfitName[$ClosetSkusOutfit @ $ClosetOutfitName], %sku);
        SkuManager;
        if ((0.0 >= %wordLoc)) {
            if (%removable) {
                $ClosetOutfitName[$ClosetSkusOutfit @ $ClosetOutfitName] = removeWord($ClosetOutfitName[$ClosetSkusOutfit @ $ClosetOutfitName], %wordLoc);
            }
        }
        $ClosetOutfitName[$ClosetSkusOutfit @ $ClosetOutfitName] = SkuManager @ $ClosetOutfitName[$ClosetSkusOutfit @ $ClosetOutfitName].overlaySkus(%sku);
        %outfitNum = findWord(, $ClosetOutfitName);
        %objectView = %outfitNum.getOutfitObjectView();
        ClosetTabs;
        %objectView.setSkus($ClosetSkusBody @ " " @ $ClosetOutfitName[$ClosetSkusOutfit @ $ClosetOutfitName]);
    }
};
