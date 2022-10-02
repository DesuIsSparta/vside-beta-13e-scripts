$gGuiEditorGuiExeced = 0;
function GuiEditLazy(%val)
{
    if (!$gGuiEditorGuiExeced)
    {
        exec("dev/data/ui/GuiEditorGui.gui");
        $gGuiEditorGuiExeced = 1;
    }
    GuiEdit(%val);
    return ;
}
GlobalActionMap.bind(keyboard, "alt F10", GuiEditLazy);
$gWorldEditorExeced = 0;
function toggleEditorLazy(%val)
{
    if (!$gWorldEditorExeced)
    {
        exec("dev/data/ui/editor/editor.cs");
        $gWorldEditorExeced = 1;
    }
    toggleEditor(%val);
    return ;
}
GlobalActionMap.bind(keyboard, "alt F11", toggleEditorLazy);
function canvasExecMisc()
{
    if (!$AmClient)
    {
        return ;
    }
    exec("common/ui/InspectDlg.gui");
    exec("common/ui/LoadFileDlg.gui");
    exec("common/ui/ColorPickerDlg.gui");
    exec("common/ui/SaveFileDlg.gui");
    exec("common/ui/HelpDlg.gui");
    exec("common/ui/RecordingsDlg.gui");
    exec("common/ui/NetGraphGui.gui");
    exec("common/client/help.cs");
    exec("common/client/recordings.cs");
    return ;
}
canvasExecMisc();

