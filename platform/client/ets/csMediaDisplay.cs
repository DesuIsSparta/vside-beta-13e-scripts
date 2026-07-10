$CSMediaDisplay::TypeEmpty = 0;
$CSMediaDisplay::TypeRadio = 1;
$CSMediaDisplay::TypeYoutube = 2;
$CSMediaDisplay::TypeShoutCast = 3;
$CSMediaDisplay::DefaultFavoriteCount = 15;
$CSMediaDisplay::TotalEntryCount = (2.0 * $CSMediaDisplay::DefaultFavoriteCount);
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
    %this.setVisible(1);
    PlayGui.focusAndRaise(%this);
    WindowManager.update();
    CustomSpaceClient::checkEditingSpace();
    if (!(%this.periodic $= "")) {
        cancel(%this.periodic);
        %this.periodic = "";
    }
    %this.syncPlayingMediaStream(%this.playingStream);
    %this.periodicCycle();
    %this.update();
};
function CSMediaDisplay::close(%this) {
    %this.setVisible(0);
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
    %this.periodic = periodicCycle @ %this.schedule(4000);
};
function CSMediaDisplay::update(%this) {
};
function CSMediaDisplay::Initialize() {
    if (!(CSMediaDisplay @ " " @ %this.initialized $= "")) {
    }
    if ((CSMediaDisplay == %this.initialized)) {
        return 1.0;
    }
    %newExtent = getWord(CSMediaFavDisplayArray, %this.childrenExtent, 0) @ " " @ 51;
    %this.childrenExtent = %newExtent @ CSMediaFavDisplayArray;
    %this.childrenExtent = %newExtent @ CSMediaHotDisplayArray;
    CSMediaFavDisplayArray.setNumChildren($CSMediaDisplay::DefaultFavoriteCount);
    CSMediaHotDisplayArray.setNumChildren($CSMediaDisplay::DefaultFavoriteCount);
    %this.visible = 0 @ CSMediaWhatsHotSelector;
    %this.playingChild = 0 @ CSMediaDisplay;
    %this.playingStream = "" @ CSMediaDisplay;
    %this.showingWhatsHot = 0 @ CSMediaDisplay;
    %this.initialized = 1 @ CSMediaDisplay;
};
function CSMediaFavDisplayArray::onCreatedChild(%this, %child, %unused, %y) {
    if ((0.0 > %y)) {
        %child.setProfile();
    }
    %child.setProfile();
    %child.isReadOnly = GuiDefaultProfile @ 0;
    ETSDroppableProfile;
    %child.systemDragDrop = 1;
    if ((1.0 == %this.getCount())) {
        CSMediaDisplay.buildChildDisplayRadio(%child);
    }
    CSMediaDisplay.buildChildDisplayEmpty(%child);
    %child.forceRadio = (1.0 == %this.getCount());
    %child.visible = 1;
    %child.oldMediaLink = "";
    if (!(getWord(%child.getNamespaceList(), 0) $= "CSMediaFavListItem")) {
        %child.bindClassName("CSMediaFavListItem");
    }
};
function CSMediaHotDisplayArray::onCreatedChild(%this, %child) {
    %child.isReadOnly = 1;
    CSMediaDisplay.buildChildDisplayEmpty(%child);
    %child.forceRadio = 0;
    %child.visible = 0;
    %child.oldMediaLink = "";
    if (!(getWord(%child.getNamespaceList(), 0) $= "CSMediaHotListItem")) {
        echo("Binding CSMediaHotListItem to " @ %child);
        %child.bindClassName("CSMediaHotListItem");
    }
};
function CSMediaDisplay::buttonWhatsHot(%this) {
    %widthDelta = (3.0 + getWord(CSMediaWhatsHotSelector.getExtent(), 0));
    %width = getWord(%this.getExtent(), 0);
    %height = getWord(%this.getExtent(), 1);
    if (%child.showingWhatsHot) {
        %child.text = " What's Hot >> " @ CSMediaWhatsHotButton;
        CSMediaDisplay;
        CSMediaWhatsHotButton.reposition("352 24");
        %widthDelta = (-(1.0) * %widthDelta);
    }
    csRequestHotMedia();
    %child.text = " << Hide " @ CSMediaWhatsHotButton;
    CSMediaWhatsHotButton.reposition("423 24");
    %this.showingWhatsHot = !(%this.showingWhatsHot);
    %this.setTrgExtent((%widthDelta + %width), %height);
    %this.visible = %this.showingWhatsHot @ CSMediaWhatsHotSelector;
};
function CSMediaDisplay::onReachedTarget(%this) {
    WindowManager.update();
};
function CSMediaDisplay::setMediaFavorites(%this, %mediaList) {
    %this.setMediaList(%mediaList, 0, CSMediaFavDisplayArray.getCount(), 0);
};
function CSMediaDisplay::setMediaHotlist(%this, %mediaList) {
    %this.setMediaList(%mediaList, $CSMediaDisplay::DefaultFavoriteCount, CSMediaHotDisplayArray.getCount(), 1);
};
function CSMediaDisplay::setMediaList(%this, %mediaList, %startIdx, %maxIdx, %hideEmpty) {
    %count = getFieldCount(%mediaList);
    %idx = 0;
    if ((%count < %idx)) {
        %linkInfo = getField(%mediaList, %idx);
        %linkName = getWord(%linkInfo, 0);
        %child = %this.getChildDisplay((%startIdx + %idx));
        %child.visible = 1;
        %infoCount = getWordCount(%linkInfo);
        %this.updateMediaLinkTo(%child, %linkName, (2.0 > %infoCount));
        if ((1.0 > %infoCount)) {
            %this.setMediaInfo(%child, getWord(%linkInfo, 1), getWord(%linkInfo, 2));
        }
        %streamID = %this.extractMediaStreamId(%linkName);
        if ((%this.playingStream $= %linkName)) {
            %this.playingChild = %child;
            %child.playButton.visible = 0;
        }
        if (!(%streamID $= "")) {
            %urlinfo = new ScriptObject("");;
            0;
            if (isObject(MissionCleanup)) {
                MissionCleanup.add(%urlinfo);
            }
            %urlinfo.bindClassName("URLInfo");
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
        %idx = (1.0 + %idx);
    }
    if ((%maxIdx < %idx)) {
        %child = %this.getChildDisplay((%startIdx + %idx));
        (%count < %idx);
        %this.updateMediaLinkTo(%child, "", 0);
        if (%hideEmpty) {
            %child.visible = 0;
        }
        %idx = (1.0 + %idx);
    }
};
function CSMediaDisplay::setMediaStatistics(%this, %url, %views, %plays) {
    %count = %this.getChildCount();
    %idx = 0;
    if ((%count < %idx)) {
        %child = %this.getChildDisplay(%idx);
        %medialink = %this.getMediaLink(%child);
        if ((%medialink $= %url)) {
            %this.setMediaInfo(%child, %views, %plays);
        }
        %idx = (1.0 + %idx);
    }
};
function CSMediaDisplay::clearMediaStatistics(%this, %url) {
    %count = %this.getChildCount();
    %idx = 0;
    if ((%count < %idx)) {
        %child = %this.getChildDisplay(%idx);
        %medialink = %this.getMediaLink(%child);
        if ((%medialink $= %url)) {
            %this.setNoMediaInfo(%child);
        }
        %idx = (1.0 + %idx);
    }
};
function CSMediaDisplay::setMediaInfo(%this, %child, %views, %plays) {
    if (!(%child.mediainfo $= "")) {
        %text = "<color:ffffff70>" @ %plays @ " plays";
        %child.mediainfo.setText(%text);
    }
};
function CSMediaDisplay::setNoMediaInfo(%this, %child) {
    if (!(%child.mediainfo $= "")) {
        %child.mediainfo.setText("(no stats)");
    }
};
function CSMediaDisplay::getMediaFavorites(%this) {
    %mediaList = "";
    %idx = 0;
    if (($CSMediaDisplay::DefaultFavoriteCount < %idx)) {
        %child = %this.getChildDisplay(%idx);
        if (!(%child $= "")) {
            %medialink = %this.getMediaLink(%child);
            if (!(%medialink $= "")) {
                if (!(%mediaList $= "")) {
                    %mediaList = %mediaList @ "\t" @ %medialink;
                }
                %mediaList = %medialink;
            }
        }
        %idx = (1.0 + %idx);
    }
    return %mediaList;
};
function CSMediaDisplay::syncPlayingAudioStream(%this, %streamID) {
    if ((0.0 < strstr(%streamID, "http://"))) {
        %this.syncPlayingMediaStream("vside://radio/" @ %streamID);
    }
    %this.syncPlayingMediaStream(%streamID);
};
function CSMediaDisplay::syncPlayingMediaStream(%this, %medialink) {
    %child = %this.findChildWithMedialink(%medialink);
    if ((0.0 > %child)) {
        %this.setPlayingChild(%child);
    }
    %this.playingStream = %medialink;
};
function CSMediaDisplay::playMediaStream(%this, %newStreamUrl, %displayURL) {
    %mediaType = %this.getMediaType(%newStreamUrl);
    %musicStream = "";
    %videoStream = "no-video";
    %streamType = "";
    if (($CSMediaDisplay::TypeRadio == %mediaType)) {
        %musicStream = %this.extractMediaStreamId(%newStreamUrl);
        %ratableURL = ;
        customSpace::SetMusicStreamID(%musicStream);
        customSpace::SetVideoURL("");
        %streamType = "RADIO";
    }
    if (($CSMediaDisplay::TypeYoutube == %mediaType)) {
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
    if (($CSMediaDisplay::TypeShoutCast == %mediaType)) {
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
        MissionCleanup.add(%url);
    }
    %url.bindClassName("URLInfo");
    %url.url = %medialink;
    %streamID = "";
    if (%url.parse()) {
        if ((0.0 == stricmp(%url.protocol, "vside"))) {
        }
        if ((0.0 == stricmp(%url.host, "radio"))) {
            %streamID = %url.Path;
        }
    }
    %url.delete();
    return %streamID;
};
function CSMediaDisplay::PlayButtonPushed(%this, %child) {
    %this.setPlayingChild(%child);
    if (!(%child.streamUrl $= "")) {
        %this.playMediaStream(%child.streamUrl, %this.playingStream);
    }
    %this.playMediaStream(%this.playingStream);
};
function CSMediaDisplay::setPlayingChild(%this, %child) {
    %count = %this.getChildCount();
    %idx = 0;
    if ((%count < %idx)) {
        %otherChild = %this.getChildDisplay(%idx);
        if ((%otherChild != %child)) {
            %otherChild.isPlaying = 0;
            if (!(%otherChild.highlight $= "")) {
            }
            if (isObject(%otherChild.highlight)) {
                %otherChild.remove(%otherChild.highlight);
                %otherChild.highlight.delete();
                %otherChild.highlight = "";
            }
            %this.setPlaybuttonVisible(%otherChild, 1);
        }
        %idx = (1.0 + %idx);
    }
    %medialink = "";
    (%count < %idx);
    if ((0.0 != %child)) {
        %child.isPlaying = 1;
        if ((%child.highlight $= "")) {
            %extent = %child.getExtent();
            %child.highlight = new GuiConvBubbleCtrl("") {
                profile = 0 @ "ETSLightHighlightProfile";
                extent = getWord(%extent, 0) @ " " @ (4.0 - getWord(%extent, 1));
                position = "0 0";
                roundRadius = 4;
                roundInterps = 2;
                sluggishness = -(1.0);
            };
            %child.add(%child.highlight);
        }
        %this.setPlaybuttonVisible(%child, 0);
        %medialink = %this.getMediaLink(%child);
    }
    %this.playingStream = %medialink;
    %this.playingChild = %child;
};
function CSMediaDisplay::findChildWithMedialink(%this, %medialink) {
    %count = %this.getChildCount();
    %idx = 0;
    if ((%count < %idx)) {
        %child = %this.getChildDisplay(%idx);
        %testlink = %this.getMediaLink(%child);
        if ((%testlink $= %medialink)) {
            return %child;
        }
        if ((%child.streamUrl $= %medialink)) {
            return %child;
        }
        %idx = (1.0 + %idx);
    }
    return 0;
};
function CSMediaDisplay::stopAllMedia(%this) {
    %this.PlayButtonPushed(0);
};
function CSMediaDisplay::showHelp(%this) {
    %msg = ;
    %dlg = MessageBoxOK("My Music & Videos - How To", %msg, "");
    %dlg.window.resize(550, 300);
};
function CSMediaDisplay::getMediaLink(%this, %child) {
    if ((%child.medialink $= "")) {
        return "";
    }
    if (($CSMediaDisplay::TypeRadio != %child.displayType)) {
        %medialinkValue = %child.medialink.getText();
    }
    %medialinkName = %child.medialink.getText();
    %idx = -(1.0);
    if (isObject($musicStreamNameMap)) {
        %idx = $musicStreamNameMap.findKey(%medialinkName);
    }
    if ((-(1.0) == %idx)) {
        %streamID = %medialinkName;
    }
    %streamID = $musicStreamNameMap.getValue(%idx);
    %medialinkValue = "vside://radio/" @ %streamID;
    return %medialinkValue;
};
function CSMediaDisplay::setMediaLink(%this, %child, %medialink, %skipStatistics) {
    if ((%child.medialink $= "")) {
        return;
    }
    if (($CSMediaDisplay::TypeYoutube == %child.displayType)) {
        %validate = %child.medialink.validate;
        %child.medialink.validate = "";
        %child.medialink.setText(%medialink);
        %child.medialink.validate = %validate;
        %child.skipStatistics = %skipStatistics;
        %this.requestYoutubeInfo(%child);
        %this.setPlaybuttonAvailable(%child, 1);
        %child.oldMediaLink = %medialink;
    }
    if (($CSMediaDisplay::TypeShoutCast == %child.displayType)) {
        %validate = %child.medialink.validate;
        %child.medialink.validate = "";
        %child.medialink.setText(%medialink);
        %child.medialink.validate = %validate;
        %child.skipStatistics = %skipStatistics;
        %this.requestShoutCastInfo(%child);
        %this.setPlaybuttonAvailable(%child, 1);
        %child.oldMediaLink = %medialink;
    }
    %url = new ScriptObject("");;
    0;
    if (isObject(MissionCleanup)) {
        MissionCleanup.add(%url);
    }
    %url.bindClassName("URLInfo");
    %url.url = %medialink;
    %url.parse();
    %path = %url.Path;
    %url.delete();
    %count = 0;
    if (isObject($musicStreamNameMap)) {
        %count = $musicStreamNameMap.size();
    }
    %idx = 0;
    if ((%count < %idx)) {
        if ((0.0 == stricmp($musicStreamNameMap.getValue(%idx), %path))) {
        }
        %idx = (1.0 + %idx);
    }
    if ((%count < %idx)) {
        %newStreamName = $musicStreamNameMap.getKey(%idx);
        (%count < %idx);
        %index = %child.medialink.findText(%newStreamName);
        %child.medialink.SetSelected(%index);
    }
    %child.medialink.setText(%path);
    %this.setPlaybuttonAvailable(%child, !(%path $= "-"));
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
    %this.schedule(0, "changeMediaLinkReally", %child);
};
function CSMediaDisplay::changeMediaLinkReally(%this, %child) {
    %newMediaType = %this.getMediaType(%child.medialink);
    %newMediaLink = %this.getMediaLink(%child);
    if ((%child.oldMediaLink $= %newMediaLink)) {
        return;
    }
    %this.updateMediaLinkTo(%child, %newMediaLink, 0);
    %newMediaType = %this.getMediaType(%newMediaLink);
    if ((%child == %this.playingChild)) {
        if (($CSMediaDisplay::TypeRadio == %newMediaType)) {
            if ((%newMediaLink $= "vside://radio/-")) {
                %newMediaLink = "";
                %this.playingChild = 0;
                %child.isPlaying = 0;
            }
        }
        if (($CSMediaDisplay::TypeShoutCast == %newMediaType)) {
            %child.autoplay = 1;
        }
        %this.playMediaStream(%newMediaLink);
        %this.playingStream = %newMediaLink;
    }
    if (($CSMediaDisplay::TypeRadio == %newMediaType)) {
    }
    if ((%newMediaLink $= "")) {
        csSaveMediaFavorites();
    }
};
function CSMediaDisplay::updateMediaLinkTo(%this, %child, %newMediaLink, %skipStats) {
    %child.streamUrl = "";
    %newMediaType = %this.getMediaType(%newMediaLink);
    if (($CSMediaDisplay::TypeRadio == %newMediaType)) {
        %url = new ScriptObject("");;
        0;
        if (isObject(MissionCleanup)) {
            MissionCleanup.add(%url);
        }
        %url.bindClassName("URLInfo");
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
    if (($CSMediaDisplay::TypeYoutube == %newMediaType)) {
        %normalizedURL = CSMediaDisplay::normalizeYoutubeURL(%newMediaLink);
        if ((%normalizedURL $= "")) {
            %newMediaType = $CSMediaDisplay::TypeEmpty;
        }
        %newMediaLink = %normalizedURL;
    }
    if ((%child.displayType != %newMediaType)) {
        %this.clearChildDisplay(%child);
        if (($CSMediaDisplay::TypeRadio == %newMediaType)) {
            %this.buildChildDisplayRadio(%child);
        }
        if (($CSMediaDisplay::TypeYoutube == %newMediaType)) {
            %this.buildChildDisplayYouTube(%child);
        }
        if (($CSMediaDisplay::TypeShoutCast == %newMediaType)) {
            %this.buildChildDisplayShoutCast(%child);
        }
        %this.buildChildDisplayEmpty(%child);
    }
    %this.setMediaLink(%child, %newMediaLink, %skipStats);
};
function CSMediaDisplay::getMediaType(%this, %medialink) {
    %url = new ScriptObject("");;
    0;
    if (isObject(MissionCleanup)) {
        MissionCleanup.add(%url);
    }
    %url.bindClassName("URLInfo");
    %url.url = %medialink;
    if (!(%url.parse())) {
        %url.delete();
        return $CSMediaDisplay::TypeEmpty;
    }
    %type = $CSMediaDisplay::TypeEmpty;
    if ((0.0 == stricmp(%url.protocol, "vside"))) {
        if ((0.0 == stricmp(%url.host, "radio"))) {
            %type = $CSMediaDisplay::TypeRadio;
        }
    }
    if ((0.0 == stricmp(%url.protocol, "http"))) {
        if ((0.0 >= strstr(%url.host, "youtube."))) {
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
    if ((%count < %idx)) {
        %child = %this.getChildDisplay(%idx);
        if (($CSMediaDisplay::TypeRadio == %child.displayType)) {
            %this.updateRadioDropDown(%child);
        }
        %idx = (1.0 + %idx);
    }
};
function CSMediaDisplay::updateRadioDropDown(%this, %child) {
    %medialink = %this.getMediaLink(%child);
    %dropdown = %child.medialink;
    %dropdown.clear();
    %count = $musicStreamNameMap.size();
    %idx = 0;
    if ((%count < %idx)) {
        %dropdown.add($musicStreamNameMap.getKey(%idx));
        %idx = (1.0 + %idx);
    }
    %this.setMediaLink(%child, %medialink, 0);
};
function CSMediaDisplay::cycleYoutubeThumbnails(%this) {
    %idx = 0;
    if (($CSMediaDisplay::TotalEntryCount < %idx)) {
        %child = %this.getChildDisplay(%idx);
        if (($CSMediaDisplay::TypeYoutube == %child.displayType)) {
            %this.selectYoutubeThumbnails(%child, 0);
        }
        %idx = (1.0 + %idx);
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
            %child.mediaauthor.setText(%authorString);
        }
    }
    %FullTitle = "<spush><b>" @ %title @ "<spop>" @ %FullTitle;
    %FullTitle = "<clip:" @ getWord(%child.mediatitle.extent, 0) @ ">" @ %FullTitle @ "</clip>";
    %child.mediatitle.setText(%FullTitle);
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
    %child.mediatitle.setText(%FullTitle);
};
function CSMediaDisplay::selectYoutubeThumbnails(%this, %child, %force) {
    if (!(%force)) {
        %chance = getRandom(0, 99);
        if ((%child.changeThumbChance > %chance)) {
            %child.changeThumbChance = (%child.changeCume + %child.changeThumbChance);
            return;
        }
    }
    %child.changeThumbChance = %child.changeCume;
    %index = getRandom(0, (1.0 - %child.thumbCount));
    %child.currentThumb = %index;
    if (!(%child.currentThumb @ " " @ %child.thumbURL $= "")) {
        %child.thumbnail.downloadAndApplyBitmap(%child.currentThumb, %child.thumbURL, "youtube");
    }
};
function CSMediaDisplay::normalizeYoutubeURL(%medialink) {
    %url = new ScriptObject("");;
    0;
    if (isObject(MissionCleanup)) {
        MissionCleanup.add(%url);
    }
    %url.bindClassName("URLInfo");
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
        if ((%child.thumbCount < %idx)) {
            %child.thumbURL = "" @ %idx;
            %idx = (1.0 + %idx);
        }
    }
    %child.thumbCount = (%child.thumbCount < %idx) @ "";
    %url = new ScriptObject("");;
    0;
    if (isObject(MissionCleanup)) {
        MissionCleanup.add(%url);
    }
    %url.bindClassName("URLInfo");
    %url.url = %this.getMediaLink(%child);
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
        MissionCleanup.add(%child.gdataRequest);
    }
    %child.gdataRequest.bindClassName("CSMDGDataRequest");
    %child.gdataRequest.control = %child;
    %child.gdataRequest.Display = %this;
    %child.gdataRequest.parseXMLFromURL(%gdataRequest);
};
function CSMediaDisplay::requestShoutCastInfo(%this, %child) {
    if (!(%child.scRequest $= "")) {
        %child.scRequest.delete();
    }
    %child.title = "";
    %child.AuthorName = "";
    %child.thumbCount = "";
    %url = %this.getMediaLink(%child);
    if ((0.0 > strstr(%url, ".mp3"))) {
        %child.scRequest = 0 @ new M3UDemuxer("");;
    }
    if ((0.0 > strstr(%url, ".pls"))) {
        %child.scRequest = 0 @ new PLSDemuxer("");;
        %child.URLtypeUnknown = 1;
    }
    if ((0.0 > strstr(%url, ".m3u"))) {
        %child.scRequest = 0 @ new M3UDemuxer("");;
    }
    %child.scRequest = 0 @ new PLSDemuxer("");;
    %child.URLtypeUnknown = 1;
    if (isObject(MissionCleanup)) {
        MissionCleanup.add(%child.scRequest);
    }
    %child.scRequest.bindClassName("CSSCDataRequest");
    %child.scRequest.control = %child;
    %child.scRequest.Display = %this;
    %child.scRequest.setURL(%this.getMediaLink(%child));
    %child.scRequest.start();
};
function CSSCDataRequest::onDone(%this, %url) {
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
    if (!(%child.skipStatistics)) {
        csRequestMediaStatistics(%medialink);
    }
    if ((1.0 == %child.autoplay)) {
        %child.autoplay = 0;
        %child.isPlaying = 1;
        if (%child.isPlaying) {
            CSMediaDisplay.stopAllMedia();
        }
        CSMediaDisplay.PlayButtonPushed(%child);
    }
    csSaveMediaFavorites();
};
function CSSCDataRequest::onError(%this) {
    %child = %this.control;
    %window = %this.Display;
    if ((1.0 == %child.URLtypeUnknown)) {
        %child.scRequest = 0 @ new M3UDemuxer("");;
        if (isObject(MissionCleanup)) {
            MissionCleanup.add(%child.scRequest);
        }
        %child.scRequest.bindClassName("CSSCDataRequest");
        %child.scRequest.control = %child;
        %child.scRequest.Display = %window;
        %child.URLtypeUnknown = 0;
        %child.scRequest.setURL(%window.getMediaLink(%child));
        %child.scRequest.start();
        schedule(%this, "delete", 0);
        return;
    }
    %medialink = %window.getMediaLink(%child);
    %child.scRequest = "";
    %child.thumbnail.setBitmap($CSMediaDisplay::ShoutCastThumb);
    %child.mediatitle.setText($CSMediaDisplay::ShoutCastErrorTitle);
    %lastError = %this.getErrorBuffer();
    if ((%lastError $= "")) {
        %lastError = $CSMediaDisplay::ShoutCastErrorInfo;
    }
    %child.mediainfo.setText(%lastError);
    echo("Invalid Media URL: " @ %window.getMediaLink(%child) @ " Error: " @ %lastError);
    %window.setPlaybuttonAvailable(%child, 0);
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
        %window.parseFeedNode(%child, %root);
        %child.changeCume = 5;
    }
    if ((%root.getValue() $= "entry")) {
        %window.parseEntryNode(%child, %root, 1);
        %child.changeCume = 2;
    }
    log("error", "media", "Root node is not an entry or feed tag");
    return;
    %window.buildYoutubeTitle(%child);
    %window.selectYoutubeThumbnails(%child, 1);
    %medialink = %window.getMediaLink(%child);
    if (!(%child.skipStatistics)) {
        csRequestMediaStatistics(%medialink);
    }
    if ((1.0 == %child.autoplay)) {
        %child.autoplay = 0;
        %child.isPlaying = 1;
        if (%child.isPlaying) {
            CSMediaDisplay.stopAllMedia();
        }
        CSMediaDisplay.PlayButtonPushed(%child);
    }
    csSaveMediaFavorites();
};
function CSMediaDisplay::parseEntryNode(%this, %child, %entryNode, %setTitle) {
    %MediaGroup = %entryNode.getFirstChild("media:group");
    if (%setTitle) {
        %AuthorNode = %entryNode.getFirstChild("author");
        %AuthorNameNode = %AuthorNode.getFirstChild("name");
        %child.AuthorName = %AuthorNameNode.getText();
        %TitleNode = %MediaGroup.getFirstChild("media:title");
        %child.title = %TitleNode.getText();
    }
    %ThumbnailIdx = %child.thumbCount;
    %ThumbnailNode = %MediaGroup.getFirstChild("media:thumbnail");
    if (%ThumbnailNode) {
        %child.thumbURL = %ThumbnailNode.getAttribute("url") @ %ThumbnailIdx;
        %ThumbnailIdx = (1.0 + %ThumbnailIdx);
        %ThumbnailNode = %ThumbnailNode.getNext("media:thumbnail");
    }
    %child.thumbCount = %ThumbnailNode @ %ThumbnailIdx;
};
function CSMediaDisplay::parseFeedNode(%this, %child, %feedNode) {
    %AuthorNode = %feedNode.getFirstChild("author");
    %AuthorNode = %AuthorNode.getFirstChild("name");
    %child.AuthorName = %AuthorNode.getText();
    %MediaGroup = %feedNode.getFirstChild("media:group");
    %TitleNode = %MediaGroup.getFirstChild("media:title");
    %child.title = %TitleNode.getText();
    %entry = %feedNode.getFirstChild("entry");
    if (%entry) {
        %this.parseEntryNode(%child, %entry, 0);
        %entry = %entry.getNext("entry");
    }
};
function CSMDGDataRequest::onError(%this) {
    %child = %this.control;
    %window = %this.Display;
    %child.gdataRequest = "";
    %child.thumbnail.setBitmap($CSMediaDisplay::YoutubeErrorThumb);
    %child.mediatitle.setText($CSMediaDisplay::YoutubeErrorTitle);
    %child.mediainfo.setText($CSMediaDisplay::YoutubeErrorInfo);
    %window.setPlaybuttonAvailable(%child, 0);
    schedule(%this, "delete", 0);
};
function CSMediaDisplay::getChildDisplay(%this, %childIdx) {
    %faveCount = CSMediaFavDisplayArray.getCount();
    if ((%childIdx > %faveCount)) {
        return CSMediaFavDisplayArray.getObject(%childIdx);
    }
    if ((CSMediaFavDisplayArray.getCount() < (%faveCount - %childIdx))) {
        return CSMediaHotDisplayArray.getObject((%faveCount - %childIdx));
    }
    return "";
};
function CSMediaDisplay::getChildCount(%this) {
    return (CSMediaHotDisplayArray.getCount() + CSMediaFavDisplayArray.getCount());
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
        if ((%child.thumbCount < %idx)) {
            %child.thumbURL = "" @ %idx;
            %idx = (1.0 + %idx);
        }
    }
    %child.thumbCount = (%child.thumbCount < %idx) @ "";
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
        position = %xPos @ " " @ (1.0 + %ypos);
        extent = "60 45";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
    };
    %bmpWidth = getWord(%bitmap.getExtent(), 0);
    %bmpHeight = getWord(%bitmap.getExtent(), 1);
    %bitmap.setBitmap($CSMediaDisplay::YoutubeDefaultThumb);
    %xPos = ((%padding + %bmpWidth) + %xPos);
    %windowWidth = (%bmpWidth - %windowWidth);
    %textTitle = new GuiMLTextCtrl("") {
        profile = 0 @ "GuiMessageTextProfile";
        horizSizing = "width";
        vertSizing = "bottom";
        position = %xPos @ " " @ %ypos;
        extent = (%padding - %windowWidth) @ " " @ 14;
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
    %ypos = ((%padding + 13.0) + %ypos);
    %textEntry = new GuiTextEditCtrl("") {
        profile = 0 @ %child.isReadOnly ? "ETSDarkReadonlyTextEditProfile" : "ETSDarkTextEditProfile";
        horizSizing = "center";
        vertSizing = "top";
        position = %xPos @ " " @ %ypos;
        extent = (1.0 - (%padding - %windowWidth)) @ " " @ 18;
        minExtent = "8 8";
        visible = 1;
        setFirstResponder = 0;
        altCommand = %this @ ".changeMediaLink(" @ %child @ ");";
        validate = %this @ ".changeMediaLink(" @ %child @ ");";
        helpTag = 0;
        historySize = 0;
        readOnly = %child.isReadOnly;
    };
    %ypos = ((%padding + 16.0) + %ypos);
    %textInfo = new GuiMLTextCtrl("") {
        profile = 0 @ "GuiMessageTextProfile";
        horizSizing = "width";
        vertSizing = "bottom";
        position = %xPos @ " " @ %ypos;
        extent = ((%authorWidth + (2.0 * %padding)) - %windowWidth) @ " " @ 14;
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
    %xPos = ((getWord(%textInfo.extent, 0) + %padding) + %xPos);
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
    %textAuthor.bindClassName("CSMediaMLText");
    %ypos = ((%padding + 14.0) + %ypos);
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
    %bitmap.setBitmap($CSMediaDisplay::RadioBunnyThumb);
    %xPos = ((%padding + %bmpWidth) + %xPos);
    %windowWidth = (%bmpWidth - %windowWidth);
    %textTitle = new GuiMLTextCtrl("") {
        profile = 0 @ "GuiMessageTextProfile";
        horizSizing = "width";
        vertSizing = "bottom";
        position = %xPos @ " " @ %ypos;
        extent = (%padding - %windowWidth) @ " " @ 14;
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
    %ypos = ((%padding + 14.0) + %ypos);
    %dropdown = new GuiPopUp2MenuCtrl(CSMediaMusicStreamPopup) {
        profile = "InfoWindowPopupProfile";
        scrollProfile = "DottedScrollProfile";
        winProfile = "InfoWindowPopupWindowProfile";
        horizSizing = "right";
        vertSizing = "bottom";
        position = %xPos @ " " @ %ypos;
        extent = (2.0 - (%padding - %windowWidth)) @ " " @ 30;
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
    %playButton.bindClassName("CSBitmapButton");
    %bitmap.add(%playButton);
    %child.add(%bitmap);
    %child.add(%textTitle);
    %child.add(%dropdown);
    if (isObject($musicStreamNameMap)) {
        %count = $musicStreamNameMap.size();
    }
    %count = 0;
    %idx = 0;
    if ((%count < %idx)) {
        %dropdown.add($musicStreamNameMap.getKey(%idx));
        %idx = (1.0 + %idx);
    }
    if (%child.isReadOnly) {
        (%count < %idx);
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
        position = %xPos @ " " @ (1.0 + %ypos);
        extent = "60 45";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
    };
    %bmpWidth = getWord(%bitmap.getExtent(), 0);
    %bmpHeight = getWord(%bitmap.getExtent(), 1);
    %bitmap.setBitmap($CSMediaDisplay::RadioBunnyThumb);
    %xPos = ((%padding + %bmpWidth) + %xPos);
    %windowWidth = (%bmpWidth - %windowWidth);
    %textTitle = new GuiMLTextCtrl("") {
        profile = 0 @ "GuiMessageTextProfile";
        horizSizing = "width";
        vertSizing = "bottom";
        position = %xPos @ " " @ %ypos;
        extent = (%padding - %windowWidth) @ " " @ 14;
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
    %ypos = ((%padding + 13.0) + %ypos);
    %textEntry = new GuiTextEditCtrl("") {
        profile = 0 @ %child.isReadOnly ? "ETSDarkReadonlyTextEditProfile" : "ETSDarkTextEditProfile";
        horizSizing = "center";
        vertSizing = "top";
        position = %xPos @ " " @ %ypos;
        extent = (1.0 - (%padding - %windowWidth)) @ " " @ 18;
        minExtent = "8 8";
        visible = 1;
        setFirstResponder = 0;
        altCommand = %this @ ".changeMediaLink(" @ %child @ ");";
        validate = %this @ ".changeMediaLink(" @ %child @ ");";
        helpTag = 0;
        historySize = 0;
        readOnly = %child.isReadOnly;
    };
    %ypos = ((%padding + 16.0) + %ypos);
    %textInfo = new GuiMLTextCtrl("") {
        profile = 0 @ "GuiMessageTextProfile";
        horizSizing = "width";
        vertSizing = "bottom";
        position = %xPos @ " " @ %ypos;
        extent = ((%authorWidth + (2.0 * %padding)) - %windowWidth) @ " " @ 14;
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
    %xPos = ((getWord(%textInfo.extent, 0) + %padding) + %xPos);
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
    %textAuthor.bindClassName("CSMediaMLText");
    %ypos = ((%padding + 14.0) + %ypos);
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
    %bitmap.setBitmap($CSMediaDisplay::EmptyBunnyThumb);
    %xPos = ((%padding + %bmpWidth) + %xPos);
    %windowWidth = (%bmpWidth - %windowWidth);
    %textTitle = new GuiMLTextCtrl("") {
        profile = 0 @ "GuiMessageTextProfile";
        horizSizing = "width";
        vertSizing = "bottom";
        position = %xPos @ " " @ %ypos;
        extent = (%padding - %windowWidth) @ " " @ 14;
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
    %ypos = ((%padding + 14.0) + %ypos);
    %textEntry = new GuiTextEditCtrl("") {
        profile = 0 @ "ETSDarkTextEditProfile";
        horizSizing = "center";
        vertSizing = "top";
        position = %xPos @ " " @ %ypos;
        extent = (1.0 - (%padding - %windowWidth)) @ " " @ 18;
        minExtent = "8 8";
        visible = 1;
        setFirstResponder = 0;
        altCommand = %this @ ".changeMediaLink(" @ %child @ ");";
        validate = %this @ ".changeMediaLink(" @ %child @ ");";
        helpTag = 0;
        historySize = 0;
        canHilite = 1;
    };
    %ypos = ((%padding + 18.0) + %ypos);
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
};
function CSMediaMLText::onMouseDragged(%this) {
    %parent = %this.getParent();
    if ((-(1.0) != findWord(%parent.getNamespaceList(), "CSMediaHotListItem"))) {
        %parent.setAsDragControl(1);
        return 1;
    }
    return 0;
};
function CSBitmapButton::onMouseDown(%this) {
    %this.origin = Canvas.getCursorPos();
};
function CSBitmapButton::onMouseDragged(%this) {
    %parent = %this.getParent().getParent();
    if ((-(1.0) == findWord(%parent.getNamespaceList(), "CSMediaHotListItem"))) {
        return 0;
    }
    %vec = VectorSub(%this.origin, Canvas.getCursorPos());
    if (((12.0 * 12.0) < VectorLenSquared(%vec))) {
        return 0;
    }
    %parent.setAsDragControl(1);
    return 1;
};
function CSMediaHotListItem::onMouseDragged(%this) {
    %this.setAsDragControl(1);
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
        position = %xPos @ " " @ (1.0 + %ypos);
        extent = "60 45";
        minExtent = "1 1";
        sluggishness = -1;
        visible = 1;
    };
    %ctrl.add(%bitmap);
    %bitmap.setBitmap(%this.getObject(0).getBitmap());
    %bmpWidth = getWord(%bitmap.getExtent(), 0);
    %bmpHeight = getWord(%bitmap.getExtent(), 1);
    %xPos = ((%padding + %bmpWidth) + %xPos);
    %windowWidth = (%bmpWidth - %windowWidth);
    %textTitle = new GuiMLTextCtrl("") {
        profile = 0 @ "GuiMessageTextProfile";
        horizSizing = "width";
        vertSizing = "bottom";
        position = %xPos @ " " @ %ypos;
        extent = (%padding - %windowWidth) @ " " @ 14;
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
    %ypos = ((%padding + 13.0) + %ypos);
    %ctrl.add(%textTitle);
    %ctrl.mediatitle = %textTitle;
    %ctrl.AuthorName = %this.AuthorName;
    %ctrl.title = %this.title;
    CSMediaDisplay.buildYoutubeTitle(%ctrl);
    return %ctrl;
};
function CSMediaFavListItem::onDragAndDropEnter(%this, %dragCtrl) {
    if ((-(1.0) == findWord(%dragCtrl.getNamespaceList(), "CSMediaHotListItem"))) {
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
    %url = CSMediaDisplay.getMediaLink(%dragCtrl);
    if ((%url $= "")) {
        return 0;
    }
    %this.autoplay = 1;
    CSMediaDisplay.updateMediaLinkTo(%this, %url, 1);
    return 1;
};
function CSMediaFavListItem::onSystemDragDropEvent(%this, %text, %eventType, %pt) {
    if (!(isURL(%text))) {
        return 0;
    }
    if (($CSMediaDisplay::TypeRadio == %this.displayType)) {
        return 0;
    }
    if (!(Parent::onSystemDragDropEvent(%this, %text, %eventType, %pt))) {
        return 0;
    }
    hiliteControl(%this.medialink);
    if ((%eventType $= "BREAK")) {
        %this.autoplay = 1;
        if (isObject(%this.mediainfo)) {
            %this.mediainfo.setText("Retrieving stream info...");
        }
        CSMediaDisplay.updateMediaLinkTo(%this, %text, 0);
    }
    return 1;
};
