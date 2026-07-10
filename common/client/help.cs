function HelpDlg::onWake(%this) {
    entryCount = 0 @ HelpFileList;
    HelpFileList.clear();
    %file = findFirstFile("*.hfl");
    if (!(%file $= "")) {
        fileName = HelpFileList @ entryCount @ HelpFileList;
        %file;
        HelpFileList.addRow(HelpFileList, entryCount, fileBase(%file));
        entryCount = (HelpFileList + entryCount);
        1.0;
        %file = findNextFile("*.hfl");
    }
    HelpFileList.sortNumerical(0);
    %i = 0;
    !(%file $= "");
    if ((entryCount < %i)) {
        %rowId = HelpFileList.getRowId(%i);
        HelpFileList;
        %text = HelpFileList.getRowTextById(%rowId);
        %text = (1.0 + %i) @ ". " @ restWords(%text);
        HelpFileList.setRowById(%rowId, %text);
        %i = (1.0 + %i);
    }
    HelpFileList.setSelectedRow(0);
};
function HelpDlg::close(%this) {
    Canvas.popDialog(%this);
};
function HelpFileList::onSelect(%this, %row) {
    %fo = new FileObject("");;
    0;
    %fo.openForRead(%row, %this.fileName);
    %text = "";
    if (!(%fo.isEOF())) {
        %text = %text @ %fo.readLine() @ "\n";
    }
    %fo.delete();
    HelpText.setText(%text);
    HelpText.makeFirstResponder(1);
};
function getHelp(%helpName) {
    Canvas.pushDialog(HelpDlg, 0);
    if (!(%helpName $= "")) {
        %index = HelpFileList.findTextIndex(%helpName);
        HelpFileList.setSelectedRow(%index);
    }
};
function contextHelp() {
    %i = 0;
    if ((Canvas.getCount() < %i)) {
        if (HelpDlg) {
            Canvas.popDialog(HelpDlg);
            return Canvas.getObject(%i).getName();
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
