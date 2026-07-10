$FMod::MetadataTimer = 0;
$FMod::FadeoutTimer = 0;
$FMod::FadeinTimer = 0;
$Fmod::randomStreamID = "";
$Fmod::randomStreamName = "";
function clientCmdMusicSpaceEnter(%spaceId, %streamUrl, %volume, %attenuation) {
    log("communication", "info", "MusicSpaceEnter: spaceId:" @ " " @ %spaceId @ " " @ "URL:" @ " " @ %streamUrl @ " " @ "attenuation:" @ " " @ %attenuation @ " " @ getScopeName(1));
    Music::setService(FMod);
    %streamUrl.setStreamUrl(FMod);
    %attenuation.pushStreamWithVolume(FMod, %spaceId, %streamUrl, %volume);
};
function clientCmdMusicSpaceLeave(%spaceId) {
    log("communication", "debug", "INFO FMod MusicSpaceLeave called on spaceId" @ " " @ %spaceId);
    %spaceId.popStream(FMod);
};
function clientCmdMusicSpaceChange(%spaceId, %newStreamUrl) {
    log("communication", "debug", "music space change called -" @ " " @ %spaceId @ " " @ "URL:" @ " " @ %newStreamUrl);
    %volume = fmodGetSourceVolume();
    %spaceId.popStream(FMod);
    %newStreamUrl.setStreamUrl(FMod);
    "".pushStreamWithVolume(FMod, %spaceId, %newStreamUrl, %volume);
};
function FMod::FadeOutVolume(%this) {
    %vol = fmodGetTopChannelVolume();
    if ((0.0 >= fmodGetTopChannelVolume())) {
        fmodStopTopChannel();
        return;
    }
    fmodSetTopChannelVolume((fmodGetTopChannelVolume() - 0.06));
    cancel($FMod::FadeoutTimer);
    $FMod::FadeoutTimer = "FadeOutVolume".schedule(%this, 50);
};
function FMod::FadeInVolume(%this) {
    fmodSetTopChannelVolume(fmodGetSourceVolume());
    fmodStartTopChannel();
    return;
};
function FMod::init(%this, %mute) {
    %this.clearMetaData();
    %this.timer();
    %mute.setMute(%this);
};
function FMod::isMusicOn(%this) {
    if (!(%this.streamUrl $= "")) {
        %value = fmodIsTopChannelAvailable();
        return %value;
    }
    return 0;
};
function FMod::setStreamUrl(%this, %streamUrl) {
    %this.streamUrl = %streamUrl;
};
function FMod::getStreamUrl(%this) {
    return %this.streamUrl;
};
function FMod::clearMetaData(%this) {
    %this.metaData = "";
};
function FMod::setMute(%this, %bool) {
    fmodSetMute(%bool);
};
function FMod::setMasterVolume(%this, %vol) {
    fmodSetMasterVolume(%vol);
};
function FMod::setVolume(%this, %vol) {
    fmodSetTopChannelVolume(%vol);
};
function FMod::getSourceVolume(%this) {
    return fmodGetSourceVolume();
};
function FMod::getArtist(%this) {
    return fmodGetArtist();
};
function FMod::getTitle(%this) {
    return fmodGetTitle();
};
function FMod::getAlbum(%this) {
    return fmodGetAlbum();
};
function FMod::getComment(%this) {
    return fmodGetComment();
};
function FMod::getAttenuation(%this) {
    return fmodGetAttenuation();
};
function FMod::pushStream(%spaceId, %streamUrl) {
    if ((strstr(%streamUrl, ".doppelganger.com") >= 0.0)) {
    }
    if ((strstr(%streamUrl, ".vside.com") >= 0.0)) {
    }
    if ((strstr(%streamUrl, ".eviltwinstudios.net") >= 0.0)) {
        %newStreamUrl = strreplace(%streamUrl, "http://", "http://" @ $Player::Name @ ":" @ $Token @ "@");
    }
    %newStreamUrl = %streamUrl;
    return fmodPushStream(%spaceId, %newStreamUrl);
};
function FMod::pushStreamWithVolume(%this, %spaceId, %streamUrl, %volume, %attenuation) {
    if (FMod::pushStream(%spaceId, %streamUrl)) {
        fmodSetSourceVolume(%volume);
        fmodSetAttenuation(%attenuation);
        if (!(%attenuation $= "")) {
            musicAttenuationTimer();
        }
        fmodSetTopChannelVolume(%volume);
        MusicHud.update();
    }
};
function FMod::popStream(%this, %spaceId) {
    fmodPopStream(%spaceId);
    %attenuation = fmodGetAttenuation();
    if (!(%attenuation $= "")) {
        musicAttenuationTimer();
    }
    fmodSetTopChannelVolume(fmodGetSourceVolume());
    fmodSetAttenuation(%attenuation);
    MusicHud.update();
};
function FMod::timer(%this) {
    %this.checkMetaData();
    cancel($FMod::MetadataTimer);
    $FMod::MetadataTimer = "timer".schedule(%this, 2000);
};
function FMod::checkMetaData(%this) {
    %artist = %this.getArtist();
    %title = %this.getTitle();
    %album = %this.getAlbum();
    %comment = %this.getComment();
    %current = %artist @ " " @ %title @ " " @ %album @ " " @ %comment;
    if ((strcmp(%this.metaData, %current) != 0.0)) {
        %this.metaData = %current;
        if ((strcmp(%this.metaData, "") != 0.0)) {
            1.displayMetaData(MusicHud, %artist, %title, %album, %comment);
        }
    }
};
