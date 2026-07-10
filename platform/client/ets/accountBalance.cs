function AccountBalanceHud::Initialize(%this) {
    if (!(%this.initialized)) {
        if (!(isObject(AccountBalancePBController))) {
            new ScriptObject(AccountBalancePBController) {
                class = "ProgressBarController";
            };
            if (isObject(MissionCleanup)) {
                AccountBalancePBController.add(MissionCleanup);
            }
        }
        "platform/client/ui/progress_sm_rcap".Initialize(AccountBalancePBController, AccountBalancePBContainer, "platform/client/ui/progress_sm_empty", "platform/client/ui/progress_sm_fill", "platform/client/ui/progress_sm_lcap");
        %this.pulsar = AnimCtrl::newAnimCtrl("2 1", "89 26");
        40.setDelay(%this.pulsar);
        %nums = "00 01 02 03 04 05 06 07 08 09 10 11";
        %i = 0;
        while ((%i < getWordCount(%nums))) {
            %num = getWord(%nums, %i);
            "platform/client/ui/vpoints_pulse/vpoints_pulse_" @ %num @ ".png".addFrame(%this.pulsar);
            %i = (%i + 1.0);
        }
        %this.pulsar.setProfile();
        0.setVisible(%this.pulsar);
        %this.pulsar.add(%this);
        %this.initialized = ETSNonModalProfile @ 1;
        (%i < getWordCount(%nums));
    }
    AccountBalanceHud.update();
};
function AccountBalanceHud::open(%this) {
    %wasVisible = %this.isVisible();
    if (!(%wasVisible)) {
        1.setVisible(%this);
        WindowManager.update();
    }
    if (!($UserPref::UI::ShowAccountHud)) {
        "close".schedule(%this, 5000);
    }
};
function AccountBalanceHud::close(%this) {
    if ($UserPref::UI::ShowAccountHud) {
        return 0;
    }
    if (%this.isVisible()) {
        0.setVisible(%this);
        WindowManager.update();
    }
    return 1;
};
function AccountBalanceHud::startPulse(%this, %numPulses) {
    1.setVisible(%this.pulsar);
    %this.pulsar.start();
    "stopPulse".schedule(%this, ((%numPulses * %this.pulsar.delay) * %this.pulsar.numFrames));
};
function AccountBalanceHud::stopPulse(%this) {
    %this.pulsar.stop();
    0.setVisible(%this.pulsar);
};
function AccountBalanceHud::update(%this) {
    if (!(%this.initialized)) {
        AccountBalanceHud.Initialize();
    }
    if ($UserPref::UI::ShowAccountHud) {
        %this.open();
    }
    %this.close();
    if (isObject(AccountBalanceVPointsText)) {
        commaify($Player::VPoints).setText(AccountBalanceVPointsText);
        commaify($Player::VBux).setText(AccountBalanceVBuxText);
        (1.0 - respektPercentToNextLevel($gMyRespektPoints)).setValue(AccountBalancePBController);
    }
};
