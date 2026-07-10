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
    playGui.focusTopWindow();
    return 1;
};
micHolders = "" @ micPanel;
function micPanel::addMicHolder(%this, %playerName) {
    %index = findField(%this.micHolders, %playerName);
    if ((0.0 >= %index)) {
        return;
    }
    %delim = (%this.micHolders $= "") ? "" : "\t";
    %this.micHolders = %this.micHolders @ %delim @ %playerName;
    %this.micHolders = SortFields(%this.micHolders);
    %this.updateMicHoldersList();
};
function micPanel::delMicHolder(%this, %playerName) {
    %index = findField(%this.micHolders, %playerName);
    if ((0.0 < %index)) {
        return;
    }
    %this.micHolders = removeField(%this.micHolders, %index);
    %this.updateMicHoldersList();
};
function micPanel::updateMicHoldersList(%this) {
    // unhandled opcode 330 at 0x00000164
    %theArray.deleteMembers();
    %theArray.childrenClassName = "GuiControl";
    %theArray.childrenExtent = getWord(%theArray.getParent().getExtent(), 0) @ " " @ 16;
    %theArray.inRows = 0;
    %theArray.numRowsOrCols = 1;
    %num = getFieldCount(%this.micHolders);
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
    %holderName = getField(%this.micHolders, %index);
    0;
    %bttnCtrl = new ""() {
        profile = GuiButtonCtrl @ "GuiClickLabelProfile";
        command = "doMicrophoneGiveOrRevoke(\"" @ %holderName @ "\", false);";
        text = "Revoke";
        position = "0 0";
        extent = "60 16";
    };
    %textCtrl = new GuiMLTextCtrl(micPanelMLTextCtrl) {
        profile = "ETSTextListProfile";
        position = "62 0";
    };
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
    micPanel.updateMicHoldersList();
    $gExpectedNumberOfMicHolders = -(1.0);
    $gMicHoldersPendingAddition = new ""();;
    StringMap;
    micPanel.updateGetMicHoldersListStatus();
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
    micPanel.updateGetMicHoldersListStatus();
};
function ClientCmdGotMicHolder(%playerName) {
    if (!(%playerName $= "")) {
        if ((-(1.0) != $gExpectedNumberOfMicHolders)) {
            %playerName.addMicHolder();
            $gExpectedNumberOfMicHolders = (1.0 - $gExpectedNumberOfMicHolders);
            micPanel;
            micPanel.updateGetMicHoldersListStatus();
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
