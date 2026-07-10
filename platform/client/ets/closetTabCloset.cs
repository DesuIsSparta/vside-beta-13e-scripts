function ClosetTabs::fillClosetTab(%this) {
    %theTab = %this.getTabWithName("CLOSET");
    if (!(isObject(%theTab))) {
        return;
    }
    profile = GuiBitmapCtrl @ new ""() @ "GuiDefaultProfile";
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
    profile = new GuiMLTextCtrl(NoItemInBrandNameLabel) @ "ClosetLeftInfoProfile";
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
    %theTab.add();
    profile = GuiTextCtrl @ new ""() @ "ClosetTitleProfile";
    0;
    horizSizing = "right";
    vertSizing = "bottom";
    position = "181 64";
    extent = "35 20";
    minExtent = "1 1";
    sluggishness = -1;
    visible = 1;
    text = "Item";
    maxLength = 255;
    %itemLabel = ;
    %theTab.add(%itemLabel);
    profile = new GuiPopUp2MenuCtrl(ClosetItemPopup) @ "ClosetPopupProfile";
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
    %itemPopup = ;
    %theTab.add(%itemPopup);
    %categoryList = "All Items" @ "\t" @ "All Garments" @ "\t" @ "All Accessories" @ "\t" @ "Tops" @ "\t" @ "Bottoms" @ "\t" @ "Shoes" @ "\t" @ "Ear" @ "\t" @ "Neck" @ "\t" @ "Waist" @ "\t" @ "Skin" @ "\t" @ "Hands" @ "\t" @ "Bags" @ "\t" @ "Glasses" @ "\t" @ "Props" @ "\t" @ "Misc" @ "\t" @ "BodyMod" @ "\t" @ "Badges" @ "\t" @ "Tokens";
    possibleCategoryList = %categoryList @ %itemPopup;
    displayedCategoryList = %categoryList @ %itemPopup;
    %n = 0;
    if ((getFieldCount(%categoryList) < %n)) {
        %category = getField(%categoryList, %n);
        if (Closet::skuListHasCategory($Player::inventory, %category)) {
            %itemPopup.add(%category);
        }
        %n = (1.0 + %n);
    }
    profile = GuiTextCtrl @ new ""() @ "ClosetTitleProfile";
    0;
    horizSizing = (getFieldCount(%categoryList) < %n) @ "right";
    vertSizing = "bottom";
    position = "27 64";
    extent = "104 20";
    minExtent = "1 1";
    sluggishness = -1;
    visible = 1;
    text = "Brand";
    maxLength = 255;
    %brandLabel = ;
    %theTab.add(%brandLabel);
    profile = new GuiPopUp2MenuCtrl(ClosetBrandPopup) @ "ClosetPopupProfile";
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
    %brandPopup = ;
    %theTab.add(%brandPopup);
    profile = new GuiControl(ClosetItemsFrame) @ "GuiDefaultProfile";
    horizSizing = "right";
    vertSizing = "bottom";
    position = "22 120";
    extent = "467 306";
    minExtent = "1 1";
    sluggishness = -1;
    visible = 1;
    %itemsFrame = ;
    profile = GuiTextCtrl @ new ""() @ "ClosetLeftInfoProfile";
    0;
    horizSizing = "right";
    vertSizing = "bottom";
    position = "1 30";
    extent = "77 16";
    minExtent = "1 1";
    sluggishness = -1;
    visible = 0;
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
    %itemsRangeText = ;
    %itemsFrame.add(%itemsRangeText);
    rangeText = %itemsRangeText @ %theTab;
    profile = GuiScrollCtrl @ new ""() @ "ETSScrollProfile";
    0;
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
    %itemsScroll = ;
    %itemsScroll.bindClassName("ClosetItemsScroll");
    itemsScroll = %itemsScroll @ %theTab;
    class = new GuiArray2Ctrl(ClosetThumbnailsCloset) @ "ClosetThumbnails";
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
    %thumbnails = ;
    %itemsScroll.add(%thumbnails);
    thumbnails = %thumbnails @ %itemsScroll;
    %itemsFrame.add(%itemsScroll);
    thumbnails = %thumbnails @ %itemsFrame;
    %theTab.add(%itemsFrame);
    thumbnails = %thumbnails @ %theTab;
    profile = new GuiMLTextCtrl(ClosetShortDescText) @ "ClosetLeftInfoProfile";
    horizSizing = "right";
    vertSizing = "bottom";
    position = "692 84";
    extent = "242 25";
    lineSpacing = -(3.0);
    %theTab.add();
    profile = new GuiMLTextCtrl(ClosetLongDescText) @ "ClosetLeftInfoProfile";
    horizSizing = "right";
    vertSizing = "bottom";
    position = "692 106";
    extent = "173 32";
    lineSpacing = -(3.0);
    %theTab.add();
    profile = new GuiControl(ClosetMyOutfitsFrame) @ "GuiDefaultProfile";
    horizSizing = "right";
    vertSizing = "bottom";
    position = "5 430";
    extent = "498 112";
    minExtent = "1 1";
    sluggishness = -1;
    visible = 1;
    bitmap = "";
    profile = GuiBitmapCtrl @ new ""() @ "GuiDefaultProfile";
    horizSizing = "right";
    vertSizing = "bottom";
    position = "0 10";
    extent = "15 56";
    minExtent = "1 1";
    sluggishness = -1;
    visible = 1;
    bitmap = "platform/client/ui/outfits";
    %myOutfitsFrame = ;
    profile = new GuiMouseEventCtrl(ClosetHangersFrame) @ "GuiDefaultProfile";
    horizSizing = "right";
    vertSizing = "bottom";
    position = "21 0";
    extent = "471 112";
    minExtent = "1 1";
    sluggishness = -(1.0);
    visible = 1;
    %hangers = ;
    %myOutfitsFrame.add(%hangers);
    hangers = %hangers @ %myOutfitsFrame;
    %xPos = 0;
    %ypos = 0;
    %i = 0;
    if (($gClosetNumOutfits < %i)) {
        profile = 0 @ new GuiObjectView @ "ClosetOutfitObjectView" @ %i() @ "GuiDefaultProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = %xPos @ " " @ %ypos;
        extent = "46 112";
        minExtent = "1 1";
        sluggishness = -1;
        CamSluggishness = 0.0000001;
        visible = 1;
        %objectView = ;
        if (isObject($player)) {
            %objectView.setSimObject($player);
        }
        %objectView.setRotation(0.2, 0, 2.8);
        %objectView.setLookAtNudge("0.2 -1 0.15");
        toonLineWidth = 2 @ %objectView;
        profile = GuiMLTextCtrl @ new ""() @ "ETSNonModalProfile";
        0;
        position = "10 0";
        extent = "30 15";
        text = "<font:Arial:16><color:ffffff><b><outline><just:right>" @ " " @ (1.0 + %i) @ " ";
        visible = 0;
        %label = ;
        profile = GuiBitmapButtonCtrl @ new ""() @ "ClosetHangerButtonProfile";
        0;
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
        %button = ;
        %button.bindClassName("ClosetOutfitButton");
        %button.setName("ClosetOutfitButton" @ %i);
        %button.add(%label);
        label = %label @ %button;
        %hangers.add(%objectView);
        %hangers.add(%button);
        button = %button @ %i @ %hangers;
        %xPos = (39.0 + %xPos);
        %i = (1.0 + %i);
    }
    %theTab.add(%myOutfitsFrame);
    profile = ($gClosetNumOutfits < %i) @ new GuiControl(ClosetWhatYourWearingContainer) @ "ETSNonModalProfile";
    horizSizing = "right";
    vertSizing = "bottom";
    position = "689 207";
    extent = "245 281";
    %whatYoureWearingContainer = ;
    %whatYoureWearingPanel = %this.createWhatYourWearingPanel();
    %whatYoureWearingContainer.add(%whatYoureWearingPanel);
    %theTab.add(%whatYoureWearingContainer);
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
    %doneButton = ;
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
    %cancelButton = ;
    %theTab.add(%doneButton);
    doneButton = %doneButton @ %theTab;
    %theTab.add(%cancelButton);
    cancelButton = %cancelButton @ %theTab;
    %skus = getFilteredInventoryForSetDrawers();
    %brandPopup.update(%skus);
    %itemPopup.update(%skus);
    %brandPopup.SetSelected(0);
    %itemPopup.SetSelected(0);
    update();
    tabClosetInitialized = ClosetItemsFrame @ 1 @ %this;
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
    origin = Canvas @ getCursorPos() @ %this;
};
function ClosetOutfitButton::onMouseDragged(%this, %modifier) {
    %vec = VectorSub(origin, getCursorPos());
    Canvas;
    if (((10.0 * 10.0) < VectorLenSquared(%vec))) {
        return 0;
    }
    if (($Platform $= "macos")) {
    }
    %mask = $EventModifier::CTRL;
    $EventModifier::ALT;
    operation = (%mask & %modifier) ? "COPY" : "SWAP" @ %this;
    %this.setAsDragControl(1);
    return 1;
};
function ClosetOutfitButton::makeVisualClone(%this) {
    profile = GuiObjectView @ new ""() @ "GuiDefaultProfile";
    0;
    horizSizing = "right";
    vertSizing = "bottom";
    position = "0 0";
    extent = "46 112";
    minExtent = "1 1";
    sluggishness = -1;
    CamSluggishness = 0.0000001;
    visible = 1;
    %objectView = ;
    if (isObject($player)) {
        %objectView.setSimObject($player);
    }
    %objectView.setRotation(0.2, 0, 2.8);
    %objectView.setLookAtNudge("0.2 -1 0.15");
    toonLineWidth = 2 @ %objectView;
    %objectView.setSkus($ClosetSkusOutfit @ ClosetMyOutfitsFrame);
    profile = GuiMLTextCtrl @ new ""() @ "ETSNonModalProfile";
    0;
    position = $ClosetSkusBody @ " " @ "10 0";
    extent = "30 15";
    text = "<font:Arial:16><color:ffffff><b><outline><just:right>" @ " " @ 1.0 @ (%this + index) @ " ";
    %label = ;
    if ((%this SPC operation $= "")) {
        operation = "SWAP" @ %this;
    }
    profile = GuiBitmapCtrl @ new ""() @ "ETSNonModalProfile";
    0;
    horizSizing = "right";
    vertSizing = "bottom";
    position = "20 76";
    extent = "20 20";
    minExtent = "1 1";
    sluggishness = -1;
    visible = 0;
    bitmap = "platform/client/ui/" @ %this @ operation;
    %operationIcon = ;
    position = GuiBitmapButtonCtrl @ new ""() @ "0 0";
    0;
    extent = %this.getExtent();
    bitmap = %this @ bitmap;
    %clone = ;
    %clone.add(%objectView);
    %clone.add(%label);
    %clone.add(%operationIcon);
    operationIcon = %operationIcon @ %clone;
    clone = %clone @ %this;
    return %clone;
};
function ClosetOutfitButton::dragAndDropCtrl(%this, %make) {
    if (%make) {
        operation = "COPY" @ %this;
    }
    operation = "SWAP" @ %this;
    operationIcon.setBitmap(%this @ operation);
};
function ClosetOutfitButton::onDragSet(%this) {
    depressed = 1 @ %this;
    %i = 0;
    %button = %i.getOutfitButton();
    if (isObject(ClosetTabs)) {
        mouseOver = 0 @ %button;
        %i = (1.0 + %i);
        %button = %i.getOutfitButton();
    }
    centerDragHiliteAroundCursor();
    mouseOver = %this @ clone;
    Canvas @ 1;
};
function ClosetOutfitButton::onDragReleased(%this) {
    depressed = 0 @ %this;
};
function ClosetOutfitButton::onDragAndDropEnter(%this, %dragCtrl) {
    if ((-(1.0) == findWord(%dragCtrl.getNamespaceList(), "ClosetOutfitButton"))) {
        return;
    }
    if ((%dragCtrl != %this)) {
        hiliteControl(%this, 1);
        depressed = 1 @ %this;
        operationIcon.setVisible(1);
        label.setVisible(1);
    }
};
function ClosetOutfitButton::onDragAndDropLeave(%this, %dragCtrl) {
    if ((%dragCtrl != %this)) {
        hiliteControl(0);
        depressed = 0 @ %this;
        operationIcon.setVisible(0);
        label.setVisible(0);
    }
};
function ClosetOutfitButton::onDragAndDropDrop(%this, %dragCtrl, %unused) {
    if ((-(1.0) == findWord(%dragCtrl.getNamespaceList(), "ClosetOutfitButton"))) {
        return 0;
    }
    if ((%dragCtrl == %this)) {
        return 0;
    }
    if ((%dragCtrl SPC operation $= "COPY")) {
        index.doCopyOutfit(index);
    }
    if ((%dragCtrl SPC operation $= "SWAP")) {
        index.doSwapOutfits(index);
    }
    return 1;
};
function ClosetBrandPopup::update(%this, %skus) {
    %this.clear();
    %i = 0;
    if ((getFieldCount($gClosetBrands) < %i)) {
        %brand = getField($gClosetBrands, %i);
        if (%skus.skusHaveBrands(SkuManager @ %brand[$gClosetBrandsIntrnl @ %brand] @ " ")) {
            %this.add(%brand);
        }
        %i = (1.0 + %i);
    }
};
function ClosetItemPopup::update(%this, %skus) {
    "".setText();
    %prevSelText = %this.getTextById(%this.GetSelected());
    NoItemInBrandNameLabel;
    if (!(%skus $= "")) {
        %newList = "";
        %n = 0;
        if ((getFieldCount(possibleCategoryList) < %n)) {
            %category = getField(possibleCategoryList, %n);
            %this;
            if (Closet::skuListHasCategory(%skus, %category)) {
                %newList = %newList @ "\t" @ %category;
                %this;
            }
            %n = (1.0 + %n);
        }
        %newList = trim(%newList);
        (getFieldCount(possibleCategoryList) < %n);
        if ((%this $= displayedCategoryList)) {
            return %this SPC %newList;
        }
    }
    %newList = getField(possibleCategoryList, 0);
    %this;
    displayedCategoryList = %newList @ %this;
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
        if ((ClosetBrandPopup SPC getText() $= "All")) {
            %brandString = "";
        }
        if ((ClosetBrandPopup SPC getText() $= "Basic")) {
            %brandString = " basic";
        }
        %brandString = ClosetBrandPopup @ getText() @ " " @ "brand";
        " ";
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
    if ((getCurrentTab() SPC name $= "CLOSET")) {
        thumbnails.setDrawers("");
    }
};
function ClosetMyOutfitsFrame::getOutfitNameForHanger(%this, %hanger) {
    return getWord(, %hanger);
};
function ClosetMyOutfitsFrame::hangerSelected(%this, %hanger) {
    currentHanger = %hanger @ %this;
    $ClosetOutfitName = %this.getOutfitNameForHanger(%hanger);
    update();
    if ((getCurrentTab() SPC name $= "CLOSET")) {
        thumbnails.setSelectedThumbs();
    }
    updateVisibleAvatar();
};
function ClosetItemPopup::onSelect(%this, %unused, %entries) {
    if ((ClosetItemsFrame SPC category $= %entries)) {
        return;
    }
    category = %entries @ ClosetItemsFrame;
    if ($gUpdatingClosetItemPopupFromThumbnailsSetDrawers) {
        $gUpdatingClosetItemPopupFromThumbnailsSetDrawers = 0;
        return;
    }
    if (tabClosetInitialized) {
        update();
    }
    getParent().scrollToTop();
};
function ClosetBrandPopup::onSelect(%this, %unused, %entries) {
    if ((ClosetItemsFrame SPC brand $= %entries)) {
        return;
    }
    brand = %entries @ ClosetItemsFrame;
    if (tabClosetInitialized) {
        update();
    }
    getParent().scrollToTop();
};
function ClosetWhatYoureWearingList::onCreatedChild(%this, %child) {
    %currentTabName = name;
    getCurrentTab();
    profile = GuiControl @ new ""() @ "ClosetLtBackgroundProfile";
    0;
    horizSizing = ClosetTabs @ "right";
    vertSizing = "bottom";
    position = "0 0";
    extent = "228 36";
    minExtent = "1 1";
    sluggishness = -1;
    visible = 1;
    %background = ;
    profile = GuiControl @ new ""() @ "ClosetHiliteProfile";
    0;
    horizSizing = "right";
    vertSizing = "bottom";
    position = "0 0";
    extent = "228 36";
    minExtent = "1 1";
    sluggishness = -1;
    visible = 0;
    %hilite = ;
    profile = GuiMLTextCtrl @ new ""() @ "ClosetSmallLinkProfile";
    0;
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
    %itemDesc = ;
    %itemDesc.bindClassName();
    profile = GuiBitmapButtonCtrl @ new ""() @ "GuiDefaultProfile";
    0;
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
    %closeBox = ;
    profile = GuiBitmapCtrl @ new ""() @ "ETSNonModalProfile";
    0;
    horizSizing = "right";
    vertSizing = "bottom";
    position = "211 19";
    extent = "16 16";
    modulationColor = "255 255 255 100";
    %ugcStatusIcon = ;
    profile = GuiBitmapCtrl @ new ""() @ "ETSNonModalProfile";
    0;
    horizSizing = "right";
    vertSizing = "bottom";
    position = "88 16";
    extent = "20 20";
    minExtent = "1 1";
    sluggishness = -1;
    visible = 1;
    bitmap = "";
    modulationColor = "255 255 255 100";
    %expiringIcon = ;
    profile = GuiMLTextCtrl @ new ""() @ "ClosetSmallLinkProfile";
    0;
    horizSizing = "right";
    vertSizing = "bottom";
    position = "80 18";
    if ((%currentTabName $= "MY DESIGNS")) {
    }
    extent = 18.0 @ (0.0 - 146.0) @ " " @ 15;
    %authorText = ;
    %child.add(%background);
    %child.add(%hilite);
    %child.add(%ugcStatusIcon);
    %child.add(%itemDesc);
    %child.add(%expiringIcon);
    %child.add(%closeBox);
    %child.add(%authorText);
    background = %background @ %child;
    hiliteCtrl = %hilite @ %child;
    desc = %itemDesc @ %child;
    ugcStatusIcon = %ugcStatusIcon @ %child;
    expiringIcon = %expiringIcon @ %child;
    closeBox = %closeBox @ %child;
    authorText = %authorText @ %child;
    sku = 0 @ %child;
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
    sku = %sku @ %child;
    %si = %sku.findBySku();
    SkuManager;
    if (!(SkuManager SPC %sku.getPropSkus() $= "")) {
        if (%sku.isInstrumentSku()) {
            %drawerAction = "  (no animation)";
            InstrumentRegistryClient;
        }
        if (isDoingPropAction) {
            %drawerAction = "  <a:gamelink stopPropAction>stop animation</a>";
            ClosetGui;
        }
        %drawerAction = "  <a:gamelink startPropAction>start animation</a>";
    }
    desc.setText(%child @ "<a:gamelink " @ %sku @ ">" @ %si @ descShrt @ "</a>" @ "<br><spush><font:Arial:12><color:00000099>" @ %si.getUserFacingDrawerName() @ %drawerAction @ "<spop>");
    if (!(%si SPC author $= "")) {
        %playerEncoded = urlEncode(stripUnprintables(author));
        %si;
        %profileURL = $Net::ProfileURL @ %playerEncoded;
        authorText.setText(%child @ "<just:right><font:Arial:12><color:00000044><linkcolor:00000066>" @ "by <a:gamelink " @ %profileURL @ ">" @ %si @ author @ "</a>");
    }
    authorText.setText("");
    if (!(%si SPC expireTime $= "")) {
        expiringIcon.setBitmap("platform/client/ui/expiring_icon");
        expiringIcon.setVisible(1);
    }
    expiringIcon.setVisible(0);
    %currentTabName = name;
    getCurrentTab();
    if ((ClosetTabs SPC %currentTabName $= "MY DESIGNS")) {
        ugcStatusIcon.setBitmap(ClosetGui_MyShop_GetSkuUGCStatusIcon(%sku));
        ugcStatusIcon.setVisible(1);
    }
    ugcStatusIcon.setVisible(0);
    if (filterByRemovable) {
        %showRemoveButton = drwrName.isOptionalDrawer();
        %si;
    }
    %showRemoveButton = 1;
    SkuManager;
    closeBox.setActive(%showRemoveButton);
    %this.hiliteCell(0, (1.0 - %this.getCount()));
    %this.reseatChildren();
};
function ClosetWhatYoureWearingList::setSkus(%this, %skus) {
    %propSku = $ClosetOutfitName[$ClosetSkusOutfit @ $ClosetOutfitName].getFirstPropSku();
    SkuManager;
    if ((%propSku $= "")) {
    }
    if ((%this != lastPropSku)) {
        stopPropAction();
    }
    lastPropSku = %propSku @ %propSku @ %this;
    if ((%propSku $= "")) {
        $player.setGenre("p");
    }
    %instrument = %propSku.getInstrumentBySku();
    InstrumentRegistryClient;
    if ((%instrument $= "")) {
        $player.setGenre("p");
    }
    $player.setGenre(genre);
    %this.setNumChildren(0);
    if ((0.0 == getWordCount(%skus))) {
        "(wearing none)".setTextWithStyle();
        if (isObject()) {
            0.setVisible();
        }
    }
    "".setTextWithStyle();
    if (isObject()) {
        1.setVisible();
    }
    %skus = %skus.sortSkusByDrawer();
    SkuManager;
    %count = getWordCount(%skus);
    MyShopCopyToOutfitText;
    %i = 0;
    MyShopCopyToOutfitText;
    if ((%count < %i)) {
        %this.addSku(getWord(%skus, %i));
        %i = (1.0 + %i);
        ClosetWhatYoureWearingNone;
    }
    skus = (%count < %i) @ %skus @ %this;
    MyShopCopyToOutfitText;
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
    if ((ClosetWhatYoureWearingList SPC %url $= "stopPropAction")) {
        stopPropAction();
        $ClosetOutfitName[$ClosetSkusOutfit @ $ClosetOutfitName].refresh();
    }
    if ((getCurrentTab() SPC name $= "CLOSET")) {
        scroll.scrollToSku(%url);
    }
    if ((getCurrentTab() SPC name $= "MY DESIGNS")) {
        ClosetGui_MyShop_ToggleCurrentSku(%url);
    }
};
function ClosetGUI_ToggleSku_Closet(%sku) {
    %drawer = drwrName;
    %sku.findBySku();
    %removable = %drawer.isOptionalDrawer();
    SkuManager;
    if (%sku.isBodySku()) {
        %wordLoc = findWord($ClosetSkusBody, %sku);
        SkuManager;
        if ((0.0 >= %wordLoc)) {
            if (%removable) {
                $ClosetSkusBody = removeWord($ClosetSkusBody, %wordLoc);
                SkuManager;
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
