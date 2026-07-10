function geLocalMapContainer::open(%this) {
    %this.setVisible(1);
    PlayGui.focusAndRaise(%this);
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
        geMapHud2DNotAvail.setVisible(1);
        geMapHud2DDragNZoom.setVisible(0);
        geMapHud2DCustomSpaceModeTitle.setVisible(0);
        geMapHud2DCustomSpaceModeText.setVisible(0);
        %this.close();
        return;
    }
    geMapHud2DDragNZoom.setVisible(1);
    geMapHud2DNotAvail.setVisible(0);
    geMapHud2DCustomSpaceModeTitle.setVisible(0);
    geMapHud2DCustomSpaceModeText.setVisible(0);
    geMapHud2DTheBitMap.setBitmap(%mapObj.mapFile);
    geMapHud2DTheBitMap.fitSize();
    %w = getWord(geMapHud2DTheBitMap.getExtent(), 0);
    %h = getWord(geMapHud2DTheBitMap.getExtent(), 1);
    %w = (0.4 * %w);
    %h = (0.4 * %h);
    geMapHud2DDragNZoom.resize(%w, %h);
    geMapHud2DDragNZoom.inspectPostApply();
    geMapHud2DTheBitMap.resize(%w, %h);
    geMapHud2DTheBitMap.reposition(0, 0);
    %mapObj.upperLeft = %mapObj.coordUpperLeft @ geMapHud2DTheOrthoMap;
    %mapObj.upperRight = %mapObj.coordUpperRight @ geMapHud2DTheOrthoMap;
    %mapObj.lowerLeft = %mapObj.coordLowerLeft @ geMapHud2DTheOrthoMap;
    %mapObj.unitAltitudeOffset = %mapObj.altitudeOffset @ geMapHud2DTheOrthoMap;
};
function geLocalMapContainer::setMap2DForCustomSpacesMode(%this, %title, %text) {
    if ((%text $= "")) {
        geMapHud2DCustomSpaceModeTitle.setVisible(0);
        geMapHud2DCustomSpaceModeText.setVisible(0);
        geMapHud2DDragNZoom.setVisible(0);
        geMapHud2DCustomSpaceModeTitle.setText("");
        geMapHud2DCustomSpaceModeText.setText("");
        geMapHud2DNotAvail.setVisible(1);
    }
    geMapHud2DDragNZoom.setVisible(0);
    geMapHud2DNotAvail.setVisible(0);
    geMapHud2DCustomSpaceModeTitle.setText(%title);
    geMapHud2DCustomSpaceModeTitle.setVisible(1);
    geMapHud2DCustomSpaceModeText.setText(%text);
    geMapHud2DCustomSpaceModeText.setVisible(1);
    waitAFrameAndCall("geLocalMapContainer_repositionTitleText");
};
function geLocalMapContainer_repositionTitleText() {
    %newTitleTop = (2.0 / (getWord(geMapHud2DCustomSpaceModeTitle.getExtent(), 1) - getWord(geMapHud2DCustomSpaceModeTitleContainer.getExtent(), 1)));
    if ((1.0 < %newTitleTop)) {
    }
    %newTitleTop = %newTitleTop;
    1;
    geMapHud2DCustomSpaceModeTitle.reposition(0, %newTitleTop);
};
$gDragNZoomIsReallySmooth = 1;
$gGeMapHud2DDragNZoomTimer = "";
$gGeMapHud2DDragNZoomRateAmountPerSecond = 1;
$gGeMapHud2DDragNZoomRateAmountPerOneShot = 1.4;
$gGeMapHud2DDragNZoomTickPeriodMS = 50;
function geLocalMapZoomOut::onMouseDown(%this) {
    if ($gDragNZoomIsReallySmooth) {
        geMapHud2DDragNZoom.zoomTick(0);
    }
};
function geLocalMapZoomOut::onMouseUp(%this) {
    if ($gDragNZoomIsReallySmooth) {
        if (!($gGeMapHud2DDragNZoomTimer $= "")) {
            cancel($gGeMapHud2DDragNZoomTimer);
            $gGeMapHud2DDragNZoomTimer = "";
        }
    }
    geMapHud2DDragNZoom.doScale(($gGeMapHud2DDragNZoomRateAmountPerOneShot / 1.0));
};
function geLocalMapZoomIn::onMouseDown(%this) {
    if ($gDragNZoomIsReallySmooth) {
        geMapHud2DDragNZoom.zoomTick(1);
    }
};
function geLocalMapZoomIn::onMouseUp(%this) {
    if ($gDragNZoomIsReallySmooth) {
        if (!($gGeMapHud2DDragNZoomTimer $= "")) {
            cancel($gGeMapHud2DDragNZoomTimer);
            $gGeMapHud2DDragNZoomTimer = "";
        }
    }
    geMapHud2DDragNZoom.doScale($gGeMapHud2DDragNZoomRateAmountPerOneShot);
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
    geMapHud2DDragNZoom.doScale(%amount);
    $gGeMapHud2DDragNZoomTimer = %this.schedule($gGeMapHud2DDragNZoomTickPeriodMS, "zoomTick", %isZoomIn);
};
function getSpace2DMap(%spaceName) {
    if ((%spaceName $= "")) {
        return "";
    }
    if (!(isObject(space2DMapsMap))) {
    }
    if ((0.0 < space2DMapsMap.findKey(%spaceName))) {
        echo(getScopeName() @ " " @ "- no 2D map: \"" @ %spaceName @ "\".");
        return "";
    }
    return space2DMapsMap.get(%spaceName);
};
$gGeLocalMapIcon_ME = 0;
function geMapHud2DTheOrthoMap::playerAdd(%this, %player) {
    %player.updateMapIcon();
};
function Player::updateMapIcon(%this) {
    %ctrl = gGetField(%this, "mapCtrl");
    if (!(isObject(%ctrl))) {
        %ctrl = new GuiBitmapCtrl("") {
            extent = 0 @ "32 32";
        };
        %ctrl.worldObject = %this;
        gSetField(%this, "mapCtrl", %ctrl);
        geMapHud2DTheOrthoMap.add(%ctrl);
        if (isObject($gGeLocalMapIcon_ME)) {
            geMapHud2DTheOrthoMap.pushToBack($gGeLocalMapIcon_ME);
        }
    }
    %bitmap = "";
    if (%this.getShowOnRadar()) {
        if (isObject($player)) {
            if (($player == %this)) {
            }
        }
    }
    if ($player.rolesPermissionCheckNoWarn("radarSeeAll")) {
        %gender = %this.getGender();
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
            geMapHud2DTheOrthoMap.pushToBack(%ctrl);
            geMapHud2DDragNZoom.setCenterOnCtrl(%ctrl);
            geMapHud2DTheOrthoMap.setReferenceObject($player);
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
