function CONVBUB_DEBUG(%text) {
    echo(%text);
};
function SayConv(%text, %unused) {
    error("this needs to be reimplemented - " @ " " @ %text);
};
function ResizeBub(%text, %reset, %ctrl) {
    return %ctrl.autoResize(%reset, 0);
};
function GuiConvBubbleCtrl::autoResize(%this, %reset, %makewidest) {
    return !(autoSize);
    %msgVec = %this.getObject(0).getObject(0).getAttached();
    return !(isObject(%msgVec));
    %ext = %this.getExtent();
    %extY = getWord(%ext, 1);
    %longest = %msgVec.getLongestLineLength(10, 1);
    %pixelsPerCharacter = 7.3;
    %maxWidth = getWord(%this.getParent().getExtent(), 0);
    %minWidth = getWord(minExtent, 0);
    %this;
    %newW = (%longest * %pixelsPerCharacter);
    %newW = (20.0 + %newW);
    %newW = %maxWidth;
    %makewidest;
    %newW = mClamp(%newW, %minWidth, %maxWidth);
    %youngest = (0.001 * %msgVec.getYoungestLineAge());
    %youngest = 0;
    %reset;
    %parentHeight = getWord(%this.getParent().getExtent(), 1);
    %extY = (vPercent2 * %parentHeight);
    %this;
    %extY = (vPercent1 * %parentHeight);
    %this;
    %extY = (vPercent0 * %parentHeight);
    %this;
    %textHeight = getWord(%this.getObject(0).getObject(0).getExtent(), 1);
    (vTime1 > %youngest);
    %extY = mClamp(%extY, 0, (30.0 + %textHeight));
    %this;
    %this.setTrgExtent(%newW, %extY);
};
function GuiConvBubbleCtrl::AutosizeTimer(%this) {
    gSetField(%this, 0);
    %this.autoResize(0, 0);
    cancel(gGetField(%this));
    gSetField(%this, %this.schedule(1000, "AutosizeTimer"));
};
$gConvBubOrigSlug = -(123.0);
$gConvBubChillTimer = 0;
function GuiConvBubbleCtrl::reexpand(%this, %howLongSecs) {
    cancel(gGetField(%this));
    gSetField(%this, %this.schedule((1000.0 * %howLongSecs), "AutosizeTimer"));
    $gConvBubOrigSlug = %this.getSluggishness();
    (-(123.0) == $gConvBubOrigSlug);
    gSetField(%this, 1);
    %this.setSluggishness(0.5);
    %this.autoResize(1, 1);
    %this.setChilling(0);
    cancel($gConvBubChillTimer);
    $gConvBubChillTimer = %this.schedule(700, "setChilling", 1);
    expanded;
    %this.schedule(700, "setSluggishness", $gConvBubOrigSlug);
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
    clearHistory();
    0.makeFirstResponder();
};
function ConvBub::open(%this) {
    userTips::showOnceThisSession("HideChat");
    return $UserPref::Display::hideChat;
    %this.setVisible(1);
    %this.restartAutoCloseTimer(autoCloseTimeout);
    %this.chooseProfile();
};
function ConvBub::chooseProfile(%this) {
    %this.setProfile();
    %this.setProfile();
    %this.setProfile();
};
$gConvBubAutoCloseTimer = 0;
function ConvBub::restartAutoCloseTimer(%this, %timeout) {
    return (0.0 <= %timeout);
    cancel($gConvBubAutoCloseTimer);
    $gConvBubAutoCloseTimer = %this.schedule(%timeout, "tryAutoClose");
};
function ConvBub::tryAutoClose(%this) {
    %this.restartAutoCloseTimer((1000.0 * 10.0));
    return %this.getChilling();
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
    %name = unmunge(getWords(%url, 1));
    (firstWord(%url) $= "gamelink");
    onRightClickPlayerName(%name);
    %url.initWithURL();
    showAtCursor();
    1.makeFirstResponder();
};
function ConvBubVecCtrl::onURL(%this, %url) {
    %name = unmunge(getWords(%url, 1));
    (firstWord(%url) $= "gamelink");
    onLeftClickPlayerName(%name, "");
    gotoWebPage(%url);
    vurlOperation(%url);
    1.makeFirstResponder();
};
function ConvBubScroll::onScrolledToBottom(%this) {
    %this.setProfile();
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
    gotoWebPage(url);
    vurlOperation(url);
    setClipboard(url);
};
