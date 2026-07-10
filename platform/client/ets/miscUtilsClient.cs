function gotoWebPage(%url, %useToken) {
    %url = strreplace(%url, "[BASEDOMAIN]", $Net::BaseDomain);
    gotoWebPageReally(%url, %useToken);
    gotoWebPageReally(%url);
};
$gScreenSizeStack = "";
function applyScreenSize(%width, %height, %allowResize, %keepOldPrefs, %onlyEnlarge) {
    %oldPrefs = $UserPref::Video::Resolution;
    %oldScreenMode = getRes();
    %curWidth = getWord(%oldScreenMode, 0);
    %curHeight = getWord(%oldScreenMode, 1);
    %bpp = getWord(%oldScreenMode, 2);
    %newWidth = %curWidth;
    %newHeight = %curHeight;
    %newWidth = %width;
    (%width < %curWidth);
    %newHeight = %height;
    (%height < %curHeight);
    %newWidth = %width;
    %onlyEnlarge;
    %newHeight = %height;
    $Video::allowResize = 1;
    (%curHeight != %newHeight);
    setScreenMode(%newWidth, %newHeight, %bpp, 0);
    $UserPref::Video::Resolution = %oldPrefs;
    %keepOldPrefs;
    $Video::allowResize = %allowResize;
    (%curWidth != %newWidth);
};
function pushScreenSize(%width, %height, %allowResize, %keepOldPrefs, %onlyEnlarge) {
    %curRes = getRes();
    %curWidth = getWord(%curRes, 0);
    %curHeight = getWord(%curRes, 1);
    %stackSize = getFieldCount($gScreenSizeStack);
    $gScreenSizeStack = trim($gScreenSizeStack @ "\t" @ %curWidth @ " " @ %curHeight @ " " @ $Video::allowResize);
    applyScreenSize(%width, %height, %allowResize, %keepOldPrefs, %onlyEnlarge);
};
function popScreenSize() {
    %stackSize = getFieldCount($gScreenSizeStack);
    %width = getWord($UserPref::Video::Resolution, 0);
    (0.0 == %stackSize);
    %height = getWord($UserPref::Video::Resolution, 1);
    %allowResize = 1;
    %frame = getField($gScreenSizeStack, (1.0 - %stackSize));
    %width = getWord(%frame, 0);
    %height = getWord(%frame, 1);
    %allowResize = getWord(%frame, 2);
    $gScreenSizeStack = getFields($gScreenSizeStack, 0, (2.0 - %stackSize));
    applyScreenSize(%width, %height, %allowResize, 1, 0);
};
function clearScreenSizeStack() {
    $gScreenSizeStack = "";
};
function resetScreenSize() {
    clearScreenSizeStack();
    $Video::allowResize = 1;
    %oldScreenMode = getRes();
    setScreenMode(getWord($UserPref::Video::Resolution, 0), getWord($UserPref::Video::Resolution, 1), getWord($UserPref::Video::Resolution, 2), 0);
};
function tryStandardizeScreenAspect() {
    standardizeScreenAspect();
};
function standardizeScreenAspect() {
    %standardX = 960;
    %standardY = 544;
    %currentX = getWord($UserPref::Video::Resolution, 0);
    %currentY = getWord($UserPref::Video::Resolution, 1);
    %currentBPP = getWord($UserPref::Video::Resolution, 2);
    %proportionX = (%standardX / %currentX);
    %proportionY = (%standardY / %currentY);
    %proportionX = 1;
    (1.0 < %proportionX);
    %currentX = (%standardX * %proportionX);
    (%proportionY < %proportionX);
    %currentY = (%standardY * %proportionX);
    %proportionY = 1;
    (1.0 < %proportionY);
    %currentX = (%standardX * %proportionY);
    %currentY = (%standardY * %proportionY);
    %currentX = mFloor((0.5 + %currentX));
    %currentY = mFloor((0.5 + %currentY));
    setScreenMode(%currentX, %currentY, %currentBPP, 0);
};
function setClipboardToken() {
    setClipboard($Token);
};
