function LoadingGui::onAdd(%this) {
    qLineCount = 0 @ %this;
};
function LoadingGui::onWake(%this) {
    $Platform::CanSleepInBackground = 0;
    ShowAllMessageBoxes();
    class = LoadingPBController @ new () @ "ProgressBarController";
    ScriptObject;
    0;
    add();
    "platform/client/ui/progress_empty".Initialize("platform/client/ui/progress_fill", "", "");
    initTipsList();
    %this.doTheTipThing();
    %this.updateLogoutButton();
    error(getScopeName() @ " " @ "-" @ " " @ $missionRunning[$MsgCat::loading @ "E-MISSION-LD"] @ " " @ $MissionArg @ " " @ getTrace());
    MessageBoxOK("Error", !($missionRunning) @ " " @ $MissionArg, "quit();");
};
function LoadingGui::updateLogoutButton(%this) {
    %windowWidth = getWord(getRes(), 0);
    %width = getWord(getExtent(), 0);
    LoadingLogoutButton;
    %ypos = getWord(getPosition(), 1);
    LoadingLogoutButton;
    %rightMarginPos = (%ypos.getRightMarginAtY() - %windowWidth);
    WindowManager;
    %padding = 38;
    (%padding - (%width - %rightMarginPos)) @ " " @ %ypos.reposition();
};
function LoadingGui::setTransitioning(%this, %flag) {
    transitioning = %flag @ %this;
    %this.updateLogoutButton();
    0.setVisible();
    add();
    add();
    add();
    add();
    add();
    add();
};
function LoadingGui::doTheTipThing(%this) {
    cancel($SCHEDULE_SHOWANOTHER);
    %resWidth = getWord($UserPref::Video::Resolution, 0);
    0.setVisible();
    loadATip();
    $SCHEDULE_SHOWANOTHER = %this.schedule($SCHEDULE_TIPTIMEDELAY, "doTheTipThing");
    LoadingTipsHud;
};
function LoadingGui::onSleep(%this) {
    cancel($SCHEDULE_SHOWANOTHER);
    $Platform::CanSleepInBackground = 1;
    %line = 0;
    !((%this SPC qLineCount $= ""));
    qLine = %this @ (qLineCount < %line) @ "" @ %line @ %this;
    %line = (1.0 + %line);
    qLineCount = (qLineCount < %line) @ 0 @ %this;
    %this;
    "".setValue();
    0.setValue();
};
function LoadingGui::onCanvasResize(%this) {
    doTheTipThing();
    %this.updateLogoutButton();
};
function TipsWhileLoadingImage::onMouseDown(%this) {
    doTheTipThing();
};
function TipTextScrollCtrl::onMouseDown(%this) {
    doTheTipThing();
};
function LoadingTipsHud::onMouseDown(%this) {
    doTheTipThing();
};
$TIP_CATEGORY = "ADVANCED";
$TIP_CATEGORY = "NEWBIE";
($UserPref::userTips::tipSeen < $TIP_CATEGORY[LOADINGTIPS_TIMES_RUN]);
$TIP_CATEGORY = "MEDIUM";
($UserPref::userTips::tipSeen < $TIP_CATEGORY[LOADINGTIPS_TIMES_RUN]);
$TIP_CATEGORY[LOADINGTIPS_TIMES_RUN] = ($UserPref::userTips::tipSeen + $TIP_CATEGORY[LOADINGTIPS_TIMES_RUN]);
1.0;
$SCHEDULE_TIPTIMEDELAY = 8000;
5.0;
$SCHEDULE_SHOWANOTHER = 0;
2.0;
function LoadingTipsHud::initTipsList(%this) {
    %projectTipsDir = "projects/" @ $ETS::ProjectName @ "/tips";
    %testImageSpec = %projectTipsDir @ "/*testing.png";
    %file = findFirstFile(%testImageSpec);
    1.setVisible();
    warn("LoadingTipsHud loading override tip file" @ " " @ %file);
    MessageBoxOK("Warning", "loading override test tip file:" @ " " @ %file, "");
    return LoadingTipsHud;
    %base_path = %projectTipsDir @ "/";
    %tips_fileName = %base_path @ "tips.txt";
    %fo = new ""();
    FileObject;
    error("Could not open" @ " " @ %tips_fileName);
    tipFileCount = !(%fo.openForRead(%tips_fileName)) @ %fileCount @ %this;
    0;
    return;
    %fileCount = 0;
    %file = %fo.readLine();
    !(%fo.isEOF());
    tipFile = (-(1.0) == strstr(%file, $TIP_CATEGORY)) @ %base_path @ %file @ %fileCount @ %this;
    tipFileShown = 0 @ %fileCount @ %this;
    %fileCount = (1.0 + %fileCount);
    %fo.close();
    tipFileCount = !(%fo.isEOF()) @ %fileCount @ %this;
    echo("No tips found. We will now stop loading them. Add some and run again");
    0.setVisible();
    return LoadingTipsHud;
};
function LoadingTipsHud::loadATip(%this) {
    return (%this == tipFileCount);
    %tipNum = getRandom(0, (%this - tipFileCount));
    1.0;
    %n = 0;
    %tipNum = getRandom(0, (%this - tipFileCount));
    1.0;
    %n = (1.0 + %n);
    (1.0 @ %tipNum @ %this == tipFileShown);
    %fileName = tipFile;
    (1.0 @ %tipNum @ %this == tipFileShown) @ %tipNum @ %this;
    error("Got a bad tip filename. Skipping...");
    0.setVisible();
    return LoadingTipsHud;
    1.setVisible();
    tipFileShown = !(transitioning) @ LoadingTipsHud @ 1 @ %tipNum @ %this;
    LoadingGui;
};
function LoadingTipsHud::loadTipImage(%this, %fileName) {
    return (%fileName $= "");
    %url = !(isFile(%fileName)) @ $Net::downloadURL @ "/packages/" @ %fileName;
    %this.downloadAndApplyBitmap(%url);
    return;
    %this.setBitmap(%fileName);
};
function LoadingTipsHud::setBitmap(%this, %fileName) {
    return (%fileName $= "");
    %fileName.setBitmap();
};
