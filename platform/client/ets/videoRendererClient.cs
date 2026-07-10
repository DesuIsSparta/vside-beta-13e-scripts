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
        %this.setTextureFile(%loadTextureName);
    }
    if (%this.getPlayWithPlaylist()) {
        renderer = %this @ VideoPlaylist;
    }
    $VideoRendererLoadable = (1.0 + $VideoRendererLoadable);
    if ((-(1.0) == strstr(%this.getNamespaceList(), "VideoRenderer"))) {
        return;
    }
    if (!(%this.videoRetryScdId $= "")) {
    }
    if ((0.0 != %this.videoRetryScdId)) {
        cancel(%this.videoRetryScdId);
    }
    %this.videoRetryScdId = 0;
    %this.load();
};
function VideoRenderer::unloadVideoRenderer(%this) {
    if ((0.0 > $VideoRendererLoadable)) {
        $VideoRendererLoadable = (1.0 - $VideoRendererLoadable);
    }
    if ((-(1.0) == strstr(%this.getNamespaceList(), "VideoRenderer"))) {
        return;
    }
    if (%this.getPlayWithPlaylist()) {
        VideoPlaylist.popStream();
    }
    %this.unload();
    %this.startFModMusic();
    if (!(%this.videoRetryScdId $= "")) {
    }
    if ((0.0 != %this.videoRetryScdId)) {
        cancel(%this.videoRetryScdId);
    }
    %this.videoRetryScdId = 0;
    %inactTextureName = %this.getInactiveTextureName();
    if (!(%inactTextureName $= "")) {
        %this.setTextureFile(%inactTextureName);
    }
};
function VideoRenderer::onLoad(%this) {
    if ((-(1.0) == strstr(%this.getNamespaceList(), "VideoRenderer"))) {
        return;
    }
    if (!(fmodIsPlaying())) {
        %this.startMetadataDisplay(0);
    }
    %this.stopFModMusic();
    %this.play();
};
function VideoRenderer::onComplete(%this) {
    if ((-(1.0) == strstr(%this.getNamespaceList(), "VideoRenderer"))) {
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
    if ((-(1.0) != strstr(%this.getNamespaceList(), "TheoraRenderer"))) {
        return;
    }
    log("media", "info", "Client advanced to next video, last video index = #" @ %index);
    if ((0.0 != $CSSpaceInfo)) {
        %spaceName = CustomSpaceClient::GetSpaceImIn();
    }
    commandToServer('VideoRendererAdvance', %this.getGhostID(), %index, %spaceName);
};
function VideoRenderer::startMetadataDisplay(%this, %fadeoutVolume) {
    if ((-(1.0) == strstr(%this.getNamespaceList(), "VideoRenderer"))) {
        return;
    }
    if ((0.0 == strstr(%this.getNamespaceList(), "SlaveRenderer"))) {
        return;
    }
    if ((0.0 > $VideoRendererLoadable)) {
        if (($FMod::MetadataTimer != 0.0)) {
            cancel($FMod::MetadataTimer);
            $FMod::MetadataTimer = 0;
        }
        Music::setService(%this);
        if (%fadeoutVolume) {
            FMod.FadeOutVolume();
        }
        $ETS::VideoRenderer::MusicStopped = 1;
        if (!(%this.getPlayWithPlaylist())) {
            if (($ETS::VideoRenderer::MetadataTimer == 0.0)) {
                $ETS::VideoRenderer::MetadataTimer = %this.schedule(500, "updateVideoMetadata");
            }
        }
    }
};
function VideoRenderer::stopFModMusic(%this) {
    if ((-(1.0) == strstr(%this.getNamespaceList(), "VideoRenderer"))) {
        return;
    }
    if (!(fmodIsPlaying())) {
        return;
    }
    %this.startMetadataDisplay(1);
};
function VideoRenderer::startFModMusic(%this) {
    if ((-(1.0) == strstr(%this.getNamespaceList(), "VideoRenderer"))) {
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
    if ((-(1.0) == strstr(%this.getNamespaceList(), "VideoRenderer"))) {
        return;
    }
    %this.stopFModMusic();
    %artist = %this.getArtist();
    %title = %this.getTitle();
    %album = %this.getAlbum();
    %current = %artist @ " " @ %title @ " " @ %album;
    if ((0.0 != strcmp(%this.videoMetaData, %current))) {
        %this.videoMetaData = %current;
        if ((0.0 != strcmp(%this.videoMetaData, ""))) {
            MusicHud.displayMetaData(%artist, %title, %album, "", %this.isDoppelgangerSite());
        }
    }
    cancel($ETS::VideoRenderer::MetadataTimer);
    $ETS::VideoRenderer::MetadataTimer = 0;
    if ((-(1.0) == strstr(%this.getNamespaceList(), "FFMPEGRenderer"))) {
        $ETS::VideoRenderer::MetadataTimer = %this.schedule(2000, "updateVideoMetadata");
    }
};
function VideoRenderer::onError(%this) {
    if ((-(1.0) == strstr(%this.getNamespaceList(), "VideoRenderer"))) {
        return;
    }
    if (!(%this.videoRetryScdId $= "")) {
    }
    if ((0.0 != %this.videoRetryScdId)) {
        cancel(%this.videoRetryScdId);
    }
    %this.videoRetryScdId = 0;
    if ((0.0 > $VideoRendererLoadable)) {
    }
    if (!(%this.getPlayWithPlaylist())) {
        %this.videoRetryScdId = %this.schedule(10000, "VideoRetry");
    }
    %this.startFModMusic();
};
function VideoRenderer::VideoRetry(%this) {
    if (!(isObject(%this))) {
        return;
    }
    if ((-(1.0) == strstr(%this.getNamespaceList(), "VideoRenderer"))) {
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
    %video = ServerConnection.resolveGhostID(%videoGhost);
    log("media", "debug", "doStartVideoPlaying(" @ %video @ ", " @ %playIndex @ ", \"" @ %videoURL @ "\")");
    if (isObject(%video)) {
        if ((0.0 != $CSSpaceInfo)) {
            $CSSpaceInfo.videoplayer = %video;
        }
        %video.unloadVideoRenderer();
        if (!(%videoURL $= "")) {
            if (!(%videoURL $= "")) {
                %video.setMediaFile(%videoURL);
            }
            %video.setNextPlayIndex(%playIndex);
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
            CSMediaDisplay.syncPlayingMediaStream(%videoURL);
        }
    }
    if ((0.0 > %retry)) {
        log("media", "info", "client not ready for doStartVideoPlaying. Rescheduling.");
        schedule(1000, 0, doStartVideoPlaying, %videoGhost, %playIndex, %videoURL, (1.0 - %retry));
    }
};
function doStartSlavePlaying(%slaveGhost, %videoGhost, %retry) {
    %slave = ServerConnection.resolveGhostID(%slaveGhost);
    log("media", "debug", "doStartSlavePlaying(" @ %slave @ ", \"" @ %videoGhost @ "\")");
    if (isObject(%slave)) {
        %slave.unloadVideoRenderer();
        %slave.setMediaFile(%videoGhost);
        %slave.loadVideoRenderer();
    }
    if ((0.0 > %retry)) {
        log("media", "info", "client not ready for doStartSlavePlaying. Rescheduling.");
        schedule(1000, 0, doStartSlavePlaying, %slaveGhost, %videoGhost, (1.0 - %retry));
    }
};
function clientCmdStopVideoPlaying(%videoGhost) {
    %video = ServerConnection.resolveGhostID(%videoGhost);
    log("media", "debug", "clientCmdStopVideoPlaying videoGhost: " @ %videoGhost @ " video: " @ %video);
    if (isObject(%video)) {
        %video.unloadVideoRenderer();
    }
};
function clientCmdVideoForceToPlaylistIndex(%videoGhost, %playIndex) {
    log("media", "debug", "Force video index to " @ %playIndex);
    %video = ServerConnection.resolveGhostID(%videoGhost);
    if (isObject(%video)) {
        %video.unload();
        %video.setNextPlayIndex(%playIndex);
        %video.load();
    }
};
