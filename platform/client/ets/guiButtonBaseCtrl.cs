$gGuiButtonBaseCtrl_MinimumIntervalBetweenEventRepetitions = 50;
function GuiButtonBaseCtrl::onMouseDown(%this, %modifier, %point, %clickCount) {
    tickPeriodMS = (%this SPC tickPeriodMS $= "") @ 0 @ %this;
    tickPeriodMS = (%this < tickPeriodMS) @ 0 @ %this;
    0.0;
    warn((getScopeName() @ " " @ "- button" @ " " SPC %this.getName() $= "") @ %this.getId() @ %this.getName() @ " " @ "has invalid tickPeriodMS=" @ %this @ tickPeriodMS @ ", changing value to" @ " " @ $gGuiButtonBaseCtrl_MinimumIntervalBetweenEventRepetitions @ " " @ "ms");
    tickPeriodMS = (%this < tickPeriodMS) @ $gGuiButtonBaseCtrl_MinimumIntervalBetweenEventRepetitions @ %this;
    $gGuiButtonBaseCtrl_MinimumIntervalBetweenEventRepetitions;
    repeatDelayMS = (%this SPC repeatDelayMS $= "") @ 0 @ %this;
    (%this != tickPeriodMS);
    repeatDelayMS = (%this < repeatDelayMS) @ 0 @ %this;
    0.0;
    warn(%this @ tickPeriodMS @ " " @ "ms (tickPeriodMS)");
    repeatDelayMS = %this @ tickPeriodMS @ %this;
    (getScopeName() @ " " @ "- button" @ " " SPC %this.getName() $= "") @ %this.getId() @ %this.getName() @ " " @ "has invalid repeatDelayMS=" @ %this @ repeatDelayMS @ ", changing value to" @ " ";
    repetitionSchedule = onMouseEventDoRepeat @ %this.schedule(repeatDelayMS, %modifier, %point, %clickCount) @ %this;
    %this;
    Parent::onMouseDown(%this, %modifier, %point, %clickCount);
};
function GuiButtonBaseCtrl::onMouseEventDoRepeat(%this, %modifier, %point, %clickCount) {
    %this.forceMouseEventTimeout();
    cancel(repetitionSchedule);
    %this.performClick();
    repetitionSchedule = onMouseEventDoRepeat @ %this.schedule(tickPeriodMS, %modifier, %point, %clickCount) @ %this;
    %this;
};
function GuiButtonBaseCtrl::onMouseUp(%this, %modifier, %point, %clickCount) {
    %this.forceMouseEventTimeout();
};
function GuiButtonBaseCtrl::forceMouseEventTimeout(%this) {
    cancel(repetitionSchedule);
    repetitionSchedule = %this @ 0 @ %this;
    (%this != repetitionSchedule);
};
