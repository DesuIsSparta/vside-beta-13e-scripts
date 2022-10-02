function setRenderQualityValue(%val)
{
    if ((%val < 0) && (%val > 3))
    {
        error("Unknown render quality:" SPC %val);
        return ;
    }
    %q = "automatic";
    if (%val < 3)
    {
        if (%val == 0)
        {
            %q = "low";
        }
        else
        {
            if (%val == 1)
            {
                %q = "medium";
            }
            else
            {
                if (%val == 2)
                {
                    %q = "high";
                }
            }
        }
        $renderQuality = %val;
        if ($UserPref::Video::renderQualitySetting $= 3)
        {
            setToonLODMode($renderQuality);
        }
    }
    if ($UserPref::Video::shadowQualitySetting $= 3)
    {
        setShadowDetailSizeValue($renderQuality);
    }
    if ($UserPref::Video::smalltextureQualitySetting $= 3)
    {
        setSmallTextureModeValue($renderQuality);
    }
    if ($UserPref::Video::visibledistanceQualitySetting $= 3)
    {
        setVisibleDistanceOptionValue($renderQuality);
    }
    if ($UserPref::Video::waterreflectionQualitySetting $= 3)
    {
        setWaterReflectionValue($renderQuality);
    }
    if ($UserPref::Video::exposureQualitySetting $= 3)
    {
        setExposureFilterValue($renderQuality);
    }
    return ;
}
function setRenderQuality(%val)
{
    $UserPref::Video::renderQualitySetting = %val;
    setRenderQualityValue(%val);
    if (%val < 3)
    {
        setToonLODMode(%val);
    }
    else
    {
        setToonLODMode(1);
    }
    echo("Setting render quality:" SPC %val SPC "(" @ %val @ ")");
    return ;
}
function setShadowDetailSize(%val)
{
    $UserPref::Video::shadowQualitySetting = %val;
    setShadowDetailSizeValue(%val);
    return ;
}
function setShadowDetailSizeValue(%val)
{
    setShadowDetailLevel(1);
    if (%val == 0)
    {
        $pref::TS::sgShadowDetailSize = 1000;
        setShadowDetailLevel(0);
    }
    else
    {
        if (%val == 1)
        {
            $pref::TS::sgShadowDetailSize = 1000;
        }
        else
        {
            if (%val == 2)
            {
                $pref::TS::sgShadowDetailSize = 0;
            }
            else
            {
                if (%val == 3)
                {
                    if ($renderQuality $= 2)
                    {
                        $pref::Water::sgShadowDetailSize = 0;
                    }
                    else
                    {
                        $pref::Water::sgShadowDetailSize = 1000;
                    }
                }
            }
        }
    }
    return ;
}
function setSmallTextureMode(%val)
{
    $UserPref::Video::smalltextureQualitySetting = %val;
    setSmallTextureModeValue(%val);
    return ;
}
function setSmallTextureModeValue(%val)
{
    if (%val == 0)
    {
        setSmallTexturesMode(2);
    }
    else
    {
        if (%val == 1)
        {
            setSmallTexturesMode(2);
        }
        else
        {
            if (%val == 2)
            {
                setSmallTexturesMode(0);
            }
            else
            {
                if (%val == 3)
                {
                    if ($renderQuality $= 2)
                    {
                        setSmallTexturesMode(0);
                    }
                    else
                    {
                        setSmallTexturesMode(2);
                    }
                }
            }
        }
    }
    return ;
}
function setVisibleDistanceOption(%val)
{
    $UserPref::Video::visibledistanceQualitySetting = %val;
    setVisibleDistanceOptionValue(%val);
    return ;
}
function setVisibleDistanceOptionValue(%val)
{
    if (%val == 3)
    {
        %dist = $Settings::VisibleDistances[$renderQuality];
    }
    else
    {
        %dist = $Settings::VisibleDistances[%val];
    }
    if (%dist > 0)
    {
        SetVisibleDistance(%dist);
    }
    else
    {
        error("render", "setVisibleDistanceOptionValue: unknown val =" SPC %val SPC "RQ =" SPC $renderQuality);
    }
    return ;
}
function setWaterReflection(%val)
{
    $UserPref::Video::waterreflectionQualitySetting = %val;
    setWaterReflectionValue(%val);
    return ;
}
function setWaterReflectionValue(%val)
{
    if (%val == 0)
    {
        $pref::Water::DynamicReflections = 0;
    }
    else
    {
        if (%val == 1)
        {
            $pref::Water::DynamicReflections = 0;
        }
        else
        {
            if (%val == 2)
            {
                $pref::Water::DynamicReflections = 1;
            }
            else
            {
                if (%val == 3)
                {
                    if ($renderQuality $= 2)
                    {
                        $pref::Water::DynamicReflections = 1;
                    }
                    else
                    {
                        $pref::Water::DynamicReflections = 0;
                    }
                }
            }
        }
    }
    return ;
}
function setExposureFilter(%val)
{
    $UserPref::Video::exposureQualitySetting = %val;
    setExposureFilterValue(%val);
    return ;
}
function setExposureFilterValue(%val)
{
    if (%val == 0)
    {
        OptionsPanel.showBrightnessControls(0);
        ExposureFilter.setVisible(0);
        if (isObject(EditorExposureFilter))
        {
            EditorExposureFilter.setVisible(0);
        }
        ExposureFilterSelfView.setVisible(0);
    }
    else
    {
        if (%val == 1)
        {
            OptionsPanel.showBrightnessControls(1);
            ExposureFilter.setVisible(1);
            if (isObject(EditorExposureFilter))
            {
                EditorExposureFilter.setVisible(1);
            }
            ExposureFilterSelfView.setVisible(1);
        }
        else
        {
            if (%val == 2)
            {
                OptionsPanel.showBrightnessControls(1);
                ExposureFilter.setVisible(1);
                if (isObject(EditorExposureFilter))
                {
                    EditorExposureFilter.setVisible(1);
                }
                ExposureFilterSelfView.setVisible(1);
            }
            else
            {
                if (%val == 3)
                {
                    if ($renderQuality == 0)
                    {
                        OptionsPanel.showBrightnessControls(0);
                        ExposureFilter.setVisible(0);
                        if (isObject(EditorExposureFilter))
                        {
                            EditorExposureFilter.setVisible(0);
                        }
                        ExposureFilterSelfView.setVisible(0);
                    }
                    else
                    {
                        OptionsPanel.showBrightnessControls(1);
                        ExposureFilter.setVisible(1);
                        if (isObject(EditorExposureFilter))
                        {
                            EditorExposureFilter.setVisible(1);
                        }
                        ExposureFilterSelfView.setVisible(1);
                    }
                }
            }
        }
    }
    return ;
}
function ClientCmdRenderModsVD(%s)
{
    $Settings::VisibleDistances[0] = getWord(%s, 0) ;
    $Settings::VisibleDistances[1] = getWord(%s, 1) ;
    $Settings::VisibleDistances[2] = getWord(%s, 2) ;
    return ;
}
function ClientCmdRenderModsSelfViewModifier(%s)
{
    $Settings::selfviewmodifier = %s;
    return ;
}
setShadowDetailSize($UserPref::Video::shadowQualitySetting);
setSmallTextureMode($UserPref::Video::smalltextureQualitySetting);
setVisibleDistanceOption($UserPref::Video::visibledistanceQualitySetting);
setWaterReflection($UserPref::Video::waterreflectionQualitySetting);
setExposureFilter($UserPref::Video::exposureQualitySetting);
setRenderQuality($UserPref::Video::renderQualitySetting);

