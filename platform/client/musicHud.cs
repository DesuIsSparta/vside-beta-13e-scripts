function MusicHud::setMusicService(%this, %musicService) {
    %this.musicService = %musicService;
};
function MusicHud::displayMetaData(%this, %artist, %title, %album, %comment, %isItune) {
    %artist = utf8Decode(%artist);
    %title = utf8Decode(%title);
    %album = utf8Decode(%album);
    %comment = utf8Decode(%comment);
    if (%isItune) {
        %this.artist = %artist.getITunesSearchLink(%this, %artist, "", "");
        %this.album = %album.getITunesSearchLink(%this, %artist, %album, "");
        if (($ETS::ProjectName $= "vmtv")) {
            %this.title = %title.getITunesSearchLink(%this, %artist, %album, %title);
        }
        %this.title = %title.getSongPageLink(%this, %artist, %album, %title);
    }
    %this.artist = %artist;
    %this.album = %album;
    %this.title = %title;
    %commentData = %comment.parseComment(%this);
    %commentText = "text".get(%commentData);
    %url = "url".get(%commentData);
    if ((%url $= "")) {
        %this.comment = %commentText;
    }
    %this.comment = "<a:" @ %url @ ">" @ %commentText @ "</a>";
    %commentData.delete();
    %this.charWidth = mMax(mMax(mMax(strlen(%artist), (2.0 + strlen(%title))), strlen(%album)), strlen(%commentText));
    %this.update();
    if ((%this.currentTabIndex < HudTabs)) {
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
    if (!(%this.musicService $= "") && !(%this.musicService.getArtist() $= "")) {
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
        0.setVisible(%this.ratingControl);
        %content = "Audio is currently muted. Unmute audio to listen to music.";
    }
    if (!(%content $= "")) {
        1.setVisible(%this.ratingControl);
    }
    0.setVisible(%this.ratingControl);
    if (isObject(FMod)) {
        if (FMod.isMusicOn()) {
            %content = "Loading music info...";
        }
        %content = "You are currently in a space without music. To listen to music visit clubs, stores, apartments, or other venues that have music playing.";
    }
    %content = "FMod music not currently available.";
    %this.updateRatingText();
    %content.setText(MusicText);
    if (MusicText.isVisible()) {
    }
    if (MusicText.isAwake()) {
        MusicText.forceReflow();
    }
    %this.ratingControl.updatePosition(MusicHud);
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
    %ratingText.setText(%this.ratingControl.label);
};
function MusicHud::setRating(%this, %rating) {
    0.setRating(%this.ratingControl, %rating);
};
function MusicHud::parseComment(%this, %comment) {
    %map = new StringMap("");;
    0;
    if (isObject(MissionCleanup)) {
        %map.add(MissionCleanup);
    }
    %comment = NextToken(%comment, var, ":");
    if (!(%var $= "DOPP")) {
        return %map;
    }
    %url = NextToken(%comment, var, "|");
    %var.put(%map, "text");
    if ((getSubStr(%url, 0, 7) $= "http://")) {
        getSubStr(%url, 7, (strlen(%url) - 7.0)).put(%map, "url");
    }
    %url.put(%map, "url");
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
        "music".selectTabWithName(HudTabs);
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
    0.keepOpen(%this);
    %this.update();
};
function MusicHud::isShowing(%this) {
    return (HudTabs.getCurrentTab().name $= "music");
};
function MusicHud::setChangeStationAllowed(%this, %flag) {
    %flag.setVisible(MusicHudMyMediaButton);
    %flag = 0;
    if (%flag) {
        1.setVisible(MusicHudChangeStationButton);
    }
    0.setVisible(MusicHudChangeStationButton);
    "basic".setView(%this);
};
function MusicHud::setView(%this, %view) {
    if ((%view $= "basic")) {
        1.setVisible(MusicHudBasicView);
        0.setVisible(MusicHudEditView);
    }
    if ((%view $= "change_station")) {
        0.setVisible(MusicHudBasicView);
        1.setVisible(MusicHudEditView);
        %this.fillStationPopup();
    }
};
function MusicHud::fillStationPopup(%this) {
    MusicHudStationPopup.clear();
    0.setActive(MusicHudStationPopup);
    if (!(%this.station $= "")) {
        %this.station.setText(MusicHudStationPopup);
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
        %field.add(MusicHudStationPopup);
        %i = (%i + 1.0);
    }
    if (((%i < getFieldCount(%stations)) @ " " @ %this.station $= "")) {
        0.SetSelected(MusicHudStationPopup);
    }
    1.setActive(MusicHudStationPopup);
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
