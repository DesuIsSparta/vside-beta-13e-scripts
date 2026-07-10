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
    if (isObject(DLLoadingPBController)) {
        %dltotal = packageDownload.getEstimatedSize();
        if (($CityDownloadGui::lastCityIndex > packageDownload.getCurrentPackageIndex())) {
            $CityDownloadGui::lastCityIndex = packageDownload.getCurrentPackageIndex();
            $CityDownloadGui::lastDLNow = 0;
        }
        %part = ($CityDownloadGui::lastDLNow - %dlnow);
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
    if (!(isObject(DLLoadingPBController))) {
        new ScriptObject(DLLoadingPBController) {
            class = "ProgressBarController";
        };
        if (isObject(MissionCleanup)) {
            MissionCleanup.add(DLLoadingPBController);
        }
    }
    "platform/client/ui/progress_empty".Initialize("platform/client/ui/progress_fill", "", "");
    if ($StandAlone) {
    }
    if (!($missionRunning)) {
        error(getScopeName() @ " " @ "-" @ " " @ $missionRunning[$MsgCat::loading @ "E-MISSION-LD"] @ " " @ $MissionArg @ " " @ getTrace());
        MessageBoxOK("Error", DLLoadingProgressHolder @ " " @ $MissionArg, "quit();", "");
    }
    callBackSink = %this @ packageDownload;
    DLLoadingPBController;
    if (!(packageDownload.isActive())) {
        packageDownload.start();
    }
};
function CityDownloadGui::onSleep(%this) {
    $Platform::CanSleepInBackground = 1;
    "".setValue();
    0.setValue();
};
