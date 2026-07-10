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
    %cellHeight = (%this + getWord(childrenExtent, 1));
    spacing;
    %ypos = (getWord(%this.getPosition(), 1) - 1.0);
    %this;
    %closestRow = mFloor((0.5 + (%cellHeight / %ypos)));
    %targetRow = mFloor((numRowsOrCols / %cellIdx));
    %this;
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
    if (!(%this SPC origPoint $= "")) {
        %this.resize(getWord(origPoint, 0), getWord(origPoint, 1), getWord(origExtnt, 0), getWord(origExtnt, 1));
    }
    if ((%this SPC %mode $= "none")) {
    }
    %mode = %mode;
    "";
    blinkMode = %this @ %mode @ %this;
    %this;
    blinkPeriodOffMS = %this @ %periodOffMS @ %this;
    blinkPeriodOnMS = %periodOnMS @ %this;
    blinkParam = %param @ %this;
    blinksRemaining = 15 @ %this;
    origPoint = "" @ %this;
    %this.blinkStateSet(0);
    if (!(%mode $= "")) {
    }
    if ((50.0 > %periodOnMS)) {
        %this.blinkDo();
    }
};
function GuiControl::blinkSetRemaining(%this, %remaining) {
    blinksRemaining = %remaining @ %this;
};
function GuiControl::blinkDo(%this) {
    cancel(blinkTimer);
    blinkTimer = %this @ "" @ %this;
    %newState = (blinkState - 1.0);
    %this;
    if (%newState) {
    }
    %period = blinkPeriodOffMS;
    %this;
    if ((0.0 == %newState)) {
        blinksRemaining = (%this - blinksRemaining);
        1.0;
    }
    if ((50.0 >= %period)) {
    }
    if ((%this > blinksRemaining)) {
        %this.blinkStateSet(%newState);
        blinkTimer = 0.0 @ %this.schedule(%period, "blinkDo") @ %this;
        blinkPeriodOnMS;
    }
    %this.blinkStateSet(0);
    if ((0.0 > %period)) {
    }
    if ((50.0 < %period)) {
        error(getScopeName() @ " " @ "- period too small:" @ " " @ %period);
    }
};
function GuiControl::blinkStateSet(%this, %state) {
    %cmd = %this @ blinkMode;
    "GuiControl_blinkStateSet_";
    if (isFunction(%cmd)) {
        call(%cmd, %this, %state);
    }
    if (!(%this SPC blinkMode $= "")) {
        error(%this @ blinkMode @ " " @ getTrace());
    }
    blinkPeriodOnMS = getScopeName() @ " " @ "- Unknown mode:" @ " " @ 0 @ %this;
    blinkPeriodOffMS = 0 @ %this;
};
function GuiControl_blinkStateSet_Bounce(%this, %state) {
    if ((%this == blinkState)) {
        return %state;
    }
    blinkState = %state @ %this;
    if ((%this SPC origPoint $= "")) {
        origPoint = %this.getPosition() @ %this;
        origExtnt = %this.getExtent() @ %this;
    }
    if ((0.0 == %state)) {
        %newPoint = origPoint;
        %this;
        %newExtnt = origExtnt;
        %this;
    }
    if ((1.0 == %state)) {
        %newPoint = VectorAdd(origPoint, getWords(blinkParam, 0, 1));
        %this;
        %newExtnt = VectorAdd(origExtnt, getWords(blinkParam, 2, 3));
        %this;
    }
    %this.resize(getWord(%newPoint, 0), getWord(%newPoint, 1), getWord(%newExtnt, 0), getWord(%newExtnt, 1));
};
function GuiMLTextCtrl::applyBaseTextWithStyle(%this, %style) {
    %this.setValue(mlStyle(baseText, %style));
};
function GuiMLTextCtrl::applyBaseText(%this) {
    %this.applyBaseTextWithStyle(style);
};
function GuiMLTextCtrl::setTextWithStyle(%this, %text, %style) {
    if (!(isDefined("%style"))) {
    }
    if ((%style $= "")) {
        %style = style;
        %this;
    }
    %this.setText(mlStyle(%text, %style));
};
function GuiControl::onSetFirstResponder(%this) {
    %ctrl = %this;
    if (%this.hasFieldValue("hiliteProxy")) {
        if (isObject(hiliteProxy)) {
            %ctrl = hiliteProxy;
            %this;
        }
    }
    hiliteControl(%ctrl);
};
function GuiControl::onClearFirstResponder(%this) {
    if (!(isObject(getFirstResponder()))) {
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
    if (isObject()) {
    }
    if ((Canvas == getContent())) {
        return getId();
    }
    if (!(isDefined("%inParent"))) {
        %inParent = 0;
    }
    if (isObject(%ctrl)) {
    }
    if (canHilite) {
    }
    if (%ctrl.isActive()) {
        if (!(isObject())) {
            profile = HiliteWindow @ new GuiWindowCtrl(HiliteWindow) @ "HiliteFrameProfile";
            %ctrl;
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
        }
        if (%inParent) {
            %parent = %ctrl.getParent();
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
        1.setVisible();
        %targetPosX @ " " @ %targetPosY.setTrgPosition();
        %targetExtX @ " " @ %targetExtY.setTrgExtent();
        hiliteCtrl = HiliteWindow @ %ctrl @ HiliteWindow;
        HiliteWindow;
    }
    if (isObject()) {
        delete();
    }
};
function getHiliteCtrl() {
    if (isObject()) {
    }
    if (isVisible()) {
    }
    return "";
};
function GuiControl::isHiliteCtrl(%this) {
    if (isObject()) {
    }
    if (isVisible()) {
    }
    return (HiliteWindow == hiliteCtrl.getId());
};
$gToolTipDelay = 500;
function GuiControl::onMouseEnterBounds(%this) {
    if (!(%this SPC tooltip $= "")) {
        cancel(tooltiptimer);
        tooltiptimer = %this @ %this.schedule($gToolTipDelay, "showToolTip") @ %this;
    }
};
function GuiControl::showToolTip(%this, %toolTip) {
    if (!($UserPref::UI::ShowTooltips)) {
        return;
    }
    if (isObject()) {
        delete();
    }
    if (!(isDefined("%tooltip"))) {
        %toolTip = tooltip;
        %this;
    }
    %cursorPos = getCursorPos();
    Canvas;
    %posX = getWord(%cursorPos, 0);
    ToolTipCtrl;
    %posY = (22.0 + getWord(%cursorPos, 1));
    ToolTipCtrl;
    %extX = getStrWidth(%toolTip);
    %extY = 16;
    %coords = onscreenCoordinates(%posX, %posY, (8.0 + %extX), %extY);
    %posX = getWord(%coords, 0);
    %posY = getWord(%coords, 1);
    profile = new GuiControl(ToolTipCtrl) @ "ToolTipProfile";
    position = %posX @ " " @ %posY;
    extent = (8.0 + %extX) @ " " @ (0.0 + %extY);
    minExtent = "1 1";
    profile = new GuiTextCtrl(ToolTipTextCtrl) @ "ToolTipTextProfile";
    position = "4 -1";
    extent = %extX @ " " @ %extY;
    text = %toolTip;
    getContent().add();
};
function GuiControl::hideToolTip(%this) {
    if (isObject()) {
        0.setVisible();
    }
    cancel(tooltiptimer);
    tooltiptimer = %this @ 0 @ %this;
    ToolTipCtrl;
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
    cancel(tooltiptimer);
    tooltiptimer = %this @ 0 @ %this;
    if ((%toolTip $= "")) {
        %this.hideToolTip();
    }
    tooltiptimer = %this.schedule($gToolTipDelay, "showToolTip", %toolTip) @ %this;
};
function CanvasDragHiliteCtrl::onReachedTarget(%this) {
    %this.setVisible(0);
};
function Canvas::getDragHiliteCtrl(%this) {
    if (!(isObject(dragHiliteCtrl))) {
        profile = %this @ new GuiControl(CanvasDragHiliteCtrl) @ "ETSNonModalProfile";
        horizSizing = "width";
        vertSizing = "height";
        position = "0 0";
        extent = "1 1";
        minExtent = "1 1";
        sluggishness = 0.25;
        visible = 1;
        trgReachedCommand = "$ThisControl.onReachedTarget();";
        dragHiliteCtrl = %this;
    }
    return dragHiliteCtrl;
};
function Canvas::onDragAndDropStart(%this, %dragCtrl, %mousePos) {
    startingDragPos = %mousePos @ %this;
    dragCtrl = %dragCtrl @ %this;
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
    if (!(isObject(dragCtrl))) {
        return %this;
    }
    %width = getWord(dragCtrl.getExtent(), 0);
    %this;
    %height = getWord(dragCtrl.getExtent(), 1);
    %this;
    %startX = (%this + getWord(dragCtrl.getScreenPosition(), 0));
    (2.0 / %width);
    %startY = (%this + getWord(dragCtrl.getScreenPosition(), 1));
    (2.0 / %height);
    startingDragPos = %startX @ " " @ %startY @ %this;
    %dragHiliteCtrl = %this.getDragHiliteCtrl();
    %mousePos = %this.getCursorPos();
    %dragHiliteCtrl.reposition(((2.0 / %width) - getWord(%mousePos, 0)), ((2.0 / %height) - getWord(%mousePos, 1)));
};
function Canvas::onDragAndDropMove(%this, %dragCtrl, %mousePos) {
    %dragHiliteCtrl = %this.getDragHiliteCtrl();
    %xPos = (getWord(startingDragPos, 0) - (getWord(%mousePos, 0) + getWord(%dragCtrl.getScreenPosition(), 0)));
    %this;
    %ypos = (getWord(startingDragPos, 1) - (getWord(%mousePos, 1) + getWord(%dragCtrl.getScreenPosition(), 1)));
    %this;
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
    if (isObject(getFirstResponder())) {
        getFirstResponder().makeFirstResponder(1);
    }
};
function onDragAndDropCtrl(%make) {
    %dragCtrl = getDragControl();
    Canvas;
    if (isObject(%dragCtrl)) {
    }
    if (%dragCtrl.hasMethod("dragAndDropCtrl")) {
        %dragCtrl.dragAndDropCtrl(%make);
    }
};
function GuiControl::makeVisualClone(%this) {
    profile = GuiControl @ new ""() @ "DragAndDropProfile";
    0;
    horizSizing = "width";
    vertSizing = "height";
    position = "0 0";
    extent = %this.getExtent();
    minExtent = "1 1";
    sluggishness = -1;
    visible = 1;
    return;
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
    if ((%this SPC fitInParentAlign $= "")) {
        return;
    }
    %this.fitSize();
    %this.fitInParent(fitInParentAlign);
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
    flashTicksRemaining = (2.0 * %numTimes) @ %this;
    flashTickPeriod = %periodMS @ %this;
    %this.flashVisibilityTick();
};
function GuiControl::flashVisibilityTick(%this) {
    cancel(flashTickTimerID);
    flashTickTimerID = %this @ "" @ %this;
    if ((%this SPC flashTicksRemaining $= "")) {
    }
    if ((%this == flashTicksRemaining)) {
        flashTicksRemaining = 0.0 @ "" @ %this;
        flashTickPeriod = "" @ %this;
        %this.setVisible(1);
    }
    flashTicksRemaining = (%this - flashTicksRemaining);
    1.0;
    %this.setVisible(!(%this.isVisible()));
    flashTickTimerID = %this @ %this.schedule(flashTickPeriod, "flashVisibilityTick") @ %this;
};
function generic_takeSnapshotReally(%previewBitmapCtrl) {
    %regionCtrl = snap_regionCtrl;
    %previewBitmapCtrl;
    %filenameBase = snap_fnBase;
    %previewBitmapCtrl;
    %filenameExt = snap_fnExt;
    %previewBitmapCtrl;
    %tookPhoto = snapshotTool::snapControl(%regionCtrl, %filenameBase @ %filenameExt);
    %previewBitmapCtrl.setBitmap("");
    if (!(%tookPhoto)) {
        MessageBoxOK("Can't take snapshot!", "Unable to create snapshot. Please post a bug report in the forums. Thank you!", "");
    }
    %topMargin = 60;
    %bottomMargin = -(10.0);
    %leftMargin = 0;
    %rightMargin = 0;
    %playerIDs = (%leftMargin - getWord(%regionCtrl.getScreenPosition(), 0)).getPlayerIDsInViewAndInRangeAndInFrame((%topMargin - getWord(%regionCtrl.getScreenPosition(), 1)), (%rightMargin + (%leftMargin + getWord(%regionCtrl.getExtent(), 0))), (%bottomMargin + (%topMargin + getWord(%regionCtrl.getExtent(), 1))));
    TheShapeNameHud;
    %numPlayers = getWordCount(%playerIDs);
    %playerNames = "";
    %n = 0;
    if ((%numPlayers < %n)) {
        %playerNames = %playerNames @ "\t" @ getWord(%playerIDs, %n).getShapeName();
        %n = (1.0 + %n);
    }
    %playerNames = trim(%playerNames);
    (%numPlayers < %n);
    playersInViewNames = %playerNames @ %previewBitmapCtrl;
    removeFile(%filenameBase @ %filenameExt);
    addFile(%filenameBase @ %filenameExt);
    %previewBitmapCtrl.setBitmap(%filenameBase);
    alxPlay();
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
        hiding_originalVisibility = %ctrl.isVisible() @ %ctrl;
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
        %ctrl.setVisible(hiding_originalVisibility);
        hiding_originalVisibility = %ctrl @ "" @ %ctrl;
        %n = (1.0 - %n);
    }
};
