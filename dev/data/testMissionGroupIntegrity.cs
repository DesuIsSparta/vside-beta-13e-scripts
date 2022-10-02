DeclareTestSuite("TestSuite_MissionGroup");
function TestSuite_MissionGroup::setup(%this)
{
    %this.addTestCase("TEST_MISSIONGROUPINTEGRITY");
    return ;
}
function TEST_MISSIONGROUPINTEGRITY::AddOkClass(%this, %okClassName)
{
    %this.okClass[%this.okClassCount] = %okClassName;
    %this.okClassCount = %this.okClassCount + 1;
    return ;
}
function TEST_MISSIONGROUPINTEGRITY::Add_NO_CacheClass(%this, %okClassName)
{
    %this.ableToNotCacheClass[%this.ableToNotCacheClassCount] = %okClassName;
    %this.ableToNotCacheClassCount = %this.ableToNotCacheClassCount + 1;
    return ;
}
function TEST_MISSIONGROUPINTEGRITY::AddInitiallyNotNetCacheableClass(%this, %classname)
{
    %this.InitiallyNotNetCacheableClass[%this.InitiallyNotNetCacheableClassCount] = %classname;
    %this.InitiallyNotNetCacheableClassCount = %this.InitiallyNotNetCacheableClassCount + 1;
    return ;
}
function TEST_MISSIONGROUPINTEGRITY::AddObjectInstancesMustHaveUniqueNamesClass(%this, %classname)
{
    %this.instancesMustHaveUniqueNamesClass = %classname TAB %this.instancesMustHaveUniqueNamesClass;
    return ;
}
function TEST_MISSIONGROUPINTEGRITY::objectInstanceMustHaveUniqueName(%this, %obj)
{
    %classname = %obj.getClassName();
    %index = findField(%this.instancesMustHaveUniqueNamesClass, %classname);
    return %index >= 0;
}
function TEST_MISSIONGROUPINTEGRITY::InitializeNPCNames(%this)
{
    if (!((MissionInfo.skipNPCCheck $= "")) && (MissionInfo.skipNPCCheck == 1))
    {
        log("general", "debug", "Skipping NPC name check.");
        %this.NPCNameMap = 0;
        return ;
    }
    %this.NPCNameMap = new StringMap();
    if (isObject(MissionCleanup))
    {
        MissionCleanup.add(%this.NPCNameMap);
    }
    %file = new FileObject();
    if (%file.openForRead("dev/data/npc_usernames.txt"))
    {
        while (!%file.isEOF())
        {
            %npcName = %file.readLine();
            %this.NPCNameMap.put(%npcName, "NPC");
        }
    }
    else
    {
        %file.delete();
        return ;
    }
    %file.close();
    %file.delete();
    return ;
}
function TEST_MISSIONGROUPINTEGRITY::setup(%this)
{
    %this.okClassCount = 0;
    %this.ableToNotCacheClassCount = 0;
    %this.InitiallyNotNetCacheableClassCount = 0;
    %this.instancesMustHaveUniqueNamesClassCount = 0;
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
    if (isFunction("Using_DShow"))
    {
        if (Using_DShow())
        {
            %this.AddOkClass("DSRenderer");
        }
    }
    %this.AddOkClass("ETSSeatMarker");
    if (isFunction("Using_FFMPEG"))
    {
        if (Using_FFMPEG())
        {
            %this.AddOkClass("FFMPEGRenderer");
        }
    }
    %this.AddOkClass("SlaveRenderer");
    if (isFunction("Using_DF"))
    {
        if (Using_DF())
        {
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
    if (isFunction("Using_Theora"))
    {
        if (Using_Theora())
        {
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
    return ;
}
$MAYBE_BAD_MODEL_UNIT_FLAG = 0;
function TEST_MISSIONGROUPINTEGRITY::runTest(%this)
{
    if ($MAYBE_BAD_MODEL_UNIT_FLAG)
    {
        %this.assert(0, "AINT NO MODEL UNIT HIGH ENOUUGH, NO MORE CRACK PIPE FOR YOU!, FIX ME!!!, filename does not contain the string modelunit, likely not a real model unit, should probably be pointing to different _generated.cs file");
    }
    %this.RecursivelyCheckForThingsThatDontBelong(MissionGroup, "invalidGroup");
    %this.CheckBuildingTransitionSetup();
    %this.CheckPrivateSpaceSetup();
    %this.CheckDatablockSetup();
    %this.CheckUniqueObjectNames();
    %this.CheckPaperDollSKUs();
    return ;
}
function TEST_MISSIONGROUPINTEGRITY::TearDown(%this)
{
    if (isObject(%this.NPCNameMap))
    {
        %this.NPCNameMap.delete();
        %this.NPCNameMap = 0;
    }
    return ;
}
function CountObjectsInMissionWithName(%name)
{
    %v = SimGroupVisitor::construct("NameCounterVisitor");
    %v.skipSimGroups = 0;
    %v.count = 0;
    %v.nameToCount = %name;
    SimGroupVisitor::VisitSimgroup(MissionGroup, %v);
    %count = %v.count;
    %v.delete();
    return %count;
}
function NameCounterVisitor::visitObject(%this, %obj)
{
    if (%obj.getName() $= %this.nameToCount)
    {
        %this.count = %this.count + 1;
    }
    return ;
}
function TEST_MISSIONGROUPINTEGRITY::assertCount(%this, %name, %expected_count, %message)
{
    if (%expected_count > 0)
    {
        %this.assert(isObject(%name), %message);
    }
    %count = CountObjectsInMissionWithName(%name);
    %this.assert(%count == %expected_count, "expected there to be" SPC %expected_count SPC "of" SPC %name SPC "but found" SPC %count SPC ".  maybe you accidentally named the other ones this? or you accidentally copied and pasted it?");
    return ;
}
function TEST_MISSIONGROUPINTEGRITY::assertCountAtMost(%this, %name, %atMost_count, %message)
{
    if (%atMost_count > 0)
    {
        %this.assert(isObject(%name), %message);
    }
    %count = CountObjectsInMissionWithName(%name);
    %this.assert(%count <= %atMost_count, "expected there to be at most" SPC %atMost_count SPC "of" SPC %name SPC "but found" SPC %count SPC ".  maybe you accidentally named the other ones this? or you accidentally copied and pasted it?");
    return ;
}
function TEST_MISSIONGROUPINTEGRITY::CheckBuildingTransitionSetup(%this)
{
    if (isObject(BuildingDefinitions))
    {
        %this.assertCount("BuildingDefinitions", 1, "there should only be a single BuildingDefinitions simgroup");
        %count = BuildingDefinitions.getCount();
        %i = 0;
        while (%i < %count)
        {
            %obj = BuildingDefinitions.getObject(%i);
            if (isObject(%obj))
            {
                %name = %obj.buildingName;
                %vurl = Buildings::getReturnVURL(%name);
                %parsedVURLobject = vurlGetParsedVurl(%vurl);
                %returnName = %parsedVURLobject.targetDest;
                %this.assert(isObject(%returnName), "Building(" SPC %name SPC ") return spawn \"" @ %returnName @ "\" is not an object, you should make a simgroup with that name and place a spawn sphere in it for this building");
                %parsedVURLobject.delete();
            }
            %i = %i + 1;
        }
    }
    if (isObject(NPCGroup))
    {
        %this.assertCountAtMost("NPCGroup", 1, "there should only be at most a single NPCGroup SimGroup");
    }
    return ;
}
function TEST_MISSIONGROUPINTEGRITY::CheckPrivateSpaceSetup(%this)
{
    %this.assertCount("MissionInfo", 1, "There is no MissionInfo scriptobject, This is used to determine various things about the missions and should exist in this one too");
    if (MissionInfo.mode $= "PrivateSpaceDesign")
    {
        if (isObject(PRIVATESPACE_OFFSETMARKER))
        {
            %this.assertCount("PRIVATESPACE_OFFSETMARKER", 1, "private space missions must have only ONE PRIVATESPACE_OFFSETMARKER, this is optional and if found it will be used as the root position for the private space instead of the customizable area");
        }
        %this.assertCount("PRIVATESPACE_GROUP", 1, "private space missions must have a PRIVATESPACE_GROUP simgroup, everything you put in this simgroup will be part of the private space that is replicated in the grid server");
        %this.assertCount("PRIVATESPACE_AREA", 1, "private space missions must have a PRIVATESPACE_AREA customizable space trigger, this defines the bounds of the customizable space, and will also control where they can move the furniture, and where the music plays.");
        %this.assertCount("PRIVATESPACE_INTERIOR", 1, "private space missions must have a PRIVATESPACE_INTERIOR dif interior defined, this is the building that can have its surfaces customized.");
        %this.assertCount("PRIVATESPACE_ENTRYSPAWN", 1, "private space missions must have a PRIVATESPACE_ENTRYSPAWN spawn sphere, this is where the player spawns when they enter the space, should go close to the door");
        %this.assertCount("PRIVATESPACE_EXITTRANSITION", 1, "private space missions must have a PRIVATESPACE_EXITTRANSITION, this is the teleportal that goes back to city you came from,should go close to the door");
        %this.assertCount("PRIVATESPACE_ZONEBOX", 1, "private space missions must have a PRIVATESPACE_ZONEBOX, this is the zonebox object that defines scope for this apartment, it should cover your entire apartment and anything in it that can be scoped in and out. this will handle it instead of the interior.  There should only be one, and it should cover your entire apartment so that all furniture, players etc will be inside it for this apartment");
        if (isObject(PRIVATESPACE_AREA) && isObject(PRIVATESPACE_ZONEBOX))
        {
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
            %contained = (((((%zoneminx <= %areaminx) && (%zoneminy <= %areaminy)) && (%zoneminz <= %areaminz)) && (%zonemaxx >= %areamaxx)) && (%zonemaxy >= %areamaxy)) && (%zonemaxz >= %areamaxz);
            %this.assert(%contained, " PRIVATESPACE_AREA  must be entirely inside of PRIVATESPACE_ZONEBOX, it looks like the area is outside in this mission, make sure the zonebox surrounds it completely. thanks!");
        }
    }
    if (MissionInfo.mode $= "PrivateSpaceGrid")
    {
        %this.assertDifferentString(MissionInfo.modelID, "", "For privatespace grid servers, a modelID must be specified in MissionInfo, this is the type of floorplan supported by this server");
        %this.assertDifferentString(MissionInfo.building, "", "For privatespace grid servers, a building must be specified in MissionInfo, this is the building that connects to this grid server");
        %this.assertDifferentString(MissionInfo.spacePrefix, "", "For privatespace grid servers, a spacePrefix must be specified in MissionInfo, this is the prefix that will be used to name each space");
    }
    return ;
}
function TEST_MISSIONGROUPINTEGRITY::CheckDatablockSetup(%this)
{
    %group = DataBlockGroup;
    if (%this.assert(isObject(%group), "no DataBlockGroup!"))
    {
        return ;
    }
    %n = %group.getCount() - 1;
    while (%n >= 0)
    {
        %obj = %group.getObject(%n);
        if (%this.assert(isObject(%obj), "non-object in DataBlockGroup!"))
        {
            continue;
        }
        if (%obj.hasMethod("checkIntegrity"))
        {
            %obj.checkIntegrity(%this);
        }
        %n = %n - 1;
    }
}

function TEST_MISSIONGROUPINTEGRITY::CheckUniqueObjectNames(%this)
{
    %group = MissionGroup;
    if (%this.assert(isObject(%group), "no MissionGroup!"))
    {
        return ;
    }
    %nameMap = safeNewScriptObject("StringMap", "", 0);
    %this._checkUniqueObjectNames_Recursive(%group, %nameMap);
    %nameMap.delete();
    return ;
}
function TEST_MISSIONGROUPINTEGRITY::_checkUniqueObjectNames_Recursive(%this, %obj, %nameMap)
{
    %objName = %obj.getName();
    if (!(%objName $= ""))
    {
        if (%nameMap.hasKey(%objName))
        {
            %otherObj = %nameMap.get(%objName);
            %this.assert(!%this.objectInstanceMustHaveUniqueName(%obj), "object \"" @ getDebugString(%obj) @ "\" must have a unique name.");
            %this.assert(!%this.objectInstanceMustHaveUniqueName(%otherObj), "object \"" @ getDebugString(%otherObj) @ "\" must have a unique name.");
        }
        else
        {
            %nameMap.put(%objName, %obj);
        }
    }
    if (%obj.isClassSimSet())
    {
        %num = %obj.getCount();
        %n = 0;
        while (%n < %num)
        {
            %child = %obj.getObject(%n);
            %this._checkUniqueObjectNames_Recursive(%child, %nameMap);
            %n = %n + 1;
        }
    }
}

function TEST_MISSIONGROUPINTEGRITY::CheckPaperDollSKUs(%this)
{
    paperDoll_InitPermutations();
    return ;
}
function MissionMarkerData::checkIntegrity(%this, %testCase)
{
    if (!%testCase.isProtectedSitAnimException(%this.sitIdle))
    {
        %isProtected = ProtectedAnimsDict.hasKey(%this.sitIdle);
        %testCase.assert(%isProtected, "seat marker datablock \"" @ %this.getName() @ "\" has a non-protected idle anim, \"" @ %this.sitIdle @ "\". You may want to add it to initializeProtectedAnims().");
    }
    return ;
}
function TEST_MISSIONGROUPINTEGRITY::IsAbleToNotCache(%this, %obj)
{
    %classname = %obj.getClassName();
    %ableToNotCache = 0;
    %i = 0;
    while (%ableToNotCache == 0)
    {
        if (%this.ableToNotCacheClass[%i] $= %classname)
        {
            %ableToNotCache = 1;
        }
        %i = %i + 1;
    }
    return %ableToNotCache;
}
function TEST_MISSIONGROUPINTEGRITY::getInitialNetCacheable(%this, %obj)
{
    if (!%obj.isClassNetObject())
    {
        return 0;
    }
    %classname = %obj.getClassName();
    %ret = 1;
    %i = 0;
    while (%ret == 1)
    {
        if (%this.InitiallyNotNetCacheableClass[%i] $= %classname)
        {
            %ret = 0;
        }
        %i = %i + 1;
    }
    return %ret;
}
function TEST_MISSIONGROUPINTEGRITY::RecursivelyCheckForThingsThatDontBelong(%this, %obj, %parentGroupName)
{
    %this.assert(isObject(%obj), %parentGroupName SPC ", the object " @ %obj @ "is not an object");
    if (!isObject(%obj))
    {
        return ;
    }
    if (%obj.isClassSimSet())
    {
        %num = %obj.getCount();
        %n = 0;
        while (%n < %num)
        {
            %this.RecursivelyCheckForThingsThatDontBelong(%obj.getObject(%n), %obj.getName());
            %n = %n + 1;
        }
    }
    %classname = %obj.getClassName();
    %belongs = 0;
    %i = 0;
    while (%i < %this.okClassCount)
    {
        if (%this.okClass[%i] $= %classname)
        {
            %belongs = 1;
        }
        %i = %i + 1;
    }
    %actionNeeded = "This object does not belong and should probably be deleted";
    %ableToNotCache = %this.IsAbleToNotCache(%obj);
    if (%ableToNotCache == 0)
    {
        %exceptions = "SimGroup SimSet SimSpace ScriptObject";
        %wordLoc = findWord(%exceptions, %classname);
        if (%wordLoc < 0)
        {
            %this.assert(%obj.isNetCacheable == 1, "this object is a type that should be able to cache, you should set isNetCacheable to 1" SPC getDebugString(%obj));
        }
    }
    if (%obj.isClassSimSpace())
    {
        if (!(getSubStr(%obj.getName(), 0, 9) $= "SimSpace_"))
        {
            %belongs = 0;
            %actionNeeded = "SimSpaces should be named starting with \"SimSpace_\"." SPC %obj.getName() SPC "is breakin\' the law!";
        }
        if (!%obj.hasSpaceDef())
        {
            %belongs = 0;
            %actionNeeded = "SimSpace" SPC %obj.getName() SPC "has no associated SpaceDef.";
        }
    }
    if (%obj.isClassAIPlayer())
    {
        %this.assert(NPCGroup.getObjectIndex(%obj) > -1, "AIPlayer is not in NPCGroup:" SPC getDebugString(%obj));
        if (isObject(%this.NPCNameMap) && (%this.NPCNameMap.findKey(%obj.getName()) == -1))
        {
            %belongs = 0;
            %actionNeeded = "NPC" SPC %obj.getName() SPC "should be listed in npc_usernames.txt.";
        }
    }
    if (%classname $= "MissionMarker")
    {
        %dbName = %obj.getDataBlock().getName();
        %seatMarkerFound = strstr(%dbName, "SeatMarker");
        if (%seatMarkerFound != -1)
        {
            %belongs = 0;
            %actionNeeded = "This is an old style seat marker and shuold be converted to an ETSSeatMarker";
        }
        if (!%this.isProtectedSitAnimException(%obj.sitIdle))
        {
            %isProtected = ProtectedAnimsDict.hasKey(%obj.sitIdle);
            %this.assert(%isProtected, "seat marker \"" @ %obj.getName() @ "\" has a non-protected idle anim, \"" @ %obj.sitIdle @ "\". You may want to add it to initializeProtectedAnims().");
        }
    }
    if (%classname $= "ETSSeatMarker")
    {
        if (%obj.listeningStation $= 0)
        {
            %belongs = 0;
            %actionNeeded = "ETSSeatMarkers should not have listening station of 0, listening station should be the name of an object or an empty string";
        }
        %this.assert(%obj.isNetCacheable == 0, "seat markers cannot be cached, you should set isNetCacheable to 0 for the seat marker:" SPC getDebugString(%obj) SPC "in the group" SPC %parentGroupName);
        if (!%this.isProtectedSitAnimException(%obj.sitIdle))
        {
            %isProtected = ProtectedAnimsDict.hasKey(%obj.sitIdle);
            %this.assert(%isProtected, "seat marker \"" @ %obj.getName() @ "\" has a non-protected idle anim, \"" @ %obj.sitIdle @ "\". You may want to add it to initializeProtectedAnims().");
        }
    }
    if (%classname $= "ScriptObject")
    {
        if (%obj.getName() $= "MissionInfo")
        {
            %belongs = 1;
        }
        if (%obj.getName() $= "BUILDINGDEF")
        {
            %belongs = 0;
            %actionNeeded = "BUILDINGDEFs are no longer used, the EntryTrigger for the building should be in teh BuildingDefinitions simgroup instead";
        }
        if (%obj.class $= "SalonChairEngageScriptObject")
        {
            %belongs = 1;
        }
    }
    if (%classname $= "Trigger")
    {
        %dbName = %obj.getDataBlock().getName();
        if (%dbName $= "SeatingArea")
        {
            if (%obj.autosit $= "")
            {
                %belongs = 0;
                %actionNeeded = "SeatingArea triggers are only needed if you are using autosit, otherwise it should be deleted";
            }
        }
        if (%dbName $= "DoorTrigger")
        {
            %myDoor = %obj.findMyDoor();
            %this.assert(isObject(%myDoor), %parentGroupName SPC ", the object " @ %myDoor @ "is not an object, this is the door referenced by" SPC getDebugString(%obj));
            %doorsDataBlock = %myDoor.getDataBlock();
            %this.assert(!(%doorsDataBlock $= ""), %parentGroupName SPC ", the object " @ %myDoor @ " is not a datablock based door, you should make the datablock first and place that in the world, not a Static, look in Shapes->Doors for your datablock name, this is the door referenced by" SPC getDebugString(%obj));
        }
    }
    if (MissionInfo.mode $= "PrivateSpaceDesign")
    {
        if (%classname $= "TSStatic")
        {
            %belongs = 0;
            %actionNeeded = "Static Shapes should not be used in individual Private spaces,  instead you should use Dynamic Shapes,   these are TSDynamic instead of TSStatic";
        }
        if (%classname $= "InteriorInstance")
        {
            %isManagingZones = %obj.managezones;
            %this.assert(!%isManagingZones, %parentGroupName SPC ", the object" SPC %obj.getDebugString() SPC " - In a private space, interiors should not be managing zones, set \"managezones\" to zero for this interior instance, and make sure you have a ZoneBox surrounding the entire apartment, the ZoneBox will serve as the zone manager");
        }
        if (%obj.getName() $= "PRIVATESPACE_AREA")
        {
            %dbName = %obj.getDataBlock().getName();
            %this.assertSameString(%dbName, "CustomizableSpaceTriggerData", "the PRIVATESPACE_AREA object should be a CustomizableSpaceTriggerData trigger");
        }
        if (%obj.getName() $= "PRIVATESPACE_INTERIOR")
        {
            %this.assertSameString(%classname, "InteriorInstance", "the PRIVATESPACE_INTERIOR object should be a InteriorInstance object");
            %this.assert(%obj.isNetCacheable == 0, "The PRIVATESPACE_INTERIOR will have the textures on it changed at runtime for this reason, it should have isNetCacheable set to 0, make that change to fix this problem");
        }
    }
    if (!(%obj.dataBlock $= ""))
    {
        %this.assertDifferentString(%obj.dataBlock, %obj.getName(), "You cannot have an object with the same name as a datablock, It\'s a good idea to name your datablocks something like blah_DB so you don\'t accidentally call your object the same name");
    }
    %openSpace = "                                       action to take:";
    %message = "in" SPC %parentGroupName SPC ": object:" SPC %obj SPC "of class:" SPC %classname SPC ", with name:" SPC %obj.getName() NL %openSpace SPC %actionNeeded;
    %this.assert(%belongs == 1, %message);
    return ;
}
function TEST_MISSIONGROUPINTEGRITY::isProtectedSitAnimException(%this, %animName)
{
    %ret = 0;
    if (%animName $= "")
    {
        %ret = 1;
    }
    else
    {
        if (%animName $= "idl1a")
        {
            %ret = 1;
        }
    }
    return %ret;
}
function RecursivelyFixOldStyleSeatingAreaProblems(%obj)
{
    if (!isObject(%obj))
    {
        return ;
    }
    if (%obj.isClassSimGroup())
    {
        %num = %obj.getCount();
        %n = 0;
        while (%n < %num)
        {
            RecursivelyFixOldStyleSeatingAreaProblems(%obj.getObject(%n));
            %n = %n + 1;
        }
    }
    return ;
    %classname = %obj.getClassName();
    if (%classname $= "ETSSeatMarker")
    {
        if (%obj.listeningStation $= 0)
        {
            %obj.listeningStation = "";
            echo("cleared listening station that was 0 for " SPC %obj SPC "of class:" SPC %classname SPC ", with name:" SPC %obj.getName());
        }
    }
    if (%classname $= "Trigger")
    {
        %dbName = %obj.getDataBlock().getName();
        if (%dbName $= "SeatingArea")
        {
            if (%obj.autosit $= "")
            {
                $OLDSEATAREA_KILLER[$OLDSEATAREA_KILLER_COUNT] = %obj.getId() ;
                $OLDSEATAREA_KILLER_COUNT = $OLDSEATAREA_KILLER_COUNT + 1;
            }
        }
    }
    return ;
}
function FixOldStyleSeatingAreaProblems()
{
    echo("getting rid of listeningstations and no longer needed seatingarea triggers ----------------------");
    $OLDSEATAREA_KILLER_COUNT = 0;
    RecursivelyFixOldStyleSeatingAreaProblems(MissionGroup);
    %i = 0;
    while (%i < $OLDSEATAREA_KILLER_COUNT)
    {
        %obj = $OLDSEATAREA_KILLER[%i];
        echo("deleteing no longer needed seating area " SPC %obj SPC "of class:" SPC %obj.getClassName() SPC ", with name:" SPC %obj.getName());
        %obj.delete();
        $OLDSEATAREA_KILLER[$OLDSEATAREA_KILLER_COUNT] = "";
        %i = %i + 1;
    }
    $OLDSEATAREA_KILLER_COUNT = 0;
    echo("done----------------------");
    return ;
}
function Utility::ListDataBlocksNotUsed()
{
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
    while (%i < %v.count)
    {
        if (!%v.uses[%i])
        {
            error(%v.theList[%i] SPC "not used");
        }
        %i = %i + 1;
    }
    echo("");
    error("-----------------------------------------------------");
    %v2.delete();
    %v.delete();
    return ;
}
function UtilityCollectNameVisitor::visitObject(%this, %obj)
{
    if (((((((((((((%obj.getClassName() $= "SimSet") || (%obj.getClassName() $= "ActionMap")) || (%obj.getClassName() $= "GuiControlProfile")) || (%obj.getClassName() $= "StringMap")) || (%obj.getClassName() $= "GuiCursor")) || (%obj.getClassName() $= "Sun")) || (%obj.getClassName() $= "MissionArea")) || (%obj.getClassName() $= "WaterBlock")) || (%obj.getClassName() $= "InteriorInstance")) || (%obj.getClassName() $= "TSStatic")) || (%obj.getClassName() $= "SimObject")) || (%obj.getClassName() $= "ScreenShotUploader")) || (%obj.getClassName() $= "fxSpectrumAnalyzer"))
    {
        return ;
    }
    %this.theList[%this.count] = %obj.getName();
    %this.uses[%this.count] = 0;
    %this.count = %this.count + 1;
    return ;
}
function UtilityDBInUseVisitor::visitObject(%this, %obj)
{
    if (((((((((((((%obj.getClassName() $= "SimSet") || (%obj.getClassName() $= "ActionMap")) || (%obj.getClassName() $= "GuiControlProfile")) || (%obj.getClassName() $= "StringMap")) || (%obj.getClassName() $= "GuiCursor")) || (%obj.getClassName() $= "Sun")) || (%obj.getClassName() $= "MissionArea")) || (%obj.getClassName() $= "WaterBlock")) || (%obj.getClassName() $= "InteriorInstance")) || (%obj.getClassName() $= "TSStatic")) || (%obj.getClassName() $= "SimObject")) || (%obj.getClassName() $= "ScreenShotUploader")) || (%obj.getClassName() $= "fxSpectrumAnalyzer"))
    {
        return ;
    }
    %i = 0;
    while (%i < %this.collector.count)
    {
        if (%obj.getClassName() $= "AudioProfile")
        {
            if (%obj.description.getName() $= %this.collector.theList[%i])
            {
                %this.collector.uses[%i] = %this.collector.uses[%i] + 1;
            }
        }
        else
        {
            if (%obj.getClassName() $= "ParticleEmitterData")
            {
                if (%obj.particles $= %this.collector.theList[%i])
                {
                    %this.collector.uses[%i] = %this.collector.uses[%i] + 1;
                }
            }
            else
            {
                if (%obj.getClassName() $= "PlayerData")
                {
                    if (%obj.splashEmitter[0] $= %this.collector.theList[%i])
                    {
                        %this.collector.uses[%i] = %this.collector.uses[%i] + 1;
                    }
                    if (%obj.splashEmitter[1] $= %this.collector.theList[%i])
                    {
                        %this.collector.uses[%i] = %this.collector.uses[%i] + 1;
                    }
                    if (%obj.splashEmitter[2] $= %this.collector.theList[%i])
                    {
                        %this.collector.uses[%i] = %this.collector.uses[%i] + 1;
                    }
                    if (%obj.Splash $= %this.collector.theList[%i])
                    {
                        %this.collector.uses[%i] = %this.collector.uses[%i] + 1;
                    }
                    if (%obj.dustEmitter $= %this.collector.theList[%i])
                    {
                        %this.collector.uses[%i] = %this.collector.uses[%i] + 1;
                    }
                    if (%obj.DecalData $= %this.collector.theList[%i])
                    {
                        %this.collector.uses[%i] = %this.collector.uses[%i] + 1;
                    }
                    if (%obj.Debris $= %this.collector.theList[%i])
                    {
                        %this.collector.uses[%i] = %this.collector.uses[%i] + 1;
                    }
                    if (%obj.FootSound1 $= %this.collector.theList[%i])
                    {
                        %this.collector.uses[%i] = %this.collector.uses[%i] + 1;
                    }
                    if (%obj.FootSound2 $= %this.collector.theList[%i])
                    {
                        %this.collector.uses[%i] = %this.collector.uses[%i] + 1;
                    }
                    if (%obj.FootSound3 $= %this.collector.theList[%i])
                    {
                        %this.collector.uses[%i] = %this.collector.uses[%i] + 1;
                    }
                    if (%obj.FootShallowSound $= %this.collector.theList[%i])
                    {
                        %this.collector.uses[%i] = %this.collector.uses[%i] + 1;
                    }
                    if (%obj.FootWadingSound $= %this.collector.theList[%i])
                    {
                        %this.collector.uses[%i] = %this.collector.uses[%i] + 1;
                    }
                    if (%obj.FootUnderwaterSound $= %this.collector.theList[%i])
                    {
                        %this.collector.uses[%i] = %this.collector.uses[%i] + 1;
                    }
                    if (%obj.HalloweenfootPuffEmitter $= %this.collector.theList[%i])
                    {
                        %this.collector.uses[%i] = %this.collector.uses[%i] + 1;
                    }
                    if (%obj.HalloweenDecalDataL $= %this.collector.theList[%i])
                    {
                        %this.collector.uses[%i] = %this.collector.uses[%i] + 1;
                    }
                    if (%obj.HalloweenDecalDataR $= %this.collector.theList[%i])
                    {
                        %this.collector.uses[%i] = %this.collector.uses[%i] + 1;
                    }
                }
                else
                {
                    if ((%obj.getClassName() $= "SplashData") && (%obj.getClassName() $= "ExplosionData"))
                    {
                        if (%obj.emitter[0] $= %this.collector.theList[%i])
                        {
                            %this.collector.uses[%i] = %this.collector.uses[%i] + 1;
                        }
                        if (%obj.emitter[1] $= %this.collector.theList[%i])
                        {
                            %this.collector.uses[%i] = %this.collector.uses[%i] + 1;
                        }
                    }
                    else
                    {
                        if (%obj.getClassName() $= "RigidShapeData")
                        {
                            if (%obj.particleTrailEmitter $= %this.collector.theList[%i])
                            {
                                %this.collector.uses[%i] = %this.collector.uses[%i] + 1;
                            }
                            if (%obj.Item $= %this.collector.theList[%i])
                            {
                                %this.collector.uses[%i] = %this.collector.uses[%i] + 1;
                            }
                        }
                        else
                        {
                            if (%obj.getClassName() $= "ShapeBaseImageData")
                            {
                                if (%obj.rigidProjectile $= %this.collector.theList[%i])
                                {
                                    %this.collector.uses[%i] = %this.collector.uses[%i] + 1;
                                }
                                if (%obj.stateEmitter[2] $= %this.collector.theList[%i])
                                {
                                    %this.collector.uses[%i] = %this.collector.uses[%i] + 1;
                                }
                            }
                            else
                            {
                                if (%obj.getClassName() $= "ItemData")
                                {
                                    if (%obj.image $= %this.collector.theList[%i])
                                    {
                                        %this.collector.uses[%i] = %this.collector.uses[%i] + 1;
                                    }
                                }
                                else
                                {
                                    if (%obj.getClassName() $= "MissionMarker")
                                    {
                                        if (%obj.sitSound $= %this.collector.theList[%i])
                                        {
                                            %this.collector.uses[%i] = %this.collector.uses[%i] + 1;
                                        }
                                    }
                                    else
                                    {
                                        if (isObject(%obj.getDataBlock()))
                                        {
                                            if (%obj.getDataBlock().getName() $= %this.collector.theList[%i])
                                            {
                                                %this.collector.uses[%i] = %this.collector.uses[%i] + 1;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        %i = %i + 1;
    }
}

function SimGroup::PrintAllDebugNames(%this)
{
    %v = SimGroupVisitor::construct("DebugStringVisitor");
    SimGroupVisitor::VisitSimgroup(%this, %v);
    %v.delete();
    return ;
}
function DebugStringVisitor::visitObject(%this, %obj)
{
    echo(%obj.getDebugString());
    return ;
}
function testMissionIntegrity()
{
    RunTestCase("TEST_MISSIONGROUPINTEGRITY", "WARNING: About that mission file you just loaded...");
    return ;
}
