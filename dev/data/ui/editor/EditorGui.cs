$AIEdit = 0;
function EStatusHudActivator::onMouseEnter(%this) {
    if (!(EStatusHud.isShowing())) {
        EStatusHud.updateStatus();
    }
};
function EWorldEditor::onWake(%this) {
    EStatusHud.initialUpdateStatus();
    fxEts::updateExposureFilter();
};
function EWorldEditor::onCanvasResize(%this) {
    if (isObject(EStatusHud)) {
        EStatusHud.update();
    }
};
function toggleStatusHud() {
    if (EStatusHud.isShowing()) {
        EStatusHud.hide();
    }
    EStatusHud.show();
    1.keepOpen(EStatusHud);
};
function EStatusHud::GetSelectTypeDisplayText(%this, %type) {
    if ((%type $= $TypeMasks::ALLTYPES)) {
        return "All Types";
    }
    if ((%type $= $TypeMasks::TriggerObjectType)) {
        return "Triggers";
    }
    if ((%type $= $TypeMasks::InteriorObjectType)) {
        return "Interiors";
    }
    if ((%type $= $TypeMasks::MarkerObjectType)) {
        return "Audio Emitters";
    }
    if ((%type $= $TypeMasks::ShapeBaseObjectType)) {
        return "Shapes and Sit Markers";
    }
    if ((%type $= $TypeMasks::StaticTSObjectType)) {
        return "Shapes and Sit Markers";
    }
    if ((%type $= $TypeMasks::ItemObjectType)) {
        return "Items";
    }
    if ((%type $= $TypeMasks::AntiPortalObjectType)) {
        return "Antiportals";
    }
    return "All Types";
};
function EStatusHud::updateStatus(%this) {
    realUpdateStatus.schedule(EStatusHud, 100);
};
function EStatusHud::initialUpdateStatus(%this) {
    realUpdateStatus.schedule(EStatusHud, 1000);
};
function EStatusHud::realUpdateStatus(%this) {
    %c2 = "\x06";
    %selected = %c2 @ EWorldEditor.getSelectionSize() @ "\x02\x01 world objects selected";
    %seltype = "\x02\x01type: " @ %c2 @ EWorldEditor.selectType.GetSelectTypeDisplayText(%this);
    %addto = "\x02\x01addgroup: " @ %c2 @ $instantGroup @ ":" @ $instantGroup.getName();
    %viewerTrans = LocalClientConnection.getControlObject().getTransform();
    %camera = "\x02\x01cam pos: " @ %c2 @ "( " @ getWord(%viewerTrans, 0) @ " , " @ getWord(%viewerTrans, 1) @ " , " @ getWord(%viewerTrans, 2) @ " )";
    %scale = "\x02\x01move scale: " @ %c2 @ EWorldEditor.mouseMoveScale;
    %grid = "\x02\x01grid size: " @ %c2 @ EWorldEditor.gridSize @ "\x02\x01,  grid snap: " @ %c2 @ EWorldEditor.snapToGrid;
    %this.charWidth = mMax(strlen(%selected), strlen(%addto));
    %this.charWidth = mMax(strlen(this.charWidth), strlen(%seltype));
    %this.charWidth = mMax(strlen(this.charWidth), strlen(%camera));
    %this.charWidth = mMax(strlen(this.charWidth), strlen(%scale));
    %this.charWidth = mMax(strlen(this.charWidth), strlen(%grid));
    %text = %selected @ "\n" @ %seltype @ "\n" @ %addto @ "\n" @ %camera @ "\n" @ %scale @ "\n" @ %grid;
    %text.setStatusText(%this);
};
function EStatusHud::setStatusText(%this, %text) {
    %this.text = %text;
    %this.charWidth = strlen(%text);
    EStatusHud.show();
    %this.hideSchedule = %this.schedule(%this, 5000, tryHide);
};
function EStatusHud::tryHide(%this) {
    if (!($UserPref::WorldEditor::keepEStatusHudOpen)) {
        %this.hide();
        return 1;
    }
    return 0;
};
function EStatusHud::update(%this) {
    %heightOffset = 40;
    %resWidth = getWord($UserPref::Video::Resolution, 0);
    if ((%resWidth <= 480.0)) {
        %widthMultiplier = 7.2;
        %heightOffset = (%heightOffset + 8.0);
        %heightDelta = 4;
        "MusicMLTextProfileSmall".setProfile(EStatusText);
    }
    if ((%resWidth <= 640.0)) {
        %widthMultiplier = 8.1;
        %heightOffset = (%heightOffset + 4.0);
        %heightDelta = 2;
        "MusicMLTextProfileMedium".setProfile(EStatusText);
    }
    %widthMultiplier = 9.0;
    %heightDelta = 0;
    "MusicMLTextProfile".setProfile(EStatusText);
    %content = "";
    if (!(%this.text $= "")) {
        %content = %this.text @ "\n";
    }
    %content.setText(EStatusText);
    %height = getWord(%this.extent, 1);
    %targetWidth = mCeil((%widthMultiplier * mMax(%this.charWidth, 20)));
    %height.setTrgExtent(%this, %targetWidth);
    %height.resize(EStatusText, 0, 0, %targetWidth);
    %this.updatePosition();
};
function EStatusHud::updatePosition(%this) {
    %trgX = getWord(%this.getTrgPosition(), 0);
    %trgY = (((getWord(ButtonBar.getTrgPosition(), 1) - getWord(%this.getExtent(), 1)) + $ButtonBarVar::VerticalAdjustment) + 12.0);
    %trgY.setTrgPosition(%this, %trgX);
};
function EStatusHud::show(%this) {
    if (%this.hideSchedule) {
        cancel(%this.hideSchedule);
    }
    %trgY = getWord(%this.getTrgPosition(), 1);
    %trgY.setTrgPosition(%this, 4);
    %this.update();
};
function EStatusHud::keepOpen(%this, %flag) {
    $UserPref::WorldEditor::keepEStatusHudOpen = %flag;
};
function EStatusHud::hide(%this) {
    %this.updatePosition();
    %width = getWord(%this.getTrgExtent(), 0);
    %trgY = getWord(%this.getTrgPosition(), 1);
    %trgY.setTrgPosition(%this, -(%width));
    0.keepOpen(%this);
};
function EStatusHud::isShowing(%this) {
    return (getWord(%this.position, 0) >= 0.0);
};
function EStatusHud::onMouseLeaveBounds(%this) {
    if (!(%this.tryHide())) {
        %this.updatePosition();
    }
};
function EStatusHud::onMouseEnter(%this) {
    %this.onMouseEnterBounds();
};
function EStatusHud::onMouseEnterBounds(%this) {
    if (%this.isShowing()) {
        %posX = getWord(%this.position, 0);
        %posY = getWord(%this.position, 1);
        %posY.setTrgPosition(%this, %posX);
    }
};
$sgEditorItemNames::sgMenu = "Synapse Gaming Tools";
$sgEditorItemNames::sgMenu[$sgEditorItemNames::sgMenuItem @ 0] = "Lighting Pack Light Editor";
function EditorGui::getPrefs() {
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
};
function EditorGui::setPrefs() {
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
};
function EditorGui::onSleep(%this) {
    %this.setPrefs();
};
function EditorGui::init(%this) {
    %this.getPrefs();
    if (!(isObject("terraformer"))) {
        new Terraformer("terraformer");
    }
    $SelectedOperation = -(1.0);
    $NextOperationId = 1;
    $HeightfieldDirtyRow = -(1.0);
    EditorMenuBar.clearMenus();
    0.addMenu(EditorMenuBar, "File");
    1.addMenuItem(EditorMenuBar, "File", "New Mission...");
    "Ctrl O".addMenuItem(EditorMenuBar, "File", "Open Mission...", 2);
    "Ctrl S".addMenuItem(EditorMenuBar, "File", "Save Mission...", 3);
    4.addMenuItem(EditorMenuBar, "File", "Save Mission As...");
    0.addMenuItem(EditorMenuBar, "File", "-");
    6.addMenuItem(EditorMenuBar, "File", "Import Terraform Data...");
    5.addMenuItem(EditorMenuBar, "File", "Import Texture Data...");
    0.addMenuItem(EditorMenuBar, "File", "-");
    7.addMenuItem(EditorMenuBar, "File", "Refresh File List");
    0.addMenuItem(EditorMenuBar, "File", "-");
    5.addMenuItem(EditorMenuBar, "File", "Export Terraform Bitmap...");
    1.addMenu(EditorMenuBar, "Edit");
    "Ctrl Z".addMenuItem(EditorMenuBar, "Edit", "Undo", 1);
    1.setMenuItemBitmap(EditorMenuBar, "Edit", "Undo");
    "Ctrl R".addMenuItem(EditorMenuBar, "Edit", "Redo", 2);
    2.setMenuItemBitmap(EditorMenuBar, "Edit", "Redo");
    0.addMenuItem(EditorMenuBar, "Edit", "-");
    "Ctrl X".addMenuItem(EditorMenuBar, "Edit", "Cut", 3);
    3.setMenuItemBitmap(EditorMenuBar, "Edit", "Cut");
    "Ctrl C".addMenuItem(EditorMenuBar, "Edit", "Copy", 4);
    4.setMenuItemBitmap(EditorMenuBar, "Edit", "Copy");
    "Ctrl V".addMenuItem(EditorMenuBar, "Edit", "Paste", 5);
    5.setMenuItemBitmap(EditorMenuBar, "Edit", "Paste");
    0.addMenuItem(EditorMenuBar, "Edit", "-");
    "Ctrl A".addMenuItem(EditorMenuBar, "Edit", "Select All", 6);
    "Ctrl N".addMenuItem(EditorMenuBar, "Edit", "Select None", 7);
    "Ctrl D".addMenuItem(EditorMenuBar, "Edit", "Select None", 8);
    "Ctrl-Shift I".addMenuItem(EditorMenuBar, "Edit", "Select Inverse", 9);
    "Ctrl F".addMenuItem(EditorMenuBar, "Edit", "Find Selected", 10);
    "Shift F".addMenuItem(EditorMenuBar, "Edit", "Zoom Camera To Selection", 11);
    "Ctrl E".addMenuItem(EditorMenuBar, "Edit", "Expand Selected Tree", 12);
    "Shift E".addMenuItem(EditorMenuBar, "Edit", "Expand And Select Selected Tree", 13);
    0.addMenuItem(EditorMenuBar, "Edit", "-");
    "Alt L".addMenuItem(EditorMenuBar, "Edit", "Relight Scene", 14);
    0.addMenuItem(EditorMenuBar, "Edit", "-");
    12.addMenuItem(EditorMenuBar, "Edit", "World Editor Settings...");
    13.addMenuItem(EditorMenuBar, "Edit", "Terrain Editor Settings...");
    0.addMenuItem(EditorMenuBar, "Edit", "-");
    "]".addMenuItem(EditorMenuBar, "Edit", "Increase Move Scale", 15);
    "[".addMenuItem(EditorMenuBar, "Edit", "Decrease Move Scale", 16);
    "g".addMenuItem(EditorMenuBar, "Edit", "Toggle Grid Visibility", 17);
    "s".addMenuItem(EditorMenuBar, "Edit", "Status Hud Toggle", 17);
    7.addMenu(EditorMenuBar, "Camera");
    "Alt Q".addMenuItem(EditorMenuBar, "Camera", "Drop Camera at Player", 1);
    "Alt W".addMenuItem(EditorMenuBar, "Camera", "Drop Player at Camera", 2);
    "Alt C".addMenuItem(EditorMenuBar, "Camera", "Toggle Camera", 10);
    "Alt T".addMenuItem(EditorMenuBar, "Camera", "Drop Camera at Selection", 19);
    0.addMenuItem(EditorMenuBar, "Camera", "-");
    1.addMenuItem(EditorMenuBar, "Camera", "Slowest", 3, "Shift 1");
    1.addMenuItem(EditorMenuBar, "Camera", "Very Slow", 4, "Shift 2");
    1.addMenuItem(EditorMenuBar, "Camera", "Slow", 5, "Shift 3");
    1.addMenuItem(EditorMenuBar, "Camera", "Medium Pace", 6, "Shift 4");
    1.addMenuItem(EditorMenuBar, "Camera", "Fast", 7, "Shift 5");
    1.addMenuItem(EditorMenuBar, "Camera", "Very Fast", 8, "Shift 6");
    1.addMenuItem(EditorMenuBar, "Camera", "Fastest", 9, "Shift 7");
    6.addMenu(EditorMenuBar, "World");
    "Ctrl L".addMenuItem(EditorMenuBar, "World", "Lock Selection", 10);
    "Ctrl Shift L".addMenuItem(EditorMenuBar, "World", "Unlock Selection", 11);
    0.addMenuItem(EditorMenuBar, "World", "-");
    "Ctrl H".addMenuItem(EditorMenuBar, "World", "Hide Selected", 12);
    "Shift H".addMenuItem(EditorMenuBar, "World", "Unhide Selected", 13);
    "Ctrl J".addMenuItem(EditorMenuBar, "World", "Invert Hidden", 14);
    0.addMenuItem(EditorMenuBar, "World", "-");
    "Delete".addMenuItem(EditorMenuBar, "World", "Delete Selection", 17);
    15.addMenuItem(EditorMenuBar, "World", "Reset Transforms");
    "Ctrl Shift D".addMenuItem(EditorMenuBar, "World", "Drop Selection", 16);
    17.addMenuItem(EditorMenuBar, "World", "Add Selection to Instant Group");
    "N".addMenuItem(EditorMenuBar, "World", "SimGroup Create", 18);
    0.addMenuItem(EditorMenuBar, "World", "-");
    1.addMenuItem(EditorMenuBar, "World", "Drop at Origin", 0, "");
    1.addMenuItem(EditorMenuBar, "World", "Drop at Camera", 1, "");
    1.addMenuItem(EditorMenuBar, "World", "Drop at Camera w/Rot", 2, "");
    1.addMenuItem(EditorMenuBar, "World", "Drop below Camera", 3, "");
    1.addMenuItem(EditorMenuBar, "World", "Drop at Screen Center", 4, "");
    1.addMenuItem(EditorMenuBar, "World", "Drop at Centroid", 5, "");
    1.addMenuItem(EditorMenuBar, "World", "Drop to Ground", 6, "");
    9.addMenu(EditorMenuBar, "SnapTo");
    "X".addMenuItem(EditorMenuBar, "SnapTo", "X", 1);
    "Shift X".addMenuItem(EditorMenuBar, "SnapTo", "X+", 2);
    "Alt X".addMenuItem(EditorMenuBar, "SnapTo", "X-", 3);
    "Y".addMenuItem(EditorMenuBar, "SnapTo", "Y", 4);
    "Shift Y".addMenuItem(EditorMenuBar, "SnapTo", "Y+", 5);
    "Alt Y".addMenuItem(EditorMenuBar, "SnapTo", "Y-", 6);
    "Z".addMenuItem(EditorMenuBar, "SnapTo", "Z", 7);
    "Shift Z".addMenuItem(EditorMenuBar, "SnapTo", "Z+", 8);
    "Alt Z".addMenuItem(EditorMenuBar, "SnapTo", "Z-", 9);
    "numpad6".addMenuItem(EditorMenuBar, "SnapTo", "X+YZ", 10);
    "numpad4".addMenuItem(EditorMenuBar, "SnapTo", "X-YZ", 11);
    "numpad8".addMenuItem(EditorMenuBar, "SnapTo", "XY+Z", 12);
    "numpad2".addMenuItem(EditorMenuBar, "SnapTo", "XY-Z", 13);
    "numpad9".addMenuItem(EditorMenuBar, "SnapTo", "XYZ+", 12);
    "numpad3".addMenuItem(EditorMenuBar, "SnapTo", "XYZ-", 13);
    "shift numpad6".addMenuItem(EditorMenuBar, "SnapTo", "ObjX+YZ", 14);
    "shift numpad4".addMenuItem(EditorMenuBar, "SnapTo", "ObjX-YZ", 15);
    "shift numpad8".addMenuItem(EditorMenuBar, "SnapTo", "ObjXY+Z", 16);
    "shift numpad2".addMenuItem(EditorMenuBar, "SnapTo", "ObjXY-Z", 17);
    "shift numpad9".addMenuItem(EditorMenuBar, "SnapTo", "ObjXYZ+", 18);
    "shift numpad3".addMenuItem(EditorMenuBar, "SnapTo", "ObjXYZ-", 19);
    10.addMenu(EditorMenuBar, "CloneTo");
    "alt numpad6".addMenuItem(EditorMenuBar, "CloneTo", "X+YZ", 1);
    "alt numpad4".addMenuItem(EditorMenuBar, "CloneTo", "X-YZ", 2);
    "alt numpad8".addMenuItem(EditorMenuBar, "CloneTo", "XY+Z", 3);
    "alt numpad2".addMenuItem(EditorMenuBar, "CloneTo", "XY-Z", 4);
    "alt numpad9".addMenuItem(EditorMenuBar, "CloneTo", "XYZ+", 5);
    "alt numpad3".addMenuItem(EditorMenuBar, "CloneTo", "XYZ-", 6);
    3.addMenu(EditorMenuBar, "Action");
    1.addMenuItem(EditorMenuBar, "Action", "Select", 1, "");
    1.addMenuItem(EditorMenuBar, "Action", "Adjust Selection", 2, "");
    0.addMenuItem(EditorMenuBar, "Action", "-");
    1.addMenuItem(EditorMenuBar, "Action", "Add Dirt", 6, "");
    1.addMenuItem(EditorMenuBar, "Action", "Excavate", 6, "");
    1.addMenuItem(EditorMenuBar, "Action", "Adjust Height", 6, "");
    1.addMenuItem(EditorMenuBar, "Action", "Flatten", 4, "");
    1.addMenuItem(EditorMenuBar, "Action", "Smooth", 5, "");
    1.addMenuItem(EditorMenuBar, "Action", "Set Height", 7, "");
    0.addMenuItem(EditorMenuBar, "Action", "-");
    1.addMenuItem(EditorMenuBar, "Action", "Set Empty", 8, "");
    1.addMenuItem(EditorMenuBar, "Action", "Clear Empty", 8, "");
    0.addMenuItem(EditorMenuBar, "Action", "-");
    1.addMenuItem(EditorMenuBar, "Action", "Paint Material", 9, "");
    4.addMenu(EditorMenuBar, "Brush");
    1.addMenuItem(EditorMenuBar, "Brush", "Box Brush", 91, "");
    1.addMenuItem(EditorMenuBar, "Brush", "Circle Brush", 92, "");
    0.addMenuItem(EditorMenuBar, "Brush", "-");
    2.addMenuItem(EditorMenuBar, "Brush", "Soft Brush", 93, "");
    2.addMenuItem(EditorMenuBar, "Brush", "Hard Brush", 94, "");
    0.addMenuItem(EditorMenuBar, "Brush", "-");
    3.addMenuItem(EditorMenuBar, "Brush", "Size 1 x 1", 1, "Alt 1");
    3.addMenuItem(EditorMenuBar, "Brush", "Size 3 x 3", 3, "Alt 2");
    3.addMenuItem(EditorMenuBar, "Brush", "Size 5 x 5", 5, "Alt 3");
    3.addMenuItem(EditorMenuBar, "Brush", "Size 9 x 9", 9, "Alt 4");
    3.addMenuItem(EditorMenuBar, "Brush", "Size 15 x 15", 15, "Alt 5");
    3.addMenuItem(EditorMenuBar, "Brush", "Size 25 x 25", 25, "Alt 6");
    2.addMenu(EditorMenuBar, "Window");
    1.addMenuItem(EditorMenuBar, "Window", "World Editor", 2, "F2");
    1.addMenuItem(EditorMenuBar, "Window", "World Editor Inspector", 3, "F3");
    1.addMenuItem(EditorMenuBar, "Window", "World Editor Creator", 4, "F4");
    1.addMenuItem(EditorMenuBar, "Window", "Mission Area Editor", 5, "F5");
    0.addMenuItem(EditorMenuBar, "Window", "-");
    1.addMenuItem(EditorMenuBar, "Window", "Terrain Editor", 6, "F6");
    1.addMenuItem(EditorMenuBar, "Window", "Terrain Terraform Editor", 7, "F7");
    1.addMenuItem(EditorMenuBar, "Window", "Terrain Texture Editor", 8, "F8");
    1.addMenuItem(EditorMenuBar, "Window", "Terrain Texture Painter", 9, "");
    %selectMenuName = "Select Type";
    %n = 1;
    11.addMenu(EditorMenuBar, %selectMenuName);
    1.addMenuItem(EditorMenuBar, %selectMenuName, "All Types", %n, "Ctrl 1");
    %n = (%n + 1.0);
    1.addMenuItem(EditorMenuBar, %selectMenuName, "Triggers", %n, "Ctrl 2");
    %n = (%n + 1.0);
    1.addMenuItem(EditorMenuBar, %selectMenuName, "Interiors", %n, "Ctrl 3");
    %n = (%n + 1.0);
    1.addMenuItem(EditorMenuBar, %selectMenuName, "Shapes and Sit Markers", %n, "Ctrl 4");
    %n = (%n + 1.0);
    1.addMenuItem(EditorMenuBar, %selectMenuName, "Antiportals", %n, "Ctrl 5");
    %n = (%n + 1.0);
    1.addMenuItem(EditorMenuBar, %selectMenuName, "Items", %n, "ctrl 6");
    %n = (%n + 1.0);
    1.addMenuItem(EditorMenuBar, %selectMenuName, "Audio Emitters", %n, "Ctrl 7");
    %n = (%n + 1.0);
    %n.addMenuItem(EditorMenuBar, %selectMenuName, "-");
    %n = (%n + 1.0);
    %n.addMenuItem(EditorMenuBar, %selectMenuName, "Select all AdvertShapes");
    %n = (%n + 1.0);
    %n.addMenuItem(EditorMenuBar, %selectMenuName, "Select all AIPlayers");
    %n = (%n + 1.0);
    %n.addMenuItem(EditorMenuBar, %selectMenuName, "Select all ETSSeatMarker");
    %n = (%n + 1.0);
    %n.addMenuItem(EditorMenuBar, %selectMenuName, "Select all InteriorInstances");
    %n = (%n + 1.0);
    %n.addMenuItem(EditorMenuBar, %selectMenuName, "Select all Markers");
    %n = (%n + 1.0);
    %n.addMenuItem(EditorMenuBar, %selectMenuName, "Select all MissionMarkers");
    %n = (%n + 1.0);
    %n.addMenuItem(EditorMenuBar, %selectMenuName, "Select all StaticShapes");
    %n = (%n + 1.0);
    %n.addMenuItem(EditorMenuBar, %selectMenuName, "Select all sgUniversalStaticLights");
    %n = (%n + 1.0);
    %n.addMenuItem(EditorMenuBar, %selectMenuName, "Select all Triggers");
    %n = (%n + 1.0);
    %n.addMenuItem(EditorMenuBar, %selectMenuName, "Select all TSStatics");
    %n = (%n + 1.0);
    %n.addMenuItem(EditorMenuBar, %selectMenuName, "Select all Waterblocks");
    %n = (%n + 1.0);
    %debugMenuName = "Render Mode";
    11.addMenu(EditorMenuBar, %debugMenuName);
    1.addMenuItem(EditorMenuBar, %debugMenuName, "normal", 1, "Shift N");
    1.addMenuItem(EditorMenuBar, %debugMenuName, "lines", 2, "Shift F2");
    1.addMenuItem(EditorMenuBar, %debugMenuName, "detail polys", 3, "Shift F3");
    1.addMenuItem(EditorMenuBar, %debugMenuName, "portal zones", 4, "Shift F4");
    1.addMenuItem(EditorMenuBar, %debugMenuName, "null surfaces", 5, "Shift F5");
    1.addMenuItem(EditorMenuBar, %debugMenuName, "portal zones nonRoot", 6, "Shift F6");
    1.addMenuItem(EditorMenuBar, %debugMenuName, "zonesNonRoot, Detail", 7, "Shift F7");
    1.addMenuItem(EditorMenuBar, %debugMenuName, "large textures", 8, "Shift F8");
    1.addMenuItem(EditorMenuBar, %debugMenuName, "detail level", 9, "Shift F9");
    1.addMenuItem(EditorMenuBar, %debugMenuName, "lightmap", 10, "Shift F10");
    1.addMenuItem(EditorMenuBar, %debugMenuName, "only textures", 11, "Shift F11");
    1.addMenuItem(EditorMenuBar, %debugMenuName, "triangle strips", 12, "Shift F12");
    1.addMenuItem(EditorMenuBar, %debugMenuName, "next mode", 13, "=");
    1.addMenuItem(EditorMenuBar, %debugMenuName, "prev mode", 14, "-");
    8.addMenu(EditorMenuBar, $sgEditorItemNames::sgMenu);
    "F12".addMenuItem(EditorMenuBar, $sgEditorItemNames::sgMenu, , 2);
    "Adjust Height".onActionMenuItemSelect(EditorMenuBar, 0);
    "Circle Brush".onBrushMenuItemSelect(EditorMenuBar, 0);
    "Soft Brush".onBrushMenuItemSelect(EditorMenuBar, 0);
    "Size 9 x 9".onBrushMenuItemSelect(EditorMenuBar, 9);
    "Medium Pace".onCameraMenuItemSelect(EditorMenuBar, 6);
    "Drop at Screen Center".onWorldMenuItemSelect(EditorMenuBar, 0);
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
};
function EditorNewMission() {
    if (ETerrainEditor.isMissionDirty) {
    }
    if (ETerrainEditor.isDirty) {
    }
    if (EWorldEditor.isDirty) {
    }
    if (EditorTree.isDirty) {
        MessageBoxYesNo("Mission Modified", "Would you like to save changes to the current mission \"" @ $Server::MissionFile @ "\" before creating a new mission?", "EditorDoNewMission(true);", "EditorDoNewMission(false);");
    }
    EditorDoNewMission(0);
};
function EditorSaveMissionMenu() {
    if (EditorGui.saveAs) {
        EditorSaveMissionAs();
    }
    EditorSaveMission();
};
function EditorSaveMission() {
    if (EWorldEditor.isDirty) {
    }
    if (EditorTree.isDirty) {
    }
    if (ETerrainEditor.isMissionDirty) {
    }
    if (!(isWriteableFileName($Server::MissionFile))) {
        MessageBoxOK("Error", "Mission file \"" @ $Server::MissionFile @ "\" is read-only.", "");
        return 0;
    }
    if (ETerrainEditor.isDirty) {
    }
    if (!(isWriteableFileName(Terrain.terrainFile))) {
        MessageBoxOK("Error", "Terrain file \"" @ Terrain.terrainFile @ "\" is read-only.", "");
        return 0;
    }
    %errorCount = RunTestCase("TEST_MISSIONGROUPINTEGRITY", "WARNING: About that mission file you just saved...");
    if (EWorldEditor.isDirty) {
    }
    if (EditorTree.isDirty) {
    }
    if (ETerrainEditor.isMissionDirty) {
        if ((MissionInfo.mode $= "PrivateSpaceDesign")) {
            if (isObject(PRIVATESPACE_GROUP)) {
                PRIVATESPACE_GROUP.add(RootGroup);
            }
            error("PrivateSpaceDesign mode, no PRIVATESPACE_GROUP object");
        }
        $Server::MissionFile.save(MissionGroup);
        if ((MissionInfo.mode $= "PrivateSpaceDesign") && isObject(PRIVATESPACE_GROUP)) {
            %spaceForGridFileName = getSubStr($Server::MissionFile, 0, (strlen($Server::MissionFile) - 4.0)) @ "_generated.cs";
            %spaceForGridFileName.save(PRIVATESPACE_GROUP);
            echo("PrivateSpaceDesign mode saving to space for grid, file named:" @ " " @ %spaceForGridFileName);
            PRIVATESPACE_GROUP.add(MissionGroup);
        }
    }
    if ((MissionInfo.mode $= "PrivateSpaceDesign") && isObject(PRIVATESPACE_GROUP)) {
        %spaceForGridFileName = getSubStr($Server::MissionFile, 0, (strlen($Server::MissionFile) - 4.0)) @ "_generated.cs";
        %spaceForGridFileName.save(PRIVATESPACE_GROUP);
        echo("PrivateSpaceDesign mode saving to space for grid, file named:" @ " " @ %spaceForGridFileName);
    }
    if (ETerrainEditor.isDirty) {
        Terrain.terrainFile.save(Terrain);
    }
    EditorTree.isDirty = 0;
    EWorldEditor.isDirty = 0;
    ETerrainEditor.isDirty = 0;
    ETerrainEditor.isMissionDirty = 0;
    EditorGui.saveAs = 0;
    return 1;
};
function EditorDoSaveAs(%missionName) {
    ETerrainEditor.isDirty = 1;
    EWorldEditor.isDirty = 1;
    EditorTree.isDirty = 1;
    %saveMissionFile = $Server::MissionFile;
    %saveTerrName = Terrain.terrainFile;
    $Server::MissionFile = %missionName;
    Terrain.terrainFile = filePath(%missionName) @ "/" @ fileBase(%missionName) @ ".ter";
    if (!(EditorSaveMission())) {
        $Server::MissionFile = %saveMissionFile;
        Terrain.terrainFile = %saveTerrName;
    }
};
function EditorSaveMissionAs() {
    getSaveFilename("*.mis", "EditorDoSaveAs", $Server::MissionFile);
};
function EditorDoLoadMission(%file) {
    Editor.close();
    loadMission(%file, 1);
    Editor::Create();
    Editor.add(MissionCleanup);
    EditorGui.loadingMission = 1;
    Editor.open();
};
function EditorSaveBeforeLoad() {
    if (EditorSaveMission()) {
        getLoadFilename("*.mis", "EditorDoLoadMission");
    }
};
function EditorDoNewMission(%saveFirst) {
    if (%saveFirst) {
        EditorSaveMission();
    }
    %file = findFirstFile("*/newMission.mis");
    if ((%file $= "")) {
        MessageBoxOK("Error", "Missing mission template \"newMission.mis\".", "");
        return;
    }
    EditorDoLoadMission(%file);
    EditorGui.saveAs = 1;
    EWorldEditor.isDirty = 1;
    ETerrainEditor.isDirty = 1;
    EditorTree.isDirty = 1;
};
function EditorOpenMission() {
    if (ETerrainEditor.isMissionDirty) {
    }
    if (ETerrainEditor.isDirty) {
    }
    if (EWorldEditor.isDirty) {
    }
    if (EditorTree.isDirty) {
        MessageBoxYesNo("Mission Modified", "Would you like to save changes to the current mission \"" @ $Server::MissionFile @ "\" before opening a new mission?", "EditorSaveBeforeLoad();", "getLoadFilename(\"*.mis\", \"EditorDoLoadMission\");");
    }
    getLoadFilename("*.mis", "EditorDoLoadMission");
};
function EditorMenuBar::onMenuSelect(%this, %unused, %menu) {
    if ((%menu $= "File")) {
        if (ETerrainEditor.isVisible()) {
        }
        %editingHeightfield = EHeightField.isVisible();
        %editingHeightfield.setMenuItemEnable(EditorMenuBar, "File", "Export Terraform Bitmap...");
        if (ETerrainEditor.isDirty) {
        }
        if (ETerrainEditor.isMissionDirty) {
        }
        if (EWorldEditor.isDirty) {
        }
        EditorTree.isDirty.setMenuItemEnable(EditorMenuBar, "File", "Save Mission...");
    }
    if ((%menu $= "Edit")) {
        %selSize = EWorldEditor.getSelectionSize();
        (%selSize > 0.0).setMenuItemEnable(EditorMenuBar, "Edit", "Zoom Camera To Selection");
        if (EWorldEditor.isVisible()) {
            1.setMenuItemEnable(EditorMenuBar, "Edit", "Select All");
            EWorldEditor.canPasteSelection().setMenuItemEnable(EditorMenuBar, "Edit", "Paste");
            %canCutCopy = (EWorldEditor.getSelectionSize() > 0.0);
            %canCutCopy.setMenuItemEnable(EditorMenuBar, "Edit", "Cut");
            %canCutCopy.setMenuItemEnable(EditorMenuBar, "Edit", "Copy");
        }
        if (ETerrainEditor.isVisible()) {
            0.setMenuItemEnable(EditorMenuBar, "Edit", "Cut");
            0.setMenuItemEnable(EditorMenuBar, "Edit", "Copy");
            0.setMenuItemEnable(EditorMenuBar, "Edit", "Paste");
            0.setMenuItemEnable(EditorMenuBar, "Edit", "Select All");
        }
    }
    if ((%menu $= "World")) {
        %selSize = EWorldEditor.getSelectionSize();
        %lockCount = EWorldEditor.getSelectionLockCount();
        %hideCount = EWorldEditor.getSelectionHiddenCount();
        (%lockCount < %selSize).setMenuItemEnable(EditorMenuBar, "World", "Lock Selection");
        (%lockCount > 0.0).setMenuItemEnable(EditorMenuBar, "World", "Unlock Selection");
        (%hideCount < %selSize).setMenuItemEnable(EditorMenuBar, "World", "Hide Selected");
        (%hideCount > 0.0).setMenuItemEnable(EditorMenuBar, "World", "Unhide Selected");
        (%selSize > 0.0).setMenuItemEnable(EditorMenuBar, "World", "Invert Hidden");
        (%selSize > 0.0).setMenuItemEnable(EditorMenuBar, "World", "Add Selection to Instant Group");
        if ((%selSize > 0.0)) {
        }
        (%lockCount == 0.0).setMenuItemEnable(EditorMenuBar, "World", "Reset Transforms");
        if ((%selSize > 0.0)) {
        }
        (%lockCount == 0.0).setMenuItemEnable(EditorMenuBar, "World", "Drop Selection");
        if ((%selSize > 0.0)) {
        }
        (%lockCount == 0.0).setMenuItemEnable(EditorMenuBar, "World", "Delete Selection");
    }
};
function EditorMenuBar::onMenuItemSelect(%this, %unused, %menu, %itemId, %item) {
    if ((%menu $= "File")) {
        %item.onFileMenuItemSelect(%this, %itemId);
    }
    if ((%menu $= "Edit")) {
        %item.onEditMenuItemSelect(%this, %itemId);
    }
    if ((%menu $= "World")) {
        %item.onWorldMenuItemSelect(%this, %itemId);
    }
    if ((%menu $= "Window")) {
        %item.onWindowMenuItemSelect(%this, %itemId);
    }
    if ((%menu $= "Select Type")) {
        %item.onSelectTypeMenuItemSelect(%this, %itemId);
    }
    if ((%menu $= "Render Mode")) {
        %item.onRenderModeMenuItemSelect(%this, %itemId);
    }
    if ((%menu $= "Action")) {
        %item.onActionMenuItemSelect(%this, %itemId);
    }
    if ((%menu $= "Brush")) {
        %item.onBrushMenuItemSelect(%this, %itemId);
    }
    if ((%menu $= "Camera")) {
        %item.onCameraMenuItemSelect(%this, %itemId);
    }
    if ((%menu $= "SnapTo")) {
        %item.OnSnapToMenuItemSelect(%this, %itemId);
    }
    if ((%menu $= "CloneTo")) {
        %item.OnCloneToMenuItemSelect(%this, %itemId);
    }
    if ((%menu $= $sgEditorItemNames::sgMenu)) {
        %item.onToggleSGTools(%this, %itemId);
    }
};
function refreshFileList() {
    setModPaths(getModPaths());
    Creator.init();
};
function EditorMenuBar::onFileMenuItemSelect(%this, %itemId, %item) {
    if ((%item $= "New Mission...")) {
        EditorNewMission();
    }
    if ((%item $= "Open Mission...")) {
        EditorOpenMission();
    }
    if ((%item $= "Save Mission...")) {
        EditorSaveMissionMenu();
    }
    if ((%item $= "Save Mission As...")) {
        EditorSaveMissionAs();
    }
    if ((%item $= "Import Texture Data...")) {
        texture::import();
    }
    if ((%item $= "Import Terraform Data...")) {
        Heightfield::import();
    }
    if ((%item $= "Export Terraform Bitmap...")) {
        Heightfield::saveBitmap("");
    }
    if ((%item $= "Refresh File List")) {
        refreshFileList();
    }
};
function EditorMenuBar::onCameraMenuItemSelect(%this, %itemId, %item) {
    if ((%item $= "Drop Camera at Player")) {
        commandToServer('dropCameraAtPlayer');
    }
    if ((%item $= "Drop Player at Camera")) {
        commandToServer('DropPlayerAtCamera');
    }
    if ((%item $= "Toggle Camera")) {
        commandToServer('ToggleCamera');
    }
    if ((%item $= "Drop Camera at Selection")) {
        EWorldEditor.dropCameraWithSelectionInView();
    }
    1.setMenuItemChecked(%this, "Camera", %itemId);
    $Camera::movementSpeed = ((((%itemId - 3.0) / 6.0) * 195.0) + 5.0);
};
function EditorMenuBar::onActionMenuItemSelect(%this, %itemId, %item) {
    1.setMenuItemChecked(EditorMenuBar, "Action", %item);
    if ((%item $= "Select")) {
        ETerrainEditor.currentMode = "select";
        ETerrainEditor.selectionHidden = 0;
        ETerrainEditor.renderVertexSelection = 1;
        "select".setAction(ETerrainEditor);
    }
    if ((%item $= "Adjust Selection")) {
        ETerrainEditor.currentMode = "adjust";
        ETerrainEditor.selectionHidden = 0;
        "adjustHeight".setAction(ETerrainEditor);
        ETerrainEditor.currentAction = brushAdjustHeight;
        ETerrainEditor.renderVertexSelection = 1;
    }
    ETerrainEditor.currentMode = "paint";
    ETerrainEditor.selectionHidden = 1;
    ETerrainEditor.currentAction.setAction(ETerrainEditor);
    if ((%item $= "Add Dirt")) {
        ETerrainEditor.currentAction = raiseHeight;
        ETerrainEditor.renderVertexSelection = 1;
    }
    if ((%item $= "Paint Material")) {
        ETerrainEditor.currentAction = paintMaterial;
        ETerrainEditor.renderVertexSelection = 1;
    }
    if ((%item $= "Excavate")) {
        ETerrainEditor.currentAction = lowerHeight;
        ETerrainEditor.renderVertexSelection = 1;
    }
    if ((%item $= "Set Height")) {
        ETerrainEditor.currentAction = setHeight;
        ETerrainEditor.renderVertexSelection = 1;
    }
    if ((%item $= "Adjust Height")) {
        ETerrainEditor.currentAction = brushAdjustHeight;
        ETerrainEditor.renderVertexSelection = 1;
    }
    if ((%item $= "Flatten")) {
        ETerrainEditor.currentAction = flattenHeight;
        ETerrainEditor.renderVertexSelection = 1;
    }
    if ((%item $= "Smooth")) {
        ETerrainEditor.currentAction = smoothHeight;
        ETerrainEditor.renderVertexSelection = 1;
    }
    if ((%item $= "Set Empty")) {
        ETerrainEditor.currentAction = setEmpty;
        ETerrainEditor.renderVertexSelection = 0;
    }
    if ((%item $= "Clear Empty")) {
        ETerrainEditor.currentAction = clearEmpty;
        ETerrainEditor.renderVertexSelection = 0;
    }
    if ((ETerrainEditor.currentMode $= "select")) {
        ETerrainEditor.currentAction.processAction(ETerrainEditor);
    }
    if ((ETerrainEditor.currentMode $= "paint")) {
        ETerrainEditor.currentAction.setAction(ETerrainEditor);
    }
};
function EditorMenuBar::onBrushMenuItemSelect(%this, %itemId, %item) {
    1.setMenuItemChecked(EditorMenuBar, "Brush", %item);
    if ((%item $= "Box Brush")) {
        box.setBrushType(ETerrainEditor);
    }
    if ((%item $= "Circle Brush")) {
        ellipse.setBrushType(ETerrainEditor);
    }
    if ((%item $= "Soft Brush")) {
        ETerrainEditor.enableSoftBrushes = 1;
    }
    if ((%item $= "Hard Brush")) {
        ETerrainEditor.enableSoftBrushes = 0;
    }
    ETerrainEditor.brushSize = %itemId;
    %itemId.setBrushSize(ETerrainEditor, %itemId);
};
function EditorMenuBar::onRenderModeMenuItemSelect(%this, %itemId, %item) {
    1.setMenuItemChecked(EditorMenuBar, "Render Mode", %item);
    if ((%item $= "normal")) {
        setInteriorRenderMode(0);
    }
    if ((%item $= "lines")) {
        setInteriorRenderMode(1);
    }
    if ((%item $= "detail polys")) {
        setInteriorRenderMode(2);
    }
    if ((%item $= "portal zones")) {
        setInteriorRenderMode(7);
    }
    if ((%item $= "null surfaces")) {
        setInteriorRenderMode(11);
    }
    if ((%item $= "portal zones nonRoot")) {
        setInteriorRenderMode(17);
    }
    if ((%item $= "zonesNonRoot, Detail")) {
        setInteriorRenderMode(18);
    }
    if ((%item $= "large textures")) {
        setInteriorRenderMode(12);
    }
    if ((%item $= "detail level")) {
        setInteriorRenderMode(16);
    }
    if ((%item $= "lightmap")) {
        setInteriorRenderMode(5);
    }
    if ((%item $= "only textures")) {
        setInteriorRenderMode(6);
    }
    if ((%item $= "triangle strips")) {
        setInteriorRenderMode(10);
    }
    if ((%item $= "prev mode")) {
        interiorRenderModePrev();
    }
    if ((%item $= "next mode")) {
        interiorRenderModeNext();
    }
    setInteriorRenderMode(0);
};
function EditorMenuBar::onSelectTypeMenuItemSelect(%this, %itemId, %item) {
    if ((getWords(%item, 0, 1) $= "Select all")) {
        %classname = getWord(%item, 2);
        %classname = getSubStr(%classname, 0, (strlen(%classname) - 1.0));
        %classname.selectAllObjectsOfClassName(EWorldEditor);
    }
    if ((%item $= "All Types")) {
        EWorldEditor.selectType = $TypeMasks::ALLTYPES;
    }
    if ((%item $= "Triggers")) {
        EWorldEditor.selectType = $TypeMasks::TriggerObjectType;
    }
    if ((%item $= "Interiors")) {
        EWorldEditor.selectType = $TypeMasks::InteriorObjectType;
    }
    if ((%item $= "Audio Emitters")) {
        EWorldEditor.selectType = $TypeMasks::MarkerObjectType;
    }
    if ((%item $= "Shapes and Sit Markers")) {
        EWorldEditor.selectType = ($TypeMasks::ShapeBaseObjectType | $TypeMasks::StaticTSObjectType);
    }
    if ((%item $= "Items")) {
        EWorldEditor.selectType = $TypeMasks::ItemObjectType;
    }
    if ((%item $= "Antiportals")) {
        EWorldEditor.selectType = $TypeMasks::AntiPortalObjectType;
    }
    EWorldEditor.selectType = $TypeMasks::ALLTYPES;
    1.setMenuItemChecked(EditorMenuBar, "Select Type", %item);
    EStatusHud.updateStatus();
};
function EditorMenuBar::onWorldMenuItemSelect(%this, %itemId, %item) {
    if ((%item $= "Lock Selection")) {
        1.lockSelection(EWorldEditor);
    }
    if ((%item $= "Unlock Selection")) {
        0.lockSelection(EWorldEditor);
    }
    if ((%item $= "Hide Selected")) {
        1.hideSelection(EWorldEditor);
    }
    if ((%item $= "Hide All But Selected")) {
        1.hideAllButSelection(EWorldEditor);
    }
    if ((%item $= "Unhide Selected")) {
        0.hideSelection(EWorldEditor);
    }
    if ((%item $= "Invert Hidden")) {
        EWorldEditor.invertHiddenSelection();
    }
    if ((%item $= "Reset Transforms")) {
        EWorldEditor.resetTransforms();
    }
    if ((%item $= "Drop Selection")) {
        EWorldEditor.dropSelection();
    }
    if ((%item $= "SimGroup Create")) {
        ObjectBuilderGui.buildSimGroup();
    }
    if ((%item $= "Delete Selection")) {
        EWorldEditor.deleteSelection();
        inspector.uninspect();
    }
    if ((%item $= "Add Selection to Instant Group")) {
        EWorldEditor.addSelectionToAddGroup();
    }
    1.setMenuItemChecked(EditorMenuBar, "World", %item);
    if ((%item $= "Drop at Origin")) {
        EWorldEditor.dropType = "atOrigin";
    }
    if ((%item $= "Drop at Camera")) {
        EWorldEditor.dropType = "atCamera";
    }
    if ((%item $= "Drop at Camera w/Rot")) {
        EWorldEditor.dropType = "atCameraRot";
    }
    if ((%item $= "Drop below Camera")) {
        EWorldEditor.dropType = "belowCamera";
    }
    if ((%item $= "Drop at Screen Center")) {
        EWorldEditor.dropType = "screenCenter";
    }
    if ((%item $= "Drop to Ground")) {
        EWorldEditor.dropType = "toGround";
    }
    if ((%item $= "Drop at Centroid")) {
        EWorldEditor.dropType = "atCentroid";
    }
};
function EditorMenuBar::OnSnapToMenuItemSelect(%this, %itemId, %item) {
    %item.multiSnapTo(EWorldEditor);
};
function EditorMenuBar::OnCloneToMenuItemSelect(%this, %itemId, %item) {
    %item.CloneTo(EWorldEditor);
};
function EditorMenuBar::onEditMenuItemSelect(%this, %itemId, %item) {
    if ((%item $= "World Editor Settings...")) {
        0.pushDialog(Canvas, WorldEditorSettingsDlg);
    }
    if ((%item $= "Terrain Editor Settings...")) {
        99.pushDialog(Canvas, TerrainEditorValuesSettingsGui);
    }
    if ((%item $= "Relight Scene")) {
        lightScene("", forceAlways);
    }
    if ((%item $= "Increase Move Scale")) {
        EWorldEditor.increaseMoveScale();
    }
    if ((%item $= "Toggle Grid Visibility")) {
        EWorldEditor.renderPlane = !(EWorldEditor.renderPlane);
        EWorldEditor.renderPlaneHashes = !(EWorldEditor.renderPlaneHashes);
    }
    if ((%item $= "Status Hud Toggle")) {
        toggleStatusHud();
    }
    if ((%item $= "Decrease Move Scale")) {
        EWorldEditor.decreaseMoveScale();
    }
    if (EWorldEditor.isVisible()) {
        if ((%item $= "Undo")) {
            EWorldEditor.undo();
        }
        if ((%item $= "Redo")) {
            EWorldEditor.redo();
        }
        if ((%item $= "Copy")) {
            EWorldEditor.copySelection();
        }
        if ((%item $= "Cut")) {
            EWorldEditor.copySelection();
            EWorldEditor.deleteSelection();
            inspector.uninspect();
        }
        if ((%item $= "Paste")) {
            EWorldEditor.pasteSelection();
        }
        if ((%item $= "Select All")) {
            EWorldEditor.selectAllObjects();
        }
        if ((%item $= "Select None")) {
            EWorldEditor.clearSelection();
        }
        if ((%item $= "Select Inverse")) {
            EWorldEditor.invertSelection();
        }
        if ((%item $= "Find Selected")) {
            FindSelectedInEditorTree();
        }
        if ((%item $= "Zoom Camera To Selection")) {
            EWorldEditor.dropCameraToSelection();
        }
        if ((%item $= "Expand Selected Tree")) {
            ExpandSelectedInEditorTree();
        }
        if ((%item $= "Expand And Select Selected Tree")) {
            ExpandSelectedAndSelectInEditorTree();
        }
    }
    if (ETerrainEditor.isVisible()) {
        if ((%item $= "Undo")) {
            ETerrainEditor.undo();
        }
        if ((%item $= "Redo")) {
            ETerrainEditor.redo();
        }
        if ((%item $= "Select None")) {
            ETerrainEditor.clearSelection();
        }
    }
};
function EditorMenuBar::onToggleSGTools(%this, %itemId, %item) {
    %item.toggleSGTools(EditorGui);
};
function EditorMenuBar::onWindowMenuItemSelect(%this, %itemId, %item) {
    %item.setEditor(EditorGui);
};
function Creator::onWake(%this) {
    Creator.init();
};
function Creator::onSleep(%this) {
    $LastEditorChosenInstantGroup = $instantGroup;
};
function EditorGui::setWorldEditorVisible(%this) {
    1.setVisible(EWorldEditor);
    0.setVisible(ETerrainEditor);
    1.setMenuVisible(EditorMenuBar, "World");
    0.setMenuVisible(EditorMenuBar, "Action");
    0.setMenuVisible(EditorMenuBar, "Brush");
    1.makeFirstResponder(EWorldEditor);
    1.open(EditorTree, MissionGroup);
};
function EditorGui::setTerrainEditorVisible(%this) {
    0.setVisible(EWorldEditor);
    1.setVisible(ETerrainEditor);
    ETerrainEditor.attachTerrain();
    0.setVisible(EHeightField);
    0.setVisible(ETexture);
    0.setMenuVisible(EditorMenuBar, "World");
    1.setMenuVisible(EditorMenuBar, "Action");
    1.setMenuVisible(EditorMenuBar, "Brush");
    1.makeFirstResponder(ETerrainEditor);
    0.setVisible(EPainter);
};
function EditorGui::toggleSGTools(%this, %item) {
    if ((%item $= %item[$sgEditorItemNames::sgMenuItem @ 0])) {
        sgLightEditor::toggle();
    }
};
function EditorGui::setEditor(%this, %editor) {
    -(1.0).setMenuItemBitmap(EditorMenuBar, "Window", %this.currentEditor);
    0.setMenuItemBitmap(EditorMenuBar, "Window", %editor);
    %this.currentEditor = %editor;
    if ((%editor $= "World Editor")) {
        0.setVisible(EWFrame);
        0.setVisible(EWMissionArea);
        %this.setWorldEditorVisible();
    }
    if ((%editor $= "World Editor Inspector")) {
        1.setVisible(EWFrame);
        0.setVisible(EWMissionArea);
        0.setVisible(EWCreatorPane);
        1.setVisible(EWInspectorPane);
        %this.setWorldEditorVisible();
    }
    if ((%editor $= "World Editor Creator")) {
        1.setVisible(EWFrame);
        0.setVisible(EWMissionArea);
        1.setVisible(EWCreatorPane);
        0.setVisible(EWInspectorPane);
        %this.setWorldEditorVisible();
    }
    if ((%editor $= "Mission Area Editor")) {
        0.setVisible(EWFrame);
        1.setVisible(EWMissionArea);
        %this.setWorldEditorVisible();
    }
    if ((%editor $= "Terrain Editor")) {
        %this.setTerrainEditorVisible();
    }
    if ((%editor $= "Terrain Terraform Editor")) {
        %this.setTerrainEditorVisible();
        1.setVisible(EHeightField);
    }
    if ((%editor $= "Terrain Texture Editor")) {
        %this.setTerrainEditorVisible();
        1.setVisible(ETexture);
    }
    if ((%editor $= "Terrain Texture Painter")) {
        %this.setTerrainEditorVisible();
        1.setVisible(EPainter);
        EPainter.setup();
    }
};
function EditorGui::getHelpPage(%this) {
    if ((%this.currentEditor $= "World Editor") && (%this.currentEditor $= "World Editor Inspector")) {
    }
    if ((%this.currentEditor $= "World Editor Creator")) {
        return "5. World Editor";
    }
    if ((%this.currentEditor $= "Mission Area Editor")) {
        return "6. Mission Area Editor";
    }
    if ((%this.currentEditor $= "Terrain Editor")) {
        return "7. Terrain Editor";
    }
    if ((%this.currentEditor $= "Terrain Terraform Editor")) {
        return "8. Terrain Terraform Editor";
    }
    if ((%this.currentEditor $= "Terrain Texture Editor")) {
        return "9. Terrain Texture Editor";
    }
    if ((%this.currentEditor $= "Terrain Texture Painter")) {
        return "10. Terrain Texture Painter";
    }
};
function ETerrainEditor::setPaintMaterial(%this, %matIndex) {
    ETerrainEditor.paintMaterial = %matIndex @ EPainter.mat;
};
function ETerrainEditor::changeMaterial(%this, %matIndex) {
    EPainter.matIndex = %matIndex;
    getLoadFilename("*/terrains/*.png\t*/terrains/*.jpg", EPainterChangeMat);
};
function EPainterChangeMat(%file) {
    %file = filePath(%file) @ "/" @ fileBase(%file);
    %i = 0;
    while ((%i < 6.0)) {
        if ((%i @ " " @ EPainter.mat $= %file)) {
            return;
        }
        %i = (%i + 1.0);
    }
    EPainter.mat = (%i < 6.0) @ %file @ EPainter.matIndex;
    %mats = "";
    %i = 0;
    while ((%i < 6.0)) {
        %mats = %mats @ %i @ EPainter.mat @ "\n";
        %i = (%i + 1.0);
    }
    %mats.setTerrainMaterials(ETerrainEditor);
    EPainter.setup();
    "ETerrainMaterialPaint" @ EPainter.matIndex.performClick();
};
function EPainter::setup(%this) {
    "Paint Material".onActionMenuItemSelect(EditorMenuBar, 0);
    %mats = ETerrainEditor.getTerrainMaterials();
    %valid = 1;
    %i = 0;
    while ((%i < 6.0)) {
        %mat = getRecord(%mats, %i);
        %this.mat = %mat @ %i;
        fileBase(%mat).setText("ETerrainMaterialText" @ %i);
        %mat.setBitmap("ETerrainMaterialBitmap" @ %i);
        1.setActive("ETerrainMaterialChange" @ %i);
        !(%mat $= "").setActive("ETerrainMaterialPaint" @ %i);
        if ((%mat $= "")) {
            "Add...".setText("ETerrainMaterialChange" @ %i);
            if (%valid) {
                %valid = 0;
            }
            0.setActive("ETerrainMaterialChange" @ %i);
        }
        "Change...".setText("ETerrainMaterialChange" @ %i);
        %i = (%i + 1.0);
    }
    ETerrainMaterialPaint0.performClick();
};
function EditorGui::onWake(%this) {
    moveMap.push();
    EditorMap.push();
    %this.currentEditor.setEditor(%this);
};
function EditorGui::onSleep(%this) {
    EditorMap.pop();
    moveMap.pop();
};
function AreaEditor::onUpdate(%this, %area) {
    "X: " @ getWord(%area, 0) @ " Y: " @ getWord(%area, 1) @ " W: " @ getWord(%area, 2) @ " H: " @ getWord(%area, 3).setValue(AreaEditingText);
};
function AreaEditor::onWorldOffset(%this, %unused) {
};
function RecurseInvertSelectObjectsInGroup(%theSimGroup) {
    %count = %theSimGroup.getCount();
    %i = 0;
    while ((%i < %count)) {
        %object = %i.getObject(%theSimGroup);
        if (%object.isClassSimGroup()) {
            RecurseInvertSelectObjectsInGroup(%object);
        }
        %object.invertSelectObject(EWorldEditor);
        %i = (%i + 1.0);
    }
};
function RecurseSelectObjectsInGroup(%theSimGroup, %classname) {
    %count = %theSimGroup.getCount();
    %i = 0;
    while ((%i < %count)) {
        %object = %i.getObject(%theSimGroup);
        if (%object.isClassSimGroup()) {
            RecurseSelectObjectsInGroup(%object, %classname);
        }
        if ((%classname $= "")) {
        }
        if ((%object.getClassName() $= %classname)) {
            %object.selectObject(EWorldEditor);
        }
        %i = (%i + 1.0);
    }
};
function WorldEditor::selectAllObjects(%this) {
    if (isObject(MissionGroup)) {
        RecurseSelectObjectsInGroup(MissionGroup, "");
    }
};
function WorldEditor::selectAllObjectsOfClassName(%this, %classname) {
    if (isObject(MissionGroup)) {
        RecurseSelectObjectsInGroup(MissionGroup, %classname);
    }
};
function WorldEditor::invertSelection(%this) {
    if (isObject(MissionGroup)) {
        RecurseInvertSelectObjectsInGroup(MissionGroup);
    }
};
function WorldEditor::increaseMoveScale(%this) {
    %max = 10;
    EWorldEditor.mouseMoveScale = (EWorldEditor.mouseMoveScale * 2.0);
    if ((EWorldEditor.mouseMoveScale > %max)) {
        EWorldEditor.mouseMoveScale = %max;
    }
    EditorGui.setPrefs();
};
function WorldEditor::decreaseMoveScale(%this) {
    %min = 0.001;
    EWorldEditor.mouseMoveScale = (EWorldEditor.mouseMoveScale / 2.0);
    if ((EWorldEditor.mouseMoveScale < %min)) {
        EWorldEditor.mouseMoveScale = %min;
    }
    EditorGui.setPrefs();
};
function WorldEditor::onDelete(%this) {
    EditorTree.deleteSelection();
    inspector.uninspect();
    EStatusHud.updateStatus();
    "".updateGeneralInfo(EWorldEditor);
};
function WorldEditor::onSelect(%this, %obj) {
    %obj.addSelection(EditorTree);
    EStatusHud.updateStatus();
    "".updateGeneralInfo(EWorldEditor);
};
function WorldEditor::onUnSelect(%this, %obj) {
    %obj.removeSelection(EditorTree);
    inspector.uninspect();
    EStatusHud.updateStatus();
    "".updateGeneralInfo(EWorldEditor);
};
function WorldEditor::onClearSelected(%this) {
    EditorTree.clearSelection();
    inspector.uninspect();
    EStatusHud.updateStatus();
    "".updateGeneralInfo(EWorldEditor);
};
function WorldEditor::onClearSelection(%this) {
    EditorTree.clearSelection();
    inspector.uninspect();
    EStatusHud.updateStatus();
    "".updateGeneralInfo(EWorldEditor);
};
function EditorTree::onDragDrop(%this) {
    EditorTree.isDirty = 1;
};
function EditorTree::onObjectDeleteCompleted(%this) {
    EditorTree.isDirty = 1;
    EWorldEditor.copySelection();
    EWorldEditor.deleteSelection();
    inspector.uninspect();
    EStatusHud.updateStatus();
    "".updateGeneralInfo(EWorldEditor);
};
function EditorTree::onClearSelected(%this) {
    WorldEditor.clearSelection();
};
function EditorTree::init(%this) {
    new GuiControl(ETContextPopupDlg) {
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
    0.setVisible(ETContextPopup);
};
function EditorTree::OnInspect(%this, %obj) {
    %obj.inspect(inspector);
    %obj.getName().setValue(InspectorNameEdit);
    %obj.updateGeneralInfo(EWorldEditor);
};
function EditorTree::onAddSelection(%this, %obj) {
    if ($AIEdit) {
        %obj.selectObject(aiEdit);
    }
    %obj.selectObject(EWorldEditor);
    %obj.isNetCacheable = %obj.getInitialNetCacheable(TEST_MISSIONGROUPINTEGRITY);
};
function EditorTree::onRemoveSelection(%this, %obj) {
    if ($AIEdit) {
        %obj.selectObject(aiEdit);
    }
    %obj.unselectObject(EWorldEditor);
};
function EditorTree::onSelect(%this, %obj) {
    EWorldEditor.clearSelection();
    if (%obj.isClassSimGroup()) {
        %obj.inspect(inspector);
        %obj.getName().setValue(InspectorNameEdit);
        $userPref::Editor::autoSelectGroupContents = $userPref::Editor::autoSelectGroupContents;
        if ($userPref::Editor::autoSelectGroupContents) {
            RecurseSelectObjectsInGroup(%obj, "");
        }
    }
    if ($AIEdit) {
        %obj.selectObject(aiEdit);
    }
    %obj.selectObject(EWorldEditor);
};
function EditorTree::onUnSelect(%this, %obj) {
    if ($AIEdit) {
        %obj.unselectObject(aiEdit);
    }
    %obj.unselectObject(EWorldEditor);
};
function ETContextPopup::onSelect(%this, %index, %unused) {
    if ((%index == 0.0)) {
        EditorTree.contextObj.delete();
    }
};
function WorldEditor::init(%this) {
    AIObjective.ignoreObjClass(%this, TerrainBlock, Sky);
    %this.numEditModes = 3;
    %this.editMode = "move" @ 0;
    %this.editMode = "rotate" @ 1;
    %this.editMode = "scale" @ 2;
    new GuiControl(WEContextPopupDlg) {
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
    0.setVisible(WEContextPopup);
};
function WorldEditor::onDblClick(%this, %obj) {
};
function WorldEditor::onClick(%this, %obj) {
    EStatusHud.updateStatus();
    "".updateGeneralInfo(EWorldEditor);
    %obj.inspect(inspector);
    %obj.getName().setValue(InspectorNameEdit);
};
function WorldEditor::onEndDrag(%this, %obj) {
    EStatusHud.updateStatus();
    %obj.inspect(inspector);
    %obj.getName().setValue(InspectorNameEdit);
};
function WorldEditor::export(%this) {
    getSaveFilename("~/editor/*.mac", %this @ ".doExport", "selection.mac");
};
function WorldEditor::doExport(%this, %file) {
    1.save(MissionGroup, "~/editor/" @ %file);
};
function WorldEditor::import(%this) {
    getLoadFilename("~/editor/*.mac", %this @ ".doImport");
};
function WorldEditor::doImport(%this, %file) {
    exec("~/editor/" @ %file);
};
function WorldEditor::onGuiUpdate(%this, %text) {
};
function WorldEditor::getSelectionLockCount(%this) {
    %ret = 0;
    %i = 0;
    while ((%i < %this.getSelectionSize())) {
        %obj = %i.getSelectedObject(%this);
        if ((%obj.locked $= "true")) {
            %ret = (%ret + 1.0);
        }
        %i = (%i + 1.0);
    }
    return %ret;
};
function WorldEditor::getSelectionHiddenCount(%this) {
    %ret = 0;
    %i = (%this.getSelectionSize() - 1.0);
    while ((%i >= 0.0)) {
        %obj = %i.getSelectedObject(%this);
        if (%obj.noShow) {
            %ret = (%ret + 1.0);
        }
        %i = (%i - 1.0);
    }
    return %ret;
};
function WorldEditor::snapTo(%this, %snapType, %objTarget, %objToSnap) {
    if ((%objTarget $= "")) {
        %objTarget = 0.getSelectedObject(%this);
    }
    if ((%objToSnap $= "")) {
        %objToSnap = (%this.getSelectionSize() - 1.0).getSelectedObject(%this);
    }
    if ((%objTarget $= "")) {
    }
    if ((%objToSnap $= "")) {
        error("Please select two objects before selecting a Snap To funciton.");
        return;
    }
    if ((%snapType $= "X")) {
        %objToSnap.snapToX(%this, %objTarget);
    }
    if ((%snapType $= "X-")) {
        %objToSnap.snapToXNeg(%this, %objTarget);
    }
    if ((%snapType $= "X+")) {
        %objToSnap.snapToXPos(%this, %objTarget);
    }
    if ((%snapType $= "Y")) {
        %objToSnap.snapToY(%this, %objTarget);
    }
    if ((%snapType $= "Y-")) {
        %objToSnap.snapToYNeg(%this, %objTarget);
    }
    if ((%snapType $= "Y+")) {
        %objToSnap.snapToYPos(%this, %objTarget);
    }
    if ((%snapType $= "Z")) {
        %objToSnap.snapToZ(%this, %objTarget);
    }
    if ((%snapType $= "Z-")) {
        %objToSnap.snapToZNeg(%this, %objTarget);
    }
    if ((%snapType $= "Z+")) {
        %objToSnap.snapToZPos(%this, %objTarget);
    }
    if ((%snapType $= "X+YZ")) {
        %objToSnap.snapToXPosYZ(%this, %objTarget);
    }
    if ((%snapType $= "X-YZ")) {
        %objToSnap.snapToXNegYZ(%this, %objTarget);
    }
    if ((%snapType $= "XY+Z")) {
        %objToSnap.snapToXYPosZ(%this, %objTarget);
    }
    if ((%snapType $= "XY-Z")) {
        %objToSnap.snapToXYNegZ(%this, %objTarget);
    }
    if ((%snapType $= "XYZ+")) {
        %objToSnap.snapToXYZPos(%this, %objTarget);
    }
    if ((%snapType $= "XYZ-")) {
        %objToSnap.snapToXYZNeg(%this, %objTarget);
    }
    if ((%snapType $= "ObjX+YZ")) {
        %objToSnap.snapToObjXPosYZ(%this, %objTarget);
    }
    if ((%snapType $= "ObjX-YZ")) {
        %objToSnap.snapToObjXNegYZ(%this, %objTarget);
    }
    if ((%snapType $= "ObjXY+Z")) {
        %objToSnap.snapToObjXYPosZ(%this, %objTarget);
    }
    if ((%snapType $= "ObjXY-Z")) {
        %objToSnap.snapToObjXYNegZ(%this, %objTarget);
    }
    if ((%snapType $= "ObjXYZ+")) {
        %objToSnap.snapToObjXYZPos(%this, %objTarget);
    }
    if ((%snapType $= "ObjXYZ-")) {
        %objToSnap.snapToObjXYZNeg(%this, %objTarget);
    }
};
function WorldEditor::snapToX(%this, %objTarget, %objToSnap) {
    setWord(%objToSnap.getTransform(), 0, (getWord(%objTarget.getTransform(), 0) + EWorldEditor.snapGapX)).setTransform(%objToSnap);
};
function WorldEditor::snapToXPos(%this, %objTarget, %objToSnap) {
    %edgeOffset = mAbs((getWord(%objToSnap.getWorldBox(), 0) - getWord(%objToSnap.getTransform(), 0)));
    %transformWithOffset = (getWord(%objTarget.getWorldBox(), 3) + %edgeOffset);
    setWord(%objToSnap.getTransform(), 0, %transformWithOffset).setTransform(%objToSnap);
};
function WorldEditor::snapToXNeg(%this, %objTarget, %objToSnap) {
    %edgeOffset = mAbs((getWord(%objToSnap.getWorldBox(), 3) - getWord(%objToSnap.getTransform(), 0)));
    %transformWithOffset = (getWord(%objTarget.getWorldBox(), 0) - %edgeOffset);
    setWord(%objToSnap.getTransform(), 0, %transformWithOffset).setTransform(%objToSnap);
};
function WorldEditor::snapToY(%this, %objTarget, %objToSnap) {
    setWord(%objToSnap.getTransform(), 1, (getWord(%objTarget.getTransform(), 1) + EWorldEditor.snapGapY)).setTransform(%objToSnap);
};
function WorldEditor::snapToYPos(%this, %objTarget, %objToSnap) {
    %edgeOffset = mAbs((getWord(%objToSnap.getWorldBox(), 1) - getWord(%objToSnap.getTransform(), 1)));
    %transformWithOffset = (getWord(%objTarget.getWorldBox(), 4) + %edgeOffset);
    setWord(%objToSnap.getTransform(), 1, %transformWithOffset).setTransform(%objToSnap);
};
function WorldEditor::snapToYNeg(%this, %objTarget, %objToSnap) {
    %edgeOffset = mAbs((getWord(%objToSnap.getWorldBox(), 4) - getWord(%objToSnap.getTransform(), 1)));
    %transformWithOffset = (getWord(%objTarget.getWorldBox(), 1) - %edgeOffset);
    setWord(%objToSnap.getTransform(), 1, %transformWithOffset).setTransform(%objToSnap);
};
function WorldEditor::snapToZ(%this, %objTarget, %objToSnap) {
    setWord(%objToSnap.getTransform(), 2, (getWord(%objTarget.getTransform(), 2) + EWorldEditor.snapGapZ)).setTransform(%objToSnap);
};
function WorldEditor::snapToZPos(%this, %objTarget, %objToSnap) {
    %edgeOffset = mAbs((getWord(%objToSnap.getWorldBox(), 2) - getWord(%objToSnap.getTransform(), 2)));
    %transformWithOffset = (getWord(%objTarget.getWorldBox(), 5) + %edgeOffset);
    setWord(%objToSnap.getTransform(), 2, %transformWithOffset).setTransform(%objToSnap);
};
function WorldEditor::snapToZNeg(%this, %objTarget, %objToSnap) {
    %edgeOffset = mAbs((getWord(%objToSnap.getWorldBox(), 5) - getWord(%objToSnap.getTransform(), 2)));
    %transformWithOffset = (getWord(%objTarget.getWorldBox(), 2) - %edgeOffset);
    setWord(%objToSnap.getTransform(), 2, %transformWithOffset).setTransform(%objToSnap);
};
function WorldEditor::snapToXPosYZ(%this, %objTarget, %objToSnap) {
    %objToSnap.snapToXPos(%this, %objTarget);
    %objToSnap.snapToY(%this, %objTarget);
    %objToSnap.snapToZ(%this, %objTarget);
};
function WorldEditor::snapToXNegYZ(%this, %objTarget, %objToSnap) {
    %objToSnap.snapToXNeg(%this, %objTarget);
    %objToSnap.snapToY(%this, %objTarget);
    %objToSnap.snapToZ(%this, %objTarget);
};
function WorldEditor::snapToXYPosZ(%this, %objTarget, %objToSnap) {
    %objToSnap.snapToX(%this, %objTarget);
    %objToSnap.snapToYPos(%this, %objTarget);
    %objToSnap.snapToZ(%this, %objTarget);
};
function WorldEditor::snapToXYNegZ(%this, %objTarget, %objToSnap) {
    %objToSnap.snapToX(%this, %objTarget);
    %objToSnap.snapToYNeg(%this, %objTarget);
    %objToSnap.snapToZ(%this, %objTarget);
};
function WorldEditor::snapToXYZPos(%this, %objTarget, %objToSnap) {
    %objToSnap.snapToX(%this, %objTarget);
    %objToSnap.snapToY(%this, %objTarget);
    %objToSnap.snapToZPos(%this, %objTarget);
};
function WorldEditor::snapToXYZNeg(%this, %objTarget, %objToSnap) {
    %objToSnap.snapToX(%this, %objTarget);
    %objToSnap.snapToY(%this, %objTarget);
    %objToSnap.snapToZNeg(%this, %objTarget);
};
function WorldEditor::snapToObjXPosYZ(%this, %objTarget, %objToSnap) {
    %edgeOffset = (mAbs(getWord(%objToSnap.getObjectBox(), 3)) + mAbs(getWord(%objTarget.getObjectBox(), 0)));
    %worldTransform = %objTarget.getWorldTransform();
    %worldTransform = setWord(%worldTransform, 0, (getWord(%worldTransform, 0) + %edgeOffset));
    %offsetMatrix = MatrixMultiply(%objTarget.getTransform(), %worldTransform);
    %newTransform = %objToSnap.getTransform();
    %newTransform = setWord(%newTransform, 0, (getWord(%objTarget.getTransform(), 0) + getWord(%offsetMatrix, 0)));
    %newTransform = setWord(%newTransform, 1, (getWord(%objTarget.getTransform(), 1) + getWord(%offsetMatrix, 1)));
    %newTransform = setWord(%newTransform, 2, (getWord(%objTarget.getTransform(), 2) + getWord(%offsetMatrix, 2)));
    %newTransform.setTransform(%objToSnap);
};
function WorldEditor::snapToObjXNegYZ(%this, %objTarget, %objToSnap) {
    %edgeOffset = (mAbs(getWord(%objToSnap.getObjectBox(), 0)) + mAbs(getWord(%objTarget.getObjectBox(), 3)));
    %worldTransform = %objTarget.getWorldTransform();
    %worldTransform = setWord(%worldTransform, 0, (getWord(%worldTransform, 0) - %edgeOffset));
    %offsetMatrix = MatrixMultiply(%objTarget.getTransform(), %worldTransform);
    %newTransform = %objToSnap.getTransform();
    %newTransform = setWord(%newTransform, 0, (getWord(%objTarget.getTransform(), 0) + getWord(%offsetMatrix, 0)));
    %newTransform = setWord(%newTransform, 1, (getWord(%objTarget.getTransform(), 1) + getWord(%offsetMatrix, 1)));
    %newTransform = setWord(%newTransform, 2, (getWord(%objTarget.getTransform(), 2) + getWord(%offsetMatrix, 2)));
    %newTransform.setTransform(%objToSnap);
};
function WorldEditor::snapToObjXYPosZ(%this, %objTarget, %objToSnap) {
    %edgeOffset = (mAbs(getWord(%objToSnap.getObjectBox(), 4)) + mAbs(getWord(%objTarget.getObjectBox(), 1)));
    %worldTransform = %objTarget.getWorldTransform();
    %worldTransform = setWord(%worldTransform, 1, (getWord(%worldTransform, 1) + %edgeOffset));
    %offsetMatrix = MatrixMultiply(%objTarget.getTransform(), %worldTransform);
    %newTransform = %objToSnap.getTransform();
    %newTransform = setWord(%newTransform, 0, (getWord(%objTarget.getTransform(), 0) + getWord(%offsetMatrix, 0)));
    %newTransform = setWord(%newTransform, 1, (getWord(%objTarget.getTransform(), 1) + getWord(%offsetMatrix, 1)));
    %newTransform = setWord(%newTransform, 2, (getWord(%objTarget.getTransform(), 2) + getWord(%offsetMatrix, 2)));
    %newTransform.setTransform(%objToSnap);
};
function WorldEditor::snapToObjXYNegZ(%this, %objTarget, %objToSnap) {
    %edgeOffset = (mAbs(getWord(%objToSnap.getObjectBox(), 1)) + mAbs(getWord(%objTarget.getObjectBox(), 4)));
    %worldTransform = %objTarget.getWorldTransform();
    %worldTransform = setWord(%worldTransform, 1, (getWord(%worldTransform, 1) - %edgeOffset));
    %offsetMatrix = MatrixMultiply(%objTarget.getTransform(), %worldTransform);
    %newTransform = %objToSnap.getTransform();
    %newTransform = setWord(%newTransform, 0, (getWord(%objTarget.getTransform(), 0) + getWord(%offsetMatrix, 0)));
    %newTransform = setWord(%newTransform, 1, (getWord(%objTarget.getTransform(), 1) + getWord(%offsetMatrix, 1)));
    %newTransform = setWord(%newTransform, 2, (getWord(%objTarget.getTransform(), 2) + getWord(%offsetMatrix, 2)));
    %newTransform.setTransform(%objToSnap);
};
function WorldEditor::snapToObjXYZPos(%this, %objTarget, %objToSnap) {
    %edgeOffset = (mAbs(getWord(%objToSnap.getObjectBox(), 5)) + mAbs(getWord(%objTarget.getObjectBox(), 2)));
    %worldTransform = %objTarget.getWorldTransform();
    %worldTransform = setWord(%worldTransform, 2, (getWord(%worldTransform, 2) + %edgeOffset));
    %offsetMatrix = MatrixMultiply(%objTarget.getTransform(), %worldTransform);
    %newTransform = %objToSnap.getTransform();
    %newTransform = setWord(%newTransform, 0, (getWord(%objTarget.getTransform(), 0) + getWord(%offsetMatrix, 0)));
    %newTransform = setWord(%newTransform, 1, (getWord(%objTarget.getTransform(), 1) + getWord(%offsetMatrix, 1)));
    %newTransform = setWord(%newTransform, 2, (getWord(%objTarget.getTransform(), 2) + getWord(%offsetMatrix, 2)));
    %newTransform.setTransform(%objToSnap);
};
function WorldEditor::snapToObjXYZNeg(%this, %objTarget, %objToSnap) {
    %edgeOffset = (mAbs(getWord(%objToSnap.getObjectBox(), 2)) + mAbs(getWord(%objTarget.getObjectBox(), 5)));
    %worldTransform = %objTarget.getWorldTransform();
    %worldTransform = setWord(%worldTransform, 2, (getWord(%worldTransform, 2) - %edgeOffset));
    %offsetMatrix = MatrixMultiply(%objTarget.getTransform(), %worldTransform);
    %newTransform = %objToSnap.getTransform();
    %newTransform = setWord(%newTransform, 0, (getWord(%objTarget.getTransform(), 0) + getWord(%offsetMatrix, 0)));
    %newTransform = setWord(%newTransform, 1, (getWord(%objTarget.getTransform(), 1) + getWord(%offsetMatrix, 1)));
    %newTransform = setWord(%newTransform, 2, (getWord(%objTarget.getTransform(), 2) + getWord(%offsetMatrix, 2)));
    %newTransform.setTransform(%objToSnap);
};
function WorldEditor::CloneTo(%this, %snapType) {
    %selSize = %this.getSelectionSize();
    %i = 0;
    while ((%i < %selSize)) {
        %i[%origObjects @ %i] = %i.getSelectedObject(%this);
        %i = (%i + 1.0);
    }
    %i = 0;
    while ((%i < %selSize)) {
        %this.clearSelection();
        %objTarget = %i[%origObjects @ %i];
        %objTarget.selectObject(%this);
        %this.copySelection();
        %this.pasteSelection();
        %objToSnap = 0.getSelectedObject(%this);
        %i[%newObjects @ %i] = (%i < %selSize) @ %objToSnap;
        %objToSnap.snapTo(%this, %snapType, %objTarget);
        %i = (%i + 1.0);
    }
    %this.clearSelection();
    %i = 0;
    while ((%i < %selSize)) {
        %i[%newObjects @ %i].selectObject(%this);
        %i = (%i + 1.0);
    }
};
function WorldEditor::multiSnapTo(%this, %snapType) {
    echo("in multiSnapTo w/ type" @ " " @ %snapType);
    %selSize = %this.getSelectionSize();
    %objTarget = (%this.getSelectionSize() - 1.0).getSelectedObject(%this);
    %i = 0;
    while ((%i < (%selSize - 1.0))) {
        %objToSnap = %i.getSelectedObject(%this);
        %objToSnap.snapTo(%this, %snapType, %objTarget);
        %i = (%i + 1.0);
    }
};
function WorldEditor::dropCameraToSelection(%this) {
    if ((%this.getSelectionSize() == 0.0)) {
        return;
    }
    %pos = %this.getSelectionCentroid();
    %cam = LocalClientConnection.Camera.getTransform();
    %cam = setWord(%cam, 0, getWord(%pos, 0));
    %cam = setWord(%cam, 1, getWord(%pos, 1));
    %cam = setWord(%cam, 2, getWord(%pos, 2));
    %cam.setTransform(LocalClientConnection.Camera);
    %control = LocalClientConnection.getControlObject();
    if ((%control != LocalClientConnection.Camera)) {
        toggleCamera();
    }
};
function WorldEditor::dropCameraWithSelectionInView(%this) {
    if ((%this.getSelectionSize() == 0.0)) {
        return;
    }
    %curCam = LocalClientConnection.getControlObject();
    %camera = LocalClientConnection.Camera;
    %pos = %this.getSelectionBoxCentroid();
    %rad = %this.getSelectionBoxRadius();
    %rad = (%rad * 1.5);
    %fov = mDegToRad(getFovCur());
    %eyeDir = %curCam.getEyeVector();
    %camPosition = fitCameraConeAroundSphere(%pos, %rad, %eyeDir, %fov);
    %camTransform = %curCam.getEyeTransform();
    %camTransform = setWord(%camTransform, 0, getWord(%camPosition, 0));
    %camTransform = setWord(%camTransform, 1, getWord(%camPosition, 1));
    %camTransform = setWord(%camTransform, 2, getWord(%camPosition, 2));
    %camTransform.setTransform(%camera);
    if ((%curCam != %camera)) {
        toggleCamera();
    }
};
function WorldEditor::moveSelectionInPlace(%this) {
    %saveDropType = %this.dropType;
    %this.dropType = "atCentroid";
    %this.copySelection();
    %this.deleteSelection();
    %this.pasteSelection();
    %this.dropType = %saveDropType;
};
function WorldEditor::addSelectionToAddGroup(%this) {
    %i = 0;
    while ((%i < %this.getSelectionSize())) {
        %obj = %i.getSelectedObject(%this);
        %obj.add($instantGroup);
        %i = (%i + 1.0);
    }
};
function WorldEditor::resetTransforms(%this) {
    %this.addUndoState();
    %i = 0;
    while ((%i < %this.getSelectionSize())) {
        %obj = %i.getSelectedObject(%this);
        %transform = %obj.getTransform();
        %transform = setWord(%transform, 3, 0);
        %transform = setWord(%transform, 4, 0);
        %transform = setWord(%transform, 5, 1);
        %transform = setWord(%transform, 6, 0);
        %transform.setTransform(%obj);
        "1 1 1".setScale(%obj);
        %i = (%i + 1.0);
    }
};
function WorldEditorToolbarDlg::init(%this) {
    "EditorToolInspectorGui".isMember(WorldEditorToolFrameSet).setValue(WorldEditorInspectorCheckBox);
    "EditorToolMissionAreaGui".isMember(WorldEditorToolFrameSet).setValue(WorldEditorMissionAreaCheckBox);
    "EditorToolTreeViewGui".isMember(WorldEditorToolFrameSet).setValue(WorldEditorTreeCheckBox);
    "EditorToolCreatorGui".isMember(WorldEditorToolFrameSet).setValue(WorldEditorCreatorCheckBox);
};
$LastEditorChosenInstantGroup = 0;
function Creator::init(%this) {
    if (isObject($LastEditorChosenInstantGroup)) {
        if ($LastEditorChosenInstantGroup.isClassSimGroup()) {
            $instantGroup = $LastEditorChosenInstantGroup;
        }
        $instantGroup = "MissionGroup";
    }
    $instantGroup = "MissionGroup";
    %base = "Interiors".insertItem(%this, 0);
    %interiorId = "";
    %file = findFirstFile("*.dif");
    echo(" Creator::init  loading interiors");
    while (!(%file $= "")) {
        %split = strreplace(%file, "/", " ");
        %dirCount = (getWordCount(%split) - 1.0);
        %parentId = %base;
        %i = 0;
        while ((%i < %dirCount)) {
            %parent = getWords(%split, 0, %i);
            if (!(%parent[%interiorId @ %parent])) {
                %parent[%interiorId @ %parent] = getWord(%split, %i).insertItem(%this, %parentId);
            }
            %parentId = %parent[%interiorId @ %parent];
            %i = (%i + 1.0);
        }
        %create = "createInterior(" @ "\"" @ %file @ "\"" @ ");";
        "Interior".insertItem(%this, %parentId, fileBase(%file), %create);
        %file = findNextFile("*.dif");
    }
    echo(" Creator::init  loading shapes");
    %base = "Shapes".insertItem(%this, 0);
    %dataGroup = "DataBlockGroup";
    %i = 0;
    while ((%i < %dataGroup.getCount())) {
        %obj = %i.getObject(%dataGroup);
        echo("Obj: " @ %obj.getName() @ " - " @ %obj.category);
        if (!(!((%i < %dirCount) @ " " @ %file $= "") @ " " @ %obj.category $= "")) {
        }
        if ((%obj.category != 0.0)) {
            %id = %obj.category.findItemByName(%this);
            if ((%id == 0.0)) {
                %grp = %obj.category.insertItem(%this, %base);
                "Item".insertItem(%this, %grp, %obj.getName(), %obj.getClassName() @ "::create(" @ %obj.getName() @ ");");
            }
            "Item".insertItem(%this, %id, %obj.getName(), %obj.getClassName() @ "::create(" @ %obj.getName() @ ");");
        }
        %i = (%i + 1.0);
    }
    echo(" Creator::init  loading static shapes");
    %base = "Static Shapes".insertItem(%this, 0);
    %staticId = "";
    %file = findFirstFile("*.dts");
    while (!((%i < %dataGroup.getCount()) @ " " @ %file $= "")) {
        %split = strreplace(%file, "/", " ");
        %dirCount = (getWordCount(%split) - 1.0);
        %parentId = %base;
        %i = 0;
        while ((%i < %dirCount)) {
            %parent = getWords(%split, 0, %i);
            if (!(%parent[%staticId @ %parent])) {
                %parent[%staticId @ %parent] = getWord(%split, %i).insertItem(%this, %parentId);
            }
            %parentId = %parent[%staticId @ %parent];
            %i = (%i + 1.0);
        }
        %create = "TSStatic::create(\"" @ %file @ "\");";
        "TSStatic".insertItem(%this, %parentId, fileBase(%file), %create);
        %file = findNextFile("*.dts");
    }
    %base = "Dynamic Shapes".insertItem(%this, 0);
    %dynamicID = "";
    %file = findFirstFile("*.dts");
    while (!(!((%i < %dirCount) @ " " @ %file $= "") @ " " @ %file $= "")) {
        %split = strreplace(%file, "/", " ");
        %dirCount = (getWordCount(%split) - 1.0);
        %parentId = %base;
        %i = 0;
        while ((%i < %dirCount)) {
            %parent = getWords(%split, 0, %i);
            if (!(%parent[%dynamicID @ %parent])) {
                %parent[%dynamicID @ %parent] = getWord(%split, %i).insertItem(%this, %parentId);
            }
            %parentId = %parent[%dynamicID @ %parent];
            %i = (%i + 1.0);
        }
        %create = "TSDynamic::create(\"" @ %file @ "\");";
        "TSDynamic".insertItem(%this, %parentId, fileBase(%file), %create);
        %file = findNextFile("*.dts");
    }
    %file[%objGroup @ 0] = !((%i < %dirCount) @ " " @ %file $= "") @ "Environment";
    %file[%objGroup @ 0][%objGroup @ 1] = "Mission";
    %file[%objGroup @ 0][%objGroup @ 1][%objGroup @ 2] = "System";
    %env_item_idx = -(1.0);
    %env_item_idx[%Environment_Item @ %env_item_idx = (%env_item_idx + 1.0)] = "Sky";
    %env_item_idx[%Environment_Item @ %env_item_idx = (%env_item_idx + 1.0)] = "Sun";
    %env_item_idx[%Environment_Item @ %env_item_idx = (%env_item_idx + 1.0)] = "Lightning";
    %env_item_idx[%Environment_Item @ %env_item_idx = (%env_item_idx + 1.0)] = "Water";
    %env_item_idx[%Environment_Item @ %env_item_idx = (%env_item_idx + 1.0)] = "Terrain";
    %env_item_idx[%Environment_Item @ %env_item_idx = (%env_item_idx + 1.0)] = "AudioEmitter";
    %env_item_idx[%Environment_Item @ %env_item_idx = (%env_item_idx + 1.0)] = "Precipitation";
    %env_item_idx[%Environment_Item @ %env_item_idx = (%env_item_idx + 1.0)] = "ParticleEmitter";
    %env_item_idx[%Environment_Item @ %env_item_idx = (%env_item_idx + 1.0)] = "fxSunLight";
    %env_item_idx[%Environment_Item @ %env_item_idx = (%env_item_idx + 1.0)] = "fxShapeReplicator";
    %env_item_idx[%Environment_Item @ %env_item_idx = (%env_item_idx + 1.0)] = "fxFoliageReplicator";
    %env_item_idx[%Environment_Item @ %env_item_idx = (%env_item_idx + 1.0)] = "fxLight";
    %env_item_idx[%Environment_Item @ %env_item_idx = (%env_item_idx + 1.0)] = "TSText";
    %env_item_idx[%Environment_Item @ %env_item_idx = (%env_item_idx + 1.0)] = "sgUniversalStaticLight";
    %env_item_idx[%Environment_Item @ %env_item_idx = (%env_item_idx + 1.0)] = "sgMissionLightingFilter";
    %env_item_idx[%Environment_Item @ %env_item_idx = (%env_item_idx + 1.0)] = "sgDecalProjector";
    %env_item_idx[%Environment_Item @ %env_item_idx = (%env_item_idx + 1.0)] = "volumeLight";
    if (isFunction("Using_DF")) {
    }
    if (Using_DF()) {
        %env_item_idx[%Environment_Item @ %env_item_idx = (%env_item_idx + 1.0)] = "DFTextureAdvert";
    }
    if (Using_DShow()) {
        %env_item_idx[%Environment_Item @ %env_item_idx = (%env_item_idx + 1.0)] = "DSRenderer";
    }
    if (Using_Theora()) {
        %env_item_idx[%Environment_Item @ %env_item_idx = (%env_item_idx + 1.0)] = "TheoraRenderer";
    }
    if (Using_FFMPEG()) {
        %env_item_idx[%Environment_Item @ %env_item_idx = (%env_item_idx + 1.0)] = "FFMPEGRenderer";
    }
    %env_item_idx[%Environment_Item @ %env_item_idx = (%env_item_idx + 1.0)] = "SlaveRenderer";
    %env_item_idx[%Environment_Item @ %env_item_idx = (%env_item_idx + 1.0)][%Mission_Item @ 0] = "MissionArea";
    %env_item_idx[%Environment_Item @ %env_item_idx = (%env_item_idx + 1.0)][%Mission_Item @ 0][%Mission_Item @ 1] = "Path";
    %env_item_idx[%Environment_Item @ %env_item_idx = (%env_item_idx + 1.0)][%Mission_Item @ 0][%Mission_Item @ 1][%Mission_Item @ 2] = "PathMarker";
    %env_item_idx[%Environment_Item @ %env_item_idx = (%env_item_idx + 1.0)][%Mission_Item @ 0][%Mission_Item @ 1][%Mission_Item @ 2][%Mission_Item @ 3] = "Trigger";
    %env_item_idx[%Environment_Item @ %env_item_idx = (%env_item_idx + 1.0)][%Mission_Item @ 0][%Mission_Item @ 1][%Mission_Item @ 2][%Mission_Item @ 3][%Mission_Item @ 4] = "PhysicalZone";
    %env_item_idx[%Environment_Item @ %env_item_idx = (%env_item_idx + 1.0)][%Mission_Item @ 0][%Mission_Item @ 1][%Mission_Item @ 2][%Mission_Item @ 3][%Mission_Item @ 4][%Mission_Item @ 5] = "Camera";
    %env_item_idx[%Environment_Item @ %env_item_idx = (%env_item_idx + 1.0)][%Mission_Item @ 0][%Mission_Item @ 1][%Mission_Item @ 2][%Mission_Item @ 3][%Mission_Item @ 4][%Mission_Item @ 5][%Mission_Item @ 6] = "AntiPortal";
    %env_item_idx[%Environment_Item @ %env_item_idx = (%env_item_idx + 1.0)][%Mission_Item @ 0][%Mission_Item @ 1][%Mission_Item @ 2][%Mission_Item @ 3][%Mission_Item @ 4][%Mission_Item @ 5][%Mission_Item @ 6][%Mission_Item @ 6] = "ZoneBox";
    %env_item_idx[%Environment_Item @ %env_item_idx = (%env_item_idx + 1.0)][%Mission_Item @ 0][%Mission_Item @ 1][%Mission_Item @ 2][%Mission_Item @ 3][%Mission_Item @ 4][%Mission_Item @ 5][%Mission_Item @ 6][%Mission_Item @ 6][%System_Item @ 0] = "SimGroup";
    %env_item_idx[%Environment_Item @ %env_item_idx = (%env_item_idx + 1.0)][%Mission_Item @ 0][%Mission_Item @ 1][%Mission_Item @ 2][%Mission_Item @ 3][%Mission_Item @ 4][%Mission_Item @ 5][%Mission_Item @ 6][%Mission_Item @ 6][%System_Item @ 0][%System_Item @ 1] = "SimSpace";
    echo(" Creator::init  loading mission objects");
    %base = "Mission Objects".insertItem(%this, 0);
    %i = 0;
    while (!(%i[%objGroup @ %i] $= "")) {
        %grp = %i[%objGroup @ %i].insertItem(%this, %base);
        %groupTag = "%" @ %i[%objGroup @ %i] @ "_Item";
        %done = 0;
        %j = 0;
        while (!(%done)) {
            eval("%itemTag = " @ %groupTag @ %j @ ";");
            if ((%itemTag $= "")) {
                %done = 1;
            }
            %itemTag.insertItem(%this, %grp, %itemTag, "ObjectBuilderGui.build" @ %itemTag @ "();");
            %j = (%j + 1.0);
        }
        %i = (%i + 1.0);
    }
    echo(" Creator::init  finished");
};
function createInterior(%name) {
    %obj = new InteriorInstance("") {
        position = "0 0 0";
        rotation = "0 0 0";
        interiorFile = %name;
    };
    %obj.isNetCacheable = %obj.getInitialNetCacheable(TEST_MISSIONGROUPINTEGRITY);
    return %obj;
};
function WorldEditor::onAddSelected(%this, %obj) {
    %obj.addSelection(EditorTree);
};
function Creator::onSelect(%this) {
    Creator.clearSelection();
};
function Creator::OnInspect(%this, %obj) {
    if (!($missionRunning)) {
        return;
    }
    %objId = eval(%obj.getItemValue(%this));
    %obj.removeSelection(Creator);
    EditorTree.clearSelection();
    EWorldEditor.clearSelection();
    %objId.selectObject(EWorldEditor);
    EWorldEditor.dropSelection();
};
function ExpandSelectedInEditorTree() {
    %id = EditorTree.getSelectedItem();
    if ((%id != -(1.0))) {
        %id.expandAllChildren(EditorTree);
    }
    echo("nothing selected");
};
function SelectRecursively(%itemId) {
    %obj = %itemId.getItemValue(EditorTree);
    if (isObject(%obj)) {
        %obj.selectObject(EWorldEditor);
    }
    %child = %itemId.getChild(EditorTree);
    if (%child) {
        SelectRecursively(%child);
    }
    %sibling = %itemId.getNextSibling(EditorTree);
    if (%sibling) {
        SelectRecursively(%sibling);
    }
};
function ExpandSelectedAndSelectInEditorTree() {
    %obj = EditorTree.getSelectedObject();
    %id = EditorTree.getSelectedItem();
    if ((%id != -(1.0))) {
        %id.expandAllChildren(EditorTree);
        if (isObject(%obj) && %obj.isClassSimGroup()) {
            RecurseSelectObjectsInGroup(%obj, "");
        }
    }
    echo("nothing selected");
};
function FindSelectedInEditorTree() {
    if ((EWorldEditor.getSelectionSize() < 1.0)) {
        echo("nothing selected");
        return;
    }
    %obj = 0.getSelectedObject(EWorldEditor);
    if (isObject(%obj)) {
        1.buildVisibleTree(EditorTree);
        %item = %obj.getId().findItemByObjectId(EditorTree);
        if ((%item != -(1.0))) {
            %item.scrollVisible(EditorTree);
            1.makeFirstResponder(EditorTree);
        }
        echo("unable to find item in EditorTree");
    }
    echo("nothing selected");
};
function Creator::Create(%this, %sel) {
    %obj = eval(%sel.getItemValue(%this));
    if ((%obj == -(1.0))) {
        return;
    }
    %obj.isNetCacheable = %obj.getInitialNetCacheable(TEST_MISSIONGROUPINTEGRITY);
    %obj.add($instantGroup);
    EWorldEditor.clearSelection();
    %obj.selectObject(EWorldEditor);
    EWorldEditor.dropSelection();
};
function TSStatic::Create(%shapeName) {
    if ((MissionInfo.mode $= "InventoryDesigner")) {
    }
    if ((MissionInfo.mode $= "PrivateSpaceDesign")) {
        MessageBoxOK("Warning", "You should use TSDynamic for inventory items and in private spaces instead of TSStatic," @ "\n" @ "I'll still make it for you, but you should change it to the TSDynamic!" @ "\n" @ "look under \"Dynamic Shapes\" for the same thing there. thanks!", "");
    }
    %obj = new TSStatic("") {
        shapeName = %shapeName;
    };
    return %obj;
};
function TSStatic::Damage(%this) {
};
function TSDynamic::Create(%shapeName) {
    %obj = new TSDynamic("") {
        shapeName = %shapeName;
    };
    return %obj;
};
function TSDynamic::Damage(%this) {
};
function TerraformerGui::init(%this) {
    TerraformerHeightfieldGui.init();
    TerraformerTextureGui.init();
};
function TerraformerGui::onWake(%this) {
    TerraformerTextureGui.update();
};
function TerraformerGui::onSleep(%this) {
    %this.setPrefs();
};
$nextTextureId = 1;
$nextTextureRegister = 1000;
$selectedMaterial = -(1.0);
$selectedTextureOperation = -(1.0);
$TerraformerTextureDir = "common/editor/textureScripts";
function TextureInit() {
    Texture_operation_menu.clear();
    "Placement Operations".setText(Texture_operation_menu);
    1.add(Texture_operation_menu, "Place by Fractal");
    2.add(Texture_operation_menu, "Place by Height");
    3.add(Texture_operation_menu, "Place by Slope");
    4.add(Texture_operation_menu, "Place by Water Level");
    $HeightfieldSrcRegister = (Heightfield_operation.rowCount() - 1.0);
    HeightfieldPreview.getValue().setValue(TexturePreview);
    %script = Terrain.getTextureScript();
    if (!(%script $= "")) {
        texture::loadFromScript(%script);
    }
    if ((Texture_material.rowCount() == 0.0)) {
        Texture_operation.clear();
        $nextTextureRegister = 1000;
    }
    %rowCount = Texture_material.rowCount();
    %row = 0;
    while ((%row < %rowCount)) {
        %data = %row.getRowText(Texture_material);
        %entry = getRecord(%data, 0);
        %reg = getField(%entry, 1);
        %reg[$dirtyTexture @ %reg] = 1;
        %opCount = getRecordCount(%data);
        %op = 2;
        while ((%op < %opCount)) {
            %entry = getRecord(%data, %op);
            %label = getField(%entry, 0);
            if (!(%label $= "Place by Fractal")) {
            }
            if (!(%label $= "Fractal Distortion")) {
                %reg = getField(%entry, 2);
                %reg[$dirtyTexture @ %reg] = 1;
            }
            %op = (%op + 1.0);
        }
        %row = (%row + 1.0);
    }
    texture::previewMaterial();
};
function TerraformerTextureGui::refresh(%this) {
};
function Texture_material_menu::onSelect(%this, %id, %text) {
    "Materials".setText(%this);
    texture::saveMaterial();
    texture::hideTab();
    %id = texture::addMaterial(%text @ "\t" @ $nextTextureRegister = ($nextTextureRegister + 1.0));
    if ((%id != -(1.0))) {
        %id.setSelectedById(Texture_material);
        texture::addOperation("Fractal Distortion\ttab_DistortMask\t" @ $nextTextureRegister = ($nextTextureRegister + 1.0) @ "\t0\tdmask_interval\t20\tdmask_rough\t0\tdmask_seed\t" @ Terraformer.generateSeed() @ "\tdmask_filter\t0.00000 0.00000 0.13750 0.487500 0.86250 1.00000 1.00000");
    }
};
function texture::addMaterialTexture() {
    %root = filePath(Terrain.terrainFile);
    getLoadFilename("*/terrains/*.png\t*/terrains/*.jpg", addLoadedMaterial);
};
function addLoadedMaterial(%file) {
    texture::saveMaterial();
    texture::hideTab();
    %text = filePath(%file) @ "/" @ fileBase(%file);
    %id = texture::addMaterial(%text @ "\t" @ $nextTextureRegister = ($nextTextureRegister + 1.0));
    if ((%id != -(1.0))) {
        %id.setSelectedById(Texture_material);
        texture::addOperation("Fractal Distortion\ttab_DistortMask\t" @ $nextTextureRegister = ($nextTextureRegister + 1.0) @ "\t0\tdmask_interval\t20\tdmask_rough\t0\tdmask_seed\t" @ Terraformer.generateSeed() @ "\tdmask_filter\t0.00000 0.00000 0.13750 0.487500 0.86250 1.00000 1.00000");
    }
    texture::save();
};
function Texture_material::onSelect(%this, %id, %text) {
    texture::saveMaterial();
    if ((%id != $selectedMaterial)) {
        $selectedTextureOperation = -(1.0);
        Texture_operation.clear();
        texture::hideTab();
        texture::restoreMaterial(%id);
    }
    %matName = getField(%text, 0);
    ETerrainEditor.paintMaterial = %matName;
    texture::previewMaterial(%id);
    $selectedMaterial = %id;
    $selectedTextureOperation = -(1.0);
    Texture_operation.clearSelection();
};
function Texture_operation_menu::onSelect(%this, %id, %text) {
    "Placement Operations".setText(%this);
    %id = -(1.0);
    if (($selectedMaterial == -(1.0))) {
        return;
    }
    %dreg = getField(0.getRowText(Texture_operation), 2);
    if ((%text $= "Place by Fractal")) {
        %id = texture::addOperation("Place by Fractal\ttab_FractalMask\t" @ $nextTextureRegister = ($nextTextureRegister + 1.0) @ "\t" @ %dreg @ "\tfbmmask_interval\t16\tfbmmask_rough\t0.000\tfbmmask_seed\t" @ Terraformer.generateSeed() @ "\tfbmmask_filter\t0.000000 0.166667 0.333333 0.500000 0.666667 0.833333 1.000000\tfBmDistort\ttrue");
    }
    if ((%text $= "Place by Height")) {
        %id = texture::addOperation("Place by Height\ttab_HeightMask\t" @ $nextTextureRegister = ($nextTextureRegister + 1.0) @ "\t" @ %dreg @ "\ttextureHeightFilter\t0 0.2 0.4 0.6 0.8 1.0\theightDistort\ttrue");
    }
    if ((%text $= "Place by Slope")) {
        %id = texture::addOperation("Place by Slope\ttab_SlopeMask\t" @ $nextTextureRegister = ($nextTextureRegister + 1.0) @ "\t" @ %dreg @ "\ttextureSlopeFilter\t0 0.2 0.4 0.6 0.8 1.0\tslopeDistort\ttrue");
    }
    if ((%text $= "Place by Water Level")) {
        %id = texture::addOperation("Place by Water Level\ttab_WaterMask\t" @ $nextTextureRegister = ($nextTextureRegister + 1.0) @ "\t" @ %dreg @ "\twaterDistort\ttrue");
    }
    texture::hideTab();
    if ((%id != -(1.0))) {
        %id.setSelectedById(Texture_operation);
    }
};
function Texture_operation::onSelect(%this, %id, %text) {
    texture::saveOperation();
    if (!(%id $= $selectedTextureOperation)) {
        texture::hideTab();
        texture::restoreOperation(%id);
        texture::showTab(%id);
    }
    texture::previewOperation(%id);
    $selectedTextureOperation = %id;
};
function texture::deleteMaterial(%id) {
    if ((%id $= "")) {
        %id = $selectedMaterial;
    }
    if ((%id == -(1.0))) {
        return;
    }
    %row = %id.getRowNumById(Texture_material);
    %row.removeRow(Texture_material);
    %rowCount = (Texture_material.rowCount() - 1.0);
    if ((%row > %rowCount)) {
        %row = %rowCount;
    }
    if ((%id == $selectedMaterial)) {
        $selectedMaterial = -(1.0);
    }
    Texture_operation.clear();
    %id = %row.getRowId(Texture_material);
    %id.setSelectedById(Texture_material);
    texture::save();
};
function texture::deleteOperation(%id) {
    if ((%id $= "")) {
        %id = $selectedTextureOperation;
    }
    if ((%id == -(1.0))) {
        return;
    }
    %row = %id.getRowNumById(Texture_operation);
    if ((%row == 0.0)) {
        return;
    }
    %row.removeRow(Texture_operation);
    %rowCount = (Texture_operation.rowCount() - 1.0);
    if ((%row > %rowCount)) {
        %row = %rowCount;
    }
    if ((%id == $selectedTextureOperation)) {
        $selectedTextureOperation = -(1.0);
    }
    %id = %row.getRowId(Texture_operation);
    %id.setSelectedById(Texture_operation);
    texture::save();
};
function texture::applyMaterials() {
    texture::saveMaterial();
    %count = Texture_material.rowCount();
    if ((%count > 0.0)) {
        %data = getRecord(0.getRowText(Texture_material), 0);
        %mat_list = getField(%data, 0);
        %reg_list = getField(%data, 1);
        texture::evalMaterial(0.getRowId(Texture_material));
        %i = 1;
        while ((%i < %count)) {
            texture::evalMaterial(%i.getRowId(Texture_material));
            %data = getRecord(%i.getRowText(Texture_material), 0);
            %mat_list = %mat_list @ " " @ getField(%data, 0);
            %reg_list = %reg_list @ " " @ getField(%data, 1);
            %i = (%i + 1.0);
        }
        %mat_list.setMaterials(Terraformer, %reg_list);
    }
};
function texture::previewMaterial(%id) {
    if ((%id $= "")) {
        %id = $selectedMaterial;
    }
    if ((%id == -(1.0))) {
        return;
    }
    %data = %id.getRowTextById(Texture_material);
    %row = %id.getRowNumById(Texture_material);
    %reg = getField(getRecord(%data, 0), 1);
    texture::evalMaterial(%id);
    %reg.preview(Terraformer, TexturePreview);
};
function texture::evalMaterial(%id) {
    if ((%id $= "")) {
        %id = $selectedMaterial;
    }
    if ((%id == -(1.0))) {
        return;
    }
    %data = %id.getRowTextById(Texture_material);
    %reg = getField(getRecord(%data, 0), 1);
    %opCount = getRecordCount(%data);
    if ((%opCount >= 2.0)) {
        %entry = getRecord(%data, 1);
        texture::evalOperationData(%entry, 1);
        %op = 2;
        while ((%op < %opCount)) {
            %entry = getRecord(%data, %op);
            %reg_list = %reg_list @ getField(%entry, 2) @ " ";
            texture::evalOperationData(%entry, %op);
            %op = (%op + 1.0);
        }
        %reg.mergeMasks(Terraformer, %reg_list);
    }
    texture::save();
};
function texture::evalOperation(%id) {
    if ((%id $= "")) {
        %id = $selectedTextureOperation;
    }
    if ((%id == -(1.0))) {
        return;
    }
    %data = %id.getRowTextById(Texture_operation);
    %row = %id.getRowNumById(Texture_operation);
    if ((%row != 0.0)) {
        texture::evalOperation(0.getRowId(Texture_operation));
    }
    texture::evalOperationData(%data, %row);
    texture::save();
};
function texture::evalOperationData(%data, %row) {
    %label = getField(%data, 0);
    %reg = getField(%data, 2);
    %dreg = getField(%data, 3);
    %id = %row.getRowId(Texture_material);
    if ((%reg[$dirtyTexture @ %reg] == 0.0)) {
        return;
    }
    if ((%label $= "Fractal Distortion")) {
        0.maskFBm(Terraformer, %reg, getField(%data, 5), getField(%data, 7), getField(%data, 9), getField(%data, 11), 0);
    }
    if ((%label $= "Place by Fractal")) {
        %dreg.maskFBm(Terraformer, %reg, getField(%data, 5), getField(%data, 7), getField(%data, 9), getField(%data, 11), getField(%data, 13));
    }
    if ((%label $= "Place by Height")) {
        %dreg.maskHeight(Terraformer, $HeightfieldSrcRegister, %reg, getField(%data, 5), getField(%data, 7));
    }
    if ((%label $= "Place by Slope")) {
        %dreg.maskSlope(Terraformer, $HeightfieldSrcRegister, %reg, getField(%data, 5), getField(%data, 7));
    }
    if ((%label $= "Place by Water Level")) {
        %dreg.maskWater(Terraformer, $HeightfieldSrcRegister, %reg, getField(%data, 5));
    }
    %reg[$dirtyTexture @ %reg] = 0;
};
function texture::previewOperation(%id) {
    if ((%id $= "")) {
        %id = $selectedTextureOperation;
    }
    if ((%id == -(1.0))) {
        return;
    }
    %row = %id.getRowNumById(Texture_operation);
    %data = %row.getRowText(Texture_operation);
    %reg = getField(%data, 2);
    texture::evalOperation(%id);
    %reg.preview(Terraformer, TexturePreview);
};
function texture::restoreMaterial(%id) {
    if ((%id == -(1.0))) {
        return;
    }
    %data = %id.getRowTextById(Texture_material);
    Texture_operation.clear();
    %recordCount = getRecordCount(%data);
    %record = 1;
    while ((%record < %recordCount)) {
        %entry = getRecord(%data, %record);
        %entry.addRow(Texture_operation, $nextTextureId = ($nextTextureId + 1.0));
        %record = (%record + 1.0);
    }
};
function texture::saveMaterial() {
    %id = $selectedMaterial;
    if ((%id == -(1.0))) {
        return;
    }
    texture::saveOperation();
    %data = %id.getRowTextById(Texture_material);
    %newData = getRecord(%data, 0);
    %rowCount = Texture_operation.rowCount();
    %row = 0;
    while ((%row < %rowCount)) {
        %newData = %newData @ "\n" @ %row.getRowText(Texture_operation);
        %row = (%row + 1.0);
    }
    %newData.setRowById(Texture_material, %id);
    texture::save();
};
function texture::restoreOperation(%id) {
    if ((%id == -(1.0))) {
        return;
    }
    %data = %id.getRowTextById(Texture_operation);
    %fieldCount = getFieldCount(%data);
    %field = 4;
    while ((%field < %fieldCount)) {
        %obj = getField(%data, %field);
        getField(%data, (%field + 1.0)).setValue(%obj);
        %field = (%field + 2.0);
    }
    texture::save();
};
function texture::saveOperation() {
    %id = $selectedTextureOperation;
    if ((%id == -(1.0))) {
        return;
    }
    %data = %id.getRowTextById(Texture_operation);
    %newData = getField(%data, 0) @ "\t" @ getField(%data, 1) @ "\t" @ getField(%data, 2) @ "\t" @ getField(%data, 3);
    %fieldCount = getFieldCount(%data);
    %field = 4;
    while ((%field < %fieldCount)) {
        %obj = getField(%data, %field);
        %newData = %newData @ "\t" @ %obj @ "\t" @ %obj.getValue();
        %field = (%field + 2.0);
    }
    %dirty = !((%field < %fieldCount) @ " " @ %data $= %newData);
    %reg = getField(%data, 2);
    %reg[$dirtyTexture @ %reg] = %dirty;
    %newData.setRowById(Texture_operation, %id);
    if ((%dirty == 1.0)) {
        %data = $selectedMaterial.getRowTextById(Texture_material);
        %reg = getField(getRecord(%data, 0), 1);
        %reg[$dirtyTexture @ %reg] = 1;
    }
    %row = %id.getRowNumById(Texture_material);
    if ((%row == 0.0)) {
        %rowCount = Texture_operation.rowCount();
        %r = 1;
        while ((%r < %rowCount)) {
            %data = %r.getRowText(Texture_operation);
            %r = (%r + 1.0);
        }
    }
    texture::save();
};
function texture::addMaterial(%entry) {
    %id = $nextTextureId = ($nextTextureId + 1.0);
    %entry.addRow(Texture_material, %id);
    %reg = getField(%entry, 1);
    %reg[$dirtyTexture @ %reg] = 1;
    texture::save();
    return %id;
};
function texture::addOperation(%entry) {
    %id = $nextTextureId = ($nextTextureId + 1.0);
    %entry.addRow(Texture_operation, %id);
    %reg = getField(%entry, 2);
    %reg[$dirtyTexture @ %reg] = 1;
    texture::save();
    return %id;
};
function texture::save() {
    %script = "";
    %rowCount = Texture_material.rowCount();
    %row = 0;
    while ((%row < %rowCount)) {
        if ((%row != 0.0)) {
            %script = %script @ "\n";
        }
        %data = expandEscape(%row.getRowText(Texture_material));
        %script = %script @ %data;
        %row = (%row + 1.0);
    }
    %script.setTextureScript(Terrain);
    ETerrainEditor.isDirty = (%row < %rowCount) @ 1;
};
function texture::import() {
    getLoadFilename("*.ter", "Texture::doLoadTexture");
};
function texture::loadFromScript(%script) {
    Texture_material.clear();
    Texture_operation.clear();
    $selectedMaterial = -(1.0);
    $selectedTextureOperation = -(1.0);
    %i = 0;
    %rec = getRecord(%script, %i);
    while (!(%rec $= "")) {
        texture::addMaterial(collapseEscape(%rec));
        %rec = getRecord(%script, %i = (%i + 1.0));
    }
    $nextTextureRegister = 1000;
    %rowCount = Texture_material.rowCount();
    %row = 0;
    while ((%row < %rowCount)) {
        $nextTextureRegister[$dirtyTexture @ $nextTextureRegister] = 1;
        %data = %row.getRowText(Texture_material);
        %rec = getRecord(%data, 0);
        %rec = setField(%rec, 1, $nextTextureRegister);
        %data = setRecord(%data, 0, %rec);
        $nextTextureRegister = ($nextTextureRegister + 1.0);
        %opCount = getRecordCount(%data);
        %op = 1;
        while ((%op < %opCount)) {
            if ((%op == 1.0)) {
                %frac_reg = $nextTextureRegister;
            }
            $nextTextureRegister[$dirtyTexture @ $nextTextureRegister] = 1;
            %rec = getRecord(%data, %op);
            %rec = setField(%rec, 2, $nextTextureRegister);
            %rec = setField(%rec, 3, %frac_reg);
            %data = setRecord(%data, %op, %rec);
            $nextTextureRegister = ($nextTextureRegister + 1.0);
            %op = (%op + 1.0);
        }
        %id = %row.getRowId(Texture_material);
        %data.setRowById(Texture_material, %id);
        %row = (%row + 1.0);
    }
    $selectedMaterial = -(1.0);
    0.getRowId(Texture_material).setSelectedById(Texture_material);
};
function texture::doLoadTexture(%name) {
    %newTerr = new TerrainBlock("") {
        position = "0 0 0";
        terrainFile = %name;
        squareSize = 8;
        visibleDistance = 100;
    };
    if (isObject(%newTerr)) {
        %script = %newTerr.getTextureScript();
        if (!(%script $= "")) {
            texture::loadFromScript(%script);
        }
        %newTerr.delete();
    }
};
function texture::hideTab() {
    0.setVisible(tab_DistortMask);
    0.setVisible(tab_FractalMask);
    0.setVisible(tab_HeightMask);
    0.setVisible(tab_SlopeMask);
    0.setVisible(tab_WaterMask);
};
function texture::showTab(%id) {
    texture::hideTab();
    %data = %id.getRowTextById(Texture_operation);
    %tab = getField(%data, 1);
    1.setVisible(%tab);
};
$TerraformerHeightfieldDir = "common/editor/heightScripts";
function tab_Blend::reset(%this) {
    blend_option.clear();
    0.add(blend_option, "Add");
    1.add(blend_option, "Subtract");
    2.add(blend_option, "Max");
    3.add(blend_option, "Min");
    4.add(blend_option, "Multiply");
};
function tab_fBm::reset(%this) {
    fbm_detail.clear();
    0.add(fbm_detail, "Very Low");
    1.add(fbm_detail, "Low");
    2.add(fbm_detail, "Normal");
    3.add(fbm_detail, "High");
    4.add(fbm_detail, "Very High");
};
function tab_RMF::reset(%this) {
    rmf_detail.clear();
    0.add(rmf_detail, "Very Low");
    1.add(rmf_detail, "Low");
    2.add(rmf_detail, "Normal");
    3.add(rmf_detail, "High");
    4.add(rmf_detail, "Very High");
};
function tab_terrainFile::reset(%this) {
    terrainFile_textList.clear();
    %filespec = $TerraformerHeightfieldDir @ "/*.ter";
    %file = findFirstFile(%filespec);
    while (!(%file $= "")) {
        fileBase(%file) @ fileExt(%file).addRow(terrainFile_textList, %i = (%i + 1.0));
        %file = findNextFile(%filespec);
    }
};
function tab_Canyon::reset() {
};
function tab_Smooth::reset() {
};
function tab_SmoothWater::reset() {
};
function tab_SmoothRidge::reset() {
};
function tab_Filter::reset() {
};
function tab_Turbulence::reset() {
};
function tab_Thermal::reset() {
};
function tab_Hydraulic::reset() {
};
function tab_General::reset() {
};
function tab_Bitmap::reset() {
};
function tab_Sinus::reset() {
};
function Heightfield::resetTabs() {
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
};
function TerraformerInit() {
    Heightfield_options.clear();
    "Operation".setText(Heightfield_options);
    0.add(Heightfield_options, "fBm Fractal");
    1.add(Heightfield_options, "Rigid MultiFractal");
    2.add(Heightfield_options, "Canyon Fractal");
    3.add(Heightfield_options, "Sinus");
    4.add(Heightfield_options, "Bitmap");
    5.add(Heightfield_options, "Turbulence");
    6.add(Heightfield_options, "Smoothing");
    7.add(Heightfield_options, "Smooth Water");
    8.add(Heightfield_options, "Smooth Ridges/Valleys");
    9.add(Heightfield_options, "Filter");
    10.add(Heightfield_options, "Thermal Erosion");
    11.add(Heightfield_options, "Hydraulic Erosion");
    12.add(Heightfield_options, "Blend");
    13.add(Heightfield_options, "Terrain File");
    Heightfield::resetTabs();
    %script = Terrain.getHeightfieldScript();
    if (!(%script $= "")) {
        Heightfield::loadFromScript(%script, 1);
    }
    if ((Heightfield_operation.rowCount() == 0.0)) {
        Heightfield_operation.clear();
        %id1 = Heightfield::add("General\tTab_general\tgeneral_min_height\t50\tgeneral_scale\t300\tgeneral_water\t0.000\tgeneral_centerx\t0\tgeneral_centery\t0");
        %id1.setSelectedById(Heightfield_operation);
    }
    Heightfield::resetTabs();
    Heightfield::preview();
};
function Heightfield_options::onSelect(%this, %unused, %text) {
    "Operation".setText(Heightfield_options);
    %id = -(1.0);
    %rowCount = Heightfield_operation.rowCount();
    if ((%text $= "Terrain File")) {
        %id = Heightfield::add("Terrain File\ttab_terrainFile\tterrainFile_terrFileText\tterrains/terr1.ter\tterrainFile_textList\tterr1.ter");
    }
    if ((%text $= "fBm Fractal")) {
        %id = Heightfield::add("fBm Fractal\ttab_fBm\tfbm_interval\t9\tfbm_rough\t0.000\tfBm_detail\tNormal\tfBm_seed\t" @ Terraformer.generateSeed());
    }
    if ((%text $= "Rigid MultiFractal")) {
        %id = Heightfield::add("Rigid MultiFractal\ttab_RMF\trmf_interval\t4\trmf_rough\t0.000\trmf_detail\tNormal\trmf_seed\t" @ Terraformer.generateSeed());
    }
    if ((%text $= "Canyon Fractal")) {
        %id = Heightfield::add("Canyon Fractal\ttab_Canyon\tcanyon_freq\t5\tcanyon_factor\t0.500\tcanyon_seed\t" @ Terraformer.generateSeed());
    }
    if ((%text $= "Sinus")) {
        %id = Heightfield::add("Sinus\ttab_Sinus\tsinus_filter\t1 0.83333 0.6666 0.5 0.33333 0.16666 0\tsinus_seed\t" @ Terraformer.generateSeed());
    }
    if ((%text $= "Bitmap")) {
        %id = Heightfield::add("Bitmap\ttab_Bitmap\tbitmap_name\t");
        Heightfield::setBitmap();
    }
    if ((Heightfield_operation.rowCount() >= 1.0)) {
        if ((%text $= "Smoothing")) {
            %id = Heightfield::add("Smoothing\ttab_Smooth\tsmooth_factor\t0.500\tsmooth_iter\t0");
        }
        if ((%text $= "Smooth Water")) {
            %id = Heightfield::add("Smooth Water\ttab_SmoothWater\twatersmooth_factor\t0.500\twatersmooth_iter\t0");
        }
        if ((%text $= "Smooth Ridges/Valleys")) {
            %id = Heightfield::add("Smooth Ridges/Valleys\ttab_SmoothRidge\tridgesmooth_factor\t0.8500\tridgesmooth_iter\t1");
        }
        if ((%text $= "Filter")) {
            %id = Heightfield::add("Filter\ttab_Filter\tfilter\t0 0.16666667 0.3333333 0.5 0.6666667 0.8333333 1");
        }
        if ((%text $= "Turbulence")) {
            %id = Heightfield::add("Turbulence\ttab_Turbulence\tturbulence_factor\t0.250\tturbulence_radius\t10");
        }
        if ((%text $= "Thermal Erosion")) {
            %id = Heightfield::add("Thermal Erosion\ttab_Thermal\tthermal_slope\t30\tthermal_cons\t80.0\tthermal_iter\t0");
        }
        if ((%text $= "Hydraulic Erosion")) {
            %id = Heightfield::add("Hydraulic Erosion\ttab_Hydraulic\thydraulic_iter\t0\thydraulic_filter\t0 0.16666667 0.3333333 0.5 0.6666667 0.8333333 1");
        }
    }
    if ((Heightfield_operation.rowCount() >= 2.0) && ("Blend" $= %text)) {
        %id = Heightfield::add("Blend\ttab_Blend\tblend_factor\t0.500\tblend_srcB\t" @ (%rowCount - 2.0) @ "\tblend_option\tadd");
    }
    if ((%id != -(1.0))) {
        %id.setSelectedById(Heightfield_operation);
    }
};
function Heightfield::eval(%id) {
    if ((%id == -(1.0))) {
        return;
    }
    %data = restWords(%id.getRowTextById(Heightfield_operation));
    %label = getField(%data, 0);
    %row = %id.getRowNumById(Heightfield_operation);
    echo("Heightfield::eval:" @ %row @ "  " @ %label);
    if ((%label $= "General")) {
        if ((Terrain.squareSize > 0.0)) {
            %size = Terrain.squareSize;
        }
        %size = 8;
        getField(%data, 7).setTerrainInfo(Terraformer, 256, %size, getField(%data, 3), getField(%data, 5));
        getField(%data, 11).setShift(Terraformer, getField(%data, 9));
        %row.terrainData(Terraformer);
    }
    if ((%label $= "Terrain File")) {
        getField(%data, 3).terrainFile(Terraformer, %row);
    }
    if ((%label $= "fBm Fractal")) {
        getField(%data, 9).fBm(Terraformer, %row, getField(%data, 3), getField(%data, 5), getField(%data, 7));
    }
    if ((%label $= "Sinus")) {
        getField(%data, 5).sinus(Terraformer, %row, getField(%data, 3));
    }
    if ((%label $= "Rigid MultiFractal")) {
        getField(%data, 9).rigidMultiFractal(Terraformer, %row, getField(%data, 3), getField(%data, 5), getField(%data, 7));
    }
    if ((%label $= "Canyon Fractal")) {
        getField(%data, 7).canyon(Terraformer, %row, getField(%data, 3), getField(%data, 5));
    }
    if ((%label $= "Smoothing")) {
        getField(%data, 5).smooth(Terraformer, (%row - 1.0), %row, getField(%data, 3));
    }
    if ((%label $= "Smooth Water")) {
        getField(%data, 5).smoothWater(Terraformer, (%row - 1.0), %row, getField(%data, 3));
    }
    if ((%label $= "Smooth Ridges/Valleys")) {
        getField(%data, 5).smoothRidges(Terraformer, (%row - 1.0), %row, getField(%data, 3));
    }
    if ((%label $= "Filter")) {
        getField(%data, 3).filter(Terraformer, (%row - 1.0), %row);
    }
    if ((%label $= "Turbulence")) {
        getField(%data, 5).turbulence(Terraformer, (%row - 1.0), %row, getField(%data, 3));
    }
    if ((%label $= "Thermal Erosion")) {
        getField(%data, 7).erodeThermal(Terraformer, (%row - 1.0), %row, getField(%data, 3), getField(%data, 5));
    }
    if ((%label $= "Hydraulic Erosion")) {
        getField(%data, 5).erodeHydraulic(Terraformer, (%row - 1.0), %row, getField(%data, 3));
    }
    if ((%label $= "Bitmap")) {
        getField(%data, 3).loadGreyscale(Terraformer, %row);
    }
    if ((%label $= "Blend")) {
        %rowCount = Heightfield_operation.rowCount();
        if ((%rowCount > 2.0)) {
            %a = (%id.getRowNumById(Heightfield_operation) - 1.0);
            %b = getField(%data, 5);
            echo("Blend: " @ %data);
            echo("Blend: " @ getField(%data, 3) @ "  " @ getField(%data, 7));
            if ((%a < %rowCount)) {
            }
            if ((%a > 0.0)) {
            }
            if ((%b < %rowCount)) {
            }
            if ((%b > 0.0)) {
                getField(%data, 7).blend(Terraformer, %a, %b, %row, getField(%data, 3));
            }
            echo("Heightfield Editor: Blend parameters out of range.");
        }
    }
};
function Heightfield::add(%entry) {
    Heightfield::saveTab();
    Heightfield::hideTab();
    %id = $NextOperationId = ($NextOperationId + 1.0);
    if (($SelectedOperation != -(1.0))) {
        %row = ($SelectedOperation.getRowNumById(Heightfield_operation) + 1.0);
        %entry = %row @ " " @ %entry;
        %row.addRow(Heightfield_operation, %id, %entry);
        %i = (%row + 1.0);
        while ((%i < Heightfield_operation.rowCount())) {
            %id = %i.getRowId(Heightfield_operation);
            %text = %id.getRowTextById(Heightfield_operation);
            %text = setWord(%text, 0, %i);
            %text.setRowById(Heightfield_operation, %id);
            %i = (%i + 1.0);
        }
    }
    %entry = Heightfield_operation.rowCount() @ " " @ %entry;
    %entry.addRow(Heightfield_operation, %id);
    %row = %id.getRowNumById(Heightfield_operation);
    if ((%row <= $HeightfieldDirtyRow)) {
        $HeightfieldDirtyRow = %row;
    }
    Heightfield::save();
    return %id;
};
function Heightfield::onDelete(%id) {
    if ((%id $= "")) {
        %id = $SelectedOperation;
    }
    %row = %id.getRowNumById(Heightfield_operation);
    if ((%row == 0.0)) {
        return;
    }
    %row.removeRow(Heightfield_operation);
    %i = %row;
    while ((%i < Heightfield_operation.rowCount())) {
        %id2 = %i.getRowId(Heightfield_operation);
        %text = %id2.getRowTextById(Heightfield_operation);
        %text = setWord(%text, 0, %i);
        %text.setRowById(Heightfield_operation, %id2);
        %i = (%i + 1.0);
    }
    if (($HeightfieldDirtyRow >= %row)) {
        $HeightfieldDirtyRow = %row;
    }
    %rowCount = (Heightfield_operation.rowCount() - 1.0);
    if ((%row > %rowCount)) {
        %row = %rowCount;
    }
    if ((%id == $SelectedOperation)) {
        $SelectedOperation = -(1.0);
    }
    %id = %row.getRowId(Heightfield_operation);
    %id.setSelectedById(Heightfield_operation);
    Heightfield::save();
};
function Heightfield_operation::onSelect(%this, %id, %text) {
    Heightfield::saveTab();
    Heightfield::hideTab();
    $SelectedOperation = %id;
    Heightfield::restoreTab($SelectedOperation);
    Heightfield::showTab($SelectedOperation);
    Heightfield::preview($SelectedOperation);
};
function Heightfield::restoreTab(%id) {
    if ((%id == -(1.0))) {
        return;
    }
    Heightfield::hideTab();
    %data = restWords(%id.getRowTextById(Heightfield_operation));
    %fieldCount = getFieldCount(%data);
    %field = 2;
    while ((%field < %fieldCount)) {
        %obj = getField(%data, %field);
        getField(%data, (%field + 1.0)).setValue(%obj);
        %field = (%field + 2.0);
    }
    Heightfield::save();
};
function Heightfield::saveTab() {
    if (($SelectedOperation == -(1.0))) {
        return;
    }
    %data = $SelectedOperation.getRowTextById(Heightfield_operation);
    %rowNum = getWord(%data, 0);
    %data = restWords(%data);
    %newData = getField(%data, 0) @ "\t" @ getField(%data, 1);
    %fieldCount = getFieldCount(%data);
    %field = 2;
    while ((%field < %fieldCount)) {
        %obj = getField(%data, %field);
        %newData = %newData @ "\t" @ %obj @ "\t" @ %obj.getValue();
        %field = (%field + 2.0);
    }
    if (!((%field < %fieldCount) @ " " @ %data $= %newData)) {
        %row = $SelectedOperation.getRowNumById(Heightfield_operation);
        if ((%row <= $HeightfieldDirtyRow)) {
        }
        if ((%row > 0.0)) {
            $HeightfieldDirtyRow = %row;
        }
    }
    %rowNum @ " " @ %newData.setRowById(Heightfield_operation, $SelectedOperation);
    Heightfield::save();
};
function Heightfield::preview(%id) {
    %rowCount = Heightfield_operation.rowCount();
    if ((%id $= "")) {
        %id = (%rowCount - 1.0).getRowId(Heightfield_operation);
    }
    %row = %id.getRowNumById(Heightfield_operation);
    Heightfield::refresh(%row);
    %row.previewScaled(Terraformer, HeightfieldPreview);
};
function Heightfield::refresh(%last) {
    if ((%last $= "")) {
        %last = (Heightfield_operation.rowCount() - 1.0);
    }
    Heightfield::eval(0.getRowId(Heightfield_operation));
    while (($HeightfieldDirtyRow <= %last)) {
        %id = $HeightfieldDirtyRow.getRowId(Heightfield_operation);
        Heightfield::eval(%id);
        $HeightfieldDirtyRow = ($HeightfieldDirtyRow + 1.0);
    }
    Heightfield::save();
};
function Heightfield::apply(%id) {
    %rowCount = Heightfield_operation.rowCount();
    if ((%rowCount < 1.0)) {
        return;
    }
    if ((%id $= "")) {
        %id = (%rowCount - 1.0).getRowId(Heightfield_operation);
    }
    %row = %id.getRowNumById(Heightfield_operation);
    HeightfieldPreview.setRoot();
    Heightfield::refresh(%row);
    %row.setTerrain(Terraformer);
    0.setCameraPosition(Terraformer, 0, 0);
    ETerrainEditor.isDirty = 1;
};
$TerraformerSaveRegister = 0;
function Heightfield::saveBitmap(%name) {
    if ((%name $= "")) {
        getSaveFilename("*.png", "Heightfield::doSaveBitmap", $TerraformerHeightfieldDir @ "/" @ fileBase($Client::MissionFile) @ ".png");
    }
    Heightfield::doSaveBitmap(%name);
};
function Heightfield::doSaveBitmap(%name) {
    %name.saveGreyscale(Terraformer, $TerraformerSaveRegister);
};
function Heightfield::save() {
    %script = "";
    %rowCount = Heightfield_operation.rowCount();
    %row = 0;
    while ((%row < %rowCount)) {
        if ((%row != 0.0)) {
            %script = %script @ "\n";
        }
        %data = restWords(%row.getRowText(Heightfield_operation));
        %script = %script @ expandEscape(%data);
        %row = (%row + 1.0);
    }
    %script.setHeightfieldScript(Terrain);
    ETerrainEditor.isDirty = (%row < %rowCount) @ 1;
};
function Heightfield::import() {
    getLoadFilename("*.ter", "Heightfield::doLoadHeightfield");
};
function Heightfield::loadFromScript(%script, %leaveCamera) {
    echo(%script);
    Heightfield_operation.clear();
    $SelectedOperation = -(1.0);
    $HeightfieldDirtyRow = -(1.0);
    HeightfieldPreview.reset();
    %rec = getRecord(%script, %i);
    while (!(%rec $= "")) {
        Heightfield::add(collapseEscape(%rec));
        %rec = getRecord(%script, %i = (%i + 1.0));
    }
    if ((Heightfield_operation.rowCount() == 0.0)) {
        Heightfield_operation.clear();
        Heightfield::add("General\tTab_general\tgeneral_min_height\t50\tgeneral_scale\t300\tgeneral_water\t0.000\tgeneral_centerx\t0\tgeneral_centery\t0");
    }
    %data = restWords(0.getRowText(Heightfield_operation));
    %x = getField(%data, 7);
    %y = getField(%data, 9);
    %y.setOrigin(HeightfieldPreview, %x);
    0.getRowId(Heightfield_operation).setSelectedById(Heightfield_operation);
    if (!(%leaveCamera)) {
        %y.setCameraPosition(Terraformer, %x);
    }
};
function strip(%stripStr, %strToStrip) {
    %len = strlen(%stripStr);
    if ((strcmp(getSubStr(%strToStrip, 0, %len), %stripStr) == 0.0)) {
        return getSubStr(%strToStrip, %len, 100000);
    }
    return %strToStrip;
};
function Heightfield::doLoadHeightfield(%name) {
    %newTerr = new TerrainBlock("") {
        position = "0 0 -1000";
        terrainFile = strip("terrains/", %name);
        squareSize = 8;
        visibleDistance = 100;
    };
    if (isObject(%newTerr)) {
        %script = %newTerr.getHeightfieldScript();
        if (!(%script $= "")) {
            Heightfield::loadFromScript(%script);
        }
        %newTerr.delete();
    }
};
function Heightfield::setBitmap() {
    getLoadFilename($TerraformerHeightfieldDir @ "/*.png", "Heightfield::doSetBitmap");
};
function Heightfield::doSetBitmap(%name) {
    %name.setValue(bitmap_name);
    Heightfield::saveTab();
    Heightfield::preview($SelectedOperation);
};
function Heightfield::hideTab() {
    0.setVisible(tab_terrainFile);
    0.setVisible(tab_fBm);
    0.setVisible(tab_RMF);
    0.setVisible(tab_Canyon);
    0.setVisible(tab_Smooth);
    0.setVisible(tab_SmoothWater);
    0.setVisible(tab_SmoothRidge);
    0.setVisible(tab_Filter);
    0.setVisible(tab_Turbulence);
    0.setVisible(tab_Thermal);
    0.setVisible(tab_Hydraulic);
    0.setVisible(tab_General);
    0.setVisible(tab_Bitmap);
    0.setVisible(tab_Blend);
    0.setVisible(tab_Sinus);
};
function Heightfield::showTab(%id) {
    Heightfield::hideTab();
    %data = restWords(%id.getRowTextById(Heightfield_operation));
    %tab = getField(%data, 1);
    echo("Tab data: " @ %data @ " tab: " @ %tab);
    1.setVisible(%tab);
};
function Heightfield::center() {
    %camera = Terraformer.getCameraPosition();
    %x = getWord(%camera, 0);
    %y = getWord(%camera, 1);
    %y.setOrigin(HeightfieldPreview, %x);
    %origin = HeightfieldPreview.getOrigin();
    %x = getWord(%origin, 0);
    %y = getWord(%origin, 1);
    %root = HeightfieldPreview.getRoot();
    %x = (%x + getWord(%root, 0));
    %y = (%y + getWord(%root, 1));
    %x.setValue(general_centerx);
    %y.setValue(general_centery);
    Heightfield::saveTab();
};
function ExportHeightfield::onAction() {
    error("Time to export the heightfield...");
    if ((Heightfield_operation.getSelectedId() != -(1.0))) {
        $TerraformerSaveRegister = getWord(Heightfield_operation.getValue(), 0);
        Heightfield::saveBitmap("");
    }
};
function TerrainEditor::onGuiUpdate(%this, %text) {
    %mouseBrushInfo = " (Mouse Brush) #: " @ getWord(%text, 0) @ "  avg: " @ getWord(%text, 1);
    %selectionInfo = " (Selection) #: " @ getWord(%text, 2) @ "  avg: " @ getWord(%text, 3);
    %mouseBrushInfo.setValue(TEMouseBrushInfo);
    %mouseBrushInfo.setValue(TEMouseBrushInfo1);
    %selectionInfo.setValue(TESelectionInfo);
    %selectionInfo.setValue(TESelectionInfo1);
};
function TerrainEditor::offsetBrush(%this, %x, %y) {
    %curPos = %this.getBrushPos();
    (getWord(%curPos, 1) + %y).setBrushPos(%this, (getWord(%curPos, 0) + %x));
};
function TerrainEditor::swapInLoneMaterial(%this, %name) {
    if ((%this.baseMaterialsSwapped $= "true")) {
        %this.baseMaterialsSwapped = "false";
        tEditor.popBaseMaterialInfo();
    }
    %this.baseMaterialsSwapped = "true";
    %this.pushBaseMaterialInfo();
    %name.setLoneBaseMaterial(%this);
    flushTextureCache();
};
function TELoadTerrainButton::onAction(%this) {
    getLoadFilename("terrains/*.ter", %this @ ".gotFileName");
};
function TELoadTerrainButton::gotFileName(%this, %name) {
    %pos = "0 0 0";
    %squareSize = 8;
    %visibleDistance = 1200;
    if (isObject(Terrain)) {
        %pos = Terrain.position;
        %squareSize = Terrain.squareSize;
        %visibleDistance = Terrain.visibleDistance;
        Terrain.delete();
    }
    new TerrainBlock(Terrain) {
        position = %pos;
        terrainFile = %name;
        squareSize = %squareSize;
        visibleDistance = %visibleDistance;
    };
    ETerrainEditor.attachTerrain();
};
function TerrainEditorSettingsGui::onWake(%this) {
    ETerrainEditor.softSelectFilter.setValue(TESoftSelectFilter);
};
function TerrainEditorSettingsGui::onSleep(%this) {
    ETerrainEditor.softSelectFilter = TESoftSelectFilter.getValue();
};
function TESettingsApplyButton::onAction(%this) {
    ETerrainEditor.softSelectFilter = TESoftSelectFilter.getValue();
    1.resetSelWeights(ETerrainEditor);
    "softSelect".processAction(ETerrainEditor);
};
function getPrefSetting(%pref, %default) {
    if ((%pref $= "")) {
        return %default;
    }
    return %pref;
};
function onNeedRelight() {
    if ((RelightMessage.visible == 0.0)) {
        RelightMessage.visible = 1;
    }
};
function Editor::open(%this) {
    if ((Canvas.getContent() == GuiEditorGui.getId())) {
        return;
    }
    %this.prevContent = Canvas.getContent();
    EditorGui.setContent(Canvas);
};
function Editor::close(%this) {
    if ((%this.prevContent == -(1.0))) {
    }
    if ((%this.prevContent $= "")) {
        %this.prevContent = "PlayGui";
    }
    %this.prevContent.setContent(Canvas);
    MessageHud.close();
};
function EWorldEditor::updateGeneralInfo(%this, %optObj) {
    %numSelected = %this.getSelectionSize();
    %color = "<color:886644>";
    if ((%numSelected == 0.0)) {
        if ((%optObj $= "")) {
            %color @ "(nothing selected)".setText(WorldEditorGeneralInfoMLText);
            return;
        }
        %obj = %optObj;
    }
    if ((%numSelected > 1.0)) {
        %color @ "(multi)".setText(WorldEditorGeneralInfoMLText);
        return;
    }
    %obj = 0.getSelectedObject(%this);
    %serverID = %clientID = -(1.0);
    %serverValid = %clientValid = 0;
    %client = $Player::Name.get(ClientDict);
    if (!(isObject(%client))) {
        error(getScopeName() @ "-> can't get $Player::Name's client object form ClientDict!");
    }
    if (%obj.isClassNetObject()) {
        if (%obj.isServerObject()) {
            %serverID = %obj.getId();
            %serverValid = 1;
            if (!(isObject(%client))) {
                %clientID = "(no client object for player)";
            }
            %ghostID = %obj.getGhostID(%client);
            if ((%ghostID <= 0.0)) {
                %clientID = "no ghost.";
            }
            %clientID = %ghostID.resolveGhostID(ServerConnection);
            if ((%clientID <= 0.0)) {
                %clientID = "no ghost (server has ghostID, tho)";
            }
            %clientValid = 1;
        }
        if (%obj.isClientObject()) {
            error(getScopeName() @ "-> unexpected: worldeditor has selected a client-side object! handling.");
            %clientID = %obj.getId();
            %clientValid = 1;
            %ghostID = %clientID.getGhostID(ServerConnection);
            if ((%ghostID <= 0.0)) {
                %serverID = "client-side only.";
            }
            if (!(isObject(%client))) {
                %serverID = "(no client object for player)";
            }
            %serverID = %ghostID.ResolveGhost(%client).getId();
            if ((%serverID <= 0.0)) {
                %serverID = "no ghost (client has ghost ID, tho)";
            }
            %serverValid = 1;
        }
        error(getScopeName() @ "-> net object which returns false on both isServer/ClientObject(), returning!");
        %color @ "(error see log!)".setText(WorldEditorGeneralInfoMLText);
    }
    %serverID = %obj;
    %serverValid = 1;
    %clientID = "Not a net object! (assumed serverside)";
    if (%serverValid) {
    }
    %serverText = %serverID;
    if (%clientValid) {
    }
    %clientText = %clientID;
    %text = %color @ "<linkcolor:775533><linkcolorhl:ddff00>";
    if (%serverValid) {
    }
    %text = %serverText @ -(1.0) @ ">" @ %serverText @ "</a>\n";
    if (%clientValid) {
    }
    %text = %clientText @ -(1.0) @ ">" @ %clientText @ "</a>";
    %text.setText(WorldEditorGeneralInfoMLText);
};
function WorldEditorGeneralInfoMLText::onUrl(%this, %url) {
    %cmd = getWord(%url, 1);
    %restWords = getWords(%url, 2, 10000);
    if ((%cmd $= "COPYTOCLIP")) {
        setClipboard(%restWords);
    }
};
