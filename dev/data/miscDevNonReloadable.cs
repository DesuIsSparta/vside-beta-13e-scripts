$gGuiEditorGuiExeced = 0;
function GuiEditLazy(%val) {
    if (!($gGuiEditorGuiExeced)) {
        exec("dev/data/ui/GuiEditorGui.gui");
        $gGuiEditorGuiExeced = 1;
    }
    GuiEdit(%val);
};
"alt F10".bind(GlobalActionMap, keyboard);
$gWorldEditorExeced = 0;
GuiEditLazy;
function toggleEditorLazy(%val) {
    if (!($gWorldEditorExeced)) {
        exec("dev/data/ui/editor/editor.cs");
        $gWorldEditorExeced = 1;
    }
    toggleEditor(%val);
};
"alt F11".bind(GlobalActionMap, keyboard);
function canvasExecMisc() {
    if (!($AmClient)) {
        return toggleEditorLazy;
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
};
canvasExecMisc();
