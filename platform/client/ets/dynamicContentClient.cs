$DC::staffSnapshotRegion = "";
function staffSnapshot(%region) {
    $DC::staffSnapshotRegion = %region;
    commandToServer('getStaffSnapshotObj');
};
function clientCmdsetStaffSnapshotObj(%id) {
    %obj = %id.resolveGhostID(ServerConnection);
    if (!(isObject(%obj))) {
        log("general", "warn", "clientCmdsetStaffSnapshot: invalid object id.");
        return;
    }
    if (!(%obj.getIsDC())) {
        log("general", "warn", "clientCmdsetStaffSnapshotObj: this is not a valid DC object to take a snapshot on.");
        return;
    }
    $screenShotNum = ($screenShotNum + 1.0);
    %fileName = "staffsnapshot_" @ getSubStr(getTimeStamp(), 0, 17) @ "_" @ ".jpg";
    %uplocal = $DC::dcFolder @ "/" @ %fileName;
    shootscreen(%uplocal, $DC::staffSnapshotRegion);
    %downurl = $DC::DownloadFolder @ "/" @ %fileName;
    %downlocal = %uplocal;
    $DC::UploadScript.setUploadURL(%obj.getDCObject());
    %uplocal.setUploadLocalFilename(%obj.getDCObject());
    %downurl.setDownloadURL(%obj.getDCObject());
    %downlocal.setDownloadLocalFilename(%obj.getDCObject());
    %obj.getDCObject().startDCUpload();
};
function dlMgrCallback_GetNewSkin(%dlItem, %unused) {
    %dlData = %dlItem.callbackData;
    %fileName = %dlData.localFilename;
    echoDebug("dlMgrCallback_GetNewSkin: Successfully completed dynamic download: " @ %fileName);
    setNewSkin(%fileName, %dlData.shapebaseobj);
};
function clientCmdgetNewSkin(%skinName, %shapebaseobj) {
    %shapebaseobj = %shapebaseobj.resolveGhostID(ServerConnection);
    %skinName = getTaggedString(%skinName);
    %fileName = %skinName @ ".jpg";
    %url = $DC::RemoteSkinsFolder @ "/" @ %fileName;
    %item = new ScriptObject("");;
    0;
    %item.skinName = %skinName;
    %item.shapebaseobj = %shapebaseobj;
    "".applyUrl(dlMgr, %url, "dlMgrCallback_GetNewSkin", "", %item);
};
function setNewSkin(%skinName, %shapebaseobj) {
    if (!(isObject(%shapebaseobj))) {
        return;
    }
    %skinName.setSkinName(%shapebaseobj);
    echo("setNewSkin: Successfully applied new skin: " @ %skinName @ " to shapebase: " @ %shapebaseobj.getId());
};
$DC::marqueeSeq1 = 0;
$DC::marqueeSeq2 = 1;
function pushMarquee(%unused) {
    $DC::marqueeSeq1 = ($DC::marqueeSeq1 + 1.0);
    %fileName = "announcement" @ ".marquee.gardenbox.png";
    commandToServer('PushNewMarquee', addTaggedString(%fileName));
    if (($DC::marqueeSeq1 == 5.0)) {
        $DC::marqueeSeq1 = 0;
    }
    %fileName = $DC::marqueeSeq2 @ ".marqueeBorder.png";
    commandToServer('PushNewMarquee', addTaggedString(%fileName));
    $DC::marqueeSeq2 = ($DC::marqueeSeq2 + 1.0);
    if (($DC::marqueeSeq2 > 2.0)) {
        $DC::marqueeSeq2 = 1;
    }
};
