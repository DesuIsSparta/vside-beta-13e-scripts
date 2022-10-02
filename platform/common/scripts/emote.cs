function addPlainToCoded(%list, %plain, %coded)
{
    %num = getFieldCount(%list);
    %list = setField(%list, %num + 0, %plain);
    %list = setField(%list, %num + 1, %coded);
    return %list;
}
function initGenderedDances()
{
    %list = "";
    %list = addPlainToCoded(%list, "Mooncurl", "hdnc1");
    %list = addPlainToCoded(%list, "Diva Dip", "hdnc2");
    %list = addPlainToCoded(%list, "Chippin", "hdnc3");
    %list = addPlainToCoded(%list, "LingyLicious", "hdnc4");
    %list = addPlainToCoded(%list, "Nara", "idnc1");
    %list = addPlainToCoded(%list, "NRG", "idnc2");
    %list = addPlainToCoded(%list, "TaraBoom", "idnc3");
    %list = addPlainToCoded(%list, "TwirlGurl", "idnc4");
    %list = addPlainToCoded(%list, "The Settle", "pdnc1");
    %list = addPlainToCoded(%list, "The Coed", "pdnc2");
    %list = addPlainToCoded(%list, "The Betty", "pdnc3");
    %list = addPlainToCoded(%list, "The Preppy", "pdnc4");
    $dancesMap_Lounge_F = %list;
    %list = "";
    %list = addPlainToCoded(%list, "HiKhoo", "hdnc1");
    %list = addPlainToCoded(%list, "2Can", "hdnc2");
    %list = addPlainToCoded(%list, "LoKhoo", "hdnc3");
    %list = addPlainToCoded(%list, "The Bot", "hdnc4");
    %list = addPlainToCoded(%list, "Heyman", "idnc1");
    %list = addPlainToCoded(%list, "The Tantrum", "idnc2");
    %list = addPlainToCoded(%list, "WooWho", "idnc3");
    %list = addPlainToCoded(%list, "Littlefield", "idnc4");
    %list = addPlainToCoded(%list, "TamJam", "pdnc1");
    %list = addPlainToCoded(%list, "The 101", "pdnc2");
    %list = addPlainToCoded(%list, "The Turtle", "pdnc3");
    %list = addPlainToCoded(%list, "Handzup", "pdnc4");
    $dancesMap_Lounge_M = %list;
    %list = "";
    %list = addPlainToCoded(%list, "Trixie Rock", "hdncb1");
    %list = addPlainToCoded(%list, "CC-Six-Step", "hdncb2");
    %list = addPlainToCoded(%list, "The JFecks", "hdncb3");
    %list = addPlainToCoded(%list, "Hollowback", "hdncb4");
    $dancesMap_Break_F = %list;
    %list = "";
    %list = addPlainToCoded(%list, "TopRock", "hdncb1");
    %list = addPlainToCoded(%list, "Six-Step", "hdncb2");
    %list = addPlainToCoded(%list, "Windmill", "hdncb3");
    %list = addPlainToCoded(%list, "Baby Freeze", "hdncb4");
    $dancesMap_Break_M = %list;
    %list = "";
    %list = addPlainToCoded(%list, "Snake Charm", "dgoth01");
    %list = addPlainToCoded(%list, "Murphy Sway", "dgoth02");
    %list = addPlainToCoded(%list, "CaressOfTheSullen", "dgoth03");
    %list = addPlainToCoded(%list, "Egyptian", "dgoth04");
    %list = addPlainToCoded(%list, "DarkAngel", "dgoth05");
    $dancesMap_Goth_F = %list;
    %list = "";
    %list = addPlainToCoded(%list, "Dreamweaver", "dgoth01");
    %list = addPlainToCoded(%list, "Murphy Sway", "dgoth02");
    %list = addPlainToCoded(%list, "Dodger Stomp", "dgoth03");
    %list = addPlainToCoded(%list, "BlackTape", "dgoth04");
    %list = addPlainToCoded(%list, "SpellMaker", "dgoth05");
    $dancesMap_Goth_M = %list;
    %list = "";
    %list = addPlainToCoded(%list, "ZombieThrust R", "mjbtrst1");
    %list = addPlainToCoded(%list, "ZombieThrust L", "mjbtrst2");
    %list = addPlainToCoded(%list, "Signature", "mjptrst");
    %list = addPlainToCoded(%list, "HeadTilt", "mjhtilt");
    %list = addPlainToCoded(%list, "HeadTurn", "mjhturn");
    %list = addPlainToCoded(%list, "ZombieClaw R", "mjclaw1");
    %list = addPlainToCoded(%list, "ZombieClaw L", "mjclaw2");
    %list = addPlainToCoded(%list, "ThunderClap R", "mjtclap2");
    %list = addPlainToCoded(%list, "ThunderClap L", "mjtclap1");
    %list = addPlainToCoded(%list, "GallopStep", "mjmstep");
    %list = addPlainToCoded(%list, "SideStep R", "mjsstep2");
    %list = addPlainToCoded(%list, "SideStep L", "mjsstep");
    %list = addPlainToCoded(%list, "ZombieStep", "mjzstep");
    %list = addPlainToCoded(%list, "ZombieShrug", "mjshrug");
    %list = addPlainToCoded(%list, "ZombiePivot", "mjpvt");
    %list = addPlainToCoded(%list, "ZombiePose", "mjzpose");
    %list = addPlainToCoded(%list, "Get-Down", "mjlih");
    %list = addPlainToCoded(%list, "MJ Spin", "mjspin");
    $dancesMap_Thrilla = %list;
    %list = "";
    %list = addPlainToCoded(%list, "Im so Sexy", "go01");
    %list = addPlainToCoded(%list, "In Your Pocket", "go02");
    %list = addPlainToCoded(%list, "Rising Sun", "go03");
    %list = addPlainToCoded(%list, "The Pony", "go04");
    %list = addPlainToCoded(%list, "Bat Swing", "go05");
    %list = addPlainToCoded(%list, "Crump-a-GoGo", "go06");
    %list = addPlainToCoded(%list, "Watching You", "go07");
    %list = addPlainToCoded(%list, "The Swim", "go08");
    %list = addPlainToCoded(%list, "De-Licious", "delite1");
    %list = addPlainToCoded(%list, "De-Groovy", "delite2");
    %list = addPlainToCoded(%list, "De-Lovely", "delite3");
    %list = addPlainToCoded(%list, "De-Gorgeous", "delite4");
    %list = addPlainToCoded(%list, "De-Vine", "delite5");
    %list = addPlainToCoded(%list, "The YMCA", "dymca1");
    %list = addPlainToCoded(%list, "ClapNTurn Boy", "dymca2");
    %list = addPlainToCoded(%list, "Macho Man", "dymca3");
    $dancesMap_GoGo_F = %list;
    %list = "";
    %list = addPlainToCoded(%list, "Im so Sexy", "go01");
    %list = addPlainToCoded(%list, "In Your Pocket", "go02");
    %list = addPlainToCoded(%list, "Rising Sun", "go03");
    %list = addPlainToCoded(%list, "The Pony", "go04");
    %list = addPlainToCoded(%list, "Bat Swing", "go05");
    %list = addPlainToCoded(%list, "Crump-a-GoGo", "go06");
    %list = addPlainToCoded(%list, "Watching You", "go07");
    %list = addPlainToCoded(%list, "The Swim", "go08");
    %list = addPlainToCoded(%list, "De-Litable", "delite1");
    %list = addPlainToCoded(%list, "De-Groovy", "delite2");
    %list = addPlainToCoded(%list, "De-Ohlalala", "delite3");
    %list = addPlainToCoded(%list, "De-Gorgeous", "delite4");
    %list = addPlainToCoded(%list, "De-Funkie", "delite5");
    %list = addPlainToCoded(%list, "The YMCA", "dymca1");
    %list = addPlainToCoded(%list, "ClapNTurn Boy", "dymca2");
    %list = addPlainToCoded(%list, "Macho Man", "dymca3");
    $dancesMap_GoGo_M = %list;
    %list = "";
    %list = addPlainToCoded(%list, "Heel-Toe", "dhtoe");
    %list = addPlainToCoded(%list, "Lean wit it", "dlnwit");
    %list = addPlainToCoded(%list, "Da Shuffle", "dshfle");
    %list = addPlainToCoded(%list, "Slip-n-Slide", "dslpsld");
    %list = addPlainToCoded(%list, "Walk it Out", "dwlkit");
    %list = addPlainToCoded(%list, "X-Hop", "dxhop");
    %list = addPlainToCoded(%list, "Two Step", "d2step");
    %list = addPlainToCoded(%list, "Cross Step", "d2stepx");
    %list = addPlainToCoded(%list, "VStep", "dvstepb");
    %list = addPlainToCoded(%list, "HipHop Brush Off", "brshft");
    $dancesMap_HipHop = %list;
    %list = "";
    %list = addPlainToCoded(%list, "Boogie Tap", "jbdnc01");
    %list = addPlainToCoded(%list, "Boogaloo", "jbdnc02");
    %list = addPlainToCoded(%list, "Funkie Chicken", "jbdnc03");
    %list = addPlainToCoded(%list, "Da Ole JB", "jbdnc04");
    %list = addPlainToCoded(%list, "Mash Taters", "jbdnc05");
    %list = addPlainToCoded(%list, "The Aow", "jbdnc06");
    %list = addPlainToCoded(%list, "JB Shuffle", "jbdnc07");
    %list = addPlainToCoded(%list, "Camel Walk", "jbdnc08");
    %list = addPlainToCoded(%list, "Get Funkie", "jbdnc10");
    $dancesMap_JB = %list;
    return ;
}
$gDanceMaps = "";
function getRandomDance()
{
    if ($gDanceMaps $= "")
    {
        $gDanceMaps = $gDanceMaps @ $dancesMap_Lounge_F @ "\t";
        $gDanceMaps = $gDanceMaps @ $dancesMap_Break_F @ "\t";
        $gDanceMaps = $gDanceMaps @ $dancesMap_Goth_F @ "\t";
        $gDanceMaps = $gDanceMaps @ $dancesMap_Thrilla @ "\t";
        $gDanceMaps = $gDanceMaps @ $dancesMap_GoGo @ "\t";
        $gDanceMaps = $gDanceMaps @ $dancesMap_HipHop @ "\t";
        $gDanceMaps = $gDanceMaps @ $dancesMap_JB @ "\t";
    }
    %num = getFieldCount($gDanceMaps) / 2;
    %n = (getRandom(0, %num - 1) * 2) + 1;
    return getField($gDanceMaps, %n);
}
function insertPlainToCodedListIntoMap(%srcList, %trgMap)
{
    %num = getFieldCount(%srcList) / 2;
    %n = 0;
    while (%n < %num)
    {
        %plain = getField(%srcList, %n * 2);
        %coded = getField(%srcList, (%n * 2) + 1);
        %trgMap.put(%plain, %coded);
        %n = %n + 1;
    }
}

