$FMod::MetadataTimer = 0;
$FMod::FadeoutTimer = 0;
$FMod::FadeinTimer = 0;
$Fmod::randomStreamID = "";
$Fmod::randomStreamName = "";
function clientCmdMusicSpaceEnter(%spaceId, %streamUrl, %volume, %attenuation)
{
    log("communication", "info", "MusicSpaceEnter: spaceId:" SPC %spaceId SPC "URL:" SPC %streamUrl SPC "attenuation:" SPC %attenuation SPC getScopeName(1));
    Music::setService(FMod);
    FMod.setStreamUrl(%streamUrl);
    FMod.pushStreamWithVolume(%spaceId, %streamUrl, %volume, %attenuation);
    return ;
}
function clientCmdMusicSpaceLeave(%spaceId)
{
    log("communication", "debug", "INFO FMod MusicSpaceLeave called on spaceId" SPC %spaceId);
    FMod.popStream(%spaceId);
    return ;
}
function clientCmdMusicSpaceChange(%spaceId, %newStreamUrl)
{
    log("communication", "debug", "music space change called -" SPC %spaceId SPC "URL:" SPC %newStreamUrl);
    %volume = fmodGetSourceVolume();
    FMod.popStream(%spaceId);
    FMod.setStreamUrl(%newStreamUrl);
    FMod.pushStreamWithVolume(%spaceId, %newStreamUrl, %volume, "");
    return ;
}
function FMod::FadeOutVolume(%this)
{
    %vol = fmodGetTopChannelVolume();
    if (0 >= fmodGetTopChannelVolume())
    {
        fmodStopTopChannel();
        return ;
    }
    else
    {
        fmodSetTopChannelVolume(fmodGetTopChannelVolume() - 0.06);
        cancel($FMod::FadeoutTimer);
        $FMod::FadeoutTimer = %this.schedule(50, "FadeOutVolume");
    }
    return ;
}
function FMod::FadeInVolume(%this)
{
    fmodSetTopChannelVolume(fmodGetSourceVolume());
    fmodStartTopChannel();
    return ;
}
function FMod::init(%this, %mute)
{
    %this.clearMetaData();
    %this.timer();
    %this.setMute(%mute);
    return ;
}
function FMod::isMusicOn(%this)
{
    if (!(%this.streamUrl $= ""))
    {
        %value = fmodIsTopChannelAvailable();
        return %value;
    }
    else
    {
        return 0;
    }
    return ;
}
function FMod::setStreamUrl(%this, %streamUrl)
{
    %this.streamUrl = %streamUrl;
    return ;
}
function FMod::getStreamUrl(%this)
{
    return %this.streamUrl;
}
function FMod::clearMetaData(%this)
{
    %this.metaData = "";
    return ;
}
function FMod::setMute(%this, %bool)
{
    fmodSetMute(%bool);
    return ;
}
function FMod::setMasterVolume(%this, %vol)
{
    fmodSetMasterVolume(%vol);
    return ;
}
function FMod::setVolume(%this, %vol)
{
    fmodSetTopChannelVolume(%vol);
    return ;
}
function FMod::getSourceVolume(%this)
{
    return fmodGetSourceVolume();
}
function FMod::getArtist(%this)
{
    return fmodGetArtist();
}
function FMod::getTitle(%this)
{
    return fmodGetTitle();
}
function FMod::getAlbum(%this)
{
    return fmodGetAlbum();
}
function FMod::getComment(%this)
{
    return fmodGetComment();
}
function FMod::getAttenuation(%this)
{
    return fmodGetAttenuation();
}
function FMod::pushStream(%spaceId, %streamUrl)
{
    if (((strstr(%streamUrl, ".doppelganger.com") >= 0) || (strstr(%streamUrl, ".vside.com") >= 0)) || (strstr(%streamUrl, ".eviltwinstudios.net") >= 0))
    {
        %newStreamUrl = strreplace(%streamUrl, "http://", "http://" @ $Player::Name @ ":" @ $Token @ "@");
    }
    else
    {
        %newStreamUrl = %streamUrl;
    }
    return fmodPushStream(%spaceId, %newStreamUrl);
}
function FMod::pushStreamWithVolume(%this, %spaceId, %streamUrl, %volume, %attenuation)
{
    if (FMod::pushStream(%spaceId, %streamUrl))
    {
        fmodSetSourceVolume(%volume);
        fmodSetAttenuation(%attenuation);
        if (!(%attenuation $= ""))
        {
            musicAttenuationTimer();
        }
        else
        {
            fmodSetTopChannelVolume(%volume);
        }
        MusicHud.update();
    }
    return ;
}
function FMod::popStream(%this, %spaceId)
{
    fmodPopStream(%spaceId);
    %attenuation = fmodGetAttenuation();
    if (!(%attenuation $= ""))
    {
        musicAttenuationTimer();
    }
    else
    {
        fmodSetTopChannelVolume(fmodGetSourceVolume());
    }
    fmodSetAttenuation(%attenuation);
    MusicHud.update();
    return ;
}
function FMod::timer(%this)
{
    %this.checkMetaData();
    cancel($FMod::MetadataTimer);
    $FMod::MetadataTimer = %this.schedule(2000, "timer");
    return ;
}
function FMod::checkMetaData(%this)
{
    %artist = %this.getArtist();
    %title = %this.getTitle();
    %album = %this.getAlbum();
    %comment = %this.getComment();
    %current = %artist SPC %title SPC %album SPC %comment;
    if (strcmp(%this.metaData, %current) != 0)
    {
        %this.metaData = %current;
        if (strcmp(%this.metaData, "") != 0)
        {
            MusicHud.displayMetaData(%artist, %title, %album, %comment, 1);
        }
    }
    return ;
}
