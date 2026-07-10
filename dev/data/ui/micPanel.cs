$gExpectedNumberOfMicHolders = -(1.0);
$gMicHoldersPendingAddition = 0;
function micPanel::toggle(%this)
{
    playGui.showRaiseOrHide(%this);
}
function micPanel::open(%this)
{
    if (!$player.rolesPermissionCheckWarn("microphones"))
    {
        return;
    }
    if (!%this.isVisible())
    {
        %this.setVisible(1);
        playGui.focusAndRaise(%this);
    }
}
function micPanel::close(%this)
{
    %this.setVisible(0);
    playGui.focusTopWindow();
    return 1;
}
micPanel.micHolders = "";
function micPanel::addMicHolder(%this, %playerName)
{
    %index = findField(%this.micHolders, %playerName);
    if ((%index >= 0.0))
    {
        return;
    }
    %delim = (%this.micHolders $= "") ? "" : "\t";
    %this.micHolders = %this.micHolders @ %delim @ %playerName;
    %this.micHolders = SortFields(%this.micHolders);
    %this.updateMicHoldersList();
}
function micPanel::delMicHolder(%this, %playerName)
{
    %index = findField(%this.micHolders, %playerName);
    if ((%index < 0.0))
    {
        return;
    }
    %this.micHolders = removeField(%this.micHolders, %index);
    %this.updateMicHoldersList();
}
function micPanel::updateMicHoldersList(%this)
{
    %theArray = micPanelArray;
    %theArray.deleteMembers();
    %theArray.childrenClassName = "GuiControl";
    %theArray.childrenExtent = getWord(%theArray.getParent().getExtent(), 0) @ " " @ 16;
    %theArray.inRows = 0;
    %theArray.numRowsOrCols = 1;
    %num = getFieldCount(%this.micHolders);
    %theArray.setNumChildren(%num);
    %n = 0;
    while ((%n < %num))
    {
        %this.updateMicHolderCell(%theArray.getObject(%n), %n);
        %n = (%n + 1.0);
    }
    if ($DevPref::Mod::autoOpenMics)
    {
        %this.open();
    }
}
function micPanel::updateMicHolderCell(%this, %cellCtrl, %index)
{
    %holderName = getField(%this.micHolders, %index);
    %bttnCtrl = new GuiButtonCtrl("") {
        profile = "GuiClickLabelProfile";
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
    %textCtrl.setValue(pChat.getPlayerMarkup(%holderName, ""));
}
function micPanelMLTextCtrl::onRightURL(%this, %url)
{
    if ((firstWord(%url) $= "gamelink"))
    {
        %name = unmunge(getWords(%url, 1));
        onRightClickPlayerName(%name);
    }
}
function micPanelMLTextCtrl::onUrl(%this, %url)
{
    if ((firstWord(%url) $= "gamelink"))
    {
        %name = unmunge(getWords(%url, 1));
        onLeftClickPlayerName(%name, "");
    }
    else
    {
        if ((getSubStr(%url, 0, 7) $= "http://"))
        {
            gotoWebPage(%url);
        }
        else
        {
            if ((getSubStr(%url, 0, 7) $= "vside:/"))
            {
                vurlOperation(%url);
            }
        }
    }
}
function doServerCommandGetMicHolders()
{
    micPanel.micHolders = "";
    micPanel.updateMicHoldersList();
    $gExpectedNumberOfMicHolders = -(1.0);
    $gMicHoldersPendingAddition = new StringMap("");
    micPanel.updateGetMicHoldersListStatus();
    commandToServer('GetMicrophoneHoldersList');
}
function ClientCmdStartGetMicHolders(%numberOfMicHolders)
{
    $gExpectedNumberOfMicHolders = %numberOfMicHolders;
    if (($gMicHoldersPendingAddition != 0.0))
    {
        %i = ($gMicHoldersPendingAddition.size() - 1.0);
        while ((%i >= 0.0))
        {
            micPanel.addMicHolder($gMicHoldersPendingAddition.getKey(%i));
            $gExpectedNumberOfMicHolders = ($gExpectedNumberOfMicHolders - 1.0);
            %i = (%i - 1.0);
        }
        $gMicHoldersPendingAddition.clear();
        $gMicHoldersPendingAddition.delete();
        $gMicHoldersPendingAddition = 0;
        (%i >= 0.0);
    }
    micPanel.updateGetMicHoldersListStatus();
}
function ClientCmdGotMicHolder(%playerName)
{
    if (!(%playerName $= ""))
    {
        if (($gExpectedNumberOfMicHolders != -(1.0)))
        {
            micPanel.addMicHolder(%playerName);
            $gExpectedNumberOfMicHolders = ($gExpectedNumberOfMicHolders - 1.0);
            micPanel.updateGetMicHoldersListStatus();
        }
        else
        {
            $gMicHoldersPendingAddition.put(%playerName, "");
        }
    }
}
function micPanel::updateGetMicHoldersListStatus(%this)
{
    if (($gExpectedNumberOfMicHolders == -(1.0)))
    {
        MicPanelRefreshListLabel.setText("Starting...");
        MicPanelRefreshListButton.setActive(0);
    }
    else
    {
        if (($gExpectedNumberOfMicHolders == 0.0))
        {
            MicPanelRefreshListLabel.setText("Done");
            MicPanelRefreshListButton.setActive(1);
        }
        else
        {
            MicPanelRefreshListLabel.setText("Getting list...");
            MicPanelRefreshListButton.setActive(0);
        }
    }
}
