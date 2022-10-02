$ImageFrame_WhiteList[0] = "vside" SPC "com";
$ImageFrame_WhiteList[1] = "doppelganger" SPC "com";
$ImageFrame_WhiteList[2] = "flickr" SPC "com";
$ImageFrame_WhiteList[3] = "photobucket" SPC "com";
$ImageFrame_WhiteList[4] = "ctv" SPC "ca";
$ImageFrame_WhiteList[5] = "warnerbros" SPC "com";
$ImageFrame_WhiteList[6] = "nasa" SPC "gov";
$ImageFrame_WhiteList[7] = "go" SPC "com";
$ImageFrame_WhiteList[8] = "yimg" SPC "com";
$ImageFrame_WhiteList[9] = "weather" SPC "com";
$ImageFrame_WhiteList[10] = "nationalgeographic" SPC "com";
$ImageFrame_WhiteList[11] = "timeinc" SPC "net";
$ImageFrame_WhiteList[12] = "aolcdn" SPC "com";
$ImageFrame_WhiteList[13] = "turner" SPC "com";
$ImageFrame_WhiteList[14] = "imageshack" SPC "us";
$ImageFrame_WhiteList[15] = "deviantart" SPC "com";
$ImageFrame_WhiteList[16] = "seventeen" SPC "com";
$ImageFrame_WhiteList[17] = "lolcats" SPC "com";
$ImageFrame_WhiteList[18] = "facebook" SPC "com";
$ImageFrame_WhiteList[19] = "myspace" SPC "com";
$ImageFrame_WhiteList[20] = "myspacecdn" SPC "com";
$ImageFrame_WhiteList[21] = "msn" SPC "com";
$ImageFrame_WhiteList[22] = "starpulse" SPC "com";
$ImageFrame_WhiteList[23] = "americanidol" SPC "com";
$ImageFrame_WhiteList[24] = "elle" SPC "com";
$ImageFrame_WhiteList[25] = "eonline" SPC "com";
$ImageFrame_WhiteList[26] = "style" SPC "com";
$ImageFrame_WhiteList[27] = "famousartistsgallery" SPC "com";
$ImageFrame_WhiteList[28] = "wikipedia" SPC "org";
$ImageFrame_WhiteList[29] = "wikimedia" SPC "org";
$ImageFrame_WhiteList[30] = "webgalactic" SPC "net";
$ImageFrame_WhiteList[31] = "theworldofmichaelparkes" SPC "com";
$ImageFrame_WhiteList[32] = "galleryofart" SPC "us";
$ImageFrame_WhiteList[33] = "fbcdn" SPC "net";
$ImageFrame_BlackList[0] = "forums";
$ImageFrame_DisplayName["unknown"] = "Image from the Web";
$ImageFrame_DisplayName["vside"] = "vSide Gallery";
$ImageFrame_DisplayName["doppelganger"] = $ImageFrame_DisplayName["vside"] ;
$ImageFrame_DisplayName["flickr"] = "Flickr";
$ImageFrame_DisplayName["photobucket"] = "Photobucket";
$ImageFrame_DisplayName["vsideevent"] = "vSide Event";
$ImageFrameBase::Type_User = 0;
$ImageFrameBase::Type_Gallery = 1;
$ImageFrameBase::Type_URL = 2;
$ImageFrameBase::Type_Event = 3;
$ImageFrameBase::Type_Gallery2 = 4;
function ImageFrameBase::onImageTagChanged(%this, %newUrl)
{
    if (strlen(%newUrl) == 0)
    {
        %this.setPortraitTexture("");
        return ;
    }
    %this.getUserPortrait(%newUrl);
    if (0)
    {
        if ((CustomSpaceClient::GetSpaceImIn() $= "") && CustomSpaceClient::isOwner())
        {
            %playerName = %this.getImageTag();
            InfoPopupDlg.close();
            InfoPopupDlg.showInfoFor(%playerName);
            InfoPopupDlg.open();
        }
    }
    return ;
}
function ImageFrameBase::getImageTagType(%this, %imageTag)
{
    %this.imageKey = "";
    %this.type = "";
    %urlinfo = new ScriptObject();
    %urlinfo.bindClassName("URLInfo");
    %urlinfo.url = %imageTag;
    %bValidURL = %urlinfo.parse();
    if (%bValidURL == 0)
    {
        %urlinfo.delete();
        %this.imageKey = %imageTag;
        if (%this.isImageGUID(%imageTag))
        {
            %this.type = $ImageFrameBase::Type_Gallery;
            return $ImageFrameBase::Type_Gallery;
        }
        %this.type = $ImageFrameBase::Type_User;
        return $ImageFrameBase::Type_User;
    }
    %mainhost = getWord(strreplace($Net::BaseDomain, ":", " "), 0);
    %retVal = $ImageFrameBase::Type_URL;
    %hostwords = strreplace(%urlinfo.host, ".", " ");
    %this.urlhost = getWord(%hostwords, getWordCount(%hostwords) - 2);
    if (stricmp(%mainhost, %urlinfo.host) == 0)
    {
        %path = trim(strreplace(%urlinfo.Path, "/", " "));
        %pathIntro = getWords(%path, 0, getWordCount(%path) - 2);
        %key = getWord(%path, getWordCount(%path) - 1);
        if (stricmp(%pathIntro, "app photo id") == 0)
        {
            %retVal = $ImageFrameBase::Type_Gallery2;
            %this.imageKey = %key;
        }
        else
        {
            if (stricmp(%pathIntro, "app event detail id") == 0)
            {
                %retVal = $ImageFrameBase::Type_Event;
                %this.imageKey = %key;
            }
        }
    }
    %urlinfo.delete();
    %this.type = %retVal;
    return %retVal;
}
$DlgPortraitSelect = 0;
function ImageFrameBase::onUse(%this)
{
    if (%this.isServerObject())
    {
        return ;
    }
    if (((((CustomSpaceClient::GetSpaceImIn() $= "") || !CustomSpaceClient::isOwner()) || !$CS_EditingCustomSpace) && CustomSpaceClient::isOwner()) && ($Keyboard::modifierKeys & $EventModifier::CTRL))
    {
        %imageTag = %this.getImageTag();
        if (%imageTag $= "")
        {
            return ;
        }
        if ((((%this.type == $ImageFrameBase::Type_Gallery) || (%this.type == $ImageFrameBase::Type_URL)) || (%this.type == $ImageFrameBase::Type_Event)) || (%this.type == $ImageFrameBase::Type_Gallery2))
        {
            if (%this.url $= "")
            {
                return ;
            }
            LinkContextMenu.initWithURLAndTitle(%this.url, %this.Caption);
            LinkContextMenu.showAtCursor();
            return ;
        }
        else
        {
            InfoPopupDlg.close();
            InfoPopupDlg.showInfoFor(%imageTag);
            InfoPopupDlg.open();
        }
        return ;
    }
    $DlgPortraitSelect = MessageBoxTextEntryWithCancel($MsgCat::furniture["IMAGEFRAME-TITLE"], $MsgCat::furniture["IMAGEFRAME-PROMPT"], ImageFrameBase_SubmitPortrait, %this.getImageTag(), 0);
    $DlgPortraitSelect.textEntry.resize(8, 68, 284, 18);
    $DlgPortraitSelect.portrait = %this;
    return ;
}
function ImageFrameBase_SubmitPortrait(%url)
{
    %obj = $DlgPortraitSelect.portrait;
    if (isURL(%url))
    {
        if (!ImageFrameBase_IsPermittedURL(%url))
        {
            MessageBoxOK($MsgCat::furniture["IMAGEFRAME-TITLENOTWLIST"], $MsgCat::furniture["IMAGEFRAME-NOTWHITELIST"], "");
            return ;
        }
    }
    commandToServer('SetUserPortrait', CustomSpaceClient::GetSpaceImIn(), %obj.getGhostID(), %url);
    return ;
}
function ImageFrameBase::onRightUse(%this)
{
    %imageTag = %this.getImageTag();
    if (%imageTag $= "")
    {
        return ;
    }
    if (%this.type == $ImageFrameBase::Type_URL)
    {
        return ;
    }
    if (((%this.type == $ImageFrameBase::Type_Gallery) || (%this.type == $ImageFrameBase::Type_Event)) || (%this.type == $ImageFrameBase::Type_Gallery2))
    {
        if (%this.url $= "")
        {
            return ;
        }
        LinkContextMenu.initWithURLAndTitle(%this.url, %this.Caption);
        LinkContextMenu.showAtCursor();
        return ;
    }
    %info = PlayerInfoMap.get(%imageTag);
    if (isObject(%info))
    {
        PlayerContextMenu.initWithPlayerName(%imageTag);
        PlayerContextMenu.showAtPoint(Canvas.getCursorPos());
    }
    else
    {
        requestPlayerInfoForWithCallback(%imageTag, "ImageFrameBase_gotInfoDoMenu", %this);
    }
    return ;
}
function ImageFrameBase::buildImageURL(%this, %imageTag, %type)
{
    if (%type == $ImageFrameBase::Type_User)
    {
        %url = $Net::AvatarURL @ urlEncode(stripUnprintables(%imageTag)) @ "?size=M256";
    }
    else
    {
        if (%type == $ImageFrameBase::Type_Gallery)
        {
            %url = $Net::GalleryPhotoURL @ %imageTag @ "?size=M";
        }
        else
        {
            if (%type == $ImageFrameBase::Type_URL)
            {
                %url = %imageTag;
            }
            else
            {
                if (%type == $ImageFrameBase::Type_Event)
                {
                    %this.GetEventInfo(%this.imageKey);
                    %url = "";
                }
                else
                {
                    if (%type == $ImageFrameBase::Type_Gallery2)
                    {
                        %url = $Net::GalleryPhotoURL @ %this.imageKey @ "?size=M";
                    }
                }
            }
        }
    }
    return %url;
}
function ImageFrameBase::buildLinkURLAndCaption(%this, %imageTag, %type)
{
    if (%type == $ImageFrameBase::Type_User)
    {
        %caption = %imageTag;
        %url = "";
    }
    else
    {
        if (%type == $ImageFrameBase::Type_Gallery)
        {
            %caption = $ImageFrame_DisplayName["vside"];
            %url = $Net::PhotoPageURL @ %imageTag;
        }
        else
        {
            if (%type == $ImageFrameBase::Type_URL)
            {
                %host = %this.urlhost;
                if (!($ImageFrame_DisplayName[%host] $= ""))
                {
                    %caption = $ImageFrame_DisplayName[%host];
                }
                else
                {
                    %caption = $ImageFrame_DisplayName["unknown"];
                }
                %url = %imageTag;
            }
            else
            {
                if (%type == $ImageFrameBase::Type_Event)
                {
                    %caption = $ImageFrame_DisplayName["vsideevent"];
                    %url = %imageTag;
                }
                else
                {
                    if (%type == $ImageFrameBase::Type_Gallery2)
                    {
                        %caption = $ImageFrame_DisplayName["vside"];
                        %url = $Net::PhotoPageURL @ %this.imageKey;
                    }
                }
            }
        }
    }
    %this.Caption = %caption;
    %this.url = %url;
    return %url;
}
function ImageFrameBase::getUserPortrait(%this, %imageTag)
{
    %type = %this.getImageTagType(%imageTag);
    %url = %this.buildImageURL(%imageTag, %type);
    %this.buildLinkURLAndCaption(%imageTag, %type);
    if (!(%url $= ""))
    {
        %this.downloadAndApplyImage(%url);
    }
    return ;
}
function ImageFrameBase::downloadAndApplyImage(%this, %url)
{
    if (!ImageFrameBase_IsPermittedURL(%url))
    {
        return ;
    }
    %this.expectedImageUrl = %url;
    dlMgr.applyUrl(%url, "dlMgrCallback_ImageFrameBase", "dlMgrErrorCallback_ImageFrameBase", %this, "");
    return ;
}
function dlMgrCallback_ImageFrameBase(%dlItem, %unused)
{
    %imageFrame = %dlItem.callbackData;
    if (!isObject(%imageFrame))
    {
        warn(getScopeName() SPC "- ImageFrame no longer exists!" SPC %imageFrame SPC %dlItem.url SPC getTrace());
        return ;
    }
    if (!(%imageFrame.expectedImageUrl $= %dlItem.url))
    {
        echoDebug(getScopeName() SPC "- unexpected URL retrieved. Expected \"" @ %imageFrame.expectedImageUrl @ "\" but got \"" @ %dlItem.url @ "\".");
    }
    else
    {
        %imageFrame.setPortraitTexture("");
        %imageFrame.setPortraitTexture(%dlItem.localFilename);
        %imageFrame.expectedUrl = "";
    }
    return ;
}
function ImageFrameBase_IsPermittedURL(%url)
{
    if (stricmp(getSubStr(%url, 0, 4), "http"))
    {
        return 1;
    }
    %start = strpos(%url, ":");
    %start = %start + 3;
    %testUrl = getSubStr(%url, %start);
    %cut = strpos(%testUrl, "/");
    if (%cut > -1)
    {
        %testUrl = getSubStr(%testUrl, 0, %cut);
    }
    %cut = strpos(%testUrl, ":");
    if (%cut > -1)
    {
        %testUrl = getSubStr(%testUrl, 0, %cut);
    }
    %testUrl = strlwr(strreplace(%testUrl, ".", " "));
    %count = getWordCount(%testUrl);
    if (%count < 2)
    {
        return 0;
    }
    %host = getWords(%testUrl, %count - 2);
    %idx = 0;
    while (!($ImageFrame_WhiteList[%idx] $= ""))
    {
        if (stricmp(%host, $ImageFrame_WhiteList[%idx]) == 0)
        {
            return 1;
        }
        %idx = %idx + 1;
    }
    return 0;
}
function isURL(%url)
{
    if (!stricmp(getSubStr(%url, 0, 4), "http"))
    {
        return 1;
    }
    return 0;
}
function ImageFrameBase::isImageGUID(%this, %guid)
{
    if (strlen(%guid) != 36)
    {
        return 0;
    }
    %guid = strreplace(%guid, "-", " ");
    %count = getWordCount(%guid);
    if (%count != 5)
    {
        return 0;
    }
    if (strlen(getWord(%guid, 0)) != 8)
    {
        return 0;
    }
    if (strlen(getWord(%guid, 1)) != 4)
    {
        return 0;
    }
    if (strlen(getWord(%guid, 2)) != 4)
    {
        return 0;
    }
    if (strlen(getWord(%guid, 3)) != 4)
    {
        return 0;
    }
    if (strlen(getWord(%guid, 4)) != 12)
    {
        return 0;
    }
    return 1;
}
function ImageFrameBase::GetEventInfo(%this, %eventId)
{
    %request = sendRequest_EventInformation(%eventId, "onDoneOrErrorCallback_EventInfo");
    %request.frame = %this;
    return ;
}
function onDoneOrErrorCallback_EventInfo(%request)
{
    %status = %request.getResult("status");
    %imgFrame = %request.frame;
    if (!(%status $= "success"))
    {
        error("client Event info request HTTP status: " @ %status);
        return ;
    }
    %imageURL = %request.getValue("photo.url");
    if (%imageURL $= "")
    {
        %imageURL = "http://" @ $Net::BaseDomain @ "/images/events/default_banner_L.jpg";
    }
    %imageCaption = %request.getValue("photo.caption");
    %imgFrame.Caption = %imageCaption;
    %imgFrame.downloadAndApplyImage(%imageURL);
    return ;
}
function dlMgrErrorCallback_ImageFrameBase(%dlItem)
{
    %obj = %dlItem.callbackData;
    %playerName = %obj.getImageTag();
    %obj.setPortraitTexture("");
    if ((CustomSpaceClient::GetSpaceImIn() $= "") && CustomSpaceClient::isOwner())
    {
        %imgTag = %obj.getImageTag();
        if (%obj.type == $ImageFrameBase::Type_URL)
        {
            %message = strreplace($MsgCat::furniture["IMAGEFRAME-LOADFAILEDURL"], "[URL]", %imgTag);
        }
        else
        {
            if ((%obj.type == $ImageFrameBase::Type_Gallery) && (%obj.type == $ImageFrameBase::Type_Gallery2))
            {
                %message = strreplace($MsgCat::furniture["IMAGEFRAME-LOADFAILEDGALLERY"], "[GUID]", %imgTag);
            }
            else
            {
                if (%obj.type == $ImageFrameBase::Type_Event)
                {
                    %message = strreplace($MsgCat::furniture["IMAGEFRAME-LOADFAILEDEVENT"], "[EVENT]", %imgTag);
                }
                else
                {
                    %info = PlayerInfoMap.get(%imgTag);
                    if (isObject(%info))
                    {
                        %obj.showDefaultPlayerPortrait(%info);
                    }
                    else
                    {
                        requestPlayerInfoForWithCallback(%imgTag, "ImageFrameBase_gotInfoPlayerSex", %obj);
                    }
                    %message = "";
                }
            }
        }
        if (!(%message $= ""))
        {
            handleSystemMessage("msgInfoMessage", %message);
        }
    }
    return ;
}
function ImageFrameBase_gotInfoDoMenu(%playerName, %info, %frame)
{
    if (isObject(%info))
    {
        PlayerContextMenu.initWithPlayerName(%playerName);
        PlayerContextMenu.showAtPoint(Canvas.getCursorPos());
    }
    return ;
}
function ImageFrameBase_gotInfoPlayerSex(%playerName, %info, %frame)
{
    echo("ImageFrameBase_gotInfoPlayerSex( \"" @ %playerName @ "\", " @ %info @ ")");
    if (isObject(%info))
    {
        %frame.showDefaultPlayerPortrait(%info);
    }
    else
    {
        %message = strreplace($MsgCat::furniture["IMAGEFRAME-LOADFAILEDUSER"], "[USER]", %playerName);
        handleSystemMessage("msgInfoMessage", %message);
    }
    return ;
}
function ImageFrameBase::showDefaultPlayerPortrait(%this, %playerInfo)
{
    %url = "http://" @ $Net::BaseDomain @ "/images/defaults/avatar_" @ %playerInfo.gender @ "_large.jpg";
    %this.downloadAndApplyImage(%url);
    return ;
}
