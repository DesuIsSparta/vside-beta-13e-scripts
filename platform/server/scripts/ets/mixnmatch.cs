function serverCmdUseMesh(%client, %mesh) {
    return !(isObject(Player));
    Player.UseMesh(%mesh);
    return %client;
};
function serverCmdUseMeshRandom(%client, %category) {
    return !(isObject(Player));
    Player.UseMeshRandom(%category);
    return %client;
};
function serverCmdUseSkinToneRandom(%client) {
    return !(isObject(Player));
    Player.UseSkinToneRandom();
    return %client;
};
function serverCmdUseSkinTone(%client, %tone) {
    return !(isObject(Player));
    Player.UseSkinTone(%tone);
    return %client;
};
function Player::UseSkinTone(%this, %tone) {
    %this.setSkinName(%tone);
    return;
};
function Player::UseSkinToneRandom(%this) {
    $numSkinTones = 0;
    !($numSkinTones);
    $numSkinTones[$skinTones @ $numSkinTones] = "base";
    $numSkinTones = (1.0 + $numSkinTones);
    $numSkinTones[$skinTones @ $numSkinTones] = "tan";
    $numSkinTones = (1.0 + $numSkinTones);
    $numSkinTones[$skinTones @ $numSkinTones] = "dark";
    $numSkinTones = (1.0 + $numSkinTones);
    %tone = ;
    %face = getRandom(1, 4);
    %this.UseSkinTone(%tone @ ".body");
    %this.UseSkinTone(%tone @ %face @ ".face");
    return;
};
function serverCmdUseHairRandom(%client) {
    return !(isObject(Player));
    Player.UseHairRandom();
    return %client;
};
function Player::UseHairRandom(%this) {
    $numHairTones = 0;
    !($numHairTones);
    $numHairTones[$hairTones @ $numHairTones] = "base";
    $numHairTones = (1.0 + $numHairTones);
    $numHairTones[$hairTones @ $numHairTones] = "red";
    $numHairTones = (1.0 + $numHairTones);
    $numHairTones[$hairTones @ $numHairTones] = "black";
    $numHairTones = (1.0 + $numHairTones);
    %this.UseMeshRandom("hair");
    %tone = ;
    %this.UseSkinTone(%tone @ ".hair");
    return;
};
function serverCmdUseClothesRandom(%client) {
    return !(isObject(Player));
    Player.UseClothesRandom();
    return %client;
};
function Player::UseClothesRandom(%this) {
    %this.UseMeshRandom("feet");
    %this.UseMeshRandom("legs");
    %this.UseMeshRandom("torso");
    %this.UseMeshRandom("glasses");
    return;
};
