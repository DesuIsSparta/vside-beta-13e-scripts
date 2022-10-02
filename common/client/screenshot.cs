function formatImageNumber(%number)
{
    if (%number < 10)
    {
        %number = 0 @ %number;
    }
    if (%number < 100)
    {
        %number = 0 @ %number;
    }
    if (%number < 1000)
    {
        %number = 0 @ %number;
    }
    if (%number < 10000)
    {
        %number = 0 @ %number;
    }
    return %number;
}
function formatSessionNumber(%number)
{
    if (%number < 10)
    {
        %number = 0 @ %number;
    }
    if (%number < 100)
    {
        %number = 0 @ %number;
    }
    return %number;
}
function recordMovie(%movieName, %fps)
{
    $timeAdvance = 1000 / %fps;
    $screenGrabThread = schedule($timeAdvance, 0, movieGrabScreen, %movieName, 0);
    return ;
}
function movieGrabScreen(%movieName, %frameNumber)
{
    ScreenShot(%movieName @ formatImageNumber(%frameNumber) @ ".png");
    $screenGrabThread = schedule($timeAdvance, 0, movieGrabScreen, %movieName, %frameNumber + 1);
    return ;
}
function stopMovie()
{
    $timeAdvance = 0;
    cancel($screenGrabThread);
    return ;
}
$screenshotNumber = 0;
function doScreenShot(%val)
{
    if (!%val)
    {
        return ;
    }
    %name = "screenshots/screen_" @ getTimeStamp();
    if ($Pref::Video::screenShotFormat $= "JPEG")
    {
        %ext = ".jpg";
        %fmt = "JPEG";
    }
    else
    {
        if ($Pref::Video::screenShotFormat $= "PNG")
        {
            %ext = ".png";
            %fmt = "PNG";
        }
        else
        {
            %ext = ".png";
            %fmt = "PNG";
        }
    }
    ScreenShot(%name @ %ext, %fmt);
    doSaveScreenShotMetaData(%name, %ext, PlayGui);
    return ;
}
GlobalActionMap.bind(keyboard, "ctrl-alt s", doScreenShot);

