$gTickerRepetitionCount = 2;
$gTickerRepetitionDelayMS = 1000 * 50;
function handleTickerMessage(%unused, %msgString)
{
    %senderName = getField(%msgString, 0);
    %body = getField(%msgString, 1);
    %priority = getField(%msgString, 2);
    %repetitions = getField(%msgString, 3);
    %repetitions = $gTickerRepetitionCount;
    if (UserListIgnores.hasKey(%senderName))
    {
        %priority = 2;
        %markedName = "<spush>" @ getPlayerMarkup(%senderName, "eeffff", 1) @ "<spop>";
        %body = "(you are ignoring" SPC %markedName SPC ")";
        %repetitions = 0;
        %msgString = %senderName TAB %body TAB %priority TAB %repetitions;
    }
    %queue = ticker_getQueue(%priority);
    ticker_enqueue(%queue, %msgString);
    %n = 1;
    while (%n < %repetitions)
    {
        schedule(%n * $gTickerRepetitionDelayMS, 0, "ticker_enqueue", %queue, %msgString);
        %n = %n + 1;
    }
}

function ticker_enqueue(%queue, %msgString)
{
    %queue.push_back(%msgString, "");
    ticker_tick();
    return ;
}
function ticker_getQueue(%priority)
{
    if ((%priority < 0) && (%priority > 3))
    {
        error(getScopeName() SPC "- invalid priority. setting to 0." SPC %msgString);
        %priority = 0;
    }
    %obj = safeEnsureScriptObject(Array, "gTickerQueue" @ %priority, 0);
    return %obj;
}
$gTicker_TimerID = "";
$gTicker_TimerPeriodMS_Paused = 100;
$gTicker_TimerPeriodMS_Regular = 30;
$gTicker_TimerPeriodMS = $gTicker_TimerPeriodMS_Regular;
$gTicker_TimerPixelsPerSecond = 65;
function ticker_tick()
{
    cancel($gTicker_TimerID);
    $gTicker_TimerID = "";
    if (isObject(geTicker_TextContainer) && geTicker_TextContainer.isVisible())
    {
        ticker_doScroll();
        $gTicker_TimerID = schedule($gTicker_TimerPeriodMS, 0, "ticker_tick");
    }
    else
    {
        ticker_newMessage();
    }
    return ;
}
function ticker_doScroll()
{
    ticker_createUI();
    if (geTicker_TextContainer.cursorInControl())
    {
        $gTicker_TimerPeriodMS = $gTicker_TimerPeriodMS_Paused;
        return ;
    }
    else
    {
        if (!isForegroundWindow())
        {
            $gTicker_TimerPeriodMS = $gTicker_TimerPeriodMS_Paused;
        }
        else
        {
            $gTicker_TimerPeriodMS = $gTicker_TimerPeriodMS_Regular;
        }
    }
    %curX = getWord(geTicker_Text.getPosition(), 0);
    %curY = getWord(geTicker_Text.getPosition(), 1);
    %pixelsToScroll = ($gTicker_TimerPixelsPerSecond * $gTicker_TimerPeriodMS) / 1000;
    %newX = %curX - %pixelsToScroll;
    %newY = %curY;
    if ((%newX + getWord(geTicker_Text.getExtent(), 0)) < 0)
    {
        geTicker_TextContainer.setVisible(0);
    }
    else
    {
        geTicker_Text.reposition(%newX, %newY);
        %curX = getWord(geTicker_Text.getPosition(), 0);
    }
    return ;
}
function ticker_newMessage()
{
    %msg = "";
    %n = 2;
    while (%msg $= "")
    {
        %queue = ticker_getQueue(%n);
        if (%queue.count() > 0)
        {
            %msg = %queue.getKey(0);
            %queue.pop_front();
        }
        %n = %n - 1;
    }
    if (!(%msg $= ""))
    {
        %senderName = getField(%msg, 0);
        %body = getField(%msg, 1);
        %priority = getField(%msg, 2);
        %repetitions = getField(%msg, 3);
        %body = TryFixBadWords(%body);
        %markedName = "<spush><b>" @ getPlayerMarkup(%senderName, "eeffff", 1) @ "<spop>";
        %unmarkedText = %senderName SPC "-" SPC %body SPC "-" SPC %senderName;
        %markedText = %markedName SPC "-" SPC %body SPC "-" SPC %markedName;
        ticker_createUI();
        geTicker_TextContainer.setVisible(1);
        geTicker_Text.reposition(getWord(geTicker_TextContainer.getExtent(), 0), 0);
        geTicker_Text.resize(getStrWidth(%unmarkedText) + 26, 14);
        geTicker_Text.setTextWithStyle(%markedText);
        ticker_tick();
    }
    else
    {
        geTicker.delete();
    }
    return %n;
}
function ticker_createUI()
{
    if (!isObject(ButtonBar))
    {
        error(getScopeName() SPC "- no ButtonBar yet." SPC getTrace());
        return ;
    }
    if (isObject(geTicker))
    {
        return ;
    }
    new GuiBitmapCtrl(geTicker)
    {
        extent = "20 29";
        bitmap = "platform/client/ui/ticker_background";
    }.add(new GuiBitmapCtrl(geTicker)
    {
        extent = "20 29";
        bitmap = "platform/client/ui/ticker_background";
    });
    $ButtonBarVar::buttonBarPaddingBottom = getWord(geTicker.getExtent(), 1);
    $ButtonBarVar::buttonBarPaddingBottom = $ButtonBarVar::buttonBarPaddingBottom - 4;
    ButtonBar.update();
    return ;
}
function geTicker::update(%this)
{
    %clientRectPosition = WindowManager.getClientRectPosition();
    %clientRectExtent = WindowManager.getClientRectExtent();
    %parentW = getWord(%this.getGroup().getExtent(), 0);
    %rightMargin = getWord(%clientRectPosition, 0) + getWord(%clientRectExtent, 0);
    %w = %parentW - ((%parentW - %rightMargin) * 2);
    %w = mMax(%w, 300);
    %this.resize(%w, 29);
    %this.alignToCenterX();
    %this.alignToBottom();
    return ;
}
function geTicker::onDelete(%this)
{
    $ButtonBarVar::buttonBarPaddingBottom = 0;
    ButtonBar.update();
    return ;
}
function geTicker_Text::onURL(%this, %url)
{
    if (!(firstWord(%url) $= "gamelink"))
    {
        Parent::onURL(%this, %url);
    }
    %name = unmunge(restWords(%url));
    onLeftClickPlayerName(%name, "");
    return ;
}
function geTicker_Text::onRightURL(%this, %url)
{
    if (!(firstWord(%url) $= "gamelink"))
    {
        Parent::onRightURL(%this, %url);
    }
    %name = unmunge(restWords(%url));
    onRightClickPlayerName(%name, "");
    return ;
}
