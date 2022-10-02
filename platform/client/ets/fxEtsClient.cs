$fxEts::todColorMod = "0 0 0 0";
$fxEts::BrightnessFlashColor = "0 0 0 0";
$fxEts::BrightnessFlashTimerPeriod = 100;
$fxEts::BrightnessFlashTimerID = 0;
$fxEts::BrightnessFlashDecay = 0.96;
function fxEts::updateExposureFilter()
{
    if (!isObject(ExposureFilter))
    {
        return ;
    }
    if ($UserPref::Video::exposureQualitySetting == 0)
    {
        return ;
    }
    else
    {
        if (($renderQuality == 0) && ($UserPref::Video::exposureQualitySetting == 3))
        {
            return ;
        }
    }
    %valSld = $UserPref::Video::Exposure;
    %colSld = %valSld SPC %valSld SPC %valSld SPC 1;
    %colTOD = $fxEts::todColorMod;
    %colTOD = ColorScale(%colTOD, 0.8);
    if ($pref::fxEts::TODNotInside)
    {
        if (isPointInside($player.getPosition()))
        {
            %colTOD = "0 0 0 0";
        }
    }
    %colFin = ColorAdd(%colSld, %colTOD);
    %colFin = ColorAdd(%colFin, $fxEts::BrightnessFlashColor);
    ExposureFilter.exposure = %colFin;
    ExposureFilterSelfView.exposure = %colFin;
    %atNeutral = 0;
    if (VectorDist(%colFin, "0.5 0.5 0.5") < 0.01)
    {
        %atNeutral = 1;
    }
    ExposureFilter.setVisible(!%atNeutral);
    ExposureFilterSelfView.setVisible(!%atNeutral);
    if (!isObject(EditorExposureFilter))
    {
        return ;
    }
    EditorExposureFilter.exposure = %colFin;
    EditorExposureFilter.setVisible(!%atNeutral);
    return ;
}
function fxEts::updateTOD(%hod)
{
    fxEts::updateTODColor(fxEts::getColorForTOD((%hod * 60) * 60));
    if (isObject(DevOptsTextTOD))
    {
        DevOptsTextTOD.setValue(mFloor(%hod + 0.5));
        %r = mFloor((getWord($fxEts::todColorMod, 0) * 100) + 0.5) / 100;
        %g = mFloor((getWord($fxEts::todColorMod, 1) * 100) + 0.5) / 100;
        %b = mFloor((getWord($fxEts::todColorMod, 2) * 100) + 0.5) / 100;
        DevOptsEditTODColor.setValue(%r SPC %g SPC %b);
    }
    return ;
}
function fxEts::updateTODColor(%color)
{
    $fxEts::todColorMod = %color;
    fxEts::updateExposureFilter();
    return ;
}
function fxEts::TODTick()
{
    if (!isObject(ExposureFilter))
    {
        return ;
    }
    %cityTOD = getSimTime() + $Sim::TimeDeltaToCity;
    %cityHOD = %cityTOD / ((60 * 60) * 1000);
    while (%cityHOD > 24)
    {
        %cityHOD = %cityHOD - 24;
    }
    while (%cityHOD < 0)
    {
        %cityHOD = %cityHOD + 24;
    }
    fxEts::updateTOD(%cityHOD);
    if (isObject(DevOptsSliderTOD))
    {
        DevOptsSliderTOD.setValue(%cityHOD);
        DevOptsTextTOD.setValue(mFloor(%cityHOD + 0.5));
    }
    fxEts::updateExposureFilter();
    return ;
}
function fxEts::TODTimer()
{
    cancel($fxEts::TODTimerID);
    fxEts::TODTick();
    if ($fxEts::TOD::ColorModSamplesNum <= 1)
    {
        error("only one or fewer color samples, turning off TODTimer.");
    }
    else
    {
        if ($fxEts::TODTimerPeriod > 0)
        {
            $fxEts::TODTimerID = schedule($fxEts::TODTimerPeriod, 0, "eval", "fxEts::TODTimer();");
        }
    }
    return ;
}
function ClientCmdTODColorMods(%s)
{
    %num = getWord(%s, 0);
    $fxEts::TOD::ColorModSamplesNum = %num;
    %n = 0;
    while (%n < %num)
    {
        %hour = getWord(%s, (%n * 4) + 1);
        %col = getWord(%s, (%n * 4) + 2);
        %col = %col SPC getWord(%s, (%n * 4) + 3);
        %col = %col SPC getWord(%s, (%n * 4) + 4);
        $fxEts::TOD::ColorModSample[%n,hour] = %hour ;
        $fxEts::TOD::ColorModSample[%n,color] = %col ;
        %n = %n + 1;
    }
    fxEts::TODTimer();
    return ;
}
function fxEts::getColorForTOD(%sod)
{
    if ($fxEts::TOD::ColorModSamplesNum == 0)
    {
        error("No Color Mod Table!");
        return "0 0 0 0";
    }
    %lowerBound = -1;
    %upperBound = 1000;
    %hod = %sod / (60 * 60);
    %n = 0;
    while (%n < $fxEts::TOD::ColorModSamplesNum)
    {
        %hour = $fxEts::TOD::ColorModSample[%n,hour] % 24;
        if ((%hour <= %hod) && (%hour > %lowerBound))
        {
            %lowerBound = %n;
        }
        if ((%hour >= %hod) && (%hour < %upperBound))
        {
            %upperBound = %n;
        }
        %n = %n + 1;
    }
    if (%upperBound == 1000)
    {
        %upperBound = %n - 1;
    }
    if (%lowerBound == -1)
    {
        %lowerBound = 0;
    }
    %lowerHour = $fxEts::TOD::ColorModSample[%lowerBound,hour];
    %lowerColr = $fxEts::TOD::ColorModSample[%lowerBound,color];
    %upperHour = $fxEts::TOD::ColorModSample[%upperBound,hour];
    %upperColr = $fxEts::TOD::ColorModSample[%upperBound,color];
    if (%lowerHour == %upperHour)
    {
        return %lowerColr;
    }
    %s = (%hod - %lowerHour) / (%upperHour - %lowerHour);
    return ColorInterp(%lowerColr, %upperColr, %s);
}
function fxEts::BrightnessFlashTick()
{
    $fxEts::BrightnessFlashColor = ColorScale($fxEts::BrightnessFlashColor, $fxEts::BrightnessFlashDecay);
    %doMore = ColorLenSquared($fxEts::BrightnessFlashColor) > 0.0001;
    if (!%doMore)
    {
        $fxEts::BrightnessFlashColor = "0 0 0 0";
    }
    fxEts::updateExposureFilter();
    return %doMore;
}
function fxEts::BrightnessFlashTimer()
{
    cancel($fxEts::BrightnessFlashTimerID);
    if (fxEts::BrightnessFlashTick() && ($fxEts::BrightnessFlashTimerPeriod > 0))
    {
        $fxEts::BrightnessFlashTimerID = schedule($fxEts::BrightnessFlashTimerPeriod, 0, "eval", "fxEts::BrightnessFlashTimer();");
    }
    return ;
}
function ClientCmdBrightnessFlash(%s)
{
    $fxEts::BrightnessFlashColor = %s;
    fxEts::BrightnessFlashTimer();
    return ;
}
