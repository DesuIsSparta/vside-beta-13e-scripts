$gGuiButtonBaseCtrl_MinimumIntervalBetweenEventRepetitions = 50;
function GuiButtonBaseCtrl::onMouseDown(%this, %modifier, %point, %clickCount) {
    if ((%this.tickPeriodMS $= "")) {
        %this.tickPeriodMS = 0;
    }
    if ((0.0 != %this.tickPeriodMS)) {
        if ((0.0 < %this.tickPeriodMS)) {
            %this.tickPeriodMS = 0;
        }
        if (($gGuiButtonBaseCtrl_MinimumIntervalBetweenEventRepetitions < %this.tickPeriodMS)) {
            if ((getScopeName() @ " " @ "- button" @ " " @ " " @ %this.getName() $= "")) {
            }
            warn(%this.getId() @ %this.getName() @ " " @ "has invalid tickPeriodMS=" @ %this.tickPeriodMS @ ", changing value to" @ " " @ $gGuiButtonBaseCtrl_MinimumIntervalBetweenEventRepetitions @ " " @ "ms");
            %this.tickPeriodMS = $gGuiButtonBaseCtrl_MinimumIntervalBetweenEventRepetitions;
        }
    }
    if ((%this.repeatDelayMS $= "")) {
        %this.repeatDelayMS = 0;
    }
    if ((0.0 != %this.repeatDelayMS)) {
        if ((0.0 < %this.repeatDelayMS)) {
            %this.repeatDelayMS = 0;
        }
        if ((%this.tickPeriodMS < %this.repeatDelayMS)) {
            if ((getScopeName() @ " " @ "- button" @ " " @ " " @ %this.getName() $= "")) {
            }
            warn(%this.getId() @ %this.getName() @ " " @ "has invalid repeatDelayMS=" @ %this.repeatDelayMS @ ", changing value to" @ " " @ %this.tickPeriodMS @ " " @ "ms (tickPeriodMS)");
            %this.repeatDelayMS = %this.tickPeriodMS;
        }
    }
    if ((0.0 == %this.repetitionSchedule)) {
    }
    if (($gGuiButtonBaseCtrl_MinimumIntervalBetweenEventRepetitions >= %this.repeatDelayMS)) {
    }
    if (($gGuiButtonBaseCtrl_MinimumIntervalBetweenEventRepetitions >= %this.tickPeriodMS)) {
        %this.repetitionSchedule = %this.schedule(%this.repeatDelayMS, onMouseEventDoRepeat, %modifier, %point, %clickCount);
    }
    Parent::onMouseDown(%this, %modifier, %point, %clickCount);
};
function GuiButtonBaseCtrl::onMouseEventDoRepeat(%this, %modifier, %point, %clickCount) {
    if (!(%this.isActive())) {
        %this.forceMouseEventTimeout();
    }
    if ((0.0 != %this.repetitionSchedule)) {
    }
    if ((0.0 > %this.tickPeriodMS)) {
        cancel(%this.repetitionSchedule);
        %this.performClick();
        %this.repetitionSchedule = %this.schedule(%this.tickPeriodMS, onMouseEventDoRepeat, %modifier, %point, %clickCount);
    }
};
function GuiButtonBaseCtrl::onMouseUp(%this, %modifier, %point, %clickCount) {
    %this.forceMouseEventTimeout();
};
function GuiButtonBaseCtrl::forceMouseEventTimeout(%this) {
    if ((0.0 != %this.repetitionSchedule)) {
        cancel(%this.repetitionSchedule);
        %this.repetitionSchedule = 0;
    }
};
