function NPCManager::init(%this)
{
    if (isObject(%this.animSets))
    {
        %this.animSets.delete();
    }
    %this.animSets = new StringMap();
    MissionCleanup.add(%this.animSets);
    %as = %this.animSets;
    %this.thinkPeriod = 1311;
    %this.NPCGroup = NPCGroup;
    %this.resetnextAnimTimes();
    %this.assertOutfits();
    %this.numResponseAnims = 0;
    %this.setAnimTimeMin("bouncer1", 10000);
    %this.setAnimTimeMax("bouncer1", 20000);
    %this.addAnimToSet("bouncer1", "idle2");
    %this.addAnimToSet("bouncer1", "idle1");
    %this.addAnimToSet("bouncer1", "idle1");
    %this.setAnimTimeMin("pcdkim", 10000);
    %this.setAnimTimeMax("pcdkim", 15000);
    %this.addAnimToSet("pcdkim", "talk2");
    %this.addAnimToSet("pcdkim", "talk3");
    %this.addAnimToSet("pcdkim", "listen2");
    %this.setAnimTimeMin("pcdashley", 10000);
    %this.setAnimTimeMax("pcdashley", 15000);
    %this.addAnimToSet("pcdashley", "listen");
    %this.setAnimTimeMin("pcdcarmit", 10000);
    %this.setAnimTimeMax("pcdcarmit", 15000);
    %this.addAnimToSet("pcdcarmit", "talk");
    %this.setAnimTimeMin("pcdmelody", 10000);
    %this.setAnimTimeMax("pcdmelody", 15000);
    %this.addAnimToSet("pcdmelody", "idle");
    %this.addAnimToSet("pcdmelody", "idle");
    %this.addAnimToSet("pcdmelody", "talk");
    %this.setAnimTimeMin("pcdjessica", 10000);
    %this.setAnimTimeMax("pcdjessica", 15000);
    %this.addAnimToSet("pcdjessica", "lidle1");
    %this.addAnimToSet("pcdjessica", "lidle2");
    %this.addAnimToSet("pcdjessica", "lidle3");
    %this.addAnimToSet("pcdjessica", "lidle4");
    %this.addAnimToSet("pcdjessica", "lidle5");
    %this.addAnimToSet("pcdjessica", "lidle6");
    %this.setAnimTimeMin("pcdnicole", 8000);
    %this.setAnimTimeMax("pcdnicole", 10000);
    %this.addAnimToSet("pcdnicole", "idle1");
    %this.addAnimToSet("pcdnicole", "idle2");
    %this.addAnimToSet("pcdnicole", "idle3");
    %this.addAnimToSet("pcdnicole", "idle4");
    %this.addAnimToSet("pcdnicole", "idle1");
    %this.addAnimToSet("pcdnicole", "idle1");
    %this.addAnimToSet("pcdnicole", "wave");
    %this.setAnimTimeMin("dj1", 3000);
    %this.setAnimTimeMax("dj1", 5000);
    %this.addAnimToSet("dj1", "idle");
    %this.addAnimToSet("dj1", "idle");
    %this.addAnimToSet("dj1", "idle");
    %this.addAnimToSet("dj1", "fiddle");
    %this.setAnimTimeMin("dj2", 3000);
    %this.setAnimTimeMax("dj2", 5000);
    %this.addAnimToSet("dj2", "idle");
    %this.addAnimToSet("dj2", "idle");
    %this.addAnimToSet("dj2", "idle");
    %this.addAnimToSet("dj2", "fiddle");
    %this.setAnimTimeMin("bartenderF", 10000);
    %this.setAnimTimeMax("bartenderF", 15000);
    %this.addAnimToSet("bartenderF", "fnbtfbb");
    %this.addAnimToSet("bartenderF", "fnbtidl1");
    %this.addAnimToSet("bartenderF", "fnbtidl1");
    %this.addAnimToSet("bartenderF", "fnbtlidl1");
    %this.addAnimToSet("bartenderF", "fnbtlidl1");
    %this.addAnimToSet("bartenderF", "fnbtlidl2");
    %this.addAnimToSet("bartenderF", "fnbtidl1");
    %this.addAnimToSet("bartenderF", "fnbtidl1");
    %this.addAnimToSet("bartenderF", "fnbtlidl1");
    %this.addAnimToSet("bartenderF", "fnbtlidl1");
    %this.addAnimToSet("bartenderF", "fnbtlidl2");
    %this.setAnimTimeMin("bartenderM", 10000);
    %this.setAnimTimeMax("bartenderM", 15000);
    %this.addAnimToSet("bartenderM", "mnbtfbb");
    %this.addAnimToSet("bartenderM", "mnbtidl1");
    %this.addAnimToSet("bartenderM", "mnbtidl1");
    %this.addAnimToSet("bartenderM", "mnbtlidl1");
    %this.addAnimToSet("bartenderM", "mnbtlidl2");
    %this.addAnimToSet("bartenderM", "mnbtlidl2");
    %this.addAnimToSet("bartenderM", "mnbtidl1");
    %this.addAnimToSet("bartenderM", "mnbtidl1");
    %this.addAnimToSet("bartenderM", "mnbtlidl1");
    %this.addAnimToSet("bartenderM", "mnbtlidl2");
    %this.addAnimToSet("bartenderM", "mnbtlidl2");
    %this.setAnimTimeMin("storekeepM", 10000);
    %this.setAnimTimeMax("storekeepM", 15000);
    %this.addAnimToSet("storekeepM", "mnbtidl1");
    %this.addAnimToSet("storekeepM", "mnbtlidl1");
    %this.addAnimToSet("storekeepM", "mnbtlidl2");
    %this.setAnimTimeMin("djgroupie1", 10000);
    %this.setAnimTimeMax("djgroupie1", 15000);
    %this.addAnimToSet("djgroupie1", "fhlidl1a");
    %this.addAnimToSet("djgroupie1", "fhlidl2a");
    %this.addAnimToSet("djgroupie1", "filidl1a");
    %this.addAnimToSet("djgroupie1", "filidl2a");
    %this.addAnimToSet("djgroupie1", "fplidl1a");
    %this.addAnimToSet("djgroupie1", "fplidl2a");
    %this.setAnimTimeMin("djgroupie2", 10000);
    %this.setAnimTimeMax("djgroupie2", 15000);
    %this.addAnimToSet("djgroupie2", "fnsitlsn");
    %this.addAnimToSet("djgroupie2", "fnsittlk");
    %this.setAnimTimeMin("djgroupie3", 7000);
    %this.setAnimTimeMax("djgroupie3", 9000);
    %this.addAnimToSet("djgroupie3", "midl2a");
    %this.addAnimToSet("djgroupie3", "midl2b");
    %this.addAnimToSet("djgroupie3", "midl2c");
    %this.addAnimToSet("djgroupie3", "midl2d");
    %this.addAnimToSet("djgroupie3", "mntlk2");
    %this.addAnimToSet("djgroupie3", "mntlk3");
    %this.addAnimToSet("djgroupie3", "mntlk");
    %this.addAnimToSet("djgroupie3", "micool");
    %this.addAnimToSet("djgroupie3", "miflr");
    %this.setAnimTimeMin("andrew", 3000);
    %this.setAnimTimeMax("andrew", 5000);
    %this.addAnimToSet("andrew", "idl1");
    %this.addAnimToSet("andrew", "idl2");
    %this.addAnimToSet("andrew", "idl3");
    %this.addAnimToSet("andrew", "idl4");
    %this.addAnimToSet("andrew", "vomit");
    %this.addAnimToSet("andrew", "whew");
    %this.addAnimToSet("ai_bouncer", "idle2");
    %this.addAnimToSet("ai_bouncer", "idle1");
    %this.addAnimToSet("ai_bouncer", "idle1");
    %this.addAnimToSet("ai_pcd", "");
    %this.addAnimToSet("ai_dj", "");
    %this.addAnimToSet("ai_generic", "");
    %this.addAnimToSet("ai_generic", "");
    %this.addAnimToSet("ai_generic", "");
    %this.addAnimToSet("ai_generic", "");
    %this.addAnimToSet("ai_generic", "");
    %this.addAnimToSet("ai_generic", "");
    %this.addAnimToSet("ai_generic", "");
    %this.addAnimToSet("ai_generic", "");
    %this.addAnimToSet("ai_generic", "");
    %this.addAnimToSet("ai_generic", "cnf");
    %this.addAnimToSet("ai_generic", "cnf");
    %this.addAnimToSet("ai_generic", "cnf");
    %this.addAnimToSet("ai_generic", "cnf");
    %this.addAnimToSet("ai_generic", "cnf");
    %this.addAnimToSet("ai_generic", "cnf");
    %this.addAnimToSet("ai_generic", "cnf");
    %this.addAnimToSet("ai_generic", "cnf");
    %this.addAnimToSet("ai_generic", "cnf");
    %this.addAnimToSet("ai_generic", "cnf");
    %this.addAnimToSet("ai_generic", "lol");
    %this.addAnimToSet("ai_generic", "lol");
    %this.addAnimToSet("ai_generic", "lol");
    %this.addAnimToSet("ai_generic", "lol");
    %this.addAnimToSet("ai_generic", "lol");
    %this.addAnimToSet("ai_generic", "lol");
    %this.addAnimToSet("ai_generic", "lol");
    %this.addAnimToSet("ai_generic", "lol");
    %this.addAnimToSet("ai_generic", "lol");
    %this.addAnimToSet("ai_generic", "lol");
    %this.addAnimToSet("ai_generic", "think");
    %this.addAnimToSet("ai_generic", "think");
    %this.addAnimToSet("ai_generic", "think");
    %this.addAnimToSet("ai_generic", "think");
    %this.addAnimToSet("ai_generic", "think");
    %this.addAnimToSet("ai_generic", "cool");
    %this.addAnimToSet("ai_generic", "cool");
    %this.addAnimToSet("ai_generic", "cool");
    %this.addAnimToSet("ai_generic", "nlst");
    %this.addAnimToSet("ai_generic", "nlst");
    %this.addAnimToSet("ai_generic", "nlst");
    %this.addAnimToSet("ai_generic", "ttth");
    %this.addAnimToSet("ai_generic", "cool");
    %this.addAnimToSet("ai_generic", "shhh");
    %this.addAnimToSet("ai_generic", "flr");
    %this.addAnimToSet("ai_generic", "lsn");
    %this.addAnimToSet("ai_generic", "sad");
    %this.addAnimToSet("ai_generic", "spr");
    %this.addAnimToSet("ai_generic", "srd");
    %this.addAnimToSet("ai_generic", "cry");
    %this.addAnimToSet("ai_generic", "ilve");
    %this.addAnimToSet("ai_generic", "kiss");
    %this.addAnimToSet("ai_generic", "emb");
    %this.addAnimToSet("ai_generic", "vom");
    %this.addAnimToSet("ai_generic", "slpy");
    %this.addAnimToSet("ai_generic", "whw");
    %this.addAnimToSet("ai_generic", "rotfl");
    %this.addAnimToSet("ai_generic", "apls");
    %this.addAnimToSet("ai_generic", "doh");
    %this.addAnimToSet("ai_generic", "busy");
    %this.addAnimToSet("ai_generic", "nlst");
    return ;
}
function storeTransformsSet(%simSet)
{
    %num = %simSet.getCount();
    %n = 0;
    while (%n < %num)
    {
        %obj = %simSet.getObject(%n);
        gSetField(%obj, origTransform, %obj.getTransform());
        %n = %n + 1;
    }
}

