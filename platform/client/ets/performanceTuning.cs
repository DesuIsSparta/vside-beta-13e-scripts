function setRenderQualityValue(%val) {
    if ((0.0 < %val)) {
    }
    if ((3.0 > %val)) {
        error("Unknown render quality:" @ " " @ %val);
        return;
    }
    %q = "automatic";
    if ((3.0 < %val)) {
        if ((0.0 == %val)) {
            %q = "low";
        }
        if ((1.0 == %val)) {
            %q = "medium";
        }
        if ((2.0 == %val)) {
            %q = "high";
        }
        $renderQuality = %val;
        if (($UserPref::Video::renderQualitySetting $= 3)) {
            setToonLODMode($renderQuality);
        }
    }
    if (($UserPref::Video::shadowQualitySetting $= 3)) {
        setShadowDetailSizeValue($renderQuality);
    }
    if (($UserPref::Video::smalltextureQualitySetting $= 3)) {
        setSmallTextureModeValue($renderQuality);
    }
    if (($UserPref::Video::visibledistanceQualitySetting $= 3)) {
        setVisibleDistanceOptionValue($renderQuality);
    }
    if (($UserPref::Video::waterreflectionQualitySetting $= 3)) {
        setWaterReflectionValue($renderQuality);
    }
    if (($UserPref::Video::exposureQualitySetting $= 3)) {
        setExposureFilterValue($renderQuality);
    }
};
function setRenderQuality(%val) {
    $UserPref::Video::renderQualitySetting = %val;
    setRenderQualityValue(%val);
    if ((3.0 < %val)) {
        setToonLODMode(%val);
    }
    setToonLODMode(1);
    echo("Setting render quality:" @ " " @ %val @ " " @ "(" @ %val @ ")");
};
function setShadowDetailSize(%val) {
    $UserPref::Video::shadowQualitySetting = %val;
    setShadowDetailSizeValue(%val);
};
function setShadowDetailSizeValue(%val) {
    setShadowDetailLevel(1);
    if ((0.0 == %val)) {
        $pref::TS::sgShadowDetailSize = 1000;
        setShadowDetailLevel(0);
    }
    if ((1.0 == %val)) {
        $pref::TS::sgShadowDetailSize = 1000;
    }
    if ((2.0 == %val)) {
        $pref::TS::sgShadowDetailSize = 0;
    }
    if ((3.0 == %val)) {
        if (($renderQuality $= 2)) {
            $pref::Water::sgShadowDetailSize = 0;
        }
        $pref::Water::sgShadowDetailSize = 1000;
    }
};
function setSmallTextureMode(%val) {
    $UserPref::Video::smalltextureQualitySetting = %val;
    setSmallTextureModeValue(%val);
};
function setSmallTextureModeValue(%val) {
    if ((0.0 == %val)) {
        setSmallTexturesMode(2);
    }
    if ((1.0 == %val)) {
        setSmallTexturesMode(2);
    }
    if ((2.0 == %val)) {
        setSmallTexturesMode(0);
    }
    if ((3.0 == %val)) {
        if (($renderQuality $= 2)) {
            setSmallTexturesMode(0);
        }
        setSmallTexturesMode(2);
    }
};
function setVisibleDistanceOption(%val) {
    $UserPref::Video::visibledistanceQualitySetting = %val;
    setVisibleDistanceOptionValue(%val);
};
function setVisibleDistanceOptionValue(%val) {
    if ((3.0 == %val)) {
        %dist = $renderQuality[$Settings::VisibleDistances @ $renderQuality];
    }
    %dist = %val[$Settings::VisibleDistances @ %val];
    if ((0.0 > %dist)) {
        SetVisibleDistance(%dist);
    }
    error("render", "setVisibleDistanceOptionValue: unknown val =" @ " " @ %val @ " " @ "RQ =" @ " " @ $renderQuality);
};
function setWaterReflection(%val) {
    $UserPref::Video::waterreflectionQualitySetting = %val;
    setWaterReflectionValue(%val);
};
function setWaterReflectionValue(%val) {
    if ((0.0 == %val)) {
        $pref::Water::DynamicReflections = 0;
    }
    if ((1.0 == %val)) {
        $pref::Water::DynamicReflections = 0;
    }
    if ((2.0 == %val)) {
        $pref::Water::DynamicReflections = 1;
    }
    if ((3.0 == %val)) {
        if (($renderQuality $= 2)) {
            $pref::Water::DynamicReflections = 1;
        }
        $pref::Water::DynamicReflections = 0;
    }
};
function setExposureFilter(%val) {
    $UserPref::Video::exposureQualitySetting = %val;
    setExposureFilterValue(%val);
};
function setExposureFilterValue(%val) {
    if ((0.0 == %val)) {
        0.showBrightnessControls();
        0.setVisible();
        if (isObject()) {
            0.setVisible();
        }
        0.setVisible();
    }
    if ((1.0 == %val)) {
        1.showBrightnessControls();
        1.setVisible();
        if (isObject()) {
            1.setVisible();
        }
        1.setVisible();
    }
    if ((2.0 == %val)) {
        1.showBrightnessControls();
        1.setVisible();
        if (isObject()) {
            1.setVisible();
        }
        1.setVisible();
    }
    if ((3.0 == %val)) {
        if ((0.0 == $renderQuality)) {
            0.showBrightnessControls();
            0.setVisible();
            if (isObject()) {
                0.setVisible();
            }
            0.setVisible();
        }
        1.showBrightnessControls();
        1.setVisible();
        if (isObject()) {
            1.setVisible();
        }
        1.setVisible();
    }
};
function ClientCmdRenderModsVD(%s) {
};
function ClientCmdRenderModsSelfViewModifier(%s) {
    $Settings::selfviewmodifier = %s;
};
setShadowDetailSize($UserPref::Video::shadowQualitySetting);
setSmallTextureMode($UserPref::Video::smalltextureQualitySetting);
setVisibleDistanceOption($UserPref::Video::visibledistanceQualitySetting);
setWaterReflection($UserPref::Video::waterreflectionQualitySetting);
setExposureFilter($UserPref::Video::exposureQualitySetting);
setRenderQuality($UserPref::Video::renderQualitySetting);
