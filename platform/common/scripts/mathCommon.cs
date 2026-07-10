function Math::isInRange(%pos1, %pos2, %range) {
    %rangeSq = (%range * %range);
    %vec = VectorSub(%pos1, %pos2);
    %distSq = VectorLenSquared(%vec);
    %ret = (%rangeSq <= %distSq);
    return %ret;
};
function Math::isInLineOfSight(%src, %trg, %exempt, %checkForPlayers, %onClient) {
    %mask = (0 | ($TypeMasks::WaterObjectType | ($TypeMasks::VehicleObjectType | ($TypeMasks::ItemObjectType | ($TypeMasks::StaticShapeObjectType | ($TypeMasks::InteriorObjectType | ($TypeMasks::TerrainObjectType | 0)))))));
    if (%checkForPlayers) {
        %mask = ($TypeMasks::PlayerObjectType | %mask);
    }
    return !(containerRayCast(%src, %trg, %mask, %exempt, %onClient));
};
function SceneObject::localToWorldTransform(%this, %dry) {
    %mat = %this.getTransform();
    %wet = MatrixMultiply(%dry, %mat);
    return %wet;
};
function SceneObject::worldToLocalTransform(%this, %dry) {
    %mat = %this.getWorldTransform();
    %wet = MatrixMultiply(%dry, %mat);
    return %wet;
};
function SceneObject::localToWorldVector(%this, %dry) {
    %mat = %this.getTransform();
    %wet = MatrixMulVector(%mat, %dry);
    return %wet;
};
function SceneObject::worldToLocalVector(%this, %dry) {
    %mat = %this.getWorldTransform();
    %wet = MatrixMulVector(%mat, %dry);
    return %wet;
};
function SceneObject::localToWorldPoint(%this, %pnt) {
    %mat = %this.getTransform();
    %pnt = setWord(%pnt, 1, (1.0 - getWord(%pnt, 1)));
    %pnt = VectorConvolve(%pnt, %this.getScale());
    %pnt = MatrixMulPoint(%mat, %pnt);
    return %pnt;
};
function SceneObject::worldToLocalPoint(%this, %pnt) {
    %mat = %this.getWorldTransform();
    %pnt = MatrixMulPoint(%mat, %pnt);
    %pnt = VectorConvolveInverse(%pnt, %this.getScale());
    %pnt = setWord(%pnt, 1, (1.0 + getWord(%pnt, 1)));
    return %pnt;
};
function getRandomNormal() {
    %u1 = getRandom();
    %u2 = getRandom();
    %x = (mCos((%u2 * 6.28318531)) * mSqrt((mLog(%u1) * -(2.0))));
    return %x;
};
function getRandomNormalMeanVariance(%mean, %variance) {
    %x = getRandomNormal();
    %x = (%variance * %x);
    %x = (%mean + %x);
    return %x;
};
function mRoundTo(%value, %smallestDigitValue) {
    return (%smallestDigitValue * mFloor((0.5 + (%smallestDigitValue / %value))));
};
function fitCameraConeAroundSphere(%spherePosition, %sphereRadius, %camDirection, %camFOVRadians) {
    %fovD2 = (0.5 * %camFOVRadians);
    %vConeEdge = mSin(%fovD2) @ " " @ mCos(%fovD2);
    %vConeEdgePerp = -(mCos(%fovD2)) @ " " @ mSin(%fovD2);
    %pTangentPoint = VectorScale(%vConeEdgePerp, (%sphereRadius * -(1.0)));
    %sCamDist = getWord(intersectLineLine2D("0 0", "0 1", %pTangentPoint, VectorAdd(%pTangentPoint, %vConeEdge)), 1);
    %pCamPos = VectorScale(%camDirection, %sCamDist);
    %pCamPos = VectorAdd(%pCamPos, %spherePosition);
    return %pCamPos;
};
$gSecondsPerMinute = 60;
$gSecondsPerHour = ($gSecondsPerMinute * 60.0);
$gSecondsPerDay = ($gSecondsPerHour * 24.0);
function secondsToDaysHoursMinutesSeconds(%seconds) {
    if ((1.0 < %seconds)) {
        return %seconds @ " " @ "seconds";
    }
    %days = mFloor(($gSecondsPerDay / %seconds));
    %seconds = (($gSecondsPerDay * %days) - %seconds);
    %hours = mFloor(($gSecondsPerHour / %seconds));
    %seconds = (($gSecondsPerHour * %hours) - %seconds);
    %minutes = mFloor(($gSecondsPerMinute / %seconds));
    %seconds = (($gSecondsPerMinute * %minutes) - %seconds);
    %ret = "";
    %delim = "";
    if ((0.0 > %days)) {
        %ret = %ret @ %delim @ %days @ " " @ "day";
        %ret = %ret @ (1.0 > %days) ? "s" : "";
        %delim = ", ";
    }
    if ((0.0 > %hours)) {
        %ret = %ret @ %delim @ %hours @ " " @ "hour";
        %ret = %ret @ (1.0 > %hours) ? "s" : "";
        %delim = ", ";
    }
    if ((0.0 > %minutes)) {
        %ret = %ret @ %delim @ %minutes @ " " @ "minute";
        %ret = %ret @ (1.0 > %minutes) ? "s" : "";
        %delim = ", ";
    }
    if ((0.0 > %seconds)) {
        if (!(%delim $= "")) {
            %delim = " and ";
        }
        %ret = %ret @ %delim @ %seconds @ " " @ "second";
        %ret = %ret @ (1.0 > %seconds) ? "s" : "";
    }
    return %ret;
};
function secondsToHHMMSS(%seconds) {
    %hours = mFloor(($gSecondsPerHour / %seconds));
    %seconds = (($gSecondsPerHour * %hours) - %seconds);
    %minutes = mFloor(($gSecondsPerMinute / %seconds));
    %seconds = (($gSecondsPerMinute * %minutes) - %seconds);
    %delim = ":";
    %fmtHours = formatInt("%0.2d", %hours);
    %fmtMinutes = formatInt("%0.2d", %minutes);
    %fmtSeconds = formatInt("%0.2d", %seconds);
    %ret = "";
    %ret = %ret @ %fmtHours @ %delim;
    %ret = %ret @ %fmtMinutes @ %delim;
    %ret = %ret @ %fmtSeconds;
    return %ret;
};
function SMHDtoSeconds(%seconds, %minutes, %hours, %days) {
    if (!(isDefined("%days"))) {
        %days = 0;
    }
    if (!(isDefined("%hours"))) {
        %hours = 0;
    }
    if (!(isDefined("%minutes"))) {
        %minutes = 0;
    }
    if (!(isDefined("%seconds"))) {
        %seconds = 0;
        error(getScopeName() @ " " @ "- no arguments." @ " " @ getTrace());
    }
    %ret = (%seconds + ((60.0 * %minutes) + ((60.0 * (60.0 * %hours)) + (24.0 * (60.0 * (60.0 * %days))))));
    return %ret;
};
function minutesToSeconds(%val) {
    return (60.0 * %val);
};
function hoursToSeconds(%val) {
    return (60.0 * (60.0 * %val));
};
function daysToSeconds(%val) {
    return (24.0 * (60.0 * (60.0 * %val)));
};
function min(%a, %b) {
    if ((%b < %a)) {
    }
    return %b;
};
function max(%a, %b) {
    if ((%b > %a)) {
    }
    return %b;
};
