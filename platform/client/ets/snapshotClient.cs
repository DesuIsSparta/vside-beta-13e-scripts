$screenShotNum = 100;
function snapshot::snapAndUpControlRegion(%control, %fileName, %removeBG)
{
    return snapshot::snapAndUpRegion(%control.getScreenPosition() @ " " @ %control.getExtent(), %fileName, %removeBG);
}
function snapControl(%ctrl, %fileName)
{
    %origin = %ctrl.getPosition();
    %extent = %ctrl.getExtent();
    %rect = %origin @ " " @ %extent;
    shootscreen(%fileName, %rect);
}
function snapshot::snapAndUpRegion(%region, %fileName, %removeBG)
{
    if (%fileName $= "")
    {
        %fileName = "screenshot_" @ getSubStr(getTimeStamp(), 0, 17) @ "_" @ $screenShotNum;
    }
    %fn_orig = %fileName;
    if ($Pref::Video::screenShotFormat $= "JPEG")
    {
        %ext = ".jpg";
    }
    else
    {
        if ($Pref::Video::screenShotFormat $= "PNG")
        {
            %ext = ".png";
        }
        %ext = ".png";
    }
    %fileName = %fileName @ %ext;
    %uploader = "";
    if (snapshotTool::snapRegion(%region, %fileName))
    {
        $screenShotNum = $screenShotNum + 1;
        %uploader = new URLPostObject("");
        %uploader.setProgress(1);
        %uploader.setURL($Net::UploadPhotoURL);
        %uploader.setURLParam("user", $Player::Name);
        %uploader.setURLParam("token", $Token);
        %uploader.setURLParam("type", "avatar");
        %uploader.setPostFile("imageBody", %fileName);
        if (%uploader.start())
        {
            if (isObject(CURLSimGroup))
            {
                CURLSimGroup.add(%uploader);
            }
        }
        else
        {
            error("Unable to upload avatar photo." @ " " @ getTrace());
        }
    }
    else
    {
        error("Unable to capture region." @ " " @ getTrace());
    }
    return %uploader;
}
function GuiControl::snapshot(%this, %fileName)
{
    return snapshot::snapRegion(%this.getScreenPosition() @ " " @ %this.getExtent(), %fileName);
}
function snapshot::snapRegion(%region, %fileName)
{
    shootscreen(%fileName, %region);
}
function getScreenShotMetaData(%guiTSCtrl)
{
    if ($pref::Render::orthoScale != 1)
    {
        return getScreenShotMetaDataOrtho(%guiTSCtrl);
    }
    %cameraTransform = PlayGui.getLastCameraTransform();
    %numPts = 0;
    %samplePts[%numPts] = "0 0";
    %numPts = %numPts + 1;
    %samplePts[%numPts] = "1 0";
    %numPts = %numPts + 1;
    %samplePts[%numPts] = "0 1";
    %numPts = %numPts + 1;
    %samplePts[%numPts] = "1 1";
    %numPts = %numPts + 1;
    %samplePts[%numPts] = "0.5 0.5";
    %numPts = %numPts + 1;
    %ctrlExtent = %guiTSCtrl.getExtent();
    %exempt = "";
    %ret = "";
    %ret = %ret @ "\n" @ "// %cameraTransform =" @ " " @ %cameraTransform;
    %ret = %ret @ "\n" @ "// %orthoScale      =" @ " " @ $pref::Render::orthoScale;
    %n = 0;
    while (%n < %numPts)
    {
        %windowCoord = VectorConvolve(%samplePts[%n], %ctrlExtent);
        %worldCoord1 = %guiTSCtrl.unproject(%windowCoord);
        %camVec = VectorSub(%worldCoord1, %cameraTransform);
        %camVec = VectorNormalize(%camVec);
        %camVec = VectorScale(%camVec, 5000);
        %worldCoord2 = VectorAdd(%worldCoord1, %camVec);
        %ret = %ret @ "\n" @ "//" @ " " @ %n @ " " @ "\"" @ %samplePts[%n] @ "\"  \"" @ %windowCoord @ "\"";
        %ret = %ret @ "\n" @ "//" @ " " @ %n @ " " @ "\"" @ %worldCoord1 @ "\" --> \"" @ %worldCoord2 @ "\"";
        %mask = $TypeMasks::WaterObjectType | $TypeMasks::InteriorObjectType;
        %hit = containerRayCast(%cameraTransform, %worldCoord2, %mask, %exempt, 1);
        %ret = %ret @ "\n" @ "//" @ " " @ %n @ " " @ "anyhit:   \"" @ getWords(%hit, 1, 3) @ "\"";
        %mask = $TypeMasks::WaterObjectType;
        %hit = containerRayCast(%cameraTransform, %worldCoord2, %mask, %exempt, 1);
        %ret = %ret @ "\n" @ "//" @ " " @ %n @ " " @ "waterhit: \"" @ getWords(%hit, 1, 3) @ "\"";
        %n = %n + 1;
    }
    return %ret;
}
function getScreenShotMetaDataOrtho(%guiTSCtrl)
{
    %cameraTransform = PlayGui.getLastCameraTransform();
    %numPts = 0;
    %sampleName[%numPts] = "upper left";
    %samplePts[%numPts] = "0 0";
    %numPts = %numPts + 1;
    %sampleName[%numPts] = "upper right";
    %samplePts[%numPts] = "1 0";
    %numPts = %numPts + 1;
    %sampleName[%numPts] = "lower left";
    %samplePts[%numPts] = "0 1";
    %numPts = %numPts + 1;
    %sampleName[%numPts] = "lower right";
    %samplePts[%numPts] = "1 1";
    %numPts = %numPts + 1;
    %sampleName[%numPts] = "center";
    %samplePts[%numPts] = "0.5 0.5";
    %numPts = %numPts + 1;
    %ctrlExtent = %guiTSCtrl.getExtent();
    %exempt = "";
    %windowCoord = VectorConvolve("0.5 0.5", %ctrlExtent);
    %worldCoord1 = %guiTSCtrl.unproject(%windowCoord);
    %centerCoord = %worldCoord1;
    %camVec = VectorSub(%worldCoord1, %cameraTransform);
    %camVec = VectorNormalize(%camVec);
    %ret = "";
    %ret = %ret @ "\n" @ "// %cameraTransform =" @ " " @ %cameraTransform;
    %ret = %ret @ "\n" @ "// %orthoScale      =" @ " " @ $pref::Render::orthoScale;
    %summary = "";
    %n = 0;
    while (%n < %numPts)
    {
        %windowCoord = %samplePts[%n];
        %windowCoord = VectorAdd(%windowCoord, "-0.5 -0.5");
        %windowCoord = VectorScale(%windowCoord, $pref::Render::orthoScale);
        %windowCoord = VectorAdd(%windowCoord, "0.5 0.5");
        %windowCoord = VectorConvolve(%windowCoord, %ctrlExtent);
        %worldCoord1 = %guiTSCtrl.unproject(%windowCoord);
        %worldCoord2 = VectorAdd(%worldCoord1, %camVec);
        %ret = %ret @ "\n" @ "//" @ " " @ %n @ " " @ "\"" @ %samplePts[%n] @ "\"  \"" @ %windowCoord @ "\"";
        %ret = %ret @ "\n" @ "//" @ " " @ %n @ " " @ "\"" @ %worldCoord1 @ "\" --> \"" @ %worldCoord2 @ "\"";
        %hit = intersectPlaneLine("0 0 0", "0 0 1", %worldCoord1, %worldCoord2);
        %ret = %ret @ "\n" @ "//" @ " " @ %n @ " " @ "XY plane: \"" @ %hit @ "\"";
        %summary = %summary @ "\n" @ formatString("%-15s:", %sampleName[%n]) @ " " @ %hit;
        %resultPts[%n] = %hit;
        if (isObject(moWorldCornerMarkers) && (%n < moWorldCornerMarkers.getCount()))
        {
            %mh = %hit;
            %mh = setWord(%mh, 2, 0);
            %marker = moWorldCornerMarkers.getObject(%n);
            %marker.setTransform(%mh);
            %marker.setScale("1 1 1");
        }
        %n = %n + 1;
    }
    %p1 = "0 0 0";
    %p2 = VectorAdd(%p1, %camVec);
    %pA = intersectPlaneLine("0 0 0", "0 0 1", %p1, %p2);
    %p1 = VectorAdd(%p1, "0 0 1");
    %p2 = VectorAdd(%p1, %camVec);
    %pB = intersectPlaneLine("0 0 0", "0 0 1", %p1, %p2);
    %pAB = VectorSub(%pB, %pA);
    %summary = %summary @ "\n" @ formatString("%-15s:", "altitudeUnit") @ " " @ %pAB;
    %command = "addSpace2DMap(\"" @ $gContiguousSpaceName @ "\", expandFilename(\"./orthomap\"), \"" @ $gContiguousSpaceName[%resultPts @ 0] @ "\", \"" @ $gContiguousSpaceName[%resultPts @ 0][%resultPts @ 1] @ "\", \"" @ $gContiguousSpaceName[%resultPts @ 0][%resultPts @ 1][%resultPts @ 2] @ "\", \"" @ %pAB @ "\");";
    return %command @ "\n" @ %summary @ "\n" @ %ret;
}
function doSaveScreenShotMetaData(%name, %ext, %guiCtrl)
{
    if ($pref::Render::orthoScale <= 1)
    {
        return;
    }
    %fn = %name @ ".cs";
    %file = new FileObject("");
    if (%file.openForWrite(%fn))
    {
        %file.writeLine(getScreenShotMetaData(%guiCtrl));
    }
    else
    {
        error(getScopeName() @ " " @ "- could not open file for write:" @ " " @ %fn);
    }
    %file.delete();
}
