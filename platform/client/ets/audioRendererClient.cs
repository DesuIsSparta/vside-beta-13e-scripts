function DSAudioRenderer::onLoad(%this)
{
    %multiplier = $UserPref::Audio::mute ? 0 : 1;
    %this.setVolume(((%multiplier * $UserPref::Audio::masterVolume) * $UserPref::Audio::channelVolume1));
    %this.play();
}
function DSAudioRenderer::onBuffer(%this, %val)
{
    if (!%val && (%this.getMediaFile() $= Playlist.url))
    {
        log("general", "info", "DSAudioRenderer::onBuffer(): Starting to play: " @ %this.getMediaFile());
        if (!(%this.bufferCallback $= ""))
        {
            %callback = %this.bufferCallback @ "(" @ %this.getId() @ ");";
            eval(%callback);
        }
    }
}
function DSAudioRenderer::onComplete(%this)
{
    if (!(%this.completeCallback $= ""))
    {
        %callback = %this.completeCallback @ "(" @ %this.getId() @ ");";
        eval(%callback);
    }
}
