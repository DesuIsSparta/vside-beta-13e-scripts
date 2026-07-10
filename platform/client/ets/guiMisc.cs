function GuiMLTextCtrl::onURL(%this, %url) {
    if ((getWord(%url, 0) $= "gamelink")) {
        %url = restWords(%url);
    }
    if ((%url $= "")) {
        return;
    }
    if (!(strnicmp(%url, "vside:/", 7))) {
        vurlOperation(%url);
        return;
    }
    gotoWebPage(%url);
};
function GuiMLTextCtrl::massageURL(%url) {
    if ((0.0 == strstr(%url, "http://"))) {
        %url = getSubStr(%url, 7, 1000000);
    }
    return %url;
};
function GuiArray2Ctrl::scrollToRowByCell(%this, %cell) {
    %cellIdx = %this.getObjectIndex(%cell);
    %this.scrollToRowByCellIndex(%cellIdx);
};
function GuiArray2Ctrl::scrollToRowByCellIndex(%this, %cellIdx) {
    %cellHeight = (%this.spacing + getWord(%this.childrenExtent, 1));
    %ypos = (getWord(%this.getPosition(), 1) - 1.0);
    %closestRow = mFloor((0.5 + (%cellHeight / %ypos)));
    %targetRow = mFloor((%this.numRowsOrCols / %cellIdx));
    if ((0.0 < %cellIdx)) {
        %targetRow = %closestRow;
    }
    %visibleRows = mFloor((%cellHeight / getWord(%this.getParent().getExtent(), 1)));
    %dRows = (%closestRow - %targetRow);
    if ((0.0 < %dRows)) {
        %targetRow = %targetRow;
    }
    if ((%visibleRows < %dRows)) {
        %targetRow = %closestRow;
    }
    %targetRow = (1.0 + (%visibleRows - %targetRow));
    %this.getParent().scrollTo(0, (%targetRow * %cellHeight));
};
function GuiControl::blinkSet(%this, %mode, %periodOffMS, %periodOnMS, %param) {
    if (!(%this.origPoint $= "")) {
        %this.resize(getWord(%this.origPoint, 0), getWord(%this.origPoint, 1), getWord(%this.origExtnt, 0), getWord(%this.origExtnt, 1));
    }
    if ((%mode $= "none")) {
    }
    %mode = %mode;
    "";
    %this.blinkMode = %mode;
    %this.blinkPeriodOffMS = %periodOffMS;
    %this.blinkPeriodOnMS = %periodOnMS;
    %this.blinkParam = %param;
    %this.blinksRemaining = 15;
    %this.origPoint = "";
    %this.blinkStateSet(0);
    if (!(%mode $= "")) {
    }
    if ((50.0 > %periodOnMS)) {
        %this.blinkDo();
    }
};
function GuiControl::blinkSetRemaining(%this, %remaining) {
    %this.blinksRemaining = %remaining;
};
function GuiControl::blinkDo(%this) {
    cancel(%this.blinkTimer);
    %this.blinkTimer = "";
    %newState = (%this.blinkState - 1.0);
    if (%newState) {
    }
    %period = %this.blinkPeriodOffMS;
    %this.blinkPeriodOnMS;
    if ((0.0 == %newState)) {
        %this.blinksRemaining = (1.0 - %this.blinksRemaining);
    }
    if ((50.0 >= %period)) {
    }
    if ((0.0 > %this.blinksRemaining)) {
        %this.blinkStateSet(%newState);
        %this.blinkTimer = %this.schedule(%period, "blinkDo");
    }
    %this.blinkStateSet(0);
    if ((0.0 > %period)) {
    }
    if ((50.0 < %period)) {
        error(getScopeName() @ " " @ "- period too small:" @ " " @ %period);
    }
};
function GuiControl::blinkStateSet(%this, %state) {
    %cmd = "GuiControl_blinkStateSet_" @ %this.blinkMode;
    if (isFunction(%cmd)) {
        call(%cmd, %this, %state);
    }
    if (!(%this.blinkMode $= "")) {
        error(getScopeName() @ " " @ "- Unknown mode:" @ " " @ %this.blinkMode @ " " @ getTrace());
    }
    %this.blinkPeriodOnMS = 0;
    %this.blinkPeriodOffMS = 0;
};
function GuiControl_blinkStateSet_Bounce(%this, %state) {
    if ((%state == %this.blinkState)) {
        return;
    }
    %this.blinkState = %state;
    if ((%this.origPoint $= "")) {
        %this.origPoint = %this.getPosition();
        %this.origExtnt = %this.getExtent();
    }
    if ((0.0 == %state)) {
        %newPoint = %this.origPoint;
        %newExtnt = %this.origExtnt;
    }
    if ((1.0 == %state)) {
        %newPoint = VectorAdd(%this.origPoint, getWords(%this.blinkParam, 0, 1));
        %newExtnt = VectorAdd(%this.origExtnt, getWords(%this.blinkParam, 2, 3));
    }
    %this.resize(getWord(%newPoint, 0), getWord(%newPoint, 1), getWord(%newExtnt, 0), getWord(%newExtnt, 1));
};
function GuiMLTextCtrl::applyBaseTextWithStyle(%this, %style) {
    %this.setValue(mlStyle(%this.baseText, %style));
};
function GuiMLTextCtrl::applyBaseText(%this) {
    %this.applyBaseTextWithStyle(%this.style);
};
function GuiMLTextCtrl::setTextWithStyle(%this, %text, %style) {
    if (!(isDefined("%style"))) {
    }
    if ((%style $= "")) {
        %style = %this.style;
    }
    %this.setText(mlStyle(%text, %style));
};
function GuiControl::onSetFirstResponder(%this) {
    %ctrl = %this;
    if (%this.hasFieldValue("hiliteProxy")) {
        if (isObject(%this.hiliteProxy)) {
            %ctrl = %this.hiliteProxy;
        }
    }
    hiliteControl(%ctrl);
};
function GuiControl::onClearFirstResponder(%this) {
    if (!(isObject(Canvas.getFirstResponder()))) {
        hiliteControl(0);
    }
};
function GuiPopUpMenuCtrl::onSetFirstResponder(%this) {
    hiliteControl(%this, 1);
};
function GuiPopUp2MenuCtrl::onSetFirstResponder(%this) {
    hiliteControl(%this, 1);
};
function hiliteControl(%ctrl, %inParent) {
    if (isObject(GuiEditorGui)) {
    }
    if ((GuiEditorGui.getId() == Canvas.getContent())) {
        return;
    }
    if (!(isDefined("%inParent"))) {
        %inParent = 0;
    }
    if (isObject(%ctrl)) {
    }
    if (%ctrl.canHilite) {
    }
    if (%ctrl.isActive()) {
        if (!(isObject(HiliteWindow))) {
            new GuiWindowCtrl(HiliteWindow) {
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
        }
        if (%inParent) {
            %parent = HiliteWindow.getParent(%ctrl);
            if (isObject(%parent)) {
                %parent.add();
                %parent.pushToBack();
                %offset = 1;
                HiliteWindow;
                %targetPosX = (%offset - getWord(%ctrl.getPosition(), 0));
                HiliteWindow;
                %targetPosY = (%offset - getWord(%ctrl.getPosition(), 1));
                %targetExtX = ((%offset * 2.0) + getWord(%ctrl.getExtent(), 0));
                %targetExtY = ((%offset * 2.0) + getWord(%ctrl.getExtent(), 1));
            }
        }
        %ctrl.add();
        %offset = 1;
        HiliteWindow;
        %targetPosX = (%offset - 0.0);
        %targetPosY = (%offset - 0.0);
        %targetExtX = ((%offset * 2.0) + getWord(%ctrl.getExtent(), 0));
        %targetExtY = ((%offset * 2.0) + getWord(%ctrl.getExtent(), 1));
        HiliteWindow.setVisible(1);
        HiliteWindow.setTrgPosition(%targetPosX @ " " @ %targetPosY);
        HiliteWindow.setTrgExtent(%targetExtX @ " " @ %targetExtY);
        hiliteCtrl = %ctrl @ HiliteWindow;
    }
    if (isObject(HiliteWindow)) {
        HiliteWindow.delete();
    }
};
function getHiliteCtrl() {
    if (isObject(HiliteWindow)) {
    }
    if (HiliteWindow.isVisible()) {
    }
    return "";
};
function GuiControl::isHiliteCtrl(%this) {
    if (isObject(HiliteWindow)) {
    }
    if (HiliteWindow.isVisible()) {
    }
    return (%this.getId() == HiliteWindow.getId(hiliteCtrl));
};
$gToolTipDelay = 500;
function GuiControl::onMouseEnterBounds(%this) {
    if (!(%this.tooltip $= "")) {
        cancel(%this.tooltiptimer);
        %this.tooltiptimer = %this.schedule($gToolTipDelay, "showToolTip");
    }
};
function GuiControl::showToolTip(%this, %toolTip) {
    if (!($UserPref::UI::ShowTooltips)) {
        return;
    }
    if (isObject(ToolTipCtrl)) {
        ToolTipCtrl.delete();
    }
    if (!(isDefined("%tooltip"))) {
        %toolTip = %this.tooltip;
    }
    %cursorPos = Canvas.getCursorPos();
    %posX = getWord(%cursorPos, 0);
    %posY = (22.0 + getWord(%cursorPos, 1));
    %extX = getStrWidth(%toolTip);
    %extY = 16;
    %coords = onscreenCoordinates(%posX, %posY, (8.0 + %extX), %extY);
    %posX = getWord(%coords, 0);
    %posY = getWord(%coords, 1);
    new GuiControl(ToolTipCtrl) {
        profile = "ToolTipProfile";
        position = %posX @ " " @ %posY;
        extent = (8.0 + %extX) @ " " @ (0.0 + %extY);
        minExtent = "1 1";
    };
    ToolTipCtrl.add(ToolTipCtrl.getContent(Canvas));
};
function GuiControl::hideToolTip(%this) {
    if (isObject(ToolTipCtrl)) {
        ToolTipCtrl.setVisible(0);
    }
    cancel(%this.tooltiptimer);
    %this.tooltiptimer = 0;
};
function GuiControl::onMouseLeaveBounds(%this) {
    %this.hideToolTip();
};
function GuiControl::onMouseDown(%this) {
    %this.hideToolTip();
};
function GuiControl::onDialogPush(%this) {
    %this.hideToolTip();
};
function GuiControl::onDialogPop(%this) {
    %this.hideToolTip();
};
function GuiMLTextCtrl::onMouseOverTooltip(%this, %toolTip) {
    cancel(%this.tooltiptimer);
    %this.tooltiptimer = 0;
    if ((%toolTip $= "")) {
        %this.hideToolTip();
    }
    %this.tooltiptimer = %this.schedule($gToolTipDelay, "showToolTip", %toolTip);
};
function CanvasDragHiliteCtrl::onReachedTarget(%this) {
    %this.setVisible(0);
};
function Canvas::getDragHiliteCtrl(%this) {
    if (!(isObject(%this.dragHiliteCtrl))) {
        %this.dragHiliteCtrl = new GuiControl(CanvasDragHiliteCtrl) {
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
};
function Canvas::onDragAndDropStart(%this, %dragCtrl, %mousePos) {
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
};
function Canvas::centerDragHiliteAroundCursor(%this) {
    if (!(isObject(%this.dragCtrl))) {
        return;
    }
    %width = getWord(%this.dragCtrl.getExtent(), 0);
    %height = getWord(%this.dragCtrl.getExtent(), 1);
    %startX = ((2.0 / %width) + getWord(%this.dragCtrl.getScreenPosition(), 0));
    %startY = ((2.0 / %height) + getWord(%this.dragCtrl.getScreenPosition(), 1));
    %this.startingDragPos = %startX @ " " @ %startY;
    %dragHiliteCtrl = %this.getDragHiliteCtrl();
    %mousePos = %this.getCursorPos();
    %dragHiliteCtrl.reposition(((2.0 / %width) - getWord(%mousePos, 0)), ((2.0 / %height) - getWord(%mousePos, 1)));
};
function Canvas::onDragAndDropMove(%this, %dragCtrl, %mousePos) {
    %dragHiliteCtrl = %this.getDragHiliteCtrl();
    %xPos = (getWord(%this.startingDragPos, 0) - (getWord(%mousePos, 0) + getWord(%dragCtrl.getScreenPosition(), 0)));
    %ypos = (getWord(%this.startingDragPos, 1) - (getWord(%mousePos, 1) + getWord(%dragCtrl.getScreenPosition(), 1)));
    %dragHiliteCtrl.reposition(%xPos, %ypos);
};
function Canvas::onDragAndDropEnd(%this, %dragCtrl, %dropAccepted) {
    %dragHiliteCtrl = %this.getDragHiliteCtrl();
    if (%dropAccepted) {
        %dragHiliteCtrl.setVisible(0);
    }
    %xPos = getWord(%dragCtrl.getScreenPosition(), 0);
    %ypos = getWord(%dragCtrl.getScreenPosition(), 1);
    %dragHiliteCtrl.setTrgPosition(%xPos, %ypos);
    if (isObject(Canvas.getFirstResponder())) {
        Canvas.getFirstResponder().makeFirstResponder(1);
    }
};
function onDragAndDropCtrl(%make) {
    %dragCtrl = Canvas.getDragControl();
    if (isObject(%dragCtrl)) {
    }
    if (%dragCtrl.hasMethod("dragAndDropCtrl")) {
        %dragCtrl.dragAndDropCtrl(%make);
    }
};
function GuiControl::makeVisualClone(%this) {
    return new GuiControl("") {
        profile = 0 @ "DragAndDropProfile";
        horizSizing = "width";
        vertSizing = "height";
        position = "0 0";
        extent = %this.getExtent();
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
    };;
};
$Conv::TypingIndicatorState = -(1.0);
$Conv::CelebOpenIndicatorState = -(1.0);
$Conv::CelebCloseIndicatorState = -(1.0);
$Conv::AffinityOpenIndicatorState = -(1.0);
$Conv::AffinityCloseIndicatorState = -(1.0);
function animateConversationTypingIndicator() {
    if ((-(1.0) == $Conv::TypingIndicatorState)) {
        %n = -(1.0);
        %n = (1.0 + %n);
        %n["_   " @ $Conv::TypingIndicators] = ;
        %n = (1.0 + %n);
        %n[" _  " @ $Conv::TypingIndicators] = ;
        %n = (1.0 + %n);
        %n["  _ " @ $Conv::TypingIndicators] = ;
        %n = (1.0 + %n);
        %n["   _" @ $Conv::TypingIndicators] = ;
        $Conv::TypingIndicatorsNum = (1.0 + %n);
        $Conv::TypingIndicatorState = (1.0 - $Conv::TypingIndicatorsNum);
    }
    $Conv::TypingIndicatorState = ($Conv::TypingIndicatorsNum % (1.0 + $Conv::TypingIndicatorState));
    $Conv::typingIndicator = $Conv::TypingIndicatorState[$Conv::TypingIndicators @ $Conv::TypingIndicatorState];
    %celebIndicatorOn = 0;
    if (%celebIndicatorOn) {
        if ((-(1.0) == $Conv::CelebOpenIndicatorState)) {
            %n = -(1.0);
            %n = (1.0 + %n);
            %n["" @ $Conv::CelebOpenIndicators] = ;
            %n = (1.0 + %n);
            %n["* " @ $Conv::CelebOpenIndicators] = ;
            %n = (1.0 + %n);
            %n["*  " @ $Conv::CelebOpenIndicators] = ;
            %n = (1.0 + %n);
            %n["*   " @ $Conv::CelebOpenIndicators] = ;
            %n = (1.0 + %n);
            %n["*    " @ $Conv::CelebOpenIndicators] = ;
            $Conv::CelebOpenIndicatorsNum = (1.0 + %n);
            $Conv::CelebOpenIndicatorState = (1.0 - $Conv::CelebOpenIndicatorsNum);
        }
        $Conv::CelebOpenIndicatorState = ($Conv::CelebOpenIndicatorsNum % (1.0 + $Conv::CelebOpenIndicatorState));
        $Conv::celebOpenIndicator = $Conv::CelebOpenIndicatorState[$Conv::CelebOpenIndicators @ $Conv::CelebOpenIndicatorState];
        if ((-(1.0) == $Conv::CelebCloseIndicatorState)) {
            %n = -(1.0);
            %n = (1.0 + %n);
            %n["" @ $Conv::CelebCloseIndicators] = ;
            %n = (1.0 + %n);
            %n[" *" @ $Conv::CelebCloseIndicators] = ;
            %n = (1.0 + %n);
            %n["  *" @ $Conv::CelebCloseIndicators] = ;
            %n = (1.0 + %n);
            %n["   *" @ $Conv::CelebCloseIndicators] = ;
            %n = (1.0 + %n);
            %n["    *" @ $Conv::CelebCloseIndicators] = ;
            $Conv::CelebCloseIndicatorsNum = (1.0 + %n);
            $Conv::CelebCloseIndicatorState = (1.0 - $Conv::CelebCloseIndicatorsNum);
        }
        $Conv::CelebCloseIndicatorState = ($Conv::CelebCloseIndicatorsNum % (1.0 + $Conv::CelebCloseIndicatorState));
        $Conv::celebCloseIndicator = $Conv::CelebCloseIndicatorState[$Conv::CelebCloseIndicators @ $Conv::CelebCloseIndicatorState];
    }
    if ((-(1.0) == $Conv::AffinityOpenIndicatorState)) {
        %n = -(1.0);
        %n = (1.0 + %n);
        %n["" @ $Conv::AffinityOpenIndicators] = ;
        %n = (1.0 + %n);
        %n["(" @ $Conv::AffinityOpenIndicators] = ;
        %n = (1.0 + %n);
        %n["(:" @ $Conv::AffinityOpenIndicators] = ;
        %n = (1.0 + %n);
        %n["(: " @ $Conv::AffinityOpenIndicators] = ;
        %n = (1.0 + %n);
        %n["(:  " @ $Conv::AffinityOpenIndicators] = ;
        %n = (1.0 + %n);
        %n["(:   " @ $Conv::AffinityOpenIndicators] = ;
        %n = (1.0 + %n);
        %n[":    " @ $Conv::AffinityOpenIndicators] = ;
        $Conv::AffinityOpenIndicatorsNum = (1.0 + %n);
        $Conv::AffinityOpenIndicatorState = (1.0 - $Conv::AffinityOpenIndicatorsNum);
    }
    $Conv::AffinityOpenIndicatorState = ($Conv::AffinityOpenIndicatorsNum % (1.0 + $Conv::AffinityOpenIndicatorState));
    $Conv::affinityOpenIndicator = $Conv::AffinityOpenIndicatorState[$Conv::AffinityOpenIndicators @ $Conv::AffinityOpenIndicatorState];
    if ((-(1.0) == $Conv::AffinityCloseIndicatorState)) {
        %n = -(1.0);
        %n = (1.0 + %n);
        %n["" @ $Conv::AffinityCloseIndicators] = ;
        %n = (1.0 + %n);
        %n[")" @ $Conv::AffinityCloseIndicators] = ;
        %n = (1.0 + %n);
        %n[":)" @ $Conv::AffinityCloseIndicators] = ;
        %n = (1.0 + %n);
        %n[" :)" @ $Conv::AffinityCloseIndicators] = ;
        %n = (1.0 + %n);
        %n["  :)" @ $Conv::AffinityCloseIndicators] = ;
        %n = (1.0 + %n);
        %n["   :)" @ $Conv::AffinityCloseIndicators] = ;
        %n = (1.0 + %n);
        %n["    :" @ $Conv::AffinityCloseIndicators] = ;
        $Conv::AffinityCloseIndicatorsNum = (1.0 + %n);
        $Conv::AffinityCloseIndicatorState = (1.0 - $Conv::AffinityCloseIndicatorsNum);
    }
    $Conv::AffinityCloseIndicatorState = ($Conv::AffinityCloseIndicatorsNum % (1.0 + $Conv::AffinityCloseIndicatorState));
    $Conv::affinityCloseIndicator = $Conv::AffinityCloseIndicatorState[$Conv::AffinityCloseIndicators @ $Conv::AffinityCloseIndicatorState];
};
$Conv::animateConversationTypingIndicatorTimerID = 0;
$Conv::animateConversationTypingIndicatorTimerPeriod = 400;
function animateConversationTypingIndicatorTimer() {
    cancel($Conv::animateConversationTypingIndicatorTimerID);
    $Conv::animateConversationTypingIndicatorTimerID = 0;
    animateConversationTypingIndicator();
    $Conv::animateConversationTypingIndicatorTimerID = schedule($Conv::animateConversationTypingIndicatorTimerPeriod, 0, "animateConversationTypingIndicatorTimer");
};
animateConversationTypingIndicatorTimer();
function GuiControl::alignToBottom(%this) {
    %posX = getWord(%this.getPosition(), 0);
    %posY = getWord(%this.getParent().getExtent(), 1);
    %posY = (getWord(%this.getExtent(), 1) - %posY);
    %this.reposition(%posX, %posY);
};
function GuiControl::alignToTop(%this) {
    %posX = getWord(%this.getPosition(), 0);
    %posY = 0;
    %this.reposition(%posX, %posY);
};
function GuiControl::alignToLeft(%this) {
    %posX = 0;
    %posY = getWord(%this.getPosition(), 1);
    %this.reposition(%posX, %posY);
};
function GuiControl::alignToRight(%this) {
    %posX = getWord(%this.getParent().getExtent(), 0);
    %posX = (getWord(%this.getExtent(), 0) - %posX);
    %posY = getWord(%this.getPosition(), 1);
    %this.reposition(%posX, %posY);
};
function GuiControl::alignToCenterX(%this) {
    %posX = getWord(%this.getParent().getExtent(), 0);
    %posX = (getWord(%this.getExtent(), 0) - %posX);
    %posX = (2.0 / %posX);
    %posY = getWord(%this.getPosition(), 1);
    %this.reposition(%posX, %posY);
};
function GuiControl::alignToCenterY(%this) {
    %posX = getWord(%this.getPosition(), 0);
    %posY = getWord(%this.getParent().getExtent(), 1);
    %posY = (getWord(%this.getExtent(), 1) - %posY);
    %posY = (2.0 / %posY);
    %this.reposition(%posX, %posY);
};
function GuiControl::alignToCenterXY(%this) {
    %posX = getWord(%this.getParent().getExtent(), 0);
    %posX = (getWord(%this.getExtent(), 0) - %posX);
    %posX = (2.0 / %posX);
    %posY = getWord(%this.getParent().getExtent(), 1);
    %posY = (getWord(%this.getExtent(), 1) - %posY);
    %posY = (2.0 / %posY);
    %this.reposition(%posX, %posY);
};
function GuiControl::fitInParentAsBitmap(%this) {
    if ((%this.fitInParentAlign $= "")) {
        return;
    }
    %this.fitSize();
    %this.fitInParent(%this.fitInParentAlign);
};
function GuiControl::globalToLocal(%this, %point) {
    %upperLeft = %this.getScreenPosition();
    %x = (getWord(%upperLeft, 0) - getWord(%point, 0));
    %y = (getWord(%upperLeft, 1) - getWord(%point, 1));
    return %x @ " " @ %y;
};
function GuiControl::dumpTreeVerbose(%this) {
    %this._dumpTreeVerboseRecursive("");
};
function GuiControl::_dumpTreeVerboseRecursive(%this, %indent) {
    echo(%indent @ getDebugString(%this));
    echo(%indent @ %this.getPosition() @ " " @ %this.getExtent());
    %num = %this.getCount();
    %n = 0;
    if ((%num < %n)) {
        %child = %this.getObject(%n);
        %child._dumpTreeVerboseRecursive(%indent @ "  ");
        %n = (1.0 + %n);
    }
};
function GuiControl::reparent(%this, %newParent, %newPosition, %newExtent, %newProfile) {
    %newParent.add(%this);
    if ((%newExtent $= "")) {
        %newExtent = %this.getExtent();
    }
    %this.reshape(%newPosition, %newExtent);
    if (!(%newProfile $= "")) {
        %this.setProfile(%newProfile);
    }
};
function GuiControl::reparentSameSize(%this, %newParent, %newProfile) {
    %pos = "0 0";
    %ext = %newParent.getExtent();
    %this.reparent(%newParent, %pos, %ext, %newProfile);
};
function GuiControl::FlashVisibility(%this, %numTimes, %periodMS) {
    %this.flashTicksRemaining = (2.0 * %numTimes);
    %this.flashTickPeriod = %periodMS;
    %this.flashVisibilityTick();
};
function GuiControl::flashVisibilityTick(%this) {
    cancel(%this.flashTickTimerID);
    %this.flashTickTimerID = "";
    if ((%this.flashTicksRemaining $= "")) {
    }
    if ((0.0 == %this.flashTicksRemaining)) {
        %this.flashTicksRemaining = "";
        %this.flashTickPeriod = "";
        %this.setVisible(1);
    }
    %this.flashTicksRemaining = (1.0 - %this.flashTicksRemaining);
    %this.setVisible(!(%this.isVisible()));
    %this.flashTickTimerID = %this.schedule(%this.flashTickPeriod, "flashVisibilityTick");
};
function generic_takeSnapshotReally(%previewBitmapCtrl) {
    %regionCtrl = %previewBitmapCtrl.snap_regionCtrl;
    %filenameBase = %previewBitmapCtrl.snap_fnBase;
    %filenameExt = %previewBitmapCtrl.snap_fnExt;
    %tookPhoto = snapshotTool::snapControl(%regionCtrl, %filenameBase @ %filenameExt);
    %previewBitmapCtrl.setBitmap("");
    if (!(%tookPhoto)) {
        MessageBoxOK("Can't take snapshot!", "Unable to create snapshot. Please post a bug report in the forums. Thank you!", "");
    }
    %topMargin = 60;
    %bottomMargin = -(10.0);
    %leftMargin = 0;
    %rightMargin = 0;
    %playerIDs = TheShapeNameHud.getPlayerIDsInViewAndInRangeAndInFrame((%leftMargin - getWord(%regionCtrl.getScreenPosition(), 0)), (%topMargin - getWord(%regionCtrl.getScreenPosition(), 1)), (%rightMargin + (%leftMargin + getWord(%regionCtrl.getExtent(), 0))), (%bottomMargin + (%topMargin + getWord(%regionCtrl.getExtent(), 1))));
    %numPlayers = getWordCount(%playerIDs);
    %playerNames = "";
    %n = 0;
    if ((%numPlayers < %n)) {
        %playerNames = %playerNames @ "\t" @ getWord(%playerIDs, %n).getShapeName();
        %n = (1.0 + %n);
    }
    %playerNames = trim(%playerNames);
    (%numPlayers < %n);
    %previewBitmapCtrl.playersInViewNames = %playerNames;
    removeFile(%filenameBase @ %filenameExt);
    addFile(%filenameBase @ %filenameExt);
    %previewBitmapCtrl.setBitmap(%filenameBase);
    alxPlay(AudioProfile_Shutter);
    commandToServer('FireEventPlayerTakesAPicture');
    %previewBitmapCtrl.onSnapshotDone(%tookPhoto);
    return %tookPhoto;
};
function hideABunchOfControls(%list) {
    %n = (1.0 - getWordCount(%list));
    if ((0.0 >= %n)) {
        %ctrl = getWord(%list, %n);
        if (!(isObject(%ctrl))) {
            error(getScopeName() @ " " @ "- invalid control:" @ " " @ %ctrl @ " " @ getTrace());
        }
        %ctrl.hiding_originalVisibility = %ctrl.isVisible();
        %ctrl.setVisible(0);
        %n = (1.0 - %n);
    }
};
function restoreABunchOfControls(%list) {
    %list = trim(%list);
    %n = (1.0 - getWordCount(%list));
    if ((0.0 >= %n)) {
        %ctrl = getWord(%list, %n);
        if (!(isObject(%ctrl))) {
            error(getScopeName() @ " " @ "- invalid control:" @ " " @ %ctrl @ " " @ getTrace());
        }
        %ctrl.setVisible(%ctrl.hiding_originalVisibility);
        %ctrl.hiding_originalVisibility = "";
        %n = (1.0 - %n);
    }
};
