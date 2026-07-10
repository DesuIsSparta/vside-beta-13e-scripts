$fxEts::todColorMod = "0 0 0 0";
$fxEts::BrightnessFlashColor = "0 0 0 0";
$fxEts::BrightnessFlashTimerPeriod = 100;
$fxEts::BrightnessFlashTimerID = 0;
$fxEts::BrightnessFlashDecay = 0.96;
function fxEts::updateExposureFilter() {
    if (!(isObject(ExposureFilter))) {
        return;
    }
    if ((0.0 == $UserPref::Video::exposureQualitySetting)) {
        return;
    }
    if ((0.0 == $renderQuality)) {
    }
    if ((3.0 == $UserPref::Video::exposureQualitySetting)) {
        return;
    }
    %valSld = $UserPref::Video::Exposure;
    %colSld = %valSld @ " " @ %valSld @ " " @ %valSld @ " " @ 1;
    %colTOD = $fxEts::todColorMod;
    %colTOD = ColorScale(%colTOD, 0.8);
    if ($pref::fxEts::TODNotInside) {
        if (isPointInside($player.getPosition())) {
            %colTOD = "0 0 0 0";
        }
    }
    %colFin = ColorAdd(%colSld, %colTOD);
    %colFin = ColorAdd(%colFin, $fxEts::BrightnessFlashColor);
    exposure = %colFin @ ExposureFilter;
    exposure = %colFin @ ExposureFilterSelfView;
    %atNeutral = 0;
    if ((0.01 < VectorDist(%colFin, "0.5 0.5 0.5"))) {
        %atNeutral = 1;
    }
    !(%atNeutral).setVisible();
    !(%atNeutral).setVisible();
    if (!(isObject(EditorExposureFilter))) {
        return ExposureFilterSelfView;
    }
    exposure = %colFin @ EditorExposureFilter;
    !(%atNeutral).setVisible();
};
function fxEts::updateTOD(%hod) {
    fxEts::updateTODColor(fxEts::getColorForTOD((60.0 * (60.0 * %hod))));
    if (isObject(DevOptsTextTOD)) {
        mFloor((0.5 + %hod)).setValue();
        %r = (100.0 / mFloor((0.5 + (100.0 * getWord($fxEts::todColorMod, 0)))));
        DevOptsTextTOD;
        %g = (100.0 / mFloor((0.5 + (100.0 * getWord($fxEts::todColorMod, 1)))));
        %b = (100.0 / mFloor((0.5 + (100.0 * getWord($fxEts::todColorMod, 2)))));
        %r @ " " @ %g @ " " @ %b.setValue();
    }
};
function fxEts::updateTODColor(%color) {
    $fxEts::todColorMod = %color;
    fxEts::updateExposureFilter();
};
function fxEts::TODTick() {
    if (!(isObject(ExposureFilter))) {
        return;
    }
    %cityTOD = ($Sim::TimeDeltaToCity + getSimTime());
    %cityHOD = ((1000.0 * (60.0 * 60.0)) / %cityTOD);
    if ((24.0 > %cityHOD)) {
        %cityHOD = (24.0 - %cityHOD);
    }
    if ((0.0 < %cityHOD)) {
        %cityHOD = (24.0 + %cityHOD);
        (24.0 > %cityHOD);
    }
    fxEts::updateTOD(%cityHOD);
    if (isObject(DevOptsSliderTOD)) {
        %cityHOD.setValue();
        mFloor((0.5 + %cityHOD)).setValue();
    }
    fxEts::updateExposureFilter();
};
function fxEts::TODTimer() {
    cancel($fxEts::TODTimerID);
    fxEts::TODTick();
    if ((1.0 <= $fxEts::TOD::ColorModSamplesNum)) {
        error("only one or fewer color samples, turning off TODTimer.");
    }
    if ((0.0 > $fxEts::TODTimerPeriod)) {
        $fxEts::TODTimerID = schedule($fxEts::TODTimerPeriod, 0, "eval", "fxEts::TODTimer();");
    }
};
function ClientCmdTODColorMods(%s) {
    %num = getWord(%s, 0);
    $fxEts::TOD::ColorModSamplesNum = %num;
    %n = 0;
    if ((%num < %n)) {
        %hour = getWord(%s, (1.0 + (4.0 * %n)));
        %col = getWord(%s, (2.0 + (4.0 * %n)));
        %col = %col @ " " @ getWord(%s, (3.0 + (4.0 * %n)));
        %col = %col @ " " @ getWord(%s, (4.0 + (4.0 * %n)));
        %n[$fxEts::TOD::ColorModSample TAB %n @ hour] = %hour;
        %n[$fxEts::TOD::ColorModSample TAB %n @ color] = %col;
        %n = (1.0 + %n);
    }
    fxEts::TODTimer();
};
function fxEts::getColorForTOD(%sod) {
    if ((0.0 == $fxEts::TOD::ColorModSamplesNum)) {
        error("No Color Mod Table!");
        return "0 0 0 0";
    }
    %lowerBound = -(1.0);
    %upperBound = 1000;
    %hod = ((60.0 * 60.0) / %sod);
    %n = 0;
    if (($fxEts::TOD::ColorModSamplesNum < %n)) {
        %hour = (24 % %n[$fxEts::TOD::ColorModSample TAB %n @ hour]);
        if ((%hod <= %hour)) {
        }
        if ((%lowerBound > %hour)) {
            %lowerBound = %n;
        }
        if ((%hod >= %hour)) {
        }
        if ((%upperBound < %hour)) {
            %upperBound = %n;
        }
        %n = (1.0 + %n);
    }
    if ((1000.0 == %upperBound)) {
        %upperBound = (1.0 - %n);
        ($fxEts::TOD::ColorModSamplesNum < %n);
    }
    if ((-(1.0) == %lowerBound)) {
        %lowerBound = 0;
    }
    %lowerHour = %lowerBound[$fxEts::TOD::ColorModSample TAB %lowerBound @ hour];
    %lowerColr = %lowerBound[$fxEts::TOD::ColorModSample TAB %lowerBound @ color];
    %upperHour = %upperBound[$fxEts::TOD::ColorModSample TAB %upperBound @ hour];
    %upperColr = %upperBound[$fxEts::TOD::ColorModSample TAB %upperBound @ color];
    if ((%upperHour == %lowerHour)) {
        return %lowerColr;
    }
    %s = ((%lowerHour - %upperHour) / (%lowerHour - %hod));
    return ColorInterp(%lowerColr, %upperColr, %s);
};
function fxEts::BrightnessFlashTick() {
    $fxEts::BrightnessFlashColor = ColorScale($fxEts::BrightnessFlashColor, $fxEts::BrightnessFlashDecay);
    %doMore = (0.0001 > ColorLenSquared($fxEts::BrightnessFlashColor));
    if (!(%doMore)) {
        $fxEts::BrightnessFlashColor = "0 0 0 0";
    }
    fxEts::updateExposureFilter();
    return %doMore;
};
function fxEts::BrightnessFlashTimer() {
    cancel($fxEts::BrightnessFlashTimerID);
    if (fxEts::BrightnessFlashTick()) {
    }
    if ((0.0 > $fxEts::BrightnessFlashTimerPeriod)) {
        $fxEts::BrightnessFlashTimerID = schedule($fxEts::BrightnessFlashTimerPeriod, 0, "eval", "fxEts::BrightnessFlashTimer();");
    }
};
function ClientCmdBrightnessFlash(%s) {
    $fxEts::BrightnessFlashColor = %s;
    fxEts::BrightnessFlashTimer();
};
