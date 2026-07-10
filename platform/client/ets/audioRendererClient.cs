function DSAudioRenderer::onLoad(%this) {
    %multiplier = $UserPref::Audio::mute ? 0 : 1;
    ((%multiplier * $UserPref::Audio::masterVolume) * $UserPref::Audio::channelVolume1).setVolume(%this);
    %this.play();
};
function DSAudioRenderer::onBuffer(%this, %val) {
    if (!(%val) && (Playlist $= url)) {
        log("general", "info", "DSAudioRenderer::onBuffer(): Starting to play: " @ %this.getMediaFile());
        if (!(%this.getMediaFile() @ " " @ %this.bufferCallback $= "")) {
            %callback = %this.bufferCallback @ "(" @ %this.getId() @ ");";
            eval(%callback);
        }
    }
};
function DSAudioRenderer::onComplete(%this) {
    if (!(%this.completeCallback $= "")) {
        %callback = %this.completeCallback @ "(" @ %this.getId() @ ");";
        eval(%callback);
    }
};
