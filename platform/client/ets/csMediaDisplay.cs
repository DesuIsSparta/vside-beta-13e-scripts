$CSMediaDisplay::TypeEmpty = 0;
$CSMediaDisplay::TypeRadio = 1;
$CSMediaDisplay::TypeYoutube = 2;
$CSMediaDisplay::TypeShoutCast = 3;
$CSMediaDisplay::DefaultFavoriteCount = 15;
$CSMediaDisplay::TotalEntryCount = $CSMediaDisplay::DefaultFavoriteCount * 2;
$CSMediaMusicOffName = "- none -";
$CSMediaMusicOffID = "-";
$CSMediaDisplay::YoutubeDefaultThumb = "projects/vside/worlds/common/shapes/videoback_loading.jpg";
$CSMediaDisplay::YoutubeErrorThumb = "platform/client/ui/youtubeerror.png";
$CSMediaDisplay::YoutubeErrorTitle = "Video Not Found";
$CSMediaDisplay::YoutubeErrorInfo = "This video does not appear to exist";
$CSMediaDisplay::RadioBunnyThumb = "projects/vside/worlds/common/shapes/ets_video_bunny.png";
$CSMediaDisplay::EmptyBunnyThumb = "projects/vside/worlds/common/shapes/ets_video_bunny.png";
$CSMediaDisplay::GDataAPIURL = "http://gdata.youtube.com/feeds/api/";
$CSMediaDisplay::GDataAPIVideoInfo = "videos/";
$CSMediaDisplay::GDataAPIPlaylistInfo = "playlists/";
$CSMediaDisplay::YoutubeProfile = "http://www.youtube.com/profile?user=";
$CSMediaDisplay::ShoutCastThumb = "platform/client/ui/shoutcasterror.png";
$CSMediaDisplay::ShoutCastErrorTitle = "Stream Not Found";
$CSMediaDisplay::ShoutCastErrorInfo = "This stream does not appear to exist";
function CSMediaDisplay::toggle(%this)
{
    if (%this.isVisible())
    {
        %this.close();
    }
    else
    {
        %this.open();
    }
    return ;
}
function CSMediaDisplay::open(%this)
{
    closeCSPanelsInOtherCategories(%this);
    %this.setVisible(1);
    PlayGui.focusAndRaise(%this);
    WindowManager.update();
    CustomSpaceClient::checkEditingSpace();
    if (!(%this.periodic $= ""))
    {
        cancel(%this.periodic);
        %this.periodic = "";
    }
    %this.syncPlayingMediaStream(%this.playingStream);
    %this.periodicCycle();
    %this.update();
    return ;
}
function CSMediaDisplay::close(%this)
{
    %this.setVisible(0);
    CustomSpaceClient::checkEditingSpace();
    PlayGui.focusTopWindow();
    WindowManager.update();
    if (!(%this.periodic $= ""))
    {
        cancel(%this.periodic);
        %this.periodic = "";
    }
    return 1;
}
function CSMediaDisplay::periodicCycle(%this)
{
    cancel(%this.periodic);
    %this.cycleYoutubeThumbnails();
    %this.periodic = %this.schedule(4000, periodicCycle);
    return ;
}
function CSMediaDisplay::update(%this)
{
    return ;
}
function CSMediaDisplay::Initialize()
{
    if (!((CSMediaDisplay.initialized $= "")) && (CSMediaDisplay.initialized == 1))
    {
        return ;
    }
    %newExtent = getWord(CSMediaFavDisplayArray.childrenExtent, 0) SPC 51;
    CSMediaFavDisplayArray.childrenExtent = %newExtent;
    CSMediaHotDisplayArray.childrenExtent = %newExtent;
    CSMediaFavDisplayArray.setNumChildren($CSMediaDisplay::DefaultFavoriteCount);
    CSMediaHotDisplayArray.setNumChildren($CSMediaDisplay::DefaultFavoriteCount);
    CSMediaWhatsHotSelector.visible = 0;
    CSMediaDisplay.playingChild = 0;
    CSMediaDisplay.playingStream = "";
    CSMediaDisplay.showingWhatsHot = 0;
    CSMediaDisplay.initialized = 1;
    return ;
}
function CSMediaFavDisplayArray::onCreatedChild(%this, %child, %unused, %y)
{
    if (%y > 0)
    {
        %child.setProfile(ETSDroppableProfile);
    }
    else
    {
        %child.setProfile(GuiDefaultProfile);
    }
    %child.isReadOnly = 0;
    %child.systemDragDrop = 1;
    if (%this.getCount() == 1)
    {
        CSMediaDisplay.buildChildDisplayRadio(%child);
    }
    else
    {
        CSMediaDisplay.buildChildDisplayEmpty(%child);
    }
    %child.forceRadio = %this.getCount() == 1;
    %child.visible = 1;
    %child.oldMediaLink = "";
    if (!(getWord(%child.getNamespaceList(), 0) $= "CSMediaFavListItem"))
    {
        %child.bindClassName("CSMediaFavListItem");
    }
    return ;
}
function CSMediaHotDisplayArray::onCreatedChild(%this, %child)
{
    %child.isReadOnly = 1;
    CSMediaDisplay.buildChildDisplayEmpty(%child);
    %child.forceRadio = 0;
    %child.visible = 0;
    %child.oldMediaLink = "";
    if (!(getWord(%child.getNamespaceList(), 0) $= "CSMediaHotListItem"))
    {
        echo("Binding CSMediaHotListItem to " @ %child);
        %child.bindClassName("CSMediaHotListItem");
    }
    return ;
}
function CSMediaDisplay::buttonWhatsHot(%this)
{
    %widthDelta = getWord(CSMediaWhatsHotSelector.getExtent(), 0) + 3;
    %width = getWord(%this.getExtent(), 0);
    %height = getWord(%this.getExtent(), 1);
    if (CSMediaDisplay.showingWhatsHot)
    {
        CSMediaWhatsHotButton.text = " What\'s Hot >> ";
        CSMediaWhatsHotButton.reposition("352 24");
        %widthDelta = %widthDelta * -1;
    }
    else
    {
        csRequestHotMedia();
        CSMediaWhatsHotButton.text = " << Hide ";
        CSMediaWhatsHotButton.reposition("423 24");
    }
    %this.showingWhatsHot = !%this.showingWhatsHot;
    %this.setTrgExtent(%width + %widthDelta, %height);
    CSMediaWhatsHotSelector.visible = %this.showingWhatsHot;
    return ;
}
function CSMediaDisplay::onReachedTarget(%this)
{
    WindowManager.update();
    return ;
}
function CSMediaDisplay::setMediaFavorites(%this, %mediaList)
{
    %this.setMediaList(%mediaList, 0, CSMediaFavDisplayArray.getCount(), 0);
    return ;
}
function CSMediaDisplay::setMediaHotlist(%this, %mediaList)
{
    %this.setMediaList(%mediaList, $CSMediaDisplay::DefaultFavoriteCount, CSMediaHotDisplayArray.getCount(), 1);
    return ;
}
function CSMediaDisplay::setMediaList(%this, %mediaList, %startIdx, %maxIdx, %hideEmpty)
{
    %count = getFieldCount(%mediaList);
    %idx = 0;
    while (%idx < %count)
    {
        %linkInfo = getField(%mediaList, %idx);
        %linkName = getWord(%linkInfo, 0);
        %child = %this.getChildDisplay(%idx + %startIdx);
        %child.visible = 1;
        %infoCount = getWordCount(%linkInfo);
        %this.updateMediaLinkTo(%child, %linkName, %infoCount > 2);
        if (%infoCount > 1)
        {
            %this.setMediaInfo(%child, getWord(%linkInfo, 1), getWord(%linkInfo, 2));
        }
        %streamID = %this.extractMediaStreamId(%linkName);
        if (%this.playingStream $= %linkName)
        {
            %this.playingChild = %child;
            %child.playButton.visible = 0;
        }
        else
        {
            if (!(%streamID $= ""))
            {
                %urlinfo = new ScriptObject();
                if (isObject(MissionCleanup))
                {
                    MissionCleanup.add(%urlinfo);
                }
                %urlinfo.bindClassName("URLInfo");
                %urlinfo.url = %this.playingStream;
                %tStreamInfo = %streamID @ ".ogg";
                if (!%urlinfo.parse())
                {
                }
                else
                {
                    if (%urlinfo.Path $= %tStreamInfo)
                    {
                        %this.playingChild = %child;
                        %child.playButton.visible = 0;
                        %this.playingStream = %linkName;
                    }
                    %urlinfo.delete();
                }
            }
        }
        %idx = %idx + 1;
    }
    while (%idx < %maxIdx)
    {
        %child = %this.getChildDisplay(%idx + %startIdx);
        %this.updateMediaLinkTo(%child, "", 0);
        if (%hideEmpty)
        {
            %child.visible = 0;
        }
        %idx = %idx + 1;
    }
}

