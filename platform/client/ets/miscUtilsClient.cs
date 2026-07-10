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
        if ((%width < %curWidth)) {
            %newWidth = %width;
        }
        if ((%height < %curHeight)) {
            %newHeight = %height;
        }
    }
    %newWidth = %width;
    %newHeight = %height;
    if ((%curWidth != %newWidth)) {
    }
    if ((%curHeight != %newHeight)) {
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
    if ((0.0 == %stackSize)) {
        %width = getWord($UserPref::Video::Resolution, 0);
        %height = getWord($UserPref::Video::Resolution, 1);
        %allowResize = 1;
    }
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
    %proportionX = (%standardX / %currentX);
    %proportionY = (%standardY / %currentY);
    if ((%proportionY < %proportionX)) {
        if ((1.0 < %proportionX)) {
            %proportionX = 1;
        }
        %currentX = (%standardX * %proportionX);
        %currentY = (%standardY * %proportionX);
    }
    if ((1.0 < %proportionY)) {
        %proportionY = 1;
    }
    %currentX = (%standardX * %proportionY);
    %currentY = (%standardY * %proportionY);
    %currentX = mFloor((0.5 + %currentX));
    %currentY = mFloor((0.5 + %currentY));
    setScreenMode(%currentX, %currentY, %currentBPP, 0);
};
function setClipboardToken() {
    setClipboard($Token);
};
