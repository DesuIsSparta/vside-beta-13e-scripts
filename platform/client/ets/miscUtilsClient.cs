function gotoWebPage(%url, %useToken) {
    %url = strreplace(%url, "[BASEDOMAIN]", $Net::BaseDomain);
    if (isDefined("%useToken")) {
        gotoWebPageReally(%url, %useToken);
    }
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
    if (%onlyEnlarge) {
        if ((%curWidth < %width)) {
            %newWidth = %width;
        }
        if ((%curHeight < %height)) {
            %newHeight = %height;
        }
    }
    %newWidth = %width;
    %newHeight = %height;
    if ((%newWidth != %curWidth) || (%newHeight != %curHeight)) {
        $Video::allowResize = 1;
        setScreenMode(%newWidth, %newHeight, %bpp, 0);
    }
    if (%keepOldPrefs) {
        $UserPref::Video::Resolution = %oldPrefs;
    }
    $Video::allowResize = %allowResize;
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
    if ((%stackSize == 0.0)) {
        %width = getWord($UserPref::Video::Resolution, 0);
        %height = getWord($UserPref::Video::Resolution, 1);
        %allowResize = 1;
    }
    %frame = getField($gScreenSizeStack, (%stackSize - 1.0));
    %width = getWord(%frame, 0);
    %height = getWord(%frame, 1);
    %allowResize = getWord(%frame, 2);
    $gScreenSizeStack = getFields($gScreenSizeStack, 0, (%stackSize - 2.0));
    applyScreenSize(%width, %height, %allowResize, 1, 0);
};
function clearScreenSizeStack() {
    $gScreenSizeStack = "";
};
function resetScreenSize() {
    clearScreenSizeStack();
    $Video::allowResize = 1;
    %oldScreenMode = getRes();
    if (!(%oldScreenMode $= $UserPref::Video::Resolution)) {
        setScreenMode(getWord($UserPref::Video::Resolution, 0), getWord($UserPref::Video::Resolution, 1), getWord($UserPref::Video::Resolution, 2), 0);
    }
};
function tryStandardizeScreenAspect() {
    if ($UserPref::Video::ConstrainWindowDimensions) {
        standardizeScreenAspect();
    }
};
function standardizeScreenAspect() {
    %standardX = 960;
    %standardY = 544;
    %currentX = getWord($UserPref::Video::Resolution, 0);
    %currentY = getWord($UserPref::Video::Resolution, 1);
    %currentBPP = getWord($UserPref::Video::Resolution, 2);
    %proportionX = (%currentX / %standardX);
    %proportionY = (%currentY / %standardY);
    if ((%proportionX < %proportionY)) {
        if ((%proportionX < 1.0)) {
            %proportionX = 1;
        }
        %currentX = (%proportionX * %standardX);
        %currentY = (%proportionX * %standardY);
    }
    if ((%proportionY < 1.0)) {
        %proportionY = 1;
    }
    %currentX = (%proportionY * %standardX);
    %currentY = (%proportionY * %standardY);
    %currentX = mFloor((%currentX + 0.5));
    %currentY = mFloor((%currentY + 0.5));
    setScreenMode(%currentX, %currentY, %currentBPP, 0);
};
function setClipboardToken() {
    setClipboard($Token);
};