function CSMediaDisplay::setMediaStatistics(%this, %url, %views, %plays)
{
    %count = %this.getChildCount();
    %idx = 0;
    while (%idx < %count)
    {
        %child = %this.getChildDisplay(%idx);
        %medialink = %this.getMediaLink(%child);
        if (%medialink $= %url)
        {
            %this.setMediaInfo(%child, %views, %plays);
        }
        %idx = %idx + 1;
    }
}

function CSMediaDisplay::clearMediaStatistics(%this, %url)
{
    %count = %this.getChildCount();
    %idx = 0;
    while (%idx < %count)
    {
        %child = %this.getChildDisplay(%idx);
        %medialink = %this.getMediaLink(%child);
        if (%medialink $= %url)
        {
            %this.setNoMediaInfo(%child);
        }
        %idx = %idx + 1;
    }
}

function CSMediaDisplay::setMediaInfo(%this, %child, %views, %plays)
{
    if (!(%child.mediainfo $= ""))
    {
        %text = "<color:ffffff70>" @ %plays @ " plays";
        %child.mediainfo.setText(%text);
    }
    return ;
}
function CSMediaDisplay::setNoMediaInfo(%this, %child)
{
    if (!(%child.mediainfo $= ""))
    {
        %child.mediainfo.setText("(no stats)");
    }
    return ;
}
function CSMediaDisplay::getMediaFavorites(%this)
{
    %mediaList = "";
    %idx = 0;
    while (%idx < $CSMediaDisplay::DefaultFavoriteCount)
    {
        %child = %this.getChildDisplay(%idx);
        if (!(%child $= ""))
        {
            %medialink = %this.getMediaLink(%child);
            if (!(%medialink $= ""))
            {
                if (!(%mediaList $= ""))
                {
                    %mediaList = %mediaList TAB %medialink;
                }
                else
                {
                    %mediaList = %medialink;
                }
            }
        }
        %idx = %idx + 1;
    }
    return %mediaList;
}
function CSMediaDisplay::syncPlayingAudioStream(%this, %streamID)
{
    if (strstr(%streamID, "http://") < 0)
    {
        %this.syncPlayingMediaStream("vside://radio/" @ %streamID);
    }
    else
    {
        %this.syncPlayingMediaStream(%streamID);
    }
    return ;
}
function CSMediaDisplay::syncPlayingMediaStream(%this, %medialink)
{
    %child = %this.findChildWithMedialink(%medialink);
    if (%child > 0)
    {
        %this.setPlayingChild(%child);
    }
    else
    {
        %this.playingStream = %medialink;
    }
    return ;
}
function CSMediaDisplay::playMediaStream(%this, %newStreamUrl, %displayURL)
{
    %mediaType = %this.getMediaType(%newStreamUrl);
    %musicStream = "";
    %videoStream = "no-video";
    %streamType = "";
    if (%mediaType == $CSMediaDisplay::TypeRadio)
    {
        %ratableURL = %musicStream = %this.extractMediaStreamId(%newStreamUrl);
        customSpace::SetMusicStreamID(%musicStream);
        customSpace::SetVideoURL("");
        %streamType = "RADIO";
    }
    else
    {
        if (%mediaType == $CSMediaDisplay::TypeYoutube)
        {
            %ratableURL = %videoStream = %newStreamUrl;
            customSpace::SetMusicStreamID("");
            customSpace::SetVideoURL(%videoStream);
            if (strstr(%newStreamUrl, "v="))
            {
                %streamType = "VIDEO";
            }
            else
            {
                if (strstr(%newStreamUrl, "p="))
                {
                    %streamType = "VIDEO_PLAYLIST";
                }
            }
        }
        else
        {
            if (%mediaType == $CSMediaDisplay::TypeShoutCast)
            {
                %musicStream = %newStreamUrl;
                %ratableURL = %displayURL;
                customSpace::SetMusicStreamID(%musicStream, %displayURL);
                customSpace::SetVideoURL("");
                %streamType = "VIDEO";
            }
            else
            {
                customSpace::SetMusicStreamID("");
                customSpace::SetVideoURL("");
            }
        }
    }
    if (!(%streamType $= ""))
    {
        csRecordMediaShow(%ratableURL, %streamType);
    }
    CustomSpaceSettings::saveSettings(CustomSpaceClient::GetSpaceImIn(), "", "", "", %musicStream, %videoStream);
    return ;
}
function CSMediaDisplay::extractMediaStreamId(%this, %medialink)
{
    %url = new ScriptObject();
    if (isObject(MissionCleanup))
    {
        MissionCleanup.add(%url);
    }
    %url.bindClassName("URLInfo");
    %url.url = %medialink;
    %streamID = "";
    if (%url.parse())
    {
        if ((stricmp(%url.protocol, "vside") == 0) && (stricmp(%url.host, "radio") == 0))
        {
            %streamID = %url.Path;
        }
    }
    %url.delete();
    return %streamID;
}
function CSMediaDisplay::PlayButtonPushed(%this, %child)
{
    %this.setPlayingChild(%child);
    if (!(%child.streamUrl $= ""))
    {
        %this.playMediaStream(%child.streamUrl, %this.playingStream);
    }
    else
    {
        %this.playMediaStream(%this.playingStream);
    }
    return ;
}
function CSMediaDisplay::setPlayingChild(%this, %child)
{
    %count = %this.getChildCount();
    %idx = 0;
    while (%idx < %count)
    {
        %otherChild = %this.getChildDisplay(%idx);
        if (%child != %otherChild)
        {
            %otherChild.isPlaying = 0;
            if (!((%otherChild.highlight $= "")) && isObject(%otherChild.highlight))
            {
                %otherChild.remove(%otherChild.highlight);
                %otherChild.highlight.delete();
                %otherChild.highlight = "";
            }
            %this.setPlaybuttonVisible(%otherChild, 1);
        }
        %idx = %idx + 1;
    }
    %medialink = "";
    if (%child != 0)
    {
        %child.isPlaying = 1;
        if (%child.highlight $= "")
        {
            %extent = %child.getExtent();
            %child.highlight = new GuiConvBubbleCtrl()
            {
                profile = "ETSLightHighlightProfile";
                extent = getWord(%extent, 0) SPC getWord(%extent, 1) - 4;
                position = "0 0";
                roundRadius = 4;
                roundInterps = 2;
                sluggishness = -1;
            };
            %child.add(%child.highlight);
        }
        %this.setPlaybuttonVisible(%child, 0);
        %medialink = %this.getMediaLink(%child);
    }
    %this.playingStream = %medialink;
    %this.playingChild = %child;
    return ;
}
function CSMediaDisplay::findChildWithMedialink(%this, %medialink)
{
    %count = %this.getChildCount();
    %idx = 0;
    while (%idx < %count)
    {
        %child = %this.getChildDisplay(%idx);
        %testlink = %this.getMediaLink(%child);
        if (%testlink $= %medialink)
        {
            return %child;
        }
        else
        {
            if (%child.streamUrl $= %medialink)
            {
                return %child;
            }
        }
        %idx = %idx + 1;
    }
    return 0;
}
function CSMediaDisplay::stopAllMedia(%this)
{
    %this.PlayButtonPushed(0);
    return ;
}
function CSMediaDisplay::showHelp(%this)
{
    %msg = $MsgCat::custSpace["MEDIA_HELP"];
    %dlg = MessageBoxOK("My Music & Videos - How To", %msg, "");
    %dlg.window.resize(550, 300);
    return ;
}
function CSMediaDisplay::getMediaLink(%this, %child)
{
    if (%child.medialink $= "")
    {
        return "";
    }
    if (%child.displayType != $CSMediaDisplay::TypeRadio)
    {
        %medialinkValue = %child.medialink.getText();
    }
    else
    {
        %medialinkName = %child.medialink.getText();
        %idx = -1;
        if (isObject($musicStreamNameMap))
        {
            %idx = $musicStreamNameMap.findKey(%medialinkName);
        }
        if (%idx == -1)
        {
            %streamID = %medialinkName;
        }
        else
        {
            %streamID = $musicStreamNameMap.getValue(%idx);
        }
        %medialinkValue = "vside://radio/" @ %streamID;
    }
    return %medialinkValue;
}
function CSMediaDisplay::setMediaLink(%this, %child, %medialink, %skipStatistics)
{
    if (%child.medialink $= "")
    {
        return ;
    }
    if (%child.displayType == $CSMediaDisplay::TypeYoutube)
    {
        %validate = %child.medialink.validate;
        %child.medialink.validate = "";
        %child.medialink.setText(%medialink);
        %child.medialink.validate = %validate;
        %child.skipStatistics = %skipStatistics;
        %this.requestYoutubeInfo(%child);
        %this.setPlaybuttonAvailable(%child, 1);
        %child.oldMediaLink = %medialink;
    }
    else
    {
        if (%child.displayType == $CSMediaDisplay::TypeShoutCast)
        {
            %validate = %child.medialink.validate;
            %child.medialink.validate = "";
            %child.medialink.setText(%medialink);
            %child.medialink.validate = %validate;
            %child.skipStatistics = %skipStatistics;
            %this.requestShoutCastInfo(%child);
            %this.setPlaybuttonAvailable(%child, 1);
            %child.oldMediaLink = %medialink;
        }
        else
        {
            %url = new ScriptObject();
            if (isObject(MissionCleanup))
            {
                MissionCleanup.add(%url);
            }
            %url.bindClassName("URLInfo");
            %url.url = %medialink;
            %url.parse();
            %path = %url.Path;
            %url.delete();
            %count = 0;
            if (isObject($musicStreamNameMap))
            {
                %count = $musicStreamNameMap.size();
            }
            %idx = 0;
            while (%idx < %count)
            {
                if (stricmp($musicStreamNameMap.getValue(%idx), %path) == 0)
                {
                    continue;
                }
                %idx = %idx + 1;
            }
            if (%idx < %count)
            {
                %newStreamName = $musicStreamNameMap.getKey(%idx);
                %index = %child.medialink.findText(%newStreamName);
                %child.medialink.SetSelected(%index);
            }
            else
            {
                %child.medialink.setText(%path);
            }
            %this.setPlaybuttonAvailable(%child, !(%path $= "-"));
        }
    }
    return ;
}
function CSMediaDisplay::setPlaybuttonAvailable(%this, %child, %avail)
{
    %child.playbuttonAvailable = %avail;
    if (!(%child.playButton $= ""))
    {
        if (!%avail)
        {
            %child.playButton.visible = 0;
        }
        else
        {
            %child.playButton.visible = !%child.isPlaying;
        }
    }
    return ;
}
function CSMediaDisplay::setPlaybuttonVisible(%this, %child, %visible)
{
    if (%child.playbuttonAvailable && !((%child.playButton $= "")))
    {
        %child.playButton.visible = %visible;
    }
    return ;
}
function CSMediaDisplay::changeMediaLink(%this, %child)
{
    %this.schedule(0, "changeMediaLinkReally", %child);
    return ;
}
function CSMediaDisplay::changeMediaLinkReally(%this, %child)
{
    %newMediaType = %this.getMediaType(%child.medialink);
    %newMediaLink = %this.getMediaLink(%child);
    if (%child.oldMediaLink $= %newMediaLink)
    {
        return ;
    }
    %this.updateMediaLinkTo(%child, %newMediaLink, 0);
    %newMediaType = %this.getMediaType(%newMediaLink);
    if (%this.playingChild == %child)
    {
        if (%newMediaType == $CSMediaDisplay::TypeRadio)
        {
            if (%newMediaLink $= "vside://radio/-")
            {
                %newMediaLink = "";
                %this.playingChild = 0;
                %child.isPlaying = 0;
            }
        }
        if (%newMediaType == $CSMediaDisplay::TypeShoutCast)
        {
            %child.autoplay = 1;
        }
        else
        {
            %this.playMediaStream(%newMediaLink);
            %this.playingStream = %newMediaLink;
        }
    }
    if ((%newMediaType == $CSMediaDisplay::TypeRadio) && (%newMediaLink $= ""))
    {
        csSaveMediaFavorites();
    }
    return ;
}
function CSMediaDisplay::updateMediaLinkTo(%this, %child, %newMediaLink, %skipStats)
{
    %child.streamUrl = "";
    %newMediaType = %this.getMediaType(%newMediaLink);
    if (%newMediaType == $CSMediaDisplay::TypeRadio)
    {
        %url = new ScriptObject();
        if (isObject(MissionCleanup))
        {
            MissionCleanup.add(%url);
        }
        %url.bindClassName("URLInfo");
        %url.url = %newMediaLink;
        %url.parse();
        %path = %url.Path;
        %url.delete();
        if ((%path $= "-") && !(%child.forceRadio))
        {
            %newMediaType = $CSMediaDisplay::TypeEmpty;
            %newMediaLink = "";
        }
    }
    else
    {
        if (%newMediaType == $CSMediaDisplay::TypeYoutube)
        {
            %normalizedURL = CSMediaDisplay::normalizeYoutubeURL(%newMediaLink);
            if (%normalizedURL $= "")
            {
                %newMediaType = $CSMediaDisplay::TypeEmpty;
            }
            else
            {
                %newMediaLink = %normalizedURL;
            }
        }
        else
        {
            if (%newMediaType == $CSMediaDisplay::TypeShoutCast)
            {
            }
        }
    }
    if (%newMediaType != %child.displayType)
    {
        %this.clearChildDisplay(%child);
        if (%newMediaType == $CSMediaDisplay::TypeRadio)
        {
            %this.buildChildDisplayRadio(%child);
        }
        else
        {
            if (%newMediaType == $CSMediaDisplay::TypeYoutube)
            {
                %this.buildChildDisplayYouTube(%child);
            }
            else
            {
                if (%newMediaType == $CSMediaDisplay::TypeShoutCast)
                {
                    %this.buildChildDisplayShoutCast(%child);
                }
                else
                {
                    %this.buildChildDisplayEmpty(%child);
                }
            }
        }
    }
    %this.setMediaLink(%child, %newMediaLink, %skipStats);
    return ;
}
function CSMediaDisplay::getMediaType(%this, %medialink)
{
    %url = new ScriptObject();
    if (isObject(MissionCleanup))
    {
        MissionCleanup.add(%url);
    }
    %url.bindClassName("URLInfo");
    %url.url = %medialink;
    if (!%url.parse())
    {
        %url.delete();
        return $CSMediaDisplay::TypeEmpty;
    }
    %type = $CSMediaDisplay::TypeEmpty;
    if (stricmp(%url.protocol, "vside") == 0)
    {
        if (stricmp(%url.host, "radio") == 0)
        {
            %type = $CSMediaDisplay::TypeRadio;
        }
    }
    else
    {
        if (stricmp(%url.protocol, "http") == 0)
        {
            if (strstr(%url.host, "youtube.") >= 0)
            {
                %type = $CSMediaDisplay::TypeYoutube;
            }
            else
            {
                %type = $CSMediaDisplay::TypeShoutCast;
            }
        }
        else
        {
            debug("Unknown media type: " @ %medialink);
        }
    }
    %url.delete();
    return %type;
}
function CSMediaDisplay::updateRadioStreams(%this)
{
    %count = %this.getChildCount();
    %idx = 0;
    while (%idx < %count)
    {
        %child = %this.getChildDisplay(%idx);
        if (%child.displayType == $CSMediaDisplay::TypeRadio)
        {
            %this.updateRadioDropDown(%child);
        }
        %idx = %idx + 1;
    }
}

