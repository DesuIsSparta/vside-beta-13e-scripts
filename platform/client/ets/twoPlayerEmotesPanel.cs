function TwoPlayerEmotesPanel::open(%this, %playerName) {
    %this.ensureAdded(PlayGui);
    1.setVisible(%this);
    %this.focusAndRaise(PlayGui);
    %this.playerName = %playerName;
    "Target: " @ %playerName.setText(TwoPlayerEmotesText);
    %this.refresh();
};
function TwoPlayerEmotesPanel::close(%this) {
    0.setVisible(%this);
    PlayGui.focusTopWindow();
    return 1;
};
function TwoPlayerEmotesPanel::refresh(%this) {
    %width = getWord(%this.getExtent(), 0);
    %height = getWord(%this.getExtent(), 1);
    %cursorPos = Canvas.getCursorPos();
    %targetX = (getWord(%cursorPos, 0) - 20.0);
    %targetY = (getWord(%cursorPos, 1) - 5.0);
    %pos = onscreenCoordinates(%targetX, %targetY, %width, %height);
    %posX = getWord(%pos, 0);
    %posY = getWord(%pos, 1);
    %posY.reposition(%this, %posX);
    %list = TwoPlayerEmotesList;
    %list.clear();
    %anims = getAllUserTriggerableCoAnims();
    %count = getFieldCount(%anims);
    %i = 0;
    while ((%i < %count)) {
        getField(%anims, %i).addRow(%list, %i);
        %i = (%i + 1.0);
    }
};
function TwoPlayerEmotesList::onSelect(%this, %id, %text) {
    if ((%id >= 0.0)) {
        doCoAnim(%text, TwoPlayerEmotesPanel.playerName);
        TwoPlayerEmotesPanel.close();
    }
};
