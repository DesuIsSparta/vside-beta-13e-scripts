function playerTexturesReload()
{
    %n = PlayerInstanceDict.size() - 1;
    while (%n >= 0)
    {
        %player = PlayerInstanceDict.getValue(%n);
        %player.setActiveSKUs(%player.getActiveSKUs(), 1);
        %n = %n - 1;
    }
}

function changedShowReloadTextures()
{
    MessageBoxOK("Restart required", $MsgCat::VHDClient["A-RESTART"], "");
    %n = 0;
    while (%n < 4)
    {
        MePopupMenuButton.schedule(%n * 300, "setVisible", 0);
        MePopupMenuButton.schedule((%n * 300) + 150, "setVisible", 1);
        %n = %n + 1;
    }
}


