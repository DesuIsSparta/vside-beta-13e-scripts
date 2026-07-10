function HelpDlg::onWake(%this) {
    entryCount = 0 @ HelpFileList;
    HelpFileList.clear();
    %file = findFirstFile("*.hfl");
    if (!(%file $= "")) {
        fileName = HelpFileList @ entryCount @ HelpFileList;
        %file;
        entryCount.addRow(fileBase(%file));
        entryCount = (HelpFileList + entryCount);
        1.0;
        %file = findNextFile("*.hfl");
        HelpFileList;
    }
    0.sortNumerical();
    %i = 0;
    HelpFileList;
    if ((entryCount < %i)) {
        %rowId = %i.getRowId();
        HelpFileList;
        %text = %rowId.getRowTextById();
        HelpFileList;
        %text = (1.0 + %i) @ ". " @ restWords(%text);
        HelpFileList;
        %rowId.setRowById(%text);
        %i = (1.0 + %i);
        HelpFileList;
    }
    0.setSelectedRow();
};
function HelpDlg::close(%this) {
    %this.popDialog();
};
function HelpFileList::onSelect(%this, %row) {
    %fo = new ""();;
    FileObject;
    %fo.openForRead(%this.fileName);
    %text = "";
    0 @ %row;
    if (!(%fo.isEOF())) {
        %text = %text @ %fo.readLine() @ "\n";
    }
    %fo.delete();
    %text.setText();
    1.makeFirstResponder();
};
function getHelp(%helpName) {
    0.pushDialog();
    if (!(HelpDlg @ " " @ %helpName $= "")) {
        %index = %helpName.findTextIndex();
        HelpFileList;
        %index.setSelectedRow();
    }
};
function contextHelp() {
    %i = 0;
    if ((Canvas.getCount() < %i)) {
        if (HelpDlg) {
            Canvas.popDialog(HelpDlg);
            return Canvas @ " " @ %i.getObject().getName();
        }
        %i = (1.0 + %i);
    }
    %content = Canvas.getContent();
    (Canvas.getCount() < %i);
    %helpPage = %content.getHelpPage();
    getHelp(%helpPage);
};
function GuiControl::getHelpPage(%this) {
    return %this.helpPage;
};
