function SalonStyleSelector::open(%this) {
    %this.ensureAdded();
    %this.Initialize();
    if (!(%this.isVisible())) {
        %this.setVisible(1);
    }
    %this.focusAndRaise();
};
function SalonStyleSelector::close(%this) {
    if ((0.0 != $gSalonChairCurrent)) {
        return 1;
    }
    %this.setVisible(0);
    PlayGui.focusTopWindow();
    return 1;
};
function SalonStyleSelector::Initialize(%this) {
    if (%this.initialized) {
        return;
    }
    %this.initialized = 1;
    if (isObject(%this.skuGuiList)) {
        %this.skuGuiList.delete();
    }
    if (isObject(%this.noSkuGuiText)) {
        %this.noSkuGuiText.delete();
    }
    %SSS_Height = 200;
    SalonStyleSelector;
    %SSS_Width = 220;
    SalonStyleSelector;
    %SSS_ButtonWidth = 20;
    SalonStyleSelector;
    %SSS_ButtonSpacing = 6;
    SalonStyleSelector;
    %SSS_ButtonMargin = ((%SSS_ButtonSpacing * 2.0) + %SSS_ButtonWidth);
    %this.minExtent = %SSS_Width @ " " @ 110;
    %this.resize(%SSS_Width, %SSS_Height);
    %gc = new GuiTextCtrl(gePropsWindowTitle) {
        profile = "ETSShadowTextNonModalProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "5 0";
        extent = (10.0 - %SSS_Width) @ " " @ 18;
        minExtent = "8 2";
        sluggishness = -1;
        visible = 1;
        canHilite = 1;
        allowAutoFirstResponderUpdates = 1;
        text = "vSalon - Pick a Style!";
        maxLength = 255;
        tooltiptimer = 0;
    };
    %this.add(%gc);
    0;
    %gc = new ""() {
        profile = GuiWindowCtrl @ "DottedWindowDkProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "0 18";
        extent = %SSS_Width @ " " @ 1;
        minExtent = "8 1";
        sluggishness = -1;
        resizeWidth = 0;
        resizeHeight = 0;
        canMove = 0;
        canMinimize = 0;
        canMaximize = 0;
        visible = 1;
        canHilite = 0;
    };
    %this.add(%gc);
    0;
    %gc = new ""() {
        position = GuiBitmapButtonCtrl @ (18.0 - %SSS_Width) @ " " @ 5;
        extent = 13 @ " " @ 13;
        bitmap = "platform/client/buttons/close_m";
        command = "SalonStyleSelector.close();";
    };
    %this.add(%gc);
    %this.closeButton = %gc;
    %gc = new GuiBitmapCtrl(SalonStyleSelectorChair) {
        profile = "GuiDefaultProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = %SSS_ButtonSpacing @ " " @ 22;
        extent = "20 20";
        minExtent = "8 8";
        sluggishness = -1;
        visible = 1;
        bitmap = "";
        tooltip = "Your Salon Station";
    };
    %this.add(%gc);
    %gc = new GuiVariableWidthButtonCtrl(ShowPropsButton) {
        profile = "BracketButton19Profile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = %SSS_ButtonMargin @ " " @ 22;
        extent = ((%SSS_ButtonMargin * 2.0) - %SSS_Width) @ " " @ 19;
        minExtent = "8 8";
        sluggishness = -1;
        visible = 1;
        command = "ShowPropsButton.onClick();";
        canHilite = 1;
        allowAutoFirstResponderUpdates = 1;
        text = "Choose Styling Prop";
        groupNum = -1;
        buttonType = "PushButton";
        depressed = 0;
        mouseOver = 0;
        helpTag = 0;
        repeatDelayMS = 0;
        simpleStyle = 0;
        tickPeriodMS = 0;
        tooltiptimer = 0;
    };
    %this.add(%gc);
    %gc = new GuiBitmapCtrl(SalonStyleSelectorProp) {
        profile = "GuiDefaultProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = (%SSS_ButtonSpacing - (%SSS_ButtonWidth - %SSS_Width)) @ " " @ 22;
        extent = "20 20";
        minExtent = "8 8";
        sluggishness = -1;
        visible = 1;
        bitmap = "";
        tooltip = "Your Styling Prop";
    };
    %this.add(%gc);
    0;
    %gc = new ""() {
        profile = GuiScrollCtrl @ "ETSScrollProfile";
        horizSizing = "width";
        vertSizing = "height";
        position = "7 48";
        extent = (13.0 - %SSS_Width) @ " " @ (61.0 - %SSS_Height);
        minExtent = "8 2";
        sluggishness = -1;
        visible = 1;
        canHilite = 1;
        allowAutoFirstResponderUpdates = 1;
        willFirstRespond = 1;
        hScrollBar = "alwaysOff";
        vScrollBar = "dynamic";
        constantThumbHeight = 1;
        childMargin = "0 0";
        saneDrag = 1;
        scrollMultiplier = 1;
        stickyBottom = 0;
        tooltiptimer = 0;
    };
    0;
    skuGuiList = new ""() {
        profile = GuiArray2Ctrl @ "GuiDefaultProfile";
        horizSizing = "width";
        vertSizing = "bottom";
        position = "1 1";
        extent = (22.0 - %SSS_Width) @ " " @ 8;
        minExtent = (22.0 - %SSS_Width) @ " " @ 8;
        sluggishness = -1;
        visible = 1;
        canHilite = 0;
        allowAutoFirstResponderUpdates = 1;
        fitParentWidth = 1;
        childrenClassName = "GuiControl";
        inRows = 0;
        numRowsOrCols = 1;
        spacing = 5;
    }; @ SalonStyleSelector
    %gc.add(skuGuiList);
    0;
    noSkuGuiText = new ""() {
        profile = GuiMLTextCtrl @ "ETSShadowTextNonModalProfile";
        horizSizing = SalonStyleSelector @ "width";
        vertSizing = "bottom";
        position = "1 1";
        extent = (22.0 - %SSS_Width) @ " " @ 8;
        minExtent = (22.0 - %SSS_Width) @ " " @ 8;
        sluggishness = -1;
        visible = 1;
        canHilite = 1;
        allowAutoFirstResponderUpdates = 1;
        text = "";
    }; @ SalonStyleSelector
    %gc.add(noSkuGuiText);
    %this.add(%gc);
    %left = 50;
    SalonStyleSelector;
    %top = (2.0 / (getWord(%this.getExtent(), 1) - getWord(PlayGui.getExtent(), 1)));
    %this.reposition(%left, %top);
};
function ShowPropsButton::onClick(%this) {
    if ($player.hasAvailableProp()) {
        toggleClosetItemCategory("props");
    }
    %salonCode = $SALON_CHAIR_DEF_DESTCODE;
    %amInSalon = ($gCurrentStoreName $= %salonCode);
    %callback = "";
    if (%amInSalon) {
        %msg = %salonCode[$MsgCat::shops @ "NO-PROPS-GO-TO-" @ %salonCode @ "-BODY-IN"];
        %callback = "toggleStore();";
    }
    %msg = %salonCode[$MsgCat::shops @ "NO-PROPS-GO-TO-" @ %salonCode @ "-BODY-OUT"];
    %callback = "vurlOperation(\"" @ %salonCode[$gDestinationVurls @ %salonCode] @ "\");";
    MessageBoxYesNo(%salonCode[$MsgCat::shops @ "NO-PROPS-GO-TO-" @ %salonCode @ "-TITLE"], %msg, %callback, "");
};
