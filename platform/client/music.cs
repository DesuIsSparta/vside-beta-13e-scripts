$musicStreamNameMap = 0;
$musicStreamIDMap = 0;
function Music::init() {
    %fmod = new ScriptObject(FMod);;
    if (isObject(MissionCleanup)) {
        FMod.add(MissionCleanup);
    }
    $UserPref::Audio::mute.init(%fmod);
    $Music::lastPos = "0 0 0";
};
function Music::setService(%service) {
    $Music::service = %service;
    %service.setMusicService(MusicHud);
};
function Music::adjustVolume() {
    %soundPos = $Music::service.getAttenuation();
    if (!(isObject($player))) {
    }
    if ((%soundPos $= "")) {
        return;
    }
    %pos = $player.getPosition();
    if ((%pos != $Music::lastPos)) {
        Music::attenuate(%pos, %soundPos, 30, $Music::service.getSourceVolume());
        $Music::lastPos = %pos;
    }
};
function musicAttenuationTimer() {
    if (!($Music::service.getAttenuation() $= "")) {
        Music::adjustVolume();
        schedule(250, 0, "musicAttenuationTimer");
    }
    $Music::lastPos = "0 0 0";
};
function Music::attenuate(%playerPos, %soundPos, %maxDistance, %maxVolume) {
    %dist = VectorLen(VectorDist(%playerPos, %soundPos));
    if ((%dist > %maxDistance)) {
        %dist = %maxDistance;
    }
    %vol = ((1.0 - (%dist / %maxDistance)) * %maxVolume);
    %vol.setVolume($Music::service);
};
function Music::setMuted(%flag) {
    if ((%flag != $UserPref::Audio::mute)) {
        Music::toggleMute();
    }
};
function Music::toggleMute() {
    $UserPref::Audio::mute = !($UserPref::Audio::mute);
    %multiplier = $UserPref::Audio::mute ? 0 : 1;
    alxListenerf(AL_GAIN_LINEAR, (%multiplier * $UserPref::Audio::masterVolume));
    if (isObject($Music::service)) {
    }
    if (!($Music::service $= "")) {
    }
    if ((strstr($Music::service.getNamespaceList(), "VideoRenderer") == -(1.0))) {
        $UserPref::Audio::mute.setMute($Music::service);
        ((%multiplier * $UserPref::Audio::masterVolume) * $UserPref::Audio::channelVolume1).setMasterVolume($Music::service);
    }
    fmodSetMute($UserPref::Audio::mute);
    $UserPref::Audio::mute.setMuted(MuteButton);
    MusicTabToggleSoundTxt.updateText();
    if (isObject(MuteCheckBox)) {
        $UserPref::Audio::mute.setValue(MuteCheckBox);
    }
    if (Using_FFMPEG()) {
        ffmpegSetMasterVolume(((%multiplier * $UserPref::Audio::masterVolume) * $UserPref::Audio::channelVolume1));
    }
    schedulePersist();
    MusicHud.update();
};
function doRateSong(%rating) {
    Music::rateSong(%rating);
};
function Music::rateSong(%rating) {
    %request = safeEnsureScriptObject("ManagerRequest", "RatingRequest");
    if (%request.isOpen()) {
        warn("network", getScopeName() @ " " @ "- got overlapping requests. postponing. url =" @ " " @ %request.getURL());
        cancel(%request.timer);
        %request.timer = schedule(100, 0, "doRateSong", %rating);
        return;
    }
    %request.user_rating = %rating;
    %url = $Net::ClientServiceURL @ "/RateSong?";
    %val1 = "user=" @ urlEncode($Player::Name);
    %val2 = "&token=" @ urlEncode($Token);
    %val3 = "&artist=" @ urlEncode($Music::service.getArtist(), 255);
    %val4 = "&album=" @ urlEncode($Music::service.getAlbum(), 255);
    %val5 = "&song=" @ urlEncode($Music::service.getTitle(), 255);
    %val6 = "&rating=" @ urlEncode(%rating);
    if (("&artist=" $= %val3)) {
    }
    if (("&song=" $= %val5)) {
        warn("did not have artist and song name, not sending rating request: " @ %url);
        return;
    }
    %url = %url @ %val1 @ %val2 @ %val3 @ %val4 @ %val5 @ %val6;
    log("communication", "debug", "sending RatingRequest for rateSong: " @ %url);
    %url.setURL(%request);
    if (!(%request.start())) {
        log("communication", "debug", "failed to send RatingRequest for rateSong: " @ %url);
    }
};
function Music::fetchRatings(%artist, %title, %album) {
    if (!(haveValidManagerHost())) {
        return;
    }
    if (($Player::Name $= "")) {
    }
    if (($Token $= "")) {
        log("communication", "debug", "the get_song_ratings request will not be made because we don't have a valid user or token");
        return;
    }
    %request = safeEnsureScriptObject("ManagerRequest", "RatingRequest");
    if (%request.isOpen()) {
        warn("network", getScopeName() @ " " @ "- got overlapping requests. aborting. url =" @ " " @ %request.getURL());
        return;
    }
    %request.user_rating = 0;
    %url = $Net::ClientServiceURL @ "/GetSongRating?";
    %val1 = "user=" @ urlEncode($Player::Name);
    %val2 = "&token=" @ urlEncode($Token);
    %val3 = "&artist=" @ urlEncode(%artist, 255);
    %val4 = "&album=" @ urlEncode(%album, 255);
    %val5 = "&song=" @ urlEncode(%title, 255);
    %url = %url @ %val1 @ %val2 @ %val3 @ %val4 @ %val5;
    log("communication", "debug", "sending RatingRequest for fetchRatings: " @ %url);
    %url.setURL(%request);
    if (!(%request.start())) {
        log("communication", "debug", "failed to send RatingRequest for fetchRatings: " @ %url);
    }
};
function RatingRequest::onError(%this, %unused, %unused) {
};
function RatingRequest::onDone(%this) {
    %status = findRequestStatus(%this);
    log("network", "debug", getScopeName() @ " " @ "- status =" @ " " @ %status @ " " @ "url =" @ " " @ %this.getURL());
    if (!(%status $= "success")) {
        0.setRating(MusicHud);
        MusicHud.update();
        error(getScopeName() @ " " @ "- status =" @ " " @ %status);
        return;
    }
    %this.community_rating = "communityRating".getValue(%this);
    %this.community_rating = mRoundTo(%this.community_rating, 0.1);
    %this.num_ratings = "voteCount".getValue(%this);
    %userRating = "individualRating".getValue(%this);
    if (!(%userRating $= "")) {
        %this.user_rating = %userRating;
    }
    %this.user_rating.setRating(MusicHud);
    MusicHud.update();
};
function Music::createGetMusicStreamsRequest() {
    if (!(haveValidManagerHost())) {
        return;
    }
    if (isObject(GetMusicStreamsRequest)) {
        return;
    }
    new ManagerRequest(GetMusicStreamsRequest);
    if (isObject(MissionCleanup)) {
        GetMusicStreamsRequest.add(MissionCleanup);
    }
    %url = $Net::ClientServiceURL @ "/GetUserFacingMusicStreams";
    %val1 = "?user=" @ urlEncode($Player::Name);
    %val2 = "&token=" @ urlEncode($Token);
    %url = %url @ %val1 @ %val2;
    log("communication", "info", "sending GetMusicStreamsRequest for getMusicStreamMapping: " @ %url);
    %url.setURL(GetMusicStreamsRequest);
    if (!(GetMusicStreamsRequest.start())) {
        log("communication", "debug", "failed to send GetMusicStreamsRequest for getMusicStreamMapping: " @ %url);
    }
};
function GetMusicStreamsRequest::onError(%this, %unused, %unused) {
    "delete".schedule(%this, 0);
};
function GetMusicStreamsRequest::onDone(%this) {
    %status = findRequestStatus(%this);
    log("network", "debug", getScopeName() @ " " @ "- status =" @ " " @ %status @ " " @ "url =" @ " " @ %this.getURL());
    if (!(%status $= "success")) {
        error(getScopeName() @ " " @ "- status =" @ " " @ %status);
        return;
    }
    log("communication", "debug", "GetMusicStreamsRequest::onDone:" @ " " @ %status);
    %count = "mountCount".getValue(%this);
    $musicStreamNameMap = new StringMap("");;
    0;
    if (isObject($musicStreamNameMap)) {
    }
    if (isObject(MissionCleanup)) {
        $musicStreamNameMap.add(MissionCleanup);
    }
    $musicStreamIDMap = new StringMap("");;
    0;
    if (isObject($musicStreamIDMap)) {
    }
    if (isObject(MissionCleanup)) {
        $musicStreamIDMap.add(MissionCleanup);
    }
    %streamField = "";
    %i = 0;
    while ((%i < %count)) {
        %prefix = "mount" @ %i @ ".";
        %streamID = %prefix @ "key".getValue(%this);
        %streamName = %prefix @ "value".getValue(%this);
        %streamID.put($musicStreamNameMap, getWords(%streamName, 1));
        getWords(%streamName, 1).put($musicStreamIDMap, %streamID);
        %streamField = %streamField @ %streamName @ "\t";
        %i = (%i + 1.0);
    }
    %streamField = SortFields(%streamField);
    (%i < %count);
    %sortedStreamField = "";
    %fieldCount = getFieldCount(%streamField);
    %i = 0;
    while ((%i < %fieldCount)) {
        %sortedStreamField = %sortedStreamField @ getWords(getField(%streamField, %i), 1) @ "\t";
        %i = (%i + 1.0);
    }
    %streamField = %sortedStreamField;
    (%i < %fieldCount);
    $CSMediaMusicOffID.put($musicStreamNameMap, $CSMediaMusicOffName);
    $CSMediaMusicOffName.put($musicStreamIDMap, $CSMediaMusicOffID);
    %streamField.updateStations(MusicHud);
    CSMediaDisplay.updateRadioStreams();
    "delete".schedule(%this, 0);
};
function MuteButton::setMuted(%this, %flag) {
    if (%flag) {
        "platform/client/buttons/muted".setBitmap(%this);
    }
    "platform/client/buttons/unmuted".setBitmap(%this);
};
