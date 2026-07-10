function serverCmdUseMesh(%client, %mesh) {
    if (!(isObject(%client.Player))) {
        return;
    }
    %client.Player.UseMesh(%mesh);
    return;
};
function serverCmdUseMeshRandom(%client, %category) {
    if (!(isObject(%client.Player))) {
        return;
    }
    %client.Player.UseMeshRandom(%category);
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
    %client.Player.UseSkinTone(%tone);
    return;
};
function Player::UseSkinTone(%this, %tone) {
    %this.setSkinName(%tone);
    return;
};
function Player::UseSkinToneRandom(%this) {
    if (!($numSkinTones)) {
        $numSkinTones = 0;
        $numSkinTones[$skinTones @ $numSkinTones] = "base";
        $numSkinTones = (1.0 + $numSkinTones);
        $numSkinTones[$skinTones @ $numSkinTones] = "tan";
        $numSkinTones = (1.0 + $numSkinTones);
        $numSkinTones[$skinTones @ $numSkinTones] = "dark";
        $numSkinTones = (1.0 + $numSkinTones);
    }
    %tone = ;
    %face = getRandom(1, 4);
    %this.UseSkinTone(%tone @ ".body");
    %this.UseSkinTone(%tone @ %face @ ".face");
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
        $numHairTones = (1.0 + $numHairTones);
        $numHairTones[$hairTones @ $numHairTones] = "red";
        $numHairTones = (1.0 + $numHairTones);
        $numHairTones[$hairTones @ $numHairTones] = "black";
        $numHairTones = (1.0 + $numHairTones);
    }
    %this.UseMeshRandom("hair");
    %tone = ;
    %this.UseSkinTone(%tone @ ".hair");
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
    %this.UseMeshRandom("feet");
    %this.UseMeshRandom("legs");
    %this.UseMeshRandom("torso");
    %this.UseMeshRandom("glasses");
    return;
};
