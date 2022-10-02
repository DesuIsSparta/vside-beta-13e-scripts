function toggleSnoopPanel()
{
    SnoopPanel.toggle();
    return ;
}
function SnoopPanel::toggle(%this)
{
    playGui.ensureAdded(%this);
    playGui.showRaiseOrHide(%this);
    return ;
}
function SnoopPanel::open(%this)
{
    if (!$player.rolesPermissionCheckNoWarn("snoop"))
    {
        return ;
    }
    playGui.ensureAdded(%this);
    if (!%this.isVisible())
    {
        %this.setVisible(1);
        %this.restoreDims();
        playGui.focusAndRaise(%this);
    }
    return ;
}
function SnoopPanel::close(%this)
{
    playGui.ensureAdded(%this);
    %this.setVisible(0);
    playGui.focusTopWindow();
    %this.storeDims();
    return ;
}
function SnoopPanel::restoreDims(%this)
{
    %dim = $DevPref::Mod::SnoopWindow::Dim;
    %this.resize(getWord(%dim, 0), getWord(%dim, 1), getWord(%dim, 2), getWord(%dim, 3));
    return ;
}
function SnoopPanel::storeDims(%this)
{
    $DevPref::Mod::SnoopWindow::Dim = %this.getPosition() SPC %this.getExtent();
    return ;
}
function SnoopPanel::addLine(%this, %text)
{
    if ($DevPref::Mod::censorSnoop)
    {
        %text = fixBadWords(%text);
    }
    if ($DevPref::Mod::autoOpenSnoop)
    {
        %this.open();
    }
    %timeStamp = SystemMessageDialog::getTimeStampNice(getTimeStamp()) @ " ";
    if (!(snoopPanelTextCtrl.getText() $= ""))
    {
        %newLine = "\n";
    }
    else
    {
        %newLine = "";
    }
    snoopPanelTextCtrl.addText(%newLine @ %timeStamp @ %text, 1, SnoopPanelScroll.isAtBottom());
    return ;
}
function SnoopPanel::addLine2(%this, %line)
{
    %this.addLine(%line);
    return ;
}
function SnoopPanel::handleIncoming(%this, %text, %name, %whisperedTo, %ignored, %speechType, %isAutoReply)
{
    %text = pChat::composeLine(%text, %name, %whisperedTo, %ignored, %speechType, %isAutoReply);
    %text = strreplace(%text, "<color:000000", "<color:ffffff");
    if (%speechType $= "sos")
    {
        %text = "<spush><color:dd0000>sos<spop>  " SPC %text;
    }
    else
    {
        if (%speechType $= "abuse")
        {
            %text = "<spush><color:dd0000>abuse<spop>  " SPC %text;
        }
        else
        {
            %text = "<spush><color:00aa00>snoop" SPC %text @ "<spop>";
        }
    }
    %this.addLine2(%text);
    if ($DevPref::Audio::NotifySnoop)
    {
        if (%speechType $= "sos")
        {
            alxPlay(Audio_SOSMessageIn);
        }
        else
        {
            if (%speechType $= "abuse")
            {
                alxPlay(Audio_SOSMessageIn);
            }
        }
    }
    return ;
}
function ClientCmdSnoopIn(%text, %name, %whisperedTo, %ignored, %speechType, %isAutoReply)
{
    SnoopPanel.handleIncoming(%text, %name, %whisperedTo, %ignored, %speechType, %isAutoReply);
    return ;
}
function onModNotificationCussing(%playerName, %param2)
{
    if (!$DevPref::Mod::cusses)
    {
        return ;
    }
    %text = NextToken(%param2, "verb", " ");
    %line = "<spush><color:880088>cuss ";
    %line = %line SPC pChat.getPlayerMarkup(%playerName, "");
    %line = %line SPC %verb SPC %text;
    %line = %line SPC "<spop>";
    SnoopPanel.addLine2(%line);
    %soundNum = stringToInteger(%playerName, $gAudioProfile_CussesNum);
    alxPlay2($gAudioProfile_Cusses[%soundNum]);
    return ;
}
function stringToInteger(%string, %maxInteger)
{
    if (%maxInteger <= 0)
    {
        error("%maxInteger must be positive" SPC getTrace());
        return 0;
    }
    %val = 0;
    %a = munge(%string);
    while (!(%a $= ""))
    {
        %chars = 4;
        %b = getSubStr(%a, 0, %chars);
        eval("%b = 0x" @ %b @ ";");
        %val = %val ^ %b;
        %a = getSubStr(%a, %chars, 10000000);
    }
    %val = %val % %maxInteger;
    return %val;
}
function snoopPanelTextCtrl::onRightURL(%this, %url)
{
    if (firstWord(%url) $= "gamelink")
    {
        %name = unmunge(getWords(%url, 1));
        onRightClickPlayerName(%name);
    }
    return ;
}
function snoopPanelTextCtrl::onUrl(%this, %url)
{
    if (firstWord(%url) $= "gamelink")
    {
        %name = unmunge(getWords(%url, 1));
        onLeftClickPlayerName(%name, "");
    }
    else
    {
        if (getSubStr(%url, 0, 7) $= "http://")
        {
            gotoWebPage(%url);
        }
        else
        {
            if (getSubStr(%url, 0, 7) $= "vside:/")
            {
                vurlOperation(%url);
            }
        }
    }
    return ;
}
function SnoopPanel::copyToClipboard(%this)
{
    setClipboard(StripMLControlChars(snoopPanelTextCtrl.getText()));
    return ;
}
function doUserSnoop(%playerName, %on)
{
    commandToServer('SnoopPlayer', %playerName, %on);
    return ;
}
