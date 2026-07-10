function MusicHud::setMusicService(%this, %musicService) {
    %this.musicService = %musicService;
};
function MusicHud::displayMetaData(%this, %artist, %title, %album, %comment, %isItune) {
    %artist = utf8Decode(%artist);
    %title = utf8Decode(%title);
    %album = utf8Decode(%album);
    %comment = utf8Decode(%comment);
    if (%isItune) {
        %this.artist = %this.getITunesSearchLink(%artist, "", "", %artist);
        %this.album = %this.getITunesSearchLink(%artist, %album, "", %album);
        if (($ETS::ProjectName $= "vmtv")) {
            %this.title = %this.getITunesSearchLink(%artist, %album, %title, %title);
        }
        %this.title = %this.getSongPageLink(%artist, %album, %title, %title);
    }
    %this.artist = %artist;
    %this.album = %album;
    %this.title = %title;
    %commentData = %this.parseComment(%comment);
    %commentText = %commentData.get("text");
    %url = %commentData.get("url");
    if ((%url $= "")) {
        %this.comment = %commentText;
    }
    %this.comment = "<a:" @ %url @ ">" @ %commentText @ "</a>";
    %commentData.delete();
    %this.charWidth = mMax(mMax(mMax(strlen(%artist), (strlen(%title) + 2.0)), strlen(%album)), strlen(%commentText));
    %this.update();
    if ((HudTabs < %this.currentTabIndex)) {
    }
    if ((0.0 @ " " @ HudTabs.getCurrentTab().name $= "music")) {
    }
    if (!($UserPref::Audio::mute)) {
    }
    if (%this.hasMusicData()) {
        %this.show();
    }
    if (!(%artist $= "")) {
    }
    if (!(%title $= "")) {
    }
    if (!(%album $= "")) {
        Music::fetchRatings(%artist, %title, %album);
    }
};
function MusicHud::hasMusicData(%this) {
    if (!(%this.musicService $= "")) {
        if (!(%this.musicService.getArtist() $= "")) {
        }
    }
    return !(%this.musicService.getTitle() $= "");
};
function MusicHud::update(%this) {
    %heightOffset = 40;
    %heightDelta = 0;
    %content = "";
    if (%this.hasMusicData()) {
        %content = %this.artist @ "\n\"" @ %this.title @ "\"";
        if ((%this.musicService.getAlbum() $= "")) {
        }
        if ((%this.musicService.getAlbum() $= "album")) {
            %heightOffset = (20.0 + %heightOffset);
        }
        %content = %content @ "\n" @ %this.album;
        %heightOffset = (%heightDelta + %heightOffset);
        if ((%this.comment $= "")) {
            %heightOffset = (20.0 + %heightOffset);
        }
        %content = %content @ "\n" @ %this.comment;
        %heightOffset = (%heightDelta + %heightOffset);
    }
    if ($UserPref::Audio::mute) {
        %this.ratingControl.setVisible(0);
        %content = "Audio is currently muted. Unmute audio to listen to music.";
    }
    if (!(%content $= "")) {
        %this.ratingControl.setVisible(1);
    }
    %this.ratingControl.setVisible(0);
    if (isObject(FMod)) {
        if (FMod.isMusicOn()) {
            %content = "Loading music info...";
        }
        %content = "You are currently in a space without music. To listen to music visit clubs, stores, apartments, or other venues that have music playing.";
    }
    %content = "FMod music not currently available.";
    %this.updateRatingText();
    %content.setText();
    if (MusicText.isVisible()) {
    }
    if (MusicText.isAwake()) {
        MusicText.forceReflow();
    }
    %this.ratingControl.updatePosition();
};
function MusicHud::updateRatingText(%this) {
    %ratingText = %this.ratingControl.descripText;
    if (!(%ratingText $= "")) {
        %ratingText = %ratingText @ "<br>";
    }
    %isObject = isObject(RatingRequest);
    if (!(%isObject)) {
    }
    if ((findRequestStatus(RatingRequest) $= "fail")) {
        %ratingText = %ratingText @ "Couldn't get song rating.";
    }
    if (%isObject) {
    }
    if (!(RatingRequest @ " " @ %this.ratingControl.community_rating $= "")) {
        %ratingText = RatingRequest @ %this.ratingControl.community_rating;
        %ratingText @ "Avg. Rating: ";
        if (!(RatingRequest @ " " @ %this.ratingControl.num_ratings $= "")) {
            %plural = !(RatingRequest @ " " @ %this.ratingControl.num_ratings $= 1) ? "s" : "";
            %ratingText = RatingRequest @ %this.ratingControl.num_ratings @ " vote" @ %plural @ ") ";
            %ratingText @ " (";
        }
    }
    %this.ratingControl.label.setText(%ratingText);
};
function MusicHud::setRating(%this, %rating) {
    %this.ratingControl.setRating(%rating, 0);
};
function MusicHud::parseComment(%this, %comment) {
    %map = new ""();;
    StringMap;
    if (isObject(MissionCleanup)) {
        %map.add();
    }
    %comment = NextToken(%comment, ":");
    var;
    if (!(MissionCleanup @ " " @ %var $= "DOPP")) {
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
        HudTabs.close();
    }
};
function MusicHud::onClose(%this) {
    %this.keepOpen(0);
    %this.update();
};
function MusicHud::isShowing(%this) {
    return (HudTabs.getCurrentTab().name $= "music");
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
    if ((MusicHudEditView @ " " @ %view $= "change_station")) {
        0.setVisible();
        1.setVisible();
        %this.fillStationPopup();
    }
};
function MusicHud::fillStationPopup(%this) {
    MusicHudStationPopup.clear();
    0.setActive();
    if (!(MusicHudStationPopup @ " " @ %this.station $= "")) {
        %this.station.setText();
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
    if (((getFieldCount(%stations) < %i) @ " " @ %this.station $= "")) {
        0.SetSelected();
    }
    1.setActive();
};
function MusicHud::stationSelected(%this) {
    %this.station = MusicHudStationPopup.getValue();
    customSpace::SetMusicStreamID(%this.station);
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
    if ((1.0 == (1.0 + %this.mouseOver))) {
        %this.descripText = "Hate It";
    }
    if ((2.0 == (1.0 + %this.mouseOver))) {
        %this.descripText = "Not So Good";
    }
    if ((3.0 == (1.0 + %this.mouseOver))) {
        %this.descripText = "So-So";
    }
    if ((4.0 == (1.0 + %this.mouseOver))) {
        %this.descripText = "Like It";
    }
    if ((5.0 == (1.0 + %this.mouseOver))) {
        %this.descripText = "Love It!";
    }
    %this.descripText = "";
    MusicHud.updateRatingText();
};
