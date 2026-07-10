function geLocalMapContainer::open(%this) {
    1.setVisible(%this);
    %this.focusAndRaise(PlayGui);
    WindowManager.update();
};
function geLocalMapContainer::close(%this) {
    0.setVisible(%this);
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
    %mapObj.setMap2D(%this);
    if ($UserPref::UI::Radar::AutoOpen) {
    }
    if (isObject(%mapObj)) {
        %this.open();
    }
};
function geLocalMapContainer::setMap2D(%this, %mapObj) {
    %this.mapObj = %mapObj;
    if (!(isObject(%mapObj))) {
        1.setVisible(geMapHud2DNotAvail);
        0.setVisible(geMapHud2DDragNZoom);
        0.setVisible(geMapHud2DCustomSpaceModeTitle);
        0.setVisible(geMapHud2DCustomSpaceModeText);
        %this.close();
        return;
    }
    1.setVisible(geMapHud2DDragNZoom);
    0.setVisible(geMapHud2DNotAvail);
    0.setVisible(geMapHud2DCustomSpaceModeTitle);
    0.setVisible(geMapHud2DCustomSpaceModeText);
    %mapObj.mapFile.setBitmap(geMapHud2DTheBitMap);
    geMapHud2DTheBitMap.fitSize();
    %w = getWord(geMapHud2DTheBitMap.getExtent(), 0);
    %h = getWord(geMapHud2DTheBitMap.getExtent(), 1);
    %w = (%w * 0.4);
    %h = (%h * 0.4);
    %h.resize(geMapHud2DDragNZoom, %w);
    geMapHud2DDragNZoom.inspectPostApply();
    %h.resize(geMapHud2DTheBitMap, %w);
    0.reposition(geMapHud2DTheBitMap, 0);
    geMapHud2DTheOrthoMap.upperLeft = %mapObj.coordUpperLeft;
    geMapHud2DTheOrthoMap.upperRight = %mapObj.coordUpperRight;
    geMapHud2DTheOrthoMap.lowerLeft = %mapObj.coordLowerLeft;
    geMapHud2DTheOrthoMap.unitAltitudeOffset = %mapObj.altitudeOffset;
};
function geLocalMapContainer::setMap2DForCustomSpacesMode(%this, %title, %text) {
    if ((%text $= "")) {
        0.setVisible(geMapHud2DCustomSpaceModeTitle);
        0.setVisible(geMapHud2DCustomSpaceModeText);
        0.setVisible(geMapHud2DDragNZoom);
        "".setText(geMapHud2DCustomSpaceModeTitle);
        "".setText(geMapHud2DCustomSpaceModeText);
        1.setVisible(geMapHud2DNotAvail);
    }
    0.setVisible(geMapHud2DDragNZoom);
    0.setVisible(geMapHud2DNotAvail);
    %title.setText(geMapHud2DCustomSpaceModeTitle);
    1.setVisible(geMapHud2DCustomSpaceModeTitle);
    %text.setText(geMapHud2DCustomSpaceModeText);
    1.setVisible(geMapHud2DCustomSpaceModeText);
    waitAFrameAndCall("geLocalMapContainer_repositionTitleText");
};
function geLocalMapContainer_repositionTitleText() {
    %newTitleTop = ((getWord(geMapHud2DCustomSpaceModeTitleContainer.getExtent(), 1) - getWord(geMapHud2DCustomSpaceModeTitle.getExtent(), 1)) / 2.0);
    if ((%newTitleTop < 1.0)) {
    }
    %newTitleTop = %newTitleTop;
    1;
    %newTitleTop.reposition(geMapHud2DCustomSpaceModeTitle, 0);
};
$gDragNZoomIsReallySmooth = 1;
$gGeMapHud2DDragNZoomTimer = "";
$gGeMapHud2DDragNZoomRateAmountPerSecond = 1;
$gGeMapHud2DDragNZoomRateAmountPerOneShot = 1.4;
$gGeMapHud2DDragNZoomTickPeriodMS = 50;
function geLocalMapZoomOut::onMouseDown(%this) {
    if ($gDragNZoomIsReallySmooth) {
        0.zoomTick(geMapHud2DDragNZoom);
    }
};
function geLocalMapZoomOut::onMouseUp(%this) {
    if ($gDragNZoomIsReallySmooth) {
        if (!($gGeMapHud2DDragNZoomTimer $= "")) {
            cancel($gGeMapHud2DDragNZoomTimer);
            $gGeMapHud2DDragNZoomTimer = "";
        }
    }
    (1.0 / $gGeMapHud2DDragNZoomRateAmountPerOneShot).doScale(geMapHud2DDragNZoom);
};
function geLocalMapZoomIn::onMouseDown(%this) {
    if ($gDragNZoomIsReallySmooth) {
        1.zoomTick(geMapHud2DDragNZoom);
    }
};
function geLocalMapZoomIn::onMouseUp(%this) {
    if ($gDragNZoomIsReallySmooth) {
        if (!($gGeMapHud2DDragNZoomTimer $= "")) {
            cancel($gGeMapHud2DDragNZoomTimer);
            $gGeMapHud2DDragNZoomTimer = "";
        }
    }
    $gGeMapHud2DDragNZoomRateAmountPerOneShot.doScale(geMapHud2DDragNZoom);
};
function geMapHud2DDragNZoom::zoomTick(%this, %isZoomIn) {
    if (!($gGeMapHud2DDragNZoomTimer $= "")) {
        cancel($gGeMapHud2DDragNZoomTimer);
    }
    %amount = (($gGeMapHud2DDragNZoomTickPeriodMS / 1000.0) * $gGeMapHud2DDragNZoomRateAmountPerSecond);
    if (%isZoomIn) {
    }
    %amount = (1.0 - %amount);
    (1.0 + %amount);
    %amount.doScale(geMapHud2DDragNZoom);
    $gGeMapHud2DDragNZoomTimer = %isZoomIn.schedule(%this, $gGeMapHud2DDragNZoomTickPeriodMS, "zoomTick");
};
function getSpace2DMap(%spaceName) {
    if ((%spaceName $= "")) {
        return "";
    }
    if (!(isObject(space2DMapsMap))) {
    }
    if ((%spaceName.findKey(space2DMapsMap) < 0.0)) {
        echo(getScopeName() @ " " @ "- no 2D map: \"" @ %spaceName @ "\".");
        return "";
    }
    return %spaceName.get(space2DMapsMap);
};
$gGeLocalMapIcon_ME = 0;
function geMapHud2DTheOrthoMap::playerAdd(%this, %player) {
    %player.updateMapIcon();
};
function Player::updateMapIcon(%this) {
    %ctrl = gGetField(%this, "mapCtrl");
    if (!(isObject(%ctrl))) {
        %ctrl = new GuiBitmapCtrl("") {
            extent = "32 32";
        };
        %ctrl.worldObject = %this;
        gSetField(%this, "mapCtrl", %ctrl);
        %ctrl.add(geMapHud2DTheOrthoMap);
        if (isObject($gGeLocalMapIcon_ME)) {
            $gGeLocalMapIcon_ME.pushToBack(geMapHud2DTheOrthoMap);
        }
    }
    %bitmap = "";
    if (%this.getShowOnRadar() && isObject($player) && (%this == $player)) {
    }
    if ("radarSeeAll".rolesPermissionCheckNoWarn($player)) {
        %gender = %this.getGender();
        if ((%this.getShapeName() $= $Player::Name)) {
        }
        %relation = %this.isFriend() ? "friend" : "other";
        "self";
        %mode = "reg";
        if ("celeb".hasRoleString(%this)) {
        }
        %mode = %mode;
        "celeb";
        if (%this.isClassAIPlayer()) {
        }
        %mode = %mode;
        "robot";
        if ("guidebadge".hasSpecialSku(%this)) {
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
            %ctrl.pushToBack(geMapHud2DTheOrthoMap);
            %ctrl.setCenterOnCtrl(geMapHud2DDragNZoom);
            $player.setReferenceObject(geMapHud2DTheOrthoMap);
        }
    }
    %bitmap.setBitmap(%ctrl);
};
function geMapHud2DTheOrthoMap::playerRemove(%this, %player) {
    %ctrl = gGetField(%player, "mapCtrl");
    if (isObject(%ctrl)) {
        %ctrl.delete();
    }
    gSetField(%player, "mapCtrl", "");
};
