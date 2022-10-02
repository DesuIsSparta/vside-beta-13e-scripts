function OpenALInit()
{
    echo("OpenAL Driver Init:");
    $Audio::initFailed = 0;
    echo("   Vendor: " @ alGetString("AL_VENDOR"));
    echo("   Version: " @ alGetString("AL_VERSION"));
    echo("   Renderer: " @ alGetString("AL_RENDERER"));
    %extString = alGetString("AL_EXTENSIONS");
    %extString = strreplace(%extString, "\n", " ");
    echo("   Extensions: " @ %extString);
    alxListenerf(AL_GAIN_LINEAR, $UserPref::Audio::masterVolume);
    %channel = 1;
    while (%channel <= 8)
    {
        alxSetChannelVolume(%channel, $UserPref::Audio::channelVolume[%channel]);
        %channel = %channel + 1;
    }
    echo("");
    return ;
}
