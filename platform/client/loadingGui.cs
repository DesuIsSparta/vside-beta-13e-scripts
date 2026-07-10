function LoadingGui::onAdd(%this) {
    qLineCount = 0 @ %this;
};
function LoadingGui::onWake(%this) {
    $Platform::CanSleepInBackground = 0;
    ShowAllMessageBoxes();
    if (!(isObject())) {
        class = LoadingPBController @ new ScriptObject(LoadingPBController) @ "ProgressBarController";
        if (isObject()) {
            add();
        }
    }
    "platform/client/ui/progress_empty".Initialize("platform/client/ui/progress_fill", "", "");
    initTipsList();
    %this.doTheTipThing();
    %this.updateLogoutButton();
    if ($StandAlone) {
    }
    if (!($missionRunning)) {
        error(getScopeName() @ " " @ "-" @ " " @ $missionRunning[$MsgCat::loading @ "E-MISSION-LD"] @ " " @ $MissionArg @ " " @ getTrace());
        MessageBoxOK("Error", LoadingTipsHud @ " " @ $MissionArg, "quit();");
    }
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
    if (%flag) {
        0.setVisible();
        add();
        add();
        add();
    }
    add();
    add();
    add();
};
function LoadingGui::doTheTipThing(%this) {
    cancel($SCHEDULE_SHOWANOTHER);
    %resWidth = getWord($UserPref::Video::Resolution, 0);
    if ((640.0 < %resWidth)) {
        0.setVisible();
    }
    loadATip();
    $SCHEDULE_SHOWANOTHER = %this.schedule($SCHEDULE_TIPTIMEDELAY, "doTheTipThing");
    LoadingTipsHud;
};
function LoadingGui::onSleep(%this) {
    cancel($SCHEDULE_SHOWANOTHER);
    $Platform::CanSleepInBackground = 1;
    if (!(%this SPC qLineCount $= "")) {
        %line = 0;
        if ((qLineCount < %line)) {
            qLine = %this @ "" @ %line @ %this;
            %line = (1.0 + %line);
        }
    }
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
if (($UserPref::userTips::tipSeen < $TIP_CATEGORY[LOADINGTIPS_TIMES_RUN])) {
    $TIP_CATEGORY = "NEWBIE";
    2.0;
}
if (($UserPref::userTips::tipSeen < $TIP_CATEGORY[LOADINGTIPS_TIMES_RUN])) {
    $TIP_CATEGORY = "MEDIUM";
    5.0;
}
$TIP_CATEGORY[LOADINGTIPS_TIMES_RUN] = ($UserPref::userTips::tipSeen + $TIP_CATEGORY[LOADINGTIPS_TIMES_RUN]);
1.0;
$SCHEDULE_TIPTIMEDELAY = 8000;
$SCHEDULE_SHOWANOTHER = 0;
function LoadingTipsHud::initTipsList(%this) {
    %projectTipsDir = "projects/" @ $ETS::ProjectName @ "/tips";
    %testImageSpec = %projectTipsDir @ "/*testing.png";
    %file = findFirstFile(%testImageSpec);
    if (!(%file $= "")) {
        if (%this.loadTipImage(%file)) {
        }
        if (!(transitioning)) {
            1.setVisible();
        }
        warn("LoadingTipsHud loading override tip file" @ " " @ %file);
        MessageBoxOK("Warning", "loading override test tip file:" @ " " @ %file, "");
        return LoadingTipsHud;
    }
    %base_path = %projectTipsDir @ "/";
    %tips_fileName = %base_path @ "tips.txt";
    %fo = new ""();
    FileObject;
    if (!(%fo.openForRead(%tips_fileName))) {
        error("Could not open" @ " " @ %tips_fileName);
        tipFileCount = 0 @ %fileCount @ %this;
        return;
    }
    %fileCount = 0;
    if (!(%fo.isEOF())) {
        %file = %fo.readLine();
        if ((-(1.0) == strstr(%file, $TIP_CATEGORY))) {
        }
        tipFile = %base_path @ %file @ %fileCount @ %this;
        tipFileShown = 0 @ %fileCount @ %this;
        %fileCount = (1.0 + %fileCount);
    }
    %fo.close();
    tipFileCount = !(%fo.isEOF()) @ %fileCount @ %this;
    if ((0.0 == %fileCount)) {
        echo("No tips found. We will now stop loading them. Add some and run again");
        0.setVisible();
        return LoadingTipsHud;
    }
};
function LoadingTipsHud::loadATip(%this) {
    if ((%this == tipFileCount)) {
        return 0.0;
    }
    %tipNum = getRandom(0, (%this - tipFileCount));
    1.0;
    %n = 0;
    if ((10.0 < %n)) {
    }
    if ((1.0 @ %tipNum @ %this == tipFileShown)) {
        %tipNum = getRandom(0, (%this - tipFileCount));
        1.0;
        %n = (1.0 + %n);
        if ((10.0 < %n)) {
        }
    }
    %fileName = tipFile;
    (1.0 @ %tipNum @ %this == tipFileShown) @ %tipNum @ %this;
    if ((%fileName $= "")) {
        error("Got a bad tip filename. Skipping...");
        0.setVisible();
        return LoadingTipsHud;
    }
    if (%this.loadTipImage(%fileName)) {
    }
    if (!(transitioning)) {
        1.setVisible();
        tipFileShown = LoadingGui @ LoadingTipsHud @ 1 @ %tipNum @ %this;
    }
};
function LoadingTipsHud::loadTipImage(%this, %fileName) {
    if ((%fileName $= "")) {
        return;
    }
    if (!(isFile(%fileName))) {
        %url = $Net::downloadURL @ "/packages/" @ %fileName;
        %this.downloadAndApplyBitmap(%url);
        return;
    }
    %this.setBitmap(%fileName);
};
function LoadingTipsHud::setBitmap(%this, %fileName) {
    if ((%fileName $= "")) {
        return;
    }
    %fileName.setBitmap();
};
