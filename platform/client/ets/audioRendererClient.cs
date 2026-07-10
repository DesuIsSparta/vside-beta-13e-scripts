function DSAudioRenderer::onLoad(%this) {
    %multiplier = $UserPref::Audio::mute ? 0 : 1;
    %this.setVolume(($UserPref::Audio::channelVolume1 * ($UserPref::Audio::masterVolume * %multiplier)));
    %this.play();
};
function DSAudioRenderer::onBuffer(%this, %val) {
    if (!(%val)) {
        if ((Playlist $= url)) {
            log("general", "info", %this.getMediaFile() @ "DSAudioRenderer::onBuffer(): Starting to play: " @ %this.getMediaFile());
            if (!(%this SPC bufferCallback $= "")) {
                %callback = %this @ bufferCallback @ "(" @ %this.getId() @ ");";
                eval(%callback);
            }
        }
    }
};
function DSAudioRenderer::onComplete(%this) {
    if (!(%this SPC completeCallback $= "")) {
        %callback = %this @ completeCallback @ "(" @ %this.getId() @ ");";
        eval(%callback);
    }
};
