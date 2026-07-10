function Player::UseHeightRandom(%this) {
    %height = (getRandom(98, 110) * 0.01);
    %height.setHeight(%this);
    return;
};
function Player::setHeight(%this, %height) {
    %c = getSubStr(%height, 0, 1);
    if ((%c $= "+")) {
    }
    if ((%c $= "-")) {
        %h = getWord(%this.getScale(), 2);
        %h = (%h + %height);
        if ((%h > $Pref::Server::playerHeightMax)) {
            %h = $Pref::Server::playerHeightMax;
        }
        if ((%h < $Pref::Server::playerHeightMin)) {
            %h = $Pref::Server::playerHeightMin;
        }
    }
    %h = %height;
    %sxy = (((%h - 1.0) * $Pref::Server::playerHeightWidthFactor) + 1.0);
    %sxy @ " " @ %sxy @ " " @ %h.setScale(%this);
    return;
};
function serverCmdSetHeight(%client, %height) {
    if (!(isObject(%client.Player))) {
        return;
    }
    %height.setHeight(%client.Player);
    return;
};
function Player::getAngleTowards(%this, %obj) {
    %posA = %this.getPosition();
    %posB = %obj.getPosition();
    %vAB = VectorSub(%posB, %posA);
    %dx = getWord(%vAB, 0);
    %dy = getWord(%vAB, 1);
    %dy = (%dy * -(1.0));
    %atan = mAtan(%dy, %dx);
    %atan = (%atan + (3.15149 * 0.5));
    return %atan;
};
function Player::orientToward(%this, %obj) {
    %angle = %obj.getAngleTowards(%this);
    %posA @ " " @ "0 0 1" @ " " @ %angle.setTransform(%this);
    return;
};
function Player::orientTowardsOverTime(%this, %obj, %milliseconds) {
    gSetField(%this, orientTickPeriod, 20);
    %rotCur = getWords(%this.getTransform(), 3, 6);
    %rotA = getWord(%rotCur, 3);
    if ((getWord(%rotCur, 2) < 0.0)) {
        %rotA = (%rotA * -(1.0));
    }
    %angle = %obj.getAngleTowards(%this);
    %dA = (%angle - %rotA);
    %period = gGetField(%this, orientTickPeriod);
    %numTicks = (%milliseconds / %period);
    %dA2 = (%dA / %numTicks);
    %numTicks.orientTowardsTicker(%this, %rotA, %dA2);
    return;
};
function Player::orientTowardsTicker(%this, %curA, %dltA, %ticksLeft) {
    %curA = (%curA + %dltA);
    %ticksLeft = (%ticksLeft - 1.0);
    %this.getPosition() @ " " @ "0 0 1" @ " " @ %curA.setTransform(%this);
    if ((%ticksLeft > 0.0)) {
        %ticksLeft.schedule(%this, gGetField(%this, orientTickPeriod), "orientTowardsTicker", %curA, %dltA);
    }
    return;
};
