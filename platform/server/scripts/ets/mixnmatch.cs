function serverCmdUseMesh(%client, %mesh) {
    if (!(isObject(%client.Player))) {
        return;
    }
    %mesh.UseMesh(%client.Player);
    return;
};
function serverCmdUseMeshRandom(%client, %category) {
    if (!(isObject(%client.Player))) {
        return;
    }
    %category.UseMeshRandom(%client.Player);
    return;
};
function serverCmdUseSkinToneRandom(%client) {
    if (!(isObject(%client.Player))) {
        return;
    }
    %client.Player.UseSkinToneRandom();
    return;
};
function serverCmdUseSkinTone(%client, %tone) {
    if (!(isObject(%client.Player))) {
        return;
    }
    %tone.UseSkinTone(%client.Player);
    return;
};
function Player::UseSkinTone(%this, %tone) {
    %tone.setSkinName(%this);
    return;
};
function Player::UseSkinToneRandom(%this) {
    if (!($numSkinTones)) {
        $numSkinTones = 0;
        $numSkinTones[$skinTones @ $numSkinTones] = "base";
        $numSkinTones = ($numSkinTones + 1.0);
        $numSkinTones[$skinTones @ $numSkinTones] = "tan";
        $numSkinTones = ($numSkinTones + 1.0);
        $numSkinTones[$skinTones @ $numSkinTones] = "dark";
        $numSkinTones = ($numSkinTones + 1.0);
    }
    %tone = ;
    %face = getRandom(1, 4);
    %tone @ ".body".UseSkinTone(%this);
    %tone @ %face @ ".face".UseSkinTone(%this);
    return;
};
function serverCmdUseHairRandom(%client) {
    if (!(isObject(%client.Player))) {
        return;
    }
    %client.Player.UseHairRandom();
    return;
};
function Player::UseHairRandom(%this) {
    if (!($numHairTones)) {
        $numHairTones = 0;
        $numHairTones[$hairTones @ $numHairTones] = "base";
        $numHairTones = ($numHairTones + 1.0);
        $numHairTones[$hairTones @ $numHairTones] = "red";
        $numHairTones = ($numHairTones + 1.0);
        $numHairTones[$hairTones @ $numHairTones] = "black";
        $numHairTones = ($numHairTones + 1.0);
    }
    "hair".UseMeshRandom(%this);
    %tone = ;
    %tone @ ".hair".UseSkinTone(%this);
    return;
};
function serverCmdUseClothesRandom(%client) {
    if (!(isObject(%client.Player))) {
        return;
    }
    %client.Player.UseClothesRandom();
    return;
};
function Player::UseClothesRandom(%this) {
    "feet".UseMeshRandom(%this);
    "legs".UseMeshRandom(%this);
    "torso".UseMeshRandom(%this);
    "glasses".UseMeshRandom(%this);
    return;
};
