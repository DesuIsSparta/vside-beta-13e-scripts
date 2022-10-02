datablock ParticleData(JacuzziSteamParticle)
{
    dragCoefficient = 0;
    windCoefficient = 0;
    gravityCoefficient = -0.06;
    inheritedVelFactor = 0;
    constantAcceleration = 0;
    lifetimeMS = 704;
    lifetimeVarianceMS = 703;
    useInvAlpha = 0;
    textureName = "projects/vside/common/characters/splash";
    colors[0] = "1.0 1.0 1.0 0.1";
    colors[1] = "1.0 1.0 1.0 0.3";
    colors[2] = "0.7 0.8 1.0 0.2";
    colors[3] = "0.7 0.8 1.0 0.1";
    sizes[0] = 0.4;
    sizes[1] = 0.4;
    sizes[2] = 0.25;
    sizes[3] = 0.2;
    times[0] = 0;
    times[1] = 0.5;
    times[2] = 0.7;
    times[3] = 1;
};
datablock ParticleEmitterData(JacuzziSteamEmitter)
{
    ejectionPeriodMS = 50;
    periodVarianceMS = 50;
    ejectionVelocity = 0;
    velocityVariance = 0;
    ejectionOffset = 1.1;
    thetaMin = 25.2;
    thetaMax = 90;
    phiReferenceVel = 1;
    phiVariance = 360;
    overrideAdvance = 0;
    particles = "JacuzziSteamParticle";
};
datablock ParticleEmitterNodeData(JacuzziSteamEmitterNode);
datablock ParticleData(BathSteamParticle)
{
    dragCoefficient = 0.14;
    windCoefficient = 0;
    gravityCoefficient = -0.16;
    inheritedVelFactor = 0;
    constantAcceleration = 0;
    lifetimeMS = 1028;
    lifetimeVarianceMS = 0;
    useInvAlpha = 0;
    textureName = "projects/vside/worlds/common/slight";
    colors[0] = "1.0 1.0 1.0 0.1";
    colors[1] = "1.0 1.0 1.0 0.3";
    colors[2] = "0.7 0.8 1.0 0.2";
    colors[3] = "0.7 0.8 1.0 0.1";
    sizes[0] = 0.77;
    sizes[1] = 1.19;
    sizes[2] = 1.25;
    sizes[3] = 0.54;
    times[0] = 0;
    times[1] = 0.5;
    times[2] = 0.7;
    times[3] = 1;
};
datablock ParticleEmitterData(BathSteamEmitter)
{
    ejectionPeriodMS = 50;
    periodVarianceMS = 50;
    ejectionVelocity = 0;
    velocityVariance = 0;
    ejectionOffset = 1.1;
    thetaMin = 90;
    thetaMax = 180;
    phiReferenceVel = 0;
    phiVariance = 360;
    overrideAdvance = 0;
    particles = "BathSteamParticle";
};
datablock ParticleEmitterNodeData(BathSteamEmitterNode);
datablock ParticleData(FallingLeafParticle)
{
    dragCoefficient = 1;
    windCoefficient = 0.6;
    gravityCoefficient = 0.12;
    inheritedVelFactor = 0;
    constantAcceleration = 5;
    lifetimeMS = 2496;
    lifetimeVarianceMS = 0;
    spinSpeed = 0.04;
    spinRandomMin = -3;
    spinRandomMax = 0.5;
    useInvAlpha = 1;
    textureName = "projects/vside/worlds/common/leaf";
    colors[0] = "1.0 1.0 1.0 0.5";
    colors[1] = "1.0 1.0 1.0 1.0";
    colors[2] = "1.0 1.0 1.0 0.0";
    sizes[0] = 0.1;
    sizes[1] = 0.1;
    sizes[2] = 0.1;
    times[0] = 0;
    times[1] = 0.5;
    times[2] = 1;
};
datablock ParticleEmitterData(FallingLeafEmitter)
{
    ejectionPeriodMS = 1000;
    periodVarianceMS = 999;
    ejectionVelocity = 0;
    velocityVariance = 0;
    ejectionOffset = 1.1;
    lifetimeMS = 0;
    lifetimeVarianceMS = 0;
    thetaMin = 57.6;
    thetaMax = 122.4;
    phiReferenceVel = 0;
    phiVariance = 360;
    overrideAdvance = 0;
    particles = "FallingLeafParticle";
};
datablock ParticleEmitterNodeData(FallingLeafEmitterNode);

