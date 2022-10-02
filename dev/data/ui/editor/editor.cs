function toggleEditor(%make)
{
    if (!$player.rolesPermissionCheckNoWarn("gameEditors"))
    {
    }
    if (%make)
    {
        if (!$missionRunning)
        {
            MessageBoxOK("Mission Required", "You must load a mission before starting the Mission Editor.", "");
            return ;
        }
        if (!isObject(Editor))
        {
            Editor::Create();
            MissionCleanup.add(Editor);
        }
        if (Canvas.getContent() == EditorGui.getId())
        {
            Editor.close();
        }
        else
        {
            Editor.open();
        }
    }
    return ;
}
function Editor::Create()
{
    new EditManager(Editor)
    {
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
    return ;
}
function Editor::onAdd(%unused)
{
    exec("./cursors.cs");
    exec("./editor.bind.cs");
    exec("./ObjectBuilderGui.gui");
    exec("./EditorGui.gui");
    exec("./EditorGui.cs");
    exec("./WorldEditorSettingsDlg.gui");
    exec("./TerrainEditorVSettingsGui.gui");
    EWorldEditor.ignoreObjClass("fxShapeReplicatedStatic");
    EditorGui.init();
    exec("./editorRender.cs");
    return ;
}
function Editor::checkActiveLoadDone()
{
    if (isObject(EditorGui) && EditorGui.loadingMission)
    {
        Canvas.setContent(EditorGui);
        EditorGui.loadingMission = 0;
        return 1;
    }
    return 0;
}
