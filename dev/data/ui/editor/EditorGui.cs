$AIEdit = 0;
function EStatusHudActivator::onMouseEnter(%this)
{
    if (!EStatusHud.isShowing())
    {
        EStatusHud.updateStatus();
    }
    return ;
}
function EWorldEditor::onWake(%this)
{
    EStatusHud.initialUpdateStatus();
    fxEts::updateExposureFilter();
    return ;
}
function EWorldEditor::onCanvasResize(%this)
{
    if (isObject(EStatusHud))
    {
        EStatusHud.update();
    }
    return ;
}
function toggleStatusHud()
{
    if (EStatusHud.isShowing())
    {
        EStatusHud.hide();
    }
    else
    {
        EStatusHud.show();
        EStatusHud.keepOpen(1);
    }
    return ;
}
function EStatusHud::GetSelectTypeDisplayText(%this, %type)
{
    if (%type $= $TypeMasks::ALLTYPES)
    {
        return "All Types";
    }
    else
    {
        if (%type $= $TypeMasks::TriggerObjectType)
        {
            return "Triggers";
        }
        else
        {
            if (%type $= $TypeMasks::InteriorObjectType)
            {
                return "Interiors";
            }
            else
            {
                if (%type $= $TypeMasks::MarkerObjectType)
                {
                    return "Audio Emitters";
                }
                else
                {
                    if (%type $= $TypeMasks::ShapeBaseObjectType)
                    {
                        return "Shapes and Sit Markers";
                    }
                    else
                    {
                        if (%type $= $TypeMasks::StaticTSObjectType)
                        {
                            return "Shapes and Sit Markers";
                        }
                        else
                        {
                            if (%type $= $TypeMasks::ItemObjectType)
                            {
                                return "Items";
                            }
                            else
                            {
                                if (%type $= $TypeMasks::AntiPortalObjectType)
                                {
                                    return "Antiportals";
                                }
                                else
                                {
                                    return "All Types";
                                }
                            }
                        }
                    }
                }
            }
        }
    }
    return ;
}
function EStatusHud::updateStatus(%this)
{
    EStatusHud.schedule(100, realUpdateStatus);
    return ;
}
function EStatusHud::initialUpdateStatus(%this)
{
    EStatusHud.schedule(1000, realUpdateStatus);
    return ;
}
function EStatusHud::realUpdateStatus(%this)
{
    %c2 = "\c5";
    %selected = %c2 @ EWorldEditor.getSelectionSize() @ "\c1\c0 world objects selected";
    %seltype = "\c1\c0type: " @ %c2 @ %this.GetSelectTypeDisplayText(EWorldEditor.selectType);
    %addto = "\c1\c0addgroup: " @ %c2 @ $instantGroup @ ":" @ $instantGroup.getName();
    %viewerTrans = LocalClientConnection.getControlObject().getTransform();
    %camera = "\c1\c0cam pos: " @ %c2 @ "( " @ getWord(%viewerTrans, 0) @ " , " @ getWord(%viewerTrans, 1) @ " , " @ getWord(%viewerTrans, 2) @ " )";
    %scale = "\c1\c0move scale: " @ %c2 @ EWorldEditor.mouseMoveScale;
    %grid = "\c1\c0grid size: " @ %c2 @ EWorldEditor.gridSize @ "\c1\c0,  grid snap: " @ %c2 @ EWorldEditor.snapToGrid;
    %this.charWidth = mMax(strlen(%selected), strlen(%addto));
    %this.charWidth = mMax(strlen(this.charWidth), strlen(%seltype));
    %this.charWidth = mMax(strlen(this.charWidth), strlen(%camera));
    %this.charWidth = mMax(strlen(this.charWidth), strlen(%scale));
    %this.charWidth = mMax(strlen(this.charWidth), strlen(%grid));
    %text = %selected NL %seltype NL %addto NL %camera NL %scale NL %grid;
    %this.setStatusText(%text);
    return ;
}
function EStatusHud::setStatusText(%this, %text)
{
    %this.text = %text;
    %this.charWidth = strlen(%text);
    EStatusHud.show();
    %this.hideSchedule = %this.schedule(5000, tryHide, %this);
    return ;
}
function EStatusHud::tryHide(%this)
{
    if (!$UserPref::WorldEditor::keepEStatusHudOpen)
    {
        %this.hide();
        return 1;
    }
    else
    {
        return 0;
    }
    return ;
}
function EStatusHud::update(%this)
{
    %heightOffset = 40;
    %resWidth = getWord($UserPref::Video::Resolution, 0);
    if (%resWidth <= 480)
    {
        %widthMultiplier = 7.2;
        %heightOffset = %heightOffset + 8;
        %heightDelta = 4;
        EStatusText.setProfile("MusicMLTextProfileSmall");
    }
    else
    {
        if (%resWidth <= 640)
        {
            %widthMultiplier = 8.1;
            %heightOffset = %heightOffset + 4;
            %heightDelta = 2;
            EStatusText.setProfile("MusicMLTextProfileMedium");
        }
        else
        {
            %widthMultiplier = 9;
            %heightDelta = 0;
            EStatusText.setProfile("MusicMLTextProfile");
        }
    }
    %content = "";
    if (!(%this.text $= ""))
    {
        %content = %this.text @ "\n";
    }
    EStatusText.setText(%content);
    %height = getWord(%this.extent, 1);
    %targetWidth = mCeil(%widthMultiplier * mMax(%this.charWidth, 20));
    %this.setTrgExtent(%targetWidth, %height);
    EStatusText.resize(0, 0, %targetWidth, %height);
    %this.updatePosition();
    return ;
}
function EStatusHud::updatePosition(%this)
{
    %trgX = getWord(%this.getTrgPosition(), 0);
    %trgY = ((getWord(ButtonBar.getTrgPosition(), 1) - getWord(%this.getExtent(), 1)) + $ButtonBarVar::VerticalAdjustment) + 12;
    %this.setTrgPosition(%trgX, %trgY);
    return ;
}
function EStatusHud::show(%this)
{
    if (%this.hideSchedule)
    {
        cancel(%this.hideSchedule);
    }
    %trgY = getWord(%this.getTrgPosition(), 1);
    %this.setTrgPosition(4, %trgY);
    %this.update();
    return ;
}
function EStatusHud::keepOpen(%this, %flag)
{
    $UserPref::WorldEditor::keepEStatusHudOpen = %flag;
    return ;
}
function EStatusHud::hide(%this)
{
    %this.updatePosition();
    %width = getWord(%this.getTrgExtent(), 0);
    %trgY = getWord(%this.getTrgPosition(), 1);
    %this.setTrgPosition(-%width, %trgY);
    %this.keepOpen(0);
    return ;
}
function EStatusHud::isShowing(%this)
{
    return getWord(%this.position, 0) >= 0;
}
function EStatusHud::onMouseLeaveBounds(%this)
{
    if (!%this.tryHide())
    {
        %this.updatePosition();
    }
    return ;
}
function EStatusHud::onMouseEnter(%this)
{
    %this.onMouseEnterBounds();
    return ;
}
function EStatusHud::onMouseEnterBounds(%this)
{
    if (%this.isShowing())
    {
        %posX = getWord(%this.position, 0);
        %posY = getWord(%this.position, 1);
        %this.setTrgPosition(%posX, %posY);
    }
    return ;
}
$sgEditorItemNames::sgMenu = "Synapse Gaming Tools";
$sgEditorItemNames::sgMenuItem[0] = "Lighting Pack Light Editor";
function EditorGui::getPrefs()
{
    EWorldEditor.dropType = getPrefSetting($Pref::WorldEditor::dropType, "atCamera");
    EWorldEditor.planarMovement = getPrefSetting($pref::WorldEditor::planarMovement, 1);
    EWorldEditor.undoLimit = getPrefSetting($pref::WorldEditor::undoLimit, 40);
    EWorldEditor.dropType = getPrefSetting($Pref::WorldEditor::dropType, "screenCenter");
    EWorldEditor.projectDistance = getPrefSetting($pref::WorldEditor::projectDistance, 2000);
    EWorldEditor.boundingBoxCollision = getPrefSetting($pref::WorldEditor::boundingBoxCollision, 1);
    EWorldEditor.renderPlane = getPrefSetting($pref::WorldEditor::renderPlane, 1);
    EWorldEditor.renderPlaneHashes = getPrefSetting($pref::WorldEditor::renderPlaneHashes, 1);
    EWorldEditor.gridColor = getPrefSetting($pref::WorldEditor::gridColor, "255 255 255 20");
    EWorldEditor.planeDim = getPrefSetting($pref::WorldEditor::planeDim, 500);
    EWorldEditor.gridSize = getPrefSetting($pref::WorldEditor::gridSize, "10 10 10");
    EWorldEditor.renderPopupBackground = getPrefSetting($pref::WorldEditor::renderPopupBackground, 1);
    EWorldEditor.popupBackgroundColor = getPrefSetting($pref::WorldEditor::popupBackgroundColor, "100 100 100");
    EWorldEditor.popupTextColor = getPrefSetting($pref::WorldEditor::popupTextColor, "255 255 0");
    EWorldEditor.selectHandle = getPrefSetting($pref::WorldEditor::selectHandle, "gui/Editor_SelectHandle.png");
    EWorldEditor.defaultHandle = getPrefSetting($pref::WorldEditor::defaultHandle, "gui/Editor_DefaultHandle.png");
    EWorldEditor.lockedHandle = getPrefSetting($pref::WorldEditor::lockedHandle, "gui/Editor_LockedHandle.png");
    EWorldEditor.objectTextColor = getPrefSetting($pref::WorldEditor::objectTextColor, "255 255 255");
    EWorldEditor.objectsUseBoxCenter = getPrefSetting($pref::WorldEditor::objectsUseBoxCenter, 1);
    EWorldEditor.axisGizmoMaxScreenLen = getPrefSetting($pref::WorldEditor::axisGizmoMaxScreenLen, 200);
    EWorldEditor.axisGizmoActive = getPrefSetting($pref::WorldEditor::axisGizmoActive, 1);
    EWorldEditor.mouseMoveScale = getPrefSetting($pref::WorldEditor::mouseMoveScale, 0.01);
    EWorldEditor.mouseRotateScale = getPrefSetting($pref::WorldEditor::mouseRotateScale, 0.01);
    EWorldEditor.mouseScaleScale = getPrefSetting($pref::WorldEditor::mouseScaleScale, 0.01);
    EWorldEditor.objSelectFillAlpha = getPrefSetting($pref::WorldEditor::objSelectFillAlpha, 100);
    EWorldEditor.minScaleFactor = getPrefSetting($pref::WorldEditor::minScaleFactor, 0.1);
    EWorldEditor.maxScaleFactor = getPrefSetting($pref::WorldEditor::maxScaleFactor, 4000);
    EWorldEditor.objSelectColor = getPrefSetting($pref::WorldEditor::objSelectColor, "255 0 0");
    EWorldEditor.objMouseOverSelectColor = getPrefSetting($pref::WorldEditor::objMouseOverSelectColor, "0 0 255");
    EWorldEditor.objMouseOverColor = getPrefSetting($pref::WorldEditor::objMouseOverColor, "0 255 0");
    EWorldEditor.showMousePopupInfo = getPrefSetting($pref::WorldEditor::showMousePopupInfo, 1);
    EWorldEditor.dragRectColor = getPrefSetting($pref::WorldEditor::dragRectColor, "255 255 0");
    EWorldEditor.renderObjText = getPrefSetting($pref::WorldEditor::renderObjText, 0);
    EWorldEditor.renderObjHandle = getPrefSetting($pref::WorldEditor::renderObjHandle, 0);
    EWorldEditor.faceSelectColor = getPrefSetting($pref::WorldEditor::faceSelectColor, "0 0 100 100");
    EWorldEditor.renderSelectionBox = getPrefSetting($pref::WorldEditor::renderSelectionBox, 0);
    EWorldEditor.selectionBoxColor = getPrefSetting($pref::WorldEditor::selectionBoxColor, "255 255 0");
    EWorldEditor.snapToGrid = getPrefSetting($pref::WorldEditor::snapToGrid, 0);
    EWorldEditor.snapRotations = getPrefSetting($pref::WorldEditor::snapRotations, 0);
    EWorldEditor.rotationSnap = getPrefSetting($pref::WorldEditor::rotationSnap, 15);
    ETerrainEditor.softSelecting = 1;
    ETerrainEditor.currentAction = "raiseHeight";
    ETerrainEditor.currentMode = "select";
    return ;
}
function EditorGui::setPrefs()
{
    $Pref::WorldEditor::dropType = EWorldEditor.dropType;
    $pref::WorldEditor::planarMovement = EWorldEditor.planarMovement;
    $pref::WorldEditor::undoLimit = EWorldEditor.undoLimit;
    $Pref::WorldEditor::dropType = EWorldEditor.dropType;
    $pref::WorldEditor::projectDistance = EWorldEditor.projectDistance;
    $pref::WorldEditor::boundingBoxCollision = EWorldEditor.boundingBoxCollision;
    $pref::WorldEditor::renderPlane = EWorldEditor.renderPlane;
    $pref::WorldEditor::renderPlaneHashes = EWorldEditor.renderPlaneHashes;
    $pref::WorldEditor::gridColor = EWorldEditor.gridColor;
    $pref::WorldEditor::planeDim = EWorldEditor.planeDim;
    $pref::WorldEditor::gridSize = EWorldEditor.gridSize;
    $pref::WorldEditor::renderPopupBackground = EWorldEditor.renderPopupBackground;
    $pref::WorldEditor::popupBackgroundColor = EWorldEditor.popupBackgroundColor;
    $pref::WorldEditor::popupTextColor = EWorldEditor.popupTextColor;
    $pref::WorldEditor::selectHandle = EWorldEditor.selectHandle;
    $pref::WorldEditor::defaultHandle = EWorldEditor.defaultHandle;
    $pref::WorldEditor::lockedHandle = EWorldEditor.lockedHandle;
    $pref::WorldEditor::objectTextColor = EWorldEditor.objectTextColor;
    $pref::WorldEditor::objectsUseBoxCenter = EWorldEditor.objectsUseBoxCenter;
    $pref::WorldEditor::axisGizmoMaxScreenLen = EWorldEditor.axisGizmoMaxScreenLen;
    $pref::WorldEditor::axisGizmoActive = EWorldEditor.axisGizmoActive;
    $pref::WorldEditor::mouseMoveScale = EWorldEditor.mouseMoveScale;
    $pref::WorldEditor::mouseRotateScale = EWorldEditor.mouseRotateScale;
    $pref::WorldEditor::mouseScaleScale = EWorldEditor.mouseScaleScale;
    $pref::WorldEditor::objSelectFillAlpha = EWorldEditor.objSelectFillAlpha;
    $pref::WorldEditor::minScaleFactor = EWorldEditor.minScaleFactor;
    $pref::WorldEditor::maxScaleFactor = EWorldEditor.maxScaleFactor;
    $pref::WorldEditor::objSelectColor = EWorldEditor.objSelectColor;
    $pref::WorldEditor::objMouseOverSelectColor = EWorldEditor.objMouseOverSelectColor;
    $pref::WorldEditor::objMouseOverColor = EWorldEditor.objMouseOverColor;
    $pref::WorldEditor::showMousePopupInfo = EWorldEditor.showMousePopupInfo;
    $pref::WorldEditor::dragRectColor = EWorldEditor.dragRectColor;
    $pref::WorldEditor::renderObjText = EWorldEditor.renderObjText;
    $pref::WorldEditor::renderObjHandle = EWorldEditor.renderObjHandle;
    $pref::WorldEditor::raceSelectColor = EWorldEditor.faceSelectColor;
    $pref::WorldEditor::renderSelectionBox = EWorldEditor.renderSelectionBox;
    $pref::WorldEditor::selectionBoxColor = EWorldEditor.selectionBoxColor;
    $pref::WorldEditor::snapToGrid = EWorldEditor.snapToGrid;
    $pref::WorldEditor::snapRotations = EWorldEditor.snapRotations;
    $pref::WorldEditor::rotationSnap = EWorldEditor.rotationSnap;
    EStatusHud.updateStatus();
    return ;
}
function EditorGui::onSleep(%this)
{
    %this.setPrefs();
    return ;
}
function EditorGui::init(%this)
{
    %this.getPrefs();
    if (!isObject("terraformer"))
    {
    }
    new Terraformer("terraformer");
    $SelectedOperation = -1;
    $NextOperationId = 1;
    $HeightfieldDirtyRow = -1;
    EditorMenuBar.clearMenus();
    EditorMenuBar.addMenu("File", 0);
    EditorMenuBar.addMenuItem("File", "New Mission...", 1);
    EditorMenuBar.addMenuItem("File", "Open Mission...", 2, "Ctrl O");
    EditorMenuBar.addMenuItem("File", "Save Mission...", 3, "Ctrl S");
    EditorMenuBar.addMenuItem("File", "Save Mission As...", 4);
    EditorMenuBar.addMenuItem("File", "-", 0);
    EditorMenuBar.addMenuItem("File", "Import Terraform Data...", 6);
    EditorMenuBar.addMenuItem("File", "Import Texture Data...", 5);
    EditorMenuBar.addMenuItem("File", "-", 0);
    EditorMenuBar.addMenuItem("File", "Refresh File List", 7);
    EditorMenuBar.addMenuItem("File", "-", 0);
    EditorMenuBar.addMenuItem("File", "Export Terraform Bitmap...", 5);
    EditorMenuBar.addMenu("Edit", 1);
    EditorMenuBar.addMenuItem("Edit", "Undo", 1, "Ctrl Z");
    EditorMenuBar.setMenuItemBitmap("Edit", "Undo", 1);
    EditorMenuBar.addMenuItem("Edit", "Redo", 2, "Ctrl R");
    EditorMenuBar.setMenuItemBitmap("Edit", "Redo", 2);
    EditorMenuBar.addMenuItem("Edit", "-", 0);
    EditorMenuBar.addMenuItem("Edit", "Cut", 3, "Ctrl X");
    EditorMenuBar.setMenuItemBitmap("Edit", "Cut", 3);
    EditorMenuBar.addMenuItem("Edit", "Copy", 4, "Ctrl C");
    EditorMenuBar.setMenuItemBitmap("Edit", "Copy", 4);
    EditorMenuBar.addMenuItem("Edit", "Paste", 5, "Ctrl V");
    EditorMenuBar.setMenuItemBitmap("Edit", "Paste", 5);
    EditorMenuBar.addMenuItem("Edit", "-", 0);
    EditorMenuBar.addMenuItem("Edit", "Select All", 6, "Ctrl A");
    EditorMenuBar.addMenuItem("Edit", "Select None", 7, "Ctrl N");
    EditorMenuBar.addMenuItem("Edit", "Select None", 8, "Ctrl D");
    EditorMenuBar.addMenuItem("Edit", "Select Inverse", 9, "Ctrl-Shift I");
    EditorMenuBar.addMenuItem("Edit", "Find Selected", 10, "Ctrl F");
    EditorMenuBar.addMenuItem("Edit", "Zoom Camera To Selection", 11, "Shift F");
    EditorMenuBar.addMenuItem("Edit", "Expand Selected Tree", 12, "Ctrl E");
    EditorMenuBar.addMenuItem("Edit", "Expand And Select Selected Tree", 13, "Shift E");
    EditorMenuBar.addMenuItem("Edit", "-", 0);
    EditorMenuBar.addMenuItem("Edit", "Relight Scene", 14, "Alt L");
    EditorMenuBar.addMenuItem("Edit", "-", 0);
    EditorMenuBar.addMenuItem("Edit", "World Editor Settings...", 12);
    EditorMenuBar.addMenuItem("Edit", "Terrain Editor Settings...", 13);
    EditorMenuBar.addMenuItem("Edit", "-", 0);
    EditorMenuBar.addMenuItem("Edit", "Increase Move Scale", 15, "]");
    EditorMenuBar.addMenuItem("Edit", "Decrease Move Scale", 16, "[");
    EditorMenuBar.addMenuItem("Edit", "Toggle Grid Visibility", 17, "g");
    EditorMenuBar.addMenuItem("Edit", "Status Hud Toggle", 17, "s");
    EditorMenuBar.addMenu("Camera", 7);
    EditorMenuBar.addMenuItem("Camera", "Drop Camera at Player", 1, "Alt Q");
    EditorMenuBar.addMenuItem("Camera", "Drop Player at Camera", 2, "Alt W");
    EditorMenuBar.addMenuItem("Camera", "Toggle Camera", 10, "Alt C");
    EditorMenuBar.addMenuItem("Camera", "Drop Camera at Selection", 19, "Alt T");
    EditorMenuBar.addMenuItem("Camera", "-", 0);
    EditorMenuBar.addMenuItem("Camera", "Slowest", 3, "Shift 1", 1);
    EditorMenuBar.addMenuItem("Camera", "Very Slow", 4, "Shift 2", 1);
    EditorMenuBar.addMenuItem("Camera", "Slow", 5, "Shift 3", 1);
    EditorMenuBar.addMenuItem("Camera", "Medium Pace", 6, "Shift 4", 1);
    EditorMenuBar.addMenuItem("Camera", "Fast", 7, "Shift 5", 1);
    EditorMenuBar.addMenuItem("Camera", "Very Fast", 8, "Shift 6", 1);
    EditorMenuBar.addMenuItem("Camera", "Fastest", 9, "Shift 7", 1);
    EditorMenuBar.addMenu("World", 6);
    EditorMenuBar.addMenuItem("World", "Lock Selection", 10, "Ctrl L");
    EditorMenuBar.addMenuItem("World", "Unlock Selection", 11, "Ctrl Shift L");
    EditorMenuBar.addMenuItem("World", "-", 0);
    EditorMenuBar.addMenuItem("World", "Hide Selected", 12, "Ctrl H");
    EditorMenuBar.addMenuItem("World", "Unhide Selected", 13, "Shift H");
    EditorMenuBar.addMenuItem("World", "Invert Hidden", 14, "Ctrl J");
    EditorMenuBar.addMenuItem("World", "-", 0);
    EditorMenuBar.addMenuItem("World", "Delete Selection", 17, "Delete");
    EditorMenuBar.addMenuItem("World", "Reset Transforms", 15);
    EditorMenuBar.addMenuItem("World", "Drop Selection", 16, "Ctrl Shift D");
    EditorMenuBar.addMenuItem("World", "Add Selection to Instant Group", 17);
    EditorMenuBar.addMenuItem("World", "SimGroup Create", 18, "N");
    EditorMenuBar.addMenuItem("World", "-", 0);
    EditorMenuBar.addMenuItem("World", "Drop at Origin", 0, "", 1);
    EditorMenuBar.addMenuItem("World", "Drop at Camera", 1, "", 1);
    EditorMenuBar.addMenuItem("World", "Drop at Camera w/Rot", 2, "", 1);
    EditorMenuBar.addMenuItem("World", "Drop below Camera", 3, "", 1);
    EditorMenuBar.addMenuItem("World", "Drop at Screen Center", 4, "", 1);
    EditorMenuBar.addMenuItem("World", "Drop at Centroid", 5, "", 1);
    EditorMenuBar.addMenuItem("World", "Drop to Ground", 6, "", 1);
    EditorMenuBar.addMenu("SnapTo", 9);
    EditorMenuBar.addMenuItem("SnapTo", "X", 1, "X");
    EditorMenuBar.addMenuItem("SnapTo", "X+", 2, "Shift X");
    EditorMenuBar.addMenuItem("SnapTo", "X-", 3, "Alt X");
    EditorMenuBar.addMenuItem("SnapTo", "Y", 4, "Y");
    EditorMenuBar.addMenuItem("SnapTo", "Y+", 5, "Shift Y");
    EditorMenuBar.addMenuItem("SnapTo", "Y-", 6, "Alt Y");
    EditorMenuBar.addMenuItem("SnapTo", "Z", 7, "Z");
    EditorMenuBar.addMenuItem("SnapTo", "Z+", 8, "Shift Z");
    EditorMenuBar.addMenuItem("SnapTo", "Z-", 9, "Alt Z");
    EditorMenuBar.addMenuItem("SnapTo", "X+YZ", 10, "numpad6");
    EditorMenuBar.addMenuItem("SnapTo", "X-YZ", 11, "numpad4");
    EditorMenuBar.addMenuItem("SnapTo", "XY+Z", 12, "numpad8");
    EditorMenuBar.addMenuItem("SnapTo", "XY-Z", 13, "numpad2");
    EditorMenuBar.addMenuItem("SnapTo", "XYZ+", 12, "numpad9");
    EditorMenuBar.addMenuItem("SnapTo", "XYZ-", 13, "numpad3");
    EditorMenuBar.addMenuItem("SnapTo", "ObjX+YZ", 14, "shift numpad6");
    EditorMenuBar.addMenuItem("SnapTo", "ObjX-YZ", 15, "shift numpad4");
    EditorMenuBar.addMenuItem("SnapTo", "ObjXY+Z", 16, "shift numpad8");
    EditorMenuBar.addMenuItem("SnapTo", "ObjXY-Z", 17, "shift numpad2");
    EditorMenuBar.addMenuItem("SnapTo", "ObjXYZ+", 18, "shift numpad9");
    EditorMenuBar.addMenuItem("SnapTo", "ObjXYZ-", 19, "shift numpad3");
    EditorMenuBar.addMenu("CloneTo", 10);
    EditorMenuBar.addMenuItem("CloneTo", "X+YZ", 1, "alt numpad6");
    EditorMenuBar.addMenuItem("CloneTo", "X-YZ", 2, "alt numpad4");
    EditorMenuBar.addMenuItem("CloneTo", "XY+Z", 3, "alt numpad8");
    EditorMenuBar.addMenuItem("CloneTo", "XY-Z", 4, "alt numpad2");
    EditorMenuBar.addMenuItem("CloneTo", "XYZ+", 5, "alt numpad9");
    EditorMenuBar.addMenuItem("CloneTo", "XYZ-", 6, "alt numpad3");
    EditorMenuBar.addMenu("Action", 3);
    EditorMenuBar.addMenuItem("Action", "Select", 1, "", 1);
    EditorMenuBar.addMenuItem("Action", "Adjust Selection", 2, "", 1);
    EditorMenuBar.addMenuItem("Action", "-", 0);
    EditorMenuBar.addMenuItem("Action", "Add Dirt", 6, "", 1);
    EditorMenuBar.addMenuItem("Action", "Excavate", 6, "", 1);
    EditorMenuBar.addMenuItem("Action", "Adjust Height", 6, "", 1);
    EditorMenuBar.addMenuItem("Action", "Flatten", 4, "", 1);
    EditorMenuBar.addMenuItem("Action", "Smooth", 5, "", 1);
    EditorMenuBar.addMenuItem("Action", "Set Height", 7, "", 1);
    EditorMenuBar.addMenuItem("Action", "-", 0);
    EditorMenuBar.addMenuItem("Action", "Set Empty", 8, "", 1);
    EditorMenuBar.addMenuItem("Action", "Clear Empty", 8, "", 1);
    EditorMenuBar.addMenuItem("Action", "-", 0);
    EditorMenuBar.addMenuItem("Action", "Paint Material", 9, "", 1);
    EditorMenuBar.addMenu("Brush", 4);
    EditorMenuBar.addMenuItem("Brush", "Box Brush", 91, "", 1);
    EditorMenuBar.addMenuItem("Brush", "Circle Brush", 92, "", 1);
    EditorMenuBar.addMenuItem("Brush", "-", 0);
    EditorMenuBar.addMenuItem("Brush", "Soft Brush", 93, "", 2);
    EditorMenuBar.addMenuItem("Brush", "Hard Brush", 94, "", 2);
    EditorMenuBar.addMenuItem("Brush", "-", 0);
    EditorMenuBar.addMenuItem("Brush", "Size 1 x 1", 1, "Alt 1", 3);
    EditorMenuBar.addMenuItem("Brush", "Size 3 x 3", 3, "Alt 2", 3);
    EditorMenuBar.addMenuItem("Brush", "Size 5 x 5", 5, "Alt 3", 3);
    EditorMenuBar.addMenuItem("Brush", "Size 9 x 9", 9, "Alt 4", 3);
    EditorMenuBar.addMenuItem("Brush", "Size 15 x 15", 15, "Alt 5", 3);
    EditorMenuBar.addMenuItem("Brush", "Size 25 x 25", 25, "Alt 6", 3);
    EditorMenuBar.addMenu("Window", 2);
    EditorMenuBar.addMenuItem("Window", "World Editor", 2, "F2", 1);
    EditorMenuBar.addMenuItem("Window", "World Editor Inspector", 3, "F3", 1);
    EditorMenuBar.addMenuItem("Window", "World Editor Creator", 4, "F4", 1);
    EditorMenuBar.addMenuItem("Window", "Mission Area Editor", 5, "F5", 1);
    EditorMenuBar.addMenuItem("Window", "-", 0);
    EditorMenuBar.addMenuItem("Window", "Terrain Editor", 6, "F6", 1);
    EditorMenuBar.addMenuItem("Window", "Terrain Terraform Editor", 7, "F7", 1);
    EditorMenuBar.addMenuItem("Window", "Terrain Texture Editor", 8, "F8", 1);
    EditorMenuBar.addMenuItem("Window", "Terrain Texture Painter", 9, "", 1);
    %selectMenuName = "Select Type";
    %n = 1;
    EditorMenuBar.addMenu(%selectMenuName, 11);
    EditorMenuBar.addMenuItem(%selectMenuName, "All Types", %n, "Ctrl 1", 1);
    %n = %n + 1;
    EditorMenuBar.addMenuItem(%selectMenuName, "Triggers", %n, "Ctrl 2", 1);
    %n = %n + 1;
    EditorMenuBar.addMenuItem(%selectMenuName, "Interiors", %n, "Ctrl 3", 1);
    %n = %n + 1;
    EditorMenuBar.addMenuItem(%selectMenuName, "Shapes and Sit Markers", %n, "Ctrl 4", 1);
    %n = %n + 1;
    EditorMenuBar.addMenuItem(%selectMenuName, "Antiportals", %n, "Ctrl 5", 1);
    %n = %n + 1;
    EditorMenuBar.addMenuItem(%selectMenuName, "Items", %n, "ctrl 6", 1);
    %n = %n + 1;
    EditorMenuBar.addMenuItem(%selectMenuName, "Audio Emitters", %n, "Ctrl 7", 1);
    %n = %n + 1;
    EditorMenuBar.addMenuItem(%selectMenuName, "-", %n);
    %n = %n + 1;
    EditorMenuBar.addMenuItem(%selectMenuName, "Select all AdvertShapes", %n);
    %n = %n + 1;
    EditorMenuBar.addMenuItem(%selectMenuName, "Select all AIPlayers", %n);
    %n = %n + 1;
    EditorMenuBar.addMenuItem(%selectMenuName, "Select all ETSSeatMarker", %n);
    %n = %n + 1;
    EditorMenuBar.addMenuItem(%selectMenuName, "Select all InteriorInstances", %n);
    %n = %n + 1;
    EditorMenuBar.addMenuItem(%selectMenuName, "Select all Markers", %n);
    %n = %n + 1;
    EditorMenuBar.addMenuItem(%selectMenuName, "Select all MissionMarkers", %n);
    %n = %n + 1;
    EditorMenuBar.addMenuItem(%selectMenuName, "Select all StaticShapes", %n);
    %n = %n + 1;
    EditorMenuBar.addMenuItem(%selectMenuName, "Select all sgUniversalStaticLights", %n);
    %n = %n + 1;
    EditorMenuBar.addMenuItem(%selectMenuName, "Select all Triggers", %n);
    %n = %n + 1;
    EditorMenuBar.addMenuItem(%selectMenuName, "Select all TSStatics", %n);
    %n = %n + 1;
    EditorMenuBar.addMenuItem(%selectMenuName, "Select all Waterblocks", %n);
    %n = %n + 1;
    %debugMenuName = "Render Mode";
    EditorMenuBar.addMenu(%debugMenuName, 11);
    EditorMenuBar.addMenuItem(%debugMenuName, "normal", 1, "Shift N", 1);
    EditorMenuBar.addMenuItem(%debugMenuName, "lines", 2, "Shift F2", 1);
    EditorMenuBar.addMenuItem(%debugMenuName, "detail polys", 3, "Shift F3", 1);
    EditorMenuBar.addMenuItem(%debugMenuName, "portal zones", 4, "Shift F4", 1);
    EditorMenuBar.addMenuItem(%debugMenuName, "null surfaces", 5, "Shift F5", 1);
    EditorMenuBar.addMenuItem(%debugMenuName, "portal zones nonRoot", 6, "Shift F6", 1);
    EditorMenuBar.addMenuItem(%debugMenuName, "zonesNonRoot, Detail", 7, "Shift F7", 1);
    EditorMenuBar.addMenuItem(%debugMenuName, "large textures", 8, "Shift F8", 1);
    EditorMenuBar.addMenuItem(%debugMenuName, "detail level", 9, "Shift F9", 1);
    EditorMenuBar.addMenuItem(%debugMenuName, "lightmap", 10, "Shift F10", 1);
    EditorMenuBar.addMenuItem(%debugMenuName, "only textures", 11, "Shift F11", 1);
    EditorMenuBar.addMenuItem(%debugMenuName, "triangle strips", 12, "Shift F12", 1);
    EditorMenuBar.addMenuItem(%debugMenuName, "next mode", 13, "=", 1);
    EditorMenuBar.addMenuItem(%debugMenuName, "prev mode", 14, "-", 1);
    EditorMenuBar.addMenu($sgEditorItemNames::sgMenu, 8);
    EditorMenuBar.addMenuItem($sgEditorItemNames::sgMenu, $sgEditorItemNames::sgMenuItem[0], 2, "F12");
    EditorMenuBar.onActionMenuItemSelect(0, "Adjust Height");
    EditorMenuBar.onBrushMenuItemSelect(0, "Circle Brush");
    EditorMenuBar.onBrushMenuItemSelect(0, "Soft Brush");
    EditorMenuBar.onBrushMenuItemSelect(9, "Size 9 x 9");
    EditorMenuBar.onCameraMenuItemSelect(6, "Medium Pace");
    EditorMenuBar.onWorldMenuItemSelect(0, "Drop at Screen Center");
    EWorldEditor.init();
    ETerrainEditor.attachTerrain();
    TerraformerInit();
    TextureInit();
    EditorTree.init();
    ObjectBuilderGui.init();
    EditorTree.isDirty = 0;
    EWorldEditor.isDirty = 0;
    ETerrainEditor.isDirty = 0;
    ETerrainEditor.isMissionDirty = 0;
    EditorGui.saveAs = 0;
    return ;
}
function EditorNewMission()
{
    if (((ETerrainEditor.isMissionDirty || ETerrainEditor.isDirty) || EWorldEditor.isDirty) || EditorTree.isDirty)
    {
        MessageBoxYesNo("Mission Modified", "Would you like to save changes to the current mission \"" @ $Server::MissionFile @ "\" before creating a new mission?", "EditorDoNewMission(true);", "EditorDoNewMission(false);");
    }
    else
    {
        EditorDoNewMission(0);
    }
    return ;
}
function EditorSaveMissionMenu()
{
    if (EditorGui.saveAs)
    {
        EditorSaveMissionAs();
    }
    else
    {
        EditorSaveMission();
    }
    return ;
}
function EditorSaveMission()
{
    if (((EWorldEditor.isDirty || EditorTree.isDirty) || ETerrainEditor.isMissionDirty) && !isWriteableFileName($Server::MissionFile))
    {
        MessageBoxOK("Error", "Mission file \"" @ $Server::MissionFile @ "\" is read-only.", "");
        return 0;
    }
    if (ETerrainEditor.isDirty && !isWriteableFileName(Terrain.terrainFile))
    {
        MessageBoxOK("Error", "Terrain file \"" @ Terrain.terrainFile @ "\" is read-only.", "");
        return 0;
    }
    %errorCount = RunTestCase("TEST_MISSIONGROUPINTEGRITY", "WARNING: About that mission file you just saved...");
    if ((EWorldEditor.isDirty || EditorTree.isDirty) || ETerrainEditor.isMissionDirty)
    {
        if (MissionInfo.mode $= "PrivateSpaceDesign")
        {
            if (isObject(PRIVATESPACE_GROUP))
            {
                RootGroup.add(PRIVATESPACE_GROUP);
            }
            else
            {
                error("PrivateSpaceDesign mode, no PRIVATESPACE_GROUP object");
            }
        }
        MissionGroup.save($Server::MissionFile);
        if (MissionInfo.mode $= "PrivateSpaceDesign")
        {
            if (isObject(PRIVATESPACE_GROUP))
            {
                %spaceForGridFileName = getSubStr($Server::MissionFile, 0, strlen($Server::MissionFile) - 4) @ "_generated.cs";
                PRIVATESPACE_GROUP.save(%spaceForGridFileName);
                echo("PrivateSpaceDesign mode saving to space for grid, file named:" SPC %spaceForGridFileName);
                MissionGroup.add(PRIVATESPACE_GROUP);
            }
        }
    }
    if (MissionInfo.mode $= "PrivateSpaceDesign")
    {
        if (isObject(PRIVATESPACE_GROUP))
        {
            %spaceForGridFileName = getSubStr($Server::MissionFile, 0, strlen($Server::MissionFile) - 4) @ "_generated.cs";
            PRIVATESPACE_GROUP.save(%spaceForGridFileName);
            echo("PrivateSpaceDesign mode saving to space for grid, file named:" SPC %spaceForGridFileName);
        }
    }
    if (ETerrainEditor.isDirty)
    {
        Terrain.save(Terrain.terrainFile);
    }
    EditorTree.isDirty = 0;
    EWorldEditor.isDirty = 0;
    ETerrainEditor.isDirty = 0;
    ETerrainEditor.isMissionDirty = 0;
    EditorGui.saveAs = 0;
    return 1;
}
function EditorDoSaveAs(%missionName)
{
    ETerrainEditor.isDirty = 1;
    EWorldEditor.isDirty = 1;
    EditorTree.isDirty = 1;
    %saveMissionFile = $Server::MissionFile;
    %saveTerrName = Terrain.terrainFile;
    $Server::MissionFile = %missionName;
    Terrain.terrainFile = filePath(%missionName) @ "/" @ fileBase(%missionName) @ ".ter";
    if (!EditorSaveMission())
    {
        $Server::MissionFile = %saveMissionFile;
        Terrain.terrainFile = %saveTerrName;
    }
    return ;
}
function EditorSaveMissionAs()
{
    getSaveFilename("*.mis", "EditorDoSaveAs", $Server::MissionFile);
    return ;
}
function EditorDoLoadMission(%file)
{
    Editor.close();
    loadMission(%file, 1);
    Editor::Create();
    MissionCleanup.add(Editor);
    EditorGui.loadingMission = 1;
    Editor.open();
    return ;
}
function EditorSaveBeforeLoad()
{
    if (EditorSaveMission())
    {
        getLoadFilename("*.mis", "EditorDoLoadMission");
    }
    return ;
}
function EditorDoNewMission(%saveFirst)
{
    if (%saveFirst)
    {
        EditorSaveMission();
    }
    %file = findFirstFile("*/newMission.mis");
    if (%file $= "")
    {
        MessageBoxOK("Error", "Missing mission template \"newMission.mis\".", "");
        return ;
    }
    EditorDoLoadMission(%file);
    EditorGui.saveAs = 1;
    EWorldEditor.isDirty = 1;
    ETerrainEditor.isDirty = 1;
    EditorTree.isDirty = 1;
    return ;
}
function EditorOpenMission()
{
    if (((ETerrainEditor.isMissionDirty || ETerrainEditor.isDirty) || EWorldEditor.isDirty) || EditorTree.isDirty)
    {
        MessageBoxYesNo("Mission Modified", "Would you like to save changes to the current mission \"" @ $Server::MissionFile @ "\" before opening a new mission?", "EditorSaveBeforeLoad();", "getLoadFilename(\"*.mis\", \"EditorDoLoadMission\");");
    }
    else
    {
        getLoadFilename("*.mis", "EditorDoLoadMission");
    }
    return ;
}
function EditorMenuBar::onMenuSelect(%this, %unused, %menu)
{
    if (%menu $= "File")
    {
        %editingHeightfield = ETerrainEditor.isVisible() && EHeightField.isVisible();
        EditorMenuBar.setMenuItemEnable("File", "Export Terraform Bitmap...", %editingHeightfield);
        EditorMenuBar.setMenuItemEnable("File", "Save Mission...", ((ETerrainEditor.isDirty || ETerrainEditor.isMissionDirty) || EWorldEditor.isDirty) || EditorTree.isDirty);
    }
    else
    {
        if (%menu $= "Edit")
        {
            %selSize = EWorldEditor.getSelectionSize();
            EditorMenuBar.setMenuItemEnable("Edit", "Zoom Camera To Selection", %selSize > 0);
            if (EWorldEditor.isVisible())
            {
                EditorMenuBar.setMenuItemEnable("Edit", "Select All", 1);
                EditorMenuBar.setMenuItemEnable("Edit", "Paste", EWorldEditor.canPasteSelection());
                %canCutCopy = EWorldEditor.getSelectionSize() > 0;
                EditorMenuBar.setMenuItemEnable("Edit", "Cut", %canCutCopy);
                EditorMenuBar.setMenuItemEnable("Edit", "Copy", %canCutCopy);
            }
            else
            {
                if (ETerrainEditor.isVisible())
                {
                    EditorMenuBar.setMenuItemEnable("Edit", "Cut", 0);
                    EditorMenuBar.setMenuItemEnable("Edit", "Copy", 0);
                    EditorMenuBar.setMenuItemEnable("Edit", "Paste", 0);
                    EditorMenuBar.setMenuItemEnable("Edit", "Select All", 0);
                }
            }
        }
        else
        {
            if (%menu $= "World")
            {
                %selSize = EWorldEditor.getSelectionSize();
                %lockCount = EWorldEditor.getSelectionLockCount();
                %hideCount = EWorldEditor.getSelectionHiddenCount();
                EditorMenuBar.setMenuItemEnable("World", "Lock Selection", %lockCount < %selSize);
                EditorMenuBar.setMenuItemEnable("World", "Unlock Selection", %lockCount > 0);
                EditorMenuBar.setMenuItemEnable("World", "Hide Selected", %hideCount < %selSize);
                EditorMenuBar.setMenuItemEnable("World", "Unhide Selected", %hideCount > 0);
                EditorMenuBar.setMenuItemEnable("World", "Invert Hidden", %selSize > 0);
                EditorMenuBar.setMenuItemEnable("World", "Add Selection to Instant Group", %selSize > 0);
                EditorMenuBar.setMenuItemEnable("World", "Reset Transforms", (%selSize > 0) && (%lockCount == 0));
                EditorMenuBar.setMenuItemEnable("World", "Drop Selection", (%selSize > 0) && (%lockCount == 0));
                EditorMenuBar.setMenuItemEnable("World", "Delete Selection", (%selSize > 0) && (%lockCount == 0));
            }
        }
    }
    return ;
}
function EditorMenuBar::onMenuItemSelect(%this, %unused, %menu, %itemId, %item)
{
    if (%menu $= "File")
    {
        %this.onFileMenuItemSelect(%itemId, %item);
    }
    else
    {
        if (%menu $= "Edit")
        {
            %this.onEditMenuItemSelect(%itemId, %item);
        }
        else
        {
            if (%menu $= "World")
            {
                %this.onWorldMenuItemSelect(%itemId, %item);
            }
            else
            {
                if (%menu $= "Window")
                {
                    %this.onWindowMenuItemSelect(%itemId, %item);
                }
                else
                {
                    if (%menu $= "Select Type")
                    {
                        %this.onSelectTypeMenuItemSelect(%itemId, %item);
                    }
                    else
                    {
                        if (%menu $= "Render Mode")
                        {
                            %this.onRenderModeMenuItemSelect(%itemId, %item);
                        }
                        else
                        {
                            if (%menu $= "Action")
                            {
                                %this.onActionMenuItemSelect(%itemId, %item);
                            }
                            else
                            {
                                if (%menu $= "Brush")
                                {
                                    %this.onBrushMenuItemSelect(%itemId, %item);
                                }
                                else
                                {
                                    if (%menu $= "Camera")
                                    {
                                        %this.onCameraMenuItemSelect(%itemId, %item);
                                    }
                                    else
                                    {
                                        if (%menu $= "SnapTo")
                                        {
                                            %this.OnSnapToMenuItemSelect(%itemId, %item);
                                        }
                                        else
                                        {
                                            if (%menu $= "CloneTo")
                                            {
                                                %this.OnCloneToMenuItemSelect(%itemId, %item);
                                            }
                                            else
                                            {
                                                if (%menu $= $sgEditorItemNames::sgMenu)
                                                {
                                                    %this.onToggleSGTools(%itemId, %item);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
    return ;
}
function refreshFileList()
{
    setModPaths(getModPaths());
    Creator.init();
    return ;
}
function EditorMenuBar::onFileMenuItemSelect(%this, %itemId, %item)
{
    if (%item $= "New Mission...")
    {
        EditorNewMission();
    }
    else
    {
        if (%item $= "Open Mission...")
        {
            EditorOpenMission();
        }
        else
        {
            if (%item $= "Save Mission...")
            {
                EditorSaveMissionMenu();
            }
            else
            {
                if (%item $= "Save Mission As...")
                {
                    EditorSaveMissionAs();
                }
                else
                {
                    if (%item $= "Import Texture Data...")
                    {
                        texture::import();
                    }
                    else
                    {
                        if (%item $= "Import Terraform Data...")
                        {
                            Heightfield::import();
                        }
                        else
                        {
                            if (%item $= "Export Terraform Bitmap...")
                            {
                                Heightfield::saveBitmap("");
                            }
                            else
                            {
                                if (%item $= "Refresh File List")
                                {
                                    refreshFileList();
                                }
                                else
                                {
                                    if (%item $= "Quit")
                                    {
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
    return ;
}
function EditorMenuBar::onCameraMenuItemSelect(%this, %itemId, %item)
{
    if (%item $= "Drop Camera at Player")
    {
        commandToServer('dropCameraAtPlayer');
    }
    else
    {
        if (%item $= "Drop Player at Camera")
        {
            commandToServer('DropPlayerAtCamera');
        }
        else
        {
            if (%item $= "Toggle Camera")
            {
                commandToServer('ToggleCamera');
            }
            else
            {
                if (%item $= "Drop Camera at Selection")
                {
                    EWorldEditor.dropCameraWithSelectionInView();
                }
                else
                {
                    %this.setMenuItemChecked("Camera", %itemId, 1);
                    $Camera::movementSpeed = (((%itemId - 3) / 6) * 195) + 5;
                }
            }
        }
    }
    return ;
}
function EditorMenuBar::onActionMenuItemSelect(%this, %itemId, %item)
{
    EditorMenuBar.setMenuItemChecked("Action", %item, 1);
    if (%item $= "Select")
    {
        ETerrainEditor.currentMode = "select";
        ETerrainEditor.selectionHidden = 0;
        ETerrainEditor.renderVertexSelection = 1;
        ETerrainEditor.setAction("select");
    }
    else
    {
        if (%item $= "Adjust Selection")
        {
            ETerrainEditor.currentMode = "adjust";
            ETerrainEditor.selectionHidden = 0;
            ETerrainEditor.setAction("adjustHeight");
            ETerrainEditor.currentAction = brushAdjustHeight;
            ETerrainEditor.renderVertexSelection = 1;
        }
        else
        {
            ETerrainEditor.currentMode = "paint";
            ETerrainEditor.selectionHidden = 1;
            ETerrainEditor.setAction(ETerrainEditor.currentAction);
            if (%item $= "Add Dirt")
            {
                ETerrainEditor.currentAction = raiseHeight;
                ETerrainEditor.renderVertexSelection = 1;
            }
            else
            {
                if (%item $= "Paint Material")
                {
                    ETerrainEditor.currentAction = paintMaterial;
                    ETerrainEditor.renderVertexSelection = 1;
                }
                else
                {
                    if (%item $= "Excavate")
                    {
                        ETerrainEditor.currentAction = lowerHeight;
                        ETerrainEditor.renderVertexSelection = 1;
                    }
                    else
                    {
                        if (%item $= "Set Height")
                        {
                            ETerrainEditor.currentAction = setHeight;
                            ETerrainEditor.renderVertexSelection = 1;
                        }
                        else
                        {
                            if (%item $= "Adjust Height")
                            {
                                ETerrainEditor.currentAction = brushAdjustHeight;
                                ETerrainEditor.renderVertexSelection = 1;
                            }
                            else
                            {
                                if (%item $= "Flatten")
                                {
                                    ETerrainEditor.currentAction = flattenHeight;
                                    ETerrainEditor.renderVertexSelection = 1;
                                }
                                else
                                {
                                    if (%item $= "Smooth")
                                    {
                                        ETerrainEditor.currentAction = smoothHeight;
                                        ETerrainEditor.renderVertexSelection = 1;
                                    }
                                    else
                                    {
                                        if (%item $= "Set Empty")
                                        {
                                            ETerrainEditor.currentAction = setEmpty;
                                            ETerrainEditor.renderVertexSelection = 0;
                                        }
                                        else
                                        {
                                            if (%item $= "Clear Empty")
                                            {
                                                ETerrainEditor.currentAction = clearEmpty;
                                                ETerrainEditor.renderVertexSelection = 0;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            if (ETerrainEditor.currentMode $= "select")
            {
                ETerrainEditor.processAction(ETerrainEditor.currentAction);
            }
            else
            {
                if (ETerrainEditor.currentMode $= "paint")
                {
                    ETerrainEditor.setAction(ETerrainEditor.currentAction);
                }
            }
        }
    }
    return ;
}
function EditorMenuBar::onBrushMenuItemSelect(%this, %itemId, %item)
{
    EditorMenuBar.setMenuItemChecked("Brush", %item, 1);
    if (%item $= "Box Brush")
    {
        ETerrainEditor.setBrushType(box);
    }
    else
    {
        if (%item $= "Circle Brush")
        {
            ETerrainEditor.setBrushType(ellipse);
        }
        else
        {
            if (%item $= "Soft Brush")
            {
                ETerrainEditor.enableSoftBrushes = 1;
            }
            else
            {
                if (%item $= "Hard Brush")
                {
                    ETerrainEditor.enableSoftBrushes = 0;
                }
                else
                {
                    ETerrainEditor.brushSize = %itemId;
                    ETerrainEditor.setBrushSize(%itemId, %itemId);
                }
            }
        }
    }
    return ;
}
function EditorMenuBar::onRenderModeMenuItemSelect(%this, %itemId, %item)
{
    EditorMenuBar.setMenuItemChecked("Render Mode", %item, 1);
    if (%item $= "normal")
    {
        setInteriorRenderMode(0);
    }
    else
    {
        if (%item $= "lines")
        {
            setInteriorRenderMode(1);
        }
        else
        {
            if (%item $= "detail polys")
            {
                setInteriorRenderMode(2);
            }
            else
            {
                if (%item $= "portal zones")
                {
                    setInteriorRenderMode(7);
                }
                else
                {
                    if (%item $= "null surfaces")
                    {
                        setInteriorRenderMode(11);
                    }
                    else
                    {
                        if (%item $= "portal zones nonRoot")
                        {
                            setInteriorRenderMode(17);
                        }
                        else
                        {
                            if (%item $= "zonesNonRoot, Detail")
                            {
                                setInteriorRenderMode(18);
                            }
                            else
                            {
                                if (%item $= "large textures")
                                {
                                    setInteriorRenderMode(12);
                                }
                                else
                                {
                                    if (%item $= "detail level")
                                    {
                                        setInteriorRenderMode(16);
                                    }
                                    else
                                    {
                                        if (%item $= "lightmap")
                                        {
                                            setInteriorRenderMode(5);
                                        }
                                        else
                                        {
                                            if (%item $= "only textures")
                                            {
                                                setInteriorRenderMode(6);
                                            }
                                            else
                                            {
                                                if (%item $= "triangle strips")
                                                {
                                                    setInteriorRenderMode(10);
                                                }
                                                else
                                                {
                                                    if (%item $= "prev mode")
                                                    {
                                                        interiorRenderModePrev();
                                                    }
                                                    else
                                                    {
                                                        if (%item $= "next mode")
                                                        {
                                                            interiorRenderModeNext();
                                                        }
                                                        else
                                                        {
                                                            setInteriorRenderMode(0);
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
    return ;
}
function EditorMenuBar::onSelectTypeMenuItemSelect(%this, %itemId, %item)
{
    if (getWords(%item, 0, 1) $= "Select all")
    {
        %classname = getWord(%item, 2);
        %classname = getSubStr(%classname, 0, strlen(%classname) - 1);
        EWorldEditor.selectAllObjectsOfClassName(%classname);
    }
    else
    {
        if (%item $= "All Types")
        {
            EWorldEditor.selectType = $TypeMasks::ALLTYPES;
        }
        else
        {
            if (%item $= "Triggers")
            {
                EWorldEditor.selectType = $TypeMasks::TriggerObjectType;
            }
            else
            {
                if (%item $= "Interiors")
                {
                    EWorldEditor.selectType = $TypeMasks::InteriorObjectType;
                }
                else
                {
                    if (%item $= "Audio Emitters")
                    {
                        EWorldEditor.selectType = $TypeMasks::MarkerObjectType;
                    }
                    else
                    {
                        if (%item $= "Shapes and Sit Markers")
                        {
                            EWorldEditor.selectType = $TypeMasks::ShapeBaseObjectType | $TypeMasks::StaticTSObjectType;
                        }
                        else
                        {
                            if (%item $= "Items")
                            {
                                EWorldEditor.selectType = $TypeMasks::ItemObjectType;
                            }
                            else
                            {
                                if (%item $= "Antiportals")
                                {
                                    EWorldEditor.selectType = $TypeMasks::AntiPortalObjectType;
                                }
                                else
                                {
                                    EWorldEditor.selectType = $TypeMasks::ALLTYPES;
                                }
                            }
                        }
                    }
                }
            }
        }
        EditorMenuBar.setMenuItemChecked("Select Type", %item, 1);
    }
    EStatusHud.updateStatus();
    return ;
}
function EditorMenuBar::onWorldMenuItemSelect(%this, %itemId, %item)
{
    if (%item $= "Lock Selection")
    {
        EWorldEditor.lockSelection(1);
    }
    else
    {
        if (%item $= "Unlock Selection")
        {
            EWorldEditor.lockSelection(0);
        }
        else
        {
            if (%item $= "Hide Selected")
            {
                EWorldEditor.hideSelection(1);
            }
            else
            {
                if (%item $= "Hide All But Selected")
                {
                    EWorldEditor.hideAllButSelection(1);
                }
                else
                {
                    if (%item $= "Unhide Selected")
                    {
                        EWorldEditor.hideSelection(0);
                    }
                    else
                    {
                        if (%item $= "Invert Hidden")
                        {
                            EWorldEditor.invertHiddenSelection();
                        }
                        else
                        {
                            if (%item $= "Reset Transforms")
                            {
                                EWorldEditor.resetTransforms();
                            }
                            else
                            {
                                if (%item $= "Drop Selection")
                                {
                                    EWorldEditor.dropSelection();
                                }
                                else
                                {
                                    if (%item $= "SimGroup Create")
                                    {
                                        ObjectBuilderGui.buildSimGroup();
                                    }
                                    else
                                    {
                                        if (%item $= "Delete Selection")
                                        {
                                            EWorldEditor.deleteSelection();
                                            inspector.uninspect();
                                        }
                                        else
                                        {
                                            if (%item $= "Add Selection to Instant Group")
                                            {
                                                EWorldEditor.addSelectionToAddGroup();
                                            }
                                            else
                                            {
                                                EditorMenuBar.setMenuItemChecked("World", %item, 1);
                                                if (%item $= "Drop at Origin")
                                                {
                                                    EWorldEditor.dropType = "atOrigin";
                                                }
                                                else
                                                {
                                                    if (%item $= "Drop at Camera")
                                                    {
                                                        EWorldEditor.dropType = "atCamera";
                                                    }
                                                    else
                                                    {
                                                        if (%item $= "Drop at Camera w/Rot")
                                                        {
                                                            EWorldEditor.dropType = "atCameraRot";
                                                        }
                                                        else
                                                        {
                                                            if (%item $= "Drop below Camera")
                                                            {
                                                                EWorldEditor.dropType = "belowCamera";
                                                            }
                                                            else
                                                            {
                                                                if (%item $= "Drop at Screen Center")
                                                                {
                                                                    EWorldEditor.dropType = "screenCenter";
                                                                }
                                                                else
                                                                {
                                                                    if (%item $= "Drop to Ground")
                                                                    {
                                                                        EWorldEditor.dropType = "toGround";
                                                                    }
                                                                    else
                                                                    {
                                                                        if (%item $= "Drop at Centroid")
                                                                        {
                                                                            EWorldEditor.dropType = "atCentroid";
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
    return ;
}
function EditorMenuBar::OnSnapToMenuItemSelect(%this, %itemId, %item)
{
    EWorldEditor.multiSnapTo(%item);
    return ;
}
function EditorMenuBar::OnCloneToMenuItemSelect(%this, %itemId, %item)
{
    EWorldEditor.CloneTo(%item);
    return ;
}
function EditorMenuBar::onEditMenuItemSelect(%this, %itemId, %item)
{
    if (%item $= "World Editor Settings...")
    {
        Canvas.pushDialog(WorldEditorSettingsDlg, 0);
    }
    else
    {
        if (%item $= "Terrain Editor Settings...")
        {
            Canvas.pushDialog(TerrainEditorValuesSettingsGui, 99);
        }
        else
        {
            if (%item $= "Relight Scene")
            {
                lightScene("", forceAlways);
            }
            else
            {
                if (%item $= "Increase Move Scale")
                {
                    EWorldEditor.increaseMoveScale();
                }
                else
                {
                    if (%item $= "Toggle Grid Visibility")
                    {
                        EWorldEditor.renderPlane = !EWorldEditor.renderPlane;
                        EWorldEditor.renderPlaneHashes = !EWorldEditor.renderPlaneHashes;
                    }
                    else
                    {
                        if (%item $= "Status Hud Toggle")
                        {
                            toggleStatusHud();
                        }
                        else
                        {
                            if (%item $= "Decrease Move Scale")
                            {
                                EWorldEditor.decreaseMoveScale();
                            }
                            else
                            {
                                if (EWorldEditor.isVisible())
                                {
                                    if (%item $= "Undo")
                                    {
                                        EWorldEditor.undo();
                                    }
                                    else
                                    {
                                        if (%item $= "Redo")
                                        {
                                            EWorldEditor.redo();
                                        }
                                        else
                                        {
                                            if (%item $= "Copy")
                                            {
                                                EWorldEditor.copySelection();
                                            }
                                            else
                                            {
                                                if (%item $= "Cut")
                                                {
                                                    EWorldEditor.copySelection();
                                                    EWorldEditor.deleteSelection();
                                                    inspector.uninspect();
                                                }
                                                else
                                                {
                                                    if (%item $= "Paste")
                                                    {
                                                        EWorldEditor.pasteSelection();
                                                    }
                                                    else
                                                    {
                                                        if (%item $= "Select All")
                                                        {
                                                            EWorldEditor.selectAllObjects();
                                                        }
                                                        else
                                                        {
                                                            if (%item $= "Select None")
                                                            {
                                                                EWorldEditor.clearSelection();
                                                            }
                                                            else
                                                            {
                                                                if (%item $= "Select Inverse")
                                                                {
                                                                    EWorldEditor.invertSelection();
                                                                }
                                                                else
                                                                {
                                                                    if (%item $= "Find Selected")
                                                                    {
                                                                        FindSelectedInEditorTree();
                                                                    }
                                                                    else
                                                                    {
                                                                        if (%item $= "Zoom Camera To Selection")
                                                                        {
                                                                            EWorldEditor.dropCameraToSelection();
                                                                        }
                                                                        else
                                                                        {
                                                                            if (%item $= "Expand Selected Tree")
                                                                            {
                                                                                ExpandSelectedInEditorTree();
                                                                            }
                                                                            else
                                                                            {
                                                                                if (%item $= "Expand And Select Selected Tree")
                                                                                {
                                                                                    ExpandSelectedAndSelectInEditorTree();
                                                                                }
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    if (ETerrainEditor.isVisible())
                                    {
                                        if (%item $= "Undo")
                                        {
                                            ETerrainEditor.undo();
                                        }
                                        else
                                        {
                                            if (%item $= "Redo")
                                            {
                                                ETerrainEditor.redo();
                                            }
                                            else
                                            {
                                                if (%item $= "Select None")
                                                {
                                                    ETerrainEditor.clearSelection();
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
    return ;
}
function EditorMenuBar::onToggleSGTools(%this, %itemId, %item)
{
    EditorGui.toggleSGTools(%item);
    return ;
}
function EditorMenuBar::onWindowMenuItemSelect(%this, %itemId, %item)
{
    EditorGui.setEditor(%item);
    return ;
}
function Creator::onWake(%this)
{
    Creator.init();
    return ;
}
function Creator::onSleep(%this)
{
    $LastEditorChosenInstantGroup = $instantGroup;
    return ;
}
function EditorGui::setWorldEditorVisible(%this)
{
    EWorldEditor.setVisible(1);
    ETerrainEditor.setVisible(0);
    EditorMenuBar.setMenuVisible("World", 1);
    EditorMenuBar.setMenuVisible("Action", 0);
    EditorMenuBar.setMenuVisible("Brush", 0);
    EWorldEditor.makeFirstResponder(1);
    EditorTree.open(MissionGroup, 1);
    return ;
}
function EditorGui::setTerrainEditorVisible(%this)
{
    EWorldEditor.setVisible(0);
    ETerrainEditor.setVisible(1);
    ETerrainEditor.attachTerrain();
    EHeightField.setVisible(0);
    ETexture.setVisible(0);
    EditorMenuBar.setMenuVisible("World", 0);
    EditorMenuBar.setMenuVisible("Action", 1);
    EditorMenuBar.setMenuVisible("Brush", 1);
    ETerrainEditor.makeFirstResponder(1);
    EPainter.setVisible(0);
    return ;
}
function EditorGui::toggleSGTools(%this, %item)
{
    if (%item $= $sgEditorItemNames::sgMenuItem[0])
    {
        sgLightEditor::toggle();
    }
    return ;
}
function EditorGui::setEditor(%this, %editor)
{
    EditorMenuBar.setMenuItemBitmap("Window", %this.currentEditor, -1);
    EditorMenuBar.setMenuItemBitmap("Window", %editor, 0);
    %this.currentEditor = %editor;
    if (%editor $= "World Editor")
    {
        EWFrame.setVisible(0);
        EWMissionArea.setVisible(0);
        %this.setWorldEditorVisible();
    }
    else
    {
        if (%editor $= "World Editor Inspector")
        {
            EWFrame.setVisible(1);
            EWMissionArea.setVisible(0);
            EWCreatorPane.setVisible(0);
            EWInspectorPane.setVisible(1);
            %this.setWorldEditorVisible();
        }
        else
        {
            if (%editor $= "World Editor Creator")
            {
                EWFrame.setVisible(1);
                EWMissionArea.setVisible(0);
                EWCreatorPane.setVisible(1);
                EWInspectorPane.setVisible(0);
                %this.setWorldEditorVisible();
            }
            else
            {
                if (%editor $= "Mission Area Editor")
                {
                    EWFrame.setVisible(0);
                    EWMissionArea.setVisible(1);
                    %this.setWorldEditorVisible();
                }
                else
                {
                    if (%editor $= "Terrain Editor")
                    {
                        %this.setTerrainEditorVisible();
                    }
                    else
                    {
                        if (%editor $= "Terrain Terraform Editor")
                        {
                            %this.setTerrainEditorVisible();
                            EHeightField.setVisible(1);
                        }
                        else
                        {
                            if (%editor $= "Terrain Texture Editor")
                            {
                                %this.setTerrainEditorVisible();
                                ETexture.setVisible(1);
                            }
                            else
                            {
                                if (%editor $= "Terrain Texture Painter")
                                {
                                    %this.setTerrainEditorVisible();
                                    EPainter.setVisible(1);
                                    EPainter.setup();
                                }
                            }
                        }
                    }
                }
            }
        }
    }
    return ;
}
function EditorGui::getHelpPage(%this)
{
    if (((%this.currentEditor $= "World Editor") || (%this.currentEditor $= "World Editor Inspector")) || (%this.currentEditor $= "World Editor Creator"))
    {
        return "5. World Editor";
    }
    else
    {
        if (%this.currentEditor $= "Mission Area Editor")
        {
            return "6. Mission Area Editor";
        }
        else
        {
            if (%this.currentEditor $= "Terrain Editor")
            {
                return "7. Terrain Editor";
            }
            else
            {
                if (%this.currentEditor $= "Terrain Terraform Editor")
                {
                    return "8. Terrain Terraform Editor";
                }
                else
                {
                    if (%this.currentEditor $= "Terrain Texture Editor")
                    {
                        return "9. Terrain Texture Editor";
                    }
                    else
                    {
                        if (%this.currentEditor $= "Terrain Texture Painter")
                        {
                            return "10. Terrain Texture Painter";
                        }
                    }
                }
            }
        }
    }
    return ;
}
function ETerrainEditor::setPaintMaterial(%this, %matIndex)
{
    ETerrainEditor.paintMaterial = EPainter.mat[%matIndex];
    return ;
}
function ETerrainEditor::changeMaterial(%this, %matIndex)
{
    EPainter.matIndex = %matIndex;
    getLoadFilename("*/terrains// .png\tterrains/*.jpg", EPainterChangeMat);
    return ;
}
function EPainterChangeMat(%file)
{
    %file = filePath(%file) @ "/" @ fileBase(%file);
    %i = 0;
    while (%i < 6)
    {
        if (EPainter.mat[%i] $= %file)
        {
            return ;
        }
        %i = %i + 1;
    }
    EPainter.mat[EPainter.matIndex] = %file;
    %mats = "";
    %i = 0;
    while (%i < 6)
    {
        %mats = %mats @ EPainter.mat[%i] @ "\n";
        %i = %i + 1;
    }
    ETerrainEditor.setTerrainMaterials(%mats);
    EPainter.setup();
    ("ETerrainMaterialPaint" @ EPainter.matIndex).performClick();
    return ;
}
function EPainter::setup(%this)
{
    EditorMenuBar.onActionMenuItemSelect(0, "Paint Material");
    %mats = ETerrainEditor.getTerrainMaterials();
    %valid = 1;
    %i = 0;
    while (%i < 6)
    {
        %mat = getRecord(%mats, %i);
        %this.mat[%i] = %mat;
        ("ETerrainMaterialText" @ %i).setText(fileBase(%mat));
        ("ETerrainMaterialBitmap" @ %i).setBitmap(%mat);
        ("ETerrainMaterialChange" @ %i).setActive(1);
        ("ETerrainMaterialPaint" @ %i).setActive(!(%mat $= ""));
        if (%mat $= "")
        {
            ("ETerrainMaterialChange" @ %i).setText("Add...");
            if (%valid)
            {
                %valid = 0;
            }
            else
            {
                ("ETerrainMaterialChange" @ %i).setActive(0);
            }
        }
        else
        {
            ("ETerrainMaterialChange" @ %i).setText("Change...");
        }
        %i = %i + 1;
    }
    ETerrainMaterialPaint0.performClick();
    return ;
}
function EditorGui::onWake(%this)
{
    moveMap.push();
    EditorMap.push();
    %this.setEditor(%this.currentEditor);
    return ;
}
function EditorGui::onSleep(%this)
{
    EditorMap.pop();
    moveMap.pop();
    return ;
}
function AreaEditor::onUpdate(%this, %area)
{
    AreaEditingText.setValue("X: " @ getWord(%area, 0) @ " Y: " @ getWord(%area, 1) @ " W: " @ getWord(%area, 2) @ " H: " @ getWord(%area, 3));
    return ;
}
function AreaEditor::onWorldOffset(%this, %unused)
{
    return ;
}
function RecurseInvertSelectObjectsInGroup(%theSimGroup)
{
    %count = %theSimGroup.getCount();
    %i = 0;
    while (%i < %count)
    {
        %object = %theSimGroup.getObject(%i);
        if (%object.isClassSimGroup())
        {
            RecurseInvertSelectObjectsInGroup(%object);
        }
        else
        {
            EWorldEditor.invertSelectObject(%object);
        }
        %i = %i + 1;
    }
}

function RecurseSelectObjectsInGroup(%theSimGroup, %classname)
{
    %count = %theSimGroup.getCount();
    %i = 0;
    while (%i < %count)
    {
        %object = %theSimGroup.getObject(%i);
        if (%object.isClassSimGroup())
        {
            RecurseSelectObjectsInGroup(%object, %classname);
        }
        else
        {
            if ((%classname $= "") && (%object.getClassName() $= %classname))
            {
                EWorldEditor.selectObject(%object);
            }
        }
        %i = %i + 1;
    }
}

function WorldEditor::selectAllObjects(%this)
{
    if (isObject(MissionGroup))
    {
        RecurseSelectObjectsInGroup(MissionGroup, "");
    }
    return ;
}
function WorldEditor::selectAllObjectsOfClassName(%this, %classname)
{
    if (isObject(MissionGroup))
    {
        RecurseSelectObjectsInGroup(MissionGroup, %classname);
    }
    return ;
}
function WorldEditor::invertSelection(%this)
{
    if (isObject(MissionGroup))
    {
        RecurseInvertSelectObjectsInGroup(MissionGroup);
    }
    return ;
}
function WorldEditor::increaseMoveScale(%this)
{
    %max = 10;
    EWorldEditor.mouseMoveScale = EWorldEditor.mouseMoveScale * 2;
    if (EWorldEditor.mouseMoveScale > %max)
    {
        EWorldEditor.mouseMoveScale = %max;
    }
    EditorGui.setPrefs();
    return ;
}
function WorldEditor::decreaseMoveScale(%this)
{
    %min = 0.001;
    EWorldEditor.mouseMoveScale = EWorldEditor.mouseMoveScale / 2;
    if (EWorldEditor.mouseMoveScale < %min)
    {
        EWorldEditor.mouseMoveScale = %min;
    }
    EditorGui.setPrefs();
    return ;
}
function WorldEditor::onDelete(%this)
{
    EditorTree.deleteSelection();
    inspector.uninspect();
    EStatusHud.updateStatus();
    EWorldEditor.updateGeneralInfo("");
    return ;
}
function WorldEditor::onSelect(%this, %obj)
{
    EditorTree.addSelection(%obj);
    EStatusHud.updateStatus();
    EWorldEditor.updateGeneralInfo("");
    return ;
}
function WorldEditor::onUnSelect(%this, %obj)
{
    EditorTree.removeSelection(%obj);
    inspector.uninspect();
    EStatusHud.updateStatus();
    EWorldEditor.updateGeneralInfo("");
    return ;
}
function WorldEditor::onClearSelected(%this)
{
    EditorTree.clearSelection();
    inspector.uninspect();
    EStatusHud.updateStatus();
    EWorldEditor.updateGeneralInfo("");
    return ;
}
function WorldEditor::onClearSelection(%this)
{
    EditorTree.clearSelection();
    inspector.uninspect();
    EStatusHud.updateStatus();
    EWorldEditor.updateGeneralInfo("");
    return ;
}
function EditorTree::onDragDrop(%this)
{
    EditorTree.isDirty = 1;
    return ;
}
function EditorTree::onObjectDeleteCompleted(%this)
{
    EditorTree.isDirty = 1;
    EWorldEditor.copySelection();
    EWorldEditor.deleteSelection();
    inspector.uninspect();
    EStatusHud.updateStatus();
    EWorldEditor.updateGeneralInfo("");
    return ;
}
function EditorTree::onClearSelected(%this)
{
    WorldEditor.clearSelection();
    return ;
}
function EditorTree::init(%this)
{
    new GuiControl(ETContextPopupDlg)
    {
        profile = "GuiModelessDialogProfile";
        horizSizing = "width";
        vertSizing = "height";
        position = "0 0";
        extent = "640 480";
        minExtent = "8 8";
        visible = 1;
        setFirstResponder = 0;
        modal = 1;
    };
    ETContextPopup.setVisible(0);
    return ;
}
function EditorTree::OnInspect(%this, %obj)
{
    inspector.inspect(%obj);
    InspectorNameEdit.setValue(%obj.getName());
    EWorldEditor.updateGeneralInfo(%obj);
    return ;
}
function EditorTree::onAddSelection(%this, %obj)
{
    if ($AIEdit)
    {
        aiEdit.selectObject(%obj);
    }
    else
    {
        EWorldEditor.selectObject(%obj);
    }
    %obj.isNetCacheable = TEST_MISSIONGROUPINTEGRITY.getInitialNetCacheable(%obj);
    return ;
}
function EditorTree::onRemoveSelection(%this, %obj)
{
    if ($AIEdit)
    {
        aiEdit.selectObject(%obj);
    }
    else
    {
        EWorldEditor.unselectObject(%obj);
    }
    return ;
}
function EditorTree::onSelect(%this, %obj)
{
    EWorldEditor.clearSelection();
    if (%obj.isClassSimGroup())
    {
        inspector.inspect(%obj);
        InspectorNameEdit.setValue(%obj.getName());
        $userPref::Editor::autoSelectGroupContents = $userPref::Editor::autoSelectGroupContents;
        if ($userPref::Editor::autoSelectGroupContents)
        {
            RecurseSelectObjectsInGroup(%obj, "");
        }
    }
    if ($AIEdit)
    {
        aiEdit.selectObject(%obj);
    }
    else
    {
        EWorldEditor.selectObject(%obj);
    }
    return ;
}
function EditorTree::onUnSelect(%this, %obj)
{
    if ($AIEdit)
    {
        aiEdit.unselectObject(%obj);
    }
    else
    {
        EWorldEditor.unselectObject(%obj);
    }
    return ;
}
function ETContextPopup::onSelect(%this, %index, %unused)
{
    if (%index == 0)
    {
        EditorTree.contextObj.delete();
    }
    return ;
}
function WorldEditor::init(%this)
{
    %this.ignoreObjClass(TerrainBlock, Sky, AIObjective);
    %this.numEditModes = 3;
    %this.editMode[0] = "move";
    %this.editMode[1] = "rotate";
    %this.editMode[2] = "scale";
    new GuiControl(WEContextPopupDlg)
    {
        profile = "GuiModelessDialogProfile";
        horizSizing = "width";
        vertSizing = "height";
        position = "0 0";
        extent = "640 480";
        minExtent = "8 8";
        visible = 1;
        setFirstResponder = 0;
        modal = 1;
    };
    WEContextPopup.setVisible(0);
    return ;
}
function WorldEditor::onDblClick(%this, %obj)
{
    return ;
}
function WorldEditor::onClick(%this, %obj)
{
    EStatusHud.updateStatus();
    EWorldEditor.updateGeneralInfo("");
    inspector.inspect(%obj);
    InspectorNameEdit.setValue(%obj.getName());
    return ;
}
function WorldEditor::onEndDrag(%this, %obj)
{
    EStatusHud.updateStatus();
    inspector.inspect(%obj);
    InspectorNameEdit.setValue(%obj.getName());
    return ;
}
function WorldEditor::export(%this)
{
    getSaveFilename("~/editor/*.mac", %this @ ".doExport", "selection.mac");
    return ;
}
function WorldEditor::doExport(%this, %file)
{
    MissionGroup.save("~/editor/" @ %file, 1);
    return ;
}
function WorldEditor::import(%this)
{
    getLoadFilename("~/editor/*.mac", %this @ ".doImport");
    return ;
}
function WorldEditor::doImport(%this, %file)
{
    exec("~/editor/" @ %file);
    return ;
}
function WorldEditor::onGuiUpdate(%this, %text)
{
    return ;
}
function WorldEditor::getSelectionLockCount(%this)
{
    %ret = 0;
    %i = 0;
    while (%i < %this.getSelectionSize())
    {
        %obj = %this.getSelectedObject(%i);
        if (%obj.locked $= "true")
        {
            %ret = %ret + 1;
        }
        %i = %i + 1;
    }
    return %ret;
}
function WorldEditor::getSelectionHiddenCount(%this)
{
    %ret = 0;
    %i = %this.getSelectionSize() - 1;
    while (%i >= 0)
    {
        %obj = %this.getSelectedObject(%i);
        if (%obj.noShow)
        {
            %ret = %ret + 1;
        }
        %i = %i - 1;
    }
    return %ret;
}
function WorldEditor::snapTo(%this, %snapType, %objTarget, %objToSnap)
{
    if (%objTarget $= "")
    {
        %objTarget = %this.getSelectedObject(0);
    }
    if (%objToSnap $= "")
    {
        %objToSnap = %this.getSelectedObject(%this.getSelectionSize() - 1);
    }
    if ((%objTarget $= "") && (%objToSnap $= ""))
    {
        error("Please select two objects before selecting a Snap To funciton.");
        return ;
    }
    if (%snapType $= "X")
    {
        %this.snapToX(%objTarget, %objToSnap);
    }
    else
    {
        if (%snapType $= "X-")
        {
            %this.snapToXNeg(%objTarget, %objToSnap);
        }
        else
        {
            if (%snapType $= "X+")
            {
                %this.snapToXPos(%objTarget, %objToSnap);
            }
            else
            {
                if (%snapType $= "Y")
                {
                    %this.snapToY(%objTarget, %objToSnap);
                }
                else
                {
                    if (%snapType $= "Y-")
                    {
                        %this.snapToYNeg(%objTarget, %objToSnap);
                    }
                    else
                    {
                        if (%snapType $= "Y+")
                        {
                            %this.snapToYPos(%objTarget, %objToSnap);
                        }
                        else
                        {
                            if (%snapType $= "Z")
                            {
                                %this.snapToZ(%objTarget, %objToSnap);
                            }
                            else
                            {
                                if (%snapType $= "Z-")
                                {
                                    %this.snapToZNeg(%objTarget, %objToSnap);
                                }
                                else
                                {
                                    if (%snapType $= "Z+")
                                    {
                                        %this.snapToZPos(%objTarget, %objToSnap);
                                    }
                                    else
                                    {
                                        if (%snapType $= "X+YZ")
                                        {
                                            %this.snapToXPosYZ(%objTarget, %objToSnap);
                                        }
                                        else
                                        {
                                            if (%snapType $= "X-YZ")
                                            {
                                                %this.snapToXNegYZ(%objTarget, %objToSnap);
                                            }
                                            else
                                            {
                                                if (%snapType $= "XY+Z")
                                                {
                                                    %this.snapToXYPosZ(%objTarget, %objToSnap);
                                                }
                                                else
                                                {
                                                    if (%snapType $= "XY-Z")
                                                    {
                                                        %this.snapToXYNegZ(%objTarget, %objToSnap);
                                                    }
                                                    else
                                                    {
                                                        if (%snapType $= "XYZ+")
                                                        {
                                                            %this.snapToXYZPos(%objTarget, %objToSnap);
                                                        }
                                                        else
                                                        {
                                                            if (%snapType $= "XYZ-")
                                                            {
                                                                %this.snapToXYZNeg(%objTarget, %objToSnap);
                                                            }
                                                            else
                                                            {
                                                                if (%snapType $= "ObjX+YZ")
                                                                {
                                                                    %this.snapToObjXPosYZ(%objTarget, %objToSnap);
                                                                }
                                                                else
                                                                {
                                                                    if (%snapType $= "ObjX-YZ")
                                                                    {
                                                                        %this.snapToObjXNegYZ(%objTarget, %objToSnap);
                                                                    }
                                                                    else
                                                                    {
                                                                        if (%snapType $= "ObjXY+Z")
                                                                        {
                                                                            %this.snapToObjXYPosZ(%objTarget, %objToSnap);
                                                                        }
                                                                        else
                                                                        {
                                                                            if (%snapType $= "ObjXY-Z")
                                                                            {
                                                                                %this.snapToObjXYNegZ(%objTarget, %objToSnap);
                                                                            }
                                                                            else
                                                                            {
                                                                                if (%snapType $= "ObjXYZ+")
                                                                                {
                                                                                    %this.snapToObjXYZPos(%objTarget, %objToSnap);
                                                                                }
                                                                                else
                                                                                {
                                                                                    if (%snapType $= "ObjXYZ-")
                                                                                    {
                                                                                        %this.snapToObjXYZNeg(%objTarget, %objToSnap);
                                                                                    }
                                                                                }
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
    return ;
}
function WorldEditor::snapToX(%this, %objTarget, %objToSnap)
{
    %objToSnap.setTransform(setWord(%objToSnap.getTransform(), 0, getWord(%objTarget.getTransform(), 0) + EWorldEditor.snapGapX));
    return ;
}
function WorldEditor::snapToXPos(%this, %objTarget, %objToSnap)
{
    %edgeOffset = mAbs(getWord(%objToSnap.getWorldBox(), 0) - getWord(%objToSnap.getTransform(), 0));
    %transformWithOffset = getWord(%objTarget.getWorldBox(), 3) + %edgeOffset;
    %objToSnap.setTransform(setWord(%objToSnap.getTransform(), 0, %transformWithOffset));
    return ;
}
function WorldEditor::snapToXNeg(%this, %objTarget, %objToSnap)
{
    %edgeOffset = mAbs(getWord(%objToSnap.getWorldBox(), 3) - getWord(%objToSnap.getTransform(), 0));
    %transformWithOffset = getWord(%objTarget.getWorldBox(), 0) - %edgeOffset;
    %objToSnap.setTransform(setWord(%objToSnap.getTransform(), 0, %transformWithOffset));
    return ;
}
function WorldEditor::snapToY(%this, %objTarget, %objToSnap)
{
    %objToSnap.setTransform(setWord(%objToSnap.getTransform(), 1, getWord(%objTarget.getTransform(), 1) + EWorldEditor.snapGapY));
    return ;
}
function WorldEditor::snapToYPos(%this, %objTarget, %objToSnap)
{
    %edgeOffset = mAbs(getWord(%objToSnap.getWorldBox(), 1) - getWord(%objToSnap.getTransform(), 1));
    %transformWithOffset = getWord(%objTarget.getWorldBox(), 4) + %edgeOffset;
    %objToSnap.setTransform(setWord(%objToSnap.getTransform(), 1, %transformWithOffset));
    return ;
}
function WorldEditor::snapToYNeg(%this, %objTarget, %objToSnap)
{
    %edgeOffset = mAbs(getWord(%objToSnap.getWorldBox(), 4) - getWord(%objToSnap.getTransform(), 1));
    %transformWithOffset = getWord(%objTarget.getWorldBox(), 1) - %edgeOffset;
    %objToSnap.setTransform(setWord(%objToSnap.getTransform(), 1, %transformWithOffset));
    return ;
}
function WorldEditor::snapToZ(%this, %objTarget, %objToSnap)
{
    %objToSnap.setTransform(setWord(%objToSnap.getTransform(), 2, getWord(%objTarget.getTransform(), 2) + EWorldEditor.snapGapZ));
    return ;
}
function WorldEditor::snapToZPos(%this, %objTarget, %objToSnap)
{
    %edgeOffset = mAbs(getWord(%objToSnap.getWorldBox(), 2) - getWord(%objToSnap.getTransform(), 2));
    %transformWithOffset = getWord(%objTarget.getWorldBox(), 5) + %edgeOffset;
    %objToSnap.setTransform(setWord(%objToSnap.getTransform(), 2, %transformWithOffset));
    return ;
}
function WorldEditor::snapToZNeg(%this, %objTarget, %objToSnap)
{
    %edgeOffset = mAbs(getWord(%objToSnap.getWorldBox(), 5) - getWord(%objToSnap.getTransform(), 2));
    %transformWithOffset = getWord(%objTarget.getWorldBox(), 2) - %edgeOffset;
    %objToSnap.setTransform(setWord(%objToSnap.getTransform(), 2, %transformWithOffset));
    return ;
}
function WorldEditor::snapToXPosYZ(%this, %objTarget, %objToSnap)
{
    %this.snapToXPos(%objTarget, %objToSnap);
    %this.snapToY(%objTarget, %objToSnap);
    %this.snapToZ(%objTarget, %objToSnap);
    return ;
}
function WorldEditor::snapToXNegYZ(%this, %objTarget, %objToSnap)
{
    %this.snapToXNeg(%objTarget, %objToSnap);
    %this.snapToY(%objTarget, %objToSnap);
    %this.snapToZ(%objTarget, %objToSnap);
    return ;
}
function WorldEditor::snapToXYPosZ(%this, %objTarget, %objToSnap)
{
    %this.snapToX(%objTarget, %objToSnap);
    %this.snapToYPos(%objTarget, %objToSnap);
    %this.snapToZ(%objTarget, %objToSnap);
    return ;
}
function WorldEditor::snapToXYNegZ(%this, %objTarget, %objToSnap)
{
    %this.snapToX(%objTarget, %objToSnap);
    %this.snapToYNeg(%objTarget, %objToSnap);
    %this.snapToZ(%objTarget, %objToSnap);
    return ;
}
function WorldEditor::snapToXYZPos(%this, %objTarget, %objToSnap)
{
    %this.snapToX(%objTarget, %objToSnap);
    %this.snapToY(%objTarget, %objToSnap);
    %this.snapToZPos(%objTarget, %objToSnap);
    return ;
}
function WorldEditor::snapToXYZNeg(%this, %objTarget, %objToSnap)
{
    %this.snapToX(%objTarget, %objToSnap);
    %this.snapToY(%objTarget, %objToSnap);
    %this.snapToZNeg(%objTarget, %objToSnap);
    return ;
}
function WorldEditor::snapToObjXPosYZ(%this, %objTarget, %objToSnap)
{
    %edgeOffset = mAbs(getWord(%objToSnap.getObjectBox(), 3)) + mAbs(getWord(%objTarget.getObjectBox(), 0));
    %worldTransform = %objTarget.getWorldTransform();
    %worldTransform = setWord(%worldTransform, 0, getWord(%worldTransform, 0) + %edgeOffset);
    %offsetMatrix = MatrixMultiply(%objTarget.getTransform(), %worldTransform);
    %newTransform = %objToSnap.getTransform();
    %newTransform = setWord(%newTransform, 0, getWord(%objTarget.getTransform(), 0) + getWord(%offsetMatrix, 0));
    %newTransform = setWord(%newTransform, 1, getWord(%objTarget.getTransform(), 1) + getWord(%offsetMatrix, 1));
    %newTransform = setWord(%newTransform, 2, getWord(%objTarget.getTransform(), 2) + getWord(%offsetMatrix, 2));
    %objToSnap.setTransform(%newTransform);
    return ;
}
function WorldEditor::snapToObjXNegYZ(%this, %objTarget, %objToSnap)
{
    %edgeOffset = mAbs(getWord(%objToSnap.getObjectBox(), 0)) + mAbs(getWord(%objTarget.getObjectBox(), 3));
    %worldTransform = %objTarget.getWorldTransform();
    %worldTransform = setWord(%worldTransform, 0, getWord(%worldTransform, 0) - %edgeOffset);
    %offsetMatrix = MatrixMultiply(%objTarget.getTransform(), %worldTransform);
    %newTransform = %objToSnap.getTransform();
    %newTransform = setWord(%newTransform, 0, getWord(%objTarget.getTransform(), 0) + getWord(%offsetMatrix, 0));
    %newTransform = setWord(%newTransform, 1, getWord(%objTarget.getTransform(), 1) + getWord(%offsetMatrix, 1));
    %newTransform = setWord(%newTransform, 2, getWord(%objTarget.getTransform(), 2) + getWord(%offsetMatrix, 2));
    %objToSnap.setTransform(%newTransform);
    return ;
}
function WorldEditor::snapToObjXYPosZ(%this, %objTarget, %objToSnap)
{
    %edgeOffset = mAbs(getWord(%objToSnap.getObjectBox(), 4)) + mAbs(getWord(%objTarget.getObjectBox(), 1));
    %worldTransform = %objTarget.getWorldTransform();
    %worldTransform = setWord(%worldTransform, 1, getWord(%worldTransform, 1) + %edgeOffset);
    %offsetMatrix = MatrixMultiply(%objTarget.getTransform(), %worldTransform);
    %newTransform = %objToSnap.getTransform();
    %newTransform = setWord(%newTransform, 0, getWord(%objTarget.getTransform(), 0) + getWord(%offsetMatrix, 0));
    %newTransform = setWord(%newTransform, 1, getWord(%objTarget.getTransform(), 1) + getWord(%offsetMatrix, 1));
    %newTransform = setWord(%newTransform, 2, getWord(%objTarget.getTransform(), 2) + getWord(%offsetMatrix, 2));
    %objToSnap.setTransform(%newTransform);
    return ;
}
function WorldEditor::snapToObjXYNegZ(%this, %objTarget, %objToSnap)
{
    %edgeOffset = mAbs(getWord(%objToSnap.getObjectBox(), 1)) + mAbs(getWord(%objTarget.getObjectBox(), 4));
    %worldTransform = %objTarget.getWorldTransform();
    %worldTransform = setWord(%worldTransform, 1, getWord(%worldTransform, 1) - %edgeOffset);
    %offsetMatrix = MatrixMultiply(%objTarget.getTransform(), %worldTransform);
    %newTransform = %objToSnap.getTransform();
    %newTransform = setWord(%newTransform, 0, getWord(%objTarget.getTransform(), 0) + getWord(%offsetMatrix, 0));
    %newTransform = setWord(%newTransform, 1, getWord(%objTarget.getTransform(), 1) + getWord(%offsetMatrix, 1));
    %newTransform = setWord(%newTransform, 2, getWord(%objTarget.getTransform(), 2) + getWord(%offsetMatrix, 2));
    %objToSnap.setTransform(%newTransform);
    return ;
}
function WorldEditor::snapToObjXYZPos(%this, %objTarget, %objToSnap)
{
    %edgeOffset = mAbs(getWord(%objToSnap.getObjectBox(), 5)) + mAbs(getWord(%objTarget.getObjectBox(), 2));
    %worldTransform = %objTarget.getWorldTransform();
    %worldTransform = setWord(%worldTransform, 2, getWord(%worldTransform, 2) + %edgeOffset);
    %offsetMatrix = MatrixMultiply(%objTarget.getTransform(), %worldTransform);
    %newTransform = %objToSnap.getTransform();
    %newTransform = setWord(%newTransform, 0, getWord(%objTarget.getTransform(), 0) + getWord(%offsetMatrix, 0));
    %newTransform = setWord(%newTransform, 1, getWord(%objTarget.getTransform(), 1) + getWord(%offsetMatrix, 1));
    %newTransform = setWord(%newTransform, 2, getWord(%objTarget.getTransform(), 2) + getWord(%offsetMatrix, 2));
    %objToSnap.setTransform(%newTransform);
    return ;
}
function WorldEditor::snapToObjXYZNeg(%this, %objTarget, %objToSnap)
{
    %edgeOffset = mAbs(getWord(%objToSnap.getObjectBox(), 2)) + mAbs(getWord(%objTarget.getObjectBox(), 5));
    %worldTransform = %objTarget.getWorldTransform();
    %worldTransform = setWord(%worldTransform, 2, getWord(%worldTransform, 2) - %edgeOffset);
    %offsetMatrix = MatrixMultiply(%objTarget.getTransform(), %worldTransform);
    %newTransform = %objToSnap.getTransform();
    %newTransform = setWord(%newTransform, 0, getWord(%objTarget.getTransform(), 0) + getWord(%offsetMatrix, 0));
    %newTransform = setWord(%newTransform, 1, getWord(%objTarget.getTransform(), 1) + getWord(%offsetMatrix, 1));
    %newTransform = setWord(%newTransform, 2, getWord(%objTarget.getTransform(), 2) + getWord(%offsetMatrix, 2));
    %objToSnap.setTransform(%newTransform);
    return ;
}
function WorldEditor::CloneTo(%this, %snapType)
{
    %selSize = %this.getSelectionSize();
    %i = 0;
    while (%i < %selSize)
    {
        %origObjects[%i] = %this.getSelectedObject(%i) ;
        %i = %i + 1;
    }
    %i = 0;
    while (%i < %selSize)
    {
        %this.clearSelection();
        %objTarget = %origObjects[%i];
        %this.selectObject(%objTarget);
        %this.copySelection();
        %this.pasteSelection();
        %objToSnap = %this.getSelectedObject(0);
        %newObjects[%i] = %objToSnap ;
        %this.snapTo(%snapType, %objTarget, %objToSnap);
        %i = %i + 1;
    }
    %this.clearSelection();
    %i = 0;
    while (%i < %selSize)
    {
        %this.selectObject(%newObjects[%i]);
        %i = %i + 1;
    }
}

function WorldEditor::multiSnapTo(%this, %snapType)
{
    echo("in multiSnapTo w/ type" SPC %snapType);
    %selSize = %this.getSelectionSize();
    %objTarget = %this.getSelectedObject(%this.getSelectionSize() - 1);
    %i = 0;
    while (%i < (%selSize - 1))
    {
        %objToSnap = %this.getSelectedObject(%i);
        %this.snapTo(%snapType, %objTarget, %objToSnap);
        %i = %i + 1;
    }
}

function WorldEditor::dropCameraToSelection(%this)
{
    if (%this.getSelectionSize() == 0)
    {
        return ;
    }
    %pos = %this.getSelectionCentroid();
    %cam = LocalClientConnection.Camera.getTransform();
    %cam = setWord(%cam, 0, getWord(%pos, 0));
    %cam = setWord(%cam, 1, getWord(%pos, 1));
    %cam = setWord(%cam, 2, getWord(%pos, 2));
    LocalClientConnection.Camera.setTransform(%cam);
    %control = LocalClientConnection.getControlObject();
    if (%control != LocalClientConnection.Camera)
    {
        toggleCamera();
    }
    return ;
}
function WorldEditor::dropCameraWithSelectionInView(%this)
{
    if (%this.getSelectionSize() == 0)
    {
        return ;
    }
    %curCam = LocalClientConnection.getControlObject();
    %camera = LocalClientConnection.Camera;
    %pos = %this.getSelectionBoxCentroid();
    %rad = %this.getSelectionBoxRadius();
    %rad = %rad * 1.5;
    %fov = mDegToRad(getFovCur());
    %eyeDir = %curCam.getEyeVector();
    %camPosition = fitCameraConeAroundSphere(%pos, %rad, %eyeDir, %fov);
    %camTransform = %curCam.getEyeTransform();
    %camTransform = setWord(%camTransform, 0, getWord(%camPosition, 0));
    %camTransform = setWord(%camTransform, 1, getWord(%camPosition, 1));
    %camTransform = setWord(%camTransform, 2, getWord(%camPosition, 2));
    %camera.setTransform(%camTransform);
    if (%curCam != %camera)
    {
        toggleCamera();
    }
    return ;
}
function WorldEditor::moveSelectionInPlace(%this)
{
    %saveDropType = %this.dropType;
    %this.dropType = "atCentroid";
    %this.copySelection();
    %this.deleteSelection();
    %this.pasteSelection();
    %this.dropType = %saveDropType;
    return ;
}
function WorldEditor::addSelectionToAddGroup(%this)
{
    %i = 0;
    while (%i < %this.getSelectionSize())
    {
        %obj = %this.getSelectedObject(%i);
        $instantGroup.add(%obj);
        %i = %i + 1;
    }
}

function WorldEditor::resetTransforms(%this)
{
    %this.addUndoState();
    %i = 0;
    while (%i < %this.getSelectionSize())
    {
        %obj = %this.getSelectedObject(%i);
        %transform = %obj.getTransform();
        %transform = setWord(%transform, 3, 0);
        %transform = setWord(%transform, 4, 0);
        %transform = setWord(%transform, 5, 1);
        %transform = setWord(%transform, 6, 0);
        %obj.setTransform(%transform);
        %obj.setScale("1 1 1");
        %i = %i + 1;
    }
}

function WorldEditorToolbarDlg::init(%this)
{
    WorldEditorInspectorCheckBox.setValue(WorldEditorToolFrameSet.isMember("EditorToolInspectorGui"));
    WorldEditorMissionAreaCheckBox.setValue(WorldEditorToolFrameSet.isMember("EditorToolMissionAreaGui"));
    WorldEditorTreeCheckBox.setValue(WorldEditorToolFrameSet.isMember("EditorToolTreeViewGui"));
    WorldEditorCreatorCheckBox.setValue(WorldEditorToolFrameSet.isMember("EditorToolCreatorGui"));
    return ;
}
$LastEditorChosenInstantGroup = 0;
function Creator::init(%this)
{
    if (isObject($LastEditorChosenInstantGroup))
    {
        if ($LastEditorChosenInstantGroup.isClassSimGroup())
        {
            $instantGroup = $LastEditorChosenInstantGroup;
        }
        else
        {
            $instantGroup = "MissionGroup";
        }
    }
    else
    {
        $instantGroup = "MissionGroup";
    }
    %base = %this.insertItem(0, "Interiors");
    %interiorId = "";
    %file = findFirstFile("*.dif");
    echo(" Creator::init  loading interiors");
    while (!(%file $= ""))
    {
        %split = strreplace(%file, "/", " ");
        %dirCount = getWordCount(%split) - 1;
        %parentId = %base;
        %i = 0;
        while (%i < %dirCount)
        {
            %parent = getWords(%split, 0, %i);
            if (!%interiorId[%parent])
            {
                %interiorId[%parent] = %this.insertItem(%parentId, getWord(%split, %i)) ;
            }
            %parentId = %interiorId[%parent];
            %i = %i + 1;
        }
        %create = "createInterior(" @ "\"" @ %file @ "\"" @ ");";
        %this.insertItem(%parentId, fileBase(%file), %create, "Interior");
        %file = findNextFile("*.dif");
    }
    echo(" Creator::init  loading shapes");
    %base = %this.insertItem(0, "Shapes");
    %dataGroup = "DataBlockGroup";
    %i = 0;
    while (%i < %dataGroup.getCount())
    {
        %obj = %dataGroup.getObject(%i);
        echo("Obj: " @ %obj.getName() @ " - " @ %obj.category);
        if (!((%obj.category $= "")) && (%obj.category != 0))
        {
            %id = %this.findItemByName(%obj.category);
            if (%id == 0)
            {
                %grp = %this.insertItem(%base, %obj.category);
                %this.insertItem(%grp, %obj.getName(), %obj.getClassName() @ "::create(" @ %obj.getName() @ ");", "Item");
            }
            else
            {
                %this.insertItem(%id, %obj.getName(), %obj.getClassName() @ "::create(" @ %obj.getName() @ ");", "Item");
            }
        }
        %i = %i + 1;
    }
    echo(" Creator::init  loading static shapes");
    %base = %this.insertItem(0, "Static Shapes");
    %staticId = "";
    %file = findFirstFile("*.dts");
    while (!(%file $= ""))
    {
        %split = strreplace(%file, "/", " ");
        %dirCount = getWordCount(%split) - 1;
        %parentId = %base;
        %i = 0;
        while (%i < %dirCount)
        {
            %parent = getWords(%split, 0, %i);
            if (!%staticId[%parent])
            {
                %staticId[%parent] = %this.insertItem(%parentId, getWord(%split, %i)) ;
            }
            %parentId = %staticId[%parent];
            %i = %i + 1;
        }
        %create = "TSStatic::create(\"" @ %file @ "\");";
        %this.insertItem(%parentId, fileBase(%file), %create, "TSStatic");
        %file = findNextFile("*.dts");
    }
    %base = %this.insertItem(0, "Dynamic Shapes");
    %dynamicID = "";
    %file = findFirstFile("*.dts");
    while (!(%file $= ""))
    {
        %split = strreplace(%file, "/", " ");
        %dirCount = getWordCount(%split) - 1;
        %parentId = %base;
        %i = 0;
        while (%i < %dirCount)
        {
            %parent = getWords(%split, 0, %i);
            if (!%dynamicID[%parent])
            {
                %dynamicID[%parent] = %this.insertItem(%parentId, getWord(%split, %i)) ;
            }
            %parentId = %dynamicID[%parent];
            %i = %i + 1;
        }
        %create = "TSDynamic::create(\"" @ %file @ "\");";
        %this.insertItem(%parentId, fileBase(%file), %create, "TSDynamic");
        %file = findNextFile("*.dts");
    }
    %objGroup[0] = "Environment";
    %objGroup[1] = "Mission";
    %objGroup[2] = "System";
    %env_item_idx = -1;
    %Environment_Item[%env_item_idx = %env_item_idx + 1] = "Sky";
    %Environment_Item[%env_item_idx = %env_item_idx + 1] = "Sun";
    %Environment_Item[%env_item_idx = %env_item_idx + 1] = "Lightning";
    %Environment_Item[%env_item_idx = %env_item_idx + 1] = "Water";
    %Environment_Item[%env_item_idx = %env_item_idx + 1] = "Terrain";
    %Environment_Item[%env_item_idx = %env_item_idx + 1] = "AudioEmitter";
    %Environment_Item[%env_item_idx = %env_item_idx + 1] = "Precipitation";
    %Environment_Item[%env_item_idx = %env_item_idx + 1] = "ParticleEmitter";
    %Environment_Item[%env_item_idx = %env_item_idx + 1] = "fxSunLight";
    %Environment_Item[%env_item_idx = %env_item_idx + 1] = "fxShapeReplicator";
    %Environment_Item[%env_item_idx = %env_item_idx + 1] = "fxFoliageReplicator";
    %Environment_Item[%env_item_idx = %env_item_idx + 1] = "fxLight";
    %Environment_Item[%env_item_idx = %env_item_idx + 1] = "TSText";
    %Environment_Item[%env_item_idx = %env_item_idx + 1] = "sgUniversalStaticLight";
    %Environment_Item[%env_item_idx = %env_item_idx + 1] = "sgMissionLightingFilter";
    %Environment_Item[%env_item_idx = %env_item_idx + 1] = "sgDecalProjector";
    %Environment_Item[%env_item_idx = %env_item_idx + 1] = "volumeLight";
    if (isFunction("Using_DF") && Using_DF())
    {
        %Environment_Item[%env_item_idx = %env_item_idx + 1] = "DFTextureAdvert";
    }
    if (Using_DShow())
    {
        %Environment_Item[%env_item_idx = %env_item_idx + 1] = "DSRenderer";
    }
    if (Using_Theora())
    {
        %Environment_Item[%env_item_idx = %env_item_idx + 1] = "TheoraRenderer";
    }
    if (Using_FFMPEG())
    {
        %Environment_Item[%env_item_idx = %env_item_idx + 1] = "FFMPEGRenderer";
    }
    %Environment_Item[%env_item_idx = %env_item_idx + 1] = "SlaveRenderer";
    %Mission_Item[0] = "MissionArea";
    %Mission_Item[1] = "Path";
    %Mission_Item[2] = "PathMarker";
    %Mission_Item[3] = "Trigger";
    %Mission_Item[4] = "PhysicalZone";
    %Mission_Item[5] = "Camera";
    %Mission_Item[6] = "AntiPortal";
    %Mission_Item[6] = "ZoneBox";
    %System_Item[0] = "SimGroup";
    %System_Item[1] = "SimSpace";
    echo(" Creator::init  loading mission objects");
    %base = %this.insertItem(0, "Mission Objects");
    %i = 0;
    while (!(%objGroup[%i] $= ""))
    {
        %grp = %this.insertItem(%base, %objGroup[%i]);
        %groupTag = "%" @ %objGroup[%i] @ "_Item";
        %done = 0;
        %j = 0;
        while (!%done)
        {
            eval("%itemTag = " @ %groupTag @ %j @ ";");
            if (%itemTag $= "")
            {
                %done = 1;
            }
            else
            {
                %this.insertItem(%grp, %itemTag, "ObjectBuilderGui.build" @ %itemTag @ "();", %itemTag);
            }
            %j = %j + 1;
        }
        %i = %i + 1;
    }
    echo(" Creator::init  finished");
    return ;
}
function createInterior(%name)
{
    %obj = new InteriorInstance()
    {
        position = "0 0 0";
        rotation = "0 0 0";
        interiorFile = %name;
    };
    %obj.isNetCacheable = TEST_MISSIONGROUPINTEGRITY.getInitialNetCacheable(%obj);
    return %obj;
}
function WorldEditor::onAddSelected(%this, %obj)
{
    EditorTree.addSelection(%obj);
    return ;
}
function Creator::onSelect(%this)
{
    Creator.clearSelection();
    return ;
}
function Creator::OnInspect(%this, %obj)
{
    if (!$missionRunning)
    {
        return ;
    }
    %objId = eval(%this.getItemValue(%obj));
    Creator.removeSelection(%obj);
    EditorTree.clearSelection();
    EWorldEditor.clearSelection();
    EWorldEditor.selectObject(%objId);
    EWorldEditor.dropSelection();
    return ;
}
function ExpandSelectedInEditorTree()
{
    %id = EditorTree.getSelectedItem();
    if (%id != -1)
    {
        EditorTree.expandAllChildren(%id);
    }
    else
    {
        echo("nothing selected");
    }
    return ;
}
function SelectRecursively(%itemId)
{
    %obj = EditorTree.getItemValue(%itemId);
    if (isObject(%obj))
    {
        EWorldEditor.selectObject(%obj);
    }
    %child = EditorTree.getChild(%itemId);
    if (%child)
    {
        SelectRecursively(%child);
    }
    %sibling = EditorTree.getNextSibling(%itemId);
    if (%sibling)
    {
        SelectRecursively(%sibling);
    }
    return ;
}
function ExpandSelectedAndSelectInEditorTree()
{
    %obj = EditorTree.getSelectedObject();
    %id = EditorTree.getSelectedItem();
    if (%id != -1)
    {
        EditorTree.expandAllChildren(%id);
        if (isObject(%obj))
        {
            if (%obj.isClassSimGroup())
            {
                RecurseSelectObjectsInGroup(%obj, "");
            }
        }
    }
    else
    {
        echo("nothing selected");
    }
    return ;
}
function FindSelectedInEditorTree()
{
    if (EWorldEditor.getSelectionSize() < 1)
    {
        echo("nothing selected");
        return ;
    }
    %obj = EWorldEditor.getSelectedObject(0);
    if (isObject(%obj))
    {
        EditorTree.buildVisibleTree(1);
        %item = EditorTree.findItemByObjectId(%obj.getId());
        if (%item != -1)
        {
            EditorTree.scrollVisible(%item);
            EditorTree.makeFirstResponder(1);
        }
        else
        {
            echo("unable to find item in EditorTree");
        }
    }
    else
    {
        echo("nothing selected");
    }
    return ;
}
function Creator::Create(%this, %sel)
{
    %obj = eval(%this.getItemValue(%sel));
    if (%obj == -1)
    {
        return ;
    }
    %obj.isNetCacheable = TEST_MISSIONGROUPINTEGRITY.getInitialNetCacheable(%obj);
    $instantGroup.add(%obj);
    EWorldEditor.clearSelection();
    EWorldEditor.selectObject(%obj);
    EWorldEditor.dropSelection();
    return ;
}
function TSStatic::Create(%shapeName)
{
    if ((MissionInfo.mode $= "InventoryDesigner") && (MissionInfo.mode $= "PrivateSpaceDesign"))
    {
        MessageBoxOK("Warning", "You should use TSDynamic for inventory items and in private spaces instead of TSStatic," NL "I\'ll still make it for you, but you should change it to the TSDynamic!" NL "look under \"Dynamic Shapes\" for the same thing there. thanks!", "");
    }
    %obj = new TSStatic();
    return %obj;
}
function TSStatic::Damage(%this)
{
    return ;
}
function TSDynamic::Create(%shapeName)
{
    %obj = new TSDynamic();
    return %obj;
}
function TSDynamic::Damage(%this)
{
    return ;
}
function TerraformerGui::init(%this)
{
    TerraformerHeightfieldGui.init();
    TerraformerTextureGui.init();
    return ;
}
function TerraformerGui::onWake(%this)
{
    TerraformerTextureGui.update();
    return ;
}
function TerraformerGui::onSleep(%this)
{
    %this.setPrefs();
    return ;
}
$nextTextureId = 1;
$nextTextureRegister = 1000;
$selectedMaterial = -1;
$selectedTextureOperation = -1;
$TerraformerTextureDir = "common/editor/textureScripts";
function TextureInit()
{
    Texture_operation_menu.clear();
    Texture_operation_menu.setText("Placement Operations");
    Texture_operation_menu.add("Place by Fractal", 1);
    Texture_operation_menu.add("Place by Height", 2);
    Texture_operation_menu.add("Place by Slope", 3);
    Texture_operation_menu.add("Place by Water Level", 4);
    $HeightfieldSrcRegister = Heightfield_operation.rowCount() - 1;
    TexturePreview.setValue(HeightfieldPreview.getValue());
    %script = Terrain.getTextureScript();
    if (!(%script $= ""))
    {
        texture::loadFromScript(%script);
    }
    if (Texture_material.rowCount() == 0)
    {
        Texture_operation.clear();
        $nextTextureRegister = 1000;
    }
    else
    {
        %rowCount = Texture_material.rowCount();
        %row = 0;
        while (%row < %rowCount)
        {
            %data = Texture_material.getRowText(%row);
            %entry = getRecord(%data, 0);
            %reg = getField(%entry, 1);
            $dirtyTexture[%reg] = 1;
            %opCount = getRecordCount(%data);
            %op = 2;
            while (%op < %opCount)
            {
                %entry = getRecord(%data, %op);
                %label = getField(%entry, 0);
                if (!((%label $= "Place by Fractal")) && !((%label $= "Fractal Distortion")))
                {
                    %reg = getField(%entry, 2);
                    $dirtyTexture[%reg] = 1;
                }
                %op = %op + 1;
            }
            %row = %row + 1;
        }
        texture::previewMaterial();
    }
    return ;
}
function TerraformerTextureGui::refresh(%this)
{
    return ;
}
function Texture_material_menu::onSelect(%this, %id, %text)
{
    %this.setText("Materials");
    texture::saveMaterial();
    texture::hideTab();
    %id = texture::addMaterial(%text @ "\t" @ $nextTextureRegister = $nextTextureRegister + 1);
    if (%id != -1)
    {
        Texture_material.setSelectedById(%id);
        texture::addOperation("Fractal Distortion\ttab_DistortMask\t" @ $nextTextureRegister = $nextTextureRegister + 1 @ "\t0\tdmask_interval\t20\tdmask_rough\t0\tdmask_seed\t" @ Terraformer.generateSeed() @ "\tdmask_filter\t0.00000 0.00000 0.13750 0.487500 0.86250 1.00000 1.00000");
    }
    return ;
}
function texture::addMaterialTexture()
{
    %root = filePath(Terrain.terrainFile);
    getLoadFilename("*/terrains// .png\tterrains/*.jpg", addLoadedMaterial);
    return ;
}
function addLoadedMaterial(%file)
{
    texture::saveMaterial();
    texture::hideTab();
    %text = filePath(%file) @ "/" @ fileBase(%file);
    %id = texture::addMaterial(%text @ "\t" @ $nextTextureRegister = $nextTextureRegister + 1);
    if (%id != -1)
    {
        Texture_material.setSelectedById(%id);
        texture::addOperation("Fractal Distortion\ttab_DistortMask\t" @ $nextTextureRegister = $nextTextureRegister + 1 @ "\t0\tdmask_interval\t20\tdmask_rough\t0\tdmask_seed\t" @ Terraformer.generateSeed() @ "\tdmask_filter\t0.00000 0.00000 0.13750 0.487500 0.86250 1.00000 1.00000");
    }
    texture::save();
    return ;
}
function Texture_material::onSelect(%this, %id, %text)
{
    texture::saveMaterial();
    if (%id != $selectedMaterial)
    {
        $selectedTextureOperation = -1;
        Texture_operation.clear();
        texture::hideTab();
        texture::restoreMaterial(%id);
    }
    %matName = getField(%text, 0);
    ETerrainEditor.paintMaterial = %matName;
    texture::previewMaterial(%id);
    $selectedMaterial = %id;
    $selectedTextureOperation = -1;
    Texture_operation.clearSelection();
    return ;
}
function Texture_operation_menu::onSelect(%this, %id, %text)
{
    %this.setText("Placement Operations");
    %id = -1;
    if ($selectedMaterial == -1)
    {
        return ;
    }
    %dreg = getField(Texture_operation.getRowText(0), 2);
    if (%text $= "Place by Fractal")
    {
        %id = texture::addOperation("Place by Fractal\ttab_FractalMask\t" @ $nextTextureRegister = $nextTextureRegister + 1 @ "\t" @ %dreg @ "\tfbmmask_interval\t16\tfbmmask_rough\t0.000\tfbmmask_seed\t" @ Terraformer.generateSeed() @ "\tfbmmask_filter\t0.000000 0.166667 0.333333 0.500000 0.666667 0.833333 1.000000\tfBmDistort\ttrue");
    }
    else
    {
        if (%text $= "Place by Height")
        {
            %id = texture::addOperation("Place by Height\ttab_HeightMask\t" @ $nextTextureRegister = $nextTextureRegister + 1 @ "\t" @ %dreg @ "\ttextureHeightFilter\t0 0.2 0.4 0.6 0.8 1.0\theightDistort\ttrue");
        }
        else
        {
            if (%text $= "Place by Slope")
            {
                %id = texture::addOperation("Place by Slope\ttab_SlopeMask\t" @ $nextTextureRegister = $nextTextureRegister + 1 @ "\t" @ %dreg @ "\ttextureSlopeFilter\t0 0.2 0.4 0.6 0.8 1.0\tslopeDistort\ttrue");
            }
            else
            {
                if (%text $= "Place by Water Level")
                {
                    %id = texture::addOperation("Place by Water Level\ttab_WaterMask\t" @ $nextTextureRegister = $nextTextureRegister + 1 @ "\t" @ %dreg @ "\twaterDistort\ttrue");
                }
            }
        }
    }
    texture::hideTab();
    if (%id != -1)
    {
        Texture_operation.setSelectedById(%id);
    }
    return ;
}
function Texture_operation::onSelect(%this, %id, %text)
{
    texture::saveOperation();
    if (!(%id $= $selectedTextureOperation))
    {
        texture::hideTab();
        texture::restoreOperation(%id);
        texture::showTab(%id);
    }
    texture::previewOperation(%id);
    $selectedTextureOperation = %id;
    return ;
}
function texture::deleteMaterial(%id)
{
    if (%id $= "")
    {
        %id = $selectedMaterial;
    }
    if (%id == -1)
    {
        return ;
    }
    %row = Texture_material.getRowNumById(%id);
    Texture_material.removeRow(%row);
    %rowCount = Texture_material.rowCount() - 1;
    if (%row > %rowCount)
    {
        %row = %rowCount;
    }
    if (%id == $selectedMaterial)
    {
        $selectedMaterial = -1;
    }
    Texture_operation.clear();
    %id = Texture_material.getRowId(%row);
    Texture_material.setSelectedById(%id);
    texture::save();
    return ;
}
function texture::deleteOperation(%id)
{
    if (%id $= "")
    {
        %id = $selectedTextureOperation;
    }
    if (%id == -1)
    {
        return ;
    }
    %row = Texture_operation.getRowNumById(%id);
    if (%row == 0)
    {
        return ;
    }
    Texture_operation.removeRow(%row);
    %rowCount = Texture_operation.rowCount() - 1;
    if (%row > %rowCount)
    {
        %row = %rowCount;
    }
    if (%id == $selectedTextureOperation)
    {
        $selectedTextureOperation = -1;
    }
    %id = Texture_operation.getRowId(%row);
    Texture_operation.setSelectedById(%id);
    texture::save();
    return ;
}
function texture::applyMaterials()
{
    texture::saveMaterial();
    %count = Texture_material.rowCount();
    if (%count > 0)
    {
        %data = getRecord(Texture_material.getRowText(0), 0);
        %mat_list = getField(%data, 0);
        %reg_list = getField(%data, 1);
        texture::evalMaterial(Texture_material.getRowId(0));
        %i = 1;
        while (%i < %count)
        {
            texture::evalMaterial(Texture_material.getRowId(%i));
            %data = getRecord(Texture_material.getRowText(%i), 0);
            %mat_list = %mat_list @ " " @ getField(%data, 0);
            %reg_list = %reg_list @ " " @ getField(%data, 1);
            %i = %i + 1;
        }
        Terraformer.setMaterials(%reg_list, %mat_list);
    }
    return ;
}
function texture::previewMaterial(%id)
{
    if (%id $= "")
    {
        %id = $selectedMaterial;
    }
    if (%id == -1)
    {
        return ;
    }
    %data = Texture_material.getRowTextById(%id);
    %row = Texture_material.getRowNumById(%id);
    %reg = getField(getRecord(%data, 0), 1);
    texture::evalMaterial(%id);
    Terraformer.preview(TexturePreview, %reg);
    return ;
}
function texture::evalMaterial(%id)
{
    if (%id $= "")
    {
        %id = $selectedMaterial;
    }
    if (%id == -1)
    {
        return ;
    }
    %data = Texture_material.getRowTextById(%id);
    %reg = getField(getRecord(%data, 0), 1);
    %opCount = getRecordCount(%data);
    if (%opCount >= 2)
    {
        %entry = getRecord(%data, 1);
        texture::evalOperationData(%entry, 1);
        %op = 2;
        while (%op < %opCount)
        {
            %entry = getRecord(%data, %op);
            %reg_list = %reg_list @ getField(%entry, 2) @ " ";
            texture::evalOperationData(%entry, %op);
            %op = %op + 1;
        }
        Terraformer.mergeMasks(%reg_list, %reg);
    }
    texture::save();
    return ;
}
function texture::evalOperation(%id)
{
    if (%id $= "")
    {
        %id = $selectedTextureOperation;
    }
    if (%id == -1)
    {
        return ;
    }
    %data = Texture_operation.getRowTextById(%id);
    %row = Texture_operation.getRowNumById(%id);
    if (%row != 0)
    {
        texture::evalOperation(Texture_operation.getRowId(0));
    }
    texture::evalOperationData(%data, %row);
    texture::save();
    return ;
}
function texture::evalOperationData(%data, %row)
{
    %label = getField(%data, 0);
    %reg = getField(%data, 2);
    %dreg = getField(%data, 3);
    %id = Texture_material.getRowId(%row);
    if ($dirtyTexture[%reg] == 0)
    {
        return ;
    }
    if (%label $= "Fractal Distortion")
    {
        Terraformer.maskFBm(%reg, getField(%data, 5), getField(%data, 7), getField(%data, 9), getField(%data, 11), 0, 0);
    }
    else
    {
        if (%label $= "Place by Fractal")
        {
            Terraformer.maskFBm(%reg, getField(%data, 5), getField(%data, 7), getField(%data, 9), getField(%data, 11), getField(%data, 13), %dreg);
        }
        else
        {
            if (%label $= "Place by Height")
            {
                Terraformer.maskHeight($HeightfieldSrcRegister, %reg, getField(%data, 5), getField(%data, 7), %dreg);
            }
            else
            {
                if (%label $= "Place by Slope")
                {
                    Terraformer.maskSlope($HeightfieldSrcRegister, %reg, getField(%data, 5), getField(%data, 7), %dreg);
                }
                else
                {
                    if (%label $= "Place by Water Level")
                    {
                        Terraformer.maskWater($HeightfieldSrcRegister, %reg, getField(%data, 5), %dreg);
                    }
                }
            }
        }
    }
    $dirtyTexture[%reg] = 0;
    return ;
}
function texture::previewOperation(%id)
{
    if (%id $= "")
    {
        %id = $selectedTextureOperation;
    }
    if (%id == -1)
    {
        return ;
    }
    %row = Texture_operation.getRowNumById(%id);
    %data = Texture_operation.getRowText(%row);
    %reg = getField(%data, 2);
    texture::evalOperation(%id);
    Terraformer.preview(TexturePreview, %reg);
    return ;
}
function texture::restoreMaterial(%id)
{
    if (%id == -1)
    {
        return ;
    }
    %data = Texture_material.getRowTextById(%id);
    Texture_operation.clear();
    %recordCount = getRecordCount(%data);
    %record = 1;
    while (%record < %recordCount)
    {
        %entry = getRecord(%data, %record);
        Texture_operation.addRow($nextTextureId = $nextTextureId + 1, %entry);
        %record = %record + 1;
    }
}

function texture::saveMaterial()
{
    %id = $selectedMaterial;
    if (%id == -1)
    {
        return ;
    }
    texture::saveOperation();
    %data = Texture_material.getRowTextById(%id);
    %newData = getRecord(%data, 0);
    %rowCount = Texture_operation.rowCount();
    %row = 0;
    while (%row < %rowCount)
    {
        %newData = %newData @ "\n" @ Texture_operation.getRowText(%row);
        %row = %row + 1;
    }
    Texture_material.setRowById(%id, %newData);
    texture::save();
    return ;
}
function texture::restoreOperation(%id)
{
    if (%id == -1)
    {
        return ;
    }
    %data = Texture_operation.getRowTextById(%id);
    %fieldCount = getFieldCount(%data);
    %field = 4;
    while (%field < %fieldCount)
    {
        %obj = getField(%data, %field);
        %obj.setValue(getField(%data, %field + 1));
        %field = %field + 2;
    }
    texture::save();
    return ;
}
function texture::saveOperation()
{
    %id = $selectedTextureOperation;
    if (%id == -1)
    {
        return ;
    }
    %data = Texture_operation.getRowTextById(%id);
    %newData = getField(%data, 0) @ "\t" @ getField(%data, 1) @ "\t" @ getField(%data, 2) @ "\t" @ getField(%data, 3);
    %fieldCount = getFieldCount(%data);
    %field = 4;
    while (%field < %fieldCount)
    {
        %obj = getField(%data, %field);
        %newData = %newData @ "\t" @ %obj @ "\t" @ %obj.getValue();
        %field = %field + 2;
    }
    %dirty = !(%data $= %newData);
    %reg = getField(%data, 2);
    $dirtyTexture[%reg] = %dirty ;
    Texture_operation.setRowById(%id, %newData);
    if (%dirty == 1)
    {
        %data = Texture_material.getRowTextById($selectedMaterial);
        %reg = getField(getRecord(%data, 0), 1);
        $dirtyTexture[%reg] = 1;
    }
    %row = Texture_material.getRowNumById(%id);
    if (%row == 0)
    {
        %rowCount = Texture_operation.rowCount();
        %r = 1;
        while (%r < %rowCount)
        {
            %data = Texture_operation.getRowText(%r);
            $dirtyTexture[getField(%data, 2)] = 1;
            %r = %r + 1;
        }
    }
    texture::save();
    return ;
}
function texture::addMaterial(%entry)
{
    %id = $nextTextureId = $nextTextureId + 1;
    Texture_material.addRow(%id, %entry);
    %reg = getField(%entry, 1);
    $dirtyTexture[%reg] = 1;
    texture::save();
    return %id;
}
function texture::addOperation(%entry)
{
    %id = $nextTextureId = $nextTextureId + 1;
    Texture_operation.addRow(%id, %entry);
    %reg = getField(%entry, 2);
    $dirtyTexture[%reg] = 1;
    texture::save();
    return %id;
}
function texture::save()
{
    %script = "";
    %rowCount = Texture_material.rowCount();
    %row = 0;
    while (%row < %rowCount)
    {
        if (%row != 0)
        {
            %script = %script @ "\n";
        }
        %data = expandEscape(Texture_material.getRowText(%row));
        %script = %script @ %data;
        %row = %row + 1;
    }
    Terrain.setTextureScript(%script);
    ETerrainEditor.isDirty = 1;
    return ;
}
function texture::import()
{
    getLoadFilename("*.ter", "Texture::doLoadTexture");
    return ;
}
function texture::loadFromScript(%script)
{
    Texture_material.clear();
    Texture_operation.clear();
    $selectedMaterial = -1;
    $selectedTextureOperation = -1;
    %i = 0;
    %rec = getRecord(%script, %i);
    while (!(%rec $= ""))
    {
        texture::addMaterial(collapseEscape(%rec));
        %rec = getRecord(%script, %i = %i + 1);
    }
    $nextTextureRegister = 1000;
    %rowCount = Texture_material.rowCount();
    %row = 0;
    while (%row < %rowCount)
    {
        $dirtyTexture[$nextTextureRegister] = 1;
        %data = Texture_material.getRowText(%row);
        %rec = getRecord(%data, 0);
        %rec = setField(%rec, 1, $nextTextureRegister);
        %data = setRecord(%data, 0, %rec);
        $nextTextureRegister = $nextTextureRegister + 1;
        %opCount = getRecordCount(%data);
        %op = 1;
        while (%op < %opCount)
        {
            if (%op == 1)
            {
                %frac_reg = $nextTextureRegister;
            }
            $dirtyTexture[$nextTextureRegister] = 1;
            %rec = getRecord(%data, %op);
            %rec = setField(%rec, 2, $nextTextureRegister);
            %rec = setField(%rec, 3, %frac_reg);
            %data = setRecord(%data, %op, %rec);
            $nextTextureRegister = $nextTextureRegister + 1;
            %op = %op + 1;
        }
        %id = Texture_material.getRowId(%row);
        Texture_material.setRowById(%id, %data);
        %row = %row + 1;
    }
    $selectedMaterial = -1;
    Texture_material.setSelectedById(Texture_material.getRowId(0));
    return ;
}
function texture::doLoadTexture(%name)
{
    %newTerr = new TerrainBlock()
    {
        position = "0 0 0";
        terrainFile = %name;
        squareSize = 8;
        visibleDistance = 100;
    };
    if (isObject(%newTerr))
    {
        %script = %newTerr.getTextureScript();
        if (!(%script $= ""))
        {
            texture::loadFromScript(%script);
        }
        %newTerr.delete();
    }
    return ;
}
function texture::hideTab()
{
    tab_DistortMask.setVisible(0);
    tab_FractalMask.setVisible(0);
    tab_HeightMask.setVisible(0);
    tab_SlopeMask.setVisible(0);
    tab_WaterMask.setVisible(0);
    return ;
}
function texture::showTab(%id)
{
    texture::hideTab();
    %data = Texture_operation.getRowTextById(%id);
    %tab = getField(%data, 1);
    %tab.setVisible(1);
    return ;
}
$TerraformerHeightfieldDir = "common/editor/heightScripts";
function tab_Blend::reset(%this)
{
    blend_option.clear();
    blend_option.add("Add", 0);
    blend_option.add("Subtract", 1);
    blend_option.add("Max", 2);
    blend_option.add("Min", 3);
    blend_option.add("Multiply", 4);
    return ;
}
function tab_fBm::reset(%this)
{
    fbm_detail.clear();
    fbm_detail.add("Very Low", 0);
    fbm_detail.add("Low", 1);
    fbm_detail.add("Normal", 2);
    fbm_detail.add("High", 3);
    fbm_detail.add("Very High", 4);
    return ;
}
function tab_RMF::reset(%this)
{
    rmf_detail.clear();
    rmf_detail.add("Very Low", 0);
    rmf_detail.add("Low", 1);
    rmf_detail.add("Normal", 2);
    rmf_detail.add("High", 3);
    rmf_detail.add("Very High", 4);
    return ;
}
function tab_terrainFile::reset(%this)
{
    terrainFile_textList.clear();
    %filespec = $TerraformerHeightfieldDir @ "/*.ter";
    %file = findFirstFile(%filespec);
    while (!(%file $= ""))
    {
        terrainFile_textList.addRow(%i = %i + 1, fileBase(%file) @ fileExt(%file));
        %file = findNextFile(%filespec);
    }
}

function tab_Canyon::reset()
{
    return ;
}
function tab_Smooth::reset()
{
    return ;
}
function tab_SmoothWater::reset()
{
    return ;
}
function tab_SmoothRidge::reset()
{
    return ;
}
function tab_Filter::reset()
{
    return ;
}
function tab_Turbulence::reset()
{
    return ;
}
function tab_Thermal::reset()
{
    return ;
}
function tab_Hydraulic::reset()
{
    return ;
}
function tab_General::reset()
{
    return ;
}
function tab_Bitmap::reset()
{
    return ;
}
function tab_Sinus::reset()
{
    return ;
}
function Heightfield::resetTabs()
{
    tab_terrainFile.reset();
    tab_fBm.reset();
    tab_RMF.reset();
    tab_Canyon.reset();
    tab_Smooth.reset();
    tab_SmoothWater.reset();
    tab_SmoothRidge.reset();
    tab_Filter.reset();
    tab_Turbulence.reset();
    tab_Thermal.reset();
    tab_Hydraulic.reset();
    tab_General.reset();
    tab_Bitmap.reset();
    tab_Blend.reset();
    tab_Sinus.reset();
    return ;
}
function TerraformerInit()
{
    Heightfield_options.clear();
    Heightfield_options.setText("Operation");
    Heightfield_options.add("fBm Fractal", 0);
    Heightfield_options.add("Rigid MultiFractal", 1);
    Heightfield_options.add("Canyon Fractal", 2);
    Heightfield_options.add("Sinus", 3);
    Heightfield_options.add("Bitmap", 4);
    Heightfield_options.add("Turbulence", 5);
    Heightfield_options.add("Smoothing", 6);
    Heightfield_options.add("Smooth Water", 7);
    Heightfield_options.add("Smooth Ridges/Valleys", 8);
    Heightfield_options.add("Filter", 9);
    Heightfield_options.add("Thermal Erosion", 10);
    Heightfield_options.add("Hydraulic Erosion", 11);
    Heightfield_options.add("Blend", 12);
    Heightfield_options.add("Terrain File", 13);
    Heightfield::resetTabs();
    %script = Terrain.getHeightfieldScript();
    if (!(%script $= ""))
    {
        Heightfield::loadFromScript(%script, 1);
    }
    if (Heightfield_operation.rowCount() == 0)
    {
        Heightfield_operation.clear();
        %id1 = Heightfield::add("General\tTab_general\tgeneral_min_height\t50\tgeneral_scale\t300\tgeneral_water\t0.000\tgeneral_centerx\t0\tgeneral_centery\t0");
        Heightfield_operation.setSelectedById(%id1);
    }
    Heightfield::resetTabs();
    Heightfield::preview();
    return ;
}
function Heightfield_options::onSelect(%this, %unused, %text)
{
    Heightfield_options.setText("Operation");
    %id = -1;
    %rowCount = Heightfield_operation.rowCount();
    if (%text $= "Terrain File")
    {
        %id = Heightfield::add("Terrain File\ttab_terrainFile\tterrainFile_terrFileText\tterrains/terr1.ter\tterrainFile_textList\tterr1.ter");
    }
    else
    {
        if (%text $= "fBm Fractal")
        {
            %id = Heightfield::add("fBm Fractal\ttab_fBm\tfbm_interval\t9\tfbm_rough\t0.000\tfBm_detail\tNormal\tfBm_seed\t" @ Terraformer.generateSeed());
        }
        else
        {
            if (%text $= "Rigid MultiFractal")
            {
                %id = Heightfield::add("Rigid MultiFractal\ttab_RMF\trmf_interval\t4\trmf_rough\t0.000\trmf_detail\tNormal\trmf_seed\t" @ Terraformer.generateSeed());
            }
            else
            {
                if (%text $= "Canyon Fractal")
                {
                    %id = Heightfield::add("Canyon Fractal\ttab_Canyon\tcanyon_freq\t5\tcanyon_factor\t0.500\tcanyon_seed\t" @ Terraformer.generateSeed());
                }
                else
                {
                    if (%text $= "Sinus")
                    {
                        %id = Heightfield::add("Sinus\ttab_Sinus\tsinus_filter\t1 0.83333 0.6666 0.5 0.33333 0.16666 0\tsinus_seed\t" @ Terraformer.generateSeed());
                    }
                    else
                    {
                        if (%text $= "Bitmap")
                        {
                            %id = Heightfield::add("Bitmap\ttab_Bitmap\tbitmap_name\t");
                            Heightfield::setBitmap();
                        }
                    }
                }
            }
        }
    }
    if (Heightfield_operation.rowCount() >= 1)
    {
        if (%text $= "Smoothing")
        {
            %id = Heightfield::add("Smoothing\ttab_Smooth\tsmooth_factor\t0.500\tsmooth_iter\t0");
        }
        else
        {
            if (%text $= "Smooth Water")
            {
                %id = Heightfield::add("Smooth Water\ttab_SmoothWater\twatersmooth_factor\t0.500\twatersmooth_iter\t0");
            }
            else
            {
                if (%text $= "Smooth Ridges/Valleys")
                {
                    %id = Heightfield::add("Smooth Ridges/Valleys\ttab_SmoothRidge\tridgesmooth_factor\t0.8500\tridgesmooth_iter\t1");
                }
                else
                {
                    if (%text $= "Filter")
                    {
                        %id = Heightfield::add("Filter\ttab_Filter\tfilter\t0 0.16666667 0.3333333 0.5 0.6666667 0.8333333 1");
                    }
                    else
                    {
                        if (%text $= "Turbulence")
                        {
                            %id = Heightfield::add("Turbulence\ttab_Turbulence\tturbulence_factor\t0.250\tturbulence_radius\t10");
                        }
                        else
                        {
                            if (%text $= "Thermal Erosion")
                            {
                                %id = Heightfield::add("Thermal Erosion\ttab_Thermal\tthermal_slope\t30\tthermal_cons\t80.0\tthermal_iter\t0");
                            }
                            else
                            {
                                if (%text $= "Hydraulic Erosion")
                                {
                                    %id = Heightfield::add("Hydraulic Erosion\ttab_Hydraulic\thydraulic_iter\t0\thydraulic_filter\t0 0.16666667 0.3333333 0.5 0.6666667 0.8333333 1");
                                }
                            }
                        }
                    }
                }
            }
        }
    }
    if (Heightfield_operation.rowCount() >= 2)
    {
        if ("Blend" $= %text)
        {
            %id = Heightfield::add("Blend\ttab_Blend\tblend_factor\t0.500\tblend_srcB\t" @ %rowCount - 2 @ "\tblend_option\tadd");
        }
    }
    if (%id != -1)
    {
        Heightfield_operation.setSelectedById(%id);
    }
    return ;
}
function Heightfield::eval(%id)
{
    if (%id == -1)
    {
        return ;
    }
    %data = restWords(Heightfield_operation.getRowTextById(%id));
    %label = getField(%data, 0);
    %row = Heightfield_operation.getRowNumById(%id);
    echo("Heightfield::eval:" @ %row @ "  " @ %label);
    if (%label $= "General")
    {
        if (Terrain.squareSize > 0)
        {
            %size = Terrain.squareSize;
        }
        else
        {
            %size = 8;
        }
        Terraformer.setTerrainInfo(256, %size, getField(%data, 3), getField(%data, 5), getField(%data, 7));
        Terraformer.setShift(getField(%data, 9), getField(%data, 11));
        Terraformer.terrainData(%row);
    }
    else
    {
        if (%label $= "Terrain File")
        {
            Terraformer.terrainFile(%row, getField(%data, 3));
        }
        else
        {
            if (%label $= "fBm Fractal")
            {
                Terraformer.fBm(%row, getField(%data, 3), getField(%data, 5), getField(%data, 7), getField(%data, 9));
            }
            else
            {
                if (%label $= "Sinus")
                {
                    Terraformer.sinus(%row, getField(%data, 3), getField(%data, 5));
                }
                else
                {
                    if (%label $= "Rigid MultiFractal")
                    {
                        Terraformer.rigidMultiFractal(%row, getField(%data, 3), getField(%data, 5), getField(%data, 7), getField(%data, 9));
                    }
                    else
                    {
                        if (%label $= "Canyon Fractal")
                        {
                            Terraformer.canyon(%row, getField(%data, 3), getField(%data, 5), getField(%data, 7));
                        }
                        else
                        {
                            if (%label $= "Smoothing")
                            {
                                Terraformer.smooth(%row - 1, %row, getField(%data, 3), getField(%data, 5));
                            }
                            else
                            {
                                if (%label $= "Smooth Water")
                                {
                                    Terraformer.smoothWater(%row - 1, %row, getField(%data, 3), getField(%data, 5));
                                }
                                else
                                {
                                    if (%label $= "Smooth Ridges/Valleys")
                                    {
                                        Terraformer.smoothRidges(%row - 1, %row, getField(%data, 3), getField(%data, 5));
                                    }
                                    else
                                    {
                                        if (%label $= "Filter")
                                        {
                                            Terraformer.filter(%row - 1, %row, getField(%data, 3));
                                        }
                                        else
                                        {
                                            if (%label $= "Turbulence")
                                            {
                                                Terraformer.turbulence(%row - 1, %row, getField(%data, 3), getField(%data, 5));
                                            }
                                            else
                                            {
                                                if (%label $= "Thermal Erosion")
                                                {
                                                    Terraformer.erodeThermal(%row - 1, %row, getField(%data, 3), getField(%data, 5), getField(%data, 7));
                                                }
                                                else
                                                {
                                                    if (%label $= "Hydraulic Erosion")
                                                    {
                                                        Terraformer.erodeHydraulic(%row - 1, %row, getField(%data, 3), getField(%data, 5));
                                                    }
                                                    else
                                                    {
                                                        if (%label $= "Bitmap")
                                                        {
                                                            Terraformer.loadGreyscale(%row, getField(%data, 3));
                                                        }
                                                        else
                                                        {
                                                            if (%label $= "Blend")
                                                            {
                                                                %rowCount = Heightfield_operation.rowCount();
                                                                if (%rowCount > 2)
                                                                {
                                                                    %a = Heightfield_operation.getRowNumById(%id) - 1;
                                                                    %b = getField(%data, 5);
                                                                    echo("Blend: " @ %data);
                                                                    echo("Blend: " @ getField(%data, 3) @ "  " @ getField(%data, 7));
                                                                    if ((((%a < %rowCount) || (%a > 0)) || (%b < %rowCount)) || (%b > 0))
                                                                    {
                                                                        Terraformer.blend(%a, %b, %row, getField(%data, 3), getField(%data, 7));
                                                                    }
                                                                    else
                                                                    {
                                                                        echo("Heightfield Editor: Blend parameters out of range.");
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
    }
    return ;
}
function Heightfield::add(%entry)
{
    Heightfield::saveTab();
    Heightfield::hideTab();
    %id = $NextOperationId = $NextOperationId + 1;
    if ($SelectedOperation != -1)
    {
        %row = Heightfield_operation.getRowNumById($SelectedOperation) + 1;
        %entry = %row @ " " @ %entry;
        Heightfield_operation.addRow(%id, %entry, %row);
        %i = %row + 1;
        while (%i < Heightfield_operation.rowCount())
        {
            %id = Heightfield_operation.getRowId(%i);
            %text = Heightfield_operation.getRowTextById(%id);
            %text = setWord(%text, 0, %i);
            Heightfield_operation.setRowById(%id, %text);
            %i = %i + 1;
        }
    }
    else
    {
        %entry = Heightfield_operation.rowCount() @ " " @ %entry;
        Heightfield_operation.addRow(%id, %entry);
    }
    %row = Heightfield_operation.getRowNumById(%id);
    if (%row <= $HeightfieldDirtyRow)
    {
        $HeightfieldDirtyRow = %row;
    }
    Heightfield::save();
    return %id;
}
function Heightfield::onDelete(%id)
{
    if (%id $= "")
    {
        %id = $SelectedOperation;
    }
    %row = Heightfield_operation.getRowNumById(%id);
    if (%row == 0)
    {
        return ;
    }
    Heightfield_operation.removeRow(%row);
    %i = %row;
    while (%i < Heightfield_operation.rowCount())
    {
        %id2 = Heightfield_operation.getRowId(%i);
        %text = Heightfield_operation.getRowTextById(%id2);
        %text = setWord(%text, 0, %i);
        Heightfield_operation.setRowById(%id2, %text);
        %i = %i + 1;
    }
    if ($HeightfieldDirtyRow >= %row)
    {
        $HeightfieldDirtyRow = %row;
    }
    %rowCount = Heightfield_operation.rowCount() - 1;
    if (%row > %rowCount)
    {
        %row = %rowCount;
    }
    if (%id == $SelectedOperation)
    {
        $SelectedOperation = -1;
    }
    %id = Heightfield_operation.getRowId(%row);
    Heightfield_operation.setSelectedById(%id);
    Heightfield::save();
    return ;
}
function Heightfield_operation::onSelect(%this, %id, %text)
{
    Heightfield::saveTab();
    Heightfield::hideTab();
    $SelectedOperation = %id;
    Heightfield::restoreTab($SelectedOperation);
    Heightfield::showTab($SelectedOperation);
    Heightfield::preview($SelectedOperation);
    return ;
}
function Heightfield::restoreTab(%id)
{
    if (%id == -1)
    {
        return ;
    }
    Heightfield::hideTab();
    %data = restWords(Heightfield_operation.getRowTextById(%id));
    %fieldCount = getFieldCount(%data);
    %field = 2;
    while (%field < %fieldCount)
    {
        %obj = getField(%data, %field);
        %obj.setValue(getField(%data, %field + 1));
        %field = %field + 2;
    }
    Heightfield::save();
    return ;
}
function Heightfield::saveTab()
{
    if ($SelectedOperation == -1)
    {
        return ;
    }
    %data = Heightfield_operation.getRowTextById($SelectedOperation);
    %rowNum = getWord(%data, 0);
    %data = restWords(%data);
    %newData = getField(%data, 0) @ "\t" @ getField(%data, 1);
    %fieldCount = getFieldCount(%data);
    %field = 2;
    while (%field < %fieldCount)
    {
        %obj = getField(%data, %field);
        %newData = %newData @ "\t" @ %obj @ "\t" @ %obj.getValue();
        %field = %field + 2;
    }
    if (!(%data $= %newData))
    {
        %row = Heightfield_operation.getRowNumById($SelectedOperation);
        if ((%row <= $HeightfieldDirtyRow) && (%row > 0))
        {
            $HeightfieldDirtyRow = %row;
        }
    }
    Heightfield_operation.setRowById($SelectedOperation, %rowNum @ " " @ %newData);
    Heightfield::save();
    return ;
}
function Heightfield::preview(%id)
{
    %rowCount = Heightfield_operation.rowCount();
    if (%id $= "")
    {
        %id = Heightfield_operation.getRowId(%rowCount - 1);
    }
    %row = Heightfield_operation.getRowNumById(%id);
    Heightfield::refresh(%row);
    Terraformer.previewScaled(HeightfieldPreview, %row);
    return ;
}
function Heightfield::refresh(%last)
{
    if (%last $= "")
    {
        %last = Heightfield_operation.rowCount() - 1;
    }
    Heightfield::eval(Heightfield_operation.getRowId(0));
    while ($HeightfieldDirtyRow <= %last)
    {
        %id = Heightfield_operation.getRowId($HeightfieldDirtyRow);
        Heightfield::eval(%id);
        $HeightfieldDirtyRow = $HeightfieldDirtyRow + 1;
    }
    Heightfield::save();
    return ;
}
function Heightfield::apply(%id)
{
    %rowCount = Heightfield_operation.rowCount();
    if (%rowCount < 1)
    {
        return ;
    }
    if (%id $= "")
    {
        %id = Heightfield_operation.getRowId(%rowCount - 1);
    }
    %row = Heightfield_operation.getRowNumById(%id);
    HeightfieldPreview.setRoot();
    Heightfield::refresh(%row);
    Terraformer.setTerrain(%row);
    Terraformer.setCameraPosition(0, 0, 0);
    ETerrainEditor.isDirty = 1;
    return ;
}
$TerraformerSaveRegister = 0;
function Heightfield::saveBitmap(%name)
{
    if (%name $= "")
    {
        getSaveFilename("*.png", "Heightfield::doSaveBitmap", $TerraformerHeightfieldDir @ "/" @ fileBase($Client::MissionFile) @ ".png");
    }
    else
    {
        Heightfield::doSaveBitmap(%name);
    }
    return ;
}
function Heightfield::doSaveBitmap(%name)
{
    Terraformer.saveGreyscale($TerraformerSaveRegister, %name);
    return ;
}
function Heightfield::save()
{
    %script = "";
    %rowCount = Heightfield_operation.rowCount();
    %row = 0;
    while (%row < %rowCount)
    {
        if (%row != 0)
        {
            %script = %script @ "\n";
        }
        %data = restWords(Heightfield_operation.getRowText(%row));
        %script = %script @ expandEscape(%data);
        %row = %row + 1;
    }
    Terrain.setHeightfieldScript(%script);
    ETerrainEditor.isDirty = 1;
    return ;
}
function Heightfield::import()
{
    getLoadFilename("*.ter", "Heightfield::doLoadHeightfield");
    return ;
}
function Heightfield::loadFromScript(%script, %leaveCamera)
{
    echo(%script);
    Heightfield_operation.clear();
    $SelectedOperation = -1;
    $HeightfieldDirtyRow = -1;
    HeightfieldPreview.reset();
    %rec = getRecord(%script, %i);
    while (!(%rec $= ""))
    {
        Heightfield::add(collapseEscape(%rec));
        %rec = getRecord(%script, %i = %i + 1);
    }
    if (Heightfield_operation.rowCount() == 0)
    {
        Heightfield_operation.clear();
        Heightfield::add("General\tTab_general\tgeneral_min_height\t50\tgeneral_scale\t300\tgeneral_water\t0.000\tgeneral_centerx\t0\tgeneral_centery\t0");
    }
    %data = restWords(Heightfield_operation.getRowText(0));
    %x = getField(%data, 7);
    %y = getField(%data, 9);
    HeightfieldPreview.setOrigin(%x, %y);
    Heightfield_operation.setSelectedById(Heightfield_operation.getRowId(0));
    if (!%leaveCamera)
    {
        Terraformer.setCameraPosition(%x, %y);
    }
    return ;
}
function strip(%stripStr, %strToStrip)
{
    %len = strlen(%stripStr);
    if (strcmp(getSubStr(%strToStrip, 0, %len), %stripStr) == 0)
    {
        return getSubStr(%strToStrip, %len, 100000);
    }
    return %strToStrip;
}
function Heightfield::doLoadHeightfield(%name)
{
    %newTerr = new TerrainBlock()
    {
        position = "0 0 -1000";
        terrainFile = strip("terrains/", %name);
        squareSize = 8;
        visibleDistance = 100;
    };
    if (isObject(%newTerr))
    {
        %script = %newTerr.getHeightfieldScript();
        if (!(%script $= ""))
        {
            Heightfield::loadFromScript(%script);
        }
        %newTerr.delete();
    }
    return ;
}
function Heightfield::setBitmap()
{
    getLoadFilename($TerraformerHeightfieldDir @ "/*.png", "Heightfield::doSetBitmap");
    return ;
}
function Heightfield::doSetBitmap(%name)
{
    bitmap_name.setValue(%name);
    Heightfield::saveTab();
    Heightfield::preview($SelectedOperation);
    return ;
}
function Heightfield::hideTab()
{
    tab_terrainFile.setVisible(0);
    tab_fBm.setVisible(0);
    tab_RMF.setVisible(0);
    tab_Canyon.setVisible(0);
    tab_Smooth.setVisible(0);
    tab_SmoothWater.setVisible(0);
    tab_SmoothRidge.setVisible(0);
    tab_Filter.setVisible(0);
    tab_Turbulence.setVisible(0);
    tab_Thermal.setVisible(0);
    tab_Hydraulic.setVisible(0);
    tab_General.setVisible(0);
    tab_Bitmap.setVisible(0);
    tab_Blend.setVisible(0);
    tab_Sinus.setVisible(0);
    return ;
}
function Heightfield::showTab(%id)
{
    Heightfield::hideTab();
    %data = restWords(Heightfield_operation.getRowTextById(%id));
    %tab = getField(%data, 1);
    echo("Tab data: " @ %data @ " tab: " @ %tab);
    %tab.setVisible(1);
    return ;
}
function Heightfield::center()
{
    %camera = Terraformer.getCameraPosition();
    %x = getWord(%camera, 0);
    %y = getWord(%camera, 1);
    HeightfieldPreview.setOrigin(%x, %y);
    %origin = HeightfieldPreview.getOrigin();
    %x = getWord(%origin, 0);
    %y = getWord(%origin, 1);
    %root = HeightfieldPreview.getRoot();
    %x = %x + getWord(%root, 0);
    %y = %y + getWord(%root, 1);
    general_centerx.setValue(%x);
    general_centery.setValue(%y);
    Heightfield::saveTab();
    return ;
}
function ExportHeightfield::onAction()
{
    error("Time to export the heightfield...");
    if (Heightfield_operation.getSelectedId() != -1)
    {
        $TerraformerSaveRegister = getWord(Heightfield_operation.getValue(), 0);
        Heightfield::saveBitmap("");
    }
    return ;
}
function TerrainEditor::onGuiUpdate(%this, %text)
{
    %mouseBrushInfo = " (Mouse Brush) #: " @ getWord(%text, 0) @ "  avg: " @ getWord(%text, 1);
    %selectionInfo = " (Selection) #: " @ getWord(%text, 2) @ "  avg: " @ getWord(%text, 3);
    TEMouseBrushInfo.setValue(%mouseBrushInfo);
    TEMouseBrushInfo1.setValue(%mouseBrushInfo);
    TESelectionInfo.setValue(%selectionInfo);
    TESelectionInfo1.setValue(%selectionInfo);
    return ;
}
function TerrainEditor::offsetBrush(%this, %x, %y)
{
    %curPos = %this.getBrushPos();
    %this.setBrushPos(getWord(%curPos, 0) + %x, getWord(%curPos, 1) + %y);
    return ;
}
function TerrainEditor::swapInLoneMaterial(%this, %name)
{
    if (%this.baseMaterialsSwapped $= "true")
    {
        %this.baseMaterialsSwapped = "false";
        tEditor.popBaseMaterialInfo();
    }
    else
    {
        %this.baseMaterialsSwapped = "true";
        %this.pushBaseMaterialInfo();
        %this.setLoneBaseMaterial(%name);
    }
    flushTextureCache();
    return ;
}
function TELoadTerrainButton::onAction(%this)
{
    getLoadFilename("terrains/*.ter", %this @ ".gotFileName");
    return ;
}
function TELoadTerrainButton::gotFileName(%this, %name)
{
    %pos = "0 0 0";
    %squareSize = 8;
    %visibleDistance = 1200;
    if (isObject(Terrain))
    {
        %pos = Terrain.position;
        %squareSize = Terrain.squareSize;
        %visibleDistance = Terrain.visibleDistance;
        Terrain.delete();
    }
    new TerrainBlock(Terrain)
    {
        position = %pos;
        terrainFile = %name;
        squareSize = %squareSize;
        visibleDistance = %visibleDistance;
    };
    ETerrainEditor.attachTerrain();
    return ;
}
function TerrainEditorSettingsGui::onWake(%this)
{
    TESoftSelectFilter.setValue(ETerrainEditor.softSelectFilter);
    return ;
}
function TerrainEditorSettingsGui::onSleep(%this)
{
    ETerrainEditor.softSelectFilter = TESoftSelectFilter.getValue();
    return ;
}
function TESettingsApplyButton::onAction(%this)
{
    ETerrainEditor.softSelectFilter = TESoftSelectFilter.getValue();
    ETerrainEditor.resetSelWeights(1);
    ETerrainEditor.processAction("softSelect");
    return ;
}
function getPrefSetting(%pref, %default)
{
    if (%pref $= "")
    {
        return %default;
    }
    else
    {
        return %pref;
    }
    return ;
}
function onNeedRelight()
{
    if (RelightMessage.visible == 0)
    {
        RelightMessage.visible = 1;
    }
    return ;
}
function Editor::open(%this)
{
    if (Canvas.getContent() == GuiEditorGui.getId())
    {
        return ;
    }
    %this.prevContent = Canvas.getContent();
    Canvas.setContent(EditorGui);
    return ;
}
function Editor::close(%this)
{
    if ((%this.prevContent == -1) && (%this.prevContent $= ""))
    {
        %this.prevContent = "PlayGui";
    }
    Canvas.setContent(%this.prevContent);
    MessageHud.close();
    return ;
}
function EWorldEditor::updateGeneralInfo(%this, %optObj)
{
    %numSelected = %this.getSelectionSize();
    %color = "<color:886644>";
    if (%numSelected == 0)
    {
        if (%optObj $= "")
        {
            WorldEditorGeneralInfoMLText.setText(%color @ "(nothing selected)");
            return ;
        }
        else
        {
            %obj = %optObj;
        }
    }
    else
    {
        if (%numSelected > 1)
        {
            WorldEditorGeneralInfoMLText.setText(%color @ "(multi)");
            return ;
        }
        else
        {
            %obj = %this.getSelectedObject(0);
        }
    }
    %serverID = %clientID = -1;
    %serverValid = %clientValid = 0;
    %client = ClientDict.get($Player::Name);
    if (!isObject(%client))
    {
        error(getScopeName() @ "-> can\'t get $Player::Name\'s client object form ClientDict!");
    }
    if (%obj.isClassNetObject())
    {
        if (%obj.isServerObject())
        {
            %serverID = %obj.getId();
            %serverValid = 1;
            if (!isObject(%client))
            {
                %clientID = "(no client object for player)";
            }
            else
            {
                %ghostID = %client.getGhostID(%obj);
                if (%ghostID <= 0)
                {
                    %clientID = "no ghost.";
                }
                else
                {
                    %clientID = ServerConnection.resolveGhostID(%ghostID);
                    if (%clientID <= 0)
                    {
                        %clientID = "no ghost (server has ghostID, tho)";
                    }
                    else
                    {
                        %clientValid = 1;
                    }
                }
            }
        }
        else
        {
            if (%obj.isClientObject())
            {
                error(getScopeName() @ "-> unexpected: worldeditor has selected a client-side object! handling.");
                %clientID = %obj.getId();
                %clientValid = 1;
                %ghostID = ServerConnection.getGhostID(%clientID);
                if (%ghostID <= 0)
                {
                    %serverID = "client-side only.";
                }
                else
                {
                    if (!isObject(%client))
                    {
                        %serverID = "(no client object for player)";
                    }
                    else
                    {
                        %serverID = %client.ResolveGhost(%ghostID).getId();
                        if (%serverID <= 0)
                        {
                            %serverID = "no ghost (client has ghost ID, tho)";
                        }
                        else
                        {
                            %serverValid = 1;
                        }
                    }
                }
            }
            else
            {
                error(getScopeName() @ "-> net object which returns false on both isServer/ClientObject(), returning!");
                WorldEditorGeneralInfoMLText.setText(%color @ "(error see log!)");
            }
        }
    }
    else
    {
        %serverID = %obj;
        %serverValid = 1;
        %clientID = "Not a net object! (assumed serverside)";
    }
    %serverText = %serverValid ? %serverID.getDebugString() : %serverID;
    %clientText = %clientValid ? %clientID.getDebugString() : %clientID;
    %text = %color @ "<linkcolor:775533><linkcolorhl:ddff00>";
    %text = %text @ "<just:left>" @ "Server:<a:gamelink COPYTOCLIP " @ %serverValid ? %serverText : 1 @ ">" @ %serverText @ "</a>\n";
    %text = %text @ "Client:  <a:gamelink COPYTOCLIP " @ %clientValid ? %clientText : 1 @ ">" @ %clientText @ "</a>";
    WorldEditorGeneralInfoMLText.setText(%text);
    return ;
}
function WorldEditorGeneralInfoMLText::onUrl(%this, %url)
{
    %cmd = getWord(%url, 1);
    %restWords = getWords(%url, 2, 10000);
    if (%cmd $= "COPYTOCLIP")
    {
        setClipboard(%restWords);
    }
    return ;
}
