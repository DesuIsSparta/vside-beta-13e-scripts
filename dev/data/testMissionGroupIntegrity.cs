DeclareTestSuite("TestSuite_MissionGroup");
function TestSuite_MissionGroup::setup(%this) {
    %this.addTestCase("TEST_MISSIONGROUPINTEGRITY");
};
function TEST_MISSIONGROUPINTEGRITY::AddOkClass(%this, %okClassName) {
    okClass = %okClassName @ %this @ okClassCount @ %this;
    okClassCount = (%this + okClassCount);
    1.0;
};
function TEST_MISSIONGROUPINTEGRITY::Add_NO_CacheClass(%this, %okClassName) {
    ableToNotCacheClass = %okClassName @ %this @ ableToNotCacheClassCount @ %this;
    ableToNotCacheClassCount = (%this + ableToNotCacheClassCount);
    1.0;
};
function TEST_MISSIONGROUPINTEGRITY::AddInitiallyNotNetCacheableClass(%this, %classname) {
    InitiallyNotNetCacheableClass = %classname @ %this @ InitiallyNotNetCacheableClassCount @ %this;
    InitiallyNotNetCacheableClassCount = (%this + InitiallyNotNetCacheableClassCount);
    1.0;
};
function TEST_MISSIONGROUPINTEGRITY::AddObjectInstancesMustHaveUniqueNamesClass(%this, %classname) {
    instancesMustHaveUniqueNamesClass = %classname @ "\t" @ %this @ instancesMustHaveUniqueNamesClass @ %this;
};
function TEST_MISSIONGROUPINTEGRITY::objectInstanceMustHaveUniqueName(%this, %obj) {
    %classname = %obj.getClassName();
    %index = findField(instancesMustHaveUniqueNamesClass, %classname);
    %this;
    return (0.0 >= %index);
};
function TEST_MISSIONGROUPINTEGRITY::InitializeNPCNames(%this) {
    if (!(MissionInfo SPC skipNPCCheck $= "")) {
    }
    if ((MissionInfo == skipNPCCheck)) {
        log("general", "debug", "Skipping NPC name check.");
        NPCNameMap = 1.0 @ 0 @ %this;
        return;
    }
    ignoreCase = StringMap @ new ""() @ 1;
    0;
    NPCNameMap = %this;
    if (isObject()) {
        NPCNameMap.add();
    }
    %file = new ""();
    FileObject;
    if (%file.openForRead("dev/data/npc_usernames.txt")) {
        if (!(%file.isEOF())) {
            %npcName = %file.readLine();
            0;
            NPCNameMap.put(%npcName, "NPC");
        }
    }
    %file.delete();
    return !(%file.isEOF());
    %file.close();
    %file.delete();
};
function TEST_MISSIONGROUPINTEGRITY::setup(%this) {
    okClassCount = 0 @ %this;
    ableToNotCacheClassCount = 0 @ %this;
    InitiallyNotNetCacheableClassCount = 0 @ %this;
    instancesMustHaveUniqueNamesClassCount = 0 @ %this;
    %this.Add_NO_CacheClass("InteriorInstance");
    %this.Add_NO_CacheClass("ETSSeatMarker");
    %this.Add_NO_CacheClass("MissionMarker");
    %this.Add_NO_CacheClass("ZoneBox");
    %this.AddInitiallyNotNetCacheableClass("ETSSeatMarker");
    %this.AddInitiallyNotNetCacheableClass("ZoneBox");
    %this.AddInitiallyNotNetCacheableClass("TheoraRenderer");
    %this.AddInitiallyNotNetCacheableClass("FFMPEGRenderer");
    %this.AddInitiallyNotNetCacheableClass("Trigger");
    %this.AddInitiallyNotNetCacheableClass("PhysicalZone");
    %this.AddInitiallyNotNetCacheableClass("SpawnSphere");
    %this.AddInitiallyNotNetCacheableClass("Player");
    %this.AddInitiallyNotNetCacheableClass("AIPlayer");
    %this.AddObjectInstancesMustHaveUniqueNamesClass("Trigger");
    %this.AddOkClass("AdvertShape");
    %this.AddOkClass("AIPlayer");
    %this.AddOkClass("AntiPortal");
    %this.AddOkClass("ZoneBox");
    %this.AddOkClass("AudioEmitter");
    if (isFunction("Using_DShow")) {
        if (Using_DShow()) {
            %this.AddOkClass("DSRenderer");
        }
    }
    %this.AddOkClass("ETSSeatMarker");
    if (isFunction("Using_FFMPEG")) {
        if (Using_FFMPEG()) {
            %this.AddOkClass("FFMPEGRenderer");
        }
    }
    %this.AddOkClass("SlaveRenderer");
    if (isFunction("Using_DF")) {
        if (Using_DF()) {
            %this.AddOkClass("DFTextureAdvert");
        }
    }
    %this.AddOkClass("InteriorInstance");
    %this.AddOkClass("TerrainBlock");
    %this.AddOkClass("Lightning");
    %this.AddOkClass("MissionArea");
    %this.AddOkClass("MissionMarker");
    %this.AddOkClass("EtsDoor");
    %this.AddOkClass("ParticleEmitterNode");
    %this.AddOkClass("Path");
    %this.AddOkClass("PhysicalZone");
    %this.AddOkClass("SimGroup");
    %this.AddOkClass("SimSpace");
    %this.AddOkClass("Sky");
    %this.AddOkClass("SpawnSphere");
    %this.AddOkClass("StaticShape");
    %this.AddOkClass("Sun");
    %this.AddOkClass("TSStatic");
    %this.AddOkClass("TSDynamic");
    if (isFunction("Using_Theora")) {
        if (Using_Theora()) {
            %this.AddOkClass("TheoraRenderer");
        }
    }
    %this.AddOkClass("Trigger");
    %this.AddOkClass("WaterBlock");
    %this.AddOkClass("WayPoint");
    %this.AddOkClass("fxFoliageReplicator");
    %this.AddOkClass("fxLight");
    %this.AddOkClass("fxShapeReplicator");
    %this.AddOkClass("fxSpectrumAnalyzer");
    %this.AddOkClass("fxSunLight");
    %this.AddOkClass("sgDecalProjector");
    %this.AddOkClass("sgMissionLightingFilter");
    %this.AddOkClass("sgUniversalStaticLight");
    %this.AddOkClass("volumeLight");
    %this.AddOkClass("Marker");
    %this.AddOkClass("BlockGameBase");
    %this.AddOkClass("BlockGameTheGrind");
    %this.AddOkClass("BlockGameMateriel");
    %this.AddOkClass("HappyFunSquiggleBall");
    %this.AddOkClass("ImageFrameBase");
    %this.AddOkClass("TSText");
    %this.InitializeNPCNames();
};
$MAYBE_BAD_MODEL_UNIT_FLAG = 0;
function TEST_MISSIONGROUPINTEGRITY::runTest(%this) {
    if ($MAYBE_BAD_MODEL_UNIT_FLAG) {
        %this.assert(0, "AINT NO MODEL UNIT HIGH ENOUUGH, NO MORE CRACK PIPE FOR YOU!, FIX ME!!!, filename does not contain the string modelunit, likely not a real model unit, should probably be pointing to different _generated.cs file");
    }
    %this.RecursivelyCheckForThingsThatDontBelong("invalidGroup");
    %this.CheckBuildingTransitionSetup();
    %this.CheckPrivateSpaceSetup();
    %this.CheckDatablockSetup();
    %this.CheckUniqueObjectNames();
    %this.CheckPaperDollSKUs();
};
function TEST_MISSIONGROUPINTEGRITY::TearDown(%this) {
    if (isObject(NPCNameMap)) {
        NPCNameMap.delete();
        NPCNameMap = %this @ 0 @ %this;
        %this;
    }
};
function CountObjectsInMissionWithName(%name) {
    %v = SimGroupVisitor::construct("NameCounterVisitor");
    skipSimGroups = 0 @ %v;
    count = 0 @ %v;
    nameToCount = %name @ %v;
    SimGroupVisitor::VisitSimgroup(%v);
    %count = count;
    %v;
    %v.delete();
    return %count;
};
function NameCounterVisitor::visitObject(%this, %obj) {
    if ((%this $= nameToCount)) {
        count = (%this + count);
        1.0;
    }
};
function TEST_MISSIONGROUPINTEGRITY::assertCount(%this, %name, %expected_count, %message) {
    if ((0.0 > %expected_count)) {
        %this.assert(isObject(%name), %message);
    }
    %count = CountObjectsInMissionWithName(%name);
    %this.assert((%expected_count == %count), "expected there to be" @ " " @ %expected_count @ " " @ "of" @ " " @ %name @ " " @ "but found" @ " " @ %count @ " " @ ".  maybe you accidentally named the other ones this? or you accidentally copied and pasted it?");
};
function TEST_MISSIONGROUPINTEGRITY::assertCountAtMost(%this, %name, %atMost_count, %message) {
    if ((0.0 > %atMost_count)) {
        %this.assert(isObject(%name), %message);
    }
    %count = CountObjectsInMissionWithName(%name);
    %this.assert((%atMost_count <= %count), "expected there to be at most" @ " " @ %atMost_count @ " " @ "of" @ " " @ %name @ " " @ "but found" @ " " @ %count @ " " @ ".  maybe you accidentally named the other ones this? or you accidentally copied and pasted it?");
};
function TEST_MISSIONGROUPINTEGRITY::CheckBuildingTransitionSetup(%this) {
    if (isObject()) {
        %this.assertCount("BuildingDefinitions", 1, "there should only be a single BuildingDefinitions simgroup");
        %count = getCount();
        BuildingDefinitions;
        %i = 0;
        BuildingDefinitions;
        if ((%count < %i)) {
            %obj = %i.getObject();
            BuildingDefinitions;
            if (isObject(%obj)) {
                %name = buildingName;
                %obj;
                %vurl = Buildings::getReturnVURL(%name);
                %parsedVURLobject = vurlGetParsedVurl(%vurl);
                %returnName = targetDest;
                %parsedVURLobject;
                %this.assert(isObject(%returnName), "Building(" @ " " @ %name @ " " @ ") return spawn \"" @ %returnName @ "\" is not an object, you should make a simgroup with that name and place a spawn sphere in it for this building");
                %parsedVURLobject.delete();
            }
            %i = (1.0 + %i);
        }
    }
    if (isObject()) {
        %this.assertCountAtMost("NPCGroup", 1, "there should only be at most a single NPCGroup SimGroup");
    }
};
function TEST_MISSIONGROUPINTEGRITY::CheckPrivateSpaceSetup(%this) {
    %this.assertCount("MissionInfo", 1, "There is no MissionInfo scriptobject, This is used to determine various things about the missions and should exist in this one too");
    if ((MissionInfo SPC mode $= "PrivateSpaceDesign")) {
        if (isObject()) {
            %this.assertCount("PRIVATESPACE_OFFSETMARKER", 1, "private space missions must have only ONE PRIVATESPACE_OFFSETMARKER, this is optional and if found it will be used as the root position for the private space instead of the customizable area");
        }
        %this.assertCount("PRIVATESPACE_GROUP", 1, "private space missions must have a PRIVATESPACE_GROUP simgroup, everything you put in this simgroup will be part of the private space that is replicated in the grid server");
        %this.assertCount("PRIVATESPACE_AREA", 1, "private space missions must have a PRIVATESPACE_AREA customizable space trigger, this defines the bounds of the customizable space, and will also control where they can move the furniture, and where the music plays.");
        %this.assertCount("PRIVATESPACE_INTERIOR", 1, "private space missions must have a PRIVATESPACE_INTERIOR dif interior defined, this is the building that can have its surfaces customized.");
        %this.assertCount("PRIVATESPACE_ENTRYSPAWN", 1, "private space missions must have a PRIVATESPACE_ENTRYSPAWN spawn sphere, this is where the player spawns when they enter the space, should go close to the door");
        %this.assertCount("PRIVATESPACE_EXITTRANSITION", 1, "private space missions must have a PRIVATESPACE_EXITTRANSITION, this is the teleportal that goes back to city you came from,should go close to the door");
        %this.assertCount("PRIVATESPACE_ZONEBOX", 1, "private space missions must have a PRIVATESPACE_ZONEBOX, this is the zonebox object that defines scope for this apartment, it should cover your entire apartment and anything in it that can be scoped in and out. this will handle it instead of the interior.  There should only be one, and it should cover your entire apartment so that all furniture, players etc will be inside it for this apartment");
        if (isObject()) {
        }
        if (isObject()) {
            %worldBox = getWorldBox();
            PRIVATESPACE_AREA;
            %minP = getWords(%worldBox, 0, 2);
            PRIVATESPACE_ZONEBOX;
            %maxP = getWords(%worldBox, 3, 5);
            PRIVATESPACE_AREA;
            %areaminx = getWord(%minP, 0);
            PRIVATESPACE_OFFSETMARKER;
            %areaminy = getWord(%minP, 1);
            %areaminz = getWord(%minP, 2);
            %areamaxx = getWord(%maxP, 0);
            %areamaxy = getWord(%maxP, 1);
            %areamaxz = getWord(%maxP, 2);
            %worldBox = getWorldBox();
            PRIVATESPACE_ZONEBOX;
            %minP = getWords(%worldBox, 0, 2);
            %maxP = getWords(%worldBox, 3, 5);
            %zoneminx = getWord(%minP, 0);
            %zoneminy = getWord(%minP, 1);
            %zoneminz = getWord(%minP, 2);
            %zonemaxx = getWord(%maxP, 0);
            %zonemaxy = getWord(%maxP, 1);
            %zonemaxz = getWord(%maxP, 2);
            if ((%areaminx <= %zoneminx)) {
            }
            if ((%areaminy <= %zoneminy)) {
            }
            if ((%areaminz <= %zoneminz)) {
            }
            if ((%areamaxx >= %zonemaxx)) {
            }
            if ((%areamaxy >= %zonemaxy)) {
            }
            %contained = (%areamaxz >= %zonemaxz);
            %this.assert(%contained, " PRIVATESPACE_AREA  must be entirely inside of PRIVATESPACE_ZONEBOX, it looks like the area is outside in this mission, make sure the zonebox surrounds it completely. thanks!");
        }
    }
    if ((MissionInfo SPC mode $= "PrivateSpaceGrid")) {
        %this.assertDifferentString(modelID, "", "For privatespace grid servers, a modelID must be specified in MissionInfo, this is the type of floorplan supported by this server");
        %this.assertDifferentString(building, "", "For privatespace grid servers, a building must be specified in MissionInfo, this is the building that connects to this grid server");
        %this.assertDifferentString(spacePrefix, "", "For privatespace grid servers, a spacePrefix must be specified in MissionInfo, this is the prefix that will be used to name each space");
    }
};
function TEST_MISSIONGROUPINTEGRITY::CheckDatablockSetup(%this) {
    // unhandled opcode 1645 at 0x00000B2E
    if (%this.assert(isObject(%group), "no DataBlockGroup!")) {
        return;
    }
    %n = (1.0 - %group.getCount());
    if ((0.0 >= %n)) {
        %obj = %group.getObject(%n);
        if (%this.assert(isObject(%obj), "non-object in DataBlockGroup!")) {
        }
        if (%obj.hasMethod("checkIntegrity")) {
            %obj.checkIntegrity(%this);
        }
        %n = (1.0 - %n);
    }
};
function TEST_MISSIONGROUPINTEGRITY::CheckUniqueObjectNames(%this) {
    // unhandled opcode 1645 at 0x00000BC9
    %n = MissionGroup;
    if (%this.assert(isObject(%group), "no MissionGroup!")) {
        return;
    }
    %nameMap = safeNewScriptObject("StringMap", "", 0);
    %this._checkUniqueObjectNames_Recursive(%group, %nameMap);
    %nameMap.delete();
};
function TEST_MISSIONGROUPINTEGRITY::_checkUniqueObjectNames_Recursive(%this, %obj, %nameMap) {
    %objName = %obj.getName();
    if (!(%objName $= "")) {
        if (%nameMap.hasKey(%objName)) {
            %otherObj = %nameMap.get(%objName);
            %this.assert(!(%this.objectInstanceMustHaveUniqueName(%obj)), "object \"" @ getDebugString(%obj) @ "\" must have a unique name.");
            %this.assert(!(%this.objectInstanceMustHaveUniqueName(%otherObj)), "object \"" @ getDebugString(%otherObj) @ "\" must have a unique name.");
        }
        %nameMap.put(%objName, %obj);
    }
    if (%obj.isClassSimSet()) {
        %num = %obj.getCount();
        %n = 0;
        if ((%num < %n)) {
            %child = %obj.getObject(%n);
            %this._checkUniqueObjectNames_Recursive(%child, %nameMap);
            %n = (1.0 + %n);
        }
    }
};
function TEST_MISSIONGROUPINTEGRITY::CheckPaperDollSKUs(%this) {
    paperDoll_InitPermutations();
};
function MissionMarkerData::checkIntegrity(%this, %testCase) {
    if (!(%testCase.isProtectedSitAnimException(sitIdle))) {
        %isProtected = sitIdle.hasKey();
        %this;
        %testCase.assert(%isProtected, %this @ ProtectedAnimsDict @ "seat marker datablock \"" @ %this.getName() @ "\" has a non-protected idle anim, \"" @ %this @ sitIdle @ "\". You may want to add it to initializeProtectedAnims().");
    }
};
function TEST_MISSIONGROUPINTEGRITY::IsAbleToNotCache(%this, %obj) {
    %classname = %obj.getClassName();
    %ableToNotCache = 0;
    %i = 0;
    if ((ableToNotCacheClassCount < %i)) {
    }
    if ((0.0 == %ableToNotCache)) {
        if ((%this @ %i @ %this SPC ableToNotCacheClass $= %classname)) {
            %ableToNotCache = 1;
        }
        %i = (1.0 + %i);
        if ((ableToNotCacheClassCount < %i)) {
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
    if ((InitiallyNotNetCacheableClassCount < %i)) {
    }
    if ((1.0 == %ret)) {
        if ((%this @ %i @ %this SPC InitiallyNotNetCacheableClass $= %classname)) {
            %ret = 0;
        }
        %i = (1.0 + %i);
        if ((InitiallyNotNetCacheableClassCount < %i)) {
        }
    }
    return %ret;
};
function TEST_MISSIONGROUPINTEGRITY::RecursivelyCheckForThingsThatDontBelong(%this, %obj, %parentGroupName) {
    %this.assert(isObject(%obj), %parentGroupName @ " " @ ", the object " @ %obj @ "is not an object");
    if (!(isObject(%obj))) {
        return;
    }
    if (%obj.isClassSimSet()) {
        %num = %obj.getCount();
        %n = 0;
        if ((%num < %n)) {
            %this.RecursivelyCheckForThingsThatDontBelong(%obj.getObject(%n), %obj.getName());
            %n = (1.0 + %n);
        }
    }
    %classname = %obj.getClassName();
    (%num < %n);
    %belongs = 0;
    %i = 0;
    if ((okClassCount < %i)) {
        if ((%this @ %i @ %this SPC okClass $= %classname)) {
            %belongs = 1;
        }
        %i = (1.0 + %i);
    }
    %actionNeeded = "This object does not belong and should probably be deleted";
    (okClassCount < %i);
    %ableToNotCache = %this.IsAbleToNotCache(%obj);
    %this;
    if ((0.0 == %ableToNotCache)) {
        %exceptions = "SimGroup SimSet SimSpace ScriptObject";
        %wordLoc = findWord(%exceptions, %classname);
        if ((0.0 < %wordLoc)) {
            %this.assert((%obj == isNetCacheable), "this object is a type that should be able to cache, you should set isNetCacheable to 1" @ " " @ getDebugString(%obj));
        }
    }
    if (%obj.isClassSimSpace()) {
        if (!(1.0 SPC getSubStr(%obj.getName(), 0, 9) $= "SimSpace_")) {
            %belongs = 0;
            %actionNeeded = "SimSpaces should be named starting with \"SimSpace_\"." @ " " @ %obj.getName() @ " " @ "is breakin' the law!";
        }
        if (!(%obj.hasSpaceDef())) {
            %belongs = 0;
            %actionNeeded = "SimSpace" @ " " @ %obj.getName() @ " " @ "has no associated SpaceDef.";
        }
    }
    if (%obj.isClassAIPlayer()) {
        %this.assert((NPCGroup > %obj.getObjectIndex()), "AIPlayer is not in NPCGroup:" @ " " @ getDebugString(%obj));
        if (isObject(NPCNameMap)) {
        }
        if ((%this == NPCNameMap.findKey(%obj.getName()))) {
            %belongs = 0;
            -(1.0);
            %actionNeeded = "NPC" @ " " @ %obj.getName() @ " " @ "should be listed in npc_usernames.txt.";
            %this;
        }
    }
    if ((-(1.0) SPC %classname $= "MissionMarker")) {
        %dbName = %obj.getDataBlock().getName();
        %seatMarkerFound = strstr(%dbName, "SeatMarker");
        if ((-(1.0) != %seatMarkerFound)) {
            %belongs = 0;
            %actionNeeded = "This is an old style seat marker and shuold be converted to an ETSSeatMarker";
        }
        if (!(%this.isProtectedSitAnimException(sitIdle))) {
            %isProtected = sitIdle.hasKey();
            %obj;
            %this.assert(%isProtected, %obj @ ProtectedAnimsDict @ "seat marker \"" @ %obj.getName() @ "\" has a non-protected idle anim, \"" @ %obj @ sitIdle @ "\". You may want to add it to initializeProtectedAnims().");
        }
    }
    if ((%classname $= "ETSSeatMarker")) {
        if ((%obj SPC listeningStation $= 0)) {
            %belongs = 0;
            %actionNeeded = "ETSSeatMarkers should not have listening station of 0, listening station should be the name of an object or an empty string";
        }
        %this.assert((%obj == isNetCacheable), "seat markers cannot be cached, you should set isNetCacheable to 0 for the seat marker:" @ " " @ getDebugString(%obj) @ " " @ "in the group" @ " " @ %parentGroupName);
        if (!(%this.isProtectedSitAnimException(sitIdle))) {
            %isProtected = sitIdle.hasKey();
            %obj;
            %this.assert(%isProtected, 0.0 @ %obj @ ProtectedAnimsDict @ "seat marker \"" @ %obj.getName() @ "\" has a non-protected idle anim, \"" @ %obj @ sitIdle @ "\". You may want to add it to initializeProtectedAnims().");
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
        if ((%obj SPC class $= "SalonChairEngageScriptObject")) {
            %belongs = 1;
        }
    }
    if ((%classname $= "Trigger")) {
        %dbName = %obj.getDataBlock().getName();
        if ((%dbName $= "SeatingArea")) {
            if ((%obj SPC autosit $= "")) {
                %belongs = 0;
                %actionNeeded = "SeatingArea triggers are only needed if you are using autosit, otherwise it should be deleted";
            }
        }
        if ((%dbName $= "DoorTrigger")) {
            %myDoor = %obj.findMyDoor();
            %this.assert(isObject(%myDoor), %parentGroupName @ " " @ ", the object " @ %myDoor @ "is not an object, this is the door referenced by" @ " " @ getDebugString(%obj));
            %doorsDataBlock = %myDoor.getDataBlock();
            %this.assert(!(%doorsDataBlock $= ""), %parentGroupName @ " " @ ", the object " @ %myDoor @ " is not a datablock based door, you should make the datablock first and place that in the world, not a Static, look in Shapes->Doors for your datablock name, this is the door referenced by" @ " " @ getDebugString(%obj));
        }
    }
    if ((MissionInfo SPC mode $= "PrivateSpaceDesign")) {
        if ((%classname $= "TSStatic")) {
            %belongs = 0;
            %actionNeeded = "Static Shapes should not be used in individual Private spaces,  instead you should use Dynamic Shapes,   these are TSDynamic instead of TSStatic";
        }
        if ((%classname $= "InteriorInstance")) {
            %isManagingZones = managezones;
            %obj;
            %this.assert(!(%isManagingZones), %parentGroupName @ " " @ ", the object" @ " " @ %obj.getDebugString() @ " " @ " - In a private space, interiors should not be managing zones, set \"managezones\" to zero for this interior instance, and make sure you have a ZoneBox surrounding the entire apartment, the ZoneBox will serve as the zone manager");
        }
        if ((%obj.getName() $= "PRIVATESPACE_AREA")) {
            %dbName = %obj.getDataBlock().getName();
            %this.assertSameString(%dbName, "CustomizableSpaceTriggerData", "the PRIVATESPACE_AREA object should be a CustomizableSpaceTriggerData trigger");
        }
        if ((%obj.getName() $= "PRIVATESPACE_INTERIOR")) {
            %this.assertSameString(%classname, "InteriorInstance", "the PRIVATESPACE_INTERIOR object should be a InteriorInstance object");
            %this.assert((%obj == isNetCacheable), "The PRIVATESPACE_INTERIOR will have the textures on it changed at runtime for this reason, it should have isNetCacheable set to 0, make that change to fix this problem");
        }
    }
    if (!(%obj SPC dataBlock $= "")) {
        %this.assertDifferentString(dataBlock, %obj.getName(), "You cannot have an object with the same name as a datablock, It's a good idea to name your datablocks something like blah_DB so you don't accidentally call your object the same name");
    }
    %openSpace = "                                       action to take:";
    %obj;
    %message = "in" @ " " @ %parentGroupName @ " " @ ": object:" @ " " @ %obj @ " " @ "of class:" @ " " @ %classname @ " " @ ", with name:" @ " " @ %obj.getName() @ "\n" @ %openSpace @ " " @ %actionNeeded;
    0.0;
    %this.assert((1.0 == %belongs), %message);
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
        if ((%num < %n)) {
            RecursivelyFixOldStyleSeatingAreaProblems(%obj.getObject(%n));
            %n = (1.0 + %n);
        }
        return (%num < %n);
    }
    %classname = %obj.getClassName();
    if ((%classname $= "ETSSeatMarker")) {
        if ((%obj SPC listeningStation $= 0)) {
            listeningStation = "" @ %obj;
            echo("cleared listening station that was 0 for " @ " " @ %obj @ " " @ "of class:" @ " " @ %classname @ " " @ ", with name:" @ " " @ %obj.getName());
        }
    }
    if ((%classname $= "Trigger")) {
        %dbName = %obj.getDataBlock().getName();
        if ((%dbName $= "SeatingArea")) {
            if ((%obj SPC autosit $= "")) {
                $OLDSEATAREA_KILLER_COUNT[$OLDSEATAREA_KILLER @ $OLDSEATAREA_KILLER_COUNT] = %obj.getId();
                $OLDSEATAREA_KILLER_COUNT = (1.0 + $OLDSEATAREA_KILLER_COUNT);
            }
        }
    }
};
function FixOldStyleSeatingAreaProblems() {
    echo("getting rid of listeningstations and no longer needed seatingarea triggers ----------------------");
    $OLDSEATAREA_KILLER_COUNT = 0;
    RecursivelyFixOldStyleSeatingAreaProblems();
    %i = 0;
    MissionGroup;
    if (($OLDSEATAREA_KILLER_COUNT < %i)) {
        %obj = %i[$OLDSEATAREA_KILLER @ %i];
        echo("deleteing no longer needed seating area " @ " " @ %obj @ " " @ "of class:" @ " " @ %obj.getClassName() @ " " @ ", with name:" @ " " @ %obj.getName());
        %obj.delete();
        $OLDSEATAREA_KILLER_COUNT[$OLDSEATAREA_KILLER @ $OLDSEATAREA_KILLER_COUNT] = "";
        %i = (1.0 + %i);
    }
    $OLDSEATAREA_KILLER_COUNT = 0;
    ($OLDSEATAREA_KILLER_COUNT < %i);
    echo("done----------------------");
};
function Utility::ListDataBlocksNotUsed() {
    %v = SimGroupVisitor::construct("UtilityCollectNameVisitor");
    skipScriptObjects = 1 @ %v;
    count = 0 @ %v;
    SimGroupVisitor::VisitSimgroup("DataBlockGroup", %v);
    %v2 = SimGroupVisitor::construct("UtilityDBInUseVisitor");
    skipScriptObjects = 1 @ %v2;
    collector = %v @ %v2;
    SimGroupVisitor::VisitSimgroup("RootGroup", %v2);
    error("Listing Datablocks not used -------------------------");
    error("note:  these are only the ones not currently used based on the active objects, they may be used later or dynamically, but this should be a good starting point to check");
    echo("");
    %i = 0;
    if ((count < %i)) {
        if (!(uses)) {
            error(theList @ " " @ "not used");
        }
        %i = (1.0 + %i);
        %v @ %i @ %v @ %i @ %v;
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
    theList = %obj.getName() @ %this @ count @ %this;
    uses = 0 @ %this @ count @ %this;
    count = (%this + count);
    1.0;
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
    if ((count < %i)) {
        if ((collector SPC %obj.getClassName() $= "AudioProfile")) {
            if ((%this @ collector $= theList)) {
                uses = (%this @ collector + uses);
                1.0 @ %i;
            }
        }
        if ((%obj SPC description.getName() @ %i SPC %obj.getClassName() $= "ParticleEmitterData")) {
            if ((%this @ collector $= theList)) {
                uses = (%this @ collector + uses);
                1.0 @ %i;
            }
        }
        if ((%obj SPC particles @ %i SPC %obj.getClassName() $= "PlayerData")) {
            if ((%this @ collector $= theList)) {
                uses = (%this @ collector + uses);
                1.0 @ %i;
            }
            if ((%this @ collector $= theList)) {
                uses = (%this @ collector + uses);
                1.0 @ %i;
            }
            if ((%this @ collector $= theList)) {
                uses = (%this @ collector + uses);
                1.0 @ %i;
            }
            if ((%this @ collector $= theList)) {
                uses = (%this @ collector + uses);
                1.0 @ %i;
            }
            if ((%this @ collector $= theList)) {
                uses = (%this @ collector + uses);
                1.0 @ %i;
            }
            if ((%this @ collector $= theList)) {
                uses = (%this @ collector + uses);
                1.0 @ %i;
            }
            if ((%this @ collector $= theList)) {
                uses = (%this @ collector + uses);
                1.0 @ %i;
            }
            if ((%this @ collector $= theList)) {
                uses = (%this @ collector + uses);
                1.0 @ %i;
            }
            if ((%this @ collector $= theList)) {
                uses = (%this @ collector + uses);
                1.0 @ %i;
            }
            if ((%this @ collector $= theList)) {
                uses = (%this @ collector + uses);
                1.0 @ %i;
            }
            if ((%this @ collector $= theList)) {
                uses = (%this @ collector + uses);
                1.0 @ %i;
            }
            if ((%this @ collector $= theList)) {
                uses = (%this @ collector + uses);
                1.0 @ %i;
            }
            if ((%this @ collector $= theList)) {
                uses = (%this @ collector + uses);
                1.0 @ %i;
            }
            if ((%this @ collector $= theList)) {
                uses = (%this @ collector + uses);
                1.0 @ %i;
            }
            if ((%this @ collector $= theList)) {
                uses = (%this @ collector + uses);
                1.0 @ %i;
            }
            if ((%this @ collector $= theList)) {
                uses = (%this @ collector + uses);
                1.0 @ %i;
            }
        }
        if ((%obj SPC HalloweenDecalDataR @ %i SPC %obj.getClassName() $= "SplashData")) {
        }
        if ((%obj SPC HalloweenDecalDataL @ %i SPC %obj.getClassName() $= "ExplosionData")) {
            if ((%this @ collector $= theList)) {
                uses = (%this @ collector + uses);
                1.0 @ %i;
            }
            if ((%this @ collector $= theList)) {
                uses = (%this @ collector + uses);
                1.0 @ %i;
            }
        }
        if ((%obj SPC HalloweenfootPuffEmitter @ %i @ 0 @ %obj SPC emitter @ %i @ 1 @ %obj SPC emitter @ %i SPC %obj.getClassName() $= "RigidShapeData")) {
            if ((%this @ collector $= theList)) {
                uses = (%this @ collector + uses);
                1.0 @ %i;
            }
            if ((%this @ collector $= theList)) {
                uses = (%this @ collector + uses);
                1.0 @ %i;
            }
        }
        if ((%obj SPC Item @ %i SPC %obj.getClassName() $= "ShapeBaseImageData")) {
            if ((%this @ collector $= theList)) {
                uses = (%this @ collector + uses);
                1.0 @ %i;
            }
            if ((%this @ collector $= theList)) {
                uses = (%this @ collector + uses);
                1.0 @ %i;
            }
        }
        if ((%obj SPC rigidProjectile @ %i @ 2 @ %obj SPC stateEmitter @ %i SPC %obj.getClassName() $= "ItemData")) {
            if ((%this @ collector $= theList)) {
                uses = (%this @ collector + uses);
                1.0 @ %i;
            }
        }
        if ((%obj SPC image @ %i SPC %obj.getClassName() $= "MissionMarker")) {
            if ((%this @ collector $= theList)) {
                uses = (%this @ collector + uses);
                1.0 @ %i;
            }
        }
        if (isObject(%obj.getDataBlock())) {
            if ((%this @ collector $= theList)) {
                uses = (%this @ collector + uses);
                1.0 @ %i;
            }
        }
        %i = (1.0 + %i);
        %obj SPC sitSound @ %i SPC %obj.getDataBlock().getName() @ %i;
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
