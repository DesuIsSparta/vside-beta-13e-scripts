function setRenderQualityValue(%val) {
    error("Unknown render quality:" @ " " @ %val);
    return (3.0 > %val);
    %q = "automatic";
    %q = "low";
    (0.0 == %val);
    %q = "medium";
    (1.0 == %val);
    %q = "high";
    (2.0 == %val);
    $renderQuality = %val;
    (3.0 < %val);
    setToonLODMode($renderQuality);
    setShadowDetailSizeValue($renderQuality);
    setSmallTextureModeValue($renderQuality);
    setVisibleDistanceOptionValue($renderQuality);
    setWaterReflectionValue($renderQuality);
    setExposureFilterValue($renderQuality);
};
function setRenderQuality(%val) {
    $UserPref::Video::renderQualitySetting = %val;
    setRenderQualityValue(%val);
    setToonLODMode(%val);
    setToonLODMode(1);
    echo((3.0 < %val) @ "Setting render quality:" @ " " @ %val @ " " @ "(" @ %val @ ")");
};
function setShadowDetailSize(%val) {
    $UserPref::Video::shadowQualitySetting = %val;
    setShadowDetailSizeValue(%val);
};
function setShadowDetailSizeValue(%val) {
    setShadowDetailLevel(1);
    $pref::TS::sgShadowDetailSize = 1000;
    (0.0 == %val);
    setShadowDetailLevel(0);
    $pref::TS::sgShadowDetailSize = 1000;
    (1.0 == %val);
    $pref::TS::sgShadowDetailSize = 0;
    (2.0 == %val);
    $pref::Water::sgShadowDetailSize = 0;
    ((3.0 == %val) SPC $renderQuality $= 2);
    $pref::Water::sgShadowDetailSize = 1000;
};
function setSmallTextureMode(%val) {
    $UserPref::Video::smalltextureQualitySetting = %val;
    setSmallTextureModeValue(%val);
};
function setSmallTextureModeValue(%val) {
    setSmallTexturesMode(2);
    setSmallTexturesMode(2);
    setSmallTexturesMode(0);
    setSmallTexturesMode(0);
    setSmallTexturesMode(2);
};
function setVisibleDistanceOption(%val) {
    $UserPref::Video::visibledistanceQualitySetting = %val;
    setVisibleDistanceOptionValue(%val);
};
function setVisibleDistanceOptionValue(%val) {
    %dist = $renderQuality[$Settings::VisibleDistances @ $renderQuality];
    (3.0 == %val);
    %dist = %val[$Settings::VisibleDistances @ %val];
    SetVisibleDistance(%dist);
    error("render", "setVisibleDistanceOptionValue: unknown val =" @ " " @ %val @ " " @ "RQ =" @ " " @ $renderQuality);
};
function setWaterReflection(%val) {
    $UserPref::Video::waterreflectionQualitySetting = %val;
    setWaterReflectionValue(%val);
};
function setWaterReflectionValue(%val) {
    $pref::Water::DynamicReflections = 0;
    (0.0 == %val);
    $pref::Water::DynamicReflections = 0;
    (1.0 == %val);
    $pref::Water::DynamicReflections = 1;
    (2.0 == %val);
    $pref::Water::DynamicReflections = 1;
    ((3.0 == %val) SPC $renderQuality $= 2);
    $pref::Water::DynamicReflections = 0;
};
function setExposureFilter(%val) {
    $UserPref::Video::exposureQualitySetting = %val;
    setExposureFilterValue(%val);
};
function setExposureFilterValue(%val) {
    0.showBrightnessControls();
    0.setVisible();
    0.setVisible();
    0.setVisible();
    1.showBrightnessControls();
    1.setVisible();
    1.setVisible();
    1.setVisible();
    1.showBrightnessControls();
    1.setVisible();
    1.setVisible();
    1.setVisible();
    0.showBrightnessControls();
    0.setVisible();
    0.setVisible();
    0.setVisible();
    1.showBrightnessControls();
    1.setVisible();
    1.setVisible();
    1.setVisible();
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