function CSMediaDisplay::updateRadioDropDown(%this, %child)
{
    %medialink = %this.getMediaLink(%child);
    %dropdown = %child.medialink;
    %dropdown.clear();
    %count = $musicStreamNameMap.size();
    %idx = 0;
    while (%idx < %count)
    {
        %dropdown.add($musicStreamNameMap.getKey(%idx));
        %idx = %idx + 1;
    }
    %this.setMediaLink(%child, %medialink, 0);
    return ;
}
function CSMediaDisplay::cycleYoutubeThumbnails(%this)
{
    %idx = 0;
    while (%idx < $CSMediaDisplay::TotalEntryCount)
    {
        %child = %this.getChildDisplay(%idx);
        if (%child.displayType == $CSMediaDisplay::TypeYoutube)
        {
            %this.selectYoutubeThumbnails(%child, 0);
        }
        %idx = %idx + 1;
    }
}

function CSMediaDisplay::buildYoutubeTitle(%this, %child)
{
    %AuthorName = "";
    %title = "YouTube Video";
    if (!(%child.AuthorName $= ""))
    {
        %AuthorName = %child.AuthorName;
    }
    if (!(%child.title $= ""))
    {
        %title = %child.title;
    }
    %FullTitle = "";
    if (!(%AuthorName $= ""))
    {
        %authorString = "<just:right><color:ffffff80><linkcolorhl:ffaaff><a:gamelink " @ $CSMediaDisplay::YoutubeProfile @ %AuthorName @ ">";
        %authorString = %authorString @ %AuthorName @ "</a>";
        if (!(%child.mediaauthor $= ""))
        {
            %child.mediaauthor.setText(%authorString);
        }
    }
    %FullTitle = "<spush><b>" @ %title @ "<spop>" @ %FullTitle;
    %FullTitle = "<clip:" @ getWord(%child.mediatitle.extent, 0) @ ">" @ %FullTitle @ "</clip>";
    %child.mediatitle.setText(%FullTitle);
    return ;
}
function CSMediaDisplay::buildShoutCastTitle(%this, %child)
{
    %AuthorName = "";
    %title = "ShoutCast Stream";
    %homeURL = "";
    if (!(%child.AuthorName $= ""))
    {
        %AuthorName = %child.AuthorName;
    }
    if (!(%child.title $= ""))
    {
        %title = %child.title;
    }
    if (!(%child.homeURL $= ""))
    {
        %homeURL = %child.homeURL;
    }
    if (!(%homeURL $= ""))
    {
        %FullTitle = "<a:gamelink " @ %homeURL @ "/><clip:" @ getWord(%child.mediatitle.extent, 0) @ ">" @ %title @ "</clip></a>";
    }
    else
    {
        %FullTitle = "<clip:" @ getWord(%child.mediatitle.extent, 0) @ ">" @ %title @ "</clip>";
    }
    %child.mediatitle.setText(%FullTitle);
    return ;
}
function CSMediaDisplay::selectYoutubeThumbnails(%this, %child, %force)
{
    if (!%force)
    {
        %chance = getRandom(0, 99);
        if (%chance > %child.changeThumbChance)
        {
            %child.changeThumbChance = %child.changeThumbChance + %child.changeCume;
            return ;
        }
    }
    %child.changeThumbChance = %child.changeCume;
    %index = getRandom(0, %child.thumbCount - 1);
    %child.currentThumb = %index;
    if (!(%child.thumbURL[%child.currentThumb] $= ""))
    {
        %child.thumbnail.downloadAndApplyBitmap(%child.thumbURL[%child.currentThumb], "youtube");
    }
    return ;
}
function CSMediaDisplay::normalizeYoutubeURL(%medialink)
{
    %url = new ScriptObject();
    if (isObject(MissionCleanup))
    {
        MissionCleanup.add(%url);
    }
    %url.bindClassName("URLInfo");
    %url.url = %medialink;
    %url.parse();
    %urlOut = "http://" @ %url.host @ "/";
    if (!(%url.param["v"] $= ""))
    {
        %urlOut = %urlOut @ "watch?v=" @ %url.param["v"];
    }
    else
    {
        if (!(%url.param["p"] $= ""))
        {
            %urlOut = %urlOut @ "view_play_list?p=" @ %url.param["p"];
        }
        else
        {
            %url.delete();
            return "";
        }
    }
    %url.delete();
    return %urlOut;
}
function CSMediaDisplay::requestYoutubeInfo(%this, %child)
{
    if (!(%child.gdataRequest $= ""))
    {
        %child.gdataRequest.delete();
    }
    %child.title = "";
    %child.AuthorName = "";
    if (!(%child.thumbCount $= ""))
    {
        %idx = 0;
        while (%idx < %child.thumbCount)
        {
            %child.thumbURL[%idx] = "";
            %idx = %idx + 1;
        }
    }
    %child.thumbCount = "";
    %url = new ScriptObject();
    if (isObject(MissionCleanup))
    {
        MissionCleanup.add(%url);
    }
    %url.bindClassName("URLInfo");
    %url.url = %this.getMediaLink(%child);
    %url.parse();
    %gdataRequest = $CSMediaDisplay::GDataAPIURL;
    if (!(%url.param["v"] $= ""))
    {
        %gdataRequest = %gdataRequest @ $CSMediaDisplay::GDataAPIVideoInfo @ %url.param["v"];
    }
    else
    {
        if (!(%url.param["p"] $= ""))
        {
            %gdataRequest = %gdataRequest @ $CSMediaDisplay::GDataAPIPlaylistInfo @ %url.param["p"];
        }
    }
    else
    {
        return ;
    }
    %child.gdataRequest = new XMLDoc();
    if (isObject(MissionCleanup))
    {
        MissionCleanup.add(%child.gdataRequest);
    }
    %child.gdataRequest.bindClassName("CSMDGDataRequest");
    %child.gdataRequest.control = %child;
    %child.gdataRequest.Display = %this;
    %child.gdataRequest.parseXMLFromURL(%gdataRequest);
    return ;
}
function CSMediaDisplay::requestShoutCastInfo(%this, %child)
{
    if (!(%child.scRequest $= ""))
    {
        %child.scRequest.delete();
    }
    %child.title = "";
    %child.AuthorName = "";
    %child.thumbCount = "";
    %url = %this.getMediaLink(%child);
    if (strstr(%url, ".mp3") > 0)
    {
        %child.scRequest = new M3UDemuxer();
    }
    else
    {
        if (strstr(%url, ".pls") > 0)
        {
            %child.scRequest = new PLSDemuxer();
            %child.URLtypeUnknown = 1;
        }
        else
        {
            if (strstr(%url, ".m3u") > 0)
            {
                %child.scRequest = new M3UDemuxer();
            }
            else
            {
                %child.scRequest = new PLSDemuxer();
                %child.URLtypeUnknown = 1;
            }
        }
    }
    if (isObject(MissionCleanup))
    {
        MissionCleanup.add(%child.scRequest);
    }
    %child.scRequest.bindClassName("CSSCDataRequest");
    %child.scRequest.control = %child;
    %child.scRequest.Display = %this;
    %child.scRequest.setURL(%this.getMediaLink(%child));
    %child.scRequest.start();
    return ;
}
function CSSCDataRequest::onDone(%this, %url)
{
    %child = %this.control;
    %window = %this.Display;
    %child.scRequest = "";
    %child.streamUrl = %url;
    %child.title = %this.getTitle();
    %child.homeURL = %this.getHomeURL();
    %window.buildShoutCastTitle(%child);
    %child.mediainfo.setText("");
    %child.thumbnail.setBitmap($CSMediaDisplay::ShoutCastThumb);
    schedule(%this, "delete", 0);
    %medialink = %window.getMediaLink(%child);
    if (!%child.skipStatistics)
    {
        csRequestMediaStatistics(%medialink);
    }
    if (%child.autoplay == 1)
    {
        %child.autoplay = 0;
        %child.isPlaying = 1;
        if (%child.isPlaying)
        {
            CSMediaDisplay.stopAllMedia();
        }
        CSMediaDisplay.PlayButtonPushed(%child);
    }
    csSaveMediaFavorites();
    return ;
}
function CSSCDataRequest::onError(%this)
{
    %child = %this.control;
    %window = %this.Display;
    if (%child.URLtypeUnknown == 1)
    {
        %child.scRequest = new M3UDemuxer();
        if (isObject(MissionCleanup))
        {
            MissionCleanup.add(%child.scRequest);
        }
        %child.scRequest.bindClassName("CSSCDataRequest");
        %child.scRequest.control = %child;
        %child.scRequest.Display = %window;
        %child.URLtypeUnknown = 0;
        %child.scRequest.setURL(%window.getMediaLink(%child));
        %child.scRequest.start();
        schedule(%this, "delete", 0);
        return ;
    }
    %medialink = %window.getMediaLink(%child);
    %child.scRequest = "";
    %child.thumbnail.setBitmap($CSMediaDisplay::ShoutCastThumb);
    %child.mediatitle.setText($CSMediaDisplay::ShoutCastErrorTitle);
    %lastError = %this.getErrorBuffer();
    if (%lastError $= "")
    {
        %lastError = $CSMediaDisplay::ShoutCastErrorInfo;
    }
    %child.mediainfo.setText(%lastError);
    echo("Invalid Media URL: " @ %window.getMediaLink(%child) @ " Error: " @ %lastError);
    %window.setPlaybuttonAvailable(%child, 0);
    schedule(%this, "delete", 0);
    return ;
}
function CSMDGDataRequest::onDone(%this)
{
    %child = %this.control;
    %window = %this.Display;
    %child.gdataRequest = "";
    %root = %this.getRootElement();
    schedule(%this, "delete", 0);
    if (!%root)
    {
        log("error", "No Root element in returned XML...");
        return ;
    }
    %child.thumbCount = 0;
    if (%root.getValue() $= "feed")
    {
        %window.parseFeedNode(%child, %root);
        %child.changeCume = 5;
    }
    else
    {
        if (%root.getValue() $= "entry")
        {
            %window.parseEntryNode(%child, %root, 1);
            %child.changeCume = 2;
        }
        else
        {
            log("error", "media", "Root node is not an entry or feed tag");
            return ;
        }
    }
    %window.buildYoutubeTitle(%child);
    %window.selectYoutubeThumbnails(%child, 1);
    %medialink = %window.getMediaLink(%child);
    if (!%child.skipStatistics)
    {
        csRequestMediaStatistics(%medialink);
    }
    if (%child.autoplay == 1)
    {
        %child.autoplay = 0;
        %child.isPlaying = 1;
        if (%child.isPlaying)
        {
            CSMediaDisplay.stopAllMedia();
        }
        CSMediaDisplay.PlayButtonPushed(%child);
    }
    csSaveMediaFavorites();
    return ;
}
function CSMediaDisplay::parseEntryNode(%this, %child, %entryNode, %setTitle)
{
    %MediaGroup = %entryNode.getFirstChild("media:group");
    if (%setTitle)
    {
        %AuthorNode = %entryNode.getFirstChild("author");
        %AuthorNameNode = %AuthorNode.getFirstChild("name");
        %child.AuthorName = %AuthorNameNode.getText();
        %TitleNode = %MediaGroup.getFirstChild("media:title");
        %child.title = %TitleNode.getText();
    }
    %ThumbnailIdx = %child.thumbCount;
    %ThumbnailNode = %MediaGroup.getFirstChild("media:thumbnail");
    while (%ThumbnailNode)
    {
        %child.thumbURL[%ThumbnailIdx] = %ThumbnailNode.getAttribute("url");
        %ThumbnailIdx = %ThumbnailIdx + 1;
        %ThumbnailNode = %ThumbnailNode.getNext("media:thumbnail");
    }
    %child.thumbCount = %ThumbnailIdx;
    return ;
}
function CSMediaDisplay::parseFeedNode(%this, %child, %feedNode)
{
    %AuthorNode = %feedNode.getFirstChild("author");
    %AuthorNode = %AuthorNode.getFirstChild("name");
    %child.AuthorName = %AuthorNode.getText();
    %MediaGroup = %feedNode.getFirstChild("media:group");
    %TitleNode = %MediaGroup.getFirstChild("media:title");
    %child.title = %TitleNode.getText();
    %entry = %feedNode.getFirstChild("entry");
    while (%entry)
    {
        %this.parseEntryNode(%child, %entry, 0);
        %entry = %entry.getNext("entry");
    }
}

