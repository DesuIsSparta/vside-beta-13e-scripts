$ImageFrameBase::Type_User = 0;
$ImageFrameBase::Type_Gallery = 1;
$ImageFrameBase::Type_URL = 2;
$ImageFrameBase::Type_Event = 3;
$ImageFrameBase::Type_Gallery2 = 4;
function ImageFrameBase::onImageTagChanged(%this, %newUrl) {
    if ((0.0 == strlen(%newUrl))) {
        %this.setPortraitTexture("");
        return;
    }
    %this.getUserPortrait(%newUrl);
    if (0) {
        if ((CustomSpaceClient::GetSpaceImIn() $= "")) {
        }
        if (CustomSpaceClient::isOwner()) {
            %playerName = %this.getImageTag();
            InfoPopupDlg.close();
            %playerName.showInfoFor();
            InfoPopupDlg.open();
        }
    }
};
function ImageFrameBase::getImageTagType(%this, %imageTag) {
    %this.imageKey = "";
    %this.type = "";
    %urlinfo = new ""();;
    ScriptObject;
    %urlinfo.bindClassName("URLInfo");
    %urlinfo.url = 0 @ %imageTag;
    %bValidURL = %urlinfo.parse();
    if ((0.0 == %bValidURL)) {
        %urlinfo.delete();
        %this.imageKey = %imageTag;
        if (%this.isImageGUID(%imageTag)) {
            %this.type = $ImageFrameBase::Type_Gallery;
            return $ImageFrameBase::Type_Gallery;
        }
        %this.type = $ImageFrameBase::Type_User;
        return $ImageFrameBase::Type_User;
    }
    %mainhost = getWord(strreplace($Net::BaseDomain, ":", " "), 0);
    %retVal = $ImageFrameBase::Type_URL;
    %hostwords = strreplace(%urlinfo.host, ".", " ");
    %this.urlhost = getWord(%hostwords, (2.0 - getWordCount(%hostwords)));
    if ((0.0 == stricmp(%mainhost, %urlinfo.host))) {
        %path = trim(strreplace(%urlinfo.Path, "/", " "));
        %pathIntro = getWords(%path, 0, (2.0 - getWordCount(%path)));
        %key = getWord(%path, (1.0 - getWordCount(%path)));
        if ((0.0 == stricmp(%pathIntro, "app photo id"))) {
            %retVal = $ImageFrameBase::Type_Gallery2;
            %this.imageKey = %key;
        }
        if ((0.0 == stricmp(%pathIntro, "app event detail id"))) {
            %retVal = $ImageFrameBase::Type_Event;
            %this.imageKey = %key;
        }
    }
    %urlinfo.delete();
    %this.type = %retVal;
    return %retVal;
};
$DlgPortraitSelect = 0;
function ImageFrameBase::onUse(%this) {
    if (%this.isServerObject()) {
        return;
    }
    if ((CustomSpaceClient::GetSpaceImIn() $= "")) {
    }
    if (!(CustomSpaceClient::isOwner())) {
        if (!($CS_EditingCustomSpace)) {
        }
        if (CustomSpaceClient::isOwner()) {
        }
    }
    if (($EventModifier::CTRL & $Keyboard::modifierKeys)) {
        %imageTag = %this.getImageTag();
        if ((%imageTag $= "")) {
            return;
        }
        if (($ImageFrameBase::Type_Gallery == %this.type)) {
        }
        if (($ImageFrameBase::Type_URL == %this.type)) {
        }
        if (($ImageFrameBase::Type_Event == %this.type)) {
        }
        if (($ImageFrameBase::Type_Gallery2 == %this.type)) {
            if ((%this.url $= "")) {
                return;
            }
            %this.url.initWithURLAndTitle(%this.Caption);
            LinkContextMenu.showAtCursor();
            return LinkContextMenu;
        }
        InfoPopupDlg.close();
        %imageTag.showInfoFor();
        InfoPopupDlg.open();
        return InfoPopupDlg;
    }
    $DlgPortraitSelect = MessageBoxTextEntryWithCancel(, , %this.getImageTag(), 0);
    ImageFrameBase_SubmitPortrait;
    $DlgPortraitSelect.textEntry.resize(8, 68, 284, 18);
    $DlgPortraitSelect.portrait = %this;
};
function ImageFrameBase_SubmitPortrait(%url) {
    %obj = $DlgPortraitSelect.portrait;
    if (isURL(%url)) {
        if (!(ImageFrameBase_IsPermittedURL(%url))) {
            MessageBoxOK(, , "");
            return;
        }
    }
    commandToServer('SetUserPortrait', CustomSpaceClient::GetSpaceImIn(), %obj.getGhostID(), %url);
};
function ImageFrameBase::onRightUse(%this) {
    %imageTag = %this.getImageTag();
    if ((%imageTag $= "")) {
        return;
    }
    if (($ImageFrameBase::Type_URL == %this.type)) {
        return;
    }
    if (($ImageFrameBase::Type_Gallery == %this.type)) {
    }
    if (($ImageFrameBase::Type_Event == %this.type)) {
    }
    if (($ImageFrameBase::Type_Gallery2 == %this.type)) {
        if ((%this.url $= "")) {
            return;
        }
        %this.url.initWithURLAndTitle(%this.Caption);
        LinkContextMenu.showAtCursor();
        return LinkContextMenu;
    }
    %info = %imageTag.get();
    PlayerInfoMap;
    if (isObject(%info)) {
        %imageTag.initWithPlayerName();
        Canvas.getCursorPos().showAtPoint();
    }
    requestPlayerInfoForWithCallback(%imageTag, "ImageFrameBase_gotInfoDoMenu", %this);
};
function ImageFrameBase::buildImageURL(%this, %imageTag, %type) {
    if (($ImageFrameBase::Type_User == %type)) {
        %url = $Net::AvatarURL @ urlEncode(stripUnprintables(%imageTag)) @ "?size=M256";
    }
    if (($ImageFrameBase::Type_Gallery == %type)) {
        %url = $Net::GalleryPhotoURL @ %imageTag @ "?size=M";
    }
    if (($ImageFrameBase::Type_URL == %type)) {
        %url = %imageTag;
    }
    if (($ImageFrameBase::Type_Event == %type)) {
        %this.GetEventInfo(%this.imageKey);
        %url = "";
    }
    if (($ImageFrameBase::Type_Gallery2 == %type)) {
        %url = $Net::GalleryPhotoURL @ %this.imageKey @ "?size=M";
    }
    return %url;
};
function ImageFrameBase::buildLinkURLAndCaption(%this, %imageTag, %type) {
    if (($ImageFrameBase::Type_User == %type)) {
        %caption = %imageTag;
        %url = "";
    }
    if (($ImageFrameBase::Type_Gallery == %type)) {
        %caption = %type[$ImageFrame_DisplayName @ "vside"];
        %url = $Net::PhotoPageURL @ %imageTag;
    }
    if (($ImageFrameBase::Type_URL == %type)) {
        %host = %this.urlhost;
        if (!(%host[$ImageFrame_DisplayName @ %host] $= "")) {
            %caption = %host[$ImageFrame_DisplayName @ %host];
        }
        %caption = %caption[$ImageFrame_DisplayName @ "unknown"];
        %url = %imageTag;
    }
    if (($ImageFrameBase::Type_Event == %type)) {
        %caption = %type[$ImageFrame_DisplayName @ "vsideevent"];
        %url = %imageTag;
    }
    if (($ImageFrameBase::Type_Gallery2 == %type)) {
        %caption = %type[$ImageFrame_DisplayName @ "vside"];
        %url = $Net::PhotoPageURL @ %this.imageKey;
    }
    %this.Caption = %caption;
    %this.url = %url;
    return %url;
};
function ImageFrameBase::getUserPortrait(%this, %imageTag) {
    %type = %this.getImageTagType(%imageTag);
    %url = %this.buildImageURL(%imageTag, %type);
    %this.buildLinkURLAndCaption(%imageTag, %type);
    if (!(%url $= "")) {
        %this.downloadAndApplyImage(%url);
    }
};
function ImageFrameBase::downloadAndApplyImage(%this, %url) {
    if (!(ImageFrameBase_IsPermittedURL(%url))) {
        return;
    }
    %this.expectedImageUrl = %url;
    %url.applyUrl("dlMgrCallback_ImageFrameBase", "dlMgrErrorCallback_ImageFrameBase", %this, "");
};
function dlMgrCallback_ImageFrameBase(%dlItem, %unused) {
    %imageFrame = %dlItem.callbackData;
    if (!(isObject(%imageFrame))) {
        warn(getScopeName() @ " " @ "- ImageFrame no longer exists!" @ " " @ %imageFrame @ " " @ %dlItem.url @ " " @ getTrace());
        return;
    }
    if (!(%imageFrame.expectedImageUrl $= %dlItem.url)) {
        echoDebug(getScopeName() @ " " @ "- unexpected URL retrieved. Expected \"" @ %imageFrame.expectedImageUrl @ "\" but got \"" @ %dlItem.url @ "\".");
    }
    %imageFrame.setPortraitTexture("");
    %imageFrame.setPortraitTexture(%dlItem.localFilename);
    %imageFrame.expectedUrl = "";
};
function ImageFrameBase_IsPermittedURL(%url) {
    if (stricmp(getSubStr(%url, 0, 4), "http")) {
        return 1;
    }
    %start = strpos(%url, ":");
    %start = (3.0 + %start);
    %testUrl = getSubStr(%url, %start);
    %cut = strpos(%testUrl, "/");
    if ((-(1.0) > %cut)) {
        %testUrl = getSubStr(%testUrl, 0, %cut);
    }
    %cut = strpos(%testUrl, ":");
    if ((-(1.0) > %cut)) {
        %testUrl = getSubStr(%testUrl, 0, %cut);
    }
    %testUrl = strlwr(strreplace(%testUrl, ".", " "));
    %count = getWordCount(%testUrl);
    if ((2.0 < %count)) {
        return 0;
    }
    %host = getWords(%testUrl, (2.0 - %count));
    %idx = 0;
    if (!(%idx[$ImageFrame_WhiteList @ %idx] $= "")) {
        if ((0.0 == stricmp(%host, %idx[$ImageFrame_WhiteList @ %idx]))) {
            return 1;
        }
        %idx = (1.0 + %idx);
    }
    return 0;
};
function isURL(%url) {
    if (!(stricmp(getSubStr(%url, 0, 4), "http"))) {
        return 1;
    }
    return 0;
};
function ImageFrameBase::isImageGUID(%this, %guid) {
    if ((36.0 != strlen(%guid))) {
        return 0;
    }
    %guid = strreplace(%guid, "-", " ");
    %count = getWordCount(%guid);
    if ((5.0 != %count)) {
        return 0;
    }
    if ((8.0 != strlen(getWord(%guid, 0)))) {
        return 0;
    }
    if ((4.0 != strlen(getWord(%guid, 1)))) {
        return 0;
    }
    if ((4.0 != strlen(getWord(%guid, 2)))) {
        return 0;
    }
    if ((4.0 != strlen(getWord(%guid, 3)))) {
        return 0;
    }
    if ((12.0 != strlen(getWord(%guid, 4)))) {
        return 0;
    }
    return 1;
};
function ImageFrameBase::GetEventInfo(%this, %eventId) {
    %request = sendRequest_EventInformation(%eventId, "onDoneOrErrorCallback_EventInfo");
    %request.frame = %this;
};
function onDoneOrErrorCallback_EventInfo(%request) {
    %status = %request.getResult("status");
    %imgFrame = %request.frame;
    if (!(%status $= "success")) {
        error("client Event info request HTTP status: " @ %status);
        return;
    }
    %imageURL = %request.getValue("photo.url");
    if ((%imageURL $= "")) {
        %imageURL = "http://" @ $Net::BaseDomain @ "/images/events/default_banner_L.jpg";
    }
    %imageCaption = %request.getValue("photo.caption");
    %imgFrame.Caption = %imageCaption;
    %imgFrame.downloadAndApplyImage(%imageURL);
};
function dlMgrErrorCallback_ImageFrameBase(%dlItem) {
    %obj = %dlItem.callbackData;
    %playerName = %obj.getImageTag();
    %obj.setPortraitTexture("");
    if ((CustomSpaceClient::GetSpaceImIn() $= "")) {
    }
    if (CustomSpaceClient::isOwner()) {
        %imgTag = %obj.getImageTag();
        if (($ImageFrameBase::Type_URL == %obj.type)) {
            %message = strreplace(%obj[$MsgCat::furniture @ "IMAGEFRAME-LOADFAILEDURL"], "[URL]", %imgTag);
        }
        if (($ImageFrameBase::Type_Gallery == %obj.type)) {
        }
        if (($ImageFrameBase::Type_Gallery2 == %obj.type)) {
            %message = strreplace(%obj[$MsgCat::furniture @ "IMAGEFRAME-LOADFAILEDGALLERY"], "[GUID]", %imgTag);
        }
        if (($ImageFrameBase::Type_Event == %obj.type)) {
            %message = strreplace(%obj[$MsgCat::furniture @ "IMAGEFRAME-LOADFAILEDEVENT"], "[EVENT]", %imgTag);
        }
        %info = %imgTag.get();
        PlayerInfoMap;
        if (isObject(%info)) {
            %obj.showDefaultPlayerPortrait(%info);
        }
        requestPlayerInfoForWithCallback(%imgTag, "ImageFrameBase_gotInfoPlayerSex", %obj);
        %message = "";
        if (!(%message $= "")) {
            handleSystemMessage("msgInfoMessage", %message);
        }
    }
};
function ImageFrameBase_gotInfoDoMenu(%playerName, %info, %frame) {
    if (isObject(%info)) {
        %playerName.initWithPlayerName();
        Canvas.getCursorPos().showAtPoint();
    }
};
function ImageFrameBase_gotInfoPlayerSex(%playerName, %info, %frame) {
    echo("ImageFrameBase_gotInfoPlayerSex( \"" @ %playerName @ "\", " @ %info @ ")");
    if (isObject(%info)) {
        %frame.showDefaultPlayerPortrait(%info);
    }
    %message = strreplace(, "[USER]", %playerName);
    handleSystemMessage("msgInfoMessage", %message);
};
function ImageFrameBase::showDefaultPlayerPortrait(%this, %playerInfo) {
    %url = "http://" @ $Net::BaseDomain @ "/images/defaults/avatar_" @ %playerInfo.gender @ "_large.jpg";
    %this.downloadAndApplyImage(%url);
};
