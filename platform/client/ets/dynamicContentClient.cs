$DC::staffSnapshotRegion = "";
function staffSnapshot(%region)
{
    $DC::staffSnapshotRegion = %region;
    commandToServer('getStaffSnapshotObj');
    return ;
}
function clientCmdsetStaffSnapshotObj(%id)
{
    %obj = ServerConnection.resolveGhostID(%id);
    if (!isObject(%obj))
    {
        log("general", "warn", "clientCmdsetStaffSnapshot: invalid object id.");
        return ;
    }
    if (!%obj.getIsDC())
    {
        log("general", "warn", "clientCmdsetStaffSnapshotObj: this is not a valid DC object to take a snapshot on.");
        return ;
    }
    %fileName = "staffsnapshot_" @ getSubStr(getTimeStamp(), 0, 17) @ "_" @ $screenShotNum = $screenShotNum + 1 @ ".jpg";
    %uplocal = $DC::dcFolder @ "/" @ %fileName;
    shootscreen(%uplocal, $DC::staffSnapshotRegion);
    %downurl = $DC::DownloadFolder @ "/" @ %fileName;
    %downlocal = %uplocal;
    %obj.getDCObject().setUploadURL($DC::UploadScript);
    %obj.getDCObject().setUploadLocalFilename(%uplocal);
    %obj.getDCObject().setDownloadURL(%downurl);
    %obj.getDCObject().setDownloadLocalFilename(%downlocal);
    %obj.getDCObject().startDCUpload();
    return ;
}
function dlMgrCallback_GetNewSkin(%dlItem, %unused)
{
    %dlData = %dlItem.callbackData;
    %fileName = %dlData.localFilename;
    echoDebug("dlMgrCallback_GetNewSkin: Successfully completed dynamic download: " @ %fileName);
    setNewSkin(%fileName, %dlData.shapebaseobj);
    return ;
}
function clientCmdgetNewSkin(%skinName, %shapebaseobj)
{
    %shapebaseobj = ServerConnection.resolveGhostID(%shapebaseobj);
    %skinName = getTaggedString(%skinName);
    %fileName = %skinName @ ".jpg";
    %url = $DC::RemoteSkinsFolder @ "/" @ %fileName;
    %item = new ScriptObject();
    %item.skinName = %skinName;
    %item.shapebaseobj = %shapebaseobj;
    dlMgr.applyUrl(%url, "dlMgrCallback_GetNewSkin", "", %item, "");
    return ;
}
function setNewSkin(%skinName, %shapebaseobj)
{
    if (!isObject(%shapebaseobj))
    {
        return ;
    }
    %shapebaseobj.setSkinName(%skinName);
    echo("setNewSkin: Successfully applied new skin: " @ %skinName @ " to shapebase: " @ %shapebaseobj.getId());
    return ;
}
$DC::marqueeSeq1 = 0;
$DC::marqueeSeq2 = 1;
function pushMarquee(%unused)
{
    %fileName = "announcement" @ $DC::marqueeSeq1 = $DC::marqueeSeq1 + 1 @ ".marquee.gardenbox.png";
    commandToServer('PushNewMarquee', addTaggedString(%fileName));
    if ($DC::marqueeSeq1 == 5)
    {
        $DC::marqueeSeq1 = 0;
    }
    %fileName = $DC::marqueeSeq2 @ ".marqueeBorder.png";
    commandToServer('PushNewMarquee', addTaggedString(%fileName));
    $DC::marqueeSeq2 = $DC::marqueeSeq2 + 1;
    if ($DC::marqueeSeq2 > 2)
    {
        $DC::marqueeSeq2 = 1;
    }
    return ;
}
