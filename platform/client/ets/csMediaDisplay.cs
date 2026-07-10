$CSMediaDisplay::TypeEmpty = 0;
$CSMediaDisplay::TypeRadio = 1;
$CSMediaDisplay::TypeYoutube = 2;
$CSMediaDisplay::TypeShoutCast = 3;
$CSMediaDisplay::DefaultFavoriteCount = 15;
$CSMediaDisplay::TotalEntryCount = ($CSMediaDisplay::DefaultFavoriteCount * 2.0);
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
function CSMediaDisplay::toggle(%this) {
    if (%this.isVisible()) {
        %this.close();
    }
    %this.open();
};
function CSMediaDisplay::open(%this) {
    closeCSPanelsInOtherCategories(%this);
    1.setVisible(%this);
    %this.focusAndRaise(PlayGui);
    WindowManager.update();
    CustomSpaceClient::checkEditingSpace();
    if (!(%this.periodic $= "")) {
        cancel(%this.periodic);
        %this.periodic = "";
    }
    %this.playingStream.syncPlayingMediaStream(%this);
    %this.periodicCycle();
    %this.update();
};
function CSMediaDisplay::close(%this) {
    0.setVisible(%this);
    CustomSpaceClient::checkEditingSpace();
    PlayGui.focusTopWindow();
    WindowManager.update();
    if (!(%this.periodic $= "")) {
        cancel(%this.periodic);
        %this.periodic = "";
    }
    return 1;
};
function CSMediaDisplay::periodicCycle(%this) {
    cancel(%this.periodic);
    %this.cycleYoutubeThumbnails();
    %this.periodic = periodicCycle @ 4000.schedule(%this);
};
function CSMediaDisplay::update(%this) {
};
function CSMediaDisplay::Initialize() {
    if (!(CSMediaDisplay @ " " @ %this.initialized $= "")) {
    }
    if ((%this.initialized == CSMediaDisplay)) {
        return 1.0;
    }
    %newExtent = getWord(CSMediaFavDisplayArray, %this.childrenExtent, 0) @ " " @ 51;
    %this.childrenExtent = %newExtent @ CSMediaFavDisplayArray;
    %this.childrenExtent = %newExtent @ CSMediaHotDisplayArray;
    $CSMediaDisplay::DefaultFavoriteCount.setNumChildren(CSMediaFavDisplayArray);
    $CSMediaDisplay::DefaultFavoriteCount.setNumChildren(CSMediaHotDisplayArray);
    %this.visible = 0 @ CSMediaWhatsHotSelector;
    %this.playingChild = 0 @ CSMediaDisplay;
    %this.playingStream = "" @ CSMediaDisplay;
    %this.showingWhatsHot = 0 @ CSMediaDisplay;
    %this.initialized = 1 @ CSMediaDisplay;
};
function CSMediaFavDisplayArray::onCreatedChild(%this, %child, %unused, %y) {
    if ((%y > 0.0)) {
        %child.setProfile();
    }
    %child.setProfile();
    %child.isReadOnly = GuiDefaultProfile @ 0;
    ETSDroppableProfile;
    %child.systemDragDrop = 1;
    if ((%this.getCount() == 1.0)) {
        %child.buildChildDisplayRadio(CSMediaDisplay);
    }
    %child.buildChildDisplayEmpty(CSMediaDisplay);
    %child.forceRadio = (%this.getCount() == 1.0);
    %child.visible = 1;
    %child.oldMediaLink = "";
    if (!(getWord(%child.getNamespaceList(), 0) $= "CSMediaFavListItem")) {
        "CSMediaFavListItem".bindClassName(%child);
    }
};
function CSMediaHotDisplayArray::onCreatedChild(%this, %child) {
    %child.isReadOnly = 1;
    %child.buildChildDisplayEmpty(CSMediaDisplay);
    %child.forceRadio = 0;
    %child.visible = 0;
    %child.oldMediaLink = "";
    if (!(getWord(%child.getNamespaceList(), 0) $= "CSMediaHotListItem")) {
        echo("Binding CSMediaHotListItem to " @ %child);
        "CSMediaHotListItem".bindClassName(%child);
    }
};
function CSMediaDisplay::buttonWhatsHot(%this) {
    %widthDelta = (getWord(CSMediaWhatsHotSelector.getExtent(), 0) + 3.0);
    %width = getWord(%this.getExtent(), 0);
    %height = getWord(%this.getExtent(), 1);
    if (%child.showingWhatsHot) {
        %child.text = " What's Hot >> " @ CSMediaWhatsHotButton;
        CSMediaDisplay;
        "352 24".reposition(CSMediaWhatsHotButton);
        %widthDelta = (%widthDelta * -(1.0));
    }
    csRequestHotMedia();
    %child.text = " << Hide " @ CSMediaWhatsHotButton;
    "423 24".reposition(CSMediaWhatsHotButton);
    %this.showingWhatsHot = !(%this.showingWhatsHot);
    %height.setTrgExtent(%this, (%width + %widthDelta));
    %this.visible = %this.showingWhatsHot @ CSMediaWhatsHotSelector;
};
function CSMediaDisplay::onReachedTarget(%this) {
    WindowManager.update();
};
function CSMediaDisplay::setMediaFavorites(%this, %mediaList) {
    0.setMediaList(%this, %mediaList, 0, CSMediaFavDisplayArray.getCount());
};
function CSMediaDisplay::setMediaHotlist(%this, %mediaList) {
    1.setMediaList(%this, %mediaList, $CSMediaDisplay::DefaultFavoriteCount, CSMediaHotDisplayArray.getCount());
};
function CSMediaDisplay::setMediaList(%this, %mediaList, %startIdx, %maxIdx, %hideEmpty) {
    %count = getFieldCount(%mediaList);
    %idx = 0;
    while ((%idx < %count)) {
        %linkInfo = getField(%mediaList, %idx);
        %linkName = getWord(%linkInfo, 0);
        %child = (%idx + %startIdx).getChildDisplay(%this);
        %child.visible = 1;
        %infoCount = getWordCount(%linkInfo);
        (%infoCount > 2.0).updateMediaLinkTo(%this, %child, %linkName);
        if ((%infoCount > 1.0)) {
            getWord(%linkInfo, 2).setMediaInfo(%this, %child, getWord(%linkInfo, 1));
        }
        %streamID = %linkName.extractMediaStreamId(%this);
        if ((%this.playingStream $= %linkName)) {
            %this.playingChild = %child;
            %child.playButton.visible = 0;
        }
        if (!(%streamID $= "")) {
            %urlinfo = new ScriptObject("");;
            0;
            if (isObject(MissionCleanup)) {
                %urlinfo.add(MissionCleanup);
            }
            "URLInfo".bindClassName(%urlinfo);
            %urlinfo.url = %this.playingStream;
            %tStreamInfo = %streamID @ ".ogg";
            if (!(%urlinfo.parse())) {
            }
            if ((%urlinfo.Path $= %tStreamInfo)) {
                %this.playingChild = %child;
                %child.playButton.visible = 0;
                %this.playingStream = %linkName;
            }
            %urlinfo.delete();
        }
        %idx = (%idx + 1.0);
    }
    while ((%idx < %maxIdx)) {
        %child = (%idx + %startIdx).getChildDisplay(%this);
        (%idx < %count);
        0.updateMediaLinkTo(%this, %child, "");
        if (%hideEmpty) {
            %child.visible = 0;
        }
        %idx = (%idx + 1.0);
    }
};
function CSMediaDisplay::setMediaStatistics(%this, %url, %views, %plays) {
    %count = %this.getChildCount();
    %idx = 0;
    while ((%idx < %count)) {
        %child = %idx.getChildDisplay(%this);
        %medialink = %child.getMediaLink(%this);
        if ((%medialink $= %url)) {
            %plays.setMediaInfo(%this, %child, %views);
        }
        %idx = (%idx + 1.0);
    }
};
function CSMediaDisplay::clearMediaStatistics(%this, %url) {
    %count = %this.getChildCount();
    %idx = 0;
    while ((%idx < %count)) {
        %child = %idx.getChildDisplay(%this);
        %medialink = %child.getMediaLink(%this);
        if ((%medialink $= %url)) {
            %child.setNoMediaInfo(%this);
        }
        %idx = (%idx + 1.0);
    }
};
function CSMediaDisplay::setMediaInfo(%this, %child, %views, %plays) {
    if (!(%child.mediainfo $= "")) {
        %text = "<color:ffffff70>" @ %plays @ " plays";
        %text.setText(%child.mediainfo);
    }
};
function CSMediaDisplay::setNoMediaInfo(%this, %child) {
    if (!(%child.mediainfo $= "")) {
        "(no stats)".setText(%child.mediainfo);
    }
};
function CSMediaDisplay::getMediaFavorites(%this) {
    %mediaList = "";
    %idx = 0;
    while ((%idx < $CSMediaDisplay::DefaultFavoriteCount)) {
        %child = %idx.getChildDisplay(%this);
        if (!(%child $= "")) {
            %medialink = %child.getMediaLink(%this);
            if (!(%medialink $= "")) {
                if (!(%mediaList $= "")) {
                    %mediaList = %mediaList @ "\t" @ %medialink;
                }
                %mediaList = %medialink;
            }
        }
        %idx = (%idx + 1.0);
    }
    return %mediaList;
};
function CSMediaDisplay::syncPlayingAudioStream(%this, %streamID) {
    if ((strstr(%streamID, "http://") < 0.0)) {
        "vside://radio/" @ %streamID.syncPlayingMediaStream(%this);
    }
    %streamID.syncPlayingMediaStream(%this);
};
function CSMediaDisplay::syncPlayingMediaStream(%this, %medialink) {
    %child = %medialink.findChildWithMedialink(%this);
    if ((%child > 0.0)) {
        %child.setPlayingChild(%this);
    }
    %this.playingStream = %medialink;
};
function CSMediaDisplay::playMediaStream(%this, %newStreamUrl, %displayURL) {
    %mediaType = %newStreamUrl.getMediaType(%this);
    %musicStream = "";
    %videoStream = "no-video";
    %streamType = "";
    if ((%mediaType == $CSMediaDisplay::TypeRadio)) {
        %musicStream = %newStreamUrl.extractMediaStreamId(%this);
        %ratableURL = ;
        customSpace::SetMusicStreamID(%musicStream);
        customSpace::SetVideoURL("");
        %streamType = "RADIO";
    }
    if ((%mediaType == $CSMediaDisplay::TypeYoutube)) {
        %videoStream = %newStreamUrl;
        %ratableURL = ;
        customSpace::SetMusicStreamID("");
        customSpace::SetVideoURL(%videoStream);
        if (strstr(%newStreamUrl, "v=")) {
            %streamType = "VIDEO";
        }
        if (strstr(%newStreamUrl, "p=")) {
            %streamType = "VIDEO_PLAYLIST";
        }
    }
    if ((%mediaType == $CSMediaDisplay::TypeShoutCast)) {
        %musicStream = %newStreamUrl;
        %ratableURL = %displayURL;
        customSpace::SetMusicStreamID(%musicStream, %displayURL);
        customSpace::SetVideoURL("");
        %streamType = "VIDEO";
    }
    customSpace::SetMusicStreamID("");
    customSpace::SetVideoURL("");
    if (!(%streamType $= "")) {
        csRecordMediaShow(%ratableURL, %streamType);
    }
    CustomSpaceSettings::saveSettings(CustomSpaceClient::GetSpaceImIn(), "", "", "", %musicStream, %videoStream);
};
function CSMediaDisplay::extractMediaStreamId(%this, %medialink) {
    %url = new ScriptObject("");;
    0;
    if (isObject(MissionCleanup)) {
        %url.add(MissionCleanup);
    }
    "URLInfo".bindClassName(%url);
    %url.url = %medialink;
    %streamID = "";
    if (%url.parse()) {
        if ((stricmp(%url.protocol, "vside") == 0.0)) {
        }
        if ((stricmp(%url.host, "radio") == 0.0)) {
            %streamID = %url.Path;
        }
    }
    %url.delete();
    return %streamID;
};
function CSMediaDisplay::PlayButtonPushed(%this, %child) {
    %child.setPlayingChild(%this);
    if (!(%child.streamUrl $= "")) {
        %this.playingStream.playMediaStream(%this, %child.streamUrl);
    }
    %this.playingStream.playMediaStream(%this);
};
function CSMediaDisplay::setPlayingChild(%this, %child) {
    %count = %this.getChildCount();
    %idx = 0;
    while ((%idx < %count)) {
        %otherChild = %idx.getChildDisplay(%this);
        if ((%child != %otherChild)) {
            %otherChild.isPlaying = 0;
            if (!(%otherChild.highlight $= "")) {
            }
            if (isObject(%otherChild.highlight)) {
                %otherChild.highlight.remove(%otherChild);
                %otherChild.highlight.delete();
                %otherChild.highlight = "";
            }
            1.setPlaybuttonVisible(%this, %otherChild);
        }
        %idx = (%idx + 1.0);
    }
    %medialink = "";
    (%idx < %count);
    if ((%child != 0.0)) {
        %child.isPlaying = 1;
        if ((%child.highlight $= "")) {
            %extent = %child.getExtent();
            %child.highlight = new GuiConvBubbleCtrl("") {
                profile = 0 @ "ETSLightHighlightProfile";
                extent = getWord(%extent, 0) @ " " @ (getWord(%extent, 1) - 4.0);
                position = "0 0";
                roundRadius = 4;
                roundInterps = 2;
                sluggishness = -(1.0);
            };
            %child.highlight.add(%child);
        }
        0.setPlaybuttonVisible(%this, %child);
        %medialink = %child.getMediaLink(%this);
    }
    %this.playingStream = %medialink;
    %this.playingChild = %child;
};
function CSMediaDisplay::findChildWithMedialink(%this, %medialink) {
    %count = %this.getChildCount();
    %idx = 0;
    while ((%idx < %count)) {
        %child = %idx.getChildDisplay(%this);
        %testlink = %child.getMediaLink(%this);
        if ((%testlink $= %medialink)) {
            return %child;
        }
        if ((%child.streamUrl $= %medialink)) {
            return %child;
        }
        %idx = (%idx + 1.0);
    }
    return 0;
};
function CSMediaDisplay::stopAllMedia(%this) {
    0.PlayButtonPushed(%this);
};
function CSMediaDisplay::showHelp(%this) {
    %msg = ;
    %dlg = MessageBoxOK("My Music & Videos - How To", %msg, "");
    300.resize(%dlg.window, 550);
};
function CSMediaDisplay::getMediaLink(%this, %child) {
    if ((%child.medialink $= "")) {
        return "";
    }
    if ((%child.displayType != $CSMediaDisplay::TypeRadio)) {
        %medialinkValue = %child.medialink.getText();
    }
    %medialinkName = %child.medialink.getText();
    %idx = -(1.0);
    if (isObject($musicStreamNameMap)) {
        %idx = %medialinkName.findKey($musicStreamNameMap);
    }
    if ((%idx == -(1.0))) {
        %streamID = %medialinkName;
    }
    %streamID = %idx.getValue($musicStreamNameMap);
    %medialinkValue = "vside://radio/" @ %streamID;
    return %medialinkValue;
};
function CSMediaDisplay::setMediaLink(%this, %child, %medialink, %skipStatistics) {
    if ((%child.medialink $= "")) {
        return;
    }
    if ((%child.displayType == $CSMediaDisplay::TypeYoutube)) {
        %validate = %child.medialink.validate;
        %child.medialink.validate = "";
        %medialink.setText(%child.medialink);
        %child.medialink.validate = %validate;
        %child.skipStatistics = %skipStatistics;
        %child.requestYoutubeInfo(%this);
        1.setPlaybuttonAvailable(%this, %child);
        %child.oldMediaLink = %medialink;
    }
    if ((%child.displayType == $CSMediaDisplay::TypeShoutCast)) {
        %validate = %child.medialink.validate;
        %child.medialink.validate = "";
        %medialink.setText(%child.medialink);
        %child.medialink.validate = %validate;
        %child.skipStatistics = %skipStatistics;
        %child.requestShoutCastInfo(%this);
        1.setPlaybuttonAvailable(%this, %child);
        %child.oldMediaLink = %medialink;
    }
    %url = new ScriptObject("");;
    0;
    if (isObject(MissionCleanup)) {
        %url.add(MissionCleanup);
    }
    "URLInfo".bindClassName(%url);
    %url.url = %medialink;
    %url.parse();
    %path = %url.Path;
    %url.delete();
    %count = 0;
    if (isObject($musicStreamNameMap)) {
        %count = $musicStreamNameMap.size();
    }
    %idx = 0;
    while ((%idx < %count)) {
        if ((stricmp(%idx.getValue($musicStreamNameMap), %path) == 0.0)) {
        }
        %idx = (%idx + 1.0);
    }
    if ((%idx < %count)) {
        %newStreamName = %idx.getKey($musicStreamNameMap);
        (%idx < %count);
        %index = %newStreamName.findText(%child.medialink);
        %index.SetSelected(%child.medialink);
    }
    %path.setText(%child.medialink);
    !(%path $= "-").setPlaybuttonAvailable(%this, %child);
};
function CSMediaDisplay::setPlaybuttonAvailable(%this, %child, %avail) {
    %child.playbuttonAvailable = %avail;
    if (!(%child.playButton $= "")) {
        if (!(%avail)) {
            %child.playButton.visible = 0;
        }
        %child.playButton.visible = !(%child.isPlaying);
    }
};
function CSMediaDisplay::setPlaybuttonVisible(%this, %child, %visible) {
    if (%child.playbuttonAvailable) {
    }
    if (!(%child.playButton $= "")) {
        %child.playButton.visible = %visible;
    }
};
function CSMediaDisplay::changeMediaLink(%this, %child) {
    %child.schedule(%this, 0, "changeMediaLinkReally");
};
function CSMediaDisplay::changeMediaLinkReally(%this, %child) {
    %newMediaType = %child.medialink.getMediaType(%this);
    %newMediaLink = %child.getMediaLink(%this);
    if ((%child.oldMediaLink $= %newMediaLink)) {
        return;
    }
    0.updateMediaLinkTo(%this, %child, %newMediaLink);
    %newMediaType = %newMediaLink.getMediaType(%this);
    if ((%this.playingChild == %child)) {
        if ((%newMediaType == $CSMediaDisplay::TypeRadio) && (%newMediaLink $= "vside://radio/-")) {
            %newMediaLink = "";
            %this.playingChild = 0;
            %child.isPlaying = 0;
        }
        if ((%newMediaType == $CSMediaDisplay::TypeShoutCast)) {
            %child.autoplay = 1;
        }
        %newMediaLink.playMediaStream(%this);
        %this.playingStream = %newMediaLink;
    }
    if ((%newMediaType == $CSMediaDisplay::TypeRadio)) {
    }
    if ((%newMediaLink $= "")) {
        csSaveMediaFavorites();
    }
};
function CSMediaDisplay::updateMediaLinkTo(%this, %child, %newMediaLink, %skipStats) {
    %child.streamUrl = "";
    %newMediaType = %newMediaLink.getMediaType(%this);
    if ((%newMediaType == $CSMediaDisplay::TypeRadio)) {
        %url = new ScriptObject("");;
        0;
        if (isObject(MissionCleanup)) {
            %url.add(MissionCleanup);
        }
        "URLInfo".bindClassName(%url);
        %url.url = %newMediaLink;
        %url.parse();
        %path = %url.Path;
        %url.delete();
        if ((%path $= "-")) {
        }
        if (!(%child.forceRadio)) {
            %newMediaType = $CSMediaDisplay::TypeEmpty;
            %newMediaLink = "";
        }
    }
    if ((%newMediaType == $CSMediaDisplay::TypeYoutube)) {
        %normalizedURL = CSMediaDisplay::normalizeYoutubeURL(%newMediaLink);
        if ((%normalizedURL $= "")) {
            %newMediaType = $CSMediaDisplay::TypeEmpty;
        }
        %newMediaLink = %normalizedURL;
    }
    if ((%newMediaType != %child.displayType)) {
        %child.clearChildDisplay(%this);
        if ((%newMediaType == $CSMediaDisplay::TypeRadio)) {
            %child.buildChildDisplayRadio(%this);
        }
        if ((%newMediaType == $CSMediaDisplay::TypeYoutube)) {
            %child.buildChildDisplayYouTube(%this);
        }
        if ((%newMediaType == $CSMediaDisplay::TypeShoutCast)) {
            %child.buildChildDisplayShoutCast(%this);
        }
        %child.buildChildDisplayEmpty(%this);
    }
    %skipStats.setMediaLink(%this, %child, %newMediaLink);
};
function CSMediaDisplay::getMediaType(%this, %medialink) {
    %url = new ScriptObject("");;
    0;
    if (isObject(MissionCleanup)) {
        %url.add(MissionCleanup);
    }
    "URLInfo".bindClassName(%url);
    %url.url = %medialink;
    if (!(%url.parse())) {
        %url.delete();
        return $CSMediaDisplay::TypeEmpty;
    }
    %type = $CSMediaDisplay::TypeEmpty;
    if ((stricmp(%url.protocol, "vside") == 0.0)) {
        if ((stricmp(%url.host, "radio") == 0.0)) {
            %type = $CSMediaDisplay::TypeRadio;
        }
    }
    if ((stricmp(%url.protocol, "http") == 0.0)) {
        if ((strstr(%url.host, "youtube.") >= 0.0)) {
            %type = $CSMediaDisplay::TypeYoutube;
        }
        %type = $CSMediaDisplay::TypeShoutCast;
    }
    debug("Unknown media type: " @ %medialink);
    %url.delete();
    return %type;
};
function CSMediaDisplay::updateRadioStreams(%this) {
    %count = %this.getChildCount();
    %idx = 0;
    while ((%idx < %count)) {
        %child = %idx.getChildDisplay(%this);
        if ((%child.displayType == $CSMediaDisplay::TypeRadio)) {
            %child.updateRadioDropDown(%this);
        }
        %idx = (%idx + 1.0);
    }
};
function CSMediaDisplay::updateRadioDropDown(%this, %child) {
    %medialink = %child.getMediaLink(%this);
    %dropdown = %child.medialink;
    %dropdown.clear();
    %count = $musicStreamNameMap.size();
    %idx = 0;
    while ((%idx < %count)) {
        %idx.getKey($musicStreamNameMap).add(%dropdown);
        %idx = (%idx + 1.0);
    }
    0.setMediaLink(%this, %child, %medialink);
};
function CSMediaDisplay::cycleYoutubeThumbnails(%this) {
    %idx = 0;
    while ((%idx < $CSMediaDisplay::TotalEntryCount)) {
        %child = %idx.getChildDisplay(%this);
        if ((%child.displayType == $CSMediaDisplay::TypeYoutube)) {
            0.selectYoutubeThumbnails(%this, %child);
        }
        %idx = (%idx + 1.0);
    }
};
function CSMediaDisplay::buildYoutubeTitle(%this, %child) {
    %AuthorName = "";
    %title = "YouTube Video";
    if (!(%child.AuthorName $= "")) {
        %AuthorName = %child.AuthorName;
    }
    if (!(%child.title $= "")) {
        %title = %child.title;
    }
    %FullTitle = "";
    if (!(%AuthorName $= "")) {
        %authorString = "<just:right><color:ffffff80><linkcolorhl:ffaaff><a:gamelink " @ $CSMediaDisplay::YoutubeProfile @ %AuthorName @ ">";
        %authorString = %authorString @ %AuthorName @ "</a>";
        if (!(%child.mediaauthor $= "")) {
            %authorString.setText(%child.mediaauthor);
        }
    }
    %FullTitle = "<spush><b>" @ %title @ "<spop>" @ %FullTitle;
    %FullTitle = "<clip:" @ getWord(%child.mediatitle.extent, 0) @ ">" @ %FullTitle @ "</clip>";
    %FullTitle.setText(%child.mediatitle);
};
function CSMediaDisplay::buildShoutCastTitle(%this, %child) {
    %AuthorName = "";
    %title = "ShoutCast Stream";
    %homeURL = "";
    if (!(%child.AuthorName $= "")) {
        %AuthorName = %child.AuthorName;
    }
    if (!(%child.title $= "")) {
        %title = %child.title;
    }
    if (!(%child.homeURL $= "")) {
        %homeURL = %child.homeURL;
    }
    if (!(%homeURL $= "")) {
        %FullTitle = "<a:gamelink " @ %homeURL @ "/><clip:" @ getWord(%child.mediatitle.extent, 0) @ ">" @ %title @ "</clip></a>";
    }
    %FullTitle = "<clip:" @ getWord(%child.mediatitle.extent, 0) @ ">" @ %title @ "</clip>";
    %FullTitle.setText(%child.mediatitle);
};
function CSMediaDisplay::selectYoutubeThumbnails(%this, %child, %force) {
    if (!(%force)) {
        %chance = getRandom(0, 99);
        if ((%chance > %child.changeThumbChance)) {
            %child.changeThumbChance = (%child.changeThumbChance + %child.changeCume);
            return;
        }
    }
    %child.changeThumbChance = %child.changeCume;
    %index = getRandom(0, (%child.thumbCount - 1.0));
    %child.currentThumb = %index;
    if (!(%child.currentThumb @ " " @ %child.thumbURL $= "")) {
        "youtube".downloadAndApplyBitmap(%child.thumbnail, %child.currentThumb, %child.thumbURL);
    }
};
function CSMediaDisplay::normalizeYoutubeURL(%medialink) {
    %url = new ScriptObject("");;
    0;
    if (isObject(MissionCleanup)) {
        %url.add(MissionCleanup);
    }
    "URLInfo".bindClassName(%url);
    %url.url = %medialink;
    %url.parse();
    %urlOut = "http://" @ %url.host @ "/";
    if (!("v" @ " " @ %url.param $= "")) {
        %urlOut = %urlOut @ "watch?v=" @ "v" @ %url.param;
    }
    if (!("p" @ " " @ %url.param $= "")) {
        %urlOut = %urlOut @ "view_play_list?p=" @ "p" @ %url.param;
    }
    %url.delete();
    return "";
    %url.delete();
    return %urlOut;
};
function CSMediaDisplay::requestYoutubeInfo(%this, %child) {
    if (!(%child.gdataRequest $= "")) {
        %child.gdataRequest.delete();
    }
    %child.title = "";
    %child.AuthorName = "";
    if (!(%child.thumbCount $= "")) {
        %idx = 0;
        while ((%idx < %child.thumbCount)) {
            %child.thumbURL = "" @ %idx;
            %idx = (%idx + 1.0);
        }
    }
    %child.thumbCount = (%idx < %child.thumbCount) @ "";
    %url = new ScriptObject("");;
    0;
    if (isObject(MissionCleanup)) {
        %url.add(MissionCleanup);
    }
    "URLInfo".bindClassName(%url);
    %url.url = %child.getMediaLink(%this);
    %url.parse();
    %gdataRequest = $CSMediaDisplay::GDataAPIURL;
    if (!("v" @ " " @ %url.param $= "")) {
        %gdataRequest = %gdataRequest @ $CSMediaDisplay::GDataAPIVideoInfo @ "v" @ %url.param;
    }
    if (!("p" @ " " @ %url.param $= "")) {
        %gdataRequest = %gdataRequest @ $CSMediaDisplay::GDataAPIPlaylistInfo @ "p" @ %url.param;
    }
    return;
    %child.gdataRequest = 0 @ new XMLDoc("");;
    if (isObject(MissionCleanup)) {
        %child.gdataRequest.add(MissionCleanup);
    }
    "CSMDGDataRequest".bindClassName(%child.gdataRequest);
    %child.gdataRequest.control = %child;
    %child.gdataRequest.Display = %this;
    %gdataRequest.parseXMLFromURL(%child.gdataRequest);
};
function CSMediaDisplay::requestShoutCastInfo(%this, %child) {
    if (!(%child.scRequest $= "")) {
        %child.scRequest.delete();
    }
    %child.title = "";
    %child.AuthorName = "";
    %child.thumbCount = "";
    %url = %child.getMediaLink(%this);
    if ((strstr(%url, ".mp3") > 0.0)) {
        %child.scRequest = 0 @ new M3UDemuxer("");;
    }
    if ((strstr(%url, ".pls") > 0.0)) {
        %child.scRequest = 0 @ new PLSDemuxer("");;
        %child.URLtypeUnknown = 1;
    }
    if ((strstr(%url, ".m3u") > 0.0)) {
        %child.scRequest = 0 @ new M3UDemuxer("");;
    }
    %child.scRequest = 0 @ new PLSDemuxer("");;
    %child.URLtypeUnknown = 1;
    if (isObject(MissionCleanup)) {
        %child.scRequest.add(MissionCleanup);
    }
    "CSSCDataRequest".bindClassName(%child.scRequest);
    %child.scRequest.control = %child;
    %child.scRequest.Display = %this;
    %child.getMediaLink(%this).setURL(%child.scRequest);
    %child.scRequest.start();
};
function CSSCDataRequest::onDone(%this, %url) {
    %child = %this.control;
    %window = %this.Display;
    %child.scRequest = "";
    %child.streamUrl = %url;
    %child.title = %this.getTitle();
    %child.homeURL = %this.getHomeURL();
    %child.buildShoutCastTitle(%window);
    "".setText(%child.mediainfo);
    $CSMediaDisplay::ShoutCastThumb.setBitmap(%child.thumbnail);
    schedule(%this, "delete", 0);
    %medialink = %child.getMediaLink(%window);
    if (!(%child.skipStatistics)) {
        csRequestMediaStatistics(%medialink);
    }
    if ((%child.autoplay == 1.0)) {
        %child.autoplay = 0;
        %child.isPlaying = 1;
        if (%child.isPlaying) {
            CSMediaDisplay.stopAllMedia();
        }
        %child.PlayButtonPushed(CSMediaDisplay);
    }
    csSaveMediaFavorites();
};
function CSSCDataRequest::onError(%this) {
    %child = %this.control;
    %window = %this.Display;
    if ((%child.URLtypeUnknown == 1.0)) {
        %child.scRequest = 0 @ new M3UDemuxer("");;
        if (isObject(MissionCleanup)) {
            %child.scRequest.add(MissionCleanup);
        }
        "CSSCDataRequest".bindClassName(%child.scRequest);
        %child.scRequest.control = %child;
        %child.scRequest.Display = %window;
        %child.URLtypeUnknown = 0;
        %child.getMediaLink(%window).setURL(%child.scRequest);
        %child.scRequest.start();
        schedule(%this, "delete", 0);
        return;
    }
    %medialink = %child.getMediaLink(%window);
    %child.scRequest = "";
    $CSMediaDisplay::ShoutCastThumb.setBitmap(%child.thumbnail);
    $CSMediaDisplay::ShoutCastErrorTitle.setText(%child.mediatitle);
    %lastError = %this.getErrorBuffer();
    if ((%lastError $= "")) {
        %lastError = $CSMediaDisplay::ShoutCastErrorInfo;
    }
    %lastError.setText(%child.mediainfo);
    echo("Invalid Media URL: " @ %child.getMediaLink(%window) @ " Error: " @ %lastError);
    0.setPlaybuttonAvailable(%window, %child);
    schedule(%this, "delete", 0);
};
function CSMDGDataRequest::onDone(%this) {
    %child = %this.control;
    %window = %this.Display;
    %child.gdataRequest = "";
    %root = %this.getRootElement();
    schedule(%this, "delete", 0);
    if (!(%root)) {
        log("error", "No Root element in returned XML...");
        return;
    }
    %child.thumbCount = 0;
    if ((%root.getValue() $= "feed")) {
        %root.parseFeedNode(%window, %child);
        %child.changeCume = 5;
    }
    if ((%root.getValue() $= "entry")) {
        1.parseEntryNode(%window, %child, %root);
        %child.changeCume = 2;
    }
    log("error", "media", "Root node is not an entry or feed tag");
    return;
    %child.buildYoutubeTitle(%window);
    1.selectYoutubeThumbnails(%window, %child);
    %medialink = %child.getMediaLink(%window);
    if (!(%child.skipStatistics)) {
        csRequestMediaStatistics(%medialink);
    }
    if ((%child.autoplay == 1.0)) {
        %child.autoplay = 0;
        %child.isPlaying = 1;
        if (%child.isPlaying) {
            CSMediaDisplay.stopAllMedia();
        }
        %child.PlayButtonPushed(CSMediaDisplay);
    }
    csSaveMediaFavorites();
};
function CSMediaDisplay::parseEntryNode(%this, %child, %entryNode, %setTitle) {
    %MediaGroup = "media:group".getFirstChild(%entryNode);
    if (%setTitle) {
        %AuthorNode = "author".getFirstChild(%entryNode);
        %AuthorNameNode = "name".getFirstChild(%AuthorNode);
        %child.AuthorName = %AuthorNameNode.getText();
        %TitleNode = "media:title".getFirstChild(%MediaGroup);
        %child.title = %TitleNode.getText();
    }
    %ThumbnailIdx = %child.thumbCount;
    %ThumbnailNode = "media:thumbnail".getFirstChild(%MediaGroup);
    while (%ThumbnailNode) {
        %child.thumbURL = "url".getAttribute(%ThumbnailNode) @ %ThumbnailIdx;
        %ThumbnailIdx = (%ThumbnailIdx + 1.0);
        %ThumbnailNode = "media:thumbnail".getNext(%ThumbnailNode);
    }
    %child.thumbCount = %ThumbnailNode @ %ThumbnailIdx;
};
function CSMediaDisplay::parseFeedNode(%this, %child, %feedNode) {
    %AuthorNode = "author".getFirstChild(%feedNode);
    %AuthorNode = "name".getFirstChild(%AuthorNode);
    %child.AuthorName = %AuthorNode.getText();
    %MediaGroup = "media:group".getFirstChild(%feedNode);
    %TitleNode = "media:title".getFirstChild(%MediaGroup);
    %child.title = %TitleNode.getText();
    %entry = "entry".getFirstChild(%feedNode);
    while (%entry) {
        0.parseEntryNode(%this, %child, %entry);
        %entry = "entry".getNext(%entry);
    }
};
function CSMDGDataRequest::onError(%this) {
    %child = %this.control;
    %window = %this.Display;
    %child.gdataRequest = "";
    $CSMediaDisplay::YoutubeErrorThumb.setBitmap(%child.thumbnail);
    $CSMediaDisplay::YoutubeErrorTitle.setText(%child.mediatitle);
    $CSMediaDisplay::YoutubeErrorInfo.setText(%child.mediainfo);
    0.setPlaybuttonAvailable(%window, %child);
    schedule(%this, "delete", 0);
};
function CSMediaDisplay::getChildDisplay(%this, %childIdx) {
    %faveCount = CSMediaFavDisplayArray.getCount();
    if ((%faveCount > %childIdx)) {
        return %childIdx.getObject(CSMediaFavDisplayArray);
    }
    if (((%childIdx - %faveCount) < CSMediaFavDisplayArray.getCount())) {
        return (%childIdx - %faveCount).getObject(CSMediaHotDisplayArray);
    }
    return "";
};
function CSMediaDisplay::getChildCount(%this) {
    return (CSMediaFavDisplayArray.getCount() + CSMediaHotDisplayArray.getCount());
};
function CSMediaDisplay::clearChildDisplay(%this, %child) {
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
    if (!(%child.thumbCount $= "")) {
        %idx = 0;
        while ((%idx < %child.thumbCount)) {
            %child.thumbURL = "" @ %idx;
            %idx = (%idx + 1.0);
        }
    }
    %child.thumbCount = (%idx < %child.thumbCount) @ "";
};
function CSMediaDisplay::buildChildDisplayYouTube(%this, %child) {
    %padding = 1;
    %windowWidth = getWord(%child.getExtent(), 0);
    %authorWidth = 80;
    %xPos = 1;
    %ypos = 0;
    %bitmap = new GuiBitmapCtrl("") {
        profile = 0 @ "ETSNonModalProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = %xPos @ " " @ (%ypos + 1.0);
        extent = "60 45";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
    };
    %bmpWidth = getWord(%bitmap.getExtent(), 0);
    %bmpHeight = getWord(%bitmap.getExtent(), 1);
    $CSMediaDisplay::YoutubeDefaultThumb.setBitmap(%bitmap);
    %xPos = (%xPos + (%bmpWidth + %padding));
    %windowWidth = (%windowWidth - %bmpWidth);
    %textTitle = new GuiMLTextCtrl("") {
        profile = 0 @ "GuiMessageTextProfile";
        horizSizing = "width";
        vertSizing = "bottom";
        position = %xPos @ " " @ %ypos;
        extent = (%windowWidth - %padding) @ " " @ 14;
        minExtent = "8 8";
        sluggishness = -1;
        visible = 1;
        lineSpacing = 2;
        allowColorChars = 0;
        maxChars = -1;
        helpTag = 0;
    };
    "CSMediaMLText".bindClassName(%textTitle);
    "YouTube Video".setText(%textTitle);
    %ypos = (%ypos + (13.0 + %padding));
    %textEntry = new GuiTextEditCtrl("") {
        profile = 0 @ %child.isReadOnly ? "ETSDarkReadonlyTextEditProfile" : "ETSDarkTextEditProfile";
        horizSizing = "center";
        vertSizing = "top";
        position = %xPos @ " " @ %ypos;
        extent = ((%windowWidth - %padding) - 1.0) @ " " @ 18;
        minExtent = "8 8";
        visible = 1;
        setFirstResponder = 0;
        altCommand = %this @ ".changeMediaLink(" @ %child @ ");";
        validate = %this @ ".changeMediaLink(" @ %child @ ");";
        helpTag = 0;
        historySize = 0;
        readOnly = %child.isReadOnly;
    };
    %ypos = (%ypos + (16.0 + %padding));
    %textInfo = new GuiMLTextCtrl("") {
        profile = 0 @ "GuiMessageTextProfile";
        horizSizing = "width";
        vertSizing = "bottom";
        position = %xPos @ " " @ %ypos;
        extent = (%windowWidth - ((%padding * 2.0) + %authorWidth)) @ " " @ 14;
        minExtent = "8 8";
        sluggishness = -1;
        visible = 1;
        lineSpacing = 2;
        allowColorChars = 0;
        maxChars = -1;
        helpTag = 0;
    };
    "CSMediaMLText".bindClassName(%textInfo);
    "Retrieving video info...".setText(%textInfo);
    %xPos = (%xPos + (%padding + getWord(%textInfo.extent, 0)));
    %textAuthor = new GuiMLTextCtrl("") {
        profile = 0 @ "GuiMessageTextProfile";
        horizSizing = "width";
        vertSizing = "bottom";
        position = %xPos @ " " @ %ypos;
        extent = %authorWidth @ " " @ 14;
        minExtent = "8 8";
        sluggishness = -1;
        visible = 1;
        lineSpacing = 2;
        allowColorChars = 0;
        maxChars = -1;
        helpTag = 0;
    };
    "CSMediaMLText".bindClassName(%textAuthor);
    %ypos = (%ypos + (14.0 + %padding));
    %playButton = new GuiBitmapButtonCtrl("") {
        profile = 0 @ "GuiDefaultProfile";
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
    "CSBitmapButton".bindClassName(%playButton);
    %playButton.add(%bitmap);
    %bitmap.add(%child);
    %textTitle.add(%child);
    %textEntry.add(%child);
    %textInfo.add(%child);
    %textAuthor.add(%child);
    %child.playButton = %playButton;
    %child.playbuttonAvailable = 1;
    %child.thumbnail = %bitmap;
    %child.medialink = %textEntry;
    %child.mediatitle = %textTitle;
    %child.mediainfo = %textInfo;
    %child.mediaauthor = %textAuthor;
    %child.displayType = $CSMediaDisplay::TypeYoutube;
};
function CSMediaDisplay::buildChildDisplayRadio(%this, %child, %url) {
    %padding = 2;
    %windowWidth = getWord(%child.getExtent(), 0);
    %xPos = 1;
    %ypos = 0;
    %bitmap = new GuiBitmapCtrl("") {
        profile = 0 @ "ETSNonModalProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = %xPos @ " " @ %ypos;
        extent = "45 45";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
    };
    %bmpWidth = 60;
    %bmpHeight = getWord(%bitmap.getExtent(), 1);
    $CSMediaDisplay::RadioBunnyThumb.setBitmap(%bitmap);
    %xPos = (%xPos + (%bmpWidth + %padding));
    %windowWidth = (%windowWidth - %bmpWidth);
    %textTitle = new GuiMLTextCtrl("") {
        profile = 0 @ "GuiMessageTextProfile";
        horizSizing = "width";
        vertSizing = "bottom";
        position = %xPos @ " " @ %ypos;
        extent = (%windowWidth - %padding) @ " " @ 14;
        minExtent = "8 8";
        sluggishness = -1;
        visible = 1;
        lineSpacing = 2;
        allowColorChars = 0;
        maxChars = -1;
        helpTag = 0;
    };
    "CSMediaMLText".bindClassName(%textTitle);
    "vSide Radio".setText(%textTitle);
    %ypos = (%ypos + (14.0 + %padding));
    %dropdown = new GuiPopUp2MenuCtrl(CSMediaMusicStreamPopup) {
        profile = "InfoWindowPopupProfile";
        scrollProfile = "DottedScrollProfile";
        winProfile = "InfoWindowPopupWindowProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = %xPos @ " " @ %ypos;
        extent = ((%windowWidth - %padding) - 2.0) @ " " @ 30;
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
        command = %this @ ".changeMediaLink(" @ %child @ ");";
        text = "";
        maxLength = 255;
        maxPopupHeight = 200;
        allowReverse = 0;
    };
    %playButton = new GuiBitmapButtonCtrl("") {
        profile = 0 @ "GuiDefaultProfile";
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
    "CSBitmapButton".bindClassName(%playButton);
    %playButton.add(%bitmap);
    %bitmap.add(%child);
    %textTitle.add(%child);
    %dropdown.add(%child);
    if (isObject($musicStreamNameMap)) {
        %count = $musicStreamNameMap.size();
    }
    %count = 0;
    %idx = 0;
    while ((%idx < %count)) {
        %idx.getKey($musicStreamNameMap).add(%dropdown);
        %idx = (%idx + 1.0);
    }
    if (%child.isReadOnly) {
        (%idx < %count);
        %blocker = new GuiMLTextCtrl("") {
            profile = 0 @ "GuiMessageTextProfile";
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
        "CSMediaMLText".bindClassName(%blocker);
        %blocker.add(%child);
    }
    %child.playButton = %playButton;
    %child.playbuttonAvailable = 1;
    %child.thumbnail = %bitmap;
    %child.medialink = %dropdown;
    %child.mediatitle = %textTitle;
    %child.mediainfo = "";
    %child.displayType = $CSMediaDisplay::TypeRadio;
};
function CSMediaDisplay::buildChildDisplayShoutCast(%this, %child) {
    %padding = 1;
    %windowWidth = getWord(%child.getExtent(), 0);
    %authorWidth = 80;
    %xPos = 1;
    %ypos = 0;
    %bitmap = new GuiBitmapCtrl("") {
        profile = 0 @ "ETSNonModalProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = %xPos @ " " @ (%ypos + 1.0);
        extent = "60 45";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
    };
    %bmpWidth = getWord(%bitmap.getExtent(), 0);
    %bmpHeight = getWord(%bitmap.getExtent(), 1);
    $CSMediaDisplay::RadioBunnyThumb.setBitmap(%bitmap);
    %xPos = (%xPos + (%bmpWidth + %padding));
    %windowWidth = (%windowWidth - %bmpWidth);
    %textTitle = new GuiMLTextCtrl("") {
        profile = 0 @ "GuiMessageTextProfile";
        horizSizing = "width";
        vertSizing = "bottom";
        position = %xPos @ " " @ %ypos;
        extent = (%windowWidth - %padding) @ " " @ 14;
        minExtent = "8 8";
        sluggishness = -1;
        visible = 1;
        lineSpacing = 2;
        allowColorChars = 0;
        maxChars = -1;
        helpTag = 0;
    };
    "CSMediaMLText".bindClassName(%textTitle);
    "SHOUTcast Stream".setText(%textTitle);
    %ypos = (%ypos + (13.0 + %padding));
    %textEntry = new GuiTextEditCtrl("") {
        profile = 0 @ %child.isReadOnly ? "ETSDarkReadonlyTextEditProfile" : "ETSDarkTextEditProfile";
        horizSizing = "center";
        vertSizing = "top";
        position = %xPos @ " " @ %ypos;
        extent = ((%windowWidth - %padding) - 1.0) @ " " @ 18;
        minExtent = "8 8";
        visible = 1;
        setFirstResponder = 0;
        altCommand = %this @ ".changeMediaLink(" @ %child @ ");";
        validate = %this @ ".changeMediaLink(" @ %child @ ");";
        helpTag = 0;
        historySize = 0;
        readOnly = %child.isReadOnly;
    };
    %ypos = (%ypos + (16.0 + %padding));
    %textInfo = new GuiMLTextCtrl("") {
        profile = 0 @ "GuiMessageTextProfile";
        horizSizing = "width";
        vertSizing = "bottom";
        position = %xPos @ " " @ %ypos;
        extent = (%windowWidth - ((%padding * 2.0) + %authorWidth)) @ " " @ 14;
        minExtent = "8 8";
        sluggishness = -1;
        visible = 1;
        lineSpacing = 2;
        allowColorChars = 0;
        maxChars = -1;
        helpTag = 0;
    };
    "CSMediaMLText".bindClassName(%textInfo);
    "Retrieving stream info...".setText(%textInfo);
    %xPos = (%xPos + (%padding + getWord(%textInfo.extent, 0)));
    %textAuthor = new GuiMLTextCtrl("") {
        profile = 0 @ "GuiMessageTextProfile";
        horizSizing = "width";
        vertSizing = "bottom";
        position = %xPos @ " " @ %ypos;
        extent = %authorWidth @ " " @ 14;
        minExtent = "8 8";
        sluggishness = -1;
        visible = 1;
        lineSpacing = 2;
        allowColorChars = 0;
        maxChars = -1;
        helpTag = 0;
    };
    "CSMediaMLText".bindClassName(%textAuthor);
    %ypos = (%ypos + (14.0 + %padding));
    %playButton = new GuiBitmapButtonCtrl("") {
        profile = 0 @ "GuiDefaultProfile";
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
    "CSBitmapButton".bindClassName(%playButton);
    %playButton.add(%bitmap);
    %bitmap.add(%child);
    %textTitle.add(%child);
    %textEntry.add(%child);
    %textInfo.add(%child);
    %textAuthor.add(%child);
    %child.playButton = %playButton;
    %child.playbuttonAvailable = 1;
    %child.thumbnail = %bitmap;
    %child.medialink = %textEntry;
    %child.mediatitle = %textTitle;
    %child.mediainfo = %textInfo;
    %child.mediaauthor = %textAuthor;
    %child.displayType = $CSMediaDisplay::TypeShoutCast;
};
function CSMediaDisplay::buildChildDisplayEmpty(%this, %child, %url) {
    %padding = 2;
    %windowWidth = getWord(%child.getExtent(), 0);
    %xPos = 1;
    %ypos = 0;
    %bitmap = new GuiBitmapCtrl("") {
        profile = 0 @ "ETSNonModalProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = %xPos @ " " @ %ypos;
        extent = "45 45";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
    };
    %bmpWidth = 60;
    %bmpHeight = getWord(%bitmap.getExtent(), 1);
    $CSMediaDisplay::EmptyBunnyThumb.setBitmap(%bitmap);
    %xPos = (%xPos + (%bmpWidth + %padding));
    %windowWidth = (%windowWidth - %bmpWidth);
    %textTitle = new GuiMLTextCtrl("") {
        profile = 0 @ "GuiMessageTextProfile";
        horizSizing = "width";
        vertSizing = "bottom";
        position = %xPos @ " " @ %ypos;
        extent = (%windowWidth - %padding) @ " " @ 14;
        minExtent = "8 8";
        sluggishness = -1;
        visible = 1;
        lineSpacing = 2;
        allowColorChars = 0;
        maxChars = -1;
        helpTag = 0;
    };
    "CSMediaMLText".bindClassName(%textTitle);
    "Media URL:".setText(%textTitle);
    %ypos = (%ypos + (14.0 + %padding));
    %textEntry = new GuiTextEditCtrl("") {
        profile = 0 @ "ETSDarkTextEditProfile";
        horizSizing = "center";
        vertSizing = "top";
        position = %xPos @ " " @ %ypos;
        extent = ((%windowWidth - %padding) - 1.0) @ " " @ 18;
        minExtent = "8 8";
        visible = 1;
        setFirstResponder = 0;
        altCommand = %this @ ".changeMediaLink(" @ %child @ ");";
        validate = %this @ ".changeMediaLink(" @ %child @ ");";
        helpTag = 0;
        historySize = 0;
        canHilite = 1;
    };
    %ypos = (%ypos + (18.0 + %padding));
    %bitmap.add(%child);
    %textTitle.add(%child);
    %textEntry.add(%child);
    %child.playButton = "";
    %child.playbuttonAvailable = 0;
    %child.thumbnail = %bitmap;
    %child.medialink = %textEntry;
    %child.mediatitle = %textTitle;
    %child.mediainfo = "";
    %child.displayType = $CSMediaDisplay::TypeEmpty;
};
function CSMediaMLText::onMouseDragged(%this) {
    %parent = %this.getParent();
    if ((findWord(%parent.getNamespaceList(), "CSMediaHotListItem") != -(1.0))) {
        1.setAsDragControl(%parent);
        return 1;
    }
    return 0;
};
function CSBitmapButton::onMouseDown(%this) {
    %this.origin = Canvas.getCursorPos();
};
function CSBitmapButton::onMouseDragged(%this) {
    %parent = %this.getParent().getParent();
    if ((findWord(%parent.getNamespaceList(), "CSMediaHotListItem") == -(1.0))) {
        return 0;
    }
    %vec = VectorSub(%this.origin, Canvas.getCursorPos());
    if ((VectorLenSquared(%vec) < (12.0 * 12.0))) {
        return 0;
    }
    1.setAsDragControl(%parent);
    return 1;
};
function CSMediaHotListItem::onMouseDragged(%this) {
    1.setAsDragControl(%this);
};
function CSMediaHotListItem::makeVisualClone(%this) {
    %padding = 1;
    %windowWidth = getWord(%this.getExtent(), 0);
    %xPos = 1;
    %ypos = 0;
    %ctrl = new GuiControl("") {
        profile = 0 @ "DragAndDropProfile";
        horizSizing = "width";
        vertSizing = "height";
        position = "0 0";
        extent = %this.getExtent();
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
    };
    %bitmap = new GuiBitmapCtrl("") {
        profile = 0 @ "ETSNonModalProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = %xPos @ " " @ (%ypos + 1.0);
        extent = "60 45";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
    };
    %bitmap.add(%ctrl);
    0.getObject(%this).getBitmap().setBitmap(%bitmap);
    %bmpWidth = getWord(%bitmap.getExtent(), 0);
    %bmpHeight = getWord(%bitmap.getExtent(), 1);
    %xPos = (%xPos + (%bmpWidth + %padding));
    %windowWidth = (%windowWidth - %bmpWidth);
    %textTitle = new GuiMLTextCtrl("") {
        profile = 0 @ "GuiMessageTextProfile";
        horizSizing = "width";
        vertSizing = "bottom";
        position = %xPos @ " " @ %ypos;
        extent = (%windowWidth - %padding) @ " " @ 14;
        minExtent = "8 8";
        sluggishness = -1;
        visible = 1;
        lineSpacing = 2;
        allowColorChars = 0;
        maxChars = -1;
        helpTag = 0;
    };
    "CSMediaMLText".bindClassName(%textTitle);
    "YouTube Video".setText(%textTitle);
    %ypos = (%ypos + (13.0 + %padding));
    %textTitle.add(%ctrl);
    %ctrl.mediatitle = %textTitle;
    %ctrl.AuthorName = %this.AuthorName;
    %ctrl.title = %this.title;
    %ctrl.buildYoutubeTitle(CSMediaDisplay);
    return %ctrl;
};
function CSMediaFavListItem::onDragAndDropEnter(%this, %dragCtrl) {
    if ((findWord(%dragCtrl.getNamespaceList(), "CSMediaHotListItem") == -(1.0))) {
        return;
    }
    hiliteControl(%this.medialink);
};
function CSMediaFavListItem::onDragAndDropLeave(%this, %dragCtrl) {
    hiliteControl(0);
};
function CSMediaFavListItem::onDragAndDropMove(%this, %dragCtrl, %unused) {
};
function CSMediaFavListItem::onDragAndDropDrop(%this, %dragCtrl, %unused) {
    %url = %dragCtrl.getMediaLink(CSMediaDisplay);
    if ((%url $= "")) {
        return 0;
    }
    %this.autoplay = 1;
    1.updateMediaLinkTo(CSMediaDisplay, %this, %url);
    return 1;
};
function CSMediaFavListItem::onSystemDragDropEvent(%this, %text, %eventType, %pt) {
    if (!(isURL(%text))) {
        return 0;
    }
    if ((%this.displayType == $CSMediaDisplay::TypeRadio)) {
        return 0;
    }
    if (!(Parent::onSystemDragDropEvent(%this, %text, %eventType, %pt))) {
        return 0;
    }
    hiliteControl(%this.medialink);
    if ((%eventType $= "BREAK")) {
        %this.autoplay = 1;
        if (isObject(%this.mediainfo)) {
            "Retrieving stream info...".setText(%this.mediainfo);
        }
        0.updateMediaLinkTo(CSMediaDisplay, %this, %text);
    }
    return 1;
};
