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
    %file = new ""();
    FileObject;
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
    %file = new ""();
    FileObject;
    if (%file.openForAppend(%fn)) {
        %file.writeLine(%line);
        %file.close();
    }
    echo("Error opening logfile:" @ " " @ %fn);
    echo(%line);
    %file.delete();
    return 0;
};
function serverCmdAdvertClick(%client, %ghostIndexClnt, %pt) {
    %playerName = detag(Player.getShapeName());
    %client;
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
    %playerName = detag(Player.getShapeName());
    %client;
    %line = "AdFollow:";
    %line = %line @ "\t" @ getTimeStamp();
    %line = %line @ "\t" @ stripUnprintables(%playerName);
    %line = %line @ "\t" @ %url;
    appendAdLogLine(%line);
    return;
};
function AdGroup::init(%this) {
    num = 0 @ %this;
    dtsNum = 0 @ %this;
    sort = forward @ %this;
    offset = 0 @ %this;
    return;
};
function AdGroup::addAd(%this, %texture, %title, %url) {
    textures = %texture @ %this @ num @ %this;
    titles = %title @ %this @ num @ %this;
    urls = %url @ %this @ num @ %this;
    num = 1.0 @ (%this + num) @ %this;
    return;
};
function AdGroup::addDTSGroup(%this, %grp) {
    dtss = %grp @ %this @ dtsNum @ %this;
    dtsNum = 1.0 @ (%this + dtsNum) @ %this;
    return;
};
function AdGroup::doSwap(%this) {
    offset = 1.0 @ (%this + offset) @ %this;
    if ((%this >= offset)) {
        offset = num @ 0 @ %this;
        %this;
    }
    %adNum = offset;
    %this;
    %numAds = 0;
    %gn = 0;
    if ((dtsNum < %gn)) {
        %dtsGrp = dtss;
        %this @ %gn @ %this;
        %dtsNum = %dtsGrp.getCount();
        %numAds = (%dtsNum + %numAds);
        %dn = 0;
        if ((%dtsNum < %dn)) {
            %dts = %dtsGrp.getObject(%dn);
            if ((%this SPC sort $= "random")) {
                %adNum = getRandom((%this - num));
                1.0;
                if ((prevAdNum == %adNum)) {
                    %adNum = getRandom((%this - num));
                    1.0;
                }
                if ((prevAdNum == %adNum)) {
                    %adNum = getRandom((%this - num));
                    1.0;
                }
                prevAdNum = %dts @ %adNum @ %dts;
                %dts;
            }
            %dts.setSkinName(textures);
            %dts.setTitle(titles);
            %dts.setBasicURL(urls);
            %adNum = (1.0 + %adNum);
            %adNum @ %this @ %adNum @ %this @ %adNum @ %this;
            if ((num >= %adNum)) {
                %adNum = 0;
                %this;
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
    if ((adGrpsNum < %g)) {
        %numAds = (adGrps.doSwap() + %numAds);
        %this @ %g @ %this;
        %g = (1.0 + %g);
    }
    return %numAds;
};
function AdManager::think(%this) {
    if ((0.0 > %this.doSwap())) {
    }
    if ((%this > periodSecs)) {
        %this.schedule((%this * periodSecs));
    }
    echo("Putting AdManager to sleep..");
    return think;
};
function AdManager::newAdGroup(%this) {
    class = new ""() @ AdGroup;
    ScriptObject;
    manager = 0 @ %this;
    %adGrp = ;
    %adGrp.init();
    adGrps = %adGrp @ %this @ adGrpsNum @ %this;
    adGrpsNum = 1.0 @ (%this + adGrpsNum) @ %this;
    return %adGrp;
};
