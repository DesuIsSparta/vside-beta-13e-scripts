$adLogFileName = "";
$adLogCreationTimestamp = "";
function getAdLogName(%timeStamp)
{
    %ServerName = stripVeryAgressively($Pref::Server::Name);
    if (%ServerName $= "")
    {
        %ServerName = "unknownServer";
    }
    %s = $userMods @ "/server/" @ $Pref::Server::adLogFolder @ "/" @ %ServerName @ "/" @ $Pref::Server::adLogBaseName;
    if ($Pref::Server::adLogAppendTimeStamp)
    {
        %s = %s @ "_" @ %timeStamp;
    }
    %s = %s @ ".log";
    return %s;
}
function checkNewAdLogFile()
{
    if ($adLogCreationTimestamp $= "")
    {
        return ;
    }
    %minsLog = getSubStr($adLogCreationTimestamp, 0, 8);
    %minsNow = getSubStr(getTimeStamp(), 0, 8);
    if (strcmp(%minsNow, %minsLog) > 0)
    {
        $adLogFileName = "";
        $adLogCreationTimestamp = "";
    }
    return ;
}
function initAdLogFile()
{
    checkNewAdLogFile();
    if (!($adLogFileName $= ""))
    {
        return $adLogFileName;
    }
    %ts = getTimeStamp();
    %ts = getSubStr(%ts, 0, 17);
    $adLogFileName = getAdLogName(%ts);
    $adLogCreationTimestamp = %ts;
    %file = new FileObject();
    if (%file.openForAppend($adLogFileName))
    {
        %file.writeLine("# Evil Twin Ads Log File");
        %file.writeLine("#" SPC $adLogFileName);
        %file.writeLine("#" SPC %ts);
        %file.writeLine("# Action: <tab> timestamp <tab> playername <tab> imagename <tab> (image-click-coords) <tab> (advert transform) <tab> (advert scale)");
        %file.writeLine("");
        %file.close();
        echo("AdLog started:" SPC $adLogFileName);
    }
    else
    {
        echo("Error opening logfile:" SPC $adLogFileName);
    }
    %file.delete();
    return $adLogFileName;
}
function appendAdLogLine(%line)
{
    %fn = initAdLogFile();
    %file = new FileObject();
    if (%file.openForAppend(%fn))
    {
        %file.writeLine(%line);
        %file.close();
    }
    else
    {
        echo("Error opening logfile:" SPC %fn);
        echo(%line);
    }
    %file.delete();
    return ;
}
function serverCmdAdvertClick(%client, %ghostIndexClnt, %pt)
{
    %playerName = detag(%client.Player.getShapeName());
    %obj = %client.resolveObjectFromGhostIndex(%ghostIndexClnt);
    %tex = %obj.getSkinName();
    %line = "AdClick: ";
    %line = %line TAB getTimeStamp();
    %line = %line TAB stripUnprintables(%playerName);
    %line = %line TAB %obj.getSkinName();
    %line = %line TAB %pt;
    %line = %line TAB "(" @ %obj.getTransform() @ ")" TAB "(" @ %obj.getScale() @ ")";
    appendAdLogLine(%line);
    return ;
}
function serverCmdAdvertFollow(%client, %url)
{
    %playerName = detag(%client.Player.getShapeName());
    %line = "AdFollow:";
    %line = %line TAB getTimeStamp();
    %line = %line TAB stripUnprintables(%playerName);
    %line = %line TAB %url;
    appendAdLogLine(%line);
    return ;
}
function AdGroup::init(%this)
{
    %this.num = 0;
    %this.dtsNum = 0;
    %this.sort = forward;
    %this.offset = 0;
    return ;
}
function AdGroup::addAd(%this, %texture, %title, %url)
{
    %this.textures[%this.num] = %texture;
    %this.titles[%this.num] = %title;
    %this.urls[%this.num] = %url;
    %this.num = %this.num + 1;
    return ;
}
function AdGroup::addDTSGroup(%this, %grp)
{
    %this.dtss[%this.dtsNum] = %grp;
    %this.dtsNum = %this.dtsNum + 1;
    return ;
}
function AdGroup::doSwap(%this)
{
    %this.offset = %this.offset + 1;
    if (%this.offset >= %this.num)
    {
        %this.offset = 0;
    }
    %adNum = %this.offset;
    %numAds = 0;
    %gn = 0;
    while (%gn < %this.dtsNum)
    {
        %dtsGrp = %this.dtss[%gn];
        %dtsNum = %dtsGrp.getCount();
        %numAds = %numAds + %dtsNum;
        %dn = 0;
        while (%dn < %dtsNum)
        {
            %dts = %dtsGrp.getObject(%dn);
            if (%this.sort $= "random")
            {
                %adNum = getRandom(%this.num - 1);
                if (%adNum == %dts.prevAdNum)
                {
                    %adNum = getRandom(%this.num - 1);
                }
                if (%adNum == %dts.prevAdNum)
                {
                    %adNum = getRandom(%this.num - 1);
                }
                %dts.prevAdNum = %adNum;
            }
            %dts.setSkinName(%this.textures[%adNum]);
            %dts.setTitle(%this.titles[%adNum]);
            %dts.setBasicURL(%this.urls[%adNum]);
            %adNum = %adNum + 1;
            if (%adNum >= %this.num)
            {
                %adNum = 0;
            }
            %dn = %dn + 1;
        }
        %gn = %gn + 1;
    }
    return %numAds;
}
function AdManager::doSwap(%this)
{
    %numAds = 0;
    %g = 0;
    while (%g < %this.adGrpsNum)
    {
        %numAds = %numAds + %this.adGrps[%g].doSwap();
        %g = %g + 1;
    }
    return %numAds;
}
function AdManager::think(%this)
{
    if ((%this.doSwap() > 0) && (%this.periodSecs > 0))
    {
        %this.schedule(%this.periodSecs * 1000, think);
    }
    else
    {
        echo("Putting AdManager to sleep..");
    }
    return ;
}
function AdManager::newAdGroup(%this)
{
    %adGrp = new ScriptObject()
    {
        class = AdGroup;
        manager = %this;
    };
    %adGrp.init();
    %this.adGrps[%this.adGrpsNum] = %adGrp;
    %this.adGrpsNum = %this.adGrpsNum + 1;
    return %adGrp;
}
