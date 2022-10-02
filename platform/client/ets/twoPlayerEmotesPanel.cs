function TwoPlayerEmotesPanel::open(%this, %playerName)
{
    PlayGui.ensureAdded(%this);
    %this.setVisible(1);
    PlayGui.focusAndRaise(%this);
    %this.playerName = %playerName;
    TwoPlayerEmotesText.setText("Target: " @ %playerName);
    %this.refresh();
    return ;
}
function TwoPlayerEmotesPanel::close(%this)
{
    %this.setVisible(0);
    PlayGui.focusTopWindow();
    return 1;
}
function TwoPlayerEmotesPanel::refresh(%this)
{
    %width = getWord(%this.getExtent(), 0);
    %height = getWord(%this.getExtent(), 1);
    %cursorPos = Canvas.getCursorPos();
    %targetX = getWord(%cursorPos, 0) - 20;
    %targetY = getWord(%cursorPos, 1) - 5;
    %pos = onscreenCoordinates(%targetX, %targetY, %width, %height);
    %posX = getWord(%pos, 0);
    %posY = getWord(%pos, 1);
    %this.reposition(%posX, %posY);
    %list = TwoPlayerEmotesList;
    %list.clear();
    %anims = getAllUserTriggerableCoAnims();
    %count = getFieldCount(%anims);
    %i = 0;
    while (%i < %count)
    {
        %list.addRow(%i, getField(%anims, %i));
        %i = %i + 1;
    }
}

function TwoPlayerEmotesList::onSelect(%this, %id, %text)
{
    if (%id >= 0)
    {
        doCoAnim(%text, TwoPlayerEmotesPanel.playerName);
        TwoPlayerEmotesPanel.close();
    }
    return ;
}
