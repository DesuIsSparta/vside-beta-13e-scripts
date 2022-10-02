function MLScrollInspectPanel::OnInspect(%this, %mlTextfileName)
{
    %fo = new FileObject();
    if (%fo.openForRead(%mlTextfileName))
    {
        %text = "";
        while (!%fo.isEOF())
        {
            %text = %text @ %fo.readLine() @ "\n";
        }
        InspectPanelMLText.setText(%text);
        %this.open();
    }
    else
    {
        InspectPanelMLText.setText("I can\'t find the file: " @ %mlTextfileName);
    }
    %fo.delete();
    return ;
}
function clientCmdShowInspectionPanel(%mlTextfileName)
{
    MLScrollInspectPanel.OnInspect(%mlTextfileName);
    return ;
}
function InspectPanelMLText::onURL(%this, %url)
{
    MLScrollInspectPanel.OnInspect(%url);
    return ;
}
function MLScrollInspectPanel::toggle(%this)
{
    PlayGui.showRaiseOrHide(%this);
    return ;
}
function MLScrollInspectPanel::updateSize(%this)
{
    %screenWidth = getWord($UserPref::Video::Resolution, 0);
    %screenHeight = getWord($UserPref::Video::Resolution, 1);
    %posX = 0;
    %posY = 0;
    %width = 358;
    %height = 243;
    if ((%screenHeight == 272) && (%screenWidth == 480))
    {
        %height = 243;
        %width = 358;
        %posX = (%screenWidth - %width) / 2;
        %posY = 0;
    }
    else
    {
        if ((%screenHeight == 363) && (%screenWidth == 640))
        {
            %height = 161 * 2;
            %width = 161 * 3;
            %posX = (%screenWidth - %width) / 2;
            %posY = 0;
        }
        else
        {
            if ((%screenHeight == 544) && (%screenWidth == 960))
            {
                %height = 242 * 2;
                %width = 242 * 3;
                %posX = (%screenWidth - %width) / 2;
                %posY = 0;
            }
            else
            {
                if ((%screenHeight == 714) && (%screenWidth == 1260))
                {
                    %height = 317 * 2;
                    %width = 317 * 3;
                    %posX = (%screenWidth - %width) / 2;
                    %posY = 0;
                }
            }
        }
    }
    %this.resize(%posX, %posY, %width, %height);
    InspectPanelScrollControl.scrollToTop();
    return ;
}
function MLScrollInspectPanel::open(%this)
{
    %this.setVisible(1);
    %this.setConstrained(1);
    PlayGui.focusAndRaise(%this);
    %this.updateSize();
    InspectPanelScrollControl.makeFirstResponder(1);
    return ;
}
function MLScrollInspectPanel::close(%this)
{
    %this.setVisible(0);
    PlayGui.focusTopWindow();
    return 1;
}
