function DancePadGui::open(%this)
{
    %this.fillDanceButtonOptions();
    %this.setVisible(1);
    PlayGui.focusAndRaise(%this);
    userTips::showOnceEver("DancePadUsage");
    return ;
}
function DancePadGui::close(%this)
{
    %this.setVisible(0);
    PlayGui.focusTopWindow();
    return 1;
}
function DancePadGui::fillDanceButtonOptions(%this)
{
    if ($UserPref::Player::gender $= "f")
    {
        %dancesList = $dancesMapF;
    }
    else
    {
        %dancesList = $dancesMapM;
    }
    %num = getFieldCount(%dancesList) / 2;
    %numlists = 8;
    %listNum = 1;
    while (%listNum <= %numlists)
    {
        %theList = "danceButton" @ %listNum @ "List";
        %theList.clear();
        %n = 0;
        while (%n < %num)
        {
            %theList.add(getField(%dancesList, %n * 2), %n);
            %n = %n + 1;
        }
        %theList.sort();
        %sel = getRandom(1, %num - 1);
        %theList.SetSelected(%sel);
        if ($UserPref::DancePad::dancePadSeen == 0)
        {
            %prefCmd = "$UserPref::DancePad::Button" @ %listNum @ " = " @ %sel @ ";";
            eval(%prefCmd);
        }
        %listNum = %listNum + 1;
    }
    if ($UserPref::DancePad::dancePadSeen == 0)
    {
        $UserPref::DancePad::dancePadSeen = 1;
        return ;
    }
    danceButton1List.SetSelected($UserPref::DancePad::Button1);
    danceButton2List.SetSelected($UserPref::DancePad::Button2);
    danceButton3List.SetSelected($UserPref::DancePad::Button3);
    danceButton4List.SetSelected($UserPref::DancePad::Button4);
    danceButton5List.SetSelected($UserPref::DancePad::Button5);
    danceButton6List.SetSelected($UserPref::DancePad::Button6);
    danceButton7List.SetSelected($UserPref::DancePad::Button7);
    danceButton8List.SetSelected($UserPref::DancePad::Button8);
    return ;
}
function dancePadDoEmote(%list)
{
    %emote = "/" @ %list.getText();
    %curAnim = $player.getCurrActionName();
    %curBase = getSubStr(%curAnim, 2, 100);
    %curProt = ProtectedAnimsDict.get(%curBase);
    if (%curProt == 1)
    {
        commandToServer('RequestToStand', 0, 0);
    }
    emote(%emote);
    return ;
}
function dancePadButton1::onMouseEnter(%this)
{
    dancePadDoEmote(danceButton1List);
    return ;
}
function dancePadButton2::onMouseEnter(%this)
{
    dancePadDoEmote(danceButton2List);
    return ;
}
function dancePadButton3::onMouseEnter(%this)
{
    dancePadDoEmote(danceButton3List);
    return ;
}
function dancePadButton4::onMouseEnter(%this)
{
    dancePadDoEmote(danceButton4List);
    return ;
}
function dancePadButton5::onMouseEnter(%this)
{
    dancePadDoEmote(danceButton5List);
    return ;
}
function dancePadButton6::onMouseEnter(%this)
{
    dancePadDoEmote(danceButton6List);
    return ;
}
function dancePadButton7::onMouseEnter(%this)
{
    dancePadDoEmote(danceButton7List);
    return ;
}
function dancePadButton8::onMouseEnter(%this)
{
    dancePadDoEmote(danceButton8List);
    return ;
}
