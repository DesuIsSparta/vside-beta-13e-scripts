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
    %dancesList = $dancesMapF;
    ($UserPref::Player::gender $= "f");
    %dancesList = $dancesMapM;
    %num = (2.0 / getFieldCount(%dancesList));
    %numlists = 8;
    %listNum = 1;
    %theList = (%numlists <= %listNum) @ "danceButton" @ %listNum @ "List";
    %theList.clear();
    %n = 0;
    %theList.add(getField(%dancesList, (2.0 * %n)), %n);
    %n = (1.0 + %n);
    (%num < %n);
    %theList.sort();
    %sel = getRandom(1, (1.0 - %num));
    (%num < %n);
    %theList.SetSelected(%sel);
    %prefCmd = (0.0 == $UserPref::DancePad::dancePadSeen) @ "$UserPref::DancePad::Button" @ %listNum @ " = " @ %sel @ ";";
    eval(%prefCmd);
    %listNum = (1.0 + %listNum);
    $UserPref::DancePad::dancePadSeen = 1;
    (0.0 == $UserPref::DancePad::dancePadSeen);
    return (%numlists <= %listNum);
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
    commandToServer('RequestToStand', 0, 0);
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
