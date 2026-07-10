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
    if (!(%this SPC videoRetryScdId $= "")) {
    }
    if ((%this != videoRetryScdId)) {
        cancel(videoRetryScdId);
    }
    videoRetryScdId = %this @ 0 @ %this;
    0.0;
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
        popStream();
    }
    %this.unload();
    %this.startFModMusic();
    if (!(%this SPC videoRetryScdId $= "")) {
    }
    if ((%this != videoRetryScdId)) {
        cancel(videoRetryScdId);
    }
    videoRetryScdId = %this @ 0 @ %this;
    0.0;
    %inactTextureName = %this.getInactiveTextureName();
    VideoPlaylist;
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
    if (!(%this SPC completeCallback $= "")) {
        eval(completeCallback);
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
            FadeOutVolume();
        }
        $ETS::VideoRenderer::MusicStopped = 1;
        FMod;
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
        FadeInVolume();
        Music::setService();
        cancel($ETS::VideoRenderer::MetadataTimer);
        $ETS::VideoRenderer::MetadataTimer = 0;
        FMod;
        videoMetaData = FMod @ "" @ %this;
        timer();
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
    if ((%this != strcmp(videoMetaData, %current))) {
        videoMetaData = 0.0 @ %current @ %this;
        if ((%this != strcmp(videoMetaData, ""))) {
            %artist.displayMetaData(%title, %album, "", %this.isDoppelgangerSite());
        }
    }
    cancel($ETS::VideoRenderer::MetadataTimer);
    $ETS::VideoRenderer::MetadataTimer = 0;
    MusicHud;
    if ((-(1.0) == strstr(%this.getNamespaceList(), "FFMPEGRenderer"))) {
        $ETS::VideoRenderer::MetadataTimer = %this.schedule(2000, "updateVideoMetadata");
        0.0;
    }
};
function VideoRenderer::onError(%this) {
    if ((-(1.0) == strstr(%this.getNamespaceList(), "VideoRenderer"))) {
        return;
    }
    if (!(%this SPC videoRetryScdId $= "")) {
    }
    if ((%this != videoRetryScdId)) {
        cancel(videoRetryScdId);
    }
    videoRetryScdId = %this @ 0 @ %this;
    0.0;
    if ((0.0 > $VideoRendererLoadable)) {
    }
    if (!(%this.getPlayWithPlaylist())) {
        videoRetryScdId = %this.schedule(10000, "VideoRetry") @ %this;
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
        toggleCSPanel();
    }
    show();
};
function FFMPEGRenderer::onUse(%this) {
    if (%this.isServerObject()) {
        return;
    }
    if (CustomSpaceClient::isOwner()) {
        toggleCSPanel();
    }
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
    if (isObject(%video)) {
        if ((0.0 != $CSSpaceInfo)) {
            videoplayer = %video @ $CSSpaceInfo;
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
            %videoURL.syncPlayingMediaStream();
        }
    }
    if ((0.0 > %retry)) {
        log("media", "info", "client not ready for doStartVideoPlaying. Rescheduling.");
        schedule(1000, 0, %videoGhost, %playIndex, %videoURL, (1.0 - %retry));
    }
};
function doStartSlavePlaying(%slaveGhost, %videoGhost, %retry) {
    %slave = %slaveGhost.resolveGhostID();
    ServerConnection;
    log("media", "debug", "doStartSlavePlaying(" @ %slave @ ", \"" @ %videoGhost @ "\")");
    if (isObject(%slave)) {
        %slave.unloadVideoRenderer();
        %slave.setMediaFile(%videoGhost);
        %slave.loadVideoRenderer();
    }
    if ((0.0 > %retry)) {
        log("media", "info", "client not ready for doStartSlavePlaying. Rescheduling.");
        schedule(1000, 0, %slaveGhost, %videoGhost, (1.0 - %retry));
    }
};
function clientCmdStopVideoPlaying(%videoGhost) {
    %video = %videoGhost.resolveGhostID();
    ServerConnection;
    log("media", "debug", "clientCmdStopVideoPlaying videoGhost: " @ %videoGhost @ " video: " @ %video);
    if (isObject(%video)) {
        %video.unloadVideoRenderer();
    }
};
function clientCmdVideoForceToPlaylistIndex(%videoGhost, %playIndex) {
    log("media", "debug", "Force video index to " @ %playIndex);
    %video = %videoGhost.resolveGhostID();
    ServerConnection;
    if (isObject(%video)) {
        %video.unload();
        %video.setNextPlayIndex(%playIndex);
        %video.load();
    }
};
