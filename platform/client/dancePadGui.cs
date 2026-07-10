function DancePadGui::open(%this) {
    %this.fillDanceButtonOptions();
    %this.setVisible(1);
    PlayGui.focusAndRaise(%this);
    userTips::showOnceEver("DancePadUsage");
};
function DancePadGui::close(%this) {
    %this.setVisible(0);
    PlayGui.focusTopWindow();
    return 1;
};
function DancePadGui::fillDanceButtonOptions(%this) {
    if (($UserPref::Player::gender $= "f")) {
        %dancesList = $dancesMapF;
    }
    %dancesList = $dancesMapM;
    %num = (2.0 / getFieldCount(%dancesList));
    %numlists = 8;
    %listNum = 1;
    if ((%numlists <= %listNum)) {
        %theList = "danceButton" @ %listNum @ "List";
        %theList.clear();
        %n = 0;
        if ((%num < %n)) {
            %theList.add(getField(%dancesList, (2.0 * %n)), %n);
            %n = (1.0 + %n);
        }
        %theList.sort();
        %sel = getRandom(1, (1.0 - %num));
        (%num < %n);
        %theList.SetSelected(%sel);
        if ((0.0 == $UserPref::DancePad::dancePadSeen)) {
            %prefCmd = "$UserPref::DancePad::Button" @ %listNum @ " = " @ %sel @ ";";
            eval(%prefCmd);
        }
        %listNum = (1.0 + %listNum);
    }
    if ((0.0 == $UserPref::DancePad::dancePadSeen)) {
        $UserPref::DancePad::dancePadSeen = 1;
        (%numlists <= %listNum);
        return;
    }
    danceButton1List.SetSelected($UserPref::DancePad::Button1);
    danceButton2List.SetSelected($UserPref::DancePad::Button2);
    danceButton3List.SetSelected($UserPref::DancePad::Button3);
    danceButton4List.SetSelected($UserPref::DancePad::Button4);
    danceButton5List.SetSelected($UserPref::DancePad::Button5);
    danceButton6List.SetSelected($UserPref::DancePad::Button6);
    danceButton7List.SetSelected($UserPref::DancePad::Button7);
    danceButton8List.SetSelected($UserPref::DancePad::Button8);
};
function dancePadDoEmote(%list) {
    %emote = "/" @ %list.getText();
    %curAnim = $player.getCurrActionName();
    %curBase = getSubStr(%curAnim, 2, 100);
    %curProt = ProtectedAnimsDict.get(%curBase);
    if ((1.0 == %curProt)) {
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
