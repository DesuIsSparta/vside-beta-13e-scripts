function getGender(%obj) {
    if ((%obj $= "f")) {
    }
    if ((%obj $= "m")) {
    }
    if ((%obj $= "n")) {
        return %obj;
    }
    if (!(isPlayerObject(%obj))) {
        return "n";
    }
    return %obj.getGender();
};
function getPronounHeSheIt(%obj) {
    return;
};
function getPronounHeSheThey(%obj) {
    return;
};
function getPronounHimHerIt(%obj) {
    return;
};
function getPronounHimHerThem(%obj) {
    return;
};
function getPronounHisHerIts(%obj) {
    return;
};
function getPronounHisHerTheir(%obj) {
    return;
};
function getPronounHisHerTheirCapital(%obj) {
    return;
};
function getPronounHisHersIts(%obj) {
    return;
};
function getPronounHisHersTheirs(%obj) {
    return;
};
function getPronounItThem(%quantity) {
    %ret = (1.0 == %quantity) ? "it" : "them";
    return %ret;
};
