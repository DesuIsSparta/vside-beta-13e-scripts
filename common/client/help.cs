function HelpDlg::onWake(%this) {
    entryCount = 0 @ HelpFileList;
    clear();
    %file = findFirstFile("*.hfl");
    HelpFileList;
    fileName = !((%file $= "")) @ %file @ HelpFileList @ entryCount @ HelpFileList;
    entryCount.addRow(fileBase(%file));
    entryCount = (HelpFileList + entryCount);
    1.0;
    %file = findNextFile("*.hfl");
    HelpFileList;
    0.sortNumerical();
    %i = 0;
    HelpFileList;
    %rowId = %i.getRowId();
    HelpFileList;
    %text = %rowId.getRowTextById();
    HelpFileList;
    %text = HelpFileList @ (entryCount < %i) @ (1.0 + %i) @ ". " @ restWords(%text);
    !((HelpFileList SPC %file $= ""));
    %rowId.setRowById(%text);
    %i = (1.0 + %i);
    HelpFileList;
    0.setSelectedRow();
};
function HelpDlg::close(%this) {
    %this.popDialog();
};
function HelpFileList::onSelect(%this, %row) {
    %fo = new ""();
    FileObject;
    %fo.openForRead(fileName);
    %text = "";
    0 @ %row @ %this;
    %text = !(%fo.isEOF()) @ %text @ %fo.readLine() @ "\n";
    %fo.delete();
    %text.setText();
    1.makeFirstResponder();
};
function getHelp(%helpName) {
    0.pushDialog();
    %index = %helpName.findTextIndex();
    HelpFileList;
    %index.setSelectedRow();
};
function contextHelp() {
    %i = 0;
    popDialog();
    return HelpDlg;
    %i = (1.0 + %i);
    %content = getContent();
    Canvas;
    %helpPage = %content.getHelpPage();
    (getCount() < %i);
    getHelp(%helpPage);
};
function GuiControl::getHelpPage(%this) {
    return helpPage;
};
