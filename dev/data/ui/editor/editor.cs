function toggleEditor(%make) {
    if (%make) {
        if (!($missionRunning)) {
            MessageBoxOK("Mission Required", "You must load a mission before starting the Mission Editor.", "");
            return !("gameEditors".rolesPermissionCheckNoWarn($player));
        }
        if (!(isObject(Editor))) {
            Editor::Create();
            Editor.add(MissionCleanup);
        }
        if ((Canvas.getContent() == EditorGui.getId())) {
            Editor.close();
        }
        Editor.open();
    }
};
function Editor::Create() {
    new EditManager(Editor) {
        profile = "GuiContentProfile";
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
};
function Editor::onAdd(%unused) {
    exec("./cursors.cs");
    exec("./editor.bind.cs");
    exec("./ObjectBuilderGui.gui");
    exec("./EditorGui.gui");
    exec("./EditorGui.cs");
    exec("./WorldEditorSettingsDlg.gui");
    exec("./TerrainEditorVSettingsGui.gui");
    "fxShapeReplicatedStatic".ignoreObjClass(EWorldEditor);
    EditorGui.init();
    exec("./editorRender.cs");
};
function Editor::checkActiveLoadDone() {
    if (isObject(EditorGui)) {
    }
    if (EditorGui.loadingMission) {
        EditorGui.setContent(Canvas);
        EditorGui.loadingMission = 0;
        return 1;
    }
    return 0;
};
