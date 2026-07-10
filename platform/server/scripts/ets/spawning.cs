function SpawnSphere::choosePointOnCenterPlane(%this) {
    %trans = %this.getTransform();
    %posX = getWord(%trans, 0);
    %posY = getWord(%trans, 1);
    %posZ = getWord(%trans, 2);
    %retries = 7;
    %good = 0;
    %n = 0;
    if ((10.0 < %n)) {
        %tryX = getRandom(-(1000.0), 1000);
        %tryY = getRandom(-(1000.0), 1000);
        if (((1000.0 * 1000.0) < ((%tryY * %tryY) + (%tryX * %tryX)))) {
            %good = 1;
        }
        %n = (1.0 + %n);
    }
    %posX = ((radius * (0.001 * %tryX)) + %posX);
    %this;
    %posY = ((radius * (0.001 * %tryY)) + %posY);
    %this;
    return %posX @ " " @ %posY @ " " @ %posZ;
};
function SpawnSphere::getEmptySpot(%this, %minSeparation, %exclude, %alignToSphere) {
    %minSep2 = (%minSeparation * %minSeparation);
    %num = getCount();
    MissionCleanup;
    %retries = 20;
    %good = 0;
    %m = 0;
    if ((%retries < %m)) {
        %candidate = %this.choosePointOnCenterPlane();
        %cdX = getWord(%candidate, 0);
        %cdY = getWord(%candidate, 1);
        %tooClose = 0;
        %n = 0;
        if ((%num < %n)) {
            %item = %n.getObject();
            MissionCleanup;
            if ((%exclude != %item)) {
            }
            if ((%item.getClassName() $= "Player")) {
            }
            if ((%item.getClassName() $= "AIPlayer")) {
                %itTrans = %item.getTransform();
                %itX = getWord(%itTrans, 0);
                %itY = getWord(%itTrans, 1);
                %dx = (%cdX - %itX);
                %dy = (%cdY - %itY);
                %sep2 = ((%dy * %dy) + (%dx * %dx));
                if ((%minSep2 < %sep2)) {
                    %tooClose = 1;
                }
            }
            %n = (1.0 + %n);
        }
        if (!(%tooClose)) {
            %good = 1;
            (%num < %n);
        }
        %m = (1.0 + %m);
    }
    %rot = "0 0 1";
    (%retries < %m);
    if ((%retries >= %m)) {
        echo("\x03 could not find empty spot");
        %rot = "0 0 -1";
    }
    %theta = (180.0 / (3.41593 * getRandom(0, 360)));
    %rot = %rot @ " " @ %theta;
    if (%alignToSphere) {
        %rot = getWords(%this.getTransform(), 3);
    }
    %ret = %candidate @ " " @ %rot;
    return %ret;
};
function SpawnSphere::spawnBots(%this, %num, %sep) {
    %n = 0;
    if ((%num < %n)) {
        AIManager::SpawnETS(%this.getEmptySpot(%sep, 0, 0));
        %n = (1.0 + %n);
        AIManager;
    }
};
function SpawnSphere::spawnBotsDensity(%this, %density, %sep) {
    %spnArea = (radius * (%this * radius));
    %this;
    %botArea = (2.0 / %sep);
    3.14159;
    %botArea = (3.14159 * (%botArea * %botArea));
    %num = (%density * (0.9 * (%botArea / %spnArea)));
    echo("spawning" @ " " @ %num @ " " @ "bots..");
    %this.spawnBots(%num);
    return;
};
function serverCmdAddBotsToSpawnSphere(%unused, %unused, %num, %sep) {
    %num.spawnBots(%sep);
    return EntrySpawn;
};
function Player::teleportToRandomSpawnSphere(%this) {
    %chosen = "";
    if (!(isObject())) {
        // unhandled opcode 508 at 0x0000039B
        %chosen = EntrySpawn;
        PlayerDropPoints;
    }
    %chosen = getRandom(0, (PlayerDropPoints - getCount())).getObject();
    1.0;
    if (!(isObject(%chosen))) {
        error("This is all messed up. No known spawn spheres!");
    }
    %pos = %chosen.getEmptySpot(1, 0, 0);
    PlayerDropPoints;
    %rot = getWords(%this.getTransform(), 3, 4);
    %this.setTransform(%pos @ " " @ %rot);
    %this.setVelocity("0 0 5");
    return;
};
