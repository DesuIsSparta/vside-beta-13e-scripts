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
    if (!(%this.autoSize)) {
        return;
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
    %minWidth = getWord(%this.minExtent, 0);
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
    if ((%this.vTime2 > %youngest)) {
        %extY = (%this.vPercent2 * %parentHeight);
    }
    if ((%this.vTime1 > %youngest)) {
        %extY = (%this.vPercent1 * %parentHeight);
    }
    %extY = (%this.vPercent0 * %parentHeight);
    %textHeight = getWord(%this.getObject(0).getObject(0).getExtent(), 1);
    %extY = mClamp(%extY, 0, (30.0 + %textHeight));
    %this.setTrgExtent(%newW, %extY);
};
function GuiConvBubbleCtrl::AutosizeTimer(%this) {
    gSetField(%this, expanded, 0);
    if (ConvBubScroll.isAtBottom()) {
        %this.autoResize(0, 0);
    }
    cancel(resizeTimer, gGetField(%this));
    gSetField(%this, resizeTimer, %this.schedule(1000, "AutosizeTimer"));
};
$gConvBubOrigSlug = -(123.0);
$gConvBubChillTimer = 0;
function GuiConvBubbleCtrl::reexpand(%this, %howLongSecs) {
    cancel(resizeTimer, gGetField(%this));
    gSetField(%this, resizeTimer, %this.schedule((1000.0 * %howLongSecs), "AutosizeTimer"));
    if (!(gGetField(%this))) {
        if ((-(123.0) == $gConvBubOrigSlug)) {
            $gConvBubOrigSlug = %this.getSluggishness();
            expanded;
        }
        gSetField(%this, expanded, 1);
        %this.setSluggishness(0.5);
        %this.autoResize(1, 1);
        %this.setChilling(0);
        cancel($gConvBubChillTimer);
        $gConvBubChillTimer = %this.schedule(700, "setChilling", 1);
        %this.schedule(700, "setSluggishness", $gConvBubOrigSlug);
    }
};
function ConvBub::onMouseLeaveBounds(%this) {
    cancel($gConvBubChillTimer);
    $gConvBubChillTimer = 0;
};
ConvBub.AutosizeTimer();
function ConvBub::updateAutoMargins(%this) {
    %clientRectPosition = WindowManager.getClientRectPosition();
    %clientRectExtent = WindowManager.getClientRectExtent();
    gePlayGuiHudlessArea.setTrgExtent(%clientRectExtent);
    gePlayGuiHudlessArea.setTrgPosition(%clientRectPosition);
};
function ConvBub::close(%this, %keepHistory) {
    ConvBub.setVisible(0);
    if (!(%keepHistory)) {
    }
    if (isObject(pChat)) {
        pChat.clearHistory();
    }
    ConvBubVecCtrl.makeFirstResponder(0);
};
function ConvBub::open(%this) {
    if ($UserPref::Display::hideChat) {
        userTips::showOnceThisSession("HideChat");
        return;
    }
    %this.setVisible(1);
    %this.restartAutoCloseTimer(%this.autoCloseTimeout);
    %this.chooseProfile();
};
function ConvBub::chooseProfile(%this) {
    if (isObject(ApplauseMeterGui)) {
    }
    if (ApplauseMeterGui.downplayChatBubble()) {
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
    ConvBub.close(1);
};
function ConvBub::onWake(%this) {
};
function ConvBub::onMouseDown(%this) {
    ConvBub.reexpand(15);
};
function ConvBubVecCtrl::onMouseDown(%this) {
    ConvBub.onMouseDown();
};
function ConvBubVecCtrl::onRightURL(%this, %url) {
    if ((firstWord(%url) $= "gamelink")) {
        %name = unmunge(getWords(%url, 1));
        onRightClickPlayerName(%name);
    }
    LinkContextMenu.initWithURL(%url);
    LinkContextMenu.showAtCursor();
    if (!(%this.selectionActive)) {
        TheShapeNameHud.makeFirstResponder(1);
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
    if (!(%this.selectionActive)) {
        TheShapeNameHud.makeFirstResponder(1);
    }
};
function ConvBubScroll::onScrolledToBottom(%this) {
    if (%this.isEavesdrop) {
        %this.setProfile();
    }
    %this.setProfile();
};
function ConvBubScroll::onMouseDown(%this) {
    ConvBub.onMouseDown();
};
function LinkContextMenu::initWithURL(%this, %url) {
    %this.initWithURLAndTitle(%url, %url);
};
function LinkContextMenu::initWithURLAndTitle(%this, %url, %title) {
    %this.clear();
    %this.setText(%title);
    %this.url = %url;
    %n = (1.0 + %n);
    %this.add("Visit Link", , 0);
    %n = (1.0 + %n);
    %this.add("Copy Link", , 0);
};
function LinkContextMenu::onSelect(%this, %unused, %text) {
    if ((%text $= "Visit Link")) {
        if ((getSubStr(%this.url, 0, 7) $= "http://")) {
            gotoWebPage(%this.url);
        }
        if ((getSubStr(%this.url, 0, 7) $= "vside:/")) {
            vurlOperation(%this.url);
        }
    }
    if ((%text $= "Copy Link")) {
        setClipboard(%this.url);
    }
};
