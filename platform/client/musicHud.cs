function MusicHud::setMusicService(%this, %musicService)
{
    %this.musicService = %musicService;
    return ;
}
function MusicHud::displayMetaData(%this, %artist, %title, %album, %comment, %isItune)
{
    %artist = utf8Decode(%artist);
    %title = utf8Decode(%title);
    %album = utf8Decode(%album);
    %comment = utf8Decode(%comment);
    if (%isItune)
    {
        %this.artist = %this.getITunesSearchLink(%artist, "", "", %artist);
        %this.album = %this.getITunesSearchLink(%artist, %album, "", %album);
        if ($ETS::ProjectName $= "vmtv")
        {
            %this.title = %this.getITunesSearchLink(%artist, %album, %title, %title);
        }
        else
        {
            %this.title = %this.getSongPageLink(%artist, %album, %title, %title);
        }
    }
    else
    {
        %this.artist = %artist;
        %this.album = %album;
        %this.title = %title;
    }
    %commentData = %this.parseComment(%comment);
    %commentText = %commentData.get("text");
    %url = %commentData.get("url");
    if (%url $= "")
    {
        %this.comment = %commentText;
    }
    else
    {
        %this.comment = "<a:" @ %url @ ">" @ %commentText @ "</a>";
    }
    %commentData.delete();
    %this.charWidth = mMax(mMax(mMax(strlen(%artist), 2 + strlen(%title)), strlen(%album)), strlen(%commentText));
    %this.update();
    if ((((HudTabs.currentTabIndex < 0) || (HudTabs.getCurrentTab().name $= "music")) && !$UserPref::Audio::mute) && %this.hasMusicData())
    {
        %this.show();
    }
    if ((!((%artist $= "")) || !((%title $= ""))) || !((%album $= "")))
    {
        Music::fetchRatings(%artist, %title, %album);
    }
    return ;
}
function MusicHud::hasMusicData(%this)
{
    return (!((%this.musicService $= "")) && !((%this.musicService.getArtist() $= ""))) || !((%this.musicService.getTitle() $= ""));
}
function MusicHud::update(%this)
{
    %heightOffset = 40;
    %heightDelta = 0;
    %content = "";
    if (%this.hasMusicData())
    {
        %content = %this.artist @ "\n\"" @ %this.title @ "\"";
        if ((%this.musicService.getAlbum() $= "") && (%this.musicService.getAlbum() $= "album"))
        {
            %heightOffset = %heightOffset + 20;
        }
        else
        {
            %content = %content @ "\n" @ %this.album;
            %heightOffset = %heightOffset + %heightDelta;
        }
        if (%this.comment $= "")
        {
            %heightOffset = %heightOffset + 20;
        }
        else
        {
            %content = %content @ "\n" @ %this.comment;
            %heightOffset = %heightOffset + %heightDelta;
        }
    }
    if ($UserPref::Audio::mute)
    {
        %this.ratingControl.setVisible(0);
        %content = "Audio is currently muted. Unmute audio to listen to music.";
    }
    else
    {
        if (!(%content $= ""))
        {
            %this.ratingControl.setVisible(1);
        }
        else
        {
            %this.ratingControl.setVisible(0);
            if (isObject(FMod))
            {
                if (FMod.isMusicOn())
                {
                    %content = "Loading music info...";
                }
                else
                {
                    %content = "You are currently in a space without music. To listen to music visit clubs, stores, apartments, or other venues that have music playing.";
                }
            }
            else
            {
                %content = "FMod music not currently available.";
            }
        }
    }
    %this.updateRatingText();
    MusicText.setText(%content);
    if (MusicText.isVisible() && MusicText.isAwake())
    {
        MusicText.forceReflow();
    }
    MusicHud.ratingControl.updatePosition();
    return ;
}
function MusicHud::updateRatingText(%this)
{
    %ratingText = %this.ratingControl.descripText;
    if (!(%ratingText $= ""))
    {
        %ratingText = %ratingText @ "<br>";
    }
    %isObject = isObject(RatingRequest);
    if (!%isObject && (findRequestStatus(RatingRequest) $= "fail"))
    {
        %ratingText = %ratingText @ "Couldn\'t get song rating.";
    }
    else
    {
        if (%isObject && !((RatingRequest.community_rating $= "")))
        {
            %ratingText = %ratingText @ "Avg. Rating: " @ RatingRequest.community_rating;
            if (!(RatingRequest.num_ratings $= ""))
            {
                %plural = !(RatingRequest.num_ratings $= 1) ? "s" : "";
                %ratingText = %ratingText @ " (" @ RatingRequest.num_ratings @ " vote" @ %plural @ ") ";
            }
        }
    }
    %this.ratingControl.label.setText(%ratingText);
    return ;
}
function MusicHud::setRating(%this, %rating)
{
    %this.ratingControl.setRating(%rating, 0);
    return ;
}
function MusicHud::parseComment(%this, %comment)
{
    %map = new StringMap();
    if (isObject(MissionCleanup))
    {
        MissionCleanup.add(%map);
    }
    %comment = NextToken(%comment, var, ":");
    if (!(%var $= "DOPP"))
    {
        return %map;
    }
    %url = NextToken(%comment, var, "|");
    %map.put("text", %var);
    if (getSubStr(%url, 0, 7) $= "http://")
    {
        %map.put("url", getSubStr(%url, 7, strlen(%url) - 7));
    }
    else
    {
        %map.put("url", %url);
    }
    return %map;
}
$Music::ITunesSearchURL = $Net::ItunesURL;
$Music::ITunesDownloadURL = "http://www.apple.com/itunes/affiliates/download";
$Music::SongPageSearchURL = $Net::SongPageURL;
function MusicHud::getITunesSearchLink(%this, %artist, %album, %title, %text)
{
    return "<a:" @ $Music::ITunesSearchURL @ "?artistTerm=" @ urlEncode(%artist) @ "&songTerm=" @ urlEncode(%title) @ "&albumTerm=" @ urlEncode(%album) @ ">" @ %text @ "</a>";
}
function MusicHud::getSongPageLink(%this, %artist, %album, %title, %text)
{
    return "<a:" @ $Music::SongPageSearchURL @ "?artistTerm=" @ urlEncode(%artist) @ "&songTerm=" @ urlEncode(%title) @ "&albumTerm=" @ urlEncode(%album) @ "&token=" @ urlEncode($Token) @ "&user=" @ urlEncode($Player::Name) @ ">" @ %text @ "</a>";
}
function MusicHud::getITunesDownloadURL(%this, %iTunesURL)
{
    return $Music::ITunesDownloadURL @ "?itmsUrl=" @ %iTunesURL;
}
function MusicHud::show(%this)
{
    if ($UserPref::HudTabs::AutoOpen["music"])
    {
        HudTabs.selectTabWithName("music");
    }
    return ;
}
function MusicHud::keepOpen(%this, %flag)
{
    $UserPref::Audio::keepMusicHudOpen = %flag;
    return ;
}
function MusicHud::hide(%this)
{
    if (%this.isShowing())
    {
        HudTabs.close();
    }
    return ;
}
function MusicHud::onClose(%this)
{
    %this.keepOpen(0);
    %this.update();
    return ;
}
function MusicHud::isShowing(%this)
{
    return HudTabs.getCurrentTab().name $= "music";
}
function MusicHud::setChangeStationAllowed(%this, %flag)
{
    MusicHudMyMediaButton.setVisible(%flag);
    %flag = 0;
    if (%flag)
    {
        MusicHudChangeStationButton.setVisible(1);
    }
    else
    {
        MusicHudChangeStationButton.setVisible(0);
        %this.setView("basic");
    }
    return ;
}
function MusicHud::setView(%this, %view)
{
    if (%view $= "basic")
    {
        MusicHudBasicView.setVisible(1);
        MusicHudEditView.setVisible(0);
    }
    else
    {
        if (%view $= "change_station")
        {
            MusicHudBasicView.setVisible(0);
            MusicHudEditView.setVisible(1);
            %this.fillStationPopup();
        }
    }
    return ;
}
function MusicHud::fillStationPopup(%this)
{
    MusicHudStationPopup.clear();
    MusicHudStationPopup.setActive(0);
    if (!(%this.station $= ""))
    {
        MusicHudStationPopup.setText(%this.station);
    }
    Music::createGetMusicStreamsRequest();
    return ;
}
function MusicHud::updateStations(%this, %stations)
{
    log("communication", "debug", "Called updateStations. Station count: " @ getFieldCount(%stations));
    %i = 0;
    while (%i < getFieldCount(%stations))
    {
        %field = getField(%stations, %i);
        if (%field $= "")
        {
            continue;
        }
        MusicHudStationPopup.add(%field);
        %i = %i + 1;
    }
    if (%this.station $= "")
    {
        MusicHudStationPopup.SetSelected(0);
    }
    MusicHudStationPopup.setActive(1);
    return ;
}
function MusicHud::stationSelected(%this)
{
    %this.station = MusicHudStationPopup.getValue();
    customSpace::SetMusicStreamID(%this.station);
    return ;
}
function MusicText::onUrl_NOOP(%this, %url)
{
    $MusicText::selectedURL = %url;
    if (iTunesIsRunning())
    {
        iTunesOpen(%url);
    }
    else
    {
        if (iTunesIsInstalled())
        {
            MessagePopup("Please Wait", "Starting ITunes...", 5000);
            iTunesOpen(%url);
        }
        else
        {
            MessageBoxYesNo("Confirm Installation", "You have selected a link to the ITunes Store " @ "but do not have ITunes installed.  " @ "Would you like to install it now?", "MusicText::installITunes();", "MusicText::declineITunes();");
        }
    }
    return ;
}
function MusicText::installITunes()
{
    gotoWebPage(getITunesDownloadURL($MusicText::selectedURL));
    return ;
}
function MusicText::declineITunes()
{
    return ;
}
function MusicText::startITunes()
{
    iTunesOpen($MusicText::selectedURL);
    return ;
}
function MusicRatingControl::onUpdate(%this)
{
    if ((%this.mouseOver + 1) == 1)
    {
        %this.descripText = "Hate It";
    }
    else
    {
        if ((%this.mouseOver + 1) == 2)
        {
            %this.descripText = "Not So Good";
        }
        else
        {
            if ((%this.mouseOver + 1) == 3)
            {
                %this.descripText = "So-So";
            }
            else
            {
                if ((%this.mouseOver + 1) == 4)
                {
                    %this.descripText = "Like It";
                }
                else
                {
                    if ((%this.mouseOver + 1) == 5)
                    {
                        %this.descripText = "Love It!";
                    }
                    else
                    {
                        %this.descripText = "";
                    }
                }
            }
        }
    }
    MusicHud.updateRatingText();
    return ;
}
