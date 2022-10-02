$TESTMissionGroupIntegrityAlreadyRun = 0;
$gPrevNumMissing = 0;
function PlayGui::onWake(%this)
{
    %this.Initialize();
    GuiTracker.updateLocation(%this);
    $enableDirectInput = 1;
    activateDirectInput();
    moveMap.push();
    functionMap.push();
    ShowAllMessageBoxes();
    AIMConvManager.wakeUp();
    BuddyHudWin.wakeUp();
    GameMgrHudWin.wakeUp();
    OptionsPanel.wakeUp();
    EmoteHudWin.wakeUp();
    WindowManager.wakeUp();
    AccountBalanceHud.Initialize();
    AccountBalanceHud.open();
    TheShapeNameHud.rolesVIP = roles::getRolesMaskFromStrings("staff moderator celeb");
    TheShapeNameHud.rolesCeleb = roles::getRolesMaskFromStrings("celeb");
    %this.schedule(100, "resetFirstResponder");
    if ($RegisterObjectFailFlag == 1)
    {
        schedule(0, 0, "MessageBoxOK", "DATABLOCK REGISTRATION OF OBJECT FAILED", "Do not continue editing this mission because you are missing datablocks and will destroy other people\'s work if you continue, but you probably just need to do an update of your working area.\n\nSearch the console.log for \'Register object failed\'." @ "\n" @ $gRegisterObjectFailList, "");
    }
    if ((getNumMissingTextures() > $gPrevNumMissing) && $ETS::devMode)
    {
        schedule(0, 0, "MessageBoxOK", "MISSING TEXTURES", getNumMissingTextures() SPC "textures were not found so far.\n\nSearch the console.log for \'missing texture:\'.", "");
        $gPrevNumMissing = getNumMissingTextures();
    }
    displayStompedObjectNameErrors();
    if (($StandAlone && $ETS::devMode) && !$TESTMissionGroupIntegrityAlreadyRun)
    {
        $TESTMissionGroupIntegrityAlreadyRun = 1;
        %errorCount = RunTestCase("TEST_MISSIONGROUPINTEGRITY", "WARNING: About that mission file you just loaded...");
    }
    return ;
}
function PlayGui::Initialize(%this)
{
    if (!%this.initialized)
    {
        %this.initialized = 1;
        %this.newContextMenu("ETSWhatsThisMenu");
        %this.newContextMenu("PlayerContextMenu");
        %this.newContextMenu("LinkContextMenu");
        %this.newContextMenu("FurnitureItemContextMenu");
    }
    return ;
}
function PlayGui::canPlayerSeeWorld(%this)
{
    if (!%this.isVisible())
    {
        return 0;
    }
    if (geTGF.isVisible())
    {
        return 0;
    }
    if (ClosetGui.isVisible())
    {
        return 0;
    }
    if (WorldMap.isVisible())
    {
        return 0;
    }
    return 1;
}
function PlayGui::onSleep(%this)
{
    functionMap.pop();
    moveMap.pop();
    return ;
}
function PlayGui::onCanvasResize(%this)
{
    if (isObject(ButtonBar))
    {
        ButtonBar.update();
    }
    if (isObject(MusicHud))
    {
        MusicHud.update();
    }
    if (isObject(WindowManager))
    {
        WindowManager.update();
    }
    if (isObject(ConvBubScroll))
    {
        ConvBubScroll.scrollToBottom();
    }
    if (isObject(PlayerContextMenu))
    {
        PlayerContextMenu.forceClose();
    }
    if (isObject(MLScrollInspectPanel))
    {
        MLScrollInspectPanel.updateSize();
    }
    return ;
}
function PlayGui::resetFirstResponder(%this)
{
    if (MessageHud.isVisible())
    {
        MessageHudEdit.makeFirstResponder(1);
    }
    else
    {
        TheShapeNameHud.makeFirstResponder(1);
    }
    return ;
}
function PlayGui::onMouseUp(%this, %obj, %pt, %worldVec)
{
    %power = 1;
    onMouseUpThrowBall(%power, %worldVec);
    return ;
}
function PlayGui::onMouseDownObj(%this, %obj, %pt, %worldVec)
{
    if ($ETS::devMode && $DevPref::Debug::PrintClickedOn)
    {
        error(getScopeName() SPC "-" SPC getDebugString(%obj));
    }
    if (isObject(adminGui))
    {
        adminGui.tryTarget(%obj);
    }
    if (isObject(animatorPanel))
    {
        animatorPanel.tryTarget(%obj);
    }
    if (isObject(salonChairControlGui))
    {
        salonChairControlGui.tryTarget(%obj);
    }
    if (!isObject(%obj))
    {
        onLeftClickSwatch(0);
        return ;
    }
    %type = %obj.getType();
    if ($gSwatchPaintingModeOn)
    {
        onLeftClickSwatch(%obj);
    }
    else
    {
        if (%type & $TypeMasks::AdvertObjectType)
        {
            %this.onAdvertClick(%obj, %pt);
        }
        else
        {
            if (%type & $TypeMasks::UsableObjectType)
            {
                %this.onUsableObjectClick(%obj, %pt);
            }
            else
            {
                if (%type & $TypeMasks::PlayerObjectType)
                {
                    onLeftClickPlayerName(%obj.getShapeName(), %obj);
                }
                else
                {
                    echoDebug("got a clicked object but didn\'t find a proper typemask:" SPC %type SPC getDebugString(%obj));
                }
            }
        }
    }
    return ;
}
function PlayGui::onRightMouseDown(%this, %obj, %pt)
{
    %type = 0;
    if (isObject(%obj))
    {
        %type = %obj.getType();
    }
    if (%type & $TypeMasks::InteriorObjectType)
    {
        onRightClickDownInterior(%obj);
    }
    return ;
}
function PlayGui::onRightMouseUp(%this, %obj)
{
    %type = 0;
    if (isObject(%obj))
    {
        %type = %obj.getType();
    }
    if (%type & $TypeMasks::AdvertObjectType)
    {
        %this.onAdvertClick(%obj, %pt);
    }
    else
    {
        if (%type & $TypeMasks::UsableObjectType)
        {
            %this.onUsableObjectRightClick(%obj);
        }
        else
        {
            if (%type & $TypeMasks::PlayerObjectType)
            {
                %this.onRMBPlayer(%obj);
            }
            else
            {
                if (%type & $TypeMasks::InteriorObjectType)
                {
                    onRightClickUpInterior(%obj);
                }
            }
        }
    }
    return ;
}
function PlayGui::onUsableObjectClick(%this, %obj, %pt)
{
    setIdle(0);
    %nuggetId = %obj.getInventoryNuggetID();
    if (%obj.seatDisplay)
    {
        ClientSittingSystemOnClick(%obj);
    }
    else
    {
        if (%nuggetId >= 0)
        {
            if ($CS_EditingCustomSpace && !(($Keyboard::modifierKeys & $EventModifier::CTRL)))
            {
                CSFurnitureMover.SelectNuggetObject(%obj);
            }
            else
            {
                if (checkInteractOK(%obj))
                {
                    if (%obj.hasMethod("onUse"))
                    {
                        %obj.onUse($player);
                    }
                    commandToServer('usableObjectClick', %obj.getGhostID());
                }
            }
        }
        else
        {
            if (checkInteractOK(%obj))
            {
                if (%obj.hasMethod("onUse"))
                {
                    %obj.onUse($player);
                }
                commandToServer('usableObjectClick', %obj.getGhostID());
            }
        }
    }
    return ;
}
function PlayGui::onUsableObjectRightClick(%this, %obj)
{
    %nuggetId = %obj.getInventoryNuggetID();
    if ((%nuggetId >= 0) && $CS_EditingCustomSpace)
    {
        FurnitureItemContextMenu.initWithObject(%obj);
        FurnitureItemContextMenu.showAtCursor();
    }
    else
    {
        if (%obj != 0)
        {
            if (checkInteractOK(%obj) && %obj.hasMethod("onRightUse"))
            {
                %obj.onRightUse();
            }
        }
    }
    return ;
}
$gPlayGuiLastMouseOver = 0;
function PlayGui::onMouseOver(%this, %obj)
{
    if (isObject($TSControl::objSelLastMouseOver))
    {
        $TSControl::objSelLastMouseOver.SetHighlighted(0);
    }
    Canvas.setCursor(ETSDefaultCursor);
    if (%obj == 0)
    {
        onMouseOverSwatchObj(0);
        return ;
    }
    if ($gSwatchPaintingModeOn)
    {
        tryOnMouseOverSwatches(%obj);
    }
    else
    {
        %highlightOk = $CS_EditingCustomSpace || checkInteractOK(%obj);
        if (%highlightOk)
        {
            %obj.SetHighlighted(1);
            Canvas.setCursor(ETSHandCursor);
            %type = %obj.getType();
            %datablock = %obj.hasMethod("getDataBlock") ? %obj.getDataBlock() : 0;
            if (((((1 && %obj.isGhost()) && (%type & $TypeMasks::UsableObjectType)) && isObject(%datablock)) && !((%datablock.playerAnimReach $= ""))) && !$CS_EditingCustomSpace)
            {
                commandToServer('UsableObjectReach', %obj.getGhostID());
            }
        }
    }
    return ;
}
function checkInteractOK(%obj)
{
    %activateDistance = 0;
    %interactOK = 1;
    if (%obj.getType() & $TypeMasks::InteriorObjectType)
    {
        return 0;
    }
    if (%obj.hasMethod("getActivationRange"))
    {
        %activateDistance = %obj.getActivationRange();
    }
    if ((%activateDistance == 0) && %obj.hasMethod("getDataBlock"))
    {
        %datablock = %obj.getDataBlock();
        %activateDistance = %datablock.activateRange;
    }
    if (%activateDistance > 0)
    {
        %rangeVal = %activateDistance * %activateDistance;
        %distVal = VectorDistSquared($player.getPosition(), %obj.getPosition());
        if (%rangeVal < %distVal)
        {
            %interactOK = 0;
        }
    }
    return %interactOK;
}
function GuiControl::getTopWindow(%this)
{
    return %this.getTopNthWindow(0);
}
function GuiControl::getTopNthWindow(%this, %ndex)
{
    %count = %this.getCount();
    %num = 0;
    %idx = %count - 1;
    while (%idx >= 0)
    {
        %obj = %this.getObject(%idx);
        if ((%obj.profile.canKeyFocus && %obj.isVisible()) && (%obj.getId() != TheShapeNameHud.getId()))
        {
            if (%num >= %ndex)
            {
                return %obj;
            }
            else
            {
                %num = %num + 1;
            }
        }
        %idx = %idx - 1;
    }
    return -1;
}
function GuiControl::focusTopWindow(%this)
{
    %obj = %this.getTopWindow();
    if (isObject(%obj))
    {
        %this.focusAndRaise(%obj);
    }
    else
    {
        TheShapeNameHud.makeFirstResponder(1);
    }
    return ;
}
function GuiControl::closeTopClosableWindow(%this)
{
    %closedOne = 0;
    %n = 0;
    while (!%closedOne)
    {
        %obj = %this.getTopNthWindow(%n);
        if (!isObject(%obj))
        {
            continue;
        }
        if (!(%obj.closeCommand $= ""))
        {
            eval("%closedOne =" SPC %obj.closeCommand);
        }
        else
        {
            %closedOne = %obj.close();
        }
        %n = %n + 1;
    }
    %this.focusTopWindow();
    return %closedOne;
}
function GuiControl::dumpTopWindows(%this)
{
    %n = 0;
    while (1)
    {
        %obj = %this.getTopNthWindow(%n);
        if (!isObject(%obj))
        {
            continue;
        }
        echo(getDebugString(%obj));
        %n = %n + 1;
    }
}

