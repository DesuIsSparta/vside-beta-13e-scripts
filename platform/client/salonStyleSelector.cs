function SalonStyleSelector::open(%this)
{
    PlayGui.ensureAdded(%this);
    %this.Initialize();
    if (!%this.isVisible())
    {
        %this.setVisible(1);
    }
    PlayGui.focusAndRaise(%this);
    return ;
}
function SalonStyleSelector::close(%this)
{
    if ($gSalonChairCurrent != 0)
    {
        return 1;
    }
    %this.setVisible(0);
    PlayGui.focusTopWindow();
    return 1;
}
function SalonStyleSelector::Initialize(%this)
{
    if (%this.initialized)
    {
        return ;
    }
    %this.initialized = 1;
    if (isObject(SalonStyleSelector.skuGuiList))
    {
        SalonStyleSelector.skuGuiList.delete();
    }
    if (isObject(SalonStyleSelector.noSkuGuiText))
    {
        SalonStyleSelector.noSkuGuiText.delete();
    }
    %SSS_Height = 200;
    %SSS_Width = 220;
    %SSS_ButtonWidth = 20;
    %SSS_ButtonSpacing = 6;
    %SSS_ButtonMargin = %SSS_ButtonWidth + (2 * %SSS_ButtonSpacing);
    %this.minExtent = %SSS_Width SPC 110;
    %this.resize(%SSS_Width, %SSS_Height);
    %gc = new GuiTextCtrl(gePropsWindowTitle)
    {
        profile = "ETSShadowTextNonModalProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "5 0";
        extent = %SSS_Width - 10 SPC 18;
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
    %gc = new GuiWindowCtrl()
    {
        profile = "DottedWindowDkProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "0 18";
        extent = %SSS_Width SPC 1;
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
    %gc = new GuiBitmapButtonCtrl()
    {
        position = %SSS_Width - 18 SPC 5;
        extent = 13 SPC 13;
        bitmap = "platform/client/buttons/close_m";
        command = "SalonStyleSelector.close();";
    };
    %this.add(%gc);
    %this.closeButton = %gc;
    %gc = new GuiBitmapCtrl(SalonStyleSelectorChair)
    {
        profile = "GuiDefaultProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = %SSS_ButtonSpacing SPC 22;
        extent = "20 20";
        minExtent = "8 8";
        sluggishness = -1;
        visible = 1;
        bitmap = "";
        tooltip = "Your Salon Station";
    };
    %this.add(%gc);
    %gc = new GuiVariableWidthButtonCtrl(ShowPropsButton)
    {
        profile = "BracketButton19Profile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = %SSS_ButtonMargin SPC 22;
        extent = %SSS_Width - (2 * %SSS_ButtonMargin) SPC 19;
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
    %gc = new GuiBitmapCtrl(SalonStyleSelectorProp)
    {
        profile = "GuiDefaultProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = (%SSS_Width - %SSS_ButtonWidth) - %SSS_ButtonSpacing SPC 22;
        extent = "20 20";
        minExtent = "8 8";
        sluggishness = -1;
        visible = 1;
        bitmap = "";
        tooltip = "Your Styling Prop";
    };
    %this.add(%gc);
    %gc = new GuiScrollCtrl()
    {
        profile = "ETSScrollProfile";
        horizSizing = "width";
        vertSizing = "height";
        position = "7 48";
        extent = %SSS_Width - 13 SPC %SSS_Height - 61;
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
    SalonStyleSelector.skuGuiList = new GuiArray2Ctrl()
    {
        profile = "GuiDefaultProfile";
        horizSizing = "width";
        vertSizing = "bottom";
        position = "1 1";
        extent = %SSS_Width - 22 SPC 8;
        minExtent = %SSS_Width - 22 SPC 8;
        sluggishness = -1;
        visible = 1;
        canHilite = 0;
        allowAutoFirstResponderUpdates = 1;
        fitParentWidth = 1;
        childrenClassName = "GuiControl";
        inRows = 0;
        numRowsOrCols = 1;
        spacing = 5;
    };
    %gc.add(SalonStyleSelector.skuGuiList);
    SalonStyleSelector.noSkuGuiText = new GuiMLTextCtrl()
    {
        profile = "ETSShadowTextNonModalProfile";
        horizSizing = "width";
        vertSizing = "bottom";
        position = "1 1";
        extent = %SSS_Width - 22 SPC 8;
        minExtent = %SSS_Width - 22 SPC 8;
        sluggishness = -1;
        visible = 1;
        canHilite = 1;
        allowAutoFirstResponderUpdates = 1;
        text = "";
    };
    %gc.add(SalonStyleSelector.noSkuGuiText);
    %this.add(%gc);
    %left = 50;
    %top = (getWord(PlayGui.getExtent(), 1) - getWord(%this.getExtent(), 1)) / 2;
    %this.reposition(%left, %top);
    return ;
}
function ShowPropsButton::onClick(%this)
{
    if ($player.hasAvailableProp())
    {
        toggleClosetItemCategory("props");
    }
    else
    {
        %salonCode = $SALON_CHAIR_DEF_DESTCODE[SalonStyleSelector.lastTypeOfSalon];
        %amInSalon = $gCurrentStoreName $= %salonCode;
        %callback = "";
        if (%amInSalon)
        {
            %msg = $MsgCat::shops["NO-PROPS-GO-TO-" @ %salonCode @ "-BODY-IN"];
            %callback = "toggleStore();";
        }
        else
        {
            %msg = $MsgCat::shops["NO-PROPS-GO-TO-" @ %salonCode @ "-BODY-OUT"];
            %callback = "vurlOperation(\"" @ $gDestinationVurls[%salonCode] @ "\");";
        }
        MessageBoxYesNo($MsgCat::shops["NO-PROPS-GO-TO-" @ %salonCode @ "-TITLE"], %msg, %callback, "");
    }
    return ;
}
