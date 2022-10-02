$VideoRendererLoadable = 0;
$ETS::VideoRenderer::MetadataTimer = 0;
$ETS::VideoRenderer::MusicStopped = 0;
$ETS::VideoRenderer::Disable = 0;
function VideoRenderer::loadVideoRenderer(%this)
{
    if ($UserPref::ETS::VideoRenderer::Disable)
    {
        userTips::showOnceThisSession("VideosDisabled");
        return ;
    }
    %loadTextureName = %this.getLoadingTextureName();
    if (!(%loadTextureName $= ""))
    {
        echo("Changing texture to " @ %loadTextureName);
        %this.setTextureFile(%loadTextureName);
    }
    if (%this.getPlayWithPlaylist())
    {
        VideoPlaylist.renderer = %this;
    }
    $VideoRendererLoadable = $VideoRendererLoadable + 1;
    if (strstr(%this.getNamespaceList(), "VideoRenderer") == -1)
    {
        return ;
    }
    if (!((%this.videoRetryScdId $= "")) && (%this.videoRetryScdId != 0))
    {
        cancel(%this.videoRetryScdId);
    }
    %this.videoRetryScdId = 0;
    %this.load();
    return ;
}
function VideoRenderer::unloadVideoRenderer(%this)
{
    if ($VideoRendererLoadable > 0)
    {
        $VideoRendererLoadable = $VideoRendererLoadable - 1;
    }
    if (strstr(%this.getNamespaceList(), "VideoRenderer") == -1)
    {
        return ;
    }
    if (%this.getPlayWithPlaylist())
    {
        VideoPlaylist.popStream();
    }
    else
    {
        %this.unload();
    }
    %this.startFModMusic();
    if (!((%this.videoRetryScdId $= "")) && (%this.videoRetryScdId != 0))
    {
        cancel(%this.videoRetryScdId);
    }
    %this.videoRetryScdId = 0;
    %inactTextureName = %this.getInactiveTextureName();
    if (!(%inactTextureName $= ""))
    {
        %this.setTextureFile(%inactTextureName);
    }
    return ;
}
function VideoRenderer::onLoad(%this)
{
    if (strstr(%this.getNamespaceList(), "VideoRenderer") == -1)
    {
        return ;
    }
    if (!fmodIsPlaying())
    {
        %this.startMetadataDisplay(0);
    }
    else
    {
        %this.stopFModMusic();
    }
    %this.play();
    return ;
}
function VideoRenderer::onComplete(%this)
{
    if (strstr(%this.getNamespaceList(), "VideoRenderer") == -1)
    {
        return ;
    }
    if (!(%this.completeCallback $= ""))
    {
        eval(%this.completeCallback);
    }
    %this.startFModMusic();
    return ;
}
function VideoRenderer::onAdvance(%this)
{
    %index = %this.getPlayIndex();
    %spaceName = "";
    if (strstr(%this.getNamespaceList(), "TheoraRenderer") != -1)
    {
        return ;
    }
    log("media", "info", "Client advanced to next video, last video index = #" @ %index);
    if ($CSSpaceInfo != 0)
    {
        %spaceName = CustomSpaceClient::GetSpaceImIn();
    }
    commandToServer('VideoRendererAdvance', %this.getGhostID(), %index, %spaceName);
    return ;
}
function VideoRenderer::startMetadataDisplay(%this, %fadeoutVolume)
{
    if (strstr(%this.getNamespaceList(), "VideoRenderer") == -1)
    {
        return ;
    }
    if (strstr(%this.getNamespaceList(), "SlaveRenderer") == 0)
    {
        return ;
    }
    if ($VideoRendererLoadable > 0)
    {
        if (0 != $FMod::MetadataTimer)
        {
            cancel($FMod::MetadataTimer);
            $FMod::MetadataTimer = 0;
        }
        Music::setService(%this);
        if (%fadeoutVolume)
        {
            FMod.FadeOutVolume();
        }
        $ETS::VideoRenderer::MusicStopped = 1;
        if (!%this.getPlayWithPlaylist())
        {
            if (0 == $ETS::VideoRenderer::MetadataTimer)
            {
                $ETS::VideoRenderer::MetadataTimer = %this.schedule(500, "updateVideoMetadata");
            }
        }
    }
    return ;
}
function VideoRenderer::stopFModMusic(%this)
{
    if (strstr(%this.getNamespaceList(), "VideoRenderer") == -1)
    {
        return ;
    }
    if (!fmodIsPlaying())
    {
        return ;
    }
    %this.startMetadataDisplay(1);
    return ;
}
function VideoRenderer::startFModMusic(%this)
{
    if (strstr(%this.getNamespaceList(), "VideoRenderer") == -1)
    {
        return ;
    }
    if (!fmodIsPlaying())
    {
        FMod.FadeInVolume();
        Music::setService(FMod);
        cancel($ETS::VideoRenderer::MetadataTimer);
        $ETS::VideoRenderer::MetadataTimer = 0;
        %this.videoMetaData = "";
        FMod.timer();
    }
    return ;
}
function VideoRenderer::updateVideoMetadata(%this)
{
    if (strstr(%this.getNamespaceList(), "VideoRenderer") == -1)
    {
        return ;
    }
    %this.stopFModMusic();
    %artist = %this.getArtist();
    %title = %this.getTitle();
    %album = %this.getAlbum();
    %current = %artist SPC %title SPC %album;
    if (strcmp(%this.videoMetaData, %current) != 0)
    {
        %this.videoMetaData = %current;
        if (strcmp(%this.videoMetaData, "") != 0)
        {
            MusicHud.displayMetaData(%artist, %title, %album, "", %this.isDoppelgangerSite());
        }
    }
    cancel($ETS::VideoRenderer::MetadataTimer);
    $ETS::VideoRenderer::MetadataTimer = 0;
    if (strstr(%this.getNamespaceList(), "FFMPEGRenderer") == -1)
    {
        $ETS::VideoRenderer::MetadataTimer = %this.schedule(2000, "updateVideoMetadata");
    }
    return ;
}
function VideoRenderer::onError(%this)
{
    if (strstr(%this.getNamespaceList(), "VideoRenderer") == -1)
    {
        return ;
    }
    if (!((%this.videoRetryScdId $= "")) && (%this.videoRetryScdId != 0))
    {
        cancel(%this.videoRetryScdId);
    }
    %this.videoRetryScdId = 0;
    if (($VideoRendererLoadable > 0) && !%this.getPlayWithPlaylist())
    {
        %this.videoRetryScdId = %this.schedule(10000, "VideoRetry");
    }
    %this.startFModMusic();
    return ;
}
function VideoRenderer::VideoRetry(%this)
{
    if (!isObject(%this))
    {
        return ;
    }
    if (strstr(%this.getNamespaceList(), "VideoRenderer") == -1)
    {
        return ;
    }
    if (%this.getPlayWithPlaylist())
    {
        error("trying to retry video and playwithplaylist = true - function not supported");
    }
    else
    {
        if (%this.getEnabled())
        {
            %this.unload();
            %this.load();
        }
    }
    return ;
}
function clientCmdOnMediaTogglerClick()
{
    if (CustomSpaceClient::isOwner())
    {
        toggleCSPanel(CSMediaDisplay);
    }
    else
    {
        MusicHud.show();
    }
    return ;
}
function FFMPEGRenderer::onUse(%this)
{
    if (%this.isServerObject())
    {
        return ;
    }
    if (CustomSpaceClient::isOwner())
    {
        toggleCSPanel(CSMediaDisplay);
    }
    else
    {
        MusicHud.show();
    }
    return ;
}
function clientCmdStartVideoPlaying(%videoGhost, %playIndex, %videoURL)
{
    doStartVideoPlaying(%videoGhost, %playIndex, %videoURL, 1);
    return ;
}
function clientCmdStartSlavePlaying(%slaveGhost, %videoGhost)
{
    doStartSlavePlaying(%slaveGhost, %videoGhost, 1);
    return ;
}
function doStartVideoPlaying(%videoGhost, %playIndex, %videoURL, %retry)
{
    %video = ServerConnection.resolveGhostID(%videoGhost);
    log("media", "debug", "doStartVideoPlaying(" @ %video @ ", " @ %playIndex @ ", \"" @ %videoURL @ "\")");
    if (isObject(%video))
    {
        if ($CSSpaceInfo != 0)
        {
            $CSSpaceInfo.videoplayer = %video;
        }
        %video.unloadVideoRenderer();
        if (!(%videoURL $= ""))
        {
            if (!(%videoURL $= ""))
            {
                %video.setMediaFile(%videoURL);
            }
            %video.setNextPlayIndex(%playIndex);
            %video.loadVideoRenderer();
            %mediaType = "";
            if (strstr(%videoURL, "v="))
            {
                %mediaType = "VIDEO";
            }
            else
            {
                if (strstr(%videoURL, "p="))
                {
                    %mediaType = "VIDEO_PLAYLIST";
                }
            }
            if (!(%mediaType $= ""))
            {
                csRecordMediaView(%videoURL, %mediaType);
            }
        }
        if (CustomSpaceClient::isOwner() && !((%videoURL $= "")))
        {
            CSMediaDisplay.syncPlayingMediaStream(%videoURL);
        }
    }
    else
    {
        if (%retry > 0)
        {
            log("media", "info", "client not ready for doStartVideoPlaying. Rescheduling.");
            schedule(1000, 0, doStartVideoPlaying, %videoGhost, %playIndex, %videoURL, %retry - 1);
        }
    }
    return ;
}
function doStartSlavePlaying(%slaveGhost, %videoGhost, %retry)
{
    %slave = ServerConnection.resolveGhostID(%slaveGhost);
    log("media", "debug", "doStartSlavePlaying(" @ %slave @ ", \"" @ %videoGhost @ "\")");
    if (isObject(%slave))
    {
        %slave.unloadVideoRenderer();
        %slave.setMediaFile(%videoGhost);
        %slave.loadVideoRenderer();
    }
    else
    {
        if (%retry > 0)
        {
            log("media", "info", "client not ready for doStartSlavePlaying. Rescheduling.");
            schedule(1000, 0, doStartSlavePlaying, %slaveGhost, %videoGhost, %retry - 1);
        }
    }
    return ;
}
function clientCmdStopVideoPlaying(%videoGhost)
{
    %video = ServerConnection.resolveGhostID(%videoGhost);
    log("media", "debug", "clientCmdStopVideoPlaying videoGhost: " @ %videoGhost @ " video: " @ %video);
    if (isObject(%video))
    {
        %video.unloadVideoRenderer();
    }
    return ;
}
function clientCmdVideoForceToPlaylistIndex(%videoGhost, %playIndex)
{
    log("media", "debug", "Force video index to " @ %playIndex);
    %video = ServerConnection.resolveGhostID(%videoGhost);
    if (isObject(%video))
    {
        %video.unload();
        %video.setNextPlayIndex(%playIndex);
        %video.load();
    }
    return ;
}
