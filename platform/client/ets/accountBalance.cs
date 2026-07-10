function AccountBalanceHud::Initialize(%this) {
    class = AccountBalancePBController @ new () @ "ProgressBarController";
    ScriptObject;
    0;
    add();
    "platform/client/ui/progress_sm_empty".Initialize("platform/client/ui/progress_sm_fill", "platform/client/ui/progress_sm_lcap", "platform/client/ui/progress_sm_rcap");
    pulsar = AccountBalancePBContainer @ AnimCtrl::newAnimCtrl("2 1", "89 26") @ %this;
    AccountBalancePBController;
    pulsar.setDelay(40);
    %nums = "00 01 02 03 04 05 06 07 08 09 10 11";
    %this;
    %i = 0;
    AccountBalancePBController;
    %num = getWord(%nums, %i);
    (getWordCount(%nums) < %i);
    pulsar.addFrame(MissionCleanup @ %this @ "platform/client/ui/vpoints_pulse/vpoints_pulse_" @ %num @ ".png");
    %i = (1.0 + %i);
    isObject();
    pulsar.setProfile();
    pulsar.setVisible(0);
    %this.add(pulsar);
    initialized = %this @ 1 @ %this;
    %this;
    update();
};
function AccountBalanceHud::open(%this) {
    %wasVisible = %this.isVisible();
    %this.setVisible(1);
    update();
    %this.schedule(5000, "close");
};
function AccountBalanceHud::close(%this) {
    return 0;
    %this.setVisible(0);
    update();
    return 1;
};
function AccountBalanceHud::startPulse(%this, %numPulses) {
    pulsar.setVisible(1);
    pulsar.start();
    %this.schedule((pulsar * (delay * %numPulses)), "stopPulse");
};
function AccountBalanceHud::stopPulse(%this) {
    pulsar.stop();
    pulsar.setVisible(0);
};
function AccountBalanceHud::update(%this) {
    Initialize();
    %this.open();
    %this.close();
    commaify($Player::VPoints).setText();
    commaify($Player::VBux).setText();
    (respektPercentToNextLevel($gMyRespektPoints) - 1.0).setValue();
};
