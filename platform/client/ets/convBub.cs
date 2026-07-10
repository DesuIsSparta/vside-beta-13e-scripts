function CONVBUB_DEBUG(%text) {
    if ($ConversationDebug) {
        echo(%text);
    }
};
function SayConv(%text, %unused) {
    error("this needs to be reimplemented - " @ " " @ %text);
};
function ResizeBub(%text, %reset, %ctrl) {
    return %ctrl.autoResize(%reset, 0);
};
function GuiConvBubbleCtrl::autoResize(%this, %reset, %makewidest) {
    if (!(autoSize)) {
        return %this;
    }
    %msgVec = %this.getObject(0).getObject(0).getAttached();
    if (!(isObject(%msgVec))) {
        return;
    }
    %ext = %this.getExtent();
    %extY = getWord(%ext, 1);
    %longest = %msgVec.getLongestLineLength(10, 1);
    %pixelsPerCharacter = 7.3;
    %maxWidth = getWord(%this.getParent().getExtent(), 0);
    %minWidth = getWord(minExtent, 0);
    %this;
    %newW = (%longest * %pixelsPerCharacter);
    %newW = (20.0 + %newW);
    if (%makewidest) {
        %newW = %maxWidth;
    }
    %newW = mClamp(%newW, %minWidth, %maxWidth);
    %youngest = (0.001 * %msgVec.getYoungestLineAge());
    if (%reset) {
        %youngest = 0;
    }
    %parentHeight = getWord(%this.getParent().getExtent(), 1);
    if ((vTime2 > %youngest)) {
        %extY = (vPercent2 * %parentHeight);
        %this;
    }
    if ((vTime1 > %youngest)) {
        %extY = (vPercent1 * %parentHeight);
        %this;
    }
    %extY = (vPercent0 * %parentHeight);
    %this;
    %textHeight = getWord(%this.getObject(0).getObject(0).getExtent(), 1);
    %this;
    %extY = mClamp(%extY, 0, (30.0 + %textHeight));
    %this;
    %this.setTrgExtent(%newW, %extY);
};
function GuiConvBubbleCtrl::AutosizeTimer(%this) {
    gSetField(%this, 0);
    if (isAtBottom()) {
        %this.autoResize(0, 0);
    }
    cancel(gGetField(%this));
    gSetField(%this, %this.schedule(1000, "AutosizeTimer"));
};
$gConvBubOrigSlug = -(123.0);
$gConvBubChillTimer = 0;
function GuiConvBubbleCtrl::reexpand(%this, %howLongSecs) {
    cancel(gGetField(%this));
    gSetField(%this, %this.schedule((1000.0 * %howLongSecs), "AutosizeTimer"));
    if (!(gGetField(%this))) {
        if ((-(123.0) == $gConvBubOrigSlug)) {
            $gConvBubOrigSlug = %this.getSluggishness();
            expanded;
        }
        gSetField(%this, 1);
        %this.setSluggishness(0.5);
        %this.autoResize(1, 1);
        %this.setChilling(0);
        cancel($gConvBubChillTimer);
        $gConvBubChillTimer = %this.schedule(700, "setChilling", 1);
        expanded;
        %this.schedule(700, "setSluggishness", $gConvBubOrigSlug);
    }
};
function ConvBub::onMouseLeaveBounds(%this) {
    cancel($gConvBubChillTimer);
    $gConvBubChillTimer = 0;
};
AutosizeTimer();
function ConvBub::updateAutoMargins(%this) {
    %clientRectPosition = getClientRectPosition();
    WindowManager;
    %clientRectExtent = getClientRectExtent();
    WindowManager;
    %clientRectExtent.setTrgExtent();
    %clientRectPosition.setTrgPosition();
};
function ConvBub::close(%this, %keepHistory) {
    0.setVisible();
    if (!(%keepHistory)) {
    }
    if (isObject()) {
        clearHistory();
    }
    0.makeFirstResponder();
};
function ConvBub::open(%this) {
    if ($UserPref::Display::hideChat) {
        userTips::showOnceThisSession("HideChat");
        return;
    }
    %this.setVisible(1);
    %this.restartAutoCloseTimer(autoCloseTimeout);
    %this.chooseProfile();
};
function ConvBub::chooseProfile(%this) {
    if (isObject()) {
    }
    if (downplayChatBubble()) {
        %this.setProfile();
    }
    if ($player.hasRoleString("hween")) {
        %this.setProfile();
    }
    %this.setProfile();
};
$gConvBubAutoCloseTimer = 0;
function ConvBub::restartAutoCloseTimer(%this, %timeout) {
    if ((0.0 <= %timeout)) {
        return;
    }
    cancel($gConvBubAutoCloseTimer);
    $gConvBubAutoCloseTimer = %this.schedule(%timeout, "tryAutoClose");
};
function ConvBub::tryAutoClose(%this) {
    if (%this.getChilling()) {
        %this.restartAutoCloseTimer((1000.0 * 10.0));
        return;
    }
    1.close();
};
function ConvBub::onWake(%this) {
};
function ConvBub::onMouseDown(%this) {
    15.reexpand();
};
function ConvBubVecCtrl::onMouseDown(%this) {
    onMouseDown();
};
function ConvBubVecCtrl::onRightURL(%this, %url) {
    if ((firstWord(%url) $= "gamelink")) {
        %name = unmunge(getWords(%url, 1));
        onRightClickPlayerName(%name);
    }
    %url.initWithURL();
    showAtCursor();
    if (!(selectionActive)) {
        1.makeFirstResponder();
    }
};
function ConvBubVecCtrl::onURL(%this, %url) {
    if ((firstWord(%url) $= "gamelink")) {
        %name = unmunge(getWords(%url, 1));
        onLeftClickPlayerName(%name, "");
    }
    if ((getSubStr(%url, 0, 7) $= "http://")) {
        gotoWebPage(%url);
    }
    if ((getSubStr(%url, 0, 7) $= "vside:/")) {
        vurlOperation(%url);
    }
    if (!(selectionActive)) {
        1.makeFirstResponder();
    }
};
function ConvBubScroll::onScrolledToBottom(%this) {
    if (isEavesdrop) {
        %this.setProfile();
    }
    %this.setProfile();
};
function ConvBubScroll::onMouseDown(%this) {
    onMouseDown();
};
function LinkContextMenu::initWithURL(%this, %url) {
    %this.initWithURLAndTitle(%url, %url);
};
function LinkContextMenu::initWithURLAndTitle(%this, %url, %title) {
    %this.clear();
    %this.setText(%title);
    url = %url @ %this;
    %n = (1.0 + %n);
    %this.add("Visit Link", , 0);
    %n = (1.0 + %n);
    %this.add("Copy Link", , 0);
};
function LinkContextMenu::onSelect(%this, %unused, %text) {
    if ((%text $= "Visit Link")) {
        if ((%this SPC getSubStr(url, 0, 7) $= "http://")) {
            gotoWebPage(url);
        }
        if ((%this SPC getSubStr(url, 0, 7) $= "vside:/")) {
            vurlOperation(url);
        }
    }
    if ((%this SPC %text $= "Copy Link")) {
        setClipboard(url);
    }
};
