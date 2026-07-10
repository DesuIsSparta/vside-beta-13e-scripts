function playerTexturesReload() {
    %n = (PlayerInstanceDict.size() - 1.0);
    while ((%n >= 0.0)) {
        %player = PlayerInstanceDict.getValue(%n);
        %player.setActiveSKUs(%player.getActiveSKUs(), 1);
        %n = (%n - 1.0);
    }
};
function changedShowReloadTextures() {
    MessageBoxOK("Restart required", $MsgCat::VHDClient["A-RESTART"], "");
    %n = 0;
    while ((%n < 4.0)) {
        MePopupMenuButton.schedule((%n * 300.0), "setVisible", 0);
        MePopupMenuButton.schedule(((%n * 300.0) + 150.0), "setVisible", 1);
        %n = (%n + 1.0);
    }
};
