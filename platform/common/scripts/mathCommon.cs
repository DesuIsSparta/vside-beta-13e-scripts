function Math::isInRange(%pos1, %pos2, %range)
{
    %rangeSq = %range * %range;
    %vec = VectorSub(%pos1, %pos2);
    %distSq = VectorLenSquared(%vec);
    %ret = %distSq <= %rangeSq;
    return %ret;
}
function Math::isInLineOfSight(%src, %trg, %exempt, %checkForPlayers, %onClient)
{
    %mask = ((((((0 | $TypeMasks::TerrainObjectType) | $TypeMasks::InteriorObjectType) | $TypeMasks::StaticShapeObjectType) | $TypeMasks::ItemObjectType) | $TypeMasks::VehicleObjectType) | $TypeMasks::WaterObjectType) | 0;
    if (%checkForPlayers)
    {
        %mask = %mask | $TypeMasks::PlayerObjectType;
    }
    return !containerRayCast(%src, %trg, %mask, %exempt, %onClient);
}
function SceneObject::localToWorldTransform(%this, %dry)
{
    %mat = %this.getTransform();
    %wet = MatrixMultiply(%dry, %mat);
    return %wet;
}
function SceneObject::worldToLocalTransform(%this, %dry)
{
    %mat = %this.getWorldTransform();
    %wet = MatrixMultiply(%dry, %mat);
    return %wet;
}
function SceneObject::localToWorldVector(%this, %dry)
{
    %mat = %this.getTransform();
    %wet = MatrixMulVector(%mat, %dry);
    return %wet;
}
function SceneObject::worldToLocalVector(%this, %dry)
{
    %mat = %this.getWorldTransform();
    %wet = MatrixMulVector(%mat, %dry);
    return %wet;
}
function SceneObject::localToWorldPoint(%this, %pnt)
{
    %mat = %this.getTransform();
    %pnt = setWord(%pnt, 1, getWord(%pnt, 1) - 1);
    %pnt = VectorConvolve(%pnt, %this.getScale());
    %pnt = MatrixMulPoint(%mat, %pnt);
    return %pnt;
}
function SceneObject::worldToLocalPoint(%this, %pnt)
{
    %mat = %this.getWorldTransform();
    %pnt = MatrixMulPoint(%mat, %pnt);
    %pnt = VectorConvolveInverse(%pnt, %this.getScale());
    %pnt = setWord(%pnt, 1, getWord(%pnt, 1) + 1);
    return %pnt;
}
function getRandomNormal()
{
    %u1 = getRandom();
    %u2 = getRandom();
    %x = mSqrt(-2 * mLog(%u1)) * mCos(6.28319 * %u2);
    return %x;
}
function getRandomNormalMeanVariance(%mean, %variance)
{
    %x = getRandomNormal();
    %x = %x * %variance;
    %x = %x + %mean;
    return %x;
}
function mRoundTo(%value, %smallestDigitValue)
{
    return mFloor((%value / %smallestDigitValue) + 0.5) * %smallestDigitValue;
}
function fitCameraConeAroundSphere(%spherePosition, %sphereRadius, %camDirection, %camFOVRadians)
{
    %fovD2 = %camFOVRadians * 0.5;
    %vConeEdge = mSin(%fovD2) SPC mCos(%fovD2);
    %vConeEdgePerp = -mCos(%fovD2) SPC mSin(%fovD2);
    %pTangentPoint = VectorScale(%vConeEdgePerp, -1 * %sphereRadius);
    %sCamDist = getWord(intersectLineLine2D("0 0", "0 1", %pTangentPoint, VectorAdd(%pTangentPoint, %vConeEdge)), 1);
    %pCamPos = VectorScale(%camDirection, %sCamDist);
    %pCamPos = VectorAdd(%pCamPos, %spherePosition);
    return %pCamPos;
}
$gSecondsPerMinute = 60;
$gSecondsPerHour = 60 * $gSecondsPerMinute;
$gSecondsPerDay = 24 * $gSecondsPerHour;
function secondsToDaysHoursMinutesSeconds(%seconds)
{
    if (%seconds < 1)
    {
        return %seconds SPC "seconds";
    }
    %days = mFloor(%seconds / $gSecondsPerDay);
    %seconds = %seconds - (%days * $gSecondsPerDay);
    %hours = mFloor(%seconds / $gSecondsPerHour);
    %seconds = %seconds - (%hours * $gSecondsPerHour);
    %minutes = mFloor(%seconds / $gSecondsPerMinute);
    %seconds = %seconds - (%minutes * $gSecondsPerMinute);
    %ret = "";
    %delim = "";
    if (%days > 0)
    {
        %ret = %ret @ %delim @ %days SPC "day";
        %ret = %ret @ %days > 1 ? "s" : "";
        %delim = ", ";
    }
    if (%hours > 0)
    {
        %ret = %ret @ %delim @ %hours SPC "hour";
        %ret = %ret @ %hours > 1 ? "s" : "";
        %delim = ", ";
    }
    if (%minutes > 0)
    {
        %ret = %ret @ %delim @ %minutes SPC "minute";
        %ret = %ret @ %minutes > 1 ? "s" : "";
        %delim = ", ";
    }
    if (%seconds > 0)
    {
        if (!(%delim $= ""))
        {
            %delim = " and ";
        }
        %ret = %ret @ %delim @ %seconds SPC "second";
        %ret = %ret @ %seconds > 1 ? "s" : "";
    }
    return %ret;
}
function secondsToHHMMSS(%seconds)
{
    %hours = mFloor(%seconds / $gSecondsPerHour);
    %seconds = %seconds - (%hours * $gSecondsPerHour);
    %minutes = mFloor(%seconds / $gSecondsPerMinute);
    %seconds = %seconds - (%minutes * $gSecondsPerMinute);
    %delim = ":";
    %fmtHours = formatInt("%0.2d", %hours);
    %fmtMinutes = formatInt("%0.2d", %minutes);
    %fmtSeconds = formatInt("%0.2d", %seconds);
    %ret = "";
    %ret = %ret @ %fmtHours @ %delim;
    %ret = %ret @ %fmtMinutes @ %delim;
    %ret = %ret @ %fmtSeconds;
    return %ret;
}
function SMHDtoSeconds(%seconds, %minutes, %hours, %days)
{
    if (!isDefined("%days"))
    {
        %days = 0;
    }
    if (!isDefined("%hours"))
    {
        %hours = 0;
    }
    if (!isDefined("%minutes"))
    {
        %minutes = 0;
    }
    if (!isDefined("%seconds"))
    {
        %seconds = 0;
        error(getScopeName() SPC "- no arguments." SPC getTrace());
    }
    %ret = (((((%days * 60) * 60) * 24) + ((%hours * 60) * 60)) + (%minutes * 60)) + %seconds;
    return %ret;
}
function minutesToSeconds(%val)
{
    return %val * 60;
}
function hoursToSeconds(%val)
{
    return (%val * 60) * 60;
}
function daysToSeconds(%val)
{
    return ((%val * 60) * 60) * 24;
}
function min(%a, %b)
{
    return %a < %b ? %a : %b;
}
function max(%a, %b)
{
    return %a > %b ? %a : %b;
}
