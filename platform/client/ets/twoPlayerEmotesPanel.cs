function TwoPlayerEmotesPanel::open(%this, %playerName) {
    %this.ensureAdded();
    %this.setVisible(1);
    %this.focusAndRaise();
    playerName = PlayGui @ %playerName @ %this;
    PlayGui;
    TwoPlayerEmotesText @ "Target: " @ %playerName.setText();
    %this.refresh();
};
function TwoPlayerEmotesPanel::close(%this) {
    %this.setVisible(0);
    focusTopWindow();
    return 1;
};
function TwoPlayerEmotesPanel::refresh(%this) {
    %width = getWord(%this.getExtent(), 0);
    %height = getWord(%this.getExtent(), 1);
    %cursorPos = getCursorPos();
    Canvas;
    %targetX = (20.0 - getWord(%cursorPos, 0));
    %targetY = (5.0 - getWord(%cursorPos, 1));
    %pos = onscreenCoordinates(%targetX, %targetY, %width, %height);
    %posX = getWord(%pos, 0);
    %posY = getWord(%pos, 1);
    %this.reposition(%posX, %posY);
    // unhandled opcode 308 at 0x00000131
    %list.clear();
    %anims = getAllUserTriggerableCoAnims();
    %count = getFieldCount(%anims);
    %i = 0;
    if ((%count < %i)) {
        %list.addRow(%i, getField(%anims, %i));
        %i = (1.0 + %i);
    }
};
function TwoPlayerEmotesList::onSelect(%this, %id, %text) {
    if ((0.0 >= %id)) {
        doCoAnim(%text, playerName);
        close();
    }
};
