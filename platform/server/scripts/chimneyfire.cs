datablock ParticleData(ChimneySmoke)
{
    textureName = "~/data/shapes/particles/smoke";
    dragCoefficient = 0;
    gravityCoefficient = -0.2;
    inheritedVelFactor = 0;
    lifetimeMS = 3000;
    lifetimeVarianceMS = 250;
    useInvAlpha = 0;
    spinRandomMin = -30;
    spinRandomMax = 30;
    colors[0] = "0.6 0.6 0.6 0.1";
    colors[1] = "0.6 0.6 0.6 0.1";
    colors[2] = "0.6 0.6 0.6 0.0";
    sizes[0] = 0.5;
    sizes[1] = 0.75;
    sizes[2] = 1.5;
    times[0] = 0;
    times[1] = 0.5;
    times[2] = 1;
};
datablock ParticleEmitterData(ChimneySmokeEmitter)
{
    ejectionPeriodMS = 20;
    periodVarianceMS = 5;
    ejectionVelocity = 0.25;
    velocityVariance = 0.1;
    thetaMin = 0;
    thetaMax = 90;
    particles = ChimneySmoke;
};
datablock ParticleEmitterNodeData(ChimneySmokeEmitterNode);
datablock ParticleData(ChimneyFire1)
{
    textureName = "~/data/shapes/particles/smoke";
    dragCoefficient = 0;
    gravityCoefficient = -0.3;
    inheritedVelFactor = 0;
    lifetimeMS = 500;
    lifetimeVarianceMS = 250;
    useInvAlpha = 0;
    spinRandomMin = -30;
    spinRandomMax = 30;
    colors[0] = "0.8 0.6 0.0 0.1";
    colors[1] = "0.8 0.6 0.0 0.1";
    colors[2] = "0.0 0.0 0.0 0.0";
    sizes[0] = 1;
    sizes[1] = 1;
    sizes[2] = 5;
    times[0] = 0;
    times[1] = 0.5;
    times[2] = 1;
};
datablock ParticleData(ChimneyFire2)
{
    textureName = "~/data/shapes/particles/smoke";
    dragCoefficient = 0;
    gravityCoefficient = -0.5;
    inheritedVelFactor = 0;
    lifetimeMS = 800;
    lifetimeVarianceMS = 150;
    useInvAlpha = 0;
    spinRandomMin = -30;
    spinRandomMax = 30;
    colors[0] = "0.6 0.6 0.0 0.1";
    colors[1] = "0.6 0.6 0.0 0.1";
    colors[2] = "0.0 0.0 0.0 0.0";
    sizes[0] = 0.5;
    sizes[1] = 0.5;
    sizes[2] = 0.5;
    times[0] = 0;
    times[1] = 0.5;
    times[2] = 1;
};
datablock ParticleEmitterData(ChimneyFireEmitter)
{
    ejectionPeriodMS = 15;
    periodVarianceMS = 5;
    ejectionVelocity = 0.25;
    velocityVariance = 0.1;
    thetaMin = 0;
    thetaMax = 90;
    particles = "ChimneyFire1" TAB "ChimneyFire2";
};
datablock ParticleEmitterNodeData(ChimneyFireEmitterNode);

