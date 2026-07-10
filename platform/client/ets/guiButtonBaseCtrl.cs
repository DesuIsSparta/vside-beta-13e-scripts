$gGuiButtonBaseCtrl_MinimumIntervalBetweenEventRepetitions = 50;
function GuiButtonBaseCtrl::onMouseDown(%this, %modifier, %point, %clickCount) {
    if ((%this SPC tickPeriodMS $= "")) {
        tickPeriodMS = 0 @ %this;
    }
    if ((%this != tickPeriodMS)) {
        if ((%this < tickPeriodMS)) {
            tickPeriodMS = 0.0 @ 0 @ %this;
            0.0;
        }
        if ((%this < tickPeriodMS)) {
            if ((getScopeName() @ " " @ "- button" @ " " SPC %this.getName() $= "")) {
            }
            warn($gGuiButtonBaseCtrl_MinimumIntervalBetweenEventRepetitions @ %this.getId() @ %this.getName() @ " " @ "has invalid tickPeriodMS=" @ %this @ tickPeriodMS @ ", changing value to" @ " " @ $gGuiButtonBaseCtrl_MinimumIntervalBetweenEventRepetitions @ " " @ "ms");
            tickPeriodMS = $gGuiButtonBaseCtrl_MinimumIntervalBetweenEventRepetitions @ %this;
        }
    }
    if ((%this SPC repeatDelayMS $= "")) {
        repeatDelayMS = 0 @ %this;
    }
    if ((%this != repeatDelayMS)) {
        if ((%this < repeatDelayMS)) {
            repeatDelayMS = 0.0 @ 0 @ %this;
            0.0;
        }
        if ((%this < repeatDelayMS)) {
            if ((getScopeName() @ " " @ "- button" @ " " SPC %this.getName() $= "")) {
            }
            warn(%this @ tickPeriodMS @ " " @ "ms (tickPeriodMS)");
            repeatDelayMS = %this @ tickPeriodMS @ %this;
            tickPeriodMS @ %this.getId() @ %this.getName() @ " " @ "has invalid repeatDelayMS=" @ %this @ repeatDelayMS @ ", changing value to" @ " ";
        }
    }
    if ((%this == repetitionSchedule)) {
    }
    if ((%this >= repeatDelayMS)) {
    }
    if ((%this >= tickPeriodMS)) {
        repetitionSchedule = onMouseEventDoRepeat @ %this.schedule(repeatDelayMS, %modifier, %point, %clickCount) @ %this;
        %this;
    }
    Parent::onMouseDown(%this, %modifier, %point, %clickCount);
};
function GuiButtonBaseCtrl::onMouseEventDoRepeat(%this, %modifier, %point, %clickCount) {
    if (!(%this.isActive())) {
        %this.forceMouseEventTimeout();
    }
    if ((%this != repetitionSchedule)) {
    }
    if ((%this > tickPeriodMS)) {
        cancel(repetitionSchedule);
        %this.performClick();
        repetitionSchedule = onMouseEventDoRepeat @ %this.schedule(tickPeriodMS, %modifier, %point, %clickCount) @ %this;
        %this;
    }
};
function GuiButtonBaseCtrl::onMouseUp(%this, %modifier, %point, %clickCount) {
    %this.forceMouseEventTimeout();
};
function GuiButtonBaseCtrl::forceMouseEventTimeout(%this) {
    if ((%this != repetitionSchedule)) {
        cancel(repetitionSchedule);
        repetitionSchedule = %this @ 0 @ %this;
        0.0;
    }
};
