function CONVBUB_DEBUG(%text)
{
    if ($ConversationDebug)
    {
        echo(%text);
    }
    return ;
}
function SayConv(%text, %unused)
{
    error("this needs to be reimplemented - " SPC %text);
    return ;
}
function ResizeBub(%text, %reset, %ctrl)
{
    return %ctrl.autoResize(%reset, 0);
}
function GuiConvBubbleCtrl::autoResize(%this, %reset, %makewidest)
{
    if (!%this.autoSize)
    {
        return ;
    }
    %msgVec = %this.getObject(0).getObject(0).getAttached();
    if (!isObject(%msgVec))
    {
        return ;
    }
    %ext = %this.getExtent();
    %extY = getWord(%ext, 1);
    %longest = %msgVec.getLongestLineLength(10, 1);
    %pixelsPerCharacter = 7.3;
    %maxWidth = getWord(%this.getParent().getExtent(), 0);
    %minWidth = getWord(%this.minExtent, 0);
    %newW = %pixelsPerCharacter * %longest;
    %newW = %newW + 20;
    if (%makewidest)
    {
        %newW = %maxWidth;
    }
    else
    {
        %newW = mClamp(%newW, %minWidth, %maxWidth);
    }
    %youngest = %msgVec.getYoungestLineAge() * 0.001;
    if (%reset)
    {
        %youngest = 0;
    }
    %parentHeight = getWord(%this.getParent().getExtent(), 1);
    if (%youngest > %this.vTime2)
    {
        %extY = %parentHeight * %this.vPercent2;
    }
    else
    {
        if (%youngest > %this.vTime1)
        {
            %extY = %parentHeight * %this.vPercent1;
        }
        else
        {
            %extY = %parentHeight * %this.vPercent0;
        }
    }
    %textHeight = getWord(%this.getObject(0).getObject(0).getExtent(), 1);
    %extY = mClamp(%extY, 0, %textHeight + 30);
    %this.setTrgExtent(%newW, %extY);
    return ;
}
function GuiConvBubbleCtrl::AutosizeTimer(%this)
{
    gSetField(%this, expanded, 0);
    if (ConvBubScroll.isAtBottom())
    {
        %this.autoResize(0, 0);
    }
    cancel(gGetField(%this, resizeTimer));
    gSetField(%this, resizeTimer, %this.schedule(1000, "AutosizeTimer"));
    return ;
}
$gConvBubOrigSlug = -123;
$gConvBubChillTimer = 0;
function GuiConvBubbleCtrl::reexpand(%this, %howLongSecs)
{
    cancel(gGetField(%this, resizeTimer));
    gSetField(%this, resizeTimer, %this.schedule(%howLongSecs * 1000, "AutosizeTimer"));
    if (!gGetField(%this, expanded))
    {
        if ($gConvBubOrigSlug == -123)
        {
            $gConvBubOrigSlug = %this.getSluggishness();
        }
        gSetField(%this, expanded, 1);
        %this.setSluggishness(0.5);
        %this.autoResize(1, 1);
        %this.setChilling(0);
        cancel($gConvBubChillTimer);
        $gConvBubChillTimer = %this.schedule(700, "setChilling", 1);
        %this.schedule(700, "setSluggishness", $gConvBubOrigSlug);
    }
    return ;
}
function ConvBub::onMouseLeaveBounds(%this)
{
    cancel($gConvBubChillTimer);
    $gConvBubChillTimer = 0;
    return ;
}
ConvBub.AutosizeTimer();
function ConvBub::updateAutoMargins(%this)
{
    %clientRectPosition = WindowManager.getClientRectPosition();
    %clientRectExtent = WindowManager.getClientRectExtent();
    gePlayGuiHudlessArea.setTrgExtent(%clientRectExtent);
    gePlayGuiHudlessArea.setTrgPosition(%clientRectPosition);
    return ;
}
function ConvBub::close(%this, %keepHistory)
{
    ConvBub.setVisible(0);
    if (!%keepHistory && isObject(pChat))
    {
        pChat.clearHistory();
    }
    ConvBubVecCtrl.makeFirstResponder(0);
    return ;
}
function ConvBub::open(%this)
{
    if ($UserPref::Display::hideChat)
    {
        userTips::showOnceThisSession("HideChat");
        return ;
    }
    %this.setVisible(1);
    %this.restartAutoCloseTimer(%this.autoCloseTimeout);
    %this.chooseProfile();
    return ;
}
function ConvBub::chooseProfile(%this)
{
    if (isObject(ApplauseMeterGui) && ApplauseMeterGui.downplayChatBubble())
    {
        %this.setProfile(ConvBubFadedProfile);
    }
    else
    {
        if ($player.hasRoleString("hween"))
        {
            %this.setProfile(ConvBubSpookyProfile);
        }
        else
        {
            %this.setProfile(ConvBubProfile);
        }
    }
    return ;
}
$gConvBubAutoCloseTimer = 0;
function ConvBub::restartAutoCloseTimer(%this, %timeout)
{
    if (%timeout <= 0)
    {
        return ;
    }
    cancel($gConvBubAutoCloseTimer);
    $gConvBubAutoCloseTimer = %this.schedule(%timeout, "tryAutoClose");
    return ;
}
function ConvBub::tryAutoClose(%this)
{
    if (%this.getChilling())
    {
        %this.restartAutoCloseTimer(10 * 1000);
        return ;
    }
    ConvBub.close(1);
    return ;
}
function ConvBub::onWake(%this)
{
    return ;
}
function ConvBub::onMouseDown(%this)
{
    ConvBub.reexpand(15);
    return ;
}
function ConvBubVecCtrl::onMouseDown(%this)
{
    ConvBub.onMouseDown();
    return ;
}
function ConvBubVecCtrl::onRightURL(%this, %url)
{
    if (firstWord(%url) $= "gamelink")
    {
        %name = unmunge(getWords(%url, 1));
        onRightClickPlayerName(%name);
    }
    else
    {
        LinkContextMenu.initWithURL(%url);
        LinkContextMenu.showAtCursor();
    }
    if (!%this.selectionActive)
    {
        TheShapeNameHud.makeFirstResponder(1);
    }
    return ;
}
function ConvBubVecCtrl::onURL(%this, %url)
{
    if (firstWord(%url) $= "gamelink")
    {
        %name = unmunge(getWords(%url, 1));
        onLeftClickPlayerName(%name, "");
    }
    else
    {
        if (getSubStr(%url, 0, 7) $= "http://")
        {
            gotoWebPage(%url);
        }
        else
        {
            if (getSubStr(%url, 0, 7) $= "vside:/")
            {
                vurlOperation(%url);
            }
        }
    }
    if (!%this.selectionActive)
    {
        TheShapeNameHud.makeFirstResponder(1);
    }
    return ;
}
function ConvBubScroll::onScrolledToBottom(%this)
{
    if (ConvBub.isEavesdrop)
    {
        %this.setProfile(ETSScrollDimProfile);
    }
    else
    {
        %this.setProfile(ETSScrollDarkProfile);
    }
    return ;
}
function ConvBubScroll::onMouseDown(%this)
{
    ConvBub.onMouseDown();
    return ;
}
function LinkContextMenu::initWithURL(%this, %url)
{
    %this.initWithURLAndTitle(%url, %url);
    return ;
}
function LinkContextMenu::initWithURLAndTitle(%this, %url, %title)
{
    %this.clear();
    %this.setText(%title);
    %this.url = %url;
    %this.add("Visit Link", %n = %n + 1, 0);
    %this.add("Copy Link", %n = %n + 1, 0);
    return ;
}
function LinkContextMenu::onSelect(%this, %unused, %text)
{
    if (%text $= "Visit Link")
    {
        if (getSubStr(%this.url, 0, 7) $= "http://")
        {
            gotoWebPage(%this.url);
        }
        if (getSubStr(%this.url, 0, 7) $= "vside:/")
        {
            vurlOperation(%this.url);
        }
    }
    else
    {
        if (%text $= "Copy Link")
        {
            setClipboard(%this.url);
        }
    }
    return ;
}
