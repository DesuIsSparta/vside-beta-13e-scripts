function toggleEditor(%make) {
    if (%make) {
        if (!($missionRunning)) {
            MessageBoxOK("Mission Required", "You must load a mission before starting the Mission Editor.", "");
            return !($player.rolesPermissionCheckNoWarn("gameEditors"));
        }
        if (!(isObject())) {
            Editor::Create();
            add();
        }
        if ((Canvas == getContent())) {
            close();
        }
        open();
    }
};
function Editor::Create() {
    profile = new EditManager(Editor) @ "GuiContentProfile";
    horizSizing = "right";
    vertSizing = "top";
    position = "0 0";
    extent = "640 480";
    minExtent = "8 8";
    visible = 1;
    setFirstResponder = 0;
    modal = 1;
    helpTag = 0;
    open = 0;
};
function Editor::onAdd(%unused) {
    exec("./cursors.cs");
    exec("./editor.bind.cs");
    exec("./ObjectBuilderGui.gui");
    exec("./EditorGui.gui");
    exec("./EditorGui.cs");
    exec("./WorldEditorSettingsDlg.gui");
    exec("./TerrainEditorVSettingsGui.gui");
    "fxShapeReplicatedStatic".ignoreObjClass();
    init();
    exec("./editorRender.cs");
};
function Editor::checkActiveLoadDone() {
    if (isObject()) {
    }
    if (loadingMission) {
        setContent();
        loadingMission = EditorGui @ 0 @ EditorGui;
        Canvas;
        return 1;
    }
    return 0;
};
