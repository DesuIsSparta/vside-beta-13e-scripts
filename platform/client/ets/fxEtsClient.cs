$fxEts::todColorMod = "0 0 0 0";
$fxEts::BrightnessFlashColor = "0 0 0 0";
$fxEts::BrightnessFlashTimerPeriod = 100;
$fxEts::BrightnessFlashTimerID = 0;
$fxEts::BrightnessFlashDecay = 0.96;
function fxEts::updateExposureFilter() {
    if (!isObject(ExposureFilter)) {
        return;
    }
    if (($UserPref::Video::exposureQualitySetting == 0.0)) {
        return;
    }
    if (($renderQuality == 0.0)) {
    }
    if (($UserPref::Video::exposureQualitySetting == 3.0)) {
        return;
    }
    %valSld = $UserPref::Video::Exposure;
    %colSld = %valSld @ " " @ %valSld @ " " @ %valSld @ " " @ 1;
    %colTOD = $fxEts::todColorMod;
    %colTOD = ColorScale(%colTOD, 0.8);
    if ($pref::fxEts::TODNotInside && isPointInside($player.getPosition())) {
        %colTOD = "0 0 0 0";
    }
    %colFin = ColorAdd(%colSld, %colTOD);
    %colFin = ColorAdd(%colFin, $fxEts::BrightnessFlashColor);
    ExposureFilter.exposure = %colFin;
    ExposureFilterSelfView.exposure = %colFin;
    %atNeutral = 0;
    if ((VectorDist(%colFin, "0.5 0.5 0.5") < 0.01)) {
        %atNeutral = 1;
    }
    ExposureFilter.setVisible(!%atNeutral);
    ExposureFilterSelfView.setVisible(!%atNeutral);
    if (!isObject(EditorExposureFilter)) {
        return;
    }
    EditorExposureFilter.exposure = %colFin;
    EditorExposureFilter.setVisible(!%atNeutral);
};
function fxEts::updateTOD(%hod) {
    fxEts::updateTODColor(fxEts::getColorForTOD(((%hod * 60.0) * 60.0)));
    if (isObject(DevOptsTextTOD)) {
        DevOptsTextTOD.setValue(mFloor((%hod + 0.5)));
        %r = (mFloor(((getWord($fxEts::todColorMod, 0) * 100.0) + 0.5)) / 100.0);
        %g = (mFloor(((getWord($fxEts::todColorMod, 1) * 100.0) + 0.5)) / 100.0);
        %b = (mFloor(((getWord($fxEts::todColorMod, 2) * 100.0) + 0.5)) / 100.0);
        DevOptsEditTODColor.setValue(%r @ " " @ %g @ " " @ %b);
    }
};
function fxEts::updateTODColor(%color) {
    $fxEts::todColorMod = %color;
    fxEts::updateExposureFilter();
};
function fxEts::TODTick() {
    if (!isObject(ExposureFilter)) {
        return;
    }
    %cityTOD = (getSimTime() + $Sim::TimeDeltaToCity);
    %cityHOD = (%cityTOD / ((60.0 * 60.0) * 1000.0));
    while ((%cityHOD > 24.0)) {
        %cityHOD = (%cityHOD - 24.0);
    }
    while ((%cityHOD < 0.0)) {
        %cityHOD = (%cityHOD + 24.0);
        (%cityHOD > 24.0);
    }
    fxEts::updateTOD(%cityHOD);
    if (isObject(DevOptsSliderTOD)) {
        DevOptsSliderTOD.setValue(%cityHOD);
        DevOptsTextTOD.setValue(mFloor((%cityHOD + 0.5)));
    }
    fxEts::updateExposureFilter();
};
function fxEts::TODTimer() {
    cancel($fxEts::TODTimerID);
    fxEts::TODTick();
    if (($fxEts::TOD::ColorModSamplesNum <= 1.0)) {
        error("only one or fewer color samples, turning off TODTimer.");
    }
    if (($fxEts::TODTimerPeriod > 0.0)) {
        $fxEts::TODTimerID = schedule($fxEts::TODTimerPeriod, 0, "eval", "fxEts::TODTimer();");
    }
};
function ClientCmdTODColorMods(%s) {
    %num = getWord(%s, 0);
    $fxEts::TOD::ColorModSamplesNum = %num;
    %n = 0;
    while ((%n < %num)) {
        %hour = getWord(%s, ((%n * 4.0) + 1.0));
        %col = getWord(%s, ((%n * 4.0) + 2.0));
        %col = %col @ " " @ getWord(%s, ((%n * 4.0) + 3.0));
        %col = %col @ " " @ getWord(%s, ((%n * 4.0) + 4.0));
        %n[%hour @ $fxEts::TOD::ColorModSample TAB %n @ hour] = ;
        %n[%col @ $fxEts::TOD::ColorModSample TAB %n @ color] = ;
        %n = (%n + 1.0);
    }
    fxEts::TODTimer();
};
function fxEts::getColorForTOD(%sod) {
    if (($fxEts::TOD::ColorModSamplesNum == 0.0)) {
        error("No Color Mod Table!");
        return "0 0 0 0";
    }
    %lowerBound = -(1.0);
    %upperBound = 1000;
    %hod = (%sod / (60.0 * 60.0));
    %n = 0;
    while ((%n < $fxEts::TOD::ColorModSamplesNum)) {
        %hour = (%n[24 @ $fxEts::TOD::ColorModSample TAB %n @ hour] % );
        if ((%hour <= %hod)) {
        }
        if ((%hour > %lowerBound)) {
            %lowerBound = %n;
        }
        if ((%hour >= %hod)) {
        }
        if ((%hour < %upperBound)) {
            %upperBound = %n;
        }
        %n = (%n + 1.0);
    }
    if ((%upperBound == 1000.0)) {
        %upperBound = (%n - 1.0);
        (%n < $fxEts::TOD::ColorModSamplesNum);
    }
    if ((%lowerBound == -(1.0))) {
        %lowerBound = 0;
    }
    %lowerHour = %lowerBound[$fxEts::TOD::ColorModSample TAB %lowerBound @ hour];
    %lowerColr = %lowerBound[$fxEts::TOD::ColorModSample TAB %lowerBound @ color];
    %upperHour = %upperBound[$fxEts::TOD::ColorModSample TAB %upperBound @ hour];
    %upperColr = %upperBound[$fxEts::TOD::ColorModSample TAB %upperBound @ color];
    if ((%lowerHour == %upperHour)) {
        return %lowerColr;
    }
    %s = ((%hod - %lowerHour) / (%upperHour - %lowerHour));
    return ColorInterp(%lowerColr, %upperColr, %s);
};
function fxEts::BrightnessFlashTick() {
    $fxEts::BrightnessFlashColor = ColorScale($fxEts::BrightnessFlashColor, $fxEts::BrightnessFlashDecay);
    %doMore = (ColorLenSquared($fxEts::BrightnessFlashColor) > 0.0001);
    if (!%doMore) {
        $fxEts::BrightnessFlashColor = "0 0 0 0";
    }
    fxEts::updateExposureFilter();
    return %doMore;
};
function fxEts::BrightnessFlashTimer() {
    cancel($fxEts::BrightnessFlashTimerID);
    if (fxEts::BrightnessFlashTick()) {
    }
    if (($fxEts::BrightnessFlashTimerPeriod > 0.0)) {
        $fxEts::BrightnessFlashTimerID = schedule($fxEts::BrightnessFlashTimerPeriod, 0, "eval", "fxEts::BrightnessFlashTimer();");
    }
};
function ClientCmdBrightnessFlash(%s) {
    $fxEts::BrightnessFlashColor = %s;
    fxEts::BrightnessFlashTimer();
};
