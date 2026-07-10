function NPCManager::init(%this) {
    if (isObject(%this.animSets)) {
        %this.animSets.delete();
    }
    %this.animSets = 0 @ new StringMap("");;
    %this.animSets.add(MissionCleanup);
    %as = %this.animSets;
    %this.thinkPeriod = 1311;
    %this.NPCGroup = NPCGroup;
    %this.resetnextAnimTimes();
    %this.assertOutfits();
    %this.numResponseAnims = 0;
    10000.setAnimTimeMin(%this, "bouncer1");
    20000.setAnimTimeMax(%this, "bouncer1");
    "idle2".addAnimToSet(%this, "bouncer1");
    "idle1".addAnimToSet(%this, "bouncer1");
    "idle1".addAnimToSet(%this, "bouncer1");
    10000.setAnimTimeMin(%this, "pcdkim");
    15000.setAnimTimeMax(%this, "pcdkim");
    "talk2".addAnimToSet(%this, "pcdkim");
    "talk3".addAnimToSet(%this, "pcdkim");
    "listen2".addAnimToSet(%this, "pcdkim");
    10000.setAnimTimeMin(%this, "pcdashley");
    15000.setAnimTimeMax(%this, "pcdashley");
    "listen".addAnimToSet(%this, "pcdashley");
    10000.setAnimTimeMin(%this, "pcdcarmit");
    15000.setAnimTimeMax(%this, "pcdcarmit");
    "talk".addAnimToSet(%this, "pcdcarmit");
    10000.setAnimTimeMin(%this, "pcdmelody");
    15000.setAnimTimeMax(%this, "pcdmelody");
    "idle".addAnimToSet(%this, "pcdmelody");
    "idle".addAnimToSet(%this, "pcdmelody");
    "talk".addAnimToSet(%this, "pcdmelody");
    10000.setAnimTimeMin(%this, "pcdjessica");
    15000.setAnimTimeMax(%this, "pcdjessica");
    "lidle1".addAnimToSet(%this, "pcdjessica");
    "lidle2".addAnimToSet(%this, "pcdjessica");
    "lidle3".addAnimToSet(%this, "pcdjessica");
    "lidle4".addAnimToSet(%this, "pcdjessica");
    "lidle5".addAnimToSet(%this, "pcdjessica");
    "lidle6".addAnimToSet(%this, "pcdjessica");
    8000.setAnimTimeMin(%this, "pcdnicole");
    10000.setAnimTimeMax(%this, "pcdnicole");
    "idle1".addAnimToSet(%this, "pcdnicole");
    "idle2".addAnimToSet(%this, "pcdnicole");
    "idle3".addAnimToSet(%this, "pcdnicole");
    "idle4".addAnimToSet(%this, "pcdnicole");
    "idle1".addAnimToSet(%this, "pcdnicole");
    "idle1".addAnimToSet(%this, "pcdnicole");
    "wave".addAnimToSet(%this, "pcdnicole");
    3000.setAnimTimeMin(%this, "dj1");
    5000.setAnimTimeMax(%this, "dj1");
    "idle".addAnimToSet(%this, "dj1");
    "idle".addAnimToSet(%this, "dj1");
    "idle".addAnimToSet(%this, "dj1");
    "fiddle".addAnimToSet(%this, "dj1");
    3000.setAnimTimeMin(%this, "dj2");
    5000.setAnimTimeMax(%this, "dj2");
    "idle".addAnimToSet(%this, "dj2");
    "idle".addAnimToSet(%this, "dj2");
    "idle".addAnimToSet(%this, "dj2");
    "fiddle".addAnimToSet(%this, "dj2");
    10000.setAnimTimeMin(%this, "bartenderF");
    15000.setAnimTimeMax(%this, "bartenderF");
    "fnbtfbb".addAnimToSet(%this, "bartenderF");
    "fnbtidl1".addAnimToSet(%this, "bartenderF");
    "fnbtidl1".addAnimToSet(%this, "bartenderF");
    "fnbtlidl1".addAnimToSet(%this, "bartenderF");
    "fnbtlidl1".addAnimToSet(%this, "bartenderF");
    "fnbtlidl2".addAnimToSet(%this, "bartenderF");
    "fnbtidl1".addAnimToSet(%this, "bartenderF");
    "fnbtidl1".addAnimToSet(%this, "bartenderF");
    "fnbtlidl1".addAnimToSet(%this, "bartenderF");
    "fnbtlidl1".addAnimToSet(%this, "bartenderF");
    "fnbtlidl2".addAnimToSet(%this, "bartenderF");
    10000.setAnimTimeMin(%this, "bartenderM");
    15000.setAnimTimeMax(%this, "bartenderM");
    "mnbtfbb".addAnimToSet(%this, "bartenderM");
    "mnbtidl1".addAnimToSet(%this, "bartenderM");
    "mnbtidl1".addAnimToSet(%this, "bartenderM");
    "mnbtlidl1".addAnimToSet(%this, "bartenderM");
    "mnbtlidl2".addAnimToSet(%this, "bartenderM");
    "mnbtlidl2".addAnimToSet(%this, "bartenderM");
    "mnbtidl1".addAnimToSet(%this, "bartenderM");
    "mnbtidl1".addAnimToSet(%this, "bartenderM");
    "mnbtlidl1".addAnimToSet(%this, "bartenderM");
    "mnbtlidl2".addAnimToSet(%this, "bartenderM");
    "mnbtlidl2".addAnimToSet(%this, "bartenderM");
    10000.setAnimTimeMin(%this, "storekeepM");
    15000.setAnimTimeMax(%this, "storekeepM");
    "mnbtidl1".addAnimToSet(%this, "storekeepM");
    "mnbtlidl1".addAnimToSet(%this, "storekeepM");
    "mnbtlidl2".addAnimToSet(%this, "storekeepM");
    10000.setAnimTimeMin(%this, "djgroupie1");
    15000.setAnimTimeMax(%this, "djgroupie1");
    "fhlidl1a".addAnimToSet(%this, "djgroupie1");
    "fhlidl2a".addAnimToSet(%this, "djgroupie1");
    "filidl1a".addAnimToSet(%this, "djgroupie1");
    "filidl2a".addAnimToSet(%this, "djgroupie1");
    "fplidl1a".addAnimToSet(%this, "djgroupie1");
    "fplidl2a".addAnimToSet(%this, "djgroupie1");
    10000.setAnimTimeMin(%this, "djgroupie2");
    15000.setAnimTimeMax(%this, "djgroupie2");
    "fnsitlsn".addAnimToSet(%this, "djgroupie2");
    "fnsittlk".addAnimToSet(%this, "djgroupie2");
    7000.setAnimTimeMin(%this, "djgroupie3");
    9000.setAnimTimeMax(%this, "djgroupie3");
    "midl2a".addAnimToSet(%this, "djgroupie3");
    "midl2b".addAnimToSet(%this, "djgroupie3");
    "midl2c".addAnimToSet(%this, "djgroupie3");
    "midl2d".addAnimToSet(%this, "djgroupie3");
    "mntlk2".addAnimToSet(%this, "djgroupie3");
    "mntlk3".addAnimToSet(%this, "djgroupie3");
    "mntlk".addAnimToSet(%this, "djgroupie3");
    "micool".addAnimToSet(%this, "djgroupie3");
    "miflr".addAnimToSet(%this, "djgroupie3");
    3000.setAnimTimeMin(%this, "andrew");
    5000.setAnimTimeMax(%this, "andrew");
    "idl1".addAnimToSet(%this, "andrew");
    "idl2".addAnimToSet(%this, "andrew");
    "idl3".addAnimToSet(%this, "andrew");
    "idl4".addAnimToSet(%this, "andrew");
    "vomit".addAnimToSet(%this, "andrew");
    "whew".addAnimToSet(%this, "andrew");
    "idle2".addAnimToSet(%this, "ai_bouncer");
    "idle1".addAnimToSet(%this, "ai_bouncer");
    "idle1".addAnimToSet(%this, "ai_bouncer");
    "".addAnimToSet(%this, "ai_pcd");
    "".addAnimToSet(%this, "ai_dj");
    "".addAnimToSet(%this, "ai_generic");
    "".addAnimToSet(%this, "ai_generic");
    "".addAnimToSet(%this, "ai_generic");
    "".addAnimToSet(%this, "ai_generic");
    "".addAnimToSet(%this, "ai_generic");
    "".addAnimToSet(%this, "ai_generic");
    "".addAnimToSet(%this, "ai_generic");
    "".addAnimToSet(%this, "ai_generic");
    "".addAnimToSet(%this, "ai_generic");
    "cnf".addAnimToSet(%this, "ai_generic");
    "cnf".addAnimToSet(%this, "ai_generic");
    "cnf".addAnimToSet(%this, "ai_generic");
    "cnf".addAnimToSet(%this, "ai_generic");
    "cnf".addAnimToSet(%this, "ai_generic");
    "cnf".addAnimToSet(%this, "ai_generic");
    "cnf".addAnimToSet(%this, "ai_generic");
    "cnf".addAnimToSet(%this, "ai_generic");
    "cnf".addAnimToSet(%this, "ai_generic");
    "cnf".addAnimToSet(%this, "ai_generic");
    "lol".addAnimToSet(%this, "ai_generic");
    "lol".addAnimToSet(%this, "ai_generic");
    "lol".addAnimToSet(%this, "ai_generic");
    "lol".addAnimToSet(%this, "ai_generic");
    "lol".addAnimToSet(%this, "ai_generic");
    "lol".addAnimToSet(%this, "ai_generic");
    "lol".addAnimToSet(%this, "ai_generic");
    "lol".addAnimToSet(%this, "ai_generic");
    "lol".addAnimToSet(%this, "ai_generic");
    "lol".addAnimToSet(%this, "ai_generic");
    "think".addAnimToSet(%this, "ai_generic");
    "think".addAnimToSet(%this, "ai_generic");
    "think".addAnimToSet(%this, "ai_generic");
    "think".addAnimToSet(%this, "ai_generic");
    "think".addAnimToSet(%this, "ai_generic");
    "cool".addAnimToSet(%this, "ai_generic");
    "cool".addAnimToSet(%this, "ai_generic");
    "cool".addAnimToSet(%this, "ai_generic");
    "nlst".addAnimToSet(%this, "ai_generic");
    "nlst".addAnimToSet(%this, "ai_generic");
    "nlst".addAnimToSet(%this, "ai_generic");
    "ttth".addAnimToSet(%this, "ai_generic");
    "cool".addAnimToSet(%this, "ai_generic");
    "shhh".addAnimToSet(%this, "ai_generic");
    "flr".addAnimToSet(%this, "ai_generic");
    "lsn".addAnimToSet(%this, "ai_generic");
    "sad".addAnimToSet(%this, "ai_generic");
    "spr".addAnimToSet(%this, "ai_generic");
    "srd".addAnimToSet(%this, "ai_generic");
    "cry".addAnimToSet(%this, "ai_generic");
    "ilve".addAnimToSet(%this, "ai_generic");
    "kiss".addAnimToSet(%this, "ai_generic");
    "emb".addAnimToSet(%this, "ai_generic");
    "vom".addAnimToSet(%this, "ai_generic");
    "slpy".addAnimToSet(%this, "ai_generic");
    "whw".addAnimToSet(%this, "ai_generic");
    "rotfl".addAnimToSet(%this, "ai_generic");
    "apls".addAnimToSet(%this, "ai_generic");
    "doh".addAnimToSet(%this, "ai_generic");
    "busy".addAnimToSet(%this, "ai_generic");
    "nlst".addAnimToSet(%this, "ai_generic");
    return;
};
function storeTransformsSet(%simSet) {
    %num = %simSet.getCount();
    %n = 0;
    while ((%n < %num)) {
        %obj = %n.getObject(%simSet);
        gSetField(%obj, origTransform, %obj.getTransform());
        %n = (%n + 1.0);
    }
};
function restoreTransformsSet(%simSet) {
    %num = %simSet.getCount();
    %n = 0;
    while ((%n < %num)) {
        %obj = %n.getObject(%simSet);
        gGetField(%obj).setTransform(%obj, origTransform);
        %n = (%n + 1.0);
    }
};
function copyObjectNamesToShapeNamesSet(%simSet) {
    %num = %simSet.getCount();
    %n = 0;
    while ((%n < %num)) {
        %obj = %n.getObject(%simSet);
        %obj.getName().setShapeName(%obj);
        %n = (%n + 1.0);
    }
};
function registerInPlayerDictSet(%simSet) {
    %num = %simSet.getCount();
    %n = 0;
    while ((%n < %num)) {
        %obj = %n.getObject(%simSet);
        %obj.put(PlayerDict, %obj.getShapeName());
        %n = (%n + 1.0);
    }
};
function NPCManager::resetnextAnimTimes(%this) {
    if (!(isObject(%this.NPCGroup))) {
        return;
    }
    %NPCNum = %this.NPCGroup.getCount();
    %n = 0;
    while ((%n < %NPCNum)) {
        gSetField(%n.getObject(%this.NPCGroup), nextAnimTime, "");
        %n = (%n + 1.0);
    }
};
function NPCManager::assertOutfits(%this) {
    if (!(isObject(%this.NPCGroup))) {
        return;
    }
    %NPCNum = %this.NPCGroup.getCount();
    %n = 0;
    while ((%n < %NPCNum)) {
        %npc = %n.getObject(%this.NPCGroup);
        %gnd = %npc.getDataBlock().gender;
        %npc.gender = %gnd;
        %gnd @ ".headphones.dj".MeshOff(%npc);
        if (!(%gnd $= "")) {
            %w = Wardrobe::findWardrobe(%gnd);
            if (%w) {
                %os = %npc.getDataBlock().outfit;
                %outfit = newOutfit(%w);
                if ((%os $= "random")) {
                    %outfit.makeRandom();
                }
                %os.deserialize(%outfit);
                %npc.assert(%outfit);
            }
        }
        %n = (%n + 1.0);
    }
};
function NPCManager::setAnimTimeMin(%this, %setName, %val) {
    %val.put(%this.animSets, %setName @ "_TimeMin");
    return;
};
function NPCManager::setAnimTimeMax(%this, %setName, %val) {
    %val.put(%this.animSets, %setName @ "_TimeMax");
    return;
};
function NPCManager::getAnimTimeMin(%this, %setName) {
    return %setName @ "_TimeMin".get(%this.animSets);
};
function NPCManager::getAnimTimeMax(%this, %setName) {
    return %setName @ "_TimeMax".get(%this.animSets);
};
function NPCManager::getRandomAnimFromSet(%this, %setName) {
    %set = %setName.get(%this.animSets);
    %num = getWordCount(%set);
    return getWord(%set, getRandom(0, (%num - 1.0)));
};
function NPCManager::addAnimToSet(%this, %setName, %val) {
    %prev = %setName.get(%this.animSets);
    if (!(%prev $= "")) {
        %newThing = %prev @ " " @ %val;
    }
    %newThing = %val;
    %newThing.put(%this.animSets, %setName);
    %sets = "setNames".get(%this.animSets);
    if ((findWord(%sets, %setName) != -(1.0))) {
        return;
    }
    if (!(%sets $= "")) {
        %newThing = %sets @ " " @ %setName;
    }
    %newThing = %setName;
    %newThing.put(%this.animSets, "setNames");
    return;
};
function NPCManager::think(%this) {
    if (!(isObject(%this.NPCGroup))) {
        warn("No NPC group, going to sleep..");
        return;
    }
    %NPCNum = %this.NPCGroup.getCount();
    %n = 0;
    while ((%n < %NPCNum)) {
        %n.getObject(%this.NPCGroup).thinkNPC(%this);
        %n = (%n + 1.0);
    }
    %this.thinkPeriod.schedule(%this);
    return think;
};
function NPCManager::thinkNPC(%this, %npc) {
    %setName = %npc.getDataBlock().animSetName;
    if (!(isObject(%this))) {
    }
    if (!(isObject(%this.animSets))) {
        echo("Trouble in little china");
        return;
    }
    %curTime = getSimTime();
    %nat = gGetField(%npc);
    nextAnimTime;
    if (!(%nat $= "")) {
    }
    if ((%curTime < %nat)) {
        return;
    }
    %nat = (%curTime + getRandom(%setName.getAnimTimeMin(%this), %setName.getAnimTimeMax(%this)));
    gSetField(%npc, nextAnimTime, %nat);
    %animSet = %setName.get(%this.animSets);
    %anim = %setName.getRandomAnimFromSet(%this);
    %anim.playAnim(%npc);
    return;
};
function NPCManager::dumpEts(%this) {
    %sets = "setNames".get(%this.animSets);
    %setsNum = getWordCount(%sets);
    %setN = 0;
    while ((%setN < %setsNum)) {
        %set = getWord(%sets, %setN);
        echo(%set @ " " @ "timeMin =" @ " " @ %set.getAnimTimeMin(%this) @ " " @ "timeMax =" @ " " @ %set.getAnimTimeMax(%this));
        %anims = %set.get(%this.animSets);
        %animsNum = getWordCount(%anims);
        %animN = 0;
        while ((%animN < %animsNum)) {
            %anim = getWord(%anims, %animN);
            echo("  " @ %anim);
            %animN = (%animN + 1.0);
        }
        %setN = (%setN + 1.0);
        (%animN < %animsNum);
    }
};
function NPCManager::handleTalkedToNPC(%this, %unused, %npc, %unused) {
    %setName = %npc.getDataBlock().aiProfile;
    %anim = %setName.getRandomAnimFromSet(%this);
    if ((%anim $= "")) {
        return;
    }
    %delay = getRandom(500, 1500);
    %anim.schedule(%npc, %delay, "playAnim");
    return;
};
