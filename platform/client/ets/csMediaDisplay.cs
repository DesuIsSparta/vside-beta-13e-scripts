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
    %this.focusAndRaise();
    update();
    CustomSpaceClient::checkEditingSpace();
    if (!(%this SPC periodic $= "")) {
        cancel(periodic);
        periodic = %this @ "" @ %this;
        WindowManager;
    }
    %this.syncPlayingMediaStream(playingStream);
    %this.periodicCycle();
    %this.update();
};
function CSMediaDisplay::close(%this) {
    %this.setVisible(0);
    CustomSpaceClient::checkEditingSpace();
    focusTopWindow();
    update();
    if (!(%this SPC periodic $= "")) {
        cancel(periodic);
        periodic = %this @ "" @ %this;
        WindowManager;
    }
    return 1;
};
function CSMediaDisplay::periodicCycle(%this) {
    cancel(periodic);
    %this.cycleYoutubeThumbnails();
    periodic = periodicCycle @ %this.schedule(4000) @ %this;
    %this;
};
function CSMediaDisplay::update(%this) {
};
function CSMediaDisplay::Initialize() {
    if (!(CSMediaDisplay SPC initialized $= "")) {
    }
    if ((CSMediaDisplay == initialized)) {
        return 1.0;
    }
    %newExtent = getWord(childrenExtent, 0) @ " " @ 51;
    CSMediaFavDisplayArray;
    childrenExtent = %newExtent @ CSMediaFavDisplayArray;
    childrenExtent = %newExtent @ CSMediaHotDisplayArray;
    $CSMediaDisplay::DefaultFavoriteCount.setNumChildren();
    $CSMediaDisplay::DefaultFavoriteCount.setNumChildren();
    visible = CSMediaHotDisplayArray @ 0 @ CSMediaWhatsHotSelector;
    CSMediaFavDisplayArray;
    playingChild = 0 @ CSMediaDisplay;
    playingStream = "" @ CSMediaDisplay;
    showingWhatsHot = 0 @ CSMediaDisplay;
    initialized = 1 @ CSMediaDisplay;
};
function CSMediaFavDisplayArray::onCreatedChild(%this, %child, %unused, %y) {
    if ((0.0 > %y)) {
        %child.setProfile();
    }
    %child.setProfile();
    isReadOnly = GuiDefaultProfile @ 0 @ %child;
    ETSDroppableProfile;
    systemDragDrop = 1 @ %child;
    if ((1.0 == %this.getCount())) {
        %child.buildChildDisplayRadio();
    }
    %child.buildChildDisplayEmpty();
    forceRadio = CSMediaDisplay @ (1.0 == %this.getCount()) @ %child;
    CSMediaDisplay;
    visible = 1 @ %child;
    oldMediaLink = "" @ %child;
    if (!(getWord(%child.getNamespaceList(), 0) $= "CSMediaFavListItem")) {
        %child.bindClassName("CSMediaFavListItem");
    }
};
function CSMediaHotDisplayArray::onCreatedChild(%this, %child) {
    isReadOnly = 1 @ %child;
    %child.buildChildDisplayEmpty();
    forceRadio = CSMediaDisplay @ 0 @ %child;
    visible = 0 @ %child;
    oldMediaLink = "" @ %child;
    if (!(getWord(%child.getNamespaceList(), 0) $= "CSMediaHotListItem")) {
        echo("Binding CSMediaHotListItem to " @ %child);
        %child.bindClassName("CSMediaHotListItem");
    }
};
function CSMediaDisplay::buttonWhatsHot(%this) {
    %widthDelta = (CSMediaWhatsHotSelector + getWord(getExtent(), 0));
    3.0;
    %width = getWord(%this.getExtent(), 0);
    %height = getWord(%this.getExtent(), 1);
    if (showingWhatsHot) {
        text = CSMediaDisplay @ " What's Hot >> " @ CSMediaWhatsHotButton;
        "352 24".reposition();
        %widthDelta = (-(1.0) * %widthDelta);
        CSMediaWhatsHotButton;
    }
    csRequestHotMedia();
    text = " << Hide " @ CSMediaWhatsHotButton;
    "423 24".reposition();
    showingWhatsHot = %this @ !(showingWhatsHot) @ %this;
    CSMediaWhatsHotButton;
    %this.setTrgExtent((%widthDelta + %width), %height);
    visible = %this @ showingWhatsHot @ CSMediaWhatsHotSelector;
};
function CSMediaDisplay::onReachedTarget(%this) {
    update();
};
function CSMediaDisplay::setMediaFavorites(%this, %mediaList) {
    %this.setMediaList(%mediaList, 0, getCount(), 0);
};
function CSMediaDisplay::setMediaHotlist(%this, %mediaList) {
    %this.setMediaList(%mediaList, $CSMediaDisplay::DefaultFavoriteCount, getCount(), 1);
};
function CSMediaDisplay::setMediaList(%this, %mediaList, %startIdx, %maxIdx, %hideEmpty) {
    %count = getFieldCount(%mediaList);
    %idx = 0;
    if ((%count < %idx)) {
        %linkInfo = getField(%mediaList, %idx);
        %linkName = getWord(%linkInfo, 0);
        %child = %this.getChildDisplay((%startIdx + %idx));
        visible = 1 @ %child;
        %infoCount = getWordCount(%linkInfo);
        %this.updateMediaLinkTo(%child, %linkName, (2.0 > %infoCount));
        if ((1.0 > %infoCount)) {
            %this.setMediaInfo(%child, getWord(%linkInfo, 1), getWord(%linkInfo, 2));
        }
        %streamID = %this.extractMediaStreamId(%linkName);
        if ((%this SPC playingStream $= %linkName)) {
            playingChild = %child @ %this;
            visible = %child @ playButton;
            0;
        }
        if (!(%streamID $= "")) {
            %urlinfo = new ""();
            ScriptObject;
            if (isObject()) {
                %urlinfo.add();
            }
            %urlinfo.bindClassName("URLInfo");
            url = %this @ playingStream @ %urlinfo;
            MissionCleanup;
            %tStreamInfo = MissionCleanup @ %streamID @ ".ogg";
            0;
            if (!(%urlinfo.parse())) {
            }
            if ((%urlinfo SPC Path $= %tStreamInfo)) {
                playingChild = %child @ %this;
                visible = %child @ playButton;
                0;
                playingStream = %linkName @ %this;
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
            visible = 0 @ %child;
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
    if (!(%child SPC mediainfo $= "")) {
        %text = "<color:ffffff70>" @ %plays @ " plays";
        mediainfo.setText(%text);
    }
};
function CSMediaDisplay::setNoMediaInfo(%this, %child) {
    if (!(%child SPC mediainfo $= "")) {
        mediainfo.setText("(no stats)");
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
    playingStream = %medialink @ %this;
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
    %url = new ""();
    ScriptObject;
    if (isObject()) {
        %url.add();
    }
    %url.bindClassName("URLInfo");
    url = MissionCleanup @ %medialink @ %url;
    MissionCleanup;
    %streamID = "";
    0;
    if (%url.parse()) {
        if ((%url == stricmp(protocol, "vside"))) {
        }
        if ((%url == stricmp(host, "radio"))) {
            %streamID = Path;
            %url;
        }
    }
    %url.delete();
    return %streamID;
};
function CSMediaDisplay::PlayButtonPushed(%this, %child) {
    %this.setPlayingChild(%child);
    if (!(%child SPC streamUrl $= "")) {
        %this.playMediaStream(streamUrl, playingStream);
    }
    %this.playMediaStream(playingStream);
};
function CSMediaDisplay::setPlayingChild(%this, %child) {
    %count = %this.getChildCount();
    %idx = 0;
    if ((%count < %idx)) {
        %otherChild = %this.getChildDisplay(%idx);
        if ((%otherChild != %child)) {
            isPlaying = 0 @ %otherChild;
            if (!(%otherChild SPC highlight $= "")) {
            }
            if (isObject(highlight)) {
                %otherChild.remove(highlight);
                highlight.delete();
                highlight = %otherChild @ "" @ %otherChild;
                %otherChild;
            }
            %this.setPlaybuttonVisible(%otherChild, 1);
        }
        %idx = (1.0 + %idx);
        %otherChild;
    }
    %medialink = "";
    (%count < %idx);
    if ((0.0 != %child)) {
        isPlaying = 1 @ %child;
        if ((%child SPC highlight $= "")) {
            %extent = %child.getExtent();
            profile = GuiConvBubbleCtrl @ new ""() @ "ETSLightHighlightProfile";
            0;
            extent = getWord(%extent, 0) @ " " @ (4.0 - getWord(%extent, 1));
            position = "0 0";
            roundRadius = 4;
            roundInterps = 2;
            sluggishness = -(1.0);
            highlight = %child;
            %child.add(highlight);
        }
        %this.setPlaybuttonVisible(%child, 0);
        %medialink = %this.getMediaLink(%child);
        %child;
    }
    playingStream = %medialink @ %this;
    playingChild = %child @ %this;
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
        if ((%child SPC streamUrl $= %medialink)) {
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
    window.resize(550, 300);
};
function CSMediaDisplay::getMediaLink(%this, %child) {
    if ((%child SPC medialink $= "")) {
        return "";
    }
    if ((%child != displayType)) {
        %medialinkValue = medialink.getText();
        %child;
    }
    %medialinkName = medialink.getText();
    %child;
    %idx = -(1.0);
    $CSMediaDisplay::TypeRadio;
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
    if ((%child SPC medialink $= "")) {
        return;
    }
    if ((%child == displayType)) {
        %validate = validate;
        medialink;
        validate = %child @ medialink;
        %child @ "";
        medialink.setText(%medialink);
        validate = %child @ medialink;
        %child @ %validate;
        skipStatistics = $CSMediaDisplay::TypeYoutube @ %skipStatistics @ %child;
        %this.requestYoutubeInfo(%child);
        %this.setPlaybuttonAvailable(%child, 1);
        oldMediaLink = %medialink @ %child;
    }
    if ((%child == displayType)) {
        %validate = validate;
        medialink;
        validate = %child @ medialink;
        %child @ "";
        medialink.setText(%medialink);
        validate = %child @ medialink;
        %child @ %validate;
        skipStatistics = $CSMediaDisplay::TypeShoutCast @ %skipStatistics @ %child;
        %this.requestShoutCastInfo(%child);
        %this.setPlaybuttonAvailable(%child, 1);
        oldMediaLink = %medialink @ %child;
    }
    %url = new ""();
    ScriptObject;
    if (isObject()) {
        %url.add();
    }
    %url.bindClassName("URLInfo");
    url = MissionCleanup @ %medialink @ %url;
    MissionCleanup;
    %url.parse();
    %path = Path;
    %url;
    %url.delete();
    %count = 0;
    0;
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
        %index = medialink.findText(%newStreamName);
        %child;
        medialink.SetSelected(%index);
    }
    medialink.setText(%path);
    %this.setPlaybuttonAvailable(%child, !(%child SPC %path $= "-"));
};
function CSMediaDisplay::setPlaybuttonAvailable(%this, %child, %avail) {
    playbuttonAvailable = %avail @ %child;
    if (!(%child SPC playButton $= "")) {
        if (!(%avail)) {
            visible = %child @ playButton;
            0;
        }
        visible = %child @ playButton;
        %child @ !(isPlaying);
    }
};
function CSMediaDisplay::setPlaybuttonVisible(%this, %child, %visible) {
    if (playbuttonAvailable) {
    }
    if (!(%child SPC playButton $= "")) {
        visible = %child @ playButton;
        %child @ %visible;
    }
};
function CSMediaDisplay::changeMediaLink(%this, %child) {
    %this.schedule(0, "changeMediaLinkReally", %child);
};
function CSMediaDisplay::changeMediaLinkReally(%this, %child) {
    %newMediaType = %this.getMediaType(medialink);
    %child;
    %newMediaLink = %this.getMediaLink(%child);
    if ((%child SPC oldMediaLink $= %newMediaLink)) {
        return;
    }
    %this.updateMediaLinkTo(%child, %newMediaLink, 0);
    %newMediaType = %this.getMediaType(%newMediaLink);
    if ((%this == playingChild)) {
        if (($CSMediaDisplay::TypeRadio == %newMediaType)) {
            if ((%child SPC %newMediaLink $= "vside://radio/-")) {
                %newMediaLink = "";
                playingChild = 0 @ %this;
                isPlaying = 0 @ %child;
            }
        }
        if (($CSMediaDisplay::TypeShoutCast == %newMediaType)) {
            autoplay = 1 @ %child;
        }
        %this.playMediaStream(%newMediaLink);
        playingStream = %newMediaLink @ %this;
    }
    if (($CSMediaDisplay::TypeRadio == %newMediaType)) {
    }
    if ((%newMediaLink $= "")) {
        csSaveMediaFavorites();
    }
};
function CSMediaDisplay::updateMediaLinkTo(%this, %child, %newMediaLink, %skipStats) {
    streamUrl = "" @ %child;
    %newMediaType = %this.getMediaType(%newMediaLink);
    if (($CSMediaDisplay::TypeRadio == %newMediaType)) {
        %url = new ""();
        ScriptObject;
        if (isObject()) {
            %url.add();
        }
        %url.bindClassName("URLInfo");
        url = MissionCleanup @ %newMediaLink @ %url;
        MissionCleanup;
        %url.parse();
        %path = Path;
        %url;
        %url.delete();
        if ((0 SPC %path $= "-")) {
        }
        if (!(forceRadio)) {
            %newMediaType = $CSMediaDisplay::TypeEmpty;
            %child;
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
    if ((displayType != %newMediaType)) {
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
    %url = new ""();
    ScriptObject;
    if (isObject()) {
        %url.add();
    }
    %url.bindClassName("URLInfo");
    url = MissionCleanup @ %medialink @ %url;
    MissionCleanup;
    if (!(%url.parse())) {
        %url.delete();
        return $CSMediaDisplay::TypeEmpty;
    }
    %type = $CSMediaDisplay::TypeEmpty;
    if ((%url == stricmp(protocol, "vside"))) {
        if ((%url == stricmp(host, "radio"))) {
            %type = $CSMediaDisplay::TypeRadio;
            0.0;
        }
    }
    if ((%url == stricmp(protocol, "http"))) {
        if ((%url >= strstr(host, "youtube."))) {
            %type = $CSMediaDisplay::TypeYoutube;
            0.0;
        }
        %type = $CSMediaDisplay::TypeShoutCast;
        0.0;
    }
    debug(0.0 @ "Unknown media type: " @ %medialink);
    %url.delete();
    return %type;
};
function CSMediaDisplay::updateRadioStreams(%this) {
    %count = %this.getChildCount();
    %idx = 0;
    if ((%count < %idx)) {
        %child = %this.getChildDisplay(%idx);
        if ((%child == displayType)) {
            %this.updateRadioDropDown(%child);
        }
        %idx = (1.0 + %idx);
        $CSMediaDisplay::TypeRadio;
    }
};
function CSMediaDisplay::updateRadioDropDown(%this, %child) {
    %medialink = %this.getMediaLink(%child);
    %dropdown = medialink;
    %child;
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
        if ((%child == displayType)) {
            %this.selectYoutubeThumbnails(%child, 0);
        }
        %idx = (1.0 + %idx);
        $CSMediaDisplay::TypeYoutube;
    }
};
function CSMediaDisplay::buildYoutubeTitle(%this, %child) {
    %AuthorName = "";
    %title = "YouTube Video";
    if (!(%child SPC AuthorName $= "")) {
        %AuthorName = AuthorName;
        %child;
    }
    if (!(%child SPC title $= "")) {
        %title = title;
        %child;
    }
    %FullTitle = "";
    if (!(%AuthorName $= "")) {
        %authorString = "<just:right><color:ffffff80><linkcolorhl:ffaaff><a:gamelink " @ $CSMediaDisplay::YoutubeProfile @ %AuthorName @ ">";
        %authorString = %authorString @ %AuthorName @ "</a>";
        if (!(%child SPC mediaauthor $= "")) {
            mediaauthor.setText(%authorString);
        }
    }
    %FullTitle = %child @ "<spush><b>" @ %title @ "<spop>" @ %FullTitle;
    %FullTitle = "<clip:" @ %child @ mediatitle @ getWord(extent, 0) @ ">" @ %FullTitle @ "</clip>";
    mediatitle.setText(%FullTitle);
};
function CSMediaDisplay::buildShoutCastTitle(%this, %child) {
    %AuthorName = "";
    %title = "ShoutCast Stream";
    %homeURL = "";
    if (!(%child SPC AuthorName $= "")) {
        %AuthorName = AuthorName;
        %child;
    }
    if (!(%child SPC title $= "")) {
        %title = title;
        %child;
    }
    if (!(%child SPC homeURL $= "")) {
        %homeURL = homeURL;
        %child;
    }
    if (!(%homeURL $= "")) {
        %FullTitle = "<a:gamelink " @ %homeURL @ "/><clip:" @ %child @ mediatitle @ getWord(extent, 0) @ ">" @ %title @ "</clip></a>";
    }
    %FullTitle = "<clip:" @ %child @ mediatitle @ getWord(extent, 0) @ ">" @ %title @ "</clip>";
    mediatitle.setText(%FullTitle);
};
function CSMediaDisplay::selectYoutubeThumbnails(%this, %child, %force) {
    if (!(%force)) {
        %chance = getRandom(0, 99);
        if ((changeThumbChance > %chance)) {
            changeThumbChance = changeCume @ (%child + changeThumbChance) @ %child;
            %child;
            return %child;
        }
    }
    changeThumbChance = %child @ changeCume @ %child;
    %index = getRandom(0, (%child - thumbCount));
    1.0;
    currentThumb = %index @ %child;
    if (!(%child @ currentThumb @ %child SPC thumbURL $= "")) {
        thumbnail.downloadAndApplyBitmap(thumbURL, "youtube");
    }
};
function CSMediaDisplay::normalizeYoutubeURL(%medialink) {
    %url = new ""();
    ScriptObject;
    if (isObject()) {
        %url.add();
    }
    %url.bindClassName("URLInfo");
    url = MissionCleanup @ %medialink @ %url;
    MissionCleanup;
    %url.parse();
    %urlOut = 0 @ "http://" @ %url @ host @ "/";
    if (!("v" @ %url SPC param $= "")) {
        %urlOut = %urlOut @ "watch?v=" @ "v" @ %url @ param;
    }
    if (!("p" @ %url SPC param $= "")) {
        %urlOut = %urlOut @ "view_play_list?p=" @ "p" @ %url @ param;
    }
    %url.delete();
    return "";
    %url.delete();
    return %urlOut;
};
function CSMediaDisplay::requestYoutubeInfo(%this, %child) {
    if (!(%child SPC gdataRequest $= "")) {
        gdataRequest.delete();
    }
    title = %child @ "" @ %child;
    AuthorName = "" @ %child;
    if (!(%child SPC thumbCount $= "")) {
        %idx = 0;
        if ((thumbCount < %idx)) {
            thumbURL = %child @ "" @ %idx @ %child;
            %idx = (1.0 + %idx);
        }
    }
    thumbCount = (thumbCount < %idx) @ "" @ %child;
    %child;
    %url = new ""();
    ScriptObject;
    if (isObject()) {
        %url.add();
    }
    %url.bindClassName("URLInfo");
    url = MissionCleanup @ %this.getMediaLink(%child) @ %url;
    MissionCleanup;
    %url.parse();
    %gdataRequest = $CSMediaDisplay::GDataAPIURL;
    0;
    if (!("v" @ %url SPC param $= "")) {
        %gdataRequest = %gdataRequest @ $CSMediaDisplay::GDataAPIVideoInfo @ "v" @ %url @ param;
    }
    if (!("p" @ %url SPC param $= "")) {
        %gdataRequest = %gdataRequest @ $CSMediaDisplay::GDataAPIPlaylistInfo @ "p" @ %url @ param;
    }
    return;
    gdataRequest = XMLDoc @ new ""() @ %child;
    0;
    if (isObject()) {
        gdataRequest.add();
    }
    gdataRequest.bindClassName("CSMDGDataRequest");
    control = %child @ gdataRequest;
    %child @ %child;
    Display = %child @ gdataRequest;
    %child @ %this;
    gdataRequest.parseXMLFromURL(%gdataRequest);
};
function CSMediaDisplay::requestShoutCastInfo(%this, %child) {
    if (!(%child SPC scRequest $= "")) {
        scRequest.delete();
    }
    title = %child @ "" @ %child;
    AuthorName = "" @ %child;
    thumbCount = "" @ %child;
    %url = %this.getMediaLink(%child);
    if ((0.0 > strstr(%url, ".mp3"))) {
        scRequest = M3UDemuxer @ new ""() @ %child;
        0;
    }
    if ((0.0 > strstr(%url, ".pls"))) {
        scRequest = PLSDemuxer @ new ""() @ %child;
        0;
        URLtypeUnknown = 1 @ %child;
    }
    if ((0.0 > strstr(%url, ".m3u"))) {
        scRequest = M3UDemuxer @ new ""() @ %child;
        0;
    }
    scRequest = PLSDemuxer @ new ""() @ %child;
    0;
    URLtypeUnknown = 1 @ %child;
    if (isObject()) {
        scRequest.add();
    }
    scRequest.bindClassName("CSSCDataRequest");
    control = %child @ scRequest;
    %child @ %child;
    Display = %child @ scRequest;
    %child @ %this;
    scRequest.setURL(%this.getMediaLink(%child));
    scRequest.start();
};
function CSSCDataRequest::onDone(%this, %url) {
    %child = control;
    %this;
    %window = Display;
    %this;
    scRequest = "" @ %child;
    streamUrl = %url @ %child;
    title = %this.getTitle() @ %child;
    homeURL = %this.getHomeURL() @ %child;
    %window.buildShoutCastTitle(%child);
    mediainfo.setText("");
    thumbnail.setBitmap($CSMediaDisplay::ShoutCastThumb);
    schedule(%this, "delete", 0);
    %medialink = %window.getMediaLink(%child);
    %child;
    if (!(skipStatistics)) {
        csRequestMediaStatistics(%medialink);
    }
    if ((%child == autoplay)) {
        autoplay = 1.0 @ 0 @ %child;
        %child;
        isPlaying = %child @ 1 @ %child;
        if (isPlaying) {
            stopAllMedia();
        }
        %child.PlayButtonPushed();
    }
    csSaveMediaFavorites();
};
function CSSCDataRequest::onError(%this) {
    %child = control;
    %this;
    %window = Display;
    %this;
    if ((%child == URLtypeUnknown)) {
        scRequest = M3UDemuxer @ new ""() @ %child;
        0;
        if (isObject()) {
            scRequest.add();
        }
        scRequest.bindClassName("CSSCDataRequest");
        control = %child @ scRequest;
        %child @ %child;
        Display = %child @ scRequest;
        %child @ %window;
        URLtypeUnknown = MissionCleanup @ 0 @ %child;
        MissionCleanup;
        scRequest.setURL(%window.getMediaLink(%child));
        scRequest.start();
        schedule(%this, "delete", 0);
        return %child;
    }
    %medialink = %window.getMediaLink(%child);
    scRequest = "" @ %child;
    thumbnail.setBitmap($CSMediaDisplay::ShoutCastThumb);
    mediatitle.setText($CSMediaDisplay::ShoutCastErrorTitle);
    %lastError = %this.getErrorBuffer();
    %child;
    if ((%child SPC %lastError $= "")) {
        %lastError = $CSMediaDisplay::ShoutCastErrorInfo;
    }
    mediainfo.setText(%lastError);
    echo(%child @ "Invalid Media URL: " @ %window.getMediaLink(%child) @ " Error: " @ %lastError);
    %window.setPlaybuttonAvailable(%child, 0);
    schedule(%this, "delete", 0);
};
function CSMDGDataRequest::onDone(%this) {
    %child = control;
    %this;
    %window = Display;
    %this;
    gdataRequest = "" @ %child;
    %root = %this.getRootElement();
    schedule(%this, "delete", 0);
    if (!(%root)) {
        log("error", "No Root element in returned XML...");
        return;
    }
    thumbCount = 0 @ %child;
    if ((%root.getValue() $= "feed")) {
        %window.parseFeedNode(%child, %root);
        changeCume = 5 @ %child;
    }
    if ((%root.getValue() $= "entry")) {
        %window.parseEntryNode(%child, %root, 1);
        changeCume = 2 @ %child;
    }
    log("error", "media", "Root node is not an entry or feed tag");
    return;
    %window.buildYoutubeTitle(%child);
    %window.selectYoutubeThumbnails(%child, 1);
    %medialink = %window.getMediaLink(%child);
    if (!(skipStatistics)) {
        csRequestMediaStatistics(%medialink);
    }
    if ((%child == autoplay)) {
        autoplay = 1.0 @ 0 @ %child;
        %child;
        isPlaying = 1 @ %child;
        if (isPlaying) {
            stopAllMedia();
        }
        %child.PlayButtonPushed();
    }
    csSaveMediaFavorites();
};
function CSMediaDisplay::parseEntryNode(%this, %child, %entryNode, %setTitle) {
    %MediaGroup = %entryNode.getFirstChild("media:group");
    if (%setTitle) {
        %AuthorNode = %entryNode.getFirstChild("author");
        %AuthorNameNode = %AuthorNode.getFirstChild("name");
        AuthorName = %AuthorNameNode.getText() @ %child;
        %TitleNode = %MediaGroup.getFirstChild("media:title");
        title = %TitleNode.getText() @ %child;
    }
    %ThumbnailIdx = thumbCount;
    %child;
    %ThumbnailNode = %MediaGroup.getFirstChild("media:thumbnail");
    if (%ThumbnailNode) {
        thumbURL = %ThumbnailNode.getAttribute("url") @ %ThumbnailIdx @ %child;
        %ThumbnailIdx = (1.0 + %ThumbnailIdx);
        %ThumbnailNode = %ThumbnailNode.getNext("media:thumbnail");
    }
    thumbCount = %ThumbnailNode @ %ThumbnailIdx @ %child;
};
function CSMediaDisplay::parseFeedNode(%this, %child, %feedNode) {
    %AuthorNode = %feedNode.getFirstChild("author");
    %AuthorNode = %AuthorNode.getFirstChild("name");
    AuthorName = %AuthorNode.getText() @ %child;
    %MediaGroup = %feedNode.getFirstChild("media:group");
    %TitleNode = %MediaGroup.getFirstChild("media:title");
    title = %TitleNode.getText() @ %child;
    %entry = %feedNode.getFirstChild("entry");
    if (%entry) {
        %this.parseEntryNode(%child, %entry, 0);
        %entry = %entry.getNext("entry");
    }
};
function CSMDGDataRequest::onError(%this) {
    %child = control;
    %this;
    %window = Display;
    %this;
    gdataRequest = "" @ %child;
    thumbnail.setBitmap($CSMediaDisplay::YoutubeErrorThumb);
    mediatitle.setText($CSMediaDisplay::YoutubeErrorTitle);
    mediainfo.setText($CSMediaDisplay::YoutubeErrorInfo);
    %window.setPlaybuttonAvailable(%child, 0);
    schedule(%this, "delete", 0);
};
function CSMediaDisplay::getChildDisplay(%this, %childIdx) {
    %faveCount = getCount();
    CSMediaFavDisplayArray;
    if ((%childIdx > %faveCount)) {
        return %childIdx.getObject();
    }
    if ((getCount() < (%faveCount - %childIdx))) {
        return (%faveCount - %childIdx).getObject();
    }
    return "";
};
function CSMediaDisplay::getChildCount(%this) {
    return (CSMediaFavDisplayArray + getCount());
};
function CSMediaDisplay::clearChildDisplay(%this, %child) {
    %child.deleteMembers();
    playButton = "" @ %child;
    playbuttonAvailable = 0 @ %child;
    thumbnail = "" @ %child;
    medialink = "" @ %child;
    mediatitle = "" @ %child;
    mediainfo = "" @ %child;
    mediaauthor = "" @ %child;
    title = "" @ %child;
    AuthorName = "" @ %child;
    if (!(%child SPC thumbCount $= "")) {
        %idx = 0;
        if ((thumbCount < %idx)) {
            thumbURL = %child @ "" @ %idx @ %child;
            %idx = (1.0 + %idx);
        }
    }
    thumbCount = (thumbCount < %idx) @ "" @ %child;
    %child;
};
function CSMediaDisplay::buildChildDisplayYouTube(%this, %child) {
    %padding = 1;
    %windowWidth = getWord(%child.getExtent(), 0);
    %authorWidth = 80;
    %xPos = 1;
    %ypos = 0;
    profile = GuiBitmapCtrl @ new ""() @ "ETSNonModalProfile";
    0;
    horizSizing = "right";
    vertSizing = "bottom";
    position = %xPos @ " " @ (1.0 + %ypos);
    extent = "60 45";
    minExtent = "1 1";
    sluggishness = -1;
    visible = 1;
    %bitmap = ;
    %bmpWidth = getWord(%bitmap.getExtent(), 0);
    %bmpHeight = getWord(%bitmap.getExtent(), 1);
    %bitmap.setBitmap($CSMediaDisplay::YoutubeDefaultThumb);
    %xPos = ((%padding + %bmpWidth) + %xPos);
    %windowWidth = (%bmpWidth - %windowWidth);
    profile = GuiMLTextCtrl @ new ""() @ "GuiMessageTextProfile";
    0;
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
    %textTitle = ;
    %textTitle.bindClassName("CSMediaMLText");
    %textTitle.setText("YouTube Video");
    %ypos = ((%padding + 13.0) + %ypos);
    profile = new ""() @ %child @ isReadOnly ? "ETSDarkReadonlyTextEditProfile" : "ETSDarkTextEditProfile";
    GuiTextEditCtrl;
    horizSizing = 0 @ "center";
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
    readOnly = %child @ isReadOnly;
    %textEntry = ;
    %ypos = ((%padding + 16.0) + %ypos);
    profile = GuiMLTextCtrl @ new ""() @ "GuiMessageTextProfile";
    0;
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
    %textInfo = ;
    %textInfo.bindClassName("CSMediaMLText");
    %textInfo.setText("Retrieving video info...");
    %xPos = ((getWord(extent, 0) + %padding) + %xPos);
    %textInfo;
    profile = GuiMLTextCtrl @ new ""() @ "GuiMessageTextProfile";
    0;
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
    %textAuthor = ;
    %textAuthor.bindClassName("CSMediaMLText");
    %ypos = ((%padding + 14.0) + %ypos);
    profile = GuiBitmapButtonCtrl @ new ""() @ "GuiDefaultProfile";
    0;
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
    %playButton = ;
    %playButton.bindClassName("CSBitmapButton");
    %bitmap.add(%playButton);
    %child.add(%bitmap);
    %child.add(%textTitle);
    %child.add(%textEntry);
    %child.add(%textInfo);
    %child.add(%textAuthor);
    playButton = %playButton @ %child;
    playbuttonAvailable = 1 @ %child;
    thumbnail = %bitmap @ %child;
    medialink = %textEntry @ %child;
    mediatitle = %textTitle @ %child;
    mediainfo = %textInfo @ %child;
    mediaauthor = %textAuthor @ %child;
    displayType = $CSMediaDisplay::TypeYoutube @ %child;
};
function CSMediaDisplay::buildChildDisplayRadio(%this, %child, %url) {
    %padding = 2;
    %windowWidth = getWord(%child.getExtent(), 0);
    %xPos = 1;
    %ypos = 0;
    profile = GuiBitmapCtrl @ new ""() @ "ETSNonModalProfile";
    0;
    horizSizing = "right";
    vertSizing = "bottom";
    position = %xPos @ " " @ %ypos;
    extent = "45 45";
    minExtent = "1 1";
    sluggishness = -1;
    visible = 1;
    %bitmap = ;
    %bmpWidth = 60;
    %bmpHeight = getWord(%bitmap.getExtent(), 1);
    %bitmap.setBitmap($CSMediaDisplay::RadioBunnyThumb);
    %xPos = ((%padding + %bmpWidth) + %xPos);
    %windowWidth = (%bmpWidth - %windowWidth);
    profile = GuiMLTextCtrl @ new ""() @ "GuiMessageTextProfile";
    0;
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
    %textTitle = ;
    %textTitle.bindClassName("CSMediaMLText");
    %textTitle.setText("vSide Radio");
    %ypos = ((%padding + 14.0) + %ypos);
    profile = new GuiPopUp2MenuCtrl(CSMediaMusicStreamPopup) @ "InfoWindowPopupProfile";
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
    %dropdown = ;
    profile = GuiBitmapButtonCtrl @ new ""() @ "GuiDefaultProfile";
    0;
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
    %playButton = ;
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
    if (isReadOnly) {
        profile = GuiMLTextCtrl @ new ""() @ "GuiMessageTextProfile";
        0;
        horizSizing = (%count < %idx) @ %child @ "width";
        vertSizing = "height";
        position = %dropdown @ position;
        extent = %dropdown @ extent;
        minExtent = %dropdown @ extent;
        visible = 1;
        setFirstResponder = 0;
        altCommand = %this @ ".changeMediaLink(" @ %child @ ");";
        modal = 0;
        helpTag = 0;
        historySize = 0;
        %blocker = ;
        %blocker.bindClassName("CSMediaMLText");
        %child.add(%blocker);
    }
    playButton = %playButton @ %child;
    playbuttonAvailable = 1 @ %child;
    thumbnail = %bitmap @ %child;
    medialink = %dropdown @ %child;
    mediatitle = %textTitle @ %child;
    mediainfo = "" @ %child;
    displayType = $CSMediaDisplay::TypeRadio @ %child;
};
function CSMediaDisplay::buildChildDisplayShoutCast(%this, %child) {
    %padding = 1;
    %windowWidth = getWord(%child.getExtent(), 0);
    %authorWidth = 80;
    %xPos = 1;
    %ypos = 0;
    profile = GuiBitmapCtrl @ new ""() @ "ETSNonModalProfile";
    0;
    horizSizing = "right";
    vertSizing = "bottom";
    position = %xPos @ " " @ (1.0 + %ypos);
    extent = "60 45";
    minExtent = "1 1";
    sluggishness = -1;
    visible = 1;
    %bitmap = ;
    %bmpWidth = getWord(%bitmap.getExtent(), 0);
    %bmpHeight = getWord(%bitmap.getExtent(), 1);
    %bitmap.setBitmap($CSMediaDisplay::RadioBunnyThumb);
    %xPos = ((%padding + %bmpWidth) + %xPos);
    %windowWidth = (%bmpWidth - %windowWidth);
    profile = GuiMLTextCtrl @ new ""() @ "GuiMessageTextProfile";
    0;
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
    %textTitle = ;
    %textTitle.bindClassName("CSMediaMLText");
    %textTitle.setText("SHOUTcast Stream");
    %ypos = ((%padding + 13.0) + %ypos);
    profile = new ""() @ %child @ isReadOnly ? "ETSDarkReadonlyTextEditProfile" : "ETSDarkTextEditProfile";
    GuiTextEditCtrl;
    horizSizing = 0 @ "center";
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
    readOnly = %child @ isReadOnly;
    %textEntry = ;
    %ypos = ((%padding + 16.0) + %ypos);
    profile = GuiMLTextCtrl @ new ""() @ "GuiMessageTextProfile";
    0;
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
    %textInfo = ;
    %textInfo.bindClassName("CSMediaMLText");
    %textInfo.setText("Retrieving stream info...");
    %xPos = ((getWord(extent, 0) + %padding) + %xPos);
    %textInfo;
    profile = GuiMLTextCtrl @ new ""() @ "GuiMessageTextProfile";
    0;
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
    %textAuthor = ;
    %textAuthor.bindClassName("CSMediaMLText");
    %ypos = ((%padding + 14.0) + %ypos);
    profile = GuiBitmapButtonCtrl @ new ""() @ "GuiDefaultProfile";
    0;
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
    %playButton = ;
    %playButton.bindClassName("CSBitmapButton");
    %bitmap.add(%playButton);
    %child.add(%bitmap);
    %child.add(%textTitle);
    %child.add(%textEntry);
    %child.add(%textInfo);
    %child.add(%textAuthor);
    playButton = %playButton @ %child;
    playbuttonAvailable = 1 @ %child;
    thumbnail = %bitmap @ %child;
    medialink = %textEntry @ %child;
    mediatitle = %textTitle @ %child;
    mediainfo = %textInfo @ %child;
    mediaauthor = %textAuthor @ %child;
    displayType = $CSMediaDisplay::TypeShoutCast @ %child;
};
function CSMediaDisplay::buildChildDisplayEmpty(%this, %child, %url) {
    %padding = 2;
    %windowWidth = getWord(%child.getExtent(), 0);
    %xPos = 1;
    %ypos = 0;
    profile = GuiBitmapCtrl @ new ""() @ "ETSNonModalProfile";
    0;
    horizSizing = "right";
    vertSizing = "bottom";
    position = %xPos @ " " @ %ypos;
    extent = "45 45";
    minExtent = "1 1";
    sluggishness = -1;
    visible = 1;
    %bitmap = ;
    %bmpWidth = 60;
    %bmpHeight = getWord(%bitmap.getExtent(), 1);
    %bitmap.setBitmap($CSMediaDisplay::EmptyBunnyThumb);
    %xPos = ((%padding + %bmpWidth) + %xPos);
    %windowWidth = (%bmpWidth - %windowWidth);
    profile = GuiMLTextCtrl @ new ""() @ "GuiMessageTextProfile";
    0;
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
    %textTitle = ;
    %textTitle.bindClassName("CSMediaMLText");
    %textTitle.setText("Media URL:");
    %ypos = ((%padding + 14.0) + %ypos);
    profile = GuiTextEditCtrl @ new ""() @ "ETSDarkTextEditProfile";
    0;
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
    %textEntry = ;
    %ypos = ((%padding + 18.0) + %ypos);
    %child.add(%bitmap);
    %child.add(%textTitle);
    %child.add(%textEntry);
    playButton = "" @ %child;
    playbuttonAvailable = 0 @ %child;
    thumbnail = %bitmap @ %child;
    medialink = %textEntry @ %child;
    mediatitle = %textTitle @ %child;
    mediainfo = "" @ %child;
    displayType = $CSMediaDisplay::TypeEmpty @ %child;
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
    origin = Canvas @ getCursorPos() @ %this;
};
function CSBitmapButton::onMouseDragged(%this) {
    %parent = %this.getParent().getParent();
    if ((-(1.0) == findWord(%parent.getNamespaceList(), "CSMediaHotListItem"))) {
        return 0;
    }
    %vec = VectorSub(origin, getCursorPos());
    Canvas;
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
    profile = GuiControl @ new ""() @ "DragAndDropProfile";
    0;
    horizSizing = "width";
    vertSizing = "height";
    position = "0 0";
    extent = %this.getExtent();
    minExtent = "1 1";
    sluggishness = -1;
    visible = 1;
    %ctrl = ;
    profile = GuiBitmapCtrl @ new ""() @ "ETSNonModalProfile";
    0;
    horizSizing = "right";
    vertSizing = "bottom";
    position = %xPos @ " " @ (1.0 + %ypos);
    extent = "60 45";
    minExtent = "1 1";
    sluggishness = -1;
    visible = 1;
    %bitmap = ;
    %ctrl.add(%bitmap);
    %bitmap.setBitmap(%this.getObject(0).getBitmap());
    %bmpWidth = getWord(%bitmap.getExtent(), 0);
    %bmpHeight = getWord(%bitmap.getExtent(), 1);
    %xPos = ((%padding + %bmpWidth) + %xPos);
    %windowWidth = (%bmpWidth - %windowWidth);
    profile = GuiMLTextCtrl @ new ""() @ "GuiMessageTextProfile";
    0;
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
    %textTitle = ;
    %textTitle.bindClassName("CSMediaMLText");
    %textTitle.setText("YouTube Video");
    %ypos = ((%padding + 13.0) + %ypos);
    %ctrl.add(%textTitle);
    mediatitle = %textTitle @ %ctrl;
    AuthorName = %this @ AuthorName @ %ctrl;
    title = %this @ title @ %ctrl;
    %ctrl.buildYoutubeTitle();
    return %ctrl;
};
function CSMediaFavListItem::onDragAndDropEnter(%this, %dragCtrl) {
    if ((-(1.0) == findWord(%dragCtrl.getNamespaceList(), "CSMediaHotListItem"))) {
        return;
    }
    hiliteControl(medialink);
};
function CSMediaFavListItem::onDragAndDropLeave(%this, %dragCtrl) {
    hiliteControl(0);
};
function CSMediaFavListItem::onDragAndDropMove(%this, %dragCtrl, %unused) {
};
function CSMediaFavListItem::onDragAndDropDrop(%this, %dragCtrl, %unused) {
    %url = %dragCtrl.getMediaLink();
    CSMediaDisplay;
    if ((%url $= "")) {
        return 0;
    }
    autoplay = 1 @ %this;
    %this.updateMediaLinkTo(%url, 1);
    return 1;
};
function CSMediaFavListItem::onSystemDragDropEvent(%this, %text, %eventType, %pt) {
    if (!(isURL(%text))) {
        return 0;
    }
    if ((%this == displayType)) {
        return 0;
    }
    if (!(Parent::onSystemDragDropEvent(%this, %text, %eventType, %pt))) {
        return 0;
    }
    hiliteControl(medialink);
    if ((%this SPC %eventType $= "BREAK")) {
        autoplay = 1 @ %this;
        if (isObject(mediainfo)) {
            mediainfo.setText("Retrieving stream info...");
        }
        %this.updateMediaLinkTo(%text, 0);
    }
    return 1;
};
