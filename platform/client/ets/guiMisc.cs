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
    if ((strstr(%url, "http://") == 0.0)) {
        %url = getSubStr(%url, 7, 1000000);
    }
    return %url;
};
function GuiArray2Ctrl::scrollToRowByCell(%this, %cell) {
    %cellIdx = %cell.getObjectIndex(%this);
    %cellIdx.scrollToRowByCellIndex(%this);
};
function GuiArray2Ctrl::scrollToRowByCellIndex(%this, %cellIdx) {
    %cellHeight = (getWord(%this.childrenExtent, 1) + %this.spacing);
    %ypos = (1.0 - getWord(%this.getPosition(), 1));
    %closestRow = mFloor(((%ypos / %cellHeight) + 0.5));
    %targetRow = mFloor((%cellIdx / %this.numRowsOrCols));
    if ((%cellIdx < 0.0)) {
        %targetRow = %closestRow;
    }
    %visibleRows = mFloor((getWord(%this.getParent().getExtent(), 1) / %cellHeight));
    %dRows = (%targetRow - %closestRow);
    if ((%dRows < 0.0)) {
        %targetRow = %targetRow;
    }
    if ((%dRows < %visibleRows)) {
        %targetRow = %closestRow;
    }
    %targetRow = ((%targetRow - %visibleRows) + 1.0);
    (%cellHeight * %targetRow).scrollTo(%this.getParent(), 0);
};
function GuiControl::blinkSet(%this, %mode, %periodOffMS, %periodOnMS, %param) {
    if (!(%this.origPoint $= "")) {
        getWord(%this.origExtnt, 1).resize(%this, getWord(%this.origPoint, 0), getWord(%this.origPoint, 1), getWord(%this.origExtnt, 0));
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
    0.blinkStateSet(%this);
    if (!(%mode $= "")) {
    }
    if ((%periodOnMS > 50.0)) {
        %this.blinkDo();
    }
};
function GuiControl::blinkSetRemaining(%this, %remaining) {
    %this.blinksRemaining = %remaining;
};
function GuiControl::blinkDo(%this) {
    cancel(%this.blinkTimer);
    %this.blinkTimer = "";
    %newState = (1.0 - %this.blinkState);
    if (%newState) {
    }
    %period = %this.blinkPeriodOffMS;
    %this.blinkPeriodOnMS;
    if ((%newState == 0.0)) {
        %this.blinksRemaining = (%this.blinksRemaining - 1.0);
    }
    if ((%period >= 50.0)) {
    }
    if ((%this.blinksRemaining > 0.0)) {
        %newState.blinkStateSet(%this);
        %this.blinkTimer = "blinkDo".schedule(%this, %period);
    }
    0.blinkStateSet(%this);
    if ((%period > 0.0)) {
    }
    if ((%period < 50.0)) {
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
    if ((%this.blinkState == %state)) {
        return;
    }
    %this.blinkState = %state;
    if ((%this.origPoint $= "")) {
        %this.origPoint = %this.getPosition();
        %this.origExtnt = %this.getExtent();
    }
    if ((%state == 0.0)) {
        %newPoint = %this.origPoint;
        %newExtnt = %this.origExtnt;
    }
    if ((%state == 1.0)) {
        %newPoint = VectorAdd(%this.origPoint, getWords(%this.blinkParam, 0, 1));
        %newExtnt = VectorAdd(%this.origExtnt, getWords(%this.blinkParam, 2, 3));
    }
    getWord(%newExtnt, 1).resize(%this, getWord(%newPoint, 0), getWord(%newPoint, 1), getWord(%newExtnt, 0));
};
function GuiMLTextCtrl::applyBaseTextWithStyle(%this, %style) {
    mlStyle(%this.baseText, %style).setValue(%this);
};
function GuiMLTextCtrl::applyBaseText(%this) {
    %this.style.applyBaseTextWithStyle(%this);
};
function GuiMLTextCtrl::setTextWithStyle(%this, %text, %style) {
    if (!(isDefined("%style"))) {
    }
    if ((%style $= "")) {
        %style = %this.style;
    }
    mlStyle(%text, %style).setText(%this);
};
function GuiControl::onSetFirstResponder(%this) {
    %ctrl = %this;
    if ("hiliteProxy".hasFieldValue(%this) && isObject(%this.hiliteProxy)) {
        %ctrl = %this.hiliteProxy;
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
    if ((Canvas.getContent() == GuiEditorGui.getId())) {
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
            %parent = %ctrl.getParent();
            if (isObject(%parent)) {
                %parent.add();
                %parent.pushToBack();
                %offset = 1;
                HiliteWindow;
                %targetPosX = (getWord(%ctrl.getPosition(), 0) - %offset);
                HiliteWindow;
                %targetPosY = (getWord(%ctrl.getPosition(), 1) - %offset);
                %targetExtX = (getWord(%ctrl.getExtent(), 0) + (2.0 * %offset));
                %targetExtY = (getWord(%ctrl.getExtent(), 1) + (2.0 * %offset));
            }
        }
        %ctrl.add();
        %offset = 1;
        HiliteWindow;
        %targetPosX = (0.0 - %offset);
        %targetPosY = (0.0 - %offset);
        %targetExtX = (getWord(%ctrl.getExtent(), 0) + (2.0 * %offset));
        %targetExtY = (getWord(%ctrl.getExtent(), 1) + (2.0 * %offset));
        1.setVisible(HiliteWindow);
        %targetPosX @ " " @ %targetPosY.setTrgPosition(HiliteWindow);
        %targetExtX @ " " @ %targetExtY.setTrgExtent(HiliteWindow);
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
    return (hiliteCtrl.getId(HiliteWindow) == %this.getId());
};
$gToolTipDelay = 500;
function GuiControl::onMouseEnterBounds(%this) {
    if (!(%this.tooltip $= "")) {
        cancel(%this.tooltiptimer);
        %this.tooltiptimer = "showToolTip".schedule(%this, $gToolTipDelay);
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
    %posY = (getWord(%cursorPos, 1) + 22.0);
    %extX = getStrWidth(%toolTip);
    %extY = 16;
    %coords = onscreenCoordinates(%posX, %posY, (%extX + 8.0), %extY);
    %posX = getWord(%coords, 0);
    %posY = getWord(%coords, 1);
    new GuiControl(ToolTipCtrl) {
        profile = "ToolTipProfile";
        position = %posX @ " " @ %posY;
        extent = (%extX + 8.0) @ " " @ (%extY + 0.0);
        minExtent = "1 1";
    };
    Canvas.getContent().add();
};
function GuiControl::hideToolTip(%this) {
    if (isObject(ToolTipCtrl)) {
        0.setVisible(ToolTipCtrl);
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
    %this.tooltiptimer = %toolTip.schedule(%this, $gToolTipDelay, "showToolTip");
};
function CanvasDragHiliteCtrl::onReachedTarget(%this) {
    0.setVisible(%this);
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
    getWord(%dragCtrl.getScreenPosition(), 1).reposition(%dragHiliteCtrl, getWord(%dragCtrl.getScreenPosition(), 0));
    getWord(%dragCtrl.getExtent(), 1).resize(%dragHiliteCtrl, getWord(%dragCtrl.getExtent(), 0));
    %dragHiliteCtrl.clear();
    %dragCtrl.makeVisualClone().add(%dragHiliteCtrl);
    %dragHiliteCtrl.add(%this.getContent());
    %dragHiliteCtrl.pushToBack(%this.getContent());
    1.setVisible(%dragHiliteCtrl);
};
function Canvas::centerDragHiliteAroundCursor(%this) {
    if (!(isObject(%this.dragCtrl))) {
        return;
    }
    %width = getWord(%this.dragCtrl.getExtent(), 0);
    %height = getWord(%this.dragCtrl.getExtent(), 1);
    %startX = (getWord(%this.dragCtrl.getScreenPosition(), 0) + (%width / 2.0));
    %startY = (getWord(%this.dragCtrl.getScreenPosition(), 1) + (%height / 2.0));
    %this.startingDragPos = %startX @ " " @ %startY;
    %dragHiliteCtrl = %this.getDragHiliteCtrl();
    %mousePos = %this.getCursorPos();
    (getWord(%mousePos, 1) - (%height / 2.0)).reposition(%dragHiliteCtrl, (getWord(%mousePos, 0) - (%width / 2.0)));
};
function Canvas::onDragAndDropMove(%this, %dragCtrl, %mousePos) {
    %dragHiliteCtrl = %this.getDragHiliteCtrl();
    %xPos = ((getWord(%dragCtrl.getScreenPosition(), 0) + getWord(%mousePos, 0)) - getWord(%this.startingDragPos, 0));
    %ypos = ((getWord(%dragCtrl.getScreenPosition(), 1) + getWord(%mousePos, 1)) - getWord(%this.startingDragPos, 1));
    %ypos.reposition(%dragHiliteCtrl, %xPos);
};
function Canvas::onDragAndDropEnd(%this, %dragCtrl, %dropAccepted) {
    %dragHiliteCtrl = %this.getDragHiliteCtrl();
    if (%dropAccepted) {
        0.setVisible(%dragHiliteCtrl);
    }
    %xPos = getWord(%dragCtrl.getScreenPosition(), 0);
    %ypos = getWord(%dragCtrl.getScreenPosition(), 1);
    %ypos.setTrgPosition(%dragHiliteCtrl, %xPos);
    if (isObject(Canvas.getFirstResponder())) {
        1.makeFirstResponder(Canvas.getFirstResponder());
    }
};
function onDragAndDropCtrl(%make) {
    %dragCtrl = Canvas.getDragControl();
    if (isObject(%dragCtrl)) {
    }
    if ("dragAndDropCtrl".hasMethod(%dragCtrl)) {
        %make.dragAndDropCtrl(%dragCtrl);
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
    if (($Conv::TypingIndicatorState == -(1.0))) {
        %n = -(1.0);
        %n = (%n + 1.0);
        %n["_   " @ $Conv::TypingIndicators] = ;
        %n = (%n + 1.0);
        %n[" _  " @ $Conv::TypingIndicators] = ;
        %n = (%n + 1.0);
        %n["  _ " @ $Conv::TypingIndicators] = ;
        %n = (%n + 1.0);
        %n["   _" @ $Conv::TypingIndicators] = ;
        $Conv::TypingIndicatorsNum = (%n + 1.0);
        $Conv::TypingIndicatorState = ($Conv::TypingIndicatorsNum - 1.0);
    }
    $Conv::TypingIndicatorState = (($Conv::TypingIndicatorState + 1.0) % $Conv::TypingIndicatorsNum);
    $Conv::typingIndicator = $Conv::TypingIndicatorState[$Conv::TypingIndicators @ $Conv::TypingIndicatorState];
    %celebIndicatorOn = 0;
    if (%celebIndicatorOn) {
        if (($Conv::CelebOpenIndicatorState == -(1.0))) {
            %n = -(1.0);
            %n = (%n + 1.0);
            %n["" @ $Conv::CelebOpenIndicators] = ;
            %n = (%n + 1.0);
            %n["* " @ $Conv::CelebOpenIndicators] = ;
            %n = (%n + 1.0);
            %n["*  " @ $Conv::CelebOpenIndicators] = ;
            %n = (%n + 1.0);
            %n["*   " @ $Conv::CelebOpenIndicators] = ;
            %n = (%n + 1.0);
            %n["*    " @ $Conv::CelebOpenIndicators] = ;
            $Conv::CelebOpenIndicatorsNum = (%n + 1.0);
            $Conv::CelebOpenIndicatorState = ($Conv::CelebOpenIndicatorsNum - 1.0);
        }
        $Conv::CelebOpenIndicatorState = (($Conv::CelebOpenIndicatorState + 1.0) % $Conv::CelebOpenIndicatorsNum);
        $Conv::celebOpenIndicator = $Conv::CelebOpenIndicatorState[$Conv::CelebOpenIndicators @ $Conv::CelebOpenIndicatorState];
        if (($Conv::CelebCloseIndicatorState == -(1.0))) {
            %n = -(1.0);
            %n = (%n + 1.0);
            %n["" @ $Conv::CelebCloseIndicators] = ;
            %n = (%n + 1.0);
            %n[" *" @ $Conv::CelebCloseIndicators] = ;
            %n = (%n + 1.0);
            %n["  *" @ $Conv::CelebCloseIndicators] = ;
            %n = (%n + 1.0);
            %n["   *" @ $Conv::CelebCloseIndicators] = ;
            %n = (%n + 1.0);
            %n["    *" @ $Conv::CelebCloseIndicators] = ;
            $Conv::CelebCloseIndicatorsNum = (%n + 1.0);
            $Conv::CelebCloseIndicatorState = ($Conv::CelebCloseIndicatorsNum - 1.0);
        }
        $Conv::CelebCloseIndicatorState = (($Conv::CelebCloseIndicatorState + 1.0) % $Conv::CelebCloseIndicatorsNum);
        $Conv::celebCloseIndicator = $Conv::CelebCloseIndicatorState[$Conv::CelebCloseIndicators @ $Conv::CelebCloseIndicatorState];
    }
    if (($Conv::AffinityOpenIndicatorState == -(1.0))) {
        %n = -(1.0);
        %n = (%n + 1.0);
        %n["" @ $Conv::AffinityOpenIndicators] = ;
        %n = (%n + 1.0);
        %n["(" @ $Conv::AffinityOpenIndicators] = ;
        %n = (%n + 1.0);
        %n["(:" @ $Conv::AffinityOpenIndicators] = ;
        %n = (%n + 1.0);
        %n["(: " @ $Conv::AffinityOpenIndicators] = ;
        %n = (%n + 1.0);
        %n["(:  " @ $Conv::AffinityOpenIndicators] = ;
        %n = (%n + 1.0);
        %n["(:   " @ $Conv::AffinityOpenIndicators] = ;
        %n = (%n + 1.0);
        %n[":    " @ $Conv::AffinityOpenIndicators] = ;
        $Conv::AffinityOpenIndicatorsNum = (%n + 1.0);
        $Conv::AffinityOpenIndicatorState = ($Conv::AffinityOpenIndicatorsNum - 1.0);
    }
    $Conv::AffinityOpenIndicatorState = (($Conv::AffinityOpenIndicatorState + 1.0) % $Conv::AffinityOpenIndicatorsNum);
    $Conv::affinityOpenIndicator = $Conv::AffinityOpenIndicatorState[$Conv::AffinityOpenIndicators @ $Conv::AffinityOpenIndicatorState];
    if (($Conv::AffinityCloseIndicatorState == -(1.0))) {
        %n = -(1.0);
        %n = (%n + 1.0);
        %n["" @ $Conv::AffinityCloseIndicators] = ;
        %n = (%n + 1.0);
        %n[")" @ $Conv::AffinityCloseIndicators] = ;
        %n = (%n + 1.0);
        %n[":)" @ $Conv::AffinityCloseIndicators] = ;
        %n = (%n + 1.0);
        %n[" :)" @ $Conv::AffinityCloseIndicators] = ;
        %n = (%n + 1.0);
        %n["  :)" @ $Conv::AffinityCloseIndicators] = ;
        %n = (%n + 1.0);
        %n["   :)" @ $Conv::AffinityCloseIndicators] = ;
        %n = (%n + 1.0);
        %n["    :" @ $Conv::AffinityCloseIndicators] = ;
        $Conv::AffinityCloseIndicatorsNum = (%n + 1.0);
        $Conv::AffinityCloseIndicatorState = ($Conv::AffinityCloseIndicatorsNum - 1.0);
    }
    $Conv::AffinityCloseIndicatorState = (($Conv::AffinityCloseIndicatorState + 1.0) % $Conv::AffinityCloseIndicatorsNum);
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
    %posY = (%posY - getWord(%this.getExtent(), 1));
    %posY.reposition(%this, %posX);
};
function GuiControl::alignToTop(%this) {
    %posX = getWord(%this.getPosition(), 0);
    %posY = 0;
    %posY.reposition(%this, %posX);
};
function GuiControl::alignToLeft(%this) {
    %posX = 0;
    %posY = getWord(%this.getPosition(), 1);
    %posY.reposition(%this, %posX);
};
function GuiControl::alignToRight(%this) {
    %posX = getWord(%this.getParent().getExtent(), 0);
    %posX = (%posX - getWord(%this.getExtent(), 0));
    %posY = getWord(%this.getPosition(), 1);
    %posY.reposition(%this, %posX);
};
function GuiControl::alignToCenterX(%this) {
    %posX = getWord(%this.getParent().getExtent(), 0);
    %posX = (%posX - getWord(%this.getExtent(), 0));
    %posX = (%posX / 2.0);
    %posY = getWord(%this.getPosition(), 1);
    %posY.reposition(%this, %posX);
};
function GuiControl::alignToCenterY(%this) {
    %posX = getWord(%this.getPosition(), 0);
    %posY = getWord(%this.getParent().getExtent(), 1);
    %posY = (%posY - getWord(%this.getExtent(), 1));
    %posY = (%posY / 2.0);
    %posY.reposition(%this, %posX);
};
function GuiControl::alignToCenterXY(%this) {
    %posX = getWord(%this.getParent().getExtent(), 0);
    %posX = (%posX - getWord(%this.getExtent(), 0));
    %posX = (%posX / 2.0);
    %posY = getWord(%this.getParent().getExtent(), 1);
    %posY = (%posY - getWord(%this.getExtent(), 1));
    %posY = (%posY / 2.0);
    %posY.reposition(%this, %posX);
};
function GuiControl::fitInParentAsBitmap(%this) {
    if ((%this.fitInParentAlign $= "")) {
        return;
    }
    %this.fitSize();
    %this.fitInParentAlign.fitInParent(%this);
};
function GuiControl::globalToLocal(%this, %point) {
    %upperLeft = %this.getScreenPosition();
    %x = (getWord(%point, 0) - getWord(%upperLeft, 0));
    %y = (getWord(%point, 1) - getWord(%upperLeft, 1));
    return %x @ " " @ %y;
};
function GuiControl::dumpTreeVerbose(%this) {
    ""._dumpTreeVerboseRecursive(%this);
};
function GuiControl::_dumpTreeVerboseRecursive(%this, %indent) {
    echo(%indent @ getDebugString(%this));
    echo(%indent @ %this.getPosition() @ " " @ %this.getExtent());
    %num = %this.getCount();
    %n = 0;
    while ((%n < %num)) {
        %child = %n.getObject(%this);
        %indent @ "  "._dumpTreeVerboseRecursive(%child);
        %n = (%n + 1.0);
    }
};
function GuiControl::reparent(%this, %newParent, %newPosition, %newExtent, %newProfile) {
    %this.add(%newParent);
    if ((%newExtent $= "")) {
        %newExtent = %this.getExtent();
    }
    %newExtent.reshape(%this, %newPosition);
    if (!(%newProfile $= "")) {
        %newProfile.setProfile(%this);
    }
};
function GuiControl::reparentSameSize(%this, %newParent, %newProfile) {
    %pos = "0 0";
    %ext = %newParent.getExtent();
    %newProfile.reparent(%this, %newParent, %pos, %ext);
};
function GuiControl::FlashVisibility(%this, %numTimes, %periodMS) {
    %this.flashTicksRemaining = (%numTimes * 2.0);
    %this.flashTickPeriod = %periodMS;
    %this.flashVisibilityTick();
};
function GuiControl::flashVisibilityTick(%this) {
    cancel(%this.flashTickTimerID);
    %this.flashTickTimerID = "";
    if ((%this.flashTicksRemaining $= "")) {
    }
    if ((%this.flashTicksRemaining == 0.0)) {
        %this.flashTicksRemaining = "";
        %this.flashTickPeriod = "";
        1.setVisible(%this);
    }
    %this.flashTicksRemaining = (%this.flashTicksRemaining - 1.0);
    !(%this.isVisible()).setVisible(%this);
    %this.flashTickTimerID = "flashVisibilityTick".schedule(%this, %this.flashTickPeriod);
};
function generic_takeSnapshotReally(%previewBitmapCtrl) {
    %regionCtrl = %previewBitmapCtrl.snap_regionCtrl;
    %filenameBase = %previewBitmapCtrl.snap_fnBase;
    %filenameExt = %previewBitmapCtrl.snap_fnExt;
    %tookPhoto = snapshotTool::snapControl(%regionCtrl, %filenameBase @ %filenameExt);
    "".setBitmap(%previewBitmapCtrl);
    if (!(%tookPhoto)) {
        MessageBoxOK("Can't take snapshot!", "Unable to create snapshot. Please post a bug report in the forums. Thank you!", "");
    }
    %topMargin = 60;
    %bottomMargin = -(10.0);
    %leftMargin = 0;
    %rightMargin = 0;
    %playerIDs = ((getWord(%regionCtrl.getExtent(), 1) + %topMargin) + %bottomMargin).getPlayerIDsInViewAndInRangeAndInFrame(TheShapeNameHud, (getWord(%regionCtrl.getScreenPosition(), 0) - %leftMargin), (getWord(%regionCtrl.getScreenPosition(), 1) - %topMargin), ((getWord(%regionCtrl.getExtent(), 0) + %leftMargin) + %rightMargin));
    %numPlayers = getWordCount(%playerIDs);
    %playerNames = "";
    %n = 0;
    while ((%n < %numPlayers)) {
        %playerNames = %playerNames @ "\t" @ getWord(%playerIDs, %n).getShapeName();
        %n = (%n + 1.0);
    }
    %playerNames = trim(%playerNames);
    (%n < %numPlayers);
    %previewBitmapCtrl.playersInViewNames = %playerNames;
    removeFile(%filenameBase @ %filenameExt);
    addFile(%filenameBase @ %filenameExt);
    %filenameBase.setBitmap(%previewBitmapCtrl);
    alxPlay(AudioProfile_Shutter);
    commandToServer('FireEventPlayerTakesAPicture');
    %tookPhoto.onSnapshotDone(%previewBitmapCtrl);
    return %tookPhoto;
};
function hideABunchOfControls(%list) {
    %n = (getWordCount(%list) - 1.0);
    while ((%n >= 0.0)) {
        %ctrl = getWord(%list, %n);
        if (!(isObject(%ctrl))) {
            error(getScopeName() @ " " @ "- invalid control:" @ " " @ %ctrl @ " " @ getTrace());
        }
        %ctrl.hiding_originalVisibility = %ctrl.isVisible();
        0.setVisible(%ctrl);
        %n = (%n - 1.0);
    }
};
function restoreABunchOfControls(%list) {
    %list = trim(%list);
    %n = (getWordCount(%list) - 1.0);
    while ((%n >= 0.0)) {
        %ctrl = getWord(%list, %n);
        if (!(isObject(%ctrl))) {
            error(getScopeName() @ " " @ "- invalid control:" @ " " @ %ctrl @ " " @ getTrace());
        }
        %ctrl.hiding_originalVisibility.setVisible(%ctrl);
        %ctrl.hiding_originalVisibility = "";
        %n = (%n - 1.0);
    }
};
