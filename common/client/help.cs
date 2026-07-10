function HelpDlg::onWake(%this) {
    HelpFileList.entryCount = 0;
    HelpFileList.clear();
    %file = findFirstFile("*.hfl");
    while (!(%file $= "")) {
        HelpFileList.fileName = %file @ HelpFileList.entryCount;
        fileBase(%file).addRow(HelpFileList, HelpFileList.entryCount);
        HelpFileList.entryCount = (HelpFileList.entryCount + 1.0);
        %file = findNextFile("*.hfl");
    }
    0.sortNumerical(HelpFileList);
    %i = 0;
    while ((%i < HelpFileList.entryCount)) {
        %rowId = %i.getRowId(HelpFileList);
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
    %fo = new FileObject("");
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
        if ((%i.getObject(Canvas).getName() $= HelpDlg)) {
            HelpDlg.popDialog(Canvas);
            return;
        }
        %i = (%i + 1.0);
    }
    %content = Canvas.getContent();
    %helpPage = %content.getHelpPage();
    getHelp(%helpPage);
};
function GuiControl::getHelpPage(%this) {
    return %this.helpPage;
};
