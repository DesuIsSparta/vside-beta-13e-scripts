$gGuiButtonBaseCtrl_MinimumIntervalBetweenEventRepetitions = 50;
function GuiButtonBaseCtrl::onMouseDown(%this, %modifier, %point, %clickCount)
{
    if ((%this.tickPeriodMS $= ""))
    {
        %this.tickPeriodMS = 0;
    }
    else
    {
        if ((%this.tickPeriodMS != 0.0))
        {
            if ((%this.tickPeriodMS < 0.0))
            {
                %this.tickPeriodMS = 0;
            }
            else
            {
                if ((%this.tickPeriodMS < $gGuiButtonBaseCtrl_MinimumIntervalBetweenEventRepetitions))
                {
                    if ((getScopeName() @ " " @ "- button" @ " " @ " " @ %this.getName() $= ""))
                    {
                    }
                    else
                    {
                    }
                    warn(%this.getId() @ %this.getName() @ " " @ "has invalid tickPeriodMS=" @ %this.tickPeriodMS @ ", changing value to" @ " " @ $gGuiButtonBaseCtrl_MinimumIntervalBetweenEventRepetitions @ " " @ "ms");
                    %this.tickPeriodMS = $gGuiButtonBaseCtrl_MinimumIntervalBetweenEventRepetitions;
                }
            }
        }
    }
    if ((%this.repeatDelayMS $= ""))
    {
        %this.repeatDelayMS = 0;
    }
    else
    {
        if ((%this.repeatDelayMS != 0.0))
        {
            if ((%this.repeatDelayMS < 0.0))
            {
                %this.repeatDelayMS = 0;
            }
            else
            {
                if ((%this.repeatDelayMS < %this.tickPeriodMS))
                {
                    if ((getScopeName() @ " " @ "- button" @ " " @ " " @ %this.getName() $= ""))
                    {
                    }
                    else
                    {
                    }
                    warn(%this.getId() @ %this.getName() @ " " @ "has invalid repeatDelayMS=" @ %this.repeatDelayMS @ ", changing value to" @ " " @ %this.tickPeriodMS @ " " @ "ms (tickPeriodMS)");
                    %this.repeatDelayMS = %this.tickPeriodMS;
                }
            }
        }
    }
    if ((%this.repetitionSchedule == 0.0))
    {
    }
    if ((%this.repeatDelayMS >= $gGuiButtonBaseCtrl_MinimumIntervalBetweenEventRepetitions))
    {
    }
    if ((%this.tickPeriodMS >= $gGuiButtonBaseCtrl_MinimumIntervalBetweenEventRepetitions))
    {
        %this.repetitionSchedule = %this.schedule(%this.repeatDelayMS, onMouseEventDoRepeat, %modifier, %point, %clickCount);
    }
    Parent::onMouseDown(%this, %modifier, %point, %clickCount);
}
function GuiButtonBaseCtrl::onMouseEventDoRepeat(%this, %modifier, %point, %clickCount)
{
    if (!%this.isActive())
    {
        %this.forceMouseEventTimeout();
    }
    else
    {
        if ((%this.repetitionSchedule != 0.0))
        {
        }
        if ((%this.tickPeriodMS > 0.0))
        {
            cancel(%this.repetitionSchedule);
            %this.performClick();
            %this.repetitionSchedule = %this.schedule(%this.tickPeriodMS, onMouseEventDoRepeat, %modifier, %point, %clickCount);
        }
    }
}
function GuiButtonBaseCtrl::onMouseUp(%this, %modifier, %point, %clickCount)
{
    %this.forceMouseEventTimeout();
}
function GuiButtonBaseCtrl::forceMouseEventTimeout(%this)
{
    if ((%this.repetitionSchedule != 0.0))
    {
        cancel(%this.repetitionSchedule);
        %this.repetitionSchedule = 0;
    }
}