function restoreTransformsSet(%simSet)
{
    %num = %simSet.getCount();
    %n = 0;
    while (%n < %num)
    {
        %obj = %simSet.getObject(%n);
        %obj.setTransform(gGetField(%obj, origTransform));
        %n = %n + 1;
    }
}

function copyObjectNamesToShapeNamesSet(%simSet)
{
    %num = %simSet.getCount();
    %n = 0;
    while (%n < %num)
    {
        %obj = %simSet.getObject(%n);
        %obj.setShapeName(%obj.getName());
        %n = %n + 1;
    }
}

function registerInPlayerDictSet(%simSet)
{
    %num = %simSet.getCount();
    %n = 0;
    while (%n < %num)
    {
        %obj = %simSet.getObject(%n);
        PlayerDict.put(%obj.getShapeName(), %obj);
        %n = %n + 1;
    }
}

function NPCManager::resetnextAnimTimes(%this)
{
    if (!isObject(%this.NPCGroup))
    {
        return ;
    }
    %NPCNum = %this.NPCGroup.getCount();
    %n = 0;
    while (%n < %NPCNum)
    {
        gSetField(%this.NPCGroup.getObject(%n), nextAnimTime, "");
        %n = %n + 1;
    }
}

function NPCManager::assertOutfits(%this)
{
    if (!isObject(%this.NPCGroup))
    {
        return ;
    }
    %NPCNum = %this.NPCGroup.getCount();
    %n = 0;
    while (%n < %NPCNum)
    {
        %npc = %this.NPCGroup.getObject(%n);
        %gnd = %npc.getDataBlock().gender;
        %npc.gender = %gnd;
        %npc.MeshOff(%gnd @ ".headphones.dj");
        if (!(%gnd $= ""))
        {
            %w = Wardrobe::findWardrobe(%gnd);
            if (%w)
            {
                %os = %npc.getDataBlock().outfit;
                %outfit = newOutfit(%w);
                if (%os $= "random")
                {
                    %outfit.makeRandom();
                }
                else
                {
                    %outfit.deserialize(%os);
                }
                %outfit.assert(%npc);
            }
        }
        %n = %n + 1;
    }
}

