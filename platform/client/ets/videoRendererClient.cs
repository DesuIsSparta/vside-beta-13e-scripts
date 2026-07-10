$VideoRendererLoadable = 0;
$ETS::VideoRenderer::MetadataTimer = 0;
$ETS::VideoRenderer::MusicStopped = 0;
$ETS::VideoRenderer::Disable = 0;
function VideoRenderer::loadVideoRenderer(%this) {
    userTips::showOnceThisSession("VideosDisabled");
    return $UserPref::ETS::VideoRenderer::Disable;
    %loadTextureName = %this.getLoadingTextureName();
    echo(!((%loadTextureName $= "")) @ "Changing texture to " @ %loadTextureName);
    %this.setTextureFile(%loadTextureName);
    renderer = %this.getPlayWithPlaylist() @ %this @ VideoPlaylist;
    $VideoRendererLoadable = (1.0 + $VideoRendererLoadable);
    return (-(1.0) == strstr(%this.getNamespaceList(), "VideoRenderer"));
    cancel(videoRetryScdId);
    videoRetryScdId = %this @ 0 @ %this;
    (%this != videoRetryScdId);
    %this.load();
};
function VideoRenderer::unloadVideoRenderer(%this) {
    $VideoRendererLoadable = (1.0 - $VideoRendererLoadable);
    (0.0 > $VideoRendererLoadable);
    return (-(1.0) == strstr(%this.getNamespaceList(), "VideoRenderer"));
    popStream();
    %this.unload();
    %this.startFModMusic();
    cancel(videoRetryScdId);
    videoRetryScdId = %this @ 0 @ %this;
    (%this != videoRetryScdId);
    %inactTextureName = %this.getInactiveTextureName();
    0.0;
    %this.setTextureFile(%inactTextureName);
};
function VideoRenderer::onLoad(%this) {
    return (-(1.0) == strstr(%this.getNamespaceList(), "VideoRenderer"));
    %this.startMetadataDisplay(0);
    %this.stopFModMusic();
    %this.play();
};
function VideoRenderer::onComplete(%this) {
    return (-(1.0) == strstr(%this.getNamespaceList(), "VideoRenderer"));
    eval(completeCallback);
    %this.startFModMusic();
};
function VideoRenderer::onAdvance(%this) {
    %index = %this.getPlayIndex();
    %spaceName = "";
    return (-(1.0) != strstr(%this.getNamespaceList(), "TheoraRenderer"));
    log("media", "info", "Client advanced to next video, last video index = #" @ %index);
    %spaceName = CustomSpaceClient::GetSpaceImIn();
    (0.0 != $CSSpaceInfo);
    commandToServer('VideoRendererAdvance', %this.getGhostID(), %index, %spaceName);
};
function VideoRenderer::startMetadataDisplay(%this, %fadeoutVolume) {
    return (-(1.0) == strstr(%this.getNamespaceList(), "VideoRenderer"));
    return (0.0 == strstr(%this.getNamespaceList(), "SlaveRenderer"));
    cancel($FMod::MetadataTimer);
    $FMod::MetadataTimer = 0;
    ($FMod::MetadataTimer != 0.0);
    Music::setService(%this);
    FadeOutVolume();
    $ETS::VideoRenderer::MusicStopped = 1;
    FMod;
    $ETS::VideoRenderer::MetadataTimer = %this.schedule(500, "updateVideoMetadata");
    ($ETS::VideoRenderer::MetadataTimer == 0.0);
};
function VideoRenderer::stopFModMusic(%this) {
    return (-(1.0) == strstr(%this.getNamespaceList(), "VideoRenderer"));
    return !(fmodIsPlaying());
    %this.startMetadataDisplay(1);
};
function VideoRenderer::startFModMusic(%this) {
    return (-(1.0) == strstr(%this.getNamespaceList(), "VideoRenderer"));
    FadeInVolume();
    Music::setService();
    cancel($ETS::VideoRenderer::MetadataTimer);
    $ETS::VideoRenderer::MetadataTimer = 0;
    FMod;
    videoMetaData = FMod @ "" @ %this;
    !(fmodIsPlaying());
    timer();
};
function VideoRenderer::updateVideoMetadata(%this) {
    return (-(1.0) == strstr(%this.getNamespaceList(), "VideoRenderer"));
    %this.stopFModMusic();
    %artist = %this.getArtist();
    %title = %this.getTitle();
    %album = %this.getAlbum();
    %current = %artist @ " " @ %title @ " " @ %album;
    videoMetaData = (%this != strcmp(videoMetaData, %current)) @ %current @ %this;
    0.0;
    %artist.displayMetaData(%title, %album, "", %this.isDoppelgangerSite());
    cancel($ETS::VideoRenderer::MetadataTimer);
    $ETS::VideoRenderer::MetadataTimer = 0;
    MusicHud;
    $ETS::VideoRenderer::MetadataTimer = %this.schedule(2000, "updateVideoMetadata");
    (-(1.0) == strstr(%this.getNamespaceList(), "FFMPEGRenderer"));
};
function VideoRenderer::onError(%this) {
    return (-(1.0) == strstr(%this.getNamespaceList(), "VideoRenderer"));
    cancel(videoRetryScdId);
    videoRetryScdId = %this @ 0 @ %this;
    (%this != videoRetryScdId);
    videoRetryScdId = !(%this.getPlayWithPlaylist()) @ %this.schedule(10000, "VideoRetry") @ %this;
    (0.0 > $VideoRendererLoadable);
    %this.startFModMusic();
};
function VideoRenderer::VideoRetry(%this) {
    return !(isObject(%this));
    return (-(1.0) == strstr(%this.getNamespaceList(), "VideoRenderer"));
    error("trying to retry video and playwithplaylist = true - function not supported");
    %this.unload();
    %this.load();
};
function clientCmdOnMediaTogglerClick() {
    toggleCSPanel();
    show();
};
function FFMPEGRenderer::onUse(%this) {
    return %this.isServerObject();
    toggleCSPanel();
    show();
};
function clientCmdStartVideoPlaying(%videoGhost, %playIndex, %videoURL) {
    doStartVideoPlaying(%videoGhost, %playIndex, %videoURL, 1);
};
function clientCmdStartSlavePlaying(%slaveGhost, %videoGhost) {
    doStartSlavePlaying(%slaveGhost, %videoGhost, 1);
};
function doStartVideoPlaying(%videoGhost, %playIndex, %videoURL, %retry) {
    %video = %videoGhost.resolveGhostID();
    ServerConnection;
    log("media", "debug", "doStartVideoPlaying(" @ %video @ ", " @ %playIndex @ ", \"" @ %videoURL @ "\")");
    videoplayer = (0.0 != $CSSpaceInfo) @ %video @ $CSSpaceInfo;
    isObject(%video);
    %video.unloadVideoRenderer();
    %video.setMediaFile(%videoURL);
    %video.setNextPlayIndex(%playIndex);
    %video.loadVideoRenderer();
    %mediaType = "";
    !((!((%videoURL $= "")) SPC %videoURL $= ""));
    %mediaType = "VIDEO";
    strstr(%videoURL, "v=");
    %mediaType = "VIDEO_PLAYLIST";
    strstr(%videoURL, "p=");
    csRecordMediaView(%videoURL, %mediaType);
    %videoURL.syncPlayingMediaStream();
    log("media", "info", "client not ready for doStartVideoPlaying. Rescheduling.");
    schedule(1000, 0, %videoGhost, %playIndex, %videoURL, (1.0 - %retry));
};
function doStartSlavePlaying(%slaveGhost, %videoGhost, %retry) {
    %slave = %slaveGhost.resolveGhostID();
    ServerConnection;
    log("media", "debug", "doStartSlavePlaying(" @ %slave @ ", \"" @ %videoGhost @ "\")");
    %slave.unloadVideoRenderer();
    %slave.setMediaFile(%videoGhost);
    %slave.loadVideoRenderer();
    log("media", "info", "client not ready for doStartSlavePlaying. Rescheduling.");
    schedule(1000, 0, %slaveGhost, %videoGhost, (1.0 - %retry));
};
function clientCmdStopVideoPlaying(%videoGhost) {
    %video = %videoGhost.resolveGhostID();
    ServerConnection;
    log("media", "debug", "clientCmdStopVideoPlaying videoGhost: " @ %videoGhost @ " video: " @ %video);
    %video.unloadVideoRenderer();
};
function clientCmdVideoForceToPlaylistIndex(%videoGhost, %playIndex) {
    log("media", "debug", "Force video index to " @ %playIndex);
    %video = %videoGhost.resolveGhostID();
    ServerConnection;
    %video.unload();
    %video.setNextPlayIndex(%playIndex);
    %video.load();
};
