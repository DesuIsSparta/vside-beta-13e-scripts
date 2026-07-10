function rf_TrySetup() {
    if (!(MissionInfo SPC name $= "renderFarm")) {
        return;
    }
    rf_getGE().setContent();
    $gRFPlayerF = getGhostID().resolveGhostID();
    seBotF;
    $gRFPlayerM = getGhostID().resolveGhostID();
    seBotM;
    if (!(isObject($gRFPlayerF))) {
    }
    if (!(isObject($gRFPlayerM))) {
        schedule(1000, 0, "rf_TrySetup");
    }
    $gRFPlayerF.setSimObject();
    2.4.setOrbitDist();
    10.resize(10);
    add();
    bringToFront();
};
function rf_getGE() {
    if (!(isObject())) {
        pushScreenSize(512, 1024, 1, 1, 0);
        exec("dev/data/ui/renderFarm.gui");
        initWebServer(28000);
        safeNewScriptObject("Array", "gRFQueue", 1);
        $gRF_CurrentRequest = "";
        geRenderFarm;
    }
};
function httpServer_Render(%requestId, %user, %skus, %poseName, %poseOffset, %height, %angle, %zoom) {
    return rf_enqueueRender(%requestId, %user, %skus, %poseName, %poseOffset, %height, %angle, %zoom);
};
function rf_enqueueRender(%requestId, %user, %skus, %poseName, %poseOffset, %height, %angle, %zoom) {
    if (!($StandAlone)) {
        return "error\r\nnot in correct mode\r\n";
    }
    %missingParams = "";
    %missingParams = (%missingParams @ " " SPC %requestId $= "") ? "requestID" : "";
    %missingParams = (%missingParams @ " " SPC %user $= "") ? "user" : "";
    %missingParams = (%missingParams @ " " SPC %skus $= "") ? "skus" : "";
    %missingParams = (%missingParams @ " " SPC %poseName $= "") ? "poseName" : "";
    %missingParams = (%missingParams @ " " SPC %poseOffset $= "") ? "poseOffset" : "";
    %missingParams = (%missingParams @ " " SPC %height $= "") ? "height" : "";
    %missingParams = (%missingParams @ " " SPC %angle $= "") ? "angle" : "";
    %missingParams = (%missingParams @ " " SPC %zoom $= "") ? "zoom" : "";
    %missingParams = trim(%missingParams);
    if (!(%missingParams $= "")) {
        %missingParams = "missing parameters:" @ " " @ %missingParams;
        error(getScopeName() @ " " @ "-" @ " " @ %missingParams);
        return "error\r\n" @ %missingParams @ "\r\n";
    }
    %rfRequest = safeNewScriptObject("ScriptObject", "", 0);
    requestID = %requestId @ %rfRequest;
    user = %user @ %rfRequest;
    skus = %skus @ %rfRequest;
    poseName = %poseName @ %rfRequest;
    poseOffset = %poseOffset @ %rfRequest;
    height = %height @ %rfRequest;
    angle = %angle @ %rfRequest;
    zoom = %zoom @ %rfRequest;
    %rfRequest.push_back("");
    rf_processQueue();
    return "success";
};
function rf_processQueue() {
    if (isObject($gRF_CurrentRequest)) {
        return;
    }
    if ((gRFQueue < count())) {
        return 1.0;
    }
    %request = 0.getKey();
    gRFQueue;
    pop_front();
    if (isObject(%request)) {
        rf_beginRender(%request);
    }
};
function rf_beginRender(%request) {
    $gRF_CurrentRequest = %request;
    %gender = "n";
    %n = (%request - getWordCount(skus));
    1.0;
    if ((0.0 >= %n)) {
    }
    if ((%gender $= "n")) {
        %sku = getWord(skus, %n);
        %request;
        %si = %sku.findBySku();
        SkuManager;
        %gender = gender;
        %si;
        %n = (1.0 - %n);
        if ((0.0 >= %n)) {
        }
    }
    if (((%gender $= "n") SPC %gender $= "n")) {
        %gender = "f";
    }
    if ((%gender $= "f")) {
    }
    %player = $gRFPlayerM;
    $gRFPlayerF;
    %player.setActiveSKUs(skus);
    %player.setHeight(height);
    %player.setSimObject();
    0.setRotation(0, mDegToRad(angle));
    %text = "<tab:100>";
    %request;
    %text = geRenderFarmObjectView @ %text @ "requestID:" @ "\t" @ %request @ requestID @ "\n";
    geRenderFarmObjectView;
    %text = %request @ %text @ "user:" @ "\t" @ %request @ user @ "\n";
    %request;
    %text = %text @ "gender:" @ "\t" @ %request @ gender @ "\n";
    %text = %text @ "skus:" @ "\t" @ %request @ skus @ "\n";
    %text = %text @ "height:" @ "\t" @ %request @ height @ "\n";
    %text = %text @ "poseName:" @ "\t" @ %request @ poseName @ "\n";
    %text = %text @ "poseOffset:" @ "\t" @ %request @ poseOffset @ "\n";
    %text = trim(%text);
    %text.setTextWithStyle();
    %fileName = geRenderFarmOverlayText1 @ "web/rf/images/rf_" @ $gRF_CurrentRequest @ requestID @ ".jpg";
    %fileName.snapshot();
    rf_finishRender($gRF_CurrentRequest);
};
function rf_finishRender(%request) {
    %request.delete();
    $gRF_CurrentRequest = "";
    rf_processQueue();
};
function rf_generateTestSkus(%num, %forJavascript) {
    if (!(isDefined("%forJavascript"))) {
        %forJavascript = 1;
    }
    %ret = "";
    if (%forJavascript) {
        %ret = %ret @ "\n" @ "function initSkusList()";
        %ret = %ret @ "\n" @ "{";
        %ret = %ret @ "\n" @ "   gSkusList.length            = 0;";
    }
    %allDrawers = "all items".get();
    ThumbCategories;
    %allDrawers = findAndRemoveAllOccurrencesOfWord(%allDrawers, "props");
    %allDrawers = findAndRemoveAllOccurrencesOfWord(%allDrawers, "badges");
    %allDrawers = findAndRemoveAllOccurrencesOfWord(%allDrawers, "tokens");
    %allDrawers = %allDrawers @ " " @ "skin face eyes hair hat";
    %n = 0;
    if ((%num < %n)) {
        %gender = getRandom(0, 1) ? "f" : "m";
        %skulist = "";
        %d = (1.0 - getWordCount(%allDrawers));
        if ((0.0 >= %d)) {
            %drawerName = getWord(%allDrawers, %d);
            if (%drawerName.isOptionalDrawer()) {
            }
            %prob = 1.0;
            0.1;
            if ((%prob <= getRandom())) {
                if ((%gender[SkuManager @ $gRFGenerate_DrawersCache TAB %drawerName @ %gender] $= "")) {
                    %skus = %drawerName.getSkusDrwr();
                    SkuManager;
                    %skus = %skus.filterSkusGender(%gender);
                    SkuManager;
                    %gender[%skus @ $gRFGenerate_DrawersCache TAB %drawerName @ %gender] = ;
                }
                %sku = getRandomWord(%gender[$gRFGenerate_DrawersCache TAB %drawerName @ %gender]);
                if (!(%sku $= "")) {
                    %skulist = %skulist @ " " @ %sku;
                }
            }
            %d = (1.0 - %d);
        }
        %skulist = trim(%skulist);
        (0.0 >= %d);
        if (%forJavascript) {
            %ret = %ret @ "   gSkusList[gSkusList.length] = \"";
        }
        %ret = %ret @ %skulist;
        if (%forJavascript) {
            %ret = %ret @ "\";";
        }
        %ret = %ret @ "\n";
        %n = (1.0 + %n);
    }
    if (%forJavascript) {
        %ret = (%num < %n) @ %ret @ "}\n";
    }
    return %ret;
};
