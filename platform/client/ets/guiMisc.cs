function GuiMLTextCtrl::onURL(%this, %url)
{
    if (getWord(%url, 0) $= "gamelink")
    {
        %url = restWords(%url);
    }
    if (%url $= "")
    {
        return ;
    }
    if (!strnicmp(%url, "vside:/", 7))
    {
        vurlOperation(%url);
        return ;
    }
    gotoWebPage(%url);
    return ;
}
function GuiMLTextCtrl::massageURL(%url)
{
    if (strstr(%url, "http://") == 0)
    {
        %url = getSubStr(%url, 7, 1000000);
    }
    return %url;
}
function GuiArray2Ctrl::scrollToRowByCell(%this, %cell)
{
    %cellIdx = %this.getObjectIndex(%cell);
    %this.scrollToRowByCellIndex(%cellIdx);
    return ;
}
function GuiArray2Ctrl::scrollToRowByCellIndex(%this, %cellIdx)
{
    %cellHeight = getWord(%this.childrenExtent, 1) + %this.spacing;
    %ypos = 1 - getWord(%this.getPosition(), 1);
    %closestRow = mFloor((%ypos / %cellHeight) + 0.5);
    %targetRow = mFloor(%cellIdx / %this.numRowsOrCols);
    if (%cellIdx < 0)
    {
        %targetRow = %closestRow;
    }
    %visibleRows = mFloor(getWord(%this.getParent().getExtent(), 1) / %cellHeight);
    %dRows = %targetRow - %closestRow;
    if (%dRows < 0)
    {
        %targetRow = %targetRow;
    }
    else
    {
        if (%dRows < %visibleRows)
        {
            %targetRow = %closestRow;
        }
        else
        {
            %targetRow = (%targetRow - %visibleRows) + 1;
        }
    }
    %this.getParent().scrollTo(0, %cellHeight * %targetRow);
    return ;
}
function GuiControl::blinkSet(%this, %mode, %periodOffMS, %periodOnMS, %param)
{
    if (!(%this.origPoint $= ""))
    {
        %this.resize(getWord(%this.origPoint, 0), getWord(%this.origPoint, 1), getWord(%this.origExtnt, 0), getWord(%this.origExtnt, 1));
    }
    %mode = %mode $= "none" ? "" : %mode;
    %this.blinkMode = %mode;
    %this.blinkPeriodOffMS = %periodOffMS;
    %this.blinkPeriodOnMS = %periodOnMS;
    %this.blinkParam = %param;
    %this.blinksRemaining = 15;
    %this.origPoint = "";
    %this.blinkStateSet(0);
    if (!((%mode $= "")) && (%periodOnMS > 50))
    {
        %this.blinkDo();
    }
    return ;
}
function GuiControl::blinkSetRemaining(%this, %remaining)
{
    %this.blinksRemaining = %remaining;
    return ;
}
function GuiControl::blinkDo(%this)
{
    cancel(%this.blinkTimer);
    %this.blinkTimer = "";
    %newState = 1 - %this.blinkState;
    %period = %newState ? %this : %this;
    if (%newState == 0)
    {
        %this.blinksRemaining = %this.blinksRemaining - 1;
    }
    if ((%period >= 50) && (%this.blinksRemaining > 0))
    {
        %this.blinkStateSet(%newState);
        %this.blinkTimer = %this.schedule(%period, "blinkDo");
    }
    else
    {
        %this.blinkStateSet(0);
        if ((%period > 0) && (%period < 50))
        {
            error(getScopeName() SPC "- period too small:" SPC %period);
        }
    }
    return ;
}
function GuiControl::blinkStateSet(%this, %state)
{
    %cmd = "GuiControl_blinkStateSet_" @ %this.blinkMode;
    if (isFunction(%cmd))
    {
        call(%cmd, %this, %state);
    }
    else
    {
        if (!(%this.blinkMode $= ""))
        {
            error(getScopeName() SPC "- Unknown mode:" SPC %this.blinkMode SPC getTrace());
        }
        %this.blinkPeriodOnMS = 0;
        %this.blinkPeriodOffMS = 0;
    }
    return ;
}
function GuiControl_blinkStateSet_Bounce(%this, %state)
{
    if (%this.blinkState == %state)
    {
        return ;
    }
    %this.blinkState = %state;
    if (%this.origPoint $= "")
    {
        %this.origPoint = %this.getPosition();
        %this.origExtnt = %this.getExtent();
    }
    if (%state == 0)
    {
        %newPoint = %this.origPoint;
        %newExtnt = %this.origExtnt;
    }
    else
    {
        if (%state == 1)
        {
            %newPoint = VectorAdd(%this.origPoint, getWords(%this.blinkParam, 0, 1));
            %newExtnt = VectorAdd(%this.origExtnt, getWords(%this.blinkParam, 2, 3));
        }
    }
    %this.resize(getWord(%newPoint, 0), getWord(%newPoint, 1), getWord(%newExtnt, 0), getWord(%newExtnt, 1));
    return ;
}
function GuiMLTextCtrl::applyBaseTextWithStyle(%this, %style)
{
    %this.setValue(mlStyle(%this.baseText, %style));
    return ;
}
function GuiMLTextCtrl::applyBaseText(%this)
{
    %this.applyBaseTextWithStyle(%this.style);
    return ;
}
function GuiMLTextCtrl::setTextWithStyle(%this, %text, %style)
{
    if (!isDefined("%style") && (%style $= ""))
    {
        %style = %this.style;
    }
    %this.setText(mlStyle(%text, %style));
    return ;
}
function GuiControl::onSetFirstResponder(%this)
{
    %ctrl = %this;
    if (%this.hasFieldValue("hiliteProxy"))
    {
        if (isObject(%this.hiliteProxy))
        {
            %ctrl = %this.hiliteProxy;
        }
    }
    hiliteControl(%ctrl);
    return ;
}
function GuiControl::onClearFirstResponder(%this)
{
    if (!isObject(Canvas.getFirstResponder()))
    {
        hiliteControl(0);
    }
    return ;
}
function GuiPopUpMenuCtrl::onSetFirstResponder(%this)
{
    hiliteControl(%this, 1);
    return ;
}
function GuiPopUp2MenuCtrl::onSetFirstResponder(%this)
{
    hiliteControl(%this, 1);
    return ;
}
function hiliteControl(%ctrl, %inParent)
{
    if (isObject(GuiEditorGui) && (Canvas.getContent() == GuiEditorGui.getId()))
    {
        return ;
    }
    if (!isDefined("%inParent"))
    {
        %inParent = 0;
    }
    if ((isObject(%ctrl) && %ctrl.canHilite) && %ctrl.isActive())
    {
        if (!isObject(HiliteWindow))
        {
        }
        new GuiWindowCtrl(HiliteWindow)
            {
                profile = "HiliteFrameProfile";
                horizSizing = "width";
                vertSizing = "height";
                position = "0 0";
                extent = "1 1";
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
        if (%inParent)
        {
            %parent = %ctrl.getParent();
            if (isObject(%parent))
            {
                %parent.add(HiliteWindow);
                %parent.pushToBack(HiliteWindow);
                %offset = 1;
                %targetPosX = getWord(%ctrl.getPosition(), 0) - %offset;
                %targetPosY = getWord(%ctrl.getPosition(), 1) - %offset;
                %targetExtX = getWord(%ctrl.getExtent(), 0) + (2 * %offset);
                %targetExtY = getWord(%ctrl.getExtent(), 1) + (2 * %offset);
            }
        }
        else
        {
            %ctrl.add(HiliteWindow);
            %offset = 1;
            %targetPosX = 0 - %offset;
            %targetPosY = 0 - %offset;
            %targetExtX = getWord(%ctrl.getExtent(), 0) + (2 * %offset);
            %targetExtY = getWord(%ctrl.getExtent(), 1) + (2 * %offset);
        }
        HiliteWindow.setVisible(1);
        HiliteWindow.setTrgPosition(%targetPosX SPC %targetPosY);
        HiliteWindow.setTrgExtent(%targetExtX SPC %targetExtY);
        HiliteWindow.hiliteCtrl = %ctrl;
    }
    else
    {
        if (isObject(HiliteWindow))
        {
            HiliteWindow.delete();
        }
    }
    return ;
}
function getHiliteCtrl()
{
    return isObject(HiliteWindow) && HiliteWindow.isVisible() ? HiliteWindow : "";
}
function GuiControl::isHiliteCtrl(%this)
{
    return (isObject(HiliteWindow) && HiliteWindow.isVisible()) && (HiliteWindow.hiliteCtrl.getId() == %this.getId());
}
$gToolTipDelay = 500;
function GuiControl::onMouseEnterBounds(%this)
{
    if (!(%this.tooltip $= ""))
    {
        cancel(%this.tooltiptimer);
        %this.tooltiptimer = %this.schedule($gToolTipDelay, "showToolTip");
    }
    return ;
}
function GuiControl::showToolTip(%this, %toolTip)
{
    if (!$UserPref::UI::ShowTooltips)
    {
        return ;
    }
    if (isObject(ToolTipCtrl))
    {
        ToolTipCtrl.delete();
    }
    if (!isDefined("%tooltip"))
    {
        %toolTip = %this.tooltip;
    }
    %cursorPos = Canvas.getCursorPos();
    %posX = getWord(%cursorPos, 0);
    %posY = getWord(%cursorPos, 1) + 22;
    %extX = getStrWidth(%toolTip);
    %extY = 16;
    %coords = onscreenCoordinates(%posX, %posY, %extX + 8, %extY);
    %posX = getWord(%coords, 0);
    %posY = getWord(%coords, 1);
    new GuiControl(ToolTipCtrl)
    {
        profile = "ToolTipProfile";
        position = %posX SPC %posY;
        extent = %extX + 8 SPC %extY + 0;
        minExtent = "1 1";
    };
    Canvas.getContent().add(ToolTipCtrl);
    return ;
}
function GuiControl::hideToolTip(%this)
{
    if (isObject(ToolTipCtrl))
    {
        ToolTipCtrl.setVisible(0);
    }
    cancel(%this.tooltiptimer);
    %this.tooltiptimer = 0;
    return ;
}
function GuiControl::onMouseLeaveBounds(%this)
{
    %this.hideToolTip();
    return ;
}
function GuiControl::onMouseDown(%this)
{
    %this.hideToolTip();
    return ;
}
function GuiControl::onDialogPush(%this)
{
    %this.hideToolTip();
    return ;
}
function GuiControl::onDialogPop(%this)
{
    %this.hideToolTip();
    return ;
}
function GuiMLTextCtrl::onMouseOverTooltip(%this, %toolTip)
{
    cancel(%this.tooltiptimer);
    %this.tooltiptimer = 0;
    if (%toolTip $= "")
    {
        %this.hideToolTip();
    }
    else
    {
        %this.tooltiptimer = %this.schedule($gToolTipDelay, "showToolTip", %toolTip);
    }
    return ;
}
function CanvasDragHiliteCtrl::onReachedTarget(%this)
{
    %this.setVisible(0);
    return ;
}
function Canvas::getDragHiliteCtrl(%this)
{
    if (!isObject(%this.dragHiliteCtrl))
    {
        %this.dragHiliteCtrl = new GuiControl(CanvasDragHiliteCtrl)
        {
            profile = "ETSNonModalProfile";
            horizSizing = "width";
            vertSizing = "height";
            position = "0 0";
            extent = "1 1";
            minExtent = "1 1";
            sluggishness = 0.25;
            visible = 1;
            trgReachedCommand = "$ThisControl.onReachedTarget();";
        };
    }
    return %this.dragHiliteCtrl;
}
function Canvas::onDragAndDropStart(%this, %dragCtrl, %mousePos)
{
    %this.startingDragPos = %mousePos;
    %this.dragCtrl = %dragCtrl;
    %dragHiliteCtrl = %this.getDragHiliteCtrl();
    %dragHiliteCtrl.reposition(getWord(%dragCtrl.getScreenPosition(), 0), getWord(%dragCtrl.getScreenPosition(), 1));
    %dragHiliteCtrl.resize(getWord(%dragCtrl.getExtent(), 0), getWord(%dragCtrl.getExtent(), 1));
    %dragHiliteCtrl.clear();
    %dragHiliteCtrl.add(%dragCtrl.makeVisualClone());
    %this.getContent().add(%dragHiliteCtrl);
    %this.getContent().pushToBack(%dragHiliteCtrl);
    %dragHiliteCtrl.setVisible(1);
    return ;
}
function Canvas::centerDragHiliteAroundCursor(%this)
{
    if (!isObject(%this.dragCtrl))
    {
        return ;
    }
    %width = getWord(%this.dragCtrl.getExtent(), 0);
    %height = getWord(%this.dragCtrl.getExtent(), 1);
    %startX = getWord(%this.dragCtrl.getScreenPosition(), 0) + (%width / 2);
    %startY = getWord(%this.dragCtrl.getScreenPosition(), 1) + (%height / 2);
    %this.startingDragPos = %startX SPC %startY;
    %dragHiliteCtrl = %this.getDragHiliteCtrl();
    %mousePos = %this.getCursorPos();
    %dragHiliteCtrl.reposition(getWord(%mousePos, 0) - (%width / 2), getWord(%mousePos, 1) - (%height / 2));
    return ;
}
function Canvas::onDragAndDropMove(%this, %dragCtrl, %mousePos)
{
    %dragHiliteCtrl = %this.getDragHiliteCtrl();
    %xPos = (getWord(%dragCtrl.getScreenPosition(), 0) + getWord(%mousePos, 0)) - getWord(%this.startingDragPos, 0);
    %ypos = (getWord(%dragCtrl.getScreenPosition(), 1) + getWord(%mousePos, 1)) - getWord(%this.startingDragPos, 1);
    %dragHiliteCtrl.reposition(%xPos, %ypos);
    return ;
}
function Canvas::onDragAndDropEnd(%this, %dragCtrl, %dropAccepted)
{
    %dragHiliteCtrl = %this.getDragHiliteCtrl();
    if (%dropAccepted)
    {
        %dragHiliteCtrl.setVisible(0);
    }
    else
    {
        %xPos = getWord(%dragCtrl.getScreenPosition(), 0);
        %ypos = getWord(%dragCtrl.getScreenPosition(), 1);
        %dragHiliteCtrl.setTrgPosition(%xPos, %ypos);
    }
    if (isObject(Canvas.getFirstResponder()))
    {
        Canvas.getFirstResponder().makeFirstResponder(1);
    }
    return ;
}
function onDragAndDropCtrl(%make)
{
    %dragCtrl = Canvas.getDragControl();
    if (isObject(%dragCtrl) && %dragCtrl.hasMethod("dragAndDropCtrl"))
    {
        %dragCtrl.dragAndDropCtrl(%make);
    }
    return ;
}
function GuiControl::makeVisualClone(%this)
{
    return new GuiControl()
    {
        profile = "DragAndDropProfile";
        horizSizing = "width";
        vertSizing = "height";
        position = "0 0";
        extent = %this.getExtent();
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
    };
    return ;
}
$Conv::TypingIndicatorState = -1;
$Conv::CelebOpenIndicatorState = -1;
$Conv::CelebCloseIndicatorState = -1;
$Conv::AffinityOpenIndicatorState = -1;
$Conv::AffinityCloseIndicatorState = -1;
function animateConversationTypingIndicator()
{
    if ($Conv::TypingIndicatorState == -1)
    {
        %n = -1;
        $Conv::TypingIndicators[%n = %n + 1] = "_   ";
        $Conv::TypingIndicators[%n = %n + 1] = " _  ";
        $Conv::TypingIndicators[%n = %n + 1] = "  _ ";
        $Conv::TypingIndicators[%n = %n + 1] = "   _";
        $Conv::TypingIndicatorsNum = %n + 1;
        $Conv::TypingIndicatorState = $Conv::TypingIndicatorsNum - 1;
    }
    $Conv::TypingIndicatorState = ($Conv::TypingIndicatorState + 1) % $Conv::TypingIndicatorsNum;
    $Conv::typingIndicator = $Conv::TypingIndicators[$Conv::TypingIndicatorState];
    %celebIndicatorOn = 0;
    if (%celebIndicatorOn)
    {
        if ($Conv::CelebOpenIndicatorState == -1)
        {
            %n = -1;
            $Conv::CelebOpenIndicators[%n = %n + 1] = "";
            $Conv::CelebOpenIndicators[%n = %n + 1] = "* ";
            $Conv::CelebOpenIndicators[%n = %n + 1] = "*  ";
            $Conv::CelebOpenIndicators[%n = %n + 1] = "*   ";
            $Conv::CelebOpenIndicators[%n = %n + 1] = "*    ";
            $Conv::CelebOpenIndicatorsNum = %n + 1;
            $Conv::CelebOpenIndicatorState = $Conv::CelebOpenIndicatorsNum - 1;
        }
        $Conv::CelebOpenIndicatorState = ($Conv::CelebOpenIndicatorState + 1) % $Conv::CelebOpenIndicatorsNum;
        $Conv::celebOpenIndicator = $Conv::CelebOpenIndicators[$Conv::CelebOpenIndicatorState];
        if ($Conv::CelebCloseIndicatorState == -1)
        {
            %n = -1;
            $Conv::CelebCloseIndicators[%n = %n + 1] = "";
            $Conv::CelebCloseIndicators[%n = %n + 1] = " *";
            $Conv::CelebCloseIndicators[%n = %n + 1] = "  *";
            $Conv::CelebCloseIndicators[%n = %n + 1] = "   *";
            $Conv::CelebCloseIndicators[%n = %n + 1] = "    *";
            $Conv::CelebCloseIndicatorsNum = %n + 1;
            $Conv::CelebCloseIndicatorState = $Conv::CelebCloseIndicatorsNum - 1;
        }
        $Conv::CelebCloseIndicatorState = ($Conv::CelebCloseIndicatorState + 1) % $Conv::CelebCloseIndicatorsNum;
        $Conv::celebCloseIndicator = $Conv::CelebCloseIndicators[$Conv::CelebCloseIndicatorState];
    }
    if ($Conv::AffinityOpenIndicatorState == -1)
    {
        %n = -1;
        $Conv::AffinityOpenIndicators[%n = %n + 1] = "";
        $Conv::AffinityOpenIndicators[%n = %n + 1] = "(";
        $Conv::AffinityOpenIndicators[%n = %n + 1] = "(:";
        $Conv::AffinityOpenIndicators[%n = %n + 1] = "(: ";
        $Conv::AffinityOpenIndicators[%n = %n + 1] = "(:  ";
        $Conv::AffinityOpenIndicators[%n = %n + 1] = "(:   ";
        $Conv::AffinityOpenIndicators[%n = %n + 1] = ":    ";
        $Conv::AffinityOpenIndicatorsNum = %n + 1;
        $Conv::AffinityOpenIndicatorState = $Conv::AffinityOpenIndicatorsNum - 1;
    }
    $Conv::AffinityOpenIndicatorState = ($Conv::AffinityOpenIndicatorState + 1) % $Conv::AffinityOpenIndicatorsNum;
    $Conv::affinityOpenIndicator = $Conv::AffinityOpenIndicators[$Conv::AffinityOpenIndicatorState];
    if ($Conv::AffinityCloseIndicatorState == -1)
    {
        %n = -1;
        $Conv::AffinityCloseIndicators[%n = %n + 1] = "";
        $Conv::AffinityCloseIndicators[%n = %n + 1] = ")";
        $Conv::AffinityCloseIndicators[%n = %n + 1] = ":)";
        $Conv::AffinityCloseIndicators[%n = %n + 1] = " :)";
        $Conv::AffinityCloseIndicators[%n = %n + 1] = "  :)";
        $Conv::AffinityCloseIndicators[%n = %n + 1] = "   :)";
        $Conv::AffinityCloseIndicators[%n = %n + 1] = "    :";
        $Conv::AffinityCloseIndicatorsNum = %n + 1;
        $Conv::AffinityCloseIndicatorState = $Conv::AffinityCloseIndicatorsNum - 1;
    }
    $Conv::AffinityCloseIndicatorState = ($Conv::AffinityCloseIndicatorState + 1) % $Conv::AffinityCloseIndicatorsNum;
    $Conv::affinityCloseIndicator = $Conv::AffinityCloseIndicators[$Conv::AffinityCloseIndicatorState];
    return ;
}
$Conv::animateConversationTypingIndicatorTimerID = 0;
$Conv::animateConversationTypingIndicatorTimerPeriod = 400;
function animateConversationTypingIndicatorTimer()
{
    cancel($Conv::animateConversationTypingIndicatorTimerID);
    $Conv::animateConversationTypingIndicatorTimerID = 0;
    animateConversationTypingIndicator();
    $Conv::animateConversationTypingIndicatorTimerID = schedule($Conv::animateConversationTypingIndicatorTimerPeriod, 0, "animateConversationTypingIndicatorTimer");
    return ;
}
animateConversationTypingIndicatorTimer();
function GuiControl::alignToBottom(%this)
{
    %posX = getWord(%this.getPosition(), 0);
    %posY = getWord(%this.getParent().getExtent(), 1);
    %posY = %posY - getWord(%this.getExtent(), 1);
    %this.reposition(%posX, %posY);
    return ;
}
function GuiControl::alignToTop(%this)
{
    %posX = getWord(%this.getPosition(), 0);
    %posY = 0;
    %this.reposition(%posX, %posY);
    return ;
}
function GuiControl::alignToLeft(%this)
{
    %posX = 0;
    %posY = getWord(%this.getPosition(), 1);
    %this.reposition(%posX, %posY);
    return ;
}
function GuiControl::alignToRight(%this)
{
    %posX = getWord(%this.getParent().getExtent(), 0);
    %posX = %posX - getWord(%this.getExtent(), 0);
    %posY = getWord(%this.getPosition(), 1);
    %this.reposition(%posX, %posY);
    return ;
}
function GuiControl::alignToCenterX(%this)
{
    %posX = getWord(%this.getParent().getExtent(), 0);
    %posX = %posX - getWord(%this.getExtent(), 0);
    %posX = %posX / 2;
    %posY = getWord(%this.getPosition(), 1);
    %this.reposition(%posX, %posY);
    return ;
}
function GuiControl::alignToCenterY(%this)
{
    %posX = getWord(%this.getPosition(), 0);
    %posY = getWord(%this.getParent().getExtent(), 1);
    %posY = %posY - getWord(%this.getExtent(), 1);
    %posY = %posY / 2;
    %this.reposition(%posX, %posY);
    return ;
}
function GuiControl::alignToCenterXY(%this)
{
    %posX = getWord(%this.getParent().getExtent(), 0);
    %posX = %posX - getWord(%this.getExtent(), 0);
    %posX = %posX / 2;
    %posY = getWord(%this.getParent().getExtent(), 1);
    %posY = %posY - getWord(%this.getExtent(), 1);
    %posY = %posY / 2;
    %this.reposition(%posX, %posY);
    return ;
}
function GuiControl::fitInParentAsBitmap(%this)
{
    if (%this.fitInParentAlign $= "")
    {
        return ;
    }
    %this.fitSize();
    %this.fitInParent(%this.fitInParentAlign);
    return ;
}
function GuiControl::globalToLocal(%this, %point)
{
    %upperLeft = %this.getScreenPosition();
    %x = getWord(%point, 0) - getWord(%upperLeft, 0);
    %y = getWord(%point, 1) - getWord(%upperLeft, 1);
    return %x SPC %y;
}
function GuiControl::dumpTreeVerbose(%this)
{
    %this._dumpTreeVerboseRecursive("");
    return ;
}
function GuiControl::_dumpTreeVerboseRecursive(%this, %indent)
{
    echo(%indent @ getDebugString(%this));
    echo(%indent @ %this.getPosition() SPC %this.getExtent());
    %num = %this.getCount();
    %n = 0;
    while (%n < %num)
    {
        %child = %this.getObject(%n);
        %child._dumpTreeVerboseRecursive(%indent @ "  ");
        %n = %n + 1;
    }
}

function GuiControl::reparent(%this, %newParent, %newPosition, %newExtent, %newProfile)
{
    %newParent.add(%this);
    if (%newExtent $= "")
    {
        %newExtent = %this.getExtent();
    }
    %this.reshape(%newPosition, %newExtent);
    if (!(%newProfile $= ""))
    {
        %this.setProfile(%newProfile);
    }
    return ;
}
function GuiControl::reparentSameSize(%this, %newParent, %newProfile)
{
    %pos = "0 0";
    %ext = %newParent.getExtent();
    %this.reparent(%newParent, %pos, %ext, %newProfile);
    return ;
}
function GuiControl::FlashVisibility(%this, %numTimes, %periodMS)
{
    %this.flashTicksRemaining = %numTimes * 2;
    %this.flashTickPeriod = %periodMS;
    %this.flashVisibilityTick();
    return ;
}
function GuiControl::flashVisibilityTick(%this)
{
    cancel(%this.flashTickTimerID);
    %this.flashTickTimerID = "";
    if ((%this.flashTicksRemaining $= "") && (%this.flashTicksRemaining == 0))
    {
        %this.flashTicksRemaining = "";
        %this.flashTickPeriod = "";
        %this.setVisible(1);
    }
    else
    {
        %this.flashTicksRemaining = %this.flashTicksRemaining - 1;
        %this.setVisible(!%this.isVisible());
        %this.flashTickTimerID = %this.schedule(%this.flashTickPeriod, "flashVisibilityTick");
    }
    return ;
}
function generic_takeSnapshotReally(%previewBitmapCtrl)
{
    %regionCtrl = %previewBitmapCtrl.snap_regionCtrl;
    %filenameBase = %previewBitmapCtrl.snap_fnBase;
    %filenameExt = %previewBitmapCtrl.snap_fnExt;
    %tookPhoto = snapshotTool::snapControl(%regionCtrl, %filenameBase @ %filenameExt);
    %previewBitmapCtrl.setBitmap("");
    if (!%tookPhoto)
    {
        MessageBoxOK("Can\'t take snapshot!", "Unable to create snapshot. Please post a bug report in the forums. Thank you!", "");
    }
    else
    {
        %topMargin = 60;
        %bottomMargin = -10;
        %leftMargin = 0;
        %rightMargin = 0;
        %playerIDs = TheShapeNameHud.getPlayerIDsInViewAndInRangeAndInFrame(getWord(%regionCtrl.getScreenPosition(), 0) - %leftMargin, getWord(%regionCtrl.getScreenPosition(), 1) - %topMargin, (getWord(%regionCtrl.getExtent(), 0) + %leftMargin) + %rightMargin, (getWord(%regionCtrl.getExtent(), 1) + %topMargin) + %bottomMargin);
        %numPlayers = getWordCount(%playerIDs);
        %playerNames = "";
        %n = 0;
        while (%n < %numPlayers)
        {
            %playerNames = %playerNames TAB getWord(%playerIDs, %n).getShapeName();
            %n = %n + 1;
        }
        %playerNames = trim(%playerNames);
        %previewBitmapCtrl.playersInViewNames = %playerNames;
        removeFile(%filenameBase @ %filenameExt);
        addFile(%filenameBase @ %filenameExt);
        %previewBitmapCtrl.setBitmap(%filenameBase);
        alxPlay(AudioProfile_Shutter);
        commandToServer('FireEventPlayerTakesAPicture');
    }
    %previewBitmapCtrl.onSnapshotDone(%tookPhoto);
    return %tookPhoto;
}
function hideABunchOfControls(%list)
{
    %n = getWordCount(%list) - 1;
    while (%n >= 0)
    {
        %ctrl = getWord(%list, %n);
        if (!isObject(%ctrl))
        {
            error(getScopeName() SPC "- invalid control:" SPC %ctrl SPC getTrace());
        }
        else
        {
            %ctrl.hiding_originalVisibility = %ctrl.isVisible();
            %ctrl.setVisible(0);
        }
        %n = %n - 1;
    }
}

function restoreABunchOfControls(%list)
{
    %list = trim(%list);
    %n = getWordCount(%list) - 1;
    while (%n >= 0)
    {
        %ctrl = getWord(%list, %n);
        if (!isObject(%ctrl))
        {
            error(getScopeName() SPC "- invalid control:" SPC %ctrl SPC getTrace());
        }
        else
        {
            %ctrl.setVisible(%ctrl.hiding_originalVisibility);
            %ctrl.hiding_originalVisibility = "";
        }
        %n = %n - 1;
    }
}


