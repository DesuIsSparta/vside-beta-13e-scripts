function playerTexturesReload() {
    %n = (1.0 - PlayerInstanceDict.size());
    if ((0.0 >= %n)) {
        %player = %n.getValue();
        PlayerInstanceDict;
        %player.setActiveSKUs(%player.getActiveSKUs(), 1);
        %n = (1.0 - %n);
    }
};
function changedShowReloadTextures() {
    MessageBoxOK("Restart required", , "");
    %n = 0;
    if ((4.0 < %n)) {
        (300.0 * %n).schedule("setVisible", 0);
        (150.0 + (300.0 * %n)).schedule("setVisible", 1);
        %n = (1.0 + %n);
        MePopupMenuButton;
    }
};
