function playerTexturesReload() {
    %n = (PlayerInstanceDict.size() - 1.0);
    while ((%n >= 0.0)) {
        %player = %n.getValue(PlayerInstanceDict);
        1.setActiveSKUs(%player, %player.getActiveSKUs());
        %n = (%n - 1.0);
    }
};
function changedShowReloadTextures() {
    MessageBoxOK("Restart required", $MsgCat::VHDClient["A-RESTART"], "");
    %n = 0;
    while ((%n < 4.0)) {
        0.schedule(MePopupMenuButton, (%n * 300.0), "setVisible");
        1.schedule(MePopupMenuButton, ((%n * 300.0) + 150.0), "setVisible");
        %n = (%n + 1.0);
    }
};
