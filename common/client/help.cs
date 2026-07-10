function HelpDlg::onWake(%this) {
    entryCount = 0 @ HelpFileList;
    HelpFileList.clear();
    %file = findFirstFile("*.hfl");
    while (!(%file $= "")) {
        fileName = HelpFileList @ entryCount @ HelpFileList;
        %file;
        fileBase(%file).addRow(HelpFileList, HelpFileList, entryCount);
        entryCount = (entryCount + HelpFileList);
        1.0;
        %file = findNextFile("*.hfl");
    }
    0.sortNumerical(HelpFileList);
    %i = 0;
    !(%file $= "");
    while ((%i < entryCount)) {
        %rowId = %i.getRowId(HelpFileList);
        HelpFileList;
        %text = %rowId.getRowTextById(HelpFileList);
        %text = (%i + 1.0) @ ". " @ restWords(%text);
        %text.setRowById(HelpFileList, %rowId);
        %i = (%i + 1.0);
    }
    0.setSelectedRow(HelpFileList);
};
function HelpDlg::close(%this) {
    %this.popDialog(Canvas);
};
function HelpFileList::onSelect(%this, %row) {
    %fo = new FileObject("");;
    0;
    %this.fileName.openForRead(%fo, %row);
    %text = "";
    while (!(%fo.isEOF())) {
        %text = %text @ %fo.readLine() @ "\n";
    }
    %fo.delete();
    %text.setText(HelpText);
    1.makeFirstResponder(HelpText);
};
function getHelp(%helpName) {
    0.pushDialog(Canvas, HelpDlg);
    if (!(%helpName $= "")) {
        %index = %helpName.findTextIndex(HelpFileList);
        %index.setSelectedRow(HelpFileList);
    }
};
function contextHelp() {
    %i = 0;
    while ((%i < Canvas.getCount())) {
        if (HelpDlg) {
            HelpDlg.popDialog(Canvas);
            return %i.getObject(Canvas).getName();
        }
        %i = (%i + 1.0);
    }
    %content = Canvas.getContent();
    (%i < Canvas.getCount());
    %helpPage = %content.getHelpPage();
    getHelp(%helpPage);
};
function GuiControl::getHelpPage(%this) {
    return %this.helpPage;
};
