function geLocalMapContainer::open(%this)
{
    %this.setVisible(1);
    PlayGui.focusAndRaise(%this);
    WindowManager.update();
    return ;
}
function geLocalMapContainer::close(%this)
{
    %this.setVisible(0);
    PlayGui.focusTopWindow();
    WindowManager.update();
    return 1;
}
function geLocalMapContainer::onSpaceChange(%this, %spaceName)
{
    if (%this.spaceName $= %spaceName)
    {
        return ;
    }
    %this.spaceName = %spaceName;
    %mapObj = getSpace2DMap(%spaceName);
    %this.setMap2D(%mapObj);
    if ($UserPref::UI::Radar::AutoOpen && isObject(%mapObj))
    {
        %this.open();
    }
    return ;
}
function geLocalMapContainer::setMap2D(%this, %mapObj)
{
    %this.mapObj = %mapObj;
    if (!isObject(%mapObj))
    {
        geMapHud2DNotAvail.setVisible(1);
        geMapHud2DDragNZoom.setVisible(0);
        geMapHud2DCustomSpaceModeTitle.setVisible(0);
        geMapHud2DCustomSpaceModeText.setVisible(0);
        %this.close();
        return ;
    }
    geMapHud2DDragNZoom.setVisible(1);
    geMapHud2DNotAvail.setVisible(0);
    geMapHud2DCustomSpaceModeTitle.setVisible(0);
    geMapHud2DCustomSpaceModeText.setVisible(0);
    geMapHud2DTheBitMap.setBitmap(%mapObj.mapFile);
    geMapHud2DTheBitMap.fitSize();
    %w = getWord(geMapHud2DTheBitMap.getExtent(), 0);
    %h = getWord(geMapHud2DTheBitMap.getExtent(), 1);
    %w = %w * 0.4;
    %h = %h * 0.4;
    geMapHud2DDragNZoom.resize(%w, %h);
    geMapHud2DDragNZoom.inspectPostApply();
    geMapHud2DTheBitMap.resize(%w, %h);
    geMapHud2DTheBitMap.reposition(0, 0);
    geMapHud2DTheOrthoMap.upperLeft = %mapObj.coordUpperLeft;
    geMapHud2DTheOrthoMap.upperRight = %mapObj.coordUpperRight;
    geMapHud2DTheOrthoMap.lowerLeft = %mapObj.coordLowerLeft;
    geMapHud2DTheOrthoMap.unitAltitudeOffset = %mapObj.altitudeOffset;
    return ;
}
function geLocalMapContainer::setMap2DForCustomSpacesMode(%this, %title, %text)
{
    if (%text $= "")
    {
        geMapHud2DCustomSpaceModeTitle.setVisible(0);
        geMapHud2DCustomSpaceModeText.setVisible(0);
        geMapHud2DDragNZoom.setVisible(0);
        geMapHud2DCustomSpaceModeTitle.setText("");
        geMapHud2DCustomSpaceModeText.setText("");
        geMapHud2DNotAvail.setVisible(1);
    }
    else
    {
        geMapHud2DDragNZoom.setVisible(0);
        geMapHud2DNotAvail.setVisible(0);
        geMapHud2DCustomSpaceModeTitle.setText(%title);
        geMapHud2DCustomSpaceModeTitle.setVisible(1);
        geMapHud2DCustomSpaceModeText.setText(%text);
        geMapHud2DCustomSpaceModeText.setVisible(1);
        waitAFrameAndCall("geLocalMapContainer_repositionTitleText");
    }
    return ;
}
function geLocalMapContainer_repositionTitleText()
{
    %newTitleTop = (getWord(geMapHud2DCustomSpaceModeTitleContainer.getExtent(), 1) - getWord(geMapHud2DCustomSpaceModeTitle.getExtent(), 1)) / 2;
    %newTitleTop = %newTitleTop < 1 ? 1 : %newTitleTop;
    geMapHud2DCustomSpaceModeTitle.reposition(0, %newTitleTop);
    return ;
}
$gDragNZoomIsReallySmooth = 1;
$gGeMapHud2DDragNZoomTimer = "";
$gGeMapHud2DDragNZoomRateAmountPerSecond = 1;
$gGeMapHud2DDragNZoomRateAmountPerOneShot = 1.4;
$gGeMapHud2DDragNZoomTickPeriodMS = 50;
function geLocalMapZoomOut::onMouseDown(%this)
{
    if ($gDragNZoomIsReallySmooth)
    {
        geMapHud2DDragNZoom.zoomTick(0);
    }
    return ;
}
function geLocalMapZoomOut::onMouseUp(%this)
{
    if ($gDragNZoomIsReallySmooth)
    {
        if (!($gGeMapHud2DDragNZoomTimer $= ""))
        {
            cancel($gGeMapHud2DDragNZoomTimer);
            $gGeMapHud2DDragNZoomTimer = "";
        }
    }
    else
    {
        geMapHud2DDragNZoom.doScale(1 / $gGeMapHud2DDragNZoomRateAmountPerOneShot);
    }
    return ;
}
function geLocalMapZoomIn::onMouseDown(%this)
{
    if ($gDragNZoomIsReallySmooth)
    {
        geMapHud2DDragNZoom.zoomTick(1);
    }
    return ;
}
function geLocalMapZoomIn::onMouseUp(%this)
{
    if ($gDragNZoomIsReallySmooth)
    {
        if (!($gGeMapHud2DDragNZoomTimer $= ""))
        {
            cancel($gGeMapHud2DDragNZoomTimer);
            $gGeMapHud2DDragNZoomTimer = "";
        }
    }
    else
    {
        geMapHud2DDragNZoom.doScale($gGeMapHud2DDragNZoomRateAmountPerOneShot);
    }
    return ;
}
function geMapHud2DDragNZoom::zoomTick(%this, %isZoomIn)
{
    if (!($gGeMapHud2DDragNZoomTimer $= ""))
    {
        cancel($gGeMapHud2DDragNZoomTimer);
    }
    %amount = ($gGeMapHud2DDragNZoomTickPeriodMS / 1000) * $gGeMapHud2DDragNZoomRateAmountPerSecond;
    %amount = %isZoomIn ? %amount : %amount;
    geMapHud2DDragNZoom.doScale(%amount);
    $gGeMapHud2DDragNZoomTimer = %this.schedule($gGeMapHud2DDragNZoomTickPeriodMS, "zoomTick", %isZoomIn);
    return ;
}
function getSpace2DMap(%spaceName)
{
    if (%spaceName $= "")
    {
        return "";
    }
    if (!isObject(space2DMapsMap) && (space2DMapsMap.findKey(%spaceName) < 0))
    {
        echo(getScopeName() SPC "- no 2D map: \"" @ %spaceName @ "\".");
        return "";
    }
    return space2DMapsMap.get(%spaceName);
}
$gGeLocalMapIcon_ME = 0;
function geMapHud2DTheOrthoMap::playerAdd(%this, %player)
{
    %player.updateMapIcon();
    return ;
}
function Player::updateMapIcon(%this)
{
    %ctrl = gGetField(%this, "mapCtrl");
    if (!isObject(%ctrl))
    {
        %ctrl = new GuiBitmapCtrl();
        %ctrl.worldObject = %this;
        gSetField(%this, "mapCtrl", %ctrl);
        geMapHud2DTheOrthoMap.add(%ctrl);
        if (isObject($gGeLocalMapIcon_ME))
        {
            geMapHud2DTheOrthoMap.pushToBack($gGeLocalMapIcon_ME);
        }
    }
    %bitmap = "";
    if (((%this.getShowOnRadar() || isObject($player)) && (%this == $player)) || $player.rolesPermissionCheckNoWarn("radarSeeAll"))
    {
        %gender = %this.getGender();
        %relation = %this.getShapeName() $= $Player::Name ? "self" : %this.isFriend();
        %mode = "reg";
        %mode = %this.hasRoleString("celeb") ? "celeb" : %mode;
        %mode = %this.isClassAIPlayer() ? "robot" : %mode;
        %mode = %this.hasSpecialSku("guidebadge") ? "guide" : %mode;
        %mode = !%this.getShowOnRadar() ? "hidden" : %mode;
        %bitmap = "platform/client/ui/mapicons/";
        %bitmap = %bitmap @ %gender @ "_";
        %bitmap = %bitmap @ %relation @ "_";
        %bitmap = %bitmap @ %mode;
        if (%relation $= "self")
        {
            $gGeLocalMapIcon_ME = %ctrl;
            geMapHud2DTheOrthoMap.pushToBack(%ctrl);
            geMapHud2DDragNZoom.setCenterOnCtrl(%ctrl);
            geMapHud2DTheOrthoMap.setReferenceObject($player);
        }
    }
    %ctrl.setBitmap(%bitmap);
    return ;
}
function geMapHud2DTheOrthoMap::playerRemove(%this, %player)
{
    %ctrl = gGetField(%player, "mapCtrl");
    if (isObject(%ctrl))
    {
        %ctrl.delete();
    }
    gSetField(%player, "mapCtrl", "");
    return ;
}
