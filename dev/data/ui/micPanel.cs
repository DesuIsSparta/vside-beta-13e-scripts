$gExpectedNumberOfMicHolders = -(1.0);
$gMicHoldersPendingAddition = 0;
function micPanel::toggle(%this) {
    %this.showRaiseOrHide();
};
function micPanel::open(%this) {
    if (!($player.rolesPermissionCheckWarn("microphones"))) {
        return;
    }
    if (!(%this.isVisible())) {
        %this.setVisible(1);
        %this.focusAndRaise();
    }
};
function micPanel::close(%this) {
    %this.setVisible(0);
    focusTopWindow();
    return 1;
};
micHolders = "" @ micPanel;
function micPanel::addMicHolder(%this, %playerName) {
    %index = findField(micHolders, %playerName);
    %this;
    if ((0.0 >= %index)) {
        return;
    }
    %delim = (%this SPC micHolders $= "") ? "" : "\t";
    micHolders = %this @ micHolders @ %delim @ %playerName @ %this;
    micHolders = %this @ SortFields(micHolders) @ %this;
    %this.updateMicHoldersList();
};
function micPanel::delMicHolder(%this, %playerName) {
    %index = findField(micHolders, %playerName);
    %this;
    if ((0.0 < %index)) {
        return;
    }
    micHolders = %this @ removeField(micHolders, %index) @ %this;
    %this.updateMicHoldersList();
};
function micPanel::updateMicHoldersList(%this) {
    // unhandled opcode 330 at 0x00000164
    %theArray.deleteMembers();
    childrenClassName = "GuiControl" @ %theArray;
    childrenExtent = getWord(%theArray.getParent().getExtent(), 0) @ " " @ 16 @ %theArray;
    inRows = 0 @ %theArray;
    numRowsOrCols = 1 @ %theArray;
    %num = getFieldCount(micHolders);
    %this;
    %theArray.setNumChildren(%num);
    %n = 0;
    if ((%num < %n)) {
        %this.updateMicHolderCell(%theArray.getObject(%n), %n);
        %n = (1.0 + %n);
    }
    if ($DevPref::Mod::autoOpenMics) {
        %this.open();
    }
};
function micPanel::updateMicHolderCell(%this, %cellCtrl, %index) {
    %holderName = getField(micHolders, %index);
    %this;
    profile = GuiButtonCtrl @ new ""() @ "GuiClickLabelProfile";
    0;
    command = "doMicrophoneGiveOrRevoke(\"" @ %holderName @ "\", false);";
    text = "Revoke";
    position = "0 0";
    extent = "60 16";
    %bttnCtrl = ;
    profile = new GuiMLTextCtrl(micPanelMLTextCtrl) @ "ETSTextListProfile";
    position = "62 0";
    %textCtrl = ;
    %cellCtrl.deleteMembers();
    %cellCtrl.add(%bttnCtrl);
    %cellCtrl.add(%textCtrl);
    %textCtrl.setValue(%holderName.getPlayerMarkup(""));
};
function micPanelMLTextCtrl::onRightURL(%this, %url) {
    if ((firstWord(%url) $= "gamelink")) {
        %name = unmunge(getWords(%url, 1));
        onRightClickPlayerName(%name);
    }
};
function micPanelMLTextCtrl::onUrl(%this, %url) {
    if ((firstWord(%url) $= "gamelink")) {
        %name = unmunge(getWords(%url, 1));
        onLeftClickPlayerName(%name, "");
    }
    if ((getSubStr(%url, 0, 7) $= "http://")) {
        gotoWebPage(%url);
    }
    if ((getSubStr(%url, 0, 7) $= "vside:/")) {
        vurlOperation(%url);
    }
};
function doServerCommandGetMicHolders() {
    micHolders = "" @ micPanel;
    updateMicHoldersList();
    $gExpectedNumberOfMicHolders = -(1.0);
    micPanel;
    $gMicHoldersPendingAddition = new ""();
    StringMap;
    updateGetMicHoldersListStatus();
    commandToServer('GetMicrophoneHoldersList');
};
function ClientCmdStartGetMicHolders(%numberOfMicHolders) {
    $gExpectedNumberOfMicHolders = %numberOfMicHolders;
    if ((0.0 != $gMicHoldersPendingAddition)) {
        %i = (1.0 - $gMicHoldersPendingAddition.size());
        if ((0.0 >= %i)) {
            $gMicHoldersPendingAddition.getKey(%i).addMicHolder();
            $gExpectedNumberOfMicHolders = (1.0 - $gExpectedNumberOfMicHolders);
            micPanel;
            %i = (1.0 - %i);
        }
        $gMicHoldersPendingAddition.clear();
        $gMicHoldersPendingAddition.delete();
        $gMicHoldersPendingAddition = 0;
        (0.0 >= %i);
    }
    updateGetMicHoldersListStatus();
};
function ClientCmdGotMicHolder(%playerName) {
    if (!(%playerName $= "")) {
        if ((-(1.0) != $gExpectedNumberOfMicHolders)) {
            %playerName.addMicHolder();
            $gExpectedNumberOfMicHolders = (1.0 - $gExpectedNumberOfMicHolders);
            micPanel;
            updateGetMicHoldersListStatus();
        }
        $gMicHoldersPendingAddition.put(%playerName, "");
    }
};
function micPanel::updateGetMicHoldersListStatus(%this) {
    if ((-(1.0) == $gExpectedNumberOfMicHolders)) {
        "Starting...".setText();
        0.setActive();
    }
    if ((0.0 == $gExpectedNumberOfMicHolders)) {
        "Done".setText();
        1.setActive();
    }
    "Getting list...".setText();
    0.setActive();
};
