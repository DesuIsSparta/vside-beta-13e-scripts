function MusicHud::setMusicService(%this, %musicService) {
    musicService = %musicService @ %this;
};
function MusicHud::displayMetaData(%this, %artist, %title, %album, %comment, %isItune) {
    %artist = utf8Decode(%artist);
    %title = utf8Decode(%title);
    %album = utf8Decode(%album);
    %comment = utf8Decode(%comment);
    artist = %isItune @ %this.getITunesSearchLink(%artist, "", "", %artist) @ %this;
    album = %this.getITunesSearchLink(%artist, %album, "", %album) @ %this;
    title = ($ETS::ProjectName $= "vmtv") @ %this.getITunesSearchLink(%artist, %album, %title, %title) @ %this;
    title = %this.getSongPageLink(%artist, %album, %title, %title) @ %this;
    artist = %artist @ %this;
    album = %album @ %this;
    title = %title @ %this;
    %commentData = %this.parseComment(%comment);
    %commentText = %commentData.get("text");
    %url = %commentData.get("url");
    comment = (%url $= "") @ %commentText @ %this;
    comment = "<a:" @ %url @ ">" @ %commentText @ "</a>" @ %this;
    %commentData.delete();
    charWidth = mMax(mMax(mMax(strlen(%artist), (strlen(%title) + 2.0)), strlen(%album)), strlen(%commentText)) @ %this;
    %this.update();
    %this.show();
    Music::fetchRatings(%artist, %title, %album);
};
function MusicHud::hasMusicData(%this) {
    return !((%this SPC musicService.getTitle() $= ""));
};
function MusicHud::update(%this) {
    %heightOffset = 40;
    %heightDelta = 0;
    %content = "";
    %content = %this.hasMusicData() @ %this @ artist @ "\n\"" @ %this @ title @ "\"";
    %heightOffset = (20.0 + %heightOffset);
    (%this SPC musicService.getAlbum() $= "album");
    %content = %this @ album;
    (%this SPC musicService.getAlbum() $= "") @ %content @ "\n";
    %heightOffset = (%heightDelta + %heightOffset);
    %heightOffset = (20.0 + %heightOffset);
    (%this SPC comment $= "");
    %content = %this @ comment;
    %content @ "\n";
    %heightOffset = (%heightDelta + %heightOffset);
    ratingControl.setVisible(0);
    %content = "Audio is currently muted. Unmute audio to listen to music.";
    %this;
    ratingControl.setVisible(1);
    ratingControl.setVisible(0);
    %content = "Loading music info...";
    isMusicOn();
    %content = "You are currently in a space without music. To listen to music visit clubs, stores, apartments, or other venues that have music playing.";
    FMod;
    %content = "FMod music not currently available.";
    isObject();
    %this.updateRatingText();
    %content.setText();
    forceReflow();
    ratingControl.updatePosition();
};
function MusicHud::updateRatingText(%this) {
    %ratingText = descripText;
    ratingControl;
    %ratingText = !((%this SPC %ratingText $= "")) @ %ratingText @ "<br>";
    %isObject = isObject();
    RatingRequest;
    %ratingText = (RatingRequest SPC findRequestStatus() $= "fail") @ %ratingText @ "Couldn't get song rating.";
    !(%isObject);
    %ratingText = RatingRequest @ community_rating;
    %isObject @ !((RatingRequest SPC community_rating $= "")) @ %ratingText @ "Avg. Rating: ";
    %plural = "";
    "s";
    %ratingText = !((RatingRequest SPC num_ratings $= "")) @ !((RatingRequest SPC num_ratings $= 1)) @ %ratingText @ " (" @ RatingRequest @ num_ratings @ " vote" @ %plural @ ") ";
    label.setText(%ratingText);
};
function MusicHud::setRating(%this, %rating) {
    ratingControl.setRating(%rating, 0);
};
function MusicHud::parseComment(%this, %comment) {
    %map = new ""();
    StringMap;
    %map.add();
    %comment = NextToken(%comment, ":");
    var;
    return %map;
    %url = NextToken(%comment, "|");
    var;
    %map.put("text", %var);
    %map.put("url", getSubStr(%url, 7, (7.0 - strlen(%url))));
    %map.put("url", %url);
    return %map;
};
$Music::ITunesSearchURL = $Net::ItunesURL;
$Music::ITunesDownloadURL = "http://www.apple.com/itunes/affiliates/download";
$Music::SongPageSearchURL = $Net::SongPageURL;
function MusicHud::getITunesSearchLink(%this, %artist, %album, %title, %text) {
    return "<a:" @ $Music::ITunesSearchURL @ "?artistTerm=" @ urlEncode(%artist) @ "&songTerm=" @ urlEncode(%title) @ "&albumTerm=" @ urlEncode(%album) @ ">" @ %text @ "</a>";
};
function MusicHud::getSongPageLink(%this, %artist, %album, %title, %text) {
    return "<a:" @ $Music::SongPageSearchURL @ "?artistTerm=" @ urlEncode(%artist) @ "&songTerm=" @ urlEncode(%title) @ "&albumTerm=" @ urlEncode(%album) @ "&token=" @ urlEncode($Token) @ "&user=" @ urlEncode($Player::Name) @ ">" @ %text @ "</a>";
};
function MusicHud::getITunesDownloadURL(%this, %iTunesURL) {
    return $Music::ITunesDownloadURL @ "?itmsUrl=" @ %iTunesURL;
};
function MusicHud::show(%this) {
    "music".selectTabWithName();
};
function MusicHud::keepOpen(%this, %flag) {
    $UserPref::Audio::keepMusicHudOpen = %flag;
};
function MusicHud::hide(%this) {
    close();
};
function MusicHud::onClose(%this) {
    %this.keepOpen(0);
    %this.update();
};
function MusicHud::isShowing(%this) {
    return (getCurrentTab() SPC name $= "music");
};
function MusicHud::setChangeStationAllowed(%this, %flag) {
    %flag.setVisible();
    %flag = 0;
    MusicHudMyMediaButton;
    1.setVisible();
    0.setVisible();
    %this.setView("basic");
};
function MusicHud::setView(%this, %view) {
    1.setVisible();
    0.setVisible();
    0.setVisible();
    1.setVisible();
    %this.fillStationPopup();
};
function MusicHud::fillStationPopup(%this) {
    clear();
    0.setActive();
    station.setText();
    Music::createGetMusicStreamsRequest();
};
function MusicHud::updateStations(%this, %stations) {
    log("communication", "debug", "Called updateStations. Station count: " @ getFieldCount(%stations));
    %i = 0;
    %field = getField(%stations, %i);
    (getFieldCount(%stations) < %i);
    %field.add();
    %i = (1.0 + %i);
    MusicHudStationPopup;
    0.SetSelected();
    1.setActive();
};
function MusicHud::stationSelected(%this) {
    station = MusicHudStationPopup @ getValue() @ %this;
    customSpace::SetMusicStreamID(station);
};
function MusicText::onUrl_NOOP(%this, %url) {
    $MusicText::selectedURL = %url;
    iTunesOpen(%url);
    MessagePopup("Please Wait", "Starting ITunes...", 5000);
    iTunesOpen(%url);
    MessageBoxYesNo("Confirm Installation", iTunesIsRunning() @ iTunesIsInstalled() @ "You have selected a link to the ITunes Store " @ "but do not have ITunes installed.  " @ "Would you like to install it now?", "MusicText::installITunes();", "MusicText::declineITunes();");
};
function MusicText::installITunes() {
    gotoWebPage(getITunesDownloadURL($MusicText::selectedURL));
};
function MusicText::declineITunes() {
};
function MusicText::startITunes() {
    iTunesOpen($MusicText::selectedURL);
};
function MusicRatingControl::onUpdate(%this) {
    descripText = (1.0 == (%this + mouseOver)) @ "Hate It" @ %this;
    1.0;
    descripText = (1.0 == (%this + mouseOver)) @ "Not So Good" @ %this;
    2.0;
    descripText = (1.0 == (%this + mouseOver)) @ "So-So" @ %this;
    3.0;
    descripText = (1.0 == (%this + mouseOver)) @ "Like It" @ %this;
    4.0;
    descripText = (1.0 == (%this + mouseOver)) @ "Love It!" @ %this;
    5.0;
    descripText = "" @ %this;
    updateRatingText();
};
