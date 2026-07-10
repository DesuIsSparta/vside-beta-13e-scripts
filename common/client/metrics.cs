exec("~/ui/FrameOverlayGui.gui");
function fpsMetricsCallback() {
    return " FPS: " @ $FPS::real @ "  mspf: " @ ($FPS::real / 1000.0);
};
function keyboardMetricsCallback() {
    return "  modifiers: " @ $Keyboard::modifierKeys;
};
function terrainMetricsCallback() {
    return fpsMetricsCallback() @ "  Terrain -" @ "  L0: " @ $T2::levelZeroCount @ "  FMC: " @ $T2::fullMipCount @ "  DTC: " @ $T2::dynamicTextureCount @ "  UNU: " @ $T2::unusedTextureCount @ "  STC: " @ $T2::staticTextureCount @ "  DTSU: " @ $T2::textureSpaceUsed @ "  STSU: " @ $T2::staticTSU @ "  FRB: " @ $T2::FogRejections;
};
function videoMetricsCallback() {
    return fpsMetricsCallback() @ "  Video -" @ "  TC: " @ ($OpenGL::triCount3 + ($OpenGL::triCount2 + ($OpenGL::triCount1 + $OpenGL::triCount0))) @ "  PC: " @ ($OpenGL::primCount3 + ($OpenGL::primCount2 + ($OpenGL::primCount1 + $OpenGL::primCount0))) @ "  T_T: " @ $OpenGL::triCount1 @ "  T_P: " @ $OpenGL::primCount1 @ "  I_T: " @ $OpenGL::triCount2 @ "  I_P: " @ $OpenGL::primCount2 @ "  TS_T: " @ $OpenGL::triCount3 @ "  TS_P: " @ $OpenGL::primCount3 @ "  ?_T: " @ $OpenGL::triCount0 @ "  ?_P: " @ $OpenGL::primCount0;
};
function interiorMetricsCallback() {
    return fpsMetricsCallback() @ "  Interior --" @ "  NTL: " @ $Video::numTexelsLoaded @ "  TRP: " @ $Video::texResidentPercentage @ "  INP: " @ $Metrics::Interior::numPrimitives @ "  INT: " @ $Matrics::Interior::numTexturesUsed @ "  INO: " @ $Metrics::Interior::numInteriors;
};
function textureMetricsCallback() {
    return fpsMetricsCallback() @ "  Texture --" @ "  NTL: " @ $Video::numTexelsLoaded @ "  TRP: " @ $Video::texResidentPercentage @ "  TCM: " @ $Video::textureCacheMisses;
};
function waterMetricsCallback() {
    return fpsMetricsCallback() @ "  Water --" @ "  Tri#: " @ $T2::waterTriCount @ "  Pnt#: " @ $T2::waterPointCount @ "  Hz#: " @ $T2::waterHazePointCount;
};
function timeMetricsCallback() {
    return fpsMetricsCallback() @ "  Time -- " @ "  Sim Time: " @ getSimTime() @ "  Mod: " @ (32 % getSimTime());
};
function vehicleMetricsCallback() {
    return fpsMetricsCallback() @ "  Vehicle --" @ "  R: " @ $Vehicle::retryCount @ "  C: " @ $Vehicle::searchCount @ "  P: " @ $Vehicle::polyCount @ "  V: " @ $Vehicle::vertexCount;
};
function audioMetricsCallback() {
    return fpsMetricsCallback() @ "  Audio --" @ " OH:  " @ $Audio::numOpenHandles @ " OLH: " @ $Audio::numOpenLoopingHandles @ " AS:  " @ $Audio::numActiveStreams @ " NAS: " @ $Audio::numNullActiveStreams @ " LAS: " @ $Audio::numActiveLoopingStreams @ " LS:  " @ $Audio::numLoopingStreams @ " ILS: " @ $Audio::numInactiveLoopingStreams @ " CLS: " @ $Audio::numCulledLoopingStreams;
};
function debugMetricsCallback() {
    return fpsMetricsCallback() @ "  Debug --" @ "  NTL: " @ $Video::numTexelsLoaded @ "  TRP: " @ $Video::texResidentPercentage @ "  NP:  " @ $Metrics::numPrimitives @ "  NT:  " @ $Metrics::numTexturesUsed @ "  NO:  " @ $Metrics::numObjectsRendered;
};
$metricsNamesList = "";
$metricsNamesList = $metricsNamesList @ "audio";
$metricsNamesList = $metricsNamesList @ " " @ "debug";
$metricsNamesList = $metricsNamesList @ " " @ "fps";
$metricsNamesList = $metricsNamesList @ " " @ "interior";
$metricsNamesList = $metricsNamesList @ " " @ "keyboard";
$metricsNamesList = $metricsNamesList @ " " @ "none";
$metricsNamesList = $metricsNamesList @ " " @ "time";
$metricsNamesList = $metricsNamesList @ " " @ "terrain";
$metricsNamesList = $metricsNamesList @ " " @ "texture";
$metricsNamesList = $metricsNamesList @ " " @ "vehicle";
$metricsNamesList = $metricsNamesList @ " " @ "video";
$metricsNamesList = $metricsNamesList @ " " @ "water";
function metrics(%expr) {
    %cb = "";
    %cb = "audioMetricsCallback()";
    (%expr $= "audio");
    %cb = "debugMetricsCallback()";
    (%expr $= "debug");
    $fps::virtual = 0;
    (%expr $= "interior");
    $Interior::numPolys = 0;
    $Interior::numTextures = 0;
    $Interior::numTexels = 0;
    $Interior::numLightmaps = 0;
    $Interior::numLumels = 0;
    %cb = "interiorMetricsCallback()";
    %cb = "fpsMetricsCallback()";
    (%expr $= "fps");
    %cb = "keyboardMetricsCallback()";
    (%expr $= "keyboard");
    %cb = "timeMetricsCallback()";
    (%expr $= "time");
    %cb = "terrainMetricsCallback()";
    (%expr $= "terrain");
    GLEnableMetrics(1);
    %cb = "textureMetricsCallback()";
    (%expr $= "texture");
    %cb = "videoMetricsCallback()";
    (%expr $= "video");
    %cb = "vehicleMetricsCallback()";
    (%expr $= "vehicle");
    %cb = "waterMetricsCallback()";
    (%expr $= "water");
    1000.pushDialog();
    %cb.setValue();
    GLEnableMetrics(0);
    popDialog();
};
