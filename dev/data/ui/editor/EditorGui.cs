$AIEdit = 0;
function EStatusHudActivator::onMouseEnter(%this) {
    if (!(isShowing())) {
        updateStatus();
    }
};
function EWorldEditor::onWake(%this) {
    initialUpdateStatus();
    fxEts::updateExposureFilter();
};
function EWorldEditor::onCanvasResize(%this) {
    if (isObject()) {
        update();
    }
};
function toggleStatusHud() {
    if (isShowing()) {
        hide();
    }
    show();
    1.keepOpen();
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
    100.schedule();
};
function EStatusHud::initialUpdateStatus(%this) {
    1000.schedule();
};
function EStatusHud::realUpdateStatus(%this) {
    %c2 = "\x06";
    %selected = %c2 @ EWorldEditor @ getSelectionSize() @ "\x02\x01 world objects selected";
    %seltype = EWorldEditor @ %this.GetSelectTypeDisplayText(selectType);
    "\x02\x01type: " @ %c2;
    %addto = "\x02\x01addgroup: " @ %c2 @ $instantGroup @ ":" @ $instantGroup.getName();
    %viewerTrans = getControlObject().getTransform();
    LocalClientConnection;
    %camera = "\x02\x01cam pos: " @ %c2 @ "( " @ getWord(%viewerTrans, 0) @ " , " @ getWord(%viewerTrans, 1) @ " , " @ getWord(%viewerTrans, 2) @ " )";
    %scale = EWorldEditor @ mouseMoveScale;
    "\x02\x01move scale: " @ %c2;
    %grid = EWorldEditor @ snapToGrid;
    "\x02\x01grid size: " @ %c2 @ EWorldEditor @ gridSize @ "\x02\x01,  grid snap: " @ %c2;
    charWidth = mMax(strlen(%selected), strlen(%addto)) @ %this;
    charWidth = this @ mMax(strlen(charWidth), strlen(%seltype)) @ %this;
    charWidth = this @ mMax(strlen(charWidth), strlen(%camera)) @ %this;
    charWidth = this @ mMax(strlen(charWidth), strlen(%scale)) @ %this;
    charWidth = this @ mMax(strlen(charWidth), strlen(%grid)) @ %this;
    %text = %selected @ "\n" @ %seltype @ "\n" @ %addto @ "\n" @ %camera @ "\n" @ %scale @ "\n" @ %grid;
    %this.setStatusText(%text);
};
function EStatusHud::setStatusText(%this, %text) {
    text = %text @ %this;
    charWidth = strlen(%text) @ %this;
    show();
    hideSchedule = tryHide @ %this.schedule(5000, %this) @ %this;
    EStatusHud;
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
        "MusicMLTextProfileSmall".setProfile();
    }
    if ((640.0 <= %resWidth)) {
        %widthMultiplier = 8.1;
        EStatusText;
        %heightOffset = (4.0 + %heightOffset);
        %heightDelta = 2;
        "MusicMLTextProfileMedium".setProfile();
    }
    %widthMultiplier = 9.0;
    EStatusText;
    %heightDelta = 0;
    "MusicMLTextProfile".setProfile();
    %content = "";
    EStatusText;
    if (!(%this SPC text $= "")) {
        %content = %this @ text @ "\n";
    }
    %content.setText();
    %height = getWord(extent, 1);
    %this;
    %targetWidth = mCeil((mMax(charWidth, 20) * %widthMultiplier));
    %this;
    %this.setTrgExtent(%targetWidth, %height);
    0.resize(0, %targetWidth, %height);
    %this.updatePosition();
};
function EStatusHud::updatePosition(%this) {
    %trgX = getWord(%this.getTrgPosition(), 0);
    %trgY = ($ButtonBarVar::VerticalAdjustment + (getWord(%this.getExtent(), 1) + (ButtonBar - getWord(getTrgPosition(), 1))));
    12.0;
    %this.setTrgPosition(%trgX, %trgY);
};
function EStatusHud::show(%this) {
    if (hideSchedule) {
        cancel(hideSchedule);
    }
    %trgY = getWord(%this.getTrgPosition(), 1);
    %this;
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
    return (%this >= getWord(position, 0));
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
        %posX = getWord(position, 0);
        %this;
        %posY = getWord(position, 1);
        %this;
        %this.setTrgPosition(%posX, %posY);
    }
};
$sgEditorItemNames::sgMenu = "Synapse Gaming Tools";
$sgEditorItemNames::sgMenu[$sgEditorItemNames::sgMenuItem @ 0] = "Lighting Pack Light Editor";
function EditorGui::getPrefs() {
    dropType = getPrefSetting($Pref::WorldEditor::dropType, "atCamera") @ EWorldEditor;
    planarMovement = getPrefSetting($pref::WorldEditor::planarMovement, 1) @ EWorldEditor;
    undoLimit = getPrefSetting($pref::WorldEditor::undoLimit, 40) @ EWorldEditor;
    dropType = getPrefSetting($Pref::WorldEditor::dropType, "screenCenter") @ EWorldEditor;
    projectDistance = getPrefSetting($pref::WorldEditor::projectDistance, 2000) @ EWorldEditor;
    boundingBoxCollision = getPrefSetting($pref::WorldEditor::boundingBoxCollision, 1) @ EWorldEditor;
    renderPlane = getPrefSetting($pref::WorldEditor::renderPlane, 1) @ EWorldEditor;
    renderPlaneHashes = getPrefSetting($pref::WorldEditor::renderPlaneHashes, 1) @ EWorldEditor;
    gridColor = getPrefSetting($pref::WorldEditor::gridColor, "255 255 255 20") @ EWorldEditor;
    planeDim = getPrefSetting($pref::WorldEditor::planeDim, 500) @ EWorldEditor;
    gridSize = getPrefSetting($pref::WorldEditor::gridSize, "10 10 10") @ EWorldEditor;
    renderPopupBackground = getPrefSetting($pref::WorldEditor::renderPopupBackground, 1) @ EWorldEditor;
    popupBackgroundColor = getPrefSetting($pref::WorldEditor::popupBackgroundColor, "100 100 100") @ EWorldEditor;
    popupTextColor = getPrefSetting($pref::WorldEditor::popupTextColor, "255 255 0") @ EWorldEditor;
    selectHandle = getPrefSetting($pref::WorldEditor::selectHandle, "gui/Editor_SelectHandle.png") @ EWorldEditor;
    defaultHandle = getPrefSetting($pref::WorldEditor::defaultHandle, "gui/Editor_DefaultHandle.png") @ EWorldEditor;
    lockedHandle = getPrefSetting($pref::WorldEditor::lockedHandle, "gui/Editor_LockedHandle.png") @ EWorldEditor;
    objectTextColor = getPrefSetting($pref::WorldEditor::objectTextColor, "255 255 255") @ EWorldEditor;
    objectsUseBoxCenter = getPrefSetting($pref::WorldEditor::objectsUseBoxCenter, 1) @ EWorldEditor;
    axisGizmoMaxScreenLen = getPrefSetting($pref::WorldEditor::axisGizmoMaxScreenLen, 200) @ EWorldEditor;
    axisGizmoActive = getPrefSetting($pref::WorldEditor::axisGizmoActive, 1) @ EWorldEditor;
    mouseMoveScale = getPrefSetting($pref::WorldEditor::mouseMoveScale, 0.01) @ EWorldEditor;
    mouseRotateScale = getPrefSetting($pref::WorldEditor::mouseRotateScale, 0.01) @ EWorldEditor;
    mouseScaleScale = getPrefSetting($pref::WorldEditor::mouseScaleScale, 0.01) @ EWorldEditor;
    objSelectFillAlpha = getPrefSetting($pref::WorldEditor::objSelectFillAlpha, 100) @ EWorldEditor;
    minScaleFactor = getPrefSetting($pref::WorldEditor::minScaleFactor, 0.1) @ EWorldEditor;
    maxScaleFactor = getPrefSetting($pref::WorldEditor::maxScaleFactor, 4000) @ EWorldEditor;
    objSelectColor = getPrefSetting($pref::WorldEditor::objSelectColor, "255 0 0") @ EWorldEditor;
    objMouseOverSelectColor = getPrefSetting($pref::WorldEditor::objMouseOverSelectColor, "0 0 255") @ EWorldEditor;
    objMouseOverColor = getPrefSetting($pref::WorldEditor::objMouseOverColor, "0 255 0") @ EWorldEditor;
    showMousePopupInfo = getPrefSetting($pref::WorldEditor::showMousePopupInfo, 1) @ EWorldEditor;
    dragRectColor = getPrefSetting($pref::WorldEditor::dragRectColor, "255 255 0") @ EWorldEditor;
    renderObjText = getPrefSetting($pref::WorldEditor::renderObjText, 0) @ EWorldEditor;
    renderObjHandle = getPrefSetting($pref::WorldEditor::renderObjHandle, 0) @ EWorldEditor;
    faceSelectColor = getPrefSetting($pref::WorldEditor::faceSelectColor, "0 0 100 100") @ EWorldEditor;
    renderSelectionBox = getPrefSetting($pref::WorldEditor::renderSelectionBox, 0) @ EWorldEditor;
    selectionBoxColor = getPrefSetting($pref::WorldEditor::selectionBoxColor, "255 255 0") @ EWorldEditor;
    snapToGrid = getPrefSetting($pref::WorldEditor::snapToGrid, 0) @ EWorldEditor;
    snapRotations = getPrefSetting($pref::WorldEditor::snapRotations, 0) @ EWorldEditor;
    rotationSnap = getPrefSetting($pref::WorldEditor::rotationSnap, 15) @ EWorldEditor;
    softSelecting = 1 @ ETerrainEditor;
    currentAction = "raiseHeight" @ ETerrainEditor;
    currentMode = "select" @ ETerrainEditor;
};
function EditorGui::setPrefs() {
    $Pref::WorldEditor::dropType = dropType;
    EWorldEditor;
    $pref::WorldEditor::planarMovement = planarMovement;
    EWorldEditor;
    $pref::WorldEditor::undoLimit = undoLimit;
    EWorldEditor;
    $Pref::WorldEditor::dropType = dropType;
    EWorldEditor;
    $pref::WorldEditor::projectDistance = projectDistance;
    EWorldEditor;
    $pref::WorldEditor::boundingBoxCollision = boundingBoxCollision;
    EWorldEditor;
    $pref::WorldEditor::renderPlane = renderPlane;
    EWorldEditor;
    $pref::WorldEditor::renderPlaneHashes = renderPlaneHashes;
    EWorldEditor;
    $pref::WorldEditor::gridColor = gridColor;
    EWorldEditor;
    $pref::WorldEditor::planeDim = planeDim;
    EWorldEditor;
    $pref::WorldEditor::gridSize = gridSize;
    EWorldEditor;
    $pref::WorldEditor::renderPopupBackground = renderPopupBackground;
    EWorldEditor;
    $pref::WorldEditor::popupBackgroundColor = popupBackgroundColor;
    EWorldEditor;
    $pref::WorldEditor::popupTextColor = popupTextColor;
    EWorldEditor;
    $pref::WorldEditor::selectHandle = selectHandle;
    EWorldEditor;
    $pref::WorldEditor::defaultHandle = defaultHandle;
    EWorldEditor;
    $pref::WorldEditor::lockedHandle = lockedHandle;
    EWorldEditor;
    $pref::WorldEditor::objectTextColor = objectTextColor;
    EWorldEditor;
    $pref::WorldEditor::objectsUseBoxCenter = objectsUseBoxCenter;
    EWorldEditor;
    $pref::WorldEditor::axisGizmoMaxScreenLen = axisGizmoMaxScreenLen;
    EWorldEditor;
    $pref::WorldEditor::axisGizmoActive = axisGizmoActive;
    EWorldEditor;
    $pref::WorldEditor::mouseMoveScale = mouseMoveScale;
    EWorldEditor;
    $pref::WorldEditor::mouseRotateScale = mouseRotateScale;
    EWorldEditor;
    $pref::WorldEditor::mouseScaleScale = mouseScaleScale;
    EWorldEditor;
    $pref::WorldEditor::objSelectFillAlpha = objSelectFillAlpha;
    EWorldEditor;
    $pref::WorldEditor::minScaleFactor = minScaleFactor;
    EWorldEditor;
    $pref::WorldEditor::maxScaleFactor = maxScaleFactor;
    EWorldEditor;
    $pref::WorldEditor::objSelectColor = objSelectColor;
    EWorldEditor;
    $pref::WorldEditor::objMouseOverSelectColor = objMouseOverSelectColor;
    EWorldEditor;
    $pref::WorldEditor::objMouseOverColor = objMouseOverColor;
    EWorldEditor;
    $pref::WorldEditor::showMousePopupInfo = showMousePopupInfo;
    EWorldEditor;
    $pref::WorldEditor::dragRectColor = dragRectColor;
    EWorldEditor;
    $pref::WorldEditor::renderObjText = renderObjText;
    EWorldEditor;
    $pref::WorldEditor::renderObjHandle = renderObjHandle;
    EWorldEditor;
    $pref::WorldEditor::raceSelectColor = faceSelectColor;
    EWorldEditor;
    $pref::WorldEditor::renderSelectionBox = renderSelectionBox;
    EWorldEditor;
    $pref::WorldEditor::selectionBoxColor = selectionBoxColor;
    EWorldEditor;
    $pref::WorldEditor::snapToGrid = snapToGrid;
    EWorldEditor;
    $pref::WorldEditor::snapRotations = snapRotations;
    EWorldEditor;
    $pref::WorldEditor::rotationSnap = rotationSnap;
    EWorldEditor;
    updateStatus();
};
function EditorGui::onSleep(%this) {
    %this.setPrefs();
};
function EditorGui::init(%this) {
    %this.getPrefs();
    if (!(isObject("terraformer"))) {
        new "terraformer"();
    }
    $SelectedOperation = -(1.0);
    Terraformer;
    $NextOperationId = 1;
    0;
    $HeightfieldDirtyRow = -(1.0);
    clearMenus();
    "File".addMenu(0);
    "File".addMenuItem("New Mission...", 1);
    "File".addMenuItem("Open Mission...", 2, "Ctrl O");
    "File".addMenuItem("Save Mission...", 3, "Ctrl S");
    "File".addMenuItem("Save Mission As...", 4);
    "File".addMenuItem("-", 0);
    "File".addMenuItem("Import Terraform Data...", 6);
    "File".addMenuItem("Import Texture Data...", 5);
    "File".addMenuItem("-", 0);
    "File".addMenuItem("Refresh File List", 7);
    "File".addMenuItem("-", 0);
    "File".addMenuItem("Export Terraform Bitmap...", 5);
    "Edit".addMenu(1);
    "Edit".addMenuItem("Undo", 1, "Ctrl Z");
    "Edit".setMenuItemBitmap("Undo", 1);
    "Edit".addMenuItem("Redo", 2, "Ctrl R");
    "Edit".setMenuItemBitmap("Redo", 2);
    "Edit".addMenuItem("-", 0);
    "Edit".addMenuItem("Cut", 3, "Ctrl X");
    "Edit".setMenuItemBitmap("Cut", 3);
    "Edit".addMenuItem("Copy", 4, "Ctrl C");
    "Edit".setMenuItemBitmap("Copy", 4);
    "Edit".addMenuItem("Paste", 5, "Ctrl V");
    "Edit".setMenuItemBitmap("Paste", 5);
    "Edit".addMenuItem("-", 0);
    "Edit".addMenuItem("Select All", 6, "Ctrl A");
    "Edit".addMenuItem("Select None", 7, "Ctrl N");
    "Edit".addMenuItem("Select None", 8, "Ctrl D");
    "Edit".addMenuItem("Select Inverse", 9, "Ctrl-Shift I");
    "Edit".addMenuItem("Find Selected", 10, "Ctrl F");
    "Edit".addMenuItem("Zoom Camera To Selection", 11, "Shift F");
    "Edit".addMenuItem("Expand Selected Tree", 12, "Ctrl E");
    "Edit".addMenuItem("Expand And Select Selected Tree", 13, "Shift E");
    "Edit".addMenuItem("-", 0);
    "Edit".addMenuItem("Relight Scene", 14, "Alt L");
    "Edit".addMenuItem("-", 0);
    "Edit".addMenuItem("World Editor Settings...", 12);
    "Edit".addMenuItem("Terrain Editor Settings...", 13);
    "Edit".addMenuItem("-", 0);
    "Edit".addMenuItem("Increase Move Scale", 15, "]");
    "Edit".addMenuItem("Decrease Move Scale", 16, "[");
    "Edit".addMenuItem("Toggle Grid Visibility", 17, "g");
    "Edit".addMenuItem("Status Hud Toggle", 17, "s");
    "Camera".addMenu(7);
    "Camera".addMenuItem("Drop Camera at Player", 1, "Alt Q");
    "Camera".addMenuItem("Drop Player at Camera", 2, "Alt W");
    "Camera".addMenuItem("Toggle Camera", 10, "Alt C");
    "Camera".addMenuItem("Drop Camera at Selection", 19, "Alt T");
    "Camera".addMenuItem("-", 0);
    "Camera".addMenuItem("Slowest", 3, "Shift 1", 1);
    "Camera".addMenuItem("Very Slow", 4, "Shift 2", 1);
    "Camera".addMenuItem("Slow", 5, "Shift 3", 1);
    "Camera".addMenuItem("Medium Pace", 6, "Shift 4", 1);
    "Camera".addMenuItem("Fast", 7, "Shift 5", 1);
    "Camera".addMenuItem("Very Fast", 8, "Shift 6", 1);
    "Camera".addMenuItem("Fastest", 9, "Shift 7", 1);
    "World".addMenu(6);
    "World".addMenuItem("Lock Selection", 10, "Ctrl L");
    "World".addMenuItem("Unlock Selection", 11, "Ctrl Shift L");
    "World".addMenuItem("-", 0);
    "World".addMenuItem("Hide Selected", 12, "Ctrl H");
    "World".addMenuItem("Unhide Selected", 13, "Shift H");
    "World".addMenuItem("Invert Hidden", 14, "Ctrl J");
    "World".addMenuItem("-", 0);
    "World".addMenuItem("Delete Selection", 17, "Delete");
    "World".addMenuItem("Reset Transforms", 15);
    "World".addMenuItem("Drop Selection", 16, "Ctrl Shift D");
    "World".addMenuItem("Add Selection to Instant Group", 17);
    "World".addMenuItem("SimGroup Create", 18, "N");
    "World".addMenuItem("-", 0);
    "World".addMenuItem("Drop at Origin", 0, "", 1);
    "World".addMenuItem("Drop at Camera", 1, "", 1);
    "World".addMenuItem("Drop at Camera w/Rot", 2, "", 1);
    "World".addMenuItem("Drop below Camera", 3, "", 1);
    "World".addMenuItem("Drop at Screen Center", 4, "", 1);
    "World".addMenuItem("Drop at Centroid", 5, "", 1);
    "World".addMenuItem("Drop to Ground", 6, "", 1);
    "SnapTo".addMenu(9);
    "SnapTo".addMenuItem("X", 1, "X");
    "SnapTo".addMenuItem("X+", 2, "Shift X");
    "SnapTo".addMenuItem("X-", 3, "Alt X");
    "SnapTo".addMenuItem("Y", 4, "Y");
    "SnapTo".addMenuItem("Y+", 5, "Shift Y");
    "SnapTo".addMenuItem("Y-", 6, "Alt Y");
    "SnapTo".addMenuItem("Z", 7, "Z");
    "SnapTo".addMenuItem("Z+", 8, "Shift Z");
    "SnapTo".addMenuItem("Z-", 9, "Alt Z");
    "SnapTo".addMenuItem("X+YZ", 10, "numpad6");
    "SnapTo".addMenuItem("X-YZ", 11, "numpad4");
    "SnapTo".addMenuItem("XY+Z", 12, "numpad8");
    "SnapTo".addMenuItem("XY-Z", 13, "numpad2");
    "SnapTo".addMenuItem("XYZ+", 12, "numpad9");
    "SnapTo".addMenuItem("XYZ-", 13, "numpad3");
    "SnapTo".addMenuItem("ObjX+YZ", 14, "shift numpad6");
    "SnapTo".addMenuItem("ObjX-YZ", 15, "shift numpad4");
    "SnapTo".addMenuItem("ObjXY+Z", 16, "shift numpad8");
    "SnapTo".addMenuItem("ObjXY-Z", 17, "shift numpad2");
    "SnapTo".addMenuItem("ObjXYZ+", 18, "shift numpad9");
    "SnapTo".addMenuItem("ObjXYZ-", 19, "shift numpad3");
    "CloneTo".addMenu(10);
    "CloneTo".addMenuItem("X+YZ", 1, "alt numpad6");
    "CloneTo".addMenuItem("X-YZ", 2, "alt numpad4");
    "CloneTo".addMenuItem("XY+Z", 3, "alt numpad8");
    "CloneTo".addMenuItem("XY-Z", 4, "alt numpad2");
    "CloneTo".addMenuItem("XYZ+", 5, "alt numpad9");
    "CloneTo".addMenuItem("XYZ-", 6, "alt numpad3");
    "Action".addMenu(3);
    "Action".addMenuItem("Select", 1, "", 1);
    "Action".addMenuItem("Adjust Selection", 2, "", 1);
    "Action".addMenuItem("-", 0);
    "Action".addMenuItem("Add Dirt", 6, "", 1);
    "Action".addMenuItem("Excavate", 6, "", 1);
    "Action".addMenuItem("Adjust Height", 6, "", 1);
    "Action".addMenuItem("Flatten", 4, "", 1);
    "Action".addMenuItem("Smooth", 5, "", 1);
    "Action".addMenuItem("Set Height", 7, "", 1);
    "Action".addMenuItem("-", 0);
    "Action".addMenuItem("Set Empty", 8, "", 1);
    "Action".addMenuItem("Clear Empty", 8, "", 1);
    "Action".addMenuItem("-", 0);
    "Action".addMenuItem("Paint Material", 9, "", 1);
    "Brush".addMenu(4);
    "Brush".addMenuItem("Box Brush", 91, "", 1);
    "Brush".addMenuItem("Circle Brush", 92, "", 1);
    "Brush".addMenuItem("-", 0);
    "Brush".addMenuItem("Soft Brush", 93, "", 2);
    "Brush".addMenuItem("Hard Brush", 94, "", 2);
    "Brush".addMenuItem("-", 0);
    "Brush".addMenuItem("Size 1 x 1", 1, "Alt 1", 3);
    "Brush".addMenuItem("Size 3 x 3", 3, "Alt 2", 3);
    "Brush".addMenuItem("Size 5 x 5", 5, "Alt 3", 3);
    "Brush".addMenuItem("Size 9 x 9", 9, "Alt 4", 3);
    "Brush".addMenuItem("Size 15 x 15", 15, "Alt 5", 3);
    "Brush".addMenuItem("Size 25 x 25", 25, "Alt 6", 3);
    "Window".addMenu(2);
    "Window".addMenuItem("World Editor", 2, "F2", 1);
    "Window".addMenuItem("World Editor Inspector", 3, "F3", 1);
    "Window".addMenuItem("World Editor Creator", 4, "F4", 1);
    "Window".addMenuItem("Mission Area Editor", 5, "F5", 1);
    "Window".addMenuItem("-", 0);
    "Window".addMenuItem("Terrain Editor", 6, "F6", 1);
    "Window".addMenuItem("Terrain Terraform Editor", 7, "F7", 1);
    "Window".addMenuItem("Terrain Texture Editor", 8, "F8", 1);
    "Window".addMenuItem("Terrain Texture Painter", 9, "", 1);
    %selectMenuName = "Select Type";
    EditorMenuBar;
    %n = 1;
    EditorMenuBar;
    %selectMenuName.addMenu(11);
    %selectMenuName.addMenuItem("All Types", %n, "Ctrl 1", 1);
    %n = (1.0 + %n);
    EditorMenuBar;
    %selectMenuName.addMenuItem("Triggers", %n, "Ctrl 2", 1);
    %n = (1.0 + %n);
    EditorMenuBar;
    %selectMenuName.addMenuItem("Interiors", %n, "Ctrl 3", 1);
    %n = (1.0 + %n);
    EditorMenuBar;
    %selectMenuName.addMenuItem("Shapes and Sit Markers", %n, "Ctrl 4", 1);
    %n = (1.0 + %n);
    EditorMenuBar;
    %selectMenuName.addMenuItem("Antiportals", %n, "Ctrl 5", 1);
    %n = (1.0 + %n);
    EditorMenuBar;
    %selectMenuName.addMenuItem("Items", %n, "ctrl 6", 1);
    %n = (1.0 + %n);
    EditorMenuBar;
    %selectMenuName.addMenuItem("Audio Emitters", %n, "Ctrl 7", 1);
    %n = (1.0 + %n);
    EditorMenuBar;
    %selectMenuName.addMenuItem("-", %n);
    %n = (1.0 + %n);
    EditorMenuBar;
    %selectMenuName.addMenuItem("Select all AdvertShapes", %n);
    %n = (1.0 + %n);
    EditorMenuBar;
    %selectMenuName.addMenuItem("Select all AIPlayers", %n);
    %n = (1.0 + %n);
    EditorMenuBar;
    %selectMenuName.addMenuItem("Select all ETSSeatMarker", %n);
    %n = (1.0 + %n);
    EditorMenuBar;
    %selectMenuName.addMenuItem("Select all InteriorInstances", %n);
    %n = (1.0 + %n);
    EditorMenuBar;
    %selectMenuName.addMenuItem("Select all Markers", %n);
    %n = (1.0 + %n);
    EditorMenuBar;
    %selectMenuName.addMenuItem("Select all MissionMarkers", %n);
    %n = (1.0 + %n);
    EditorMenuBar;
    %selectMenuName.addMenuItem("Select all StaticShapes", %n);
    %n = (1.0 + %n);
    EditorMenuBar;
    %selectMenuName.addMenuItem("Select all sgUniversalStaticLights", %n);
    %n = (1.0 + %n);
    EditorMenuBar;
    %selectMenuName.addMenuItem("Select all Triggers", %n);
    %n = (1.0 + %n);
    EditorMenuBar;
    %selectMenuName.addMenuItem("Select all TSStatics", %n);
    %n = (1.0 + %n);
    EditorMenuBar;
    %selectMenuName.addMenuItem("Select all Waterblocks", %n);
    %n = (1.0 + %n);
    EditorMenuBar;
    %debugMenuName = "Render Mode";
    EditorMenuBar;
    %debugMenuName.addMenu(11);
    %debugMenuName.addMenuItem("normal", 1, "Shift N", 1);
    %debugMenuName.addMenuItem("lines", 2, "Shift F2", 1);
    %debugMenuName.addMenuItem("detail polys", 3, "Shift F3", 1);
    %debugMenuName.addMenuItem("portal zones", 4, "Shift F4", 1);
    %debugMenuName.addMenuItem("null surfaces", 5, "Shift F5", 1);
    %debugMenuName.addMenuItem("portal zones nonRoot", 6, "Shift F6", 1);
    %debugMenuName.addMenuItem("zonesNonRoot, Detail", 7, "Shift F7", 1);
    %debugMenuName.addMenuItem("large textures", 8, "Shift F8", 1);
    %debugMenuName.addMenuItem("detail level", 9, "Shift F9", 1);
    %debugMenuName.addMenuItem("lightmap", 10, "Shift F10", 1);
    %debugMenuName.addMenuItem("only textures", 11, "Shift F11", 1);
    %debugMenuName.addMenuItem("triangle strips", 12, "Shift F12", 1);
    %debugMenuName.addMenuItem("next mode", 13, "=", 1);
    %debugMenuName.addMenuItem("prev mode", 14, "-", 1);
    $sgEditorItemNames::sgMenu.addMenu(8);
    $sgEditorItemNames::sgMenu.addMenuItem(EditorMenuBar, 2, "F12");
    0.onActionMenuItemSelect("Adjust Height");
    0.onBrushMenuItemSelect("Circle Brush");
    0.onBrushMenuItemSelect("Soft Brush");
    9.onBrushMenuItemSelect("Size 9 x 9");
    6.onCameraMenuItemSelect("Medium Pace");
    0.onWorldMenuItemSelect("Drop at Screen Center");
    init();
    attachTerrain();
    TerraformerInit();
    TextureInit();
    init();
    init();
    isDirty = ObjectBuilderGui @ 0 @ EditorTree;
    EditorTree;
    isDirty = ETerrainEditor @ 0 @ EWorldEditor;
    EWorldEditor;
    isDirty = EditorMenuBar @ 0 @ ETerrainEditor;
    EditorMenuBar;
    isMissionDirty = EditorMenuBar @ 0 @ ETerrainEditor;
    EditorMenuBar;
    saveAs = EditorMenuBar @ 0 @ EditorGui;
    EditorMenuBar;
};
function EditorNewMission() {
    if (isMissionDirty) {
    }
    if (isDirty) {
    }
    if (isDirty) {
    }
    if (isDirty) {
        MessageBoxYesNo("Mission Modified", EWorldEditor @ EditorTree @ "Would you like to save changes to the current mission \"" @ $Server::MissionFile @ "\" before creating a new mission?", "EditorDoNewMission(true);", "EditorDoNewMission(false);");
    }
    EditorDoNewMission(0);
};
function EditorSaveMissionMenu() {
    if (saveAs) {
        EditorSaveMissionAs();
    }
    EditorSaveMission();
};
function EditorSaveMission() {
    if (isDirty) {
    }
    if (isDirty) {
    }
    if (isMissionDirty) {
    }
    if (!(isWriteableFileName($Server::MissionFile))) {
        MessageBoxOK("Error", EditorTree @ ETerrainEditor @ "Mission file \"" @ $Server::MissionFile @ "\" is read-only.", "");
        return 0;
    }
    if (isDirty) {
    }
    if (!(isWriteableFileName(terrainFile))) {
        MessageBoxOK("Error", Terrain @ "Terrain file \"" @ Terrain @ terrainFile @ "\" is read-only.", "");
        return 0;
    }
    %errorCount = RunTestCase("TEST_MISSIONGROUPINTEGRITY", "WARNING: About that mission file you just saved...");
    if (isDirty) {
    }
    if (isDirty) {
    }
    if (isMissionDirty) {
        if ((MissionInfo SPC mode $= "PrivateSpaceDesign")) {
            if (isObject()) {
                add();
            }
            error("PrivateSpaceDesign mode, no PRIVATESPACE_GROUP object");
        }
        $Server::MissionFile.save();
        if ((MissionInfo SPC mode $= "PrivateSpaceDesign")) {
            if (isObject()) {
                %spaceForGridFileName = PRIVATESPACE_GROUP @ getSubStr($Server::MissionFile, 0, (4.0 - strlen($Server::MissionFile))) @ "_generated.cs";
                MissionGroup;
                %spaceForGridFileName.save();
                echo("PrivateSpaceDesign mode saving to space for grid, file named:" @ " " @ %spaceForGridFileName);
                add();
            }
        }
    }
    if ((MissionInfo SPC mode $= "PrivateSpaceDesign")) {
        if (isObject()) {
            %spaceForGridFileName = PRIVATESPACE_GROUP @ getSubStr($Server::MissionFile, 0, (4.0 - strlen($Server::MissionFile))) @ "_generated.cs";
            PRIVATESPACE_GROUP;
            %spaceForGridFileName.save();
            echo("PrivateSpaceDesign mode saving to space for grid, file named:" @ " " @ %spaceForGridFileName);
        }
    }
    if (isDirty) {
        terrainFile.save();
    }
    isDirty = Terrain @ 0 @ EditorTree;
    Terrain;
    isDirty = ETerrainEditor @ 0 @ EWorldEditor;
    PRIVATESPACE_GROUP;
    isDirty = MissionGroup @ 0 @ ETerrainEditor;
    PRIVATESPACE_GROUP;
    isMissionDirty = PRIVATESPACE_GROUP @ 0 @ ETerrainEditor;
    RootGroup;
    saveAs = PRIVATESPACE_GROUP @ 0 @ EditorGui;
    ETerrainEditor;
    return 1;
};
function EditorDoSaveAs(%missionName) {
    isDirty = 1 @ ETerrainEditor;
    isDirty = 1 @ EWorldEditor;
    isDirty = 1 @ EditorTree;
    %saveMissionFile = $Server::MissionFile;
    %saveTerrName = terrainFile;
    Terrain;
    $Server::MissionFile = %missionName;
    terrainFile = filePath(%missionName) @ "/" @ fileBase(%missionName) @ ".ter" @ Terrain;
    if (!(EditorSaveMission())) {
        $Server::MissionFile = %saveMissionFile;
        terrainFile = %saveTerrName @ Terrain;
    }
};
function EditorSaveMissionAs() {
    getSaveFilename("*.mis", "EditorDoSaveAs", $Server::MissionFile);
};
function EditorDoLoadMission(%file) {
    close();
    loadMission(%file, 1);
    Editor::Create();
    add();
    loadingMission = Editor @ 1 @ EditorGui;
    MissionCleanup;
    open();
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
    saveAs = 1 @ EditorGui;
    isDirty = 1 @ EWorldEditor;
    isDirty = 1 @ ETerrainEditor;
    isDirty = 1 @ EditorTree;
};
function EditorOpenMission() {
    if (isMissionDirty) {
    }
    if (isDirty) {
    }
    if (isDirty) {
    }
    if (isDirty) {
        MessageBoxYesNo("Mission Modified", EWorldEditor @ EditorTree @ "Would you like to save changes to the current mission \"" @ $Server::MissionFile @ "\" before opening a new mission?", "EditorSaveBeforeLoad();", "getLoadFilename(\"*.mis\", \"EditorDoLoadMission\");");
    }
    getLoadFilename("*.mis", "EditorDoLoadMission");
};
function EditorMenuBar::onMenuSelect(%this, %unused, %menu) {
    if ((%menu $= "File")) {
        if (isVisible()) {
        }
        %editingHeightfield = isVisible();
        EHeightField;
        "File".setMenuItemEnable("Export Terraform Bitmap...", %editingHeightfield);
        if (isDirty) {
        }
        if (isMissionDirty) {
        }
        if (isDirty) {
        }
        "File".setMenuItemEnable("Save Mission...", isDirty);
    }
    if ((EditorTree SPC %menu $= "Edit")) {
        %selSize = getSelectionSize();
        EWorldEditor;
        "Edit".setMenuItemEnable("Zoom Camera To Selection", (0.0 > %selSize));
        if (isVisible()) {
            "Edit".setMenuItemEnable("Select All", 1);
            "Edit".setMenuItemEnable("Paste", canPasteSelection());
            %canCutCopy = (EWorldEditor > getSelectionSize());
            0.0;
            "Edit".setMenuItemEnable("Cut", %canCutCopy);
            "Edit".setMenuItemEnable("Copy", %canCutCopy);
        }
        if (isVisible()) {
            "Edit".setMenuItemEnable("Cut", 0);
            "Edit".setMenuItemEnable("Copy", 0);
            "Edit".setMenuItemEnable("Paste", 0);
            "Edit".setMenuItemEnable("Select All", 0);
        }
    }
    if ((EditorMenuBar SPC %menu $= "World")) {
        %selSize = getSelectionSize();
        EWorldEditor;
        %lockCount = getSelectionLockCount();
        EWorldEditor;
        %hideCount = getSelectionHiddenCount();
        EWorldEditor;
        "World".setMenuItemEnable("Lock Selection", (%selSize < %lockCount));
        "World".setMenuItemEnable("Unlock Selection", (0.0 > %lockCount));
        "World".setMenuItemEnable("Hide Selected", (%selSize < %hideCount));
        "World".setMenuItemEnable("Unhide Selected", (0.0 > %hideCount));
        "World".setMenuItemEnable("Invert Hidden", (0.0 > %selSize));
        "World".setMenuItemEnable("Add Selection to Instant Group", (0.0 > %selSize));
        if ((0.0 > %selSize)) {
        }
        "World".setMenuItemEnable("Reset Transforms", (0.0 == %lockCount));
        if ((0.0 > %selSize)) {
        }
        "World".setMenuItemEnable("Drop Selection", (0.0 == %lockCount));
        if ((0.0 > %selSize)) {
        }
        "World".setMenuItemEnable("Delete Selection", (0.0 == %lockCount));
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
    init();
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
        dropCameraWithSelectionInView();
    }
    %this.setMenuItemChecked("Camera", %itemId, 1);
    $Camera::movementSpeed = (5.0 + (195.0 * (6.0 / (3.0 - %itemId))));
    EWorldEditor;
};
function EditorMenuBar::onActionMenuItemSelect(%this, %itemId, %item) {
    "Action".setMenuItemChecked(%item, 1);
    if ((EditorMenuBar SPC %item $= "Select")) {
        currentMode = "select" @ ETerrainEditor;
        selectionHidden = 0 @ ETerrainEditor;
        renderVertexSelection = 1 @ ETerrainEditor;
        "select".setAction();
    }
    if ((ETerrainEditor SPC %item $= "Adjust Selection")) {
        currentMode = "adjust" @ ETerrainEditor;
        selectionHidden = 0 @ ETerrainEditor;
        "adjustHeight".setAction();
        currentAction = brushAdjustHeight @ ETerrainEditor;
        ETerrainEditor;
        renderVertexSelection = 1 @ ETerrainEditor;
    }
    currentMode = "paint" @ ETerrainEditor;
    selectionHidden = 1 @ ETerrainEditor;
    currentAction.setAction();
    if ((ETerrainEditor SPC %item $= "Add Dirt")) {
        currentAction = raiseHeight @ ETerrainEditor;
        ETerrainEditor;
        renderVertexSelection = 1 @ ETerrainEditor;
    }
    if ((%item $= "Paint Material")) {
        currentAction = paintMaterial @ ETerrainEditor;
        renderVertexSelection = 1 @ ETerrainEditor;
    }
    if ((%item $= "Excavate")) {
        currentAction = lowerHeight @ ETerrainEditor;
        renderVertexSelection = 1 @ ETerrainEditor;
    }
    if ((%item $= "Set Height")) {
        currentAction = setHeight @ ETerrainEditor;
        renderVertexSelection = 1 @ ETerrainEditor;
    }
    if ((%item $= "Adjust Height")) {
        currentAction = brushAdjustHeight @ ETerrainEditor;
        renderVertexSelection = 1 @ ETerrainEditor;
    }
    if ((%item $= "Flatten")) {
        currentAction = flattenHeight @ ETerrainEditor;
        renderVertexSelection = 1 @ ETerrainEditor;
    }
    if ((%item $= "Smooth")) {
        currentAction = smoothHeight @ ETerrainEditor;
        renderVertexSelection = 1 @ ETerrainEditor;
    }
    if ((%item $= "Set Empty")) {
        currentAction = setEmpty @ ETerrainEditor;
        renderVertexSelection = 0 @ ETerrainEditor;
    }
    if ((%item $= "Clear Empty")) {
        currentAction = clearEmpty @ ETerrainEditor;
        renderVertexSelection = 0 @ ETerrainEditor;
    }
    if ((ETerrainEditor SPC currentMode $= "select")) {
        currentAction.processAction();
    }
    if ((ETerrainEditor SPC currentMode $= "paint")) {
        currentAction.setAction();
    }
};
function EditorMenuBar::onBrushMenuItemSelect(%this, %itemId, %item) {
    "Brush".setMenuItemChecked(%item, 1);
    if ((EditorMenuBar SPC %item $= "Box Brush")) {
        setBrushType();
    }
    if ((box SPC %item $= "Circle Brush")) {
        setBrushType();
    }
    if ((ellipse SPC %item $= "Soft Brush")) {
        enableSoftBrushes = ETerrainEditor @ 1 @ ETerrainEditor;
        ETerrainEditor;
    }
    if ((%item $= "Hard Brush")) {
        enableSoftBrushes = 0 @ ETerrainEditor;
    }
    brushSize = %itemId @ ETerrainEditor;
    %itemId.setBrushSize(%itemId);
};
function EditorMenuBar::onRenderModeMenuItemSelect(%this, %itemId, %item) {
    "Render Mode".setMenuItemChecked(%item, 1);
    if ((EditorMenuBar SPC %item $= "normal")) {
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
        %classname.selectAllObjectsOfClassName();
    }
    if ((EWorldEditor SPC %item $= "All Types")) {
        selectType = $TypeMasks::ALLTYPES @ EWorldEditor;
    }
    if ((%item $= "Triggers")) {
        selectType = $TypeMasks::TriggerObjectType @ EWorldEditor;
    }
    if ((%item $= "Interiors")) {
        selectType = $TypeMasks::InteriorObjectType @ EWorldEditor;
    }
    if ((%item $= "Audio Emitters")) {
        selectType = $TypeMasks::MarkerObjectType @ EWorldEditor;
    }
    if ((%item $= "Shapes and Sit Markers")) {
        selectType = ($TypeMasks::StaticTSObjectType | $TypeMasks::ShapeBaseObjectType) @ EWorldEditor;
    }
    if ((%item $= "Items")) {
        selectType = $TypeMasks::ItemObjectType @ EWorldEditor;
    }
    if ((%item $= "Antiportals")) {
        selectType = $TypeMasks::AntiPortalObjectType @ EWorldEditor;
    }
    selectType = $TypeMasks::ALLTYPES @ EWorldEditor;
    "Select Type".setMenuItemChecked(%item, 1);
    updateStatus();
};
function EditorMenuBar::onWorldMenuItemSelect(%this, %itemId, %item) {
    if ((%item $= "Lock Selection")) {
        1.lockSelection();
    }
    if ((EWorldEditor SPC %item $= "Unlock Selection")) {
        0.lockSelection();
    }
    if ((EWorldEditor SPC %item $= "Hide Selected")) {
        1.hideSelection();
    }
    if ((EWorldEditor SPC %item $= "Hide All But Selected")) {
        1.hideAllButSelection();
    }
    if ((EWorldEditor SPC %item $= "Unhide Selected")) {
        0.hideSelection();
    }
    if ((EWorldEditor SPC %item $= "Invert Hidden")) {
        invertHiddenSelection();
    }
    if ((EWorldEditor SPC %item $= "Reset Transforms")) {
        resetTransforms();
    }
    if ((EWorldEditor SPC %item $= "Drop Selection")) {
        dropSelection();
    }
    if ((EWorldEditor SPC %item $= "SimGroup Create")) {
        buildSimGroup();
    }
    if ((ObjectBuilderGui SPC %item $= "Delete Selection")) {
        deleteSelection();
        uninspect();
    }
    if ((inspector SPC %item $= "Add Selection to Instant Group")) {
        addSelectionToAddGroup();
    }
    "World".setMenuItemChecked(%item, 1);
    if ((EditorMenuBar SPC %item $= "Drop at Origin")) {
        dropType = EWorldEditor @ "atOrigin" @ EWorldEditor;
        EWorldEditor;
    }
    if ((%item $= "Drop at Camera")) {
        dropType = "atCamera" @ EWorldEditor;
    }
    if ((%item $= "Drop at Camera w/Rot")) {
        dropType = "atCameraRot" @ EWorldEditor;
    }
    if ((%item $= "Drop below Camera")) {
        dropType = "belowCamera" @ EWorldEditor;
    }
    if ((%item $= "Drop at Screen Center")) {
        dropType = "screenCenter" @ EWorldEditor;
    }
    if ((%item $= "Drop to Ground")) {
        dropType = "toGround" @ EWorldEditor;
    }
    if ((%item $= "Drop at Centroid")) {
        dropType = "atCentroid" @ EWorldEditor;
    }
};
function EditorMenuBar::OnSnapToMenuItemSelect(%this, %itemId, %item) {
    %item.multiSnapTo();
};
function EditorMenuBar::OnCloneToMenuItemSelect(%this, %itemId, %item) {
    %item.CloneTo();
};
function EditorMenuBar::onEditMenuItemSelect(%this, %itemId, %item) {
    if ((%item $= "World Editor Settings...")) {
        0.pushDialog();
    }
    if ((WorldEditorSettingsDlg SPC %item $= "Terrain Editor Settings...")) {
        99.pushDialog();
    }
    if ((TerrainEditorValuesSettingsGui SPC %item $= "Relight Scene")) {
        lightScene("");
    }
    if ((forceAlways SPC %item $= "Increase Move Scale")) {
        increaseMoveScale();
    }
    if ((EWorldEditor SPC %item $= "Toggle Grid Visibility")) {
        renderPlane = EWorldEditor @ !(renderPlane) @ EWorldEditor;
        Canvas;
        renderPlaneHashes = EWorldEditor @ !(renderPlaneHashes) @ EWorldEditor;
        Canvas;
    }
    if ((%item $= "Status Hud Toggle")) {
        toggleStatusHud();
    }
    if ((%item $= "Decrease Move Scale")) {
        decreaseMoveScale();
    }
    if (isVisible()) {
        if ((EWorldEditor SPC %item $= "Undo")) {
            undo();
        }
        if ((EWorldEditor SPC %item $= "Redo")) {
            redo();
        }
        if ((EWorldEditor SPC %item $= "Copy")) {
            copySelection();
        }
        if ((EWorldEditor SPC %item $= "Cut")) {
            copySelection();
            deleteSelection();
            uninspect();
        }
        if ((inspector SPC %item $= "Paste")) {
            pasteSelection();
        }
        if ((EWorldEditor SPC %item $= "Select All")) {
            selectAllObjects();
        }
        if ((EWorldEditor SPC %item $= "Select None")) {
            clearSelection();
        }
        if ((EWorldEditor SPC %item $= "Select Inverse")) {
            invertSelection();
        }
        if ((EWorldEditor SPC %item $= "Find Selected")) {
            FindSelectedInEditorTree();
        }
        if ((EWorldEditor SPC %item $= "Zoom Camera To Selection")) {
            dropCameraToSelection();
        }
        if ((EWorldEditor SPC %item $= "Expand Selected Tree")) {
            ExpandSelectedInEditorTree();
        }
        if ((EWorldEditor SPC %item $= "Expand And Select Selected Tree")) {
            ExpandSelectedAndSelectInEditorTree();
        }
    }
    if (isVisible()) {
        if ((ETerrainEditor SPC %item $= "Undo")) {
            undo();
        }
        if ((ETerrainEditor SPC %item $= "Redo")) {
            redo();
        }
        if ((ETerrainEditor SPC %item $= "Select None")) {
            clearSelection();
        }
    }
};
function EditorMenuBar::onToggleSGTools(%this, %itemId, %item) {
    %item.toggleSGTools();
};
function EditorMenuBar::onWindowMenuItemSelect(%this, %itemId, %item) {
    %item.setEditor();
};
function Creator::onWake(%this) {
    init();
};
function Creator::onSleep(%this) {
    $LastEditorChosenInstantGroup = $instantGroup;
};
function EditorGui::setWorldEditorVisible(%this) {
    1.setVisible();
    0.setVisible();
    "World".setMenuVisible(1);
    "Action".setMenuVisible(0);
    "Brush".setMenuVisible(0);
    1.makeFirstResponder();
    1.open();
};
function EditorGui::setTerrainEditorVisible(%this) {
    0.setVisible();
    1.setVisible();
    attachTerrain();
    0.setVisible();
    0.setVisible();
    "World".setMenuVisible(0);
    "Action".setMenuVisible(1);
    "Brush".setMenuVisible(1);
    1.makeFirstResponder();
    0.setVisible();
};
function EditorGui::toggleSGTools(%this, %item) {
    if ((%item $= %item[$sgEditorItemNames::sgMenuItem @ 0])) {
        sgLightEditor::toggle();
    }
};
function EditorGui::setEditor(%this, %editor) {
    "Window".setMenuItemBitmap(currentEditor, -(1.0));
    "Window".setMenuItemBitmap(%editor, 0);
    currentEditor = EditorMenuBar @ %editor @ %this;
    %this;
    if ((EditorMenuBar SPC %editor $= "World Editor")) {
        0.setVisible();
        0.setVisible();
        %this.setWorldEditorVisible();
    }
    if ((EWMissionArea SPC %editor $= "World Editor Inspector")) {
        1.setVisible();
        0.setVisible();
        0.setVisible();
        1.setVisible();
        %this.setWorldEditorVisible();
    }
    if ((EWInspectorPane SPC %editor $= "World Editor Creator")) {
        1.setVisible();
        0.setVisible();
        1.setVisible();
        0.setVisible();
        %this.setWorldEditorVisible();
    }
    if ((EWInspectorPane SPC %editor $= "Mission Area Editor")) {
        0.setVisible();
        1.setVisible();
        %this.setWorldEditorVisible();
    }
    if ((EWMissionArea SPC %editor $= "Terrain Editor")) {
        %this.setTerrainEditorVisible();
    }
    if ((EWFrame SPC %editor $= "Terrain Terraform Editor")) {
        %this.setTerrainEditorVisible();
        1.setVisible();
    }
    if ((EHeightField SPC %editor $= "Terrain Texture Editor")) {
        %this.setTerrainEditorVisible();
        1.setVisible();
    }
    if ((ETexture SPC %editor $= "Terrain Texture Painter")) {
        %this.setTerrainEditorVisible();
        1.setVisible();
        setup();
    }
};
function EditorGui::getHelpPage(%this) {
    if ((%this SPC currentEditor $= "World Editor")) {
        if ((%this SPC currentEditor $= "World Editor Inspector")) {
        }
    }
    if ((%this SPC currentEditor $= "World Editor Creator")) {
        return "5. World Editor";
    }
    if ((%this SPC currentEditor $= "Mission Area Editor")) {
        return "6. Mission Area Editor";
    }
    if ((%this SPC currentEditor $= "Terrain Editor")) {
        return "7. Terrain Editor";
    }
    if ((%this SPC currentEditor $= "Terrain Terraform Editor")) {
        return "8. Terrain Terraform Editor";
    }
    if ((%this SPC currentEditor $= "Terrain Texture Editor")) {
        return "9. Terrain Texture Editor";
    }
    if ((%this SPC currentEditor $= "Terrain Texture Painter")) {
        return "10. Terrain Texture Painter";
    }
};
function ETerrainEditor::setPaintMaterial(%this, %matIndex) {
    paintMaterial = %matIndex @ EPainter @ mat @ ETerrainEditor;
};
function ETerrainEditor::changeMaterial(%this, %matIndex) {
    matIndex = %matIndex @ EPainter;
    getLoadFilename("*/terrains/*.png\t*/terrains/*.jpg");
};
function EPainterChangeMat(%file) {
    %file = filePath(%file) @ "/" @ fileBase(%file);
    %i = 0;
    if ((6.0 < %i)) {
        if ((%i @ EPainter SPC mat $= %file)) {
            return;
        }
        %i = (1.0 + %i);
    }
    mat = (6.0 < %i) @ %file @ EPainter @ matIndex @ EPainter;
    %mats = "";
    %i = 0;
    if ((6.0 < %i)) {
        %mats = %mats @ %i @ EPainter @ mat @ "\n";
        %i = (1.0 + %i);
    }
    %mats.setTerrainMaterials();
    setup();
    EPainter @ matIndex.performClick();
};
function EPainter::setup(%this) {
    0.onActionMenuItemSelect("Paint Material");
    %mats = getTerrainMaterials();
    ETerrainEditor;
    %valid = 1;
    EditorMenuBar;
    %i = 0;
    if ((6.0 < %i)) {
        %mat = getRecord(%mats, %i);
        mat = %mat @ %i @ %this;
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
    performClick();
};
function EditorGui::onWake(%this) {
    push();
    push();
    %this.setEditor(currentEditor);
};
function EditorGui::onSleep(%this) {
    pop();
    pop();
};
function AreaEditor::onUpdate(%this, %area) {
    AreaEditingText @ "X: " @ getWord(%area, 0) @ " Y: " @ getWord(%area, 1) @ " W: " @ getWord(%area, 2) @ " H: " @ getWord(%area, 3).setValue();
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
        %object.invertSelectObject();
        %i = (1.0 + %i);
        EWorldEditor;
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
            %object.selectObject();
        }
        %i = (1.0 + %i);
        EWorldEditor;
    }
};
function WorldEditor::selectAllObjects(%this) {
    if (isObject()) {
        RecurseSelectObjectsInGroup("");
    }
};
function WorldEditor::selectAllObjectsOfClassName(%this, %classname) {
    if (isObject()) {
        RecurseSelectObjectsInGroup(%classname);
    }
};
function WorldEditor::invertSelection(%this) {
    if (isObject()) {
        RecurseInvertSelectObjectsInGroup();
    }
};
function WorldEditor::increaseMoveScale(%this) {
    %max = 10;
    mouseMoveScale = (EWorldEditor * mouseMoveScale);
    2.0;
    if ((EWorldEditor > mouseMoveScale)) {
        mouseMoveScale = %max @ %max @ EWorldEditor;
    }
    setPrefs();
};
function WorldEditor::decreaseMoveScale(%this) {
    %min = 0.001;
    mouseMoveScale = (EWorldEditor / mouseMoveScale);
    2.0;
    if ((EWorldEditor < mouseMoveScale)) {
        mouseMoveScale = %min @ %min @ EWorldEditor;
    }
    setPrefs();
};
function WorldEditor::onDelete(%this) {
    deleteSelection();
    uninspect();
    updateStatus();
    "".updateGeneralInfo();
};
function WorldEditor::onSelect(%this, %obj) {
    %obj.addSelection();
    updateStatus();
    "".updateGeneralInfo();
};
function WorldEditor::onUnSelect(%this, %obj) {
    %obj.removeSelection();
    uninspect();
    updateStatus();
    "".updateGeneralInfo();
};
function WorldEditor::onClearSelected(%this) {
    clearSelection();
    uninspect();
    updateStatus();
    "".updateGeneralInfo();
};
function WorldEditor::onClearSelection(%this) {
    clearSelection();
    uninspect();
    updateStatus();
    "".updateGeneralInfo();
};
function EditorTree::onDragDrop(%this) {
    isDirty = 1 @ EditorTree;
};
function EditorTree::onObjectDeleteCompleted(%this) {
    isDirty = 1 @ EditorTree;
    copySelection();
    deleteSelection();
    uninspect();
    updateStatus();
    "".updateGeneralInfo();
};
function EditorTree::onClearSelected(%this) {
    clearSelection();
};
function EditorTree::init(%this) {
    profile = new GuiControl(ETContextPopupDlg) @ "GuiModelessDialogProfile";
    horizSizing = "width";
    vertSizing = "height";
    position = "0 0";
    extent = "640 480";
    minExtent = "8 8";
    visible = 1;
    setFirstResponder = 0;
    modal = 1;
    profile = new GuiPopUpMenuCtrl(ETContextPopup) @ "GuiScrollProfile";
    position = "0 0";
    extent = "0 0";
    minExtent = "0 0";
    maxPopupHeight = 200;
    command = "canvas.popDialog(ETContextPopupDlg);";
    0.setVisible();
};
function EditorTree::OnInspect(%this, %obj) {
    %obj.inspect();
    %obj.getName().setValue();
    %obj.updateGeneralInfo();
};
function EditorTree::onAddSelection(%this, %obj) {
    if ($AIEdit) {
        %obj.selectObject();
    }
    %obj.selectObject();
    isNetCacheable = TEST_MISSIONGROUPINTEGRITY @ %obj.getInitialNetCacheable() @ %obj;
    EWorldEditor;
};
function EditorTree::onRemoveSelection(%this, %obj) {
    if ($AIEdit) {
        %obj.selectObject();
    }
    %obj.unselectObject();
};
function EditorTree::onSelect(%this, %obj) {
    clearSelection();
    if (%obj.isClassSimGroup()) {
        %obj.inspect();
        %obj.getName().setValue();
        $userPref::Editor::autoSelectGroupContents = $userPref::Editor::autoSelectGroupContents;
        InspectorNameEdit;
        if ($userPref::Editor::autoSelectGroupContents) {
            RecurseSelectObjectsInGroup(%obj, "");
        }
    }
    if ($AIEdit) {
        %obj.selectObject();
    }
    %obj.selectObject();
};
function EditorTree::onUnSelect(%this, %obj) {
    if ($AIEdit) {
        %obj.unselectObject();
    }
    %obj.unselectObject();
};
function ETContextPopup::onSelect(%this, %index, %unused) {
    if ((0.0 == %index)) {
        contextObj.delete();
    }
};
function WorldEditor::init(%this) {
    %this.ignoreObjClass();
    numEditModes = AIObjective @ 3 @ %this;
    Sky;
    editMode = TerrainBlock @ "move" @ 0 @ %this;
    editMode = "rotate" @ 1 @ %this;
    editMode = "scale" @ 2 @ %this;
    profile = new GuiControl(WEContextPopupDlg) @ "GuiModelessDialogProfile";
    horizSizing = "width";
    vertSizing = "height";
    position = "0 0";
    extent = "640 480";
    minExtent = "8 8";
    visible = 1;
    setFirstResponder = 0;
    modal = 1;
    profile = new GuiPopUpMenuCtrl(WEContextPopup) @ "GuiScrollProfile";
    position = "0 0";
    extent = "0 0";
    minExtent = "0 0";
    maxPopupHeight = 200;
    command = "canvas.popDialog(WEContextPopupDlg);";
    0.setVisible();
};
function WorldEditor::onDblClick(%this, %obj) {
};
function WorldEditor::onClick(%this, %obj) {
    updateStatus();
    "".updateGeneralInfo();
    %obj.inspect();
    %obj.getName().setValue();
};
function WorldEditor::onEndDrag(%this, %obj) {
    updateStatus();
    %obj.inspect();
    %obj.getName().setValue();
};
function WorldEditor::export(%this) {
    getSaveFilename("~/editor/*.mac", %this @ ".doExport", "selection.mac");
};
function WorldEditor::doExport(%this, %file) {
    MissionGroup @ "~/editor/" @ %file.save(1);
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
        if ((%obj SPC locked $= "true")) {
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
        if (noShow) {
            %ret = (1.0 + %ret);
            %obj;
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
    %objToSnap.setTransform(setWord(%objToSnap.getTransform(), 0, (snapGapX + getWord(%objTarget.getTransform(), 0))));
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
    %objToSnap.setTransform(setWord(%objToSnap.getTransform(), 1, (snapGapY + getWord(%objTarget.getTransform(), 1))));
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
    %objToSnap.setTransform(setWord(%objToSnap.getTransform(), 2, (snapGapZ + getWord(%objTarget.getTransform(), 2))));
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
    %cam = Camera.getTransform();
    LocalClientConnection;
    %cam = setWord(%cam, 0, getWord(%pos, 0));
    %cam = setWord(%cam, 1, getWord(%pos, 1));
    %cam = setWord(%cam, 2, getWord(%pos, 2));
    Camera.setTransform(%cam);
    %control = getControlObject();
    LocalClientConnection;
    if ((Camera != %control)) {
        toggleCamera();
    }
};
function WorldEditor::dropCameraWithSelectionInView(%this) {
    if ((0.0 == %this.getSelectionSize())) {
        return;
    }
    %curCam = getControlObject();
    LocalClientConnection;
    %camera = Camera;
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
    %saveDropType = dropType;
    %this;
    dropType = "atCentroid" @ %this;
    %this.copySelection();
    %this.deleteSelection();
    %this.pasteSelection();
    dropType = %saveDropType @ %this;
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
    "EditorToolInspectorGui".isMember().setValue();
    "EditorToolMissionAreaGui".isMember().setValue();
    "EditorToolTreeViewGui".isMember().setValue();
    "EditorToolCreatorGui".isMember().setValue();
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
        %create = (%dirCount < %i) @ "createInterior(" @ "\"" @ %file @ "\"" @ ");";
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
        echo(%obj @ category);
        if (!(%obj SPC category $= "")) {
        }
        if ((%obj != category)) {
            %id = %this.findItemByName(category);
            %obj;
            if ((0.0 == %id)) {
                %grp = %this.insertItem(%base, category);
                %obj;
                %this.insertItem(%grp, %obj.getName(), "Obj: " @ %obj.getName() @ " - " @ 0.0 @ %obj.getClassName() @ "::create(" @ %obj.getName() @ ");", "Item");
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
        %create = (%dirCount < %i) @ "TSStatic::create(\"" @ %file @ "\");";
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
        %create = (%dirCount < %i) @ "TSDynamic::create(\"" @ %file @ "\");";
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
    position = InteriorInstance @ new ""() @ "0 0 0";
    0;
    rotation = "0 0 0";
    interiorFile = %name;
    %obj = ;
    isNetCacheable = TEST_MISSIONGROUPINTEGRITY @ %obj.getInitialNetCacheable() @ %obj;
    return %obj;
};
function WorldEditor::onAddSelected(%this, %obj) {
    %obj.addSelection();
};
function Creator::onSelect(%this) {
    clearSelection();
};
function Creator::OnInspect(%this, %obj) {
    if (!($missionRunning)) {
        return;
    }
    %objId = eval(%this.getItemValue(%obj));
    %obj.removeSelection();
    clearSelection();
    clearSelection();
    %objId.selectObject();
    dropSelection();
};
function ExpandSelectedInEditorTree() {
    %id = getSelectedItem();
    EditorTree;
    if ((-(1.0) != %id)) {
        %id.expandAllChildren();
    }
    echo("nothing selected");
};
function SelectRecursively(%itemId) {
    %obj = %itemId.getItemValue();
    EditorTree;
    if (isObject(%obj)) {
        %obj.selectObject();
    }
    %child = %itemId.getChild();
    EditorTree;
    if (%child) {
        SelectRecursively(%child);
    }
    %sibling = %itemId.getNextSibling();
    EditorTree;
    if (%sibling) {
        SelectRecursively(%sibling);
    }
};
function ExpandSelectedAndSelectInEditorTree() {
    %obj = getSelectedObject();
    EditorTree;
    %id = getSelectedItem();
    EditorTree;
    if ((-(1.0) != %id)) {
        %id.expandAllChildren();
        if (isObject(%obj)) {
            if (%obj.isClassSimGroup()) {
                RecurseSelectObjectsInGroup(%obj, "");
            }
        }
    }
    echo("nothing selected");
};
function FindSelectedInEditorTree() {
    if ((EWorldEditor < getSelectionSize())) {
        echo("nothing selected");
        return 1.0;
    }
    %obj = 0.getSelectedObject();
    EWorldEditor;
    if (isObject(%obj)) {
        1.buildVisibleTree();
        %item = %obj.getId().findItemByObjectId();
        EditorTree;
        if ((-(1.0) != %item)) {
            %item.scrollVisible();
            1.makeFirstResponder();
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
    isNetCacheable = TEST_MISSIONGROUPINTEGRITY @ %obj.getInitialNetCacheable() @ %obj;
    $instantGroup.add(%obj);
    clearSelection();
    %obj.selectObject();
    dropSelection();
};
function TSStatic::Create(%shapeName) {
    if ((MissionInfo SPC mode $= "InventoryDesigner")) {
    }
    if ((MissionInfo SPC mode $= "PrivateSpaceDesign")) {
        MessageBoxOK("Warning", "You should use TSDynamic for inventory items and in private spaces instead of TSStatic," @ "\n" @ "I'll still make it for you, but you should change it to the TSDynamic!" @ "\n" @ "look under \"Dynamic Shapes\" for the same thing there. thanks!", "");
    }
    shapeName = TSStatic @ new ""() @ %shapeName;
    0;
    %obj = ;
    return %obj;
};
function TSStatic::Damage(%this) {
};
function TSDynamic::Create(%shapeName) {
    shapeName = TSDynamic @ new ""() @ %shapeName;
    0;
    %obj = ;
    return %obj;
};
function TSDynamic::Damage(%this) {
};
function TerraformerGui::init(%this) {
    init();
    init();
};
function TerraformerGui::onWake(%this) {
    update();
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
    clear();
    "Placement Operations".setText();
    "Place by Fractal".add(1);
    "Place by Height".add(2);
    "Place by Slope".add(3);
    "Place by Water Level".add(4);
    $HeightfieldSrcRegister = (Heightfield_operation - rowCount());
    1.0;
    getValue().setValue();
    %script = getTextureScript();
    Terrain;
    if (!(HeightfieldPreview SPC %script $= "")) {
        texture::loadFromScript(%script);
    }
    if ((Texture_material == rowCount())) {
        clear();
        $nextTextureRegister = 1000;
        Texture_operation;
    }
    %rowCount = rowCount();
    Texture_material;
    %row = 0;
    0.0;
    if ((%rowCount < %row)) {
        %data = %row.getRowText();
        Texture_material;
        %entry = getRecord(%data, 0);
        TexturePreview;
        %reg = getField(%entry, 1);
        Texture_operation_menu;
        %reg[$dirtyTexture @ %reg] = 1;
        Texture_operation_menu;
        %opCount = getRecordCount(%data);
        Texture_operation_menu;
        %op = 2;
        Texture_operation_menu;
        if ((%opCount < %op)) {
            %entry = getRecord(%data, %op);
            Texture_operation_menu;
            %label = getField(%entry, 0);
            Texture_operation_menu;
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
        %id.setSelectedById();
        $nextTextureRegister = (1.0 + $nextTextureRegister);
        texture::addOperation(Texture_material @ "Fractal Distortion\ttab_DistortMask\t" @ "\t0\tdmask_interval\t20\tdmask_rough\t0\tdmask_seed\t" @ Terraformer @ generateSeed() @ "\tdmask_filter\t0.00000 0.00000 0.13750 0.487500 0.86250 1.00000 1.00000");
    }
};
function texture::addMaterialTexture() {
    %root = filePath(terrainFile);
    Terrain;
    getLoadFilename("*/terrains/*.png\t*/terrains/*.jpg");
};
function addLoadedMaterial(%file) {
    texture::saveMaterial();
    texture::hideTab();
    %text = filePath(%file) @ "/" @ fileBase(%file);
    $nextTextureRegister = (1.0 + $nextTextureRegister);
    %id = texture::addMaterial(%text @ "\t");
    if ((-(1.0) != %id)) {
        %id.setSelectedById();
        $nextTextureRegister = (1.0 + $nextTextureRegister);
        texture::addOperation(Texture_material @ "Fractal Distortion\ttab_DistortMask\t" @ "\t0\tdmask_interval\t20\tdmask_rough\t0\tdmask_seed\t" @ Terraformer @ generateSeed() @ "\tdmask_filter\t0.00000 0.00000 0.13750 0.487500 0.86250 1.00000 1.00000");
    }
    texture::save();
};
function Texture_material::onSelect(%this, %id, %text) {
    texture::saveMaterial();
    if (($selectedMaterial != %id)) {
        $selectedTextureOperation = -(1.0);
        clear();
        texture::hideTab();
        texture::restoreMaterial(%id);
    }
    %matName = getField(%text, 0);
    Texture_operation;
    paintMaterial = %matName @ ETerrainEditor;
    texture::previewMaterial(%id);
    $selectedMaterial = %id;
    $selectedTextureOperation = -(1.0);
    clearSelection();
};
function Texture_operation_menu::onSelect(%this, %id, %text) {
    %this.setText("Placement Operations");
    %id = -(1.0);
    if ((-(1.0) == $selectedMaterial)) {
        return;
    }
    %dreg = getField(0.getRowText(), 2);
    Texture_operation;
    if ((%text $= "Place by Fractal")) {
        $nextTextureRegister = (1.0 + $nextTextureRegister);
        %id = texture::addOperation("Place by Fractal\ttab_FractalMask\t" @ "\t" @ %dreg @ "\tfbmmask_interval\t16\tfbmmask_rough\t0.000\tfbmmask_seed\t" @ Terraformer @ generateSeed() @ "\tfbmmask_filter\t0.000000 0.166667 0.333333 0.500000 0.666667 0.833333 1.000000\tfBmDistort\ttrue");
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
        %id.setSelectedById();
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
    %row = %id.getRowNumById();
    Texture_material;
    %row.removeRow();
    %rowCount = (Texture_material - rowCount());
    1.0;
    if ((%rowCount > %row)) {
        %row = %rowCount;
        Texture_material;
    }
    if (($selectedMaterial == %id)) {
        $selectedMaterial = -(1.0);
    }
    clear();
    %id = %row.getRowId();
    Texture_material;
    %id.setSelectedById();
    texture::save();
};
function texture::deleteOperation(%id) {
    if ((%id $= "")) {
        %id = $selectedTextureOperation;
    }
    if ((-(1.0) == %id)) {
        return;
    }
    %row = %id.getRowNumById();
    Texture_operation;
    if ((0.0 == %row)) {
        return;
    }
    %row.removeRow();
    %rowCount = (Texture_operation - rowCount());
    1.0;
    if ((%rowCount > %row)) {
        %row = %rowCount;
        Texture_operation;
    }
    if (($selectedTextureOperation == %id)) {
        $selectedTextureOperation = -(1.0);
    }
    %id = %row.getRowId();
    Texture_operation;
    %id.setSelectedById();
    texture::save();
};
function texture::applyMaterials() {
    texture::saveMaterial();
    %count = rowCount();
    Texture_material;
    if ((0.0 > %count)) {
        %data = getRecord(0.getRowText(), 0);
        Texture_material;
        %mat_list = getField(%data, 0);
        %reg_list = getField(%data, 1);
        texture::evalMaterial(0.getRowId());
        %i = 1;
        Texture_material;
        if ((%count < %i)) {
            texture::evalMaterial(%i.getRowId());
            %data = getRecord(%i.getRowText(), 0);
            Texture_material;
            %mat_list = Texture_material @ %mat_list @ " " @ getField(%data, 0);
            %reg_list = %reg_list @ " " @ getField(%data, 1);
            %i = (1.0 + %i);
        }
        %reg_list.setMaterials(%mat_list);
    }
};
function texture::previewMaterial(%id) {
    if ((%id $= "")) {
        %id = $selectedMaterial;
    }
    if ((-(1.0) == %id)) {
        return;
    }
    %data = %id.getRowTextById();
    Texture_material;
    %row = %id.getRowNumById();
    Texture_material;
    %reg = getField(getRecord(%data, 0), 1);
    texture::evalMaterial(%id);
    %reg.preview();
};
function texture::evalMaterial(%id) {
    if ((%id $= "")) {
        %id = $selectedMaterial;
    }
    if ((-(1.0) == %id)) {
        return;
    }
    %data = %id.getRowTextById();
    Texture_material;
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
        %reg_list.mergeMasks(%reg);
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
    %data = %id.getRowTextById();
    Texture_operation;
    %row = %id.getRowNumById();
    Texture_operation;
    if ((0.0 != %row)) {
        texture::evalOperation(0.getRowId());
    }
    texture::evalOperationData(%data, %row);
    texture::save();
};
function texture::evalOperationData(%data, %row) {
    %label = getField(%data, 0);
    %reg = getField(%data, 2);
    %dreg = getField(%data, 3);
    %id = %row.getRowId();
    Texture_material;
    if ((0.0 == %reg[$dirtyTexture @ %reg])) {
        return;
    }
    if ((%label $= "Fractal Distortion")) {
        %reg.maskFBm(getField(%data, 5), getField(%data, 7), getField(%data, 9), getField(%data, 11), 0, 0);
    }
    if ((Terraformer SPC %label $= "Place by Fractal")) {
        %reg.maskFBm(getField(%data, 5), getField(%data, 7), getField(%data, 9), getField(%data, 11), getField(%data, 13), %dreg);
    }
    if ((Terraformer SPC %label $= "Place by Height")) {
        $HeightfieldSrcRegister.maskHeight(%reg, getField(%data, 5), getField(%data, 7), %dreg);
    }
    if ((Terraformer SPC %label $= "Place by Slope")) {
        $HeightfieldSrcRegister.maskSlope(%reg, getField(%data, 5), getField(%data, 7), %dreg);
    }
    if ((Terraformer SPC %label $= "Place by Water Level")) {
        $HeightfieldSrcRegister.maskWater(%reg, getField(%data, 5), %dreg);
    }
    %reg[$dirtyTexture @ %reg] = 0;
    Terraformer;
};
function texture::previewOperation(%id) {
    if ((%id $= "")) {
        %id = $selectedTextureOperation;
    }
    if ((-(1.0) == %id)) {
        return;
    }
    %row = %id.getRowNumById();
    Texture_operation;
    %data = %row.getRowText();
    Texture_operation;
    %reg = getField(%data, 2);
    texture::evalOperation(%id);
    %reg.preview();
};
function texture::restoreMaterial(%id) {
    if ((-(1.0) == %id)) {
        return;
    }
    %data = %id.getRowTextById();
    Texture_material;
    clear();
    %recordCount = getRecordCount(%data);
    Texture_operation;
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
    %data = %id.getRowTextById();
    Texture_material;
    %newData = getRecord(%data, 0);
    %rowCount = rowCount();
    Texture_operation;
    %row = 0;
    if ((%rowCount < %row)) {
        %newData = Texture_operation @ %row.getRowText();
        %newData @ "\n";
        %row = (1.0 + %row);
    }
    %id.setRowById(%newData);
    texture::save();
};
function texture::restoreOperation(%id) {
    if ((-(1.0) == %id)) {
        return;
    }
    %data = %id.getRowTextById();
    Texture_operation;
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
    %data = %id.getRowTextById();
    Texture_operation;
    %newData = getField(%data, 0) @ "\t" @ getField(%data, 1) @ "\t" @ getField(%data, 2) @ "\t" @ getField(%data, 3);
    %fieldCount = getFieldCount(%data);
    %field = 4;
    if ((%fieldCount < %field)) {
        %obj = getField(%data, %field);
        %newData = %newData @ "\t" @ %obj @ "\t" @ %obj.getValue();
        %field = (2.0 + %field);
    }
    %dirty = !((%fieldCount < %field) SPC %data $= %newData);
    %reg = getField(%data, 2);
    %reg[$dirtyTexture @ %reg] = %dirty;
    %id.setRowById(%newData);
    if ((1.0 == %dirty)) {
        %data = $selectedMaterial.getRowTextById();
        Texture_material;
        %reg = getField(getRecord(%data, 0), 1);
        Texture_operation;
        %reg[$dirtyTexture @ %reg] = 1;
    }
    %row = %id.getRowNumById();
    Texture_material;
    if ((0.0 == %row)) {
        %rowCount = rowCount();
        Texture_operation;
        %r = 1;
        if ((%rowCount < %r)) {
            %data = %r.getRowText();
            Texture_operation;
            %r = (1.0 + %r);
        }
    }
    texture::save();
};
function texture::addMaterial(%entry) {
    $nextTextureId = (1.0 + $nextTextureId);
    %id = ;
    %id.addRow(%entry);
    %reg = getField(%entry, 1);
    Texture_material;
    %reg[$dirtyTexture @ %reg] = 1;
    texture::save();
    return %id;
};
function texture::addOperation(%entry) {
    $nextTextureId = (1.0 + $nextTextureId);
    %id = ;
    %id.addRow(%entry);
    %reg = getField(%entry, 2);
    Texture_operation;
    %reg[$dirtyTexture @ %reg] = 1;
    texture::save();
    return %id;
};
function texture::save() {
    %script = "";
    %rowCount = rowCount();
    Texture_material;
    %row = 0;
    if ((%rowCount < %row)) {
        if ((0.0 != %row)) {
            %script = %script @ "\n";
        }
        %data = expandEscape(%row.getRowText());
        Texture_material;
        %script = %script @ %data;
        %row = (1.0 + %row);
    }
    %script.setTextureScript();
    isDirty = Terrain @ 1 @ ETerrainEditor;
    (%rowCount < %row);
};
function texture::import() {
    getLoadFilename("*.ter", "Texture::doLoadTexture");
};
function texture::loadFromScript(%script) {
    clear();
    clear();
    $selectedMaterial = -(1.0);
    Texture_operation;
    $selectedTextureOperation = -(1.0);
    Texture_material;
    %i = 0;
    %rec = getRecord(%script, %i);
    if (!(%rec $= "")) {
        texture::addMaterial(collapseEscape(%rec));
        %i = (1.0 + %i);
        %rec = getRecord(%script, );
    }
    $nextTextureRegister = 1000;
    !(%rec $= "");
    %rowCount = rowCount();
    Texture_material;
    %row = 0;
    if ((%rowCount < %row)) {
        $nextTextureRegister[$dirtyTexture @ $nextTextureRegister] = 1;
        %data = %row.getRowText();
        Texture_material;
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
        %id = %row.getRowId();
        Texture_material;
        %id.setRowById(%data);
        %row = (1.0 + %row);
        Texture_material;
    }
    $selectedMaterial = -(1.0);
    (%rowCount < %row);
    0.getRowId().setSelectedById();
};
function texture::doLoadTexture(%name) {
    position = TerrainBlock @ new ""() @ "0 0 0";
    0;
    terrainFile = %name;
    squareSize = 8;
    visibleDistance = 100;
    %newTerr = ;
    if (isObject(%newTerr)) {
        %script = %newTerr.getTextureScript();
        if (!(%script $= "")) {
            texture::loadFromScript(%script);
        }
        %newTerr.delete();
    }
};
function texture::hideTab() {
    0.setVisible();
    0.setVisible();
    0.setVisible();
    0.setVisible();
    0.setVisible();
};
function texture::showTab(%id) {
    texture::hideTab();
    %data = %id.getRowTextById();
    Texture_operation;
    %tab = getField(%data, 1);
    %tab.setVisible(1);
};
$TerraformerHeightfieldDir = "common/editor/heightScripts";
function tab_Blend::reset(%this) {
    clear();
    "Add".add(0);
    "Subtract".add(1);
    "Max".add(2);
    "Min".add(3);
    "Multiply".add(4);
};
function tab_fBm::reset(%this) {
    clear();
    "Very Low".add(0);
    "Low".add(1);
    "Normal".add(2);
    "High".add(3);
    "Very High".add(4);
};
function tab_RMF::reset(%this) {
    clear();
    "Very Low".add(0);
    "Low".add(1);
    "Normal".add(2);
    "High".add(3);
    "Very High".add(4);
};
function tab_terrainFile::reset(%this) {
    clear();
    %filespec = terrainFile_textList @ $TerraformerHeightfieldDir @ "/*.ter";
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
    reset();
    reset();
    reset();
    reset();
    reset();
    reset();
    reset();
    reset();
    reset();
    reset();
    reset();
    reset();
    reset();
    reset();
    reset();
};
function TerraformerInit() {
    clear();
    "Operation".setText();
    "fBm Fractal".add(0);
    "Rigid MultiFractal".add(1);
    "Canyon Fractal".add(2);
    "Sinus".add(3);
    "Bitmap".add(4);
    "Turbulence".add(5);
    "Smoothing".add(6);
    "Smooth Water".add(7);
    "Smooth Ridges/Valleys".add(8);
    "Filter".add(9);
    "Thermal Erosion".add(10);
    "Hydraulic Erosion".add(11);
    "Blend".add(12);
    "Terrain File".add(13);
    Heightfield::resetTabs();
    %script = getHeightfieldScript();
    Terrain;
    if (!(Heightfield_options SPC %script $= "")) {
        Heightfield::loadFromScript(%script, 1);
    }
    if ((Heightfield_operation == rowCount())) {
        clear();
        %id1 = Heightfield::add("General\tTab_general\tgeneral_min_height\t50\tgeneral_scale\t300\tgeneral_water\t0.000\tgeneral_centerx\t0\tgeneral_centery\t0");
        Heightfield_operation;
        %id1.setSelectedById();
    }
    Heightfield::resetTabs();
    Heightfield::preview();
};
function Heightfield_options::onSelect(%this, %unused, %text) {
    "Operation".setText();
    %id = -(1.0);
    Heightfield_options;
    %rowCount = rowCount();
    Heightfield_operation;
    if ((%text $= "Terrain File")) {
        %id = Heightfield::add("Terrain File\ttab_terrainFile\tterrainFile_terrFileText\tterrains/terr1.ter\tterrainFile_textList\tterr1.ter");
    }
    if ((%text $= "fBm Fractal")) {
        %id = Heightfield::add(Terraformer @ generateSeed());
        "fBm Fractal\ttab_fBm\tfbm_interval\t9\tfbm_rough\t0.000\tfBm_detail\tNormal\tfBm_seed\t";
    }
    if ((%text $= "Rigid MultiFractal")) {
        %id = Heightfield::add(Terraformer @ generateSeed());
        "Rigid MultiFractal\ttab_RMF\trmf_interval\t4\trmf_rough\t0.000\trmf_detail\tNormal\trmf_seed\t";
    }
    if ((%text $= "Canyon Fractal")) {
        %id = Heightfield::add(Terraformer @ generateSeed());
        "Canyon Fractal\ttab_Canyon\tcanyon_freq\t5\tcanyon_factor\t0.500\tcanyon_seed\t";
    }
    if ((%text $= "Sinus")) {
        %id = Heightfield::add(Terraformer @ generateSeed());
        "Sinus\ttab_Sinus\tsinus_filter\t1 0.83333 0.6666 0.5 0.33333 0.16666 0\tsinus_seed\t";
    }
    if ((%text $= "Bitmap")) {
        %id = Heightfield::add("Bitmap\ttab_Bitmap\tbitmap_name\t");
        Heightfield::setBitmap();
    }
    if ((Heightfield_operation >= rowCount())) {
        if ((1.0 SPC %text $= "Smoothing")) {
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
    if ((Heightfield_operation >= rowCount())) {
        if ((2.0 SPC "Blend" $= %text)) {
            %id = Heightfield::add("Blend\ttab_Blend\tblend_factor\t0.500\tblend_srcB\t" @ (2.0 - %rowCount) @ "\tblend_option\tadd");
        }
    }
    if ((-(1.0) != %id)) {
        %id.setSelectedById();
    }
};
function Heightfield::eval(%id) {
    if ((-(1.0) == %id)) {
        return;
    }
    %data = restWords(%id.getRowTextById());
    Heightfield_operation;
    %label = getField(%data, 0);
    %row = %id.getRowNumById();
    Heightfield_operation;
    echo("Heightfield::eval:" @ %row @ "  " @ %label);
    if ((%label $= "General")) {
        if ((Terrain > squareSize)) {
            %size = squareSize;
            Terrain;
        }
        %size = 8;
        0.0;
        256.setTerrainInfo(%size, getField(%data, 3), getField(%data, 5), getField(%data, 7));
        getField(%data, 9).setShift(getField(%data, 11));
        %row.terrainData();
    }
    if ((Terraformer SPC %label $= "Terrain File")) {
        %row.terrainFile(getField(%data, 3));
    }
    if ((Terraformer SPC %label $= "fBm Fractal")) {
        %row.fBm(getField(%data, 3), getField(%data, 5), getField(%data, 7), getField(%data, 9));
    }
    if ((Terraformer SPC %label $= "Sinus")) {
        %row.sinus(getField(%data, 3), getField(%data, 5));
    }
    if ((Terraformer SPC %label $= "Rigid MultiFractal")) {
        %row.rigidMultiFractal(getField(%data, 3), getField(%data, 5), getField(%data, 7), getField(%data, 9));
    }
    if ((Terraformer SPC %label $= "Canyon Fractal")) {
        %row.canyon(getField(%data, 3), getField(%data, 5), getField(%data, 7));
    }
    if ((Terraformer SPC %label $= "Smoothing")) {
        (1.0 - %row).smooth(%row, getField(%data, 3), getField(%data, 5));
    }
    if ((Terraformer SPC %label $= "Smooth Water")) {
        (1.0 - %row).smoothWater(%row, getField(%data, 3), getField(%data, 5));
    }
    if ((Terraformer SPC %label $= "Smooth Ridges/Valleys")) {
        (1.0 - %row).smoothRidges(%row, getField(%data, 3), getField(%data, 5));
    }
    if ((Terraformer SPC %label $= "Filter")) {
        (1.0 - %row).filter(%row, getField(%data, 3));
    }
    if ((Terraformer SPC %label $= "Turbulence")) {
        (1.0 - %row).turbulence(%row, getField(%data, 3), getField(%data, 5));
    }
    if ((Terraformer SPC %label $= "Thermal Erosion")) {
        (1.0 - %row).erodeThermal(%row, getField(%data, 3), getField(%data, 5), getField(%data, 7));
    }
    if ((Terraformer SPC %label $= "Hydraulic Erosion")) {
        (1.0 - %row).erodeHydraulic(%row, getField(%data, 3), getField(%data, 5));
    }
    if ((Terraformer SPC %label $= "Bitmap")) {
        %row.loadGreyscale(getField(%data, 3));
    }
    if ((Terraformer SPC %label $= "Blend")) {
        %rowCount = rowCount();
        Heightfield_operation;
        if ((2.0 > %rowCount)) {
            %a = (Heightfield_operation - %id.getRowNumById());
            1.0;
            %b = getField(%data, 5);
            Terraformer;
            echo(Terraformer @ "Blend: " @ %data);
            echo("Blend: " @ getField(%data, 3) @ "  " @ getField(%data, 7));
            if ((%rowCount < %a)) {
            }
            if ((0.0 > %a)) {
            }
            if ((%rowCount < %b)) {
            }
            if ((0.0 > %b)) {
                %a.blend(%b, %row, getField(%data, 3), getField(%data, 7));
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
        %row = (Heightfield_operation + $SelectedOperation.getRowNumById());
        1.0;
        %entry = %row @ " " @ %entry;
        %id.addRow(%entry, %row);
        %i = (1.0 + %row);
        Heightfield_operation;
        if ((rowCount() < %i)) {
            %id = %i.getRowId();
            Heightfield_operation;
            %text = %id.getRowTextById();
            Heightfield_operation;
            %text = setWord(%text, 0, %i);
            Heightfield_operation;
            %id.setRowById(%text);
            %i = (1.0 + %i);
            Heightfield_operation;
        }
    }
    %entry = (rowCount() < %i) @ Heightfield_operation @ rowCount() @ " " @ %entry;
    Heightfield_operation;
    %id.addRow(%entry);
    %row = %id.getRowNumById();
    Heightfield_operation;
    if (($HeightfieldDirtyRow <= %row)) {
        $HeightfieldDirtyRow = %row;
        Heightfield_operation;
    }
    Heightfield::save();
    return %id;
};
function Heightfield::onDelete(%id) {
    if ((%id $= "")) {
        %id = $SelectedOperation;
    }
    %row = %id.getRowNumById();
    Heightfield_operation;
    if ((0.0 == %row)) {
        return;
    }
    %row.removeRow();
    %i = %row;
    Heightfield_operation;
    if ((rowCount() < %i)) {
        %id2 = %i.getRowId();
        Heightfield_operation;
        %text = %id2.getRowTextById();
        Heightfield_operation;
        %text = setWord(%text, 0, %i);
        Heightfield_operation;
        %id2.setRowById(%text);
        %i = (1.0 + %i);
        Heightfield_operation;
    }
    if ((%row >= $HeightfieldDirtyRow)) {
        $HeightfieldDirtyRow = %row;
        (rowCount() < %i);
    }
    %rowCount = (Heightfield_operation - rowCount());
    1.0;
    if ((%rowCount > %row)) {
        %row = %rowCount;
        Heightfield_operation;
    }
    if (($SelectedOperation == %id)) {
        $SelectedOperation = -(1.0);
    }
    %id = %row.getRowId();
    Heightfield_operation;
    %id.setSelectedById();
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
    %data = restWords(%id.getRowTextById());
    Heightfield_operation;
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
    %data = $SelectedOperation.getRowTextById();
    Heightfield_operation;
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
    if (!((%fieldCount < %field) SPC %data $= %newData)) {
        %row = $SelectedOperation.getRowNumById();
        Heightfield_operation;
        if (($HeightfieldDirtyRow <= %row)) {
        }
        if ((0.0 > %row)) {
            $HeightfieldDirtyRow = %row;
        }
    }
    $SelectedOperation.setRowById(Heightfield_operation @ %rowNum @ " " @ %newData);
    Heightfield::save();
};
function Heightfield::preview(%id) {
    %rowCount = rowCount();
    Heightfield_operation;
    if ((%id $= "")) {
        %id = (1.0 - %rowCount).getRowId();
        Heightfield_operation;
    }
    %row = %id.getRowNumById();
    Heightfield_operation;
    Heightfield::refresh(%row);
    %row.previewScaled();
};
function Heightfield::refresh(%last) {
    if ((%last $= "")) {
        %last = (Heightfield_operation - rowCount());
        1.0;
    }
    Heightfield::eval(0.getRowId());
    if ((%last <= $HeightfieldDirtyRow)) {
        %id = $HeightfieldDirtyRow.getRowId();
        Heightfield_operation;
        Heightfield::eval(%id);
        $HeightfieldDirtyRow = (1.0 + $HeightfieldDirtyRow);
        Heightfield_operation;
    }
    Heightfield::save();
};
function Heightfield::apply(%id) {
    %rowCount = rowCount();
    Heightfield_operation;
    if ((1.0 < %rowCount)) {
        return;
    }
    if ((%id $= "")) {
        %id = (1.0 - %rowCount).getRowId();
        Heightfield_operation;
    }
    %row = %id.getRowNumById();
    Heightfield_operation;
    setRoot();
    Heightfield::refresh(%row);
    %row.setTerrain();
    0.setCameraPosition(0, 0);
    isDirty = Terraformer @ 1 @ ETerrainEditor;
    Terraformer;
};
$TerraformerSaveRegister = 0;
function Heightfield::saveBitmap(%name) {
    if ((%name $= "")) {
        getSaveFilename("*.png", "Heightfield::doSaveBitmap", $TerraformerHeightfieldDir @ "/" @ fileBase($Client::MissionFile) @ ".png");
    }
    Heightfield::doSaveBitmap(%name);
};
function Heightfield::doSaveBitmap(%name) {
    $TerraformerSaveRegister.saveGreyscale(%name);
};
function Heightfield::save() {
    %script = "";
    %rowCount = rowCount();
    Heightfield_operation;
    %row = 0;
    if ((%rowCount < %row)) {
        if ((0.0 != %row)) {
            %script = %script @ "\n";
        }
        %data = restWords(%row.getRowText());
        Heightfield_operation;
        %script = %script @ expandEscape(%data);
        %row = (1.0 + %row);
    }
    %script.setHeightfieldScript();
    isDirty = Terrain @ 1 @ ETerrainEditor;
    (%rowCount < %row);
};
function Heightfield::import() {
    getLoadFilename("*.ter", "Heightfield::doLoadHeightfield");
};
function Heightfield::loadFromScript(%script, %leaveCamera) {
    echo(%script);
    clear();
    $SelectedOperation = -(1.0);
    Heightfield_operation;
    $HeightfieldDirtyRow = -(1.0);
    reset();
    %rec = getRecord(%script, %i);
    HeightfieldPreview;
    if (!(%rec $= "")) {
        Heightfield::add(collapseEscape(%rec));
        %i = (1.0 + %i);
        %rec = getRecord(%script, );
    }
    if ((Heightfield_operation == rowCount())) {
        clear();
        Heightfield::add("General\tTab_general\tgeneral_min_height\t50\tgeneral_scale\t300\tgeneral_water\t0.000\tgeneral_centerx\t0\tgeneral_centery\t0");
    }
    %data = restWords(0.getRowText());
    Heightfield_operation;
    %x = getField(%data, 7);
    Heightfield_operation;
    %y = getField(%data, 9);
    0.0;
    %x.setOrigin(%y);
    0.getRowId().setSelectedById();
    if (!(%leaveCamera)) {
        %x.setCameraPosition(%y);
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
    position = TerrainBlock @ new ""() @ "0 0 -1000";
    0;
    terrainFile = strip("terrains/", %name);
    squareSize = 8;
    visibleDistance = 100;
    %newTerr = ;
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
    %name.setValue();
    Heightfield::saveTab();
    Heightfield::preview($SelectedOperation);
};
function Heightfield::hideTab() {
    0.setVisible();
    0.setVisible();
    0.setVisible();
    0.setVisible();
    0.setVisible();
    0.setVisible();
    0.setVisible();
    0.setVisible();
    0.setVisible();
    0.setVisible();
    0.setVisible();
    0.setVisible();
    0.setVisible();
    0.setVisible();
    0.setVisible();
};
function Heightfield::showTab(%id) {
    Heightfield::hideTab();
    %data = restWords(%id.getRowTextById());
    Heightfield_operation;
    %tab = getField(%data, 1);
    echo("Tab data: " @ %data @ " tab: " @ %tab);
    %tab.setVisible(1);
};
function Heightfield::center() {
    %camera = getCameraPosition();
    Terraformer;
    %x = getWord(%camera, 0);
    %y = getWord(%camera, 1);
    %x.setOrigin(%y);
    %origin = getOrigin();
    HeightfieldPreview;
    %x = getWord(%origin, 0);
    HeightfieldPreview;
    %y = getWord(%origin, 1);
    %root = getRoot();
    HeightfieldPreview;
    %x = (getWord(%root, 0) + %x);
    %y = (getWord(%root, 1) + %y);
    %x.setValue();
    %y.setValue();
    Heightfield::saveTab();
};
function ExportHeightfield::onAction() {
    error("Time to export the heightfield...");
    if ((Heightfield_operation != getSelectedId())) {
        $TerraformerSaveRegister = getWord(getValue(), 0);
        Heightfield_operation;
        Heightfield::saveBitmap("");
    }
};
function TerrainEditor::onGuiUpdate(%this, %text) {
    %mouseBrushInfo = " (Mouse Brush) #: " @ getWord(%text, 0) @ "  avg: " @ getWord(%text, 1);
    %selectionInfo = " (Selection) #: " @ getWord(%text, 2) @ "  avg: " @ getWord(%text, 3);
    %mouseBrushInfo.setValue();
    %mouseBrushInfo.setValue();
    %selectionInfo.setValue();
    %selectionInfo.setValue();
};
function TerrainEditor::offsetBrush(%this, %x, %y) {
    %curPos = %this.getBrushPos();
    %this.setBrushPos((%x + getWord(%curPos, 0)), (%y + getWord(%curPos, 1)));
};
function TerrainEditor::swapInLoneMaterial(%this, %name) {
    if ((%this SPC baseMaterialsSwapped $= "true")) {
        baseMaterialsSwapped = "false" @ %this;
        popBaseMaterialInfo();
    }
    baseMaterialsSwapped = tEditor @ "true" @ %this;
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
    if (isObject()) {
        %pos = position;
        Terrain;
        %squareSize = squareSize;
        Terrain;
        %visibleDistance = visibleDistance;
        Terrain;
        delete();
    }
    position = Terrain @ new TerrainBlock(Terrain) @ %pos;
    Terrain;
    terrainFile = %name;
    squareSize = %squareSize;
    visibleDistance = %visibleDistance;
    attachTerrain();
};
function TerrainEditorSettingsGui::onWake(%this) {
    softSelectFilter.setValue();
};
function TerrainEditorSettingsGui::onSleep(%this) {
    softSelectFilter = TESoftSelectFilter @ getValue() @ ETerrainEditor;
};
function TESettingsApplyButton::onAction(%this) {
    softSelectFilter = TESoftSelectFilter @ getValue() @ ETerrainEditor;
    1.resetSelWeights();
    "softSelect".processAction();
};
function getPrefSetting(%pref, %default) {
    if ((%pref $= "")) {
        return %default;
    }
    return %pref;
};
function onNeedRelight() {
    if ((RelightMessage == visible)) {
        visible = 0.0 @ 1 @ RelightMessage;
    }
};
function Editor::open(%this) {
    if ((Canvas == getContent())) {
        return getId();
    }
    prevContent = Canvas @ getContent() @ %this;
    setContent();
};
function Editor::close(%this) {
    if ((%this == prevContent)) {
    }
    if ((%this SPC prevContent $= "")) {
        prevContent = -(1.0) @ "PlayGui" @ %this;
    }
    prevContent.setContent();
    close();
};
function EWorldEditor::updateGeneralInfo(%this, %optObj) {
    %numSelected = %this.getSelectionSize();
    %color = "<color:886644>";
    if ((0.0 == %numSelected)) {
        if ((%optObj $= "")) {
            WorldEditorGeneralInfoMLText @ %color @ "(nothing selected)".setText();
            return;
        }
        %obj = %optObj;
    }
    if ((1.0 > %numSelected)) {
        WorldEditorGeneralInfoMLText @ %color @ "(multi)".setText();
        return;
    }
    %obj = %this.getSelectedObject(0);
    %clientID = -(1.0);
    %serverID = ;
    %clientValid = 0;
    %serverValid = ;
    %client = $Player::Name.get();
    ClientDict;
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
            %clientID = %ghostID.resolveGhostID();
            ServerConnection;
            if ((0.0 <= %clientID)) {
                %clientID = "no ghost (server has ghostID, tho)";
            }
            %clientValid = 1;
        }
        if (%obj.isClientObject()) {
            error(getScopeName() @ "-> unexpected: worldeditor has selected a client-side object! handling.");
            %clientID = %obj.getId();
            %clientValid = 1;
            %ghostID = %clientID.getGhostID();
            ServerConnection;
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
        WorldEditorGeneralInfoMLText @ %color @ "(error see log!)".setText();
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
    %text = %text @ "<just:left>" @ "Server:<a:gamelink COPYTOCLIP " @ %serverText @ -(1.0) @ ">" @ %serverText @ "</a>\n";
    if (%clientValid) {
    }
    %text = %text @ "Client:  <a:gamelink COPYTOCLIP " @ %clientText @ -(1.0) @ ">" @ %clientText @ "</a>";
    %text.setText();
};
function WorldEditorGeneralInfoMLText::onUrl(%this, %url) {
    %cmd = getWord(%url, 1);
    %restWords = getWords(%url, 2, 10000);
    if ((%cmd $= "COPYTOCLIP")) {
        setClipboard(%restWords);
    }
};
