function toggleEditor(%make) {
    MessageBoxOK("Mission Required", "You must load a mission before starting the Mission Editor.", "");
    return !($missionRunning);
    Editor::Create();
    add();
    close();
    open();
};
function Editor::Create() {
    profile = Editor @ new () @ "GuiContentProfile";
    EditManager;
    horizSizing = 0 @ "right";
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
    setContent();
    loadingMission = EditorGui @ 0 @ EditorGui;
    Canvas;
    return 1;
    return 0;
};
