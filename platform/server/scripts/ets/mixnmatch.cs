function serverCmdUseMesh(%client, %mesh)
{
    if (!isObject(%client.Player))
    {
        return ;
    }
    %client.Player.UseMesh(%mesh);
    return ;
}
function serverCmdUseMeshRandom(%client, %category)
{
    if (!isObject(%client.Player))
    {
        return ;
    }
    %client.Player.UseMeshRandom(%category);
    return ;
}
function serverCmdUseSkinToneRandom(%client)
{
    if (!isObject(%client.Player))
    {
        return ;
    }
    %client.Player.UseSkinToneRandom();
    return ;
}
function serverCmdUseSkinTone(%client, %tone)
{
    if (!isObject(%client.Player))
    {
        return ;
    }
    %client.Player.UseSkinTone(%tone);
    return ;
}
function Player::UseSkinTone(%this, %tone)
{
    %this.setSkinName(%tone);
    return ;
}
function Player::UseSkinToneRandom(%this)
{
    if (!$numSkinTones)
    {
        $numSkinTones = 0;
        $skinTones[$numSkinTones] = "base";
        $numSkinTones = $numSkinTones + 1;
        $skinTones[$numSkinTones] = "tan";
        $numSkinTones = $numSkinTones + 1;
        $skinTones[$numSkinTones] = "dark";
        $numSkinTones = $numSkinTones + 1;
    }
    %tone = $skinTones[getRandom(0, $numSkinTones - 1)];
    %face = getRandom(1, 4);
    %this.UseSkinTone(%tone @ ".body");
    %this.UseSkinTone(%tone @ %face @ ".face");
    return ;
}
function serverCmdUseHairRandom(%client)
{
    if (!isObject(%client.Player))
    {
        return ;
    }
    %client.Player.UseHairRandom();
    return ;
}
function Player::UseHairRandom(%this)
{
    if (!$numHairTones)
    {
        $numHairTones = 0;
        $hairTones[$numHairTones] = "base";
        $numHairTones = $numHairTones + 1;
        $hairTones[$numHairTones] = "red";
        $numHairTones = $numHairTones + 1;
        $hairTones[$numHairTones] = "black";
        $numHairTones = $numHairTones + 1;
    }
    %this.UseMeshRandom("hair");
    %tone = $hairTones[getRandom(0, $numHairTones - 1)];
    %this.UseSkinTone(%tone @ ".hair");
    return ;
}
function serverCmdUseClothesRandom(%client)
{
    if (!isObject(%client.Player))
    {
        return ;
    }
    %client.Player.UseClothesRandom();
    return ;
}
function Player::UseClothesRandom(%this)
{
    %this.UseMeshRandom("feet");
    %this.UseMeshRandom("legs");
    %this.UseMeshRandom("torso");
    %this.UseMeshRandom("glasses");
    return ;
}
