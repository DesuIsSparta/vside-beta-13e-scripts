$gExpectedNumberOfMicHolders = -(1.0);
$gMicHoldersPendingAddition = 0;
function micPanel::toggle(%this) {
    %this.showRaiseOrHide(playGui);
};
function micPanel::open(%this) {
    if (!("microphones".rolesPermissionCheckWarn($player))) {
        return;
    }
    if (!(%this.isVisible())) {
        1.setVisible(%this);
        %this.focusAndRaise(playGui);
    }
};
function micPanel::close(%this) {
    0.setVisible(%this);
    playGui.focusTopWindow();
    return 1;
};
micHolders = "" @ micPanel;
function micPanel::addMicHolder(%this, %playerName) {
    %index = findField(%this.micHolders, %playerName);
    if ((%index >= 0.0)) {
        return;
    }
    %delim = (%this.micHolders $= "") ? "" : "\t";
    %this.micHolders = %this.micHolders @ %delim @ %playerName;
    %this.micHolders = SortFields(%this.micHolders);
    %this.updateMicHoldersList();
};
function micPanel::delMicHolder(%this, %playerName) {
    %index = findField(%this.micHolders, %playerName);
    if ((%index < 0.0)) {
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
    %num.setNumChildren(%theArray);
    %n = 0;
    while ((%n < %num)) {
        %n.updateMicHolderCell(%this, %n.getObject(%theArray));
        %n = (%n + 1.0);
    }
    if ($DevPref::Mod::autoOpenMics) {
        %this.open();
    }
};
function micPanel::updateMicHolderCell(%this, %cellCtrl, %index) {
    %holderName = getField(%this.micHolders, %index);
    %bttnCtrl = new GuiButtonCtrl("") {
        profile = 0 @ "GuiClickLabelProfile";
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
    %bttnCtrl.add(%cellCtrl);
    %textCtrl.add(%cellCtrl);
    "".getPlayerMarkup(pChat, %holderName).setValue(%textCtrl);
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
    $gMicHoldersPendingAddition = new StringMap("");;
    0;
    micPanel.updateGetMicHoldersListStatus();
    commandToServer('GetMicrophoneHoldersList');
};
function ClientCmdStartGetMicHolders(%numberOfMicHolders) {
    $gExpectedNumberOfMicHolders = %numberOfMicHolders;
    if (($gMicHoldersPendingAddition != 0.0)) {
        %i = ($gMicHoldersPendingAddition.size() - 1.0);
        while ((%i >= 0.0)) {
            %i.getKey($gMicHoldersPendingAddition).addMicHolder(micPanel);
            $gExpectedNumberOfMicHolders = ($gExpectedNumberOfMicHolders - 1.0);
            %i = (%i - 1.0);
        }
        $gMicHoldersPendingAddition.clear();
        $gMicHoldersPendingAddition.delete();
        $gMicHoldersPendingAddition = 0;
        (%i >= 0.0);
    }
    micPanel.updateGetMicHoldersListStatus();
};
function ClientCmdGotMicHolder(%playerName) {
    if (!(%playerName $= "")) {
        if (($gExpectedNumberOfMicHolders != -(1.0))) {
            %playerName.addMicHolder(micPanel);
            $gExpectedNumberOfMicHolders = ($gExpectedNumberOfMicHolders - 1.0);
            micPanel.updateGetMicHoldersListStatus();
        }
        "".put($gMicHoldersPendingAddition, %playerName);
    }
};
function micPanel::updateGetMicHoldersListStatus(%this) {
    if (($gExpectedNumberOfMicHolders == -(1.0))) {
        "Starting...".setText(MicPanelRefreshListLabel);
        0.setActive(MicPanelRefreshListButton);
    }
    if (($gExpectedNumberOfMicHolders == 0.0)) {
        "Done".setText(MicPanelRefreshListLabel);
        1.setActive(MicPanelRefreshListButton);
    }
    "Getting list...".setText(MicPanelRefreshListLabel);
    0.setActive(MicPanelRefreshListButton);
};
