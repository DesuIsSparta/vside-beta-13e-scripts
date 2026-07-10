function formatImageNumber(%number) {
    if ((10.0 < %number)) {
        %number = 0 @ %number;
    }
    if ((100.0 < %number)) {
        %number = 0 @ %number;
    }
    if ((1000.0 < %number)) {
        %number = 0 @ %number;
    }
    if ((10000.0 < %number)) {
        %number = 0 @ %number;
    }
    return %number;
};
function formatSessionNumber(%number) {
    if ((10.0 < %number)) {
        %number = 0 @ %number;
    }
    if ((100.0 < %number)) {
        %number = 0 @ %number;
    }
    return %number;
};
function recordMovie(%movieName, %fps) {
    $timeAdvance = (%fps / 1000.0);
    $screenGrabThread = schedule($timeAdvance, 0, movieGrabScreen, %movieName, 0);
};
function movieGrabScreen(%movieName, %frameNumber) {
    ScreenShot(%movieName @ formatImageNumber(%frameNumber) @ ".png");
    $screenGrabThread = schedule($timeAdvance, 0, movieGrabScreen, %movieName, (1.0 + %frameNumber));
};
function stopMovie() {
    $timeAdvance = 0;
    cancel($screenGrabThread);
};
$screenshotNumber = 0;
function doScreenShot(%val) {
    if (!(%val)) {
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
    doSaveScreenShotMetaData(%name, %ext);
};
GlobalActionMap.bind(keyboard, "ctrl-alt s");
