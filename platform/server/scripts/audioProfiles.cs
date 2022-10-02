datablock AudioDescription(AudioDefault3d)
{
    volume = 1;
    isLooping = 0;
    is3D = 1;
    referenceDistance = 20;
    maxDistance = 100;
    type = $SimAudioType;
};
datablock AudioDescription(AudioClose3d)
{
    volume = 1;
    isLooping = 0;
    is3D = 1;
    referenceDistance = 10;
    maxDistance = 60;
    type = $SimAudioType;
};
datablock AudioDescription(AudioClosest3d)
{
    volume = 1;
    isLooping = 0;
    is3D = 1;
    referenceDistance = 5;
    maxDistance = 30;
    type = $SimAudioType;
};
datablock AudioDescription(AudioFootstepDescription : AudioClosest3d)
{
    volume = 0.4;
    referenceDistance = 5;
    maxDistance = 20;
};
datablock AudioDescription(AudioAmbientDescription : AudioClosest3d)
{
    volume = 0.8;
    referenceDistance = 5;
    maxDistance = 10;
};
datablock AudioDescription(AudioDefaultLooping3d)
{
    volume = 1;
    isLooping = 1;
    is3D = 1;
    referenceDistance = 20;
    maxDistance = 100;
    type = $SimAudioType;
};
datablock AudioDescription(AudioCloseLooping3d)
{
    volume = 1;
    isLooping = 1;
    is3D = 1;
    referenceDistance = 10;
    maxDistance = 50;
    type = $SimAudioType;
};
datablock AudioDescription(AudioClosestLooping3d)
{
    volume = 1;
    isLooping = 1;
    is3D = 1;
    referenceDistance = 5;
    maxDistance = 30;
    type = $SimAudioType;
};
datablock AudioDescription(Audio2D)
{
    volume = 1;
    isLooping = 0;
    is3D = 0;
    type = $SimAudioType;
};
datablock AudioDescription(AudioLooping2D)
{
    volume = 1;
    isLooping = 1;
    is3D = 0;
    type = $SimAudioType;
};
datablock AudioProfile(takeme)
{
    fileName = "~/data/sound/takeme.wav";
    description = "AudioDefaultLooping3d";
    preload = 0;
};
datablock AudioProfile(SitLounge)
{
    fileName = "intersection/data/sound/LEATHER_CHAIR_ADJUST_02_1.ogg";
    description = "AudioDefault3d";
    preload = 0;
};
datablock AudioProfile(SitJacuzzi)
{
    fileName = "intersection/data/sound/BODY_JUMP_IN_L2.ogg";
    description = "AudioClose3d";
    preload = 0;
};
datablock AudioProfile(StepSoundDefault)
{
    fileName = "intersection/data/sound/walk_default.ogg";
    description = "AudioFootstepDescription";
    preload = 0;
};
datablock AudioProfile(StepSoundWood)
{
    fileName = "intersection/data/sound/WALK_WOOD_B.ogg";
    description = "AudioFootstepDescription";
    preload = 0;
};
datablock AudioProfile(HotTubAmbientSound)
{
    fileName = "intersection/data/sound/hottub_bubbles.ogg";
    description = "AudioAmbientDescription";
    preload = 0;
};