function removePlainToCodedListFromMap(%srcList, %trgMap)
{
    %num = getFieldCount(%srcList) / 2;
    %n = 0;
    while (%n < %num)
    {
        %plain = getField(%srcList, %n * 2);
        %coded = getField(%srcList, (%n * 2) + 1);
        %trgMap.remove(%plain);
        %n = %n + 1;
    }
}

function initializeEmoteDictPublic(%dict)
{
    initGenderedDances();
    insertPlainToCodedListIntoMap($dancesMap_Lounge_F, %dict);
    insertPlainToCodedListIntoMap($dancesMap_Lounge_M, %dict);
    insertPlainToCodedListIntoMap($dancesMap_Break_F, %dict);
    insertPlainToCodedListIntoMap($dancesMap_Break_M, %dict);
    insertPlainToCodedListIntoMap($dancesMap_Thrilla, %dict);
    insertPlainToCodedListIntoMap($dancesMap_GoGo_F, %dict);
    insertPlainToCodedListIntoMap($dancesMap_GoGo_M, %dict);
    insertPlainToCodedListIntoMap($dancesMap_HipHop, %dict);
    insertPlainToCodedListIntoMap($dancesMap_JB, %dict);
    insertPlainToCodedListIntoMap($dancesMap_Goth_F, %dict);
    insertPlainToCodedListIntoMap($dancesMap_Goth_M, %dict);
    %dict.put("smile", "sml");
    %dict.put("angry", "ang");
    %dict.put("talk-to-the-hand", "ttth");
    %dict.put("wave", "wve");
    %dict.put("bow", "bow");
    %dict.put("boo", "boo");
    %dict.put("cool", "cool");
    %dict.put("shhh", "shhh");
    %dict.put("flirt", "flr");
    %dict.put("lol", "lol");
    %dict.put("talk", "tlk");
    %dict.put("listen", "lsn");
    %dict.put("sad", "sad");
    %dict.put("surprised", "spr");
    %dict.put("scared", "srd");
    %dict.put("cry", "cry");
    %dict.put("in-love", "ilve");
    %dict.put("kiss", "kiss");
    %dict.put("embarassed", "emb");
    %dict.put("embarrassed", "emb");
    %dict.put("vomit", "vom");
    %dict.put("puke", "vom");
    %dict.put("upchuck", "vom");
    %dict.put("confused", "cnf");
    %dict.put("sleepy", "slpy");
    %dict.put("whew", "whw");
    %dict.put("rotfl", "rotfl");
    %dict.put("applause", "apls");
    %dict.put("applaud for", "apls01");
    %dict.put("doh", "doh");
    %dict.put("waiting", "wait");
    %dict.put("busy", "busy");
    %dict.put("not-listening", "nlst");
    %dict.put("hmm", "think");
    %dict.put("thinking", "think");
    %dict.put("sit", "widl2");
    %dict.put("shoo", "shoo");
    %dict.put("shrug", "shrug");
    %dict.put("supermodel-turn", "tyrturn");
    %dict.put("point", "point");
    %dict.put("yes", "yes");
    %dict.put("no", "no");
    %dict.put("come-here", "here");
    %dict.put("thumbs-up", "thmup");
    %dict.put("thumbs-down", "thmdn");
    %dict.put("shake-fist-at", "skfst");
    %dict.put("photo-pose-01", "ph01");
    %dict.put("photo-pose-02", "ph02");
    %dict.put("photo-pose-03", "ph03");
    %dict.put("photo-pose-04", "ph04");
    %dict.put("photo-pose-05", "ph05");
    %dict.put("photo-pose-06", "ph06");
    %dict.put("photo-pose-07", "ph07");
    %dict.put("photo-pose-08", "ph08");
    %dict.put("hug-initiate", "huger");
    %dict.put("hug-finish", "hugee");
    %dict.put("vside", "vside");
    %dict.put("reauxshambeaux synch", "rsbloop");
    %dict.put("reaux", "rsbreaux");
    %dict.put("sham", "rsbsham");
    %dict.put("beaux", "rsbbeaux");
    %dict.put("hiFive-initiate", "hi5er");
    %dict.put("hiFive-finish", "hi5ee");
    %dict.put("tapglass", "tapglass");
    %dict.put("crowd-wave", "crdwve");
    %dict.put("loser", "losr");
    %dict.put("o-my-nails", "admrnail");
    return ;
}
function initializeEmoteDictProtected(%dict)
{
    %dict.put("walk-forwards", "wlkf1");
    %dict.put("walk-backwards", "wlkb1");
    %dict.put("idle-1a", "idl1a");
    %dict.put("idle-1b", "idl1b");
    %dict.put("idle-1c", "idl1c");
    %dict.put("idle-1d", "idl1d");
    %dict.put("idle-2a", "idl2a");
    %dict.put("idle-2b", "idl2b");
    %dict.put("idle-2c", "idl2c");
    %dict.put("idle-2d", "idl2d");
    %dict.put("idle-3a", "idl3a");
    %dict.put("idle-3b", "idl3b");
    %dict.put("idle-3c", "idl3c");
    %dict.put("idle-3d", "idl3d");
    %dict.put("idle-sit-1", "cidl1a");
    %dict.put("idle-sit-2", "cidl2a");
    %dict.put("idle-lounge-1a", "lidl1a");
    %dict.put("idle-lounge-2a", "lidl2a");
    %dict.put("sit-1", "cent");
    %dict.put("stand-1", "cext");
    %dict.put("sit-2", "lent");
    %dict.put("stand-2", "lext");
    %dict.put("jump", "jmp");
    %dict.put("fall", "fall");
    %dict.put("rail-sit-1", "rent");
    %dict.put("rail-stand-1", "rext");
    %dict.put("idle-rail-1", "ridl1");
    %dict.put("idle-wall-1", "widl1");
    %dict.put("wall-sit-1", "went2");
    %dict.put("wall-stand-1", "wext2");
    %dict.put("idle-wall-2", "widl2");
    %dict.put("idle-stool-1", "sidl1");
    %dict.put("stool-sit-1", "sent");
    %dict.put("stool-stand-1", "sext");
    %dict.put("hottub-idle-1", "htidl1");
    %dict.put("listeningstation-idle", "lsnidl1");
    %dict.put("listeningstation-enter", "lsnent");
    %dict.put("listeningstation-exit", "lsnext");
    %dict.put("climb-sit-1", "clbent");
    %dict.put("climb-stand-1", "clbext");
    %dict.put("climb-rail-1", "clbidl1");
    %dict.put("lay-idle-1", "lyidl1");
    %dict.put("bunny-hop", "bhop");
    %dict.put("bed-getin-right", "bedentr");
    %dict.put("bed-getout-right", "bedextr");
    %dict.put("bed-getin-left", "bedextl");
    %dict.put("bed-getout-left", "bedentl");
    %dict.put("bed-lay-onback", "bedslpbk");
    %dict.put("bed-lay-onrightside", "bedslpsdr");
    %dict.put("bed-lay-onleftside", "bedslpsdl");
    %dict.put("bed-lay-handsbehindhead", "bedrlx");
    %dict.put("bed-lay-chezlounge", "chzlngidl1");
    %dict.put("pocketbike-ride", "pckride");
    return ;
}
function initializeEmoteDict()
{
    if (isObject(EmoteDict))
    {
        EmoteDict.delete();
    }
    new StringMap(EmoteDict);
    if (isObject(MissionCleanup))
    {
        MissionCleanup.add(EmoteDict);
    }
    initializeEmoteDictPublic(EmoteDict);
    return ;
}
initializeEmoteDict();
function intializeSharedEmoteDict()
{
    if (isObject(SharedEmoteDict))
    {
        SharedEmoteDict.delete();
    }
    new StringMap(SharedEmoteDict);
    if (isObject(MissionCleanup))
    {
        MissionCleanup.add(SharedEmoteDict);
    }
    %dict = SharedEmoteDict;
    %dict.put("dnc1", "dnc2");
    %dict.put("dnc2", "dnc3");
    %dict.put("dnc3", "dnc4");
    %dict.put("dnc4", "dnc1");
    %dict.put("wve", "wve");
    %dict.put("cool", "cool");
    %dict.put("shhh", "nlst");
    %dict.put("shhh", "nlst");
    %dict.put("tlk", "lsn");
    %dict.put("tlk", "lsn");
    %dict.put("widl2", "widl2");
    %dict.put("hdncb4", "hdncb1");
    %dict.put("hdncb1", "hdncb2");
    %dict.put("hdncb2", "hdncb3");
    %dict.put("hdncb3", "hdncb4");
    return ;
}
function getSharedEmote(%theirEmote)
{
    %got = SharedEmoteDict.get(%theirEmote);
    return %got;
}
intializeSharedEmoteDict();
function initializeProtectedAnims()
{
    if (isObject(ProtectedAnimsDict))
    {
        ProtectedAnimsDict.delete();
    }
    new StringMap(ProtectedAnimsDict);
    if (isObject(MissionCleanup))
    {
        MissionCleanup.add(ProtectedAnimsDict);
    }
    %dict = ProtectedAnimsDict;
    %dict.put("dnc1", 2);
    %dict.put("dnc2", 2);
    %dict.put("dnc3", 2);
    %dict.put("dnc4", 2);
    %dict.put("hdncb1", 2);
    %dict.put("hdncb2", 2);
    %dict.put("hdncb3", 2);
    %dict.put("hdncb4", 2);
    %dict.put("tlk", 2);
    %dict.put("cidl1a", 2);
    %dict.put("cidl2a", 2);
    %dict.put("lidl1a", 2);
    %dict.put("lidl2a", 2);
    %dict.put("ridl1", 2);
    %dict.put("widl1", 2);
    %dict.put("widl2", 2);
    %dict.put("sidl1", 2);
    %dict.put("htidl1", 2);
    %dict.put("lsnidl1", 2);
    %dict.put("blidl1", 2);
    %dict.put("sidl2", 2);
    %dict.put("sidl3", 2);
    %dict.put("lidl3a", 2);
    %dict.put("rlidl1", 2);
    %dict.put("zsidl1", 2);
    %dict.put("plidl1", 2);
    %dict.put("sidl4", 2);
    %dict.put("mjbtrst1", 2);
    %dict.put("mjbtrst2", 2);
    %dict.put("mjclaw1", 2);
    %dict.put("mjclaw2", 2);
    %dict.put("mjdstep", 2);
    %dict.put("mjhtilt", 2);
    %dict.put("mjhturn", 2);
    %dict.put("mjlih", 2);
    %dict.put("mjmstep", 2);
    %dict.put("mjptrst", 2);
    %dict.put("mjpvt", 2);
    %dict.put("mjshrug", 2);
    %dict.put("mjspin", 2);
    %dict.put("mjsstep", 2);
    %dict.put("mjsstep2", 2);
    %dict.put("mjtclap1", 2);
    %dict.put("mjtclap2", 2);
    %dict.put("mjzpose", 2);
    %dict.put("mjzstep", 2);
    %dict.put("zidl1", 2);
    %dict.put("sitks", 2);
    %dict.put("go01", 2);
    %dict.put("go02", 2);
    %dict.put("go03", 2);
    %dict.put("go04", 2);
    %dict.put("go05", 2);
    %dict.put("go06", 2);
    %dict.put("go07", 2);
    %dict.put("go08", 2);
    %dict.put("dhtoe", 2);
    %dict.put("dlnwit", 2);
    %dict.put("dshfle", 2);
    %dict.put("dslpsld", 2);
    %dict.put("dwlkit", 2);
    %dict.put("dxhop", 2);
    %dict.put("d2step", 2);
    %dict.put("d2stepx", 2);
    %dict.put("dvstepb", 2);
    %dict.put("jbdnc01", 2);
    %dict.put("jbdnc02", 2);
    %dict.put("jbdnc03", 2);
    %dict.put("jbdnc04", 2);
    %dict.put("jbdnc05", 2);
    %dict.put("jbdnc06", 2);
    %dict.put("jbdnc07", 2);
    %dict.put("jbdnc08", 2);
    %dict.put("jbdnc10", 2);
    %dict.put("lyidl1", 2);
    %dict.put("dgoth01", 2);
    %dict.put("dgoth02", 2);
    %dict.put("dgoth03", 2);
    %dict.put("dgoth04", 2);
    %dict.put("dgoth05", 2);
    %dict.put("bedslpbk", 2);
    %dict.put("bedslpsdr", 2);
    %dict.put("bedslpsdl", 2);
    %dict.put("bedrlx", 2);
    %dict.put("chzlngidl1", 2);
    %dict.put("tsitidl01", 2);
    %dict.put("reachdown", 2);
    %dict.put("spinbottle", 1);
    %dict.put("arcadeidl", 2);
    %dict.put("ssidl1", 2);
    %dict.put("pckride", 2);
    %dict.put("spidl1", 2);
    %dict.put("bchlid1a", 2);
    %dict.put("cidl3a", 2);
    %dict.put("clbidl1", 2);
    %dict.put("delite1", 2);
    %dict.put("delite2", 2);
    %dict.put("delite3", 2);
    %dict.put("delite4", 2);
    %dict.put("delite5", 2);
    %dict.put("dymca1", 2);
    %dict.put("dymca2", 2);
    %dict.put("dymca3", 2);
    %dict.put("cutout01", 2);
    %dict.put("cutout02", 2);
    %dict.put("smanidl1", 2);
    %dict.put("spedidl", 2);
    %dict.put("spcft", 2);
    %dict.put("smcidl1", 2);
    %dict.put("spcidl1", 2);
    %dict.put("edrink01", 2);
    %dict.put("edrink02", 2);
    %dict.put("edrink03", 2);
    %dict.put("fdrink01", 2);
    %dict.put("fdrink02", 2);
    %dict.put("fdrink03", 2);
    %dict.put("udrink01", 2);
    %dict.put("drinkbottle01", 2);
    %dict.put("drinkbottle02", 2);
    %dict.put("drinkbottle03", 2);
    %dict.put("drinkmugwipe02", 2);
    return ;
}
initializeProtectedAnims();
function intializeDrinkExcludedAnims()
{
    if (isObject(DrinkExcludedAnimsDict))
    {
        DrinkExcludedAnimsDict.delete();
    }
    new StringMap(DrinkExcludedAnimsDict);
    if (isObject(MissionCleanup))
    {
        MissionCleanup.add(DrinkExcludedAnimsDict);
    }
    %dict = DrinkExcludedAnimsDict;
    %dict.put("rotfl", 2);
    return ;
}
intializeDrinkExcludedAnims();

