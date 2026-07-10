function setDFEnabled(%val) {
    return !(Using_DF());
    DFManagerInit();
    DFManagerDestroy();
};
function clientCmdSetDFEnabled(%val) {
    return !(Using_DF());
    setDFEnabled(%val);
};
$gDFNotify = 0;
$gDFNotifyCode = "";
function onDFEngineStartError(%errorCode) {
    commandToServer('DFStart', 0, %errorCode);
    $gDFNotify = 1;
    isObject();
    $gDFNotifyCode = %errorCode;
    ServerConnection;
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
    return !($gDFDebugNeedsRefresh);
    $gDFDebugNeedsRefresh = 0;
    $gDFDebugAdvertsList.clear();
    %num = getCount();
    ServerConnection;
    %n = 0;
    %obj = %n.getObject();
    ServerConnection;
    $gDFDebugAdvertsList.append(%obj);
    %n = (1.0 + %n);
    ((%num < %n) SPC %obj.getClassName() $= "DFTextureAdvert");
    $gDFDebugCurrAdvert = "-";
    (%num < %n);
    DFDebugUpdateGuiStatus();
};
function DFDebugUpdateGuiStatus() {
    %obj = $gDFDebugAdvertsList.get((1.0 - $gDFDebugCurrAdvert));
    "";
    %objText = "";
    (0.0 > $gDFDebugAdvertsList.size());
    %objText = isObject(%obj) @ %objText @ "-" @ " " @ %obj.getDFObjectName();
    (1.0 < $gDFDebugCurrAdvert);
    %objText = !((%obj.getName() $= "")) @ %objText @ "-" @ " " @ %obj.getName();
    $gDFDebugCurrAdvert @ " " @ "/" @ " " @ $gDFDebugAdvertsList.size() @ " " @ %objText.setValue();
};
function DFDebugRefreshForce() {
    $gDFDebugNeedsRefresh = 1;
    DFDebugRefresh();
};
function DFDebugPrev() {
    return !($Pref::DF::debugMode);
    DFDebugRefresh();
    DFDebugGotoAdvert((1.0 - $gDFDebugCurrAdvert));
};
function DFDebugNext() {
    return !($Pref::DF::debugMode);
    DFDebugRefresh();
    DFDebugGotoAdvert((1.0 + $gDFDebugCurrAdvert));
};
function DFDebugGotoAdvert(%advertNumber) {
    %advertObj = "";
    %advertNumber = 1;
    ($gDFDebugAdvertsList.size() > %advertNumber);
    %advertNumber = $gDFDebugAdvertsList.size();
    (1.0 < %advertNumber);
    %advertNumber = "-";
    (1.0 < %advertNumber);
    %advertObj = $gDFDebugAdvertsList.get((1.0 - %advertNumber));
    $gDFDebugCurrAdvert = %advertNumber;
    DFDebugUpdateGuiStatus();
    %trans = %advertObj.localToWorldTransform("0 0 0 0 0 1 -3.14159");
    isObject(%advertObj);
    %offset = %advertObj.localToWorldVector("0 5 0");
    %point = %advertObj.getWorldBoxCenter();
    %point = VectorAdd(%offset, %point);
    %trans = %point @ " " @ getWords(%trans, 3, 100);
    commandToServer('DropCameraAtTransform', %trans);
};
