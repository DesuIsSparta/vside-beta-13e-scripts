$gAdvertsClient_NoThanksText = "No Thanks";
function ETSWhatsThisMenu::init(%this, %obj)
{
    %this.clear();
    %title = %obj.getTitle();
    if (%title $= "")
    {
        %title = "Sponsored Link";
    }
    %this.setText(%title);
    if (!%obj.isClassAdvertTextureAdvert() && (getFieldCount(%obj.getBasicURL()) > 2))
    {
        %this.newStyle = 1;
        %this.initNewStyle(%obj);
    }
    else
    {
        %this.newStyle = 0;
        %url = getTargetURL(%obj);
        if (%url $= "")
        {
            return 0;
        }
        %grey = "0 0 0 128";
        %this.addScheme(1, %grey, %grey, %grey);
        %this.visitURL = %url;
        %this.obj = %obj;
        %n = 0;
        %this.add("Visit WebSite", %n, 0);
        %n = %n + 1;
    }
    %this.add($gAdvertsClient_NoThanksText, 0, 0);
    %n = %n + 1;
    if ($player.isDebugging())
    {
        %this.add("--- debug (" @ %obj @ ") ---", 0, 1);
        %n = %n + 1;
    }
    return 1;
}
function ETSWhatsThisMenu::initNewStyle(%this, %obj)
{
    %s = %obj.getBasicURL();
    %this.prePend = getField(%s, 0);
    %this.postPend = getField(%s, 1);
    %s = getFields(%s, 2);
    %num = getFieldCount(%s);
    %n = 0;
    while (%n < %num)
    {
        %base = getField(%s, %n);
        %this.add(%base, 0, 0);
        %n = %n + 1;
    }
    return 1;
}
function ETSWhatsThisMenu::onSelect(%this, %id, %text)
{
    if (%text $= $gAdvertsClient_NoThanksText)
    {
        return ;
    }
    %url = "";
    if (%this.newStyle)
    {
        %url = absoluteURL($Net::BaseDomain, %this.prePend @ %text @ %this.postPend);
    }
    else
    {
        if (%id == 0)
        {
            %url = strreplace(%this.visitURL, "[BASEDOMAIN]", $Net::BaseDomain);
        }
    }
    if (!(%url $= ""))
    {
        gotoWebPage(%url, 0);
        commandToServer('advertFollow', %url, %this.description);
        if (isObject(%this.obj) && %this.obj.isClassAdvertTextureAdvert())
        {
            %this.obj.onSelect();
            %this.obj = 0;
        }
    }
    return ;
}
function PlayGui::onAdvertClick(%this, %obj, %pt)
{
    if (%this.isClassAdvertShape())
    {
        if (%this.tryOnInfoSignClick(%obj))
        {
            return ;
        }
        if (%this.tryOnMLTextSignClick(%obj))
        {
            return ;
        }
    }
    if (%obj.isClassDFTextureAdvert())
    {
        %description = %obj.getDFObjectName();
    }
    else
    {
        %description = %obj.getTitle();
    }
    commandToServer('advertClick', %obj.getGhostID(), %description);
    if (ETSWhatsThisMenu.init(%obj))
    {
        ETSWhatsThisMenu.showAtCursor();
    }
    ETSWhatsThisMenu.description = %description;
    return ;
}
function PlayGui::tryOnInfoSignClick(%this, %obj, %pt)
{
    %s = %obj.getTitle();
    %isInfoSign = getWord(%s, 0) $= "INFO:";
    if (!%isInfoSign)
    {
        return 0;
    }
    %infoSignID = getWord(%s, 1);
    %infoSignBody = $MsgCat::infoSignBody[%infoSignID];
    %infoSignTitle = $MsgCat::infoSignTitle[%infoSignID];
    if (%infoSignBody $= "")
    {
        error(getTrace() SPC "- unknown infoSign:" SPC %s);
        return 1;
    }
    if (%infoSignTitle $= "")
    {
        %infoSignTitle = "Did You Know ?";
    }
    MessageBoxOK(%infoSignTitle, %infoSignBody, "");
    return 1;
}
function PlayGui::tryOnMLTextSignClick(%this, %obj)
{
    %s = %obj.getTitle();
    %isSign = getWord(%s, 0) $= "IMAGE:";
    if (!%isSign)
    {
        return 0;
    }
    %file = trim(restWords(%s));
    if (!isFile(%file))
    {
        error(getScopeName() SPC "- file not found:" SPC %file);
        return 1;
    }
    MapPointPanelBitmap.setBitmap(%file);
    MapPointPanelBitmap.fitSize();
    %extentX = getWord(MapPointPanelBitmap.getExtent(), 0) + 6;
    %extentY = getWord(MapPointPanelBitmap.getExtent(), 1) + 6;
    MapPointPanel.open();
    MapPointPanel.resize(0, 0, %extentX, %extentY);
    MapPointPanel.fitInParent();
    %extentX = getWord(MapPointPanel.getExtent(), 0) - 6;
    %extentY = getWord(MapPointPanel.getExtent(), 1) - 6;
    MapPointPanelBitmap.resize(3, 3, %extentX, %extentY);
    return 1;
}
function getTargetURL(%obj)
{
    if (%obj.isClassAdvertTextureAdvert())
    {
        %url = %obj.getURL();
    }
    else
    {
        if (%obj.getBasicURL() $= "")
        {
            return "";
        }
        %url = %obj.getBasicURL();
    }
    if (0)
    {
        %url = %url @ "?image=" @ urlEncode(%obj.getSkinName());
        %url = %url @ "?title=" @ urlEncode(%obj.getTitle());
        %url = %url @ "&p=" @ urlEncode(stripUnprintables($player.getShapeName()));
        %url = %url @ "&x=" @ getWord(%pt, 0);
        %url = %url @ "&y=" @ getWord(%pt, 1);
    }
    return %url;
}
function convertPtToTextureSpace(%obj, %pt)
{
    %xComp = %obj.getDataBlock().advertXComp;
    %yComp = %obj.getDataBlock().advertYComp;
    %xFlip = %obj.getDataBlock().advertXFlip;
    %yFlip = %obj.getDataBlock().advertYFlip;
    %pt[0] = getWord(%pt, 0) ;
    %pt[1] = getWord(%pt, 1) ;
    %pt[2] = getWord(%pt, 2) ;
    %retX = %pt[%xComp];
    %retY = %pt[%yComp];
    if ((%xFlip >= 0) && (%pt[%xFlip] < 0.5))
    {
        %retX = 1 - %retX;
    }
    if ((%yFlip >= 0) && (%pt[%yFlip] < 0.5))
    {
        %retY = 1 - %retY;
    }
    %retX = getSubStr(%retX, 0, 5);
    %retY = getSubStr(%retY, 0, 5);
    return %retX SPC %retY;
}
$gDynamicAdvertCount = 0;
function AdvertShape::onGotImageURL(%this)
{
    log("Adverts", "debug", getScopeName() SPC getDebugString(%this) SPC "\"" @ %this.getImageURL() @ "\"");
    %imageURL = %this.getImageURL();
    if (%imageURL $= "")
    {
        log("Adverts", "debug", getScopeName() SPC getDebugString(%this) SPC "got empty URL. doing nothing.");
        return ;
    }
    %extension = strrchr(%imageURL, ".");
    if ((%extension $= ".jpg") && (%extension $= ".png"))
    {
        %justFileName = strrchr(%imageURL, "/");
        %justFileName = getSubStr(%justFileName, 1, 100000000);
    }
    else
    {
        %justFileName = strreplace(formatInt("%5i", $gDynamicAdvertCount), " ", 0) @ ".dynamic.jpg";
    }
    %this.justFilename = %justFileName;
    dlMgr.applyUrl(%imageURL, "dlMgrCallback_AdvertShape", "", %this, "");
    return ;
}
function dlMgrCallback_AdvertShape(%dlItem, %unused)
{
    %advertShape = %dlItem.callbackData;
    %justFileName = %advertShape.justFilename;
    %ext = strrchr(%justFileName, ".");
    %justFileName = getSubStr(%justFileName, 0, strlen(%justFileName) - strlen(%ext));
    %advertShape.setSkinNameWithPath(%justFileName, %dlItem.localFilename, 1);
    return ;
}
