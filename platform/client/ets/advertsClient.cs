$gAdvertsClient_NoThanksText = "No Thanks";
function ETSWhatsThisMenu::init(%this, %obj) {
    %this.clear();
    %title = %obj.getTitle();
    if ((%title $= "")) {
        %title = "Sponsored Link";
    }
    %title.setText(%this);
    if (!(%obj.isClassAdvertTextureAdvert())) {
    }
    if ((getFieldCount(%obj.getBasicURL()) > 2.0)) {
        %this.newStyle = 1;
        %obj.initNewStyle(%this);
    }
    %this.newStyle = 0;
    %url = getTargetURL(%obj);
    if ((%url $= "")) {
        return 0;
    }
    %grey = "0 0 0 128";
    %grey.addScheme(%this, 1, %grey, %grey);
    %this.visitURL = %url;
    %this.obj = %obj;
    %n = 0;
    0.add(%this, "Visit WebSite", %n);
    %n = (%n + 1.0);
    0.add(%this, $gAdvertsClient_NoThanksText, 0);
    %n = (%n + 1.0);
    if ($player.isDebugging()) {
        1.add(%this, "--- debug (" @ %obj @ ") ---", 0);
        %n = (%n + 1.0);
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
    while ((%n < %num)) {
        %base = getField(%s, %n);
        0.add(%this, %base, 0);
        %n = (%n + 1.0);
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
    if ((%id == 0.0)) {
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
        if (%obj.tryOnInfoSignClick(%this)) {
            return;
        }
        if (%obj.tryOnMLTextSignClick(%this)) {
            return;
        }
    }
    if (%obj.isClassDFTextureAdvert()) {
        %description = %obj.getDFObjectName();
    }
    %description = %obj.getTitle();
    commandToServer('advertClick', %obj.getGhostID(), %description);
    if (%obj.init(ETSWhatsThisMenu)) {
        ETSWhatsThisMenu.showAtCursor();
    }
    ETSWhatsThisMenu.description = %description;
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
    %file.setBitmap(MapPointPanelBitmap);
    MapPointPanelBitmap.fitSize();
    %extentX = (getWord(MapPointPanelBitmap.getExtent(), 0) + 6.0);
    %extentY = (getWord(MapPointPanelBitmap.getExtent(), 1) + 6.0);
    MapPointPanel.open();
    %extentY.resize(MapPointPanel, 0, 0, %extentX);
    MapPointPanel.fitInParent();
    %extentX = (getWord(MapPointPanel.getExtent(), 0) - 6.0);
    %extentY = (getWord(MapPointPanel.getExtent(), 1) - 6.0);
    %extentY.resize(MapPointPanelBitmap, 3, 3, %extentX);
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
    %pt[0] = getWord(%pt, 0);
    %pt[1] = getWord(%pt, 1);
    %pt[2] = getWord(%pt, 2);
    %retX = %xComp[%pt @ %xComp];
    %retY = %yComp[%pt @ %yComp];
    if ((%xFlip >= 0.0)) {
    }
    if ((%xFlip[%pt @ %xFlip] < 0.5)) {
        %retX = (1.0 - %retX);
    }
    if ((%yFlip >= 0.0)) {
    }
    if ((%yFlip[%pt @ %yFlip] < 0.5)) {
        %retY = (1.0 - %retY);
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
    "".applyUrl(dlMgr, %imageURL, "dlMgrCallback_AdvertShape", "", %this);
};
function dlMgrCallback_AdvertShape(%dlItem, %unused) {
    %advertShape = %dlItem.callbackData;
    %justFileName = %advertShape.justFilename;
    %ext = strrchr(%justFileName, ".");
    %justFileName = getSubStr(%justFileName, 0, (strlen(%justFileName) - strlen(%ext)));
    1.setSkinNameWithPath(%advertShape, %justFileName, %dlItem.localFilename);
};
