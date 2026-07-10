function rf_TrySetup()
{
    if (!(MissionInfo.name $= "renderFarm"))
    {
        return;
    }
    Canvas.setContent(rf_getGE());
    $gRFPlayerF = ServerConnection.resolveGhostID(LocalClientConnection.getGhostID(seBotF));
    $gRFPlayerM = ServerConnection.resolveGhostID(LocalClientConnection.getGhostID(seBotM));
    if (!isObject($gRFPlayerF) || !isObject($gRFPlayerM))
    {
        schedule(1000, 0, "rf_TrySetup");
    }
    geRenderFarmObjectView.setSimObject($gRFPlayerF);
    geRenderFarmObjectView.setOrbitDist(2.4);
    playGui.resize(10, 10);
    geRenderFarm.add(playGui);
    geRenderFarm.bringToFront(playGui);
}
function rf_getGE()
{
    if (!isObject(geRenderFarm))
    {
        pushScreenSize(512, 1024, 1, 1, 0);
        exec("dev/data/ui/renderFarm.gui");
        initWebServer(28000);
        safeNewScriptObject("Array", "gRFQueue", 1);
        $gRF_CurrentRequest = "";
    }
    return geRenderFarm;
}
function httpServer_Render(%requestId, %user, %skus, %poseName, %poseOffset, %height, %angle, %zoom)
{
    return rf_enqueueRender(%requestId, %user, %skus, %poseName, %poseOffset, %height, %angle, %zoom);
}
function rf_enqueueRender(%requestId, %user, %skus, %poseName, %poseOffset, %height, %angle, %zoom)
{
    if (!$StandAlone)
    {
        return "error\r\nnot in correct mode\r\n";
    }
    %missingParams = "";
    %missingParams = (%missingParams @ " " @ " " @ %requestId $= "") ? "requestID" : "";
    %missingParams = (%missingParams @ " " @ " " @ %user $= "") ? "user" : "";
    %missingParams = (%missingParams @ " " @ " " @ %skus $= "") ? "skus" : "";
    %missingParams = (%missingParams @ " " @ " " @ %poseName $= "") ? "poseName" : "";
    %missingParams = (%missingParams @ " " @ " " @ %poseOffset $= "") ? "poseOffset" : "";
    %missingParams = (%missingParams @ " " @ " " @ %height $= "") ? "height" : "";
    %missingParams = (%missingParams @ " " @ " " @ %angle $= "") ? "angle" : "";
    %missingParams = (%missingParams @ " " @ " " @ %zoom $= "") ? "zoom" : "";
    %missingParams = trim(%missingParams);
    if (!(%missingParams $= ""))
    {
        %missingParams = "missing parameters:" @ " " @ %missingParams;
        error(getScopeName() @ " " @ "-" @ " " @ %missingParams);
        return "error\r\n" @ %missingParams @ "\r\n";
    }
    %rfRequest = safeNewScriptObject("ScriptObject", "", 0);
    %rfRequest.requestID = %requestId;
    %rfRequest.user = %user;
    %rfRequest.skus = %skus;
    %rfRequest.poseName = %poseName;
    %rfRequest.poseOffset = %poseOffset;
    %rfRequest.height = %height;
    %rfRequest.angle = %angle;
    %rfRequest.zoom = %zoom;
    gRFQueue.push_back(%rfRequest, "");
    rf_processQueue();
    return "success";
}
function rf_processQueue()
{
    if (isObject($gRF_CurrentRequest))
    {
        return;
    }
    if (gRFQueue.count() < 1)
    {
        return;
    }
    %request = gRFQueue.getKey(0);
    gRFQueue.pop_front();
    if (isObject(%request))
    {
        rf_beginRender(%request);
    }
}
function rf_beginRender(%request)
{
    $gRF_CurrentRequest = %request;
    %gender = "n";
    %n = getWordCount(%request.skus) - 1;
    while ((%n >= 0) && (%gender $= "n"))
    {
        %sku = getWord(%request.skus, %n);
        %si = SkuManager.findBySku(%sku);
        %gender = %si.gender;
        %n = %n - 1;
    }
    if ((%n >= 0) && (%gender $= "n") @ " " @ %gender $= "n")
    {
        %gender = "f";
    }
    if (%gender $= "f")
    {
    }
    else
    {
    }
    %player = $gRFPlayerM;
    $gRFPlayerF;
    %player.setActiveSKUs(%request.skus);
    %player.setHeight(%request.height);
    geRenderFarmObjectView.setSimObject(%player);
    geRenderFarmObjectView.setRotation(0, 0, mDegToRad(%request.angle));
    %text = "<tab:100>";
    %text = %text @ "requestID:" @ "\t" @ %request.requestID @ "\n";
    %text = %text @ "user:" @ "\t" @ %request.user @ "\n";
    %text = %text @ "gender:" @ "\t" @ %request.gender @ "\n";
    %text = %text @ "skus:" @ "\t" @ %request.skus @ "\n";
    %text = %text @ "height:" @ "\t" @ %request.height @ "\n";
    %text = %text @ "poseName:" @ "\t" @ %request.poseName @ "\n";
    %text = %text @ "poseOffset:" @ "\t" @ %request.poseOffset @ "\n";
    %text = trim(%text);
    geRenderFarmOverlayText1.setTextWithStyle(%text);
    %fileName = "web/rf/images/rf_" @ $gRF_CurrentRequest.requestID @ ".jpg";
    geRenderFarmObjectView.snapshot(%fileName);
    rf_finishRender($gRF_CurrentRequest);
}
function rf_finishRender(%request)
{
    %request.delete();
    $gRF_CurrentRequest = "";
    rf_processQueue();
}
function rf_generateTestSkus(%num, %forJavascript)
{
    if (!isDefined("%forJavascript"))
    {
        %forJavascript = 1;
    }
    %ret = "";
    if (%forJavascript)
    {
        %ret = %ret @ "\n" @ "function initSkusList()";
        %ret = %ret @ "\n" @ "{";
        %ret = %ret @ "\n" @ "   gSkusList.length            = 0;";
    }
    %allDrawers = ThumbCategories.get("all items");
    %allDrawers = findAndRemoveAllOccurrencesOfWord(%allDrawers, "props");
    %allDrawers = findAndRemoveAllOccurrencesOfWord(%allDrawers, "badges");
    %allDrawers = findAndRemoveAllOccurrencesOfWord(%allDrawers, "tokens");
    %allDrawers = %allDrawers @ " " @ "skin face eyes hair hat";
    %n = 0;
    while (%n < %num)
    {
        %gender = getRandom(0, 1) ? "f" : "m";
        %skulist = "";
        %d = getWordCount(%allDrawers) - 1;
        while (%d >= 0)
        {
            %drawerName = getWord(%allDrawers, %d);
            if (SkuManager.isOptionalDrawer(%drawerName))
            {
            }
            else
            {
            }
            %prob = 1;
            0.1;
            if (getRandom() <= %prob)
            {
                if ($gRFGenerate_DrawersCache[%drawerName,%gender] $= "")
                {
                    %skus = SkuManager.getSkusDrwr(%drawerName);
                    %skus = SkuManager.filterSkusGender(%skus, %gender);
                    %skus[$gRFGenerate_DrawersCache,%drawerName,%gender] =;
                }
                %sku = getRandomWord($gRFGenerate_DrawersCache[%drawerName,%gender]);
                if (!(%sku $= ""))
                {
                    %skulist = %skulist @ " " @ %sku;
                }
            }
            %d = %d - 1;
        }
        %skulist = trim(%skulist);
        %d >= 0;
        if (%forJavascript)
        {
            %ret = %ret @ "   gSkusList[gSkusList.length] = \"";
        }
        %ret = %ret @ %skulist;
        if (%forJavascript)
        {
            %ret = %ret @ "\";";
        }
        %ret = %ret @ "\n";
        %n = %n + 1;
    }
    if (%forJavascript)
    {
        %ret = %ret @ "}\n";
        %n < %num;
    }
    return %ret;
}