function CSMDGDataRequest::onError(%this)
{
    %child = %this.control;
    %window = %this.Display;
    %child.gdataRequest = "";
    %child.thumbnail.setBitmap($CSMediaDisplay::YoutubeErrorThumb);
    %child.mediatitle.setText($CSMediaDisplay::YoutubeErrorTitle);
    %child.mediainfo.setText($CSMediaDisplay::YoutubeErrorInfo);
    %window.setPlaybuttonAvailable(%child, 0);
    schedule(%this, "delete", 0);
    return ;
}
function CSMediaDisplay::getChildDisplay(%this, %childIdx)
{
    %faveCount = CSMediaFavDisplayArray.getCount();
    if (%faveCount > %childIdx)
    {
        return CSMediaFavDisplayArray.getObject(%childIdx);
    }
    else
    {
        if ((%childIdx - %faveCount) < CSMediaFavDisplayArray.getCount())
        {
            return CSMediaHotDisplayArray.getObject(%childIdx - %faveCount);
        }
    }
    return "";
}
function CSMediaDisplay::getChildCount(%this)
{
    return CSMediaFavDisplayArray.getCount() + CSMediaHotDisplayArray.getCount();
}
function CSMediaDisplay::clearChildDisplay(%this, %child)
{
    %child.deleteMembers();
    %child.playButton = "";
    %child.playbuttonAvailable = 0;
    %child.thumbnail = "";
    %child.medialink = "";
    %child.mediatitle = "";
    %child.mediainfo = "";
    %child.mediaauthor = "";
    %child.title = "";
    %child.AuthorName = "";
    if (!(%child.thumbCount $= ""))
    {
        %idx = 0;
        while (%idx < %child.thumbCount)
        {
            %child.thumbURL[%idx] = "";
            %idx = %idx + 1;
        }
    }
    %child.thumbCount = "";
    return ;
}
function CSMediaDisplay::buildChildDisplayYouTube(%this, %child)
{
    %padding = 1;
    %windowWidth = getWord(%child.getExtent(), 0);
    %authorWidth = 80;
    %xPos = 1;
    %ypos = 0;
    %bitmap = new GuiBitmapCtrl()
    {
        profile = "ETSNonModalProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = %xPos SPC %ypos + 1;
        extent = "60 45";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
    };
    %bmpWidth = getWord(%bitmap.getExtent(), 0);
    %bmpHeight = getWord(%bitmap.getExtent(), 1);
    %bitmap.setBitmap($CSMediaDisplay::YoutubeDefaultThumb);
    %xPos = %xPos + (%bmpWidth + %padding);
    %windowWidth = %windowWidth - %bmpWidth;
    %textTitle = new GuiMLTextCtrl()
    {
        profile = "GuiMessageTextProfile";
        horizSizing = "width";
        vertSizing = "bottom";
        position = %xPos SPC %ypos;
        extent = %windowWidth - %padding SPC 14;
        minExtent = "8 8";
        sluggishness = -1;
        visible = 1;
        lineSpacing = 2;
        allowColorChars = 0;
        maxChars = -1;
        helpTag = 0;
    };
    %textTitle.bindClassName("CSMediaMLText");
    %textTitle.setText("YouTube Video");
    %ypos = %ypos + (13 + %padding);
    %textEntry = new GuiTextEditCtrl()
    {
        profile = %child.isReadOnly ? "ETSDarkReadonlyTextEditProfile" : "ETSDarkTextEditProfile";
        horizSizing = "center";
        vertSizing = "top";
        position = %xPos SPC %ypos;
        extent = (%windowWidth - %padding) - 1 SPC 18;
        minExtent = "8 8";
        visible = 1;
        setFirstResponder = 0;
        altCommand = %this @ ".changeMediaLink(" @ %child @ ");";
        validate = %this @ ".changeMediaLink(" @ %child @ ");";
        helpTag = 0;
        historySize = 0;
        readOnly = %child.isReadOnly;
    };
    %ypos = %ypos + (16 + %padding);
    %textInfo = new GuiMLTextCtrl()
    {
        profile = "GuiMessageTextProfile";
        horizSizing = "width";
        vertSizing = "bottom";
        position = %xPos SPC %ypos;
        extent = %windowWidth - ((%padding * 2) + %authorWidth) SPC 14;
        minExtent = "8 8";
        sluggishness = -1;
        visible = 1;
        lineSpacing = 2;
        allowColorChars = 0;
        maxChars = -1;
        helpTag = 0;
    };
    %textInfo.bindClassName("CSMediaMLText");
    %textInfo.setText("Retrieving video info...");
    %xPos = %xPos + (%padding + getWord(%textInfo.extent, 0));
    %textAuthor = new GuiMLTextCtrl()
    {
        profile = "GuiMessageTextProfile";
        horizSizing = "width";
        vertSizing = "bottom";
        position = %xPos SPC %ypos;
        extent = %authorWidth SPC 14;
        minExtent = "8 8";
        sluggishness = -1;
        visible = 1;
        lineSpacing = 2;
        allowColorChars = 0;
        maxChars = -1;
        helpTag = 0;
    };
    %textAuthor.bindClassName("CSMediaMLText");
    %ypos = %ypos + (14 + %padding);
    %playButton = new GuiBitmapButtonCtrl()
    {
        profile = "GuiDefaultProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "2 2";
        extent = "60 45";
        minExtent = "22 22";
        sluggishness = -1;
        visible = 1;
        command = %this @ ".PlayButtonPushed(" @ %child @ ");";
        text = "";
        groupNum = -1;
        buttonType = "PushButton";
        bitmap = "platform/client/buttons/playMedia";
        modulationColor = "255 255 255 180";
    };
    %playButton.bindClassName("CSBitmapButton");
    %bitmap.add(%playButton);
    %child.add(%bitmap);
    %child.add(%textTitle);
    %child.add(%textEntry);
    %child.add(%textInfo);
    %child.add(%textAuthor);
    %child.playButton = %playButton;
    %child.playbuttonAvailable = 1;
    %child.thumbnail = %bitmap;
    %child.medialink = %textEntry;
    %child.mediatitle = %textTitle;
    %child.mediainfo = %textInfo;
    %child.mediaauthor = %textAuthor;
    %child.displayType = $CSMediaDisplay::TypeYoutube;
    return ;
}
function CSMediaDisplay::buildChildDisplayRadio(%this, %child, %url)
{
    %padding = 2;
    %windowWidth = getWord(%child.getExtent(), 0);
    %xPos = 1;
    %ypos = 0;
    %bitmap = new GuiBitmapCtrl()
    {
        profile = "ETSNonModalProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = %xPos SPC %ypos;
        extent = "45 45";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
    };
    %bmpWidth = 60;
    %bmpHeight = getWord(%bitmap.getExtent(), 1);
    %bitmap.setBitmap($CSMediaDisplay::RadioBunnyThumb);
    %xPos = %xPos + (%bmpWidth + %padding);
    %windowWidth = %windowWidth - %bmpWidth;
    %textTitle = new GuiMLTextCtrl()
    {
        profile = "GuiMessageTextProfile";
        horizSizing = "width";
        vertSizing = "bottom";
        position = %xPos SPC %ypos;
        extent = %windowWidth - %padding SPC 14;
        minExtent = "8 8";
        sluggishness = -1;
        visible = 1;
        lineSpacing = 2;
        allowColorChars = 0;
        maxChars = -1;
        helpTag = 0;
    };
    %textTitle.bindClassName("CSMediaMLText");
    %textTitle.setText("vSide Radio");
    %ypos = %ypos + (14 + %padding);
    %dropdown = new GuiPopUp2MenuCtrl(CSMediaMusicStreamPopup)
    {
        profile = "InfoWindowPopupProfile";
        scrollProfile = "DottedScrollProfile";
        winProfile = "InfoWindowPopupWindowProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = %xPos SPC %ypos;
        extent = (%windowWidth - %padding) - 2 SPC 30;
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        command = %this @ ".changeMediaLink(" @ %child @ ");";
        text = "";
        maxLength = 255;
        maxPopupHeight = 200;
        allowReverse = 0;
    };
    %playButton = new GuiBitmapButtonCtrl()
    {
        profile = "GuiDefaultProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "2 2";
        extent = "60 45";
        minExtent = "22 22";
        sluggishness = -1;
        visible = 1;
        command = %this @ ".PlayButtonPushed(" @ %child @ ");";
        text = "";
        groupNum = -1;
        buttonType = "PushButton";
        bitmap = "platform/client/buttons/playMedia";
        modulationColor = "255 255 255 180";
    };
    %playButton.bindClassName("CSBitmapButton");
    %bitmap.add(%playButton);
    %child.add(%bitmap);
    %child.add(%textTitle);
    %child.add(%dropdown);
    if (isObject($musicStreamNameMap))
    {
        %count = $musicStreamNameMap.size();
    }
    else
    {
        %count = 0;
    }
    %idx = 0;
    while (%idx < %count)
    {
        %dropdown.add($musicStreamNameMap.getKey(%idx));
        %idx = %idx + 1;
    }
    if (%child.isReadOnly)
    {
        %blocker = new GuiMLTextCtrl()
        {
            profile = "GuiMessageTextProfile";
            horizSizing = "width";
            vertSizing = "height";
            position = %dropdown.position;
            extent = %dropdown.extent;
            minExtent = %dropdown.extent;
            visible = 1;
            setFirstResponder = 0;
            altCommand = %this @ ".changeMediaLink(" @ %child @ ");";
            modal = 0;
            helpTag = 0;
            historySize = 0;
        };
        %blocker.bindClassName("CSMediaMLText");
        %child.add(%blocker);
    }
    %child.playButton = %playButton;
    %child.playbuttonAvailable = 1;
    %child.thumbnail = %bitmap;
    %child.medialink = %dropdown;
    %child.mediatitle = %textTitle;
    %child.mediainfo = "";
    %child.displayType = $CSMediaDisplay::TypeRadio;
    return ;
}
function CSMediaDisplay::buildChildDisplayShoutCast(%this, %child)
{
    %padding = 1;
    %windowWidth = getWord(%child.getExtent(), 0);
    %authorWidth = 80;
    %xPos = 1;
    %ypos = 0;
    %bitmap = new GuiBitmapCtrl()
    {
        profile = "ETSNonModalProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = %xPos SPC %ypos + 1;
        extent = "60 45";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
    };
    %bmpWidth = getWord(%bitmap.getExtent(), 0);
    %bmpHeight = getWord(%bitmap.getExtent(), 1);
    %bitmap.setBitmap($CSMediaDisplay::RadioBunnyThumb);
    %xPos = %xPos + (%bmpWidth + %padding);
    %windowWidth = %windowWidth - %bmpWidth;
    %textTitle = new GuiMLTextCtrl()
    {
        profile = "GuiMessageTextProfile";
        horizSizing = "width";
        vertSizing = "bottom";
        position = %xPos SPC %ypos;
        extent = %windowWidth - %padding SPC 14;
        minExtent = "8 8";
        sluggishness = -1;
        visible = 1;
        lineSpacing = 2;
        allowColorChars = 0;
        maxChars = -1;
        helpTag = 0;
    };
    %textTitle.bindClassName("CSMediaMLText");
    %textTitle.setText("SHOUTcast Stream");
    %ypos = %ypos + (13 + %padding);
    %textEntry = new GuiTextEditCtrl()
    {
        profile = %child.isReadOnly ? "ETSDarkReadonlyTextEditProfile" : "ETSDarkTextEditProfile";
        horizSizing = "center";
        vertSizing = "top";
        position = %xPos SPC %ypos;
        extent = (%windowWidth - %padding) - 1 SPC 18;
        minExtent = "8 8";
        visible = 1;
        setFirstResponder = 0;
        altCommand = %this @ ".changeMediaLink(" @ %child @ ");";
        validate = %this @ ".changeMediaLink(" @ %child @ ");";
        helpTag = 0;
        historySize = 0;
        readOnly = %child.isReadOnly;
    };
    %ypos = %ypos + (16 + %padding);
    %textInfo = new GuiMLTextCtrl()
    {
        profile = "GuiMessageTextProfile";
        horizSizing = "width";
        vertSizing = "bottom";
        position = %xPos SPC %ypos;
        extent = %windowWidth - ((%padding * 2) + %authorWidth) SPC 14;
        minExtent = "8 8";
        sluggishness = -1;
        visible = 1;
        lineSpacing = 2;
        allowColorChars = 0;
        maxChars = -1;
        helpTag = 0;
    };
    %textInfo.bindClassName("CSMediaMLText");
    %textInfo.setText("Retrieving stream info...");
    %xPos = %xPos + (%padding + getWord(%textInfo.extent, 0));
    %textAuthor = new GuiMLTextCtrl()
    {
        profile = "GuiMessageTextProfile";
        horizSizing = "width";
        vertSizing = "bottom";
        position = %xPos SPC %ypos;
        extent = %authorWidth SPC 14;
        minExtent = "8 8";
        sluggishness = -1;
        visible = 1;
        lineSpacing = 2;
        allowColorChars = 0;
        maxChars = -1;
        helpTag = 0;
    };
    %textAuthor.bindClassName("CSMediaMLText");
    %ypos = %ypos + (14 + %padding);
    %playButton = new GuiBitmapButtonCtrl()
    {
        profile = "GuiDefaultProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = "2 2";
        extent = "60 45";
        minExtent = "22 22";
        sluggishness = -1;
        visible = 1;
        command = %this @ ".PlayButtonPushed(" @ %child @ ");";
        text = "";
        groupNum = -1;
        buttonType = "PushButton";
        bitmap = "platform/client/buttons/playMedia";
        modulationColor = "255 255 255 180";
    };
    %playButton.bindClassName("CSBitmapButton");
    %bitmap.add(%playButton);
    %child.add(%bitmap);
    %child.add(%textTitle);
    %child.add(%textEntry);
    %child.add(%textInfo);
    %child.add(%textAuthor);
    %child.playButton = %playButton;
    %child.playbuttonAvailable = 1;
    %child.thumbnail = %bitmap;
    %child.medialink = %textEntry;
    %child.mediatitle = %textTitle;
    %child.mediainfo = %textInfo;
    %child.mediaauthor = %textAuthor;
    %child.displayType = $CSMediaDisplay::TypeShoutCast;
    return ;
}
function CSMediaDisplay::buildChildDisplayEmpty(%this, %child, %url)
{
    %padding = 2;
    %windowWidth = getWord(%child.getExtent(), 0);
    %xPos = 1;
    %ypos = 0;
    %bitmap = new GuiBitmapCtrl()
    {
        profile = "ETSNonModalProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = %xPos SPC %ypos;
        extent = "45 45";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
    };
    %bmpWidth = 60;
    %bmpHeight = getWord(%bitmap.getExtent(), 1);
    %bitmap.setBitmap($CSMediaDisplay::EmptyBunnyThumb);
    %xPos = %xPos + (%bmpWidth + %padding);
    %windowWidth = %windowWidth - %bmpWidth;
    %textTitle = new GuiMLTextCtrl()
    {
        profile = "GuiMessageTextProfile";
        horizSizing = "width";
        vertSizing = "bottom";
        position = %xPos SPC %ypos;
        extent = %windowWidth - %padding SPC 14;
        minExtent = "8 8";
        sluggishness = -1;
        visible = 1;
        lineSpacing = 2;
        allowColorChars = 0;
        maxChars = -1;
        helpTag = 0;
    };
    %textTitle.bindClassName("CSMediaMLText");
    %textTitle.setText("Media URL:");
    %ypos = %ypos + (14 + %padding);
    %textEntry = new GuiTextEditCtrl()
    {
        profile = "ETSDarkTextEditProfile";
        horizSizing = "center";
        vertSizing = "top";
        position = %xPos SPC %ypos;
        extent = (%windowWidth - %padding) - 1 SPC 18;
        minExtent = "8 8";
        visible = 1;
        setFirstResponder = 0;
        altCommand = %this @ ".changeMediaLink(" @ %child @ ");";
        validate = %this @ ".changeMediaLink(" @ %child @ ");";
        helpTag = 0;
        historySize = 0;
        canHilite = 1;
    };
    %ypos = %ypos + (18 + %padding);
    %child.add(%bitmap);
    %child.add(%textTitle);
    %child.add(%textEntry);
    %child.playButton = "";
    %child.playbuttonAvailable = 0;
    %child.thumbnail = %bitmap;
    %child.medialink = %textEntry;
    %child.mediatitle = %textTitle;
    %child.mediainfo = "";
    %child.displayType = $CSMediaDisplay::TypeEmpty;
    return ;
}
function CSMediaMLText::onMouseDragged(%this)
{
    %parent = %this.getParent();
    if (findWord(%parent.getNamespaceList(), "CSMediaHotListItem") != -1)
    {
        %parent.setAsDragControl(1);
        return 1;
    }
    return 0;
}
function CSBitmapButton::onMouseDown(%this)
{
    %this.origin = Canvas.getCursorPos();
    return ;
}
function CSBitmapButton::onMouseDragged(%this)
{
    %parent = %this.getParent().getParent();
    if (findWord(%parent.getNamespaceList(), "CSMediaHotListItem") == -1)
    {
        return 0;
    }
    %vec = VectorSub(%this.origin, Canvas.getCursorPos());
    if (VectorLenSquared(%vec) < (12 * 12))
    {
        return 0;
    }
    %parent.setAsDragControl(1);
    return 1;
}
function CSMediaHotListItem::onMouseDragged(%this)
{
    %this.setAsDragControl(1);
    return ;
}
function CSMediaHotListItem::makeVisualClone(%this)
{
    %padding = 1;
    %windowWidth = getWord(%this.getExtent(), 0);
    %xPos = 1;
    %ypos = 0;
    %ctrl = new GuiControl()
    {
        profile = "DragAndDropProfile";
        horizSizing = "width";
        vertSizing = "height";
        position = "0 0";
        extent = %this.getExtent();
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
    };
    %bitmap = new GuiBitmapCtrl()
    {
        profile = "ETSNonModalProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = %xPos SPC %ypos + 1;
        extent = "60 45";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
    };
    %ctrl.add(%bitmap);
    %bitmap.setBitmap(%this.getObject(0).getBitmap());
    %bmpWidth = getWord(%bitmap.getExtent(), 0);
    %bmpHeight = getWord(%bitmap.getExtent(), 1);
    %xPos = %xPos + (%bmpWidth + %padding);
    %windowWidth = %windowWidth - %bmpWidth;
    %textTitle = new GuiMLTextCtrl()
    {
        profile = "GuiMessageTextProfile";
        horizSizing = "width";
        vertSizing = "bottom";
        position = %xPos SPC %ypos;
        extent = %windowWidth - %padding SPC 14;
        minExtent = "8 8";
        sluggishness = -1;
        visible = 1;
        lineSpacing = 2;
        allowColorChars = 0;
        maxChars = -1;
        helpTag = 0;
    };
    %textTitle.bindClassName("CSMediaMLText");
    %textTitle.setText("YouTube Video");
    %ypos = %ypos + (13 + %padding);
    %ctrl.add(%textTitle);
    %ctrl.mediatitle = %textTitle;
    %ctrl.AuthorName = %this.AuthorName;
    %ctrl.title = %this.title;
    CSMediaDisplay.buildYoutubeTitle(%ctrl);
    return %ctrl;
}
function CSMediaFavListItem::onDragAndDropEnter(%this, %dragCtrl)
{
    if (findWord(%dragCtrl.getNamespaceList(), "CSMediaHotListItem") == -1)
    {
        return ;
    }
    hiliteControl(%this.medialink);
    return ;
}
function CSMediaFavListItem::onDragAndDropLeave(%this, %dragCtrl)
{
    hiliteControl(0);
    return ;
}
function CSMediaFavListItem::onDragAndDropMove(%this, %dragCtrl, %unused)
{
    return ;
}
function CSMediaFavListItem::onDragAndDropDrop(%this, %dragCtrl, %unused)
{
    %url = CSMediaDisplay.getMediaLink(%dragCtrl);
    if (%url $= "")
    {
        return 0;
    }
    %this.autoplay = 1;
    CSMediaDisplay.updateMediaLinkTo(%this, %url, 1);
    return 1;
}
function CSMediaFavListItem::onSystemDragDropEvent(%this, %text, %eventType, %pt)
{
    if (!isURL(%text))
    {
        return 0;
    }
    if (%this.displayType == $CSMediaDisplay::TypeRadio)
    {
        return 0;
    }
    if (!Parent::onSystemDragDropEvent(%this, %text, %eventType, %pt))
    {
        return 0;
    }
    hiliteControl(%this.medialink);
    if (%eventType $= "BREAK")
    {
        %this.autoplay = 1;
        if (isObject(%this.mediainfo))
        {
            %this.mediainfo.setText("Retrieving stream info...");
        }
        CSMediaDisplay.updateMediaLinkTo(%this, %text, 0);
    }
    return 1;
}
