function geLocalMapContainer::open(%this) {
    %this.setVisible(1);
    %this.focusAndRaise();
    WindowManager.update();
};
function geLocalMapContainer::close(%this) {
    %this.setVisible(0);
    PlayGui.focusTopWindow();
    WindowManager.update();
    return 1;
};
function geLocalMapContainer::onSpaceChange(%this, %spaceName) {
    if ((%this.spaceName $= %spaceName)) {
        return;
    }
    %this.spaceName = %spaceName;
    %mapObj = getSpace2DMap(%spaceName);
    %this.setMap2D(%mapObj);
    if ($UserPref::UI::Radar::AutoOpen) {
    }
    if (isObject(%mapObj)) {
        %this.open();
    }
};
function geLocalMapContainer::setMap2D(%this, %mapObj) {
    %this.mapObj = %mapObj;
    if (!(isObject(%mapObj))) {
        1.setVisible();
        0.setVisible();
        0.setVisible();
        0.setVisible();
        %this.close();
        return geMapHud2DCustomSpaceModeText;
    }
    1.setVisible();
    0.setVisible();
    0.setVisible();
    0.setVisible();
    %mapObj.mapFile.setBitmap();
    geMapHud2DTheBitMap.fitSize();
    %w = getWord(geMapHud2DTheBitMap.getExtent(), 0);
    geMapHud2DTheBitMap;
    %h = getWord(geMapHud2DTheBitMap.getExtent(), 1);
    geMapHud2DCustomSpaceModeText;
    %w = (0.4 * %w);
    geMapHud2DCustomSpaceModeTitle;
    %h = (0.4 * %h);
    geMapHud2DNotAvail;
    %w.resize(%h);
    geMapHud2DDragNZoom.inspectPostApply();
    %w.resize(%h);
    0.reposition(0);
    %mapObj.upperLeft = %mapObj.coordUpperLeft @ geMapHud2DTheOrthoMap;
    geMapHud2DTheBitMap;
    %mapObj.upperRight = %mapObj.coordUpperRight @ geMapHud2DTheOrthoMap;
    geMapHud2DTheBitMap;
    %mapObj.lowerLeft = %mapObj.coordLowerLeft @ geMapHud2DTheOrthoMap;
    geMapHud2DDragNZoom;
    %mapObj.unitAltitudeOffset = %mapObj.altitudeOffset @ geMapHud2DTheOrthoMap;
    geMapHud2DDragNZoom;
};
function geLocalMapContainer::setMap2DForCustomSpacesMode(%this, %title, %text) {
    if ((%text $= "")) {
        0.setVisible();
        0.setVisible();
        0.setVisible();
        "".setText();
        "".setText();
        1.setVisible();
    }
    0.setVisible();
    0.setVisible();
    %title.setText();
    1.setVisible();
    %text.setText();
    1.setVisible();
    waitAFrameAndCall("geLocalMapContainer_repositionTitleText");
};
function geLocalMapContainer_repositionTitleText() {
    %newTitleTop = (2.0 / (getWord(geMapHud2DCustomSpaceModeTitle.getExtent(), 1) - getWord(geMapHud2DCustomSpaceModeTitleContainer.getExtent(), 1)));
    if ((1.0 < %newTitleTop)) {
    }
    %newTitleTop = %newTitleTop;
    1;
    0.reposition(%newTitleTop);
};
$gDragNZoomIsReallySmooth = 1;
$gGeMapHud2DDragNZoomTimer = "";
$gGeMapHud2DDragNZoomRateAmountPerSecond = 1;
$gGeMapHud2DDragNZoomRateAmountPerOneShot = 1.4;
$gGeMapHud2DDragNZoomTickPeriodMS = 50;
function geLocalMapZoomOut::onMouseDown(%this) {
    if ($gDragNZoomIsReallySmooth) {
        0.zoomTick();
    }
};
function geLocalMapZoomOut::onMouseUp(%this) {
    if ($gDragNZoomIsReallySmooth) {
        if (!($gGeMapHud2DDragNZoomTimer $= "")) {
            cancel($gGeMapHud2DDragNZoomTimer);
            $gGeMapHud2DDragNZoomTimer = "";
        }
    }
    ($gGeMapHud2DDragNZoomRateAmountPerOneShot / 1.0).doScale();
};
function geLocalMapZoomIn::onMouseDown(%this) {
    if ($gDragNZoomIsReallySmooth) {
        1.zoomTick();
    }
};
function geLocalMapZoomIn::onMouseUp(%this) {
    if ($gDragNZoomIsReallySmooth) {
        if (!($gGeMapHud2DDragNZoomTimer $= "")) {
            cancel($gGeMapHud2DDragNZoomTimer);
            $gGeMapHud2DDragNZoomTimer = "";
        }
    }
    $gGeMapHud2DDragNZoomRateAmountPerOneShot.doScale();
};
function geMapHud2DDragNZoom::zoomTick(%this, %isZoomIn) {
    if (!($gGeMapHud2DDragNZoomTimer $= "")) {
        cancel($gGeMapHud2DDragNZoomTimer);
    }
    %amount = ($gGeMapHud2DDragNZoomRateAmountPerSecond * (1000.0 / $gGeMapHud2DDragNZoomTickPeriodMS));
    if (%isZoomIn) {
    }
    %amount = (%amount - 1.0);
    (%amount + 1.0);
    %amount.doScale();
    $gGeMapHud2DDragNZoomTimer = %this.schedule($gGeMapHud2DDragNZoomTickPeriodMS, "zoomTick", %isZoomIn);
    geMapHud2DDragNZoom;
};
function getSpace2DMap(%spaceName) {
    if ((%spaceName $= "")) {
        return "";
    }
    if (!(isObject(space2DMapsMap))) {
    }
    if ((space2DMapsMap < %spaceName.findKey())) {
        echo(getScopeName() @ " " @ "- no 2D map: \"" @ %spaceName @ "\".");
        return "";
    }
    return %spaceName.get();
};
$gGeLocalMapIcon_ME = 0;
function geMapHud2DTheOrthoMap::playerAdd(%this, %player) {
    %player.updateMapIcon();
};
function Player::updateMapIcon(%this) {
    %ctrl = gGetField(%this, "mapCtrl");
    if (!(isObject(%ctrl))) {
        0;
        %ctrl = new ""() {
            extent = GuiBitmapCtrl @ "32 32";
        };
        %ctrl.worldObject = %this;
        gSetField(%this, "mapCtrl", %ctrl);
        %ctrl.add();
        if (isObject($gGeLocalMapIcon_ME)) {
            $gGeLocalMapIcon_ME.pushToBack();
        }
    }
    %bitmap = "";
    geMapHud2DTheOrthoMap;
    if (%this.getShowOnRadar()) {
        if (isObject($player)) {
            if (($player == %this)) {
            }
        }
    }
    if ($player.rolesPermissionCheckNoWarn("radarSeeAll")) {
        %gender = %this.getGender();
        geMapHud2DTheOrthoMap;
        if ((%this.getShapeName() $= $Player::Name)) {
        }
        %relation = %this.isFriend() ? "friend" : "other";
        "self";
        %mode = "reg";
        if (%this.hasRoleString("celeb")) {
        }
        %mode = %mode;
        "celeb";
        if (%this.isClassAIPlayer()) {
        }
        %mode = %mode;
        "robot";
        if (%this.hasSpecialSku("guidebadge")) {
        }
        %mode = %mode;
        "guide";
        if (!(%this.getShowOnRadar())) {
        }
        %mode = %mode;
        "hidden";
        %bitmap = "platform/client/ui/mapicons/";
        %bitmap = %bitmap @ %gender @ "_";
        %bitmap = %bitmap @ %relation @ "_";
        %bitmap = %bitmap @ %mode;
        if ((%relation $= "self")) {
            $gGeLocalMapIcon_ME = %ctrl;
            %ctrl.pushToBack();
            %ctrl.setCenterOnCtrl();
            $player.setReferenceObject();
        }
    }
    %ctrl.setBitmap(%bitmap);
};
function geMapHud2DTheOrthoMap::playerRemove(%this, %player) {
    %ctrl = gGetField(%player, "mapCtrl");
    if (isObject(%ctrl)) {
        %ctrl.delete();
    }
    gSetField(%player, "mapCtrl", "");
};
