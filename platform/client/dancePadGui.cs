function DancePadGui::open(%this) {
    %this.fillDanceButtonOptions();
    %this.setVisible(1);
    %this.focusAndRaise();
    userTips::showOnceEver("DancePadUsage");
};
function DancePadGui::close(%this) {
    %this.setVisible(0);
    focusTopWindow();
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
    $UserPref::DancePad::Button1.SetSelected();
    $UserPref::DancePad::Button2.SetSelected();
    $UserPref::DancePad::Button3.SetSelected();
    $UserPref::DancePad::Button4.SetSelected();
    $UserPref::DancePad::Button5.SetSelected();
    $UserPref::DancePad::Button6.SetSelected();
    $UserPref::DancePad::Button7.SetSelected();
    $UserPref::DancePad::Button8.SetSelected();
};
function dancePadDoEmote(%list) {
    %emote = "/" @ %list.getText();
    %curAnim = $player.getCurrActionName();
    %curBase = getSubStr(%curAnim, 2, 100);
    %curProt = %curBase.get();
    ProtectedAnimsDict;
    if ((1.0 == %curProt)) {
        commandToServer('RequestToStand', 0, 0);
    }
    emote(%emote);
};
function dancePadButton1::onMouseEnter(%this) {
    dancePadDoEmote();
};
function dancePadButton2::onMouseEnter(%this) {
    dancePadDoEmote();
};
function dancePadButton3::onMouseEnter(%this) {
    dancePadDoEmote();
};
function dancePadButton4::onMouseEnter(%this) {
    dancePadDoEmote();
};
function dancePadButton5::onMouseEnter(%this) {
    dancePadDoEmote();
};
function dancePadButton6::onMouseEnter(%this) {
    dancePadDoEmote();
};
function dancePadButton7::onMouseEnter(%this) {
    dancePadDoEmote();
};
function dancePadButton8::onMouseEnter(%this) {
    dancePadDoEmote();
};
