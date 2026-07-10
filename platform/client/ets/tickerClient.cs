$gTickerRepetitionCount = 2;
$gTickerRepetitionDelayMS = (50.0 * 1000.0);
function handleTickerMessage(%unused, %msgString) {
    %senderName = getField(%msgString, 0);
    %body = getField(%msgString, 1);
    %priority = getField(%msgString, 2);
    %repetitions = getField(%msgString, 3);
    %repetitions = $gTickerRepetitionCount;
    if (%senderName.hasKey()) {
        %priority = 2;
        UserListIgnores;
        %markedName = "<spush>" @ getPlayerMarkup(%senderName, "eeffff", 1) @ "<spop>";
        %body = "(you are ignoring" @ " " @ %markedName @ " " @ ")";
        %repetitions = 0;
        %msgString = %senderName @ "\t" @ %body @ "\t" @ %priority @ "\t" @ %repetitions;
    }
    %queue = ticker_getQueue(%priority);
    ticker_enqueue(%queue, %msgString);
    %n = 1;
    if ((%repetitions < %n)) {
        schedule(($gTickerRepetitionDelayMS * %n), 0, "ticker_enqueue", %queue, %msgString);
        %n = (1.0 + %n);
    }
};
function ticker_enqueue(%queue, %msgString) {
    %queue.push_back(%msgString, "");
    ticker_tick();
};
function ticker_getQueue(%priority) {
    if ((0.0 < %priority)) {
    }
    if ((3.0 > %priority)) {
        error(getScopeName() @ " " @ "- invalid priority. setting to 0." @ " " @ %msgString);
        %priority = 0;
    }
    %obj = safeEnsureScriptObject("gTickerQueue" @ %priority, 0);
    Array;
    return %obj;
};
$gTicker_TimerID = "";
$gTicker_TimerPeriodMS_Paused = 100;
$gTicker_TimerPeriodMS_Regular = 30;
$gTicker_TimerPeriodMS = $gTicker_TimerPeriodMS_Regular;
$gTicker_TimerPixelsPerSecond = 65;
function ticker_tick() {
    cancel($gTicker_TimerID);
    $gTicker_TimerID = "";
    if (isObject(geTicker_TextContainer)) {
    }
    if (geTicker_TextContainer.isVisible()) {
        ticker_doScroll();
        $gTicker_TimerID = schedule($gTicker_TimerPeriodMS, 0, "ticker_tick");
    }
    ticker_newMessage();
};
function ticker_doScroll() {
    ticker_createUI();
    if (geTicker_TextContainer.cursorInControl()) {
        $gTicker_TimerPeriodMS = $gTicker_TimerPeriodMS_Paused;
        return;
    }
    if (!(isForegroundWindow())) {
        $gTicker_TimerPeriodMS = $gTicker_TimerPeriodMS_Paused;
    }
    $gTicker_TimerPeriodMS = $gTicker_TimerPeriodMS_Regular;
    %curX = getWord(geTicker_Text.getPosition(), 0);
    %curY = getWord(geTicker_Text.getPosition(), 1);
    %pixelsToScroll = (1000.0 / ($gTicker_TimerPeriodMS * $gTicker_TimerPixelsPerSecond));
    %newX = (%pixelsToScroll - %curX);
    %newY = %curY;
    if ((0.0 < (getWord(geTicker_Text.getExtent(), 0) + %newX))) {
        0.setVisible();
    }
    %newX.reposition(%newY);
    %curX = getWord(geTicker_Text.getPosition(), 0);
    geTicker_Text;
};
function ticker_newMessage() {
    %msg = "";
    %n = 2;
    if ((0.0 >= %n)) {
    }
    if ((%msg $= "")) {
        %queue = ticker_getQueue(%n);
        if ((0.0 > %queue.count())) {
            %msg = %queue.getKey(0);
            %queue.pop_front();
        }
        %n = (1.0 - %n);
        if ((0.0 >= %n)) {
        }
    }
    if (!((%msg $= "") @ " " @ %msg $= "")) {
        %senderName = getField(%msg, 0);
        %body = getField(%msg, 1);
        %priority = getField(%msg, 2);
        %repetitions = getField(%msg, 3);
        %body = TryFixBadWords(%body);
        %markedName = "<spush><b>" @ getPlayerMarkup(%senderName, "eeffff", 1) @ "<spop>";
        %unmarkedText = %senderName @ " " @ "-" @ " " @ %body @ " " @ "-" @ " " @ %senderName;
        %markedText = %markedName @ " " @ "-" @ " " @ %body @ " " @ "-" @ " " @ %markedName;
        ticker_createUI();
        1.setVisible();
        getWord(geTicker_TextContainer.getExtent(), 0).reposition(0);
        (26.0 + getStrWidth(%unmarkedText)).resize(14);
        %markedText.setTextWithStyle();
        ticker_tick();
    }
    geTicker.delete();
};
function ticker_createUI() {
    if (!(isObject(ButtonBar))) {
        error(getScopeName() @ " " @ "- no ButtonBar yet." @ " " @ getTrace());
        return;
    }
    if (isObject(geTicker)) {
        return;
    }
    new GuiBitmapCtrl(geTicker) {
        extent = PlayGui @ "20 29";
        bitmap = "platform/client/ui/ticker_background";
    };.add();
    $ButtonBarVar::buttonBarPaddingBottom = getWord(geTicker.getExtent(), 1);
    new GuiControl(geTicker_TextContainer) {
        horizSizing = "width";
        extent = 2 @ " " @ 16;
        position = 9 @ " " @ 6;
        visible = 0;
    };
    $ButtonBarVar::buttonBarPaddingBottom = (4.0 - $ButtonBarVar::buttonBarPaddingBottom);
    new GuiMLTextCtrl(geTicker_Text) {
        horizSizing = "right";
        extent = 14 @ " " @ 16;
        position = 0 @ " " @ 0;
        style = "ticker";
        stripGamelink = 0;
    };
    ButtonBar.update();
};
function geTicker::update(%this) {
    %clientRectPosition = WindowManager.getClientRectPosition();
    %clientRectExtent = WindowManager.getClientRectExtent();
    %parentW = getWord(%this.getGroup().getExtent(), 0);
    %rightMargin = (getWord(%clientRectExtent, 0) + getWord(%clientRectPosition, 0));
    %w = ((2.0 * (%rightMargin - %parentW)) - %parentW);
    %w = mMax(%w, 300);
    %this.resize(%w, 29);
    %this.alignToCenterX();
    %this.alignToBottom();
};
function geTicker::onDelete(%this) {
    $ButtonBarVar::buttonBarPaddingBottom = 0;
    ButtonBar.update();
};
function geTicker_Text::onURL(%this, %url) {
    if (!(firstWord(%url) $= "gamelink")) {
        Parent::onURL(%this, %url);
    }
    %name = unmunge(restWords(%url));
    onLeftClickPlayerName(%name, "");
};
function geTicker_Text::onRightURL(%this, %url) {
    if (!(firstWord(%url) $= "gamelink")) {
        Parent::onRightURL(%this, %url);
    }
    %name = unmunge(restWords(%url));
    onRightClickPlayerName(%name, "");
};
