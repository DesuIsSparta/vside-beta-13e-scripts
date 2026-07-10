$adLogFileName = "";
$adLogCreationTimestamp = "";
function getAdLogName(%timeStamp) {
    %ServerName = stripVeryAgressively($Pref::Server::Name);
    if ((%ServerName $= "")) {
        %ServerName = "unknownServer";
    }
    %s = $userMods @ "/server/" @ $Pref::Server::adLogFolder @ "/" @ %ServerName @ "/" @ $Pref::Server::adLogBaseName;
    if ($Pref::Server::adLogAppendTimeStamp) {
        %s = %s @ "_" @ %timeStamp;
    }
    %s = %s @ ".log";
    return %s;
};
function checkNewAdLogFile() {
    if (($adLogCreationTimestamp $= "")) {
        return;
    }
    %minsLog = getSubStr($adLogCreationTimestamp, 0, 8);
    %minsNow = getSubStr(getTimeStamp(), 0, 8);
    if ((strcmp(%minsNow, %minsLog) > 0.0)) {
        $adLogFileName = "";
        $adLogCreationTimestamp = "";
    }
    return;
};
function initAdLogFile() {
    checkNewAdLogFile();
    if (!($adLogFileName $= "")) {
        return $adLogFileName;
    }
    %ts = getTimeStamp();
    %ts = getSubStr(%ts, 0, 17);
    $adLogFileName = getAdLogName(%ts);
    $adLogCreationTimestamp = %ts;
    %file = new FileObject("");
    if ($adLogFileName.openForAppend(%file)) {
        "# Evil Twin Ads Log File".writeLine(%file);
        "#" @ " " @ $adLogFileName.writeLine(%file);
        "#" @ " " @ %ts.writeLine(%file);
        "# Action: <tab> timestamp <tab> playername <tab> imagename <tab> (image-click-coords) <tab> (advert transform) <tab> (advert scale)".writeLine(%file);
        "".writeLine(%file);
        %file.close();
        echo("AdLog started:" @ " " @ $adLogFileName);
    }
    echo("Error opening logfile:" @ " " @ $adLogFileName);
    %file.delete();
    return $adLogFileName;
};
function appendAdLogLine(%line) {
    %fn = initAdLogFile();
    %file = new FileObject("");
    if (%fn.openForAppend(%file)) {
        %line.writeLine(%file);
        %file.close();
    }
    echo("Error opening logfile:" @ " " @ %fn);
    echo(%line);
    %file.delete();
    return;
};
function serverCmdAdvertClick(%client, %ghostIndexClnt, %pt) {
    %playerName = detag(%client.Player.getShapeName());
    %obj = %ghostIndexClnt.resolveObjectFromGhostIndex(%client);
    %tex = %obj.getSkinName();
    %line = "AdClick: ";
    %line = %line @ "\t" @ getTimeStamp();
    %line = %line @ "\t" @ stripUnprintables(%playerName);
    %line = %line @ "\t" @ %obj.getSkinName();
    %line = %line @ "\t" @ %pt;
    %line = %line @ "\t" @ "(" @ %obj.getTransform() @ ")" @ "\t" @ "(" @ %obj.getScale() @ ")";
    appendAdLogLine(%line);
    return;
};
function serverCmdAdvertFollow(%client, %url) {
    %playerName = detag(%client.Player.getShapeName());
    %line = "AdFollow:";
    %line = %line @ "\t" @ getTimeStamp();
    %line = %line @ "\t" @ stripUnprintables(%playerName);
    %line = %line @ "\t" @ %url;
    appendAdLogLine(%line);
    return;
};
function AdGroup::init(%this) {
    %this.num = 0;
    %this.dtsNum = 0;
    %this.sort = forward;
    %this.offset = 0;
    return;
};
function AdGroup::addAd(%this, %texture, %title, %url) {
    %this.textures = %texture @ %this.num;
    %this.titles = %title @ %this.num;
    %this.urls = %url @ %this.num;
    %this.num = (%this.num + 1.0);
    return;
};
function AdGroup::addDTSGroup(%this, %grp) {
    %this.dtss = %grp @ %this.dtsNum;
    %this.dtsNum = (%this.dtsNum + 1.0);
    return;
};
function AdGroup::doSwap(%this) {
    %this.offset = (%this.offset + 1.0);
    if ((%this.offset >= %this.num)) {
        %this.offset = 0;
    }
    %adNum = %this.offset;
    %numAds = 0;
    %gn = 0;
    while ((%gn < %this.dtsNum)) {
        %dtsGrp = %this.dtss;
        %gn;
        %dtsNum = %dtsGrp.getCount();
        %numAds = (%numAds + %dtsNum);
        %dn = 0;
        while ((%dn < %dtsNum)) {
            %dts = %dn.getObject(%dtsGrp);
            if ((%this.sort $= "random")) {
                %adNum = getRandom((%this.num - 1.0));
                if ((%adNum == %dts.prevAdNum)) {
                    %adNum = getRandom((%this.num - 1.0));
                }
                if ((%adNum == %dts.prevAdNum)) {
                    %adNum = getRandom((%this.num - 1.0));
                }
                %dts.prevAdNum = %adNum;
            }
            %this.textures.setSkinName(%dts, %adNum);
            %this.titles.setTitle(%dts, %adNum);
            %this.urls.setBasicURL(%dts, %adNum);
            %adNum = (%adNum + 1.0);
            if ((%adNum >= %this.num)) {
                %adNum = 0;
            }
            %dn = (%dn + 1.0);
        }
        %gn = (%gn + 1.0);
        (%dn < %dtsNum);
    }
    return %numAds;
};
function AdManager::doSwap(%this) {
    %numAds = 0;
    %g = 0;
    while ((%g < %this.adGrpsNum)) {
        %numAds = (%numAds + %this.adGrps.doSwap(%g));
        %g = (%g + 1.0);
    }
    return %numAds;
};
function AdManager::think(%this) {
    if ((%this.doSwap() > 0.0)) {
    }
    if ((%this.periodSecs > 0.0)) {
        think.schedule(%this, (%this.periodSecs * 1000.0));
    }
    echo("Putting AdManager to sleep..");
    return;
};
function AdManager::newAdGroup(%this) {
    %adGrp = new ScriptObject("") {
        class = AdGroup;
        manager = %this;
    };
    %adGrp.init();
    %this.adGrps = %adGrp @ %this.adGrpsNum;
    %this.adGrpsNum = (%this.adGrpsNum + 1.0);
    return %adGrp;
};
