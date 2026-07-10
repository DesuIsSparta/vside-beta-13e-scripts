function setDFEnabled(%val) {
    if (!(isFunction("Using_DF"))) {
    }
    if (!(Using_DF())) {
        return;
    }
    if (%val) {
        DFManagerInit();
    }
    DFManagerDestroy();
};
function clientCmdSetDFEnabled(%val) {
    if (!(isFunction("Using_DF"))) {
    }
    if (!(Using_DF())) {
        return;
    }
    setDFEnabled(%val);
};
$gDFNotify = 0;
$gDFNotifyCode = "";
function onDFEngineStartError(%errorCode) {
    if (isObject(ServerConnection)) {
        commandToServer('DFStart', 0, %errorCode);
    }
    $gDFNotify = 1;
    $gDFNotifyCode = %errorCode;
};
function onDFEngineStarted() {
    commandToServer('DFStart', 1, "");
    $gDFNotify = 0;
    $gDFNotifyCode = "";
};
$gDFDebugNeedsRefresh = 1;
$gDFDebugAdvertsList = new_ScriptArray("");
$gDFDebugCurrAdvert = "-";
function DFDebugRefresh() {
    if (!($gDFDebugNeedsRefresh)) {
        return;
    }
    $gDFDebugNeedsRefresh = 0;
    $gDFDebugAdvertsList.clear();
    %num = ServerConnection.getCount();
    %n = 0;
    while ((%n < %num)) {
        %obj = %n.getObject(ServerConnection);
        if ((%obj.getClassName() $= "DFTextureAdvert")) {
            %obj.append($gDFDebugAdvertsList);
        }
        %n = (%n + 1.0);
    }
    $gDFDebugCurrAdvert = "-";
    (%n < %num);
    DFDebugUpdateGuiStatus();
};
function DFDebugUpdateGuiStatus() {
    if (($gDFDebugCurrAdvert < 1.0)) {
    }
    if (($gDFDebugAdvertsList.size() > 0.0)) {
    }
    %obj = ($gDFDebugCurrAdvert - 1.0).get($gDFDebugAdvertsList);
    "";
    %objText = "";
    if (isObject(%obj)) {
        %objText = %objText @ "-" @ " " @ %obj.getDFObjectName();
        if (!(%obj.getName() $= "")) {
            %objText = %objText @ "-" @ " " @ %obj.getName();
        }
    }
    $gDFDebugCurrAdvert @ " " @ "/" @ " " @ $gDFDebugAdvertsList.size() @ " " @ %objText.setValue(geDFDebugStatusText);
};
function DFDebugRefreshForce() {
    $gDFDebugNeedsRefresh = 1;
    DFDebugRefresh();
};
function DFDebugPrev() {
    if (!($Pref::DF::debugMode)) {
        return;
    }
    DFDebugRefresh();
    DFDebugGotoAdvert(($gDFDebugCurrAdvert - 1.0));
};
function DFDebugNext() {
    if (!($Pref::DF::debugMode)) {
        return;
    }
    DFDebugRefresh();
    DFDebugGotoAdvert(($gDFDebugCurrAdvert + 1.0));
};
function DFDebugGotoAdvert(%advertNumber) {
    %advertObj = "";
    if ((%advertNumber > $gDFDebugAdvertsList.size())) {
        %advertNumber = 1;
    }
    if ((%advertNumber < 1.0)) {
        %advertNumber = $gDFDebugAdvertsList.size();
    }
    if ((%advertNumber < 1.0)) {
        %advertNumber = "-";
    }
    %advertObj = (%advertNumber - 1.0).get($gDFDebugAdvertsList);
    $gDFDebugCurrAdvert = %advertNumber;
    DFDebugUpdateGuiStatus();
    if (isObject(%advertObj)) {
        %trans = "0 0 0 0 0 1 -3.14159".localToWorldTransform(%advertObj);
        %offset = "0 5 0".localToWorldVector(%advertObj);
        %point = %advertObj.getWorldBoxCenter();
        %point = VectorAdd(%offset, %point);
        %trans = %point @ " " @ getWords(%trans, 3, 100);
        commandToServer('DropCameraAtTransform', %trans);
    }
};
