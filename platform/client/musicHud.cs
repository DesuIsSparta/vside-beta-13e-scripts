function MusicHud::setMusicService(%this, %musicService) {
    musicService = %musicService @ %this;
};
function MusicHud::displayMetaData(%this, %artist, %title, %album, %comment, %isItune) {
    %artist = utf8Decode(%artist);
    %title = utf8Decode(%title);
    %album = utf8Decode(%album);
    %comment = utf8Decode(%comment);
    if (%isItune) {
        artist = %this.getITunesSearchLink(%artist, "", "", %artist) @ %this;
        album = %this.getITunesSearchLink(%artist, %album, "", %album) @ %this;
        if (($ETS::ProjectName $= "vmtv")) {
            title = %this.getITunesSearchLink(%artist, %album, %title, %title) @ %this;
        }
        title = %this.getSongPageLink(%artist, %album, %title, %title) @ %this;
    }
    artist = %artist @ %this;
    album = %album @ %this;
    title = %title @ %this;
    %commentData = %this.parseComment(%comment);
    %commentText = %commentData.get("text");
    %url = %commentData.get("url");
    if ((%url $= "")) {
        comment = %commentText @ %this;
    }
    comment = "<a:" @ %url @ ">" @ %commentText @ "</a>" @ %this;
    %commentData.delete();
    charWidth = mMax(mMax(mMax(strlen(%artist), (strlen(%title) + 2.0)), strlen(%album)), strlen(%commentText)) @ %this;
    %this.update();
    if ((HudTabs < currentTabIndex)) {
    }
    if ((getCurrentTab() SPC name $= "music")) {
    }
    if (!($UserPref::Audio::mute)) {
    }
    if (%this.hasMusicData()) {
        %this.show();
    }
    if (!(HudTabs SPC %artist $= "")) {
    }
    if (!(0.0 SPC %title $= "")) {
    }
    if (!(%album $= "")) {
        Music::fetchRatings(%artist, %title, %album);
    }
};
function MusicHud::hasMusicData(%this) {
    if (!(%this SPC musicService $= "")) {
        if (!(%this SPC musicService.getArtist() $= "")) {
        }
    }
    return !(%this SPC musicService.getTitle() $= "");
};
function MusicHud::update(%this) {
    %heightOffset = 40;
    %heightDelta = 0;
    %content = "";
    if (%this.hasMusicData()) {
        %content = %this @ artist @ "\n\"" @ %this @ title @ "\"";
        if ((%this SPC musicService.getAlbum() $= "")) {
        }
        if ((%this SPC musicService.getAlbum() $= "album")) {
            %heightOffset = (20.0 + %heightOffset);
        }
        %content = %this @ album;
        %content @ "\n";
        %heightOffset = (%heightDelta + %heightOffset);
        if ((%this SPC comment $= "")) {
            %heightOffset = (20.0 + %heightOffset);
        }
        %content = %this @ comment;
        %content @ "\n";
        %heightOffset = (%heightDelta + %heightOffset);
    }
    if ($UserPref::Audio::mute) {
        ratingControl.setVisible(0);
        %content = "Audio is currently muted. Unmute audio to listen to music.";
        %this;
    }
    if (!(%content $= "")) {
        ratingControl.setVisible(1);
    }
    ratingControl.setVisible(0);
    if (isObject()) {
        if (isMusicOn()) {
            %content = "Loading music info...";
            FMod;
        }
        %content = "You are currently in a space without music. To listen to music visit clubs, stores, apartments, or other venues that have music playing.";
        FMod;
    }
    %content = "FMod music not currently available.";
    %this;
    %this.updateRatingText();
    %content.setText();
    if (isVisible()) {
    }
    if (isAwake()) {
        forceReflow();
    }
    ratingControl.updatePosition();
};
function MusicHud::updateRatingText(%this) {
    %ratingText = descripText;
    ratingControl;
    if (!(%this SPC %ratingText $= "")) {
        %ratingText = %ratingText @ "<br>";
    }
    %isObject = isObject();
    RatingRequest;
    if (!(%isObject)) {
    }
    if ((RatingRequest SPC findRequestStatus() $= "fail")) {
        %ratingText = %ratingText @ "Couldn't get song rating.";
    }
    if (%isObject) {
    }
    if (!(RatingRequest SPC community_rating $= "")) {
        %ratingText = RatingRequest @ community_rating;
        %ratingText @ "Avg. Rating: ";
        if (!(RatingRequest SPC num_ratings $= "")) {
            %plural = !(RatingRequest SPC num_ratings $= 1) ? "s" : "";
            %ratingText = %ratingText @ " (" @ RatingRequest @ num_ratings @ " vote" @ %plural @ ") ";
        }
    }
    label.setText(%ratingText);
};
function MusicHud::setRating(%this, %rating) {
    ratingControl.setRating(%rating, 0);
};
function MusicHud::parseComment(%this, %comment) {
    %map = new ""();
    StringMap;
    if (isObject()) {
        %map.add();
    }
    %comment = NextToken(%comment, ":");
    var;
    if (!(MissionCleanup SPC %var $= "DOPP")) {
        return %map;
    }
    %url = NextToken(%comment, "|");
    var;
    %map.put("text", %var);
    if ((getSubStr(%url, 0, 7) $= "http://")) {
        %map.put("url", getSubStr(%url, 7, (7.0 - strlen(%url))));
    }
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
    if (%iTunesURL[$UserPref::HudTabs::AutoOpen @ "music"]) {
        "music".selectTabWithName();
    }
};
function MusicHud::keepOpen(%this, %flag) {
    $UserPref::Audio::keepMusicHudOpen = %flag;
};
function MusicHud::hide(%this) {
    if (%this.isShowing()) {
        close();
    }
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
    if (%flag) {
        1.setVisible();
    }
    0.setVisible();
    %this.setView("basic");
};
function MusicHud::setView(%this, %view) {
    if ((%view $= "basic")) {
        1.setVisible();
        0.setVisible();
    }
    if ((MusicHudEditView SPC %view $= "change_station")) {
        0.setVisible();
        1.setVisible();
        %this.fillStationPopup();
    }
};
function MusicHud::fillStationPopup(%this) {
    clear();
    0.setActive();
    if (!(%this SPC station $= "")) {
        station.setText();
    }
    Music::createGetMusicStreamsRequest();
};
function MusicHud::updateStations(%this, %stations) {
    log("communication", "debug", "Called updateStations. Station count: " @ getFieldCount(%stations));
    %i = 0;
    if ((getFieldCount(%stations) < %i)) {
        %field = getField(%stations, %i);
        if ((%field $= "")) {
        }
        %field.add();
        %i = (1.0 + %i);
        MusicHudStationPopup;
    }
    if ((%this SPC station $= "")) {
        0.SetSelected();
    }
    1.setActive();
};
function MusicHud::stationSelected(%this) {
    station = MusicHudStationPopup @ getValue() @ %this;
    customSpace::SetMusicStreamID(station);
};
function MusicText::onUrl_NOOP(%this, %url) {
    $MusicText::selectedURL = %url;
    if (iTunesIsRunning()) {
        iTunesOpen(%url);
    }
    if (iTunesIsInstalled()) {
        MessagePopup("Please Wait", "Starting ITunes...", 5000);
        iTunesOpen(%url);
    }
    MessageBoxYesNo("Confirm Installation", "You have selected a link to the ITunes Store " @ "but do not have ITunes installed.  " @ "Would you like to install it now?", "MusicText::installITunes();", "MusicText::declineITunes();");
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
    if ((1.0 == (%this + mouseOver))) {
        descripText = 1.0 @ "Hate It" @ %this;
    }
    if ((1.0 == (%this + mouseOver))) {
        descripText = 2.0 @ "Not So Good" @ %this;
    }
    if ((1.0 == (%this + mouseOver))) {
        descripText = 3.0 @ "So-So" @ %this;
    }
    if ((1.0 == (%this + mouseOver))) {
        descripText = 4.0 @ "Like It" @ %this;
    }
    if ((1.0 == (%this + mouseOver))) {
        descripText = 5.0 @ "Love It!" @ %this;
    }
    descripText = "" @ %this;
    updateRatingText();
};
