$gGuiButtonBaseCtrl_MinimumIntervalBetweenEventRepetitions = 50;
function GuiButtonBaseCtrl::onMouseDown(%this, %modifier, %point, %clickCount) {
    if ((%this.tickPeriodMS $= "")) {
        %this.tickPeriodMS = 0;
    }
    if ((%this.tickPeriodMS != 0.0)) {
        if ((%this.tickPeriodMS < 0.0)) {
            %this.tickPeriodMS = 0;
        }
        if ((%this.tickPeriodMS < $gGuiButtonBaseCtrl_MinimumIntervalBetweenEventRepetitions)) {
            if ((getScopeName() @ " " @ "- button" @ " " @ " " @ %this.getName() $= "")) {
            }
            warn(%this.getId() @ %this.getName() @ " " @ "has invalid tickPeriodMS=" @ %this.tickPeriodMS @ ", changing value to" @ " " @ $gGuiButtonBaseCtrl_MinimumIntervalBetweenEventRepetitions @ " " @ "ms");
            %this.tickPeriodMS = $gGuiButtonBaseCtrl_MinimumIntervalBetweenEventRepetitions;
        }
    }
    if ((%this.repeatDelayMS $= "")) {
        %this.repeatDelayMS = 0;
    }
    if ((%this.repeatDelayMS != 0.0)) {
        if ((%this.repeatDelayMS < 0.0)) {
            %this.repeatDelayMS = 0;
        }
        if ((%this.repeatDelayMS < %this.tickPeriodMS)) {
            if ((getScopeName() @ " " @ "- button" @ " " @ " " @ %this.getName() $= "")) {
            }
            warn(%this.getId() @ %this.getName() @ " " @ "has invalid repeatDelayMS=" @ %this.repeatDelayMS @ ", changing value to" @ " " @ %this.tickPeriodMS @ " " @ "ms (tickPeriodMS)");
            %this.repeatDelayMS = %this.tickPeriodMS;
        }
    }
    if ((%this.repetitionSchedule == 0.0)) {
    }
    if ((%this.repeatDelayMS >= $gGuiButtonBaseCtrl_MinimumIntervalBetweenEventRepetitions)) {
    }
    if ((%this.tickPeriodMS >= $gGuiButtonBaseCtrl_MinimumIntervalBetweenEventRepetitions)) {
        %this.repetitionSchedule = %clickCount.schedule(%this, %this.repeatDelayMS, onMouseEventDoRepeat, %modifier, %point);
    }
    Parent::onMouseDown(%this, %modifier, %point, %clickCount);
};
function GuiButtonBaseCtrl::onMouseEventDoRepeat(%this, %modifier, %point, %clickCount) {
    if (!(%this.isActive())) {
        %this.forceMouseEventTimeout();
    }
    if ((%this.repetitionSchedule != 0.0)) {
    }
    if ((%this.tickPeriodMS > 0.0)) {
        cancel(%this.repetitionSchedule);
        %this.performClick();
        %this.repetitionSchedule = %clickCount.schedule(%this, %this.tickPeriodMS, onMouseEventDoRepeat, %modifier, %point);
    }
};
function GuiButtonBaseCtrl::onMouseUp(%this, %modifier, %point, %clickCount) {
    %this.forceMouseEventTimeout();
};
function GuiButtonBaseCtrl::forceMouseEventTimeout(%this) {
    if ((%this.repetitionSchedule != 0.0)) {
        cancel(%this.repetitionSchedule);
        %this.repetitionSchedule = 0;
    }
};
