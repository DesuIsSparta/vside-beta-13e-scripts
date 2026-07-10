$VideoRendererLoadable = 0;
$ETS::VideoRenderer::MetadataTimer = 0;
$ETS::VideoRenderer::MusicStopped = 0;
$ETS::VideoRenderer::Disable = 0;
function VideoRenderer::loadVideoRenderer(%this) {
    if ($UserPref::ETS::VideoRenderer::Disable) {
        userTips::showOnceThisSession("VideosDisabled");
        return;
    }
    %loadTextureName = %this.getLoadingTextureName();
    if (!(%loadTextureName $= "")) {
        echo("Changing texture to " @ %loadTextureName);
        %loadTextureName.setTextureFile(%this);
    }
    if (%this.getPlayWithPlaylist()) {
        VideoPlaylist.renderer = %this;
    }
    $VideoRendererLoadable = ($VideoRendererLoadable + 1.0);
    if ((strstr(%this.getNamespaceList(), "VideoRenderer") == -(1.0))) {
        return;
    }
    if (!(%this.videoRetryScdId $= "")) {
    }
    if ((%this.videoRetryScdId != 0.0)) {
        cancel(%this.videoRetryScdId);
    }
    %this.videoRetryScdId = 0;
    %this.load();
};
function VideoRenderer::unloadVideoRenderer(%this) {
    if (($VideoRendererLoadable > 0.0)) {
        $VideoRendererLoadable = ($VideoRendererLoadable - 1.0);
    }
    if ((strstr(%this.getNamespaceList(), "VideoRenderer") == -(1.0))) {
        return;
    }
    if (%this.getPlayWithPlaylist()) {
        VideoPlaylist.popStream();
    }
    %this.unload();
    %this.startFModMusic();
    if (!(%this.videoRetryScdId $= "")) {
    }
    if ((%this.videoRetryScdId != 0.0)) {
        cancel(%this.videoRetryScdId);
    }
    %this.videoRetryScdId = 0;
    %inactTextureName = %this.getInactiveTextureName();
    if (!(%inactTextureName $= "")) {
        %inactTextureName.setTextureFile(%this);
    }
};
function VideoRenderer::onLoad(%this) {
    if ((strstr(%this.getNamespaceList(), "VideoRenderer") == -(1.0))) {
        return;
    }
    if (!(fmodIsPlaying())) {
        0.startMetadataDisplay(%this);
    }
    %this.stopFModMusic();
    %this.play();
};
function VideoRenderer::onComplete(%this) {
    if ((strstr(%this.getNamespaceList(), "VideoRenderer") == -(1.0))) {
        return;
    }
    if (!(%this.completeCallback $= "")) {
        eval(%this.completeCallback);
    }
    %this.startFModMusic();
};
function VideoRenderer::onAdvance(%this) {
    %index = %this.getPlayIndex();
    %spaceName = "";
    if ((strstr(%this.getNamespaceList(), "TheoraRenderer") != -(1.0))) {
        return;
    }
    log("media", "info", "Client advanced to next video, last video index = #" @ %index);
    if (($CSSpaceInfo != 0.0)) {
        %spaceName = CustomSpaceClient::GetSpaceImIn();
    }
    commandToServer('VideoRendererAdvance', %this.getGhostID(), %index, %spaceName);
};
function VideoRenderer::startMetadataDisplay(%this, %fadeoutVolume) {
    if ((strstr(%this.getNamespaceList(), "VideoRenderer") == -(1.0))) {
        return;
    }
    if ((strstr(%this.getNamespaceList(), "SlaveRenderer") == 0.0)) {
        return;
    }
    if (($VideoRendererLoadable > 0.0)) {
        if ((0.0 != $FMod::MetadataTimer)) {
            cancel($FMod::MetadataTimer);
            $FMod::MetadataTimer = 0;
        }
        Music::setService(%this);
        if (%fadeoutVolume) {
            FMod.FadeOutVolume();
        }
        $ETS::VideoRenderer::MusicStopped = 1;
        if (!(%this.getPlayWithPlaylist()) && (0.0 == $ETS::VideoRenderer::MetadataTimer)) {
            $ETS::VideoRenderer::MetadataTimer = "updateVideoMetadata".schedule(%this, 500);
        }
    }
};
function VideoRenderer::stopFModMusic(%this) {
    if ((strstr(%this.getNamespaceList(), "VideoRenderer") == -(1.0))) {
        return;
    }
    if (!(fmodIsPlaying())) {
        return;
    }
    1.startMetadataDisplay(%this);
};
function VideoRenderer::startFModMusic(%this) {
    if ((strstr(%this.getNamespaceList(), "VideoRenderer") == -(1.0))) {
        return;
    }
    if (!(fmodIsPlaying())) {
        FMod.FadeInVolume();
        Music::setService(FMod);
        cancel($ETS::VideoRenderer::MetadataTimer);
        $ETS::VideoRenderer::MetadataTimer = 0;
        %this.videoMetaData = "";
        FMod.timer();
    }
};
function VideoRenderer::updateVideoMetadata(%this) {
    if ((strstr(%this.getNamespaceList(), "VideoRenderer") == -(1.0))) {
        return;
    }
    %this.stopFModMusic();
    %artist = %this.getArtist();
    %title = %this.getTitle();
    %album = %this.getAlbum();
    %current = %artist @ " " @ %title @ " " @ %album;
    if ((strcmp(%this.videoMetaData, %current) != 0.0)) {
        %this.videoMetaData = %current;
        if ((strcmp(%this.videoMetaData, "") != 0.0)) {
            %this.isDoppelgangerSite().displayMetaData(MusicHud, %artist, %title, %album, "");
        }
    }
    cancel($ETS::VideoRenderer::MetadataTimer);
    $ETS::VideoRenderer::MetadataTimer = 0;
    if ((strstr(%this.getNamespaceList(), "FFMPEGRenderer") == -(1.0))) {
        $ETS::VideoRenderer::MetadataTimer = "updateVideoMetadata".schedule(%this, 2000);
    }
};
function VideoRenderer::onError(%this) {
    if ((strstr(%this.getNamespaceList(), "VideoRenderer") == -(1.0))) {
        return;
    }
    if (!(%this.videoRetryScdId $= "")) {
    }
    if ((%this.videoRetryScdId != 0.0)) {
        cancel(%this.videoRetryScdId);
    }
    %this.videoRetryScdId = 0;
    if (($VideoRendererLoadable > 0.0)) {
    }
    if (!(%this.getPlayWithPlaylist())) {
        %this.videoRetryScdId = "VideoRetry".schedule(%this, 10000);
    }
    %this.startFModMusic();
};
function VideoRenderer::VideoRetry(%this) {
    if (!(isObject(%this))) {
        return;
    }
    if ((strstr(%this.getNamespaceList(), "VideoRenderer") == -(1.0))) {
        return;
    }
    if (%this.getPlayWithPlaylist()) {
        error("trying to retry video and playwithplaylist = true - function not supported");
    }
    if (%this.getEnabled()) {
        %this.unload();
        %this.load();
    }
};
function clientCmdOnMediaTogglerClick() {
    if (CustomSpaceClient::isOwner()) {
        toggleCSPanel(CSMediaDisplay);
    }
    MusicHud.show();
};
function FFMPEGRenderer::onUse(%this) {
    if (%this.isServerObject()) {
        return;
    }
    if (CustomSpaceClient::isOwner()) {
        toggleCSPanel(CSMediaDisplay);
    }
    MusicHud.show();
};
function clientCmdStartVideoPlaying(%videoGhost, %playIndex, %videoURL) {
    doStartVideoPlaying(%videoGhost, %playIndex, %videoURL, 1);
};
function clientCmdStartSlavePlaying(%slaveGhost, %videoGhost) {
    doStartSlavePlaying(%slaveGhost, %videoGhost, 1);
};
function doStartVideoPlaying(%videoGhost, %playIndex, %videoURL, %retry) {
    %video = %videoGhost.resolveGhostID(ServerConnection);
    log("media", "debug", "doStartVideoPlaying(" @ %video @ ", " @ %playIndex @ ", \"" @ %videoURL @ "\")");
    if (isObject(%video)) {
        if (($CSSpaceInfo != 0.0)) {
            $CSSpaceInfo.videoplayer = %video;
        }
        %video.unloadVideoRenderer();
        if (!(%videoURL $= "")) {
            if (!(%videoURL $= "")) {
                %videoURL.setMediaFile(%video);
            }
            %playIndex.setNextPlayIndex(%video);
            %video.loadVideoRenderer();
            %mediaType = "";
            if (strstr(%videoURL, "v=")) {
                %mediaType = "VIDEO";
            }
            if (strstr(%videoURL, "p=")) {
                %mediaType = "VIDEO_PLAYLIST";
            }
            if (!(%mediaType $= "")) {
                csRecordMediaView(%videoURL, %mediaType);
            }
        }
        if (CustomSpaceClient::isOwner()) {
        }
        if (!(%videoURL $= "")) {
            %videoURL.syncPlayingMediaStream(CSMediaDisplay);
        }
    }
    if ((%retry > 0.0)) {
        log("media", "info", "client not ready for doStartVideoPlaying. Rescheduling.");
        schedule(1000, 0, doStartVideoPlaying, %videoGhost, %playIndex, %videoURL, (%retry - 1.0));
    }
};
function doStartSlavePlaying(%slaveGhost, %videoGhost, %retry) {
    %slave = %slaveGhost.resolveGhostID(ServerConnection);
    log("media", "debug", "doStartSlavePlaying(" @ %slave @ ", \"" @ %videoGhost @ "\")");
    if (isObject(%slave)) {
        %slave.unloadVideoRenderer();
        %videoGhost.setMediaFile(%slave);
        %slave.loadVideoRenderer();
    }
    if ((%retry > 0.0)) {
        log("media", "info", "client not ready for doStartSlavePlaying. Rescheduling.");
        schedule(1000, 0, doStartSlavePlaying, %slaveGhost, %videoGhost, (%retry - 1.0));
    }
};
function clientCmdStopVideoPlaying(%videoGhost) {
    %video = %videoGhost.resolveGhostID(ServerConnection);
    log("media", "debug", "clientCmdStopVideoPlaying videoGhost: " @ %videoGhost @ " video: " @ %video);
    if (isObject(%video)) {
        %video.unloadVideoRenderer();
    }
};
function clientCmdVideoForceToPlaylistIndex(%videoGhost, %playIndex) {
    log("media", "debug", "Force video index to " @ %playIndex);
    %video = %videoGhost.resolveGhostID(ServerConnection);
    if (isObject(%video)) {
        %video.unload();
        %playIndex.setNextPlayIndex(%video);
        %video.load();
    }
};
