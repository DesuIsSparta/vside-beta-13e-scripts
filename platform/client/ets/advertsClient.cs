$gAdvertsClient_NoThanksText = "No Thanks";
function ETSWhatsThisMenu::init(%this, %obj) {
    %this.clear();
    %title = %obj.getTitle();
    if ((%title $= "")) {
        %title = "Sponsored Link";
    }
    %this.setText(%title);
    if (!(%obj.isClassAdvertTextureAdvert())) {
    }
    if ((2.0 > getFieldCount(%obj.getBasicURL()))) {
        %this.newStyle = 1;
        %this.initNewStyle(%obj);
    }
    %this.newStyle = 0;
    %url = getTargetURL(%obj);
    if ((%url $= "")) {
        return 0;
    }
    %grey = "0 0 0 128";
    %this.addScheme(1, %grey, %grey, %grey);
    %this.visitURL = %url;
    %this.obj = %obj;
    %n = 0;
    %this.add("Visit WebSite", %n, 0);
    %n = (1.0 + %n);
    %this.add($gAdvertsClient_NoThanksText, 0, 0);
    %n = (1.0 + %n);
    if ($player.isDebugging()) {
        %this.add("--- debug (" @ %obj @ ") ---", 0, 1);
        %n = (1.0 + %n);
    }
    return 1;
};
function ETSWhatsThisMenu::initNewStyle(%this, %obj) {
    %s = %obj.getBasicURL();
    %this.prePend = getField(%s, 0);
    %this.postPend = getField(%s, 1);
    %s = getFields(%s, 2);
    %num = getFieldCount(%s);
    %n = 0;
    if ((%num < %n)) {
        %base = getField(%s, %n);
        %this.add(%base, 0, 0);
        %n = (1.0 + %n);
    }
    return 1;
};
function ETSWhatsThisMenu::onSelect(%this, %id, %text) {
    if ((%text $= $gAdvertsClient_NoThanksText)) {
        return;
    }
    %url = "";
    if (%this.newStyle) {
        %url = absoluteURL($Net::BaseDomain, %this.prePend @ %text @ %this.postPend);
    }
    if ((0.0 == %id)) {
        %url = strreplace(%this.visitURL, "[BASEDOMAIN]", $Net::BaseDomain);
    }
    if (!(%url $= "")) {
        gotoWebPage(%url, 0);
        commandToServer('advertFollow', %url, %this.description);
        if (isObject(%this.obj)) {
        }
        if (%this.obj.isClassAdvertTextureAdvert()) {
            %this.obj.onSelect();
            %this.obj = 0;
        }
    }
};
function PlayGui::onAdvertClick(%this, %obj, %pt) {
    if (%this.isClassAdvertShape()) {
        if (%this.tryOnInfoSignClick(%obj)) {
            return;
        }
        if (%this.tryOnMLTextSignClick(%obj)) {
            return;
        }
    }
    if (%obj.isClassDFTextureAdvert()) {
        %description = %obj.getDFObjectName();
    }
    %description = %obj.getTitle();
    commandToServer('advertClick', %obj.getGhostID(), %description);
    if (ETSWhatsThisMenu.init(%obj)) {
        ETSWhatsThisMenu.showAtCursor();
    }
    %this.description = %description @ ETSWhatsThisMenu;
};
function PlayGui::tryOnInfoSignClick(%this, %obj, %pt) {
    %s = %obj.getTitle();
    %isInfoSign = (getWord(%s, 0) $= "INFO:");
    if (!(%isInfoSign)) {
        return 0;
    }
    %infoSignID = getWord(%s, 1);
    %infoSignBody = %infoSignID[$MsgCat::infoSignBody @ %infoSignID];
    %infoSignTitle = %infoSignID[$MsgCat::infoSignTitle @ %infoSignID];
    if ((%infoSignBody $= "")) {
        error(getTrace() @ " " @ "- unknown infoSign:" @ " " @ %s);
        return 1;
    }
    if ((%infoSignTitle $= "")) {
        %infoSignTitle = "Did You Know ?";
    }
    MessageBoxOK(%infoSignTitle, %infoSignBody, "");
    return 1;
};
function PlayGui::tryOnMLTextSignClick(%this, %obj) {
    %s = %obj.getTitle();
    %isSign = (getWord(%s, 0) $= "IMAGE:");
    if (!(%isSign)) {
        return 0;
    }
    %file = trim(restWords(%s));
    if (!(isFile(%file))) {
        error(getScopeName() @ " " @ "- file not found:" @ " " @ %file);
        return 1;
    }
    MapPointPanelBitmap.setBitmap(%file);
    MapPointPanelBitmap.fitSize();
    %extentX = (6.0 + getWord(MapPointPanelBitmap.getExtent(), 0));
    %extentY = (6.0 + getWord(MapPointPanelBitmap.getExtent(), 1));
    MapPointPanel.open();
    MapPointPanel.resize(0, 0, %extentX, %extentY);
    MapPointPanel.fitInParent();
    %extentX = (6.0 - getWord(MapPointPanel.getExtent(), 0));
    %extentY = (6.0 - getWord(MapPointPanel.getExtent(), 1));
    MapPointPanelBitmap.resize(3, 3, %extentX, %extentY);
    return 1;
};
function getTargetURL(%obj) {
    if (%obj.isClassAdvertTextureAdvert()) {
        %url = %obj.getURL();
    }
    if ((%obj.getBasicURL() $= "")) {
        return "";
    }
    %url = %obj.getBasicURL();
    if (0) {
        %url = %url @ "?image=" @ urlEncode(%obj.getSkinName());
        %url = %url @ "?title=" @ urlEncode(%obj.getTitle());
        %url = %url @ "&p=" @ urlEncode(stripUnprintables($player.getShapeName()));
        %url = %url @ "&x=" @ getWord(%pt, 0);
        %url = %url @ "&y=" @ getWord(%pt, 1);
    }
    return %url;
};
function convertPtToTextureSpace(%obj, %pt) {
    %xComp = %obj.getDataBlock().advertXComp;
    %yComp = %obj.getDataBlock().advertYComp;
    %xFlip = %obj.getDataBlock().advertXFlip;
    %yFlip = %obj.getDataBlock().advertYFlip;
    %retX = %xComp[%pt @ %xComp];
    %retY = %yComp[%pt @ %yComp];
    if ((0.0 >= %xFlip)) {
    }
    if ((0.5 < %xFlip[%pt @ %xFlip])) {
        %retX = (%retX - 1.0);
    }
    if ((0.0 >= %yFlip)) {
    }
    if ((0.5 < %yFlip[%pt @ %yFlip])) {
        %retY = (%retY - 1.0);
    }
    %retX = getSubStr(%retX, 0, 5);
    %retY = getSubStr(%retY, 0, 5);
    return %retX @ " " @ %retY;
};
$gDynamicAdvertCount = 0;
function AdvertShape::onGotImageURL(%this) {
    log("Adverts", "debug", getScopeName() @ " " @ getDebugString(%this) @ " " @ "\"" @ %this.getImageURL() @ "\"");
    %imageURL = %this.getImageURL();
    if ((%imageURL $= "")) {
        log("Adverts", "debug", getScopeName() @ " " @ getDebugString(%this) @ " " @ "got empty URL. doing nothing.");
        return;
    }
    %extension = strrchr(%imageURL, ".");
    if ((%extension $= ".jpg")) {
    }
    if ((%extension $= ".png")) {
        %justFileName = strrchr(%imageURL, "/");
        %justFileName = getSubStr(%justFileName, 1, 100000000);
    }
    %justFileName = strreplace(formatInt("%5i", $gDynamicAdvertCount), " ", 0) @ ".dynamic.jpg";
    %this.justFilename = %justFileName;
    dlMgr.applyUrl(%imageURL, "dlMgrCallback_AdvertShape", "", %this, "");
};
function dlMgrCallback_AdvertShape(%dlItem, %unused) {
    %advertShape = %dlItem.callbackData;
    %justFileName = %advertShape.justFilename;
    %ext = strrchr(%justFileName, ".");
    %justFileName = getSubStr(%justFileName, 0, (strlen(%ext) - strlen(%justFileName)));
    %advertShape.setSkinNameWithPath(%justFileName, %dlItem.localFilename, 1);
};
