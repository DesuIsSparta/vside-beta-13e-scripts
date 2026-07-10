function CONVBUB_DEBUG(%text) {
    if ($ConversationDebug) {
        echo(%text);
    }
};
function SayConv(%text, %unused) {
    error("this needs to be reimplemented - " @ " " @ %text);
};
function ResizeBub(%text, %reset, %ctrl) {
    return 0.autoResize(%ctrl, %reset);
};
function GuiConvBubbleCtrl::autoResize(%this, %reset, %makewidest) {
    if (!(%this.autoSize)) {
        return;
    }
    %msgVec = 0.getObject(0.getObject(%this)).getAttached();
    if (!(isObject(%msgVec))) {
        return;
    }
    %ext = %this.getExtent();
    %extY = getWord(%ext, 1);
    %longest = 1.getLongestLineLength(%msgVec, 10);
    %pixelsPerCharacter = 7.3;
    %maxWidth = getWord(%this.getParent().getExtent(), 0);
    %minWidth = getWord(%this.minExtent, 0);
    %newW = (%pixelsPerCharacter * %longest);
    %newW = (%newW + 20.0);
    if (%makewidest) {
        %newW = %maxWidth;
    }
    %newW = mClamp(%newW, %minWidth, %maxWidth);
    %youngest = (%msgVec.getYoungestLineAge() * 0.001);
    if (%reset) {
        %youngest = 0;
    }
    %parentHeight = getWord(%this.getParent().getExtent(), 1);
    if ((%youngest > %this.vTime2)) {
        %extY = (%parentHeight * %this.vPercent2);
    }
    if ((%youngest > %this.vTime1)) {
        %extY = (%parentHeight * %this.vPercent1);
    }
    %extY = (%parentHeight * %this.vPercent0);
    %textHeight = getWord(0.getObject(0.getObject(%this)).getExtent(), 1);
    %extY = mClamp(%extY, 0, (%textHeight + 30.0));
    %extY.setTrgExtent(%this, %newW);
};
function GuiConvBubbleCtrl::AutosizeTimer(%this) {
    gSetField(%this, expanded, 0);
    if (ConvBubScroll.isAtBottom()) {
        0.autoResize(%this, 0);
    }
    cancel(resizeTimer, gGetField(%this));
    gSetField(%this, resizeTimer, "AutosizeTimer".schedule(%this, 1000));
};
$gConvBubOrigSlug = -(123.0);
$gConvBubChillTimer = 0;
function GuiConvBubbleCtrl::reexpand(%this, %howLongSecs) {
    cancel(resizeTimer, gGetField(%this));
    gSetField(%this, resizeTimer, "AutosizeTimer".schedule(%this, (%howLongSecs * 1000.0)));
    if (!(gGetField(%this))) {
        if (($gConvBubOrigSlug == -(123.0))) {
            $gConvBubOrigSlug = %this.getSluggishness();
            expanded;
        }
        gSetField(%this, expanded, 1);
        0.5.setSluggishness(%this);
        1.autoResize(%this, 1);
        0.setChilling(%this);
        cancel($gConvBubChillTimer);
        $gConvBubChillTimer = 1.schedule(%this, 700, "setChilling");
        $gConvBubOrigSlug.schedule(%this, 700, "setSluggishness");
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
    %clientRectExtent.setTrgExtent(gePlayGuiHudlessArea);
    %clientRectPosition.setTrgPosition(gePlayGuiHudlessArea);
};
function ConvBub::close(%this, %keepHistory) {
    0.setVisible(ConvBub);
    if (!(%keepHistory)) {
    }
    if (isObject(pChat)) {
        pChat.clearHistory();
    }
    0.makeFirstResponder(ConvBubVecCtrl);
};
function ConvBub::open(%this) {
    if ($UserPref::Display::hideChat) {
        userTips::showOnceThisSession("HideChat");
        return;
    }
    1.setVisible(%this);
    %this.autoCloseTimeout.restartAutoCloseTimer(%this);
    %this.chooseProfile();
};
function ConvBub::chooseProfile(%this) {
    if (isObject(ApplauseMeterGui)) {
    }
    if (ApplauseMeterGui.downplayChatBubble()) {
        %this.setProfile();
    }
    if ("hween".hasRoleString($player)) {
        %this.setProfile();
    }
    %this.setProfile();
};
$gConvBubAutoCloseTimer = 0;
function ConvBub::restartAutoCloseTimer(%this, %timeout) {
    if ((%timeout <= 0.0)) {
        return;
    }
    cancel($gConvBubAutoCloseTimer);
    $gConvBubAutoCloseTimer = "tryAutoClose".schedule(%this, %timeout);
};
function ConvBub::tryAutoClose(%this) {
    if (%this.getChilling()) {
        (10.0 * 1000.0).restartAutoCloseTimer(%this);
        return;
    }
    1.close(ConvBub);
};
function ConvBub::onWake(%this) {
};
function ConvBub::onMouseDown(%this) {
    15.reexpand(ConvBub);
};
function ConvBubVecCtrl::onMouseDown(%this) {
    ConvBub.onMouseDown();
};
function ConvBubVecCtrl::onRightURL(%this, %url) {
    if ((firstWord(%url) $= "gamelink")) {
        %name = unmunge(getWords(%url, 1));
        onRightClickPlayerName(%name);
    }
    %url.initWithURL(LinkContextMenu);
    LinkContextMenu.showAtCursor();
    if (!(%this.selectionActive)) {
        1.makeFirstResponder(TheShapeNameHud);
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
        1.makeFirstResponder(TheShapeNameHud);
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
    %url.initWithURLAndTitle(%this, %url);
};
function LinkContextMenu::initWithURLAndTitle(%this, %url, %title) {
    %this.clear();
    %title.setText(%this);
    %this.url = %url;
    %n = (%n + 1.0);
    0.add(%this, "Visit Link", );
    %n = (%n + 1.0);
    0.add(%this, "Copy Link", );
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
