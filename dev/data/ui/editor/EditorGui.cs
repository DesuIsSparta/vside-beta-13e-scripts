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
    EStatusHud.keepOpen(1);
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
    EStatusHud.schedule(100);
};
function EStatusHud::initialUpdateStatus(%this) {
    EStatusHud.schedule(1000);
};
function EStatusHud::realUpdateStatus(%this) {
    %c2 = "\x06";
    %selected = %c2 @ EWorldEditor.getSelectionSize() @ "\x02\x01 world objects selected";
    %seltype = "\x02\x01type: " @ %c2 @ %this.GetSelectTypeDisplayText(EWorldEditor, selectType);
    %addto = "\x02\x01addgroup: " @ %c2 @ $instantGroup @ ":" @ $instantGroup.getName();
    %viewerTrans = LocalClientConnection.getControlObject().getTransform();
    %camera = "\x02\x01cam pos: " @ %c2 @ "( " @ getWord(%viewerTrans, 0) @ " , " @ getWord(%viewerTrans, 1) @ " , " @ getWord(%viewerTrans, 2) @ " )";
    %scale = EWorldEditor @ mouseMoveScale;
    "\x02\x01move scale: " @ %c2;
    %grid = EWorldEditor @ snapToGrid;
    EWorldEditor @ gridSize @ "\x02\x01,  grid snap: " @ %c2;
    %this.charWidth = "\x02\x01grid size: " @ %c2 @ mMax(strlen(%selected), strlen(%addto));
    %this.charWidth = mMax(strlen(this, %this.charWidth), strlen(%seltype));
    %this.charWidth = mMax(strlen(this, %this.charWidth), strlen(%camera));
    %this.charWidth = mMax(strlen(this, %this.charWidth), strlen(%scale));
    %this.charWidth = mMax(strlen(this, %this.charWidth), strlen(%grid));
    %text = %selected @ "\n" @ %seltype @ "\n" @ %addto @ "\n" @ %camera @ "\n" @ %scale @ "\n" @ %grid;
    %this.setStatusText(%text);
};
function EStatusHud::setStatusText(%this, %text) {
    %this.text = %text;
    %this.charWidth = strlen(%text);
    EStatusHud.show();
    %this.hideSchedule = %this.schedule(5000, tryHide, %this);
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
    if ((480.0 <= %resWidth)) {
        %widthMultiplier = 7.2;
        %heightOffset = (8.0 + %heightOffset);
        %heightDelta = 4;
        EStatusText.setProfile("MusicMLTextProfileSmall");
    }
    if ((640.0 <= %resWidth)) {
        %widthMultiplier = 8.1;
        %heightOffset = (4.0 + %heightOffset);
        %heightDelta = 2;
        EStatusText.setProfile("MusicMLTextProfileMedium");
    }
    %widthMultiplier = 9.0;
    %heightDelta = 0;
    EStatusText.setProfile("MusicMLTextProfile");
    %content = "";
    if (!(%this.text $= "")) {
        %content = %this.text @ "\n";
    }
    EStatusText.setText(%content);
    %height = getWord(%this.extent, 1);
    %targetWidth = mCeil((mMax(%this.charWidth, 20) * %widthMultiplier));
    %this.setTrgExtent(%targetWidth, %height);
    EStatusText.resize(0, 0, %targetWidth, %height);
    %this.updatePosition();
};
function EStatusHud::updatePosition(%this) {
    %trgX = getWord(%this.getTrgPosition(), 0);
    %trgY = (12.0 + ($ButtonBarVar::VerticalAdjustment + (getWord(%this.getExtent(), 1) - getWord(ButtonBar.getTrgPosition(), 1))));
    %this.setTrgPosition(%trgX, %trgY);
};
function EStatusHud::show(%this) {
    if (%this.hideSchedule) {
        cancel(%this.hideSchedule);
    }
    %trgY = getWord(%this.getTrgPosition(), 1);
    %this.setTrgPosition(4, %trgY);
    %this.update();
};
function EStatusHud::keepOpen(%this, %flag) {
    $UserPref::WorldEditor::keepEStatusHudOpen = %flag;
};
function EStatusHud::hide(%this) {
    %this.updatePosition();
    %width = getWord(%this.getTrgExtent(), 0);
    %trgY = getWord(%this.getTrgPosition(), 1);
    %this.setTrgPosition(-(%width), %trgY);
    %this.keepOpen(0);
};
function EStatusHud::isShowing(%this) {
    return (0.0 >= getWord(%this.position, 0));
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
        %this.setTrgPosition(%posX, %posY);
    }
};
$sgEditorItemNames::sgMenu = "Synapse Gaming Tools";
$sgEditorItemNames::sgMenu[$sgEditorItemNames::sgMenuItem @ 0] = "Lighting Pack Light Editor";
function EditorGui::getPrefs() {
    %this.dropType = getPrefSetting($Pref::WorldEditor::dropType, "atCamera") @ EWorldEditor;
    %this.planarMovement = getPrefSetting($pref::WorldEditor::planarMovement, 1) @ EWorldEditor;
    %this.undoLimit = getPrefSetting($pref::WorldEditor::undoLimit, 40) @ EWorldEditor;
    %this.dropType = getPrefSetting($Pref::WorldEditor::dropType, "screenCenter") @ EWorldEditor;
    %this.projectDistance = getPrefSetting($pref::WorldEditor::projectDistance, 2000) @ EWorldEditor;
    %this.boundingBoxCollision = getPrefSetting($pref::WorldEditor::boundingBoxCollision, 1) @ EWorldEditor;
    %this.renderPlane = getPrefSetting($pref::WorldEditor::renderPlane, 1) @ EWorldEditor;
    %this.renderPlaneHashes = getPrefSetting($pref::WorldEditor::renderPlaneHashes, 1) @ EWorldEditor;
    %this.gridColor = getPrefSetting($pref::WorldEditor::gridColor, "255 255 255 20") @ EWorldEditor;
    %this.planeDim = getPrefSetting($pref::WorldEditor::planeDim, 500) @ EWorldEditor;
    %this.gridSize = getPrefSetting($pref::WorldEditor::gridSize, "10 10 10") @ EWorldEditor;
    %this.renderPopupBackground = getPrefSetting($pref::WorldEditor::renderPopupBackground, 1) @ EWorldEditor;
    %this.popupBackgroundColor = getPrefSetting($pref::WorldEditor::popupBackgroundColor, "100 100 100") @ EWorldEditor;
    %this.popupTextColor = getPrefSetting($pref::WorldEditor::popupTextColor, "255 255 0") @ EWorldEditor;
    %this.selectHandle = getPrefSetting($pref::WorldEditor::selectHandle, "gui/Editor_SelectHandle.png") @ EWorldEditor;
    %this.defaultHandle = getPrefSetting($pref::WorldEditor::defaultHandle, "gui/Editor_DefaultHandle.png") @ EWorldEditor;
    %this.lockedHandle = getPrefSetting($pref::WorldEditor::lockedHandle, "gui/Editor_LockedHandle.png") @ EWorldEditor;
    %this.objectTextColor = getPrefSetting($pref::WorldEditor::objectTextColor, "255 255 255") @ EWorldEditor;
    %this.objectsUseBoxCenter = getPrefSetting($pref::WorldEditor::objectsUseBoxCenter, 1) @ EWorldEditor;
    %this.axisGizmoMaxScreenLen = getPrefSetting($pref::WorldEditor::axisGizmoMaxScreenLen, 200) @ EWorldEditor;
    %this.axisGizmoActive = getPrefSetting($pref::WorldEditor::axisGizmoActive, 1) @ EWorldEditor;
    %this.mouseMoveScale = getPrefSetting($pref::WorldEditor::mouseMoveScale, 0.01) @ EWorldEditor;
    %this.mouseRotateScale = getPrefSetting($pref::WorldEditor::mouseRotateScale, 0.01) @ EWorldEditor;
    %this.mouseScaleScale = getPrefSetting($pref::WorldEditor::mouseScaleScale, 0.01) @ EWorldEditor;
    %this.objSelectFillAlpha = getPrefSetting($pref::WorldEditor::objSelectFillAlpha, 100) @ EWorldEditor;
    %this.minScaleFactor = getPrefSetting($pref::WorldEditor::minScaleFactor, 0.1) @ EWorldEditor;
    %this.maxScaleFactor = getPrefSetting($pref::WorldEditor::maxScaleFactor, 4000) @ EWorldEditor;
    %this.objSelectColor = getPrefSetting($pref::WorldEditor::objSelectColor, "255 0 0") @ EWorldEditor;
    %this.objMouseOverSelectColor = getPrefSetting($pref::WorldEditor::objMouseOverSelectColor, "0 0 255") @ EWorldEditor;
    %this.objMouseOverColor = getPrefSetting($pref::WorldEditor::objMouseOverColor, "0 255 0") @ EWorldEditor;
    %this.showMousePopupInfo = getPrefSetting($pref::WorldEditor::showMousePopupInfo, 1) @ EWorldEditor;
    %this.dragRectColor = getPrefSetting($pref::WorldEditor::dragRectColor, "255 255 0") @ EWorldEditor;
    %this.renderObjText = getPrefSetting($pref::WorldEditor::renderObjText, 0) @ EWorldEditor;
    %this.renderObjHandle = getPrefSetting($pref::WorldEditor::renderObjHandle, 0) @ EWorldEditor;
    %this.faceSelectColor = getPrefSetting($pref::WorldEditor::faceSelectColor, "0 0 100 100") @ EWorldEditor;
    %this.renderSelectionBox = getPrefSetting($pref::WorldEditor::renderSelectionBox, 0) @ EWorldEditor;
    %this.selectionBoxColor = getPrefSetting($pref::WorldEditor::selectionBoxColor, "255 255 0") @ EWorldEditor;
    %this.snapToGrid = getPrefSetting($pref::WorldEditor::snapToGrid, 0) @ EWorldEditor;
    %this.snapRotations = getPrefSetting($pref::WorldEditor::snapRotations, 0) @ EWorldEditor;
    %this.rotationSnap = getPrefSetting($pref::WorldEditor::rotationSnap, 15) @ EWorldEditor;
    %this.softSelecting = 1 @ ETerrainEditor;
    %this.currentAction = "raiseHeight" @ ETerrainEditor;
    %this.currentMode = "select" @ ETerrainEditor;
};
function EditorGui::setPrefs() {
    $Pref::WorldEditor::dropType = %this.dropType;
    EWorldEditor;
    $pref::WorldEditor::planarMovement = %this.planarMovement;
    EWorldEditor;
    $pref::WorldEditor::undoLimit = %this.undoLimit;
    EWorldEditor;
    $Pref::WorldEditor::dropType = %this.dropType;
    EWorldEditor;
    $pref::WorldEditor::projectDistance = %this.projectDistance;
    EWorldEditor;
    $pref::WorldEditor::boundingBoxCollision = %this.boundingBoxCollision;
    EWorldEditor;
    $pref::WorldEditor::renderPlane = %this.renderPlane;
    EWorldEditor;
    $pref::WorldEditor::renderPlaneHashes = %this.renderPlaneHashes;
    EWorldEditor;
    $pref::WorldEditor::gridColor = %this.gridColor;
    EWorldEditor;
    $pref::WorldEditor::planeDim = %this.planeDim;
    EWorldEditor;
    $pref::WorldEditor::gridSize = %this.gridSize;
    EWorldEditor;
    $pref::WorldEditor::renderPopupBackground = %this.renderPopupBackground;
    EWorldEditor;
    $pref::WorldEditor::popupBackgroundColor = %this.popupBackgroundColor;
    EWorldEditor;
    $pref::WorldEditor::popupTextColor = %this.popupTextColor;
    EWorldEditor;
    $pref::WorldEditor::selectHandle = %this.selectHandle;
    EWorldEditor;
    $pref::WorldEditor::defaultHandle = %this.defaultHandle;
    EWorldEditor;
    $pref::WorldEditor::lockedHandle = %this.lockedHandle;
    EWorldEditor;
    $pref::WorldEditor::objectTextColor = %this.objectTextColor;
    EWorldEditor;
    $pref::WorldEditor::objectsUseBoxCenter = %this.objectsUseBoxCenter;
    EWorldEditor;
    $pref::WorldEditor::axisGizmoMaxScreenLen = %this.axisGizmoMaxScreenLen;
    EWorldEditor;
    $pref::WorldEditor::axisGizmoActive = %this.axisGizmoActive;
    EWorldEditor;
    $pref::WorldEditor::mouseMoveScale = %this.mouseMoveScale;
    EWorldEditor;
    $pref::WorldEditor::mouseRotateScale = %this.mouseRotateScale;
    EWorldEditor;
    $pref::WorldEditor::mouseScaleScale = %this.mouseScaleScale;
    EWorldEditor;
    $pref::WorldEditor::objSelectFillAlpha = %this.objSelectFillAlpha;
    EWorldEditor;
    $pref::WorldEditor::minScaleFactor = %this.minScaleFactor;
    EWorldEditor;
    $pref::WorldEditor::maxScaleFactor = %this.maxScaleFactor;
    EWorldEditor;
    $pref::WorldEditor::objSelectColor = %this.objSelectColor;
    EWorldEditor;
    $pref::WorldEditor::objMouseOverSelectColor = %this.objMouseOverSelectColor;
    EWorldEditor;
    $pref::WorldEditor::objMouseOverColor = %this.objMouseOverColor;
    EWorldEditor;
    $pref::WorldEditor::showMousePopupInfo = %this.showMousePopupInfo;
    EWorldEditor;
    $pref::WorldEditor::dragRectColor = %this.dragRectColor;
    EWorldEditor;
    $pref::WorldEditor::renderObjText = %this.renderObjText;
    EWorldEditor;
    $pref::WorldEditor::renderObjHandle = %this.renderObjHandle;
    EWorldEditor;
    $pref::WorldEditor::raceSelectColor = %this.faceSelectColor;
    EWorldEditor;
    $pref::WorldEditor::renderSelectionBox = %this.renderSelectionBox;
    EWorldEditor;
    $pref::WorldEditor::selectionBoxColor = %this.selectionBoxColor;
    EWorldEditor;
    $pref::WorldEditor::snapToGrid = %this.snapToGrid;
    EWorldEditor;
    $pref::WorldEditor::snapRotations = %this.snapRotations;
    EWorldEditor;
    $pref::WorldEditor::rotationSnap = %this.rotationSnap;
    EWorldEditor;
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
    0;
    $NextOperationId = 1;
    $HeightfieldDirtyRow = -(1.0);
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
    %n = (1.0 + %n);
    EditorMenuBar.addMenuItem(%selectMenuName, "Triggers", %n, "Ctrl 2", 1);
    %n = (1.0 + %n);
    EditorMenuBar.addMenuItem(%selectMenuName, "Interiors", %n, "Ctrl 3", 1);
    %n = (1.0 + %n);
    EditorMenuBar.addMenuItem(%selectMenuName, "Shapes and Sit Markers", %n, "Ctrl 4", 1);
    %n = (1.0 + %n);
    EditorMenuBar.addMenuItem(%selectMenuName, "Antiportals", %n, "Ctrl 5", 1);
    %n = (1.0 + %n);
    EditorMenuBar.addMenuItem(%selectMenuName, "Items", %n, "ctrl 6", 1);
    %n = (1.0 + %n);
    EditorMenuBar.addMenuItem(%selectMenuName, "Audio Emitters", %n, "Ctrl 7", 1);
    %n = (1.0 + %n);
    EditorMenuBar.addMenuItem(%selectMenuName, "-", %n);
    %n = (1.0 + %n);
    EditorMenuBar.addMenuItem(%selectMenuName, "Select all AdvertShapes", %n);
    %n = (1.0 + %n);
    EditorMenuBar.addMenuItem(%selectMenuName, "Select all AIPlayers", %n);
    %n = (1.0 + %n);
    EditorMenuBar.addMenuItem(%selectMenuName, "Select all ETSSeatMarker", %n);
    %n = (1.0 + %n);
    EditorMenuBar.addMenuItem(%selectMenuName, "Select all InteriorInstances", %n);
    %n = (1.0 + %n);
    EditorMenuBar.addMenuItem(%selectMenuName, "Select all Markers", %n);
    %n = (1.0 + %n);
    EditorMenuBar.addMenuItem(%selectMenuName, "Select all MissionMarkers", %n);
    %n = (1.0 + %n);
    EditorMenuBar.addMenuItem(%selectMenuName, "Select all StaticShapes", %n);
    %n = (1.0 + %n);
    EditorMenuBar.addMenuItem(%selectMenuName, "Select all sgUniversalStaticLights", %n);
    %n = (1.0 + %n);
    EditorMenuBar.addMenuItem(%selectMenuName, "Select all Triggers", %n);
    %n = (1.0 + %n);
    EditorMenuBar.addMenuItem(%selectMenuName, "Select all TSStatics", %n);
    %n = (1.0 + %n);
    EditorMenuBar.addMenuItem(%selectMenuName, "Select all Waterblocks", %n);
    %n = (1.0 + %n);
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
    EditorMenuBar.addMenuItem($sgEditorItemNames::sgMenu, , 2, "F12");
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
    %this.isDirty = 0 @ EditorTree;
    %this.isDirty = 0 @ EWorldEditor;
    %this.isDirty = 0 @ ETerrainEditor;
    %this.isMissionDirty = 0 @ ETerrainEditor;
    %this.saveAs = 0 @ EditorGui;
};
function EditorNewMission() {
    if (%this.isMissionDirty) {
    }
    if (%this.isDirty) {
    }
    if (%this.isDirty) {
    }
    if (%this.isDirty) {
        MessageBoxYesNo("Mission Modified", "Would you like to save changes to the current mission \"" @ $Server::MissionFile @ "\" before creating a new mission?", "EditorDoNewMission(true);", "EditorDoNewMission(false);");
    }
    EditorDoNewMission(0);
};
function EditorSaveMissionMenu() {
    if (%this.saveAs) {
        EditorSaveMissionAs();
    }
    EditorSaveMission();
};
function EditorSaveMission() {
    if (%this.isDirty) {
    }
    if (%this.isDirty) {
    }
    if (%this.isMissionDirty) {
    }
    if (!(isWriteableFileName($Server::MissionFile))) {
        MessageBoxOK("Error", "Mission file \"" @ $Server::MissionFile @ "\" is read-only.", "");
        return 0;
    }
    if (%this.isDirty) {
    }
    if (!(isWriteableFileName(Terrain, %this.terrainFile))) {
        MessageBoxOK("Error", "Terrain file \"", Terrain @ %this.terrainFile @ "\" is read-only.", "");
        return 0;
    }
    %errorCount = RunTestCase("TEST_MISSIONGROUPINTEGRITY", "WARNING: About that mission file you just saved...");
    if (%this.isDirty) {
    }
    if (%this.isDirty) {
    }
    if (%this.isMissionDirty) {
        if ((MissionInfo @ " " @ %this.mode $= "PrivateSpaceDesign")) {
            if (isObject(PRIVATESPACE_GROUP)) {
                RootGroup.add(PRIVATESPACE_GROUP);
            }
            error("PrivateSpaceDesign mode, no PRIVATESPACE_GROUP object");
        }
        MissionGroup.save($Server::MissionFile);
        if ((MissionInfo @ " " @ %this.mode $= "PrivateSpaceDesign")) {
            if (isObject(PRIVATESPACE_GROUP)) {
                %spaceForGridFileName = getSubStr($Server::MissionFile, 0, (4.0 - strlen($Server::MissionFile))) @ "_generated.cs";
                ETerrainEditor;
                PRIVATESPACE_GROUP.save(%spaceForGridFileName);
                echo("PrivateSpaceDesign mode saving to space for grid, file named:" @ " " @ %spaceForGridFileName);
                MissionGroup.add(PRIVATESPACE_GROUP);
            }
        }
    }
    if ((MissionInfo @ " " @ %this.mode $= "PrivateSpaceDesign")) {
        if (isObject(PRIVATESPACE_GROUP)) {
            %spaceForGridFileName = getSubStr($Server::MissionFile, 0, (4.0 - strlen($Server::MissionFile))) @ "_generated.cs";
            EditorTree;
            PRIVATESPACE_GROUP.save(%spaceForGridFileName);
            echo("PrivateSpaceDesign mode saving to space for grid, file named:" @ " " @ %spaceForGridFileName);
        }
    }
    if (%this.isDirty) {
        Terrain.save(Terrain, %this.terrainFile);
    }
    %this.isDirty = 0 @ EditorTree;
    ETerrainEditor;
    %this.isDirty = 0 @ EWorldEditor;
    EWorldEditor;
    %this.isDirty = 0 @ ETerrainEditor;
    %this.isMissionDirty = 0 @ ETerrainEditor;
    %this.saveAs = 0 @ EditorGui;
    return 1;
};
function EditorDoSaveAs(%missionName) {
    %this.isDirty = 1 @ ETerrainEditor;
    %this.isDirty = 1 @ EWorldEditor;
    %this.isDirty = 1 @ EditorTree;
    %saveMissionFile = $Server::MissionFile;
    %saveTerrName = %this.terrainFile;
    Terrain;
    $Server::MissionFile = %missionName;
    %this.terrainFile = filePath(%missionName) @ "/" @ fileBase(%missionName) @ ".ter" @ Terrain;
    if (!(EditorSaveMission())) {
        $Server::MissionFile = %saveMissionFile;
        %this.terrainFile = %saveTerrName @ Terrain;
    }
};
function EditorSaveMissionAs() {
    getSaveFilename("*.mis", "EditorDoSaveAs", $Server::MissionFile);
};
function EditorDoLoadMission(%file) {
    Editor.close();
    loadMission(%file, 1);
    Editor::Create();
    MissionCleanup.add(Editor);
    %this.loadingMission = 1 @ EditorGui;
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
    %this.saveAs = 1 @ EditorGui;
    %this.isDirty = 1 @ EWorldEditor;
    %this.isDirty = 1 @ ETerrainEditor;
    %this.isDirty = 1 @ EditorTree;
};
function EditorOpenMission() {
    if (%this.isMissionDirty) {
    }
    if (%this.isDirty) {
    }
    if (%this.isDirty) {
    }
    if (%this.isDirty) {
        MessageBoxYesNo("Mission Modified", "Would you like to save changes to the current mission \"" @ $Server::MissionFile @ "\" before opening a new mission?", "EditorSaveBeforeLoad();", "getLoadFilename(\"*.mis\", \"EditorDoLoadMission\");");
    }
    getLoadFilename("*.mis", "EditorDoLoadMission");
};
function EditorMenuBar::onMenuSelect(%this, %unused, %menu) {
    if ((%menu $= "File")) {
        if (ETerrainEditor.isVisible()) {
        }
        %editingHeightfield = EHeightField.isVisible();
        EditorMenuBar.setMenuItemEnable("File", "Export Terraform Bitmap...", %editingHeightfield);
        if (%this.isDirty) {
        }
        if (%this.isMissionDirty) {
        }
        if (%this.isDirty) {
        }
        EditorMenuBar.setMenuItemEnable("File", "Save Mission...", ETerrainEditor, ETerrainEditor, EWorldEditor, EditorTree, %this.isDirty);
    }
    if ((%menu $= "Edit")) {
        %selSize = EWorldEditor.getSelectionSize();
        EditorMenuBar.setMenuItemEnable("Edit", "Zoom Camera To Selection", (0.0 > %selSize));
        if (EWorldEditor.isVisible()) {
            EditorMenuBar.setMenuItemEnable("Edit", "Select All", 1);
            EditorMenuBar.setMenuItemEnable("Edit", "Paste", EWorldEditor.canPasteSelection());
            %canCutCopy = (0.0 > EWorldEditor.getSelectionSize());
            EditorMenuBar.setMenuItemEnable("Edit", "Cut", %canCutCopy);
            EditorMenuBar.setMenuItemEnable("Edit", "Copy", %canCutCopy);
        }
        if (ETerrainEditor.isVisible()) {
            EditorMenuBar.setMenuItemEnable("Edit", "Cut", 0);
            EditorMenuBar.setMenuItemEnable("Edit", "Copy", 0);
            EditorMenuBar.setMenuItemEnable("Edit", "Paste", 0);
            EditorMenuBar.setMenuItemEnable("Edit", "Select All", 0);
        }
    }
    if ((%menu $= "World")) {
        %selSize = EWorldEditor.getSelectionSize();
        %lockCount = EWorldEditor.getSelectionLockCount();
        %hideCount = EWorldEditor.getSelectionHiddenCount();
        EditorMenuBar.setMenuItemEnable("World", "Lock Selection", (%selSize < %lockCount));
        EditorMenuBar.setMenuItemEnable("World", "Unlock Selection", (0.0 > %lockCount));
        EditorMenuBar.setMenuItemEnable("World", "Hide Selected", (%selSize < %hideCount));
        EditorMenuBar.setMenuItemEnable("World", "Unhide Selected", (0.0 > %hideCount));
        EditorMenuBar.setMenuItemEnable("World", "Invert Hidden", (0.0 > %selSize));
        EditorMenuBar.setMenuItemEnable("World", "Add Selection to Instant Group", (0.0 > %selSize));
        if ((0.0 > %selSize)) {
        }
        EditorMenuBar.setMenuItemEnable("World", "Reset Transforms", (0.0 == %lockCount));
        if ((0.0 > %selSize)) {
        }
        EditorMenuBar.setMenuItemEnable("World", "Drop Selection", (0.0 == %lockCount));
        if ((0.0 > %selSize)) {
        }
        EditorMenuBar.setMenuItemEnable("World", "Delete Selection", (0.0 == %lockCount));
    }
};
function EditorMenuBar::onMenuItemSelect(%this, %unused, %menu, %itemId, %item) {
    if ((%menu $= "File")) {
        %this.onFileMenuItemSelect(%itemId, %item);
    }
    if ((%menu $= "Edit")) {
        %this.onEditMenuItemSelect(%itemId, %item);
    }
    if ((%menu $= "World")) {
        %this.onWorldMenuItemSelect(%itemId, %item);
    }
    if ((%menu $= "Window")) {
        %this.onWindowMenuItemSelect(%itemId, %item);
    }
    if ((%menu $= "Select Type")) {
        %this.onSelectTypeMenuItemSelect(%itemId, %item);
    }
    if ((%menu $= "Render Mode")) {
        %this.onRenderModeMenuItemSelect(%itemId, %item);
    }
    if ((%menu $= "Action")) {
        %this.onActionMenuItemSelect(%itemId, %item);
    }
    if ((%menu $= "Brush")) {
        %this.onBrushMenuItemSelect(%itemId, %item);
    }
    if ((%menu $= "Camera")) {
        %this.onCameraMenuItemSelect(%itemId, %item);
    }
    if ((%menu $= "SnapTo")) {
        %this.OnSnapToMenuItemSelect(%itemId, %item);
    }
    if ((%menu $= "CloneTo")) {
        %this.OnCloneToMenuItemSelect(%itemId, %item);
    }
    if ((%menu $= $sgEditorItemNames::sgMenu)) {
        %this.onToggleSGTools(%itemId, %item);
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
    %this.setMenuItemChecked("Camera", %itemId, 1);
    $Camera::movementSpeed = (5.0 + (195.0 * (6.0 / (3.0 - %itemId))));
};
function EditorMenuBar::onActionMenuItemSelect(%this, %itemId, %item) {
    EditorMenuBar.setMenuItemChecked("Action", %item, 1);
    if ((%item $= "Select")) {
        %this.currentMode = "select" @ ETerrainEditor;
        %this.selectionHidden = 0 @ ETerrainEditor;
        %this.renderVertexSelection = 1 @ ETerrainEditor;
        ETerrainEditor.setAction("select");
    }
    if ((%item $= "Adjust Selection")) {
        %this.currentMode = "adjust" @ ETerrainEditor;
        %this.selectionHidden = 0 @ ETerrainEditor;
        ETerrainEditor.setAction("adjustHeight");
        %this.currentAction = brushAdjustHeight @ ETerrainEditor;
        %this.renderVertexSelection = 1 @ ETerrainEditor;
    }
    %this.currentMode = "paint" @ ETerrainEditor;
    %this.selectionHidden = 1 @ ETerrainEditor;
    ETerrainEditor.setAction(ETerrainEditor, %this.currentAction);
    if ((%item $= "Add Dirt")) {
        %this.currentAction = raiseHeight @ ETerrainEditor;
        %this.renderVertexSelection = 1 @ ETerrainEditor;
    }
    if ((%item $= "Paint Material")) {
        %this.currentAction = paintMaterial @ ETerrainEditor;
        %this.renderVertexSelection = 1 @ ETerrainEditor;
    }
    if ((%item $= "Excavate")) {
        %this.currentAction = lowerHeight @ ETerrainEditor;
        %this.renderVertexSelection = 1 @ ETerrainEditor;
    }
    if ((%item $= "Set Height")) {
        %this.currentAction = setHeight @ ETerrainEditor;
        %this.renderVertexSelection = 1 @ ETerrainEditor;
    }
    if ((%item $= "Adjust Height")) {
        %this.currentAction = brushAdjustHeight @ ETerrainEditor;
        %this.renderVertexSelection = 1 @ ETerrainEditor;
    }
    if ((%item $= "Flatten")) {
        %this.currentAction = flattenHeight @ ETerrainEditor;
        %this.renderVertexSelection = 1 @ ETerrainEditor;
    }
    if ((%item $= "Smooth")) {
        %this.currentAction = smoothHeight @ ETerrainEditor;
        %this.renderVertexSelection = 1 @ ETerrainEditor;
    }
    if ((%item $= "Set Empty")) {
        %this.currentAction = setEmpty @ ETerrainEditor;
        %this.renderVertexSelection = 0 @ ETerrainEditor;
    }
    if ((%item $= "Clear Empty")) {
        %this.currentAction = clearEmpty @ ETerrainEditor;
        %this.renderVertexSelection = 0 @ ETerrainEditor;
    }
    if ((ETerrainEditor @ " " @ %this.currentMode $= "select")) {
        ETerrainEditor.processAction(ETerrainEditor, %this.currentAction);
    }
    if ((ETerrainEditor @ " " @ %this.currentMode $= "paint")) {
        ETerrainEditor.setAction(ETerrainEditor, %this.currentAction);
    }
};
function EditorMenuBar::onBrushMenuItemSelect(%this, %itemId, %item) {
    EditorMenuBar.setMenuItemChecked("Brush", %item, 1);
    if ((%item $= "Box Brush")) {
        ETerrainEditor.setBrushType(box);
    }
    if ((%item $= "Circle Brush")) {
        ETerrainEditor.setBrushType(ellipse);
    }
    if ((%item $= "Soft Brush")) {
        %this.enableSoftBrushes = 1 @ ETerrainEditor;
    }
    if ((%item $= "Hard Brush")) {
        %this.enableSoftBrushes = 0 @ ETerrainEditor;
    }
    %this.brushSize = %itemId @ ETerrainEditor;
    ETerrainEditor.setBrushSize(%itemId, %itemId);
};
function EditorMenuBar::onRenderModeMenuItemSelect(%this, %itemId, %item) {
    EditorMenuBar.setMenuItemChecked("Render Mode", %item, 1);
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
        %classname = getSubStr(%classname, 0, (1.0 - strlen(%classname)));
        EWorldEditor.selectAllObjectsOfClassName(%classname);
    }
    if ((%item $= "All Types")) {
        %this.selectType = $TypeMasks::ALLTYPES @ EWorldEditor;
    }
    if ((%item $= "Triggers")) {
        %this.selectType = $TypeMasks::TriggerObjectType @ EWorldEditor;
    }
    if ((%item $= "Interiors")) {
        %this.selectType = $TypeMasks::InteriorObjectType @ EWorldEditor;
    }
    if ((%item $= "Audio Emitters")) {
        %this.selectType = $TypeMasks::MarkerObjectType @ EWorldEditor;
    }
    if ((%item $= "Shapes and Sit Markers")) {
        %this.selectType = ($TypeMasks::StaticTSObjectType | $TypeMasks::ShapeBaseObjectType) @ EWorldEditor;
    }
    if ((%item $= "Items")) {
        %this.selectType = $TypeMasks::ItemObjectType @ EWorldEditor;
    }
    if ((%item $= "Antiportals")) {
        %this.selectType = $TypeMasks::AntiPortalObjectType @ EWorldEditor;
    }
    %this.selectType = $TypeMasks::ALLTYPES @ EWorldEditor;
    EditorMenuBar.setMenuItemChecked("Select Type", %item, 1);
    EStatusHud.updateStatus();
};
function EditorMenuBar::onWorldMenuItemSelect(%this, %itemId, %item) {
    if ((%item $= "Lock Selection")) {
        EWorldEditor.lockSelection(1);
    }
    if ((%item $= "Unlock Selection")) {
        EWorldEditor.lockSelection(0);
    }
    if ((%item $= "Hide Selected")) {
        EWorldEditor.hideSelection(1);
    }
    if ((%item $= "Hide All But Selected")) {
        EWorldEditor.hideAllButSelection(1);
    }
    if ((%item $= "Unhide Selected")) {
        EWorldEditor.hideSelection(0);
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
    EditorMenuBar.setMenuItemChecked("World", %item, 1);
    if ((%item $= "Drop at Origin")) {
        %this.dropType = "atOrigin" @ EWorldEditor;
    }
    if ((%item $= "Drop at Camera")) {
        %this.dropType = "atCamera" @ EWorldEditor;
    }
    if ((%item $= "Drop at Camera w/Rot")) {
        %this.dropType = "atCameraRot" @ EWorldEditor;
    }
    if ((%item $= "Drop below Camera")) {
        %this.dropType = "belowCamera" @ EWorldEditor;
    }
    if ((%item $= "Drop at Screen Center")) {
        %this.dropType = "screenCenter" @ EWorldEditor;
    }
    if ((%item $= "Drop to Ground")) {
        %this.dropType = "toGround" @ EWorldEditor;
    }
    if ((%item $= "Drop at Centroid")) {
        %this.dropType = "atCentroid" @ EWorldEditor;
    }
};
function EditorMenuBar::OnSnapToMenuItemSelect(%this, %itemId, %item) {
    EWorldEditor.multiSnapTo(%item);
};
function EditorMenuBar::OnCloneToMenuItemSelect(%this, %itemId, %item) {
    EWorldEditor.CloneTo(%item);
};
function EditorMenuBar::onEditMenuItemSelect(%this, %itemId, %item) {
    if ((%item $= "World Editor Settings...")) {
        Canvas.pushDialog(WorldEditorSettingsDlg, 0);
    }
    if ((%item $= "Terrain Editor Settings...")) {
        Canvas.pushDialog(TerrainEditorValuesSettingsGui, 99);
    }
    if ((%item $= "Relight Scene")) {
        lightScene("");
    }
    if ((forceAlways @ " " @ %item $= "Increase Move Scale")) {
        EWorldEditor.increaseMoveScale();
    }
    if ((%item $= "Toggle Grid Visibility")) {
        %this.renderPlane = !(%this.renderPlane) @ EWorldEditor;
        EWorldEditor;
        %this.renderPlaneHashes = !(%this.renderPlaneHashes) @ EWorldEditor;
        EWorldEditor;
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
    EditorGui.toggleSGTools(%item);
};
function EditorMenuBar::onWindowMenuItemSelect(%this, %itemId, %item) {
    EditorGui.setEditor(%item);
};
function Creator::onWake(%this) {
    Creator.init();
};
function Creator::onSleep(%this) {
    $LastEditorChosenInstantGroup = $instantGroup;
};
function EditorGui::setWorldEditorVisible(%this) {
    EWorldEditor.setVisible(1);
    ETerrainEditor.setVisible(0);
    EditorMenuBar.setMenuVisible("World", 1);
    EditorMenuBar.setMenuVisible("Action", 0);
    EditorMenuBar.setMenuVisible("Brush", 0);
    EWorldEditor.makeFirstResponder(1);
    EditorTree.open(MissionGroup, 1);
};
function EditorGui::setTerrainEditorVisible(%this) {
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
};
function EditorGui::toggleSGTools(%this, %item) {
    if ((%item $= %item[$sgEditorItemNames::sgMenuItem @ 0])) {
        sgLightEditor::toggle();
    }
};
function EditorGui::setEditor(%this, %editor) {
    EditorMenuBar.setMenuItemBitmap("Window", %this.currentEditor, -(1.0));
    EditorMenuBar.setMenuItemBitmap("Window", %editor, 0);
    %this.currentEditor = %editor;
    if ((%editor $= "World Editor")) {
        EWFrame.setVisible(0);
        EWMissionArea.setVisible(0);
        %this.setWorldEditorVisible();
    }
    if ((%editor $= "World Editor Inspector")) {
        EWFrame.setVisible(1);
        EWMissionArea.setVisible(0);
        EWCreatorPane.setVisible(0);
        EWInspectorPane.setVisible(1);
        %this.setWorldEditorVisible();
    }
    if ((%editor $= "World Editor Creator")) {
        EWFrame.setVisible(1);
        EWMissionArea.setVisible(0);
        EWCreatorPane.setVisible(1);
        EWInspectorPane.setVisible(0);
        %this.setWorldEditorVisible();
    }
    if ((%editor $= "Mission Area Editor")) {
        EWFrame.setVisible(0);
        EWMissionArea.setVisible(1);
        %this.setWorldEditorVisible();
    }
    if ((%editor $= "Terrain Editor")) {
        %this.setTerrainEditorVisible();
    }
    if ((%editor $= "Terrain Terraform Editor")) {
        %this.setTerrainEditorVisible();
        EHeightField.setVisible(1);
    }
    if ((%editor $= "Terrain Texture Editor")) {
        %this.setTerrainEditorVisible();
        ETexture.setVisible(1);
    }
    if ((%editor $= "Terrain Texture Painter")) {
        %this.setTerrainEditorVisible();
        EPainter.setVisible(1);
        EPainter.setup();
    }
};
function EditorGui::getHelpPage(%this) {
    if ((%this.currentEditor $= "World Editor")) {
        if ((%this.currentEditor $= "World Editor Inspector")) {
        }
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
    %this.paintMaterial = %this.mat @ ETerrainEditor;
    %matIndex @ EPainter;
};
function ETerrainEditor::changeMaterial(%this, %matIndex) {
    %this.matIndex = %matIndex @ EPainter;
    getLoadFilename("*/terrains/*.png\t*/terrains/*.jpg");
};
function EPainterChangeMat(%file) {
    %file = filePath(%file) @ "/" @ fileBase(%file);
    %i = 0;
    if ((6.0 < %i)) {
        if ((%i @ EPainter @ " " @ %this.mat $= %file)) {
            return;
        }
        %i = (1.0 + %i);
    }
    %this.mat = EPainter @ %this.matIndex @ EPainter;
    %file;
    %mats = "";
    (6.0 < %i);
    %i = 0;
    if ((6.0 < %i)) {
        %mats = %i @ EPainter @ %this.mat @ "\n";
        %mats;
        %i = (1.0 + %i);
    }
    ETerrainEditor.setTerrainMaterials(%mats);
    EPainter.setup();
    "ETerrainMaterialPaint".performClick(EPainter @ %this.matIndex);
};
function EPainter::setup(%this) {
    EditorMenuBar.onActionMenuItemSelect(0, "Paint Material");
    %mats = ETerrainEditor.getTerrainMaterials();
    %valid = 1;
    %i = 0;
    if ((6.0 < %i)) {
        %mat = getRecord(%mats, %i);
        %this.mat = %mat @ %i;
        "ETerrainMaterialText" @ %i.setText(fileBase(%mat));
        "ETerrainMaterialBitmap" @ %i.setBitmap(%mat);
        "ETerrainMaterialChange" @ %i.setActive(1);
        "ETerrainMaterialPaint" @ %i.setActive(!(%mat $= ""));
        if ((%mat $= "")) {
            "ETerrainMaterialChange" @ %i.setText("Add...");
            if (%valid) {
                %valid = 0;
            }
            "ETerrainMaterialChange" @ %i.setActive(0);
        }
        "ETerrainMaterialChange" @ %i.setText("Change...");
        %i = (1.0 + %i);
    }
    ETerrainMaterialPaint0.performClick();
};
function EditorGui::onWake(%this) {
    moveMap.push();
    EditorMap.push();
    %this.setEditor(%this.currentEditor);
};
function EditorGui::onSleep(%this) {
    EditorMap.pop();
    moveMap.pop();
};
function AreaEditor::onUpdate(%this, %area) {
    AreaEditingText.setValue("X: " @ getWord(%area, 0) @ " Y: " @ getWord(%area, 1) @ " W: " @ getWord(%area, 2) @ " H: " @ getWord(%area, 3));
};
function AreaEditor::onWorldOffset(%this, %unused) {
};
function RecurseInvertSelectObjectsInGroup(%theSimGroup) {
    %count = %theSimGroup.getCount();
    %i = 0;
    if ((%count < %i)) {
        %object = %theSimGroup.getObject(%i);
        if (%object.isClassSimGroup()) {
            RecurseInvertSelectObjectsInGroup(%object);
        }
        EWorldEditor.invertSelectObject(%object);
        %i = (1.0 + %i);
    }
};
function RecurseSelectObjectsInGroup(%theSimGroup, %classname) {
    %count = %theSimGroup.getCount();
    %i = 0;
    if ((%count < %i)) {
        %object = %theSimGroup.getObject(%i);
        if (%object.isClassSimGroup()) {
            RecurseSelectObjectsInGroup(%object, %classname);
        }
        if ((%classname $= "")) {
        }
        if ((%object.getClassName() $= %classname)) {
            EWorldEditor.selectObject(%object);
        }
        %i = (1.0 + %i);
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
    %this.mouseMoveScale = (EWorldEditor * %this.mouseMoveScale);
    2.0;
    if ((EWorldEditor > %this.mouseMoveScale)) {
        %this.mouseMoveScale = %max @ EWorldEditor;
        %max;
    }
    EditorGui.setPrefs();
};
function WorldEditor::decreaseMoveScale(%this) {
    %min = 0.001;
    %this.mouseMoveScale = (EWorldEditor / %this.mouseMoveScale);
    2.0;
    if ((EWorldEditor < %this.mouseMoveScale)) {
        %this.mouseMoveScale = %min @ EWorldEditor;
        %min;
    }
    EditorGui.setPrefs();
};
function WorldEditor::onDelete(%this) {
    EditorTree.deleteSelection();
    inspector.uninspect();
    EStatusHud.updateStatus();
    EWorldEditor.updateGeneralInfo("");
};
function WorldEditor::onSelect(%this, %obj) {
    EditorTree.addSelection(%obj);
    EStatusHud.updateStatus();
    EWorldEditor.updateGeneralInfo("");
};
function WorldEditor::onUnSelect(%this, %obj) {
    EditorTree.removeSelection(%obj);
    inspector.uninspect();
    EStatusHud.updateStatus();
    EWorldEditor.updateGeneralInfo("");
};
function WorldEditor::onClearSelected(%this) {
    EditorTree.clearSelection();
    inspector.uninspect();
    EStatusHud.updateStatus();
    EWorldEditor.updateGeneralInfo("");
};
function WorldEditor::onClearSelection(%this) {
    EditorTree.clearSelection();
    inspector.uninspect();
    EStatusHud.updateStatus();
    EWorldEditor.updateGeneralInfo("");
};
function EditorTree::onDragDrop(%this) {
    %this.isDirty = 1 @ EditorTree;
};
function EditorTree::onObjectDeleteCompleted(%this) {
    %this.isDirty = 1 @ EditorTree;
    EWorldEditor.copySelection();
    EWorldEditor.deleteSelection();
    inspector.uninspect();
    EStatusHud.updateStatus();
    EWorldEditor.updateGeneralInfo("");
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
    ETContextPopup.setVisible(0);
};
function EditorTree::OnInspect(%this, %obj) {
    inspector.inspect(%obj);
    InspectorNameEdit.setValue(ETContextPopupDlg.getName(%obj));
    EWorldEditor.updateGeneralInfo(%obj);
};
function EditorTree::onAddSelection(%this, %obj) {
    if ($AIEdit) {
        aiEdit.selectObject(%obj);
    }
    EWorldEditor.selectObject(%obj);
    %obj.isNetCacheable = TEST_MISSIONGROUPINTEGRITY.getInitialNetCacheable(%obj);
};
function EditorTree::onRemoveSelection(%this, %obj) {
    if ($AIEdit) {
        aiEdit.selectObject(%obj);
    }
    EWorldEditor.unselectObject(%obj);
};
function EditorTree::onSelect(%this, %obj) {
    EWorldEditor.clearSelection();
    if (%obj.isClassSimGroup()) {
        inspector.inspect(%obj);
        InspectorNameEdit.setValue(%obj.getName());
        $userPref::Editor::autoSelectGroupContents = $userPref::Editor::autoSelectGroupContents;
        if ($userPref::Editor::autoSelectGroupContents) {
            RecurseSelectObjectsInGroup(%obj, "");
        }
    }
    if ($AIEdit) {
        aiEdit.selectObject(%obj);
    }
    EWorldEditor.selectObject(%obj);
};
function EditorTree::onUnSelect(%this, %obj) {
    if ($AIEdit) {
        aiEdit.unselectObject(%obj);
    }
    EWorldEditor.unselectObject(%obj);
};
function ETContextPopup::onSelect(%this, %index, %unused) {
    if ((0.0 == %index)) {
        EditorTree.delete(%obj.contextObj);
    }
};
function WorldEditor::init(%this) {
    %this.ignoreObjClass();
    %this.numEditModes = AIObjective @ 3;
    Sky;
    %this.editMode = TerrainBlock @ "move" @ 0;
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
    WEContextPopup.setVisible(0);
};
function WorldEditor::onDblClick(%this, %obj) {
};
function WorldEditor::onClick(%this, %obj) {
    WEContextPopupDlg.updateStatus(EStatusHud);
    EWorldEditor.updateGeneralInfo("");
    inspector.inspect(%obj);
    InspectorNameEdit.setValue(WEContextPopupDlg.getName(%obj));
};
function WorldEditor::onEndDrag(%this, %obj) {
    WEContextPopupDlg.updateStatus(EStatusHud);
    inspector.inspect(%obj);
    InspectorNameEdit.setValue(WEContextPopupDlg.getName(%obj));
};
function WorldEditor::export(%this) {
    getSaveFilename("~/editor/*.mac", %this @ ".doExport", "selection.mac");
};
function WorldEditor::doExport(%this, %file) {
    MissionGroup.save("~/editor/" @ %file, 1);
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
    if ((%this.getSelectionSize() < %i)) {
        %obj = %this.getSelectedObject(%i);
        if ((%obj.locked $= "true")) {
            %ret = (1.0 + %ret);
        }
        %i = (1.0 + %i);
    }
    return %ret;
};
function WorldEditor::getSelectionHiddenCount(%this) {
    %ret = 0;
    %i = (1.0 - %this.getSelectionSize());
    if ((0.0 >= %i)) {
        %obj = %this.getSelectedObject(%i);
        if (%obj.noShow) {
            %ret = (1.0 + %ret);
        }
        %i = (1.0 - %i);
    }
    return %ret;
};
function WorldEditor::snapTo(%this, %snapType, %objTarget, %objToSnap) {
    if ((%objTarget $= "")) {
        %objTarget = %this.getSelectedObject(0);
    }
    if ((%objToSnap $= "")) {
        %objToSnap = %this.getSelectedObject((1.0 - %this.getSelectionSize()));
    }
    if ((%objTarget $= "")) {
    }
    if ((%objToSnap $= "")) {
        error("Please select two objects before selecting a Snap To funciton.");
        return;
    }
    if ((%snapType $= "X")) {
        %this.snapToX(%objTarget, %objToSnap);
    }
    if ((%snapType $= "X-")) {
        %this.snapToXNeg(%objTarget, %objToSnap);
    }
    if ((%snapType $= "X+")) {
        %this.snapToXPos(%objTarget, %objToSnap);
    }
    if ((%snapType $= "Y")) {
        %this.snapToY(%objTarget, %objToSnap);
    }
    if ((%snapType $= "Y-")) {
        %this.snapToYNeg(%objTarget, %objToSnap);
    }
    if ((%snapType $= "Y+")) {
        %this.snapToYPos(%objTarget, %objToSnap);
    }
    if ((%snapType $= "Z")) {
        %this.snapToZ(%objTarget, %objToSnap);
    }
    if ((%snapType $= "Z-")) {
        %this.snapToZNeg(%objTarget, %objToSnap);
    }
    if ((%snapType $= "Z+")) {
        %this.snapToZPos(%objTarget, %objToSnap);
    }
    if ((%snapType $= "X+YZ")) {
        %this.snapToXPosYZ(%objTarget, %objToSnap);
    }
    if ((%snapType $= "X-YZ")) {
        %this.snapToXNegYZ(%objTarget, %objToSnap);
    }
    if ((%snapType $= "XY+Z")) {
        %this.snapToXYPosZ(%objTarget, %objToSnap);
    }
    if ((%snapType $= "XY-Z")) {
        %this.snapToXYNegZ(%objTarget, %objToSnap);
    }
    if ((%snapType $= "XYZ+")) {
        %this.snapToXYZPos(%objTarget, %objToSnap);
    }
    if ((%snapType $= "XYZ-")) {
        %this.snapToXYZNeg(%objTarget, %objToSnap);
    }
    if ((%snapType $= "ObjX+YZ")) {
        %this.snapToObjXPosYZ(%objTarget, %objToSnap);
    }
    if ((%snapType $= "ObjX-YZ")) {
        %this.snapToObjXNegYZ(%objTarget, %objToSnap);
    }
    if ((%snapType $= "ObjXY+Z")) {
        %this.snapToObjXYPosZ(%objTarget, %objToSnap);
    }
    if ((%snapType $= "ObjXY-Z")) {
        %this.snapToObjXYNegZ(%objTarget, %objToSnap);
    }
    if ((%snapType $= "ObjXYZ+")) {
        %this.snapToObjXYZPos(%objTarget, %objToSnap);
    }
    if ((%snapType $= "ObjXYZ-")) {
        %this.snapToObjXYZNeg(%objTarget, %objToSnap);
    }
};
function WorldEditor::snapToX(%this, %objTarget, %objToSnap) {
    %objToSnap.setTransform(setWord(%objToSnap.getTransform(), 0, EWorldEditor, (%obj.snapGapX + getWord(%objTarget.getTransform(), 0))));
};
function WorldEditor::snapToXPos(%this, %objTarget, %objToSnap) {
    %edgeOffset = mAbs((getWord(%objToSnap.getTransform(), 0) - getWord(%objToSnap.getWorldBox(), 0)));
    %transformWithOffset = (%edgeOffset + getWord(%objTarget.getWorldBox(), 3));
    %objToSnap.setTransform(setWord(%objToSnap.getTransform(), 0, %transformWithOffset));
};
function WorldEditor::snapToXNeg(%this, %objTarget, %objToSnap) {
    %edgeOffset = mAbs((getWord(%objToSnap.getTransform(), 0) - getWord(%objToSnap.getWorldBox(), 3)));
    %transformWithOffset = (%edgeOffset - getWord(%objTarget.getWorldBox(), 0));
    %objToSnap.setTransform(setWord(%objToSnap.getTransform(), 0, %transformWithOffset));
};
function WorldEditor::snapToY(%this, %objTarget, %objToSnap) {
    %objToSnap.setTransform(setWord(%objToSnap.getTransform(), 1, EWorldEditor, (%obj.snapGapY + getWord(%objTarget.getTransform(), 1))));
};
function WorldEditor::snapToYPos(%this, %objTarget, %objToSnap) {
    %edgeOffset = mAbs((getWord(%objToSnap.getTransform(), 1) - getWord(%objToSnap.getWorldBox(), 1)));
    %transformWithOffset = (%edgeOffset + getWord(%objTarget.getWorldBox(), 4));
    %objToSnap.setTransform(setWord(%objToSnap.getTransform(), 1, %transformWithOffset));
};
function WorldEditor::snapToYNeg(%this, %objTarget, %objToSnap) {
    %edgeOffset = mAbs((getWord(%objToSnap.getTransform(), 1) - getWord(%objToSnap.getWorldBox(), 4)));
    %transformWithOffset = (%edgeOffset - getWord(%objTarget.getWorldBox(), 1));
    %objToSnap.setTransform(setWord(%objToSnap.getTransform(), 1, %transformWithOffset));
};
function WorldEditor::snapToZ(%this, %objTarget, %objToSnap) {
    %objToSnap.setTransform(setWord(%objToSnap.getTransform(), 2, EWorldEditor, (%obj.snapGapZ + getWord(%objTarget.getTransform(), 2))));
};
function WorldEditor::snapToZPos(%this, %objTarget, %objToSnap) {
    %edgeOffset = mAbs((getWord(%objToSnap.getTransform(), 2) - getWord(%objToSnap.getWorldBox(), 2)));
    %transformWithOffset = (%edgeOffset + getWord(%objTarget.getWorldBox(), 5));
    %objToSnap.setTransform(setWord(%objToSnap.getTransform(), 2, %transformWithOffset));
};
function WorldEditor::snapToZNeg(%this, %objTarget, %objToSnap) {
    %edgeOffset = mAbs((getWord(%objToSnap.getTransform(), 2) - getWord(%objToSnap.getWorldBox(), 5)));
    %transformWithOffset = (%edgeOffset - getWord(%objTarget.getWorldBox(), 2));
    %objToSnap.setTransform(setWord(%objToSnap.getTransform(), 2, %transformWithOffset));
};
function WorldEditor::snapToXPosYZ(%this, %objTarget, %objToSnap) {
    %this.snapToXPos(%objTarget, %objToSnap);
    %this.snapToY(%objTarget, %objToSnap);
    %this.snapToZ(%objTarget, %objToSnap);
};
function WorldEditor::snapToXNegYZ(%this, %objTarget, %objToSnap) {
    %this.snapToXNeg(%objTarget, %objToSnap);
    %this.snapToY(%objTarget, %objToSnap);
    %this.snapToZ(%objTarget, %objToSnap);
};
function WorldEditor::snapToXYPosZ(%this, %objTarget, %objToSnap) {
    %this.snapToX(%objTarget, %objToSnap);
    %this.snapToYPos(%objTarget, %objToSnap);
    %this.snapToZ(%objTarget, %objToSnap);
};
function WorldEditor::snapToXYNegZ(%this, %objTarget, %objToSnap) {
    %this.snapToX(%objTarget, %objToSnap);
    %this.snapToYNeg(%objTarget, %objToSnap);
    %this.snapToZ(%objTarget, %objToSnap);
};
function WorldEditor::snapToXYZPos(%this, %objTarget, %objToSnap) {
    %this.snapToX(%objTarget, %objToSnap);
    %this.snapToY(%objTarget, %objToSnap);
    %this.snapToZPos(%objTarget, %objToSnap);
};
function WorldEditor::snapToXYZNeg(%this, %objTarget, %objToSnap) {
    %this.snapToX(%objTarget, %objToSnap);
    %this.snapToY(%objTarget, %objToSnap);
    %this.snapToZNeg(%objTarget, %objToSnap);
};
function WorldEditor::snapToObjXPosYZ(%this, %objTarget, %objToSnap) {
    %edgeOffset = (mAbs(getWord(%objTarget.getObjectBox(), 0)) + mAbs(getWord(%objToSnap.getObjectBox(), 3)));
    %worldTransform = %objTarget.getWorldTransform();
    %worldTransform = setWord(%worldTransform, 0, (%edgeOffset + getWord(%worldTransform, 0)));
    %offsetMatrix = MatrixMultiply(%objTarget.getTransform(), %worldTransform);
    %newTransform = %objToSnap.getTransform();
    %newTransform = setWord(%newTransform, 0, (getWord(%offsetMatrix, 0) + getWord(%objTarget.getTransform(), 0)));
    %newTransform = setWord(%newTransform, 1, (getWord(%offsetMatrix, 1) + getWord(%objTarget.getTransform(), 1)));
    %newTransform = setWord(%newTransform, 2, (getWord(%offsetMatrix, 2) + getWord(%objTarget.getTransform(), 2)));
    %objToSnap.setTransform(%newTransform);
};
function WorldEditor::snapToObjXNegYZ(%this, %objTarget, %objToSnap) {
    %edgeOffset = (mAbs(getWord(%objTarget.getObjectBox(), 3)) + mAbs(getWord(%objToSnap.getObjectBox(), 0)));
    %worldTransform = %objTarget.getWorldTransform();
    %worldTransform = setWord(%worldTransform, 0, (%edgeOffset - getWord(%worldTransform, 0)));
    %offsetMatrix = MatrixMultiply(%objTarget.getTransform(), %worldTransform);
    %newTransform = %objToSnap.getTransform();
    %newTransform = setWord(%newTransform, 0, (getWord(%offsetMatrix, 0) + getWord(%objTarget.getTransform(), 0)));
    %newTransform = setWord(%newTransform, 1, (getWord(%offsetMatrix, 1) + getWord(%objTarget.getTransform(), 1)));
    %newTransform = setWord(%newTransform, 2, (getWord(%offsetMatrix, 2) + getWord(%objTarget.getTransform(), 2)));
    %objToSnap.setTransform(%newTransform);
};
function WorldEditor::snapToObjXYPosZ(%this, %objTarget, %objToSnap) {
    %edgeOffset = (mAbs(getWord(%objTarget.getObjectBox(), 1)) + mAbs(getWord(%objToSnap.getObjectBox(), 4)));
    %worldTransform = %objTarget.getWorldTransform();
    %worldTransform = setWord(%worldTransform, 1, (%edgeOffset + getWord(%worldTransform, 1)));
    %offsetMatrix = MatrixMultiply(%objTarget.getTransform(), %worldTransform);
    %newTransform = %objToSnap.getTransform();
    %newTransform = setWord(%newTransform, 0, (getWord(%offsetMatrix, 0) + getWord(%objTarget.getTransform(), 0)));
    %newTransform = setWord(%newTransform, 1, (getWord(%offsetMatrix, 1) + getWord(%objTarget.getTransform(), 1)));
    %newTransform = setWord(%newTransform, 2, (getWord(%offsetMatrix, 2) + getWord(%objTarget.getTransform(), 2)));
    %objToSnap.setTransform(%newTransform);
};
function WorldEditor::snapToObjXYNegZ(%this, %objTarget, %objToSnap) {
    %edgeOffset = (mAbs(getWord(%objTarget.getObjectBox(), 4)) + mAbs(getWord(%objToSnap.getObjectBox(), 1)));
    %worldTransform = %objTarget.getWorldTransform();
    %worldTransform = setWord(%worldTransform, 1, (%edgeOffset - getWord(%worldTransform, 1)));
    %offsetMatrix = MatrixMultiply(%objTarget.getTransform(), %worldTransform);
    %newTransform = %objToSnap.getTransform();
    %newTransform = setWord(%newTransform, 0, (getWord(%offsetMatrix, 0) + getWord(%objTarget.getTransform(), 0)));
    %newTransform = setWord(%newTransform, 1, (getWord(%offsetMatrix, 1) + getWord(%objTarget.getTransform(), 1)));
    %newTransform = setWord(%newTransform, 2, (getWord(%offsetMatrix, 2) + getWord(%objTarget.getTransform(), 2)));
    %objToSnap.setTransform(%newTransform);
};
function WorldEditor::snapToObjXYZPos(%this, %objTarget, %objToSnap) {
    %edgeOffset = (mAbs(getWord(%objTarget.getObjectBox(), 2)) + mAbs(getWord(%objToSnap.getObjectBox(), 5)));
    %worldTransform = %objTarget.getWorldTransform();
    %worldTransform = setWord(%worldTransform, 2, (%edgeOffset + getWord(%worldTransform, 2)));
    %offsetMatrix = MatrixMultiply(%objTarget.getTransform(), %worldTransform);
    %newTransform = %objToSnap.getTransform();
    %newTransform = setWord(%newTransform, 0, (getWord(%offsetMatrix, 0) + getWord(%objTarget.getTransform(), 0)));
    %newTransform = setWord(%newTransform, 1, (getWord(%offsetMatrix, 1) + getWord(%objTarget.getTransform(), 1)));
    %newTransform = setWord(%newTransform, 2, (getWord(%offsetMatrix, 2) + getWord(%objTarget.getTransform(), 2)));
    %objToSnap.setTransform(%newTransform);
};
function WorldEditor::snapToObjXYZNeg(%this, %objTarget, %objToSnap) {
    %edgeOffset = (mAbs(getWord(%objTarget.getObjectBox(), 5)) + mAbs(getWord(%objToSnap.getObjectBox(), 2)));
    %worldTransform = %objTarget.getWorldTransform();
    %worldTransform = setWord(%worldTransform, 2, (%edgeOffset - getWord(%worldTransform, 2)));
    %offsetMatrix = MatrixMultiply(%objTarget.getTransform(), %worldTransform);
    %newTransform = %objToSnap.getTransform();
    %newTransform = setWord(%newTransform, 0, (getWord(%offsetMatrix, 0) + getWord(%objTarget.getTransform(), 0)));
    %newTransform = setWord(%newTransform, 1, (getWord(%offsetMatrix, 1) + getWord(%objTarget.getTransform(), 1)));
    %newTransform = setWord(%newTransform, 2, (getWord(%offsetMatrix, 2) + getWord(%objTarget.getTransform(), 2)));
    %objToSnap.setTransform(%newTransform);
};
function WorldEditor::CloneTo(%this, %snapType) {
    %selSize = %this.getSelectionSize();
    %i = 0;
    if ((%selSize < %i)) {
        %i[%origObjects @ %i] = %this.getSelectedObject(%i);
        %i = (1.0 + %i);
    }
    %i = 0;
    (%selSize < %i);
    if ((%selSize < %i)) {
        %this.clearSelection();
        %objTarget = %i[%origObjects @ %i];
        %this.selectObject(%objTarget);
        %this.copySelection();
        %this.pasteSelection();
        %objToSnap = %this.getSelectedObject(0);
        %i[%newObjects @ %i] = %objToSnap;
        %this.snapTo(%snapType, %objTarget, %objToSnap);
        %i = (1.0 + %i);
    }
    %this.clearSelection();
    %i = 0;
    (%selSize < %i);
    if ((%selSize < %i)) {
        %this.selectObject(%i[%newObjects @ %i]);
        %i = (1.0 + %i);
    }
};
function WorldEditor::multiSnapTo(%this, %snapType) {
    echo("in multiSnapTo w/ type" @ " " @ %snapType);
    %selSize = %this.getSelectionSize();
    %objTarget = %this.getSelectedObject((1.0 - %this.getSelectionSize()));
    %i = 0;
    if (((1.0 - %selSize) < %i)) {
        %objToSnap = %this.getSelectedObject(%i);
        %this.snapTo(%snapType, %objTarget, %objToSnap);
        %i = (1.0 + %i);
    }
};
function WorldEditor::dropCameraToSelection(%this) {
    if ((0.0 == %this.getSelectionSize())) {
        return;
    }
    %pos = %this.getSelectionCentroid();
    %cam = LocalClientConnection.getTransform(%obj.Camera);
    %cam = setWord(%cam, 0, getWord(%pos, 0));
    %cam = setWord(%cam, 1, getWord(%pos, 1));
    %cam = setWord(%cam, 2, getWord(%pos, 2));
    LocalClientConnection.setTransform(%obj.Camera, %cam);
    %control = LocalClientConnection.getControlObject();
    if ((%obj.Camera != %control)) {
        toggleCamera();
    }
};
function WorldEditor::dropCameraWithSelectionInView(%this) {
    if ((0.0 == %this.getSelectionSize())) {
        return;
    }
    %curCam = LocalClientConnection.getControlObject();
    %camera = %obj.Camera;
    LocalClientConnection;
    %pos = %this.getSelectionBoxCentroid();
    %rad = %this.getSelectionBoxRadius();
    %rad = (1.5 * %rad);
    %fov = mDegToRad(getFovCur());
    %eyeDir = %curCam.getEyeVector();
    %camPosition = fitCameraConeAroundSphere(%pos, %rad, %eyeDir, %fov);
    %camTransform = %curCam.getEyeTransform();
    %camTransform = setWord(%camTransform, 0, getWord(%camPosition, 0));
    %camTransform = setWord(%camTransform, 1, getWord(%camPosition, 1));
    %camTransform = setWord(%camTransform, 2, getWord(%camPosition, 2));
    %camera.setTransform(%camTransform);
    if ((%camera != %curCam)) {
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
    if ((%this.getSelectionSize() < %i)) {
        %obj = %this.getSelectedObject(%i);
        $instantGroup.add(%obj);
        %i = (1.0 + %i);
    }
};
function WorldEditor::resetTransforms(%this) {
    %this.addUndoState();
    %i = 0;
    if ((%this.getSelectionSize() < %i)) {
        %obj = %this.getSelectedObject(%i);
        %transform = %obj.getTransform();
        %transform = setWord(%transform, 3, 0);
        %transform = setWord(%transform, 4, 0);
        %transform = setWord(%transform, 5, 1);
        %transform = setWord(%transform, 6, 0);
        %obj.setTransform(%transform);
        %obj.setScale("1 1 1");
        %i = (1.0 + %i);
    }
};
function WorldEditorToolbarDlg::init(%this) {
    WorldEditorInspectorCheckBox.setValue(WorldEditorToolFrameSet.isMember("EditorToolInspectorGui"));
    WorldEditorMissionAreaCheckBox.setValue(WorldEditorToolFrameSet.isMember("EditorToolMissionAreaGui"));
    WorldEditorTreeCheckBox.setValue(WorldEditorToolFrameSet.isMember("EditorToolTreeViewGui"));
    WorldEditorCreatorCheckBox.setValue(WorldEditorToolFrameSet.isMember("EditorToolCreatorGui"));
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
    %base = %this.insertItem(0, "Interiors");
    %interiorId = "";
    %file = findFirstFile("*.dif");
    echo(" Creator::init  loading interiors");
    if (!(%file $= "")) {
        %split = strreplace(%file, "/", " ");
        %dirCount = (1.0 - getWordCount(%split));
        %parentId = %base;
        %i = 0;
        if ((%dirCount < %i)) {
            %parent = getWords(%split, 0, %i);
            if (!(%parent[%interiorId @ %parent])) {
                %parent[%interiorId @ %parent] = %this.insertItem(%parentId, getWord(%split, %i));
            }
            %parentId = %parent[%interiorId @ %parent];
            %i = (1.0 + %i);
        }
        %create = "createInterior(" @ "\"" @ %file @ "\"" @ ");";
        (%dirCount < %i);
        %this.insertItem(%parentId, fileBase(%file), %create, "Interior");
        %file = findNextFile("*.dif");
    }
    echo(" Creator::init  loading shapes");
    %base = %this.insertItem(0, "Shapes");
    !(%file $= "");
    %dataGroup = "DataBlockGroup";
    %i = 0;
    if ((%dataGroup.getCount() < %i)) {
        %obj = %dataGroup.getObject(%i);
        echo("Obj: " @ %obj.getName() @ " - " @ %obj.category);
        if (!(%obj.category $= "")) {
        }
        if ((0.0 != %obj.category)) {
            %id = %this.findItemByName(%obj.category);
            if ((0.0 == %id)) {
                %grp = %this.insertItem(%base, %obj.category);
                %this.insertItem(%grp, %obj.getName(), %obj.getClassName() @ "::create(" @ %obj.getName() @ ");", "Item");
            }
            %this.insertItem(%id, %obj.getName(), %obj.getClassName() @ "::create(" @ %obj.getName() @ ");", "Item");
        }
        %i = (1.0 + %i);
    }
    echo(" Creator::init  loading static shapes");
    %base = %this.insertItem(0, "Static Shapes");
    (%dataGroup.getCount() < %i);
    %staticId = "";
    %file = findFirstFile("*.dts");
    if (!(%file $= "")) {
        %split = strreplace(%file, "/", " ");
        %dirCount = (1.0 - getWordCount(%split));
        %parentId = %base;
        %i = 0;
        if ((%dirCount < %i)) {
            %parent = getWords(%split, 0, %i);
            if (!(%parent[%staticId @ %parent])) {
                %parent[%staticId @ %parent] = %this.insertItem(%parentId, getWord(%split, %i));
            }
            %parentId = %parent[%staticId @ %parent];
            %i = (1.0 + %i);
        }
        %create = "TSStatic::create(\"" @ %file @ "\");";
        (%dirCount < %i);
        %this.insertItem(%parentId, fileBase(%file), %create, "TSStatic");
        %file = findNextFile("*.dts");
    }
    %base = %this.insertItem(0, "Dynamic Shapes");
    !(%file $= "");
    %dynamicID = "";
    %file = findFirstFile("*.dts");
    if (!(%file $= "")) {
        %split = strreplace(%file, "/", " ");
        %dirCount = (1.0 - getWordCount(%split));
        %parentId = %base;
        %i = 0;
        if ((%dirCount < %i)) {
            %parent = getWords(%split, 0, %i);
            if (!(%parent[%dynamicID @ %parent])) {
                %parent[%dynamicID @ %parent] = %this.insertItem(%parentId, getWord(%split, %i));
            }
            %parentId = %parent[%dynamicID @ %parent];
            %i = (1.0 + %i);
        }
        %create = "TSDynamic::create(\"" @ %file @ "\");";
        (%dirCount < %i);
        %this.insertItem(%parentId, fileBase(%file), %create, "TSDynamic");
        %file = findNextFile("*.dts");
    }
    %file[%objGroup @ 0] = !(%file $= "") @ "Environment";
    %file[%objGroup @ 0][%objGroup @ 1] = "Mission";
    %file[%objGroup @ 0][%objGroup @ 1][%objGroup @ 2] = "System";
    %env_item_idx = -(1.0);
    %env_item_idx = (1.0 + %env_item_idx);
    %env_item_idx["Sky" @ %Environment_Item] = ;
    %env_item_idx = (1.0 + %env_item_idx);
    %env_item_idx["Sun" @ %Environment_Item] = ;
    %env_item_idx = (1.0 + %env_item_idx);
    %env_item_idx["Lightning" @ %Environment_Item] = ;
    %env_item_idx = (1.0 + %env_item_idx);
    %env_item_idx["Water" @ %Environment_Item] = ;
    %env_item_idx = (1.0 + %env_item_idx);
    %env_item_idx["Terrain" @ %Environment_Item] = ;
    %env_item_idx = (1.0 + %env_item_idx);
    %env_item_idx["AudioEmitter" @ %Environment_Item] = ;
    %env_item_idx = (1.0 + %env_item_idx);
    %env_item_idx["Precipitation" @ %Environment_Item] = ;
    %env_item_idx = (1.0 + %env_item_idx);
    %env_item_idx["ParticleEmitter" @ %Environment_Item] = ;
    %env_item_idx = (1.0 + %env_item_idx);
    %env_item_idx["fxSunLight" @ %Environment_Item] = ;
    %env_item_idx = (1.0 + %env_item_idx);
    %env_item_idx["fxShapeReplicator" @ %Environment_Item] = ;
    %env_item_idx = (1.0 + %env_item_idx);
    %env_item_idx["fxFoliageReplicator" @ %Environment_Item] = ;
    %env_item_idx = (1.0 + %env_item_idx);
    %env_item_idx["fxLight" @ %Environment_Item] = ;
    %env_item_idx = (1.0 + %env_item_idx);
    %env_item_idx["TSText" @ %Environment_Item] = ;
    %env_item_idx = (1.0 + %env_item_idx);
    %env_item_idx["sgUniversalStaticLight" @ %Environment_Item] = ;
    %env_item_idx = (1.0 + %env_item_idx);
    %env_item_idx["sgMissionLightingFilter" @ %Environment_Item] = ;
    %env_item_idx = (1.0 + %env_item_idx);
    %env_item_idx["sgDecalProjector" @ %Environment_Item] = ;
    %env_item_idx = (1.0 + %env_item_idx);
    %env_item_idx["volumeLight" @ %Environment_Item] = ;
    if (isFunction("Using_DF")) {
    }
    if (Using_DF()) {
        %env_item_idx = (1.0 + %env_item_idx);
        %env_item_idx["DFTextureAdvert" @ %Environment_Item] = ;
    }
    if (Using_DShow()) {
        %env_item_idx = (1.0 + %env_item_idx);
        %env_item_idx["DSRenderer" @ %Environment_Item] = ;
    }
    if (Using_Theora()) {
        %env_item_idx = (1.0 + %env_item_idx);
        %env_item_idx["TheoraRenderer" @ %Environment_Item] = ;
    }
    if (Using_FFMPEG()) {
        %env_item_idx = (1.0 + %env_item_idx);
        %env_item_idx["FFMPEGRenderer" @ %Environment_Item] = ;
    }
    %env_item_idx = (1.0 + %env_item_idx);
    %env_item_idx["SlaveRenderer" @ %Environment_Item] = ;
    %env_item_idx["SlaveRenderer" @ %Environment_Item][%Mission_Item @ 0] = "MissionArea";
    %env_item_idx["SlaveRenderer" @ %Environment_Item][%Mission_Item @ 0][%Mission_Item @ 1] = "Path";
    %env_item_idx["SlaveRenderer" @ %Environment_Item][%Mission_Item @ 0][%Mission_Item @ 1][%Mission_Item @ 2] = "PathMarker";
    %env_item_idx["SlaveRenderer" @ %Environment_Item][%Mission_Item @ 0][%Mission_Item @ 1][%Mission_Item @ 2][%Mission_Item @ 3] = "Trigger";
    %env_item_idx["SlaveRenderer" @ %Environment_Item][%Mission_Item @ 0][%Mission_Item @ 1][%Mission_Item @ 2][%Mission_Item @ 3][%Mission_Item @ 4] = "PhysicalZone";
    %env_item_idx["SlaveRenderer" @ %Environment_Item][%Mission_Item @ 0][%Mission_Item @ 1][%Mission_Item @ 2][%Mission_Item @ 3][%Mission_Item @ 4][%Mission_Item @ 5] = "Camera";
    %env_item_idx["SlaveRenderer" @ %Environment_Item][%Mission_Item @ 0][%Mission_Item @ 1][%Mission_Item @ 2][%Mission_Item @ 3][%Mission_Item @ 4][%Mission_Item @ 5][%Mission_Item @ 6] = "AntiPortal";
    %env_item_idx["SlaveRenderer" @ %Environment_Item][%Mission_Item @ 0][%Mission_Item @ 1][%Mission_Item @ 2][%Mission_Item @ 3][%Mission_Item @ 4][%Mission_Item @ 5][%Mission_Item @ 6][%Mission_Item @ 6] = "ZoneBox";
    %env_item_idx["SlaveRenderer" @ %Environment_Item][%Mission_Item @ 0][%Mission_Item @ 1][%Mission_Item @ 2][%Mission_Item @ 3][%Mission_Item @ 4][%Mission_Item @ 5][%Mission_Item @ 6][%Mission_Item @ 6][%System_Item @ 0] = "SimGroup";
    %env_item_idx["SlaveRenderer" @ %Environment_Item][%Mission_Item @ 0][%Mission_Item @ 1][%Mission_Item @ 2][%Mission_Item @ 3][%Mission_Item @ 4][%Mission_Item @ 5][%Mission_Item @ 6][%Mission_Item @ 6][%System_Item @ 0][%System_Item @ 1] = "SimSpace";
    echo(" Creator::init  loading mission objects");
    %base = %this.insertItem(0, "Mission Objects");
    %i = 0;
    if (!(%i[%objGroup @ %i] $= "")) {
        %grp = %this.insertItem(%base, %i[%objGroup @ %i]);
        %groupTag = "%" @ %i[%objGroup @ %i] @ "_Item";
        %done = 0;
        %j = 0;
        if (!(%done)) {
            eval("%itemTag = " @ %groupTag @ %j @ ";");
            if ((%itemTag $= "")) {
                %done = 1;
            }
            %this.insertItem(%grp, %itemTag, "ObjectBuilderGui.build" @ %itemTag @ "();", %itemTag);
            %j = (1.0 + %j);
        }
        %i = (1.0 + %i);
        !(%done);
    }
    echo(" Creator::init  finished");
};
function createInterior(%name) {
    %obj = new InteriorInstance("") {
        position = 0 @ "0 0 0";
        rotation = "0 0 0";
        interiorFile = %name;
    };
    %obj.isNetCacheable = TEST_MISSIONGROUPINTEGRITY.getInitialNetCacheable(%obj);
    return %obj;
};
function WorldEditor::onAddSelected(%this, %obj) {
    EditorTree.addSelection(%obj);
};
function Creator::onSelect(%this) {
    Creator.clearSelection();
};
function Creator::OnInspect(%this, %obj) {
    if (!($missionRunning)) {
        return;
    }
    %objId = eval(%this.getItemValue(%obj));
    Creator.removeSelection(%obj);
    EditorTree.clearSelection();
    EWorldEditor.clearSelection();
    EWorldEditor.selectObject(%objId);
    EWorldEditor.dropSelection();
};
function ExpandSelectedInEditorTree() {
    %id = EditorTree.getSelectedItem();
    if ((-(1.0) != %id)) {
        EditorTree.expandAllChildren(%id);
    }
    echo("nothing selected");
};
function SelectRecursively(%itemId) {
    %obj = EditorTree.getItemValue(%itemId);
    if (isObject(%obj)) {
        EWorldEditor.selectObject(%obj);
    }
    %child = EditorTree.getChild(%itemId);
    if (%child) {
        SelectRecursively(%child);
    }
    %sibling = EditorTree.getNextSibling(%itemId);
    if (%sibling) {
        SelectRecursively(%sibling);
    }
};
function ExpandSelectedAndSelectInEditorTree() {
    %obj = EditorTree.getSelectedObject();
    %id = EditorTree.getSelectedItem();
    if ((-(1.0) != %id)) {
        EditorTree.expandAllChildren(%id);
        if (isObject(%obj)) {
            if (%obj.isClassSimGroup()) {
                RecurseSelectObjectsInGroup(%obj, "");
            }
        }
    }
    echo("nothing selected");
};
function FindSelectedInEditorTree() {
    if ((1.0 < EWorldEditor.getSelectionSize())) {
        echo("nothing selected");
        return;
    }
    %obj = EWorldEditor.getSelectedObject(0);
    if (isObject(%obj)) {
        EditorTree.buildVisibleTree(1);
        %item = EditorTree.findItemByObjectId(%obj.getId());
        if ((-(1.0) != %item)) {
            EditorTree.scrollVisible(%item);
            EditorTree.makeFirstResponder(1);
        }
        echo("unable to find item in EditorTree");
    }
    echo("nothing selected");
};
function Creator::Create(%this, %sel) {
    %obj = eval(%this.getItemValue(%sel));
    if ((-(1.0) == %obj)) {
        return;
    }
    %obj.isNetCacheable = TEST_MISSIONGROUPINTEGRITY.getInitialNetCacheable(%obj);
    $instantGroup.add(%obj);
    EWorldEditor.clearSelection();
    EWorldEditor.selectObject(%obj);
    EWorldEditor.dropSelection();
};
function TSStatic::Create(%shapeName) {
    if ((MissionInfo @ " " @ %obj.mode $= "InventoryDesigner")) {
    }
    if ((MissionInfo @ " " @ %obj.mode $= "PrivateSpaceDesign")) {
        MessageBoxOK("Warning", "You should use TSDynamic for inventory items and in private spaces instead of TSStatic," @ "\n" @ "I'll still make it for you, but you should change it to the TSDynamic!" @ "\n" @ "look under \"Dynamic Shapes\" for the same thing there. thanks!", "");
    }
    %obj = new TSStatic("") {
        shapeName = 0 @ %shapeName;
    };
    return %obj;
};
function TSStatic::Damage(%this) {
};
function TSDynamic::Create(%shapeName) {
    %obj = new TSDynamic("") {
        shapeName = 0 @ %shapeName;
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
    Texture_operation_menu.setText("Placement Operations");
    Texture_operation_menu.add("Place by Fractal", 1);
    Texture_operation_menu.add("Place by Height", 2);
    Texture_operation_menu.add("Place by Slope", 3);
    Texture_operation_menu.add("Place by Water Level", 4);
    $HeightfieldSrcRegister = (1.0 - Heightfield_operation.rowCount());
    TexturePreview.setValue(HeightfieldPreview.getValue());
    %script = Terrain.getTextureScript();
    if (!(%script $= "")) {
        texture::loadFromScript(%script);
    }
    if ((0.0 == Texture_material.rowCount())) {
        Texture_operation.clear();
        $nextTextureRegister = 1000;
    }
    %rowCount = Texture_material.rowCount();
    %row = 0;
    if ((%rowCount < %row)) {
        %data = Texture_material.getRowText(%row);
        %entry = getRecord(%data, 0);
        %reg = getField(%entry, 1);
        %reg[$dirtyTexture @ %reg] = 1;
        %opCount = getRecordCount(%data);
        %op = 2;
        if ((%opCount < %op)) {
            %entry = getRecord(%data, %op);
            %label = getField(%entry, 0);
            if (!(%label $= "Place by Fractal")) {
            }
            if (!(%label $= "Fractal Distortion")) {
                %reg = getField(%entry, 2);
                %reg[$dirtyTexture @ %reg] = 1;
            }
            %op = (1.0 + %op);
        }
        %row = (1.0 + %row);
        (%opCount < %op);
    }
    texture::previewMaterial();
};
function TerraformerTextureGui::refresh(%this) {
};
function Texture_material_menu::onSelect(%this, %id, %text) {
    %this.setText("Materials");
    texture::saveMaterial();
    texture::hideTab();
    $nextTextureRegister = (1.0 + $nextTextureRegister);
    %id = texture::addMaterial(%text @ "\t");
    if ((-(1.0) != %id)) {
        Texture_material.setSelectedById(%id);
        $nextTextureRegister = (1.0 + $nextTextureRegister);
        texture::addOperation("Fractal Distortion\ttab_DistortMask\t" @ "\t0\tdmask_interval\t20\tdmask_rough\t0\tdmask_seed\t" @ Terraformer.generateSeed() @ "\tdmask_filter\t0.00000 0.00000 0.13750 0.487500 0.86250 1.00000 1.00000");
    }
};
function texture::addMaterialTexture() {
    %root = filePath(Terrain, terrainFile);
    getLoadFilename("*/terrains/*.png\t*/terrains/*.jpg");
};
function addLoadedMaterial(%file) {
    texture::saveMaterial();
    texture::hideTab();
    %text = filePath(%file) @ "/" @ fileBase(%file);
    $nextTextureRegister = (1.0 + $nextTextureRegister);
    %id = texture::addMaterial(%text @ "\t");
    if ((-(1.0) != %id)) {
        Texture_material.setSelectedById(%id);
        $nextTextureRegister = (1.0 + $nextTextureRegister);
        texture::addOperation("Fractal Distortion\ttab_DistortMask\t" @ "\t0\tdmask_interval\t20\tdmask_rough\t0\tdmask_seed\t" @ Terraformer.generateSeed() @ "\tdmask_filter\t0.00000 0.00000 0.13750 0.487500 0.86250 1.00000 1.00000");
    }
    texture::save();
};
function Texture_material::onSelect(%this, %id, %text) {
    texture::saveMaterial();
    if (($selectedMaterial != %id)) {
        $selectedTextureOperation = -(1.0);
        Texture_operation.clear();
        texture::hideTab();
        texture::restoreMaterial(%id);
    }
    %matName = getField(%text, 0);
    paintMaterial = %matName @ ETerrainEditor;
    texture::previewMaterial(%id);
    $selectedMaterial = %id;
    $selectedTextureOperation = -(1.0);
    Texture_operation.clearSelection();
};
function Texture_operation_menu::onSelect(%this, %id, %text) {
    %this.setText("Placement Operations");
    %id = -(1.0);
    if ((-(1.0) == $selectedMaterial)) {
        return;
    }
    %dreg = getField(Texture_operation.getRowText(0), 2);
    if ((%text $= "Place by Fractal")) {
        $nextTextureRegister = (1.0 + $nextTextureRegister);
        %id = texture::addOperation("Place by Fractal\ttab_FractalMask\t" @ "\t" @ %dreg @ "\tfbmmask_interval\t16\tfbmmask_rough\t0.000\tfbmmask_seed\t" @ Terraformer.generateSeed() @ "\tfbmmask_filter\t0.000000 0.166667 0.333333 0.500000 0.666667 0.833333 1.000000\tfBmDistort\ttrue");
    }
    if ((%text $= "Place by Height")) {
        $nextTextureRegister = (1.0 + $nextTextureRegister);
        %id = texture::addOperation("Place by Height\ttab_HeightMask\t" @ "\t" @ %dreg @ "\ttextureHeightFilter\t0 0.2 0.4 0.6 0.8 1.0\theightDistort\ttrue");
    }
    if ((%text $= "Place by Slope")) {
        $nextTextureRegister = (1.0 + $nextTextureRegister);
        %id = texture::addOperation("Place by Slope\ttab_SlopeMask\t" @ "\t" @ %dreg @ "\ttextureSlopeFilter\t0 0.2 0.4 0.6 0.8 1.0\tslopeDistort\ttrue");
    }
    if ((%text $= "Place by Water Level")) {
        $nextTextureRegister = (1.0 + $nextTextureRegister);
        %id = texture::addOperation("Place by Water Level\ttab_WaterMask\t" @ "\t" @ %dreg @ "\twaterDistort\ttrue");
    }
    texture::hideTab();
    if ((-(1.0) != %id)) {
        Texture_operation.setSelectedById(%id);
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
    if ((-(1.0) == %id)) {
        return;
    }
    %row = Texture_material.getRowNumById(%id);
    Texture_material.removeRow(%row);
    %rowCount = (1.0 - Texture_material.rowCount());
    if ((%rowCount > %row)) {
        %row = %rowCount;
    }
    if (($selectedMaterial == %id)) {
        $selectedMaterial = -(1.0);
    }
    Texture_operation.clear();
    %id = Texture_material.getRowId(%row);
    Texture_material.setSelectedById(%id);
    texture::save();
};
function texture::deleteOperation(%id) {
    if ((%id $= "")) {
        %id = $selectedTextureOperation;
    }
    if ((-(1.0) == %id)) {
        return;
    }
    %row = Texture_operation.getRowNumById(%id);
    if ((0.0 == %row)) {
        return;
    }
    Texture_operation.removeRow(%row);
    %rowCount = (1.0 - Texture_operation.rowCount());
    if ((%rowCount > %row)) {
        %row = %rowCount;
    }
    if (($selectedTextureOperation == %id)) {
        $selectedTextureOperation = -(1.0);
    }
    %id = Texture_operation.getRowId(%row);
    Texture_operation.setSelectedById(%id);
    texture::save();
};
function texture::applyMaterials() {
    texture::saveMaterial();
    %count = Texture_material.rowCount();
    if ((0.0 > %count)) {
        %data = getRecord(Texture_material.getRowText(0), 0);
        %mat_list = getField(%data, 0);
        %reg_list = getField(%data, 1);
        texture::evalMaterial(Texture_material.getRowId(0));
        %i = 1;
        if ((%count < %i)) {
            texture::evalMaterial(Texture_material.getRowId(%i));
            %data = getRecord(Texture_material.getRowText(%i), 0);
            %mat_list = %mat_list @ " " @ getField(%data, 0);
            %reg_list = %reg_list @ " " @ getField(%data, 1);
            %i = (1.0 + %i);
        }
        Terraformer.setMaterials(%reg_list, %mat_list);
    }
};
function texture::previewMaterial(%id) {
    if ((%id $= "")) {
        %id = $selectedMaterial;
    }
    if ((-(1.0) == %id)) {
        return;
    }
    %data = Texture_material.getRowTextById(%id);
    %row = Texture_material.getRowNumById(%id);
    %reg = getField(getRecord(%data, 0), 1);
    texture::evalMaterial(%id);
    Terraformer.preview(TexturePreview, %reg);
};
function texture::evalMaterial(%id) {
    if ((%id $= "")) {
        %id = $selectedMaterial;
    }
    if ((-(1.0) == %id)) {
        return;
    }
    %data = Texture_material.getRowTextById(%id);
    %reg = getField(getRecord(%data, 0), 1);
    %opCount = getRecordCount(%data);
    if ((2.0 >= %opCount)) {
        %entry = getRecord(%data, 1);
        texture::evalOperationData(%entry, 1);
        %op = 2;
        if ((%opCount < %op)) {
            %entry = getRecord(%data, %op);
            %reg_list = %reg_list @ getField(%entry, 2) @ " ";
            texture::evalOperationData(%entry, %op);
            %op = (1.0 + %op);
        }
        Terraformer.mergeMasks(%reg_list, %reg);
    }
    texture::save();
};
function texture::evalOperation(%id) {
    if ((%id $= "")) {
        %id = $selectedTextureOperation;
    }
    if ((-(1.0) == %id)) {
        return;
    }
    %data = Texture_operation.getRowTextById(%id);
    %row = Texture_operation.getRowNumById(%id);
    if ((0.0 != %row)) {
        texture::evalOperation(Texture_operation.getRowId(0));
    }
    texture::evalOperationData(%data, %row);
    texture::save();
};
function texture::evalOperationData(%data, %row) {
    %label = getField(%data, 0);
    %reg = getField(%data, 2);
    %dreg = getField(%data, 3);
    %id = Texture_material.getRowId(%row);
    if ((0.0 == %reg[$dirtyTexture @ %reg])) {
        return;
    }
    if ((%label $= "Fractal Distortion")) {
        Terraformer.maskFBm(%reg, getField(%data, 5), getField(%data, 7), getField(%data, 9), getField(%data, 11), 0, 0);
    }
    if ((%label $= "Place by Fractal")) {
        Terraformer.maskFBm(%reg, getField(%data, 5), getField(%data, 7), getField(%data, 9), getField(%data, 11), getField(%data, 13), %dreg);
    }
    if ((%label $= "Place by Height")) {
        Terraformer.maskHeight($HeightfieldSrcRegister, %reg, getField(%data, 5), getField(%data, 7), %dreg);
    }
    if ((%label $= "Place by Slope")) {
        Terraformer.maskSlope($HeightfieldSrcRegister, %reg, getField(%data, 5), getField(%data, 7), %dreg);
    }
    if ((%label $= "Place by Water Level")) {
        Terraformer.maskWater($HeightfieldSrcRegister, %reg, getField(%data, 5), %dreg);
    }
    %reg[$dirtyTexture @ %reg] = 0;
};
function texture::previewOperation(%id) {
    if ((%id $= "")) {
        %id = $selectedTextureOperation;
    }
    if ((-(1.0) == %id)) {
        return;
    }
    %row = Texture_operation.getRowNumById(%id);
    %data = Texture_operation.getRowText(%row);
    %reg = getField(%data, 2);
    texture::evalOperation(%id);
    Terraformer.preview(TexturePreview, %reg);
};
function texture::restoreMaterial(%id) {
    if ((-(1.0) == %id)) {
        return;
    }
    %data = Texture_material.getRowTextById(%id);
    Texture_operation.clear();
    %recordCount = getRecordCount(%data);
    %record = 1;
    if ((%recordCount < %record)) {
        %entry = getRecord(%data, %record);
        $nextTextureId = (1.0 + $nextTextureId);
        Texture_operation.addRow(%entry);
        %record = (1.0 + %record);
    }
};
function texture::saveMaterial() {
    %id = $selectedMaterial;
    if ((-(1.0) == %id)) {
        return;
    }
    texture::saveOperation();
    %data = Texture_material.getRowTextById(%id);
    %newData = getRecord(%data, 0);
    %rowCount = Texture_operation.rowCount();
    %row = 0;
    if ((%rowCount < %row)) {
        %newData = %newData @ "\n" @ Texture_operation.getRowText(%row);
        %row = (1.0 + %row);
    }
    Texture_material.setRowById(%id, %newData);
    texture::save();
};
function texture::restoreOperation(%id) {
    if ((-(1.0) == %id)) {
        return;
    }
    %data = Texture_operation.getRowTextById(%id);
    %fieldCount = getFieldCount(%data);
    %field = 4;
    if ((%fieldCount < %field)) {
        %obj = getField(%data, %field);
        %obj.setValue(getField(%data, (1.0 + %field)));
        %field = (2.0 + %field);
    }
    texture::save();
};
function texture::saveOperation() {
    %id = $selectedTextureOperation;
    if ((-(1.0) == %id)) {
        return;
    }
    %data = Texture_operation.getRowTextById(%id);
    %newData = getField(%data, 0) @ "\t" @ getField(%data, 1) @ "\t" @ getField(%data, 2) @ "\t" @ getField(%data, 3);
    %fieldCount = getFieldCount(%data);
    %field = 4;
    if ((%fieldCount < %field)) {
        %obj = getField(%data, %field);
        %newData = %newData @ "\t" @ %obj @ "\t" @ %obj.getValue();
        %field = (2.0 + %field);
    }
    %dirty = !((%fieldCount < %field) @ " " @ %data $= %newData);
    %reg = getField(%data, 2);
    %reg[$dirtyTexture @ %reg] = %dirty;
    Texture_operation.setRowById(%id, %newData);
    if ((1.0 == %dirty)) {
        %data = Texture_material.getRowTextById($selectedMaterial);
        %reg = getField(getRecord(%data, 0), 1);
        %reg[$dirtyTexture @ %reg] = 1;
    }
    %row = Texture_material.getRowNumById(%id);
    if ((0.0 == %row)) {
        %rowCount = Texture_operation.rowCount();
        %r = 1;
        if ((%rowCount < %r)) {
            %data = Texture_operation.getRowText(%r);
            %r = (1.0 + %r);
        }
    }
    texture::save();
};
function texture::addMaterial(%entry) {
    $nextTextureId = (1.0 + $nextTextureId);
    %id = ;
    Texture_material.addRow(%id, %entry);
    %reg = getField(%entry, 1);
    %reg[$dirtyTexture @ %reg] = 1;
    texture::save();
    return %id;
};
function texture::addOperation(%entry) {
    $nextTextureId = (1.0 + $nextTextureId);
    %id = ;
    Texture_operation.addRow(%id, %entry);
    %reg = getField(%entry, 2);
    %reg[$dirtyTexture @ %reg] = 1;
    texture::save();
    return %id;
};
function texture::save() {
    %script = "";
    %rowCount = Texture_material.rowCount();
    %row = 0;
    if ((%rowCount < %row)) {
        if ((0.0 != %row)) {
            %script = %script @ "\n";
        }
        %data = expandEscape(Texture_material.getRowText(%row));
        %script = %script @ %data;
        %row = (1.0 + %row);
    }
    Terrain.setTextureScript(%script);
    isDirty = 1 @ ETerrainEditor;
    (%rowCount < %row);
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
    if (!(%rec $= "")) {
        texture::addMaterial(collapseEscape(%rec));
        %i = (1.0 + %i);
        %rec = getRecord(%script, );
    }
    $nextTextureRegister = 1000;
    !(%rec $= "");
    %rowCount = Texture_material.rowCount();
    %row = 0;
    if ((%rowCount < %row)) {
        $nextTextureRegister[$dirtyTexture @ $nextTextureRegister] = 1;
        %data = Texture_material.getRowText(%row);
        %rec = getRecord(%data, 0);
        %rec = setField(%rec, 1, $nextTextureRegister);
        %data = setRecord(%data, 0, %rec);
        $nextTextureRegister = (1.0 + $nextTextureRegister);
        %opCount = getRecordCount(%data);
        %op = 1;
        if ((%opCount < %op)) {
            if ((1.0 == %op)) {
                %frac_reg = $nextTextureRegister;
            }
            $nextTextureRegister[$dirtyTexture @ $nextTextureRegister] = 1;
            %rec = getRecord(%data, %op);
            %rec = setField(%rec, 2, $nextTextureRegister);
            %rec = setField(%rec, 3, %frac_reg);
            %data = setRecord(%data, %op, %rec);
            $nextTextureRegister = (1.0 + $nextTextureRegister);
            %op = (1.0 + %op);
        }
        %id = Texture_material.getRowId(%row);
        (%opCount < %op);
        Texture_material.setRowById(%id, %data);
        %row = (1.0 + %row);
    }
    $selectedMaterial = -(1.0);
    (%rowCount < %row);
    Texture_material.setSelectedById(Texture_material.getRowId(0));
};
function texture::doLoadTexture(%name) {
    %newTerr = new TerrainBlock("") {
        position = 0 @ "0 0 0";
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
    tab_DistortMask.setVisible(0);
    tab_FractalMask.setVisible(0);
    tab_HeightMask.setVisible(0);
    tab_SlopeMask.setVisible(0);
    tab_WaterMask.setVisible(0);
};
function texture::showTab(%id) {
    texture::hideTab();
    %data = Texture_operation.getRowTextById(%id);
    %tab = getField(%data, 1);
    %tab.setVisible(1);
};
$TerraformerHeightfieldDir = "common/editor/heightScripts";
function tab_Blend::reset(%this) {
    blend_option.clear();
    blend_option.add("Add", 0);
    blend_option.add("Subtract", 1);
    blend_option.add("Max", 2);
    blend_option.add("Min", 3);
    blend_option.add("Multiply", 4);
};
function tab_fBm::reset(%this) {
    fbm_detail.clear();
    fbm_detail.add("Very Low", 0);
    fbm_detail.add("Low", 1);
    fbm_detail.add("Normal", 2);
    fbm_detail.add("High", 3);
    fbm_detail.add("Very High", 4);
};
function tab_RMF::reset(%this) {
    rmf_detail.clear();
    rmf_detail.add("Very Low", 0);
    rmf_detail.add("Low", 1);
    rmf_detail.add("Normal", 2);
    rmf_detail.add("High", 3);
    rmf_detail.add("Very High", 4);
};
function tab_terrainFile::reset(%this) {
    terrainFile_textList.clear();
    %filespec = $TerraformerHeightfieldDir @ "/*.ter";
    %file = findFirstFile(%filespec);
    if (!(%file $= "")) {
        %i = (1.0 + %i);
        terrainFile_textList.addRow(fileBase(%file) @ fileExt(%file));
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
    if (!(%script $= "")) {
        Heightfield::loadFromScript(%script, 1);
    }
    if ((0.0 == Heightfield_operation.rowCount())) {
        Heightfield_operation.clear();
        %id1 = Heightfield::add("General\tTab_general\tgeneral_min_height\t50\tgeneral_scale\t300\tgeneral_water\t0.000\tgeneral_centerx\t0\tgeneral_centery\t0");
        Heightfield_operation.setSelectedById(%id1);
    }
    Heightfield::resetTabs();
    Heightfield::preview();
};
function Heightfield_options::onSelect(%this, %unused, %text) {
    Heightfield_options.setText("Operation");
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
    if ((1.0 >= Heightfield_operation.rowCount())) {
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
    if ((2.0 >= Heightfield_operation.rowCount())) {
        if (("Blend" $= %text)) {
            %id = Heightfield::add("Blend\ttab_Blend\tblend_factor\t0.500\tblend_srcB\t" @ (2.0 - %rowCount) @ "\tblend_option\tadd");
        }
    }
    if ((-(1.0) != %id)) {
        Heightfield_operation.setSelectedById(%id);
    }
};
function Heightfield::eval(%id) {
    if ((-(1.0) == %id)) {
        return;
    }
    %data = restWords(Heightfield_operation.getRowTextById(%id));
    %label = getField(%data, 0);
    %row = Heightfield_operation.getRowNumById(%id);
    echo("Heightfield::eval:" @ %row @ "  " @ %label);
    if ((%label $= "General")) {
        if ((Terrain > squareSize)) {
            %size = squareSize;
            Terrain;
        }
        %size = 8;
        0.0;
        Terraformer.setTerrainInfo(256, %size, getField(%data, 3), getField(%data, 5), getField(%data, 7));
        Terraformer.setShift(getField(%data, 9), getField(%data, 11));
        Terraformer.terrainData(%row);
    }
    if ((%label $= "Terrain File")) {
        Terraformer.terrainFile(%row, getField(%data, 3));
    }
    if ((%label $= "fBm Fractal")) {
        Terraformer.fBm(%row, getField(%data, 3), getField(%data, 5), getField(%data, 7), getField(%data, 9));
    }
    if ((%label $= "Sinus")) {
        Terraformer.sinus(%row, getField(%data, 3), getField(%data, 5));
    }
    if ((%label $= "Rigid MultiFractal")) {
        Terraformer.rigidMultiFractal(%row, getField(%data, 3), getField(%data, 5), getField(%data, 7), getField(%data, 9));
    }
    if ((%label $= "Canyon Fractal")) {
        Terraformer.canyon(%row, getField(%data, 3), getField(%data, 5), getField(%data, 7));
    }
    if ((%label $= "Smoothing")) {
        Terraformer.smooth((1.0 - %row), %row, getField(%data, 3), getField(%data, 5));
    }
    if ((%label $= "Smooth Water")) {
        Terraformer.smoothWater((1.0 - %row), %row, getField(%data, 3), getField(%data, 5));
    }
    if ((%label $= "Smooth Ridges/Valleys")) {
        Terraformer.smoothRidges((1.0 - %row), %row, getField(%data, 3), getField(%data, 5));
    }
    if ((%label $= "Filter")) {
        Terraformer.filter((1.0 - %row), %row, getField(%data, 3));
    }
    if ((%label $= "Turbulence")) {
        Terraformer.turbulence((1.0 - %row), %row, getField(%data, 3), getField(%data, 5));
    }
    if ((%label $= "Thermal Erosion")) {
        Terraformer.erodeThermal((1.0 - %row), %row, getField(%data, 3), getField(%data, 5), getField(%data, 7));
    }
    if ((%label $= "Hydraulic Erosion")) {
        Terraformer.erodeHydraulic((1.0 - %row), %row, getField(%data, 3), getField(%data, 5));
    }
    if ((%label $= "Bitmap")) {
        Terraformer.loadGreyscale(%row, getField(%data, 3));
    }
    if ((%label $= "Blend")) {
        %rowCount = Heightfield_operation.rowCount();
        if ((2.0 > %rowCount)) {
            %a = (1.0 - Heightfield_operation.getRowNumById(%id));
            %b = getField(%data, 5);
            echo("Blend: " @ %data);
            echo("Blend: " @ getField(%data, 3) @ "  " @ getField(%data, 7));
            if ((%rowCount < %a)) {
            }
            if ((0.0 > %a)) {
            }
            if ((%rowCount < %b)) {
            }
            if ((0.0 > %b)) {
                Terraformer.blend(%a, %b, %row, getField(%data, 3), getField(%data, 7));
            }
            echo("Heightfield Editor: Blend parameters out of range.");
        }
    }
};
function Heightfield::add(%entry) {
    Heightfield::saveTab();
    Heightfield::hideTab();
    $NextOperationId = (1.0 + $NextOperationId);
    %id = ;
    if ((-(1.0) != $SelectedOperation)) {
        %row = (1.0 + Heightfield_operation.getRowNumById($SelectedOperation));
        %entry = %row @ " " @ %entry;
        Heightfield_operation.addRow(%id, %entry, %row);
        %i = (1.0 + %row);
        if ((Heightfield_operation.rowCount() < %i)) {
            %id = Heightfield_operation.getRowId(%i);
            %text = Heightfield_operation.getRowTextById(%id);
            %text = setWord(%text, 0, %i);
            Heightfield_operation.setRowById(%id, %text);
            %i = (1.0 + %i);
        }
    }
    %entry = Heightfield_operation.rowCount() @ " " @ %entry;
    (Heightfield_operation.rowCount() < %i);
    Heightfield_operation.addRow(%id, %entry);
    %row = Heightfield_operation.getRowNumById(%id);
    if (($HeightfieldDirtyRow <= %row)) {
        $HeightfieldDirtyRow = %row;
    }
    Heightfield::save();
    return %id;
};
function Heightfield::onDelete(%id) {
    if ((%id $= "")) {
        %id = $SelectedOperation;
    }
    %row = Heightfield_operation.getRowNumById(%id);
    if ((0.0 == %row)) {
        return;
    }
    Heightfield_operation.removeRow(%row);
    %i = %row;
    if ((Heightfield_operation.rowCount() < %i)) {
        %id2 = Heightfield_operation.getRowId(%i);
        %text = Heightfield_operation.getRowTextById(%id2);
        %text = setWord(%text, 0, %i);
        Heightfield_operation.setRowById(%id2, %text);
        %i = (1.0 + %i);
    }
    if ((%row >= $HeightfieldDirtyRow)) {
        $HeightfieldDirtyRow = %row;
        (Heightfield_operation.rowCount() < %i);
    }
    %rowCount = (1.0 - Heightfield_operation.rowCount());
    if ((%rowCount > %row)) {
        %row = %rowCount;
    }
    if (($SelectedOperation == %id)) {
        $SelectedOperation = -(1.0);
    }
    %id = Heightfield_operation.getRowId(%row);
    Heightfield_operation.setSelectedById(%id);
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
    if ((-(1.0) == %id)) {
        return;
    }
    Heightfield::hideTab();
    %data = restWords(Heightfield_operation.getRowTextById(%id));
    %fieldCount = getFieldCount(%data);
    %field = 2;
    if ((%fieldCount < %field)) {
        %obj = getField(%data, %field);
        %obj.setValue(getField(%data, (1.0 + %field)));
        %field = (2.0 + %field);
    }
    Heightfield::save();
};
function Heightfield::saveTab() {
    if ((-(1.0) == $SelectedOperation)) {
        return;
    }
    %data = Heightfield_operation.getRowTextById($SelectedOperation);
    %rowNum = getWord(%data, 0);
    %data = restWords(%data);
    %newData = getField(%data, 0) @ "\t" @ getField(%data, 1);
    %fieldCount = getFieldCount(%data);
    %field = 2;
    if ((%fieldCount < %field)) {
        %obj = getField(%data, %field);
        %newData = %newData @ "\t" @ %obj @ "\t" @ %obj.getValue();
        %field = (2.0 + %field);
    }
    if (!((%fieldCount < %field) @ " " @ %data $= %newData)) {
        %row = Heightfield_operation.getRowNumById($SelectedOperation);
        if (($HeightfieldDirtyRow <= %row)) {
        }
        if ((0.0 > %row)) {
            $HeightfieldDirtyRow = %row;
        }
    }
    Heightfield_operation.setRowById($SelectedOperation, %rowNum @ " " @ %newData);
    Heightfield::save();
};
function Heightfield::preview(%id) {
    %rowCount = Heightfield_operation.rowCount();
    if ((%id $= "")) {
        %id = Heightfield_operation.getRowId((1.0 - %rowCount));
    }
    %row = Heightfield_operation.getRowNumById(%id);
    Heightfield::refresh(%row);
    Terraformer.previewScaled(HeightfieldPreview, %row);
};
function Heightfield::refresh(%last) {
    if ((%last $= "")) {
        %last = (1.0 - Heightfield_operation.rowCount());
    }
    Heightfield::eval(Heightfield_operation.getRowId(0));
    if ((%last <= $HeightfieldDirtyRow)) {
        %id = Heightfield_operation.getRowId($HeightfieldDirtyRow);
        Heightfield::eval(%id);
        $HeightfieldDirtyRow = (1.0 + $HeightfieldDirtyRow);
    }
    Heightfield::save();
};
function Heightfield::apply(%id) {
    %rowCount = Heightfield_operation.rowCount();
    if ((1.0 < %rowCount)) {
        return;
    }
    if ((%id $= "")) {
        %id = Heightfield_operation.getRowId((1.0 - %rowCount));
    }
    %row = Heightfield_operation.getRowNumById(%id);
    HeightfieldPreview.setRoot();
    Heightfield::refresh(%row);
    Terraformer.setTerrain(%row);
    Terraformer.setCameraPosition(0, 0, 0);
    isDirty = 1 @ ETerrainEditor;
};
$TerraformerSaveRegister = 0;
function Heightfield::saveBitmap(%name) {
    if ((%name $= "")) {
        getSaveFilename("*.png", "Heightfield::doSaveBitmap", $TerraformerHeightfieldDir @ "/" @ fileBase($Client::MissionFile) @ ".png");
    }
    Heightfield::doSaveBitmap(%name);
};
function Heightfield::doSaveBitmap(%name) {
    Terraformer.saveGreyscale($TerraformerSaveRegister, %name);
};
function Heightfield::save() {
    %script = "";
    %rowCount = Heightfield_operation.rowCount();
    %row = 0;
    if ((%rowCount < %row)) {
        if ((0.0 != %row)) {
            %script = %script @ "\n";
        }
        %data = restWords(Heightfield_operation.getRowText(%row));
        %script = %script @ expandEscape(%data);
        %row = (1.0 + %row);
    }
    Terrain.setHeightfieldScript(%script);
    isDirty = 1 @ ETerrainEditor;
    (%rowCount < %row);
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
    if (!(%rec $= "")) {
        Heightfield::add(collapseEscape(%rec));
        %i = (1.0 + %i);
        %rec = getRecord(%script, );
    }
    if ((0.0 == Heightfield_operation.rowCount())) {
        Heightfield_operation.clear();
        Heightfield::add("General\tTab_general\tgeneral_min_height\t50\tgeneral_scale\t300\tgeneral_water\t0.000\tgeneral_centerx\t0\tgeneral_centery\t0");
    }
    %data = restWords(Heightfield_operation.getRowText(0));
    !(%rec $= "");
    %x = getField(%data, 7);
    %y = getField(%data, 9);
    HeightfieldPreview.setOrigin(%x, %y);
    Heightfield_operation.setSelectedById(Heightfield_operation.getRowId(0));
    if (!(%leaveCamera)) {
        Terraformer.setCameraPosition(%x, %y);
    }
};
function strip(%stripStr, %strToStrip) {
    %len = strlen(%stripStr);
    if ((0.0 == strcmp(getSubStr(%strToStrip, 0, %len), %stripStr))) {
        return getSubStr(%strToStrip, %len, 100000);
    }
    return %strToStrip;
};
function Heightfield::doLoadHeightfield(%name) {
    %newTerr = new TerrainBlock("") {
        position = 0 @ "0 0 -1000";
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
    bitmap_name.setValue(%name);
    Heightfield::saveTab();
    Heightfield::preview($SelectedOperation);
};
function Heightfield::hideTab() {
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
};
function Heightfield::showTab(%id) {
    Heightfield::hideTab();
    %data = restWords(Heightfield_operation.getRowTextById(%id));
    %tab = getField(%data, 1);
    echo("Tab data: " @ %data @ " tab: " @ %tab);
    %tab.setVisible(1);
};
function Heightfield::center() {
    %camera = Terraformer.getCameraPosition();
    %x = getWord(%camera, 0);
    %y = getWord(%camera, 1);
    HeightfieldPreview.setOrigin(%x, %y);
    %origin = HeightfieldPreview.getOrigin();
    %x = getWord(%origin, 0);
    %y = getWord(%origin, 1);
    %root = HeightfieldPreview.getRoot();
    %x = (getWord(%root, 0) + %x);
    %y = (getWord(%root, 1) + %y);
    general_centerx.setValue(%x);
    general_centery.setValue(%y);
    Heightfield::saveTab();
};
function ExportHeightfield::onAction() {
    error("Time to export the heightfield...");
    if ((-(1.0) != Heightfield_operation.getSelectedId())) {
        $TerraformerSaveRegister = getWord(Heightfield_operation.getValue(), 0);
        Heightfield::saveBitmap("");
    }
};
function TerrainEditor::onGuiUpdate(%this, %text) {
    %mouseBrushInfo = " (Mouse Brush) #: " @ getWord(%text, 0) @ "  avg: " @ getWord(%text, 1);
    %selectionInfo = " (Selection) #: " @ getWord(%text, 2) @ "  avg: " @ getWord(%text, 3);
    TEMouseBrushInfo.setValue(%mouseBrushInfo);
    TEMouseBrushInfo1.setValue(%mouseBrushInfo);
    TESelectionInfo.setValue(%selectionInfo);
    TESelectionInfo1.setValue(%selectionInfo);
};
function TerrainEditor::offsetBrush(%this, %x, %y) {
    %curPos = %this.getBrushPos();
    %this.setBrushPos((%x + getWord(%curPos, 0)), (%y + getWord(%curPos, 1)));
};
function TerrainEditor::swapInLoneMaterial(%this, %name) {
    if ((%this.baseMaterialsSwapped $= "true")) {
        %this.baseMaterialsSwapped = "false";
        tEditor.popBaseMaterialInfo();
    }
    %this.baseMaterialsSwapped = "true";
    %this.pushBaseMaterialInfo();
    %this.setLoneBaseMaterial(%name);
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
        %pos = %this.position;
        Terrain;
        %squareSize = %this.squareSize;
        Terrain;
        %visibleDistance = %this.visibleDistance;
        Terrain;
        Terrain.delete();
    }
    new TerrainBlock(Terrain) {
        position = %pos;
        terrainFile = %name;
        squareSize = %squareSize;
        visibleDistance = %visibleDistance;
    };
    Terrain.attachTerrain(ETerrainEditor);
};
function TerrainEditorSettingsGui::onWake(%this) {
    TESoftSelectFilter.setValue(ETerrainEditor, softSelectFilter);
};
function TerrainEditorSettingsGui::onSleep(%this) {
    softSelectFilter = Terrain.getValue(TESoftSelectFilter) @ ETerrainEditor;
};
function TESettingsApplyButton::onAction(%this) {
    softSelectFilter = TESoftSelectFilter.getValue() @ ETerrainEditor;
    ETerrainEditor.resetSelWeights(1);
    ETerrainEditor.processAction("softSelect");
};
function getPrefSetting(%pref, %default) {
    if ((%pref $= "")) {
        return %default;
    }
    return %pref;
};
function onNeedRelight() {
    if ((RelightMessage == visible)) {
        visible = 1 @ RelightMessage;
        0.0;
    }
};
function Editor::open(%this) {
    if ((GuiEditorGui.getId() == Canvas.getContent())) {
        return;
    }
    %this.prevContent = Canvas.getContent();
    Canvas.setContent(EditorGui);
};
function Editor::close(%this) {
    if ((-(1.0) == %this.prevContent)) {
    }
    if ((%this.prevContent $= "")) {
        %this.prevContent = "PlayGui";
    }
    Canvas.setContent(%this.prevContent);
    MessageHud.close();
};
function EWorldEditor::updateGeneralInfo(%this, %optObj) {
    %numSelected = %this.getSelectionSize();
    %color = "<color:886644>";
    if ((0.0 == %numSelected)) {
        if ((%optObj $= "")) {
            WorldEditorGeneralInfoMLText.setText(%color @ "(nothing selected)");
            return;
        }
        %obj = %optObj;
    }
    if ((1.0 > %numSelected)) {
        WorldEditorGeneralInfoMLText.setText(%color @ "(multi)");
        return;
    }
    %obj = %this.getSelectedObject(0);
    %clientID = -(1.0);
    %serverID = ;
    %clientValid = 0;
    %serverValid = ;
    %client = ClientDict.get($Player::Name);
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
            %ghostID = %client.getGhostID(%obj);
            if ((0.0 <= %ghostID)) {
                %clientID = "no ghost.";
            }
            %clientID = ServerConnection.resolveGhostID(%ghostID);
            if ((0.0 <= %clientID)) {
                %clientID = "no ghost (server has ghostID, tho)";
            }
            %clientValid = 1;
        }
        if (%obj.isClientObject()) {
            error(getScopeName() @ "-> unexpected: worldeditor has selected a client-side object! handling.");
            %clientID = %obj.getId();
            %clientValid = 1;
            %ghostID = ServerConnection.getGhostID(%clientID);
            if ((0.0 <= %ghostID)) {
                %serverID = "client-side only.";
            }
            if (!(isObject(%client))) {
                %serverID = "(no client object for player)";
            }
            %serverID = %client.ResolveGhost(%ghostID).getId();
            if ((0.0 <= %serverID)) {
                %serverID = "no ghost (client has ghost ID, tho)";
            }
            %serverValid = 1;
        }
        error(getScopeName() @ "-> net object which returns false on both isServer/ClientObject(), returning!");
        WorldEditorGeneralInfoMLText.setText(%color @ "(error see log!)");
    }
    %serverID = %obj;
    %serverValid = 1;
    %clientID = "Not a net object! (assumed serverside)";
    if (%serverValid) {
    }
    %serverText = %serverID;
    %serverID.getDebugString();
    if (%clientValid) {
    }
    %clientText = %clientID;
    %clientID.getDebugString();
    %text = %color @ "<linkcolor:775533><linkcolorhl:ddff00>";
    if (%serverValid) {
    }
    %text = %serverText @ -(1.0) @ ">" @ %serverText @ "</a>\n";
    %text @ "<just:left>" @ "Server:<a:gamelink COPYTOCLIP ";
    if (%clientValid) {
    }
    %text = %clientText @ -(1.0) @ ">" @ %clientText @ "</a>";
    %text @ "Client:  <a:gamelink COPYTOCLIP ";
    WorldEditorGeneralInfoMLText.setText(%text);
};
function WorldEditorGeneralInfoMLText::onUrl(%this, %url) {
    %cmd = getWord(%url, 1);
    %restWords = getWords(%url, 2, 10000);
    if ((%cmd $= "COPYTOCLIP")) {
        setClipboard(%restWords);
    }
};
