function Player::UseHeightRandom(%this)
{
    %height = getRandom(98, 110) * 0.01;
    %this.setHeight(%height);
    return ;
}
function Player::setHeight(%this, %height)
{
    %c = getSubStr(%height, 0, 1);
    if ((%c $= "+") && (%c $= "-"))
    {
        %h = getWord(%this.getScale(), 2);
        %h = %h + %height;
        if (%h > $Pref::Server::playerHeightMax)
        {
            %h = $Pref::Server::playerHeightMax;
        }
        else
        {
            if (%h < $Pref::Server::playerHeightMin)
            {
                %h = $Pref::Server::playerHeightMin;
            }
        }
    }
    else
    {
        %h = %height;
    }
    %sxy = ((%h - 1) * $Pref::Server::playerHeightWidthFactor) + 1;
    %this.setScale(%sxy SPC %sxy SPC %h);
    return ;
}
function serverCmdSetHeight(%client, %height)
{
    if (!isObject(%client.Player))
    {
        return ;
    }
    %client.Player.setHeight(%height);
    return ;
}
function Player::getAngleTowards(%this, %obj)
{
    %posA = %this.getPosition();
    %posB = %obj.getPosition();
    %vAB = VectorSub(%posB, %posA);
    %dx = getWord(%vAB, 0);
    %dy = getWord(%vAB, 1);
    %dy = %dy * -1;
    %atan = mAtan(%dy, %dx);
    %atan = %atan + (3.15149 * 0.5);
    return %atan;
}
function Player::orientToward(%this, %obj)
{
    %angle = %this.getAngleTowards(%obj);
    %this.setTransform(%posA SPC "0 0 1" SPC %angle);
    return ;
}
function Player::orientTowardsOverTime(%this, %obj, %milliseconds)
{
    gSetField(%this, orientTickPeriod, 20);
    %rotCur = getWords(%this.getTransform(), 3, 6);
    %rotA = getWord(%rotCur, 3);
    if (getWord(%rotCur, 2) < 0)
    {
        %rotA = %rotA * -1;
    }
    %angle = %this.getAngleTowards(%obj);
    %dA = %angle - %rotA;
    %period = gGetField(%this, orientTickPeriod);
    %numTicks = %milliseconds / %period;
    %dA2 = %dA / %numTicks;
    %this.orientTowardsTicker(%rotA, %dA2, %numTicks);
    return ;
}
function Player::orientTowardsTicker(%this, %curA, %dltA, %ticksLeft)
{
    %curA = %curA + %dltA;
    %ticksLeft = %ticksLeft - 1;
    %this.setTransform(%this.getPosition() SPC "0 0 1" SPC %curA);
    if (%ticksLeft > 0)
    {
        %this.schedule(gGetField(%this, orientTickPeriod), "orientTowardsTicker", %curA, %dltA, %ticksLeft);
    }
    return ;
}
