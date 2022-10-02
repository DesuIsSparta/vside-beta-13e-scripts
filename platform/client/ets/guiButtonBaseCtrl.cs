$gGuiButtonBaseCtrl_MinimumIntervalBetweenEventRepetitions = 50;
function GuiButtonBaseCtrl::onMouseDown(%this, %modifier, %point, %clickCount)
{
    if (%this.tickPeriodMS $= "")
    {
        %this.tickPeriodMS = 0;
    }
    else
    {
        if (%this.tickPeriodMS != 0)
        {
            if (%this.tickPeriodMS < 0)
            {
                %this.tickPeriodMS = 0;
            }
            else
            {
                if (%this.tickPeriodMS < $gGuiButtonBaseCtrl_MinimumIntervalBetweenEventRepetitions)
                {
                    warn(getScopeName() SPC "- button" SPC %this.getName() $= "" ? %this.getId() : %this.getName() SPC "has invalid tickPeriodMS=" @ %this.tickPeriodMS @ ", changing value to" SPC $gGuiButtonBaseCtrl_MinimumIntervalBetweenEventRepetitions SPC "ms");
                    %this.tickPeriodMS = $gGuiButtonBaseCtrl_MinimumIntervalBetweenEventRepetitions;
                }
            }
        }
    }
    if (%this.repeatDelayMS $= "")
    {
        %this.repeatDelayMS = 0;
    }
    else
    {
        if (%this.repeatDelayMS != 0)
        {
            if (%this.repeatDelayMS < 0)
            {
                %this.repeatDelayMS = 0;
            }
            else
            {
                if (%this.repeatDelayMS < %this.tickPeriodMS)
                {
                    warn(getScopeName() SPC "- button" SPC %this.getName() $= "" ? %this.getId() : %this.getName() SPC "has invalid repeatDelayMS=" @ %this.repeatDelayMS @ ", changing value to" SPC %this.tickPeriodMS SPC "ms (tickPeriodMS)");
                    %this.repeatDelayMS = %this.tickPeriodMS;
                }
            }
        }
    }
    if (((%this.repetitionSchedule == 0) && (%this.repeatDelayMS >= $gGuiButtonBaseCtrl_MinimumIntervalBetweenEventRepetitions)) && (%this.tickPeriodMS >= $gGuiButtonBaseCtrl_MinimumIntervalBetweenEventRepetitions))
    {
        %this.repetitionSchedule = %this.schedule(%this.repeatDelayMS, onMouseEventDoRepeat, %modifier, %point, %clickCount);
    }
    Parent::onMouseDown(%this, %modifier, %point, %clickCount);
    return ;
}
function GuiButtonBaseCtrl::onMouseEventDoRepeat(%this, %modifier, %point, %clickCount)
{
    if (!%this.isActive())
    {
        %this.forceMouseEventTimeout();
    }
    else
    {
        if ((%this.repetitionSchedule != 0) && (%this.tickPeriodMS > 0))
        {
            cancel(%this.repetitionSchedule);
            %this.performClick();
            %this.repetitionSchedule = %this.schedule(%this.tickPeriodMS, onMouseEventDoRepeat, %modifier, %point, %clickCount);
        }
    }
    return ;
}
function GuiButtonBaseCtrl::onMouseUp(%this, %modifier, %point, %clickCount)
{
    %this.forceMouseEventTimeout();
    return ;
}
function GuiButtonBaseCtrl::forceMouseEventTimeout(%this)
{
    if (%this.repetitionSchedule != 0)
    {
        cancel(%this.repetitionSchedule);
        %this.repetitionSchedule = 0;
    }
    return ;
}
