$CityDownloadGui::lastDLNow = 0;
$CityDownloadGui::lastCityIndex = 0;
$CityDownloadGui::totalDownloaded = 0;
function CityDownloadGui::onDone(%this) {
    echo("Finished downloading updated cities.");
    1.setValue();
    0.setTransitioning();
    "LoadingGui".setContent();
    $lastVURL.doServerJoin();
};
function CityDownloadGui::onProgress(%this, %dltotal, %dlnow) {
    %dltotal = getEstimatedSize();
    packageDownload;
    $CityDownloadGui::lastCityIndex = getCurrentPackageIndex();
    packageDownload;
    $CityDownloadGui::lastDLNow = 0;
    (packageDownload > getCurrentPackageIndex());
    %part = ($CityDownloadGui::lastDLNow - %dlnow);
    $CityDownloadGui::lastCityIndex;
    $CityDownloadGui::lastDLNow = %dlnow;
    isObject();
    $CityDownloadGui::totalDownloaded = (%part + $CityDownloadGui::totalDownloaded);
    DLLoadingPBController;
    %progressValue = (%dltotal / $CityDownloadGui::totalDownloaded);
    %progressValue.setValue();
};
function CityDownloadGui::open(%this) {
    $Video::allowResize = 0;
    $CityDownloadGui::totalDownloaded = 0;
    $CityDownloadGui::lastDLNow = 0;
    $CityDownloadGui::lastCityIndex = 0;
};
function CityDownloadGui::close(%this) {
    %this.setVisible(0);
    $Video::allowResize = 1;
};
function CityDownloadGui::onWake(%this) {
    $Platform::CanSleepInBackground = 0;
    class = DLLoadingPBController @ new () @ "ProgressBarController";
    ScriptObject;
    0;
    add();
    "platform/client/ui/progress_empty".Initialize("platform/client/ui/progress_fill", "", "");
    error(getScopeName() @ " " @ "-" @ " " @ $missionRunning[$MsgCat::loading @ "E-MISSION-LD"] @ " " @ $MissionArg @ " " @ getTrace());
    MessageBoxOK("Error", !($missionRunning) @ " " @ $MissionArg, "quit();", "");
    callBackSink = $StandAlone @ %this @ packageDownload;
    DLLoadingProgressHolder;
    start();
};
function CityDownloadGui::onSleep(%this) {
    $Platform::CanSleepInBackground = 1;
    "".setValue();
    0.setValue();
};
