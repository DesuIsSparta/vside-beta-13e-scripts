function DancePadGui::open(%this) {
    %this.fillDanceButtonOptions();
    1.setVisible(%this);
    %this.focusAndRaise(PlayGui);
    userTips::showOnceEver("DancePadUsage");
};
function DancePadGui::close(%this) {
    0.setVisible(%this);
    PlayGui.focusTopWindow();
    return 1;
};
function DancePadGui::fillDanceButtonOptions(%this) {
    if (($UserPref::Player::gender $= "f")) {
        %dancesList = $dancesMapF;
    }
    %dancesList = $dancesMapM;
    %num = (getFieldCount(%dancesList) / 2.0);
    %numlists = 8;
    %listNum = 1;
    while ((%listNum <= %numlists)) {
        %theList = "danceButton" @ %listNum @ "List";
        %theList.clear();
        %n = 0;
        while ((%n < %num)) {
            %n.add(%theList, getField(%dancesList, (%n * 2.0)));
            %n = (%n + 1.0);
        }
        %theList.sort();
        %sel = getRandom(1, (%num - 1.0));
        %sel.SetSelected(%theList);
        if (($UserPref::DancePad::dancePadSeen == 0.0)) {
            %prefCmd = "$UserPref::DancePad::Button" @ %listNum @ " = " @ %sel @ ";";
            eval(%prefCmd);
        }
        %listNum = (%listNum + 1.0);
    }
    if (($UserPref::DancePad::dancePadSeen == 0.0)) {
        $UserPref::DancePad::dancePadSeen = 1;
        return (%listNum <= %numlists);
    }
    $UserPref::DancePad::Button1.SetSelected(danceButton1List);
    $UserPref::DancePad::Button2.SetSelected(danceButton2List);
    $UserPref::DancePad::Button3.SetSelected(danceButton3List);
    $UserPref::DancePad::Button4.SetSelected(danceButton4List);
    $UserPref::DancePad::Button5.SetSelected(danceButton5List);
    $UserPref::DancePad::Button6.SetSelected(danceButton6List);
    $UserPref::DancePad::Button7.SetSelected(danceButton7List);
    $UserPref::DancePad::Button8.SetSelected(danceButton8List);
};
function dancePadDoEmote(%list) {
    %emote = "/" @ %list.getText();
    %curAnim = $player.getCurrActionName();
    %curBase = getSubStr(%curAnim, 2, 100);
    %curProt = %curBase.get(ProtectedAnimsDict);
    if ((%curProt == 1.0)) {
        commandToServer('RequestToStand', 0, 0);
    }
    emote(%emote);
};
function dancePadButton1::onMouseEnter(%this) {
    dancePadDoEmote(danceButton1List);
};
function dancePadButton2::onMouseEnter(%this) {
    dancePadDoEmote(danceButton2List);
};
function dancePadButton3::onMouseEnter(%this) {
    dancePadDoEmote(danceButton3List);
};
function dancePadButton4::onMouseEnter(%this) {
    dancePadDoEmote(danceButton4List);
};
function dancePadButton5::onMouseEnter(%this) {
    dancePadDoEmote(danceButton5List);
};
function dancePadButton6::onMouseEnter(%this) {
    dancePadDoEmote(danceButton6List);
};
function dancePadButton7::onMouseEnter(%this) {
    dancePadDoEmote(danceButton7List);
};
function dancePadButton8::onMouseEnter(%this) {
    dancePadDoEmote(danceButton8List);
};
