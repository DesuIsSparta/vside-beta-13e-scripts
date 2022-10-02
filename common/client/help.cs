function HelpDlg::onWake(%this)
{
    HelpFileList.entryCount = 0;
    HelpFileList.clear();
    %file = findFirstFile("*.hfl");
    while (!(%file $= ""))
    {
        HelpFileList.fileName[HelpFileList.entryCount] = %file;
        HelpFileList.addRow(HelpFileList.entryCount, fileBase(%file));
        HelpFileList.entryCount = HelpFileList.entryCount + 1;
        %file = findNextFile("*.hfl");
    }
    HelpFileList.sortNumerical(0);
    %i = 0;
    while (%i < HelpFileList.entryCount)
    {
        %rowId = HelpFileList.getRowId(%i);
        %text = HelpFileList.getRowTextById(%rowId);
        %text = %i + 1 @ ". " @ restWords(%text);
        HelpFileList.setRowById(%rowId, %text);
        %i = %i + 1;
    }
    HelpFileList.setSelectedRow(0);
    return ;
}
function HelpDlg::close(%this)
{
    Canvas.popDialog(%this);
    return ;
}
function HelpFileList::onSelect(%this, %row)
{
    %fo = new FileObject();
    %fo.openForRead(%this.fileName[%row]);
    %text = "";
    while (!%fo.isEOF())
    {
        %text = %text @ %fo.readLine() @ "\n";
    }
    %fo.delete();
    HelpText.setText(%text);
    HelpText.makeFirstResponder(1);
    return ;
}
function getHelp(%helpName)
{
    Canvas.pushDialog(HelpDlg, 0);
    if (!(%helpName $= ""))
    {
        %index = HelpFileList.findTextIndex(%helpName);
        HelpFileList.setSelectedRow(%index);
    }
    return ;
}
function contextHelp()
{
    %i = 0;
    while (%i < Canvas.getCount())
    {
        if (Canvas.getObject(%i).getName() $= HelpDlg)
        {
            Canvas.popDialog(HelpDlg);
            return ;
        }
        %i = %i + 1;
    }
    %content = Canvas.getContent();
    %helpPage = %content.getHelpPage();
    getHelp(%helpPage);
    return ;
}
function GuiControl::getHelpPage(%this)
{
    return %this.helpPage;
}
