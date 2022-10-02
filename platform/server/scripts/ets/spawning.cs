function SpawnSphere::choosePointOnCenterPlane(%this)
{
    %trans = %this.getTransform();
    %posX = getWord(%trans, 0);
    %posY = getWord(%trans, 1);
    %posZ = getWord(%trans, 2);
    %retries = 7;
    %good = 0;
    %n = 0;
    while (%n < 10)
    {
        %tryX = getRandom(-1000, 1000);
        %tryY = getRandom(-1000, 1000);
        if (((%tryX * %tryX) + (%tryY * %tryY)) < (1000 * 1000))
        {
            %good = 1;
        }
        %n = %n + 1;
    }
    %posX = %posX + ((%tryX * 0.001) * %this.radius);
    %posY = %posY + ((%tryY * 0.001) * %this.radius);
    return %posX SPC %posY SPC %posZ;
}
function SpawnSphere::getEmptySpot(%this, %minSeparation, %exclude, %alignToSphere)
{
    %minSep2 = %minSeparation * %minSeparation;
    %num = MissionCleanup.getCount();
    %retries = 20;
    %good = 0;
    %m = 0;
    while (%m < %retries)
    {
        %candidate = %this.choosePointOnCenterPlane();
        %cdX = getWord(%candidate, 0);
        %cdY = getWord(%candidate, 1);
        %tooClose = 0;
        %n = 0;
        while (%n < %num)
        {
            %item = MissionCleanup.getObject(%n);
            if (((%item != %exclude) && (%item.getClassName() $= "Player")) || (%item.getClassName() $= "AIPlayer"))
            {
                %itTrans = %item.getTransform();
                %itX = getWord(%itTrans, 0);
                %itY = getWord(%itTrans, 1);
                %dx = %itX - %cdX;
                %dy = %itY - %cdY;
                %sep2 = (%dx * %dx) + (%dy * %dy);
                if (%sep2 < %minSep2)
                {
                    %tooClose = 1;
                }
            }
            %n = %n + 1;
        }
        if (!%tooClose)
        {
            %good = 1;
        }
        %m = %m + 1;
    }
    %rot = "0 0 1";
    if (%m >= %retries)
    {
        echo("\c2 could not find empty spot");
        %rot = "0 0 -1";
    }
    %theta = (getRandom(0, 360) * 3.41593) / 180;
    %rot = %rot SPC %theta;
    if (%alignToSphere)
    {
        %rot = getWords(%this.getTransform(), 3);
    }
    %ret = %candidate SPC %rot;
    return %ret;
}
function SpawnSphere::spawnBots(%this, %num, %sep)
{
    %n = 0;
    while (%n < %num)
    {
        AIManager::SpawnETS(AIManager, %this.getEmptySpot(%sep, 0, 0));
        %n = %n + 1;
    }
}

function SpawnSphere::spawnBotsDensity(%this, %density, %sep)
{
    %spnArea = (%this.radius * %this.radius) * 3.14159;
    %botArea = %sep / 2;
    %botArea = (%botArea * %botArea) * 3.14159;
    %num = ((%spnArea / %botArea) * 0.9) * %density;
    echo("spawning" SPC %num SPC "bots..");
    %this.spawnBots(%num);
    return ;
}
function serverCmdAddBotsToSpawnSphere(%unused, %unused, %num, %sep)
{
    EntrySpawn.spawnBots(%num, %sep);
    return ;
}
function Player::teleportToRandomSpawnSphere(%this)
{
    %chosen = "";
    if (!isObject(PlayerDropPoints))
    {
        %chosen = EntrySpawn;
    }
    else
    {
        %chosen = PlayerDropPoints.getObject(getRandom(0, PlayerDropPoints.getCount() - 1));
    }
    if (!isObject(%chosen))
    {
        error("This is all messed up. No known spawn spheres!");
    }
    %pos = %chosen.getEmptySpot(1, 0, 0);
    %rot = getWords(%this.getTransform(), 3, 4);
    %this.setTransform(%pos SPC %rot);
    %this.setVelocity("0 0 5");
    return ;
}
