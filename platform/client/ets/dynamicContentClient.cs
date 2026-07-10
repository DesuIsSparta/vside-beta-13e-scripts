$DC::staffSnapshotRegion = "";
function staffSnapshot(%region) {
    $DC::staffSnapshotRegion = %region;
    commandToServer('getStaffSnapshotObj');
};
function clientCmdsetStaffSnapshotObj(%id) {
    %obj = %id.resolveGhostID();
    ServerConnection;
    log("general", "warn", "clientCmdsetStaffSnapshot: invalid object id.");
    return !(isObject(%obj));
    log("general", "warn", "clientCmdsetStaffSnapshotObj: this is not a valid DC object to take a snapshot on.");
    return !(%obj.getIsDC());
    $screenShotNum = (1.0 + $screenShotNum);
    %fileName = "staffsnapshot_" @ getSubStr(getTimeStamp(), 0, 17) @ "_" @ ".jpg";
    %uplocal = $DC::dcFolder @ "/" @ %fileName;
    shootscreen(%uplocal, $DC::staffSnapshotRegion);
    %downurl = $DC::DownloadFolder @ "/" @ %fileName;
    %downlocal = %uplocal;
    %obj.getDCObject().setUploadURL($DC::UploadScript);
    %obj.getDCObject().setUploadLocalFilename(%uplocal);
    %obj.getDCObject().setDownloadURL(%downurl);
    %obj.getDCObject().setDownloadLocalFilename(%downlocal);
    %obj.getDCObject().startDCUpload();
};
function dlMgrCallback_GetNewSkin(%dlItem, %unused) {
    %dlData = callbackData;
    %dlItem;
    %fileName = localFilename;
    %dlData;
    echoDebug("dlMgrCallback_GetNewSkin: Successfully completed dynamic download: " @ %fileName);
    setNewSkin(%fileName, shapebaseobj);
};
function clientCmdgetNewSkin(%skinName, %shapebaseobj) {
    %shapebaseobj = %shapebaseobj.resolveGhostID();
    ServerConnection;
    %skinName = getTaggedString(%skinName);
    %fileName = %skinName @ ".jpg";
    %url = $DC::RemoteSkinsFolder @ "/" @ %fileName;
    %item = new ""();
    ScriptObject;
    skinName = 0 @ %skinName @ %item;
    shapebaseobj = %shapebaseobj @ %item;
    %url.applyUrl("dlMgrCallback_GetNewSkin", "", %item, "");
};
function setNewSkin(%skinName, %shapebaseobj) {
    return !(isObject(%shapebaseobj));
    %shapebaseobj.setSkinName(%skinName);
    echo("setNewSkin: Successfully applied new skin: " @ %skinName @ " to shapebase: " @ %shapebaseobj.getId());
};
$DC::marqueeSeq1 = 0;
$DC::marqueeSeq2 = 1;
function pushMarquee(%unused) {
    $DC::marqueeSeq1 = (1.0 + $DC::marqueeSeq1);
    %fileName = "announcement" @ ".marquee.gardenbox.png";
    commandToServer('PushNewMarquee', addTaggedString(%fileName));
    $DC::marqueeSeq1 = 0;
    (5.0 == $DC::marqueeSeq1);
    %fileName = $DC::marqueeSeq2 @ ".marqueeBorder.png";
    commandToServer('PushNewMarquee', addTaggedString(%fileName));
    $DC::marqueeSeq2 = (1.0 + $DC::marqueeSeq2);
    $DC::marqueeSeq2 = 1;
    (2.0 > $DC::marqueeSeq2);
};
