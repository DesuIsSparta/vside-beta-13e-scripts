function SalonStyleSelector::open(%this) {
    %this.ensureAdded(PlayGui);
    %this.Initialize();
    if (!(%this.isVisible())) {
        1.setVisible(%this);
    }
    %this.focusAndRaise(PlayGui);
};
function SalonStyleSelector::close(%this) {
    if (($gSalonChairCurrent != 0.0)) {
        return 1;
    }
    0.setVisible(%this);
    PlayGui.focusTopWindow();
    return 1;
};
function SalonStyleSelector::Initialize(%this) {
    if (%this.initialized) {
        return;
    }
    %this.initialized = 1;
    if (isObject(SalonStyleSelector, %this.skuGuiList)) {
        %this.skuGuiList.delete(SalonStyleSelector);
    }
    if (isObject(SalonStyleSelector, %this.noSkuGuiText)) {
        %this.noSkuGuiText.delete(SalonStyleSelector);
    }
    %SSS_Height = 200;
    %SSS_Width = 220;
    %SSS_ButtonWidth = 20;
    %SSS_ButtonSpacing = 6;
    %SSS_ButtonMargin = (%SSS_ButtonWidth + (2.0 * %SSS_ButtonSpacing));
    %this.minExtent = %SSS_Width @ " " @ 110;
    %SSS_Height.resize(%this, %SSS_Width);
    %gc = new GuiTextCtrl(gePropsWindowTitle) {
        profile = "ETSShadowTextNonModalProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "5 0";
        extent = (%SSS_Width - 10.0) @ " " @ 18;
        minExtent = "8 2";
        sluggishness = -1;
        visible = 1;
        canHilite = 1;
        allowAutoFirstResponderUpdates = 1;
        text = "vSalon - Pick a Style!";
        maxLength = 255;
        tooltiptimer = 0;
    };
    %gc.add(%this);
    %gc = new GuiWindowCtrl("") {
        profile = 0 @ "DottedWindowDkProfile";
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
    %gc.add(%this);
    %gc = new GuiBitmapButtonCtrl("") {
        position = 0 @ (%SSS_Width - 18.0) @ " " @ 5;
        extent = 13 @ " " @ 13;
        bitmap = "platform/client/buttons/close_m";
        command = "SalonStyleSelector.close();";
    };
    %gc.add(%this);
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
    %gc.add(%this);
    %gc = new GuiVariableWidthButtonCtrl(ShowPropsButton) {
        profile = "BracketButton19Profile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = %SSS_ButtonMargin @ " " @ 22;
        extent = (%SSS_Width - (2.0 * %SSS_ButtonMargin)) @ " " @ 19;
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
    %gc.add(%this);
    %gc = new GuiBitmapCtrl(SalonStyleSelectorProp) {
        profile = "GuiDefaultProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = ((%SSS_Width - %SSS_ButtonWidth) - %SSS_ButtonSpacing) @ " " @ 22;
        extent = "20 20";
        minExtent = "8 8";
        sluggishness = -1;
        visible = 1;
        bitmap = "";
        tooltip = "Your Styling Prop";
    };
    %gc.add(%this);
    %gc = new GuiScrollCtrl("") {
        profile = 0 @ "ETSScrollProfile";
        horizSizing = "width";
        vertSizing = "height";
        position = "7 48";
        extent = (%SSS_Width - 13.0) @ " " @ (%SSS_Height - 61.0);
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
    skuGuiList = new GuiArray2Ctrl("") {
        profile = 0 @ "GuiDefaultProfile";
        horizSizing = "width";
        vertSizing = "bottom";
        position = "1 1";
        extent = (%SSS_Width - 22.0) @ " " @ 8;
        minExtent = (%SSS_Width - 22.0) @ " " @ 8;
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
    skuGuiList.add(%gc, SalonStyleSelector);
    noSkuGuiText = new GuiMLTextCtrl("") {
        profile = 0 @ "ETSShadowTextNonModalProfile";
        horizSizing = "width";
        vertSizing = "bottom";
        position = "1 1";
        extent = (%SSS_Width - 22.0) @ " " @ 8;
        minExtent = (%SSS_Width - 22.0) @ " " @ 8;
        sluggishness = -1;
        visible = 1;
        canHilite = 1;
        allowAutoFirstResponderUpdates = 1;
        text = "";
    }; @ SalonStyleSelector
    noSkuGuiText.add(%gc, SalonStyleSelector);
    %gc.add(%this);
    %left = 50;
    %top = ((getWord(PlayGui.getExtent(), 1) - getWord(%this.getExtent(), 1)) / 2.0);
    %top.reposition(%this, %left);
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
