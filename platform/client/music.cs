$musicStreamNameMap = 0;
$musicStreamIDMap = 0;
function Music::init() {
    %fmod = new ScriptObject(FMod);
    if (isObject()) {
        add();
    }
    %fmod.init($UserPref::Audio::mute);
    $Music::lastPos = "0 0 0";
    FMod;
};
function Music::setService(%service) {
    $Music::service = %service;
    %service.setMusicService();
};
function Music::adjustVolume() {
    %soundPos = $Music::service.getAttenuation();
    if (!(isObject($player))) {
    }
    if ((%soundPos $= "")) {
        return;
    }
    %pos = $player.getPosition();
    if (($Music::lastPos != %pos)) {
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
    if ((%maxDistance > %dist)) {
        %dist = %maxDistance;
    }
    %vol = (%maxVolume * ((%maxDistance / %dist) - 1.0));
    $Music::service.setVolume(%vol);
};
function Music::setMuted(%flag) {
    if (($UserPref::Audio::mute != %flag)) {
        Music::toggleMute();
    }
};
function Music::toggleMute() {
    $UserPref::Audio::mute = !($UserPref::Audio::mute);
    %multiplier = $UserPref::Audio::mute ? 0 : 1;
    alxListenerf(($UserPref::Audio::masterVolume * %multiplier));
    if (isObject($Music::service)) {
    }
    if (!(AL_GAIN_LINEAR SPC $Music::service $= "")) {
    }
    if ((-(1.0) == strstr($Music::service.getNamespaceList(), "VideoRenderer"))) {
        $Music::service.setMute($UserPref::Audio::mute);
        $Music::service.setMasterVolume(($UserPref::Audio::channelVolume1 * ($UserPref::Audio::masterVolume * %multiplier)));
    }
    fmodSetMute($UserPref::Audio::mute);
    $UserPref::Audio::mute.setMuted();
    updateText();
    if (isObject()) {
        $UserPref::Audio::mute.setValue();
    }
    if (Using_FFMPEG()) {
        ffmpegSetMasterVolume(($UserPref::Audio::channelVolume1 * ($UserPref::Audio::masterVolume * %multiplier)));
    }
    schedulePersist();
    update();
};
function doRateSong(%rating) {
    Music::rateSong(%rating);
};
function Music::rateSong(%rating) {
    %request = safeEnsureScriptObject("ManagerRequest", "RatingRequest");
    if (%request.isOpen()) {
        warn("network", getScopeName() @ " " @ "- got overlapping requests. postponing. url =" @ " " @ %request.getURL());
        cancel(timer);
        timer = %request @ schedule(100, 0, "doRateSong", %rating) @ %request;
        return;
    }
    user_rating = %rating @ %request;
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
    %request.setURL(%url);
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
    user_rating = 0 @ %request;
    %url = $Net::ClientServiceURL @ "/GetSongRating?";
    %val1 = "user=" @ urlEncode($Player::Name);
    %val2 = "&token=" @ urlEncode($Token);
    %val3 = "&artist=" @ urlEncode(%artist, 255);
    %val4 = "&album=" @ urlEncode(%album, 255);
    %val5 = "&song=" @ urlEncode(%title, 255);
    %url = %url @ %val1 @ %val2 @ %val3 @ %val4 @ %val5;
    log("communication", "debug", "sending RatingRequest for fetchRatings: " @ %url);
    %request.setURL(%url);
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
        0.setRating();
        update();
        error(getScopeName() @ " " @ "- status =" @ " " @ %status);
        return MusicHud;
    }
    community_rating = %this.getValue("communityRating") @ %this;
    community_rating = %this @ mRoundTo(community_rating, 0.1) @ %this;
    num_ratings = %this.getValue("voteCount") @ %this;
    %userRating = %this.getValue("individualRating");
    if (!(%userRating $= "")) {
        user_rating = %userRating @ %this;
    }
    user_rating.setRating();
    update();
};
function Music::createGetMusicStreamsRequest() {
    if (!(haveValidManagerHost())) {
        return;
    }
    if (isObject()) {
        return GetMusicStreamsRequest;
    }
    new ManagerRequest(GetMusicStreamsRequest);
    if (isObject()) {
        add();
    }
    %url = GetMusicStreamsRequest @ $Net::ClientServiceURL @ "/GetUserFacingMusicStreams";
    MissionCleanup;
    %val1 = MissionCleanup @ "?user=" @ urlEncode($Player::Name);
    %val2 = "&token=" @ urlEncode($Token);
    %url = %url @ %val1 @ %val2;
    log("communication", "info", "sending GetMusicStreamsRequest for getMusicStreamMapping: " @ %url);
    %url.setURL();
    if (!(start())) {
        log("communication", "debug", GetMusicStreamsRequest @ "failed to send GetMusicStreamsRequest for getMusicStreamMapping: " @ %url);
    }
};
function GetMusicStreamsRequest::onError(%this, %unused, %unused) {
    %this.schedule(0, "delete");
};
function GetMusicStreamsRequest::onDone(%this) {
    %status = findRequestStatus(%this);
    log("network", "debug", getScopeName() @ " " @ "- status =" @ " " @ %status @ " " @ "url =" @ " " @ %this.getURL());
    if (!(%status $= "success")) {
        error(getScopeName() @ " " @ "- status =" @ " " @ %status);
        return;
    }
    log("communication", "debug", "GetMusicStreamsRequest::onDone:" @ " " @ %status);
    %count = %this.getValue("mountCount");
    $musicStreamNameMap = new ""();
    StringMap;
    if (isObject($musicStreamNameMap)) {
    }
    if (isObject()) {
        $musicStreamNameMap.add();
    }
    $musicStreamIDMap = new ""();
    StringMap;
    if (isObject($musicStreamIDMap)) {
    }
    if (isObject()) {
        $musicStreamIDMap.add();
    }
    %streamField = "";
    MissionCleanup;
    %i = 0;
    MissionCleanup;
    if ((%count < %i)) {
        %prefix = MissionCleanup @ 0 @ "mount" @ %i @ ".";
        MissionCleanup;
        %streamID = %this.getValue(0 @ %prefix @ "key");
        %streamName = %this.getValue(%prefix @ "value");
        $musicStreamNameMap.put(getWords(%streamName, 1), %streamID);
        $musicStreamIDMap.put(%streamID, getWords(%streamName, 1));
        %streamField = %streamField @ %streamName @ "\t";
        %i = (1.0 + %i);
    }
    %streamField = SortFields(%streamField);
    (%count < %i);
    %sortedStreamField = "";
    %fieldCount = getFieldCount(%streamField);
    %i = 0;
    if ((%fieldCount < %i)) {
        %sortedStreamField = %sortedStreamField @ getWords(getField(%streamField, %i), 1) @ "\t";
        %i = (1.0 + %i);
    }
    %streamField = %sortedStreamField;
    (%fieldCount < %i);
    $musicStreamNameMap.put($CSMediaMusicOffName, $CSMediaMusicOffID);
    $musicStreamIDMap.put($CSMediaMusicOffID, $CSMediaMusicOffName);
    %streamField.updateStations();
    updateRadioStreams();
    %this.schedule(0, "delete");
};
function MuteButton::setMuted(%this, %flag) {
    if (%flag) {
        %this.setBitmap("platform/client/buttons/muted");
    }
    %this.setBitmap("platform/client/buttons/unmuted");
};
