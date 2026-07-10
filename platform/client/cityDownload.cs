$CityDownloadGui::lastDLNow = 0;
$CityDownloadGui::lastCityIndex = 0;
$CityDownloadGui::totalDownloaded = 0;
function CityDownloadGui::onDone(%this) {
    echo("Finished downloading updated cities.");
    1.setValue(DLLoadingPBController);
    0.setTransitioning(LoadingGui);
    "LoadingGui".setContent(Canvas);
    $lastVURL.doServerJoin(WorldMap);
};
function CityDownloadGui::onProgress(%this, %dltotal, %dlnow) {
    if (isObject(DLLoadingPBController)) {
        %dltotal = packageDownload.getEstimatedSize();
        if ((packageDownload.getCurrentPackageIndex() > $CityDownloadGui::lastCityIndex)) {
            $CityDownloadGui::lastCityIndex = packageDownload.getCurrentPackageIndex();
            $CityDownloadGui::lastDLNow = 0;
        }
        %part = (%dlnow - $CityDownloadGui::lastDLNow);
        $CityDownloadGui::lastDLNow = %dlnow;
        $CityDownloadGui::totalDownloaded = ($CityDownloadGui::totalDownloaded + %part);
        %progressValue = ($CityDownloadGui::totalDownloaded / %dltotal);
        %progressValue.setValue(DLLoadingPBController);
    }
};
function CityDownloadGui::open(%this) {
    $Video::allowResize = 0;
    $CityDownloadGui::totalDownloaded = 0;
    $CityDownloadGui::lastDLNow = 0;
    $CityDownloadGui::lastCityIndex = 0;
};
function CityDownloadGui::close(%this) {
    0.setVisible(%this);
    $Video::allowResize = 1;
};
function CityDownloadGui::onWake(%this) {
    $Platform::CanSleepInBackground = 0;
    if (!(isObject(DLLoadingPBController))) {
        new ScriptObject(DLLoadingPBController) {
            class = "ProgressBarController";
        };
        if (isObject(MissionCleanup)) {
            DLLoadingPBController.add(MissionCleanup);
        }
    }
    "".Initialize(DLLoadingPBController, DLLoadingProgressHolder, "platform/client/ui/progress_empty", "platform/client/ui/progress_fill", "");
    if ($StandAlone) {
    }
    if (!($missionRunning)) {
        error(getScopeName() @ " " @ "-" @ " " @ $missionRunning[$MsgCat::loading @ "E-MISSION-LD"] @ " " @ $MissionArg @ " " @ getTrace());
        MessageBoxOK("Error",  @ " " @ $MissionArg, "quit();", "");
    }
    packageDownload.callBackSink = %this;
    if (!(packageDownload.isActive())) {
        packageDownload.start();
    }
};
function CityDownloadGui::onSleep(%this) {
    $Platform::CanSleepInBackground = 1;
    "".setValue(DLLoadingProgressText);
    0.setValue(DLLoadingPBController);
};
