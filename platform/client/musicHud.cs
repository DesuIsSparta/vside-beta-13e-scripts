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
    %this.charWidth = mMax(mMax(mMax(strlen(%artist), (2.0 + strlen(%title))), strlen(%album)), strlen(%commentText));
    %this.update();
    if ((HudTabs.currentTabIndex < 0.0) || (HudTabs.getCurrentTab().name $= "music")) {
    }
    if (!$UserPref::Audio::mute) {
    }
    if (%this.hasMusicData()) {
        %this.show();
    }
    if (!(%artist $= "") || !(%title $= "") || !(%album $= "")) {
        Music::fetchRatings(%artist, %title, %album);
    }
};
function MusicHud::hasMusicData(%this) {
    if (!(%this.musicService $= "")) {
    }
    return !(%this.musicService.getArtist() $= "") || !(%this.musicService.getTitle() $= "");
};
function MusicHud::update(%this) {
    %heightOffset = 40;
    %heightDelta = 0;
    %content = "";
    if (%this.hasMusicData()) {
        %content = %this.artist @ "\n\"" @ %this.title @ "\"";
        if ((%this.musicService.getAlbum() $= "") || (%this.musicService.getAlbum() $= "album")) {
            %heightOffset = (%heightOffset + 20.0);
        }
        %content = %content @ "\n" @ %this.album;
        %heightOffset = (%heightOffset + %heightDelta);
        if ((%this.comment $= "")) {
            %heightOffset = (%heightOffset + 20.0);
        }
        %content = %content @ "\n" @ %this.comment;
        %heightOffset = (%heightOffset + %heightDelta);
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
    MusicText.setText(%content);
    if (MusicText.isVisible()) {
    }
    if (MusicText.isAwake()) {
        MusicText.forceReflow();
    }
    MusicHud.ratingControl.updatePosition();
};
function MusicHud::updateRatingText(%this) {
    %ratingText = %this.ratingControl.descripText;
    if (!(%ratingText $= "")) {
        %ratingText = %ratingText @ "<br>";
    }
    %isObject = isObject(RatingRequest);
    if (!%isObject || (findRequestStatus(RatingRequest) $= "fail")) {
        %ratingText = %ratingText @ "Couldn't get song rating.";
    }
    if (%isObject) {
    }
    if (!(RatingRequest.community_rating $= "")) {
        %ratingText = %ratingText @ "Avg. Rating: " @ RatingRequest.community_rating;
        if (!(RatingRequest.num_ratings $= "")) {
            %plural = !(RatingRequest.num_ratings $= 1) ? "s" : "";
            %ratingText = %ratingText @ " (" @ RatingRequest.num_ratings @ " vote" @ %plural @ ") ";
        }
    }
    %this.ratingControl.label.setText(%ratingText);
};
function MusicHud::setRating(%this, %rating) {
    %this.ratingControl.setRating(%rating, 0);
};
function MusicHud::parseComment(%this, %comment) {
    %map = new StringMap("");
    if (isObject(MissionCleanup)) {
        MissionCleanup.add(%map);
    }
    %comment = NextToken(%comment, var, ":");
    if (!(%var $= "DOPP")) {
        return %map;
    }
    %url = NextToken(%comment, var, "|");
    %map.put("text", %var);
    if ((getSubStr(%url, 0, 7) $= "http://")) {
        %map.put("url", getSubStr(%url, 7, (strlen(%url) - 7.0)));
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
        HudTabs.selectTabWithName("music");
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
    MusicHudMyMediaButton.setVisible(%flag);
    %flag = 0;
    if (%flag) {
        MusicHudChangeStationButton.setVisible(1);
    }
    MusicHudChangeStationButton.setVisible(0);
    %this.setView("basic");
};
function MusicHud::setView(%this, %view) {
    if ((%view $= "basic")) {
        MusicHudBasicView.setVisible(1);
        MusicHudEditView.setVisible(0);
    }
    if ((%view $= "change_station")) {
        MusicHudBasicView.setVisible(0);
        MusicHudEditView.setVisible(1);
        %this.fillStationPopup();
    }
};
function MusicHud::fillStationPopup(%this) {
    MusicHudStationPopup.clear();
    MusicHudStationPopup.setActive(0);
    if (!(%this.station $= "")) {
        MusicHudStationPopup.setText(%this.station);
    }
    Music::createGetMusicStreamsRequest();
};
function MusicHud::updateStations(%this, %stations) {
    log("communication", "debug", "Called updateStations. Station count: " @ getFieldCount(%stations));
    %i = 0;
    while ((%i < getFieldCount(%stations))) {
        %field = getField(%stations, %i);
        if ((%field $= "")) {
        }
        MusicHudStationPopup.add(%field);
        %i = (%i + 1.0);
    }
    if (((%i < getFieldCount(%stations)) @ " " @ %this.station $= "")) {
        MusicHudStationPopup.SetSelected(0);
    }
    MusicHudStationPopup.setActive(1);
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
    if (((%this.mouseOver + 1.0) == 1.0)) {
        %this.descripText = "Hate It";
    }
    if (((%this.mouseOver + 1.0) == 2.0)) {
        %this.descripText = "Not So Good";
    }
    if (((%this.mouseOver + 1.0) == 3.0)) {
        %this.descripText = "So-So";
    }
    if (((%this.mouseOver + 1.0) == 4.0)) {
        %this.descripText = "Like It";
    }
    if (((%this.mouseOver + 1.0) == 5.0)) {
        %this.descripText = "Love It!";
    }
    %this.descripText = "";
    MusicHud.updateRatingText();
};
