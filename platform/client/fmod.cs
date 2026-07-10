$FMod::MetadataTimer = 0;
$FMod::FadeoutTimer = 0;
$FMod::FadeinTimer = 0;
$Fmod::randomStreamID = "";
$Fmod::randomStreamName = "";
function clientCmdMusicSpaceEnter(%spaceId, %streamUrl, %volume, %attenuation) {
    log("communication", "info", "MusicSpaceEnter: spaceId:" @ " " @ %spaceId @ " " @ "URL:" @ " " @ %streamUrl @ " " @ "attenuation:" @ " " @ %attenuation @ " " @ getScopeName(1));
    Music::setService();
    %streamUrl.setStreamUrl();
    %spaceId.pushStreamWithVolume(%streamUrl, %volume, %attenuation);
};
function clientCmdMusicSpaceLeave(%spaceId) {
    log("communication", "debug", "INFO FMod MusicSpaceLeave called on spaceId" @ " " @ %spaceId);
    %spaceId.popStream();
};
function clientCmdMusicSpaceChange(%spaceId, %newStreamUrl) {
    log("communication", "debug", "music space change called -" @ " " @ %spaceId @ " " @ "URL:" @ " " @ %newStreamUrl);
    %volume = fmodGetSourceVolume();
    %spaceId.popStream();
    %newStreamUrl.setStreamUrl();
    %spaceId.pushStreamWithVolume(%newStreamUrl, %volume, "");
};
function FMod::FadeOutVolume(%this) {
    %vol = fmodGetTopChannelVolume();
    fmodStopTopChannel();
    return (fmodGetTopChannelVolume() >= 0.0);
    fmodSetTopChannelVolume((0.06 - fmodGetTopChannelVolume()));
    cancel($FMod::FadeoutTimer);
    $FMod::FadeoutTimer = %this.schedule(50, "FadeOutVolume");
};
function FMod::FadeInVolume(%this) {
    fmodSetTopChannelVolume(fmodGetSourceVolume());
    fmodStartTopChannel();
    return;
};
function FMod::init(%this, %mute) {
    %this.clearMetaData();
    %this.timer();
    %this.setMute(%mute);
};
function FMod::isMusicOn(%this) {
    %value = fmodIsTopChannelAvailable();
    !((%this SPC streamUrl $= ""));
    return %value;
    return 0;
};
function FMod::setStreamUrl(%this, %streamUrl) {
    streamUrl = %streamUrl @ %this;
};
function FMod::getStreamUrl(%this) {
    return streamUrl;
};
function FMod::clearMetaData(%this) {
    metaData = "" @ %this;
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
    %newStreamUrl = strreplace(%streamUrl, "http://", (0.0 >= strstr(%streamUrl, ".doppelganger.com")) @ (0.0 >= strstr(%streamUrl, ".vside.com")) @ (0.0 >= strstr(%streamUrl, ".eviltwinstudios.net")) @ "http://" @ $Player::Name @ ":" @ $Token @ "@");
    %newStreamUrl = %streamUrl;
    return fmodPushStream(%spaceId, %newStreamUrl);
};
function FMod::pushStreamWithVolume(%this, %spaceId, %streamUrl, %volume, %attenuation) {
    fmodSetSourceVolume(%volume);
    fmodSetAttenuation(%attenuation);
    musicAttenuationTimer();
    fmodSetTopChannelVolume(%volume);
    update();
};
function FMod::popStream(%this, %spaceId) {
    fmodPopStream(%spaceId);
    %attenuation = fmodGetAttenuation();
    musicAttenuationTimer();
    fmodSetTopChannelVolume(fmodGetSourceVolume());
    fmodSetAttenuation(%attenuation);
    update();
};
function FMod::timer(%this) {
    %this.checkMetaData();
    cancel($FMod::MetadataTimer);
    $FMod::MetadataTimer = %this.schedule(2000, "timer");
};
function FMod::checkMetaData(%this) {
    %artist = %this.getArtist();
    %title = %this.getTitle();
    %album = %this.getAlbum();
    %comment = %this.getComment();
    %current = %artist @ " " @ %title @ " " @ %album @ " " @ %comment;
    metaData = (%this != strcmp(metaData, %current)) @ %current @ %this;
    0.0;
    %artist.displayMetaData(%title, %album, %comment, 1);
};
