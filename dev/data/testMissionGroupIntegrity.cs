DeclareTestSuite("TestSuite_MissionGroup");
function TestSuite_MissionGroup::setup(%this) {
    "TEST_MISSIONGROUPINTEGRITY".addTestCase(%this);
};
function TEST_MISSIONGROUPINTEGRITY::AddOkClass(%this, %okClassName) {
    %this.okClass = %okClassName @ %this.okClassCount;
    %this.okClassCount = (%this.okClassCount + 1.0);
};
function TEST_MISSIONGROUPINTEGRITY::Add_NO_CacheClass(%this, %okClassName) {
    %this.ableToNotCacheClass = %okClassName @ %this.ableToNotCacheClassCount;
    %this.ableToNotCacheClassCount = (%this.ableToNotCacheClassCount + 1.0);
};
function TEST_MISSIONGROUPINTEGRITY::AddInitiallyNotNetCacheableClass(%this, %classname) {
    %this.InitiallyNotNetCacheableClass = %classname @ %this.InitiallyNotNetCacheableClassCount;
    %this.InitiallyNotNetCacheableClassCount = (%this.InitiallyNotNetCacheableClassCount + 1.0);
};
function TEST_MISSIONGROUPINTEGRITY::AddObjectInstancesMustHaveUniqueNamesClass(%this, %classname) {
    %this.instancesMustHaveUniqueNamesClass = %classname @ "\t" @ %this.instancesMustHaveUniqueNamesClass;
};
function TEST_MISSIONGROUPINTEGRITY::objectInstanceMustHaveUniqueName(%this, %obj) {
    %classname = %obj.getClassName();
    %index = findField(%this.instancesMustHaveUniqueNamesClass, %classname);
    return (%index >= 0.0);
};
function TEST_MISSIONGROUPINTEGRITY::InitializeNPCNames(%this) {
    if (!(MissionInfo @ " " @ %this.skipNPCCheck $= "")) {
    }
    if ((%this.skipNPCCheck == MissionInfo)) {
        log("general", "debug", "Skipping NPC name check.");
        %this.NPCNameMap = 1.0 @ 0;
        return;
    }
    %this.NPCNameMap = new StringMap("") {
        ignoreCase = 0 @ 1;
    };
    if (isObject(MissionCleanup)) {
        %this.NPCNameMap.add(MissionCleanup);
    }
    %file = new FileObject("");;
    0;
    if ("dev/data/npc_usernames.txt".openForRead(%file)) {
        while (!(%file.isEOF())) {
            %npcName = %file.readLine();
            "NPC".put(%this.NPCNameMap, %npcName);
        }
    }
    %file.delete();
    return !(%file.isEOF());
    %file.close();
    %file.delete();
};
function TEST_MISSIONGROUPINTEGRITY::setup(%this) {
    %this.okClassCount = 0;
    %this.ableToNotCacheClassCount = 0;
    %this.InitiallyNotNetCacheableClassCount = 0;
    %this.instancesMustHaveUniqueNamesClassCount = 0;
    "InteriorInstance".Add_NO_CacheClass(%this);
    "ETSSeatMarker".Add_NO_CacheClass(%this);
    "MissionMarker".Add_NO_CacheClass(%this);
    "ZoneBox".Add_NO_CacheClass(%this);
    "ETSSeatMarker".AddInitiallyNotNetCacheableClass(%this);
    "ZoneBox".AddInitiallyNotNetCacheableClass(%this);
    "TheoraRenderer".AddInitiallyNotNetCacheableClass(%this);
    "FFMPEGRenderer".AddInitiallyNotNetCacheableClass(%this);
    "Trigger".AddInitiallyNotNetCacheableClass(%this);
    "PhysicalZone".AddInitiallyNotNetCacheableClass(%this);
    "SpawnSphere".AddInitiallyNotNetCacheableClass(%this);
    "Player".AddInitiallyNotNetCacheableClass(%this);
    "AIPlayer".AddInitiallyNotNetCacheableClass(%this);
    "Trigger".AddObjectInstancesMustHaveUniqueNamesClass(%this);
    "AdvertShape".AddOkClass(%this);
    "AIPlayer".AddOkClass(%this);
    "AntiPortal".AddOkClass(%this);
    "ZoneBox".AddOkClass(%this);
    "AudioEmitter".AddOkClass(%this);
    if (isFunction("Using_DShow") && Using_DShow()) {
        "DSRenderer".AddOkClass(%this);
    }
    "ETSSeatMarker".AddOkClass(%this);
    if (isFunction("Using_FFMPEG") && Using_FFMPEG()) {
        "FFMPEGRenderer".AddOkClass(%this);
    }
    "SlaveRenderer".AddOkClass(%this);
    if (isFunction("Using_DF") && Using_DF()) {
        "DFTextureAdvert".AddOkClass(%this);
    }
    "InteriorInstance".AddOkClass(%this);
    "TerrainBlock".AddOkClass(%this);
    "Lightning".AddOkClass(%this);
    "MissionArea".AddOkClass(%this);
    "MissionMarker".AddOkClass(%this);
    "EtsDoor".AddOkClass(%this);
    "ParticleEmitterNode".AddOkClass(%this);
    "Path".AddOkClass(%this);
    "PhysicalZone".AddOkClass(%this);
    "SimGroup".AddOkClass(%this);
    "SimSpace".AddOkClass(%this);
    "Sky".AddOkClass(%this);
    "SpawnSphere".AddOkClass(%this);
    "StaticShape".AddOkClass(%this);
    "Sun".AddOkClass(%this);
    "TSStatic".AddOkClass(%this);
    "TSDynamic".AddOkClass(%this);
    if (isFunction("Using_Theora") && Using_Theora()) {
        "TheoraRenderer".AddOkClass(%this);
    }
    "Trigger".AddOkClass(%this);
    "WaterBlock".AddOkClass(%this);
    "WayPoint".AddOkClass(%this);
    "fxFoliageReplicator".AddOkClass(%this);
    "fxLight".AddOkClass(%this);
    "fxShapeReplicator".AddOkClass(%this);
    "fxSpectrumAnalyzer".AddOkClass(%this);
    "fxSunLight".AddOkClass(%this);
    "sgDecalProjector".AddOkClass(%this);
    "sgMissionLightingFilter".AddOkClass(%this);
    "sgUniversalStaticLight".AddOkClass(%this);
    "volumeLight".AddOkClass(%this);
    "Marker".AddOkClass(%this);
    "BlockGameBase".AddOkClass(%this);
    "BlockGameTheGrind".AddOkClass(%this);
    "BlockGameMateriel".AddOkClass(%this);
    "HappyFunSquiggleBall".AddOkClass(%this);
    "ImageFrameBase".AddOkClass(%this);
    "TSText".AddOkClass(%this);
    %this.InitializeNPCNames();
};
$MAYBE_BAD_MODEL_UNIT_FLAG = 0;
function TEST_MISSIONGROUPINTEGRITY::runTest(%this) {
    if ($MAYBE_BAD_MODEL_UNIT_FLAG) {
        "AINT NO MODEL UNIT HIGH ENOUUGH, NO MORE CRACK PIPE FOR YOU!, FIX ME!!!, filename does not contain the string modelunit, likely not a real model unit, should probably be pointing to different _generated.cs file".assert(%this, 0);
    }
    "invalidGroup".RecursivelyCheckForThingsThatDontBelong(%this, MissionGroup);
    %this.CheckBuildingTransitionSetup();
    %this.CheckPrivateSpaceSetup();
    %this.CheckDatablockSetup();
    %this.CheckUniqueObjectNames();
    %this.CheckPaperDollSKUs();
};
function TEST_MISSIONGROUPINTEGRITY::TearDown(%this) {
    if (isObject(%this.NPCNameMap)) {
        %this.NPCNameMap.delete();
        %this.NPCNameMap = 0;
    }
};
function CountObjectsInMissionWithName(%name) {
    %v = SimGroupVisitor::construct("NameCounterVisitor");
    %v.skipSimGroups = 0;
    %v.count = 0;
    %v.nameToCount = %name;
    SimGroupVisitor::VisitSimgroup(MissionGroup, %v);
    %count = %v.count;
    %v.delete();
    return %count;
};
function NameCounterVisitor::visitObject(%this, %obj) {
    if ((%obj.getName() $= %this.nameToCount)) {
        %this.count = (%this.count + 1.0);
    }
};
function TEST_MISSIONGROUPINTEGRITY::assertCount(%this, %name, %expected_count, %message) {
    if ((%expected_count > 0.0)) {
        %message.assert(%this, isObject(%name));
    }
    %count = CountObjectsInMissionWithName(%name);
    "expected there to be" @ " " @ %expected_count @ " " @ "of" @ " " @ %name @ " " @ "but found" @ " " @ %count @ " " @ ".  maybe you accidentally named the other ones this? or you accidentally copied and pasted it?".assert(%this, (%count == %expected_count));
};
function TEST_MISSIONGROUPINTEGRITY::assertCountAtMost(%this, %name, %atMost_count, %message) {
    if ((%atMost_count > 0.0)) {
        %message.assert(%this, isObject(%name));
    }
    %count = CountObjectsInMissionWithName(%name);
    "expected there to be at most" @ " " @ %atMost_count @ " " @ "of" @ " " @ %name @ " " @ "but found" @ " " @ %count @ " " @ ".  maybe you accidentally named the other ones this? or you accidentally copied and pasted it?".assert(%this, (%count <= %atMost_count));
};
function TEST_MISSIONGROUPINTEGRITY::CheckBuildingTransitionSetup(%this) {
    if (isObject(BuildingDefinitions)) {
        "there should only be a single BuildingDefinitions simgroup".assertCount(%this, "BuildingDefinitions", 1);
        %count = BuildingDefinitions.getCount();
        %i = 0;
        while ((%i < %count)) {
            %obj = %i.getObject(BuildingDefinitions);
            if (isObject(%obj)) {
                %name = %obj.buildingName;
                %vurl = Buildings::getReturnVURL(%name);
                %parsedVURLobject = vurlGetParsedVurl(%vurl);
                %returnName = %parsedVURLobject.targetDest;
                "Building(" @ " " @ %name @ " " @ ") return spawn \"" @ %returnName @ "\" is not an object, you should make a simgroup with that name and place a spawn sphere in it for this building".assert(%this, isObject(%returnName));
                %parsedVURLobject.delete();
            }
            %i = (%i + 1.0);
        }
    }
    if (isObject(NPCGroup)) {
        "there should only be at most a single NPCGroup SimGroup".assertCountAtMost(%this, "NPCGroup", 1);
    }
};
function TEST_MISSIONGROUPINTEGRITY::CheckPrivateSpaceSetup(%this) {
    "There is no MissionInfo scriptobject, This is used to determine various things about the missions and should exist in this one too".assertCount(%this, "MissionInfo", 1);
    if ((MissionInfo @ " " @ %parsedVURLobject.mode $= "PrivateSpaceDesign")) {
        if (isObject(PRIVATESPACE_OFFSETMARKER)) {
            "private space missions must have only ONE PRIVATESPACE_OFFSETMARKER, this is optional and if found it will be used as the root position for the private space instead of the customizable area".assertCount(%this, "PRIVATESPACE_OFFSETMARKER", 1);
        }
        "private space missions must have a PRIVATESPACE_GROUP simgroup, everything you put in this simgroup will be part of the private space that is replicated in the grid server".assertCount(%this, "PRIVATESPACE_GROUP", 1);
        "private space missions must have a PRIVATESPACE_AREA customizable space trigger, this defines the bounds of the customizable space, and will also control where they can move the furniture, and where the music plays.".assertCount(%this, "PRIVATESPACE_AREA", 1);
        "private space missions must have a PRIVATESPACE_INTERIOR dif interior defined, this is the building that can have its surfaces customized.".assertCount(%this, "PRIVATESPACE_INTERIOR", 1);
        "private space missions must have a PRIVATESPACE_ENTRYSPAWN spawn sphere, this is where the player spawns when they enter the space, should go close to the door".assertCount(%this, "PRIVATESPACE_ENTRYSPAWN", 1);
        "private space missions must have a PRIVATESPACE_EXITTRANSITION, this is the teleportal that goes back to city you came from,should go close to the door".assertCount(%this, "PRIVATESPACE_EXITTRANSITION", 1);
        "private space missions must have a PRIVATESPACE_ZONEBOX, this is the zonebox object that defines scope for this apartment, it should cover your entire apartment and anything in it that can be scoped in and out. this will handle it instead of the interior.  There should only be one, and it should cover your entire apartment so that all furniture, players etc will be inside it for this apartment".assertCount(%this, "PRIVATESPACE_ZONEBOX", 1);
        if (isObject(PRIVATESPACE_AREA)) {
        }
        if (isObject(PRIVATESPACE_ZONEBOX)) {
            %worldBox = PRIVATESPACE_AREA.getWorldBox();
            %minP = getWords(%worldBox, 0, 2);
            %maxP = getWords(%worldBox, 3, 5);
            %areaminx = getWord(%minP, 0);
            %areaminy = getWord(%minP, 1);
            %areaminz = getWord(%minP, 2);
            %areamaxx = getWord(%maxP, 0);
            %areamaxy = getWord(%maxP, 1);
            %areamaxz = getWord(%maxP, 2);
            %worldBox = PRIVATESPACE_ZONEBOX.getWorldBox();
            %minP = getWords(%worldBox, 0, 2);
            %maxP = getWords(%worldBox, 3, 5);
            %zoneminx = getWord(%minP, 0);
            %zoneminy = getWord(%minP, 1);
            %zoneminz = getWord(%minP, 2);
            %zonemaxx = getWord(%maxP, 0);
            %zonemaxy = getWord(%maxP, 1);
            %zonemaxz = getWord(%maxP, 2);
            if ((%zoneminx <= %areaminx)) {
            }
            if ((%zoneminy <= %areaminy)) {
            }
            if ((%zoneminz <= %areaminz)) {
            }
            if ((%zonemaxx >= %areamaxx)) {
            }
            if ((%zonemaxy >= %areamaxy)) {
            }
            %contained = (%zonemaxz >= %areamaxz);
            " PRIVATESPACE_AREA  must be entirely inside of PRIVATESPACE_ZONEBOX, it looks like the area is outside in this mission, make sure the zonebox surrounds it completely. thanks!".assert(%this, %contained);
        }
    }
    if ((MissionInfo @ " " @ %parsedVURLobject.mode $= "PrivateSpaceGrid")) {
        "For privatespace grid servers, a modelID must be specified in MissionInfo, this is the type of floorplan supported by this server".assertDifferentString(%this, MissionInfo, %parsedVURLobject.modelID, "");
        "For privatespace grid servers, a building must be specified in MissionInfo, this is the building that connects to this grid server".assertDifferentString(%this, MissionInfo, %parsedVURLobject.building, "");
        "For privatespace grid servers, a spacePrefix must be specified in MissionInfo, this is the prefix that will be used to name each space".assertDifferentString(%this, MissionInfo, %parsedVURLobject.spacePrefix, "");
    }
};
function TEST_MISSIONGROUPINTEGRITY::CheckDatablockSetup(%this) {
    // unhandled opcode 1645 at 0x00000B2E
    if ("no DataBlockGroup!".assert(%this, isObject(%group))) {
        return;
    }
    %n = (%group.getCount() - 1.0);
    while ((%n >= 0.0)) {
        %obj = %n.getObject(%group);
        if ("non-object in DataBlockGroup!".assert(%this, isObject(%obj))) {
        }
        if ("checkIntegrity".hasMethod(%obj)) {
            %this.checkIntegrity(%obj);
        }
        %n = (%n - 1.0);
    }
};
function TEST_MISSIONGROUPINTEGRITY::CheckUniqueObjectNames(%this) {
    // unhandled opcode 1645 at 0x00000BC9
    %n = MissionGroup;
    if ("no MissionGroup!".assert(%this, isObject(%group))) {
        return;
    }
    %nameMap = safeNewScriptObject("StringMap", "", 0);
    %nameMap._checkUniqueObjectNames_Recursive(%this, %group);
    %nameMap.delete();
};
function TEST_MISSIONGROUPINTEGRITY::_checkUniqueObjectNames_Recursive(%this, %obj, %nameMap) {
    %objName = %obj.getName();
    if (!(%objName $= "")) {
        if (%objName.hasKey(%nameMap)) {
            %otherObj = %objName.get(%nameMap);
            "object \"" @ getDebugString(%obj) @ "\" must have a unique name.".assert(%this, !(%obj.objectInstanceMustHaveUniqueName(%this)));
            "object \"" @ getDebugString(%otherObj) @ "\" must have a unique name.".assert(%this, !(%otherObj.objectInstanceMustHaveUniqueName(%this)));
        }
        %obj.put(%nameMap, %objName);
    }
    if (%obj.isClassSimSet()) {
        %num = %obj.getCount();
        %n = 0;
        while ((%n < %num)) {
            %child = %n.getObject(%obj);
            %nameMap._checkUniqueObjectNames_Recursive(%this, %child);
            %n = (%n + 1.0);
        }
    }
};
function TEST_MISSIONGROUPINTEGRITY::CheckPaperDollSKUs(%this) {
    paperDoll_InitPermutations();
};
function MissionMarkerData::checkIntegrity(%this, %testCase) {
    if (!(%this.sitIdle.isProtectedSitAnimException(%testCase))) {
        %isProtected = %this.sitIdle.hasKey(ProtectedAnimsDict);
        "seat marker datablock \"" @ %this.getName() @ "\" has a non-protected idle anim, \"" @ %this.sitIdle @ "\". You may want to add it to initializeProtectedAnims().".assert(%testCase, %isProtected);
    }
};
function TEST_MISSIONGROUPINTEGRITY::IsAbleToNotCache(%this, %obj) {
    %classname = %obj.getClassName();
    %ableToNotCache = 0;
    %i = 0;
    if ((%i < %this.ableToNotCacheClassCount)) {
    }
    while ((%ableToNotCache == 0.0)) {
        if ((%i @ " " @ %this.ableToNotCacheClass $= %classname)) {
            %ableToNotCache = 1;
        }
        %i = (%i + 1.0);
        if ((%i < %this.ableToNotCacheClassCount)) {
        }
    }
    return %ableToNotCache;
};
function TEST_MISSIONGROUPINTEGRITY::getInitialNetCacheable(%this, %obj) {
    if (!(%obj.isClassNetObject())) {
        return 0;
    }
    %classname = %obj.getClassName();
    %ret = 1;
    %i = 0;
    if ((%i < %this.InitiallyNotNetCacheableClassCount)) {
    }
    while ((%ret == 1.0)) {
        if ((%i @ " " @ %this.InitiallyNotNetCacheableClass $= %classname)) {
            %ret = 0;
        }
        %i = (%i + 1.0);
        if ((%i < %this.InitiallyNotNetCacheableClassCount)) {
        }
    }
    return %ret;
};
function TEST_MISSIONGROUPINTEGRITY::RecursivelyCheckForThingsThatDontBelong(%this, %obj, %parentGroupName) {
    %parentGroupName @ " " @ ", the object " @ %obj @ "is not an object".assert(%this, isObject(%obj));
    if (!(isObject(%obj))) {
        return;
    }
    if (%obj.isClassSimSet()) {
        %num = %obj.getCount();
        %n = 0;
        while ((%n < %num)) {
            %obj.getName().RecursivelyCheckForThingsThatDontBelong(%this, %n.getObject(%obj));
            %n = (%n + 1.0);
        }
    }
    %classname = %obj.getClassName();
    (%n < %num);
    %belongs = 0;
    %i = 0;
    while ((%i < %this.okClassCount)) {
        if ((%i @ " " @ %this.okClass $= %classname)) {
            %belongs = 1;
        }
        %i = (%i + 1.0);
    }
    %actionNeeded = "This object does not belong and should probably be deleted";
    (%i < %this.okClassCount);
    %ableToNotCache = %obj.IsAbleToNotCache(%this);
    if ((%ableToNotCache == 0.0)) {
        %exceptions = "SimGroup SimSet SimSpace ScriptObject";
        %wordLoc = findWord(%exceptions, %classname);
        if ((%wordLoc < 0.0)) {
            "this object is a type that should be able to cache, you should set isNetCacheable to 1" @ " " @ getDebugString(%obj).assert(%this, (%obj.isNetCacheable == 1.0));
        }
    }
    if (%obj.isClassSimSpace()) {
        if (!(getSubStr(%obj.getName(), 0, 9) $= "SimSpace_")) {
            %belongs = 0;
            %actionNeeded = "SimSpaces should be named starting with \"SimSpace_\"." @ " " @ %obj.getName() @ " " @ "is breakin' the law!";
        }
        if (!(%obj.hasSpaceDef())) {
            %belongs = 0;
            %actionNeeded = "SimSpace" @ " " @ %obj.getName() @ " " @ "has no associated SpaceDef.";
        }
    }
    if (%obj.isClassAIPlayer()) {
        "AIPlayer is not in NPCGroup:" @ " " @ getDebugString(%obj).assert(%this, (%obj.getObjectIndex(NPCGroup) > -(1.0)));
        if (isObject(%this.NPCNameMap)) {
        }
        if ((%obj.getName().findKey(%this.NPCNameMap) == -(1.0))) {
            %belongs = 0;
            %actionNeeded = "NPC" @ " " @ %obj.getName() @ " " @ "should be listed in npc_usernames.txt.";
        }
    }
    if ((%classname $= "MissionMarker")) {
        %dbName = %obj.getDataBlock().getName();
        %seatMarkerFound = strstr(%dbName, "SeatMarker");
        if ((%seatMarkerFound != -(1.0))) {
            %belongs = 0;
            %actionNeeded = "This is an old style seat marker and shuold be converted to an ETSSeatMarker";
        }
        if (!(%obj.sitIdle.isProtectedSitAnimException(%this))) {
            %isProtected = %obj.sitIdle.hasKey(ProtectedAnimsDict);
            "seat marker \"" @ %obj.getName() @ "\" has a non-protected idle anim, \"" @ %obj.sitIdle @ "\". You may want to add it to initializeProtectedAnims().".assert(%this, %isProtected);
        }
    }
    if ((%classname $= "ETSSeatMarker")) {
        if ((%obj.listeningStation $= 0)) {
            %belongs = 0;
            %actionNeeded = "ETSSeatMarkers should not have listening station of 0, listening station should be the name of an object or an empty string";
        }
        "seat markers cannot be cached, you should set isNetCacheable to 0 for the seat marker:" @ " " @ getDebugString(%obj) @ " " @ "in the group" @ " " @ %parentGroupName.assert(%this, (%obj.isNetCacheable == 0.0));
        if (!(%obj.sitIdle.isProtectedSitAnimException(%this))) {
            %isProtected = %obj.sitIdle.hasKey(ProtectedAnimsDict);
            "seat marker \"" @ %obj.getName() @ "\" has a non-protected idle anim, \"" @ %obj.sitIdle @ "\". You may want to add it to initializeProtectedAnims().".assert(%this, %isProtected);
        }
    }
    if ((%classname $= "ScriptObject")) {
        if ((%obj.getName() $= "MissionInfo")) {
            %belongs = 1;
        }
        if ((%obj.getName() $= "BUILDINGDEF")) {
            %belongs = 0;
            %actionNeeded = "BUILDINGDEFs are no longer used, the EntryTrigger for the building should be in teh BuildingDefinitions simgroup instead";
        }
        if ((%obj.class $= "SalonChairEngageScriptObject")) {
            %belongs = 1;
        }
    }
    if ((%classname $= "Trigger")) {
        %dbName = %obj.getDataBlock().getName();
        if ((%dbName $= "SeatingArea") && (%obj.autosit $= "")) {
            %belongs = 0;
            %actionNeeded = "SeatingArea triggers are only needed if you are using autosit, otherwise it should be deleted";
        }
        if ((%dbName $= "DoorTrigger")) {
            %myDoor = %obj.findMyDoor();
            %parentGroupName @ " " @ ", the object " @ %myDoor @ "is not an object, this is the door referenced by" @ " " @ getDebugString(%obj).assert(%this, isObject(%myDoor));
            %doorsDataBlock = %myDoor.getDataBlock();
            %parentGroupName @ " " @ ", the object " @ %myDoor @ " is not a datablock based door, you should make the datablock first and place that in the world, not a Static, look in Shapes->Doors for your datablock name, this is the door referenced by" @ " " @ getDebugString(%obj).assert(%this, !(%doorsDataBlock $= ""));
        }
    }
    if ((MissionInfo @ " " @ %obj.mode $= "PrivateSpaceDesign")) {
        if ((%classname $= "TSStatic")) {
            %belongs = 0;
            %actionNeeded = "Static Shapes should not be used in individual Private spaces,  instead you should use Dynamic Shapes,   these are TSDynamic instead of TSStatic";
        }
        if ((%classname $= "InteriorInstance")) {
            %isManagingZones = %obj.managezones;
            %parentGroupName @ " " @ ", the object" @ " " @ %obj.getDebugString() @ " " @ " - In a private space, interiors should not be managing zones, set \"managezones\" to zero for this interior instance, and make sure you have a ZoneBox surrounding the entire apartment, the ZoneBox will serve as the zone manager".assert(%this, !(%isManagingZones));
        }
        if ((%obj.getName() $= "PRIVATESPACE_AREA")) {
            %dbName = %obj.getDataBlock().getName();
            "the PRIVATESPACE_AREA object should be a CustomizableSpaceTriggerData trigger".assertSameString(%this, %dbName, "CustomizableSpaceTriggerData");
        }
        if ((%obj.getName() $= "PRIVATESPACE_INTERIOR")) {
            "the PRIVATESPACE_INTERIOR object should be a InteriorInstance object".assertSameString(%this, %classname, "InteriorInstance");
            "The PRIVATESPACE_INTERIOR will have the textures on it changed at runtime for this reason, it should have isNetCacheable set to 0, make that change to fix this problem".assert(%this, (%obj.isNetCacheable == 0.0));
        }
    }
    if (!(%obj.dataBlock $= "")) {
        "You cannot have an object with the same name as a datablock, It's a good idea to name your datablocks something like blah_DB so you don't accidentally call your object the same name".assertDifferentString(%this, %obj.dataBlock, %obj.getName());
    }
    %openSpace = "                                       action to take:";
    %message = "in" @ " " @ %parentGroupName @ " " @ ": object:" @ " " @ %obj @ " " @ "of class:" @ " " @ %classname @ " " @ ", with name:" @ " " @ %obj.getName() @ "\n" @ %openSpace @ " " @ %actionNeeded;
    %message.assert(%this, (%belongs == 1.0));
};
function TEST_MISSIONGROUPINTEGRITY::isProtectedSitAnimException(%this, %animName) {
    %ret = 0;
    if ((%animName $= "")) {
        %ret = 1;
    }
    if ((%animName $= "idl1a")) {
        %ret = 1;
    }
    return %ret;
};
function RecursivelyFixOldStyleSeatingAreaProblems(%obj) {
    if (!(isObject(%obj))) {
        return;
    }
    if (%obj.isClassSimGroup()) {
        %num = %obj.getCount();
        %n = 0;
        while ((%n < %num)) {
            RecursivelyFixOldStyleSeatingAreaProblems(%n.getObject(%obj));
            %n = (%n + 1.0);
        }
        return (%n < %num);
    }
    %classname = %obj.getClassName();
    if ((%classname $= "ETSSeatMarker") && (%obj.listeningStation $= 0)) {
        %obj.listeningStation = "";
        echo("cleared listening station that was 0 for " @ " " @ %obj @ " " @ "of class:" @ " " @ %classname @ " " @ ", with name:" @ " " @ %obj.getName());
    }
    if ((%classname $= "Trigger")) {
        %dbName = %obj.getDataBlock().getName();
        if ((%dbName $= "SeatingArea") && (%obj.autosit $= "")) {
            $OLDSEATAREA_KILLER_COUNT[$OLDSEATAREA_KILLER @ $OLDSEATAREA_KILLER_COUNT] = %obj.getId();
            $OLDSEATAREA_KILLER_COUNT = ($OLDSEATAREA_KILLER_COUNT + 1.0);
        }
    }
};
function FixOldStyleSeatingAreaProblems() {
    echo("getting rid of listeningstations and no longer needed seatingarea triggers ----------------------");
    $OLDSEATAREA_KILLER_COUNT = 0;
    RecursivelyFixOldStyleSeatingAreaProblems(MissionGroup);
    %i = 0;
    while ((%i < $OLDSEATAREA_KILLER_COUNT)) {
        %obj = %i[$OLDSEATAREA_KILLER @ %i];
        echo("deleteing no longer needed seating area " @ " " @ %obj @ " " @ "of class:" @ " " @ %obj.getClassName() @ " " @ ", with name:" @ " " @ %obj.getName());
        %obj.delete();
        $OLDSEATAREA_KILLER_COUNT[$OLDSEATAREA_KILLER @ $OLDSEATAREA_KILLER_COUNT] = "";
        %i = (%i + 1.0);
    }
    $OLDSEATAREA_KILLER_COUNT = 0;
    (%i < $OLDSEATAREA_KILLER_COUNT);
    echo("done----------------------");
};
function Utility::ListDataBlocksNotUsed() {
    %v = SimGroupVisitor::construct("UtilityCollectNameVisitor");
    %v.skipScriptObjects = 1;
    %v.count = 0;
    SimGroupVisitor::VisitSimgroup("DataBlockGroup", %v);
    %v2 = SimGroupVisitor::construct("UtilityDBInUseVisitor");
    %v2.skipScriptObjects = 1;
    %v2.collector = %v;
    SimGroupVisitor::VisitSimgroup("RootGroup", %v2);
    error("Listing Datablocks not used -------------------------");
    error("note:  these are only the ones not currently used based on the active objects, they may be used later or dynamically, but this should be a good starting point to check");
    echo("");
    %i = 0;
    while ((%i < %v.count)) {
        if (!(%v.uses)) {
            error(%v.theList @ " " @ "not used");
        }
        %i = (%i + 1.0);
        %i @ %i;
    }
    echo("");
    error("-----------------------------------------------------");
    %v2.delete();
    %v.delete();
};
function UtilityCollectNameVisitor::visitObject(%this, %obj) {
    if ((%obj.getClassName() $= "SimSet")) {
    }
    if ((%obj.getClassName() $= "ActionMap")) {
    }
    if ((%obj.getClassName() $= "GuiControlProfile")) {
    }
    if ((%obj.getClassName() $= "StringMap")) {
    }
    if ((%obj.getClassName() $= "GuiCursor")) {
    }
    if ((%obj.getClassName() $= "Sun")) {
    }
    if ((%obj.getClassName() $= "MissionArea")) {
    }
    if ((%obj.getClassName() $= "WaterBlock")) {
    }
    if ((%obj.getClassName() $= "InteriorInstance")) {
    }
    if ((%obj.getClassName() $= "TSStatic")) {
    }
    if ((%obj.getClassName() $= "SimObject")) {
    }
    if ((%obj.getClassName() $= "ScreenShotUploader")) {
    }
    if ((%obj.getClassName() $= "fxSpectrumAnalyzer")) {
        return;
    }
    %this.theList = %obj.getName() @ %this.count;
    %this.uses = 0 @ %this.count;
    %this.count = (%this.count + 1.0);
};
function UtilityDBInUseVisitor::visitObject(%this, %obj) {
    if ((%obj.getClassName() $= "SimSet")) {
    }
    if ((%obj.getClassName() $= "ActionMap")) {
    }
    if ((%obj.getClassName() $= "GuiControlProfile")) {
    }
    if ((%obj.getClassName() $= "StringMap")) {
    }
    if ((%obj.getClassName() $= "GuiCursor")) {
    }
    if ((%obj.getClassName() $= "Sun")) {
    }
    if ((%obj.getClassName() $= "MissionArea")) {
    }
    if ((%obj.getClassName() $= "WaterBlock")) {
    }
    if ((%obj.getClassName() $= "InteriorInstance")) {
    }
    if ((%obj.getClassName() $= "TSStatic")) {
    }
    if ((%obj.getClassName() $= "SimObject")) {
    }
    if ((%obj.getClassName() $= "ScreenShotUploader")) {
    }
    if ((%obj.getClassName() $= "fxSpectrumAnalyzer")) {
        return;
    }
    %i = 0;
    while ((%i < %this.collector.count)) {
        if ((%obj.getClassName() $= "AudioProfile")) {
            if ((%obj.description.getName() @ %i $= %this.collector.theList)) {
                %this.collector.uses = (%this.collector.uses + 1.0 @ %i);
            }
        }
        if ((%obj.getClassName() $= "ParticleEmitterData")) {
            if ((%obj.particles @ %i $= %this.collector.theList)) {
                %this.collector.uses = (%this.collector.uses + 1.0 @ %i);
            }
        }
        if ((%obj.getClassName() $= "PlayerData")) {
            if ((0 @ " " @ %obj.splashEmitter @ %i $= %this.collector.theList)) {
                %this.collector.uses = (%this.collector.uses + 1.0 @ %i);
            }
            if ((1 @ " " @ %obj.splashEmitter @ %i $= %this.collector.theList)) {
                %this.collector.uses = (%this.collector.uses + 1.0 @ %i);
            }
            if ((2 @ " " @ %obj.splashEmitter @ %i $= %this.collector.theList)) {
                %this.collector.uses = (%this.collector.uses + 1.0 @ %i);
            }
            if ((%obj.Splash @ %i $= %this.collector.theList)) {
                %this.collector.uses = (%this.collector.uses + 1.0 @ %i);
            }
            if ((%obj.dustEmitter @ %i $= %this.collector.theList)) {
                %this.collector.uses = (%this.collector.uses + 1.0 @ %i);
            }
            if ((%obj.DecalData @ %i $= %this.collector.theList)) {
                %this.collector.uses = (%this.collector.uses + 1.0 @ %i);
            }
            if ((%obj.Debris @ %i $= %this.collector.theList)) {
                %this.collector.uses = (%this.collector.uses + 1.0 @ %i);
            }
            if ((%obj.FootSound1 @ %i $= %this.collector.theList)) {
                %this.collector.uses = (%this.collector.uses + 1.0 @ %i);
            }
            if ((%obj.FootSound2 @ %i $= %this.collector.theList)) {
                %this.collector.uses = (%this.collector.uses + 1.0 @ %i);
            }
            if ((%obj.FootSound3 @ %i $= %this.collector.theList)) {
                %this.collector.uses = (%this.collector.uses + 1.0 @ %i);
            }
            if ((%obj.FootShallowSound @ %i $= %this.collector.theList)) {
                %this.collector.uses = (%this.collector.uses + 1.0 @ %i);
            }
            if ((%obj.FootWadingSound @ %i $= %this.collector.theList)) {
                %this.collector.uses = (%this.collector.uses + 1.0 @ %i);
            }
            if ((%obj.FootUnderwaterSound @ %i $= %this.collector.theList)) {
                %this.collector.uses = (%this.collector.uses + 1.0 @ %i);
            }
            if ((%obj.HalloweenfootPuffEmitter @ %i $= %this.collector.theList)) {
                %this.collector.uses = (%this.collector.uses + 1.0 @ %i);
            }
            if ((%obj.HalloweenDecalDataL @ %i $= %this.collector.theList)) {
                %this.collector.uses = (%this.collector.uses + 1.0 @ %i);
            }
            if ((%obj.HalloweenDecalDataR @ %i $= %this.collector.theList)) {
                %this.collector.uses = (%this.collector.uses + 1.0 @ %i);
            }
        }
        if ((%obj.getClassName() $= "SplashData")) {
        }
        if ((%obj.getClassName() $= "ExplosionData")) {
            if ((0 @ " " @ %obj.emitter @ %i $= %this.collector.theList)) {
                %this.collector.uses = (%this.collector.uses + 1.0 @ %i);
            }
            if ((1 @ " " @ %obj.emitter @ %i $= %this.collector.theList)) {
                %this.collector.uses = (%this.collector.uses + 1.0 @ %i);
            }
        }
        if ((%obj.getClassName() $= "RigidShapeData")) {
            if ((%obj.particleTrailEmitter @ %i $= %this.collector.theList)) {
                %this.collector.uses = (%this.collector.uses + 1.0 @ %i);
            }
            if ((%obj.Item @ %i $= %this.collector.theList)) {
                %this.collector.uses = (%this.collector.uses + 1.0 @ %i);
            }
        }
        if ((%obj.getClassName() $= "ShapeBaseImageData")) {
            if ((%obj.rigidProjectile @ %i $= %this.collector.theList)) {
                %this.collector.uses = (%this.collector.uses + 1.0 @ %i);
            }
            if ((2 @ " " @ %obj.stateEmitter @ %i $= %this.collector.theList)) {
                %this.collector.uses = (%this.collector.uses + 1.0 @ %i);
            }
        }
        if ((%obj.getClassName() $= "ItemData")) {
            if ((%obj.image @ %i $= %this.collector.theList)) {
                %this.collector.uses = (%this.collector.uses + 1.0 @ %i);
            }
        }
        if ((%obj.getClassName() $= "MissionMarker")) {
            if ((%obj.sitSound @ %i $= %this.collector.theList)) {
                %this.collector.uses = (%this.collector.uses + 1.0 @ %i);
            }
        }
        if (isObject(%obj.getDataBlock()) && (%obj.getDataBlock().getName() @ %i $= %this.collector.theList)) {
            %this.collector.uses = (%this.collector.uses + 1.0 @ %i);
        }
        %i = (%i + 1.0);
    }
};
function SimGroup::PrintAllDebugNames(%this) {
    %v = SimGroupVisitor::construct("DebugStringVisitor");
    SimGroupVisitor::VisitSimgroup(%this, %v);
    %v.delete();
};
function DebugStringVisitor::visitObject(%this, %obj) {
    echo(%obj.getDebugString());
};
function testMissionIntegrity() {
    RunTestCase("TEST_MISSIONGROUPINTEGRITY", "WARNING: About that mission file you just loaded...");
};
