function formatImageNumber(%number) {
    if ((%number < 10.0)) {
        %number = 0 @ %number;
    }
    if ((%number < 100.0)) {
        %number = 0 @ %number;
    }
    if ((%number < 1000.0)) {
        %number = 0 @ %number;
    }
    if ((%number < 10000.0)) {
        %number = 0 @ %number;
    }
    return %number;
};
function formatSessionNumber(%number) {
    if ((%number < 10.0)) {
        %number = 0 @ %number;
    }
    if ((%number < 100.0)) {
        %number = 0 @ %number;
    }
    return %number;
};
function recordMovie(%movieName, %fps) {
    $timeAdvance = (1000.0 / %fps);
    $screenGrabThread = schedule($timeAdvance, 0, movieGrabScreen, %movieName, 0);
};
function movieGrabScreen(%movieName, %frameNumber) {
    ScreenShot(%movieName @ formatImageNumber(%frameNumber) @ ".png");
    $screenGrabThread = schedule($timeAdvance, 0, movieGrabScreen, %movieName, (%frameNumber + 1.0));
};
function stopMovie() {
    $timeAdvance = 0;
    cancel($screenGrabThread);
};
$screenshotNumber = 0;
function doScreenShot(%val) {
    if (!%val) {
        return;
    }
    %name = "screenshots/screen_" @ getTimeStamp();
    if (($Pref::Video::screenShotFormat $= "JPEG")) {
        %ext = ".jpg";
        %fmt = "JPEG";
    }
    if (($Pref::Video::screenShotFormat $= "PNG")) {
        %ext = ".png";
        %fmt = "PNG";
    }
    %ext = ".png";
    %fmt = "PNG";
    ScreenShot(%name @ %ext, %fmt);
    doSaveScreenShotMetaData(%name, %ext, PlayGui);
};
GlobalActionMap.bind(keyboard, "ctrl-alt s", doScreenShot);
