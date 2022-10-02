function ClosetTabs::fillBodyTab(%this)
{
    %theTab = %this.getTabWithName("BODY");
    if (!isObject(%theTab))
    {
        return ;
    }
    %featuresLabel = new GuiTextCtrl()
    {
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
    %theTab.add(%featuresLabel);
    %featuresPopup = new GuiPopUp2MenuCtrl(BodyFeaturesPopup)
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
    %theTab.add(%featuresPopup);
    %featuresPopup.rebuildPopupList();
    %itemsFrame = new GuiControl(BodyItemsFrame)
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
    %thumbnails = new GuiArray2Ctrl(ClosetThumbnailsBody)
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
    %featuresPopup.SetSelected(0);
    %this.tabBodyInitialized = 1;
    BodyItemsFrame.update();
    return ;
}
function ClosetTabs::updateBodyTabDisplay(%this)
{
    if (!%this.tabBodyInitialized)
    {
        return ;
    }
    BodyHeightSlider.setValue($UserPref::Player::height);
    BodyHeightSlider.valueChanged();
    return ;
}
function BodyItemsFrame::update(%this)
{
    %this.thumbnails.setDrawers(ThumbCategories.get(strlwr(%this.features)));
    %this.thumbnails.makeFirstResponder(1);
    if (%this.features $= "Height")
    {
        BodyHeightFrame.setVisible(1);
        BodyHeightFrame.getParent().pushToBack(BodyHeightFrame);
    }
    else
    {
        BodyHeightFrame.setVisible(0);
    }
    if (%this.features $= "Stance")
    {
        BodyStanceButtons.setVisible(1);
        BodyStanceButtons.getParent().pushToBack(BodyStanceButtons);
    }
    else
    {
        BodyStanceButtons.setVisible(0);
    }
    return ;
}
function BodyHeightDisplayText::update(%this)
{
    %myHeight = $UserPref::Player::height * $gClosetNeutralHeightInches[$player.getGender()];
    %myFeet = mFloor(%myHeight / 12);
    %myInches = mFloor(%myHeight - (%myFeet * 12));
    %this.setText(%myFeet @ "\'" SPC %myInches @ "\"");
    return ;
}
function BodyHeightSlider::valueChanged(%this)
{
    %h = %this.getValue();
    $UserPref::Player::height = %h;
    %sxy = ((%h - 1) * $Pref::Wardrobe::playerHeightWidthFactor) + 1;
    $player.setScale(%sxy SPC %sxy SPC %h);
    BodyHeightDisplayText.update();
    return ;
}
function BodyFeaturesPopup::onSelect(%this, %unused, %entries)
{
    if (BodyItemsFrame.features $= %entries)
    {
        return ;
    }
    BodyItemsFrame.features = %entries;
    if (ClosetTabs.tabBodyInitialized)
    {
        BodyItemsFrame.update();
    }
    ClosetThumbnailsBody.getParent().scrollToTop();
    return ;
}
function BodyFeaturesPopup::rebuildPopupList(%this)
{
    %this.clear();
    %categoryList = "All Features" TAB "Skin" TAB "Face" TAB "Eyes" TAB "Hair";
    %n = 0;
    while (%n < getFieldCount(%categoryList))
    {
        %category = getField(%categoryList, %n);
        if (Closet::skuListHasCategory($Player::inventory, %category))
        {
            %this.add(%category);
        }
        %n = %n + 1;
    }
    %this.add("Height");
    %this.SetSelected(0);
    return ;
}
function ClosetGUI_ToggleSku_Body(%sku)
{
    ClosetGUI_ToggleSku_Closet(%sku);
    return ;
}
