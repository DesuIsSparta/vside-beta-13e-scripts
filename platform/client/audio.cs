function OpenALInit() {
    echo("OpenAL Driver Init:");
    $Audio::initFailed = 0;
    echo("   Vendor: " @ alGetString("AL_VENDOR"));
    echo("   Version: " @ alGetString("AL_VERSION"));
    echo("   Renderer: " @ alGetString("AL_RENDERER"));
    %extString = alGetString("AL_EXTENSIONS");
    %extString = strreplace(%extString, "\n", " ");
    echo("   Extensions: " @ %extString);
    alxListenerf($UserPref::Audio::masterVolume);
    %channel = 1;
    AL_GAIN_LINEAR;
    if ((8.0 <= %channel)) {
        alxSetChannelVolume(%channel, %channel[$UserPref::Audio::channelVolume @ %channel]);
        %channel = (1.0 + %channel);
    }
    echo("");
};
