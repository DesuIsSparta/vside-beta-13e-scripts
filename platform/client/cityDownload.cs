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
    if (isObject()) {
        %dltotal = getEstimatedSize();
        packageDownload;
        if ((packageDownload > getCurrentPackageIndex())) {
            $CityDownloadGui::lastCityIndex = getCurrentPackageIndex();
            packageDownload;
            $CityDownloadGui::lastDLNow = 0;
            $CityDownloadGui::lastCityIndex;
        }
        %part = ($CityDownloadGui::lastDLNow - %dlnow);
        DLLoadingPBController;
        $CityDownloadGui::lastDLNow = %dlnow;
        $CityDownloadGui::totalDownloaded = (%part + $CityDownloadGui::totalDownloaded);
        %progressValue = (%dltotal / $CityDownloadGui::totalDownloaded);
        %progressValue.setValue();
    }
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
    if (!(isObject())) {
        class = DLLoadingPBController @ new ScriptObject(DLLoadingPBController) @ "ProgressBarController";
        if (isObject()) {
            add();
        }
    }
    "platform/client/ui/progress_empty".Initialize("platform/client/ui/progress_fill", "", "");
    if ($StandAlone) {
    }
    if (!($missionRunning)) {
        error(getScopeName() @ " " @ "-" @ " " @ $missionRunning[$MsgCat::loading @ "E-MISSION-LD"] @ " " @ $MissionArg @ " " @ getTrace());
        MessageBoxOK("Error", DLLoadingProgressHolder @ " " @ $MissionArg, "quit();", "");
    }
    callBackSink = DLLoadingPBController @ %this @ packageDownload;
    DLLoadingPBController;
    if (!(isActive())) {
        start();
    }
};
function CityDownloadGui::onSleep(%this) {
    $Platform::CanSleepInBackground = 1;
    "".setValue();
    0.setValue();
};
