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
    if (isObject()) {
        commandToServer('DFStart', 0, %errorCode);
    }
    $gDFNotify = 1;
    ServerConnection;
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
    %num = getCount();
    ServerConnection;
    %n = 0;
    if ((%num < %n)) {
        %obj = %n.getObject();
        ServerConnection;
        if ((%obj.getClassName() $= "DFTextureAdvert")) {
            $gDFDebugAdvertsList.append(%obj);
        }
        %n = (1.0 + %n);
    }
    $gDFDebugCurrAdvert = "-";
    (%num < %n);
    DFDebugUpdateGuiStatus();
};
function DFDebugUpdateGuiStatus() {
    if ((1.0 < $gDFDebugCurrAdvert)) {
    }
    if ((0.0 > $gDFDebugAdvertsList.size())) {
    }
    %obj = $gDFDebugAdvertsList.get((1.0 - $gDFDebugCurrAdvert));
    "";
    %objText = "";
    if (isObject(%obj)) {
        %objText = %objText @ "-" @ " " @ %obj.getDFObjectName();
        if (!(%obj.getName() $= "")) {
            %objText = %objText @ "-" @ " " @ %obj.getName();
        }
    }
    $gDFDebugCurrAdvert @ " " @ "/" @ " " @ $gDFDebugAdvertsList.size() @ " " @ %objText.setValue();
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
    DFDebugGotoAdvert((1.0 - $gDFDebugCurrAdvert));
};
function DFDebugNext() {
    if (!($Pref::DF::debugMode)) {
        return;
    }
    DFDebugRefresh();
    DFDebugGotoAdvert((1.0 + $gDFDebugCurrAdvert));
};
function DFDebugGotoAdvert(%advertNumber) {
    %advertObj = "";
    if (($gDFDebugAdvertsList.size() > %advertNumber)) {
        %advertNumber = 1;
    }
    if ((1.0 < %advertNumber)) {
        %advertNumber = $gDFDebugAdvertsList.size();
    }
    if ((1.0 < %advertNumber)) {
        %advertNumber = "-";
    }
    %advertObj = $gDFDebugAdvertsList.get((1.0 - %advertNumber));
    $gDFDebugCurrAdvert = %advertNumber;
    DFDebugUpdateGuiStatus();
    if (isObject(%advertObj)) {
        %trans = %advertObj.localToWorldTransform("0 0 0 0 0 1 -3.14159");
        %offset = %advertObj.localToWorldVector("0 5 0");
        %point = %advertObj.getWorldBoxCenter();
        %point = VectorAdd(%offset, %point);
        %trans = %point @ " " @ getWords(%trans, 3, 100);
        commandToServer('DropCameraAtTransform', %trans);
    }
};