function GuiControl::showRaiseOrHide(%this, %ctrl)
{
    if (%ctrl.isVisible())
    {
        %top = %this.getTopWindow();
        if (%top.getId() == %ctrl.getId())
        {
            %ctrl.close();
        }
        else
        {
            %this.focusAndRaise(%ctrl);
        }
    }
    else
    {
        %ctrl.open();
    }
    return ;
}
function GuiControl::showRaise(%this, %ctrl)
{
    if (%ctrl.isVisible())
    {
        %top = %this.getTopWindow();
        if (%top.getId() != %ctrl.getId())
        {
            %this.focusAndRaise(%ctrl);
        }
    }
    else
    {
        %ctrl.open();
    }
    return ;
}
function GuiControl::focusAndRaise(%this, %ctrl)
{
    Canvas.cursorOn();
    %this.pushToBack(%ctrl);
    %ctrl.makeFirstResponder(1);
    return ;
}
function GuiControl::ensureAdded(%this, %panel)
{
    if (%panel.getParent() == %this.getId())
    {
        return ;
    }
    %this.add(%panel);
    %panel.setVisible(0);
    return ;
}
function checkDistance(%a, %b)
{
    %dist = 0;
    if (isObject(%a) && isObject(%b))
    {
        %apos = %a.getPosition();
        %bpos = %b.getPosition();
        %ax = getWord(%apos, 0);
        %ay = getWord(%apos, 1);
        %bx = getWord(%bpos, 0);
        %by = getWord(%bpos, 1);
        %distx = %bx - %ax;
        %disty = %by - %ay;
        %dist = (%distx * %distx) + (%disty * %disty);
    }
    return %dist;
}
function onBuddyStateChange(%index)
{
    BuddyHudWin.refreshAIMBuddyList();
    %buddyName = aimGetBuddyName(%index);
    %buddyState = aimGetBuddyState(%index);
    AIMConvManager.buddyStateChanged(stripUnprintables(%buddyName), %buddyState);
    return ;
}
function SitHud::sitDown(%this)
{
    commandToServer('SitDown');
    return ;
}
function SitHud::standUp(%this)
{
    commandToServer('StandUp');
    return ;
}
function clientCmdShowSitHud(%val, %sitOrStand)
{
    SitButton.setVisible(%sitOrStand);
    StandButton.setVisible(!%sitOrStand);
    SitHud.setVisible(%val);
    return ;
}
function clientCmdShowSitButton(%sitOrStand)
{
    SitButton.setVisible(%sitOrStand);
    StandButton.setVisible(!%sitOrStand);
    return ;
}
function BitmapFullScreenFlasher::FlashImage(%this, %bitmapName, %fadeInTime, %waitTime, %fadeOutTime)
{
    %this.setBitmap(%bitmapName);
    %this.setVisible(1);
    %this.fadeInTime = %fadeInTime;
    %this.waitTime = %waitTime;
    %this.fadeOutTime = %fadeOutTime;
    %this.fadeoutColor = "0 0 0 0";
    %this.reset();
    return ;
}
function BitmapFullScreenFlasher::onFinishedFading(%this)
{
    %this.setVisible(0);
    return ;
}
function clientCmdFlashImage(%imageName, %fadeInTime, %waitTime, %fadeOutTime)
{
    BitmapFullScreenFlasher.FlashImage(%imageName, %fadeInTime, %waitTime, %fadeOutTime);
    return ;
}
