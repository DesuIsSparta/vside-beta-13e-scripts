exec("projects/common/characters/initReloadable.cs");
datablock AudioProfile(FootLightSoftSound)
{
    fileName = "projects/common/sound/footstep_soft.wav";
    description = AudioClosest3d;
    preload = 1;
};
datablock AudioProfile(FootLightHardSound)
{
    fileName = "projects/common/sound/footstep_hard.wav";
    description = AudioClose3d;
    preload = 1;
};
datablock AudioProfile(FootLightMetalSound)
{
    fileName = "projects/common/sound/footstep_hard.wav";
    description = AudioClose3d;
    preload = 1;
};
datablock AudioProfile(FootLightSnowSound)
{
    fileName = "projects/common/sound/footstep_soft.wav";
    description = AudioClosest3d;
    preload = 1;
};
datablock AudioProfile(FootLightShallowSplashSound)
{
    fileName = "projects/common/sound/footstep_water.wav";
    description = AudioClose3d;
    preload = 1;
};
datablock AudioProfile(FootLightWadingSound)
{
    fileName = "projects/common/sound/footstep_water.wav";
    description = AudioClose3d;
    preload = 1;
};
datablock AudioProfile(FootLightUnderwaterSound)
{
    fileName = "projects/common/sound/footstep_water.wav";
    description = AudioClosest3d;
    preload = 1;
};
datablock AudioProfile(FootLightBubblesSound)
{
    fileName = "projects/common/sound/replaceme.wav";
    description = AudioClose3d;
    preload = 1;
};
datablock AudioProfile(ArmorMoveBubblesSound)
{
    fileName = "projects/common/sound/replaceme.wav";
    description = AudioCloseLooping3d;
    preload = 1;
};
datablock AudioProfile(WaterBreathMaleSound)
{
    fileName = "projects/common/sound/replaceme.wav";
    description = AudioClosestLooping3d;
    preload = 1;
};
datablock AudioProfile(ImpactLightSoftSound)
{
    fileName = "projects/common/sound/replaceme.wav";
    description = AudioClose3d;
    preload = 1;
    effect = ImpactSoftEffect;
};
datablock AudioProfile(ImpactLightHardSound)
{
    fileName = "projects/common/sound/replaceme.wav";
    description = AudioClose3d;
    preload = 1;
    effect = ImpactHardEffect;
};
datablock AudioProfile(ImpactLightMetalSound)
{
    fileName = "projects/common/sound/replaceme.wav";
    description = AudioClose3d;
    preload = 1;
    effect = ImpactMetalEffect;
};
datablock AudioProfile(ImpactLightSnowSound)
{
    fileName = "projects/common/sound/replaceme.wav";
    description = AudioClosest3d;
    preload = 1;
    effect = ImpactSnowEffect;
};
datablock AudioProfile(ImpactLightWaterEasySound)
{
    fileName = "projects/common/sound/replaceme.wav";
    description = AudioClose3d;
    preload = 1;
};
datablock AudioProfile(ImpactLightWaterMediumSound)
{
    fileName = "projects/common/sound/replaceme.wav";
    description = AudioClose3d;
    preload = 1;
};
datablock AudioProfile(ImpactLightWaterHardSound)
{
    fileName = "projects/common/sound/replaceme.wav";
    description = AudioDefault3d;
    preload = 1;
};
datablock AudioProfile(ExitingWaterLightSound)
{
    fileName = "projects/common/sound/replaceme.wav";
    description = AudioClose3d;
    preload = 1;
};
datablock ParticleData(PlayerSplashMist)
{
    dragCoefficient = 2;
    gravityCoefficient = -0.05;
    inheritedVelFactor = 0;
    constantAcceleration = 0;
    lifetimeMS = 400;
    lifetimeVarianceMS = 100;
    useInvAlpha = 0;
    spinRandomMin = -90;
    spinRandomMax = 500;
    textureName = "projects/common/characters/splash";
    colors[0] = "0.7 0.8 1.0 1.0";
    colors[1] = "0.7 0.8 1.0 0.5";
    colors[2] = "0.7 0.8 1.0 0.0";
    sizes[0] = 0.5;
    sizes[1] = 0.5;
    sizes[2] = 0.8;
    times[0] = 0;
    times[1] = 0.5;
    times[2] = 1;
};
datablock ParticleEmitterData(PlayerSplashMistEmitter)
{
    ejectionPeriodMS = 5;
    periodVarianceMS = 0;
    ejectionVelocity = 3;
    velocityVariance = 2;
    ejectionOffset = 0;
    thetaMin = 85;
    thetaMax = 85;
    phiReferenceVel = 0;
    phiVariance = 360;
    overrideAdvance = 0;
    lifetimeMS = 250;
    particles = "PlayerSplashMist";
};
datablock ParticleData(PlayerBubbleParticle)
{
    dragCoefficient = 0;
    gravityCoefficient = -0.5;
    inheritedVelFactor = 0;
    constantAcceleration = 0;
    lifetimeMS = 400;
    lifetimeVarianceMS = 100;
    useInvAlpha = 0;
    textureName = "projects/common/characters/splash";
    colors[0] = "0.7 0.8 1.0 0.4";
    colors[1] = "0.7 0.8 1.0 0.4";
    colors[2] = "0.7 0.8 1.0 0.0";
    sizes[0] = 0.1;
    sizes[1] = 0.3;
    sizes[2] = 0.3;
    times[0] = 0;
    times[1] = 0.5;
    times[2] = 1;
};
datablock ParticleEmitterData(PlayerBubbleEmitter)
{
    ejectionPeriodMS = 1;
    periodVarianceMS = 0;
    ejectionVelocity = 2;
    ejectionOffset = 0.5;
    velocityVariance = 0.5;
    thetaMin = 0;
    thetaMax = 80;
    phiReferenceVel = 0;
    phiVariance = 360;
    overrideAdvance = 0;
    particles = "PlayerBubbleParticle";
};
datablock ParticleData(PlayerFoamParticle)
{
    dragCoefficient = 2;
    gravityCoefficient = -0.05;
    inheritedVelFactor = 0;
    constantAcceleration = 0;
    lifetimeMS = 400;
    lifetimeVarianceMS = 100;
    useInvAlpha = 0;
    spinRandomMin = -90;
    spinRandomMax = 500;
    textureName = "projects/common/characters/splash";
    colors[0] = "0.7 0.8 1.0 0.20";
    colors[1] = "0.7 0.8 1.0 0.20";
    colors[2] = "0.7 0.8 1.0 0.00";
    sizes[0] = 0.2;
    sizes[1] = 0.4;
    sizes[2] = 1.6;
    times[0] = 0;
    times[1] = 0.5;
    times[2] = 1;
};
datablock ParticleEmitterData(PlayerFoamEmitter)
{
    ejectionPeriodMS = 10;
    periodVarianceMS = 0;
    ejectionVelocity = 3;
    velocityVariance = 1;
    ejectionOffset = 0;
    thetaMin = 85;
    thetaMax = 85;
    phiReferenceVel = 0;
    phiVariance = 360;
    overrideAdvance = 0;
    particles = "PlayerFoamParticle";
};
datablock ParticleData(PlayerFoamDropletsParticle)
{
    dragCoefficient = 1;
    gravityCoefficient = 0.2;
    inheritedVelFactor = 0.2;
    constantAcceleration = -0;
    lifetimeMS = 600;
    lifetimeVarianceMS = 0;
    textureName = "projects/common/characters/splash";
    colors[0] = "0.7 0.8 1.0 1.0";
    colors[1] = "0.7 0.8 1.0 0.5";
    colors[2] = "0.7 0.8 1.0 0.0";
    sizes[0] = 0.8;
    sizes[1] = 0.3;
    sizes[2] = 0;
    times[0] = 0;
    times[1] = 0.5;
    times[2] = 1;
};
datablock ParticleEmitterData(PlayerFoamDropletsEmitter)
{
    ejectionPeriodMS = 7;
    periodVarianceMS = 0;
    ejectionVelocity = 2;
    velocityVariance = 1;
    ejectionOffset = 0;
    thetaMin = 60;
    thetaMax = 80;
    phiReferenceVel = 0;
    phiVariance = 360;
    overrideAdvance = 0;
    orientParticles = 1;
    particles = "PlayerFoamDropletsParticle";
};
datablock ParticleData(PlayerSplashParticle)
{
    dragCoefficient = 1;
    gravityCoefficient = 0.2;
    inheritedVelFactor = 0.2;
    constantAcceleration = -0;
    lifetimeMS = 600;
    lifetimeVarianceMS = 0;
    colors[0] = "0.7 0.8 1.0 1.0";
    colors[1] = "0.7 0.8 1.0 0.5";
    colors[2] = "0.7 0.8 1.0 0.0";
    sizes[0] = 0.5;
    sizes[1] = 0.5;
    sizes[2] = 0.5;
    times[0] = 0;
    times[1] = 0.5;
    times[2] = 1;
};
datablock ParticleEmitterData(PlayerSplashEmitter)
{
    ejectionPeriodMS = 1;
    periodVarianceMS = 0;
    ejectionVelocity = 3;
    velocityVariance = 1;
    ejectionOffset = 0;
    thetaMin = 60;
    thetaMax = 80;
    phiReferenceVel = 0;
    phiVariance = 360;
    overrideAdvance = 0;
    orientParticles = 1;
    lifetimeMS = 100;
    particles = "PlayerSplashParticle";
};
datablock SplashData(PlayerSplash)
{
    numSegments = 15;
    ejectionFreq = 15;
    ejectionAngle = 40;
    ringLifetime = 0.5;
    lifetimeMS = 300;
    velocity = 4;
    startRadius = 0;
    acceleration = -3;
    texWrap = 5;
    texture = "projects/common/characters/splash";
    emitter[0] = PlayerSplashEmitter;
    emitter[1] = PlayerSplashMistEmitter;
    colors[0] = "0.7 0.8 1.0 0.0";
    colors[1] = "0.7 0.8 1.0 0.3";
    colors[2] = "0.7 0.8 1.0 0.7";
    colors[3] = "0.7 0.8 1.0 0.0";
    times[0] = 0;
    times[1] = 0.4;
    times[2] = 0.8;
    times[3] = 1;
};
datablock ParticleData(LightPuff)
{
    dragCoefficient = 2;
    gravityCoefficient = -0.01;
    inheritedVelFactor = 0.6;
    constantAcceleration = 0;
    lifetimeMS = 800;
    lifetimeVarianceMS = 100;
    useInvAlpha = 1;
    spinRandomMin = -35;
    spinRandomMax = 35;
    colors[0] = "1.0 1.0 1.0 1.0";
    colors[1] = "1.0 1.0 1.0 0.0";
    sizes[0] = 0.1;
    sizes[1] = 0.8;
    times[0] = 0.3;
    times[1] = 1;
};
datablock ParticleEmitterData(LightPuffEmitter)
{
    ejectionPeriodMS = 35;
    periodVarianceMS = 10;
    ejectionVelocity = 0.2;
    velocityVariance = 0.1;
    ejectionOffset = 0;
    thetaMin = 20;
    thetaMax = 60;
    phiReferenceVel = 0;
    phiVariance = 360;
    overrideAdvance = 0;
    useEmitterColors = 1;
    particles = "LightPuff";
};
datablock ParticleData(LiftoffDust)
{
    dragCoefficient = 1;
    gravityCoefficient = -0.01;
    inheritedVelFactor = 0;
    constantAcceleration = 0;
    lifetimeMS = 1000;
    lifetimeVarianceMS = 100;
    useInvAlpha = 1;
    spinRandomMin = -90;
    spinRandomMax = 500;
    colors[0] = "1.0 1.0 1.0 1.0";
    sizes[0] = 1;
    times[0] = 1;
};
datablock ParticleEmitterData(LiftoffDustEmitter)
{
    ejectionPeriodMS = 5;
    periodVarianceMS = 0;
    ejectionVelocity = 2;
    velocityVariance = 0;
    ejectionOffset = 0;
    thetaMin = 90;
    thetaMax = 90;
    phiReferenceVel = 0;
    phiVariance = 360;
    overrideAdvance = 0;
    useEmitterColors = 1;
    particles = "LiftoffDust";
};
datablock DecalData(PlayerFootprint)
{
    sizeX = 0.25;
    sizeY = 0.25;
    textureName = "projects/common/characters/footprint";
};
datablock DebrisData(PlayerDebris)
{
    explodeOnMaxBounce = 0;
    elasticity = 0.15;
    friction = 0.5;
    lifetime = 4;
    lifetimeVariance = 0;
    minSpinSpeed = 40;
    maxSpinSpeed = 600;
    numBounces = 5;
    bounceVariance = 0;
    staticOnMaxBounce = 1;
    gravModifier = 1;
    useRadiusMass = 1;
    baseRadius = 1;
    velocity = 20;
    velocityVariance = 12;
};
datablock PlayerData(PlayerBody)
{
    renderFirstPerson = 0;
    emap = 1;
    className = armor;
    shapeFile = "projects/common/characters/f_player/f_player.dts";
    cameraMaxDist = 3;
    computeCRC = 1;
    meshCategory01 = "feet";
    meshCategory02 = "legs";
    meshCategory03 = "torso";
    meshCategory04 = "face";
    meshCategory05 = "hair";
    meshCategory06 = "glasses";
    meshCategory07 = "";
    meshCategory08 = "";
    meshCategory09 = "";
    meshCategory10 = "";
    meshCategory11 = "";
    meshCategory12 = "";
    toonShaded = 1;
    toonUseAmbient = 1;
    toonStyle = 3;
    toonLineWidth = 3;
    toonLightMapTexture = "projects/common/cubemaps/celcube2";
    toonColorOutl = "0.0 0.0 0.0";
    toonColorOutl1 = "0.3 0.0 0.1";
    toonColorOutl2 = "0.2 0.0 0.1";
    toonColorOutl3 = "1.0 0.5 0.1";
    toonColorFill = "0.0 0.0 0.0";
    toonColorFillS = "1.0 0.0 0.1";
    toonColorFillH = "0.4 0.8 0.9";
    toonColorFillHS = "1.0 0.5 0.1";
    toonColorFillAFK = "0.2 0.1 0.1";
    toonMaxThicknessDist = 2;
    canObserve = 1;
    cmdCategory = "Clients";
    cameraDefaultFov = 90;
    cameraMinFov = 56;
    cameraMaxFov = 120;
    cameraLookatOffset = "0.2 0 -0.4";
    cameraMaxDist = 2;
    debrisShapeName = "";
    Debris = PlayerDebris;
    aiAvoidThis = 1;
    minLookAngle = -1;
    maxLookAngle = 1.6;
    maxFreelookAngle = 3;
    maxTimeScale = 1.2;
    maxStepHeight = 0.5;
    mass = 90;
    drag = 0.3;
    maxDrag = 0.4;
    density = 10;
    maxDamage = 100;
    maxEnergy = 60;
    repairRate = 0.33;
    energyPerDamagePoint = 75;
    rechargeRate = 0.256;
    runForce = 48 * 90;
    runEnergyDrain = 0;
    minRunEnergy = 0;
    minForwardSpeed = 3;
    maxForwardSpeed = 6;
    minToMaxSpeedSecs = 8;
    maxBackwardSpeed = 3;
    maxSideSpeed = 2;
    maxUnderwaterForwardSpeed = 8.4;
    maxUnderwaterBackwardSpeed = 7.8;
    maxUnderwaterSideSpeed = 7.8;
    jumpForce = 8.3 * 90;
    jumpEnergyDrain = 0;
    minJumpEnergy = 0;
    jumpDelay = 12;
    recoverDelay = 9;
    recoverRunForceScale = 1.2;
    minImpactSpeed = 45;
    speedDamageScale = 0.4;
    boundingBox = "0.65 0.65 1.7";
    vsPlayerRadius = 0.2;
    vsPlayerPushing = 0.2;
    vsPlayerMushing = 0.85;
    pickupRadius = 0.5;
    boxNormalHeadPercentage = 0.83;
    boxNormalTorsoPercentage = 0.49;
    boxHeadLeftPercentage = 0;
    boxHeadRightPercentage = 1;
    boxHeadBackPercentage = 0;
    boxHeadFrontPercentage = 1;
    DecalData = PlayerFootprint;
    decalOffset = 0.25;
    footPuffEmitter = LightPuffEmitter;
    footPuffNumParts = 10;
    footPuffRadius = 0.25;
    dustEmitter = LiftoffDustEmitter;
    Splash = PlayerSplash;
    splashVelocity = 4;
    splashAngle = 67;
    splashFreqMod = 300;
    splashVelEpsilon = 0.6;
    bubbleEmitTime = 0.4;
    splashEmitter[0] = PlayerFoamDropletsEmitter;
    splashEmitter[1] = PlayerFoamEmitter;
    splashEmitter[2] = PlayerBubbleEmitter;
    mediumSplashSoundVelocity = 10;
    hardSplashSoundVelocity = 20;
    exitSplashSoundVelocity = 5;
    runSurfaceAngle = 70;
    jumpSurfaceAngle = 80;
    minJumpSpeed = 20;
    maxJumpSpeed = 30;
    horizMaxSpeed = 68;
    horizResistSpeed = 33;
    horizResistFactor = 0.35;
    upMaxSpeed = 80;
    upResistSpeed = 25;
    upResistFactor = 0.3;
    footstepSplashHeight = 0.35;
    FootSound0 = StepSoundWood;
    FootSound1 = StepSoundDefault;
    FootSound2 = StepSoundDefault;
    FootSound3 = StepSoundDefault;
    FootShallowSound = StepSoundDefault;
    FootWadingSound = StepSoundDefault;
    FootUnderwaterSound = StepSoundDefault;
    groundImpactMinSpeed = 10;
    groundImpactShakeFreq = "4.0 4.0 4.0";
    groundImpactShakeAmp = "1.0 1.0 1.0";
    groundImpactShakeDuration = 0.8;
    groundImpactShakeFalloff = 10;
    observeParameters = "0.5 4.5 4.5";
    maxInv[BulletAmmo] = 20;
    maxInv[HealthKit] = 1;
    maxInv[RifleAmmo] = 100;
    maxInv[CrossbowAmmo] = 50;
    maxInv[Crossbow] = 1;
    maxInv[Rifle] = 1;
    gender = "";
};
datablock PlayerData(PlayerF : PlayerBody)
{
    possibleGenders = "fmn";
    possibleGenres = "hipn";
    gender = "f";
    shapeFile = "projects/common/characters/f_player/f_player.dts";
    wardrobeInitFunc = "wardrobeInitF()";
};
datablock PlayerData(PlayerM : PlayerBody)
{
    possibleGenders = "mfn";
    possibleGenres = "hipn";
    gender = "m";
    shapeFile = "projects/common/characters/m_player/m_player.dts";
    wardrobeInitFunc = "wardrobeInitM()";
};
function armor::onAdd(%this, %obj)
{
    gSetField(%this, mountVehicle, 1);
    %obj.setRechargeRate(%this.rechargeRate);
    %obj.setRepairRate(0);
    return ;
}
function armor::onRemove(%this, %obj)
{
    if (%obj.client.Player == %obj)
    {
        %obj.client.Player = 0;
    }
    if (%obj.isDancing)
    {
        %obj.stopDance();
    }
    if (%obj.isSitting)
    {
        freeSeat(%obj.mySeat);
    }
    return ;
}
function armor::onNewDataBlock(%this, %obj)
{
    return ;
}
function armor::onMount(%this, %obj, %vehicle, %node)
{
    if (%node == 0)
    {
        %obj.setTransform("0 0 0 0 0 1 0");
        %obj.setActionThread(%vehicle.getDataBlock().mountPose[%node], 1, 1);
        %obj.lastWeapon = %obj.getMountedImage($WeaponSlot);
        %obj.unmountImage($WeaponSlot);
        %obj.setControlObject(%vehicle);
        %obj.client.setObjectActiveImage(%vehicle, 2);
    }
    return ;
}
function armor::onUnmount(%this, %obj, %vehicle, %node)
{
    if (%node == 0)
    {
        %obj.mountImage(%obj.lastWeapon, $WeaponSlot);
    }
    return ;
}
function armor::doDismount(%this, %obj, %forced)
{
    if (!%obj.isMounted())
    {
        return ;
    }
    %pos = getWords(%obj.getTransform(), 0, 2);
    %oldPos = %pos;
    %vec[0] = " 0  0  1";
    %vec[1] = " 0  0  1";
    %vec[2] = " 0  0 -1";
    %vec[3] = " 1  0  0";
    %vec[4] = "-1  0  0";
    %impulseVec = "0 0 0";
    %vec[0] = MatrixMulVector(%obj.getTransform(), %vec[0]) ;
    %pos = "0 0 0";
    %numAttempts = 5;
    %success = -1;
    %i = 0;
    while (%i < %numAttempts)
    {
        %pos = VectorAdd(%oldPos, VectorScale(%vec[%i], 3));
        if (%obj.checkDismountPoint(%oldPos, %pos))
        {
            %success = %i;
            %impulseVec = %vec[%i];
            break;
        }
        %i = %i + 1;
    }
    if (%forced && (%success == -1))
    {
        %pos = %oldPos;
    }
    gSetField(%this, mountVehicle, 0);
    %obj.schedule(4000, "mountVehicles", 1);
    %obj.setTransform(%pos);
    %obj.applyImpulse(%pos, VectorScale(%impulseVec, %obj.getDataBlock().mass));
    %obj.setPilot(0);
    %obj.vehicleTurret = "";
    return ;
}
function armor::onCollision(%this, %obj, %col)
{
    if (%obj.getState() $= "Dead")
    {
        return ;
    }
    if (%col.getClassName() $= "Item")
    {
        %obj.pickup(%col);
    }
    %this = %col.getDataBlock();
    if ((((%this.className $= WheeledVehicleData) && %obj.mountVehicle) && (%obj.getState() $= "Move")) && %col.mountable)
    {
        %node = 0;
        %col.mountObject(%obj, %node);
        %obj.mVehicle = %col;
    }
    return ;
}
function armor::onImpact(%this, %obj, %unused, %vec, %vecLen)
{
    %obj.Damage(0, VectorAdd(%obj.getPosition(), %vec), %vecLen * %this.speedDamageScale, "Impact");
    return ;
}
function armor::Damage(%this, %obj, %sourceObject, %unused, %damage, %damageType)
{
    if (%obj.getState() $= "Dead")
    {
        return ;
    }
    %obj.applyDamage(%damage);
    %location = "Body";
    %client = %obj.client;
    %sourceClient = %sourceObject ? %sourceObject : 0;
    if (%obj.getState() $= "Dead")
    {
        %client.onDeath(%sourceObject, %sourceClient, %damageType, %location);
    }
    return ;
}
function armor::onDamage(%this, %obj, %delta)
{
    if ((%delta > 0) && !((%obj.getState() $= "Dead")))
    {
        %flash = %obj.getDamageFlash() + ((%delta / %this.maxDamage) * 2);
        if (%flash > 0.75)
        {
            %flash = 0.75;
        }
        %obj.setDamageFlash(%flash);
        if (%delta > 10)
        {
            %obj.playPain();
        }
    }
    return ;
}
function armor::onDisabled(%this, %obj, %unused)
{
    %obj.playDeathCry();
    %obj.playDeathAnimation();
    %obj.setDamageFlash(0.75);
    %obj.setImageTrigger(0, 0);
    %obj.schedule($CorpseTimeoutValue - 1000, "startFade", 1000, 0, 1);
    %obj.schedule($CorpseTimeoutValue, "delete");
    return ;
}
function armor::onLeaveMissionArea(%this, %obj)
{
    if (isObject(%obj.client))
    {
        %obj.client.onLeaveMissionArea();
    }
    return ;
}
function armor::onEnterMissionArea(%this, %obj)
{
    if (isObject(%obj.client))
    {
        %obj.client.onEnterMissionArea();
    }
    return ;
}
function armor::onEnterLiquid(%this, %obj, %unused, %type)
{
    if (%type == 0)
    {
    }
    else
    {
        if (%type == 1)
        {
        }
        else
        {
            if (%type == 2)
            {
            }
            else
            {
                if (%type == 3)
                {
                }
                else
                {
                    if (%type == 4)
                    {
                        %obj.setDamageDt(%this, $DamageLava, "Lava");
                    }
                    else
                    {
                        if (%type == 5)
                        {
                            %obj.setDamageDt(%this, $DamageHotLava, "Lava");
                        }
                        else
                        {
                            if (%type == 6)
                            {
                                %obj.setDamageDt(%this, $DamageCrustyLava, "Lava");
                            }
                            else
                            {
                                if (%type == 7)
                                {
                                }
                            }
                        }
                    }
                }
            }
        }
    }
    return ;
}
function armor::onLeaveLiquid(%this, %obj, %type)
{
    %obj.clearDamageDt();
    return ;
}
function armor::onTrigger(%this, %obj, %unused, %unused)
{
    return ;
}
function armor::animationDone(%this)
{
    return ;
}
function Player::kill(%this, %damageType)
{
    %this.Damage(0, %this.getPosition(), 10000, %damageType);
    return ;
}
function Player::mountVehicles(%this, %bool)
{
    gSetField(%this, mountVehicle, %bool);
    return ;
}
function Player::isPilot(%this)
{
    %vehicle = %this.getObjectMount();
    if (%vehicle)
    {
        if (%vehicle.getMountNodeObject(0) == %this)
        {
            return 1;
        }
    }
    return 0;
}
function Player::playCelAnimation(%this, %anim)
{
    if (!(%this.getState() $= "Dead"))
    {
        %this.setActionThread("emote_" @ %anim);
    }
    return ;
}
function Player::playAnim(%this, %anim)
{
    if (!(%this.getState() $= "Dead"))
    {
        %this.setActionThread(%anim);
    }
    return ;
}
function Player::onNewDataBlock(%this, %obj)
{
    echo("ON NEW DATABLOCK" SPC %this SPC %obj);
    return ;
}
function AddDance(%danceObj, %sequence, %timeTillSwitch, %transitionTime)
{
    if (!%danceObj.count)
    {
        %danceObj.count = 0;
    }
    %danceObj.anim[%danceObj.count] = %sequence;
    %danceObj.time[%danceObj.count] = %timeTillSwitch;
    %danceObj.transition[%danceObj.count] = %transitionTime;
    %danceObj.count = %danceObj.count + 1;
    return ;
}
$DANCE_PULSE_FREQ = 100;
function Player::dancePulse(%player)
{
    %playerVel = %player.getVelocity();
    %vel = VectorLen(%playerVel);
    if (%vel > 0.1)
    {
        %player.stopDance();
        return ;
    }
    %player.danceTimeRemaining = %player.danceTimeRemaining - $DANCE_PULSE_FREQ;
    if (%player.danceTimeRemaining <= 0)
    {
        echo("choosing new dance");
        %dNum = getRandom(0, %player.danceObj.count - 1);
        %player.setActionThread(%player.danceObj.anim[%dNum], 0, 1, %player.danceObj.transition[%dNum] / 1000);
        %player.danceTimeRemaining = %player.danceObj.time[%dNum];
        echo("number " @ %dNum @ " chose anime" @ %player.danceObj.anim[%dNum] @ " for " @ %player.danceTimeRemaining @ " milliseconds");
    }
    %player.danceSchedule = %player.schedule($DANCE_PULSE_FREQ, "dancePulse");
    return ;
}
function Player::startDance(%player)
{
    echo("Player::startDance");
    if (%player.isDancing)
    {
        echo("already dancing");
        %player.stopDance();
    }
    echo("setting up dance moves");
    %player.danceObj = new ScriptObject();
    AddDance(%player.danceObj, "idl3a", 3000, 500);
    AddDance(%player.danceObj, "idl3b", 3000, 500);
    AddDance(%player.danceObj, "idl3c", 3000, 500);
    AddDance(%player.danceObj, "idl3d", 3000, 500);
    echo("starting schedule");
    %player.isDancing = 1;
    %player.danceSchedule = %player.schedule($DANCE_PULSE_FREQ, "dancePulse");
    return ;
}
function Player::stopDance(%player)
{
    echo("Player::stopDance");
    cancel(%player.danceSchedule);
    if (isObject(%player.danceObj))
    {
        %player.danceObj.delete();
    }
    %player.danceObj = 0;
    %player.danceTimeRemaining = 0;
    %player.isDancing = 0;
    return ;
}
