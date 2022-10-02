function AccountBalanceHud::Initialize(%this)
{
    if (!%this.initialized)
    {
        if (!isObject(AccountBalancePBController))
        {
            new ScriptObject(AccountBalancePBController);
            if (isObject(MissionCleanup))
            {
                MissionCleanup.add(AccountBalancePBController);
            }
        }
        AccountBalancePBController.Initialize(AccountBalancePBContainer, "platform/client/ui/progress_sm_empty", "platform/client/ui/progress_sm_fill", "platform/client/ui/progress_sm_lcap", "platform/client/ui/progress_sm_rcap");
        %this.pulsar = AnimCtrl::newAnimCtrl("2 1", "89 26");
        %this.pulsar.setDelay(40);
        %nums = "00 01 02 03 04 05 06 07 08 09 10 11";
        %i = 0;
        while (%i < getWordCount(%nums))
        {
            %num = getWord(%nums, %i);
            %this.pulsar.addFrame("platform/client/ui/vpoints_pulse/vpoints_pulse_" @ %num @ ".png");
            %i = %i + 1;
        }
        %this.pulsar.setProfile(ETSNonModalProfile);
        %this.pulsar.setVisible(0);
        %this.add(%this.pulsar);
        %this.initialized = 1;
    }
    AccountBalanceHud.update();
    return ;
}
function AccountBalanceHud::open(%this)
{
    %wasVisible = %this.isVisible();
    if (!%wasVisible)
    {
        %this.setVisible(1);
        WindowManager.update();
    }
    if (!$UserPref::UI::ShowAccountHud)
    {
        %this.schedule(5000, "close");
    }
    return ;
}
function AccountBalanceHud::close(%this)
{
    if ($UserPref::UI::ShowAccountHud)
    {
        return 0;
    }
    else
    {
        if (%this.isVisible())
        {
            %this.setVisible(0);
            WindowManager.update();
        }
        return 1;
    }
    return ;
}
function AccountBalanceHud::startPulse(%this, %numPulses)
{
    %this.pulsar.setVisible(1);
    %this.pulsar.start();
    %this.schedule((%numPulses * %this.pulsar.delay) * %this.pulsar.numFrames, "stopPulse");
    return ;
}
function AccountBalanceHud::stopPulse(%this)
{
    %this.pulsar.stop();
    %this.pulsar.setVisible(0);
    return ;
}
function AccountBalanceHud::update(%this)
{
    if (!%this.initialized)
    {
        AccountBalanceHud.Initialize();
    }
    if ($UserPref::UI::ShowAccountHud)
    {
        %this.open();
    }
    else
    {
        %this.close();
    }
    if (isObject(AccountBalanceVPointsText))
    {
        AccountBalanceVPointsText.setText(commaify($Player::VPoints));
        AccountBalanceVBuxText.setText(commaify($Player::VBux));
        AccountBalancePBController.setValue(1 - respektPercentToNextLevel($gMyRespektPoints));
    }
    return ;
}
