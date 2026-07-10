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
    if ((0.0 > strcmp(%minsNow, %minsLog))) {
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
    %file = new FileObject("");;
    0;
    if (%file.openForAppend($adLogFileName)) {
        %file.writeLine("# Evil Twin Ads Log File");
        %file.writeLine("#" @ " " @ $adLogFileName);
        %file.writeLine("#" @ " " @ %ts);
        %file.writeLine("# Action: <tab> timestamp <tab> playername <tab> imagename <tab> (image-click-coords) <tab> (advert transform) <tab> (advert scale)");
        %file.writeLine("");
        %file.close();
        echo("AdLog started:" @ " " @ $adLogFileName);
    }
    echo("Error opening logfile:" @ " " @ $adLogFileName);
    %file.delete();
    return $adLogFileName;
};
function appendAdLogLine(%line) {
    %fn = initAdLogFile();
    %file = new FileObject("");;
    0;
    if (%file.openForAppend(%fn)) {
        %file.writeLine(%line);
        %file.close();
    }
    echo("Error opening logfile:" @ " " @ %fn);
    echo(%line);
    %file.delete();
    return;
};
function serverCmdAdvertClick(%client, %ghostIndexClnt, %pt) {
    %playerName = detag(%client.Player.getShapeName());
    %obj = %client.resolveObjectFromGhostIndex(%ghostIndexClnt);
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
    %this.num = (1.0 + %this.num);
    return;
};
function AdGroup::addDTSGroup(%this, %grp) {
    %this.dtss = %grp @ %this.dtsNum;
    %this.dtsNum = (1.0 + %this.dtsNum);
    return;
};
function AdGroup::doSwap(%this) {
    %this.offset = (1.0 + %this.offset);
    if ((%this.num >= %this.offset)) {
        %this.offset = 0;
    }
    %adNum = %this.offset;
    %numAds = 0;
    %gn = 0;
    if ((%this.dtsNum < %gn)) {
        %dtsGrp = %this.dtss;
        %gn;
        %dtsNum = %dtsGrp.getCount();
        %numAds = (%dtsNum + %numAds);
        %dn = 0;
        if ((%dtsNum < %dn)) {
            %dts = %dtsGrp.getObject(%dn);
            if ((%this.sort $= "random")) {
                %adNum = getRandom((1.0 - %this.num));
                if ((%dts.prevAdNum == %adNum)) {
                    %adNum = getRandom((1.0 - %this.num));
                }
                if ((%dts.prevAdNum == %adNum)) {
                    %adNum = getRandom((1.0 - %this.num));
                }
                %dts.prevAdNum = %adNum;
            }
            %dts.setSkinName(%adNum, %this.textures);
            %dts.setTitle(%adNum, %this.titles);
            %dts.setBasicURL(%adNum, %this.urls);
            %adNum = (1.0 + %adNum);
            if ((%this.num >= %adNum)) {
                %adNum = 0;
            }
            %dn = (1.0 + %dn);
        }
        %gn = (1.0 + %gn);
        (%dtsNum < %dn);
    }
    return %numAds;
};
function AdManager::doSwap(%this) {
    %numAds = 0;
    %g = 0;
    if ((%this.adGrpsNum < %g)) {
        %numAds = (%g.doSwap(%this.adGrps) + %numAds);
        %g = (1.0 + %g);
    }
    return %numAds;
};
function AdManager::think(%this) {
    if ((0.0 > %this.doSwap())) {
    }
    if ((0.0 > %this.periodSecs)) {
        %this.schedule((1000.0 * %this.periodSecs));
    }
    echo("Putting AdManager to sleep..");
    return think;
};
function AdManager::newAdGroup(%this) {
    %adGrp = new ScriptObject("") {
        class = 0 @ AdGroup;
        manager = %this;
    };
    %adGrp.init();
    %this.adGrps = %adGrp @ %this.adGrpsNum;
    %this.adGrpsNum = (1.0 + %this.adGrpsNum);
    return %adGrp;
};
