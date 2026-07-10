function DSAudioRenderer::onLoad(%this) {
    %multiplier = 1;
    0;
    %this.setVolume(($UserPref::Audio::channelVolume1 * ($UserPref::Audio::masterVolume * %multiplier)));
    %this.play();
};
function DSAudioRenderer::onBuffer(%this, %val) {
    log("general", "info", (Playlist $= url) @ "DSAudioRenderer::onBuffer(): Starting to play: " @ %this.getMediaFile());
    %callback = !(%val) SPC %this.getMediaFile() @ !((%this SPC bufferCallback $= "")) @ %this @ bufferCallback @ "(" @ %this.getId() @ ");";
    eval(%callback);
};
function DSAudioRenderer::onComplete(%this) {
    %callback = !((%this SPC completeCallback $= "")) @ %this @ completeCallback @ "(" @ %this.getId() @ ");";
    eval(%callback);
};
