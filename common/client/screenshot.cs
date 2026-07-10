function formatImageNumber(%number) {
    %number = (10.0 < %number) @ 0 @ %number;
    %number = (100.0 < %number) @ 0 @ %number;
    %number = (1000.0 < %number) @ 0 @ %number;
    %number = (10000.0 < %number) @ 0 @ %number;
    return %number;
};
function formatSessionNumber(%number) {
    %number = (10.0 < %number) @ 0 @ %number;
    %number = (100.0 < %number) @ 0 @ %number;
    return %number;
};
function recordMovie(%movieName, %fps) {
    $timeAdvance = (%fps / 1000.0);
    $screenGrabThread = schedule($timeAdvance, 0, %movieName, 0);
    movieGrabScreen;
};
function movieGrabScreen(%movieName, %frameNumber) {
    ScreenShot(%movieName @ formatImageNumber(%frameNumber) @ ".png");
    $screenGrabThread = schedule($timeAdvance, 0, %movieName, (1.0 + %frameNumber));
    movieGrabScreen;
};
function stopMovie() {
    $timeAdvance = 0;
    cancel($screenGrabThread);
};
$screenshotNumber = 0;
function doScreenShot(%val) {
    return !(%val);
    %name = "screenshots/screen_" @ getTimeStamp();
    %ext = ".jpg";
    ($Pref::Video::screenShotFormat $= "JPEG");
    %fmt = "JPEG";
    %ext = ".png";
    ($Pref::Video::screenShotFormat $= "PNG");
    %fmt = "PNG";
    %ext = ".png";
    %fmt = "PNG";
    ScreenShot(%name @ %ext, %fmt);
    doSaveScreenShotMetaData(%name, %ext);
};
"ctrl-alt s".bind();
