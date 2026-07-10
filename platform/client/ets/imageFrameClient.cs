$ImageFrameBase::Type_User = 0;
$ImageFrameBase::Type_Gallery = 1;
$ImageFrameBase::Type_URL = 2;
$ImageFrameBase::Type_Event = 3;
$ImageFrameBase::Type_Gallery2 = 4;
function ImageFrameBase::onImageTagChanged(%this, %newUrl) {
    if ((strlen(%newUrl) == 0.0)) {
        "".setPortraitTexture(%this);
        return;
    }
    %newUrl.getUserPortrait(%this);
    if (0) {
        if ((CustomSpaceClient::GetSpaceImIn() $= "")) {
        }
        if (CustomSpaceClient::isOwner()) {
            %playerName = %this.getImageTag();
            InfoPopupDlg.close();
            %playerName.showInfoFor(InfoPopupDlg);
            InfoPopupDlg.open();
        }
    }
};
function ImageFrameBase::getImageTagType(%this, %imageTag) {
    %this.imageKey = "";
    %this.type = "";
    %urlinfo = new ScriptObject("");;
    0;
    "URLInfo".bindClassName(%urlinfo);
    %urlinfo.url = %imageTag;
    %bValidURL = %urlinfo.parse();
    if ((%bValidURL == 0.0)) {
        %urlinfo.delete();
        %this.imageKey = %imageTag;
        if (%imageTag.isImageGUID(%this)) {
            %this.type = $ImageFrameBase::Type_Gallery;
            return $ImageFrameBase::Type_Gallery;
        }
        %this.type = $ImageFrameBase::Type_User;
        return $ImageFrameBase::Type_User;
    }
    %mainhost = getWord(strreplace($Net::BaseDomain, ":", " "), 0);
    %retVal = $ImageFrameBase::Type_URL;
    %hostwords = strreplace(%urlinfo.host, ".", " ");
    %this.urlhost = getWord(%hostwords, (getWordCount(%hostwords) - 2.0));
    if ((stricmp(%mainhost, %urlinfo.host) == 0.0)) {
        %path = trim(strreplace(%urlinfo.Path, "/", " "));
        %pathIntro = getWords(%path, 0, (getWordCount(%path) - 2.0));
        %key = getWord(%path, (getWordCount(%path) - 1.0));
        if ((stricmp(%pathIntro, "app photo id") == 0.0)) {
            %retVal = $ImageFrameBase::Type_Gallery2;
            %this.imageKey = %key;
        }
        if ((stricmp(%pathIntro, "app event detail id") == 0.0)) {
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
    if (($Keyboard::modifierKeys & $EventModifier::CTRL)) {
        %imageTag = %this.getImageTag();
        if ((%imageTag $= "")) {
            return;
        }
        if ((%this.type == $ImageFrameBase::Type_Gallery)) {
        }
        if ((%this.type == $ImageFrameBase::Type_URL)) {
        }
        if ((%this.type == $ImageFrameBase::Type_Event)) {
        }
        if ((%this.type == $ImageFrameBase::Type_Gallery2)) {
            if ((%this.url $= "")) {
                return;
            }
            %this.Caption.initWithURLAndTitle(LinkContextMenu, %this.url);
            LinkContextMenu.showAtCursor();
            return;
        }
        InfoPopupDlg.close();
        %imageTag.showInfoFor(InfoPopupDlg);
        InfoPopupDlg.open();
        return;
    }
    $DlgPortraitSelect = MessageBoxTextEntryWithCancel(, , ImageFrameBase_SubmitPortrait, %this.getImageTag(), 0);
    18.resize($DlgPortraitSelect.textEntry, 8, 68, 284);
    $DlgPortraitSelect.portrait = %this;
};
function ImageFrameBase_SubmitPortrait(%url) {
    %obj = $DlgPortraitSelect.portrait;
    if (isURL(%url) && !(ImageFrameBase_IsPermittedURL(%url))) {
        MessageBoxOK(, , "");
        return;
    }
    commandToServer('SetUserPortrait', CustomSpaceClient::GetSpaceImIn(), %obj.getGhostID(), %url);
};
function ImageFrameBase::onRightUse(%this) {
    %imageTag = %this.getImageTag();
    if ((%imageTag $= "")) {
        return;
    }
    if ((%this.type == $ImageFrameBase::Type_URL)) {
        return;
    }
    if ((%this.type == $ImageFrameBase::Type_Gallery)) {
    }
    if ((%this.type == $ImageFrameBase::Type_Event)) {
    }
    if ((%this.type == $ImageFrameBase::Type_Gallery2)) {
        if ((%this.url $= "")) {
            return;
        }
        %this.Caption.initWithURLAndTitle(LinkContextMenu, %this.url);
        LinkContextMenu.showAtCursor();
        return;
    }
    %info = %imageTag.get(PlayerInfoMap);
    if (isObject(%info)) {
        %imageTag.initWithPlayerName(PlayerContextMenu);
        Canvas.getCursorPos().showAtPoint(PlayerContextMenu);
    }
    requestPlayerInfoForWithCallback(%imageTag, "ImageFrameBase_gotInfoDoMenu", %this);
};
function ImageFrameBase::buildImageURL(%this, %imageTag, %type) {
    if ((%type == $ImageFrameBase::Type_User)) {
        %url = $Net::AvatarURL @ urlEncode(stripUnprintables(%imageTag)) @ "?size=M256";
    }
    if ((%type == $ImageFrameBase::Type_Gallery)) {
        %url = $Net::GalleryPhotoURL @ %imageTag @ "?size=M";
    }
    if ((%type == $ImageFrameBase::Type_URL)) {
        %url = %imageTag;
    }
    if ((%type == $ImageFrameBase::Type_Event)) {
        %this.imageKey.GetEventInfo(%this);
        %url = "";
    }
    if ((%type == $ImageFrameBase::Type_Gallery2)) {
        %url = $Net::GalleryPhotoURL @ %this.imageKey @ "?size=M";
    }
    return %url;
};
function ImageFrameBase::buildLinkURLAndCaption(%this, %imageTag, %type) {
    if ((%type == $ImageFrameBase::Type_User)) {
        %caption = %imageTag;
        %url = "";
    }
    if ((%type == $ImageFrameBase::Type_Gallery)) {
        %caption = %type[$ImageFrame_DisplayName @ "vside"];
        %url = $Net::PhotoPageURL @ %imageTag;
    }
    if ((%type == $ImageFrameBase::Type_URL)) {
        %host = %this.urlhost;
        if (!(%host[$ImageFrame_DisplayName @ %host] $= "")) {
            %caption = %host[$ImageFrame_DisplayName @ %host];
        }
        %caption = %caption[$ImageFrame_DisplayName @ "unknown"];
        %url = %imageTag;
    }
    if ((%type == $ImageFrameBase::Type_Event)) {
        %caption = %type[$ImageFrame_DisplayName @ "vsideevent"];
        %url = %imageTag;
    }
    if ((%type == $ImageFrameBase::Type_Gallery2)) {
        %caption = %type[$ImageFrame_DisplayName @ "vside"];
        %url = $Net::PhotoPageURL @ %this.imageKey;
    }
    %this.Caption = %caption;
    %this.url = %url;
    return %url;
};
function ImageFrameBase::getUserPortrait(%this, %imageTag) {
    %type = %imageTag.getImageTagType(%this);
    %url = %type.buildImageURL(%this, %imageTag);
    %type.buildLinkURLAndCaption(%this, %imageTag);
    if (!(%url $= "")) {
        %url.downloadAndApplyImage(%this);
    }
};
function ImageFrameBase::downloadAndApplyImage(%this, %url) {
    if (!(ImageFrameBase_IsPermittedURL(%url))) {
        return;
    }
    %this.expectedImageUrl = %url;
    "".applyUrl(dlMgr, %url, "dlMgrCallback_ImageFrameBase", "dlMgrErrorCallback_ImageFrameBase", %this);
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
    "".setPortraitTexture(%imageFrame);
    %dlItem.localFilename.setPortraitTexture(%imageFrame);
    %imageFrame.expectedUrl = "";
};
function ImageFrameBase_IsPermittedURL(%url) {
    if (stricmp(getSubStr(%url, 0, 4), "http")) {
        return 1;
    }
    %start = strpos(%url, ":");
    %start = (%start + 3.0);
    %testUrl = getSubStr(%url, %start);
    %cut = strpos(%testUrl, "/");
    if ((%cut > -(1.0))) {
        %testUrl = getSubStr(%testUrl, 0, %cut);
    }
    %cut = strpos(%testUrl, ":");
    if ((%cut > -(1.0))) {
        %testUrl = getSubStr(%testUrl, 0, %cut);
    }
    %testUrl = strlwr(strreplace(%testUrl, ".", " "));
    %count = getWordCount(%testUrl);
    if ((%count < 2.0)) {
        return 0;
    }
    %host = getWords(%testUrl, (%count - 2.0));
    %idx = 0;
    while (!(%idx[$ImageFrame_WhiteList @ %idx] $= "")) {
        if ((stricmp(%host, %idx[$ImageFrame_WhiteList @ %idx]) == 0.0)) {
            return 1;
        }
        %idx = (%idx + 1.0);
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
    if ((strlen(%guid) != 36.0)) {
        return 0;
    }
    %guid = strreplace(%guid, "-", " ");
    %count = getWordCount(%guid);
    if ((%count != 5.0)) {
        return 0;
    }
    if ((strlen(getWord(%guid, 0)) != 8.0)) {
        return 0;
    }
    if ((strlen(getWord(%guid, 1)) != 4.0)) {
        return 0;
    }
    if ((strlen(getWord(%guid, 2)) != 4.0)) {
        return 0;
    }
    if ((strlen(getWord(%guid, 3)) != 4.0)) {
        return 0;
    }
    if ((strlen(getWord(%guid, 4)) != 12.0)) {
        return 0;
    }
    return 1;
};
function ImageFrameBase::GetEventInfo(%this, %eventId) {
    %request = sendRequest_EventInformation(%eventId, "onDoneOrErrorCallback_EventInfo");
    %request.frame = %this;
};
function onDoneOrErrorCallback_EventInfo(%request) {
    %status = "status".getResult(%request);
    %imgFrame = %request.frame;
    if (!(%status $= "success")) {
        error("client Event info request HTTP status: " @ %status);
        return;
    }
    %imageURL = "photo.url".getValue(%request);
    if ((%imageURL $= "")) {
        %imageURL = "http://" @ $Net::BaseDomain @ "/images/events/default_banner_L.jpg";
    }
    %imageCaption = "photo.caption".getValue(%request);
    %imgFrame.Caption = %imageCaption;
    %imageURL.downloadAndApplyImage(%imgFrame);
};
function dlMgrErrorCallback_ImageFrameBase(%dlItem) {
    %obj = %dlItem.callbackData;
    %playerName = %obj.getImageTag();
    "".setPortraitTexture(%obj);
    if ((CustomSpaceClient::GetSpaceImIn() $= "")) {
    }
    if (CustomSpaceClient::isOwner()) {
        %imgTag = %obj.getImageTag();
        if ((%obj.type == $ImageFrameBase::Type_URL)) {
            %message = strreplace(%obj[$MsgCat::furniture @ "IMAGEFRAME-LOADFAILEDURL"], "[URL]", %imgTag);
        }
        if ((%obj.type == $ImageFrameBase::Type_Gallery)) {
        }
        if ((%obj.type == $ImageFrameBase::Type_Gallery2)) {
            %message = strreplace(%obj[$MsgCat::furniture @ "IMAGEFRAME-LOADFAILEDGALLERY"], "[GUID]", %imgTag);
        }
        if ((%obj.type == $ImageFrameBase::Type_Event)) {
            %message = strreplace(%obj[$MsgCat::furniture @ "IMAGEFRAME-LOADFAILEDEVENT"], "[EVENT]", %imgTag);
        }
        %info = %imgTag.get(PlayerInfoMap);
        if (isObject(%info)) {
            %info.showDefaultPlayerPortrait(%obj);
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
        %playerName.initWithPlayerName(PlayerContextMenu);
        Canvas.getCursorPos().showAtPoint(PlayerContextMenu);
    }
};
function ImageFrameBase_gotInfoPlayerSex(%playerName, %info, %frame) {
    echo("ImageFrameBase_gotInfoPlayerSex( \"" @ %playerName @ "\", " @ %info @ ")");
    if (isObject(%info)) {
        %info.showDefaultPlayerPortrait(%frame);
    }
    %message = strreplace(, "[USER]", %playerName);
    handleSystemMessage("msgInfoMessage", %message);
};
function ImageFrameBase::showDefaultPlayerPortrait(%this, %playerInfo) {
    %url = "http://" @ $Net::BaseDomain @ "/images/defaults/avatar_" @ %playerInfo.gender @ "_large.jpg";
    %url.downloadAndApplyImage(%this);
};