function NPCManager::setAnimTimeMin(%this, %setName, %val)
{
    %this.animSets.put(%setName @ "_TimeMin", %val);
    return ;
}
function NPCManager::setAnimTimeMax(%this, %setName, %val)
{
    %this.animSets.put(%setName @ "_TimeMax", %val);
    return ;
}
function NPCManager::getAnimTimeMin(%this, %setName)
{
    return %this.animSets.get(%setName @ "_TimeMin");
}
function NPCManager::getAnimTimeMax(%this, %setName)
{
    return %this.animSets.get(%setName @ "_TimeMax");
}
function NPCManager::getRandomAnimFromSet(%this, %setName)
{
    %set = %this.animSets.get(%setName);
    %num = getWordCount(%set);
    return getWord(%set, getRandom(0, %num - 1));
}
function NPCManager::addAnimToSet(%this, %setName, %val)
{
    %prev = %this.animSets.get(%setName);
    if (!(%prev $= ""))
    {
        %newThing = %prev SPC %val;
    }
    else
    {
        %newThing = %val;
    }
    %this.animSets.put(%setName, %newThing);
    %sets = %this.animSets.get("setNames");
    if (findWord(%sets, %setName) != -1)
    {
        return ;
    }
    if (!(%sets $= ""))
    {
        %newThing = %sets SPC %setName;
    }
    else
    {
        %newThing = %setName;
    }
    %this.animSets.put("setNames", %newThing);
    return ;
}
function NPCManager::think(%this)
{
    if (!isObject(%this.NPCGroup))
    {
        warn("No NPC group, going to sleep..");
        return ;
    }
    %NPCNum = %this.NPCGroup.getCount();
    %n = 0;
    while (%n < %NPCNum)
    {
        %this.thinkNPC(%this.NPCGroup.getObject(%n));
        %n = %n + 1;
    }
    %this.schedule(%this.thinkPeriod, think);
    return ;
}
function NPCManager::thinkNPC(%this, %npc)
{
    %setName = %npc.getDataBlock().animSetName;
    if (!isObject(%this) && !isObject(%this.animSets))
    {
        echo("Trouble in little china");
        return ;
    }
    %curTime = getSimTime();
    %nat = gGetField(%npc, nextAnimTime);
    if (!((%nat $= "")) && (%curTime < %nat))
    {
        return ;
    }
    %nat = %curTime + getRandom(%this.getAnimTimeMin(%setName), %this.getAnimTimeMax(%setName));
    gSetField(%npc, nextAnimTime, %nat);
    %animSet = %this.animSets.get(%setName);
    %anim = %this.getRandomAnimFromSet(%setName);
    %npc.playAnim(%anim);
    return ;
}
function NPCManager::dumpEts(%this)
{
    %sets = %this.animSets.get("setNames");
    %setsNum = getWordCount(%sets);
    %setN = 0;
    while (%setN < %setsNum)
    {
        %set = getWord(%sets, %setN);
        echo(%set SPC "timeMin =" SPC %this.getAnimTimeMin(%set) SPC "timeMax =" SPC %this.getAnimTimeMax(%set));
        %anims = %this.animSets.get(%set);
        %animsNum = getWordCount(%anims);
        %animN = 0;
        while (%animN < %animsNum)
        {
            %anim = getWord(%anims, %animN);
            echo("  " @ %anim);
            %animN = %animN + 1;
        }
        %setN = %setN + 1;
    }
}

function NPCManager::handleTalkedToNPC(%this, %unused, %npc, %unused)
{
    %setName = %npc.getDataBlock().aiProfile;
    %anim = %this.getRandomAnimFromSet(%setName);
    if (%anim $= "")
    {
        return ;
    }
    %delay = getRandom(500, 1500);
    %npc.schedule(%delay, "playAnim", %anim);
    return ;
}
