function ClosetTabs::fillBodyTab(%this) {
    %theTab = "BODY".getTabWithName(%this);
    if (!(isObject(%theTab))) {
        return;
    }
    new GuiBitmapCtrl("") {
        profile = "GuiDefaultProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "26 26";
        extent = "571 37";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        bitmap = "platform/client/ui/closet_tabs_bracket";
    };.add(%theTab);
    %featuresLabel = new GuiTextCtrl("") {
        profile = "ClosetTitleProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "181 64";
        extent = "72 20";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        text = "Features";
        maxLength = 255;
    };
    %featuresLabel.add(%theTab);
    %featuresPopup = new GuiPopUp2MenuCtrl(BodyFeaturesPopup) {
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
    %featuresPopup.add(%theTab);
    %featuresPopup.rebuildPopupList();
    new GuiSliderCtrl(BodyHeightSlider) {
        profile = new GuiBitmapCtrl("") {
        profile = new GuiBitmapCtrl("") {
        profile = new GuiBitmapCtrl("") {
        profile = new GuiBitmapCtrl("") {
        profile = new GuiBitmapCtrl("") {
        profile = new GuiVariableWidthButtonCtrl(BodyHeightDisplayText) {
        profile = "BracketButton15InertProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "2 5";
        extent = "55 15";
        minExtent = "1 1";
        visible = 1;
        command = "";
        text = "---";
        groupNum = -1;
        buttonType = "PushButton";
        helpTag = 0;
        drawText = 1;
    }; @ "GuiDefaultProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "59 77";
        extent = "30 10";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        bitmap = "platform/client/ui/divot";
    }; @ "GuiDefaultProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "59 141";
        extent = "30 10";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        bitmap = "platform/client/ui/divot";
    }; @ "GuiDefaultProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "59 201";
        extent = "30 10";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        bitmap = "platform/client/ui/divot";
    }; @ "GuiDefaultProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "65 5";
        extent = "19 16";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        bitmap = "platform/client/ui/hslider_top";
    }; @ "GuiDefaultProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "65 270";
        extent = "19 17";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        bitmap = "platform/client/ui/hslider_bottom";
    }; @ "DottedSliderProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "64 10";
        extent = "21 270";
        minExtent = "1 1";
        altCommand = "BodyHeightSlider.valueChanged();";
        sluggishness = -1;
        visible = 1;
        range = $Pref::Wardrobe::playerHeightMin @ " " @ $Pref::Wardrobe::playerHeightMax;
        defaultValues = "0.930 1.000 1.075";
        snapToDefaultRangeRatio = 0.04;
        ticks = 10;
        value = $UserPref::Player::height;
    };
    %itemsFrame = new GuiControl(BodyItemsFrame) {
        profile = "GuiDefaultProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "22 120";
        extent = "467 350";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
    };
    new GuiControl(BodyStanceButtons) {
        profile = new GuiControl(BodyHeightFrame) {
        profile = "GuiDefaultProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "368 62";
        extent = "90 290";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 0;
    }; @ "GuiDefaultProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "19 41";
        extent = "333 26";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 0;
    };
    %itemsInfoText = new GuiTextCtrl("") {
        profile = new GuiVariableWidthButtonCtrl(BodyStanceButtonPreppy) {
        profile = new GuiVariableWidthButtonCtrl(BodyStanceButtonIndie) {
        profile = new GuiVariableWidthButtonCtrl(BodyStanceButtonHipHop) {
        profile = "BracketButton19NonDefaultProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "0 0";
        extent = "60 19";
        minExtent = "1 1";
        visible = 1;
        command = "ClosetGui.selectGenre(\"h\");";
        text = "Hip-Hop";
        groupNum = $BodyStanceGroup;
        buttonType = "RadioButton";
        helpTag = 0;
        drawText = 1;
    }; @ "BracketButton19NonDefaultProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "68 0";
        extent = "60 19";
        minExtent = "1 1";
        visible = 1;
        command = "ClosetGui.selectGenre(\"i\");";
        text = "Indie";
        groupNum = $BodyStanceGroup;
        buttonType = "RadioButton";
        helpTag = 0;
        drawText = 1;
    }; @ "BracketButton19NonDefaultProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "136 0";
        extent = "60 19";
        minExtent = "1 1";
        visible = 1;
        command = "ClosetGui.selectGenre(\"p\");";
        text = "Preppy";
        groupNum = $BodyStanceGroup;
        buttonType = "RadioButton";
        helpTag = 0;
        drawText = 1;
    }; @ "ClosetLeftInfoProfile";
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
    %itemsRangeText.add(%itemsFrame);
    %theTab.rangeText = %itemsRangeText;
    %itemsScroll = new GuiScrollCtrl("") {
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
    "ClosetItemsScroll".bindClassName(%itemsScroll);
    %theTab.itemsScroll = %itemsScroll;
    %thumbnails = new GuiArray2Ctrl(ClosetThumbnailsBody) {
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
    new GuiMLTextCtrl(BodyShortDescText) {
        profile = "ClosetLeftInfoProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "692 84";
        extent = "242 25";
        lineSpacing = -(3.0);
    };.add(%theTab);
    new GuiMLTextCtrl(BodyLongDescText) {
        profile = "ClosetLeftInfoProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "692 106";
        extent = "173 32";
        lineSpacing = -(3.0);
    };.add(%theTab);
    %doneButton = new GuiVariableWidthButtonCtrl("") {
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
    %cancelButton = new GuiVariableWidthButtonCtrl("") {
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
    %doneButton.add(%theTab);
    %theTab.doneButton = %doneButton;
    %cancelButton.add(%theTab);
    %theTab.cancelButton = %cancelButton;
    0.SetSelected(%featuresPopup);
    %this.tabBodyInitialized = 1;
    BodyItemsFrame.update();
};
function ClosetTabs::updateBodyTabDisplay(%this) {
    if (!(%this.tabBodyInitialized)) {
        return;
    }
    $UserPref::Player::height.setValue(BodyHeightSlider);
    BodyHeightSlider.valueChanged();
};
function BodyItemsFrame::update(%this) {
    strlwr(%this.features).get(ThumbCategories).setDrawers(%this.thumbnails);
    1.makeFirstResponder(%this.thumbnails);
    if ((%this.features $= "Height")) {
        1.setVisible(BodyHeightFrame);
        BodyHeightFrame.pushToBack(BodyHeightFrame.getParent());
    }
    0.setVisible(BodyHeightFrame);
    if ((%this.features $= "Stance")) {
        1.setVisible(BodyStanceButtons);
        BodyStanceButtons.pushToBack(BodyStanceButtons.getParent());
    }
    0.setVisible(BodyStanceButtons);
};
function BodyHeightDisplayText::update(%this) {
    %myHeight = ($UserPref::Player::height * [$player.getGender()]);
    $gClosetNeutralHeightInches;
    %myFeet = mFloor((%myHeight / 12.0));
    %myInches = mFloor((%myHeight - (%myFeet * 12.0)));
    %myFeet @ "'" @ " " @ %myInches @ "\"".setText(%this);
};
function BodyHeightSlider::valueChanged(%this) {
    %h = %this.getValue();
    $UserPref::Player::height = %h;
    %sxy = (((%h - 1.0) * $Pref::Wardrobe::playerHeightWidthFactor) + 1.0);
    %sxy @ " " @ %sxy @ " " @ %h.setScale($player);
    BodyHeightDisplayText.update();
};
function BodyFeaturesPopup::onSelect(%this, %unused, %entries) {
    if ((BodyItemsFrame.features $= %entries)) {
        return;
    }
    BodyItemsFrame.features = %entries;
    if (ClosetTabs.tabBodyInitialized) {
        BodyItemsFrame.update();
    }
    ClosetThumbnailsBody.getParent().scrollToTop();
};
function BodyFeaturesPopup::rebuildPopupList(%this) {
    %this.clear();
    %categoryList = "All Features" @ "\t" @ "Skin" @ "\t" @ "Face" @ "\t" @ "Eyes" @ "\t" @ "Hair";
    %n = 0;
    while ((%n < getFieldCount(%categoryList))) {
        %category = getField(%categoryList, %n);
        if (Closet::skuListHasCategory($Player::inventory, %category)) {
            %category.add(%this);
        }
        %n = (%n + 1.0);
    }
    "Height".add(%this);
    0.SetSelected(%this);
};
function ClosetGUI_ToggleSku_Body(%sku) {
    ClosetGUI_ToggleSku_Closet(%sku);
};
