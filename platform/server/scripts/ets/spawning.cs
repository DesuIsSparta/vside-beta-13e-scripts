function SpawnSphere::choosePointOnCenterPlane(%this) {
    %trans = %this.getTransform();
    %posX = getWord(%trans, 0);
    %posY = getWord(%trans, 1);
    %posZ = getWord(%trans, 2);
    %retries = 7;
    %good = 0;
    %n = 0;
    while ((%n < 10.0)) {
        %tryX = getRandom(-(1000.0), 1000);
        %tryY = getRandom(-(1000.0), 1000);
        if ((((%tryX * %tryX) + (%tryY * %tryY)) < (1000.0 * 1000.0))) {
            %good = 1;
        }
        %n = (%n + 1.0);
    }
    %posX = (%posX + ((%tryX * 0.001) * %this.radius));
    (%n < 10.0);
    %posY = (%posY + ((%tryY * 0.001) * %this.radius));
    return %posX @ " " @ %posY @ " " @ %posZ;
};
function SpawnSphere::getEmptySpot(%this, %minSeparation, %exclude, %alignToSphere) {
    %minSep2 = (%minSeparation * %minSeparation);
    %num = MissionCleanup.getCount();
    %retries = 20;
    %good = 0;
    %m = 0;
    while ((%m < %retries)) {
        %candidate = %this.choosePointOnCenterPlane();
        %cdX = getWord(%candidate, 0);
        %cdY = getWord(%candidate, 1);
        %tooClose = 0;
        %n = 0;
        while ((%n < %num)) {
            %item = %n.getObject(MissionCleanup);
            if ((%item != %exclude)) {
            }
            if ((%item.getClassName() $= "Player")) {
            }
            if ((%item.getClassName() $= "AIPlayer")) {
                %itTrans = %item.getTransform();
                %itX = getWord(%itTrans, 0);
                %itY = getWord(%itTrans, 1);
                %dx = (%itX - %cdX);
                %dy = (%itY - %cdY);
                %sep2 = ((%dx * %dx) + (%dy * %dy));
                if ((%sep2 < %minSep2)) {
                    %tooClose = 1;
                }
            }
            %n = (%n + 1.0);
        }
        if (!(%tooClose)) {
            %good = 1;
            (%n < %num);
        }
        %m = (%m + 1.0);
    }
    %rot = "0 0 1";
    (%m < %retries);
    if ((%m >= %retries)) {
        echo("\x03 could not find empty spot");
        %rot = "0 0 -1";
    }
    %theta = ((getRandom(0, 360) * 3.41593) / 180.0);
    %rot = %rot @ " " @ %theta;
    if (%alignToSphere) {
        %rot = getWords(%this.getTransform(), 3);
    }
    %ret = %candidate @ " " @ %rot;
    return %ret;
};
function SpawnSphere::spawnBots(%this, %num, %sep) {
    %n = 0;
    while ((%n < %num)) {
        AIManager::SpawnETS(AIManager, 0.getEmptySpot(%this, %sep, 0));
        %n = (%n + 1.0);
    }
};
function SpawnSphere::spawnBotsDensity(%this, %density, %sep) {
    %spnArea = ((%this.radius * %this.radius) * 3.14159);
    %botArea = (%sep / 2.0);
    %botArea = ((%botArea * %botArea) * 3.14159);
    %num = (((%spnArea / %botArea) * 0.9) * %density);
    echo("spawning" @ " " @ %num @ " " @ "bots..");
    %num.spawnBots(%this);
    return;
};
function serverCmdAddBotsToSpawnSphere(%unused, %unused, %num, %sep) {
    %sep.spawnBots(EntrySpawn, %num);
    return;
};
function Player::teleportToRandomSpawnSphere(%this) {
    %chosen = "";
    if (!(isObject(PlayerDropPoints))) {
        %chosen = EntrySpawn;
    }
    %chosen = getRandom(0, (PlayerDropPoints.getCount() - 1.0)).getObject(PlayerDropPoints);
    if (!(isObject(%chosen))) {
        error("This is all messed up. No known spawn spheres!");
    }
    %pos = 0.getEmptySpot(%chosen, 1, 0);
    %rot = getWords(%this.getTransform(), 3, 4);
    %pos @ " " @ %rot.setTransform(%this);
    "0 0 5".setVelocity(%this);
    return;
};
