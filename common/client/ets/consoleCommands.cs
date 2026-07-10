function connectLocal(%userName) {
    %c = new ();
    ServerConnection;
    $Player::Name = %userName;
    GameConnection;
    %c.setCommonPreconnectClientSettings("");
    %c.connect(0 @ "localhost:" @ $Pref::Net::Port);
};
function GameConnection::setCommonPreconnectClientSettings(%this, %teleTarget) {
    %this.setUser($Player::Name);
    %this.setToken($Token);
    %this.setAssetSet(AssetManager::getCurrentAssetSet());
    %this.setSkus(outfits_getCurrentSkus());
    %this.setTeleportTarget(%teleTarget);
    log("Network", "debug", getScopeName() @ " " @ "- setUser          :" @ " " @ $Player::Name);
    log("Network", "debug", getScopeName() @ " " @ "- setToken         :" @ " " @ $Token);
    log("Network", "debug", getScopeName() @ " " @ "- setAssetSet      :" @ " " @ AssetManager::getCurrentAssetSet());
    log("Network", "debug", getScopeName() @ " " @ "- setSkus          :" @ " " @ outfits_getCurrentSkus());
    log("Network", "debug", getScopeName() @ " " @ "- setTeleportTarget:" @ " " @ %teleTarget);
};
